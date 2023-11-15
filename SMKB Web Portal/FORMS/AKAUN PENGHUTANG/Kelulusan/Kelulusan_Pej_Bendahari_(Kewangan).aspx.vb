Imports System.Drawing
Imports System.Data.SqlClient
Imports System.IO
Public Class Kelulusan_Pej_Bendahari__Kewangan_
    Inherits System.Web.UI.Page

    Dim sLogBaru As String
    Dim sLogMedan As String
    Dim strNoStaf As String = UserInfo.strSesStaffID
    Dim sLogLevel As String = UserInfo.strUsrTahap
    Dim lsLogUsrIP As String = UserInfo.strUsrPcIP
    Dim lsLogUsrPC As String = UserInfo.strUsrPCName
    Private dbconnSMSM As New DBSMConn()
    Dim ds As New DataSet
    Dim ds1 As New DataSet
    Private countButiran As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Session("sesKodSubMenu") = Request.QueryString("KodSubMenu")
            fLoadKW()
            fLoadKO2()
            fLoadPTj2()
            fLoadKP2()
            fLoadVot2()

            fBindDdlFilStat()
            fLoadLst()

            divList.Visible = True
            divDetail.Visible = False
        End If
    End Sub
    Private Sub fLoadKW()
        Try

            Dim strSql As String = "Select KodKw,Butiran,(KodKw + ' - ' + Butiran ) as ButiranKW from MK_Kw order by KodKw"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)

            ViewState("dsKW") = ds

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fLoadKO2()
        Try
            Dim strSql As String = "SELECT DISTINCT TOP 100 PERCENT dbo.MK_kodOperasi.KodKO, (dbo.MK_kodOperasi.KodKO + ' - ' + dbo.MK_kodOperasi.Butiran) as Butiran " &
                "FROM dbo.MK_kodOperasi INNER JOIN dbo.MK01_VotTahun ON dbo.MK_kodOperasi.KodKO = dbo.MK01_VotTahun.KodKO " &
                "where dbo.MK01_VotTahun.MK01_Tahun = YEAR(GETDATE()) ORDER BY dbo.MK_kodOperasi.Kodko"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)
            ViewState("dsKO") = ds

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fLoadPTj2()
        Try

            Dim strSql As String = "SELECT DISTINCT   MK_PTJ.KodPTJ  , (MK_PTJ.KodPTJ   + ' - ' + MK_PTJ.Butiran  ) as Butiran  
                    From MK_PTJ INNER Join MK01_VotTahun ON MK_PTJ.KodPTJ   = MK01_VotTahun.KodPTJ   
                    where MK01_VotTahun.MK01_Tahun = YEAR(GETDATE()) and MK_PTJ.KodPTJ <> '-' and MK_PTJ.status = 1
ORDER BY MK_PTJ.KodPTJ "

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)
            ViewState("dsPTj") = ds

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fLoadKP2()
        Try

            Dim strSql As String = "SELECT DISTINCT TOP 100 PERCENT MK_KodProjek.KodProjek as KodKP, (MK_KodProjek .KodProjek  + ' - ' + MK_KodProjek.Butiran ) as Butiran  
                    From MK_KodProjek INNER Join MK01_VotTahun ON MK_KodProjek.KodProjek  = MK01_VotTahun.KodKP
                    where MK01_VotTahun.MK01_Tahun = YEAR(GETDATE())  ORDER BY MK_KodProjek.KodProjek"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)
            ViewState("dsKP") = ds

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fLoadVot2()
        Try

            Dim strSql As String = "SELECT DISTINCT A.KodVot, (A.KODVOT + ' - ' + B.BUTIRAN) as Butiran FROM MK03_AmTahun A INNER JOIN MK_VOT B ON A.KODVOT = B.KODVOT 
Where a.MK03_TAHUN = YEAR(GETDATE()) AND  RIGHT(A.KODVOT,2) <> '00' ORDER BY A.KODVOT"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)
            ViewState("dsVot") = ds


        Catch ex As Exception

        End Try
    End Sub
    Private Sub fBindDdlFilStat()
        Dim dbconn As New DBKewConn
        Try
            Dim strSql As String = "select KodStatus, Butiran from AR_StatusDok where KodStatus in (01,03)"

            Using ds = dbconn.fSelectCommand(strSql)
                ddlFilStat.DataSource = ds
                ddlFilStat.DataTextField = "Butiran"
                ddlFilStat.DataValueField = "KodStatus"
                ddlFilStat.DataBind()

                ddlFilStat.Items.Insert(0, New ListItem("- KESELURUHAN -", "0"))
                ddlFilStat.SelectedValue = "01"
            End Using

        Catch ex As Exception

        End Try
    End Sub


    Private Sub fLoadLst()

        Dim intRec As Integer

        Dim strFilter As String
        Dim strStat As String

        fClearGvLst()
        Try

            If ddlCarian.SelectedValue = 1 Then
                strFilter = ""
            ElseIf ddlCarian.SelectedValue = 2 Then
                strFilter = " AND AR01_NoBilSem ='" & Trim(txtCarian.Text.TrimEnd) & "'"
            End If

            If ddlFilStat.SelectedValue = 0 Then
                strStat = "01,03"
            Else
                strStat = ddlFilStat.SelectedValue
            End If

            Dim strSql As String = "select a.AR01_IdBil, a.AR01_NoBilSem,AR01_NoBil, Convert(varchar, a.AR01_TkhMohon, 103) As AR01_TkhMohon, a.AR01_Tujuan, a.AR01_NamaPenerima, AR01_Kategori, (select Butiran  from MK_KategoriPenerima where Kod = a.AR01_Kategori ) as ButKat, a.ar01_Jumlah, AR01_TkhMohon  as Tarikh_Bil, ar01_statusdok, (Select B.Butiran  from AR_StatusDok B where B.KodStatus = A.AR01_StatusDok) as ButStatus
from AR01_Bil a
where ar01_statusdok in (" & strStat & ") and ar01_jenis='01' " & strFilter

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim dt As New DataTable
                    dt = ds.Tables(0)
                    For Each dtRow As DataRow In dt.Rows
                        If dtRow("AR01_NoBil") = dtRow("AR01_NoBilSem") Then
                            dtRow("AR01_NoBil") = "-"
                        End If
                    Next

                    gvLst.DataSource = ds.Tables(0)
                    gvLst.DataBind()

                    intRec = ds.Tables(0).Rows.Count
                End If
            End If

            lblJumRekod.InnerText = intRec

        Catch ex As Exception

        End Try
    End Sub
    Private Sub fBindGvTransInvDt(ByVal strNoInv As String)
        Try
            Dim dbconn As New DBKewConn()
            Dim dtTkhMohon As Date
            Dim strSqlInv = $"Select AR01_NoBilSem, AR01_TkhMohon,AR01_KODPTJMOHON, AR01_NOSTAF, AR01_Jenis,AR01_KODBANK,AR01_NORUJUKAN, AR01_JenisUrusniaga,AR01_ALMT1,AR01_ALMT2,
                            AR01_KATEGORI,AR01_NAMAPENERIMA,kodnegara, kodnegeri,AR01_IDPENERIMA, AR01_TUJUAN, AR01_BANDAR,AR01_POSKOD, AR01_TkhKonDari,AR01_TkhKonHingga,AR01_NOTEL, 
                            AR01_NOFAKS, AR01_TempohKontrak,AR01_TkhPeringatan1 ,AR01_TkhPeringatan2 , AR01_TkhPeringatan3, AR01_TkhLulus ,AR01_Emel,AR01_UtkPerhatian from AR01_Bil where AR01_NoBilSem='{strNoInv}';"
            Dim strSqlInvDt = $"select KodKw,KodKO,KodPTJ,KodKP,KodVot,AR01_Perkara,AR01_Kuantiti,AR01_kadarHarga,AR01_Jumlah from AR01_BilDt where AR01_NoBilSem='{strNoInv}';"
            Dim strSqlInvStat = $"Select ar06_tarikh from AR06_StatusDok Where ar06_statusdok='01' and ar06_nobilsem='{strNoInv}';"

            'Load Permohonan Invois
            Dim ds = dbconn.fSelectCommand(strSqlInv + strSqlInvDt + strSqlInvStat)

            Using MohonDt = ds.Tables(1)
                gvTrans.DataSource = MohonDt
                gvTrans.DataBind()
            End Using

            Using tkhStatusDokDt = ds.Tables(2)
                If tkhStatusDokDt.Rows.Count > 0 Then
                    Dim Tkh As Date = tkhStatusDokDt.Rows(0)("AR06_Tarikh")

                End If
            End Using

        Catch ex As Exception

        End Try

    End Sub
    Function ListBil()
        Try
            Dim strsql As String
            Dim strKodPTJ As String = Trim(Session("ssusrKodPTj")).TrimEnd
            strsql = "Select * from AR01_Bil Where (ar01_statusdok='01') and ar01_jenis='01' and ar01_kodptjmohon='" & strKodPTJ & "'"

        Catch ex As Exception

        End Try
    End Function

    Private Function fSetDtTrans() As DataTable
        Try
            Dim dt As New DataTable()
            dt.Columns.AddRange(New DataColumn() {
                                New DataColumn("AR01_BilDtID", GetType(Integer)),
                                New DataColumn("AR01_Bil", GetType(Integer)),
                                New DataColumn("AR01_NoBilSem", GetType(Integer)),
                                New DataColumn("KodKw", GetType(String)),
                                New DataColumn("KodKO", GetType(String)),
                                New DataColumn("kodPTJ", GetType(String)),
                                New DataColumn("kodKP", GetType(String)),
                                New DataColumn("kodVot", GetType(String)),
                                New DataColumn("AR01_Perkara", GetType(String)),
                                New DataColumn("AR01_Kuantiti", GetType(Integer)),
                                New DataColumn("AR01_KadarHarga", GetType(Decimal)),
                                New DataColumn("AR01_Jumlah", GetType(Decimal))})

            Return dt
        Catch ex As Exception

        End Try
    End Function

    ''' <summary>
    ''' Jana No.Invois
    ''' </summary>
    ''' <returns></returns>
    Function fGetID_Bil() As String
        Try
            Dim strSql As String
            Dim strIdx As String
            Dim intLastIdx As Integer
            Dim strCol As String
            Dim strTahun As String = Now.Year
            Dim strKodModul As String
            Dim strPrefix As String
            Dim strButiran As String = "Max No Sem Kewangan AR"

            strSql = "select * from mk_noakhir where tahun = '" & strTahun & "' and kodmodul='AR' and prefix='K'"
            strCol = "NoAkhir"
            strKodModul = "AR"
            strPrefix = "K"

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
    Private Sub fClear()
        Try

            gvLst.DataSource = New List(Of String)
            gvLst.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fClearGvLst()
        gvLst.DataSource = New List(Of String)
        gvLst.DataBind()
    End Sub

    Private Sub fClearGvTrans()
        gvTrans.DataSource = New List(Of String)
        gvTrans.DataBind()
    End Sub

    'Private Sub fClearGvLamp()
    '    gvLamp.DataSource = New List(Of String)
    '    gvLamp.DataBind()
    'End Sub

    Dim strTotJumlah2 As String
    Dim decJumlah2 As Decimal

    Private Function fLulus()
        Dim blnSuccess As Boolean = True
        Dim strNoStaf As String = Trim(lblNoStafPel.Text.TrimEnd)
        Dim intIdBil As Integer = CInt(hidIdBil.Value)
        Dim dbconn As New DBKewConn()
        Dim strNoBil As String
        Dim strKodJen, strKodJenLanjut As String
        Dim strKodKW2, strKodKO2, strKodKP2 As String

        Try
            If String.IsNullOrEmpty(strNoStaf) Then
                blnSuccess = False
                Exit Try
            End If

            Dim strSql As String
            Dim strNoInvSem As String = Trim(lblNoInvSem.Text.TrimEnd)
            Dim strTkhToday As String = Now.ToString("yyyy-MM-dd")
            Dim strTahun As String = Now.Year
            Dim strBank As String = Trim(lblKodBank.Text.TrimEnd)
            Dim strTujuan As String = Trim(lblTujuan.Text.TrimEnd)


            Dim strStatDok = "03"
            Dim decTotJum As Decimal

            For Each gvRow As GridViewRow In gvTrans.Rows
                Dim decJumTrans As Decimal = CDec(CType(gvRow.FindControl("lblJumlah"), Label).Text)
                decTotJum += decJumTrans
            Next

            Dim strButiranNoAkhir As String = "Max No Kewangan AR"
            Dim strKodModul = "AR"
            Dim strPrefix = "K"

            strNoBil = fGetNo(strTahun, strKodModul, strPrefix, strButiranNoAkhir)

            strSql = "UPDATE AR01_Bil Set AR01_NOBIL = @NoBil, AR01_JUMLAH = @Jumlah,
                              AR01_JUMBLMBYR = @Jumlahblmbyr, AR01_STATUSDOK = @StatDok , AR01_KODBANK = @KodBank, 
