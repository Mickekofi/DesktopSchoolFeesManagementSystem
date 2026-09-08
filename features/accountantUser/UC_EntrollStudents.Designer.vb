<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UC_EntrollStudents
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
        dgvApplicants = New DataGridView()
        Panel1 = New Panel()
        PanelWithDgv = New Panel()
        PanelWithCrudButtons = New Panel()
        Label2 = New Label()
        PanelWithSearch = New Panel()
        btnDelete = New Button()
        Panel2 = New Panel()
        lblFilePath = New Label()
        PictureBox1 = New PictureBox()
        btnUpload = New Button()
        cmbProgramShow = New ComboBox()
        txtIndexNumberSearch = New TextBox()
        Qu = New Label()
        Label10 = New Label()
        CType(dgvApplicants, ComponentModel.ISupportInitialize).BeginInit()
        Panel1.SuspendLayout()
        PanelWithDgv.SuspendLayout()
        PanelWithCrudButtons.SuspendLayout()
        PanelWithSearch.SuspendLayout()
        Panel2.SuspendLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' dgvApplicants
        ' 
        dgvApplicants.BackgroundColor = Color.WhiteSmoke
        dgvApplicants.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvApplicants.Location = New Point(0, 303)
        dgvApplicants.Margin = New Padding(4, 5, 4, 5)
        dgvApplicants.Name = "dgvApplicants"
        dgvApplicants.RowHeadersWidth = 51
        dgvApplicants.Size = New Size(1884, 693)
        dgvApplicants.TabIndex = 28
        ' 
        ' Panel1
        ' 
        Panel1.AutoScroll = True
        Panel1.BackColor = Color.White
        Panel1.Controls.Add(PanelWithDgv)
        Panel1.Dock = DockStyle.Fill
        Panel1.Location = New Point(0, 0)
        Panel1.Margin = New Padding(4, 5, 4, 5)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(2231, 1018)
        Panel1.TabIndex = 5
        ' 
        ' PanelWithDgv
        ' 
        PanelWithDgv.AutoScroll = True
        PanelWithDgv.BackColor = Color.White
        PanelWithDgv.Controls.Add(PanelWithCrudButtons)
        PanelWithDgv.Controls.Add(dgvApplicants)
        PanelWithDgv.Controls.Add(PanelWithSearch)
        PanelWithDgv.Dock = DockStyle.Fill
        PanelWithDgv.Location = New Point(0, 0)
        PanelWithDgv.Margin = New Padding(4, 5, 4, 5)
        PanelWithDgv.Name = "PanelWithDgv"
        PanelWithDgv.Size = New Size(2231, 1018)
        PanelWithDgv.TabIndex = 2
        ' 
        ' PanelWithCrudButtons
        ' 
        PanelWithCrudButtons.BackColor = Color.White
        PanelWithCrudButtons.Controls.Add(Label2)
        PanelWithCrudButtons.Dock = DockStyle.Top
        PanelWithCrudButtons.Location = New Point(0, 0)
        PanelWithCrudButtons.Margin = New Padding(4, 5, 4, 5)
        PanelWithCrudButtons.Name = "PanelWithCrudButtons"
        PanelWithCrudButtons.Size = New Size(2272, 76)
        PanelWithCrudButtons.TabIndex = 29
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.BackColor = Color.Transparent
        Label2.Font = New Font("Segoe UI", 22.2F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.ForeColor = Color.DarkSlateGray
        Label2.Location = New Point(0, 9)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(564, 61)
        Label2.TabIndex = 22
        Label2.Text = "Student Activation Portal"
        ' 
        ' PanelWithSearch
        ' 
        PanelWithSearch.BackColor = Color.WhiteSmoke
        PanelWithSearch.Controls.Add(btnDelete)
        PanelWithSearch.Controls.Add(Panel2)
        PanelWithSearch.Controls.Add(cmbProgramShow)
        PanelWithSearch.Controls.Add(txtIndexNumberSearch)
        PanelWithSearch.Controls.Add(Qu)
        PanelWithSearch.Controls.Add(Label10)
        PanelWithSearch.Location = New Point(0, 76)
        PanelWithSearch.Margin = New Padding(4, 5, 4, 5)
        PanelWithSearch.Name = "PanelWithSearch"
        PanelWithSearch.Size = New Size(2272, 226)
        PanelWithSearch.TabIndex = 27
        ' 
        ' btnDelete
        ' 
        btnDelete.BackColor = Color.Red
        btnDelete.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnDelete.ForeColor = Color.LavenderBlush
        btnDelete.Location = New Point(651, 164)
        btnDelete.Margin = New Padding(4, 5, 4, 5)
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(184, 58)
        btnDelete.TabIndex = 29
        btnDelete.Text = "Delete"
        btnDelete.UseVisualStyleBackColor = False
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(lblFilePath)
        Panel2.Controls.Add(PictureBox1)
        Panel2.Controls.Add(btnUpload)
        Panel2.Location = New Point(1138, 20)
        Panel2.Margin = New Padding(4)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(615, 198)
        Panel2.TabIndex = 28
        ' 
        ' lblFilePath
        ' 
        lblFilePath.AutoSize = True
        lblFilePath.Font = New Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblFilePath.Location = New Point(172, 31)
        lblFilePath.Margin = New Padding(4, 0, 4, 0)
        lblFilePath.Name = "lblFilePath"
        lblFilePath.Size = New Size(406, 38)
        lblFilePath.TabIndex = 2
        lblFilePath.Text = "File://upload_student_excel_List"
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Image = My.Resources.Resources.file_logo
        PictureBox1.Location = New Point(105, 24)
        PictureBox1.Margin = New Padding(4)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(60, 46)
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox1.TabIndex = 5
        PictureBox1.TabStop = False
        ' 
        ' btnUpload
        ' 
        btnUpload.BackColor = Color.Red
        btnUpload.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnUpload.ForeColor = Color.LavenderBlush
        btnUpload.Location = New Point(160, 110)
        btnUpload.Margin = New Padding(4, 5, 4, 5)
        btnUpload.Name = "btnUpload"
        btnUpload.Size = New Size(372, 71)
        btnUpload.TabIndex = 3
        btnUpload.Text = "UPLOAD"
        btnUpload.UseVisualStyleBackColor = False
        ' 
        ' cmbProgramShow
        ' 
        cmbProgramShow.BackColor = Color.GhostWhite
        cmbProgramShow.Font = New Font("Garamond", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        cmbProgramShow.ForeColor = Color.Red
        cmbProgramShow.FormattingEnabled = True
        cmbProgramShow.Location = New Point(26, 69)
        cmbProgramShow.Margin = New Padding(4, 5, 4, 5)
        cmbProgramShow.Name = "cmbProgramShow"
        cmbProgramShow.Size = New Size(600, 44)
        cmbProgramShow.TabIndex = 27
        cmbProgramShow.Text = "--Select Program--"
        ' 
        ' txtIndexNumberSearch
        ' 
        txtIndexNumberSearch.BackColor = Color.GhostWhite
        txtIndexNumberSearch.Font = New Font("Garamond", 14F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        txtIndexNumberSearch.ForeColor = SystemColors.ActiveCaptionText
        txtIndexNumberSearch.Location = New Point(26, 179)
        txtIndexNumberSearch.Margin = New Padding(4, 5, 4, 5)
        txtIndexNumberSearch.Name = "txtIndexNumberSearch"
        txtIndexNumberSearch.Size = New Size(600, 39)
        txtIndexNumberSearch.TabIndex = 27
        ' 
        ' Qu
        ' 
        Qu.AutoSize = True
        Qu.BackColor = Color.Transparent
        Qu.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Qu.ForeColor = Color.Black
        Qu.Location = New Point(26, 138)
        Qu.Margin = New Padding(4, 0, 4, 0)
        Qu.Name = "Qu"
        Qu.Size = New Size(329, 33)
        Qu.TabIndex = 19
        Qu.Text = "Search By Index Number"
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.BackColor = Color.Transparent
        Label10.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label10.ForeColor = Color.Black
        Label10.Location = New Point(21, 24)
        Label10.Margin = New Padding(4, 0, 4, 0)
        Label10.Name = "Label10"
        Label10.Size = New Size(122, 33)
        Label10.TabIndex = 18
        Label10.Text = "Program"
        ' 
        ' UC_EntrollStudents
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(Panel1)
        Margin = New Padding(4)
        Name = "UC_EntrollStudents"
        Size = New Size(2231, 1018)
        CType(dgvApplicants, ComponentModel.ISupportInitialize).EndInit()
        Panel1.ResumeLayout(False)
        PanelWithDgv.ResumeLayout(False)
        PanelWithCrudButtons.ResumeLayout(False)
        PanelWithCrudButtons.PerformLayout()
        PanelWithSearch.ResumeLayout(False)
        PanelWithSearch.PerformLayout()
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents dgvApplicants As DataGridView
    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelWithDgv As Panel
    Friend WithEvents PanelWithCrudButtons As Panel
    Friend WithEvents PanelWithSearch As Panel
    Friend WithEvents cmbProgramShow As ComboBox
    Friend WithEvents txtIndexNumberSearch As TextBox
    Friend WithEvents Qu As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents btnUpload As Button
    Friend WithEvents lblFilePath As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents btnDelete As Button

End Class
