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


' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class Transaksi_WS
    Inherits System.Web.Services.WebService

    Dim sqlcmd As SqlCommand
    Dim sqlcon As SqlConnection
    Dim sqlread As SqlDataReader
    Dim dt As DataTable


    Public Function HelloWorld() As String
        Return "Hello World"
    End Function

    <System.Web.Services.WebMethod(EnableSession:=True)>
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

    <System.Web.Services.WebMethod(EnableSession:=True)>
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
                resp.Failed("Gagal Menyimpan Maklumat.")
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
                    resp.Failed("Gagal Menyimpan Maklumat")
                    Return JsonConvert.SerializeObject(resp.GetResult())
                    ' Exit Function
                End If
            End If
        End If

        For Each orderDetail As OrderDetail In order.OrderDetails

            If orderDetail.ddlPTJ = "" Then
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


    <System.Web.Services.WebMethod(EnableSession:=True)>
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
                    resp.Failed("Gagal Menyimpan Maklumat.")
                    Return JsonConvert.SerializeObject(resp.GetResult())
                    ' Exit Function
                End If
            End If

        End If

        For Each orderDetail As OrderDetail In order.OrderDetails

            If orderDetail.ddlPTJ = "" Then
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
        param.Add(New SqlParameter("@No_Staf", Session("ssusrID")))
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
        param.Add(New SqlParameter("@No_Staf", Session("ssusrID")))
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
        param.Add(New SqlParameter("@No_Staf", Session("ssusrID")))
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
            query &= " where A.Status = 1 and (A.Kod_Vot LIKE '%' + @kod + '%' or B.Butiran LIKE '%' + @kod2 + '%') and A.Kod_PTJ =@kod3 and b.Kod_Klasifikasi = 'D' order by A.Kod_Vot "
        Else
            query &= " where A.Status = 1 and A.Kod_PTJ =@kod3 and b.Kod_Klasifikasi = 'D' order by A.Kod_Vot"
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
        Dim query As String = "select Kod_Trans as value, Butiran as text From SMKB_Jurnal_Jenis_Trans where status = 1"
        Dim param As New List(Of SqlParameter)
        If kod <> "" Then
            'query &= " AND (Kod_Trans='A'or Kod_Trans='L') and Butiran like 'PELARASAN%'  and (Kod_Trans LIKE '%' + @kod + '%' or Butiran LIKE '%' + @kod2 + '%')"
            query &= " AND (Kod_Trans LIKE '%' + @kod + '%' or Butiran LIKE '%' + @kod2 + '%')"
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
    Public Function LoadOrderRecord_SenaraiLulusTransaksiJurnal(category_filter As String, isClicked As Boolean, tkhMula As DateTime, tkhTamat As DateTime) As String
        Dim resp As New ResponseRepository

        If isClicked = False Then
            Return JsonConvert.SerializeObject(New DataTable)
        End If

        dt = GetOrder_SenaraiLulusTransaksiJurnal(category_filter, tkhMula, tkhTamat)
        'resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(dt)
    End Function


    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetOrder_SenaraiLulusTransaksiJurnal(category_filter As String, tkhMula As DateTime, tkhTamat As DateTime) As DataTable
        Dim db = New DBKewConn
        Dim tarikhQuery As String = ""
        Dim param As List(Of SqlParameter)

        If category_filter = "1" Then 'Harini
            'tarikhQuery = " and a.Tkh_Transaksi = getdate() "
            tarikhQuery = " and CAST(a.Tkh_Transaksi AS DATE) = CAST(getdate() AS DATE) "
        ElseIf category_filter = "2" Then 'Semalam
            'tarikhQuery = " and a.Tkh_Transaksi >= DATEADD(day, -1, getdate()) and a.Tkh_Transaksi <= getdate() "
            tarikhQuery = " and CAST(a.Tkh_Transaksi AS DATE) = CAST(DATEADD(DAY, -1, GETDATE()) AS DATE) "
        ElseIf category_filter = "3" Then 'seminggu
            'tarikhQuery = " and a.Tkh_Transaksi >= DATEADD(day, -8, getdate()) and a.Tkh_Transaksi <= getdate() "
            tarikhQuery = " and CAST(a.Tkh_Transaksi AS DATE) >= CAST(DATEADD(DAY, -7, GETDATE()) AS DATE) "
        ElseIf category_filter = "4" Then '30 hari
            tarikhQuery = " and a.Tkh_Transaksi >= DATEADD(month, -1, getdate()) and a.Tkh_Transaksi <= getdate() "
        ElseIf category_filter = "5" Then '60 hari
            tarikhQuery = " and a.Tkh_Transaksi >= DATEADD(month, -2, getdate()) and a.Tkh_Transaksi <= getdate() "
        ElseIf category_filter = "6" Then 'custom
            tarikhQuery = " and a.Tkh_Transaksi >= @tkhMula and a.Tkh_Transaksi <= @TkhTamat "
            param = New List(Of SqlParameter)
            param.Add(New SqlParameter("@tkhMula", tkhMula))
            param.Add(New SqlParameter("@TkhTamat", tkhTamat))
        End If

        Dim query As String = "select No_Jurnal , A.No_Rujukan, Butiran, Jumlah , FORMAT (A.Tkh_Transaksi, 'dd-MM-yyyy') as Tkh_Transaksi , 
        (select Butiran from SMKB_Kod_Status_Dok where Kod_Modul = '04' and Status = 1 and Kod_Status_Dok = A.Kod_Status_Dok) as Kod_Status_Dok ,
        (select Butiran from SMKB_Jurnal_Jenis_Trans b where b.Kod_Trans = A.Jenis_Trans) as Jenis_Trans , MS01_Nama as No_Staf
        from SMKB_Jurnal_Hdr A
		LEFT JOIN SMKB_Status_Dok AS B ON A.No_Jurnal = B.No_Rujukan AND B.Kod_Status_Dok = '02'
		Left join [DEVMIS\SQL_INS01].dbStaf.dbo.MS01_Peribadi as c on c.MS01_NoStaf = b.No_Staf
        where A.status = 1  and A.Kod_Status_Dok in ('02','04','09') " & tarikhQuery & " order by a.Tkh_Transaksi desc"

        Return db.Read(query, param)
    End Function


    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadOrderRecord_SenaraiTransaksiJurnal(category_filter As String, isClicked As Boolean, tkhMula As DateTime, tkhTamat As DateTime) As String

        Dim resp As New ResponseRepository

        If isClicked = False Then
            Return JsonConvert.SerializeObject(New DataTable)
        End If

        dt = GetOrder_SenaraiTransaksiJurnal(category_filter, tkhMula, tkhTamat)
        'resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(dt)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetOrder_SenaraiTransaksiJurnal(category_filter As String, tkhMula As DateTime, tkhTamat As DateTime) As DataTable
        Dim db = New DBKewConn

        Dim tarikhQuery As String = ""
        Dim param As List(Of SqlParameter)

        If category_filter = "1" Then 'Harini
            'tarikhQuery = " and a.Tkh_Transaksi = getdate() "
            tarikhQuery = " WHERE CAST(Tkh_Transaksi AS DATE) = CAST(getdate() AS DATE) "
        ElseIf category_filter = "2" Then 'Semalam
            'tarikhQuery = " and a.Tkh_Transaksi >= DATEADD(day, -1, getdate()) and a.Tkh_Transaksi <= getdate() "
            tarikhQuery = " WHERE CAST(Tkh_Transaksi AS DATE) = CAST(DATEADD(DAY, -1, GETDATE()) AS DATE) "
        ElseIf category_filter = "3" Then 'seminggu
            'tarikhQuery = " and a.Tkh_Transaksi >= DATEADD(day, -8, getdate()) and a.Tkh_Transaksi <= getdate() "
            tarikhQuery = " WHERE CAST(Tkh_Transaksi AS DATE) >= CAST(DATEADD(DAY, -7, GETDATE()) AS DATE) "
        ElseIf category_filter = "4" Then '30 hari
            tarikhQuery = " WHERE Tkh_Transaksi >= DATEADD(month, -1, getdate()) and Tkh_Transaksi <= getdate() "
        ElseIf category_filter = "5" Then '60 hari
            tarikhQuery = " WHERE Tkh_Transaksi >= DATEADD(month, -2, getdate()) and Tkh_Transaksi <= getdate() "
        ElseIf category_filter = "6" Then 'custom
            tarikhQuery = " WHERE Tkh_Transaksi >= @tkhMula and Tkh_Transaksi <= @TkhTamat "
            param = New List(Of SqlParameter)
            param.Add(New SqlParameter("@tkhMula", tkhMula))
            param.Add(New SqlParameter("@TkhTamat", tkhTamat))
        End If

        Dim query As String = "select No_Jurnal, No_Rujukan, Butiran, Jumlah , FORMAT (Tkh_Transaksi, 'dd-MM-yyyy') as Tkh_Transaksi , 
        (select Butiran from SMKB_Kod_Status_Dok where Kod_Modul = '04' and Status = 1 and Kod_Status_Dok = SMKB_Jurnal_Hdr.Kod_Status_Dok) as Kod_Status_Dok
        from SMKB_Jurnal_Hdr  " & tarikhQuery

        Return db.Read(query, param)
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

    <System.Web.Services.WebMethod(EnableSession:=True)>
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


End Class