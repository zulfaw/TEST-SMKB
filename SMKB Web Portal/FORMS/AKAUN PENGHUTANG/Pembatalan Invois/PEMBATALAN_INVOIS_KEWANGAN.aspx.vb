Imports System.Data.SqlClient

Public Class PEMBATALAN_INVOIS_KEWANGAN
    Inherits System.Web.UI.Page
    '---PEMBOLEHUBAH UTK AUDITLOG---
    Dim sLogBaru As String
    Dim sLogMedan As String
    Dim strNoStaf As String = UserInfo.strSesStaffID
    Dim sLogLevel As String = UserInfo.strUsrTahap
    Dim lsLogUsrIP As String = UserInfo.strUsrPcIP
    Dim lsLogUsrPC As String = UserInfo.strUsrPCName

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

    Private Sub fBindDdlFilStat()
        Dim dbconn As New DBKewConn
        Try
            Dim strSql As String = "select KodStatus, Butiran from AR_StatusDok where KodStatus in (03, 15, 16)"

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
                strStat = "03, 15, 16"
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

    Private Sub fClearGvLst()
        gvLst.DataSource = New List(Of String)
        gvLst.DataBind()
    End Sub

    Private Sub gvLst_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvLst.SelectedIndexChanged
        Try
            Dim row As GridViewRow = gvLst.SelectedRow

            Dim strNoInv As String = CType(gvLst.SelectedRow.FindControl("lblNoInv"), Label).Text.TrimEnd
            Dim intIdBil As Integer = CInt(CType(gvLst.SelectedRow.FindControl("lblIdBil"), Label).Text.TrimEnd)
            Dim strStatDok As String = CType(gvLst.SelectedRow.FindControl("lblKodStatus"), Label).Text.TrimEnd

            sLoadInv(strNoInv)
            sLoadTransAsl(strNoInv)
            'sLoadLampiran(intIdBil)

            If strStatDok = "03" Then
                lbtnBatal.Visible = True
            Else
                lbtnBatal.Visible = False
            End If

            sSetPenyedia()

            TabContainer.ActiveTab = tabTransAsl
            divList.Visible = False
            divDetail.Visible = True

        Catch ex As Exception

        End Try
    End Sub

    Private Sub sLoadInv(ByVal strNoInv As String)
        Try
            Dim dbconn As New DBKewConn()
            Dim dtTkhMohon As Date
            Dim strSqlInv = "Select AR01_NoBil, AR01_TkhMohon,AR01_KODPTJMOHON, (select Butiran  from MK_PTJ where KodPTJ = AR01_Bil.AR01_KodPTJMohon ) as ButPTj, AR01_NOSTAF, AR01_Jenis,AR01_KODBANK, (select NamaBank  from mk_bank where kodbank = AR01_Bil.AR01_KodBank ) as ButBank, AR01_NORUJUKAN, AR01_ALMT1,AR01_ALMT2, AR01_KATEGORI, (SELECT Butiran from MK_KategoriPenerima where kod = AR01_Bil.AR01_Kategori ) as ButKat, AR01_NAMAPENERIMA,kodnegara, (select Butiran from MK_Negara where KodNegara = AR01_Bil.kodnegara) as ButNegara, kodnegeri, (select Butiran from MK_Negeri  where KodNegeri = AR01_Bil.kodnegeri ) as ButNegeri, AR01_IDPENERIMA, AR01_TUJUAN, AR01_BANDAR,AR01_POSKOD, AR01_TkhKonDari, AR01_TkhKonHingga, AR01_NOTEL, AR01_NOFAKS, AR01_TempohKontrak, AR01_JenTemp, AR01_TkhPeringatan1 ,AR01_TkhPeringatan2, AR01_TkhPeringatan3, AR01_TkhLulus,AR01_Emel,AR01_UtkPerhatian, AR01_StatusDok, (select Butiran  from AR_StatusDok where KodStatus = AR01_Bil.AR01_StatusDok ) as ButStatDok, AR01_Kontrak 
