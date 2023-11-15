Imports System.Web.Services
Imports Newtonsoft.Json
Imports System.Data
Public Class Daftar_Invois
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim A = "ayam"
    End Sub


End Class

Public Class PItemList
    Public Property text As String
    Public Property value As String

    Public Sub New()

    End Sub

    Public Sub New(text_, val_)
        text = text_
        value = val_
    End Sub
End Class

Public Class POrder
    Public Property OrderID As String
    Public Property JenisInvois As String
    Public Property JenisBayar As String
    Public Property NoInvois As String
    Public Property TkhInvois As String
    Public Property TkhTerimaInvois As String
    Public Property NoDO As String
    Public Property TkhDO As String
    Public Property TkhTerimaDO As String
    Public Property BayarKepada As String
    Public Property Tujuan As String
    Public Property Jumlah As String

    'penerima
    Public Property Bil As String
    Public Property kodPemiutang As String


    Public Property OrderDetails As List(Of POrderDetail)
    'Public Property PenerimaOrder As List(Of PenerimaOrder)

    Public Sub New()

    End Sub

    Public Sub New(orderId_ As String, JenisInvois_ As String, JenisBayar_ As String, NoInvois_ As String, TkhInvois_ As String, TkhTerimaInvois_ As String, NoDO_ As String,
                   TkhDO_ As String, TkhTerimaDO_ As String, Tujuan_ As String, Jumlah_ As String, Bil_ As String, kodPemiutang_ As String)
        OrderID = orderId_
        JenisInvois = JenisInvois_
        JenisBayar = JenisBayar_
        NoInvois = NoInvois_
        TkhInvois = TkhInvois_
        TkhTerimaInvois = TkhTerimaInvois_
        NoDO = NoDO_
        TkhDO = TkhDO_
        TkhTerimaDO = TkhTerimaDO_
        Tujuan = Tujuan_
        Jumlah = Jumlah_
        Bil = Bil_
        kodPemiutang = kodPemiutang_

    End Sub
End Class

Public Class POrderDetail
    Public Property id As String
    Public Property OrderID As String
    Public Property ddlVot As String
    Public Property ddlPTJ As String
    Public Property ddlKw As String
    Public Property ddlKo As String
    Public Property ddlKp As String
    Public Property details As String
    Public Property quantity As String
    Public Property price As String
    Public Property amount As String
    Public Property cukai As String
    Public Property diskaun As String


    Public Sub New()

    End Sub

    Public Sub New(Optional id_ As String = "", Optional orderId_ As String = "", Optional ddlVot_ As String = "", Optional details_ As String = "",
                   Optional quantity_ As String = "", Optional price_ As String = "", Optional amount_ As String = "", Optional cukai_ As String = "",
                   Optional diskaun_ As String = "", Optional ddlPTJ_ As String = "",
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
        cukai = cukai_
        diskaun = diskaun_
    End Sub

End Class

Public Class PenerimaOrder
    Public Property OrderID As String
    Public Property Bil As String
    Public Property Kategori As String
    Public Property kodPemiutang As String
    Public Property AmaunSebenar As Decimal

    Public Sub New()

    End Sub

    Public Sub New(Optional orderId_ As String = "", Optional Bil_ As String = "", Optional Kategori_ As String = "",
                   Optional KodPemiutang_ As String = "", Optional AmaunSebenar_ As String = "")

        OrderID = orderId_
        Bil = Bil_
        Kategori = Kategori_
        kodPemiutang = KodPemiutang_
        AmaunSebenar = AmaunSebenar_
    End Sub

End Class