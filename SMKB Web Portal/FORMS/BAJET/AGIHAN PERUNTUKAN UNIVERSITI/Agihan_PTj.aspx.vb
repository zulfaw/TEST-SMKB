Imports System.Drawing
Imports System.Data.SqlClient
Imports SMKB_Web_Portal.clsAlert
Public Class Agihan_PTj
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
                sClearGvPTj()
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
                    "where dbo.MK01_VotTahun.MK01_Tahun = '" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' ORDER BY dbo.MK_Kw.KodKw"

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
    Private Sub ddlKW_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKW.SelectedIndexChanged

        sClearField()
        sClearGvPTj()

        If ddlKW.SelectedIndex <> 0 Then
            hidIndKW.Value = fGetIdxKW()
            fFindBajetKW()
            If CDec(txtBajetKW.Text) = 0 Then
                fGlobalAlert("Tiada bajet untuk KW " & ddlKW.SelectedItem.Text & "!", Me.Page, Me.[GetType]())
                sClearGvPTj()
            Else
                fBindGvPTj()
            End If
        End If


    End Sub

    Private Sub fBindGvPTj()
        Try
            Dim strSql As String
            Dim decTotJumObjAmV As Decimal
            Dim strKodKW As String = Trim(ddlKW.SelectedValue.TrimEnd)
            Dim strTahun = ddlTahunAgih.SelectedValue.TrimEnd

            sClearGvPTj()
            strSql = "select distinct MK_PTJ.KodPTJ, MK_PTJ.Butiran   from MK_PTJ inner join MK01_VotTahun on MK01_VotTahun.KodPTJ = MK_PTJ.KodPTJ
