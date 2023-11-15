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
Public Class Transaksi_WS1
    Inherits System.Web.Services.WebService

    Dim sqlcmd As SqlCommand
    Dim sqlcon As SqlConnection
    Dim sqlread As SqlDataReader
    Dim dt As DataTable
    Private strConnx As String = "Data Source=devmis12.utem.edu.my;Initial Catalog=dbKewanganV4;Persist Security Info=True;User ID=smkb;Password=Smkb@Dev2012;"
    Dim BulanGaji As String
    Dim TahunGaji As String

    Public Function HelloWorld() As String
        Return "Hello World"
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadListMaster(ByVal nostaf As String) As String
        Dim resp As New ResponseRepository


        dt = GetListMaster(nostaf)
        'resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(dt)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadListLejar(ByVal nostaf As String, ByVal tahun As Integer, ByVal bulan As Integer) As String
        Dim resp As New ResponseRepository


        dt = GetListLejar(nostaf, tahun, bulan)
        'resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(dt)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadListStaf() As String
        Dim resp As New ResponseRepository


        dt = getListStaf()
        'resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(dt)
    End Function
    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetJenisTrans() As String
        'Dim newList As List(Of ItemList)
        'newList = listItem

        'If (q <> "") Then
        '    newList = listItem.Where(
        '        Function(x) x.value.Contains(q) Or x.text.Contains(q)
        '    ).ToList()
        'End If

        Dim tmpDT As DataTable = GetDataJenisTrans()
        Return JsonConvert.SerializeObject(tmpDT)
    End Function
    Private Function GetDataJenisTrans() As DataTable
        Dim db = New DBKewConn

        Dim query As String = $"select Jenis_Trans, Butiran from SMKB_Gaji_Jenis_Trans  order by Jenis_Trans"

        Return db.Read(query)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetKodTrans(ByVal jenis As String) As String
        'Dim newList As List(Of ItemList)
        'newList = listItem

        'If (q <> "") Then
        '    newList = listItem.Where(
        '        Function(x) x.value.Contains(q) Or x.text.Contains(q)
        '    ).ToList()
        'End If

        Dim tmpDT As DataTable = GetDataKodTrans(jenis)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function
    Private Function GetDataKodTrans(jenis As String) As DataTable
        Dim db = New DBKewConn

        Dim query As String = $"select Kod_Trans, Butiran from SMKB_Gaji_Kod_Trans where jenis_trans= '{jenis}' order by Kod_Trans"

        Return db.Read(query)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetPerkeso() As String
        'Dim newList As List(Of ItemList)
        'newList = listItem

        'If (q <> "") Then
        '    newList = listItem.Where(
        '        Function(x) x.value.Contains(q) Or x.text.Contains(q)
        '    ).ToList()
        'End If

        Dim tmpDT As DataTable = GetDataPerkeso()
        Return JsonConvert.SerializeObject(tmpDT)
    End Function
    Private Function GetDataPerkeso() As DataTable
        Dim db = New DBKewConn

        Dim query As String = $"select distinct(kod),butiran from smkb_gaji_Perkeso_hdr order by Kod"

        Return db.Read(query)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadRekodStaf(nostaf As String)
        Dim db As New DBKewConn

        Dim query As String = $"Select a.MS01_NoStaf,a.MS01_Nama,a.MS01_KpB,b.JawatanS,b.gredgajis,b.PejabatS,b.jumlahgajis,
                                a.MS01_NoPencen,a.MS01_NoKWSP,(select nama_bank from smkb_bank_master where staf_bank = a.MS01_KodBank) as bank,a.MS01_NoAkaun,a.MS01_NoCukai,
                                datediff(day,convert(datetime,a.ms01_tkhlahir,103),getdate())/365 as umur,a.MS01_Pilihan,a.MS01_TkhKhidmat,
                                case when a.ms01_status =1 then 'AKTIF' else 'TIDAK AKTIF' end status_staf,b.tarafkhidmat From [qa11].dbstaf.dbo.MS01_Peribadi a, [qa11].dbstaf.dbo.VPerjawatan1 b
                                WHERE a.MS01_NoStaf = b.nostaf and ms01_nostaf = '{nostaf}' and ms08_staterkini=1;"
        Dim dt As DataTable = db.fselectCommandDt(query)

        Return JsonConvert.SerializeObject(dt)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadBulanGaji()
        Dim db As New DBKewConn

        Dim query As String = $"select bulan,tahun from SMKB_Gaji_bulan;"
        Dim dt As DataTable = db.fselectCommandDt(query)

        Return JsonConvert.SerializeObject(dt)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadBlnThnGaji()
        Dim db As New DBKewConn


        Dim query As String = $"select bulan,tahun,cast(bulan as varchar(2)) + '/' + cast(tahun as varchar(5)) as butir from SMKB_Gaji_bulan;"
        Dim dt As DataTable = db.fSelectCommandDt(query)

        Return JsonConvert.SerializeObject(dt)
    End Function

    Private Function fInsertMaster(DataMaster As DataMaster)
        Dim db As New DBKewConn
        Dim strTkhToday As String = Now.ToString("yyyy-MM-dd")
        Dim dtTkhToday As DateTime = CDate(strTkhToday)
        Dim jumlah As Decimal
        Dim staMaster As String = ""

        If DataMaster.Sta_Trans = "AKTIF" Then
            staMaster = "A"
        ElseIf DataMaster.Sta_Trans = "BATAL" Then
            staMaster = "B"
        End If

        Dim query As String = "insert into SMKB_Gaji_Master (No_Staf, Kod_Sumber, Jenis_Trans, Kod_Trans,Tkh_Mula, Tkh_Tamat, Amaun, No_Trans, Catatan, Status, Bayar_Drpd  ) values (@nostaf,@sumber,@jenis,@kod, @tkhmula, @tkhtmt, @amaun, @noruj, @catatan, @statrans, @byr)"
        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@nostaf", DataMaster.No_Staf))
        param.Add(New SqlParameter("@sumber", DataMaster.Kod_Sumber))
        param.Add(New SqlParameter("@jenis", DataMaster.Jenis_Trans))
        param.Add(New SqlParameter("@kod", DataMaster.Kod_Trans))
        param.Add(New SqlParameter("@tkhmula", CDate(DataMaster.Tkh_Mula_Trans)))
        param.Add(New SqlParameter("@tkhtmt", CDate(DataMaster.Tkh_Tamat_Trans)))
        param.Add(New SqlParameter("@amaun", Decimal.Parse(DataMaster.AmaunTrans).ToString("N2")))
        param.Add(New SqlParameter("@noruj", DataMaster.No_Trans))
        param.Add(New SqlParameter("@catatan", DataMaster.Catatan))
        param.Add(New SqlParameter("@statrans", staMaster))
        param.Add(New SqlParameter("@byr", "P"))

        Return db.Process(query, param)
    End Function
    Private Function fDeleteMaster(DataMaster As DataMaster)
        Dim db As New DBKewConn
        Dim strTkhToday As String = Now.ToString("yyyy-MM-dd")
        Dim dtTkhToday As DateTime = CDate(strTkhToday)
        Dim jumlah As Decimal = 0
        Dim staMaster As String = ""

        If DataMaster.Sta_Trans = "AKTIF" Then
            staMaster = "A"
        ElseIf DataMaster.Sta_Trans = "BATAL" Then
            staMaster = "B"
        End If

        Dim query As String = "DELETE SMKB_Gaji_Master WHERE no_staf = @nostaf and Kod_Sumber = @sumber and Jenis_Trans = @jenis and Kod_Trans = @kod"
        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@nostaf", DataMaster.No_Staf))
        param.Add(New SqlParameter("@sumber", DataMaster.Kod_Sumber))
        param.Add(New SqlParameter("@jenis", DataMaster.Jenis_Trans))
        param.Add(New SqlParameter("@kod", DataMaster.Kod_Trans))

        Return db.Process(query, param)
    End Function
    Private Function fUpdateMaster(DataMaster As DataMaster)
        Dim db As New DBKewConn
        Dim strTkhToday As String = Now.ToString("yyyy-MM-dd")
        Dim dtTkhToday As DateTime = CDate(strTkhToday)
        Dim jumlah As Decimal = 0
        Dim staMaster As String = ""

        If DataMaster.Sta_Trans = "AKTIF" Then
            staMaster = "A"
        ElseIf DataMaster.Sta_Trans = "BATAL" Then
            staMaster = "B"
        End If

        Dim query As String = "UPDATE SMKB_Gaji_Master SET Tkh_Mula = @tkhmula, Tkh_Tamat = @tkhtmt, Amaun = @amaun, No_Trans = @noruj, Catatan = @catatan, Status = @statrans WHERE no_staf = @nostaf and Kod_Sumber = @sumber and Jenis_Trans = @jenis and Kod_Trans = @kod"
        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@tkhmula", CDate(DataMaster.Tkh_Mula_Trans)))
        param.Add(New SqlParameter("@tkhtmt", CDate(DataMaster.Tkh_Tamat_Trans)))
        param.Add(New SqlParameter("@amaun", Decimal.Parse(DataMaster.AmaunTrans).ToString("N2")))
        param.Add(New SqlParameter("@noruj", DataMaster.No_Trans))
        param.Add(New SqlParameter("@catatan", DataMaster.Catatan))
        param.Add(New SqlParameter("@statrans", staMaster))
        param.Add(New SqlParameter("@nostaf", DataMaster.No_Staf))
        param.Add(New SqlParameter("@sumber", DataMaster.Kod_Sumber))
        param.Add(New SqlParameter("@jenis", DataMaster.Jenis_Trans))
        param.Add(New SqlParameter("@kod", DataMaster.Kod_Trans))

        Return db.Process(query, param)
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function HapusMaster(DataMaster As DataMaster) As String
        'Public Function SimpanAK(No_Arahan As String, No_Surat As String, No_Staf_Peg_AK As String, Kod_PTJ As String, KW As String, Kod_Vot As String, Tkh_Mula As String Tkh_Tamat As String, Lokasi As String, PeneranganK As String,  Jen_Dok as string, File_Name as string) As String
        Dim resp As New ResponseRepository
        resp.Success("Data telah dipadam")
        Dim success As Integer = 0
        Dim JumRekod As Integer = 0
        Dim strTkhToday2 As String = Now.ToString("yyyy-MM-dd")
        Dim dtTkhToday2 As DateTime = CDate(strTkhToday2)
        Dim dbconn As New DBKewConn

        If DataMaster Is Nothing Then
            resp.Failed("Tiada data!Rekod tidak dipadam")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        'If fInsertMaster(DataMaster.No_Staf, DataMaster.Kod_Sumber, DataMaster.Jenis_Trans, DataMaster.Kod_Trans, DataMaster.Tkh_Mula_Trans, DataMaster.Tkh_Tamat_Trans, DataMaster.AmaunTrans, DataMaster.No_Trans, DataMaster.Catatan, DataMaster.Sta_Trans) <> "OK" Then
        If fDeleteMaster(DataMaster) <> "OK" Then
            resp.Failed("Gagal Menyimpan Rekod")
            Return JsonConvert.SerializeObject(resp.GetResult())
            Exit Function
        Else

            success = 1

        End If


        If success = 1 Then
            Session("NoStaf") = DataMaster.No_Staf
            resp.Success("Rekod berjaya dipadam", "00", DataMaster)
            Return JsonConvert.SerializeObject(resp.GetResult())
        Else
            resp.Failed("Rekod tidak berjaya dipadam")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        Return JsonConvert.SerializeObject(resp.GetResult())
