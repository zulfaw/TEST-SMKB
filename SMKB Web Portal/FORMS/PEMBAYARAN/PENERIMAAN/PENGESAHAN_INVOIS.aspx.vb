﻿Imports System.Web.Services
Imports Newtonsoft.Json
Imports System.Data

Public Class PENGESAHAN_INVOIS
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim A = "ayam"
    End Sub


End Class

Public Class SahItemList
    Public Property text As String
    Public Property value As String

    Public Sub New()

    End Sub

    Public Sub New(text_, val_)
        text = text_
        value = val_
    End Sub
End Class

Public Class SahOrder
    Public Property OrderID As String
    Public Property PenghutangID As String
    Public Property TkhMula As String
    Public Property TkhTamat As String
    Public Property Kontrak As String
    Public Property JenisUrusniaga As String
    Public Property Tujuan As String
    Public Property OrderDetails As List(Of SahOrderDetail)

    Public Sub New()

    End Sub

    Public Sub New(orderId_ As String, PenghutangID_ As String, TkhMula_ As String, TkhTamat_ As String, Kontrak_ As String, JenisUrusniaga_ As String, Tujuan_ As String, lOrderDetails_ As List(Of SahOrderDetail))
        OrderID = orderId_
        PenghutangID = PenghutangID_
        TkhMula = TkhMula_
        TkhTamat = TkhTamat_
        Kontrak = Kontrak_
        JenisUrusniaga = JenisUrusniaga_
        Tujuan = Tujuan_
        OrderDetails = lOrderDetails_
    End Sub
End Class

Public Class SahOrderDetail
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

    Public Sub New()

    End Sub

    Public Sub New(Optional id_ As String = "", Optional orderId_ As String = "", Optional ddlVot_ As String = "", Optional details_ As String = "",
                   Optional quantity_ As String = "", Optional price_ As String = "", Optional amount_ As String = "", Optional ddlPTJ_ As String = "",
                   Optional ddlKw_ As String = "", Optional ddlKo_ As String = "", Optional ddlKp_ As String = "")
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
    End Sub

End Class