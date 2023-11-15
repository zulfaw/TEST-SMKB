Imports System.Drawing
Imports System.Data.SqlClient

Public Class Agihan_Objek_Sebagai
    Inherits System.Web.UI.Page

    Dim decTotBajet As Decimal = 0.00
    Dim decTotTambahan As Decimal = 0.00
    Dim decTotKurangan As Decimal = 0.00
    Dim decTotBakiBF As Decimal = 0.00
    Dim decTotJumlah As Decimal = 0.00

    Dim decTotBajet2 As Decimal = 0.00
    Dim decTotTambahan2 As Decimal = 0.00
    Dim decTotKurangan2 As Decimal = 0.00
    Dim decTotBakiBF2 As Decimal = 0.00
    Dim decTotJumlah2 As Decimal = 0.00
    Dim sLogBaru As String
    Dim sLogMedan As String
    Dim strNoStaf As String = UserInfo.strSesStaffID
    Dim sLogLevel As String = UserInfo.strUsrTahap
    Dim lsLogUsrIP As String = UserInfo.strUsrPcIP
    Dim lsLogUsrPC As String = UserInfo.strUsrPCName

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Session("sesKodSubMenu") = Request.QueryString("KodSubMenu")
            fBindDdlTahunAgih()
            'txtTahun.Text = Now.Year
            fLoadKP()
            fBindDdlPTJ()
            txtKW.Text = ""
            txtObjAm.Text = ""
            txtBajetAm.Text = "0.00"
            txtAgihan.Text = "0.00"
            txtBaki.Text = "0.00"

            txtBaki.Attributes.Add("readonly", "readonly")

            Dim strStaffID = Session("ssusrID")
            Dim strKodSubMenu As String = Request.QueryString("KodSubMenu")

            If fCheckPowerUser(strStaffID, strKodSubMenu) Then
                ddlPTJ.Enabled = True
                ddlKW.Items.Add(New ListItem("- SILA PILIH PTj -", 0))
            Else
                ddlPTJ.SelectedValue = Session("ssusrKodPTj")
                ddlPTJ.Enabled = False
                fBindDdlKW()
            End If

            ddlPTJ.Enabled = True
            ddlKW.Items.Add(New ListItem("- SILA PILIH PTj -", 0))
            ddlKO.Items.Add(New ListItem("- SILA PILIH KW -", 0))
            ddlKP.Items.Add(New ListItem("- SILA PILIH KO -", 0))
            gvObjSebagai.DataSource = New List(Of String)
            gvObjSebagai.DataBind()

            gvObjAm.DataSource = New List(Of String)
            gvObjAm.DataBind()

        End If
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


    Private Sub fBindDdlPTJ()
        Try
            Dim strSql As String

            'strSql = "select KodPTJ ,(KodPTJ + ' - ' + Butiran ) as ButiranPTj from MK_PTJ where KodPTJ <> '-' and KodKategoriPTJ = 'P' and status = 1 order by KodPTJ "

            strSql = "SELECT DISTINCT TOP 100 PERCENT dbo.MK_PTJ.KodPTJ,(MK_PTJ.KodPTJ + ' - ' + dbo.MK_PTJ.Butiran) as Butiran
FROM dbo.MK_PTJ INNER JOIN dbo.MK01_VotTahun ON dbo.MK_PTJ.KodPTJ = dbo.MK01_VotTahun.KodPTJ 
WHERE dbo.MK01_VotTahun.mk01_tahun = '" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' order by KodPtj"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fselectCommand(strSql)
            ddlPTJ.DataSource = ds
            ddlPTJ.DataTextField = "Butiran"
            ddlPTJ.DataValueField = "KodPTJ"
            ddlPTJ.DataBind()

            ddlPTJ.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlPTJ.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindDdlKW()
        Try

            'Dim strSql As String = "Select KodKw,Butiran,(KodKw + ' - ' + Butiran ) as ButiranKW from MK_Kw order by KodKw"
            Dim strSql As String = "SELECT DISTINCT TOP 100 PERCENT dbo.MK_Kw.KodKw ,dbo.MK_Kw.Butiran
