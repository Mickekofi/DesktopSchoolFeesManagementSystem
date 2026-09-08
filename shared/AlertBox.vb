Option Strict On
Option Explicit On

Imports System.Drawing
Imports System.Windows.Forms

Public Module AlertBox

    Public Enum AlertType
        Success
        Warning
    End Enum

    ' Because this is in a module, this Sub is globally available!
    Public Sub Show(message As String, title As String, type As AlertType)
        ' 1. Create the Form
        Dim frm As New Register()
        frm.FormBorderStyle = FormBorderStyle.None
        frm.StartPosition = FormStartPosition.CenterScreen
        frm.Size = New Size(450, 200)
        frm.TopMost = True

        ' 2. Create the Top Title Bar
        Dim pnlTitle As New Panel() With {
            .Height = 40,
            .Dock = DockStyle.Top
        }

        Dim lblTitle As New Label() With {
            .Text = "  " & title,
            .Font = New Font("Segoe UI", 12, FontStyle.Bold),
            .ForeColor = Color.White,
            .Dock = DockStyle.Fill,
            .TextAlign = ContentAlignment.MiddleLeft
        }
        pnlTitle.Controls.Add(lblTitle)

        ' 3. Create the Bottom Panel for the Button
        Dim pnlBottom As New Panel() With {
            .Height = 60,
            .Dock = DockStyle.Bottom
        }

        Dim btnOK As New Button() With {
            .Text = "OK",
            .Size = New Size(120, 40),
            .Location = New Point(frm.Width - 140, 10),
            .FlatStyle = FlatStyle.Flat,
            .Cursor = Cursors.Hand,
            .Font = New Font("Segoe UI", 10, FontStyle.Bold),
            .ForeColor = Color.White
        }
        btnOK.FlatAppearance.BorderSize = 0
        pnlBottom.Controls.Add(btnOK)

        ' 4. Create the Message Label
        Dim lblMessage As New Label() With {
            .Text = message,
            .Font = New Font("Segoe UI", 11, FontStyle.Regular),
            .Dock = DockStyle.Fill,
            .TextAlign = ContentAlignment.MiddleCenter,
            .Padding = New Padding(20)
        }

        ' 5. Apply "Scary" or "Appealing" Themes
        If type = AlertType.Warning Then
            ' SCARY WARNING (Dark Theme with Blood Red)
            frm.BackColor = Color.FromArgb(30, 30, 30) ' Dark gray
            lblMessage.ForeColor = Color.LightCoral
            pnlTitle.BackColor = Color.DarkRed
            pnlBottom.BackColor = Color.FromArgb(20, 20, 20)
            btnOK.BackColor = Color.Crimson
        Else
            ' BEAUTIFUL SUCCESS (Clean Light Theme with calming Green)
            frm.BackColor = Color.White
            lblMessage.ForeColor = Color.DarkSlateGray
            pnlTitle.BackColor = Color.MediumSeaGreen
            pnlBottom.BackColor = Color.WhiteSmoke
            btnOK.BackColor = Color.SeaGreen
        End If

        ' Add controls to Form (Order matters for Docking)
        frm.Controls.Add(lblMessage)
        frm.Controls.Add(pnlTitle)
        frm.Controls.Add(pnlBottom)

        ' Close form when OK is clicked
        AddHandler btnOK.Click, Sub(sender As Object, e As EventArgs)
                                    frm.Close()
                                End Sub

        ' Add a border effect
        AddHandler frm.Paint, Sub(sender As Object, e As PaintEventArgs)
                                  Dim penColor As Color = If(type = AlertType.Warning, Color.DarkRed, Color.MediumSeaGreen)
                                  ControlPaint.DrawBorder(e.Graphics, frm.ClientRectangle, penColor, ButtonBorderStyle.Solid)
                              End Sub

        ' Show the popup
        frm.ShowDialog()
    End Sub

End Module




'HOW TO USE
'Call it from ANY Screen
'Since it Is now a Module, you can trigger it from literally any form, button, Or Event In your entire application by simply typing
'For a Scary Error

'    AlertBox.Show("CRITICAL ERROR: Invalid input detected!", "ACCESS DENIED", AlertBox.AlertType.Warning)

'For a Beautiful Success
'    AlertBox.Show("Student registered successfully.", "SUCCESS", AlertBox.AlertType.Success)
