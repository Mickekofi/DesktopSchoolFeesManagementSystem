Imports System.Text.RegularExpressions
Imports System.Security.Cryptography
Imports System.Text
Imports MySql.Data.MySqlClient

Public Class login

    Private ep As New ErrorProvider()

    Private Sub FadeIn(sender As Object, e As EventArgs)
        If Me.Opacity < 1 Then
            Me.Opacity += 0.05
        Else
            fadeTimer2.Stop()
            RemoveHandler fadeTimer2.Tick, AddressOf FadeIn
        End If
    End Sub

    'The Main Login Form Load Event
    Private Sub login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        Me.Text = "Login"

        RadiusButton(btnLogin, 1.5F)

        ' Style the navigation buttons using the NavButtonStyles module
        Dim navButtons As New List(Of Button) From {
            btnHide,
            btnLogin,
            btnNoUse
        }

        NavButtonStyles.InitializeNavButtons(Me, navButtons, btnNoUse)

        Me.Opacity = 0
        fadeTimer2.Start()
        AddHandler fadeTimer2.Tick, AddressOf FadeIn
        txtUsername.Focus()

        'LOADING THE GIF IMAGE FROM THE EMBEDDED RESOURCE
        Try
            Dim asm = System.Reflection.Assembly.GetExecutingAssembly()
            Dim stream = asm.GetManifestResourceStream("School_Fees_Management_System.Giffy.gif")
            If stream IsNot Nothing Then
                PictureBox1.Image = Image.FromStream(stream)
            End If
        Catch ex As Exception
            ' Silently handle missing resource so app doesn't crash on startup
        End Try
    End Sub

    ' Validate Username: Allow only letters and numbers (alphanumeric)
    Private Sub txtUsername_TextChanged(sender As Object, e As EventArgs) Handles txtUsername.TextChanged
        Dim alphanumericPattern = "^[a-zA-Z0-9]+$"
        Dim input = txtUsername.Text.Trim()

        If input.Length > 0 AndAlso Not Regex.IsMatch(input, alphanumericPattern) Then
            ep.SetError(txtUsername, "Username must contain only Index Digits(Student ID) Or Alphabets.")
        Else
            ep.SetError(txtUsername, "")
        End If
    End Sub

    'txtUsername key down to txtPassword
    Private Sub txtUsername_KeyDown(sender As Object, e As KeyEventArgs) Handles txtUsername.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True ' Prevent the ding sound
            txtPassword.Focus() ' Move focus to password field
        End If
    End Sub

    'txtPassword key down to btnLogin
    Private Sub txtPassword_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPassword.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True ' Prevent the ding sound
            btnLogin.PerformClick() ' Trigger the login button click event
        End If
    End Sub

    ' Handle login authentication and navigation (CONNECTED TO MYSQL)
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim alphanumericPattern = "^[a-zA-Z0-9]+$"
        Dim input = txtUsername.Text.Trim()
        Dim pass = txtPassword.Text.Trim()

        ' 1. Validate Username Regex
        If Not Regex.IsMatch(input, alphanumericPattern) Then
            MessageBox.Show("Invalid username format!" & vbCrLf & "Username must contain Only Index Digits(Student ID) Or Alphabets.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtUsername.Focus()
            Return
        End If

        ' 2. Basic validation to ensure password isn't empty
        If String.IsNullOrWhiteSpace(pass) Then
            MessageBox.Show("Password field cannot be empty.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPassword.Focus()
            Return
        End If

        ' Disable button to prevent double-clicking during DB check
        btnLogin.Enabled = False
        Me.Cursor = Cursors.WaitCursor

        Try
            ' Hash the input password to match DB
            Dim hashedInput As String = ComputeSHA256(pass)

            ' 3. CHECK STAFF/ADMIN TABLE FIRST
            Dim staffName As String = ""
            Dim staffRole As String = ""

            If AuthenticateStaff(input, hashedInput, staffName, staffRole) Then
                ' -> WRITE TO GLOBAL SESSION <-
                Session.CurrentUsername = input
                Session.CurrentFullName = staffName
                Session.CurrentRole = staffRole
                Session.IsStudent = False
                Session.CurrentProgramName = "" ' Staff have no academic program

                Dim accDashboard As New accountantDashboard()
                accDashboard.Show()
                Me.Hide()
                Return
            End If

            ' 4. CHECK STUDENTS TABLE SECOND
            Dim studentName As String = ""
            Dim studentProgram As String = ""
            Dim isFirstLogin As Boolean = False ' The routing flag

            If AuthenticateStudent(input, hashedInput, studentName, studentProgram, isFirstLogin) Then
                ' -> WRITE TO GLOBAL SESSION <-
                Session.CurrentUsername = input
                Session.CurrentFullName = studentName
                Session.CurrentRole = "Student"
                Session.IsStudent = True
                Session.CurrentProgramName = studentProgram

                ' STRICT ROUTING GATE
                If isFirstLogin Then
                    ' Trap them in the registration flow
                    Dim registerForm As New Register()
                    registerForm.Show()
                Else
                    ' Standard operational flow
                    Dim studentDashboard As New StudentDashboard()
                    studentDashboard.Show()
                End If

                Me.Hide()
                Return
            End If

            ' 5. FAILED LOGIN
            MessageBox.Show("Invalid username or password.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtUsername.Focus()

        Catch ex As Exception
            MessageBox.Show("Database connection error: " & ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            btnLogin.Enabled = True
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    ' ======================================================================
    ' DATABASE AUTHENTICATION FUNCTIONS
    ' ======================================================================

    Private Function AuthenticateStaff(username As String, hash As String, ByRef outName As String, ByRef outRole As String) As Boolean
        Dim query As String = "SELECT full_name, role FROM staff_users WHERE username = @User AND password_hash = @Hash AND is_active = TRUE"

        Using conn As MySqlConnection = Database.CreateOpenConnection()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@User", username)
                cmd.Parameters.AddWithValue("@Hash", hash)

                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        outName = reader("full_name").ToString()
                        outRole = reader("role").ToString()
                        Return True
                    End If
                End Using
            End Using
        End Using
        Return False
    End Function

    ' Added the outIsFirstLogin ByRef parameter
    Private Function AuthenticateStudent(indexNumber As String, hash As String, ByRef outName As String, ByRef outProgram As String, ByRef outIsFirstLogin As Boolean) As Boolean
        Dim query As String = "
            SELECT s.full_name, p.program_name, s.is_first_login 
            FROM students s
            LEFT JOIN programs p ON s.program_id = p.id
            WHERE s.index_number = @Index 
            AND s.password_hash = @Hash 
            AND s.account_status = 'Active'"

        Using conn As MySqlConnection = Database.CreateOpenConnection()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Index", indexNumber)
                cmd.Parameters.AddWithValue("@Hash", hash)

                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        outName = reader("full_name").ToString()
                        outProgram = If(IsDBNull(reader("program_name")), "Unassigned Program", reader("program_name").ToString())

                        ' Extract the boolean directly from the database
                        outIsFirstLogin = Convert.ToBoolean(reader("is_first_login"))

                        Return True
                    End If
                End Using
            End Using
        End Using
        Return False
    End Function

    Private Function ComputeSHA256(input As String) As String
        Using sha256Engine As SHA256 = SHA256.Create()
            Dim bytes As Byte() = sha256Engine.ComputeHash(Encoding.UTF8.GetBytes(input))
            Dim builder As New StringBuilder()
            For i As Integer = 0 To bytes.Length - 1
                builder.Append(bytes(i).ToString("x2"))
            Next
            Return builder.ToString()
        End Using
    End Function

    ' ======================================================================
    ' FORM EVENTS
    ' ======================================================================

    Private Sub login_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Application.Exit()
    End Sub

    Private Sub btnHide_Click(sender As Object, e As EventArgs) Handles btnHide.Click
        If btnHide.Text = "Show" Then
            txtPassword.UseSystemPasswordChar = False
            btnHide.Text = "Hide"
        Else
            txtPassword.UseSystemPasswordChar = True
            txtPassword.PasswordChar = ControlChars.NullChar
            btnHide.Text = "Show"
        End If
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class