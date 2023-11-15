Imports System.ComponentModel
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports Newtonsoft.Json
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
'Imports System.Web.Http
Imports System.Data.SqlClient

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class Transaksi_InvoisWS
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
        Dim query As String = "SELECT top 10 CONCAT(a.Kod_Vot, ' - ', vot.Butiran, ', ', a.Kod_Operasi, ' - ', ko.Butiran, ' , ', a.Kod_Projek, ' - ', kp.Butiran, ', ', a.Kod_Kump_Wang, ' - ', 
					REPLACE(kw.Butiran, 'KUMPULAN WANG', 'KW'), ', ', LEFT(a.Kod_PTJ,2), ' - ', mj.Pejabat) AS text,
                    a.Kod_Vot AS value ,
                    mj.Pejabat as colPTJ , kw.Butiran as colKW , ko.Butiran as colKO , kp.Butiran as colKp , 
                    a.Kod_PTJ as colhidptj , a.Kod_Kump_Wang as colhidkw , a.Kod_Operasi as colhidko , a.Kod_Projek as colhidkp
                    FROM SMKB_COA_Master AS a 
                    JOIN SMKB_Vot AS vot ON a.Kod_Vot = vot.Kod_Vot and vot.Kod_Klasifikasi = 'D'
                    JOIN SMKB_Operasi AS ko ON a.Kod_Operasi = ko.Kod_Operasi
                    JOIN SMKB_Kump_Wang AS kw ON a.Kod_Kump_Wang = kw.Kod_Kump_Wang
					JOIN SMKB_Projek as kp on kp.Kod_Projek = a.Kod_Projek
					JOIN [DEVMIS\SQL_INS01].dbStaf.dbo.MS_PEJABAT AS mj ON mj.status = '1' and mj.kodpejabat = left(a.Kod_PTJ,2) 
                    WHERE a.status = 1"

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
    Public Function GetJenInvList(ByVal q As String) As String
        Dim tmpDT As DataTable = GetKodJenInvList(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetKodJenInvList(kod As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT Kod as value, Kod + ' - ' + Butiran as text FROM SMKB_Pembayaran_Jenis_Invois WHERE Status='1'"
        Dim param As New List(Of SqlParameter)
        If kod <> "" Then
            query &= " AND Kod LIKE '%' + @kod + '%' OR Butiran LIKE '%' + @kod2 + '%' "
            param.Add(New SqlParameter("@kod", kod))
            param.Add(New SqlParameter("@kod2", kod))
        End If

        Return db.Read(query, param)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetJenByrList(ByVal q As String) As String
        Dim tmpDT As DataTable = GetKodJenByrList(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetKodJenByrList(kod As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT Kod as value, Kod + ' - ' + Butiran as text FROM SMKB_Pembayaran_Jenis_Bayar WHERE Status='1'"
        Dim param As New List(Of SqlParameter)
        If kod <> "" Then
            query &= " AND Kod LIKE '%' + @kod + '%' OR Butiran LIKE '%' + @kod2 + '%' "
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

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function DeleteOrder(ByVal id As String) As String
        Dim resp As New ResponseRepository

        DeleteOrderDetails("", id)


        If DeleteOrderRecord(id) <> "OK" Then
            resp.Failed("Gagal")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        resp.Success("Order telah dihapuskan")
        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

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

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SaveOrders(order As POrder) As String
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
            If InsertNewOrder(order.OrderID, order.JenisInvois, order.JenisBayar, order.NoInvois, order.TkhInvois, order.TkhTerimaInvois, order.NoDO, order.TkhDO, order.TkhTerimaDO, order.Tujuan, order.Jumlah) <> "OK" Then
                resp.Failed("Gagal Menyimpan order")
                Return JsonConvert.SerializeObject(resp.GetResult())
                Exit Function
            End If
            order.Bil = GenerateOrderBil(order.OrderID)
            If InsertNewPenerima(order.OrderID, order.Bil, order.BayarKepada, order.Jumlah) <> "OK" Then
                resp.Failed("Gagal Menyimpan order")
                Exit Function
            End If
        Else
            If UpdateNewOrder(order.OrderID, order.JenisInvois, order.JenisBayar, order.NoInvois, order.TkhInvois, order.TkhTerimaInvois, order.NoDO, order.TkhDO, order.TkhTerimaDO, order.Tujuan, order.Jumlah) <> "OK" Then
                resp.Failed("Gagal Menyimpan order")
                Exit Function
            End If
            If UpdateNewPenerima(order.OrderID, order.Bil, order.BayarKepada, order.Jumlah) <> "OK" Then
                resp.Failed("Gagal Menyimpan order")
                Exit Function
            End If
        End If

        'If order.OrderID <> "" Then
        '    order.Bil = GenerateOrderBil(order.OrderID)
        '    If InsertNewPenerima(order.OrderID, order.Bil, order.BayarKepada, order.Jumlah) <> "OK" Then
        '        resp.Failed("Gagal Menyimpan order")
        '        Exit Function
        '    End If
        'End If

        For Each orderDetail As POrderDetail In order.OrderDetails

            If orderDetail.ddlPTJ = "" Then
                Continue For
            End If

            If orderDetail.cukai = "" Then
                orderDetail.cukai = 0.00
            End If

            If orderDetail.diskaun = "" Then
                orderDetail.diskaun = 0.00
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
            'resp.Success("Rekod berjaya disimpan", "00", order)
        End If

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    Private Function InsertNewOrder(OrderID As String, JenisInvois As String, JenisBayar As String, NoInvois As String, TkhInvois As String, TkhTerimaInvois As String, NoDO As String, TkhDO As String, TkhTerimaDO As String, Tujuan As String, Jumlah As String)
        Dim db As New DBKewConn
        Dim query As String = "INSERT INTO SMKB_Pembayaran_Invois_Hdr (ID_Rujukan,No_Invois,Tarikh_Invois,Tarikh_Terima_Invois, No_DO,Tarikh_DO,Tarikh_Terima_DO,Jenis_Bayar,Jumlah_Sebenar,Jenis_Invois,Tujuan, tarikh_Daftar, Dibuat_Oleh, Tarikh_Dibuat, Status_Dok)
        VALUES(@ID_Invois, @No_Invois, @Tarikh_Invois, @Tarikh_Terima_Invois, @No_DO, @Tarikh_DO, @Tarikh_Terima_DO, @Jenis_Bayar, @Jumlah_Sebenar, @Jenis_Invois, @Tujuan, getdate(), @Dibuat_Oleh, getdate(), @Status_Dok)"
        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@ID_Invois", OrderID))
        param.Add(New SqlParameter("@No_Invois", NoInvois))
        param.Add(New SqlParameter("@Tarikh_Invois", TkhInvois))
        param.Add(New SqlParameter("@Tarikh_Terima_Invois", TkhTerimaInvois))
        param.Add(New SqlParameter("@No_DO", NoDO))
        param.Add(New SqlParameter("@Tarikh_DO", TkhDO))
        param.Add(New SqlParameter("@Tarikh_Terima_DO", TkhTerimaDO))
        param.Add(New SqlParameter("@Jenis_Bayar", JenisBayar))
        param.Add(New SqlParameter("@Jumlah_Sebenar", Jumlah))
        param.Add(New SqlParameter("@Jenis_Invois", JenisInvois))
        param.Add(New SqlParameter("@Tujuan", Tujuan))
        param.Add(New SqlParameter("@Dibuat_Oleh", "02636"))
        param.Add(New SqlParameter("@Status_Dok", "01"))

        Return db.Process(query, param)
    End Function

    Private Function UpdateNewOrder(OrderID As String, JenisInvois As String, JenisBayar As String, NoInvois As String, TkhInvois As String, TkhTerimaInvois As String, NoDO As String, TkhDO As String, TkhTerimaDO As String, Tujuan As String, Jumlah As String)
        Dim db As New DBKewConn
        Dim query As String = "UPDATE SMKB_Pembayaran_Invois_Hdr SET No_Invois = @No_Invois , Tarikh_Invois =@Tarikh_Invois , Tarikh_Terima_Invois =@Tarikh_Terima_Invois , No_DO =@No_DO,
                               Tarikh_DO = @Tarikh_DO, Jenis_Bayar = @Jenis_Bayar, Jumlah_Sebenar = @Jumlah_Sebenar, Jenis_Invois = @Jenis_Invois, Tujuan = @Tujuan
                               WHERE ID_Rujukan=@ID_Rujukan AND Status = 1"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@ID_Rujukan", OrderID))
        param.Add(New SqlParameter("@No_Invois", NoInvois))
        param.Add(New SqlParameter("@Tarikh_Invois", TkhInvois))
        param.Add(New SqlParameter("@Tarikh_Terima_Invois", TkhTerimaInvois))
        param.Add(New SqlParameter("@No_DO", NoDO))
        param.Add(New SqlParameter("@Tarikh_DO", TkhDO))
        param.Add(New SqlParameter("@Jenis_Bayar", JenisBayar))
        param.Add(New SqlParameter("@Jumlah_Sebenar", Jumlah))
        param.Add(New SqlParameter("@Jenis_Invois", JenisInvois))
        param.Add(New SqlParameter("@Tujuan", Tujuan))

        Return db.Process(query, param)
    End Function

    Private Function InsertNewPenerima(orderId As String, Bil As String, KodPemiutang As String, Jumlah As String)
        Dim db As New DBKewConn
        Dim query As String = "INSERT INTO SMKB_Pembayaran_Invois_Penerima (ID_Rujukan, Bil_Penerima, Kod_Pemiutang, Amaun_Sebenar, Status)
        VALUES(@ID_Invois, @Bil_Penerima, @Kod_Pemiutang, @Jumlah_Sebenar,@Status)"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@ID_Invois", orderId))
        param.Add(New SqlParameter("@Bil_Penerima", Bil))
        param.Add(New SqlParameter("@Kod_Pemiutang", KodPemiutang))
        param.Add(New SqlParameter("@Jumlah_Sebenar", Jumlah))
        param.Add(New SqlParameter("@Status", "1"))

        Return db.Process(query, param)
    End Function

    Private Function UpdateNewPenerima(orderId As String, Bil As String, KodPemiutang As String, Jumlah As String)
        Dim db As New DBKewConn
        Dim query As String = "UPDATE SMKB_Pembayaran_Invois_Penerima SET Kod_Pemiutang = @Kod_Pemiutang, Amaun_Sebenar = @Amaun_Sebenar
        WHERE ID_Rujukan = @ID_Rujukan AND Bil_Penerima = @Bil_Penerima AND Status = '1'"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@ID_Rujukan", orderId))
        param.Add(New SqlParameter("@Bil_Penerima", Bil))
        param.Add(New SqlParameter("@Kod_Pemiutang", KodPemiutang))
        param.Add(New SqlParameter("@Amaun_Sebenar", Jumlah))

        Return db.Process(query, param)
    End Function



    'orer detail sini <>
    Public Function InsertOrderDetail(orderDetail As POrderDetail)
        Dim db = New DBKewConn
        Dim query As String = "INSERT INTO SMKB_Pembayaran_Invois_Dtl (ID_Rujukan, No_Item, Kod_Kump_Wang, Kod_Operasi, Kod_PTJ, Kod_Projek, Kod_Vot, Butiran, Kuantiti_Sebenar, Kadar_Harga, Amaun_Sebenar, Cukai, Diskaun, Status)
        VALUES( @ID_Rujukan, @No_Item, @Kod_Kump_Wang, @Kod_Operasi, @Kod_PTJ, @Kod_Projek, @Kod_Vot, @Butiran, @Kuantiti_Sebenar, @Kadar_Harga, @Amaun_Sebenar, @Cukai, @Diskaun, '1')"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@ID_Rujukan", orderDetail.OrderID))
        param.Add(New SqlParameter("@No_Item", orderDetail.id))
        param.Add(New SqlParameter("@Kod_Kump_Wang", orderDetail.ddlKw))
        param.Add(New SqlParameter("@Kod_Operasi", orderDetail.ddlKo))
        param.Add(New SqlParameter("@Kod_PTJ", orderDetail.ddlPTJ))
        param.Add(New SqlParameter("@Kod_Projek", orderDetail.ddlKp))
        param.Add(New SqlParameter("@Kod_Vot", orderDetail.ddlVot))
        param.Add(New SqlParameter("@Butiran", orderDetail.details))
        param.Add(New SqlParameter("@Kuantiti_Sebenar", orderDetail.quantity))
        param.Add(New SqlParameter("@Kadar_Harga", orderDetail.price))
        param.Add(New SqlParameter("@Amaun_Sebenar", orderDetail.amount))
        param.Add(New SqlParameter("@Cukai", orderDetail.cukai))
        param.Add(New SqlParameter("@Diskaun", orderDetail.diskaun))

        Return db.Process(query, param)
    End Function

    Public Function UpdateOrderDetail(orderDetail As POrderDetail)
        Dim db = New DBKewConn
        Dim data
        data = Split(orderDetail.id, "|")
        Dim idrujukan = data(0)
        Dim noitem = data(1)
        Dim query As String = "UPDATE SMKB_Pembayaran_Invois_Dtl
                                SET Kod_Kump_Wang=@Kod_Kump_Wang,Kod_Operasi=@Kod_Operasi,Kod_PTJ=@Kod_PTJ,Kod_Projek=@Kod_Projek,Kod_Vot=@Kod_Vot,
                                Butiran=@Butiran,Kuantiti_Sebenar=@Kuantiti_Sebenar,Kadar_Harga=@Kadar_Harga,Amaun_Sebenar=@Amaun_Sebenar,Diskaun=@Diskaun,Cukai=@Cukai
                                WHERE ID_Rujukan=@ID_Rujukan AND No_Item=@no_item AND Status = '1'"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@ID_Rujukan", idrujukan))
        param.Add(New SqlParameter("@no_item", noitem))
        param.Add(New SqlParameter("@Kod_Kump_Wang", orderDetail.ddlKw))
        param.Add(New SqlParameter("@Kod_Operasi", orderDetail.ddlKo))
        param.Add(New SqlParameter("@Kod_PTJ", orderDetail.ddlPTJ))
        param.Add(New SqlParameter("@Kod_Projek", orderDetail.ddlKp))
        param.Add(New SqlParameter("@Kod_Vot", orderDetail.ddlVot))
        param.Add(New SqlParameter("@Butiran", orderDetail.details))
        param.Add(New SqlParameter("@Kuantiti_Sebenar", orderDetail.quantity))
        param.Add(New SqlParameter("@Kadar_Harga", orderDetail.price))
        param.Add(New SqlParameter("@Amaun_Sebenar", orderDetail.amount))
        param.Add(New SqlParameter("@Diskaun", orderDetail.diskaun))
        param.Add(New SqlParameter("@Cukai", orderDetail.cukai))

        Return db.Process(query, param)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SubmitOrders(order As POrder) As String
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
            If InsertNewOrder(order.OrderID, order.JenisInvois, order.JenisBayar, order.NoInvois, order.TkhInvois, order.TkhTerimaInvois, order.NoDO, order.TkhDO, order.TkhTerimaDO, order.Tujuan, order.Jumlah) <> "OK" Then
                resp.Failed("Gagal Menyimpan order")
                Exit Function
            End If
            order.Bil = GenerateOrderBil(order.OrderID)
            If InsertNewPenerima(order.OrderID, order.Bil, order.BayarKepada, order.Jumlah) <> "OK" Then
                resp.Failed("Gagal Menyimpan order")
                Exit Function
            End If
        Else
            If UpdateSubmitOrder(order.OrderID, order.JenisInvois, order.JenisBayar, order.NoInvois, order.TkhInvois, order.TkhTerimaInvois, order.NoDO, order.TkhDO, order.TkhTerimaDO, order.Tujuan, order.Jumlah) <> "OK" Then
                resp.Failed("Gagal Menyimpan order")
                Exit Function
            End If
            If UpdateNewPenerima(order.OrderID, order.Bil, order.BayarKepada, order.Jumlah) <> "OK" Then
                resp.Failed("Gagal Menyimpan order")
                Exit Function
            End If
        End If

        For Each orderDetail As POrderDetail In order.OrderDetails

            If orderDetail.ddlPTJ = "" Then
                Continue For
            End If

            If orderDetail.cukai = "" Then
                orderDetail.cukai = 0.00
            End If

            If orderDetail.diskaun = "" Then
                orderDetail.diskaun = 0.00
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
            'resp.Success("Rekod berjaya disimpan", "00", order)
        End If

        Return JsonConvert.SerializeObject(resp.GetResult())

    End Function

    Private Function UpdateSubmitOrder(OrderID As String, JenisInvois As String, JenisBayar As String, NoInvois As String, TkhInvois As String, TkhTerimaInvois As String, NoDO As String, TkhDO As String, TkhTerimaDO As String, Tujuan As String, Jumlah As String)
        Dim db As New DBKewConn
        Dim query As String = "UPDATE SMKB_Pembayaran_Invois_Hdr SET No_Invois = @No_Invois , Tarikh_Invois =@Tarikh_Invois , Tarikh_Terima_Invois =@Tarikh_Terima_Invois , No_DO =@No_DO,
                               Tarikh_DO = @Tarikh_DO, Jenis_Bayar = @Jenis_Bayar, Jumlah_Sebenar = @Jumlah_Sebenar, Jenis_Invois = @Jenis_Invois, Tujuan = @Tujuan, Status_Dok = @Status_Dok 
                               WHERE ID_Rujukan=@ID_Rujukan AND Status = 1"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@ID_Rujukan", OrderID))
        param.Add(New SqlParameter("@No_Invois", NoInvois))
        param.Add(New SqlParameter("@Tarikh_Invois", TkhInvois))
        param.Add(New SqlParameter("@Tarikh_Terima_Invois", TkhTerimaInvois))
        param.Add(New SqlParameter("@No_DO", NoDO))
        param.Add(New SqlParameter("@Tarikh_DO", TkhDO))
        param.Add(New SqlParameter("@Jenis_Bayar", JenisBayar))
        param.Add(New SqlParameter("@Jumlah_Sebenar", Jumlah))
        param.Add(New SqlParameter("@Jenis_Invois", JenisInvois))
        param.Add(New SqlParameter("@Tujuan", Tujuan))
        param.Add(New SqlParameter("@Status_Dok", "02"))

        Return db.Process(query, param)
    End Function

    Public Function GenerateOrderBil(orderid As String) As String
        Dim db = New DBKewConn
        Dim lastBil As Integer = 1
        Dim newOrderBil As String = ""
        Dim param As New List(Of SqlParameter)

        Dim query As String = "SELECT TOP 1 Bil_Penerima as id 
                                FROM SMKB_Pembayaran_Invois_Penerima 
                                WHERE ID_Rujukan= @orderid
                                ORDER BY Bil_Penerima DESC"

        param.Add(New SqlParameter("@orderid", orderid))

        dt = db.Read(query, param)
        If dt IsNot Nothing Then
            If dt.Rows.Count > 0 Then
                lastBil = CInt(dt.Rows(0).Item("id")) + 1
            End If
        End If

        newOrderBil = lastBil
        Return newOrderBil
    End Function

    Public Function GenerateGetBil(orderid As String) As String
        Dim db = New DBKewConn
        Dim lastBil As Integer = 1
        Dim newOrderBil As String = ""
        Dim param As New List(Of SqlParameter)

        Dim query As String = "SELECT Bil_Penerima as id, Kod_Pemiutang 
                                FROM SMKB_Pembayaran_Invois_Penerima 
                                WHERE ID_Rujukan= @orderid
                                AND Kod_Pemiutang = @Kod_Pemiutang"

        param.Add(New SqlParameter("@orderid", orderid))

        dt = db.Read(query, param)
        If dt IsNot Nothing Then
            If dt.Rows.Count > 0 Then
                lastBil = CInt(dt.Rows(0).Item("id")) + 1
            End If
        End If

        newOrderBil = lastBil
        Return newOrderBil
    End Function

    Public Function GenerateOrderDetailID(orderid As String) As String
        Dim db = New DBKewConn
        Dim lastID As Integer = 1
        Dim newOrderID As String = ""
        Dim param As New List(Of SqlParameter)

        Dim query As String = "SELECT TOP 1 No_Item as id 
                                FROM SMKB_Pembayaran_Invois_Dtl 
                                WHERE ID_Rujukan= @orderid
                                ORDER BY No_Item DESC"

        param.Add(New SqlParameter("@orderid", orderid))

        dt = db.Read(query, param)
        If dt IsNot Nothing Then
            If dt.Rows.Count > 0 Then
                lastID = CInt(dt.Rows(0).Item("id")) + 1
            End If
        End If

        newOrderID = lastID
        Return newOrderID
    End Function

    Private Function GenerateOrderID() As String
        Dim db As New DBKewConn

        Dim year = Date.Now.ToString("yyyy")
        Dim month = Date.Now.Month

        Dim lastID As Integer = 1
        Dim newOrderID As String = ""

        Dim query As String = $"select TOP 1 No_Akhir as id from SMKB_No_Akhir where Kod_Modul ='03' AND Prefix ='PP' AND Tahun =@year"
        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@year", year))

        dt = db.Read(query, param)

        If dt.Rows.Count > 0 Then
            lastID = CInt(dt.Rows(0).Item("id")) + 1

            UpdateNoAkhir("03", "PP", year, lastID)
        Else

            InsertNoAkhir("03", "PP", year, lastID)
        End If
        newOrderID = "PP" + Format(lastID, "000000").ToString + month.ToString("00") + Right(year.ToString(), 2)

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

    Private Function DeleteOrderDetails(Optional kod As String = "", Optional order_id As String = "")
        Dim db = New db
        Dim query As String = "DELETE FROM orderDetails WHERE "
        Dim param As New List(Of SqlParameter)

        If kod <> "" Then
            query &= " id = @id "
            param.Add(New SqlParameter("@id", kod))
        Else
            query &= " order_id = @order_id "

            param.Add(New SqlParameter("@order_id", order_id))
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

        Dim query As String = "SELECT a.ID_Rujukan, a.No_Invois, a.No_DO, a.Jenis_Bayar, b.Kod + ' - ' + b.Butiran as ButirByr, a.Jenis_Invois, 
                                c.Kod + ' - ' + c.Butiran as JenisInv, a.Tujuan, 
                                (SELECT Bil_Penerima FROM SMKB_Pembayaran_Invois_Penerima
                                WHERE ID_Rujukan = @No_Invois) AS BilPemiutang,
                                (SELECT f.Kod_Pemiutang FROM SMKB_Pembayaran_Invois_Penerima e, SMKB_Pemiutang_Master f 
                                WHERE ID_Rujukan = @No_Invois AND e.Kod_Pemiutang = f.Kod_Pemiutang) AS KodPemiutang,
                                (SELECT f.Kategori_Pemiutang + ' - ' + f.Nama_Pemiutang FROM SMKB_Pembayaran_Invois_Penerima e, SMKB_Pemiutang_Master f 
                                WHERE ID_Rujukan = @No_Invois AND e.Kod_Pemiutang = f.Kod_Pemiutang) AS NamaPemiutang,
                                CASE WHEN a.Tarikh_Invois <> '' THEN FORMAT(a.Tarikh_Invois, 'yyyy-MM-dd') END AS Tarikh_Invois,
                                CASE WHEN a.Tarikh_Terima_Invois <> '' THEN FORMAT(a.Tarikh_Terima_Invois, 'yyyy-MM-dd') END AS Tarikh_Terima_Invois,
                                CASE WHEN a.Tarikh_DO <> '' THEN FORMAT(a.Tarikh_DO, 'yyyy-MM-dd') END AS Tarikh_DO,
                                CASE WHEN a.Tarikh_Terima_DO <> '' THEN FORMAT(a.Tarikh_Terima_DO, 'yyyy-MM-dd') END AS Tarikh_Terima_DO
                                FROM SMKB_Pembayaran_Invois_Hdr a, SMKB_Pembayaran_Jenis_Bayar b, SMKB_Pembayaran_Jenis_Invois c 
                                WHERE a.ID_Rujukan = @No_Invois AND a.Jenis_Bayar = b.Kod AND a.Jenis_Invois = c.Kod"

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

        Dim query As String = "select a.ID_Rujukan+'|'+a.No_Item as dataid, Kod_Kump_Wang as colhidkw ,(select Butiran from SMKB_Kump_Wang as kw where a.Kod_Kump_Wang = kw.Kod_Kump_Wang) as colKW,
        Kod_Operasi  as colhidko, (select Butiran from SMKB_Operasi as ko where a.Kod_Operasi = ko.Kod_Operasi) as colKO,
        Kod_Projek as colhidkp,  (select Butiran from SMKB_Projek as kp where a.Kod_Projek = kp.Kod_Projek) as colKp,
        Kod_PTJ as colhidptj , (SELECT b.Pejabat FROM [DEVMIS\SQL_INS01].dbStaf.dbo.MS_PEJABAT as b
        WHERE b.STATUS = 1 and b.kodpejabat = left(Kod_PTJ,2)) as ButiranPTJ ,
        Kod_Vot , (select Kod_Vot + ' - ' + Butiran from SMKB_Vot as vot where a.Kod_Vot = vot.Kod_Vot) as ButiranVot,
        Butiran ,Kuantiti_Sebenar, Kadar_Harga, Amaun_Sebenar, Diskaun, Cukai
        from SMKB_Pembayaran_Invois_Dtl as a
        where ID_Rujukan = @No_Invois
        and status = 1
        order by No_Item"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@No_Invois", kod))

        Return db.Read(query, param)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadOrderRecord_SenaraiTransaksiInvois() As String
        Dim resp As New ResponseRepository

        dt = GetOrder_SenaraiTransaksiInvois()

        Return JsonConvert.SerializeObject(dt)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetOrder_SenaraiTransaksiInvois() As DataTable
        Dim db = New DBKewConn

        Dim query As String = "SELECT DISTINCT a.ID_Rujukan, a.No_Invois, a.No_DO, a.Jenis_Bayar, b.Kod + ' - ' + b.Butiran as ButirByr, a.Jenis_Invois, a.Jumlah_Sebenar,
                                c.Kod + ' - ' + c.Butiran as JenisInv, a.Tujuan, f.Kod_Pemiutang, f.Kategori_Pemiutang + ' - ' + f.Nama_Pemiutang AS NamaPemiutang,
                                CASE WHEN a.Tarikh_Invois <> '' THEN FORMAT(a.Tarikh_Invois, 'dd-MM-yyyy') END AS Tarikh_Invois,
                                CASE WHEN a.Tarikh_Terima_Invois <> '' THEN FORMAT(a.Tarikh_Terima_Invois, 'dd-MM-yyyy') END AS Tarikh_Terima_Invois,
                                CASE WHEN a.Tarikh_DO <> '' THEN FORMAT(a.Tarikh_DO, 'dd-MM-yyyy') END AS Tarikh_DO,
                                CASE WHEN a.Tarikh_Terima_DO <> '' THEN FORMAT(a.Tarikh_Terima_DO, 'dd-MM-yyyy') END AS Tarikh_Terima_DO
                                FROM SMKB_Pembayaran_Invois_Hdr a, SMKB_Pembayaran_Jenis_Bayar b, SMKB_Pembayaran_Jenis_Invois c, SMKB_Pembayaran_Invois_Penerima e, SMKB_Pemiutang_Master f  
                                WHERE a.Status_Dok = '01' AND a.Jenis_Bayar = b.Kod AND a.Jenis_Invois = c.Kod AND a.ID_Rujukan = e.ID_Rujukan AND e.Kod_Pemiutang = f.Kod_Pemiutang"

        Return db.Read(query)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetPemiutangList(ByVal q As String) As String
        Dim tmpDT As DataTable = GetPenerimaList(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetPenerimaList(kod As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT TOP 10 Kod_Pemiutang as value, Kategori_Pemiutang + ' - ' + Nama_Pemiutang as text 
                                FROM SMKB_Pemiutang_Master WHERE Status='1'"
        Dim param As New List(Of SqlParameter)
        query &= " AND Kod_Pemiutang LIKE '%' + @kod + '%' OR Nama_Pemiutang LIKE '%' + @kod2 + '%' "
        param.Add(New SqlParameter("@kod", kod))
        param.Add(New SqlParameter("@kod2", kod))

        Return db.Read(query, param)
    End Function

End Class