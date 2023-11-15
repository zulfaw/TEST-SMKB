Imports System.Data.SqlClient

Module modPembayaran

    Public Function fGetPT(ByVal strNoPT As String) As DataSet
        Dim strSql As String
        Try

            'strSql = "SELECT PO19_PtID, PO19_Pt.PO19_NoPt, ROC01_Syarikat.ROC01_NoSya, ROC01_Syarikat.ROC01_NamaSya, PO19_Pt.PO19_BekalKepada,PO01_PPembelian.PO01_Tujuan, PO01_PPembelian.PO01_JenisBrg as KodJenBekalan, PO_JenisItem.Butiran as JenBekalan, PO19_Pt.PO19_JenisDokPT ,AP_Jenis.Kod as KodJenBayaran , AP_Jenis.Butiran as ButiranJenbayaran " &
            '    "FROM PO19_Pt " &
            '    "INNER JOIN ROC01_Syarikat ON PO19_Pt.ROC01_IdSya = ROC01_Syarikat.ROC01_IDSya " &
            '    "INNER JOIN PO01_PPembelian ON PO19_Pt.PO01_NoMohon = PO01_PPembelian.PO01_NoMohon " &
            '    "INNER JOIN  PO_JenisItem ON PO01_PPembelian.PO01_JenisBrg = PO_JenisItem .Kod " &
            '    "inner join AP_Jenis on PO19_Pt.PO19_JenisDokPT = AP_Jenis.KodJenPO " &
            '    "WHERE ROC01_Syarikat.ROC01_KodLulus = '1' AND ROC01_Syarikat.ROC01_KodAktif = '01' And PO19_Pt.PO19_Status = 'A' and  PO19_Pt.PO19_StatusPP = '074' and PO19_Pt.PO19_NoPt = '" & strNoPT & "'"

            strSql = "select IdPT, NoPt , NoSya , NamaSya ,BekalKpd, Tujuan, kodJenBekal, JenBekal  from VAP_MaklumatPT 
where NoPt = '" & strNoPT & "'"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            Return ds
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function fGetPtDt(ByVal intPtID As Integer) As DataSet
        Dim strSql As String
        Try
            'strSql = "select PO19_PtDtId, PO01_PPembelianDt.PO01_Butiran , PO01_PPembelianDt.PO01_Kuantiti  ,PO19_PtDt.PO19_KadarHarga  , PO19_PtDt.PO19_JumKadar " &
            '            "from PO19_PtDt " &
            '            "INNER JOIN PO01_PPembelianDt on PO19_PtDt .PO01_DtID  = PO01_PPembelianDt.PO01_DtID " &
            '            "where PO19_PtID = " & intPtID & ""

            strSql = "select PO19_PtDtId, PO19_Butiran, KodKw, KodKO, KodPtj, KodKP, KodVot, PO19_Kuantiti, PO19_QBlmByr, PO19_KadarHarga, PO19_JumKadar  
from PO19_PtDt 
where PO19_PtID = " & intPtID

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            Return ds
        Catch ex As Exception

        End Try
    End Function

    Public Function fGetGRNdt(ByVal intIdGRN As Integer) As DataSet
        Try
            Dim strSql As String
            'strSql = "select AP02_GRNDt.AP02_IdGRNDt, AP02_GRNDt.PO19_PtDtId, PO19_PtDt.PO19_Butiran, PO19_PtDt.KodKw, PO19_PtDt.KodKO, PO19_PtDt.KodPtj, PO19_PtDt.KodKP, PO19_PtDt.KodVot, PO19_PtDt.PO19_Kuantiti,  AP02_GRNDt.AP02_Terima, AP02_GRNDt.AP02_Selisih,PO19_PtDt.PO19_KadarHarga , AP02_GRNDt.AP02_Amaun   
            'from AP02_GRNDt 
            'inner join AP02_GRN on AP02_GRN .AP02_idGRN = AP02_GRNDt .AP02_idGRN
            'inner join PO19_PtDt on PO19_Ptdt .PO19_PtDtId = AP02_GRNDt .PO19_PtDtId  
            'where AP02_GRN .AP02_Status = 1 and AP02_GRN .AP02_NoGRN = '" & strNoGRN & "' and AP02_GRNDt .Status = 1"

            '            strSql = "select AP02_IdGRNDt , PO19_PtDtId , PO19_Butiran , KodKw , KodKO , KodPtj , KodKP , KodVot , PO19_Kuantiti , AP02_KuantitiTerima , AP02_KuantitiSelisih , PO19_KadarHarga , AP02_JumBayarDt  
            'from VAP_GRNDt  where AP02_NoGRN = '" & strNoGRN & "'
            'order by KodKw"

            strSql = "select AP02_IdGRNDt, PO19_Butiran, KodKw, KodKO, KodPtj, KodKP, KodVot, AP02_NoDok, AP02_TkhDok,  AP02_KuantitiTerima, PO19_KadarHarga , AP02_JumBayarDt  
