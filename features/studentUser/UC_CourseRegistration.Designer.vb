<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UC_CourseRegistration
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
        btnSubmitRegistration = New Button()
        Panel4 = New Panel()
        PictureBox1 = New PictureBox()
        lblSuccessFullRegistration = New Label()
        Panel1.SuspendLayout()
        Panel4.SuspendLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(btnSubmitRegistration)
        Panel1.Controls.Add(Panel4)
        Panel1.Dock = DockStyle.Fill
        Panel1.Location = New Point(0, 0)
        Panel1.Margin = New Padding(4)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(2311, 1002)
        Panel1.TabIndex = 0
        ' 
        ' btnSubmitRegistration
        ' 
        btnSubmitRegistration.BackColor = Color.DarkRed
        btnSubmitRegistration.FlatAppearance.BorderSize = 0
        btnSubmitRegistration.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Bold)
        btnSubmitRegistration.ForeColor = Color.Transparent
        btnSubmitRegistration.Location = New Point(753, 130)
        btnSubmitRegistration.Margin = New Padding(4, 5, 4, 5)
        btnSubmitRegistration.Name = "btnSubmitRegistration"
        btnSubmitRegistration.Size = New Size(342, 60)
        btnSubmitRegistration.TabIndex = 22
        btnSubmitRegistration.Text = "SUBMIT REGISTRATION"
        btnSubmitRegistration.UseVisualStyleBackColor = False
        ' 
        ' Panel4
        ' 
        Panel4.BackColor = Color.White
        Panel4.Controls.Add(PictureBox1)
        Panel4.Controls.Add(lblSuccessFullRegistration)
        Panel4.Location = New Point(384, 199)
        Panel4.Margin = New Padding(4)
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(1116, 746)
        Panel4.TabIndex = 21
        ' 
        ' PictureBox1
        ' 
        PictureBox1.BackColor = Color.Transparent
        PictureBox1.Location = New Point(245, 36)
        PictureBox1.Margin = New Padding(4, 5, 4, 5)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(630, 588)
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox1.TabIndex = 24
        PictureBox1.TabStop = False
        ' 
        ' lblSuccessFullRegistration
        ' 
        lblSuccessFullRegistration.AutoSize = True
        lblSuccessFullRegistration.BackColor = Color.Transparent
        lblSuccessFullRegistration.Font = New Font("Segoe UI", 22.2F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblSuccessFullRegistration.ForeColor = Color.DarkSlateGray
        lblSuccessFullRegistration.Location = New Point(130, 682)
        lblSuccessFullRegistration.Margin = New Padding(4, 0, 4, 0)
        lblSuccessFullRegistration.Name = "lblSuccessFullRegistration"
        lblSuccessFullRegistration.Size = New Size(854, 61)
        lblSuccessFullRegistration.TabIndex = 23
        lblSuccessFullRegistration.Text = "COURSE REGISTRATION IS SUCESSFULL"
        ' 
        ' UC_CourseRegistration
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(Panel1)
        Margin = New Padding(4)
        Name = "UC_CourseRegistration"
        Size = New Size(2311, 1002)
        Panel1.ResumeLayout(False)
        Panel4.ResumeLayout(False)
        Panel4.PerformLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents btnSubmitRegistration As Button
    Friend WithEvents lblSuccessFullRegistration As Label

End Class
