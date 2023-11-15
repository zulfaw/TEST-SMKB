Module ModFunctionBajet

    Public Function fGetStatusDok(kod As String) As Dictionary(Of String, String)
        Dim strSql As String = $"Select KodStatusDok, Butiran from BG_StatusDok where KodStatusDok = '{kod}'"

        Dim ds As New DataSet
        Dim dbconn As New DBKewConn
        ds = dbconn.fselectCommand(strSql)

        'Convert dataset to datatable
        Dim dt = ds.Tables(0)

        Dim dictStatusDok As New Dictionary(Of String, String)
        Dim NamaStatus = ""
        Dim KodStatusDok = ""

        If dt.Rows.Count > 0 Then
            NamaStatus = dt.Rows(0)("Butiran").ToString
            KodStatusDok = dt.Rows(0)("KodStatusDok").ToString
            dictStatusDok.Add(KodStatusDok, NamaStatus)
        End If

        Return dictStatusDok
    End Function

    Public Function fGetPeringkatBajetPTJ(kodPTJ As String) As Integer
        Dim kodPrgkt = 0
        Try
            Dim currentYear = Date.Now.Year.ToString
            Dim strSql = $"select KodPeringkat FROM BG40_PenentuanPeringkat WHERE KodPTJ = '{kodPTJ}' AND Tahun='{currentYear}'"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            dbconn.sSelectCommand(strSql, kodPrgkt)

        Catch ex As Exception

        End Try

        Return kodPrgkt
    End Function
End Module
