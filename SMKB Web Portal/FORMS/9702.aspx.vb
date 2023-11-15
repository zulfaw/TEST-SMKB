Public Class _9702
    Inherits System.Web.UI.Page

    Public isFresh As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            hidFresh.Text = "Y"
            Exit Sub
        End If
        hidFresh.Text = ""
    End Sub

End Class