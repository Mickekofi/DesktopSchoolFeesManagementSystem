<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UC_Settings
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Panel1 = New Panel()
        PanelInputBundle = New Panel()
        cmbAcadamicYear = New ComboBox()
        btnAddSettings = New Button()
        cmbSemester = New ComboBox()
        Label4 = New Label()
        LinkLabel2 = New LinkLabel()
        Label6 = New Label()
        Label1 = New Label()
        PanelRedDesign = New Panel()
        PanelWithCrudButtons = New Panel()
        Label2 = New Label()
        Panel1.SuspendLayout()
        PanelInputBundle.SuspendLayout()
        PanelWithCrudButtons.SuspendLayout()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.WhiteSmoke
        Panel1.Controls.Add(PanelInputBundle)
        Panel1.Controls.Add(PanelRedDesign)
        Panel1.Controls.Add(PanelWithCrudButtons)
        Panel1.Dock = DockStyle.Fill
        Panel1.Location = New Point(0, 0)
        Panel1.Margin = New Padding(4, 5, 4, 5)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(1811, 1000)
        Panel1.TabIndex = 4
        ' 
        ' PanelInputBundle
        ' 
        PanelInputBundle.BackColor = Color.White
        PanelInputBundle.Controls.Add(cmbAcadamicYear)
        PanelInputBundle.Controls.Add(btnAddSettings)
        PanelInputBundle.Controls.Add(cmbSemester)
        PanelInputBundle.Controls.Add(Label4)
        PanelInputBundle.Controls.Add(LinkLabel2)
        PanelInputBundle.Controls.Add(Label6)
        PanelInputBundle.Controls.Add(Label1)
        PanelInputBundle.Location = New Point(751, 86)
        PanelInputBundle.Margin = New Padding(4, 5, 4, 5)
        PanelInputBundle.Name = "PanelInputBundle"
        PanelInputBundle.Size = New Size(784, 861)
        PanelInputBundle.TabIndex = 0
        ' 
        ' cmbAcadamicYear
        ' 
        cmbAcadamicYear.BackColor = SystemColors.Info
        cmbAcadamicYear.Font = New Font("Garamond", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        cmbAcadamicYear.ForeColor = Color.Red
        cmbAcadamicYear.FormattingEnabled = True
        cmbAcadamicYear.Items.AddRange(New Object() {"Semester 1", "Semester 2"})
        cmbAcadamicYear.Location = New Point(29, 408)
        cmbAcadamicYear.Margin = New Padding(4, 5, 4, 5)
        cmbAcadamicYear.Name = "cmbAcadamicYear"
        cmbAcadamicYear.Size = New Size(726, 44)
        cmbAcadamicYear.TabIndex = 31
        cmbAcadamicYear.Text = "--Set Semester"
        ' 
        ' btnAddSettings
        ' 
        btnAddSettings.BackColor = Color.Red
        btnAddSettings.FlatAppearance.BorderSize = 0
        btnAddSettings.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Bold)
        btnAddSettings.ForeColor = Color.Transparent
        btnAddSettings.Location = New Point(242, 590)
        btnAddSettings.Margin = New Padding(4, 5, 4, 5)
        btnAddSettings.Name = "btnAddSettings"
        btnAddSettings.Size = New Size(342, 60)
        btnAddSettings.TabIndex = 12
        btnAddSettings.Text = "Apply"
        btnAddSettings.UseVisualStyleBackColor = False
        ' 
        ' cmbSemester
        ' 
        cmbSemester.BackColor = SystemColors.Info
        cmbSemester.Font = New Font("Garamond", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        cmbSemester.ForeColor = Color.Red
        cmbSemester.FormattingEnabled = True
        cmbSemester.Items.AddRange(New Object() {"Semester 1", "Semester 2"})
        cmbSemester.Location = New Point(21, 231)
        cmbSemester.Margin = New Padding(4, 5, 4, 5)
        cmbSemester.Name = "cmbSemester"
        cmbSemester.Size = New Size(726, 44)
        cmbSemester.TabIndex = 30
        cmbSemester.Text = "--Set Semester"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.BackColor = Color.Transparent
        Label4.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label4.ForeColor = Color.Black
        Label4.Location = New Point(34, 192)
        Label4.Margin = New Padding(4, 0, 4, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(172, 33)
        Label4.TabIndex = 29
        Label4.Text = "Set Semester"
        ' 
        ' LinkLabel2
        ' 
        LinkLabel2.AutoSize = True
        LinkLabel2.Font = New Font("Segoe UI", 12F)
        LinkLabel2.Location = New Point(284, 811)
        LinkLabel2.Margin = New Padding(4, 0, 4, 0)
        LinkLabel2.Name = "LinkLabel2"
        LinkLabel2.Size = New Size(211, 32)
        LinkLabel2.TabIndex = 25
        LinkLabel2.TabStop = True
        LinkLabel2.Text = "I have a Problem ?"
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.BackColor = Color.Transparent
        Label6.Font = New Font("Arial Rounded MT Bold", 21.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label6.ForeColor = Color.DarkSlateGray
        Label6.Location = New Point(19, 19)
        Label6.Margin = New Padding(4, 0, 4, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(318, 51)
        Label6.TabIndex = 15
        Label6.Text = "Configuration"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.BackColor = Color.Transparent
        Label1.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.Black
        Label1.Location = New Point(21, 369)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(247, 33)
        Label1.TabIndex = 16
        Label1.Text = "Set Acadamic Year"
        ' 
        ' PanelRedDesign
        ' 
        PanelRedDesign.BackColor = Color.DarkSlateGray
        PanelRedDesign.Location = New Point(728, 121)
        PanelRedDesign.Margin = New Padding(4, 5, 4, 5)
        PanelRedDesign.Name = "PanelRedDesign"
        PanelRedDesign.Size = New Size(678, 859)
        PanelRedDesign.TabIndex = 3
        ' 
        ' PanelWithCrudButtons
        ' 
        PanelWithCrudButtons.BackColor = Color.WhiteSmoke
        PanelWithCrudButtons.Controls.Add(Label2)
        PanelWithCrudButtons.Dock = DockStyle.Top
        PanelWithCrudButtons.Location = New Point(0, 0)
        PanelWithCrudButtons.Margin = New Padding(4, 5, 4, 5)
        PanelWithCrudButtons.Name = "PanelWithCrudButtons"
        PanelWithCrudButtons.Size = New Size(1811, 76)
        PanelWithCrudButtons.TabIndex = 1
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.BackColor = Color.Transparent
        Label2.Font = New Font("Arial Rounded MT Bold", 21.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label2.ForeColor = Color.DarkSlateGray
        Label2.Location = New Point(16, 11)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(200, 51)
        Label2.TabIndex = 16
        Label2.Text = "Settings"
        ' 
        ' UC_Settings
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(Panel1)
        Margin = New Padding(4)
        Name = "UC_Settings"
        Size = New Size(1811, 1000)
        Panel1.ResumeLayout(False)
        PanelInputBundle.ResumeLayout(False)
        PanelInputBundle.PerformLayout()
        PanelWithCrudButtons.ResumeLayout(False)
        PanelWithCrudButtons.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelInputBundle As Panel
    Friend WithEvents btnAddSettings As Button
    Friend WithEvents cmbSemester As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents LinkLabel2 As LinkLabel
    Friend WithEvents Label6 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents PanelRedDesign As Panel
    Friend WithEvents PanelWithCrudButtons As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents cmbAcadamicYear As ComboBox

End Class
