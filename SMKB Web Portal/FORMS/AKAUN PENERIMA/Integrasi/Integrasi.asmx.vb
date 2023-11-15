Imports System.ComponentModel
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports Newtonsoft.Json
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
'Imports System.Web.Http
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Threading
Imports Microsoft.Ajax.Utilities
Imports AjaxControlToolkit
Imports System.Data.Entity.Core.Mapping
Imports System

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class Integrasi
    Inherits System.Web.Services.WebService
    Dim dt As DataTable

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadOrderRecord_SenaraiLulusInvois(category_filter As String, isClicked As Boolean, tkhMula As String, tkhTamat As String) As String
        Dim resp As New ResponseRepository

        If isClicked = False Then
            Return JsonConvert.SerializeObject(New DataTable)
        End If

        dt = GetOrder_SenaraiTransaksiInvois(category_filter, tkhMula, tkhTamat)

        Return JsonConvert.SerializeObject(dt)
    End Function
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetOrder_SenaraiTransaksiInvois(category_filter As String, tkhMula As String, tkhTamat As String) As DataTable
        Dim db = New DBKewConn
        Dim tarikhQuery As String = ""
        Dim param As List(Of SqlParameter) = New List(Of SqlParameter)

        'If category_filter = "1" Then 'Harini
        '    tarikhQuery = " AND CAST(A.Tkh_Mohon AS DATE) = CAST(CURRENT_TIMESTAMP AS DATE) "
        'ElseIf category_filter = "2" Then 'Semalam
        '    tarikhQuery = " AND CAST(A.Tkh_Mohon AS DATE) = CAST(DATEADD(day, -1, CURRENT_TIMESTAMP) AS DATE) "
        'ElseIf category_filter = "3" Then 'seminggu
        '    tarikhQuery = " AND A.Tkh_Mohon >= DATEADD(day, -7, CURRENT_TIMESTAMP) AND A.Tkh_Mohon < CURRENT_TIMESTAMP "
        'ElseIf category_filter = "4" Then '30 hari
        '    tarikhQuery = " AND A.Tkh_Mohon >= DATEADD(day, -30, CURRENT_TIMESTAMP) AND A.Tkh_Mohon < CURRENT_TIMESTAMP "
        'ElseIf category_filter = "5" Then '60 hari
        '    tarikhQuery = " AND A.Tkh_Mohon >= DATEADD(day, -60, CURRENT_TIMESTAMP) AND A.Tkh_Mohon < CURRENT_TIMESTAMP "
        'ElseIf category_filter = "6" Then 'custom
        '    tarikhQuery = " AND A.Tkh_Mohon >= @tkhMula AND A.Tkh_Mohon <= @TkhTamat "
        '    param.Add(New SqlParameter("@tkhMula", tkhMula))
        '    param.Add(New SqlParameter("@TkhTamat", tkhTamat))
        'End If

        Dim query As String = "SELECT DISTINCT A.SMP01_Nomatrik,B.SMP01_Nama ,B.SMP01_KP, 
                            SMP01_Alamat1,SMP01_Alamat2,SMP01_Poskod,SMP01_Bandar,SMP01_Negeri, 
                            SMP01_Alamat1+', '+SMP01_Alamat2+', '+SMP01_Poskod+', '+SMP01_Bandar+', '+SMP01_Negeri AS ALAMAT, SMP01_NoTelBimBit
                            FROM SMP51_APDebitCaj A
                            LEFT JOIN SMP01_Peribadi B ON A.SMP01_Nomatrik=B.SMP01_Nomatrik
                            WHERE B.SMP01_Fakulti='FTMK' AND A.SMP51_KodSesi='2-2022/2023'
                            ORDER BY SMP01_Nomatrik" & tarikhQuery

        Return db.Read(query, param)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetKategoriPelajar(category As String, kod As String) As String
        Dim tmpDT As DataTable = GetKodKategoriPelajar(category, kod)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetKodKategoriPelajar(category As String, kod As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT TOP(10) Kod_Penghutang as value, Kategori_Penghutang + ' - ' + Nama_Penghutang as text FROM SMKB_Penghutang_Master WHERE Status='1'"
        Dim param As New List(Of SqlParameter)
        If kod <> "" Then
            query &= " AND (Kod_Penghutang LIKE '%' + @kod + '%' OR Nama_Penghutang LIKE '%' + @kod2 + '%' OR No_Rujukan LIKE '%' + @kod3 + '%')"
            param.Add(New SqlParameter("@kod", kod))
            param.Add(New SqlParameter("@kod2", kod))
            param.Add(New SqlParameter("@kod3", kod))
        End If

        If category <> "" Then
            query &= " AND Kategori_Penghutang = @category"
            param.Add(New SqlParameter("@category", category))
        End If

        Return db.Read(query, param)
    End Function
    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetPenajaList(ByVal q As String) As String
        Dim tmpDT As DataTable = GetKodPenaja(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetKodPenaja(kod As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT Kod_Penghutang AS value,Nama_Penghutang AS text,No_Rujukan FROM SMKB_Penghutang_Master WHERE Kategori_Penghutang='PN' AND Status='1'"
        Dim param As New List(Of SqlParameter)
        If kod <> "" Then
            query &= " AND (Kod_Penghutang LIKE '%' + @kod + '%' OR Nama_Penghutang LIKE '%' + @kod2 + '%' OR No_Rujukan LIKE '%' + @kod3 + '%')"
            param.Add(New SqlParameter("@kod", kod))
            param.Add(New SqlParameter("@kod2", kod))
            param.Add(New SqlParameter("@kod3", kod))
        End If

        Return db.Read(query, param)
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function Load_MaklumatBilTerperinciPelajar(category_filter As String, isClicked As Boolean, tkhMula As DateTime, tkhTamat As DateTime, custcode As String, status As String) As String
        Dim resp As New ResponseRepository

        'If isClicked = False Then
        '    Return JsonConvert.SerializeObject(New DataTable)
        'End If

        dt = Get_MaklumatBilTerperinciPelajar(category_filter, tkhMula, tkhTamat, custcode, status)

        Return JsonConvert.SerializeObject(dt)
    End Function
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function Get_MaklumatBilTerperinciPelajar(category_filter As String, tkhMula As DateTime, tkhTamat As DateTime, custcode As String, status As String) As DataTable
        Dim db = New DBKewConn
        Dim tarikhQuery As String = ""
        Dim param As New List(Of SqlParameter)
        Dim query As String = ""

        ' If category_filter = "1" Then 'Harini
        '     tarikhQuery = " AND CAST(A.Tkh_Mohon AS DATE) = CAST(CURRENT_TIMESTAMP AS DATE) "
        ' ElseIf category_filter = "2" Then 'Semalam
        '     tarikhQuery = " AND CAST(A.Tkh_Mohon AS DATE) = CAST(DATEADD(day, -1, CURRENT_TIMESTAMP) AS DATE) "
        ' ElseIf category_filter = "3" Then 'seminggu
        '     tarikhQuery = " AND A.Tkh_Mohon >= DATEADD(day, -7, CURRENT_TIMESTAMP) AND A.Tkh_Mohon < CURRENT_TIMESTAMP "
        ' ElseIf category_filter = "4" Then '30 hari
        '     tarikhQuery = " AND A.Tkh_Mohon >= DATEADD(day, -30, CURRENT_TIMESTAMP) AND A.Tkh_Mohon < CURRENT_TIMESTAMP "
        ' ElseIf category_filter = "5" Then '60 hari
        '     tarikhQuery = " AND A.Tkh_Mohon >= DATEADD(day, -60, CURRENT_TIMESTAMP) AND A.Tkh_Mohon < CURRENT_TIMESTAMP "
        ' ElseIf category_filter = "6" Then 'custom
        '     tarikhQuery = " AND A.Tkh_Mohon >= @tkhMula AND A.Tkh_Mohon <= @TkhTamat "
        '     param = New List(Of SqlParameter)
        '     param.Add(New SqlParameter("@tkhMula", tkhMula))
        '     param.Add(New SqlParameter("@TkhTamat", tkhTamat))
        ' End If

        ' Dim query As String = "SELECT A.No_Bil as No_Invois,B.Nama_Penghutang,C.Butiran as UrusNiaga,A.Tujuan,FORMAT(A.Tkh_Mohon, 'dd/MM/yyyy') AS TKHMOHON,
        '                         CASE WHEN Tkh_Mula <> '' THEN FORMAT(Tkh_Mula, 'dd/MM/yyyy') END AS Tkh_Mula,
        '                         CASE WHEN Tkh_Tamat <> '' THEN FORMAT(Tkh_Tamat, 'dd/MM/yyyy') END AS Tkh_Tamat,Jumlah
        '                         FROM SMKB_Bil_Hdr A
        '                         inner JOIN SMKB_Penghutang_Master B ON A.Kod_Penghutang=B.Kod_Penghutang
        '                         inner JOIN SMKB_Kod_Urusniaga C ON A.Kod_Urusniaga=C.Kod_Urusniaga
        '                         WHERE  A.Status='1' AND A.Kod_Status_Dok=@status AND A.Kod_Penghutang = @kodpenghutang;"

        'If status = "03" Then
        query = "SELECT DISTINCT A.SMP51_DCID,A.SMP01_Nomatrik,CONVERT(varchar, SMP51_TrkDC, 103) AS SMP51_TrkDC,SMP51_JenisDC,'YURAN' AS TUJUAN,SMP51_JumDC FROM SMP51_APDEBITCAJ A
                        INNER JOIN SMP51_APDEBITCAJDT B ON A.SMP51_DCID=B.SMP51_DCID
                        WHERE A.SMP01_Nomatrik='B020310247'"
        param.Add(New SqlParameter("@kodpenghutang", custcode))

        'Else 'Dah bayar
        '    query = "Select A.No_Dok As No_Invois,
        '                B.Nama_Penghutang,
        '                C.Butiran As UrusNiaga,
        '                A.Tujuan,
        '                FORMAT(A.Tkh_Daftar, 'dd/MM/yyyy') AS TKHMOHON,
        '                A.Jumlah_Bayar as Jumlah
        '            FROM SMKB_Terima_Hdr A
        '            INNER JOIN SMKB_Penghutang_Master B ON A.Kod_Penghutang = B.Kod_Penghutang
        '            INNER JOIN SMKB_Kod_Urusniaga C ON A.Kod_Urusniaga = C.Kod_Urusniaga
        '            WHERE A.Status = '1' AND B.Kod_Penghutang = @kodpenghutang;"

        '    param.Add(New SqlParameter("@kodpenghutang", custcode))

        'End If

        Return db.Read(query, param)

        Try
            Return db.Read(query, param)
        Catch ex As SqlException
            ' Handle the exception, and log the error message.
            Console.WriteLine("SQL Exception Message: " & ex.Message)
        End Try

    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadHdrInvois(ByVal id As String) As String
        Dim resp As New ResponseRepository

        dt = GetHdrInvois(id)
        resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    Private Function GetHdrInvois(id As String) As DataTable
        Dim db = New DBKewConn

        Dim query As String = "SELECT SMP51_DCID AS No_Bil,SMP01_Nomatrik AS Kod_Penghutang, '0' AS Kontrak,'YURAN'+' '+SMP51_KodSesi AS Tujuan , '03' AS Kod_Status_Dok,
                                CASE WHEN SMP51_TrkDC <> '' THEN FORMAT(SMP51_TrkDC, 'dd/MM/yyyy') END AS TkhMula,
                                CASE WHEN SMP51_TrkDC <> '' THEN FORMAT(SMP51_TrkDC, 'dd/MM/yyyy') END AS TkhTamat,SMP51_TrkDC AS Tkh_Mula,SMP51_TrkDC AS Tkh_Tamat,SMP51_TrkDC AS Tkh_Bil,SMP01_Nomatrik AS No_Rujukan
                                FROM SMP51_APDEBITCAJ
                                WHERE SMP51_DCID='6136'"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@No_Invois", id))

        Return db.Read(query, param)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadRecordInvoisTerimaanBayaran(ByVal id As String) As String
        Dim resp As New ResponseRepository

        dt = GetTransaksiInvoisForTerimaanBayaran(id)
        resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    Private Function GetTransaksiInvoisForTerimaanBayaran(kod As String) As DataTable
        Dim db = New DBKewConn

        'Dim jumlahBayar As Decimal = GetJumlaBayarByNoDokFromTerimaHdr(kod)
        Dim query As String = "SELECT 
                            ROW_NUMBER() OVER (ORDER BY B.SMP51_DCID) AS dataid,
                            C.Kod_Kump_Wang AS colhidkw,
                            (SELECT Butiran FROM SMKB_Kump_Wang AS kw WHERE C.Kod_Kump_Wang = kw.Kod_Kump_Wang) AS colKW,
                            C.Kod_Operasi AS colhidko,
                            (SELECT Butiran FROM SMKB_Operasi AS ko WHERE C.Kod_Operasi = ko.Kod_Operasi) AS colKO,
                            C.Kod_Projek AS colhidkp,
                            (SELECT Butiran FROM SMKB_Projek AS kp WHERE C.Kod_Projek = kp.Kod_Projek) AS colKp,
                            C.Kod_PTJ AS colhidptj,
                            (SELECT F.Pejabat FROM [DEVMIS\SQL_INS01].dbStaf.dbo.MS_PEJABAT AS F
                            WHERE F.STATUS = 1 AND F.kodpejabat = LEFT(C.Kod_PTJ, 2)) AS ButiranPTJ,
                            C.Kod_Vot,
                            (SELECT Kod_Vot + ' - ' + Butiran FROM SMKB_Vot AS vot WHERE C.Kod_Vot = vot.Kod_Vot) AS ButiranVot,
                            (SELECT Butiran FROM SMKB_Vot AS vot WHERE C.Kod_Vot = vot.Kod_Vot) AS Perkara,
                            '1.00' AS Kuantiti,
                            B.SMP51_AMAUNDC AS Kadar_Harga,
                            B.SMP51_AMAUNDC AS Jumlah,
                            '0' AS Jumlah_Bayar,
                            '0.00' AS Diskaun,
                            '0.00' AS Cukai
                            FROM SMP51_APDebitCaj A
                            INNER JOIN SMP51_APDEBITCAJDT B ON A.SMP51_DCID=B.SMP51_DCID
                            INNER JOIN SMKB_COA_Master C ON B.SMP51_KODVOT=C.Kod_Vot AND Kod_PTJ='500000'
                            WHERE A.SMP51_DCID='6136'"

        Dim param As New List(Of SqlParameter)
        'param.Add(New SqlParameter("@jumlahBayar", jumlahBayar))
        param.Add(New SqlParameter("@No_Invois", kod))

        Return db.Read(query, param)
    End Function

    Public Function GetJumlaBayarByNoDokFromTerimaHdr(noBil As String) As Decimal
        Dim db = New DBKewConn

        ' Create a SQL command for selecting No_Bil
        ' Dim selectQuery As String = "SELECT SUM(Jumlah_Bayar) as Jumlah FROM SMKB_Terima_Hdr WHERE No_Rujukan=@nobil"
        Dim selectQuery As String = "Select Jumlah_Bayar as Jumlah from SMKB_Terima_Hdr where No_Dok=@nodok"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@nodok", noBil))

        ' Execute the query to retrieve No_Bil
        Dim result As DataTable = db.Read(selectQuery, param)

        Dim jumlah As New Decimal

        ' handle error
        If Not IsDBNull(result.Rows(0)("Jumlah")) Then
            jumlah = result.Rows(0)("Jumlah")
        Else
            jumlah = 0.00
        End If

        Return jumlah
    End Function


    <WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadOrderRecord_SenaraiPelajarPenaja(category_filter As String, isClicked As Boolean, tkhMula As String, tkhTamat As String) As String
        Dim resp As New ResponseRepository

        If isClicked = False Then
            Return JsonConvert.SerializeObject(New DataTable)
        End If

        dt = GetOrder_SSenaraiPelajarPenaja(category_filter, tkhMula, tkhTamat)

        Return JsonConvert.SerializeObject(dt)
    End Function
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetOrder_SSenaraiPelajarPenaja(category_filter As String, tkhMula As String, tkhTamat As String) As DataTable
        Dim db = New DBKewConn
        Dim tarikhQuery As String = ""
        Dim param As List(Of SqlParameter) = New List(Of SqlParameter)

        'If category_filter = "1" Then 'Harini
        '    tarikhQuery = " AND CAST(A.Tkh_Mohon AS DATE) = CAST(CURRENT_TIMESTAMP AS DATE) "
        'ElseIf category_filter = "2" Then 'Semalam
        '    tarikhQuery = " AND CAST(A.Tkh_Mohon AS DATE) = CAST(DATEADD(day, -1, CURRENT_TIMESTAMP) AS DATE) "
        'ElseIf category_filter = "3" Then 'seminggu
        '    tarikhQuery = " AND A.Tkh_Mohon >= DATEADD(day, -7, CURRENT_TIMESTAMP) AND A.Tkh_Mohon < CURRENT_TIMESTAMP "
        'ElseIf category_filter = "4" Then '30 hari
        '    tarikhQuery = " AND A.Tkh_Mohon >= DATEADD(day, -30, CURRENT_TIMESTAMP) AND A.Tkh_Mohon < CURRENT_TIMESTAMP "
        'ElseIf category_filter = "5" Then '60 hari
        '    tarikhQuery = " AND A.Tkh_Mohon >= DATEADD(day, -60, CURRENT_TIMESTAMP) AND A.Tkh_Mohon < CURRENT_TIMESTAMP "
        'ElseIf category_filter = "6" Then 'custom
        '    tarikhQuery = " AND A.Tkh_Mohon >= @tkhMula AND A.Tkh_Mohon <= @TkhTamat "
        '    param.Add(New SqlParameter("@tkhMula", tkhMula))
        '    param.Add(New SqlParameter("@TkhTamat", tkhTamat))
        'End If

        Dim query As String = "SELECT A.Kod_Penghutang,B.Nama_Penghutang,B.No_Rujukan,A.Tujuan,Jumlah 
                                FROM SMP_Order_Hdr A
                                INNER JOIN SMKB_Penghutang_Master B ON A.Kod_Penghutang=b.Kod_Penghutang
                                WHERE Kod_Penaja='KWSP' AND Kod_Sesi = '2-2022/2023'" & tarikhQuery

        Return db.Read(query, param)
    End Function
End Class