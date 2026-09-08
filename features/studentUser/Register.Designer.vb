<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Register
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
        Panel3 = New Panel()
        Panel1 = New Panel()
        Panel2 = New Panel()
        Pan = New Panel()
        lblfull_name = New Label()
        Panel8 = New Panel()
        txtPassword = New TextBox()
        lblStrength = New Label()
        Label2 = New Label()
        txtPasswordStrength = New Label()
        txtPhone = New TextBox()
        Label1 = New Label()
        btnSubmit = New Button()
        btnReload = New Button()
        lblPhotoPath = New Label()
        Panel4 = New Panel()
        btnUploadPhoto = New Button()
        picPassport = New PictureBox()
        cmbGender = New ComboBox()
        dtpDOB = New DateTimePicker()
        lbGender = New Label()
        lbDOB = New Label()
        lbPhone = New Label()
        txtEmail = New TextBox()
        lbEmail = New Label()
        lbFullName = New Label()
        Panel5 = New Panel()
        Panel6 = New Panel()
        Panel7 = New Panel()
        Panel9 = New Panel()
        Panel10 = New Panel()
        Panel11 = New Panel()
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        Pan.SuspendLayout()
        Panel4.SuspendLayout()
        CType(picPassport, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Panel3
        ' 
        Panel3.BackColor = Color.DarkSlateGray
        Panel3.Location = New Point(341, 226)
        Panel3.Margin = New Padding(4, 5, 4, 5)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(1029, 994)
        Panel3.TabIndex = 4
        ' 
        ' Panel1
        ' 
        Panel1.AutoScroll = True
        Panel1.BackColor = SystemColors.ButtonFace
        Panel1.Controls.Add(Panel2)
        Panel1.Controls.Add(Panel3)
        Panel1.Dock = DockStyle.Fill
        Panel1.Location = New Point(0, 0)
        Panel1.Margin = New Padding(4, 5, 4, 5)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(1769, 1050)
        Panel1.TabIndex = 2
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = SystemColors.ButtonHighlight
        Panel2.Controls.Add(Pan)
        Panel2.Controls.Add(Panel8)
        Panel2.Controls.Add(txtPassword)
        Panel2.Controls.Add(lblStrength)
        Panel2.Controls.Add(Label2)
        Panel2.Controls.Add(txtPasswordStrength)
        Panel2.Controls.Add(txtPhone)
        Panel2.Controls.Add(Label1)
        Panel2.Controls.Add(btnSubmit)
        Panel2.Controls.Add(btnReload)
        Panel2.Controls.Add(lblPhotoPath)
        Panel2.Controls.Add(Panel4)
        Panel2.Controls.Add(cmbGender)
        Panel2.Controls.Add(dtpDOB)
        Panel2.Controls.Add(lbGender)
        Panel2.Controls.Add(lbDOB)
        Panel2.Controls.Add(lbPhone)
        Panel2.Controls.Add(txtEmail)
        Panel2.Controls.Add(lbEmail)
        Panel2.Controls.Add(lbFullName)
        Panel2.Controls.Add(Panel5)
        Panel2.Controls.Add(Panel6)
        Panel2.Controls.Add(Panel7)
        Panel2.Controls.Add(Panel9)
        Panel2.Controls.Add(Panel10)
        Panel2.Controls.Add(Panel11)
        Panel2.Location = New Point(369, 56)
        Panel2.Margin = New Padding(4, 5, 4, 5)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(1251, 1484)
        Panel2.TabIndex = 0
        ' 
        ' Pan
        ' 
        Pan.BackColor = SystemColors.ButtonHighlight
        Pan.Controls.Add(lblfull_name)
        Pan.Location = New Point(43, 461)
        Pan.Margin = New Padding(4)
        Pan.Name = "Pan"
        Pan.Size = New Size(1154, 51)
        Pan.TabIndex = 77
        ' 
        ' lblfull_name
        ' 
        lblfull_name.AutoSize = True
        lblfull_name.Font = New Font("Garamond", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblfull_name.ForeColor = Color.Black
        lblfull_name.Location = New Point(7, 10)
        lblfull_name.Margin = New Padding(4, 0, 4, 0)
        lblfull_name.Name = "lblfull_name"
        lblfull_name.Size = New Size(223, 31)
        lblfull_name.TabIndex = 0
        lblfull_name.Text = "Simms John Jerry"
        ' 
        ' Panel8
        ' 
        Panel8.BackColor = Color.DarkSlateGray
        Panel8.Location = New Point(33, 450)
        Panel8.Margin = New Padding(4)
        Panel8.Name = "Panel8"
        Panel8.Size = New Size(155, 43)
        Panel8.TabIndex = 82
        ' 
        ' txtPassword
        ' 
        txtPassword.BackColor = SystemColors.ButtonFace
        txtPassword.Font = New Font("Garamond", 14.25F)
        txtPassword.Location = New Point(50, 1164)
        txtPassword.Margin = New Padding(4, 5, 4, 5)
        txtPassword.Name = "txtPassword"
        txtPassword.Size = New Size(1153, 40)
        txtPassword.TabIndex = 76
        ' 
        ' lblStrength
        ' 
        lblStrength.AutoSize = True
        lblStrength.Font = New Font("Georgia", 10.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblStrength.Location = New Point(38, 1232)
        lblStrength.Margin = New Padding(4, 0, 4, 0)
        lblStrength.Name = "lblStrength"
        lblStrength.RightToLeft = RightToLeft.Yes
        lblStrength.Size = New Size(180, 25)
        lblStrength.TabIndex = 75
        lblStrength.Text = "eg:$LearnCreate6"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Garamond", 14.25F)
        Label2.Location = New Point(38, 1312)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.RightToLeft = RightToLeft.Yes
        Label2.Size = New Size(0, 33)
        Label2.TabIndex = 74
        ' 
        ' txtPasswordStrength
        ' 
        txtPasswordStrength.AutoSize = True
        txtPasswordStrength.Font = New Font("Garamond", 14.25F)
        txtPasswordStrength.Location = New Point(42, 1305)
        txtPasswordStrength.Margin = New Padding(4, 0, 4, 0)
        txtPasswordStrength.Name = "txtPasswordStrength"
        txtPasswordStrength.RightToLeft = RightToLeft.Yes
        txtPasswordStrength.Size = New Size(0, 33)
        txtPasswordStrength.TabIndex = 73
        ' 
        ' txtPhone
        ' 
        txtPhone.Font = New Font("Garamond", 14.25F)
        txtPhone.Location = New Point(41, 735)
        txtPhone.Margin = New Padding(4, 5, 4, 5)
        txtPhone.Name = "txtPhone"
        txtPhone.Size = New Size(1153, 40)
        txtPhone.TabIndex = 72
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Garamond", 14.25F)
        Label1.Location = New Point(38, 1109)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.RightToLeft = RightToLeft.Yes
        Label1.Size = New Size(298, 33)
        Label1.TabIndex = 70
        Label1.Text = "*Enter a New Password*"
        ' 
        ' btnSubmit
        ' 
        btnSubmit.BackColor = Color.Red
        btnSubmit.Font = New Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnSubmit.ForeColor = Color.White
        btnSubmit.Location = New Point(38, 1329)
        btnSubmit.Margin = New Padding(4, 5, 4, 5)
        btnSubmit.Name = "btnSubmit"
        btnSubmit.Size = New Size(478, 70)
        btnSubmit.TabIndex = 67
        btnSubmit.Text = "Submit"
        btnSubmit.UseVisualStyleBackColor = False
        ' 
        ' btnReload
        ' 
        btnReload.BackColor = Color.DarkSlateGray
        btnReload.Font = New Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnReload.ForeColor = Color.White
        btnReload.Location = New Point(1062, 26)
        btnReload.Margin = New Padding(4, 5, 4, 5)
        btnReload.Name = "btnReload"
        btnReload.Size = New Size(156, 49)
        btnReload.TabIndex = 36
        btnReload.Text = "Reload Page"
        btnReload.UseVisualStyleBackColor = False
        ' 
        ' lblPhotoPath
        ' 
        lblPhotoPath.AutoSize = True
        lblPhotoPath.Font = New Font("Segoe UI", 5F)
        lblPhotoPath.Location = New Point(880, 439)
        lblPhotoPath.Margin = New Padding(4, 0, 4, 0)
        lblPhotoPath.Name = "lblPhotoPath"
        lblPhotoPath.Size = New Size(69, 12)
        lblPhotoPath.TabIndex = 63
        lblPhotoPath.Text = "path To Image"
        ' 
        ' Panel4
        ' 
        Panel4.Controls.Add(btnUploadPhoto)
        Panel4.Controls.Add(picPassport)
        Panel4.Location = New Point(969, 100)
        Panel4.Margin = New Padding(4, 5, 4, 5)
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(261, 299)
        Panel4.TabIndex = 61
        ' 
        ' btnUploadPhoto
        ' 
        btnUploadPhoto.BackColor = Color.Red
        btnUploadPhoto.Font = New Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnUploadPhoto.ForeColor = Color.White
        btnUploadPhoto.Location = New Point(30, 248)
        btnUploadPhoto.Margin = New Padding(4, 5, 4, 5)
        btnUploadPhoto.Name = "btnUploadPhoto"
        btnUploadPhoto.Size = New Size(198, 49)
        btnUploadPhoto.TabIndex = 35
        btnUploadPhoto.Text = "Upload Passport"
        btnUploadPhoto.UseVisualStyleBackColor = False
        ' 
        ' picPassport
        ' 
        picPassport.BackColor = Color.White
        picPassport.Location = New Point(19, 6)
        picPassport.Margin = New Padding(4, 5, 4, 5)
        picPassport.Name = "picPassport"
        picPassport.Size = New Size(216, 230)
        picPassport.SizeMode = PictureBoxSizeMode.StretchImage
        picPassport.TabIndex = 0
        picPassport.TabStop = False
        ' 
        ' cmbGender
        ' 
        cmbGender.BackColor = SystemColors.Info
        cmbGender.DropDownStyle = ComboBoxStyle.DropDownList
        cmbGender.Font = New Font("Garamond", 14.25F)
        cmbGender.FormattingEnabled = True
        cmbGender.Items.AddRange(New Object() {"Male", "Female"})
        cmbGender.Location = New Point(50, 1005)
        cmbGender.Margin = New Padding(4, 5, 4, 5)
        cmbGender.Name = "cmbGender"
        cmbGender.Size = New Size(1153, 41)
        cmbGender.TabIndex = 37
        ' 
        ' dtpDOB
        ' 
        dtpDOB.Font = New Font("Garamond", 14.25F)
        dtpDOB.Location = New Point(42, 869)
        dtpDOB.Margin = New Padding(4, 5, 4, 5)
        dtpDOB.Name = "dtpDOB"
        dtpDOB.Size = New Size(1153, 40)
        dtpDOB.TabIndex = 21
        ' 
        ' lbGender
        ' 
        lbGender.AutoSize = True
        lbGender.Font = New Font("Garamond", 14.25F)
        lbGender.Location = New Point(42, 948)
        lbGender.Margin = New Padding(4, 0, 4, 0)
        lbGender.Name = "lbGender"
        lbGender.RightToLeft = RightToLeft.Yes
        lbGender.Size = New Size(101, 33)
        lbGender.TabIndex = 11
        lbGender.Text = "Gender"
        ' 
        ' lbDOB
        ' 
        lbDOB.AutoSize = True
        lbDOB.Font = New Font("Garamond", 14.25F)
        lbDOB.Location = New Point(41, 823)
        lbDOB.Margin = New Padding(4, 0, 4, 0)
        lbDOB.Name = "lbDOB"
        lbDOB.RightToLeft = RightToLeft.Yes
        lbDOB.Size = New Size(165, 33)
        lbDOB.TabIndex = 10
        lbDOB.Text = "Date of Birth"
        ' 
        ' lbPhone
        ' 
        lbPhone.AutoSize = True
        lbPhone.Font = New Font("Garamond", 14.25F)
        lbPhone.Location = New Point(38, 684)
        lbPhone.Margin = New Padding(4, 0, 4, 0)
        lbPhone.Name = "lbPhone"
        lbPhone.RightToLeft = RightToLeft.Yes
        lbPhone.Size = New Size(88, 33)
        lbPhone.TabIndex = 5
        lbPhone.Text = "Phone"
        ' 
        ' txtEmail
        ' 
        txtEmail.BackColor = SystemColors.ButtonHighlight
        txtEmail.Font = New Font("Garamond", 14.25F)
        txtEmail.Location = New Point(41, 609)
        txtEmail.Margin = New Padding(4, 5, 4, 5)
        txtEmail.Name = "txtEmail"
        txtEmail.Size = New Size(1153, 40)
        txtEmail.TabIndex = 4
        ' 
        ' lbEmail
        ' 
        lbEmail.AutoSize = True
        lbEmail.Font = New Font("Garamond", 14.25F)
        lbEmail.Location = New Point(38, 559)
        lbEmail.Margin = New Padding(4, 0, 4, 0)
        lbEmail.Name = "lbEmail"
        lbEmail.RightToLeft = RightToLeft.Yes
        lbEmail.Size = New Size(82, 33)
        lbEmail.TabIndex = 3
        lbEmail.Text = "Email"
        ' 
        ' lbFullName
        ' 
        lbFullName.AutoSize = True
        lbFullName.Font = New Font("Garamond", 14.25F)
        lbFullName.Location = New Point(49, 405)
        lbFullName.Margin = New Padding(4, 0, 4, 0)
        lbFullName.Name = "lbFullName"
        lbFullName.RightToLeft = RightToLeft.Yes
        lbFullName.Size = New Size(134, 33)
        lbFullName.TabIndex = 2
        lbFullName.Text = "Full Name"
        ' 
        ' Panel5
        ' 
        Panel5.BackColor = Color.Red
        Panel5.Location = New Point(961, 119)
        Panel5.Margin = New Padding(4, 5, 4, 5)
        Panel5.Name = "Panel5"
        Panel5.Size = New Size(234, 299)
        Panel5.TabIndex = 62
        ' 
        ' Panel6
        ' 
        Panel6.BackColor = Color.DarkSlateGray
        Panel6.Location = New Point(38, 728)
        Panel6.Margin = New Padding(4)
        Panel6.Name = "Panel6"
        Panel6.Size = New Size(129, 47)
        Panel6.TabIndex = 83
        ' 
        ' Panel7
        ' 
        Panel7.BackColor = Color.DarkSlateGray
        Panel7.Location = New Point(38, 602)
        Panel7.Margin = New Padding(4)
        Panel7.Name = "Panel7"
        Panel7.Size = New Size(114, 44)
        Panel7.TabIndex = 84
        ' 
        ' Panel9
        ' 
        Panel9.BackColor = Color.DarkSlateGray
        Panel9.Location = New Point(38, 862)
        Panel9.Margin = New Padding(4)
        Panel9.Name = "Panel9"
        Panel9.Size = New Size(118, 41)
        Panel9.TabIndex = 85
        ' 
        ' Panel10
        ' 
        Panel10.BackColor = Color.DarkSlateGray
        Panel10.Location = New Point(43, 994)
        Panel10.Margin = New Padding(4)
        Panel10.Name = "Panel10"
        Panel10.Size = New Size(147, 40)
        Panel10.TabIndex = 86
        ' 
        ' Panel11
        ' 
        Panel11.BackColor = Color.DarkSlateGray
        Panel11.Location = New Point(43, 1158)
        Panel11.Margin = New Padding(4)
        Panel11.Name = "Panel11"
        Panel11.Size = New Size(143, 37)
        Panel11.TabIndex = 87
        ' 
        ' Register
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1769, 1050)
        Controls.Add(Panel1)
        Margin = New Padding(4)
        Name = "Register"
        Text = "Form"
        Panel1.ResumeLayout(False)
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        Pan.ResumeLayout(False)
        Pan.PerformLayout()
        Panel4.ResumeLayout(False)
        CType(picPassport, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub
    Protected WithEvents Panel3 As Panel
    Friend WithEvents Panel1 As Panel
    Protected WithEvents Panel2 As Panel
    Friend WithEvents Pan As Panel
    Friend WithEvents lblfull_name As Label
    Friend WithEvents Panel8 As Panel
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents lblStrength As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txtPasswordStrength As Label
    Friend WithEvents txtPhone As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents btnSubmit As Button
    Friend WithEvents btnReload As Button
    Friend WithEvents lblPhotoPath As Label
    Friend WithEvents Panel4 As Panel
    Friend WithEvents btnUploadPhoto As Button
    Friend WithEvents picPassport As PictureBox
    Friend WithEvents cmbGender As ComboBox
    Friend WithEvents dtpDOB As DateTimePicker
    Friend WithEvents lbGender As Label
    Friend WithEvents lbDOB As Label
    Friend WithEvents lbPhone As Label
    Friend WithEvents txtEmail As TextBox
    Friend WithEvents lbEmail As Label
    Friend WithEvents lbFullName As Label
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Panel9 As Panel
    Friend WithEvents Panel10 As Panel
    Friend WithEvents Panel11 As Panel
End Class
