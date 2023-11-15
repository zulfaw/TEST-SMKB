Imports System.Web.Services
Imports Newtonsoft.Json
Imports System.Data
Public Class KategoriPenghutang
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim A = "ayam"
    End Sub


End Class

Public Class CategoryPenghutang
    Public Property Kod As String
    Public Property OldKod As String
    Public Property Butiran As String
    Public Property Status As Integer
End Class