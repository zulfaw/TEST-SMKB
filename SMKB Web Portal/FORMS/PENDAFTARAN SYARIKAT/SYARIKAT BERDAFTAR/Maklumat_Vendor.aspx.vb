Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization
Imports System.IO

Public Class Maklumat_Vendor
    Inherits System.Web.UI.Page

    Private dbconn As New DBKewConn()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim KodSubmenu = Request.QueryString("KodSubMenu")


            hdNoSya.Value = Request.QueryString("noSya")
            hdNoIDSemSya.Value = Request.QueryString("noSem")
            txtNoSya.Text = hdNoSya.Value


            'fLoadGVStaf()
            fLoadDetail()
            fLoadGVSalinanLesen()
            fLoadGVSalinanSijilStaf()
            fLoadGVSalinanGST()

            If KodSubmenu.Equals("070102") Then
                'Kelulusan Pendaftaran
                rfvrbKelulusanDaftar.Enabled = True
                trbtnHantar.Visible = True
                trLulusDaftar.Visible = True
            ElseIf KodSubmenu.Equals("070103") Then
                'Kelulusan Kemaskini
                rfvrbKelulusanKemaskini.Enabled = True
                trbtnHantar.Visible = True
                trLulusKemaskini.Visible = True
            ElseIf KodSubmenu.Equals("070104") Then
                'Penamatan
                rfvddlPenamatan.Enabled = True
                trbtnHantar.Visible = True
                trTamat.Visible = True
                trUlasan.Visible = True
                rfvUlasan.Enabled = True
            ElseIf KodSubmenu.Equals("020701") OrElse KodSubmenu.Equals("020702") Then
                'Penilaian Harga 
                txtNoSya.Visible = False
                txtNamaSya.Visible = False
            ElseIf KodSubmenu.Equals("020603") Then
                'Penilaian Teknikal 
                txtNoSya.Visible = False
                txtNamaSya.Visible = False
            ElseIf KodSubmenu.Equals("020801") OrElse KodSubmenu.Equals("020802") Then
                'Pengesyoran
                txtNoSya.Visible = False
                txtNamaSya.Visible = False
            ElseIf KodSubmenu.Equals("020903") Then
                'Perlantikan 
                txtNoSya.Visible = False
                txtNamaSya.Visible = False
            End If
        End If
    End Sub

    Private Sub fBindGVLesen()
        'Sijil Syarikat
        Dim sqlROC02 As String = $"SELECT [ROC02_ID],[ROC02_NoSya],[KodDaftar],[ROC02_NoDaftar],
cast([ROC02_TkhMula] as Date) as TkhMula,cast([ROC02_TkhTamat] as Date) as TkhTmt,[ROC02_TkhTamat],[ROC01_IDSem]
FROM [ROC02_DaftarSya]
WHERE [ROC01_IDSem] = '{hdNoIDSemSya.Value}';"

        Dim ds = dbconn.fselectCommand(sqlROC02)
        Dim dtsqlROC02 = ds.Tables(0)
        If dtsqlROC02.Rows.Count > 0 Then
            gvLesen.DataSource = dtsqlROC02
            gvLesen.DataBind()
        Else
            gvLesen.DataSource = New List(Of String)
            gvLesen.DataBind()
        End If
    End Sub

    Private Sub fLoadDetail()

        Dim sqlROC01 As String = $"SELECT [ROC01_IDSya],[ROC01_NoSya],[ROC01_NamaSya],[ROC01_KodGred]
      ,[ROC01_KodJenPem],[ROC01_KodBumi],[ROC01_KodBank],[ROC01_NoAkaun],[ROC01_Keterangan],[ROC01_AlmtP1],[ROC01_AlmtP2]
      ,[ROC01_BandarP],[ROC01_PoskodP],[ROC01_NegeriP],[ROC01_NegaraP],[ROC01_Tel1P],[ROC01_Tel2P],[ROC01_FaksP]
      ,[ROC01_WebP],[ROC01_EmelP],[ROC01_AlmtSM1],[ROC01_AlmtSM2],[ROC01_BandarSM],[ROC01_PoskodSM],[ROC01_NegeriSM]
      ,[ROC01_NegaraSM],[ROC01_Tel1SM],[ROC01_Tel2SM],[ROC01_FaksSM],[ROC01_WebSM],[ROC01_EmelSM]
      ,[ROC01_KemaskiniStatus],[ROC01_KodLulus],[ROC01_ButiranLulus],[ROC01_TkhLulus],[ROC01_KodAktif],[ROC01_WakilSya]
      ,[ROC01_TkhKemaskini],[ROC01_CetakSijil],[ROC01_Cawangan],[ROC01_SyktPnjm],[ROC01_KodKelas]
      ,[ROC01_IDSem],[CLM_LoginID],[ROC01_TkhTmtUniv],[ROC01_KodAktifUniv],[ROC01_FlagKend],[ROC01_NoGST]
      ,[ROC01_TkhSah],[ROC01_KategoriSya], ROC01_TahunAktif, ROC01_Bayar, c.Butiran AS NegaraP, d.Butiran AS NegeriP,
