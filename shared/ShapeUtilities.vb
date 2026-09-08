Imports System.Drawing
Imports System.Windows.Forms
Module ShapeUtilities

    ' Function to Circle a Shape(either Picture Box or Any) with two Parameters (Control[Name], size)
    Public Sub CircleShape(ctrl As Control, circleness As Single)
        If circleness <= 0 Then Exit Sub

        Dim radius As Integer
        Dim diameter As Integer = Math.Min(ctrl.Width, ctrl.Height)

        ' Calculate rounding
        If circleness >= 1 Then
            radius = diameter \ 2
        Else
            radius = CInt((diameter \ 2) * circleness)
        End If

        ' Create rounded region
        Dim path As New Drawing2D.GraphicsPath()
        path.StartFigure()
        path.AddArc(0, 0, radius * 2, radius * 2, 180, 90)
        path.AddArc(ctrl.Width - (radius * 2), 0, radius * 2, radius * 2, 270, 90)
        path.AddArc(ctrl.Width - (radius * 2), ctrl.Height - (radius * 2), radius * 2, radius * 2, 0, 90)
        path.AddArc(0, ctrl.Height - (radius * 2), radius * 2, radius * 2, 90, 90)
        path.CloseFigure()

        ctrl.Region = New Region(path)
    End Sub

    'How to use this function
    'Call the function in the Form Load Event or any other event where you want to apply the radius effect to a control. For example:
    ' CircleShape(PictureBox1, 1.0F) ' This will make the PictureBox fully circular if it's a square PictureBox.


    '===============================================================================

    ' Function To Send Buttons to Radius Shapes 

    'Function For Radius Button (Control[Name], size)
    Public Sub RadiusButton(btn As Button, circleness As Single)
        If circleness <= 0 Then Exit Sub
        Dim radius As Integer
        Dim diameter As Integer = Math.Min(btn.Width, btn.Height)
        ' Calculate rounding
        If circleness >= 1 Then
            radius = diameter \ 2
        Else
            radius = CInt((diameter \ 2) * circleness)
        End If
        ' Create rounded region
        Dim path As New Drawing2D.GraphicsPath()
        path.StartFigure()
        path.AddArc(0, 0, radius * 2, radius * 2, 180, 90)
        path.AddArc(btn.Width - (radius * 2), 0, radius * 2, radius * 2, 270, 90)
        path.AddArc(btn.Width - (radius * 2), btn.Height - (radius * 2), radius * 2, radius * 2, 0, 90)
        path.AddArc(0, btn.Height - (radius * 2), radius * 2, radius * 2, 90, 90)
        path.CloseFigure()
        btn.Region = New Region(path)
    End Sub


    '''HOW TO USE THIS FUNCTION
    '''Call the function in the Form Load Event or any other event where you want to apply the radius effect to a button. For example:
    ' RadiusButton(btnLogin, 1.0F) ' This will make the button fully circular if it's a square button.



    'Ended the Radius Function....................................................................................................








End Module