from AR01_Bil
where AR01_NoBil = '" & strNoInv & "'"

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

                    Dim strJenTemp As String '= dtMhn.Rows(0)("AR01_JenTemp")
                    If IsDBNull(dtMhn.Rows(0)("AR01_JenTemp")) Then
                        strJenTemp = ""
                    Else
                        Dim strKodJenTemp = Trim(dtMhn.Rows(0)("AR01_JenTemp").ToString.TrimEnd)
                        strJenTemp = fGetJenTemp(strKodJenTemp)
                    End If

                    lblTkhMhn2.Text = strTkhMohon
                    lblNoInv.Text = strNoInv
                    lblKodPtj.Text = strKodPTJ
                    lblNPTJ.Text = dtMhn.Rows(0)("ButPTj")
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
                    lblJenKontrak.Text = IIf(strJenKontrak = True, "Ya", "Tidak")
                    lblTkhMulaKon.Text = TkhKonDr
                    lblTkhTamatKon.Text = TkhKonHingga
                    lblTemKon.Text = TmphKon
                    lblJenTem.Text = strJenTemp
                    lblTkhInv.Text = TkhLulus
                End If
            End Using

        Catch ex As Exception

        End Try
    End Sub

    Private Sub sLoadTransAsl(strNoInv)
        Try
            fClearGvTransAsl()
            Dim strSql = "select KodKw, KodKO, KodPTJ, KodKP, KodVot, MK06_Butiran, MK06_Debit, MK06_Kredit from MK06_Transaksi where MK06_NoDok = '" & strNoInv & "'"

            Dim dbconn As New DBKewConn
            Dim ds As New DataSet
            ds = dbconn.fSelectCommand(strSql)

            Using dt = ds.Tables(0)
                gvTransAsl.DataSource = dt
                gvTransAsl.DataBind()

                'Load Trans Lejar
                Dim dtLej As DataTable = dt
                For i As Integer = 0 To dtLej.Rows.Count - 1
                    Dim strDt = dtLej.Rows(i)("MK06_Debit")
                    Dim strKt = dtLej.Rows(i)("MK06_Kredit")
                    dtLej.Rows(i)("MK06_Debit") = strKt
                    dtLej.Rows(i)("MK06_Kredit") = strDt
                Next

                gvTransLej.DataSource = dtLej
                gvTransLej.DataBind()

            End Using


        Catch ex As Exception

        End Try
    End Sub

    'Private Sub sLoadLampiran(ByVal intIdBil As Integer)
    '    Dim ds As New DataSet
    '    Dim dbconn As New DBKewConn()
    '    Try
    '        sClearGvLamp()
    '        Dim strSql As String = "select AR11_ID, AR01_IdBil, AR11_Path, AR11_NamaDok  from AR11_Lampiran where AR01_IdBil= @AR01_IdBil AND AR11_Status  = @Status"

    '        Dim paramSql() As SqlParameter =
    '            {
    '            New SqlParameter("@AR01_IdBil", intIdBil),
    '             New SqlParameter("@Status", 1)
    '            }

    '        ds = dbconn.fSelectCommand(strSql, paramSql)
    '        If ds.Tables.Count > 0 Then
    '            gvLamp.DataSource = ds
    '            gvLamp.DataBind()

    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub sSetPenyedia()
        lblNoStaf.Text = Session("ssusrID")
        lblJwtn.Text = Session("ssusrPost")
        lblKodPTjPenyedia.Text = Session("ssusrKodPTj")
        lblNamaPTjPenyedia.Text = Session("ssusrPTj")
        lblNama.Text = Session("ssusrName")

        Dim strTkhToday As String = Now.ToString("dd/MM/yyyy")
        lblTarikh.Text = strTkhToday
    End Sub

    Private Sub sClearGvLamp()
        gvLst.DataSource = New List(Of String)
        gvLst.DataBind()
    End Sub

    Private Sub fClearGvTransAsl()
        gvTransAsl.DataSource = New List(Of String)
        gvTransAsl.DataBind()
    End Sub

    Private Sub lbtnKembali_Click(sender As Object, e As EventArgs) Handles lbtnKembali.Click
        divList.Visible = True
        divDetail.Visible = False
        fClear()
        fLoadLst()
    End Sub

    Private Sub fClear()
        Try

            gvLst.DataSource = New List(Of String)
            gvLst.DataBind()

        Catch ex As Exception

        End Try
    End Sub

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

    Private Sub lbtnBatal_Click(sender As Object, e As EventArgs) Handles lbtnBatal.Click

        If fBatalInv() Then
            fGlobalAlert("Maklumat telah disimpan!", Me.Page, Me.[GetType]())
            lbtnBatal.Visible = False
        Else
            fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
        End If

    End Sub


    Private Function fBatalInv() As Boolean
        Dim strNoJurnSem, strSql As String
        Dim dbconn As New DBKewConn
        Dim blnSuccess As Boolean = True
        Dim strTkhToday As String = Now.ToString("yyyy-MM-dd")
        Dim dtTkhToday As DateTime = CDate(strTkhToday)
        Dim paramSql() As SqlParameter
        Try

            Dim strNoInv = Trim(lblNoInv.Text.TrimEnd)
            Dim strTujuan = Trim(lblTujuan.Text.TrimEnd)

            Dim decJumlah As Decimal
            For Each gvRow As GridViewRow In gvTransLej.Rows
                Dim decAmt As Decimal = CDec(TryCast(gvRow.FindControl("lblDebit"), Label).Text)
                decJumlah += decAmt
            Next

            strNoJurnSem = fGetNoJurSem("JS") ' JanaJurnal("GL", "JS")
            Dim strKodDok = "GL-JE"

            strSql = "insert into GL01_JrnlTran (GL01_NoJrnlSem, GL01_NoJrnl, GL01_TkhTran, KodDok, GL01_Rujukan, GL01_IdPenerima, GL01_Perihal, GL01_Jumlah, GL01_JenisTran, GL01_StatusDok, GL01_Status, GL01_JenisPljr, GL01_JenisTranPel, GL01_StatusLejar, GL01_CetakJSem, GL01_CetakJ, GL01_StatusBatalCek, GL01_NoDok) Values (@GL01_NoJrnlSem, @GL01_NoJrnl, @GL01_TkhTran, @KodDok, @GL01_Rujukan, @GL01_IdPenerima, @GL01_Perihal, @GL01_Jumlah, @GL01_JenisTran, @GL01_StatusDok, @GL01_Status, @GL01_JenisPljr, @GL01_JenisTranPel, @GL01_StatusLejar, @GL01_CetakJSem, @GL01_CetakJ, @GL01_StatusBatalCek, @GL01_NoDok)"

            paramSql =
                {
                New SqlParameter("@GL01_NoJrnlSem", strNoJurnSem),
                New SqlParameter("@GL01_NoJrnl", strNoJurnSem),
                New SqlParameter("@GL01_TkhTran", dtTkhToday),
                New SqlParameter("@KodDok", strKodDok),
                New SqlParameter("@GL01_Rujukan", strNoInv),
                New SqlParameter("@GL01_IdPenerima", "-"),
                New SqlParameter("@GL01_Perihal", strTujuan),
                New SqlParameter("@GL01_Jumlah", decJumlah),
                New SqlParameter("@GL01_JenisTran", "L"),
                New SqlParameter("@GL01_StatusDok", "01"),
                New SqlParameter("@GL01_Status", "A"),
                New SqlParameter("@GL01_JenisPljr", "-"),
                New SqlParameter("@GL01_JenisTranPel", "-"),
                New SqlParameter("@GL01_StatusLejar", 1),
                New SqlParameter("@GL01_CetakJSem", 1),
                New SqlParameter("@GL01_CetakJ", False),
                New SqlParameter("@GL01_StatusBatalCek", False),
                New SqlParameter("@GL01_NoDok", "-")
                }

            dbconn.sConnBeginTrans()
            If Not dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                blnSuccess = False
                Exit Try
            Else
                'AUDIT LOG
                sLogMedan = "GL01_NoJrnlSem|GL01_NoJrnl|GL01_TkhTran|KodDok|GL01_Rujukan|GL01_IdPenerima|GL01_Perihal|GL01_Jumlah| GL01_JenisTran|GL01_StatusDok|GL01_Status|GL01_JenisPljr|GL01_JenisTranPel|GL01_StatusLejar|GL01_CetakJSem| GL01_CetakJ|GL01_StatusBatalCek|GL01_NoDok"
                sLogBaru = strNoJurnSem & "|" & strNoJurnSem & "|" & strKodDok & "|" & strNoInv & "|-|" & strTujuan & "|" & decJumlah & "|L|01|A|-|-|1|1|False|False|-"

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
                    New SqlParameter("@InfoTable", "GL01_JrnlTran"),
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
            For Each gvDtRow As GridViewRow In gvTransLej.Rows
                intBil += 1
                Dim strKW = TryCast(gvDtRow.FindControl("lblKW"), Label).Text
                Dim strKO = TryCast(gvDtRow.FindControl("lblKO"), Label).Text
                Dim strPTj = TryCast(gvDtRow.FindControl("lblPTj"), Label).Text
                Dim strKP = TryCast(gvDtRow.FindControl("lblKP"), Label).Text
                Dim strVot = TryCast(gvDtRow.FindControl("lblVot"), Label).Text
                Dim strButiran = TryCast(gvDtRow.FindControl("lblButiran"), Label).Text
                Dim decDt As Decimal = CDec(TryCast(gvDtRow.FindControl("lblDebit"), Label).Text)
                Dim decKt As Decimal = CDec(TryCast(gvDtRow.FindControl("lblKredit"), Label).Text)

                strSql = "insert into GL01_JrnlTranDt (GL01_NoJrnlSem, GL01_NoJrnl, GL01_Rujukan, GL01_TkhTran, GL01_Bil, KodKw, KodKO, KodPtj, KodKP, KodVot, GL01_Butiran, GL01_Debit, GL01_Kredit, KodPenyesuaian, GL01_PenyId, GL01_TkhSesuai) Values (@GL01_NoJrnlSem, @GL01_NoJrnl, @GL01_Rujukan, @GL01_TkhTran, @GL01_Bil, @KodKw, @KodKO, @KodPtj, @KodKP, @KodVot, @GL01_Butiran, @GL01_Debit, @GL01_Kredit, @KodPenyesuaian, @GL01_PenyId, @GL01_TkhSesuai)"

                paramSql =
                    {
                    New SqlParameter("@GL01_NoJrnlSem", strNoJurnSem),
                     New SqlParameter("@GL01_NoJrnl", strNoJurnSem),
                     New SqlParameter("@GL01_Rujukan", strNoInv),
                     New SqlParameter("@GL01_TkhTran", dtTkhToday),
                     New SqlParameter("@GL01_Bil", intBil),
                     New SqlParameter("@KodKw", strKW),
                     New SqlParameter("@KodKO", strKO),
                     New SqlParameter("@KodPTJ", strPTj),
                     New SqlParameter("@KodKP", strKP),
                     New SqlParameter("@KodVot", strVot),
                     New SqlParameter("@GL01_Butiran", strButiran),
                     New SqlParameter("@GL01_Debit", decDt),
                     New SqlParameter("@GL01_Kredit", decKt),
                     New SqlParameter("@KodPenyesuaian", "U"),
                     New SqlParameter("@GL01_PenyId", "-"),
                     New SqlParameter("@GL01_TkhSesuai", DBNull.Value)
                    }

                If Not dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                    blnSuccess = False
                    Exit Try
                Else
                    'AUDIT LOG
                    sLogMedan = "GL01_NoJrnlSem|GL01_NoJrnl|GL01_Rujukan|GL01_TkhTran|GL01_Bil|KodKw|KodKO|KodPtj|KodKP|KodVot| GL01_Butiran|GL01_Debit|GL01_Kredit|KodPenyesuaian|GL01_PenyId|GL01_TkhSesuai"
                    sLogBaru = strNoJurnSem & "|" & strNoJurnSem & "|" & strNoInv & "|" & dtTkhToday & "|" & intBil & "|" & strKW & "|" & strKO & "|" & strPTj & "|" & strKP & "|" & strVot & "|" & strButiran & "|" & decDt & "|" & decKt & "|U|-|Null"

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
                        New SqlParameter("@InfoTable", "GL01_JrnlTranDt"),
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

            strSql = "INSERT INTO GL03_StatusDok (GL01_NoJrnlSem, GL01_NoJrnl, GL03_Kod, GL03_NoStaf, GL03_Tkh, GL03_Ulasan) " &
                       " VALUES (@NoJrnalSem, @NoJrnal, @GLKod, @NoStaf,@Tarikh, @Ulasan)"

            paramSql =
            {
             New SqlParameter("@NoJrnalSem", strNoJurnSem),
             New SqlParameter("@NoJrnal", strNoJurnSem),
             New SqlParameter("@GLKod", "01"),
             New SqlParameter("@NoStaf", Session("ssusrID")),
             New SqlParameter("@Tarikh", dtTkhToday),
             New SqlParameter("@Ulasan", "")
                 }

            If Not dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                blnSuccess = False
                Exit Try
            End If


            'UPDATE TABLE

            strSql = "UPDATE AR01_Bil SET AR01_StatusDok = @StatDok WHERE AR01_NoBil = @AR01_NoBil"
            paramSql =
            {
             New SqlParameter("@StatDok", 14),
             New SqlParameter("@AR01_NoBil", strNoInv)
                 }

            If Not dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                blnSuccess = False
                Exit Try
            Else
                'AUDIT LOG
                sLogMedan = "AR01_NoBil|AR01_StatusDok"
                sLogBaru = strNoInv & "|14"

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

        Catch ex As Exception
            blnSuccess = False
        End Try

        If blnSuccess = True Then
            dbconn.sConnCommitTrans()
            txtNoJurnSem.Text = strNoJurnSem
            Return True

        ElseIf blnSuccess = False Then
            dbconn.sConnRollbackTrans()
            Return False
        End If

    End Function

End Class