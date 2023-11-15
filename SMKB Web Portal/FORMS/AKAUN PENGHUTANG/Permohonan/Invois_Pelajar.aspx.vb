
Imports System.Data.SqlClient

Public Class Invois_Pelajar
    Inherits System.Web.UI.Page
    'Private dbconn As New DBKewConn()

    Dim sLogBaru As String
    Dim sLogMedan As String
    Dim strNoStaf As String = UserInfo.strSesStaffID
    Dim sLogLevel As String = UserInfo.strUsrTahap
    Dim lsLogUsrIP As String = UserInfo.strUsrPcIP
    Dim lsLogUsrPC As String = UserInfo.strUsrPCName
    Dim strTotJumlah As String
    Dim decJumlah As Decimal
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Session("sesKodSubMenu") = Request.QueryString("KodSubMenu")
                Dim strStaffID = Session("ssusrID")
                Dim strKodSubMenu As String = Request.QueryString("KodSubMenu")
                If fCheckPowerUser(strStaffID, strKodSubMenu) Then
                    ViewState("PowerUser") = True
                Else
                    ViewState("PowerUser") = False
                End If
                Dim strNoInvCuk As String = Request.QueryString("IdInvCuk")

                txtTkhMohon.Text = Date.Today.ToString("dd/MM/yyyy")
                txtKodPTj.Text = Session("ssusrKodPTj")
                txtPTj.Text = Session("ssusrPTj")
                lblNoPmhn.Text = Session("ssusrID")
                lblNamaPemohon.Text = Session("ssusrName")
                lblJawatan.Text = Session("ssusrPost")

                fBindDdlBank()
                fBindDdlFilStat()
                sLoadLst()

                sClearGvLamp()
                sCleargvInvDt()

                'fBindDdlNegara()
                'fBindDdlNegeri()
                'fLoadKW()
                'fLoadKO2()
                'fLoadPTj2()
                'fLoadKP2()
                'fLoadVot2()


            End If
        Catch ex As Exception

        End Try


    End Sub

    Private Sub fBindDdlFilStat()
        Dim dbconn As New DBKewConn
        Try
            Dim strSql As String = "select KodStatus, Butiran from AR_StatusDok where KodStatus in (01,02,03, 04, 05,12)"

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

    Private Sub sLoadLst()
        Dim intRec As Integer = 0
        Dim strFilter As String
        Dim strStatDok As String
        Try

            sClearGvLst()

            If ddlCarian.SelectedValue = 1 Then
                strFilter = " and AR01_NoBilSem = '" & Trim(txtCarian.Text.TrimEnd) & "'"
            End If

            If ddlFilStat.SelectedValue = 0 Then
                strStatDok = "01,02,03,04,05,12"
            Else
                strStatDok = ddlFilStat.SelectedValue
            End If

            Dim strSql As String = "select AR01_IdBil, AR01_NoBilSem, AR01_NoBil, AR01_TkhMohon, AR01_Tujuan, AR01_Kategori, (select Butiran  from MK_KategoriPenerima where Kod = AR01_Bil .AR01_Kategori ) as ButKat, AR01_Jumlah ,AR01_StatusDok, (select Butiran  from AR_StatusDok where KodStatus = AR01_Bil .AR01_StatusDok ) as ButStatDok  
