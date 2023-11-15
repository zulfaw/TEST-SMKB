
Public Class Master_Gaji
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' set dropdown default value using selectedvalue
            ddlStatus.SelectedValue = "AKTIF"
        End If
    End Sub

End Class

Public Class DataMaster

    Public Property No_Staf As String
    Public Property Kod_Sumber As String
    Public Property Jenis_Trans As String
    Public Property Kod_Trans As String
    Public Property Tkh_Mula_Trans As String
    Public Property Tkh_Tamat_Trans As String
    Public Property AmaunTrans As String
    Public Property No_Trans As String
    Public Property Catatan As String
    Public Property Sta_Trans As String


End Class
Public Class DataStaf

    Public Property No_Staf As String
    Public Property Kat_Perkeso As String
    Public Property No_perkeso As String
    Public Property Proses_Gaji As String
    Public Property Proses_Pencen As String
    Public Property Proses_Kwsp As String
    Public Property Proses_Cukai As String
    Public Property Proses_Perkeso As String
    Public Property Proses_Bonus As String
    Public Property Tahan_Gaji As String
    Public Property Bayar_Cek As String

End Class