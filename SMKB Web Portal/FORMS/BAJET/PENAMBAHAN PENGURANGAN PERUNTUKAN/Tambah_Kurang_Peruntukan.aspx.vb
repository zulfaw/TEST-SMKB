Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.IO

Public Class Penambahan

    Inherits System.Web.UI.Page
    'Public Shared dv As New DataView
    Public KodStatus As String = String.Empty
    Dim sLogBaru As String
    Dim sLogMedan As String
    Dim strNoStaf As String = UserInfo.strSesStaffID
    Dim sLogLevel As String = UserInfo.strUsrTahap
    Dim lsLogUsrIP As String = UserInfo.strUsrPcIP
    Dim lsLogUsrPC As String = UserInfo.strUsrPCName

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                ViewState("sesKodSubMenu") = Request.QueryString("KodSubMenu")
                txtTahun.Text = Now.Year
                fBindDddlKW()
                ddlKO.Items.Add(New ListItem("- SILA PILIH KUMPULAN WANG -", 0))
                ddlPTJ.Items.Add(New ListItem("- SILA PILIH KOD OPERASI -", 0))
                ddlKP.Items.Add(New ListItem("- SILA PILIH PTJ -", 0))
                ddlObjAm.Items.Add(New ListItem("- SILA PILIH KOD PROJEK -", 0))
                ddlObjSbg.Items.Add(New ListItem("- SILA PILIH OBJEK AM -", 0))

                fBindGvSenarai()
                lbtnHapus.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub fBindDddlKW()

        Try
            Dim strSql As String = "select KodKw, (KodKW + ' - ' + Butiran) as Butiran from MK_Kw where Status = 1 order by KodKw"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)

            ddlKW.DataSource = ds
            ddlKW.DataTextField = "Butiran"
            ddlKW.DataValueField = "KodKw"
            ddlKW.DataBind()

            ddlKW.Items.Insert(0, "- SILA PILIH -")
            ddlKW.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindGvSenarai()
        Try
            sClearGvSenarai()

            Dim intRec As Integer = 0
            Dim strSql As String

            'Dim dt As New DataTable
            'dt.Columns.Add("kodKw", GetType(String))
            'dt.Columns.Add("kodKo", GetType(String))
            'dt.Columns.Add("PTj", GetType(String))
            'dt.Columns.Add("KodKp", GetType(String))
            'dt.Columns.Add("ObjSbg", GetType(String))
            'dt.Columns.Add("Amaun", GetType(String))
            'dt.Columns.Add("Jenis", GetType(String))
            'dt.Columns.Add("TkhMohon", GetType(String))
            'dt.Columns.Add("Butiran", GetType(String))
            'dt.Columns.Add("IndKW", GetType(String))
            'dt.Columns.Add("IndPTj", GetType(String))
            'dt.Columns.Add("IndObjAm", GetType(String))
            'dt.Columns.Add("IndObjSbg", GetType(String))

            'strSql = "SELECT dbo.bg04_AgihKw.bg04_IndKw as IndKW, dbo.bg05_AgihPTJ.bg05_IndPTJ as IndPTj, dbo.bg06_AgihObjAm.bg06_IndObjAm as IndObjAm,  dbo.bg07_AgihObjSbg.bg07_IndObjSbg as IndObjSbg, dbo.bg04_AgihKw.bg04_Tahun, " &
            '"dbo.bg04_AgihKw.KodKw,dbo.bg05_AgihPTJ.KodKo , (Select dbo.MK_PTJ.KodPTJ + ' - ' + dbo.MK_PTJ.Butiran from dbo.MK_PTJ where dbo.MK_PTJ.KodPTJ  = dbo.bg05_AgihPTJ.KodPTJ) as PTj, dbo.bg06_AgihObjAm.KodKP ,dbo.bg06_AgihObjAm.KodVot AS objam, " &
            '"(Select dbo.MK_Vot.KodVot + ' - ' + dbo.MK_Vot.Butiran  from dbo.MK_Vot where dbo.MK_Vot .KodVot = dbo.bg07_AgihObjSbg.KodVot) AS objsbg, dbo.bg04_AgihKw.bg04_Butiran, dbo.bg04_AgihKw.bg04_Amaun,dbo.bg04_AgihKw.KodAgih , " &
            '"dbo.bg04_AgihKw.bg04_TkhAgih, dbo.bg07_AgihObjSbg.bg07_StatSah, dbo.bg07_AgihObjSbg.bg07_StatLulus, dbo.bg07_AgihObjSbg.bg07_Status, dbo.bg04_AgihKw.KodAgih FROM dbo.bg04_AgihKw " &
            '"INNER JOIN dbo.bg05_AgihPTJ ON dbo.bg04_AgihKw.bg04_IndKw = dbo.bg05_AgihPTJ.bg04_IndKw INNER JOIN dbo.bg06_AgihObjAm ON dbo.bg05_AgihPTJ.bg05_IndPTJ = dbo.bg06_AgihObjAm.bg05_IndPTJ " &
            '"INNER JOIN dbo.bg07_AgihObjSbg ON dbo.bg06_AgihObjAm.bg06_IndObjAm = dbo.bg07_AgihObjSbg.bg06_IndObjAm where dbo.bg07_AgihObjSbg.bg07_StatSah Is NULL And dbo.bg07_AgihObjSbg.bg07_StatLulus Is NULL And " &
            '"dbo.bg07_AgihObjSbg.bg07_Status = '1' And dbo.bg07_AgihObjSbg.KodAgih in ('TB','KG') and dbo.bg04_AgihKw.bg04_Tahun ='" & Trim(txtTahun.Text.TrimEnd) & "' order by dbo.bg04_AgihKw.BG04_IndKw , dbo.bg04_AgihKw.KodKw, dbo.bg05_AgihPTJ.KodPTJ,dbo.bg07_Agihobjsbg.Kodvot, " &
            '"dbo.bg07_Agihobjsbg.bg07_tkhagih"


            strSql = "SELECT dbo.bg04_AgihKw.bg04_IndKw, 