from AP02_GRNDt  where AP02_idGRN = '" & intIdGRN & "'
order by AP02_IdGRNDt"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)
            Return ds
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function fGetJenByrn(ByVal KodJenByrn As String) As String
        Dim strSql As String

        Try
            strSql = "select kod + ' - ' + Butiran as JenByrn from AP_Jenis  where kod = '" & KodJenByrn & "'"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    fGetJenByrn = ds.Tables(0).Rows(0)("JenByrn").ToString
                Else
                    fGetJenByrn = Nothing
                End If
            Else
                fGetJenByrn = Nothing
            End If
        Catch ex As Exception
            fGetJenByrn = Nothing
        End Try
    End Function

    Public Function fCheckFIR(ByVal strNoPT As String, ByRef ds As DataSet) As Boolean
        Dim strSql As String

        Try
            strSql = " select IdSya, NoSya, NamaSya,  PO07_Alamat1, PO07_Alamat2, PO07_Bandar, ButiranNegeri, PO07_Negeri, PO07_ByrAtasNama, ButiranNegara, PO07_Negara, PO07_Poskod, PO07_Negara, PO07_Negeri, MK_Bank, PO07_NoAkaun, Emel, PO07_Status 
          from VAP_NomineesFIR where PO19_NoPt =  '" & strNoPT & "'"

            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Return True
                Else
                    Return False

                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try

    End Function


    Public Function fGetSya(ByVal strNoPT As String) As DataSet
        Try
            Dim strSql As String
            Dim ds As New DataSet
            strSql = "select IdSya, NoSya, NamaSya, AlmtP1, AlmtP2, BandarP, KodNegeriP, NegeriP, KodNegaraP,  NegaraP,  PoskodP, KodBank,(SELECT b.Nama  from MK_BankCreditOnline b where b.Kod = KodBank  ) as NamaBank, NoAkaun, Emel, Jumbayar  
                        from VAP_MaklumatPT where NoPt = '" & strNoPT & "'"

            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)
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

    Public Function fGetJenItem() As DataSet
        Try
            Dim strSql As String

            strSql = "select Kod, Butiran  from AP_JenItem"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)
            Return ds
        Catch ex As Exception

        End Try
    End Function

    Public Function fDeleteGRN(ByVal strNoGRN As String) As Boolean
        Dim strSql As String
        Dim dbConn As New DBKewConn
        Dim blnSuccess As Boolean = True
        Try
            strSql = "update AP02_GRN Set AP09_StatusDok  = 38 where AP02_NoGRN = '" & strNoGRN & "'"
            dbConn.sConnBeginTrans()
            If dbConn.fUpdateCommand(strSql) > 0 Then
                dbConn.sConnCommitTrans()
                Return True
            Else
                dbConn.sConnRollbackTrans()
                Return False
            End If

        Catch ex As Exception

        End Try

    End Function

    Public Function fGetInfoSya(ByVal strIdSya As String) As DataSet
        Dim strSql As String

        Try
            strSql = "select IDSyarikat , NoSyarikat , NamaSyarikat , AlmtP1 ,AlmtP2 ,BandarP , PoskodP , ButiranNegeri , ButiranNegara , KodBank, NamaBank, NoAkaun  
