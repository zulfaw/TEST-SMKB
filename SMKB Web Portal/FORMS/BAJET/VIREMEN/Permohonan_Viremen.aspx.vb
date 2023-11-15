Imports System.Drawing
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Public Class Permohonan_Viremen
    Inherits System.Web.UI.Page


    ' Public KodStatus As String = String.Empty

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                lbtnHapus.Visible = False
                ViewState("savemode") = 1
                txtStatus.Text = "PERMOHONAN BARU"
                txtNoStaf.Text = Session("ssusrID")
                Dim strKodSubMenu As String = Request.QueryString("KodSubMenu")

                If fCheckPowerUser(Trim(Session("ssusrID")), strKodSubMenu) Then
                    fEnable()
                    fListViremenUserPower()
                    ViewState("poweruser") = True
                Else
                    fDisable()
                    ViewState("poweruser") = False
                End If


                ViewState("KodPTj") = Session("ssusrKodPTj")
                'fBindDdlJenisDasar()
                fBindDdlKumpWang()
                fBindDdlStatus()

                ddlKOk.Items.Add(New ListItem("- SILA PILIH KUMPULAN WANG -", 0))
                ddlPTJk.Items.Add(New ListItem("- SILA PILIH KOD OPERASI -", 0))
                ddlKPk.Items.Add(New ListItem("- SILA PILIH PTJ -", 0))
                ddlObjSbgK.Items.Add(New ListItem("- SILA PILIH KOD PROJEK -", 0))
                ddlKOm.Items.Add(New ListItem("- SILA PILIH KUMPULAN WANG -", 0))
                ddlPTJm.Items.Add(New ListItem("- SILA PILIH KOD OPERASI -", 0))
                ddlKPm.Items.Add(New ListItem("- SILA PILIH PTJ -", 0))
                ddlObjSbgM.Items.Add(New ListItem("- SILA PILIH KOD PROJEK -", 0))

                txtNamaPemohon.Text = Session("ssusrName")
                txtJawPemohon.Text = Session("ssusrPost")
                txtTkhMohon.Text = Now.ToString("dd/MM/yyyy")
                txtTahunVire.Text = Now.Year.ToString

                txtBakiSmsK.Text = "0.00"
                txtBakiSlpsVireK.Text = "0.00"
                txtBakiSmsM.Text = "0.00"
                txtBakiSlpsVireM.Text = "0.00"
                txtBoxA.Text = "0.00"
                txtBoxA1.Text = "0.00"
                txtBoxB.Text = "0.00"
                txtBoxB1.Text = "0.00"
                txtBoxC.Text = "0.00"
                txtBoxD.Text = "0.00"
                txtBoxE.Text = "0.00"
                txtBoxF.Text = "0.00"
                txtBoxG.Text = "0.00"
                txtBoxH.Text = "0.00"
                txtBoxI.Text = "0.00"

                HidKodStatus.Value = ""
                txtJumVire.Enabled = False

            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub fBindDdlKumpWang()
        Try
            Dim strSqlKW As String = " select KodKw, (KodKW + ' - ' + Butiran) as Butiran from MK_Kw where Status = 1 order by KodKw"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fselectCommand(strSqlKW)

            ddlKWk.DataSource = ds
            ddlKWk.DataTextField = "Butiran"
            ddlKWk.DataValueField = "KodKw"
            ddlKWk.DataBind()

            ddlKWk.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlKWk.SelectedIndex = 0

            ddlKWm.DataSource = ds
            ddlKWm.DataTextField = "Butiran"
            ddlKWm.DataValueField = "KodKw"
            ddlKWm.DataBind()

            ddlKWm.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlKWm.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub
    Private Sub fBindDdlKodOperasi(ByVal intDdl As Int16, ByVal strKodKW As String)
        Try
            Dim strSql As String = "SELECT DISTINCT TOP 100 PERCENT MK_KodOperasi.KodKO ,(MK_KodOperasi.KodKO + ' - ' + MK_KodOperasi.Butiran) as Butiran FROM MK_KodOperasi  INNER JOIN MK01_VotTahun ON MK_KodOperasi.KodKO  = MK01_VotTahun.KodKO " &
                                        "WHERE MK01_VotTahun.KodKw = '" & Trim(strKodKW.TrimEnd) & "' AND MK01_VotTahun.MK01_Tahun  = '" & Trim(txtTahunVire.Text.TrimEnd) & "'"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable

            ds = dbconn.fselectCommand(strSql)

            If intDdl = 1 Then
                ddlKOk.DataSource = ds
                ddlKOk.DataTextField = "Butiran"
                ddlKOk.DataValueField = "KodKO"
                ddlKOk.DataBind()

                ddlKOk.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
                ddlKOk.SelectedIndex = 0
            ElseIf intDdl = 2 Then
                ddlKOm.DataSource = ds
                ddlKOm.DataTextField = "Butiran"
                ddlKOm.DataValueField = "KodKO"
                ddlKOm.DataBind()

                ddlKOm.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
                ddlKOm.SelectedIndex = 0
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindDdlPtj(ByVal intDdl As Int16, ByVal strKO As String)
        Try

            Dim strSqlPTJ = "SELECT DISTINCT TOP 100 PERCENT MK_PTJ.KodPTJ,(MK_PTJ.KodPTJ + ' - ' + MK_PTJ.Butiran) as Butiran2 FROM MK_PTJ INNER JOIN MK01_VotTahun ON MK_PTJ.KodPTJ = MK01_VotTahun.KodPTJ " &
            "WHERE MK01_VotTahun.KodKw = '" & Trim(ddlKWk.SelectedValue.TrimEnd) & "' AND  MK01_VotTahun.KodKO = '" & Trim(strKO.TrimEnd) & "' AND MK01_VotTahun.MK01_Tahun  = '" & Trim(txtTahunVire.Text.TrimEnd) & "'"
            '"select KodPTJ, Butiran, (KodPTJ + ' - ' + Butiran) as Butiran2 from MK_PTJ where Status='True' order by KodPTJ"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn

            ds = dbconn.fselectCommand(strSqlPTJ)

            If intDdl = 1 Then
                ddlPTJk.DataSource = ds
                ddlPTJk.DataTextField = "Butiran2"
                ddlPTJk.DataValueField = "KodPTJ"
                ddlPTJk.DataBind()

                ddlPTJk.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
                ddlPTJk.SelectedIndex = 0
            ElseIf intDdl = 2 Then
                ddlPTJm.DataSource = ds
                ddlPTJm.DataTextField = "Butiran2"
                ddlPTJm.DataValueField = "KodPTJ"
                ddlPTJm.DataBind()

                ddlPTJm.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
                ddlPTJm.SelectedIndex = 0
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub fBindDdlKP(ByVal intDdl As Int16, ByVal strkodKP As String)
        Try
            Dim strSql As String = "SELECT DISTINCT TOP 100 PERCENT MK_KodProjek.KodProjek  ,(MK_KodProjek.KodProjek  + ' - ' + MK_KodProjek.Butiran) as Butiran2 FROM MK_KodProjek  INNER JOIN MK01_VotTahun ON MK_KodProjek.KodProjek  = MK01_VotTahun.KodKP  
                                    WHERE MK01_VotTahun.MK01_Tahun  = '" & Trim(txtTahunVire.Text.TrimEnd) & "' and  MK01_VotTahun.KodKw = '" & Trim(ddlKWk.SelectedValue.TrimEnd) & "' 
                                    AND  MK01_VotTahun .KodKO = '" & Trim(ddlKOk.SelectedValue.TrimEnd) & "'"

            'And  MK01_VotTahun.KodKP = '" & strkodKP & "'

            '"SELECT DISTINCT TOP 100 PERCENT MK_KodProjek.KodProjek  ,(MK_KodProjek.KodProjek  + ' - ' + MK_KodProjek.Butiran) as Butiran FROM MK_KodProjek  INNER JOIN MK01_VotTahun ON MK_KodProjek.KodProjek  = MK01_VotTahun.KodKP  " &
            '                        "WHERE MK01_VotTahun.MK01_Tahun  = '" & Trim(txtTahunVire.Text.TrimEnd) & "' and  MK01_VotTahun.KodKw = '" & Trim(ddlKW.SelectedValue.TrimEnd) & "' AND  MK01_VotTahun .KodKO = '" & Trim(ddlKO.SelectedValue.TrimEnd) & "' AND  MK01_VotTahun .KodPTJ = '" & Trim(ddlPTJ1.SelectedValue.TrimEnd) & "'"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fselectCommand(strSql)

            If intDdl = 1 Then

                ddlKPk.DataSource = ds
                ddlKPk.DataTextField = "Butiran2"
                ddlKPk.DataValueField = "KodProjek"
                ddlKPk.DataBind()

                ddlKPk.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
                ddlKPk.SelectedIndex = 0

            ElseIf intDdl = 2 Then
                ddlKPm.DataSource = ds
                ddlKPm.DataTextField = "Butiran2"
                ddlKPm.DataValueField = "KodProjek"
                ddlKPm.DataBind()

                ddlKPm.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
                ddlKPm.SelectedIndex = 0
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub fBindDdlObjSebagai(ByVal intDdl As Int16, ByVal strkodKP As String)

        Try
            Dim strSqlObjSbg As String = "Select DISTINCT KODVOT,(KODVOT + ' - ' + BUTIRAN) as Butiran FROM vbg_objsbg where mk01_tahun = '" & Trim(txtTahunVire.Text.TrimEnd) & "' and kodkw = '" & Trim(ddlKWk.SelectedValue.TrimEnd) & "'and kodkp = '" & Trim(ddlKPk.SelectedValue.TrimEnd) & "' ORDER BY kodvot"

            '"Select DISTINCT KODVOT,(KODVOT + ' - ' + BUTIRAN) as Butiran FROM vbg_objsbg where mk01_tahun = '" & Trim(txtTahunVire.Text.TrimEnd) & "' and kodkw = '" & Trim(ddlKW.SelectedValue.TrimEnd) & "' and kodkp = '" & Trim(ddlKP.SelectedValue.TrimEnd) & "' and LEFT(KodVot, 2) As KodObjekSebagai ORDER BY kodvot"

            '"SELECT KodVot, Butiran, Klasifikasi,(KodVot + ' - ' + Butiran) as Butiran2, 
            'LEFT(KodVot, 1) As KodObjekAm,
            'LEFT(KodVot, 2) As KodObjekSebagai
            'From MK_Vot Order By KodVot"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fselectCommand(strSqlObjSbg)

            If intDdl = 1 Then
                ddlObjSbgK.DataSource = ds
                ddlObjSbgK.DataTextField = "Butiran"
                ddlObjSbgK.DataValueField = "KodVot"
                ddlObjSbgK.DataBind()


                ddlObjSbgK.Items.Insert(0, New ListItem("-SILA PILIH -", 0))
                ddlObjSbgK.SelectedIndex = 0
            ElseIf intDdl = 2 Then

                ddlObjSbgM.DataSource = ds
                ddlObjSbgM.DataTextField = "Butiran"
                ddlObjSbgM.DataValueField = "KodVot"
                ddlObjSbgM.DataBind()

                ddlObjSbgM.Items.Insert(0, New ListItem("-SILA PILIH -", 0))
                ddlObjSbgM.SelectedIndex = 0
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Function fBindDdlStatus() As String
        Try
            Dim strSql As String = "select KodStatusDok, Butiran  from BG_StatusDok where KodStatusDok in ('12','13','03','06','05','07','08','02','16')"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            ddlStatusFil.DataSource = ds
            ddlStatusFil.DataTextField = "Butiran"
            ddlStatusFil.DataValueField = "KodStatusDok"
            ddlStatusFil.DataBind()

            ddlStatusFil.Items.Insert(0, New ListItem("- KESELURUHAN -", 0))
            ddlStatusFil.SelectedIndex = 0

        Catch ex As Exception

        End Try

    End Function
    Private Sub fBindGViremen()
        Dim trackno As Integer
        Dim strKodStatusFil As String
        If ddlStatusFil.SelectedIndex = 0 Then
            strKodStatusFil = "'12','13','03','06','05','07','08','02','16'"
        Else
            strKodStatusFil = ddlStatusFil.SelectedValue
        End If

        Try

            Dim intRec As Integer = 0
            Dim strSql As String
            Dim dt As New DataTable

            dt.Columns.Add("NoViremen", GetType(String))
            dt.Columns.Add("TkhMohon", GetType(String))
            dt.Columns.Add("KwD", GetType(String))
            dt.Columns.Add("KOD", GetType(String))
            dt.Columns.Add("PtjD", GetType(String))
            dt.Columns.Add("KpD", GetType(String))
            dt.Columns.Add("ObjSbgD", GetType(String))
            dt.Columns.Add("JumlahD", GetType(String))
            dt.Columns.Add("KwK", GetType(String))
            dt.Columns.Add("KOK", GetType(String))
            dt.Columns.Add("PtjK", GetType(String))
            dt.Columns.Add("KpK", GetType(String))
            dt.Columns.Add("ObjSbgK", GetType(String))
            dt.Columns.Add("JumlahK", GetType(String))
            dt.Columns.Add("Status", GetType(String))
            dt.Columns.Add("KodStatus", GetType(String))

            strSql = "SELECT DISTINCT dbo.BG10_Viremen.BG10_NoViremen,convert(varchar,dbo.BG10_Viremen.BG10_TkhMohon,103) as BG10_TkhMohon,dbo.BG10_Viremen.BG10_NoStaf, dbo.BG10_Viremen.BG10_RujSurat,dbo.BG10_Viremen.BG10_Tujuan,dbo.BG10_Viremen.bg10_JnsDsr, dbo.BG10_Viremen.KodStatusDok,