dbo.bg05_AgihPTJ.bg05_IndPTJ, 
dbo.bg06_AgihObjAm.bg06_IndObjAm,  
dbo.bg07_AgihObjSbg.bg07_IndObjSbg, 
dbo.bg04_AgihKw.bg04_Tahun, 
dbo.bg04_AgihKw.KodKw,
dbo.bg05_AgihPTJ.KodKo, 
(Select dbo.MK_PTJ.KodPTJ + ' - ' + dbo.MK_PTJ.Butiran from dbo.MK_PTJ where dbo.MK_PTJ.KodPTJ  = dbo.bg05_AgihPTJ.KodPTJ) as PTj, 
dbo.bg06_AgihObjAm.KodKP,
dbo.bg06_AgihObjAm.KodVot, 
(Select dbo.MK_Vot.KodVot + ' - ' + dbo.MK_Vot.Butiran from dbo.MK_Vot where dbo.MK_Vot.KodVot = dbo.bg07_AgihObjSbg.KodVot) as VotSebagai, 
dbo.bg04_AgihKw.bg04_Butiran, 
dbo.bg04_AgihKw.bg04_Amaun,
dbo.bg04_AgihKw.KodAgih, 
dbo.bg04_AgihKw.bg04_TkhAgih, 
dbo.bg07_AgihObjSbg.bg07_StatSah, 
dbo.bg07_AgihObjSbg.bg07_StatLulus, 
dbo.bg07_AgihObjSbg.bg07_Status, 
dbo.bg04_AgihKw.KodAgih 
FROM dbo.bg04_AgihKw 
INNER JOIN dbo.bg05_AgihPTJ ON dbo.bg04_AgihKw.bg04_IndKw = dbo.bg05_AgihPTJ.bg04_IndKw 
INNER JOIN dbo.bg06_AgihObjAm ON dbo.bg05_AgihPTJ.bg05_IndPTJ = dbo.bg06_AgihObjAm.bg05_IndPTJ 
INNER JOIN dbo.bg07_AgihObjSbg ON dbo.bg06_AgihObjAm.bg06_IndObjAm = dbo.bg07_AgihObjSbg.bg06_IndObjAm 
where dbo.bg07_AgihObjSbg.bg07_StatSah Is NULL And dbo.bg07_AgihObjSbg.bg07_StatLulus Is NULL And dbo.bg07_AgihObjSbg.bg07_Status = '1' And dbo.bg07_AgihObjSbg.KodAgih in ('TB','KG') and dbo.bg04_AgihKw.bg04_Tahun ='" & Trim(txtTahun.Text.TrimEnd) & "' 
order by dbo.bg04_AgihKw.BG04_IndKw , dbo.bg04_AgihKw.KodKw, dbo.bg05_AgihPTJ.KodPTJ,dbo.bg07_Agihobjsbg.Kodvot, dbo.bg07_Agihobjsbg.bg07_tkhagih"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            'Dim strKW, strKO, strPTj, strKP, strObjSbg, strAmaun, strJenis, strTarikh, strButiran, strIndKW, strIndPTj, strIndObjAm, strIndObjSbg As String
            'Dim decAmaun As Decimal
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    gvSenarai.DataSource = ds.Tables(0)
                    gvSenarai.DataBind()

                    intRec = ds.Tables(0).Rows.Count
                End If
                'For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                '    strKW = ds.Tables(0).Rows(i)("KodKw")
                '    strKO = ds.Tables(0).Rows(i)("KodKo")
                '    strPTj = ds.Tables(0).Rows(i)("PTj")
                '    strKP = ds.Tables(0).Rows(i)("KodKP")
                '    strObjSbg = ds.Tables(0).Rows(i)("objsbg")
                '    decAmaun = CDec(ds.Tables(0).Rows(i)("bg04_Amaun"))
                '    strAmaun = decAmaun.ToString("#,##0.00")
                '    strJenis = ds.Tables(0).Rows(i)("KodAgih")
                '    strTarikh = ds.Tables(0).Rows(i)("bg04_TkhAgih")
                '    strButiran = ds.Tables(0).Rows(i)("bg04_Butiran")
                '    strIndKW = ds.Tables(0).Rows(i)("IndKW")
                '    strIndPTj = ds.Tables(0).Rows(i)("IndPTj")
                '    strIndObjAm = ds.Tables(0).Rows(i)("IndObjAm")
                '    strIndObjSbg = ds.Tables(0).Rows(i)("IndObjSbg")

                '    dt.Rows.Add(strKW, strKO, strPTj, strKP, strObjSbg, strAmaun, strJenis, strTarikh, strButiran, strIndKW, strIndPTj, strIndObjAm, strIndObjSbg)
                'Next
            End If

            lblJumRekod.InnerText = intRec
        Catch ex As Exception

        End Try
    End Sub

    Private Sub sClearGvSenarai()
        Try
            gvSenarai.DataSource = New List(Of String)
            gvSenarai.DataBind()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub fBindDdlKO()
        Try
            Dim strSql As String = "SELECT DISTINCT TOP 100 PERCENT MK_KodOperasi.KodKO ,(MK_KodOperasi.KodKO  + ' - ' + MK_KodOperasi.Butiran) as Butiran FROM MK_KodOperasi  INNER JOIN MK01_VotTahun ON MK_KodOperasi.KodKO  = MK01_VotTahun.KodKO " &
                                        "WHERE MK01_VotTahun.KodKw = '" & Trim(ddlKW.SelectedValue.TrimEnd) & "' AND MK01_VotTahun.MK01_Tahun  = '" & Trim(txtTahun.Text.TrimEnd) & "'"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable

            ds = dbconn.fSelectCommand(strSql)
            ddlKO.DataSource = ds
            ddlKO.DataTextField = "Butiran"
            ddlKO.DataValueField = "KodKO"
            ddlKO.DataBind()

            ddlKO.Items.Insert(0, "- SILA PILIH -")
            ddlKO.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub
    Private Sub fBindDdlPTJ()
        Try
            Dim strSql As String = "SELECT DISTINCT TOP 100 PERCENT MK_PTJ.KodPTJ,(MK_PTJ.KodPTJ + ' - ' + MK_PTJ.Butiran) as Butiran FROM MK_PTJ INNER JOIN MK01_VotTahun ON MK_PTJ.KodPTJ = MK01_VotTahun.KodPTJ " &
            "WHERE MK01_VotTahun.KodKw = '" & Trim(ddlKW.SelectedValue.TrimEnd) & "' AND  MK01_VotTahun .KodKO = '" & Trim(ddlKO.SelectedValue.TrimEnd) & "' AND MK01_VotTahun.MK01_Tahun  = '" & Trim(txtTahun.Text.TrimEnd) & "'"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)
            ddlPTJ.DataSource = ds
            ddlPTJ.DataTextField = "Butiran"
            ddlPTJ.DataValueField = "KodPTJ"
            ddlPTJ.DataBind()

            ddlPTJ.Items.Insert(0, "- SILA PILIH -")
            ddlPTJ.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindDdlKP()
        Try
            Dim strSql As String = "SELECT DISTINCT TOP 100 PERCENT MK_KodProjek.KodProjek  ,(MK_KodProjek.KodProjek  + ' - ' + MK_KodProjek.Butiran) as Butiran FROM MK_KodProjek  INNER JOIN MK01_VotTahun ON MK_KodProjek.KodProjek  = MK01_VotTahun.KodKP  " &
                                    "WHERE MK01_VotTahun.MK01_Tahun  = '" & Trim(txtTahun.Text.TrimEnd) & "' and  MK01_VotTahun.KodKw = '" & Trim(ddlKW.SelectedValue.TrimEnd) & "' AND  MK01_VotTahun .KodKO = '" & Trim(ddlKO.SelectedValue.TrimEnd) & "' AND  MK01_VotTahun .KodPTJ = '" & Trim(ddlPTJ.SelectedValue.TrimEnd) & "'"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)

            ddlKP.DataSource = ds
            ddlKP.DataTextField = "Butiran"
            ddlKP.DataValueField = "KodProjek"
            ddlKP.DataBind()

            ddlKP.Items.Insert(0, "- SILA PILIH -")
            ddlKP.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindDdlObjAm()
        Try
            Dim strSql As String = "SELECT DISTINCT KODVOT,(KODVOT + ' - ' + BUTIRAN) as BUTIRAN FROM vbg_objam where mk01_tahun='" & Trim(txtTahun.Text.TrimEnd) & "' and KodKw='" & Trim(ddlKW.SelectedValue.TrimEnd) & "' and kodptj= '" & Trim(ddlPTJ.SelectedValue.TrimEnd) & "' and kodkp = '" & Trim(ddlKP.SelectedValue.TrimEnd) & "' ORDER BY Kodvot"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)

            ddlObjAm.DataSource = ds
            ddlObjAm.DataTextField = "BUTIRAN"
            ddlObjAm.DataValueField = "KODVOT"
            ddlObjAm.DataBind()

            ddlObjAm.Items.Insert(0, "- SILA PILIH -")
            ddlObjAm.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlObjekAm_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlObjAm.SelectedIndexChanged
        Dim strObjAm As String
        strObjAm = ddlObjAm.SelectedValue.ToString

        fBindDdlObjSbg(strObjAm.Substring(0, 1))

        txtBaki.Text = "0.00"
        sClearTxtAmaun()
    End Sub
    Private Sub fBindDdlObjSbg(ByVal strObjAm As String)
        Try
            Dim strSql As String = "SELECT DISTINCT KODVOT,(KODVOT + ' - ' + BUTIRAN) as Butiran FROM vbg_objsbg where mk01_tahun = '" & Trim(txtTahun.Text.TrimEnd) & "' and kodkw = '" & Trim(ddlKW.SelectedValue.TrimEnd) & "' and kodptj='" & Trim(ddlPTJ.SelectedValue.TrimEnd) & "' and kodkp = '" & Trim(ddlKP.SelectedValue.TrimEnd) & "' and LEFT(kodvot,1) = '" & Trim(strObjAm.Substring(0, 1).TrimEnd) & "' ORDER BY kodvot"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)

            ddlObjSbg.DataSource = ds
            ddlObjSbg.DataTextField = "Butiran"
            ddlObjSbg.DataValueField = "KodVot"
            ddlObjSbg.DataBind()

            ddlObjSbg.Items.Insert(0, "- SILA PILIH -")
            ddlObjSbg.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub




    Private Sub fReset()
        Try
            rbMenu.SelectedValue = 0
            rbMenu.Enabled = True
            ddlKW.SelectedIndex = 0

            ddlKO.Items.Clear()
            ddlKO.Items.Add(New ListItem("- SILA PILIH KUMPULAN WANG -", 0))

            ddlPTJ.Items.Clear()
            ddlPTJ.Items.Add(New ListItem("- SILA PILIH KOD OPERASI -", 0))

            ddlKP.Items.Clear()
            ddlKP.Items.Add(New ListItem("- SILA PILIH PTJ -", 0))

            ddlObjAm.Items.Clear()
            ddlObjAm.Items.Add(New ListItem("- SILA PILIH KOD PROJEK -", 0))

            ddlObjSbg.Items.Clear()
            ddlObjSbg.Items.Add(New ListItem("- SILA PILIH OBJEK AM -", 0))

            txtBaki.Text = ""
            txtButiran.Text = ""
            txtAmaun.Text = ""
            lbtnHapus.Visible = False
            lbtnSimpan.Visible = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlKO_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKO.SelectedIndexChanged
        fBindDdlPTJ()

        ddlKP.Items.Clear()
        ddlKP.Items.Add(New ListItem("- SILA PILIH PTJ -", 0))

        ddlObjAm.Items.Clear()
        ddlObjAm.Items.Add(New ListItem("- SILA PILIH KOD PROJEK -", 0))

        ddlObjSbg.Items.Clear()
        ddlObjSbg.Items.Add(New ListItem("- SILA PILIH OBJEK AM -", 0))

        txtBaki.Text = "0.00"
        sClearTxtAmaun()
    End Sub

    Private Sub ddlKW_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKW.SelectedIndexChanged
        fBindDdlKO()

        ddlPTJ.Items.Clear()
        ddlPTJ.Items.Add(New ListItem("- SILA PILIH KOD OPERASI -", 0))

        ddlKP.Items.Clear()
        ddlKP.Items.Add(New ListItem("- SILA PILIH PTJ -", 0))

        ddlObjAm.Items.Clear()
        ddlObjAm.Items.Add(New ListItem("- SILA PILIH KOD PROJEK -", 0))

        ddlObjSbg.Items.Clear()
        ddlObjSbg.Items.Add(New ListItem("- SILA PILIH OBJEK AM -", 0))

        txtBaki.Text = "0.00"
        sClearTxtAmaun()
    End Sub

    Private Sub ddlPTJ_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPTJ.SelectedIndexChanged
        fBindDdlKP()

        ddlObjAm.Items.Clear()
        ddlObjAm.Items.Add(New ListItem("- SILA PILIH KOD PROJEK -", 0))

        ddlObjSbg.Items.Clear()
        ddlObjSbg.Items.Add(New ListItem("- SILA PILIH OBJEK AM -", 0))

        txtBaki.Text = "0.00"
        sClearTxtAmaun()
    End Sub

    Private Sub ddlObjSbg_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlObjSbg.SelectedIndexChanged
        Try
            Dim strBaki As String
            Dim intTahun As Integer = Trim(txtTahun.Text.TrimEnd)
            Dim strTarikh As String = Now.ToString("yyyy-MM-dd")
            Dim strKodKw As String = Trim(ddlKW.SelectedValue.TrimEnd)
            Dim strKodKo As String = Trim(ddlKO.SelectedValue.TrimEnd)
            Dim strKodPTj As String = Trim(ddlPTJ.SelectedValue.TrimEnd)
            Dim strKodKp As String = "0000000"  'Trim(ddlk.SelectedValue.TrimEnd)
            Dim strKodVot As String = Trim(ddlObjSbg.SelectedValue.TrimEnd)

            Dim decBaki As Decimal = fGetBakiSebenar(intTahun, strTarikh, strKodKw, strKodKo, strKodPTj, strKodKp, strKodVot)
            strBaki = FormatNumber(decBaki, 2) 'decBaki.ToString("#,##0.00")

            txtBaki.Text = strBaki
            sClearTxtAmaun()
        Catch ex As Exception

        End Try



    End Sub

    Private Sub ddlKP_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKP.SelectedIndexChanged
        fBindDdlObjAm()

        ddlObjSbg.Items.Clear()
        ddlObjSbg.Items.Add(New ListItem("- SILA PILIH OBJEK AM -", 0))

        txtBaki.Text = "0.00"
        sClearTxtAmaun()
    End Sub

    Private Sub gvSenarai_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvSenarai.RowCommand
        Try
            If e.CommandName = "Select" Then
                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvSenarai.Rows(index)

                Dim strKodKW As String = TryCast(selectedRow.FindControl("lblKw"), Label).Text.ToString
                Dim strKodKo As String = TryCast(selectedRow.FindControl("lblKo"), Label).Text.ToString
                Dim strPTj As String = TryCast(selectedRow.FindControl("lblPTj"), Label).Text.ToString
                Dim strKodPTj As String = strPTj.Substring(0, 6)
                Dim strKodKp As String = TryCast(selectedRow.FindControl("lblKp"), Label).Text.ToString
                Dim strObjSbg As String = TryCast(selectedRow.FindControl("lblObjSbg"), Label).Text.ToString
                Dim strKodSbg As String = strObjSbg.Substring(0, 5)
                Dim strObjAm As String = strObjSbg.Substring(0, 1) & "0000"
                Dim strAmaun As String = TryCast(selectedRow.FindControl("lblAmaun"), Label).Text.ToString
                Dim strJenis As String = TryCast(selectedRow.FindControl("lblJenis"), Label).Text.ToString
                Dim strTkhMohon As String = TryCast(selectedRow.FindControl("lblTkhMohon"), Label).Text.ToString
                Dim strButiranP As String = TryCast(selectedRow.FindControl("lblButiran"), Label).Text.ToString
                Dim strTkhToday As String = Now.ToString("yyyy-MM-dd")

                If strJenis = "TB" Then
                    rbMenu.SelectedValue = 0
                ElseIf strJenis = "KG" Then
                    rbMenu.SelectedValue = 1
                End If
                rbMenu.Enabled = False

                ddlKW.SelectedValue = strKodKW

                fBindDdlKO()
                ddlKO.SelectedValue = strKodKo

                fBindDdlPTJ()
                ddlPTJ.SelectedValue = strKodPTj

                fBindDdlKP()
                ddlKP.SelectedValue = strKodKp

                fBindDdlObjAm()
                ddlObjAm.SelectedValue = strObjAm

                fBindDdlObjSbg(strObjAm)

                ddlObjSbg.SelectedValue = strKodSbg

                Dim decBaki As Decimal = fGetBakiSebenar(Trim(txtTahun.Text.TrimEnd), strTkhToday, strKodKW, strKodKo, strKodPTj, strKodKp, strKodSbg)
                Dim strBaki As String = decBaki.ToString("#,##0.00")
                txtBaki.Text = strBaki
                txtButiran.Text = Trim(strButiranP.TrimEnd)
                txtAmaun.Text = FormatNumber(strAmaun)

                Dim strIndKW As String = TryCast(selectedRow.FindControl("lblIndKW"), Label).Text.ToString
                Dim strIndPTj As String = TryCast(selectedRow.FindControl("lblIndPTj"), Label).Text.ToString
                Dim strIndObjAm As String = TryCast(selectedRow.FindControl("lblIndObjAm"), Label).Text.ToString
                Dim strIndObjSbg As String = TryCast(selectedRow.FindControl("lblIndObjSbg"), Label).Text.ToString

                sLoadLamp(strIndKW, strIndPTj, strIndObjAm, strIndObjSbg)

                hidIndKW.Value = strIndKW
                hidIndPTj.Value = strIndPTj
                hidIndObjAm.Value = strIndObjAm
                hidIndObjSbg.Value = strIndObjSbg

                lbtnHapus.Visible = True
                lbtnSimpan.Visible = False

                divList.Visible = False
                divDetail.Visible = True



            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub sLoadLamp(strIndKW, strIndPTj, strIndObjAm, strIndObjSbg)

        sClearGvLamp()

        Try
            Dim strSql = "select BG14_Id, BG04_IndKw, BG14_Path, bg14_NamaDok, BG14_ContentType, BG14_Status from BG14_Lampiran where BG14_Status = 1 and BG04_IndKw = '" & strIndKW & "' and BG05_IndPTJ = '" & strIndPTj & "' and BG06_IndObjAm = '" & strIndObjAm & "' and BG07_IndObjSbg = '" & strIndObjSbg & "' order by BG14_Id"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)


            Dim dt As New DataTable
            dt = fSetDtLamp()

            Dim GuidId As Guid
            Dim intID As Integer
            Dim strPath As String
            Dim strNamaDok, IndKw As String
            Dim strContType As String
            Dim strStat As String

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                GuidId = Guid.NewGuid()
                intID = ds.Tables(0).Rows(i)("BG14_Id").ToString
                IndKw = ds.Tables(0).Rows(i)("BG04_IndKw").ToString
                strPath = ds.Tables(0).Rows(i)("BG14_Path").ToString
                strNamaDok = ds.Tables(0).Rows(i)("BG14_NamaDok").ToString
                strContType = ds.Tables(0).Rows(i)("BG14_ContentType").ToString
                strStat = ds.Tables(0).Rows(i)("BG14_Status").ToString

                dt.Rows.Add(GuidId, intID, IndKw, strPath, strNamaDok, strContType, strStat)
            Next

            ViewState("dtLampiran") = dt
            gvLamp.DataSource = dt
            gvLamp.DataBind()

        Catch ex As Exception

        End Try



    End Sub

    Private Function fSetDtLamp() As DataTable

        Try
            Dim dt As DataTable = New DataTable()
            Dim dr As DataRow = Nothing
            dt.Columns.Add(New DataColumn("GUID", GetType(Guid)))
            dt.Columns.Add(New DataColumn("BG14_Id", GetType(Integer)))
            dt.Columns.Add(New DataColumn("BG04_IndKw", GetType(String)))
            dt.Columns.Add(New DataColumn("BG14_Path", GetType(String)))
            dt.Columns.Add(New DataColumn("BG14_NamaDok", GetType(String)))
            dt.Columns.Add(New DataColumn("BG14_ContentType", GetType(String)))
            dt.Columns.Add(New DataColumn("BG14_Status", GetType(String)))

            Return dt

        Catch ex As Exception

        End Try

    End Function

    Private Sub sClearGvLamp()
        gvLamp.DataSource = New List(Of String)
        gvLamp.DataBind()
    End Sub

    Private Function fFindKO(ByVal strKodKo As String, ByRef strButiran As String)
        Try
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn

            Dim strSql As String = "select KodKO , Butiran  from MK_KodOperasi where KodKO = '" & strKodKo & "'"
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strButiran = ds.Tables(0).Rows(0)("Butiran").ToString
                End If
            End If
        Catch ex As Exception

        End Try
    End Function

    Private Function fFindKP(ByVal strKodKp As String, ByRef strButiran As String)
        Try
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn

            Dim strSql As String = "select KodProjek , Butiran  from MK_KodProjek where KodProjek = '" & strKodKp & "'"
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strButiran = ds.Tables(0).Rows(0)("Butiran").ToString
                End If
            End If
        Catch ex As Exception

        End Try
    End Function

    Private Function fFindVot(ByVal strKodVot As String, ByRef strButiran As String)
        Try
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn

            Dim strSql As String = "select kodvot, Butiran  from mk_vot where KodVot = '" & strKodVot & "'"
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strButiran = ds.Tables(0).Rows(0)("Butiran").ToString
                End If
            End If
        Catch ex As Exception

        End Try
    End Function

    'Private Function fFindDasar(ByVal strKodDasar As String, ByRef strButiran As String)
    '    Try
    '        Dim ds As New DataSet
    '        Dim dbconn As New DBKewConn

    '        Dim strSql As String = "select BG_KodDasar , Butiran from BG_Dasar where BG_KodDasar  = '" & strKodDasar & "'"
    '        ds = dbconn.fselectCommand(strSql)

    '        If Not ds Is Nothing Then
    '            If ds.Tables(0).Rows.Count > 0 Then
    '                strButiran = ds.Tables(0).Rows(0)("Butiran").ToString
    '            End If
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Function
    Private Sub gvSenarai_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvSenarai.RowDataBound
        Try
            Dim strJenis As String
            If e.Row.RowType = DataControlRowType.DataRow Then
                strJenis = DataBinder.Eval(e.Row.DataItem, "KodAgih").ToString()
                If strJenis = "TB" Then
                    e.Row.FindControl("btnTB").Visible = True
                    e.Row.FindControl("btnKG").Visible = False
                    TryCast(e.Row.FindControl("lblAmaun"), Label).ForeColor = System.Drawing.Color.Blue
                ElseIf strJenis = "KG" Then
                    e.Row.FindControl("btnTB").Visible = False
                    e.Row.FindControl("btnKG").Visible = True
                    TryCast(e.Row.FindControl("lblAmaun"), Label).ForeColor = ColorTranslator.FromHtml("#990000")
                End If
            End If


        Catch ex As Exception

        End Try
    End Sub





    Private Sub gvSenarai_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvSenarai.Sorting
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
            gvSenarai.DataSource = sortedView
            gvSenarai.DataBind()

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

    Protected Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.Click

        If rbMenu.SelectedValue = 1 Then
            Dim decBaki As Decimal = CDec(txtBaki.Text)
            Dim decAmaun As Decimal = CDec(txtAmaun.Text)

            If decAmaun > decBaki Then
                fGlobalAlert("Amaun yang dimasukkan melebihi dari baki peruntukan vot!", Me.Page, Me.[GetType]())
                Exit Sub
            End If
        End If

        If fSimpan() Then
            fGlobalAlert("Rekod telah disimpan!", Me.Page, Me.[GetType]())
            fReset()
            fBindGvSenarai()
        Else
            fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
        End If

    End Sub

    Private Function fSimpan()

        Dim strSubMod As String
        Dim dbconn As New DBKewConn
        Dim blnSuccess As Boolean = True
        Dim paramSql() As SqlParameter
        Dim strKodSubMenu = ViewState("sesKodSubMenu")

        Try
            Dim strSql As String

            Dim strTkhAgih As String = Now.ToString("yyyy-MM-dd")
            Dim strTahun As String = Trim(txtTahun.Text.TrimEnd)
            Dim strKodKw As String = Trim(ddlKW.SelectedValue.TrimEnd)
            Dim strKodKo As String = Trim(ddlKO.SelectedValue.TrimEnd)
            Dim strKodPTj As String = Trim(ddlPTJ.SelectedValue.TrimEnd)
            Dim strKodKp As String = Trim(ddlKP.SelectedValue.TrimEnd)
            Dim strObjAm As String = Trim(ddlObjAm.SelectedValue.TrimEnd)
            Dim strObjSbg As String = Trim(ddlObjSbg.SelectedValue.TrimEnd)
            Dim strAmaun As String = CDec(Trim(txtAmaun.Text.TrimEnd))
            Dim strKodAgih As String
            Dim strButiran As String = Trim(txtButiran.Text.TrimEnd)


            If rbMenu.SelectedIndex = 0 Then
                strKodAgih = "TB"
                strSubMod = "Penambahan"
            ElseIf rbMenu.SelectedIndex = 1 Then
                strKodAgih = "KG"
                strSubMod = "Pengurangan"
            End If


            '1- SIMPAN KW
            dbconn.sConnBeginTrans()
            Dim strIndKw As String = fGetNoSiri("KW", Trim(txtTahun.Text.TrimEnd))
            strSql = "INSERT INTO BG04_AgihKw(BG04_IndKw,BG04_Tahun,KodKw,BG04_Amaun,KodAgih,BG04_TkhAgih,BG04_Butiran,BG04_Status) " &
                        "VALUES (@IndKw,@Tahun,@KodKw,@Amaun,@KodAgih,@TkhAgih,@Butiran,@Status)"

            paramSql = {
                New SqlParameter("@IndKw", strIndKw),
                New SqlParameter("@Tahun", strTahun),
                New SqlParameter("@KodKw", strKodKw),
                New SqlParameter("@Amaun", strAmaun),
                New SqlParameter("@KodAgih", strKodAgih),
                New SqlParameter("@TkhAgih", strTkhAgih),
                New SqlParameter("@Butiran", strButiran),
                New SqlParameter("@Status", 1)}


            If Not dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                blnSuccess = False
                Exit Try
            Else
                'AUDIT LOG
                sLogMedan = "BG04_IndKw|BG04_Tahun|KodKw|BG04_Amaun|KodAgih|BG04_TkhAgih|BG04_Butiran|BG04_Status"
                sLogBaru = strIndKw & "|" & strTahun & "|" & strKodKw & "|" & strKodAgih & "|" & strTkhAgih & "|" & strKodAgih & "|" & strTkhAgih & "|" & strButiran & "|1"

                strSql = "insert into MK_AuditLog (UserID, UserGroup, UserUbah, SubMenu, Keterangan, InfoTable, RefKey, RefNo, InfoMedan, InfoBaru, " _
    & " InfoLama, UserIP, PCName) " _
    & " values (@UserID,@UserGroup,@UserUbah,@SubMenu,@Keterangan,@InfoTable,@RefKey, @RefNo,@InfoMedan,@InfoBaru,@InfoLama, " _
    & " @UserIP,@PCName)"

                paramSql = {
                    New SqlParameter("@UserID", strNoStaf),
                    New SqlParameter("@UserGroup", sLogLevel),
                    New SqlParameter("@UserUbah", "-"),
                     New SqlParameter("@SubMenu", strKodSubMenu),
                    New SqlParameter("@Keterangan", "INSERT"),
                    New SqlParameter("@InfoTable", "BG04_AgihKw"),
                    New SqlParameter("@RefKey", "-"),
                    New SqlParameter("@RefNo", "-"),
                    New SqlParameter("@InfoMedan", sLogMedan),
                    New SqlParameter("@InfoBaru", sLogBaru),
                    New SqlParameter("@InfoLama", "-"),
                    New SqlParameter("@UserIP", lsLogUsrIP),
                    New SqlParameter("@PCName", lsLogUsrPC)
                }

                If Not dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                    blnSuccess = False
                    Exit Try
                End If
            End If

            '2- SIMPAN PTJ
            Dim strIndPTj As String = fGetNoSiri("PTj", Trim(txtTahun.Text.TrimEnd))
            strSql = "INSERT INTO BG05_AgihPTJ (BG04_IndKw,BG05_IndPTJ,BG05_Tahun,KodKw,KodKO,KodPTJ,BG05_Amaun,KodAgih,BG05_TkhAgih,BG05_Status) " &
                        "VALUES (@IndKw,@IndPTj,@Tahun,@KodKw,@KodKo,@KodPTj,@Amaun,@KodAgih,@TkhAgih,@Status)"

            paramSql = {
                New SqlParameter("@IndKw", strIndKw),
                New SqlParameter("@IndPTj", strIndPTj),
                New SqlParameter("@Tahun", strTahun),
                New SqlParameter("@KodKw", strKodKw),
                New SqlParameter("@KodKo", strKodKo),
                New SqlParameter("@KodPTj", strKodPTj),
                New SqlParameter("@Amaun", strAmaun),
                New SqlParameter("@KodAgih", strKodAgih),
                New SqlParameter("@TkhAgih", strTkhAgih),
                 New SqlParameter("@Status", 1)}

            If Not dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                blnSuccess = False
                Exit Try
            Else
                'AUDIT LOG
                sLogMedan = "BG04_IndKw|BG05_IndPTJ|BG05_Tahun|KodKw|KodKO|KodPTJ|BG05_Amaun|KodAgih|BG05_TkhAgih|BG05_Status"
                sLogBaru = strIndKw & "|" & strIndPTj & "|" & strTahun & "|" & strKodKw & "|" & strKodKo & "|" & strKodPTj & "|" & strAmaun & "|" & strKodAgih & "|" & strTkhAgih & "|1"

                strSql = "insert into MK_AuditLog (UserID, UserGroup, UserUbah, SubMenu, Keterangan, InfoTable, RefKey, RefNo, InfoMedan, InfoBaru, " _
    & " InfoLama, UserIP, PCName) " _
    & " values (@UserID,@UserGroup,@UserUbah,@SubMenu,@Keterangan,@InfoTable,@RefKey, @RefNo,@InfoMedan,@InfoBaru,@InfoLama, " _
    & " @UserIP,@PCName)"

                paramSql = {
                    New SqlParameter("@UserID", strNoStaf),
                    New SqlParameter("@UserGroup", sLogLevel),
                    New SqlParameter("@UserUbah", "-"),
                     New SqlParameter("@SubMenu", strKodSubMenu),
                    New SqlParameter("@Keterangan", "INSERT"),
                    New SqlParameter("@InfoTable", "BG05_AgihPTJ"),
                    New SqlParameter("@RefKey", "-"),
                    New SqlParameter("@RefNo", "-"),
                    New SqlParameter("@InfoMedan", sLogMedan),
                    New SqlParameter("@InfoBaru", sLogBaru),
                    New SqlParameter("@InfoLama", "-"),
                    New SqlParameter("@UserIP", lsLogUsrIP),
                    New SqlParameter("@PCName", lsLogUsrPC)
                }

                If Not dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                    blnSuccess = False
                    Exit Try
                End If
            End If

            '3- SIMPAN OBJEK AM
            Dim strIndObjAm As String = fGetNoSiri("OBJAM", Trim(txtTahun.Text.TrimEnd))
            strSql = "INSERT INTO BG06_AgihObjAm (BG05_IndPTJ,BG06_IndObjAm,BG06_Tahun,KodKw, KodKo, KodPTJ, KodKp ,KodVot,BG06_Amaun,KodAgih,BG06_TkhAgih,BG06_Status)  " &
