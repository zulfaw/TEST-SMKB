Imports System.Web.Services
Imports Newtonsoft.Json

Public Class PermohonanDiri
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


End Class

Public Class MhnAdvance
    Public Property OrderID As String
    Public Property stafID As String
    Public Property noTel As String
    Public Property JnsTugas As String
    Public Property JnsJalan As String
    Public Property JnsTempat As String
    Public Property JnsPginapan As String

    Public Property PtjMohon As String
    'Public Property JnsBayar As String
    Public Property TkhMula As String
    Public Property TkhTamat As String
    Public Property JnsBayar As String
    Public Property Tempoh As String
    Public Property Lokasi As String
    Public Property Tujuan As String
    'Public Property ArahanK As String
    Public Property statusPemohon As String
    Public Property File_Name As String
    Public Property Folder As String
    Public Property Jen_Dok As String
    Public Property TkhAdvance As String
    Public Property JumlahAll As String
    Public Property hadMin As String
    Public Property KodVot As String
    Public Property kodPTj As String
    Public Property kodKW As String
    Public Property kodKO As String
    Public Property kodKP As String
    Public Property hidPtj As String
    Public Property staMakan As String
    Public Property tkhMohon As String
    Public Property noPemohon As String
    Public Property Tkh_Upload As String

    'Public Property param.Add(New SqlParameter("@JenisPginap", AdvList.JnsPginapan)) As Boolean


    ' Public Property OrderDetails As List(Of OrderDetail)

    Public Sub New()

    End Sub

    Public Sub New(OrderID_ As String, tkhMohon_ As String, stafID_ As String, noTel_ As String, JnsTugas_ As String, JnsJalan_ As String, JnsTempat_ As String, JnsPginapan_ As String,
                   JnsBayar_ As String, TkhMula_ As String, TkhTamat_ As String, Tempoh_ As String, Lokasi_ As String, Tujuan_ As String,
                   File_Name_ As String, Folder_ As String, TkhAdvance_ As String, JumlahAll_ As String, hadMin_ As String, KodVot_ As String, kodPTj_ As String,
                   kodKW_ As String, kodKO_ As String, kodKP_ As String)

        OrderID = OrderID_
        tkhMohon = tkhMohon_
        stafID = stafID_
        noTel = noTel_
        JnsTugas = JnsTugas_
        JnsJalan = JnsJalan_
        JnsTempat = JnsTempat_
        JnsPginapan = JnsPginapan_
        JnsBayar = JnsBayar_
        TkhMula = TkhMula_
        TkhTamat = TkhTamat_
        Tempoh = Tempoh_
        Lokasi = Lokasi_
        Tujuan = Tujuan_
        File_Name = File_Name_
        Folder = Folder_
        TkhAdvance = TkhAdvance_
        JumlahAll = JumlahAll_
        hadMin = hadMin_
        KodVot = KodVot_
        kodPTj = kodPTj_
        kodKW = kodKW_
        kodKO = kodKO_
        kodKP = kodKP_

    End Sub
End Class
