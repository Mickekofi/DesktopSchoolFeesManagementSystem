Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Windows.Forms
Imports System.Text.RegularExpressions

Public Class UC_Settings

    Private ep As New ErrorProvider()
    Private hintToolTip As New ToolTip()

    ' ======================================================================
    ' INITIALIZATION & UI SETUP
    ' ======================================================================
    Private Sub UC_Settings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Apply rounded corners using your established utility
        ShapeUtilities.RadiusButton(btnAddSettings, 1.5F)

        ep.BlinkStyle = ErrorBlinkStyle.NeverBlink

        ' Enforce ComboBox Constraints
        cmbSemester.DropDownStyle = ComboBoxStyle.DropDownList
        If cmbSemester.Items.Count = 0 Then
            cmbSemester.Items.Add("Semester 1")
            cmbSemester.Items.Add("Semester 2")
        End If

        ' The Academic Year box must allow typing for NEW years, but act as a dropdown for EXISTING ones
        cmbAcadamicYear.DropDownStyle = ComboBoxStyle.DropDown

        ' Attach ToolTips
        hintToolTip.SetToolTip(cmbSemester, "Select the active semester." & vbCrLf & "Rule A: Semester 1 enforces >= 50% payment." & vbCrLf & "Rule B: Semester 2 enforces 100% payment.")
        hintToolTip.SetToolTip(cmbAcadamicYear, "Select an existing year or type a new one strictly as YYYY/YYYY (e.g., 2026/2027).")
        hintToolTip.SetToolTip(btnAddSettings, "Save and activate this academic configuration. This will deactivate all previous terms.")

        ' Make the system state-aware on load
        LoadCurrentConfiguration()
    End Sub

    ' ======================================================================
    ' STATE AWARENESS ENGINE
    ' ======================================================================
    Private Sub LoadCurrentConfiguration()
        cmbAcadamicYear.Items.Clear()

        Try
            Using conn As MySqlConnection = Database.CreateOpenConnection()
                ' 1. Populate the Academic Year dropdown with all historical/existing years
                Dim historyQuery As String = "SELECT year_label FROM academic_years ORDER BY year_label DESC"
                Using cmdHist As New MySqlCommand(historyQuery, conn)
                    Using reader As MySqlDataReader = cmdHist.ExecuteReader()
                        While reader.Read()
                            cmbAcadamicYear.Items.Add(reader("year_label").ToString())
                        End While
                    End Using
                End Using

                ' 2. Find the currently active Year and Semester and pre-select them
                Dim activeQuery As String = "
                    SELECT y.year_label, s.semester_name 
                    FROM academic_years y 
                    JOIN semesters s ON s.academic_year_id = y.id 
                    WHERE y.is_active = 1 AND s.is_active = 1 LIMIT 1"

                Using cmdActive As New MySqlCommand(activeQuery, conn)
                    Using reader As MySqlDataReader = cmdActive.ExecuteReader()
                        If reader.Read() Then
                            cmbAcadamicYear.Text = reader("year_label").ToString()
                            cmbSemester.SelectedItem = reader("semester_name").ToString()
                        Else
                            ' System is completely blank/unconfigured
                            cmbAcadamicYear.Text = ""
                            cmbSemester.SelectedIndex = -1
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ' Silently log or ignore on load to prevent locking the UI, but user will see blank fields
            Console.WriteLine("Could not load prior state: " & ex.Message)
        End Try
    End Sub

    ' ======================================================================
    ' REAL-TIME VALIDATION EXTENSIONS
    ' ======================================================================
    Private Sub cmbAcadamicYear_TextChanged(sender As Object, e As EventArgs) Handles cmbAcadamicYear.TextChanged
        ' Enforce the exact Regex we locked in for the Excel Ingestion Engine
        Dim yearPattern = "^\d{4}/\d{4}$"
        Dim input = cmbAcadamicYear.Text.Trim()

        If input.Length > 0 AndAlso Not Regex.IsMatch(input, yearPattern) Then
            ep.SetError(cmbAcadamicYear, "Format must be strictly YYYY/YYYY (e.g., 2026/2027).")
        Else
            ep.SetError(cmbAcadamicYear, "")
        End If
    End Sub

    ' ======================================================================
    ' SUBMISSION & DATABASE LOGIC (STATE MANAGEMENT)
    ' ======================================================================
    Private Sub btnAddSettings_Click(sender As Object, e As EventArgs) Handles btnAddSettings.Click
        Dim yearPattern = "^\d{4}/\d{4}$"
        Dim acadYear = cmbAcadamicYear.Text.Trim()
        Dim selectedSem = If(cmbSemester.SelectedItem IsNot Nothing, cmbSemester.SelectedItem.ToString(), "")

        ' 1. Strict Input Validation Gates
        If String.IsNullOrWhiteSpace(acadYear) Then
            MessageBox.Show("Please enter or select the Academic Year.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbAcadamicYear.Focus()
            Return
        End If

        If Not Regex.IsMatch(acadYear, yearPattern) Then
            MessageBox.Show("Invalid Academic Year format. You must use YYYY/YYYY (e.g., 2026/2027).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cmbAcadamicYear.Focus()
            Return
        End If

        If String.IsNullOrWhiteSpace(selectedSem) Then
            MessageBox.Show("Please select a Semester.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbSemester.Focus()
            Return
        End If

        ' Determine enum mapping for your schema
        Dim semNumber As Integer = If(selectedSem = "Semester 1", 1, 2)

        ' 2. Execute Relational Database Transactions
        Try
            Using conn As MySqlConnection = Database.CreateOpenConnection()
                Using transaction = conn.BeginTransaction()
                    Try
                        ' --- STAGE A: Resolve or Insert Academic Year ---
                        Dim acadYearId As Integer = 0
                        Dim checkYearCmd As New MySqlCommand("SELECT id FROM academic_years WHERE year_label = @Year", conn, transaction)
                        checkYearCmd.Parameters.AddWithValue("@Year", acadYear)

                        Dim result = checkYearCmd.ExecuteScalar()
                        If result IsNot Nothing Then
                            acadYearId = Convert.ToInt32(result)
                        Else
                            Dim insertYearCmd As New MySqlCommand("INSERT INTO academic_years (year_label, is_active) VALUES (@Year, 0)", conn, transaction)
                            insertYearCmd.Parameters.AddWithValue("@Year", acadYear)
                            insertYearCmd.ExecuteNonQuery()
                            acadYearId = Convert.ToInt32(insertYearCmd.LastInsertedId)
                        End If

                        ' --- STAGE B: Resolve or Insert Semester ---
                        Dim semesterId As Integer = 0
                        Dim checkSemCmd As New MySqlCommand("SELECT id FROM semesters WHERE academic_year_id = @AcId AND semester_name = @SemName", conn, transaction)
                        checkSemCmd.Parameters.AddWithValue("@AcId", acadYearId)
                        checkSemCmd.Parameters.AddWithValue("@SemName", selectedSem)

                        Dim semResult = checkSemCmd.ExecuteScalar()
                        If semResult IsNot Nothing Then
                            semesterId = Convert.ToInt32(semResult)
                        Else
                            Dim insertSemCmd As New MySqlCommand("INSERT INTO semesters (academic_year_id, semester_name, semester_number, is_active) VALUES (@AcId, @SemName, @SemNum, 0)", conn, transaction)
                            insertSemCmd.Parameters.AddWithValue("@AcId", acadYearId)
                            insertSemCmd.Parameters.AddWithValue("@SemName", selectedSem)
                            insertSemCmd.Parameters.AddWithValue("@SemNum", semNumber)
                            insertSemCmd.ExecuteNonQuery()
                            semesterId = Convert.ToInt32(insertSemCmd.LastInsertedId)
                        End If

                        ' --- STAGE C: Enforce Singleton Active State ---
                        ' 1. Deactivate everything
                        Dim deactivateYearsCmd As New MySqlCommand("UPDATE academic_years SET is_active = 0", conn, transaction)
                        deactivateYearsCmd.ExecuteNonQuery()

                        Dim deactivateSemsCmd As New MySqlCommand("UPDATE semesters SET is_active = 0", conn, transaction)
                        deactivateSemsCmd.ExecuteNonQuery()

                        ' 2. Activate only the selected configuration
                        Dim activateYearCmd As New MySqlCommand("UPDATE academic_years SET is_active = 1 WHERE id = @AcId", conn, transaction)
                        activateYearCmd.Parameters.AddWithValue("@AcId", acadYearId)
                        activateYearCmd.ExecuteNonQuery()

                        Dim activateSemCmd As New MySqlCommand("UPDATE semesters SET is_active = 1 WHERE id = @SemId", conn, transaction)
                        activateSemCmd.Parameters.AddWithValue("@SemId", semesterId)
                        activateSemCmd.ExecuteNonQuery()

                        ' Commit all changes at once
                        transaction.Commit()

                        MessageBox.Show($"System successfully updated and activated!" & vbCrLf & vbCrLf &
                                        $"Active Year: {acadYear}" & vbCrLf &
                                        $"Active Term: {selectedSem}" & vbCrLf & vbCrLf &
                                        $"Financial billing rules for {selectedSem} are now in effect.",
                                        "Configuration Applied", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        ep.Clear()

                        ' Refresh the dropdown to ensure any newly typed year is now in the list
                        LoadCurrentConfiguration()

                    Catch ex As Exception
                        transaction.Rollback() ' Abort all changes if anything fails
                        Throw New Exception("Transaction failed. System state rolled back to prevent corruption.", ex)
                    End Try
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show($"Failed to apply settings." & vbCrLf & $"Details: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
    End Sub

    Private Sub PanelInputBundle_Paint(sender As Object, e As PaintEventArgs) Handles PanelInputBundle.Paint
    End Sub

    Private Sub cmbAccadamicYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAcadamicYear.SelectedIndexChanged
    End Sub
End Class