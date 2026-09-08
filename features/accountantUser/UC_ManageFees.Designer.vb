<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UC_ManageFees
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
        dgvFeeStructures = New DataGridView()
        PanelBackground = New Panel()
        PanelWithDgv = New Panel()
        FlowLayoutPanel1 = New FlowLayoutPanel()
        PanelWithSearch = New Panel()
        lblActiveTerm = New Label()
        btnGenerateFees = New Button()
        Panel1 = New Panel()
        CType(dgvFeeStructures, ComponentModel.ISupportInitialize).BeginInit()
        PanelBackground.SuspendLayout()
        PanelWithDgv.SuspendLayout()
        FlowLayoutPanel1.SuspendLayout()
        PanelWithSearch.SuspendLayout()
        Panel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' dgvFeeStructures
        ' 
        dgvFeeStructures.BackgroundColor = Color.White
        dgvFeeStructures.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvFeeStructures.Dock = DockStyle.Fill
        dgvFeeStructures.Location = New Point(0, 0)
        dgvFeeStructures.Margin = New Padding(4)
        dgvFeeStructures.Name = "dgvFeeStructures"
        dgvFeeStructures.RowHeadersWidth = 51
        dgvFeeStructures.Size = New Size(2252, 940)
        dgvFeeStructures.TabIndex = 0
        ' 
        ' PanelBackground
        ' 
        PanelBackground.Controls.Add(PanelWithDgv)
        PanelBackground.Dock = DockStyle.Fill
        PanelBackground.Location = New Point(0, 0)
        PanelBackground.Margin = New Padding(4)
        PanelBackground.Name = "PanelBackground"
        PanelBackground.Size = New Size(2285, 1036)
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
        PanelWithDgv.Size = New Size(2285, 1036)
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
        FlowLayoutPanel1.Size = New Size(2285, 1036)
        FlowLayoutPanel1.TabIndex = 0
        ' 
        ' PanelWithSearch
        ' 
        PanelWithSearch.BackColor = Color.White
        PanelWithSearch.Controls.Add(lblActiveTerm)
        PanelWithSearch.Controls.Add(btnGenerateFees)
        PanelWithSearch.Dock = DockStyle.Top
        PanelWithSearch.Location = New Point(4, 5)
        PanelWithSearch.Margin = New Padding(4, 5, 4, 5)
        PanelWithSearch.Name = "PanelWithSearch"
        PanelWithSearch.Size = New Size(2256, 102)
        PanelWithSearch.TabIndex = 28
        ' 
        ' lblActiveTerm
        ' 
        lblActiveTerm.AutoSize = True
        lblActiveTerm.BackColor = Color.Transparent
        lblActiveTerm.Font = New Font("Garamond", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblActiveTerm.ForeColor = Color.DarkRed
        lblActiveTerm.Location = New Point(638, 39)
        lblActiveTerm.Margin = New Padding(4, 0, 4, 0)
        lblActiveTerm.Name = "lblActiveTerm"
        lblActiveTerm.Size = New Size(265, 31)
        lblActiveTerm.TabIndex = 61
        lblActiveTerm.Text = "Administrator Portal "
        lblActiveTerm.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' btnGenerateFees
        ' 
        btnGenerateFees.BackColor = Color.Red
        btnGenerateFees.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnGenerateFees.ForeColor = Color.LavenderBlush
        btnGenerateFees.Location = New Point(1378, 25)
        btnGenerateFees.Margin = New Padding(4, 5, 4, 5)
        btnGenerateFees.Name = "btnGenerateFees"
        btnGenerateFees.Size = New Size(372, 59)
        btnGenerateFees.TabIndex = 60
        btnGenerateFees.Text = "ACTIVATE"
        btnGenerateFees.UseVisualStyleBackColor = False
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(dgvFeeStructures)
        Panel1.Dock = DockStyle.Bottom
        Panel1.Location = New Point(4, 116)
        Panel1.Margin = New Padding(4)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(2252, 940)
        Panel1.TabIndex = 29
        ' 
        ' UC_ManageFees
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(PanelBackground)
        Margin = New Padding(4)
        Name = "UC_ManageFees"
        Size = New Size(2285, 1036)
        CType(dgvFeeStructures, ComponentModel.ISupportInitialize).EndInit()
        PanelBackground.ResumeLayout(False)
        PanelWithDgv.ResumeLayout(False)
        FlowLayoutPanel1.ResumeLayout(False)
        PanelWithSearch.ResumeLayout(False)
        PanelWithSearch.PerformLayout()
        Panel1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents dgvFeeStructures As DataGridView
    Friend WithEvents PanelBackground As Panel
    Friend WithEvents PanelWithDgv As Panel
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents PanelWithSearch As Panel
    Friend WithEvents btnGenerateFees As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents lblActiveTerm As Label

End Class
