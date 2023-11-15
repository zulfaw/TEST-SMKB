Imports System.ComponentModel
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports Newtonsoft.Json
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
Imports System.Data.SqlClient
Imports System.Collections.Generic


Imports System.Drawing
Imports System.Globalization

Imports System.Net
Imports System.Net.Mail
Imports System.Web.Configuration

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class PenutupanAkaunWS
    Inherits System.Web.Services.WebService

    <System.Web.Services.WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetTahunProses(ByVal q As String) As String
        Dim tmpDT As DataTable = YearEndProcessing(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function YearEndProcessing(YearInput As String) As DataTable
        Dim db As New DBKewConn
        Dim query As String = "select Tahun from SMKB_Kod_Tahun Order by Tahun Desc"
        Dim param As New List(Of SqlParameter)

        Return db.Read(query, param)
    End Function


    <System.Web.Services.WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SubmitOrders(Month As String) As String
        Dim resp As New ResponseRepository
        MEP(Month)

        resp.Success("Update executed successfully.")
        Return JsonConvert.SerializeObject(resp.GetResult())
        'Return "Succesfully add bulan " & Month
    End Function

    Private Function MEP(Month)
        Dim db As New DBKewConn
        Dim queryString As String = "UPDATE [DbKewanganV4].[dbo].[SMKB_Lejar_Am] 
                                    set Dr_1 = (SELECT SUM(DEBIT) as DEBIT
                                    FROM SMKB_Jurnal_Hdr A JOIN SMKB_Jurnal_Dtl B ON A.No_Jurnal = B.No_Jurnal 
                                    WHERE MONTH(Tkh_Transaksi) = 7 and B.[Status] =1)"

        Dim param As New List(Of SqlParameter)
        Return db.Process(queryString, param)
    End Function


    '-----------------------------------------------New testing

    <System.Web.Services.WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ProcessSelectedValue(selectedValue As String) As String
        ' Process the selected value (you can add your logic here)
        Return "You selected: " & selectedValue
    End Function
    '-----------------------------------------------end of new testing

    Public Class ResponseRepository
        Private response_ As Response
        '00 success
        '01 failed
        Public Sub New()
            response_ = New Response
            response_.Code = ""
            response_.Payload = Nothing
            Failed("")
        End Sub

        Public Sub Failed(message As String, Optional kod As String = "01")
            response_.Code = kod
            response_.Status = False
            response_.Message = message
        End Sub

        Public Sub Success(message As String, Optional kod As String = "00", Optional obj As Object = Nothing)
            response_.Code = kod
            response_.Status = True
            response_.Message = message
            response_.Payload = obj
        End Sub

        Public Sub SuccessPayload(obj As Object)
            response_.Code = "00"
            response_.Status = True
            response_.Message = "Request has been successfully."
            response_.Payload = obj
        End Sub

        Public Function GetResult() As Response
            Return response_
        End Function
    End Class


    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadOrderRecord_SenaraiLulusTransaksiJurnal() As String
        Dim dt As DataTable = GetOrder_SenaraiLulusTransaksiJurnal()
        Return JsonConvert.SerializeObject(dt)
    End Function


    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetOrder_SenaraiLulusTransaksiJurnal() As DataTable
        Dim db = New DBKewConn

        Dim query As String = "select A.No_Jurnal , A.No_Rujukan, A.Butiran, Jumlah , FORMAT (A.Tkh_Transaksi, 'dd-MM-yyyy') as Tkh_Transaksi , 
        (select Butiran from SMKB_Kod_Status_Dok where Kod_Modul = '04' and Status = 1 and Kod_Status_Dok = A.Kod_Status_Dok) as Kod_Status_Dok ,
        (select Butiran from SMKB_Jurnal_Jenis_Trans b where b.Kod_Trans = A.Jenis_Trans) as Jenis_Trans , MS01_Nama as No_Staf ,
		 Kod_Vot ,Debit , Kredit
        from SMKB_Jurnal_Hdr A 
		INNER JOIN SMKB_Jurnal_Dtl AS DT ON A.No_Jurnal = DT.No_Jurnal
		LEFT JOIN SMKB_Status_Dok AS B ON A.No_Jurnal = B.No_Rujukan AND B.Kod_Status_Dok = '02'
		Left join [DEVMIS\SQL_INS01].dbStaf.dbo.MS01_Peribadi as c on c.MS01_NoStaf = b.No_Staf
        where A.status = 1  and A.Kod_Status_Dok in ('04') order by a.Tkh_Transaksi desc"

        'Dim query As String = "Select No_Rujukan,No_Rujukan,No_Rujukan,No_Rujukan,No_Rujukan,No_Rujukan,No_Rujukan,No_Rujukan from SMKB_Status_Dok"

        Return db.Read(query)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadTest() As String
        Dim dt As DataTable = GetLoadTest()
        Return JsonConvert.SerializeObject(dt)
    End Function

    Private Function GetLoadTest() As DataTable
        Dim db = New DBKewConn
        Dim query As String = "Select top(10) No_Rujukan From SMKB_Status_Dok"
        Return db.Read(Query)
    End Function

End Class