#Disable Warning' Function doesn't return a value on all code paths
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SimpanMaster(DataMaster As DataMaster) As String
        'Public Function SimpanAK(No_Arahan As String, No_Surat As String, No_Staf_Peg_AK As String, Kod_PTJ As String, KW As String, Kod_Vot As String, Tkh_Mula As String Tkh_Tamat As String, Lokasi As String, PeneranganK As String,  Jen_Dok as string, File_Name as string) As String
        Dim resp As New ResponseRepository
        resp.Success("Data telah disimpan")
        Dim success As Integer = 0
        Dim JumRekod As Integer = 0
        Dim strTkhToday2 As String = Now.ToString("yyyy-MM-dd")
        Dim dtTkhToday2 As DateTime = CDate(strTkhToday2)
        Dim dbconn As New DBKewConn

        If DataMaster Is Nothing Then
            resp.Failed("Tiada data!Rekod tidak disimpan")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If


        Dim strSql = "select count(*) from smkb_gaji_master  where no_staf= '" & DataMaster.No_Staf & "' and Kod_trans = '" & DataMaster.Kod_Trans & "' and jenis_trans =  '" & DataMaster.Jenis_Trans & "'"
        Dim intCnt As Integer = dbconn.fSelectCount(strSql)
        If intCnt > 0 Then
            resp.Failed("Kod transaksi yang dimasukkan telah wujud bagi staf ini! Sila masukkan Kod Transaksi lain.")
            Return JsonConvert.SerializeObject(resp.GetResult())
            Exit Function
        End If


        'If fInsertMaster(DataMaster.No_Staf, DataMaster.Kod_Sumber, DataMaster.Jenis_Trans, DataMaster.Kod_Trans, DataMaster.Tkh_Mula_Trans, DataMaster.Tkh_Tamat_Trans, DataMaster.AmaunTrans, DataMaster.No_Trans, DataMaster.Catatan, DataMaster.Sta_Trans) <> "OK" Then
        If fInsertMaster(DataMaster) <> "OK" Then
            resp.Failed("Gagal Menyimpan Rekod")
            Return JsonConvert.SerializeObject(resp.GetResult())
            Exit Function
        Else

            success = 1

        End If


        If success = 1 Then
            Session("NoStaf") = DataMaster.No_Staf
            resp.Success("Rekod berjaya disimpan", "00", DataMaster)
            Return JsonConvert.SerializeObject(resp.GetResult())
        Else
            resp.Failed("Rekod tidak berjaya disimpan")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        Return JsonConvert.SerializeObject(resp.GetResult())
