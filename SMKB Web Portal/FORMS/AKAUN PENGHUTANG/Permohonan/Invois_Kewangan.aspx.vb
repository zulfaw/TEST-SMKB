Imports System.Data.SqlClient
Imports System.Globalization
Imports System.IO
Imports System.Web.Configuration

Public Class Invois_Cukai_Kewangan1
    Inherits System.Web.UI.Page
    Dim sLogBaru As String
    Dim sLogMedan As String
    Dim strNoStaf As String = UserInfo.strSesStaffID
    Dim sLogLevel As String = UserInfo.strUsrTahap
    Dim lsLogUsrIP As String = UserInfo.strUsrPcIP
    Dim lsLogUsrPC As String = UserInfo.strUsrPCName

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Try
                Session("sesKodSubMenu") = Request.QueryString("KodSubMenu")
                txtTkhMohon.Text = Now.ToString("dd/MM/yyyy")

                Dim strStaffID = Session("ssusrID")

                Dim strKodSubMenu As String = Request.QueryString("KodSubMenu")
                'ViewState("KodSubMenu") = strKodSubMenu
                If fCheckPowerUser(strStaffID, strKodSubMenu) Then
                    ViewState("PowerUser") = True
                Else
                    ViewState("PowerUser") = False
                End If

                fBindDdlBank()
                fBindDdlKategori()
                fBindJenisUrusniaga()
                fBindDdlNegara()
                fBindDdlNegeri()
                fBindDdlJenTemp()

                fClearGvInv()
                fLoadKW()
                fLoadKO2()
                fLoadPTj2()
                fLoadKP2()
                fLoadVot2()
                SetInitialRow()

            Catch ex As Exception

            End Try

        Else
            ClientScript.RegisterHiddenField("isPostBack", "1")
        End If
    End Sub

    Private Sub fBindDdlJenTemp()
        Try
            Dim strSql As String

            strSql = "select Kod, Butiran from PO_Tempoh order by Kod"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)
            ddlJenTemp.DataSource = ds
            ddlJenTemp.DataTextField = "Butiran"
            ddlJenTemp.DataValueField = "Kod"
            ddlJenTemp.DataBind()

            ddlJenTemp.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlJenTemp.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindDdlBank()
        Try
            Dim strSql As String

            strSql = "select KodBank, (KodBank + ' - ' + NamaBank) as NamaBank from MK_Bank where kodbank ='76106' order by kodbank "

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)
            ddlBank.DataSource = ds
            ddlBank.DataTextField = "NamaBank"
            ddlBank.DataValueField = "KodBank"
            ddlBank.DataBind()

            ddlBank.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindDdlKategori()
        Try
            Dim strSql As String

            strSql = "SELECT Kod,(Kod + ' - ' + Butiran ) as Butiran from mk_kategoripenerima where (kod <> 'KJ' and kod <> 'PG' and kod <> 'UG' and kod <> 'PL')"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable

            ds = dbconn.fSelectCommand(strSql)
            ddlKat.DataSource = ds
            ddlKat.DataTextField = "Butiran"
            ddlKat.DataValueField = "Kod"
            ddlKat.DataBind()

            ddlKat.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlKat.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindJenisUrusniaga()
        Try
            Dim strsql As String
            strsql = "Select KodUrusniaga ,(CONVERT (nvarchar, KodUrusniaga) + ' - ' +  Butiran) as Butiran 
