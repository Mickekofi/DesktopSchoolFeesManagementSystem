Public Class Session
    ' Universal identity tracking variables
    Public Shared CurrentUsername As String = ""
    Public Shared CurrentFullName As String = ""
    Public Shared CurrentRole As String = ""
    Public Shared IsStudent As Boolean = False

    ' NEW: Academic tracking
    Public Shared CurrentProgramName As String = ""

    ' Call this when a user clicks Log Out to nuke their data
    Public Shared Sub ClearSession()
        CurrentUsername = ""
        CurrentFullName = ""
        CurrentRole = ""
        IsStudent = False
        CurrentProgramName = "" ' Nuke this as well
    End Sub
End Class