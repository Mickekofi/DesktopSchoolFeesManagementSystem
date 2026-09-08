Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Windows.Forms
Imports System.Drawing

Public Class UC_History

    ' Internal state
    Private currentStudentId As Integer = 0
    Private dtHistory As New DataTable()

    Private Sub UC_History_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' UI Polishing
        ShapeUtilities.RadiusButton(btnExport, 1.5F)
        DataGridViewHelper.ApplyBeautifulStyle(dgvTransactions)

        ' We do NOT hardcode the filters here anymore. 
        ' They will be built dynamically after we fetch the data.
        cmbFIlter.DropDownStyle = ComboBoxStyle.DropDownList

        LoadStudentIdAndData()
    End Sub

    ' ======================================================================
    ' 1. DATA ACQUISITION, RELATIONAL MAPPING & SMART FILTER GENERATION
    ' ======================================================================
    Private Sub LoadStudentIdAndData()
        Try
            Using conn As MySqlConnection = Database.CreateOpenConnection()
                ' First, resolve the student's ID from the active session
                Dim studentQuery As String = "SELECT id FROM students WHERE index_number = @Index"
                Using cmd As New MySqlCommand(studentQuery, conn)
                    cmd.Parameters.AddWithValue("@Index", Session.CurrentUsername)
                    Dim result = cmd.ExecuteScalar()

                    If result IsNot Nothing Then
                        currentStudentId = Convert.ToInt32(result)
                    Else
                        MessageBox.Show("Student profile authentication failed.", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        btnExport.Enabled = False
                        Return
                    End If
                End Using

                ' Second, pull the entire financial history and the target fees for EACH year
                Dim historyQuery As String = "
                    SELECT 
                        p.transaction_reference AS 'Receipt No.',
                        y.year_label AS 'Academic Year',
                        p.payment_method AS 'Method',
                        p.amount_paid AS 'Amount Paid (GHS)',
                        p.payment_date AS 'Date',
                        y.is_active AS 'IsActiveYear',
                        fs.fee_amount AS 'AnnualTarget',
                        y.id AS 'YearId'
                    FROM payments p
                    JOIN fee_structures fs ON p.fee_structure_id = fs.id
                    JOIN academic_years y ON fs.academic_year_id = y.id
                    WHERE p.student_id = @StuId AND p.payment_status = 'Completed'
                    ORDER BY y.id ASC, p.payment_date ASC"

                Using cmdHistory As New MySqlCommand(historyQuery, conn)
                    cmdHistory.Parameters.AddWithValue("@StuId", currentStudentId)
                    Using da As New MySqlDataAdapter(cmdHistory)
                        dtHistory.Clear()
                        da.Fill(dtHistory)
                    End Using
                End Using

                ' --- THE SEGREGATED MATHEMATICAL ENGINE ---
                dtHistory.Columns.Add("Amount Left (GHS)", GetType(Decimal))
                dtHistory.Columns.Add("Completion (%)", GetType(String))

                Dim runningBalances As New Dictionary(Of Integer, Decimal)
                Dim uniqueYears As New HashSet(Of String)()
                Dim uniqueMethods As New HashSet(Of String)()

                For Each row As DataRow In dtHistory.Rows
                    Dim yearId As Integer = Convert.ToInt32(row("YearId"))
                    Dim annualTarget As Decimal = Convert.ToDecimal(row("AnnualTarget"))
                    Dim paidAmount As Decimal = Convert.ToDecimal(row("Amount Paid (GHS)"))

                    ' Collect unique data points for our Smart Filters
                    uniqueYears.Add(row("Academic Year").ToString())
                    uniqueMethods.Add(row("Method").ToString())

                    If Not runningBalances.ContainsKey(yearId) Then
                        runningBalances(yearId) = annualTarget
                    End If

                    runningBalances(yearId) -= paidAmount
                    Dim currentLeft As Decimal = Math.Max(0, runningBalances(yearId))
                    row("Amount Left (GHS)") = currentLeft

                    Dim totalPaidSoFar As Decimal = annualTarget - currentLeft
                    Dim percentage As Decimal = If(annualTarget > 0, (totalPaidSoFar / annualTarget) * 100, 0)
                    row("Completion (%)") = $"{percentage:N1}%"
                Next

                ' Reverse sort the view so newest transactions appear on top
                dtHistory.DefaultView.Sort = "Date DESC"

                ' Bind to UI
                dgvTransactions.DataSource = dtHistory.DefaultView
                dgvTransactions.ReadOnly = True
                dgvTransactions.AllowUserToAddRows = False

                ' Hide operational columns
                If dgvTransactions.Columns.Contains("IsActiveYear") Then dgvTransactions.Columns("IsActiveYear").Visible = False
                If dgvTransactions.Columns.Contains("AnnualTarget") Then dgvTransactions.Columns("AnnualTarget").Visible = False
                If dgvTransactions.Columns.Contains("YearId") Then dgvTransactions.Columns("YearId").Visible = False

                ' Format financial columns
                If dgvTransactions.Columns.Contains("Amount Paid (GHS)") Then dgvTransactions.Columns("Amount Paid (GHS)").DefaultCellStyle.Format = "N2"
                If dgvTransactions.Columns.Contains("Amount Left (GHS)") Then dgvTransactions.Columns("Amount Left (GHS)").DefaultCellStyle.Format = "N2"

                ' --- BUILD THE SMART FILTERS DYNAMICALLY ---
                BuildSmartFilters(uniqueYears, uniqueMethods)

            End Using
        Catch ex As Exception
            MessageBox.Show($"Failed to retrieve transaction ledger: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ======================================================================
    ' 2. DYNAMIC UI GENERATION & IN-MEMORY STATE FILTERING
    ' ======================================================================
    Private Sub BuildSmartFilters(years As HashSet(Of String), methods As HashSet(Of String))
        ' Disconnect the handler temporarily so it doesn't fire while we build the list
        RemoveHandler cmbFIlter.SelectedIndexChanged, AddressOf cmbFIlter_SelectedIndexChanged

        cmbFIlter.Items.Clear()

        ' 1. Primary Filters
        cmbFIlter.Items.Add("All Transactions")
        cmbFIlter.Items.Add("Current Academic Year")

        ' 2. Specific Academic Year Filters (Generated from actual student data)
        If years.Count > 0 Then
            cmbFIlter.Items.Add("--- Filter by Year ---")
            For Each y In years.OrderByDescending(Function(val) val)
                cmbFIlter.Items.Add($"Year: {y}")
            Next
        End If

        ' 3. Specific Payment Method Filters (Generated from actual student data)
        If methods.Count > 0 Then
            cmbFIlter.Items.Add("--- Filter by Method ---")
            For Each m In methods
                cmbFIlter.Items.Add($"Method: {m}")
            Next
        End If

        cmbFIlter.SelectedIndex = 0

        ' Reconnect the handler
        AddHandler cmbFIlter.SelectedIndexChanged, AddressOf cmbFIlter_SelectedIndexChanged
    End Sub

    Private Sub cmbFIlter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFIlter.SelectedIndexChanged
        If dtHistory Is Nothing OrElse dtHistory.Rows.Count = 0 Then Return

        Dim filterSelection As String = cmbFIlter.SelectedItem.ToString()

        ' Ignore unclickable category headers
        If filterSelection.StartsWith("---") Then Return

        Select Case filterSelection
            Case "All Transactions"
                dtHistory.DefaultView.RowFilter = ""
            Case "Current Academic Year"
                dtHistory.DefaultView.RowFilter = "IsActiveYear = 1"
            Case Else
                ' Handle the dynamically generated filters
                If filterSelection.StartsWith("Year: ") Then
                    Dim targetYear As String = filterSelection.Replace("Year: ", "")
                    dtHistory.DefaultView.RowFilter = $"[Academic Year] = '{targetYear.Replace("'", "''")}'"

                ElseIf filterSelection.StartsWith("Method: ") Then
                    Dim targetMethod As String = filterSelection.Replace("Method: ", "")
                    dtHistory.DefaultView.RowFilter = $"[Method] = '{targetMethod.Replace("'", "''")}'"
                End If
        End Select
    End Sub

    ' ======================================================================
    ' 3. LATE-BOUND EXCEL EXPORT ENGINE
    ' ======================================================================
    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        If dgvTransactions.Rows.Count = 0 Then
            MessageBox.Show("There are no transactions to export.", "Empty Ledger", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        btnExport.Enabled = False
        btnExport.Text = "Generating..."
        Application.DoEvents()

        Try
            Dim excelApp As Object = CreateObject("Excel.Application")
            If excelApp Is Nothing Then Throw New Exception("Microsoft Excel is not installed on this machine.")

            Dim workbook As Object = excelApp.Workbooks.Add()
            Dim worksheet As Object = workbook.Sheets(1)
            worksheet.Name = "Transaction History"

            Dim colIndex As Integer = 1
            For Each col As DataGridViewColumn In dgvTransactions.Columns
                If col.Visible Then
                    worksheet.Cells(1, colIndex) = col.HeaderText
                    worksheet.Cells(1, colIndex).Font.Bold = True
                    worksheet.Cells(1, colIndex).Interior.Color = ColorTranslator.ToOle(Color.FromArgb(41, 128, 185))
                    worksheet.Cells(1, colIndex).Font.Color = ColorTranslator.ToOle(Color.White)
                    colIndex += 1
                End If
            Next

            Dim rowIndex As Integer = 2
            For Each row As DataGridViewRow In dgvTransactions.Rows
                If Not row.IsNewRow Then
                    colIndex = 1
                    For Each col As DataGridViewColumn In dgvTransactions.Columns
                        If col.Visible Then
                            Dim cellValue As String = If(row.Cells(col.Index).Value IsNot Nothing, row.Cells(col.Index).Value.ToString(), "")
                            worksheet.Cells(rowIndex, colIndex) = cellValue
                            colIndex += 1
                        End If
                    Next
                    rowIndex += 1
                End If
            Next

            Dim dataRange As Object = worksheet.Range(worksheet.Cells(1, 1), worksheet.Cells(rowIndex - 1, colIndex - 1))
            dataRange.Borders.LineStyle = 1
            dataRange.Columns.AutoFit()

            excelApp.Visible = True

        Catch ex As Exception
            MessageBox.Show($"Export Failed. This usually occurs if Microsoft Excel is not properly installed on this computer." & vbCrLf & vbCrLf & $"Technical Details: {ex.Message}", "Architecture Limitation", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            btnExport.Enabled = True
            btnExport.Text = "Export to Excel"
        End Try
    End Sub

    Private Sub dgvTransactions_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvTransactions.CellContentClick
    End Sub

End Class