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
Imports AjaxControlToolkit
Imports System.Reflection
Imports System

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.

<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class Proses_WS
    Inherits System.Web.Services.WebService

    Dim sqlcmd As SqlCommand
    Dim sqlcon As SqlConnection
    Dim sqlread As SqlDataReader
    Dim dt As DataTable
    Dim BulanGaji As String
    Dim TahunGaji As String
    Private strConnx As String = "Data Source=devmis12.utem.edu.my;Initial Catalog=dbKewanganV4;Persist Security Info=True;User ID=smkb;Password=Smkb@Dev2012;"


    Public Function HelloWorld() As String
        Return "Hello World"
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetBankMaster() As String
        'Dim newList As List(Of ItemList)
        'newList = listItem

        'If (q <> "") Then
        '    newList = listItem.Where(
        '        Function(x) x.value.Contains(q) Or x.text.Contains(q)
        '    ).ToList()
        'End If

        Dim tmpDT As DataTable = GetDataBank()
        Return JsonConvert.SerializeObject(tmpDT)
    End Function
    Private Function GetDataBank() As DataTable
        Dim db = New DBKewConn

        Dim query As String = $"select Kod_Bank, Nama_Sing from SMKB_Bank_Master where Kod_bank='76106' order by Kod_Bank"

        Return db.Read(query)
    End Function


    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadBlnThnGaji()
        Dim db As New DBKewConn


        Dim query As String = $"select bulan,tahun,cast(bulan as varchar(2)) + '/' + cast(tahun as varchar(5)) as butir from SMKB_Gaji_bulan;"
        Dim dt As DataTable = db.fSelectCommandDt(query)

        Return JsonConvert.SerializeObject(dt)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function fProsesGaji(ptjDr As String, ptjHg As String, nostafDr As String, nostafHg As String, kodparam As String)
        Dim resp As New ResponseRepository
        resp.Success("Data telah disimpan")
        Dim success As Integer = 0
        Dim counter As Integer = 0
        Dim sqlComm As New SqlCommand
        Dim cmd = New SqlCommand
        Dim dt As New DataTable
        Dim problem As String = ""
        Dim dbconn As New DBKewConn

        dt = dbconn.Read("SELECT bulan,tahun FROM SMKB_Gaji_Bulan ")
        If dt.Rows.Count > 0 Then
            BulanGaji = dt.Rows(0).Item("bulan").ToString()
            TahunGaji = dt.Rows(0).Item("tahun").ToString()
        End If

        Dim strSql = "select count(*) from smkb_gaji_param  where kod_param =  '" & kodparam & "' and status in ('02')"
        Dim intCnt As Integer = DBConn.fSelectCount(strSql)
        If intCnt <= 0 Then
            resp.Failed("Pengesahan Statutory tidak lengkap! Sila buat Pengesahan Statutory.")
            Return JsonConvert.SerializeObject(resp.GetResult())
            Exit Function
        End If

        Dim strSql1 = "select count(*) from SMKB_Gaji_Tarikh_Gaji  where bulan =  '" & BulanGaji & "' and tahun =  '" & TahunGaji & "'"
        Dim intCnt1 As Integer = dbconn.fSelectCount(strSql1)
        If intCnt1 <= 0 Then
            resp.Failed("Proses gaji tidak boleh dilakukan lagi kerana Tarikh Gaji tidak lengkap!")
            Return JsonConvert.SerializeObject(resp.GetResult())
            Exit Function
        End If

        Dim strSql2 = "select count(*) from SMKB_Gaji_AP  where kod_param =  '" & kodparam & "' and status = '02'"
        Dim intCnt2 As Integer = dbconn.fSelectCount(strSql2)
        If intCnt2 > 0 Then
            resp.Failed("Proses gaji tidak boleh dilakukan lagi kerana Baucar sudah dikeluarkan!")
            Return JsonConvert.SerializeObject(resp.GetResult())
            Exit Function
        End If

        Dim strSql3 = "select count(*) from SMKB_Gaji_AP  where kod_param =  '" & kodparam & "' and status = '01'"
        Dim intCnt3 As Integer = dbconn.fSelectCount(strSql3)
        If intCnt3 > 0 Then
            resp.Failed("Proses gaji tidak boleh dilakukan lagi kerana Draf Baucar sudah dikeluarkan!")
            Return JsonConvert.SerializeObject(resp.GetResult())
            Exit Function
        End If

        Using sqlcon = New SqlConnection(strConnx)
            sqlComm.Connection = sqlcon
            sqlComm.CommandTimeout = 600
            sqlComm.CommandText = "USP_PROSES_GAJI"
            sqlComm.CommandType = CommandType.StoredProcedure

            sqlComm.Parameters.Clear()

            sqlComm.Parameters.AddWithValue("@iBulan", BulanGaji)
            sqlComm.Parameters.AddWithValue("@iTahun", TahunGaji)
            sqlComm.Parameters.AddWithValue("@strStafDr", nostafDr)
            sqlComm.Parameters.AddWithValue("@strStafHg", nostafHg)
            sqlComm.Parameters.AddWithValue("@strPtjDr", ptjDr)
            sqlComm.Parameters.AddWithValue("@strPtjHg", ptjHg)

            sqlcon.Open()

            'sqlComm.ExecuteNonQuery()
            'Dim rowsAffected As Integer = sqlComm.ExecuteNonQuery()
            Dim X = sqlComm.ExecuteNonQuery()
            If X > 0 Then
                success = 1
            Else

                success = 0
            End If
        End Using

        If success = 1 Then
            resp.Success("Rekod berjaya disimpan")
        Else
            resp.Failed("Rekod tidak berjaya disimpan")
        End If

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function fProsesAP(ibln As String, ithn As String, ibank As String)
        Dim resp As New ResponseRepository
        resp.Success("Data telah disimpan")
        Dim success As Integer = 0
        Dim counter As Integer = 0
        Dim sqlComm As New SqlCommand
        Dim cmd = New SqlCommand
        Dim dt As New DataTable
        Dim problem As String = ""
        Dim dbconn As New DBKewConn

        dt = dbconn.Read("SELECT bulan,tahun FROM SMKB_Gaji_Bulan ")
        If dt.Rows.Count > 0 Then
            BulanGaji = dt.Rows(0).Item("bulan").ToString()
            TahunGaji = dt.Rows(0).Item("tahun").ToString()
        End If

        Using sqlcon = New SqlConnection(strConnx)
            sqlComm.Connection = sqlcon
            sqlComm.CommandTimeout = 600
            sqlComm.CommandText = "USP_APSource"
            sqlComm.CommandType = CommandType.StoredProcedure

            sqlComm.Parameters.Clear()

            sqlComm.Parameters.AddWithValue("@arg_bulan", BulanGaji)
            sqlComm.Parameters.AddWithValue("@arg_tahun", TahunGaji)
            sqlComm.Parameters.AddWithValue("@BankVot", ibank)


            sqlcon.Open()

            'sqlComm.ExecuteNonQuery()
            'Dim rowsAffected As Integer = sqlComm.ExecuteNonQuery()
            Dim X = sqlComm.ExecuteNonQuery()
            If X > 0 Then
                success = 1
            Else

                success = 0
            End If
        End Using

        If success = 1 Then
            resp.Success("Rekod berjaya disimpan")
        Else
            resp.Failed("Rekod tidak berjaya disimpan")
        End If

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

End Class