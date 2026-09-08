Imports Microsoft.VisualBasic.Logging

Public Class SplashScreen


    'Array Collections of Quotes pop Ups on loading 
    Dim quotes() As String = {
        "To provide a centralized digital platform",
        "To enable students to view their current fee",
        "To automate the verification of tuition fee payment",
        "To improve the accuracy, transparency",
        "           please wait...",
        "           please wait...",
        "           please wait"
    }




    Dim quoteIndex As Integer = 0

    Private Sub SplashScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Change form bar name
        Me.Text = ""

        'Window State to Maximized
        ' Me.WindowState = FormWindowState.Maximized

        'Apply Circled hat picture
        CircleShape(PictureBox1, 300)


        'Always Remember that the Speed or progress oft the ProgressBar is always Controlled by the Timer Control, Do it From the UI
        'Mentioned the [Name] ProgressBar and set to start 
        ProgressBar1.Value = 10

        'Assigning ProgressBar motion counts to Lable [Name] called lblPercentage for display progress in percent
        lblPercentage.Text = ProgressBar1.Value.ToString() & "%"
        lblQuote.Text = quotes(quoteIndex)

        Timer1.Start()




    End Sub




    'This Handles the Timer Control Effects on Both the ProgressBar and the Quotes 
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ' Progress increment
        If ProgressBar1.Value < 100 Then
            ProgressBar1.Value += 1
            lblPercentage.Text = ProgressBar1.Value.ToString() & "%"

            ' Change quote every 10%
            If ProgressBar1.Value Mod 10 = 0 Then
                quoteIndex = (quoteIndex + 1) Mod quotes.Length
                lblQuote.Text = quotes(quoteIndex)
            End If

        Else
            Timer1.Stop()
            'Showing the Next Page
            Dim nextForm As New login()
            ' nextForm.WindowState = FormWindowState.Maximized ' or Maximized, as you prefer
            nextForm.Show()
            Me.Hide()

        End If
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class