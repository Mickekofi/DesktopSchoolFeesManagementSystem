Public Class accountantDashboard

    ' ======================================================================
    ' USER CONTROL ROUTING ENGINE
    ' ======================================================================
    Private Sub LoadControl(control As UserControl)
        PanelWithUC.Controls.Clear()
        control.Dock = DockStyle.Fill
        PanelWithUC.Controls.Add(control)
    End Sub

    ' ======================================================================
    ' FORM INITIALIZATION & UI STYLING
    ' ======================================================================
    Private Sub accountantDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized

        ' Dynamically display the active user's identity from our secure Session
        Me.Text = $"UCAM Dashboard - Logged in as: {Session.CurrentFullName} ({Session.CurrentRole})"

        ' Using your specific RadiusButton function as requested
        RadiusButton(btnRegister, 1.5F)
        RadiusButton(btnPrograms, 1.5F)
        RadiusButton(btnSettings, 1.5F)
        RadiusButton(btnManageFees, 1.5F)
        RadiusButton(btnReports, 1.5F)

        ' Apply unified navigation button styles
        Dim navButtons As New List(Of Button) From {
            btnRegister,
            btnPrograms,
            btnSettings,
            btnManageFees,
            btnReports
        }
        NavButtonStyles.InitializeNavButtons(Me, navButtons, btnRegister)
    End Sub

    ' Fire the default screen only after the dashboard UI has fully rendered
    Private Sub accountantDashboard_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Dim uc_RegisterStudents As New UC_EntrollStudents()
        LoadControl(uc_RegisterStudents)
    End Sub

    ' ======================================================================
    ' SIDEBAR NAVIGATION EVENTS
    ' ======================================================================
    Private Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
        Dim uc_RegisterStudents As New UC_EntrollStudents
        LoadControl(uc_RegisterStudents)
    End Sub

    Private Sub btnPrograms_Click(sender As Object, e As EventArgs) Handles btnPrograms.Click
        Dim uc_Programs As New UC_Programs()
        LoadControl(uc_Programs)
    End Sub

    Private Sub btnSettings_Click(sender As Object, e As EventArgs) Handles btnSettings.Click
        Dim uc_Settings As New UC_Settings()
        LoadControl(uc_Settings)
    End Sub

    ' ======================================================================
    ' MENUSTRIP EVENTS & SECURITY
    ' ======================================================================
    Private Sub ViewProgramToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewProgramToolStripMenuItem.Click
        Dim uc_VPrograms As New UC_ViewPrograms()
        LoadControl(uc_VPrograms)
    End Sub


    Private Sub AboutUsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutUsToolStripMenuItem.Click
        ' Leave empty for now until you build the About modal
    End Sub

    Private Sub btnManageFees_Click(sender As Object, e As EventArgs) Handles btnManageFees.Click
        Dim uc_ManageFees As New UC_ManageFees()
        LoadControl(uc_ManageFees)
    End Sub

    Private Sub PanelWithUC_Paint(sender As Object, e As PaintEventArgs) Handles PanelWithUC.Paint

    End Sub

    Private Sub LOGOUTToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles LOGOUTToolStripMenuItem1.Click
        ' 1. Wipe the universal tracking variables to prevent session hijacking
        Session.ClearSession()

        ' 2. Instantiate a fresh, clean login window
        Dim newLogin As New login()
        newLogin.Show()

        ' 3. Completely destroy this dashboard instance to free up memory
        Me.Close()
    End Sub

    Private Sub StatisticsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StatisticsToolStripMenuItem.Click
        Dim uc_Statistics As New UC_Statistics()
        LoadControl(uc_Statistics)
    End Sub

    Private Sub btnReports_Click(sender As Object, e As EventArgs) Handles btnReports.Click
        Dim uc As New UC_Reports()
        ' Assuming PanelWithUC is the name of your main dashboard panel
        PanelWithUC.Controls.Clear()
        uc.Dock = DockStyle.Fill
        PanelWithUC.Controls.Add(uc)
    End Sub
End Class