e.Butiran As StatAktif, f.Butiran AS StatLulus, g.nama as NamaBank, ROC01_Bekalan, ROC01_Perkhidmatan, ROC01_Kerja
FROM [ROC01_Syarikat] a,  MK_Negara c, MK_Negeri d, ROC_StatAktif e, ROC_StatLulus f, MK_BankCreditOnline g
WHERE  c.KodNegara = a.ROC01_NegaraP AND d.KodNegeri = a.ROC01_NegeriP
AND e.KodAktif = a.ROC01_KodAktif AND f.KodLulus = a.ROC01_KodLulus AND g.Kod=a.ROC01_KodBank
AND [ROC01_IDSem] = '{hdNoIDSemSya.Value}';"

        'Sijil Syarikat
        Dim sqlROC02 As String = $"SELECT [ROC02_ID],[ROC02_NoSya],[KodDaftar],[ROC02_NoDaftar],
cast([ROC02_TkhMula] as Date) as TkhMula,cast([ROC02_TkhTamat] as Date) as TkhTmt,[ROC02_TkhTamat]
FROM [ROC02_DaftarSya]
WHERE [ROC01_IDSem] = '{hdNoIDSemSya.Value}';"

        'Bidang MOF
        Dim sqlROC03 As String = $"SELECT [ROC03_ID],[ROC03_Bil],ROC03_KodBidang, b.Butiran 
FROM ROC03_BidangSya a, ROC_Bidang b 
WHERE ROC03_KodBidang = KodBidang AND [ROC01_IDSem] = '{hdNoIDSemSya.Value}';"

        'Staff
        Dim sqlROC04 As String = $"SELECT [ROC04_ID],ROC04_IC,[ROC04_NoSya],[ROC04_Ruj],[ROC04_Nama],[ROC04_Jwtn]
        ,[ROC04_Jbtn],[ROC04_NoTel],[ROC04_Gelaran],ROC04_JnsSijil
        FROM [ROC04_Rujukan]
        WHERE [ROC01_IDSem] = '{hdNoIDSemSya.Value}'  AND ROC04_FlagStaf = 1;"

        'CIDB
        Dim sqlROC05 As String = $"SELECT [ROC07_ID],[ROC07_ID],[ROC07_KodKategori],[ROC07_KodKhusus], b.Butiran
FROM [ROC07_KhususCIDB] a, ROC_PengkhususanCIDB b 
WHERE [ROC01_IDSem] = '{hdNoIDSemSya.Value}' AND ROC07_KodKhusus = b.KodKhusus;"

        Dim strLembPeng6 = $"Select ROC04_ID,ROC04_NoSya,[ROC01_IDSem],ROC04_Nama, ROC04_Ruj, ROC04_IC,
ROC04_NoTel, ROC04_Gelaran 
From [ROC04_Rujukan]
Where [ROC01_IDSem] = '{hdNoIDSemSya.Value}' AND (ROC04_FlagStaf = 2);"

        Dim strPemegangSaham7 As String = $"SELECT ROC04_ID,ROC04_NoSya,[ROC01_IDSem],ROC04_Nama, ROC04_Ruj, ROC04_IC,