FROM dbo.MK_Kw INNER JOIN dbo.MK01_VotTahun ON dbo.MK_Kw.KodKw  = dbo.MK01_VotTahun.KodKw  
WHERE dbo.MK01_VotTahun.mk01_tahun = '" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' and dbo.MK01_VotTahun.KodPTJ  = '" & Trim(ddlPTJ.SelectedValue.TrimEnd) & "' order by KodKw"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn

            ds = dbconn.fselectCommand(strSql)

            Dim dvKW As New DataView
            dvKW = New DataView(ds.Tables(0))
            ViewState("dvkW") = dvKW.Table

            ddlKW.DataSource = ds
            ddlKW.DataTextField = "Butiran"
            ddlKW.DataValueField = "KodKw"
            ddlKW.DataBind()

            ddlKW.Items.Insert(0, New ListItem("- SILA PILIH -", "0"))
            ddlKW.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub



    Private Sub fLoadKP()
        Try

            Dim strSql As String = "select KodProjek, Butiran from MK_KodProjek"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn

            ds = dbconn.fselectCommand(strSql)

            Dim dvKP As New DataView
            dvKP = New DataView(ds.Tables(0))
            ViewState("dvKP") = dvKP.Table

        Catch ex As Exception

        End Try
    End Sub

    Private Function fBindGvObjAM()

        Try
            Dim strSql As String
            Dim strFilter As String

            Dim dt As New DataTable

            dt.Columns.Add("KW", GetType(String))
            dt.Columns.Add("kodKO", GetType(String))
            dt.Columns.Add("PTj", GetType(String))
            dt.Columns.Add("KodKP", GetType(String))
            dt.Columns.Add("ObjAm", GetType(String))
            dt.Columns.Add("Bajet", GetType(String))
            dt.Columns.Add("Tambahan", GetType(String))
            dt.Columns.Add("Kurangan", GetType(String))
            dt.Columns.Add("BakiBF", GetType(String))
            dt.Columns.Add("Jumlah", GetType(String))
            dt.Columns.Add("IndObjAm", GetType(String))

            Dim strJumPTj As String
            Dim strTahun As String = Trim(ddlTahunAgih.SelectedValue.TrimEnd)

            Dim strKodPTj As String = Trim(ddlPTJ.SelectedValue.TrimEnd)
            Dim strKodKW As String = Trim(ddlKW.SelectedValue.TrimEnd)
            Dim strKodKO As String = Trim(ddlKO.SelectedValue.TrimEnd)
            Dim strKodKP As String = Trim(ddlKP.SelectedValue.TrimEnd)

            If strKodKO <> "0" Then
                strFilter = " and kodKO = '" & strKodKO & "'"
            End If

            If strKodKP <> "0" Then
                strFilter = strFilter & " and KodKP = '" & strKodKP & "'"
            End If

            strSql = "select sum(bg05_amaun) as JumPtj from bg05_agihptj where bg05_tahun = '" & strTahun & "' and kodptj = '" & strKodPTj & "' and kodagih in ('AL','TB','KG') and kodKW = '" & strKodKW & "' " '& strFilter

            'trackno = 2
            Dim ds1 As New DataSet
            Dim dbconn As New DBKewConn
            ds1 = dbconn.fSelectCommand(strSql)
            If Not ds1 Is Nothing Then
                'trackno = 3
                strJumPTj = ds1.Tables(0).Rows(0)(0).ToString()

                If strJumPTj = "" Then
                    fGlobalAlert("Tiada peruntukan untuk " & ddlKW.SelectedItem.Text & ".", Me.Page, Me.[GetType]())
                    sClearGvObjAm()
                    sClearGvObjSbg()
                    sClearField()
                    Exit Function
                End If

                strSql = "select sum(bg06_amaun) as JumAgih from bg06_agihobjam where bg06_tahun = '" & strTahun & "' and kodptj = '" & strKodPTj & "' and kodagih in ('AL','TB','KG') and kodKW = '" & strKodKW & "' " ' & strFilter

                Dim strJumAgih As String
                Dim ds2 As New DataSet
                dbconn = New DBKewConn
                ds2 = dbconn.fSelectCommand(strSql)

                'trackno = 4
                If Not ds2 Is Nothing Then
                    If ds2.Tables(0).Rows.Count <> 0 Then
                        strJumAgih = ds2.Tables(0).Rows(0)("JumAgih").ToString()
                        If strJumAgih = "" Then
                            fGlobalAlert("Peruntukan belum selesai diagihkan untuk " & ddlKW.SelectedItem.Text & ".", Me.Page, Me.[GetType]())
                            sClearGvObjAm()
                            sClearGvObjSbg()
                            sClearField()
                            Exit Function
                        End If

                        If strJumPTj <> "" AndAlso strJumAgih <> "" Then
                            If strJumPTj = strJumAgih Then
                                'strSql = "Select a.BG06_IndObjAm,sum(a.BG06_amaun) As JumObjAm,a.kodkw,a.KodKO , a.kodkp, a.kodvot, " &
                                '    "(select (c.kodvot + ' - ' + c.butiran) as ObjAm from mk_vot c where c.KodVot =  a.kodvot) as ObjAm, a.kodptj  " &
                                '    "from BG06_AgihObjAm a  where a.BG06_tahun = '" & strTahun & "' and " &
                                '   " a.kodKW = '" & strKodKW & "' and a.kodptj = '" & strKodPTj & "' " & strFilter & " group by a.BG06_IndObjAm, a.kodkw,a.KodKO,a.kodkp,a.kodvot,a.kodptj HAVING (SUM(a.BG06_Amaun) <> 0) " &
                                '    "order by a.kodkw,a.KodKO,a.kodptj,a.kodkp,a.kodvot"

                                strSql = "select a.BG06_IndObjAm,sum(a.BG06_amaun) As JumObjAm,a.kodkw,a.KodKO , a.kodkp, a.kodvot, (select (c.kodvot + ' - ' + c.butiran) as ObjAm from mk_vot c where c.KodVot =  a.kodvot) as ObjAm, a.kodptj
