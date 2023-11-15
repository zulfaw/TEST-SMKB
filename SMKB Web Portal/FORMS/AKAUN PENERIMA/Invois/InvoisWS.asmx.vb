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
Public Class InvoisWS
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
    Public Function GetPenghutangList(category As String, kod As String) As String
        Dim tmpDT As DataTable = GetKodPenghutangList(category, kod)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetKodPenghutangList(category As String, kod As String) As DataTable
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

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetKategoriPenghutangList(ByVal q As String) As String
        Dim tmpDT As DataTable = GetKategoriPenghutang(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetKategoriPenghutang(kod As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT Kod_Detail AS value, Butiran AS text FROM SMKB_Lookup_Detail WHERE Kod='0152'"
        Return db.Read(query)
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
        Dim query As String = "SELECT Kod_Urusniaga AS value,Butiran AS text FROM SMKB_Kod_Urusniaga WHERE Status='1'"
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
    Public Function GetTempohBayaran(ByVal q As String) As String
        'Dim newList As List(Of ItemList)
        'newList = listItem

        'If (q <> "") Then
        '    newList = listItem.Where(
        '        Function(x) x.value.Contains(q) Or x.text.Contains(q)
        '    ).ToList()
        'End If

        Dim tmpDT As DataTable = GetKodTempohBayaran(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetKodTempohBayaran(kod As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT Kod_Detail AS value, Butiran AS text FROM SMKB_Lookup_Detail WHERE Kod='AR09' AND Status='1'"
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
        'Dim query As String = "SELECT top 10 CONCAT(a.Kod_Vot, ' - ', vot.Butiran, ', ', a.Kod_Operasi, ' - ', ko.Butiran, ', ', a.Kod_Projek, ', ', a.Kod_Kump_Wang, ' - ', REPLACE(kw.Butiran, 'KUMPULAN WANG', 'KW'), ', ', a.Kod_PTJ) AS text,
        '                    a.Kod_Kump_Wang + a.Kod_Operasi + a.Kod_PTJ + a.Kod_Projek + a.Kod_Vot AS value 
        '                    FROM SMKB_COA_Master AS a
        '                    JOIN SMKB_Vot AS vot ON a.Kod_Vot = vot.Kod_Vot
        '                    JOIN SMKB_Operasi AS ko ON a.Kod_Operasi = ko.Kod_Operasi
        '                    JOIN SMKB_Kump_Wang AS kw ON a.Kod_Kump_Wang = kw.Kod_Kump_Wang
        '                    WHERE a.status = 1 "
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
            'query &= "AND (a.Kod_Vot LIKE '%' + @kod + '%' OR a.Kod_Operasi LIKE '%' + @kod2 + '%' OR a.Kod_Projek LIKE '%' + @kod3 + '%' OR a.Kod_Kump_Wang LIKE '%' + @kod4 + '%' OR a.Kod_PTJ LIKE '%' + @kod5 + '%' OR vot.Butiran LIKE '%' + @kodButir + '%' OR ko.Butiran LIKE '%' + @kodButir1 + '%'  OR kw.Butiran LIKE '%' + @kodButir2 + '%')"
            query &= "AND (a.Kod_Vot LIKE '%' + @kod + '%' OR a.Kod_Operasi LIKE '%' + @kod2 + '%' OR a.Kod_Projek LIKE '%' + @kod3 + '%' OR a.Kod_Kump_Wang LIKE '%' + @kod4 + '%' OR a.Kod_PTJ LIKE '%' + @kod5 + '%' OR vot.Butiran LIKE '%' + @kodButir + '%' OR ko.Butiran LIKE '%' + @kodButir1 + '%'  OR kw.Butiran LIKE '%' + @kodButir2 + '%' OR mj.pejabat LIKE '%' + @kodButir3 + '%')"
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

    <WebMethod(EnableSession:=True)>
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
            If order.Jumlah = "" Then
                order.Jumlah = "0.00"
            Else
                order.Jumlah = order.Jumlah
            End If
            If InsertNewOrder(order.OrderID, order.PenghutangID, order.TkhMula, order.TkhTamat, order.Kontrak, order.JenisUrusniaga, order.Tujuan, order.Jumlah, order.tkhBil, order.tempoh, order.tempohbyrn, order.norujukan, "01") <> "OK" Then
                resp.Failed("Gagal Menyimpan order")
                Exit Function
            End If
        Else

            If Updateorder(order.OrderID, order.PenghutangID, order.TkhMula, order.TkhTamat, order.Kontrak, order.JenisUrusniaga, order.Tujuan, order.Jumlah, order.tkhBil, order.tempoh, order.tempohbyrn, order.norujukan) <> "OK" Then
                resp.Failed("Gagal Menyimpan order")
                Exit Function
            End If
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

    'SubmitOrders
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SubmitOrders(order As Order_inv) As String
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
            If order.Jumlah = "" Then
                order.Jumlah = "0.00"
            Else
                order.Jumlah = order.Jumlah
            End If
            If InsertNewOrder(order.OrderID, order.PenghutangID, order.TkhMula, order.TkhTamat, order.Kontrak, order.JenisUrusniaga, order.Tujuan, order.Jumlah, order.tkhBil, order.tempoh, order.tempohbyrn, order.norujukan, "02") <> "OK" Then
                resp.Failed("Gagal Menyimpan order")
                Exit Function
            End If
        Else

            If UpdateSubmitorder(order.OrderID, order.PenghutangID, order.TkhMula, order.TkhTamat, order.Kontrak, order.JenisUrusniaga, order.Tujuan, order.Jumlah, "02", order.tkhBil, order.tempoh, order.tempohbyrn, order.norujukan) <> "OK" Then
                resp.Failed("Gagal Menyimpan order")
                Exit Function
            End If
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

    'SubmitOrdersKaunter
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SubmitOrdersKaunter(order As Order_Resit) As String
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
            order.OrderIDResit = GenerateOrderIDResit()
            If order.Jumlah = "" Then
                order.Jumlah = "0.00"
            Else
                order.Jumlah = order.Jumlah
            End If
            If InsertNewOrder(order.OrderID, order.PenghutangID, order.TkhMula, order.TkhTamat, order.Kontrak, order.JenisUrusniaga, order.Tujuan, order.Jumlah, order.tkhBil, order.tempoh, order.tempohbyrn, order.norujukan, "03") <> "OK" Then
                resp.Failed("Gagal Menyimpan order")
                Exit Function
            End If
            If InsertNewOrderResit(order.OrderID, order.OrderIDResit, order.PenghutangID, order.TkhMula, order.TkhTamat, order.Kontrak, order.JenisUrusniaga, order.Tujuan, order.Jumlah, order.tkhBil, order.tempoh, order.tempohbyrn, order.norujukan, order.modterima, order.bankutem, "06") <> "OK" Then
                resp.Failed("Gagal Menyimpan order")
                Exit Function
            End If
        Else

            If UpdateSubmitorder(order.OrderID, order.PenghutangID, order.TkhMula, order.TkhTamat, order.Kontrak, order.JenisUrusniaga, order.Tujuan, order.Jumlah, "02", order.tkhBil, order.tempoh, order.tempohbyrn, order.norujukan) <> "OK" Then
                resp.Failed("Gagal Menyimpan order")
                Exit Function
            End If
        End If

        For Each orderDetail As OrderDetail_Resit In order.OrderDetails

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

            If orderDetail.id = "" Then
                orderDetail.id = GenerateOrderDetailID(order.OrderID)
                orderDetail.OrderID = order.OrderID
                orderDetail.OrderIDResit = order.OrderIDResit
                If InsertOrderDetailKaunterBil(orderDetail) = "OK" Then
                    success += 1
                End If
                If InsertOrderDetailKaunter(orderDetail) = "OK" Then
                    'success += 1
                End If
            Else
                If UpdateOrderDetailKaunter(orderDetail) = "OK" Then
                    success += 1
                End If
            End If
            '--- TAMBAHAN FUNCTION CALL POSTING KE LAJER PENGHUTANG ----
            'Try
            '    Dim myGetTicket As New TokenResponseModel()

            '    Dim servicex As New ValuesService()
            '    Dim kodkw = orderDetail.ddlKw
            '    Dim kodko = orderDetail.ddlKo
            '    Dim kodkp = orderDetail.ddlKp
            '    Dim kodptj = orderDetail.ddlPTJ 'SEMAK SEMULA BETUL KE COA?
            '    Dim kodvot = order.bankutem
            '    Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("ms-MY")
            '    Dim parsedDate As Date = CDate(Now()).ToString("yyyy-MM-dd")
            '    Dim vBulan As String = parsedDate.Month
            '    Dim vTahun As String = parsedDate.Year
            '    Dim values As String = Await servicex.SendDataLejar(myGetTicket.GetTicket("smkb", Session("ssusrID")),
            '                            "GL", "UTeM", kodkw, kodptj,
            '                            kodvot, kodko, kodkp, order.JumlahTerima, "DR", vBulan, vTahun)

            '    If values.Contains("ok") Then

            '        '    'lblModalMessaage.Text = "Rekod telah disimpan" 'message di modal
            '        '    'ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)

            '    Else
            '        '    'lblModalMessaage.Text = "Rekod Gagal disimpan" 'message di modal
            '        '    'ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
            '    End If
            'Catch
            '    'lblModalMessaage.Text = "Invalid Token" 'message di modal
            '    'ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)

            'End Try
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

    Private Function InsertNewOrder(orderid As String, penghutangid As String, TkhMula As String, TkhTamat As String, Kontrak As String, JenisUrusniaga As String, Tujuan As String, Jumlah As String, tkhBil As String, tempoh As String, tempohbyrn As String, norujukan As String, statusdok As String)
        Dim db As New DBKewConn
        Dim query As String = "INSERT INTO SMKB_Bil_Hdr (No_Bil,Tkh_Mohon,Kod_Status_Dok, Status, Kod_Penghutang,Tkh_Mula,Tkh_Tamat,Kontrak,Kod_Urusniaga,Tujuan, Jumlah,No_Staf_Penyedia,Tkh_Bil, Tempoh_Kontrak, Jenis_Tempoh,No_Rujukan)
        VALUES(@No_Bil, getdate(), @statusdok ,'1',@Kod_Penghutang,@Tkh_Mula,@Tkh_Tamat,@Kontrak,@Kod_Urusniaga,@Tujuan,@Jumlah,@NoStafPenyedia,@tkhbil,@tempoh,@tempohbyrn, @norujukan)"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@No_Bil", orderid))
        param.Add(New SqlParameter("@statusdok", statusdok))
        param.Add(New SqlParameter("@Kod_Penghutang", penghutangid))
        param.Add(New SqlParameter("@Tkh_Mula", tkhBil))
        param.Add(New SqlParameter("@Tkh_Tamat", TkhTamat))
        param.Add(New SqlParameter("@Kontrak", Kontrak))
        param.Add(New SqlParameter("@Kod_Urusniaga", JenisUrusniaga))
        param.Add(New SqlParameter("@Tujuan", Tujuan))
        param.Add(New SqlParameter("@Jumlah", Jumlah))
        param.Add(New SqlParameter("@NoStafPenyedia", Session("ssusrID")))
        param.Add(New SqlParameter("@tkhbil", tkhBil))
        param.Add(New SqlParameter("@tempoh", "0"))
        param.Add(New SqlParameter("@tempohbyrn", "01"))
        param.Add(New SqlParameter("@norujukan", norujukan))


        Return db.Process(query, param)
    End Function

    Private Function InsertNewOrderResit(orderid As String, orderidresit As String, penghutangid As String, TkhMula As String, TkhTamat As String, Kontrak As String, JenisUrusniaga As String, Tujuan As String, Jumlah As String, tkhBil As String, tempoh As String, tempohbyrn As String, norujukan As String, modterima As String, bankutem As String, statusdok As String)
        Dim db As New DBKewConn
        Dim query As String = "INSERT INTO SMKB_Terima_Hdr (No_Dok, Kod_Penghutang, No_Rujukan,Tujuan,Kod_Urusniaga,Mod_Terima,Tkh_Daftar,Staf_Terima,Jumlah_Sebenar,Jumlah_Bayar,Kod_Bank,Kod_Terima,Kod_Status_Dok,Flag_Kaunter)
                               VALUES (@noresit,@Kod_Penghutang,@No_Bil,@Tujuan,@Kod_Urusniaga,@modterima,@tarikh,@No_Staf,@Jumlah,@jumlahterima,@kodbank,'TK',@status,@flag)"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@noresit", orderidresit))
        param.Add(New SqlParameter("@Kod_Penghutang", penghutangid))
        param.Add(New SqlParameter("@No_Bil", orderid))
        param.Add(New SqlParameter("@Tujuan", Tujuan))
        param.Add(New SqlParameter("@Kod_Urusniaga", JenisUrusniaga))
        param.Add(New SqlParameter("@modterima", modterima))
        param.Add(New SqlParameter("@tarikh", tkhBil))
        param.Add(New SqlParameter("@No_Staf", Session("ssusrID")))
        param.Add(New SqlParameter("@Jumlah", Jumlah))
        param.Add(New SqlParameter("@jumlahterima", Jumlah))
        param.Add(New SqlParameter("@kodbank", bankutem))
        param.Add(New SqlParameter("@status", statusdok))
        param.Add(New SqlParameter("@flag", "1"))

        Return db.Process(query, param)
    End Function


    Public Function InsertOrderDetail(orderDetail As OrderDetail_inv)
        Dim db = New DBKewConn
        Dim query As String = "INSERT INTO SMKB_Bil_Dtl(No_Bil,No_Item,Kod_Kump_Wang,Kod_Operasi,Kod_PTJ,Kod_Projek,Kod_Vot,Perkara,Kuantiti,Kadar_Harga,Jumlah,Tahun,Status,Diskaun,Cukai)
        VALUES( @no_bil , @no_item, @Kod_Kump_Wang, @Kod_Operasi , @Kod_PTJ , @Kod_Projek  , @Kod_Vot, @Perkara, @Kuantiti, @Kadar_Harga, @Jumlah, '2023', '1', @Diskaun, @Cukai)    "
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

    Public Function InsertOrderDetailKaunterBil(orderDetail As OrderDetail_Resit)
        Dim db = New DBKewConn
        Dim query As String = "INSERT INTO SMKB_Bil_Dtl(No_Bil,No_Item,Kod_Kump_Wang,Kod_Operasi,Kod_PTJ,Kod_Projek,Kod_Vot,Perkara,Kuantiti,Kadar_Harga,Jumlah,Tahun,Status,Diskaun,Cukai)
        VALUES( @no_bil , @no_item, @Kod_Kump_Wang, @Kod_Operasi , @Kod_PTJ , @Kod_Projek  , @Kod_Vot, @Perkara, @Kuantiti, @Kadar_Harga, @Jumlah, '2023', '1', @Diskaun, @Cukai)    "
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

    Public Function InsertOrderDetailKaunter(orderDetail As OrderDetail_Resit)
        Dim db = New DBKewConn
        Dim query As String = "INSERT INTO SMKB_Terima_Dtl
                                VALUES( @noresit , @no_item, @Kod_Kump_Wang, @Kod_Operasi , @Kod_PTJ , @Kod_Projek  , @Kod_Vot, @Perkara,'','0.00', @Jumlah, @Cukai,'1')"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@noresit", orderDetail.OrderIDResit))
        param.Add(New SqlParameter("@no_item", orderDetail.id))
        param.Add(New SqlParameter("@Kod_Kump_Wang", orderDetail.ddlKw))
        param.Add(New SqlParameter("@Kod_Operasi", orderDetail.ddlKo))
        param.Add(New SqlParameter("@Kod_PTJ", orderDetail.ddlPTJ))
        param.Add(New SqlParameter("@Kod_Projek", orderDetail.ddlKp))
        param.Add(New SqlParameter("@Kod_Vot", orderDetail.ddlVot))
        param.Add(New SqlParameter("@Perkara", orderDetail.details))
        param.Add(New SqlParameter("@Jumlah", orderDetail.amount))
        param.Add(New SqlParameter("@Cukai", orderDetail.Cukai))


        Return db.Process(query, param)
    End Function


    Private Function Updateorder(orderid As String, penghutangid As String, TkhMula As String, TkhTamat As String, Kontrak As String, JenisUrusniaga As String, Tujuan As String, Jumlah As String, tkhBil As String, tempoh As String, tempohbyrn As String, norujukan As String)
        Dim db As New DBKewConn
        Dim query As String = "UPDATE SMKB_Bil_Hdr 
		SET Kod_Penghutang=@Kod_Penghutang,Tkh_Mula=@Tkh_Mula,Tkh_Tamat=@Tkh_Tamat,Kontrak=@Kontrak,Kod_Urusniaga=@Kod_Urusniaga,Tujuan=@Tujuan,Jumlah=@Jumlah,Tkh_Bil=@tkhbil,Tempoh_Kontrak=@tempoh,Jenis_Tempoh=@tempohbyrn, No_Rujukan=@norujukan
		WHERE No_Bil=@no_bil"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@No_Bil", orderid))
        param.Add(New SqlParameter("@Kod_Penghutang", penghutangid))
        param.Add(New SqlParameter("@Tkh_Mula", tkhBil))
        param.Add(New SqlParameter("@Tkh_Tamat", TkhTamat))
        param.Add(New SqlParameter("@Kontrak", Kontrak))
        param.Add(New SqlParameter("@Kod_Urusniaga", JenisUrusniaga))
        param.Add(New SqlParameter("@Tujuan", Tujuan))
        param.Add(New SqlParameter("@Jumlah", Jumlah))
        param.Add(New SqlParameter("@tkhbil", tkhBil))
        param.Add(New SqlParameter("@tempoh", tempoh))
        param.Add(New SqlParameter("@tempohbyrn", tempohbyrn))
        param.Add(New SqlParameter("@norujukan", norujukan))

        Return db.Process(query, param)
    End Function
    Private Function UpdateSubmitorder(orderid As String, penghutangid As String, TkhMula As String, TkhTamat As String, Kontrak As String, JenisUrusniaga As String, Tujuan As String, Jumlah As String, status As String, tkhBil As String, tempoh As String, tempohbyrn As String, norujukan As String)
        Dim db As New DBKewConn
        Dim query As String = "UPDATE SMKB_Bil_Hdr 
		SET Kod_Penghutang=@Kod_Penghutang,Tkh_Mula=@Tkh_Mula,Tkh_Tamat=@Tkh_Tamat,Kontrak=@Kontrak,Kod_Urusniaga=@Kod_Urusniaga,Tujuan=@Tujuan,Jumlah=@Jumlah, Kod_Status_Dok=@status,Tkh_Bil=@tkhbil,Tempoh_Kontrak=@tempoh,Jenis_Tempoh=@tempohbyrn, No_Rujukan=@norujukan
		WHERE No_Bil=@no_bil"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@No_Bil", orderid))
        param.Add(New SqlParameter("@Kod_Penghutang", penghutangid))
        param.Add(New SqlParameter("@Tkh_Mula", tkhBil))
        param.Add(New SqlParameter("@Tkh_Tamat", TkhTamat))
        param.Add(New SqlParameter("@Kontrak", Kontrak))
        param.Add(New SqlParameter("@Kod_Urusniaga", JenisUrusniaga))
        param.Add(New SqlParameter("@Tujuan", Tujuan))
        param.Add(New SqlParameter("@Jumlah", Jumlah))
        param.Add(New SqlParameter("@status", status))
        param.Add(New SqlParameter("@tkhbil", tkhBil))
        param.Add(New SqlParameter("@tempoh", tempoh))
        param.Add(New SqlParameter("@tempohbyrn", tempohbyrn))
        param.Add(New SqlParameter("@norujukan", norujukan))

        Return db.Process(query, param)
    End Function

    Public Function UpdateOrderDetail(orderDetail As OrderDetail_inv)
        Dim db = New DBKewConn
        Dim data
        data = Split(orderDetail.id, "|")
        Dim nobil = data(0)
        Dim noitem = data(1)
        Dim query As String = "UPDATE SMKB_Bil_Dtl
                                SET Kod_Kump_Wang=@Kod_Kump_Wang,Kod_Operasi=@Kod_Operasi,Kod_PTJ=@Kod_PTJ,Kod_Projek=@Kod_Projek,Kod_Vot=@Kod_Vot,
                                Perkara=@Perkara,Kuantiti=@Kuantiti,Kadar_Harga=@Kadar_Harga,Jumlah=@Jumlah,Tahun='2023',Status='1',Diskaun=@Diskaun,Cukai=@Cukai
                                WHERE No_Bil=@no_bil AND No_Item=@no_item"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@no_bil", nobil))
        param.Add(New SqlParameter("@no_item", noitem))
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

    Public Function UpdateOrderDetailKaunter(orderDetail As OrderDetail_Resit)
        Dim db = New DBKewConn
        Dim data
        data = Split(orderDetail.id, "|")
        Dim nobil = data(0)
        Dim noitem = data(1)
        Dim query As String = "UPDATE SMKB_Bil_Dtl
                                SET Kod_Kump_Wang=@Kod_Kump_Wang,Kod_Operasi=@Kod_Operasi,Kod_PTJ=@Kod_PTJ,Kod_Projek=@Kod_Projek,Kod_Vot=@Kod_Vot,
                                Perkara=@Perkara,Kuantiti=@Kuantiti,Kadar_Harga=@Kadar_Harga,Jumlah=@Jumlah,Tahun='2023',Status='1',Diskaun=@Diskaun,Cukai=@Cukai
                                WHERE No_Bil=@no_bil AND No_Item=@no_item"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@no_bil", nobil))
        param.Add(New SqlParameter("@no_item", noitem))
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

        Dim query As String = $"select TOP 1 No_Akhir as id from SMKB_No_Akhir where Kod_Modul ='12' AND Prefix ='AR' AND Tahun =@year"
        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@year", year))

        dt = db.Read(query, param)

        If dt.Rows.Count > 0 Then
            lastID = CInt(dt.Rows(0).Item("id")) + 1

            UpdateNoAkhir("12", "AR", year, lastID)
        Else

            InsertNoAkhir("12", "AR", year, lastID)
        End If
        newOrderID = "AR" + Format(lastID, "000000").ToString + month.ToString("00") + Right(year.ToString(), 2)

        Return newOrderID
    End Function

    Private Function GenerateOrderIDResit() As String
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
    Public Function LoadHdrInvois(ByVal id As String) As String
        Dim resp As New ResponseRepository

        dt = GetHdrInvois(id)
        resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    Private Function GetHdrInvois(id As String) As DataTable
        Dim db = New DBKewConn

        Dim query As String = "SELECT A.No_Bil, A.Kod_Penghutang, B.Nama_Penghutang, B.Emel, B.Kategori_Penghutang, D.Butiran as Butiran_Kategori, A.Kod_Urusniaga, C.Butiran, A.Kontrak, A.Tujuan, Kod_Status_Dok,
                                CASE WHEN Tkh_Mula <> '' THEN FORMAT(Tkh_Mula, 'yyyy-MM-dd') END AS TkhMula,
                                CASE WHEN Tkh_Tamat <> '' THEN FORMAT(Tkh_Tamat, 'yyyy-MM-dd') END AS TkhTamat,Tkh_Mula,Tkh_Tamat,Tkh_Bil,A.No_Rujukan,Tempoh_Kontrak,Jenis_Tempoh,F.Butiran AS JenisTempoh
                                FROM SMKB_Bil_Hdr A
                                LEFT JOIN SMKB_Penghutang_Master B ON A.Kod_Penghutang=B.Kod_Penghutang
                                LEFT JOIN SMKB_Kod_Urusniaga C ON A.Kod_Urusniaga=C.Kod_Urusniaga
                                LEFT JOIN SMKB_Lookup_Detail D ON B.Kategori_Penghutang = D.Kod_Detail
                                LEFT JOIN SMKB_Lookup_Detail F ON A.Jenis_Tempoh=F.Kod_Detail AND F.Kod='AR09'
                                WHERE No_Bil = @No_Invois AND A.Status='1' AND D.Kod='0152'"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@No_Invois", id))

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
        Kod_Vot , (select Kod_Vot+' - '+ Butiran from SMKB_Vot as vot where a.Kod_Vot = vot.Kod_Vot) as ButiranVot,
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

        Dim query As String = "select a.No_Bil,Perkara ,Kuantiti, Kadar_Harga, Jumlah, Diskaun, Cukai
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
    Public Function LoadRecordBil_Header(ByVal id As String) As String
        Dim resp As New ResponseRepository

        dt = GetTransaksiBil_Header(id)
        resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    Private Function GetTransaksiBil_Header(kod As String) As DataTable
        Dim db = New DBKewConn

        Dim query As String = "SELECT A.No_Bil,A.Kod_Penghutang,B.Nama_Penghutang,A.Kod_Urusniaga,C.Butiran, A.Kontrak,A.Tujuan,Kod_Status_Dok,
                                CASE WHEN Tkh_Lulus <> '' THEN FORMAT(Tkh_Lulus, 'dd/MM/yyyy') END AS Tkh_Lulus,
                                CASE WHEN Tkh_Tamat <> '' THEN FORMAT(Tkh_Tamat, 'yyyy-MM-dd') END AS Tkh_Tamat,
                                (SELECT SUM(a.Jumlah) FROM SMKB_Bil_Dtl as a where No_Bil = @No_Bil and status = 1 ) AS Jumlah, 
                                (SELECT SUM(a.Diskaun) FROM SMKB_Bil_Dtl as a where No_Bil = @No_Bil and status = 1 ) AS Diskaun,
                                (SELECT SUM(a.Cukai) FROM SMKB_Bil_Dtl as a where No_Bil = @No_Bil and status = 1 ) AS Cukai,
                                Jumlah as JumlahSebenar
                                FROM SMKB_Bil_Hdr A
                                INNER JOIN SMKB_Penghutang_Master B ON A.Kod_Penghutang=B.Kod_Penghutang
                                INNER JOIN SMKB_Kod_Urusniaga C ON A.Kod_Urusniaga=C.Kod_Urusniaga
                                WHERE No_Bil = @No_Bil  AND A.Status='1'"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@No_Bil", kod))

        Return db.Read(query, param)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadOrderRecord_SenaraiTransaksiInvois(category_filter As String, isClicked As Boolean, tkhMula As DateTime, tkhTamat As DateTime) As String
        Dim resp As New ResponseRepository

        If isClicked = False Then
            Return JsonConvert.SerializeObject(New DataTable)
        End If

        dt = GetOrder_SenaraiTransaksiInvois(category_filter, tkhMula, tkhTamat)
        'resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(dt)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetOrder_SenaraiTransaksiInvois(category_filter As String, tkhMula As DateTime, tkhTamat As DateTime) As DataTable
        Dim db = New DBKewConn
        Dim tarikhQuery As String = ""
        Dim param As List(Of SqlParameter)

        If category_filter = "1" Then 'Harini
            tarikhQuery = " AND CAST(Tkh_Mohon AS DATE) = CAST(CURRENT_TIMESTAMP AS DATE) "
        ElseIf category_filter = "2" Then 'Semalam
            tarikhQuery = " AND CAST(Tkh_Mohon AS DATE) = CAST(DATEADD(day, -1, CURRENT_TIMESTAMP) AS DATE) "
        ElseIf category_filter = "3" Then 'seminggu
            tarikhQuery = " AND Tkh_Mohon >= DATEADD(day, -7, CURRENT_TIMESTAMP) AND Tkh_Mohon < CURRENT_TIMESTAMP "
        ElseIf category_filter = "4" Then '30 hari
            tarikhQuery = " AND Tkh_Mohon >= DATEADD(day, -30, CURRENT_TIMESTAMP) AND Tkh_Mohon < CURRENT_TIMESTAMP "
        ElseIf category_filter = "5" Then '60 hari
            tarikhQuery = " AND Tkh_Mohon >= DATEADD(day, -60, CURRENT_TIMESTAMP) AND Tkh_Mohon < CURRENT_TIMESTAMP "
        ElseIf category_filter = "6" Then 'custom
            tarikhQuery = " AND Tkh_Mohon >= @tkhMula AND Tkh_Mohon <= @TkhTamat "
            param = New List(Of SqlParameter)
            param.Add(New SqlParameter("@tkhMula", tkhMula))
            param.Add(New SqlParameter("@TkhTamat", tkhTamat))
        End If

        Dim query As String = "SELECT A.No_Bil as No_Bil,B.Nama_Penghutang,C.Butiran as UrusNiaga,A.Tujuan,
                                CASE WHEN Tkh_Mula <> '' THEN FORMAT(Tkh_Mula, 'dd/MM/yyyy') END AS Tkh_Mula,
                                CASE WHEN Tkh_Tamat <> '' THEN FORMAT(Tkh_Tamat, 'dd/MM/yyyy') END AS Tkh_Tamat,
                                CASE WHEN Tkh_Mohon <> '' THEN FORMAT(Tkh_Mohon, 'dd/MM/yyyy') END AS Tkh_Mohon,
                                CASE WHEN Tkh_Lulus <> '' THEN FORMAT(Tkh_Lulus, 'dd/MM/yyyy') END AS Tkh_Lulus,
                                A.Jumlah,D.Butiran AS STATUS_BIL,
                                CASE 
                                WHEN A.No_Staf_Penyedia  <> '' THEN E.MS01_Nama 
                                END
                                AS Penyedia,A.No_Staf_Penyedia, (SELECT COUNT(No_Item) FROM SMKB_Bil_Dtl WHERE SMKB_Bil_Dtl.No_Bil = A.No_Bil ) AS NO_ITEM
                                FROM SMKB_Bil_Hdr A
                                inner JOIN SMKB_Penghutang_Master B ON A.Kod_Penghutang=B.Kod_Penghutang
                                inner JOIN SMKB_Kod_Urusniaga C ON A.Kod_Urusniaga=C.Kod_Urusniaga
                                INNER JOIN SMKB_Kod_Status_Dok D ON A.Kod_Status_Dok=D.Kod_Status_Dok AND Kod_Modul='12'
                                Left join [DEVMIS\SQL_INS01].dbStaf.dbo.MS01_Peribadi as E on E.MS01_NoStaf = A.No_Staf_Penyedia 
                                WHERE  A.Status='1' " & tarikhQuery & "
                                ORDER BY Tkh_Mohon , No_Bil"

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

        'Check all required field is not empty
        If penghutang.Nama = "" Or penghutang.KategoriPenghutang = "" Or penghutang.NoTelefon = "" Or penghutang.Email = "" Or penghutang.Id = "" Then
            resp.Failed("Sila isi semua ruangan yang diperlukan")
            Return JsonConvert.SerializeObject(resp.GetResult())
        Else
            'Do insertNewPenghutang if No_Rujukan is not exist in SMKB_Penghutang_Master
            If penghutang.IdPenghutang = "" Then
                If CheckPenghutangExist(penghutang) = False Then
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

        'Dim result As String = db.Process(query, param)

        Dim dt As DataTable
        dt = db.Read(query, param)

        If dt.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

        ' Check if the result is not null or empty
        'Return Not String.IsNullOrEmpty(result)
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
            UpdateNoAkhirPenghutang("12", "PH", year, lastID)
        Else
            InsertNoAkhirPenghutang("12", "PH", year, lastID)
        End If
        newPenghutangID = "PH" + Format(lastID, "000000").ToString + Right(year.ToString(), 2)

        Return newPenghutangID
    End Function
    Private Function UpdateNoAkhirPenghutang(kodModul As String, prefix As String, year As String, ID As String)
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

    Private Function InsertNoAkhirPenghutang(kodModul As String, prefix As String, year As String, ID As String)
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

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetPoskodValue(ByVal q As String) As String
        Dim tmpDT As DataTable = GetPoskodValueFromKod(q)
        Return JsonConvert.SerializeObject(tmpDT)
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
End Class