where MK01_Tahun = '" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' and MK01_VotTahun.KodKw = '" & strKodKW & "' order by MK_PTJ .KodPTJ"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then

                    Dim dt As New DataTable
                    dt.Columns.Add("KodPTj", GetType(String))
                    dt.Columns.Add("PTj", GetType(String))
                    dt.Columns.Add("JumBajetPast", GetType(String))
                    dt.Columns.Add("JumBajetUsed", GetType(String))
                    dt.Columns.Add("JumBajet", GetType(String))
                    dt.Columns.Add("TB", GetType(String))
                    dt.Columns.Add("KG", GetType(String))
                    dt.Columns.Add("BakiBF", GetType(String))
                    dt.Columns.Add("Jumlah", GetType(String))

                    Dim JumBajetV, JumTambV, JumKurangV, JumBFV, jumObjAmV As String
                    Dim decJumBajetV, decJumTambV, decJumKurangV, decJumBFV, decJumObjAmV As Decimal

                    Dim strKodPTJ, strButiranPTj, strJumBajetPast, strJumBajetUsed As String

                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        strKodPTJ = Trim(ds.Tables(0).Rows(i)("KodPTJ")).TrimEnd
                        strButiranPTj = ds.Tables(0).Rows(i)("Butiran")

                        strJumBajetPast = fGetJumBajet(CInt(strTahun) - 1, strKodKW, strKodPTJ)
                        strJumBajetUsed = fGetBelanja(CInt(strTahun) - 1, strKodKW, strKodPTJ)

                        decJumBajetV = fGetJumBajet(strTahun, strKodKW, strKodPTJ)
                        JumBajetV = decJumBajetV.ToString("#,##0.00")
                        decJumTambV = fGetJumTB(strKodPTJ)
                        JumTambV = decJumTambV.ToString("#,##0.00")
                        decJumKurangV = fGetJumKG(strKodPTJ)
                        JumKurangV = decJumKurangV.ToString("#,##0.00")
                        decJumBFV = fGetJumBF(strKodPTJ)
                        JumBFV = decJumBFV.ToString("#,##0.00")

                        decJumObjAmV = decJumBajetV + decJumTambV + decJumBFV - decJumKurangV
                        jumObjAmV = decJumObjAmV.ToString("#,##0.00")

                        dt.Rows.Add(strKodPTJ, strButiranPTj, strJumBajetPast, strJumBajetUsed, JumBajetV, JumTambV, JumKurangV, JumBFV, jumObjAmV)

                        decTotJumObjAmV += decJumObjAmV
                    Next

                    lblJumRekod.InnerText = dt.Rows.Count
                    gvPTj.DataSource = dt
                    gvPTj.DataBind()
                End If
            End If

        Catch ex As Exception

        End Try

    End Sub


    Private Sub sClearGvPTj()
        gvPTj.DataSource = New List(Of String)
        gvPTj.DataBind()
    End Sub

    Private Function fGetJumBajet(strTahun, strKodKW, strKodPTj) As Decimal
        Try

            Dim strSql As String
            strSql = "SELECT sum(bg05_amaun) as JumBajet FROM BG05_AgihPTJ " &
                "WHERE BG05_Tahun='" & strTahun & "' and kodkw = '" & strKodKW & "' and kodptj = '" & strKodPTj & "' and kodagih='AL'"

            Dim strJumBajet As Decimal = 0.00
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strJumBajet = IIf(IsDBNull(ds.Tables(0).Rows(0)("JumBajet")), 0.00, ds.Tables(0).Rows(0)("JumBajet"))
                End If
            End If

            Return strJumBajet
        Catch ex As Exception

        End Try
    End Function

    Private Function fGetJumTB(ByVal strKodPTj As String) As Decimal
        Try
            Dim strSql As String
            strSql = "SELECT SUM(dbo.BG07_AgihObjSbg.BG07_Amaun) AS JumTB FROM dbo.BG05_AgihPTJ " &
                "INNER JOIN dbo.BG06_AgihObjAm ON dbo.BG05_AgihPTJ.BG05_IndPTJ = dbo.BG06_AgihObjAm.BG05_IndPTJ " &
            "INNER JOIN dbo.BG07_AgihObjSbg ON dbo.BG06_AgihObjAm.BG06_IndObjAm = dbo.BG07_AgihObjSbg.BG06_IndObjAm " &
            "WHERE (dbo.BG05_AgihPTJ.KodKw = '" & ddlKW.SelectedValue & "') and (dbo.BG05_AgihPTJ.BG05_Tahun = '" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "') and dbo.BG05_AgihPTJ.kodptj='" & strKodPTj & "' " &
            "AND (dbo.BG05_AgihPTJ.KodAgih = 'TB') AND (dbo.BG07_AgihObjSbg.BG07_StatLulus = '1') AND (dbo.BG05_AgihPTJ.BG05_Status = '1')"

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

    Private Function fGetJumKG(ByVal strKodPTj As String) As Decimal
        Try
            Dim strSql As String

            strSql = "SELECT SUM(dbo.BG07_AgihObjSbg.BG07_Amaun) AS JumKG FROM dbo.BG05_AgihPTJ " &
            "INNER JOIN dbo.BG06_AgihObjAm ON dbo.BG05_AgihPTJ.BG05_IndPTJ = dbo.BG06_AgihObjAm.BG05_IndPTJ " &
            "INNER JOIN dbo.BG07_AgihObjSbg ON dbo.BG06_AgihObjAm.BG06_IndObjAm = dbo.BG07_AgihObjSbg.BG06_IndObjAm " &
            "WHERE(dbo.BG05_AgihPTJ.KodKw = '" & ddlKW.SelectedValue & "') And (dbo.BG05_AgihPTJ.BG05_Tahun = '" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "') and dbo.BG05_AgihPTJ.kodptj='" & strKodPTj & "' " &
            "AND (dbo.BG05_AgihPTJ.KodAgih = 'KG') AND (dbo.BG07_AgihObjSbg.BG07_StatLulus = '1') AND (dbo.BG05_AgihPTJ.BG05_Status = '1')"

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

    Private Function fGetJumBF(ByVal strKodPTj As String) As Decimal
        Try
            Dim strSql As String
            strSql = "SELECT sum(mk09_debit) as JumBF FROM mk09_bajetbf WHERE mk09_Tahun='" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' " &
                "And kodkw='" & ddlKW.SelectedValue & "' and kodptj='" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' "

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

    Private Sub fFindBajetKW()

        Dim decBajetKW, decJumAsal, decJumTmbh, decJumBF, decJumKrg As Decimal
        Try
            decJumAsal = fGetJumAsalKW() 'BAJET ASAL KW
            decJumTmbh = fGetJumTBKW() 'BAJET TAMBAHAN KW
            decJumKrg = fGetJumKGKW() 'BAJET KURANGAN KW
            decJumBF = fGetJumBFKW() 'BAJET BF KW

            decBajetKW = decJumAsal + decJumTmbh + decJumBF - decJumKrg
            'strBajetKW = decBajetKW.ToString("#,##0.00")
            txtBajetKW.Text = FormatNumber(decJumAsal) 'FormatNumber(decBajetKW, 2)

            Dim decJumAsalP, decJumTbhP, decJumKrgP, decBajetBFP, decSumPTj, decBaki As Decimal
            Dim strSumPTj, strBaki As String

            'CARI BAJET PTJ
            decJumAsalP = fGetJumAsalPTj() 'BAJET ASAL KW
            decJumTbhP = fGetJumTBPTJ() 'BAJET TAMBAHAN KW
            decJumKrgP = fGetJumKGPTJ() 'BAJET KURANGAN KW
            decBajetBFP = fGetJumBFPTJ() 'BAJET BF KW

            decSumPTj = decJumAsalP + decJumTbhP + decBajetBFP - decJumKrgP
            strSumPTj = FormatNumber(decSumPTj, 2)
            decBaki = decBajetKW - decSumPTj
            strBaki = FormatNumber(decBaki, 2)
            txtAgihKW.Text = FormatNumber(decJumAsalP) 'strSumPTj
            hidTxtAgihKW.Text = strSumPTj

            txtBakiKW.Text = strBaki

        Catch ex As Exception

        End Try
    End Sub
    Private Function fGetJumAsalKW() As Decimal
        Try
            Dim strSql As String

            strSql = "SELECT sum(bg04_amaun) as JumAsalKW FROM BG04_AgihKw WHERE BG04_Tahun='" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' and kodagih='AL' and kodkw = '" & ddlKW.SelectedValue & "'"
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

    Private Function fGetJumTBKW() As Decimal
        Try
            Dim strSql As String
            strSql = "SELECT SUM(dbo.BG07_AgihObjSbg.BG07_Amaun) AS JumTBKW FROM dbo.BG04_AgihKw INNER JOIN dbo.BG05_AgihPTJ ON dbo.BG04_AgihKw.BG04_IndKw = dbo.BG05_AgihPTJ.BG04_IndKw " &
                    "INNER JOIN dbo.BG06_AgihObjAm ON dbo.BG05_AgihPTJ.BG05_IndPTJ = dbo.BG06_AgihObjAm.BG05_IndPTJ " &
                    "INNER JOIN dbo.BG07_AgihObjSbg ON dbo.BG06_AgihObjAm.BG06_IndObjAm = dbo.BG07_AgihObjSbg.BG06_IndObjAm " &
                    "WHERE (dbo.BG07_AgihObjSbg.BG07_StatLulus = '1') AND (dbo.BG04_AgihKw.BG04_Status = '1') AND (dbo.BG04_AgihKw.KodAgih = 'TB') " &
                    "AND(dbo.BG04_AgihKw.KodKw = '" & ddlKW.SelectedValue & "') and dbo.BG04_AgihKw.Bg04_Tahun='" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "'"

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

    Private Function fGetJumKGKW() As Decimal
        Try
            Dim strSql As String
            strSql = "SELECT SUM(dbo.BG07_AgihObjSbg.BG07_Amaun) AS JumKGKW FROM dbo.BG05_AgihPTJ INNER JOIN dbo.BG06_AgihObjAm ON dbo.BG05_AgihPTJ.BG05_IndPTJ = dbo.BG06_AgihObjAm.BG05_IndPTJ " &
                    "INNER JOIN dbo.BG07_AgihObjSbg ON dbo.BG06_AgihObjAm.BG06_IndObjAm = dbo.BG07_AgihObjSbg.BG06_IndObjAm " &
                    "WHERE (dbo.BG05_AgihPTJ.KodKw = '" & ddlKW.SelectedValue & "') AND (dbo.BG05_AgihPTJ.KodAgih = 'KG') AND (dbo.BG07_AgihObjSbg.BG07_StatLulus = '1') AND (dbo.BG05_AgihPTJ.BG05_Tahun = '" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "') and dbo.BG05_AgihPTJ.BG05_Status='1'"

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

    Private Function fGetJumBFKW() As Decimal
        Try
            Dim strSql As String
            strSql = "SELECT sum(mk09_debit) as JumBF FROM mk09_bajetbf WHERE mk09_Tahun='" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' and kodkw='" & ddlKW.SelectedValue & "'"
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

            strSql = "SELECT sum(bg05_amaun) as JumAsalPTj FROM BG05_AgihPtj WHERE BG05_Tahun='" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' and kodkw = '" & ddlKW.SelectedValue & "' and kodagih='AL'"

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
                    "WHERE (dbo.BG05_AgihPTJ.KodKw = '" & ddlKW.SelectedValue & "') AND (dbo.BG05_AgihPTJ.KodAgih = 'TB') AND (dbo.BG07_AgihObjSbg.BG07_StatLulus = '1') AND (dbo.BG05_AgihPTJ.BG05_Tahun = '" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "') " &
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
                    "WHERE (dbo.BG05_AgihPTJ.KodKw = '" & ddlKW.SelectedValue & "') AND (dbo.BG05_AgihPTJ.KodAgih = 'KG') AND (dbo.BG07_AgihObjSbg.BG07_StatLulus = '1') AND (dbo.BG05_AgihPTJ.BG05_Tahun = '" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "') and dbo.BG05_AgihPTJ.BG05_Status='1'"

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

            strSql = "SELECT sum(mk09_debit) as JumBFPTj FROM mk09_bajetbf WHERE mk09_Tahun='" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' and kodkw='" & ddlKW.SelectedValue & "'"
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



    Protected Sub txtBajet_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim footerRow = gvPTj.FooterRow

            Dim txtBajet As TextBox = CType(sender, TextBox)
            Dim gvr As GridViewRow = CType(txtBajet.Parent.Parent, GridViewRow)
            Dim rowindex As Integer = gvr.RowIndex
            Dim selectedRow As GridViewRow = gvPTj.Rows(gvr.RowIndex)

            Dim lblJum As Label = CType(selectedRow.FindControl("lblJum"), Label)

            If txtBajet.Text = "" Then txtBajet.Text = "0.00"
            Dim decBajet As Decimal = CDec(txtBajet.Text)
            txtBajet.Text = FormatNumber(decBajet)

            'Semak jumlah besar
            Dim decJumBajetPTj As Decimal
            Dim decBajetPTj As Decimal
            For Each gvRow As GridViewRow In gvPTj.Rows
                decBajetPTj = Trim(CType(gvRow.FindControl("txtBajet"), TextBox).Text.TrimEnd)
                decJumBajetPTj += decBajetPTj 'Total Jumlah Bajet PTj
            Next

            Dim decBajetKW As Decimal = CDec(txtBajetKW.Text)

            Dim decTotJum As Decimal
            If decJumBajetPTj > decBajetKW Then
                fGlobalAlert("Amaun yang dimasukkan melebihi amaun bajet '" & Trim(ddlKW.SelectedItem.Text.TrimEnd) & "'", Me.Page, Me.[GetType]())
                txtBajet.Text = "0.00"
            End If

            decJumBajetPTj = 0
            'Kira Jumlah Besar PTj
            Dim decTbPTj As Decimal = Trim(CType(selectedRow.FindControl("lblTB"), Label).Text.TrimEnd) 'amaun Tambahan
            Dim decKgPTj As Decimal = Trim(CType(selectedRow.FindControl("lblKG"), Label).Text.TrimEnd) 'amaun Kurangan
            Dim decBakiBfPTj As Decimal = Trim(CType(selectedRow.FindControl("lblBakiBF"), Label).Text.TrimEnd) 'amaun Baki BF

            Dim decJumPTj As Decimal = CDec(txtBajet.Text) + decTbPTj - decKgPTj + decBakiBfPTj
            lblJum.Text = FormatNumber(decJumPTj)

            'Kira Jumlah Besar
            For Each gvRow As GridViewRow In gvPTj.Rows
                decBajetPTj = Trim(CType(gvRow.FindControl("txtBajet"), TextBox).Text.TrimEnd)
                decJumBajetPTj += decBajetPTj 'Total Jumlah Bajet

                Dim decJumlah As Decimal = Trim(CType(gvRow.FindControl("lblJum"), Label).Text.TrimEnd)
                decTotJum += decJumlah 'Total Jumlah Besar
            Next

            txtAgihKW.Text = FormatNumber(decJumBajetPTj)
            txtBakiKW.Text = FormatNumber(decBajetKW - decJumBajetPTj)

            CType(footerRow.FindControl("lblTotBajet"), Label).Text = FormatNumber(decJumBajetPTj) 'Total Jumlah Besar Bajet
            CType(footerRow.FindControl("lblTotJum"), Label).Text = FormatNumber(decTotJum) 'Total Jumlah Besar

        Catch ex As Exception

        End Try
    End Sub

    Dim TotBajet, TotTamb, TotKurg, TotBaki, TotJum, TotBajetPast, TotBajetUsed As Decimal
    Dim strTotBajet, strTotTamb, strTotKurg, strTotBaki, strTotJum As String

    Private Sub gvPTj_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvPTj.RowDataBound

        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.ForeColor = ColorTranslator.FromHtml("#000000")

                Dim lblJumBajetPast As Label = CType(e.Row.FindControl("lblJumBajetPast"), Label)
                TotBajetPast += CDec(lblJumBajetPast.Text)

                Dim lblJumBajetUsed As Label = CType(e.Row.FindControl("lblJumBajetUsed"), Label)
                TotBajetUsed += CDec(lblJumBajetUsed.Text)

                Dim txtBajet As TextBox = CType(e.Row.FindControl("txtBajet"), TextBox)
                TotBajet += CDec(txtBajet.Text)


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


                Dim strKodPTj As String = CType(e.Row.FindControl("lblKodPTJ"), Label).Text
                If fCheckLulus(strKodPTj) Then
                    txtBajet.Enabled = False
                Else
                    txtBajet.Enabled = True
                End If

            End If

            If e.Row.RowType = DataControlRowType.Footer Then

                Dim lblTotBajetPast As Label = CType(e.Row.FindControl("lblTotBajetPast"), Label)
                lblTotBajetPast.Text = FormatNumber(TotBajetPast)

                Dim lblTotBajetUsed As Label = CType(e.Row.FindControl("lblTotBajetUsed"), Label)
                lblTotBajetUsed.Text = FormatNumber(TotBajetUsed)

                Dim lblTotBajet As Label = CType(e.Row.FindControl("lblTotBajet"), Label)
                lblTotBajet.Text = FormatNumber(TotBajet)


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

            If e.Row.RowType = DataControlRowType.Header Then
                Dim lblBajetPast As Label = CType(e.Row.FindControl("lblBajetPast"), Label)
                lblBajetPast.Text = "Bajet Agih " & CInt(ddlTahunAgih.SelectedValue) - 1 & " (RM)"

                Dim lblJumGuna As Label = CType(e.Row.FindControl("lblJumGuna"), Label)
                lblJumGuna.Text = "Bajet Belanja " & CInt(ddlTahunAgih.SelectedValue) - 1 & " (RM)"

                Dim lblBajetCur As Label = CType(e.Row.FindControl("lblBajetCur"), Label)
                lblBajetCur.Text = "Bajet Agih " & CInt(ddlTahunAgih.SelectedValue) & " (RM)"

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function fCheckLulus(ByVal strKodPTj As String)
        Dim blnLulus As Boolean = False
        Try
            Dim strSql As String = "Select distinct statlulus  from vbg_lulusagih_2 where tahun = '" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' and KodPTJ = '" & strKodPTj & "'"
            Dim dbconn As New DBKewConn
            Dim ds As New DataSet
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    blnLulus = CBool(ds.Tables(0).Rows(0)("statLulus").ToString)
                End If
            End If

        Catch ex As Exception

        End Try
        Return blnLulus
    End Function
    Private Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.Click
        Dim ds As New DataSet
        Dim dbconn As New DBKewConn
        Dim blnSuccess As Boolean = True

        Dim strSql As String
        Dim strIndPTj As String
        Dim strDateNow As String = Now.ToString("yyyy-MM-dd")
        Dim strTahun As String = Trim(ddlTahunAgih.SelectedValue.TrimEnd)
        Dim strKodKW As String = Trim(ddlKW.SelectedValue.TrimEnd)
        Dim strKodAgih = "AL"
        ' Dim decAmaun As Decimal = CDec(Trim(txtBajetPTj.Text.TrimEnd))

        Try


            'Dim footerRow = gvPTj.FooterRow
            'Dim decJumBajetPTj As Decimal = CDec(CType(footerRow.FindControl("lblTotBajet"), Label).Text)
            Dim decJumBajetPTj As Decimal
            For Each gvRow As GridViewRow In gvPTj.Rows
                Dim decBajetPTj As Decimal = CDec(TryCast(gvRow.FindControl("txtBajet"), TextBox).Text)
                decJumBajetPTj += decBajetPTj
            Next


            If decJumBajetPTj = 0 Then
                fGlobalAlert("Sila masukkan amaun agihan kepada PTj!", Me.Page, Me.[GetType]())
                Exit Sub
            End If

            dbconn.sConnBeginTrans()
            For Each gvRow As GridViewRow In gvPTj.Rows
                Dim decBajetPTj As Decimal = CDec(TryCast(gvRow.FindControl("txtBajet"), TextBox).Text)

                'If decBajetPTj > 0 Then
                Dim strKodPTj As String = Trim(TryCast(gvRow.FindControl("lblKodPTJ"), Label).Text.TrimEnd)
                    strSql = "select count(*) from BG05_AgihPTJ with (nolock) where BG05_Tahun = '" & strTahun & "' and KodKw = '" & strKodKW & "' and KodPTJ = '" & strKodPTj & "' and KodAgih = 'AL'"

                    If fCheckRec(strSql) > 0 Then
                        'UPDATE
                        strSql = "UPDATE BG05_AgihPTJ SET BG05_Amaun=@Amaun, BG05_TkhAgih=@TkhAgih WHERE bg05_tahun = @Tahun and kodkw = @KodKW and kodptj= @KodPTj  and kodagih = @KodAgih"

                        Dim paramSql() As SqlParameter = {
                            New SqlParameter("@Amaun", decBajetPTj),
                            New SqlParameter("@TkhAgih", strDateNow),
                        New SqlParameter("@Tahun", strTahun),
                        New SqlParameter("@KodKW", strKodKW),
                        New SqlParameter("@KodPTj", strKodPTj),
                        New SqlParameter("@KodAgih", strKodAgih)
                            }

                        If Not dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                            blnSuccess = False
                            Exit Try
                        Else
                            'AUDIT LOG
                            sLogMedan = "BG05_Amaun|BG05_TkhAgih|bg05_tahun|kodkw|KodPTJ|kodagih"
                            sLogBaru = decBajetPTj & "|" & strDateNow & "|" & strTahun & "|" & strKodKW & "|" & strKodPTj & "|" & strKodAgih

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
                        'INSERT
                        strIndPTj = fGetNoSiri("PTj", strTahun)
                        Dim strIndKW = Trim(hidIndKW.Value.TrimEnd)
                        strSql = "insert into BG05_AgihPTJ (BG04_IndKw , BG05_IndPTJ , BG05_Tahun , KodKw , KodPTJ , BG05_Amaun , KodAgih ,BG05_TkhAgih ,BG05_Status)" &
                                    "values (@IndKW,@IndPTj, @Tahun, @KodKW, @KodPTj, @Amaun, @KodAgih, @TkhAgih, @Status)"

                        Dim paramSql() As SqlParameter = {
                            New SqlParameter("@IndKW", strIndKW),
                            New SqlParameter("@IndPTj", strIndPTj),
                            New SqlParameter("@Tahun", strTahun),
                            New SqlParameter("@KodKW", strKodKW),
                            New SqlParameter("@KodPTj", strKodPTj),
                            New SqlParameter("@Amaun", decBajetPTj),
                            New SqlParameter("@KodAgih", strKodAgih),
                            New SqlParameter("@TkhAgih", strDateNow),
                            New SqlParameter("@Status", "1")}

                        If Not dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                            blnSuccess = False
                            Exit Try
                        Else
                            'AUDIT LOG
                            sLogMedan = "BG04_IndKw|BG05_IndPTJ|BG05_Tahun|KodKw|KodPTJ|BG05_Amaun|KodAgih|BG05_TkhAgih|BG05_Status"
                            sLogBaru = decBajetPTj & "|" & strDateNow & "|" & strTahun & "|" & strKodKW & "|" & strKodPTj & "|" & strKodAgih & "|" & strDateNow & "|1"

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
                'End If
            Next

        Catch ex As Exception
            blnSuccess = False
        End Try

        If blnSuccess Then
            dbconn.sConnCommitTrans()
            fGlobalAlert("Maklumat agihan telah disimpan!", Me.Page, Me.[GetType]())
            fFindBajetKW()
        Else
            dbconn.sConnRollbackTrans()
            fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
        End If
    End Sub

    Private Sub sClearField()

        txtBajetKW.Text = ""
        txtAgihKW.Text = ""
        txtBakiKW.Text = ""
        hidIndKW.Value = ""
    End Sub


    Private Function fGetIdxKW() As String
        Try
            Dim strSql As String

            strSql = "Select bg04_indkw from BG04_AgihKw  where bg04_tahun='" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' and kodkw = '" & ddlKW.SelectedValue & "' and kodagih='AL'"
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

    Private Sub ddlTahunAgih_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTahunAgih.SelectedIndexChanged
        sClearGvPTj()
        fBindDdlKW()
    End Sub

    Public Function fGetBelanja(strTahun, strKW, strKodPTj) As Decimal

        Dim dbconn As New DBKewConn
        Dim decBelanjaSbnr, decJrnlDt, decJrnlKt, decJrnlLejar, decJumBil As Decimal
        Dim ds As New DataSet
        Dim strSql
        Try

            strSql = "Select  isnull(sum(mk06_debit) ,0) as JumDebit, isnull(sum(mk06_kredit),0) as JumKredit
 From MK06_TRANSAKSI Where
 KODkw = '" & strKW & "' And year(mk06_tkhtran) = '" & strTahun & "'
 and KodPTJ = '" & strKodPTj & "'
 And substring(mk06_rujukan,1,2) in ('RT','BK','RS','JP','BP');"

            ds = dbconn.fSelectCommand(strSql)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    decJrnlDt = ds.Tables(0).Rows(0)("JumDebit")
                    decJrnlKt = ds.Tables(0).Rows(0)("JumKredit")
                End If
            End If

            strSql = "Select isnull(sum(mk06_debit),0) - isnull(sum(mk06_kredit),0) as JumJrnlLejar
