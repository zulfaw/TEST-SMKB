'Imports System.ComponentModel
'Imports System.Web.Services
'Imports System.Web.Services.Protocols

'' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
'' <System.Web.Script.Services.ScriptService()> _
Imports System.ComponentModel
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports Newtonsoft.Json
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
'Imports System.Web.Http
Imports System.Data.SqlClient
Imports System.Collections.Generic

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class LejarPemiutangWS
    Inherits System.Web.Services.WebService
    Dim dt As DataTable

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadOrderRecord_TransaksiLejarPemiutang(tahun As String, syarikat As String, ptj As String) As String
        Dim resp As New ResponseRepository
        If tahun = "" Or syarikat = "" Or ptj = "" Then
            Return JsonConvert.SerializeObject(New DataTable)
        End If
        Session("tahun") = tahun
        Session("syarikat") = syarikat
        Session("ptj") = ptj
        dt = GetOrder_TransaksiLejarPemiutang(tahun, syarikat, ptj)

        Dim totalRecords As Integer = dt.Rows.Count

        Return JsonConvert.SerializeObject(dt)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetOrder_TransaksiLejarPemiutang(tahun As String, syarikat As String, ptj As String) As DataTable
        Dim db = New DBKewConn
        Dim dt As New DataTable

        Dim connectionString As String = "server=devmis12.utem.edu.my;database=DbKewanganV4;uid=Smkb;pwd=Smkb@Dev2012;"

        Using sqlconn As New SqlConnection(connectionString)
            Dim cmd As New SqlCommand
            sqlconn.Open()

            Dim query As String

            If ptj = "00" Then

                query = "Select *,B.Nama_Pemiutang from SMKB_Lejar_Pemiutang A
                        INNER JOIN SMKB_Pemiutang_Master B ON A.Kod_Pemiutang=B.Kod_Pemiutang
                        where Tahun = @tahun
                        Order by Kod_Vot,Kod_Kump_Wang"
            Else

                query = "Select *,B.Nama_Pemiutang from SMKB_Lejar_Pemiutang A
                        INNER JOIN SMKB_Pemiutang_Master B ON A.Kod_Pemiutang=B.Kod_Pemiutang
                        where Tahun = @tahun
                        And Kod_PTJ >= @ptj and Kod_PTJ <= @ptj
                        Order by Kod_Vot,Kod_Kump_Wang"
            End If

            cmd.Connection = sqlconn
            cmd.CommandText = query

            cmd.Parameters.Add(New SqlParameter("@tahun", tahun))
            cmd.Parameters.Add(New SqlParameter("@ptj", ptj))
            dt.Load(cmd.ExecuteReader())
            Return dt
        End Using
    End Function

End Class