Imports System.Data
Imports System.Text.RegularExpressions
Imports MySql.Data.MySqlClient
Imports System.Windows.Forms

Public Class UC_Programs

    ' Initialize ErrorProvider and ToolTip
    Private ep As New ErrorProvider()
    Private hintToolTip As New ToolTip()

    ' ======================================================================
    ' INITIALIZATION & UI SETUP
    ' ======================================================================
    Private Sub UC_Programs_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Apply rounded corners using your established utility
        ShapeUtilities.RadiusButton(btnAddProgram, 1.5F)

        ep.BlinkStyle = ErrorBlinkStyle.NeverBlink

        ' Attach ToolTips for admin instruction
        hintToolTip.SetToolTip(txtProgramName, "Enter full academic program name. Example: Bsc. Mathematics Education")
        hintToolTip.SetToolTip(txtSetFees, "Enter the base fee amount for the program (e.g., 1500 or 1500.50)")
        hintToolTip.SetToolTip(btnAddProgram, "Verify and save the new program to the active database")
    End Sub

    ' ======================================================================
    ' REAL-TIME VALIDATION EXTENSIONS
    ' ======================================================================

    ' Validate Program Name: Allow letters, spaces, periods, hyphens, and ampersands
    Private Sub txtProgramName_TextChanged(sender As Object, e As EventArgs) Handles txtProgramName.TextChanged
        ' Fixed Regex to actually allow your "Bsc." example
        Dim safePattern = "^[A-Za-z\s\.\-\&]+$"
        Dim input = txtProgramName.Text.Trim()

        If input.Length > 0 AndAlso Not Regex.IsMatch(input, safePattern) Then
            ep.SetError(txtProgramName, "Invalid character. Use letters, spaces, and basic punctuation (., -, &).")
        Else
            ep.SetError(txtProgramName, "")
        End If
    End Sub

    ' Validate Set Fees: Allow integers and decimals up to 2 decimal places
    Private Sub txtSetFees_TextChanged(sender As Object, e As EventArgs) Handles txtSetFees.TextChanged
        ' Fixed Regex to allow financial formats like 1500.50
        Dim feePattern = "^\d+(\.\d{1,2})?$"
        Dim input = txtSetFees.Text.Trim()

        If input.Length > 0 AndAlso Not Regex.IsMatch(input, feePattern) Then
            ep.SetError(txtSetFees, "Enter a valid financial amount (e.g., 2000 or 2000.50).")
        Else
            ep.SetError(txtSetFees, "")
        End If
    End Sub

    ' ======================================================================
    ' SUBMISSION & DATABASE LOGIC
    ' ======================================================================
    Private Sub btnAddProgram_Click(sender As Object, e As EventArgs) Handles btnAddProgram.Click
        Dim progPattern = "^[A-Za-z\s\.\-\&]+$"
        Dim feePattern = "^\d+(\.\d{1,2})?$"

        Dim progName = txtProgramName.Text.Trim()
        Dim progFee = txtSetFees.Text.Trim()

        ' 1. Check for blank inputs
        If String.IsNullOrWhiteSpace(progName) Then
            MessageBox.Show("Please enter a Program Name.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtProgramName.Focus()
            Return
        End If

        If String.IsNullOrWhiteSpace(progFee) Then
            MessageBox.Show("Please enter the Set Fees.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtSetFees.Focus()
            Return
        End If

        ' 2. Enforce strict Regex validation before hitting the database
        If Not Regex.IsMatch(progName, progPattern) Then
            MessageBox.Show("Invalid Program Name." & vbCrLf & "Please use only letters, spaces, periods, hyphens, or ampersands.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtProgramName.Focus()
            Return
        End If

        If Not Regex.IsMatch(progFee, feePattern) Then
            MessageBox.Show("Invalid Fees Format." & vbCrLf & "Please enter a valid monetary amount.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtSetFees.Focus()
            Return
        End If

        Dim baseFeeDecimal As Decimal = Convert.ToDecimal(progFee)

        ' 3. Execute Database Transactions
        Try
            Using conn As MySqlConnection = Database.CreateOpenConnection()

                ' GATE 1: Duplicate Entry Check
                Dim checkQuery As String = "SELECT COUNT(*) FROM programs WHERE program_name = @Name"
                Using checkCmd As New MySqlCommand(checkQuery, conn)
                    checkCmd.Parameters.AddWithValue("@Name", progName)
                    Dim count As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())

                    If count > 0 Then
                        MessageBox.Show($"The program '{progName}' already exists in the system. Duplicates are not allowed.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        txtProgramName.Focus()
                        Return
                    End If
                End Using

                ' GATE 2: Secure Database Insertion
                Dim insertQuery As String = "INSERT INTO programs (program_name, base_fee, status) VALUES (@Name, @Fee, 'Active')"
                Using insertCmd As New MySqlCommand(insertQuery, conn)
                    insertCmd.Parameters.AddWithValue("@Name", progName)
                    insertCmd.Parameters.AddWithValue("@Fee", baseFeeDecimal)
                    insertCmd.ExecuteNonQuery()
                End Using

            End Using

            ' 4. Success State and UI Reset
            MessageBox.Show($"Program '{progName}' has been added successfully with a base fee of {baseFeeDecimal:C}!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Clear the fields ready for the next entry
            txtProgramName.Clear()
            txtSetFees.Clear()
            ep.Clear() ' Clear any lingering error icons
            txtProgramName.Focus()

        Catch ex As Exception
            MessageBox.Show($"Failed to save program to database." & vbCrLf & $"Details: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub PanelInputBundle_Paint(sender As Object, e As PaintEventArgs) Handles PanelInputBundle.Paint
        ' Retained as requested
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
        ' Retained as requested
    End Sub

End Class