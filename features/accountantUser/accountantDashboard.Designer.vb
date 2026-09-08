<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class accountantDashboard
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        PanelWithNavButtons = New Panel()
        btnRegister = New Button()
        btnManageFees = New Button()
        Label4 = New Label()
        btnSettings = New Button()
        btnPrograms = New Button()
        MenuStrip1 = New MenuStrip()
        MoreOptionsToolStripMenuItem = New ToolStripMenuItem()
        AboutUsToolStripMenuItem = New ToolStripMenuItem()
        ViewProgramToolStripMenuItem = New ToolStripMenuItem()
        StatisticsToolStripMenuItem = New ToolStripMenuItem()
        LOGOUTToolStripMenuItem1 = New ToolStripMenuItem()
        FlowLayoutPanelBackground = New FlowLayoutPanel()
        PanelWithUC = New Panel()
        btnReports = New Button()
        PanelWithNavButtons.SuspendLayout()
        MenuStrip1.SuspendLayout()
        FlowLayoutPanelBackground.SuspendLayout()
        SuspendLayout()
        ' 
        ' PanelWithNavButtons
        ' 
        PanelWithNavButtons.BackColor = Color.DarkRed
        PanelWithNavButtons.Controls.Add(btnReports)
        PanelWithNavButtons.Controls.Add(btnRegister)
        PanelWithNavButtons.Controls.Add(btnManageFees)
        PanelWithNavButtons.Controls.Add(Label4)
        PanelWithNavButtons.Controls.Add(btnSettings)
        PanelWithNavButtons.Controls.Add(btnPrograms)
        PanelWithNavButtons.Controls.Add(MenuStrip1)
        PanelWithNavButtons.Location = New Point(4, 4)
        PanelWithNavButtons.Margin = New Padding(4)
        PanelWithNavButtons.Name = "PanelWithNavButtons"
        PanelWithNavButtons.Size = New Size(2221, 99)
        PanelWithNavButtons.TabIndex = 29
        ' 
        ' btnRegister
        ' 
        btnRegister.BackColor = Color.DarkRed
        btnRegister.FlatAppearance.BorderSize = 0
        btnRegister.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Bold)
        btnRegister.ForeColor = Color.Transparent
        btnRegister.Location = New Point(1129, 18)
        btnRegister.Margin = New Padding(4, 5, 4, 5)
        btnRegister.Name = "btnRegister"
        btnRegister.Size = New Size(265, 60)
        btnRegister.TabIndex = 13
        btnRegister.Text = "Register Students"
        btnRegister.UseVisualStyleBackColor = False
        ' 
        ' btnManageFees
        ' 
        btnManageFees.BackColor = Color.DarkRed
        btnManageFees.FlatAppearance.BorderSize = 0
        btnManageFees.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Bold)
        btnManageFees.ForeColor = Color.Transparent
        btnManageFees.Location = New Point(861, 18)
        btnManageFees.Margin = New Padding(4, 5, 4, 5)
        btnManageFees.Name = "btnManageFees"
        btnManageFees.Size = New Size(260, 60)
        btnManageFees.TabIndex = 21
        btnManageFees.Text = "Manage Fees"
        btnManageFees.UseVisualStyleBackColor = False
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.BackColor = Color.Transparent
        Label4.Font = New Font("Garamond", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label4.ForeColor = Color.White
        Label4.Location = New Point(19, 47)
        Label4.Margin = New Padding(4, 0, 4, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(265, 31)
        Label4.TabIndex = 19
        Label4.Text = "Administrator Portal "
        Label4.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' btnSettings
        ' 
        btnSettings.BackColor = Color.DarkRed
        btnSettings.FlatAppearance.BorderSize = 0
        btnSettings.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Bold)
        btnSettings.ForeColor = Color.Transparent
        btnSettings.Location = New Point(609, 18)
        btnSettings.Margin = New Padding(4, 5, 4, 5)
        btnSettings.Name = "btnSettings"
        btnSettings.Size = New Size(244, 60)
        btnSettings.TabIndex = 15
        btnSettings.Text = "Settings"
        btnSettings.UseVisualStyleBackColor = False
        ' 
        ' btnPrograms
        ' 
        btnPrograms.BackColor = Color.DarkRed
        btnPrograms.FlatAppearance.BorderSize = 0
        btnPrograms.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Bold)
        btnPrograms.ForeColor = Color.Transparent
        btnPrograms.Location = New Point(349, 18)
        btnPrograms.Margin = New Padding(4, 5, 4, 5)
        btnPrograms.Name = "btnPrograms"
        btnPrograms.Size = New Size(241, 60)
        btnPrograms.TabIndex = 14
        btnPrograms.Text = "Create Programs"
        btnPrograms.UseVisualStyleBackColor = False
        ' 
        ' MenuStrip1
        ' 
        MenuStrip1.BackColor = Color.Transparent
        MenuStrip1.Dock = DockStyle.Left
        MenuStrip1.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        MenuStrip1.ImageScalingSize = New Size(20, 20)
        MenuStrip1.Items.AddRange(New ToolStripItem() {MoreOptionsToolStripMenuItem})
        MenuStrip1.Location = New Point(0, 0)
        MenuStrip1.Name = "MenuStrip1"
        MenuStrip1.Padding = New Padding(8, 2, 0, 2)
        MenuStrip1.Size = New Size(153, 99)
        MenuStrip1.TabIndex = 20
        MenuStrip1.Text = "MenuStrip1"
        ' 
        ' MoreOptionsToolStripMenuItem
        ' 
        MoreOptionsToolStripMenuItem.BackColor = Color.DarkSlateGray
        MoreOptionsToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {AboutUsToolStripMenuItem, StatisticsToolStripMenuItem, LOGOUTToolStripMenuItem1})
        MoreOptionsToolStripMenuItem.ForeColor = Color.White
        MoreOptionsToolStripMenuItem.Name = "MoreOptionsToolStripMenuItem"
        MoreOptionsToolStripMenuItem.Size = New Size(136, 29)
        MoreOptionsToolStripMenuItem.Text = "More Options"
        ' 
        ' AboutUsToolStripMenuItem
        ' 
        AboutUsToolStripMenuItem.BackColor = Color.DarkRed
        AboutUsToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {ViewProgramToolStripMenuItem})
        AboutUsToolStripMenuItem.ForeColor = Color.White
        AboutUsToolStripMenuItem.Name = "AboutUsToolStripMenuItem"
        AboutUsToolStripMenuItem.Size = New Size(195, 34)
        AboutUsToolStripMenuItem.Text = "Programs"
        ' 
        ' ViewProgramToolStripMenuItem
        ' 
        ViewProgramToolStripMenuItem.BackColor = Color.Maroon
        ViewProgramToolStripMenuItem.ForeColor = Color.White
        ViewProgramToolStripMenuItem.Name = "ViewProgramToolStripMenuItem"
        ViewProgramToolStripMenuItem.Size = New Size(241, 34)
        ViewProgramToolStripMenuItem.Text = "View Programs"
        ' 
        ' StatisticsToolStripMenuItem
        ' 
        StatisticsToolStripMenuItem.BackColor = Color.DarkRed
        StatisticsToolStripMenuItem.ForeColor = Color.White
        StatisticsToolStripMenuItem.Name = "StatisticsToolStripMenuItem"
        StatisticsToolStripMenuItem.Size = New Size(195, 34)
        StatisticsToolStripMenuItem.Text = "Statistics"
        ' 
        ' LOGOUTToolStripMenuItem1
        ' 
        LOGOUTToolStripMenuItem1.BackColor = Color.DarkRed
        LOGOUTToolStripMenuItem1.ForeColor = Color.White
        LOGOUTToolStripMenuItem1.Name = "LOGOUTToolStripMenuItem1"
        LOGOUTToolStripMenuItem1.Size = New Size(195, 34)
        LOGOUTToolStripMenuItem1.Text = "LOGOUT"
        ' 
        ' FlowLayoutPanelBackground
        ' 
        FlowLayoutPanelBackground.Controls.Add(PanelWithNavButtons)
        FlowLayoutPanelBackground.Controls.Add(PanelWithUC)
        FlowLayoutPanelBackground.Dock = DockStyle.Fill
        FlowLayoutPanelBackground.Location = New Point(0, 0)
        FlowLayoutPanelBackground.Margin = New Padding(4)
        FlowLayoutPanelBackground.Name = "FlowLayoutPanelBackground"
        FlowLayoutPanelBackground.Size = New Size(1811, 1050)
        FlowLayoutPanelBackground.TabIndex = 0
        ' 
        ' PanelWithUC
        ' 
        PanelWithUC.Dock = DockStyle.Bottom
        PanelWithUC.Location = New Point(4, 111)
        PanelWithUC.Margin = New Padding(4)
        PanelWithUC.Name = "PanelWithUC"
        PanelWithUC.Size = New Size(2266, 1055)
        PanelWithUC.TabIndex = 30
        ' 
        ' btnReports
        ' 
        btnReports.BackColor = Color.DarkRed
        btnReports.FlatAppearance.BorderSize = 0
        btnReports.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Bold)
        btnReports.ForeColor = Color.Transparent
        btnReports.Location = New Point(1402, 18)
        btnReports.Margin = New Padding(4, 5, 4, 5)
        btnReports.Name = "btnReports"
        btnReports.Size = New Size(265, 60)
        btnReports.TabIndex = 22
        btnReports.Text = "Reports"
        btnReports.UseVisualStyleBackColor = False
        ' 
        ' accountantDashboard
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1811, 1050)
        Controls.Add(FlowLayoutPanelBackground)
        MainMenuStrip = MenuStrip1
        Margin = New Padding(4)
        Name = "accountantDashboard"
        Text = "accountantDashboard"
        PanelWithNavButtons.ResumeLayout(False)
        PanelWithNavButtons.PerformLayout()
        MenuStrip1.ResumeLayout(False)
        MenuStrip1.PerformLayout()
        FlowLayoutPanelBackground.ResumeLayout(False)
        ResumeLayout(False)
    End Sub
    Friend WithEvents PanelWithNavButtons As Panel
    Friend WithEvents btnSettings As Button
    Friend WithEvents btnPrograms As Button
    Friend WithEvents btnRegister As Button
    Friend WithEvents FlowLayoutPanelBackground As FlowLayoutPanel
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents PanelWithUC As Panel
    Friend WithEvents Label4 As Label
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents MoreOptionsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AboutUsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents StatisticsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ViewProgramToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents btnManageFees As Button
    Friend WithEvents LOGOUTToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents btnReports As Button
End Class
