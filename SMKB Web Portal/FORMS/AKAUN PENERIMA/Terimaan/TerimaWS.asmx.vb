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

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class TerimaWS
    Inherits System.Web.Services.WebService

    'Dim sqlcmd As SqlCommand
    'Dim sqlcon As SqlConnection    
    'Dim sqlread As SqlDataReader
    Dim dt As DataTable

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetVotCOA(ByVal q As String) As String


        Dim tmpDT As DataTable = GetKodCOAList(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetKodCOAList(kodCariVot As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT top 10 CONCAT(a.Kod_Vot, ' - ', vot.Butiran, ', ', a.Kod_Operasi, ' - ', ko.Butiran, ', ', kp.Butiran, ', ', a.Kod_Kump_Wang, ' - ', REPLACE(kw.Butiran, 'KUMPULAN WANG', 'KW'), ', ', mj.Pejabat) AS text,
                    a.Kod_Vot AS value ,
                    mj.Pejabat as colPTJ , kw.Butiran as colKW , ko.Butiran as colKO ,  kp.Butiran as colKp , 
                    a.Kod_PTJ as colhidptj , a.Kod_Kump_Wang as colhidkw , a.Kod_Operasi as colhidko , a.Kod_Projek as colhidkp
                    FROM SMKB_COA_Master AS a 
                    JOIN SMKB_Vot AS vot ON a.Kod_Vot = vot.Kod_Vot
                    JOIN SMKB_Operasi AS ko ON a.Kod_Operasi = ko.Kod_Operasi
                    JOIN SMKB_Kump_Wang AS kw ON a.Kod_Kump_Wang = kw.Kod_Kump_Wang
					JOIN SMKB_Projek as kp on kp.Kod_Projek = a.Kod_Projek
					JOIN [DEVMIS\SQL_INS01].dbStaf.dbo.MS_PEJABAT AS mj ON mj.status = '1' and mj.kodpejabat = left(a.Kod_PTJ,2) 
                    WHERE a.status = 1 "

        Dim param As New List(Of SqlParameter)
        If kodCariVot <> "" Then
            query &= "AND (a.Kod_Vot LIKE '%' + @kod + '%' OR a.Kod_Operasi LIKE '%' + @kod2 + '%' OR a.Kod_Projek LIKE '%' + @kod3 + '%' OR a.Kod_Kump_Wang LIKE '%' + @kod4 + '%' OR a.Kod_PTJ LIKE '%' + @kod5 + '%' OR vot.Butiran LIKE '%' + @kodButir + '%' OR ko.Butiran LIKE '%' + @kodButir1 + '%'  OR kw.Butiran LIKE '%' + @kodButir2 + '%' OR mj.pejabat LIKE '%' + @kodButir3 + '%')"

            param.Add(New SqlParameter("@kod", kodCariVot))
            param.Add(New SqlParameter("@kod2", kodCariVot))
            param.Add(New SqlParameter("@kod3", kodCariVot))
            param.Add(New SqlParameter("@kod4", kodCariVot))
            param.Add(New SqlParameter("@kod5", kodCariVot))
            param.Add(New SqlParameter("@kodButir", kodCariVot))
            param.Add(New SqlParameter("@kodButir1", kodCariVot))
            param.Add(New SqlParameter("@kodButir2", kodCariVot))
            param.Add(New SqlParameter("@kodButir3", kodCariVot))
        End If

        Return db.Read(query, param)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetPTJList(ByVal q As String) As String
        'Dim newList As List(Of ItemList)
        'newList = listItem

        'If (q <> "") Then
        '    newList = listItem.Where(
        '        Function(x) x.value.Contains(q) Or x.text.Contains(q)
        '    ).ToList()
        'End If

        Dim tmpDT As DataTable = GetKodPTJList(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetKodPTJList(kod As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT DISTINCT A.Kod_PTJ AS value, B.Pejabat as text
                                FROM SMKB_COA_Master A
                                INNER JOIN [DEVMIS\SQL_INS01].dbStaf.dbo.MS_PEJABAT AS B 
                                ON A.STATUS = 1 AND B.kodpejabat = left(A.Kod_PTJ,2) "
        Dim param As New List(Of SqlParameter)
        If kod <> "" Then
            query &= " WHERE A.Kod_PTJ LIKE '%' + @kod + '%' OR B.Pejabat LIKE '%' + @kod2 + '%' "
            param.Add(New SqlParameter("@kod", kod))
            param.Add(New SqlParameter("@kod2", kod))
        End If

        Return db.Read(query, param)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetVotList(ByVal q As String, ByVal kodptj As String) As String
        'Dim newList As List(Of ItemList)
        'newList = listItem

        'If (q <> "") Then
        '    newList = listItem.Where(
        '        Function(x) x.value.Contains(q) Or x.text.Contains(q)
        '    ).ToList()
        'End If

        Dim tmpDT As DataTable = GetKodVotList(q, kodptj)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetKodVotList(kod As String, kodptj As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT DISTINCT A.Kod_Vot as value,B.Butiran as text
                                FROM SMKB_COA_Master A
                                INNER JOIN SMKB_Vot B ON A.Kod_Vot=B.Kod_Vot"
        Dim param As New List(Of SqlParameter)
        If kodptj <> "" Then
            query &= " WHERE A.Kod_PTJ = @kodptj "
            param.Add(New SqlParameter("@kodptj", kodptj))
        End If
        If kod <> "" Then
            query &= " WHERE A.Kod_Vot LIKE '%' + @kod + '%' OR B.Butiran LIKE '%' + @kod2 + '%' "
            param.Add(New SqlParameter("@kod", kod))
            param.Add(New SqlParameter("@kod2", kod))
        End If

        Return db.Read(query, param)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetKWList(ByVal q As String, ByVal kodvot As String) As String
        'Dim newList As List(Of ItemList)
        'newList = listItem

        'If (q <> "") Then
        '    newList = listItem.Where(
        '        Function(x) x.value.Contains(q) Or x.text.Contains(q)
        '    ).ToList()
        'End If

        Dim tmpDT As DataTable = GetKodKWList(q, kodvot)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetKodKWList(kod As String, kodvot As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT DISTINCT A.Kod_Kump_Wang as value,B.Butiran as text
                                FROM SMKB_COA_Master A
                                INNER JOIN SMKB_Kump_Wang B ON A.Kod_Kump_Wang=B.Kod_Kump_Wang"
        Dim param As New List(Of SqlParameter)
        If kodvot <> "" Then
            query &= " WHERE A.Kod_Vot = @kodvot "
            param.Add(New SqlParameter("@kodvot", kodvot))
        End If
        If kod <> "" Then
            query &= " WHERE A.Kod_Kump_Wang LIKE '%' + @kod + '%' OR B.Butiran LIKE '%' + @kod2 + '%' "
            param.Add(New SqlParameter("@kod", kod))
            param.Add(New SqlParameter("@kod2", kod))
        End If

        Return db.Read(query, param)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetProjekList(ByVal q As String, ByVal kodptj As String, ByVal kodvot As String) As String
        'Dim newList As List(Of ItemList)
        'newList = listItem

        'If (q <> "") Then
        '    newList = listItem.Where(
        '        Function(x) x.value.Contains(q) Or x.text.Contains(q)
        '    ).ToList()
        'End If

        Dim tmpDT As DataTable = GetKodProjekList(q, kodptj, kodvot)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetKodProjekList(kod As String, kodptj As String, kodvot As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT DISTINCT A.Kod_Projek as value,B.Butiran as text FROM SMKB_COA_Master A
                                INNER JOIN SMKB_Projek B ON A.Kod_Projek = B.Kod_Projek"
        Dim param As New List(Of SqlParameter)
        If kodptj <> "" And kodvot <> "" Then
            query &= " WHERE A.Kod_PTJ =@kodptj AND A.Kod_Vot=@kodvot "
            param.Add(New SqlParameter("@kodptj", kodptj))
            param.Add(New SqlParameter("@kodvot", kodvot))
        End If
        If kod <> "" Then
            query &= " WHERE A.Kod_Projek LIKE '%' + @kod + '%' OR B.Butiran LIKE '%' + @kod2 + '%' "
            param.Add(New SqlParameter("@kod", kod))
            param.Add(New SqlParameter("@kod2", kod))
        End If

        Return db.Read(query, param)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetPenghutangList(ByVal q As String) As String
        'Dim newList As List(Of ItemList)
        'newList = listItem

        'If (q <> "") Then
        '    newList = listItem.Where(
        '        Function(x) x.value.Contains(q) Or x.text.Contains(q)
        '    ).ToList()
        'End If

        Dim tmpDT As DataTable = GetKodPenghutangList(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetKodPenghutangList(kod As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT Kod_Penghutang as value, Nama_Penghutang as text FROM SMKB_Penghutang_Master WHERE Status='1'"
        Dim param As New List(Of SqlParameter)
        If kod <> "" Then
            query &= " AND Kod_Penghutang LIKE '%' + @kod + '%' OR Nama_Penghutang LIKE '%' + @kod2 + '%' "
            param.Add(New SqlParameter("@kod", kod))
            param.Add(New SqlParameter("@kod2", kod))
        End If

        Return db.Read(query, param)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetUrusniagaList(ByVal q As String) As String
        'Dim newList As List(Of ItemList)
        'newList = listItem

        'If (q <> "") Then
        '    newList = listItem.Where(
        '        Function(x) x.value.Contains(q) Or x.text.Contains(q)
        '    ).ToList()
        'End If

        Dim tmpDT As DataTable = GetKodUrusniagaList(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetKodUrusniagaList(kod As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT Kod_Urusniaga AS value,Butiran AS text FROM SMKB_Kod_Urusniaga WHERE Status='1' AND Kod_Urusniaga='10'"
        Dim param As New List(Of SqlParameter)
        If kod <> "" Then
            query &= " AND (Kod_Urusniaga LIKE '%' + @kod + '%' OR Butiran LIKE '%' + @kod2 + '%') "
            param.Add(New SqlParameter("@kod", kod))
            param.Add(New SqlParameter("@kod2", kod))
        End If

        Return db.Read(query, param)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetModTerimaList(kod As String) As String

        Dim tmpDT As DataTable = GetLoadModTerimaList(kod)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetLoadModTerimaList(kod As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT mod_Bayar AS value,Butiran AS text FROM SMKB_Mod_Bayar WHERE Status='1'"
        Dim param As New List(Of SqlParameter)
        If kod <> "" Then
            query &= " AND (mod_Bayar LIKE '%' + @kod + '%' OR Butiran LIKE '%' + @kod2 + '%') "
            param.Add(New SqlParameter("@kod", kod))
            param.Add(New SqlParameter("@kod2", kod))
        End If
        Return db.Read(query, param)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetKodBankList(kod As String) As String

        Dim tmpDT As DataTable = GetLoadKodBankList(kod)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetLoadKodBankList(kod As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT Kod_Bank AS value,Kod_Bank+'-'+Nama_Bank AS text FROM SMKB_Bank_Master"
        Dim param As New List(Of SqlParameter)
        If kod <> "" Then
            query &= " AND (Kod_Bank LIKE '%' + @kod + '%' OR Nama_Bank LIKE '%' + @kod2 + '%') "
            param.Add(New SqlParameter("@kod", kod))
            param.Add(New SqlParameter("@kod2", kod))
        End If

        Return db.Read(query, param)
    End Function




    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetKOList(ByVal q As String, ByVal kodkw As String) As String
        'Dim newList As List(Of ItemList)
        'newList = listItem

        'If (q <> "") Then
        '    newList = listItem.Where(
        '        Function(x) x.value.Contains(q) Or x.text.Contains(q)
        '    ).ToList()
        'End If

        Dim tmpDT As DataTable = GetKodKOList(q, kodkw)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetKodKOList(kod As String, kodkw As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT DISTINCT A.Kod_Operasi as value,B.Butiran as text
                                FROM SMKB_COA_Master A
                                INNER JOIN SMKB_Operasi B ON A.Kod_Operasi=B.Kod_Operasi"
        Dim param As New List(Of SqlParameter)
        If kodkw <> "" Then
            query &= " WHERE A.Kod_Kump_Wang = @kodkw "
            param.Add(New SqlParameter("@kodkw", kodkw))
        End If
        If kod <> "" Then
            query &= " WHERE A.Kod_Operasi LIKE '%' + @kod + '%' OR B.Butiran LIKE '%' + @kod2 + '%' "
            param.Add(New SqlParameter("@kod", kod))
            param.Add(New SqlParameter("@kod2", kod))
        End If

        Return db.Read(query, param)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetCarianVotList(ByVal q As String) As String
        'Dim newList As List(Of ItemList)
        'newList = listItem

        'If (q <> "") Then
        '    newList = listItem.Where(
        '        Function(x) x.value.Contains(q) Or x.text.Contains(q)
        '    ).ToList()
        'End If

        Dim tmpDT As DataTable = GetCarianKodVotList(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetCarianKodVotList(kodCariVot As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT CONCAT(a.Kod_Vot, ' - ', vot.Butiran, ', ', a.Kod_Operasi, ' - ', ko.Butiran, ', ', a.Kod_Projek, ', ', a.Kod_Kump_Wang, ' - ', REPLACE(kw.Butiran, 'KUMPULAN WANG', 'KW'), ', ', a.Kod_PTJ) AS text,
                            a.Kod_Kump_Wang + a.Kod_Operasi + a.Kod_PTJ + a.Kod_Projek + a.Kod_Vot AS value 
                            FROM SMKB_COA_Master AS a
                            JOIN SMKB_Vot AS vot ON a.Kod_Vot = vot.Kod_Vot
                            JOIN SMKB_Operasi AS ko ON a.Kod_Operasi = ko.Kod_Operasi
                            JOIN SMKB_Kump_Wang AS kw ON a.Kod_Kump_Wang = kw.Kod_Kump_Wang
                            WHERE a.status = 1 "

        Dim param As New List(Of SqlParameter)
        If kodCariVot <> "" Then
            query &= "AND (a.Kod_Vot LIKE '%' + @kod + '%' OR a.Kod_Operasi LIKE '%' + @kod2 + '%' OR a.Kod_Projek LIKE '%' + @kod3 + '%' OR a.Kod_Kump_Wang LIKE '%' + @kod4 + '%' OR a.Kod_PTJ LIKE '%' + @kod5 + '%' OR vot.Butiran LIKE '%' + @kodButir + '%' OR ko.Butiran LIKE '%' + @kodButir1 + '%'  OR kw.Butiran LIKE '%' + @kodButir2 + '%')"

            param.Add(New SqlParameter("@kod", kodCariVot))
            param.Add(New SqlParameter("@kod2", kodCariVot))
            param.Add(New SqlParameter("@kod3", kodCariVot))
            param.Add(New SqlParameter("@kod4", kodCariVot))
            param.Add(New SqlParameter("@kod5", kodCariVot))
            param.Add(New SqlParameter("@kodButir", kodCariVot))
            param.Add(New SqlParameter("@kodButir1", kodCariVot))
            param.Add(New SqlParameter("@kodButir2", kodCariVot))
        End If

        Return db.Read(query, param)
    End Function

    '<WebMethod()>
    '<ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    'Public Function DeleteOrder(ByVal id As String) As String
    '    Dim resp As New ResponseRepository

    '    'DeleteOrderDetails("", id)


    '    If DeleteOrderRecord(id) <> "OK" Then
    '        resp.Failed("Gagal")
    '        Return JsonConvert.SerializeObject(resp.GetResult())
    '    End If

    '    resp.Success("Order telah dihapuskan")
    '    Return JsonConvert.SerializeObject(resp.GetResult())
    'End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function DeleteRecord(ByVal id As String) As String
        Dim resp As New ResponseRepository
        If DeleteOrderDetails(id) = "OK" Then
            resp.Success("Berjaya")
        Else
            resp.Failed("Gagal")
        End If


        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadOrderRecord(ByVal id As String) As String
        Dim resp As New ResponseRepository

        If id = "" Then
            resp.Failed("ID diperlukan")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        'Dim record As Order = listOfOrder.Where(Function(x) x.OrderID = id).FirstOrDefault

        'If IsNothing(record) Then
        '    resp.Failed("Rekod tidak dijumpai")
        '    Return JsonConvert.SerializeObject(resp.GetResult())
        'End If

        dt = GetOrder(id)
        resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    '<WebMethod()>
    '<ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    'Public Function LoadRecord_SenaraiUrusniaga(ByVal id As String) As String
    '    Dim resp As New ResponseRepository

    '    If id = "" Then
    '        resp.Failed("ID diperlukan")
    '        Return JsonConvert.SerializeObject(resp.GetResult())
    '    End If

    '    'Dim record As Order = listOfOrder.Where(Function(x) x.OrderID = id).FirstOrDefault

    '    'If IsNothing(record) Then
    '    '    resp.Failed("Rekod tidak dijumpai")
    '    '    Return JsonConvert.SerializeObject(resp.GetResult())
    '    'End If

    '    dt = GetSenaraiUrusniaga(id)
    '    resp.SuccessPayload(dt)

    '    Return JsonConvert.SerializeObject(resp.GetResult())
    'End Function

    'Private Function GetSenaraiUrusniaga(kod As String) As DataTable
    '    Dim db = New DBKewConn

    '    'tentukan jenis urusniaga
    '    If kod = "10" Then
    '        Dim query As String = "SELECT No_Bil,Kod_Penghutang,Tkh_Lulus,Tujuan,Jumlah 
    '                            FROM SMKB_Bil_Hdr 
    '                            WHERE Kod_Status_Dok='03' "
    '        Dim param As New List(Of SqlParameter)
    '        'If kod <> "" Then
    '        '    param.Add(New SqlParameter("@kod", kod))
    '        'End If

    '        Return db.Read(query, param)
    '    End If
    'End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadRecord_SenaraiUrusniaga(ByVal id As String) As String
        Dim resp As New ResponseRepository

        If id = "" Then
            resp.Failed("ID diperlukan")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        'Dim record As Order = listOfOrder.Where(Function(x) x.OrderID = id).FirstOrDefault

        'If IsNothing(record) Then
        '    resp.Failed("Rekod tidak dijumpai")
        '    Return JsonConvert.SerializeObject(resp.GetResult())
        'End If

        dt = GetSenaraiUrusniaga(id)
        resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    Private Function GetSenaraiUrusniaga(kod As String) As DataTable
        Dim db = New DBKewConn

        'tentukan jenis urusniaga
        If kod = "10" Then
            Dim query As String = "SELECT No_Bil,Kod_Penghutang,CASE WHEN Tkh_Lulus <> '' THEN CONVERT(VARCHAR,Tkh_Lulus, 103) END AS Tkh_Lulus,Tujuan,Jumlah 
                                FROM SMKB_Bil_Hdr 
                                WHERE Kod_Status_Dok='03' "
            Dim param As New List(Of SqlParameter)
            'If kod <> "" Then
            '    param.Add(New SqlParameter("@kod", kod))
            'End If

            Return db.Read(query, param)
        End If
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadOrderRecord_SenaraiLulusInvois(category_filter As String, isClicked As Boolean, tkhMula As DateTime, tkhTamat As DateTime) As String
        Dim resp As New ResponseRepository

        If isClicked = False Then
            Return JsonConvert.SerializeObject(New DataTable)
        End If

        dt = GetOrder_SenaraiTransaksiInvois(category_filter, tkhMula, tkhTamat)

        Return JsonConvert.SerializeObject(dt)
    End Function
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetOrder_SenaraiTransaksiInvois(category_filter As String, tkhMula As DateTime, tkhTamat As DateTime) As DataTable
        Dim db = New DBKewConn
        Dim tarikhQuery As String = ""
        Dim param As List(Of SqlParameter) = New List(Of SqlParameter)

        If category_filter = "1" Then 'Harini
            tarikhQuery = " AND CAST(A.Tkh_Mohon AS DATE) = CAST(CURRENT_TIMESTAMP AS DATE) "
        ElseIf category_filter = "2" Then 'Semalam
            tarikhQuery = " AND CAST(A.Tkh_Mohon AS DATE) = CAST(DATEADD(day, -1, CURRENT_TIMESTAMP) AS DATE) "
        ElseIf category_filter = "3" Then 'seminggu
            tarikhQuery = " AND A.Tkh_Mohon >= DATEADD(day, -7, CURRENT_TIMESTAMP) AND A.Tkh_Mohon < CURRENT_TIMESTAMP "
        ElseIf category_filter = "4" Then '30 hari
            tarikhQuery = " AND A.Tkh_Mohon >= DATEADD(day, -30, CURRENT_TIMESTAMP) AND A.Tkh_Mohon < CURRENT_TIMESTAMP "
        ElseIf category_filter = "5" Then '60 hari
            tarikhQuery = " AND A.Tkh_Mohon >= DATEADD(day, -60, CURRENT_TIMESTAMP) AND A.Tkh_Mohon < CURRENT_TIMESTAMP "
        ElseIf category_filter = "6" Then 'custom
            tarikhQuery = " AND A.Tkh_Mohon >= @tkhMula AND A.Tkh_Mohon <= @TkhTamat "
            param.Add(New SqlParameter("@tkhMula", tkhMula))
            param.Add(New SqlParameter("@TkhTamat", tkhTamat))
        End If

        Dim query As String = "SELECT DISTINCT A.Kod_Penghutang AS NoAkaun,B.Nama_Penghutang,B.No_Rujukan,
                                    (B.Alamat_1 + ',' + B.Alamat_2 + ',' + B.Poskod + ',' + B.Bandar + ',' + D.Butiran + ',' + E.Butiran) AS ALAMAT, B.Tel_Bimbit
                                    FROM SMKB_Bil_Hdr A
                                    INNER JOIN SMKB_Penghutang_Master B ON A.Kod_Penghutang=B.Kod_Penghutang
                                    INNER JOIN SMKB_Lookup_Detail D ON D.Kod='0002' AND B.Kod_Negeri=D.Kod_Detail
                                    INNER JOIN SMKB_Lookup_Detail E ON E.Kod='0001' AND B.Kod_Negara=E.Kod_Detail
                                    WHERE Kod_Status_Dok NOT IN ('01','02') " & tarikhQuery

        Return db.Read(query, param)
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadOrderRecord_SenaraiLulusTerima(category_filter As String, isClicked As Boolean, tkhMula As DateTime, tkhTamat As DateTime) As String
        Dim resp As New ResponseRepository

        If isClicked = False Then
            Return JsonConvert.SerializeObject(New DataTable)
        End If

        dt = GetOrder_SenaraiLulusTerima(category_filter, tkhMula, tkhTamat)

        Return JsonConvert.SerializeObject(dt)
    End Function
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetOrder_SenaraiLulusTerima(category_filter As String, tkhMula As DateTime, tkhTamat As DateTime) As DataTable
        Dim db = New DBKewConn
        Dim tarikhQuery As String = ""
        Dim param As List(Of SqlParameter)

        If category_filter = "1" Then 'Harini
            tarikhQuery = " AND CAST(A.Tkh_Daftar AS DATE) = CAST(CURRENT_TIMESTAMP AS DATE) "
        ElseIf category_filter = "2" Then 'Semalam
            tarikhQuery = " AND CAST(A.Tkh_Daftar AS DATE) = CAST(DATEADD(day, -1, CURRENT_TIMESTAMP) AS DATE) "
        ElseIf category_filter = "3" Then 'seminggu
            tarikhQuery = " AND A.Tkh_Daftar >= DATEADD(day, -7, CURRENT_TIMESTAMP) AND A.Tkh_Daftar < CURRENT_TIMESTAMP "
        ElseIf category_filter = "4" Then '30 hari
            tarikhQuery = " AND A.Tkh_Daftar >= DATEADD(day, -30, CURRENT_TIMESTAMP) AND A.Tkh_Daftar < CURRENT_TIMESTAMP "
        ElseIf category_filter = "5" Then '60 hari
            tarikhQuery = " AND A.Tkh_Daftar >= DATEADD(day, -60, CURRENT_TIMESTAMP) AND A.Tkh_Daftar < CURRENT_TIMESTAMP "
        ElseIf category_filter = "6" Then 'custom
            tarikhQuery = " AND A.Tkh_Daftar >= @tkhMula AND A.Tkh_Daftar <= @TkhTamat "
            param = New List(Of SqlParameter)
            param.Add(New SqlParameter("@tkhMula", tkhMula))
            param.Add(New SqlParameter("@TkhTamat", tkhTamat))
        End If

        Dim query As String = "SELECT A.No_Dok ,B.Nama_Penghutang,C.Butiran as UrusNiaga,A.Tujuan,
                                CASE WHEN A.Tkh_Daftar <> '' THEN FORMAT(A.Tkh_Daftar, 'dd/MM/yyyy') END AS Tkh_Daftar,
                                A.Jumlah_Bayar,E.Butiran AS MOD_TERIMA, (SELECT COUNT(No_Item) FROM SMKB_Terima_Dtl WHERE No_Dok=A.No_Dok) AS BILITEM,
                                F.MS01_Nama
                                FROM SMKB_Terima_Hdr A
                                LEFT JOIN SMKB_Penghutang_Master B ON A.Kod_Penghutang=B.Kod_Penghutang
                                LEFT JOIN SMKB_Kod_Urusniaga C ON A.Kod_Urusniaga=C.Kod_Urusniaga
                                LEFT JOIN SMKB_Lookup_Detail E ON E.Kod='AR01' AND REPLACE(A.Mod_Terima,'0','')= E.Kod_Detail
                                LEFT JOIN [DEVMIS\SQL_INS01].dbStaf.dbo.MS01_Peribadi as F on F.MS01_NoStaf = A.Staf_Terima
                                WHERE  A.Status='1' AND A.Kod_Status_Dok='04'" & tarikhQuery

        Return db.Read(query)
    End Function


    <WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function Load_MaklumatBilTerperinci(category_filter As String, isClicked As Boolean, tkhMula As DateTime, tkhTamat As DateTime, custcode As String, status As String) As String
        Dim resp As New ResponseRepository

        'If isClicked = False Then
        '    Return JsonConvert.SerializeObject(New DataTable)
        'End If

        dt = Get_MaklumatBilTerperinci(category_filter, tkhMula, tkhTamat, custcode, status)

        Return JsonConvert.SerializeObject(dt)
    End Function
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function Get_MaklumatBilTerperinci(category_filter As String, tkhMula As DateTime, tkhTamat As DateTime, custcode As String, status As String) As DataTable
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

        If status = "03" Then
            query = "SELECT A.No_Bil AS No_Invois,
                        B.Nama_Penghutang,
                        C.Butiran AS UrusNiaga,
                        A.Tujuan,
                        FORMAT(A.Tkh_Mohon, 'dd/MM/yyyy') AS TKHMOHON,
                        CASE WHEN Tkh_Mula <> '' THEN FORMAT(Tkh_Mula, 'dd/MM/yyyy') END AS Tkh_Mula,
                        CASE WHEN Tkh_Tamat <> '' THEN FORMAT(Tkh_Tamat, 'dd/MM/yyyy') END AS Tkh_Tamat,
                        A.Jumlah,
                        Terima.Max_Jumlah_Sebenar,
                        Terima.Sum_Jumlah_Bayar
                    FROM SMKB_Bil_Hdr A
                    INNER JOIN SMKB_Penghutang_Master B ON A.Kod_Penghutang = B.Kod_Penghutang
                    INNER JOIN SMKB_Kod_Urusniaga C ON A.Kod_Urusniaga = C.Kod_Urusniaga
                    LEFT JOIN (
                        SELECT No_Rujukan, MAX(Jumlah_Sebenar) AS Max_Jumlah_Sebenar, SUM(Jumlah_Bayar) AS Sum_Jumlah_Bayar
                        FROM SMKB_Terima_Hdr
                        WHERE Kod_Penghutang = @kodpenghutang
                        GROUP BY No_Rujukan
                    ) Terima ON A.No_Bil = Terima.No_Rujukan
                    WHERE A.Status = '1' AND A.Kod_Status_Dok = '03' AND A.Kod_Penghutang = @kodpenghutang
                    AND (Terima.Sum_Jumlah_Bayar != A.Jumlah OR Terima.No_Rujukan IS NULL);"
            param.Add(New SqlParameter("@kodpenghutang", custcode))

        Else 'Dah bayar
            query = "Select A.No_Dok As No_Invois,
                        B.Nama_Penghutang,
                        C.Butiran As UrusNiaga,
                        A.Tujuan,
                        FORMAT(A.Tkh_Daftar, 'dd/MM/yyyy') AS TKHMOHON,
                        A.Jumlah_Bayar as Jumlah
                    FROM SMKB_Terima_Hdr A
                    INNER JOIN SMKB_Penghutang_Master B ON A.Kod_Penghutang = B.Kod_Penghutang
                    INNER JOIN SMKB_Kod_Urusniaga C ON A.Kod_Urusniaga = C.Kod_Urusniaga
                    WHERE A.Status = '1' AND B.Kod_Penghutang = @kodpenghutang;"

            param.Add(New SqlParameter("@kodpenghutang", custcode))

        End If

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

        Dim query As String = "SELECT D.No_Dok as No_Bil, A.Kod_Penghutang, B.Nama_Penghutang, A.Kod_Urusniaga, C.Butiran, A.Kontrak, A.Tujuan,
                                    CASE WHEN A.Tkh_Mula <> '' THEN FORMAT(A.Tkh_Mula, 'dd/MM/yyyy') END AS Tkh_Mula,
                                    CASE WHEN A.Tkh_Tamat <> '' THEN FORMAT(A.Tkh_Tamat, 'dd/MM/yyyy') END AS Tkh_Tamat, D.Jumlah_Bayar
                                FROM SMKB_Bil_Hdr A
                                INNER JOIN SMKB_Penghutang_Master B ON A.Kod_Penghutang = B.Kod_Penghutang
                                LEFT JOIN SMKB_Kod_Urusniaga C ON A.Kod_Urusniaga = C.Kod_Urusniaga
                                INNER JOIN SMKB_Terima_Hdr D ON A.No_Bil = D.No_Rujukan
                                WHERE D.No_Dok = @No_Dok AND A.Status = '1';"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@No_Dok", id))

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

        Dim jumlahBayar As Decimal = GetJumlaBayarByNoDokFromTerimaHdr(kod)
        Dim query As String = "SELECT 
                A.No_Bil + '|' + B.No_Item AS dataid,
                B.Kod_Kump_Wang AS colhidkw,
                (SELECT Butiran FROM SMKB_Kump_Wang AS kw WHERE B.Kod_Kump_Wang = kw.Kod_Kump_Wang) AS colKW,
                B.Kod_Operasi AS colhidko,
                (SELECT Butiran FROM SMKB_Operasi AS ko WHERE B.Kod_Operasi = ko.Kod_Operasi) AS colKO,
                B.Kod_Projek AS colhidkp,
                (SELECT Butiran FROM SMKB_Projek AS kp WHERE B.Kod_Projek = kp.Kod_Projek) AS colKp,
                B.Kod_PTJ AS colhidptj,
                (SELECT F.Pejabat FROM [DEVMIS\SQL_INS01].dbStaf.dbo.MS_PEJABAT AS F
                    WHERE F.STATUS = 1 AND F.kodpejabat = LEFT(B.Kod_PTJ, 2)) AS ButiranPTJ,
                B.Kod_Vot,
                (SELECT Kod_Vot + ' - ' + Butiran FROM SMKB_Vot AS vot WHERE B.Kod_Vot = vot.Kod_Vot) AS ButiranVot,
                Perkara,
                Kuantiti,
                Kadar_Harga,
                B.Jumlah,
                D.Kredit AS Jumlah_Bayar,
                Diskaun,
                Cukai
                FROM SMKB_Bil_Hdr A
                INNER JOIN SMKB_Bil_Dtl B ON A.No_Bil=B.No_Bil
                INNER JOIN SMKB_Terima_Hdr C ON B.No_Bil=C.No_Rujukan
                INNER JOIN SMKB_Terima_Dtl D ON C.No_Dok=D.No_Dok AND B.No_Item=D.No_Item
                WHERE C.No_Dok=@No_Invois"
        'Dim query As String =
        '    "DECLARE @Jumlah_Bayar DECIMAL(18, 2) -- Declare the variable to store Jumlah_Bayar
        '    DECLARE @remaining DECIMAL(18, 2) -- Declare the variable for remaining
        '    SET @Jumlah_Bayar = @jumlahBayar -- Set the initial value
        '    SET @remaining = @Jumlah_Bayar; -- Initialize remaining with the initial value

        '    -- Create a temporary table to store the intermediate results
        '    CREATE TABLE #TempResults (
        '        dataid NVARCHAR(MAX),
        '        colhidkw NVARCHAR(MAX),
        '        colKW NVARCHAR(MAX),
        '        colhidko NVARCHAR(MAX),
        '        colKO NVARCHAR(MAX),
        '        colhidkp NVARCHAR(MAX),
        '        colKp NVARCHAR(MAX),
        '        colhidptj NVARCHAR(MAX),
        '        ButiranPTJ NVARCHAR(MAX),
        '        Kod_Vot NVARCHAR(MAX),
        '        ButiranVot NVARCHAR(MAX),
        '        Perkara NVARCHAR(MAX),
        '        Kuantiti DECIMAL(18, 2),
        '        Kadar_Harga DECIMAL(18, 2),
        '        Jumlah DECIMAL(18, 2),
        '        Diskaun DECIMAL(18, 2),
        '        Cukai DECIMAL(18, 2),
        '        Jumlah_Bayar DECIMAL(18, 2), -- Add Jumlah_Bayar column
        '        RowNum INT,
        '        NoItem INT
        '    );

        '    INSERT INTO #TempResults (
        '        dataid,
        '        colhidkw,
        '        colKW,
        '        colhidko,
        '        colKO,
        '        colhidkp,
        '        colKp,
        '        colhidptj,
        '        ButiranPTJ,
        '        Kod_Vot,
        '        ButiranVot,
        '        Perkara,
        '        Kuantiti,
        '        Kadar_Harga,
        '        Jumlah,
        '        Diskaun,
        '        Cukai,
        '        RowNum,
        '     NoItem
        '    )
        '    SELECT
        '        a.No_Bil + '|' + a.No_Item AS dataid,
        '        Kod_Kump_Wang AS colhidkw,
        '        (SELECT Butiran FROM SMKB_Kump_Wang AS kw WHERE a.Kod_Kump_Wang = kw.Kod_Kump_Wang) AS colKW,
        '        Kod_Operasi AS colhidko,
        '        (SELECT Butiran FROM SMKB_Operasi AS ko WHERE a.Kod_Operasi = ko.Kod_Operasi) AS colKO,
        '        Kod_Projek AS colhidkp,
        '        (SELECT Butiran FROM SMKB_Projek AS kp WHERE a.Kod_Projek = kp.Kod_Projek) AS colKp,
        '        Kod_PTJ AS colhidptj,
        '        (SELECT b.Pejabat FROM [DEVMIS\SQL_INS01].dbStaf.dbo.MS_PEJABAT AS b
        '         WHERE b.STATUS = 1 AND b.kodpejabat = LEFT(Kod_PTJ, 2)) AS ButiranPTJ,
        '        Kod_Vot,
        '        (SELECT Kod_Vot + ' - ' + Butiran FROM SMKB_Vot AS vot WHERE a.Kod_Vot = vot.Kod_Vot) AS ButiranVot,
        '        Perkara,
        '        Kuantiti,
        '        Kadar_Harga,
        '        Jumlah,
        '        Diskaun,
        '        Cukai,
        '        ROW_NUMBER() OVER (PARTITION BY a.No_Item ORDER BY a.No_Item) AS RowNum, -- Add a row number for iteration,
        '        a.No_Item -- Include No_Item in the SELECT list
        '    FROM SMKB_Bil_Dtl AS a
        '    LEFT JOIN SMKB_Terima_Hdr AS h ON a.No_Bil = h.No_Rujukan
        '    WHERE h.No_Dok = @No_Invois
        '        AND a.status = 1
        '    GROUP BY
        '        a.No_Bil + '|' + a.No_Item, -- Use the expression directly in GROUP BY
        '        Kod_Kump_Wang,
        '        Kod_Operasi,
        '        Kod_Projek,
        '        Kod_PTJ,
        '        Kod_Vot,
        '        Perkara,
        '        Kuantiti,
        '        Kadar_Harga,
        '        Jumlah,
        '        Diskaun,
        '        Cukai,
        '        a.No_Item; -- Include No_Item in GROUP BY

        '    -- Declare a cursor to loop through the rows
        '    DECLARE @dataid NVARCHAR(MAX);
        '    DECLARE @payment DECIMAL(18, 2);

        '    DECLARE paymentCursor CURSOR FOR
        '    SELECT dataid, Jumlah
        '    FROM #TempResults
        '    ORDER BY RowNum;

        '    -- Open the cursor
        '    OPEN paymentCursor;

        '    -- Fetch the first row
        '    FETCH NEXT FROM paymentCursor INTO @dataid, @payment;

        '    -- Loop through the rows and update Jumlah_Bayar and @remaining
        '    WHILE @@FETCH_STATUS = 0
        '    BEGIN
        '        IF @remaining >= @payment
        '        BEGIN
        '            UPDATE #TempResults
        '            SET Jumlah_Bayar = @payment
        '            WHERE dataid = @dataid;

        '            SET @remaining = @remaining - @payment;
        '        END
        '        ELSE
        '        BEGIN
        '            IF @remaining > 0
        '            BEGIN
        '                UPDATE #TempResults
        '                SET Jumlah_Bayar = @remaining
        '                WHERE dataid = @dataid;

        '                SET @remaining = 0.00;
        '            END
        '            ELSE
        '            BEGIN
        '                UPDATE #TempResults
        '                SET Jumlah_Bayar = 0.00
        '                WHERE dataid = @dataid;
        '            END
        '        END

        '        -- Fetch the next row
        '        FETCH NEXT FROM paymentCursor INTO @dataid, @payment;
        '    END

        '    -- Close and deallocate the cursor
        '    CLOSE paymentCursor;
        '    DEALLOCATE paymentCursor;

        '    -- Select the final results
        '    SELECT
        '        dataid,
        '        colhidkw,
        '        colKW,
        '        colhidko,
        '        colKO,
        '        colhidkp,
        '        colKp,
        '        colhidptj,
        '        ButiranPTJ,
        '        Kod_Vot,
        '        ButiranVot,
        '        Perkara,
        '        Kuantiti,
        '        Kadar_Harga,
        '        Jumlah,
        '        Jumlah_Bayar,
        '        Diskaun,
        '        Cukai
        '    FROM #TempResults;

        '    -- Drop the temporary table
        '    DROP TABLE #TempResults;"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@jumlahBayar", jumlahBayar))
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
    Public Function SaveOrders(order As Order_terima) As String
        Dim resp As New ResponseRepository
        resp.Success("Data telah disimpan")
        Dim success As Integer = 0
        Dim JumRekod As Integer = 0
        If order Is Nothing Then
            resp.Failed("Tidak disimpan")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        If order.OrderID = "" Then
            order.OrderID = GenerateOrderID()
            If InsertNewOrder(order) <> "OK" Then
                resp.Failed("Gagal Menyimpan rekod")
                Exit Function
            Else
                If InsertTerimaBil(order) <> "OK" Then
                    resp.Failed("Gagal Menyimpan rekod")
                    Exit Function
                End If
            End If
            'If updatestatusbil(order) <> "OK" Then
            '    resp.Failed("Gagal Menyimpan rekod")
            '    Exit Function
            'End If

        Else
            dt = GetOrder(order.OrderID)
            If dt.Rows.Count = 0 Then
                resp.Failed("Nombor order tidak ditemui")
                Return False
            End If
        End If



        For Each orderDetail As OrderDetail_inv In order.OrderDetails

            If orderDetail.ddlPTJ = "" Then
                Continue For
            End If

            JumRekod += 1

            orderDetail.amount = orderDetail.quantity * orderDetail.price 'This can be automated inside orderdetail model

            Dim data
            data = Split(orderDetail.id, "|")
            Dim nobil = data(0)
            orderDetail.id = data(1)

            If orderDetail.id = "" Then
                orderDetail.id = GenerateOrderDetailID(order.OrderID)
                orderDetail.OrderID = order.OrderID
                'If InsertOrderDetail(order, orderDetail) = "OK" Then
                '    success += 1
                'End If
            Else
                'If UpdateOrderDetail(orderDetail) = "OK" Then
                '    success += 1
                'End If
                If InsertOrderDetail(order, orderDetail) = "OK" Then
                    'success += 1
                End If
                'If InsertOrderDetailTrans(order, orderDetail) = "OK" Then
                '    success += 1
                'End If
            End If
        Next

        UpdateOrderDetailTerimaan(order)
        UpdateStatusHeaderTerima(order)

        'UpdateOrderDetailBilStatus(order)
        UpdateOrderDetailBilStatusNew(order)

        If success = 0 Then
            resp.Failed("Rekod order detail gagal disimpan")
        End If

        If Not success = JumRekod Then
            resp.Success("Rekod order detail berjaya disimpan dengan beberapa rekod tidak disimpan", "00", order)
        Else
            resp.Success("Rekod berjaya disimpan", "00", order)
        End If

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    Private Function InsertNewOrder(order As Order_terima)
        Dim db As New DBKewConn
        Dim parsedDate As Date = CDate(Now()).ToString("yyyy-MM-dd")
        Dim query As String = "INSERT INTO SMKB_Terima_Hdr (No_Dok, Kod_Penghutang, No_Rujukan,Tujuan,Kod_Urusniaga,Mod_Terima,Tkh_Daftar,Staf_Terima,Jumlah_Sebenar,Jumlah_Bayar,Kod_Bank,Kod_Terima,Kod_Status_Dok)
                                SELECT @noresit,Kod_Penghutang,No_Bil,Tujuan,Kod_Urusniaga,@modterima,@tarikh,@No_Staf,Jumlah,@jumlahterima,@kodbank,'TK','04'
                                FROM SMKB_Bil_Hdr
                                WHERE No_Bil = @nobil"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@noresit", order.OrderID))
        param.Add(New SqlParameter("@modterima", order.ModTerima))
        param.Add(New SqlParameter("@tarikh", parsedDate))
        param.Add(New SqlParameter("@jumlahterima", order.JumlahTerima))
        param.Add(New SqlParameter("@kodbank", order.KodBank))
        param.Add(New SqlParameter("@nobil", order.NoBil))
        param.Add(New SqlParameter("@No_Staf", Session("ssusrID")))

        Return db.Process(query, param)
    End Function


    Private Function InsertTerimaBil(order As Order_terima)
        Dim db As New DBKewConn
        Dim parsedDate As Date = CDate(Now()).ToString("yyyy-MM-dd")
        Dim id As String = order.OrderID + order.NoBil
        Dim query As String = "INSERT INTO SMKB_Terima_Bil VALUES (@id,@noresit,@nobil,@tkh,@status)"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@id", id))
        param.Add(New SqlParameter("@noresit", order.OrderID))
        param.Add(New SqlParameter("@nobil", order.NoBil))
        param.Add(New SqlParameter("@tkh", parsedDate))
        param.Add(New SqlParameter("@status", "1"))

        Return db.Process(query, param)
    End Function

    Private Function updatestatusbil(order As Order_terima)
        Dim db As New DBKewConn
        Dim query As String = "UPDATE SMKB_Bil_Hdr
                                SET Kod_Status_Dok = '04'
                                where No_Bil=@nobil"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@nobil", order.NoBil))

        Return db.Process(query, param)
    End Function


    Private Function InsertNewOrderDetails(order As Order_terima)
        Dim db As New DBKewConn
        Dim parsedDate As Date = CDate(Now()).ToString("yyyy-MM-dd")
        Dim query As String = "INSERT INTO SMKB_Terima_Hdr (No_Dok, Kod_Penghutang, No_Rujukan,Tujuan,Kod_Urusniaga,Mod_Terima,Tkh_Daftar,Staf_Terima,Jumlah_Sebenar,Jumlah_Bayar,Kod_Bank,Kod_Terima)
                                SELECT @noresit,Kod_Penghutang,No_Bil,Tujuan,Kod_Urusniaga,@modterima,@tarikh,No_Staf,Jumlah,@jumlahterima,@kodbank,'TP'
                                FROM SMKB_Bil_Hdr
                                WHERE No_Bil = @nobil "
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@noresit", order.OrderID))
        param.Add(New SqlParameter("@modterima", order.ModTerima))
        param.Add(New SqlParameter("@tarikh", parsedDate))
        param.Add(New SqlParameter("@jumlahterima", order.JumlahTerima))
        param.Add(New SqlParameter("@kodbank", order.KodBank))
        param.Add(New SqlParameter("@nobil", order.NoBil))

        Return db.Process(query, param)
    End Function

    Public Function InsertOrderDetail(order As Order_terima, orderDetail As OrderDetail_inv)
        Dim db = New DBKewConn
        'Dim query As String = "INSERT INTO SMKB_Terima_Dtl (No_Dok, No_Item, Kod_Kump_Wang, Kod_Operasi, Kod_PTJ, Kod_Projek, Kod_Vot, Butiran, Debit, Kredit, Cukai, Status)
        '                        VALUES (@noresit, @no_item, @Kod_Kump_Wang, @Kod_Operasi ,@Kod_PTJ ,@Kod_Projek ,@Kod_Vot, @Perkara,'', '0.00', '0.00', @Jumlah, @Cukai, 1)"

        Dim query As String = "INSERT INTO SMKB_Terima_Dtl
                                VALUES( @noresit , @no_item, @Kod_Kump_Wang, @Kod_Operasi , @Kod_PTJ , @Kod_Projek  , @Kod_Vot, @Perkara,'','0.00', '0.00', @Cukai,@diskaun,'1')"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@noresit", order.OrderID))
        param.Add(New SqlParameter("@no_item", orderDetail.id))
        param.Add(New SqlParameter("@Kod_Kump_Wang", orderDetail.ddlKw))
        param.Add(New SqlParameter("@Kod_Operasi", orderDetail.ddlKo))
        param.Add(New SqlParameter("@Kod_PTJ", orderDetail.ddlPTJ))
        param.Add(New SqlParameter("@Kod_Projek", orderDetail.ddlKp))
        param.Add(New SqlParameter("@Kod_Vot", orderDetail.ddlVot))
        param.Add(New SqlParameter("@Perkara", orderDetail.details))
        'param.Add(New SqlParameter("@Jumlah", orderDetail.amount))
        param.Add(New SqlParameter("@Cukai", orderDetail.Cukai))
        param.Add(New SqlParameter("@diskaun", orderDetail.Diskaun))


        Return db.Process(query, param)
    End Function

    Public Function InsertOrderDetailTrans(order As Order_terima, orderDetail As OrderDetail_inv)
        Dim db = New DBKewConn
        Dim parsedDate As Date = CDate(Now()).ToString("yyyy-MM-dd")
        Dim query As String = "INSERT INTO SMKB_Terima_Transaksi
        VALUES( @noresit ,@nobil, @no_item,@tkhterima,@amaunterima,'','','','', @diskaun,@Cukai,@Kod_Kump_Wang, @Kod_Operasi , @Kod_PTJ , @Kod_Projek  , @Kod_Vot, '1') "
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@noresit", order.OrderID))
        param.Add(New SqlParameter("@no_item", orderDetail.id))
        param.Add(New SqlParameter("@Kod_Kump_Wang", orderDetail.ddlKw))
        param.Add(New SqlParameter("@Kod_Operasi", orderDetail.ddlKo))
        param.Add(New SqlParameter("@Kod_PTJ", orderDetail.ddlPTJ))
        param.Add(New SqlParameter("@Kod_Projek", orderDetail.ddlKp))
        param.Add(New SqlParameter("@Kod_Vot", orderDetail.ddlVot))
        param.Add(New SqlParameter("@amaunterima", orderDetail.amount))
        param.Add(New SqlParameter("@diskaun", orderDetail.Diskaun))
        param.Add(New SqlParameter("@Cukai", orderDetail.Cukai))
        param.Add(New SqlParameter("@tkhterima", parsedDate))
        param.Add(New SqlParameter("@nobil", order.NoBil))


        Return db.Process(query, param)
    End Function

    Public Function UpdateOrderDetailTerimaan(order As Order_terima)
        Dim db = New DBKewConn

        ' Create a SQL command for selecting rows to update
        'Dim selectQuery As String = "SELECT No_Item, Kredit FROM SMKB_Terima_Dtl WHERE No_Dok = @noresit ORDER BY No_Item"
        Dim selectQuery As String = "SELECT B.No_Item, SUM(Kredit) AS Kredit,C.Jumlah
                                    FROM SMKB_Terima_Hdr A 
                                    INNER JOIN SMKB_Terima_Dtl AS B ON A.No_Dok=B.No_Dok
                                    INNER JOIN SMKB_Bil_Dtl AS C ON A.No_Rujukan=C.No_Bil AND B.No_Item=C.No_Item
                                    WHERE A.No_Rujukan = @nobil
                                    GROUP BY B.No_Item,C.Jumlah
                                    ORDER BY No_Item"
        '"SELECT B.No_Item, B.Kredit FROM 
        'SMKB_Terima_Hdr AS A 
        'INNER JOIN SMKB_Terima_Dtl AS B ON A.No_Dok=B.No_Dok
        'INNER JOIN SMKB_Bil_Dtl AS C ON B.No_Item=C.No_Item AND C.No_Bil=A.No_Rujukan AND Status_Bayaran<>'BP'
        'WHERE A.No_Dok = @noresit ORDER BY No_Item"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@nobil", order.NoBil))

        dt = db.Read(selectQuery, param)

        Dim totalKredit As String = order.JumlahTerima
        Dim jumlahSebenar As Decimal = GetJumlahSebenarByNoResit(order.OrderID)
        Dim remainingKredit As Decimal = totalKredit
        Dim Totalpaid As Decimal

        Using connection As New SqlConnection(db.ConnectionString)
            connection.Open()

            ' Iterate through the rows to update
            For Each row As DataRow In dt.Rows
                Totalpaid = GetJumlahSebenarByNoBil(order.NoBil, order.OrderID)
                Dim noItem As String = row.Field(Of String)("No_Item")
                Dim currentKredit As Decimal = row.Field(Of Decimal)("Kredit")
                Dim jumlahperitem As Decimal = row.Field(Of Decimal)("Jumlah")
                Dim updateKredit As Decimal
                Dim balancedbyitem As Decimal
                If remainingKredit > 0 AndAlso Totalpaid < jumlahSebenar Then
                    If currentKredit = 0 AndAlso remainingKredit < jumlahperitem Then
                        updateKredit = remainingKredit
                    Else
                        balancedbyitem = jumlahperitem - currentKredit
                        If balancedbyitem > remainingKredit Then
                            updateKredit = remainingKredit
                        Else
                            updateKredit = jumlahperitem - currentKredit
                        End If
                    End If


                    ' Math.Min(remainingKredit, jumlahSebenar - currentKredit)
                    ' Update the Debit column for the current row
                    Dim updateQuery As String = "UPDATE SMKB_Terima_Dtl SET Kredit = Kredit + @UpdateKredit WHERE No_Dok = @noresit AND No_Item = @NoItem"
                    Using updateCommand As New SqlCommand(updateQuery, connection)
                        updateCommand.Parameters.AddWithValue("@UpdateKredit", updateKredit)
                        updateCommand.Parameters.AddWithValue("@noresit", order.OrderID)
                        updateCommand.Parameters.AddWithValue("@NoItem", noItem)

                        updateCommand.ExecuteNonQuery()

                        remainingKredit -= updateKredit
                    End Using
                End If

                If remainingKredit <= 0 Then
                    Exit For
                End If
            Next

            connection.Close()
        End Using

        'UpdateStatusHeaderTerima(order)

    End Function

    Public Function UpdateOrderDetailBilStatus(order As Order_terima)
        Dim db = New DBKewConn

        Dim noBil As String = GetNoBilByNoresit(order.OrderID)

        Dim dt1 As New DataTable

        ' Create a SQL command for selecting rows to update
        Dim selectQuery As String = "SELECT No_Item FROM SMKB_Bil_Dtl WHERE No_Bil = @nobil ORDER BY No_Item"
        'Dim selectQuery As String = "SELECT No_Dok FROM SMKB_Terima_Hdr WHERE No_Rujukan = @nobil"
        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@nobil", noBil))

        dt1 = db.Read(selectQuery, param)

        Using connection As New SqlConnection(db.ConnectionString)
            connection.Open()

            ' Iterate through the rows to update
            For Each row As DataRow In dt1.Rows
                Dim noItem As String = row.Field(Of String)("No_Item")

                ' Calculate the Status_Bayaran based on your conditions
                Dim statusBayaran As String = CalculateStatusBayaran(order.OrderID, noItem, noBil)

                ' Update the Status_Bayaran column for the current row
                Dim updateQuery As String = "UPDATE SMKB_Bil_Dtl SET Status_Bayaran = @StatusBayaran WHERE No_Bil = @nobil AND No_Item = @NoItem"
                Using updateCommand As New SqlCommand(updateQuery, connection)
                    updateCommand.Parameters.AddWithValue("@StatusBayaran", statusBayaran)
                    updateCommand.Parameters.AddWithValue("@nobil", noBil)
                    updateCommand.Parameters.AddWithValue("@NoItem", noItem)

                    updateCommand.ExecuteNonQuery()
                End Using
            Next

            connection.Close()
        End Using
    End Function

    Private Function UpdateStatusHeaderTerima(order As Order_terima)
        Dim db As New DBKewConn
        Dim query As String = "UPDATE SMKB_Terima_Hdr
                                SET Kod_Status_Dok = '@status'
                                where No_Bil=@nobil"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@nobil", order.NoBil))
        param.Add(New SqlParameter("@status", "04"))

        Return db.Process(query, param)
    End Function

    Private Function CalculateStatusBayaran(orderID As String, noItem As Integer, noBil As String) As String
        Dim db = New DBKewConn

        Dim totalKredit As Decimal = GetTotalKreditByItem(noBil, noItem)

        ' Create a SQL command to check if Debit = Kredit for the specified item
        Dim checkQuery As String = "SELECT Jumlah FROM SMKB_Bil_Dtl WHERE No_Bil = @nobil AND No_Item = @NoItem"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@nobil", noBil))
        param.Add(New SqlParameter("@NoItem", noItem))

        Dim dt As DataTable = db.Read(checkQuery, param)

        If dt.Rows.Count > 0 Then
            Dim jumlahByItem As Decimal = Decimal.Parse(dt.Rows(0)("Jumlah").ToString())

            If totalKredit = jumlahByItem Then
                Return "BP"
            ElseIf totalKredit <> 0.0 Then
                Return "BS"
            Else
                Return "BB"
            End If
        Else
            ' Handle the case where there are no rows found for the specified item
            ' You can return an appropriate value or handle it as needed
            Return "Unknown"
        End If
    End Function

    Public Function GetNoBilByNoresit(noresit As String) As String
        Dim db = New DBKewConn

        ' Create a SQL command for selecting No_Bil
        Dim selectQuery As String = "SELECT No_Rujukan FROM SMKB_Terima_Hdr WHERE No_Dok = @noresit"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@noresit", noresit))

        ' Execute the query to retrieve No_Bil
        Dim result As DataTable = db.Read(selectQuery, param)

        Dim noBil As String = result.Rows(0)("No_Rujukan")

        Return noBil.ToString()
    End Function

    Public Function GetJumlahSebenarByNoBil(nobil As String, noresit As String) As String
        Dim db = New DBKewConn

        ' Create a SQL command for selecting No_Bil
        Dim selectQuery As String = "SELECT CASE WHEN SUM(Jumlah_Bayar) <> NULL THEN SUM(Jumlah_Bayar) ELSE '0.00' END AS Jumlah_Bayar FROM SMKB_Terima_Hdr WHERE No_Rujukan = @nobil AND No_Dok <> @noresit"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@nobil", nobil))
        param.Add(New SqlParameter("@noresit", noresit))
        ' Execute the query to retrieve No_Bil
        Dim result As DataTable = db.Read(selectQuery, param)

        Dim jumlahbayar As Decimal = result.Rows(0)("Jumlah_Bayar")

        Return jumlahbayar
    End Function

    Public Function GetJumlahSebenarByNoResit(noresit As String) As Decimal
        Dim db = New DBKewConn

        ' Create a SQL command for selecting No_Bil
        Dim selectQuery As String = "SELECT Jumlah_Sebenar FROM SMKB_Terima_Hdr WHERE No_Dok= @noresit"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@noresit", noresit))

        ' Execute the query to retrieve No_Bil
        Dim result As DataTable = db.Read(selectQuery, param)

        Dim jumlah As Decimal = result.Rows(0)("Jumlah_Sebenar")

        Return jumlah
    End Function

    Public Function GetJumlahByNoBil(noresit As String) As Decimal
        Dim db = New DBKewConn

        ' Create a SQL command for selecting No_Bil
        Dim selectQuery As String = "SELECT Jumlah FROM SMKB_Bil_Dtl WHERE No_Bil= @noresit"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@noresit", noresit))

        ' Execute the query to retrieve No_Bil
        Dim result As DataTable = db.Read(selectQuery, param)

        Dim jumlah As Decimal = result.Rows(0)("Jumlah")

        Return jumlah
    End Function

    Public Function GetJumlahByNoBilFromTerimaHdr(noBil As String) As Decimal
        Dim db = New DBKewConn

        ' Create a SQL command for selecting No_Bil
        Dim selectQuery As String = "SELECT SUM(Jumlah_Bayar) as Jumlah FROM SMKB_Terima_Hdr WHERE No_Rujukan=@nobil"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@nobil", noBil))

        ' Execute the query to retrieve No_Bil
        Dim result As DataTable = db.Read(selectQuery, param)

        Dim jumlah As Decimal = result.Rows(0)("Jumlah")

        Return jumlah
    End Function

    Public Function GetTotalKreditByItem(No_Rujukan As String, noItem As Integer) As Decimal
        Dim db = New DBKewConn

        ' Create a SQL command for selecting No_Bil
        Dim selectQuery As String = "SELECT No_Dok FROM SMKB_Terima_Hdr WHERE No_Rujukan=@No_Rujukan"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@No_Rujukan", No_Rujukan))

        Dim jumlahByItem As New Decimal

        Dim dt = db.Read(selectQuery, param)

        Debug.Print(No_Rujukan)

        Using connection As New SqlConnection(db.ConnectionString)
            connection.Open()

            ' Iterate through the rows to update
            For Each row As DataRow In dt.Rows
                Dim noDok As String = row.Field(Of String)("No_Dok")

                Debug.Print(noDok)

                Dim selectJumlahQuery As String = "SELECT SUM(Kredit) AS TotalKredit FROM SMKB_Terima_Dtl WHERE No_Dok = @noDok AND No_Item = @No_Item"

                Dim param1 As New List(Of SqlParameter)
                param1.Add(New SqlParameter("@noDok", noDok))
                param1.Add(New SqlParameter("@No_Item", noItem))

                Dim dt1 As DataTable = db.Read(selectJumlahQuery, param1)

                If dt1.Rows.Count > 0 Then
                    Dim totalKredit As Decimal

                    If Not IsDBNull(dt1.Rows(0)("TotalKredit")) AndAlso Decimal.TryParse(dt1.Rows(0)("TotalKredit").ToString(), totalKredit) Then
                        jumlahByItem += totalKredit
                    Else
                        ' Handle the case where the value cannot be converted to Decimal
                        ' You can log an error or take appropriate action here

                    End If

                End If

            Next
            connection.Close()
        End Using

        Return jumlahByItem
    End Function

    Public Sub UpdateOrderDetailBilStatusNew(order As Order_terima)
        Dim db = New DBKewConn

        Dim noBil As String = GetNoBilByNoresit(order.OrderID)

        Using connection As New SqlConnection(db.ConnectionString)
            connection.Open()

            ' Create a SQL command for selecting rows to update
            Dim selectQuery As String = "SELECT No_Item, Jumlah FROM SMKB_Bil_Dtl WHERE No_Bil = @nobil ORDER BY No_Item"
            Dim param As New List(Of SqlParameter)
            param.Add(New SqlParameter("@nobil", noBil))

            Dim dt1 As DataTable = db.Read(selectQuery, param)

            'Dim balance As Decimal = order.JumlahTerima ' Set the initial balance

            Dim balance As Decimal = GetJumlahByNoBilFromTerimaHdr(order.NoBil)

            Dim remainingRows As Boolean = False

            ' Iterate through the rows to update
            For Each row As DataRow In dt1.Rows
                Dim noItem As String = row.Field(Of String)("No_Item")
                Dim jumlahByItem As Decimal = Decimal.Parse(row("Jumlah").ToString())
                Dim statusBayaran As String = ""

                balance -= jumlahByItem

                ' Update the Status_Bayaran based on the balance
                If balance >= 0 Then
                    statusBayaran = "BP"
                    'balance -= jumlahByItem
                ElseIf balance < 0 AndAlso remainingRows Then
                    statusBayaran = "BB"
                Else
                    statusBayaran = "BS"
                    remainingRows = True
                End If

                ' Update the Status_Bayaran column for the current row
                Dim updateQuery As String = "UPDATE SMKB_Bil_Dtl SET Status_Bayaran = @StatusBayaran WHERE No_Bil = @nobil AND No_Item = @NoItem"
                Using updateCommand As New SqlCommand(updateQuery, connection)
                    updateCommand.Parameters.AddWithValue("@StatusBayaran", statusBayaran)
                    updateCommand.Parameters.AddWithValue("@nobil", noBil)
                    updateCommand.Parameters.AddWithValue("@NoItem", noItem)

                    updateCommand.ExecuteNonQuery()
                End Using
            Next

            connection.Close()
        End Using
    End Sub


    Public Function UpdateOrderDetail(orderDetail As OrderDetail_inv)
        Dim db = New db
        Dim query As String = "UPDATE ORDERDETAILS
        set ddlVot = @ddlVot, details = @details, quantity = @quantity, 
        price = @price, amount = @amount
        where id = @id"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@ddlVot", orderDetail.ddlVot))
        param.Add(New SqlParameter("@details", orderDetail.details))
        param.Add(New SqlParameter("@quantity", orderDetail.quantity))
        param.Add(New SqlParameter("@price", orderDetail.price))
        param.Add(New SqlParameter("@amount", orderDetail.amount))
        param.Add(New SqlParameter("@id", orderDetail.id))

        Return db.Process(query, param)
    End Function

    Public Function GenerateOrderDetailID(orderid As String) As String
        Dim db = New DBKewConn
        Dim lastID As Integer = 1
        Dim newOrderID As String = ""
        Dim param As New List(Of SqlParameter)

        Dim query As String = "SELECT TOP 1 No_Item as id 
                                FROM SMKB_Bil_Dtl 
                                WHERE No_Bil= @orderid
                                ORDER BY No_Item DESC"

        param.Add(New SqlParameter("@orderid", orderid))

        dt = db.Read(query, param)
        If dt IsNot Nothing Then
            If dt.Rows.Count > 0 Then
                'lastID = 1
                'Else
                lastID = CInt(dt.Rows(0).Item("id")) + 1
            End If
        End If

        'newOrderID = orderid + "SUBORD" + Right("0" + CStr(lastID), 2)
        newOrderID = lastID
        Return newOrderID
    End Function

    'Public Function InsertNewOrder(orderid As String)
    'Dim db = New db
    'Dim query As String = "INSERT INTO SMKB_Bil_Hdr VALUES (@orderid, getdate())"
    'Dim param As New List(Of SqlParameter)

    'param.Add(New SqlParameter("@orderid", orderid))

    'Return db.Process(query, param)
    'End Function

    Private Function GenerateOrderID() As String
        Dim db As New DBKewConn

        Dim year = Date.Now.ToString("yyyy")
        Dim month = Date.Now.Month

        Dim lastID As Integer = 1
        Dim newOrderID As String = ""

        Dim query As String = $"select TOP 1 No_Akhir as id from SMKB_No_Akhir where Kod_Modul ='12' AND Prefix ='RT' AND Tahun =@year"
        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@year", year))

        dt = db.Read(query, param)

        If dt.Rows.Count > 0 Then
            lastID = CInt(dt.Rows(0).Item("id")) + 1

            UpdateNoAkhir("12", "RT", year, lastID)
        Else

            InsertNoAkhir("12", "RT", year, lastID)
        End If
        newOrderID = "RT" + Format(lastID, "000000").ToString + month.ToString("00") + Right(year.ToString(), 2)

        Return newOrderID
    End Function

    Private Function UpdateNoAkhir(kodModul As String, prefix As String, year As String, ID As String)
        Dim db As New DBKewConn
        Dim query As String = "UPDATE SMKB_No_Akhir
        set No_Akhir = @No_Akhir
        where Kod_Modul=@Kod_Modul and Prefix=@Prefix and Tahun =@Tahun"
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

    Private Function DeleteOrderRecord(orderid As String)
        Dim db = New db
        Dim query As String = "DELETE FROM orders WHERE order_id = @orderid "
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@orderid", orderid))

        Return db.Process(query, param)
    End Function

    Private Function DeleteOrderDetails(Optional id As String = "")
        Dim db = New DBKewConn
        Dim data
        data = Split(id, "|")
        Dim nobil = data(0)
        Dim noitem = data(1)
        Dim query As String = "DELETE FROM SMKB_Bil_Dtl WHERE No_Bil=@nobil AND No_Item=@noitem "
        Dim param As New List(Of SqlParameter)

        If id <> "" Then
            param.Add(New SqlParameter("@nobil", nobil))
            param.Add(New SqlParameter("@noitem", noitem))
        End If

        Return db.Process(query, param)
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

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadHdrResit(ByVal id As String) As String
        Dim resp As New ResponseRepository

        dt = GetHdrResit(id)
        resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    Private Function GetHdrResit(id As String) As DataTable
        Dim db = New DBKewConn

        Dim query As String = "SELECT A.No_Dok,A.Kod_Penghutang,B.Nama_Penghutang,A.Kod_Urusniaga,C.Butiran AS URUSNIAGA, A.Tujuan,
                                CASE WHEN Tkh_Daftar <> '' THEN FORMAT(Tkh_Daftar, 'dd/MM/yyyy') END AS Tkh_Daftar,Jumlah_Bayar,E.Butiran AS MODTERIMA,F.Kod_Bank
                                FROM SMKB_Terima_Hdr A
                                LEFT JOIN SMKB_Bil_Hdr D ON A.No_Rujukan=D.No_Bil
                                INNER JOIN SMKB_Penghutang_Master B ON A.Kod_Penghutang=B.Kod_Penghutang
                                INNER JOIN SMKB_Kod_Urusniaga C ON A.Kod_Urusniaga=C.Kod_Urusniaga
                                LEFT JOIN SMKB_Lookup_Detail E ON E.Kod='AR01' AND REPLACE(A.Mod_Terima,'0','')= E.Kod_Detail
                                LEFT JOIN SMKB_Bank_Master F ON A.Kod_Bank=F.Kod_Bank
                                WHERE No_Dok = @no_dok AND A.Status='1'"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@no_dok", id))

        Return db.Read(query, param)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadHdrBil(ByVal id As String) As String
        Dim resp As New ResponseRepository

        dt = GetHdrBil(id)
        resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    Private Function GetHdrBil(id As String) As DataTable
        Dim db = New DBKewConn

        Dim query As String = "SELECT A.No_Bil,A.Kod_Penghutang,B.Nama_Penghutang,A.Kod_Urusniaga,C.Butiran, A.Kontrak,A.Tujuan,
                                CASE WHEN Tkh_Mula <> '' THEN FORMAT(Tkh_Mula, 'yyyy-MM-dd') END AS Tkh_Mula,
                                CASE WHEN Tkh_Tamat <> '' THEN FORMAT(Tkh_Tamat, 'yyyy-MM-dd') END AS Tkh_Tamat
                                FROM SMKB_Bil_Hdr A
                                INNER JOIN SMKB_Penghutang_Master B ON A.Kod_Penghutang=B.Kod_Penghutang
                                INNER JOIN SMKB_Kod_Urusniaga C ON A.Kod_Urusniaga=C.Kod_Urusniaga
                                WHERE No_Bil = @No_Bil AND A.Status='1'"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@No_Bil", id))

        Return db.Read(query, param)
    End Function



    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadRecordResit(ByVal id As String) As String
        Dim resp As New ResponseRepository

        dt = GetTransaksiResit(id)
        resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    Private Function GetTransaksiResit(kod As String) As DataTable
        Dim db = New DBKewConn

        Dim query As String = "select Kod_Kump_Wang as colhidkw ,(select Butiran from SMKB_Kump_Wang as kw where a.Kod_Kump_Wang = kw.Kod_Kump_Wang) as colKW,
                            Kod_Operasi  as colhidko, (select Butiran from SMKB_Operasi as ko where a.Kod_Operasi = ko.Kod_Operasi) as colKO,
                            Kod_Projek as colhidkp,  (select Butiran from SMKB_Projek as kp where a.Kod_Projek = kp.Kod_Projek) as colKp,
                            Kod_PTJ as colhidptj , (SELECT b.Pejabat FROM [DEVMIS\SQL_INS01].dbStaf.dbo.MS_PEJABAT as b
                            WHERE b.STATUS = 1 and b.kodpejabat = left(Kod_PTJ,2)) as ButiranPTJ ,
                            Kod_Vot , (select Kod_Vot+' - '+ Butiran from SMKB_Vot as vot where a.Kod_Vot = vot.Kod_Vot) as ButiranVot,
                            A.Butiran , Kredit
                            from SMKB_Terima_Dtl as a
                            LEFT JOIN SMKB_Terima_Hdr B ON A.No_Dok=B.No_Dok 
                            WHERE A.No_Dok=@no_dok AND Kredit<>'0.00'
                            and B.status = 1
                            order by No_Item"
        '"select a.No_Bil+'|'+a.No_Item as dataid,Kod_Kump_Wang as colhidkw ,(select Butiran from SMKB_Kump_Wang as kw where a.Kod_Kump_Wang = kw.Kod_Kump_Wang) as colKW,
        '                        Kod_Operasi  as colhidko, (select Butiran from SMKB_Operasi as ko where a.Kod_Operasi = ko.Kod_Operasi) as colKO,
        '                        Kod_Projek as colhidkp,  (select Butiran from SMKB_Projek as kp where a.Kod_Projek = kp.Kod_Projek) as colKp,
        '                        Kod_PTJ as colhidptj , (SELECT b.Pejabat FROM [DEVMIS\SQL_INS01].dbStaf.dbo.MS_PEJABAT as b
        '                        WHERE b.STATUS = 1 and b.kodpejabat = left(Kod_PTJ,2)) as ButiranPTJ ,
        '                        Kod_Vot , (select Kod_Vot+' - '+ Butiran from SMKB_Vot as vot where a.Kod_Vot = vot.Kod_Vot) as ButiranVot,
        '                        Perkara ,Kuantiti, Kadar_Harga, Jumlah, Diskaun, Cukai
        '                        from SMKB_Bil_Dtl as a
        '                        LEFT JOIN SMKB_Terima_Hdr B ON A.No_Bil=B.No_Rujukan 
        '                        WHERE No_Dok=@no_dok
        '                        and B.status = 1
        '                        order by No_Item"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@no_dok", kod))

        Return db.Read(query, param)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadRecordBil(ByVal id As String) As String
        Dim resp As New ResponseRepository

        dt = GetTransaksiBil(id)
        resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    Private Function GetTransaksiBil(kod As String) As DataTable
        Dim db = New DBKewConn

        Dim query As String = "select a.No_Bil+'|'+a.No_Item as dataid,Kod_Kump_Wang as colhidkw ,(select Butiran from SMKB_Kump_Wang as kw where a.Kod_Kump_Wang = kw.Kod_Kump_Wang) as colKW,
        Kod_Operasi  as colhidko, (select Butiran from SMKB_Operasi as ko where a.Kod_Operasi = ko.Kod_Operasi) as colKO,
        Kod_Projek as colhidkp,  (select Butiran from SMKB_Projek as kp where a.Kod_Projek = kp.Kod_Projek) as colKp,
        Kod_PTJ as colhidptj , (SELECT b.Pejabat FROM [DEVMIS\SQL_INS01].dbStaf.dbo.MS_PEJABAT as b
        WHERE b.STATUS = 1 and b.kodpejabat = left(Kod_PTJ,2)) as ButiranPTJ ,
        Kod_Vot , (select Butiran from SMKB_Vot as vot where a.Kod_Vot = vot.Kod_Vot) as ButiranVot,
        Perkara ,Kuantiti, Kadar_Harga, Jumlah, Diskaun, Cukai
        from SMKB_Bil_Dtl as a
        where No_Bil = @No_Bil
        and status = 1
        order by No_Item"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@No_Bil", kod))

        Return db.Read(query, param)
    End Function



    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadOrderRecord_SenaraiTransaksiInvois() As String
        Dim resp As New ResponseRepository


        dt = GetOrder_SenaraiTransaksiInvois()
        'resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(dt)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetOrder_SenaraiTransaksiInvois() As DataTable
        Dim db = New DBKewConn

        Dim query As String = "SELECT A.No_Bil as No_Invois,B.Nama_Penghutang,C.Butiran as UrusNiaga,A.Tujuan,
                                CASE WHEN Tkh_Mula <> '' THEN FORMAT(Tkh_Mula, 'dd/MM/yyyy') END AS Tkh_Mula,
                                CASE WHEN Tkh_Tamat <> '' THEN FORMAT(Tkh_Tamat, 'dd/MM/yyyy') END AS Tkh_Tamat
                                FROM SMKB_Bil_Hdr A
                                inner JOIN SMKB_Penghutang_Master B ON A.Kod_Pelanggan=B.Kod_Penghutang
                                inner JOIN SMKB_Kod_Urusniaga C ON A.Jenis_Urusniaga=C.Kod_Urusniaga
                                WHERE  A.Status='1'"

        Return db.Read(query)
    End Function


    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadRecordTuntutan(NoBil As String) As String
        Dim resp As New ResponseRepository

        dt = GetRecordTuntutan(NoBil)
        'resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(dt)
    End Function
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetRecordTuntutan(NoBil As String) As DataTable
        Dim db As New DBKewConn

        Dim query As String = "SELECT No_Item, Kod_Kump_Wang, Kod_Operasi, Kod_PTJ, Kod_Projek, Kod_Vot, Perkara, Kuantiti, FORMAT(Kadar_Harga, 'N2') AS Kadar_Harga, Jumlah,'0.00' As Debit 
                FROM SMKB_Bil_Dtl WHERE No_Bil = @NoBil"
        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@NoBil", NoBil))


        Return db.Read(query, param)
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Async Function SaveLulus(order As Order_terima) As Tasks.Task(Of String)
        Dim resp As New ResponseRepository
        resp.Success("Data telah diluluskan")
        Dim success As Integer = 0
        Dim JumRekod As Integer = 0
        If order Is Nothing Then
            resp.Failed("Tiada simpan")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If



        If UpdateLulusOrder(order) <> "OK" Then

            resp.Failed("Transaksi Tidak Berjaya")
            Return JsonConvert.SerializeObject(resp.GetResult())
            Exit Function
        Else


        End If
        If UpdateLulusBil(order) <> "OK" Then
            resp.Failed("Transaksi Tidak Berjaya")
            Return JsonConvert.SerializeObject(resp.GetResult())
            Exit Function
        End If

        For Each orderDetail As OrderDetail_inv In order.OrderDetails



            If orderDetail.ddlPTJ = "" Then
                Continue For
            End If

            If orderDetail.Cukai Is Nothing Then
                orderDetail.Cukai = 0.00
            End If

            If orderDetail.Diskaun Is Nothing Then
                orderDetail.Diskaun = 0.00
            End If

            JumRekod += 1

            orderDetail.amount = orderDetail.quantity * orderDetail.price 'This can be automated inside orderdetail model

            'If orderDetail.id = "" Then
            '    orderDetail.id = GenerateOrderDetailID(order.OrderID)
            '    orderDetail.OrderID = order.OrderID
            '    If InsertOrderDetail(orderDetail) = "OK" Then
            '        success += 1
            '    End If
            'Else
            'If UpdateLulusOrderDetail(orderDetail) >= 1 Then

            'If updatelejaram(orderDetail) >= 1 Then
            '    success += 1
            'End If

            '--- TAMBAHAN FUNCTION CALL POSTING KE LAJER PENGHUTANG ----
            Try
                Dim myGetTicket As New TokenResponseModel()

                Dim servicex As New ValuesService()
                Dim kodkw = orderDetail.ddlKw
                Dim kodko = orderDetail.ddlKo
                Dim kodkp = orderDetail.ddlKp
                Dim kodptj = orderDetail.ddlPTJ 'SEMAK SEMULA BETUL KE COA?
                Dim kodvot = order.KodBank
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("ms-MY")
                Dim parsedDate As Date = CDate(Now()).ToString("yyyy-MM-dd")
                Dim vBulan As String = parsedDate.Month
                Dim vTahun As String = parsedDate.Year
                Dim values As String = Await servicex.SendDataLejar(myGetTicket.GetTicket("smkb", Session("ssusrID")),
                                        "GL", "UTeM", kodkw, kodptj,
                                        kodvot, kodko, kodkp, order.JumlahTerima, "DR", vBulan, vTahun)

                If values.Contains("ok") Then

                    '    'lblModalMessaage.Text = "Rekod telah disimpan" 'message di modal
                    '    'ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)

                Else
                    '    'lblModalMessaage.Text = "Rekod Gagal disimpan" 'message di modal
                    '    'ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                End If
            Catch
                'lblModalMessaage.Text = "Invalid Token" 'message di modal
                'ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)

            End Try
            Try
                Dim myGetTicket As New TokenResponseModel()

                Dim servicex As New ValuesService()
                Dim kodkw = orderDetail.ddlKw
                Dim kodko = orderDetail.ddlKo
                Dim kodkp = orderDetail.ddlKp
                Dim kodptj = orderDetail.ddlPTJ
                Dim kodvot = "71901"
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("ms-MY")
                Dim parsedDate As Date = CDate(Now()).ToString("yyyy-MM-dd")
                Dim vBulan As String = parsedDate.Month
                Dim vTahun As String = parsedDate.Year
                Dim values As String = Await servicex.SendDataLejar(myGetTicket.GetTicket("smkb", Session("ssusrID")),
                                        "GL", "UTeM", kodkw, kodptj,
                                        kodvot, kodko, kodkp, order.JumlahTerima, "CR", vBulan, vTahun)

                If values.Contains("ok") Then

                    '    'lblModalMessaage.Text = "Rekod telah disimpan" 'message di modal
                    '    'ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)

                Else
                    '    'lblModalMessaage.Text = "Rekod Gagal disimpan" 'message di modal
                    '    'ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                End If
            Catch
                'lblModalMessaage.Text = "Invalid Token" 'message di modal
                'ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)

            End Try
            '---------------------- END ------------------------------

            'End If
            'End If
            Try
                Dim myGetTicket As New TokenResponseModel()

                Dim servicex As New ValuesService()
                Dim kodkw = orderDetail.ddlKw
                Dim kodko = orderDetail.ddlKo
                Dim kodkp = orderDetail.ddlKp
                Dim kodptj = orderDetail.ddlPTJ
                Dim kodvot = "71901"
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("ms-MY")
                Dim parsedDate As Date = CDate(Now()).ToString("yyyy-MM-dd")
                Dim vBulan As String = parsedDate.Month
                Dim vTahun As String = parsedDate.Year
                Dim values As String = Await servicex.SendDataLejar(myGetTicket.GetTicket("smkb", Session("ssusrID")),
                                        "AR", order.PenghutangID, kodkw, kodptj,
                                        kodvot, kodko, kodkp, order.JumlahTerima, "CR", vBulan, vTahun)

                If values.Contains("ok") Then

                    '    'lblModalMessaage.Text = "Rekod telah disimpan" 'message di modal
                    '    'ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                    resp.Success("Rekod berjaya disimpan")
                    Return JsonConvert.SerializeObject(resp.GetResult())
                Else
                    '    'lblModalMessaage.Text = "Rekod Gagal disimpan" 'message di modal
                    '    'ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                    resp.Failed("Transaksi Tidak Berjaya")
                    Return JsonConvert.SerializeObject(resp.GetResult())
                    Exit Function
                End If
            Catch
                'lblModalMessaage.Text = "Invalid Token" 'message di modal
                'ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)

            End Try
        Next
        'If updatelejarHUTANG(order.OrderID, order.PenghutangID, order.Jumlah) >= 1 Then
        '    success += 1
        'End If


        '---------------------- END ------------------------------

        'If success = 0 Then
        '    resp.Failed("Rekod order detail gagal disimpan")
        '    Return JsonConvert.SerializeObject(resp.GetResult())

        'Else
        '    resp.Success("Rekod berjaya disimpan", "00", order)
        '    Return JsonConvert.SerializeObject(resp.GetResult())
        'End If


        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function
    Private Function UpdateLulusBil(orderid As Order_terima)
        Dim db As New DBKewConn
        Dim kodstatusLulus As String = "06" 'Kelulusan Resit

        Dim query As String = "UPDATE R 
                            SET R.Kod_Status_Dok = @kodstatus 
                            FROM SMKB_Bil_Hdr AS R
                            INNER JOIN SMKB_Terima_Hdr AS P 
                                    ON R.No_Bil = P.No_Rujukan 
                            WHERE P.No_Dok = @id "
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@id", orderid.OrderID))
        param.Add(New SqlParameter("@kodstatus", kodstatusLulus))

        Return db.Process(query, param)

    End Function
    Private Function UpdateLulusOrder(orderid As Order_terima)
        Dim db As New DBKewConn

        'Dim kodstatusLulus As String = "04" 'Kelulusan Resit

        Dim query As String = "UPDATE SMKB_Terima_Hdr
                                SET Flag_Lulus=@flaglulus,Tkh_Lulus=@tkhlulus,Staf_Lulus=@staflulus
                                WHERE No_Dok=@id"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@flaglulus", 1))
        param.Add(New SqlParameter("@tkhlulus", Now()))
        param.Add(New SqlParameter("@id", orderid.OrderID))
        param.Add(New SqlParameter("@staflulus", Session("ssusrID")))

        Return db.Process(query, param)

    End Function
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadOrderRecord_PertanyaanTerimaan(category_filter As String, isClicked As Boolean, tkhMula As String, tkhTamat As String) As String
        Dim resp As New ResponseRepository

        If isClicked = False Then
            Return JsonConvert.SerializeObject(New DataTable)
        End If

        dt = GetOrder_PertanyaanTerimaan(category_filter, tkhMula, tkhTamat)

        Return JsonConvert.SerializeObject(dt)
    End Function
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetOrder_PertanyaanTerimaan(category_filter As String, tkhMula As String, tkhTamat As String) As DataTable
        Dim db = New DBKewConn
        Dim tarikhQuery As String = ""
        Dim param As New List(Of SqlParameter)
        Try

            If category_filter = "1" Then 'Harini
                tarikhQuery = " AND CAST(A.Tkh_Daftar AS DATE) = CAST(CURRENT_TIMESTAMP AS DATE) "
            ElseIf category_filter = "2" Then 'Semalam
                tarikhQuery = " AND CAST(A.Tkh_Daftar AS DATE) = CAST(DATEADD(day, -1, CURRENT_TIMESTAMP) AS DATE) "
            ElseIf category_filter = "3" Then 'seminggu
                tarikhQuery = " AND A.Tkh_Daftar >= DATEADD(day, -7, CURRENT_TIMESTAMP) AND A.Tkh_Daftar < CURRENT_TIMESTAMP "
            ElseIf category_filter = "4" Then '30 hari
                tarikhQuery = " AND A.Tkh_Daftar >= DATEADD(day, -30, CURRENT_TIMESTAMP) AND A.Tkh_Daftar < CURRENT_TIMESTAMP "
            ElseIf category_filter = "5" Then '60 hari
                tarikhQuery = " AND A.Tkh_Daftar >= DATEADD(day, -60, CURRENT_TIMESTAMP) AND A.Tkh_Daftar < CURRENT_TIMESTAMP "
            ElseIf category_filter = "6" Then 'custom
                tarikhQuery = " AND CAST(A.Tkh_Daftar AS DATE) >= @tkhMula AND CAST(A.Tkh_Daftar AS DATE) <= @tkhTamat"
            End If

            param.Add(New SqlParameter("@tkhMula", tkhMula))
            param.Add(New SqlParameter("@tkhTamat", tkhTamat))

            Dim query As String = "SELECT A.No_Dok ,B.Nama_Penghutang,C.Butiran as UrusNiaga,A.Tujuan,
                                CASE WHEN A.Tkh_Daftar <> '' THEN FORMAT(A.Tkh_Daftar, 'dd/MM/yyyy') END AS Tkh_Daftar,
                                A.Jumlah_Bayar,E.Butiran AS MOD_TERIMA, (SELECT COUNT(No_Item) FROM SMKB_Terima_Dtl WHERE No_Dok=A.No_Dok) AS BILITEM,
                                F.MS01_Nama,G.Butiran as Status
                                FROM SMKB_Terima_Hdr A
                                LEFT JOIN SMKB_Penghutang_Master B ON A.Kod_Penghutang=B.Kod_Penghutang
                                LEFT JOIN SMKB_Kod_Urusniaga C ON A.Kod_Urusniaga=C.Kod_Urusniaga
                                LEFT JOIN SMKB_Lookup_Detail E ON E.Kod='AR01' AND REPLACE(A.Mod_Terima,'0','')= E.Kod_Detail
                                LEFT JOIN [DEVMIS\SQL_INS01].dbStaf.dbo.MS01_Peribadi as F on F.MS01_NoStaf = A.Staf_Terima
                                INNER JOIN SMKB_Kod_Status_Dok G ON G.Kod_Modul='12' AND A.Kod_Status_Dok=G.Kod_Status_Dok
                                WHERE  A.Status='1'" & tarikhQuery
            Return db.Read(query, param)
        Catch ex As SqlException
            Throw
        End Try
    End Function
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadOrderRecord_PembatalanResit(category_filter As String, isClicked As Boolean, tkhMula As String, tkhTamat As String) As String
        Dim resp As New ResponseRepository

        If isClicked = False Then
            Return JsonConvert.SerializeObject(New DataTable)
        End If

        dt = GetOrder_PembatalanResit(category_filter, tkhMula, tkhTamat)

        Return JsonConvert.SerializeObject(dt)
    End Function
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetOrder_PembatalanResit(category_filter As String, tkhMula As String, tkhTamat As String) As DataTable
        Dim db = New DBKewConn
        Dim tarikhQuery As String = ""
        Dim param As New List(Of SqlParameter)
        Try

            If category_filter = "1" Then 'Harini
                tarikhQuery = " AND CAST(A.Tkh_Daftar AS DATE) = CAST(CURRENT_TIMESTAMP AS DATE) "
            ElseIf category_filter = "2" Then 'Semalam
                tarikhQuery = " AND CAST(A.Tkh_Daftar AS DATE) = CAST(DATEADD(day, -1, CURRENT_TIMESTAMP) AS DATE) "
            ElseIf category_filter = "3" Then 'seminggu
                tarikhQuery = " AND A.Tkh_Daftar >= DATEADD(day, -7, CURRENT_TIMESTAMP) AND A.Tkh_Daftar < CURRENT_TIMESTAMP "
            ElseIf category_filter = "4" Then '30 hari
                tarikhQuery = " AND A.Tkh_Daftar >= DATEADD(day, -30, CURRENT_TIMESTAMP) AND A.Tkh_Daftar < CURRENT_TIMESTAMP "
            ElseIf category_filter = "5" Then '60 hari
                tarikhQuery = " AND A.Tkh_Daftar >= DATEADD(day, -60, CURRENT_TIMESTAMP) AND A.Tkh_Daftar < CURRENT_TIMESTAMP "
            ElseIf category_filter = "6" Then 'custom
                tarikhQuery = " AND CAST(A.Tkh_Daftar AS DATE) >= @tkhMula AND CAST(A.Tkh_Daftar AS DATE) <= @tkhTamat"
            End If

            param.Add(New SqlParameter("@tkhMula", tkhMula))
            param.Add(New SqlParameter("@tkhTamat", tkhTamat))

            Dim query As String = "SELECT A.No_Dok ,B.Nama_Penghutang,C.Butiran as UrusNiaga,A.Tujuan,
                            CASE WHEN A.Tkh_Daftar <> '' THEN FORMAT(A.Tkh_Daftar, 'dd/MM/yyyy') END AS Tkh_Daftar,
                            A.Jumlah_Bayar,E.Butiran AS MOD_TERIMA, (SELECT COUNT(No_Item) FROM SMKB_Terima_Dtl WHERE No_Dok=A.No_Dok) AS BILITEM,
                            F.MS01_Nama,G.Butiran as Status
                            FROM SMKB_Terima_Hdr A
                            LEFT JOIN SMKB_Penghutang_Master B ON A.Kod_Penghutang=B.Kod_Penghutang
                            LEFT JOIN SMKB_Kod_Urusniaga C ON A.Kod_Urusniaga=C.Kod_Urusniaga
                            LEFT JOIN SMKB_Lookup_Detail E ON E.Kod='AR01' AND REPLACE(A.Mod_Terima,'0','')= E.Kod_Detail
                            LEFT JOIN [DEVMIS\SQL_INS01].dbStaf.dbo.MS01_Peribadi as F on F.MS01_NoStaf = A.Staf_Terima
                            INNER JOIN SMKB_Kod_Status_Dok G ON G.Kod_Modul='12' AND A.Kod_Status_Dok=G.Kod_Status_Dok
                            WHERE  A.Status='1' AND A.Kod_Status_Dok <> '11'" & tarikhQuery
            Return db.Read(query, param)
        Catch ex As SqlException
            Throw
        End Try
    End Function
    <System.Web.Services.WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SaveUpdate(id As String) As String
        Dim resp As New ResponseRepository

        If String.IsNullOrEmpty(id) Then
            resp.Failed("Failed")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        If Update_Batal(id) <> "OK" Then
            resp.Failed("Failed")
        Else
            resp.Success("Successful")
        End If

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    Private Function Update_Batal(id As String)
        Dim db As New DBKewConn
        Dim param As New List(Of SqlParameter)

        Dim query As String = "UPDATE SMKB_Terima_Hdr SET Kod_Status_Dok = @kod_status_dok WHERE No_Dok = @id"
        param.Add(New SqlParameter("@kod_status_dok", "11"))
        param.Add(New SqlParameter("@id", id))

        Return db.Process(query, param)
    End Function
End Class