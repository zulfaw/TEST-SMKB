Imports System.Data.SqlClient

Public Class Daftar_Penghutang
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Dim strStaffID = Session("ssusrID")
                Dim strKodSubMenu As String = Request.QueryString("KodSubMenu")

                fBindDdlKategori()
                fBindDdlNegeri()
                fBindDdlNegara()
                sLoadLst()
                rbStatus.SelectedValue = 1
            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub lBtnBaru_Click(sender As Object, e As EventArgs) Handles lBtnBaru.Click
        sClearFields()
        txtNoPenghutang.Text = ""
        ddlKat.SelectedIndex = 0
        rbStatus.SelectedValue = True
        'ddlPenerima.Items.Clear()

        ddlKat.Enabled = True
        ' ddlPenerima.Enabled = True

        divList.Visible = False
        divDt.Visible = True
        alert.Visible = False
    End Sub

    Protected Sub lbtnKembali_Click(sender As Object, e As EventArgs) Handles lbtnKembali.Click
        sLoadLst()
        divList.Visible = True
        divDt.Visible = False
        'trPen.Visible = True
    End Sub

    Private Sub sLoadLst()
        Dim intRec As Integer = 0
        'Dim strStatDok As String
        Dim strFilter As String = ""
        Dim strSql As String
        Try

            sClearGvLst()

            If ddlCarian.SelectedValue = 1 Then
                strFilter = "Where KodPenghutang = '" & Trim(txtCarian.Text.TrimEnd) & "'"
            ElseIf ddlCarian.SelectedValue = 2 Then
                strFilter = "Where NamaPenghutang like '%" & Trim(txtCarian.Text.TrimEnd) & "%'"
            ElseIf ddlCarian.SelectedValue = 3 Then
                strFilter = "Where IdPenghutang = '" & Trim(txtCarian.Text.TrimEnd) & "'"
            End If

            strSql = "select Id, KodPenghutang, NamaPenghutang, IdPenghutang, Kategori, (select MK_KategoriPenerima.Butiran from MK_KategoriPenerima where MK_KategoriPenerima.kod = AR_Penghutang.Kategori) as ButKat, Alamat1, Alamat2, Bandar, Poskod, KodNegeri, KodNegara, NoTel, NoFax, Emel, Status from AR_Penghutang " & strFilter

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim dt As New DataTable
                    dt = ds.Tables(0)
                    'For Each dtRow As DataRow In dt.Rows
                    '    If dtRow("Status") = True Then
                    '        dtRow("Status") = "Aktif"
                    '    Else
                    '        dtRow("Status") = "Tidak Aktif"
                    '    End If
                    'Next
                    ViewState("dtSenarai") = dt
                    gvLst.DataSource = dt
                    gvLst.DataBind()
                    intRec = ds.Tables(0).Rows.Count
                End If
            End If

            lblJumRekod.InnerText = intRec

        Catch ex As Exception

        End Try
    End Sub

    Private Sub sClearGvLst()
        gvLst.DataSource = New List(Of String)
        gvLst.DataBind()
    End Sub

    Private Sub fBindDdlKategori()
        Try
            Dim strSql As String

            strSql = "SELECT Kod,(Kod + ' - ' + Butiran ) as Butiran from mk_kategoripenerima where kod in ('OA', 'ST', 'SY', 'UG','PG','SH')"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable

            ds = dbconn.fSelectCommand(strSql)
            ddlKat.DataSource = ds
            ddlKat.DataTextField = "Butiran"
            ddlKat.DataValueField = "Kod"
            ddlKat.DataBind()

            ddlKat.Items.Insert(0, New ListItem("- SILA PILIH -", "-"))
            ddlKat.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindDdlNegara()
        Try
            Dim strsql As String
            strsql = "select KodNegara, Butiran from MK_Negara order by KodNegara "

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strsql)
            ddlNegara.DataSource = ds
            ddlNegara.DataTextField = "Butiran"
            ddlNegara.DataValueField = "KodNegara"
            ddlNegara.DataBind()

            ddlNegara.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlNegara.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindDdlNegeri()
        Try
            Dim strsql As String
            strsql = "select KodNegeri, Butiran from MK_Negeri order by KodNegeri"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strsql)
            ddlNegeri.DataSource = ds
            ddlNegeri.DataTextField = "Butiran"
            ddlNegeri.DataValueField = "KodNegeri"
            ddlNegeri.DataBind()

            ddlNegeri.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlNegeri.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' PAPAR SENARAI PENAJA PRASISWAZAH
    ''' </summary>
    Private Sub fBindPenajaPra()
        Try
            Dim dbSMPconn As New DBSMPConn
            Dim intRec
            Dim strFilter
            Dim strSql

            sClearGvLstPenghutang()

            If ddlFilter.SelectedIndex = 1 Then
                strFilter = " WHERE smp_bswNAMA like '%" & Trim(txtFilter.Text.TrimEnd) & "%'"
            ElseIf ddlFilter.SelectedIndex = 2 Then
                strFilter = " WHERE smp_bswkod = '" & Trim(txtFilter.Text.TrimEnd) & "'"
            End If

            strSql = "Select smp_bswkod as ID, smp_bswNAMA as Nama, SMP_BSWALAMAT AS Almt1,'-' AS Almt2, '-' AS Bandar, SMP_BSWPOSKOD as Poskod, SMP_BSWNEGERI as KodNegeri, (select Negeri from SMP_NegeriPL where SMP_NegeriPL.Kod_Negeri = SMP_APBiasiswa.SMP_BSWNEGERI) as ButNegeri, '121' as KodNegara, 'MALAYSIA' as ButNegara, SMP_BSWTEL as NoTel, SMP_BSWFAX as NoFax, SMP_BSWEMAIL as Email, SMP_BSWPEGAWAI as Pegawai from SMP_APBiasiswa " & strFilter & " order by ID;"

            Using ds = dbSMPconn.fselectCommand(strSql)
                If Not ds Is Nothing Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        Dim dt As New DataTable
                        dt = ds.Tables(0)

                        gvLstPenghutang.DataSource = dt
                        gvLstPenghutang.DataBind()

                        ViewState("dtPenghutang") = dt

                        intRec = dt.Rows.Count
                    End If
                End If
            End Using

            lblJumRec.Text = intRec

        Catch ex As Exception

        End Try

    End Sub

    ''' <summary>
    ''' PAPAR SENARAI PENAJA PASCASISWAZAH
    ''' </summary>
    Private Function fBindPenajaPas() As DataSet
        Dim strSql
        Try
            Dim dbSMPconn As New DBSMPConn
            Dim intRec
            Dim strFilter
            If ddlFilter.SelectedIndex = 1 Then
                strFilter = " WHERE smg_bswNAMA like '%" & Trim(txtFilter.Text.TrimEnd) & "%'"
            ElseIf ddlFilter.SelectedIndex = 2 Then
                strFilter = " WHERE smg_bswkod = '" & Trim(txtFilter.Text.TrimEnd) & "'"
            End If

            strSql = "select smg_bswkod as ID, smg_bswNAMA as Nama, SMG_BSWALAMAT1 AS Almt1 , SMG_BSWALAMAT2 AS Almt2, SMG_BSWBANDAR as Bandar, SMG_BSWPOSKOD as Poskod, SMG_BSWNEGERI as KodNegeri, (select negeri from SMP_NegeriPL where SMP_NegeriPL.Kod_Negeri = SMg_APBiasiswa.SMG_BSWNEGERI) as butNegeri, SMG_BSWNEGARA as KodNegara, (select SMG_NEGARA from SMG_NEGARAPL where SMG_KODNEGARA = SMg_APBiasiswa.SMG_BSWNEGARA) as ButNegara, SMG_BSWTEL as NoTel, SMG_BSWFAX as NoFax, SMG_BSWEMAIL as Email, SMG_BSWPEGAWAI as Pegawai from SMg_APBiasiswa " & strFilter & " order by ID"

            'Dim ds As New DataSet
            'ds = dbSMPconn.fselectCommand(strSql)

            Using ds = dbSMPconn.fselectCommand(strSql)
                If Not ds Is Nothing Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        Dim dt As New DataTable
                        dt = ds.Tables(0)

                        gvLstPenghutang.DataSource = dt
                        gvLstPenghutang.DataBind()

                        ViewState("dtPenghutang") = dt

                        intRec = dt.Rows.Count
                    End If
                End If
            End Using

        Catch ex As Exception

        End Try

    End Function

    Private Sub fBindStaff()

        Dim dbconnSMSM As New DBSMConn()
        Dim intRec As Integer
        Try
            sClearGvLstPenghutang()

            Dim strFilter

            If ddlFilter.SelectedIndex = 1 Then
                strFilter = " AND MS01_Nama like '%" & Trim(txtFilter.Text.TrimEnd) & "%'"
            ElseIf ddlFilter.SelectedIndex = 2 Then
                strFilter = " AND MS01_NoStaf = '" & Trim(txtFilter.Text.TrimEnd) & "'"
            End If

            Dim strSQLSMSM = "select MS01_NoStaf as ID, MS01_Nama as Nama from MS01_Peribadi Where MS01_Status = '1'" & strFilter & " Order by ID"

            Using ds = dbconnSMSM.fselectCommand(strSQLSMSM)
                If Not ds Is Nothing Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        Dim dt As New DataTable
                        dt = ds.Tables(0)

                        gvLstPenghutang.DataSource = dt
                        gvLstPenghutang.DataBind()

                        ViewState("dtPenghutang") = dt
                        intRec = dt.Rows.Count
                    End If
                End If
            End Using

            lblJumRec.Text = intRec


        Catch ex As Exception

        End Try
    End Sub


    Private Sub fBindSyarikat()
        Try
            Dim dbconn As New DBKewConn
            Dim strsql As String
            Dim intRec As Integer
            Dim strFilter

            If ddlFilter.SelectedIndex = 1 Then
                strFilter = " AND ROC01_NamaSya like '%" & Trim(txtFilter.Text.TrimEnd) & "%'"
            ElseIf ddlFilter.SelectedIndex = 2 Then
                strFilter = " AND ROC01_IDSYA = '" & Trim(txtFilter.Text.TrimEnd) & "'"
            End If

            strsql = "Select ROC01_IDSYA as ID, ROC01_NamaSya as Nama, ROC01_AlmtP1 AS Almt1, ROC01_AlmtP2 AS Almt2, ROC01_BandarP AS Bandar, ROC01_NegeriP as KodNegeri, ROC01_NegaraP as KodNegara, ROC01_Tel1P as NoTel, ROC01_FaksP as NoFax, ROC01_EmelP as Email, '-' as Pegawai from ROC01_Syarikat 
