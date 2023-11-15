Imports System.Web.Services
Imports Newtonsoft.Json

Public Class PengesahanPD
    Inherits System.Web.UI.Page

    Public dsKod As New DataSet
    Public dvKodKW As New DataView
    Dim dbconn As New DBKewConn

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Dim hadMin As String = txtHadMin.Value
        'Dim updatedvalue As String = Convert.ToDecimal(hadMin).ToString("N2")
        'txtHadMin.Value = updatedvalue
        ' fCariPTJ()
    End Sub
    Public Class MaklumatPermohonanPD
        Public Property Id As String
        Public Property KategoriPenghutang As String
        Public Property Nama As String
        Public Property IdPenghutang As String
        Public Property Email As String
        Public Property NoTelefon As String
    End Class


    Public Class SokongPD
        Public Property mohonID As String
        Public Property stafID As String
        Public Property statusDok As String
        Public Property catatan As String
        Public Property Email As String

    End Class





End Class