Imports System.Drawing
Public Class Senarai_Kelulusan
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Dim dateNow As Date = Now.AddYears(+1)
                Dim strNextYear As String = dateNow.Year

                txtTahun.Text = Now.Year
                fBindGvSenarai()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindGvSenarai()
        Try

            Dim intRec As Integer
            Dim strSql As String
            Dim dt As New DataTable
            dt.Columns.Add("kodKw", GetType(String))
            dt.Columns.Add("ButKW", GetType(String))
            dt.Columns.Add("kodKo", GetType(String))
            dt.Columns.Add("ButKO", GetType(String))
            dt.Columns.Add("KodPTJ", GetType(String))
            dt.Columns.Add("ButPTJ", GetType(String))
            dt.Columns.Add("KodKp", GetType(String))
            dt.Columns.Add("ButKP", GetType(String))
            dt.Columns.Add("KodObjSbg", GetType(String))
            dt.Columns.Add("ButObjSbg", GetType(String))
            dt.Columns.Add("KodObjAm", GetType(String))
            dt.Columns.Add("ButObjAm", GetType(String))
            dt.Columns.Add("Amaun", GetType(String))
            dt.Columns.Add("Jenis", GetType(String))
            dt.Columns.Add("TkhMohon", GetType(String))
            dt.Columns.Add("Butiran", GetType(String))
            dt.Columns.Add("IndKW", GetType(String))
            dt.Columns.Add("IndPTj", GetType(String))
            dt.Columns.Add("IndObjAm", GetType(String))
            dt.Columns.Add("IndObjSbg", GetType(String))
            dt.Columns.Add("Status", GetType(String))

            'strSql = "SELECT dbo.bg04_AgihKw.bg04_IndKw as IndKW, dbo.bg05_AgihPTJ.bg05_IndPTJ as IndPTj, dbo.bg06_AgihObjAm.bg06_IndObjAm as IndObjAm, dbo.bg08_AgihDasar.bg08_IndDasar as IndDasar, " &
            '"dbo.bg07_AgihObjSbg.bg07_IndObjSbg as IndObjSbg, dbo.bg04_AgihKw.bg04_Tahun, dbo.bg04_AgihKw.KodKw,dbo.bg05_AgihPTJ.KodKo , (Select dbo.MK_PTJ.KodPTJ + ' - ' + dbo.MK_PTJ.Butiran " &
            '"from dbo.MK_PTJ where dbo.MK_PTJ.KodPTJ  = dbo.bg05_AgihPTJ.KodPTJ) as PTj, dbo.bg06_AgihObjAm.KodKP ,dbo.bg06_AgihObjAm.KodVot AS objam, dbo.bg08_AgihDasar.KodDasar, " &
            '"(Select dbo.MK_Vot.KodVot + ' - ' + dbo.MK_Vot.Butiran  from dbo.MK_Vot where dbo.MK_Vot .KodVot = dbo.bg07_AgihObjSbg.KodVot and dbo.MK_Vot.Status = 1) AS objsbg, dbo.bg04_AgihKw.bg04_Butiran, " &
            '"dbo.bg04_AgihKw.bg04_Amaun,dbo.bg04_AgihKw.KodAgih , dbo.bg04_AgihKw.bg04_TkhAgih, dbo.bg07_AgihObjSbg.bg07_StatLulus, dbo.bg07_AgihObjSbg.bg07_Status, dbo.bg04_AgihKw.KodAgih " &
            '"FROM dbo.bg04_AgihKw INNER JOIN dbo.bg05_AgihPTJ ON dbo.bg04_AgihKw.bg04_IndKw = dbo.bg05_AgihPTJ.bg04_IndKw INNER JOIN dbo.bg06_AgihObjAm ON dbo.bg05_AgihPTJ.bg05_IndPTJ = dbo.bg06_AgihObjAm.bg05_IndPTJ " &
            '"INNER JOIN dbo.bg08_AgihDasar ON dbo.bg06_AgihObjAm.bg06_IndObjAm = dbo.bg08_AgihDasar.bg06_IndObjAm INNER JOIN dbo.bg07_AgihObjSbg ON dbo.bg06_AgihObjAm.bg06_IndObjAm = dbo.bg07_AgihObjSbg.bg06_IndObjAm " &
            '"where dbo.bg07_AgihObjSbg.BG07_StatLulus = '1' And dbo.bg07_AgihObjSbg.KodAgih in ('TB','KG') and dbo.bg04_AgihKw.bg04_Tahun ='" & Trim(txtTahun.Text.TrimEnd) & "' " &
            '"order by dbo.bg04_AgihKw.BG04_IndKw , dbo.bg04_AgihKw.KodKw, dbo.bg05_AgihPTJ.KodPTJ,dbo.bg07_Agihobjsbg.Kodvot,dbo.bg07_Agihobjsbg.bg07_tkhagih"

            strSql = "select A.KodKw, (select G.Butiran from MK_Kw G where G.KodKw =A.KodKw) as ButKW, A.KodKO, (select H.Butiran from MK_KodOperasi H where H.KodKO = A.KodKO) as ButKO, A.KodPTJ,(select E.Butiran from MK_PTJ E where E.kodptj =  A.KodPTJ) as ButPTj, 
