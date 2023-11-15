Imports System.Data.SqlClient
Imports System.Drawing
Imports SMKB_Web_Portal.clsAlert

Public Class Agihan_Objek_Am
    Inherits System.Web.UI.Page
    Dim sLogBaru As String
    Dim sLogMedan As String
    Dim strNoStaf As String = UserInfo.strSesStaffID
    Dim sLogLevel As String = UserInfo.strUsrTahap
    Dim lsLogUsrIP As String = UserInfo.strUsrPcIP
    Dim lsLogUsrPC As String = UserInfo.strUsrPCName

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Session("sesKodSubMenu") = Request.QueryString("KodSubMenu")
                fBindDdlTahunAgih()
                fBindDdlKW()

                ddlPTj.Items.Add(New ListItem("- SILA PILIH KW -", 0))
                ddlKO.Items.Add(New ListItem("- SILA PILIH PTj -", 0))
                ddlKp.Items.Add(New ListItem("- SILA PILIH KO -", 0))
                sClearObjAm()
                txtJumBajetKp.Enabled = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindDdlTahunAgih()

        ddlTahunAgih.Items.Clear()
        Dim strTahun
        For i As Integer = 0 To 1
            strTahun = Now.Year + i
            ddlTahunAgih.Items.Add(New ListItem(strTahun, strTahun))
        Next

        ddlTahunAgih.SelectedValue = Now.Year + 1

    End Sub

    Private Sub sClearObjAm()
        gvObjAm.DataSource = New List(Of String)
        gvObjAm.DataBind()
    End Sub

    Private Sub sClearField()
        txtJumBajetKp.Text = "0.00"
        txtTBKp.Text = "0.00"
        txtKGKp.Text = "0.00"
        txtBFKp.Text = "0.00"
        txtJumKp.Text = "0.00"
        txtAgihanKp.Text = "0.00"
        txtBakiKp.Text = "0.00"
    End Sub

    Private Sub fBindDdlPTJ(ByVal strKodKW As String)
        Try
            Dim strSql As String

            strSql = "SELECT DISTINCT TOP 100 PERCENT dbo.MK_PTJ.KodPTJ,(MK_PTJ.KodPTJ + ' - ' + dbo.MK_PTJ.Butiran) as Butiran " &
