<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UC_Statistics
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
        dgvStatistics = New DataGridView()
        PanelBackground = New Panel()
        PanelWithDgv = New Panel()
        FlowLayoutPanel1 = New FlowLayoutPanel()
        PanelWithSearch = New Panel()
        Label5 = New Label()
        txtSearch = New TextBox()
        cmbStatus = New ComboBox()
        cmbProgram = New ComboBox()
        Label1 = New Label()
        btnExport = New Button()
        cmbYear = New ComboBox()
        Panel1 = New Panel()
        Panel2 = New Panel()
        Panel5 = New Panel()
        Label4 = New Label()
        lblTotalDeficit = New Label()
        Panel4 = New Panel()
        Label3 = New Label()
        lblTotalCollected = New Label()
        Panel3 = New Panel()
        Label2 = New Label()
        lblTotalExpected = New Label()
        CType(dgvStatistics, ComponentModel.ISupportInitialize).BeginInit()
        PanelBackground.SuspendLayout()
        PanelWithDgv.SuspendLayout()
        FlowLayoutPanel1.SuspendLayout()
        PanelWithSearch.SuspendLayout()
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        Panel5.SuspendLayout()
        Panel4.SuspendLayout()
        Panel3.SuspendLayout()
        SuspendLayout()
        ' 
        ' dgvStatistics
        ' 
        dgvStatistics.BackgroundColor = Color.White
        dgvStatistics.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvStatistics.Location = New Point(4, 4)
        dgvStatistics.Margin = New Padding(4)
        dgvStatistics.Name = "dgvStatistics"
        dgvStatistics.RowHeadersWidth = 51
        dgvStatistics.Size = New Size(1453, 972)
        dgvStatistics.TabIndex = 0
        ' 
        ' PanelBackground
        ' 
        PanelBackground.Controls.Add(PanelWithDgv)
        PanelBackground.Dock = DockStyle.Fill
        PanelBackground.Location = New Point(0, 0)
        PanelBackground.Margin = New Padding(4)
        PanelBackground.Name = "PanelBackground"
        PanelBackground.Size = New Size(1771, 735)
        PanelBackground.TabIndex = 1
        ' 
        ' PanelWithDgv
        ' 
        PanelWithDgv.AutoScroll = True
        PanelWithDgv.BackColor = Color.White
        PanelWithDgv.Controls.Add(FlowLayoutPanel1)
        PanelWithDgv.Dock = DockStyle.Fill
        PanelWithDgv.Location = New Point(0, 0)
        PanelWithDgv.Margin = New Padding(4, 5, 4, 5)
        PanelWithDgv.Name = "PanelWithDgv"
        PanelWithDgv.Size = New Size(1771, 735)
        PanelWithDgv.TabIndex = 4
        ' 
        ' FlowLayoutPanel1
        ' 
        FlowLayoutPanel1.Controls.Add(PanelWithSearch)
        FlowLayoutPanel1.Controls.Add(Panel1)
        FlowLayoutPanel1.Controls.Add(Panel2)
        FlowLayoutPanel1.Dock = DockStyle.Fill
        FlowLayoutPanel1.Location = New Point(0, 0)
        FlowLayoutPanel1.Margin = New Padding(4)
        FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        FlowLayoutPanel1.Size = New Size(1771, 735)
        FlowLayoutPanel1.TabIndex = 0
        ' 
        ' PanelWithSearch
        ' 
        PanelWithSearch.BackColor = Color.White
        PanelWithSearch.Controls.Add(Label5)
        PanelWithSearch.Controls.Add(txtSearch)
        PanelWithSearch.Controls.Add(cmbStatus)
        PanelWithSearch.Controls.Add(cmbProgram)
        PanelWithSearch.Controls.Add(Label1)
        PanelWithSearch.Controls.Add(btnExport)
        PanelWithSearch.Controls.Add(cmbYear)
        PanelWithSearch.Location = New Point(4, 5)
        PanelWithSearch.Margin = New Padding(4, 5, 4, 5)
        PanelWithSearch.Name = "PanelWithSearch"
        PanelWithSearch.Size = New Size(1753, 146)
        PanelWithSearch.TabIndex = 28
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.BackColor = Color.Transparent
        Label5.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label5.ForeColor = Color.Black
        Label5.Location = New Point(976, 99)
        Label5.Margin = New Padding(4, 0, 4, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(69, 25)
        Label5.TabIndex = 70
        Label5.Text = "Search"
        ' 
        ' txtSearch
        ' 
        txtSearch.BackColor = SystemColors.ButtonHighlight
        txtSearch.Font = New Font("Garamond", 14.25F)
        txtSearch.Location = New Point(1053, 91)
        txtSearch.Margin = New Padding(4, 5, 4, 5)
        txtSearch.Name = "txtSearch"
        txtSearch.Size = New Size(333, 40)
        txtSearch.TabIndex = 67
        ' 
        ' cmbStatus
        ' 
        cmbStatus.Font = New Font("Garamond", 14.25F, FontStyle.Bold)
        cmbStatus.ForeColor = Color.DarkSlateGray
        cmbStatus.FormattingEnabled = True
        cmbStatus.Location = New Point(976, 25)
        cmbStatus.Margin = New Padding(4, 5, 4, 5)
        cmbStatus.Name = "cmbStatus"
        cmbStatus.Size = New Size(410, 41)
        cmbStatus.TabIndex = 66
        ' 
        ' cmbProgram
        ' 
        cmbProgram.Font = New Font("Garamond", 14.25F, FontStyle.Bold)
        cmbProgram.ForeColor = Color.DarkSlateGray
        cmbProgram.FormattingEnabled = True
        cmbProgram.Location = New Point(525, 90)
        cmbProgram.Margin = New Padding(4, 5, 4, 5)
        cmbProgram.Name = "cmbProgram"
        cmbProgram.Size = New Size(386, 41)
        cmbProgram.TabIndex = 65
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.BackColor = Color.Transparent
        Label1.Font = New Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.DarkSlateGray
        Label1.Location = New Point(389, 46)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(0, 38)
        Label1.TabIndex = 62
        ' 
        ' btnExport
        ' 
        btnExport.BackColor = Color.Red
        btnExport.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnExport.ForeColor = Color.LavenderBlush
        btnExport.Location = New Point(1486, 15)
        btnExport.Margin = New Padding(4, 5, 4, 5)
        btnExport.Name = "btnExport"
        btnExport.Size = New Size(251, 59)
        btnExport.TabIndex = 60
        btnExport.Text = "Export"
        btnExport.UseVisualStyleBackColor = False
        ' 
        ' cmbYear
        ' 
        cmbYear.Font = New Font("Garamond", 14.25F, FontStyle.Bold)
        cmbYear.ForeColor = Color.DarkSlateGray
        cmbYear.FormattingEnabled = True
        cmbYear.Location = New Point(525, 25)
        cmbYear.Margin = New Padding(4, 5, 4, 5)
        cmbYear.Name = "cmbYear"
        cmbYear.Size = New Size(386, 41)
        cmbYear.TabIndex = 59
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(dgvStatistics)
        Panel1.Location = New Point(4, 160)
        Panel1.Margin = New Padding(4)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(1457, 940)
        Panel1.TabIndex = 29
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.WhiteSmoke
        Panel2.Controls.Add(Panel5)
        Panel2.Controls.Add(Panel4)
        Panel2.Controls.Add(Panel3)
        Panel2.Location = New Point(1468, 159)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(300, 613)
        Panel2.TabIndex = 30
        ' 
        ' Panel5
        ' 
        Panel5.BackColor = Color.White
        Panel5.Controls.Add(Label4)
        Panel5.Controls.Add(lblTotalDeficit)
        Panel5.Location = New Point(22, 413)
        Panel5.Name = "Panel5"
        Panel5.Size = New Size(262, 150)
        Panel5.TabIndex = 2
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.BackColor = Color.Transparent
        Label4.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label4.ForeColor = Color.Black
        Label4.Location = New Point(71, 12)
        Label4.Margin = New Padding(4, 0, 4, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(120, 25)
        Label4.TabIndex = 70
        Label4.Text = "Total Deficit:"
        ' 
        ' lblTotalDeficit
        ' 
        lblTotalDeficit.AutoSize = True
        lblTotalDeficit.BackColor = Color.Transparent
        lblTotalDeficit.Font = New Font("Segoe UI", 26F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblTotalDeficit.ForeColor = Color.Black
        lblTotalDeficit.Location = New Point(13, 53)
        lblTotalDeficit.Margin = New Padding(4, 0, 4, 0)
        lblTotalDeficit.Name = "lblTotalDeficit"
        lblTotalDeficit.Size = New Size(60, 70)
        lblTotalDeficit.TabIndex = 64
        lblTotalDeficit.Text = "0"
        ' 
        ' Panel4
        ' 
        Panel4.BackColor = Color.Black
        Panel4.Controls.Add(Label3)
        Panel4.Controls.Add(lblTotalCollected)
        Panel4.Location = New Point(22, 214)
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(262, 177)
        Panel4.TabIndex = 1
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.BackColor = Color.Transparent
        Label3.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label3.ForeColor = Color.White
        Label3.Location = New Point(61, 20)
        Label3.Margin = New Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(143, 25)
        Label3.TabIndex = 69
        Label3.Text = "Total Collected:"
        ' 
        ' lblTotalCollected
        ' 
        lblTotalCollected.AutoSize = True
        lblTotalCollected.BackColor = Color.Transparent
        lblTotalCollected.Font = New Font("Segoe UI", 26F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblTotalCollected.ForeColor = Color.White
        lblTotalCollected.Location = New Point(13, 71)
        lblTotalCollected.Margin = New Padding(4, 0, 4, 0)
        lblTotalCollected.Name = "lblTotalCollected"
        lblTotalCollected.Size = New Size(60, 70)
        lblTotalCollected.TabIndex = 63
        lblTotalCollected.Text = "0"
        ' 
        ' Panel3
        ' 
        Panel3.BackColor = Color.DarkSlateGray
        Panel3.Controls.Add(Label2)
        Panel3.Controls.Add(lblTotalExpected)
        Panel3.Location = New Point(21, 18)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(258, 177)
        Panel3.TabIndex = 0
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.BackColor = Color.Transparent
        Label2.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.ForeColor = Color.White
        Label2.Location = New Point(25, 15)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(216, 25)
        Label2.TabIndex = 68
        Label2.Text = "Total Amount Expected:"
        ' 
        ' lblTotalExpected
        ' 
        lblTotalExpected.AutoSize = True
        lblTotalExpected.BackColor = Color.Transparent
        lblTotalExpected.Font = New Font("Segoe UI", 26F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblTotalExpected.ForeColor = Color.White
        lblTotalExpected.Location = New Point(4, 66)
        lblTotalExpected.Margin = New Padding(4, 0, 4, 0)
        lblTotalExpected.Name = "lblTotalExpected"
        lblTotalExpected.Size = New Size(60, 70)
        lblTotalExpected.TabIndex = 61
        lblTotalExpected.Text = "0"
        ' 
        ' UC_Statistics
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(PanelBackground)
        Name = "UC_Statistics"
        Size = New Size(1771, 735)
        CType(dgvStatistics, ComponentModel.ISupportInitialize).EndInit()
        PanelBackground.ResumeLayout(False)
        PanelWithDgv.ResumeLayout(False)
        FlowLayoutPanel1.ResumeLayout(False)
        PanelWithSearch.ResumeLayout(False)
        PanelWithSearch.PerformLayout()
        Panel1.ResumeLayout(False)
        Panel2.ResumeLayout(False)
        Panel5.ResumeLayout(False)
        Panel5.PerformLayout()
        Panel4.ResumeLayout(False)
        Panel4.PerformLayout()
        Panel3.ResumeLayout(False)
        Panel3.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents dgvStatistics As DataGridView
    Friend WithEvents PanelBackground As Panel
    Friend WithEvents PanelWithDgv As Panel
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents PanelWithSearch As Panel
    Friend WithEvents btnExport As Button
    Friend WithEvents cmbYear As ComboBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents lblTotalExpected As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents lblTotalDeficit As Label
    Friend WithEvents lblTotalCollected As Label
    Friend WithEvents cmbStatus As ComboBox
    Friend WithEvents cmbProgram As ComboBox
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label5 As Label
    Friend WithEvents Panel5 As Panel

End Class