from VAP_SenaraiPembekal where IDSyarikat = '" & strIdSya & "'"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)
            Return ds
        Catch ex As Exception

        End Try
    End Function

    Public Function fGetAmaunTrans(ByVal intIdBaucarDt As Integer, ByVal strNoDraf As String, ByVal strKodKW As String, ByVal strKodKO As String, ByVal strKodPTj As String, ByVal strKodKP As String, ByVal strKodVot As String) As Decimal
        Dim strSql As String
        Dim ds As New DataSet
        Dim dbConn As New DBKewConn
        Dim decAmt As Decimal
        Try
            strSql = "select AP04_Debit from AP04_BaucarDt with (nolock) where AP04_IdBaucarDt = '" & intIdBaucarDt & "' AND AP04_NoDraf = '" & strNoDraf & "' and KodKw = '" & strKodKW & "' and KodKO = '" & strKodKO & "' and KodPtj = '" & strKodPTj & "' and KodKP = '" & strKodKP & "' and KodVot = '" & strKodVot & "'"
            ds = dbConn.fSelectCommand(strSql)


            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    decAmt = CDec(ds.Tables(0).Rows(0).Item("AP04_Debit").ToString)
                    Return decAmt
                Else
                    Return 0
                End If
            Else
                Return 0
            End If
        Catch ex As Exception
            Return 0
        End Try

    End Function

    Public Function fGetNoBK(ByVal strKodModul As String, ByRef strPrefix As String, ByVal strTahun As String, ByVal strKodPTj As String) As String
        Try
            Dim blnFound As Boolean = False
            Dim ds As New DataSet
            Dim strIdBD As String
            Dim dbconn As New DBKewConn
            Dim intNoAkhir As Integer

            Dim strSql As String = " SELECT NoAkhir From MK_NoAkhir WHERE KodModul = '" & strKodModul & "' AND  Prefix = '" & strPrefix & "' AND Tahun = '" & strTahun & "'"
            ds = fGetRec(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    intNoAkhir = CInt(ds.Tables(0).Rows(0)("NoAKhir").ToString)
                    blnFound = True
                Else
                    intNoAkhir = 0
                End If
            Else
                intNoAkhir = 0
            End If

            intNoAkhir = intNoAkhir + 1

            If blnFound = True Then
                strSql = "update MK_NoAkhir set NoAkhir =@noakhir  WHERE KodModul = @kodmodul AND  Prefix = @prefix AND Tahun = @tahun AND KodPTJ = @KodPTJ "

                Dim paramSql2() As SqlParameter = {
                        New SqlParameter("@noakhir", intNoAkhir),
                        New SqlParameter("@tahun", strTahun),
                        New SqlParameter("@kodmodul", strKodModul),
                        New SqlParameter("@prefix", strPrefix),
                         New SqlParameter("@KodPTJ", strKodPTj)
                        }

                dbconn.sConnBeginTrans()
                If dbconn.fUpdateCommand(strSql, paramSql2) > 0 Then
                    dbconn.sConnCommitTrans()
                Else
                    dbconn.sConnRollbackTrans()
                End If
            Else
                strSql = "insert into MK_NoAkhir (KodModul , Prefix , NoAkhir , Tahun , Butiran , KodPTJ ) " &
            "values (@KodModul, @Prefix, @NoAkhir, @Tahun, @Butiran, @KodPTJ)"
                Dim paramSql() As SqlParameter = {
                New SqlParameter("@KodModul", strKodModul),
                New SqlParameter("@Prefix", strPrefix),
                New SqlParameter("@NoAkhir", 1),
                New SqlParameter("@Tahun", strTahun),
                New SqlParameter("@Butiran", "-"),
                New SqlParameter("@KodPTJ", strKodPTj)
            }

                dbconn = New DBKewConn
                dbconn.sConnBeginTrans()
                If dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                    dbconn.sConnCommitTrans()
                Else
                    dbconn.sConnRollbackTrans()
                End If
            End If

            strIdBD = strPrefix + intNoAkhir.ToString("D6") + Now.ToString("MM") + Now.ToString("yy")

            Return strIdBD

        Catch ex As Exception

        End Try
    End Function

    Public Function fGetRunNo(ByVal strPrefix As String, ByVal strKodPTj As String, ByVal intSeq As Integer) As String
        Try
            Dim blnFound As Boolean = False
            Dim ds As New DataSet
            Dim strRunNo As String
            Dim dbconn As New DBKewConn
            Dim strKodModul As String = "AP"
            Dim strTahun As String = Now.Year
            Dim intNoAkhir As Integer

            Dim strSql As String = "SELECT NoAkhir From MK_NoAkhir WHERE KodModul = '" & strKodModul & "' AND  Prefix = '" & strPrefix & "' AND Tahun = '" & Now.Year & "' AND KodPTJ = '" & strKodPTj & "' "
            ds = fGetRec(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    intNoAkhir = CInt(ds.Tables(0).Rows(0)("NoAKhir").ToString)
                    blnFound = True
                Else
                    intNoAkhir = 0
                End If
            Else
                intNoAkhir = 0
            End If

            intNoAkhir = intNoAkhir + 1

            If blnFound = True Then
                strSql = "update MK_NoAkhir set NoAkhir =@noakhir  WHERE KodModul = @kodmodul AND  Prefix = @prefix AND Tahun = @tahun AND KodPTJ = @KodPTJ "

                Dim paramSql2() As SqlParameter = {
                            New SqlParameter("@noakhir", intNoAkhir),
                            New SqlParameter("@tahun", Now.Year),
                            New SqlParameter("@kodmodul", strKodModul),
                            New SqlParameter("@prefix", strPrefix),
                             New SqlParameter("@KodPTJ", strKodPTj)
                            }

                dbconn.sConnBeginTrans()
                If dbconn.fUpdateCommand(strSql, paramSql2) > 0 Then
                    dbconn.sConnCommitTrans()
                Else
                    dbconn.sConnRollbackTrans()
                    Return String.Empty
                End If
            Else
                strSql = "insert into MK_NoAkhir (KodModul , Prefix , NoAkhir , Tahun , Butiran , KodPTJ ) " &
                "values (@KodModul, @Prefix, @NoAkhir, @Tahun, @Butiran, @KodPTJ)"
                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@KodModul", strKodModul),
                    New SqlParameter("@Prefix", strPrefix),
                    New SqlParameter("@NoAkhir", 1),
                    New SqlParameter("@Tahun", strTahun),
                    New SqlParameter("@Butiran", "-"),
                    New SqlParameter("@KodPTJ", strKodPTj)
                }

                dbconn = New DBKewConn
                dbconn.sConnBeginTrans()
                If dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                    dbconn.sConnCommitTrans()
                Else
                    dbconn.sConnRollbackTrans()
                    Return String.Empty
                End If
            End If

            'IPT 410000 000004 01 18
            strRunNo = strPrefix + strKodPTj + intNoAkhir.ToString("D" & intSeq) + Now.ToString("MM") + Now.ToString("yy")
            Return strRunNo

        Catch ex As Exception
            Return String.Empty
        End Try
    End Function

    Public Function fGetNegeri() As DataSet
        Try
            Dim strSql As String = "select kodnegeri, Butiran from MK_Negeri where KodNegeri <> '-'"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)
            Return ds
        Catch ex As Exception
            '("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
        End Try
    End Function

    Public Function fGetNegara() As DataSet
        Try
            Dim strSql As String = "select KodNegara , Butiran  from MK_Negara where KodNegara <> '-'"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)
            Return ds
        Catch ex As Exception
            '("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
        End Try
    End Function

    Public Function fGetBank() As DataSet
        Try
            Dim strSql As String = "select Kod,  Nama from MK_BankCreditOnline where Kod  <> '-' order by Nama"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)
            Return ds
        Catch ex As Exception
            '("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
        End Try
    End Function

    Public Function fGetButKW(strKodKW) As String
        Dim strButiran As String
        Try
            Dim strSql As String = "select Butiran from MK_Kw where KodKw = '" & strKodKW & "'"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strButiran = ds.Tables(0).Rows(0)("Butiran").ToString
                End If
            End If
            Return strButiran
        Catch ex As Exception
            '("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
        End Try
    End Function

    Public Function fGetKat() As DataSet
        Try
            Dim strSql As String = "select KOD, (KOD + ' - ' + BUTIRAN) as BUTIRAN from MK_KATEGORIPENERIMA WHERE KOD NOT IN ('KJ') ORDER BY KOD DESC"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)
            Return ds
        Catch ex As Exception
            '("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
        End Try
    End Function

    Public Function fGetStatPel() As DataSet
        Try
            Dim strSql As String = "select distinct smp01_status as Status from SMP01_PERIBADI order by smp01_status"

            Dim ds As New DataSet
            Dim dbconn As New DBSMPConn
            ds = dbconn.fselectCommand(strSql)
            Return ds
        Catch ex As Exception
            '("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
        End Try
    End Function

    Public Function fGetAddrUtem() As DataSet
        Try
            Dim strSql As String = "SELECT A.Nama, A.Almt1, A.Almt2, A.Bandar, A.Poskod , A.KodNegeri, B.Butiran as ButNegeri,  A.KodNegara, C.Butiran as ButNegara, A.NoTel1  FROM MK_Korporat A