dbo.VBG_VirKeluar.KodKw As kodkwk, dbo.VBG_VirKeluar.kodko  As kodkok,dbo.VBG_VirKeluar.KodPTJ As kodptjk,dbo.VBG_VirKeluar.kodkp  As kodkpk, dbo.VBG_VirKeluar.KodVot As kodvotk,  dbo.VBG_VirKeluar.BG10_Amaun As jumk,
dbo.VBG_VirKeluar.KodVir AS kodvirk, dbo.VBG_VirMasuk.KodKw AS kodkwm,  dbo.VBG_VirMasuk.KodKO AS kodkom,dbo.VBG_VirMasuk.KodPTJ AS kodptjm,  dbo.VBG_VirMasuk.KodKP AS kodkpm, dbo.VBG_VirMasuk.KodVot AS kodvotm,
dbo.VBG_VirMasuk.BG10_Amaun As jumm, dbo.VBG_VirMasuk.KodVir As kodvirm, dbo.BG10_Viremen.BG10_StatusBen, dbo.BG10_Viremen.BG10_StatusKJ, dbo.VBG_VirKeluar.BG10_BakiSms As bakismsk, MAX(dbo.VBG_VirKeluar.BG12_TkhProses) As BG09_TkhProses,
dbo.VBG_VirMasuk.BG10_BakiSms AS bakismsm, dbo.BG10_Viremen.BG10_RujSuratLulus, BG10_Viremen .KodStatusDok ,  (select BG_StatusDok.Butiran from BG_StatusDok where BG_StatusDok.KodStatusDok = BG10_Viremen .KodStatusDok ) as Status
From dbo.BG10_Viremen 
INNER JOIN dbo.VBG_VirKeluar ON dbo.BG10_Viremen.BG10_NoViremen = dbo.VBG_VirKeluar.BG10_NoViremen 
INNER JOIN dbo.VBG_VirMasuk ON dbo.BG10_Viremen.BG10_NoViremen = dbo.VBG_VirMasuk.BG10_NoViremen
WHERE BG10_Tahun = '" & Trim(txtTahunVire.Text.TrimEnd) & "' and dbo.VBG_VirKeluar.KodPTJ='" & ViewState("KodPTj") & "' And (dbo.BG10_Viremen.KodStatusDok in (" & strKodStatusFil & " ) ) 
GROUP BY dbo.BG10_Viremen.BG10_NoViremen, dbo.BG10_Viremen.BG10_TkhMohon, dbo.BG10_Viremen.BG10_NoStaf, dbo.BG10_Viremen.BG10_RujSurat, dbo.BG10_Viremen.BG10_Tujuan, dbo.BG10_Viremen.bg10_JnsDsr, 
dbo.BG10_Viremen.KodStatusDok, dbo.VBG_VirKeluar.KodKw, dbo.VBG_VirKeluar.kodko, dbo.VBG_VirKeluar.KodPTJ, dbo.VBG_VirKeluar.kodkp, dbo.VBG_VirKeluar.KodVot,
dbo.VBG_VirKeluar.BG10_Amaun, dbo.VBG_VirKeluar.KodVir, dbo.VBG_VirMasuk.KodKw, dbo.VBG_VirMasuk.KodKO, dbo.VBG_VirMasuk.KodPTJ, dbo.VBG_VirMasuk.KodKP, dbo.VBG_VirMasuk.KodVot,
dbo.VBG_VirMasuk.BG10_Amaun, dbo.VBG_VirMasuk.KodVir, dbo.VBG_VirKeluar.BG10_BakiSms, dbo.VBG_VirMasuk.BG10_BakiSms, dbo.BG10_Viremen.BG10_RujSuratLulus, dbo.BG10_Viremen.BG10_StatusBen, 
dbo.BG10_Viremen.BG10_StatusKJ
ORDER BY dbo.BG10_Viremen.BG10_NoViremen DESC"

            trackno = 1
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            trackno = 2
            Dim dvViremen As New DataView
            dvViremen = New DataView(ds.Tables(0))
            ViewState("dvViremen") = dvViremen.Table

            trackno = 3
            Dim strNoViremen, strTkhMohon, strTujuan, strJnsDsr As String
            Dim strKwD, strKOD, strPtjD, strKpD, strObjSbgD, strJumlahD As String
            Dim strKwK, strKOK, strPtjK, strKpK, strObjSbgK, strJumlahK As String
            Dim decJumlahD, decJumlahK As Decimal
            'Dim dtTkhMohon As Date
            Dim strStatus, strKodStatus As String
            trackno = 4
            If Not ds Is Nothing Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    strNoViremen = ds.Tables(0).Rows(i)("BG10_NoViremen")
                    'dtTkhMohon = ds.Tables(0).Rows(i)("BG10_TkhMohon")
                    strTkhMohon = ds.Tables(0).Rows(i)("BG10_TkhMohon")
                    'strNoStaf = ds.Tables(0).Rows(i)("BG10_NoStaf")
                    'strRujSurat = ds.Tables(0).Rows(i)("BG10_RujSurat")
                    strTujuan = ds.Tables(0).Rows(i)("BG10_Tujuan")
                    strJnsDsr = IIf(IsDBNull(ds.Tables(0).Rows(i)("bg10_JnsDsr")), String.Empty, ds.Tables(0).Rows(i)("bg10_JnsDsr"))

                    strKwD = ds.Tables(0).Rows(i)("kodkwk")
                    strKOD = ds.Tables(0).Rows(i)("kodkok")
                    strPtjD = ds.Tables(0).Rows(i)("kodptjk")
                    strKpD = ds.Tables(0).Rows(i)("kodkpk")
                    strObjSbgD = ds.Tables(0).Rows(i)("kodvotk")
                    decJumlahD = CDec(ds.Tables(0).Rows(i)("jumk"))
                    strJumlahD = decJumlahD.ToString("#,##0.00")

                    strKwK = ds.Tables(0).Rows(i)("kodkwm")
                    strKOK = ds.Tables(0).Rows(i)("kodkom")
                    strPtjK = ds.Tables(0).Rows(i)("kodptjm")
                    strKpK = ds.Tables(0).Rows(i)("kodkpm")
                    strObjSbgK = ds.Tables(0).Rows(i)("kodvotm")
                    decJumlahK = CDec(ds.Tables(0).Rows(i)("jumm"))
                    strJumlahK = decJumlahK.ToString("#,##0.00")
                    strStatus = ds.Tables(0).Rows(i)("Status")
                    strKodStatus = ds.Tables(0).Rows(i)("KodStatusDok")

                    dt.Rows.Add(strNoViremen, strTkhMohon, strKwD, strKOD, strPtjD, strKpD, strObjSbgD, strJumlahD, strKwK, strKOK, strPtjK, strKpK, strObjSbgK, strJumlahK, strStatus, strKodStatus)

                Next
                trackno = 5
                gvViremen.DataSource = dt
                gvViremen.DataBind()
                ViewState("dtList") = dt
                intRec = ds.Tables(0).Rows.Count

            End If
            trackno = 6
            lblJumRekod.InnerText = intRec

            trackno = 7
            gvViremen.DataSource = dt
            gvViremen.DataBind()
        Catch ex As Exception
            fGlobalAlert("trackno - " & trackno & "| " & ex.Message.ToString, Me.Page, Me.[GetType]())
        End Try
    End Sub
    Private Sub fListViremenUserPower()
        Try

            Dim intRec As Integer
            Dim strSql As String
            Dim dt As New DataTable

            dt.Columns.Add("NoViremen", GetType(String))
            dt.Columns.Add("TkhMohon", GetType(String))
            dt.Columns.Add("NoStaf", GetType(String))
            dt.Columns.Add("RujSurat", GetType(String))
            dt.Columns.Add("Tujuan", GetType(String))
            dt.Columns.Add("JnsDsr", GetType(String))
            dt.Columns.Add("KwD", GetType(String))
            dt.Columns.Add("KOD", GetType(String))
            dt.Columns.Add("PtjD", GetType(String))
            dt.Columns.Add("KpD", GetType(String))
            dt.Columns.Add("ObjSbgD", GetType(String))
            dt.Columns.Add("JumlahD", GetType(String))
            dt.Columns.Add("KwK", GetType(String))
            dt.Columns.Add("KOK", GetType(String))
            dt.Columns.Add("PtjK", GetType(String))
            dt.Columns.Add("KpK", GetType(String))
            dt.Columns.Add("ObjSbgK", GetType(String))
            dt.Columns.Add("JumlahK", GetType(String))

            strSql = "Select DISTINCT dbo.BG10_Viremen.BG10_NoViremen, dbo.BG10_Viremen.BG10_TkhMohon, dbo.BG10_Viremen.BG10_NoStaf, dbo.BG10_Viremen.BG10_RujSurat, dbo.BG10_Viremen.BG10_Tujuan, dbo.BG10_Viremen.bg10_JnsDsr, dbo.BG10_Viremen.KodStatusDok, dbo.VBG_VirKeluar.KodKw As kodkwk, 
                        dbo.VBG_VirKeluar.kodko  AS kodkok,dbo.VBG_VirKeluar.KodPTJ AS kodptjk,dbo.VBG_VirKeluar.kodkp  AS kodkpk, dbo.VBG_VirKeluar.KodVot AS kodvotk,  dbo.VBG_VirKeluar.BG10_Amaun AS jumk, dbo.VBG_VirKeluar.KodVir AS kodvirk, 
                        dbo.VBG_VirMasuk.KodKw AS kodkwm,  dbo.VBG_VirMasuk.KodKO AS kodkom,dbo.VBG_VirMasuk.KodPTJ AS kodptjm,  dbo.VBG_VirMasuk.KodKP AS kodkpm, dbo.VBG_VirMasuk.KodVot AS kodvotm,  dbo.VBG_VirMasuk.BG10_Amaun AS jumm, 
                        dbo.VBG_VirMasuk.KodVir AS kodvirm, dbo.BG10_Viremen.BG10_StatusBen, dbo.BG10_Viremen.BG10_StatusKJ, dbo.VBG_VirKeluar.BG10_BakiSms AS bakismsk, MAX(dbo.VBG_VirKeluar.BG12_TkhProses) AS BG09_TkhProses, dbo.VBG_VirMasuk.BG10_BakiSms AS bakismsm,
                        dbo.BG10_Viremen.BG10_RujSuratLulus FROM dbo.BG10_Viremen INNER JOIN dbo.VBG_VirKeluar ON dbo.BG10_Viremen.BG10_NoViremen = dbo.VBG_VirKeluar.BG10_NoViremen INNER JOIN dbo.VBG_VirMasuk ON dbo.BG10_Viremen.BG10_NoViremen = dbo.VBG_VirMasuk.BG10_NoViremen 
                        where right(dbo.BG10_Viremen.BG10_NoViremen,2)='" & Date.Now.Year.ToString.Substring(2, 2) & "' and dbo.VBG_VirKeluar.KodPTJ='" & ViewState("KodPTj") & "' And (dbo.BG10_Viremen.KodStatusDok in ('13','03','05','06','07','08')) 
                        GROUP BY dbo.BG10_Viremen.BG10_NoViremen, dbo.BG10_Viremen.BG10_TkhMohon,dbo.BG10_Viremen.BG10_NoStaf, dbo.BG10_Viremen.BG10_RujSurat,dbo.BG10_Viremen.BG10_Tujuan,dbo.BG10_Viremen.bg10_JnsDsr, dbo.BG10_Viremen.KodStatusDok, dbo.VBG_VirKeluar.KodKw, dbo.VBG_VirKeluar.kodko, dbo.VBG_VirKeluar.KodPTJ, 
                        dbo.VBG_VirKeluar.kodkp, dbo.VBG_VirKeluar.KodVot,  dbo.VBG_VirKeluar.BG10_Amaun, dbo.VBG_VirKeluar.KodVir, dbo.VBG_VirMasuk.KodKw, dbo.VBG_VirMasuk.KodKO, dbo.VBG_VirMasuk.KodPTJ, dbo.VBG_VirMasuk.KodKP, dbo.VBG_VirMasuk.KodVot, 
                        dbo.VBG_VirMasuk.BG10_Amaun, dbo.VBG_VirMasuk.KodVir, dbo.VBG_VirKeluar.BG10_BakiSms, dbo.VBG_VirMasuk.BG10_BakiSms, dbo.BG10_Viremen.BG10_RujSuratLulus, dbo.BG10_Viremen.BG10_StatusBen , dbo.BG10_Viremen.BG10_StatusKJ"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            Dim dvViremen As New DataView
            dvViremen = New DataView(ds.Tables(0))
            ViewState("dvViremen") = dvViremen.Table

            Dim strNoViremen, strTkhMohon, strNoStaf, strRujSurat, strTujuan, strJnsDsr As String
            Dim strKwD, strKOD, strPtjD, strKpD, strObjSbgD, strJumlahD As String
            Dim strKwK, strKOK, strPtjK, strKpK, strObjSbgK, strJumlahK As String
            Dim decJumlahD, decJumlahK As Decimal
            Dim dtTkhMohon As Date
            If Not ds Is Nothing Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    strNoViremen = ds.Tables(0).Rows(i)("BG10_NoViremen")
                    dtTkhMohon = ds.Tables(0).Rows(i)("BG10_TkhMohon")
                    strTkhMohon = dtTkhMohon.ToString("dd/MM/yyyy")
                    strNoStaf = ds.Tables(0).Rows(i)("BG10_NoStaf")
                    strRujSurat = ds.Tables(0).Rows(i)("BG10_RujSurat")
                    strTujuan = ds.Tables(0).Rows(i)("BG10_Tujuan")
                    strJnsDsr = IIf(IsDBNull(ds.Tables(0).Rows(i)("bg10_JnsDsr")), String.Empty, ds.Tables(0).Rows(i)("bg10_JnsDsr"))
                    'strRujSurat = IIf(IsDBNull(ds.Tables(0).Rows(i)("BG10_RujSurat")), String.Empty, ds.Tables(0).Rows(i)("BG10_RujSurat"))
                    'strTujuan = IIf(IsDBNull(ds.Tables(0).Rows(i)("BG10_Tujuan")), String.Empty, ds.Tables(0).Rows(i)("BG10_Tujuan"))

                    strKwD = ds.Tables(0).Rows(i)("kodkwk")
                    strKOD = ds.Tables(0).Rows(i)("kodkok")
                    strPtjD = ds.Tables(0).Rows(i)("kodptjk")
                    strKpD = ds.Tables(0).Rows(i)("kodkpk")
                    strObjSbgD = ds.Tables(0).Rows(i)("kodvotk")
                    decJumlahD = CDec(ds.Tables(0).Rows(i)("jumk"))
                    strJumlahD = decJumlahD.ToString("#,##0.00")

                    strKwK = ds.Tables(0).Rows(i)("kodkwm")
                    strKOK = ds.Tables(0).Rows(i)("kodkom")
                    strPtjK = ds.Tables(0).Rows(i)("kodptjm")
                    strKpK = ds.Tables(0).Rows(i)("kodkpm")
                    strObjSbgK = ds.Tables(0).Rows(i)("kodvotm")
                    decJumlahK = CDec(ds.Tables(0).Rows(i)("jumm"))
                    strJumlahK = decJumlahK.ToString("#,##0.00")

                    dt.Rows.Add(strNoViremen, strTkhMohon, strNoStaf, strRujSurat, strTujuan, strJnsDsr, strKwD, strKOD, strPtjD, strKpD, strObjSbgD, strJumlahD, strKwK, strKOK, strPtjK, strKpK, strObjSbgK, strJumlahK)

                Next

                gvViremen.DataSource = dt
                gvViremen.DataBind()

                intRec = ds.Tables(0).Rows.Count
            Else
                intRec = 0

            End If

            lblJumRekod.InnerText = intRec

            gvViremen.DataSource = dt
            gvViremen.DataBind()
        Catch ex As Exception

        End Try
    End Sub
    Private Function fGetNoVire()
        Try

            Dim strSql As String
            Dim strIdx As String
            Dim intLastIdx As Integer
            Dim intIdx As Integer
            Dim strCol As String
            Dim strTahun As String = Trim(txtTahunVire.Text.TrimEnd)
            Dim strKodModul As String
            Dim strPrefix As String
            Dim strButiran As String = "MAX BG07_VIREMEN"
            Dim strPTJ As String

            strSql = "select * from MK_NoAkhir where tahun ='" & strTahun & "' and KodModul ='BG' and prefix ='VR'"
            strCol = "NoAkhir"
            strKodModul = "BG"
            strPrefix = "VR"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    intLastIdx = CInt(ds.Tables(0).Rows(0)(strCol).ToString)
                Else
                    intLastIdx = 0

                End If

                If intLastIdx = 0 Then
                    strIdx = strPrefix & "000001" & Now.ToString("MM") & Now.ToString("yy")

                    strSql = " insert into mk_noakhir (KodModul , Prefix, NoAkhir, Tahun, Butiran, KodPTJ )" &
                    "values(@kodmodul,@prefix,@noakhir,@tahun,@butiran,@kodPTJ)"
                    Dim paramSql3() As SqlParameter = {
                        New SqlParameter("@kodmodul", strKodModul),
                        New SqlParameter("@prefix", strPrefix),
                        New SqlParameter("@noakhir", 1),
                        New SqlParameter("@tahun", strTahun),
                        New SqlParameter("@butiran", strButiran),
                        New SqlParameter("@kodPTJ", "-")}

                    dbconn = New DBKewConn
                    dbconn.sConnBeginTrans()
                    If dbconn.fInsertCommand(strSql, paramSql3) > 0 Then
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

    Private Sub gvViremen_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvViremen.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then

            Dim row As GridViewRow = New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal)

            Dim cell As TableCell = New TableCell()
            cell.ColumnSpan = 3
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.BackColor = Color.White
            row.Cells.Add(cell)

            cell = New TableCell()
            cell.ColumnSpan = 6
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.Text = "Viremen Keluar"
            row.Cells.Add(cell)

            cell = New TableCell()
            cell.ColumnSpan = 6
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.Text = "Viremen Masuk"
            row.Cells.Add(cell)

            cell = New TableCell()
            cell.ColumnSpan = 2
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.BackColor = Color.White
            row.Cells.Add(cell)

            gvViremen.Controls(0).Controls.AddAt(0, row)

            e.Row.BackColor = ColorTranslator.FromHtml("#FECB18")

        End If
    End Sub
    Private Function virementran()
        Dim strSql As String
        Dim dbconn As New DBKewConn

        Dim strIndVire As String = (Trim(txtNoVire.Text.TrimEnd))
        Dim decTxtBoxA As Decimal = CDec(Trim(txtBoxA.Text.TrimEnd))

        Dim dvViremenTran As New DataView
        dvViremenTran = New DataView(ViewState("dvViremen"))
        'dvViremenTran.RowFilter = "BG11_NoViremen = '" & strIndVire & "'"

        decTxtBoxA = dvViremenTran.Item(0)("bg11_lastperutk").ToString

        strSql = "select bg11_lastperutk from BG11_ViremenTran where BG10_NoViremen ='" & strIndVire & "'"
        Dim ds As New DataSet

        ds = dbconn.fselectCommand(strSql)

        Dim blnFound As Boolean = False
        If Not ds Is Nothing Then
            If ds.Tables(0).Rows.Count > 0 Then
                blnFound = True
            End If
        End If

        If blnFound = False Then
            txtBoxA.Text = decTxtBoxA.ToString("#,##0.00")

        ElseIf blnFound = True Then
        End If



    End Function

    Private Function fBatalV() As Boolean
        Dim dbconn As New DBKewConn
        Try

            Dim strSql As String
            Dim strIndVire As String = Trim(txtNoVire.Text.TrimEnd)
            Dim strStatusDok As String = "08"
            Dim strTkhProses As String = Now.ToString("yyyy-MM-dd")
            Dim strNoStaf As String = Session("ssusrID")

            strSql = "select count(*) from bg10_viremen where BG10_NoViremen = '" & strIndVire & "'"
            If fCheckRec(strSql) > 0 Then

                dbconn.sConnBeginTrans()

                strSql = "update BG10_Viremen set KodStatusDok =@kodstatusdok where BG10_NoViremen =@NoViremen"

                Dim paramSql() As SqlParameter =
                    {
                        New SqlParameter("@NoViremen", strIndVire),
                        New SqlParameter("@KodStatusDok", "08")
                       }

                If Not dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                    dbconn.sConnRollbackTrans()
                End If

                strSql = "select count(*) from BG12_StatusDok where BG10_NoViremen='" & strIndVire & "' and KodStatusDok ='08'"
                If fCheckRec(strSql) > 0 Then
                    strSql = "UPDATE BG12_StatusDok SET KodStatusDok =@kodstatusdok , BG12_TkhProses =@TkhProses, BG12_NoStaf = @NoStaf, BG12_Ulasan =@Ulasan where BG10_NoViremen =@NoViremen"

                    Dim paramSql1() As SqlParameter =
                    {
                    New SqlParameter("@NoViremen", strIndVire),
                        New SqlParameter("@KodStatusDok", "08"),
                        New SqlParameter("@TkhProses", strTkhProses),
                        New SqlParameter("@NoStaf", strNoStaf),
                        New SqlParameter("@Ulasan", "BATAL")
                       }

                    If Not dbconn.fUpdateCommand(strSql, paramSql1) > 0 Then
                        dbconn.sConnRollbackTrans()
                        Return False
                    End If
                Else
                    strSql = "INSERT INTO BG12_StatusDok(BG10_NoViremen,KodStatusDok,BG12_TkhProses,BG12_NoStaf,BG12_Ulasan) " &
                                "VALUES (@NoViremen,@KodStatusDok,@TkhProses,@NoStaf,@Ulasan)"

                    Dim paramSql2() As SqlParameter =
                 {
                New SqlParameter("@NoViremen", strIndVire),
                New SqlParameter("@KodStatusDok", "08"),
                New SqlParameter("@TkhProses", strTkhProses),
                New SqlParameter("@NoStaf", strNoStaf),
                New SqlParameter("@Ulasan", "-")
                }

                    If Not dbconn.fInsertCommand(strSql, paramSql2) > 0 Then
                        dbconn.sConnRollbackTrans()
                        Return False
                    End If
                End If
            End If

            dbconn.sConnCommitTrans()
            Return True
        Catch ex As Exception
            dbconn.sConnRollbackTrans()
            Return False
        End Try
    End Function





    Private Sub gvViremen_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvViremen.RowCommand
        Try
            If e.CommandName = "Select" Then
                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvViremen.Rows(index)

                Dim strNoViremen As String = selectedRow.Cells(1).Text
                Dim dvViremen As New DataView
                dvViremen = New DataView(ViewState("dvViremen"))
                dvViremen.RowFilter = "BG10_NoViremen = '" & strNoViremen & "'"

                txtNoVire.Text = strNoViremen
                Dim intTahun As Integer = CInt(Trim(txtTahunVire.Text.TrimEnd))
                Dim strTarikh As String = Now.ToString("yyyy-MM-dd")
                Dim strTkhMohon As String = dvViremen.Item(0)("BG10_TkhMohon").ToString
                Dim strNoStafPmhn As String = dvViremen.Item(0)("BG10_NoStaf").ToString

                Dim strNamaPmhn, strJawPmhn, strKodPTjPmhn, strPTjPmhn As String

                Dim dt As New DataTable
                dt = fGetUserInfo(strNoStafPmhn)
                If dt.Rows.Count > 0 Then
                    strNamaPmhn = dt.Rows.Item(0).Item("MS01_Nama")
                    strJawPmhn = dt.Rows.Item(0).Item("JawGiliran")
                    strKodPTjPmhn = dt.Rows.Item(0).Item("KodPejabat") & "0000"
                    strPTjPmhn = dt.Rows.Item(0).Item("Pejabat")
                End If
                Dim strJnsDsr As String = dvViremen.Item(0)("BG10_JnsDsr").ToString
                Dim strTujuan As String = dvViremen.Item(0)("BG10_Tujuan").ToString
                Dim strRujSurat As String = dvViremen.Item(0)("BG10_RujSurat").ToString

                'VIREMEN KELUAR
                Dim strKwD As String = dvViremen.Item(0)("kodkwk").ToString
                Dim strKOD As String = dvViremen.Item(0)("kodkok").ToString
                Dim strPtjD As String = dvViremen.Item(0)("kodptjk").ToString
                Dim strKpD As String = dvViremen.Item(0)("kodkpk").ToString.TrimEnd
                Dim strObjSbgD As String = dvViremen.Item(0)("kodvotk").ToString
                Dim decJumlahD As Decimal = dvViremen.Item(0)("jumk").ToString
                ' Dim strBakiSmsD As String = decJumlahD.ToString("#,##0.00")
                Dim decBakiSmsD As Decimal = CDec(fGetBakiSebenar(intTahun, strTarikh, strKwD, strKOD, strPtjD, strKpD, strObjSbgD))
                Dim strBakiSlpsD As String = FormatNumber(decBakiSmsD - decJumlahD, 2)


                'VIREMEN MASUK
                Dim strKwK As String = dvViremen.Item(0)("kodkwm").ToString
                Dim strKOK As String = dvViremen.Item(0)("kodkom").ToString.TrimEnd
                Dim strPtjK As String = dvViremen.Item(0)("kodptjm").ToString
                Dim kodPtjK = strPtjK.Substring(0, 2)
                Dim strKpK As String = dvViremen.Item(0)("kodkpm").ToString.TrimEnd
                Dim strObjSbgK As String = dvViremen.Item(0)("kodvotm").ToString
                Dim decJumlahK As Decimal = dvViremen.Item(0)("jumm").ToString
                'Dim strBakiSmsT As String = decJumlahK.ToString("#,##0.00")
                Dim decSmsBakiK As Decimal = CDec(fGetBakiSebenar(intTahun, strTarikh, strKwK, strKOK, strPtjK, strKpK, strObjSbgK))
                Dim strBakiSlpsK As String = FormatNumber(decSmsBakiK + decJumlahK, 2)

                Dim strStatus As String = dvViremen.Item(0)("Status").ToString
                Dim strKodStatus As String = dvViremen.Item(0)("KodStatusDok").ToString


                txtNoStaf.Text = strNoStafPmhn
                txtNamaPemohon.Text = strNamaPmhn
                txtJawPemohon.Text = strJawPmhn

                txtStatus.Text = strStatus

                HidKodStatus.Value = strKodStatus
                txtTkhMohon.Text = strTkhMohon
                txtTujuan.Text = strTujuan
                'fBindDdlJenisDasar()
                'ddlJenDasar.SelectedValue = strJnsDsr
                ddlKWk.SelectedValue = strKwD
                fBindDdlKodOperasi(1, strKwD)
                ddlKOk.SelectedValue = strKOD
                fBindDdlPtj(1, strKOD)
                ddlPTJk.SelectedValue = strPtjD
                fBindDdlKP(1, strPtjD)
                ddlKPk.SelectedValue = strKpD
                fBindDdlObjSebagai(1, strKpD)
                ddlObjSbgK.SelectedValue = strObjSbgD
                txtRujSurat.Text = strRujSurat
                ' txtJumVireK.Text = decJumlahD

                txtBakiSmsK.Text = FormatNumber(decBakiSmsD, 2) 'decBakiSmsD.ToString("#,##0.00")

                ddlKWm.SelectedValue = strKwK
                fBindDdlKodOperasi(2, strKwK)
                ddlKOm.SelectedValue = strKOK
                fBindDdlPtj(2, strKOK)
                ddlPTJm.SelectedValue = strPtjK
                fBindDdlKP(2, strPtjK)
                ddlKPm.SelectedValue = strKpK
                fBindDdlObjSebagai(2, strKpK)
                ddlObjSbgM.SelectedValue = strObjSbgK
                'txtJumVireM.Text = decJumlahK
                txtJumVire.Text = FormatNumber(decJumlahD, 0)

                txtBakiSmsM.Text = FormatNumber(decSmsBakiK, 2)  'decBakiK.ToString("#,##0.00")

                txtBakiSlpsVireK.Text = strBakiSlpsD
                txtBakiSlpsVireM.Text = strBakiSlpsK

                'CERAKINKAN

                Dim dbconn As New DBKewConn
                Dim strSql As String
                Dim ds As New DataSet
                'strSql = "select * from BG11_ViremenTran where BG10_NoViremen='" & strNoViremen & "'"
                strSql = "select bg11_lastperutk,bg11_lastblj,bg11_curperutkasal,bg11_curtmbh,bg11_curkurang,bg11_curblj,bg11_curbakitng,bg11_tngbaru,bg11_curwarsekatan from BG11_ViremenTran where BG10_NoViremen='" & strNoViremen & "'"

                ds = dbconn.fSelectCommand(strSql)
                If Not ds Is Nothing Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        txtBoxA.Text = FormatNumber(ds.Tables(0).Rows(0)("bg11_lastperutk").ToString, 2)
                        txtBoxB.Text = FormatNumber(ds.Tables(0).Rows(0)("bg11_lastblj").ToString, 2)
                        txtBoxA1.Text = FormatNumber(ds.Tables(0).Rows(0)("bg11_curperutkasal").ToString, 2)
                        txtBoxB1.Text = FormatNumber(ds.Tables(0).Rows(0)("bg11_curtmbh").ToString, 2)
                        txtBoxC.Text = FormatNumber(ds.Tables(0).Rows(0)("bg11_curkurang").ToString, 2)

                        Dim decBoxD As Decimal = ds.Tables(0).Rows(0)("bg11_curperutkasal") + ds.Tables(0).Rows(0)("bg11_curtmbh") - ds.Tables(0).Rows(0)("bg11_curkurang")
                        txtBoxD.Text = FormatNumber(decBoxD.ToString, 2)
                        txtBoxE.Text = FormatNumber(ds.Tables(0).Rows(0)("bg11_curblj").ToString, 2)
                        txtBoxF.Text = FormatNumber(ds.Tables(0).Rows(0)("bg11_curbakitng").ToString, 2)
                        txtBoxG.Text = FormatNumber(ds.Tables(0).Rows(0)("bg11_tngbaru").ToString, 2)

                        Dim decBoxH As Decimal = ds.Tables(0).Rows(0)("bg11_curblj") + ds.Tables(0).Rows(0)("bg11_curbakitng") + ds.Tables(0).Rows(0)("bg11_tngbaru")
                        txtBoxH.Text = FormatNumber(decBoxH.ToString, 2)

                        txtBoxI.Text = FormatNumber(ds.Tables(0).Rows(0)("bg11_curwarsekatan").ToString, 2)
                    End If

                End If

                ViewState("savemode") = 2

                If HidKodStatus.Value = "03" OrElse HidKodStatus.Value = "06" OrElse HidKodStatus.Value = "05" OrElse HidKodStatus.Value = "07" OrElse HidKodStatus.Value = "08" Then
                    lbtnHapus.Visible = False
                    lbtnSimpan.Visible = False
                    txtJumVire.Enabled = False
                Else
                    lbtnHapus.Visible = True
                    lbtnSimpan.Visible = True
                    txtJumVire.Enabled = True
                End If
            End If

            mpeLst.Hide()
        Catch ex As Exception

        End Try

    End Sub

    'Private Sub lbtnHantar_Click(sender As Object, e As EventArgs) Handles lbtnHantar.Click

    '    Dim strSql As String
    '    Dim strTkhToday As String = Now.ToString("yyyy-MM-dd")
    '    Dim strNoStaf As String = Trim(txtNoStaf.Text.TrimEnd)
    '    Dim blnSuccess As Boolean = True

    '    Dim dbconn As New DBKewConn
    '    Try
    '        If gvViremen.Rows.Count > 0 Then

    '            strSql = "select BG10_NoViremen  from BG10_Viremen where KodStatusDok = 12"
    '            Dim ds As New DataSet
    '            ds = fGetRec(strSql)

    '            If Not ds Is Nothing Then
    '                If ds.Tables(0).Rows.Count > 0 Then
    '                    dbconn.sConnBeginTrans()
    '                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
    '                        Dim strNoViremen As String = ds.Tables(0).Rows(i)("BG10_NoViremen").ToString

    '                        strSql = "update BG10_Viremen set KodStatusDok = 13 where BG10_NoViremen = @NoViremen"
    '                        Dim paramSql() As SqlParameter =
    '                            {
    '                                New SqlParameter("@NoViremen", strNoViremen),
    '                                New SqlParameter("@KodStatusDok", "13")
    '                            }
    '                        If Not dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
    '                            blnSuccess = False
    '                            Exit Try
    '                        End If

    '                        strSql = "INSERT INTO BG12_StatusDok(BG10_NoViremen,KodStatusDok,BG12_TkhProses,BG12_NoStaf,BG12_Ulasan) " &
    '                                                "VALUES (@NoViremen,@KodStatusDok,@TkhProses,@NoStaf,@Ulasan)"

    '                        Dim paramSql2() As SqlParameter =
    '                            {
    '                                New SqlParameter("@NoViremen", strNoViremen),
    '                                New SqlParameter("@KodStatusDok", "13"),
    '                                New SqlParameter("@TkhProses", strTkhToday),
    '                                New SqlParameter("@NoStaf", strNoStaf),
    '                                New SqlParameter("@Ulasan", "-")
    '                               }

    '                        If Not dbconn.fInsertCommand(strSql, paramSql2) > 0 Then
    '                            blnSuccess = False
    '                            Exit Try
    '                        End If

    '                    Next
    '                End If
    '            Else
    '                fGlobalAlert("Tiada rekod permohonan viremen baru!", Me.Page, Me.[GetType]())
    '                Exit Sub
    '            End If

    '        Else
    '            fGlobalAlert("Tiada rekod permohonan viremen baru!", Me.Page, Me.[GetType]())
    '            Exit Sub
    '        End If

    '    Catch ex As Exception
    '        blnSuccess = False
    '    End Try
    '    If blnSuccess Then
    '        dbconn.sConnCommitTrans()
    '        fGlobalAlert("Maklumat permohonan viremen telah dihantar!", Me.Page, Me.[GetType]())
    '        fBindGViremen("'02','12','13'")
    '    Else
    '        dbconn.sConnRollbackTrans()
    '        fGlobalAlert("Maklumat permohonan viremen gagal dihantar!", Me.Page, Me.[GetType]())
    '    End If


    'End Sub

    Protected Sub ddlKWk_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKWk.SelectedIndexChanged
        Dim strkodkW As String
        strkodkW = ddlKWk.SelectedValue
        ddlKWm.SelectedValue = strkodkW

        fBindDdlKodOperasi(1, strkodkW)
        fBindDdlKodOperasi(2, strkodkW)
    End Sub

    Protected Sub ddlKOk_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKOk.SelectedIndexChanged
        Dim strkodkO As String
        strkodkO = ddlKOk.SelectedValue

        If ViewState("poweruser") Then
            fBindDdlPtj(1, strkodkO)
        Else
            fBindDdlPtj(1, strkodkO)
            ddlPTJk.SelectedValue = Session("ssusrKodPTj")
            fBindDdlKP(1, Session("ssusrKodPTj"))
        End If
        'fBindDdlPtj(1, strkodkO)
    End Sub

    Protected Sub ddlPTJk_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPTJk.SelectedIndexChanged
        Dim strkodPTJ As String
        strkodPTJ = ddlPTJk.SelectedValue
        fBindDdlKP(1, strkodPTJ)
    End Sub

    Protected Sub ddlKPk_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKPk.SelectedIndexChanged
        Dim strkodKP As String
        strkodKP = ddlKPk.SelectedValue
        fBindDdlObjSebagai(1, strkodKP)
    End Sub

    Protected Sub ddlKWm_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKWm.SelectedIndexChanged
        Dim strkodkW As String
        strkodkW = ddlKWm.SelectedValue

        If ViewState("poweruser") Then
            fBindDdlKodOperasi(2, strkodkW)
        Else
            fBindDdlKodOperasi(2, strkodkW)
            ddlKWm.SelectedValue = strkodkW

        End If
    End Sub

    Protected Sub ddlKOm_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKOm.SelectedIndexChanged

        Dim strkodkO As String
        strkodkO = ddlKOm.SelectedValue
        'fBindDdlPtj(2, strkodkO)

        If ViewState("poweruser") Then
            fBindDdlPtj(2, strkodkO)
        Else
            fBindDdlPtj(2, strkodkO)
            ddlPTJm.SelectedValue = Session("ssusrKodPTj")
            fBindDdlKP(2, Session("ssusrKodPTj"))
        End If
    End Sub

    Protected Sub ddlPTJm_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPTJm.SelectedIndexChanged

        Dim decJumD As Decimal
        Dim decBakiSmsD As Decimal
        Dim strkodPTJ As String
        Try

            strkodPTJ = ddlPTJm.SelectedValue

            If txtJumVire.Text = "" Then
                decJumD = 0
            Else
                decJumD = CDec(Trim(txtJumVire.Text.TrimEnd))
            End If

            If txtBakiSmsK.Text = "" Then
                decBakiSmsD = 0
            Else
                decBakiSmsD = CDec(Trim(txtBakiSmsK.Text.TrimEnd))
            End If



            'Dim decJumBakiSlpsD As Decimal = CDbl(Trim(txtBakiSlpsVireK.Text.TrimEnd))
            'decJumBakiSlpsD = decJumD - decBakiSmsD


            fBindDdlKP(2, strkodPTJ)
        Catch ex As Exception

        End Try


    End Sub

    Protected Sub ddlKPm_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKPm.SelectedIndexChanged
        Dim strkodKP As String
        strkodKP = ddlKPm.SelectedValue
        fBindDdlObjSebagai(2, strkodKP)
    End Sub

    Private Sub fDisable()
        Try
            ddlPTJk.Enabled = False
            ddlPTJm.Enabled = False
            ddlKWm.Enabled = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub fEnable()
        Try
            ddlPTJk.Enabled = True
            ddlPTJm.Enabled = True
            ddlKWm.Enabled = True
        Catch ex As Exception

        End Try
    End Sub


    Private Sub fReset()

        Try
            ViewState("savemode") = 1
            ddlKWk.Items.Clear()
            ddlKWk.Items.Add(New ListItem("- SILA PILIH KUMPULAN WANG -", 0))


            ddlKOk.Items.Clear()
            ddlKOk.Items.Add(New ListItem("- SILA PILIH KOD OPERASI -", 0))


            ddlKPk.Items.Clear()
            ddlKPk.Items.Add(New ListItem("- SILA PILIH KOD PROJEK -", 0))

            ddlObjSbgK.Items.Clear()
            ddlObjSbgK.Items.Add(New ListItem("- SILA PILIH OBJEK SEBAGAI -", 0))

            ddlKWm.Items.Clear()
            ddlKWm.Items.Add(New ListItem("- SILA PILIH KUMPULAN WANG -", 0))

            fBindDdlKumpWang()

            ddlKOm.Items.Clear()
            ddlKOm.Items.Add(New ListItem("- SILA PILIH KOD OPERASI -", 0))

            ddlPTJm.Items.Clear()
            ddlPTJm.Items.Add(New ListItem("- SILA PILIH PTJ -", 0))

            ddlKPm.Items.Clear()
            ddlKPm.Items.Add(New ListItem("- SILA PILIH KOD PROJEK -", 0))

            ddlObjSbgM.Items.Clear()
            ddlObjSbgM.Items.Add(New ListItem("- SILA PILIH OBJEK SEBAGAI -", 0))

            txtNoVire.Text = ""
            txtTujuan.Text = ""
            txtRujSurat.Text = ""
            txtJumVire.Text = "0.00"
            txtBakiSmsK.Text = "0.00"
            txtBakiSlpsVireK.Text = "0.00"
            txtBakiSmsM.Text = "0.00"

            txtBakiSlpsVireM.Text = "0.00"
            txtBoxA.Text = "0.00"
            txtBoxB.Text = "0.00"
            txtBoxA1.Text = "0.00"
            txtBoxB1.Text = "0.00"
            txtBoxC.Text = "0.00"
            txtBoxD.Text = "0.00"
            txtBoxE.Text = "0.00"
            txtBoxF.Text = "0.00"
            txtBoxG.Text = "0.00"
            txtBoxH.Text = "0.00"
            txtBoxI.Text = "0.00"
            HidKodStatus.Value = ""

            txtJumVire.Text = ""

            lbtnSimpan.Visible = True

            txtTkhMohon.Text = Now.ToString("dd/MM/yyyy")
            txtJumVire.Enabled = False
            lbtnHapus.Visible = False
            txtStatus.Text = "PERMOHONAN BARU"
        Catch ex As Exception

        End Try
    End Sub

    Private Function fSimpan() As Boolean
        Dim blnSuccess As Boolean = True
        Dim dbconn As New DBKewConn

        Try

            Dim strSql As String

            Dim strTkhMohon As String = Now.ToString("yyyy-MM-dd")
            Dim strNoStaf As String = Trim(txtNoStaf.Text.TrimEnd)
            Dim strTahun As String = Trim(txtTahunVire.Text.TrimEnd)
            Dim strTujuan As String = Trim(txtTujuan.Text.TrimEnd)

            Dim strKwD As String = Trim(ddlKWk.SelectedValue.TrimEnd)
            Dim strKOD As String = Trim(ddlKOk.SelectedValue.TrimEnd)
            Dim strPtjD As String = Trim(ddlPTJk.SelectedValue.TrimEnd)
            Dim strKpD As String = Trim(ddlKPk.SelectedValue.TrimEnd)
            Dim strObjSbgD As String = Trim(ddlObjSbgK.SelectedValue.TrimEnd)
            Dim strRujSurat As String = Trim(txtRujSurat.Text.TrimEnd)
            Dim decBakiSmsD As Decimal = CDec(Trim(txtBakiSmsK.Text.TrimEnd))
            Dim decJumlahD As Decimal = CDec(Trim(txtJumVire.Text.TrimEnd))
            'Trim(txtJum.Text.TrimEnd)

            Dim strKwK As String = Trim(ddlKWm.SelectedValue.TrimEnd)
            Dim strKOK As String = Trim(ddlKOm.SelectedValue.TrimEnd)
            Dim strPtjK As String = Trim(ddlPTJm.SelectedValue.TrimEnd)
            Dim strKpK As String = Trim(ddlKPm.SelectedValue.TrimEnd)
            Dim strObjSbgK As String = Trim(ddlObjSbgM.SelectedValue.TrimEnd)
            Dim decBakiSmsK As Decimal = CDec(Trim(txtBakiSmsM.Text.TrimEnd))
            Dim decJumlahK As Decimal = CDec(Trim(txtJumVire.Text.TrimEnd))

            Dim decLstPerutk As Decimal = CDec(Trim(txtBoxA.Text.TrimEnd))
            Dim decLastBlj As Decimal = CDec(Trim(txtBoxB.Text.TrimEnd))
            Dim decCurPerutkAsal As Decimal = CDec(Trim(txtBoxA1.Text.TrimEnd))
            Dim decCurTmbh As Decimal = CDec(Trim(txtBoxB1.Text.TrimEnd))
            Dim decCurkurang As Decimal = CDec(Trim(txtBoxC.Text.TrimEnd))
            Dim decCurBlj As Decimal = CDec(Trim(txtBoxE.Text.TrimEnd))
            Dim decCurBakiTng As Decimal = CDec(Trim(txtBoxF.Text.TrimEnd))
            Dim decTngBaru As Decimal = CDec(Trim(txtBoxG.Text.TrimEnd))
            Dim decCurWarSekatan As Decimal = CDec(Trim(txtBoxI.Text.TrimEnd))

            Dim strStatusDok As String = "12"
            dbconn.sConnBeginTrans()

            'Insert into BG10_Viremen
            Dim strIndVire As String = fGetNoVire()
            strSql = "insert into BG10_Viremen (BG10_NoViremen ,BG10_Amaun,BG10_StatusKJ ,BG10_StatusBen ,KodPTj ,BG10_Tahun ,BG10_NoStaf ,BG10_TkhMohon ,KodStatusDok ,BG10_RujSurat ,BG10_RujSuratLulus ,BG10_Tujuan)" &
                      "VALUES (@NoViremen,@Amaun,@StatusKJ,@StatusBen,@KodPtj,@Tahun,@NoStaf,@TkhMohon,@KodStatusDok,@RujSurat,@RujSuratLulus,@Tujuan)"

            Dim paramSql() As SqlParameter =
            {
                New SqlParameter("@NoViremen", strIndVire),
                New SqlParameter("@Amaun", decJumlahD),
                New SqlParameter("@StatusKJ", 0),
                New SqlParameter("@StatusBen", 0),
                New SqlParameter("@KodPtj", strPtjD),
                New SqlParameter("@Tahun", strTahun),
                New SqlParameter("@NoStaf", strNoStaf),
                New SqlParameter("@TkhMohon", strTkhMohon),
                New SqlParameter("@KodStatusDok", strStatusDok),
                New SqlParameter("@RujSurat", Trim(txtRujSurat.Text.TrimEnd)),
                New SqlParameter("@RujSuratLulus", ""),
                New SqlParameter("@Tujuan", Trim(txtTujuan.Text.TrimEnd))
                }


            If Not dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                blnSuccess = False
            End If

            'Insert BG10_ViremenDT
            'Kod vire K'
            strSql = "insert into BG10_ViremenDT (BG10_NoViremen , KodKW ,KodKO ,KodPTJ ,KodKP ,KodVot ,BG10_BakiSms ,KodVir )" &
                        "values(@NoViremen,@Kw,@KodKO,@KodPTJ,@KodKP,@KodVot,@BakiSms,@KodVir)"


            Dim paramSql2() As SqlParameter =
            {
                New SqlParameter("@NoViremen", strIndVire),
                New SqlParameter("@Kw", strKwD),
                New SqlParameter("@KodKO", strKOD),
                New SqlParameter("@KodPTJ", strPtjD),
                New SqlParameter("@KodKP", strKpD),
                New SqlParameter("@KodVot", strObjSbgD),
                New SqlParameter("@BakiSms", decBakiSmsD),
                New SqlParameter("@KodVir", "K")
                }


            If Not dbconn.fInsertCommand(strSql, paramSql2) > 0 Then
                blnSuccess = False
            End If

            'Kod vire M'
            strSql = "insert into BG10_ViremenDT (BG10_NoViremen , KodKW ,KodKO ,KodPTJ ,KodKP ,KodVot ,BG10_BakiSms ,KodVir )" &
                        "values(@NoViremen,@Kw,@KodKO,@KodPTJ,@KodKP,@KodVot,@BakiSms,@KodVir)"


            Dim paramSql3() As SqlParameter =
            {
                New SqlParameter("@NoViremen", strIndVire),
                New SqlParameter("@Kw", strKwK),
                New SqlParameter("@KodKO", strKOK),
                New SqlParameter("@KodPTJ", strPtjK),
                New SqlParameter("@KodKP", strKpK),
                New SqlParameter("@KodVot", strObjSbgK),
                New SqlParameter("@BakiSms", decBakiSmsK),
                New SqlParameter("@KodVir", "M")
                }


            If Not dbconn.fInsertCommand(strSql, paramSql3) > 0 Then
                blnSuccess = False
            End If

            strSql = "select count(*) from BG11_ViremenTran where BG10_NoViremen ='" & strIndVire & "'"
            If fCheckRec(strSql) > 0 Then
                'update BG11_ViremenTran
                strSql = "update BG11_ViremenTran Set BG11_LastPerutk =@LstPerutk ,BG11_LastBlj=@LastBlj ,BG11_CurPerutkAsal=@CurPerutkAsal,BG11_CurTmbh=@CurTmbh,BG11_CurKurang=@CurKurang,BG11_CurBlj=@CurBlj,BG11_CurBakiTng=@CurBakiTng,BG11_TngBaru=@TngBaru,BG11_CurWarSekatan=@CurWarSekatan where BG10_NoViremen= @NoViremen"


                Dim paramSql5() As SqlParameter =
            {
                New SqlParameter("@NoViremen", strIndVire),
                New SqlParameter("@LstPerutk", decLstPerutk),
                New SqlParameter("@LastBlj", decLastBlj),
                New SqlParameter("@CurPerutkAsal", decCurPerutkAsal),
                 New SqlParameter("@CurTmbh", decCurTmbh),
                New SqlParameter("@CurKurang", decCurkurang),
                New SqlParameter("@CurBlj", decCurBlj),
                New SqlParameter("@CurBakiTng", decCurBakiTng),
                New SqlParameter("@TngBaru", decTngBaru),
                New SqlParameter("@CurWarSekatan", decCurWarSekatan)
               }


                If Not dbconn.fUpdateCommand(strSql, paramSql5) > 0 Then
                    blnSuccess = False
                End If
            Else
                strSql = "INSERT INTO BG11_ViremenTran (BG10_NoViremen,BG11_LastPerutk,BG11_LastBlj,BG11_CurPerutkAsal,BG11_CurTmbh,BG11_CurKurang,BG11_CurBlj,BG11_CurBakiTng,BG11_TngBaru,BG11_CurWarSekatan)" &
                    "VALUES(@NoViremen,@LstPerutk,@LastBlj,@CurPerutkAsal,@CurTmbh,@CurKurang,@CurBlj,@CurBakiTng,@TngBaru,@CurWarSekatan)"

                Dim paramSql4() As SqlParameter =
            {
                New SqlParameter("@NoViremen", strIndVire),
                New SqlParameter("@LstPerutk", decLstPerutk),
                New SqlParameter("@LastBlj", decLastBlj),
                New SqlParameter("@CurPerutkAsal", decCurPerutkAsal),
                 New SqlParameter("@CurTmbh", decCurTmbh),
                New SqlParameter("@CurKurang", decCurkurang),
                New SqlParameter("@CurBlj", decCurBlj),
                New SqlParameter("@CurBakiTng", decCurBakiTng),
                New SqlParameter("@TngBaru", decTngBaru),
                New SqlParameter("@CurWarSekatan", decCurWarSekatan)
               }


                If Not dbconn.fInsertCommand(strSql, paramSql4) > 0 Then
                    blnSuccess = False
                End If
            End If

            'Insert Into BG12_StatusDok
            strSql = "INSERT INTO BG12_StatusDok(BG10_NoViremen,KodStatusDok,BG12_TkhProses,BG12_NoStaf,BG12_Ulasan) " &
              "VALUES (@NoViremen,@KodStatusDok,@TkhProses,@NoStaf,@Ulasan)"

            Dim paramSql7() As SqlParameter =
            {
                New SqlParameter("@NoViremen", strIndVire),
                New SqlParameter("@KodStatusDok", strStatusDok),
                New SqlParameter("@TkhProses", strTkhMohon),
                New SqlParameter("@NoStaf", strNoStaf),
                New SqlParameter("@Ulasan", "-")
               }

            If Not dbconn.fInsertCommand(strSql, paramSql7) > 0 Then
                blnSuccess = False
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

    Protected Sub lbtnHapus_Click(sender As Object, e As EventArgs) Handles lbtnHapus.Click
        If fBatalV() Then
            fGlobalAlert("Permohonan viremen telah dihapuskan!", Me.Page, Me.[GetType]())
            fReset()
        Else
            fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
        End If
    End Sub

    Protected Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.Click

        If txtBakiSlpsVireK.Text < 0 Then
            fGlobalAlert("Baki Selepas Viremen untuk Objek Sebagai di bahagian 'Daripada' tidak dibenarkan menjadi negatif! Sila masukkan 'Jumlah Viremen' semula.", Me.Page, Me.[GetType]())
            Exit Sub
        End If

        If txtJumVire.Text = "0.00" Then
            fGlobalAlert("Sila masukkan Jumlah Viremen!", Me.Page, Me.[GetType]())
            Exit Sub
        End If

        If ViewState("savemode") = 1 Then
            'REKOD BARU
            If fSimpan() = True Then
                fGlobalAlert("Permohonan viremen telah disimpan!", Me.Page, Me.[GetType]())
                fReset()
            Else
                fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
            End If
        ElseIf ViewState("savemode") = 2 Then
            'KEMAS KINI
            If fKemasKini() Then
                fGlobalAlert("Permohonan viremen telah disimpan!", Me.Page, Me.[GetType]())
                fReset()
            Else
                fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
            End If
        End If

        If ViewState("poweruser") = False Then
            fDisable()
        End If

    End Sub

    Private Sub lbtnBaru_Click(sender As Object, e As EventArgs) Handles lbtnBaru.Click
        fReset()
    End Sub



    Private Function fKemasKini() As Boolean

        Dim strSql As String
        Dim dbConn As New DBKewConn
        Dim blnSuccess As Boolean = True

        Try
            Dim strNoViremen As String = Trim(txtNoVire.Text.TrimEnd)
            Dim strJumVireK As Decimal = CDec(Trim(txtJumVire.Text.TrimEnd))
            Dim strTkhMohon As String = Now.ToString("yyyy-MM-dd")
            Dim strNoStaf As String = Trim(txtNoStaf.Text.TrimEnd)
            Dim strTahun As String = Trim(txtTahunVire.Text.TrimEnd)
            Dim strTujuan As String = Trim(txtTujuan.Text.TrimEnd)

            Dim strKwK As String = Trim(ddlKWk.SelectedValue.TrimEnd)
            Dim strKOK As String = Trim(ddlKOk.SelectedValue.TrimEnd)
            Dim strPtjK As String = Trim(ddlPTJk.SelectedValue.TrimEnd)
            Dim strKpK As String = Trim(ddlKPk.SelectedValue.TrimEnd)
            Dim strObjSbgK As String = Trim(ddlObjSbgK.SelectedValue.TrimEnd)
            Dim strRujSurat As String = Trim(txtRujSurat.Text.TrimEnd)
            Dim decBakiSmsK As Decimal = CDec(Trim(txtBakiSmsK.Text.TrimEnd))

            Dim strKwM As String = Trim(ddlKWm.SelectedValue.TrimEnd)
            Dim strKOM As String = Trim(ddlKOm.SelectedValue.TrimEnd)
            Dim strPtjM As String = Trim(ddlPTJm.SelectedValue.TrimEnd)
            Dim strKpM As String = Trim(ddlKPm.SelectedValue.TrimEnd)
            Dim strObjSbgM As String = Trim(ddlObjSbgM.SelectedValue.TrimEnd)
            Dim decBakiSmsM As Decimal = CDec(Trim(txtBakiSmsM.Text.TrimEnd))

            Dim decLstPerutk As Decimal = CDec(Trim(txtBoxA.Text.TrimEnd))
            Dim decLastBlj As Decimal = CDec(Trim(txtBoxB.Text.TrimEnd))
            Dim decCurPerutkAsal As Decimal = CDec(Trim(txtBoxA1.Text.TrimEnd))
            Dim decCurTmbh As Decimal = CDec(Trim(txtBoxB1.Text.TrimEnd))
            Dim decCurkurang As Decimal = CDec(Trim(txtBoxC.Text.TrimEnd))
            Dim decCurBlj As Decimal = CDec(Trim(txtBoxE.Text.TrimEnd))
            Dim decCurBakiTng As Decimal = CDec(Trim(txtBoxF.Text.TrimEnd))
            Dim decTngBaru As Decimal = CDec(Trim(txtBoxG.Text.TrimEnd))
            Dim decCurWarSekatan As Decimal = CDec(Trim(txtBoxI.Text.TrimEnd))

            Dim strStatDok, strStatKJ As String
            If HidKodStatus.Value = "16" Then
                strStatDok = "02"
                strStatKJ = "0"
            Else
                strStatDok = "12"
                strStatKJ = "0"
            End If

            Try
                'KEMAS KINI BG10_Viremen
                dbConn.sConnBeginTrans()
                strSql = "UPDATE BG10_Viremen SET BG10_Amaun=@Amaun,BG10_StatusKJ=@StatusKJ,BG10_StatusBen=@StatusBen,BG10_NoStaf=@NoStaf,kodstatusdok=@KodStatusDok,BG10_RujSurat=@RujSurat,BG10_Tujuan=@Tujuan where BG10_NoViremen =@NoViremen"

                Dim paramSql7() As SqlParameter =
                {
                    New SqlParameter("@NoViremen", strNoViremen),
                    New SqlParameter("@Amaun", strJumVireK),
                    New SqlParameter("@StatusKJ", strStatKJ),
                    New SqlParameter("@StatusBen", 0),
                    New SqlParameter("@NoStaf", strNoStaf),
                    New SqlParameter("@KodStatusDok", strStatDok),
                    New SqlParameter("@RujSurat", Trim(txtRujSurat.Text.TrimEnd)),
                   New SqlParameter("@Tujuan", Trim(txtTujuan.Text.TrimEnd))
                }

                If Not dbConn.fUpdateCommand(strSql, paramSql7) > 0 Then
                    blnSuccess = False
                    Exit Try
                End If

                ''Update BG10_ViremenDT
                ''Kod vire K'
                strSql = "UPDATE BG10_ViremenDT SET KodKW =@Kw, KodKO =@KodKO , kodPTJ = @KodPTJ , KodKP =@KodKP , KodVot = @KodVot , BG10_BakiSms =@BakiSms where BG10_NoViremen =@NoViremen and KodVir =@KodVir"

                Dim paramSql8() As SqlParameter =
                {
                    New SqlParameter("@NoViremen", strNoViremen),
                    New SqlParameter("@Kw", strKwK),
                    New SqlParameter("@KodKO", strKOK),
                    New SqlParameter("@KodPTJ", strPtjK),
                    New SqlParameter("@KodKP", strKpK),
                    New SqlParameter("@KodVot", strObjSbgK),
                    New SqlParameter("@BakiSms", decBakiSmsK),
                    New SqlParameter("@KodVir", "K")
                    }

                If Not dbConn.fUpdateCommand(strSql, paramSql8) > 0 Then
                    blnSuccess = False
                    Exit Try
                End If

                'Kod vire M'
                strSql = "UPDATE BG10_ViremenDT SET KodKW =@Kw , KodKO =@KodKO , kodPTJ = @KodPTJ , KodKP =@KodKP , KodVot = @KodVot , BG10_BakiSms =@BakiSms where BG10_NoViremen =@NoViremen and KodVir =@KodVir"
                '"UPDATE BG10_ViremenDT SET BG10_NoViremen =@NoViremen where KodKW =@Kw and KodKO =@KodKO and kodPTJ = @KodPTJ and KodKP =@KodKP and KodVot = @KodVot and BG10_BakiSms =@BakiSms and KodVir =@KodVir"

                Dim paramSql9() As SqlParameter =
                {
                    New SqlParameter("@NoViremen", strNoViremen),
                    New SqlParameter("@Kw", strKwM),
                    New SqlParameter("@KodKO", strKOM),
                    New SqlParameter("@KodPTJ", strPtjM),
                    New SqlParameter("@KodKP", strKpM),
                    New SqlParameter("@KodVot", strObjSbgM),
                    New SqlParameter("@BakiSms", decBakiSmsM),
                    New SqlParameter("@KodVir", "M")
                    }

                If Not dbConn.fUpdateCommand(strSql, paramSql9) > 0 Then
                    blnSuccess = False
                    Exit Try
                End If

                strSql = "select count(*) from BG11_ViremenTran with (nolock) where BG10_NoViremen ='" & strNoViremen & "'"
                If fCheckRec(strSql) > 0 Then
                    'UPDATE BG11_ViremenTran
                    strSql = "update BG11_ViremenTran Set BG11_LastPerutk=@LstPerutk, BG11_LastBlj=@LastBlj, BG11_CurPerutkAsal=@CurPerutkAsal,BG11_CurTmbh=@CurTmbh, BG11_CurKurang=@CurKurang, BG11_CurBlj=@CurBlj, BG11_CurBakiTng=@CurBakiTng, BG11_TngBaru= @TngBaru, BG11_CurWarSekatan= @CurWarSekatan where BG10_NoViremen= @NoViremen"

                    Dim paramSql11() As SqlParameter =
                {
                    New SqlParameter("@NoViremen", strNoViremen),
                    New SqlParameter("@LstPerutk", decLstPerutk),
                    New SqlParameter("@LastBlj", decLastBlj),
                    New SqlParameter("@CurPerutkAsal", decCurPerutkAsal),
                     New SqlParameter("@CurTmbh", decCurTmbh),
                    New SqlParameter("@CurKurang", decCurkurang),
                    New SqlParameter("@CurBlj", decCurBlj),
                    New SqlParameter("@CurBakiTng", decCurBakiTng),
                    New SqlParameter("@TngBaru", decTngBaru),
                    New SqlParameter("@CurWarSekatan", decCurWarSekatan)
                   }

                    If Not dbConn.fUpdateCommand(strSql, paramSql11) > 0 Then
                        blnSuccess = False
                        Exit Try
                    End If
                Else

                    'INSERT INTO BG11_ViremenTran
                    strSql = "INSERT INTO BG11_ViremenTran (BG10_NoViremen,BG11_LastPerutk,BG11_LastBlj,BG11_CurPerutkAsal,BG11_CurTmbh,BG11_CurKurang,BG11_CurBlj,BG11_CurBakiTng,BG11_TngBaru,BG11_CurWarSekatan)" &
                        "VALUES(@NoViremen,@LstPerutk,@LastBlj,@CurPerutkAsal,@CurTmbh,@CurKurang,@CurBlj,@CurBakiTng,@TngBaru,@CurWarSekatan)"

                    Dim paramSql10() As SqlParameter =
                    {
                        New SqlParameter("@NoViremen", strNoViremen),
                        New SqlParameter("@LstPerutk", decLstPerutk),
                        New SqlParameter("@LastBlj", decLastBlj),
                        New SqlParameter("@CurPerutkAsal", decCurPerutkAsal),
                         New SqlParameter("@CurTmbh", decCurTmbh),
                        New SqlParameter("@CurKurang", decCurkurang),
                        New SqlParameter("@CurBlj", decCurBlj),
                        New SqlParameter("@CurBakiTng", decCurBakiTng),
                        New SqlParameter("@TngBaru", decTngBaru),
                        New SqlParameter("@CurWarSekatan", decCurWarSekatan)
                       }

                    If Not dbConn.fInsertCommand(strSql, paramSql10) > 0 Then
                        blnSuccess = False
                        Exit Try
                    End If


                End If

                'UPDATE BG12_StatusDok
                strSql = "select count(*) from bg12_statusdok where bg07_noviremen = '" & strNoViremen & "' and kodstatusdok='" & strStatDok & "'"
                If fCheckRec(strSql) > 0 Then
                    strSql = "UPDATE BG12_StatusDok SET  BG12_TkhProses =@TkhProses, BG12_NoStaf = @NoStaf, BG12_Ulasan =@Ulasan where BG10_NoViremen =@NoViremen"

                    Dim paramSql12() As SqlParameter =
                    {
                        New SqlParameter("@NoViremen", strNoViremen),
                        New SqlParameter("@KodStatusDok", strStatDok),
                        New SqlParameter("@TkhProses", strTkhMohon),
                        New SqlParameter("@NoStaf", strNoStaf),
                        New SqlParameter("@Ulasan", "-")
                       }


                    If Not dbConn.fUpdateCommand(strSql, paramSql12) > 0 Then
                        blnSuccess = False
                        Exit Try
                    End If
                Else
                    strSql = "INSERT INTO BG12_StatusDok(BG10_NoViremen,KodStatusDok,BG12_TkhProses,BG12_NoStaf,BG12_Ulasan) " &
                                "VALUES (@NoViremen,@KodStatusDok,@TkhProses,@NoStaf,@Ulasan)"

                    Dim paramSql10() As SqlParameter =
            {
                New SqlParameter("@NoViremen", strNoViremen),
                New SqlParameter("@KodStatusDok", strStatDok),
                New SqlParameter("@TkhProses", strTkhMohon),
                New SqlParameter("@NoStaf", strNoStaf),
                New SqlParameter("@Ulasan", "-")
               }

                    If Not dbConn.fInsertCommand(strSql, paramSql10) > 0 Then
                        blnSuccess = False
                        Exit Try
                    End If
                End If
            Catch ex As Exception
                blnSuccess = False
            End Try

            If blnSuccess Then
                dbConn.sConnCommitTrans()
                Return True
            Else
                dbConn.sConnRollbackTrans()
                Return False
            End If

        Catch ex As Exception
            Return False
            Exit Function
        End Try

    End Function

    Private Sub ddlObjSbgK_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlObjSbgK.SelectedIndexChanged
        Try
            Dim strTkh As String = Now.ToString("yyyy-MM-dd")

            'Keluar
            Dim strTahun As String = Trim(txtTahunVire.Text.TrimEnd)
            Dim strKWK As String = Trim(ddlKWk.SelectedValue.TrimEnd)
            Dim strKOK As String = Trim(ddlKOk.SelectedValue.TrimEnd)
            Dim strPTjK As String = Trim(ddlPTJk.SelectedValue.TrimEnd)
            Dim strKPK As String = Trim(ddlKPk.SelectedValue.TrimEnd)
            Dim strObjSbgK As String = Trim(ddlObjSbgK.SelectedValue.TrimEnd)

            Dim decBakiSbnr As Decimal = fGetBakiSebenar(strTahun, strTkh, strKWK, strKOK, strPTjK, strKPK, strObjSbgK)

            txtBakiSmsK.Text = FormatNumber(decBakiSbnr)

            If ddlObjSbgK.SelectedIndex <> 0 AndAlso ddlObjSbgM.SelectedIndex <> 0 Then
                txtJumVire.Enabled = True
                txtJumVire.Text = "0.00"
            End If

            txtBakiSlpsVireM.Text = FormatNumber(CDec(txtBakiSmsM.Text))
            txtBakiSlpsVireK.Text = FormatNumber(CDec(txtBakiSmsK.Text))

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlObjSbgM_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlObjSbgM.SelectedIndexChanged

        sClearField()
        Dim dbconn As New DBKewConn
        Dim strSql As String
        Try
            Dim strTarikh As String = Now.ToString("yyyy-MM-dd")
            Dim intTahun As Integer = CInt(Trim(txtTahunVire.Text.TrimEnd))


            'Masuk
            Dim strKWM As String = Trim(ddlKWm.SelectedValue.TrimEnd)
            Dim strKOM As String = Trim(ddlKOm.SelectedValue.TrimEnd)
            Dim strPTjM As String = Trim(ddlPTJm.SelectedValue.TrimEnd)
            Dim strKPM As String = Trim(ddlKPm.SelectedValue.TrimEnd)
            Dim strObjSbgM As String = Trim(ddlObjSbgM.SelectedValue.TrimEnd)

            'Dim curdate As String
            Dim Thn As Integer
            Dim l_perutk As Decimal
            Dim l_perutktbh As Decimal
            Dim l_perutkrg As Decimal

            Dim l_vir_masuk As Decimal
            Dim l_vir_keluar As Decimal
            Dim l_belanja As Decimal
            Dim l_tng As Decimal
            Dim ld_lst_perutk As Decimal

            Dim ld_lst_blj As Decimal
            Dim ld_baki_tnh As Decimal
            Dim ld_lst_baki_tng As Decimal
            Dim ld_baki_tng As Decimal
            Dim ld_baki_perutk As Decimal
            Dim ld_remain As Decimal
            Dim ld_bajetbf As Decimal
            Dim refvot As String
            Dim lst_jrnl_debit As Decimal
            Dim lst_jrnl_kredit As Decimal
            Dim l_jrnl_debit As Decimal
            Dim l_jrnl_kredit As Decimal
            Dim lst_j_debit As Decimal
            Dim lst_j_kredit As Decimal
            Dim l_j_debit As Decimal
            Dim l_j_kredit As Decimal

            Dim strTahun As String = Now.Year
            Dim intTahunLps As Integer = CInt(strTahun) - 1
            Dim ls_last_date As String = "31/12/" + CStr(intTahunLps)

            txtBakiSmsM.Text = "0.00"
            txtBakiSlpsVireM.Text = "0.00"

            If ddlPTJk.SelectedValue = strPTjM AndAlso ddlKWk.SelectedValue = strKWM AndAlso ddlObjSbgK.SelectedValue = strObjSbgM Then
                fGlobalAlert("Objek sebagai yang sama tidak dibenarkan!", Me.Page, Me.[GetType]())
            Else
                strSql = "select isnull(sum(mk09_debit),0) as jum from mk09_bajetbf where kodkw ='" & strKWM & "' and KodKO = '" & strKOM & "' and kodptj = '" & strPTjM & "' and KodKP = '" & strKPM & "' and kodvot ='" & strObjSbgM & "' and mk09_tahun = '" & strTahun & "'"
                Dim ds As New DataSet
                ds = dbconn.fSelectCommand(strSql)

                If Not ds Is Nothing Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        ld_bajetbf = CDec(ds.Tables(0).Rows(0)("jum").ToString)
                    End If
                End If

                refvot = strObjSbgM.Substring(0, 2) 'Mid(Me.cmbObjSbgK.Text, 1, 2)

                strSql = "select isnull(sum(mk06_debit),0) as jumdebit from mk06_transaksi where year(mk06_tkhtran) = '" & intTahunLps & "' and kodkw = '" & strKWM & "' and KodKO = '" & strKOM & "' and kodptj ='" & strPTjM & "' and KodKP = '" & strKPM & "' and substring(kodvot,1,2) ='" & refvot & "' and substring(mk06_rujukan,1,2) in ('RT','JP','BK','RS','BP')"
                ds = Nothing
                ds = dbconn.fSelectCommand(strSql)

                If Not ds Is Nothing Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        lst_jrnl_debit = CDec(ds.Tables(0).Rows(0)("jumdebit").ToString)
                    End If
                End If

                strSql = "select isnull(sum(mk06_kredit),0) as jumkredit from mk06_transaksi where year(mk06_tkhtran) = '" & intTahunLps & "' and kodkw = '" & strKWM & "' and KodKO = '" & strKOM & "' and kodptj ='" & strPTjM & "' and KodKP = '" & strKPM & "' and substring(kodvot,1,2) ='" & refvot & "' and substring(mk06_rujukan,1,2) in ('RT','JP','BK','RS','BP')"
                ds = Nothing
                ds = dbconn.fSelectCommand(strSql)
                If Not ds Is Nothing Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        lst_jrnl_kredit = CDec(ds.Tables(0).Rows(0)("jumkredit").ToString)
                    End If
                End If

                strSql = "select isnull(sum(mk06_debit),0) as jumdebit from mk06_transaksi,gl01_jrnltran where mk06_rujukan=gl01_nojrnl and (gl01_jrnltran.gl01_statuslejar is null or gl01_jrnltran.gl01_statuslejar=0) and year(mk06_tkhtran) = '" & intTahunLps & "' and kodkw = '" & strKWM & "' and KodKO = '" & strKOM & "' and KodKP = '" & strKPM & "' and kodptj ='" & strPTjM & "' and substring(kodvot,1,2) ='" & refvot & "' and substring(mk06_rujukan,1,2) in ('JK')"
                ds = Nothing
                ds = dbconn.fSelectCommand(strSql)
                If Not ds Is Nothing Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        lst_j_debit = CDec(ds.Tables(0).Rows(0)("jumdebit").ToString)
                    End If
                End If

                strSql = "select isnull(sum(mk06_kredit),0) as jumkredit from mk06_transaksi,gl01_jrnltran where mk06_rujukan=gl01_nojrnl and (gl01_jrnltran.gl01_statuslejar is null or gl01_jrnltran.gl01_statuslejar=0) and year(mk06_tkhtran) = '" & intTahunLps & "' and kodkw = '" & strKWM & "' and KodKO = '" & strKOM & "' and kodptj ='" & strPTjM & "' and KodKP = '" & strKPM & "' and substring(kodvot,1,2) ='" & refvot & "' and substring(mk06_rujukan,1,2) in ('JK')"
                ds = Nothing
                ds = dbconn.fSelectCommand(strSql)
                If Not ds Is Nothing Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        lst_j_kredit = CDec(ds.Tables(0).Rows(0)("jumkredit").ToString)
                    End If
                End If

                ld_lst_perutk = CDec(fGetBakiSebenar(intTahunLps, strTarikh, strKWM, strKOM, strPTjM, strKPM, strObjSbgM))
                ld_lst_blj = (lst_jrnl_debit - lst_jrnl_kredit) + (lst_j_debit - lst_j_kredit)

                txtBoxA.Text = FormatNumber(ld_lst_perutk)
                txtBoxB.Text = FormatNumber(ld_lst_blj)


                strSql = "select isnull(mk01_bakisms,0) as mk01_bakisms, isnull(mk01_virmasuk,0) as mk01_virmasuk, isnull(mk01_virkeluar,0) as mk01_virkeluar, isnull(MK01_BljYtd,0) as MK01_BljYtd, isnull(mk01_tngytd,0) as mk01_tngytd from mk01_vottahun where mk01_tahun='" & strTahun & "'" _
    & " and kodkw ='" & strKWM & "' and KodKO = '" & strKOM & "' and kodptj ='" & strPTjM & "'" _
    & " and KodKP = '" & strKPM & "' and kodvot='" & strObjSbgM & "'"
                ds = Nothing
                ds = dbconn.fSelectCommand(strSql)
                If Not ds Is Nothing Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        l_vir_masuk = CDec(ds.Tables(0).Rows(0)("mk01_bakisms").ToString)
                        l_vir_keluar = CDec(ds.Tables(0).Rows(0)("mk01_virmasuk").ToString)
                        l_belanja = CDec(ds.Tables(0).Rows(0)("MK01_BljYtd").ToString)
                        l_tng = CDec(ds.Tables(0).Rows(0)("mk01_tngytd").ToString)
                    End If
                End If

                'perutk asal
                strSql = "select isnull(sum(bg07_amaun),0) as jum from BG07_AgihObjSbg where kodkw ='" & strKWM & "'" _
    & " and KodKO = '" & strKOM & "' and kodptj = '" & strPTjM & "' and KodKP = '" & strKPM & "' and kodvot ='" & strObjSbgM & "'" _
    & " and kodagih = 'AL' and bg07_tahun = '" & strTahun & "' and bg07_tkhagih <= CONVERT(DATETIME,'" & strTarikh & "', 102)"
                ds = Nothing
                ds = dbconn.fSelectCommand(strSql)
                If Not ds Is Nothing Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        l_perutk = CDec(ds.Tables(0).Rows(0)("jum").ToString)
                    End If
                End If
                txtBoxA1.Text = FormatNumber(l_perutk + ld_bajetbf)

                'perutk tbh
                strSql = "select isnull(sum(bg07_amaun),0) as jum from bg07_agihobjsbg where kodkw ='" & strKWM & "'" _
    & " and KodKO = '" & strKOM & "' and kodptj = '" & strPTjM & "' and KodKP = '" & strKPM & "' and kodvot ='" & strObjSbgM & "'" _
    & " and kodagih = 'TB' and bg07_tahun = '" & strTahun & "' and bg07_tkhagih <= CONVERT(DATETIME,'" & strTarikh & "', 102)"
                ds = Nothing
                ds = dbconn.fSelectCommand(strSql)
                If Not ds Is Nothing Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        l_perutktbh = CDec(ds.Tables(0).Rows(0)("jum").ToString)
                    End If
                End If
                txtBoxB1.Text = FormatNumber(l_perutktbh + l_vir_masuk)

                'kurang perutk
                strSql = "select isnull(sum(bg07_amaun),0) as jum from bg07_agihobjsbg where kodkw ='" & strKWM & "'" _
    & " and KodKO = '" & strKOM & "' and kodptj = '" & strPTjM & "' and KodKP = '" & strKPM & "' and kodvot ='" & strObjSbgM & "'" _
    & " and kodagih = 'KG' and bg07_tahun = '" & strTahun & "' and bg07_tkhagih <= CONVERT(DATETIME,'" & strTarikh & "', 102)"
                ds = Nothing
                ds = dbconn.fSelectCommand(strSql)
                If Not ds Is Nothing Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        l_perutkrg = CDec(ds.Tables(0).Rows(0)("jum").ToString)
                    End If
                End If

                strSql = "select isnull(sum(mk06_debit),0) as jumdebit from mk06_transaksi where mk06_tkhtran <= CONVERT(DATETIME,'" & strTarikh & "', 102) and year(mk06_tkhtran) = '" & Thn & "' 
