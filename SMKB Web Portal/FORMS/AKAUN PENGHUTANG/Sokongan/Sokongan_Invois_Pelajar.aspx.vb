Imports System.Data.SqlClient

Public Class Sokongan_Invois_Pelajar
    Inherits System.Web.UI.Page

    Dim sLogBaru As String
    Dim sLogMedan As String
    Dim strNoStaf As String = UserInfo.strSesStaffID
    Dim sLogLevel As String = UserInfo.strUsrTahap
    Dim lsLogUsrIP As String = UserInfo.strUsrPcIP
    Dim lsLogUsrPC As String = UserInfo.strUsrPCName

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
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

            Dim strTkhToday As String = Now.ToString("dd/MM/yyyy")

            fBindDdlFilStat()
            fLoadLst()

        End If
    End Sub

    Private Sub gvLst_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvLst.SelectedIndexChanged
        Try
            Dim row As GridViewRow = gvLst.SelectedRow

            Dim intIdBil As String = CInt(CType(gvLst.SelectedRow.FindControl("lblIdBil"), Label).Text.TrimEnd)
            Dim strNoInvSem As String = CType(gvLst.SelectedRow.FindControl("lblNoInvSem"), Label).Text.TrimEnd
            Dim strStatDok As String = CType(gvLst.SelectedRow.FindControl("lblStatDok"), Label).Text.TrimEnd


            sLoadInv(strNoInvSem)
            sLoadTrans(strNoInvSem)
            sloadMohon(strNoInvSem, strStatDok)
            sLoadInvLamp(intIdBil)

            If strStatDok = "01" Then
                lbtnSokong.Visible = True
                lbtnXSokong.Visible = True
                sSetSokong()
            Else
                lbtnSokong.Visible = False
                lbtnXSokong.Visible = False
                sLoadSokong(strNoInvSem, strStatDok)
            End If

            TabContainer1.ActiveTabIndex = 0

            divList.Visible = False
            divDetail.Visible = True



        Catch ex As Exception

        End Try

    End Sub

    Private Sub sLoadInvLamp(ByVal intIdBil As Integer)
        Try
            Dim strSql = "select AR02_IdLamp, AR02_NamaPenerima, AR02_NoKP, AR02_NoMatrik, AR02_Kursus, AR02_Amaun, AR02_Butiran from AR02_Lampiran where AR01_IdBil = " & intIdBil
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

            strSql = "select a.AR01_IdBil, a.AR01_NoBilSem,AR01_NoBil, Convert(varchar, a.AR01_TkhMohon, 103) As AR01_TkhMohon, a.AR01_Tujuan, a.AR01_NamaPenerima, AR01_Kategori, (select Butiran  from MK_KategoriPenerima where Kod = a.AR01_Kategori ) as ButKat, a.ar01_Jumlah, AR01_TkhMohon  as Tarikh_Bil, ar01_statusdok, (Select B.Butiran  from AR_StatusDok B where B.KodStatus = A.AR01_StatusDok) as ButStatDok
from AR01_Bil a 
where ar01_statusdok in (" & strStat & ") and ar01_kodptjmohon='" & Trim(Session("ssusrKodPTj")).TrimEnd & "' and ar01_jenis='02' " & strFilter

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

    Private Sub fClearGvLst()
        gvLst.DataSource = New List(Of String)
        gvLst.DataBind()
    End Sub

    Private Sub sLoadInv(ByVal strNoInv As String)

        Dim dbconn As New DBKewConn
        Try
            Dim strSqlInv = $"Select  AR01_TkhMohon, AR01_KODPTJMOHON, (select mk_ptj.Butiran from mk_ptj where MK_PTJ.KodPTJ = AR01_Bil.AR01_KodPTJMohon) as ButPTj, AR01_NOSTAF, AR01_Jenis, AR01_KODBANK, AR01_NORUJUKAN, AR01_ALMT1, AR01_ALMT2,
