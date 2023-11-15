Imports System.Drawing
Imports System.Data.SqlClient
Imports System.IO
Imports System.Web.Configuration

Public Class Sokongan_Invois_Kewangan
    Inherits System.Web.UI.Page

    Dim sLogBaru As String
    Dim sLogMedan As String
    Dim strNoStaf As String = UserInfo.strSesStaffID
    Dim sLogLevel As String = UserInfo.strUsrTahap
    Dim lsLogUsrIP As String = UserInfo.strUsrPcIP
    Dim lsLogUsrPC As String = UserInfo.strUsrPCName

    Dim clsMail As New clsMail.Mail
    Dim strSMTPServer As String = WebConfigurationManager.AppSettings("SMTPServer")
    Dim strSMTPPort As String = WebConfigurationManager.AppSettings("SMTPPort")
    Dim strSenderAdr As String = WebConfigurationManager.AppSettings("SenderAddr")

    Private dbconnSMSM As New DBSMConn()
    Dim ds As New DataSet
    Dim ds1 As New DataSet
    Private countButiran As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'Dim Viewstate("ssusrID") = Session("ssusrID")
            Dim strKodSubMenu As String = Request.QueryString("KodSubMenu")
            Session("sesKodSubMenu") = Request.QueryString("KodSubMenu")

            divList.Visible = True
            divDetail.Visible = False

            'Dim strStaffID = Session("ssusrID")
            'Dim strKodSubMenu As String = Request.QueryString("KodSubMenu")
            'If fCheckPowerUser(strStaffID, strKodSubMenu) Then
            '    ViewState("PowerUser") = True
            'Else
            '    ViewState("PowerUser") = False
            'End If


            fBindDdlFilStat()
            fLoadLst()

        End If
    End Sub

    Private Sub fBindDdlFilStat()
        Dim dbconn As New DBKewConn
        Try
            Dim strSql As String = "select KodStatus, Butiran from AR_StatusDok where KodStatus in (01,02,04)"

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
        Dim strSql As String
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
                strStat = "01,02,04"
            Else
                strStat = ddlFilStat.SelectedValue
            End If

            strSql = "select a.AR01_NoBilSem,AR01_NoBil, Convert(varchar, a.AR01_TkhMohon, 103) As AR01_TkhMohon, a.AR01_Tujuan, a.AR01_NamaPenerima, AR01_Kategori, (select Butiran  from MK_KategoriPenerima where Kod = a.AR01_Kategori ) as ButKat, a.ar01_Jumlah, AR01_TkhMohon  as Tarikh_Bil, ar01_statusdok, (Select B.Butiran  from AR_StatusDok B where B.KodStatus = A.AR01_StatusDok) as ButStatus