and kodkw = '" & strKWM & "' and KodKO = '" & strKOM & "' and kodptj ='" & strPTjM & "' and KodKP = '" & strKPM & "' and substring(kodvot,1,2) ='" & refvot & "' and substring(mk06_rujukan,1,2) in ('RT','JP','BK','RS','BP')"
                ds = Nothing
                ds = dbconn.fSelectCommand(strSql)
                If Not ds Is Nothing Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        l_jrnl_debit = CDec(ds.Tables(0).Rows(0)("jumdebit").ToString)
                    End If
                End If

                strSql = "select isnull(sum(mk06_kredit),0) as jumkredit from mk06_transaksi where mk06_tkhtran <= CONVERT(DATETIME,'" & strTarikh & "', 102) and year(mk06_tkhtran) = '" & Thn & "' 
and kodkw = '" & strKWM & "' and KodKO = '" & strKOM & "' and kodptj ='" & strPTjM & "' and KodKP = '" & strKPM & "' and substring(kodvot,1,2) ='" & refvot & "' and substring(mk06_rujukan,1,2) in ('RT','JP','BK','RS','BP')"
                ds = Nothing
                ds = dbconn.fSelectCommand(strSql)
                If Not ds Is Nothing Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        l_jrnl_kredit = CDec(ds.Tables(0).Rows(0)("jumkredit").ToString)
                    End If
                End If

                strSql = "select isnull(sum(mk06_debit),0) as jumdebit from mk06_transaksi,gl01_jrnltran where mk06_rujukan=gl01_nojrnl and (gl01_jrnltran.gl01_statuslejar is null or gl01_jrnltran.gl01_statuslejar=0) and mk06_tkhtran <= CONVERT(DATETIME,'" & strTarikh & "', 102) and year(mk06_tkhtran) = '" & Thn & "' 