from BG06_AgihObjAm a
where BG06_Tahun = '" & strTahun & "' and KodKw = '" & strKodKW & "' and KodPTJ = '" & strKodPTj & "' and KodAgih = 'AL' and a.BG06_Amaun <> 0
group by a.BG06_IndObjAm, a.kodkw,a.KodKO,a.kodkp,a.kodvot,a.kodptj 
order by a.kodkw,a.KodKO,a.kodptj,a.kodkp,a.kodvot"

                                Dim ds3 As New DataSet
                                dbconn = New DBKewConn
                                ds3 = dbconn.fSelectCommand(strSql)

                                If Not ds3 Is Nothing Then
                                    Dim strIndObjAm As String
                                    Dim kodkw2 As String
                                    Dim kodKO As String
                                    Dim kodKP As String
                                    Dim kodvot As String
                                    Dim kodptj As String
                                    Dim ObjAm As String
                                    Dim JumBajet As String
                                    Dim JumTamb As String
                                    Dim JumKurang As String
                                    Dim JumBF As String
                                    Dim jumObjAm As String
                                    Dim decJumBajet As Decimal
                                    Dim decJumTamb As Decimal
                                    Dim decJumKurang As Decimal
                                    Dim decJumBF As Decimal
                                    Dim decJumObjAm As Decimal

                                    For i As Integer = 0 To ds3.Tables(0).Rows.Count - 1
                                        kodkw2 = ds3.Tables(0).Rows(i)("kodkw")
                                        kodKO = ds3.Tables(0).Rows(i)("KodKO")
                                        kodptj = ds3.Tables(0).Rows(i)("kodptj")
                                        kodKP = ds3.Tables(0).Rows(i)("KodKP")
                                        kodvot = ds3.Tables(0).Rows(i)("kodvot")
                                        ObjAm = ds3.Tables(0).Rows(i)("ObjAm")

                                        decJumBajet = fGetJumBajet(kodkw2, kodKO, kodptj, kodKP, kodvot)
                                        JumBajet = decJumBajet.ToString("#,##0.00")
                                        decJumTamb = fGetJumTamb(kodkw2, kodKO, kodptj, kodKP, kodvot)
                                        JumTamb = decJumTamb.ToString("#,##0.00")
                                        decJumKurang = fGetJumKurang(kodkw2, kodKO, kodptj, kodKP, kodvot)
                                        JumKurang = decJumKurang.ToString("#,##0.00")
                                        decJumBF = fGetJumBF(kodkw2, kodKO, kodptj, kodKP, kodvot)
                                        JumBF = decJumBF.ToString("#,##0.00")
                                        decJumObjAm = decJumBajet + decJumTamb + decJumBF - decJumKurang
                                        jumObjAm = decJumObjAm.ToString("#,##0.00")
                                        strIndObjAm = ds3.Tables(0).Rows(i)("BG06_IndObjAm")
                                        dt.Rows.Add(kodkw2, kodKO, kodptj, kodKP, ObjAm, JumBajet, JumTamb, JumKurang, JumBF, jumObjAm, strIndObjAm)
                                    Next
                                    ViewState("dtObjAm") = dt

                                    lblJumRekod.InnerText = dt.Rows.Count
                                    gvObjAm.DataSource = dt
                                    gvObjAm.DataBind()

                                    fBindDdlKO()

                                End If
                            Else
                                fGlobalAlert("Agihan peruntukan belum selesai untuk " & ddlKW.SelectedItem.Text & " dalam PTj", Me.Page, Me.[GetType]())
                                sClearGvObjAm()
                                sClearGvObjSbg()
                                sClearField()
                                Exit Function
                            End If
                        End If
                    End If
                End If
            End If



        Catch ex As Exception

        End Try

    End Function

    Private Sub sClearGvObjAm()
        gvObjAm.DataSource = New List(Of String)
        gvObjAm.DataBind()
    End Sub

    Private Sub sClearGvObjSbg()
        gvObjSebagai.DataSource = New List(Of String)
        gvObjSebagai.DataBind()
    End Sub


    ''' <summary>
    ''' BAJET ASAL OBJEK AM
    ''' </summary>
    ''' <param name="strKodKW"></param>
    ''' <param name="strKodPTj"></param>
    ''' <param name="strKodVot"></param>
    ''' <returns></returns>
    Private Function fGetJumBajet(ByVal strKodKW As String, ByVal strKodKO As String, ByVal strKodPTj As String, ByVal strKodKP As String, ByVal strKodVot As String) As Decimal
        Try
            Dim strSql As String = "SELECT sum(bg06_amaun) as JumObjAm,kodkw,kodvot FROM bg06_agihobjam where bg06_tahun = '" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' " &
                " and kodkw='" & strKodKW & "' and KodKO = '" & strKodKO & "' And kodptj = '" & strKodPTj & "' and kodKP = '" & strKodKP & "'   and kodvot='" & strKodVot & "' and kodagih='AL'" &
                "group by kodkw,kodvot HAVING (SUM(BG06_Amaun) <> 0) order by kodkw,kodvot"

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

    Private Function fGetJumBF(ByVal strKodKW As String, ByVal strKodKO As String, ByVal strKodptj As String, ByVal strKodKP As String, ByVal strKodvot As String) As Decimal
        Try
            Dim strSql As String = "SELECT sum(mk09_debit) as JumBF FROM mk09_bajetbf WHERE mk09_Tahun='" & ddlTahunAgih.SelectedValue.TrimEnd & "' " &
                "And kodkw='" & strKodKW & "' And KodKO='" & strKodKO & "' and kodptj='" & strKodptj & "' and kodKP = '" & strKodKP & "' and left(kodvot,1)='" & strKodvot.Substring(0, 1) & "'"

            Dim strJumBF As Decimal = 0.00
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strJumBF = IIf(IsDBNull(ds.Tables(0).Rows(0)("JumBF")), 0.00, ds.Tables(0).Rows(0)("JumBF"))
                End If
            End If

            Return strJumBF
        Catch ex As Exception

        End Try
    End Function
    ''' <summary>
    ''' BAJET TAMBAHAN OBJEK AM
    ''' </summary>
    ''' <param name="strKodKw"></param>
    ''' <param name="strKodPtj"></param>
    ''' <param name="strKodVot"></param>
    ''' <returns></returns>
    Private Function fGetJumTamb(ByVal strKodKw As String, ByVal strKodKO As String, ByVal strKodPtj As String, ByVal strKodKP As String, ByVal strKodVot As String) As Decimal
        Try
            strKodVot = strKodVot.Substring(0, 1)
            Dim strSql As String = "SELECT sum(bg07_amaun) as jumtbh FROM bg07_agihobjsbg " &
            "WHERE BG07_Tahun='" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' and kodkw = '" & strKodKw & "' and KodKO = '" & strKodKO & "' " &
            "And kodptj = '" & strKodPtj & "' and KodKP = '" & strKodKP & "' and left(kodvot,1)='" & strKodVot & "' and kodagih='TB' and bg07_status='1' and bg07_statlulus='1'"

            Dim strJumtbh As Decimal = 0.00
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strJumtbh = IIf(IsDBNull(ds.Tables(0).Rows(0)("jumtbh")), 0.00, ds.Tables(0).Rows(0)("jumtbh"))
                End If
            End If

            Return strJumtbh
        Catch ex As Exception

        End Try
    End Function
    ''' <summary>
    ''' BAJET KURANGAN OBJEK AM
    ''' </summary>
    ''' <param name="strKodkw"></param>
    ''' <param name="strKodptj"></param>
    ''' <param name="strKodvot"></param>
    ''' <returns></returns>
    Private Function fGetJumKurang(ByVal strKodkw As String, ByVal strKodKO As String, ByVal strKodptj As String, ByVal strKodKP As String, ByVal strKodvot As String) As Decimal
        Try
            Dim strSql As String = "SELECT SUM(dbo.BG07_AgihObjSbg.BG07_Amaun) AS JumKrg FROM dbo.BG06_AgihObjAm INNER JOIN dbo.BG07_AgihObjSbg " &
                "ON dbo.BG06_AgihObjAm.BG06_IndObjAm = dbo.BG07_AgihObjSbg.BG06_IndObjAm " &
                "WHERE (dbo.BG06_AgihObjAm.BG06_Tahun = '" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "') AND (dbo.BG06_AgihObjAm.KodKw = '" & strKodkw & "') AND (dbo.BG06_AgihObjAm.KodKO = '" & strKodKO & "') " &
                "And (dbo.BG06_AgihObjAm.KodPTJ = '" & strKodptj & "') AND (dbo.BG06_AgihObjAm.KodKP = '" & strKodKP & "') AND (dbo.BG06_AgihObjAm.KodVot = '" & strKodvot & "') " &
                "And (dbo.BG06_AgihObjAm.KodAgih = 'KG') AND (dbo.BG06_AgihObjAm.BG06_Status = '1') " &
                "And (dbo.BG07_AgihObjSbg.BG07_StatLulus = 1) Having (Sum(dbo.BG06_AgihObjAm.BG06_Amaun) <> 0)"

            Dim DecJumKurang As Decimal = 0.00
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    DecJumKurang = IIf(IsDBNull(ds.Tables(0).Rows(0)("JumKrg")), 0.00, ds.Tables(0).Rows(0)("JumKrg"))
                End If
            End If

            Return DecJumKurang
        Catch ex As Exception

        End Try
    End Function

    Private Sub gvObjAm_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvObjAm.RowCreated

        Try
            If e.Row.RowType = DataControlRowType.Footer Then
                Dim intNoOfMergeCol As Integer = 7
                For intCellCol As Integer = 1 To intNoOfMergeCol - 1
                    e.Row.Cells.RemoveAt(1)
                Next
                e.Row.Cells(0).ColumnSpan = intNoOfMergeCol
                e.Row.Cells(0).Text = "Jumlah Besar (RM)"
                e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(0).Font.Bold = True

            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub gvObjAm_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvObjAm.RowDataBound
        Try
            'If e.Row.RowType = DataControlRowType.DataRow Then
            '    e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvObjAm, "Select$" & e.Row.RowIndex)
            '    e.Row.ToolTip = "Klik untuk pilih rekod ini."
            '    e.Row.ForeColor = ColorTranslator.FromHtml("#000000")
            'End If



            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim strKW As String = DataBinder.Eval(e.Row.DataItem, "KW").ToString()
                Dim strKO As String = DataBinder.Eval(e.Row.DataItem, "kodKO").ToString()
                Dim strPTj As String = DataBinder.Eval(e.Row.DataItem, "PTJ").ToString()
                Dim strKP As String = DataBinder.Eval(e.Row.DataItem, "kodKP").ToString()
                Dim strObjAm As String = DataBinder.Eval(e.Row.DataItem, "ObjAm").ToString.Substring(0, 5)

                Dim decJumAgih As Decimal = CDec(fCheckAgih(strKW, strKO, strPTj, strKP, strObjAm))

                Dim decJumBajet As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Bajet").ToString())
                Dim decJumTambahan As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Tambahan").ToString())
                Dim decJumKurangan As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Kurangan").ToString())
                Dim decBakiBF As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BakiBF").ToString())
                Dim decBaki As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Jumlah").ToString())
                decTotBajet += decJumBajet
                decTotTambahan += decJumTambahan
                decTotKurangan += decJumKurangan
                decTotBakiBF += decBakiBF
                decTotJumlah += decBaki


                Dim strTotBajet As String = decTotBajet.ToString("#,##0.00")
                ViewState("strTotBajet") = strTotBajet
                Dim strTotTambahan As String = decTotTambahan.ToString("#,##0.00")
                ViewState("strTotTambahan") = strTotTambahan
                Dim strTotKurangan As String = decTotKurangan.ToString("#,##0.00")
                ViewState("strTotKurangan") = strTotKurangan
                Dim strTotBakiBF As String = decTotBakiBF.ToString("#,##0.00")
                ViewState("strTotBakiBF") = strTotBakiBF
                Dim strTotJumlah As String = decTotJumlah.ToString("#,##0.00")
                ViewState("strTotJumlah") = strTotJumlah

                If decJumAgih = decJumBajet Then
                    'e.Row.ForeColor = Color.Blue
                    e.Row.FindControl("btnAgih1").Visible = True
                    e.Row.FindControl("btnAgih2").Visible = False
                Else
                    e.Row.FindControl("btnAgih1").Visible = False
                    e.Row.FindControl("btnAgih2").Visible = True
                End If

            ElseIf e.Row.RowType = DataControlRowType.Footer Then
                e.Row.Cells(1).Text = ViewState("strTotBajet")
                e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(1).Font.Bold = True
                e.Row.Cells(2).Text = ViewState("strTotTambahan").ToString()
                e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(2).Font.Bold = True
                e.Row.Cells(3).Text = ViewState("strTotKurangan").ToString()
                e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(3).Font.Bold = True
                e.Row.Cells(4).Text = ViewState("strTotBakiBF").ToString()
                e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(4).Font.Bold = True
                e.Row.Cells(5).Text = ViewState("strTotJumlah").ToString()
                e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(5).Font.Bold = True
            End If




        Catch ex As Exception

        End Try
    End Sub

    Dim TotBajet, TotTamb, TotKurg, TotBaki, TotJum As Decimal
    Dim strTotBajet, strTotTamb, strTotKurg, strTotBaki, strTotJum As String
    Protected Sub gvObjSebagai_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvObjSebagai.RowDataBound
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.ForeColor = ColorTranslator.FromHtml("#000000")

                Dim txtBajet As TextBox = CType(e.Row.FindControl("txtBajet"), TextBox)
                TotBajet += Decimal.Parse(txtBajet.Text)
                strTotBajet = TotBajet.ToString("#,##0.00")
                txtBajet.Style("text-align") = "right"

                Dim lblTamb As Label = CType(e.Row.FindControl("lblTamb"), Label)
                TotTamb += Decimal.Parse(lblTamb.Text)
                strTotTamb = TotTamb.ToString("#,##0.00")

                Dim lblKurg As Label = CType(e.Row.FindControl("lblKurg"), Label)
                TotKurg += Decimal.Parse(lblKurg.Text)
                strTotKurg = TotKurg.ToString("#,##0.00")

                Dim lblBaki As Label = CType(e.Row.FindControl("lblBaki"), Label)
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

                Dim lblTotTamb As Label = CType(e.Row.FindControl("lblTotTamb"), Label)
                lblTotTamb.Text = strTotTamb.ToString()
                lblTotTamb.Font.Bold = True

                Dim lblTotKurg As Label = CType(e.Row.FindControl("lblTotKurg"), Label)
                lblTotKurg.Text = strTotKurg.ToString()
                lblTotKurg.Font.Bold = True

                Dim lblTotBaki As Label = CType(e.Row.FindControl("lblTotBaki"), Label)
                lblTotBaki.Text = strTotBaki.ToString()
                lblTotBaki.Font.Bold = True

                Dim lblTotJum As Label = CType(e.Row.FindControl("lblTotJum"), Label)
                lblTotJum.Text = strTotJum.ToString()
                lblTotJum.Font.Bold = True
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Function fBindGvObjSebagai(ByVal intInd As Integer)
        Dim strSql As String
        Dim blnFound As Boolean = True

        sClearGvObjSbg()
        Try

            Dim strKW As String = gvObjAm.Rows(intInd).Cells(2).Text.ToString
            Dim strKO As String = gvObjAm.Rows(intInd).Cells(3).Text.ToString
            Dim strPTj As String = gvObjAm.Rows(intInd).Cells(4).Text.ToString
            Dim strKP As String = gvObjAm.Rows(intInd).Cells(5).Text.ToString
            Dim strKodVot As String = gvObjAm.Rows(intInd).Cells(6).Text.ToString

            Dim dt As New DataTable
            dt.Columns.Add("KodSebagai", GetType(String))
            dt.Columns.Add("ObjSebagai", GetType(String))
            dt.Columns.Add("Bajet", GetType(String))
            dt.Columns.Add("Tambahan", GetType(String))
            dt.Columns.Add("Kurangan", GetType(String))
            dt.Columns.Add("BakiBF", GetType(String))
            dt.Columns.Add("Jumlah", GetType(String))

            strSql = "SELECT DISTINCT TOP 100 PERCENT dbo.MK_Vot.KodVot,  dbo.MK_Vot.Butiran " &
            "FROM dbo.MK_Vot INNER JOIN dbo.MK01_VotTahun ON dbo.MK_Vot.KodVot = dbo.MK01_VotTahun.KodVot " &
            "WHERE (dbo.MK_Vot.Klasifikasi = 'H2') AND (dbo.MK01_VotTahun.MK01_Status = '1') and " &
            "dbo.MK01_VotTahun.mk01_tahun = '" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' and dbo.MK01_VotTahun.kodkw ='" & strKW & "'  and dbo.MK01_VotTahun.kodptj='" & strPTj & "' " &
            "And dbo.MK01_VotTahun.KodKO ='" & strKO & "' and dbo.MK01_VotTahun.KodKP  ='" & strKP & "' " &
            "and left(dbo.MK_Vot.kodvot,1) = '" & strKodVot.Substring(0, 1) & "' ORDER BY dbo.MK_Vot.KodVot"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim KodSbg As String
                    Dim JumBajet, JumTamb, JumKurang, JumBakiBF, JumObjSbg As String
                    Dim decJumBajet, decJumTamb, decJumKurang, decJumBakiBF, decJumObjSbg As Decimal
                    Dim Butiran As String
                    Dim strAgihan, strBaki As String
                    Dim decAgihan, decBaki As Decimal

                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        KodSbg = ds.Tables(0).Rows(i)("KodVot")
                        Butiran = Context.Server.HtmlDecode(ds.Tables(0).Rows(i)("Butiran"))
                        decJumBajet = fGetBajet(strKW, strKO, strPTj, strKP, KodSbg)
                        JumBajet = decJumBajet.ToString("#,##0.00")
                        decJumTamb = fGetTamb(strKW, strKO, strPTj, strKP, KodSbg)
                        JumTamb = decJumTamb.ToString("#,##0.00")
                        decJumKurang = fGetKurang(strKW, strKO, strPTj, strKP, KodSbg)
                        JumKurang = decJumKurang.ToString("#,##0.00")
                        decJumBakiBF = fGetBakiBF(strKW, strKO, strPTj, strKP, KodSbg)
                        JumBakiBF = decJumBakiBF.ToString("#,##0.00")

                        decJumObjSbg = decJumBajet + decJumTamb + decJumBakiBF - decJumKurang
                        JumObjSbg = decJumObjSbg.ToString("#,##0.00")

                        dt.Rows.Add(KodSbg, KodSbg & " - " & Butiran, JumBajet, JumTamb, JumKurang, JumBakiBF, JumObjSbg)
                        decAgihan = decAgihan + decJumBajet
                    Next

                    ViewState("dtObjSbg") = dt
                    gvObjSebagai.DataSource = dt
                    gvObjSebagai.DataBind()

                    strAgihan = decAgihan.ToString("#,##0.00")
                    txtAgihan.Text = strAgihan

                    Dim decTotBajet As Decimal = CDec(txtBajetAm.Text.ToString)
                    decBaki = decTotBajet - decAgihan
                    strBaki = decBaki.ToString("#,##0.00")
                    txtBaki.Text = strBaki
                Else
                    blnFound = False
                End If
            Else
                blnFound = False
            End If

            If blnFound = False Then
                fGlobalAlert("Carta akaun belum dibina!", Me.Page, Me.[GetType]())
            End If

        Catch ex As Exception

        End Try
    End Function

    Private Function fGetBajet(ByVal strKW As String, ByVal strKO As String, ByVal strPTj As String, ByVal strKP As String, ByVal KodSbg As String) As Decimal
        Try
            Dim strJumObjSbg As Decimal
            Dim strSql As String = "Select sum(bg07_amaun) As JumObjSbg FROM bg07_agihobjsbg WHERE BG07_Tahun='" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' " &
            " And kodkw = '" & strKW & "'  and kodKO = '" & strKO & "' and kodptj = '" & strPTj & "' and kodKP = '" & strKP & "' and kodvot='" & KodSbg & "' and kodagih='AL'"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strJumObjSbg = IIf(IsDBNull(ds.Tables(0).Rows(0)("JumObjSbg")), 0.00, ds.Tables(0).Rows(0)("JumObjSbg"))
                End If
            End If

            Return strJumObjSbg
        Catch ex As Exception

        End Try
    End Function

    Private Function fGetTamb(ByVal strKW As String, ByVal strKO As String, ByVal strPTj As String, ByVal strKP As String, ByVal KodSbg As String) As Decimal
        Try
            Dim strJumTamb As Decimal

            Dim strSql As String = "SELECT sum(bg07_amaun) as JumTbh FROM bg07_agihobjsbg " &
                "WHERE BG07_Tahun='" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' and kodkw = '" & strKW & "' and kodKO = '" & strKO & "' and kodptj = '" & strPTj & "' and kodKP = '" & strKP & "' and kodvot='" & KodSbg & "' " &
                "and kodagih='TB' and bg07_status='1' and bg07_statlulus='1'"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strJumTamb = IIf(IsDBNull(ds.Tables(0).Rows(0)("JumTbh")), 0.00, ds.Tables(0).Rows(0)("JumTbh"))
                End If
            End If

            Return strJumTamb
        Catch ex As Exception

        End Try
    End Function

    Protected Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.Click

        Dim footerRow = gvObjSebagai.FooterRow
        Dim decTotAgih As Decimal = CDec(CType(footerRow.FindControl("lblTotBajet"), Label).Text)

        If decTotAgih <> CDec(txtBajetAm.Text) Then
            fGlobalAlert("Jumlah agihan kepada Objek Sebagai tidak sama dengan jumlah bajet untuk Objek Am ini!", Me.Page, Me.[GetType]())
            Exit Sub
        End If

        If txtKodKW.Text <> "" Then
            If fSimpan() Then
                fGlobalAlert("Maklumat agihan telah disimpan!", Me.Page, Me.[GetType]())
                fBindGvObjAM()
                sClearGvObjSbg()
                sClearField()
            Else
                fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
            End If
        End If

    End Sub

    Private Sub sClearField()
        txtKW.Text = ""
        txtKodKW.Text = ""
        txtKOButiran.Text = ""
        txtKodKO.Text = ""
        txtKodKP.Text = ""
        txtKodKPButiran.Text = ""
        txtKodObjAm.Text = ""
        txtObjAm.Text = ""
        txtBajetAm.Text = ""
        txtAgihan.Text = ""
        txtBaki.Text = ""
    End Sub
    Private Function fGetKurang(ByVal strKW As String, ByVal strKO As String, ByVal strPTj As String, ByVal strKP As String, ByVal KodSbg As String) As Decimal
        Try
            Dim strJumKurang As Decimal


            Dim strSql As String = "SELECT sum(bg07_amaun) as JumKrg FROM bg07_agihobjsbg 
WHERE BG07_Tahun='" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' and kodkw = '" & strKW & "' and kodKO = '" & strKO & "' and kodptj = '" & strPTj & "' and kodKP = '" & strKP & "' and kodvot='" & KodSbg & "' 
and kodagih='KG' and bg07_status='1' and bg07_statlulus='1'"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strJumKurang = IIf(IsDBNull(ds.Tables(0).Rows(0)("JumKrg")), 0.00, ds.Tables(0).Rows(0)("JumKrg"))
                End If
            End If

            Return strJumKurang
        Catch ex As Exception

        End Try
    End Function

    Private Function fSimpan() As Boolean
        Dim dbconn As New DBKewConn
        Dim strSql As String
        Dim blnSuccess As Boolean = True
        Dim strdtToday As String = Now.ToString("yyyy-MM-dd")
        Dim strAmt As String
        Dim decAmt As Decimal
        Dim strKodPTj As String = Trim(ddlPTJ.SelectedValue.TrimEnd.Substring(0, 6))
        Dim strTahun As String = Trim(ddlTahunAgih.SelectedValue.TrimEnd)
        Dim strKodKW As String = Trim(txtKodKW.Text.TrimEnd)
        Dim strKodKO As String = Trim(txtKodKO.Text.TrimEnd)
        Dim strKodKP As String = Trim(txtKodKP.Text.TrimEnd)
        Dim strKodSbg As String
        Dim strIndObjAm As String = Trim(hidIndObjAm.Value.TrimEnd)
        Dim strKodAgih = "AL"

        Try
            For Each row As GridViewRow In gvObjSebagai.Rows
                Dim strIndObjSbg As String = ""

                strAmt = CDec(TryCast(row.FindControl("txtBajet"), TextBox).Text)
                If strAmt = "" Then
                    strAmt = "0"
                End If
                decAmt = CDec(strAmt)
                strKodSbg = TryCast(row.FindControl("ObjSebagai"), Label).Text.ToString.Substring(0, 5)

                dbconn.sConnBeginTrans()

                strSql = "select count(*) from BG07_AgihObjSbg with (NOLOCK) where BG07_Tahun = '" & strTahun & "' and KodKw = '" & strKodKW & "' and KodKO = '" & strKodKO & "' and KodPTJ = '" & strKodPTj & "' and kodkp = '" & strKodKP & "' and KodVot = '" & strKodSbg & "' and kodagih = 'AL' and BG07_Status = 1"
                If fCheckRec(strSql) > 0 Then
                    'UPDATE
                    strSql = " UPDATE BG07_AgihObjSbg SET BG07_Amaun=@Amaun,BG07_TkhAgih=@TkhAgih WHERE BG07_Tahun = @Tahun and KodKw = @KodKW and KodKO = @KodKO and KodPTJ = @KodPTj and kodkp = @KodKP and KodVot = @KodVot and kodagih = @KodAgih and BG07_Status = @Status"
                    Dim paramSql() As SqlParameter = {
                        New SqlParameter("@Amaun", strAmt),
                        New SqlParameter("@TkhAgih", strdtToday),
                        New SqlParameter("@Tahun", strTahun),
                        New SqlParameter("@KodKW", strKodKW),
                        New SqlParameter("@KodKO", strKodKO),
                        New SqlParameter("@KodPTj", strKodPTj),
                        New SqlParameter("@KodKP", strKodKP),
                        New SqlParameter("@KodVot", strKodSbg),
                        New SqlParameter("@KodAgih", strKodAgih),
                        New SqlParameter("@Status", 1)
                        }

                    If Not dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                        blnSuccess = False
                        Exit Try
                    Else
                        'AUDIT LOG
                        sLogMedan = "BG07_Amaun|BG07_TkhAgih|BG07_Tahun|KodKw|KodKO|KodPTJ|kodkp|KodVot|kodagih|BG07_Status"
                        sLogBaru = strAmt & "|" & strdtToday & "|" & strTahun & "|" & strKodKW & "|" & strKodKO & "|" & strKodPTj & "|" & strKodKP & "|" & strKodSbg & "|" & strKodAgih & "|1"

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
                            New SqlParameter("@InfoTable", "BG07_AgihObjSbg"),
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
                    strIndObjSbg = fGetNoSiri("OBJSBG", strTahun)
                    strSql = "insert into BG07_AgihObjSbg (BG06_IndObjAm , BG07_IndObjSbg , BG07_Tahun , KodKw ,KodKO ,KodPTJ ,KodKP, KodVot , BG07_Amaun , KodAgih , BG07_Status , BG07_StatLulus , BG07_TkhAgih ,BG07_Ulasan ) " &
         "values (@IndObjAm, @IndObjSbg, @Tahun, @KodKw, @KodKo, @KodPTj, @KodKp, @KodVot, @Amaun, @KodAgih, @Status, @Statlulus, @TkhAgih, @Ulasan)"

                    Dim paramSql() As SqlParameter = {
                        New SqlParameter("@IndObjAm", strIndObjAm),
                        New SqlParameter("@IndObjSbg", strIndObjSbg),
                        New SqlParameter("@Tahun", strTahun),
                        New SqlParameter("@KodKw", strKodKW),
                        New SqlParameter("@KodKo", strKodKO),
                        New SqlParameter("@KodPTj", strKodPTj),
                        New SqlParameter("@KodKp", strKodKP),
                        New SqlParameter("@KodVot", strKodSbg),
                        New SqlParameter("@Amaun", decAmt),
                         New SqlParameter("@KodAgih", strKodAgih),
                        New SqlParameter("@Status", 1),
                        New SqlParameter("@Statlulus", 0),
                        New SqlParameter("@TkhAgih", strdtToday),
                        New SqlParameter("@Ulasan", "-")}


                    If Not dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                        blnSuccess = False
                        Exit Try
                    Else
                        'AUDIT LOG
                        sLogMedan = "BG06_IndObjAm|BG07_IndObjSbg|BG07_Tahun|KodKw|KodKO|KodPTJ|KodKP|KodVot|BG07_Amaun|KodAgih| BG07_Status|BG07_StatLulus|BG07_TkhAgih|BG07_Ulasan "
                        sLogBaru = strIndObjAm & "|" & strIndObjSbg & "|" & strTahun & "|" & strKodKW & "|" & strKodKO & "|" & strKodPTj & "|" & strKodKP & "|" & strKodSbg & "|" & decAmt & "|" & strKodAgih & "|1|0|" & strdtToday & "|-"

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
                            New SqlParameter("@InfoTable", "BG07_AgihObjSbg"),
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

        Catch ex As Exception
            blnSuccess = False
        End Try

        If blnSuccess = True Then
            dbconn.sConnCommitTrans()
            Return True
        Else
            dbconn.sConnRollbackTrans()
            Return False
        End If
    End Function

    Private Function fGetBakiBF(ByVal strKW As String, ByVal strKO As String, ByVal strPTj As String, ByVal strKP As String, ByVal KodSbg As String) As Decimal
        Try
            Dim strJumBF As Decimal

            Dim strSql As String = "SELECT sum(mk09_debit) as JumBF FROM mk09_bajetbf WHERE mk09_Tahun='" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' and kodkw='" & strKW & "' " &
            " And kodKO ='" & strKO & "' and kodptj='" & strPTj & "' and kodKP = '" & strKP & "' and kodvot='" & KodSbg & "'"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strJumBF = IIf(IsDBNull(ds.Tables(0).Rows(0)("JumBF")), 0.00, ds.Tables(0).Rows(0)("JumBF"))
                End If
            End If

            Return strJumBF
        Catch ex As Exception

        End Try
    End Function

    Private Sub gvObjAm_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvObjAm.RowCommand
        Try
            If e.CommandName = "Select" Then
                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvObjAm.Rows(index)

                Dim lblIndObjAm As Label = CType(selectedRow.FindControl("lblIndObjAm"), Label)
                Dim strIndObjAm As String = Trim(lblIndObjAm.Text.TrimEnd)
                hidIndObjAm.Value = strIndObjAm

                Dim strKodKW As String = selectedRow.Cells(2).Text
                Dim strKodKO As String = selectedRow.Cells(3).Text
                Dim strPTj As String = selectedRow.Cells(4).Text
                Dim strKodKP As String = selectedRow.Cells(5).Text

                Dim strObjAm As String = selectedRow.Cells(6).Text
                Dim strBajet As String = selectedRow.Cells(7).Text
                Dim strTambahan As String = selectedRow.Cells(8).Text
                Dim strKurangan As String = selectedRow.Cells(9).Text
                Dim strBakiBF As String = selectedRow.Cells(10).Text
                Dim strJumlah As String = selectedRow.Cells(11).Text

                Dim dvKW As New DataView
                dvKW = New DataView(ViewState("dvkW"))
                dvKW.RowFilter = "KodKW = '" & strKodKW & "'"
                txtKodKW.Text = strKodKW
                txtKW.Text = dvKW.Item(0)("Butiran").ToString

                Dim dvKO As New DataView
                dvKO = New DataView(ViewState("dvKO"))
                dvKO.RowFilter = "KodKO = '" & strKodKO & "'"
                txtKodKO.Text = strKodKO
                txtKOButiran.Text = dvKO.Item(0)("Butiran").ToString

                Dim dvKP As New DataView
                dvKP = New DataView(ViewState("dvKP"))
                dvKP.RowFilter = "KodProjek = '" & strKodKP & "'"
                txtKodKP.Text = strKodKP
                txtKodKPButiran.Text = dvKP.Item(0)("Butiran").ToString

                txtKodObjAm.Text = strObjAm.Substring(0, 5)
                txtObjAm.Text = strObjAm.Substring(8, strObjAm.Length - 8)

                txtBajetAm.Text = strBajet
                gvObjSebagai.EditIndex = -1
                fBindGvObjSebagai(index)
            End If

        Catch ex As Exception

        End Try
    End Sub




    'Private Function fGetIndexObjSbg() As String
    '    Try
    '        Dim intInd As Integer = 0
    '        Dim strIndex As String
    '        Dim strSql As String
    '        Dim strTahun As String = Trim(txtTahun.Text.TrimEnd)
    '        strSql = "select top 1 bg07_indobjsbg from bg07_agihobjsbg where bg07_tahun='" & strTahun & "'  order by bg07_indobjsbg desc"
    '        Dim ds As New DataSet
    '        Dim dbconn As New DBKewConn

    '        ds = dbconn.fselectCommand(strSql)

    '        Dim rec As String
    '        If ds.Tables(0).Rows.Count = 0 Then
    '            strIndex = strTahun + "S" + String.Format("{0:0000}", intInd)
    '        Else
    '            strIndex = ds.Tables(0).Rows(0).Item("bg07_indobjsbg")
    '            intInd = strIndex.Substring(strIndex.Length - 5)
    '            intInd += 1
    '            strIndex = strTahun + "S" + String.Format("{0:00000}", intInd)
    '        End If
    '        Return strIndex
    '    Catch ex As Exception

    '    End Try
    'End Function
    Private Sub BindData()
        gvObjSebagai.DataSource = ViewState("dtObjSbg")
        gvObjSebagai.DataBind()

    End Sub



    Private Sub ddlKW_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKW.SelectedIndexChanged
        Try
            If ddlKW.SelectedIndex <> 0 Then
                fBindGvObjAM()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlPTJ_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPTJ.SelectedIndexChanged
        If ddlPTJ.SelectedValue = 0 Then
            ddlKW.Items.Clear()
            ddlKW.Items.Add(New ListItem("- SILA PILIH PTj -", 0))
        Else
            fBindDdlKW()
        End If

    End Sub



    Private Sub lbtnTapis_Click(sender As Object, e As EventArgs) Handles lbtnTapis.Click
        fBindGvObjAM()
    End Sub

    Private Sub ddlKO_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKO.SelectedIndexChanged
        ModalPopupExtender1.Show()
        fBindDdlKP()
    End Sub

    Protected Sub txtBajet_TextChanged(sender As Object, e As EventArgs)

        Try
            Dim footerRow = gvObjSebagai.FooterRow

            Dim txtBajet As TextBox = CType(sender, TextBox)
            Dim gvr As GridViewRow = CType(txtBajet.Parent.Parent, GridViewRow)
            Dim rowindex As Integer = gvr.RowIndex
            Dim selectedRow As GridViewRow = gvObjSebagai.Rows(gvr.RowIndex)

            Dim lblJum As Label = CType(selectedRow.FindControl("lblJum"), Label)

            If txtBajet.Text = "" Then txtBajet.Text = "0.00"
            Dim decBajet As Decimal = CDec(txtBajet.Text)
            txtBajet.Text = FormatNumber(decBajet)

            'Semak jumlah besar
            Dim decJumBajetPTj As Decimal
            Dim decBajetPTj As Decimal
            For Each gvRow As GridViewRow In gvObjSebagai.Rows
                decBajetPTj = Trim(CType(gvRow.FindControl("txtBajet"), TextBox).Text.TrimEnd)
                decJumBajetPTj += decBajetPTj 'Total Jumlah Bajet PTj
            Next

            Dim decTotJum As Decimal
            If decJumBajetPTj > CDec(txtBajetAm.Text) Then
                fGlobalAlert("Amaun yang dimasukkan melebihi jumlah bajet Objek Am ini! Sila masukkan amaun lain.", Me.Page, Me.[GetType]())
                txtBajet.Text = "0.00"
            End If

            decJumBajetPTj = 0
            'Kira Jumlah Besar PTj
            Dim decTbPTj As Decimal = Trim(CType(selectedRow.FindControl("lblTamb"), Label).Text.TrimEnd) 'amaun Tambahan
            Dim decKgPTj As Decimal = Trim(CType(selectedRow.FindControl("lblKurg"), Label).Text.TrimEnd) 'amaun Kurangan
            Dim decBakiBfPTj As Decimal = Trim(CType(selectedRow.FindControl("lblBaki"), Label).Text.TrimEnd) 'amaun Baki BF

            Dim decJumPTj As Decimal = CDec(txtBajet.Text) + decTbPTj - decKgPTj + decBakiBfPTj
            lblJum.Text = FormatNumber(decJumPTj)

            'Kira Jumlah Besar
            For Each gvRow As GridViewRow In gvObjSebagai.Rows
                decBajetPTj = Trim(CType(gvRow.FindControl("txtBajet"), TextBox).Text.TrimEnd)
                decJumBajetPTj += decBajetPTj 'Total Jumlah Bajet

                Dim decJumlah As Decimal = Trim(CType(gvRow.FindControl("lblJum"), Label).Text.TrimEnd)
                decTotJum += decJumlah 'Total Jumlah Besar
            Next

            CType(footerRow.FindControl("lblTotBajet"), Label).Text = FormatNumber(decJumBajetPTj) 'Total Jumlah Besar Bajet
            CType(footerRow.FindControl("lblTotJum"), Label).Text = FormatNumber(decTotJum) 'Total Jumlah Besar

        Catch ex As Exception

        End Try


    End Sub

    Private Sub btnFilter_ServerClick(sender As Object, e As EventArgs) Handles btnFilter.ServerClick
        If gvObjAm.Rows.Count > 0 Then
            ModalPopupExtender1.Show()
        End If

    End Sub

    Private Function fCheckAgih(ByVal strKW As String, ByVal strKO As String, ByVal strPTj As String, ByVal strKP As String, ByVal strObjAm As String) As Decimal
        Try
            Dim strJumObjSbg As String
            Dim strSql As String = "Select sum(bg07_amaun) As JumObjSbg FROM bg07_agihobjsbg WHERE BG07_Tahun='" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' " &
                " And kodkw = '" & strKW & "' and kodko = '" & strKO & "' and kodptj = '" & strPTj & "' and kodKP = '" & strKP & "' and left(kodvot,1)='" & strObjAm.Substring(0, 1) & "' and kodagih='AL'"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn

            ds = dbconn.fselectCommand(strSql)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strJumObjSbg = ds.Tables(0).Rows(0)("JumObjSbg").ToString
                    Return CDec(IIf(strJumObjSbg = "", "0", strJumObjSbg))
                End If
            End If

        Catch ex As Exception

        End Try

    End Function

    Private Sub btnCancel_ServerClick(sender As Object, e As EventArgs) Handles btnCancel.ServerClick
        ddlKO.SelectedIndex = 0
        ModalPopupExtender1.Hide()
    End Sub



    Private Sub fBindDdlKO()
        Try

            'Dim strSql As String = "select KodKO,(KodKO + ' - ' + Butiran ) as ButiranKO from MK_KodOperasi "
            Dim strSql As String = "SELECT DISTINCT TOP 100 PERCENT dbo.MK_kodOperasi.KodKO, dbo.MK_kodOperasi.Butiran " &
                "FROM dbo.MK_kodOperasi INNER JOIN dbo.MK01_VotTahun ON dbo.MK_kodOperasi.KodKO = dbo.MK01_VotTahun.KodKO " &
                "where dbo.MK01_VotTahun.MK01_Tahun = '" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' and dbo.mk01_votTahun.KodKw = '" & ddlKW.SelectedValue & "' and dbo.mk01_votTahun.KodPTj = '" & ddlPTJ.SelectedValue & "' ORDER BY dbo.MK_kodOperasi.Kodko"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn

            ds = dbconn.fselectCommand(strSql)

            Dim dvKW As New DataView
            dvKW = New DataView(ds.Tables(0))
            ViewState("dvKO") = dvKW.Table

            ddlKO.DataSource = ds
            ddlKO.DataTextField = "Butiran"
            ddlKO.DataValueField = "KodKO"
            ddlKO.DataBind()

            ddlKO.Items.Insert(0, New ListItem("- KESELURUHAN -", "0"))
            ddlKO.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindDdlKP()
        Try
            Dim strSql As String

            strSql = "SELECT dbo.MK_KodProjek .KodProjek , (dbo.MK_KodProjek .KodProjek  + ' - ' + dbo.MK_KodProjek.Butiran ) as Butiran  " &
                    "From dbo.MK_KodProjek INNER Join dbo.MK01_VotTahun ON dbo.MK_KodProjek.KodProjek  = dbo.MK01_VotTahun.KodKP   " &
                    "where dbo.MK01_VotTahun.MK01_Tahun = '" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' and dbo.mk01_votTahun.KodKw = '" & ddlKW.SelectedValue & "' and MK01_VotTahun .KodPTJ = '" & ddlPTJ.SelectedValue & "'  ORDER BY dbo.MK_KodProjek.KodProjek"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable

            ds = dbconn.fselectCommand(strSql)
            ddlKP.DataSource = ds
            ddlKP.DataTextField = "Butiran"
            ddlKP.DataValueField = "KodProjek"
            ddlKP.DataBind()

            ddlKP.Items.Insert(0, New ListItem("- KESELURUHAN -", "0"))
            ddlKP.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlKP_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKP.SelectedIndexChanged
        ModalPopupExtender1.Show()
    End Sub

    Private Sub ddlSaizRekod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSaizRekod.SelectedIndexChanged
        Try
            gvObjAm.PageSize = CInt(ddlSaizRekod.Text)
            fBindGvObjAM()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvObjAm_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvObjAm.PageIndexChanging
        Try
            gvObjAm.PageIndex = e.NewPageIndex
            If ViewState("dtObjAm") IsNot Nothing Then
                gvObjAm.DataSource = ViewState("dtObjAm")
                gvObjAm.DataBind()
            Else
                fBindGvObjAM()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvObjAm_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvObjAm.SelectedIndexChanged
        For Each row As GridViewRow In gvObjAm.Rows
            If row.RowIndex = gvObjAm.SelectedIndex Then
                row.ForeColor = ColorTranslator.FromHtml("#0000FF")
                'row.ToolTip = String.Empty
            Else
                row.ForeColor = ColorTranslator.FromHtml("#000000")
                ' row.ToolTip = "Klik untuk pilih rekod ini."
            End If
        Next
    End Sub

    Private Sub ddlTahunAgih_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTahunAgih.SelectedIndexChanged
        fLoadKP()
        fBindDdlPTJ()
        txtKW.Text = ""
        txtObjAm.Text = ""
        txtBajetAm.Text = "0.00"
        txtAgihan.Text = "0.00"
        txtBaki.Text = "0.00"

        txtBaki.Attributes.Add("readonly", "readonly")

        Dim strStaffID = Session("ssusrID")
        Dim strKodSubMenu As String = Request.QueryString("KodSubMenu")

        If fCheckPowerUser(strStaffID, strKodSubMenu) Then
            ddlPTJ.Enabled = True
            ddlKW.Items.Add(New ListItem("- SILA PILIH PTj -", 0))
        Else
            ddlPTJ.SelectedValue = Session("ssusrKodPTj")
            ddlPTJ.Enabled = False
            fBindDdlKW()
        End If

        ddlPTJ.Enabled = True
        ddlKW.Items.Add(New ListItem("- SILA PILIH PTj -", 0))
        ddlKO.Items.Add(New ListItem("- SILA PILIH KW -", 0))
        ddlKP.Items.Add(New ListItem("- SILA PILIH KO -", 0))
        gvObjSebagai.DataSource = New List(Of String)
        gvObjSebagai.DataBind()

        gvObjAm.DataSource = New List(Of String)
        gvObjAm.DataBind()
    End Sub
End Class