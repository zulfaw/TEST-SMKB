Imports System.Web.Services
Imports Newtonsoft.Json

Public Class Dalam_Negara
    Inherits System.Web.UI.Page
    Public dsKod As New DataSet
    Public dvKodKW As New DataView
    Dim dbconn As New DBKewConn

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub


    Public Class MhnDlmNegara
        Public Property OrderID As String
        Public Property StafID As String
        Public Property noTel As String
        Public Property Tahun As String
        Public Property Bulan As String
        Public Property TkhMohon As String
        Public Property KumpWang As String
        Public Property KodOperasi As String
        Public Property KodPtj As String
        Public Property KodProjek As String
        Public Property hidPtjPemohon As String
        Public Property staPemohon As String
        Public Property NoPemohon As String
        Public Property noPendahuluan As String
        Public Property jumlahBaucer As String
        Public Property Jumlah As String
        Public Property JnsTuntutan As String
        Public Property sebabLewat As String




    End Class
End Class