inner join MK_Negeri B on b.KodNegeri = a.KodNegeri 
inner join MK_Negara C on C.KodNegara = a.KodNegara "

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)
            Return ds
        Catch ex As Exception
            '("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
        End Try
    End Function

    Public Function fCreateDtNom() As DataTable
        Try
            Dim dt As New DataTable

            Dim colRowID As DataColumn = dt.Columns.Add("IdNom", GetType(Int32))
            colRowID.AutoIncrement = True
            colRowID.AutoIncrementSeed = 1
            colRowID.AutoIncrementStep = 1
            colRowID.Unique = True
            colRowID.ReadOnly = True

            dt.Columns.Add("Kat", GetType(String))
            dt.Columns.Add("IdPenerima", GetType(String))
            dt.Columns.Add("Penerima", GetType(String))
            dt.Columns.Add("Jumlah", GetType(String))
            dt.Columns.Add("Almt1", GetType(String))
            dt.Columns.Add("Almt2", GetType(String))
            dt.Columns.Add("Bandar", GetType(String))
            dt.Columns.Add("Poskod", GetType(String))
            dt.Columns.Add("KodNegeri", GetType(String))
            dt.Columns.Add("KodNegara", GetType(String))
            dt.Columns.Add("Emel", GetType(String))
            dt.Columns.Add("KodBank", GetType(String))
            dt.Columns.Add("NoAkaun", GetType(String))

            Return dt

        Catch ex As Exception

        End Try
    End Function

    Public Function fCreateDtTrans() As DataTable
        Try
            Dim dt As New DataTable
            Dim dr As DataRow = Nothing
            Dim colRowID As DataColumn = dt.Columns.Add("IdPPDt", GetType(Int32))
            colRowID.AutoIncrement = True
            colRowID.AutoIncrementSeed = 1
            colRowID.AutoIncrementStep = 1
            colRowID.Unique = True
            colRowID.ReadOnly = True

            dt.Columns.Add("KW", GetType(String))
            dt.Columns.Add("KO", GetType(String))
            dt.Columns.Add("PTj", GetType(String))
            dt.Columns.Add("KP", GetType(String))
            dt.Columns.Add("Vot", GetType(String))
            dt.Columns.Add("NoRuj", GetType(String))
            dt.Columns.Add("Tarikh", GetType(String))
            dt.Columns.Add("Jumlah", GetType(String))
            dt.Columns.Add("BakiP", GetType(String))

            Return dt

        Catch ex As Exception

        End Try
    End Function

    Public Function fGetVot(ByVal strKodKW As String, ByVal strKodKO As String, ByVal strKodPTj As String, ByVal strKodKP As String) As DataSet
        Try

            Dim strSql As String = "SELECT DISTINCT A.KodVot, (A.KODVOT + ' - ' + B.BUTIRAN) as Butiran FROM MK03_AmTahun A INNER JOIN MK_VOT B ON A.KODVOT = B.KODVOT 
