Imports System.ComponentModel
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports Newtonsoft.Json
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
Imports System.Web.Http
Imports System.Data.SqlClient

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService()>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class Transaksi_EOT
    Inherits System.Web.Services.WebService

    Dim sqlcmd As SqlCommand
    Dim sqlcon As SqlConnection
    Dim sqlread As SqlDataReader
    Dim dt As DataTable

    Public Function HelloWorld() As String
        Return "Hello World"
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetPegPengesah(ByVal q As String) As String

        Dim tmpDT As DataTable = GetPengesah(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetPengesah(kodPTJ As String) As DataTable
        Dim db As New DBKewConn
        Dim query As String = "SELECT distinct a.MS01_NoStaf,a.MS01_Nama,(a.ms01_nostaf + ' - ' + a.ms01_nama) as NAMA FROM MS01_Peribadi a 
                            inner join MS02_Perjawatan b ON a.MS01_NoStaf = b.MS01_NoStaf  where a.MS01_Status='1' "
        Dim param As New List(Of SqlParameter)

        If kodPTJ <> "" Then
            query &= " and c.ms08_staterkini='1' and c.ms08_Pejabat=@kodPTJ  ORDER BY a.MS01_Nama "
        Else
            query &= " ORDER BY a.MS01_Nama "
        End If

        param.Add(New SqlParameter("@kodPTJ", kodPTJ))
        Return db.Read(query, param)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetStaf() As DataTable
        Dim db = New DBKewConn

        Dim query As String = "select NoStaf, Nama, singkat As PTJ from [qa11].dbstaf.dbo.vperibadi12"

        Return db.Read(query)
    End Function

    Private Function fGetARNo() As String
        Dim db = New DBKewConn
        Dim lastID As Integer = 1
        Dim newARNo As String = ""
        Dim param As New List(Of SqlParameter)

        Dim query As String = "SELECT   Kod_Modul, Prefix, No_Akhir, Tahun, Butiran, Kod_PTJ, ID
                               FROM  SMKB_No_Akhir WHERE Kod_Modul = 'EOT' AND Prefix = 'AR'
                               AND Tahun = '{Date.Now.Year}'"

        dt = db.Read(query)
        If dt.Rows.Count > 0 Then
            lastID = CInt(dt.Rows(0).Item("No_Akhir")) + 1
        End If

        newARNo = lastID
        Return newARNo
    End Function


    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadSenarai(ByVal id As String) As String
        Dim resp As New ResponseRepository



        'dt = GetSenarai(id)
        'resp.SuccessPayload(dt)

        'Return JsonConvert.SerializeObject(resp.GetResult())
    End Function


    Private Function GetOrder(kod As String) As DataTable
        Dim db = New db

        Dim query As String = "SELECT A.order_id, B.id, b.order_id, b.ddlVot,  
        C.details as detailvot, b.details, b.quantity, b.price, b.amount 
        FROM orders A
        INNER JOIN orderDetails B
	        ON A.order_id = B.order_id 
        INNER JOIN vot C 
	        on B.ddlvot = C.ddlvot"

        Dim param As New List(Of SqlParameter)
        If kod <> "" Then
            query &= "  WHERE A.order_id = @ord "
            param.Add(New SqlParameter("@ord", kod))
        End If

        Return db.Read(query, param)
    End Function
End Class