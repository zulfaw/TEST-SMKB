Public Class PenerimaanPD
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Class Terimaan
        Public Property mohonID As String
        Public Property stafID As String
        Public Property jumlahMohon As String
        Public Property jumlahLulus As String
        Public Property TkhMula As String
        Public Property TkhTamat As String
        Public Property statusDok As String
        Public Property Tempoh As String
        Public Property catatan As String


    End Class

End Class