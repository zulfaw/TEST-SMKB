
Imports System.Web.Services
Imports Antlr.Runtime
Imports Newtonsoft.Json
Public Class Pelbagai
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

End Class

Public Class MhnPelbagai
    Public Property mohonID As String
    Public Property stafID As String
    Public Property NoTel As String
    Public Property TkhMula As String
    Public Property TkhTamat As String
    Public Property PtjMohon As String
    Public Property StatusPemohon As String
    Public Property TujuanProgram As String
    Public Property TempatProgram As String
    Public Property Peruntukan As String
    Public Property JnsBayar As String
    Public Property TunjukSebab As String
    Public Property TkhAdvPerlu As String
    Public Property TkhMohon As String
    Public Property NoPemohon As String
    Public Property KodVot As String
    Public Property kodPTj As String
    Public Property kodKW As String
    Public Property kodKO As String
    Public Property kodKP As String
    Public Property Jumlah As Decimal
    Public Property GroupItem As List(Of ItemDetail)
End Class

Public Class ItemDetail
    Public Property id As String
    Public Property mohonID As String
    Public Property txtKeperluan As String
    Public Property Kuantiti As Integer
    Public Property txtKadarHarga As Decimal
    Public Property txtAnggaran As Decimal
    Public Property totalKt As Decimal


    Public Sub New()

    End Sub

    Public Sub New(Optional id_ As String = "", Optional mohonID_ As String = "", Optional txtKeperluan_ As String = "",
                  Optional Kuantiti_ As Decimal = 0.00, Optional txtKadarHarga_ As Decimal = 0.00, Optional txtAnggaran_ As Decimal = 0.00,
                  Optional totalKt_ As Decimal = 0.00)
        id = id_
        mohonID = mohonID_
        txtKeperluan = txtKeperluan_
        Kuantiti = Kuantiti_
        txtKadarHarga = txtKadarHarga_
        txtAnggaran = txtAnggaran_
        totalKt = totalKt_


    End Sub

    Public Class Lampiran
        Public Property idSurat As String
        Public Property mohonID As String
        Public Property namaSurat As String
        Public Property folderSurat As String
        Public Property staHantarSurat As String
        Public Property idBajet As String
        Public Property namaBajet As String
        Public Property folderBajet As String
        Public Property staHantarBajet As String
        Public Property Tkh_Upload As String


    End Class
End Class