"FROM dbo.MK_PTJ INNER JOIN dbo.MK01_VotTahun ON dbo.MK_PTJ.KodPTJ = dbo.MK01_VotTahun.KodPTJ " &
"WHERE dbo.MK01_VotTahun.mk01_tahun = '" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' and dbo.MK01_VotTahun.KodKw = '" & strKodKW & "' order by KodPtj"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)
            ddlPTj.DataSource = ds
            ddlPTj.DataTextField = "Butiran"
            ddlPTj.DataValueField = "KodPTJ"
            ddlPTj.DataBind()

            ddlPTj.Items.Insert(0, "- SILA PILIH -")
            ddlPTj.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub


    Private Sub fBindDdlKW()
        Try

            Dim strSql As String = "SELECT DISTINCT dbo.MK_Kw.KodKw, (dbo.MK_Kw.KodKw + ' - ' + dbo.MK_Kw.Butiran) as Butiran " &
                  "FROM dbo.MK_Kw INNER JOIN dbo.MK01_VotTahun ON dbo.MK_Kw.KodKw = dbo.MK01_VotTahun.KodKw " &
                    "where dbo.MK01_VotTahun.MK01_Tahun = '" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' ORDER BY dbo.MK_Kw.KodKw"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fselectCommand(strSql)
            ddlKW.DataSource = ds

            ddlKW.DataTextField = "Butiran"
            ddlKW.DataValueField = "KodKW"
            ddlKW.DataBind()

            ddlKW.Items.Insert(0, "- SILA PILIH -")
            ddlKW.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlKW_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKW.SelectedIndexChanged
        sClear()
        fClearDdlKP()
        fClearDdlKO()
        Dim strKodKW As String = Trim(ddlKW.SelectedValue.TrimEnd)
        fBindDdlPTJ(strKodKW)
    End Sub

    Private Sub ddlPTj_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPTj.SelectedIndexChanged
        sClear()
        fClearDdlKP()

        If ddlPTj.SelectedIndex <> 0 Then
            Dim strKodPTj As String = Trim(ddlPTj.SelectedValue.TrimEnd)
            fGetBajetPTj(strKodPTj)
            If CDec(txtJumBajetPTj.Text) <> 0 Then
                fBindDdlKO()
            Else
                fGlobalAlert("Tiada bajet untuk PTj " & ddlPTj.SelectedItem.Text & " dalam KW " & ddlKW.SelectedItem.Text & ".", Me.Page, Me.[GetType]())
                fClearDdlKO()
            End If
        End If

    End Sub

    Private Sub fGetBajetPTj(ByVal strKodPTj As String)
        Try
            Dim decJumBajetPTj, decJumAgihPTj As Decimal
            decJumBajetPTj = fGetJumBajetPTj(strKodPTj)
            decJumAgihPTj = fGetAgihanPTj(strKodPTj)

            txtJumBajetPTj.Text = FormatNumber(decJumBajetPTj, 2)
            txtJumAgihPTj.Text = FormatNumber(decJumAgihPTj, 2)
            hidAgihPTj.Value = FormatNumber(decJumAgihPTj, 2)

            txtJumBakiPTj.Text = FormatNumber(decJumBajetPTj - decJumAgihPTj, 2)

        Catch ex As Exception

        End Try
    End Sub
    Private Function fGetJumBajetPTj(ByVal strKodPTj As String) As Decimal
        Try

            Dim strSql As String
            strSql = "SELECT sum(bg05_amaun) as JumBajet FROM BG05_AgihPTJ " &
                "WHERE BG05_Tahun='" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' and kodkw = '" & ddlKW.SelectedValue & "' and kodptj = '" & strKodPTj & "' and kodagih='AL' and BG05_Status = 1"

            Dim decJumBajet As Decimal = 0.00
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    decJumBajet = IIf(IsDBNull(ds.Tables(0).Rows(0)("JumBajet")), 0.00, ds.Tables(0).Rows(0)("JumBajet"))
                End If
            End If

            Return decJumBajet
        Catch ex As Exception

        End Try
    End Function

    Private Function fGetAgihanPTj(ByVal strKodPTj As String) As Decimal
        Try
            Dim strSql As String
            strSql = "select sum(BG06_Amaun ) as JumAgih from BG06_AgihObjAm where BG06_Tahun = '" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' and KodKw = '" & Trim(ddlKW.SelectedValue.TrimEnd) & "' and KodPTJ = '" & strKodPTj & "' and KodAgih = 'AL' and BG06_Status = 1"

            Dim decJumAgih As Decimal = 0.00
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    decJumAgih = IIf(IsDBNull(ds.Tables(0).Rows(0)("JumAgih")), 0.00, ds.Tables(0).Rows(0)("JumAgih"))
                End If
            End If

            Return decJumAgih
        Catch ex As Exception

        End Try
    End Function

    Private Sub fBindDdlKO()
        Try
            Dim strSql As String = "SELECT DISTINCT TOP 100 PERCENT dbo.MK_kodOperasi.KodKO, (dbo.MK_kodOperasi.KodKO + ' - ' + dbo.MK_kodOperasi.Butiran) as Butiran " &
                "FROM dbo.MK_kodOperasi INNER JOIN dbo.MK01_VotTahun ON dbo.MK_kodOperasi.KodKO = dbo.MK01_VotTahun.KodKO " &
                "where dbo.MK01_VotTahun.MK01_Tahun = '" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' and dbo.mk01_votTahun.KodKw = '" & ddlKW.SelectedValue & "' and dbo.mk01_votTahun.KodPTj = '" & ddlPTj.SelectedValue & "' and MK_KodOperasi .Status = 1 ORDER BY dbo.MK_kodOperasi.Kodko"

            'strSql = "SELECT DISTINCT TOP 100 PERCENT dbo.mk_kodOperasi.KodKO, (dbo.mk_kodOperasi.KodKO + ' - ' + dbo.mk_kodOperasi.Butiran) as Butiran FROM dbo.mk_kodOperasi INNER JOIN dbo.MK01_VotTahun ON dbo.mk_kodOperasi.KodKO = dbo.MK01_VotTahun.kodko where dbo.MK01_VotTahun.MK01_Tahun = '2018' and MK01_VotTahun.KodKw = '05' ORDER BY dbo.mk_kodOperasi.KodKO"


            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable

            ds = dbconn.fselectCommand(strSql)
            ddlKO.DataSource = ds
            ddlKO.DataTextField = "Butiran"
            ddlKO.DataValueField = "KodKO"
            ddlKO.DataBind()

            ddlKO.Items.Insert(0, "- SILA PILIH -")
            ddlKO.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlKO_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKO.SelectedIndexChanged
        sClear()
        If ddlPTj.SelectedIndex = 0 Then
            fClearDdlKP()
        Else
            fBindDdlKP()
        End If
    End Sub

    Private Sub sClear()
        Try

            gvObjAm.DataSource = New List(Of String)
            gvObjAm.DataBind()
            lblJumRekod.InnerText = ""
            txtJumBajetKp.Text = ""
            txtTBKp.Text = ""
            txtKGKp.Text = ""
            txtBFKp.Text = ""
            txtAgihanKp.Text = ""
            txtBakiKp.Text = ""
            txtJumKp.Text = ""
            txtJumBajetKp.Enabled = False

        Catch ex As Exception

        End Try
    End Sub
    Private Sub fClearDdlKO()
        Try
            Me.ddlKO.Items.Clear()
            ddlKO.Items.Add(New ListItem("- SILA PILIH PTj -", 0))
        Catch ex As Exception

        End Try
    End Sub
    Private Sub fClearDdlKP()
        Try
            Me.ddlKp.Items.Clear()
            ddlKp.Items.Add(New ListItem("- SILA PILIH KO -", 0))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindDdlKP()
        Try
            Dim strSql As String

            strSql = "SELECT DISTINCT TOP 100 PERCENT dbo.MK_KodProjek .KodProjek , (dbo.MK_KodProjek .KodProjek  + ' - ' + dbo.MK_KodProjek.Butiran ) as Butiran  " &
                    "From dbo.MK_KodProjek INNER Join dbo.MK01_VotTahun ON dbo.MK_KodProjek.KodProjek  = dbo.MK01_VotTahun.KodKP   " &
                    "where dbo.MK01_VotTahun.MK01_Tahun = '" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' and dbo.mk01_votTahun.KodKw = '" & ddlKW.SelectedValue & "' and MK01_VotTahun .KodPTJ = '" & ddlPTj.SelectedValue & "'  ORDER BY dbo.MK_KodProjek.KodProjek"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable

            ds = dbconn.fselectCommand(strSql)
            ddlKp.DataSource = ds
            ddlKp.DataTextField = "Butiran"
            ddlKp.DataValueField = "KodProjek"
            ddlKp.DataBind()

            ddlKp.Items.Insert(0, "- SILA PILIH -")
            ddlKp.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlKp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKp.SelectedIndexChanged

        If ddlKp.SelectedIndex <> 0 Then
            fBindGvObjAm()
        Else
            sClearObjAm()
            sClearField()
        End If
    End Sub

    Private Sub fBindGvObjAm()

        Dim blnFound As Boolean = True

        Dim decJumAsal, decJumTamb, decJumKurang, decJumBF, decJumBajet As Decimal

        decJumAsal = fGetJumBajet() 'Asal
        decJumTamb = fGetJumTB() 'Tambahan
        decJumKurang = fGetJumKG() 'Kurangan
        decJumBF = fGetJumBF() 'Baki BF
        decJumBajet = decJumAsal + decJumTamb + decJumBF - decJumKurang 'Jumlah Bajet


        txtJumBajetKp.Text = FormatNumber(decJumAsal) 'Asal
        hidBajetKp.Value = FormatNumber(decJumAsal) 'Hidden Asal
        txtTBKp.Text = FormatNumber(decJumTamb) 'Tambahan
        txtBFKp.Text = FormatNumber(decJumBF) 'Baki BF
        txtKGKp.Text = FormatNumber(decJumKurang) 'Kurangan
        txtJumKp.Text = FormatNumber(decJumBajet) 'Jumlah bajet

        sClearObjAm()
        Dim strSql As String
        Dim decJumAgihan As Decimal
        Dim ds As New DataSet
        Dim dbconn As New DBKewConn
        Try
            'strSql = "select a.KodVot, (select b.Butiran from MK_Vot b where b.KodVot = a.kodvot) as Butiran from bg_setupobjam a " &
            '    "where a.kodkw='" & ddlKW.SelectedValue & "' order by a.kodvot"

            '            strSql = "SELECT DISTINCT TOP 100 PERCENT dbo.MK_Vot.KodVot,  dbo.MK_Vot.Butiran 
            'FROM dbo.MK_Vot 
            'INNER JOIN dbo.MK01_VotTahun ON dbo.MK_Vot.KodVot = dbo.MK01_VotTahun.KodVot 
            'WHERE (dbo.MK_Vot.Klasifikasi = 'H2') AND (dbo.MK01_VotTahun.MK01_Status = '1') and dbo.MK01_VotTahun.mk01_tahun = '" & Trim(txtTahun.Text.TrimEnd) & "' 
            'and dbo.MK01_VotTahun.kodkw ='" & Trim(ddlKW.SelectedValue.TrimEnd) & "'  and dbo.MK01_VotTahun.kodptj='" & Trim(ddlPTj.SelectedValue.TrimEnd) & "' 
            'And dbo.MK01_VotTahun.KodKO ='" & Trim(ddlKO.SelectedValue.TrimEnd) & "' and dbo.MK01_VotTahun.KodKP  ='" & Trim(ddlKp.SelectedValue.TrimEnd) & "' 
            'and left(dbo.MK_Vot.kodvot,1) = '1' ORDER BY dbo.MK_Vot.KodVot"

            strSql = "select distinct a.KodVot,(select butiran from MK_Vot b where b.KodVot = a.KodVot) as Butiran from MK01_VotTahun a where a.MK01_Tahun = '" & (ddlTahunAgih.SelectedValue.TrimEnd) & "' and a.KodKw = '" & Trim(ddlKW.SelectedValue.TrimEnd) & "' and a.KodKO  = '" & Trim(ddlKO.SelectedValue.TrimEnd) & "' and a.KodPTJ = '" & Trim(ddlPTj.SelectedValue.TrimEnd) & "' and a.KodKP = '" & Trim(ddlKp.SelectedValue.TrimEnd) & "' and RIGHT (a.kodvot,4) = '0000'