from AR01_Bil where AR01_StatusDok in (" & strStatDok & ") AND AR01_NoStaf ='" & Session("ssusrID") & "' and AR01_Jenis ='02' and AR01_KodPTJMohon = '" & Session("ssusrKodPTj") & "' " & strFilter & " Order BY AR01_TkhMohon"

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

    'Private Sub sClearGvLstPel()
    '    gvLstPel.DataSource = New List(Of String)
    '    gvLstPel.DataBind()
    'End Sub

    Private Sub sClearGvLamp()
        gvLamp.DataSource = New List(Of String)
        gvLamp.DataBind()
    End Sub

    Private Sub fBindDdlBank()
        Try
            Dim strSql As String
            strSql = "select KodBank, (KodBank + ' - ' + NamaBank) as NamaBank from MK_Bank where kodbank ='76107' order by kodbank "

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)
            ddlBank.DataSource = ds
            ddlBank.DataTextField = "NamaBank"
            ddlBank.DataValueField = "KodBank"
            ddlBank.DataBind()

            'ddlBank.Items.Insert(0, New ListItem(" - SILA PILIH -", 0))
            ddlBank.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub
    'Private Sub fBindJenisUrusniaga()
    '    Try
    '        Dim strsql As String
    '        strsql = "select KodUrusniaga , (KodUrusniaga + ' - ' + Butiran ) as Butiran from RC_Urusniaga where KodUrusniaga in (7,10) order by KodUrusniaga desc"

    '        Dim ds As New DataSet
    '        Dim dbconn As New DBKewConn
    '        Dim dt As New DataTable
    '        ds = dbconn.fSelectCommand(strsql)
    '        ddlJenisUrus.DataSource = ds
    '        ddlJenisUrus.DataTextField = "Butiran"
    '        ddlJenisUrus.DataValueField = "KodUrusniaga"
    '        ddlJenisUrus.DataBind()

    '        ddlJenisUrus.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
    '        ddlJenisUrus.SelectedIndex = 0
    '    Catch ex As Exception

    '    End Try

    'End Sub
    'Private Sub fBindDdlNegeri()
    '    Try
    '        Dim strsql As String
    '        strsql = "select KodNegeri , (KodNegeri + ' - ' + Butiran ) as Butiran from MK_Negeri order by KodNegeri"

    '        Dim ds As New DataSet
    '        Dim dbconn As New DBKewConn
    '        Dim dt As New DataTable
    '        ds = dbconn.fSelectCommand(strsql)
    '        ddlNegeri.DataSource = ds
    '        ddlNegeri.DataTextField = "Butiran"
    '        ddlNegeri.DataValueField = "KodNegeri"
    '        ddlNegeri.DataBind()

    '        ddlNegeri.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
    '        ddlNegeri.SelectedIndex = 0
    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Private Sub fBindDdlNegara()
    '    Try
    '        Dim strsql As String
    '        strsql = "select KodNegara , (KodNegara + ' - ' + Butiran ) as Butiran from MK_Negara order by KodNegara "

    '        Dim ds As New DataSet
    '        Dim dbconn As New DBKewConn
    '        Dim dt As New DataTable
    '        ds = dbconn.fSelectCommand(strsql)
    '        ddlNegara.DataSource = ds
    '        ddlNegara.DataTextField = "Butiran"
    '        ddlNegara.DataValueField = "KodNegara"
    '        ddlNegara.DataBind()

    '        ddlNegara.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
    '        ddlNegara.SelectedIndex = 0
    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Private Sub fBindDdlNegaraSMP()
    '    Try
    '        Dim strsql As String
    '        strsql = "select SMP_KODNEGARA  , (SMP_KODNEGARA + ' - ' + SMP_NEGARA ) as Butiran from SMP_Negara order by SMP_KODNEGARA "

    '        Dim ds As New DataSet
    '        Dim dbconn As New DBKewConn
    '        Dim dt As New DataTable
    '        ds = dbconn.fSelectCommand(strsql)
    '        ddlNegara.DataSource = ds
    '        ddlNegara.DataTextField = "Butiran"
    '        ddlNegara.DataValueField = "SMP_KODNEGARA"
    '        ddlNegara.DataBind()

    '        ddlNegara.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
    '        ddlNegara.SelectedIndex = 0
    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Private Sub SetInitialRow()
    '    Try
    '        Dim dt As DataTable = New DataTable()
    '        Dim dr As DataRow = Nothing
    '        dt.Columns.Add(New DataColumn("AR01_BilDtID", GetType(Integer)))
    '        dt.Columns.Add(New DataColumn("KodKw", GetType(String)))
    '        dt.Columns.Add(New DataColumn("KodKO", GetType(String)))
    '        dt.Columns.Add(New DataColumn("KodPTJ", GetType(String)))
    '        dt.Columns.Add(New DataColumn("KodKP", GetType(String)))
    '        dt.Columns.Add(New DataColumn("KodVot", GetType(String)))
    '        dt.Columns.Add(New DataColumn("AR01_Perkara", GetType(String)))
    '        dt.Columns.Add(New DataColumn("AR01_Kuantiti", GetType(String)))
    '        dt.Columns.Add(New DataColumn("AR01_kadarHarga", GetType(String)))
    '        dt.Columns.Add(New DataColumn("AR01_Jumlah", GetType(String)))

    '        dr = dt.NewRow()
    '        dr("AR01_BilDtID") = 0
    '        dr("KodKw") = String.Empty
    '        dr("KodKO") = String.Empty
    '        dr("KodPTJ") = String.Empty
    '        dr("KodKP") = String.Empty
    '        dr("KodVot") = String.Empty
    '        dr("AR01_Perkara") = String.Empty
    '        dr("AR01_Kuantiti") = "0"
    '        dr("AR01_kadarHarga") = "0.00"
    '        dr("AR01_Jumlah") = "0.00"

    '        dt.Rows.Add(dr)
    '        ViewState("dtInvDt") = dt
    '        gvTransDt.DataSource = dt
    '        gvTransDt.DataBind()

    '    Catch ex As Exception

    '    End Try

    'End Sub

    Private Function sInitDtLamp()
        Try
            Dim dt As DataTable = New DataTable()
            dt.Columns.Add(New DataColumn("AR02_IdLamp", GetType(String)))
            dt.Columns.Add(New DataColumn("AR02_NoMatrik", GetType(String)))
            dt.Columns.Add(New DataColumn("AR02_NamaPenerima", GetType(String)))
            dt.Columns.Add(New DataColumn("AR02_Kursus", GetType(String)))
            dt.Columns.Add(New DataColumn("AR02_NoKP", GetType(String)))
            dt.Columns.Add(New DataColumn("AR02_Butiran", GetType(String)))
            dt.Columns.Add(New DataColumn("AR02_Amaun", GetType(String)))

            'ViewState("dtLamp") = dt
            Return dt

        Catch ex As Exception
            fErrorLog("sInitDtLamp - " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function


    'Private Sub ddlBank_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBank.SelectedIndexChanged
    '    fBindJenisUrusniaga()

    'End Sub
    'Private Sub ddlJenis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlJenis.SelectedIndexChanged
    '    fBindDdlKategori()
    'End Sub
    'Private Sub ddlNegara_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNegara.SelectedIndexChanged
    '    fBindDdlNegeri()
    'End Sub
    'Private Sub fLoadKW()
    '    Try

    '        Dim strSql As String = "Select KodKw,Butiran,(KodKw + ' - ' + Butiran ) as ButiranKW from MK_Kw order by KodKw"

    '        Dim ds As New DataSet
    '        Dim dbconn As New DBKewConn
    '        Dim dt As New DataTable
    '        ds = dbconn.fSelectCommand(strSql)

    '        ViewState("dsKW") = ds

    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Private Sub fLoadKO2()
    '    Try
    '        Dim strSql As String = "SELECT DISTINCT TOP 100 PERCENT dbo.MK_kodOperasi.KodKO, (dbo.MK_kodOperasi.KodKO + ' - ' + dbo.MK_kodOperasi.Butiran) as Butiran " &
    '            "FROM dbo.MK_kodOperasi INNER JOIN dbo.MK01_VotTahun ON dbo.MK_kodOperasi.KodKO = dbo.MK01_VotTahun.KodKO " &
    '            "where dbo.MK01_VotTahun.MK01_Tahun = YEAR(GETDATE()) ORDER BY dbo.MK_kodOperasi.Kodko"

    '        Dim ds As New DataSet
    '        Dim dbconn As New DBKewConn
    '        Dim dt As New DataTable
    '        ds = dbconn.fSelectCommand(strSql)
    '        ViewState("dsKO") = ds

    '    Catch ex As Exception

    '    End Try
    'End Sub

    '    Private Sub fLoadPTj2()
    '        Try
    '            Dim strSql As String = "SELECT DISTINCT   MK_PTJ.KodPTJ  , (MK_PTJ.KodPTJ   + ' - ' + MK_PTJ.Butiran  ) as Butiran  
    '                    From MK_PTJ INNER Join MK01_VotTahun ON MK_PTJ.KodPTJ   = MK01_VotTahun.KodPTJ   
    '                    where MK01_VotTahun.MK01_Tahun = YEAR(GETDATE()) and MK_PTJ.KodPTJ <> '-' and MK_PTJ.status = 1 
    'ORDER BY MK_PTJ.KodPTJ "

    '            Dim ds As New DataSet
    '            Dim dbconn As New DBKewConn
    '            Dim dt As New DataTable
    '            ds = dbconn.fSelectCommand(strSql)
    '            ViewState("dsPTj") = ds

    '        Catch ex As Exception

    '        End Try
    '    End Sub

    'Private Sub fLoadKP2()
    '    Try

    '        Dim strSql As String = "SELECT DISTINCT TOP 100 PERCENT MK_KodProjek.KodProjek as KodKP, (MK_KodProjek .KodProjek  + ' - ' + MK_KodProjek.Butiran ) as Butiran  
    '                From MK_KodProjek INNER Join MK01_VotTahun ON MK_KodProjek.KodProjek  = MK01_VotTahun.KodKP
    '                where MK01_VotTahun.MK01_Tahun = YEAR(GETDATE())  ORDER BY MK_KodProjek.KodProjek"

    '        Dim ds As New DataSet
    '        Dim dbconn As New DBKewConn
    '        Dim dt As New DataTable
    '        ds = dbconn.fSelectCommand(strSql)
    '        ViewState("dsKP") = ds

    '    Catch ex As Exception

    '    End Try
    'End Sub

    '    Private Sub fLoadVot2()
    '        Try

    '            Dim strSql As String = "SELECT DISTINCT A.KodVot, (A.KODVOT + ' - ' + B.BUTIRAN) as Butiran FROM MK03_AmTahun A INNER JOIN MK_VOT B ON A.KODVOT = B.KODVOT 
    'Where a.MK03_TAHUN = YEAR(GETDATE()) AND  RIGHT(A.KODVOT,2) <> '00' ORDER BY A.KODVOT"

    '            Dim ds As New DataSet
    '            Dim dbconn As New DBKewConn
    '            Dim dt As New DataTable
    '            ds = dbconn.fSelectCommand(strSql)
    '            ViewState("dsVot") = ds


    '        Catch ex As Exception

    '        End Try
    '    End Sub
    'Private Function fLoadKO(ByVal strKodKW As String) As DataSet
    '    Try

    '        'Dim strSql As String = "SELECT DISTINCT TOP 100 PERCENT dbo.MK_kodOperasi.KodKO, (dbo.MK_kodOperasi.KodKO + ' - ' + dbo.MK_kodOperasi.Butiran) as ButiranKO " &
    '        '    "FROM dbo.MK_kodOperasi INNER JOIN dbo.MK01_VotTahun ON dbo.MK_kodOperasi.KodKO = dbo.MK01_VotTahun.KodKO " &
    '        '    "where dbo.MK01_VotTahun.MK01_Tahun = '" & Now.Year & "' and dbo.mk01_votTahun.KodKw = '" & ddlKW.SelectedValue & "' and dbo.mk01_votTahun.KodPTj = '" & ddlPTJ.SelectedValue & "' ORDER BY dbo.MK_kodOperasi.Kodko"

    '        Dim strSql As String = "SELECT DISTINCT TOP 100 PERCENT dbo.MK_kodOperasi.KodKO, (dbo.MK_kodOperasi.KodKO + ' - ' + dbo.MK_kodOperasi.Butiran) as ButiranKO " &
    '            "FROM dbo.MK_kodOperasi INNER JOIN dbo.MK01_VotTahun ON dbo.MK_kodOperasi.KodKO = dbo.MK01_VotTahun.KodKO " &
    '            "where dbo.MK01_VotTahun.MK01_Tahun = YEAR(GETDATE()) and dbo.mk01_votTahun.KodKw = '" & strKodKW & "' ORDER BY dbo.MK_kodOperasi.Kodko"

    '        'Dim strSql As String = "SELECT DISTINCT TOP 100 PERCENT dbo.MK_kodOperasi.KodKO, (dbo.MK_kodOperasi.KodKO + ' - ' + dbo.MK_kodOperasi.Butiran) as ButiranKO " &
    '        '    "FROM dbo.MK_kodOperasi INNER JOIN dbo.MK01_VotTahun ON dbo.MK_kodOperasi.KodKO = dbo.MK01_VotTahun.KodKO " &
    '        '    "where dbo.MK01_VotTahun.MK01_Tahun = '" & Now.Year & "' ORDER BY dbo.MK_kodOperasi.Kodko"

    '        Dim ds As New DataSet
    '        Dim dbconn As New DBKewConn
    '        Dim dt As New DataTable
    '        ds = dbconn.fSelectCommand(strSql)
    '        'ViewState("dsKO") = ds
    '        Return ds

    '    Catch ex As Exception

    '    End Try
    'End Function
    Private Function fLoadPTj(ByVal strKodKW As String, ByVal strKodKO As String) As DataSet
        Try

            Dim strFilter
            If ViewState("PowerUser") = False Then
                strFilter = " and MK_PTJ.KodPTJ = '" & Session("ssusrKodPTj") & "' "
            End If

            Dim strSql As String = "SELECT DISTINCT   MK_PTJ.KodPTJ  , (MK_PTJ.KodPTJ   + ' - ' + MK_PTJ.Butiran  ) as ButiranPTj  
From MK_PTJ INNER Join MK01_VotTahun ON MK_PTJ.KodPTJ   = MK01_VotTahun.KodPTJ   
where MK01_VotTahun.MK01_Tahun = YEAR(GETDATE()) and MK01_VotTahun.KodKw = '" & strKodKW & "' and MK01_VotTahun.KodKO = '" & strKodKO & "' and MK_PTJ.KodPTJ <> '-' and MK_PTJ.status = 1 " & strFilter & " 
ORDER BY MK_PTJ.KodPTJ"

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

    'Protected Sub ddlKW_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    Try

    '        Dim gvr As GridViewRow = CType(((CType(sender, Control)).NamingContainer), GridViewRow)
    '        Dim ddlKW As DropDownList = CType(gvr.FindControl("ddlKW"), DropDownList)
    '        Dim strKodKW As String = ddlKW.SelectedItem.Value

    '        Dim ddlKO = DirectCast(gvr.FindControl("ddlKO"), DropDownList)
    '        ddlKO.DataSource = fLoadKO(strKodKW)   'ViewState("dsKO")
    '        ddlKO.DataTextField = "ButiranKO"
    '        ddlKO.DataValueField = "KodKO"
    '        ddlKO.DataBind()

    '        ddlKO.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
    '        ddlKO.SelectedIndex = 0
    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Protected Sub ddlKO_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    Try

    '        Dim gvr As GridViewRow = CType(((CType(sender, Control)).NamingContainer), GridViewRow)
    '        Dim ddlKW As DropDownList = CType(gvr.FindControl("ddlKW"), DropDownList)
    '        Dim strKodKW As String = ddlKW.SelectedItem.Value

    '        Dim ddlKO As DropDownList = CType(gvr.FindControl("ddlKO"), DropDownList)
    '        Dim strKodKO As String = ddlKO.SelectedItem.Value

    '        'Load ddlPTj
    '        Dim ddlPTj = DirectCast(gvr.FindControl("ddlPTj"), DropDownList)
    '        ddlPTj.DataSource = fLoadPTj(strKodKW, strKodKO)
    '        ddlPTj.DataTextField = "ButiranPTj"
    '        ddlPTj.DataValueField = "KodPTj"
    '        ddlPTj.DataBind()
    '        ddlPTj.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
    '        ddlPTj.SelectedIndex = 0

    '        'set ddlKP kepada default
    '        Dim ddlKP = DirectCast(gvr.FindControl("ddlKP"), DropDownList)
    '        ddlKP.Items.Clear()
    '        ddlKP.Items.Insert(0, New ListItem("- SILA PILIH PTj -", 0))
    '        ddlKP.SelectedIndex = 0

    '        'set ddlVot kepada default
    '        Dim ddlVot = DirectCast(gvr.FindControl("ddlVot"), DropDownList)
    '        ddlVot.Items.Clear()
    '        ddlVot.Items.Insert(0, New ListItem("- SILA PILIH KP -", 0))
    '        ddlVot.SelectedIndex = 0
    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Protected Sub ddlPTj_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    Try

    '        Dim gvr As GridViewRow = CType(((CType(sender, Control)).NamingContainer), GridViewRow)
    '        Dim ddlKW As DropDownList = CType(gvr.FindControl("ddlKW"), DropDownList)
    '        Dim strKodKW As String = ddlKW.SelectedItem.Value

    '        Dim ddlKO As DropDownList = CType(gvr.FindControl("ddlKO"), DropDownList)
    '        Dim strKodKO As String = ddlKO.SelectedItem.Value

    '        Dim ddlPTj As DropDownList = CType(gvr.FindControl("ddlPTj"), DropDownList)
    '        Dim strKodPTj As String = ddlPTj.SelectedItem.Value

    '        'Load ddlKP
    '        Dim ddlKP = DirectCast(gvr.FindControl("ddlKP"), DropDownList)
    '        ddlKP.DataSource = fGetKP(strKodKW, strKodKO, strKodPTj)
    '        ddlKP.DataTextField = "Butiran"
    '        ddlKP.DataValueField = "KodProjek"
    '        ddlKP.DataBind()
    '        ddlKP.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
    '        ddlKP.SelectedIndex = 0

    '        'set ddlVot kepada default
    '        Dim ddlVot = DirectCast(gvr.FindControl("ddlVot"), DropDownList)
    '        ddlVot.Items.Clear()
    '        ddlVot.Items.Insert(0, New ListItem("- SILA PILIH KP -", 0))
    '        ddlVot.SelectedIndex = 0

    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Protected Sub ddlKP_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    Try

    '        Dim gvr As GridViewRow = CType(((CType(sender, Control)).NamingContainer), GridViewRow)
    '        Dim ddlKW As DropDownList = CType(gvr.FindControl("ddlKW"), DropDownList)
    '        Dim strKodKW As String = ddlKW.SelectedItem.Value

    '        Dim ddlKO As DropDownList = CType(gvr.FindControl("ddlKO"), DropDownList)
    '        Dim strKodKO As String = ddlKO.SelectedItem.Value

    '        Dim ddlPTj As DropDownList = CType(gvr.FindControl("ddlPTj"), DropDownList)
    '        Dim strKodPTj As String = ddlPTj.SelectedItem.Value

    '        Dim ddlKP As DropDownList = CType(gvr.FindControl("ddlKP"), DropDownList)
    '        Dim strKodKP As String = ddlKP.SelectedItem.Value

    '        Dim ddlVot = DirectCast(gvr.FindControl("ddlVot"), DropDownList)
    '        ddlVot.DataSource = fLoadVot(strKodKW, strKodKO, strKodPTj, strKodKP)   'ViewState("dsKO")
    '        ddlVot.DataTextField = "Butiran"
    '        ddlVot.DataValueField = "KodVot"
    '        ddlVot.DataBind()

    '        ddlVot.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
    '        ddlVot.SelectedIndex = 0
    '    Catch ex As Exception

    '    End Try
    'End Sub


    'Private Sub AddNewRowToGrid()
    '    Try
    '        Dim rowIndex As Integer = 0
    '        'Dim decJumBayar As Decimal
    '        If ViewState("dtInvDt") IsNot Nothing Then
    '            Dim dtvsDtInv As DataTable = CType(ViewState("dtInvDt"), DataTable)
    '            Dim drCurrentRow As DataRow = Nothing

    '            If dtvsDtInv.Rows.Count > 0 Then

    '                For i As Integer = 1 To dtvsDtInv.Rows.Count
    '                    Dim ddl1 As DropDownList = CType(gvTransDt.Rows(rowIndex).Cells(1).FindControl("ddlKW"), DropDownList)
    '                    Dim ddl2 As DropDownList = CType(gvTransDt.Rows(rowIndex).Cells(2).FindControl("ddlKO"), DropDownList)
    '                    Dim ddl3 As DropDownList = CType(gvTransDt.Rows(rowIndex).Cells(3).FindControl("ddlPTj"), DropDownList)
    '                    Dim ddl4 As DropDownList = CType(gvTransDt.Rows(rowIndex).Cells(4).FindControl("ddlKP"), DropDownList)
    '                    Dim ddl5 As DropDownList = CType(gvTransDt.Rows(rowIndex).Cells(5).FindControl("ddlVot"), DropDownList)
    '                    Dim box1 As TextBox = CType(gvTransDt.Rows(rowIndex).Cells(6).FindControl("txtPerkara"), TextBox)
    '                    Dim box2 As TextBox = CType(gvTransDt.Rows(rowIndex).Cells(7).FindControl("txtKuantiti"), TextBox)
    '                    Dim box3 As TextBox = CType(gvTransDt.Rows(rowIndex).Cells(8).FindControl("txtHarga"), TextBox)
    '                    Dim box4 As Label = CType(gvTransDt.Rows(rowIndex).Cells(9).FindControl("lblJumlah"), Label)


    '                    drCurrentRow = dtvsDtInv.NewRow()
    '                    dtvsDtInv.Rows(i - 1)("KodKw") = ddl1.Text
    '                    dtvsDtInv.Rows(i - 1)("KodKO") = ddl2.Text
    '                    dtvsDtInv.Rows(i - 1)("KodPTJ") = ddl3.Text
    '                    dtvsDtInv.Rows(i - 1)("KodKP") = ddl4.Text
    '                    dtvsDtInv.Rows(i - 1)("KodVot") = ddl5.Text
    '                    dtvsDtInv.Rows(i - 1)("AR01_Perkara") = box1.Text
    '                    dtvsDtInv.Rows(i - 1)("AR01_Kuantiti") = box2.Text
    '                    dtvsDtInv.Rows(i - 1)("AR01_kadarHarga") = box3.Text
    '                    dtvsDtInv.Rows(i - 1)("AR01_Jumlah") = box4.Text

    '                    rowIndex += 1
    '                Next

    '                dtvsDtInv.Rows.Add(drCurrentRow)
    '                ViewState("dtInvDt") = dtvsDtInv
    '                gvTransDt.DataSource = dtvsDtInv
    '                gvTransDt.DataBind()
    '            End If
    '        Else
    '            Response.Write("ViewState is null")
    '        End If

    '        SetPreviousData()
    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Private Sub SetPreviousData()
    '    Try
    '        Dim rowIndex As Integer = 0

    '        If ViewState("dtInvDt") IsNot Nothing Then
    '            Dim dt As DataTable = CType(ViewState("dtInvDt"), DataTable)

    '            If dt.Rows.Count > 0 Then

    '                For i As Integer = 0 To dt.Rows.Count - 1
    '                    Dim gvRow As GridViewRow = gvTransDt.Rows(i)
    '                    Dim ddl1 As DropDownList = CType(gvRow.FindControl("ddlKW"), DropDownList)
    '                    Dim ddl2 As DropDownList = CType(gvRow.FindControl("ddlKO"), DropDownList)
    '                    Dim ddl3 As DropDownList = CType(gvRow.FindControl("ddlPTj"), DropDownList)
    '                    Dim ddl4 As DropDownList = CType(gvRow.FindControl("ddlKP"), DropDownList)
    '                    Dim ddl5 As DropDownList = CType(gvRow.FindControl("ddlVot"), DropDownList)
    '                    Dim box1 As TextBox = CType(gvRow.FindControl("txtPerkara"), TextBox)
    '                    Dim box2 As TextBox = CType(gvRow.FindControl("txtKuantiti"), TextBox)
    '                    Dim box3 As TextBox = CType(gvRow.FindControl("txtHarga"), TextBox)
    '                    Dim box4 As Label = CType(gvRow.FindControl("lblJumlah"), Label)

    '                    ddl1.SelectedValue = dt.Rows(i)("KodKw").ToString()
    '                    ddl2.SelectedValue = dt.Rows(i)("KodKO").ToString()
    '                    ddl3.SelectedValue = dt.Rows(i)("KodPTJ").ToString()
    '                    ddl4.SelectedValue = dt.Rows(i)("KodKP").ToString()
    '                    ddl5.SelectedValue = dt.Rows(i)("KodVot").ToString()

    '                    box1.Text = dt.Rows(i)("AR01_Perkara").ToString()
    '                    box2.Text = dt.Rows(i)("AR01_Kuantiti").ToString()
    '                    box3.Text = dt.Rows(i)("AR01_kadarHarga").ToString()
    '                    box4.Text = dt.Rows(i)("AR01_Jumlah").ToString()

    '                    rowIndex += 1

    '                Next
    '            End If
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub




    'Protected Sub txtKuantiti_TextChanged(sender As Object, e As EventArgs)

    '    Dim intKtt As Integer
    '    Try
    '        Dim txtKuantiti As TextBox = CType(CType(sender, Control), TextBox)


    '        Dim gvRow As GridViewRow = CType(CType(sender, Control), TextBox).Parent.Parent
    '        Dim txtHarga As TextBox = CType(gvRow.FindControl("txtHarga"), TextBox)
    '        Dim lblJumlah As Label = CType(gvRow.FindControl("lblJumlah"), Label)

    '        Dim strKtt As String = txtKuantiti.Text
    '        If strKtt = "" Then intKtt = 0 Else intKtt = CInt(strKtt)

    '        If txtHarga.Text = "" Then txtHarga.Text = 0
    '        Dim angHrgSeunit = CDec(txtHarga.Text)
    '        Dim JumAngHrg = CDec(intKtt * angHrgSeunit)
    '        lblJumlah.Text = FormatNumber(JumAngHrg)

    '        fSetFooter()
    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Protected Sub txtHarga_TextChanged(sender As Object, e As EventArgs)

    '    Dim decHarga, decJumlah As Decimal
    '    Dim intKtt As Integer

    '    Try
    '        Dim txtHarga As TextBox = CType(CType(sender, Control), TextBox)

    '        Dim gvRow As GridViewRow = CType(CType(sender, Control), TextBox).Parent.Parent
    '        Dim txtKuantiti As TextBox = CType(gvRow.FindControl("txtKuantiti"), TextBox)
    '        Dim lblJumlah As Label = CType(gvRow.FindControl("lblJumlah"), Label)

    '        Dim strHarga = txtHarga.Text
    '        If strHarga = "" Then decHarga = 0 Else decHarga = CDec(strHarga)

    '        Dim strKtt As String = txtKuantiti.Text
    '        If strKtt = "" Then intKtt = 0 Else intKtt = CInt(strKtt)

    '        decJumlah = CDec(intKtt * decHarga)
    '        lblJumlah.Text = FormatNumber(decJumlah)
    '        txtHarga.Text = FormatNumber(decHarga)

    '        fSetFooter()
    '    Catch ex As Exception

    '    End Try

    'End Sub
    Private Sub fSetFooter()
        Try
            Dim decJumTrans As Decimal
            For Each gvRow1 As GridViewRow In gvTransDt.Rows
                Dim decJumlah As String = CDec(Trim(CType(gvRow1.FindControl("lblJumlah"), Label).Text.TrimEnd))
                decJumTrans += decJumlah
            Next

            Dim footerRow = gvTransDt.FooterRow
            CType(footerRow.FindControl("lblTotJum"), Label).Text = FormatNumber(decJumTrans, 2)
        Catch ex As Exception

        End Try
    End Sub


    'Protected Sub txtJumlah_TextChanged(sender As Object, e As EventArgs)
    '    Try
    '        Dim decJumTrans As Decimal
    '        For Each gvRow As GridViewRow In gvTransDt.Rows
    '            Dim decJumlah As String = CDec(Trim(CType(gvRow.FindControl("txtJumlah"), TextBox).Text.TrimEnd))
    '            decJumTrans += decJumlah
    '        Next

    '        Dim footerRow = gvTransDt.FooterRow
    '        CType(footerRow.FindControl("lblTotJum"), Label).Text = FormatNumber(decJumTrans, 2)

    '    Catch ex As Exception

    '    End Try
    'End Sub


    'Function fGetID_Bil()
    '    Try
    '        Dim strSql As String
    '        Dim strIdx As String
    '        Dim intLastIdx As Integer
    '        Dim strCol As String
    '        Dim strTahun As String = Now.Year
    '        Dim strKodModul As String
    '        Dim strPrefix As String
    '        Dim strButiran As String

    '        If rbKatPel.SelectedValue = 0 Then
    '            strSql = "select * from mk_noakhir where tahun = '" & strTahun & "' and kodmodul='AR' and prefix='SU'"
    '            strCol = "NoAkhir"
    '            strKodModul = "AR"
    '            strPrefix = "SU"
    '            strButiran = "Max No Sem Pelajar Undergrade AR"

    '            Dim ds As New DataSet
    '            Dim dbconn As New DBKewConn
    '            ds = dbconn.fSelectCommand(strSql)

    '            If Not ds Is Nothing Then
    '                If ds.Tables(0).Rows.Count > 0 Then
    '                    intLastIdx = CInt(ds.Tables(0).Rows(0)(strCol).ToString)
    '                Else
    '                    intLastIdx = 0

    '                End If

    '                If intLastIdx = 0 Then
    '                    strIdx = strPrefix & "000001" & Now.ToString("MM") & Now.ToString("yy")

    '                    strSql = " insert into mk_noakhir (KodModul , Prefix, NoAkhir, Tahun, Butiran, KodPTJ)" &
    '                    "values(@kodmodul,@prefix,@noakhir,@tahun,@butiran,@kodPTJ)"

    '                    Dim paramSql() As SqlParameter = {
    '                        New SqlParameter("@kodmodul", strKodModul),
    '                        New SqlParameter("@prefix", strPrefix),
    '                        New SqlParameter("@noakhir", 1),
    '                        New SqlParameter("@tahun", strTahun),
    '                        New SqlParameter("@butiran", strButiran),
    '                        New SqlParameter("@kodPTJ", "-")
    '                    }

    '                    dbconn = New DBKewConn
    '                    dbconn.sConnBeginTrans()
    '                    If dbconn.fInsertCommand(strSql, paramSql) > 0 Then
    '                        dbconn.sConnCommitTrans()
    '                    Else
    '                        dbconn.sConnRollbackTrans()
    '                    End If

    '                Else

    '                    intLastIdx = intLastIdx + 1
    '                    strIdx = strPrefix & intLastIdx.ToString("D6") & Now.ToString("MM") & Now.ToString("yy")

    '                    strSql = "update MK_NoAkhir set NoAkhir =@noakhir where tahun =@tahun and KodModul =@kodmodul and prefix = @prefix"

    '                    Dim paramSql2() As SqlParameter = {
    '                                New SqlParameter("@noakhir", intLastIdx),
    '                                New SqlParameter("@tahun", strTahun),
    '                                New SqlParameter("@kodmodul", strKodModul),
    '                                New SqlParameter("@prefix", strPrefix)
    '                                }

    '                    dbconn.sConnBeginTrans()
    '                    If dbconn.fUpdateCommand(strSql, paramSql2) > 0 Then
    '                        dbconn.sConnCommitTrans()
    '                    Else
    '                        dbconn.sConnRollbackTrans()
    '                    End If
    '                    Return strIdx

    '                End If
    '            End If
    '        ElseIf rbKatPel.SelectedValue = 1 Then
    '            strSql = "select * from mk_noakhir where tahun = '" & strTahun & "' and kodmodul='AR' and prefix='SP'"
    '            strCol = "NoAkhir"
    '            strKodModul = "AR"
    '            strPrefix = "SP"
    '            strButiran = "Max No Sem Pelajar Postgrade AR"

    '            Dim ds As New DataSet
    '            Dim dbconn As New DBKewConn
    '            ds = dbconn.fSelectCommand(strSql)

    '            If Not ds Is Nothing Then
    '                If ds.Tables(0).Rows.Count > 0 Then
    '                    intLastIdx = CInt(ds.Tables(0).Rows(0)(strCol).ToString)
    '                Else
    '                    intLastIdx = 0

    '                End If

    '                If intLastIdx = 0 Then
    '                    strIdx = strPrefix & "000001" & Now.ToString("MM") & Now.ToString("yy")

    '                    strSql = " insert into mk_noakhir (KodModul , Prefix, NoAkhir, Tahun, Butiran, KodPTJ)" &
    '                    "values(@kodmodul,@prefix,@noakhir,@tahun,@butiran,@kodPTJ)"

    '                    Dim paramSql() As SqlParameter = {
    '                        New SqlParameter("@kodmodul", strKodModul),
    '                        New SqlParameter("@prefix", strPrefix),
    '                        New SqlParameter("@noakhir", 1),
    '                        New SqlParameter("@tahun", strTahun),
    '                        New SqlParameter("@butiran", strButiran),
    '                        New SqlParameter("@kodPTJ", "-")
    '                    }

    '                    dbconn = New DBKewConn
    '                    dbconn.sConnBeginTrans()
    '                    If dbconn.fInsertCommand(strSql, paramSql) > 0 Then
    '                        dbconn.sConnCommitTrans()
    '                    Else
    '                        dbconn.sConnRollbackTrans()
    '                    End If

    '                Else

    '                    intLastIdx = intLastIdx + 1
    '                    strIdx = strPrefix & intLastIdx.ToString("D6") & Now.ToString("MM") & Now.ToString("yy")

    '                    strSql = "update MK_NoAkhir set NoAkhir =@noakhir where tahun =@tahun and KodModul =@kodmodul and prefix = @prefix"

    '                    Dim paramSql2() As SqlParameter = {
    '                                New SqlParameter("@noakhir", intLastIdx),
    '                                New SqlParameter("@tahun", strTahun),
    '                                New SqlParameter("@kodmodul", strKodModul),
    '                                New SqlParameter("@prefix", strPrefix)
    '                                }

    '                    dbconn.sConnBeginTrans()
    '                    If dbconn.fUpdateCommand(strSql, paramSql2) > 0 Then
    '                        dbconn.sConnCommitTrans()
    '                    Else
    '                        dbconn.sConnRollbackTrans()
    '                    End If

    '                End If
    '                Return strIdx
    '            End If
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Function

    Private Sub rbKatPel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rbKatPel.SelectedIndexChanged
        sClearDdlPenaja()
        Dim strKat = Trim(rbKatPel.SelectedValue.TrimEnd)

        If strKat = "UG" Then
            ddlStatPel.Items.Clear()
            ddlStatPel.Items.Insert(0, New ListItem("PERDANA", "PERDANA"))
            ddlStatPel.SelectedIndex = 0

        ElseIf strKat = "PG" Then
            ddlStatPel.Items.Clear()
            ddlStatPel.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlStatPel.Items.Insert(1, New ListItem("PERDANA", "PERDANA"))
            ddlStatPel.Items.Insert(2, New ListItem("PESISIR", "PESISIR"))
            ddlStatPel.SelectedIndex = 0

        ElseIf strKat = "PSH" Then
            ddlStatPel.Items.Clear()
            ddlStatPel.Items.Insert(0, New ListItem("SEPARA MASA", "SEPARA MASA"))
            ddlStatPel.SelectedIndex = 0

        End If

        sLoadPenaja(strKat)
        sClearDdlSesi()
        sClearInfoPenaja()
        sClearGvLamp()
        sCleargvInvDt()

        alert.Visible = False

    End Sub

    Private Sub sLoadPenaja(strKat)
        Try

            Dim ds As DataSet

            'If strKat = "PSH" Then strKat = "UG"

            ds = fLoadPenghutang(strKat)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    ddlPenaja.DataSource = ds.Tables(0)
                    ddlPenaja.DataTextField = "Penghutang"
                    ddlPenaja.DataValueField = "IdPenghutang"
                    ddlPenaja.DataBind()

                    ddlPenaja.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
                    ddlPenaja.SelectedIndex = 0

                    Dim dt As DataTable = ds.Tables(0)
                    ViewState("dtPenghutang") = dt
                Else
                    fGlobalAlert("Tiada rekod penghutang untuk kategori pelajar ini! Sila daftar penghutang di skrin 'Daftar Penghutang'", Me.Page, Me.[GetType]())

                    ddlPenaja.Items.Clear()
                    ddlPenaja.Items.Insert(0, New ListItem("- TIADA REKOD PENAJA -", 0))
                    ddlPenaja.SelectedIndex = 0
                End If
            Else
                fGlobalAlert("Tiada rekod penghutang untuk kategori pelajar ini! Sila daftar penghutang di skrin 'Daftar Penghutang'", Me.Page, Me.[GetType]())

                ddlPenaja.Items.Clear()
                ddlPenaja.Items.Insert(0, New ListItem("- TIADA REKOD PENAJA -", 0))
                ddlPenaja.SelectedIndex = 0
            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Sub fLoadPenPraSis() 'PAPAR KOD Prasiswazah
        Try
            Dim dbSMPconn As New DBSMPConn

            ddlPenaja.Items.Clear()

            Dim str = $"select SMP_BSWKOD, (SMP_BSWKOD + '-' + SMP_BSWNAMA) as SMP_BSWNAMA, SMP_BSWJENIS , SMP_BSWTEL , SMP_BSWFAX , SMP_BSWKPDUNI , SMP_BSWPEGAWAI , SMP_BSWEMAIL , SMP_BSWALAMAT,SMP_BSWPOSKOD , SMP_BSWNEGERI, (select Negeri from SMP_NegeriPL where SMP_NegeriPL.Kod_Negeri = SMP_APBiasiswa.SMP_BSWNEGERI) as ButNegeri from SMP_APBiasiswa order by smp_bswkod;"


            Dim dt = dbSMPconn.fselectCommandDt(str)
            ddlPenaja.DataSource = dt
            ddlPenaja.DataTextField = "SMP_BSWNAMA"
            ddlPenaja.DataValueField = "SMP_BSWKOD"
            ddlPenaja.DataBind()

            ddlPenaja.Items.Insert(0, New ListItem(" - SILA PILIH -", 0))
            ddlPenaja.SelectedIndex = 0

            ViewState("dtPenPraSis") = dt

        Catch ex As Exception

        End Try

    End Sub

    Private Sub sClearDdlStatPelajar()
        Try
            ddlStatPel.Items.Clear()
            ddlStatPel.Items.Insert(0, New ListItem(" - SILA PILIH KATEGORI PELAJAR-", 0))
            ddlStatPel.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub

    Private Sub sClearDdlPenaja()
        Try
            ddlPenaja.Items.Clear()
            ddlPenaja.Items.Insert(0, New ListItem(" - SILA PILIH KATEGORI PELAJAR-", 0))
            ddlPenaja.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub

    Private Sub sClearDdlSesi()
        Try
            ddlSesi.Items.Clear()
            ddlSesi.Items.Insert(0, New ListItem(" - SILA PILIH PENAJA-", 0))
            ddlSesi.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub

    Private Sub fLoadPenPasSis() 'PAPAR KOD Pascasiswazah
        Try
            Dim dbSMPconn As New DBSMPConn

            ddlPenaja.Items.Clear()

            Dim str = $"select SMG_BSWKOD, (SMG_BSWKOD + '-' + SMG_BSWNAMA) as SMG_BSWNAMA, SMG_BSWJENIS ,SMG_BSWTEL , SMG_BSWFAX , SMG_BSWKPDUNI,SMG_BSWPEGAWAI , SMG_BSWEMAIL , SMG_BSWALAMAT1 , SMG_BSWALAMAT2,SMG_BSWPOSKOD , SMG_BSWBANDAR, SMG_BSWNEGERI,
                        SMG_BSWNEGARA , SMG_KODMYMOHES from SMG_APBIASISWA order by SMG_BSWKOD;"

            Dim dt = dbSMPconn.fselectCommandDt(str)
            ddlPenaja.DataSource = dt
            ddlPenaja.DataTextField = "SMG_BSWNAMA"
            ddlPenaja.DataValueField = "SMG_BSWKOD"
            ddlPenaja.DataBind()

            ddlPenaja.Items.Insert(0, New ListItem(" - SILA PILIH -", 0))
            ddlPenaja.SelectedIndex = 0

            ViewState("dtPenPasSis") = dt

        Catch ex As Exception

        End Try

    End Sub

    Private Function fLoadDtPenPraSis(strIdPenaja)
        Dim dbSMPconn As New DBSMPConn

        Try
            'Dim str = $"Select SMP_BSWKOD , SMP_BSWNAMA , SMP_BSWJENIS , SMP_BSWTEL , SMP_BSWFAX , SMP_BSWKPDUNI , SMP_BSWPEGAWAI , SMP_BSWEMAIL , SMP_BSWALAMAT,SMP_BSWPOSKOD , SMP_BSWNEGERI  from SMP_APBiasiswa where smp_bswkod = '{strIdPenaja}';"

            'Dim ds As New DataSet
            'ds = dbSMPconn.fselectCommand(str)

            'If ds.Tables(0).Rows.Count > 0 Then
            '    'txtIDPenerima.Text = ds.Tables(0).Rows(0)("SMP_BSWKOD").ToString
            '    txtAlamat1.Text = ds.Tables(0).Rows(0)("SMP_BSWALAMAT").ToString
            '    txtAlamat2.Text = "-"
            '    ' txtBandar.Text = ds.Tables(0).Rows(0)("ROC01_BandarP").ToString
            '    txtPoskod.Text = ds.Tables(0).Rows(0)("SMP_BSWPOSKOD").ToString
            '    ddlNegara.SelectedValue = "121"
            '    fBindDdlNegeri()
            '    ddlNegeri.SelectedValue = ds.Tables(0).Rows(0)("SMP_BSWNEGERI").ToString
            '    txtNoTel.Text = ds.Tables(0).Rows(0)("SMP_BSWTEL").ToString
            '    txtNoFax.Text = ds.Tables(0).Rows(0)("SMP_BSWFAX").ToString
            '    txtEmel.Text = ds.Tables(0).Rows(0)("SMP_BSWEMAIL").ToString
            '    txtPerhatian.Text = ds.Tables(0).Rows(0)("SMP_BSWPEGAWAI").ToString
            'End If

            sClearInfoPenaja()

            Dim dtPenPraSis As DataTable = ViewState("dtPenPraSis")
            Dim dvPenPraSis As New DataView(dtPenPraSis)

            dvPenPraSis.RowFilter = "SMP_BSWKOD = '" & strIdPenaja & "'"

            If Not dvPenPraSis Is Nothing Then
                If dvPenPraSis.Count > 0 Then

                    Dim strKodNegeri = dvPenPraSis.Item(0)("SMP_BSWNEGERI").ToString

                    txtAlamat1.Text = dvPenPraSis.Item(0)("SMP_BSWALAMAT").ToString
                    txtAlamat2.Text = "-"
                    txtPoskod.Text = dvPenPraSis.Item(0)("SMP_BSWPOSKOD").ToString
                    txtNoTel.Text = dvPenPraSis.Item(0)("SMP_BSWTEL").ToString
                    txtNoFax.Text = dvPenPraSis.Item(0)("SMP_BSWFAX").ToString
                    txtEmel.Text = dvPenPraSis.Item(0)("SMP_BSWEMAIL").ToString
                    txtPerhatian.Text = dvPenPraSis.Item(0)("SMP_BSWPEGAWAI").ToString

                    txtKodNegeri.Text = IIf(strKodNegeri = "", "-", strKodNegeri)
                    'txtNegeri.Text =
                    txtKodNegara.Text = "121"
                    'txtNegara.Text = 

                End If
            End If

        Catch ex As Exception

        End Try
    End Function
    Private Function fLoadDtPenPasSis(strIdPenaja)
        Dim dbSMPconn As New DBSMPConn

        Try
            'Dim str = $"Select SMG_BSWKOD , SMG_BSWNAMA , SMG_BSWJENIS ,SMG_BSWTEL , SMG_BSWFAX , SMG_BSWKPDUNI,SMG_BSWPEGAWAI , SMG_BSWEMAIL , SMG_BSWALAMAT1 , SMG_BSWALAMAT2,SMG_BSWPOSKOD , SMG_BSWBANDAR, SMG_BSWNEGERI,
            '            SMG_BSWNEGARA , SMG_KODMYMOHES from SMG_APBiasiswa where smg_bswkod = '{strIdPenaja }';"

            'Dim ds As New DataSet
            'ds = dbSMPconn.fselectCommand(str)

            'If Not ds Is Nothing Then
            '    If ds.Tables(0).Rows.Count > 0 Then
            '        'txtIDPenerima.Text = ds.Tables(0).Rows(0)("SMG_BSWNAMA").ToString
            '        txtAlamat1.Text = ds.Tables(0).Rows(0)("SMG_BSWALAMAT1").ToString
            '        txtAlamat2.Text = ds.Tables(0).Rows(0)("SMG_BSWALAMAT2").ToString
            '        txtBandar.Text = ds.Tables(0).Rows(0)("SMG_BSWBANDAR").ToString
            '        txtPoskod.Text = ds.Tables(0).Rows(0)("SMG_BSWPOSKOD").ToString
            '        ddlNegara.SelectedIndex = ds.Tables(0).Rows(0)("SMG_BSWNEGARA").ToString
            '        fBindDdlNegeri()
            '        ddlNegeri.SelectedValue = ds.Tables(0).Rows(0)("SMG_BSWNEGERI").ToString
            '        txtNoTel.Text = ds.Tables(0).Rows(0)("SMG_BSWTEL").ToString
            '        txtNoFax.Text = ds.Tables(0).Rows(0)("SMG_BSWFAX").ToString
            '        txtEmel.Text = ds.Tables(0).Rows(0)("SMG_BSWEMAIL").ToString

            '    End If
            'End If
            sClearInfoPenaja()

            Dim dtPenPasSis As DataTable = ViewState("dtPenPasSis")
            Dim dvPenPasSis As New DataView(dtPenPasSis)

            dvPenPasSis.RowFilter = "SMG_BSWKOD = '" & strIdPenaja & "'"

            If Not dvPenPasSis Is Nothing Then
                If dvPenPasSis.Count > 0 Then
                    txtAlamat1.Text = dvPenPasSis.Item(0)("SMG_BSWALAMAT1").ToString
                    txtAlamat2.Text = dvPenPasSis.Item(0)("SMG_BSWALAMAT2").ToString
                    txtBandar.Text = dvPenPasSis.Item(0)("SMG_BSWBANDAR").ToString
                    txtPoskod.Text = dvPenPasSis.Item(0)("SMG_BSWPOSKOD").ToString
                    'fBindDdlNegara()
                    'ddlNegara.SelectedIndex = dvPenPasSis.Item(0)("SMG_BSWNEGARA").ToString
                    'fBindDdlNegeri()
                    'ddlNegeri.SelectedValue = dvPenPasSis.Item(0)("SMG_BSWNEGERI").ToString
                    txtNoTel.Text = dvPenPasSis.Item(0)("SMG_BSWTEL").ToString
                    txtNoFax.Text = dvPenPasSis.Item(0)("SMG_BSWFAX").ToString
                    txtEmel.Text = dvPenPasSis.Item(0)("SMG_BSWEMAIL").ToString
                End If
            End If


        Catch ex As Exception

        End Try
    End Function



    Private Function fSimpan() As Boolean
        Dim blnSuccess As Boolean = True
        Dim dbcoon As New DBKewConn
        Dim strNoInvSem As String
        Dim dbconn As New DBKewConn()
        Dim intIdBil As Integer

        Dim intBil As Integer
        Dim blnResult As Boolean = False
        Try
            Dim strNoStaf As String = UserInfo.strSesStaffID ' Session("ssusrID")
            If String.IsNullOrEmpty(strNoStaf) OrElse strNoStaf Is Nothing OrElse strNoStaf = "" Then
                blnSuccess = False
                Exit Try
            End If

            Dim strSql As String
            Dim noInvSmtra As String = ViewState("NoMhn")
            Dim strTkhToday As String = Now.ToString("yyyy-MM-dd")
            Dim strTahun As String = Now.Year
            Dim strStatDok As String = "01"
            Dim strIDPmhn As String = Trim(lblNoPmhn.Text.TrimEnd)
            Dim strPerhatian As String = Trim(txtPerhatian.Text.TrimEnd)
            Dim strNoRujuk As String = Trim(txtNoRujukan.Text.TrimEnd)
            Dim strKodPenghutang As String = Trim(txtKodPenghutang.Text.TrimEnd)
            Dim strIdPenerima = Trim(ddlPenaja.SelectedValue.TrimEnd)
            Dim strNamaPenerima = Trim(ddlPenaja.SelectedItem.Text.TrimEnd.Split("-")(1))
            Dim strAlamat1 As String = Trim(txtAlamat1.Text.TrimEnd)
            Dim strAlamat2 As String = Trim(txtAlamat2.Text.TrimEnd)
            Dim strBdr As String = Trim(txtBandar.Text.TrimEnd)
            Dim strPoskod As String = Trim(txtPoskod.Text.TrimEnd)
            Dim KodNegara As String = Trim(txtKodNegara.Text.TrimEnd)
            Dim KodNegeri As String = Trim(txtKodNegeri.Text.TrimEnd)
            Dim strEmel As String = Trim(txtEmel.Text.TrimEnd)
            Dim strNoTel As String = Trim(txtNoTel.Text.TrimEnd)
            Dim strNoFax As String = Trim(txtNoFax.Text.TrimEnd)
            Dim strKat As String = Trim(rbKatPel.SelectedValue.TrimEnd)
            Dim strBank As String = Trim(ddlBank.SelectedValue.TrimEnd)
            Dim strTujuan As String = Trim(txtTujuan.Text.TrimEnd)
            Dim strKodPTJ As String = Trim(txtKodPTj.Text.TrimEnd)
            Dim strJenisPljr As String = Trim(rbKatPel.SelectedValue.TrimEnd)
            Dim strStatPel As String = Trim(ddlStatPel.SelectedValue.TrimEnd)
            Dim strKodSesi = Trim(ddlSesi.SelectedValue.TrimEnd)
            Dim strKodModul
            Dim strPrefix
            Dim strButiranNoAkhir

            If rbKatPel.SelectedValue = "UG" Then
                strJenisPljr = "UG"
                strKodModul = "AR"
                strPrefix = "SU"
                strButiranNoAkhir = "Max No Sem Pelajar Undergrade AR"
            ElseIf rbKatPel.SelectedValue = "PG" Then
                strJenisPljr = "PG"
                strKodModul = "AR"
                strPrefix = "SP"
                strButiranNoAkhir = "Max No Sem Pelajar Postgrade AR"
            End If

            Dim decTotJum As Decimal
            For Each gvRow As GridViewRow In gvTransDt.Rows
                Dim decJumTrans As Decimal = CDec(CType(gvRow.FindControl("lblJumlah"), Label).Text)
                decTotJum += decJumTrans
            Next

            dbconn.sConnBeginTrans()

            'JANA NO. BIL SEM
            strNoInvSem = fGetNo(strTahun, strKodModul, strPrefix, strButiranNoAkhir)

            If strNoInvSem Is Nothing Then
                blnSuccess = False
                Exit Try
            End If

            'Insert Into AR01_Bil
            strSql = "insert into AR01_Bil (AR01_NOBILSEM, AR01_NOBIL, AR01_Tahun, AR01_TkhMohon, AR01_JENIS, AR01_KODPTJMOHON,AR01_NOSTAF, AR01_UTKPERHATIAN ,AR01_NoRujukan ,AR01_Jumlah, AR01_STATUSDOK, AR01_STATUSCETAKBILSBNR, AR01_STATUSCETAKBILSMTR, AR01_FLAGADJ, AR01_Kategori, KodPenghutang, AR01_IDPENERIMA, AR01_NAMAPENERIMA,AR01_ALMT1,AR01_ALMT2,AR01_BANDAR,AR01_POSKOD,KODNEGARA,KODNEGERI,AR01_EMEL,AR01_NOTEL,AR01_NOFAKS,AR01_KODBANK,AR01_TUJUAN,AR01_JumBlmByr,AR01_JenisPljr,AR01_StatPel, AR01_KodSesi)" &
                     "values(@NOBILSEM,@NOBIL, @AR01_Tahun, @TkhMhn,@JENIS ,@KODPTJMOHON,@NOSTAF,@UTKPERHATIAN,@NORUJUKAN,@Jumlah,@STATUSDOK,@STATUSCETAKBILSBNR,@STATUSCETAKBILSMTR,@FLAGADJ, @Kategori, @KodPenghutang, @IDPenerima, @NamaPenerima, @ALMT1, @ALMT2,@BANDAR,@POSKOD,@KODNEGARA,@KODNEGERI,@EMEL,@NOTEL,@NOFAKS,@KODBANK,@TUJUAN,@JumBlmByr,@JenisPljr,@AR01_StatPel, @AR01_KodSesi)"

            Dim paramSql() As SqlParameter =
                {
                New SqlParameter("@NOBILSEM", strNoInvSem),
                New SqlParameter("@NOBIL", strNoInvSem),
                New SqlParameter("@AR01_Tahun", strTahun),
                New SqlParameter("@TkhMhn", strTkhToday),
                New SqlParameter("@JENIS", "02"),
                New SqlParameter("@KODPTJMOHON", strKodPTJ),
                New SqlParameter("@NOSTAF", strIDPmhn),
                New SqlParameter("@UTKPERHATIAN", strPerhatian),
                New SqlParameter("@NORUJUKAN", strNoRujuk),
                New SqlParameter("@Jumlah", decTotJum),
                New SqlParameter("@STATUSDOK", strStatDok),
                New SqlParameter("@STATUSCETAKBILSBNR", 0),
                New SqlParameter("@STATUSCETAKBILSMTR", 0),
                New SqlParameter("@FLAGADJ", 0),
                New SqlParameter("@Kategori", "PL"),
                 New SqlParameter("@KodPenghutang", strKodPenghutang),
                New SqlParameter("@IDPENERIMA", strIdPenerima),
                New SqlParameter("@NAMAPENERIMA", strNamaPenerima),
                New SqlParameter("@ALMT1", strAlamat1),
                New SqlParameter("@ALMT2", strAlamat2),
                New SqlParameter("@BANDAR", strBdr),
                New SqlParameter("@POSKOD", strPoskod),
                New SqlParameter("@KODNEGARA", KodNegara),
                New SqlParameter("@KODNEGERI", KodNegeri),
                New SqlParameter("@EMEL", strEmel),
                New SqlParameter("@NOTEL", strNoTel),
                New SqlParameter("@NOFAKS", strNoFax),
                New SqlParameter("@KODBANK", strBank),
                New SqlParameter("@TUJUAN", strTujuan),
                New SqlParameter("@JumBlmByr", decTotJum),
                New SqlParameter("@JenisPljr", strJenisPljr),
                New SqlParameter("@AR01_StatPel", strStatPel),
                New SqlParameter("@AR01_KodSesi", strKodSesi)
                }

            If Not dbconn.fInsertCommand(strSql, paramSql, intIdBil) > 0 Then
                blnSuccess = False
                Exit Try
            Else

                'AUDIT LOG
                sLogMedan = "AR01_NOBILSEM | AR01_NOBIL | AR01_TkhMohon| AR01_JENIS |AR01_KODPTJMOHON |AR01_NOSTAF |AR01_UTKPERHATIAN |AR01_NoRujukan |AR01_Jumlah|AR01_STATUSDOK|AR01_STATUSCETAKBILSBNR| AR01_STATUSCETAKBILSMTR|AR01_FLAGADJ|AR01_Kategori|AR01_IDPENERIMA|AR01_NAMAPENERIMA|AR01_ALMT1|AR01_ALMT2|AR01_BANDAR|AR01_POSKOD|KODNEGARA|KODNEGERI|AR01_EMEL|AR01_NOTEL|AR01_NOFAKS|AR01_KODBANK|AR01_TUJUAN|AR01_JumBlmByr|AR01_JenisPljr"
                sLogBaru = strNoInvSem & "|" & strNoInvSem & "|" & strTkhToday & "|02| " & strKodPTJ & "|" & strIDPmhn & "|" & strPerhatian & "|" & strNoRujuk & "|" & decTotJum & "|" & strStatDok & "|0|0|0|PL|" & strIdPenerima & "|" & strNamaPenerima & "|" & strAlamat1 & "|" & strAlamat2 & "|" & strBdr & "|" & strPoskod & "|" & KodNegara & "|" & KodNegeri & "|" & strEmel & "|" & strNoTel & "|" & strNoFax & "|" & strBank & "|" & strTujuan & "|" & decTotJum & "|" & strJenisPljr & ""


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

            intBil = 0
            Dim strKodKW, strKodKO, strPTJ, strKodKP, strKodVot, strPerkara As String
            Dim decKdrHrg, decJum As Decimal
            Dim intKuantiti As Integer

            For Each gvTransInvrow As GridViewRow In gvTransDt.Rows
                intBil += 1
                strKodKW = TryCast(gvTransInvrow.FindControl("lblKodKW"), Label).Text
                strKodKO = TryCast(gvTransInvrow.FindControl("lblKodKO"), Label).Text
                strPTJ = TryCast(gvTransInvrow.FindControl("lblKodPTj"), Label).Text
                strKodKP = TryCast(gvTransInvrow.FindControl("lblKodKP"), Label).Text
                strKodVot = TryCast(gvTransInvrow.FindControl("lblKodVot"), Label).Text
                strPerkara = TryCast(gvTransInvrow.FindControl("lblPerkara"), Label).Text
                intKuantiti = CInt(TryCast(gvTransInvrow.FindControl("lblKuantiti"), Label).Text)
                decKdrHrg = CDec(TryCast(gvTransInvrow.FindControl("lblHarga"), Label).Text)
                decJum = CDec(TryCast(gvTransInvrow.FindControl("lblJumlah"), Label).Text)

                strSql = " INSERT INTO AR01_BilDT(AR01_IdBil, AR01_NoBilSem, AR01_NoBil, AR01_Bil, KodKw, KodKO,KodPTJ, KodKP, KodVot, AR01_Perkara, AR01_Kuantiti, AR01_kadarHarga, AR01_Jumlah)" &
                             "values (@IdBil, @NOBILSEM, @NOBIL, @BIL, @kodKw, @KodKO, @KodPTJ, @KodKP, @KodVot, @Perkara, @Kuantiti,@kadarHarga,@Jumlah)"

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
                     New SqlParameter("@Kuantiti", intKuantiti),
                     New SqlParameter("@kadarHarga", decKdrHrg),
                     New SqlParameter("@Jumlah", decJum)
                    }

                If Not dbconn.fInsertCommand(strSql, paramSql2) > 0 Then
                    blnSuccess = False
                    Exit Try
                Else
                    'AUDIT LOG
                    sLogMedan = "AR01_IdBil| AR01_NoBilSem| AR01_NoBil| AR01_Bil| KodKw| KodKO|KodPTJ| KodKP| KodVot| AR01_Perkara| AR01_Kuantiti| AR01_kadarHarga| AR01_Jumlah"
                    sLogBaru = intIdBil & "|" & strNoInvSem & "|" & strNoInvSem & "|" & intBil & "|" & strKodKW & "|" & strKodKO & "|" & strPTJ & "|" & strKodKP & "|" & strKodVot & "|" & strPerkara & "|" & intKuantiti & "|" & decKdrHrg & "|" & decJum & ""


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

            intBil = 0
            For Each gvLampRow As GridViewRow In gvLamp.Rows
                Dim chkSel As CheckBox = DirectCast(gvLampRow.FindControl("cbSelect"), CheckBox)
                If chkSel.Checked = True Then
                    intBil += 1

                    Dim strNamaPen = TryCast(gvLampRow.FindControl("lblNmPen"), Label).Text
                    Dim strNoKP = TryCast(gvLampRow.FindControl("lblNoKP"), Label).Text
                    Dim strNoMatrik = TryCast(gvLampRow.FindControl("lblNoMatrik"), Label).Text
                    Dim strKursus = TryCast(gvLampRow.FindControl("lblKursus"), Label).Text
                    Dim strButiran = TryCast(gvLampRow.FindControl("lblButiran"), Label).Text
                    Dim decAmaun As Decimal = CDec(TryCast(gvLampRow.FindControl("lblJumDC"), Label).Text)

                    strSql = "INSERT INTO AR02_LAMPIRAN(AR01_IdBil, AR02_NoBilSem, AR02_NoBil, AR02_Bil, AR02_NAMAPENERIMA, AR02_NOKP, AR02_NOMATRIK, AR02_KURSUS,AR02_Butiran, AR02_AMAUN)