from RC_Urusniaga 
where KodUrusniaga in (4,6,7,8,11,14,15,16,17) 
order by KodUrusniaga"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strsql)
            ddlJenis.DataSource = ds
            ddlJenis.DataTextField = "Butiran"
            ddlJenis.DataValueField = "KodUrusniaga"
            ddlJenis.DataBind()

            ddlJenis.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlJenis.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindDdlNegeri()
        Try
            Dim strsql As String
            strsql = "select KodNegeri , (KodNegeri + ' - ' + Butiran ) as Butiran from MK_Negeri order by KodNegeri"

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
    Private Sub fBindDdlNegara()
        Try
            Dim strsql As String
            strsql = "select KodNegara , (KodNegara + ' - ' + Butiran ) as Butiran from MK_Negara order by KodNegara "

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



    Private Sub fBindOA()
        Try
            Dim strsql As String
            strsql = "select IDNo_KP ,IDNama  from MK_Orang_Awam order by IDNama asc "

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strsql)

            ddlNmPenerima.DataSource = ds.Tables(0)
            ddlNmPenerima.DataTextField = "IDNama"
            ddlNmPenerima.DataValueField = "IDNo_KP"
            ddlNmPenerima.DataBind()

            ddlNmPenerima.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlNmPenerima.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindStaff()
        Dim ds As New DataSet
        Dim dbconnSMSM As New DBSMConn()
        Try

            Dim strSQLSMSM = "select MS01_NoStaf, (MS01_NoStaf + ' - ' + MS01_Nama) as NamaStaf from MS01_Peribadi Where MS01_Status = '1' Order by MS01_Nama"
            '"Select a.MS01_Nama, a.MS01_NoStaf as NoStaf, c.JawGiliran,(a.MS01_NoStaf + '-' + a.MS01_Nama) as Butiran
            '             FROM MS01_Peribadi a inner join  ms08_penempatan e On a.ms01_nostaf = e.ms01_nostaf And e.MS08_StaTerkini = 1, MS02_Perjawatan b, MS_Jawatan c
            '             Where 1 = 1
            '             And b.MS01_NoStaf = a.MS01_NoStaf
            '             And c.KodJawatan = b.MS02_JawSandang
            '             And b.ms02_kumpjawatan = '1'
            '             ORDER BY a.MS01_Nama"
            ds = dbconnSMSM.fselectCommand(strSQLSMSM)

            ddlNmPenerima.DataSource = ds
            ddlNmPenerima.DataTextField = "NamaStaf"
            ddlNmPenerima.DataValueField = "MS01_NoStaf"
            ddlNmPenerima.DataBind()

            ddlNmPenerima.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlNmPenerima.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindDdlSyarikat()
        Try
            Dim strsql As String
            strsql = "select ROC01_IDSYA, (ROC01_IDSYA + ' - ' + ROC01_NamaSya ) as NamaSyarikat  from ROC01_Syarikat WHERE ROC01_KodLulus = '1' AND ROC01_KodAktif = '01' ORDER BY ROC01_NamaSya"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn

            ds = dbconn.fSelectCommand(strsql)
            ddlNmPenerima.DataSource = ds.Tables(0)
            ddlNmPenerima.DataTextField = "NamaSyarikat"
            ddlNmPenerima.DataValueField = "ROC01_IDSYA"
            ddlNmPenerima.DataBind()

            ddlNmPenerima.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlNmPenerima.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub



    Private Sub ddlKat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKat.SelectedIndexChanged
        Try
            'ddlNmPenerima.Items.Clear()
            'If ddlKat.SelectedValue = "OA" Then
            '    fBindOA()

            'ElseIf ddlKat.SelectedValue = "ST" Then
            '    fBindStaff()

            'ElseIf ddlKat.SelectedValue = "SY" Then
            '    fBindDdlSyarikat()

            'End If

            Try

                If ddlKat.SelectedValue = "SY" Then
                    rbKontrak.Enabled = True
                Else
                    rbKontrak.SelectedValue = "False"
                    rbKontrak.Enabled = False
                End If



                ddlNmPenerima.Items.Clear()
                txtIDPenerima.Text = ""
                txtNamaPenerima.Text = ""
                sLoadPenghutang(Trim(ddlKat.SelectedValue.TrimEnd))
            Catch ex As Exception

            End Try


        Catch ex As Exception

        End Try
    End Sub

    Private Sub sLoadPenghutang(strKat As String)
        Try
            Dim ds As DataSet
            ds = fLoadPenghutang(strKat)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    ddlNmPenerima.DataSource = ds.Tables(0)
                    ddlNmPenerima.DataTextField = "Penghutang"
                    ddlNmPenerima.DataValueField = "IdPenghutang"
                    ddlNmPenerima.DataBind()

                    ddlNmPenerima.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
                    ddlNmPenerima.SelectedIndex = 0

                    Dim dt As DataTable = ds.Tables(0)
                    ViewState("dtPenghutang") = dt
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlNmPenerima_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNmPenerima.SelectedIndexChanged
        Try

            Dim strIdPenghutang As String = Trim(ddlNmPenerima.SelectedValue.TrimEnd)

            Dim dtPenghutang As DataTable = ViewState("dtPenghutang")
            Dim dvPenghutang As New DataView(dtPenghutang)

            dvPenghutang.RowFilter = "IdPenghutang = '" & strIdPenghutang & "'"

            txtIDPenerima.Text = dvPenghutang.Item(0)("IdPenghutang").ToString
            txtKodPenghutang.Text = dvPenghutang.Item(0)("KodPenghutang").ToString
            txtAlmt1.Value = dvPenghutang.Item(0)("Alamat1").ToString
            txtAlmt2.Value = dvPenghutang.Item(0)("Alamat2").ToString
            txtBandar.Value = dvPenghutang.Item(0)("Bandar").ToString
            txtPoskod.Value = dvPenghutang.Item(0)("Poskod").ToString
            ddlNegeri.SelectedValue = dvPenghutang.Item(0)("KodNegeri").ToString
            ddlNegara.SelectedValue = dvPenghutang.Item(0)("KodNegara").ToString
            txtNoTel.Value = dvPenghutang.Item(0)("NoTel").ToString
            txtNoFax.Value = dvPenghutang.Item(0)("NoFax").ToString
            txtEmel.Value = dvPenghutang.Item(0)("Emel").ToString
            txtPerhatian.Value = IIf(IsDBNull(dvPenghutang.Item(0)("Perhatian").ToString), "-", dvPenghutang.Item(0)("Perhatian").ToString)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub AlamatOA()
        Try
            Dim dbconn As New DBKewConn
            Dim strSql As String
            Dim idOA As String = Trim(ddlNmPenerima.SelectedValue.TrimEnd)

            strSql = "select alamat1, alamat2, kodnegara, kodnegeri,bandar,poskod,no_tel,no_fax,emel from mk_orang_awam where idno_kp='" & idOA & "'"

            Dim ds As New DataSet
            ds = dbconn.fSelectCommand(strSql)

            If ds.Tables(0).Rows.Count > 0 Then
                txtAlmt1.Value = ds.Tables(0).Rows(0)("alamat1").ToString
                txtAlmt2.Value = ds.Tables(0).Rows(0)("alamat2").ToString
                txtBandar.Value = ds.Tables(0).Rows(0)("Bandar").ToString
                txtPoskod.Value = ds.Tables(0).Rows(0)("Poskod").ToString
                ddlNegara.SelectedValue = ds.Tables(0).Rows(0)("KodNegara").ToString
                fBindDdlNegeri()
                ddlNegeri.SelectedValue = ds.Tables(0).Rows(0)("KodNegeri").ToString
                txtNoTel.Value = ds.Tables(0).Rows(0)("No_Tel").ToString
                txtNoFax.Value = ds.Tables(0).Rows(0)("No_Fax").ToString
                txtEmel.Value = ds.Tables(0).Rows(0)("Emel").ToString
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub AlamatStaff()
        Dim dbconn As New DBKewConn
        Dim dbconnSMSM As New DBSMConn()
        Try
            Dim strSql As String
            Dim idstaf As String = Trim(txtIDPenerima.Text.TrimEnd)

            Dim strSQLSMSM = "SELECT a.MS01_NoStaf, a.MS01_Nama , a.MS01_KpB,b.MS08_Unit,a.ms01_email 
                      FROM MS01_Peribadi a,MS08_Penempatan b WHERE b.MS08_StaTerkini = '1' AND a.MS01_NoStaf=b.MS01_NoStaf 
                      AND a.MS01_NoStaf =  '" & idstaf & "'"

            Dim ds As New DataSet
            ds = dbconnSMSM.fselectCommand(strSQLSMSM)

            If Not ds Is Nothing Then
                strSql = "Select Singkatan from MK_Ptj where KodPTJ = '" & ds.Tables(0).Rows(0)("MS08_Unit").ToString & "'"

                Dim ds2 As New DataSet
                ds2 = dbconn.fSelectCommand(strSql)

                If ds2.Tables(0).Rows.Count > 0 Then
                    txtAlmt1.Value = ds2.Tables(0).Rows(0)("Singkatan").ToString
                Else
                    txtAlmt1.Value = "-"
                End If
                txtEmel.Value = ds.Tables(0).Rows(0)("ms01_email").ToString
            End If

            strSql = "SELECT * FROM MK_Korporat"

            Dim ds3 As New DataSet
            ds3 = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds3.Tables(0).Rows.Count > 0 Then
                    txtAlmt2.Value = ds3.Tables(0).Rows(0)("Nama").ToString
                    txtBandar.Value = ds3.Tables(0).Rows(0)("Bandar").ToString
                    txtPoskod.Value = ds3.Tables(0).Rows(0)("Poskod").ToString
                    ddlNegara.SelectedValue = ds3.Tables(0).Rows(0)("KodNegara").ToString
                    fBindDdlNegeri()
                    ddlNegeri.SelectedValue = ds3.Tables(0).Rows(0)("KodNegeri").ToString
                    txtNoTel.Value = ds3.Tables(0).Rows(0)("NoTel1").ToString

                End If
            End If

        Catch ex As Exception

        End Try
    End Sub
    Function AlamatSyarikat()
        Try
            Dim strSql As String
            Dim IDSya As String = Trim(txtIDPenerima.Text.TrimEnd)

            strSql = "Select ROC01_AlmtP1,ROC01_AlmtP2,ROC01_BandarP,ROC01_PoskodP,ROC01_NegeriP,ROC01_NegaraP,ROC01_Tel1P,ROC01_FaksP,ROC01_EmelP,isnull(ROC01_WakilSya,'-') as ROC01_WakilSya from ROC01_Syarikat where ROC01_IDSya = '" & IDSya & "'"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then

                    txtAlmt1.Value = ds.Tables(0).Rows(0)("ROC01_AlmtP1").ToString
                    txtAlmt2.Value = ds.Tables(0).Rows(0)("ROC01_AlmtP2").ToString
                    txtBandar.Value = ds.Tables(0).Rows(0)("ROC01_BandarP").ToString
                    txtPoskod.Value = ds.Tables(0).Rows(0)("ROC01_PoskodP").ToString
                    ddlNegara.SelectedValue = ds.Tables(0).Rows(0)("ROC01_NegaraP").ToString
                    fBindDdlNegeri()
                    ddlNegeri.SelectedValue = ds.Tables(0).Rows(0)("ROC01_NegeriP").ToString
                    txtNoTel.Value = ds.Tables(0).Rows(0)("ROC01_Tel1P").ToString
                    txtNoFax.Value = ds.Tables(0).Rows(0)("ROC01_FaksP").ToString
                    txtEmel.Value = ds.Tables(0).Rows(0)("ROC01_EmelP").ToString
                    txtPerhatian.Value = ds.Tables(0).Rows(0)("ROC01_WakilSya").ToString

                End If

            End If
        Catch ex As Exception

        End Try
    End Function

    Private Sub fClearGvInv()
        Try
            gvInvDt.DataSource = New List(Of String)
            gvInvDt.DataBind()

        Catch ex As Exception

        End Try
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
            'ViewState("dsKO") = ds

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
            'ViewState("dsPTj") = ds

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
            ' ViewState("dsKP") = ds

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
            'ViewState("dsVot") = ds

        Catch ex As Exception

        End Try
    End Sub

    Private Sub SetInitialRow()
        Try
            Dim dt As DataTable = New DataTable()
            Dim dr As DataRow = Nothing
            dt.Columns.Add(New DataColumn("AR01_BilDtID", GetType(String)))
            dt.Columns.Add(New DataColumn("KodKw", GetType(String)))
            dt.Columns.Add(New DataColumn("KodKO", GetType(String)))
            dt.Columns.Add(New DataColumn("KodPTJ", GetType(String)))
            dt.Columns.Add(New DataColumn("KodKP", GetType(String)))
            dt.Columns.Add(New DataColumn("KodVot", GetType(String)))
            dt.Columns.Add(New DataColumn("AR01_Perkara", GetType(String)))
            dt.Columns.Add(New DataColumn("AR01_Kuantiti", GetType(String)))
            dt.Columns.Add(New DataColumn("AR01_kadarHarga", GetType(String)))
            dt.Columns.Add(New DataColumn("AR01_Jumlah", GetType(String)))

            dr = dt.NewRow()
            dr("AR01_BilDtID") = String.Empty
            dr("KodKw") = String.Empty
            dr("KodKO") = String.Empty
            dr("KodPTJ") = String.Empty
            dr("KodKP") = String.Empty
            dr("KodVot") = String.Empty
            dr("AR01_Perkara") = String.Empty
            dr("AR01_Kuantiti") = String.Empty
            dr("AR01_kadarHarga") = String.Empty
            dr("AR01_Jumlah") = String.Empty

            dt.Rows.Add(dr)
            ViewState("dtInvDt") = dt
            gvInvDt.DataSource = dt
            gvInvDt.DataBind()

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ddlKW_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try

            Dim gvr As GridViewRow = CType(((CType(sender, Control)).NamingContainer), GridViewRow)
            Dim ddlKW As DropDownList = CType(gvr.FindControl("ddlKW"), DropDownList)
            Dim strKodKW As String = ddlKW.SelectedItem.Value

            Dim ddlKO = DirectCast(gvr.FindControl("ddlKO"), DropDownList)
            ddlKO.DataSource = fLoadKO(strKodKW)   'ViewState("dsKO")
            ddlKO.DataTextField = "Butiran"
            ddlKO.DataValueField = "KodKO"
            ddlKO.DataBind()

            ddlKO.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlKO.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Function fLoadKO(ByVal strKodKW As String) As DataSet
        Try

            'Dim strSql As String = "SELECT DISTINCT TOP 100 PERCENT dbo.MK_kodOperasi.KodKO, (dbo.MK_kodOperasi.KodKO + ' - ' + dbo.MK_kodOperasi.Butiran) as ButiranKO " &
            '    "FROM dbo.MK_kodOperasi INNER JOIN dbo.MK01_VotTahun ON dbo.MK_kodOperasi.KodKO = dbo.MK01_VotTahun.KodKO " &
            '    "where dbo.MK01_VotTahun.MK01_Tahun = '" & Now.Year & "' and dbo.mk01_votTahun.KodKw = '" & ddlKW.SelectedValue & "' and dbo.mk01_votTahun.KodPTj = '" & ddlPTJ.SelectedValue & "' ORDER BY dbo.MK_kodOperasi.Kodko"



            'Dim strSql As String = "SELECT DISTINCT TOP 100 PERCENT dbo.MK_kodOperasi.KodKO, (dbo.MK_kodOperasi.KodKO + ' - ' + dbo.MK_kodOperasi.Butiran) as ButiranKO " &
            '    "FROM dbo.MK_kodOperasi INNER JOIN dbo.MK01_VotTahun ON dbo.MK_kodOperasi.KodKO = dbo.MK01_VotTahun.KodKO " &
            '    "where dbo.MK01_VotTahun.MK01_Tahun = '" & Now.Year & "' ORDER BY dbo.MK_kodOperasi.Kodko"

            Dim strSql As String = "SELECT DISTINCT TOP 100 PERCENT dbo.MK_kodOperasi.KodKO, (dbo.MK_kodOperasi.KodKO + ' - ' + dbo.MK_kodOperasi.Butiran) as Butiran " &
                "FROM dbo.MK_kodOperasi INNER JOIN dbo.MK01_VotTahun ON dbo.MK_kodOperasi.KodKO = dbo.MK01_VotTahun.KodKO " &
                "where dbo.MK01_VotTahun.MK01_Tahun = YEAR(GETDATE()) and dbo.mk01_votTahun.KodKw = '" & strKodKW & "' ORDER BY dbo.MK_kodOperasi.Kodko"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)
            ViewState("dsKO") = ds
            Return ds

        Catch ex As Exception

        End Try
    End Function

    Protected Sub ddlKO_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try

            Dim gvr As GridViewRow = CType(((CType(sender, Control)).NamingContainer), GridViewRow)
            Dim ddlKW As DropDownList = CType(gvr.FindControl("ddlKW"), DropDownList)
            Dim strKodKW As String = ddlKW.SelectedItem.Value

            Dim ddlKO As DropDownList = CType(gvr.FindControl("ddlKO"), DropDownList)
            Dim strKodKO As String = ddlKO.SelectedItem.Value

            Dim ddlPTj = DirectCast(gvr.FindControl("ddlPTj"), DropDownList)
            ddlPTj.DataSource = fLoadPTj(strKodKW, strKodKO)   'ViewState("dsKO")
            ddlPTj.DataTextField = "Butiran"
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
            ddlKP.DataSource = fLoadKP(strKodKW, strKodKO, strKodPTj)   'ViewState("dsKO")
            ddlKP.DataTextField = "Butiran"
            ddlKP.DataValueField = "KodKP"
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

            Dim strKodJenUrusniaga = Trim(ddlJenis.SelectedValue.TrimEnd)

            Dim ddlVot = DirectCast(gvr.FindControl("ddlVot"), DropDownList)
            ddlVot.DataSource = fLoadVot(strKodKW, strKodKO, strKodPTj, strKodKP, strKodJenUrusniaga)   'ViewState("dsKO")
            ddlVot.DataTextField = "Butiran"
            ddlVot.DataValueField = "KodVot"
            ddlVot.DataBind()

            ddlVot.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlVot.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub

    Private Function fLoadPTj(ByVal strKodKW As String, ByVal strKodKO As String) As DataSet
        Try


            Dim strSql As String = "SELECT DISTINCT MK_PTJ.KodPTJ  , (MK_PTJ.KodPTJ   + ' - ' + MK_PTJ.Butiran  ) as Butiran  
                    From MK_PTJ INNER Join MK01_VotTahun ON MK_PTJ.KodPTJ   = MK01_VotTahun.KodPTJ   
                    where MK01_VotTahun.MK01_Tahun = YEAR(GETDATE()) and MK01_VotTahun.KodKw = '" & strKodKW & "' and MK01_VotTahun.KodKO = '" & strKodKO & "' and MK_PTJ.KodPTJ <> '-' and MK_PTJ.status = 1 
                    ORDER BY MK_PTJ.KodPTJ "

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)
            ViewState("dsPTj") = ds
            Return ds

        Catch ex As Exception

        End Try
    End Function

    Private Function fLoadKP(ByVal strKodKW As String, ByVal strKodKO As String, ByVal strKodPTj As String) As DataSet
        Try

            Dim strSql As String = "SELECT DISTINCT TOP 100 PERCENT MK_KodProjek.KodProjek as KodKP, (MK_KodProjek .KodProjek  + ' - ' + MK_KodProjek.Butiran ) as Butiran  
                    From MK_KodProjek INNER Join MK01_VotTahun ON MK_KodProjek.KodProjek  = MK01_VotTahun.KodKP
                    where MK01_VotTahun.MK01_Tahun = YEAR(GETDATE()) and MK01_VotTahun.KodKw = '" & strKodKW & "' and MK01_VotTahun.KodKO = '" & strKodKO & "' and MK01_VotTahun .KodPTJ = '" & strKodPTj & "' ORDER BY MK_KodProjek.KodProjek"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)
            ViewState("dsKP") = ds
            Return ds

        Catch ex As Exception

        End Try
    End Function

    Private Function fLoadVot(ByVal strKodKW As String, ByVal strKodKO As String, ByVal strKodPTj As String, ByVal strKodKP As String, ByVal strKodJenUrusniaga As String) As DataSet
        Try

            'Dim strSql As String = "SELECT DISTINCT A.KodVot, (A.KODVOT + ' - ' + B.BUTIRAN) as Butiran FROM MK03_AmTahun A INNER JOIN MK_VOT B ON A.KODVOT = B.KODVOT 
            'Where a.MK03_TAHUN = YEAR(GETDATE()) AND A.KodKw = '" & strKodKW & "' and A.KodKO = '" & strKodKO & "' AND A.KODPTJ = '" & strKodPTj & "' AND A.KodKP  = '" & strKodKP & "' AND  RIGHT(A.KODVOT,2) <> '00' ORDER BY A.KODVOT"

            Dim strSql = "SELECT DISTINCT A.KodVot, (A.KODVOT + ' - ' + B.BUTIRAN) as Butiran FROM MK03_AmTahun A INNER JOIN MK_VOT B ON A.KODVOT = B.KODVOT 
