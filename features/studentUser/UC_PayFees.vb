Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Text.RegularExpressions
Imports System.IO

' --- PDFSHARP IMPORTS ---
Imports PdfSharp.Pdf
Imports PdfSharp.Drawing

Public Class UC_PayFees
    ' Initialize ErrorProvider for live validations
    Private errProvider As New ErrorProvider()

    ' Internal state variables
    Private currentStudentId As Integer = 0
    Private studentProgramId As Integer = 0
    Private activeFeeStructureId As Integer = 0
    Private activeYearId As Integer = 0
    Private feeAmount As Decimal = 0D
    Private totalPaid As Decimal = 0D
    Private amountOwed As Decimal = 0D
    Private studentPhone As String = ""

    Private Structure ReceiptInfo
        Public TransactionRef As String
        Public ReceiptNumber As String
        Public AmountPaid As Decimal
        Public TotalPaidSoFar As Decimal
        Public AnnualTarget As Decimal
        Public PaymentDate As DateTime
    End Structure

    Private Sub UC_PayFees_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' --- INJECT THE OS FONT RESOLVER ---
        ' This must run once per application instance to prevent crashes
        If PdfSharp.Fonts.GlobalFontSettings.FontResolver Is Nothing Then
            PdfSharp.Fonts.GlobalFontSettings.FontResolver = New WindowsFontResolver()
        End If

        ' UI Enhancements
        ShapeUtilities.RadiusButton(btnPayFees, 1.5F)
        DataGridViewHelper.ApplyBeautifulStyle(dgvTransactions)

        LoadFinancialState()
        LoadTransactionHistory()

        ' UI Enhancements
        ShapeUtilities.RadiusButton(btnPayFees, 1.5F)
        DataGridViewHelper.ApplyBeautifulStyle(dgvTransactions)

        LoadFinancialState()
        LoadTransactionHistory()
    End Sub

    ' ======================================================================
    ' FINANCIAL STATE CALCULATIONS
    ' ======================================================================
    Private Sub LoadFinancialState()
        Try
            Using conn As MySqlConnection = Database.CreateOpenConnection()
                ' 1. Fetch Student ID, Phone, and Program ID
                Dim studentQuery As String = "SELECT id, phone, program_id FROM students WHERE index_number = @Index"
                Using cmd As New MySqlCommand(studentQuery, conn)
                    cmd.Parameters.AddWithValue("@Index", Session.CurrentUsername)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            currentStudentId = Convert.ToInt32(reader("id"))
                            studentPhone = reader("phone").ToString()
                            studentProgramId = If(IsDBNull(reader("program_id")), 0, Convert.ToInt32(reader("program_id")))
                        Else
                            MessageBox.Show("Student profile not found.", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            btnPayFees.Enabled = False
                            Return
                        End If
                    End Using
                End Using

                If studentProgramId = 0 Then
                    MessageBox.Show("You are not assigned to an academic program. Contact Administration.", "Registration Blocked", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    btnPayFees.Enabled = False
                    Return
                End If

                ' 2. Resolve the Active Academic Year and Extract the Annual Fee Target
                Dim feeQuery As String = "
                    SELECT fs.id, fs.academic_year_id, fs.fee_amount
                    FROM fee_structures fs
                    JOIN academic_years y ON fs.academic_year_id = y.id
                    JOIN semesters s ON s.academic_year_id = y.id
                    WHERE fs.program_id = @ProgId AND y.is_active = 1 AND s.is_active = 1
                    LIMIT 1"

                Using cmdFee As New MySqlCommand(feeQuery, conn)
                    cmdFee.Parameters.AddWithValue("@ProgId", studentProgramId)
                    Using reader As MySqlDataReader = cmdFee.ExecuteReader()
                        If reader.Read() Then
                            activeFeeStructureId = Convert.ToInt32(reader("id"))
                            activeYearId = Convert.ToInt32(reader("academic_year_id"))
                            feeAmount = Convert.ToDecimal(reader("fee_amount"))
                        Else
                            MessageBox.Show("No active fee structure found for your program this academic year. Payment gateway locked.", "System Locked", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            btnPayFees.Enabled = False
                            Return
                        End If
                    End Using
                End Using

                ' 3. Calculate Total Valid Payments for the ENTIRE Academic Year
                Dim sumQuery As String = "
                    SELECT SUM(p.amount_paid) 
                    FROM payments p
                    JOIN fee_structures fs ON p.fee_structure_id = fs.id
                    WHERE p.student_id = @StuId AND fs.academic_year_id = @YearId AND p.payment_status = 'Completed'"
                Using cmdSum As New MySqlCommand(sumQuery, conn)
                    cmdSum.Parameters.AddWithValue("@StuId", currentStudentId)
                    cmdSum.Parameters.AddWithValue("@YearId", activeYearId)
                    Dim pResult = cmdSum.ExecuteScalar()
                    totalPaid = If(IsDBNull(pResult), 0D, Convert.ToDecimal(pResult))
                End Using

                ' 4. Calculate Owed and Update UI
                amountOwed = feeAmount - totalPaid
                If amountOwed < 0 Then amountOwed = 0

                lblAmountOwned.Text = $"GHS {amountOwed:N2}"
                lblAmountOwned.ForeColor = If(amountOwed > 0, Color.DarkRed, Color.DarkGreen)

                ' 5. Fetch the single most recent payment
                Dim recentQuery As String = "SELECT amount_paid FROM payments WHERE student_id = @StuId AND payment_status = 'Completed' ORDER BY payment_date DESC LIMIT 1"
                Using cmdRecent As New MySqlCommand(recentQuery, conn)
                    cmdRecent.Parameters.AddWithValue("@StuId", currentStudentId)
                    Dim recentResult = cmdRecent.ExecuteScalar()
                    If recentResult IsNot Nothing AndAlso Not IsDBNull(recentResult) Then
                        lblRecently.Text = $"GHS {Convert.ToDecimal(recentResult):N2}"
                    Else
                        lblRecently.Text = "GHS 0.00"
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"Failed to load financial state: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ======================================================================
    ' TRANSACTION GRID POPULATION
    ' ======================================================================
    Private Sub LoadTransactionHistory()
        If activeYearId = 0 Then Return

        Dim query As String = "
            SELECT 
                p.transaction_reference AS 'Receipt No.',
                p.payment_method AS 'Payment System',
                y.year_label AS 'Academic Year',
                p.amount_paid AS 'Amount Paid (GHS)',
                p.payment_date AS 'Date'
            FROM payments p
            JOIN fee_structures fs ON p.fee_structure_id = fs.id
            JOIN academic_years y ON fs.academic_year_id = y.id
            WHERE p.student_id = @StuId AND fs.academic_year_id = @YearId AND p.payment_status = 'Completed'
            ORDER BY p.payment_date ASC"

        Dim dt As New DataTable()
        Try
            Using conn As MySqlConnection = Database.CreateOpenConnection()
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@StuId", currentStudentId)
                    cmd.Parameters.AddWithValue("@YearId", activeYearId)
                    Using da As New MySqlDataAdapter(cmd)
                        da.Fill(dt)
                    End Using
                End Using
            End Using

            dt.Columns.Add("Amount Left (GHS)", GetType(Decimal))

            Dim runningBalance As Decimal = feeAmount

            For i As Integer = 0 To dt.Rows.Count - 1
                runningBalance -= Convert.ToDecimal(dt.Rows(i)("Amount Paid (GHS)"))
                dt.Rows(i)("Amount Left (GHS)") = Math.Max(0, runningBalance)
            Next

            dt.DefaultView.Sort = "Date DESC"
            dgvTransactions.DataSource = dt.DefaultView
            dgvTransactions.ReadOnly = True
            dgvTransactions.AllowUserToAddRows = False

            If dgvTransactions.Columns.Contains("Amount Paid (GHS)") Then dgvTransactions.Columns("Amount Paid (GHS)").DefaultCellStyle.Format = "N2"
            If dgvTransactions.Columns.Contains("Amount Left (GHS)") Then dgvTransactions.Columns("Amount Left (GHS)").DefaultCellStyle.Format = "N2"

        Catch ex As Exception
            MessageBox.Show($"Failed to load transaction history: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ======================================================================
    ' PAYMENT DIALOG & LEDGER INJECTION
    ' ======================================================================
    Private Sub btnPayFees_Click(sender As Object, e As EventArgs) Handles btnPayFees.Click
        If activeFeeStructureId = 0 Then
            MessageBox.Show("System cannot authorize payments without an active fee structure.", "System Locked", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If amountOwed <= 0 Then
            MessageBox.Show("Your fees are fully paid for this academic year. No further payments are required.", "Balance Cleared", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Using payForm As New Form()
            payForm.Text = "TransFlow: Secure School Fees Payment"
            payForm.Size = New Size(400, 380)
            payForm.StartPosition = FormStartPosition.CenterParent
            payForm.FormBorderStyle = FormBorderStyle.FixedDialog
            payForm.MaximizeBox = False
            payForm.MinimizeBox = False
            payForm.BackColor = Color.WhiteSmoke

            ' Header
            Dim lblTitle As New Label() With {.Text = "Mobile Money Gateway", .Font = New Font("Segoe UI", 12, FontStyle.Bold), .Location = New Point(20, 20), .AutoSize = True}

            ' Phone Number Input
            Dim lblPhone As New Label() With {.Text = "Mobile Number:", .Location = New Point(20, 60), .AutoSize = True}
            Dim txtPhone As New TextBox() With {.Text = studentPhone, .Location = New Point(20, 80), .Width = 340, .Font = New Font("Segoe UI", 11)}

            ' Auto-Detected Network Display
            Dim lblNetworkLabel As New Label() With {.Text = "Detected Network:", .Location = New Point(20, 120), .AutoSize = True}
            Dim lblNetworkValue As New Label() With {.Text = "Unknown", .Font = New Font("Segoe UI", 10, FontStyle.Bold), .ForeColor = Color.Gray, .Location = New Point(140, 120), .AutoSize = True}

            ' Amount Input
            Dim lblAmount As New Label() With {.Text = $"Amount to Pay (Max: GHS {amountOwed:N2}):", .Location = New Point(20, 160), .AutoSize = True}
            Dim txtAmount As New TextBox() With {.Location = New Point(20, 180), .Width = 340, .Font = New Font("Segoe UI", 11)}

            Dim btnSubmitPay As New Button() With {.Text = "Authorize Payment", .Location = New Point(20, 250), .Width = 340, .Height = 45, .BackColor = Color.SeaGreen, .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat, .Font = New Font("Segoe UI", 10, FontStyle.Bold)}

            ' --- Telco Auto-Detection Logic ---
            Dim DetectNetwork = Sub()
                                    Dim num = txtPhone.Text.Trim()
                                    If num.Length >= 3 Then
                                        Dim prefix = num.Substring(0, 3)
                                        Select Case prefix
                                            Case "024", "054", "055", "059", "025", "053"
                                                lblNetworkValue.Text = "MTN Mobile Money"
                                                lblNetworkValue.ForeColor = Color.Orange
                                            Case "020", "050"
                                                lblNetworkValue.Text = "Telecel Cash"
                                                lblNetworkValue.ForeColor = Color.Red
                                            Case "027", "057", "026", "056"
                                                lblNetworkValue.Text = "AT Money"
                                                lblNetworkValue.ForeColor = Color.Blue
                                            Case Else
                                                lblNetworkValue.Text = "Invalid Provider"
                                                lblNetworkValue.ForeColor = Color.Gray
                                        End Select
                                    Else
                                        lblNetworkValue.Text = "Waiting for input..."
                                        lblNetworkValue.ForeColor = Color.Gray
                                    End If
                                End Sub

            AddHandler txtPhone.TextChanged, Sub(s, ev) DetectNetwork()
            DetectNetwork()

            ' --- Transaction Submission Logic ---
            AddHandler btnSubmitPay.Click, Sub(s, ev)
                                               Dim payAmount As Decimal = 0D
                                               Dim phonePattern = "^(02|05)\d{8}$"

                                               If Not Regex.IsMatch(txtPhone.Text.Trim(), phonePattern) Then
                                                   MessageBox.Show("Invalid phone number format.", "Payment Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                   Return
                                               End If

                                               If lblNetworkValue.Text = "Invalid Provider" Or lblNetworkValue.Text = "Waiting for input..." Then
                                                   MessageBox.Show("Unsupported Mobile Network.", "Payment Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                   Return
                                               End If

                                               If Not Decimal.TryParse(txtAmount.Text.Trim(), payAmount) OrElse payAmount <= 0 Then
                                                   MessageBox.Show("Please enter a valid monetary amount.", "Payment Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                   Return
                                               End If

                                               If payAmount > amountOwed Then
                                                   MessageBox.Show($"You cannot overpay. Maximum allowable amount is GHS {amountOwed:N2}.", "Payment Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                   Return
                                               End If

                                               btnSubmitPay.Enabled = False
                                               Try
                                                   Dim transactionRef As String = "TXN" & DateTime.Now.ToString("yyyyMMddHHmmss") & (New Random().Next(100, 999)).ToString()
                                                   Dim receiptNo As String = "REC" & DateTime.Now.ToString("yyyyMMddHHmmss")
                                                   Dim methodName As String = "MoMo"

                                                   Using conn As MySqlConnection = Database.CreateOpenConnection()
                                                       Using trans = conn.BeginTransaction()
                                                           Try
                                                               ' 1. Insert Payment
                                                               Dim insertPayQuery As String = "INSERT INTO payments (student_id, fee_structure_id, amount_paid, payment_method, transaction_reference, payment_status, payment_date) VALUES (@StuId, @FeeStructId, @Amount, @Method, @Ref, 'Completed', NOW())"
                                                               Using cmd As New MySqlCommand(insertPayQuery, conn, trans)
                                                                   cmd.Parameters.AddWithValue("@StuId", currentStudentId)
                                                                   cmd.Parameters.AddWithValue("@FeeStructId", activeFeeStructureId)
                                                                   cmd.Parameters.AddWithValue("@Amount", payAmount)
                                                                   cmd.Parameters.AddWithValue("@Method", methodName)
                                                                   cmd.Parameters.AddWithValue("@Ref", transactionRef)
                                                                   cmd.ExecuteNonQuery()
                                                               End Using

                                                               ' 2. Retrieve the inserted Payment ID
                                                               Dim paymentId As Integer = 0
                                                               Using cmdId As New MySqlCommand("SELECT LAST_INSERT_ID()", conn, trans)
                                                                   paymentId = Convert.ToInt32(cmdId.ExecuteScalar())
                                                               End Using

                                                               ' 3. Insert the Physical Receipt Record (Crucial for Auditing)
                                                               Dim insertRecQuery As String = "INSERT INTO receipts (receipt_number, payment_id, student_name, index_number, amount) VALUES (@RecNo, @PayId, @SName, @Index, @Amount)"
                                                               Using cmdRec As New MySqlCommand(insertRecQuery, conn, trans)
                                                                   cmdRec.Parameters.AddWithValue("@RecNo", receiptNo)
                                                                   cmdRec.Parameters.AddWithValue("@PayId", paymentId)
                                                                   cmdRec.Parameters.AddWithValue("@SName", Session.CurrentFullName)
                                                                   cmdRec.Parameters.AddWithValue("@Index", Session.CurrentUsername)
                                                                   cmdRec.Parameters.AddWithValue("@Amount", payAmount)
                                                                   cmdRec.ExecuteNonQuery()
                                                               End Using

                                                               trans.Commit()
                                                           Catch exTrans As Exception
                                                               trans.Rollback()
                                                               Throw New Exception("Ledger transaction failed: " & exTrans.Message)
                                                           End Try
                                                       End Using
                                                   End Using

                                                   ' 4. Generate the beautiful PDF Receipt instantly
                                                   Dim rData As New ReceiptInfo With {
                                                       .TransactionRef = transactionRef,
                                                       .ReceiptNumber = receiptNo,
                                                       .AmountPaid = payAmount,
                                                       .TotalPaidSoFar = totalPaid + payAmount,
                                                       .AnnualTarget = feeAmount,
                                                       .PaymentDate = DateTime.Now
                                                   }

                                                   GeneratePdfReceipt(rData)

                                                   MessageBox.Show($"Payment of GHS {payAmount:N2} was successful." & vbCrLf & $"Receipt saved successfully.", "Transaction Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                   payForm.DialogResult = DialogResult.OK

                                               Catch ex As Exception
                                                   MessageBox.Show($"System Error: {ex.Message}", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                   btnSubmitPay.Enabled = True
                                               End Try
                                           End Sub

            payForm.Controls.AddRange(New Control() {lblTitle, lblPhone, txtPhone, lblNetworkLabel, lblNetworkValue, lblAmount, txtAmount, btnSubmitPay})

            If payForm.ShowDialog() = DialogResult.OK Then
                LoadFinancialState()
                LoadTransactionHistory()
            End If
        End Using
    End Sub

    ' ======================================================================
    ' RECEIPT ENGINE (PDFSHARP DIRECT TO FILE)
    ' ======================================================================
    Private Sub GeneratePdfReceipt(data As ReceiptInfo)
        ' 1. Let the user choose where to save the beautiful PDF
        Dim sfd As New SaveFileDialog()
        sfd.Filter = "PDF Document (*.pdf)|*.pdf"
        sfd.FileName = $"Receipt_{data.ReceiptNumber}.pdf"
        sfd.Title = "Save Official Receipt"

        If sfd.ShowDialog() <> DialogResult.OK Then Return

        ' 2. Initialize PdfSharp Document
        Dim doc As New PdfDocument()
        doc.Info.Title = "Official Fees Receipt"
        Dim page As PdfPage = doc.AddPage()

        ' Set dimensions (Standard A4) using the fully qualified PdfSharp namespace
        page.Size = PdfSharp.PageSize.A4
        Dim g As XGraphics = XGraphics.FromPdfPage(page)

        ' 3. Typography Configuration
        ' PdfSharp 6.x uses XFontStyleEx instead of XFontStyle
        Dim fontHeader As New XFont("Arial", 22, XFontStyleEx.Bold)
        Dim fontSubHeader As New XFont("Arial", 14, XFontStyleEx.Bold)
        Dim fontNormal As New XFont("Arial", 11, XFontStyleEx.Regular)
        Dim fontBold As New XFont("Arial", 11, XFontStyleEx.Bold)
        Dim fontMassive As New XFont("Arial", 36, XFontStyleEx.Bold)
        Dim brushDark As XBrush = XBrushes.Black
        Dim brushGray As XBrush = XBrushes.DimGray
        Dim penLine As New XPen(XColors.LightGray, 1)

        Dim startX As Double = 50
        Dim startY As Double = 50
        Dim offset As Double = 0

        ' 4. Draw Header
        g.DrawString("UNIVERSITY OF EDUCATION, WINNEBA", fontHeader, brushDark, startX, startY)
        offset += 40
        g.DrawString("OFFICIAL FEES PAYMENT RECEIPT", fontSubHeader, brushDark, startX, startY + offset)
        offset += 30
        g.DrawLine(penLine, startX, startY + offset, page.Width - 50, startY + offset)
        offset += 20

        ' 5. Draw Metadata
        g.DrawString($"Receipt No: {data.ReceiptNumber}", fontNormal, brushDark, startX, startY + offset)
        g.DrawString($"Date: {data.PaymentDate.ToString("dd MMM yyyy, hh:mm tt")}", fontNormal, brushDark, page.Width - 250, startY + offset)
        offset += 20
        g.DrawString($"Transaction Ref: {data.TransactionRef}", fontNormal, brushDark, startX, startY + offset)
        offset += 30
        g.DrawLine(penLine, startX, startY + offset, page.Width - 50, startY + offset)
        offset += 20

        ' 6. Draw Student Identity
        g.DrawString("STUDENT DETAILS", fontSubHeader, brushGray, startX, startY + offset)
        offset += 30
        g.DrawString($"Name: {Session.CurrentFullName}", fontBold, brushDark, startX, startY + offset)
        offset += 20
        g.DrawString($"Index Number: {Session.CurrentUsername}", fontNormal, brushDark, startX, startY + offset)
        offset += 40

        ' 7. The Massive Amount Section
        g.DrawString("AMOUNT PAID", fontSubHeader, brushGray, startX, startY + offset)
        offset += 40
        g.DrawString($"GHS {data.AmountPaid:N2}", fontMassive, brushDark, startX, startY + offset)
        offset += 60

        ' 8. Financial Progress Metrics
        Dim percentage As Decimal = If(data.AnnualTarget > 0, (data.TotalPaidSoFar / data.AnnualTarget) * 100, 0)

        g.DrawLine(penLine, startX, startY + offset, page.Width - 50, startY + offset)
        offset += 20
        g.DrawString("ANNUAL FEE STATUS", fontSubHeader, brushGray, startX, startY + offset)
        offset += 30
        g.DrawString($"Annual Target Fee: GHS {data.AnnualTarget:N2}", fontNormal, brushDark, startX, startY + offset)
        offset += 20
        g.DrawString($"Total Paid to Date: GHS {data.TotalPaidSoFar:N2}", fontBold, brushDark, startX, startY + offset)
        offset += 20
        g.DrawString($"Overall Completion: {percentage:N1}%", fontBold, brushDark, startX, startY + offset)
        offset += 50

        ' 9. Security Footer
        g.DrawString("This is a system-generated receipt and requires no physical signature.", fontNormal, brushGray, startX, startY + offset)

        ' 10. Save and Clean up
        doc.Save(sfd.FileName)
        doc.Close()

        ' Optionally open the PDF immediately for the user to view
        Try
            Process.Start(New ProcessStartInfo(sfd.FileName) With {.UseShellExecute = True})
        Catch ex As Exception
            ' Ignore if no PDF viewer is installed on the machine
        End Try
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
    End Sub

    Private Sub dgvTransactions_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvTransactions.CellContentClick
    End Sub
End Class


' ======================================================================
' OS FONT RESOLVER BRIDGE FOR PDFSHARP 6.X
' ======================================================================
' ======================================================================
' OS FONT RESOLVER BRIDGE FOR PDFSHARP 6.X
' ======================================================================
Public Class WindowsFontResolver
    Implements PdfSharp.Fonts.IFontResolver

    ' Note: DefaultFontName was removed in PdfSharp 6.x. Do not add it back.

    Public Function ResolveTypeface(familyName As String, isBold As Boolean, isItalic As Boolean) As PdfSharp.Fonts.FontResolverInfo Implements PdfSharp.Fonts.IFontResolver.ResolveTypeface
        ' Map the requested font style to the exact physical Windows font filename
        Dim faceName As String = "arial"
        If isBold AndAlso isItalic Then
            faceName = "arialbi"
        ElseIf isBold Then
            faceName = "arialbd"
        ElseIf isItalic Then
            faceName = "ariali"
        End If

        Return New PdfSharp.Fonts.FontResolverInfo(faceName)
    End Function

    Public Function GetFont(faceName As String) As Byte() Implements PdfSharp.Fonts.IFontResolver.GetFont
        ' Dig into the C:\Windows\Fonts directory to extract the raw byte array
        Dim fontPath As String = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), faceName & ".ttf")

        If System.IO.File.Exists(fontPath) Then
            Return System.IO.File.ReadAllBytes(fontPath)
        End If

        ' Ultimate fallback if the specific style is missing
        Return System.IO.File.ReadAllBytes(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf"))
    End Function
End Class


