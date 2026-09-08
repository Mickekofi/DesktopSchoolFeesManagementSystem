Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Text.RegularExpressions

Public Class UC_ViewPrograms

    Private bsPrograms As New BindingSource()
    Private dtPrograms As New DataTable()

    ' ======================================================================
    ' INITIALIZATION & UI SETUP
    ' ======================================================================
    Private Sub UC_ViewPrograms_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Apply rounded corners
        ShapeUtilities.RadiusButton(btnDelete, 1.5F)

        ' Apply Beautiful UI styling
        DataGridViewHelper.ApplyBeautifulStyle(dgvPrograms)
        DataGridViewHelper.AdjustDataGridViewRowHeight(dgvPrograms, 45)

        ' Secure the grid
        dgvPrograms.AllowUserToAddRows = False
        dgvPrograms.ReadOnly = True
        dgvPrograms.SelectionMode = DataGridViewSelectionMode.FullRowSelect

        PopulateProgramFilter()
        LoadLiveDatabaseToGrid()
    End Sub

    ' ======================================================================
    ' CORE DATA PIPELINE
    ' ======================================================================
    Private Sub LoadLiveDatabaseToGrid()
        Dim query As String = "
            SELECT 
                id AS 'Program ID',
                program_name AS 'Program Name', 
                base_fee AS 'Base Fee', 
                status AS 'Status',
                created_at
            FROM programs 
            ORDER BY program_name ASC"

        dtPrograms.Clear()

        Try
            Using conn As MySqlConnection = Database.CreateOpenConnection()
                Using cmd As New MySqlCommand(query, conn)
                    Using da As New MySqlDataAdapter(cmd)
                        da.Fill(dtPrograms)
                    End Using
                End Using
            End Using

            bsPrograms.DataSource = dtPrograms
            dgvPrograms.DataSource = bsPrograms

            ' Hide backend tracking columns from the user
            If dgvPrograms.Columns.Contains("Program ID") Then dgvPrograms.Columns("Program ID").Visible = False
            If dgvPrograms.Columns.Contains("created_at") Then dgvPrograms.Columns("created_at").Visible = False

            ' Format the fee column to look like actual money
            If dgvPrograms.Columns.Contains("Base Fee") Then
                dgvPrograms.Columns("Base Fee").DefaultCellStyle.Format = "N2"
            End If

        Catch ex As Exception
            MessageBox.Show($"Failed to fetch programs: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PopulateProgramFilter()
        cmbProgram.Items.Clear()
        cmbProgram.Items.Add("All Programs")

        Dim query As String = "SELECT program_name FROM programs ORDER BY program_name ASC"
        Try
            Using conn As MySqlConnection = Database.CreateOpenConnection()
                Using cmd As New MySqlCommand(query, conn)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            cmbProgram.Items.Add(reader("program_name").ToString())
                        End While
                    End Using
                End Using
            End Using
            cmbProgram.SelectedIndex = 0
        Catch ex As Exception
            ' Fail silently for the combo box
        End Try
    End Sub

    ' ======================================================================
    ' LIVE FILTERING
    ' ======================================================================
    Private Sub cmbProgram_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbProgram.SelectedIndexChanged
        If bsPrograms.DataSource Is Nothing Then Return

        Try
            If cmbProgram.SelectedIndex > 0 Then
                Dim selectedProgram = cmbProgram.Text.Replace("'", "''")
                bsPrograms.Filter = $"[Program Name] = '{selectedProgram}'"
            Else
                bsPrograms.Filter = ""
            End If
        Catch ex As Exception
            bsPrograms.Filter = ""
        End Try
    End Sub

    ' ======================================================================
    ' ROW DELETION ENGINE (WITH RELATIONAL SAFEGUARDS)
    ' ======================================================================
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If dgvPrograms.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a program row to delete.", "Select Row", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim progId As Integer = Convert.ToInt32(dgvPrograms.SelectedRows(0).Cells("Program ID").Value)
        Dim progName As String = dgvPrograms.SelectedRows(0).Cells("Program Name").Value.ToString()

        ' GATE 1: Check if this program has enrolled students
        Dim studentCount As Integer = 0
        Try
            Using conn As MySqlConnection = Database.CreateOpenConnection()
                Using checkCmd As New MySqlCommand("SELECT COUNT(*) FROM students WHERE program_id = @ProgId", conn)
                    checkCmd.Parameters.AddWithValue("@ProgId", progId)
                    studentCount = Convert.ToInt32(checkCmd.ExecuteScalar())
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"Verification failed: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

        If studentCount > 0 Then
            ' VIOLENT REJECTION OF HARD DELETE. Offer Soft-Delete instead.
            If MessageBox.Show($"CRITICAL WARNING: You cannot permanently delete '{progName}' because {studentCount} student(s) are currently enrolled in it." & vbCrLf & vbCrLf &
                               "Do you want to Archive (Deactivate) this program instead? This hides it from new enrollments but preserves historical data.",
                               "Integrity Violation Blocked", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
                Try
                    Using conn As MySqlConnection = Database.CreateOpenConnection()
                        Using updateCmd As New MySqlCommand("UPDATE programs SET status = 'Inactive' WHERE id = @ProgId", conn)
                            updateCmd.Parameters.AddWithValue("@ProgId", progId)
                            updateCmd.ExecuteNonQuery()
                        End Using
                    End Using
                    LoadLiveDatabaseToGrid()
                    MessageBox.Show("Program successfully deactivated.", "Archived", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    MessageBox.Show($"Deactivation failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
            Return
        End If

        ' If zero students are enrolled, allow the hard delete.
        If MessageBox.Show($"Are you absolutely sure you want to permanently delete '{progName}'? This action cannot be undone.", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) = DialogResult.Yes Then
            Try
                Using conn As MySqlConnection = Database.CreateOpenConnection()
                    Using deleteCmd As New MySqlCommand("DELETE FROM programs WHERE id = @ProgId", conn)
                        deleteCmd.Parameters.AddWithValue("@ProgId", progId)
                        deleteCmd.ExecuteNonQuery()
                    End Using
                End Using
                LoadLiveDatabaseToGrid()
                PopulateProgramFilter() ' Refresh dropdown
                MessageBox.Show("Program permanently deleted.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show($"Deletion failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    ' ======================================================================
    ' DYNAMIC ROW UPDATE EDITOR (STRICT VALIDATION GATES)
    ' ======================================================================
    Private Sub dgvPrograms_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPrograms.CellDoubleClick
        If e.RowIndex < 0 Then Return

        Dim selectedRow As DataGridViewRow = dgvPrograms.Rows(e.RowIndex)
        Dim currentId As Integer = Convert.ToInt32(selectedRow.Cells("Program ID").Value)
        Dim currentName As String = selectedRow.Cells("Program Name").Value.ToString()
        Dim currentFee As String = selectedRow.Cells("Base Fee").Value.ToString()
        Dim currentStatus As String = selectedRow.Cells("Status").Value.ToString()

        Using updateForm As New Form()
            updateForm.Text = $"Update Program: {currentName}"
            updateForm.Size = New Size(400, 360)
            updateForm.StartPosition = FormStartPosition.CenterParent
            updateForm.FormBorderStyle = FormBorderStyle.FixedDialog
            updateForm.MaximizeBox = False
            updateForm.MinimizeBox = False

            Dim lblName As New Label() With {.Text = "Program Name:", .Location = New Point(20, 20), .AutoSize = True}
            Dim txtName As New TextBox() With {.Text = currentName, .Location = New Point(20, 40), .Width = 340}

            Dim lblFee As New Label() With {.Text = "Base Fee (Numeric Only):", .Location = New Point(20, 80), .AutoSize = True}
            Dim txtFee As New TextBox() With {.Text = currentFee, .Location = New Point(20, 100), .Width = 340}

            Dim lblStatus As New Label() With {.Text = "Program Status:", .Location = New Point(20, 140), .AutoSize = True}
            Dim cmbStatus As New ComboBox() With {.Location = New Point(20, 160), .Width = 340, .DropDownStyle = ComboBoxStyle.DropDownList}
            cmbStatus.Items.Add("Active")
            cmbStatus.Items.Add("Inactive")
            cmbStatus.SelectedItem = currentStatus

            Dim btnSave As New Button() With {.Text = "Save Changes", .Location = New Point(120, 240), .Width = 140, .Height = 40, .BackColor = Color.Teal, .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}

            AddHandler btnSave.Click, Sub(s, ev)
                                          Dim progPattern = "^[A-Za-z\s\.\-\&]+$"
                                          Dim feePattern = "^\d+(\.\d{1,2})?$"

                                          Dim newName = txtName.Text.Trim()
                                          Dim newFee = txtFee.Text.Trim()
                                          Dim newStatus = cmbStatus.SelectedItem.ToString()

                                          ' Validation Gates
                                          If String.IsNullOrWhiteSpace(newName) OrElse Not Regex.IsMatch(newName, progPattern) Then
                                              MessageBox.Show("Invalid Program Name. Use only letters, spaces, periods, hyphens, or ampersands.", "Validation Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                              Return
                                          End If

                                          If String.IsNullOrWhiteSpace(newFee) OrElse Not Regex.IsMatch(newFee, feePattern) Then
                                              MessageBox.Show("Invalid Fee Format. Use valid numbers only (e.g., 1500 or 1500.50).", "Validation Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                              Return
                                          End If

                                          Dim baseFeeDecimal As Decimal = Convert.ToDecimal(newFee)

                                          ' Execute Update
                                          Try
                                              Using conn As MySqlConnection = Database.CreateOpenConnection()
                                                  Dim updateQuery As String = "UPDATE programs SET program_name = @Name, base_fee = @Fee, status = @Status WHERE id = @Id"
                                                  Using cmd As New MySqlCommand(updateQuery, conn)
                                                      cmd.Parameters.AddWithValue("@Name", newName)
                                                      cmd.Parameters.AddWithValue("@Fee", baseFeeDecimal)
                                                      cmd.Parameters.AddWithValue("@Status", newStatus)
                                                      cmd.Parameters.AddWithValue("@Id", currentId)
                                                      cmd.ExecuteNonQuery()
                                                  End Using
                                              End Using
                                              updateForm.DialogResult = DialogResult.OK
                                          Catch ex As Exception
                                              MessageBox.Show($"Update failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                          End Try
                                      End Sub

            updateForm.Controls.AddRange(New Control() {lblName, txtName, lblFee, txtFee, lblStatus, cmbStatus, btnSave})

            If updateForm.ShowDialog() = DialogResult.OK Then
                LoadLiveDatabaseToGrid()
                PopulateProgramFilter()
                MessageBox.Show("Program architecture securely updated.", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Using
    End Sub

    Private Sub dgvPrograms_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPrograms.CellContentClick

    End Sub
End Class