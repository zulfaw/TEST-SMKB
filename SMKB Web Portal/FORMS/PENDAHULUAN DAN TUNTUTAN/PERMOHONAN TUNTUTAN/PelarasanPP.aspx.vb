Public Class PelarasanPP
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

End Class

Public Class MhnTuntutan
    Public Property mohonID As String
    Public Property noPendahuluan As String
    Public Property stafID As String
    Public Property TkhMohon As String
    Public Property NoTel As String
    Public Property PtjMohon As String
    Public Property blnTuntut As String
    Public Property thnTuntut As String
    Public Property StatusPemohon As String
    Public Property NoPemohon As String
    Public Property TujuanMohon As String
    Public Property kodPTj As String
    Public Property kodKW As String
    Public Property kodKO As String
    Public Property kodKP As String
    Public Property jumlahPendahuluan As Decimal
    Public Property GroupItem As List(Of ItemDetail)
End Class
Public Class UploadResit
    Public Property idItem As String
    Public Property mohonID As String
    Public Property namaItem As String
    Public Property kuantiti As String
    Public Property jumlah As String
    Public Property Tkh_Upload As String


End Class
Public Class KiraTuntut

    Public Property mohonID As String
    Public Property jumlahPendahuluan As Decimal
    Public Property jumlahTuntut As Decimal
    Public Property bakiTuntut As Decimal

End Class