WHERE ROC01_KodLulus = '1' AND ROC01_KodAktif = '01'" & strFilter & " ORDER BY ID"

            Using ds = dbconn.fSelectCommand(strsql)
                If Not ds Is Nothing Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        Dim dt As New DataTable
                        dt = ds.Tables(0)

                        gvLstPenghutang.DataSource = dt
                        gvLstPenghutang.DataBind()

                        ViewState("dtPenghutang") = dt
                        intRec = dt.Rows.Count
                    End If
                End If

            End Using
            lblJumRec.Text = intRec
        Catch ex As Exception

        End Try
    End Sub

    'Private Sub ddlPenerima_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPenerima.SelectedIndexChanged

    '    Try
    '        sClearFields()

    '        txtNmPenerima.Enabled = False
    '        txtIDNom.Enabled = False

    '        Dim strKat As String = Trim(ddlKat.SelectedValue.TrimEnd)
    '        Dim strIdPnrm As String = Trim(ddlPenerima.SelectedValue.TrimEnd)
    '        Dim strNmPnrm As String = Trim(ddlPenerima.SelectedItem.Text.TrimEnd)

    '        strNmPnrm = Trim(strNmPnrm.Split("-")(1)).TrimEnd

    '        If fExist(strIdPnrm) Then
    '            alert.Visible = True
    '            fGlobalAlert("Individu/Syarikat ini telah didaftarkan sebagai penghutang!", Me.Page, Me.[GetType]())
    '        Else

    '            alert.Visible = False
    '            txtIDNom.Text = strIdPnrm
    '            txtNmPenerima.Text = strNmPnrm
    '            sLoadAlamat(strIdPnrm, strKat)
    '        End If

    '    Catch ex As Exception

    '    End Try

    'End Sub

    Private Sub sClearFields()
        txtNoPenghutang.Text = ""
        txtNamaPenghutang.Text = ""
        txtIDPenghutang.Text = ""
        txtAlmt1.Text = ""
        txtAlmt2.Text = ""
        txtBandar.Text = ""
        txtPoskod.Text = ""
        txtNoTel.Text = ""
        txtNoFax.Text = ""
        txtEmel.Text = ""
        ddlNegeri.SelectedIndex = 0
        ddlNegara.SelectedIndex = 0
        txtPerhatian.Text = ""
    End Sub


    Private Sub sLoadAlamat(ByVal strIdPnrm As String, ByVal strKat As String)
        Dim dbconn As New DBKewConn
        Dim dbconnSMSM As New DBSMConn()
        Try
            Dim strSql As String
            Dim strAlamat1 As String
            Dim strAlamat2 As String
            Dim strBandar As String
            Dim strPoskod As String
            Dim strKodNegeri As String
            Dim strKodNegara As String
            Dim strNoTel As String
            Dim strNoFax As String
            Dim strEmel As String
            Dim strPerhatian As String

            If strKat = "ST" Then
                Dim strSQLSMSM = "SELECT a.MS01_NoStaf, a.MS01_Nama , a.MS01_KpB,b.MS08_Unit,a.ms01_email 
                  FROM MS01_Peribadi a,MS08_Penempatan b WHERE b.MS08_StaTerkini = '1' AND a.MS01_NoStaf=b.MS01_NoStaf 
                  AND a.MS01_NoStaf =  '" & strIdPnrm & "'"

                Dim ds As New DataSet
                ds = dbconnSMSM.fselectCommand(strSQLSMSM)

                If Not ds Is Nothing Then
                    strSql = "Select Singkatan from MK_Ptj where KodPTJ = '" & ds.Tables(0).Rows(0)("MS08_Unit").ToString & "'"

                    Dim ds2 As New DataSet
                    ds2 = dbconn.fSelectCommand(strSql)

                    If ds2.Tables(0).Rows.Count > 0 Then
                        txtAlmt1.Text = ds2.Tables(0).Rows(0)("Singkatan").ToString
                    Else
                        txtAlmt1.Text = "-"
                    End If
                    txtEmel.Text = ds.Tables(0).Rows(0)("ms01_email").ToString
                End If

                strSql = "SELECT Nama, Almt1, Almt2 , Bandar, Poskod, NoTel1, NoFaks1, KodNegeri, (select Butiran  from MK_Negeri where MK_Negeri.KodNegeri = MK_Korporat .KodNegeri) as ButNegeri, KodNegara, (select Butiran  from MK_Negara where MK_Negara .KodNegara = MK_Korporat .KodNegara) as ButNegara FROM MK_Korporat"

                Dim ds3 As New DataSet
                ds3 = dbconn.fSelectCommand(strSql)

                If Not ds Is Nothing Then
                    If ds3.Tables(0).Rows.Count > 0 Then
                        txtAlmt2.Text = ds3.Tables(0).Rows(0)("Nama").ToString
                        txtBandar.Text = ds3.Tables(0).Rows(0)("Bandar").ToString
                        txtPoskod.Text = ds3.Tables(0).Rows(0)("Poskod").ToString
                        'txtKodNegeri.Text = ds3.Tables(0).Rows(0)("KodNegeri").ToString
                        'txtNegeri.Text = ds3.Tables(0).Rows(0)("ButNegeri").ToString
                        'txtKodNegeri.Text = ds3.Tables(0).Rows(0)("KodNegara").ToString
                        'txtNegara.Text = ds3.Tables(0).Rows(0)("ButNegara").ToString

                        ddlNegeri.SelectedValue = ds.Tables(0).Rows(0)("KodNegeri").ToString
                        ddlNegara.SelectedValue = ds.Tables(0).Rows(0)("KodNegara").ToString

                        txtNoTel.Text = ds3.Tables(0).Rows(0)("NoTel1").ToString
                        txtNoFax.Text = ds3.Tables(0).Rows(0)("NoFaks1").ToString
                        txtPerhatian.Text = "-"
                    End If
                End If

                '            ElseIf strKat = "SY" Then

                '                strSql = "Select ROC01_AlmtP1 as Almt1,ROC01_AlmtP2 as Almt2, ROC01_BandarP as Bandar, ROC01_PoskodP as Poskod, ROC01_NegeriP as KodNegeri, (select Butiran  from MK_Negeri where MK_Negeri.KodNegeri = ROC01_Syarikat.ROC01_NegeriP) as ButNegeri, ROC01_NegaraP as KodNegara, (select Butiran from MK_Negara where MK_Negara.KodNegara = ROC01_Syarikat.ROC01_NegaraP) as ButNegara, ROC01_Tel1P as NoTel, ROC01_FaksP as NoFax, ROC01_EmelP as Email, isnull(ROC01_WakilSya,'-') as ROC01_WakilSya from ROC01_Syarikat 
                'where ROC01_IDSya = '" & strIdPnrm & "'"

                '                Dim ds As New DataSet
                '                ds = dbconn.fSelectCommand(strSql)

                '                If Not ds Is Nothing Then
                '                    If ds.Tables(0).Rows.Count > 0 Then
                '                        strAlamat1 = ds.Tables(0).Rows(0)("Almt1").ToString
                '                        strAlamat2 = ds.Tables(0).Rows(0)("Almt2").ToString
                '                        strBandar = ds.Tables(0).Rows(0)("Bandar").ToString
                '                        strPoskod = ds.Tables(0).Rows(0)("Poskod").ToString
                '                        strKodNegeri = ds.Tables(0).Rows(0)("KodNegeri").ToString
                '                        strKodNegara = ds.Tables(0).Rows(0)("KodNegara").ToString
                '                        txtNoTel.Text = ds.Tables(0).Rows(0)("NoTel").ToString
                '                        txtNoFax.Text = ds.Tables(0).Rows(0)("NoFax").ToString
                '                        txtEmel.Text = ds.Tables(0).Rows(0)("Email").ToString
                '                        txtPerhatian.Text = ds.Tables(0).Rows(0)("ROC01_WakilSya").ToString

                '                    End If
                '                End If

            ElseIf strKat = "UG" OrElse strKat = "PG" OrElse strKat = "SY" Then
                Dim dtLstPel As DataTable = TryCast(ViewState("dtPenghutang"), DataTable)
                Dim dvLstPel As New DataView(dtLstPel)

                dvLstPel.RowFilter = "ID = '" & strIdPnrm & "'"

                strAlamat1 = dvLstPel.Item(0)("Almt1").ToString
                strAlamat2 = dvLstPel.Item(0)("Almt2").ToString
                strBandar = dvLstPel.Item(0)("Bandar").ToString
                strPoskod = dvLstPel.Item(0)("Poskod").ToString
                strKodNegeri = dvLstPel.Item(0)("KodNegeri").ToString
                strKodNegara = dvLstPel.Item(0)("KodNegara").ToString
                strNoTel = dvLstPel.Item(0)("NoTel").ToString
                strNoFax = dvLstPel.Item(0)("NoFax").ToString
                strEmel = dvLstPel.Item(0)("Email").ToString
                strPerhatian = dvLstPel.Item(0)("Pegawai").ToString

            End If

            txtAlmt1.Text = IIf(strAlamat1 = "", "-", strAlamat1)
            txtAlmt2.Text = IIf(strAlamat2 = "", "-", strAlamat2)
            txtBandar.Text = IIf(strBandar = "", "-", strBandar)
            txtPoskod.Text = IIf(strPoskod = "", "-", strPoskod)

            txtNoTel.Text = IIf(strNoTel = "", "-", strNoTel)
            txtNoFax.Text = IIf(strNoFax = "", "-", strNoFax)
            txtEmel.Text = IIf(strEmel = "", "-", strEmel)
            txtPerhatian.Text = IIf(strPerhatian = "", "-", strPerhatian)

            Try
                ddlNegeri.SelectedValue = strKodNegeri

            Catch ex As Exception
                ddlNegeri.SelectedValue = 0
            End Try

            Try
                ddlNegara.SelectedValue = strKodNegara
            Catch ex As Exception
                ddlNegara.SelectedValue = 0
            End Try



        Catch ex As Exception

        End Try


    End Sub

    Private Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.Click
        If Page.IsValid Then

            Dim strNoPenghutang As String = Trim(txtNoPenghutang.Text.TrimEnd)
            If String.IsNullOrEmpty(strNoPenghutang) Then

                If Trim(ddlKat.SelectedValue) = "OA" Then
                    Dim strIdPenghutang As String = Trim(txtIDPenghutang.Text.TrimEnd)
                    If fExist(strIdPenghutang) Then
                        alert.Visible = True
                        lbtnNew.Visible = True
                        fGlobalAlert("Individu/Syarikat ini telah didaftarkan sebagai penghutang!", Me.Page, Me.[GetType]())
                        Exit Sub
                    End If
                End If

                If fSimpan() = True Then
                        fGlobalAlert("Maklumat telah disimpan!", Me.Page, Me.[GetType]())
                        lbtnNew.Visible = True
                    Else
                        fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
                    End If
                Else
                    If fKemasKini(strNoPenghutang) = True Then
                    fGlobalAlert("Maklumat telah disimpan!", Me.Page, Me.[GetType]())
                Else
                    fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
                End If
            End If

        End If
    End Sub

    Private Function fSimpan() As Boolean
        Dim blnSuccess As Boolean = True

        Dim dbconn As New DBKewConn
        Dim strTahun As String = Now.Year
        Dim strNoPenghutang As String
        Try

            Dim strSql As String
            Dim strTkhToday As String = Now.ToString("yyyy-MM-dd")
            Dim dtTkhToday As DateTime = CDate(strTkhToday)

            Dim IDPenghutang As String = Trim(txtIDPenghutang.Text.TrimEnd)
            Dim NamaPenghutang As String = Trim(txtNamaPenghutang.Text.TrimEnd)
            Dim strAlamat1 As String = Trim(txtAlmt1.Text.TrimEnd)
            Dim strAlamat2 As String = Trim(txtAlmt2.Text.TrimEnd)
            Dim strBdr As String = Trim(txtBandar.Text.TrimEnd)
            Dim strPoskod As String = Trim(txtPoskod.Text.TrimEnd)

            'Dim KodNegeri As String = Trim(txtKodNegeri.Text.TrimEnd)
            'Dim strNegeri As String = Trim(txtNegeri.Text.TrimEnd)
            'Dim KodNegara As String = Trim(txtKodNegara.Text.TrimEnd)
            'Dim strNegara As String = Trim(txtNegara.Text.TrimEnd)

            Dim KodNegeri As String = Trim(ddlNegeri.SelectedValue.TrimEnd)
            Dim KodNegara As String = Trim(ddlNegara.SelectedValue.TrimEnd)

            Dim strEmel As String = Trim(txtEmel.Text.TrimEnd)
            Dim strNoTel As String = Trim(txtNoTel.Text.TrimEnd)
            Dim strNoFax As String = Trim(txtNoFax.Text.TrimEnd)
            Dim strKat As String = Trim(ddlKat.SelectedValue.TrimEnd)
            Dim blnStatus As Boolean = rbStatus.SelectedValue
            Dim strPerhatian As String = Trim(txtPerhatian.Text.TrimEnd)
            Dim strButiranNoAkhir As String = "Kod Penghutang"
            Dim strKodModul = "AR"
            Dim strPrefix = "H"

            strNoPenghutang = fGetNoPenghutang(strTahun, strKodModul, strPrefix, strButiranNoAkhir)

            If strNoPenghutang Is Nothing Then
                blnSuccess = False
                Exit Try
            End If


            dbconn.sConnBeginTrans()

            'Insert Into AR01_Bil
            strSql = "insert into AR_Penghutang (KodPenghutang, NamaPenghutang, IdPenghutang, Kategori, Alamat1 , Alamat2, Bandar, Poskod, KodNegeri, KodNegara, NoTel, NoFax, Emel, Perhatian, Status) Values (@KodPenghutang, @NamaPenghutang, @IdPenghutang, @Kategori, @Alamat1, @Alamat2, @Bandar, @Poskod, @KodNegeri, @KodNegara, @NoTel, @NoFax, @Emel, @Perhatian, @Status)"

            Dim paramSql() As SqlParameter =
                {
                New SqlParameter("@KodPenghutang", strNoPenghutang),
                New SqlParameter("@NamaPenghutang", NamaPenghutang),
                New SqlParameter("@IdPenghutang", IDPenghutang),
                New SqlParameter("@Kategori", strKat),
                New SqlParameter("@Alamat1", IIf(strAlamat1 = "", "-", strAlamat1)),
                New SqlParameter("@Alamat2", IIf(strAlamat2 = "", "-", strAlamat2)),
                New SqlParameter("@Bandar", IIf(strBdr = "", "-", strBdr)),
                New SqlParameter("@Poskod", IIf(strPoskod = "", "-", strPoskod)),
                New SqlParameter("@KodNegeri", KodNegeri),
                New SqlParameter("@KodNegara", KodNegara),
                New SqlParameter("@NoTel", IIf(strNoTel = "", "-", strNoTel)),
                New SqlParameter("@NoFax", IIf(strNoFax = "", "-", strNoFax)),
                New SqlParameter("@Emel", IIf(strEmel = "", "-", strEmel)),
                New SqlParameter("@Perhatian", IIf(strPerhatian = "", "-", strPerhatian)),
                New SqlParameter("@Status", blnStatus)
                }

            If Not dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                blnSuccess = False
                Exit Try
            End If

        Catch ex As Exception
            fErrorLog(ex.Message.ToString)
            blnSuccess = False
        End Try

        If blnSuccess = True Then
            dbconn.sConnCommitTrans()
            txtNoPenghutang.Text = strNoPenghutang
            Return True
        ElseIf blnSuccess = False Then
            dbconn.sConnRollbackTrans()
            Return False
        End If

    End Function

    Public Function fGetNoPenghutang(ByVal strTahun As String, ByVal strKodModul As String, ByVal strPrefix As String, ByVal strButiran As String)
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
                    strIdx = strPrefix & Now.ToString("yy") & "001"

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
                    strIdx = strPrefix & Now.ToString("yy") & intLastIdx.ToString("D3")

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

    Protected Sub lbtnNew_Click(sender As Object, e As EventArgs) Handles lbtnNew.Click
        sClearFields()
        txtNoPenghutang.Text = ""
        ddlKat.SelectedIndex = 0
        rbStatus.SelectedValue = 1
        'ddlPenerima.Items.Clear()

        ddlKat.Enabled = True
        'ddlPenerima.Enabled = True
        alert.Visible = False
    End Sub

    Private Sub gvLst_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvLst.SelectedIndexChanged
        Try
            Dim row As GridViewRow = gvLst.SelectedRow

            Dim strIdPenghutang As String = CType(gvLst.SelectedRow.FindControl("lblIdPenghutang"), Label).Text.TrimEnd


            sLoadDetail(strIdPenghutang)

            divList.Visible = False
            divDt.Visible = True

        Catch ex As Exception

        End Try
    End Sub

    Private Sub sLoadDetail(ByVal strIdPenghutang As String)
        Dim dbconn As New DBKewConn
        Try
            sClearFields()

            Dim strSqlInv = "select Id, KodPenghutang, NamaPenghutang, IdPenghutang, Kategori, (select MK_KategoriPenerima.Butiran from MK_KategoriPenerima where MK_KategoriPenerima.kod = AR_Penghutang.Kategori) as ButKat, Alamat1, Alamat2, Bandar, Poskod, KodNegeri, KodNegara, NoTel, NoFax, Emel, Status, Perhatian from AR_Penghutang 