From mk06_transaksi d, gl01_jrnltran
Where d.mk06_rujukan = gl01_jrnltran.gl01_nojrnl
And d.kodkw = '" & strKW & "' And year(d.mk06_tkhtran) = '" & strTahun & "'
and KodPTJ = '" & strKodPTj & "'   
And substring(d.mk06_rujukan,1,2) in ('JK')            
And (gl01_statuslejar Is null Or gl01_statuslejar=0)"

            ds = dbconn.fSelectCommand(strSql)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    decJrnlLejar = ds.Tables(0).Rows(0)("JumJrnlLejar")
                End If
            End If

            strSql = " Select isnull(sum(mk06_debit),0) - isnull(sum(mk06_kredit),0) as JumBil
 From mk06_transaksi Where Year(mk06_tkhtran) = '" & strTahun & "' 
 And kodkw = '" & strKW & "' And KodPTJ = '" & strKodPTj & "' and koddok in ('BIL','ADJ_BIL')"

            ds = dbconn.fSelectCommand(strSql)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    decJumBil = ds.Tables(0).Rows(0)("JumBil")
                End If
            End If

            decBelanjaSbnr = (decJrnlDt - decJrnlKt) + decJrnlLejar + decJumBil

            Return decBelanjaSbnr

        Catch ex As Exception
            Return 0
        End Try


    End Function
End Class