AR01_KATEGORI, AR01_IDPENERIMA, AR01_NAMAPENERIMA, kodnegeri, (select MK_Negeri.Butiran from MK_Negeri where MK_Negeri.KodNegeri = AR01_Bil.KodNegeri) as ButNegeri, kodnegara,(select MK_Negara.Butiran from MK_Negara where MK_Negara.KodNegara = AR01_Bil.KodNegara ) as ButNegara, AR01_TUJUAN, AR01_BANDAR,AR01_POSKOD, AR01_NOTEL, 
AR01_NOFAKS, AR01_TempohKontrak, AR01_Emel,AR01_UtkPerhatian, AR01_StatusDok, (select Butiran  from AR_StatusDok where KodStatus = AR01_Bil.AR01_StatusDok) as ButStatDok, AR01_JenisPljr, (select MK_KategoriPenerima .Butiran  from MK_KategoriPenerima where MK_KategoriPenerima.Kod = AR01_Bil.AR01_JenisPljr) as ButJenisPljr from AR01_Bil where AR01_NoBilSem= '{strNoInv}';"

            'Load Permohonan Invois
            Dim ds = dbconn.fSelectCommand(strSqlInv)

            Using dtMhn = ds.Tables(0)
                If dtMhn.Rows.Count > 0 Then
                    Dim TkhMhn As Date = dtMhn.Rows(0)("AR01_TkhMohon")
                    Dim KodPTJ As String = dtMhn.Rows(0)("AR01_KODPTJMOHON")
                    Dim strButPTj As String = dtMhn.Rows(0)("ButPTj")
                    Dim NoPmhn As String = dtMhn.Rows(0)("AR01_NOSTAF")
                    Dim katInv As String = dtMhn.Rows(0)("AR01_Jenis")
                    Dim Bank As String = dtMhn.Rows(0)("AR01_KODBANK")
                    Dim NoRuj As String = dtMhn.Rows(0)("AR01_NORUJUKAN")
                    Dim Almt1 As String = dtMhn.Rows(0)("AR01_ALMT1")
                    Dim Almt2 As String = dtMhn.Rows(0)("AR01_ALMT2")
                    Dim Kat As String = dtMhn.Rows(0)("AR01_KATEGORI")
                    Dim NPenerima As String = dtMhn.Rows(0)("AR01_NAMAPENERIMA")
                    Dim IDPenerima As String = dtMhn.Rows(0)("AR01_IDPENERIMA")
                    Dim KodNegeri As String = dtMhn.Rows(0)("kodnegeri")
                    Dim strButNegeri As String = dtMhn.Rows(0)("ButNegeri")

                    Dim KodNegara As String = dtMhn.Rows(0)("kodnegara")
                    Dim strButNegara As String = dtMhn.Rows(0)("ButNegara")

                    Dim Tujuan As String = dtMhn.Rows(0)("AR01_TUJUAN")
                    Dim Bandar As String = dtMhn.Rows(0)("AR01_BANDAR")
                    Dim Poskod As String = dtMhn.Rows(0)("AR01_POSKOD")
                    Dim NoTel As String = dtMhn.Rows(0)("AR01_NOTEL")
                    Dim NoFax As String = dtMhn.Rows(0)("AR01_NOFAKS")
                    Dim Emel As String = dtMhn.Rows(0)("AR01_Emel")
                    Dim Perhatian As String = dtMhn.Rows(0)("AR01_UtkPerhatian")
                    Dim strStatDok As String = dtMhn.Rows(0)("AR01_StatusDok")
                    Dim strJenPel As String = dtMhn.Rows(0)("AR01_JenisPljr")
                    Dim strButJenPel As String = dtMhn.Rows(0)("ButJenisPljr")

                    lblStat.Text = dtMhn.Rows(0)("ButStatDok")
                    lblTkhMohon.Text = TkhMhn
                    lblNoInvSem.Text = strNoInv

                    lblNmPen.Text = NPenerima
                    lblIdPen.Text = IDPenerima
                    lblTujuan.Text = Tujuan
                    lblNoRuj.Text = IIf(NoRuj = "", "-", NoRuj)
                    lblAlmt1.Text = Almt1
                    lblAlmt2.Text = Almt2
                    lblBandar.Text = IIf(Bandar = "", "-", Bandar)
                    lblPoskod.Text = Poskod
                    lblTel.Text = NoTel
                    lblFax.Text = NoFax
                    lblEmel.Text = Emel
                    lblUP.Text = IIf(Perhatian = "", "-", Perhatian)
                    lblBank.Text = Bank
                    lblNegeri.Text = KodNegeri + " - " + strButNegeri
                    lblNegara.Text = KodNegara + " - " + strButNegara
                    lblKatPel.Text = strJenPel + " - " + strButJenPel
                    lblKodPTj.Text = KodPTJ
                    lblNmPTj.Text = strButPTj
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

    Private Sub sloadMohon(ByVal strNoInvSem As String, ByVal strKodStat As String)
        Try
            Dim dbconn As New DBKewConn
            Dim strNoStaf, strUlasan As String

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

    Private Sub fClearGvTrans()
        gvTrans.DataSource = New List(Of String)
        gvTrans.DataBind()
    End Sub

    Private Sub lbtnKembali_Click(sender As Object, e As EventArgs) Handles lbtnKembali.Click
        fClearInfo()
        fLoadLst()
        divList.Visible = True
        divDetail.Visible = False
    End Sub

    Private Sub fClearInfo()
        lblNoInvSem.Text = ""
        lblTkhMohon.Text = ""
        lblKodPTj.Text = ""
        lblNmPTj.Text = ""
        lblStat.Text = ""
        lblBank.Text = ""
        lblKatPel.Text = ""
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
        lblTel.Text = ""
        lblFax.Text = ""
        lblEmel.Text = ""

        lblNoPmhn.Text = ""
        lblNmPemohon.Text = ""
        lblJawatan.Text = ""
        lblKodPTjPmhn.Text = ""
        lblNmPTjPmhn.Text = ""

        fClearGvTrans()
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



    Private Function fSokong()
        Dim blnSuccess As Boolean = True
        Dim strNoStaf As String = Trim(lblNoStafSokong.Text.TrimEnd)
        Dim dbconn As New DBKewConn()

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

        Catch ex As Exception
            blnSuccess = False
        End Try

        If blnSuccess Then
            dbconn.sConnCommitTrans()
            Return True
        Else
            dbconn.sConnRollbackTrans()
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
                sLogMedan = "AR01_STATUSDOK|ar01_nobilsem"
                sLogBaru = strStatDok & "|" & strNoBilSem

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

    Private Sub lbtnSokong_Click(sender As Object, e As EventArgs) Handles lbtnSokong.Click
        If fSokong() Then
            fGlobalAlert("Maklumat invois telah disokong!", Me.Page, Me.[GetType]())
            divList.Visible = True
            divDetail.Visible = False
            fLoadLst()
        Else
            fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
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

    Private Sub ddlFilStat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFilStat.SelectedIndexChanged
        fLoadLst()
    End Sub

    Dim decJumLamp As Decimal
    Private Sub gvLamp_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvLamp.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim strJumlah As String = CType(e.Row.FindControl("lblJumlah"), Label).Text
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

    Dim decJumDt As Decimal
    Private Sub gvTrans_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvTrans.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim strJumlah As String = CType(e.Row.FindControl("lblJumlah"), Label).Text
                If strJumlah = "" Then strJumlah = "0.00"
                decJumDt += CDec(strJumlah)
            End If

            If e.Row.RowType = DataControlRowType.Footer Then
                Dim lblTotJum As Label = CType(e.Row.FindControl("lblTotJum"), Label)
                lblTotJum.Text = FormatNumber(decJumDt, 2)
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class