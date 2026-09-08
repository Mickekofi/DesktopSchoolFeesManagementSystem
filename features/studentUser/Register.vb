Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Drawing
Imports MySql.Data.MySqlClient
Imports System.Windows.Forms

Public Class Register
    ' Initialize an ErrorProvider for form validations
    Private errProvider As New ErrorProvider()
    Private hintToolTip As New ToolTip()

    Private Sub Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        Me.Text = "Student Registration"

        RadiusButton(btnSubmit, 1.5F)
        RadiusButton(btnUploadPhoto, 1.5F)
        RadiusButton(btnReload, 1.5F)

        ' Set MaxDate for Date of Birth to 10 years ago to allow only Age 10 and above
        dtpDOB.MaxDate = DateTime.Now.AddYears(-10)

        ' Clear password label text on startup
        lblStrength.Text = ""

        ' 1. LOAD STUDENT IDENTITY FROM SESSION
        If Not String.IsNullOrEmpty(Session.CurrentFullName) Then
            lblfull_name.Text = Session.CurrentFullName
        Else
            lblfull_name.Text = "Unknown Student"
        End If

        ' 2. APPLY TOOLTIPS FOR UX
        hintToolTip.SetToolTip(lblfull_name, "Your official name as registered in the system.")
        hintToolTip.SetToolTip(txtEmail, "Enter a valid active email address for school communications.")
        hintToolTip.SetToolTip(txtPhone, "Enter your 10-digit mobile number starting with 02 or 05.")
        hintToolTip.SetToolTip(dtpDOB, "Select your date of birth. You must be at least 10 years old.")
        hintToolTip.SetToolTip(cmbGender, "Select your gender identity.")
        hintToolTip.SetToolTip(txtPassword, "Create a strong password (minimum 8 characters, with letters and numbers).")
        hintToolTip.SetToolTip(btnUploadPhoto, "Upload a clear passport photo with a primarily Red or White background.")
        hintToolTip.SetToolTip(btnSubmit, "Save your profile and activate your account.")
    End Sub

    ' ======================================================================
    ' UI RESET
    ' ======================================================================
    Private Sub btnReload_Click(sender As Object, e As EventArgs) Handles btnReload.Click
        txtEmail.Clear()
        txtPhone.Clear()
        txtPassword.Clear()
        cmbGender.SelectedIndex = -1
        dtpDOB.Value = dtpDOB.MaxDate

        If picPassport.Image IsNot Nothing Then
            picPassport.Image.Dispose()
            picPassport.Image = Nothing
        End If
        lblPhotoPath.Text = ""

        lblStrength.Text = ""
        lblStrength.BackColor = Color.Transparent
        errProvider.Clear()
        txtEmail.Focus()
    End Sub

    ' ======================================================================
    ' PASSPORT IMAGE HANDLING
    ' ======================================================================
    Private Sub btnUploadPhoto_Click(sender As Object, e As EventArgs) Handles btnUploadPhoto.Click
        Dim ofd As New OpenFileDialog
        ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"
        ofd.Title = "Select Passport Picture"

        If ofd.ShowDialog = DialogResult.OK Then
            Try
                Dim uploadedImage = Image.FromFile(ofd.FileName)

                If IsPassportBackgroundRedOrWhite(uploadedImage) Then
                    picPassport.Image = uploadedImage
                    picPassport.SizeMode = PictureBoxSizeMode.Zoom
                    ' Temporarily store the original file path to extract extension later
                    lblPhotoPath.Text = ofd.FileName
                    errProvider.SetError(btnUploadPhoto, "")
                Else
                    MessageBox.Show("The passport background must be primarily Red or White. Please choose a compliant photo.", "Invalid Background", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    uploadedImage.Dispose()
                End If
            Catch ex As Exception
                MessageBox.Show("Error loading image: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Function IsPassportBackgroundRedOrWhite(img As System.Drawing.Image) As Boolean
        Using bmp As New System.Drawing.Bitmap(img)
            Dim redCount As Integer = 0
            Dim whiteCount As Integer = 0
            Dim totalCount As Long = 0

            Dim samplePoints = {
                New Point(0, 0), New Point(bmp.Width - 1, 0),
                New Point(0, bmp.Height - 1), New Point(bmp.Width - 1, bmp.Height - 1),
                New Point(bmp.Width \ 2, 0), New Point(bmp.Width \ 2, bmp.Height - 1),
                New Point(0, bmp.Height \ 2), New Point(bmp.Width - 1, bmp.Height \ 2)
            }

            For Each pt In samplePoints
                If pt.X >= 0 AndAlso pt.X < bmp.Width AndAlso pt.Y >= 0 AndAlso pt.Y < bmp.Height Then
                    Dim c As Color = bmp.GetPixel(pt.X, pt.Y)
                    totalCount += 1
                    If IsColorRed(c) Then redCount += 1
                    If IsColorWhite(c) Then whiteCount += 1
                End If
            Next

            Dim threshold As Long = CInt(totalCount * 0.75)
            Return (redCount + whiteCount) >= threshold
        End Using
    End Function

    Private Function IsColorRed(c As Color) As Boolean
        Return c.R > 180 AndAlso c.G < 100 AndAlso c.B < 100
    End Function

    Private Function IsColorWhite(c As Color) As Boolean
        Return c.R > 200 AndAlso c.G > 200 AndAlso c.B > 200
    End Function

    ' ======================================================================
    ' LIVE VALIDATIONS & UX ROUTING (OPTIMIZED UX)
    ' ======================================================================
    Private Sub txtEmail_KeyDown(sender As Object, e As KeyEventArgs) Handles txtEmail.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            txtPhone.Focus()
        End If
    End Sub

    Private Sub txtPhone_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPhone.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            dtpDOB.Focus()
        End If
    End Sub

    Private Sub dtpDOB_KeyDown(sender As Object, e As KeyEventArgs) Handles dtpDOB.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            txtPassword.Focus()
        End If
    End Sub

    Private Sub txtPassword_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPassword.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            btnSubmit.PerformClick()
        End If
    End Sub

    ' UX FIX: Clear errors instantly when typing, but DO NOT validate yet.
    Private Sub txtPhone_TextChanged(sender As Object, e As EventArgs) Handles txtPhone.TextChanged
        errProvider.SetError(txtPhone, "")
    End Sub

    Private Sub txtEmail_TextChanged(sender As Object, e As EventArgs) Handles txtEmail.TextChanged
        errProvider.SetError(txtEmail, "")
    End Sub

    ' UX FIX: Validate only when the user finishes and leaves the textbox.
    Private Sub txtPhone_Leave(sender As Object, e As EventArgs) Handles txtPhone.Leave
        Dim pattern = "^(02|05)\d{8}$"
        Dim input = txtPhone.Text.Trim
        If input.Length > 0 AndAlso Not Regex.IsMatch(input, pattern) Then
            errProvider.SetError(txtPhone, "Phone must be 10 digits and start with 02 or 05.")
        End If
    End Sub

    Private Sub txtEmail_Leave(sender As Object, e As EventArgs) Handles txtEmail.Leave
        Dim pattern = "^[\w\.-]+@[\w\.-]+\.\w+$"
        Dim input = txtEmail.Text.Trim
        If input.Length > 0 AndAlso Not Regex.IsMatch(input, pattern) Then
            errProvider.SetError(txtEmail, "Enter a valid email address.")
        End If
    End Sub

    Private Sub dtpDOB_ValueChanged(sender As Object, e As EventArgs) Handles dtpDOB.ValueChanged
        Dim minAge = 10
        Dim age As Integer = DateDiff(DateInterval.Year, dtpDOB.Value, Date.Now)
        If age < minAge Then
            errProvider.SetError(dtpDOB, "Age must be at least 10 years.")
        Else
            errProvider.SetError(dtpDOB, "")
        End If
    End Sub

    Private Sub txtPassword_TextChanged(sender As Object, e As EventArgs) Handles txtPassword.TextChanged
        Dim pwd = txtPassword.Text
        If pwd.Length = 0 Then
            lblStrength.Text = ""
            lblStrength.BackColor = Color.Transparent
            Return
        End If

        Dim score = 0
        If pwd.Length >= 8 Then score += 1
        If Regex.IsMatch(pwd, "[a-z]") AndAlso Regex.IsMatch(pwd, "[A-Z]") Then score += 1
        If Regex.IsMatch(pwd, "[0-9]") Then score += 1
        If Regex.IsMatch(pwd, "[^a-zA-Z0-9]") Then score += 1

        Select Case score
            Case 0, 1
                lblStrength.Text = "Weak"
                lblStrength.ForeColor = Color.White
                lblStrength.BackColor = Color.Red
            Case 2, 3
                lblStrength.Text = "Medium"
                lblStrength.ForeColor = Color.Black
                lblStrength.BackColor = Color.Yellow
            Case 4
                lblStrength.Text = "Strong"
                lblStrength.ForeColor = Color.White
                lblStrength.BackColor = Color.Green
        End Select
    End Sub

    ' ======================================================================
    ' STRICT SUBMISSION GATES & DATABASE INJECTION
    ' ======================================================================
    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        ' 1. Final Hard Validation Gate
        If String.IsNullOrWhiteSpace(txtEmail.Text) OrElse Not Regex.IsMatch(txtEmail.Text.Trim, "^[\w\.-]+@[\w\.-]+\.\w+$") Then
            MessageBox.Show("Please provide a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtEmail.Focus()
            Return
        End If

        If String.IsNullOrWhiteSpace(txtPhone.Text) OrElse Not Regex.IsMatch(txtPhone.Text.Trim, "^(02|05)\d{8}$") Then
            MessageBox.Show("Please provide a valid 10-digit phone number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPhone.Focus()
            Return
        End If

        If cmbGender.SelectedIndex < 0 Then
            MessageBox.Show("Please select your Gender.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbGender.Focus()
            Return
        End If

        If String.IsNullOrWhiteSpace(txtPassword.Text) Then
            MessageBox.Show("Please create a new password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPassword.Focus()
            Return
        End If

        If picPassport.Image Is Nothing OrElse String.IsNullOrWhiteSpace(lblPhotoPath.Text) Then
            MessageBox.Show("You must upload a compliant passport photograph to proceed.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Disable button to prevent double-execution
        btnSubmit.Enabled = False
        Cursor = Cursors.WaitCursor

        Try
            ' 2. PHYSICAL FILE MANAGEMENT (CREATE DIRECTORY AND SAVE)
            Dim targetDirectory = Path.Combine(Application.StartupPath, "Passports")
            If Not Directory.Exists(targetDirectory) Then
                Directory.CreateDirectory(targetDirectory)
            End If

            Dim originalExt = Path.GetExtension(lblPhotoPath.Text)
            Dim newFileName = $"{Session.CurrentUsername}_passport{originalExt}"
            Dim finalSavePath = Path.Combine(targetDirectory, newFileName)

            ' Copy the physical file to the secure application directory
            File.Copy(lblPhotoPath.Text, finalSavePath, True)

            ' 3. SECURE DATABASE UPDATE
            ' Note: Assuming you altered your database to include a 'dob' column.
            Dim updateQuery = "
                UPDATE students 
                SET email = @Email, 
                    phone = @Phone, 
                    gender = @Gender, 
                    password_hash = SHA2(@Pass, 256), 
                    passport_photo_path = @PhotoPath, 
                    is_first_login = FALSE 
                WHERE index_number = @Idx"

            Using conn = Database.CreateOpenConnection
                Using cmd As New MySqlCommand(updateQuery, conn)
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim)
                    cmd.Parameters.AddWithValue("@Phone", txtPhone.Text.Trim)
                    cmd.Parameters.AddWithValue("@Gender", cmbGender.SelectedItem.ToString)
                    cmd.Parameters.AddWithValue("@Pass", txtPassword.Text.Trim)
                    cmd.Parameters.AddWithValue("@PhotoPath", finalSavePath) ' Storing the exact location
                    cmd.Parameters.AddWithValue("@Idx", Session.CurrentUsername) ' Locked to the logged-in user

                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show("Registration complete. Your account is now active.", "Profile Secured", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' 4. REDIRECT TO DASHBOARD
            Dim dashboard As New StudentDashboard
            dashboard.Show()
            Close() ' Close rather than Hide to free up memory from this one-time screen

        Catch ex As Exception
            MessageBox.Show($"Failed to activate account." & vbCrLf & $"System Error: {ex.Message}", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            btnSubmit.Enabled = True
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub
End Class