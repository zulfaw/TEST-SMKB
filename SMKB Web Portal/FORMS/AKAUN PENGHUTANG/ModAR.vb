Imports System.Data.SqlClient
Imports System.Web.Configuration

Module ModAR
    Public Function fGetButirInv(ByVal strNoInv) As DataSet
        Try
            Dim strSql As String
            Dim ds As New DataSet
            strSql = "select a.AR01_NoBil, a.ar01_TkhMohon, a.AR01_Jenis, a.AR01_KodPTJMohon,a.AR01_NoStaf,a.AR01_UtkPerhatian,a.AR01_NoRujukan,a.AR01_Jumlah,a.AR01_StatusDok, a.AR01_StatusCetakBilSbnr,a.AR01_StatusCetakBilSmtr,
                      a.AR01_FlagAdj,a.AR01_JenisUrusniaga,a.AR01_Kategori,a.AR01_IDPenerima,a.AR01_NamaPenerima,a.AR01_Almt1,a.AR01_Almt2,a.AR01_Bandar,a.AR01_Poskod,a.KodNegeri,a.KodNegara,a.AR01_Emel,
                      a.AR01_NoTel,a.AR01_NoFaks,a.AR01_KodBank,a.AR01_Tujuan,a.AR01_JumBlmByr,a.AR01_JenisPljr,a.AR01_TkhKonDari,a.AR01_TkhKonHingga,a.AR01_TempohKontrak,a.AR01_Peringatan1,a.AR01_Peringatan2,
                      a.AR01_Peringatan3, a.AR01_TkhPeringatan1, a.AR01_TkhPeringatan2,a.AR01_TkhPeringatan3,a.AR01_TkhLulus,a.AR01_FlagGST,a.AR01_JumGST,a.AR01_JumTanpaGST,
                      (select b.AR06_Tarikh  from AR06_statusdok b where b.AR06_NoBil  = a.AR01_NoBil and ar06_statusdok='03' ) as Tarikh_Bil
                      from AR01_Bil a 
                      where  a.ar01_nobil='" & strNoInv & "'"

            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Return ds
                Else
                    Return Nothing
                End If
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Private Function fGetJenis(Butiran) As DataSet
        Try
            Dim strSql As String
            Dim ds As New DataSet
            strSql = "select KodUrusniaga , (KodUrusniaga + ' - ' + Butiran ) as Butiran from RC_Urusniaga order by KodUrusniaga"

            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Return ds
                Else
                    Return Nothing
                End If
            Else
                Return Nothing
            End If
        Catch ex As Exception

        End Try
    End Function
    Function getJawatan()
        Try

            Dim strSql As String
            Dim ds As New DataSet
            Dim strSQLSMSM = "select MS01_NoStaf, (MS01_NoStaf + ' - ' + MS01_Nama) as NamaStaf from MS01_Peribadi Where MS01_Status = '1' Order by MS01_Nama"
            '"Select a.MS01_Nama, a.MS01_NoStaf as NoStaf, c.JawGiliran,(a.MS01_NoStaf + '-' + a.MS01_Nama) as Butiran
            '             FROM MS01_Peribadi a inner join  ms08_penempatan e On a.ms01_nostaf = e.ms01_nostaf And e.MS08_StaTerkini = 1, MS02_Perjawatan b, MS_Jawatan c
            '             Where 1 = 1
            '             And b.MS01_NoStaf = a.MS01_NoStaf
            '             And c.KodJawatan = b.MS02_JawSandang
            '             And b.ms02_kumpjawatan = '1'
            '             ORDER BY a.MS01_Nama"
            Dim dbconnSMSM As New DBSMConn
            ds = dbconnSMSM.fselectCommand(strSQLSMSM)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Return ds
                Else
                    Return Nothing
                End If
            Else
                Return Nothing
            End If
        Catch ex As Exception

        End Try
    End Function
    Public Function fDeleteInv(ByVal strInv As String) As Boolean
        Dim strSql As String
        Dim dbConn As New DBKewConn
        Dim blnSuccess As Boolean = True
        Try

            strSql = "update AR01_Bil set AR01_StatusDok = @StatDok where AR01_NoBilSem = @NoBilSem"

            Dim paramSql() As SqlParameter = {
                        New SqlParameter("@StatDok", "12"),
                        New SqlParameter("@NoBilSem", strInv)
                        }

            dbConn.sConnBeginTrans()
            If dbConn.fUpdateCommand(strSql, paramSql) > 0 Then
                dbConn.sConnCommitTrans()
                Return True
            Else
                dbConn.sConnRollbackTrans()
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function fGetNo(ByVal strTahun As String, ByVal strKodModul As String, ByVal strPrefix As String, ByVal strButiran As String)
        Try
            Dim strSql As String
            Dim intLastIdx As Integer
            Dim strIdx As String

            strSql = "select * from mk_noakhir where tahun = '" & strTahun & "' and kodmodul='" & strKodModul & "' and prefix='" & strPrefix & "'"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    intLastIdx = CInt(ds.Tables(0).Rows(0)("NoAkhir").ToString)
                Else
                    intLastIdx = 0
                End If

                If intLastIdx = 0 Then
                    strIdx = strPrefix & "000001" & Now.ToString("MM") & Now.ToString("yy")

                    strSql = " insert into mk_noakhir (KodModul , Prefix, NoAkhir, Tahun, Butiran, KodPTJ)" &
                    "values(@kodmodul,@prefix,@noakhir,@tahun,@butiran,@kodPTJ)"

                    Dim paramSql() As SqlParameter = {
                        New SqlParameter("@kodmodul", strKodModul),
                        New SqlParameter("@prefix", strPrefix),
                        New SqlParameter("@noakhir", 1),
                        New SqlParameter("@tahun", strTahun),
                        New SqlParameter("@butiran", strButiran),
                        New SqlParameter("@kodPTJ", "-")
                    }

                    dbconn = New DBKewConn
                    dbconn.sConnBeginTrans()
                    If dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                        dbconn.sConnCommitTrans()
                        Return strIdx
                    Else
                        dbconn.sConnRollbackTrans()
                        Return Nothing
                    End If
                Else

                    intLastIdx = intLastIdx + 1
                    strIdx = strPrefix & intLastIdx.ToString("D6") & Now.ToString("MM") & Now.ToString("yy")

                    strSql = "update MK_NoAkhir set NoAkhir =@noakhir where tahun =@tahun and KodModul =@kodmodul and prefix = @prefix"

                    Dim paramSql2() As SqlParameter = {
                                New SqlParameter("@noakhir", intLastIdx),
                                New SqlParameter("@tahun", strTahun),
                                New SqlParameter("@kodmodul", strKodModul),
                                New SqlParameter("@prefix", strPrefix)
                                }

                    dbconn.sConnBeginTrans()
                    If dbconn.fUpdateCommand(strSql, paramSql2) > 0 Then
                        dbconn.sConnCommitTrans()
                        Return strIdx
                    Else
                        dbconn.sConnRollbackTrans()
                        Return Nothing
                    End If

                End If
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function fGetJenTemp(strKodJenTemp) As String
        Try
            Dim strSql = "select Butiran from PO_Tempoh where kod = '" & strKodJenTemp & "'"

            Dim dbconn As New DBKewConn
            Dim ds As New DataSet
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Return ds.Tables(0).Rows(0)("Butiran")
                Else
                    Return ""
                End If
            Else
                Return ""
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function fGetNoJurSem(strPrefix)
        Try

            Dim strSql As String
            Dim strIdx As String
            Dim intLastIdx As Integer
            Dim strCol As String
            Dim strTahun As String = Now.Year
            Dim strKodModul As String

            Dim strButiran As String = "Jurnal Sementara"

            strSql = "select * from mk_noakhir where tahun = '" & strTahun & "' and kodmodul='GL' and prefix='" & strPrefix & "'"
            strCol = "NoAkhir"
            strKodModul = "GL"


            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    intLastIdx = CInt(ds.Tables(0).Rows(0)(strCol).ToString)
                Else
                    intLastIdx = 0

                End If

                If intLastIdx = 0 Then
                    strIdx = strPrefix & "000001" & Now.ToString("MM") & Now.ToString("yy")

                    strSql = " insert into mk_noakhir (KodModul , Prefix, NoAkhir, Tahun, Butiran, KodPTJ)" &
                    "values(@kodmodul,@prefix,@noakhir,@tahun,@butiran,@kodPTJ)"

                    Dim paramSql() As SqlParameter = {
                        New SqlParameter("@kodmodul", strKodModul),
                        New SqlParameter("@prefix", strPrefix),
                        New SqlParameter("@noakhir", 1),
                        New SqlParameter("@tahun", strTahun),
                        New SqlParameter("@butiran", strButiran),
                        New SqlParameter("@kodPTJ", "-")
                    }

                    dbconn = New DBKewConn
                    dbconn.sConnBeginTrans()
                    If dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                        dbconn.sConnCommitTrans()
                    Else
                        dbconn.sConnRollbackTrans()
                    End If

                Else

                    intLastIdx = intLastIdx + 1
                    strIdx = strPrefix & intLastIdx.ToString("D6") & Now.ToString("MM") & Now.ToString("yy")

                    strSql = "update MK_NoAkhir set NoAkhir =@noakhir where tahun =@tahun and KodModul =@kodmodul and prefix = @prefix"

                    Dim paramSql2() As SqlParameter = {
                                New SqlParameter("@noakhir", intLastIdx),
                                New SqlParameter("@tahun", strTahun),
                                New SqlParameter("@kodmodul", strKodModul),
                                New SqlParameter("@prefix", strPrefix)
                                }

                    dbconn.sConnBeginTrans()
                    If dbconn.fUpdateCommand(strSql, paramSql2) > 0 Then
                        dbconn.sConnCommitTrans()
                    Else
                        dbconn.sConnRollbackTrans()
                    End If

                End If

                Return strIdx
            End If


        Catch ex As Exception

        End Try
    End Function

    Public Function fLoadPenghutang(strKat) As DataSet
        Try
            Dim strsql As String
            strsql = "select KodPenghutang, IdPenghutang, NamaPenghutang, (IdPenghutang + ' - ' + NamaPenghutang) as Penghutang, Kategori ,Alamat1 , Alamat2 , Bandar, Poskod, KodNegeri, (select MK_Negeri.Butiran from MK_Negeri where MK_Negeri.KodNegeri = AR_Penghutang .KodNegeri) as ButNegeri, KodNegara, (select MK_Negara .Butiran  from MK_Negara where MK_Negara .KodNegara = AR_Penghutang .KodNegara ) as ButNegara, NoTel , nofax, emel , (KodPenghutang + ' - ' + NamaPenghutang) as Penghutang, Perhatian from AR_Penghutang where Kategori = '" & strKat & "' and Status = 1 order by KodPenghutang"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn

            ds = dbconn.fSelectCommand(strsql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Return ds
                Else
                    Return Nothing
                End If
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function fGetEmailPIC(strKodSubMenu, intKodTugas) As String
        Dim strSql As String
        Dim dbconn As New DBKewConn
        Try
            strSql = "select Email from MK_TugasDt where KodSubMenu = '" & strKodSubMenu & "' and KodTugas = " & intKodTugas & " and Status = 1"

            Dim ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim strEmail = ds.Tables(0).Rows(0)("Email").ToString
                    Return strEmail
                Else
                    Return ""
                End If
            Else
                Return ""
            End If

        Catch ex As Exception
            Return ""
        End Try
    End Function

End Module
