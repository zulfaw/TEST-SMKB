Imports System.Drawing
Imports System.Data.SqlClient


Public Class Pengagihan_Pej_Bendahari
    Inherits System.Web.UI.Page

    Dim GrandBajet As Decimal = 0.00
    Dim GrandTambahan As Decimal = 0.00
    Dim GrandKurangan As Decimal = 0.00
    Dim GrandBaki As Decimal = 0.00
    Dim GrandJumlah As Decimal = 0.00
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

                ddlPTJ.Items.Add(New ListItem("- SILA PILIH KW -", 0))
                ddlKO.Items.Add(New ListItem("- SILA PILIH PTj -", 0))
                ddlKp.Items.Add(New ListItem("- SILA PILIH KO -", 0))

                gvObjAm.DataSource = New List(Of String)
                gvObjAm.DataBind()
                txtBajetPTj.Enabled = False

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
    Private Sub fBindDdlKW()
        Try

            Dim strSql As String = "SELECT DISTINCT TOP 100 PERCENT dbo.MK_Kw.KodKw, (dbo.MK_Kw.KodKw + ' - ' + dbo.MK_Kw.Butiran) as Butiran " &
                  "FROM dbo.MK_Kw INNER JOIN dbo.MK01_VotTahun ON dbo.MK_Kw.KodKw = dbo.MK01_VotTahun.KodKw " &
                    "where dbo.MK01_VotTahun.MK01_Tahun = '" & ddlTahunAgih.SelectedValue & "' ORDER BY dbo.MK_Kw.KodKw"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fselectCommand(strSql)
            ddlKW.DataSource = ds

            ddlKW.DataTextField = "Butiran"
            ddlKW.DataValueField = "KodKW"
            ddlKW.DataBind()

            ddlKW.Items.Insert(0, "- SILA PILIH KW -")
            ddlKW.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub
    Private Sub fBindDdlPTJ()
        Try
            Dim strSql As String

            strSql = "SELECT DISTINCT TOP 100 PERCENT dbo.MK_PTJ.KodPTJ,(MK_PTJ.KodPTJ + ' - ' + dbo.MK_PTJ.Butiran) as Butiran " &
