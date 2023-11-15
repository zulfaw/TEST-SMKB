Public Class DaftarPenghutang
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

End Class

Public Class Penghutang
    Public Property Nama As String
    Public Property Id As String
    Public Property OldId As String
    Public Property NoAkaun As String
    Public Property Alamat1 As String
    Public Property Alamat2 As String
    Public Property KodNegara As String
    Public Property KodNegeri As String
    Public Property Poskod As String
    Public Property NoTelefon As String
    Public Property Email As String
    Public Property KategoriPenghutang As String
    Public Property Status As String
End Class