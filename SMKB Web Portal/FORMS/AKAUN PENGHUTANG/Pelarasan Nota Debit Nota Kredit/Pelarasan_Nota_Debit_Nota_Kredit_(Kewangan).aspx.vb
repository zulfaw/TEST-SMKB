Imports System.Drawing
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class Pelarasan_Nota_Debit_Nota_Kredit
    Inherits System.Web.UI.Page

    Dim sLogBaru As String
    Dim sLogMedan As String
    Dim strNoStaf As String = UserInfo.strSesStaffID
    Dim sLogLevel As String = UserInfo.strUsrTahap
    Dim lsLogUsrIP As String = UserInfo.strUsrPcIP
    Dim lsLogUsrPC As String = UserInfo.strUsrPCName

    Private dbconnSMSM As New DBSMConn()
    Private countButiran As Integer = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Session("sesKodSubMenu") = Request.QueryString("KodSubMenu")
            fLoadKW()
            fLoadKO2()
            fLoadPTj2()
            fLoadKP2()
            fLoadVot2()

            txtTkhPlrasan.Text = Date.Now

            TabContainer.ActiveTab = tabLampAsal

            fClearGvTransPel()
            fClearGvTransAsal()
            divList.Visible = True
            divDt.Visible = False
            fBindDdlFilStat()
            fLoadLst()

        End If
    End Sub

    Private Sub fBindDdlFilStat()
        Dim dbconn As New DBKewConn
        Try
            Dim strSql As String = "select KodStatus, Butiran from AR_StatusDok where KodStatus in (
