Public Class Permohonan
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

End Class

Public Class ItemList2
    Public Property text As String
    Public Property value As String

    Public Sub New()

    End Sub

    Public Sub New(text_, val_)
        text = text_
        value = val_
    End Sub
End Class

Public Class OrderM

    Public Property OrderMId As String
    Public Property Tarikh As String
    Public Property NoPtj As String
    Public Property Kumpulan As String
    Public Property Jumlah As String
    Public Property OrderMDetails As List(Of OrderMDetail)

    Public Sub New()

    End Sub

    Public Sub New(OrderMId_ As String, Tarikh_ As String, NoPtj_ As String, Kumpulan_ As String, Jumlah_ As String, lOrderMDetails_ As List(Of OrderMDetail))
        OrderMId = OrderMId_
        Tarikh = Tarikh_
        NoPtj = NoPtj_
        Kumpulan = Kumpulan_
        Jumlah = Jumlah_
        OrderMDetails = lOrderMDetails_
    End Sub

End Class

Public Class OrderMDetail
    Public Property id As String
    Public Property OrderMId As String
    Public Property butiran As String
    Public Property resit As String
    Public Property ddlcoa As String
    Public Property kw As String
    Public Property ko As String
    Public Property kp As String
    Public Property ptj As String
    Public Property jumlah As Decimal
    Public Property baki As Decimal

    Public Sub New()

    End Sub

    Public Sub New(Optional id_ As String = "", Optional OrderMid_ As String = "", Optional butiran_ As String = "", Optional resit_ As String = "",
                   Optional ptj_ As String = "", Optional kw_ As String = "", Optional ko_ As String = "", Optional kp_ As String = "",
                   Optional ddlcoa_ As String = "", Optional jumlah_ As Decimal = 0.00, Optional baki_ As Decimal = 0.00)
        id = id_
        OrderMId = OrderMid_
        butiran = butiran_
        resit = resit_
        ptj = ptj_
        kw = kw_
        ko = ko_
        kp = kp_
        ddlcoa = ddlcoa_
        jumlah = jumlah_
        baki = baki_

    End Sub
End Class