"VALUES (@IndPTj,@IndObjAm,@Tahun,@KodKw,@KodKo,@KodPTj, @KodKp,@KodVot,@Amaun,@KodAgih,@TkhAgih,@Status)"

            paramSql = {
                New SqlParameter("@IndPTj", strIndPTj),
                New SqlParameter("@IndObjAm", strIndObjAm),
                New SqlParameter("@Tahun", strTahun),
                New SqlParameter("@KodKw", strKodKw),
                New SqlParameter("@KodKo", strKodKo),
                New SqlParameter("@KodPTj", strKodPTj),
                New SqlParameter("@KodKp", strKodKp),
                New SqlParameter("@KodVot", strObjAm),
                New SqlParameter("@Amaun", strAmaun),
                New SqlParameter("@KodAgih", strKodAgih),
                New SqlParameter("@TkhAgih", strTkhAgih),
                New SqlParameter("@Status", 1)}

            If Not dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                blnSuccess = False
                Exit Try
            Else
                'AUDIT LOG
                sLogMedan = "BG05_IndPTJ|BG06_IndObjAm|BG06_Tahun|KodKw| KodKo| KodPTJ| KodKp |KodVot|BG06_Amaun|KodAgih|BG06_TkhAgih|BG06_Status"
                sLogBaru = strIndPTj & "|" & strIndObjAm & "|" & strTahun & "|" & strKodKw & "|" & strKodKo & "|" & strKodPTj & "|" & strKodKp & "|" & strObjAm & "|" & strAmaun & "|" & strKodAgih & "|" & strTkhAgih & "|1"

                strSql = "insert into MK_AuditLog (UserID, UserGroup, UserUbah, SubMenu, Keterangan, InfoTable, RefKey, RefNo, InfoMedan, InfoBaru, " _
    & " InfoLama, UserIP, PCName) " _
    & " values (@UserID,@UserGroup,@UserUbah,@SubMenu,@Keterangan,@InfoTable,@RefKey, @RefNo,@InfoMedan,@InfoBaru,@InfoLama, " _
    & " @UserIP,@PCName)"

                paramSql = {
                    New SqlParameter("@UserID", strNoStaf),
                    New SqlParameter("@UserGroup", sLogLevel),
                    New SqlParameter("@UserUbah", "-"),
                     New SqlParameter("@SubMenu", strKodSubMenu),
                    New SqlParameter("@Keterangan", "INSERT"),
                    New SqlParameter("@InfoTable", "BG06_AgihObjAm"),
                    New SqlParameter("@RefKey", "-"),
                    New SqlParameter("@RefNo", "-"),
                    New SqlParameter("@InfoMedan", sLogMedan),
                    New SqlParameter("@InfoBaru", sLogBaru),
                    New SqlParameter("@InfoLama", "-"),
                    New SqlParameter("@UserIP", lsLogUsrIP),
                    New SqlParameter("@PCName", lsLogUsrPC)
                }

                If Not dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                    blnSuccess = False
                    Exit Try
                End If
            End If

            '4- SIMPAN OBJEK SEBAGAI
            Dim strIndObjSbg As String = fGetNoSiri("OBJSBG", Trim(txtTahun.Text.TrimEnd))
            strSql = "INSERT INTO BG07_AgihObjSbg(BG06_IndObjAm,BG07_IndObjSbg,BG07_Tahun,KodKw,KodKO,KodPTJ,KodKp, KodVot,BG07_Amaun,KodAgih,BG07_TkhAgih,BG07_Status, BG07_Ulasan) " &
                        "VALUES (@IndObjAm,@IndObjSbg,@Tahun,@KodKw,@KodKo,@KodPTj,@KodKp,@KodVot,@Amaun,@KodAgih,@TkhAgih,@Status, @Ulasan)"

            paramSql = {
                New SqlParameter("@IndObjAm", strIndObjAm),
                New SqlParameter("@IndObjSbg", strIndObjSbg),
                New SqlParameter("@Tahun", strTahun),
                New SqlParameter("@KodKw", strKodKw),
                New SqlParameter("@KodKo", strKodKo),
                New SqlParameter("@KodPTj", strKodPTj),
                New SqlParameter("@KodKp", strKodKp),
                New SqlParameter("@KodVot", strObjSbg),
                New SqlParameter("@Amaun", strAmaun),
                New SqlParameter("@KodAgih", strKodAgih),
                New SqlParameter("@TkhAgih", strTkhAgih),
                New SqlParameter("@Status", 1),
                New SqlParameter("@Ulasan", strButiran)}

            If Not dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                blnSuccess = False
                Exit Try
            Else
                'AUDIT LOG
                sLogMedan = "BG06_IndObjAm|BG07_IndObjSbg|BG07_Tahun|KodKw|KodKO|KodPTJ|KodKp| KodVot|BG07_Amaun|KodAgih|BG07_TkhAgih|BG07_Status| BG07_Ulasan"
                sLogBaru = strIndObjAm & "|" & strIndObjSbg & "|" & strTahun & "|" & strKodKw & "|" & strKodKo & "|" & strKodPTj & "|" & strKodKp & "|" & strObjSbg & "|" & strAmaun & "|" & strAmaun & "|" & strKodAgih & "|" & strTkhAgih & "|1" & strButiran

                strSql = "insert into MK_AuditLog (UserID, UserGroup, UserUbah, SubMenu, Keterangan, InfoTable, RefKey, RefNo, InfoMedan, InfoBaru, " _
    & " InfoLama, UserIP, PCName) " _
    & " values (@UserID,@UserGroup,@UserUbah,@SubMenu,@Keterangan,@InfoTable,@RefKey, @RefNo,@InfoMedan,@InfoBaru,@InfoLama, " _
    & " @UserIP,@PCName)"

                paramSql = {
                    New SqlParameter("@UserID", strNoStaf),
                    New SqlParameter("@UserGroup", sLogLevel),
                    New SqlParameter("@UserUbah", "-"),
                     New SqlParameter("@SubMenu", strKodSubMenu),
                    New SqlParameter("@Keterangan", "INSERT"),
                    New SqlParameter("@InfoTable", "BG07_AgihObjSbg"),
                    New SqlParameter("@RefKey", "-"),
                    New SqlParameter("@RefNo", "-"),
                    New SqlParameter("@InfoMedan", sLogMedan),
                    New SqlParameter("@InfoBaru", sLogBaru),
                    New SqlParameter("@InfoLama", "-"),
                    New SqlParameter("@UserIP", lsLogUsrIP),
                    New SqlParameter("@PCName", lsLogUsrPC)
                }

                If Not dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                    blnSuccess = False
                    Exit Try
                End If
            End If

            Try
                Dim intBil As Integer = 0
                Dim FileName As String
                Dim folderPath As String
                Dim strKodSub As String = Request.QueryString("KodSub")
                Dim strKodModul As String = strKodSub.Substring(0, 2)

                Dim intIdLamp As Integer
                For i As Integer = 0 To Request.Files.Count - 1
                    intBil += 1
                    Dim PostedFile As HttpPostedFile = Request.Files(i)

                    If PostedFile.ContentLength > 0 Then

                        FileName = System.IO.Path.GetFileName(PostedFile.FileName)

                        Dim strExt As String = Path.GetExtension(FileName)

                        folderPath = Server.MapPath("~/Upload/Document/" & strKodModul & "/" & strIndKw & "/")

                        Dim strConType = PostedFile.ContentType


                        'Insert Into AR06_STATUSDOK
                        strSql = "insert into BG14_Lampiran (BG04_IndKw, BG05_IndPTJ, BG06_IndObjAm, BG07_IndObjSbg, BG14_Path, BG14_NamaDok, BG14_ContentType, BG14_Status)