from AR01_Bil a 
where ar01_statusdok in (" & strStat & ") and ar01_kodptjmohon='" & Trim(Session("ssusrKodPTj")).TrimEnd & "' and ar01_jenis='01' " & strFilter

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

    Dim strTotJumlah As String
    Dim decJumlah As Decimal

    Private Sub fClearGvLst()
        gvLst.DataSource = New List(Of String)
        gvLst.DataBind()
    End Sub

    Private Sub fClearGvTrans()
        gvTrans.DataSource = New List(Of String)
        gvTrans.DataBind()
    End Sub

    Dim strTotJumlah2 As String
    Dim decJumlah2 As Decimal

    Private Function fSokong()
        Dim blnSuccess As Boolean = True
        Dim strNoStaf As String = Trim(lblNoStafSokong.Text.TrimEnd)
        Dim dbconn As New DBKewConn()
        Dim strMsg As String
        Try
            If String.IsNullOrEmpty(strNoStaf) Then
                blnSuccess = False
                Exit Try
            End If

            Dim strSql As String
            Dim strNoInvSem As String = Trim(lblNoInvSem.Text.TrimEnd)
            Dim strTkhToday As String = Now.ToString("yyyy-MM-dd")
            Dim strTahun As String = Now.Year
            Dim strTujuan As String = Trim(lblTujuan.Text.TrimEnd)
            Dim strStatDok = "02"

            strSql = "UPDATE AR01_Bil Set  
                                AR01_STATUSDOK = @StatDok
                              WHERE ar01_nobilsem = @NoBilSem;"

            Dim paramSql() As SqlParameter

            paramSql =
               {
               New SqlParameter("@StatDok", strStatDok),
               New SqlParameter("@NoBilSem", strNoInvSem)
               }

            dbconn.sConnBeginTrans()
            If Not dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                blnSuccess = False
                Exit Try
            Else
                'AUDIT LOG
                sLogMedan = "AR01_STATUSDOK|ar01_nobilsem"
                sLogBaru = strStatDok & "|" & strNoInvSem

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
            strSql = "INSERT INTO AR06_STATUSDOK(AR06_NOBILSEM,AR06_NoBil, AR06_StatusDok,AR06_Tarikh,AR06_Ulasan,AR06_NoStaf)" &
                             "VALUES (@NOBILSEM,@NOBIL, @STATUSDOK,@TARIKH,@ULASAN, @NOSTAF)"

            paramSql =
                {
                New SqlParameter("@NOBILSEM", strNoInvSem),
                 New SqlParameter("@NOBIL", strNoInvSem),
                 New SqlParameter("@STATUSDOK", strStatDok),
                 New SqlParameter("@TARIKH", strTkhToday),
                 New SqlParameter("@ULASAN", " - "),
                 New SqlParameter("@NOSTAF", strNoStaf)
                }

            If Not dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                blnSuccess = False
                Exit Try
            End If

            'If fSendEmail() = False Then
            '    blnSuccess = False
            '    strMsg = " hantar email"
            '    Exit Try
            'End If

        Catch ex As Exception
            blnSuccess = False
        End Try

        If blnSuccess Then
            dbconn.sConnCommitTrans()
            fGlobalAlert("Maklumat invois telah disokong!", Me.Page, Me.[GetType]())
            Return True
        Else
            dbconn.sConnRollbackTrans()
            fGlobalAlert("Ralat" & strMsg & "!", Me.Page, Me.[GetType]())
            Return False
        End If

    End Function
    Private Function fXSokong()
        Dim strSql As String
        Dim blnSuccess As Boolean = True
        Dim strTkhToday As String = Now.ToString("yyyy-MM-dd")
        Dim strTahun As String = Now.Year
        Dim strStatDok = "04"
        Dim strNoBilSem As String = Trim(lblNoInvSem.Text.TrimEnd)
        Dim strUlasan = Trim(txtUlasan.Text.Trim)
        Dim paramSql() As SqlParameter
        Dim strNoStaf As String = Trim(lblNoStafSokong.Text.TrimEnd)
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
                sLogBaru = strNoBilSem & "|" & strStatDok

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
                             "VALUES (@NOBILSEM,@NOBIL, @STATUSDOK,@TARIKH,@ULASAN, @NoStaf)"

            paramSql =
                {
                New SqlParameter("@NOBILSEM", strNoBilSem),
                 New SqlParameter("@NOBIL", strNoBilSem),
                 New SqlParameter("@STATUSDOK", strStatDok),
                 New SqlParameter("@TARIKH", strTkhToday),
                 New SqlParameter("@ULASAN", strUlasan),
                 New SqlParameter("@NoStaf", strNoStaf)
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
        fClearInfo()
        fLoadLst()
        divList.Visible = True
        divDetail.Visible = False
    End Sub

    Private Sub ddlSaizRekod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSaizRekod.SelectedIndexChanged
        gvLst.PageSize = CInt(ddlSaizRekod.Text)
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
    Protected Sub ViewFile(sender As Object, e As EventArgs)
        Dim id As Integer = Integer.Parse(TryCast(sender, LinkButton).CommandArgument)
        'Dim bytes As Byte()
        'Dim fileName As String, contentType As String
        'Dim paramSql() As SqlParameter
        'Dim dbconn As New DBKewConn

        'Dim str = "select PO13_NamaDok,PO13_ContentType,PO13_Data from PO13_Lampiran where PO13_ID=@Id"
        'paramSql = {
        '            New SqlParameter("@Id", id)
        '            }

        'Dim ds = dbconn.fselectCommand(str)
        'Dim dt = ds.Tables(0)

        'bytes = DirectCast(dt.Rows(0)("PO13_Data"), Byte())
        'contentType = dt.Rows(0)("PO13_ContentType").ToString()
        'fileName = dt.Rows(0)("PO13_NamaDok").ToString()

        'Dim embed As String = "<object data=""{0}"" type=""application/pdf"" width=""100%"" height=""1500px"">"
        'embed += "If you are unable to view file, you can download from <a href = ""{0}"">here</a>"
        'embed += " or download <a target = ""_blank"" href = ""http://get.adobe.com/reader/"">Adobe PDF Reader</a> to view the file."
        'embed += "</object>"
        'ltEmbed.Text = String.Format(embed, ResolveUrl("~/FORMS/Perolehan/Permohonan Perolehan/FilePO.ashx?Id="), id)
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
            Dim strKodStat As String = CType(gvLst.SelectedRow.FindControl("lblKodStatus"), Label).Text.TrimEnd

            sLoadInv(strNoInvSem)
            sLoadTrans(strNoInvSem)
            sLoadLampiran(strNoInvSem)
            sloadMohon(strNoInvSem, strKodStat)

            If strKodStat = "01" Then
                lbtnSokong.Visible = True
                lbtnXSokong.Visible = True
                sSetSokong()
            Else
                lbtnSokong.Visible = False
                lbtnXSokong.Visible = False
                sLoadSokong(strNoInvSem, strKodStat)
            End If

            divList.Visible = False
            divDetail.Visible = True

        Catch ex As Exception

        End Try
    End Sub

    Private Sub sSetSokong()
        lblKodPTjSokong.Text = Session("ssusrKodPTj")
        lblNmPTjSokong.Text = Session("ssusrPTj")
        lblNoStafSokong.Text = Session("ssusrID")
        lblNmStafSokong.Text = Session("ssusrName")
        lblJawSokong.Text = Session("ssusrPost")
        txtUlasan.Text = ""
        Dim strTkhToday As String = Now.ToString("dd/MM/yyyy")
        lblTkhSokong.Text = strTkhToday
    End Sub

    Private Sub sloadMohon(ByVal strNoInvSem As String, ByVal strKodStat As String)
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
    Private Sub sLoadSokong(ByVal strNoInvSem As String, ByVal strKodStat As String)
        Try
            Dim dbconn As New DBKewConn
            Dim strNoStaf, strUlasan As String

            Dim strSql As String = "select AR06_NoStaf, AR06_Ulasan, AR06_Tarikh from AR06_StatusDok where AR06_NoBilSem = '" & strNoInvSem & "' and AR06_StatusDok = '" & strKodStat & "'"

            Using ds = dbconn.fSelectCommand(strSql)
                If Not ds Is Nothing Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        strNoStaf = ds.Tables(0).Rows(0)("AR06_NoStaf").ToString
                        strUlasan = ds.Tables(0).Rows(0)("AR06_Ulasan").ToString
                        Dim dtTkhSokong As Date = CDate(ds.Tables(0).Rows(0)("AR06_Tarikh").ToString)
                        lblTkhSokong.Text = dtTkhSokong.ToString("dd/MM/yyyy")

                        lblNoStafSokong.Text = strNoStaf
                        txtUlasan.Text = strUlasan

                        Using dt = fGetUserInfo(strNoStaf)
                            lblNmStafSokong.Text = dt.Rows(0)("MS01_Nama").ToString
                            lblJawSokong.Text = dt.Rows(0)("JawGiliran").ToString
                            lblKodPTjSokong.Text = dt.Rows(0)("KodPejabat").ToString & "0000"
                            lblNmPTjSokong.Text = dt.Rows(0)("Pejabat").ToString
                        End Using
                    End If
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
                            AR01_NOFAKS, AR01_TempohKontrak,AR01_TkhPeringatan1 ,AR01_TkhPeringatan2 , AR01_TkhPeringatan3, AR01_TkhLulus ,AR01_Emel,AR01_UtkPerhatian, AR01_StatusDok, (select Butiran  from AR_StatusDok where KodStatus = AR01_Bil.AR01_StatusDok ) as ButStatDok, AR01_Kontrak 
from AR01_Bil where AR01_NoBilSem='" & strNoInvSem & "'"
            Dim ds = dbconn.fSelectCommand(strSqlInv)

            Using dtMhn = ds.Tables(0)
                If dtMhn.Rows.Count > 0 Then
                    Dim strTkhMohon As String
                    strTkhMohon = dtMhn.Rows(0)("AR01_TkhMohon").ToString
                    If strTkhMohon <> "" Then
                        dtTkhMohon = CDate(strTkhMohon)
                        strTkhMohon = dtTkhMohon.ToString("dd/MM/yyyy")
                    End If

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
                    Dim TkhKonDr As String = (IIf(IsDBNull(dtMhn.Rows(0)("AR01_TkhKonDari")), Nothing, dtMhn.Rows(0)("AR01_TkhKonDari")))
                    Dim TkhKonHingga As String = (IIf(IsDBNull(dtMhn.Rows(0)("AR01_TkhKonHingga")), Nothing, dtMhn.Rows(0)("AR01_TkhKonHingga")))
                    Dim NoTel As String = IIf(IsDBNull(dtMhn.Rows(0)("AR01_NOTEL")), "-", dtMhn.Rows(0)("AR01_NOTEL"))
                    Dim NoFax As String = IIf(IsDBNull(dtMhn.Rows(0)("AR01_NOFAKS")), "-", dtMhn.Rows(0)("AR01_NOFAKS"))
                    Dim TmphKon As String = (IIf(IsDBNull(dtMhn.Rows(0)("AR01_TempohKontrak")), "-", dtMhn.Rows(0)("AR01_TempohKontrak")))
                    Dim TkhPeringatan1 As String = (IIf(IsDBNull(dtMhn.Rows(0)("AR01_TkhPeringatan1")), Nothing, dtMhn.Rows(0)("AR01_TkhPeringatan1")))
                    Dim TkhPeringatan2 As String = (IIf(IsDBNull(dtMhn.Rows(0)("AR01_TkhPeringatan2")), Nothing, dtMhn.Rows(0)("AR01_TkhPeringatan2")))
                    Dim TkhPeringatan3 As String = (IIf(IsDBNull(dtMhn.Rows(0)("AR01_TkhPeringatan3")), Nothing, dtMhn.Rows(0)("AR01_TkhPeringatan3")))
                    Dim TkhLulus As String = (IIf(IsDBNull(dtMhn.Rows(0)("AR01_TkhLulus")), Nothing, dtMhn.Rows(0)("AR01_TkhLulus")))
                    Dim Emel As String = dtMhn.Rows(0)("AR01_Emel")
                    Dim Perhatian As String = dtMhn.Rows(0)("AR01_UtkPerhatian")
                    Dim strJenKontrak As String = dtMhn.Rows(0)("AR01_Kontrak")


                    lblTkhMohon.Text = strTkhMohon
                    lblNoInvSem.Text = strNoInvSem
                    lblPtj.Text = strKodPTJ & " - " & dtMhn.Rows(0)("ButPTj")
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
                    lblUP.Text = IIf(Perhatian = "", "-", Perhatian)
                    lblBank.Text = strKodBank & " - " & dtMhn.Rows(0)("ButBank")
                    lblKat.Text = strKodKat & " - " & dtMhn.Rows(0)("ButKat")
                    lblNegara.Text = strKodNegara & " - " & dtMhn.Rows(0)("ButNegara")
                    lblNegeri.Text = strKodNegeri & " - " & dtMhn.Rows(0)("ButNegeri")
                    lblNmPen.Text = NPenerima
                    lblStat.Text = dtMhn.Rows(0)("ButStatDok")

                    lblKodPTjPmhn.Text = strKodPTJ
                    lblNmPTjPmhn.Text = dtMhn.Rows(0)("ButPTj")

                    If strJenKontrak = True Then
                        lblJenKontrak.Text = "Ya"
                        lblTkhMulaKon.Text = TkhKonDr
                        lblTkhTamatKon.Text = TkhKonHingga
                        lblTemKon.Text = TmphKon & " hari"
                    Else
                        lblJenKontrak.Text = "Tidak"
                        lblTkhMulaKon.Text = "-"
                        lblTkhTamatKon.Text = "-"
                        lblTemKon.Text = "-"
                    End If

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
            Dim strSql = "select KodKw,KodKO,KodPTJ,KodKP,KodVot,AR01_Perkara, CAST(AR01_Kuantiti AS int) as AR01_Kuantiti, AR01_kadarHarga,AR01_Jumlah from AR01_BilDt where AR01_NoBilSem='" & strNoInvSem & "'"

            Dim dbconn As New DBKewConn
            Dim ds As New DataSet
            ds = dbconn.fSelectCommand(strSql)

            Using MohonDt = ds.Tables(0)
                gvTrans.DataSource = MohonDt
                gvTrans.DataBind()
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

    Private Sub gvTrans_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvTrans.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim strJumlah As String = CType(e.Row.FindControl("lblJumlah"), Label).Text
                If strJumlah = "" Then strJumlah = "0.00"
                decJumlah += CDec(strJumlah)

            End If

            If e.Row.RowType = DataControlRowType.Footer Then
                Dim lblTotJum As Label = CType(e.Row.FindControl("lblTotJum"), Label)
                lblTotJum.Text = FormatNumber(decJumlah, 2)

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
        Dim strNoInvSem As String = Trim(txtCarian.Text.TrimEnd)
        fLoadLst()
    End Sub

    Private Sub ddlFilStat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFilStat.SelectedIndexChanged
        fLoadLst()
    End Sub

    Private Sub fClearInfo()
        lblNoInvSem.Text = ""
        lblTkhMohon.Text = ""
        lblPtj.Text = ""
        lblStat.Text = ""
        lblBank.Text = ""
        lblKat.Text = ""
        lblNmPen.Text = ""
        lblIdPen.Text = ""
        lblTujuan.Text = ""
        lblNoRuj.Text = ""
        lblUP.Text = ""
        lblAlmt1.Text = ""
        lblAlmt2.Text = ""
        lblNegara.Text = ""
        lblNegeri.Text = ""
        lblBandar.Text = ""
        lblPoskod.Text = ""
        lblNoTel.Text = ""
        lblNoFax.Text = ""
        lblEmel.Text = ""

        lblNoStafPmhn.Text = ""
        lblNmPmhn.Text = ""
        lblJwtanMhn.Text = ""
        lblKodPTjPmhn.Text = ""
        lblNmPTjPmhn.Text = ""

        lblNoStafSokong.Text = ""
        lblNmStafSokong.Text = ""
        lblJawSokong.Text = ""
        lblKodPTjSokong.Text = ""
        lblNmPTjSokong.Text = ""
        lblTkhSokong.Text = ""
        txtUlasan.Text = ""

        fClearGvTrans()
    End Sub

    Private Sub lbtnSokong_Click(sender As Object, e As EventArgs) Handles lbtnSokong.Click
        If fSokong() Then

            divList.Visible = True
            divDetail.Visible = False
            fLoadLst()
        End If
    End Sub

    Private Sub lbtnXSokong_Click(sender As Object, e As EventArgs) Handles lbtnXSokong.Click
        If txtUlasan.Text = "" Then
            fGlobalAlert("Sila masukkan ulasan!", Me.Page, Me.[GetType]())
            Exit Sub
        End If

        If fXSokong() Then
            fGlobalAlert("Permohonan invois telah disimpan sebagai 'Tidak Sokong'!", Me.Page, Me.[GetType]())
            lbtnSokong.Visible = False
            lbtnXSokong.Visible = False
        Else
            fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
        End If
    End Sub

    Private Function fSendEmail() As Boolean
        Dim strMsg As String = ""

        Try

            Dim strTo As String
            Dim strFrom As String
            Dim strsubject As String
            Dim strbody As String = ""
            Dim strAtt As String

            Dim strSql As String = "select Email from MK_TugasDt where KodSubMenu = '120401' and Status = 1"
            Dim dbconn As New DBKewConn
            Dim ds As New DataSet
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strTo = ds.Tables(0).Rows(0)("Email").ToString
                Else
                    fErrorLog("fSendEmail(), Err - Alamat emel gagal dijumpai. " & strSql)
                    Return False
                End If
            Else
                fErrorLog("fSendEmail(), Err - Alamat emel gagal dijumpai. " & strSql)
                Return False
            End If

            strsubject = "(SMKB) PERMOHONAN INVOIS"
            strbody += "<br> Pemohon : " & Trim(lblNoStafPmhn.Text.TrimEnd) & " - " & Trim(lblNmPmhn.Text.TrimEnd)
            strbody += "<br> PTj : " & Trim(lblKodPTjPmhn.Text.TrimEnd) & " - " & Trim(lblNmPTjPmhn.Text.TrimEnd)
            strbody += "<br><br>Email ini tidak perlu dibalas."

            strFrom = strSenderAdr  ' "smkbTest@utem.edu.my"
            'strTo = "hanafi.mohtar@utem.edu.my"  'Put semicolon(;) for multiple receipients i.e: "aaa@utem.edu.my;bbb@gmail.com"

            strSMTPServer = strSMTPServer  ' "smtp01.utem.edu.my"
            strSMTPPort = strSMTPPort ' "25"
            ' strAtt = "test.pdf;test2.pdf;test.txt;test.bmp;test.docx" 'Put semicolon(;) for multiple attachment i.e: "aaa.pdf;bbb.txt"

            strMsg = clsMail.fSendMail(strFrom, strTo, strsubject, strbody, strSMTPServer, strSMTPPort, strAtt)
            If strMsg = "1" Then
                Return True
            Else

                Return False
            End If


        Catch ex As Exception
            'clsSearch.sWriteErrorLog(Session("dwspath"), "(Check_In.aspx)fSendNotification() --> " & ex.Message)
            Return False
        End Try
    End Function

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