AR01_TkhLulus = @TkhLulus, AR01_NoStafLulus = @NoStaf
                              WHERE ar01_nobilsem = @NoBilSem;"

            Dim paramSql() As SqlParameter

            paramSql =
               {
               New SqlParameter("@NoBilSem", strNoInvSem),
               New SqlParameter("@NOBIL", strNoBil),
               New SqlParameter("@Jumlah", decTotJum),
               New SqlParameter("@Jumlahblmbyr", decTotJum),
               New SqlParameter("@StatDok", strStatDok),
               New SqlParameter("@KodBank", strBank),
               New SqlParameter("@TkhLulus", strTkhToday),
               New SqlParameter("@NoStaf", strNoStaf)
               }

            dbconn.sConnBeginTrans()
            If Not dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                blnSuccess = False
                Exit Try
            Else
                'AUDIT LOG
                sLogMedan = "AR01_NOBIL| AR01_JUMLAH| AR01_JUMBLMBYR| AR01_STATUSDOK| AR01_KODBANK| AR01_TkhLulus| AR01_NoStafLulus| ar01_nobilsem"
                sLogBaru = strNoBil & "|" & decTotJum & "|" & decTotJum & "|" & strStatDok & "|" & strBank & "|" & strTkhToday & "|" & strNoStaf & ""

                strSql = "insert into MK_AuditLog (UserID, UserGroup, UserUbah, SubMenu, Keterangan, InfoTable, RefKey, RefNo, InfoMedan, InfoBaru, " _
    & " InfoLama, UserIP, PCName) " _
    & " values (@UserID,@UserGroup,@UserUbah,@SubMenu,@Keterangan,@InfoTable,@RefKey, @RefNo,@InfoMedan,@InfoBaru,@InfoLama, " _
    & " @UserIP,@PCName)"

                Dim paramSqlLog() As SqlParameter = {
                    New SqlParameter("@UserID", strNoStaf),
                    New SqlParameter("@UserGroup", sLogLevel),
                    New SqlParameter("@UserUbah", "-"),
                     New SqlParameter("@SubMenu", Session("sesKodSubMenu")),
                    New SqlParameter("@Keterangan", "UPDATE"),
                    New SqlParameter("@InfoTable", "AR01_Bil"),
                    New SqlParameter("@RefKey", "-"),
                    New SqlParameter("@RefNo", "-"),
                    New SqlParameter("@InfoMedan", sLogMedan),
                    New SqlParameter("@InfoBaru", sLogBaru),
                    New SqlParameter("@InfoLama", "-"),
                    New SqlParameter("@UserIP", lsLogUsrIP),
                    New SqlParameter("@PCName", lsLogUsrPC)
                }

                If Not dbconn.fInsertCommand(strSql, paramSqlLog) > 0 Then
                    blnSuccess = False
                    Exit Try
                End If
            End If

            strSql = "delete from AR01_BilDt where AR01_NoBilSem = '" & strNoInvSem & "'"
            If Not dbconn.fUpdateCommand(strSql) > 0 Then
                blnSuccess = False
                Exit Try
            Else
                'AUDIT LOG
                sLogMedan = "AR01_NoBilSem"
                sLogBaru = strNoInvSem

                strSql = "insert into MK_AuditLog (UserID, UserGroup, UserUbah, SubMenu, Keterangan, InfoTable, RefKey, RefNo, InfoMedan, InfoBaru, " _
    & " InfoLama, UserIP, PCName) " _
    & " values (@UserID,@UserGroup,@UserUbah,@SubMenu,@Keterangan,@InfoTable,@RefKey, @RefNo,@InfoMedan,@InfoBaru,@InfoLama, " _
    & " @UserIP,@PCName)"

                Dim paramSqlLog() As SqlParameter = {
                    New SqlParameter("@UserID", strNoStaf),
                    New SqlParameter("@UserGroup", sLogLevel),
                    New SqlParameter("@UserUbah", "-"),
                     New SqlParameter("@SubMenu", Session("sesKodSubMenu")),
                    New SqlParameter("@Keterangan", "DELETE"),
                    New SqlParameter("@InfoTable", "AR01_BilDt"),
                    New SqlParameter("@RefKey", "-"),
                    New SqlParameter("@RefNo", "-"),
                    New SqlParameter("@InfoMedan", sLogMedan),
                    New SqlParameter("@InfoBaru", sLogBaru),
                    New SqlParameter("@InfoLama", "-"),
                    New SqlParameter("@UserIP", lsLogUsrIP),
                    New SqlParameter("@PCName", lsLogUsrPC)
                }

                If Not dbconn.fInsertCommand(strSql, paramSqlLog) > 0 Then
                    blnSuccess = False
                    Exit Try
                End If
            End If

            Dim KodDok As String = "BIL"
            Dim KODAP As String = "-"
            Dim Status As String = "0"
            'Dim db As Double = decTotJum
            ' Dim Kt As Double
            Dim BulanTran As String = Now.Month.ToString

            Dim intBil As Integer
            Dim strKodKW, strKodKO, strPTJ, strKodKP, strKodVot, strPerkara As String
            Dim decKuantiti, decKdrHrg, decJum As Decimal
            For Each gvTransInvrow As GridViewRow In gvTrans.Rows
                intBil = intBil + 1
                strKodKW = TryCast(gvTransInvrow.FindControl("ddlKW"), DropDownList).SelectedValue
                strKodKO = TryCast(gvTransInvrow.FindControl("ddlKO"), DropDownList).SelectedValue
                strPTJ = TryCast(gvTransInvrow.FindControl("ddlPTj"), DropDownList).SelectedValue
                strKodKP = TryCast(gvTransInvrow.FindControl("ddlKP"), DropDownList).SelectedValue
                strKodVot = TryCast(gvTransInvrow.FindControl("ddlVot"), DropDownList).SelectedValue
                strPerkara = TryCast(gvTransInvrow.FindControl("txtPerkara"), TextBox).Text
                decKuantiti = CDec(TryCast(gvTransInvrow.FindControl("txtKuantiti"), TextBox).Text)
                decKdrHrg = CDec(TryCast(gvTransInvrow.FindControl("txtHarga"), TextBox).Text)
                decJum = CDec(TryCast(gvTransInvrow.FindControl("lblJumlah"), Label).Text)

                'insert into AR01_BilDT
                strSql = " INSERT INTO AR01_BilDT(AR01_IdBil, AR01_NoBilSem,AR01_NoBil,AR01_Bil,KodKw,KodKO,KodPTJ,KodKP,KodVot,AR01_Perkara,AR01_Kuantiti,AR01_kadarHarga,AR01_Jumlah)" &
                             "values (@IdBil,@NOBILSEM, @NOBIL,@BIL,@kodKw, @KodKO, @KodPTJ, @KodKP, @KodVot, @Perkara, @Kuantiti,@kadarHarga,@Jumlah)"

                Dim paramSql2() As SqlParameter =
                    {
                    New SqlParameter("@IdBil", intIdBil),
                     New SqlParameter("@NOBILSEM", strNoInvSem),
                     New SqlParameter("@NOBIL", strNoBil),
                     New SqlParameter("@BIL", intBil),
                     New SqlParameter("@kodKw", strKodKW),
                     New SqlParameter("@KodKO", strKodKO),
                     New SqlParameter("@KodPTJ", strPTJ),
                     New SqlParameter("@KodKP", strKodKP),
                     New SqlParameter("@KodVot", strKodVot),
                     New SqlParameter("@Perkara", strPerkara),
                     New SqlParameter("@Kuantiti", decKuantiti),
                     New SqlParameter("@kadarHarga", decKdrHrg),
                     New SqlParameter("@Jumlah", decJum)
                    }

                If Not dbconn.fInsertCommand(strSql, paramSql2) > 0 Then
                    blnSuccess = False
                    Exit Try
                Else
                    'AUDIT LOG
                    sLogMedan = "AR01_IdBil| AR01_NoBilSem|AR01_NoBil|AR01_Bil|KodKw|KodKO|KodPTJ|KodKP|KodVot|AR01_Perkara|AR01_Kuantiti|AR01_kadarHarga|AR01_Jumlah"
                    sLogBaru = intIdBil & "|" & strNoInvSem & "|" & strNoBil & "|" & intBil & "|" & strKodKW & "|" & strKodKO & "|" & strPTJ & "|" & strKodKP & "|" & strKodVot & "|" & strPerkara & "|" & decKuantiti & "|" & decKdrHrg & "|" & decJum & ""

                    strSql = "insert into MK_AuditLog (UserID, UserGroup, UserUbah, SubMenu, Keterangan, InfoTable, RefKey, RefNo, InfoMedan, InfoBaru, " _
        & " InfoLama, UserIP, PCName) " _
        & " values (@UserID,@UserGroup,@UserUbah,@SubMenu,@Keterangan,@InfoTable,@RefKey, @RefNo,@InfoMedan,@InfoBaru,@InfoLama, " _
        & " @UserIP,@PCName)"

                    Dim paramSqlLog() As SqlParameter = {
                        New SqlParameter("@UserID", strNoStaf),
                        New SqlParameter("@UserGroup", sLogLevel),
                        New SqlParameter("@UserUbah", "-"),
                         New SqlParameter("@SubMenu", Session("sesKodSubMenu")),
                        New SqlParameter("@Keterangan", "INSERT"),
                        New SqlParameter("@InfoTable", "AR01_BilDT"),
                        New SqlParameter("@RefKey", "-"),
                        New SqlParameter("@RefNo", "-"),
                        New SqlParameter("@InfoMedan", sLogMedan),
                        New SqlParameter("@InfoBaru", sLogBaru),
                        New SqlParameter("@InfoLama", "-"),
                        New SqlParameter("@UserIP", lsLogUsrIP),
                        New SqlParameter("@PCName", lsLogUsrPC)
                    }

                    If Not dbconn.fInsertCommand(strSql, paramSqlLog) > 0 Then
                        blnSuccess = False
                        Exit Try
                    End If
                End If


                'posting ke mk06_transaksi

                fGetKodJen(strKodVot, strKodJen, strKodJenLanjut)

                strSql = "INSERT INTO mk06_transaksi(mk06_tkhtran,mk06_rujukan,mk06_nodok,mk06_butiran,koddok,mk06_kodap,mk06_bil,kodkw,KodKO,kodptj,KodKP,kodvot,kodjen,kodjenlanjut,mk06_debit,mk06_kredit,mk06_bulan,mk06_tahun,mk06_status)
                            VALUES (@dtTkhToday,@Rujukan,@NoDok,@mk06_butiran,@KodDok,@KODAP,@BIL,@kodKw,@KodKO,@KodPTJ,@KodKP,@KodVot,@kodJen,@KodJenLanjut,@db,@Kt,@BulanTran,@Tahun,@Status)"

                paramSql =
                    {
                     New SqlParameter("@dtTkhToday", strTkhToday),
                     New SqlParameter("@Rujukan", strNoBil),
                     New SqlParameter("@NoDok", strNoBil),
                     New SqlParameter("@mk06_butiran", strPerkara),
                     New SqlParameter("@KodDok", KodDok),
                     New SqlParameter("@KODAP", KODAP),
                     New SqlParameter("@BIL", intBil),
                     New SqlParameter("@kodKw", strKodKW),
                     New SqlParameter("@KodKO", strKodKO),
                     New SqlParameter("@KodPTJ", strPTJ),
                     New SqlParameter("@KodKP", strKodKP),
                     New SqlParameter("@KodVot", strKodVot),
                     New SqlParameter("@kodJen", strKodJen),
                     New SqlParameter("@KodJenLanjut", strKodJenLanjut),
                     New SqlParameter("@db", 0.00),
                     New SqlParameter("@Kt", decJum),
                     New SqlParameter("@BulanTran", BulanTran),
                     New SqlParameter("@Tahun", strTahun),
                     New SqlParameter("@Status", Status)
                    }

                If Not dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                    blnSuccess = False
                    Exit Try
                Else
                    'AUDIT LOG
                    sLogMedan = "mk06_tkhtran|mk06_rujukan|mk06_nodok|mk06_butiran|koddok|mk06_kodap|mk06_bil|kodkw|KodKO|kodptj|KodKP|kodvot|kodjen|kodjenlanjut|mk06_debit|mk06_kredit|mk06_bulan|mk06_tahun|mk06_status"
                    sLogBaru = strTkhToday & "|" & strNoBil & "|" & strNoBil & "|" & strTujuan & "|" & KodDok & "|" & KODAP & "|" & intBil & "|" & strKodKW & "|" & strKodKO & "|" & strPTJ & "|" & strKodKP & "|" & strKodVot & "|" & strKodJen & "|" & strKodJenLanjut & "|" & decJum & "|0.00|" & BulanTran & "|" & strTahun & "|" & Status & ""

                    strSql = "insert into MK_AuditLog (UserID, UserGroup, UserUbah, SubMenu, Keterangan, InfoTable, RefKey, RefNo, InfoMedan, InfoBaru, " _
        & " InfoLama, UserIP, PCName) " _
        & " values (@UserID,@UserGroup,@UserUbah,@SubMenu,@Keterangan,@InfoTable,@RefKey, @RefNo,@InfoMedan,@InfoBaru,@InfoLama, " _
        & " @UserIP,@PCName)"

                    Dim paramSqlLog() As SqlParameter = {
                        New SqlParameter("@UserID", strNoStaf),
                        New SqlParameter("@UserGroup", sLogLevel),
                        New SqlParameter("@UserUbah", "-"),
                         New SqlParameter("@SubMenu", Session("sesKodSubMenu")),
                        New SqlParameter("@Keterangan", "INSERT"),
                        New SqlParameter("@InfoTable", "mk06_transaksi"),
                        New SqlParameter("@RefKey", "-"),
                        New SqlParameter("@RefNo", "-"),
                        New SqlParameter("@InfoMedan", sLogMedan),
                        New SqlParameter("@InfoBaru", sLogBaru),
                        New SqlParameter("@InfoLama", "-"),
                        New SqlParameter("@UserIP", lsLogUsrIP),
                        New SqlParameter("@PCName", lsLogUsrPC)
                    }

                    If Not dbconn.fInsertCommand(strSql, paramSqlLog) > 0 Then
                        blnSuccess = False
                        Exit Try
                    End If
                End If

                strKodKW2 = strKodKW
                strKodKO2 = strKodKO
                strKodKP2 = strKodKP
            Next

            'posting ke mk06_transaksi
            ' Dim strKodJen, strKodJenLanjut As String
            intBil = intBil + 1
            strPTJ = "50000"
            strKodVot = "71901"
            fGetKodJen(strKodVot, strKodJen, strKodJenLanjut)

            strSql = "INSERT INTO mk06_transaksi(mk06_tkhtran,mk06_rujukan,mk06_nodok,mk06_butiran,koddok,mk06_kodap,mk06_bil,kodkw,KodKO,kodptj,KodKP,kodvot,kodjen,kodjenlanjut,mk06_debit,mk06_kredit,mk06_bulan,mk06_tahun,mk06_status)
                            VALUES (@dtTkhToday,@Rujukan,@NoDok,@mk06_butiran,@KodDok,@KODAP,@BIL,@kodKw,@KodKO,@KodPTJ,@KodKP,@KodVot,@kodJen,@KodJenLanjut,@db,@Kt,@BulanTran,@Tahun,@Status)"

            paramSql =
                    {
                     New SqlParameter("@dtTkhToday", strTkhToday),
                     New SqlParameter("@Rujukan", strNoBil),
                     New SqlParameter("@NoDok", strNoBil),
                     New SqlParameter("@mk06_butiran", strTujuan),
                     New SqlParameter("@KodDok", KodDok),
                     New SqlParameter("@KODAP", KODAP),
                     New SqlParameter("@BIL", intBil),
                     New SqlParameter("@kodKw", strKodKW2),
                     New SqlParameter("@KodKO", strKodKO2),
                     New SqlParameter("@KodPTJ", strPTJ),
                     New SqlParameter("@KodKP", strKodKP2),
                     New SqlParameter("@KodVot", strKodVot),
                     New SqlParameter("@kodJen", strKodJen),
                     New SqlParameter("@KodJenLanjut", strKodJenLanjut),
                     New SqlParameter("@db", decTotJum),
                     New SqlParameter("@Kt", 0.00),
                     New SqlParameter("@BulanTran", BulanTran),
                     New SqlParameter("@Tahun", strTahun),
                     New SqlParameter("@Status", Status)
                    }

            If Not dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                blnSuccess = False
                Exit Try
            Else
                'AUDIT LOG
                sLogMedan = "mk06_tkhtran|mk06_rujukan|mk06_nodok|mk06_butiran|koddok|mk06_kodap|mk06_bil|kodkw|KodKO|kodptj|KodKP|kodvot|kodjen|kodjenlanjut|mk06_debit|mk06_kredit|mk06_bulan|mk06_tahun|mk06_status"
                sLogBaru = strTkhToday & "|" & strNoBil & "|" & strNoBil & "|" & strTujuan & "|" & KodDok & "|" & KODAP & "|" & intBil & "|" & strKodKW & "|" & strKodKO & "|" & strPTJ & "|" & strKodKP & "|" & strKodVot & "|" & strKodJen & "|" & strKodJenLanjut & "|" & decJum & "|0.00|" & BulanTran & "|" & strTahun & "|" & Status & ""

                strSql = "insert into MK_AuditLog (UserID, UserGroup, UserUbah, SubMenu, Keterangan, InfoTable, RefKey, RefNo, InfoMedan, InfoBaru, " _
        & " InfoLama, UserIP, PCName) " _
        & " values (@UserID,@UserGroup,@UserUbah,@SubMenu,@Keterangan,@InfoTable,@RefKey, @RefNo,@InfoMedan,@InfoBaru,@InfoLama, " _
        & " @UserIP,@PCName)"

                Dim paramSqlLog() As SqlParameter = {
                        New SqlParameter("@UserID", strNoStaf),
                        New SqlParameter("@UserGroup", sLogLevel),
                        New SqlParameter("@UserUbah", "-"),
                         New SqlParameter("@SubMenu", Session("sesKodSubMenu")),
                        New SqlParameter("@Keterangan", "INSERT"),
                        New SqlParameter("@InfoTable", "mk06_transaksi"),
                        New SqlParameter("@RefKey", "-"),
                        New SqlParameter("@RefNo", "-"),
                        New SqlParameter("@InfoMedan", sLogMedan),
                        New SqlParameter("@InfoBaru", sLogBaru),
                        New SqlParameter("@InfoLama", "-"),
                        New SqlParameter("@UserIP", lsLogUsrIP),
                        New SqlParameter("@PCName", lsLogUsrPC)
                    }

                If Not dbconn.fInsertCommand(strSql, paramSqlLog) > 0 Then
                    blnSuccess = False
                    Exit Try
                End If
            End If

            'Insert Into AR06_STATUSDOK
            strSql = "INSERT INTO AR06_STATUSDOK(AR06_NOBILSEM,AR06_NoBil, AR06_StatusDok,AR06_Tarikh,AR06_Ulasan,AR06_NoStaf)" &
                             "VALUES (@NOBILSEM,@NOBIL, @STATUSDOK,@TARIKH,@ULASAN, @NOSTAF)"

            paramSql =
                {
                New SqlParameter("@NOBILSEM", strNoInvSem),
                 New SqlParameter("@NOBIL", strNoBil),
                 New SqlParameter("@STATUSDOK", strStatDok),
                 New SqlParameter("@TARIKH", strTkhToday),
                 New SqlParameter("@ULASAN", " - "),
                 New SqlParameter("@NOSTAF", strNoStaf)
                }

            If Not dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                blnSuccess = False
                Exit Try
            End If

        Catch ex As Exception
            blnSuccess = False
        End Try

        If blnSuccess Then
            txtNoInv.Text = strNoBil
            dbconn.sConnCommitTrans()
            Return True
        Else
            dbconn.sConnRollbackTrans()
            Return False
        End If

    End Function
    Private Function fXLulus()
        Dim strSql As String
        Dim blnSuccess As Boolean = True
        Dim strNoStaf As String = Trim(Session("ssusrID")).TrimEnd
        Dim strTkhToday As String = Now.ToString("yyyy-MM-dd")
        Dim strTahun As String = Now.Year
        Dim strStatDok = "05"
        Dim strNoBilSem As String = Trim(lblNoInvSem.Text.TrimEnd)
        Dim strUlasan = Trim(txtUlasan.Text.Trim)
        Dim paramSql() As SqlParameter
        Dim dbconn As New DBKewConn()
        Try

            dbconn.sConnBeginTrans()

            strSql = "update AR01_Bil set AR01_StatusDok = @STATUSDOK where AR01_NoBilSem = @NOBILSEM"
            paramSql =
                {
                New SqlParameter("@NOBILSEM", strNoBilSem),
                New SqlParameter("@STATUSDOK", strStatDok)
                }

            If Not dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                blnSuccess = False
                Exit Try
            Else
                'AUDIT LOG
                sLogMedan = "AR01_StatusDok|AR01_NoBilSem"
                sLogBaru = strNoBilSem & "|" & strStatDok & ""

                strSql = "insert into MK_AuditLog (UserID, UserGroup, UserUbah, SubMenu, Keterangan, InfoTable, RefKey, RefNo, InfoMedan, InfoBaru, " _
    & " InfoLama, UserIP, PCName) " _
    & " values (@UserID,@UserGroup,@UserUbah,@SubMenu,@Keterangan,@InfoTable,@RefKey, @RefNo,@InfoMedan,@InfoBaru,@InfoLama, " _
    & " @UserIP,@PCName)"

                Dim paramSqlLog() As SqlParameter = {
                    New SqlParameter("@UserID", strNoStaf),
                    New SqlParameter("@UserGroup", sLogLevel),
                    New SqlParameter("@UserUbah", "-"),
                     New SqlParameter("@SubMenu", Session("sesKodSubMenu")),
                    New SqlParameter("@Keterangan", "UPDATE"),
                    New SqlParameter("@InfoTable", "AR01_Bil"),
                    New SqlParameter("@RefKey", "-"),
                    New SqlParameter("@RefNo", "-"),
                    New SqlParameter("@InfoMedan", sLogMedan),
                    New SqlParameter("@InfoBaru", sLogBaru),
                    New SqlParameter("@InfoLama", "-"),
                    New SqlParameter("@UserIP", lsLogUsrIP),
                    New SqlParameter("@PCName", lsLogUsrPC)
                }

                If Not dbconn.fInsertCommand(strSql, paramSqlLog) > 0 Then
                    blnSuccess = False
                    Exit Try
                End If
            End If

            'Insert Into AR06_STATUSDOK
            strSql = "INSERT INTO AR06_STATUSDOK(AR06_NOBILSEM,AR06_NoBil, AR06_StatusDok,AR06_Tarikh,AR06_Ulasan, AR06_NoStaf)" &
                             "VALUES (@NOBILSEM,@NOBIL, @STATUSDOK,@TARIKH,@ULASAN, @NOSTAF)"

            paramSql =
                {
                New SqlParameter("@NOBILSEM", strNoBilSem),
                 New SqlParameter("@NOBIL", strNoBilSem),
                 New SqlParameter("@STATUSDOK", strStatDok),
                 New SqlParameter("@TARIKH", strTkhToday),
                 New SqlParameter("@ULASAN", strUlasan),
                 New SqlParameter("@NOSTAF", strNoStaf)
                }

            If Not dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                blnSuccess = False
                Exit Try
            End If

        Catch ex As Exception
            blnSuccess = False
        End Try

        If blnSuccess = True Then
            dbconn.sConnCommitTrans()
            Return True
        ElseIf blnSuccess = False Then
            dbconn.sConnRollbackTrans()
            Return False
        End If
    End Function



    Private Function fLoadKO(ByVal strKodKW As String) As DataSet
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
    Private Function fLoadPTj(ByVal strKodKW As String, ByVal strKodKO As String) As DataSet
        Try


            Dim strSql As String = "SELECT DISTINCT   MK_PTJ.KodPTJ  , (MK_PTJ.KodPTJ   + ' - ' + MK_PTJ.Butiran  ) as ButiranPTj  
                    From MK_PTJ INNER Join MK01_VotTahun ON MK_PTJ.KodPTJ   = MK01_VotTahun.KodPTJ   
                    where MK01_VotTahun.MK01_Tahun = YEAR(GETDATE()) and MK01_VotTahun.KodKw = '" & strKodKW & "' and MK01_VotTahun.KodKO = '" & strKodKO & "' and MK_PTJ.KodPTJ <> '-' and MK_PTJ.KodKategoriPTJ = 'P' and MK_PTJ.status = 1 
