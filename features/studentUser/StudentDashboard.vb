Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Windows.Forms
Imports System.Drawing

Public Class StudentDashboard

    ' --- System State Variables ---
    Private activeYearId As Integer = 0
    Private activeSemesterId As Integer = 0
    Private activeSemesterName As String = ""

    Private currentStudentId As Integer = 0
    Private studentProgramId As Integer = 0
    Private totalPaidAmount As Decimal = 0D
    Private isRegisteredForTerm As Boolean = False

    ' ======================================================================
    ' CORE UI & INITIALIZATION
    ' ======================================================================
    Private Sub StudentDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        Me.Text = "Student Dashboard"

        ' Ensure the Register form is completely closed to free memory, not just hidden
        If Application.OpenForms.OfType(Of Register)().Any() Then
            Application.OpenForms("Register").Hide()
        End If

        ShapeUtilities.RadiusButton(btnPayFees, 1.5F)
        ShapeUtilities.RadiusButton(btnCourseRegistration, 1.5F)
        ShapeUtilities.RadiusButton(btnHistory, 1.5F)

        Dim navButtons As New List(Of Button) From {
            btnHistory,
            btnPayFees,
            btnCourseRegistration
        }
        NavButtonStyles.InitializeNavButtons(Me, navButtons, btnPayFees)

        ' Boot up the State Engine
        EstablishSystemState()
    End Sub

    Private Sub LoadControl(control As UserControl)
        PanelWithUC.Controls.Clear()
        control.Dock = DockStyle.Fill
        PanelWithUC.Controls.Add(control)
    End Sub

    ' ======================================================================
    ' STATE MANAGEMENT ENGINE (THE SINGLE SOURCE OF TRUTH)
    ' ======================================================================
    Private Sub EstablishSystemState()
        Try
            Using conn As MySqlConnection = Database.CreateOpenConnection()

                ' 1. Get the Active Academic Configuration
                Dim configQuery As String = "
                    SELECT y.id as year_id, s.id as sem_id, s.semester_name 
                    FROM academic_years y 
                    JOIN semesters s ON s.academic_year_id = y.id 
                    WHERE y.is_active = 1 AND s.is_active = 1 LIMIT 1"

                Using configCmd As New MySqlCommand(configQuery, conn)
                    Using reader As MySqlDataReader = configCmd.ExecuteReader()
                        If reader.Read() Then
                            activeYearId = Convert.ToInt32(reader("year_id"))
                            activeSemesterId = Convert.ToInt32(reader("sem_id"))
                            activeSemesterName = reader("semester_name").ToString()
                        Else
                            MessageBox.Show("CRITICAL ERROR: No active academic semester found. Contact Administration.", "System Locked", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            btnCourseRegistration.Enabled = False
                            Return
                        End If
                    End Using
                End Using

                ' 2. Get Student Identity
                Dim studentQuery As String = "
                    SELECT s.id, s.program_id, p.program_name 
                    FROM students s 
                    LEFT JOIN programs p ON s.program_id = p.id 
                    WHERE s.index_number = @Index"

                Using stuCmd As New MySqlCommand(studentQuery, conn)
                    stuCmd.Parameters.AddWithValue("@Index", Session.CurrentUsername)
                    Using reader As MySqlDataReader = stuCmd.ExecuteReader()
                        If reader.Read() Then
                            currentStudentId = Convert.ToInt32(reader("id"))
                            studentProgramId = If(IsDBNull(reader("program_id")), 0, Convert.ToInt32(reader("program_id")))
                            lblProgram.Text = If(IsDBNull(reader("program_name")), "Unassigned Program", reader("program_name").ToString())
                        End If
                    End Using
                End Using

                ' 3. Calculate Total Valid Payments (STRICTLY FOR THE ACTIVE YEAR)
                Dim paymentQuery As String = "
                    SELECT SUM(p.amount_paid) 
                    FROM payments p
                    JOIN fee_structures fs ON p.fee_structure_id = fs.id
                    WHERE p.student_id = @StuId AND fs.academic_year_id = @YearId AND p.payment_status = 'Completed'"

                Using payCmd As New MySqlCommand(paymentQuery, conn)
                    payCmd.Parameters.AddWithValue("@StuId", currentStudentId)
                    payCmd.Parameters.AddWithValue("@YearId", activeYearId)
                    Dim result = payCmd.ExecuteScalar()
                    totalPaidAmount = If(IsDBNull(result), 0D, Convert.ToDecimal(result))
                End Using

                ' 4. Check Current Registration Status
                Dim regQuery As String = "SELECT registration_status FROM course_registrations WHERE student_id = @StuId AND semester_id = @SemId AND academic_year_id = @YearId"
                Using regCmd As New MySqlCommand(regQuery, conn)
                    regCmd.Parameters.AddWithValue("@StuId", currentStudentId)
                    regCmd.Parameters.AddWithValue("@SemId", activeSemesterId)
                    regCmd.Parameters.AddWithValue("@YearId", activeYearId)
                    Dim regResult = regCmd.ExecuteScalar()

                    If regResult IsNot Nothing Then
                        isRegisteredForTerm = True
                        lblRegistrationStatus.Text = $"Status: {regResult.ToString()}"
                        lblRegistrationStatus.ForeColor = Color.Green
                    Else
                        isRegisteredForTerm = False
                        lblRegistrationStatus.Text = "Status: Not Registered"
                        lblRegistrationStatus.ForeColor = Color.Red
                    End If
                End Using

            End Using
        Catch ex As Exception
            MessageBox.Show($"State Engine Failure: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ======================================================================
    ' NAVIGATION & DYNAMIC FINANCIAL COMPLIANCE GATES
    ' ======================================================================
    Private Sub btnPayFees_Click(sender As Object, e As EventArgs) Handles btnPayFees.Click
        Dim uc_PayFees As New UC_PayFees()
        LoadControl(uc_PayFees)
    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Dim uc_History As New UC_History()
        LoadControl(uc_History)
    End Sub

    Private Sub btnCourseRegistration_Click(sender As Object, e As EventArgs) Handles btnCourseRegistration.Click
        ' 1. Refresh state instantly to avoid bypassed checks
        EstablishSystemState()

        ' 2. Prevent duplicate registrations
        If isRegisteredForTerm Then
            MessageBox.Show("You have already registered your courses for the current active semester.", "Registration Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Dim uc_CourseRegView As New UC_CourseRegistration()
            ' Reattach the listener just in case they are making an edit
            AddHandler uc_CourseRegView.RegistrationCompleted, AddressOf OnRegistrationCompleted
            LoadControl(uc_CourseRegView)
            Return
        End If

        ' 3. The Financial Compliance Engine (Fetching dynamically from DB)
        Dim annualTargetFee As Decimal = 0D
        Dim dbConfiguredPercentage As Decimal = 0D

        Try
            Using conn As MySqlConnection = Database.CreateOpenConnection()
                Dim targetQuery As String = "SELECT fee_amount, min_payment_percentage FROM fee_structures WHERE program_id = @ProgId AND academic_year_id = @YearId"
                Using cmdTarget As New MySqlCommand(targetQuery, conn)
                    cmdTarget.Parameters.AddWithValue("@ProgId", studentProgramId)
                    cmdTarget.Parameters.AddWithValue("@YearId", activeYearId)
                    Using reader As MySqlDataReader = cmdTarget.ExecuteReader()
                        If reader.Read() Then
                            annualTargetFee = Convert.ToDecimal(reader("fee_amount"))
                            dbConfiguredPercentage = Convert.ToDecimal(reader("min_payment_percentage"))
                        Else
                            MessageBox.Show("No financial target found for your program this year. Administration must configure fees before you can register.", "System Locked", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Return
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"Compliance Engine Error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

        ' ======================================================================
        ' 4. AIRTIGHT SEMESTER EVALUATION & DYNAMIC THRESHOLD CALCULATION
        ' ======================================================================
        Dim requiredThreshold As Decimal = 0D
        Dim minPercentageRequired As Decimal = 0D
        Dim ruleExplanation As String = ""

        ' Enforce strict term-based rules
        If activeSemesterName.Trim().Equals("Semester 2", StringComparison.OrdinalIgnoreCase) Then
            ' SEMESTER 2 HARD LOCK: REQUIRES 100% FULL PAYMENT
            minPercentageRequired = 100D
            requiredThreshold = annualTargetFee
            ruleExplanation = $"Semester 2 Policy requires 100% FULL PAYMENT (GHS {requiredThreshold:N2}) of your annual fee target before course registration is unlocked."
        Else
            ' SEMESTER 1 (OR DEFAULT): USE CONFIGURABLE PERCENTAGE FROM DB (e.g., 50%)
            minPercentageRequired = dbConfiguredPercentage
            requiredThreshold = annualTargetFee * (minPercentageRequired / 100D)
            ruleExplanation = $"{activeSemesterName} Policy requires a minimum payment of {minPercentageRequired:N0}% (GHS {requiredThreshold:N2}) of your annual fee target (GHS {annualTargetFee:N2})."
        End If

        ' ======================================================================
        ' 5. EXECUTE THE CHECK AGAINST THE LEDGER
        ' ======================================================================
        If totalPaidAmount >= requiredThreshold Then
            ' SUCCESS: Open registration and listen for completion
            MessageBox.Show($"Financial clearance granted! You have paid GHS {totalPaidAmount:N2}, meeting the {minPercentageRequired:N0}% requirement for {activeSemesterName}.", "Clearance Approved", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Dim uc_CourseRegistration As New UC_CourseRegistration()
            ' THE BRIDGE: Tells Dashboard to update UI when registration succeeds
            AddHandler uc_CourseRegistration.RegistrationCompleted, AddressOf OnRegistrationCompleted
            LoadControl(uc_CourseRegistration)
        Else
            ' FAILURE: Expose deficit and route to payment
            Dim deficit As Decimal = requiredThreshold - totalPaidAmount
            Dim warningMsg As String =
                $"FINANCIAL CLEARANCE DENIED" & vbCrLf & vbCrLf &
                $"{ruleExplanation}" & vbCrLf & vbCrLf &
                $"Total Paid So Far: GHS {totalPaidAmount:N2}" & vbCrLf &
                $"Required Amount:   GHS {requiredThreshold:N2}" & vbCrLf &
                $"Outstanding Deficit: GHS {deficit:N2}" & vbCrLf & vbCrLf &
                $"You must pay the remaining balance of GHS {deficit:N2} before the system will unlock course registration."

            MessageBox.Show(warningMsg, "Registration Locked", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            btnPayFees.PerformClick()
        End If
    End Sub

    ' ======================================================================
    ' CROSS-MODULE COMMUNICATION CALLBACKS
    ' ======================================================================
    ' This method fires automatically when UC_CourseRegistration raises its event
    Private Sub OnRegistrationCompleted(sender As Object, e As EventArgs)
        ' Force the Dashboard to re-read the DB and update lblRegistrationStatus
        EstablishSystemState()
    End Sub

    Private Sub LOGOUTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LOGOUTToolStripMenuItem.Click
        Session.CurrentUsername = ""
        Session.CurrentFullName = ""
        Session.CurrentRole = ""
        Session.IsStudent = False
        Session.CurrentProgramName = ""

        Dim loginForm As New login()
        loginForm.Show()
        Me.Close()
    End Sub

    Private Sub PanelWithUC_Paint(sender As Object, e As PaintEventArgs) Handles PanelWithUC.Paint
    End Sub

    Private Sub ProfileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProfileToolStripMenuItem.Click
        Dim uc_Profile As New UC_Profile()
        LoadControl(uc_Profile)
    End Sub

End Class