#Disable Warning' Function doesn't return a value on all code paths
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function UpdateMaster(DataMaster As DataMaster) As String
        'Public Function SimpanAK(No_Arahan As String, No_Surat As String, No_Staf_Peg_AK As String, Kod_PTJ As String, KW As String, Kod_Vot As String, Tkh_Mula As String Tkh_Tamat As String, Lokasi As String, PeneranganK As String,  Jen_Dok as string, File_Name as string) As String
        Dim resp As New ResponseRepository
        resp.Success("Data telah disimpan")
        Dim success As Integer = 0
        Dim JumRekod As Integer = 0
        Dim strTkhToday2 As String = Now.ToString("yyyy-MM-dd")
        Dim dtTkhToday2 As DateTime = CDate(strTkhToday2)
        Dim dbconn As New DBKewConn

        If DataMaster Is Nothing Then
            resp.Failed("Tiada data!Rekod tidak disimpan")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        'Dim strSql = "select count(*) from smkb_gaji_master  where no_staf= '" & DataMaster.No_Staf & "' and Kod_trans = '" & DataMaster.Kod_Trans & "' and jenis_trans =  '" & DataMaster.Jenis_Trans & "'"
        'Dim intCnt As Integer = dbconn.fSelectCount(strSql)
        'If intCnt > 0 Then
        '    resp.Failed("Kod transaksi yang dimasukkan telah wujud bagi staf ini! Sila masukkan Kod Transaksi lain.")
        '    Return JsonConvert.SerializeObject(resp.GetResult())
        '    Exit Function
        'End If

        'If fInsertMaster(DataMaster.No_Staf, DataMaster.Kod_Sumber, DataMaster.Jenis_Trans, DataMaster.Kod_Trans, DataMaster.Tkh_Mula_Trans, DataMaster.Tkh_Tamat_Trans, DataMaster.AmaunTrans, DataMaster.No_Trans, DataMaster.Catatan, DataMaster.Sta_Trans) <> "OK" Then
        If fUpdateMaster(DataMaster) <> "OK" Then
            resp.Failed("Gagal Menyimpan Rekod")
            Return JsonConvert.SerializeObject(resp.GetResult())
            Exit Function
        Else

            success = 1

        End If


        If success = 1 Then
            Session("NoStaf") = DataMaster.No_Staf
            resp.Success("Rekod berjaya disimpan", "00", DataMaster)
            Return JsonConvert.SerializeObject(resp.GetResult())
        Else
            resp.Failed("Rekod tidak berjaya disimpan")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        Return JsonConvert.SerializeObject(resp.GetResult())
