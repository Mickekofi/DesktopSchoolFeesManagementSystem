<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PasswordHasher
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
        components = New ComponentModel.Container()
        Panel1 = New Panel()
        lblStatus = New Label()
        Panel3 = New Panel()
        lblUserlogin = New Label()
        PictureBox1 = New PictureBox()
        btnGenerate = New Button()
        lblPassword = New Label()
        txtPassword = New TextBox()
        Panel4 = New Panel()
        fadeTimer2 = New Timer(components)
        Panel1.SuspendLayout()
        Panel3.SuspendLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.AutoScroll = True
        Panel1.BackColor = Color.White
        Panel1.Controls.Add(lblStatus)
        Panel1.Controls.Add(Panel3)
        Panel1.Controls.Add(Panel4)
        Panel1.Dock = DockStyle.Fill
        Panel1.Location = New Point(0, 0)
        Panel1.Margin = New Padding(4, 5, 4, 5)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(1599, 974)
        Panel1.TabIndex = 2
        ' 
        ' lblStatus
        ' 
        lblStatus.AutoSize = True
        lblStatus.Location = New Point(752, 600)
        lblStatus.Margin = New Padding(4, 0, 4, 0)
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(0, 25)
        lblStatus.TabIndex = 7
        ' 
        ' Panel3
        ' 
        Panel3.BackColor = Color.White
        Panel3.Controls.Add(lblUserlogin)
        Panel3.Controls.Add(PictureBox1)
        Panel3.Controls.Add(btnGenerate)
        Panel3.Controls.Add(lblPassword)
        Panel3.Controls.Add(txtPassword)
        Panel3.Location = New Point(181, 16)
        Panel3.Margin = New Padding(4, 5, 4, 5)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(1095, 864)
        Panel3.TabIndex = 11
        ' 
        ' lblUserlogin
        ' 
        lblUserlogin.AutoSize = True
        lblUserlogin.BackColor = Color.Transparent
        lblUserlogin.Font = New Font("Garamond", 24F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblUserlogin.Location = New Point(412, 454)
        lblUserlogin.Margin = New Padding(4, 0, 4, 0)
        lblUserlogin.Name = "lblUserlogin"
        lblUserlogin.Size = New Size(383, 54)
        lblUserlogin.TabIndex = 0
        lblUserlogin.Text = "Password Hasher"
        lblUserlogin.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' PictureBox1
        ' 
        PictureBox1.BackColor = Color.White
        PictureBox1.Location = New Point(368, -84)
        PictureBox1.Margin = New Padding(4, 5, 4, 5)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(468, 509)
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox1.TabIndex = 8
        PictureBox1.TabStop = False
        ' 
        ' btnGenerate
        ' 
        btnGenerate.BackColor = Color.SeaGreen
        btnGenerate.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnGenerate.ForeColor = Color.LavenderBlush
        btnGenerate.Location = New Point(368, 764)
        btnGenerate.Margin = New Padding(4, 5, 4, 5)
        btnGenerate.Name = "btnGenerate"
        btnGenerate.Size = New Size(506, 71)
        btnGenerate.TabIndex = 2
        btnGenerate.Text = "LOGIN"
        btnGenerate.UseVisualStyleBackColor = False
        ' 
        ' lblPassword
        ' 
        lblPassword.AutoSize = True
        lblPassword.BackColor = Color.Transparent
        lblPassword.Font = New Font("Garamond", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblPassword.Location = New Point(571, 618)
        lblPassword.Margin = New Padding(4, 0, 4, 0)
        lblPassword.Name = "lblPassword"
        lblPassword.Size = New Size(110, 27)
        lblPassword.TabIndex = 5
        lblPassword.Text = "Password"
        ' 
        ' txtPassword
        ' 
        txtPassword.BackColor = Color.GhostWhite
        txtPassword.Font = New Font("Arial Rounded MT Bold", 14.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txtPassword.Location = New Point(246, 665)
        txtPassword.Margin = New Padding(4, 5, 4, 5)
        txtPassword.Name = "txtPassword"
        txtPassword.Size = New Size(782, 40)
        txtPassword.TabIndex = 4
        ' 
        ' Panel4
        ' 
        Panel4.BackColor = Color.DarkRed
        Panel4.Location = New Point(156, 168)
        Panel4.Margin = New Padding(4, 5, 4, 5)
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(958, 718)
        Panel4.TabIndex = 13
        ' 
        ' fadeTimer2
        ' 
        fadeTimer2.Interval = 30
        ' 
        ' PasswordHasher
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1599, 974)
        Controls.Add(Panel1)
        Margin = New Padding(4)
        Name = "PasswordHasher"
        Text = "PasswordHasher"
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        Panel3.ResumeLayout(False)
        Panel3.PerformLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents lblStatus As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents lblUserlogin As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents btnNoUse As Button
    Friend WithEvents btnGenerate As Button
    Friend WithEvents lblPassword As Label
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents fadeTimer2 As Timer
End Class
