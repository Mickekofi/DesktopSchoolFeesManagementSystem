Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Windows.Forms
Imports System.Drawing

Public Class UC_CourseRegistration

    ' ======================================================================
    ' 1. THE COMMUNICATION BRIDGE (MANDATORY FOR DASHBOARD SYNC)
    ' ======================================================================
    Public Event RegistrationCompleted(sender As Object, e As EventArgs)

    ' --- Internal State Variables ---
    Private currentStudentId As Integer = 0
    Private activeYearId As Integer = 0
    Private activeSemesterId As Integer = 0
    Private isAlreadyRegistered As Boolean = False

    ' ======================================================================
    ' INITIALIZATION
    ' ======================================================================
    Private Sub UC_CourseRegistration_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 1. Load the Success GIF securely
        Try
            Dim asm = System.Reflection.Assembly.GetExecutingAssembly()
            Dim stream = asm.GetManifestResourceStream("School_Fees_Management_System.Sucessful_Gif.gif")
            If stream IsNot Nothing Then
                PictureBox1.Image = Image.FromStream(stream)
            End If
        Catch ex As Exception
            ' Silently handle missing embedded resources to prevent app crash
            Console.WriteLine("GIF Resource not found.")
        End Try

        ' 2. Fetch system state and configure the UI
        LoadSystemStateAndConfigureUI()
    End Sub

    ' ======================================================================
    ' STATE RESOLUTION & UI ROUTING
    ' ======================================================================
    Private Sub LoadSystemStateAndConfigureUI()
        Try
            Using conn As MySqlConnection = Database.CreateOpenConnection()
                ' A. Resolve Active Term & Student Profile concurrently
                Dim stateQuery As String = "
                    SELECT 
                        y.id AS year_id, 
                        s.id AS sem_id,
                        st.id AS student_id
                    FROM academic_years y
                    JOIN semesters s ON s.academic_year_id = y.id
                    JOIN students st ON st.index_number = @Index
                    WHERE y.is_active = 1 AND s.is_active = 1 LIMIT 1"

                Using cmd As New MySqlCommand(stateQuery, conn)
                    cmd.Parameters.AddWithValue("@Index", Session.CurrentUsername)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            activeYearId = Convert.ToInt32(reader("year_id"))
                            activeSemesterId = Convert.ToInt32(reader("sem_id"))
                            currentStudentId = Convert.ToInt32(reader("student_id"))
                        Else
                            MessageBox.Show("Failed to load active term or student profile.", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            btnSubmitRegistration.Enabled = False
                            Return
                        End If
                    End Using
                End Using

                ' B. Check if already registered
                Dim checkQuery As String = "SELECT id FROM course_registrations WHERE student_id = @StuId AND academic_year_id = @YearId AND semester_id = @SemId"
                Using cmdCheck As New MySqlCommand(checkQuery, conn)
                    cmdCheck.Parameters.AddWithValue("@StuId", currentStudentId)
                    cmdCheck.Parameters.AddWithValue("@YearId", activeYearId)
                    cmdCheck.Parameters.AddWithValue("@SemId", activeSemesterId)
                    Dim result = cmdCheck.ExecuteScalar()
                    isAlreadyRegistered = (result IsNot Nothing AndAlso Not IsDBNull(result))
                End Using

                ' C. Route UI based on state
                If isAlreadyRegistered Then
                    ShowSuccessState()
                Else
                    ShowPendingState()
                End If

            End Using
        Catch ex As Exception
            MessageBox.Show($"Failed to initialize registration module: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ======================================================================
    ' UI STATE HANDLERS
    ' ======================================================================
    Private Sub ShowSuccessState()
        PictureBox1.Visible = True
        lblSuccessFullRegistration.Visible = True
        btnSubmitRegistration.Visible = False
    End Sub

    Private Sub ShowPendingState()
        PictureBox1.Visible = False
        lblSuccessFullRegistration.Visible = False
        btnSubmitRegistration.Visible = True
        btnSubmitRegistration.Enabled = True
    End Sub

    ' ======================================================================
    ' THE DATABASE TRANSACTION
    ' ======================================================================
    Private Sub btnSubmitRegistration_Click(sender As Object, e As EventArgs) Handles btnSubmitRegistration.Click
        ' Safety catch
        If isAlreadyRegistered Then Return

        ' Lock UI to prevent spam-clicking and duplicate entries
        btnSubmitRegistration.Enabled = False
        btnSubmitRegistration.Text = "Processing..."
        Application.DoEvents()

        Try
            Using conn As MySqlConnection = Database.CreateOpenConnection()
                ' Insert the formal registration record
                Dim insertQuery As String = "
                    INSERT INTO course_registrations (student_id, academic_year_id, semester_id, registration_status, registration_date)
                    VALUES (@StuId, @YearId, @SemId, 'Approved', NOW())"

                Using cmd As New MySqlCommand(insertQuery, conn)
                    cmd.Parameters.AddWithValue("@StuId", currentStudentId)
                    cmd.Parameters.AddWithValue("@YearId", activeYearId)
                    cmd.Parameters.AddWithValue("@SemId", activeSemesterId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            ' Update internal state
            isAlreadyRegistered = True

            ' 1. Switch the UI to show the GIF
            ShowSuccessState()

            ' 2. RAISE THE EVENT! This tells the Dashboard to turn the label Green instantly.
            RaiseEvent RegistrationCompleted(Me, EventArgs.Empty)

            MessageBox.Show("Registration successful! Your status has been updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show($"Failed to submit registration: {ex.Message}", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            btnSubmitRegistration.Enabled = True
            btnSubmitRegistration.Text = "Register Courses"
        End Try
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub Panel4_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub Panel4_Paint_1(sender As Object, e As PaintEventArgs) Handles Panel4.Paint

    End Sub
End Class