Where IdPenghutang = '" & strIdPenghutang & "'"

            Dim ds = dbconn.fSelectCommand(strSqlInv)

            Using dt = ds.Tables(0)
                If dt.Rows.Count > 0 Then

                    Dim strKat As String = dt.Rows(0)("Kategori")
                    Dim strNamaPenghutang As String = dt.Rows(0)("NamaPenghutang")
                    Dim strKodPenghutang As String = dt.Rows(0)("KodPenghutang")
                    Dim strAlamat1 As String = dt.Rows(0)("Alamat1")
                    Dim strAlamat2 As String = dt.Rows(0)("Alamat2")
                    Dim strBandar As String = dt.Rows(0)("Bandar")
                    Dim strPoskod As String = dt.Rows(0)("Poskod")
                    Dim strKodNegeri As String = dt.Rows(0)("KodNegeri")
                    Dim strKodNegara As String = dt.Rows(0)("KodNegara")
                    Dim strNoTel As String = dt.Rows(0)("NoTel")
                    Dim strNoFax As String = dt.Rows(0)("NoFax")
                    Dim strEmel As String = dt.Rows(0)("Emel")
                    Dim blnStatus As Boolean = dt.Rows(0)("Status")
                    Dim strPerhatian As String = dt.Rows(0)("Perhatian")

                    txtNoPenghutang.Text = strKodPenghutang
                    ddlKat.SelectedValue = strKat
                    txtNamaPenghutang.Text = strNamaPenghutang
                    txtIDPenghutang.Text = strIdPenghutang
                    txtAlmt1.Text = IIf(strAlamat1 = "", "-", strAlamat1)
                    txtAlmt2.Text = IIf(strAlamat2 = "", "-", strAlamat2)
                    txtBandar.Text = IIf(strBandar = "", "-", strBandar)
                    txtPoskod.Text = IIf(strPoskod = "", "-", strPoskod)
                    ddlNegeri.SelectedValue = strKodNegeri
                    ddlNegara.SelectedValue = strKodNegara
                    txtNoTel.Text = IIf(strNoTel = "", "-", strNoTel)
                    txtNoFax.Text = IIf(strNoFax = "", "-", strNoFax)
                    txtEmel.Text = IIf(strEmel = "", "-", strEmel)
                    rbStatus.SelectedValue = blnStatus
                    ddlKat.Enabled = False
                    txtPerhatian.Text = IIf(strPerhatian = "", "-", strPerhatian)
                End If
            End Using

        Catch ex As Exception

        End Try

    End Sub

    Private Function fKemasKini(ByVal strNoPenghutang As String) As Boolean

        Dim strSql As String
        Dim dbconn As New DBKewConn
        Dim blnSuccess As Boolean = True

        Try

            Dim strAlamat1 As String = Trim(txtAlmt1.Text.TrimEnd)
            Dim strAlamat2 As String = Trim(txtAlmt2.Text.TrimEnd)
            Dim strBdr As String = Trim(txtBandar.Text.TrimEnd)
            Dim strPoskod As String = Trim(txtPoskod.Text.TrimEnd)
            Dim KodNegeri As String = Trim(ddlNegeri.SelectedValue.TrimEnd)
            Dim KodNegara As String = Trim(ddlNegara.SelectedValue.TrimEnd)
            Dim strEmel As String = Trim(txtEmel.Text.TrimEnd)
            Dim strNoTel As String = Trim(txtNoTel.Text.TrimEnd)
            Dim strNoFax As String = Trim(txtNoFax.Text.TrimEnd)
            Dim blnStatus As Boolean = rbStatus.SelectedValue
            Dim strPerhatian As String = Trim(txtPerhatian.Text.TrimEnd)

            'Update AR01_Bil
            strSql = "update AR_Penghutang SET Alamat1 = @Alamat1, Alamat2 = @Alamat2, Bandar = @Bandar, Poskod = @Poskod,
				KodNegeri = @KodNegeri, KodNegara = @KodNegara, NoTel = @NoTel, NoFax = @NoFax, Emel = @Emel, Perhatian = @Perhatian, Status = @Status"

            Dim paramSql() As SqlParameter =
                {
                New SqlParameter("@Alamat1", IIf(strAlamat1 = "", "-", strAlamat1)),
                New SqlParameter("@Alamat2", IIf(strAlamat2 = "", "-", strAlamat2)),
                New SqlParameter("@Bandar", IIf(strBdr = "", "-", strBdr)),
                New SqlParameter("@Poskod", IIf(strPoskod = "", "-", strPoskod)),
                New SqlParameter("@KodNegeri", KodNegeri),
                New SqlParameter("@KodNegara", KodNegara),
                New SqlParameter("@NoTel", IIf(strNoTel = "", "-", strNoTel)),
                New SqlParameter("@NoFax", IIf(strNoFax = "", "-", strNoFax)),
                New SqlParameter("@Emel", IIf(strEmel = "", "-", strEmel)),
                New SqlParameter("@Perhatian", IIf(strPerhatian = "", "-", strPerhatian)),
                New SqlParameter("@Status", blnStatus)
                }

            dbconn.sConnBeginTrans()
            If Not dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
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

    Private Function fExist(ByVal strIdPenghutang As String) As Boolean
        Dim dbconn As New DBKewConn
        Try
            '            Dim strSqlInv = "select Id, KodPenghutang, NamaPenghutang, IdPenghutang, Kategori, (select MK_KategoriPenerima.Butiran from MK_KategoriPenerima where MK_KategoriPenerima.kod = AR_Penghutang.Kategori) as ButKat, Alamat1, Alamat2, Bandar, Poskod, KodNegeri, KodNegara, NoTel, NoFax, Emel, Status, Perhatian from AR_Penghutang 
            'Where IdPenghutang = '" & strIdPnrm & "'"

            Dim strSql
            strSql = "Select KodPenghutang from AR_Penghutang Where IdPenghutang = '" & strIdPenghutang & "'"

            Dim ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    sLoadDetail(strIdPenghutang)
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If

        Catch ex As Exception

        End Try

    End Function

    Private Sub lbtnHapus_Click(sender As Object, e As EventArgs) Handles lbtnHapus.Click
        Dim dbconn As New DBKewConn

        Try
            Dim strNoPenghutang As String = Trim(txtNoPenghutang.Text.TrimEnd)

            Dim strSql As String = "delete from AR_Penghutang where KodPenghutang = '" & strNoPenghutang & "'"
            dbconn.sConnBeginTrans()
            If dbconn.fUpdateCommand(strSql) > 0 Then
                dbconn.sConnCommitTrans()
                fGlobalAlert("Rekod penghutang ini telah dihapuskan!", Me.Page, Me.[GetType]())
                divDt.Visible = False
                divList.Visible = True
                lbtnHapus.Visible = False
                lbtnSimpan.Visible = True
                lbtnNew.Visible = True
                sLoadLst()
            Else
                dbconn.sConnRollbackTrans()
                fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
            End If

        Catch ex As Exception
            fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
        End Try

    End Sub

    Private Sub lbtnOpenLst_Click(sender As Object, e As EventArgs) Handles lbtnOpenLst.Click
        Try

            alert.Visible = False
            gvLstPenghutang.PageIndex = 0
            ddlFilter.Items.Clear()
            txtFilter.Text = ""
            txtFilter.Enabled = False

            If ddlKat.SelectedIndex = 0 Then
                fGlobalAlert("Sila pilih 'Kategori'!", Me.Page, Me.[GetType]())
                Exit Sub

            End If

            Dim arLst As New ArrayList()

            sClearGvLstPenghutang()

            If ddlKat.SelectedValue = "ST" Then
                fBindStaff()
                lblTitle.Text = "SENARAI STAF"

                arLst.Add("Nama Staf")
                arLst.Add("No Staf")

            ElseIf ddlKat.SelectedValue = "SY" Then
                fBindSyarikat()
                lblTitle.Text = "SENARAI SYARIKAT"

                arLst.Add("Nama Syarikat")
                arLst.Add("ID Syarikat")

            ElseIf ddlKat.SelectedValue = "UG" Then
                fBindPenajaPra()
                lblTitle.Text = "SENARAI PENAJA PRA SISWAZAH"

                arLst.Add("Nama Penaja")
                arLst.Add("ID Penaja")

            ElseIf ddlKat.SelectedValue = "PG" Then
                fBindPenajaPas()
                lblTitle.Text = "SENARAI PENAJA PASCA SISWAZAH"

                arLst.Add("Nama Penaja")
                arLst.Add("ID Penaja")

            End If

            ddlFilter.Items.Insert(0, New ListItem("- KESELURUHAN -", 0))

            For Each customer As Object In arLst
                ddlFilter.Items.Add(New ListItem(customer.ToString(), customer.ToString()))
            Next
            ddlFilter.SelectedIndex = 0

            mpeLstPenghutang.Show()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlKat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKat.SelectedIndexChanged
        If ddlKat.SelectedValue = "OA" Then
            lbtnOpenLst.Visible = False
            txtNamaPenghutang.Enabled = True
            txtIDPenghutang.Enabled = True
        Else
            lbtnOpenLst.Visible = True
            txtNamaPenghutang.Enabled = False
            txtIDPenghutang.Enabled = False
        End If

        sClearFields()
    End Sub

    Private Sub sClearGvLstPenghutang()
        gvLstPenghutang.DataSource = New List(Of String)
        gvLstPenghutang.DataBind()
    End Sub

    Private Sub gvLstPenghutang_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvLstPenghutang.PageIndexChanging

        gvLstPenghutang.PageIndex = e.NewPageIndex
        If ViewState("dtPenghutang") IsNot Nothing Then
            gvLstPenghutang.DataSource = ViewState("dtPenghutang")
            gvLstPenghutang.DataBind()
        End If
        mpeLstPenghutang.Show()
    End Sub

    Private Sub ddlFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFilter.SelectedIndexChanged
        Try
            If ddlFilter.SelectedIndex = 0 Then
                txtFilter.Enabled = False
                txtFilter.Text = ""
            Else
                txtFilter.Enabled = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvLstPenghutang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvLstPenghutang.SelectedIndexChanged

        sClearFields()

        Dim selectedRow = gvLstPenghutang.SelectedRow

        Dim strID As String = Trim(CType(selectedRow.FindControl("lblID"), Label).Text.TrimEnd)
        Dim strNama As String = Trim(CType(selectedRow.FindControl("lblNama"), Label).Text.TrimEnd)

        If fExist(strID) Then
            alert.Visible = True
            fGlobalAlert("Individu/Syarikat ini telah didaftarkan sebagai penghutang!", Me.Page, Me.[GetType]())
        Else

            alert.Visible = False
            txtNamaPenghutang.Text = strNama
            txtIDPenghutang.Text = strID

            Dim strKat As String = ddlKat.SelectedValue
            sLoadAlamat(strID, strKat)
        End If

        mpeLstPenghutang.Hide()
    End Sub

    Private Sub lbtnFind_Click(sender As Object, e As EventArgs) Handles lbtnFind.Click


        Try
            'Dim dtPenghutang As DataTable = TryCast(ViewState("dtPenghutang"), DataTable)
            'Dim dvPenghutang As DataView

            'dvPenghutang = dtPenghutang.DefaultView

            'If ddlFilter.SelectedIndex = 0 Then 'KESELURUHAN
            'ElseIf ddlFilter.SelectedIndex = 1 Then 'NAMA
            '    dvPenghutang.RowFilter = "Nama like '%" & Trim(txtFilter.Text.TrimEnd) & "%'"
            'ElseIf ddlFilter.SelectedIndex = 2 Then 'ID
            '    dvPenghutang.RowFilter = "ID = '" & Trim(txtFilter.Text.TrimEnd) & "'"
            'End If

            If ddlKat.SelectedValue = "ST" Then
                fBindStaff()

            ElseIf ddlKat.SelectedValue = "SY" Then
                fBindSyarikat()

            ElseIf ddlKat.SelectedValue = "UG" Then
                fBindPenajaPra()

            ElseIf ddlKat.SelectedValue = "PG" Then
                fBindPenajaPas()
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub gvLstPenghutang_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvLstPenghutang.Sorting
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
            dt = CType(ViewState("dtPenghutang"), DataTable)

            Dim sortedView As New DataView(dt)
            sortedView.Sort = Convert.ToString(e.SortExpression + " ") & sortingDirection
            Session("SortedView") = sortedView
            gvLstPenghutang.DataSource = sortedView
            gvLstPenghutang.DataBind()

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
End Class