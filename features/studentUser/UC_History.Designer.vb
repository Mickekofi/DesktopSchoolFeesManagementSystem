<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UC_History
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
        dgvTransactions = New DataGridView()
        Panel1 = New Panel()
        PanelWithDgv = New Panel()
        PanelWithCrudButtons = New Panel()
        Label2 = New Label()
        PanelWithSearch = New Panel()
        btnExport = New Button()
        cmbFIlter = New ComboBox()
        Label10 = New Label()
        CType(dgvTransactions, ComponentModel.ISupportInitialize).BeginInit()
        Panel1.SuspendLayout()
        PanelWithDgv.SuspendLayout()
        PanelWithCrudButtons.SuspendLayout()
        PanelWithSearch.SuspendLayout()
        SuspendLayout()
        ' 
        ' dgvTransactions
        ' 
        dgvTransactions.BackgroundColor = Color.White
        dgvTransactions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvTransactions.Location = New Point(0, 181)
        dgvTransactions.Margin = New Padding(4, 5, 4, 5)
        dgvTransactions.Name = "dgvTransactions"
        dgvTransactions.RowHeadersWidth = 51
        dgvTransactions.Size = New Size(1675, 901)
        dgvTransactions.TabIndex = 28
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
        Panel1.Size = New Size(2250, 1035)
        Panel1.TabIndex = 6
        ' 
        ' PanelWithDgv
        ' 
        PanelWithDgv.AutoScroll = True
        PanelWithDgv.BackColor = Color.White
        PanelWithDgv.Controls.Add(PanelWithCrudButtons)
        PanelWithDgv.Controls.Add(dgvTransactions)
        PanelWithDgv.Controls.Add(PanelWithSearch)
        PanelWithDgv.Dock = DockStyle.Fill
        PanelWithDgv.Location = New Point(0, 0)
        PanelWithDgv.Margin = New Padding(4, 5, 4, 5)
        PanelWithDgv.Name = "PanelWithDgv"
        PanelWithDgv.Size = New Size(2250, 1035)
        PanelWithDgv.TabIndex = 2
        ' 
        ' PanelWithCrudButtons
        ' 
        PanelWithCrudButtons.BackColor = Color.WhiteSmoke
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
        Label2.Location = New Point(36, 0)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(698, 61)
        Label2.TabIndex = 22
        Label2.Text = "School Fees Transaction History"
        ' 
        ' PanelWithSearch
        ' 
        PanelWithSearch.BackColor = Color.WhiteSmoke
        PanelWithSearch.Controls.Add(btnExport)
        PanelWithSearch.Controls.Add(cmbFIlter)
        PanelWithSearch.Controls.Add(Label10)
        PanelWithSearch.Location = New Point(0, 76)
        PanelWithSearch.Margin = New Padding(4, 5, 4, 5)
        PanelWithSearch.Name = "PanelWithSearch"
        PanelWithSearch.Size = New Size(2272, 110)
        PanelWithSearch.TabIndex = 27
        ' 
        ' btnExport
        ' 
        btnExport.BackColor = Color.Red
        btnExport.Font = New Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnExport.ForeColor = Color.White
        btnExport.Location = New Point(1257, 13)
        btnExport.Margin = New Padding(4, 5, 4, 5)
        btnExport.Name = "btnExport"
        btnExport.Size = New Size(342, 74)
        btnExport.TabIndex = 38
        btnExport.Text = "Export Transactions"
        btnExport.UseVisualStyleBackColor = False
        ' 
        ' cmbFIlter
        ' 
        cmbFIlter.BackColor = SystemColors.Info
        cmbFIlter.Font = New Font("Garamond", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        cmbFIlter.ForeColor = Color.Red
        cmbFIlter.FormattingEnabled = True
        cmbFIlter.Location = New Point(178, 28)
        cmbFIlter.Margin = New Padding(4, 5, 4, 5)
        cmbFIlter.Name = "cmbFIlter"
        cmbFIlter.Size = New Size(600, 44)
        cmbFIlter.TabIndex = 27
        cmbFIlter.Text = "01/22/2026"
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.BackColor = Color.Transparent
        Label10.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label10.ForeColor = Color.Black
        Label10.Location = New Point(92, 32)
        Label10.Margin = New Padding(4, 0, 4, 0)
        Label10.Name = "Label10"
        Label10.Size = New Size(75, 33)
        Label10.TabIndex = 18
        Label10.Text = "Date"
        ' 
        ' UC_History
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(Panel1)
        Margin = New Padding(4)
        Name = "UC_History"
        Size = New Size(2250, 1035)
        CType(dgvTransactions, ComponentModel.ISupportInitialize).EndInit()
        Panel1.ResumeLayout(False)
        PanelWithDgv.ResumeLayout(False)
        PanelWithCrudButtons.ResumeLayout(False)
        PanelWithCrudButtons.PerformLayout()
        PanelWithSearch.ResumeLayout(False)
        PanelWithSearch.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents dgvTransactions As DataGridView
    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelWithDgv As Panel
    Friend WithEvents PanelWithCrudButtons As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents PanelWithSearch As Panel
    Friend WithEvents cmbFIlter As ComboBox
    Friend WithEvents Label10 As Label
    Friend WithEvents btnExport As Button

End Class
