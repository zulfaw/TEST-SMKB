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
Public Class Transaksi_TerimaWS
    Inherits System.Web.Services.WebService

    'Dim sqlcmd As SqlCommand
    'Dim sqlcon As SqlConnection
    'Dim sqlread As SqlDataReader
    Dim dt As DataTable

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetVotCOASah(ByVal q As String) As String
        Dim tmpDT As DataTable = GetKodCOAListSah(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetKodCOAListSah(kodCariVot As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT TOP 10 CONCAT(a.Kod_Vot, ' - ', vot.Butiran, ', ', a.Kod_Operasi, ' - ', ko.Butiran, ', ', kp.Butiran, ', ', a.Kod_Kump_Wang, ' - ', REPLACE(kw.Butiran, 'KUMPULAN WANG', 'KW'), ', ', mj.Pejabat) AS text,
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
    Public Function GetPTJListSah(ByVal q As String) As String
        'Dim newList As List(Of ItemList)
        'newList = listItem

        'If (q <> "") Then
        '    newList = listItem.Where(
        '        Function(x) x.value.Contains(q) Or x.text.Contains(q)
        '    ).ToList()
        'End If

        Dim tmpDT As DataTable = GetKodPTJListSah(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetKodPTJListSah(kod As String) As DataTable
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
    Public Function GetVotListSah(ByVal q As String, ByVal kodptj As String) As String
        Dim tmpDT As DataTable = GetKodVotListSah(q, kodptj)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetKodVotListSah(kod As String, kodptj As String) As DataTable
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
    Public Function GetKWListSah(ByVal q As String, ByVal kodvot As String) As String
        Dim tmpDT As DataTable = GetKodKWListSah(q, kodvot)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetKodKWListSah(kod As String, kodvot As String) As DataTable
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
    Public Function GetProjekListSah(ByVal q As String, ByVal kodptj As String, ByVal kodvot As String) As String
        'Dim newList As List(Of ItemList)
        'newList = listItem

        'If (q <> "") Then
        '    newList = listItem.Where(
        '        Function(x) x.value.Contains(q) Or x.text.Contains(q)
        '    ).ToList()
        'End If

        Dim tmpDT As DataTable = GetKodProjekListSah(q, kodptj, kodvot)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetKodProjekListSah(kod As String, kodptj As String, kodvot As String) As DataTable
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
    Public Function GetJenInvListSah(ByVal q As String) As String
        Dim tmpDT As DataTable = GetKodJenInvListSah(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetKodJenInvListSah(kod As String) As DataTable
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
    Public Function GetJenByrListSah(ByVal q As String) As String
        Dim tmpDT As DataTable = GetKodJenByrListSah(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetKodJenByrListSah(kod As String) As DataTable
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
    Public Function GetUrusniagaListSah(ByVal q As String) As String
        'Dim newList As List(Of ItemList)
        'newList = listItem

        'If (q <> "") Then
        '    newList = listItem.Where(
        '        Function(x) x.value.Contains(q) Or x.text.Contains(q)
        '    ).ToList()
        'End If

        Dim tmpDT As DataTable = GetKodUrusniagaListSah(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetKodUrusniagaListSah(kod As String) As DataTable
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
    Public Function GetKOListSah(ByVal q As String, ByVal kodkw As String) As String
        'Dim newList As List(Of ItemList)
        'newList = listItem

        'If (q <> "") Then
        '    newList = listItem.Where(
        '        Function(x) x.value.Contains(q) Or x.text.Contains(q)
        '    ).ToList()
        'End If

        Dim tmpDT As DataTable = GetKodKOListSah(q, kodkw)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetKodKOListSah(kod As String, kodkw As String) As DataTable
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
    Public Function GetCarianVotListSah(ByVal q As String) As String
        'Dim newList As List(Of ItemList)
        'newList = listItem

        'If (q <> "") Then
        '    newList = listItem.Where(
        '        Function(x) x.value.Contains(q) Or x.text.Contains(q)
        '    ).ToList()
        'End If

        Dim tmpDT As DataTable = GetCarianKodVotListSah(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetCarianKodVotListSah(kodCariVot As String) As DataTable
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
    Public Function DeleteOrderSah(ByVal id As String) As String
        Dim resp As New ResponseRepository

        DeleteOrderDetailsSah("", id)


        If DeleteOrderRecordSah(id) <> "OK" Then
            resp.Failed("Gagal")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        resp.Success("Order telah dihapuskan")
        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function DeleteRecordSah(ByVal id As String) As String
        Dim resp As New ResponseRepository
        If DeleteOrderDetailsSah(id) = "OK" Then
            resp.Success("Berjaya")
        Else
            resp.Failed("Gagal")
        End If


        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadOrderRecordSah(ByVal id As String) As String
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

        dt = GetOrderSah(id)
        resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SaveOrdersSah(order As POrder) As String
        Dim resp As New ResponseRepository
        resp.Success("Data telah disimpan")
        Dim success As Integer = 0
        Dim JumRekod As Integer = 0
        If order Is Nothing Then
            resp.Failed("Tidak disimpan")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        If order.OrderID = "" Then
            order.OrderID = GenerateOrderIDSah()
            If InsertNewOrderSah(order.OrderID, order.JenisInvois, order.JenisBayar, order.NoInvois, order.TkhInvois, order.TkhTerimaInvois, order.NoDO, order.TkhDO, order.TkhTerimaDO, order.Tujuan, order.Jumlah) <> "OK" Then
                resp.Failed("Gagal Menyimpan order")
                Exit Function
            End If
        Else
            dt = GetOrderSah(order.OrderID)
            If dt.Rows.Count = 0 Then
                resp.Failed("Nombor order tidak ditemui")
                Return False
            End If
        End If

        If order.OrderID <> "" Then
            order.Bil = GenerateOrderBilSah(order.OrderID)
            If InsertNewPenerimaSah(order.orderId, order.Bil, order.BayarKepada, order.Jumlah) <> "OK" Then
                resp.Failed("Gagal Menyimpan order")
                Exit Function
            End If
        End If

        For Each orderDetail As POrderDetail In order.OrderDetails

            If orderDetail.ddlPTJ = "" Then
                Continue For
            End If

            JumRekod += 1

            orderDetail.amount = orderDetail.quantity * orderDetail.price 'This can be automated insie orderdetail model

            If orderDetail.id = "" Then
                orderDetail.id = GenerateOrderDetailIDSah(order.OrderID)
                orderDetail.OrderID = order.OrderID
                If InsertOrderDetailSah(orderDetail) = "OK" Then
                    success += 1
                End If
            Else
                If UpdateOrderDetailSah(orderDetail) = "OK" Then
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

    Private Function InsertNewOrderSah(OrderID As String, JenisInvois As String, JenisBayar As String, NoInvois As String, TkhInvois As String, TkhTerimaInvois As String, NoDO As String, TkhDO As String, TkhTerimaDO As String, Tujuan As String, Jumlah As String)
        Dim db As New DBKewConn
        Dim query As String = "INSERT INTO SMKB_Pembayaran_Invois_Hdr (ID_Rujukan,No_Invois,Tarikh_Invois,Tarikh_Terima_Invois, No_DO,Tarikh_DO,Tarikh_Terima_DO,Jenis_Bayar,Jumlah_Sebenar,Jenis_Invois,Tujuan, tarikh_Daftar, Status_Dok)
        VALUES(@ID_Invois, @No_Invois, @Tarikh_Invois, @Tarikh_Terima_Invois, @No_DO, @Tarikh_DO, @Tarikh_Terima_DO, @Jenis_Bayar, @Jumlah_Sebenar, @Jenis_Invois, @Tujuan, getdate(), @Status_Dok)"
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
        param.Add(New SqlParameter("@Status_Dok", "01"))

        Return db.Process(query, param)
    End Function

    Private Function InsertNewPenerimaSah(orderId As String, Bil As String, KodPemiutang As String, Jumlah As String)
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

    Public Function InsertOrderDetailSah(orderDetail As POrderDetail)
        Dim db = New DBKewConn
        Dim query As String = "INSERT INTO SMKB_Pembayaran_Invois_Dtl (ID_Rujukan, No_Item, Kod_Kump_Wang, Kod_Operasi, Kod_PTJ, Kod_Projek, Kod_Vot, Butiran, Kuantiti_Sebenar, Kadar_Harga, Amaun_Sebenar, Status)
        VALUES( @ID_Rujukan, @No_Item, @Kod_Kump_Wang, @Kod_Operasi, @Kod_PTJ, @Kod_Projek, @Kod_Vot, @Butiran, @Kuantiti_Sebenar, @Kadar_Harga, @Amaun_Sebenar, '1')"
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
        'param.Add(New SqlParameter("@Tahun", orderDetail.amount))
        'param.Add(New SqlParameter("@Status_Bayaran", orderDetail.amount))

        Return db.Process(query, param)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SubmitOrdersSah(order As POrder) As String
        Dim resp As New ResponseRepository
        resp.Success("Data telah disimpan")
        Dim success As Integer = 0
        Dim JumRekod As Integer = 0
        If order Is Nothing Then
            resp.Failed("Tidak disimpan")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        'start call function submit
        UpdateStatusSubmitSah(order.OrderID)

        'Try
        '    Dim myGetTicket As New TokenResponseModel()

        '    Dim servicex As New ValuesService()
        '    Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("ms-MY")
        '    Dim parsedDate As Date = CDate(Now).ToString("yyyy-MM-dd")
        '    Dim vBulan As String = parsedDate.Month
        '    Dim vTahun As String = parsedDate.Year
        '    Dim values As String = Await servicex.SendDataLejar(myGetTicket.GetTicket("smkb", Session("ssusrID")),
        '                            "AP", "", "07", "430000", "81101", "01", "0000000", "2500", "CR", vBulan, vTahun)

        'If values.Contains("ok") Then
        '        lblModalMessaage.Text = "Rekod telah disimpan" 'message di modal
        '        ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)

        '    Else
        '        lblModalMessaage.Text = "Rekod Gagal disimpan" 'message di modal
        '        ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
        '    End If
        'Catch
        '    lblModalMessaage.Text = "Invalid Token" 'message di modal
        '    ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
        'End Try

        'InsertLejarPemiutang(order.OrderID)
        'InsertLejarAm(order.OrderID)
        'end call function submit

        'resp.Success("Rekod berjaya dihantar", "00", order)

        ''start emel
        'Dim message As String = String.Empty
        'Dim strNamaPenyokong As String
        'Dim strEmel As String

        'strNamaPenyokong = "Syafiqah"
        'strEmel = "syafiqah@utem.edu.my"



        'Dim Subject = "Pengesahan Transaksi Jurnal"
        'Dim body As String = "Assalamualaikum Wrh. Wbt. dan salam sejahtera,"
        'body += "<br /><br />Prof. / Prof. Madya/ Dr./ Tuan / Puan"
        'body += "<br /><br />Mohon log masuk sistem SMKB untuk mengesahkan transaksi jurnal."
        'body += "<br /><br />Terima kasih"
        'Dim sent = clsSharedMails.sendEmail(strEmel, Subject, body)
        ''close emel
        'End If

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    Private Function UpdateNewOrderSah(OrderID As String, JenisInvois As String, JenisBayar As String, NoInvois As String, TkhInvois As String, TkhTerimaInvois As String, NoDO As String, TkhDO As String, TkhTerimaDO As String, Tujuan As String, Jumlah As String)
        Dim db As New DBKewConn
        Dim query As String = "UPDATE SMKB_Pembayaran_Invois_Hdr SET No_Invois = @No_Invois , Tarikh_Invois =@Tarikh_Invois , Tarikh_Terima_Invois =@Tarikh_Terima_Invois , No_DO =@No_DO,
                               Tarikh_DO = @Tarikh_DO, Jenis_Bayar = @Jenis_Bayar, Jumlah_Sebenar = @Jumlah_Sebenar, Jenis_Invois = @Jenis_Invois, Tujuan = @Tujuan
                               WHERE ID_Rujukan=@No_Jurnal AND Status = 1"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@No_Jurnal", OrderID))
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

    Private Function DeleteOrderHdrSah(orderid As String)
        Dim db As New DBKewConn
        Dim query As String = "DELETE FROM SMKB_Pembayaran_Invois_Hdr WHERE ID_Rujukan=@No_Jurnal "
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@No_Jurnal", orderid))

        Return db.Process(query, param)
    End Function

    Private Function DeleteOrderDtlSah(orderid As String)
        Dim db As New DBKewConn
        Dim query As String = "DELETE FROM SMKB_Pembayaran_Invois_Dtl WHERE ID_Rujukan=@No_Jurnal AND Status = 1"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@No_Jurnal", orderid))

        Return db.Process(query, param)
    End Function

    Private Function UpdateStatusSubmitSah(kod As String)
        Dim db As New DBKewConn

        Dim statusdok As String = "07" 'hantar
        Dim query As String = "Update SMKB_Pembayaran_Invois_Hdr set Status_Dok =  @statusdok where ID_Rujukan = @No_Jurnal and Status = 1"

        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@No_Jurnal", kod))
        param.Add(New SqlParameter("@statusdok", statusdok))


        Return db.Process(query, param)
    End Function

    Private Function InsertLejarPemiutang(kod As String)
        Dim db As New DBKewConn

        Dim query As String = "INSERT INTO SMKB_Lejar_Pemiutang (Kod_Pemiutang, Tahun, Kod_Kump_Wang, Kod_Operasi, Kod_Projek, Kod_PTJ, Kod_Vot, Cr_6)
                                VALUES (@Kod_Pemiutang, @Tahun, @Kod_Kump_Wang, @Kod_Operasi, @Kod_Projek, @Kod_PTJ, @Kod_Vot, @Cr_6)"

        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@Kod_Pemiutang", kod))
        param.Add(New SqlParameter("@Tahun", "2023"))
        param.Add(New SqlParameter("@Kod_Kump_Wang", kod))
        param.Add(New SqlParameter("@Kod_Operasi", kod))
        param.Add(New SqlParameter("@Kod_Projek", kod))
        param.Add(New SqlParameter("@Kod_PTJ", kod))
        param.Add(New SqlParameter("@Kod_Vot", kod))
        param.Add(New SqlParameter("@Cr_5", kod))

        Return db.Process(query, param)
    End Function

    Private Function InsertLejarAm(kod As String)
        Dim db As New DBKewConn

        Dim query As String = "INSERT INTO SMKB_Lejar_Am (Tahun, Kod_Kump_Wang, Kod_Operasi, Kod_Projek, Kod_PTJ, Kod_Vot, Cr_5)
                                VALUES (@Tahun, @Kod_Kump_Wang, @Kod_Operasi, @Kod_Projek, @Kod_PTJ, @Kod_Vot, @Cr_5)"

        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@Tahun", kod))
        param.Add(New SqlParameter("@Kod_Kump_Wang", kod))
        param.Add(New SqlParameter("@Kod_Operasi", kod))
        param.Add(New SqlParameter("@Kod_Projek", kod))
        param.Add(New SqlParameter("@Kod_PTJ", kod))
        param.Add(New SqlParameter("@Kod_Vot", kod))
        param.Add(New SqlParameter("@Cr_5", kod))

        Return db.Process(query, param)
    End Function

    Public Function UpdateOrderDetailSah(orderDetail As POrderDetail)
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

    Public Function GenerateOrderBilSah(orderid As String) As String
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

    Public Function GenerateOrderDetailIDSah(orderid As String) As String
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

    Private Function GenerateOrderIDSah() As String
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

            UpdateNoAkhirSah("03", "PP", year, lastID)
        Else

            InsertNoAkhirSah("03", "PP", year, lastID)
        End If
        newOrderID = "PP" + Format(lastID, "000000").ToString + month.ToString("00") + Right(year.ToString(), 2)

        Return newOrderID
    End Function

    Private Function UpdateNoAkhirSah(kodModul As String, prefix As String, year As String, ID As String)
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

    Private Function InsertNoAkhirSah(kodModul As String, prefix As String, year As String, ID As String)
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

    Private Function DeleteOrderRecordSah(orderid As String)
        Dim db = New db
        Dim query As String = "DELETE FROM orders WHERE order_id = @orderid "
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@orderid", orderid))

        Return db.Process(query, param)
    End Function

    Private Function DeleteOrderDetailsSah(Optional kod As String = "", Optional order_id As String = "")
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

    Private Function GetOrderSah(kod As String) As DataTable
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
    Public Function LoadHdrInvoisSah(ByVal id As String) As String
        Dim resp As New ResponseRepository

        dt = GetHdrInvoisSah(id)
        resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    Private Function GetHdrInvoisSah(id As String) As DataTable
        Dim db = New DBKewConn

        Dim query As String = "SELECT a.ID_Rujukan, a.No_Invois, a.No_DO, a.Jenis_Bayar, b.Kod + ' - ' + b.Butiran as ButirByr, a.Jenis_Invois, 
                                c.Kod + ' - ' + c.Butiran as JenisInv, a.Tujuan, 
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

        'SELECT A.No_Bil,A.Kod_Pelanggan,B.Nama_Penghutang,A.Jenis_Urusniaga,C.Butiran, A.Kontrak,A.Tujuan,
        '                                CASE WHEN Tkh_Mula <> '' THEN FORMAT(Tkh_Mula, 'dd/MM/yyyy') END AS Tkh_Mula,
        '                                CASE WHEN Tkh_Tamat <> '' THEN FORMAT(Tkh_Tamat, 'dd/MM/yyyy') END AS Tkh_Tamat
        '                                FROM SMKB_Bil_Hdr A
        '                                INNER JOIN SMKB_Penghutang_Master B ON A.Kod_Pelanggan=B.Kod_Penghutang
        '                                INNER JOIN SMKB_Kod_Urusniaga C ON A.Jenis_Urusniaga=C.Kod_Urusniaga
        '                                WHERE No_Bil = @No_Invois AND A.Status='1'"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@No_Invois", id))

        Return db.Read(query, param)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadRecordInvoisSah(ByVal id As String) As String
        Dim resp As New ResponseRepository

        dt = GetTransaksiInvoisSah(id)
        resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    Private Function GetTransaksiInvoisSah(kod As String) As DataTable
        Dim db = New DBKewConn

        Dim query As String = "select Kod_Kump_Wang as colhidkw ,(select Butiran from SMKB_Kump_Wang as kw where a.Kod_Kump_Wang = kw.Kod_Kump_Wang) as colKW,
        Kod_Operasi  as colhidko, (select Butiran from SMKB_Operasi as ko where a.Kod_Operasi = ko.Kod_Operasi) as colKO,
        Kod_Projek as colhidkp,  (select Butiran from SMKB_Projek as kp where a.Kod_Projek = kp.Kod_Projek) as colKp,
        Kod_PTJ as colhidptj , (SELECT b.Pejabat FROM [DEVMIS\SQL_INS01].dbStaf.dbo.MS_PEJABAT as b
        WHERE b.STATUS = 1 and b.kodpejabat = left(Kod_PTJ,2)) as ButiranPTJ ,
        Kod_Vot , (select Butiran from SMKB_Vot as vot where a.Kod_Vot = vot.Kod_Vot) as ButiranVot,
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
    Public Function LoadOrderRecord_SenaraiTransaksiInvois_Sah() As String
        Dim resp As New ResponseRepository

        dt = GetOrder_SenaraiTransaksiInvois_Sah()

        Return JsonConvert.SerializeObject(dt)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetOrder_SenaraiTransaksiInvois_Sah() As DataTable
        Dim db = New DBKewConn

        Dim query As String = "SELECT DISTINCT a.ID_Rujukan, a.No_Invois, a.No_DO, a.Jenis_Bayar, b.Kod + ' - ' + b.Butiran as ButirByr, a.Jenis_Invois, a.Jumlah_Sebenar,
                                c.Kod + ' - ' + c.Butiran as JenisInv, a.Tujuan, f.Kod_Pemiutang, f.Kategori_Pemiutang + ' - ' + f.Nama_Pemiutang AS NamaPemiutang,
                                CASE WHEN a.Tarikh_Invois <> '' THEN FORMAT(a.Tarikh_Invois, 'dd-MM-yyyy') END AS Tarikh_Invois,
                                CASE WHEN a.Tarikh_Terima_Invois <> '' THEN FORMAT(a.Tarikh_Terima_Invois, 'dd-MM-yyyy') END AS Tarikh_Terima_Invois,
                                CASE WHEN a.Tarikh_DO <> '' THEN FORMAT(a.Tarikh_DO, 'dd-MM-yyyy') END AS Tarikh_DO,
                                CASE WHEN a.Tarikh_Terima_DO <> '' THEN FORMAT(a.Tarikh_Terima_DO, 'dd-MM-yyyy') END AS Tarikh_Terima_DO
                                FROM SMKB_Pembayaran_Invois_Hdr a, SMKB_Pembayaran_Jenis_Bayar b, SMKB_Pembayaran_Jenis_Invois c, SMKB_Pembayaran_Invois_Penerima e, SMKB_Pemiutang_Master f  
                                WHERE a.Status_Dok = '02' AND a.Jenis_Bayar = b.Kod AND a.Jenis_Invois = c.Kod AND a.ID_Rujukan = e.ID_Rujukan AND e.Kod_Pemiutang = f.Kod_Pemiutang"

        Return db.Read(query)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetPemiutangListSah(ByVal q As String) As String
        Dim tmpDT As DataTable = GetPenerimaListSah(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetPenerimaListSah(kod As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT Kod_Pemiutang as value, Kategori_Pemiutang + ' - ' + Nama_Pemiutang as text 
                                FROM SMKB_Pemiutang_Master WHERE Status='1'"
        Dim param As New List(Of SqlParameter)
        If kod <> "" Then
            query &= " AND Kod_Pemiutang LIKE '%' + @kod + '%' OR Nama_Pemiutang LIKE '%' + @kod2 + '%' "
            param.Add(New SqlParameter("@kod", kod))
            param.Add(New SqlParameter("@kod2", kod))
        End If

        Return db.Read(query, param)
    End Function

End Class