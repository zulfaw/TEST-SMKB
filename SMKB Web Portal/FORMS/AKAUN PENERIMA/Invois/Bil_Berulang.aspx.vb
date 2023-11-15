Public Class Bil_Berulang
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

End Class

Public Class Bil_Hdr
    Public Property OrderID As String
    Public Property PenghutangID As String
    Public Property TkhMula As String
    Public Property TkhTamat As String
    Public Property Kontrak As String
    Public Property JenisUrusniaga As String
    Public Property Tujuan As String
    Public Property Jumlah As String
    Public Property tkhBil As String
    Public Property tempoh As String
    Public Property tempohbyrn As String
    Public Property norujukan As String
    Public Property Id As String



    Public Property OrderDetails As List(Of Bil_Dtl)

    Public Sub New()

    End Sub

    Public Sub New(orderId_ As String, PenghutangID_ As String, TkhMula_ As String, TkhTamat_ As String, Kontrak_ As String, JenisUrusniaga_ As String, Tujuan_ As String, Jumlah_ As String, tkhBil_ As String, tempoh_ As String, tempohbyrn_ As String, norujukan_ As String, Id_ As String, lOrderDetails_ As List(Of Bil_Dtl))
        OrderID = orderId_
        PenghutangID = PenghutangID_
        TkhMula = TkhMula_
        TkhTamat = TkhTamat_
        Kontrak = Kontrak_
        JenisUrusniaga = JenisUrusniaga_
        Tujuan = Tujuan_
        OrderDetails = lOrderDetails_
        Jumlah = Jumlah_
        tkhBil = tkhBil_
        tempoh = tempoh_
        tempohbyrn = tempohbyrn_
        norujukan = norujukan_
        Id = Id_
    End Sub
End Class

Public Class Bil_Dtl
    Public Property id As String
    Public Property OrderID As String
    Public Property ddlVot As String
    Public Property ddlPTJ As String
    Public Property ddlKw As String
    Public Property ddlKo As String
    Public Property ddlKp As String
    Public Property details As String
    Public Property quantity As Integer?
    Public Property price As Decimal?
    Public Property amount As Decimal?
    Public Property Diskaun As Decimal?
    Public Property Cukai As Decimal?
    Public Property Kadar_harga As Decimal?

    Public Property PenghutangID As String

    Public Sub New()

    End Sub

    Public Sub New(Optional id_ As String = "", Optional orderId_ As String = "", Optional ddlVot_ As String = "", Optional details_ As String = "",
                   Optional quantity_ As String = "", Optional price_ As String = "", Optional amount_ As String = "", Optional ddlPTJ_ As String = "",
                   Optional ddlKw_ As String = "", Optional ddlKo_ As String = "", Optional ddlKp_ As String = "", Optional Diskaun_ As String = "",
                   Optional Cukai_ As String = "", Optional Kadar_harga_ As String = "", Optional PenghutangID_ As String = "")
        id = id_
        ddlVot = ddlVot_
        details = details_
        quantity = quantity_
        price = price_
        amount = amount_
        OrderID = orderId_
        ddlPTJ = ddlPTJ_
        ddlKw = ddlKw_
        ddlKo = ddlKo_
        ddlKp = ddlKp_
        Diskaun = Diskaun_
        Cukai = Cukai_
        Kadar_harga = Kadar_harga_
        PenghutangID = PenghutangID_
    End Sub
End Class

'Public Class BilBerulang
'    Public Property OrderID As String
'    Public Property Bil As Integer
'    Public Property Jumlah As Decimal
'    Public Property FlagBayar As String
'    Public Property StatKontrak As String
'    Public Property Ulasan As String
'    Public Property NoRujukan As String
'    Public Property BilDetails As List(Of Bil_Dtl)
'End Class

'Public Class Bil_Hdr
'    Public Property OrderID As String
'    Public Property Id As String
'    Public Property PenghutangID As String
'    Public Property TkhMula As String
'    Public Property TkhTamat As String
'    Public Property Kontrak As String
'    Public Property JenisUrusniaga As String
'    Public Property Tujuan As String
'    Public Property Jumlah As String
'    Public Property TkhBil As String
'    Public Property Tempoh As String
'    Public Property Tempohbyrn As String
'    Public Property NoRujukan As String

'    Public Property OrderDetails As List(Of Bil_Dtl)
'End Class

'Public Class Bil_Dtl

'    Public Property OrderID As String
'    Public Property id As String
'    Public Property PenghutangID As String
'    Public Property ddlVot As String
'    Public Property ddlPTJ As String
'    Public Property ddlKw As String
'    Public Property ddlKo As String
'    Public Property ddlKp As String
'    Public Property details As String
'    Public Property quantity As Integer?
'    Public Property price As Decimal?
'    Public Property Diskaun As Decimal?
'    Public Property Cukai As Decimal?
'    Public Property amount As Decimal?
'    'Public Property Kadar_harga As Decimal?

'    Public Sub New(Optional id_ As String = "", Optional orderId_ As String = "", Optional ddlVot_ As String = "", Optional details_ As String = "",
'                  Optional quantity_ As String = "", Optional price_ As String = "", Optional amount_ As String = "", Optional ddlPTJ_ As String = "",
'                  Optional ddlKw_ As String = "", Optional ddlKo_ As String = "", Optional ddlKp_ As String = "", Optional Diskaun_ As String = "",
'                  Optional Cukai_ As String = "", Optional Kadar_harga_ As String = "", Optional PenghutangID_ As String = "")
'        id = id_
'        ddlVot = ddlVot_
'        details = details_
'        quantity = quantity_
'        price = price_
'        amount = amount_
'        OrderID = orderId_
'        ddlPTJ = ddlPTJ_
'        ddlKw = ddlKw_
'        ddlKo = ddlKo_
'        ddlKp = ddlKp_
'        Diskaun = Diskaun_
'        Cukai = Cukai_
'        PenghutangID = PenghutangID_
'    End Sub
'End Class