and a.kodvot in (select kodvot from bg_setupobjam where KodKw = '" & Trim(ddlKW.SelectedValue.TrimEnd) & "' and Status = 1)
order by a.KodVot "

            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then

                    Dim dt As New DataTable
                    dt.Columns.Add("ObjAm", GetType(String))
                    dt.Columns.Add("Bajet", GetType(String))
                    dt.Columns.Add("TB", GetType(String))
                    dt.Columns.Add("KG", GetType(String))
                    dt.Columns.Add("BakiBF", GetType(String))
                    dt.Columns.Add("Jumlah", GetType(String))

                    Dim strKodVot, strObjAm, strJumBajetAm, strJumTBAm, strJumKGAm, strJumBFAm, strJumObjAm As String
                    Dim decJumBajetAm, decJumTBAm, decJumKGAm, decJumBFAm, decJumObjAm As Decimal

                    Dim strKodKW, strKodPTj As String

                    strKodKW = ddlKW.SelectedValue
                    strKodPTj = ddlPTj.SelectedValue

                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        strKodVot = Trim(ds.Tables(0).Rows(i)("kodvot")).TrimEnd
                        strObjAm = strKodVot & " - " & ds.Tables(0).Rows(i)("Butiran")

                        decJumBajetAm = fGetJumBajetAm(strKodVot)
                        strJumBajetAm = FormatNumber(decJumBajetAm)
                        decJumTBAm = fGetJumTBAm(strKodVot)
                        strJumTBAm = FormatNumber(decJumTBAm)
                        decJumKGAm = fGetJumKGAm(strKodVot)
                        strJumKGAm = FormatNumber(decJumKGAm)
                        decJumBFAm = fGetJumBFAm(strKodVot)
                        strJumBFAm = FormatNumber(decJumBFAm)

                        decJumObjAm = decJumBajetAm + decJumTBAm + decJumBFAm - decJumKGAm
                        strJumObjAm = FormatNumber(decJumObjAm)

                        dt.Rows.Add(strObjAm, strJumBajetAm, strJumTBAm, strJumKGAm, strJumBFAm, strJumObjAm)

                        decJumAgihan += decJumObjAm
                    Next

                    lblJumRekod.InnerText = dt.Rows.Count
                    gvObjAm.DataSource = dt
                    gvObjAm.DataBind()
                Else
                    blnFound = False
                End If
            Else
                blnFound = False
            End If

            If blnFound = False Then
                txtJumBajetKp.Enabled = False
                fGlobalAlert("Carta akaun belum dibina!", Me.Page, Me.[GetType]())
                Exit Sub
            Else
                txtJumBajetKp.Enabled = True
            End If

            txtAgihanKp.Text = FormatNumber(decJumAgihan)
            txtBakiKp.Text = decJumBajet - decJumAgihan

        Catch ex As Exception

        End Try

    End Sub

    Private Function fGetJumBajetAm(ByVal strKodVot As String) As Decimal
        Try
            Dim strSql As String
            strSql = "SELECT sum(bg06_amaun) as JumObjAm FROM BG06_AgihObjAm WHERE BG06_Tahun='" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' " &
                "and kodkw = '" & ddlKW.SelectedValue & "' and KodKO = '" & ddlKO.SelectedValue & "' and kodptj = '" & ddlPTj.SelectedValue & "' and KodKP = '" & ddlKp.SelectedValue & "' and kodvot='" & strKodVot & "' and kodagih='AL'"

            Dim strJumObjAm As Decimal = 0.00
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strJumObjAm = IIf(IsDBNull(ds.Tables(0).Rows(0)("JumObjAm")), 0.00, ds.Tables(0).Rows(0)("JumObjAm"))
                End If
            End If

            Return strJumObjAm
        Catch ex As Exception

        End Try
    End Function

    Private Function fGetJumTBAm(ByVal strKodVot As String) As Decimal
        Try
            Dim strSql As String
            strSql = "SELECT SUM(dbo.BG07_AgihObjSbg.BG07_Amaun) AS JumTB FROM dbo.BG06_AgihObjAm " &
            "INNER JOIN dbo.BG07_AgihObjSbg ON dbo.BG06_AgihObjAm.BG06_IndObjAm = dbo.BG07_AgihObjSbg.BG06_IndObjAm " &
            "WHERE (dbo.BG06_AgihObjAm.BG06_Tahun = '" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "') AND (dbo.BG06_AgihObjAm.KodKw = '" & ddlKW.SelectedValue & "') AND (dbo.BG06_AgihObjAm.KodKO  = '" & ddlKO.SelectedValue & "') AND (dbo.BG06_AgihObjAm.KodPTJ = '" & ddlPTj.SelectedValue & "') " &
            "And (dbo.BG06_AgihObjAm.KodKP  = '" & ddlKp.SelectedValue & "') And (dbo.BG06_AgihObjAm.KodVot = '" & strKodVot & "') AND (dbo.BG06_AgihObjAm.KodAgih = 'TB') AND (dbo.BG06_AgihObjAm.BG06_Status = '1') AND (dbo.BG07_AgihObjSbg.BG07_StatLulus = 1)"

            Dim strJumTB As Decimal = 0.00
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strJumTB = IIf(IsDBNull(ds.Tables(0).Rows(0)("JumTB")), 0.00, ds.Tables(0).Rows(0)("JumTB"))
                End If
            End If

            Return strJumTB
        Catch ex As Exception

        End Try
    End Function

    Private Function fGetJumKGAm(ByVal strKodVot As String) As Decimal
        Try
            Dim strSql As String

            strSql = "SELECT SUM(dbo.BG07_AgihObjSbg.BG07_Amaun) AS JumKG FROM dbo.BG06_AgihObjAm " &
            "INNER JOIN dbo.BG07_AgihObjSbg ON dbo.BG06_AgihObjAm.BG06_IndObjAm = dbo.BG07_AgihObjSbg.BG06_IndObjAm " &
            "WHERE (dbo.BG06_AgihObjAm.BG06_Tahun = '" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "') AND (dbo.BG06_AgihObjAm.KodKw = '" & ddlKW.SelectedValue & "') AND (dbo.BG06_AgihObjAm.KodKO  = '" & ddlKO.SelectedValue & "') AND (dbo.BG06_AgihObjAm.KodPTJ = '" & ddlPTj.SelectedValue & "') " &
            "And (dbo.BG06_AgihObjAm.KodKP  = '" & ddlKp.SelectedValue & "') And (dbo.BG06_AgihObjAm.KodVot = '" & strKodVot & "') AND (dbo.BG06_AgihObjAm.KodAgih = 'KG') AND (dbo.BG06_AgihObjAm.BG06_Status = '1') AND (dbo.BG07_AgihObjSbg.BG07_StatLulus = 1)"

            Dim strJumKG As Decimal = 0.00
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strJumKG = IIf(IsDBNull(ds.Tables(0).Rows(0)("JumKG")), 0.00, ds.Tables(0).Rows(0)("JumKG"))
                End If
            End If

            Return strJumKG
        Catch ex As Exception

        End Try
    End Function

    Private Function fGetJumBFAm(ByVal strKodVot As String) As Decimal
        Try
            Dim strSql As String

            strKodVot = strKodVot.Substring(0, 1)


            strSql = "SELECT sum(mk09_debit) as JumBF FROM mk09_bajetbf WHERE mk09_Tahun='" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' " &
                "And kodkw='" & ddlKW.SelectedValue & "' AND KodKO  = '" & ddlKO.SelectedValue & "' and kodptj='" & ddlPTj.SelectedValue & "' and KodKP = '" & ddlKp.SelectedValue & "' and left(kodvot,1)='" & strKodVot & "'"

            Dim strJumKG As Decimal = 0.00
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strJumKG = IIf(IsDBNull(ds.Tables(0).Rows(0)("JumBF")), 0.00, ds.Tables(0).Rows(0)("JumBF"))
                End If
            End If

            Return strJumKG
        Catch ex As Exception

        End Try
    End Function

    Private Function fGetJumBajet() As Decimal
        Try

            Dim strSql As String
            strSql = "select sum(BG06_Amaun ) as JumBajetKp from BG06_AgihObjAm " &
            "where BG06_Tahun = '" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' and KodKw = '" & ddlKW.SelectedValue & "' and kodko = '" & ddlKO.SelectedValue & "' and KodPTJ = '" & ddlPTj.SelectedValue & "' and KodKP = '" & ddlKp.SelectedValue & "'  and KodAgih = 'AL' and BG06_Status  = 1"

            Dim strJumBajet As Decimal = 0.00
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strJumBajet = IIf(IsDBNull(ds.Tables(0).Rows(0)("JumBajetKp")), 0.00, ds.Tables(0).Rows(0)("JumBajetKp"))
                End If
            End If

            Return strJumBajet
        Catch ex As Exception

        End Try
    End Function

    Private Function fGetJumTB() As Decimal
        Try
            Dim strSql As String
            strSql = "SELECT SUM(dbo.BG07_AgihObjSbg.BG07_Amaun) AS JumTB FROM dbo.BG05_AgihPTJ " &
                "INNER JOIN dbo.BG06_AgihObjAm ON dbo.BG05_AgihPTJ.BG05_IndPTJ = dbo.BG06_AgihObjAm.BG05_IndPTJ " &
            "INNER JOIN dbo.BG07_AgihObjSbg ON dbo.BG06_AgihObjAm.BG06_IndObjAm = dbo.BG07_AgihObjSbg.BG06_IndObjAm " &
            "WHERE (dbo.BG05_AgihPTJ.KodKw = '" & ddlKW.SelectedValue & "') AND (dbo.BG05_AgihPTJ.kodKO='" & ddlKO.SelectedValue & "') AND (dbo.BG05_AgihPTJ.KodAgih = 'TB') AND (dbo.BG07_AgihObjSbg.BG07_StatLulus = '1') AND " &
            "(dbo.BG05_AgihPTJ.BG05_Status = '1') and (dbo.BG05_AgihPTJ.BG05_Tahun = '" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "') and dbo.BG05_AgihPTJ.kodptj='" & ddlPTj.SelectedValue & "' "

            Dim strJumTB As Decimal = 0.00
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strJumTB = IIf(IsDBNull(ds.Tables(0).Rows(0)("JumTB")), 0.00, ds.Tables(0).Rows(0)("JumTB"))
                End If
            End If

            Return strJumTB
        Catch ex As Exception

        End Try
    End Function

    Private Function fGetJumKG() As Decimal
        Try
            Dim strSql As String

            strSql = "SELECT SUM(dbo.BG07_AgihObjSbg.BG07_Amaun) AS JumKG FROM dbo.BG05_AgihPTJ " &
            "INNER JOIN dbo.BG06_AgihObjAm ON dbo.BG05_AgihPTJ.BG05_IndPTJ = dbo.BG06_AgihObjAm.BG05_IndPTJ " &
            "INNER JOIN dbo.BG07_AgihObjSbg ON dbo.BG06_AgihObjAm.BG06_IndObjAm = dbo.BG07_AgihObjSbg.BG06_IndObjAm " &
            "WHERE(dbo.BG05_AgihPTJ.KodKw = '" & ddlKW.SelectedValue & "') AND (dbo.BG05_AgihPTJ.kodKO='02') AND (dbo.BG05_AgihPTJ.KodAgih = 'KG') AND (dbo.BG07_AgihObjSbg.BG07_StatLulus = '1') AND (dbo.BG05_AgihPTJ.BG05_Status = '1') " &
            "And (dbo.BG05_AgihPTJ.BG05_Tahun = '" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "') and dbo.BG05_AgihPTJ.kodptj='" & ddlPTj.SelectedValue & "' "

            Dim strJumKG As Decimal = 0.00
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strJumKG = IIf(IsDBNull(ds.Tables(0).Rows(0)("JumKG")), 0.00, ds.Tables(0).Rows(0)("JumKG"))
                End If
            End If

            Return strJumKG
        Catch ex As Exception

        End Try
    End Function

    Private Function fGetJumBF() As Decimal
        Try
            Dim strSql As String
            strSql = "SELECT sum(mk09_debit) as JumBF FROM mk09_bajetbf WHERE mk09_Tahun='" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' " &
                "And kodkw='" & ddlKW.SelectedValue & "' and KodKO = '" & ddlKO.SelectedValue & "' and kodptj='" & ddlPTj.SelectedValue & "'"

            Dim strJumKG As Decimal = 0.00
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strJumKG = IIf(IsDBNull(ds.Tables(0).Rows(0)("JumBF")), 0.00, ds.Tables(0).Rows(0)("JumBF"))
                End If
            End If

            Return strJumKG
        Catch ex As Exception

        End Try
    End Function

    Private Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.Click

        Dim footerRow = gvObjAm.FooterRow
        Dim decTotAgihAm As Decimal = CDec(CType(footerRow.FindControl("lblTotBajet"), Label).Text)

        If decTotAgihAm <> CDec(txtJumBajetKp.Text) Then
            fGlobalAlert("Jumlah agihan kepada Objek Am tidak sama dengan jumlah bajet untuk kod projek ini!", Me.Page, Me.[GetType]())
            Exit Sub
        End If

        If fSimpan() Then
            fGlobalAlert("Maklumat agihan telah disimpan!", Me.Page, Me.[GetType]())
            fGetBajetPTj(ddlPTj.SelectedValue)
        Else
            fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
        End If

    End Sub

    Private Function fSimpan() As Boolean
        Try
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim blnSuccess As Boolean = True
            Dim strSql As String
            Dim strIndPTj As String

            Try
                Dim strDateNow As String = Now.ToString("yyyy-MM-dd")
                Dim strTahun As String = Trim(ddlTahunAgih.SelectedValue.TrimEnd)
                Dim strKodKW As String = Trim(ddlKW.SelectedValue.TrimEnd)
                Dim strkodKO As String = Trim(ddlKO.SelectedValue.TrimEnd)
                Dim strKodPTj As String = Trim(ddlPTj.SelectedValue.TrimEnd)
                Dim strkodKP As String = Trim(ddlKp.SelectedValue.TrimEnd)
                Dim strKodAgih = "AL"


                dbconn.sConnBeginTrans()

                strSql = "select BG05_IndPTJ  from BG05_AgihPTJ with (nolock)  where BG05_Tahun = '" & strTahun & "' and KodKw = '" & strKodKW & "' and KodPTJ = '" & strKodPTj & "' and KodAgih = 'AL'"
                strIndPTj = fGetIndPTj(strSql)

                If gvObjAm.Rows.Count > 0 Then
                    For Each row As GridViewRow In gvObjAm.Rows
                        Dim strIndObjAM As String
                        Dim strAmtObjAm As Decimal = CDec(TryCast(row.FindControl("txtBajet"), TextBox).Text)
                        Dim strObjAm As String = TryCast(row.FindControl("ObjAm"), Label).Text.Substring(0, 5).TrimEnd

                        strSql = "select count(*) from BG06_AgihObjAm WITH (NOLOCK) where BG06_Tahun = '" & strTahun & "' and KodKw = '" & strKodKW & "' " &
                            "And KodKO = '" & strkodKO & "' and KodPTJ = '" & strKodPTj & "' and KodKP = '" & strkodKP & "' and KodAgih = 'AL' and KodVot = '" & strObjAm & "'"
                        If fCheckRec(strSql) > 0 Then
                            'UPDATE
                            strSql = "update BG06_AgihObjAm set BG06_Amaun = @Amaun, BG06_TkhAgih = @TkhAgih  where BG06_Tahun = @Tahun and KodKw = @KodKW " &
                            "And KodKO = @KodKO and KodPTJ = @KodPTj and KodKP = @KodKP and KodAgih = @KodAgih and KodVot = @KodVot"

                            Dim paramSql2() As SqlParameter = {
                                    New SqlParameter("@Amaun", strAmtObjAm),
                                    New SqlParameter("@TkhAgih", strDateNow),
                                    New SqlParameter("@Tahun", strTahun),
                                    New SqlParameter("@KodKW", strKodKW),
                                    New SqlParameter("@KodKO", strkodKO),
                                    New SqlParameter("@KodPTj", strKodPTj),
                                    New SqlParameter("@KodKP", strkodKP),
                                    New SqlParameter("@KodAgih", strKodAgih),
                                    New SqlParameter("@KodVot", strObjAm)
                                    }

                            If Not dbconn.fUpdateCommand(strSql, paramSql2) > 0 Then
                                blnSuccess = False
                                Exit Try
                            Else
                                'AUDIT LOG
                                sLogMedan = "BG06_Amaun|BG06_TkhAgih|BG06_Tahun|KodKw|KodKO|KodPTJ|KodKP|KodVot|KodAgih"
                                sLogBaru = strAmtObjAm & "|" & strDateNow & "|" & strTahun & "|" & strKodKW & "|" & strkodKO & "|" & strKodPTj & "|" & strkodKP & "|" & strObjAm & "|" & strKodAgih

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
                                    New SqlParameter("@InfoTable", "BG06_AgihObjAm"),
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

                        Else
                            'INSERT
                            strIndObjAM = fGetNoSiri("OBJAM", strTahun)

                            strSql = "insert into BG06_AgihObjAm (BG05_IndPTJ , BG06_IndObjAm , BG06_Tahun , KodKw , KodKO , KodPTJ ,KodKP, KodVot, BG06_Amaun ,KodAgih ,BG06_TkhAgih ,BG06_Status) " &
                                        "values (@IndPTj, @IndObjAm, @Tahun, @KodKW, @KodKO, @KodPTj, @KodKP, @KodVot, @Amaun, @KodAgih,@TkhAgih, @Status)"

                            Dim paramSql3() As SqlParameter = {
                                New SqlParameter("@IndPTj", strIndPTj),
                                New SqlParameter("@IndObjAm", strIndObjAM),
                                New SqlParameter("@Tahun", strTahun),
                                New SqlParameter("@KodKW", strKodKW),
                                New SqlParameter("@KodKO", strkodKO),
                                New SqlParameter("@KodPTj", strKodPTj),
                                New SqlParameter("@KodKP", strkodKP),
                                New SqlParameter("@KodVot", strObjAm),
                                New SqlParameter("@Amaun", strAmtObjAm),
                                New SqlParameter("@KodAgih", strKodAgih),
                                New SqlParameter("@TkhAgih", strDateNow),
                                New SqlParameter("@Status", "1")}

                            If Not dbconn.fInsertCommand(strSql, paramSql3) > 0 Then
                                blnSuccess = False
                                Exit Try
                            Else
                                'AUDIT LOG
                                sLogMedan = "BG05_IndPTJ|BG06_IndObjAm|BG06_Tahun|KodKw|KodKO|KodPTJ|KodKP|KodVot|BG06_Amaun|KodAgih|BG06_TkhAgih|BG06_Status"
                                sLogBaru = strIndPTj & "|" & strIndObjAM & "|" & strTahun & "|" & strKodKW & "|" & strkodKO & "|" & strKodPTj & "|" & strkodKP & "|" & strObjAm & "|" & strAmtObjAm & "|" & strKodAgih & "|" & strDateNow & "|1"

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
                                    New SqlParameter("@InfoTable", "BG06_AgihObjAm"),
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
                Else
                    Exit Function
                End If

            Catch ex As Exception
                blnSuccess = False
            End Try

            If blnSuccess = False Then
                dbconn.sConnRollbackTrans()
                Return False
            Else
                dbconn.sConnCommitTrans()
                Return True
            End If
        Catch ex As Exception

        End Try
    End Function

    Private Function fGetIndPTj(strSql) As String
        Try
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Return ds.Tables(0).Rows(0).Item(0).ToString
                End If
            End If
        Catch ex As Exception

        End Try
    End Function

    Dim decTotBajet, decTotTamb, decTotKurg, decTotBaki, decTotJum As Decimal
    Dim strTotBajet, strTotTamb, strTotKurg, strTotBaki, strTotJum As String
    Private Sub gvObjAm_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvObjAm.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim txtBajet As TextBox = CType(e.Row.FindControl("txtBajet"), TextBox)
                decTotBajet += CDec(txtBajet.Text)

                Dim lblTamb As Label = CType(e.Row.FindControl("lblTB"), Label)
                decTotTamb += Decimal.Parse(lblTamb.Text)

                Dim lblKurg As Label = CType(e.Row.FindControl("lblKG"), Label)
                decTotKurg += Decimal.Parse(lblKurg.Text)

                Dim lblBaki As Label = CType(e.Row.FindControl("lblBakiBF"), Label)
                decTotBaki += Decimal.Parse(lblBaki.Text)

                Dim lblJum As Label = CType(e.Row.FindControl("lblJum"), Label)
                decTotJum += Decimal.Parse(lblJum.Text)

            End If

            If e.Row.RowType = DataControlRowType.Footer Then

                Dim lblTotBajet As Label = CType(e.Row.FindControl("lblTotBajet"), Label)
                lblTotBajet.Text = FormatNumber(decTotBajet)
                lblTotBajet.Font.Bold = True

                Dim lblTotTamb As Label = CType(e.Row.FindControl("lblTotTB"), Label)
                lblTotTamb.Text = FormatNumber(decTotTamb)
                lblTotTamb.Font.Bold = True

                Dim lblTotKurg As Label = CType(e.Row.FindControl("lblTotKG"), Label)
                lblTotKurg.Text = FormatNumber(decTotKurg) 'strTotKurg.ToString()
                lblTotKurg.Font.Bold = True

                Dim lblTotBaki As Label = CType(e.Row.FindControl("lblTotBakiBF"), Label)
                lblTotBaki.Text = FormatNumber(decTotBaki)
                lblTotBaki.Font.Bold = True

                Dim lblTotJum As Label = CType(e.Row.FindControl("lblTotJum"), Label)
                lblTotJum.Text = FormatNumber(decTotJum)
                lblTotJum.Font.Bold = True
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtJumBajetKp_TextChanged(sender As Object, e As EventArgs) Handles txtJumBajetKp.TextChanged
        Try

            If txtJumBajetKp.Text = "" Then txtJumBajetKp.Text = 0

            Dim decBajetKP As Decimal = CDec(txtJumBajetKp.Text)
            Dim decJumBajetPTj As Decimal = CDec(txtJumBajetPTj.Text)
            Dim decJumAgihPTj As Decimal = CDec(txtJumAgihPTj.Text)
            Dim decBakiAgihKP As Decimal = CDec(txtAgihanKp.Text)

            If decBajetKP > decJumBajetPTj Then
                fGlobalAlert("Amaun yang dimasukkan melebihi jumlah bajet KW untuk PTj ini! Sila masukkan amaun lain.", Me.Page, Me.[GetType]())
                txtJumBajetKp.Text = "0.00"
                txtJumKp.Text = "0.00"
                txtBakiKp.Text = "0.00"
                txtJumBajetKp.Focus()
                Exit Sub
            End If

            Dim decAslBajetKP As Decimal = CDec(hidBajetKp.Value)

            If (decJumAgihPTj - decAslBajetKP + decBajetKP) > decJumBajetPTj Then
                fGlobalAlert("Amaun yang dimasukkan melebihi jumlah bajet KW untuk PTj ini! Sila masukkan amaun lain.", Me.Page, Me.[GetType]())
                txtJumBajetKp.Text = FormatNumber(decAslBajetKP)
                txtJumKp.Text = FormatNumber(decAslBajetKP)
                txtBakiKp.Text = FormatNumber(decAslBajetKP - decBakiAgihKP)
                txtJumBajetKp.Focus()
                Exit Sub
            End If

            Dim decHidBajetKP As Decimal = CDec(hidBajetKp.Value)
            Dim decHidBajetPTj As Decimal = CDec(hidAgihPTj.Value)

            txtJumKp.Text = FormatNumber(decBajetKP)
            txtJumBajetKp.Text = FormatNumber(decBajetKP)


            Dim decBakiKP As Decimal = decBajetKP - decBakiAgihKP
            txtBakiKp.Text = FormatNumber(decBakiKP)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txtBajet_TextChanged(sender As Object, e As EventArgs)

        Try
            Dim footerRow = gvObjAm.FooterRow

            Dim txtBajet As TextBox = CType(sender, TextBox)
            Dim gvr As GridViewRow = CType(txtBajet.Parent.Parent, GridViewRow)
            Dim rowindex As Integer = gvr.RowIndex
            Dim selectedRow As GridViewRow = gvObjAm.Rows(gvr.RowIndex)

            Dim lblJum As Label = CType(selectedRow.FindControl("lblJum"), Label)

            If txtBajet.Text = "" Then txtBajet.Text = "0.00"
            Dim decBajet As Decimal = CDec(txtBajet.Text)
            txtBajet.Text = FormatNumber(decBajet)

            'Semak jumlah besar
            Dim decJumBajetAm As Decimal
            Dim decBajetAm As Decimal
            For Each gvRow As GridViewRow In gvObjAm.Rows
                decBajetAm = Trim(CType(gvRow.FindControl("txtBajet"), TextBox).Text.TrimEnd)
                decJumBajetAm += decBajetAm 'Total Jumlah Bajet Objek Am
            Next

            Dim decTotJum As Decimal
            If decJumBajetAm > CDec(txtJumBajetKp.Text) Then
                fGlobalAlert("Amaun yang dimasukkan melebihi jumlah bajet Objek Am ini! Sila masukkan amaun lain.", Me.Page, Me.[GetType]())
                txtBajet.Text = "0.00"
            End If

            decJumBajetAm = 0
            'Kira Jumlah Besar PTj
            Dim decTbPTj As Decimal = Trim(CType(selectedRow.FindControl("lblTB"), Label).Text.TrimEnd) 'amaun Tambahan
            Dim decKgPTj As Decimal = Trim(CType(selectedRow.FindControl("lblKG"), Label).Text.TrimEnd) 'amaun Kurangan
            Dim decBakiBfPTj As Decimal = Trim(CType(selectedRow.FindControl("lblBakiBF"), Label).Text.TrimEnd) 'amaun Baki BF

            Dim decJumPTj As Decimal = CDec(txtBajet.Text) + decTbPTj - decKgPTj + decBakiBfPTj
            lblJum.Text = FormatNumber(decJumPTj)

            'Kira Jumlah Besar
            For Each gvRow As GridViewRow In gvObjAm.Rows
                decBajetAm = Trim(CType(gvRow.FindControl("txtBajet"), TextBox).Text.TrimEnd)
                decJumBajetAm += decBajetAm 'Total Jumlah Bajet

                Dim decJumlah As Decimal = Trim(CType(gvRow.FindControl("lblJum"), Label).Text.TrimEnd)
                decTotJum += decJumlah 'Total Jumlah Besar
            Next

            Dim decBajetKP As Decimal = CDec(txtJumBajetKp.Text)
            txtAgihanKp.Text = FormatNumber(decJumBajetAm)
            txtBakiKp.Text = FormatNumber(decBajetKP - decJumBajetAm)

            CType(footerRow.FindControl("lblTotBajet"), Label).Text = FormatNumber(decJumBajetAm) 'Total Jumlah Besar Bajet
            CType(footerRow.FindControl("lblTotJum"), Label).Text = FormatNumber(decTotJum) 'Total Jumlah Besar

        Catch ex As Exception

        End Try



    End Sub

    Private Sub ddlTahunAgih_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTahunAgih.SelectedIndexChanged
        fBindDdlKW()

        ddlPTj.Items.Add(New ListItem("- SILA PILIH KW -", 0))
        ddlKO.Items.Add(New ListItem("- SILA PILIH PTj -", 0))
        ddlKp.Items.Add(New ListItem("- SILA PILIH KO -", 0))
        sClearObjAm()
        txtJumBajetKp.Enabled = False
    End Sub
End Class