ORDER BY MK_PTJ.KodPTJ "

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)

            Return ds

        Catch ex As Exception

        End Try
    End Function

    Private Function fLoadKP(ByVal strKodKW As String, ByVal strKodKO As String, ByVal strKodPTj As String) As DataSet
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

    Private Function fLoadVot(ByVal strKodKW As String, ByVal strKodKO As String, ByVal strKodPTj As String, ByVal strKodKP As String) As DataSet
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
    Protected Sub ddlKW_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try

            Dim gvr As GridViewRow = CType(((CType(sender, Control)).NamingContainer), GridViewRow)
            Dim ddlKW As DropDownList = CType(gvr.FindControl("ddlKW"), DropDownList)
            Dim strKodKW As String = ddlKW.SelectedItem.Value

            Dim ddlKO = DirectCast(gvr.FindControl("ddlKO"), DropDownList)
            ddlKO.DataSource = fLoadKO(strKodKW)
            ddlKO.DataTextField = "ButiranKO"
            ddlKO.DataValueField = "KodKO"
            ddlKO.DataBind()

            ddlKO.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlKO.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub ddlKO_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try

            Dim gvr As GridViewRow = CType(((CType(sender, Control)).NamingContainer), GridViewRow)
            Dim ddlKW As DropDownList = CType(gvr.FindControl("ddlKW"), DropDownList)
            Dim strKodKW As String = ddlKW.SelectedItem.Value

            Dim ddlKO As DropDownList = CType(gvr.FindControl("ddlKO"), DropDownList)
            Dim strKodKO As String = ddlKO.SelectedItem.Value

            Dim ddlPTj = DirectCast(gvr.FindControl("ddlPTj"), DropDownList)
            ddlPTj.DataSource = fLoadPTj(strKodKW, strKodKO)   'ViewState("dsKO")
            ddlPTj.DataTextField = "ButiranPTj"
            ddlPTj.DataValueField = "KodPTj"
            ddlPTj.DataBind()

            ddlPTj.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlPTj.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub ddlPTj_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try

            Dim gvr As GridViewRow = CType(((CType(sender, Control)).NamingContainer), GridViewRow)
            Dim ddlKW As DropDownList = CType(gvr.FindControl("ddlKW"), DropDownList)
            Dim strKodKW As String = ddlKW.SelectedItem.Value

            Dim ddlKO As DropDownList = CType(gvr.FindControl("ddlKO"), DropDownList)
            Dim strKodKO As String = ddlKO.SelectedItem.Value

            Dim ddlPTj As DropDownList = CType(gvr.FindControl("ddlPTj"), DropDownList)
            Dim strKodPTj As String = ddlPTj.SelectedItem.Value

            Dim ddlKP = DirectCast(gvr.FindControl("ddlKP"), DropDownList)
            ddlKP.DataSource = fLoadKP(strKodKW, strKodKO, strKodPTj)
            ddlKP.DataTextField = "Butiran"
            ddlKP.DataValueField = "KodProjek"
            ddlKP.DataBind()

            ddlKP.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlKP.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlKP_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try

            Dim gvr As GridViewRow = CType(((CType(sender, Control)).NamingContainer), GridViewRow)
            Dim ddlKW As DropDownList = CType(gvr.FindControl("ddlKW"), DropDownList)
            Dim strKodKW As String = ddlKW.SelectedItem.Value

            Dim ddlKO As DropDownList = CType(gvr.FindControl("ddlKO"), DropDownList)
            Dim strKodKO As String = ddlKO.SelectedItem.Value

            Dim ddlPTj As DropDownList = CType(gvr.FindControl("ddlPTj"), DropDownList)
            Dim strKodPTj As String = ddlPTj.SelectedItem.Value

            Dim ddlKP As DropDownList = CType(gvr.FindControl("ddlKP"), DropDownList)
            Dim strKodKP As String = ddlKP.SelectedItem.Value

            Dim ddlVot = DirectCast(gvr.FindControl("ddlVot"), DropDownList)
            ddlVot.DataSource = fLoadVot(strKodKW, strKodKO, strKodPTj, strKodKP)
            ddlVot.DataTextField = "Butiran"
            ddlVot.DataValueField = "KodVot"
            ddlVot.DataBind()

            ddlVot.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlVot.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub

    Private Sub lbtnKembali_Click(sender As Object, e As EventArgs) Handles lbtnKembali.Click
        divList.Visible = True
        divDetail.Visible = False
        fClear()
        fLoadLst()
    End Sub

    Private Sub ddlSaizRekod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSaizRekod.SelectedIndexChanged
        gvLst.PageSize = CInt(ddlSaizRekod.Text)
        fLoadLst()
        'BindGvViewButiran()
    End Sub

    Public Property direction() As SortDirection
        Get
            If ViewState("directionState") Is Nothing Then
                ViewState("directionState") = SortDirection.Ascending
            End If
            Return DirectCast(ViewState("directionState"), SortDirection)
        End Get
        Set
            ViewState("directionState") = Value
        End Set
    End Property


    Private Sub fSetFooter()
        Try
            Dim decJumTrans As Decimal
            For Each gvRow1 As GridViewRow In gvTrans.Rows
                Dim decJumlah As String = CDec(Trim(CType(gvRow1.FindControl("lblJumlah"), Label).Text.TrimEnd))
                decJumTrans += decJumlah
            Next

            Dim footerRow = gvTrans.FooterRow
            CType(footerRow.FindControl("lblTotJum"), Label).Text = FormatNumber(decJumTrans, 2)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub txtJumlah_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim decJumTrans As Decimal
            For Each gvRow As GridViewRow In gvTrans.Rows
                Dim decJumlah As String = CDec(Trim(CType(gvRow.FindControl("txtJumlah"), TextBox).Text.TrimEnd))
                decJumTrans += decJumlah
            Next

            Dim footerRow = gvTrans.FooterRow
            CType(footerRow.FindControl("lblTotJum"), Label).Text = FormatNumber(decJumTrans, 2)


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ViewFile(sender As Object, e As EventArgs)
        Dim id As Integer = Integer.Parse(TryCast(sender, LinkButton).CommandArgument)

        Try
            Dim url1 = Server.MapPath("~/")
            Dim url = Server.MapPath("~/FORMS/Akaun Penghutang/Permohon/FileAR.ashx")
            Dim fileExtension = ResolveUrl($"'{url}'?Id='{id}'")

            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "a", "window.open('" + fileExtension + "','_blank');", True)
        Catch ex As Exception

        End Try

    End Sub



    Private Sub gvLst_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvLst.SelectedIndexChanged
        Try
            Dim row As GridViewRow = gvLst.SelectedRow

            Dim strNoInvSem As String = CType(gvLst.SelectedRow.FindControl("lblNoInvSem"), Label).Text.TrimEnd
            Dim strStatDok As String = CType(gvLst.SelectedRow.FindControl("lblKodStatus"), Label).Text.TrimEnd

            hidIdBil.Value = CType(gvLst.SelectedRow.FindControl("lblIdBil"), Label).Text.TrimEnd

            sLoadInv(strNoInvSem)
            sLoadTrans(strNoInvSem)
            sLoadLampiran(strNoInvSem)
            sloadMohon(strNoInvSem)
            'sLoadSokong(strNoInvSem)

            If strStatDok = "01" Then
                lbtnLulus.Visible = True
                lbtnXLulus.Visible = True
                sSetLulus()

            Else
                lbtnLulus.Visible = False
                lbtnXLulus.Visible = False
                sLoadLulus(strNoInvSem, strStatDok)
            End If

            divList.Visible = False
            divDetail.Visible = True

        Catch ex As Exception

        End Try
    End Sub

    Private Sub sloadMohon(ByVal strNoInvSem As String)
        Try
            Dim dbconn As New DBKewConn
            Dim strNoStaf As String

            Dim strSql As String = "select AR06_NoStaf, AR06_Ulasan from AR06_StatusDok where AR06_NoBilSem = '" & strNoInvSem & "' and AR06_StatusDok = 01"

            Using ds = dbconn.fSelectCommand(strSql)
                If Not ds Is Nothing Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        strNoStaf = ds.Tables(0).Rows(0)("AR06_NoStaf").ToString
                        lblNoStafPmhn.Text = strNoStaf

                        Using dt = fGetUserInfo(strNoStaf)
                            lblNmPmhn.Text = dt.Rows(0)("MS01_Nama").ToString
                            lblJwtanMhn.Text = dt.Rows(0)("JawGiliran").ToString
                            lblKodPTjPmhn.Text = dt.Rows(0)("KodPejabat").ToString & "0000"
                            lblNmPTjPmhn.Text = dt.Rows(0)("Pejabat").ToString
                        End Using
                    End If
                End If
            End Using

        Catch ex As Exception

        End Try

    End Sub
    'Private Sub sLoadSokong(strNoInvSem As String)
    '    Try
    '        Dim strSql As String = "Select AR06_NoStaf, AR06_Tarikh from AR06_StatusDok where AR06_NoBilSem = '" & strNoInvSem & "' and AR06_StatusDok = '02' "
    '        Dim dbconn As New DBKewConn
    '        Using ds = dbconn.fSelectCommand(strSql)
    '            Dim strNoStafSokong As String = ds.Tables(0).Rows(0)("AR06_NoStaf").ToString
    '            Dim dtTkhSokong As Date = CDate(ds.Tables(0).Rows(0)("AR06_Tarikh").ToString)
    '            Dim strTkhSokong As String = dtTkhSokong.ToString("dd/MM/yyyy")

    '            lblNoStafSokong.Text = strNoStafSokong
    '            lblTkhSokong.Text = strTkhSokong

    '            If Not String.IsNullOrEmpty(strNoStafSokong) Then
    '                Using dt = fGetUserInfo(strNoStafSokong)
    '                    lblJawSokong.Text = dt.Rows(0)("JawGiliran").ToString
    '                    lblNmStafSokong.Text = dt.Rows(0)("MS01_Nama").ToString
    '                    lblKodPTjSokong.Text = dt.Rows(0)("KodPejabat").ToString & "0000"
    '                    lblNmPTjSokong.Text = dt.Rows(0)("Pejabat").ToString
    '                End Using
    '            End If
    '        End Using

    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub sSetLulus()
        lblKodPTjPel.Text = Session("ssusrKodPTj")
        lblNmPTjPel.Text = Session("ssusrPTj")
        lblNoStafPel.Text = Session("ssusrID")
        lblNmStafPel.Text = Session("ssusrName")
        lblJawPel.Text = Session("ssusrPost")
        txtUlasan.Text = ""

        Dim strTkhToday As String = Now.ToString("dd/MM/yyyy")
        lblTkhLulus.Text = strTkhToday
    End Sub

    Private Sub sLoadLulus(strNoInvSem As String, ByVal strStatDok As String)
        Try
            Dim strSql As String = "Select AR06_NoStaf, AR06_Tarikh from AR06_StatusDok where AR06_NoBilSem = '" & strNoInvSem & "' and AR06_StatusDok =  '" & strStatDok & "' "
            Dim dbconn As New DBKewConn
            Using ds = dbconn.fSelectCommand(strSql)
                Dim strNoStafLulus As String = ds.Tables(0).Rows(0)("AR06_NoStaf").ToString
                Dim dtTkhLulus As Date = CDate(ds.Tables(0).Rows(0)("AR06_Tarikh").ToString)
                Dim strTkhLulus As String = dtTkhLulus.ToString("dd/MM/yyyy")

                lblNoStafPel.Text = strNoStafLulus
                lblTkhLulus.Text = strTkhLulus

                If Not String.IsNullOrEmpty(strNoStafLulus) Then
                    Using dt = fGetUserInfo(strNoStafLulus)
                        lblJawPel.Text = dt.Rows(0)("JawGiliran").ToString
                        lblNmStafPel.Text = dt.Rows(0)("MS01_Nama").ToString
                        lblKodPTjPel.Text = dt.Rows(0)("KodPejabat").ToString & "0000"
                        lblNmPTjPel.Text = dt.Rows(0)("Pejabat").ToString
                    End Using
                End If
            End Using

        Catch ex As Exception

        End Try
    End Sub

    Private Sub sLoadInv(ByVal strNoInvSem As String)
        Try
            Dim dbconn As New DBKewConn()
            Dim dtTkhMohon As Date
            Dim strSqlInv = "Select AR01_NoBilSem, AR01_TkhMohon,AR01_KODPTJMOHON, (select Butiran  from MK_PTJ where KodPTJ = AR01_Bil.AR01_KodPTJMohon ) as ButPTj, AR01_NOSTAF, AR01_Jenis,AR01_KODBANK, (select NamaBank  from mk_bank where kodbank = AR01_Bil.AR01_KodBank ) as ButBank, AR01_NORUJUKAN, AR01_ALMT1,AR01_ALMT2,
                            AR01_KATEGORI, (SELECT Butiran from MK_KategoriPenerima where kod = AR01_Bil.AR01_Kategori ) as ButKat, AR01_NAMAPENERIMA,kodnegara, (select Butiran from MK_Negara where KodNegara = AR01_Bil.kodnegara) as ButNegara, kodnegeri, (select Butiran from MK_Negeri  where KodNegeri = AR01_Bil.kodnegeri ) as ButNegeri, AR01_IDPENERIMA, AR01_TUJUAN, AR01_BANDAR,AR01_POSKOD, AR01_TkhKonDari,AR01_TkhKonHingga,AR01_NOTEL, 
                            AR01_NOFAKS, AR01_TempohKontrak, AR01_JenTemp, AR01_TkhPeringatan1 ,AR01_TkhPeringatan2 , AR01_TkhPeringatan3, AR01_TkhLulus ,AR01_Emel,AR01_UtkPerhatian, AR01_StatusDok, (select Butiran  from AR_StatusDok where KodStatus = AR01_Bil.AR01_StatusDok ) as ButStatDok, AR01_Kontrak 
