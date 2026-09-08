Imports System.Data
Imports MySql.Data.MySqlClient
Imports Microsoft.Reporting.WinForms
Imports System.Windows.Forms

Public Class UC_Reports

    ' 1. Declare the viewer dynamically to bypass designer corruption
    Private WithEvents rptViewer As New ReportViewer()
    Private isLoaded As Boolean = False

    Private Sub UC_Reports_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not isLoaded Then
            ' 2. Register the .NET 8 Text Encoding Engine
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance)

            ' 3. Physically mount the viewer and force it to the front
            rptViewer.Dock = DockStyle.Fill
            Me.Controls.Add(rptViewer)
            rptViewer.BringToFront()

            ' 4. Ignite the Data Pipeline
            LoadEnterpriseReport()
            isLoaded = True
        End If
    End Sub

    ' ======================================================================
    ' MODERN .NET REPORTING ENGINE 
    ' ======================================================================
    Private Sub LoadEnterpriseReport()
        Try
            ' Reset and target the canvas
            rptViewer.LocalReport.DataSources.Clear()
            rptViewer.LocalReport.ReportPath = Application.StartupPath & "\Reports\FinancialReport.rdlc"

            ' Extract and Map Data
            Dim reportData As New List(Of FinancialReportModel)()

            Using conn As MySqlConnection = Database.CreateOpenConnection()
                Dim query As String = "
                    SELECT 
                        s.index_number AS 'IndexNo',
                        s.full_name AS 'StudentName',
                        p.program_name AS 'Program',
                        fs.fee_amount AS 'TargetFee',
                        COALESCE((SELECT SUM(amount_paid) FROM payments pay WHERE pay.student_id = s.id AND pay.fee_structure_id = fs.id AND pay.payment_status = 'Completed'), 0) AS 'AmountPaid'
                    FROM students s
                    JOIN programs p ON s.program_id = p.id
                    JOIN fee_structures fs ON fs.program_id = p.id
                    JOIN academic_years y ON fs.academic_year_id = y.id
                    WHERE y.is_active = 1
                    ORDER BY p.program_name ASC, s.full_name ASC"

                Using cmd As New MySqlCommand(query, conn)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim target As Decimal = Convert.ToDecimal(reader("TargetFee"))
                            Dim paid As Decimal = Convert.ToDecimal(reader("AmountPaid"))

                            Dim record As New FinancialReportModel() With {
                                .IndexNo = reader("IndexNo").ToString(),
                                .StudentName = reader("StudentName").ToString(),
                                .Program = reader("Program").ToString(),
                                .TargetFee = target,
                                .AmountPaid = paid,
                                .Deficit = Math.Max(0, target - paid)
                            }
                            reportData.Add(record)
                        End While
                    End Using
                End Using
            End Using

            ' Data Trap Verification
            If reportData.Count = 0 Then
                MessageBox.Show("No active financial data found. The active academic year might be empty.", "Empty Ledger", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            ' Inject into the Viewer
            Dim rds As New ReportDataSource("DataSet1", reportData)
            rptViewer.LocalReport.DataSources.Add(rds)

            ' Force the UI to Paint the Layout
            rptViewer.SetDisplayMode(DisplayMode.PrintLayout)
            rptViewer.ZoomMode = ZoomMode.Percent
            rptViewer.ZoomPercent = 100

            ' Command the GDI+ Engine to render
            rptViewer.RefreshReport()

        Catch ex As Exception
            MessageBox.Show($"Report Generation Failed." & vbCrLf & vbCrLf & $"Detail: {ex.Message}", "Architecture Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class