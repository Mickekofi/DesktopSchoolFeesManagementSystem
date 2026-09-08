Imports System.Data
Imports System.Data.OleDb
Imports MySql.Data.MySqlClient
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Text.RegularExpressions

Public Class UC_EntrollStudents

    Private bsApplicants As New BindingSource()
    Private dtApplicants As New DataTable()

    ' The visual warning component you requested
    Private uploadValidator As New ErrorProvider()

    ' ======================================================================
    ' INITIALIZATION & UI SETUP
    ' ======================================================================
    Private Sub UC_EntrollStudents_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ShapeUtilities.RadiusButton(btnUpload, 1.5F)
        ShapeUtilities.RadiusButton(btnDelete, 1.5F)

        DataGridViewHelper.ApplyBeautifulStyle(dgvApplicants)
        DataGridViewHelper.AdjustDataGridViewRowHeight(dgvApplicants, 45)

        dgvApplicants.AllowUserToAddRows = False
        dgvApplicants.ReadOnly = True
        dgvApplicants.SelectionMode = DataGridViewSelectionMode.FullRowSelect

        uploadValidator.BlinkStyle = ErrorBlinkStyle.BlinkIfDifferentError

        PopulateProgramComboBox()
        LoadLiveDatabaseToGrid()
    End Sub

    ' ======================================================================
    ' CORE DATA PIPELINE: MYSQL TO GRID
    ' ======================================================================
    Private Sub LoadLiveDatabaseToGrid()
        Dim query As String = "
            SELECT 
                s.index_number AS 'Index Number', 
                s.full_name AS 'Full Name', 
                p.program_name AS 'Program Name', 
                s.admission_year AS 'Admission Year', 
                s.default_password AS 'Default Password',
                s.created_at
            FROM students s
            LEFT JOIN programs p ON s.program_id = p.id
            ORDER BY s.created_at DESC"

        dtApplicants.Clear()

        Try
            Using conn As MySqlConnection = Database.CreateOpenConnection()
                Using cmd As New MySqlCommand(query, conn)
                    Using da As New MySqlDataAdapter(cmd)
                        da.Fill(dtApplicants)
                    End Using
                End Using
            End Using

            bsApplicants.DataSource = dtApplicants
            dgvApplicants.DataSource = bsApplicants

            If dgvApplicants.Columns.Contains("created_at") Then
                dgvApplicants.Columns("created_at").Visible = False
            End If

        Catch ex As Exception
            MessageBox.Show($"Failed to fetch student registry: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PopulateProgramComboBox()
        cmbProgramShow.Items.Clear()
        cmbProgramShow.Items.Add("All Programs")

        Dim query As String = "SELECT program_name FROM programs WHERE status = 'Active'"
        Try
            Using conn As MySqlConnection = Database.CreateOpenConnection()
                Using cmd As New MySqlCommand(query, conn)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            cmbProgramShow.Items.Add(reader("program_name").ToString())
                        End While
                    End Using
                End Using
            End Using
            cmbProgramShow.SelectedIndex = 0
        Catch ex As Exception
            ' Fail silently on combo box to prevent crashing UI
        End Try
    End Sub

    ' ======================================================================
    ' VISUAL ROW HIGHLIGHTING
    ' ======================================================================
    Private Sub dgvApplicants_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvApplicants.CellFormatting
        If e.RowIndex >= 0 AndAlso dgvApplicants.Columns.Contains("created_at") Then
            Dim row As DataGridViewRow = dgvApplicants.Rows(e.RowIndex)
            If Not IsDBNull(row.Cells("created_at").Value) Then
                Dim createdAt As DateTime = Convert.ToDateTime(row.Cells("created_at").Value)
                If (DateTime.Now - createdAt).TotalMinutes <= 5 Then
                    row.DefaultCellStyle.BackColor = Color.WhiteSmoke
                End If
            End If
        End If
    End Sub

    ' ======================================================================
    ' EXCEL INGESTION ENGINE (STRICT ATOMICITY & REGEX GATE)
    ' ======================================================================
    Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Dim ofd As New OpenFileDialog()
        ofd.Title = "Select Excel File"
        ofd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx"

        If ofd.ShowDialog() = DialogResult.OK Then
            Try
                ' Reset warnings
                uploadValidator.Clear()
                lblFilePath.Text = ofd.FileName
                lblFilePath.ForeColor = Color.DarkOrange
                Me.Cursor = Cursors.WaitCursor

                ' 1. Read Excel into memory
                Dim rawExcelData As DataTable = LoadExcelToDataTable(ofd.FileName)

                ' 2. Push directly to MySQL with Strict Validation Gate
                Dim recordsAdded As Integer = ImportToDatabase(rawExcelData)

                ' If recordsAdded is 0, it means validation failed or no new records existed
                If recordsAdded > 0 Then
                    LoadLiveDatabaseToGrid()
                    lblFilePath.ForeColor = Color.Green
                    MessageBox.Show($"Upload Complete! Successfully imported {recordsAdded} new students.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            Catch ex As Exception
                uploadValidator.SetError(lblFilePath, "System failure during import sequence.")
                MessageBox.Show($"Import sequence failed." & vbCrLf & $"Details: {ex.Message}", "Upload Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                lblFilePath.ForeColor = Color.Red
            Finally
                Me.Cursor = Cursors.Default
            End Try
        End If
    End Sub

    Private Function LoadExcelToDataTable(filePath As String) As DataTable
        Dim dt As New DataTable()
        Dim connStr As String = If(filePath.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase),
            $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={filePath};Extended Properties=""Excel 12.0 Xml;HDR=YES;IMEX=1"";",
            $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={filePath};Extended Properties=""Excel 8.0;HDR=YES;IMEX=1"";")

        Using conn As New OleDbConnection(connStr)
            conn.Open()
            Dim sheetSchema As DataTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
            Dim firstSheetName As String = sheetSchema.Rows(0)("TABLE_NAME").ToString()
            Dim cmd As New OleDbCommand($"SELECT * FROM [{firstSheetName}]", conn)
            Using da As New OleDbDataAdapter(cmd)
                da.Fill(dt)
            End Using
        End Using
        Return dt
    End Function

    Private Function ImportToDatabase(excelData As DataTable) As Integer
        Dim insertCount As Integer = 0
        Dim errorLog As New StringBuilder()

        Using conn As MySqlConnection = Database.CreateOpenConnection()
            Dim programMap As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)
            Using pCmd As New MySqlCommand("SELECT id, program_name FROM programs WHERE status = 'Active'", conn)
                Using reader As MySqlDataReader = pCmd.ExecuteReader()
                    While reader.Read()
                        programMap(reader("program_name").ToString().Trim()) = Convert.ToInt32(reader("id"))
                    End While
                End Using
            End Using

            ' ==================================================
            ' STAGE 1: STRICT REGEX VALIDATION GATE
            ' ==================================================
            Dim excelRowNumber As Integer = 2
            Dim hasCriticalErrors As Boolean = False
            ' Regex to strictly enforce YYYY/YYYY (e.g., 2025/2026)
            Dim yearFormatRegex As New Regex("^\d{4}/\d{4}$")

            For Each row As DataRow In excelData.Rows
                Dim rawIndex As String = If(IsDBNull(row("Index Number")), "", row("Index Number").ToString().Trim())
                Dim rawName As String = If(IsDBNull(row("Full Name")), "", row("Full Name").ToString().Trim())
                Dim rawProgram As String = If(IsDBNull(row("Program Name")), "", row("Program Name").ToString().Trim())
                Dim rawYear As String = If(IsDBNull(row("Admission Year")), "", row("Admission Year").ToString().Trim())
                Dim rawPass As String = If(IsDBNull(row("Default Password")), "", row("Default Password").ToString().Trim())

                If String.IsNullOrWhiteSpace(rawIndex) AndAlso String.IsNullOrWhiteSpace(rawName) AndAlso String.IsNullOrWhiteSpace(rawProgram) Then
                    excelRowNumber += 1
                    Continue For
                End If

                Dim rowErrors As New List(Of String)()

                If String.IsNullOrWhiteSpace(rawIndex) Then rowErrors.Add("Missing Index Number")
                If String.IsNullOrWhiteSpace(rawName) Then rowErrors.Add("Missing Full Name")
                If String.IsNullOrWhiteSpace(rawPass) Then rowErrors.Add("Missing Default Password")

                ' Enforcing strict YYYY/YYYY string format
                If String.IsNullOrWhiteSpace(rawYear) OrElse Not yearFormatRegex.IsMatch(rawYear) Then
                    rowErrors.Add("Invalid Admission Year format (Must be strictly YYYY/YYYY)")
                End If

                If String.IsNullOrWhiteSpace(rawProgram) Then
                    rowErrors.Add("Missing Program Name")
                ElseIf Not programMap.ContainsKey(rawProgram) Then
                    rowErrors.Add($"Program '{rawProgram}' does not exist in database")
                End If

                If rowErrors.Count > 0 Then
                    hasCriticalErrors = True
                    errorLog.AppendLine($"Excel Row {excelRowNumber}: " & String.Join(", ", rowErrors))
                End If

                excelRowNumber += 1
            Next

            If hasCriticalErrors Then
                uploadValidator.SetError(lblFilePath, "Data validation failed. Check Excel file.")
                lblFilePath.ForeColor = Color.Red
                MessageBox.Show("UPLOAD ABORTED. Critical data errors found in your Excel file:" & vbCrLf & vbCrLf &
                                errorLog.ToString() & vbCrLf &
                                "Fix these errors in Excel and try again. No students were inserted.",
                                "Validation Gate Failed", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Return 0
            End If

            ' ==================================================
            ' STAGE 2: STRING DATABASE INSERTION
            ' ==================================================
            Dim insertQuery As String = "
                INSERT IGNORE INTO students (index_number, full_name, program_id, admission_year, default_password, password_hash) 
                VALUES (@Idx, @Name, @ProgId, @Year, @Pass, SHA2(@Pass, 256))"

            Using cmd As New MySqlCommand(insertQuery, conn)
                cmd.Parameters.Add("@Idx", MySqlDbType.VarChar)
                cmd.Parameters.Add("@Name", MySqlDbType.VarChar)
                cmd.Parameters.Add("@ProgId", MySqlDbType.Int32)
                cmd.Parameters.Add("@Year", MySqlDbType.VarChar) ' Accepting the String
                cmd.Parameters.Add("@Pass", MySqlDbType.VarChar)

                For Each row As DataRow In excelData.Rows
                    Dim rawIndex As String = If(IsDBNull(row("Index Number")), "", row("Index Number").ToString().Trim())
                    If String.IsNullOrWhiteSpace(rawIndex) Then Continue For

                    cmd.Parameters("@Idx").Value = rawIndex
                    cmd.Parameters("@Name").Value = row("Full Name").ToString().Trim()
                    cmd.Parameters("@ProgId").Value = programMap(row("Program Name").ToString().Trim())
                    cmd.Parameters("@Year").Value = row("Admission Year").ToString().Trim()
                    cmd.Parameters("@Pass").Value = row("Default Password").ToString().Trim()

                    insertCount += cmd.ExecuteNonQuery()
                Next
            End Using
        End Using

        Return insertCount
    End Function

    ' ======================================================================
    ' LIVE SEARCH & FILTERING
    ' ======================================================================
    Private Sub ApplyFilters()
        If bsApplicants.DataSource Is Nothing Then Return

        Dim filterParts As New List(Of String)
        If cmbProgramShow.SelectedIndex > 0 Then
            Dim selectedProgram = cmbProgramShow.Text.Replace("'", "''")
            filterParts.Add($"[Program Name] = '{selectedProgram}'")
        End If

        If Not String.IsNullOrWhiteSpace(txtIndexNumberSearch.Text) Then
            Dim searchVal = txtIndexNumberSearch.Text.Replace("'", "''")
            filterParts.Add($"(CONVERT([Index Number], 'System.String') LIKE '%{searchVal}%' OR [Full Name] LIKE '%{searchVal}%')")
        End If

        Try
            bsApplicants.Filter = If(filterParts.Count > 0, String.Join(" AND ", filterParts), "")
        Catch ex As Exception
            bsApplicants.Filter = ""
        End Try
    End Sub

    Private Sub cmbProgramShow_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbProgramShow.SelectedIndexChanged
        ApplyFilters()
    End Sub

    Private Sub txtIndexNumberSearch_TextChanged(sender As Object, e As EventArgs) Handles txtIndexNumberSearch.TextChanged
        ApplyFilters()
    End Sub

    ' ======================================================================
    ' ROW DELETION ENGINE
    ' ======================================================================
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If dgvApplicants.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a student row to delete.", "Select Row", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim indexNumber As String = dgvApplicants.SelectedRows(0).Cells("Index Number").Value.ToString()
        Dim studentName As String = dgvApplicants.SelectedRows(0).Cells("Full Name").Value.ToString()

        If MessageBox.Show($"Are you sure you want to permanently delete {studentName} ({indexNumber})? This will also wipe their payment history.", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
            Try
                Using conn As MySqlConnection = Database.CreateOpenConnection()
                    Using cmd As New MySqlCommand("DELETE FROM students WHERE index_number = @Idx", conn)
                        cmd.Parameters.AddWithValue("@Idx", indexNumber)
                        cmd.ExecuteNonQuery()
                    End Using
                End Using
                LoadLiveDatabaseToGrid()
            Catch ex As Exception
                MessageBox.Show($"Deletion failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    ' ======================================================================
    ' DYNAMIC ROW UPDATE EDITOR (STRICT REGEX GATE APPLIED)
    ' ======================================================================
    Private Sub dgvApplicants_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvApplicants.CellDoubleClick
        If e.RowIndex < 0 Then Return

        Dim selectedRow As DataGridViewRow = dgvApplicants.Rows(e.RowIndex)
        Dim currentIndex As String = selectedRow.Cells("Index Number").Value.ToString()
        Dim currentName As String = selectedRow.Cells("Full Name").Value.ToString()
        Dim currentYear As String = selectedRow.Cells("Admission Year").Value.ToString()
        Dim currentProgram As String = selectedRow.Cells("Program Name").Value.ToString()

        Using updateForm As New Form()
            updateForm.Text = $"Update Student: {currentIndex}"
            updateForm.Size = New Size(400, 360)
            updateForm.StartPosition = FormStartPosition.CenterParent
            updateForm.FormBorderStyle = FormBorderStyle.FixedDialog
            updateForm.MaximizeBox = False
            updateForm.MinimizeBox = False

            Dim lblName As New Label() With {.Text = "Full Name:", .Location = New Point(20, 20), .AutoSize = True}
            Dim txtName As New TextBox() With {.Text = currentName, .Location = New Point(20, 40), .Width = 340}

            Dim lblYear As New Label() With {.Text = "Admission Year (YYYY/YYYY):", .Location = New Point(20, 80), .AutoSize = True}
            Dim txtYear As New TextBox() With {.Text = currentYear, .Location = New Point(20, 100), .Width = 340}

            Dim lblProgram As New Label() With {.Text = "Program:", .Location = New Point(20, 140), .AutoSize = True}
            Dim cmbProgram As New ComboBox() With {.Location = New Point(20, 160), .Width = 340, .DropDownStyle = ComboBoxStyle.DropDownList}

            Dim programMap As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)
            Try
                Using conn As MySqlConnection = Database.CreateOpenConnection()
                    Using cmd As New MySqlCommand("SELECT id, program_name FROM programs WHERE status = 'Active'", conn)
                        Using reader As MySqlDataReader = cmd.ExecuteReader()
                            While reader.Read()
                                Dim pName As String = reader("program_name").ToString()
                                Dim pId As Integer = Convert.ToInt32(reader("id"))
                                programMap(pName) = pId
                                cmbProgram.Items.Add(pName)
                            End While
                        End Using
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show($"Failed to load programs: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            If Not String.IsNullOrWhiteSpace(currentProgram) AndAlso cmbProgram.Items.Contains(currentProgram) Then
                cmbProgram.SelectedItem = currentProgram
            End If

            Dim btnSave As New Button() With {.Text = "Save Changes", .Location = New Point(120, 240), .Width = 140, .Height = 40, .BackColor = Color.Teal, .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}

            AddHandler btnSave.Click, Sub(s, ev)
                                          ' 1. Check if Program is Selected
                                          If cmbProgram.SelectedIndex < 0 Then
                                              MessageBox.Show("You must assign a valid program to this student.", "Validation Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                              Return
                                          End If

                                          ' 2. Check if Admission Year matches YYYY/YYYY format
                                          Dim yearFormatRegex As New Regex("^\d{4}/\d{4}$")
                                          If Not yearFormatRegex.IsMatch(txtYear.Text.Trim()) Then
                                              MessageBox.Show("Admission Year must be strictly formatted as YYYY/YYYY (e.g., 2025/2026).", "Validation Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                              Return
                                          End If

                                          Dim selectedProgId As Integer = programMap(cmbProgram.SelectedItem.ToString())

                                          Try
                                              Using conn As MySqlConnection = Database.CreateOpenConnection()
                                                  Dim updateQuery As String = "UPDATE students SET full_name = @Name, admission_year = @Year, program_id = @ProgId WHERE index_number = @Idx"
                                                  Using cmd As New MySqlCommand(updateQuery, conn)
                                                      cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim())
                                                      cmd.Parameters.AddWithValue("@Year", txtYear.Text.Trim())
                                                      cmd.Parameters.AddWithValue("@ProgId", selectedProgId)
                                                      cmd.Parameters.AddWithValue("@Idx", currentIndex)
                                                      cmd.ExecuteNonQuery()
                                                  End Using
                                              End Using
                                              updateForm.DialogResult = DialogResult.OK
                                          Catch ex As Exception
                                              MessageBox.Show($"Update failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                          End Try
                                      End Sub

            updateForm.Controls.AddRange(New Control() {lblName, txtName, lblYear, txtYear, lblProgram, cmbProgram, btnSave})

            If updateForm.ShowDialog() = DialogResult.OK Then
                LoadLiveDatabaseToGrid()
                MessageBox.Show("Student profile securely updated.", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Using
    End Sub

    Private Sub dgvApplicants_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvApplicants.CellContentClick

    End Sub
End Class