and kodkw = '" & strKWM & "' and KodKO = '" & strKOM & "' and kodptj ='" & strPTjM & "' and KodKP = '" & strKPM & "' and substring(kodvot,1,2) ='" & refvot & "' and substring(mk06_rujukan,1,2) in ('JK')"
                ds = Nothing
                ds = dbconn.fSelectCommand(strSql)
                If Not ds Is Nothing Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        l_jrnl_debit = CDec(ds.Tables(0).Rows(0)("jumdebit").ToString)
                    End If
                End If

                strSql = "select isnull(sum(mk06_kredit),0) as jumkredit from mk06_transaksi,gl01_jrnltran where mk06_rujukan=gl01_nojrnl and (gl01_jrnltran.gl01_statuslejar is null or gl01_jrnltran.gl01_statuslejar=0) and mk06_tkhtran <= CONVERT(DATETIME,'" & strTarikh & "', 102) and year(mk06_tkhtran) = '" & Thn & "' 
and kodkw = '" & strKWM & "' and KodKO = '" & strKOM & "' and kodptj ='" & strPTjM & "' and KodKP = '" & strKPM & "' and substring(kodvot,1,2) ='" & refvot & "' and substring(mk06_rujukan,1,2) in ('JK')"
                ds = Nothing
                ds = dbconn.fSelectCommand(strSql)
                If Not ds Is Nothing Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        l_j_kredit = CDec(ds.Tables(0).Rows(0)("jumkredit").ToString)
                    End If
                End If

                'get the current year belanja
                l_belanja = (l_jrnl_debit - l_jrnl_kredit) + (l_j_debit - l_j_kredit)

                'get the tanggungan
                ld_baki_tng = fgetBakiTng(strTahun, strTarikh, strKWM, strKOM, strPTjM, strKPM, strObjSbgM)

                'get the last year tanggungan
                ld_lst_baki_tng = fgetLstTng(strTahun, strTarikh, strKWM, strKOM, strPTjM, strKPM, strObjSbgM)

                txtBoxC.Text = FormatNumber(l_vir_keluar + l_perutkrg)
                txtBoxD.Text = FormatNumber(l_perutk + ld_bajetbf + l_perutktbh + l_vir_masuk - l_vir_keluar - l_perutkrg, 2)
                txtBoxE.Text = FormatNumber(l_belanja)
                txtBoxF.Text = FormatNumber(ld_baki_tng + ld_lst_baki_tng)

                ld_baki_perutk = CDec(fGetBakiSebenar(strTahun, strTarikh, strKWM, strKOM, strPTjM, strKPM, strObjSbgM))
                Me.txtBakiSmsM.Text = FormatNumber(ld_baki_perutk)


            End If

            If ddlObjSbgK.SelectedIndex <> 0 AndAlso ddlObjSbgM.SelectedIndex <> 0 Then
                txtJumVire.Enabled = True
                txtJumVire.Text = "0.00"
            End If

            txtBakiSlpsVireK.Text = FormatNumber(CDec(txtBakiSmsK.Text))

        Catch ex As Exception

        End Try
    End Sub

    Private Sub sClearField()
        Try
            txtBoxA.Text = "0.00"
            txtBoxB.Text = "0.00"

            txtBoxA1.Text = "0.00"
            txtBoxB1.Text = "0.00"
            txtBoxC.Text = "0.00"
            txtBoxD.Text = "0.00"
            txtBoxE.Text = "0.00"
            txtBoxF.Text = "0.00"
            txtBoxG.Text = "0.00"
            txtBoxH.Text = "0.00"
            txtBoxI.Text = "0.00"

        Catch ex As Exception

        End Try
    End Sub
    Private Sub txtJumVire_TextChanged(sender As Object, e As EventArgs) Handles txtJumVire.TextChanged

        Try
            If txtJumVire.Text = "" Then txtJumVire.Text = 0
            Dim decJumV As Decimal = CDec(txtJumVire.Text)

            'Baki selesas vire keluar
            Dim decBakiSmsK As Decimal = CDec(txtBakiSmsK.Text)
            Dim decBakiSlpsVK As Decimal = decBakiSmsK - decJumV
            txtBakiSlpsVireK.Text = FormatNumber(decBakiSlpsVK)

            'Baki selesas vire masuk
            Dim decBakiSmsM As Decimal = CDec(txtBakiSmsM.Text)
            Dim decBakiSlmsVm = decBakiSmsM + decJumV
            txtBakiSlpsVireM.Text = FormatNumber(decBakiSlmsVm)

            'D = A + B + C
            Dim decA As Decimal = CDec(txtBoxA1.Text)
            Dim decB As Decimal = CDec(txtBoxB1.Text)
            Dim decC As Decimal = CDec(txtBoxC.Text)

            Dim decD As Decimal = decA + decB + decD
            txtBoxD.Text = FormatNumber(decD)

            'G = jumv
            txtBoxG.Text = FormatNumber(decJumV)

            'H = E + F + G
            Dim decE As Decimal = CDec(txtBoxE.Text)
            Dim decF As Decimal = CDec(txtBoxF.Text)
            Dim decG As Decimal = CDec(txtBoxG.Text)
            Dim decH As Decimal = decE + decF + decG
            txtBoxH.Text = FormatNumber(decH)

            txtBakiSlpsVireK.Text = FormatNumber(CDec(txtBakiSmsK.Text) - decJumV)
            txtBakiSlpsVireM.Text = FormatNumber(CDec(txtBakiSmsM.Text) + decJumV)

            txtJumVire.Text = FormatNumber(decJumV)

        Catch ex As Exception

        End Try


    End Sub

    Private Sub lbtnList_Click(sender As Object, e As EventArgs) Handles lbtnList.Click
        ddlStatusFil.SelectedValue = 12
        fBindGViremen()
        mpeLst.Show()
    End Sub

    Protected Sub lnkBtnCari_Click(sender As Object, e As EventArgs) Handles lnkBtnCari.Click
        fBindGViremen()
        mpeLst.Show()
    End Sub

    Private Sub gvViremen_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvViremen.PageIndexChanging
        Try
            gvViremen.PageIndex = e.NewPageIndex
            If ViewState("dtList") IsNot Nothing Then
                gvViremen.DataSource = ViewState("dtList")
                gvViremen.DataBind()
            End If
            mpeLst.Show()
        Catch ex As Exception

        End Try
    End Sub


End Class

