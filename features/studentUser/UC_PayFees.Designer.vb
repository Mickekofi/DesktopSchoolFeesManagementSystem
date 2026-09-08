<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UC_PayFees
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
        Label10 = New Label()
        Label9 = New Label()
        Panel7 = New Panel()
        lblRecently = New Label()
        PictureBox2 = New PictureBox()
        PictureBox1 = New PictureBox()
        Panel3 = New Panel()
        lblAmountOwned = New Label()
        Panel2 = New Panel()
        PanelWithPayment = New Panel()
        PanelwithDgv = New Panel()
        dgvTransactions = New DataGridView()
        Label3 = New Label()
        Panel4 = New Panel()
        btnPayFees = New Button()
        Panel6 = New Panel()
        Panel8 = New Panel()
        Panel1.SuspendLayout()
        Panel7.SuspendLayout()
        CType(PictureBox2, ComponentModel.ISupportInitialize).BeginInit()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        Panel3.SuspendLayout()
        PanelWithPayment.SuspendLayout()
        PanelwithDgv.SuspendLayout()
        CType(dgvTransactions, ComponentModel.ISupportInitialize).BeginInit()
        Panel4.SuspendLayout()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.AutoScroll = True
        Panel1.BackColor = Color.WhiteSmoke
        Panel1.Controls.Add(Label10)
        Panel1.Controls.Add(Label9)
        Panel1.Controls.Add(Panel7)
        Panel1.Controls.Add(PictureBox2)
        Panel1.Controls.Add(PictureBox1)
        Panel1.Controls.Add(Panel3)
        Panel1.Controls.Add(Panel2)
        Panel1.Controls.Add(PanelWithPayment)
        Panel1.Controls.Add(Label3)
        Panel1.Controls.Add(Panel4)
        Panel1.Controls.Add(Panel6)
        Panel1.Controls.Add(Panel8)
        Panel1.Dock = DockStyle.Fill
        Panel1.Location = New Point(0, 0)
        Panel1.Margin = New Padding(4, 4, 4, 4)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(2255, 1015)
        Panel1.TabIndex = 0
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.BackColor = Color.Transparent
        Label10.Font = New Font("MS UI Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label10.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        Label10.Location = New Point(144, 19)
        Label10.Margin = New Padding(4, 0, 4, 0)
        Label10.Name = "Label10"
        Label10.Size = New Size(204, 24)
        Label10.TabIndex = 28
        Label10.Text = "Pay Directly With:"
        Label10.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.BackColor = Color.Transparent
        Label9.Font = New Font("MS UI Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label9.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        Label9.Location = New Point(179, 384)
        Label9.Margin = New Padding(4, 0, 4, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(257, 24)
        Label9.TabIndex = 27
        Label9.Text = "Recently Paid Amount:"
        Label9.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Panel7
        ' 
        Panel7.BackColor = Color.White
        Panel7.Controls.Add(lblRecently)
        Panel7.Location = New Point(449, 384)
        Panel7.Margin = New Padding(4, 4, 4, 4)
        Panel7.Name = "Panel7"
        Panel7.Size = New Size(448, 76)
        Panel7.TabIndex = 25
        ' 
        ' lblRecently
        ' 
        lblRecently.AutoSize = True
        lblRecently.BackColor = Color.Transparent
        lblRecently.Font = New Font("Garamond", 25.8000011F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblRecently.ForeColor = Color.SeaGreen
        lblRecently.Location = New Point(14, 14)
        lblRecently.Margin = New Padding(4, 0, 4, 0)
        lblRecently.Name = "lblRecently"
        lblRecently.Size = New Size(97, 59)
        lblRecently.TabIndex = 17
        lblRecently.Text = "350"
        lblRecently.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' PictureBox2
        ' 
        PictureBox2.Image = My.Resources.Resources.MTN
        PictureBox2.Location = New Point(428, 59)
        PictureBox2.Margin = New Padding(4, 4, 4, 4)
        PictureBox2.Name = "PictureBox2"
        PictureBox2.Size = New Size(231, 240)
        PictureBox2.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox2.TabIndex = 23
        PictureBox2.TabStop = False
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Image = My.Resources.Resources.telecel
        PictureBox1.Location = New Point(144, 59)
        PictureBox1.Margin = New Padding(4, 4, 4, 4)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(239, 240)
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox1.TabIndex = 22
        PictureBox1.TabStop = False
        ' 
        ' Panel3
        ' 
        Panel3.BackColor = Color.White
        Panel3.Controls.Add(lblAmountOwned)
        Panel3.Location = New Point(1066, 130)
        Panel3.Margin = New Padding(4, 4, 4, 4)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(570, 138)
        Panel3.TabIndex = 18
        ' 
        ' lblAmountOwned
        ' 
        lblAmountOwned.AutoSize = True
        lblAmountOwned.BackColor = Color.Transparent
        lblAmountOwned.Font = New Font("Garamond", 36F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblAmountOwned.ForeColor = Color.Red
        lblAmountOwned.Location = New Point(4, 30)
        lblAmountOwned.Margin = New Padding(4, 0, 4, 0)
        lblAmountOwned.Name = "lblAmountOwned"
        lblAmountOwned.Size = New Size(137, 81)
        lblAmountOwned.TabIndex = 17
        lblAmountOwned.Text = "350"
        lblAmountOwned.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.DarkSlateGray
        Panel2.Location = New Point(1045, 112)
        Panel2.Margin = New Padding(4, 4, 4, 4)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(308, 69)
        Panel2.TabIndex = 1
        ' 
        ' PanelWithPayment
        ' 
        PanelWithPayment.AutoScroll = True
        PanelWithPayment.Controls.Add(PanelwithDgv)
        PanelWithPayment.Dock = DockStyle.Bottom
        PanelWithPayment.Location = New Point(0, 508)
        PanelWithPayment.Margin = New Padding(4, 4, 4, 4)
        PanelWithPayment.Name = "PanelWithPayment"
        PanelWithPayment.Size = New Size(2229, 601)
        PanelWithPayment.TabIndex = 21
        ' 
        ' PanelwithDgv
        ' 
        PanelwithDgv.BackColor = Color.White
        PanelwithDgv.Controls.Add(dgvTransactions)
        PanelwithDgv.Location = New Point(4, 4)
        PanelwithDgv.Margin = New Padding(4, 4, 4, 4)
        PanelwithDgv.Name = "PanelwithDgv"
        PanelwithDgv.Size = New Size(2225, 631)
        PanelwithDgv.TabIndex = 23
        ' 
        ' dgvTransactions
        ' 
        dgvTransactions.BackgroundColor = Color.WhiteSmoke
        dgvTransactions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvTransactions.Location = New Point(0, 28)
        dgvTransactions.Margin = New Padding(4, 4, 4, 4)
        dgvTransactions.Name = "dgvTransactions"
        dgvTransactions.RowHeadersWidth = 51
        dgvTransactions.Size = New Size(2225, 600)
        dgvTransactions.TabIndex = 0
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.BackColor = Color.Transparent
        Label3.Font = New Font("MS UI Gothic", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label3.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        Label3.Location = New Point(1102, 30)
        Label3.Margin = New Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(314, 28)
        Label3.TabIndex = 19
        Label3.Text = "Amounted Fees Owned "
        Label3.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Panel4
        ' 
        Panel4.BackColor = Color.White
        Panel4.Controls.Add(btnPayFees)
        Panel4.Location = New Point(940, 19)
        Panel4.Margin = New Padding(4, 4, 4, 4)
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(700, 489)
        Panel4.TabIndex = 20
        ' 
        ' btnPayFees
        ' 
        btnPayFees.BackColor = Color.Red
        btnPayFees.Font = New Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnPayFees.ForeColor = Color.White
        btnPayFees.Location = New Point(118, 388)
        btnPayFees.Margin = New Padding(4, 5, 4, 5)
        btnPayFees.Name = "btnPayFees"
        btnPayFees.Size = New Size(441, 74)
        btnPayFees.TabIndex = 37
        btnPayFees.Text = "Pay School Fees"
        btnPayFees.UseVisualStyleBackColor = False
        ' 
        ' Panel6
        ' 
        Panel6.BackColor = Color.DarkSlateGray
        Panel6.Location = New Point(108, 138)
        Panel6.Margin = New Padding(4, 4, 4, 4)
        Panel6.Name = "Panel6"
        Panel6.Size = New Size(618, 60)
        Panel6.TabIndex = 24
        ' 
        ' Panel8
        ' 
        Panel8.BackColor = Color.DarkSlateGray
        Panel8.Location = New Point(409, 416)
        Panel8.Margin = New Padding(4, 4, 4, 4)
        Panel8.Name = "Panel8"
        Panel8.Size = New Size(162, 64)
        Panel8.TabIndex = 26
        ' 
        ' UC_PayFees
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(Panel1)
        Margin = New Padding(4, 4, 4, 4)
        Name = "UC_PayFees"
        Size = New Size(2255, 1015)
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        Panel7.ResumeLayout(False)
        Panel7.PerformLayout()
        CType(PictureBox2, ComponentModel.ISupportInitialize).EndInit()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        Panel3.ResumeLayout(False)
        Panel3.PerformLayout()
        PanelWithPayment.ResumeLayout(False)
        PanelwithDgv.ResumeLayout(False)
        CType(dgvTransactions, ComponentModel.ISupportInitialize).EndInit()
        Panel4.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents lblAmountOwned As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Panel4 As Panel
    Friend WithEvents btnPayFees As Button
    Friend WithEvents PanelWithPayment As Panel
    Friend WithEvents PanelwithDgv As Panel
    Friend WithEvents dgvTransactions As DataGridView
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents lblRecently As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label

End Class
