Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports Newtonsoft.Json
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
'Imports System.Web.Http
Imports System.Data.SqlClient
Imports System

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<System.Web.Script.Services.ScriptService()>
<ToolboxItem(False)>
Public Class PenghutangWS
    Inherits System.Web.Services.WebService

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetKategoriPenghutangList(ByVal q As String) As String
        Dim tmpDT As DataTable = GetKategoriPenghutang(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function
    Private Function GetKategoriPenghutang(kod As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT Kod_Detail as kodDetail, Butiran as butiran, Status as status FROM SMKB_Lookup_Detail WHERE Kod='0152'"
        Dim param As New List(Of SqlParameter)
        ' If (kod <> "") Then
        '     query = query & " WHERE Nama_Penghutang LIKE '%' + @kod + '%'"
        '     param.Add(New SqlParameter("@kod", kod))
        ' End If
        Return db.Read(query, param)
    End Function

    'DAFTAR PENGHUTANG
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetKodPenghutangList(ByVal q As String) As String
        Dim tmpDT As DataTable = GetKodPenghutang(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetKodPenghutang(kod As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT Kod_Detail as value, Butiran as text FROM SMKB_Lookup_Detail WHERE Kod='0152' AND status=1"
        Return db.Read(query)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetPenghutangList(ByVal q As String) As String
        Dim resp As New ResponseRepository
        Dim tmpDT As DataTable = GetPenghutang(q)
        resp.SuccessPayload(tmpDT)
        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function
    Private Function GetPenghutang(kod As String) As DataTable
        Dim db = New DBKewConn
        'Dim query As String = "SELECT Nama_Penghutang as nama, Kategori_Penghutang as kategoriPenghutang, Tel_Bimbit as telBimbit, Emel as emel, ID_Penghutang as idPenghutang, Kod_Penghutang as id, Status as status FROM SMKB_Penghutang_Master"
        'Dim query As String = "SELECT Nama_Penghutang as nama, Kategori_Penghutang as kategoriPenghutang, Tel_Bimbit as telBimbit, Emel as emel, ID_Penghutang as idPenghutang, Kod_Penghutang as id, No_Akaun as noAkaun, Kod_Bank as kodBank FROM SMKB_Penghutang_Master ORDER BY id"
        Dim query As String = "SELECT a.Nama_Penghutang as nama, a.Kategori_Penghutang as kategoriPenghutang, a.Tel_Bimbit as telBimbit, a.Emel as emel, a.No_Rujukan as noRujukan, a.Kod_Penghutang as id, a.No_Akaun as noAkaun, a.Kod_Bank as kodBank, b.Butiran as namaBank FROM SMKB_Penghutang_Master as a LEFT JOIN SMKB_Lookup_Detail as b ON a.Kod_Bank=b.Kod_Detail AND b.Kod='0097'
                                    WHERE
                                        a.Kategori_Penghutang = '" + kod + "'
                                    ORDER BY
                                        id;"
        Return db.Read(query)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetPenghutangAllDetailValue(ByVal q As String) As String
        Dim tmpDT As DataTable = GetPenghutangAllDetail(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function
    Private Function GetPenghutangAllDetail(kod As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT a.Nama_Penghutang as nama, a.Kategori_Penghutang as kategoriPenghutang, a.Tel_Bimbit as telBimbit, a.Emel as emel, a.No_Rujukan as noRujukan, a.Kod_Penghutang as id, a.Alamat_1 as alamat1, a.Alamat_2 as alamat2, a.Poskod as poskod, a.Bandar as bandar, a.Kod_Negeri as kodNegeri, a.Kod_Negara as kodNegara, a.No_Akaun as noAkaun, a.Kod_Bank as kodBank, b.Butiran as namaBank FROM SMKB_Penghutang_Master as a LEFT JOIN SMKB_Lookup_Detail as b ON a.Kod_Bank=b.Kod_Detail AND b.Kod='0097'"
        Dim param As New List(Of SqlParameter)
        If (kod <> "") Then
            query = query & " WHERE a.Kod_Penghutang LIKE '%' + @kod + '%'"
            param.Add(New SqlParameter("@kod", kod))
        End If
        Return db.Read(query, param)
    End Function

    'Filter Penghutang list by Category
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadList_SenaraiPenghutang(category_filter As String, isClicked As Boolean) As String
        Dim resp As New ResponseRepository
        Dim dt As DataTable
        If isClicked = False Then
            Return JsonConvert.SerializeObject(New DataTable)
        End If
        dt = GetList_SenaraiPenghutang(category_filter)
        Return JsonConvert.SerializeObject(dt)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetList_SenaraiPenghutang(category_filter As String) As DataTable
        Dim db = New DBKewConn
        Dim param As New List(Of SqlParameter)

        Dim query As String

        If category_filter <> "" Then
            query = "SELECT a.Nama_Penghutang as nama, a.Kategori_Penghutang as kategoriPenghutang, a.Tel_Bimbit as telBimbit, a.Emel as emel, a.No_Rujukan as noRujukan, a.Kod_Penghutang as id, a.No_Akaun as noAkaun, a.Kod_Bank as kodBank, b.Butiran as namaBank FROM SMKB_Penghutang_Master as a LEFT JOIN SMKB_Lookup_Detail as b ON a.Kod_Bank=b.Kod_Detail AND b.Kod='0097'
                    WHERE
                        a.Kategori_Penghutang LIKE '%' + @kod + '%'
                    ORDER BY
                        id;"
            param.Add(New SqlParameter("@kod", category_filter))
            Return db.Read(query, param)
        Else
            query = "SELECT a.Nama_Penghutang as nama, a.Kategori_Penghutang as kategoriPenghutang, a.Tel_Bimbit as telBimbit, a.Emel as emel, a.No_Rujukan as noRujukan, a.Kod_Penghutang as id, a.No_Akaun as noAkaun, a.Kod_Bank as kodBank, b.Butiran as namaBank FROM SMKB_Penghutang_Master as a LEFT JOIN SMKB_Lookup_Detail as b ON a.Kod_Bank=b.Kod_Detail AND b.Kod='0097'
                    ORDER BY
                        id;"
            Return db.Read(query)
        End If
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetStaffList(ByVal q As String) As String
        Dim tmpDT As DataTable = GetStaff(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function
    Private Function GetStaff(kod As String) As DataTable
        Dim db = New DBSMConn
        Dim query As String = "SELECT TOP(10) MS01_NoStaf, MS01_NoTelBimbit, MS01_Nama, MS01_KpB, MS01_AlamatSurat1, MS01_AlamatSurat2, MS01_BandarSurat,MS01_PoskodSurat,MS01_NegeriSurat,MS01_NegeriSurat, MS01_NoAkaun, MS01_KodBank FROM MS01_Peribadi"
        Dim param As New List(Of SqlParameter)
        If (kod <> "") Then
            query = query & " WHERE MS01_NoStaf LIKE '%' + @kod + '%'"
            param.Add(New SqlParameter("@kod", kod))
        End If
        Return db.Read(query, param)
    End Function

    'SQL to fetch top 10 result of staf list for dropdown
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetKodStaffList(ByVal q As String) As String
        Dim tmpDT As DataTable = GetKodStaff(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function
    Private Function GetKodStaff(kod As String) As DataTable
        Dim db = New DBSMConn

        Dim query As String = "SELECT TOP(10) MS01_NoStaf as value, MS01_Nama as text FROM MS01_Peribadi"
        'Dim query As String = "SELECT TOP(10) MS01_NoStaf as value, MS01_Nama as text FROM MS01_Peribadi WHERE NOT MS01_NoStaf = '00000'" 'exclude TYT DS Khalil Yaakob
        Dim param As New List(Of SqlParameter)
        If (kod <> "") Then
            query = query & " WHERE LOWER(MS01_Nama) LIKE '%' + @kod + '%' OR MS01_NoStaf LIKE '%' + @kod + '%'"
            param.Add(New SqlParameter("@kod", kod))
        End If
        query &= " ORDER BY MS01_NoStaf"
        Return db.Read(query, param)
    End Function

    'SQL to fetch top 10 result of Pelajar UG list for dropdown
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetKodPelajarUGList(ByVal q As String) As String
        Dim tmpDT As DataTable = GetKodPelajarUG(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function
    Private Function GetKodPelajarUG(kod As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT TOP(10) SMP01_Nomatrik as value, SMP01_Nama as text FROM SMP01_Peribadi WHERE SMP01_Status='AKTIF'"
        Dim param As New List(Of SqlParameter)
        If (kod <> "") Then
            query = query & " AND (LOWER(SMP01_Nama) LIKE '%' + @kod + '%' OR LOWER(SMP01_Nomatrik) LIKE '%' + @kod + '%')"
            param.Add(New SqlParameter("@kod", kod))
        End If
        query &= " ORDER BY SMP01_Nomatrik"
        Return db.Read(query, param)
    End Function

    'SQL to fetch top 10 result of Pelajar PG list for dropdown
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetKodPelajarPGList(ByVal q As String) As String
        Dim tmpDT As DataTable = GetKodPelajarPG(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function
    Private Function GetKodPelajarPG(kod As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT TOP(10) SMG01_NAMA as text, B.SMG01_NOMATRIK as value FROM SMG01_PERIBADI_MOHON A INNER JOIN SMG01_PENGAJIAN B ON A.SMG01_ID=B.SMG02_ID"
        Dim param As New List(Of SqlParameter)
        If (kod <> "") Then
            query = query & " WHERE LOWER(SMG01_NAMA) LIKE '%' + @kod + '%' OR LOWER(B.SMG01_NOMATRIK) LIKE '%' + @kod + '%'"
            param.Add(New SqlParameter("@kod", kod))
        End If
        query &= " ORDER BY B.SMG01_NOMATRIK"
        Return db.Read(query, param)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetNegaraList(ByVal q As String) As String
        Dim tmpDT As DataTable = GetNegara(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function
    Private Function GetNegara(kod As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT Kod_Detail as value, Butiran as text FROM SMKB_Lookup_Detail WHERE Kod='0001'"
        Dim param As New List(Of SqlParameter)
        If (kod <> "") Then
            query = query & " AND LOWER(Butiran) LIKE '%' + @kod + '%'"
            param.Add(New SqlParameter("@kod", kod))
        End If
        query &= " AND Butiran LIKE '%[abcdefghijklmnopqrstuvwxyz]%' collate Latin1_General_CS_AS ORDER BY Butiran ASC"
        Return db.Read(query, param)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetNegeriList(ByVal q As String) As String
        Dim tmpDT As DataTable = GetNegeri(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function
    Private Function GetNegeri(kod As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT Kod_Detail as value, Butiran as text FROM SMKB_Lookup_Detail WHERE Kod='0002' AND Kod_Detail != 98 AND Kod_Detail != '00'"
        Dim param As New List(Of SqlParameter)
        If (kod <> "") Then
            query = query & " AND LOWER(Butiran) LIKE '%' + @kod + '%'"
            param.Add(New SqlParameter("@kod", kod))
        End If
        query &= " ORDER BY Butiran ASC"
        Return db.Read(query, param)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetPoskodList(ByVal q As String) As String
        Dim tmpDT As DataTable = GetPoskod(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function
    Private Function GetPoskod(kod As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT Kod_Detail as value, Kod_Detail as text FROM SMKB_Lookup_Detail WHERE Kod='0079'"
        Dim param As New List(Of SqlParameter)
        If (kod <> "") Then
            query = query & " AND Kod_Detail LIKE '%' + @kod + '%'"
            param.Add(New SqlParameter("@kod", kod))
        End If
        query &= " ORDER BY Kod_Detail ASC"
        Return db.Read(query, param)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetBandarList(ByVal q As String) As String
        Dim tmpDT As DataTable = GetBandar(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function
    Private Function GetBandar(kod As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT Kod_Detail as value, Butiran as text FROM SMKB_Lookup_Detail WHERE Kod='0003'"
        Dim param As New List(Of SqlParameter)
        If (kod <> "") Then
            query = query & " AND Butiran LIKE '%' + @kod + '%'"
            param.Add(New SqlParameter("@kod", kod))
        End If
        query &= " ORDER BY Butiran ASC"
        Return db.Read(query, param)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetBankList(ByVal q As String) As String
        Dim tmpDT As DataTable = GetBank(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function
    Private Function GetBank(kod As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT Kod_Detail as value, Butiran as text FROM SMKB_Lookup_Detail WHERE Kod='0097'"
        Dim param As New List(Of SqlParameter)
        If (kod <> "") Then
            query = query & " AND Butiran LIKE '%' + @kod + '%'"
            param.Add(New SqlParameter("@kod", kod))
        End If
        query &= " ORDER BY Butiran ASC"
        Return db.Read(query, param)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetKategoriValue(ByVal q As String) As String
        Dim tmpDT As DataTable = GetKategoriValueFromKod(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function
    Private Function GetKategoriValueFromKod(kod As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT Kod_Detail as value, Butiran as text FROM SMKB_Lookup_Detail WHERE Kod='0152'"
        Dim param As New List(Of SqlParameter)
        If (kod <> "") Then
            query = query & " AND Kod_Detail=@kod"
            param.Add(New SqlParameter("@kod", kod))
        End If
        Return db.Read(query, param)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetNegaraValue(ByVal q As String) As String
        Dim tmpDT As DataTable = GetNegaraValueFromKod(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function
    Private Function GetNegaraValueFromKod(kod As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT Kod_Detail as value, Butiran as text FROM SMKB_Lookup_Detail WHERE Kod='0001' AND Butiran LIKE '%[abcdefghijklmnopqrstuvwxyz]%' collate Latin1_General_CS_AS"
        Dim param As New List(Of SqlParameter)
        If (kod <> "") Then
            query = query & " AND Kod_Detail=@kod "
            param.Add(New SqlParameter("@kod", kod))
        End If
        Return db.Read(query, param)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetNegeriValue(ByVal q As String) As String
        Dim tmpDT As DataTable = GetNegeriValueFromKod(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function
    Private Function GetNegeriValueFromKod(kod As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT Kod_Detail as value, Butiran as text FROM SMKB_Lookup_Detail WHERE Kod='0002'"
        Dim param As New List(Of SqlParameter)
        If (kod <> "") Then
            query = query & " AND Kod_Detail=@kod"
            param.Add(New SqlParameter("@kod", kod))
        End If
        Return db.Read(query, param)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetPoskodValue(ByVal q As String) As String
        Dim tmpDT As DataTable = GetPoskodValueFromKod(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function
    Private Function GetPoskodValueFromKod(kod As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT TOP (500) Kod_Detail as value, Kod_Detail as text FROM SMKB_Lookup_Detail WHERE Kod='0079'"
        Dim param As New List(Of SqlParameter)
        If (kod <> "") Then
            query = query & " AND Kod_Detail=@kod"
            param.Add(New SqlParameter("@kod", kod))
        End If
        query = query & " ORDER BY Kod_Detail ASC"
        Return db.Read(query, param)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetBandarValue(ByVal q As String) As String
        Dim tmpDT As DataTable = GetBandarValueFromKod(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function
    Private Function GetBandarValueFromKod(kod As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT TOP (500) Kod_Detail as value, Butiran as text FROM SMKB_Lookup_Detail WHERE Kod='0003'"
        Dim param As New List(Of SqlParameter)
        If (kod <> "") Then
            query = query & " AND Kod_Detail=@kod"
            param.Add(New SqlParameter("@kod", kod))
        End If
        Return db.Read(query, param)
    End Function

    'Get bandar value by name to map from other db to dbSMKB
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetBandarValueMap(ByVal q As String) As String
        Dim tmpDT As DataTable = GetBandarValueByName(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function
    Private Function GetBandarValueByName(kod As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT Kod_Detail as value, Butiran as text FROM SMKB_Lookup_Detail WHERE Kod='0003'"
        Dim param As New List(Of SqlParameter)
        If (kod <> "") Then
            query = query & " AND Butiran=@kod"
            param.Add(New SqlParameter("@kod", kod))
        End If
        Return db.Read(query, param)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetBankValue(ByVal q As String) As String
        Dim tmpDT As DataTable = GetBankValueFromKod(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function
    Private Function GetBankValueFromKod(kod As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT TOP (500) Kod_Detail as value, Butiran as text FROM SMKB_Lookup_Detail WHERE Kod='0097'"
        Dim param As New List(Of SqlParameter)
        If (kod <> "") Then
            query = query & " AND Kod_Detail=@kod"
            param.Add(New SqlParameter("@kod", kod))
        End If
        Return db.Read(query, param)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetPenghutangListDropdown(ByVal q As String) As String
        Dim tmpDT As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT Kod_Penghutang as id FROM SMKB_Penghutang_Master"
        Dim param As New List(Of SqlParameter)
        If (q <> "") Then
            query = query & " WHERE Kod_Penghutang LIKE '%' + @id + '%'"
            param.Add(New SqlParameter("@id", q))
        End If
        tmpDT = db.Read(query, param)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    'SQL to get all details of Staf based on NoStaff
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetStafValue(ByVal q As String) As String
        Dim tmpDT As DataTable = GetStafValueFromKod(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function
    Private Function GetStafValueFromKod(kod As String) As DataTable
        Dim db = New DBSMConn
        Dim query As String = "SELECT a.MS01_NoStaf, a.MS01_NoTelBimbit, a.MS01_Nama, a.MS01_KpB, a.MS01_Email, a.MS01_AlamatSurat1, a.MS01_AlamatSurat2, a.MS01_BandarSurat, a.MS01_PoskodSurat, a.MS01_NegeriSurat, a.MS01_NegeriSurat, a.MS01_NoAkaun, a.MS01_KodBank, b.NamaBank FROM MS01_Peribadi AS a JOIN MS_Bank AS b ON a.MS01_KodBank = b.KodBank"
        Dim param As New List(Of SqlParameter)
        If (kod <> "") Then
            query = query & " WHERE MS01_NoStaf LIKE '%' + @kod + '%'"
            param.Add(New SqlParameter("@kod", kod))
        End If
        Return db.Read(query, param)
    End Function

    'SQL to get all details of Pelajar UG based on NoMatrik
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetPelajarUGValue(ByVal q As String) As String
        Dim tmpDT As DataTable = GetPelajarUGValueFromKod(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function
    Private Function GetPelajarUGValueFromKod(kod As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT SMP01_Nama, B.Fakulti AS SMP01_Alamat1, 'JALAN HANG TUAH JAYA' AS SMP01_Alamat2, '76100' AS SMP01_Poskod, '04' AS SMP01_Negeri, 'MY' AS SMP01_Negara, SMP01_NoTelBimBit, SMP01_Nomatrik+'@student.utem.edu.my' AS SMP01_Emel, SMP01_Nomatrik, '040305' AS SMP01_Bandar, SMP01_NoAkaun, SMP01_Bank
                                FROM SMP01_Peribadi A 
                                JOIN SMG_FAKULTI B ON A.SMP01_Fakulti=B.Kod_Fakulti
                                WHERE SMP01_Status='AKTIF'"
        Dim param As New List(Of SqlParameter)
        If (kod <> "") Then
            query = query & " AND SMP01_Nomatrik LIKE '%' + @kod + '%'"
            param.Add(New SqlParameter("@kod", kod))
        End If
        Return db.Read(query, param)
    End Function

    'SQL to get all details of Pelajar PG based on NoMatrik
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetPelajarPGValue(ByVal q As String) As String
        Dim tmpDT As DataTable = GetPelajarPGValueFromKod(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function
    Private Function GetPelajarPGValueFromKod(kod As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT SMG02_NAMA, 'PG' AS SMG01_KATEGORI, SMG02_NoAkaun, C.Fakulti AS SMG01_Alamat1, 'JALAN HANG TUAH JAYA' AS SMG01_Alamat2,'76100' AS SMG01_Poskod, '04' AS SMG01_Negeri, 'MY' AS SMG01_Negara, SMG02_NOTEL, B.SMG01_NOMATRIK+'@student.utem.edu.my' AS SMP01_Emel,B.SMG01_NOMATRIK, '02' AS KATEGORI_ID,'1' AS STATUS, '040305' AS SMG01_Bandar,'' AS KODBANK
            FROM SMG01_PENGAJIAN A, SMG02_Peribadi B, SMG_FAKULTI C
            WHERE SMG01_STATUS = 'AKTIF' AND A.SMG01_NOMATRIK=B.SMG01_NOMATRIK 
            AND C.Status='1' AND A.SMG01_KODFAKULTI=C.Kod_Fakulti"
        Dim param As New List(Of SqlParameter)
        If (kod <> "") Then
            query = query & " AND B.SMG01_NOMATRIK LIKE '%' + @kod + '%'"
            param.Add(New SqlParameter("@kod", kod))
        End If
        Return db.Read(query, param)
    End Function

    'SQL to get all details of Pelajar PH based on NoMatrik
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetPelajarPHValue(ByVal q As String) As String
        Dim tmpDT As DataTable = GetPelajarPHValueFromKod(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function
    Private Function GetPelajarPHValueFromKod(kod As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT SMP01_Nama, SMP01_NoTelBimBit, SMP01_Nomatrik+'@student.utem.edu.my' AS SMP01_Emel, SMP01_Nomatrik, SMP01_KP, SMP01_Alamat1, SMP01_Alamat2, SMP01_Bandar, SMP01_Poskod, SMP01_Negeri, SMP01_NoAkaun, SMP01_Bank FROM SMP01_Peribadi"
        Dim param As New List(Of SqlParameter)
        If (kod <> "") Then
            query = query & " WHERE SMP01_Nomatrik LIKE '%' + @kod + '%'"
            param.Add(New SqlParameter("@kod", kod))
        End If
        Return db.Read(query, param)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SavePenghutang(penghutang As MaklumatPenghutang) As String
        Dim resp As New ResponseRepository
        resp.Success("Data telah disimpan")
        Dim success As Integer = 0
        If penghutang Is Nothing Then
            resp.Failed("Tidak disimpan")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        'Do insertNewPenghutang if No_Rujukan is not exist in SMKB_Penghutang_Master
        If penghutang.IdPenghutang = "" Then
            If CheckPenghutangExist(penghutang) = False Or penghutang.KategoriPenghutang = "OA" Then
                If InsertNewPenghutang(penghutang) <> "OK" Then
                    resp.Failed("Gagal menyimpan penghutang")
                    Return JsonConvert.SerializeObject(resp.GetResult())
                End If
            Else
                resp.Failed("No. Rujukan telah wujud")
                Return JsonConvert.SerializeObject(resp.GetResult())
            End If
        Else
            If UpdatePenghutang(penghutang) <> "OK" Then
                resp.Failed("Gagal menyimpan penghutang")
                Return JsonConvert.SerializeObject(resp.GetResult())
            End If
        End If

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function
    Private Function InsertNewPenghutang(penghutang As MaklumatPenghutang)
        Dim db As New DBKewConn

        Dim query As String = "
            INSERT INTO SMKB_Penghutang_Master (Kod_Penghutang, Nama_Penghutang, Kategori_Penghutang, Tel_Bimbit, Emel, No_Rujukan, Status, Alamat_1, Alamat_2, Poskod, Bandar, Kod_Negeri, Kod_Negara, Kod_Bank, No_Akaun)
                                        VALUES (@kodPenghutang, @nama, @kategori, @telBimbit, @emel, @idPenghutang, @status, @alamat1, @alamat2, @poskod, @bandar, @kodNegeri, @kodNegara, @kodBank, @noAkaun)"

        Dim param As New List(Of SqlParameter)

        Dim idPenghutang As String = GeneratePenghutangID()

        'Maklumat penghutang
        param.Add(New SqlParameter("@kodPenghutang", idPenghutang))
        param.Add(New SqlParameter("@nama", penghutang.Nama))
        param.Add(New SqlParameter("@kategori", penghutang.KategoriPenghutang))
        param.Add(New SqlParameter("@telBimbit", penghutang.NoTelefon))
        param.Add(New SqlParameter("@emel", penghutang.Email))
        param.Add(New SqlParameter("@idPenghutang", penghutang.Id))
        param.Add(New SqlParameter("@status", 1))

        'Maklumat alamat
        param.Add(New SqlParameter("@alamat1", penghutang.Alamat1))
        param.Add(New SqlParameter("@alamat2", penghutang.Alamat2))
        param.Add(New SqlParameter("@bandar", penghutang.Bandar))
        param.Add(New SqlParameter("@poskod", penghutang.Poskod))
        param.Add(New SqlParameter("@kodNegeri", penghutang.KodNegeri))
        param.Add(New SqlParameter("@kodNegara", penghutang.KodNegara))

        'Maklumat bank
        param.Add(New SqlParameter("@noAkaun", penghutang.NoAkaun))
        param.Add(New SqlParameter("@kodBank", penghutang.KodBank))

        Return db.Process(query, param)
    End Function
    Private Function UpdatePenghutang(penghutang As MaklumatPenghutang)
        Dim db As New DBKewConn

        Dim query As String = "
            UPDATE SMKB_Penghutang_Master SET Nama_Penghutang=@nama, Kod_Penghutang=@idPenghutang, No_Rujukan=@id, Kategori_Penghutang=@kategori, Tel_Bimbit=@telBimbit, Emel=@emel, Alamat_1=@alamat1, Alamat_2=@alamat2, Bandar=@bandar, Poskod=@poskod, Kod_Negeri=@kodNegeri, Kod_Negara=@kodNegara, No_Akaun=@noAkaun, Kod_Bank=@kodBank WHERE Kod_Penghutang=@idPenghutang"

        Dim param As New List(Of SqlParameter)

        'Maklumat penghutang
        param.Add(New SqlParameter("@id", penghutang.Id))
        param.Add(New SqlParameter("@nama", penghutang.Nama))
        param.Add(New SqlParameter("@kategori", penghutang.KategoriPenghutang))
        param.Add(New SqlParameter("@telBimbit", penghutang.NoTelefon))
        param.Add(New SqlParameter("@emel", penghutang.Email))
        param.Add(New SqlParameter("@idPenghutang", penghutang.IdPenghutang))

        'Maklumat alamat
        param.Add(New SqlParameter("@alamat1", penghutang.Alamat1))
        param.Add(New SqlParameter("@alamat2", penghutang.Alamat2))
        param.Add(New SqlParameter("@bandar", penghutang.Bandar))
        param.Add(New SqlParameter("@poskod", penghutang.Poskod))
        param.Add(New SqlParameter("@kodNegeri", penghutang.KodNegeri))
        param.Add(New SqlParameter("@kodNegara", penghutang.KodNegara))

        'Maklumat bank
        param.Add(New SqlParameter("@noAkaun", penghutang.NoAkaun))
        param.Add(New SqlParameter("@kodBank", penghutang.KodBank))

        Return db.Process(query, param)
    End Function
    Private Function CheckPenghutangExist(penghutang As MaklumatPenghutang) As Boolean
        Dim db As New DBKewConn

        Dim query As String = "
            SELECT Nama_Penghutang as nama FROM SMKB_Penghutang_Master WHERE No_Rujukan=@id"

        Dim param As New List(Of SqlParameter)

        'Maklumat penghutang
        param.Add(New SqlParameter("@id", penghutang.Id))

        Dim dt As DataTable
        dt = db.Read(query, param)

        Debug.Print(dt.Rows.Count)

        If dt.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    'Generate Kod_Penghutang for every new data insertion
    Private Function GeneratePenghutangID() As String
        Dim db As New DBKewConn

        Dim year = Date.Now.ToString("yyyy")

        Dim lastID As Integer = 1
        Dim newPenghutangID As String = ""

        Dim query As String = "SELECT TOP 1 No_Akhir as id FROM SMKB_No_Akhir WHERE Kod_Modul ='12' AND Prefix ='PH' AND Tahun=@year"
        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@year", year))

        Dim dt As DataTable
        dt = db.Read(query, param)

        If dt.Rows.Count > 0 Then
            lastID = CInt(dt.Rows(0).Item("id")) + 1
            UpdateNoAkhir("12", "PH", year, lastID)
        Else
            InsertNoAkhir("12", "PH", year, lastID)
        End If
        newPenghutangID = "PH" + Format(lastID, "000000").ToString + Right(year.ToString(), 2)

        Return newPenghutangID
    End Function
    Private Function UpdateNoAkhir(kodModul As String, prefix As String, year As String, ID As String)
        Dim db As New DBKewConn
        Dim query As String = "UPDATE SMKB_No_Akhir
        SET No_Akhir=@No_Akhir
        WHERE Kod_Modul=@Kod_Modul AND Prefix=@Prefix AND Tahun=@Tahun"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@No_Akhir", ID))
        param.Add(New SqlParameter("@Kod_Modul", kodModul))
        param.Add(New SqlParameter("@Prefix", prefix))
        param.Add(New SqlParameter("@Tahun", year))

        Return db.Process(query, param)
    End Function

    Private Function InsertNoAkhir(kodModul As String, prefix As String, year As String, ID As String)
        Dim db As New DBKewConn
        Dim query As String = "INSERT INTO SMKB_No_Akhir
        VALUES(@Kod_Modul ,@Prefix, @No_Akhir, @Tahun, @Butiran, @Kod_PTJ)"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@Kod_Modul", kodModul))
        param.Add(New SqlParameter("@Prefix", prefix))
        param.Add(New SqlParameter("@No_Akhir", ID))
        param.Add(New SqlParameter("@Tahun", year))
        param.Add(New SqlParameter("@Butiran", "Jurnal"))
        param.Add(New SqlParameter("@Kod_PTJ", "-"))

        Return db.Process(query, param)
    End Function
End Class