"FROM dbo.MK_PTJ INNER JOIN dbo.MK01_VotTahun ON dbo.MK_PTJ.KodPTJ = dbo.MK01_VotTahun.KodPTJ " &
"WHERE dbo.MK01_VotTahun.mk01_tahun = '" & ddlTahunAgih.SelectedValue & "' and dbo.MK01_VotTahun.KodKw = '" & ddlKW.SelectedValue & "' order by KodPtj"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fselectCommand(strSql)
            ddlPTJ.DataSource = ds
            ddlPTJ.DataTextField = "Butiran"
            ddlPTJ.DataValueField = "KodPTJ"
            ddlPTJ.DataBind()

            ddlPTJ.Items.Insert(0, "- SILA PILIH -")
            ddlPTJ.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindDdlKO()
        Try
            Dim strSql As String = "SELECT DISTINCT TOP 100 PERCENT dbo.MK_kodOperasi.KodKO, (dbo.MK_kodOperasi.KodKO + ' - ' + dbo.MK_kodOperasi.Butiran) as Butiran " &
                "FROM dbo.MK_kodOperasi INNER JOIN dbo.MK01_VotTahun ON dbo.MK_kodOperasi.KodKO = dbo.MK01_VotTahun.KodKO " &
                "where dbo.MK01_VotTahun.MK01_Tahun = '" & ddlTahunAgih.SelectedValue & "' and dbo.mk01_votTahun.KodKw = '" & ddlKW.SelectedValue & "'  ORDER BY dbo.MK_kodOperasi.Kodko"

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

    Private Sub fBindDdlKP()
        Try
            Dim strSql As String

            strSql = "SELECT DISTINCT TOP 100 PERCENT dbo.MK_KodProjek .KodProjek , (dbo.MK_KodProjek .KodProjek  + ' - ' + dbo.MK_KodProjek.Butiran ) as Butiran  " &
                    "From dbo.MK_KodProjek INNER Join dbo.MK01_VotTahun ON dbo.MK_KodProjek.KodProjek  = dbo.MK01_VotTahun.KodKP   " &
                    "where dbo.MK01_VotTahun.MK01_Tahun = '" & ddlTahunAgih.SelectedValue & "' and dbo.mk01_votTahun.KodKw = '" & ddlKW.SelectedValue & "' and MK01_VotTahun .KodPTJ = '" & ddlPTJ.SelectedValue & "'  ORDER BY dbo.MK_KodProjek.KodProjek"

            'strSql = "SELECT DISTINCT TOP 100 PERCENT dbo.mk_kodOperasi.KodKO, (dbo.mk_kodOperasi.KodKO + ' - ' + dbo.mk_kodOperasi.Butiran) as Butiran FROM dbo.mk_kodOperasi INNER JOIN dbo.MK01_VotTahun ON dbo.mk_kodOperasi.KodKO = dbo.MK01_VotTahun.kodko where dbo.MK01_VotTahun.MK01_Tahun = '2018' and MK01_VotTahun.KodKw = '05' ORDER BY dbo.mk_kodOperasi.KodKO"


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



    Private Sub ddlKW_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKW.SelectedIndexChanged

        Try
            Dim strSql As String
            Dim strBajetKW As String
            Dim decBajetKW, decJumAsal, decJumTmbh, decJumBF, decJumKrg As Decimal

            hidTxtIndKW.Text = fGetIdxKW()

            'CARI BAJET KW
            decJumAsal = fGetJumAsalKW() 'BAJET ASAL KW
            decJumTmbh = fGetJumTBKW() 'BAJET TAMBAHAN KW
            decJumKrg = fGetJumKGKW() 'BAJET KURANGAN KW
            decJumBF = fGetJumBFKW() 'BAJET BF KW

            decBajetKW = decJumAsal + decJumTmbh + decJumBF - decJumKrg
            strBajetKW = decBajetKW.ToString("#,##0.00")
            txtBajetKW.Text = strBajetKW

            Dim decJumAsalP, decJumTbhP, decJumKrgP, decBajetBFP, decSumPTj, decBaki As Decimal
            Dim strSumPTj, strBaki As String

            'CARI BAJET PTJ
            decJumAsalP = fGetJumAsalPTj() 'BAJET ASAL KW
            decJumTbhP = fGetJumTBPTJ() 'BAJET TAMBAHAN KW
            decJumKrgP = fGetJumKGPTJ() 'BAJET KURANGAN KW
            decBajetBFP = fGetJumBFPTJ() 'BAJET BF KW

            decSumPTj = decJumAsalP + decJumTbhP + decBajetBFP - decJumKrgP
            strSumPTj = decSumPTj.ToString("#,##0.00")
            decBaki = decBajetKW - decSumPTj
            strBaki = decBaki.ToString("#,##0.00")
            txtAgihKW.Text = strSumPTj
            hidTxtAgihKW.Text = strSumPTj

            txtBakiKW.Text = strBaki

            fBindDdlPTJ()
            fClear()
            fClearDdlKO()


            'ScriptManager.RegisterStartupScript(Me.Page, Me.[GetType](), "", "fDisableBtn()", True)



        Catch ex As Exception

        End Try

    End Sub

    Private Function fGetIdxKW() As String
        Try
            Dim strSql As String

            strSql = "Select bg04_indkw from BG04_AgihKw  where bg04_tahun='" & ddlTahunAgih.SelectedValue & "' and kodkw = '" & ddlKW.SelectedValue & "' and kodagih='AL'"
            Dim strIdxKW As String
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strIdxKW = ds.Tables(0).Rows(0)("bg04_indkw")
                End If
            End If

            Return strIdxKW
        Catch ex As Exception

        End Try
    End Function

    ''' <summary>
    ''' 'BAJET ASAL KW
    ''' </summary>
    ''' <returns></returns>
    Private Function fGetJumAsalKW() As Decimal
        Try
            Dim strSql As String

            strSql = "SELECT sum(bg04_amaun) as JumAsalKW FROM BG04_AgihKw WHERE BG04_Tahun='" & ddlTahunAgih.SelectedValue & "' and kodagih='AL' and kodkw = '" & ddlKW.SelectedValue & "'"
            Dim strJumAsalKW As Decimal = 0.00
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strJumAsalKW = IIf(IsDBNull(ds.Tables(0).Rows(0)("JumAsalKW")), 0.00, ds.Tables(0).Rows(0)("JumAsalKW"))
                End If
            End If

            Return strJumAsalKW
        Catch ex As Exception

        End Try
    End Function

    ''' <summary>
    ''' 'BAJET TAMBAHAN KW
    ''' </summary>
    ''' <returns></returns>
    Private Function fGetJumTBKW() As Decimal
        Try
            Dim strSql As String
            strSql = "SELECT SUM(dbo.BG07_AgihObjSbg.BG07_Amaun) AS JumTBKW FROM dbo.BG04_AgihKw INNER JOIN dbo.BG05_AgihPTJ ON dbo.BG04_AgihKw.BG04_IndKw = dbo.BG05_AgihPTJ.BG04_IndKw " &
                    "INNER JOIN dbo.BG06_AgihObjAm ON dbo.BG05_AgihPTJ.BG05_IndPTJ = dbo.BG06_AgihObjAm.BG05_IndPTJ " &
                    "INNER JOIN dbo.BG07_AgihObjSbg ON dbo.BG06_AgihObjAm.BG06_IndObjAm = dbo.BG07_AgihObjSbg.BG06_IndObjAm " &
                    "WHERE (dbo.BG07_AgihObjSbg.BG07_StatLulus = '1') AND (dbo.BG04_AgihKw.BG04_Status = '1') AND (dbo.BG04_AgihKw.KodAgih = 'TB') " &
                    "AND(dbo.BG04_AgihKw.KodKw = '" & ddlKW.SelectedValue & "') and dbo.BG04_AgihKw.Bg04_Tahun='" & ddlTahunAgih.SelectedValue & "'"

            Dim strJumTBKW As Decimal = 0.00
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strJumTBKW = IIf(IsDBNull(ds.Tables(0).Rows(0)("JumTBKW")), 0.00, ds.Tables(0).Rows(0)("JumTBKW"))
                End If
            End If

            Return strJumTBKW
        Catch ex As Exception

        End Try
    End Function

    ''' <summary>
    ''' 'BAJET KURANGAN KW
    ''' </summary>
    ''' <returns></returns>
    Private Function fGetJumKGKW() As Decimal
        Try
            Dim strSql As String
            strSql = "SELECT SUM(dbo.BG07_AgihObjSbg.BG07_Amaun) AS JumKGKW FROM dbo.BG05_AgihPTJ INNER JOIN dbo.BG06_AgihObjAm ON dbo.BG05_AgihPTJ.BG05_IndPTJ = dbo.BG06_AgihObjAm.BG05_IndPTJ " &
                    "INNER JOIN dbo.BG07_AgihObjSbg ON dbo.BG06_AgihObjAm.BG06_IndObjAm = dbo.BG07_AgihObjSbg.BG06_IndObjAm " &
                    "WHERE (dbo.BG05_AgihPTJ.KodKw = '" & ddlKW.SelectedValue & "') AND (dbo.BG05_AgihPTJ.KodAgih = 'KG') AND (dbo.BG07_AgihObjSbg.BG07_StatLulus = '1') AND (dbo.BG05_AgihPTJ.BG05_Tahun = '" & ddlTahunAgih.SelectedValue & "') and dbo.BG05_AgihPTJ.BG05_Status='1'"

            Dim strJumKGKW As Decimal = 0.00
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strJumKGKW = IIf(IsDBNull(ds.Tables(0).Rows(0)("JumKGKW")), 0.00, ds.Tables(0).Rows(0)("JumKGKW"))
                End If
            End If

            Return strJumKGKW
        Catch ex As Exception

        End Try
    End Function

    ''' <summary>
    ''' 'BAJET BF KW
    ''' </summary>
    ''' <returns></returns>
    Private Function fGetJumBFKW() As Decimal
        Try
            Dim strSql As String
            strSql = "SELECT sum(mk09_debit) as JumBF FROM mk09_bajetbf WHERE mk09_Tahun='" & ddlTahunAgih.SelectedValue & "' and kodkw='" & ddlKW.SelectedValue & "'"
            Dim strJumTmbh As Decimal = 0.00
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strJumTmbh = IIf(IsDBNull(ds.Tables(0).Rows(0)("JumBF")), 0.00, ds.Tables(0).Rows(0)("JumBF"))
                End If
            End If

            Return strJumTmbh
        Catch ex As Exception

        End Try
    End Function

    ''' <summary>
    ''' BAJET ASAL PTJ
    ''' </summary>
    ''' <returns></returns>
    Private Function fGetJumAsalPTj() As Decimal
        Try
            Dim strSql As String

            strSql = "SELECT sum(bg05_amaun) as JumAsalPTj FROM BG05_AgihPtj WHERE BG05_Tahun='" & ddlTahunAgih.SelectedValue & "' and kodkw = '" & ddlKW.SelectedValue & "' and kodagih='AL'"

            Dim strJumAsalPTj As Decimal = 0.00
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strJumAsalPTj = IIf(IsDBNull(ds.Tables(0).Rows(0)("JumAsalPTj")), 0.00, ds.Tables(0).Rows(0)("JumAsalPTj"))
                End If
            End If

            Return strJumAsalPTj
        Catch ex As Exception

        End Try
    End Function

    ''' <summary>
    ''' 'BAJET TAMBAHAN PTJ
    ''' </summary>
    ''' <returns></returns>
    Private Function fGetJumTBPTJ() As Decimal
        Try
            Dim strSql As String

            strSql = "SELECT SUM(dbo.BG07_AgihObjSbg.BG07_Amaun) AS JumTBPTj FROM dbo.BG05_AgihPTJ INNER JOIN dbo.BG06_AgihObjAm ON dbo.BG05_AgihPTJ.BG05_IndPTJ = dbo.BG06_AgihObjAm.BG05_IndPTJ " &
                    "INNER JOIN dbo.BG07_AgihObjSbg ON dbo.BG06_AgihObjAm.BG06_IndObjAm = dbo.BG07_AgihObjSbg.BG06_IndObjAm " &
                    "WHERE (dbo.BG05_AgihPTJ.KodKw = '" & ddlKW.SelectedValue & "') AND (dbo.BG05_AgihPTJ.KodAgih = 'TB') AND (dbo.BG07_AgihObjSbg.BG07_StatLulus = '1') AND (dbo.BG05_AgihPTJ.BG05_Tahun = '" & ddlTahunAgih.SelectedValue & "') " &
                    "and dbo.BG05_AgihPTJ.BG05_Status='1'"

            Dim strJumTmbhKW As Decimal = 0.00
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strJumTmbhKW = IIf(IsDBNull(ds.Tables(0).Rows(0)("JumTBPTj")), 0.00, ds.Tables(0).Rows(0)("JumTBPTj"))
                End If
            End If

            Return strJumTmbhKW
        Catch ex As Exception

        End Try
    End Function

    ''' <summary>
    ''' 'BAJET KURANGAN PTJ
    ''' </summary>
    ''' <returns></returns>
    Private Function fGetJumKGPTJ() As Decimal
        Try
            Dim strSql As String
            strSql = "SELECT SUM(dbo.BG07_AgihObjSbg.BG07_Amaun) AS JumKgPTj FROM dbo.BG05_AgihPTJ INNER JOIN dbo.BG06_AgihObjAm ON dbo.BG05_AgihPTJ.BG05_IndPTJ = dbo.BG06_AgihObjAm.BG05_IndPTJ " &
                    "INNER JOIN dbo.BG07_AgihObjSbg ON dbo.BG06_AgihObjAm.BG06_IndObjAm = dbo.BG07_AgihObjSbg.BG06_IndObjAm " &
                    "WHERE (dbo.BG05_AgihPTJ.KodKw = '" & ddlKW.SelectedValue & "') AND (dbo.BG05_AgihPTJ.KodAgih = 'KG') AND (dbo.BG07_AgihObjSbg.BG07_StatLulus = '1') AND (dbo.BG05_AgihPTJ.BG05_Tahun = '" & ddlTahunAgih.SelectedValue & "') and dbo.BG05_AgihPTJ.BG05_Status='1'"

            Dim strJumKgPTj As Decimal = 0.00
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strJumKgPTj = IIf(IsDBNull(ds.Tables(0).Rows(0)("JumKgPTj")), 0.00, ds.Tables(0).Rows(0)("JumKgPTj"))
                End If
            End If

            Return strJumKgPTj
        Catch ex As Exception

        End Try
    End Function

    ''' <summary>
    ''' BAJET BF PTJ
    ''' </summary>
    ''' <returns></returns>
    Private Function fGetJumBFPTJ() As Decimal
        Try
            Dim strSql As String

            strSql = "SELECT sum(mk09_debit) as JumBFPTj FROM mk09_bajetbf WHERE mk09_Tahun='" & ddlTahunAgih.SelectedValue & "' and kodkw='" & ddlKW.SelectedValue & "'"
            Dim strJumBFPTj As Decimal = 0.00
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strJumBFPTj = IIf(IsDBNull(ds.Tables(0).Rows(0)("JumBFPTj")), 0.00, ds.Tables(0).Rows(0)("JumBFPTj"))
                End If
            End If

            Return strJumBFPTj
        Catch ex As Exception

        End Try
    End Function


    Private Sub ddlKO_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKO.SelectedIndexChanged

        'If ddlKO.SelectedIndex = 0 Then
        '    ScriptManager.RegisterStartupScript(Me.Page, Me.[GetType](), "", "fDisableBtn()", True)
        'Else

        'End If
        fClear()
        If ddlPTJ.SelectedIndex = 0 Then
            fClearDdlKP()
        Else
            fBindDdlKP()
        End If

    End Sub


    Private Sub fBindGvObjAm()
        Try
            Dim strSql As String
            Dim decTotJumBajetV, decTotJumObjAmV As Decimal

            strSql = "select a.KodVot, (select b.Butiran from MK_Vot b where b.KodVot = a.kodvot) as Butiran from bg_setupobjam a " &
                "where a.kodkw='" & ddlKW.SelectedValue & "' order by a.kodvot"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then

                    Dim dt As New DataTable
                    dt.Columns.Add("ObjAm", GetType(String))
                    dt.Columns.Add("Bajet", GetType(String))
                    dt.Columns.Add("TB", GetType(String))
                    dt.Columns.Add("KG", GetType(String))
                    dt.Columns.Add("BakiBF", GetType(String))
                    dt.Columns.Add("Jumlah", GetType(String))

                    Dim strKodVot, strObjAm, JumBajetV, JumTambV, JumKurangV, JumBFV, jumObjAmV As String
                    Dim decJumBajetV, decJumTambV, decJumKurangV, decJumBFV, decJumObjAmV As Decimal

                    Dim strKodKW, strKodPTj As String

                    strKodKW = ddlKW.SelectedValue
                    strKodPTj = ddlPTJ.SelectedValue

                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        strKodVot = Trim(ds.Tables(0).Rows(i)("kodvot")).TrimEnd
                        strObjAm = strKodVot & " - " & ds.Tables(0).Rows(i)("Butiran")

                        decJumBajetV = fGetJumBajetV(strKodVot)
                        JumBajetV = decJumBajetV.ToString("#,##0.00")
                        decJumTambV = fGetJumTBV(strKodVot)
                        JumTambV = decJumTambV.ToString("#,##0.00")
                        decJumKurangV = fGetJumKGV(strKodVot)
                        JumKurangV = decJumKurangV.ToString("#,##0.00")
                        decJumBFV = fGetJumBFV(strKodVot)
                        JumBFV = decJumBFV.ToString("#,##0.00")

                        decJumObjAmV = decJumBajetV + decJumTambV + decJumBFV - decJumKurangV
                        jumObjAmV = decJumObjAmV.ToString("#,##0.00")

                        dt.Rows.Add(strObjAm, JumBajetV, JumTambV, JumKurangV, JumBFV, jumObjAmV)

                        ' decTotJumBajetV += decJumBajetV
                        decTotJumObjAmV += decJumObjAmV
                    Next

                    lblJumRekod.InnerText = dt.Rows.Count
                    gvObjAm.DataSource = dt
                    gvObjAm.DataBind()
                End If
            End If

            Dim JumBajet, JumTamb, JumKurang, JumBF, jumObjAm As String
            Dim decJumBajet, decJumTamb, decJumKurang, decJumBF, decJumObjAm As Decimal

            decJumBajet = fGetJumBajet()
            JumBajet = decJumBajet.ToString("#,##0.00")
            decJumTamb = fGetJumTB()
            JumTamb = decJumTamb.ToString("#,##0.00")
            decJumKurang = fGetJumKG()
            JumKurang = decJumKurang.ToString("#,##0.00")
            decJumBF = fGetJumBF()
            JumBF = decJumBF.ToString("#,##0.00")

            decJumObjAm = decJumBajet + decJumTamb + decJumBF - decJumKurang
            jumObjAm = decJumObjAm.ToString("#,##0.00")

            txtBajetPTj.Text = JumBajet
            hidTxtBajetPTj.Text = JumBajet
            txtTambahan.Text = FormatNumber(CDec(JumTamb), 2)
            txtBF.Text = FormatNumber(CDec(JumBF), 2)
            txtKurangan.Text = JumKurang

            txtJumP.Text = (CDec(txtBajetPTj.Text) + CDec(txtTambahan.Text) + CDec(txtBF.Text) - CDec(txtKurangan.Text)).ToString("#,##0.00") 'Format(CDbl(Me.txtBajet.Text) + CDbl(Me.lblTmbhn.caption) - CDbl(Me.lblKrg.caption), "###,###,###,###.00")

            txtAgihanP.Text = decTotJumObjAmV.ToString("#,##0.00")
            txtBakiP.Text = (CDec(txtJumP.Text) - CDec(txtAgihanP.Text)).ToString("#,##0.00")

            If fCheckAgih() = True AndAlso txtBakiP.Text = "0.00" Then
                txtBajetPTj.Enabled = False
                txtBajetPTj.BackColor = ColorTranslator.FromHtml("#FFFFCC")
            Else
                txtBajetPTj.Enabled = True
                txtBajetPTj.BackColor = Color.White
            End If

        Catch ex As Exception

        End Try

    End Sub
    Private Function fCheckAgih() As Boolean
        Try
            Dim strSql As String
            strSql = "select sum(bg06_amaun) as jum from BG06_AgihObjAm where bg06_tahun='" & ddlTahunAgih.SelectedValue & "' and kodkw='" & ddlKW.SelectedValue & "' and kodKo = '" & ddlKO.SelectedValue & "' and kodptj='" & ddlPTJ.SelectedValue & "'"

            Dim strJumBajet As Decimal = 0.00
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strJumBajet = IIf(IsDBNull(ds.Tables(0).Rows(0)("jum")), 0.00, ds.Tables(0).Rows(0)("jum"))
                End If
            End If

            If strJumBajet > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception

        End Try

    End Function

    Private Sub ddlPTJ_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPTJ.SelectedIndexChanged
        Try

            fClear()
            If ddlPTJ.SelectedIndex = 0 Then
                fClearDdlKO()
            Else
                fBindDdlKO()
            End If
            'ScriptManager.RegisterStartupScript(Me.Page, Me.[GetType](), "", "fDisableBtn()", True)
        Catch ex As Exception

        End Try
    End Sub

    Private Function fGetJumBajet() As Decimal
        Try

            Dim strSql As String
            strSql = "SELECT sum(bg05_amaun) as JumBajet FROM BG05_AgihPTJ " &
                "WHERE BG05_Tahun='" & ddlTahunAgih.SelectedValue & "' and kodkw = '" & ddlKW.SelectedValue & "' and kodko = '" & ddlKO.SelectedValue & "' and kodptj = '" & ddlPTJ.SelectedValue & "' and kodagih='AL'"

            Dim strJumBajet As Decimal = 0.00
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strJumBajet = IIf(IsDBNull(ds.Tables(0).Rows(0)("JumBajet")), 0.00, ds.Tables(0).Rows(0)("JumBajet"))
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
            "(dbo.BG05_AgihPTJ.BG05_Status = '1') and (dbo.BG05_AgihPTJ.BG05_Tahun = '" & ddlTahunAgih.SelectedValue & "') and dbo.BG05_AgihPTJ.kodptj='" & ddlPTJ.SelectedValue & "' "

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
            "And (dbo.BG05_AgihPTJ.BG05_Tahun = '" & ddlTahunAgih.SelectedValue & "') and dbo.BG05_AgihPTJ.kodptj='" & ddlPTJ.SelectedValue & "' "

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
            strSql = "SELECT sum(mk09_debit) as JumBF FROM mk09_bajetbf WHERE mk09_Tahun='" & ddlTahunAgih.SelectedValue & "' " &
                "And kodkw='" & ddlKW.SelectedValue & "' and KodKO = '" & ddlKO.SelectedValue & "' and kodptj='" & ddlPTJ.SelectedValue & "'"

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

    Private Function fGetJumBajetV(ByVal strObjAm As String) As Decimal
        Try
            Dim strSql As String
            strSql = "SELECT sum(bg06_amaun) as JumObjAm FROM BG06_AgihObjAm WHERE BG06_Tahun='" & ddlTahunAgih.SelectedValue & "' " &
                "and kodkw = '" & ddlKW.SelectedValue & "' and KodKO = '" & ddlKO.SelectedValue & "' and kodptj = '" & ddlPTJ.SelectedValue & "' and kodvot='" & strObjAm & "' and kodagih='AL'"

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

    Private Function fGetJumTBV(ByVal strObjAm As String) As Decimal
        Try
            Dim strSql As String
            strSql = "SELECT SUM(dbo.BG07_AgihObjSbg.BG07_Amaun) AS JumTB FROM dbo.BG06_AgihObjAm " &
            "INNER JOIN dbo.BG07_AgihObjSbg ON dbo.BG06_AgihObjAm.BG06_IndObjAm = dbo.BG07_AgihObjSbg.BG06_IndObjAm " &
            "WHERE (dbo.BG06_AgihObjAm.BG06_Tahun = '" & ddlTahunAgih.SelectedValue & "') AND (dbo.BG06_AgihObjAm.KodKw = '" & ddlKW.SelectedValue & "') AND (dbo.BG06_AgihObjAm.KodKO  = '" & ddlKO.SelectedValue & "') AND (dbo.BG06_AgihObjAm.KodPTJ = '" & ddlPTJ.SelectedValue & "') " &
            "And (dbo.BG06_AgihObjAm.KodVot = '" & strObjAm & "') AND (dbo.BG06_AgihObjAm.KodAgih = 'TB') AND (dbo.BG06_AgihObjAm.BG06_Status = '1') AND (dbo.BG07_AgihObjSbg.BG07_StatLulus = 1)"

            Dim strJumTB As Decimal = 0.00
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strJumTB = IIf(IsDBNull(ds.Tables(0).Rows(0)("JumTB")), 0.00, ds.Tables(0).Rows(0)("JumTB"))
                End If
            End If

            Return strJumTB
        Catch ex As Exception

        End Try
    End Function

    Private Function fGetJumKGV(ByVal strObjAm As String) As Decimal
        Try
            Dim strSql As String

            strSql = "SELECT SUM(dbo.BG07_AgihObjSbg.BG07_Amaun) AS JumKG FROM dbo.BG06_AgihObjAm " &
            "INNER JOIN dbo.BG07_AgihObjSbg ON dbo.BG06_AgihObjAm.BG06_IndObjAm = dbo.BG07_AgihObjSbg.BG06_IndObjAm " &
            "WHERE (dbo.BG06_AgihObjAm.BG06_Tahun = '" & ddlTahunAgih.SelectedValue & "') AND (dbo.BG06_AgihObjAm.KodKw = '" & ddlKW.SelectedValue & "') AND (dbo.BG06_AgihObjAm.KodKO  = '" & ddlKO.SelectedValue & "') AND (dbo.BG06_AgihObjAm.KodPTJ = '" & ddlPTJ.SelectedValue & "') " &
            "And (dbo.BG06_AgihObjAm.KodVot = '" & strObjAm & "') AND (dbo.BG06_AgihObjAm.KodAgih = 'KG') AND (dbo.BG06_AgihObjAm.BG06_Status = '1') AND (dbo.BG07_AgihObjSbg.BG07_StatLulus = 1)"

            Dim strJumKG As Decimal = 0.00
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strJumKG = IIf(IsDBNull(ds.Tables(0).Rows(0)("JumKG")), 0.00, ds.Tables(0).Rows(0)("JumKG"))
                End If
            End If

            Return strJumKG
        Catch ex As Exception

        End Try
    End Function

    Private Function fGetJumBFV(ByVal strObjAm As String) As Decimal
        Try
            Dim strSql As String

            strObjAm = strObjAm.Substring(0, 1)


            strSql = "SELECT sum(mk09_debit) as JumBF FROM mk09_bajetbf WHERE mk09_Tahun='" & ddlTahunAgih.SelectedValue & "' " &
                "And kodkw='" & ddlKW.SelectedValue & "' AND KodKO  = '" & ddlKO.SelectedValue & "' and kodptj='" & ddlPTJ.SelectedValue & "' and left(kodvot,1)='" & strObjAm & "'"

            Dim strJumKG As Decimal = 0.00
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strJumKG = IIf(IsDBNull(ds.Tables(0).Rows(0)("JumBF")), 0.00, ds.Tables(0).Rows(0)("JumBF"))
                End If
            End If

            Return strJumKG
        Catch ex As Exception

        End Try
    End Function

    Private Sub fClear()
        Try
            gvObjAm.DataSource = New List(Of String)
            gvObjAm.DataBind()

            txtBajetPTj.Text = ""
            txtTambahan.Text = ""
            txtKurangan.Text = ""
            txtJumP.Text = ""
            txtAgihanP.Text = ""
            txtBakiP.Text = ""



        Catch ex As Exception

        End Try
    End Sub
    Dim TotBajet, TotTamb, TotKurg, TotBaki, TotJum As Decimal
    Dim strTotBajet, strTotTamb, strTotKurg, strTotBaki, strTotJum As String

    Protected Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.Click
        Dim ds As New DataSet
        Dim dbconn As New DBKewConn
        Dim blnSuccess As Boolean = True
        Try

            If ddlKW.SelectedIndex = 0 OrElse ddlPTJ.SelectedIndex = 0 OrElse ddlKO.SelectedIndex = 0 Then
                Exit Sub
            End If

            Dim strSql As String
            Dim strIndPTj As String
            Dim strDateNow As String = Now.ToString("yyyy-MM-dd")
            Dim strTahun As String = Trim(ddlTahunAgih.SelectedValue.TrimEnd)
            Dim strKodKW As String = Trim(ddlKW.SelectedValue.TrimEnd)
            Dim strkodKO As String = Trim(ddlKO.SelectedValue.TrimEnd)
            Dim strKodPTj As String = Trim(ddlPTJ.SelectedValue.TrimEnd)
            Dim strkodKP As String = Trim(ddlKp.SelectedValue.TrimEnd)

            Dim decAmaun As Decimal = CDec(Trim(txtBajetPTj.Text.TrimEnd))
            Dim strKodAgih = "AL"


            dbconn.sConnBeginTrans()
            strSql = "select count (*) from BG05_AgihPTJ where  bg05_tahun = '" & strTahun & "' and kodkw = '" & strKodKW & "' and  kodko = '" & strkodKO & "' and kodptj='" & strKodPTj & "'  and kodagih = 'AL' "
            If fCheckRec(strSql) > 0 Then
                strSql = "UPDATE BG05_AgihPTJ SET BG05_Amaun=@Amaun, BG05_TkhAgih=@TkhAgih WHERE bg05_tahun = @Tahun and kodkw = @KodKW and  kodko = @KodKO and kodptj= @KodPTj  and kodagih = @KodAgih"

                Dim paramSql() As SqlParameter = {
                        New SqlParameter("@Amaun", decAmaun),
                        New SqlParameter("@TkhAgih", strDateNow),
                    New SqlParameter("@Tahun", strTahun),
                    New SqlParameter("@KodKW", strKodKW),
                    New SqlParameter("@KodKO", strkodKO),
                    New SqlParameter("@KodPTj", strKodPTj),
                    New SqlParameter("@KodAgih", strKodAgih)
                        }

                If Not dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                    blnSuccess = False
                    Exit Try
                Else
                    'AUDIT LOG
                    sLogMedan = "BG05_Amaun|BG05_TkhAgih|bg05_tahun|kodkw|kodko|kodptj|kodagih"
                    sLogBaru = decAmaun & "|" & strDateNow & "|" & strTahun & "|" & strKodKW & "|" & strkodKO & "|" & strKodPTj & "|" & strKodAgih

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
                        New SqlParameter("@InfoTable", "BG05_AgihPTJ"),
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
                strIndPTj = fGetNoSiri("PTj", ddlTahunAgih.SelectedValue)
                Dim strIndKW = Trim(hidTxtIndKW.Text.TrimEnd)
                strSql = "insert into BG05_AgihPTJ (BG04_IndKw , BG05_IndPTJ , BG05_Tahun , KodKw , KodKo , KodPTJ , BG05_Amaun , KodAgih ,BG05_TkhAgih ,BG05_Status)" &
                            "values (@IndKW,@IndPTj, @Tahun, @KodKW, @KodKo, @KodPTj, @Amaun, @KodAgih, @TkhAgih, @Status)"

                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@IndKW", strIndKW),
                    New SqlParameter("@IndPTj", strIndPTj),
                    New SqlParameter("@Tahun", strTahun),
                    New SqlParameter("@KodKW", strKodKW),
                    New SqlParameter("@KodKo", strkodKO),
                    New SqlParameter("@KodPTj", strKodPTj),
                    New SqlParameter("@Amaun", decAmaun),
                    New SqlParameter("@KodAgih", strKodAgih),
                    New SqlParameter("@TkhAgih", strDateNow),
                    New SqlParameter("@Status", "1")}

                If Not dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                    blnSuccess = False
                    Exit Try
                Else
                    'AUDIT LOG
                    sLogMedan = "BG04_IndKw|BG05_IndPTJ|BG05_Tahun|KodKw|KodKo|KodPTJ|BG05_Amaun|KodAgih|BG05_TkhAgih|BG05_Status"
                    sLogBaru = strIndKW & "|" & strIndPTj & "|" & strTahun & "|" & strKodKW & "|" & strkodKO & "|" & strKodPTj & "|" & decAmaun & "|" & strKodAgih & "|" & strDateNow & "|1"

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
                        New SqlParameter("@InfoTable", "BG05_AgihPTJ"),
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

            If strIndPTj <> "" OrElse strIndPTj Is Nothing Then
                strSql = "select BG05_IndPTJ  from BG05_AgihPTJ with (nolock)  where BG05_Tahun = '" & strTahun & "' and KodKw = '" & strKodKW & "' and KodKo = '" & strkodKO & "' and KodPTJ = '" & strKodPTj & "' and KodAgih = 'AL'"
                strIndPTj = fGetIndPTj(strSql)
            End If

            'INSERT AMAUN OBJEK AM
            If gvObjAm.Rows.Count > 0 Then
                For Each row As GridViewRow In gvObjAm.Rows
                    Dim blnFound2 As Boolean = False
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
                                New SqlParameter("@KodAgih", "AL"),
                                New SqlParameter("@KodVot", strObjAm)
                                }

                        If Not dbconn.fUpdateCommand(strSql, paramSql2) > 0 Then
                            blnSuccess = False
                            Exit Try
                        Else
                            'AUDIT LOG
                            sLogMedan = "BG06_Amaun|BG06_TkhAgih|BG06_Tahun|KodKw|KodKO|KodPTJ|KodKP|KodAgih|KodVot"
                            sLogBaru = strAmtObjAm & "|" & strDateNow & "|" & strTahun & "|" & strKodKW & "|" & strkodKO & "|" & strKodPTj & "|" & strkodKP & "|" & strKodAgih & "|" & strObjAm

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
                        strIndObjAM = fGetNoSiri("OBJAM", ddlTahunAgih.SelectedValue)

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
                            sLogMedan = "BG05_IndPTJ|BG06_IndObjAm|BG06_Tahun|KodKw|KodKO|KodPTJ|KodKP|KodVot|BG06_Amaun|KodAgih |BG06_TkhAgih|BG06_Status "
                            sLogBaru = strIndPTj & "|" & strIndObjAM & "|" & strTahun & "|" & strKodKW & "|" & strkodKO & "|" & strKodPTj & "|" & strkodKP & "|" & strKodAgih & "|" & strObjAm & "|" & strAmtObjAm & "|" & strKodAgih & "|" & strDateNow & "|1"

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
            End If

        Catch ex As Exception
            blnSuccess = False
        End Try

        If blnSuccess = False Then
            dbconn.sConnRollbackTrans()
            fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
        Else
            dbconn.sConnCommitTrans()
            fGlobalAlert("Rekod agihan telah disimpan!", Me.Page, Me.[GetType]())
            fBindGvObjAm()
        End If
    End Sub

    Private Sub gvObjAm_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvObjAm.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.ForeColor = ColorTranslator.FromHtml("#000000")

                Dim txtBajet As TextBox = CType(e.Row.FindControl("txtBajet"), TextBox)
                TotBajet += Decimal.Parse(txtBajet.Text)
                strTotBajet = TotBajet.ToString("#,##0.00")
                txtBajet.Style("text-align") = "right"

                Dim lblTamb As Label = CType(e.Row.FindControl("lblTB"), Label)
                TotTamb += Decimal.Parse(lblTamb.Text)
                strTotTamb = TotTamb.ToString("#,##0.00")

                Dim lblKurg As Label = CType(e.Row.FindControl("lblKG"), Label)
                TotKurg += Decimal.Parse(lblKurg.Text)
                strTotKurg = TotKurg.ToString("#,##0.00")

                Dim lblBaki As Label = CType(e.Row.FindControl("lblBakiBF"), Label)
                TotBaki += Decimal.Parse(lblBaki.Text)
                strTotBaki = TotBaki.ToString("#,##0.00")

                Dim lblJum As Label = CType(e.Row.FindControl("lblJum"), Label)
                TotJum += Decimal.Parse(lblJum.Text)
                strTotJum = TotJum.ToString("#,##0.00")
            End If

            If e.Row.RowType = DataControlRowType.Footer Then

                Dim lblTotBajet As Label = CType(e.Row.FindControl("lblTotBajet"), Label)
                lblTotBajet.Text = strTotBajet.ToString()
                lblTotBajet.Font.Bold = True

                Dim lblTotTamb As Label = CType(e.Row.FindControl("lblTotTB"), Label)
                lblTotTamb.Text = strTotTamb.ToString()
                lblTotTamb.Font.Bold = True

                Dim lblTotKurg As Label = CType(e.Row.FindControl("lblTotKG"), Label)
                lblTotKurg.Text = strTotKurg.ToString()
                lblTotKurg.Font.Bold = True

                Dim lblTotBaki As Label = CType(e.Row.FindControl("lblTotBakiBF"), Label)
                lblTotBaki.Text = strTotBaki.ToString()
                lblTotBaki.Font.Bold = True

                Dim lblTotJum As Label = CType(e.Row.FindControl("lblTotJum"), Label)
                lblTotJum.Text = strTotJum.ToString()
                lblTotJum.Font.Bold = True
            End If




        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvObjAm_PreRender(sender As Object, e As EventArgs) Handles gvObjAm.PreRender
        Try
            If gvObjAm.Rows.Count > 0 Then
                Dim blnDisable As Boolean = True
                If txtBajetPTj.Text = "0.00" OrElse txtBajetPTj.Text = "" Then
                    For i As Integer = 0 To gvObjAm.Rows.Count - 1
                        Dim txtBajet As TextBox = TryCast(gvObjAm.Rows(i).FindControl("txtBajet"), TextBox)
                        txtBajet.Enabled = False
                    Next
                    'ScriptManager.RegisterStartupScript(Me.Page, Me.[GetType](), "", "fDisableBtn()", True)
                End If
            End If

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

    Private Sub ddlKp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKp.SelectedIndexChanged

        fBindGvObjAm()
    End Sub

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

    Private Sub ddlTahunAgih_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTahunAgih.SelectedIndexChanged
        fBindDdlKW()
    End Sub
End Class

