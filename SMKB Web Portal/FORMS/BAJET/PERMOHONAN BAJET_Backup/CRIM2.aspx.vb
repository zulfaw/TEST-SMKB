
Public Class CRIM
    Inherits System.Web.UI.Page
    Dim TotalJumBesar As Decimal = 0.00

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("LoggedIn") <> "" Then

                fbindGVButiran()
            Else
            End If
        End If
    End Sub

    Private Sub btnStep2_Click(sender As Object, e As EventArgs) Handles btnStep2.Click
        Try
            Response.Redirect("/CRIM1.aspx")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvButiran_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvButiran.RowDataBound
        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(1).Text = "Jumlah Besar (RM)"
            e.Row.Cells(2).Text = ""
            e.Row.Cells(3).Text = ""
            e.Row.Cells(3).Text = ""
            e.Row.Cells(4).Text = ""
            e.Row.Cells(4).Text = ""
            e.Row.Cells(5).Text = TotalJumBesar.ToString("#,##0.00")
            e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Right

            TotalJumBesar += 0.00

        End If
    End Sub

    Private Function fbindGVButiran()
        Try
            Dim dt As New DataTable
            dt.Columns.Add("Bil", GetType(Integer))
            dt.Columns.Add("KodVot", GetType(String))
            dt.Columns.Add("Item", GetType(String))
            dt.Columns.Add("amount", GetType(String))

            dt.Rows.Add(1, "", "", "")
            gvButiran.DataSource = dt
            gvButiran.DataBind()

        Catch ex As Exception

        End Try
    End Function

End Class