03,06,13,10,11)"

            Using ds = dbconn.fSelectCommand(strSql)
                ddlFilStat.DataSource = ds
                ddlFilStat.DataTextField = "Butiran"
                ddlFilStat.DataValueField = "KodStatus"
                ddlFilStat.DataBind()

                ddlFilStat.Items.Insert(0, New ListItem("- KESELURUHAN -", "0"))
                ddlFilStat.SelectedValue = "03"
            End Using



        Catch ex As Exception

        End Try
    End Sub

    Private Sub fLoadLst()
        Dim strSql As String = ""
        Dim intRec As Integer = 0
        Dim strNoBil As String
        Dim strFilter As String
        Dim strStatDok As String
        Try
            fClearGvLst()

            If ddlFindInv.SelectedIndex = 0 Then
                strFilter = ""
            ElseIf ddlFindInv.SelectedIndex = 1 Then
                strNoBil = Trim(txtCarian.Text.TrimEnd)
                strFilter = " and AR01_NoBil  ='" & strNoBil & "'"
            End If

            If ddlFilStat.SelectedValue = 0 Then
                strStatDok = "03,06,10,11,13"
            Else
                strStatDok = ddlFilStat.SelectedValue
            End If

            strSql = $"Select distinct (select (convert (varchar,b.AR06_Tarikh,103)) from AR06_statusdok b where b.AR06_NoBil  = a.AR01_NoBil And ar06_statusdok='03' ) as AR06_Tarikh,
                        a.AR01_NoBil,a.AR01_Tujuan , a.AR01_NamaPenerima,a.ar01_jumlah as Jumlah, a.AR01_FlagAdj , a.ar01_statusdok,b.Butiran as statusbil
                        From AR01_Bil a , AR_StatusDok b
                        Where b.KodStatus  = a.AR01_StatusDok and ar01_statusdok in (" & strStatDok & ") and ar01_jenis='01' and AR01_FlagAdj in (0,1) " & strFilter & " order by ar01_nobil asc"

            Dim dbconn As New DBKewConn
            Dim ds As New DataSet
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    gvLst.DataSource = ds.Tables(0)
                    gvLst.DataBind()
                    intRec = ds.Tables(0).Rows.Count
                End If
            End If

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
    Private Sub fBindInvAdj(strNoInv As String, strFlag As Boolean, strStatDok As String)
        Try
            Dim dbconn As New DBKewConn
            Dim ds As New DataSet


            Dim strSqlInv = $"Select ar01_nobil, AR01_TkhMohon, AR01_KODPTJMOHON, AR01_NOSTAF, AR01_Jenis, AR01_KODBANK, AR01_NORUJUKAN, AR01_ALMT1,AR01_ALMT2,
                            AR01_KATEGORI, AR01_NAMAPENERIMA, kodnegara, (select b.Butiran from MK_Negara b where b.KodNegara = AR01_Bil.KodNegara) as ButNegara, kodnegeri, (select c.Butiran from MK_Negeri c where c.KodNegeri = AR01_Bil .KodNegeri) as ButNegeri, AR01_IDPENERIMA, AR01_TUJUAN, AR01_BANDAR, AR01_POSKOD, AR01_TkhKonDari, AR01_TkhKonHingga, AR01_NOTEL,
                            AR01_NOFAKS, AR01_TempohKontrak, AR01_TkhPeringatan1, AR01_TkhPeringatan2, AR01_TkhPeringatan3, AR01_TkhLulus, AR01_Emel, AR01_UtkPerhatian, AR01_DokSok, AR01_JUMLAH from AR01_Bil where ar01_nobil='{strNoInv}';"
            Dim strSqlInvStat = $" Select * From AR06_statusdok Where (AR06_statusdok ='03') and ar06_nobil='{strNoInv}';"
            Dim strSqlAdj = $"select AR04_NoAdjSem, AR04_TkhAdj from AR04_BilAdj where AR04_NoBil = '{strNoInv}';"

            ds = dbconn.fSelectCommand(strSqlInv + strSqlInvStat + strSqlAdj)


            Using dtAdj = ds.Tables(0)
                If dtAdj.Rows.Count > 0 Then

                    'Dim strDate = Right(txtNoInv.Text, 5)
                    'Dim strDate2 = Left(strDate, 1) + "/" + strDate.Substring(2, 2) + "/" + Right(strDate, 2)
                    'Dim DateInv = CDate(strDate2)

                    'Dim intThn As Integer = DateInv.Year
                    Dim TkhMhn As Date = IIf(IsDBNull(dtAdj.Rows(0)("AR01_TkhMohon")), Nothing, dtAdj.Rows(0)("AR01_TkhMohon"))
                    Dim KodPTJ As String = dtAdj.Rows(0)("AR01_KODPTJMOHON")
                    Dim NoPmhn As String = dtAdj.Rows(0)("AR01_NOSTAF")
                    Dim katInv As String = dtAdj.Rows(0)("AR01_Jenis")
                    Dim Bank As String = dtAdj.Rows(0)("AR01_KODBANK")
                    Dim NoRuj As String = IIf(IsDBNull(dtAdj.Rows(0)("AR01_NORUJUKAN")), Nothing, dtAdj.Rows(0)("AR01_NORUJUKAN"))
                    Dim Almt1 As String = dtAdj.Rows(0)("AR01_ALMT1")
                    Dim Almt2 As String = dtAdj.Rows(0)("AR01_ALMT2")
                    Dim Kat As String = dtAdj.Rows(0)("AR01_KATEGORI")
                    Dim NPenerima As String = dtAdj.Rows(0)("AR01_NAMAPENERIMA")
                    Dim Negara As String = dtAdj.Rows(0)("kodnegara")
                    Dim ButNegara As String = dtAdj.Rows(0)("ButNegara")
                    Dim Negeri As String = dtAdj.Rows(0)("kodnegeri")
                    Dim ButNegeri As String = dtAdj.Rows(0)("ButNegeri")
                    Dim IDPenerima As String = dtAdj.Rows(0)("AR01_IDPENERIMA")
                    Dim Tujuan As String = dtAdj.Rows(0)("AR01_TUJUAN")
                    Dim Poskod As String = dtAdj.Rows(0)("AR01_POSKOD")
                    Dim Bandar As String = dtAdj.Rows(0)("AR01_BANDAR")
                    Dim strJumlah As String = dtAdj.Rows(0)("AR01_JUMLAH")
                    Dim TkhKonDr As String = IIf(IsDBNull(dtAdj.Rows(0)("AR01_TkhKonDari")), Nothing, dtAdj.Rows(0)("AR01_TkhKonDari"))
                    Dim TkhKonHingga As String = IIf(IsDBNull(dtAdj.Rows(0)("AR01_TkhKonHingga")), Nothing, dtAdj.Rows(0)("AR01_TkhKonHingga"))
                    Dim NoTel As String = dtAdj.Rows(0)("AR01_NOTEL")
                    Dim NoFax As String = dtAdj.Rows(0)("AR01_NOFAKS")
                    Dim TmphKon As String = IIf(IsDBNull(dtAdj.Rows(0)("AR01_TempohKontrak")), Nothing, dtAdj.Rows(0)("AR01_TempohKontrak"))
                    Dim Emel As String = dtAdj.Rows(0)("AR01_Emel")
                    Dim Perhatian As String = dtAdj.Rows(0)("AR01_UtkPerhatian")
                    Dim TkhLulus As String = IIf(IsDBNull(dtAdj.Rows(0)("AR01_TkhLulus")), Nothing, dtAdj.Rows(0)("AR01_TkhLulus"))
                    Dim DokSok As String = IIf(IsDBNull(dtAdj.Rows(0)("AR01_DokSok")), Nothing, dtAdj.Rows(0)("AR01_DokSok"))


                    txtNoInv.Text = strNoInv
                    lblKodPTjPmhn.Text = KodPTJ
                    lblNoStafPmhn.Text = NoPmhn

                    ViewState("NoStafPmhn") = NoPmhn


                    txtIDPenerima.Text = IDPenerima
                    txtTujuan.Text = Tujuan
                    hidJumAsal.Value = strJumlah
                    txtNoRujukan.Text = NoRuj
                    txtAlamat1.Text = Almt1
                    txtAlamat2.Text = Almt2
                    txtBandar.Text = Bandar
                    txtPoskod.Text = Poskod
                    txtNoTel.Text = NoTel
                    txtNoFax.Text = NoFax
                    txtEmel.Text = Emel
                    txtPerhatian.Text = Perhatian
                    txtBank.Text = Bank

                    txtKat.Text = Kat
                    txtNegara.Text = Negara & " - " & ButNegara
                    txtNegeri.Text = Negeri & " - " & ButNegeri

                    Using dt = fGetUserInfo(NoPmhn)
                        lblJwtanMhn.Text = dt.Rows(0)("JawGiliran").ToString
                        lblNmPTjPmhn.Text = dt.Rows(0)("Pejabat").ToString
                        lblNmPmhn.Text = dt.Rows(0)("MS01_Nama").ToString
                    End Using


                End If
            End Using

            Using tkhStatusDokDt = ds.Tables(1)
                If tkhStatusDokDt.Rows.Count > 0 Then
                    Dim Tkh As Date = tkhStatusDokDt.Rows(0)("AR06_Tarikh")
                End If
            End Using

            Using dtPel = ds.Tables(2)
                If dtPel.Rows.Count > 0 Then
                    Dim strNoPelSem As String = dtPel.Rows(0)("AR04_NoAdjSem")
                    Dim dtTkhPel As Date = dtPel.Rows(0)("AR04_TkhAdj")

                    txtNoPelSem.Text = strNoPelSem
                    txtTkhPlrasan.Text = dtTkhPel.ToString("dd/MM/yyyy")

                End If
            End Using



        Catch ex As Exception

        End Try

    End Sub
    Private Sub fBindInvAdjDt(strNoInv As String, strFlag As Boolean, strStatDok As String)
        Try
            Dim dbconn As New DBKewConn
            Dim strSql As String
            Dim dtTransAsal, dtTransPel As New DataTable

            'Load Transaksi Asal Invois
            Dim strSqlInvDt = $"select KodKw,KodKO,KodPTJ,KodKP,KodVot, AR01_Perkara as Perkara,AR01_Kuantiti as Kuantiti, AR01_kadarHarga as KadarHarga,AR01_Jumlah as Jumlah, '' as Petunjuk from AR01_BilDt where AR01_NoBil='{strNoInv}';"
            Dim dsTransAsal As New DataSet
            dsTransAsal = dbconn.fSelectCommand(strSqlInvDt)

            If Not dsTransAsal Is Nothing Then
                If dsTransAsal.Tables(0).Rows.Count > 0 Then
                    dtTransAsal = dsTransAsal.Tables(0)

                    gvTransAsal.DataSource = dtTransAsal
                    gvTransAsal.DataBind()
                End If
            End If


            If strFlag And strStatDok = "10" Then
                strSql = "Select max(ar04_bilpel) as bilpel from AR04_Biladj Where ar04_nobil='" & strNoInv & "'"
                Dim ds As New DataSet
                ds = dbconn.fSelectCommand(strSql)
                If Not ds Is Nothing Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        Dim intBilPel As Integer = ds.Tables(0).Rows(0)("bilpel")
                        hidBilPel.Value = intBilPel

                        strSql = "Select ar04_jumbesar from AR04_Biladj Where ar04_nobil='" & strNoInv & "' and ar04_bilpel='" & intBilPel & "'"

                        Dim ds2 As New DataSet
                        ds2 = dbconn.fSelectCommand(strSql)
                        If Not ds2 Is Nothing Then
                            If ds2.Tables(0).Rows.Count > 0 Then
                                hidJumAsal.Value = ds2.Tables(0).Rows(0)("ar04_jumbesar")
                            End If
                        End If

                        strSql = "Select KodKw, KodKO, kodPTJ, KodKP, kodVot, ar07_perkara as Perkara, ar07_petunjuk as Petunjuk, ar07_kuantiti as Kuantiti, ar07_kadarharga as KadarHarga, ar07_jumlah as Jumlah from AR07_Bezaadj Where ar07_nobil='" & strNoInv & "' and ar07_bilpel='" & intBilPel & "'"
                        Dim ds3 As New DataSet
                        ds3 = dbconn.fSelectCommand(strSql)

                        If Not ds3 Is Nothing Then
                            If ds3.Tables(0).Rows.Count > 0 Then
                                dtTransPel = ds3.Tables(0)
                            End If
                        End If
                    End If
                End If


            ElseIf strFlag And strStatDok = "06" Then
                strSql = "select KodKw, KodKO, KodPTJ, KodKP, KodVot, AR07_Perkara as Perkara, AR07_Kuantiti as Kuantiti, AR07_KadarHarga as KadarHarga, AR07_Jumlah as Jumlah, AR07_Petunjuk as Petunjuk from AR07_BezaAdj where AR07_NoBil = '" & strNoInv & "'"

                Using ds = dbconn.fSelectCommand(strSql)
                    dtTransPel = ds.Tables(0)
                End Using


            Else
                dtTransPel = dtTransAsal


            End If

            gvTransPel.DataSource = dtTransPel
            gvTransPel.DataBind()

            ViewState("dtTransPel") = dtTransPel

            hidJumBaki.Value = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fClearGvLst()
        gvLst.DataSource = New List(Of String)
        gvLst.DataBind()
    End Sub

    Private Sub fClearGvTransAsal()
        gvTransAsal.DataSource = New List(Of String)
        gvTransAsal.DataBind()
    End Sub
    Private Sub fClearGvTransPel()
        gvTransPel.DataSource = New List(Of String)
        gvTransPel.DataBind()
    End Sub

    'Function fGetID_Bil()
    '    Try
    '        Dim strSql As String
    '        Dim strIdx As String
    '        Dim intLastIdx As Integer
    '        Dim strCol As String
    '        Dim strTahun As String = Now.Year
    '        Dim strKodModul As String
    '        Dim strPrefix As String
    '        Dim strButiran As String = "Max No Pelarasan Sementara AR"

    '        strSql = "select * from mk_noakhir where tahun = '" & strTahun & "' and kodmodul='AR' and prefix='S'"
    '        strCol = "NoAkhir"
    '        strKodModul = "AR"
    '        strPrefix = "S"

    '        Dim ds As New DataSet
    '        Dim dbconn As New DBKewConn
    '        ds = dbconn.fselectCommand(strSql)

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
    '                Else
    '                    dbconn.sConnRollbackTrans()
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
    '                Else
    '                    dbconn.sConnRollbackTrans()
    '                End If

    '            End If

    '            Return strIdx
    '        End If


    '    Catch ex As Exception

    '    End Try
    'End Function


    Private Sub fClear()

        txtNoInv.Text = ""
        txtTkhBil.Text = ""
        txtBank.Text = ""
        txtKat.Text = ""
        txtNPenerima.Text = ""
        txtIDPenerima.Text = ""
        txtTujuan.Text = ""
        txtNoPelSem.Text = ""
        txtTkhPlrasan.Text = ""
        txtNoRujukan.Text = ""
        txtAlamat1.Text = ""
        txtAlamat2.Text = ""
        txtNegara.Text = ""
        txtNegeri.Text = ""
        txtBandar.Text = ""
        txtPoskod.Text = ""
        txtNoTel.Text = ""
        txtNoFax.Text = ""
        txtEmel.Text = ""
        txtPerhatian.Text = ""

    End Sub

    Private Function fSimpan() As Boolean
        Dim blnSuccess As Boolean = True
        Dim dbconn As New DBKewConn
        Dim ds As New DataSet
        Dim strNoPelSem As String
        Dim strTahun As String = Now.Year
        Try
            Dim strSql As String
            Dim strNoInv As String = Trim(txtNoInv.Text.TrimEnd)

            Dim tkhBil As String = Trim(txtTkhBil.Text.Trim)
            Dim strTkhToday As String = Now.ToString("yyyy-MM-dd")
            Dim dtTkhToday As DateTime = CDate(strTkhToday)
            Dim strPerhatian As String = Trim(txtPerhatian.Text.TrimEnd)
            Dim strNoRujuk As String = Trim(txtNoRujukan.Text.TrimEnd)
            Dim IDPenerima As String = Trim(txtIDPenerima.Text.TrimEnd)
            Dim NamaPenerima As String = Trim(txtNPenerima.Text.ToString)
            Dim strAlamat1 As String = Trim(txtAlamat1.Text.TrimEnd)
            Dim strAlamat2 As String = Trim(txtAlamat2.Text.TrimEnd)
            Dim strBdr As String = Trim(txtBandar.Text.TrimEnd)
            Dim strPoskod As String = Trim(txtPoskod.Text.TrimEnd)
            Dim KodNegara As String = Trim(txtNegara.Text.TrimEnd)
            Dim KodNegeri As String = Trim(txtNegeri.Text.TrimEnd)
            Dim strEmel As String = Trim(txtEmel.Text.TrimEnd)
            Dim strNoTel As String = Trim(txtNoTel.Text.TrimEnd)
            Dim strNoFax As String = Trim(txtNoFax.Text.TrimEnd)

            Dim strKat As String = Trim(txtKat.Text.TrimEnd)
            Dim strBank As String = Trim(txtBank.Text.TrimEnd)
            Dim strTujuan As String = Trim(txtTujuan.Text.TrimEnd)

            Dim strUlasan As String = Trim(txtUlasBeza.Text.Trim)

            Dim strNoStaf As String = Trim(lblNoStafPmhn.Text.TrimEnd)


            ' Dim decJumasal As Decimal = CDec(hidJumAsal.Value)
            'jumPel = CDbl(Me.lbljumbaki.caption)
            'Dim decJumPel As Decimal = CDec(hidJumBaki.Value)


            Dim decJumDt, decJumPel As Decimal
            For Each gvrow As GridViewRow In gvTransPel.Rows
                decJumDt = CDec(TryCast(gvrow.FindControl("lblJum"), Label).Text)
                decJumPel += decJumDt
            Next

            strSql = "select max(ar04_bilpel) as bil from ar04_biladj where ar04_nobil='" & strNoInv & "'"
            ds = New DataSet
            ds = dbconn.fSelectCommand(strSql)

            Dim intBilPel As Integer
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    If IsDBNull(ds.Tables(0).Rows(0)("bil")) Then
                        intBilPel = 1
                    Else
                        intBilPel = CInt(ds.Tables(0).Rows(0)("bil")) + 1
                    End If

                Else
                    intBilPel = 1
                End If
            Else
                intBilPel = 1
            End If

            dbconn.sConnBeginTrans()
            strSql = $"UPDATE ar01_bil set ar01_statusdok='06',ar01_flagadj='1',ar01_jumblmbyr='{decJumPel}' where ar01_nobil='{strNoInv}'"
            Dim paramSql() As SqlParameter =
                       {
                       New SqlParameter("@NOBIL", strNoInv),
                       New SqlParameter("@Status", "06"),
                       New SqlParameter("@flagadj", "1"),
                       New SqlParameter("@jumBlmByr", decJumPel)
                       }

            If Not dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                blnSuccess = False
                Exit Try
            Else
                'AUDIT LOG
                sLogMedan = "ar01_statusdok|ar01_flagadj|ar01_jumblmbyr|ar01_nobil"
                sLogBaru = "06|1|" & decJumPel & "|" & decJumPel

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
                    New SqlParameter("@InfoTable", "ar01_bil"),
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


            Dim strButiranNoAkhir As String = "Max No Pelarasan Sementara AR"
            Dim strKodModul = "AR"
            Dim strPrefix = "S"

            strNoPelSem = fGetNo(strTahun, strKodModul, strPrefix, strButiranNoAkhir)


            strSql = "insert into AR04_BilAdj  (AR04_NoAdjSem , AR04_NoAdj , AR04_NoBil , AR04_TkhAdj , AR04_Ulasan , AR04_JumBesar , AR04_BilPel , AR04_Tujuan , AR04_JumBlmByr ,AR04_JumGST , AR04_JumTanpaGST)" &
                          "values (@NoAdjSem , @NoAdj , @NoBil , @TkhAdj , @UlasBeza , @JumBesar , @BilPel , @Tujuan , @JumBlmByr ,@JumGST , @JumTanpaGST)"

            Dim paramSql1() As SqlParameter =
                    {
                    New SqlParameter("@NoAdjSem", strNoPelSem),
                    New SqlParameter("@NoAdj", strNoPelSem),
                    New SqlParameter("@NoBil", strNoInv),
                    New SqlParameter("@TkhAdj", dtTkhToday),
                    New SqlParameter("@UlasBeza", strUlasan),
                    New SqlParameter("@JumBesar", decJumPel),
                    New SqlParameter("@BilPel", intBilPel),
                    New SqlParameter("@Tujuan", strTujuan),
                    New SqlParameter("@JumBlmByr", decJumPel),
                     New SqlParameter("@JumGST", "0.00"),
                    New SqlParameter("@JumTanpaGST", "0.00")
                    }

            If Not dbconn.fInsertCommand(strSql, paramSql1) > 0 Then
                blnSuccess = False
                Exit Try
            Else
                'AUDIT LOG
                sLogMedan = "AR04_NoAdjSem|AR04_NoAdj|AR04_NoBil|AR04_TkhAdj|AR04_Ulasan|AR04_JumBesar|AR04_BilPel|AR04_Tujuan|AR04_JumBlmByr|AR04_JumGST|AR04_JumTanpaGST"
                sLogBaru = strNoPelSem & "|" & strNoPelSem & "|" & strNoInv & "|" & dtTkhToday & "|" & strUlasan & "|" & decJumPel & "|" & intBilPel & "|" & strTujuan & "|" & decJumPel & "|0.00|0.00"

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
                    New SqlParameter("@InfoTable", "AR04_BilAdj"),
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

            strSql = "select COUNT (*) from AR07_BEZAADJ Where AR04_NoAdjSem = '" & strNoPelSem & "'"

            If fCheckRec(strSql) > 0 Then
                strSql = $"delete from AR07_BEZAADJ Where AR04_NoAdjSem = @NoAdjSem"

                paramSql =
                           {
                           New SqlParameter("@NoAdjSem", strNoPelSem)
                           }

                If Not dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                    blnSuccess = False
                    Exit Try
                Else
                    'AUDIT LOG
                    sLogMedan = "AR04_NoAdjSem"
                    sLogBaru = strNoPelSem

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
                        New SqlParameter("@InfoTable", "AR07_BezaAdj"),
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
            End If




            Dim intBil As Integer
            Dim strKodKW, strKodKO, strPTJ, strKodKP, strKodVot, strPerkara, strPetunjuk As String
            Dim decKuantiti, decKdrHrg, decJum As Decimal
            For Each gvTransInvrow As GridViewRow In gvTransPel.Rows
                intBil = intBil + 1
                strKodKW = TryCast(gvTransInvrow.FindControl("ddlKW"), DropDownList).SelectedValue
                strKodKO = TryCast(gvTransInvrow.FindControl("ddlKO"), DropDownList).SelectedValue
                strPTJ = TryCast(gvTransInvrow.FindControl("ddlPTj"), DropDownList).SelectedValue
                strKodKP = TryCast(gvTransInvrow.FindControl("ddlKP"), DropDownList).SelectedValue
                strKodVot = TryCast(gvTransInvrow.FindControl("ddlVot"), DropDownList).SelectedValue
                strPerkara = TryCast(gvTransInvrow.FindControl("txtPerkara"), TextBox).Text
                strPetunjuk = TryCast(gvTransInvrow.FindControl("ddlPetunjuk"), DropDownList).SelectedValue
                decKuantiti = CDec(TryCast(gvTransInvrow.FindControl("txtKuantiti"), TextBox).Text)
                decKdrHrg = CDec(TryCast(gvTransInvrow.FindControl("txtHarga"), TextBox).Text)
                decJum = CDec(TryCast(gvTransInvrow.FindControl("lblJum"), Label).Text)

                strSql = "INSERT INTO AR07_BEZAADJ(AR07_NoAdj,AR04_NoAdjSem,AR07_Bil,KodKw,KodKo,KodPTJ, KodKP, KodVot,AR07_Perkara, AR07_Petunjuk,AR07_Kuantiti,AR07_kadarHarga,AR07_Jumlah,AR07_NoBil,AR07_BilPel,AR04_Bil,AR07_Kriteria,VotGST, JenTax, KodTax, PtjGST, KwGST, AktGST, AR07_JumGST, AR07_JumTanpaGST)" &
                          "values (@NoAdj, @NoAdjSem ,@Bil,@KodKw,@KodKo, @KodPTJ, @KodKP, @KodVot, @Perkara, @Petunjuk,@Kuantiti, @kadarHarga, @Jum, @NoBil,@BilPel,@AR04Bil, @Kriteria,
                          @VotGST, @JenTax, @KodTax, @PtjGST, @KwGST, @AktGST, @JumGST, @JumTanpaGST)"

                Dim paramSql2() As SqlParameter =
                    {
                New SqlParameter("@NoAdj", strNoPelSem),
                New SqlParameter("@NoAdjSem", strNoPelSem),
                New SqlParameter("@Bil", intBil),
                New SqlParameter("@KodKw", strKodKW),
                New SqlParameter("@KodKo", strKodKO),
                New SqlParameter("@KodPTJ", strPTJ),
                New SqlParameter("@KodKP", strKodKP),
                New SqlParameter("@KodVot", strKodVot),
                New SqlParameter("@Perkara", strPerkara),
                New SqlParameter("@Petunjuk", strPetunjuk),
                New SqlParameter("@Kuantiti", decKuantiti),
                New SqlParameter("@kadarHarga", decKdrHrg),
                New SqlParameter("@Jum", decJum),
                New SqlParameter("@NoBil", strNoInv),
                New SqlParameter("@BilPel", intBilPel),
                New SqlParameter("@AR04Bil", intBil),
                New SqlParameter("@Kriteria", ""),
                New SqlParameter("@VotGST", ""),
                New SqlParameter("@JenTax", "SUP"),
                New SqlParameter("@KodTax", ""),
                New SqlParameter("@PtjGST", ""),
                New SqlParameter("@KwGST", ""),
                New SqlParameter("@AktGST", ""),
                New SqlParameter("@JumGST", 0.00),
                New SqlParameter("@JumTanpaGST", 0.00)
                    }

                If Not dbconn.fInsertCommand(strSql, paramSql2) > 0 Then
                    blnSuccess = False
                    Exit Try
                Else
                    'AUDIT LOG
                    sLogMedan = "AR07_NoAdj|AR04_NoAdjSem|AR07_Bil|KodKw|KodKo|KodPTJ| KodKP| KodVot|AR07_Perkara| AR07_Petunjuk|AR07_Kuantiti|AR07_kadarHarga|AR07_Jumlah|AR07_NoBil|AR07_BilPel|AR04_Bil|AR07_Kriteria|VotGST| JenTax| KodTax| PtjGST| KwGST| AktGST| AR07_JumGST| AR07_JumTanpaGST"
                    sLogBaru = strNoPelSem & "|" & strNoPelSem & "|" & intBil & "|" & strKodKW & "|" & strKodKO & "|" & strPTJ & "|" & strKodKP & "|" & strKodVot & "|" & strPerkara & "|" & strPetunjuk & "|" & decKuantiti & "|" & decKdrHrg & "|" & decJum & "|" & strNoInv & "|" & intBilPel & "|" & intBil & "|||SUP|||||0.00|0.00"

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
                        New SqlParameter("@InfoTable", "AR07_BEZAADJ"),
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

            Next

            'Insert Into AR06_STATUSDOK
            strSql = "INSERT INTO AR06_STATUSDOK(AR06_NOBILSEM,AR06_NoBil, AR06_StatusDok,AR06_Tarikh,AR06_Ulasan, AR06_NoStaf)" &
                             "VALUES (@NOBILSEM,@NOBIL, @STATUSDOK,@TARIKH,@ULASAN, @NOSTAF)"

            Dim paramSql3() As SqlParameter =
                {
                New SqlParameter("@NOBILSEM", strNoPelSem),
                 New SqlParameter("@NOBIL", strNoInv),
                 New SqlParameter("@STATUSDOK", "06"),
                 New SqlParameter("@TARIKH", dtTkhToday),
                 New SqlParameter("@ULASAN", " - "),
                  New SqlParameter("@NOSTAF", strNoStaf)
                }

            If Not dbconn.fInsertCommand(strSql, paramSql3) > 0 Then
                blnSuccess = False
                Exit Try
            End If


        Catch ex As Exception
            blnSuccess = False
        End Try

        If blnSuccess = True Then
            dbconn.sConnCommitTrans()
            ViewState("SaveInv") = True
            txtNoPelSem.Text = strNoPelSem
            txtTkhPlrasan.Text = Now.ToString("dd-MM-yyyy")
            Return True
        ElseIf blnSuccess = False Then
            dbconn.sConnRollbackTrans()
            Return False
        End If

    End Function

    Private Function fKemasKini(ByVal strNoPelSem) As Boolean
        Dim strSql As String
        Dim dbconn As New DBKewConn
        Dim blnSuccess As Boolean = True
        Dim paramSql() As SqlParameter

        Try
            Dim strNoInv As String = Trim(txtNoInv.Text.TrimEnd)
            ' Dim strNoPel As String = Trim(txtNoPelSem.Text.TrimEnd)
            Dim strUlasan As String = Trim(txtUlasBeza.Text.TrimEnd)
            Dim dtTkhToday As Date = CDate(Now.ToString("yyyy-MM-dd"))
            Dim strNoStaf As String = ViewState("NoStafPmhn")

            If String.IsNullOrEmpty(strNoStaf) Then
                blnSuccess = False
                Exit Try
            End If

            Dim decJumBaru, decTotJumBaru As Decimal
            For Each gvrow As GridViewRow In gvTransPel.Rows
                decJumBaru = CDec(TryCast(gvrow.FindControl("lblJum"), Label).Text)
                decTotJumBaru += decJumBaru
            Next

            Dim intBilPel As Integer
            strSql = "select max(ar04_bilpel) as bil from ar04_biladj where ar04_nobil='" & strNoInv & "'"
            Using ds = dbconn.fSelectCommand(strSql)
                If ds.Tables(0).Rows.Count > 0 Then
                    If IsDBNull(ds.Tables(0).Rows(0)("bil")) Then
                        intBilPel = 1
                    Else
                        intBilPel = CInt(ds.Tables(0).Rows(0)("bil")) + 1
                    End If

                Else
                    intBilPel = 1
                End If
            End Using

            dbconn.sConnBeginTrans()

            strSql = "UPDATE ar01_bil set ar01_statusdok= @StatDok,ar01_flagadj= @flagAdj,ar01_jumblmbyr=@JumBlmByr where ar01_nobil=@NoBil"

            paramSql =
                {
                New SqlParameter("@StatDok", "06"),
                 New SqlParameter("@flagAdj", 1),
                 New SqlParameter("@JumBlmByr", decTotJumBaru),
                 New SqlParameter("@NoBil", strNoInv)
                }

            If Not dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                blnSuccess = False
                Exit Try
            Else
                'AUDIT LOG
                sLogMedan = "ar01_statusdok|ar01_flagadj|ar01_jumblmbyr|ar01_nobil"
                sLogBaru = "06|1|" & decTotJumBaru & "|" & strNoInv

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
                    New SqlParameter("@InfoTable", "ar01_bil"),
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

            strSql = "update AR04_BilAdj set AR04_Ulasan = @Ulasan, AR04_JumBesar = @JumBesar, AR04_JumBlmByr = @JumBlmByr, AR04_BilPel = @BilPel where AR04_NoBil = @NoBil"

            paramSql =
                {
                New SqlParameter("@Ulasan", strUlasan),
                 New SqlParameter("@JumBesar", decTotJumBaru),
                 New SqlParameter("@JumBlmByr", decTotJumBaru),
                 New SqlParameter("@BilPel", intBilPel),
                 New SqlParameter("@NoBil", strNoInv)
                }

            If Not dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                blnSuccess = False
                Exit Try
            Else
                'AUDIT LOG
                sLogMedan = "AR04_Ulasan|AR04_JumBesar|AR04_JumBlmByr|AR04_BilPel|AR04_NoBil"
                sLogBaru = strUlasan & "|" & decTotJumBaru & "|" & decTotJumBaru & "|" & intBilPel & "|" & strNoInv

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
                    New SqlParameter("@InfoTable", "AR04_BilAdj"),
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

            strSql = $"delete from AR07_BEZAADJ Where AR04_NoAdjSem = '{strNoPelSem}'"
            If Not dbconn.fUpdateCommand(strSql) > 0 Then
                blnSuccess = False
                Exit Try
            Else
                'AUDIT LOG
                sLogMedan = "AR04_NoAdjSem"
                sLogBaru = strNoPelSem

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
                    New SqlParameter("@InfoTable", "AR07_BEZAADJ"),
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

            Dim intBil As Integer
            Dim strKodKW, strKodKO, strPTJ, strKodKP, strKodVot, strPerkara, strPetunjuk As String
            Dim decKuantiti, decKdrHrg, decJum As Decimal
            For Each gvTransInvrow As GridViewRow In gvTransPel.Rows
                intBil = intBil + 1
                strKodKW = TryCast(gvTransInvrow.FindControl("ddlKW"), DropDownList).SelectedValue
                strKodKO = TryCast(gvTransInvrow.FindControl("ddlKO"), DropDownList).SelectedValue
                strPTJ = TryCast(gvTransInvrow.FindControl("ddlPTj"), DropDownList).SelectedValue
                strKodKP = TryCast(gvTransInvrow.FindControl("ddlKP"), DropDownList).SelectedValue
                strKodVot = TryCast(gvTransInvrow.FindControl("ddlVot"), DropDownList).SelectedValue
                strPerkara = TryCast(gvTransInvrow.FindControl("txtPerkara"), TextBox).Text
                strPetunjuk = TryCast(gvTransInvrow.FindControl("ddlPetunjuk"), DropDownList).SelectedValue
                decKuantiti = CDec(TryCast(gvTransInvrow.FindControl("txtKuantiti"), TextBox).Text)
                decKdrHrg = CDec(TryCast(gvTransInvrow.FindControl("txtHarga"), TextBox).Text)
                decJum = CDec(TryCast(gvTransInvrow.FindControl("lblJum"), Label).Text)

                strSql = "INSERT INTO AR07_BEZAADJ(AR07_NoAdj,AR04_NoAdjSem,AR07_Bil,KodKw,KodKo,KodPTJ, KodKP, KodVot,AR07_Perkara, AR07_Petunjuk,AR07_Kuantiti,AR07_kadarHarga,AR07_Jumlah,AR07_NoBil,AR07_BilPel,AR04_Bil,AR07_Kriteria,VotGST, JenTax, KodTax, PtjGST, KwGST, AktGST, AR07_JumGST, AR07_JumTanpaGST)" &
                          "values (@NoAdj, @NoAdjSem ,@Bil,@KodKw,@KodKo, @KodPTJ, @KodKP, @KodVot, @Perkara, @Petunjuk,@Kuantiti, @kadarHarga, @Jum, @NoBil,@BilPel,@AR04Bil, @Kriteria,
                          @VotGST, @JenTax, @KodTax, @PtjGST, @KwGST, @AktGST, @JumGST, @JumTanpaGST)"

                Dim paramSql2() As SqlParameter =
                    {
                New SqlParameter("@NoAdj", strNoPelSem),
                New SqlParameter("@NoAdjSem", strNoPelSem),
                New SqlParameter("@Bil", intBil),
                New SqlParameter("@KodKw", strKodKW),
                New SqlParameter("@KodKo", strKodKO),
                New SqlParameter("@KodPTJ", strPTJ),
                New SqlParameter("@KodKP", strKodKP),
                New SqlParameter("@KodVot", strKodVot),
                New SqlParameter("@Perkara", strPerkara),
                New SqlParameter("@Petunjuk", strPetunjuk),
                New SqlParameter("@Kuantiti", decKuantiti),
                New SqlParameter("@kadarHarga", decKdrHrg),
                New SqlParameter("@Jum", decJum),
                New SqlParameter("@NoBil", strNoInv),
                New SqlParameter("@BilPel", intBilPel),
                New SqlParameter("@AR04Bil", intBil),
                New SqlParameter("@Kriteria", ""),
                New SqlParameter("@VotGST", ""),
                New SqlParameter("@JenTax", "SUP"),
                New SqlParameter("@KodTax", ""),
                New SqlParameter("@PtjGST", ""),
                New SqlParameter("@KwGST", ""),
                New SqlParameter("@AktGST", ""),
                New SqlParameter("@JumGST", 0.00),
                New SqlParameter("@JumTanpaGST", 0.00)
                    }

                If Not dbconn.fInsertCommand(strSql, paramSql2) > 0 Then
                    blnSuccess = False
                    Exit Try
                Else
                    'AUDIT LOG
                    sLogMedan = "AR07_NoAdj|AR04_NoAdjSem|AR07_Bil|KodKw|KodKo|KodPTJ| KodKP| KodVot|AR07_Perkara| AR07_Petunjuk|AR07_Kuantiti|AR07_kadarHarga|AR07_Jumlah|AR07_NoBil|AR07_BilPel|AR04_Bil|AR07_Kriteria|VotGST| JenTax| KodTax| PtjGST| KwGST| AktGST| AR07_JumGST| AR07_JumTanpaGST"
                    sLogBaru = strNoPelSem & "|" & strNoPelSem & "|" & intBil & "|" & strKodKW & "|" & strKodKO & "|" & strPTJ & "|" & strKodKP & "|" & strKodVot & "|" & strPerkara & "|" & strPetunjuk & "|" & decKuantiti & "|" & decKdrHrg & "|" & decJum & "|" & strNoInv & "|" & intBilPel & "|" & intBil & "|||SUP|||||0.00|0.00"

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
                        New SqlParameter("@InfoTable", "AR07_BEZAADJ"),
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

            Next

            'Insert Into AR06_STATUSDOK
            strSql = "update AR06_STATUSDOK set AR06_Tarikh= @TARIKH, AR06_Ulasan = @ULASAN where AR06_NOBILSEM = @NOBILSEM and AR06_NoBil = @NOBIL and AR06_StatusDok = '06'"

            Dim paramSql3() As SqlParameter =
                {
                New SqlParameter("@NOBILSEM", strNoPelSem),
                 New SqlParameter("@NOBIL", strNoInv),
                 New SqlParameter("@STATUSDOK", "06"),
                 New SqlParameter("@TARIKH", dtTkhToday),
                 New SqlParameter("@ULASAN", strUlasan),
                  New SqlParameter("@NOSTAF", strNoStaf)
                }

            If Not dbconn.fUpdateCommand(strSql, paramSql3) > 0 Then
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
                    where MK01_VotTahun.MK01_Tahun = YEAR(GETDATE()) and MK_PTJ.KodPTJ <> '-' and MK_PTJ.KodKategoriPTJ = 'P' and MK_PTJ.status = 1 
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
    Private Function fLoadKO(ByVal strKodKW As String) As DataSet
        Try

            'Dim strSql As String = "SELECT DISTINCT TOP 100 PERCENT dbo.MK_kodOperasi.KodKO, (dbo.MK_kodOperasi.KodKO + ' - ' + dbo.MK_kodOperasi.Butiran) as ButiranKO " &
            '    "FROM dbo.MK_kodOperasi INNER JOIN dbo.MK01_VotTahun ON dbo.MK_kodOperasi.KodKO = dbo.MK01_VotTahun.KodKO " &
            '    "where dbo.MK01_VotTahun.MK01_Tahun = '" & Now.Year & "' and dbo.mk01_votTahun.KodKw = '" & ddlKW.SelectedValue & "' and dbo.mk01_votTahun.KodPTj = '" & ddlPTJ.SelectedValue & "' ORDER BY dbo.MK_kodOperasi.Kodko"

            Dim strSql As String = "SELECT DISTINCT TOP 100 PERCENT dbo.MK_kodOperasi.KodKO, (dbo.MK_kodOperasi.KodKO + ' - ' + dbo.MK_kodOperasi.Butiran) as ButiranKO " &
                "FROM dbo.MK_kodOperasi INNER JOIN dbo.MK01_VotTahun ON dbo.MK_kodOperasi.KodKO = dbo.MK01_VotTahun.KodKO " &
                "where dbo.MK01_VotTahun.MK01_Tahun = YEAR(GETDATE()) and dbo.mk01_votTahun.KodKw = '" & strKodKW & "' ORDER BY dbo.MK_kodOperasi.Kodko"

            'Dim strSql As String = "SELECT DISTINCT TOP 100 PERCENT dbo.MK_kodOperasi.KodKO, (dbo.MK_kodOperasi.KodKO + ' - ' + dbo.MK_kodOperasi.Butiran) as ButiranKO " &
            '    "FROM dbo.MK_kodOperasi INNER JOIN dbo.MK01_VotTahun ON dbo.MK_kodOperasi.KodKO = dbo.MK01_VotTahun.KodKO " &
            '    "where dbo.MK01_VotTahun.MK01_Tahun = '" & Now.Year & "' ORDER BY dbo.MK_kodOperasi.Kodko"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)
            'ViewState("dsKO") = ds
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
            'ViewState("dsPTj") = ds
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
            'ViewState("dsKP") = ds
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
            'ViewState("dsKP") = ds
            Return ds

        Catch ex As Exception

        End Try
    End Function
    Protected Sub ddlKW_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try

            Dim gvr As GridViewRow = CType(((CType(sender, Control)).NamingContainer), GridViewRow)
            Dim ddlKW As DropDownList = CType(gvr.FindControl("ddlKW"), DropDownList)
            Dim strKodKW As String = ddlKW.SelectedItem.Value

            'Load ddlKO
            Dim ddlKO = DirectCast(gvr.FindControl("ddlKO"), DropDownList)
            ddlKO.DataSource = fLoadKO(strKodKW)   'ViewState("dsKO")
            ddlKO.DataTextField = "ButiranKO"
            ddlKO.DataValueField = "KodKO"
            ddlKO.DataBind()
            ddlKO.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlKO.SelectedIndex = 0

            'Set ddlPTj kepada default
            Dim ddlPTj = DirectCast(gvr.FindControl("ddlPTj"), DropDownList)
            ddlPTj.Items.Clear()
            ddlPTj.Items.Insert(0, New ListItem("- SILA PILIH KO -", 0))
            ddlPTj.SelectedIndex = 0

            'Set ddlKP kepada default
            Dim ddlKP = DirectCast(gvr.FindControl("ddlKP"), DropDownList)
            ddlKP.Items.Clear()
            ddlKP.Items.Insert(0, New ListItem("- SILA PILIH PTj -", 0))
            ddlKP.SelectedIndex = 0

            'Set ddlVot kepada default
            Dim ddlVot = DirectCast(gvr.FindControl("ddlVot"), DropDownList)
            ddlVot.Items.Clear()
            ddlVot.Items.Insert(0, New ListItem("- SILA PILIH KP -", 0))
            ddlVot.SelectedIndex = 0

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

            'Load ddlPTj
            Dim ddlPTj = DirectCast(gvr.FindControl("ddlPTj"), DropDownList)
            ddlPTj.DataSource = fLoadPTj(strKodKW, strKodKO)   'ViewState("dsKO")
            ddlPTj.DataTextField = "ButiranPTj"
            ddlPTj.DataValueField = "KodPTj"
            ddlPTj.DataBind()
            ddlPTj.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlPTj.SelectedIndex = 0

            'set ddlKP kepada default
            Dim ddlKP = DirectCast(gvr.FindControl("ddlKP"), DropDownList)
            ddlKP.Items.Clear()
            ddlKP.Items.Insert(0, New ListItem("- SILA PILIH PTj -", 0))
            ddlKP.SelectedIndex = 0

            'set ddlVot kepada default
            Dim ddlVot = DirectCast(gvr.FindControl("ddlVot"), DropDownList)
            ddlVot.Items.Clear()
            ddlVot.Items.Insert(0, New ListItem("- SILA PILIH KP -", 0))
            ddlVot.SelectedIndex = 0


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

            'Load ddlKP
            Dim ddlKP = DirectCast(gvr.FindControl("ddlKP"), DropDownList)
            ddlKP.DataSource = fLoadKP(strKodKW, strKodKO, strKodPTj)   'ViewState("dsKO")
            ddlKP.DataTextField = "Butiran"
            ddlKP.DataValueField = "KodProjek"
            ddlKP.DataBind()
            ddlKP.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlKP.SelectedIndex = 0

            'set ddlVot kepada default
            Dim ddlVot = DirectCast(gvr.FindControl("ddlVot"), DropDownList)
            ddlVot.Items.Clear()
            ddlVot.Items.Insert(0, New ListItem("- SILA PILIH KP -", 0))
            ddlVot.SelectedIndex = 0

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

            'Load ddlVot
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
    Protected Sub txtKuantiti_TextChanged(sender As Object, e As EventArgs)
        Dim decHarga As Decimal
        Dim intKtt As Integer
        Try
            Dim strKtt = CType(CType(sender, Control), TextBox).Text
            Dim gvRow As GridViewRow = CType(CType(sender, Control), TextBox).Parent.Parent
            Dim txtHarga As TextBox = CType(gvRow.FindControl("txtHarga"), TextBox)
            Dim lblJumlah As Label = CType(gvRow.FindControl("lblJum"), Label)

            Dim strHarga As String = txtHarga.Text

            If strHarga = "" Then decHarga = 0 Else decHarga = CDec(strHarga)
            If strKtt = "" Then intKtt = 0 Else intKtt = CInt(strKtt)

            Dim JumHarga As Decimal = intKtt * decHarga
            lblJumlah.Text = FormatNumber(JumHarga, 2)

            fSetFooter()

            Dim ddlPetunjuk As DropDownList = CType(gvRow.FindControl("ddlPetunjuk"), DropDownList)
            Dim strPetunjuk As String = ddlPetunjuk.SelectedValue

            'sKiraBaki(strPetunjuk, intKtt, decHarga)

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub txtHarga_TextChanged(sender As Object, e As EventArgs)
        Dim decHarga As Decimal
        Dim intKtt As Integer
        Try

            Dim txtHarga As TextBox = CType(CType(sender, Control), TextBox)
            Dim gvRow As GridViewRow = CType(CType(sender, Control), TextBox).Parent.Parent
            Dim txtKuantiti As TextBox = CType(gvRow.FindControl("txtKuantiti"), TextBox)
            Dim lblJumlah As Label = CType(gvRow.FindControl("lblJum"), Label)

            'Dim angHrgSeunit = CDec(HrgUnit)
            'Dim JumAngHrg As Decimal = 0.00

            Dim strHarga As String = txtHarga.Text
            Dim strKtt As String = txtKuantiti.Text

            If strHarga = "" Then decHarga = 0 Else decHarga = CDec(strHarga)
            If strKtt = "" Then intKtt = 0 Else intKtt = CInt(strKtt)


            Dim JumHarga As Decimal = intKtt * decHarga
            lblJumlah.Text = FormatNumber(JumHarga)
            txtHarga.Text = FormatNumber(decHarga)


            fSetFooter()

            Dim ddlPetunjuk As DropDownList = CType(gvRow.FindControl("ddlPetunjuk"), DropDownList)
            Dim strPetunjuk As String = ddlPetunjuk.SelectedValue


            'sKiraBaki(strPetunjuk, intKtt, decHarga)


        Catch ex As Exception

        End Try

    End Sub
    Private Sub fSetFooter()
        Try
            Dim decJumTrans As Decimal
            For Each gvRow1 As GridViewRow In gvTransPel.Rows
                Dim decJumlah As String = CDec(Trim(CType(gvRow1.FindControl("lblJum"), Label).Text.TrimEnd))
                decJumTrans += decJumlah
            Next

            Dim footerRow = gvTransPel.FooterRow
            CType(footerRow.FindControl("lblTotJum"), Label).Text = FormatNumber(decJumTrans, 2)
        Catch ex As Exception

        End Try
    End Sub
    'Protected Sub txtJumlah_TextChanged(sender As Object, e As EventArgs)
    '    Try
    '        Dim decJumTrans As Decimal
    '        For Each gvRow As GridViewRow In GvTransInvAdj.Rows
    '            Dim decJumlah As String = CDec(Trim(CType(gvRow.FindControl("lblJum"), Label).Text.TrimEnd))
    '            decJumTrans += decJumlah
    '        Next

    '        Dim footerRow = GvTransInvAdj.FooterRow
    '        CType(footerRow.FindControl("lblTotJum"), Label).Text = FormatNumber(decJumTrans, 2)


    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub AddNewRowToGrid()
        Try
            Dim rowIndex As Integer = 0
            Dim decJumBayar As Decimal
            If ViewState("dtTransPel") IsNot Nothing Then
                Dim dtvsDtInv As DataTable = CType(ViewState("dtTransPel"), DataTable)
                Dim drCurrentRow As DataRow = Nothing

                If dtvsDtInv.Rows.Count > 0 Then

                    For i As Integer = 1 To dtvsDtInv.Rows.Count
                        Dim ddl1 As DropDownList = CType(gvTransPel.Rows(rowIndex).Cells(1).FindControl("ddlKW"), DropDownList)
                        Dim ddl2 As DropDownList = CType(gvTransPel.Rows(rowIndex).Cells(2).FindControl("ddlKO"), DropDownList)
                        Dim ddl3 As DropDownList = CType(gvTransPel.Rows(rowIndex).Cells(3).FindControl("ddlPTj"), DropDownList)
                        Dim ddl4 As DropDownList = CType(gvTransPel.Rows(rowIndex).Cells(4).FindControl("ddlKP"), DropDownList)
                        Dim ddl5 As DropDownList = CType(gvTransPel.Rows(rowIndex).Cells(5).FindControl("ddlVot"), DropDownList)
                        Dim ddl6 As DropDownList = CType(gvTransPel.Rows(rowIndex).Cells(5).FindControl("ddlPetunjuk"), DropDownList)
                        Dim box1 As TextBox = CType(gvTransPel.Rows(rowIndex).Cells(6).FindControl("txtPerkara"), TextBox)
                        Dim box2 As TextBox = CType(gvTransPel.Rows(rowIndex).Cells(7).FindControl("txtKuantiti"), TextBox)
                        Dim box3 As TextBox = CType(gvTransPel.Rows(rowIndex).Cells(8).FindControl("txtHarga"), TextBox)
                        Dim box4 As Label = CType(gvTransPel.Rows(rowIndex).Cells(9).FindControl("lblJum"), Label)

                        drCurrentRow = dtvsDtInv.NewRow()
                        dtvsDtInv.Rows(i - 1)("KodKw") = ddl1.SelectedValue
                        dtvsDtInv.Rows(i - 1)("KodKO") = ddl2.SelectedValue
                        dtvsDtInv.Rows(i - 1)("KodPTJ") = ddl3.SelectedValue
                        dtvsDtInv.Rows(i - 1)("KodKP") = ddl4.SelectedValue
                        dtvsDtInv.Rows(i - 1)("KodVot") = ddl5.SelectedValue
                        dtvsDtInv.Rows(i - 1)("Petunjuk") = ddl6.SelectedValue
                        dtvsDtInv.Rows(i - 1)("Perkara") = box1.Text
                        dtvsDtInv.Rows(i - 1)("Kuantiti") = box2.Text
                        dtvsDtInv.Rows(i - 1)("KadarHarga") = box3.Text
                        dtvsDtInv.Rows(i - 1)("Jumlah") = box4.Text

                        Dim angHrgSeunit = CDec(box3.Text)
                        Dim JumAngHrg As Decimal = 0.00
                        If box3.Text IsNot String.Empty Then
                            angHrgSeunit = CDec(box3.Text)
                            JumAngHrg = CDec(box2.Text * angHrgSeunit)
                            box4.Text = JumAngHrg.ToString("#,##0.00")
                        End If

                        JumAngHrg = CDec(box2.Text * angHrgSeunit)
                        box4.Text = JumAngHrg.ToString("#,##0.00")
                        box3.Text = angHrgSeunit.ToString("#,##0.00")

                        Dim decJum As Decimal
                        If box4.Text = "" Then decJum = 0 Else decJum = CDec(box4.Text)
                        decJumBayar += decJum
                        ViewState("JumByr") = FormatNumber(decJumBayar, 2)

                        rowIndex += 1
                    Next

                    dtvsDtInv.Rows.Add(drCurrentRow)
                    ViewState("dtTransPel") = dtvsDtInv
                    gvTransPel.DataSource = dtvsDtInv
                    gvTransPel.DataBind()
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
                    Dim gvRow As GridViewRow = gvTransPel.Rows(i)
                    Dim ddl1 As DropDownList = CType(gvRow.FindControl("ddlKW"), DropDownList)
                    Dim ddl2 As DropDownList = CType(gvRow.FindControl("ddlKO"), DropDownList)
                    Dim ddl3 As DropDownList = CType(gvRow.FindControl("ddlPTj"), DropDownList)
                    Dim ddl4 As DropDownList = CType(gvRow.FindControl("ddlKP"), DropDownList)
                    Dim ddl5 As DropDownList = CType(gvRow.FindControl("ddlVot"), DropDownList)
                    Dim ddl6 As DropDownList = CType(gvRow.FindControl("ddlPetunjuk"), DropDownList)
                    Dim box1 As TextBox = CType(gvRow.FindControl("txtPerkara"), TextBox)
                    Dim box2 As TextBox = CType(gvRow.FindControl("txtKuantiti"), TextBox)
                    Dim box3 As TextBox = CType(gvRow.FindControl("txtHarga"), TextBox)
                    Dim box4 As Label = CType(gvRow.FindControl("lblJum"), Label)

                    ddl1.SelectedValue = dt.Rows(i)("KodKw").ToString()
                    ddl2.SelectedValue = dt.Rows(i)("KodKO").ToString()
                    ddl3.SelectedValue = dt.Rows(i)("KodPTJ").ToString()
                    ddl4.SelectedValue = dt.Rows(i)("KodKP").ToString()
                    ddl5.SelectedValue = dt.Rows(i)("KodVot").ToString()
                    ddl6.SelectedValue = dt.Rows(i)("Petunjuk").ToString()
                    box1.Text = dt.Rows(i)("Perkara").ToString()
                    box2.Text = dt.Rows(i)("Kuantiti").ToString()
                    box3.Text = FormatNumber(dt.Rows(i)("KadarHarga").ToString())
                    box4.Text = FormatNumber(dt.Rows(i)("Jumlah").ToString())

                    rowIndex += 1

                Next
            End If
        Catch ex As Exception

        End Try
    End Sub
    Dim strTotJumlah As String
    Dim decJumlah As Decimal


    Dim decJumlahAsal As Decimal


    Private Sub lbtnCari_Click(sender As Object, e As EventArgs) Handles lbtnCari.Click
        fLoadLst()
    End Sub

    Private Sub ddlFindInv_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFindInv.SelectedIndexChanged
        If ddlFindInv.SelectedIndex = 0 Then
            txtCarian.Enabled = False
            txtCarian.Text = ""
        ElseIf ddlFindInv.SelectedIndex = 1 Then
            txtCarian.Enabled = True
        End If


    End Sub





    Private Sub gvLst_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvLst.PageIndexChanging
        Try
            gvLst.PageIndex = e.NewPageIndex
            If ViewState("dtSenarai") IsNot Nothing Then
                gvLst.DataSource = ViewState("dtSenarai")
                gvLst.DataBind()
            Else
                Dim dt As New DataTable
                ViewState("dtSenarai") = dt

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvLst_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvLst.SelectedIndexChanged
        Try

            fClear()

            Dim row As GridViewRow = gvLst.SelectedRow

            Dim strNoInvCuk As String = CType(row.FindControl("lblNoInvCuk"), Label).Text.Trim
            Dim strTkhBil As String = CType(row.FindControl("lblTkhBil"), Label).Text.Trim
            Dim strTuj As String = CType(row.FindControl("lblTujuan"), Label).Text.Trim
            Dim strNPenerima As String = CType(row.FindControl("lblNPenerima"), Label).Text.Trim
            Dim strJum As String = CType(row.FindControl("lblJum"), Label).Text.Trim
            Dim strStatDok As String = CType(row.FindControl("lblStatDok"), Label).Text.Trim
            Dim strFlag As Boolean = CType(row.FindControl("lblFlagAdj"), Label).Text.Trim
            Dim strButStatus As String = CType(row.FindControl("lblStatus"), Label).Text.Trim

            lblStatus.Text = strButStatus
            txtTkhBil.Text = strTkhBil
            txtNoInv.Text = strNoInvCuk
            txtTujuan.Text = strTuj
            txtNPenerima.Text = strNPenerima

            fBindInvAdj(strNoInvCuk, strFlag, strStatDok)
            fBindInvAdjDt(strNoInvCuk, strFlag, strStatDok)

            If strStatDok = "03" OrElse strStatDok = "06" Then
                lbtnSimpan.Visible = True
                lbtnBatal.Visible = True
            Else
                lbtnSimpan.Visible = False
                lbtnBatal.Visible = False
            End If

            TabContainer.ActiveTabIndex = 0
            divList.Visible = False
            divDt.Visible = True

        Catch ex As Exception

        End Try
    End Sub

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
            dt = CType(ViewState("dtSenarai"), DataTable)

            Dim sortedView As New DataView(dt)
            sortedView.Sort = Convert.ToString(e.SortExpression + " ") & sortingDirection
            Session("SortedView") = sortedView
            gvLst.DataSource = sortedView
            gvLst.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub lbtnKembali_Click(sender As Object, e As EventArgs) Handles lbtnKembali.Click
        fLoadLst()
        divList.Visible = True
        divDt.Visible = False
    End Sub

    Protected Sub lbtnTambah_Click(sender As Object, e As EventArgs)
        Try
            Dim strKW As String = CType(gvTransPel.Rows(gvTransPel.Rows.Count - 1).FindControl("ddlKW"), DropDownList).Text
            Dim strKO As String = CType(gvTransPel.Rows(gvTransPel.Rows.Count - 1).FindControl("ddlKO"), DropDownList).Text
            Dim strPTj As String = CType(gvTransPel.Rows(gvTransPel.Rows.Count - 1).FindControl("ddlPTj"), DropDownList).Text
            Dim strKP As String = CType(gvTransPel.Rows(gvTransPel.Rows.Count - 1).FindControl("ddlKP"), DropDownList).Text
            Dim strVot As String = CType(gvTransPel.Rows(gvTransPel.Rows.Count - 1).FindControl("ddlVot"), DropDownList).Text
            Dim strPerkara As String = CType(gvTransPel.Rows(gvTransPel.Rows.Count - 1).FindControl("txtPerkara"), TextBox).Text
            Dim intKtt As Integer = CInt(CType(gvTransPel.Rows(gvTransPel.Rows.Count - 1).FindControl("txtKuantiti"), TextBox).Text)
            Dim decHarga As Decimal = CDec(CType(gvTransPel.Rows(gvTransPel.Rows.Count - 1).FindControl("txtHarga"), TextBox).Text)
            Dim decJumlah As Decimal = CDec(CType(gvTransPel.Rows(gvTransPel.Rows.Count - 1).FindControl("lblJum"), Label).Text)

            If strKW <> "0" AndAlso strKO <> "0" AndAlso strPTj <> "0" AndAlso strKP <> "0" AndAlso strVot <> "0" AndAlso strPerkara <> "" AndAlso intKtt <> 0 AndAlso decHarga <> 0.00 AndAlso decJumlah <> 0.00 Then
                AddNewRowToGrid()
            End If

        Catch ex As Exception

        End Try
    End Sub



    Protected Sub ddlPetunjuk_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim decHarga As Decimal
        Dim intKtt As Decimal
        Try
            Dim ddlPetunjuk As DropDownList = CType(CType(sender, Control), DropDownList)
            Dim strPetunjuk As String = ddlPetunjuk.SelectedValue

            Dim gvRow As GridViewRow = CType(CType(sender, Control), DropDownList).Parent.Parent
            Dim strKtt As String = CType(gvRow.FindControl("txtKuantiti"), TextBox).Text
            Dim strHarga As String = CType(gvRow.FindControl("txtHarga"), TextBox).Text

            If strHarga = "" Then decHarga = 0 Else decHarga = CDec(strHarga)
            If strKtt = "" Then intKtt = 0 Else intKtt = CInt(strKtt)


            'sKiraBaki(strPetunjuk, intKtt, decHarga)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlFilStat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFilStat.SelectedIndexChanged
        fLoadLst()
    End Sub

    Private Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.Click
        Dim strNoPelSem As String = Trim(txtNoPelSem.Text)

        If String.IsNullOrEmpty(strNoPelSem) Then
            If fSimpan() Then
                fGlobalAlert("Maklumat telah disimpan!", Me.Page, Me.[GetType]())
            Else
                fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
            End If
        Else
            If fKemasKini(strNoPelSem) Then
                fGlobalAlert("Maklumat telah disimpan!", Me.Page, Me.[GetType]())
            Else
                fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
            End If
        End If
    End Sub

    Private Sub lbtnBatal_Click(sender As Object, e As EventArgs) Handles lbtnBatal.Click
        Try
            If txtNoInv.Text <> "" Then
                If fDeleteInv(Trim(txtNoInv.Text.TrimEnd)) = True Then
                    fLoadLst()
                    divList.Visible = True
                    divDt.Visible = False
                Else
                    fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvTransPel_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvTransPel.RowDeleting
        Try
            Dim index As Integer = Convert.ToInt32(e.RowIndex)
            If index > 0 Then
                Dim dtTrans As DataTable = ViewState("dtTransPel") 'fLoadDtTrans()
                dtTrans.Rows(index).Delete()

                If dtTrans.Rows.Count = 0 Then
                    gvTransPel.DataSource = fInitDtTrans()
                    gvTransPel.DataBind()
                Else
                    gvTransPel.DataSource = dtTrans
                    gvTransPel.DataBind()

                    SetPreviousData(dtTrans)
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvTransPel_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvTransPel.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

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
                    ddlKP.SelectedValue = TryCast(e.Row.FindControl("hidKP"), HiddenField).Value
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
                    ddlVot.SelectedValue = TryCast(e.Row.FindControl("hidVot"), HiddenField).Value
                End If

                Dim ddlPetunjuk = DirectCast(e.Row.FindControl("ddlPetunjuk"), DropDownList)
                Dim strSelPetunjuk As String = TryCast(e.Row.FindControl("hidPetunjuk"), HiddenField).Value

                ddlPetunjuk.Items.Add(New ListItem("+", "+"))
                ddlPetunjuk.Items.Add(New ListItem("-", "-"))
                ddlPetunjuk.Items.Insert(0, New ListItem("", ""))
                ddlPetunjuk.SelectedValue = strSelPetunjuk

                Dim strJumlah As String = CType(e.Row.FindControl("lblJum"), Label).Text
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

    Private Sub gvTransAsal_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvTransAsal.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim strJumlah As String = CType(e.Row.FindControl("lblJum"), Label).Text
                If strJumlah = "" Then strJumlah = "0.00"
                decJumlahAsal += CDec(strJumlah)
            End If

            If e.Row.RowType = DataControlRowType.Footer Then
                Dim lblTotJum As Label = CType(e.Row.FindControl("lblTotJum"), Label)
                lblTotJum.Text = FormatNumber(decJumlahAsal, 2)
            End If
        Catch ex As Exception

        End Try
    End Sub

    'Private Sub sKiraBaki(ByVal strPetunjuk As String, ByVal intKtt As Integer, ByVal decHarga As Decimal)

    '    If strPetunjuk = "" OrElse intKtt = 0 OrElse decHarga = 0 Then
    '        Exit Sub
    '    End If

    '    Dim JumBesar As Decimal = 0
    '    Dim decJumBaki, decJumAsal As Decimal
    '    Try
    '        Dim strJumAsal As String = hidJumAsal.Value

    '        If strJumAsal = "" Then decJumAsal = 0 Else decJumAsal = CDec(strJumAsal)
    '        decJumBaki = decJumAsal

    '        Dim decJumlah As Decimal = intKtt * decHarga

    '            If strPetunjuk = "+" Then
    '            decJumBaki = decJumBaki + decJumlah
    '            JumBesar = JumBesar + decJumlah
    '            ElseIf strPetunjuk = "-" Then
    '            decJumBaki = decJumBaki - decJumlah
    '            JumBesar = JumBesar - decJumlah
    '            End If

    '        hidJumBaki.Value = decJumBaki
    '    Catch ex As Exception

    '        End Try
    'End Sub

End Class