values (@BG04_IndKw, @BG05_IndPTJ, @BG06_IndObjAm, @BG07_IndObjSbg, @BG14_Path, @BG14_NamaDok,@BG14_ContentType, @BG14_Status)"

                        paramSql =
                         {
                            New SqlParameter("@BG04_IndKw", strIndKw),
                            New SqlParameter("@BG05_IndPTJ", strIndPTj),
                            New SqlParameter("@BG06_IndObjAm", strIndObjAm),
                            New SqlParameter("@BG07_IndObjSbg", strIndObjSbg),
                            New SqlParameter("@BG14_Path", folderPath),
                            New SqlParameter("@BG14_NamaDok", FileName),
                            New SqlParameter("@BG14_ContentType", strConType),
                            New SqlParameter("@BG14_Status", 1)
                        }

                        If Not dbconn.fInsertCommand(strSql, paramSql, intIdLamp) > 0 Then
                            blnSuccess = False
                            Exit Try
                        Else
                            'AUDIT LOG
                            sLogMedan = "BG04_IndKw|BG05_IndPTJ|BG06_IndObjAm|BG07_IndObjSbg|BG14_Path| BG14_ContentType|BG14_Status"
                            sLogBaru = strIndKw & "|" & strIndPTj & "|" & strIndObjAm & "|" & strIndObjSbg & "|" & folderPath & "|" & strConType & "|1"

                            strSql = "insert into MK_AuditLog (UserID, UserGroup, UserUbah, SubMenu, Keterangan, InfoTable, RefKey, RefNo, InfoMedan, InfoBaru, " _
                & " InfoLama, UserIP, PCName) " _
                & " values (@UserID,@UserGroup,@UserUbah,@SubMenu,@Keterangan,@InfoTable,@RefKey, @RefNo,@InfoMedan,@InfoBaru,@InfoLama, " _
                & " @UserIP,@PCName)"

                            paramSql = {
                                New SqlParameter("@UserID", strNoStaf),
                                New SqlParameter("@UserGroup", sLogLevel),
                                New SqlParameter("@UserUbah", "-"),
                                 New SqlParameter("@SubMenu", strKodSubMenu),
                                New SqlParameter("@Keterangan", "INSERT"),
                                New SqlParameter("@InfoTable", "BG14_Lampiran"),
                                New SqlParameter("@RefKey", "-"),
                                New SqlParameter("@RefNo", "-"),
                                New SqlParameter("@InfoMedan", sLogMedan),
                                New SqlParameter("@InfoBaru", sLogBaru),
                                New SqlParameter("@InfoLama", "-"),
                                New SqlParameter("@UserIP", lsLogUsrIP),
                                New SqlParameter("@PCName", lsLogUsrPC)
                            }

                            If Not dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                                blnSuccess = False
                                Exit Try
                            End If
                        End If


                        If Not Directory.Exists(folderPath) Then
                            Directory.CreateDirectory(folderPath)
                        End If


                        FileName = strIndKw & "-" & intIdLamp & strExt
                        PostedFile.SaveAs(folderPath & FileName)
                        Dim bytFileContent As Byte() = fReadFile(folderPath & "/" & FileName)
                        Dim SrcPath = folderPath & "/" & strIndKw & "-" & intIdLamp


                        If fEncFile(SrcPath, bytFileContent) Then

                            fErrorLog("folderPath - " & folderPath & " , FileName - " & FileName & ", fullpath - " & folderPath & "/" & FileName & ", SrcPath - " & SrcPath)
                            'File.SetAttributes(folderPath & "/" & FileName, FileAttributes.Normal)
                            File.Delete(folderPath & "/" & FileName)

                        Else
                            blnSuccess = False
                            Exit Try
                        End If

                    End If
                Next

            Catch ex As Exception
                blnSuccess = False
                Exit Try
            End Try

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

    Protected Sub lbtnBaru_Click(sender As Object, e As EventArgs) Handles lbtnBaru.Click
        fReset()
    End Sub

    Protected Sub lbtnHapus_Click(sender As Object, e As EventArgs) Handles lbtnHapus.Click
        Try
            Dim blnSuccess As Boolean = True
            Dim strSql As String
            Dim dbConn As New DBKewConn

            Dim dt As New DataTable
            dt = CType(ViewState("dtInd"), DataTable)

            Dim strIndKW As String = dt.Rows(0)("IndKW")
            Dim strIndPTj As String = dt.Rows(0)("IndPTj")
            Dim strIndObjAm As String = dt.Rows(0)("IndObjAm")
            'Dim strIndDasar As String = dt.Rows(0)("IndDasar")
            Dim strIndObjSbg As String = dt.Rows(0)("IndObjSbg")

            '1- PADAM AGIH KW
            strSql = "update BG04_AgihKw set BG04_Status = @Status where BG04_IndKw = @IndKW"
            Dim paramSql() As SqlParameter = {
                New SqlParameter("@Status", 0),
                New SqlParameter("@IndKW", strIndKW)
                }

            dbConn.sConnBeginTrans()
            If dbConn.fUpdateCommand(strSql, paramSql) > 0 Then
                dbConn.sConnCommitTrans()
            Else
                blnSuccess = False
                dbConn.sConnRollbackTrans()
                Exit Sub
            End If

            '2- PADAM AGIH PTJ
            strSql = "update BG05_AgihPTJ set BG05_Status = @Status where BG05_IndPTJ = @IndPTj"
            Dim paramSql2() As SqlParameter = {
                New SqlParameter("@Status", 0),
                New SqlParameter("@IndPTj", strIndPTj)
                }

            dbConn.sConnBeginTrans()
            If dbConn.fUpdateCommand(strSql, paramSql2) > 0 Then
                dbConn.sConnCommitTrans()
            Else
                blnSuccess = False
                dbConn.sConnRollbackTrans()
                Exit Sub
            End If

            '3- PADAM AGIH OBJEK AM
            strSql = "update BG06_AgihObjAm set BG06_Status = @Status where BG06_IndObjAm = @IndObjAm"
            Dim paramSql3() As SqlParameter = {
                New SqlParameter("@Status", 0),
                New SqlParameter("@IndObjAm", strIndObjAm)
                }

            dbConn.sConnBeginTrans()
            If dbConn.fUpdateCommand(strSql, paramSql3) > 0 Then
                dbConn.sConnCommitTrans()
            Else
                blnSuccess = False
                dbConn.sConnRollbackTrans()
                Exit Sub
            End If

            '4- PADAM AGIH OBJEK SEBAGAI
            strSql = "update BG07_AgihObjSbg set BG07_Status = @Status where BG07_IndObjSbg = @IndObjAm"
            Dim paramSql4() As SqlParameter = {
                New SqlParameter("@Status", 0),
                New SqlParameter("@IndObjAm", strIndObjSbg)
                }

            dbConn.sConnBeginTrans()
            If dbConn.fUpdateCommand(strSql, paramSql4) > 0 Then
                dbConn.sConnCommitTrans()
            Else
                blnSuccess = False
                dbConn.sConnRollbackTrans()
                Exit Sub
            End If


            If blnSuccess Then
                fGlobalAlert("Maklumat telah dipadam!", Me.Page, Me.[GetType]())
                fReset()
                fBindGvSenarai()
            Else
                fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtAmaun_TextChanged(sender As Object, e As EventArgs) Handles txtAmaun.TextChanged
        Try

            Dim txtAmaun As TextBox = CType(CType(sender, Control), TextBox)
            Dim decAmaun As Decimal = CDec(txtAmaun.Text)

            txtAmaun.BackColor = Color.White
            If rbMenu.SelectedValue = 1 Then
                Dim decBaki As Decimal = CDec(txtBaki.Text)


                If decAmaun > decBaki Then
                    txtAmaun.BackColor = ColorTranslator.FromHtml("#f1b6b1")
                    fGlobalAlert("Amaun yang dimasukkan melebihi dari baki peruntukan vot!", Me.Page, Me.[GetType]())
                End If

            End If

            txtAmaun.Text = FormatNumber(decAmaun)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub sClearTxtAmaun()
        txtAmaun.BackColor = Color.White
        txtAmaun.Text = ""
    End Sub

    Private Sub rbMenu_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rbMenu.SelectedIndexChanged

        ddlObjSbg.SelectedIndex = 0

        txtBaki.Text = ""
        txtAmaun.Text = ""
    End Sub

    Protected Sub lbtnMohonbaru_Click(sender As Object, e As EventArgs) Handles lbtnMohonbaru.Click
        fReset()

        divList.Visible = False
        divDetail.Visible = True

    End Sub

    Private Sub lnkBtnBack_Click(sender As Object, e As EventArgs) Handles lnkBtnBack.Click

        fBindGvSenarai()
        divList.Visible = True
        divDetail.Visible = False

        hidIndKW.Value = ""
        hidIndPTj.Value = ""
        hidIndObjAm.Value = ""
        hidIndObjSbg.Value = ""

        lbtnHapus.Visible = False
        lbtnSimpan.Visible = True

    End Sub

End Class