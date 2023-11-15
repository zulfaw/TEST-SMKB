Imports System.Drawing

Public Class Senarai_Agihan
    Inherits System.Web.UI.Page

    Dim decTotBajet2 As Decimal = 0.00
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fBindDdlTahunAgih()
            sClearGvLst()
            fBindDdlPTJ()
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

            strSql = "SELECT DISTINCT TOP 100 PERCENT dbo.MK_PTJ.KodPTJ,(MK_PTJ.KodPTJ + ' - ' + dbo.MK_PTJ.Butiran) as Butiran " &
"FROM dbo.MK_PTJ INNER JOIN dbo.MK01_VotTahun ON dbo.MK_PTJ.KodPTJ = dbo.MK01_VotTahun.KodPTJ " &
"WHERE dbo.MK01_VotTahun.mk01_tahun = '" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' order by KodPtj"

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
    Private Sub sClearGvLst()
        gvListAgihan.DataSource = New List(Of String)
        gvListAgihan.DataBind()
    End Sub

    Dim TotJum As Decimal

    Private Sub gvListAgihan_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvListAgihan.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim lblTotBajet As Label = CType(e.Row.FindControl("lblIndObjSbg"), Label)
                Dim strIndObjSbg As String = lblTotBajet.Text

                If strIndObjSbg = "" Then
                    e.Row.Font.Bold = True
                    e.Row.BackColor = System.Drawing.Color.FromArgb(251, 238, 213)
                    e.Row.Cells(7).ForeColor = ColorTranslator.FromHtml("#a71815")
                    Dim lblBajet As Label = CType(e.Row.FindControl("lblBajet"), Label)
                    TotJum += Decimal.Parse(lblBajet.Text)
                    ViewState("strJumBajet") = TotJum.ToString("#,##0.00")
                End If
            End If

            If e.Row.RowType = DataControlRowType.Footer Then
                Dim lblTotBajet As Label = CType(e.Row.FindControl("lblTotBajet"), Label)
                lblTotBajet.Text = ViewState("strJumBajet").ToString()
                lblTotBajet.Font.Bold = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function fBindGvlistAgihan()

        Dim dbconn As New DBKewConn
        Try
            Dim strSql As String
            Dim dt As New DataTable
            dt.Columns.Add("IndObjSbg", GetType(String))
            dt.Columns.Add("kodKW", GetType(String))
            dt.Columns.Add("kodKO", GetType(String))
            dt.Columns.Add("kodPTj", GetType(String))
            dt.Columns.Add("kodKP", GetType(String))
            dt.Columns.Add("ObjAm", GetType(String))
            dt.Columns.Add("ObjSbg", GetType(String))
            dt.Columns.Add("Bajet", GetType(String))

            strSql = "select a.indsbg,a.indam,a.kodkw,a.kodKO,a.kodptj, a.kodkp, a.kodvot,(select b.KodVot + ' - ' + b.Butiran  from MK_Vot b where b.KodVot = a.KodVot) as ButiranVot,a.amaun " &
                                    "from vbg_lulusagih_2 a where a.tahun ='" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' and a.kodptj='" & Trim(ddlPTJ.SelectedValue.TrimEnd) & "'" &
                                    "order by a.kodkw,a.kodKO,a.kodptj, a.kodkp,a.kodvot"

            Dim ds As New DataSet
            dbconn = New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim strKodKW1, strKodKO, strKodPTj, strKodKP, strObjAm, strObjSbg, strBajet As String
                    Dim strIndSbg As String
                    Dim decBajet As Decimal
                    For j As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        strIndSbg = ds.Tables(0).Rows(j).Item("indsbg").ToString
                        If strIndSbg = "" Then
                            strKodKW1 = ds.Tables(0).Rows(j).Item("kodkw").ToString
                            strKodKO = ds.Tables(0).Rows(j).Item("KodKO").ToString
                            strKodPTj = ds.Tables(0).Rows(j).Item("kodptj").ToString
                            strKodKP = ds.Tables(0).Rows(j).Item("kodKP").ToString
                            strObjAm = ds.Tables(0).Rows(j).Item("ButiranVot").ToString

                            strObjSbg = String.Empty
                            decBajet = ds.Tables(0).Rows(j).Item("amaun").ToString
                            strBajet = decBajet.ToString("#,##0.00")
                        Else
                            strKodKW1 = String.Empty
                            strKodKO = String.Empty
                            strKodPTj = String.Empty
                            strKodKP = String.Empty
                            strObjAm = String.Empty

                            strObjSbg = ds.Tables(0).Rows(j).Item("ButiranVot").ToString
                            decBajet = ds.Tables(0).Rows(j).Item("amaun").ToString
                            strBajet = decBajet.ToString("#,##0.00")
                        End If
                        dt.Rows.Add(strIndSbg, strKodKW1, strKodKO, strKodPTj, strKodKP, strObjAm, strObjSbg, strBajet)
                    Next
                End If

            End If

            If dt.Rows.Count > 0 Then
                gvListAgihan.DataSource = dt
                gvListAgihan.DataBind()
            Else
                fGlobalAlert("Tiada agihan peruntukan yang telah diluluskan untuk PTj - " & ddlPTJ.SelectedItem.Text, Me.Page, Me.[GetType]())
                sClearGvLst()
            End If


        Catch ex As Exception
        End Try
    End Function

    Private Sub ddlPTJ_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPTJ.SelectedIndexChanged
        If ddlPTJ.SelectedIndex <> 0 Then
            fBindGvlistAgihan()
        End If

    End Sub
End Class