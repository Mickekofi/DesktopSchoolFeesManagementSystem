<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class StudentDashboard
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        PanelWithNavButtons = New Panel()
        Panel1 = New Panel()
        lblRegistrationStatus = New Label()
        lblProgram = New Label()
        btnHistory = New Button()
        btnCourseRegistration = New Button()
        btnPayFees = New Button()
        MenuStrip1 = New MenuStrip()
        ProfileToolStripMenuItem = New ToolStripMenuItem()
        LOGOUTToolStripMenuItem = New ToolStripMenuItem()
        PanelWithUC = New Panel()
        FlowLayoutPanelBackground = New FlowLayoutPanel()
        PanelWithNavButtons.SuspendLayout()
        Panel1.SuspendLayout()
        MenuStrip1.SuspendLayout()
        FlowLayoutPanelBackground.SuspendLayout()
        SuspendLayout()
        ' 
        ' PanelWithNavButtons
        ' 
        PanelWithNavButtons.BackColor = Color.DarkRed
        PanelWithNavButtons.Controls.Add(Panel1)
        PanelWithNavButtons.Controls.Add(lblProgram)
        PanelWithNavButtons.Controls.Add(btnHistory)
        PanelWithNavButtons.Controls.Add(btnCourseRegistration)
        PanelWithNavButtons.Controls.Add(btnPayFees)
        PanelWithNavButtons.Controls.Add(MenuStrip1)
        PanelWithNavButtons.Location = New Point(4, 4)
        PanelWithNavButtons.Margin = New Padding(4)
        PanelWithNavButtons.Name = "PanelWithNavButtons"
        PanelWithNavButtons.Size = New Size(2232, 128)
        PanelWithNavButtons.TabIndex = 29
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.WhiteSmoke
        Panel1.Controls.Add(lblRegistrationStatus)
        Panel1.Location = New Point(-2, 63)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(375, 34)
        Panel1.TabIndex = 23
        ' 
        ' lblRegistrationStatus
        ' 
        lblRegistrationStatus.AutoSize = True
        lblRegistrationStatus.BackColor = Color.Transparent
        lblRegistrationStatus.Font = New Font("Garamond", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblRegistrationStatus.ForeColor = Color.White
        lblRegistrationStatus.Location = New Point(29, 0)
        lblRegistrationStatus.Margin = New Padding(4, 0, 4, 0)
        lblRegistrationStatus.Name = "lblRegistrationStatus"
        lblRegistrationStatus.Size = New Size(21, 31)
        lblRegistrationStatus.TabIndex = 22
        lblRegistrationStatus.Text = "!"
        lblRegistrationStatus.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' lblProgram
        ' 
        lblProgram.AutoSize = True
        lblProgram.BackColor = Color.Transparent
        lblProgram.Font = New Font("Garamond", 10.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblProgram.ForeColor = Color.White
        lblProgram.Location = New Point(-1, 12)
        lblProgram.Margin = New Padding(4, 0, 4, 0)
        lblProgram.Name = "lblProgram"
        lblProgram.Size = New Size(484, 25)
        lblProgram.TabIndex = 17
        lblProgram.Text = "Bsc. Information and Communication Technology"
        lblProgram.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' btnHistory
        ' 
        btnHistory.BackColor = Color.DarkRed
        btnHistory.FlatAppearance.BorderSize = 0
        btnHistory.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Bold)
        btnHistory.ForeColor = Color.Transparent
        btnHistory.Location = New Point(1364, 14)
        btnHistory.Margin = New Padding(4, 5, 4, 5)
        btnHistory.Name = "btnHistory"
        btnHistory.Size = New Size(342, 60)
        btnHistory.TabIndex = 15
        btnHistory.Text = "History"
        btnHistory.UseVisualStyleBackColor = False
        ' 
        ' btnCourseRegistration
        ' 
        btnCourseRegistration.BackColor = Color.DarkRed
        btnCourseRegistration.FlatAppearance.BorderSize = 0
        btnCourseRegistration.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Bold)
        btnCourseRegistration.ForeColor = Color.Transparent
        btnCourseRegistration.Location = New Point(994, 14)
        btnCourseRegistration.Margin = New Padding(4, 5, 4, 5)
        btnCourseRegistration.Name = "btnCourseRegistration"
        btnCourseRegistration.Size = New Size(342, 60)
        btnCourseRegistration.TabIndex = 14
        btnCourseRegistration.Text = "Course Registration"
        btnCourseRegistration.UseVisualStyleBackColor = False
        ' 
        ' btnPayFees
        ' 
        btnPayFees.BackColor = Color.DarkRed
        btnPayFees.FlatAppearance.BorderSize = 0
        btnPayFees.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Bold)
        btnPayFees.ForeColor = Color.Transparent
        btnPayFees.Location = New Point(631, 14)
        btnPayFees.Margin = New Padding(4, 5, 4, 5)
        btnPayFees.Name = "btnPayFees"
        btnPayFees.Size = New Size(342, 60)
        btnPayFees.TabIndex = 13
        btnPayFees.Text = "Pay Fees"
        btnPayFees.UseVisualStyleBackColor = False
        ' 
        ' MenuStrip1
        ' 
        MenuStrip1.Dock = DockStyle.Bottom
        MenuStrip1.ImageScalingSize = New Size(20, 20)
        MenuStrip1.Items.AddRange(New ToolStripItem() {ProfileToolStripMenuItem, LOGOUTToolStripMenuItem})
        MenuStrip1.Location = New Point(0, 95)
        MenuStrip1.Name = "MenuStrip1"
        MenuStrip1.Padding = New Padding(8, 2, 0, 2)
        MenuStrip1.Size = New Size(2232, 33)
        MenuStrip1.TabIndex = 21
        MenuStrip1.Text = "MenuStrip1"
        ' 
        ' ProfileToolStripMenuItem
        ' 
        ProfileToolStripMenuItem.Name = "ProfileToolStripMenuItem"
        ProfileToolStripMenuItem.Size = New Size(78, 29)
        ProfileToolStripMenuItem.Text = "Profile"
        ' 
        ' LOGOUTToolStripMenuItem
        ' 
        LOGOUTToolStripMenuItem.Name = "LOGOUTToolStripMenuItem"
        LOGOUTToolStripMenuItem.Size = New Size(96, 29)
        LOGOUTToolStripMenuItem.Text = "LOGOUT"
        ' 
        ' PanelWithUC
        ' 
        PanelWithUC.Dock = DockStyle.Bottom
        PanelWithUC.Location = New Point(4, 140)
        PanelWithUC.Margin = New Padding(4)
        PanelWithUC.Name = "PanelWithUC"
        PanelWithUC.Size = New Size(2266, 1278)
        PanelWithUC.TabIndex = 30
        ' 
        ' FlowLayoutPanelBackground
        ' 
        FlowLayoutPanelBackground.Controls.Add(PanelWithNavButtons)
        FlowLayoutPanelBackground.Controls.Add(PanelWithUC)
        FlowLayoutPanelBackground.Dock = DockStyle.Fill
        FlowLayoutPanelBackground.Location = New Point(0, 0)
        FlowLayoutPanelBackground.Margin = New Padding(4)
        FlowLayoutPanelBackground.Name = "FlowLayoutPanelBackground"
        FlowLayoutPanelBackground.Size = New Size(1924, 1036)
        FlowLayoutPanelBackground.TabIndex = 1
        ' 
        ' StudentDashboard
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1924, 1036)
        Controls.Add(FlowLayoutPanelBackground)
        Margin = New Padding(4)
        Name = "StudentDashboard"
        Text = "StudenDashboard"
        PanelWithNavButtons.ResumeLayout(False)
        PanelWithNavButtons.PerformLayout()
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        MenuStrip1.ResumeLayout(False)
        MenuStrip1.PerformLayout()
        FlowLayoutPanelBackground.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents PanelWithNavButtons As Panel
    Friend WithEvents btnHistory As Button
    Friend WithEvents btnCourseRegistration As Button
    Friend WithEvents btnPayFees As Button
    Friend WithEvents PanelWithUC As Panel
    Friend WithEvents FlowLayoutPanelBackground As FlowLayoutPanel
    Friend WithEvents lblProgram As Label
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ProfileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LOGOUTToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents lblRegistrationStatus As Label
    Friend WithEvents Panel1 As Panel
End Class