Where a.MK03_TAHUN = YEAR(GETDATE()) AND A.KodKw = '" & strKodKW & "' and A.KodKO = '" & strKodKO & "' AND A.KODPTJ = '" & strKodPTj & "' AND A.KodKP  = '" & strKodKP & "' AND  RIGHT(A.KODVOT,2) <> '00' ORDER BY A.KODVOT"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)
            Return ds

        Catch ex As Exception

        End Try
    End Function

    Public Function fGetKO(ByVal strKodKW As String) As DataSet
        Try
            Dim strSql As String = "SELECT DISTINCT TOP 100 PERCENT dbo.MK_kodOperasi.KodKO, (dbo.MK_kodOperasi.KodKO + ' - ' + dbo.MK_kodOperasi.Butiran) as ButiranKO " &
                "FROM dbo.MK_kodOperasi INNER JOIN dbo.MK01_VotTahun ON dbo.MK_kodOperasi.KodKO = dbo.MK01_VotTahun.KodKO " &
                "where dbo.MK01_VotTahun.MK01_Tahun = YEAR(GETDATE()) and dbo.mk01_votTahun.KodKw = '" & strKodKW & "' ORDER BY dbo.MK_kodOperasi.Kodko"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)
            Return ds

        Catch ex As Exception

        End Try
    End Function

    Public Function fGetKP(ByVal strKodKW As String, ByVal strKodKO As String, ByVal strKodPTj As String) As DataSet
        Try

            Dim strSql As String = "SELECT DISTINCT TOP 100 PERCENT MK_KodProjek.KodProjek , (MK_KodProjek .KodProjek  + ' - ' + MK_KodProjek.Butiran ) as Butiran  
                    From MK_KodProjek INNER Join MK01_VotTahun ON MK_KodProjek.KodProjek  = MK01_VotTahun.KodKP
                    where MK01_VotTahun.MK01_Tahun = YEAR(GETDATE()) and MK01_VotTahun.KodKw = '" & strKodKW & "' and MK01_VotTahun.KodKO = '" & strKodKO & "' and MK01_VotTahun .KodPTJ = '" & strKodPTj & "' ORDER BY MK_KodProjek.KodProjek"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)
            Return ds

        Catch ex As Exception

        End Try
    End Function

    Public Function fInitDtTrans() As DataTable
        Try
            Dim dt As DataTable = New DataTable()
            dt = fCreateDtTrans()
            Dim dr As DataRow = Nothing
            dr = dt.NewRow()
            dr("KW") = 0
            dr("KO") = 0
            dr("PTj") = 0
            dr("KP") = 0
            dr("Vot") = 0
            dr("NoRuj") = String.Empty
            dr("Tarikh") = String.Empty
            dr("Jumlah") = "0.00"
            dr("BakiP") = "0.00"

            dt.Rows.Add(dr)
            Return dt
        Catch ex As Exception

        End Try

    End Function

    Public Function fGetCaraByr() As DataSet
        Try

            Dim strSql As String = "Select Kod,(Kod + ' - ' + Butiran) as Butiran from AP_CaraByr"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)
            Return ds

        Catch ex As Exception

        End Try
    End Function

    Public Function fGetKodCukai() As DataSet
        Try

            Dim strSql As String = "SELECT Kod,(kod + ' - ' + Butiran ) as Butiran FROM AP_KODCUKAI"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)
            Return ds

        Catch ex As Exception

        End Try
    End Function

    Public Function fGetMark(ByVal strNoGRN As String, ByVal strKod As String, ByVal strKodPres As String)
        Try
            Dim strMar As String
            Dim dbconn As New DBKewConn
            Dim ds As New DataSet
            Dim strSql As String = "select Markah  from PO18_PrestasiDT where  Kod = '" & strKod & "' and KodPrestasi = '" & strKodPres & "' and PO18_IDPen = (select PO18_IDPen from PO18_Prestasi where AP02_NoGRN = '" & strNoGRN & "' )"
            ds = dbconn.fSelectCommand(strSql)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strMar = ds.Tables(0).Rows(0)("Markah").ToString
                    Return strMar
                End If
            End If
            Return 6
        Catch ex As Exception

        End Try
    End Function

    Public Function fGetAmt(ByVal strNoDraf As String, ByRef decJumBaucar As Decimal, ByRef decJumBaucarDt As Decimal, ByRef decJumNominess As Decimal)
        Try
            Dim dbconn As New DBKewConn
            Dim strSql As String

            strSql = "SELECT TBL_A.AP04_JUMLAH as JumBaucar, TBL_B.AP04_JUMLAHDT as JumBaucarDt, TBL_C.AP05_JUMLAH as JumNominess
