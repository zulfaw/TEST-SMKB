Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Public Class Cetakan_Surat_Permohonan
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                gvPermohonanViremen.DataSource = New List(Of String)
                gvPermohonanViremen.DataBind()
            End If
        Catch ex As Exception
            '("Pendaftaran_Menu.aspx(Page_Load())- " & ex.Message.ToString)
        End Try
    End Sub
    Public Sub OnConfirm(sender As Object, e As EventArgs) Handles btnCari.Click, btnCari.Click

        Dim dt As New DataTable
        dt = fFindRec(Trim(txtNoviremen.Text.TrimEnd))
        gvPermohonanViremen.DataSource = dt
        gvPermohonanViremen.DataBind()

    End Sub

    Private Function fFindRec(ByVal strCarianNoViremen As String) As DataTable
        Try
            Dim ds As New DataSet
            Dim dt As New DataTable
            Dim dcBil = New DataColumn("Bil", GetType(Int32))
            Dim dcTarikh = New DataColumn("Tarikh", GetType(Date))
            Dim dcNoViremen = New DataColumn("NoViremen", GetType(String))
            Dim dcRujukanSUrat = New DataColumn("RujukanSurat", GetType(String))

            dt.Columns.Add(dcBil)
            dt.Columns.Add(dcTarikh)
            dt.Columns.Add(dcNoViremen)
            dt.Columns.Add(dcRujukanSUrat)

            Dim intBil As Integer
            Dim strNoViremen As String
            Dim strRujukanSurat As String
            Dim dTarikh As Date

            Dim strSql As String = "select b.BG09_TkhProses, a.BG07_NoViremen, a.BG07_RujSurat
                                    from  BG07_Viremen a INNER JOIN BG09_StatusDok b ON a.BG07_NoViremen = b.BG07_NoViremen
                                    where (a.KodStatusDok = '02') and (a.BG07_NoViremen like '%" + strCarianNoViremen + "%')"
            'KodStatusDok = '02'=kemaskini PTJ
            ds = BindGridView(strSql)
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                intBil = intBil + 1
                dTarikh = ds.Tables(0).Rows(i)(0).ToString
                strNoViremen = ds.Tables(0).Rows(i)(1).ToString
                strRujukanSurat = ds.Tables(0).Rows(i)(2).ToString
                dt.Rows.Add(intBil, dTarikh, strNoViremen, strRujukanSurat)
            Next

            Return dt

        Catch ex As Exception
            '("Pendaftaran_Menu.aspx(fCreateDt())- " & ex.Message.ToString())
        End Try
    End Function

    Private Function BindGridView(ByVal strSql As String) As DataSet

        Try
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)
            Return ds
        Catch ex As Exception
            '(ex.Message.ToString)
        End Try
    End Function
    Private Sub gvPermohonanViremen_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvPermohonanViremen.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvPermohonanViremen, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Klik untuk pilih rekod ini."
            e.Row.ForeColor = ColorTranslator.FromHtml("#000000")
        End If
    End Sub

    Protected Sub gvPermohonanViremen_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvPermohonanViremen.SelectedIndexChanged

        For Each row As GridViewRow In gvPermohonanViremen.Rows
            If row.RowIndex = gvPermohonanViremen.SelectedIndex Then

                row.ForeColor = ColorTranslator.FromHtml("#0000FF")
                row.ToolTip = String.Empty

                Dim strNoViremen As String = gvPermohonanViremen.Rows(row.RowIndex).Cells(2).Text

                HidNoViremen.Value = strNoViremen
            Else
                row.ForeColor = ColorTranslator.FromHtml("#000000")
                row.ToolTip = "Klik untuk pilih rekod ini."
            End If
        Next
    End Sub
    Private Sub gvPermohonanViremen_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvPermohonanViremen.PageIndexChanging
        Try

            gvPermohonanViremen.PageIndex = e.NewPageIndex

            If Session("SortedView") IsNot Nothing Then
                gvPermohonanViremen.DataSource = Session("SortedView")
                gvPermohonanViremen.DataBind()
            Else

                Dim dt As New DataTable
                dt = fFindRec(Trim(txtNoviremen.Text.TrimEnd))
                gvPermohonanViremen.DataSource = dt
                gvPermohonanViremen.DataBind()

            End If

        Catch ex As Exception
            '(ex.Message.ToString)
        End Try
    End Sub
    Private Sub gvPermohonanViremen_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvPermohonanViremen.Sorting
        Try

            Dim sortingDirection As String = String.Empty
            If direction = SortDirection.Ascending Then
                direction = SortDirection.Descending
                sortingDirection = "Desc"
            Else
                direction = SortDirection.Ascending
                sortingDirection = "Asc"
            End If

            Dim sortedView As New DataView(fFindRec(Trim(txtNoviremen.Text.TrimEnd)))
            sortedView.Sort = Convert.ToString(e.SortExpression + " ") & sortingDirection
            Session("SortedView") = sortedView
            gvPermohonanViremen.DataSource = sortedView
            gvPermohonanViremen.DataBind()

        Catch ex As Exception
            '(ex.Message.ToString)
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
End Class