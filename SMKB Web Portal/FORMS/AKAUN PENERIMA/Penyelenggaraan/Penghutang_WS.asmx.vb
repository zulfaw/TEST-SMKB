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
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class Penghutang_WS
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
        Dim query As String = "SELECT TOP 10 CONCAT(a.Kod_Vot, ' - ', vot.Butiran, ', ', mj.Pejabat, ', ', a.Kod_Kump_Wang, ' - ', REPLACE(kw.Butiran, 'KUMPULAN WANG', 'KW'), ', ', a.Kod_Operasi, ' - ', ko.Butiran, ', ', kp.Butiran) AS text,
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
        Dim query As String = "SELECT Kod_Detail as value, Butiran as text, Status as status FROM SMKB_Lookup_Detail WHERE Kod='0152'"
        Dim param As New List(Of SqlParameter)
        If kod <> "" Then
            query &= " AND (Kod_Detail LIKE '%' + @kod + '%' OR Butiran LIKE '%' + @kod2 + '%') "
            param.Add(New SqlParameter("@kod", kod))
            param.Add(New SqlParameter("@kod2", kod))
        End If

        Return db.Read(query, param)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetModTerimaList(ByVal q As String) As String
        'Dim newList As List(Of ItemList)
        'newList = listItem

        'If (q <> "") Then
        '    newList = listItem.Where(
        '        Function(x) x.value.Contains(q) Or x.text.Contains(q)
        '    ).ToList()
        'End If

        Dim tmpDT As DataTable = GetLoadModTerimaList(q)
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
        Dim db
        Dim query As String
        'tentukan jenis urusniaga
        If (kod = "ST") Then
            db = New DBKewConn
            query = "SELECT MS01_NoStaf AS NO_RUJUKAN,MS01_Nama AS NAMA, MS01_KpB AS IDPENGHUTANG,MS01_KodBank AS KODBANK,MS01_NoAkaun AS AKAUN,MS01_JenAkaun AS JENIS_AKAUN,
                MS01_AlamatT1 AS ALAMAT1,MS01_AlamatT2 AS ALAMAT2,MS01_PoskodTetap AS POSKOD,MS01_BandarTetap AS BANDAR,C.NamaNegeri AS NEGERI,
                B.NamaNegara AS NEGARA,MS01_Email AS EMEL,MS01_TelPejabat AS NOTEL 
                FROM [DEVMIS\SQL_INS01].dbStaf.dbo.MS01_Peribadi A
                INNER JOIN [DEVMIS\SQL_INS01].dbStaf.dbo.MS_Negara B ON A.MS01_NegaraTetap=B.KodNegara
                INNER JOIN [DEVMIS\SQL_INS01].dbStaf.dbo.MS_Negeri C ON A.MS01_NegeriTetap=C.KodNegeri
                WHERE MS01_Status=1"
        ElseIf (kod = "PL") Then
            db = New DBSMPConn
            query = "SELECT SMP01_Nomatrik AS NO_RUJUKAN,SMP01_Nama AS NAMA, SMP01_KP AS IDPENGHUTANG,SMP01_Bank AS KODBANK,SMP01_NoAkaun AS AKAUN,'' AS JENIS_AKAUN,
                SMP01_Alamat1 AS ALAMAT1,SMP01_Alamat2 AS ALAMAT2,SMP01_Poskod AS POSKOD,SMP01_Bandar AS BANDAR,SMP01_Negeri AS NEGERI,
                'MALAYSIA' AS NEGARA,SMP01_Emel AS EMEL,SMP01_NoTelBimBit AS NOTEL
                FROM SMP01_Peribadi
                WHERE SMP01_Status='AKTIF'"
        End If
        Dim param As New List(Of SqlParameter)
        Return db.Read(query, param)

    End Function



    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SaveOrders(order As Order_inv) As String
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
            If InsertNewOrder(order.OrderID, order.PenghutangID, order.TkhMula, order.TkhTamat, order.Kontrak, order.JenisUrusniaga, order.Tujuan, order.Jumlah) <> "OK" Then
                resp.Failed("Gagal Menyimpan order")
                Exit Function
            End If
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

            orderDetail.amount = orderDetail.quantity * orderDetail.price 'This can be automated insie orderdetail model

            If orderDetail.id = "" Then
                orderDetail.id = GenerateOrderDetailID(order.OrderID)
                orderDetail.OrderID = order.OrderID
                If InsertOrderDetail(orderDetail) = "OK" Then
                    success += 1
                End If
            Else
                If UpdateOrderDetail(orderDetail) = "OK" Then
                    success += 1
                End If
            End If
        Next

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

    Private Function InsertNewOrder(orderid As String, penghutangid As String, TkhMula As String, TkhTamat As String, Kontrak As String, JenisUrusniaga As String, Tujuan As String, Jumlah As String)
        Dim db As New DBKewConn
        Dim query As String = "INSERT INTO SMKB_Bil_Hdr (No_Bil,Tkh_Mohon,Kod_Status_Dok, Status, Kod_Pelanggan,Tkh_Mula,Tkh_Tamat,Kontrak,Jenis_Urusniaga,Tujuan, Jumlah)
        VALUES(@No_Bil, getdate(), '01' ,'1',@Kod_Pelanggan,@Tkh_Mula,@Tkh_Tamat,@Kontrak,@Jenis_Urusniaga,@Tujuan,@Jumlah)"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@No_Bil", orderid))
        param.Add(New SqlParameter("@Kod_Pelanggan", penghutangid))
        param.Add(New SqlParameter("@Tkh_Mula", TkhMula))
        param.Add(New SqlParameter("@Tkh_Tamat", TkhTamat))
        param.Add(New SqlParameter("@Kontrak", Kontrak))
        param.Add(New SqlParameter("@Jenis_Urusniaga", JenisUrusniaga))
        param.Add(New SqlParameter("@Tujuan", Tujuan))
        param.Add(New SqlParameter("@Jumlah", Jumlah))

        Return db.Process(query, param)
    End Function

    Public Function InsertOrderDetail(orderDetail As OrderDetail_inv)
        Dim db = New DBKewConn
        Dim query As String = "INSERT INTO SMKB_Bil_Dtl
        VALUES( @no_bil , @no_item, @Kod_Kump_Wang, @Kod_Operasi , @Kod_PTJ , @Kod_Projek  , @Kod_Vot, 
            @Perkara, @Kuantiti, @Kadar_Harga, @Jumlah, '2023' , '-' , '1', @Diskaun, @Cukai)    "
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@no_bil", orderDetail.OrderID))
        param.Add(New SqlParameter("@no_item", orderDetail.id))
        param.Add(New SqlParameter("@Kod_Kump_Wang", orderDetail.ddlKw))
        param.Add(New SqlParameter("@Kod_Operasi", orderDetail.ddlKo))
        param.Add(New SqlParameter("@Kod_PTJ", orderDetail.ddlPTJ))
        param.Add(New SqlParameter("@Kod_Projek", orderDetail.ddlKp))
        param.Add(New SqlParameter("@Kod_Vot", orderDetail.ddlVot))
        param.Add(New SqlParameter("@Perkara", orderDetail.details))
        param.Add(New SqlParameter("@Kuantiti", orderDetail.quantity))
        param.Add(New SqlParameter("@Kadar_Harga", orderDetail.price))
        param.Add(New SqlParameter("@Jumlah", orderDetail.amount))
        param.Add(New SqlParameter("@Diskaun", orderDetail.Diskaun))
        param.Add(New SqlParameter("@Cukai", orderDetail.Cukai))

        Return db.Process(query, param)
    End Function

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

        Dim query As String = $"select TOP 1 No_Akhir as id from SMKB_No_Akhir where Kod_Modul ='12' AND Prefix ='ILL' AND Tahun =@year"
        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@year", year))

        dt = db.Read(query, param)

        If dt.Rows.Count > 0 Then
            lastID = CInt(dt.Rows(0).Item("id")) + 1

            UpdateNoAkhir("12", "ILL", year, lastID)
        Else

            InsertNoAkhir("12", "ILL", year, lastID)
        End If
        newOrderID = "ILL" + Format(lastID, "000000").ToString + month.ToString("00") + Right(year.ToString(), 2)

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
    Public Function LoadHdrInvois(ByVal id As String) As String
        Dim resp As New ResponseRepository

        dt = GetHdrInvois(id)
        resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    Private Function GetHdrInvois(id As String) As DataTable
        Dim db = New DBKewConn

        Dim query As String = "SELECT A.No_Bil,A.Kod_Pelanggan,B.Nama_Penghutang,A.Jenis_Urusniaga,C.Butiran, A.Kontrak,A.Tujuan,
                                CASE WHEN Tkh_Mula <> '' THEN FORMAT(Tkh_Mula, 'yyyy-MM-dd') END AS Tkh_Mula,
                                CASE WHEN Tkh_Tamat <> '' THEN FORMAT(Tkh_Tamat, 'yyyy-MM-dd') END AS Tkh_Tamat
                                FROM SMKB_Bil_Hdr A
                                INNER JOIN SMKB_Penghutang_Master B ON A.Kod_Pelanggan=B.Kod_Penghutang
                                INNER JOIN SMKB_Kod_Urusniaga C ON A.Jenis_Urusniaga=C.Kod_Urusniaga
                                WHERE No_Bil = @No_Invois AND A.Status='1'"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@No_Invois", id))

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
    Public Function LoadRecordInvois(ByVal id As String) As String
        Dim resp As New ResponseRepository

        dt = GetTransaksiInvois(id)
        resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    Private Function GetTransaksiInvois(kod As String) As DataTable
        Dim db = New DBKewConn

        Dim query As String = "select a.No_Bil+'|'+a.No_Item as dataid,Kod_Kump_Wang as colhidkw ,(select Butiran from SMKB_Kump_Wang as kw where a.Kod_Kump_Wang = kw.Kod_Kump_Wang) as colKW,
        Kod_Operasi  as colhidko, (select Butiran from SMKB_Operasi as ko where a.Kod_Operasi = ko.Kod_Operasi) as colKO,
        Kod_Projek as colhidkp,  (select Butiran from SMKB_Projek as kp where a.Kod_Projek = kp.Kod_Projek) as colKp,
        Kod_PTJ as colhidptj , (SELECT b.Pejabat FROM [DEVMIS\SQL_INS01].dbStaf.dbo.MS_PEJABAT as b
        WHERE b.STATUS = 1 and b.kodpejabat = left(Kod_PTJ,2)) as ButiranPTJ ,
        Kod_Vot , (select Butiran from SMKB_Vot as vot where a.Kod_Vot = vot.Kod_Vot) as ButiranVot,
        Perkara ,Kuantiti, Kadar_Harga, Jumlah, Diskaun, Cukai
        from SMKB_Bil_Dtl as a
        where No_Bil = @No_Invois
        and status = 1
        order by No_Item"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@No_Invois", kod))

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


End Class