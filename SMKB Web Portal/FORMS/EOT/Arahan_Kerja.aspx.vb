Public Class Arahan_Kerja
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None


    End Sub

End Class


Public Class ArahanK


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
    Public Property No_Staf_Lulus As String
    Public Property userID As String
    Public Property Jen_Dok As String
    Public Property Folder As String
    Public Property File_Name As String

    Public Property Tkh_Upload As String


    Public Sub New()

    End Sub

    Public Sub New(No_Arahan_ As String, No_Mohon_ As String, No_Surat_ As String, No_Staf_Peg_AK_ As String, Kod_PTJ_ As String, KW_ As String, Kod_Vot_ As String, Tkh_Mula_ As Date, Tkh_Tamat_ As Date, Lokasi_ As String, PeneranganK_ As String)
        No_Arahan = No_Arahan_
        No_Mohon = No_Mohon_
        No_Surat = No_Surat_
        No_Staf_Peg_AK = No_Staf_Peg_AK_
        Kod_PTJ = Kod_PTJ_
        KW = KW_
        Kod_Vot = Kod_Vot_
        Tkh_Mula = Tkh_Mula_
        Tkh_Tamat = Tkh_Tamat_
        Lokasi = Lokasi_
        PeneranganK = PeneranganK_



    End Sub

End Class