Where a.MK03_TAHUN = YEAR(GETDATE()) AND A.KodKw = '" & strKodKW & "' and A.KodKO = '" & strKodKO & "' AND A.KODPTJ = '" & strKodPTj & "' AND A.KodKP  = '" & strKodKP & "' AND  RIGHT(A.KODVOT,2) <> '00'
AND  A.KODVOT IN (SELECT KodVot FROM AR_VotUrusniaga WHERE KodKW = '" & strKodKW & "' AND KodJenUrusniaga = '" & strKodJenUrusniaga & "')
ORDER BY A.KODVOT"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)
            ViewState("dsVot") = ds
            Return ds

        Catch ex As Exception

        End Try
    End Function

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

    Protected Sub txtJumlah_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim decJumTrans As Decimal
            For Each gvRow As GridViewRow In gvInvDt.Rows
                Dim decJumlah As String = CDec(Trim(CType(gvRow.FindControl("txtJumlah"), TextBox).Text.TrimEnd))
                decJumTrans += decJumlah
            Next

            Dim footerRow = gvInvDt.FooterRow
            CType(footerRow.FindControl("lblTotJum"), Label).Text = FormatNumber(decJumTrans, 2)


        Catch ex As Exception

        End Try
    End Sub
    Private Sub fSetFooter()
        Try
            Dim decJumTrans As Decimal
            For Each gvRow1 As GridViewRow In gvInvDt.Rows
                Dim decJumlah As String = CDec(Trim(CType(gvRow1.FindControl("lblJumlah"), Label).Text.TrimEnd))
                decJumTrans += decJumlah
            Next

            Dim footerRow = gvInvDt.FooterRow
            CType(footerRow.FindControl("lblTotJum"), Label).Text = FormatNumber(decJumTrans, 2)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lbtnTambah_Click(sender As Object, e As EventArgs)
        AddNewRowToGrid()
    End Sub

    Private Sub AddNewRowToGrid()
        Try

            'If fCheckTrans() = False Then
            '    Exit Sub
            'End If

            Dim rowIndex As Integer = 0
            If ViewState("dtInvDt") IsNot Nothing Then
                Dim dtvsDtInv As DataTable = CType(ViewState("dtInvDt"), DataTable)
                Dim drCurrentRow As DataRow = Nothing

                If dtvsDtInv.Rows.Count > 0 Then

                    For i As Integer = 1 To dtvsDtInv.Rows.Count

                        Dim strKW = CType(gvInvDt.Rows(rowIndex).Cells(1).FindControl("ddlKW"), DropDownList).Text
                        Dim strKO = CType(gvInvDt.Rows(rowIndex).Cells(2).FindControl("ddlKO"), DropDownList).Text
                        Dim strPTj = CType(gvInvDt.Rows(rowIndex).Cells(3).FindControl("ddlPTj"), DropDownList).Text
                        Dim strKP = CType(gvInvDt.Rows(rowIndex).Cells(4).FindControl("ddlKP"), DropDownList).Text
                        Dim strVot = CType(gvInvDt.Rows(rowIndex).Cells(5).FindControl("ddlVot"), DropDownList).Text
                        Dim strPerkara = CType(gvInvDt.Rows(rowIndex).Cells(6).FindControl("txtPerkara"), TextBox).Text
                        Dim strKuantiti = CType(gvInvDt.Rows(rowIndex).Cells(7).FindControl("txtKuantiti"), TextBox).Text
                        Dim strHarga = CType(gvInvDt.Rows(rowIndex).Cells(8).FindControl("txtHarga"), TextBox).Text
                        Dim strJumlah = CType(gvInvDt.Rows(rowIndex).Cells(9).FindControl("lblJumlah"), Label).Text

                        dtvsDtInv.Rows(i - 1)("KodKw") = strKW
                        dtvsDtInv.Rows(i - 1)("KodKO") = strKO
                        dtvsDtInv.Rows(i - 1)("KodPTJ") = strPTj
                        dtvsDtInv.Rows(i - 1)("KodKP") = strKP
                        dtvsDtInv.Rows(i - 1)("KodVot") = strVot
                        dtvsDtInv.Rows(i - 1)("AR01_Perkara") = strPerkara
                        dtvsDtInv.Rows(i - 1)("AR01_Kuantiti") = IIf(strKuantiti = "", 0, strKuantiti)
                        dtvsDtInv.Rows(i - 1)("AR01_kadarHarga") = IIf(strHarga = "", 0, strHarga)
                        dtvsDtInv.Rows(i - 1)("AR01_Jumlah") = IIf(strJumlah = "", 0, strJumlah)

                        rowIndex += 1
                    Next

                    drCurrentRow = dtvsDtInv.NewRow()
                    dtvsDtInv.Rows.Add(drCurrentRow)
                    ViewState("dtInvDt") = dtvsDtInv
                    gvInvDt.DataSource = dtvsDtInv
                    gvInvDt.DataBind()
                End If
            Else
                Response.Write("ViewState is null")
            End If

            SetPreviousData()
        Catch ex As Exception

        End Try
    End Sub

    'Private Function fCheckTrans() As Boolean
    '    Try

    '        For Each gvTransInvrow As GridViewRow In gvInvDt.Rows
    '            Dim strKW = TryCast(gvTransInvrow.FindControl("ddlKW"), DropDownList).SelectedValue
    '            Dim strKO = TryCast(gvTransInvrow.FindControl("ddlKO"), DropDownList).SelectedValue
    '            Dim strPTJ = TryCast(gvTransInvrow.FindControl("ddlPTj"), DropDownList).SelectedValue
    '            Dim strKP = TryCast(gvTransInvrow.FindControl("ddlKP"), DropDownList).SelectedValue
    '            Dim strVot = TryCast(gvTransInvrow.FindControl("ddlVot"), DropDownList).SelectedValue
    '            Dim strPerkara = TryCast(gvTransInvrow.FindControl("txtPerkara"), TextBox).Text
    '            Dim strKuantiti = TryCast(gvTransInvrow.FindControl("txtKuantiti"), TextBox).Text
    '            strKuantiti = IIf(strKuantiti = "", 0, strKuantiti)
    '            Dim intKtt As Integer = CInt(strKuantiti)

    '            Dim strHarga = TryCast(gvTransInvrow.FindControl("txtHarga"), TextBox).Text
    '            strHarga = IIf(strHarga = "", 0, strHarga)
    '            Dim decharga As Decimal = CDec(strHarga)

    '            If strKW = "0" OrElse strKO = "0" OrElse strPTJ = "0" OrElse strKP = "0" OrElse strVot = "0" OrElse strPerkara = "" OrElse intKtt = 0 OrElse decharga = 0 Then
    '                Return False
    '            End If
    '        Next
    '        Return True

    '    Catch ex As Exception
    '        Return False
    '    End Try
    'End Function

    Private Sub SetPreviousData()
        Try
            Dim rowIndex As Integer = 0

            If ViewState("dtInvDt") IsNot Nothing Then
                Dim dt As DataTable = CType(ViewState("dtInvDt"), DataTable)

                If dt.Rows.Count > 0 Then

                    For i As Integer = 0 To dt.Rows.Count - 1

                        Dim gvRow As GridViewRow = gvInvDt.Rows(i)
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
                        box3.Text = IIf(strHarga = "", 0, strHarga)
                        box4.Text = IIf(strJumlah = "", 0, strJumlah)

                        rowIndex += 1
                    Next
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Dim decJumlah As Decimal
    Private Sub gvInvDt_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvInvDt.RowDataBound
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

            End If

            If e.Row.RowType = DataControlRowType.Footer Then
                Dim lblTotJum As Label = CType(e.Row.FindControl("lblTotJum"), Label)
                lblTotJum.Text = FormatNumber(decJumlah, 2)

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvInvDt_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvInvDt.RowDeleting
        Try
            Dim rowIndex As Integer = 0
            Dim index As Integer = Convert.ToInt32(e.RowIndex)

            If ViewState("dtInvDt") IsNot Nothing Then
                Dim dtCurrentTable As DataTable = CType(ViewState("dtInvDt"), DataTable)
                Dim drCurrentRow As DataRow = Nothing

                If dtCurrentTable.Rows.Count > 0 Then
                    dtCurrentTable.Rows(index).Delete()
                    dtCurrentTable.AcceptChanges()
                    ViewState("dtInvDt") = dtCurrentTable
                    gvInvDt.DataSource = dtCurrentTable
                    gvInvDt.DataBind()
                End If
            Else
                sCleargvInvDt()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub sCleargvInvDt()
        gvInvDt.DataSource = New List(Of String)
        gvInvDt.DataBind()
    End Sub

    Private Function fSimpanInv() As Boolean
        Dim blnSuccess As Boolean = True
        Dim strNoInvSem
        Dim dbconn As New DBKewConn
        Dim intIdBil As Integer
        Dim strKodSub As String = Request.QueryString("KodSub")
        Dim strKodModul As String = strKodSub.Substring(0, 2)
        Dim folderPath As String
        Dim FileName As String
        Dim trackno As Integer

        Try
            trackno = 1

            Dim strSql As String
            Dim noInvSmtra As String = ViewState("NoMhn")
            Dim strTkhToday As String = Now.ToString("yyyy-MM-dd")
            Dim dtTkhToday As DateTime = CDate(strTkhToday)
            Dim strNoStaf As String = Session("ssusrID")
            Dim strKodPTJ As String = Session("ssusrKodPTj")
            Dim strPerhatian As String = Trim(txtPerhatian.Value.TrimEnd)
            Dim strNoRujuk As String = Trim(txtNoRujukan.Value.TrimEnd)
            Dim IDPenerima As String = Trim(txtIDPenerima.Text.TrimEnd)
            Dim strKodPenghutang As String = Trim(txtKodPenghutang.Text.TrimEnd)
            Dim NamaPenerima As String = Trim(ddlNmPenerima.SelectedItem.ToString.Split("-")(1))
            Dim strAlamat1 As String = Trim(txtAlmt1.Value.TrimEnd)
            Dim strAlamat2 As String = Trim(txtAlmt2.Value.TrimEnd)
            Dim strBdr As String = Trim(txtBandar.Value.TrimEnd)
            Dim strPoskod As String = Trim(txtPoskod.Value.TrimEnd)
            Dim KodNegara As String = Trim(ddlNegara.SelectedValue.TrimEnd)
            Dim KodNegeri As String = Trim(ddlNegeri.SelectedValue.TrimEnd)
            Dim strEmel As String = Trim(txtEmel.Value.TrimEnd)
            Dim strNoTel As String = Trim(txtNoTel.Value.TrimEnd)
            Dim strNoFax As String = Trim(txtNoFax.Value.TrimEnd)
            Dim strJenisUrusniaga As String = Trim(ddlJenis.SelectedValue.TrimEnd)
            Dim strKat As String = Trim(ddlKat.SelectedValue.TrimEnd)
            Dim strBank As String = Trim(ddlBank.SelectedValue.TrimEnd)
            Dim strTujuan As String = Trim(txtTujuan.Value.TrimEnd)
            Dim blnKontrak As Boolean = CBool(rbKontrak.SelectedValue)
            Dim strStatDok As String = "01"
            Dim strTkhKonMula, strTkhKonTamat As String
            Dim intTempKon As Integer
            Dim strNoMemo
            Dim strJenTemp
            Dim strTahun As String = Now.Year

            trackno = 2
            If rbKontrak.SelectedValue = "True" Then
                Dim dtTkhMula As Date = DateTime.ParseExact(Trim(txtTrkhMula.Text.TrimEnd), "dd/MM/yyyy", CultureInfo.CurrentCulture)
                strTkhKonMula = dtTkhMula.ToString("yyyy-MM-dd")

                Dim dtTkhTamat As Date = DateTime.ParseExact(Trim(txtTrkhTmt.Text.TrimEnd), "dd/MM/yyyy", CultureInfo.CurrentCulture)
                strTkhKonTamat = dtTkhTamat.ToString("yyyy-MM-dd")

                intTempKon = CInt(txtTemKon.Text)

                strNoMemo = Trim(ddlMemo.SelectedValue.TrimEnd)
                strJenTemp = ddlJenTemp.SelectedValue.TrimEnd
            Else
                strTkhKonMula = ""
                strTkhKonTamat = ""
                intTempKon = 0
                strNoMemo = ""
                strJenTemp = ""
            End If


            dbconn.sConnBeginTrans()

            trackno = 3
            strNoInvSem = fGetID_Bil()

            Dim footerTrans = gvInvDt.FooterRow
            Dim decTotJum As Decimal = CDec(CType(footerTrans.FindControl("lblTotJum"), Label).Text)

            trackno = 4
            'Insert Into AR01_Bil
            strSql = "insert into AR01_Bil (AR01_NOBILSEM, AR01_NOBIL, AR01_TkhMohon, AR01_JENIS ,AR01_KODPTJMOHON ,AR01_NOSTAF ,AR01_UTKPERHATIAN ,AR01_NoRujukan ,AR01_Jumlah,AR01_STATUSDOK,AR01_STATUSCETAKBILSBNR, AR01_STATUSCETAKBILSMTR,AR01_FLAGADJ, KodPenghutang, AR01_IDPENERIMA,AR01_NAMAPENERIMA,AR01_ALMT1,AR01_ALMT2,AR01_BANDAR,AR01_POSKOD,KODNEGARA,KODNEGERI,AR01_EMEL,AR01_NOTEL,AR01_NOFAKS,AR01_JenisUrusniaga,AR01_KATEGORI,AR01_KODBANK,AR01_TUJUAN,AR01_JumBlmByr,AR01_TkhKonDari,AR01_TkhKonHingga,AR01_TempohKontrak, AR01_Kontrak, AR01_NoMemo, AR01_JenTemp, AR01_Tahun)" &
                      "values(@NOBILSEM,@NOBIL,@TkhMhn,@JENIS,@KODPTJMOHON,@NOSTAF,@UTKPERHATIAN,@NORUJUKAN,@Jumlah,@STATUSDOK,@STATUSCETAKBILSBNR,@STATUSCETAKBILSMTR,@FLAGADJ, @KodPenghutang, @IDPenerima, @NamaPenerima, @ALMT1, @ALMT2,@BANDAR,@POSKOD,@KODNEGARA,@KODNEGERI,@EMEL,@NOTEL,@NOFAKS,@JenisUrusniaga,@KATEGORI,@KODBANK,@TUJUAN,@JumBlmByr,@TkhKonDari,@TkhKonHingga,@TempohKontrak, @Kontrak, @AR01_NoMemo, @AR01_JenTemp, @AR01_Tahun)"

            Dim paramSql() As SqlParameter =
                {
                New SqlParameter("@NOBILSEM", strNoInvSem),
                New SqlParameter("@NOBIL", strNoInvSem),
                New SqlParameter("@TkhMhn", dtTkhToday),
                New SqlParameter("@JENIS", "01"),
                New SqlParameter("@KODPTJMOHON", strKodPTJ),
                New SqlParameter("@NOSTAF", strNoStaf),
                New SqlParameter("@UTKPERHATIAN", strPerhatian),
                New SqlParameter("@NORUJUKAN", strNoRujuk),
                New SqlParameter("@Jumlah", decTotJum),
                New SqlParameter("@STATUSDOK", strStatDok),
                New SqlParameter("@STATUSCETAKBILSBNR", 0),
                New SqlParameter("@STATUSCETAKBILSMTR", 0),
                New SqlParameter("@FLAGADJ", 0),
                New SqlParameter("@KodPenghutang", strKodPenghutang),
                New SqlParameter("@IDPENERIMA", IDPenerima),
                New SqlParameter("@NAMAPENERIMA", NamaPenerima),
                New SqlParameter("@ALMT1", strAlamat1),
                New SqlParameter("@ALMT2", strAlamat2),
                New SqlParameter("@BANDAR", strBdr),
                New SqlParameter("@POSKOD", strPoskod),
                New SqlParameter("@KODNEGARA", KodNegara),
                New SqlParameter("@KODNEGERI", KodNegeri),
                New SqlParameter("@EMEL", strEmel),
                New SqlParameter("@NOTEL", strNoTel),
                New SqlParameter("@NOFAKS", strNoFax),
                 New SqlParameter("@JenisUrusniaga", strJenisUrusniaga),
                New SqlParameter("@KATEGORI", strKat),
                New SqlParameter("@KODBANK", strBank),
                New SqlParameter("@TUJUAN", strTujuan),
                New SqlParameter("@JumBlmByr", decTotJum),
                New SqlParameter("@TkhKonDari", IIf(strTkhKonMula = "", DBNull.Value, strTkhKonMula)),
                New SqlParameter("@TkhKonHingga", IIf(strTkhKonTamat = "", DBNull.Value, strTkhKonTamat)),
                New SqlParameter("@TempohKontrak", IIf(intTempKon = 0, DBNull.Value, intTempKon)),
                New SqlParameter("@Kontrak", blnKontrak),
                New SqlParameter("@AR01_NoMemo", IIf(strNoMemo = "", DBNull.Value, strNoMemo)),
                New SqlParameter("@AR01_JenTemp", IIf(strJenTemp = "", DBNull.Value, strJenTemp)),
                New SqlParameter("@AR01_Tahun", strTahun)
                }

            If Not dbconn.fInsertCommand(strSql, paramSql, intIdBil) > 0 Then
                blnSuccess = False
                Exit Try
            Else
                'AUDIT LOG
                sLogMedan = "AR01_NOBILSEM| AR01_NOBIL| AR01_TkhMohon| AR01_JENIS |AR01_KODPTJMOHON |AR01_NOSTAF |AR01_UTKPERHATIAN |AR01_NoRujukan |AR01_Jumlah|AR01_STATUSDOK|AR01_STATUSCETAKBILSBNR| AR01_STATUSCETAKBILSMTR|AR01_FLAGADJ| KodPenghutang| AR01_IDPENERIMA|AR01_NAMAPENERIMA|AR01_ALMT1|AR01_ALMT2|AR01_BANDAR|AR01_POSKOD|KODNEGARA|KODNEGERI|AR01_EMEL|AR01_NOTEL|AR01_NOFAKS|AR01_JenisUrusniaga|AR01_KATEGORI|AR01_KODBANK|AR01_TUJUAN|AR01_JumBlmByr|AR01_TkhKonDari|AR01_TkhKonHingga|AR01_TempohKontrak| AR01_Kontrak| AR01_NoMemo| AR01_JenTemp| AR01_Tahun"
                sLogBaru = intIdBil & "|" & strNoInvSem & "|" & strNoInvSem & "|" & dtTkhToday & "|01|" & strKodPTJ & "|" & strNoStaf & "|" & strPerhatian & "|" & strNoRujuk & "|" & decTotJum & "|" & strStatDok & "|0|0|0|" & strKodPenghutang & "|" & IDPenerima & "|" & NamaPenerima & "|" & "|" & strAlamat1 & "|" & "|" & strAlamat2 & "|" & "|" & strBdr & "|" & "|" & strPoskod & "|" & "|" & KodNegara & "|" & "|" & KodNegeri & "|" & "|" & strEmel & "|" & "|" & strNoTel & "|" & "|" & strNoFax & "|" & "|" & strJenisUrusniaga & "|" & "|" & strKat & "|" & "|" & strBank & "|" & "|" & strTujuan & "|" & "|" & decTotJum & "|" & "|" & strTkhKonMula & "|" & "|" & strTkhKonTamat & "|" & "|" & intTempKon & "|" & "|" & blnKontrak & "|" & "|" & strNoMemo & "|" & "|" & strJenTemp & "|" & "|" & strTahun

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

            trackno = 5
            Dim intBil As Integer
            Dim strKodKW, strKodKO, strPTJ, strKodKP, strKodVot, strPerkara As String
            Dim decKuantiti, decKdrHrg, decJum As Decimal
            For Each gvTransInvrow As GridViewRow In gvInvDt.Rows
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

                strSql = " INSERT INTO AR01_BilDT(AR01_IdBil, AR01_NoBilSem,AR01_NoBil,AR01_Bil,KodKw,KodKO,KodPTJ,KodKP,KodVot,AR01_Perkara,AR01_Kuantiti,AR01_kadarHarga,AR01_Jumlah)" &
                             "values (@IdBil,@NOBILSEM, @NOBIL,@BIL,@kodKw, @KodKO, @KodPTJ, @KodKP, @KodVot, @Perkara, @Kuantiti,@kadarHarga,@Jumlah)"

                Dim paramSql2() As SqlParameter =
                    {
                    New SqlParameter("@IdBil", intIdBil),
                     New SqlParameter("@NOBILSEM", strNoInvSem),
                     New SqlParameter("@NOBIL", strNoInvSem),
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
                    sLogMedan = "AR01_IdBil|AR01_NoBilSem|AR01_NoBil|AR01_Bil|KodKw|KodKO|KodPTJ|KodKP|KodVot|AR01_Perkara|AR01_Kuantiti|AR01_kadarHarga|AR01_Jumlah"
                    sLogBaru = intIdBil & "|" & strNoInvSem & "|" & strNoInvSem & "|" & intBil & "|" & strKodKW & "|" & strKodKO & "|" & strPTJ & "|" & strKodKP & "|" & strKodVot & "|" & strPerkara & "|" & decKuantiti & "|" & decKdrHrg & "|" & decJum & "|"

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
            Next

            trackno = 6
            'Insert Into AR06_STATUSDOK
            strSql = "INSERT INTO AR06_STATUSDOK(AR06_NOBILSEM,AR06_NoBil, AR06_StatusDok,AR06_Tarikh,AR06_Ulasan,AR06_NoStaf)" &
                             "VALUES (@NOBILSEM,@NOBIL, @STATUSDOK,@TARIKH,@ULASAN, @NOSTAF)"

            Dim paramSql3() As SqlParameter =
                {
                New SqlParameter("@NOBILSEM", strNoInvSem),
                 New SqlParameter("@NOBIL", strNoInvSem),
                 New SqlParameter("@STATUSDOK", strStatDok),
                 New SqlParameter("@TARIKH", dtTkhToday),
                 New SqlParameter("@ULASAN", " - "),
                 New SqlParameter("@NOSTAF", strNoStaf)
                }

            If Not dbconn.fInsertCommand(strSql, paramSql3) > 0 Then
                blnSuccess = False
                Exit Try

            End If


            trackno = 7
            Try
                intBil = 0
                Dim intIdLamp As Integer
                For i As Integer = 0 To Request.Files.Count - 1
                    intBil += 1
                    Dim PostedFile As HttpPostedFile = Request.Files(i)
                    trackno = 8
                    If PostedFile.ContentLength > 0 Then
                        trackno = 9
                        FileName = System.IO.Path.GetFileName(PostedFile.FileName)
                        trackno = 10
                        Dim strExt As String = Path.GetExtension(FileName)
                        trackno = 11
                        folderPath = Server.MapPath("~/Upload/Document/" & strKodModul & "/" & intIdBil & "/")
                        trackno = 12
                        Dim strConType = PostedFile.ContentType

                        trackno = 13
                        'Insert Into AR06_STATUSDOK
                        strSql = "INSERT INTO AR11_Lampiran (AR01_IdBil, AR01_NoBilSem, AR11_NamaDok, AR11_Bil, AR11_Path , AR11_ContentType , AR11_Status)
VALUES (@IdBil, @NoInvSem, @NamaDok, @Bil, @Path, @ConType, @Status)"

                        Dim paramSql4() As SqlParameter =
                         {
                            New SqlParameter("@IdBil", intIdBil),
                            New SqlParameter("@NoInvSem", strNoInvSem),
                            New SqlParameter("@NamaDok", FileName),
                            New SqlParameter("@Bil", intBil),
                            New SqlParameter("@Path", folderPath),
                            New SqlParameter("@ConType", strConType),
                            New SqlParameter("@Status", 1)
                        }

                        If Not dbconn.fInsertCommand(strSql, paramSql4, intIdLamp) > 0 Then
                            blnSuccess = False
                            Exit Try
                        Else
                            'AUDIT LOG
                            sLogMedan = "AR01_IdBil|AR01_NoBilSem|AR11_NamaDok|AR11_Bil|AR11_Path"
                            sLogBaru = intIdBil & "|" & strNoInvSem & "|" & FileName & "|" & intBil & "|" & folderPath & "|" & strConType & "|1"

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
                                New SqlParameter("@InfoTable", "AR11_Lampiran"),
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

                        trackno = 14
                        If Not Directory.Exists(folderPath) Then
                            trackno = 15
                            Directory.CreateDirectory(folderPath)
                        End If

                        trackno = 16
                        FileName = intIdBil & "-" & intIdLamp & strExt

                        trackno = 17
                        PostedFile.SaveAs(folderPath & FileName)

                        trackno = 18
                        Dim bytFileContent As Byte() = fReadFile(folderPath & "/" & FileName)

                        trackno = 19
                        Dim SrcPath = folderPath & "/" & intIdBil & "-" & intIdLamp

                        trackno = 20
                        If fEncFile(SrcPath, bytFileContent) Then
                            trackno = 21
                            fErrorLog("folderPath - " & folderPath & " , FileName - " & FileName & ", fullpath - " & folderPath & "/" & FileName & ", SrcPath - " & SrcPath)
                            'File.SetAttributes(folderPath & "/" & FileName, FileAttributes.Normal)
                            File.Delete(folderPath & "/" & FileName)

                        Else
                            blnSuccess = False
                            Exit Try
                        End If

                    End If
                Next
                trackno = 22
            Catch ex As Exception
                blnSuccess = False
                Exit Try
            End Try

            If fSendEmail(strNoInvSem, strTkhToday) = False Then
                fErrorLog(" Invois_Kewangan.aspx, fSimpanInv(), Err - Ralat hantar email.")
            End If

        Catch ex As Exception
            blnSuccess = False
        End Try

        If blnSuccess = True Then
            dbconn.sConnCommitTrans()
            ViewState("SaveInv") = True
            txtNoInvSem.Text = strNoInvSem
            Return True

        ElseIf blnSuccess = False Then
            fErrorLog("trackno - " & trackno)
            dbconn.sConnRollbackTrans()
            Return False
        End If

    End Function

    Private Function fSendEmail(strNoInvSem, strTarikh) As Boolean
        Dim clsMail As New clsMail.Mail

        Dim strSMTPServer As String = WebConfigurationManager.AppSettings("SMTPServer") ' "smtp01.utem.edu.my"
        Dim strSMTPPort As String = WebConfigurationManager.AppSettings("SMTPPort") ' "25"
        Dim strSenderAdr As String = WebConfigurationManager.AppSettings("SenderAddr") ' "smkbTest@utem.edu.my"

        Try

            Dim strsubject As String
            Dim strbody As String = ""
            Dim strAtt As String = ""

            Dim strNoStaf = Session("ssusrID")
            Dim strNamaStaf = Session("ssusrName")
            Dim strKodPTj = Session("ssusrKodPTj")
            Dim strPTj = Session("ssusrPTj")

            strsubject = "PERMOHONAN INVOIS (KEWANGAN)"
            strbody += "<br><b>PERMOHONAN BARU INVOIS (KEWANGAN)"
            strbody += "<br><b> No. Invois Sementara : " & strNoInvSem
            strbody += "<br><b>No. Staf : " & strNoStaf & " - " & strNamaStaf
            strbody += "<br><b>PTj : " & strKodPTj & " - " & strPTj & "<br><b>"
            strbody += "<br><b>Tarikh : " & strTarikh & "<br><b>"
            strbody += "<br>Email ini tidak perlu dibalas."

            'strTo = "hanafi.mohtar@utem.edu.my" 'fGetEmail() 'Put semicolon(;) for multiple receipients i.e: "aaa@utem.edu.my;bbb@gmail.com"
            ' strTo = fGetEmail()

            Dim strTo = fGetEmailPIC(ViewState("sesKodSubMenu"), 1)

            If strTo = "" Then
                fErrorLog(" Invois_Kewangan.aspx, fSimpanInv(), Err - Ralat load email pelulus.")
                Return False
            End If

            'strAtt = "test.pdf;test2.pdf;test.txt;test.bmp;test.docx" 'Put semicolon(;) for multiple attachment i.e: "aaa.pdf;bbb.txt"
            Dim strMsg = clsMail.fSendMail(strSenderAdr, strTo, strsubject, strbody, strSMTPServer, strSMTPPort, strAtt)

            If strMsg = "1" Then
                Return True
            Else
                fErrorLog(" Invois_Kewangan.aspx, fSendEmail(), Err - " & strMsg)
                Return False
            End If

        Catch ex As Exception
            fErrorLog(" Invois_Kewangan.aspx, fSendEmail(), Err - " & ex.Message.ToString)
            Return False
        End Try

    End Function


    Private Sub sRekodBaru()
        txtNoInvSem.Text = ""
        txtStatus.Text = "PERMOHONAN "
        ' ddlBank.SelectedValue = 0
        ddlJenis.SelectedValue = 0
        ddlKat.SelectedValue = 0

        ddlNegara.SelectedValue = 0
        ddlNegeri.SelectedValue = 0
        txtIDPenerima.Text = ""
        txtTujuan.Value = ""

        txtNoRujukan.Value = ""
        txtAlmt1.Value = ""
        txtAlmt2.Value = ""
        txtBandar.Value = ""
        txtPoskod.Value = ""
        txtNoTel.Value = ""
        txtNoFax.Value = ""
        txtEmel.Value = ""
        txtPerhatian.Value = ""
        gvInvDt.DataSource = New List(Of String)
        gvInvDt.DataBind()

        txtNamaPenerima.Text = ""
        txtNamaPenerima.Visible = False
        ddlNmPenerima.Visible = True
        ddlNmPenerima.SelectedValue = 0
        'divLamp.Visible = False
        SetInitialRow()

        ddlJenis.Enabled = True
        ddlKat.Enabled = True
        rbKontrak.SelectedValue = "False"

        txtTrkhMula.Text = ""
        txtTrkhTmt.Text = ""
        txtTrkhMula.Enabled = False
        txtTrkhTmt.Enabled = False
        btnCalMula.Disabled = True
        btnCalTamat.Disabled = True
        txtTemKon.Text = 0

        ddlMemo.Items.Clear()
        ddlJenTemp.SelectedValue = 0
        rbKontrak.Enabled = False
        ddlMemo.Enabled = False

    End Sub

    Function fGetID_Bil()
        Try

            Dim strSql As String
            Dim strIdx As String
            Dim intLastIdx As Integer
            Dim strCol As String
            Dim strTahun As String = Now.Year
            Dim strKodModul As String
            Dim strPrefix As String
            Dim strButiran As String = "Max No Sem Kewangan AR"

            strSql = "select * from mk_noakhir where tahun = '" & strTahun & "' and kodmodul='AR' and prefix='SK'"
            strCol = "NoAkhir"
            strKodModul = "AR"
            strPrefix = "SK"

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

    Private Sub sLoadInv(ByVal strNoInvSem As String)

        Dim dbconn As New DBKewConn
        Try

            Dim strSqlInv = $"Select AR01_IdBil, AR01_TkhMohon,AR01_KODPTJMOHON, AR01_NOSTAF, AR01_Jenis,AR01_KODBANK,AR01_NORUJUKAN,AR01_ALMT1,AR01_ALMT2,
                            AR01_KATEGORI, KodPenghutang, AR01_IDPENERIMA, AR01_NAMAPENERIMA,kodnegara, kodnegeri, AR01_TUJUAN, AR01_BANDAR,AR01_POSKOD, AR01_TkhKonDari,AR01_TkhKonHingga,AR01_NOTEL, 
                            AR01_NOFAKS, AR01_TempohKontrak, AR01_Emel,AR01_UtkPerhatian, AR01_StatusDok, (select Butiran  from AR_StatusDok where KodStatus = AR01_Bil.AR01_StatusDok  ) as ButStatDok, AR01_JenisUrusniaga from AR01_Bil where AR01_NoBilSem='{strNoInvSem}';"

            Dim strSqlTrans = "select AR01_BilDtID, KodKw,KodKO,KodPTJ,KodKP,KodVot,AR01_Perkara,AR01_Kuantiti,AR01_kadarHarga,AR01_Jumlah from AR01_BilDt where AR01_NoBilSem='" & strNoInvSem & "';"

            Dim strSqlLamp = "Select  AR01_IdBil, AR11_ID, AR11_Path, AR11_NamaDok,AR11_ContentType, AR11_Status from AR11_Lampiran where AR01_NoBilSem = '" & strNoInvSem & "' and AR11_Status = 1 order by AR11_Bil "

            'Load Permohonan Invois
            Dim ds = dbconn.fSelectCommand(strSqlInv + strSqlTrans + strSqlLamp)

            Using dtMhn = ds.Tables(0)
                If dtMhn.Rows.Count > 0 Then
                    Dim intIdBil As Integer = dtMhn.Rows(0)("AR01_IdBil")
                    Dim TkhMhn As Date = dtMhn.Rows(0)("AR01_TkhMohon")
                    Dim KodPTJ As String = dtMhn.Rows(0)("AR01_KODPTJMOHON")
                    Dim NoPmhn As String = dtMhn.Rows(0)("AR01_NOSTAF")
                    Dim katInv As String = dtMhn.Rows(0)("AR01_Jenis")
                    Dim Bank As String = dtMhn.Rows(0)("AR01_KODBANK")
                    Dim NoRuj As String = dtMhn.Rows(0)("AR01_NORUJUKAN")
                    Dim Almt1 As String = dtMhn.Rows(0)("AR01_ALMT1")
                    Dim Almt2 As String = dtMhn.Rows(0)("AR01_ALMT2")
                    Dim Kat As String = dtMhn.Rows(0)("AR01_KATEGORI")
                    Dim strKodPenghutang As String = dtMhn.Rows(0)("KodPenghutang")
                    Dim NPenerima As String = dtMhn.Rows(0)("AR01_NAMAPENERIMA")
                    Dim Negara As String = dtMhn.Rows(0)("kodnegara")
                    Dim Negeri As String = dtMhn.Rows(0)("kodnegeri")
                    Dim IDPenerima As String = dtMhn.Rows(0)("AR01_IDPENERIMA")
                    Dim Tujuan As String = dtMhn.Rows(0)("AR01_TUJUAN")
                    Dim Bandar As String = dtMhn.Rows(0)("AR01_BANDAR")
                    Dim Poskod As String = dtMhn.Rows(0)("AR01_POSKOD")
                    Dim NoTel As String = dtMhn.Rows(0)("AR01_NOTEL")
                    Dim NoFax As String = dtMhn.Rows(0)("AR01_NOFAKS")
                    Dim Emel As String = dtMhn.Rows(0)("AR01_Emel")
                    Dim Perhatian As String = dtMhn.Rows(0)("AR01_UtkPerhatian")
                    Dim strStatDok As String = dtMhn.Rows(0)("AR01_StatusDok")
                    Dim JnsUrus As String = dtMhn.Rows(0)("AR01_JenisUrusniaga")

                    txtStatus.Text = dtMhn.Rows(0)("ButStatDok")
                    txtTkhMohon.Text = TkhMhn

                    hidIdBil.Value = intIdBil

                    txtNoInvSem.Text = strNoInvSem
                    txtIDPenerima.Text = IDPenerima
                    txtNamaPenerima.Text = NPenerima
                    txtTujuan.Value = Tujuan
                    txtNoRujukan.Value = IIf(NoRuj = "", "-", NoRuj)
                    txtAlmt1.Value = Almt1
                    txtAlmt2.Value = Almt2
                    txtBandar.Value = Bandar
                    txtPoskod.Value = Poskod
                    txtNoTel.Value = NoTel
                    txtNoFax.Value = NoFax
                    txtEmel.Value = Emel
                    txtPerhatian.Value = IIf(Perhatian = "", "-", Perhatian)

                    ddlBank.SelectedValue = Bank
                    ddlKat.SelectedValue = Kat
                    ddlNegara.SelectedValue = Negara
                    ddlNegeri.SelectedValue = Negeri
                    ddlJenis.SelectedValue = JnsUrus

                    ddlKat.Enabled = False
                    ddlNmPenerima.Visible = False
                    txtNamaPenerima.Visible = True
                    txtIDPenerima.Visible = True

                End If
            End Using

            'Load Invois Trans

            Using dt = ds.Tables(1)
                ViewState("dtInvDt") = dt
                gvInvDt.DataSource = dt
                gvInvDt.DataBind()
            End Using

        Catch ex As Exception

        End Try

    End Sub


    Private Sub rbKontrak_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rbKontrak.SelectedIndexChanged
        If rbKontrak.SelectedValue = "True" Then
            btnCalMula.Disabled = False
            btnCalTamat.Disabled = False
            ddlMemo.Enabled = True
            ddlJenTemp.Enabled = True

            sBindMemo()
            'divTkhMula.Attributes.Add("class", "form-group")
            'divTkhTamat.Attributes.Add("class", "form-group")
        Else
            btnCalMula.Disabled = True
            btnCalTamat.Disabled = True
            ddlMemo.Enabled = False
            ddlJenTemp.Enabled = False
            'divTkhMula.Attributes("class") = ""
            'divTkhTamat.Attributes("class") = ""
        End If
        ddlJenTemp.SelectedIndex = 0
    End Sub

    ''' <summary>
    ''' Load No. Memo dari table PUU06_DaftarMemo
    ''' </summary>
    Private Sub sBindMemo()

        Try

            Dim strKodPenghutang = Trim(ddlNmPenerima.SelectedValue.TrimEnd)
            Dim strSql = "select PUU05_NoMemo, PUU06_TkhMula, PUU06_TkhTamat, PUU06_Tempoh, PUU06_JnsTempoh from PUU06_DaftarMemo where ROC01_IDSya = (select IdPenghutang  from AR_Penghutang where KodPenghutang = '" & strKodPenghutang & "')"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)

            ddlMemo.DataSource = ds
            ddlMemo.DataTextField = "PUU05_NoMemo"
            ddlMemo.DataValueField = "PUU05_NoMemo"
            ddlMemo.DataBind()

            ddlMemo.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlMemo.SelectedIndex = 0

            ViewState("dsMemo") = ds

        Catch ex As Exception

        End Try
    End Sub

    Private Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.Click


        If fSimpanInv() Then
            fGlobalAlert("Maklumat telah disimpan!", Me.Page, Me.[GetType]())
            sRekodBaru()
        Else
            fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
        End If
    End Sub

    Private Sub btnPadamMula_ServerClick(sender As Object, e As EventArgs) Handles btnPadamMula.ServerClick
        txtTrkhMula.Text = ""
        txtTemKon.Text = "0"
    End Sub

    Private Sub btnPadamTamat_ServerClick(sender As Object, e As EventArgs) Handles btnPadamTamat.ServerClick
        txtTrkhTmt.Text = ""
        txtTemKon.Text = "0"

    End Sub

    'Private Sub txtTkhMula_TextChanged(sender As Object, e As EventArgs) Handles txtTkhMula.TextChanged
    '    If txtTkhMula.Text <> "" AndAlso txtTkhTamat.Text <> "" Then
    '        Dim dtTkhMula As Date = DateTime.ParseExact(Trim(txtTkhMula.Text.TrimEnd), "dd/MM/yyyy", CultureInfo.CurrentCulture)
    '        Dim dtTkhTamat As Date = DateTime.ParseExact(Trim(txtTkhTamat.Text.TrimEnd), "dd/MM/yyyy", CultureInfo.CurrentCulture)
    '        txtTemKon.Text = (dtTkhTamat - dtTkhMula).TotalDays + 1
    '    End If
    'End Sub

    'Private Sub txtTkhTamat_TextChanged(sender As Object, e As EventArgs) Handles txtTkhTamat.TextChanged
    '    If txtTkhMula.Text <> "" AndAlso txtTkhTamat.Text <> "" Then
    '        Dim dtTkhMula As Date = DateTime.ParseExact(Trim(txtTkhMula.Text.TrimEnd), "dd/MM/yyyy", CultureInfo.CurrentCulture)
    '        Dim dtTkhTamat As Date = DateTime.ParseExact(Trim(txtTkhTamat.Text.TrimEnd), "dd/MM/yyyy", CultureInfo.CurrentCulture)
    '        txtTemKon.Text = (dtTkhTamat - dtTkhMula).TotalDays + 1
    '    End If
    'End Sub


    Private Sub txtTrkhMula_TextChanged(sender As Object, e As EventArgs) Handles txtTrkhMula.TextChanged
        Try
            If txtTrkhMula.Text <> "" AndAlso txtTrkhTmt.Text <> "" Then
                Dim dtTkhMula As Date = DateTime.ParseExact(Trim(txtTrkhMula.Text.TrimEnd), "dd/MM/yyyy", CultureInfo.CurrentCulture)
                Dim dtTkhTamat As Date = DateTime.ParseExact(Trim(txtTrkhTmt.Text.TrimEnd), "dd/MM/yyyy", CultureInfo.CurrentCulture)
                txtTemKon.Text = (dtTkhTamat - dtTkhMula).TotalDays + 1
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtTrkhTmt_TextChanged(sender As Object, e As EventArgs) Handles txtTrkhTmt.TextChanged
        Try
            If txtTrkhMula.Text <> "" AndAlso txtTrkhTmt.Text <> "" Then
                Dim dtTkhMula As Date = DateTime.ParseExact(Trim(txtTrkhMula.Text.TrimEnd), "dd/MM/yyyy", CultureInfo.CurrentCulture)
                Dim dtTkhTamat As Date = DateTime.ParseExact(Trim(txtTrkhTmt.Text.TrimEnd), "dd/MM/yyyy", CultureInfo.CurrentCulture)
                txtTemKon.Text = (dtTkhTamat - dtTkhMula).TotalDays + 1
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlMemo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMemo.SelectedIndexChanged
        Try
            Dim strNoMemo = Trim(ddlMemo.SelectedValue.TrimEnd)

            Dim dsMemo As DataSet = ViewState("dsMemo")
            Dim dvMemo As New DataView(dsMemo.Tables(0))

            dvMemo.RowFilter = "PUU05_NoMemo = '" & strNoMemo & "'"

            Dim strTkhMula = dvMemo.Item(0)("PUU06_TkhMula").ToString
            Dim strTrkhTamat = dvMemo.Item(0)("PUU06_TkhTamat").ToString
            Dim strTempoh = dvMemo.Item(0)("PUU06_Tempoh").ToString
            Dim intJenTemp = dvMemo.Item(0)("PUU06_JnsTempoh").ToString

            Dim dtTrkhMula As Date = CDate(strTkhMula)
            strTkhMula = dtTrkhMula.ToString("dd/MM/yyyy")

            Dim dtTrkhTamat As Date = CDate(strTrkhTamat)
            strTrkhTamat = dtTrkhTamat.ToString("dd/MM/yyyy")

            ddlJenTemp.SelectedValue = intJenTemp

            txtTrkhMula.Text = strTkhMula
            txtTrkhTmt.Text = strTrkhTamat
            txtTemKon.Text = strTempoh

        Catch ex As Exception

        End Try
    End Sub

    Private Sub lbtnRekBaru_Click(sender As Object, e As EventArgs) Handles lbtnRekBaru.Click
        sRekodBaru()
    End Sub
End Class