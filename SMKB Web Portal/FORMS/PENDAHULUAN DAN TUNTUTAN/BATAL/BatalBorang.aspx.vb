Public Class BatalBorang
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Class BatalPermohonan
        Public Property mohonID As String
        Public Property stafID As String
        Public Property statusDok As String
        Public Property catatan As String
        Public Property Email As String

    End Class

End Class