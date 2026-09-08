<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UC_Programs
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
        PanelRedDesign = New Panel()
        Label6 = New Label()
        txtProgramName = New TextBox()
        Label3 = New Label()
        Panel1 = New Panel()
        PanelInputBundle = New Panel()
        txtSetFees = New TextBox()
        btnAddProgram = New Button()
        Label2 = New Label()
        PanelWithCrudButtons = New Panel()
        Panel6 = New Panel()
        Panel1.SuspendLayout()
        PanelInputBundle.SuspendLayout()
        SuspendLayout()
        ' 
        ' PanelRedDesign
        ' 
        PanelRedDesign.BackColor = Color.DarkSlateGray
        PanelRedDesign.Location = New Point(609, 139)
        PanelRedDesign.Margin = New Padding(3, 4, 3, 4)
        PanelRedDesign.Name = "PanelRedDesign"
        PanelRedDesign.Size = New Size(497, 554)
        PanelRedDesign.TabIndex = 3
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.BackColor = Color.Transparent
        Label6.Font = New Font("Arial Rounded MT Bold", 21.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label6.ForeColor = Color.DarkGreen
        Label6.Location = New Point(15, 15)
        Label6.Name = "Label6"
        Label6.Size = New Size(432, 43)
        Label6.TabIndex = 15
        Label6.Text = "Create A New Program"
        ' 
        ' txtProgramName
        ' 
        txtProgramName.BackColor = Color.Ivory
        txtProgramName.Font = New Font("Garamond", 18F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        txtProgramName.ForeColor = SystemColors.ActiveCaptionText
        txtProgramName.Location = New Point(15, 153)
        txtProgramName.Margin = New Padding(3, 4, 3, 4)
        txtProgramName.Name = "txtProgramName"
        txtProgramName.Size = New Size(582, 41)
        txtProgramName.TabIndex = 15
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.BackColor = Color.Transparent
        Label3.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label3.ForeColor = Color.Black
        Label3.Location = New Point(13, 108)
        Label3.Name = "Label3"
        Label3.Size = New Size(182, 27)
        Label3.TabIndex = 14
        Label3.Text = "*Program Name"
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.WhiteSmoke
        Panel1.Controls.Add(PanelInputBundle)
        Panel1.Controls.Add(PanelWithCrudButtons)
        Panel1.Controls.Add(Panel6)
        Panel1.Controls.Add(PanelRedDesign)
        Panel1.Dock = DockStyle.Fill
        Panel1.Location = New Point(0, 0)
        Panel1.Margin = New Padding(3, 4, 3, 4)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(1795, 788)
        Panel1.TabIndex = 3
        ' 
        ' PanelInputBundle
        ' 
        PanelInputBundle.BackColor = Color.White
        PanelInputBundle.Controls.Add(txtSetFees)
        PanelInputBundle.Controls.Add(btnAddProgram)
        PanelInputBundle.Controls.Add(Label2)
        PanelInputBundle.Controls.Add(Label6)
        PanelInputBundle.Controls.Add(txtProgramName)
        PanelInputBundle.Controls.Add(Label3)
        PanelInputBundle.Location = New Point(625, 69)
        PanelInputBundle.Margin = New Padding(3, 4, 3, 4)
        PanelInputBundle.Name = "PanelInputBundle"
        PanelInputBundle.Size = New Size(627, 558)
        PanelInputBundle.TabIndex = 0
        ' 
        ' txtSetFees
        ' 
        txtSetFees.BackColor = Color.Ivory
        txtSetFees.Font = New Font("Garamond", 18F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        txtSetFees.ForeColor = SystemColors.ActiveCaptionText
        txtSetFees.Location = New Point(17, 312)
        txtSetFees.Margin = New Padding(3, 4, 3, 4)
        txtSetFees.Name = "txtSetFees"
        txtSetFees.Size = New Size(582, 41)
        txtSetFees.TabIndex = 28
        ' 
        ' btnAddProgram
        ' 
        btnAddProgram.BackColor = Color.Red
        btnAddProgram.FlatAppearance.BorderSize = 0
        btnAddProgram.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Bold)
        btnAddProgram.ForeColor = Color.Transparent
        btnAddProgram.Location = New Point(173, 416)
        btnAddProgram.Margin = New Padding(3, 4, 3, 4)
        btnAddProgram.Name = "btnAddProgram"
        btnAddProgram.Size = New Size(274, 48)
        btnAddProgram.TabIndex = 12
        btnAddProgram.Text = "Add Prgram"
        btnAddProgram.UseVisualStyleBackColor = False
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.BackColor = Color.Transparent
        Label2.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.ForeColor = Color.Black
        Label2.Location = New Point(17, 260)
        Label2.Name = "Label2"
        Label2.Size = New Size(215, 27)
        Label2.TabIndex = 27
        Label2.Text = "*Set Program Fees*"
        ' 
        ' PanelWithCrudButtons
        ' 
        PanelWithCrudButtons.BackColor = Color.WhiteSmoke
        PanelWithCrudButtons.Dock = DockStyle.Top
        PanelWithCrudButtons.Location = New Point(0, 0)
        PanelWithCrudButtons.Margin = New Padding(3, 4, 3, 4)
        PanelWithCrudButtons.Name = "PanelWithCrudButtons"
        PanelWithCrudButtons.Size = New Size(1795, 61)
        PanelWithCrudButtons.TabIndex = 1
        ' 
        ' Panel6
        ' 
        Panel6.BackColor = Color.DarkSlateGray
        Panel6.Location = New Point(16, 84)
        Panel6.Name = "Panel6"
        Panel6.Size = New Size(1244, 48)
        Panel6.TabIndex = 25
        ' 
        ' UC_Programs
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(Panel1)
        Name = "UC_Programs"
        Size = New Size(1795, 788)
        Panel1.ResumeLayout(False)
        PanelInputBundle.ResumeLayout(False)
        PanelInputBundle.PerformLayout()
        ResumeLayout(False)
    End Sub
    Friend WithEvents PanelRedDesign As Panel
    Friend WithEvents Label6 As Label
    Friend WithEvents txtProgramName As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelInputBundle As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents PanelWithCrudButtons As Panel
    Friend WithEvents btnAddProgram As Button
    Friend WithEvents Panel6 As Panel
    Friend WithEvents txtSetFees As TextBox

End Class