FROM (SELECT AP04_NODRAF, AP04_JUMLAH FROM AP04_BAUCAR) TBL_A 
Inner Join (SELECT AP04_NODRAF, (SUM(AP04_DEBIT)-SUM(AP04_KREDIT)) AS AP04_JUMLAHDT FROM AP04_BAUCARDT GROUP BY AP04_NODRAF) TBL_B ON TBL_A.AP04_NODRAF = TBL_B.AP04_NODRAF 
Inner Join (SELECT AP04_NODRAF, SUM(AP05_JUMLAH) AS AP05_JUMLAH FROM AP05_BAUCARNOMINEES GROUP BY AP04_NODRAF) TBL_C ON TBL_A.AP04_NODRAF = TBL_C.AP04_NODRAF 
WHERE TBL_A.AP04_NODRAF = '" & strNoDraf & "' "
            Dim ds1 As New DataSet
            ds1 = dbconn.fSelectCommand(strSql)
            If Not ds1 Is Nothing Then
                If ds1.Tables(0).Rows.Count > 0 Then
                    decJumBaucar = CDec(ds1.Tables(0).Rows(0).Item("JumBaucar").ToString)
                    decJumBaucarDt = CDec(ds1.Tables(0).Rows(0).Item("JumBaucarDt").ToString)
                    decJumNominess = CDec(ds1.Tables(0).Rows(0).Item("JumNominess").ToString)
                End If
            End If


        Catch ex As Exception

        End Try
    End Function

    Public Function fGetNoInd(ByVal strNoBK As String) As DataSet
        Try
            Dim strSql = "Select DISTINCT IND01_NOINDEN FROM IND04_TransaksiByrn with (Nolock)