VALUES(@IdBil, @NoBilSem, @NoBil, @Bil, @NamaPen, @NoKP, @NoMatrik, @Kursus, @AR02_Butiran, @Amaun)"
                    Dim paramSql2() As SqlParameter =
                        {
                         New SqlParameter("@IdBil", intIdBil),
                         New SqlParameter("@NoBilSem", strNoInvSem),
                         New SqlParameter("@NoBil", strNoInvSem),
                         New SqlParameter("@Bil", intBil),
                         New SqlParameter("@NamaPen", strNamaPen),
                         New SqlParameter("@NoKP", strNoKP),
                         New SqlParameter("@NoMatrik", strNoMatrik),
                         New SqlParameter("@Kursus", strKursus),
                         New SqlParameter("@AR02_Butiran", strButiran),
                         New SqlParameter("@Amaun", decAmaun)
                        }

                    If Not dbconn.fInsertCommand(strSql, paramSql2) > 0 Then
                        blnSuccess = False
                        Exit Try
                    Else
                        'AUDIT LOG
                        sLogMedan = "AR01_IdBil|AR02_NoBilSem|AR02_NoBil|AR02_Bil|AR02_NAMAPENERIMA|AR02_NOKP|AR02_NOMATRIK|AR02_KURSUS|AR02_AMAUN"
                        sLogBaru = intIdBil & "|" & strNoInvSem & "|" & strNoInvSem & "|" & intBil & "|" & strNamaPen & "|" & strNoKP & "|" & strNoMatrik & "|" & strKursus & "|" & decAmaun & ""


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
                            New SqlParameter("@InfoTable", "AR02_LAMPIRAN"),
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
            Next

            'Insert Into AR06_STATUSDOK
            strSql = "INSERT INTO AR06_STATUSDOK(AR06_NOBILSEM,AR06_NoBil, AR06_StatusDok,AR06_Tarikh,AR06_Ulasan,AR06_NoStaf)" &
                             "VALUES (@NOBILSEM,@NOBIL, @STATUSDOK,@TARIKH,@ULASAN,@NOSTAF)"

            Dim paramSql3() As SqlParameter =
                {
                New SqlParameter("@NOBILSEM", strNoInvSem),
                 New SqlParameter("@NOBIL", strNoInvSem),
                 New SqlParameter("@STATUSDOK", "01"),
                 New SqlParameter("@TARIKH", strTkhToday),
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
            txtNoInvSem.Text = strNoInvSem
            Return True
        ElseIf blnSuccess = False Then
            dbconn.sConnRollbackTrans()
            Return False
        End If

    End Function

    '    Private Function fKemasKini(ByVal strNoInvSem As String) As Boolean
    '        Dim strSql As String
    '        Dim dbconn As New DBKewConn
    '        Dim blnSuccess As Boolean = True

    '        Try
    '            Dim intIdBil As Integer = CInt(hidIdBil.Value)
    '            Dim strNoBilSem As String = Trim(txtNoInvSem.Text.TrimEnd)
    '            Dim dtTkhMhnInv As DateTime = CDate(Trim(txtTkhMohon.Text.TrimEnd))
    '            Dim strTkhMhnInv As String = dtTkhMhnInv.ToString("yyyy-MM-dd")
    '            Dim strIDPmhn As String = Trim(lblNoPmhn.Text.TrimEnd)
    '            Dim strPerhatian As String = Trim(txtPerhatian.Text.TrimEnd)
    '            Dim strNoRujuk As String = Trim(txtNoRujukan.Text.TrimEnd)
    '            Dim IDPenerima As String = Trim(ddlPenaja.SelectedValue.TrimEnd)
    '            Dim NamaPenerima As String = Trim(ddlPenaja.SelectedItem.Text.TrimEnd)
    '            Dim strAlamat1 As String = Trim(txtAlamat1.Text.TrimEnd)
    '            Dim strAlamat2 As String = Trim(txtAlamat2.Text.TrimEnd)
    '            Dim strBdr As String = Trim(txtBandar.Text.TrimEnd)
    '            Dim strPoskod As String = Trim(txtPoskod.Text.TrimEnd)
    '            Dim KodNegara As String = Trim(txtKodNegara.Text.TrimEnd)
    '            Dim KodNegeri As String = Trim(txtKodNegeri.Text.TrimEnd)
    '            Dim strEmel As String = Trim(txtEmel.Text.TrimEnd)
    '            Dim strNoTel As String = Trim(txtNoTel.Text.TrimEnd)
    '            Dim strNoFax As String = Trim(txtNoFax.Text.TrimEnd)
    '            Dim strBank As String = Trim(ddlBank.SelectedValue.TrimEnd)
    '            Dim strTujuan As String = Trim(txtTujuan.Text.TrimEnd)
    '            Dim strKodPTJ As String = Trim(txtKodPTj.Text.TrimEnd)
    '            Dim strKodSesi = Trim(ddlSesi.SelectedValue.TrimEnd)
    '            Dim decJumDt, decTotJum As Decimal
    '            For Each gvrow As GridViewRow In gvTransDt.Rows
    '                decJumDt = CDec(TryCast(gvrow.FindControl("lblJumlah"), Label).Text)
    '                decTotJum += decJumDt
    '            Next

    '            'Update AR01_Bil
    '            strSql = "update AR01_Bil Set AR01_UtkPerhatian = @AR01_UtkPerhatian, AR01_NoRujukan = @AR01_NoRujukan, AR01_Jumlah = @AR01_Jumlah, 
    '				 AR01_Almt1 = @AR01_Almt1, AR01_Almt2 = @AR01_Almt2, AR01_Bandar = @AR01_Bandar, AR01_Poskod = @AR01_Poskod, KodNegara =@KodNegara, KodNegeri =@KodNegeri, AR01_Emel = @AR01_Emel, AR01_NoTel = @AR01_NoTel, AR01_NoFaks = @AR01_NoFaks, AR01_KodBank = @AR01_KodBank, AR01_Tujuan = @AR01_Tujuan where AR01_NoBilSem = @AR01_NoBilSem"

    '            Dim paramSql() As SqlParameter =
    '                {
    '                New SqlParameter("@AR01_UtkPerhatian", strPerhatian),
    '                New SqlParameter("@AR01_NoRujukan", strNoRujuk),
    '                New SqlParameter("@AR01_Jumlah", decTotJum),
    '                New SqlParameter("@AR01_Almt1", strAlamat1),
    '                New SqlParameter("@AR01_Almt2", strAlamat2),
    '                New SqlParameter("@AR01_Bandar", strBdr),
    '                New SqlParameter("@AR01_Poskod", strPoskod),
    '                New SqlParameter("@KodNegara", KodNegara),
    '                New SqlParameter("@KodNegeri", KodNegeri),
    '                New SqlParameter("@AR01_Emel", strEmel),
    '                New SqlParameter("@AR01_NoTel", strNoTel),
    '                New SqlParameter("@AR01_NoFaks", strNoFax),
    '                New SqlParameter("@AR01_KodBank", strBank),
    '                New SqlParameter("@AR01_Tujuan", strTujuan),
    '                New SqlParameter("@AR01_NoBilSem", strNoInvSem)
    '                }

    '            dbconn.sConnBeginTrans()
    '            If Not dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
    '                blnSuccess = False
    '                Exit Try
    '            Else
    '                'AUDIT LOG
    '                sLogMedan = "AR01_UtkPerhatian| AR01_NoRujukan| AR01_Jumlah| AR01_Almt1| AR01_Almt2| AR01_Bandar| AR01_Poskod| KodNegara | KodNegeri | AR01_Emel | AR01_NoTel | AR01_NoFaks | AR01_KodBank | AR01_Tujuan | AR01_NoBilSem"
    '                sLogBaru = strPerhatian & "|" & strNoRujuk & "|" & decTotJum & "|" & strAlamat1 & "|" & strAlamat2 & "|" & strBdr & "|" & strPoskod & "|" & KodNegara & "|" & KodNegeri & "|" & strEmel & "|" & strNoTel & "|" & strNoFax & "|" & strBank & "|" & strTujuan & "|" & strNoInvSem & ""

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

    '                If Not dbconn.fInsertCommand(strSql, paramSqlLog) > 0 Then
    '                    blnSuccess = False
    '                    Exit Try
    '                End If
    '            End If

    '            strSql = "delete from AR01_BilDT where AR01_IdBil = " & intIdBil & " and AR01_NoBilSem = '" & strNoInvSem & "'"
    '            If Not dbconn.fUpdateCommand(strSql) > 0 Then
    '                blnSuccess = False
    '                Exit Try
    '            Else
    '                'AUDIT LOG
    '                sLogMedan = "AR01_IdBil| AR01_NoBilSem"
    '                sLogBaru = intIdBil & "|" & strNoInvSem & ""

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

    '                If Not dbconn.fInsertCommand(strSql, paramSqlLog) > 0 Then
    '                    blnSuccess = False
    '                    Exit Try
    '                End If
    '            End If

    '            'MASUK REKOD TRANSAKSI KE DALAM TABLE AR01_BilDT
    '            Dim intBil As Integer
    '            For Each gvTransInvrow As GridViewRow In gvTransDt.Rows
    '                Dim intDtId As Integer

    '                intBil = intBil + 1
    '                Dim strDtId As String = TryCast(gvTransInvrow.FindControl("lblDtId"), Label).Text
    '                If strDtId = "" Then intDtId = 0 Else intDtId = CInt(strDtId)
    '                Dim strKodKW As String = TryCast(gvTransInvrow.FindControl("ddlKW"), DropDownList).SelectedValue
    '                Dim strKodKO As String = TryCast(gvTransInvrow.FindControl("ddlKO"), DropDownList).SelectedValue
    '                Dim strPTJ As String = TryCast(gvTransInvrow.FindControl("ddlPTj"), DropDownList).SelectedValue
    '                Dim strKodKP As String = TryCast(gvTransInvrow.FindControl("ddlKP"), DropDownList).SelectedValue
    '                Dim strKodVot As String = TryCast(gvTransInvrow.FindControl("ddlVot"), DropDownList).SelectedValue
    '                Dim strPerkara As String = TryCast(gvTransInvrow.FindControl("txtPerkara"), TextBox).Text
    '                Dim decKuantiti As Decimal = CDec(TryCast(gvTransInvrow.FindControl("txtKuantiti"), TextBox).Text)
    '                Dim decKdrHrg As Decimal = CDec(TryCast(gvTransInvrow.FindControl("txtHarga"), TextBox).Text)
    '                Dim decJum As Decimal = CDec(TryCast(gvTransInvrow.FindControl("lblJumlah"), Label).Text)

    '                'masuk rekod baru
    '                strSql = " INSERT INTO AR01_BilDT(AR01_IdBil, AR01_NoBilSem,AR01_NoBil,AR01_Bil,KodKw,KodKO,KodPTJ,KodKP,KodVot,AR01_Perkara,AR01_Kuantiti,AR01_kadarHarga,AR01_Jumlah)" &
    '                             "values (@IdBil, @NOBILSEM, @NOBILSEM, @Bil,@kodKw, @KodKO, @KodPTJ, @KodKP, @KodVot, @Perkara, @Kuantiti,@kadarHarga,@Jumlah)"

    '                Dim paramSql2() As SqlParameter =
    '                        {
    '                        New SqlParameter("@IdBil", intIdBil),
    '                         New SqlParameter("@NOBILSEM", strNoInvSem),
    '                         New SqlParameter("@Bil", intBil),
    '                         New SqlParameter("@kodKw", strKodKW),
    '                         New SqlParameter("@KodKO", strKodKO),
    '                         New SqlParameter("@KodPTJ", strPTJ),
    '                         New SqlParameter("@KodKP", strKodKP),
    '                         New SqlParameter("@KodVot", strKodVot),
    '                         New SqlParameter("@Perkara", strPerkara),
    '                         New SqlParameter("@Kuantiti", decKuantiti),
    '                         New SqlParameter("@kadarHarga", decKdrHrg),
    '                         New SqlParameter("@Jumlah", decJum)
    '                        }

    '                If Not dbconn.fInsertCommand(strSql, paramSql2) > 0 Then
    '                    blnSuccess = False
    '                    Exit Try
    '                Else
    '                    'AUDIT LOG
    '                    sLogMedan = "AR01_IdBil| AR01_NoBilSem|AR01_NoBil|AR01_Bil|KodKw|KodKO|KodPTJ|KodKP|KodVot|AR01_Perkara|AR01_Kuantiti|AR01_kadarHarga|AR01_Jumlah"
    '                    sLogBaru = intIdBil & "|" & strNoInvSem & "|" & intBil & "|" & strKodKW & "|" & strKodKO & "|" & strPTJ & "|" & strKodKP & "|" & strKodVot & "|" & strPerkara & "|" & decKuantiti & "|" & decKdrHrg & "|" & decJum & ""

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

    '                    If Not dbconn.fInsertCommand(strSql, paramSqlLog) > 0 Then
    '                        blnSuccess = False
    '                        Exit Try
    '                    End If
    '                End If
    '            Next

    '            'PADAM REKOD DALAM TABLE AR02_Lampiran
    '            strSql = "DELETE FROM AR02_Lampiran WHERE AR01_IdBil = " & intIdBil & " AND AR02_NoBilSem = '" & strNoInvSem & "'"

    '            If Not dbconn.fUpdateCommand(strSql) > 0 Then
    '                blnSuccess = False
    '                Exit Try
    '            Else
    '                'AUDIT LOG
    '                sLogMedan = "AR01_IdBil| AR02_NoBilSem"
    '                sLogBaru = intIdBil & "|" & strNoInvSem & ""

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

    '                If Not dbconn.fInsertCommand(strSql, paramSqlLog) > 0 Then
    '                    blnSuccess = False
    '                    Exit Try
    '                End If
    '            End If

    '            'MASUK REKOD LAMPIRAN
    '            intBil = 0
    '            For Each gvLampRow As GridViewRow In gvLamp.Rows
    '                Dim chkSel As CheckBox = DirectCast(gvLampRow.FindControl("cbSelect"), CheckBox)
    '                If chkSel.Checked = True Then
    '                    intBil += 1

    '                    Dim strNamaPen = TryCast(gvLampRow.FindControl("lblNmPen"), Label).Text
    '                    Dim strNoKP = TryCast(gvLampRow.FindControl("lblNoKP"), Label).Text
    '                    Dim strNoMatrik = TryCast(gvLampRow.FindControl("lblNoMatrik"), Label).Text
    '                    Dim strKursus = TryCast(gvLampRow.FindControl("lblKursus"), Label).Text
    '                    Dim decJumPel As Decimal = CDec(TryCast(gvLampRow.FindControl("txtAmaun"), TextBox).Text)

    '                    strSql = "INSERT INTO AR02_LAMPIRAN(AR01_IdBil, AR02_NoBilSem, AR02_NoBil, AR02_Bil, AR02_NAMAPENERIMA, AR02_NOKP, AR02_NOMATRIK, AR02_KURSUS, AR02_AMAUN)
    'VALUES(@IdBil, @NoBilSem, @NoBil, @Bil, @NamaPen, @NoKP, @NoMatrik, @Kursus, @Amaun)"
    '                    Dim paramSql2() As SqlParameter =
    '                            {
    '                             New SqlParameter("@IdBil", intIdBil),
    '                             New SqlParameter("@NoBilSem", strNoBilSem),
    '                             New SqlParameter("@NoBil", strNoBilSem),
    '                             New SqlParameter("@Bil", intBil),
    '                             New SqlParameter("@NamaPen", strNamaPen),
    '                             New SqlParameter("@NoKP", strNoKP),
    '                             New SqlParameter("@NoMatrik", strNoMatrik),
    '                             New SqlParameter("@Kursus", strKursus),
    '                             New SqlParameter("@Amaun", decJumPel)
    '                            }

    '                    If Not dbconn.fInsertCommand(strSql, paramSql2) > 0 Then
    '                        blnSuccess = False
    '                        Exit Try
    '                    Else
    '                        'AUDIT LOG
    '                        sLogMedan = "AR01_IdBil| AR02_NoBilSem| AR02_NoBil| AR02_Bil| AR02_NAMAPENERIMA| AR02_NOKP| AR02_NOMATRIK| AR02_KURSUS| AR02_AMAUN"
    '                        sLogBaru = intIdBil & "|" & strNoBilSem & "|" & strNoBilSem & "|" & intBil & "|" & strNamaPen & "|" & strNoKP & "|" & strNoMatrik & "|" & strKursus & "|" & decJumPel & ""

    '                        strSql = "insert into MK_AuditLog (UserID, UserGroup, UserUbah, SubMenu, Keterangan, InfoTable, RefKey, RefNo, InfoMedan, InfoBaru, " _
    '            & " InfoLama, UserIP, PCName) " _
    '            & " values (@UserID,@UserGroup,@UserUbah,@SubMenu,@Keterangan,@InfoTable,@RefKey, @RefNo,@InfoMedan,@InfoBaru,@InfoLama, " _
    '            & " @UserIP,@PCName)"

    '                        Dim paramSqlLog() As SqlParameter = {
    '                            New SqlParameter("@UserID", strNoStaf),
    '                            New SqlParameter("@UserGroup", sLogLevel),
    '                            New SqlParameter("@UserUbah", "-"),
    '                             New SqlParameter("@SubMenu", Session("sesKodSubMenu")),
    '                            New SqlParameter("@Keterangan", "INSERT"),
    '                            New SqlParameter("@InfoTable", "AR02_LAMPIRAN"),
    '                            New SqlParameter("@RefKey", "-"),
    '                            New SqlParameter("@RefNo", "-"),
    '                            New SqlParameter("@InfoMedan", sLogMedan),
    '                            New SqlParameter("@InfoBaru", sLogBaru),
    '                            New SqlParameter("@InfoLama", "-"),
    '                            New SqlParameter("@UserIP", lsLogUsrIP),
    '                            New SqlParameter("@PCName", lsLogUsrPC)
    '                        }

    '                        If Not dbconn.fInsertCommand(strSql, paramSqlLog) > 0 Then
    '                            blnSuccess = False
    '                            Exit Try
    '                        End If
    '                    End If
    '                End If

    '            Next

    '        Catch ex As Exception
    '            blnSuccess = False
    '        End Try

    '        If blnSuccess = True Then
    '            dbconn.sConnCommitTrans()
    '            Return True
    '        ElseIf blnSuccess = False Then
    '            dbconn.sConnRollbackTrans()
    '            Return False
    '        End If
    '    End Function

    'Protected Sub lnkBtnHantar_click(sender As Object, e As EventArgs)
    '    Dim strSql As String
    '    Dim dbConn As New DBKewConn
    '    Try
    '        Dim strNoInvCuk As String = lblNoInvSmtr.Text
    '        strSql = "update AR01_Bil set AR01_StatusDok ='02' where AR01_NoBilSem =@NoInvCuk"

    '        Dim paramSql() As SqlParameter = {
    '                New SqlParameter("@NoInvCuk", strNoInvCuk),
    '                New SqlParameter("@AR01_StatusDok", "02")
    '            }
    '        dbConn.sConnBeginTrans()
    '        If dbConn.fUpdateCommand(strSql, paramSql) > 0 Then
    '            dbConn.sConnCommitTrans()
    '            fGlobalAlert("Maklumat invois telah dihantar ke pejabat Bendahari", Me.Page, Me.[GetType]())
    '            fClearWizSt1()
    '            fClearWizSt2()

    '            divWiz.Visible = False
    '        Else
    '            dbConn.sConnRollbackTrans()
    '            fGlobalAlert("Tidak berjaya!", Me.Page, Me.[GetType]())
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub
    Private Sub sClear()
        txtNoInvSem.Text = ""
        txtStatus.Text = "PERMOHONAN "
        'ddlBank.SelectedValue = 0

        txtTujuan.Text = ""
        txtNoRujukan.Text = ""
        txtPerhatian.Text = ""
        rbKatPel.ClearSelection()
        sClearDdlPenaja()
        sClearDdlSesi()
        sClearGvLamp()
        sCleargvInvDt()
        sClearInfoPenaja()
        sClearDdlStatPelajar()
        txtPenaja.Text = ""
        ddlPenaja.Visible = True
        txtPenaja.Visible = False
        ddlSesi.Visible = True
        txtSesi.Visible = False
        rbKatPel.Enabled = True
        txtStatPel.Visible = False
        ddlStatPel.Visible = True
        lBtnSimpan.Visible = True
        lbtnHapus.Visible = False



    End Sub

    Private Sub sClearInfoPenaja()
        txtAlamat1.Text = ""
        txtAlamat2.Text = ""
        txtBandar.Text = ""
        txtPoskod.Text = ""
        txtNoTel.Text = ""
        txtNoFax.Text = ""
        txtEmel.Text = ""
        txtKodNegeri.Text = ""
        txtNegeri.Text = ""
        txtKodNegara.Text = ""
        txtNegara.Text = ""
        txtPerhatian.Text = ""
        txtKodPenghutang.Text = ""

        'ddlNegara.SelectedValue = 0
        'ddlNegeri.SelectedValue = 0

        'ddlNegeri.Items.Clear()
        'ddlNegara.Items.Clear()



    End Sub

    'Protected Sub lbtnTambah_Click(sender As Object, e As EventArgs)
    '    Dim decJumlah As Decimal
    '    Dim decHarga As Decimal
    '    Dim intKtt As Integer
    '    Try
    '        Dim strKW As String = CType(gvTransDt.Rows(gvTransDt.Rows.Count - 1).FindControl("ddlKW"), DropDownList).Text
    '        Dim strKO As String = CType(gvTransDt.Rows(gvTransDt.Rows.Count - 1).FindControl("ddlKO"), DropDownList).Text
    '        Dim strPTj As String = CType(gvTransDt.Rows(gvTransDt.Rows.Count - 1).FindControl("ddlPTj"), DropDownList).Text
    '        Dim strKP As String = CType(gvTransDt.Rows(gvTransDt.Rows.Count - 1).FindControl("ddlKP"), DropDownList).Text
    '        Dim strVot As String = CType(gvTransDt.Rows(gvTransDt.Rows.Count - 1).FindControl("ddlVot"), DropDownList).Text
    '        Dim strPerkara As String = CType(gvTransDt.Rows(gvTransDt.Rows.Count - 1).FindControl("txtPerkara"), TextBox).Text

    '        Dim strKtt As String = CType(gvTransDt.Rows(gvTransDt.Rows.Count - 1).FindControl("txtKuantiti"), TextBox).Text
    '        If strKtt = "" Then intKtt = 0 Else intKtt = CInt(strKtt)

    '        Dim strHarga As String = CType(gvTransDt.Rows(gvTransDt.Rows.Count - 1).FindControl("txtHarga"), TextBox).Text
    '        If strHarga = "" Then decHarga = 0 Else decHarga = CDec(strHarga)

    '        Dim strJumlah As String = CType(gvTransDt.Rows(gvTransDt.Rows.Count - 1).FindControl("lblJumlah"), Label).Text
    '        If strJumlah = "" Then decJumlah = 0 Else decJumlah = CDec(strJumlah)

    '        If strKW <> "0" AndAlso strKO <> "0" AndAlso strPTj <> "0" AndAlso strKP <> "0" AndAlso strVot <> "0" AndAlso strPerkara <> "" AndAlso intKtt <> 0 AndAlso decHarga <> 0.00 AndAlso decJumlah <> 0.00 Then
    '            AddNewRowToGrid()
    '        End If

    '    Catch ex As Exception

    '    End Try
    'End Sub

    Protected Sub lBtnSimpan_Click(sender As Object, e As EventArgs) Handles lBtnSimpan.Click

        Dim decJumtrans, decTotJumTrans As Decimal
        For Each gvRow As GridViewRow In gvTransDt.Rows
            Dim strJumlah = CType(gvRow.FindControl("lblJumlah"), Label).Text
            If strJumlah = "" Then decJumtrans = 0 Else decJumtrans = CDec(strJumlah)
            decTotJumTrans += decJumtrans
        Next

        Dim decTotJumLamp As Decimal
        For Each gvLampRow As GridViewRow In gvLamp.Rows
            Dim chkSel As CheckBox = DirectCast(gvLampRow.FindControl("cbSelect"), CheckBox)
            If chkSel.Checked = True Then
                Dim decAmtDC As Decimal = CDec(TryCast(gvLampRow.FindControl("lblJumDC"), Label).Text)
                decTotJumLamp += decAmtDC
            End If
        Next

        If decTotJumTrans <> decTotJumLamp Then
            fGlobalAlert("Jumlah Transaksi tidak sama dengan jumlah Lampiran!", Me.Page, Me.[GetType]())
            Exit Sub
        End If


        'Dim strNoInvSem As String = Trim(txtNoInvSem.Text.TrimEnd)
        'If String.IsNullOrEmpty(strNoInvSem) Then
        If fSimpan() Then
            fGlobalAlert("Permohonan invois telah disimpan!", Me.Page, Me.[GetType]())
            lBtnSimpan.Visible = False
            lbtnHapus.Visible = True
        Else
            fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
        End If
        'Else
        'If fKemasKini(strNoInvSem) Then
        '    fGlobalAlert("Permohonan invois telah disimpan!", Me.Page, Me.[GetType]())
        'Else
        '    fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
        'End If

        'End If


    End Sub

    Private Sub gvTransDt_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvTransDt.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim strJumlah As String = CType(e.Row.FindControl("lblJumlah"), Label).Text
                If strJumlah = "" Then strJumlah = "0.00"
                decJumlah += CDec(strJumlah)

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
                lblTotJum.Text = FormatNumber(decJumlah, 2)

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub sBatal()
        Dim strSql As String
        Dim strStatDok As String = "12"
        Dim strNoBilSem As String = Trim(txtNoInvSem.Text.TrimEnd)
        Dim blnSuccess As Boolean = True
        Dim strTkhToday As String = Now.ToString("yyyy-MM-dd")
        Dim dbconn As New DBKewConn
        Dim strNoStaf As String = Session("ssusrID")


        Try
            dbconn.sConnBeginTrans()

            If String.IsNullOrEmpty(strNoStaf) OrElse strNoStaf Is Nothing Then
                blnSuccess = False
                Exit Sub
            End If

            strSql = "update AR01_Bil set AR01_StatusDok = @StatusDok where AR01_NoBilSem = @NoBilSem"

            Dim paramSql() As SqlParameter =
                {
                New SqlParameter("@StatusDok", strStatDok),
                New SqlParameter("@NoBilSem", strNoBilSem)
                }


            If Not dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                blnSuccess = False
                Exit Try
            End If

            'Insert Into AR06_STATUSDOK
            strSql = "INSERT INTO AR06_STATUSDOK(AR06_NOBILSEM,AR06_NoBil, AR06_StatusDok,AR06_Tarikh,AR06_Ulasan, AR06_NoStaf)" &
                             "VALUES (@NOBILSEM,@NOBIL, @STATUSDOK,@TARIKH,@ULASAN, @NOSTAF)"

            Dim paramSql3() As SqlParameter =
                {
                New SqlParameter("@NOBILSEM", strNoBilSem),
                 New SqlParameter("@NOBIL", strNoBilSem),
                 New SqlParameter("@STATUSDOK", strStatDok),
                 New SqlParameter("@TARIKH", strTkhToday),
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
            fGlobalAlert("Permohonan invois telah dibatalkan!", Me.Page, Me.[GetType]())
            sLoadLst()
            divDetail.Visible = False
            divList.Visible = True
        ElseIf blnSuccess = False Then
            dbconn.sConnRollbackTrans()
            fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
        End If
    End Sub

    Private Sub lBtnBaru_Click(sender As Object, e As EventArgs) Handles lBtnBaru.Click
        sClear()
        'SetInitialRow()

        divList.Visible = False
        divDetail.Visible = True
        alert.Visible = False
    End Sub

    Private Sub lnkBtnBack_Click(sender As Object, e As EventArgs) Handles lnkBtnBack.Click
        sLoadLst()
        divList.Visible = True
        divDetail.Visible = False
    End Sub

    Private Sub gvLst_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvLst.SelectedIndexChanged
        sClear()
        Try
            Dim row As GridViewRow = gvLst.SelectedRow

            Dim intIdBil As Integer = CInt(CType(gvLst.SelectedRow.FindControl("lblIdBil"), Label).Text.TrimEnd)
            Dim strNoInvSem As String = CType(gvLst.SelectedRow.FindControl("lblNoInvSem"), Label).Text.TrimEnd
            Dim strStatDok As String = CType(gvLst.SelectedRow.FindControl("lblStatDok"), Label).Text.TrimEnd

            hidIdBil.Value = intIdBil

            If strStatDok = "01" Then
                lbtnHapus.Visible = True
            Else
                lbtnHapus.Visible = False
            End If

            sLoadInv(strNoInvSem)
            sLoadInvDt(strNoInvSem)
            sLoadInvLamp(intIdBil)

            divList.Visible = False
            divDetail.Visible = True
            lBtnSimpan.Visible = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub sLoadInv(ByVal strNoInv As String)

        Dim dbconn As New DBKewConn
        Try
            'Dim strSqlInv = $"Select  AR01_TkhMohon,AR01_KODPTJMOHON, AR01_NOSTAF, AR01_Jenis,AR01_KODBANK,AR01_NORUJUKAN,AR01_ALMT1,AR01_ALMT2,
            '                AR01_KATEGORI,AR01_IDPENERIMA, AR01_NAMAPENERIMA,kodnegara, kodnegeri, AR01_TUJUAN, AR01_BANDAR,AR01_POSKOD, AR01_TkhKonDari,AR01_TkhKonHingga,AR01_NOTEL, 
            '                AR01_NOFAKS, AR01_TempohKontrak, AR01_Emel,AR01_UtkPerhatian, AR01_StatusDok, (select Butiran  from AR_StatusDok where KodStatus = AR01_Bil.AR01_StatusDok  ) as ButStatDok, AR01_JenisPljr, AR01_StatPel, AR01_TkhKonDari, AR01_TkhKonHingga, AR01_TempohKontrak, AR01_KodSesi, KodPenghutang from AR01_Bil where AR01_NoBilSem='{strNoInvCuk}';"

            Dim strSql
            strSql = "Select  AR01_TkhMohon,AR01_KODPTJMOHON, AR01_NOSTAF, AR01_Jenis,AR01_KODBANK,AR01_NORUJUKAN,AR01_ALMT1,AR01_ALMT2,
                            AR01_KATEGORI,AR01_IDPENERIMA, AR01_NAMAPENERIMA, kodnegeri, (select Butiran from mk_negeri where mk_negeri.KodNegeri = AR01_Bil.KodNegeri) as Negeri, kodnegara, (select Butiran from mk_negara where mk_negara.kodnegara = AR01_Bil.kodnegara) as Negara, AR01_TUJUAN, AR01_BANDAR,AR01_POSKOD, AR01_TkhKonDari,AR01_TkhKonHingga, AR01_NOTEL, 
                            AR01_NOFAKS, AR01_TempohKontrak, AR01_Emel,AR01_UtkPerhatian, AR01_StatusDok, (select Butiran  from AR_StatusDok where KodStatus = AR01_Bil.AR01_StatusDok  ) as ButStatDok, AR01_JenisPljr, AR01_StatPel, AR01_TkhKonDari, AR01_TkhKonHingga, AR01_TempohKontrak, AR01_KodSesi, KodPenghutang from AR01_Bil where AR01_NoBilSem='" & strNoInv & "';"

            'Load Permohonan Invois
            Dim ds = dbconn.fSelectCommand(strSql)

            Using dtMhn = ds.Tables(0)
                If dtMhn.Rows.Count > 0 Then
                    Dim TkhMhn As Date = dtMhn.Rows(0)("AR01_TkhMohon")
                    Dim KodPTJ As String = dtMhn.Rows(0)("AR01_KODPTJMOHON")
                    Dim NoPmhn As String = dtMhn.Rows(0)("AR01_NOSTAF")
                    Dim katInv As String = dtMhn.Rows(0)("AR01_Jenis")
                    Dim strKodBank As String = dtMhn.Rows(0)("AR01_KODBANK")
                    Dim NoRuj As String = dtMhn.Rows(0)("AR01_NORUJUKAN")
                    Dim Almt1 As String = dtMhn.Rows(0)("AR01_ALMT1")
                    Dim Almt2 As String = dtMhn.Rows(0)("AR01_ALMT2")
                    Dim Kat As String = dtMhn.Rows(0)("AR01_KATEGORI")
                    Dim strNamaPenaja As String = dtMhn.Rows(0)("AR01_NAMAPENERIMA")
                    Dim strIdPenaja As String = dtMhn.Rows(0)("AR01_IDPENERIMA")
                    Dim strKodNegeri As String = dtMhn.Rows(0)("kodnegeri")
                    Dim strNegeri As String = dtMhn.Rows(0)("Negeri")
                    Dim strKodNegara As String = dtMhn.Rows(0)("kodnegara")
                    Dim strNegara As String = dtMhn.Rows(0)("Negara")
                    Dim Tujuan As String = dtMhn.Rows(0)("AR01_TUJUAN")
                    Dim Bandar As String = dtMhn.Rows(0)("AR01_BANDAR")
                    Dim Poskod As String = dtMhn.Rows(0)("AR01_POSKOD")
                    Dim NoTel As String = dtMhn.Rows(0)("AR01_NOTEL")
                    Dim NoFax As String = dtMhn.Rows(0)("AR01_NOFAKS")
                    Dim Emel As String = dtMhn.Rows(0)("AR01_Emel")
                    Dim Perhatian As String = dtMhn.Rows(0)("AR01_UtkPerhatian")
                    Dim strStatDok As String = dtMhn.Rows(0)("AR01_StatusDok")
                    Dim strJenPel As String = dtMhn.Rows(0)("AR01_JenisPljr")
                    Dim strStatPel As String = dtMhn.Rows(0)("AR01_StatPel")
                    Dim strKodSesi As String = dtMhn.Rows(0)("AR01_KodSesi")
                    Dim strKodPenghutang As String = dtMhn.Rows(0)("KodPenghutang")

                    txtStatus.Text = dtMhn.Rows(0)("ButStatDok")
                    txtTkhMohon.Text = TkhMhn
                    txtNoInvSem.Text = strSql

                    rbKatPel.SelectedValue = strJenPel
                    txtStatPel.Text = strStatPel
                    txtPenaja.Text = strIdPenaja & " - " & strNamaPenaja
                    txtKodPenghutang.Text = strIdPenaja
                    txtSesi.Text = strKodSesi
                    txtTujuan.Text = Tujuan
                    txtNoRujukan.Text = IIf(NoRuj = "", "-", NoRuj)
                    txtAlamat1.Text = IIf(Almt1 = "", "-", Almt1)
                    txtAlamat2.Text = IIf(Almt2 = "", "-", Almt2)
                    txtBandar.Text = IIf(Bandar = "", "-", Bandar)
                    txtPoskod.Text = IIf(Poskod = "", "-", Poskod)
                    txtNoTel.Text = IIf(NoTel = "", "-", NoTel)
                    txtNoFax.Text = IIf(NoFax = "", "-", NoFax)
                    txtEmel.Text = IIf(Emel = "", "-", Emel)
                    txtPerhatian.Text = IIf(Perhatian = "", "-", Perhatian)
                    ddlBank.SelectedValue = strKodBank

                    txtKodNegeri.Text = IIf(strKodNegeri = "", "-", strKodNegeri)
                    txtNegeri.Text = IIf(strNegeri = "", "-", strNegeri)
                    txtKodNegara.Text = IIf(strKodNegara = "", "-", strKodNegara)
                    txtNegara.Text = IIf(strNegara = "", "-", strNegara)

                    ddlPenaja.Visible = False
                    txtPenaja.Visible = True
                    ddlSesi.Visible = False
                    txtSesi.Visible = True
                    rbKatPel.Enabled = False
                    txtStatPel.Visible = True
                    ddlStatPel.Visible = False
                End If
            End Using

        Catch ex As Exception

        End Try

    End Sub

    Private Sub sLoadInvDt(strNoInvSem)
        Try
            Dim strSql = "select AR01_BilDtID, KodKw,KodKO,KodPTJ,KodKP,KodVot,AR01_Perkara,AR01_Kuantiti,AR01_kadarHarga,AR01_Jumlah from AR01_BilDt where AR01_NoBilSem='" & strNoInvSem & "';"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            Using dt = ds.Tables(0)
                ViewState("dtInvDt") = dt
                gvTransDt.DataSource = dt
                gvTransDt.DataBind()
            End Using
        Catch ex As Exception

        End Try
    End Sub

    Private Sub sLoadInvLamp(ByVal intIdBil As Integer)
        Try
            Dim strSql = "select AR02_IdLamp, AR02_NamaPenerima as Nama, AR02_NoKP as NoKP, AR02_NoMatrik as NoMatrik, AR02_Kursus as Kursus, AR02_Amaun as JumDC, AR02_Butiran as Sesi, '-' as KP from AR02_Lampiran where AR01_IdBil = " & intIdBil
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

    'Private Sub gvTransDt_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvTransDt.RowDeleting
    '    Try
    '        Dim rowIndex As Integer = 0
    '        Dim index As Integer = Convert.ToInt32(e.RowIndex)

    '        If ViewState("dtInvDt") IsNot Nothing Then
    '            Dim dtCurrentTable As DataTable = CType(ViewState("dtInvDt"), DataTable)
    '            Dim drCurrentRow As DataRow = Nothing

    '            If dtCurrentTable.Rows.Count > 1 Then
    '                dtCurrentTable.Rows(index).Delete()
    '                dtCurrentTable.AcceptChanges()
    '                ViewState("dtInvDt") = dtCurrentTable
    '                gvTransDt.DataSource = dtCurrentTable
    '                gvTransDt.DataBind()
    '            End If

    '        End If

    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub sCleargvInvDt()
        gvTransDt.DataSource = New List(Of String)
        gvTransDt.DataBind()
    End Sub

    Private Sub ddlFilStat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFilStat.SelectedIndexChanged
        sLoadLst()
    End Sub

    Protected Sub ddlNmPen_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    'Private Sub lbtnTambahPen_Click(sender As Object, e As EventArgs) Handles lbtnTambahPen.Click

    '    If rbKatPel.SelectedIndex = -1 Then
    '        fGlobalAlert("Sila pilih kategori pelajar!", Me.Page, Me.[GetType]())
    '        Exit Sub
    '    End If
    '    txtFilter.Text = ""
    '    sClearGvLstPel()
    '    mpeLstPel.Show()
    'End Sub

    'Private Sub ddlFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFilter.SelectedIndexChanged
    '    'If ddlFilter.SelectedIndex = 0 Then
    '    '    txtFilter.Enabled = False
    '    'Else
    '    '    txtFilter.Enabled = True
    '    'End If
    '    txtFilter.Text = ""
    '    mpeLstPel.Show()
    'End Sub



    'Private Sub sLoadPel()

    '    Try
    '        ' sClearGvLstPel()
    '        Dim strSql, strFilter As String
    '        Dim strFiltParam As String = Trim(txtFilter.Text.TrimEnd)

    '        If rbKatPel.SelectedValue = 0 Then 'PRA SIS
    '            If ddlFilter.SelectedValue = 1 Then
    '                'NO MATRIK
    '                strFilter = " where smp01_nomatrik like '%" & strFiltParam & "%'"
    '            ElseIf ddlFilter.SelectedValue = 2 Then
    '                'NAMA
    '                strFilter = " where smp01_nama like '%" & strFiltParam & "%'"
    '            End If

    '            strSql = "Select SMP01_NOMATRIK as NoMatrik, SMP01_NAMA as NamaPel,(SMP01_NOMATRIK + ' - ' + SMP01_NAMA) as Pel, SMP01_KURSUS as KodKursus,smp01_kp as IDPel from smp01_peribadi " & strFilter & " order by smp01_nomatrik"


    '        ElseIf rbKatPel.SelectedValue = 1 Then 'PASCA SIS
    '            If ddlFilter.SelectedValue = 1 Then
    '                'NO MATRIK
    '                strFilter = " and a.smg01_nomatrik like '%" & strFiltParam & "%'"
    '            ElseIf ddlFilter.SelectedValue = 2 Then
    '                'NAMA
    '                strFilter = " and a.smg02_nama like '%" & strFiltParam & "%'"
    '            End If

    '            strSql = "Select a.SMG01_NOMATRIK as NoMatrik, a.SMG02_NAMA as NamaPel, (a.SMG01_NOMATRIK + ' - ' +  a.SMG02_NAMA) as Pel, b.SMG01_KODKURSUS as KodKursus,a.SMG02_ID as IDPel from SMG02_PERIBADI a,SMG01_PENGAJIAN b where a.smg01_nomatrik=b.smg01_nomatrik " & strFilter & " order by a.smg01_nomatrik"
    '        End If

    '        Dim dbconn As New DBSMPConn
    '        Using ds = dbconn.fselectCommand(strSql)
    '            If ds.Tables(0).Rows.Count > 0 Then
    '                gvLstPel.DataSource = ds
    '                gvLstPel.DataBind()


    '                Dim dt As New DataTable
    '                dt = ds.Tables(0)

    '                'ddlPel.DataSource = dt
    '                'ddlPel.DataTextField = "Pel"
    '                'ddlPel.DataValueField = "NoMatrik"
    '                'ddlPel.DataBind()

    '                'ddlPel.Items.Insert(0, New ListItem(" - SILA PILIH -", 0))
    '                'ddlPel.SelectedIndex = 0

    '                ViewState("dtLstPel") = dt
    '            End If
    '        End Using

    '    Catch ex As Exception

    '    End Try

    '    mpeLstPel.Show()
    'End Sub

    'Private Sub gvLstPel_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvLstPel.PageIndexChanging
    '    Try
    '        gvLstPel.PageIndex = e.NewPageIndex
    '        If ViewState("dtLstPel") IsNot Nothing Then
    '            gvLstPel.DataSource = ViewState("dtLstPel")
    '            gvLstPel.DataBind()
    '        End If
    '        mpeLstPel.Show()
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Private Sub gvLstPel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvLstPel.SelectedIndexChanged

    '    Try

    '        Dim dt As New DataTable
    '        dt = sLoadDtLamp()

    '        If Not dt Is Nothing Then
    '            Dim selRow As GridViewRow = gvLstPel.SelectedRow
    '            Dim strNoMatrik As String = CType(selRow.FindControl("lblNoMatrik"), Label).Text.TrimEnd
    '            Dim strNmPel As String = CType(selRow.FindControl("lblNmPel"), Label).Text.TrimEnd
    '            Dim strKodKursus As String = CType(selRow.FindControl("lblKodKursus"), Label).Text.TrimEnd
    '            Dim strIdPel As String = CType(selRow.FindControl("lblIdPel"), Label).Text.TrimEnd

    '            Dim dr As DataRow
    '            dr = dt.NewRow()
    '            dr("AR02_IdLamp") = String.Empty
    '            dr("AR02_NoMatrik") = strNoMatrik
    '            dr("AR02_NamaPenerima") = strNmPel
    '            dr("AR02_Kursus") = strKodKursus
    '            dr("AR02_NoKP") = strIdPel
    '            dr("AR02_Butiran") = String.Empty
    '            dr("AR02_Amaun") = String.Empty

    '            dt.Rows.Add(dr)
    '            ViewState("dtLamp") = dt

    '            gvLamp.DataSource = dt
    '            gvLamp.DataBind()

    '        End If

    '    Catch ex As Exception
    '        fErrorLog("gvLstPel_SelectedIndexChanged - " & ex.Message.ToString)
    '    End Try
    'End Sub

    Private Function sLoadDtLamp() As DataTable
        Try
            Dim dt As New DataTable
            dt = sInitDtLamp()
            If Not dt Is Nothing Then
                If gvLamp.Rows.Count > 0 Then
                    For Each gvRow As GridViewRow In gvLamp.Rows
                        Dim strIdLamp = CType(gvRow.FindControl("lblIdLamp"), Label).Text
                        Dim strNoMatrik = CType(gvRow.FindControl("lblNoMatrik"), Label).Text
                        Dim strNmPel = CType(gvRow.FindControl("lblNmPen"), Label).Text
                        Dim strKodKursus = CType(gvRow.FindControl("lblKursus"), Label).Text
                        Dim strIdPel = CType(gvRow.FindControl("lblNoKP"), Label).Text
                        Dim strButiran = CType(gvRow.FindControl("txtButiran"), TextBox).Text
                        Dim strJumlah = CType(gvRow.FindControl("lblJumDC"), Label).Text

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
            Else
                Return Nothing
            End If

        Catch ex As Exception
            fErrorLog("sLoadDtLamp - " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function

    Private Sub gvLamp_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvLamp.RowDeleting
        Try
            Dim rowIndex As Integer = 0
            Dim index As Integer = Convert.ToInt32(e.RowIndex)

            'If ViewState("dtLamp") IsNot Nothing Then

            Dim dt As New DataTable
            dt = sLoadDtLamp()

            Dim dtCurrentTable As DataTable = dt 'CType(ViewState("dtLamp"), DataTable)
            Dim drCurrentRow As DataRow = Nothing

            'If dtCurrentTable.Rows.Count > 1 Then
            dtCurrentTable.Rows(index).Delete()
            dtCurrentTable.AcceptChanges()
            ViewState("dtLamp") = dtCurrentTable
            gvLamp.DataSource = dtCurrentTable
            gvLamp.DataBind()
            'End If
            'End If

        Catch ex As Exception

        End Try
    End Sub
    Dim decJumLamp As Decimal
    Private Sub gvLamp_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvLamp.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim strJumlah As String = CType(e.Row.FindControl("lblJumDC"), Label).Text
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

    'Private Sub lbtnCariPel_Click(sender As Object, e As EventArgs) Handles lbtnCariPel.Click
    '    sLoadPel()
    'End Sub

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


    'Protected Sub txtAmaun_TextChanged(sender As Object, e As EventArgs)
    '    Try
    '        Dim decAmaun As Decimal
    '        Dim txtAmaun As TextBox = CType(CType(sender, Control), TextBox)
    '        Dim strAmaun As String = txtAmaun.Text
    '        Dim decJumAmaun As Decimal

    '        If strAmaun = "" Then decAmaun = 0 Else decAmaun = CDec(strAmaun)

    '        txtAmaun.Text = FormatNumber(decAmaun)

    '        For Each gvRow As GridViewRow In gvLamp.Rows
    '            Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("cbSelect"), CheckBox)

    '            If chkSel.Checked = True Then
    '                Dim decJumlah As String = CDec(Trim(CType(gvRow.FindControl("txtAmaun"), TextBox).Text.TrimEnd))
    '                decJumAmaun += decJumlah
    '            End If
    '        Next

    '        Dim footerRow = gvLamp.FooterRow
    '        CType(footerRow.FindControl("lblTotJum"), Label).Text = FormatNumber(decJumAmaun)

    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub ddlPenaja_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPenaja.SelectedIndexChanged
        Try
            sClearDdlSesi()
            sCleargvInvDt()
            sClearGvLamp()
            sLoadInfoPenaja()
            fBindSesi()
            alert.Visible = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub sLoadInfoPenaja()
        Try
            sClearInfoPenaja()

            Dim strIdPenghutang As String = ddlPenaja.SelectedValue.Trim
            Dim dtPenghutang As DataTable = ViewState("dtPenghutang")
            Dim dvPenghutang As New DataView(dtPenghutang)

            dvPenghutang.RowFilter = "IdPenghutang = '" & strIdPenghutang & "'"

            If Not dvPenghutang Is Nothing Then
                If dvPenghutang.Count > 0 Then
                    'Dim strPerhatian = dvPenghutang.Item(0)("").ToString
                    Dim strKodNegeri = dvPenghutang.Item(0)("KodNegeri").ToString
                    Dim strKodNegara = dvPenghutang.Item(0)("KodNegara").ToString

                    txtKodPenghutang.Text = dvPenghutang.Item(0)("KodPenghutang").ToString

                    txtAlamat1.Text = dvPenghutang.Item(0)("Alamat1").ToString
                    txtAlamat2.Text = dvPenghutang.Item(0)("Alamat2").ToString
                    txtPoskod.Text = dvPenghutang.Item(0)("Poskod").ToString
                    txtBandar.Text = dvPenghutang.Item(0)("Bandar").ToString
                    txtNoTel.Text = dvPenghutang.Item(0)("NoTel").ToString
                    txtNoFax.Text = dvPenghutang.Item(0)("NoFax").ToString
                    txtEmel.Text = dvPenghutang.Item(0)("Emel").ToString
                    txtPerhatian.Text = IIf(IsDBNull(dvPenghutang.Item(0)("Perhatian").ToString), "-", dvPenghutang.Item(0)("Perhatian").ToString)

                    txtKodNegeri.Text = IIf(strKodNegeri = "", "-", strKodNegeri)
                    txtNegeri.Text = dvPenghutang.Item(0)("ButNegeri").ToString

                    txtKodNegara.Text = IIf(strKodNegara = "", "-", strKodNegara)
                    txtNegara.Text = dvPenghutang.Item(0)("ButNegara").ToString

                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub sLoadPelPraSis(ByVal strIdPenaja As String, ByVal strKodSesiSem As String, ByRef dt As DataTable)
        Dim dbSMPconn As New DBSMPConn
        Dim intCount As Integer = 0
        Dim strSql

        Try

            sClearGvLamp()

            strSql = "select SMP51_APDebitCaj.SMP01_Nomatrik AS NoMatrik, SMP01_Peribadi.SMP01_KP AS NoKP, SMP01_Peribadi.SMP01_Nama AS Nama, SMP01_Peribadi.SMP01_Kursus AS Kursus, sum(SMP51_JumDC) AS JumDC, SMP49_APBiasiswaPL.SMP_BSWKOD, SMP51_KodSesi as Sesi, '-' as KP 
from SMP51_APDebitCaj
inner join SMP49_APBiasiswaPL on SMP49_APBiasiswaPL.SMP01_Nomatrik = SMP51_APDebitCaj.SMP01_Nomatrik and SMP49_APBiasiswaPL.KodSesi_Sem = SMP51_APDebitCaj.SMP51_KodSesi
inner join SMP01_Peribadi on SMP01_Peribadi.SMP01_Nomatrik = SMP51_APDebitCaj.SMP01_Nomatrik
where SMP51_APDebitCaj.SMP51_KodSesi = '" & strKodSesiSem & "' and SMP49_APBiasiswaPL.SMP_BSWKOD = '" & strIdPenaja & "'
group by SMP51_APDebitCaj.SMP01_Nomatrik, SMP01_Peribadi.SMP01_KP, SMP01_Peribadi.SMP01_Nama, SMP01_Peribadi.SMP01_Kursus, SMP49_APBiasiswaPL.SMP_BSWKOD, SMP51_APDebitCaj.SMP51_KodSesi"

            Using ds = dbSMPconn.fselectCommand(strSql)
                If Not ds Is Nothing Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        gvLamp.DataSource = ds.Tables(0)
                        gvLamp.DataBind()
                        intCount = ds.Tables(0).Rows.Count

                        dt = ds.Tables(0)
                    End If
                End If
            End Using

            If intCount = 0 Then
                lblPenaja.Text = Trim(ddlPenaja.SelectedItem.Text.TrimEnd)
                lblSesi.Text = Trim(ddlSesi.SelectedValue.TrimEnd)
                alert.Visible = True
            Else
                alert.Visible = False
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub sLoadPelPasSis(ByVal strIdPenaja As String, ByVal strKodSesiSem As String, ByRef dt As DataTable)
        Dim dbSMPconn As New DBSMPConn
        Dim strSql
        Dim intCount As Integer
        Dim strFilter
        Try

            sClearGvLamp()

            Dim strStatPel = Trim(ddlStatPel.SelectedValue.TrimEnd)


            If strStatPel = "PERDANA" Then
                strFilter = "AND LEFT(SMG01_PENGAJIAN.SMG01_KODYURAN, 1) <> 'K'"
            ElseIf strStatPel = "PESISIR" Then
                strFilter = "AND LEFT(SMG01_PENGAJIAN.SMG01_KODYURAN, 1) = 'K'"
            End If

            strSql = "select SMG27_APDEBITCAJ.SMG01_Nomatrik AS NoMatrik, SMG02_Peribadi.SMG02_ID AS NoKP, SMG02_Peribadi.SMG02_NAMA AS Nama, SMG01_PENGAJIAN.SMG01_KODKURSUS AS Kursus, sum(SMG27_APDEBITCAJ.SMG27_JumDC) AS JumDC, SMG27_APDEBITCAJ.SMG27_KodSesi as Sesi, SMG06_AGENSIPEMBIAYA.SMG06_AGENSI, SMG01_PENGAJIAN.SMG01_KODYURAN as KP from SMG27_APDEBITCAJ
inner join SMG06_AGENSIPEMBIAYA on SMG06_AGENSIPEMBIAYA.SMG01_NOMATRIK = SMG27_APDEBITCAJ.SMG01_Nomatrik
inner join SMG02_Peribadi on SMG02_Peribadi.SMG01_NOMATRIK = SMG27_APDEBITCAJ.SMG01_Nomatrik
inner join  SMG01_PENGAJIAN on SMG01_PENGAJIAN.SMG01_NOMATRIK = SMG02_Peribadi.SMG01_NOMATRIK
where SMG27_KodSesi = '" & strKodSesiSem & "' AND SMG06_AGENSIPEMBIAYA.SMG06_AGENSI = '" & strIdPenaja & "' and SMG27_Status = 1 " & strFilter & "
group by SMG27_APDEBITCAJ.SMG01_Nomatrik, SMG02_Peribadi.SMG02_ID, SMG02_Peribadi.SMG02_NAMA, SMG01_PENGAJIAN.SMG01_KODKURSUS, SMG27_APDEBITCAJ.SMG27_KodSesi, SMG06_AGENSIPEMBIAYA.SMG06_AGENSI,SMG01_PENGAJIAN.SMG01_KODYURAN"

            Using ds = dbSMPconn.fselectCommand(strSql)
                If Not ds Is Nothing Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        gvLamp.DataSource = ds.Tables(0)
                        gvLamp.DataBind()
                        intCount = ds.Tables(0).Rows.Count
                        dt = ds.Tables(0)
                    End If
                End If
            End Using

            If intCount = 0 Then
                lblPenaja.Text = Trim(ddlPenaja.SelectedItem.Text.TrimEnd)
                lblSesi.Text = Trim(ddlSesi.SelectedValue.TrimEnd)
                alert.Visible = True
            Else
                alert.Visible = False
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub ddlSesi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSesi.SelectedIndexChanged
        Dim dtPel As New DataTable
        Dim strIdPenaja = Trim(ddlPenaja.SelectedValue.TrimEnd)
        Dim strKodSesiSem = Trim(ddlSesi.SelectedValue.TrimEnd)
        alert.Visible = False

        Dim strKatPel = Trim(rbKatPel.SelectedValue.TrimEnd)
        Dim strStatPel = Trim(ddlStatPel.SelectedValue.TrimEnd)

        If strKatPel = "UG" Then
            sLoadPelPraSis(strIdPenaja, strKodSesiSem, dtPel)
        ElseIf strKatPel = "PG" Then
            sLoadPelPasSis(strIdPenaja, strKodSesiSem, dtPel)
        ElseIf strKatPel = "SPPSH" Then

        End If

        sLoadTrans(strKatPel, strStatPel, dtPel)

    End Sub

    Private Sub sLoadTrans(ByVal strKatPel As String, ByVal strStatPel As String, ByVal dtPel As DataTable)

        Try

            If dtPel Is Nothing Then
                Exit Sub
            End If

            If dtPel.Rows.Count = 0 Then
                Exit Sub
            End If


            Dim strKW, strKO, strPTj, strKP, strVot

            If strKatPel = "UG" Then

                If strStatPel = "PERDANA" Then
                    strKW = "01"
                    strKO = "00"
                    strPTj = "50000"
                    strKP = "0000000"
                    strVot = "71998"
                End If

            ElseIf strKatPel = "PG" Then
                If strStatPel = "PERDANA" Then
                    strKW = "07"
                    strKO = "00"
                    strPTj = "50000"
                    strKP = "0000000"
                    strVot = "71999"

                ElseIf strStatPel = "PESISIR" Then

                    strKW = "08"
                    strKO = "00"

                    strKP = dtPel.Rows(0)("KP").ToString

                    If strKP.Contains("M") Then
                        strKP = Trim(strKP.replace("M", "")).TrimEnd
                    End If

                    Select Case strKP
                            Case "K35014", "K35013", "K35008", "K35015", "K35003", "K35008", "K35015", "K35019"
                                strPTj = "35000"

                        End Select




                        strVot = "71999"

                    End If

                ElseIf strKatPel = "SPPSH" Then
                If strStatPel = "SEPARA MASA" Then
                    strKW = "08"
                    strKO = "00"
                    strPTj = ""
                    strKP = ""
                    strVot = "71998"

                End If
            End If

            Dim dtTrans As New DataTable

            dtTrans.Columns.Add(New DataColumn("KodKw", GetType(String)))
            dtTrans.Columns.Add(New DataColumn("KodKO", GetType(String)))
            dtTrans.Columns.Add(New DataColumn("kodPTJ", GetType(String)))
            dtTrans.Columns.Add(New DataColumn("kodKP", GetType(String)))
            dtTrans.Columns.Add(New DataColumn("kodVot", GetType(String)))
            dtTrans.Columns.Add(New DataColumn("AR01_Perkara", GetType(String)))
            dtTrans.Columns.Add(New DataColumn("AR01_Kuantiti", GetType(Integer)))
            dtTrans.Columns.Add(New DataColumn("AR01_kadarHarga", GetType(Decimal)))
            dtTrans.Columns.Add(New DataColumn("AR01_Jumlah", GetType(Decimal)))

            Dim dtrow = From row In dtPel.AsEnumerable()
                        Group row By grp = New With {
                              Key .JumDC = row.Field(Of Decimal)("JumDC"),
                              Key .Sesi = row.Field(Of String)("Sesi")
                                } Into Group
                        Select New With {
                                               Key .count = Group.Count,
                                               Key .JumDC = grp.JumDC,
                                               Key .Sesi = grp.Sesi
                                        }

            For Each item In dtrow
                Dim intBilPel As Integer = item.count
                Dim decAmtDC As Decimal = item.JumDC
                Dim decJumDC As Decimal = intBilPel * decAmtDC

                dtTrans.Rows.Add(strKW, strKO, strPTj, strKP, strVot, item.Sesi, intBilPel, Format(decAmtDC), Format(decJumDC))
            Next

            gvTransDt.DataSource = dtTrans
            gvTransDt.DataBind()

        Catch ex As Exception

        End Try

    End Sub

    Private Sub fBindSesi()
        Dim strSql As String
        Dim dbconn As New DBSMPConn

        Try
            ddlSesi.Items.Clear()

            Dim strLstTahun, strTahun As String

            For i As Integer = 0 To 2
                strTahun = Now.Year - i
                strLstTahun = strLstTahun & "," & strTahun
            Next

            strLstTahun = strLstTahun.Remove(0, 1)

            If rbKatPel.SelectedValue = "UG" Then
                'strSql = "select KodSesi_Sem from SMP_SesiPengajian where Stat_Skrg = 1 and KodSesi_Sem like '%" & Now.Year & "%'"

                strSql = "SELECT KodSesi_Sem from SMP_SesiPengajian where left(KodSesi_Sem,1) <> 0 and Keaktifan = 1 and year(TrkMula_Sem) in (" & strLstTahun & ") order by bil desc"
            ElseIf rbKatPel.SelectedValue = "PG" Then
                strSql = "select KodSesi_Sem from SMG_SESIPENGAJIAN where Keaktifan = 1 and year(TrkMula_Sem) in (" & strLstTahun & ") order by bil desc "
            End If

            Using ds = dbconn.fSelectCommand(strSql)
                ddlSesi.DataSource = ds
                ddlSesi.DataTextField = "KodSesi_Sem"
                ddlSesi.DataValueField = "KodSesi_Sem"
                ddlSesi.DataBind()

                ddlSesi.Items.Insert(0, New ListItem("- SILA PILIH -", "0"))
                ddlSesi.SelectedValue = "0"
            End Using


        Catch ex As Exception

        End Try


    End Sub

    Protected Sub cbSelect_CheckedChanged(sender As Object, e As EventArgs)

        Try
            Dim decJumDC As Decimal
            For Each gvLampRow As GridViewRow In gvLamp.Rows
                Dim chkSel As CheckBox = DirectCast(gvLampRow.FindControl("cbSelect"), CheckBox)
                Dim lblJumDC As Label = TryCast(gvLampRow.FindControl("lblJumDC"), Label)

                If chkSel.Checked = True Then
                    gvLampRow.Font.Strikeout = False
                    Dim decAmtDC As Decimal = CDec(lblJumDC.Text)
                    decJumDC += decAmtDC

                Else
                    gvLampRow.Font.Strikeout = True

                End If
            Next

            Dim footerTrans = gvLamp.FooterRow
            Dim lblTotJum As Label = CType(footerTrans.FindControl("lblTotJum"), Label)
            lblTotJum.Text = FormatNumber(decJumDC)

            Dim strKatPel = Trim(rbKatPel.SelectedValue.TrimEnd)
            Dim strStatPel = Trim(ddlStatPel.SelectedValue.TrimEnd)


            Dim dtPel As New DataTable

            dtPel.Columns.Add(New DataColumn("Nama", GetType(String)))
            dtPel.Columns.Add(New DataColumn("NoKP", GetType(String)))
            dtPel.Columns.Add(New DataColumn("NoMatrik", GetType(String)))
            dtPel.Columns.Add(New DataColumn("Kursus", GetType(String)))
            dtPel.Columns.Add(New DataColumn("Sesi", GetType(String)))
            dtPel.Columns.Add(New DataColumn("JumDC", GetType(Decimal)))
            dtPel.Columns.Add(New DataColumn("KP", GetType(String)))


            For Each gvLampRow As GridViewRow In gvLamp.Rows
                Dim chkSel As CheckBox = DirectCast(gvLampRow.FindControl("cbSelect"), CheckBox)
                If chkSel.Checked = True Then
                    Dim strNamaPen = TryCast(gvLampRow.FindControl("lblNmPen"), Label).Text
                    Dim strNoKP = TryCast(gvLampRow.FindControl("lblNoKP"), Label).Text
                    Dim strNoMatrik = TryCast(gvLampRow.FindControl("lblNoMatrik"), Label).Text
                    Dim strKursus = TryCast(gvLampRow.FindControl("lblKursus"), Label).Text
                    Dim strButiran = TryCast(gvLampRow.FindControl("lblButiran"), Label).Text
                    Dim decAmaun As Decimal = CDec(TryCast(gvLampRow.FindControl("lblJumDC"), Label).Text)
                    Dim strKP As String = TryCast(gvLampRow.FindControl("lblKP"), Label).Text

                    dtPel.Rows.Add(strNamaPen, strNoKP, strNoMatrik, strKursus, strButiran, decAmaun, strKP)
                End If
            Next

            sLoadTrans(strKatPel, strStatPel, dtPel)

        Catch ex As Exception

        End Try

    End Sub

    Private Sub lbtnHapus_Click(sender As Object, e As EventArgs) Handles lbtnHapus.Click
        sBatal()
    End Sub

    Private Sub ddlStatPel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatPel.SelectedIndexChanged
        Try

            sClearDdlSesi()
            sClearInfoPenaja()
            sClearGvLamp()
            sCleargvInvDt()
            ddlPenaja.SelectedIndex = 0
            alert.Visible = False

        Catch ex As Exception

        End Try
    End Sub
End Class