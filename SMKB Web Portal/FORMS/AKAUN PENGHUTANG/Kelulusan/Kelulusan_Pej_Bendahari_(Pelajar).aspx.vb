Imports System.Drawing
Imports System.Data.SqlClient
Imports System.IO
Public Class Kelulusan_Pej_Bendahari__Pelajar_
    Inherits System.Web.UI.Page

    Dim sLogBaru As String
    Dim sLogMedan As String
    Dim strNoStaf As String = UserInfo.strSesStaffID
    Dim sLogLevel As String = UserInfo.strUsrTahap
    Dim lsLogUsrIP As String = UserInfo.strUsrPcIP
    Dim lsLogUsrPC As String = UserInfo.strUsrPCName
    Private dbconn As New DBKewConn()
    Private dbconnSMSM As New DBSMConn()
    Dim ds As New DataSet
    Dim ds1 As New DataSet
    Private countButiran As Integer = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Session("sesKodSubMenu") = Request.QueryString("KodSubMenu")
            divList.Visible = True
            divWiz.Visible = False

            lblTkhBil.Text = Now.ToString("dd/MM/yyyy")

            fLoadKW()
            fLoadKO2()
            fLoadPTj2()
            fLoadKP2()
            fLoadVot2()

            fBindDdlBank()
            fBindDdlFilStat()

            sLoadLst()
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
            Dim strSql As String = "select KodStatus, Butiran from AR_StatusDok where KodStatus in (01,03,05)"

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

    'Private Sub sSetLulus()
    '    lblKodPTjPel.Text = Session("ssusrKodPTj")
    '    lblNmPTjPel.Text = Session("ssusrPTj")
    '    lblNoStafPel.Text = Session("ssusrID")
    '    lblNmStafPel.Text = Session("ssusrName")
    '    lblJawPel.Text = Session("ssusrPost")
    '    txtUlasan.Text = ""

    '    Dim strTkhToday As String = Now.ToString("dd/MM/yyyy")
    '    lblTkhLulus.Text = strTkhToday
    'End Sub
    Private Sub fBindDdlBank()
        Try
            Dim strSql As String
            strSql = "select KodBank, (KodBank + '-' + NamaBank) as NamaBank from VMK_Bank01 order by kodbank"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)
            ddlBank.DataSource = ds
            ddlBank.DataTextField = "NamaBank"
            ddlBank.DataValueField = "KodBank"
            ddlBank.DataBind()

            ddlBank.Items.Insert(0, New ListItem(" - SILA PILIH -", 0))
            ddlBank.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub sLoadLst()
        Dim intRec As Integer = 0
        Dim strStat As String
        Dim strFilter As String
        Try

            sClearGvSenInv()

            If ddlCarian.SelectedValue = 1 Then
                strFilter = " and AR01_NoBilSem = '" & Trim(txtCarian.Text.TrimEnd) & "'"
            End If

            If ddlFilStat.SelectedValue = 0 Then
                strStat = "01,03, 05"
            Else
                strStat = ddlFilStat.SelectedValue
            End If

            Dim strSql As String = "select a.AR01_IdBil, a.AR01_NoBilSem,AR01_NoBil, Convert(varchar, a.AR01_TkhMohon, 103) As AR01_TkhMohon, a.AR01_Tujuan, a.AR01_NamaPenerima, AR01_Kategori, (select Butiran  from MK_KategoriPenerima where Kod = a.AR01_Kategori ) as ButKat, a.ar01_Jumlah, AR01_TkhMohon  as Tarikh_Bil, ar01_statusdok, (Select B.Butiran  from AR_StatusDok B where B.KodStatus = A.AR01_StatusDok) as ButStatus
from AR01_Bil a
where ar01_statusdok in (" & strStat & ") and ar01_jenis='02' " & strFilter

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
                    ViewState("dtLst") = dt
                    gvLst.DataSource = dt
                    gvLst.DataBind()
                    intRec = ds.Tables(0).Rows.Count
                End If
            End If

            lblJumRekod.InnerText = intRec

        Catch ex As Exception

        End Try
    End Sub

    Private Sub sClearGvSenInv()
        gvLst.DataSource = New List(Of String)
        gvLst.DataBind()
    End Sub


    Private Sub sClearGvTrans()
        gvTrans.DataSource = New List(Of String)
        gvTrans.DataBind()
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
    'Private Sub firstBindGVInvCuk()
    '    Try
    '        Dim dt As New DataTable()
    '        dt.Columns.AddRange(New DataColumn() {
    '                            New DataColumn("AR01_BilDtID", GetType(Integer)),
    '                            New DataColumn("AR01_Bil", GetType(Integer)),
    '                            New DataColumn("AR01_NoBilSem", GetType(Integer)),
    '                            New DataColumn("KodKw", GetType(String)),
    '                            New DataColumn("KodKO", GetType(String)),
    '                            New DataColumn("kodPTJ", GetType(String)),
    '                            New DataColumn("kodKP", GetType(String)),
    '                            New DataColumn("kodVot", GetType(String)),
    '                            New DataColumn("AR01_Perkara", GetType(String)),
    '                            New DataColumn("AR01_Kuantiti", GetType(Integer)),
    '                            New DataColumn("AR01_KadarHarga", GetType(Decimal)),
    '                            New DataColumn("AR01_Jumlah", GetType(Decimal))})

    '        ViewState("vsDtInv") = dt
    '        BindGvViewButiran()
    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Protected Sub BindGvViewButiran()
    '    Try
    '        Dim dt = DirectCast(ViewState("vsDtInv"), DataTable)
    '        gvTrans.DataSource = dt
    '        gvTrans.DataBind()
    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Jana No.Invois
    'Function fGetID_Bil()
    '    Try
    '        Dim strSql As String
    '        Dim intLastIdx As Integer
    '        Dim strCol As String
    '        Dim strTahun As String = Now.Year
    '        Dim strKodModul As String
    '        Dim strPrefix As String
    '        Dim strButiran As String = "Max No Sem Kewangan AR"
    '        Dim strIdx As String

    '        strSql = "select * from mk_noakhir where tahun = '" & strTahun & "' and kodmodul='AR' and prefix='K'"
    '        strCol = "NoAkhir"
    '        strKodModul = "AR"
    '        strPrefix = "K"

    '        Dim ds As New DataSet
    '        Dim dbconn As New DBKewConn
    '        ds = dbconn.fSelectCommand(strSql)

    '        If Not ds Is Nothing Then
    '            If ds.Tables(0).Rows.Count > 0 Then
    '                intLastIdx = CInt(ds.Tables(0).Rows(0)(strCol).ToString)
    '            Else
    '                intLastIdx = 0
    '            End If

    '            If intLastIdx = 0 Then
    '                strIdx = strPrefix & "000001" & Now.ToString("MM") & Now.ToString("yy")

    '                strSql = " insert into mk_noakhir (KodModul , Prefix, NoAkhir, Tahun, Butiran, KodPTJ)" &
    '                "values(@kodmodul,@prefix,@noakhir,@tahun,@butiran,@kodPTJ)"

    '                Dim paramSql() As SqlParameter = {
    '                    New SqlParameter("@kodmodul", strKodModul),
    '                    New SqlParameter("@prefix", strPrefix),
    '                    New SqlParameter("@noakhir", 1),
    '                    New SqlParameter("@tahun", strTahun),
    '                    New SqlParameter("@butiran", strButiran),
    '                    New SqlParameter("@kodPTJ", "-")
    '                }

    '                dbconn = New DBKewConn
    '                dbconn.sConnBeginTrans()
    '                If dbconn.fInsertCommand(strSql, paramSql) > 0 Then
    '                    dbconn.sConnCommitTrans()
    '                    Return strIdx
    '                Else
    '                    dbconn.sConnRollbackTrans()
    '                    Return Nothing
    '                End If
    '            Else

    '                intLastIdx = intLastIdx + 1
    '                strIdx = strPrefix & intLastIdx.ToString("D6") & Now.ToString("MM") & Now.ToString("yy")

    '                strSql = "update MK_NoAkhir set NoAkhir =@noakhir where tahun =@tahun and KodModul =@kodmodul and prefix = @prefix"

    '                Dim paramSql2() As SqlParameter = {
    '                            New SqlParameter("@noakhir", intLastIdx),
    '                            New SqlParameter("@tahun", strTahun),
    '                            New SqlParameter("@kodmodul", strKodModul),
    '                            New SqlParameter("@prefix", strPrefix)
    '                            }

    '                dbconn.sConnBeginTrans()
    '                If dbconn.fUpdateCommand(strSql, paramSql2) > 0 Then
    '                    dbconn.sConnCommitTrans()
    '                    Return strIdx
    '                Else
    '                    dbconn.sConnRollbackTrans()
    '                    Return Nothing
    '                End If

    '            End If
    '        End If
    '    Catch ex As Exception
    '        Return Nothing
    '    End Try
    'End Function
    Function sClear()
        Try
            lblIdPenerima.Text = ""
            lblTujuan.Text = ""
            lblNoRuj.Text = ""
            lblAlmt1.Text = ""
            lblAlmt2.Text = ""
            lblBandar.Text = ""
            lblPoskod.Text = ""
            lblNoTel.Text = ""
            lblNoFax.Text = ""
            lblEmail.Text = ""
            lblUP.Text = ""
            lblNamaP.Text = ""
            sClearGvTrans()

        Catch ex As Exception

        End Try
    End Function

    Dim strTotJumlah As String
    Dim decJumlah As Decimal

    Private Sub sLoadInv(ByVal strNoInvCuk As String)

        Dim dbconn As New DBKewConn
        Try
            Dim strSqlInv = "Select  AR01_TkhMohon,AR01_KODPTJMOHON, (select MK_PTJ.Butiran from MK_PTJ where MK_PTJ.KodPTJ = AR01_Bil.AR01_KodPTJMohon) as ButPTj, AR01_NOSTAF, AR01_Jenis,AR01_KODBANK,AR01_NORUJUKAN,AR01_ALMT1,AR01_ALMT2,