#Disable Warning' Function doesn't return a value on all code paths
    End Function

    Private Function GetListMaster(nostaf As String) As DataTable
        Dim db = New DBKewConn

        Dim query As String = $"select Kod_Sumber, Jenis_Trans, Kod_Trans,CONVERT(VARCHAR,ISNULL(Tkh_Mula,GETDATE()),103) Tkh_Mula,
                            CONVERT(VARCHAR,ISNULL(Tkh_Tamat,GETDATE()),103) Tkh_Tamat , Amaun, isnull(No_Trans,'') as no_trans,isnull(catatan,'') as catatan, 
                            case when status='A' then 'AKTIF' when status='B' then 'BATAL' else '-' end as status,no_staf from SMKB_Gaji_Master where no_staf = '{nostaf}' order by (CASE WHEN Jenis_Trans = 'G' THEN 0 WHEN Jenis_Trans = 'E' THEN 1 ELSE 2 END),Kod_Trans"


        Return db.Read(query)
    End Function
    Private Function GetListLejar(nostaf As String, thn As Integer, bln As Integer) As DataTable
        Dim db = New DBKewConn

        Dim query As String = $"select a.Kod_Sumber + '|' + c.Butiran Kod_Sumber, a.Jenis_Trans + '|' + d.Butiran Jenis_Trans, a.Kod_Trans Kod_Trans,b.butiran Butiran,
                           a.Amaun Amaun, a.no_staf no_staf,a.kod_ptj kod_ptj from SMKB_Gaji_Lejar a,smkb_gaji_kod_trans b,SMKB_Gaji_Sumber c,SMKB_Gaji_Jenis_Trans d where
                            a.Kod_Sumber = c.Kod_Sumber and b.Jenis_Trans = d.Jenis_Trans and a.kod_trans = b.kod_trans and a.no_staf = '{nostaf}' and a.bulan = {bln} and a.tahun = {thn} 
                           order by (CASE WHEN a.Jenis_Trans = 'G' THEN 0 WHEN a.Jenis_Trans = 'E' THEN 1 ELSE 2 END),a.Kod_Trans"


        Return db.Read(query)
    End Function
    Public Function getListStaf() As DataTable
        Dim db = New DBSMConn

        'Dim query As String = "Select a.MS01_NoStaf as MS01_NoStaf ,a.MS01_Nama as MS01_Nama ,a.MS01_KpB,b.JawatanS,b.gredgajis,b.PejabatS as PejabatS ,b.jumlahgajis From MS01_Peribadi a, VPerjawatan1 b WHERE a.MS01_NoStaf = b.nostaf and b.ms08_staterkini=1"
        Dim query As String = "Select a.MS01_NoStaf as MS01_NoStaf ,a.MS01_Nama as MS01_Nama, b.PejabatS as PejabatS From MS01_Peribadi a, VPerjawatan1 b WHERE a.MS01_NoStaf = b.nostaf and b.ms08_staterkini=1 and a.MS01_Status=1"

        Return db.Read(query)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function fProsesKWSP(kodparam As String)
        Dim resp As New ResponseRepository
        resp.Success("Data telah disimpan")
        Dim success As Integer = 0
        Dim counter As Integer = 0
        Dim sqlComm As New SqlCommand
        Dim cmd = New SqlCommand
        Dim dt As New DataTable
        Dim problem As String = ""
        Dim kodstatus As String = "06"
        Dim db As New DBKewConn

        dt = db.Read("SELECT bulan,tahun FROM SMKB_Gaji_Bulan ")
        If dt.Rows.Count > 0 Then
            BulanGaji = dt.Rows(0).Item("bulan").ToString()
            TahunGaji = dt.Rows(0).Item("tahun").ToString()
        End If

        Using sqlcon = New SqlConnection(strConnx)
            sqlComm.Connection = sqlcon
            sqlComm.CommandTimeout = 600
            sqlComm.CommandText = "USP_PROSESKWSP"
            sqlComm.CommandType = CommandType.StoredProcedure

            sqlComm.Parameters.Clear()

            sqlComm.Parameters.AddWithValue("@iBulan", BulanGaji)
            sqlComm.Parameters.AddWithValue("@iTahun", TahunGaji)

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
    Public Function fProsesPerkeso()
        Dim resp As New ResponseRepository
        resp.Success("Data telah disimpan")
        Dim success As Integer = 0
        Dim counter As Integer = 0
        Dim sqlComm As New SqlCommand
        Dim cmd = New SqlCommand
        Dim dt As New DataTable
        Dim problem As String = ""
        Dim db As New DBKewConn

        dt = db.Read("SELECT bulan,tahun FROM SMKB_Gaji_Bulan ")
        If dt.Rows.Count > 0 Then
            BulanGaji = dt.Rows(0).Item("bulan").ToString()
            TahunGaji = dt.Rows(0).Item("tahun").ToString()
        End If

        Using sqlcon = New SqlConnection(strConnx)
            sqlComm.Connection = sqlcon
            sqlComm.CommandTimeout = 600
            sqlComm.CommandText = "USP_PROSES_SOCSO"
            sqlComm.CommandType = CommandType.StoredProcedure

            sqlComm.Parameters.Clear()

            sqlComm.Parameters.AddWithValue("@iBulan", BulanGaji)
            sqlComm.Parameters.AddWithValue("@iTahun", TahunGaji)

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
    Public Function fProsesCukai()
        Dim resp As New ResponseRepository
        resp.Success("Data telah disimpan")
        Dim success As Integer = 0
        Dim counter As Integer = 0
        Dim sqlComm As New SqlCommand
        Dim cmd = New SqlCommand
        Dim dt As New DataTable
        Dim problem As String = ""
        Dim db As New DBKewConn

        dt = db.Read("SELECT bulan,tahun FROM SMKB_Gaji_Bulan ")
        If dt.Rows.Count > 0 Then
            BulanGaji = dt.Rows(0).Item("bulan").ToString()
            TahunGaji = dt.Rows(0).Item("tahun").ToString()
        End If

        Using sqlcon = New SqlConnection(strConnx)
            sqlComm.Connection = sqlcon
            sqlComm.CommandTimeout = 60
            sqlComm.CommandText = "USP_PROSES_CUKAI"
            sqlComm.CommandType = CommandType.StoredProcedure

            sqlComm.Parameters.Clear()

            sqlComm.Parameters.AddWithValue("@iBulan", BulanGaji)
            sqlComm.Parameters.AddWithValue("@iTahun", TahunGaji)

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
    Public Function fProsesPencen()
        Dim resp As New ResponseRepository
        resp.Success("Data telah disimpan")
        Dim success As Integer = 0
        Dim counter As Integer = 0
        Dim sqlComm As New SqlCommand
        Dim cmd = New SqlCommand
        Dim dt As New DataTable
        Dim problem As String = ""
        Dim db As New DBKewConn

        dt = db.Read("SELECT bulan,tahun FROM SMKB_Gaji_Bulan ")
        If dt.Rows.Count > 0 Then
            BulanGaji = dt.Rows(0).Item("bulan").ToString()
            TahunGaji = dt.Rows(0).Item("tahun").ToString()
        End If

        Using sqlcon = New SqlConnection(strConnx)
            sqlComm.Connection = sqlcon
            sqlComm.CommandTimeout = 60
            sqlComm.CommandText = "USP_PROSESPENCEN"
            sqlComm.CommandType = CommandType.StoredProcedure

            sqlComm.Parameters.Clear()

            sqlComm.Parameters.AddWithValue("@iBulan", BulanGaji)
            sqlComm.Parameters.AddWithValue("@iTahun", TahunGaji)

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
    Public Function fProsesStatutory()
        Dim resp As New ResponseRepository
        resp.Success("Data telah disimpan")
        Dim success As Integer = 0
        Dim counter As Integer = 0
        Dim sqlComm As New SqlCommand
        Dim cmd = New SqlCommand
        Dim dt As New DataTable
        Dim problem As String = ""

        Dim db As New DBKewConn

        dt = db.Read("SELECT bulan,tahun FROM SMKB_Gaji_Bulan ")
        If dt.Rows.Count > 0 Then
            BulanGaji = dt.Rows(0).Item("bulan").ToString()
            TahunGaji = dt.Rows(0).Item("tahun").ToString()
        End If

        Using sqlcon = New SqlConnection(strConnx)
            sqlComm.Connection = sqlcon
            sqlComm.CommandTimeout = 1500
            sqlComm.CommandText = "USP_PROSES_STATUTORY"
            sqlComm.CommandType = CommandType.StoredProcedure

            sqlComm.Parameters.Clear()

            sqlComm.Parameters.AddWithValue("@iBulan", BulanGaji)
            sqlComm.Parameters.AddWithValue("@iTahun", TahunGaji)

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

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SimpanStaf(DataStaf As DataStaf) As String
        'Public Function SimpanAK(No_Arahan As String, No_Surat As String, No_Staf_Peg_AK As String, Kod_PTJ As String, KW As String, Kod_Vot As String, Tkh_Mula As String Tkh_Tamat As String, Lokasi As String, PeneranganK As String,  Jen_Dok as string, File_Name as string) As String
        Dim resp As New ResponseRepository
        resp.Success("Data telah disimpan")
        Dim success As Integer = 0
        Dim JumRekod As Integer = 0
        Dim strTkhToday2 As String = Now.ToString("yyyy-MM-dd")
        Dim dtTkhToday2 As DateTime = CDate(strTkhToday2)
        Dim dbconn As New DBKewConn

        If DataStaf Is Nothing Then
            resp.Failed("Tiada data!Rekod tidak disimpan")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        'If fInsertMaster(DataMaster.No_Staf, DataMaster.Kod_Sumber, DataMaster.Jenis_Trans, DataMaster.Kod_Trans, DataMaster.Tkh_Mula_Trans, DataMaster.Tkh_Tamat_Trans, DataMaster.AmaunTrans, DataMaster.No_Trans, DataMaster.Catatan, DataMaster.Sta_Trans) <> "OK" Then
        Dim strSql = "select count(*) from smkb_gaji_staf  where no_staf= '" & DataStaf.No_Staf & "'"
        Dim intCnt As Integer = dbconn.fSelectCount(strSql)
        If intCnt > 0 Then
            If fUpDateStaf(DataStaf) <> "OK" Then
                resp.Failed("Gagal Menyimpan Rekod")
                Return JsonConvert.SerializeObject(resp.GetResult())
                Exit Function
            Else

                success = 2

            End If

        Else
            If fInsertStaf(DataStaf) <> "OK" Then
                resp.Failed("Gagal Menyimpan Rekod")
                Return JsonConvert.SerializeObject(resp.GetResult())
                Exit Function
            Else

                success = 1

            End If
        End If

        If success = 1 Then
            Session("NoStaf") = DataStaf.No_Staf
            resp.Success("Rekod berjaya disimpan", "00", DataStaf)
            Return JsonConvert.SerializeObject(resp.GetResult())
        ElseIf success = 2 Then
            Session("NoStaf") = DataStaf.No_Staf
            resp.Success("Rekod berjaya dikemaskini", "00", DataStaf)
            Return JsonConvert.SerializeObject(resp.GetResult())
        Else
            resp.Failed("Rekod tidak berjaya disimpan")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        Return JsonConvert.SerializeObject(resp.GetResult())
