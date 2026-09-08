Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Windows.Forms
Imports System.Drawing
Imports System.IO

Public Class UC_Statistics

    Private dtMasterStats As New DataTable()
    Private isInitializing As Boolean = True

    Private Sub UC_Statistics_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' UI Polishing
        ShapeUtilities.RadiusButton(btnExport, 1.5F)
        DataGridViewHelper.ApplyBeautifulStyle(dgvStatistics)

        ' Make the grid rows tall enough to actually see a passport photo
        dgvStatistics.RowTemplate.Height = 60

        ' Set up status dropdown
        cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList
        cmbStatus.Items.AddRange(New String() {"All Students", "Fully Cleared (100%)", "In Debt (< 100%)", "Zero Paid (0%)"})
        cmbStatus.SelectedIndex = 0

        LoadMasterData()
        isInitializing = False
        ApplySmartFilters() ' Trigger initial KPI calculation
    End Sub

    ' ======================================================================
    ' 1. MASSIVE DATA EXTRACTION & MEMORY-SAFE IMAGE LOADING
    ' ======================================================================
    ' ======================================================================
    ' 1. MASSIVE DATA EXTRACTION & MEMORY-SAFE IMAGE LOADING
    ' ======================================================================
    Private Sub LoadMasterData()
        Try
            Using conn As MySqlConnection = Database.CreateOpenConnection()
                Dim query As String = "
                    SELECT 
                        s.passport_photo_path AS 'PhotoPath',
                        s.index_number AS 'Index No.',
                        s.full_name AS 'Student Name',
                        p.program_name AS 'Program',
                        y.year_label AS 'Academic Year',
                        fs.fee_amount AS 'TargetFeeRaw',
                        COALESCE((SELECT SUM(amount_paid) FROM payments pay WHERE pay.student_id = s.id AND pay.fee_structure_id = fs.id AND pay.payment_status = 'Completed'), 0) AS 'TotalPaidRaw',
                        y.is_active AS 'IsActiveYear'
                    FROM students s
                    JOIN programs p ON s.program_id = p.id
                    JOIN fee_structures fs ON fs.program_id = p.id
                    JOIN academic_years y ON fs.academic_year_id = y.id
                    ORDER BY s.full_name ASC"

                Using cmd As New MySqlCommand(query, conn)
                    Using da As New MySqlDataAdapter(cmd)
                        dtMasterStats.Clear()
                        da.Fill(dtMasterStats)
                    End Using
                End Using

                ' --- Inject Computed Columns for the UI ---
                ' CRITICAL FIX: Changed GetType(Image) to GetType(Byte()). 
                ' This prevents the GDI+ stream crash.
                dtMasterStats.Columns.Add("Photo", GetType(Byte()))
                dtMasterStats.Columns.Add("Target Fee", GetType(Decimal))
                dtMasterStats.Columns.Add("Amount Paid", GetType(Decimal))
                dtMasterStats.Columns.Add("Deficit", GetType(Decimal))
                dtMasterStats.Columns.Add("Completion %", GetType(Decimal))

                Dim uniqueYears As New HashSet(Of String)()
                Dim uniquePrograms As New HashSet(Of String)()
                Dim activeYearLabel As String = ""

                For Each row As DataRow In dtMasterStats.Rows
                    ' 1. Financial Math
                    Dim target As Decimal = Convert.ToDecimal(row("TargetFeeRaw"))
                    Dim paid As Decimal = Convert.ToDecimal(row("TotalPaidRaw"))
                    Dim deficit As Decimal = Math.Max(0, target - paid)
                    Dim percentage As Decimal = If(target > 0, (paid / target) * 100, 0)

                    row("Target Fee") = target
                    row("Amount Paid") = paid
                    row("Deficit") = deficit
                    row("Completion %") = percentage

                    ' 2. Filter Generation
                    uniqueYears.Add(row("Academic Year").ToString())
                    uniquePrograms.Add(row("Program").ToString())

                    If Convert.ToInt32(row("IsActiveYear")) = 1 Then
                        activeYearLabel = row("Academic Year").ToString()
                    End If

                    ' 3. Bulletproof Image Loading (Byte Array Direct Injection)
                    Dim photoPathStr As String = If(IsDBNull(row("PhotoPath")), "", row("PhotoPath").ToString())
                    If Not String.IsNullOrWhiteSpace(photoPathStr) AndAlso File.Exists(photoPathStr) Then
                        Try
                            row("Photo") = File.ReadAllBytes(photoPathStr)
                        Catch ex As Exception
                            row("Photo") = DBNull.Value ' Protect against locked files
                        End Try
                    Else
                        row("Photo") = DBNull.Value
                    End If
                Next

                ' Configure the Grid View
                dgvStatistics.DataSource = dtMasterStats.DefaultView
                dgvStatistics.ReadOnly = True
                dgvStatistics.AllowUserToAddRows = False

                ' Hide operational raw data from the screen
                If dgvStatistics.Columns.Contains("PhotoPath") Then dgvStatistics.Columns("PhotoPath").Visible = False
                If dgvStatistics.Columns.Contains("TargetFeeRaw") Then dgvStatistics.Columns("TargetFeeRaw").Visible = False
                If dgvStatistics.Columns.Contains("TotalPaidRaw") Then dgvStatistics.Columns("TotalPaidRaw").Visible = False
                If dgvStatistics.Columns.Contains("IsActiveYear") Then dgvStatistics.Columns("IsActiveYear").Visible = False

                ' Format the Photo Column
                If dgvStatistics.Columns.Contains("Photo") Then
                    Dim imgCol As DataGridViewImageColumn = CType(dgvStatistics.Columns("Photo"), DataGridViewImageColumn)
                    imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom
                    imgCol.Width = 60
                    ' Stop the grid from rendering an ugly red X if the student has no photo
                    Dim emptyBitmap As New Bitmap(1, 1)
                    imgCol.DefaultCellStyle.NullValue = emptyBitmap
                    imgCol.DisplayIndex = 0 ' Force photo to the left
                End If

                ' Format monetary and percentage columns
                If dgvStatistics.Columns.Contains("Target Fee") Then dgvStatistics.Columns("Target Fee").DefaultCellStyle.Format = "N2"
                If dgvStatistics.Columns.Contains("Amount Paid") Then dgvStatistics.Columns("Amount Paid").DefaultCellStyle.Format = "N2"
                If dgvStatistics.Columns.Contains("Deficit") Then dgvStatistics.Columns("Deficit").DefaultCellStyle.Format = "N2"
                If dgvStatistics.Columns.Contains("Completion %") Then dgvStatistics.Columns("Completion %").DefaultCellStyle.Format = "N1"

                ' --- Populate Dropdowns ---
                PopulateDropdown(cmbYear, uniqueYears, "All Years")
                PopulateDropdown(cmbProgram, uniquePrograms, "All Programs")

                ' Auto-select the active year if it exists
                If Not String.IsNullOrEmpty(activeYearLabel) AndAlso cmbYear.Items.Contains(activeYearLabel) Then
                    cmbYear.SelectedItem = activeYearLabel
                End If

            End Using
        Catch ex As Exception
            MessageBox.Show($"Failed to generate statistics: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PopulateDropdown(cmb As ComboBox, items As HashSet(Of String), defaultText As String)
        cmb.DropDownStyle = ComboBoxStyle.DropDownList
        cmb.Items.Clear()
        cmb.Items.Add(defaultText)
        For Each item In items.OrderBy(Function(x) x)
            cmb.Items.Add(item)
        Next
        cmb.SelectedIndex = 0
    End Sub

    ' ======================================================================
    ' 2. IN-MEMORY SMART FILTERING & SEARCH
    ' ======================================================================
    Private Sub FilterTriggers(sender As Object, e As EventArgs) Handles cmbYear.SelectedIndexChanged, cmbProgram.SelectedIndexChanged, cmbStatus.SelectedIndexChanged, txtSearch.TextChanged
        If isInitializing Then Return
        ApplySmartFilters()
    End Sub

    Private Sub ApplySmartFilters()
        If dtMasterStats Is Nothing OrElse dtMasterStats.Rows.Count = 0 Then Return

        Dim filters As New List(Of String)

        ' 1. Year Filter
        If cmbYear.SelectedIndex > 0 Then
            filters.Add($"[Academic Year] = '{cmbYear.SelectedItem.ToString().Replace("'", "''")}'")
        End If

        ' 2. Program Filter
        If cmbProgram.SelectedIndex > 0 Then
            filters.Add($"[Program] = '{cmbProgram.SelectedItem.ToString().Replace("'", "''")}'")
        End If

        ' 3. Status Filter (Mathematical Filtering)
        Select Case cmbStatus.SelectedItem.ToString()
            Case "Fully Cleared (100%)"
                filters.Add("[Completion %] >= 100")
            Case "In Debt (< 100%)"
                filters.Add("[Completion %] < 100")
            Case "Zero Paid (0%)"
                filters.Add("[Amount Paid] = 0")
        End Select

        ' 4. Search Bar (Checks both Index Number and Name instantly)
        Dim searchText As String = txtSearch.Text.Trim().Replace("'", "''")
        If searchText.Length > 0 Then
            filters.Add($"([Index No.] LIKE '%{searchText}%' OR [Student Name] LIKE '%{searchText}%')")
        End If

        ' Apply the unified filter string to the memory view
        If filters.Count > 0 Then
            dtMasterStats.DefaultView.RowFilter = String.Join(" AND ", filters)
        Else
            dtMasterStats.DefaultView.RowFilter = ""
        End If

        ' Update the top KPI cards based on the visible data
        UpdateKPIs()
    End Sub

    ' ======================================================================
    ' 3. DYNAMIC KPI CALCULATION
    ' ======================================================================
    Private Sub UpdateKPIs()
        Dim expected As Decimal = 0D
        Dim collected As Decimal = 0D
        Dim deficit As Decimal = 0D

        ' Iterate only the rows currently visible after filtering
        For Each rowView As DataRowView In dtMasterStats.DefaultView
            expected += Convert.ToDecimal(rowView("Target Fee"))
            collected += Convert.ToDecimal(rowView("Amount Paid"))
            deficit += Convert.ToDecimal(rowView("Deficit"))
        Next

        lblTotalExpected.Text = $"GHS {expected:N2}"
        lblTotalCollected.Text = $"GHS {collected:N2}"
        lblTotalDeficit.Text = $"GHS {deficit:N2}"

        ' Give the Admin visual danger warnings
        lblTotalDeficit.ForeColor = If(deficit > 0, Color.DarkRed, Color.DarkGreen)
    End Sub

    ' ======================================================================
    ' 4. LATE-BOUND EXCEL EXPORT ENGINE (IMAGE SAFE)
    ' ======================================================================
    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        If dgvStatistics.Rows.Count = 0 Then
            MessageBox.Show("There is no data to export.", "Empty Grid", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        btnExport.Enabled = False
        btnExport.Text = "Generating..."
        Application.DoEvents()

        Try
            Dim excelApp As Object = CreateObject("Excel.Application")
            If excelApp Is Nothing Then Throw New Exception("Microsoft Excel is not installed.")

            Dim workbook As Object = excelApp.Workbooks.Add()
            Dim worksheet As Object = workbook.Sheets(1)
            worksheet.Name = "Financial Statistics"

            ' Inject Headers
            Dim colIndex As Integer = 1
            For Each col As DataGridViewColumn In dgvStatistics.Columns
                ' CRITICAL PROTECTION: Do not export Image columns to Excel
                If col.Visible AndAlso Not TypeOf col Is DataGridViewImageColumn Then
                    worksheet.Cells(1, colIndex) = col.HeaderText
                    worksheet.Cells(1, colIndex).Font.Bold = True
                    worksheet.Cells(1, colIndex).Interior.Color = ColorTranslator.ToOle(Color.FromArgb(44, 62, 80)) ' Midnight Blue
                    worksheet.Cells(1, colIndex).Font.Color = ColorTranslator.ToOle(Color.White)
                    colIndex += 1
                End If
            Next

            ' Inject Data
            Dim rowIndex As Integer = 2
            For Each row As DataGridViewRow In dgvStatistics.Rows
                If Not row.IsNewRow Then
                    colIndex = 1
                    For Each col As DataGridViewColumn In dgvStatistics.Columns
                        ' CRITICAL PROTECTION: Skip writing image data to text cells
                        If col.Visible AndAlso Not TypeOf col Is DataGridViewImageColumn Then
                            Dim cellValue As String = If(row.Cells(col.Index).Value IsNot Nothing, row.Cells(col.Index).Value.ToString(), "")
                            worksheet.Cells(rowIndex, colIndex) = cellValue
                            colIndex += 1
                        End If
                    Next
                    rowIndex += 1
                End If
            Next

            ' Formatting
            Dim dataRange As Object = worksheet.Range(worksheet.Cells(1, 1), worksheet.Cells(rowIndex - 1, colIndex - 1))
            dataRange.Borders.LineStyle = 1
            dataRange.Columns.AutoFit()

            excelApp.Visible = True

        Catch ex As Exception
            MessageBox.Show($"Export Failed. Details: {ex.Message}", "Architecture Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            btnExport.Enabled = True
            btnExport.Text = "Export to Excel"
        End Try
    End Sub

    Private Sub dgvStatistics_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvStatistics.CellContentClick
    End Sub

    Private Sub PanelWithSearch_Paint(sender As Object, e As PaintEventArgs) Handles PanelWithSearch.Paint

    End Sub
End Class