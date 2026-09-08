<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SplashScreen
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SplashScreen))
        Panel1 = New Panel()
        FlowLayoutPanel1 = New FlowLayoutPanel()
        lblPercentage = New Label()
        ProgressBar1 = New ProgressBar()
        Label1 = New Label()
        lblQuote = New Label()
        PictureBox1 = New PictureBox()
        Timer1 = New Timer(components)
        Panel1.SuspendLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Panel1.BackColor = Color.DarkSlateGray
        Panel1.Controls.Add(FlowLayoutPanel1)
        Panel1.Controls.Add(lblPercentage)
        Panel1.Controls.Add(ProgressBar1)
        Panel1.Controls.Add(Label1)
        Panel1.Controls.Add(lblQuote)
        Panel1.Controls.Add(PictureBox1)
        Panel1.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(3))
        Panel1.Location = New Point(-4, -142)
        Panel1.Margin = New Padding(4, 5, 4, 5)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(1081, 885)
        Panel1.TabIndex = 2
        ' 
        ' FlowLayoutPanel1
        ' 
        FlowLayoutPanel1.Location = New Point(2129, 366)
        FlowLayoutPanel1.Margin = New Padding(4)
        FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        FlowLayoutPanel1.Size = New Size(10, 274)
        FlowLayoutPanel1.TabIndex = 6
        ' 
        ' lblPercentage
        ' 
        lblPercentage.AutoSize = True
        lblPercentage.Font = New Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point, CByte(3))
        lblPercentage.ForeColor = Color.Snow
        lblPercentage.Location = New Point(938, 796)
        lblPercentage.Margin = New Padding(4, 0, 4, 0)
        lblPercentage.Name = "lblPercentage"
        lblPercentage.Size = New Size(61, 41)
        lblPercentage.TabIndex = 4
        lblPercentage.Text = "0%"
        ' 
        ' ProgressBar1
        ' 
        ProgressBar1.ForeColor = Color.MediumSeaGreen
        ProgressBar1.Location = New Point(194, 805)
        ProgressBar1.Margin = New Padding(4, 5, 4, 5)
        ProgressBar1.Minimum = 10
        ProgressBar1.Name = "ProgressBar1"
        ProgressBar1.Size = New Size(808, 24)
        ProgressBar1.Style = ProgressBarStyle.Continuous
        ProgressBar1.TabIndex = 3
        ProgressBar1.Value = 10
        ' 
        ' Label1
        ' 
        Label1.BackColor = Color.Transparent
        Label1.CausesValidation = False
        Label1.Font = New Font("Bahnschrift Condensed", 20F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.White
        Label1.Location = New Point(-137, 688)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(1423, 75)
        Label1.TabIndex = 1
        Label1.Text = "School Fees Management System(Efe)"
        Label1.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' lblQuote
        ' 
        lblQuote.AutoSize = True
        lblQuote.Font = New Font("Bahnschrift SemiCondensed", 10F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblQuote.ForeColor = Color.Snow
        lblQuote.Location = New Point(461, 763)
        lblQuote.Margin = New Padding(4, 0, 4, 0)
        lblQuote.Name = "lblQuote"
        lblQuote.Size = New Size(235, 24)
        lblQuote.TabIndex = 5
        lblQuote.Text = "UEW LOCAL EDITION 1.0 BETA"
        ' 
        ' PictureBox1
        ' 
        PictureBox1.BackColor = Color.Transparent
        PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), Image)
        PictureBox1.Location = New Point(283, 109)
        PictureBox1.Margin = New Padding(4, 5, 4, 5)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(583, 634)
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox1.TabIndex = 2
        PictureBox1.TabStop = False
        ' 
        ' Timer1
        ' 
        ' 
        ' SplashScreen
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        AutoSize = True
        AutoValidate = AutoValidate.EnablePreventFocusChange
        ClientSize = New Size(1079, 738)
        ControlBox = False
        Controls.Add(Panel1)
        Margin = New Padding(4)
        Name = "SplashScreen"
        StartPosition = FormStartPosition.CenterScreen
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents lblQuote As Label
    Friend WithEvents lblPercentage As Label
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Timer1 As Timer
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
End Class
