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

    'DAFTAR KATEGORI PENGHUTANG
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SaveCategory(category As CategoryPenghutang) As String
        Dim resp As New ResponseRepository
        resp.Success("Data telah disimpan")
        Dim success As Integer = 0
        If category Is Nothing Then
            resp.Failed("Tidak disimpan")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        If category.OldKod <> "" Then
            If UpdateKategoriPenghutang(category) <> "OK" Then
                resp.Failed("Gagal menyimpan kategori")
                Return JsonConvert.SerializeObject(resp.GetResult())
            End If
        Else
            If InsertNewKategoriPenghutang(category) <> "OK" Then
                resp.Failed("Gagal menyimpan kategori")
                Return JsonConvert.SerializeObject(resp.GetResult())
            End If
        End If

        Return JsonConvert.SerializeObject(resp.GetResult())

    End Function
    Private Function InsertNewKategoriPenghutang(category As CategoryPenghutang)
        Dim db As New DBKewConn

        Dim query As String = "INSERT INTO SMKB_Lookup_Detail(Kod, Kod_Detail, Butiran, Tarikh_Mula, Tarikh_Tamat, Status, Dibuat_Oleh, Tarikh_Dibuat, Kod_Korporat) VALUES (@kod, @kodDetail, @butiran, @tarikhMula, @tarikhTamat, @status, @dibuatOleh, @tarikhDibuat, @kodKorporat)"

        Dim param As New List(Of SqlParameter)

        Dim userId = "02642"

        param.Add(New SqlParameter("@kod", "0152"))
        param.Add(New SqlParameter("@kodDetail", category.Kod))
        param.Add(New SqlParameter("@butiran", category.Butiran))
        param.Add(New SqlParameter("@tarikhMula", DateTime.Now))
        param.Add(New SqlParameter("@tarikhTamat", DateTime.Now))
        param.Add(New SqlParameter("@status", category.Status))
        param.Add(New SqlParameter("@dibuatOleh", userId))
        param.Add(New SqlParameter("@tarikhDibuat", DateTime.Now))
        param.Add(New SqlParameter("@kodKorporat", "UTEM"))

        Return db.Process(query, param)
    End Function

    Private Function UpdateKategoriPenghutang(category As CategoryPenghutang)
        Dim db As New DBKewConn
        Dim query As String = "UPDATE SMKB_Lookup_Detail SET Kod_Detail=@kodDetail, Butiran=@butiran, Status=@status WHERE Kod='0152' AND Kod_Detail=@oldKod"
        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@kodDetail", category.Kod))
        param.Add(New SqlParameter("@butiran", category.Butiran))
        param.Add(New SqlParameter("@status", category.Status))
        param.Add(New SqlParameter("@oldKod", category.OldKod))
        Return db.Process(query, param)
    End Function

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
    Public Function SavePenghutang(penghutang As Penghutang) As String
        Dim resp As New ResponseRepository
        resp.Success("Data telah disimpan")
        Dim success As Integer = 0
        If penghutang Is Nothing Then
            resp.Failed("Tidak disimpan")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        'Do insertNewPenghutang if ID_Penghutang is not exist in SMKB_Penghutang_Master
        If penghutang.OldId = "" Then
            If InsertNewPenghutang(penghutang) <> "OK" Then
                resp.Failed("Gagal menyimpan penghutang")
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
    Private Function InsertNewPenghutang(penghutang As Penghutang)
        Dim db As New DBKewConn

        Dim query As String = "
            INSERT INTO SMKB_Penghutang_Master (Kod_Penghutang, Nama_Penghutang, Kategori_Penghutang, No_Akaun, Alamat_1, Alamat_2, Poskod, Kod_Negeri, Kod_Negara, Tel_Bimbit, Emel, ID_Penghutang, Jenis_ID, Status)
                                        VALUES (@kodPenghutang, @nama, @kategori, @noAkaun, @alamat1, @alamat2, @poskod, @kodNegeri, @kodNegara, @telBimbit, @emel, @idPenghutang, @jenisId, @status)"

        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@kodPenghutang", penghutang.Id))
        param.Add(New SqlParameter("@nama", penghutang.Nama))
        param.Add(New SqlParameter("@kategori", penghutang.KategoriPenghutang))
        param.Add(New SqlParameter("@noAkaun", penghutang.NoAkaun))
        param.Add(New SqlParameter("@alamat1", penghutang.Alamat1))
        param.Add(New SqlParameter("@alamat2", penghutang.Alamat2))
        param.Add(New SqlParameter("@poskod", penghutang.Poskod))
        param.Add(New SqlParameter("@kodNegeri", penghutang.KodNegeri))
        param.Add(New SqlParameter("@kodNegara", penghutang.KodNegara))
        param.Add(New SqlParameter("@telBimbit", penghutang.NoTelefon))
        param.Add(New SqlParameter("@emel", penghutang.Email))
        param.Add(New SqlParameter("@idPenghutang", penghutang.Id))
        param.Add(New SqlParameter("@jenisId", "01"))
        param.Add(New SqlParameter("@status", penghutang.Status))

        Return db.Process(query, param)
    End Function
    Private Function UpdatePenghutang(penghutang As Penghutang)
        Dim db As New DBKewConn

        Dim query As String = "
            UPDATE SMKB_Penghutang_Master SET Nama_Penghutang=@nama, Kod_Penghutang=@idPenghutang, ID_Penghutang=@idPenghutang, Kategori_Penghutang=@kategori, No_Akaun=@noAkaun, Alamat_1=@alamat1, Alamat_2=@alamat2, Poskod=@poskod, Kod_Negeri=@kodNegeri, Kod_Negara=@kodNegara, Tel_Bimbit=@telBimbit, Emel=@emel, Status=@status WHERE ID_Penghutang=@oldId"

        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@nama", penghutang.Nama))
        param.Add(New SqlParameter("@kategori", penghutang.KategoriPenghutang))
        param.Add(New SqlParameter("@noAkaun", penghutang.NoAkaun))
        param.Add(New SqlParameter("@idPenghutang", penghutang.Id))
        param.Add(New SqlParameter("@alamat1", penghutang.Alamat1))
        param.Add(New SqlParameter("@alamat2", penghutang.Alamat2))
        param.Add(New SqlParameter("@poskod", penghutang.Poskod))
        param.Add(New SqlParameter("@kodNegeri", penghutang.KodNegeri))
        param.Add(New SqlParameter("@kodNegara", penghutang.KodNegara))
        param.Add(New SqlParameter("@telBimbit", penghutang.NoTelefon))
        param.Add(New SqlParameter("@emel", penghutang.Email))
        param.Add(New SqlParameter("@oldId", penghutang.OldId))
        param.Add(New SqlParameter("@status", penghutang.Status))

        Return db.Process(query, param)
    End Function

    Private Function CheckPenghutangExist(penghutang As Penghutang) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT * FROM SMKB_Penghutang_Master WHERE Kod_Penghutang=@idPenghutang"
        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@idPenghutang", penghutang.Id))
        Return db.Read(query, param)
    End Function

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
        Dim query As String = "SELECT Kod_Detail as value, Butiran as text FROM SMKB_Lookup_Detail WHERE Kod='0079'"
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
    Public Function GetPenghutangList(ByVal q As String) As String
        Dim tmpDT As DataTable = GetPenghutang(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function
    Private Function GetPenghutang(kod As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT Nama_Penghutang as nama, Kategori_Penghutang as kategoriPenghutang, No_Akaun as noAkaun, Alamat_1 as alamat1, Alamat_2 as alamat2, Poskod as poskod, Kod_Negeri as kodNegeri, Kod_Negara as kodNegara, Tel_Bimbit as telBimbit, Emel as emel, ID_Penghutang as idPenghutang, Status as status FROM SMKB_Penghutang_Master"
        Dim param As New List(Of SqlParameter)
        If (kod <> "") Then
            query = query & " WHERE Nama_Penghutang LIKE '%' + @kod + '%'"
            param.Add(New SqlParameter("@kod", kod))
        End If
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
        Dim query As String = "SELECT TOP (500) Kod_Detail as value, Butiran as text FROM SMKB_Lookup_Detail WHERE Kod='0079'"
        Dim param As New List(Of SqlParameter)
        If (kod <> "") Then
            query = query & " AND Kod_Detail=@kod"
            param.Add(New SqlParameter("@kod", kod))
        End If
        Return db.Read(query, param)
    End Function
End Class