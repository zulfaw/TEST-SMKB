Public Class Semakan_Arahan
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None
    End Sub
End Class

Public Class SemakArahan
        Public Property No_Arahan As String
        Public Property No_Mohon As String
        Public Property No_Surat As String
        Public Property No_Staf_Peg_AK As String
        Public Property Kod_PTJ As String
        Public Property KW As String
        Public Property Kod_Vot As String
    Public Property Tkh_Mula As String
    Public Property Tkh_Tamat As String
    Public Property Lokasi As String
        Public Property PeneranganK As String
        Public Property No_Staf_Sah As String
    Public Property Ketua_PTJ As String

    Public Property No_Staf_Lulus As String
    Public Property userID As String
        Public Property Jen_Dok As String
        Public Property Folder As String
        Public Property File_Name As String

    Public Property Tkh_Upload As String

End Class
