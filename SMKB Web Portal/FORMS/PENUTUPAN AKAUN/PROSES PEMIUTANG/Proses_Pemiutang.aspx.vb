Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient

Public Class Proses_Pemiutang
    Inherits System.Web.UI.Page
    Dim RefDateTime As DateTime


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                gvProsesPemiutang.DataSource = New List(Of String)
                gvProsesPemiutang.DataBind()
                fBindDdlKat()
                fBindDddlPTj()
                fBindDdlTapisan()
                fBindGv()
                'If fCheckSetupKW() Then
                '    fBindDddlKW()
                '    ddlPTJ.Items.Insert(0, New ListItem("-SILA PILIH PTj-", "0"))
                'Else
                '    fGlobalAlert("Sila pastikan kaedah proses bawa bajet telah ditetapkan!", Me.Page, Me.[GetType]())
                'End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function fCheckSetupKW() As Boolean
        Try
            Dim strSql As String = "select count(*) from mk08_pindahanbajet where mk08_tahun = '" & Trim(ddlKat.SelectedValue.TrimEnd) & "'"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
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
    Private Sub fBindDdlKat()

        Try
            'Dim yNow As DateTime = Now.AddYears(-1)
            'Dim pYear As String = yNow.Year
            ddlKat.Items.Insert(0, New ListItem("-SILA PILIH-", "0"))
            ddlKat.Items.Insert(1, New ListItem("ILL", "1"))
            ddlKat.Items.Insert(2, New ListItem("IPT", "2"))
            ddlKat.Items.Insert(3, New ListItem("IND", "3"))
            ddlKat.SelectedIndex = 0
            'ddlKW.Items.Insert(0, New ListItem("-SILA PILIH-", "0"))
            'ddlKW.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub
    Private Sub fBindDdlTapisan()

        Try
            'Dim yNow As DateTime = Now.AddYears(-1)
            'Dim pYear As String = yNow.Year
            ddlTapisan.Items.Insert(0, New ListItem("-SILA PILIH-", "0"))
            ddlTapisan.Items.Insert(1, New ListItem("No. Invois Dari", "1"))
            ddlTapisan.Items.Insert(2, New ListItem("Tarikh Terima Invois Dari", "2"))
            ddlTapisan.Items.Insert(3, New ListItem("Pembekal Dari", "3"))
            ddlTapisan.Items.Insert(4, New ListItem("No. PT/Inden Belum Selesai Dari", "4"))
            ddlTapisan.Items.Insert(5, New ListItem("Kumpulang Wang", "5"))
            ddlTapisan.SelectedIndex = 0
            'ddlKW.Items.Insert(0, New ListItem("-SILA PILIH-", "0"))
            'ddlKW.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub
    'Private Sub fBindDddlKW()

    '    Try
    '        Dim strSql As String = "select a.kodkw,(a.kodkw + ' - ' + b.butiran) as ButiranKW from MK08_PindahanBajet a,MK_Kw b Where a.kodkw=b.kodkw and mk08_tahun = '" & Trim(ddlKat.SelectedValue.TrimEnd) & "' and MK08_Cara ='BAKI PERUNTUKAN' order by a.kodkw asc"
    '        Dim ds As New DataSet
    '        Dim dbconn As New DBKewConn
    '        Dim dt As New DataTable
    '        ds = dbconn.fSelectCommand(strSql)

    '        ddlKW.DataSource = ds
    '        ddlKW.DataTextField = "ButiranKW"
    '        ddlKW.DataValueField = "KodKw"
    '        ddlKW.DataBind()

    '        ddlKW.Items.Insert(0, New ListItem("-SILA PILIH-", "0"))
    '        ddlKW.SelectedIndex = 0

    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub fBindDddlPTj()
        Try
            Dim strSql As String = "SELECT KodPTJ, Butiran FROM MK_PTJ WHERE KodKategoriPTJ ='P' AND Singkatan <> '-'"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    ddlPTJ.DataSource = ds
                    ddlPTJ.DataTextField = "butiran"
                    ddlPTJ.DataValueField = "KodPTJ"
                    ddlPTJ.DataBind()

                    ddlPTJ.Items.Insert(0, New ListItem("-SILA PILIH PTj-", "0"))
                    ddlPTJ.SelectedIndex = 0
                End If
            End If



        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlTapisan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTapisan.SelectedIndexChanged
        Try
            If ddlTapisan.SelectedValue = "1" Then
                Me.NoInv.Visible = True
                Me.TrkInv.Visible = False
                Me.Pemb.Visible = False
                Me.KumpW.Visible = False
            ElseIf ddlTapisan.SelectedValue = "2" Then
                Me.TrkInv.Visible = True
                Me.NoInv.Visible = False
                Me.Pemb.Visible = False
                Me.KumpW.Visible = False
            ElseIf ddlTapisan.SelectedValue = "3" Then
                Me.TrkInv.Visible = False
                Me.NoInv.Visible = False
                Me.Pemb.Visible = True
                Me.KumpW.Visible = False
                Try
                    Dim strSql As String = "select NamaSyarikat, IDSyarikat from VAP_SenaraiPembekal ORDER BY NamaSyarikat"
                    Dim ds As New DataSet
                    Dim dbconn As New DBKewConn
                    Dim dt As New DataTable
                    ds = dbconn.fSelectCommand(strSql)

                    If Not ds Is Nothing Then
                        If ds.Tables(0).Rows.Count > 0 Then
                            cmbPembekal.DataSource = ds
                            cmbPembekal.DataTextField = "NamaSyarikat"
                            cmbPembekal.DataValueField = "IDSyarikat"
                            cmbPembekal.DataBind()
                            cmbPembekal.Items.Insert(0, New ListItem("-SILA PILIH PTj-", "0"))
                            cmbPembekal.SelectedIndex = 0

                            cmbPembekal2.DataSource = ds
                            cmbPembekal2.DataTextField = "NamaSyarikat"
                            cmbPembekal2.DataValueField = "IDSyarikat"
                            cmbPembekal2.DataBind()
                            cmbPembekal2.Items.Insert(0, New ListItem("-SILA PILIH PTj-", "0"))
                            cmbPembekal2.SelectedIndex = 0
                        End If
                    End If



                Catch ex As Exception

                End Try
            ElseIf ddlTapisan.SelectedValue = "4" Then
                Me.TrkInv.Visible = False
                Me.NoInv.Visible = False
                Me.Pemb.Visible = False
                Me.KumpW.Visible = False
            ElseIf ddlTapisan.SelectedValue = "5" Then
                Me.TrkInv.Visible = False
                Me.NoInv.Visible = False
                Me.Pemb.Visible = False
                Me.KumpW.Visible = True

            End If


            fBindDddlPTj()
        Catch ex As Exception

        End Try
    End Sub



    Private Sub fBindGv()
        Try
            Dim blnFound As Boolean = False
            Dim strtahun As String = Trim(ddlKat.SelectedValue.TrimEnd)
            Dim strTkhHingga As String = "31/12/" & strtahun
            Dim intRec As Integer

            Dim dbConn As New DBKewConn
            Dim strFilter As String
            Dim strTapis As String
            Dim NoId As String

            strTapis = "WHERE "

            Select Case ddlKat.Text
                'Case "Semua"
                Case "ILL"
                    strTapis = strTapis & "AND TBL_A.AP01_JenInv = 'ILL' "
                Case "IPT"
                    strTapis = strTapis & "AND TBL_A.AP01_JenInv = 'IPT' "
                Case "IND"
                    strTapis = strTapis & "AND TBL_A.AP01_JenInv = 'IND' "
            End Select


            Select Case ddlTapisan.SelectedValue
                Case "1"
                    strTapis = strTapis & "AND TBL_A.AP01_NoInv >= '" & Me.txtNoInv.Text & "' " &
            "AND TBL_A.AP01_NoInv <= '" & Me.txtNoInv2.Text & "'"

                Case "2"
                    strTapis = strTapis & "AND TBL_A.AP01_TkhInv >= CONVERT(DateTime, '" & Format(Me.dtTerima.Text, "yyyy-MM-dd") & "', 102) " &
            "AND TBL_A.AP01_TkhInv <= CONVERT(DateTime, '" & Format(Me.dtTerima2.Text, "yyyy-MM-dd") & " 23:59:59', 102) "

                Case "3"
                    strTapis = strTapis & "AND (CASE TBL_A.ROC01_IDSya  WHEN '-' THEN TBL_B.AP01_IDPENERIMA ELSE TBL_A.ROC01_IDSya END) >= '" & Right(Me.cmbPembekal.SelectedValue, 8) & "' " &
            "AND (CASE TBL_A.ROC01_IDSya  WHEN '-' THEN TBL_B.AP01_IDPENERIMA ELSE TBL_A.ROC01_IDSya END) <= '" & Right(Me.cmbPembekal2.SelectedValue, 8) & "'"

                Case "4"
            '        strTapis = strTapis & "AND TBL_A.PO02_NoPt >= '" & Me.txtNoPT.Text & "' " &
            '"AND TBL_A.PO02_NoPt <= '" & Me.txtNoPT2.Text & "'"

                Case "5"
                    strTapis = strTapis & "AND TBL_A.KodKw = '" & Left(Me.CmbKW.SelectedValue, 2) & "' "

            End Select

            strTapis = Replace(strTapis, "WHERE AND", "WHERE")
            If strTapis = "WHERE " Then
                strTapis = ""
            End If
            'If strKodPTj <> "000000" Then
            '    strFilter = " and a.kodptj= '" & Trim(ddlPTJ.SelectedValue.TrimEnd) & "'"
            'End If

            Dim strSql As String = "SELECT distinct TBL_A.KodKw, TBL_A.AP01_NoInv, TBL_A.AP01_TkhInv, TBL_A.PO19_NoPt, TBL_A.AP01_NoId, TBL_A.AP01_JenInv, " &
    "(CASE TBL_A.ROC01_IDSya  WHEN '-' THEN TBL_B.AP01_IDPENERIMA ELSE TBL_A.ROC01_IDSya END) AS IDPENERIMA, " &
    "(CASE TBL_A.ROC01_IDSya  WHEN '-' THEN TBL_B.AP01_PENERIMA ELSE TBL_C.ROC01_NamaSya END) AS PENERIMA, " &
    "(CASE TBL_A.ROC01_IDSya  WHEN '-' THEN TBL_B.AP01_JUMLAH ELSE TBL_A.AP01_JUMLAH END) AS JUMLAH " &
    "From (SELECT B.KodKw, A.AP01_NoInv, FORMAT(A.AP01_TkhInv,'dd/MM/yyyy') AS AP01_TkhInv, A.PO19_NoPt, A.AP01_NoId, A.ROC01_IDSya, A.AP01_JUMLAH, A.AP01_JenInv FROM AP01_Invois A " &
    "INNER JOIN AP01_InvoisDT B ON A.AP01_NoId = B.AP01_NoId " &
    "WHERE A.AP09_StatusDok IN ('01','02') " &
    "AND LEFT(A.AP01_NoJS,2) not in ('JS','JK') " &
    "AND AP01_STATUSTNG = 1 " &
    "GROUP BY B.KodKw, A.AP01_NoInv, A.AP01_TkhInv, A.PO19_NoPt, A.AP01_NoId, A.ROC01_IDSya, A.AP01_JUMLAH, A.AP01_JenInv) TBL_A " &
    "Left Join (SELECT AP01_NoId, AP01_IDPENERIMA, AP01_PENERIMA, AP01_JUMLAH FROM AP01_INVOISNOMINEES)TBL_B ON TBL_A.AP01_NoId = TBL_B.AP01_NoId " &
    "Left Join (SELECT ROC01_IDSya, ROC01_NamaSya from ROC01_Syarikat) TBL_C ON TBL_A.ROC01_IDSya = TBL_C.ROC01_IDSya AND TBL_C.ROC01_IDSya <> '-' " & strTapis

            Dim ds As New DataSet
            ds = dbConn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    blnFound = True
                End If
            End If

            If blnFound = True Then

                Dim dt As New DataTable
                dt.Columns.Add("kodKW", GetType(String))
                dt.Columns.Add("Invois", GetType(String))
                dt.Columns.Add("TkhInv", GetType(String))
                dt.Columns.Add("NoPt", GetType(String))
                dt.Columns.Add("Pembekal", GetType(String))
                dt.Columns.Add("Amaun", GetType(String))
                dt.Columns.Add("AP01_NoId", GetType(String))


                Dim strKW, strInv, strTkhInv, strNoPT, strPemb As String
                Dim decBakiPerutk, decBakiSbnr As Decimal
                Dim strBakiPerutk, strBakiSbnr As String

                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    strKW = ds.Tables(0).Rows(i)("KodKW")
                    strInv = ds.Tables(0).Rows(i)("AP01_NoInv")
                    strTkhInv = ds.Tables(0).Rows(i)("AP01_TkhInv")
                    strNoPT = ds.Tables(0).Rows(i)("PO19_NoPt")
                    strPemb = ds.Tables(0).Rows(i)("PENERIMA")

                    decBakiPerutk = CDec(ds.Tables(0).Rows(i)("JUMLAH"))
                    strBakiPerutk = decBakiPerutk.ToString("#,##0.00")
                    NoId = ds.Tables(0).Rows(i)("AP01_NoId")

                    'decBakiSbnr = CDec(fGetBakiSebenar(strtahun, strTkhHingga, strKW, strKO, strPTj, strKP, strKodvot))
                    'strBakiSbnr = decBakiSbnr.ToString("#,##0.00")

                    dt.Rows.Add(strKW, strInv, strTkhInv, strNoPT, strPemb, strBakiPerutk, NoId)
                Next
                gvProsesPemiutang.DataSource = dt
                gvProsesPemiutang.DataBind()

                intRec = ds.Tables(0).Rows.Count
                ViewState("dtBajet") = dt

            Else
                fGlobalAlert("Tiada Rekod Pemiutang!", Me.Page, Me.[GetType]())
                intRec = 0
            End If

            lblJumRekod.InnerText = intRec
        Catch ex As Exception

        End Try
    End Sub
    Private Sub fInvGv(InvID As String)
        Try
            Dim dbConn As New DBKewConn
            Dim strSql As String = "Select KodKw, KodPtj, KodVot,  AP01_KuantitiAkanByr," &
    " KodTax, PtjGST, KwGST, AktGST, AP01_JumGST, AP01_JumTanpaGST, VotGST, AP01_flagInclusiveGST," &
    " AP01_KadarHarga, AP01_AmaunAkanByr" &
    " from AP01_InvoisDt where AP01_NoId = '" & InvID & "'"
            Dim blnFound As Boolean = False
            Dim ds As New DataSet
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    blnFound = True
                End If
            End If

            If blnFound = True Then

                Dim dt As New DataTable
                dt.Columns.Add("kodKW", GetType(String))
                dt.Columns.Add("KodPtj", GetType(String))
                dt.Columns.Add("KodVot", GetType(String))
                dt.Columns.Add("AP01_KadarHarga", GetType(String))
                dt.Columns.Add("AP01_AmaunAkanByr", GetType(String))


                Dim strKW, strInv, strTkhInv, strNoPT, strPemb As String
                Dim decBakiPerutk, decBakiSbnr, decAmn As Decimal
                Dim strBakiPerutk, strBakiSbnr, strAmn As String

                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    strKW = ds.Tables(0).Rows(i)("kodKW")
                    strInv = ds.Tables(0).Rows(i)("KodPtj")
                    strNoPT = ds.Tables(0).Rows(i)("KodVot")
                    'strTkhInv = ds.Tables(0).Rows(i)("AP01_KadarHarga")


                    decBakiSbnr = CDec(ds.Tables(0).Rows(i)("AP01_KadarHarga"))
                    strBakiSbnr = decBakiSbnr.ToString("#,##0.00")


                    decBakiPerutk = CDec(ds.Tables(0).Rows(i)("AP01_AmaunAkanByr"))
                    strBakiPerutk = decBakiPerutk.ToString("#,##0.00")


                    'NoId = ds.Tables(0).Rows(i)("AP01_NoId")

                    'decBakiSbnr = CDec(fGetBakiSebenar(strtahun, strTkhHingga, strKW, strKO, strPTj, strKP, strKodvot))
                    'strBakiSbnr = decBakiSbnr.ToString("#,##0.00")

                    dt.Rows.Add(strKW, strInv, strNoPT, strBakiSbnr, strBakiPerutk)
                Next
                gvInvDt.DataSource = dt
                gvInvDt.DataBind()

                'intRec = ds.Tables(0).Rows.Count
                'ViewState("dtBajet") = dt

            Else
                fGlobalAlert("Tiada Rekod Pemiutang!", Me.Page, Me.[GetType]())
                'intRec = 0
            End If
        Catch ex As Exception

        End Try
    End Sub
    Dim decTotJumAsl, decTotJumBaki As Decimal
    Dim strGJumAsl, strGJumBaki As String
    Private Sub gvInvDt_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvInvDt.RowDataBound
        Try


            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim txtJumAkan As Label = CType(e.Row.FindControl("txtHargaAsal"), Label)
                decTotJumAsl += Decimal.Parse(txtJumAkan.Text)
                strGJumAsl = FormatNumber(decTotJumAsl)

                Dim txtJuminv As Label = CType(e.Row.FindControl("txtHargaInv"), Label)
                decTotJumBaki += Decimal.Parse(txtJumAkan.Text)
                strGJumBaki = FormatNumber(decTotJumAsl)

            ElseIf e.Row.RowType = DataControlRowType.Footer Then
                Dim lblGJumByr As Label = CType(e.Row.FindControl("JumDt"), Label)
                Dim lblGJumInv As Label = CType(e.Row.FindControl("JumInv"), Label)
                lblGJumByr.Text = strGJumAsl.ToString()
                lblGJumInv.Text = strGJumAsl.ToString()

            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub gvProsesPemiutang_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvProsesPemiutang.PageIndexChanging
        Try

            gvProsesPemiutang.PageIndex = e.NewPageIndex
            If Session("SortedView") IsNot Nothing Then
                gvProsesPemiutang.DataSource = Session("SortedView")
                gvProsesPemiutang.DataBind()
            Else
                fBindGv()
            End If

        Catch ex As Exception

        End Try
    End Sub



    Private Sub lbtnPapar_Click(sender As Object, e As EventArgs) Handles lbtnPapar.Click
        Try
            'Dim strKodPTj As String = ddlPTJ.SelectedValue
            fBindGv()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.Click
        Try
            Dim rs As String
            Dim rs1 As String
            Dim rs2 As String
            Dim bil As Integer
            Dim Bil1 As String
            Dim BilDt As Integer
            Dim NoJrnl As String
            Dim Tarikh As String
            Dim TempNoJrnl As String
            Dim InputText As String
            Dim getres As String
            Dim i As Integer, j As Integer
            Dim iJumpa As Integer
            Dim iJum As Double
            Dim strAkt As String, strKW As String
            Dim dblJum As Double
            Dim thnProses As Integer
            Dim Perihal As String
            Dim strSql As String

            If Me.Thn1.Checked = True Then
                Tarikh = "31/12/" & CStr(Year(DateTime.Today))
                thnProses = Year(DateTime.Today)
            End If

            If Me.Thn.Checked = True Then
                Tarikh = "31/12/" & CStr(Year(DateTime.Today) - 1)
                thnProses = Year(DateTime.Today) - 1
            End If

            If Me.lblNoJurnal.Text <> "" Then Exit Sub

            iJumpa = 0
            For Each gvr As GridViewRow In gvProsesPemiutang.Rows
                If (CType(gvr.FindControl("lblNoID"), CheckBox)).Checked = True Then
                    iJumpa = 1
                End If
            Next gvr



            If iJumpa = 0 Then Exit Sub

            rs = "select GL01_NoJrnl, GL01_NoJrnlSem, GL01_TkhTran, KodDok, GL01_Rujukan, " &
    "GL01_IdPenerima, GL01_Perihal, GL01_Jumlah, GL01_JenisTran, GL01_StatusDok, GL01_Status, " &
    "GL01_CetakJSem, GL01_CetakJ, GL01_NoDok from GL01_JrnlTran " &
    "WHERE GL01_NoJrnlSem = '" & TempNoJrnl & "' "
            Dim dbconn As New DBKewConn
            Dim ds As New DataSet
            ds = dbconn.fSelectCommand(rs)
            If ds Is Nothing Then
                Bil1 = NoJurnalSem("GL", "JS", thnProses)



                If Me.Thn1.Checked = True Then
                    NoJrnl = "JS" & Bil1 & "12" & Right(CStr(Year(RefDateTime)), 2)
                    Perihal = "PROSES PEMIUTANG TAHUN " & CStr(Year(RefDateTime)) & " KATEGORI " & get_kategori(Me.ddlKat.Text) & " KW " & Left(Me.CmbKW.Text, 2)
                End If
                If Me.Thn.Checked = True Then
                    NoJrnl = "JS" & Bil1 & "12" & Right(CStr(Year(RefDateTime) - 1), 2)
                    Perihal = "PROSES PEMIUTANG TAHUN " & CStr(Year(RefDateTime) - 1) & " KATEGORI " & get_kategori(Me.ddlKat.Text) & " KW " & Left(Me.CmbKW.Text, 2)

                End If


                rs = "insert into GL01_JrnlTran (GL01_NoJrnl , GL01_NoJrnlSem , GL01_TkhTran , KodDok , GL01_Rujukan , GL01_IdPenerima,GL01_Jumlah, " &
                    "GL01_JenisTran , GL01_StatusDok , GL01_Status , GL01_CetakJSem , GL01_CetakJ , GL01_NoDok) " &
               "values (@NoJrnl, @NoJrnlSem, @TkhTran, @KodDok, @Rujukan, @IdPenerima, @Jumlah, @JenisTran, @StatusDok, @Status,  @CetakJSem,  @CetakJ, @NoDok)"
                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@NoJrnl", NoJrnl),
                    New SqlParameter("@NoJrnlSem", NoJrnl),
                    New SqlParameter("@TkhTran", Tarikh),
                    New SqlParameter("@KodDok", "GL-TP"),
                    New SqlParameter("@Rujukan", "-"),
                    New SqlParameter("@IdPenerima", "-"),
                    New SqlParameter("@Jumlah", 0),
                    New SqlParameter("@JenisTran", "L"),
                    New SqlParameter("@StatusDok", "01"),
                    New SqlParameter("@Status", "A"),
                    New SqlParameter("@CetakJSem", False),
                    New SqlParameter("@CetakJ", False),
                    New SqlParameter("@NoDok", NoJrnl)
                }
                dbconn = New DBKewConn
                dbconn.sConnBeginTrans()
                If dbconn.fInsertCommand(rs, paramSql) > 0 Then
                    dbconn.sConnCommitTrans()
                Else
                    dbconn.sConnRollbackTrans()
                End If
            Else

            End If
            rs = ""
            Me.lblNoJurnal.Text = NoJrnl
            TempNoJrnl = NoJrnl
            Me.lblJum.Text = "0.00"
            Me.lblNoJurnal.Text = TempNoJrnl

            For Each rowView As GridViewRow In gvProsesPemiutang.Rows
                'Dim gvr As GridViewRow = CType(((CType(sender, Control)).NamingContainer), GridViewRow)
                Dim ddlKW As CheckBox = TryCast(rowView.Cells(0).FindControl("chkRow"), CheckBox)

                Dim NoIDInv As String = TryCast(rowView.Cells(0).FindControl("lblNoID"), Label).Text



                If ddlKW.Checked Then
                    rs = "select (count(GL01_NoJrnlSem) + 1) as Bil FROM GL01_JrnlTranDt " &
                        "where GL01_NoJrnlSem = '" & TempNoJrnl & "'"

                    Dim ds1 As New DataSet
                    ds1 = dbconn.fSelectCommand(rs)

                    If Not ds1 Is Nothing Then
                        If ds1.Tables(0).Rows.Count > 0 Then
                            BilDt = ds1.Tables(0)(i)("Bil")

                        End If
                    End If
                    rs = ""


                    rs = "select KodKw, KodPtj, KodVot, KodAkt, sum(AP01_JumTanpaGST) as AP01_AmaunAkanByr " &
                "from AP01_InvoisDt where AP01_NoId = '" & NoIDInv & "' " &
                "group by KodKw, KodPtj, KodVot, KodAkt order by KodKw, KodPtj, KodVot, KodAkt"
                    Dim ds2 As New DataSet
                    ds2 = dbconn.fSelectCommand(rs)
                    If Not ds2 Is Nothing Then
                        If ds2.Tables(0).Rows.Count > 0 Then
                            For j = 0 To ds2.Tables(0).Rows.Count - 1
                                strSql = "INSERT INTO GL01_JrnlTranDt (GL01_NoJrnlSem, GL01_NoJrnl, GL01_TkhTran, GL01_Bil,KodKw, KodPtj, KodVot, KodAkt, GL01_Butiran, GL01_Debit, " &
                                    " GL01_Kredit, KodPenyesuaian, GL01_PenyID, GL01_Rujukan)" &
                "VALUES (@NoJrnlSem, @NoJrnl, @TkhTran, @Bil, @KodKw, @KodPtj,@KodVot, @KodAkt, @Butiran, @Debit, @Kredit, @KodPenyesuaian, @PenyID, @Rujukan)"
                                Dim paramSql() As SqlParameter = {
                                        New SqlParameter("@NoJrnlSem", TempNoJrnl),
                                        New SqlParameter("@NoJrnl", TempNoJrnl),
                                        New SqlParameter("@TkhTran", "CONVERT(DateTime, '" & Format(Tarikh, "yyyy-MM-dd") & "', 102), "),
                                        New SqlParameter("@Bil", BilDt),
                                        New SqlParameter("@KodKw", ds2.Tables(0)(i)("KodKw").ToString),
                                        New SqlParameter("@KodPtj", ds2.Tables(0)(i)("KodPtj").ToString),
                                        New SqlParameter("@KodVot", ds2.Tables(0)(i)("KodVot").ToString),
                                        New SqlParameter("@KodAkt", ds2.Tables(0)(i)("KodAkt").ToString),
                                        New SqlParameter("@Butiran", "Pelbagai Pemiutang"),
                                        New SqlParameter("@Debit", ds2.Tables(0)(i)("AP01_AmaunAkanByr").ToString),
                                        New SqlParameter("@Kredit", 0),
                                        New SqlParameter("@KodPenyesuaian", "U"),
                                        New SqlParameter("@PenyID", "-"),
                                        New SqlParameter("@Rujukan", rowView.Cells(5).Text)
                                    }

                                dbconn.sConnBeginTrans()
                                If Not dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                                    'blnSuccess = False
                                    Exit Try
                                End If
                                BilDt = BilDt + 1

                            Next
                        End If
                    End If
                    rs = ""



                    rs = "UPDATE AP01_Invois SET AP01_NoJS = @NoJS " &
                "WHERE AP01_NoId = '" & NoIDInv & "'"
                    Dim paramSql1() As SqlParameter = {
                    New SqlParameter("@NoJS", TempNoJrnl)}
                    dbconn.sConnBeginTrans()
                    If Not dbconn.fUpdateCommand(rs, paramSql1) > 0 Then
                        'blnSuccess = False
                        Exit Try
                    End If
                    rs = ""
                End If

            Next



            'CARI JUMLAH DETAIL
            rs = "select isnull(sum(GL01_Debit),0) as Jum from GL01_JrnlTranDt " &
        "where GL01_NoJrnlSem = '" & TempNoJrnl & "' "
            Dim ds3 As New DataSet
            ds3 = dbconn.fSelectCommand(rs)
            If Not ds3 Is Nothing Then
                If ds3.Tables(0).Rows.Count > 0 Then

                End If
            End If

            'UPDATE JURNAL DETAIL
            For i = 1 To gvProsesPemiutang.Rows.Count - 1 
                If Me.ListView1.ListItems(i).Checked = True Then

                Set rs = myDB("select (count(GL01_NoJrnlSem) + 1) as Bil FROM GL01_JrnlTranDt " & _
                "where GL01_NoJrnlSem = '" & TempNoJrnl & "'", adLockReadOnly)
                If rs.EOF = False Then
                        BilDt = rs("Bil")
                    End If
                    rs.Close
                Set rs = Nothing


                Set rs = myDB("select KodKw, KodPtj, KodVot, KodAkt, sum(AP01_JumTanpaGST) as AP01_AmaunAkanByr " & _
                "from AP01_InvoisDt where AP01_NoId = '" & Me.ListView1.ListItems.Item(i).SubItems(8) & "' " & _
                "group by KodKw, KodPtj, KodVot, KodAkt order by KodKw, KodPtj, KodVot, KodAkt", adLockReadOnly)
                For j = 1 To rs.RecordCount

                    Set rs1 = myDB("insert into GL01_JrnlTranDt " & _
                    "(GL01_NoJrnlSem, GL01_NoJrnl, GL01_TkhTran, GL01_Bil, " & _
                    "KodKw, KodPtj, KodVot, KodAkt, GL01_Butiran, GL01_Debit, " & _
                    "GL01_Kredit, KodPenyesuaian, GL01_PenyID, GL01_Rujukan) " & _
                    "values('" & TempNoJrnl & "', '" & TempNoJrnl & "', " & _
                    "CONVERT(DateTime, '" & Format(Tarikh, "yyyy-MM-dd") & "', 102), " & BilDt & ", " & _
                    " '" & rs("KodKw") & "', '" & rs("KodPtj") & " ', '" & rs("KodVot") & " ', " & _
                    "'" & rs("KodAkt") & " ', 'Pelbagai Pemiutang', '" & rs("AP01_AmaunAkanByr") & "', " & _
                    "0, 'U', '-', '" & Me.ListView1.ListItems.Item(i).SubItems(2) & "' ) ", adLockOptimistic)
                    Set rs1 = Nothing


                    BilDt = BilDt + 1

                        rs.MoveNext
                    Next
                Set rs = Nothing

                Set rs = myDB("select VotGST,  PtjGST, KwGST, AktGST, sum(AP01_JumGST) as AP01_AmaunAkanByr " & _
                "from AP01_InvoisDt where AP01_NoId = '" & Me.ListView1.ListItems.Item(i).SubItems(8) & "' AND AP01_JumGST > 0 " & _
                "group by VotGST,  PtjGST, KwGST, AktGST order by KwGST,  PtjGST, VotGST, AktGST", adLockReadOnly)
                For j = 1 To rs.RecordCount

                    Set rs1 = myDB("insert into GL01_JrnlTranDt " & _
                    "(GL01_NoJrnlSem, GL01_NoJrnl, GL01_TkhTran, GL01_Bil, " & _
                    "KodKw, KodPtj, KodVot, KodAkt, GL01_Butiran, GL01_Debit, " & _
                    "GL01_Kredit, KodPenyesuaian, GL01_PenyID, GL01_Rujukan) " & _
                    "values('" & TempNoJrnl & "', '" & TempNoJrnl & "', " & _
                    "CONVERT(DateTime, '" & Format(Tarikh, "yyyy-MM-dd") & "', 102), " & BilDt & ", " & _
                    " '" & rs("KwGST") & "', '" & rs("PtjGST") & " ', '" & rs("VotGST") & " ', " & _
                    "'" & rs("AktGST") & " ', 'Pelbagai Pemiutang', '" & rs("AP01_AmaunAkanByr") & "', " & _
                    "0, 'U', '-', '" & Me.ListView1.ListItems.Item(i).SubItems(2) & "' ) ", adLockOptimistic)
                    Set rs1 = Nothing


                    BilDt = BilDt + 1

                        rs.MoveNext
                    Next
                Set rs = Nothing




                Set rs = myDB("UPDATE AP01_Invois SET AP01_NoJS = '" & TempNoJrnl & "' " & _
                "WHERE AP01_NoId = '" & Me.ListView1.ListItems.Item(i).SubItems(8) & "'", adLockOptimistic)
                Set rs = Nothing

            End If
            Next

        'CARI JUMLAH DETAIL
        Set rs = myDB("select isnull(sum(GL01_Debit),0) as Jum from GL01_JrnlTranDt " & _
        "where GL01_NoJrnlSem = '" & TempNoJrnl & "' ", adLockReadOnly)
        If rs.EOF = False Then
                iJum = rs("Jum")
                Me.lblJum.caption = Format(Abs(iJum), "############.00")
            Else
                iJum = 0
                Me.lblJum.caption = "0.00"
            End If
        Set rs = Nothing

        'UPDATE JUMLAH GL01_JrnlTran
        Set rs = myDB("UPDATE GL01_JrnlTran SET GL01_Jumlah = " & iJum & "  " & _
        "where GL01_NoJrnlSem = '" & TempNoJrnl & "' ", adLockOptimistic)
        Set rs = Nothing

        'UPDATE JUMLAH GL01_JrnlTrandt MENGIKUT KW
        '---------------------------------------
        Set rs = myDB("select (count(GL01_NoJrnlSem) + 1) as Bil, " & _
        "isnull(sum(GL01_Debit),0) as Jum, KodKw, KodAkt FROM GL01_JrnlTranDt " & _
        "where GL01_NoJrnlSem = '" & TempNoJrnl & "' group by KodKw, KodAkt ", adLockReadOnly)
        If rs.EOF = False Then
                For i = 1 To rs.RecordCount

                    dblJum = rs("Jum")
                    strKW = rs("KodKw")
                    strAkt = rs("KodAkt")


                Set rs2 = myDB("insert into GL01_JrnlTranDt " & _
                "(GL01_NoJrnlSem, GL01_NoJrnl, GL01_TkhTran, GL01_Bil, " & _
                "KodKw, KodPtj, KodVot, KodAkt, GL01_Butiran, GL01_Debit, " & _
                "GL01_Kredit, KodPenyesuaian, GL01_PenyID)" & _
                "values ('" & TempNoJrnl & "', '" & TempNoJrnl & "', " & _
                "CONVERT(DateTime, '" & Format(Tarikh, "yyyy-MM-dd") & "', 102), " & BilDt & ", " & _
                "'" & strKW & "', '500000', '81101', '" & strAkt & "', " & _
                "'Pelbagai Pemiutang', 0, " & dblJum & ", 'U', '-') ", adLockOptimistic)
                Set rs2 = Nothing

                rs.MoveNext

                    BilDt = BilDt + 1
                Next

            End If
            rs.Close
        Set rs = Nothing

        '------------------------------------


        'UPDATE TABLE GL03_StatusDOK
        'JURNAL BARU
        Set rs = myDB("INSERT INTO GL03_StatusDok " & _
        "(GL01_NoJrnlSem, GL01_NoJrnl, GL03_Kod, GL03_NoStaf, GL03_Tkh, GL03_Ulasan) " & _
        "VALUES ('" & NoJrnl & "', '" & NoJrnl & "', '01', '" & RefPengguna & "', " & _
        "CONVERT(DATETIME, '" & Format(RefDateTime, "yyyy-mm-dd") & "',102), '-')", adLockOptimistic)
        Set rs = Nothing



        MsgBox("Data telah disimpan ke dalam pengkalan data. No. Jurnal : " & NoJrnl)

            Reset()

err_handler:
            If Err.Number <> 0 Then
                Dim TemErr As String

                TemErr = ErrRpt(Name, "simpan", Err.Number, Err.Description)
            End If



        Catch ex As Exception

        End Try
    End Sub
    Private Sub gvProsesPemiutang_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvProsesPemiutang.RowCommand
        Try
            If e.CommandName = "Select" Then
                'Dim InvID As String
                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvProsesPemiutang.Rows(index)
                Dim InvID As String = CType(selectedRow.FindControl("lblNoID"), Label).Text
                mpeLstInv.Show()

                fInvGv(InvID)
            End If
        Catch ex As Exception

        End Try
    End Sub



    Function get_kategori(kod As String) As String

        Select Case kod
            Case "ILL" : get_kategori = "LAIN - LAIN PEMBAYARAN"
            Case "IPT" : get_kategori = "PESANAN TEMPATAN"
            Case "IND" : get_kategori = "INDEN"
        End Select
        Return get_kategori
    End Function


End Class