WHERE AP04_NOBAUCAR = '" & strNoBK & "'"
            Dim dbconn As New DBKewConn
            Dim ds As New DataSet
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

    Public Function fGetAdv(ByVal strNoDraf As String) As DataSet
        Try

            Dim strSql = "Select ADV01_NoBaucar, ADV01_TkhBaucar, ADV01_StatusDok, ADV01_NoAdv, ADV01_AmaunCek FROM ADV01_PENDAHULUAN 
                WHERE ADV01_NoBaucarBD = '" & strNoDraf & "'"

            Dim dbconn As New DBKewConn
            Dim ds As New DataSet
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

    Public Function fGetPJM01(ByVal strNoDraf As String) As DataSet
        Try
            Dim strSql As String = "SELECT  PJM01_NoPinj, PJM01_NoPinjSem FROM PJM01_Daftar 
                WHERE PJM01_NoBaucer = '" & strNoDraf & "'"

            Dim dbconn As New DBKewConn
            Dim ds As New DataSet
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

    Public Function fGetCL01(ByVal strNoDraf As String) As DataSet
        Try
            Dim strSql As String = "SELECT  CL01_NoClm FROM CL01_Tuntutan WHERE CL01_NoBaucar = '" & strNoDraf & "'"

            Dim dbconn As New DBKewConn
            Dim ds As New DataSet
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

    Public Function fGetTransBaucar(ByVal intIdBaucar As String) As DataSet

        Try
            Dim strSql = "SELECT B.PO19_NoPt, B.KodKw, b.KodKO, B.KodPtj, B.KodKP, B.KodVot, B.AP04_CATATAN, B.AP04_DEBIT, B.AP04_NOBAUCAR 
                FROM AP04_BAUCAR A 
                INNER JOIN AP04_BAUCARDT B ON A.AP04_IdBaucar = B.AP04_IdBaucar 
                WHERE A.AP02_JENIS = 'J09' AND A.AP04_IdBaucar = " & intIdBaucar & " 
                AND B.AP04_KREDIT = 0
                GROUP BY B.PO19_NoPt,B.KodKw, b.KodKO, B.KodPtj, B.KodKP, B.KodVot, B.AP04_CATATAN, B.AP04_DEBIT, B.AP04_NOBAUCAR"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)
            Return ds

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

End Module
