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
'Imports System.Globalization
Imports System.Threading
Imports System.Security.Cryptography

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class SenaraiBilBerulang
    Inherits System.Web.Services.WebService

    Dim dt As DataTable

    <WebMethod()>
    Public Function HelloWorld() As String
        Return "Hello World"
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SaveBilBerulang(order As Bil_Hdr) As String
        Dim resp As New ResponseRepository
        resp.Success("Data telah disimpan")
        Dim success As Integer = 0
        Dim JumRekod As Integer = 0
        Console.WriteLine("Received order: " & JsonConvert.SerializeObject(order))
        If order Is Nothing Then
            resp.Failed("Tiada Data")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        If order.OrderID = "" Then
            order.OrderID = GenerateNoBil()
            order.Id = GenerateBilKontrakID(order.OrderID)
            If order.Jumlah = "" Then
                order.Jumlah = "0.00"
            Else
                order.Jumlah = order.Jumlah
            End If
            If InsertBilHdr(order.OrderID, order.PenghutangID, order.TkhMula, order.TkhTamat, order.Kontrak, order.JenisUrusniaga, order.Tujuan, order.Jumlah, order.tkhBil, order.tempoh, order.tempohbyrn, order.norujukan, "01") <> "OK" Then
                resp.Failed("Tidak Masuk")
                Return JsonConvert.SerializeObject(resp.GetResult())
                Exit Function
            Else
                If InsertBilKontrak(order.OrderID, order.Id, order.Jumlah, order.Kontrak, order.Tujuan, order.norujukan) <> "OK" Then
                    resp.Failed("Bil Berulang Tidak Masuk")
                    Return JsonConvert.SerializeObject(resp.GetResult())
                    Exit Function
                End If
            End If
        Else
            resp.Failed("Bil Telah Wujud")
            Return JsonConvert.SerializeObject(resp.GetResult())
            Exit Function
        End If

        For Each orderDetail As Bil_Dtl In order.OrderDetails
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
            'orderDetail.amount = orderDetail.quantity * orderDetail.price 'This can be automated inside orderdetail model

            If orderDetail.id = "" Then
                orderDetail.id = GenerateOrderDetailID(order.OrderID)
                orderDetail.OrderID = order.OrderID
                If InsertBilDtl(orderDetail) = "OK" Then
                    success += 1
                Else
                    resp.Failed("Record gagal dijana")
                    Return JsonConvert.SerializeObject(resp.GetResult())
                End If
            End If
        Next

        If success = 0 Then
            resp.Failed("Bil Gagal Dijana")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        If Not success = JumRekod Then
            resp.Success("Rekod order detail berjaya disimpan dengan beberapa rekod tidak disimpan", "00", order)
            Return JsonConvert.SerializeObject(resp.GetResult())
        Else
            resp.Success("Bil Berjaya Dijana")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadOrderRecord_SenaraiBilBerulang(category_filter As String, isClicked As Boolean, tkhMula As DateTime, tkhTamat As DateTime) As String
        Dim resp As New ResponseRepository

        If isClicked = False Then
            Return JsonConvert.SerializeObject(New DataTable)
        End If

        dt = GetOrder_SenaraiBilBerulang(category_filter, tkhMula, tkhTamat)

        Return JsonConvert.SerializeObject(dt)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetOrder_SenaraiBilBerulang(category_filter As String, tkhMula As DateTime, tkhTamat As DateTime) As DataTable
        Dim db = New DBKewConn
        Dim tarikhQuery As String = ""
        Dim param As List(Of SqlParameter)

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
            param = New List(Of SqlParameter)
            param.Add(New SqlParameter("@tkhMula", tkhMula))
            param.Add(New SqlParameter("@TkhTamat", tkhTamat))
        End If

        Dim query As String = "SELECT A.No_Bil as No_Invois,B.Nama_Penghutang,C.Butiran as UrusNiaga,A.Tujuan,FORMAT(A.Tkh_Mohon, 'dd/MM/yyyy') AS TKHMOHON,
                                CASE WHEN Tkh_Mula <> '' THEN FORMAT(Tkh_Mula, 'dd/MM/yyyy') END AS Tkh_Mula,
                                CASE WHEN Tkh_Tamat <> '' THEN FORMAT(Tkh_Tamat, 'dd/MM/yyyy') END AS Tkh_Tamat,Jumlah
                                FROM SMKB_Bil_Hdr A
                                inner JOIN SMKB_Penghutang_Master B ON A.Kod_Penghutang=B.Kod_Penghutang
                                inner JOIN SMKB_Kod_Urusniaga C ON A.Kod_Urusniaga=C.Kod_Urusniaga
                                WHERE  A.Status='1' AND A.Kontrak = '1' AND No_Bil NOT IN (SELECT No_Bil FROM SMKB_Bil_Kontrak) " & tarikhQuery

        Return db.Read(query)
    End Function

    Private Function GenerateNoBil() As String
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

    Private Function UpdateNoAkhir(kodModul As String, prefix As String, year As String, ID As String)
        Dim db As New DBKewConn
        Dim query As String = "UPDATE SMKB_No_Akhir
                               set No_Akhir = @No_Akhir
                               where Kod_Modul=@Kod_Modul and Prefix=@Prefix and Tahun =@Tahun"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@No_Akhir", ID))
        param.Add(New SqlParameter("@kod_Modul", kodModul))
        param.Add(New SqlParameter("@prefix", prefix))
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

    Public Function GenerateOrderDetailID(noBil As String) As String
        Dim db = New DBKewConn
        Dim lastID As Integer = 1
        Dim newOrderID As String = ""
        Dim param As New List(Of SqlParameter)

        Dim query As String = "SELECT TOP 1 No_Item as id 
                                FROM SMKB_Bil_Dtl 
                                WHERE No_Bil= @noBil
                                ORDER BY No_Item DESC"

        param.Add(New SqlParameter("@noBil", noBil))

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

    Public Function GenerateBilKontrakID(noBil As String) As String
        Dim db = New DBKewConn
        Dim lastID As Integer = 1
        Dim newOrderID As String = ""
        Dim param As New List(Of SqlParameter)

        'Dim query As String = "SELECT TOP 1 Bil as id 
        '                       FROM SMKB_Bil_Kontrak 
        '                       WHERE No_Bil= @noBil
        '                       ORDER BY Bil DESC"
        'COUNT BIL YG MEMPUNYAI NO_BIL YG SAMA
        Dim query As String = "SELECT COUNT(Bil) as id 
                                FROM SMKB_Bil_Kontrak 
                                WHERE No_Rujukan=@noBil"

        param.Add(New SqlParameter("@noBil", noBil))

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

    Private Function InsertBilKontrak(OrderID As String, Bil As String, Jumlah As String, StatKontrak As String, Ulasan As String, NoRujukan As String)
        Dim db As New DBKewConn
        Dim query As String = "INSERT INTO SMKB_Bil_Kontrak 
                               (No_Bil, Bil, Jumlah, Status_Kontrak, Ulasan, No_Rujukan) 
                               VALUES (@No_Bil, @Bil, @Jumlah, @StatKontrak, @Ulasan, @NoRujukan)"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@No_Bil", OrderID))
        param.Add(New SqlParameter("@Bil", Bil))
        param.Add(New SqlParameter("@Jumlah", Jumlah))
        'param.Add(New SqlParameter("@FlagBayar", FlagBayar))
        param.Add(New SqlParameter("@StatKontrak", StatKontrak))
        param.Add(New SqlParameter("@Ulasan", Ulasan))
        param.Add(New SqlParameter("@NoRujukan", NoRujukan))

        Return db.Process(query, param)
    End Function

    Private Function InsertBilHdr(orderid As String, penghutangid As String, TkhMula As String, TkhTamat As String, Kontrak As String, JenisUrusniaga As String, Tujuan As String, Jumlah As String, tkhBil As String, tempoh As String, tempohbyrn As String, norujukan As String, statusdok As String)
        Dim db As New DBKewConn
        Dim query As String = "INSERT INTO SMKB_Bil_Hdr (No_Bil,Tkh_Mohon,Kod_Status_Dok, Status, Kod_Penghutang,Tkh_Mula,Tkh_Tamat,Kontrak,Kod_Urusniaga,Tujuan, Jumlah,No_Staf_Penyedia,Tkh_Bil, Tempoh_Kontrak, Jenis_Tempoh,No_Rujukan)
        VALUES(@No_Bil, getdate(), @statusdok ,'1',@Kod_Penghutang,@Tkh_Mula,@Tkh_Tamat,@Kontrak,@Kod_Urusniaga,@Tujuan,@Jumlah,@NoStafPenyedia,@tkhbil,@tempoh,@tempohbyrn, @norujukan)"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@No_Bil", orderid))
        param.Add(New SqlParameter("@statusdok", statusdok))
        param.Add(New SqlParameter("@Kod_Penghutang", penghutangid))
        param.Add(New SqlParameter("@Tkh_Mula", TkhMula))
        param.Add(New SqlParameter("@Tkh_Tamat", TkhTamat))
        param.Add(New SqlParameter("@Kontrak", Kontrak))
        param.Add(New SqlParameter("@Kod_Urusniaga", JenisUrusniaga))
        param.Add(New SqlParameter("@Tujuan", Tujuan))
        param.Add(New SqlParameter("@Jumlah", Jumlah))
        param.Add(New SqlParameter("@NoStafPenyedia", Session("ssusrID")))
        param.Add(New SqlParameter("@tkhbil", TkhMula))
        param.Add(New SqlParameter("@tempoh", tempoh))
        param.Add(New SqlParameter("@tempohbyrn", tempohbyrn))
        param.Add(New SqlParameter("@norujukan", norujukan))

        Return db.Process(query, param)
    End Function

    Private Function InsertBilDtl(orderDetail As Bil_Dtl)
        Dim db As New DBKewConn
        Dim query As String = "INSERT INTO SMKB_Bil_Dtl(No_Bil,No_Item,Kod_Kump_Wang,Kod_Operasi,Kod_PTJ,Kod_Projek,Kod_Vot,Perkara,Kuantiti,Kadar_Harga,Jumlah,Tahun,Status,Diskaun,Cukai)
        VALUES( @no_bil , @no_item, @Kod_Kump_Wang, @Kod_Operasi , @Kod_PTJ , @Kod_Projek  , @Kod_Vot, @Perkara, @Kuantiti, @Kadar_Harga, @Jumlah, '2023', '1', @Diskaun, @Cukai)"
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
End Class