#Disable Warning' Function doesn't return a value on all code paths
    End Function

    Private Function fInsertStaf(DataStaf As DataStaf)
        Dim db As New DBKewConn
        Dim strTkhToday As String = Now.ToString("yyyy-MM-dd")
        Dim dtTkhToday As DateTime = CDate(strTkhToday)
        Dim jumlah As Decimal
        'Dim staMaster As String = ""

        'If DataStaf.Sta_Trans = "AKTIF" Then
        '    staMaster = "A"
        'ElseIf DataStaf.Sta_Trans = "BATAL" Then
        '    staMaster = "B"
        'End If

        Dim query As String = "insert into SMKB_Gaji_Staf (No_Staf, No_Perkeso, Kategori_Perkeso,Proses_Gaji,Proses_Kwsp,Proses_Perkeso,Proses_Cukai,Proses_Pencen,Proses_Bonus,Tahan_Gaji,Bayar_Cek) 
                                values (@nostaf,@nosocso,@katsocso,@gaji, @kwsp, @socso, @cukai, @pencen, @bonus, @tahan, @cek)"
        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@nostaf", DataStaf.No_Staf))
        param.Add(New SqlParameter("@nosocso", DataStaf.Kat_Perkeso))
        param.Add(New SqlParameter("@katsocso", DataStaf.No_perkeso))
        param.Add(New SqlParameter("@gaji", DataStaf.Proses_Gaji))
        param.Add(New SqlParameter("@kwsp", DataStaf.Proses_Kwsp))
        param.Add(New SqlParameter("@socso", DataStaf.Proses_Perkeso))
        param.Add(New SqlParameter("@cukai", DataStaf.Proses_Cukai))
        param.Add(New SqlParameter("@pencen", DataStaf.Proses_Pencen))
        param.Add(New SqlParameter("@bonus", DataStaf.Proses_Bonus))
        param.Add(New SqlParameter("@tahan", DataStaf.Tahan_Gaji))
        param.Add(New SqlParameter("@cek", DataStaf.Bayar_Cek))

        Return db.Process(query, param)
    End Function

    Private Function fUpdateStaf(DataStaf As DataStaf)
        Dim db As New DBKewConn
        Dim strTkhToday As String = Now.ToString("yyyy-MM-dd")
        Dim dtTkhToday As DateTime = CDate(strTkhToday)
        Dim jumlah As Decimal
        'Dim staMaster As String = ""

        'If DataStaf.Sta_Trans = "AKTIF" Then
        '    staMaster = "A"
        'ElseIf DataStaf.Sta_Trans = "BATAL" Then
        '    staMaster = "B"
        'End If

        Dim query As String = "UPDATE SMKB_Gaji_Staf SET No_Perkeso = @nosocso, Kategori_Perkeso = @katsocso, Proses_Gaji = @gaji, Proses_Kwsp = @kwsp, Proses_Perkeso = @socso, Proses_Cukai = @cukai,Proses_Pencen = @pencen ,Proses_Bonus = @bonus,Tahan_Gaji = @tahan, Bayar_Cek = @cek WHERE no_staf = @nostaf"
        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@nosocso", DataStaf.Kat_Perkeso))
        param.Add(New SqlParameter("@katsocso", DataStaf.No_perkeso))
        param.Add(New SqlParameter("@gaji", DataStaf.Proses_Gaji))
        param.Add(New SqlParameter("@kwsp", DataStaf.Proses_Kwsp))
        param.Add(New SqlParameter("@socso", DataStaf.Proses_Perkeso))
        param.Add(New SqlParameter("@cukai", DataStaf.Proses_Cukai))
        param.Add(New SqlParameter("@pencen", DataStaf.Proses_Pencen))
        param.Add(New SqlParameter("@bonus", DataStaf.Proses_Bonus))
        param.Add(New SqlParameter("@tahan", DataStaf.Tahan_Gaji))
        param.Add(New SqlParameter("@cek", DataStaf.Bayar_Cek))
        param.Add(New SqlParameter("@nostaf", DataStaf.No_Staf))

        Return db.Process(query, param)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadProsesStaf(nostaf As String)
        Dim db As New DBKewConn

        Dim query As String = $"SELECT No_Staf, No_Perkeso, Kategori_Perkeso,Kategori_Cukai, Proses_Gaji,Proses_Kwsp,Proses_Perkeso,Proses_Cukai,Proses_Pencen,Proses_Bonus,Tahan_Gaji,Bayar_Cek From SMKB_Gaji_Staf WHERE no_staf = '{nostaf}';"
        Dim dt As DataTable = db.fselectCommandDt(query)

        Return JsonConvert.SerializeObject(dt)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadListIncome(ByVal nostaf As String, ByVal tahun As Integer, ByVal bulan As Integer) As String
        Dim resp As New ResponseRepository


        dt = GetListIncome(nostaf, tahun, bulan)

        Return JsonConvert.SerializeObject(dt)
    End Function

    Private Function GetListIncome(nostaf As String, thn As Integer, bln As Integer) As DataTable
        Dim db = New DBKewConn

        Dim query As String = $"Select SMKB_Gaji_Lejar.No_Staf,SMKB_Gaji_Lejar.Kod_Trans,SMKB_Gaji_Kod_Trans.Butiran,sum(SMKB_Gaji_Lejar.Amaun) As Amaun 
        From SMKB_Gaji_Lejar, SMKB_Gaji_Kod_Trans
        Where SMKB_Gaji_Lejar.Kod_Trans = SMKB_Gaji_Kod_Trans.Kod_Trans And SMKB_Gaji_Lejar.Jenis_Trans In ('H','B','G','E','O') 
        And SMKB_Gaji_Lejar.Bulan={bln} And SMKB_Gaji_Lejar.Tahun={thn} And SMKB_Gaji_Lejar.No_Staf='{nostaf}' GROUP BY SMKB_Gaji_Lejar.No_Staf,   
        SMKB_Gaji_Lejar.Kod_Trans, SMKB_Gaji_Kod_Trans.Butiran, SMKB_Gaji_Lejar.Jenis_Trans
        order by(CASE WHEN SMKB_Gaji_Lejar.Jenis_Trans = 'G' THEN 0 WHEN SMKB_Gaji_Lejar.Jenis_Trans = 'E' THEN 1 ELSE 2 END)"


        Return db.Read(query)
    End Function
    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadListPotonganSlip(ByVal nostaf As String, ByVal tahun As Integer, ByVal bulan As Integer) As String
        Dim resp As New ResponseRepository


        dt = GetListPotonganSlip(nostaf, tahun, bulan)

        Return JsonConvert.SerializeObject(dt)
    End Function
    Private Function GetListPotonganSlip(nostaf As String, thn As Integer, bln As Integer) As DataTable
        Dim db = New DBKewConn

        Dim query As String = $"SELECT SMKB_Gaji_Lejar.No_Staf,   
        SMKB_Gaji_Lejar.Kod_Trans,
        SMKB_Gaji_Kod_Trans.Butiran,   
        sum(SMKB_Gaji_Lejar.Amaun) as Amaun
        FROM   SMKB_Gaji_Lejar, SMKB_Gaji_Kod_Trans
        WHERE  SMKB_Gaji_Lejar.Kod_Trans = SMKB_Gaji_Kod_Trans.Kod_Trans AND SMKB_Gaji_Lejar.Jenis_Trans in ('P','C','K','N','T','S') and SMKB_Gaji_Lejar.Bayar_Drpd='P'
        AND SMKB_Gaji_Lejar.Bulan={bln} AND SMKB_Gaji_Lejar.Tahun={thn} and SMKB_Gaji_Lejar.No_Staf='{nostaf}'
        GROUP BY SMKB_Gaji_Lejar.No_Staf,   
        SMKB_Gaji_Lejar.Kod_Trans,SMKB_Gaji_Kod_Trans.Butiran,SMKB_Gaji_Lejar.Jenis_Trans
        order by (CASE WHEN SMKB_Gaji_Lejar.Jenis_Trans = 'K' THEN 0 WHEN SMKB_Gaji_Lejar.Jenis_Trans = 'N' THEN 1  WHEN SMKB_Gaji_Lejar.Jenis_Trans = 'S' THEN 2 WHEN SMKB_Gaji_Lejar.Jenis_Trans = 'T' THEN 3  ELSE 4 END)"
        Return db.Read(query)
    End Function
    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadListPotongan(ByVal nostaf As String, ByVal tahun As Integer, ByVal bulan As Integer) As String
        Dim resp As New ResponseRepository


        dt = GetListPotongan(nostaf, tahun, bulan)

        Return JsonConvert.SerializeObject(dt)
    End Function

    Private Function GetListPotongan(nostaf As String, thn As Integer, bln As Integer) As DataTable
        Dim db = New DBKewConn

        Dim query As String = $"SELECT SMKB_Gaji_Lejar.No_Staf,   
        SMKB_Gaji_Lejar.Kod_Trans,
        SMKB_Gaji_Kod_Trans.Butiran,   
        sum(SMKB_Gaji_Lejar.Amaun) as Amaun
        FROM   SMKB_Gaji_Lejar, SMKB_Gaji_Kod_Trans
        WHERE  SMKB_Gaji_Lejar.Kod_Trans = SMKB_Gaji_Kod_Trans.Kod_Trans AND SMKB_Gaji_Lejar.Jenis_Trans in ('P','C','K','N','T','S') 
        AND SMKB_Gaji_Lejar.Bulan={bln} AND SMKB_Gaji_Lejar.Tahun={thn} and SMKB_Gaji_Lejar.No_Staf='{nostaf}'
        GROUP BY SMKB_Gaji_Lejar.No_Staf,   
        SMKB_Gaji_Lejar.Kod_Trans,SMKB_Gaji_Kod_Trans.Butiran,SMKB_Gaji_Lejar.Jenis_Trans
        order by (CASE WHEN SMKB_Gaji_Lejar.Jenis_Trans = 'K' THEN 0 WHEN SMKB_Gaji_Lejar.Jenis_Trans = 'N' THEN 1  WHEN SMKB_Gaji_Lejar.Jenis_Trans = 'S' THEN 2 WHEN SMKB_Gaji_Lejar.Jenis_Trans = 'T' THEN 3  ELSE 4 END)"
        Return db.Read(query)
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SahStatutory(kodparam As String) As String
        Dim resp As New ResponseRepository
        resp.Success("Data telah disimpan")
        Dim success As Integer = 0
        Dim JumRekod As Integer = 0

        Dim dbconn As New DBKewConn
        Dim kodstatus As String = "02"

        Dim strSql = "select count(*) from smkb_gaji_param  where kod_param =  '" & kodparam & "' and status in ('06','07','08','09')"
        Dim intCnt As Integer = dbconn.fSelectCount(strSql)
        If intCnt < 4 Then
            resp.Failed("Proses Statutory tidak lengkap! Sila Proses Keseluruhan Statutory.")
            Return JsonConvert.SerializeObject(resp.GetResult())
            Exit Function
        End If

        fDeleteParam(kodparam, kodstatus)

        If fInsertParam(kodparam, kodstatus) <> "OK" Then
            resp.Failed("Gagal Menyimpan Rekod")
            Return JsonConvert.SerializeObject(resp.GetResult())
            Exit Function
        Else

            success = 1

        End If


        If success = 1 Then

            resp.Success("Rekod berjaya disimpan", "00", kodparam)
            Return JsonConvert.SerializeObject(resp.GetResult())
        Else
            resp.Failed("Rekod tidak berjaya disimpan")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        Return JsonConvert.SerializeObject(resp.GetResult())
#Disable Warning' Function doesn't return a value on all code paths
    End Function

    Private Function fInsertParam(kodparam As String, kodstatus As String)
        Dim db As New DBKewConn
        Dim strTkhToday As String = Now.ToString("yyyy-MM-dd")
        Dim dtTkhToday As DateTime = CDate(strTkhToday)


        Dim query As String = "insert into SMKB_Gaji_Param (kod_param, status, Tarikh ) values (@kod,@staparam,@tarikh)"
        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@kod", kodparam))
        param.Add(New SqlParameter("@staparam", kodstatus))
        param.Add(New SqlParameter("@tarikh", dtTkhToday))


        Return db.Process(query, param)
    End Function

    Private Function fDeleteParam(kodparam As String, kodstatus As String)
        Dim db As New DBKewConn
        Dim strTkhToday As String = Now.ToString("yyyy-MM-dd")
        Dim dtTkhToday As DateTime = CDate(strTkhToday)

        Dim query As String = "DELETE SMKB_Gaji_Param where kod_param= @kod and status=@staparam"
        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@kod", kodparam))
        param.Add(New SqlParameter("@staparam", kodstatus))

        Return db.Process(query, param)
    End Function
End Class