ROC04_NoTel, ROC04_Peratus
FROM [ROC04_Rujukan] 
WHERE [ROC01_IDSem] = '{hdNoIDSemSya.Value}' AND ROC04_FlagStaf = 3;"

        Dim ds = dbconn.fselectCommand(sqlROC01 + sqlROC02 + sqlROC03 + sqlROC04 + sqlROC05 + strLembPeng6 + strPemegangSaham7)

        Using dt = ds.Tables(0)
            If dt.Rows.Count > 0 Then
                txtNoSya.Text = dt.Rows(0)("ROC01_NoSya").ToString
                txtNamaSya.Text = dt.Rows(0)("ROC01_NamaSya").ToString

                Dim Bekalan As Boolean = IIf(IsDBNull(dt.Rows(0)("ROC01_Bekalan")), False, dt.Rows(0)("ROC01_Bekalan"))
                Dim Perkhidmatan As Boolean = IIf(IsDBNull(dt.Rows(0)("ROC01_Perkhidmatan")), False, dt.Rows(0)("ROC01_Perkhidmatan"))
                Dim Kerja As Boolean = IIf(IsDBNull(dt.Rows(0)("ROC01_Kerja")), False, dt.Rows(0)("ROC01_Kerja"))

                If Bekalan Then
                    chxNiagaUtama.Items(0).Selected = True
                End If
                If Perkhidmatan Then
                    chxNiagaUtama.Items(1).Selected = True
                End If
                If Kerja Then
                    chxNiagaUtama.Items(2).Selected = True
                End If

                lblGredKerja.Text = dt.Rows(0)("ROC01_KodGred").ToString
                rbBumi.SelectedValue = dt.Rows(0)("ROC01_KodBumi").ToString

                txtNoGST.Text = dt.Rows(0)("ROC01_NoGST").ToString

                txtAlamat1.Text = dt.Rows(0)("ROC01_AlmtP1").ToString
                txtAlamat2.Text = dt.Rows(0)("ROC01_AlmtP2").ToString
                txtBandar.Text = dt.Rows(0)("ROC01_BandarP").ToString
                txtPoskod.Text = dt.Rows(0)("ROC01_PoskodP").ToString
                txtTelp1.Text = dt.Rows(0)("ROC01_Tel1P").ToString
                txtTelp2.Text = dt.Rows(0)("ROC01_Tel2P").ToString
                txtFax.Text = dt.Rows(0)("ROC01_FaksP").ToString
                txtWeb.Text = dt.Rows(0)("ROC01_WebP").ToString
                txtEmailSya.Text = dt.Rows(0)("ROC01_EmelP").ToString
                txtNegara.Text = dt.Rows(0)("NegaraP").ToString
                txtNegeri.Text = dt.Rows(0)("NegeriP").ToString

                txtAlamat1Caw.Text = dt.Rows(0)("ROC01_AlmtSM1").ToString
                txtAlamat2Caw.Text = dt.Rows(0)("ROC01_AlmtSM2").ToString
                txtBandarCaw.Text = dt.Rows(0)("ROC01_BandarSM").ToString
                txtPoskodCaw.Text = dt.Rows(0)("ROC01_PoskodSM").ToString
                txtTelp1Caw.Text = dt.Rows(0)("ROC01_Tel1SM").ToString
                txtTelp2Caw.Text = dt.Rows(0)("ROC01_Tel2SM").ToString
                txtFaxCaw.Text = dt.Rows(0)("ROC01_FaksSM").ToString
                txtWebCaw.Text = dt.Rows(0)("ROC01_WebSM").ToString
                txtEmailCaw.Text = dt.Rows(0)("ROC01_EmelSM").ToString
                txtNegaraCaw.Text = dt.Rows(0)("ROC01_NegaraSM").ToString
                txtNegeriCaw.Text = dt.Rows(0)("ROC01_NegeriSM").ToString
                txtStatAktif.Text = dt.Rows(0)("StatAktif").ToString
                txtStatLulus.Text = dt.Rows(0)("StatLulus").ToString
                'ViewState("StatusDok") = IIf(IsDBNull(dt.Rows(0)("StatusDok")), 0, dt.Rows(0)("StatusDok"))
                'ViewState("FlagDaftar") = IIf(IsDBNull(dt.Rows(0)("ROC01_FlagDaftar")), 0, dt.Rows(0)("ROC01_FlagDaftar"))
                ' ViewState("StatusKemaskini") = IIf(IsDBNull(dt.Rows(0)("ROC01_KemaskiniStatus")), False, dt.Rows(0)("ROC01_KemaskiniStatus"))
                txtNoAkaun.Text = dt.Rows(0)("ROC01_NoAkaun").ToString
                txtKodBank.Text = dt.Rows(0)("ROC01_KodBank").ToString + " - " + dt.Rows(0)("NamaBank").ToString

                lblThnAktif.Text = dt.Rows(0)("ROC01_TahunAktif").ToString
                Dim StatusBayar = dt.Rows(0)("ROC01_Bayar")

                If StatusBayar IsNot DBNull.Value Then
                    If CBool(StatusBayar) = True Then
                        lblStatusBayar.Text = "Berjaya"
                    Else
                        lblStatusBayar.Text = "Tidak Berjaya"
                    End If
                End If
            End If
        End Using

        Try


            Using dtStaf = ds.Tables(3)
                If dtStaf.Rows.Count > 0 Then
                    gvStaf.DataSource = dtStaf
                    gvStaf.DataBind()
                Else
                    gvStaf.DataSource = New List(Of String)
                    gvStaf.DataBind()
                End If
            End Using

            Using dtsqlROC02 = ds.Tables(1)
                If dtsqlROC02.Rows.Count > 0 Then
                    gvLesen.DataSource = dtsqlROC02
                    gvLesen.DataBind()
                Else
                    gvLesen.DataSource = New List(Of String)
                    gvLesen.DataBind()
                End If
            End Using

            Using dtMOF = ds.Tables(2)
                If dtMOF.Rows.Count > 0 Then
                    gvSyarikatBidang.DataSource = dtMOF
                    gvSyarikatBidang.DataBind()
                Else
                    gvSyarikatBidang.DataSource = New List(Of String)
                    gvSyarikatBidang.DataBind()
                End If
            End Using

            Using dtCIDB = ds.Tables(4)
                If dtCIDB.Rows.Count > 0 Then
                    GVSyarikatCIDB.DataSource = dtCIDB
                    GVSyarikatCIDB.DataBind()
                Else
                    gvSyarikatCIDB.DataSource = New List(Of String)
                    gvSyarikatCIDB.DataBind()
                End If
            End Using

            Using dtLembPeng = ds.Tables(5)
                If dtLembPeng.Rows.Count > 0 Then
                    gvStafLembPeng.DataSource = dtLembPeng
                    gvStafLembPeng.DataBind()
                Else
                    gvStafLembPeng.DataSource = New List(Of String)
                    gvStafLembPeng.DataBind()
                End If
            End Using

            Using dtPemegangShm = ds.Tables(6)
                If dtPemegangShm.Rows.Count > 0 Then
                    gvStafPemegangSaham.DataSource = dtPemegangShm
                    gvStafPemegangSaham.DataBind()
                Else
                    gvStafPemegangSaham.DataSource = New List(Of String)
                    gvStafPemegangSaham.DataBind()
                End If

            End Using

        Catch ex As Exception

        End Try

    End Sub


    Private Sub fLoadGVStaf()
        '[ROC04_Bil],[ROC01_IDSya],[ROC04_IC],[ROC04_Nama],[ROC04_Jwtn],[ROC04_Jbtn],[ROC04_NoTel],[ROC04_Gelaran],[ROC04_JenisSijil]
        Try

            Dim strSql As String = $"SELECT ROC04_ID,ROC04_NoSya,[ROC01_IDSem],ROC04_Nama, ROC04_Ruj, ROC04_IC,