AR01_KATEGORI,AR01_IDPENERIMA, AR01_NAMAPENERIMA, kodnegeri,(select MK_Negeri.Butiran from MK_Negeri where MK_Negeri.KodNegeri = AR01_Bil.kodnegeri) as Butnegeri,kodnegara, (select MK_Negara.Butiran from MK_Negara where MK_Negara.KodNegara = AR01_Bil.KodNegara) as Butnegara, AR01_TUJUAN, AR01_BANDAR,AR01_POSKOD, AR01_TkhKonDari,AR01_TkhKonHingga,AR01_NOTEL, 
AR01_NOFAKS, AR01_TempohKontrak, AR01_Emel,AR01_UtkPerhatian, AR01_StatusDok, (select Butiran  from AR_StatusDok where KodStatus = AR01_Bil.AR01_StatusDok  ) as ButStatDok, AR01_JenisPljr, AR01_StatPel , AR01_KodSesi
from AR01_Bil 
where AR01_NoBilSem='" & strNoInvCuk & "'"

            'Load Permohonan Invois
            Dim ds = dbconn.fSelectCommand(strSqlInv)

            Using dtMhn = ds.Tables(0)
                If dtMhn.Rows.Count > 0 Then
                    Dim TkhMhn As Date = dtMhn.Rows(0)("AR01_TkhMohon")
                    Dim strKodPTJ As String = dtMhn.Rows(0)("AR01_KODPTJMOHON")
                    Dim strButpTj As String = dtMhn.Rows(0)("ButPTj")
                    Dim NoPmhn As String = dtMhn.Rows(0)("AR01_NOSTAF")
                    Dim katInv As String = dtMhn.Rows(0)("AR01_Jenis")
                    Dim Bank As String = dtMhn.Rows(0)("AR01_KODBANK")
                    Dim NoRuj As String = dtMhn.Rows(0)("AR01_NORUJUKAN")
                    Dim Almt1 As String = IIf(IsDBNull(dtMhn.Rows(0)("AR01_ALMT1")), "-", dtMhn.Rows(0)("AR01_ALMT1"))
                    Dim Almt2 As String = IIf(IsDBNull(dtMhn.Rows(0)("AR01_ALMT2")), "-", dtMhn.Rows(0)("AR01_ALMT2"))
                    Dim Kat As String = dtMhn.Rows(0)("AR01_KATEGORI")
                    Dim NPenerima As String = dtMhn.Rows(0)("AR01_NAMAPENERIMA")
                    Dim Negara As String = IIf(IsDBNull(dtMhn.Rows(0)("Butnegara")), "-", dtMhn.Rows(0)("Butnegara"))
                    Dim Negeri As String = IIf(IsDBNull(dtMhn.Rows(0)("Butnegeri")), "-", dtMhn.Rows(0)("Butnegeri"))
                    Dim IDPenerima As String = dtMhn.Rows(0)("AR01_IDPENERIMA")
                    Dim Tujuan As String = dtMhn.Rows(0)("AR01_TUJUAN")
                    Dim Bandar As String = IIf(IsDBNull(dtMhn.Rows(0)("AR01_BANDAR")), "-", dtMhn.Rows(0)("AR01_BANDAR"))
                    Dim Poskod As String = IIf(IsDBNull(dtMhn.Rows(0)("AR01_POSKOD")), "-", dtMhn.Rows(0)("AR01_POSKOD"))
                    Dim NoTel As String = IIf(IsDBNull(dtMhn.Rows(0)("AR01_NOTEL")), "-", dtMhn.Rows(0)("AR01_NOTEL"))
                    Dim NoFax As String = IIf(IsDBNull(dtMhn.Rows(0)("AR01_NOFAKS")), "-", dtMhn.Rows(0)("AR01_NOFAKS"))
                    Dim Emel As String = IIf(IsDBNull(dtMhn.Rows(0)("AR01_Emel")), "-", dtMhn.Rows(0)("AR01_Emel"))
                    Dim Perhatian As String = IIf(IsDBNull(dtMhn.Rows(0)("AR01_UtkPerhatian")), "-", dtMhn.Rows(0)("AR01_UtkPerhatian"))
                    Dim strStatDok As String = IIf(IsDBNull(dtMhn.Rows(0)("AR01_StatusDok")), "-", dtMhn.Rows(0)("AR01_StatusDok"))

                    Dim strKatPel As String = dtMhn.Rows(0)("AR01_JenisPljr")
                    Dim strStatPel As String = dtMhn.Rows(0)("AR01_StatPel")
                    Dim strKodSesi As String = dtMhn.Rows(0)("AR01_KodSesi")

                    lblStatus.Text = dtMhn.Rows(0)("ButStatDok")
                    lblTkhMohon.Text = TkhMhn

                    lblKodPTJP.Text = strKodPTJ
                    lblNPTJP.Text = strButpTj

                    lblKodKatPel.Text = strKatPel

                    If strKatPel = "UG" Then
                        lblKatPel.Text = "Pra Siswazah"
                    ElseIf strKatPel = "PG" Then
                        lblKatPel.Text = "Pasca Siswazah"
                    End If

                    'sLoadPel()

                    lblNoInvSem.Text = strNoInvCuk
                    lblIdPenerima.Text = IDPenerima
                    lblNamaP.Text = NPenerima
                    lblTujuan.Text = Tujuan                 
                    lblNoRuj.Text = IIf(NoRuj = "", "-", NoRuj)
                    lblAlmt1.Text = IIf(Almt1 = "", "-", Almt1)
                    lblAlmt2.Text = IIf(Almt2 = "", "-", Almt2)
                    lblBandar.Text = IIf(Bandar = "", "-", Bandar)
                    lblPoskod.Text = IIf(Poskod = "", "-", Poskod)
                    lblNegeri.Text = IIf(Negeri = "", "-", Negeri)
                    lblNegara.Text = IIf(Negara = "", "-", Negara)
                    lblNoTel.Text = IIf(NoTel = "", "-", NoTel)
                    lblNoFax.Text = IIf(NoFax = "", "-", NoFax)
                    lblEmail.Text = IIf(Emel = "", "-", Emel)
                    lblUP.Text = IIf(Perhatian = "", "-", Perhatian)
                    lblStatPel.Text = strStatPel
                    ddlBank.SelectedValue = Bank
                    lblSesi.Text = strKodSesi
                End If
            End Using

        Catch ex As Exception

        End Try

    End Sub

    Private Sub sLoadMohon(ByVal strNoInvSem As String)
        Try
            Dim dbconn As New DBKewConn
            Dim strNoStaf As String

            Dim strSql As String = "select AR06_NoStaf, AR06_Ulasan from AR06_StatusDok where AR06_NoBilSem = '" & strNoInvSem & "' and AR06_StatusDok = 01"

            Using ds = dbconn.fSelectCommand(strSql)
                If Not ds Is Nothing Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        strNoStaf = ds.Tables(0).Rows(0)("AR06_NoStaf").ToString
                        lblNoPmhn.Text = strNoStaf

                        Using dt = fGetUserInfo(strNoStaf)
                            lblNmPemohon.Text = dt.Rows(0)("MS01_Nama").ToString
                            lblJawatan.Text = dt.Rows(0)("JawGiliran").ToString
                            lblKodPTjPmhn.Text = dt.Rows(0)("KodPejabat").ToString & "0000"
                            lblNmPTjPmhn.Text = dt.Rows(0)("Pejabat").ToString
                        End Using
                    End If
                End If
            End Using

        Catch ex As Exception

        End Try
    End Sub

    Dim strTotJumlah2 As String
    Dim decJumlah2 As Decimal

    '    Private Function fLulus() As Boolean
    '        Dim strSql As String
    '        Dim dbConn As New DBKewConn
    '        Dim blnSuccess As Boolean = True
    '        Dim strStatDok As String = "03"
    '        Dim strNoBil As String
    '        Dim strNoBilSem As String = Trim(lblNoInvSem.Text.TrimEnd)
    '        Dim decTotJum As Decimal
    '        Dim intIdBil As Integer = CInt(hidIdBil.Value)
    '        Dim strTahun As String = Now.Year.ToString
    '        Dim strKodJen, strKodJenLanjut As String
    '        Dim strKodKW2, strKodKO2, strKodKP2 As String
    '        Dim strTkhToday As String = Now.ToString("yyyy-MM-dd")
    '        Dim dtTkhToday As DateTime = CDate(strTkhToday)
    '        Dim paramSql() As SqlParameter

    '        Try
    '            Dim strKodBank As String = Trim(ddlBank.SelectedValue.TrimEnd)
    '            Dim strTkhLulus As String = Now.ToString("yyyy-MM-dd")
    '            Dim strNoStaf As String = Session("ssusrID")

    '            Dim strTujuan = Trim(lblTujuan.Text.TrimEnd)

    '            If String.IsNullOrEmpty(strNoStaf) OrElse strNoStaf Is Nothing Then
    '                blnSuccess = False
    '                Exit Try
    '            End If

    '            dbConn.sConnBeginTrans()

    '            For Each gvRow As GridViewRow In gvTrans.Rows
    '                Dim decJumTrans As Decimal = CDec(CType(gvRow.FindControl("lblJumlah"), Label).Text)
    '                decTotJum += decJumTrans
    '            Next

    '            'JANA NO. BIL
    '            Dim strKodModul
    '            Dim strPrefix
    '            Dim strButiranNoAkhir As String


    '            Dim strKodKatPel As String = Trim(lblKodKatPel.Text.TrimEnd)

    '            If strKodKatPel = "UG" Then
    '                strKodModul = "AR"
    '                strPrefix = "U"
    '                strButiranNoAkhir = "Max No Pelajar Undergrade AR"

    '            ElseIf strKodKatPel = "PG" Then
    '                strKodModul = "AR"
    '                strPrefix = "P"
    '                strButiranNoAkhir = "Max No Pelajar Postgrade AR"

    '            End If

    '            strNoBil = fGetNo(strTahun, strKodModul, strPrefix, strButiranNoAkhir)

    '            If strNoBil Is Nothing Then
    '                blnSuccess = False
    '                Exit Try
    '            End If

    '            'UPDATE AR01_Bil
    '            strSql = "update AR01_Bil set AR01_NOBIL = @NOBIL, AR01_KODBANK = @KodBank,AR01_StatusDok = @AR01_StatusDok, AR01_TkhLulus = @TkhLulus, AR01_JUMLAH = @Jumlah, AR01_JUMBLMBYR= @JumBlmByr  where AR01_NoBilSem =@NoInvSem"

    '            Dim paramSql2() As SqlParameter = {
    '                New SqlParameter("@NOBIL", strNoBil),
    '                New SqlParameter("@KodBank", strKodBank),
    '                    New SqlParameter("@AR01_StatusDok", strStatDok),
    '                    New SqlParameter("@TkhLulus", strTkhLulus),
    '                    New SqlParameter("@NoInvSem", strNoBilSem),
    '                    New SqlParameter("@Jumlah", decTotJum),
    '                    New SqlParameter("@JumBlmByr", decTotJum)
    '                }

    '            If Not dbConn.fUpdateCommand(strSql, paramSql2) > 0 Then
    '                blnSuccess = False
    '                Exit Try
    '            Else
    '                'AUDIT LOG
    '                sLogMedan = "AR01_NOBIL|AR01_KODBANK|AR01_StatusDok|AR01_TkhLulus|AR01_JUMLAH|AR01_JUMBLMBYR|AR01_NoBilSem"
    '                sLogBaru = strNoBil & "|" & strKodBank & "|" & strStatDok & "|" & strTkhLulus & "|" & strNoBilSem & "|" & decTotJum & "|" & decTotJum & ""

    '                strSql = "insert into MK_AuditLog (UserID, UserGroup, UserUbah, SubMenu, Keterangan, InfoTable, RefKey, RefNo, InfoMedan, InfoBaru, " _
    '    & " InfoLama, UserIP, PCName) " _
    '    & " values (@UserID,@UserGroup,@UserUbah,@SubMenu,@Keterangan,@InfoTable,@RefKey, @RefNo,@InfoMedan,@InfoBaru,@InfoLama, " _
    '    & " @UserIP,@PCName)"

    '                Dim paramSqlLog() As SqlParameter = {
    '                    New SqlParameter("@UserID", strNoStaf),
    '                    New SqlParameter("@UserGroup", sLogLevel),
    '                    New SqlParameter("@UserUbah", "-"),
    '                     New SqlParameter("@SubMenu", Session("sesKodSubMenu")),
    '                    New SqlParameter("@Keterangan", "UPDATE"),
    '                    New SqlParameter("@InfoTable", "AR01_Bil"),
    '                    New SqlParameter("@RefKey", "-"),
    '                    New SqlParameter("@RefNo", "-"),
    '                    New SqlParameter("@InfoMedan", sLogMedan),
    '                    New SqlParameter("@InfoBaru", sLogBaru),
    '                    New SqlParameter("@InfoLama", "-"),
    '                    New SqlParameter("@UserIP", lsLogUsrIP),
    '                    New SqlParameter("@PCName", lsLogUsrPC)
    '                }

    '                If Not dbConn.fInsertCommand(strSql, paramSqlLog) > 0 Then
    '                    blnSuccess = False
    '                    Exit Try
    '                End If
    '            End If

    '            'PADAM REKOD DALAM TABLE AR01_BilDT
    '            strSql = "delete from AR01_BilDT where AR01_IdBil = " & intIdBil & " and AR01_NoBilSem = '" & strNoBilSem & "'"
    '            If Not dbConn.fUpdateCommand(strSql) > 0 Then
    '                blnSuccess = False
    '                Exit Try
    '            Else
    '                'AUDIT LOG
    '                sLogMedan = "AR01_IdBil|AR01_NoBilSem"
    '                sLogBaru = intIdBil & "|" & strNoBilSem & ""

    '                strSql = "insert into MK_AuditLog (UserID, UserGroup, UserUbah, SubMenu, Keterangan, InfoTable, RefKey, RefNo, InfoMedan, InfoBaru, " _
    '    & " InfoLama, UserIP, PCName) " _
    '    & " values (@UserID,@UserGroup,@UserUbah,@SubMenu,@Keterangan,@InfoTable,@RefKey, @RefNo,@InfoMedan,@InfoBaru,@InfoLama, " _
    '    & " @UserIP,@PCName)"

    '                Dim paramSqlLog() As SqlParameter = {
    '                    New SqlParameter("@UserID", strNoStaf),
    '                    New SqlParameter("@UserGroup", sLogLevel),
    '                    New SqlParameter("@UserUbah", "-"),
    '                     New SqlParameter("@SubMenu", Session("sesKodSubMenu")),
    '                    New SqlParameter("@Keterangan", "DELETE"),
    '                    New SqlParameter("@InfoTable", "AR01_BilDT"),
    '                    New SqlParameter("@RefKey", "-"),
    '                    New SqlParameter("@RefNo", "-"),
    '                    New SqlParameter("@InfoMedan", sLogMedan),
    '                    New SqlParameter("@InfoBaru", sLogBaru),
    '                    New SqlParameter("@InfoLama", "-"),
    '                    New SqlParameter("@UserIP", lsLogUsrIP),
    '                    New SqlParameter("@PCName", lsLogUsrPC)
    '                }

    '                If Not dbConn.fInsertCommand(strSql, paramSqlLog) > 0 Then
    '                    blnSuccess = False
    '                    Exit Try
    '                End If
    '            End If

    '            Dim KodDok As String = "BIL"
    '            Dim KODAP As String = "-"
    '            Dim Status As String = "0"

    '            Dim BulanTran As String = Now.Month.ToString
    '            Dim intBil As Integer
    '            Dim strKodKW, strKodKO, strPTJ, strKodKP, strKodVot, strPerkara As String
    '            Dim decKdrHrg, decAmt As Decimal
    '            Dim intKuantiti As Integer

    '            For Each gvTransInvrow As GridViewRow In gvTrans.Rows
    '                intBil = intBil + 1
    '                Dim strIdDt As String = TryCast(gvTransInvrow.FindControl("lblId"), Label).Text
    '                Dim intDtId As Integer
    '                If strIdDt = "" Then intDtId = 0 Else intDtId = CInt(strIdDt)

    '                strKodKW = TryCast(gvTransInvrow.FindControl("lblKW"), Label).Text
    '                strKodKO = TryCast(gvTransInvrow.FindControl("lblKO"), Label).Text
    '                strPTJ = TryCast(gvTransInvrow.FindControl("lblPTj"), Label).Text
    '                strKodKP = TryCast(gvTransInvrow.FindControl("lblKP"), Label).Text
    '                strKodVot = TryCast(gvTransInvrow.FindControl("lblVot"), Label).Text
    '                strPerkara = TryCast(gvTransInvrow.FindControl("lblPerkara"), Label).Text
    '                intKuantiti = CInt(TryCast(gvTransInvrow.FindControl("lblKuantiti"), Label).Text)
    '                decKdrHrg = CDec(TryCast(gvTransInvrow.FindControl("lblHarga"), Label).Text)
    '                decAmt = CDec(TryCast(gvTransInvrow.FindControl("lblJumlah"), Label).Text)

    '                'MASUK REKOD TRANSAKSI KE DALAM TABLE AR01_BilDT
    '                strSql = " INSERT INTO AR01_BilDT(AR01_IdBil, AR01_NoBilSem,AR01_NoBil,AR01_Bil,KodKw,KodKO,KodPTJ,KodKP,KodVot,AR01_Perkara,AR01_Kuantiti,AR01_kadarHarga,AR01_Jumlah)" &
    '                             "values (@IdBil,@NOBILSEM, @NOBIL,@BIL,@kodKw, @KodKO, @KodPTJ, @KodKP, @KodVot, @Perkara, @Kuantiti,@kadarHarga,@Jumlah)"

    '                paramSql =
    '                        {
    '                        New SqlParameter("@IdBil", intIdBil),
    '                         New SqlParameter("@NOBILSEM", strNoBilSem),
    '                         New SqlParameter("@NOBIL", strNoBil),
    '                         New SqlParameter("@BIL", intBil),
    '                         New SqlParameter("@kodKw", strKodKW),
    '                         New SqlParameter("@KodKO", strKodKO),
    '                         New SqlParameter("@KodPTJ", strPTJ),
    '                         New SqlParameter("@KodKP", strKodKP),
    '                         New SqlParameter("@KodVot", strKodVot),
    '                         New SqlParameter("@Perkara", strPerkara),
    '                         New SqlParameter("@Kuantiti", intKuantiti),
    '                         New SqlParameter("@kadarHarga", decKdrHrg),
    '                         New SqlParameter("@Jumlah", decAmt)
    '                        }

    '                If Not dbConn.fInsertCommand(strSql, paramSql) > 0 Then
    '                    blnSuccess = False
    '                    Exit Try
    '                Else
    '                    'AUDIT LOG
    '                    sLogMedan = "AR01_IdBil| AR01_NoBilSem|AR01_NoBil|AR01_Bil|KodKw|KodKO|KodPTJ|KodKP|KodVot|AR01_Perkara|AR01_Kuantiti|AR01_kadarHarga|AR01_Jumlah"
    '                    sLogBaru = intIdBil & "|" & strNoBilSem & "|" & strNoBil & "|" & intBil & "|" & strKodKW & "|" & strKodKO & "|" & strPTJ & "|" & strKodKP & "|" & strKodVot & "|" & strPerkara & "|" & intKuantiti & "|" & decKdrHrg & "|" & decAmt & ""

    '                    strSql = "insert into MK_AuditLog (UserID, UserGroup, UserUbah, SubMenu, Keterangan, InfoTable, RefKey, RefNo, InfoMedan, InfoBaru, " _
    '        & " InfoLama, UserIP, PCName) " _
    '        & " values (@UserID,@UserGroup,@UserUbah,@SubMenu,@Keterangan,@InfoTable,@RefKey, @RefNo,@InfoMedan,@InfoBaru,@InfoLama, " _
    '        & " @UserIP,@PCName)"

    '                    Dim paramSqlLog() As SqlParameter = {
    '                        New SqlParameter("@UserID", strNoStaf),
    '                        New SqlParameter("@UserGroup", sLogLevel),
    '                        New SqlParameter("@UserUbah", "-"),
    '                         New SqlParameter("@SubMenu", Session("sesKodSubMenu")),
    '                        New SqlParameter("@Keterangan", "INSERT"),
    '                        New SqlParameter("@InfoTable", "AR01_BilDT"),
    '                        New SqlParameter("@RefKey", "-"),
    '                        New SqlParameter("@RefNo", "-"),
    '                        New SqlParameter("@InfoMedan", sLogMedan),
    '                        New SqlParameter("@InfoBaru", sLogBaru),
    '                        New SqlParameter("@InfoLama", "-"),
    '                        New SqlParameter("@UserIP", lsLogUsrIP),
    '                        New SqlParameter("@PCName", lsLogUsrPC)
    '                    }

    '                    If Not dbConn.fInsertCommand(strSql, paramSqlLog) > 0 Then
    '                        blnSuccess = False
    '                        Exit Try
    '                    End If
    '                End If

    '                'POST KE MK06_TRANSAKSI =======================================================


    '                fGetKodJen(strKodVot, strKodJen, strKodJenLanjut)

    '                strSql = "INSERT INTO mk06_transaksi(mk06_tkhtran,mk06_rujukan,mk06_nodok,mk06_butiran,koddok,mk06_kodap,mk06_bil,kodkw,KodKO,kodptj,KodKP,kodvot,kodjen,kodjenlanjut,mk06_debit,mk06_kredit,mk06_bulan,mk06_tahun,mk06_status)
    '                            VALUES (@dtTkhToday,@NOBILSEM,@NOBIL,@TUJUAN,@KodDok,@KODAP,@BIL,@kodKw,@KodKO,@KodPTJ,@KodKP,@KodVot,@kodJen,@KodJenLanjut,@db,@Kt,@BulanTran,@Tahun,@Status)"

    '                paramSql =
    '                    {
    '                     New SqlParameter("@dtTkhToday", dtTkhToday),
    '                     New SqlParameter("@NOBILSEM", strNoBil),
    '                     New SqlParameter("@NOBIL", strNoBil),
    '                     New SqlParameter("@TUJUAN", strPerkara),
    '                     New SqlParameter("@KodDok", KodDok),
    '                     New SqlParameter("@KODAP", KODAP),
    '                     New SqlParameter("@BIL", intBil),
    '                     New SqlParameter("@kodKw", strKodKW),
    '                     New SqlParameter("@KodKO", strKodKO),
    '                     New SqlParameter("@KodPTJ", strPTJ),
    '                     New SqlParameter("@KodKP", strKodKP),
    '                     New SqlParameter("@KodVot", strKodVot),
    '                     New SqlParameter("@kodJen", strKodJen),
    '                     New SqlParameter("@KodJenLanjut", strKodJenLanjut),
    '                     New SqlParameter("@db", 0.00),
    '                     New SqlParameter("@Kt", decAmt),
    '                     New SqlParameter("@BulanTran", BulanTran),
    '                     New SqlParameter("@Tahun", strTahun),
    '                     New SqlParameter("@Status", Status)
    '                    }

    '                If Not dbConn.fInsertCommand(strSql, paramSql) > 0 Then
    '                    blnSuccess = False
    '                    Exit Try
    '                Else
    '                    'AUDIT LOG
    '                    sLogMedan = "mk06_tkhtran|mk06_rujukan|mk06_nodok|mk06_butiran|koddok|mk06_kodap|mk06_bil|kodkw|KodKO|kodptj|KodKP|kodvot|kodjen|kodjenlanjut|mk06_debit|mk06_kredit|mk06_bulan|mk06_tahun|mk06_status"
    '                    sLogBaru = dtTkhToday & "|" & strNoBilSem & "|" & strNoBil & "|" & strTujuan & "|" & KodDok & "|" & intBil & "|" & strKodKW & "|" & strKodKO & "|" & strPTJ & "|" & strKodKP & "|" & strKodVot & "|" & strKodJen & "|" & strKodJenLanjut & "|0.00|" & decAmt & "|" & BulanTran & "|" & strTahun & "|" & Status & ""

    '                    strSql = "insert into MK_AuditLog (UserID, UserGroup, UserUbah, SubMenu, Keterangan, InfoTable, RefKey, RefNo, InfoMedan, InfoBaru, " _
    '        & " InfoLama, UserIP, PCName) " _
    '        & " values (@UserID,@UserGroup,@UserUbah,@SubMenu,@Keterangan,@InfoTable,@RefKey, @RefNo,@InfoMedan,@InfoBaru,@InfoLama, " _
    '        & " @UserIP,@PCName)"

    '                    Dim paramSqlLog() As SqlParameter = {
    '                        New SqlParameter("@UserID", strNoStaf),
    '                        New SqlParameter("@UserGroup", sLogLevel),
    '                        New SqlParameter("@UserUbah", "-"),
    '                         New SqlParameter("@SubMenu", Session("sesKodSubMenu")),
    '                        New SqlParameter("@Keterangan", "INSERT"),
    '                        New SqlParameter("@InfoTable", "mk06_transaksi"),
    '                        New SqlParameter("@RefKey", "-"),
    '                        New SqlParameter("@RefNo", "-"),
    '                        New SqlParameter("@InfoMedan", sLogMedan),
    '                        New SqlParameter("@InfoBaru", sLogBaru),
    '                        New SqlParameter("@InfoLama", "-"),
    '                        New SqlParameter("@UserIP", lsLogUsrIP),
    '                        New SqlParameter("@PCName", lsLogUsrPC)
    '                    }

    '                    If Not dbConn.fInsertCommand(strSql, paramSqlLog) > 0 Then
    '                        blnSuccess = False
    '                        Exit Try
    '                    End If
    '                End If

    '                'end POST KE MK06_TRANSAKSI=======================================================

    '            Next

    '            'POST KE  
    '            ' Dim strKodJen, strKodJenLanjut As String
    '            intBil = intBil + 1
    '            strKodKW2 = "07"
    '            strPTJ = "50000"
    '            strKodVot = "71901"
    '            strKodKO2 = "00"
    '            strKodKP2 = "0000000"
    '            fGetKodJen(strKodVot, strKodJen, strKodJenLanjut)

    '            strSql = "INSERT INTO mk06_transaksi(mk06_tkhtran,mk06_rujukan,mk06_nodok,mk06_butiran,koddok,mk06_kodap,mk06_bil,kodkw,KodKO,kodptj,KodKP,kodvot,kodjen,kodjenlanjut,mk06_debit,mk06_kredit,mk06_bulan,mk06_tahun,mk06_status)
    '                            VALUES (@dtTkhToday,@Rujukan,@NoDok,@mk06_butiran,@KodDok,@KODAP,@BIL,@kodKw,@KodKO,@KodPTJ,@KodKP,@KodVot,@kodJen,@KodJenLanjut,@db,@Kt,@BulanTran,@Tahun,@Status)"

    '            paramSql =
    '                    {
    '                     New SqlParameter("@dtTkhToday", dtTkhToday),
    '                     New SqlParameter("@Rujukan", strNoBil),
    '                     New SqlParameter("@NoDok", strNoBil),
    '                     New SqlParameter("@mk06_butiran", strTujuan),
    '                     New SqlParameter("@KodDok", KodDok),
    '                     New SqlParameter("@KODAP", KODAP),
    '                     New SqlParameter("@BIL", intBil),
    '                     New SqlParameter("@kodKw", strKodKW2),
    '                     New SqlParameter("@KodKO", strKodKO2),
    '                     New SqlParameter("@KodPTJ", strPTJ),
    '                     New SqlParameter("@KodKP", strKodKP2),
    '                     New SqlParameter("@KodVot", strKodVot),
    '                     New SqlParameter("@kodJen", strKodJen),
    '                     New SqlParameter("@KodJenLanjut", strKodJenLanjut),
    '                     New SqlParameter("@db", decTotJum),
    '                     New SqlParameter("@Kt", 0.00),
    '                     New SqlParameter("@BulanTran", BulanTran),
    '                     New SqlParameter("@Tahun", strTahun),
    '                     New SqlParameter("@Status", Status)
    '                    }

    '            If Not dbConn.fInsertCommand(strSql, paramSql) > 0 Then
    '                blnSuccess = False
    '                Exit Try
    '            Else
    '                'AUDIT LOG
    '                sLogMedan = "mk06_tkhtran|mk06_rujukan|mk06_nodok|mk06_butiran|koddok|mk06_kodap|mk06_bil|kodkw|KodKO|kodptj|KodKP|kodvot|kodjen|kodjenlanjut|mk06_debit|mk06_kredit|mk06_bulan|mk06_tahun|mk06_status"
    '                sLogBaru = dtTkhToday & "|" & strNoBil & "|" & strNoBil & "|" & strTujuan & "|" & KodDok & "|" & KODAP & "|" & intBil & "|" & strKodKW & "|" & strKodKO & "|" & strPTJ & "|" & strKodKP & "|" & strKodVot & "|" & strKodJen & "|" & strKodJenLanjut & "|" & decTotJum & "|0.00|" & BulanTran & "|" & strTahun & "|" & Status & ""

    '                strSql = "insert into MK_AuditLog (UserID, UserGroup, UserUbah, SubMenu, Keterangan, InfoTable, RefKey, RefNo, InfoMedan, InfoBaru, " _
    '        & " InfoLama, UserIP, PCName) " _
    '        & " values (@UserID,@UserGroup,@UserUbah,@SubMenu,@Keterangan,@InfoTable,@RefKey, @RefNo,@InfoMedan,@InfoBaru,@InfoLama, " _
    '        & " @UserIP,@PCName)"

    '                Dim paramSqlLog() As SqlParameter = {
    '                        New SqlParameter("@UserID", strNoStaf),
    '                        New SqlParameter("@UserGroup", sLogLevel),
    '                        New SqlParameter("@UserUbah", "-"),
    '                         New SqlParameter("@SubMenu", Session("sesKodSubMenu")),
    '                        New SqlParameter("@Keterangan", "INSERT"),
    '                        New SqlParameter("@InfoTable", "mk06_transaksi"),
    '                        New SqlParameter("@RefKey", "-"),
    '                        New SqlParameter("@RefNo", "-"),
    '                        New SqlParameter("@InfoMedan", sLogMedan),
    '                        New SqlParameter("@InfoBaru", sLogBaru),
    '                        New SqlParameter("@InfoLama", "-"),
    '                        New SqlParameter("@UserIP", lsLogUsrIP),
    '                        New SqlParameter("@PCName", lsLogUsrPC)
    '                    }

    '                If Not dbConn.fInsertCommand(strSql, paramSqlLog) > 0 Then
    '                    blnSuccess = False
    '                    Exit Try
    '                End If
    '            End If

    '            'PADAM REKOD DALAM TABLE AR02_Lampiran
    '            strSql = "DELETE FROM AR02_Lampiran WHERE AR01_IdBil = " & intIdBil & " AND AR02_NoBilSem = '" & strNoBilSem & "'"

    '            If Not dbConn.fUpdateCommand(strSql) > 0 Then
    '                blnSuccess = False
    '                Exit Try
    '            Else
    '                'AUDIT LOG
    '                sLogMedan = "AR01_IdBil|AR02_NoBilSem"
    '                sLogBaru = intIdBil & "|" & strNoBilSem & ""

    '                strSql = "insert into MK_AuditLog (UserID, UserGroup, UserUbah, SubMenu, Keterangan, InfoTable, RefKey, RefNo, InfoMedan, InfoBaru, " _
    '    & " InfoLama, UserIP, PCName) " _
    '    & " values (@UserID,@UserGroup,@UserUbah,@SubMenu,@Keterangan,@InfoTable,@RefKey, @RefNo,@InfoMedan,@InfoBaru,@InfoLama, " _
    '    & " @UserIP,@PCName)"

    '                Dim paramSqlLog() As SqlParameter = {
    '                    New SqlParameter("@UserID", strNoStaf),
    '                    New SqlParameter("@UserGroup", sLogLevel),
    '                    New SqlParameter("@UserUbah", "-"),
    '                     New SqlParameter("@SubMenu", Session("sesKodSubMenu")),
    '                    New SqlParameter("@Keterangan", "DELETE"),
    '                    New SqlParameter("@InfoTable", "AR02_Lampiran"),
    '                    New SqlParameter("@RefKey", "-"),
    '                    New SqlParameter("@RefNo", "-"),
    '                    New SqlParameter("@InfoMedan", sLogMedan),
    '                    New SqlParameter("@InfoBaru", sLogBaru),
    '                    New SqlParameter("@InfoLama", "-"),
    '                    New SqlParameter("@UserIP", lsLogUsrIP),
    '                    New SqlParameter("@PCName", lsLogUsrPC)
    '                }

    '                If Not dbConn.fInsertCommand(strSql, paramSqlLog) > 0 Then
    '                    blnSuccess = False
    '                    Exit Try
    '                End If
    '            End If

    '            'MASUK REKOD LAMPIRAN
    '            intBil = 0
    '            For Each gvLampRow As GridViewRow In gvLamp.Rows
    '                intBil += 1
    '                Dim strIdLamp = TryCast(gvLampRow.FindControl("lblIdLamp"), Label).Text
    '                Dim intIdLamp As Integer
    '                If strIdLamp = "" Then intIdLamp = 0 Else intIdLamp = CInt(strIdLamp)
    '                Dim strNamaPen = TryCast(gvLampRow.FindControl("lblNmPen"), Label).Text
    '                Dim strNoKP = TryCast(gvLampRow.FindControl("lblNoKP"), Label).Text
    '                Dim strNoMatrik = TryCast(gvLampRow.FindControl("lblNoMatrik"), Label).Text
    '                Dim strKursus = TryCast(gvLampRow.FindControl("lblKursus"), Label).Text
    '                Dim strButiran = TryCast(gvLampRow.FindControl("txtButiran"), TextBox).Text
    '                Dim decJumPel As Decimal = CDec(TryCast(gvLampRow.FindControl("lblAmaun"), Label).Text)

    '                strSql = "INSERT INTO AR02_LAMPIRAN(AR01_IdBil, AR02_NoBilSem, AR02_NoBil, AR02_Bil, AR02_NAMAPENERIMA, AR02_NOKP, AR02_NOMATRIK, AR02_KURSUS, AR02_AMAUN)
    'VALUES(@IdBil, @NoBilSem, @NoBil, @Bil, @NamaPen, @NoKP, @NoMatrik, @Kursus, @Amaun)"
    '                paramSql2 =
    '                        {
    '                         New SqlParameter("@IdBil", intIdBil),
    '                         New SqlParameter("@NoBilSem", strNoBilSem),
    '                         New SqlParameter("@NoBil", strNoBil),
    '                         New SqlParameter("@Bil", intBil),
    '                         New SqlParameter("@NamaPen", strNamaPen),
    '                         New SqlParameter("@NoKP", strNoKP),
    '                         New SqlParameter("@NoMatrik", strNoMatrik),
    '                         New SqlParameter("@Kursus", strKursus),
    '                         New SqlParameter("@Amaun", decJumPel)
    '                        }

    '                If Not dbConn.fInsertCommand(strSql, paramSql2) > 0 Then
    '                    blnSuccess = False
    '                    Exit Try
    '                Else
    '                    'AUDIT LOG
    '                    sLogMedan = "AR01_IdBil| AR02_NoBilSem| AR02_NoBil| AR02_Bil| AR02_NAMAPENERIMA| AR02_NOKP| AR02_NOMATRIK| AR02_KURSUS| AR02_AMAUN"
    '                    sLogBaru = intIdBil & "|" & strNoBilSem & "|" & strNoBil & "|" & intBil & "|" & strNamaPen & "|" & strNoKP & "|" & strNoMatrik & "|" & strKursus & "|" & decJumPel & ""

    '                    strSql = "insert into MK_AuditLog (UserID, UserGroup, UserUbah, SubMenu, Keterangan, InfoTable, RefKey, RefNo, InfoMedan, InfoBaru, " _
    '        & " InfoLama, UserIP, PCName) " _
    '        & " values (@UserID,@UserGroup,@UserUbah,@SubMenu,@Keterangan,@InfoTable,@RefKey, @RefNo,@InfoMedan,@InfoBaru,@InfoLama, " _
    '        & " @UserIP,@PCName)"

    '                    Dim paramSqlLog() As SqlParameter = {
    '                        New SqlParameter("@UserID", strNoStaf),
    '                        New SqlParameter("@UserGroup", sLogLevel),
    '                        New SqlParameter("@UserUbah", "-"),
    '                         New SqlParameter("@SubMenu", Session("sesKodSubMenu")),
    '                        New SqlParameter("@Keterangan", "INSERT"),
    '                        New SqlParameter("@InfoTable", "AR02_LAMPIRAN"),
    '                        New SqlParameter("@RefKey", "-"),
    '                        New SqlParameter("@RefNo", "-"),
    '                        New SqlParameter("@InfoMedan", sLogMedan),
    '                        New SqlParameter("@InfoBaru", sLogBaru),
    '                        New SqlParameter("@InfoLama", "-"),
    '                        New SqlParameter("@UserIP", lsLogUsrIP),
    '                        New SqlParameter("@PCName", lsLogUsrPC)
    '                    }

    '                    If Not dbConn.fInsertCommand(strSql, paramSqlLog) > 0 Then
    '                        blnSuccess = False
    '                        Exit Try
    '                    End If
    '                End If
    '            Next

    '            'Insert Into AR06_STATUSDOK
    '            strSql = "INSERT INTO AR06_STATUSDOK(AR06_NOBILSEM,AR06_NoBil, AR06_StatusDok,AR06_Tarikh,AR06_Ulasan, AR06_NoStaf )" &
    '                             "VALUES (@NOBILSEM,@NOBIL, @STATUSDOK,@TARIKH,@ULASAN, @NoStaf)"

    '            Dim paramSql4() As SqlParameter =
    '                {
    '                New SqlParameter("@NOBILSEM", strNoBilSem),
    '                 New SqlParameter("@NOBIL", strNoBil),
    '                 New SqlParameter("@STATUSDOK", strStatDok),
    '                 New SqlParameter("@TARIKH", dtTkhToday),
    '                 New SqlParameter("@ULASAN", " - "),
    '                 New SqlParameter("@NoStaf", strNoStaf)
    '                }

    '            If Not dbConn.fInsertCommand(strSql, paramSql4) > 0 Then
    '                blnSuccess = False
    '                Exit Try
    '            End If

    '        Catch ex As Exception
    '            blnSuccess = False
    '        End Try

    '        If blnSuccess = True Then
    '            dbConn.sConnCommitTrans()
    '            txtNoInv.Text = strNoBil
    '            Return True
    '        ElseIf blnSuccess = False Then
    '            dbConn.sConnRollbackTrans()
    '            Return False
    '        End If
    '    End Function

    Private Function fLulus() As Boolean
        Dim strSql As String
        Dim dbConn As New DBKewConn
        Dim blnSuccess As Boolean = True
        Dim strStatDok As String = "03"
        Dim strNoBil As String
        Dim strNoBilSem As String = Trim(lblNoInvSem.Text.TrimEnd)
        Dim decTotJum As Decimal
        Dim intIdBil As Integer = CInt(hidIdBil.Value)
        Dim strTahun As String = Now.Year.ToString
        Dim strKodJen, strKodJenLanjut As String
        Dim strKodKW2, strKodKO2, strKodKP2 As String
        Dim strTkhToday As String = Now.ToString("yyyy-MM-dd")
        Dim dtTkhToday As DateTime = CDate(strTkhToday)
        Dim paramSql() As SqlParameter

        Try
            Dim strKodBank As String = Trim(ddlBank.SelectedValue.TrimEnd)
            Dim strTkhLulus As String = Now.ToString("yyyy-MM-dd")
            Dim strNoStaf As String = Session("ssusrID")

            Dim strTujuan = Trim(lblTujuan.Text.TrimEnd)

            If String.IsNullOrEmpty(strNoStaf) OrElse strNoStaf Is Nothing Then
                blnSuccess = False
                Exit Try
            End If

            dbConn.sConnBeginTrans()

            For Each gvRow As GridViewRow In gvTrans.Rows
                Dim decJumTrans As Decimal = CDec(CType(gvRow.FindControl("lblJumlah"), Label).Text)
                decTotJum += decJumTrans
            Next

            'JANA NO. BIL
            Dim strKodModul
            Dim strPrefix
            Dim strButiranNoAkhir As String


            Dim strKodKatPel As String = Trim(lblKodKatPel.Text.TrimEnd)

            If strKodKatPel = "UG" Then
                strKodModul = "AR"
                strPrefix = "U"
                strButiranNoAkhir = "Max No Pelajar Undergrade AR"

            ElseIf strKodKatPel = "PG" Then
                strKodModul = "AR"
                strPrefix = "P"
                strButiranNoAkhir = "Max No Pelajar Postgrade AR"

            End If

            strNoBil = fGetNo(strTahun, strKodModul, strPrefix, strButiranNoAkhir)

            If strNoBil Is Nothing Then
                blnSuccess = False
                Exit Try
            End If

            'UPDATE AR01_Bil
            strSql = "update AR01_Bil set AR01_NOBIL = @NOBIL, AR01_KODBANK = @KodBank,AR01_StatusDok = @AR01_StatusDok, AR01_TkhLulus = @TkhLulus, AR01_JUMLAH = @Jumlah, AR01_JUMBLMBYR= @JumBlmByr  where AR01_NoBilSem =@NoInvSem"

            Dim paramSql2() As SqlParameter = {
                New SqlParameter("@NOBIL", strNoBil),
                New SqlParameter("@KodBank", strKodBank),
                    New SqlParameter("@AR01_StatusDok", strStatDok),
                    New SqlParameter("@TkhLulus", strTkhLulus),
                    New SqlParameter("@NoInvSem", strNoBilSem),
                    New SqlParameter("@Jumlah", decTotJum),
                    New SqlParameter("@JumBlmByr", decTotJum)
                }

            If Not dbConn.fUpdateCommand(strSql, paramSql2) > 0 Then
                blnSuccess = False
                Exit Try
            Else
                'AUDIT LOG
                sLogMedan = "AR01_NOBIL|AR01_KODBANK|AR01_StatusDok|AR01_TkhLulus|AR01_JUMLAH|AR01_JUMBLMBYR|AR01_NoBilSem"
                sLogBaru = strNoBil & "|" & strKodBank & "|" & strStatDok & "|" & strTkhLulus & "|" & strNoBilSem & "|" & decTotJum & "|" & decTotJum & ""

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

                If Not dbConn.fInsertCommand(strSql, paramSqlLog) > 0 Then
                    blnSuccess = False
                    Exit Try
                End If
            End If

            Dim KodDok As String = "BIL"
            Dim KODAP As String = "-"
            Dim Status As String = "0"

            Dim BulanTran As String = Now.Month.ToString
            Dim intBil As Integer
            Dim strKodKW, strKodKO, strPTJ, strKodKP, strKodVot, strPerkara As String
            Dim decKdrHrg, decAmt As Decimal
            Dim intKuantiti As Integer

            For Each gvTransInvrow As GridViewRow In gvTrans.Rows
                intBil = intBil + 1
                Dim strIdDt As String = TryCast(gvTransInvrow.FindControl("lblId"), Label).Text
                Dim intDtId As Integer
                If strIdDt = "" Then intDtId = 0 Else intDtId = CInt(strIdDt)

                strKodKW = TryCast(gvTransInvrow.FindControl("lblKW"), Label).Text
                strKodKO = TryCast(gvTransInvrow.FindControl("lblKO"), Label).Text
                strPTJ = TryCast(gvTransInvrow.FindControl("lblPTj"), Label).Text
                strKodKP = TryCast(gvTransInvrow.FindControl("lblKP"), Label).Text
                strKodVot = TryCast(gvTransInvrow.FindControl("lblVot"), Label).Text
                strPerkara = TryCast(gvTransInvrow.FindControl("lblPerkara"), Label).Text
                intKuantiti = CInt(TryCast(gvTransInvrow.FindControl("lblKuantiti"), Label).Text)
                decKdrHrg = CDec(TryCast(gvTransInvrow.FindControl("lblHarga"), Label).Text)
                decAmt = CDec(TryCast(gvTransInvrow.FindControl("lblJumlah"), Label).Text)

                fGetKodJen(strKodVot, strKodJen, strKodJenLanjut)

                strSql = "INSERT INTO mk06_transaksi(mk06_tkhtran,mk06_rujukan,mk06_nodok,mk06_butiran,koddok,mk06_kodap,mk06_bil,kodkw,KodKO,kodptj,KodKP,kodvot,kodjen,kodjenlanjut,mk06_debit,mk06_kredit,mk06_bulan,mk06_tahun,mk06_status)
                            VALUES (@dtTkhToday,@NOBILSEM,@NOBIL,@TUJUAN,@KodDok,@KODAP,@BIL,@kodKw,@KodKO,@KodPTJ,@KodKP,@KodVot,@kodJen,@KodJenLanjut,@db,@Kt,@BulanTran,@Tahun,@Status)"

                paramSql =
                    {
                     New SqlParameter("@dtTkhToday", dtTkhToday),
                     New SqlParameter("@NOBILSEM", strNoBil),
                     New SqlParameter("@NOBIL", strNoBil),
                     New SqlParameter("@TUJUAN", strPerkara),
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
                     New SqlParameter("@Kt", decAmt),
                     New SqlParameter("@BulanTran", BulanTran),
                     New SqlParameter("@Tahun", strTahun),
                     New SqlParameter("@Status", Status)
                    }

                If Not dbConn.fInsertCommand(strSql, paramSql) > 0 Then
                    blnSuccess = False
                    Exit Try
                Else
                    'AUDIT LOG
                    sLogMedan = "mk06_tkhtran|mk06_rujukan|mk06_nodok|mk06_butiran|koddok|mk06_kodap|mk06_bil|kodkw|KodKO|kodptj|KodKP|kodvot|kodjen|kodjenlanjut|mk06_debit|mk06_kredit|mk06_bulan|mk06_tahun|mk06_status"
                    sLogBaru = dtTkhToday & "|" & strNoBilSem & "|" & strNoBil & "|" & strTujuan & "|" & KodDok & "|" & intBil & "|" & strKodKW & "|" & strKodKO & "|" & strPTJ & "|" & strKodKP & "|" & strKodVot & "|" & strKodJen & "|" & strKodJenLanjut & "|0.00|" & decAmt & "|" & BulanTran & "|" & strTahun & "|" & Status & ""

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

                    If Not dbConn.fInsertCommand(strSql, paramSqlLog) > 0 Then
                        blnSuccess = False
                        Exit Try
                    End If
                End If

                'end POST KE MK06_TRANSAKSI=======================================================

            Next

            'POST KE  
            ' Dim strKodJen, strKodJenLanjut As String
            intBil = intBil + 1
            strKodKW2 = "07"
            strPTJ = "50000"
            strKodVot = "71901"
            strKodKO2 = "00"
            strKodKP2 = "0000000"
            fGetKodJen(strKodVot, strKodJen, strKodJenLanjut)

            strSql = "INSERT INTO mk06_transaksi(mk06_tkhtran,mk06_rujukan,mk06_nodok,mk06_butiran,koddok,mk06_kodap,mk06_bil,kodkw,KodKO,kodptj,KodKP,kodvot,kodjen,kodjenlanjut,mk06_debit,mk06_kredit,mk06_bulan,mk06_tahun,mk06_status)
                            VALUES (@dtTkhToday,@Rujukan,@NoDok,@mk06_butiran,@KodDok,@KODAP,@BIL,@kodKw,@KodKO,@KodPTJ,@KodKP,@KodVot,@kodJen,@KodJenLanjut,@db,@Kt,@BulanTran,@Tahun,@Status)"

            paramSql =
                    {
                     New SqlParameter("@dtTkhToday", dtTkhToday),
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

            If Not dbConn.fInsertCommand(strSql, paramSql) > 0 Then
                blnSuccess = False
                Exit Try
            Else
                'AUDIT LOG
                sLogMedan = "mk06_tkhtran|mk06_rujukan|mk06_nodok|mk06_butiran|koddok|mk06_kodap|mk06_bil|kodkw|KodKO|kodptj|KodKP|kodvot|kodjen|kodjenlanjut|mk06_debit|mk06_kredit|mk06_bulan|mk06_tahun|mk06_status"
                sLogBaru = dtTkhToday & "|" & strNoBil & "|" & strNoBil & "|" & strTujuan & "|" & KodDok & "|" & KODAP & "|" & intBil & "|" & strKodKW & "|" & strKodKO & "|" & strPTJ & "|" & strKodKP & "|" & strKodVot & "|" & strKodJen & "|" & strKodJenLanjut & "|" & decTotJum & "|0.00|" & BulanTran & "|" & strTahun & "|" & Status & ""

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

                If Not dbConn.fInsertCommand(strSql, paramSqlLog) > 0 Then
                    blnSuccess = False
                    Exit Try
                End If
            End If

            'Insert Into AR06_STATUSDOK
            strSql = "INSERT INTO AR06_STATUSDOK(AR06_NOBILSEM,AR06_NoBil, AR06_StatusDok,AR06_Tarikh,AR06_Ulasan, AR06_NoStaf )" &
                             "VALUES (@NOBILSEM,@NOBIL, @STATUSDOK,@TARIKH,@ULASAN, @NoStaf)"

            Dim paramSql4() As SqlParameter =
                {
                New SqlParameter("@NOBILSEM", strNoBilSem),
                 New SqlParameter("@NOBIL", strNoBil),
                 New SqlParameter("@STATUSDOK", strStatDok),
                 New SqlParameter("@TARIKH", dtTkhToday),
                 New SqlParameter("@ULASAN", " - "),
                 New SqlParameter("@NoStaf", strNoStaf)
                }

            If Not dbConn.fInsertCommand(strSql, paramSql4) > 0 Then
                blnSuccess = False
                Exit Try
            End If

        Catch ex As Exception
            blnSuccess = False
        End Try

        If blnSuccess = True Then
            dbConn.sConnCommitTrans()
            txtNoInv.Text = strNoBil
            Return True
        ElseIf blnSuccess = False Then
            dbConn.sConnRollbackTrans()
            Return False
        End If
    End Function

    Private Function fXLulus() As Boolean
        Dim strSql As String
        Dim dbConn As New DBKewConn
        Dim blnSuccess As Boolean = True
        Dim strTkhToday As String = Now.ToString("yyyy-MM-dd")
        Dim dtTkhToday As DateTime = CDate(strTkhToday)
        Dim strStatDok As String = "05"
        Try
            'Dim strNoBil As String = fGetID_Bil()

            Dim strNoBilSem As String = lblNoInvSem.Text

            strSql = "update AR01_Bil set AR01_StatusDok = @AR01_StatusDok where AR01_NoBilSem =@NoInvCuk"

            Dim paramSql() As SqlParameter = {
                    New SqlParameter("@AR01_StatusDok", strStatDok),
                    New SqlParameter("@NoInvCuk", strNoBilSem)
                }
            dbConn.sConnBeginTrans()
            If Not dbConn.fInsertCommand(strSql, paramSql) > 0 Then
                blnSuccess = False
                Exit Try
            Else
                'AUDIT LOG
                sLogMedan = "AR01_StatusDok| AR01_NoBilSem"
                sLogBaru = strStatDok & "|" & strNoBilSem & ""

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

                If Not dbConn.fInsertCommand(strSql, paramSqlLog) > 0 Then
                    blnSuccess = False
                    Exit Try
                End If
            End If

            'Insert Into AR06_STATUSDOK
            strSql = "INSERT INTO AR06_STATUSDOK(AR06_NOBILSEM,AR06_NoBil, AR06_StatusDok,AR06_Tarikh,AR06_Ulasan )" &
                             "VALUES (@NOBILSEM,@NOBIL, @STATUSDOK,@TARIKH,@ULASAN)"

            Dim paramSql3() As SqlParameter =
                {
                New SqlParameter("@NOBILSEM", strNoBilSem),
                 New SqlParameter("@NOBIL", strNoBilSem),
                 New SqlParameter("@STATUSDOK", strStatDok),
                 New SqlParameter("@TARIKH", dtTkhToday),
                 New SqlParameter("@ULASAN", " - ")
                }

            If Not dbConn.fInsertCommand(strSql, paramSql3) > 0 Then
                blnSuccess = False
                Exit Try
            End If

        Catch ex As Exception
            blnSuccess = False
        End Try

        If blnSuccess = True Then
            dbConn.sConnCommitTrans()
            fGlobalAlert("Maklumat telah disimpan!", Me.Page, Me.[GetType]())
            sLoadLst()
            divList.Visible = True
            divWiz.Visible = False
        ElseIf blnSuccess = False Then
            dbConn.sConnRollbackTrans()
            fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
        End If
    End Function



    Private Sub lbtnKembali_Click(sender As Object, e As EventArgs) Handles lbtnKembali.Click
        sLoadLst()
        divList.Visible = True
        divWiz.Visible = False
    End Sub

    Private Sub ddlSaizRekod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSaizRekod.SelectedIndexChanged
        gvLst.PageSize = CInt(ddlSaizRekod.Text)
        'BindGvViewButiran()
        sLoadLst()
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

    Protected Sub lbtnLulus_Click(sender As Object, e As EventArgs) Handles lbtnLulus.Click

        Dim decJumtrans, decJumLamp As Decimal
        Dim decTotJumTrans, decTotJumLamp As Decimal
        For Each gvRow As GridViewRow In gvTrans.Rows
            Dim strJumlah = CType(gvRow.FindControl("lblJumlah"), Label).Text
            If strJumlah = "" Then decJumtrans = 0 Else decJumtrans = CDec(strJumlah)
            decTotJumTrans += decJumtrans
        Next

        For Each gvRow As GridViewRow In gvLamp.Rows
            Dim strJumlah = CType(gvRow.FindControl("lblAmaun"), Label).Text
            If strJumlah = "" Then decJumLamp = 0 Else decJumLamp = CDec(strJumlah)
            decTotJumLamp += decJumLamp
        Next

        If decTotJumTrans <> decTotJumLamp Then
            fGlobalAlert("Jumlah Transaksi tidak sama dengan jumlah Lampiran!", Me.Page, Me.[GetType]())
            Exit Sub
        End If


        If fLulus() Then
            fGlobalAlert("Maklumat invois telah diluluskan!", Me.Page, Me.[GetType]())
            lbtnLulus.Visible = False
            lbtnXLulus.Visible = False
        Else
            fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
        End If
    End Sub

    Private Sub lbtnXLulus_Click(sender As Object, e As EventArgs) Handles lbtnXLulus.Click
        fXLulus()
    End Sub

    Protected Sub lbtnCari_Click(sender As Object, e As EventArgs) Handles lbtnCari.Click
        sLoadLst()
    End Sub

    Private Sub ddlCarian_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCarian.SelectedIndexChanged
        If ddlCarian.SelectedIndex = 0 Then
            txtCarian.Enabled = False
        Else
            txtCarian.Enabled = True
        End If
        txtCarian.Text = ""
    End Sub

    Private Sub ddlFilStat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFilStat.SelectedIndexChanged
        sLoadLst()
    End Sub

    Private Sub gvLst_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvLst.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim lbtnSelect As LinkButton = DirectCast(e.Row.FindControl("lbtnSelect"), LinkButton)
                Dim btnDelete As LinkButton = DirectCast(e.Row.FindControl("btnDelete"), LinkButton)
                divList.Visible = True
                divWiz.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvLst_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvLst.SelectedIndexChanged
        Try
            Dim gvRow As GridViewRow = gvLst.SelectedRow

            Dim intIdBil As Integer = CInt(CType(gvLst.SelectedRow.FindControl("lblIdBil"), Label).Text.TrimEnd)
            Dim strNoInvSem As String = CType(gvRow.FindControl("lblNoInvSem"), Label).Text.TrimEnd
            Dim strStatDok As String = CType(gvRow.FindControl("lblStatDok"), Label).Text.TrimEnd
            hidIdBil.Value = CType(gvLst.SelectedRow.FindControl("lblIdBil"), Label).Text.TrimEnd

            sLoadInv(strNoInvSem)
            sLoadTrans(strNoInvSem)
            sLoadMohon(strNoInvSem)
            'sLoadSokong(strNoInvSem)
            sLoadInvLamp(intIdBil)

            If strStatDok = "01" Then
                lbtnLulus.Visible = True
                lbtnXLulus.Visible = True
                'sSetLulus()
            Else
                lbtnLulus.Visible = False
                lbtnXLulus.Visible = False
                'sLoadLulus(strNoInvSem, strStatDok)
            End If

            divList.Visible = False
            divWiz.Visible = True

        Catch ex As Exception

        End Try
    End Sub

    Private Sub sLoadInvLamp(ByVal intIdBil As Integer)
        Try
            Dim strSql = "select AR02_IdLamp, AR02_NamaPenerima, AR02_NoKP, AR02_NoMatrik, AR02_Kursus,AR02_Butiran, AR02_Amaun from AR02_Lampiran where AR01_IdBil = " & intIdBil

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            Using dt = ds.Tables(0)
                ViewState("dtLamp") = dt
                gvLamp.DataSource = dt
                gvLamp.DataBind()
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

    'Private Sub sLoadLulus(ByVal strNoInvSem As String, ByVal strStatDok As String)
    '    Try
    '        Dim strSql As String = "Select AR06_NoStaf, AR06_Tarikh from AR06_StatusDok where AR06_NoBilSem = '" & strNoInvSem & "' and AR06_StatusDok = '" & strStatDok & "' "
    '        Dim dbconn As New DBKewConn
    '        Using ds = dbconn.fSelectCommand(strSql)
    '            Dim strNoStafLulus As String = ds.Tables(0).Rows(0)("AR06_NoStaf").ToString
    '            Dim dtTkhLulus As Date = CDate(ds.Tables(0).Rows(0)("AR06_Tarikh").ToString)
    '            Dim strTkhLulus As String = dtTkhLulus.ToString("dd/MM/yyyy")

    '            lblNoStafPel.Text = strNoStafLulus
    '            lblTkhLulus.Text = strTkhLulus

    '            If Not String.IsNullOrEmpty(strNoStafLulus) Then
    '                Using dt = fGetUserInfo(strNoStafLulus)
    '                    lblJawPel.Text = dt.Rows(0)("JawGiliran").ToString
    '                    lblNmStafPel.Text = dt.Rows(0)("MS01_Nama").ToString
    '                    lblKodPTjPel.Text = dt.Rows(0)("KodPejabat").ToString & "0000"
    '                    lblNmPTjPel.Text = dt.Rows(0)("Pejabat").ToString
    '                End Using
    '            End If
    '        End Using

    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub gvLst_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvLst.Sorting
        Try
            Dim sortingDirection As String = String.Empty
            If direction = SortDirection.Ascending Then
                direction = SortDirection.Descending
                sortingDirection = "Desc"
            Else
                direction = SortDirection.Ascending
                sortingDirection = "Asc"
            End If

            Dim dt As New DataTable
            dt = CType(ViewState("dtLst"), DataTable)

            Dim sortedView As New DataView(dt)
            sortedView.Sort = Convert.ToString(e.SortExpression + " ") & sortingDirection
            Session("SortedView") = sortedView
            gvLst.DataSource = sortedView
            gvLst.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvLst_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvLst.PageIndexChanging
        Try
            gvLst.PageIndex = e.NewPageIndex
            If ViewState("dtLst") IsNot Nothing Then
                gvLst.DataSource = ViewState("dtlst")
                gvLst.DataBind()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub sLoadTrans(strNoInvSem)
        Try
            sClearGvTrans()
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

    Dim decJumDt As Decimal
    Private Sub gvTrans_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvTrans.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim strJumlah As String = CType(e.Row.FindControl("lblJumlah"), Label).Text
                If strJumlah = "" Then strJumlah = "0.00"
                decJumDt += CDec(strJumlah)

                'Dim ddlKW = DirectCast(e.Row.FindControl("ddlKW"), DropDownList)
                'ddlKW.DataSource = ViewState("dsKW")
                'ddlKW.DataTextField = "ButiranKW"
                'ddlKW.DataValueField = "KodKw"
                'ddlKW.DataBind()
                'ddlKW.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
                'ddlKW.SelectedValue = TryCast(e.Row.FindControl("hidKW"), HiddenField).Value

                'Dim ddlKO = DirectCast(e.Row.FindControl("ddlKO"), DropDownList)
                'Dim strSelKO As String = TryCast(e.Row.FindControl("hidKO"), HiddenField).Value

                'If strSelKO = "0" OrElse strSelKO = "" Then
                '    ddlKO.Items.Insert(0, New ListItem("- SILA PILIH KW -", 0))
                '    ddlKO.SelectedValue = strSelKO
                'Else
                '    ddlKO.DataSource = ViewState("dsKO")
                '    ddlKO.DataTextField = "Butiran"
                '    ddlKO.DataValueField = "KodKO"
                '    ddlKO.DataBind()
                '    ddlKO.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
                '    ddlKO.SelectedValue = strSelKO
                'End If

                'Dim ddlPTj = DirectCast(e.Row.FindControl("ddlPTj"), DropDownList)
                'Dim strSelPTj As String = TryCast(e.Row.FindControl("hidPTj"), HiddenField).Value

                'If strSelPTj = "0" OrElse strSelPTj = "" Then
                '    ddlPTj.Items.Insert(0, New ListItem("- SILA PILIH KO -", 0))
                '    ddlPTj.SelectedValue = strSelPTj
                'Else
                '    ddlPTj.DataSource = ViewState("dsPTj")
                '    ddlPTj.DataTextField = "Butiran"
                '    ddlPTj.DataValueField = "KodPTj"
                '    ddlPTj.DataBind()
                '    ddlPTj.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
                '    ddlPTj.SelectedValue = strSelPTj
                'End If

                'Dim ddlKP = DirectCast(e.Row.FindControl("ddlKP"), DropDownList)
                'Dim strSelKP As String = TryCast(e.Row.FindControl("hidKP"), HiddenField).Value

                'If strSelKP = "0" OrElse strSelKP = "" Then
                '    ddlKP.Items.Insert(0, New ListItem("- SILA PILIH PTj -", 0))
                '    ddlKP.SelectedValue = strSelKP
                'Else
                '    ddlKP.DataSource = ViewState("dsKP")
                '    ddlKP.DataTextField = "Butiran"
                '    ddlKP.DataValueField = "KodKP"
                '    ddlKP.DataBind()

                '    ddlKP.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
                '    ddlKP.SelectedValue = strSelKP
                'End If

                'Dim ddlVot = DirectCast(e.Row.FindControl("ddlVot"), DropDownList)
                'Dim strSelVot As String = TryCast(e.Row.FindControl("hidVot"), HiddenField).Value


                'If strSelVot = "0" OrElse strSelVot = "" Then
                '    ddlVot.Items.Insert(0, New ListItem("- SILA PILIH KP -", 0))
                '    ddlVot.SelectedValue = strSelVot
                'Else
                '    ddlVot.DataSource = ViewState("dsVot")
                '    ddlVot.DataTextField = "Butiran"
                '    ddlVot.DataValueField = "KodVot"
                '    ddlVot.DataBind()

                '    ddlVot.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
                '    ddlVot.SelectedValue = strSelVot
                'End If

            End If

            If e.Row.RowType = DataControlRowType.Footer Then
                Dim lblTotJum As Label = CType(e.Row.FindControl("lblTotJum"), Label)
                lblTotJum.Text = FormatNumber(decJumDt, 2)
            End If

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

    'Private Sub AddNewRowToGrid()
    '    Try
    '        Dim rowIndex As Integer = 0
    '        If ViewState("dtInvDt") IsNot Nothing Then
    '            Dim dtvsDtInv As DataTable = CType(ViewState("dtInvDt"), DataTable)
    '            Dim drCurrentRow As DataRow = Nothing

    '            If dtvsDtInv.Rows.Count > 0 Then

    '                For i As Integer = 1 To dtvsDtInv.Rows.Count

    '                    Dim strKW = CType(gvTrans.Rows(rowIndex).Cells(1).FindControl("ddlKW"), DropDownList).Text
    '                    Dim strKO = CType(gvTrans.Rows(rowIndex).Cells(2).FindControl("ddlKO"), DropDownList).Text
    '                    Dim strPTj = CType(gvTrans.Rows(rowIndex).Cells(3).FindControl("ddlPTj"), DropDownList).Text
    '                    Dim strKP = CType(gvTrans.Rows(rowIndex).Cells(4).FindControl("ddlKP"), DropDownList).Text
    '                    Dim strVot = CType(gvTrans.Rows(rowIndex).Cells(5).FindControl("ddlVot"), DropDownList).Text
    '                    Dim strPerkara = CType(gvTrans.Rows(rowIndex).Cells(6).FindControl("txtPerkara"), TextBox).Text
    '                    Dim strKuantiti = CType(gvTrans.Rows(rowIndex).Cells(7).FindControl("txtKuantiti"), TextBox).Text
    '                    Dim strHarga = CType(gvTrans.Rows(rowIndex).Cells(8).FindControl("txtHarga"), TextBox).Text
    '                    Dim strJumlah = CType(gvTrans.Rows(rowIndex).Cells(9).FindControl("lblJumlah"), Label).Text

    '                    dtvsDtInv.Rows(i - 1)("KodKw") = strKW
    '                    dtvsDtInv.Rows(i - 1)("KodKO") = strKO
    '                    dtvsDtInv.Rows(i - 1)("KodPTJ") = strPTj
    '                    dtvsDtInv.Rows(i - 1)("KodKP") = strKP
    '                    dtvsDtInv.Rows(i - 1)("KodVot") = strVot
    '                    dtvsDtInv.Rows(i - 1)("AR01_Perkara") = strPerkara
    '                    dtvsDtInv.Rows(i - 1)("AR01_Kuantiti") = IIf(strKuantiti = "", 0, strKuantiti)
    '                    dtvsDtInv.Rows(i - 1)("AR01_kadarHarga") = IIf(strHarga = "", 0.00, strHarga)
    '                    dtvsDtInv.Rows(i - 1)("AR01_Jumlah") = IIf(strJumlah = "", 0.00, strJumlah)

    '                    rowIndex += 1
    '                Next

    '                drCurrentRow = dtvsDtInv.NewRow()
    '                dtvsDtInv.Rows.Add(drCurrentRow)
    '                ViewState("dtInvDt") = dtvsDtInv
    '                gvTrans.DataSource = dtvsDtInv
    '                gvTrans.DataBind()
    '                SetPreviousData(dtvsDtInv)
    '            End If

    '        End If


    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Private Sub SetPreviousData(ByVal dt As DataTable)
    '    Try
    '        Dim rowIndex As Integer = 0

    '        If dt.Rows.Count > 0 Then
    '            For i As Integer = 0 To dt.Rows.Count - 1

    '                Dim gvRow As GridViewRow = gvTrans.Rows(i)
    '                Dim ddl1 As DropDownList = CType(gvRow.FindControl("ddlKW"), DropDownList)
    '                Dim ddl2 As DropDownList = CType(gvRow.FindControl("ddlKO"), DropDownList)
    '                Dim ddl3 As DropDownList = CType(gvRow.FindControl("ddlPTj"), DropDownList)
    '                Dim ddl4 As DropDownList = CType(gvRow.FindControl("ddlKP"), DropDownList)
    '                Dim ddl5 As DropDownList = CType(gvRow.FindControl("ddlVot"), DropDownList)
    '                Dim box1 As TextBox = CType(gvRow.FindControl("txtPerkara"), TextBox)
    '                Dim box2 As TextBox = CType(gvRow.FindControl("txtKuantiti"), TextBox)
    '                Dim box3 As TextBox = CType(gvRow.FindControl("txtHarga"), TextBox)
    '                Dim box4 As Label = CType(gvRow.FindControl("lblJumlah"), Label)

    '                Dim strKtt As String = dt.Rows(i)("AR01_Kuantiti").ToString()
    '                Dim strHarga As String = dt.Rows(i)("AR01_kadarHarga").ToString()
    '                Dim strJumlah As String = dt.Rows(i)("AR01_Jumlah").ToString()

    '                ddl1.SelectedValue = dt.Rows(i)("KodKw").ToString()
    '                ddl2.SelectedValue = dt.Rows(i)("KodKO").ToString()
    '                ddl3.SelectedValue = dt.Rows(i)("KodPTJ").ToString()
    '                ddl4.SelectedValue = dt.Rows(i)("KodKP").ToString()
    '                ddl5.SelectedValue = dt.Rows(i)("KodVot").ToString()
    '                box1.Text = dt.Rows(i)("AR01_Perkara").ToString()
    '                box2.Text = IIf(strKtt = "", 0, strKtt)
    '                box3.Text = IIf(strHarga = "", "0.00", strHarga)
    '                box4.Text = IIf(strJumlah = "", "0.00", strJumlah)

    '                rowIndex += 1
    '            Next
    '        End If

    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Protected Sub lbtnTambah_Click(sender As Object, e As EventArgs)

    '    Try
    '        Dim strKW As String = CType(gvTrans.Rows(gvTrans.Rows.Count - 1).FindControl("ddlKW"), DropDownList).Text
    '        Dim strKO As String = CType(gvTrans.Rows(gvTrans.Rows.Count - 1).FindControl("ddlKO"), DropDownList).Text
    '        Dim strPTj As String = CType(gvTrans.Rows(gvTrans.Rows.Count - 1).FindControl("ddlPTj"), DropDownList).Text
    '        Dim strKP As String = CType(gvTrans.Rows(gvTrans.Rows.Count - 1).FindControl("ddlKP"), DropDownList).Text
    '        Dim strVot As String = CType(gvTrans.Rows(gvTrans.Rows.Count - 1).FindControl("ddlVot"), DropDownList).Text
    '        Dim strPerkara As String = CType(gvTrans.Rows(gvTrans.Rows.Count - 1).FindControl("txtPerkara"), TextBox).Text
    '        Dim intKtt As Integer = CInt(CType(gvTrans.Rows(gvTrans.Rows.Count - 1).FindControl("txtKuantiti"), TextBox).Text)
    '        Dim decHarga As Decimal = CDec(CType(gvTrans.Rows(gvTrans.Rows.Count - 1).FindControl("txtHarga"), TextBox).Text)
    '        Dim decJumlah As Decimal = CDec(CType(gvTrans.Rows(gvTrans.Rows.Count - 1).FindControl("lblJumlah"), Label).Text)

    '        If strKW <> "0" AndAlso strKO <> "0" AndAlso strPTj <> "0" AndAlso strKP <> "0" AndAlso strVot <> "0" AndAlso strPerkara <> "" AndAlso intKtt <> 0 AndAlso decHarga <> 0.00 AndAlso decJumlah <> 0.00 Then
    '            AddNewRowToGrid()
    '        End If

    '    Catch ex As Exception

    '    End Try
    'End Sub

    '    Protected Sub ddlKW_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Try

    '            Dim gvr As GridViewRow = CType(((CType(sender, Control)).NamingContainer), GridViewRow)
    '            Dim ddlKW As DropDownList = CType(gvr.FindControl("ddlKW"), DropDownList)
    '            Dim strKodKW As String = ddlKW.SelectedItem.Value

    '            Dim ddlKO = DirectCast(gvr.FindControl("ddlKO"), DropDownList)
    '            ddlKO.DataSource = fLoadKO(strKodKW)
    '            ddlKO.DataTextField = "ButiranKO"
    '            ddlKO.DataValueField = "KodKO"
    '            ddlKO.DataBind()

    '            ddlKO.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
    '            ddlKO.SelectedIndex = 0
    '        Catch ex As Exception

    '        End Try
    '    End Sub
    '    Protected Sub ddlKO_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Try

    '            Dim gvr As GridViewRow = CType(((CType(sender, Control)).NamingContainer), GridViewRow)
    '            Dim ddlKW As DropDownList = CType(gvr.FindControl("ddlKW"), DropDownList)
    '            Dim strKodKW As String = ddlKW.SelectedItem.Value

    '            Dim ddlKO As DropDownList = CType(gvr.FindControl("ddlKO"), DropDownList)
    '            Dim strKodKO As String = ddlKO.SelectedItem.Value

    '            Dim ddlPTj = DirectCast(gvr.FindControl("ddlPTj"), DropDownList)
    '            ddlPTj.DataSource = fLoadPTj(strKodKW, strKodKO)   'ViewState("dsKO")
    '            ddlPTj.DataTextField = "ButiranPTj"
    '            ddlPTj.DataValueField = "KodPTj"
    '            ddlPTj.DataBind()

    '            ddlPTj.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
    '            ddlPTj.SelectedIndex = 0
    '        Catch ex As Exception

    '        End Try
    '    End Sub
    '    Protected Sub ddlPTj_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Try

    '            Dim gvr As GridViewRow = CType(((CType(sender, Control)).NamingContainer), GridViewRow)
    '            Dim ddlKW As DropDownList = CType(gvr.FindControl("ddlKW"), DropDownList)
    '            Dim strKodKW As String = ddlKW.SelectedItem.Value

    '            Dim ddlKO As DropDownList = CType(gvr.FindControl("ddlKO"), DropDownList)
    '            Dim strKodKO As String = ddlKO.SelectedItem.Value

    '            Dim ddlPTj As DropDownList = CType(gvr.FindControl("ddlPTj"), DropDownList)
    '            Dim strKodPTj As String = ddlPTj.SelectedItem.Value

    '            Dim ddlKP = DirectCast(gvr.FindControl("ddlKP"), DropDownList)
    '            ddlKP.DataSource = fLoadKP(strKodKW, strKodKO, strKodPTj)
    '            ddlKP.DataTextField = "Butiran"
    '            ddlKP.DataValueField = "KodProjek"
    '            ddlKP.DataBind()

    '            ddlKP.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
    '            ddlKP.SelectedIndex = 0
    '        Catch ex As Exception

    '        End Try
    '    End Sub

    '    Protected Sub ddlKP_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Try

    '            Dim gvr As GridViewRow = CType(((CType(sender, Control)).NamingContainer), GridViewRow)
    '            Dim ddlKW As DropDownList = CType(gvr.FindControl("ddlKW"), DropDownList)
    '            Dim strKodKW As String = ddlKW.SelectedItem.Value

    '            Dim ddlKO As DropDownList = CType(gvr.FindControl("ddlKO"), DropDownList)
    '            Dim strKodKO As String = ddlKO.SelectedItem.Value

    '            Dim ddlPTj As DropDownList = CType(gvr.FindControl("ddlPTj"), DropDownList)
    '            Dim strKodPTj As String = ddlPTj.SelectedItem.Value

    '            Dim ddlKP As DropDownList = CType(gvr.FindControl("ddlKP"), DropDownList)
    '            Dim strKodKP As String = ddlKP.SelectedItem.Value

    '            Dim ddlVot = DirectCast(gvr.FindControl("ddlVot"), DropDownList)
    '            ddlVot.DataSource = fLoadVot(strKodKW, strKodKO, strKodPTj, strKodKP)
    '            ddlVot.DataTextField = "Butiran"
    '            ddlVot.DataValueField = "KodVot"
    '            ddlVot.DataBind()

    '            ddlVot.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
    '            ddlVot.SelectedIndex = 0
    '        Catch ex As Exception

    '        End Try
    '    End Sub

    '    Private Function fLoadKO(ByVal strKodKW As String) As DataSet
    '        Try
    '            Dim strSql As String = "SELECT DISTINCT TOP 100 PERCENT dbo.MK_kodOperasi.KodKO, (dbo.MK_kodOperasi.KodKO + ' - ' + dbo.MK_kodOperasi.Butiran) as ButiranKO " &
    '                "FROM dbo.MK_kodOperasi INNER JOIN dbo.MK01_VotTahun ON dbo.MK_kodOperasi.KodKO = dbo.MK01_VotTahun.KodKO " &
    '                "where dbo.MK01_VotTahun.MK01_Tahun = YEAR(GETDATE()) and dbo.mk01_votTahun.KodKw = '" & strKodKW & "' ORDER BY dbo.MK_kodOperasi.Kodko"

    '            Dim ds As New DataSet
    '            Dim dbconn As New DBKewConn
    '            Dim dt As New DataTable
    '            ds = dbconn.fSelectCommand(strSql)

    '            Return ds

    '        Catch ex As Exception

    '        End Try
    '    End Function

    '    Private Function fLoadPTj(ByVal strKodKW As String, ByVal strKodKO As String) As DataSet
    '        Try
    '            Dim strSql As String = "SELECT DISTINCT   MK_PTJ.KodPTJ  , (MK_PTJ.KodPTJ   + ' - ' + MK_PTJ.Butiran  ) as ButiranPTj  
    '                    From MK_PTJ INNER Join MK01_VotTahun ON MK_PTJ.KodPTJ   = MK01_VotTahun.KodPTJ   
    '                    where MK01_VotTahun.MK01_Tahun = YEAR(GETDATE()) and MK01_VotTahun.KodKw = '" & strKodKW & "' and MK01_VotTahun.KodKO = '" & strKodKO & "' and MK_PTJ.KodPTJ <> '-'  and MK_PTJ.status = 1 
    'ORDER BY MK_PTJ.KodPTJ "

    '            Dim ds As New DataSet
    '            Dim dbconn As New DBKewConn
    '            Dim dt As New DataTable
    '            ds = dbconn.fSelectCommand(strSql)

    '            Return ds

    '        Catch ex As Exception

    '        End Try
    '    End Function

    '    Private Function fLoadKP(ByVal strKodKW As String, ByVal strKodKO As String, ByVal strKodPTj As String) As DataSet
    '        Try

    '            Dim strSql As String = "SELECT DISTINCT TOP 100 PERCENT MK_KodProjek.KodProjek , (MK_KodProjek .KodProjek  + ' - ' + MK_KodProjek.Butiran ) as Butiran  
    '                    From MK_KodProjek INNER Join MK01_VotTahun ON MK_KodProjek.KodProjek  = MK01_VotTahun.KodKP
    '                    where MK01_VotTahun.MK01_Tahun = YEAR(GETDATE()) and MK01_VotTahun.KodKw = '" & strKodKW & "' and MK01_VotTahun.KodKO = '" & strKodKO & "' and MK01_VotTahun .KodPTJ = '" & strKodPTj & "' ORDER BY MK_KodProjek.KodProjek"

    '            Dim ds As New DataSet
    '            Dim dbconn As New DBKewConn
    '            Dim dt As New DataTable
    '            ds = dbconn.fSelectCommand(strSql)
    '            Return ds

    '        Catch ex As Exception

    '        End Try
    '    End Function

    '    Private Function fLoadVot(ByVal strKodKW As String, ByVal strKodKO As String, ByVal strKodPTj As String, ByVal strKodKP As String) As DataSet
    '        Try

    '            Dim strSql As String = "SELECT DISTINCT A.KodVot, (A.KODVOT + ' - ' + B.BUTIRAN) as Butiran FROM MK03_AmTahun A INNER JOIN MK_VOT B ON A.KODVOT = B.KODVOT 
    'Where a.MK03_TAHUN = YEAR(GETDATE()) AND A.KodKw = '" & strKodKW & "' and A.KodKO = '" & strKodKO & "' AND A.KODPTJ = '" & strKodPTj & "' AND A.KodKP  = '" & strKodKP & "' AND  RIGHT(A.KODVOT,2) <> '00' ORDER BY A.KODVOT"

    '            Dim ds As New DataSet
    '            Dim dbconn As New DBKewConn
    '            Dim dt As New DataTable
    '            ds = dbconn.fSelectCommand(strSql)
    '            Return ds

    '        Catch ex As Exception

    '        End Try
    '    End Function

    'Protected Sub txtHarga_TextChanged(sender As Object, e As EventArgs)
    '    Try
    '        Dim txtHarga As TextBox = CType(CType(sender, Control), TextBox)
    '        Dim HrgUnit = txtHarga.Text
    '        Dim gvRow As GridViewRow = CType(CType(sender, Control), TextBox).Parent.Parent
    '        Dim txtKuantiti As TextBox = CType(gvRow.FindControl("txtKuantiti"), TextBox)
    '        Dim lblJumlah As Label = CType(gvRow.FindControl("lblJumlah"), Label)

    '        '  If txtKuantiti.Text IsNot String.Empty Then
    '        Dim angHrgSeunit = CDec(HrgUnit)
    '        Dim JumAngHrg As Decimal = 0.00

    '        JumAngHrg = CDec(CInt(txtKuantiti.Text) * angHrgSeunit)
    '        lblJumlah.Text = FormatNumber(JumAngHrg, 2) 'JumAngHrg.ToString("N2")
    '        ' End If

    '        txtHarga.Text = FormatNumber(txtHarga.Text, 2)
    '        fSetFooter()

    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Protected Sub txtKuantiti_TextChanged(sender As Object, e As EventArgs)
    '    Try
    '        Dim kuantiti = CType(CType(sender, Control), TextBox).Text
    '        Dim gvRow As GridViewRow = CType(CType(sender, Control), TextBox).Parent.Parent
    '        Dim txtHarga As TextBox = CType(gvRow.FindControl("txtHarga"), TextBox)
    '        Dim lblJumlah As Label = CType(gvRow.FindControl("lblJumlah"), Label)
    '        ' If txtHarga.Text IsNot String.Empty Then
    '        If txtHarga.Text = "" Then txtHarga.Text = 0
    '        Dim angHrgSeunit = CDec(txtHarga.Text)
    '        Dim JumAngHrg = CDec(CInt(kuantiti) * angHrgSeunit)
    '        lblJumlah.Text = FormatNumber(JumAngHrg, 2) 'JumAngHrg.ToString("N2")
    '        '  End If

    '        fSetFooter()

    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Private Sub fSetFooter()
    '    Try
    '        Dim decJumTrans As Decimal
    '        For Each gvRow1 As GridViewRow In gvTrans.Rows
    '            Dim decJumlah As String = CDec(Trim(CType(gvRow1.FindControl("lblJumlah"), Label).Text.TrimEnd))
    '            decJumTrans += decJumlah
    '        Next

    '        Dim footerRow = gvTrans.FooterRow
    '        CType(footerRow.FindControl("lblTotJum"), Label).Text = FormatNumber(decJumTrans, 2)
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Protected Sub txtJumLamp_TextChanged(sender As Object, e As EventArgs)
    '    Try
    '        Dim txtJumLamp As TextBox = CType(CType(sender, Control), TextBox)
    '        Dim strJumLamp As String = txtJumLamp.Text
    '        Dim decJumLamp As Decimal

    '        If strJumLamp = "" Then decJumLamp = 0 Else decJumLamp = CDec(strJumLamp)

    '        txtJumLamp.Text = FormatNumber(decJumLamp)

    '        Dim decJumTotLamp As Decimal
    '        For Each gvRow As GridViewRow In gvLamp.Rows
    '            Dim decJumlah As String = CDec(Trim(CType(gvRow.FindControl("txtJumLamp"), TextBox).Text.TrimEnd))
    '            decJumTotLamp += decJumlah
    '        Next

    '        Dim footerRow = gvLamp.FooterRow
    '        CType(footerRow.FindControl("lblTotJum"), Label).Text = FormatNumber(decJumTotLamp, 2)

    '    Catch ex As Exception

    '    End Try
    'End Sub

    Dim decJumLamp As Decimal
    Private Sub gvLamp_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvLamp.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim strJumlah As String = CType(e.Row.FindControl("lblAmaun"), Label).Text
                If strJumlah = "" Then strJumlah = "0.00"
                decJumLamp += CDec(strJumlah)
            End If

            If e.Row.RowType = DataControlRowType.Footer Then
                Dim lblTotJum As Label = CType(e.Row.FindControl("lblTotJum"), Label)
                lblTotJum.Text = FormatNumber(decJumLamp, 2)
            End If
        Catch ex As Exception

        End Try
    End Sub

    'Private Sub lbtnTambahPen_Click(sender As Object, e As EventArgs) Handles lbtnTambahPen.Click
    '    'If rbKatPel.SelectedIndex = -1 Then
    '    '    fGlobalAlert("Sila pilih kategori pelajar!", Me.Page, Me.[GetType]())
    '    '    Exit Sub
    '    'End If
    '    txtFilter.Text = ""
    '    sClearGvLstPel()
    '    mpeLstPel.Show()

    '    'Try

    '    '    Dim dtLamp As DataTable = TryCast(ViewState("dtLamp"), DataTable)

    '    '    If dtLamp Is Nothing Then
    '    '        dtLamp = sInitDtLamp()
    '    '    End If

    '    '    Dim dr As DataRow
    '    '    dr = dtLamp.NewRow()
    '    '    dr("AR02_IdLamp") = 0
    '    '    dr("AR02_NamaPenerima") = Trim(txtNamaPelajar.Text.TrimEnd)
    '    '    dr("AR02_NoMatrik") = Trim(txtNoMatrik.Text.TrimEnd)
    '    '    dr("AR02_Kursus") = Trim(txtKodKursus.Text.TrimEnd)
    '    '    dr("AR02_NoKP") = Trim(txtNoKP.Text.TrimEnd)
    '    '    dr("AR02_Butiran") = Trim(txtButiran.Text.TrimEnd)
    '    '    dr("AR02_Amaun") = Trim(txtAmaun.Text.TrimEnd)

    '    '    dtLamp.Rows.Add(dr)

    '    '    gvLamp.DataSource = dtLamp
    '    '    gvLamp.DataBind()

    '    '    ViewState("dtLamp") = dtLamp
    '    '    ddlPel.SelectedIndex = 0
    '    '    sClearPel()
    '    'Catch ex As Exception

    '    'End Try
    'End Sub

    Private Sub sClearGvLstPel()
        gvLstPel.DataSource = New List(Of String)
        gvLstPel.DataBind()
    End Sub

    Private Sub gvLstPel_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvLstPel.PageIndexChanging
        Try
            gvLstPel.PageIndex = e.NewPageIndex
            If ViewState("dtLstPel") IsNot Nothing Then
                gvLstPel.DataSource = ViewState("dtLstPel")
                gvLstPel.DataBind()
            End If
            mpeLstPel.Show()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvLstPel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvLstPel.SelectedIndexChanged
        Try

            Dim dt As New DataTable
            dt = sLoadDtLamp()

            If Not dt Is Nothing Then
                Dim selRow As GridViewRow = gvLstPel.SelectedRow
                Dim strNoMatrik As String = CType(selRow.FindControl("lblNoMatrik"), Label).Text.TrimEnd
                Dim strNmPel As String = CType(selRow.FindControl("lblNmPel"), Label).Text.TrimEnd
                Dim strKodKursus As String = CType(selRow.FindControl("lblKodKursus"), Label).Text.TrimEnd
                Dim strIdPel As String = CType(selRow.FindControl("lblIdPel"), Label).Text.TrimEnd

                Dim dr As DataRow
                dr = dt.NewRow()
                dr("AR02_IdLamp") = String.Empty
                dr("AR02_NoMatrik") = strNoMatrik
                dr("AR02_NamaPenerima") = strNmPel
                dr("AR02_Kursus") = strKodKursus
                dr("AR02_NoKP") = strIdPel
                dr("AR02_Butiran") = String.Empty
                dr("AR02_Amaun") = String.Empty

                dt.Rows.Add(dr)
                ViewState("dtLamp") = dt

                gvLamp.DataSource = dt
                gvLamp.DataBind()

            End If

        Catch ex As Exception
            fErrorLog("gvLstPel_SelectedIndexChanged - " & ex.Message.ToString)
        End Try
    End Sub



    Private Sub sLoadPel()

        Try
            'sClearGvLstPel()
            Dim strSql, strFilter As String
            Dim strFiltParam As String = Trim(txtFilter.Text.TrimEnd)

            Dim strKodkatPel As String = Trim(lblKodKatPel.Text.TrimEnd)

            If strKodkatPel = "UG" Then 'PRA SIS
                If ddlFilter.SelectedValue = 1 Then
                    'NO MATRIK
                    strFilter = " where smp01_nomatrik like '%" & strFiltParam & "%'"
                ElseIf ddlFilter.SelectedValue = 2 Then
                    'NAMA
                    strFilter = " where smp01_nama like '%" & strFiltParam & "%'"
                End If

                strSql = "Select SMP01_NOMATRIK as NoMatrik, SMP01_NAMA as NamaPel,(SMP01_NOMATRIK + ' - ' + SMP01_NAMA) as Pel, SMP01_KURSUS as KodKursus,smp01_kp as IDPel from smp01_peribadi " & strFilter & " order by smp01_nomatrik"


            ElseIf strKodkatPel = "PG" Then 'PASCA SIS
                If ddlFilter.SelectedValue = 1 Then
                    'NO MATRIK
                    strFilter = " and a.smg01_nomatrik like '%" & strFiltParam & "%'"
                ElseIf ddlFilter.SelectedValue = 2 Then
                    'NAMA
                    strFilter = " and a.smg02_nama like '%" & strFiltParam & "%'"
                End If

                strSql = "Select a.SMG01_NOMATRIK as NoMatrik, a.SMG02_NAMA as NamaPel, (a.SMG01_NOMATRIK + ' - ' +  a.SMG02_NAMA) as Pel, b.SMG01_KODKURSUS as KodKursus,a.SMG02_ID as IDPel from SMG02_PERIBADI a,SMG01_PENGAJIAN b where a.smg01_nomatrik=b.smg01_nomatrik " & strFilter & " order by a.smg01_nomatrik"
            End If

            Dim dbconn As New DBSMPConn
            Using ds = dbconn.fselectCommand(strSql)
                If ds.Tables(0).Rows.Count > 0 Then
                    gvLstPel.DataSource = ds
                    gvLstPel.DataBind()

                    Dim dt As New DataTable
                    dt = ds.Tables(0)

                    'ddlPel.DataSource = dt
                    'ddlPel.DataTextField = "Pel"
                    'ddlPel.DataValueField = "NoMatrik"
                    'ddlPel.DataBind()

                    'ddlPel.Items.Insert(0, New ListItem(" - SILA PILIH -", 0))
                    'ddlPel.SelectedIndex = 0

                    ViewState("dtLstPel") = dt
                End If
            End Using

        Catch ex As Exception

        End Try

        mpeLstPel.Show()
    End Sub

    Private Function sInitDtLamp() As DataTable
        Try
            Dim dt As DataTable = New DataTable()
            dt.Columns.Add(New DataColumn("AR02_IdLamp", GetType(String)))
            dt.Columns.Add(New DataColumn("AR02_NoMatrik", GetType(String)))
            dt.Columns.Add(New DataColumn("AR02_NamaPenerima", GetType(String)))
            dt.Columns.Add(New DataColumn("AR02_Kursus", GetType(String)))
            dt.Columns.Add(New DataColumn("AR02_NoKP", GetType(String)))
            dt.Columns.Add(New DataColumn("AR02_Butiran", GetType(String)))
            dt.Columns.Add(New DataColumn("AR02_Amaun", GetType(String)))

            Return dt

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Private Function sLoadDtLamp() As DataTable
        Try
            Dim dt As New DataTable
            dt = sInitDtLamp()
            If Not dt Is Nothing Then
                For Each gvRow As GridViewRow In gvLamp.Rows
                    Dim strIdLamp = CType(gvRow.FindControl("lblIdLamp"), Label).Text
                    Dim strNoMatrik = CType(gvRow.FindControl("lblNoMatrik"), Label).Text
                    Dim strNmPel = CType(gvRow.FindControl("lblNmPen"), Label).Text
                    Dim strKodKursus = CType(gvRow.FindControl("lblKursus"), Label).Text
                    Dim strIdPel = CType(gvRow.FindControl("lblNoKP"), Label).Text
                    Dim strButiran = CType(gvRow.FindControl("txtButiran"), TextBox).Text
                    Dim strJumlah = CType(gvRow.FindControl("lblAmaun"), Label).Text

                    Dim dr As DataRow
                    dr = dt.NewRow()
                    dr("AR02_IdLamp") = strIdLamp
                    dr("AR02_NoMatrik") = strNoMatrik
                    dr("AR02_NamaPenerima") = strNmPel
                    dr("AR02_Kursus") = strKodKursus
                    dr("AR02_NoKP") = strIdPel
                    dr("AR02_Butiran") = strButiran
                    dr("AR02_Amaun") = strJumlah

                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Private Sub ddlFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFilter.SelectedIndexChanged
        'If ddlFilter.SelectedIndex = 0 Then
        '    txtFilter.Enabled = False
        'Else
        '    txtFilter.Enabled = True
        'End If
        txtFilter.Text = ""
        mpeLstPel.Show()
    End Sub

    Private Sub lbtnCariPel_Click(sender As Object, e As EventArgs) Handles lbtnCariPel.Click
        sLoadPel()
    End Sub

    Private Sub gvLamp_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvLamp.RowDeleting
        Try
            Dim rowIndex As Integer = 0
            Dim index As Integer = Convert.ToInt32(e.RowIndex)

            Dim dt As New DataTable
            dt = sLoadDtLamp()


            'If ViewState("dtLamp") IsNot Nothing Then
            Dim dtCurrentTable As DataTable = dt ' CType(ViewState("dtLamp"), DataTable)
            Dim drCurrentRow As DataRow = Nothing

                If dtCurrentTable.Rows.Count > 1 Then
                    dtCurrentTable.Rows(index).Delete()
                    dtCurrentTable.AcceptChanges()
                    ViewState("dtLamp") = dtCurrentTable
                    gvLamp.DataSource = dtCurrentTable
                    gvLamp.DataBind()
                End If
            'End If

        Catch ex As Exception

        End Try
    End Sub

    'Private Sub ddlPel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPel.SelectedIndexChanged
    '    Try
    '        sClearPel()
    '        Dim strNoMatrik As String = Trim(ddlPel.SelectedValue)

    '        Dim dtLstPel As DataTable = TryCast(ViewState("dtLstPel"), DataTable)
    '        Dim dvLstPel As New DataView(dtLstPel)

    '        dvLstPel.RowFilter = "NoMatrik = '" & strNoMatrik & "'"

    '        txtNamaPelajar.Text = dvLstPel.Item(0)("NamaPel").ToString
    '        txtNoMatrik.Text = dvLstPel.Item(0)("NoMatrik").ToString
    '        txtNoKP.Text = dvLstPel.Item(0)("IDPel").ToString
    '        txtKodKursus.Text = dvLstPel.Item(0)("KodKursus").ToString


    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Private Sub sClearPel()
    '    txtNamaPelajar.Text = ""
    '    txtNoMatrik.Text = ""
    '    txtNoKP.Text = ""
    '    txtKodKursus.Text = ""
    '    txtButiran.Text = ""
    '    txtAmaun.Text = ""

    'End Sub
End Class