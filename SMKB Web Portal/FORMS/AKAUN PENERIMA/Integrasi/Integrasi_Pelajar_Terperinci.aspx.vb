Public Class Integrasi_Pelajar_Terperinci
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim penghutang As String = Request.QueryString("kodpenghutang")
        lblhidkodpenghutang.Text = penghutang
    End Sub

End Class
Public Class Order_terima_pelajar
    Public Property OrderID As String
    Public Property PenghutangID As String
    Public Property JumlahTerima As String
    Public Property ModTerima As String
    Public Property KodBank As String
    Public Property NoBil As String
    Public Property Tujuan As String
    Public Property Jumlah As String


    Public Property OrderDetails As List(Of OrderDetail_inv)

    Public Sub New()

    End Sub

    Public Sub New(orderId_ As String, PenghutangID_ As String, JumlahTerima_ As String, ModTerima_ As String, KodBank_ As String, NoBil_ As String, Tujuan_ As String, Jumlah_ As String, lOrderDetails_ As List(Of OrderDetail_inv))
        OrderID = orderId_
        PenghutangID = PenghutangID_
        JumlahTerima = JumlahTerima_
        ModTerima = ModTerima_
        KodBank = KodBank_
        NoBil = NoBil_
        Tujuan = Tujuan_
        OrderDetails = lOrderDetails_
        Jumlah = Jumlah_
    End Sub
End Class