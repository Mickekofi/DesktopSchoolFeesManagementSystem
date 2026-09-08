Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Windows.Forms
Imports System.Drawing
Imports System.IO

Public Class UC_Profile

    Private Sub UC_Profile_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadStudentProfile()
    End Sub

    Private Sub LoadStudentProfile()
        ' Default reset to protect against stale memory
        lblFullName.Text = "Loading..."
        lblIndexNumber.Text = Session.CurrentUsername
        lblStatus.Text = "Checking Status..."
        lblStatus.ForeColor = Color.Gray
        lblAccadamicYearFees.Text = "GHS 0.00"
        lblFeesOwned.Text = "GHS 0.00"
        pbxPassport.Image = Nothing

        Try
            Using conn As MySqlConnection = Database.CreateOpenConnection()

                ' ======================================================================
                ' PHASE 1: RESOLVE THE ACTIVE ACADEMIC TIME-SPACE
                ' ======================================================================
                Dim activeYearId As Integer = 0
                Dim activeSemesterId As Integer = 0
                Dim activeSemesterName As String = ""

                Dim termQuery As String = "
                    SELECT y.id AS year_id, s.id AS sem_id, s.semester_name 
                    FROM academic_years y 
                    JOIN semesters s ON s.academic_year_id = y.id 
                    WHERE y.is_active = 1 AND s.is_active = 1 LIMIT 1"

                Using cmdTerm As New MySqlCommand(termQuery, conn)
                    Using reader As MySqlDataReader = cmdTerm.ExecuteReader()
                        If reader.Read() Then
                            activeYearId = Convert.ToInt32(reader("year_id"))
                            activeSemesterId = Convert.ToInt32(reader("sem_id"))
                            activeSemesterName = reader("semester_name").ToString()
                        Else
                            lblStatus.Text = "SYSTEM OFFLINE: No Active Term"
                            lblStatus.ForeColor = Color.DarkRed
                            Return ' Cannot compute profile without an active term
                        End If
                    End Using
                End Using

                ' ======================================================================
                ' PHASE 2: EXTRACT CORE IDENTITY & IMAGE RENDERING
                ' ======================================================================
                Dim currentStudentId As Integer = 0
                Dim studentProgramId As Integer = 0
                Dim photoPath As String = ""

                Dim identityQuery As String = "SELECT id, full_name, index_number, passport_photo_path, program_id FROM students WHERE index_number = @Index"
                Using cmdId As New MySqlCommand(identityQuery, conn)
                    cmdId.Parameters.AddWithValue("@Index", Session.CurrentUsername)
                    Using reader As MySqlDataReader = cmdId.ExecuteReader()
                        If reader.Read() Then
                            currentStudentId = Convert.ToInt32(reader("id"))
                            lblFullName.Text = reader("full_name").ToString()
                            lblIndexNumber.Text = reader("index_number").ToString()
                            studentProgramId = If(IsDBNull(reader("program_id")), 0, Convert.ToInt32(reader("program_id")))
                            photoPath = If(IsDBNull(reader("passport_photo_path")), "", reader("passport_photo_path").ToString())
                        Else
                            MessageBox.Show("Critical Error: Identity resolution failed.", "Authentication Guard", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Return
                        End If
                    End Using
                End Using

                ' Defensive Image Loading Strategy
                Try
                    If Not String.IsNullOrWhiteSpace(photoPath) AndAlso File.Exists(photoPath) Then
                        Using fs As New FileStream(photoPath, FileMode.Open, FileAccess.Read)
                            pbxPassport.Image = Image.FromStream(fs)
                            pbxPassport.SizeMode = PictureBoxSizeMode.Zoom
                        End Using
                    Else
                        pbxPassport.BackColor = Color.LightGray ' Visual indicator of missing photo
                    End If
                Catch ex As Exception
                    ' Silently fail image load rather than crashing the profile module
                    pbxPassport.BackColor = Color.IndianRed
                End Try

                ' ======================================================================
                ' PHASE 3: AGGREGATE FINANCIAL DEBT (PURE ANNUAL TARGETING)
                ' ======================================================================
                Dim feeTarget As Decimal = 0D
                Dim totalPaid As Decimal = 0D

                If studentProgramId > 0 Then
                    ' REFACTORED: Stripped the MAX() hack. Fetching the exact configured target.
                    Dim targetQuery As String = "SELECT fee_amount FROM fee_structures WHERE program_id = @ProgId AND academic_year_id = @YearId LIMIT 1"
                    Using cmdTarget As New MySqlCommand(targetQuery, conn)
                        cmdTarget.Parameters.AddWithValue("@ProgId", studentProgramId)
                        cmdTarget.Parameters.AddWithValue("@YearId", activeYearId)
                        Dim tResult = cmdTarget.ExecuteScalar()
                        feeTarget = If(IsDBNull(tResult) OrElse tResult Is Nothing, 0D, Convert.ToDecimal(tResult))
                    End Using

                    ' Sum all successful payments strictly bound to this Academic Year
                    Dim sumQuery As String = "
                        SELECT SUM(p.amount_paid) 
                        FROM payments p
                        JOIN fee_structures fs ON p.fee_structure_id = fs.id
                        WHERE p.student_id = @StuId AND fs.academic_year_id = @YearId AND p.payment_status = 'Completed'"
                    Using cmdSum As New MySqlCommand(sumQuery, conn)
                        cmdSum.Parameters.AddWithValue("@StuId", currentStudentId)
                        cmdSum.Parameters.AddWithValue("@YearId", activeYearId)
                        Dim pResult = cmdSum.ExecuteScalar()
                        totalPaid = If(IsDBNull(pResult) OrElse pResult Is Nothing, 0D, Convert.ToDecimal(pResult))
                    End Using
                End If

                Dim amountOwed As Decimal = Math.Max(0, feeTarget - totalPaid)
                lblAccadamicYearFees.Text = $"GHS {feeTarget:N2}"
                lblFeesOwned.Text = $"GHS {amountOwed:N2}"

                ' Visual feedback for debt
                lblFeesOwned.ForeColor = If(amountOwed > 0, Color.DarkRed, Color.DarkGreen)

                ' ======================================================================
                ' PHASE 4: DYNAMIC REGISTRATION STATUS (SINGLE SOURCE OF TRUTH)
                ' ======================================================================
                Dim regQuery As String = "
                    SELECT registration_status 
                    FROM course_registrations 
                    WHERE student_id = @StuId AND academic_year_id = @YearId AND semester_id = @SemId"

                Using cmdReg As New MySqlCommand(regQuery, conn)
                    cmdReg.Parameters.AddWithValue("@StuId", currentStudentId)
                    cmdReg.Parameters.AddWithValue("@YearId", activeYearId)
                    cmdReg.Parameters.AddWithValue("@SemId", activeSemesterId)

                    Dim regResult = cmdReg.ExecuteScalar()

                    If regResult IsNot Nothing AndAlso Not IsDBNull(regResult) Then
                        ' The UI blindly accepts whatever the database dictates
                        Dim statusStr As String = regResult.ToString()
                        lblStatus.Text = $"Registration Status: {statusStr}"

                        ' The UI only decides the color based on keywords
                        Select Case statusStr.ToLower()
                            Case "approved", "registered"
                                lblStatus.ForeColor = Color.DarkGreen
                            Case "pending"
                                lblStatus.ForeColor = Color.DarkOrange
                            Case Else
                                lblStatus.ForeColor = Color.DarkRed
                        End Select
                    Else
                        lblStatus.Text = $"Not Registered for {activeSemesterName}"
                        lblStatus.ForeColor = Color.DarkRed
                    End If
                End Using

            End Using
        Catch ex As Exception
            MessageBox.Show($"Profile generation failed: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs)
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
    End Sub

    Private Sub Panel13_Paint(sender As Object, e As PaintEventArgs) Handles Panel13.Paint
    End Sub

End Class