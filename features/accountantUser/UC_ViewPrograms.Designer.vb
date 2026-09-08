<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UC_ViewPrograms
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
        PanelBackground = New Panel()
        PanelWithDgv = New Panel()
        FlowLayoutPanel1 = New FlowLayoutPanel()
        PanelWithSearch = New Panel()
        btnDelete = New Button()
        cmbProgram = New ComboBox()
        Panel1 = New Panel()
        dgvPrograms = New DataGridView()
        PanelBackground.SuspendLayout()
        PanelWithDgv.SuspendLayout()
        FlowLayoutPanel1.SuspendLayout()
        PanelWithSearch.SuspendLayout()
        Panel1.SuspendLayout()
        CType(dgvPrograms, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' PanelBackground
        ' 
        PanelBackground.Controls.Add(PanelWithDgv)
        PanelBackground.Dock = DockStyle.Fill
        PanelBackground.Location = New Point(0, 0)
        PanelBackground.Margin = New Padding(4)
        PanelBackground.Name = "PanelBackground"
        PanelBackground.Size = New Size(2256, 1028)
        PanelBackground.TabIndex = 0
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
        PanelWithDgv.Size = New Size(2256, 1028)
        PanelWithDgv.TabIndex = 4
        ' 
        ' FlowLayoutPanel1
        ' 
        FlowLayoutPanel1.Controls.Add(PanelWithSearch)
        FlowLayoutPanel1.Controls.Add(Panel1)
        FlowLayoutPanel1.Dock = DockStyle.Fill
        FlowLayoutPanel1.Location = New Point(0, 0)
        FlowLayoutPanel1.Margin = New Padding(4)
        FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        FlowLayoutPanel1.Size = New Size(2256, 1028)
        FlowLayoutPanel1.TabIndex = 0
        ' 
        ' PanelWithSearch
        ' 
        PanelWithSearch.BackColor = Color.White
        PanelWithSearch.Controls.Add(btnDelete)
        PanelWithSearch.Controls.Add(cmbProgram)
        PanelWithSearch.Dock = DockStyle.Top
        PanelWithSearch.Location = New Point(4, 5)
        PanelWithSearch.Margin = New Padding(4, 5, 4, 5)
        PanelWithSearch.Name = "PanelWithSearch"
        PanelWithSearch.Size = New Size(2256, 102)
        PanelWithSearch.TabIndex = 28
        ' 
        ' btnDelete
        ' 
        btnDelete.BackColor = Color.Red
        btnDelete.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnDelete.ForeColor = Color.LavenderBlush
        btnDelete.Location = New Point(1386, 25)
        btnDelete.Margin = New Padding(4, 5, 4, 5)
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(372, 59)
        btnDelete.TabIndex = 60
        btnDelete.Text = "DELETE"
        btnDelete.UseVisualStyleBackColor = False
        ' 
        ' cmbProgram
        ' 
        cmbProgram.Font = New Font("Garamond", 14.25F, FontStyle.Bold)
        cmbProgram.ForeColor = Color.DarkSlateGray
        cmbProgram.FormattingEnabled = True
        cmbProgram.Location = New Point(881, 25)
        cmbProgram.Margin = New Padding(4, 5, 4, 5)
        cmbProgram.Name = "cmbProgram"
        cmbProgram.Size = New Size(386, 41)
        cmbProgram.TabIndex = 59
        cmbProgram.Text = "--select program--"
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(dgvPrograms)
        Panel1.Dock = DockStyle.Bottom
        Panel1.Location = New Point(4, 116)
        Panel1.Margin = New Padding(4)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(2252, 940)
        Panel1.TabIndex = 29
        ' 
        ' dgvPrograms
        ' 
        dgvPrograms.BackgroundColor = Color.White
        dgvPrograms.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvPrograms.Location = New Point(4, 4)
        dgvPrograms.Margin = New Padding(4)
        dgvPrograms.Name = "dgvPrograms"
        dgvPrograms.RowHeadersWidth = 51
        dgvPrograms.Size = New Size(2054, 880)
        dgvPrograms.TabIndex = 0
        ' 
        ' UC_ViewPrograms
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(PanelBackground)
        Margin = New Padding(4)
        Name = "UC_ViewPrograms"
        Size = New Size(2256, 1028)
        PanelBackground.ResumeLayout(False)
        PanelWithDgv.ResumeLayout(False)
        FlowLayoutPanel1.ResumeLayout(False)
        PanelWithSearch.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        CType(dgvPrograms, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents PanelBackground As Panel
    Friend WithEvents PanelWithDgv As Panel
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents PanelWithSearch As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents dgvPrograms As DataGridView
    Friend WithEvents cmbProgram As ComboBox
    Friend WithEvents btnDelete As Button

End Class
