Option Strict On
Option Explicit On

Imports System.Drawing
Imports System.Windows.Forms
Imports System.Collections.Generic

Module NavButtonStyles

    ' Explicitly use System.Windows.Forms.Form to prevent namespace collision
    Private ButtonGroups As New Dictionary(Of System.Windows.Forms.Form, List(Of Button))
    Private ActiveButtons As New Dictionary(Of System.Windows.Forms.Form, Button)

    ' Call this function from your Form's Load event
    Public Sub InitializeNavButtons(parentForm As System.Windows.Forms.Form, buttons As List(Of Button), Optional defaultActive As Button = Nothing)
        ButtonGroups(parentForm) = buttons

        For Each btn As Button In buttons
            ' Reset default styling
            SetNormalStyle(btn)

            ' Ensure we don't duplicate handlers
            RemoveHandler btn.Click, AddressOf Button_Click
            RemoveHandler btn.MouseEnter, AddressOf Button_MouseEnter
            RemoveHandler btn.MouseLeave, AddressOf Button_MouseLeave
            RemoveHandler btn.MouseDown, AddressOf Button_MouseDown

            ' Attach events
            AddHandler btn.Click, AddressOf Button_Click
            AddHandler btn.MouseEnter, AddressOf Button_MouseEnter
            AddHandler btn.MouseLeave, AddressOf Button_MouseLeave
            AddHandler btn.MouseDown, AddressOf Button_MouseDown
        Next

        ' Set the initial active button if one was provided
        If defaultActive IsNot Nothing Then
            SetActive(parentForm, defaultActive)
        End If

        ' Clean up memory when the form is closed
        AddHandler parentForm.FormClosed, Sub(s As Object, e As FormClosedEventArgs)
                                              ButtonGroups.Remove(parentForm)
                                              ActiveButtons.Remove(parentForm)
                                          End Sub
    End Sub

    Private Sub SetNormalStyle(btn As Button)
        btn.BackColor = Color.White
        btn.ForeColor = Color.Black
        btn.FlatStyle = FlatStyle.Flat
        btn.FlatAppearance.BorderSize = 0
        btn.Cursor = Cursors.Hand
    End Sub

    Private Sub SetActive(parentForm As System.Windows.Forms.Form, btn As Button)
        ' Reset previous active button
        If ActiveButtons.ContainsKey(parentForm) AndAlso ActiveButtons(parentForm) IsNot Nothing Then
            SetNormalStyle(ActiveButtons(parentForm))
        End If

        ' Apply active style
        ActiveButtons(parentForm) = btn
        btn.BackColor = Color.DarkRed
        btn.ForeColor = Color.White
        btn.FlatAppearance.BorderSize = 3
        btn.FlatAppearance.BorderColor = Color.White
    End Sub

    ' --- Helper function to find the form a button belongs to safely ---
    Private Function GetParentForm(btn As Button) As System.Windows.Forms.Form
        For Each frm As System.Windows.Forms.Form In ButtonGroups.Keys
            If ButtonGroups(frm).Contains(btn) Then
                Return frm
            End If
        Next
        Return Nothing
    End Function

    ' --- Event Handlers ---

    Private Sub Button_Click(sender As Object, e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        Dim parentForm As System.Windows.Forms.Form = GetParentForm(btn)
        If parentForm IsNot Nothing Then SetActive(parentForm, btn)
    End Sub

    Private Sub Button_MouseEnter(sender As Object, e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        Dim parentForm As System.Windows.Forms.Form = GetParentForm(btn)

        ' Only apply hover effect if it's NOT the active button
        If parentForm IsNot Nothing AndAlso (Not ActiveButtons.ContainsKey(parentForm) OrElse ActiveButtons(parentForm) IsNot btn) Then
            btn.BackColor = Color.Red
            btn.ForeColor = Color.WhiteSmoke
        End If
    End Sub

    Private Sub Button_MouseLeave(sender As Object, e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        Dim parentForm As System.Windows.Forms.Form = GetParentForm(btn)

        ' Reset to normal only if it's NOT the active button
        If parentForm IsNot Nothing AndAlso (Not ActiveButtons.ContainsKey(parentForm) OrElse ActiveButtons(parentForm) IsNot btn) Then
            SetNormalStyle(btn)
        End If
    End Sub

    Private Sub Button_MouseDown(sender As Object, e As MouseEventArgs)
        Dim btn As Button = CType(sender, Button)
        btn.BackColor = Color.Indigo
    End Sub





    ''''How To Use 
    '''List the buttons you want to style in a List(Of Button) and call InitializeNavButtons from your form's Load event. For example:
    '''
    'Dim navButtons As New List(Of Button) From {
    '        btnHide,
    '        btnLogin
    '}

    '    ' Just pass Me directly now!
    '    NavButtonStyles.InitializeNavButtons(Me, navButtons, btnHide)
















End Module