from AR01_Bil where AR01_NoBilSem='" & strNoInvSem & "'"

            'Load Permohonan Invois
            Dim ds = dbconn.fSelectCommand(strSqlInv)

            Using dtMhn = ds.Tables(0)
                If dtMhn.Rows.Count > 0 Then
                    Dim strTkhMohon As String
                    strTkhMohon = dtMhn.Rows(0)("AR01_TkhMohon").ToString
                    If strTkhMohon <> "" Then
                        dtTkhMohon = CDate(strTkhMohon)
                        strTkhMohon = dtTkhMohon.ToString("dd/MM/yyyy")
                    End If

                    ' Dim TkhMhn As Date = CDate(IIf(IsDBNull(dtMhn.Rows(0)("AR01_TkhMohon")), Nothing, dtMhn.Rows(0)("AR01_TkhMohon")))
                    Dim strKodPTJ As String = dtMhn.Rows(0)("AR01_KODPTJMOHON")
                    Dim strNoStafPmhn As String = dtMhn.Rows(0)("AR01_NOSTAF")
                    Dim katInv As String = dtMhn.Rows(0)("AR01_Jenis")
                    Dim strKodBank As String = dtMhn.Rows(0)("AR01_KODBANK")
                    Dim strNoRuj As String = IIf(IsDBNull(dtMhn.Rows(0)("AR01_NORUJUKAN")), "-", dtMhn.Rows(0)("AR01_NORUJUKAN"))

                    Dim Almt1 As String = dtMhn.Rows(0)("AR01_ALMT1")
                    Dim Almt2 As String = dtMhn.Rows(0)("AR01_ALMT2")
                    Dim strKodKat As String = dtMhn.Rows(0)("AR01_KATEGORI")
                    Dim NPenerima As String = dtMhn.Rows(0)("AR01_NAMAPENERIMA")
                    Dim strKodNegara As String = dtMhn.Rows(0)("kodnegara")
                    Dim strKodNegeri As String = dtMhn.Rows(0)("kodnegeri")
                    Dim IDPenerima As String = dtMhn.Rows(0)("AR01_IDPENERIMA")
                    Dim Tujuan As String = dtMhn.Rows(0)("AR01_TUJUAN")
                    Dim Bandar As String = dtMhn.Rows(0)("AR01_BANDAR")
                    Dim Poskod As String = dtMhn.Rows(0)("AR01_POSKOD")
                    Dim TkhKonDr As String = (IIf(IsDBNull(dtMhn.Rows(0)("AR01_TkhKonDari")), "-", dtMhn.Rows(0)("AR01_TkhKonDari")))
                    Dim TkhKonHingga As String = (IIf(IsDBNull(dtMhn.Rows(0)("AR01_TkhKonHingga")), "-", dtMhn.Rows(0)("AR01_TkhKonHingga")))
                    Dim NoTel As String = IIf(IsDBNull(dtMhn.Rows(0)("AR01_NOTEL")), "-", dtMhn.Rows(0)("AR01_NOTEL"))
                    Dim NoFax As String = IIf(IsDBNull(dtMhn.Rows(0)("AR01_NOFAKS")), "-", dtMhn.Rows(0)("AR01_NOFAKS"))
                    Dim TmphKon As String = (IIf(IsDBNull(dtMhn.Rows(0)("AR01_TempohKontrak")), "-", dtMhn.Rows(0)("AR01_TempohKontrak")))
                    Dim TkhPeringatan1 As String = (IIf(IsDBNull(dtMhn.Rows(0)("AR01_TkhPeringatan1")), Nothing, dtMhn.Rows(0)("AR01_TkhPeringatan1")))
                    Dim TkhPeringatan2 As String = (IIf(IsDBNull(dtMhn.Rows(0)("AR01_TkhPeringatan2")), Nothing, dtMhn.Rows(0)("AR01_TkhPeringatan2")))
                    Dim TkhPeringatan3 As String = (IIf(IsDBNull(dtMhn.Rows(0)("AR01_TkhPeringatan3")), Nothing, dtMhn.Rows(0)("AR01_TkhPeringatan3")))
                    Dim TkhLulus As String = (IIf(IsDBNull(dtMhn.Rows(0)("AR01_TkhLulus")), Nothing, dtMhn.Rows(0)("AR01_TkhLulus")))
                    Dim Emel As String = dtMhn.Rows(0)("AR01_Emel")
                    Dim Perhatian As String = dtMhn.Rows(0)("AR01_UtkPerhatian")
                    Dim strJenKontrak As String = (IIf(IsDBNull(dtMhn.Rows(0)("AR01_Kontrak")), "Tidak", dtMhn.Rows(0)("AR01_Kontrak")))
                    Dim strJenTemp As String
                    If IsDBNull(dtMhn.Rows(0)("AR01_JenTemp")) Then
                        strJenTemp = ""
                    Else
                        Dim strKodJenTemp = Trim(dtMhn.Rows(0)("AR01_JenTemp").ToString.TrimEnd)
                        strJenTemp = fGetjenTemp(strKodJenTemp)
                    End If

                    lblTkhMhn2.Text = strTkhMohon
                    lblNoInvSem.Text = strNoInvSem
                    lblKodPtj.Text = strKodPTJ
                    lblNPTJ.Text = dtMhn.Rows(0)("ButPTj")
                    lblNoStafPmhn.Text = strNoStafPmhn
                    lblIdPen.Text = IDPenerima
                    lblTujuan.Text = Tujuan
                    lblNoRuj.Text = IIf(strNoRuj = "", "-", strNoRuj)
                    lblAlmt1.Text = Almt1
                    lblAlmt2.Text = Almt2
                    lblBandar.Text = Bandar
                    lblPoskod.Text = Poskod
                    lblNoTel.Text = NoTel
                    lblNoFax.Text = IIf(NoFax = "", "-", NoFax)
                    lblEmel.Text = Emel
                    lblPerhatian.Text = IIf(Perhatian = "", "-", Perhatian)

                    lblKodBank.Text = strKodBank
                    lblNmBank.Text = dtMhn.Rows(0)("ButBank")

                    lblKodKat.Text = strKodKat
                    lblKat.Text = dtMhn.Rows(0)("ButKat")
                    lblKodNegara.Text = strKodNegara
                    lblNegara.Text = dtMhn.Rows(0)("ButNegara")
                    lblKodNegeri.Text = strKodNegeri
                    lblNegeri.Text = dtMhn.Rows(0)("ButNegeri")
                    lblNmPen.Text = NPenerima
                    lblStatus.Text = dtMhn.Rows(0)("ButStatDok")

                    lblKodPTjPmhn.Text = strKodPTJ
                    lblNmPTjPmhn.Text = dtMhn.Rows(0)("ButPTj")

                    lblJenKontrak.Text = IIf(strJenKontrak = True, "Ya", "Tidak")
                    lblTkhMulaKon.Text = TkhKonDr
                    lblTkhTamatKon.Text = TkhKonHingga
                    lblTemKon.Text = TmphKon
                    lblJenTem.Text = strJenTemp
                    lblTkhInv.Text = TkhLulus

                    If Not String.IsNullOrEmpty(strNoStafPmhn) Then
                        Using dt = fGetUserInfo(strNoStafPmhn)
                            lblJwtanMhn.Text = dt.Rows(0)("JawGiliran").ToString
                            lblNmPmhn.Text = dt.Rows(0)("MS01_Nama").ToString
                        End Using
                    End If

                End If
            End Using

        Catch ex As Exception

        End Try
    End Sub



    Private Sub sLoadTrans(strNoInvSem)
        Try
            fClearGvTrans()
            Dim strSql = "select AR01_BilDtID, KodKw,KodKO,KodPTJ,KodKP,KodVot,AR01_Perkara, CAST(AR01_Kuantiti AS int) as AR01_Kuantiti, AR01_kadarHarga,AR01_Jumlah from AR01_BilDt where AR01_NoBilSem='" & strNoInvSem & "'"

            Dim dbconn As New DBKewConn
            Dim ds As New DataSet
            ds = dbconn.fSelectCommand(strSql)

            Using MohonDt = ds.Tables(0)
                gvTrans.DataSource = MohonDt
                gvTrans.DataBind()
                ViewState("dtInvDt") = MohonDt
            End Using
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvLst_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvLst.PageIndexChanging
        Try
            gvLst.PageIndex = e.NewPageIndex
            If ViewState("vsDtInv") IsNot Nothing Then
                gvLst.DataSource = ViewState("vsDtInv")
                gvLst.DataBind()
            End If
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub lbtnLulus_Click(sender As Object, e As EventArgs) Handles lbtnLulus.Click

        If fLulus() Then
            fGlobalAlert("Maklumat invois telah diluluskan!", Me.Page, Me.[GetType]())
            lbtnLulus.Visible = False
            lbtnXLulus.Visible = False
        Else
            fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
        End If


    End Sub

    Protected Sub lbtnXLulus_Click(sender As Object, e As EventArgs) Handles lbtnXLulus.Click

        If txtUlasan.Text = "" Then
            fGlobalAlert("Sila masukkan ulasan!", Me.Page, Me.[GetType]())
            Exit Sub
        End If

        If fXLulus() Then
            fGlobalAlert("Maklumat telah disimpan sebagai 'Tidak Lulus'!", Me.Page, Me.[GetType]())
            fClear()
            divList.Visible = True
            divDetail.Visible = False
            fLoadLst()
        Else
            fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
        End If

    End Sub


    Dim decJumDt As Decimal
    Private Sub gvTrans_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvTrans.RowDataBound

        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim strJumlah As String = CType(e.Row.FindControl("lblJumlah"), Label).Text
                If strJumlah = "" Then strJumlah = "0.00"
                decJumDt += CDec(strJumlah)

                Dim ddlKW = DirectCast(e.Row.FindControl("ddlKW"), DropDownList)
                ddlKW.DataSource = ViewState("dsKW")
                ddlKW.DataTextField = "ButiranKW"
                ddlKW.DataValueField = "KodKw"
                ddlKW.DataBind()
                ddlKW.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
                ddlKW.SelectedValue = TryCast(e.Row.FindControl("hidKW"), HiddenField).Value

                Dim ddlKO = DirectCast(e.Row.FindControl("ddlKO"), DropDownList)
                Dim strSelKO As String = TryCast(e.Row.FindControl("hidKO"), HiddenField).Value

                If strSelKO = "0" OrElse strSelKO = "" Then
                    ddlKO.Items.Insert(0, New ListItem("- SILA PILIH KW -", 0))
                    ddlKO.SelectedValue = strSelKO
                Else
                    ddlKO.DataSource = ViewState("dsKO")
                    ddlKO.DataTextField = "Butiran"
                    ddlKO.DataValueField = "KodKO"
                    ddlKO.DataBind()
                    ddlKO.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
                    ddlKO.SelectedValue = strSelKO
                End If

                Dim ddlPTj = DirectCast(e.Row.FindControl("ddlPTj"), DropDownList)
                Dim strSelPTj As String = TryCast(e.Row.FindControl("hidPTj"), HiddenField).Value

                If strSelPTj = "0" OrElse strSelPTj = "" Then
                    ddlPTj.Items.Insert(0, New ListItem("- SILA PILIH KO -", 0))
                    ddlPTj.SelectedValue = strSelPTj
                Else
                    ddlPTj.DataSource = ViewState("dsPTj")
                    ddlPTj.DataTextField = "Butiran"
                    ddlPTj.DataValueField = "KodPTj"
                    ddlPTj.DataBind()
                    ddlPTj.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
                    ddlPTj.SelectedValue = strSelPTj
                End If

                Dim ddlKP = DirectCast(e.Row.FindControl("ddlKP"), DropDownList)
                Dim strSelKP As String = TryCast(e.Row.FindControl("hidKP"), HiddenField).Value

                If strSelKP = "0" OrElse strSelKP = "" Then
                    ddlKP.Items.Insert(0, New ListItem("- SILA PILIH PTj -", 0))
                    ddlKP.SelectedValue = strSelKP
                Else
                    ddlKP.DataSource = ViewState("dsKP")
                    ddlKP.DataTextField = "Butiran"
                    ddlKP.DataValueField = "KodKP"
                    ddlKP.DataBind()

                    ddlKP.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
                    ddlKP.SelectedValue = strSelKP
                End If

                Dim ddlVot = DirectCast(e.Row.FindControl("ddlVot"), DropDownList)
                Dim strSelVot As String = TryCast(e.Row.FindControl("hidVot"), HiddenField).Value


                If strSelVot = "0" OrElse strSelVot = "" Then
                    ddlVot.Items.Insert(0, New ListItem("- SILA PILIH KP -", 0))
                    ddlVot.SelectedValue = strSelVot
                Else
                    ddlVot.DataSource = ViewState("dsVot")
                    ddlVot.DataTextField = "Butiran"
                    ddlVot.DataValueField = "KodVot"
                    ddlVot.DataBind()

                    ddlVot.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
                    ddlVot.SelectedValue = strSelVot
                End If

            ElseIf e.Row.RowType = DataControlRowType.Footer Then
                Dim lblTotJum As Label = CType(e.Row.FindControl("lblTotJum"), Label)
                lblTotJum.Text = FormatNumber(decJumDt, 2)

            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ddlCarian_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCarian.SelectedIndexChanged
        If ddlCarian.SelectedIndex = 0 Then
            txtCarian.Enabled = False
        Else
            txtCarian.Enabled = True
        End If
    End Sub

    Private Sub lbtnCari_Click(sender As Object, e As EventArgs) Handles lbtnCari.Click
        Dim strNoInvCuk As String = Trim(txtCarian.Text.TrimEnd)
        fLoadLst()
    End Sub

    Private Sub ddlFilStat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFilStat.SelectedIndexChanged
        fLoadLst()
    End Sub

    Protected Sub lbtnTambah_Click(sender As Object, e As EventArgs)

        Try
            Dim strKW As String = CType(gvTrans.Rows(gvTrans.Rows.Count - 1).FindControl("ddlKW"), DropDownList).Text
            Dim strKO As String = CType(gvTrans.Rows(gvTrans.Rows.Count - 1).FindControl("ddlKO"), DropDownList).Text
            Dim strPTj As String = CType(gvTrans.Rows(gvTrans.Rows.Count - 1).FindControl("ddlPTj"), DropDownList).Text
            Dim strKP As String = CType(gvTrans.Rows(gvTrans.Rows.Count - 1).FindControl("ddlKP"), DropDownList).Text
            Dim strVot As String = CType(gvTrans.Rows(gvTrans.Rows.Count - 1).FindControl("ddlVot"), DropDownList).Text
            Dim strPerkara As String = CType(gvTrans.Rows(gvTrans.Rows.Count - 1).FindControl("txtPerkara"), TextBox).Text
            Dim intKtt As Integer = CInt(CType(gvTrans.Rows(gvTrans.Rows.Count - 1).FindControl("txtKuantiti"), TextBox).Text)
            Dim decHarga As Decimal = CDec(CType(gvTrans.Rows(gvTrans.Rows.Count - 1).FindControl("txtHarga"), TextBox).Text)
            Dim decJumlah As Decimal = CDec(CType(gvTrans.Rows(gvTrans.Rows.Count - 1).FindControl("lblJumlah"), Label).Text)

            If strKW <> "0" AndAlso strKO <> "0" AndAlso strPTj <> "0" AndAlso strKP <> "0" AndAlso strVot <> "0" AndAlso strPerkara <> "" AndAlso intKtt <> 0 AndAlso decHarga <> 0.00 AndAlso decJumlah <> 0.00 Then
                AddNewRowToGrid()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub AddNewRowToGrid()
        Try
            Dim rowIndex As Integer = 0
            If ViewState("dtInvDt") IsNot Nothing Then
                Dim dtvsDtInv As DataTable = CType(ViewState("dtInvDt"), DataTable)
                Dim drCurrentRow As DataRow = Nothing

                If dtvsDtInv.Rows.Count > 0 Then

                    For i As Integer = 1 To dtvsDtInv.Rows.Count

                        Dim strKW = CType(gvTrans.Rows(rowIndex).Cells(1).FindControl("ddlKW"), DropDownList).Text
                        Dim strKO = CType(gvTrans.Rows(rowIndex).Cells(2).FindControl("ddlKO"), DropDownList).Text
                        Dim strPTj = CType(gvTrans.Rows(rowIndex).Cells(3).FindControl("ddlPTj"), DropDownList).Text
                        Dim strKP = CType(gvTrans.Rows(rowIndex).Cells(4).FindControl("ddlKP"), DropDownList).Text
                        Dim strVot = CType(gvTrans.Rows(rowIndex).Cells(5).FindControl("ddlVot"), DropDownList).Text
                        Dim strPerkara = CType(gvTrans.Rows(rowIndex).Cells(6).FindControl("txtPerkara"), TextBox).Text
                        Dim strKuantiti = CType(gvTrans.Rows(rowIndex).Cells(7).FindControl("txtKuantiti"), TextBox).Text
                        Dim strHarga = CType(gvTrans.Rows(rowIndex).Cells(8).FindControl("txtHarga"), TextBox).Text
                        Dim strJumlah = CType(gvTrans.Rows(rowIndex).Cells(9).FindControl("lblJumlah"), Label).Text

                        dtvsDtInv.Rows(i - 1)("KodKw") = strKW
                        dtvsDtInv.Rows(i - 1)("KodKO") = strKO
                        dtvsDtInv.Rows(i - 1)("KodPTJ") = strPTj
                        dtvsDtInv.Rows(i - 1)("KodKP") = strKP
                        dtvsDtInv.Rows(i - 1)("KodVot") = strVot
                        dtvsDtInv.Rows(i - 1)("AR01_Perkara") = strPerkara
                        dtvsDtInv.Rows(i - 1)("AR01_Kuantiti") = IIf(strKuantiti = "", 0, strKuantiti)
                        dtvsDtInv.Rows(i - 1)("AR01_kadarHarga") = IIf(strHarga = "", 0.00, strHarga)
                        dtvsDtInv.Rows(i - 1)("AR01_Jumlah") = IIf(strJumlah = "", 0.00, strJumlah)

                        rowIndex += 1
                    Next

                    drCurrentRow = dtvsDtInv.NewRow()
                    dtvsDtInv.Rows.Add(drCurrentRow)
                    ViewState("dtInvDt") = dtvsDtInv
                    gvTrans.DataSource = dtvsDtInv
                    gvTrans.DataBind()
                    SetPreviousData(dtvsDtInv)
                End If

            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Sub SetPreviousData(ByVal dt As DataTable)
        Try
            Dim rowIndex As Integer = 0

            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1

                    Dim gvRow As GridViewRow = gvTrans.Rows(i)
                    Dim ddl1 As DropDownList = CType(gvRow.FindControl("ddlKW"), DropDownList)
                    Dim ddl2 As DropDownList = CType(gvRow.FindControl("ddlKO"), DropDownList)
                    Dim ddl3 As DropDownList = CType(gvRow.FindControl("ddlPTj"), DropDownList)
                    Dim ddl4 As DropDownList = CType(gvRow.FindControl("ddlKP"), DropDownList)
                    Dim ddl5 As DropDownList = CType(gvRow.FindControl("ddlVot"), DropDownList)
                    Dim box1 As TextBox = CType(gvRow.FindControl("txtPerkara"), TextBox)
                    Dim box2 As TextBox = CType(gvRow.FindControl("txtKuantiti"), TextBox)
                    Dim box3 As TextBox = CType(gvRow.FindControl("txtHarga"), TextBox)
                    Dim box4 As Label = CType(gvRow.FindControl("lblJumlah"), Label)

                    Dim strKtt As String = dt.Rows(i)("AR01_Kuantiti").ToString()
                    Dim strHarga As String = dt.Rows(i)("AR01_kadarHarga").ToString()
                    Dim strJumlah As String = dt.Rows(i)("AR01_Jumlah").ToString()

                    ddl1.SelectedValue = dt.Rows(i)("KodKw").ToString()
                    ddl2.SelectedValue = dt.Rows(i)("KodKO").ToString()
                    ddl3.SelectedValue = dt.Rows(i)("KodPTJ").ToString()
                    ddl4.SelectedValue = dt.Rows(i)("KodKP").ToString()
                    ddl5.SelectedValue = dt.Rows(i)("KodVot").ToString()
                    box1.Text = dt.Rows(i)("AR01_Perkara").ToString()
                    box2.Text = IIf(strKtt = "", 0, strKtt)
                    box3.Text = IIf(strHarga = "", "0.00", strHarga)
                    box4.Text = IIf(strJumlah = "", "0.00", strJumlah)

                    rowIndex += 1
                Next
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txtHarga_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim txtHarga As TextBox = CType(CType(sender, Control), TextBox)
            Dim HrgUnit = txtHarga.Text
            Dim gvRow As GridViewRow = CType(CType(sender, Control), TextBox).Parent.Parent
            Dim txtKuantiti As TextBox = CType(gvRow.FindControl("txtKuantiti"), TextBox)
            Dim lblJumlah As Label = CType(gvRow.FindControl("lblJumlah"), Label)

            '  If txtKuantiti.Text IsNot String.Empty Then
            Dim angHrgSeunit = CDec(HrgUnit)
            Dim JumAngHrg As Decimal = 0.00

            JumAngHrg = CDec(CInt(txtKuantiti.Text) * angHrgSeunit)
            lblJumlah.Text = FormatNumber(JumAngHrg, 2) 'JumAngHrg.ToString("N2")
            ' End If

            txtHarga.Text = FormatNumber(txtHarga.Text, 2)
            fSetFooter()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txtKuantiti_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim kuantiti = CType(CType(sender, Control), TextBox).Text
            Dim gvRow As GridViewRow = CType(CType(sender, Control), TextBox).Parent.Parent
            Dim txtHarga As TextBox = CType(gvRow.FindControl("txtHarga"), TextBox)
            Dim lblJumlah As Label = CType(gvRow.FindControl("lblJumlah"), Label)
            ' If txtHarga.Text IsNot String.Empty Then
            If txtHarga.Text = "" Then txtHarga.Text = 0
            Dim angHrgSeunit = CDec(txtHarga.Text)
            Dim JumAngHrg = CDec(CInt(kuantiti) * angHrgSeunit)
            lblJumlah.Text = FormatNumber(JumAngHrg, 2) 'JumAngHrg.ToString("N2")
            '  End If

            fSetFooter()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvTrans_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvTrans.RowDeleting
        Try
            Dim rowIndex As Integer = 0
            Dim index As Integer = Convert.ToInt32(e.RowIndex)

            If ViewState("dtInvDt") IsNot Nothing Then
                Dim dtCurrentTable As DataTable = CType(ViewState("dtInvDt"), DataTable)
                Dim drCurrentRow As DataRow = Nothing

                If dtCurrentTable.Rows.Count > 1 Then
                    dtCurrentTable.Rows(index).Delete()
                    dtCurrentTable.AcceptChanges()
                    ViewState("dtInvDt") = dtCurrentTable
                    gvTrans.DataSource = dtCurrentTable
                    gvTrans.DataBind()
                End If
            Else
                sClearGvTrans()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub sClearGvTrans()
        gvTrans.DataSource = New List(Of String)
        gvTrans.DataBind()
    End Sub

    Private Sub sLoadLampiran(ByVal strNoInvSem As String)
        Dim ds As New DataSet
        Dim dbconn As New DBKewConn()
        Try
            sClearGvLamp()
            Dim strSql As String = "select AR11_ID, AR01_IdBil, AR11_Path, AR11_NamaDok  from AR11_Lampiran where AR01_NoBilSem= @NoBilSem AND AR11_Status  = @Status"

            Dim paramSql() As SqlParameter =
                {
                New SqlParameter("@NoBilSem", strNoInvSem),
                 New SqlParameter("@Status", 1)
                }

            ds = dbconn.fSelectCommand(strSql, paramSql)
            If ds.Tables.Count > 0 Then
                gvLamp.DataSource = ds
                gvLamp.DataBind()

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub sClearGvLamp()
        gvLst.DataSource = New List(Of String)
        gvLst.DataBind()
    End Sub

    Private Sub gvLamp_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvLamp.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim boolStatus As Boolean = CBool(CType(e.Row.FindControl("lblStatus"), Label).Text)
                Dim lbtnSelect As LinkButton = CType(e.Row.FindControl("lbtnSelect"), LinkButton)

                If boolStatus = False Then
                    lbtnSelect.Visible = False
                Else
                    lbtnSelect.Visible = True
                End If

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvLamp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvLamp.SelectedIndexChanged
        Try
            Dim row As GridViewRow = gvLamp.SelectedRow
            Dim strKodModul = Request.QueryString("KodSub").Substring(0, 2)

            Dim intIdBil As String = CType(gvLamp.SelectedRow.FindControl("lblIdBil"), Label).Text.TrimEnd
            Dim intId As String = CType(gvLamp.SelectedRow.FindControl("lblId"), Label).Text.TrimEnd

            Dim url As String = "../../View_File.aspx?KodModul=" & strKodModul & "&intIdBil=" & intIdBil & "&Id=" & intId
            Dim fullURL As String = (Convert.ToString("window.open('") & url) + "', '_blank', 'height=800,width=850,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=yes,titlebar=no' );"
            ScriptManager.RegisterStartupScript(Me, GetType(String), "OPEN_WINDOW", fullURL, True)

        Catch ex As Exception

        End Try
    End Sub
End Class