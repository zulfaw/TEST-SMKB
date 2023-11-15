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
Imports Newtonsoft.Json.Linq
Imports Org.BouncyCastle.Asn1.Sec


' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class LejarPenghutangWS
    Inherits System.Web.Services.WebService

    Dim sqlcmd As SqlCommand
    Dim sqlcon As SqlConnection
    Dim sqlread As SqlDataReader
    Dim dt As DataTable


    Public Function HelloWorld() As String
        Return "Hello World"
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetVotList(ByVal q As String, ByVal kodVot As String) As String
        'Dim newList As List(Of ItemList)
        'newList = listItem

        'If (q <> "") Then
        '    newList = listItem.Where(
        '        Function(x) x.value.Contains(q) Or x.text.Contains(q)
        '    ).ToList()
        'End If

        Dim tmpDT As DataTable = GetKodVotList(q, kodVot)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetVotPTJ(ByVal q As String) As String
        'Dim newList As List(Of ItemList)
        'newList = listItem

        'If (q <> "") Then
        '    newList = listItem.Where(
        '        Function(x) x.value.Contains(q) Or x.text.Contains(q)
        '    ).ToList()
        'End If

        Dim tmpDT As DataTable = GetKodPtjList(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function


    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetVotCOA(ByVal q As String) As String


        Dim tmpDT As DataTable = GetKodCOAList(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function


    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetKWList(ByVal q As String, ByVal kodkw As String) As String
        'Dim newList As List(Of ItemList)
        'newList = listItem

        'If (q <> "") Then
        '    newList = listItem.Where(
        '        Function(x) x.value.Contains(q) Or x.text.Contains(q)
        '    ).ToList()
        'End If

        Dim tmpDT As DataTable = GetKodKWList(q, kodkw)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetKoList(ByVal q As String, ByVal kodko As String) As String
        'Dim newList As List(Of ItemList)
        'newList = listItem

        'If (q <> "") Then
        '    newList = listItem.Where(
        '        Function(x) x.value.Contains(q) Or x.text.Contains(q)
        '    ).ToList()
        'End If

        Dim tmpDT As DataTable = GetKodKoList(q, kodko)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetKpList(ByVal q As String, ByVal kodko As String) As String
        'Dim newList As List(Of ItemList)
        'newList = listItem

        'If (q <> "") Then
        '    newList = listItem.Where(
        '        Function(x) x.value.Contains(q) Or x.text.Contains(q)
        '    ).ToList()
        'End If

        Dim tmpDT As DataTable = GetKodKPList(q, kodko)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function


    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetJenisTransaksi(ByVal q As String) As String
        'Dim newList As List(Of ItemList)
        'newList = listItem

        'If (q <> "") Then
        '    newList = listItem.Where(
        '        Function(x) x.value.Contains(q) Or x.text.Contains(q)
        '    ).ToList()
        'End If

        Dim tmpDT As DataTable = GetJenTransaksi(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetJenisStatus(ByVal q As String) As String
        'Dim newList As List(Of ItemList)
        'newList = listItem

        'If (q <> "") Then
        '    newList = listItem.Where(
        '        Function(x) x.value.Contains(q) Or x.text.Contains(q)
        '    ).ToList()
        'End If

        Dim tmpDT As DataTable = GetJenStatus(q)
        Return JsonConvert.SerializeObject(tmpDT)
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
    Public Function SaveOrders(order As Order) As String
        Dim resp As New ResponseRepository
        resp.Success("Data telah disimpan")
        Dim success As Integer = 0
        Dim JumRekod As Integer = 0
        If order Is Nothing Then
            resp.Failed("Tiada simpan")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        If order.JumlahBeza <> 0.00 Then
            resp.Failed("Gagal Menyimpan maklumat. Sila pastikan jumlah debit dan jumlah kredit seimbang.")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        If order.OrderID = "" Then 'untuk permohonan baru
            order.OrderID = GenerateOrderID()
            If InsertNewOrder(order.OrderID, order.NoRujukan, order.Perihal, order.Tarikh, order.JenisTransaksi, order.JumlahDebit) <> "OK" Then
                resp.Failed("Gagal Menyimpan order 1266")
                Return JsonConvert.SerializeObject(resp.GetResult())
                ' Exit Function
            End If
        Else 'untuk permohonan sedia ada

            'start delete dlu detail sedia ada...
            DeleteOrderHdr(order.OrderID, order.NoRujukan, order.Perihal, order.Tarikh, order.JenisTransaksi, order.JumlahDebit)
            DeleteOrderDtl(order.OrderID)
            'end delete

            If UpdateNewOrder(order.OrderID, order.NoRujukan, order.Perihal, order.Tarikh, order.JenisTransaksi, order.JumlahDebit) <> "OK" Then
                If InsertNewOrder(order.OrderID, order.NoRujukan, order.Perihal, order.Tarikh, order.JenisTransaksi, order.JumlahDebit) <> "OK" Then
                    resp.Failed("Gagal Menyimpan order 1266")
                    Return JsonConvert.SerializeObject(resp.GetResult())
                    ' Exit Function
                End If
            End If
        End If

        For Each orderDetail As OrderDetail In order.OrderDetails

            If orderDetail.ddlVot = "" Then
                Continue For
            End If

            JumRekod += 1

            'orderDetail.kredit = 0 'orderDetail.quantity * orderDetail.debit 'This can be automated insie orderdetail model

            If orderDetail.id = "" Then
                orderDetail.id = GenerateOrderDetailID(order.OrderID)
                orderDetail.OrderID = order.OrderID
                If InsertOrderDetail(orderDetail) = "OK" Then
                    success += 1
                End If
            Else
                If InsertOrderDetail(orderDetail) = "OK" Then
                    success += 1
                End If
            End If
        Next

        If UpdateStatusDokOrder_Mohon(order.OrderID, "Y") <> "OK" Then

            'resp.Failed("Berjaya simpan") 'Gagal Menyimpan order YX
            Return JsonConvert.SerializeObject(resp.GetResult())
            ' Exit Function

        End If

        If success = 0 Then
            resp.Failed("Rekod order detail gagal disimpan")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        If Not success = JumRekod Then
            resp.Success("Rekod order detail berjaya disimpan dengan beberapa rekod tidak disimpan", "00", order)
            Return JsonConvert.SerializeObject(resp.GetResult())
        Else
            resp.Success("Rekod berjaya disimpan", "00", order)
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function


    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SubmitOrders(order As Order) As String
        Dim resp As New ResponseRepository
        resp.Success("Data telah di hantar.")
        Dim success As Integer = 0
        Dim JumRekod As Integer = 0
        If order Is Nothing Then
            resp.Failed("Tiada simpan")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        If order.JumlahBeza <> 0.00 Then
            resp.Failed("Gagal menghantar maklumat. Sila pastikan jumlah debit dan jumlah kredit seimbang.")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        If order.OrderID = "" Then
            order.OrderID = GenerateOrderID()
            If InsertNewOrder(order.OrderID, order.NoRujukan, order.Perihal, order.Tarikh, order.JenisTransaksi, order.JumlahDebit) <> "OK" Then
                resp.Failed("Gagal menghantar maklumat")
                Return JsonConvert.SerializeObject(resp.GetResult())
                ' Exit Function
            End If
        Else
            'start delete dlu detail sedia ada...
            DeleteOrderHdr(order.OrderID, order.NoRujukan, order.Perihal, order.Tarikh, order.JenisTransaksi, order.JumlahDebit)
            DeleteOrderDtl(order.OrderID)
            'end delete

            If UpdateNewOrder(order.OrderID, order.NoRujukan, order.Perihal, order.Tarikh, order.JenisTransaksi, order.JumlahDebit) <> "OK" Then
                If InsertNewOrder(order.OrderID, order.NoRujukan, order.Perihal, order.Tarikh, order.JenisTransaksi, order.JumlahDebit) <> "OK" Then
                    resp.Failed("Gagal Menyimpan order 1266")
                    Return JsonConvert.SerializeObject(resp.GetResult())
                    ' Exit Function
                End If
            End If

        End If

        For Each orderDetail As OrderDetail In order.OrderDetails

            If orderDetail.ddlVot = "" Then
                Continue For
            End If

            JumRekod += 1

            'orderDetail.kredit = 0 'orderDetail.quantity * orderDetail.debit 'This can be automated insie orderdetail model

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

        If UpdateStatusDokOrder_Submit(order.OrderID, "Y") <> "OK" Then

            'resp.Failed("Berjaya simpan") 'Gagal Menyimpan order YX
            Return JsonConvert.SerializeObject(resp.GetResult())
            ' Exit Function

        End If


        If success = 0 Then
            resp.Failed("Rekod order detail gagal dihantar")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        If Not success = JumRekod Then
            resp.Success("Rekod order detail berjaya disimpan dengan beberapa rekod tidak disimpan", "00", order)
            Return JsonConvert.SerializeObject(resp.GetResult())
        Else
            'start call function submit
            UpdateStatusSubmit(order.OrderID)
            'end call function submit

            resp.Success("Rekod berjaya dihantar", "00", order)



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

            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        Return JsonConvert.SerializeObject(resp.GetResult())
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


    Private Function InsertOrderDetail(orderDetail As OrderDetail)
        Dim db As New DBKewConn
        Dim query As String = "INSERT INTO SMKB_Jurnal_Dtl
        VALUES(@No_Jurnal , @No_Item, @Kod_Kump_Wang, @Kod_Operasi , @Kod_Projek, @Kod_PTJ  , @Kod_Vot ,
        @Butiran, @Debit, @Kredit, @Kod_Penyesuaian, @ID_Penyesuaian, @Status_Lejar, NULL , @status)"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@No_Jurnal", orderDetail.OrderID))
        param.Add(New SqlParameter("@No_Item", orderDetail.id))
        param.Add(New SqlParameter("@Kod_Kump_Wang", orderDetail.ddlKw))
        param.Add(New SqlParameter("@Kod_Operasi", orderDetail.ddlKo))
        param.Add(New SqlParameter("@Kod_Projek", orderDetail.ddlKp))
        param.Add(New SqlParameter("@Kod_PTJ", orderDetail.ddlPTJ))
        param.Add(New SqlParameter("@Kod_Vot", orderDetail.ddlVot))
        param.Add(New SqlParameter("@Butiran", "test"))
        param.Add(New SqlParameter("@Debit", orderDetail.debit))
        param.Add(New SqlParameter("@Kredit", orderDetail.kredit))
        param.Add(New SqlParameter("@Kod_Penyesuaian", "U"))
        param.Add(New SqlParameter("@ID_Penyesuaian", "-"))
        param.Add(New SqlParameter("@Status_Lejar", "-"))
        param.Add(New SqlParameter("@Status", 1))

        Return db.Process(query, param)
    End Function

    Private Function UpdateOrderDetail(orderDetail As OrderDetail)
        Dim db = New db
        Dim query As String = "UPDATE ORDERDETAILS
        set ddlVot = @ddlVot, details = @details, quantity = @quantity, 
        price = @price, amount = @amount
        where id = @id"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@ddlVot", orderDetail.ddlVot))
        param.Add(New SqlParameter("@price", orderDetail.debit))
        param.Add(New SqlParameter("@amount", orderDetail.kredit))
        param.Add(New SqlParameter("@id", orderDetail.id))

        Return db.Process(query, param)
    End Function

    Private Function UpdateStatusSubmit(kod As String)
        Dim db As New DBKewConn

        Dim statusdok As String = "02" 'hantar
        Dim query As String = "Update SMKB_Jurnal_Hdr set Kod_Status_Dok =  @statusdok where No_Jurnal = @No_Jurnal and Status = 1"

        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@No_Jurnal", kod))
        param.Add(New SqlParameter("@statusdok", statusdok))


        Return db.Process(query, param)
    End Function

    Private Function GenerateOrderDetailID(orderid As String) As String
        Dim db = New DBKewConn
        Dim lastID As Integer = 1
        Dim newOrderID As String = ""
        Dim param As New List(Of SqlParameter)

        Dim query As String = "select TOP 1 No_Item as id
        from SMKB_Jurnal_Dtl 
        where No_Jurnal = @orderid
        ORDER BY No_Item DESC"

        param.Add(New SqlParameter("@orderid", orderid))

        dt = db.Read(query, param)

        If dt.Rows.Count > 0 Then
            lastID = CInt(dt.Rows(0).Item("id")) + 1
        End If

        newOrderID = lastID

        Return newOrderID
    End Function

    Private Function InsertNewOrder(orderid As String, norujukan As String, perihal As String, tarikh As String, jenistransaksi As String, jumlahBesar As Decimal)
        Dim db As New DBKewConn
        Dim query As String = "INSERT INTO SMKB_Jurnal_Hdr
        VALUES(@No_Jurnal ,@Tarikh, 'GL-JE', @JenisTrans, @No_Rujukan, @Butiran, @Jumlah, '01', '-' ,'-' ,'-' ,'-' ,'-' ,'1')"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@No_Jurnal", orderid))
        param.Add(New SqlParameter("@No_Rujukan", norujukan))
        param.Add(New SqlParameter("@Butiran", perihal))
        param.Add(New SqlParameter("@Jumlah", jumlahBesar))
        param.Add(New SqlParameter("@Tarikh", tarikh))
        param.Add(New SqlParameter("@JenisTrans", jenistransaksi))
        'param.Add(New SqlParameter("@Kod_Projek","0000000"))
        'param.Add(New SqlParameter("@Kod_PTJ", "430000"))
        'param.Add(New SqlParameter("@Kod_Vot", orderDetail.ddlVot))
        'param.Add(New SqlParameter("@Debit", 53))
        'param.Add(New SqlParameter("@Kredit", 69))
        'param.Add(New SqlParameter("@Kod_Penyesuaian", "U"))
        'param.Add(New SqlParameter("@ID_Penyesuaian", "-"))
        'param.Add(New SqlParameter("@Status_Lejar", "-"))
        'param.Add(New SqlParameter("@Status", 1))

        Return db.Process(query, param)
    End Function

    Private Function DeleteOrderHdr(orderid As String, norujukan As String, perihal As String, tarikh As String, jenistransaksi As String, jumlahBesar As Decimal)
        Dim db As New DBKewConn
        Dim query As String = "DELETE FROM SMKB_Jurnal_Hdr WHERE No_Jurnal=@No_Jurnal "
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@No_Jurnal", orderid))

        Return db.Process(query, param)
    End Function

    Private Function DeleteOrderDtl(orderid As String)
        Dim db As New DBKewConn
        Dim query As String = "DELETE FROM SMKB_Jurnal_Dtl WHERE No_Jurnal=@No_Jurnal AND Status = 1"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@No_Jurnal", orderid))

        Return db.Process(query, param)
    End Function


    Private Function UpdateNewOrder(orderid As String, norujukan As String, perihal As String, tarikh As String, jenistransaksi As String, jumlahBesar As Decimal)
        Dim db As New DBKewConn
        Dim query As String = "UPDATE SMKB_Jurnal_Hdr SET Jenis_Trans = @JenisTrans , No_Rujukan =@No_Rujukan , Butiran =@Butiran , Jumlah =@Jumlah
                               WHERE No_Jurnal=@No_Jurnal AND Status = 1"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@No_Jurnal", orderid))
        param.Add(New SqlParameter("@No_Rujukan", norujukan))
        param.Add(New SqlParameter("@Butiran", perihal))
        param.Add(New SqlParameter("@Jumlah", jumlahBesar))
        param.Add(New SqlParameter("@JenisTrans", jenistransaksi))

        Return db.Process(query, param)
    End Function

    Private Function UpdateLulusOrder(orderid As String)
        Dim db As New DBKewConn

        Dim kodstatusLulus As String = "04" 'Kelulusan 1

        Dim query As String = "UPDATE SMKB_Jurnal_Hdr SET Kod_Status_Dok = @kodStatus
                               WHERE No_Jurnal=@No_Jurnal AND Status = 1"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@No_Jurnal", orderid))
        param.Add(New SqlParameter("@kodStatus", kodstatusLulus))

        Return db.Process(query, param)

    End Function

    Private Function UpdateLulusOrderDetail(orderid As String)
        Dim db As New DBKewConn


        Dim query As String = "INSERT INTO SMKB_Lejar_Am
        select year(b.Tkh_Transaksi) as Tahun, Kod_Kump_Wang, Kod_Operasi, Kod_PTJ, Kod_Vot, Kod_Projek , 'UTeM', 
        CASE WHEN MONTH(b.Tkh_Transaksi) = 1 THEN SUM(Debit) ELSE 0 END AS Debit_1 ,
        CASE WHEN MONTH(b.Tkh_Transaksi) = 1 THEN SUM(Kredit) ELSE 0 END AS Kredit_1 ,
        CASE WHEN MONTH(b.Tkh_Transaksi) = 2 THEN SUM(Debit) ELSE 0 END AS Debit_2 ,
        CASE WHEN MONTH(b.Tkh_Transaksi) = 2 THEN SUM(Kredit) ELSE 0 END AS Kredit_2 ,
        CASE WHEN MONTH(b.Tkh_Transaksi) = 3 THEN SUM(Debit) ELSE 0 END AS Debit_3 ,
        CASE WHEN MONTH(b.Tkh_Transaksi) = 3 THEN SUM(Kredit) ELSE 0 END AS Kredit_3 ,
        CASE WHEN MONTH(b.Tkh_Transaksi) = 4 THEN SUM(Debit) ELSE 0 END AS Debit_4 ,
        CASE WHEN MONTH(b.Tkh_Transaksi) = 4 THEN SUM(Kredit) ELSE 0 END AS Kredit_4 ,
        CASE WHEN MONTH(b.Tkh_Transaksi) = 5 THEN SUM(Debit) ELSE 0 END AS Debit_5 ,
        CASE WHEN MONTH(b.Tkh_Transaksi) = 5 THEN SUM(Kredit) ELSE 0 END AS Kredit_5 ,
        CASE WHEN MONTH(b.Tkh_Transaksi) = 6 THEN SUM(Debit) ELSE 0 END AS Debit_6 ,
        CASE WHEN MONTH(b.Tkh_Transaksi) = 6 THEN SUM(Kredit) ELSE 0 END AS Kredit_6 ,
        CASE WHEN MONTH(b.Tkh_Transaksi) = 7 THEN SUM(Debit) ELSE 0 END AS Debit_7 ,
        CASE WHEN MONTH(b.Tkh_Transaksi) = 7 THEN SUM(Kredit) ELSE 0 END AS Kredit_7 ,
        CASE WHEN MONTH(b.Tkh_Transaksi) = 8 THEN SUM(Debit) ELSE 0 END AS Debit_8 ,
        CASE WHEN MONTH(b.Tkh_Transaksi) = 8 THEN SUM(Kredit) ELSE 0 END AS Kredit_8 ,
        CASE WHEN MONTH(b.Tkh_Transaksi) = 9 THEN SUM(Debit) ELSE 0 END AS Debit_9 ,
        CASE WHEN MONTH(b.Tkh_Transaksi) = 9 THEN SUM(Kredit) ELSE 0 END AS Kredit_9 ,
        CASE WHEN MONTH(b.Tkh_Transaksi) = 10 THEN SUM(Debit) ELSE 0 END AS Debit_10 ,
        CASE WHEN MONTH(b.Tkh_Transaksi) = 10 THEN SUM(Kredit) ELSE 0 END AS Kredit_10 ,
        CASE WHEN MONTH(b.Tkh_Transaksi) = 11 THEN SUM(Debit) ELSE 0 END AS Debit_11 ,
        CASE WHEN MONTH(b.Tkh_Transaksi) = 11 THEN SUM(Kredit) ELSE 0 END AS Kredit_11 ,
        CASE WHEN MONTH(b.Tkh_Transaksi) = 12 THEN SUM(Debit) ELSE 0 END AS Debit_12 ,
        CASE WHEN MONTH(b.Tkh_Transaksi) = 12 THEN SUM(Kredit) ELSE 0 END AS Kredit_12 ,
        CASE WHEN MONTH(b.Tkh_Transaksi) = 13 THEN SUM(Debit) ELSE 0 END AS Debit_13 ,
        CASE WHEN MONTH(b.Tkh_Transaksi) = 13 THEN SUM(Kredit) ELSE 0 END AS Kredit_13 ,
        b.Status
        from SMKB_Jurnal_Dtl a ,  SMKB_Jurnal_Hdr b 
        where a.No_Jurnal = b.No_Jurnal and
        a.No_Jurnal = @No_Jurnal
        and a.Status = 1
        and b.Status = 1
        group by year(b.Tkh_Transaksi) , Kod_Kump_Wang, Kod_Operasi, Kod_PTJ, Kod_Vot, Kod_Projek , MONTH(b.Tkh_Transaksi), b.Status"

        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@No_Jurnal", orderid))


        Return db.Process(query, param)

    End Function

    Private Function UpdateXLulusOrder(orderid As String)
        Dim db As New DBKewConn


        Dim kodstatusXLulus As String = "09" 'Gagal Kelulusan 1

        Dim query As String = "UPDATE SMKB_Jurnal_Hdr SET Kod_Status_Dok = @kodStatus
                               WHERE No_Jurnal=@No_Jurnal AND Status = 1"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@No_Jurnal", orderid))
        param.Add(New SqlParameter("@kodStatus", kodstatusXLulus))

        Return db.Process(query, param)

    End Function

    Private Function UpdateStatusDokOrder(orderid As String, statusLulus As String)
        Dim db As New DBKewConn

        Dim kodstatusLulus As String

        If statusLulus = "Y" Then

            kodstatusLulus = "04"

        ElseIf statusLulus = "N" Then

            kodstatusLulus = "09"

        End If


        Dim query As String = "INSERT INTO SMKB_Status_Dok (Kod_Modul  , Kod_Status_Dok  ,  No_Rujukan , No_Staf , Tkh_Tindakan , Tkh_Transaksi , Status_Transaksi , Status , Ulasan )
							VALUES
							(@Kod_Modul , @Kod_Status_Dok , @No_Rujukan , @No_Staf , getdate() , getdate(), @Status_Transaksi , @Status , @Ulasan)"

        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@Kod_Modul", "04"))
        param.Add(New SqlParameter("@Kod_Status_Dok", kodstatusLulus))
        param.Add(New SqlParameter("@No_Rujukan", orderid))
        param.Add(New SqlParameter("@No_Staf", "02634"))
        'param.Add(New SqlParameter("@Tkh_Tindakan", orderid))
        'param.Add(New SqlParameter("@Tkh_Transaksi", orderid))
        param.Add(New SqlParameter("@Status_Transaksi", 1))
        param.Add(New SqlParameter("@Status", 1))
        param.Add(New SqlParameter("@Ulasan", "-"))

        Return db.Process(query, param)

    End Function

    Private Function UpdateStatusDokOrder_Mohon(orderid As String, statusLulus As String)
        Dim db As New DBKewConn

        Dim kodstatusLulus As String

        If statusLulus = "Y" Then

            kodstatusLulus = "02"

        End If


        Dim query As String = "INSERT INTO SMKB_Status_Dok (Kod_Modul  , Kod_Status_Dok  ,  No_Rujukan , No_Staf , Tkh_Tindakan , Tkh_Transaksi , Status_Transaksi , Status , Ulasan )
							VALUES
							(@Kod_Modul , @Kod_Status_Dok , @No_Rujukan , @No_Staf , getdate() , getdate(), @Status_Transaksi , @Status , @Ulasan)"

        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@Kod_Modul", "04"))
        param.Add(New SqlParameter("@Kod_Status_Dok", kodstatusLulus))
        param.Add(New SqlParameter("@No_Rujukan", orderid))
        param.Add(New SqlParameter("@No_Staf", "02634"))
        'param.Add(New SqlParameter("@Tkh_Tindakan", orderid))
        'param.Add(New SqlParameter("@Tkh_Transaksi", orderid))
        param.Add(New SqlParameter("@Status_Transaksi", 1))
        param.Add(New SqlParameter("@Status", 1))
        param.Add(New SqlParameter("@Ulasan", "-"))

        Return db.Process(query, param)

    End Function

    Private Function UpdateStatusDokOrder_Submit(orderid As String, statusLulus As String)
        Dim db As New DBKewConn

        Dim kodstatusLulus As String

        If statusLulus = "Y" Then

            kodstatusLulus = "04"

        End If


        Dim query As String = "INSERT INTO SMKB_Status_Dok (Kod_Modul  , Kod_Status_Dok  ,  No_Rujukan , No_Staf , Tkh_Tindakan , Tkh_Transaksi , Status_Transaksi , Status , Ulasan )
							VALUES
							(@Kod_Modul , @Kod_Status_Dok , @No_Rujukan , @No_Staf , getdate() , getdate(), @Status_Transaksi , @Status , @Ulasan)"

        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@Kod_Modul", "04"))
        param.Add(New SqlParameter("@Kod_Status_Dok", kodstatusLulus))
        param.Add(New SqlParameter("@No_Rujukan", orderid))
        param.Add(New SqlParameter("@No_Staf", "02634"))
        'param.Add(New SqlParameter("@Tkh_Tindakan", orderid))
        'param.Add(New SqlParameter("@Tkh_Transaksi", orderid))
        param.Add(New SqlParameter("@Status_Transaksi", 1))
        param.Add(New SqlParameter("@Status", 1))
        param.Add(New SqlParameter("@Ulasan", "-"))

        Return db.Process(query, param)

    End Function


    Private Function GenerateOrderID() As String
        Dim db As New DBKewConn

        Dim year = Date.Now.ToString("yyyy")
        Dim month = Date.Now.Month

        Dim lastID As Integer = 1
        Dim newOrderID As String = ""

        Dim query As String = $"select TOP 1 No_Akhir as id from SMKB_No_Akhir where Kod_Modul ='07' AND Prefix ='JK' AND Tahun =@year"
        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@year", year))

        dt = db.Read(query, param)

        If dt.Rows.Count > 0 Then
            lastID = CInt(dt.Rows(0).Item("id")) + 1

            UpdateNoAkhir("07", "JK", year, lastID)
        Else

            InsertNoAkhir("07", "JK", year, lastID)
        End If
        newOrderID = "JK" + Format(lastID, "000000").ToString + month.ToString("00") + Right(year.ToString(), 2)

        Return newOrderID
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

    Private Function GetKodVotList(kod As String, kodVot As String) As DataTable
        Dim db As New DBKewConn
        Dim query As String = "SELECT DISTINCT A.Kod_Vot as value,B.Butiran as text
                                FROM SMKB_COA_Master A
                                INNER JOIN SMKB_Vot B ON A.Kod_Vot=B.Kod_Vot"

        Dim param As New List(Of SqlParameter)
        If kod <> "" Then
            query &= " where A.Status = 1 and (A.Kod_Vot LIKE '%' + @kod + '%' or B.Butiran LIKE '%' + @kod2 + '%') and A.Kod_PTJ =@kod3 order by A.Kod_Vot"
        Else
            query &= " where A.Status = 1 and A.Kod_PTJ =@kod3 order by A.Kod_Vot"
        End If

        param.Add(New SqlParameter("@kod", kod))
        param.Add(New SqlParameter("@kod2", kod))
        param.Add(New SqlParameter("@kod3", kodVot))

        Return db.Read(query, param)
    End Function

    Private Function GetKodKWList(kod As String, kodkw As String) As DataTable
        Dim db As New DBKewConn
        Dim query As String = "SELECT DISTINCT A.Kod_Kump_Wang as value,B.Butiran as text
                                FROM SMKB_COA_Master A
                                INNER JOIN SMKB_Kump_Wang B ON A.Kod_Kump_Wang=B.Kod_Kump_Wang"
        Dim param As New List(Of SqlParameter)
        If kod <> "" Then
            query &= " where A.Status = 1 and (A.Kod_Kump_Wang LIKE '%' + @kod + '%' or B.Butiran LIKE '%' + @kod2 + '%') and A.Kod_Vot =@kod3 order by A.Kod_Kump_Wang"
        Else
            query &= " where A.Status = 1 and A.Kod_Vot =@kod3 order by A.Kod_Kump_Wang"
        End If

        param.Add(New SqlParameter("@kod", kod))
        param.Add(New SqlParameter("@kod2", kod))
        param.Add(New SqlParameter("@kod3", kodkw))

        Return db.Read(query, param)
    End Function

    Private Function GetKodPtjList(kod As String) As DataTable
        Dim db As New DBKewConn
        Dim query As String = "Select distinct Kod_PTJ as value, 
        (SELECT b.Pejabat FROM [DEVMIS\SQL_INS01].dbStaf.dbo.MS_PEJABAT as b
        WHERE b.STATUS = 1 and b.kodpejabat = left(Kod_PTJ,2)) as text
        from SMKB_COA_Master  "

        Dim param As New List(Of SqlParameter)
        If kod <> "" Then
            query &= " where Status = 1 and (Kod_PTJ LIKE '%' + @kod + '%' or 
        (left(Kod_PTJ,2) in (SELECT b.kodpejabat FROM [DEVMIS\SQL_INS01].dbStaf.dbo.MS_PEJABAT as b
        WHERE b.STATUS = 1 and b.kodpejabat = left(Kod_PTJ,2) and b.Pejabat like '%' + @kod2 + '%'))) order by Kod_PTJ"
            param.Add(New SqlParameter("@kod", kod))
            param.Add(New SqlParameter("@kod2", kod))
        End If

        Return db.Read(query, param)
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

    Private Function GetKodKoList(kod As String, kodko As String) As DataTable
        Dim db As New DBKewConn
        Dim query As String = "SELECT DISTINCT A.Kod_Operasi as value, B.Butiran As text
                                From SMKB_COA_Master A
                                INNER Join SMKB_Operasi B ON A.Kod_Operasi=B.Kod_Operasi"
        Dim param As New List(Of SqlParameter)
        If kod <> "" Then
            query &= " where A.Status = 1 And (A.Kod_Operasi Like '%' + @kod + '%' or B.Butiran LIKE '%' + @kod2 + '%') and A.Kod_Kump_Wang =@kod3 order by A.Kod_Operasi"
        Else
            query &= " where A.Status = 1 and A.Kod_Kump_Wang =@kod3 order by A.Kod_Operasi"
        End If

        param.Add(New SqlParameter("@kod", kod))
        param.Add(New SqlParameter("@kod2", kod))
        param.Add(New SqlParameter("@kod3", kodko))

        Return db.Read(query, param)
    End Function

    Private Function GetKodKPList(kod As String, kodkp As String) As DataTable
        Dim db As New DBKewConn
        Dim query As String = "SELECT DISTINCT A.Kod_Projek as value,B.Butiran as text FROM SMKB_COA_Master A
                                INNER JOIN SMKB_Projek B ON A.Kod_Projek = B.Kod_Projek"
        Dim param As New List(Of SqlParameter)
        If kod <> "" Then
            query &= " where A.Status = 1 and (A.Kod_Projek LIKE '%' + @kod + '%' or B.Butiran LIKE '%' + @kod2 + '%') and A.Kod_Operasi =@kod3 order by A.Kod_Projek"
        Else
            query &= " where A.Status = 1 and A.Kod_Operasi =@kod3 order by A.Kod_Projek"
        End If

        param.Add(New SqlParameter("@kod", kod))
        param.Add(New SqlParameter("@kod2", kod))
        param.Add(New SqlParameter("@kod3", kodkp))

        Return db.Read(query, param)
    End Function

    Private Function GetJenTransaksi(kod As String) As DataTable
        Dim db As New DBKewConn
        Dim query As String = "select Kod_Trans as value, Butiran as text From SMKB_Jurnal_Jenis_Trans "
        Dim param As New List(Of SqlParameter)
        If kod <> "" Then
            query &= " where (Kod_Trans='A'or Kod_Trans='L') and Butiran like 'PELARASAN%'  and (Kod_Trans LIKE '%' + @kod + '%' or Butiran LIKE '%' + @kod2 + '%')"
            param.Add(New SqlParameter("@kod", kod))
            param.Add(New SqlParameter("@kod2", kod))
        End If

        Return db.Read(query, param)
    End Function

    Private Function GetJenStatus(kod As String) As DataTable
        Dim db As New DBKewConn
        Dim query As String = "select Butiran  as text, Butiran as value , Kod_Status_Dok from SMKB_Kod_Status_Dok where Kod_Modul = '04' and Status = 1 
                                union 
                                select 'SEMUA' AS text , 'SEMUA' AS value , '00' AS Kod_Status_Dok 
                                order by Kod_Status_Dok"

        'Dim param As New List(Of SqlParameter)
        'If kod <> "" Then
        '    query &= "where Kod_Modul = '04' and Status = 1 and (Kod_Status_Dok LIKE '%' + @kod + '%' or Butiran LIKE '%' + @kod2 + '%')  "
        '    param.Add(New SqlParameter("@kod", kod))
        '    param.Add(New SqlParameter("@kod2", kod))
        'End If

        Return db.Read(query)
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
    Public Function LoadOrderRecord_SenaraiLulusTransaksiJurnal() As String
        Dim resp As New ResponseRepository

        dt = GetOrder_SenaraiLulusTransaksiJurnal()
        'resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(dt)
    End Function


    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetOrder_SenaraiLulusTransaksiJurnal() As DataTable
        Dim db = New DBKewConn

        Dim query As String = "select A.No_Jurnal , A.No_Rujukan, A.Butiran, Jumlah , FORMAT (A.Tkh_Transaksi, 'dd-MM-yyyy') as Tkh_Transaksi , 
        (select Butiran from SMKB_Kod_Status_Dok where Kod_Modul = '04' and Status = 1 and Kod_Status_Dok = A.Kod_Status_Dok) as Kod_Status_Dok ,
        (select Butiran from SMKB_Jurnal_Jenis_Trans b where b.Kod_Trans = A.Jenis_Trans) as Jenis_Trans , MS01_Nama as No_Staf ,
		 Kod_Vot ,Debit , Kredit
        from SMKB_Jurnal_Hdr A 
		INNER JOIN SMKB_Jurnal_Dtl AS DT ON A.No_Jurnal = DT.No_Jurnal
		LEFT JOIN SMKB_Status_Dok AS B ON A.No_Jurnal = B.No_Rujukan AND B.Kod_Status_Dok = '02'
		Left join [DEVMIS\SQL_INS01].dbStaf.dbo.MS01_Peribadi as c on c.MS01_NoStaf = b.No_Staf
        where A.status = 1  and A.Kod_Status_Dok in ('04') order by a.Tkh_Transaksi desc"

        Return db.Read(query)
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadOrderRecord_SenaraiLulusTransaksiPNL(bulan As String, tahun As String, syarikat As String, ptj As String) As String
        Dim resp As New ResponseRepository
        If bulan = "" Or tahun = "" Or syarikat = "" Or ptj = "" Then
            Return JsonConvert.SerializeObject(New DataTable)
        End If
        Session("bulan") = bulan
        Session("tahun") = tahun
        Session("syarikat") = syarikat
        Session("ptj") = ptj
        dt = GetOrder_SenaraiLulusTransaksiPNL(bulan, tahun, syarikat, ptj)
        'resp.SuccessPayload(dt)
        Dim totalRecords As Integer = dt.Rows.Count
        'New With {
        '        .draw = 1,
        '        .recordsTotal = totalRecords,
        '        .recordsFiltered = 0,
        '        .data = dt
        '    }
        Return JsonConvert.SerializeObject(dt)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetOrder_SenaraiLulusTransaksiPNL(bulan As String, tahun As String, syarikat As String, ptj As String) As DataTable
        Dim db = New DBKewConn
        Dim dt As New DataTable

        'Dim connectionString As String = "server=v-sql12.utem.edu.my;database=DbKewangan;uid=smkb;pwd=smkb*pwd;"
        Dim connectionString As String = "server=devmis12.utem.edu.my;database=DbKewanganV4;uid=smkb;pwd=Smkb@Dev2012;"


        Using sqlconn As New SqlConnection(connectionString)
            Dim cmd As New SqlCommand
            sqlconn.Open()

            Dim query As String

            If ptj = "00" Then

                query = "SELECT substring(kodvot, 1, 2) + '000' as kodvot , ButiranDetail, Kod_Jenis, SUM(CAST(amaun AS DECIMAL(18, 2))) AS amaun , SUM(CAST(amaun2 AS DECIMAL(18, 2))) AS amaun2 FROM 
                        (
                        SELECT substring(A.Kod_Vot, 1, 2) + '000' AS kodvot,
	                        (select Butiran from SMKB_Vot where Kod_Vot = substring(A.Kod_Vot, 1, 2) + '000') as ButiranDetail,
	                        substring(A.Kod_Vot, 1, 1) + '0000' AS kodvotH,
	                        (select Butiran from SMKB_Vot where Kod_Vot = substring(A.Kod_Vot, 1, 1) + '0000') as ButiranHeader,
	                        B.Kod_Jenis, 
	                        REPLACE(SUM(
		                        CASE
		                        WHEN @bulan = 1 THEN (A.Dr_1 - A.Cr_1)
		                        WHEN @bulan = 2 THEN (A.Dr_1 + A.Dr_2) - (A.Cr_1 + A.Cr_2)
		                        WHEN @bulan = 3 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3) - (A.Cr_1 + A.Cr_2 + A.Cr_3)
		                        WHEN @bulan = 4 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4) - (A.Cr_1 + A.Cr_2 + A.Cr_2 + A.Cr_4)
		                        WHEN @bulan = 5 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5)
		                        WHEN @bulan = 6 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6)
		                        WHEN @bulan = 7 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7)
		                        WHEN @bulan = 8 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8)	
		                        WHEN @bulan = 9 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9)
		                        WHEN @bulan = 10 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10)
		                        WHEN @bulan = 11 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11)
		                        WHEN @bulan = 12 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)
		                        ELSE 0 
		                        END
	                        ), '-', '') AS amaun,
	                        REPLACE(SUM(CASE 
	                        WHEN @bulan = 1 THEN (A.Dr_1 - A.Cr_1)
	                        WHEN @bulan = 2 THEN (A.Dr_2 - A.Cr_2) 
	                        WHEN @bulan = 3 THEN (A.Dr_3 - A.Cr_3) 
                            WHEN @bulan = 4 THEN (A.Dr_4 - A.Cr_4) 
	                        WHEN @bulan = 5 THEN (A.Dr_5 - A.Cr_5)
                            WHEN @bulan = 6 THEN (A.Dr_6 - A.Cr_6)
	                        WHEN @bulan = 7 THEN (A.Dr_7 - A.Cr_7)
	                        WHEN @bulan = 8 THEN (A.Dr_8 - A.Cr_8)
	                        WHEN @bulan = 9 THEN (A.Dr_9 - A.Cr_9)
	                        WHEN @bulan = 10 THEN (A.Dr_10 - A.Cr_10)
	                        WHEN @bulan = 11 THEN (A.Dr_11 - A.Cr_11)
                            WHEN @bulan = 12 THEN (A.Dr_12 - A.Cr_12)

	                        ELSE 0 
	                        END), '-','') AS amaun2
                        FROM
                        SMKB_Lejar_Am A
                        JOIN SMKB_Vot B ON A.Kod_Vot=B.Kod_Vot
                        WHERE
                        A.Tahun = @tahun 
                        And B.Kod_Jenis In ('I','E')
                        And A.Kod_Kump_Wang >= '01'
                        And A.Kod_Kump_Wang <= '11'
                        And A.Kod_PTJ >= '010000'
                        And A.Kod_PTJ <='Y00005'
                        --AND Kod_Syarikat = 'UTeM'
                        GROUP BY
                        B.Kod_Jenis,A.Kod_Vot
                        )mainTbl WHERE (CAST(amaun as decimal)  <> 0.00 or CAST(amaun2 as decimal) <> 0.00)
                        Group By substring(kodvot, 1, 2) + '000' , Kod_Jenis , ButiranDetail
                        ORDER BY
                        Kod_Jenis DESC"

            Else query = "SELECT substring(kodvot, 1, 2) + '000' as kodvot , ButiranDetail, Kod_Jenis, SUM(CAST(amaun AS DECIMAL(18, 2))) AS amaun , SUM(CAST(amaun2 AS DECIMAL(18, 2))) AS amaun2 FROM 
                        (
                        SELECT substring(A.Kod_Vot, 1, 2) + '000' AS kodvot,
	                        (select Butiran from SMKB_Vot where Kod_Vot = substring(A.Kod_Vot, 1, 2) + '000') as ButiranDetail,
	                        substring(A.Kod_Vot, 1, 1) + '0000' AS kodvotH,
	                        (select Butiran from SMKB_Vot where Kod_Vot = substring(A.Kod_Vot, 1, 1) + '0000') as ButiranHeader,
	                        B.Kod_Jenis, 
	                        REPLACE(SUM(
		                        CASE
		                        WHEN @bulan = 1 THEN (A.Dr_1 - A.Cr_1)
		                        WHEN @bulan = 2 THEN (A.Dr_1 + A.Dr_2) - (A.Cr_1 + A.Cr_2)
		                        WHEN @bulan = 3 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3) - (A.Cr_1 + A.Cr_2 + A.Cr_3)
		                        WHEN @bulan = 4 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4) - (A.Cr_1 + A.Cr_2 + A.Cr_2 + A.Cr_4)
		                        WHEN @bulan = 5 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5)
		                        WHEN @bulan = 6 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6)
		                        WHEN @bulan = 7 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7)
		                        WHEN @bulan = 8 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8)	
		                        WHEN @bulan = 9 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9)
		                        WHEN @bulan = 10 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10)
		                        WHEN @bulan = 11 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11)
		                        WHEN @bulan = 12 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)
		                        ELSE 0 
		                        END
	                        ), '-', '') AS amaun,
	                        REPLACE(SUM(CASE 
	                        WHEN @bulan = 1 THEN (A.Dr_1 - A.Cr_1)
	                        WHEN @bulan = 2 THEN (A.Dr_2 - A.Cr_2) 
	                        WHEN @bulan = 3 THEN (A.Dr_3 - A.Cr_3) 
                            WHEN @bulan = 4 THEN (A.Dr_4 - A.Cr_4) 
	                        WHEN @bulan = 5 THEN (A.Dr_5 - A.Cr_5)
                            WHEN @bulan = 6 THEN (A.Dr_6 - A.Cr_6)
	                        WHEN @bulan = 7 THEN (A.Dr_7 - A.Cr_7)
	                        WHEN @bulan = 8 THEN (A.Dr_8 - A.Cr_8)
	                        WHEN @bulan = 9 THEN (A.Dr_9 - A.Cr_9)
	                        WHEN @bulan = 10 THEN (A.Dr_10 - A.Cr_10)
	                        WHEN @bulan = 11 THEN (A.Dr_11 - A.Cr_11)
                            WHEN @bulan = 12 THEN (A.Dr_12 - A.Cr_12)

	                        ELSE 0 
	                        END), '-','') AS amaun2
                        FROM
                        SMKB_Lejar_Am A
                        JOIN SMKB_Vot B ON A.Kod_Vot=B.Kod_Vot
                        WHERE
                        A.Tahun = @tahun 
                        And B.Kod_Jenis In ('I','E')
                        And A.Kod_Kump_Wang >= '01'
                        And A.Kod_Kump_Wang <= '11'
                        And A.Kod_PTJ >= @ptj
                        And A.Kod_PTJ <=@ptj
                        --AND Kod_Syarikat = 'UTeM'
                        GROUP BY
                        B.Kod_Jenis,A.Kod_Vot
                        )mainTbl WHERE (CAST(amaun as decimal)  <> 0.00 or CAST(amaun2 as decimal) <> 0.00)
                        Group By substring(kodvot, 1, 2) + '000' , Kod_Jenis , ButiranDetail
                        ORDER BY
                        Kod_Jenis DESC"
            End If

            cmd.Connection = sqlconn
            cmd.CommandText = query

            cmd.Parameters.Add(New SqlParameter("@tahun", tahun))
            cmd.Parameters.Add(New SqlParameter("@bulan", bulan))
            'cmd.Parameters.Add(New SqlParameter("@syarikat", syarikat))
            cmd.Parameters.Add(New SqlParameter("@ptj", ptj))
            dt.Load(cmd.ExecuteReader())
            Return dt
        End Using
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadRecord_ImbanganDuga(bulan As String, tahun As String, syarikat As String, ptj As String, kodkw As String) As String
        Dim resp As New ResponseRepository
        If bulan = "" Or tahun = "" Or syarikat = "" Or ptj = "" Or kodkw = "" Then
            Return JsonConvert.SerializeObject(New DataTable)
        End If
        Session("bulan") = bulan
        Session("tahun") = tahun
        Session("syarikat") = syarikat
        Session("ptj") = ptj
        Session("kodkw") = kodkw


        dt = Get_SenaraiImbanganDuga(bulan, tahun, syarikat, ptj, kodkw)
        Dim totalRecords As Integer = dt.Rows.Count

        Return JsonConvert.SerializeObject(dt)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function Get_SenaraiImbanganDuga(bulan As String, tahun As String, syarikat As String, ptj As String, kodkw As String) As DataTable
        Dim db = New DBKewConn
        Dim dt As New DataTable

        Dim connectionString As String = "server=devmis12.utem.edu.my;database=DbKewanganV4;uid=smkb;pwd=Smkb@Dev2012;"


        Using sqlconn As New SqlConnection(connectionString)
            Dim cmd As New SqlCommand
            sqlconn.Open()

            Dim query As String

            Dim optionalWhere As String = ""

            If Session("kodkw") = "00" Then
                optionalWhere = "And A.Kod_Kump_Wang >= 1 And A.Kod_Kump_Wang <= 11"
            Else
                optionalWhere = "And A.Kod_Kump_Wang >= @kodkw And A.Kod_Kump_Wang <= @kodkw"
            End If

            If ptj = "00" Then

                query = "SELECT * FROM 
                            (
                                SELECT 
                                    A.Kod_Vot AS kodvot,
                                    (SELECT Butiran FROM SMKB_Vot WHERE Kod_Vot = A.Kod_Vot) as ButiranDetail,
                                    A.Kod_Vot AS kodvotH,
                                    (SELECT Butiran FROM SMKB_Vot WHERE Kod_Vot = A.Kod_Vot) as ButiranHeader,
                                    B.Kod_Jenis, 
                                    REPLACE(
                                        SUM(
                                            CASE 
                                                WHEN @bulan = 1 THEN (A.Dr_1)
                                                WHEN @bulan = 2 THEN (A.Dr_1 + A.Dr_2)
                                                WHEN @bulan = 3 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3)
                                                WHEN @bulan = 4 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4)
                                                WHEN @bulan = 5 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5)
                                                WHEN @bulan = 6 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6)
                                                WHEN @bulan = 7 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7)
                                                WHEN @bulan = 8 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8)	
                                                WHEN @bulan = 9 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9)
                                                WHEN @bulan = 10 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10)
                                                WHEN @bulan = 11 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11)
                                                WHEN @bulan = 12 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12)
                                                ELSE 0 
                                            END
                                        ), '-', ''
                                    ) AS amaunTerkumpulDebit,
		                             REPLACE(
                                        SUM(
                                            CASE 
                                                WHEN @bulan = 1 THEN (A.Cr_1)
                                                WHEN @bulan = 2 THEN (A.Cr_1 + A.Cr_2)
                                                WHEN @bulan = 3 THEN (A.Cr_1 + A.Cr_2 + A.Cr_3)
                                                WHEN @bulan = 4 THEN (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4)
                                                WHEN @bulan = 5 THEN (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4  + A.Cr_5)
                                                WHEN @bulan = 6 THEN (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4  + A.Cr_5 + A.Cr_6)
                                                WHEN @bulan = 7 THEN (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4  + A.Cr_5 + A.Cr_6 + A.Cr_7)
                                                WHEN @bulan = 8 THEN (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4  + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8)	
                                                WHEN @bulan = 9 THEN (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4  + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9)
                                                WHEN @bulan = 10 THEN (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4  + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10)
                                                WHEN @bulan = 11 THEN (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4  + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11)
                                                WHEN @bulan = 12 THEN (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4  + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)
                                                ELSE 0 
                                            END
                                        ), '-', ''
                                    ) AS amaunTerkumpulKredit,
                                    REPLACE(
                                        SUM(
                                            CASE 
                                                WHEN @bulan = 1 THEN (A.Dr_1)
                                                WHEN @bulan = 2 THEN (A.Dr_2) 
                                                WHEN @bulan = 3 THEN (A.Dr_3) 
                                                WHEN @bulan = 4 THEN (A.Dr_4) 
                                                WHEN @bulan = 5 THEN (A.Dr_5)
                                                WHEN @bulan = 6 THEN (A.Dr_6)
                                                WHEN @bulan = 7 THEN (A.Dr_7)
                                                WHEN @bulan = 8 THEN (A.Dr_8)
                                                WHEN @bulan = 9 THEN (A.Dr_9)
                                                WHEN @bulan = 10 THEN (A.Dr_10)
                                                WHEN @bulan = 11 THEN (A.Dr_11)
                                                WHEN @bulan = 12 THEN (A.Dr_12)
                                                ELSE 0 
                                            END
                                        ), '-',''
                                    ) AS amaunSemasaDebit,
		                            REPLACE(
                                        SUM(
                                            CASE 
                                                WHEN @bulan = 1 THEN (A.Cr_1)
                                                WHEN @bulan = 2 THEN (A.Cr_2) 
                                                WHEN @bulan = 3 THEN (A.Cr_3) 
                                                WHEN @bulan = 4 THEN (A.Cr_4) 
                                                WHEN @bulan = 5 THEN (A.Cr_5)
                                                WHEN @bulan = 6 THEN (A.Cr_6)
                                                WHEN @bulan = 7 THEN (A.Cr_7)
                                                WHEN @bulan = 8 THEN (A.Cr_8)
                                                WHEN @bulan = 9 THEN (A.Cr_9)
                                                WHEN @bulan = 10 THEN (A.Cr_10)
                                                WHEN @bulan = 11 THEN (A.Cr_11)
                                                WHEN @bulan = 12 THEN (A.Cr_12)
                                                ELSE 0 
                                            END
                                        ), '-',''
                                    ) AS amaunSemasaKredit
                                FROM
                                    SMKB_Lejar_Am A
                                JOIN SMKB_Vot B ON A.Kod_Vot=B.Kod_Vot
                                WHERE
                                    A.Tahun = @tahun 
                                    --AND B.Kod_Jenis IN ('A','L','C')
                                    " + optionalWhere + "
                                    --And A.Kod_Kump_Wang >= @kodkw
                                    --And A.Kod_Kump_Wang <= @kodkw
                                    AND A.Kod_PTJ >= '010000'
                                    AND A.Kod_PTJ <= 'Y00005'
                                GROUP BY
                                    A.Kod_Vot, B.Kod_Jenis
                            ) mainTbl 
                            WHERE (CAST(amaunTerkumpulDebit AS DECIMAL) <> 0.00 OR CAST(amaunTerkumpulKredit AS DECIMAL)<> 0.00 OR CAST(amaunSemasaDebit AS DECIMAL) <> 0.00 OR CAST(amaunSemasaKredit AS DECIMAL) <> 0.00)
                            ORDER BY
                                Kod_Jenis, kodvot;"

            Else query = "SELECT * FROM 
                            (
                                SELECT 
                                    A.Kod_Vot AS kodvot,
                                    (SELECT Butiran FROM SMKB_Vot WHERE Kod_Vot = A.Kod_Vot) as ButiranDetail,
                                    A.Kod_Vot AS kodvotH,
                                    (SELECT Butiran FROM SMKB_Vot WHERE Kod_Vot = A.Kod_Vot) as ButiranHeader,
                                    B.Kod_Jenis, 
                                    REPLACE(
                                        SUM(
                                            CASE 
                                                WHEN @bulan = 1 THEN (A.Dr_1)
                                                WHEN @bulan = 2 THEN (A.Dr_1 + A.Dr_2)
                                                WHEN @bulan = 3 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3)
                                                WHEN @bulan = 4 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4)
                                                WHEN @bulan = 5 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5)
                                                WHEN @bulan = 6 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6)
                                                WHEN @bulan = 7 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7)
                                                WHEN @bulan = 8 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8)	
                                                WHEN @bulan = 9 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9)
                                                WHEN @bulan = 10 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10)
                                                WHEN @bulan = 11 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11)
                                                WHEN @bulan = 12 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12)
                                                ELSE 0 
                                            END
                                        ), '-', ''
                                    ) AS amaunTerkumpulDebit,
		                             REPLACE(
                                        SUM(
                                            CASE 
                                                WHEN @bulan = 1 THEN (A.Cr_1)
                                                WHEN @bulan = 2 THEN (A.Cr_1 + A.Cr_2)
                                                WHEN @bulan = 3 THEN (A.Cr_1 + A.Cr_2 + A.Cr_3)
                                                WHEN @bulan = 4 THEN (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4)
                                                WHEN @bulan = 5 THEN (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4  + A.Cr_5)
                                                WHEN @bulan = 6 THEN (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4  + A.Cr_5 + A.Cr_6)
                                                WHEN @bulan = 7 THEN (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4  + A.Cr_5 + A.Cr_6 + A.Cr_7)
                                                WHEN @bulan = 8 THEN (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4  + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8)	
                                                WHEN @bulan = 9 THEN (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4  + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9)
                                                WHEN @bulan = 10 THEN (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4  + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10)
                                                WHEN @bulan = 11 THEN (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4  + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11)
                                                WHEN @bulan = 12 THEN (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4  + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)
                                                ELSE 0 
                                            END
                                        ), '-', ''
                                    ) AS amaunTerkumpulKredit,
                                    REPLACE(
                                        SUM(
                                            CASE 
                                                WHEN @bulan = 1 THEN (A.Dr_1)
                                                WHEN @bulan = 2 THEN (A.Dr_2) 
                                                WHEN @bulan = 3 THEN (A.Dr_3) 
                                                WHEN @bulan = 4 THEN (A.Dr_4) 
                                                WHEN @bulan = 5 THEN (A.Dr_5)
                                                WHEN @bulan = 6 THEN (A.Dr_6)
                                                WHEN @bulan = 7 THEN (A.Dr_7)
                                                WHEN @bulan = 8 THEN (A.Dr_8)
                                                WHEN @bulan = 9 THEN (A.Dr_9)
                                                WHEN @bulan = 10 THEN (A.Dr_10)
                                                WHEN @bulan = 11 THEN (A.Dr_11)
                                                WHEN @bulan = 12 THEN (A.Dr_12)
                                                ELSE 0 
                                            END
                                        ), '-',''
                                    ) AS amaunSemasaDebit,
		                            REPLACE(
                                        SUM(
                                            CASE 
                                                WHEN @bulan = 1 THEN (A.Cr_1)
                                                WHEN @bulan = 2 THEN (A.Cr_2) 
                                                WHEN @bulan = 3 THEN (A.Cr_3) 
                                                WHEN @bulan = 4 THEN (A.Cr_4) 
                                                WHEN @bulan = 5 THEN (A.Cr_5)
                                                WHEN @bulan = 6 THEN (A.Cr_6)
                                                WHEN @bulan = 7 THEN (A.Cr_7)
                                                WHEN @bulan = 8 THEN (A.Cr_8)
                                                WHEN @bulan = 9 THEN (A.Cr_9)
                                                WHEN @bulan = 10 THEN (A.Cr_10)
                                                WHEN @bulan = 11 THEN (A.Cr_11)
                                                WHEN @bulan = 12 THEN (A.Cr_12)
                                                ELSE 0 
                                            END
                                        ), '-',''
                                    ) AS amaunSemasaKredit
                                FROM
                                    SMKB_Lejar_Am A
                                JOIN SMKB_Vot B ON A.Kod_Vot=B.Kod_Vot
                                WHERE
                                    A.Tahun = @tahun 
                                    --AND B.Kod_Jenis IN ('A','L','C')
                                    " + optionalWhere + "
                                   --And A.Kod_Kump_Wang >= @kodkw
                                   --And A.Kod_Kump_Wang <= @kodkw
                                    And A.Kod_PTJ >= @ptj
                                    And A.Kod_PTJ <=@ptj
                                GROUP BY
                                    A.Kod_Vot, B.Kod_Jenis
                            ) mainTbl 
                            WHERE (CAST(amaunTerkumpulDebit AS DECIMAL) <> 0.00 OR CAST(amaunTerkumpulKredit AS DECIMAL)<> 0.00 OR CAST(amaunSemasaDebit AS DECIMAL) <> 0.00 OR CAST(amaunSemasaKredit AS DECIMAL) <> 0.00)
                            ORDER BY
                                Kod_Jenis, kodvot;"
            End If

            cmd.Connection = sqlconn
            cmd.CommandText = query

            cmd.Parameters.Add(New SqlParameter("@tahun", tahun))
            cmd.Parameters.Add(New SqlParameter("@bulan", bulan))
            'cmd.Parameters.Add(New SqlParameter("@syarikat", syarikat))
            cmd.Parameters.Add(New SqlParameter("@ptj", ptj))
            cmd.Parameters.Add(New SqlParameter("@kodkw", kodkw))

            dt.Load(cmd.ExecuteReader())
            Return dt
        End Using
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadOrderRecord_TransaksiLejarAm(tahun As String, syarikat As String, ptj As String, kodjenis As String, kodkw As String) As String
        Dim resp As New ResponseRepository
        If tahun = "" Or syarikat = "" Or ptj = "" Then
            Return JsonConvert.SerializeObject(New DataTable)
        End If
        Session("tahun") = tahun
        Session("syarikat") = syarikat
        Session("ptj") = ptj
        dt = GetOrder_TransaksiLejarAm(tahun, syarikat, ptj, kodjenis, kodkw)

        Dim totalRecords As Integer = dt.Rows.Count

        Return JsonConvert.SerializeObject(dt)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetOrder_TransaksiLejarAm(tahun As String, syarikat As String, ptj As String, kodjenis As String, kodkw As String) As DataTable
        Dim db = New DBKewConn
        Dim dt As New DataTable

        Dim connectionString As String = "server=devmis12.utem.edu.my;database=DbKewanganV4;uid=Smkb;pwd=Smkb@Dev2012;"

        Using sqlconn As New SqlConnection(connectionString)
            Dim cmd As New SqlCommand
            Dim jenisQuery As String = ""
            sqlconn.Open()

            Dim query As String


            If kodjenis <> "#" Then
                jenisQuery = " AND B.Kod_Jenis = @kodjenis "
            End If

            If ptj = "00" Then

                query = "Select * from SMKB_Lejar_Am
                         where Tahun = @tahun
                         and Kod_Kump_Wang = @kodkw
                         Order by Kod_Vot,Kod_Kump_Wang"

                query = "Select A.*, B.Kod_Jenis, B.Kod_Vot
                        from SMKB_Lejar_Am A
                        INNER JOIN SMKB_Vot B 
	                        ON A.Kod_Vot = B.Kod_Vot
                        where Tahun = @tahun " & jenisQuery & "
                        and Kod_Kump_Wang = @kodkw 
                        Order by A.Kod_Vot,Kod_Kump_Wang"
            Else

                query = "Select * from SMKB_Lejar_Am
                         where Tahun = @tahun
                         and Kod_Kump_Wang = @kodkw 
                         And Kod_PTJ >= @ptj and Kod_PTJ <= @ptj
                         Order by Kod_Vot,Kod_Kump_Wang"

                query = "Select A.*, B.Kod_Jenis, B.Kod_Vot
                        from SMKB_Lejar_Am A
                        INNER JOIN SMKB_Vot B 
	                        ON A.Kod_Vot = B.Kod_Vot
                        where Tahun = @tahun " & jenisQuery & "
                        And Kod_PTJ >= @ptj and Kod_PTJ <= @ptj
                        and Kod_Kump_Wang = @kodkw 
                        Order by A.Kod_Vot,Kod_Kump_Wang"
            End If

            cmd.Connection = sqlconn
            cmd.CommandText = query

            cmd.Parameters.Add(New SqlParameter("@tahun", tahun))
            cmd.Parameters.Add(New SqlParameter("@ptj", ptj))
            cmd.Parameters.Add(New SqlParameter("@kodjenis", kodjenis))
            cmd.Parameters.Add(New SqlParameter("@kodkw", kodkw))
            dt.Load(cmd.ExecuteReader())
            Return dt
        End Using
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadVotDetails(KodVot As String, KodPtj As String, Tahun As String, KodKW As String, KodOperasi As String, KodProjek As String) As String
        Dim resp As New ResponseRepository

        If KodVot = "" Then
            Return JsonConvert.SerializeObject(New DataTable)
        End If

        Dim connectionString As String = "server=devmis12.utem.edu.my;database=DbKewanganV4;uid=Smkb;pwd=Smkb@Dev2012;"

        Using sqlconn As New SqlConnection(connectionString)
            Dim cmd As New SqlCommand
            sqlconn.Open()
            dt = New DataTable

            Dim query As String
            query = "
            SELECT CONVERT(INT, Kod_Detail) AS bulan, Butiran AS namaBulan, Debit, Kredit
            FROM SMKB_Lookup_Detail A            OUTER APPLY (	            select CASE 
		            WHEN Butiran = 'January' THEN Dr_1 
		            WHEN Butiran = 'February' THEN Dr_2 
		            WHEN Butiran = 'March' THEN Dr_3
		            WHEN Butiran = 'April' THEN Dr_4 
		            WHEN Butiran = 'May' THEN Dr_5
		            WHEN Butiran = 'June' THEN Dr_6 
		            WHEN Butiran = 'July' THEN Dr_7
		            WHEN Butiran = 'August' THEN Dr_8 
		            WHEN Butiran = 'September' THEN Dr_9 
		            WHEN Butiran = 'October' THEN Dr_10
		            WHEN Butiran = 'November' THEN Dr_11
		            WHEN Butiran = 'December' THEN Dr_12
		            ELSE 0.00 
	            END as Debit,
	            CASE 
		            WHEN Butiran = 'January' THEN Cr_1 
		            WHEN Butiran = 'February' THEN Cr_2 
		            WHEN Butiran = 'March' THEN Cr_3
		            WHEN Butiran = 'April' THEN Cr_4 
		            WHEN Butiran = 'May' THEN Cr_5
		            WHEN Butiran = 'June' THEN Cr_6 
		            WHEN Butiran = 'July' THEN Cr_7
		            WHEN Butiran = 'August' THEN Cr_8 
		            WHEN Butiran = 'September' THEN Cr_9 
		            WHEN Butiran = 'October' THEN Cr_10
		            WHEN Butiran = 'November' THEN Cr_11
		            WHEN Butiran = 'December' THEN Cr_12
		            ELSE 0.00 
	            END as Kredit
	            from SMKB_Lejar_Am
	            where Kod_Vot = @KodVot
	            and Tahun = @Tahun
	            and Kod_Kump_Wang = @KodKW
	            and Kod_Operasi = @KodOperasi
	            and Kod_PTJ = @KodPtj
                and Kod_Vot = @KodVot 
	            and Kod_Projek = @KodProjek            ) B            WHERE kod = '0147'            ORDER BY bulan"
            cmd.Connection = sqlconn
            cmd.CommandText = query

            cmd.Parameters.Add(New SqlParameter("@KodVot", KodVot))
            cmd.Parameters.Add(New SqlParameter("@KodPtj", KodPtj))
            cmd.Parameters.Add(New SqlParameter("@Tahun", Tahun))
            cmd.Parameters.Add(New SqlParameter("@KodKW", KodKW))
            cmd.Parameters.Add(New SqlParameter("@KodOperasi", KodOperasi))
            cmd.Parameters.Add(New SqlParameter("@KodProjek", KodProjek))

            dt.Load(cmd.ExecuteReader())
        End Using

        Return JsonConvert.SerializeObject(dt)
    End Function
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadOrderRecord_PNLDetail(bulan As String, tahun As String, syarikat As String, ptj As String, kodvot As String, level As String) As String
        Dim resp As New ResponseRepository
        If bulan = "" Or tahun = "" Or syarikat = "" Or ptj = "" Then
            Return JsonConvert.SerializeObject(New DataTable)
        End If
        Session("bulan") = bulan
        Session("tahun") = tahun
        Session("syarikat") = syarikat
        Session("ptj") = ptj


        If level = "1" Then
            dt = GetOrder_LV1(bulan, tahun, syarikat, ptj, kodvot)
        ElseIf level = "2" Then
            dt = GetOrder_LV2(bulan, tahun, syarikat, ptj, kodvot)
        ElseIf level = "3" Then
            dt = GetOrder_LV3(bulan, tahun, syarikat, ptj, kodvot)
        End If
        'resp.SuccessPayload(dt)
        Dim totalRecords As Integer = dt.Rows.Count
        'New With {
        '        .draw = 1,
        '        .recordsTotal = totalRecords,
        '        .recordsFiltered = 0,
        '        .data = dt
        '    }
        Return JsonConvert.SerializeObject(dt)
    End Function

    Private Function GetOrder_LV1(bulan As String, tahun As String, syarikat As String, ptj As String, Optional kod As String = "") As DataTable
        Dim db = New DBKewConn
        Dim dt As New DataTable
        Dim where As String = ""
        'Dim connectionString As String = "server=v-sql12.utem.edu.my;database=DbKewangan;uid=smkb;pwd=smkb*pwd;"
        Dim connectionString As String = "server=devmis12.utem.edu.my;database=DbKewanganV4;uid=smkb;pwd=Smkb@Dev2012;"



        Using sqlconn As New SqlConnection(connectionString)
            Dim cmd As New SqlCommand
            sqlconn.Open()
            Dim secondWhere As String = ""

            Dim query As String

            If ptj = "00" Then
                secondWhere = " And A.Kod_PTJ >= '010000'
                And A.Kod_PTJ <='Y00005' "
            Else
                secondWhere = "AND     And A.Kod_PTJ >= @ptj
                And A.Kod_PTJ <= @ptj "
            End If

            query = "
                 
                SELECT *, Kod_Jenis as KodJen, CASE WHEN RIGHT(kodvot,3)=000 THEN 'H2' ELSE 'D' END AS LEVEL FROM 
                (
                SELECT DetailKod AS kodvot,
	                B.Butiran as ButiranDetail,
	                Lv2Kod AS kodvotH,
	                (select Butiran from SMKB_Vot where Kod_Vot = Lv2Kod) as ButiranHeader,
	                B.Kod_Jenis, 
	                REPLACE(SUM(
		                CASE 
		               WHEN @bulan = 1 AND A.Kod_Vot <> DetailKod THEN (A.Dr_1 - A.Cr_1)
		               WHEN @bulan = 2 AND A.Kod_Vot <> DetailKod THEN (A.Dr_1 + A.Dr_2) - (A.Cr_1 + A.Cr_2)
		               WHEN @bulan = 3 AND A.Kod_Vot <> DetailKod THEN (A.Dr_1 + A.Dr_2 + A.Dr_3) - (A.Cr_1 + A.Cr_2 + A.Cr_3)
		               WHEN @bulan = 4 AND A.Kod_Vot <> DetailKod THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4) - (A.Cr_1 + A.Cr_2 + A.Cr_2 + A.Cr_4)
		               WHEN @bulan = 5 AND A.Kod_Vot <> DetailKod THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5)
		               WHEN @bulan = 6 AND A.Kod_Vot <> DetailKod THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6)
		               WHEN @bulan = 7 AND A.Kod_Vot <> DetailKod THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7)
		               WHEN @bulan = 8 AND A.Kod_Vot <> DetailKod THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8)	
		               WHEN @bulan = 9 AND A.Kod_Vot <> DetailKod THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9)
		               WHEN @bulan = 10 AND A.Kod_Vot <> DetailKod THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10)
		               WHEN @bulan = 11 AND A.Kod_Vot <> DetailKod THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11)
		               WHEN @bulan = 12 AND A.Kod_Vot <> DetailKod THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)
		                ELSE 0 
		                END
	                ), '-', '') AS amaun,
	                REPLACE(SUM(CASE 
	               WHEN @bulan = 1 AND A.Kod_Vot <> DetailKod THEN (A.Dr_1 - A.Cr_1)
	               WHEN @bulan = 2 AND A.Kod_Vot <> DetailKod THEN (A.Dr_2 - A.Cr_2) 
	               WHEN @bulan = 3 AND A.Kod_Vot <> DetailKod THEN (A.Dr_3 - A.Cr_3) 
                   WHEN @bulan = 4 AND A.Kod_Vot <> DetailKod THEN (A.Dr_4 - A.Cr_4) 
	               WHEN @bulan = 5 AND A.Kod_Vot <> DetailKod THEN (A.Dr_5 - A.Cr_5)
                   WHEN @bulan = 6 AND A.Kod_Vot <> DetailKod THEN (A.Dr_6 - A.Cr_6)
	               WHEN @bulan = 7 AND A.Kod_Vot <> DetailKod THEN (A.Dr_7 - A.Cr_7)
	               WHEN @bulan = 8 AND A.Kod_Vot <> DetailKod THEN (A.Dr_8 - A.Cr_8)
	               WHEN @bulan = 9 AND A.Kod_Vot <> DetailKod THEN (A.Dr_9 - A.Cr_9)
	               WHEN @bulan = 10 AND A.Kod_Vot <> DetailKod THEN (A.Dr_10 - A.Cr_10)
	               WHEN @bulan = 11 AND A.Kod_Vot <> DetailKod THEN (A.Dr_11 - A.Cr_11)
                   WHEN @bulan = 12 AND A.Kod_Vot <> DetailKod THEN (A.Dr_12 - A.Cr_12)
	                ELSE 0 
	                END), '-','') AS amaun2, Kod_Klasifikasi 
                FROM
                SMKB_Lejar_Am A
				CROSS APPLY (
					SELECT substring(A.Kod_Vot, 1, 2) + '000' as DetailKod,
					substring(A.Kod_Vot, 1, 1) + '0000' as Lv2Kod
				) C
                JOIN SMKB_Vot B ON C.DetailKod=B.Kod_Vot
                WHERE
                A.Tahun = @tahun 
                And B.Kod_Jenis In ('I','E')
                And A.Kod_Kump_Wang >= '01'
                And A.Kod_Kump_Wang <= '11'
                " + secondWhere + "
                GROUP BY
                B.Kod_Jenis,
				Butiran,
                DetailKod,
                Lv2Kod, B.Kod_Klasifikasi
                )mainTbl 
				WHERE (CAST(amaun as decimal)  <> 0.00 or CAST(amaun2 as decimal) <> 0.00)  
				and  (CASE WHEN RIGHT(kodvot,3)=000 THEN 'H2' ELSE 'D' END) <> 'D'  
                ORDER BY
                Kod_Jenis DESC, kodvot"

            cmd.Connection = sqlconn
            cmd.CommandText = query

            cmd.Parameters.Add(New SqlParameter("@tahun", tahun))
            cmd.Parameters.Add(New SqlParameter("@bulan", bulan))
            'cmd.Parameters.Add(New SqlParameter("@syarikat", syarikat))
            cmd.Parameters.Add(New SqlParameter("@ptj", ptj))

            If kod <> "" Then
                cmd.Parameters.Add(New SqlParameter("@kodvot", Left(kod, 2)))
            End If
            dt.Load(cmd.ExecuteReader())
            Return dt
        End Using
    End Function

    Private Function GetOrder_LV2(bulan As String, tahun As String, syarikat As String, ptj As String, Optional kod As String = "") As DataTable
        Dim db = New DBKewConn
        Dim dt As New DataTable
        Dim where As String = ""
        'Dim connectionString As String = "server=v-sql12.utem.edu.my;database=DbKewangan;uid=smkb;pwd=smkb*pwd;"
        Dim connectionString As String = "server=devmis12.utem.edu.my;database=DbKewanganV4;uid=smkb;pwd=Smkb@Dev2012;"

        Using sqlconn As New SqlConnection(connectionString)
            Dim cmd As New SqlCommand
            sqlconn.Open()
            Dim secondWhere As String = ""

            Dim query As String

            If ptj = "00" Then
                secondWhere = " And A.Kod_PTJ >= '010000'
                And A.Kod_PTJ <='Y00005' "
            Else
                secondWhere = "AND     And A.Kod_PTJ >= @ptj
                And A.Kod_PTJ <= @ptj "
            End If

            query = "
                SELECT *, Kod_Klasifikasi as KodJen, CASE WHEN RIGHT(kodvot,3)=000 THEN 'H2' ELSE 'D' END AS LEVEL FROM 
                (
                SELECT DetailKod AS kodvot,
	                B.Butiran as ButiranDetail,
	                Lv2Kod AS kodvotH,
	                (select Butiran from SMKB_Vot where Kod_Vot = Lv2Kod) as ButiranHeader,
	                B.Kod_Klasifikasi, 
	                REPLACE(SUM(
		                CASE 
		                WHEN @bulan = 1 THEN (A.Dr_1 - A.Cr_1)
		                WHEN @bulan = 2 THEN (A.Dr_1 + A.Dr_2) - (A.Cr_1 + A.Cr_2)
		                WHEN @bulan = 3 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3) - (A.Cr_1 + A.Cr_2 + A.Cr_3)
		                WHEN @bulan = 4 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4) - (A.Cr_1 + A.Cr_2 + A.Cr_2 + A.Cr_4)
		                WHEN @bulan = 5 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5)
		                WHEN @bulan = 6 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6)
		                WHEN @bulan = 7 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7)
		                WHEN @bulan = 8 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8)	
		                WHEN @bulan = 9 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9)
		                WHEN @bulan = 10 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10)
		                WHEN @bulan = 11 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11)
		                WHEN @bulan = 12 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)
		                ELSE 0 
		                END	
	                ), '-', '') AS amaun,
	                REPLACE(SUM(CASE 
	                WHEN @bulan = 1 THEN (A.Dr_1 - A.Cr_1)
	                WHEN @bulan = 2 THEN (A.Dr_2 - A.Cr_2) 
	                WHEN @bulan = 3 THEN (A.Dr_3 - A.Cr_3) 
                    WHEN @bulan = 4 THEN (A.Dr_4 - A.Cr_4) 
	                WHEN @bulan = 5 THEN (A.Dr_5 - A.Cr_5)
                    WHEN @bulan = 6 THEN (A.Dr_6 - A.Cr_6)
	                WHEN @bulan = 7 THEN (A.Dr_7 - A.Cr_7)
	                WHEN @bulan = 8 THEN (A.Dr_8 - A.Cr_8)
	                WHEN @bulan = 9 THEN (A.Dr_9 - A.Cr_9)
	                WHEN @bulan = 10 THEN (A.Dr_10 - A.Cr_10)
	                WHEN @bulan = 11 THEN (A.Dr_11 - A.Cr_11)
                    WHEN @bulan = 12 THEN (A.Dr_12 - A.Cr_12)

	                ELSE 0 
	                END), '-','') AS amaun2
                FROM
                SMKB_Lejar_Am A
				CROSS APPLY (
					SELECT substring(A.Kod_Vot, 1, 3) + '00' as DetailKod,
					substring(A.Kod_Vot, 1, 2) + '000' as Lv2Kod
				) C
                JOIN SMKB_Vot B ON B.Kod_Vot= C.DetailKod
                WHERE
                A.Tahun = @tahun
                And B.Kod_Jenis In ('I','E')
                And A.Kod_Kump_Wang >= '01'
                And A.Kod_Kump_Wang <= '11'
                " + secondWhere + "
                GROUP BY
                B.Kod_Klasifikasi,
				B.Butiran,
                DetailKod,
                Lv2Kod
                )mainTbl 
				WHERE (CAST(amaun as decimal)  <> 0.00 or CAST(amaun2 as decimal) <> 0.00)  
				and (CASE WHEN RIGHT(kodvot,3)=000 THEN 'H2' ELSE 'D' END) = 'D' AND LEFT(kodvot,2) = LEFT(@kodvot, 2)
                ORDER BY
                Kod_Klasifikasi DESC, kodvot"

            cmd.Connection = sqlconn
            cmd.CommandText = query

            cmd.Parameters.Add(New SqlParameter("@tahun", tahun))
            cmd.Parameters.Add(New SqlParameter("@bulan", bulan))
            'cmd.Parameters.Add(New SqlParameter("@syarikat", syarikat))
            cmd.Parameters.Add(New SqlParameter("@ptj", ptj))

            If kod <> "" Then
                cmd.Parameters.Add(New SqlParameter("@kodvot", kod))
            End If
            dt.Load(cmd.ExecuteReader())
            Return dt
        End Using
    End Function

    Private Function GetOrder_LV3(bulan As String, tahun As String, syarikat As String, ptj As String, Optional kod As String = "") As DataTable
        Dim db = New DBKewConn
        Dim dt As New DataTable
        Dim where As String = ""
        'Dim connectionString As String = "server=v-sql12.utem.edu.my;database=DbKewangan;uid=smkb;pwd=smkb*pwd;"
        Dim connectionString As String = "server=devmis12.utem.edu.my;database=DbKewanganV4;uid=smkb;pwd=Smkb@Dev2012;"

        Using sqlconn As New SqlConnection(connectionString)
            Dim cmd As New SqlCommand
            sqlconn.Open()
            Dim secondWhere As String = ""

            Dim query As String

            If ptj = "00" Then
                secondWhere = " And A.Kod_PTJ >= '010000'
                And A.Kod_PTJ <='Y00005' "
            Else
                secondWhere = "AND     And A.Kod_PTJ >= @ptj
                And A.Kod_PTJ <= @ptj "
            End If

            query = "
                SELECT *, Kod_Jenis as KodJen, CASE WHEN RIGHT(kodvot,3)=000 THEN 'H2' ELSE 'D' END AS LEVEL FROM 
                (
                SELECT DetailKod AS kodvot,
	                (select Butiran from SMKB_Vot where Kod_Vot = DetailKod) as ButiranDetail,
	                Lv2Kod AS kodvotH,
	                (select Butiran from SMKB_Vot where Kod_Vot = Lv2Kod) as ButiranHeader,
	                B.Kod_Jenis, 
	                REPLACE(SUM(
		                CASE 
		                WHEN @bulan = 1 THEN (A.Dr_1 - A.Cr_1)
		                WHEN @bulan = 2 THEN (A.Dr_1 + A.Dr_2) - (A.Cr_1 + A.Cr_2)
		                WHEN @bulan = 3 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3) - (A.Cr_1 + A.Cr_2 + A.Cr_3)
		                WHEN @bulan = 4 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4) - (A.Cr_1 + A.Cr_2 + A.Cr_2 + A.Cr_4)
		                WHEN @bulan = 5 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5)
		                WHEN @bulan = 6 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6)
		                WHEN @bulan = 7 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7)
		                WHEN @bulan = 8 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8)	
		                WHEN @bulan = 9 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9)
		                WHEN @bulan = 10 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10)
		                WHEN @bulan = 11 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11)
		                WHEN @bulan = 12 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)
		                ELSE 0 
		                END
	                ), '-', '') AS amaun,
	                REPLACE(SUM(CASE 
	                WHEN @bulan = 1 THEN (A.Dr_1 - A.Cr_1)
	                WHEN @bulan = 2 THEN (A.Dr_2 - A.Cr_2) 
	                WHEN @bulan = 3 THEN (A.Dr_3 - A.Cr_3) 
                    WHEN @bulan = 4 THEN (A.Dr_4 - A.Cr_4) 
	                WHEN @bulan = 5 THEN (A.Dr_5 - A.Cr_5)
                    WHEN @bulan = 6 THEN (A.Dr_6 - A.Cr_6)
	                WHEN @bulan = 7 THEN (A.Dr_7 - A.Cr_7)
	                WHEN @bulan = 8 THEN (A.Dr_8 - A.Cr_8)
	                WHEN @bulan = 9 THEN (A.Dr_9 - A.Cr_9)
	                WHEN @bulan = 10 THEN (A.Dr_10 - A.Cr_10)
	                WHEN @bulan = 11 THEN (A.Dr_11 - A.Cr_11)
                    WHEN @bulan = 12 THEN (A.Dr_12 - A.Cr_12)
	                ELSE 0 
	                END), '-','') AS amaun2
                FROM
                SMKB_Lejar_Am A
                JOIN SMKB_Vot B ON A.Kod_Vot=B.Kod_Vot
				CROSS APPLY (
					SELECT a.Kod_Vot as DetailKod,
					substring(A.Kod_Vot, 1, 3) + '00' as Lv2Kod
				) C
                WHERE
                A.Tahun = @tahun 
                And B.Kod_Jenis In ('I','E')
                And A.Kod_Kump_Wang >= '01'
                And A.Kod_Kump_Wang <= '11'
                " + secondWhere + " 
				AND LEFT(A.Kod_Vot,3) = LEFT(@kodvot, 3)
                GROUP BY
                B.Kod_Jenis,
                DetailKod,
                Lv2Kod
                )mainTbl 
				WHERE (CAST(amaun as decimal)  <> 0.00 or CAST(amaun2 as decimal) <> 0.00)  
                ORDER BY
                Kod_Jenis DESC, kodvot"

            cmd.Connection = sqlconn
            cmd.CommandText = query

            cmd.Parameters.Add(New SqlParameter("@tahun", tahun))
            cmd.Parameters.Add(New SqlParameter("@bulan", bulan))
            'cmd.Parameters.Add(New SqlParameter("@syarikat", syarikat))
            cmd.Parameters.Add(New SqlParameter("@ptj", ptj))

            If kod <> "" Then
                cmd.Parameters.Add(New SqlParameter("@kodvot", kod))
            End If
            dt.Load(cmd.ExecuteReader())
            Return dt
        End Using
    End Function

    Private Function GetOrder_PNLDetail(bulan As String, tahun As String, syarikat As String, ptj As String, Optional kod As String = "") As DataTable
        Dim db = New DBKewConn
        Dim dt As New DataTable
        Dim where As String = ""
        'Dim connectionString As String = "server=v-sql12.utem.edu.my;database=DbKewangan;uid=smkb;pwd=smkb*pwd;"
        Dim connectionString As String = "server=devmis12.utem.edu.my;database=DbKewanganV4;uid=smkb;pwd=Smkb@Dev2012;"



        Using sqlconn As New SqlConnection(connectionString)
            Dim cmd As New SqlCommand
            sqlconn.Open()

            If kod = "" Then
                where = " and  (CASE WHEN RIGHT(kodvot,3)=000 THEN 'H2' ELSE 'D' END) <> 'D' "
            Else
                where = " and (CASE WHEN RIGHT(kodvot,3)=000 THEN 'H2' ELSE 'D' END) = 'D' AND LEFT(kodvot,2) = LEFT(@kodvot, 2) "
            End If

            Dim query As String

            If ptj = "00" Then

                query = "SELECT *,CASE WHEN RIGHT(kodvot,3)=000 THEN 'H2' ELSE 'D' END AS LEVEL FROM (
                         SELECT
                                substring(mk06_transaksi.kodvot, 1, 2) + '000' AS kodvot,
                                                          (select Butiran from MK_Vot where KodVot = substring(mk06_transaksi.kodvot, 1, 2) + '000') as ButiranDetail,
                                                          --substring(mk06_transaksi.kodvot, 1, 1) + '0000' AS kodvotH,
                                                          --(select Butiran from MK_Vot where KodVot = substring(mk06_transaksi.kodvot, 1, 1) + '0000') as ButiranHeader,

                                                          mk06_transaksi.kodjen,
                                                          replace(sum(CASE WHEN Month(mk06_transaksi.mk06_tkhtran) <= @bulan THEN isnull(mk06_transaksi.mk06_debit, 0) - isnull(mk06_transaksi.mk06_kredit, 0) ELSE 0 END),'-','') AS amaun,
                                                          replace(sum(CASE WHEN Month(mk06_transaksi.mk06_tkhtran) = @bulan THEN isnull(mk06_transaksi.mk06_debit, 0) - isnull(mk06_transaksi.mk06_kredit, 0) ELSE 0 END),'-','') AS amaun2
                                                        FROM
                                                          mk06_transaksi
                                WHERE
                                year(mk06_transaksi.mk06_tkhtran) = @tahun
                                                          And mk06_transaksi.kodjen In ('I','E')
                                                          And mk06_transaksi.kodkw >= '01'
                                                          And mk06_transaksi.kodkw <= '11'
                                                          And mk06_transaksi.kodptj >= '010000'
                                                          And mk06_transaksi.kodptj <= 'Y00005'
                                                          And mk06_transaksi.koddok Not IN ('LO', 'ADJ_LO', 'CF_LO', 'BJTKURANG', 'BJTTAMBAH', 'VIRKELUAR', 'VIRMASUK')
                                                          And mk06_transaksi.mk06_status IN (0, 1)
                                                        GROUP BY
                                                          mk06_transaksi.kodjen,
                                                          substring(mk06_transaksi.kodvot, 1, 2) + '000',
                                                          substring(mk06_transaksi.kodvot, 1, 1) + '0000'

                    --
				                    UNION ALL
				                    SELECT
					                    mk06_transaksi.kodvot,
					                    MK_Vot.Butiran AS ButiranDetail,
					                    --MK_Vot.klasifikasi,
					                    mk06_transaksi.kodjen,
					                    REPLACE(SUM(CASE WHEN MONTH(mk06_transaksi.mk06_tkhtran) <= @bulan THEN ISNULL(mk06_transaksi.mk06_debit, 0) - ISNULL(mk06_transaksi.mk06_kredit, 0) ELSE 0 END),'-','') AS amaun,
					                    REPLACE(SUM(CASE WHEN MONTH(mk06_transaksi.mk06_tkhtran) = @bulan THEN ISNULL(mk06_transaksi.mk06_debit, 0) - ISNULL(mk06_transaksi.mk06_kredit, 0) ELSE 0 END),'-','') AS amaun2
				                    FROM
					                    mk06_transaksi
				                    LEFT JOIN
					                    MK_Vot ON MK_Vot.KodVot = mk06_transaksi.kodvot
				                    WHERE
					                    YEAR(mk06_transaksi.mk06_tkhtran) = @tahun
					                    AND mk06_transaksi.kodjen IN ('I','E')
					                    AND mk06_transaksi.kodkw >= '01'
					                    AND mk06_transaksi.kodkw <= '11'
					                    AND mk06_transaksi.kodptj >= '010000'
					                    AND mk06_transaksi.kodptj <= 'Y00005'
					                    AND mk06_transaksi.koddok NOT IN ('LO', 'ADJ_LO', 'CF_LO', 'BJTKURANG', 'BJTTAMBAH', 'VIRKELUAR', 'VIRMASUK')
					                    AND mk06_transaksi.mk06_status IN (0, 1)
    
				                    GROUP BY
					                    mk06_transaksi.kodjen,
					                    mk06_transaksi.kodvot,
					                    MK_Vot.Butiran,
					                    MK_Vot.klasifikasi
				                    --ORDER BY
				                    --	mk06_transaksi.kodjen DESC,
				                    --	mk06_transaksi.kodvot
                                    
                                ) mainTbl 
			                    WHERE (cast(amaun as decimal(16,2)) <> 0.00 or cast(amaun2 as decimal(16,2)) <> 0.00) " + where + "
                                    ORDER BY kodvot,CASE WHEN RIGHT(kodvot,3)=000 THEN 'H2' ELSE 'D' END DESC
                                "

                query = "

                SELECT *, Kod_Jenis as KodJen, CASE WHEN RIGHT(kodvot,3)=000 THEN 'H2' ELSE 'D' END AS LEVEL FROM 
                (
                SELECT substring(A.Kod_Vot, 1, 2) + '000' AS kodvot,
	                (select Butiran from SMKB_Vot where Kod_Vot = substring(A.Kod_Vot, 1, 2) + '000') as ButiranDetail,
	                substring(A.Kod_Vot, 1, 1) + '0000' AS kodvotH,
	                (select Butiran from SMKB_Vot where Kod_Vot = substring(A.Kod_Vot, 1, 1) + '0000') as ButiranHeader,
	                B.Kod_Jenis, 
	                REPLACE(SUM(
		                CASE 
		                WHEN @bulan = 1 THEN (A.Dr_1 - A.Cr_1)
		                WHEN @bulan = 2 THEN (A.Dr_1 + A.Dr_2) - (A.Cr_1 + A.Cr_2)
		                WHEN @bulan = 3 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3) - (A.Cr_1 + A.Cr_2 + A.Cr_3)
		                WHEN @bulan = 4 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4) - (A.Cr_1 + A.Cr_2 + A.Cr_2 + A.Cr_4)
		                WHEN @bulan = 5 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5)
		                WHEN @bulan = 6 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6)
		                WHEN @bulan = 7 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7)
		                WHEN @bulan = 8 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8)	
		                WHEN @bulan = 9 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9)
		                WHEN @bulan = 10 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10)
		                WHEN @bulan = 11 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11)
		                WHEN @bulan = 12 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)
		                ELSE 0 
		                END
	                ), '-', '') AS amaun,
	                REPLACE(SUM(CASE 
	                WHEN @bulan = 1 THEN (A.Dr_1 - A.Cr_1)
	                WHEN @bulan = 2 THEN (A.Dr_2 - A.Cr_2) 
	                WHEN @bulan = 3 THEN (A.Dr_3 - A.Cr_3) 
                    WHEN @bulan = 4 THEN (A.Dr_4 - A.Cr_4) 
	                WHEN @bulan = 5 THEN (A.Dr_5 - A.Cr_5)
                    WHEN @bulan = 6 THEN (A.Dr_6 - A.Cr_6)
	                WHEN @bulan = 7 THEN (A.Dr_7 - A.Cr_7)
	                WHEN @bulan = 8 THEN (A.Dr_8 - A.Cr_8)
	                WHEN @bulan = 9 THEN (A.Dr_9 - A.Cr_9)
	                WHEN @bulan = 10 THEN (A.Dr_10 - A.Cr_10)
	                WHEN @bulan = 11 THEN (A.Dr_11 - A.Cr_11)
                    WHEN @bulan = 12 THEN (A.Dr_12 - A.Cr_12)

	                ELSE 0 
	                END), '-','') AS amaun2
                FROM
                SMKB_Lejar_Am A
                JOIN SMKB_Vot B ON A.Kod_Vot=B.Kod_Vot
                WHERE
                A.Tahun = @tahun 
                And B.Kod_Jenis In ('I','E')
                And A.Kod_Kump_Wang >= '01'
                And A.Kod_Kump_Wang <= '11'
                And A.Kod_PTJ >= '010000'
                And A.Kod_PTJ <='Y00005'
                --And A.Kod_PTJ >= @ptj
                --And A.Kod_PTJ <= @ptj
                --And A.Kod_Vot >= @kodVotFrom AND A.Kod_Vot <= @kodVotTo
                --AND Kod_Syarikat = 'UTeM'
                GROUP BY
                B.Kod_Jenis,
                substring(A.Kod_Vot, 1, 2) + '000',
                substring(A.Kod_Vot, 1, 1) + '0000'
                )mainTbl WHERE (CAST(amaun as decimal)  <> 0.00 or CAST(amaun2 as decimal) <> 0.00) " + where + "
                ORDER BY
                Kod_Jenis DESC, kodvot"

            Else
                query = "SELECT *,CASE WHEN RIGHT(kodvot,3)=000 THEN 'H2' ELSE 'D' END AS LEVEL FROM (
                         SELECT
                            substring(mk06_transaksi.kodvot, 1, 2) + '000' AS kodvot,
                                                      (select Butiran from MK_Vot where KodVot = substring(mk06_transaksi.kodvot, 1, 2) + '000') as ButiranDetail,
                                                      --substring(mk06_transaksi.kodvot, 1, 1) + '0000' AS kodvotH,
                                                      --(select Butiran from MK_Vot where KodVot = substring(mk06_transaksi.kodvot, 1, 1) + '0000') as ButiranHeader,

                                                      mk06_transaksi.kodjen,
                                                      replace(sum(CASE WHEN Month(mk06_transaksi.mk06_tkhtran) <= @bulan THEN isnull(mk06_transaksi.mk06_debit, 0) - isnull(mk06_transaksi.mk06_kredit, 0) ELSE 0 END),'-','') AS amaun,
                                                      replace(sum(CASE WHEN Month(mk06_transaksi.mk06_tkhtran) = @bulan THEN isnull(mk06_transaksi.mk06_debit, 0) - isnull(mk06_transaksi.mk06_kredit, 0) ELSE 0 END),'-','') AS amaun2
                                                    FROM
                                                      mk06_transaksi
                            WHERE
                            year(mk06_transaksi.mk06_tkhtran) = @tahun
                                                      And mk06_transaksi.kodjen In ('I','E')
                                                      And mk06_transaksi.kodkw >= '01'
                                                      And mk06_transaksi.kodkw <= '11'
                                                      And mk06_transaksi.kodptj >= '010000'
                                                      And mk06_transaksi.kodptj <= 'Y00005'
                                                      And mk06_transaksi.koddok Not IN ('LO', 'ADJ_LO', 'CF_LO', 'BJTKURANG', 'BJTTAMBAH', 'VIRKELUAR', 'VIRMASUK')
                                                      And mk06_transaksi.mk06_status IN (0, 1)
                                                    GROUP BY
                                                      mk06_transaksi.kodjen,
                                                      substring(mk06_transaksi.kodvot, 1, 2) + '000',
                                                      substring(mk06_transaksi.kodvot, 1, 1) + '0000'

                --
				                UNION ALL
				                SELECT
					                mk06_transaksi.kodvot,
					                MK_Vot.Butiran AS ButiranDetail,
					                --MK_Vot.klasifikasi,
					                mk06_transaksi.kodjen,
					                REPLACE(SUM(CASE WHEN MONTH(mk06_transaksi.mk06_tkhtran) <= @bulan THEN ISNULL(mk06_transaksi.mk06_debit, 0) - ISNULL(mk06_transaksi.mk06_kredit, 0) ELSE 0 END),'-','') AS amaun,
					                REPLACE(SUM(CASE WHEN MONTH(mk06_transaksi.mk06_tkhtran) = @bulan THEN ISNULL(mk06_transaksi.mk06_debit, 0) - ISNULL(mk06_transaksi.mk06_kredit, 0) ELSE 0 END),'-','') AS amaun2
				                FROM
					                mk06_transaksi
				                LEFT JOIN
					                MK_Vot ON MK_Vot.KodVot = mk06_transaksi.kodvot
				                WHERE
					                YEAR(mk06_transaksi.mk06_tkhtran) = @tahun
					                AND mk06_transaksi.kodjen IN ('I','E')
					                AND mk06_transaksi.kodkw >= '01'
					                AND mk06_transaksi.kodkw <= '11'
					                AND mk06_transaksi.kodptj >= @ptj
					                AND mk06_transaksi.kodptj <= @ptj
					                AND mk06_transaksi.koddok NOT IN ('LO', 'ADJ_LO', 'CF_LO', 'BJTKURANG', 'BJTTAMBAH', 'VIRKELUAR', 'VIRMASUK')
					                AND mk06_transaksi.mk06_status IN (0, 1)
    
				                GROUP BY
					                mk06_transaksi.kodjen,
					                mk06_transaksi.kodvot,
					                MK_Vot.Butiran,
					                MK_Vot.klasifikasi
				                --ORDER BY
				                --	mk06_transaksi.kodjen DESC,
				                --	mk06_transaksi.kodvot
                                    
                            ) mainTbl 
			                WHERE (cast(amaun as decimal(16,2)) <> 0.00 or cast(amaun2 as decimal(16,2)) <> 0.00)  " + where + "
                                ORDER BY kodvot,CASE WHEN RIGHT(kodvot,3)=000 THEN 'H2' ELSE 'D' END DESC
                            "

                query = "

                SELECT *, Kod_Jenis as KodJen, CASE WHEN RIGHT(kodvot,3)=000 THEN 'H2' ELSE 'D' END AS LEVEL FROM 
                (
                SELECT substring(A.Kod_Vot, 1, 2) + '000' AS kodvot,
	                (select Butiran from SMKB_Vot where Kod_Vot = substring(A.Kod_Vot, 1, 2) + '000') as ButiranDetail,
	                substring(A.Kod_Vot, 1, 1) + '0000' AS kodvotH,
	                (select Butiran from SMKB_Vot where Kod_Vot = substring(A.Kod_Vot, 1, 1) + '0000') as ButiranHeader,
	                B.Kod_Jenis, 
	                REPLACE(SUM(
		                CASE 
		                WHEN @bulan = 1 THEN (A.Dr_1 - A.Cr_1)
		                WHEN @bulan = 2 THEN (A.Dr_1 + A.Dr_2) - (A.Cr_1 + A.Cr_2)
		                WHEN @bulan = 3 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3) - (A.Cr_1 + A.Cr_2 + A.Cr_3)
		                WHEN @bulan = 4 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4) - (A.Cr_1 + A.Cr_2 + A.Cr_2 + A.Cr_4)
		                WHEN @bulan = 5 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5)
		                WHEN @bulan = 6 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6)
		                WHEN @bulan = 7 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7)
		                WHEN @bulan = 8 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8)	
		                WHEN @bulan = 9 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9)
		                WHEN @bulan = 10 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10)
		                WHEN @bulan = 11 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11)
		                WHEN @bulan = 12 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)
		                ELSE 0 
		                END
	                ), '-', '') AS amaun,
	                REPLACE(SUM(CASE 
	                WHEN @bulan = 1 THEN (A.Dr_1 - A.Cr_1)
	                WHEN @bulan = 2 THEN (A.Dr_2 - A.Cr_2) 
	                WHEN @bulan = 3 THEN (A.Dr_3 - A.Cr_3) 
                    WHEN @bulan = 4 THEN (A.Dr_4 - A.Cr_4) 
	                WHEN @bulan = 5 THEN (A.Dr_5 - A.Cr_5)
                    WHEN @bulan = 6 THEN (A.Dr_6 - A.Cr_6)
	                WHEN @bulan = 7 THEN (A.Dr_7 - A.Cr_7)
	                WHEN @bulan = 8 THEN (A.Dr_8 - A.Cr_8)
	                WHEN @bulan = 9 THEN (A.Dr_9 - A.Cr_9)
	                WHEN @bulan = 10 THEN (A.Dr_10 - A.Cr_10)
	                WHEN @bulan = 11 THEN (A.Dr_11 - A.Cr_11)
                    WHEN @bulan = 12 THEN (A.Dr_12 - A.Cr_12)

	                ELSE 0 
	                END), '-','') AS amaun2
                FROM
                SMKB_Lejar_Am A
                JOIN SMKB_Vot B ON A.Kod_Vot=B.Kod_Vot
                WHERE
                A.Tahun = @tahun 
                And B.Kod_Jenis In ('I','E')
                And A.Kod_Kump_Wang >= '01'
                And A.Kod_Kump_Wang <= '11'
                --And A.Kod_PTJ >= '010000'
                --And A.Kod_PTJ <='Y00005'
                And A.Kod_PTJ >= @ptj
                And A.Kod_PTJ <= @ptj
                --And A.Kod_Vot >= @kodVotFrom AND A.Kod_Vot <= @kodVotTo
                --AND Kod_Syarikat = 'UTeM'
                GROUP BY
                B.Kod_Jenis,
                substring(A.Kod_Vot, 1, 2) + '000',
                substring(A.Kod_Vot, 1, 1) + '0000'
                )mainTbl WHERE (CAST(amaun as decimal)  <> 0.00 or CAST(amaun2 as decimal) <> 0.00) " + where + "
                ORDER BY
                Kod_Jenis DESC, kodvot"
            End If

            cmd.Connection = sqlconn
            cmd.CommandText = query

            cmd.Parameters.Add(New SqlParameter("@tahun", tahun))
            cmd.Parameters.Add(New SqlParameter("@bulan", bulan))
            'cmd.Parameters.Add(New SqlParameter("@syarikat", syarikat))
            cmd.Parameters.Add(New SqlParameter("@ptj", ptj))

            If kod <> "" Then
                cmd.Parameters.Add(New SqlParameter("@kodvot", Left(kod, 2)))
            End If
            dt.Load(cmd.ExecuteReader())
            Return dt
        End Using
    End Function


    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadOrderRecord_SenaraiTransaksiJurnal() As String
        Dim resp As New ResponseRepository


        dt = GetOrder_SenaraiTransaksiJurnal()
        'resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(dt)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetOrder_SenaraiTransaksiJurnal() As DataTable
        Dim db = New DBKewConn

        Dim query As String = "select No_Jurnal, No_Rujukan, Butiran, Jumlah , FORMAT (Tkh_Transaksi, 'dd-MM-yyyy') as Tkh_Transaksi , 
        (select Butiran from SMKB_Kod_Status_Dok where Kod_Modul = '04' and Status = 1 and Kod_Status_Dok = SMKB_Jurnal_Hdr.Kod_Status_Dok) as Kod_Status_Dok
        from SMKB_Jurnal_Hdr 
        where status = 1 "

        Return db.Read(query)
    End Function


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

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadRecordJurnal(ByVal id As String) As String
        Dim resp As New ResponseRepository

        dt = GetTransaksiJurnal(id)
        resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadHdrJurnal(ByVal id As String) As String
        Dim resp As New ResponseRepository

        dt = GetHdrJurnal(id)
        resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    Private Function GetHdrJurnal(id As String) As DataTable
        Dim db = New DBKewConn

        Dim query As String = "select No_Jurnal, FORMAT(Tkh_Transaksi, 'yyyy-MM-dd') as Tkh_Transaksi, Jenis_Trans, 
        (select Butiran from SMKB_Jurnal_Jenis_Trans as jt where a.Jenis_Trans = jt.Kod_Trans) as ButiranJenis,
        No_Rujukan, Butiran, Jumlah , Kod_Status_Dok
        from  SMKB_Jurnal_Hdr a
        where No_Jurnal = @No_Jurnal and status =1"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@No_Jurnal", id))

        Return db.Read(query, param)
    End Function

    Private Function GetTransaksiJurnal(kod As String) As DataTable
        Dim db = New DBKewConn

        Dim query As String = "select Kod_Kump_Wang as colhidkw ,(select Butiran from SMKB_Kump_Wang as kw where a.Kod_Kump_Wang = kw.Kod_Kump_Wang) as colKW,
        Kod_Operasi  as colhidko, (select Butiran from SMKB_Operasi as ko where a.Kod_Operasi = ko.Kod_Operasi) as colKO,
        Kod_Projek as colhidkp,  (select Butiran from SMKB_Projek as kp where a.Kod_Projek = kp.Kod_Projek) as colKp,
        Kod_PTJ as colhidptj , (SELECT b.Pejabat FROM [DEVMIS\SQL_INS01].dbStaf.dbo.MS_PEJABAT as b
        WHERE b.STATUS = 1 and b.kodpejabat = left(Kod_PTJ,2)) as ButiranPTJ ,
        Kod_Vot , (select Butiran from SMKB_Vot as vot where a.Kod_Vot = vot.Kod_Vot) as ButiranVot,
        Butiran , Debit , Kredit
        from SMKB_Jurnal_Dtl as a
        where No_Jurnal = @No_Jurnal
        and status = 1
        order by No_Item"


        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@No_Jurnal", kod))

        Return db.Read(query, param)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function Lulusorder(order As Order) As String
        Dim resp As New ResponseRepository
        resp.Success("Data telah diluluskan")
        Dim success As Integer = 0
        Dim JumRekod As Integer = 0
        If order Is Nothing Then
            resp.Failed("Tiada simpan")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        If UpdateLulusOrder(order.OrderID) <> "OK" Then

            resp.Failed("Berjaya simpan")  'Gagal Menyimpan order XX 
            Return JsonConvert.SerializeObject(resp.GetResult())
            ' Exit Function
        Else

            If UpdateLulusOrderDetail(order.OrderID) <> "OK" Then

                resp.Failed("Berjaya simpan") 'Gagal Menyimpan order YX
                Return JsonConvert.SerializeObject(resp.GetResult())
                ' Exit Function
            Else
                If UpdateStatusDokOrder(order.OrderID, "Y") <> "OK" Then

                    resp.Failed("Berjaya simpan") 'Gagal Menyimpan order YX
                    Return JsonConvert.SerializeObject(resp.GetResult())
                    ' Exit Function

                End If
            End If

            success += 1

        End If



        If success = 0 Then
            resp.Failed("Rekod order detail gagal disimpan")
            Return JsonConvert.SerializeObject(resp.GetResult())

        Else
            resp.Success("Rekod berjaya disimpan", "00", order)
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If




        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function


    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function XLulusorder(order As Order) As String
        Dim resp As New ResponseRepository
        resp.Success("Data telah TIDAK diluluskan")
        Dim success As Integer = 0
        Dim JumRekod As Integer = 0
        If order Is Nothing Then
            resp.Failed("Tiada simpan")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        If UpdateXLulusOrder(order.OrderID) <> "OK" Then

            resp.Failed("Gagal Menyimpan order")
            Return JsonConvert.SerializeObject(resp.GetResult())
            ' Exit Function
        Else

            If UpdateStatusDokOrder(order.OrderID, "N") <> "OK" Then

                resp.Failed("Gagal Menyimpan order")
                Return JsonConvert.SerializeObject(resp.GetResult())
                ' Exit Function

            End If

            success += 1

        End If



        If success = 0 Then
            resp.Failed("Rekod order detail gagal disimpan")
            Return JsonConvert.SerializeObject(resp.GetResult())

        Else
            resp.Success("Rekod berjaya disimpan", "00", order)
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If




        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    Private Function SendEmail() As String
        Dim db As New DBKewConn

        Dim year = Date.Now.ToString("yyyy")
        Dim month = Date.Now.Month

        Dim lastID As Integer = 1
        Dim newOrderID As String = ""

        Dim query As String = $"select TOP 1 No_Akhir as id from SMKB_No_Akhir where Kod_Modul ='07' AND Prefix ='JK' AND Tahun =@year"
        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@year", year))

        dt = db.Read(query, param)

        If dt.Rows.Count > 0 Then
            lastID = CInt(dt.Rows(0).Item("id")) + 1

            UpdateNoAkhir("07", "JK", year, lastID)
        Else

            InsertNoAkhir("07", "JK", year, lastID)
        End If
        newOrderID = "JK" + Format(lastID, "000000").ToString + month.ToString("00") + Right(year.ToString(), 2)

        Return newOrderID
    End Function

    <System.Web.Services.WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadDataAliran(tahun As String)
        Session("tahun") = tahun
        Return LoadData()
    End Function

    <System.Web.Services.WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadData()

        Dim db As New DBKewConn

        Dim query As String = $"
            select (
            SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0) 
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = @tahun
            AND (Kod_Vot LIKE '9%'
            OR Kod_Vot LIKE '0%'
            OR Kod_Vot LIKE '1%'
            OR Kod_Vot LIKE '2%'
            OR Kod_Vot LIKE '36%'
            OR Kod_Vot LIKE '4%'
            OR Kod_Vot LIKE '5%')
            ) as LebihanPendapatan

            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = @tahun
            AND (Kod_Vot LIKE '91201%'
            OR Kod_Vot LIKE '91301%'
            OR Kod_Vot LIKE '91401%'
            OR Kod_Vot LIKE '91501%'
            OR Kod_Vot LIKE '91601%')) as Pelunasan_Geran

            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = @tahun
            AND (Kod_Vot LIKE '95%'
            OR Kod_Vot LIKE '971%'
            OR Kod_Vot LIKE '972%')) as Faedah_Keuntungan

            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = @tahun
            AND (Kod_Vot LIKE '812%'
            OR Kod_Vot LIKE '821%')) as Manfaat_Pekerja
            ,

            ( SELECT isnull(SUM((A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12) - (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = @tahun
            AND (Kod_Vot LIKE '03%')) as Susut_Nilai
            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = @tahun
            AND (Kod_Vot LIKE '04%')) as Belanja_Penulasan
            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = @tahun
            AND (Kod_Vot LIKE '07%')) as Belanja_Hutang_Ragu
            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = @tahun
            AND (Kod_Vot LIKE '29129%')) as Faedah_Pajakan
            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = @tahun
            AND (Kod_Vot LIKE '')) as Pelarasan_Pengkelasan_Akaun
            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = @tahun
            AND (Kod_Vot LIKE '')) as Pelarasan_Hartanah
            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = @tahun
            AND (Kod_Vot LIKE '97401')) as Keuntungan_atas_Pelupusan
            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = @tahun
            AND (Kod_Vot LIKE '51201')) as Kerugian_atas_Pelupusan

            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = @tahun
            AND (Kod_Vot LIKE '71%'
            OR Kod_Vot LIKE '79%')) as Pengurangan_AkaunBelumTerima_1
            ,

            ( SELECT isnull(SUM((A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12) - (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = @tahun
            AND (Kod_Vot LIKE '66101%')) as Pengurangan_AkaunBelumTerima_2
            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = @tahun
            AND (Kod_Vot LIKE '07101%')) as Pengurangan_AkaunBelumTerima_3
            ,
            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = @tahun
            AND (Kod_Vot LIKE '72%')) as Pengurangan_UrusNiaga_1
            ,
            ( SELECT isnull(SUM( (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12) - (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = @tahun
            AND (Kod_Vot LIKE '66102%')) as Pengurangan_UrusNiaga_2
            ,
            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = @tahun
            AND (Kod_Vot LIKE '74%')) as Pertambahan_Prabayar_1
            ,
            ( SELECT isnull(SUM( (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12) - (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = @tahun
            AND (Kod_Vot LIKE '662%')) as Pertambahan_Prabayar_2
            ,
            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = @tahun
            AND (Kod_Vot LIKE '07102%')) as Pertambahan_Prabayar_3
            ,
            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = @tahun
            AND (Kod_Vot LIKE '751%'
            or Kod_Vot LIKE '752%')) as Pengurangan_Terakru
            ,

            ( SELECT isnull(SUM( (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12) - (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = @tahun
            AND (Kod_Vot LIKE '811%'
            or Kod_Vot LIKE '814%'
            or Kod_Vot LIKE '83%'
            or Kod_Vot LIKE '84%'
            or Kod_Vot LIKE '85%'
            or Kod_Vot LIKE '86%'
            or Kod_Vot LIKE '87%')) as Pengurangan_Belum_Bayar
            ,

            ( SELECT isnull(SUM( (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12) - (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = @tahun
            AND (Kod_Vot LIKE '')) as Manfaat_Pekerja_Dibayar
            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = @tahun
            AND (Kod_Vot LIKE '95%'
            or Kod_Vot LIKE '97101%'
            or Kod_Vot LIKE '97201%')) as Faedah_Keuntungan_Terima_1
            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = @tahun
            AND (Kod_Vot LIKE '95301%')) as Faedah_Keuntungan_Terima_2
            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = @tahun
            AND (Kod_Vot LIKE '95301%')) as Dividen_Terima
            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = @tahun
            AND (Kod_Vot LIKE '95401%')) as Pelaburan_Lain
            ,

            ( SELECT isnull(SUM((A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = @tahun
            AND (Kod_Vot LIKE '71301%'
            or Kod_Vot LIKE '71401%'
            or Kod_Vot LIKE '71801%'
            AND Kod_Vot LIKE '79201%'
            or Kod_Vot LIKE '79301%'
            or Kod_Vot LIKE '79501%')) as Penerimaan_Pinjaman
            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = @tahun
            AND (Kod_Vot LIKE '71301%'
            or Kod_Vot LIKE '71401%'
            or Kod_Vot LIKE '71801%'
            AND Kod_Vot LIKE '79201%'
            or Kod_Vot LIKE '79301%'
            or Kod_Vot LIKE '79501%')) as Bayaran_Pinjaman
            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = @tahun
            AND (Kod_Vot LIKE '31%'
            or Kod_Vot LIKE '32%'
            or Kod_Vot LIKE '33%'
            AND Kod_Vot LIKE '34%'
            or Kod_Vot LIKE '35%')) as Pembelian_Hartanah
            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = @tahun
            AND (Kod_Vot LIKE '38%')) as Pembelian_Aset_Tak_Ketara
            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = @tahun
            AND (Kod_Vot LIKE '')) as Terimaan_Hasil_Pelupusan
            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = @tahun
            AND (Kod_Vot LIKE '322%'
            or Kod_Vot LIKE '333%'
            or Kod_Vot LIKE '37%')) as Pembayaran_Pembinaan
            ,

            -(( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = @tahun
            AND (Kod_Vot LIKE '813%'))) as Pembayaran_Pajakan
            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = @tahun
            AND (Kod_Vot LIKE '')) as Penerimaan_Geran_Khusus

            ,

            ( SELECT isnull(SUM((A.Dr_1) - (A.Cr_1)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = @tahun
            AND (Kod_Vot LIKE '73%'
            or Kod_Vot LIKE '76%')) as Baki_1_Jan
            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = @tahun
            AND (Kod_Vot LIKE '73%')) as Simpanan_Tetap
            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = @tahun
            AND (Kod_Vot LIKE '76%')) as Tunai_Di_Tangan"

        'Dim dt As DataTable = db.fSelectCommandDt(query)

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@tahun", Session("tahun")))

        Dim dt As DataTable = db.Read(query, param)

        Return JsonConvert.SerializeObject(dt)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadData2()

        Dim db As New DBKewConn

        Dim query As String = $"
            select (
            SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0) 
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = '2022'
            AND (Kod_Vot LIKE '9%'
            OR Kod_Vot LIKE '0%'
            OR Kod_Vot LIKE '1%'
            OR Kod_Vot LIKE '2%'
            OR Kod_Vot LIKE '36%'
            OR Kod_Vot LIKE '4%'
            OR Kod_Vot LIKE '5%')
            ) as LebihanPendapatan

            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = '2022'
            AND (Kod_Vot LIKE '91201%'
            OR Kod_Vot LIKE '91301%'
            OR Kod_Vot LIKE '91401%'
            OR Kod_Vot LIKE '91501%'
            OR Kod_Vot LIKE '91601%')) as Pelunasan_Geran

            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = '2022'
            AND (Kod_Vot LIKE '95%'
            OR Kod_Vot LIKE '971%'
            OR Kod_Vot LIKE '972%')) as Faedah_Keuntungan

            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = '2022'
            AND (Kod_Vot LIKE '812%'
            OR Kod_Vot LIKE '821%')) as Manfaat_Pekerja
            ,

            ( SELECT isnull(SUM((A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12) - (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = '2022'
            AND (Kod_Vot LIKE '03%')) as Susut_Nilai
            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = '2022'
            AND (Kod_Vot LIKE '04%')) as Belanja_Penulasan
            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = '2022'
            AND (Kod_Vot LIKE '07%')) as Belanja_Hutang_Ragu
            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = '2022'
            AND (Kod_Vot LIKE '29129%')) as Faedah_Pajakan
            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = '2022'
            AND (Kod_Vot LIKE '')) as Pelarasan_Pengkelasan_Akaun
            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = '2022'
            AND (Kod_Vot LIKE '')) as Pelarasan_Hartanah
            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = '2022'
            AND (Kod_Vot LIKE '97401')) as Keuntungan_atas_Pelupusan
            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = '2022'
            AND (Kod_Vot LIKE '51201')) as Kerugian_atas_Pelupusan

            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = '2022'
            AND (Kod_Vot LIKE '71%'
            OR Kod_Vot LIKE '79%')) as Pengurangan_AkaunBelumTerima_1
            ,

            ( SELECT isnull(SUM((A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12) - (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = '2022'
            AND (Kod_Vot LIKE '66101%')) as Pengurangan_AkaunBelumTerima_2
            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = '2022'
            AND (Kod_Vot LIKE '07101%')) as Pengurangan_AkaunBelumTerima_3
            ,
            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = '2022'
            AND (Kod_Vot LIKE '72%')) as Pengurangan_UrusNiaga_1
            ,
            ( SELECT isnull(SUM( (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12) - (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = '2022'
            AND (Kod_Vot LIKE '66102%')) as Pengurangan_UrusNiaga_2
            ,
            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = '2022'
            AND (Kod_Vot LIKE '74%')) as Pertambahan_Prabayar_1
            ,
            ( SELECT isnull(SUM( (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12) - (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = '2022'
            AND (Kod_Vot LIKE '662%')) as Pertambahan_Prabayar_2
            ,
            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = '2022'
            AND (Kod_Vot LIKE '07102%')) as Pertambahan_Prabayar_3
            ,
            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = '2022'
            AND (Kod_Vot LIKE '751%'
            or Kod_Vot LIKE '752%')) as Pengurangan_Terakru
            ,

            ( SELECT isnull(SUM( (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12) - (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = '2022'
            AND (Kod_Vot LIKE '811%'
            or Kod_Vot LIKE '814%'
            or Kod_Vot LIKE '83%'
            or Kod_Vot LIKE '84%'
            or Kod_Vot LIKE '85%'
            or Kod_Vot LIKE '86%'
            or Kod_Vot LIKE '87%')) as Pengurangan_Belum_Bayar
            ,

            ( SELECT isnull(SUM( (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12) - (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = '2022'
            AND (Kod_Vot LIKE '')) as Manfaat_Pekerja_Dibayar
            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = '2022'
            AND (Kod_Vot LIKE '95%'
            or Kod_Vot LIKE '97101%'
            or Kod_Vot LIKE '97201%')) as Faedah_Keuntungan_Terima_1
            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = '2022'
            AND (Kod_Vot LIKE '95301%')) as Faedah_Keuntungan_Terima_2
            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = '2022'
            AND (Kod_Vot LIKE '95301%')) as Dividen_Terima
            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = '2022'
            AND (Kod_Vot LIKE '95401%')) as Pelaburan_Lain
            ,

            ( SELECT isnull(SUM((A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = '2022'
            AND (Kod_Vot LIKE '71301%'
            or Kod_Vot LIKE '71401%'
            or Kod_Vot LIKE '71801%'
            AND Kod_Vot LIKE '79201%'
            or Kod_Vot LIKE '79301%'
            or Kod_Vot LIKE '79501%')) as Penerimaan_Pinjaman
            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = '2022'
            AND (Kod_Vot LIKE '71301%'
            or Kod_Vot LIKE '71401%'
            or Kod_Vot LIKE '71801%'
            AND Kod_Vot LIKE '79201%'
            or Kod_Vot LIKE '79301%'
            or Kod_Vot LIKE '79501%')) as Bayaran_Pinjaman
            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = '2022'
            AND (Kod_Vot LIKE '31%'
            or Kod_Vot LIKE '32%'
            or Kod_Vot LIKE '33%'
            AND Kod_Vot LIKE '34%'
            or Kod_Vot LIKE '35%')) as Pembelian_Hartanah
            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = '2022'
            AND (Kod_Vot LIKE '38%')) as Pembelian_Aset_Tak_Ketara
            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = '2022'
            AND (Kod_Vot LIKE '')) as Terimaan_Hasil_Pelupusan
            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = '2022'
            AND (Kod_Vot LIKE '322%'
            or Kod_Vot LIKE '333%'
            or Kod_Vot LIKE '37%')) as Pembayaran_Pembinaan
            ,

            -(( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = '2022'
            AND (Kod_Vot LIKE '813%'))) as Pembayaran_Pajakan
            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = '2022'
            AND (Kod_Vot LIKE '')) as Penerimaan_Geran_Khusus

            ,

            ( SELECT isnull(SUM((A.Dr_1) - (A.Cr_1)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = '2022'
            AND (Kod_Vot LIKE '73%'
            or Kod_Vot LIKE '76%')) as Baki_1_Jan
            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = '2022'
            AND (Kod_Vot LIKE '73%')) as Simpanan_Tetap
            ,

            ( SELECT isnull(SUM((A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)),0)
            FROM SMKB_Lejar_Am AS A
            WHERE Tahun = '2022'
            AND (Kod_Vot LIKE '76%')) as Tunai_Di_Tangan"

        Dim dt As DataTable = db.fSelectCommandDt(query)

        'Dim param As New List(Of SqlParameter)
        'param.Add(New SqlParameter("@tahun", "2022"))

        'Return db.Read(query, param)
        Return JsonConvert.SerializeObject(dt)
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadOrderRecord_SenaraiKewangan() As String
        Dim resp As New ResponseRepository

        Dim dt = GetOrder_SenaraiKewangan()
        Return JsonConvert.SerializeObject(dt)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetOrder_SenaraiKewangan() As DataTable
        Dim db As New DBKewConn

        Dim query As String
        query = "SELECT Kod_Kump_Wang, Butiran FROM SMKB_Kump_Wang WHERE Status = @Status"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@Status", "1"))

        Return db.Read(query, param)
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadRecord_BukuVot(votMula As String, votHingga As String, tkhMula As String, tkhHingga As String, syarikat As String, ptj As String, kodkw As String) As String
        Dim resp As New ResponseRepository
        If tkhMula = "" Or tkhHingga = "" Or syarikat = "" Or ptj = "" Or kodkw = "" Then
            Return JsonConvert.SerializeObject(New DataTable)
        End If
        'Session("tkhMula") = tkhMula
        'Session("tkhHingga") = tkhHingga
        'Session("syarikat") = syarikat
        'Session("ptj") = ptj
        'Session("kodkw") = kodkw


        dt = Get_SenaraiBukuVot(tkhMula, tkhHingga, votMula, votHingga, syarikat, ptj, kodkw)
        Dim totalRecords As Integer = dt.Rows.Count

        Return JsonConvert.SerializeObject(dt)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function Get_SenaraiBukuVot(tkhMula As String, tkhHingga As String, votMula As String, votHingga As String, syarikat As String, ptj As String, kodkw As String) As DataTable
        Dim db = New DBKewConn
        Dim dt As New DataTable

        Dim connectionString As String = "server=v-sql12.utem.edu.my;database=DbKewangan;uid=smkb;pwd=smkb*pwd;"


        Using sqlconn As New SqlConnection(connectionString)
            Dim cmd As New SqlCommand
            sqlconn.Open()

            Dim query As String

            Dim optionalWhere As String = ""

            'If Session("kodkw") = "00" Then
            '    optionalWhere = "And A.Kod_Kump_Wang >= 1 And A.Kod_Kump_Wang <= 11"
            'Else
            '    optionalWhere = "And A.Kod_Kump_Wang >= @kodkw And A.Kod_Kump_Wang <= @kodkw"
            'End If

            query = "SELECT VBG_BUKUVOT.BUTIRAN,   
                    VBG_BUKUVOT.JENIS,   
                    VBG_BUKUVOT.KW,   
                    VBG_BUKUVOT.VOT,   
                    VBG_BUKUVOT.JBT,   
                    SUM(VBG_BUKUVOT.DEBIT) as DEBIT,   
                    SUM(VBG_BUKUVOT.CREDIT) as CREDIT,   
                    ISNULL(VBG_BUKUVOT.TNG,0) AS TNG,
                    VBG_BUKUVOT.TARIKH,   
                    VBG_BUKUVOT.TAHUN,   
                    VBG_BUKUVOT.HD_VOT,   
                    VBG_BUKUVOT.NO_DOK,   
                    VBG_BUKUVOT.SEQ,
                    VBG_BUKUVOT.AP_JENIS,
                    0 as lst_tng,
                    0 as vk
                    FROM VBG_BUKUVOT
                    WHERE ( VBG_BUKUVOT.tarikh >= CONVERT(DATETIME, @tkhMula, 102) ) AND  
                    ( VBG_BUKUVOT.tarikh <= CONVERT(DATETIME, @tkhHingga, 102) ) AND
                    ( VBG_BUKUVOT.TAHUN = '2023' ) AND
                    ( VBG_BUKUVOT.kw >= @kodkw and VBG_BUKUVOT.kw <= @kodkw) AND
                    ( VBG_BUKUVOT.JBT >= @ptj and VBG_BUKUVOT.JBT <= @ptj) and
                    ( VBG_BUKUVOT.HD_VOT >= @VotMula and VBG_BUKUVOT.HD_VOT <= @VotHingga)
                    group by VBG_BUKUVOT.BUTIRAN,   
                    VBG_BUKUVOT.JENIS,   
                    VBG_BUKUVOT.KW,   
                    VBG_BUKUVOT.VOT,   
                    VBG_BUKUVOT.JBT,   
                    VBG_BUKUVOT.TNG,
                    VBG_BUKUVOT.TARIKH,   
                    VBG_BUKUVOT.TAHUN,   
                    VBG_BUKUVOT.HD_VOT,   
                    VBG_BUKUVOT.NO_DOK,   
                    VBG_BUKUVOT.SEQ,
                    VBG_BUKUVOT.AP_JENIS
                    UNION ALL
                    SELECT 'BAKI AWAL' as butiran, 'BK' as jenis,	
                    bb.kw as kw, bb.vot as vot, bb.ptj as jbt,	
                    (	
	                    dbo.func_get_blj(bb.tahun, CONVERT(DATETIME, @tkhHingga, 102), bb.kw, bb.ptj, bb.vot) +
	                    dbo.func_get_vk (bb.tahun, CONVERT(DATETIME, @tkhHingga, 102), bb.kw, bb.ptj, bb.vot)
                    ) as debit,	
                    (	
	                    dbo.func_get_perutk(bb.tahun, CONVERT(DATETIME,@tkhHingga, 102), bb.kw, bb.ptj, bb.vot) +
	                    dbo.func_get_bajetbf(bb.tahun, bb.kw, bb.ptj, bb.vot)- 
	                    dbo.func_get_vk (bb.tahun, CONVERT(DATETIME, @tkhHingga, 102), bb.kw, bb.ptj, bb.vot)
                    ) as credit,	
                    (	
	                    dbo.func_get_baki_tng( bb.tahun, CONVERT(DATETIME, @tkhHingga, 102), bb.kw, bb.ptj, bb.vot) +
	                    dbo.func_get_lst_tng(bb.tahun, bb.kw, bb.ptj, bb.vot) 
                    ) as tng,	
                    @tkhMula AS tarikh,	
                    bb.tahun as tahun, bb.vot as hd_vot, '' as no_dok, 0 as seq, '',	
                    (
	                    dbo.func_get_lst_tng(bb.tahun, bb.kw, bb.ptj, bb.vot) - 
	                    dbo.func_get_piutang_tng(bb.tahun, bb.kw, bb.ptj, bb.vot,'')
                    ) as lst_tng,
                    dbo.func_get_vk (bb.tahun,CONVERT(DATETIME, @tkhHingga, 102), bb.kw, bb.ptj, bb.vot) AS vk	
                    from
	                    (	
		                    SELECT DISTINCT mk01_vottahun.kodkw as kw, mk01_vottahun.kodptj as ptj,
		                    mk01_vottahun.kodvot as vot, mk01_vottahun.mk01_tahun as tahun
		                    from mk01_vottahun
		                    where mk01_vottahun.kodKW >= @kodkw
		                    and mk01_vottahun.kodVOT >= @VotMula
		                    and mk01_vottahun.kodKW <= @kodkw
		                    and mk01_vottahun.kodVOT <= @VotHingga
		                    and mk01_vottahun.kodptj >= @ptj
		                    and mk01_vottahun.kodptj <= @ptj
		                    and mk01_vottahun.mk01_TAHUN = year(@tkhHingga)
		                    and substring(mk01_vottahun.kodvot,2,4) <> '0000'
		                    and substring(@tkhHingga, 1, 5) <> '01/01'
	                    GROUP BY mk01_vottahun.mk01_tahun, mk01_vottahun.kodkw, mk01_vottahun.kodptj, mk01_vottahun.kodvot
                    ) bb

                    ORDER BY VOT, SEQ"

            cmd.Connection = sqlconn
            cmd.CommandText = query


            cmd.Parameters.Add(New SqlParameter("@ptj", ptj))
            cmd.Parameters.Add(New SqlParameter("@kodkw", kodkw))
            cmd.Parameters.Add(New SqlParameter("@tkhMula", tkhMula))
            cmd.Parameters.Add(New SqlParameter("@tkhHingga", tkhHingga))
            cmd.Parameters.Add(New SqlParameter("@VotMula", votMula))
            cmd.Parameters.Add(New SqlParameter("@VotHingga", votHingga))

            dt.Load(cmd.ExecuteReader())
            Return dt
        End Using
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadRecordPenyataEkuiti(tahun As String)
        Session("tahun") = tahun
        Return Get_PenyataEkuiti()
    End Function

    <System.Web.Services.WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function Get_PenyataEkuiti()
        Dim db = New DBKewConn

        If String.IsNullOrEmpty(Session("tahun")) Then
            Return JsonConvert.SerializeObject(New DataTable())
        End If

        Dim query As String

        query = "SELECT 'Baki pada 1 Januari 2022' AS [vot_name],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '01' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_1],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '02' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_2],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '03' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_3],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '04' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_4],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '05' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_5],
	           REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '06' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_6],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '07' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_7],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '08' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_8],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '09' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_9],
	           REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '10' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_10],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '11' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_11]
        FROM SMKB_Lejar_Am
        WHERE (Kod_Kump_Wang >= '01' AND Kod_Kump_Wang <= '11')
          AND Tahun = @tahun
          AND Kod_Vot = '91101' 
        UNION ALL
        ---91101 Baki pada 1 Januari 2022 ---
        SELECT 'Geran Kerajaan' AS [vot_name],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '01' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_1],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '02' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_2],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '03' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_3],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '04' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_4],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '05' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_5],
	           REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '06' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_6],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '07' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_7],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '08' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_8],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '09' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_9],
	           REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '10' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_10],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '11' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_11]
        FROM SMKB_Lejar_Am
        WHERE (Kod_Kump_Wang >= '01' AND Kod_Kump_Wang <= '11')
          AND Tahun = @tahun
          AND Kod_Vot = '91101' 
        UNION ALL
        ---91200, 91300, 91400, 91500, 91600 Geran yang Dilunaskan ---
        SELECT 'Geran yang Dilunaskan' AS [vot_name],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '01' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_1],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '02' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_2],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '03' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_3],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '04' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_4],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '05' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_5],
	           REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '06' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_6],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '07' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_7],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '08' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_8],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '09' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_9],
	           REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '10' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_10],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '11' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_11]
	        FROM SMKB_Lejar_Am
	        WHERE (Kod_Kump_Wang >= '01' AND Kod_Kump_Wang <= '11')
	          AND Tahun = @tahun
	          AND (Kod_Vot LIKE '912%'
	          or Kod_Vot LIKE '913%'
	          or Kod_Vot LIKE '914%'
	          or Kod_Vot LIKE '915%'
	          or Kod_Vot LIKE '916%')
	          UNION ALL
        ---92000, 93000, 94000, 95000, 96000 & 97000 Pendapatan Lain ---
        SELECT 'Pendapatan Lain' AS [vot_name],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '01' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_1],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '02' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_2],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '03' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_3],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '04' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_4],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '05' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_5],
	           REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '06' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_6],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '07' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_7],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '08' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_8],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '09' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_9],
	           REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '10' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_10],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '11' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_11]
	        FROM SMKB_Lejar_Am
	        WHERE (Kod_Kump_Wang >= '01' AND Kod_Kump_Wang <= '11')
	          AND Tahun = @tahun
	          AND (Kod_Vot LIKE '92%'
	          or Kod_Vot LIKE '93%'
	          or Kod_Vot LIKE '94%'
	          or Kod_Vot LIKE '95%'
	          or Kod_Vot LIKE '96%'
	          or Kod_Vot LIKE '97%')
	          UNION ALL
        --- 10000, 20000, 36000, 40000 & 50000	(-) Perbelanjaan
        SELECT '(-) Perbelanjaan' AS [vot_name],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '01' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_1],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '02' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_2],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '03' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_3],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '04' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_4],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '05' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_5],
	           REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '06' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_6],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '07' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_7],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '08' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_8],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '09' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_9],
	           REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '10' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_10],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '11' THEN (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) - (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_11]
	        FROM SMKB_Lejar_Am
	        WHERE (Kod_Kump_Wang >= '01' AND Kod_Kump_Wang <= '11')
	          AND Tahun = @tahun
	          AND (Kod_Vot LIKE '1%'
	          or Kod_Vot LIKE '2%'
	          or Kod_Vot LIKE '3%'
	          or Kod_Vot LIKE '4%'
	          or Kod_Vot LIKE '5%')
	          UNION ALL
               --- 89101 (Jumlah Kredit Tolak Debit) Pelarasan Kumpulan Wang
               SELECT 'Pelarasan Kumpulan Wang' AS [vot_name],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '01' THEN (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) - (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_1],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '02' THEN (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) - (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_2],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '03' THEN (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) - (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_3],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '04' THEN (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) - (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_4],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '05' THEN (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) - (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_5],
	           REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '06' THEN (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) - (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_6],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '07' THEN (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) - (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_7],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '08' THEN (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) - (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_8],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '09' THEN (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) - (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_9],
	           REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '10' THEN (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) - (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_10],
               REPLACE(ISNULL(SUM(CASE WHEN Kod_Kump_Wang = '11' THEN (Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12) - (Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12) ELSE 0 END), 0), '-', '') as [Total_Kod_Kw_11]
	        FROM SMKB_Lejar_Am
	        WHERE (Kod_Kump_Wang >= '01' AND Kod_Kump_Wang <= '11')
	          AND Tahun = @tahun
	          AND Kod_Vot LIKE '89101%'"


        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@tahun", Session("tahun")))

        Dim dt As DataTable = db.Read(query, param)

        Return JsonConvert.SerializeObject(dt)




    End Function
End Class