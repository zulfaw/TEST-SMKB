Public Class Unit_Ukuran
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not Page.IsPostBack) Then
            fbindGV()
        End If

        txtBoxKod.Focus()
    End Sub

    Private Function fbindGV()

        Try

            Dim strSql As String = "SELECT KodUkuran, Butiran FROM PS_Ukuran ORDER BY KODUKURAN"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fselectCommand(strSql)

            Dim dcBil = New DataColumn("Bil", GetType(Int32))
            Dim dcKodUkuran = New DataColumn("KodUkuran", GetType(String))
            Dim dcButiran = New DataColumn("Butiran", GetType(String))

            dt.Columns.Add(dcBil)
            dt.Columns.Add(dcKodUkuran)
            dt.Columns.Add(dcButiran)

            Dim intBil As Integer
            Dim strKodUkuran As String
            Dim strButiran As String

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                intBil = intBil + 1
                strKodUkuran = ds.Tables(0).Rows(i)(0).ToString
                strButiran = ds.Tables(0).Rows(i)(1).ToString
                dt.Rows.Add(intBil, strKodUkuran, strButiran)
            Next

            gvUnitUkuran.DataSource = dt
            gvUnitUkuran.DataBind()


        Catch ex As Exception
            'fErrorLog(ex.ToString)
        End Try

    End Function

    Private Sub gvUnitUkuran_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvUnitUkuran.PageIndexChanging

        Try
            'gvPeruntukan.PageIndex = e.NewPageIndex
            'gvPeruntukan.DataBind()
            'fbindGV()

            gvUnitUkuran.PageIndex = e.NewPageIndex
            If Session("SortedView") IsNot Nothing Then
                gvUnitUkuran.DataSource = Session("SortedView")
                gvUnitUkuran.DataBind()
            Else
                gvUnitUkuran.DataSource = BindGridView()
                gvUnitUkuran.DataBind()
            End If

        Catch ex As Exception
            'fErrorLog("")
        End Try


    End Sub

    Private Sub gvUnitUkuran_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvUnitUkuran.Sorting
        Try
            Dim sortingDirection As String = String.Empty
            If direction = SortDirection.Ascending Then
                direction = SortDirection.Descending
                sortingDirection = "Desc"
            Else
                direction = SortDirection.Ascending

                sortingDirection = "Asc"
            End If
            Dim sortedView As New DataView(BindGridView())
            sortedView.Sort = Convert.ToString(e.SortExpression + " ") & sortingDirection
            Session("SortedView") = sortedView
            gvUnitUkuran.DataSource = sortedView
            gvUnitUkuran.DataBind()

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

    Private Function BindGridView() As DataTable
        Dim strSql As String = "SELECT KodUkuran, Butiran FROM PS_Ukuran ORDER BY KODUKURAN"

        Dim ds As New DataSet
        Dim dbconn As New DBKewConn

        ds = dbconn.fselectCommand(strSql)
        Return ds.Tables(0)
    End Function

    Private Sub gvUnitUkuran_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvUnitUkuran.RowEditing
        lblStatus.Text = ""

        gvUnitUkuran.EditIndex = e.NewEditIndex
        fbindGV()

    End Sub

    Private Sub gvUnitUkuran_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvUnitUkuran.RowDeleting

        Dim strSql As String = $"DELETE FROM PS_Ukuran WHERE KodUkuran=('{e?.Values.Values(1)}')"
        Dim dbconn As New DBKewConn

        If dbconn.fUpdateCommand(strSql) > 0 Then
            fbindGV()
            lblStatus.Text = "Maklumat telah berjaya dipadam"
        End If
    End Sub

    
    Protected Sub btnTambah_Click(sender As Object, e As EventArgs) Handles btnTambah.Click
        Page.Validate()
        If Page.IsValid Then
            Dim strSql As String = $"INSERT INTO PS_Ukuran VALUES ('{txtBoxKod?.Text}', '{txtBoxButiran?.Text}')"

            Dim dbconn As New DBKewConn
            If dbconn.fInsertCommand(strSql) > 0 Then
                lblStatus.Text = "Maklumat telah berjaya disimpan"
                fbindGV()
                txtBoxKod.Text = ""
                txtBoxButiran.Text = ""
            Else
                lblStatus.Text = $"Maklumat tidak berjaya disimpan kerana terdapat kod {txtBoxKod?.Text} telah wujud"
            End If
        Else
            lblStatus.Text = ""
        End If
    End Sub

    Protected Sub gvUnitUkuran_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvUnitUkuran.RowUpdating

        Dim dbconn As New DBKewConn
        Dim row As GridViewRow = DirectCast(gvUnitUkuran.Rows(e.RowIndex), GridViewRow)
        Dim txtBxKod As TextBox = DirectCast(row.Cells(1).Controls(0), TextBox)
        Dim txtBxButiran As TextBox = DirectCast(row.Cells(2).Controls(0), TextBox)
        gvUnitUkuran.EditIndex = -1

        Dim strSql As String = $"UPDATE PS_Ukuran SET Butiran='{txtBxButiran?.Text}' WHERE KodUkuran=('{txtBxKod?.Text}')"
        If dbconn.fUpdateCommand(strSql) > 0 Then
            fbindGV()
            lblStatus.Text = "Maklumat telah berjaya dikemaskini"
        End If
    End Sub

    'Protected Sub gvUnitUkuran_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvUnitUkuran.RowCancelingEdit
    '    gvUnitUkuran.EditIndex = -1
    '    fbindGV()
    'End Sub

    Protected Sub gvUnitUkuran_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvUnitUkuran.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow And (e.Row.RowState = DataControlRowState.Edit Or e.Row.RowState = (DataControlRowState.Alternate Or DataControlRowState.Edit)) Then
            Dim txtBxKod As TextBox = DirectCast(e.Row.Cells(1).Controls(0), TextBox)
            txtBxKod.TextMode = TextBoxMode.SingleLine
            txtBxKod.Enabled = False
            txtBxKod.MaxLength = 2
            txtBxKod.Width = 50

            Dim txtBxBil As TextBox = DirectCast(e.Row.Cells(0).Controls(0), TextBox)
            txtBxBil.Enabled = False
        End If
    End Sub

    Protected Sub gvUnitUkuran_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvUnitUkuran.SelectedIndexChanged
        ' Get the selected row.
        Dim row As GridViewRow = gvUnitUkuran.SelectedRow

        ' Check the row state. If the row is not in edit mode and is selected,
        ' exit edit mode. This ensures that the GridView control exits edit mode
        ' when a user selects a different row while the GridView control is in 
        ' edit mode. Notice that the DataControlRowState enumeration is a flag
        ' enumeration, which means that you can combine values using bitwise
        ' operations.
        If row.RowState <> (DataControlRowState.Edit Or DataControlRowState.Selected) Then
            gvUnitUkuran.EditIndex = -1
            fbindGV()
        End If
    End Sub


End Class