ROC04_Jwtn,ROC04_Jbtn,ROC04_NoTel,ROC04_Gelaran,ROC04_JnsSijil 
FROM [ROC04_Rujukan] 
WHERE [ROC01_IDSem] = '{hdNoIDSemSya.Value}'"

            Dim ds = dbconn.fselectCommand(strSql)
            ViewState("vsStaf") = ds.Tables(0)

            BindGridViewStaf()

        Catch ex As Exception
            'fErrorLog("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
        End Try
    End Sub

    Private Sub fLoadGVLesen()

        Try
            Dim sqlROC02 As String = $"SELECT [ROC02_ID],[ROC02_NoSya],[KodDaftar],[ROC02_NoDaftar],
cast([ROC02_TkhMula] as Date) as TkhMula,cast([ROC02_TkhTamat] as Date) as TkhTmt,[ROC02_TkhTamat],[ROC01_IDSem]
FROM [ROC02_DaftarSya]
WHERE [ROC01_IDSem] = '{hdNoIDSemSya.Value}';"

            Dim dsSijil = dbconn.fselectCommand(sqlROC02)
            If dsSijil.Tables.Count > 0 Then
                gvLesen.DataSource = dsSijil
                gvLesen.DataBind()
            End If

            fLoadGVSalinanLesen()

        Catch ex As Exception
            'fErrorLog("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' Bind GridViewButiran
    ''' </summary>
    Protected Sub BindGridViewStaf()
        Try
            Dim dt = DirectCast(ViewState("vsStaf"), DataTable)

            gvStaf.DataSource = dt
            gvStaf.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub fBindGVSyarikatBidang()
        Dim strSql As String = $"SELECT ROC03_KodBidang, b.Butiran FROM ROC03_BidangSya a, ROC_Bidang b WHERE ROC03_KodBidang = KodBidang AND [ROC01_IDSem] = '{hdNoIDSemSya.Value}'"

        Dim ds As New DataSet
        ds = dbconn.fselectCommand(strSql)

        gvSyarikatBidang.DataSource = ds
        gvSyarikatBidang.DataBind()

        'gvSyarikatBidang2.DataSource = ds
        'gvSyarikatBidang2.DataBind()

    End Sub

    Private Sub fBindGVSyarikatCIDB()
        Dim sqlROC07 As String = $"SELECT [ROC07_ID],[ROC07_KodKategori],[ROC07_KodKhusus], b.Butiran
FROM [ROC07_KhususCIDB] a, ROC_PengkhususanCIDB b 
WHERE [ROC01_IDSem] = '{hdNoIDSemSya.Value}' AND ROC07_KodKhusus = b.KodKhusus;"


        Dim ds As New DataSet
        ds = dbconn.fselectCommand(sqlROC07)

        gvSyarikatCIDB.DataSource = ds
        gvSyarikatCIDB.DataBind()

        'gvSyarikatCIDB2.DataSource = ds
        'gvSyarikatCIDB2.DataBind()
    End Sub

    Protected Sub lbtnKembali_Click(sender As Object, e As EventArgs) Handles lbtnKembali.Click
        Dim KodSubmenu = Request.QueryString("KodSubMenu")
        If KodSubmenu.Equals("070101") Then
            'Semakan Syarikat
            Response.Redirect($"~/Forms/PENDAFTARAN SYARIKAT/SYARIKAT BERDAFTAR/Semakan_Syarikat_Berdaftar.aspx?KodSub=0701&KodSubMenu={KodSubmenu}")
        ElseIf KodSubmenu.Equals("070102") Then
            'Kelulusan Pendaftaran
            Response.Redirect($"~/Forms/PENDAFTARAN SYARIKAT/SYARIKAT BERDAFTAR/Kelulusan_Pendaftaran_Syarikat.aspx?KodSub=0701&KodSubMenu={KodSubmenu}")

        ElseIf KodSubmenu.Equals("070103") Then
            'Kelulusan Kemaskini
            Response.Redirect($"~/Forms/PENDAFTARAN SYARIKAT/SYARIKAT BERDAFTAR/Kelulusan_Kemaskini_Syarikat.aspx?KodSub=0701&KodSubMenu={KodSubmenu}")

        ElseIf KodSubmenu.Equals("070104") Then
            'Penamatan
            Response.Redirect($"~/Forms/PENDAFTARAN SYARIKAT/SYARIKAT BERDAFTAR/Penamatan_Syarikat_Berdaftar.aspx?KodSub=0701&KodSubMenu={KodSubmenu}")
        ElseIf KodSubmenu.Equals("020701") OrElse KodSubmenu.Equals("020702") Then
            'Penilaian Harga 
            Dim idNJ = Request.QueryString("NoNJ")
            Response.Redirect($"~/FORMS/Perolehan/Penilaian Harga/MaklumatPenilaianHarga.aspx?KodSub=0207&KodSubMenu={KodSubmenu}&NoNJ={idNJ}")
        ElseIf KodSubmenu.Equals("020603") Then
            'Penilaian Teknikal
            Dim idNJ = Request.QueryString("NoNJ")
            Response.Redirect($"~/FORMS/Perolehan/Penilaian Teknikal/MaklumatPenilaianTeknikalExtra.aspx?KodSub=0206&KodSubMenu={KodSubmenu}&NoNJ={idNJ}")
        ElseIf KodSubmenu.Equals("020801") OrElse KodSubmenu.Equals("020802") Then
            'Pengesyoran
            Dim idNJ = Request.QueryString("NoNJ")
            Response.Redirect($"~/FORMS/Perolehan/Pengesyoran/MaklumatPengesyoran.aspx?KodSub=0208&KodSubMenu={KodSubmenu}&NoNJ={idNJ}")
        ElseIf KodSubmenu.Equals("020903") Then
            'Perlantikan
            Dim idNJ = Request.QueryString("NoNJ")
            Response.Redirect($"~/FORMS/Perolehan/Perlantikan Vendor/MaklumatPerlantikanExtra.aspx?KodSub=0209&KodSubMenu={KodSubmenu}&NoNJ={idNJ}")
        End If

    End Sub

    Protected Sub btnNextView1_Click(sender As Object, e As EventArgs) Handles lbtnNextView1.Click
        mvSyarikat.SetActiveView(View2)
    End Sub

    Protected Sub btnNextView2_Click(sender As Object, e As EventArgs) Handles lbtnNextView2.Click
        mvSyarikat.SetActiveView(View3)
    End Sub

    Protected Sub lbtnPrevView2_Click(sender As Object, e As EventArgs) Handles lbtnPrevView2.Click
        mvSyarikat.SetActiveView(View1)
    End Sub


    Protected Sub lbtnPrevView4_Click(sender As Object, e As EventArgs) Handles lbtnPrevView4.Click
        mvSyarikat.SetActiveView(View3)
    End Sub

    Protected Sub lbtnPrevView3_Click(sender As Object, e As EventArgs) Handles lbtnPrevView3.Click
        mvSyarikat.SetActiveView(View2)
    End Sub

    Protected Sub lbtnNextView3_Click(sender As Object, e As EventArgs) Handles lbtnNextView3.Click
        mvSyarikat.SetActiveView(View4)
    End Sub

    Protected Sub lbtnNextView2_Click(sender As Object, e As EventArgs) Handles lbtnNextView2.Click
        mvSyarikat.SetActiveView(View3)
    End Sub

    Protected Sub lbtnNextView1_Click(sender As Object, e As EventArgs) Handles lbtnNextView1.Click

        mvSyarikat.SetActiveView(View2)
    End Sub

    Private Sub fLoadGVSalinanLesen()
        Try

            Dim str = $"SELECT a.ROC02_ID, KodDaftar, ROC02_NoDaftar, a.[ROC01_IDSem], [ROC09_ID], [ROC09_NamaDok]
FROM ROC02_DaftarSya a
LEFT JOIN ROC09_Lampiran b 
ON  a.ROC01_IDSem = b.ROC01_IDSem AND a.ROC02_ID = b.ROC_IDFK
WHERE a.ROC01_IDSem='{hdNoIDSemSya.Value}' AND (ROC09_StatusDelete=0 OR ROC09_StatusDelete IS NULL)"

            Dim ds = dbconn.fselectCommand(str)
            If ds IsNot Nothing Then
                Dim dt = ds.Tables(0)
                If dt.Rows.Count > 0 Then
                    For Each row In dt.Rows
                        If IsDBNull(row("ROC09_ID")) Then
                            row("ROC09_ID") = 0
                            row("ROC09_NamaDok") = ""
                        End If
                    Next
                    gvSijilLesen.DataSource = dt
                    gvSijilLesen.DataBind()
                Else
                    gvSijilLesen.DataSource = New List(Of ListItem)
                    gvSijilLesen.DataBind()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub fLoadGVSalinanSijilStaf()
        Try

            Dim str = $"SELECT a.ROC04_ID, a.ROC04_Nama, a.ROC04_JnsSijil, a.[ROC01_IDSem], [ROC09_ID], [ROC09_NamaDok]
FROM ROC04_Rujukan a
LEFT JOIN ROC09_Lampiran b 
ON  a.ROC01_IDSem = b.ROC01_IDSem AND a.ROC04_ID = b.ROC_IDFK
WHERE a.ROC01_IDSem='{hdNoIDSemSya.Value}' AND (ROC09_StatusDelete=0 OR ROC09_StatusDelete IS NULL)"

            Dim ds = dbconn.fselectCommand(str)
            If ds IsNot Nothing Then
                Dim dt = ds.Tables(0)
                If dt.Rows.Count > 0 Then
                    For Each row In dt.Rows
                        If IsDBNull(row("ROC09_ID")) Then
                            row("ROC09_ID") = 0
                            row("ROC09_NamaDok") = ""
                        End If
                    Next
                    gvLampSijilStaf.DataSource = dt
                    gvLampSijilStaf.DataBind()
                Else
                    gvLampSijilStaf.DataSource = New List(Of ListItem)
                    gvLampSijilStaf.DataBind()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub



    Private Sub fLoadGVSalinanGST()
        Try

            Dim str = $"SELECT [ROC01_IDSem], [ROC09_ID], [ROC09_NamaDok]
FROM  ROC09_Lampiran
WHERE ROC01_IDSem='{hdNoIDSemSya.Value}' AND ROC09_JenisDok='Gst' AND ROC09_StatusDelete = 0"

            Dim ds = dbconn.fselectCommand(str)
            If ds IsNot Nothing Then
                Dim dt = ds.Tables(0)
                If dt.Rows.Count > 0 Then
                    gvGST.DataSource = dt
                    gvGST.DataBind()
                Else
                    gvGST.DataSource = New List(Of ListItem)
                    gvGST.DataBind()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lbtnHantar_Click(sender As Object, e As EventArgs) Handles lbtnHantar.Click
        Dim KodSubmenu = Request.QueryString("KodSubMenu")
        Dim strSql, kodlulus As String
        Dim dr As DataRow
        Dim rUpdate = False
        Dim rCommit = False
        Dim paramSql() As SqlParameter = {}
        Dim tarikhNow = Now
        Dim norocBaru = fJanaNoROC()
        Dim ulasan = ""
        Dim dbconn As New DBKewConn
        dbconn.sConnBeginTrans()

        If KodSubmenu.Equals("070102") Then
            'Kelulusan Pendaftaran
            kodlulus = rbKelulusanDaftar.SelectedValue

            strSql = $"SELECT ROC01_IdSem, ROC01_KodLulus, ROC01_KodAktif, ROC01_TkhLulus, ROC01_IDSya From ROC01_Syarikat 
    WHERE ROC01_IdSem = '{hdNoIDSemSya.Value}';"

            Using dsMohon = dbconn.fSelectCommand(strSql, "Sya", True)
                If dsMohon.Tables(0).Rows.Count > 0 Then
                    dr = dsMohon.Tables(0).Rows(0)
                    dr("ROC01_KodLulus") = kodlulus

                    If kodlulus.Equals("1") Then
                        'Lulus
                        dr("ROC01_IDSya") = norocBaru
                        dr("ROC01_TkhLulus") = tarikhNow
                        dr("ROC01_KodAktif") = "01"
                    End If
                End If
                dbconn.sUpdateCommand(dsMohon, strSql, rUpdate, False)
            End Using

            If rUpdate Then
                '1. Insert StatusDok
                strSql = $"INSERT INTO ROC10_StatusDok VALUES (@IdSEM,@NoSya,@KodLulus,@noId,@tarikh,@ulasan);"

                If txtUlasan.Text.Equals(String.Empty) Then
                    ulasan = "LULUS Pendaftaran Syarikat"
                Else
                    ulasan = txtUlasan.Text.Trim
                End If
                paramSql = {
                        New SqlParameter("@NoSya", hdNoSya.Value),
                        New SqlParameter("@IdSEM", hdNoIDSemSya.Value),
                        New SqlParameter("@KodLulus", kodlulus),
                        New SqlParameter("@noId", Session("ssusrID")),
                        New SqlParameter("@tarikh", tarikhNow),
                        New SqlParameter("@ulasan", ulasan)
                        }
                dbconn.sUpdateCommand2(strSql, paramSql, True, True, rUpdate, rCommit)

                If rCommit Then
                    UpdateNoAkhirROC()
                    fGlobalAlert("Rekod telah dihantar!", Me.Page, Me.[GetType](), $"../../PENDAFTARAN SYARIKAT/SYARIKAT BERDAFTAR/Kelulusan_Pendaftaran_Syarikat.aspx?KodSub=0701&KodSubMenu={KodSubmenu}")
                End If
            Else
                fGlobalAlert("Rekod tidak boleh dihantar!. Sila hubungi admin!", Me.Page, Me.GetType())
                Exit Sub
            End If

        ElseIf KodSubmenu.Equals("070103") Then
            'Kelulusan Kemaskini
            kodlulus = rbKelulusanKemaskini.SelectedValue

            strSql = $"SELECT ROC01_IdSem, ROC01_KodLulus, ROC01_TkhSahKemaskini, ROC01_IDSya, ROC01_KodAktif From ROC01_Syarikat 
    WHERE ROC01_IdSem = '{hdNoIDSemSya.Value}';"

            Using dsMohon = dbconn.fSelectCommand(strSql, "Sya", True)
                If dsMohon.Tables(0).Rows.Count > 0 Then
                    dr = dsMohon.Tables(0).Rows(0)
                    dr("ROC01_KodLulus") = kodlulus

                    If kodlulus.Equals("1") Then
                        'Lulus
                        dr("ROC01_KodAktif") = "01"
                        dr("ROC01_TkhSahKemaskini") = tarikhNow
                    End If
                End If
                dbconn.sUpdateCommand(dsMohon, strSql, rUpdate, False)
            End Using

            If rUpdate Then
                '1. Insert StatusDok
                strSql = $"INSERT INTO ROC10_StatusDok VALUES (@IdSEM,@NoSya,@KodLulus,@noId,@tarikh,@ulasan);"

                If txtUlasan.Text.Equals(String.Empty) Then
                    ulasan = "LULUS Kemaskini Syarikat"
                Else
                    ulasan = txtUlasan.Text.Trim
                End If

                paramSql = {
                        New SqlParameter("@NoSya", hdNoSya.Value),
                        New SqlParameter("@IdSEM", hdNoIDSemSya.Value),
                        New SqlParameter("@KodLulus", kodlulus),
                        New SqlParameter("@noId", Session("ssusrID")),
                        New SqlParameter("@tarikh", tarikhNow),
                        New SqlParameter("@ulasan", ulasan)
                        }
                dbconn.sUpdateCommand2(strSql, paramSql, True, True, rUpdate, rCommit)
                If rCommit Then
                    fGlobalAlert("Rekod telah dihantar!", Me.Page, Me.[GetType](), $"../../PENDAFTARAN SYARIKAT/SYARIKAT BERDAFTAR/Kelulusan_Kemaskini_Syarikat.aspx?KodSub=0701&KodSubMenu={KodSubmenu}")

                End If
            Else
                fGlobalAlert("Rekod tidak boleh dihantar!. Sila hubungi admin!", Me.Page, Me.GetType())
                Exit Sub
            End If
        ElseIf KodSubmenu.Equals("070104") Then
            'Penamatan

            kodlulus = ddlPenamatan.SelectedValue

            strSql = $"SELECT ROC01_IdSem, ROC01_KodAktif From ROC01_Syarikat WHERE ROC01_IdSem = '{hdNoIDSemSya.Value}';"

            Using dsMohon = dbconn.fSelectCommand(strSql, "Sya", True)
                If dsMohon.Tables(0).Rows.Count > 0 Then
                    dr = dsMohon.Tables(0).Rows(0)
                    dr("ROC01_KodAktif") = kodlulus
                End If
                dbconn.sUpdateCommand(dsMohon, strSql, rUpdate, False)
            End Using

            If rUpdate Then
                '1. Insert StatusDok
                strSql = $"INSERT INTO ROC10_StatusDok VALUES (@IdSEM,@NoSya,@KodLulus,@noId,@tarikh,@ulasan);"
                paramSql = {
                        New SqlParameter("@NoSya", hdNoSya.Value),
                        New SqlParameter("@IdSEM", hdNoIDSemSya.Value),
                        New SqlParameter("@KodLulus", kodlulus),
                        New SqlParameter("@noId", Session("ssusrID")),
                        New SqlParameter("@tarikh", tarikhNow),
                        New SqlParameter("@ulasan", txtUlasan.Text)
                        }
                dbconn.sUpdateCommand2(strSql, paramSql, True, True, rUpdate, rCommit)
                If rCommit Then
                    fGlobalAlert("Rekod telah dihantar!", Me.Page, Me.[GetType](), $"../../PENDAFTARAN SYARIKAT/SYARIKAT BERDAFTAR/Penamatan_Syarikat_Berdaftar.aspx?KodSub=0701&KodSubMenu={KodSubmenu}")

                End If
            Else
                fGlobalAlert("Rekod tidak boleh dihantar!. Sila hubungi admin!", Me.Page, Me.GetType())
                Exit Sub
            End If

        End If
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
        Dim ds = dbconn.fSelectCommand(strSql, "MKNoAkhir")

        If ds.Tables(0).Rows.Count > 0 Then
            ds.Tables("MKNoAkhir").Rows(0)("NoAkhir") = ds.Tables("MKNoAkhir").Rows(0)("NoAkhir") + 1
        Else
            Dim dr As DataRow
            dr = ds.Tables("MKNoAkhir").NewRow
            dr("KodModul") = "ROC"
            dr("Prefix") = "RC"
            dr("noakhir") = 1
            dr("Butiran") = "ROC RC"
            dr("kodPTJ") = ""
            ds.Tables("MKNoAkhir").Rows.Add(dr)
        End If

        dbconn.sUpdateCommand(ds, strSql)
    End Sub

    Protected Sub rbKelulusanDaftar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rbKelulusanDaftar.SelectedIndexChanged
        If rbKelulusanDaftar.SelectedValue = "1" Then
            trUlasan.Visible = False
            rfvUlasan.Enabled = False
        Else
            trUlasan.Visible = True
            rfvUlasan.Enabled = True
        End If
    End Sub

    Protected Sub rbKelulusanKemaskini_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rbKelulusanKemaskini.SelectedIndexChanged
        If rbKelulusanKemaskini.SelectedValue = "1" Then
            trUlasan.Visible = False
            rfvUlasan.Enabled = False
        Else
            trUlasan.Visible = True
            rfvUlasan.Enabled = True
        End If
    End Sub

    Protected Sub lbtnPerubahan_Click(sender As Object, e As EventArgs) Handles lbtnPerubahan.Click
        BindGridViewSenarai()
        MPESenarai.Show()
    End Sub

    Protected Sub gvSenarai_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvSenarai.Sorting
        Dim sortedView As New DataView(fCreateDtSenarai())
        sortedView.Sort = e.SortExpression & " " & GetSortDirection(e.SortExpression)
        Session("SortedView") = sortedView
        gvSenarai.DataSource = sortedView
        gvSenarai.DataBind()
    End Sub

    Protected Sub gvSenarai_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvSenarai.PageIndexChanging
        gvSenarai.PageIndex = e.NewPageIndex

        If Session("SortedView") IsNot Nothing Then
            gvSenarai.DataSource = Session("SortedView")
            gvSenarai.DataBind()
        Else
            BindGridViewSenarai()
        End If
    End Sub

    Protected Sub ddlSaizRekodSenarai_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSaizRekodSenarai.SelectedIndexChanged
        gvSenarai.PageSize = CInt(ddlSaizRekodSenarai.SelectedValue)
        BindGridViewSenarai()
    End Sub

    Private Sub BindGridViewSenarai()
        gvSenarai.DataSource = fCreateDtSenarai()
        gvSenarai.DataBind()
    End Sub

    Private Function fCreateDtSenarai() As DataTable
        Dim str1 = $"select ROC10_Tkh, ROC10_Ulasan from ROC10_StatusDok Where ROC10_NoID1='{hdNoIDSemSya.Value}' order by ROC10_Tkh desc"

        Dim dtBil = dbconn.fSelectCommandDt(str1)

        lblJumRekodSenarai.InnerText = dtBil.Rows.Count
        Return dtBil
    End Function

    Private Function GetSortDirection(ByVal column As String) As String

        ' By default, set the sort direction to ascending.
        Dim sortDirection = "ASC"

        ' Retrieve the last column that was sorted.
        Dim sortExpression = TryCast(ViewState("SortExpression"), String)

        If sortExpression IsNot Nothing Then
            ' Check if the same column is being sorted.
            ' Otherwise, the default value can be returned.
            If sortExpression = column Then
                Dim lastDirection = TryCast(ViewState("SortDirection"), String)
                If lastDirection IsNot Nothing AndAlso lastDirection = "ASC" Then
                    sortDirection = "DESC"
                End If
            End If
        End If

        ' Save new values in ViewState.
        ViewState("SortDirection") = sortDirection
        ViewState("SortExpression") = column

        Return sortDirection

    End Function
End Class

