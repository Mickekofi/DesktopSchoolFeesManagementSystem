Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Windows.Forms

Public Class UC_ManageFees

    ' Internal state variables to track temporal constraints
    Private activeYearId As Integer = 0
    Private activeYearLabel As String = ""
    Private activeSemesterName As String = "" ' Retained strictly for UI/Business Rule calculations

    Private Sub UC_ManageFees_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' UI Enhancements
        ShapeUtilities.RadiusButton(btnGenerateFees, 1.5F)
        DataGridViewHelper.ApplyBeautifulStyle(dgvFeeStructures)

        VerifySystemState()
        LoadExistingFeeStructures()
    End Sub

    ' ======================================================================
    ' 1. STATE VERIFICATION
    ' ======================================================================
    Private Sub VerifySystemState()
        Try
            Using conn As MySqlConnection = Database.CreateOpenConnection()
                ' We query the calendar tables to see what term the campus is currently running
                Dim query As String = "
                    SELECT y.id AS year_id, y.year_label, s.semester_name 
                    FROM academic_years y 
                    JOIN semesters s ON s.academic_year_id = y.id 
                    WHERE y.is_active = 1 AND s.is_active = 1 LIMIT 1"

                Using cmd As New MySqlCommand(query, conn)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            activeYearId = Convert.ToInt32(reader("year_id"))
                            activeYearLabel = reader("year_label").ToString()
                            activeSemesterName = reader("semester_name").ToString()

                            ' Inform the administrator of the exact active timeline context
                            lblActiveTerm.Text = $"Active Financial Term: {activeYearLabel} ({activeSemesterName})"
                            lblActiveTerm.ForeColor = System.Drawing.Color.DarkGreen
                            btnGenerateFees.Enabled = True
                        Else
                            lblActiveTerm.Text = "System Offline: No Active Term Set in Configuration"
                            lblActiveTerm.ForeColor = System.Drawing.Color.DarkRed
                            btnGenerateFees.Enabled = False
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"Failed to verify system state: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ======================================================================
    ' 2. VIEW EXISTING LEDGER ENTRIES
    ' ======================================================================
    Private Sub LoadExistingFeeStructures()
        If activeYearId = 0 Then Return ' Halt if system is offline

        ' REFACTORED: Stripped fs.semester condition from the query completely
        Dim query As String = "
            SELECT 
                p.program_name AS 'Academic Program',
                p.base_fee AS 'Base Catalog Fee (GHS)',
                fs.fee_amount AS 'Annual Billed Target (GHS)',
                fs.min_payment_percentage AS 'Min Payment %'
            FROM fee_structures fs
            JOIN programs p ON fs.program_id = p.id
            WHERE fs.academic_year_id = @YearId
            ORDER BY p.program_name ASC"

        Dim dt As New DataTable()
        Try
            Using conn As MySqlConnection = Database.CreateOpenConnection()
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@YearId", activeYearId)
                    Using da As New MySqlDataAdapter(cmd)
                        dt.Clear()
                        da.Fill(dt)
                    End Using
                End Using
            End Using

            dgvFeeStructures.DataSource = dt
            dgvFeeStructures.ReadOnly = True
            dgvFeeStructures.AllowUserToAddRows = False

            If dgvFeeStructures.Columns.Contains("Annual Billed Target (GHS)") Then
                dgvFeeStructures.Columns("Annual Billed Target (GHS)").DefaultCellStyle.Format = "N2"
            End If

        Catch ex As Exception
            MessageBox.Show($"Failed to load fee structures: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ======================================================================
    ' 3. BULK INVOICE GENERATOR ENGINE
    ' ======================================================================
    Private Sub btnGenerateFees_Click(sender As Object, e As EventArgs) Handles btnGenerateFees.Click
        If activeYearId = 0 Then Return

        Dim confirm = MessageBox.Show($"You are about to generate official ANNUAL target fee structures for ALL active programs for the academic year {activeYearLabel}." & vbCrLf & vbCrLf &
                                      $"Note: Current campus timeline is set to {activeSemesterName}. If annual fees have already been generated for this year, duplicates will be ignored.", "System Authentication", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If confirm = DialogResult.No Then Return

        btnGenerateFees.Enabled = False
        Dim programsProcessed As Integer = 0
        Dim structuresCreated As Integer = 0

        Try
            Using conn As MySqlConnection = Database.CreateOpenConnection()
                Using transaction = conn.BeginTransaction()
                    Try
                        ' Step A: Pull all active programs and their baseline pricing
                        Dim fetchProgramsCmd As New MySqlCommand("SELECT id, base_fee FROM programs WHERE status = 'Active'", conn, transaction)
                        Dim dtPrograms As New DataTable()
                        Using da As New MySqlDataAdapter(fetchProgramsCmd)
                            da.Fill(dtPrograms)
                        End Using

                        ' Step B: Determine dynamic enforcement baselines based on current term context
                        ' If an admin initializes fees during Semester 1, require a 50% threshold to register courses.
                        ' If initializing/updating during Semester 2, kick the financial gate threshold up to 100%.
                        Dim minPercentage As Decimal = If(activeSemesterName = "Semester 1", 50D, 100D)

                        ' Step C: Process records with clean exception/duplicate traps
                        For Each row As DataRow In dtPrograms.Rows
                            Dim progId As Integer = Convert.ToInt32(row("id"))
                            Dim baseFee As Decimal = Convert.ToDecimal(row("base_fee"))
                            programsProcessed += 1

                            ' Idempotent Check: One program + One year = One row max
                            Dim checkCmd As New MySqlCommand("SELECT COUNT(*) FROM fee_structures WHERE program_id = @ProgId AND academic_year_id = @YearId", conn, transaction)
                            checkCmd.Parameters.AddWithValue("@ProgId", progId)
                            checkCmd.Parameters.AddWithValue("@YearId", activeYearId)

                            Dim exists As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())

                            ' REFACTORED: Completely eliminated @Semester parameter insertions
                            If exists = 0 Then
                                Dim insertCmd As New MySqlCommand("
                                    INSERT INTO fee_structures (program_id, academic_year_id, fee_amount, min_payment_percentage, is_active) 
                                    VALUES (@ProgId, @YearId, @FeeAmount, @MinPct, 1)", conn, transaction)

                                insertCmd.Parameters.AddWithValue("@ProgId", progId)
                                insertCmd.Parameters.AddWithValue("@YearId", activeYearId)
                                insertCmd.Parameters.AddWithValue("@FeeAmount", baseFee)
                                insertCmd.Parameters.AddWithValue("@MinPct", minPercentage)

                                insertCmd.ExecuteNonQuery()
                                structuresCreated += 1
                            End If
                        Next

                        transaction.Commit()
                        MessageBox.Show($"Execution Complete." & vbCrLf & vbCrLf & $"Active Programs Evaluated: {programsProcessed}" & vbCrLf & $"New Annual Structures Implemented: {structuresCreated}", "System Output", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Catch ex As Exception
                        transaction.Rollback()
                        Throw New Exception("Transaction aborted due to operational integrity failure. Data rolled back.", ex)
                    End Try
                End Using
            End Using

            ' Refresh visual state
            LoadExistingFeeStructures()

        Catch ex As Exception
            MessageBox.Show($"Fatal error during fee generation: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            btnGenerateFees.Enabled = True
        End Try
    End Sub

    Private Sub dgvFeeStructures_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvFeeStructures.CellContentClick

    End Sub
End Class