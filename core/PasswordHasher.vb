Imports System.Security.Cryptography
Imports System.Text

Public Class PasswordHasher

    'On load, set the form to be centered and fixed size
    Private Sub PasswordHasher_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
    End Sub


    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        Dim plainPassword As String = txtPassword.Text.Trim()

        If String.IsNullOrEmpty(plainPassword) Then
            MessageBox.Show("Enter a password to hash.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Compute hash
        Dim hashedPassword As String = HashPassword(plainPassword)

        ' Copy to clipboard
        Clipboard.SetText(hashedPassword)

        ' Show dialog with hash
        MessageBox.Show("Hashed password copied to clipboard:" & vbCrLf & vbCrLf & hashedPassword,
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Function HashPassword(password As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim bytes As Byte() = Encoding.UTF8.GetBytes(password)
            Dim hashBytes As Byte() = sha256.ComputeHash(bytes)
            Return BitConverter.ToString(hashBytes).Replace("-", "").ToLower()
        End Using
    End Function

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Panel3_Paint(sender As Object, e As PaintEventArgs) Handles Panel3.Paint

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub
End Class