A.KodKP, (select I.Butiran from MK_KodProjek I where I.KodProjek = A.KodKP) as ButKP, A.KodVot, (select F.Butiran from MK_Vot F where F.KodVot = A.KodVot) as ButObjSbg, 
(SUBSTRING (A.kodvot,1,1) + '0000') as KodObjAm, (select J.Butiran from mk_vot J where J.KodVot = (select SUBSTRING (A.kodvot,1,1) + '0000') ) as ButObjAm,
A.BG07_Amaun, A.KodAgih, A.BG07_TkhAgih, D.BG04_Butiran, D.BG04_IndKw, C.BG05_IndPTJ, A.BG06_IndObjAm, A.BG07_IndObjSbg, A.BG07_StatLulus              
from BG07_AgihObjSbg A
inner join BG06_AgihObjAm B on B.BG06_IndObjAm = A.BG06_IndObjAm
inner join BG05_AgihPTJ C on C.BG05_IndPTJ = B.BG05_IndPTJ  
inner join BG04_AgihKw D on D.BG04_IndKw = C.BG04_IndKw  
where A.KodAgih in ('TB','KG') and BG07_Tahun = '" & Trim(txtTahun.Text.TrimEnd) & "' and A.BG07_StatLulus in (0,1)"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            Dim strKW, strKO, strPTj, strKP, strObjSbg, strAmaun, strJenis, strTarikh, strDasar, strButiran, strIndKW, strIndPTj, strIndObjAm, strIndDasar, strIndObjSbg As String
            Dim strButPTj, strButObjSbg, strButKW, strButKO, strButKP, strKodObjAm, strButObjAm As String
            Dim decAmaun As Decimal
            Dim strStat As String

            If Not ds Is Nothing Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    strKW = ds.Tables(0).Rows(i)("KodKw")
                    strButKW = ds.Tables(0).Rows(i)("ButKW")
                    strKO = ds.Tables(0).Rows(i)("KodKO")
                    strButKO = ds.Tables(0).Rows(i)("ButKO")
                    strPTj = ds.Tables(0).Rows(i)("KodPTJ")
                    strButPTj = ds.Tables(0).Rows(i)("ButPTj")
                    strKP = ds.Tables(0).Rows(i)("KodKP")
                    strButKP = ds.Tables(0).Rows(i)("ButKP")
                    strObjSbg = ds.Tables(0).Rows(i)("KodVot")
                    strButObjSbg = ds.Tables(0).Rows(i)("ButObjSbg")
                    strKodObjAm = ds.Tables(0).Rows(i)("KodObjAm")
                    strButObjAm = ds.Tables(0).Rows(i)("ButObjAm")
                    decAmaun = CDec(ds.Tables(0).Rows(i)("BG07_Amaun"))
                    strAmaun = FormatNumber(decAmaun, 2)
                    strJenis = ds.Tables(0).Rows(i)("KodAgih")
                    strTarikh = ds.Tables(0).Rows(i)("BG07_TkhAgih")
                    strButiran = ds.Tables(0).Rows(i)("BG04_Butiran")
                    strIndKW = ds.Tables(0).Rows(i)("BG04_IndKw")
                    strIndPTj = ds.Tables(0).Rows(i)("BG05_IndPTJ")
                    strIndObjAm = ds.Tables(0).Rows(i)("BG06_IndObjAm")
                    strIndObjSbg = ds.Tables(0).Rows(i)("BG07_IndObjSbg")
                    strStat = ds.Tables(0).Rows(i)("BG07_StatLulus")
                    If strStat = False Then
                        strStat = "Tidak Lulus"
                    Else
                        strStat = "Lulus"
                    End If

                    dt.Rows.Add(strKW, strButKW, strKO, strButKO, strPTj, strButPTj, strKP, strButKP, strObjSbg, strButObjSbg, strKodObjAm, strButObjAm, strAmaun, strJenis, strTarikh, strButiran, strIndKW, strIndPTj, strIndObjAm, strIndObjSbg, strStat)
                Next

                gvSenarai.DataSource = dt
                gvSenarai.DataBind()
                ViewState("dtSenarai") = dt
                intRec = ds.Tables(0).Rows.Count
            Else
                intRec = 0
            End If

            lblJumRekod.InnerText = intRec
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvSenarai_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvSenarai.RowCommand
        Try
            If e.CommandName = "Select" Then

                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvSenarai.Rows(index)

                Dim strKodKW As String = TryCast(selectedRow.FindControl("lblKw"), Label).Text.ToString
                Dim strButKW As String = TryCast(selectedRow.FindControl("lblButKW"), Label).Text.ToString
                Dim strKodKo As String = TryCast(selectedRow.FindControl("lblKo"), Label).Text.ToString
                Dim strButKO As String = TryCast(selectedRow.FindControl("lblButKO"), Label).Text.ToString
                Dim strKodPTj As String = TryCast(selectedRow.FindControl("lblPTjKod"), Label).Text.ToString
                Dim strPTjBut As String = TryCast(selectedRow.FindControl("lblPTjBut"), Label).Text.ToString
                Dim strKodKp As String = TryCast(selectedRow.FindControl("lblKodKP"), Label).Text.ToString
                Dim strButKP As String = TryCast(selectedRow.FindControl("lblButKP"), Label).Text.ToString
                Dim strKodObjSbg As String = TryCast(selectedRow.FindControl("lblKodObjSbg"), Label).Text.ToString
                Dim strButObjSbg As String = TryCast(selectedRow.FindControl("lblButObjSbg"), Label).Text.ToString
                Dim strKodObjAm As String = TryCast(selectedRow.FindControl("lblKodObjAm"), Label).Text.ToString
                Dim strButObjAm As String = TryCast(selectedRow.FindControl("lblButObjAm"), Label).Text.ToString
                Dim strAmaun As String = TryCast(selectedRow.FindControl("lblAmaun"), Label).Text.ToString
                Dim strJenis As String = TryCast(selectedRow.FindControl("lblJenis"), Label).Text.ToString
                Dim strTkhMohon As String = TryCast(selectedRow.FindControl("lblTkhMohon"), Label).Text.ToString
                Dim strButiranP As String = TryCast(selectedRow.FindControl("lblButiran"), Label).Text.ToString
                Dim strTkhToday As String = Now.ToString("yyyy-MM-dd")

                lblKodJenis.Text = strJenis
                If strJenis = "TB" Then
                    lblJenis.Text = "Tambahan"
                    lblJenis.ForeColor = ColorTranslator.FromHtml("#0000FF")
                ElseIf strJenis = "KG" Then
                    lblJenis.Text = "Kurangan"
                    lblJenis.ForeColor = ColorTranslator.FromHtml("#990000")
                End If

                txtTarikh.Text = strTkhMohon

                'KW
                txtKodKW.Text = strKodKW
                txtKw.Text = strButKW
                'KO
                txtKodKo.Text = strKodKo
                txtKo.Text = strButKO
                'PTj
                txtKodPTj.Text = strKodPTj
                txtPTj.Text = strPTjBut
                'KP
                txtKodKP.Text = strKodKp
                txtKP.Text = strButKP
                'Objek Am
                txtKodObjAm.Text = strKodObjAm
                txtObjAm.Text = strButObjAm
                'Objek Sebagai
                txtKodObjSbg.Text = strKodObjSbg
                txtObjSbg.Text = strButObjSbg

                Dim decBaki As Decimal = fGetBakiSebenar(Trim(txtTahun.Text.TrimEnd), strTkhToday, strKodKW, strKodKo, strKodPTj, strKodKp, strKodObjSbg)
                Dim strBaki As String = FormatNumber(decBaki, 2)
                txtBaki.Text = strBaki
                txtButiran.Text = Trim(strButiranP.TrimEnd)
                txtAmaun.Text = strAmaun

                divlst.Visible = False
                divDt.Visible = True

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvSenarai_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvSenarai.RowDataBound
        Try
            Dim strJenis As String
            If e.Row.RowType = DataControlRowType.DataRow Then
                strJenis = DataBinder.Eval(e.Row.DataItem, "Jenis").ToString()
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

    Private Sub lbtnKembali_Click(sender As Object, e As EventArgs) Handles lbtnKembali.Click
        divlst.Visible = True
        divDt.Visible = False
    End Sub
End Class