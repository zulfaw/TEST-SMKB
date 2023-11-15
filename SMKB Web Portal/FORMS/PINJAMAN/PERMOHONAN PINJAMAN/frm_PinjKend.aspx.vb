Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Collections
Imports System.Web.HttpFileCollection

Public Class frm_PinjKend
    Inherits System.Web.UI.Page

    Private dbconn As New DBKewConn
    Public KodStatus As Int16 = 0
    Public KodLulus As Int16 = 0
    Private Sub get_MakPemohon()
        'lblTahun.Text = Now.Year
        'lblTkhMhn.Text = Date.Today.ToString("dd/MM/yyyy")
        'lblKodPTJ.Text = Session("ssusrKodPTj")
        'lblNamaPTJ.Text = Session("ssusrPTj")
        lblNoPmhn.Text = Session("ssusrID")
        lblNamaPemohon.Text = Session("ssusrName")
        lblJawSkrang.Text = Session("ssusrPost")

        Dim dbconnStaf As New DBSMConn
        Dim strSql As String
        Dim ds As DataSet
        Dim dtMohon As DataTable

        strSql = $"SELECT MS01_NoStaf, MS01_Nama, MS01_Voip as MS01_TelPejabat, MS01_NoLesen, MS01_KelasLesen,
                                MS01_KpB, MS01_TkhLahir
                                FROM ms01_peribadi
                                WHERE 1 = 1
                                AND ms01_nostaf ='" & Session("ssusrID") & "'"

        ds = dbconnStaf.fselectCommand(strSql)
        dtMohon = ds.Tables(0)
        If dtMohon.Rows.Count > 0 Then
            lblNoTelPejabat.Text = dtMohon.Rows(0)("MS01_TelPejabat").ToString
            lblNoLesen.Text = dtMohon.Rows(0)("MS01_NoLesen").ToString
            lblNoKP.Text = dtMohon.Rows(0)("MS01_KpB").ToString

            Dim a, b, c
            'Dim thisDate As Date
            'thisDate = Today

            a = DateDiff("m", CDate(dtMohon.Rows(0)("MS01_TkhLahir").ToString), Today)
            If InStr(CStr(a / 12), ".") = 0 Then
                b = CStr(a / 12)
            Else
                b = Left(CStr(a / 12), InStr(CStr(a / 12), ".") - 1)
            End If
            c = a - (b * 12)
            lblUmur.Text = b & " TAHUN DAN " & c & " BULAN"

            lblTkhLahir.Text = dtMohon.Rows(0)("MS01_TkhLahir").ToString

        End If

        '--------------------------------------------------
        strSql = $"select TarafKhidmat
        from MS_TarafKhidmat where KodTarafKhidmat in
        (select ms02_taraf from MS02_Perjawatan where ms01_nostaf ='" & Session("ssusrID") & "')"

        ds = dbconnStaf.fselectCommand(strSql)
        dtMohon = ds.Tables(0)
        If dtMohon.Rows.Count > 0 Then
            lbltarafPerkhidmatan.Text = dtMohon.Rows(0)("TarafKhidmat").ToString

        End If
        '--------------------------------------------------

        '--------------------------------------------------
        strSql = $"Select JawGiliran
                From MS_Jawatan
                Where KodJawatan
		        IN (Select MS19_KodJwt FROM MS19_JawGiliran WHERE MS01_NoStaf = '" & Session("ssusrID") & "')"

        ds = dbconnStaf.fselectCommand(strSql)
        dtMohon = ds.Tables(0)
        If dtMohon.Rows.Count > 0 Then
            lbljawGiliranNama.Text = dtMohon.Rows(0)("JawGiliran").ToString

        End If
        '--------------------------------------------------

        '--------------------------------------------------
        strSql = $"SELECT jawGiliran
        FROM MS_Jawatan
        WHERE KodJawatan
        IN (SELECT MS02_jawsandang FROM MS02_Perjawatan WHERE MS01_NoStaf ='" & Session("ssusrID") & "')"

        ds = dbconnStaf.fselectCommand(strSql)
        dtMohon = ds.Tables(0)
        If dtMohon.Rows.Count > 0 Then
            lblJawSkrang.Text = dtMohon.Rows(0)("JawGiliran").ToString

        End If
        '--------------------------------------------------

        '--------------------------------------------------
        strSql = $"Select FORMAT (MS02_TkhLantikKUTKM, 'dd/MM/yyyy ')  as MS02_TkhLantikKUTKM
            From MS02_Perjawatan
            Where ms01_nostaf ='" & Session("ssusrID") & "'"

        ds = dbconnStaf.fselectCommand(strSql)
            dtMohon = ds.Tables(0)
        If dtMohon.Rows.Count > 0 Then
            lblTkhLantik.Text = dtMohon.Rows(0)("MS02_TkhLantikKUTKM").ToString

        End If
        '--------------------------------------------------

        '--------------------------------------------------
        strSql = $"Select MS02_JumlahGajiS
			from MS02_Perjawatan
			where ms01_nostaf ='" & Session("ssusrID") & "'"

        ds = dbconnStaf.fselectCommand(strSql)
        dtMohon = ds.Tables(0)
        If dtMohon.Rows.Count > 0 Then
            lblJumGaji.Text = dtMohon.Rows(0)("MS02_JumlahGajiS").ToString

        End If
        '--------------------------------------------------

        strSql = $"select Pejabat
                from MS_Pejabat where KodPejabat in
                (select MS08_Pejabat from MS08_Penempatan where ms01_nostaf ='" & Session("ssusrID") & "' and ms08_staterkini = 1)"


        ds = dbconnStaf.fselectCommand(strSql)
        dtMohon = ds.Tables(0)
        If dtMohon.Rows.Count > 0 Then
            lblNamaPejabat.Text = dtMohon.Rows(0)("Pejabat").ToString

        End If

        strSql = $"SELECT a.Nama, a.Almt1, a.Almt2, a.Bandar, a.Poskod, b.Butiran AS NamaNegeri, c.Butiran AS NamaNegara
             FROM MK_Korporat As a, MK_Negeri As b, MK_Negara As c
             WHERE 1 = 1
             And b.KodNegeri = a.KodNegeri
            AND c.KodNegara = a.KodNegara"


        ds = dbconn.fSelectCommand(strSql)
        dtMohon = ds.Tables(0)
        If dtMohon.Rows.Count > 0 Then
            lblAlamatPejabat.Text = "&nbsp;&nbsp" & dtMohon.Rows(0)("Nama").ToString & "," & "<br>&nbsp;&nbsp" & dtMohon.Rows(0)("Almt1").ToString & "<br>&nbsp;&nbsp" &
                dtMohon.Rows(0)("Nama").ToString & "<br>&nbsp;&nbsp" & dtMohon.Rows(0)("Poskod").ToString & " " & dtMohon.Rows(0)("Bandar").ToString & " " & dtMohon.Rows(0)("NamaNegeri").ToString & ", <br>&nbsp;&nbsp;" & dtMohon.Rows(0)("NamaNegara").ToString

        End If

        '--------------------------------------------------
        strSql = $"Select MS19_TkhMula
			from MS19_JawGiliran
			where ms01_nostaf ='" & Session("ssusrID") & "'"

            ds = dbconnStaf.fselectCommand(strSql)
            dtMohon = ds.Tables(0)
            If dtMohon.Rows.Count > 0 Then
            tkhMula.Text = dtMohon.Rows(0)("MS19_TkhMula").ToString

        End If
        '--------------------------------------------------
        '--------------------------------------------------
        strSql = $"Select MS19_TkhTamat
			from MS19_JawGiliran
			where ms01_nostaf ='" & Session("ssusrID") & "'"

        ds = dbconnStaf.fselectCommand(strSql)
        dtMohon = ds.Tables(0)
        If dtMohon.Rows.Count > 0 Then
            lbltkhTamat.Text = dtMohon.Rows(0)("MS19_TkhTamat").ToString

        End If
        '--------------------------------------------------

        '--------------------------------------------------
        strSql = $"Select MS02_TkhSah
			from MS02_Perjawatan
			where ms01_nostaf ='" & Session("ssusrID") & "'"

        ds = dbconnStaf.fselectCommand(strSql)
        dtMohon = ds.Tables(0)
        If dtMohon.Rows.Count > 0 Then
            lblTkhSah.Text = dtMohon.Rows(0)("MS02_TkhSah").ToString

        End If
        '--------------------------------------------------

        strSql = $"Select MS02_GredGajiS
			from MS02_Perjawatan
			where ms01_nostaf ='" & Session("ssusrID") & "'"

        ds = dbconnStaf.fselectCommand(strSql)
        dtMohon = ds.Tables(0)
        If dtMohon.Rows.Count > 0 Then
            lblGredGaji.Text = dtMohon.Rows(0)("MS02_GredGajiS").ToString

        End If

        strSql = "SELECT MS11_Jumlah, MS10_NamaJawPangku FROM VK_AdvClmPangku WHERE MS01_NoStaf = '" & Session("ssusrID") & "'"

        ds = dbconnStaf.fselectCommand(strSql)
        dtMohon = ds.Tables(0)
        If dtMohon.Rows.Count > 0 Then
            lblElaunMangku.Text = dtMohon.Rows(0)("MS11_Jumlah").ToString
            lblJawMangku.Text = dtMohon.Rows(0)("MS10_NamaJawPangku").ToString

        End If






        'lblJnsTerimaan.Text = "TERIMAAN KEWANGAN"

        'fGetZonning(Session("ssusrID"), ViewState("zon"), lblZon.Text)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            If Not Session("LoggedIn") Then
                Response.Redirect("~/Account/Logout.aspx")
                Exit Sub
            End If

            If Not Session("SmkbMessage") = String.Empty Then
                lblWarning.Visible = True
                lblErr.Text = Session("SmkbMessage")
            End If

            'hdNoIDSem.Value = Session("ssusrID")
            ViewState("vsDoneEdit") = False
            ViewState("kodsubmenu") = Request.QueryString("KodSubMenu")
            get_MakPemohon()
            'fBindDdlNegara()
            'fBindDdlNegeri()
            'fBindDdlKodBank()
            'fLoadMakSya()
            'fLoadGVPenyata()
            'fLoadGVProfilSyarikat()
            'fLoadPenyata()

            'If Session("GrantAccess") = 0 Or Session("GrantAccess") = 6 Then
            '    ' Dim dbconnVen As New DBConn(DBVen)
            '    Dim strDaftar = $"SELECT [NoDaftar],[NamaSyarikat],[Email] FROM [ROC_Login] WHERE [UserID] = '{hdNoIDSem.Value}'"
            '    Using dt = dbconnVen.fselectCommandDt(strDaftar)
            '        txtNamaSya.Text = dt.Rows(0)("NamaSyarikat")
            '        txtNoSya.Text = dt.Rows(0)("NoDaftar")
            '        lblEmailSya.Text = dt.Rows(0)("Email")

            '        'New registration
            '        Session("GrantAccess") = 6
            '    End Using
            'Else


            'If Session("GrantAccess") = 2 Then
            '    fGlobalAlert("Syarikat tuan telah disenarai hitam oleh pihak UTeM. Sila hubungi pihak UTeM.", Page, Me.GetType, "~/../../../../Default.aspx")
            '    Exit Sub
            'End If

            'If Session("GrantAccess") = 3 Then
            '    fGlobalAlert("Syarikat tuan telah digantung oleh pihak UTeM. Sila hubungi pihak UTeM.", Page, Me.GetType, "~/../../../../Default.aspx")
            '    Exit Sub
            'End If

            'If Session("GrantAccess") = 4 Then
            '    fGlobalAlert("Syarikat tuan sedang dalam proses kelulusan pendaftaran oleh pihak UTeM. Sila hubungi pihak UTeM.", Page, Me.GetType, "~/../../../../Default.aspx")
            '    Exit Sub
            'End If

            'If Session("GrantAccess") = 5 Then
            '    fGlobalAlert("Syarikat tuan sedang dalam proses kelulusan kemaskini pendaftaran oleh pihak UTeM. Sila hubungi pihak UTeM.", Page, Me.GetType, "~/../../../../Default.aspx")
            '    Exit Sub
            'End If

            ViewState("kodsubmenu") = Request.QueryString("KodSubMenu")
            ' fLoadDetail()

            'If Session("GrantAccess") = 1 Then
            '    If rbKategoriSyarikat.SelectedItem Is Nothing Then
            '        lbtnNext.Visible = False
            '    End If
            'End If
        End If
        'lblMakSya.Text = Session("ssusrNoSya") + " /  " + Session("ssusrNamaSya")
        KodStatus = 1
        'End If
    End Sub

    Private Sub fLoadUlasan()

        'Dim strSql As String = $"SELECT TOP (1) [ROC10_NoID1],[ROC10_NoID2],[KodDok],[ROC10_NoPerson],[ROC10_Tkh],[ROC10_Ulasan]
        '                          FROM [DbKewanganV1].[dbo].[ROC10_StatusDok]
        '                          WHERE [ROC10_NoID1] = '{hdNoIDSem.Value}'
        '                          ORDER BY ROC10_Tkh DESC"
        'Using dt = dbconn.fSelectCommandDt(strSql)

        '    If dt.Rows.Count > 0 Then
        '        ltrUlasan.Text = dt.Rows(0)("ROC10_Ulasan").ToString
        '    End If
        'End Using
    End Sub

    Private Sub fLoadPenyata()
        '        Dim str2 = $"SELECT ROC01_IDBank,ROC01_IDSem,ROC01_IDSya,ROC01_KodBank,ROC01_NoAkaun,ROC01_StatusDelete 
        'FROM ROC01_BankSya
        'WHERE ROC01_IDSem='{hdNoIDSem.Value}' AND ROC01_StatusDelete <> 0 ;"

        '        Using ds = dbconn.fselectCommandDs(str2)
        '            Using dt2 = ds.Tables(0)
        '                ViewState("vsDt") = dt2
        '                BindGridViewButiran()
        '            End Using
        '        End Using
    End Sub


    Protected Sub lbtnNext_Click(sender As Object, e As EventArgs) Handles lbtnNext.Click
        Response.Redirect($"~/Forms/Pinjaman/Permohonan Pinjaman/frm_PinjKend_2.aspx?KodSubmenu={ViewState("kodsubmenu")}")
    End Sub

    Protected Sub btnStep2_Click(sender As Object, e As EventArgs) Handles btnStep2.ServerClick
        Response.Redirect($"~/Forms/Pinjaman/Permohonan Pinjaman/frm_PinjKend_2.aspx?KodSubmenu={ViewState("kodsubmenu")}")
    End Sub
    Protected Sub btnStep5_Click(sender As Object, e As EventArgs) Handles btnStep5.ServerClick
        Response.Redirect($"~/Forms/Pinjaman/Permohonan Pinjaman/frm_PinjKend_5.aspx?KodSubmenu={ViewState("kodsubmenu")}")
    End Sub

    Private Function fJanaNoROC()
        Dim strNoROC As String = ""
        Dim indexPO As Integer = 0
        Dim noakhir As Double
        Dim strSQL As String = $"SELECT TOP(1) NoAkhir From MK_NoAkhir WHERE KodModul = 'ROC' AND Prefix = 'RC'"
        dbconn.sSelectCommand(strSQL, noakhir)

        If noakhir <> Nothing Then
            indexPO = noakhir + 1
        Else
            indexPO = 1
        End If

        strNoROC = "RC" + Format(indexPO, "000000").ToString

        Return strNoROC
    End Function

    Private Sub UpdateNoAkhirROC()
        Dim strSql = "SELECT TOP(1) * From MK_NoAkhir WHERE KodModul = 'ROC' AND Prefix = 'RC'"
        Dim dt = dbconn.fSelectCommandDt(strSql, True)

        If dt.Rows.Count > 0 Then
            dt.Rows(0)("NoAkhir") = dt.Rows(0)("NoAkhir") + 1
        Else
            Dim dr As DataRow
            dr = dt.NewRow
            dr("KodModul") = "ROC"
            dr("Prefix") = "RC"
            dr("noakhir") = 1
            dr("Butiran") = "ROC RC"
            dr("kodPTJ") = ""
            dt.Rows.Add(dr)
        End If

        dbconn.sUpdateCommandDt(dt)
    End Sub

    Private Sub fBindDdlKodBank(ddl As DropDownList)

        Dim strsql = "Select KodBank, (KodBank + ' - ' + NamaBank ) as Butiran from MK_Bank ORDER BY NamaBank"

        Dim ds = dbconn.fselectCommandDs(strsql)
        ddl.DataSource = ds
        ddl.DataTextField = "Butiran"
        ddl.DataValueField = "KodBank"
        ddl.DataBind()

        ddl.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
        ddl.SelectedIndex = 0
    End Sub

    Private Function getButiranVot(kod) As String
        Dim butiranVot As String = ""
        Dim str = $"Select butiran from mk_vot where kodVot ='{ kod }';"
        dbconn.sSelectCommand(str, butiranVot)
        Return butiranVot
    End Function

End Class