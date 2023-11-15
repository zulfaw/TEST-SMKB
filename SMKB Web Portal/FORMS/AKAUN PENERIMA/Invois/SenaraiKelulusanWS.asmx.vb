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

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.

<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class SenaraiKelulusanWS
    Inherits System.Web.Services.WebService
    Dim dt As DataTable

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
                                WHERE  A.Status='1' AND A.Kod_Status_Dok='02' " & tarikhQuery

        Return db.Read(query)
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
        Dim Tempoh As String
        Dim bil_tempoh As String
        Dim Tarikh_Mula As String

        'Dim query As String = "SELECT A.No_Bil as No_Invois,B.Nama_Penghutang,C.Butiran as UrusNiaga,A.Tujuan,FORMAT(A.Tkh_Mohon, 'dd/MM/yyyy') AS TKHMOHON,
        '                        Tkh_Mula,
        '                        CASE WHEN Tkh_Tamat <> '' THEN FORMAT(Tkh_Tamat, 'dd/MM/yyyy') END AS Tkh_Tamat,Jumlah,Kontrak,
        '                        D.Butiran AS TEMPOH,Tempoh_Kontrak
        '                        FROM SMKB_Bil_Hdr A
        '                        INNER JOIN SMKB_Penghutang_Master B ON A.Kod_Penghutang=B.Kod_Penghutang
        '                        INNER JOIN SMKB_Kod_Urusniaga C ON A.Kod_Urusniaga=C.Kod_Urusniaga
        '                        INNER JOIN SMKB_Lookup_Detail D ON D.Kod_Detail=A.Jenis_Tempoh AND D.Kod='AR09'
        '                        WHERE  A.Status='1' AND A.Kod_Status_Dok='03' AND Kontrak='1' AND Tkh_Tamat>=GETDATE() AND Tkh_Mula<=GETDATE()"
        'dt = db.Read(query, param)
        'If dt.Rows.Count > 0 Then
        '    Tempoh = CInt(dt.Rows(0).Item("TEMPOH"))
        '    bil_tempoh = CInt(dt.Rows(0).Item("Tempoh_Kontrak"))
        '    Tarikh_Mula = CInt(dt.Rows(0).Item("Tkh_Mula"))
        '    If Tempoh = "TAHUN" Then

        '    ElseIf Tempoh = "BULAN" Then


        '    End If
        'End If
        If category_filter = "1" Then 'Harini
                tarikhQuery = " AND CAST(A.Tkh_Mula AS DATE) = CAST(CURRENT_TIMESTAMP AS Tkh_Mula) "
            ElseIf category_filter = "2" Then 'Semalam
                tarikhQuery = " AND CAST(A.Tkh_Mula AS DATE) = CAST(DATEADD(day, -1, CURRENT_TIMESTAMP) AS DATE) "
            ElseIf category_filter = "3" Then 'seminggu
                tarikhQuery = " AND A.Tkh_Mula >= DATEADD(day, -7, CURRENT_TIMESTAMP) AND A.Tkh_Mula < CURRENT_TIMESTAMP "
            ElseIf category_filter = "4" Then '30 hari
                tarikhQuery = " AND A.Tkh_Mula >= DATEADD(day, -30, CURRENT_TIMESTAMP) AND A.Tkh_Mula < CURRENT_TIMESTAMP "
            ElseIf category_filter = "5" Then '60 hari
                tarikhQuery = " AND A.Tkh_Mula >= DATEADD(day, -60, CURRENT_TIMESTAMP) AND A.Tkh_Mula < CURRENT_TIMESTAMP "
            ElseIf category_filter = "6" Then 'custom
                tarikhQuery = " AND A.Tkh_Mula >= @tkhMula AND A.Tkh_Mula <= @TkhTamat "
            param = New List(Of SqlParameter)
            param.Add(New SqlParameter("@tkhMula", tkhMula))
            param.Add(New SqlParameter("@TkhTamat", tkhTamat))
        End If

        Dim query As String = "SELECT A.No_Bil as No_Invois,B.Nama_Penghutang,C.Butiran as UrusNiaga,A.Tujuan,FORMAT(A.Tkh_Mohon, 'dd/MM/yyyy') AS TKHMOHON,
                                CASE WHEN Tkh_Mula <> '' THEN FORMAT(Tkh_Mula, 'dd/MM/yyyy') END AS Tkh_Mula,
                                CASE WHEN Tkh_Tamat <> '' THEN FORMAT(Tkh_Tamat, 'dd/MM/yyyy') END AS Tkh_Tamat,Jumlah,Kontrak
                                FROM SMKB_Bil_Hdr A
                                INNER JOIN SMKB_Penghutang_Master B ON A.Kod_Penghutang=B.Kod_Penghutang
                                INNER JOIN SMKB_Kod_Urusniaga C ON A.Kod_Urusniaga=C.Kod_Urusniaga
                                WHERE  A.Status='1' AND A.Kod_Status_Dok='03' AND Kontrak='1' AND Tkh_Tamat>=GETDATE() AND Tkh_Mula<=GETDATE() " & tarikhQuery

        Return db.Read(query)
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

        Dim query As String = "SELECT A.No_Bil,A.Kod_Penghutang,B.Nama_Penghutang,A.Kod_Urusniaga,C.Butiran, A.Kontrak,A.Tujuan,
                                CASE WHEN Tkh_Mula <> '' THEN FORMAT(Tkh_Mula, 'dd/MM/yyyy') END AS Tkh_Mula,
                                CASE WHEN Tkh_Tamat <> '' THEN FORMAT(Tkh_Tamat, 'dd/MM/yyyy') END AS Tkh_Tamat,Jumlah
                                FROM SMKB_Bil_Hdr A
                                INNER JOIN SMKB_Penghutang_Master B ON A.Kod_Penghutang=B.Kod_Penghutang
                                INNER JOIN SMKB_Kod_Urusniaga C ON A.Kod_Urusniaga=C.Kod_Urusniaga
                                WHERE No_Bil = @No_Invois AND A.Status='1'"

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
    Public Function LoadRecordInvoisTerimaanBayaran(ByVal id As String) As String
        Dim resp As New ResponseRepository

        dt = GetTransaksiInvoisForTerimaanBayaran(id)
        resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    Private Function GetTransaksiInvoisForTerimaanBayaran(kod As String) As DataTable
        Dim db = New DBKewConn

        Dim jumlahBayar As Decimal = GetJumlahByNoBilFromTerimaHdr(kod)

        Dim query As String =
            "DECLARE @Jumlah_Bayar DECIMAL(18, 2) -- Declare the variable to store Jumlah_Bayar
            DECLARE @remaining DECIMAL(18, 2) -- Declare the variable for remaining
            SET @Jumlah_Bayar = @jumlahBayar -- Set the initial value
            SET @remaining = @Jumlah_Bayar; -- Initialize remaining with the initial value

            -- Create a temporary table to store the intermediate results
            CREATE TABLE #TempResults (
                dataid NVARCHAR(MAX),
                colhidkw NVARCHAR(MAX),
                colKW NVARCHAR(MAX),
                colhidko NVARCHAR(MAX),
                colKO NVARCHAR(MAX),
                colhidkp NVARCHAR(MAX),
                colKp NVARCHAR(MAX),
                colhidptj NVARCHAR(MAX),
                ButiranPTJ NVARCHAR(MAX),
                Kod_Vot NVARCHAR(MAX),
                ButiranVot NVARCHAR(MAX),
                Perkara NVARCHAR(MAX),
                Kuantiti DECIMAL(18, 2),
                Kadar_Harga DECIMAL(18, 2),
                Jumlah DECIMAL(18, 2),
                Diskaun DECIMAL(18, 2),
                Cukai DECIMAL(18, 2),
                Jumlah_Bayar DECIMAL(18, 2), -- Add Jumlah_Bayar column
                RowNum INT,
                NoItem INT
            );

            INSERT INTO #TempResults (
                dataid,
                colhidkw,
                colKW,
                colhidko,
                colKO,
                colhidkp,
                colKp,
                colhidptj,
                ButiranPTJ,
                Kod_Vot,
                ButiranVot,
                Perkara,
                Kuantiti,
                Kadar_Harga,
                Jumlah,
                Diskaun,
                Cukai,
                RowNum,
	            NoItem
            )
            SELECT
                a.No_Bil + '|' + a.No_Item AS dataid,
                Kod_Kump_Wang AS colhidkw,
                (SELECT Butiran FROM SMKB_Kump_Wang AS kw WHERE a.Kod_Kump_Wang = kw.Kod_Kump_Wang) AS colKW,
                Kod_Operasi AS colhidko,
                (SELECT Butiran FROM SMKB_Operasi AS ko WHERE a.Kod_Operasi = ko.Kod_Operasi) AS colKO,
                Kod_Projek AS colhidkp,
                (SELECT Butiran FROM SMKB_Projek AS kp WHERE a.Kod_Projek = kp.Kod_Projek) AS colKp,
                Kod_PTJ AS colhidptj,
                (SELECT b.Pejabat FROM [DEVMIS\SQL_INS01].dbStaf.dbo.MS_PEJABAT AS b
                 WHERE b.STATUS = 1 AND b.kodpejabat = LEFT(Kod_PTJ, 2)) AS ButiranPTJ,
                Kod_Vot,
                (SELECT Kod_Vot + ' - ' + Butiran FROM SMKB_Vot AS vot WHERE a.Kod_Vot = vot.Kod_Vot) AS ButiranVot,
                Perkara,
                Kuantiti,
                Kadar_Harga,
                Jumlah,
                Diskaun,
                Cukai,
                ROW_NUMBER() OVER (PARTITION BY a.No_Item ORDER BY a.No_Item) AS RowNum, -- Add a row number for iteration,
                a.No_Item -- Include No_Item in the SELECT list
            FROM SMKB_Bil_Dtl AS a
            LEFT JOIN SMKB_Terima_Hdr AS h ON a.No_Bil = h.No_Rujukan
            WHERE a.No_Bil = @No_Invois
                AND a.status = 1
            GROUP BY
                a.No_Bil + '|' + a.No_Item, -- Use the expression directly in GROUP BY
                Kod_Kump_Wang,
                Kod_Operasi,
                Kod_Projek,
                Kod_PTJ,
                Kod_Vot,
                Perkara,
                Kuantiti,
                Kadar_Harga,
                Jumlah,
                Diskaun,
                Cukai,
                a.No_Item; -- Include No_Item in GROUP BY

            -- Declare a cursor to loop through the rows
            DECLARE @dataid NVARCHAR(MAX);
            DECLARE @payment DECIMAL(18, 2);

            DECLARE paymentCursor CURSOR FOR
            SELECT dataid, Jumlah
            FROM #TempResults
            ORDER BY RowNum;

            -- Open the cursor
            OPEN paymentCursor;

            -- Fetch the first row
            FETCH NEXT FROM paymentCursor INTO @dataid, @payment;

            -- Loop through the rows and update Jumlah_Bayar and @remaining
            WHILE @@FETCH_STATUS = 0
            BEGIN
                IF @remaining >= @payment
                BEGIN
                    UPDATE #TempResults
                    SET Jumlah_Bayar = @payment
                    WHERE dataid = @dataid;

                    SET @remaining = @remaining - @payment;
                END
                ELSE
                BEGIN
                    IF @remaining > 0
                    BEGIN
                        UPDATE #TempResults
                        SET Jumlah_Bayar = @remaining
                        WHERE dataid = @dataid;

                        SET @remaining = 0.00;
                    END
                    ELSE
                    BEGIN
                        UPDATE #TempResults
                        SET Jumlah_Bayar = 0.00
                        WHERE dataid = @dataid;
                    END
                END

                -- Fetch the next row
                FETCH NEXT FROM paymentCursor INTO @dataid, @payment;
            END

            -- Close and deallocate the cursor
            CLOSE paymentCursor;
            DEALLOCATE paymentCursor;

            -- Select the final results
            SELECT
                dataid,
                colhidkw,
                colKW,
                colhidko,
                colKO,
                colhidkp,
                colKp,
                colhidptj,
                ButiranPTJ,
                Kod_Vot,
                ButiranVot,
                Perkara,
                Kuantiti,
                Kadar_Harga,
                Jumlah,
                Jumlah_Bayar,
                Diskaun,
                Cukai
            FROM #TempResults;

            -- Drop the temporary table
            DROP TABLE #TempResults;"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@jumlahBayar", jumlahBayar))
        param.Add(New SqlParameter("@No_Invois", kod))

        Return db.Read(query, param)
    End Function

    Public Function GetJumlahByNoBilFromTerimaHdr(noBil As String) As Decimal
        Dim db = New DBKewConn

        ' Create a SQL command for selecting No_Bil
        Dim selectQuery As String = "SELECT SUM(Jumlah_Bayar) as Jumlah FROM SMKB_Terima_Hdr WHERE No_Rujukan=@nobil"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@nobil", noBil))

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
        Dim query As String = "SELECT TOP 10 CONCAT(a.Kod_Vot, ' - ', vot.Butiran, ', ', a.Kod_Operasi, ' - ', ko.Butiran, ', ', a.Kod_Projek, ', ', a.Kod_Kump_Wang, ' - ', REPLACE(kw.Butiran, 'KUMPULAN WANG', 'KW'), ', ', a.Kod_PTJ) AS text,
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

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Async Function SaveLulus(order As Order_inv) As Tasks.Task(Of String)
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
                Dim kodptj = orderDetail.ddlPTJ
                Dim kodvot = orderDetail.ddlVot
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("ms-MY")
                Dim parsedDate As Date = CDate(Now()).ToString("yyyy-MM-dd")
                Dim vBulan As String = parsedDate.Month
                Dim vTahun As String = parsedDate.Year
                Dim values As String = Await servicex.SendDataLejar(myGetTicket.GetTicket("smkb", Session("ssusrID")),
                                        "GL", "UTeM", kodkw, kodptj,
                                        kodvot, kodko, kodkp, order.Jumlah, "CR", vBulan, vTahun)

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
                                        kodvot, kodko, kodkp, order.Jumlah, "DR", vBulan, vTahun)

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
                                        kodvot, kodko, kodkp, order.Jumlah, "DR", vBulan, vTahun)

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

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function RejectLulus(order As Order_inv) As String
        Dim resp As New ResponseRepository
        resp.Success("Data telah diluluskan")
        Dim success As Integer = 0
        Dim JumRekod As Integer = 0
        If order Is Nothing Then
            resp.Failed("Tiada simpan")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        If UpdateRejectOrder(order.OrderID) <> "OK" Then

            resp.Failed("Rekod Gagal disimpan")
            Return JsonConvert.SerializeObject(resp.GetResult())
            Exit Function
        Else

            'If UpdateLulusOrderDetail(order.OrderID) <> "OK" Then

            '    resp.Failed("Gagal Menyimpan order YX")
            '    Return JsonConvert.SerializeObject(resp.GetResult())
            '    ' Exit Function
            'Else
            '    'If UpdateStatusDokOrder(order.OrderID, "Y") <> "OK" Then

            '    '    resp.Failed("Gagal Menyimpan order CV")
            '    '    Return JsonConvert.SerializeObject(resp.GetResult())
            '    '    ' Exit Function

            '    'End If
            'End If

            'success += 1

        End If

        'For Each orderDetail As OrderDetail In order.OrderDetails

        '    If orderDetail.ddlPTJ = "" Then
        '        Continue For
        '    End If

        '    If orderDetail.Cukai Is Nothing Then
        '        orderDetail.Cukai = 0.00
        '    End If

        '    If orderDetail.Diskaun Is Nothing Then
        '        orderDetail.Diskaun = 0.00
        '    End If

        '    JumRekod += 1

        '    orderDetail.amount = orderDetail.quantity * orderDetail.price 'This can be automated inside orderdetail model

        '    'If orderDetail.id = "" Then
        '    '    orderDetail.id = GenerateOrderDetailID(order.OrderID)
        '    '    orderDetail.OrderID = order.OrderID
        '    '    If InsertOrderDetail(orderDetail) = "OK" Then
        '    '        success += 1
        '    '    End If
        '    'Else
        '    If UpdateLulusOrderDetail(orderDetail) >= 1 Then
        '        If updatelejaram(orderDetail) >= 1 Then
        '            success += 1
        '        End If

        '    End If
        '    'End If
        'Next


        'If success = 0 Then
        '    resp.Failed("Rekod order detail gagal disimpan")
        '    Return JsonConvert.SerializeObject(resp.GetResult())

        'Else
        resp.Success("Rekod berjaya disimpan", "00", order)
        Return JsonConvert.SerializeObject(resp.GetResult())
        'End If




        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function


    Private Function UpdateLulusOrder(orderid As Order_inv)
        Dim db As New DBKewConn

        Dim kodstatusLulus As String = "03" 'Kelulusan Bil

        Dim query As String = "UPDATE SMKB_Bil_Hdr
                                SET Flag_Lulus='1', Kod_Status_Dok=@kodstatus, Tkh_Lulus=@tarikh, Jumlah=@jumlah
                                WHERE No_Bil=@id"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@id", orderid.OrderID))
        param.Add(New SqlParameter("@kodStatus", kodstatusLulus))
        param.Add(New SqlParameter("@tarikh", Now()))
        param.Add(New SqlParameter("@jumlah", orderid.Jumlah))

        Return db.Process(query, param)

    End Function

    Private Function UpdateRejectOrder(orderid As String)
        Dim db As New DBKewConn

        Dim kodstatusLulus As String = "07" 'Kelulusan Bil

        Dim query As String = "UPDATE SMKB_Bil_Hdr
                                SET Flag_Lulus='1', Kod_Status_Dok=@kodstatus
                                WHERE No_Bil=@id"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@id", orderid))
        param.Add(New SqlParameter("@kodStatus", kodstatusLulus))

        Return db.Process(query, param)

    End Function


    Private Function UpdateLulusOrderDetail(orderid As OrderDetail_inv)
        Dim db As New DBKewConn

        Dim success As Integer = 0
        Dim kodpenghutang = orderid.PenghutangID
        Dim nobil = orderid.OrderID
        Dim kodkw = orderid.ddlKw
        Dim kodko = orderid.ddlKo
        Dim kodkp = orderid.ddlKp
        Dim kodptj = orderid.ddlPTJ
        Dim kodvot = orderid.ddlVot
        Dim bulan_kredit = Month(Now())
        Dim tahun_kredit = Year(Now())

        Dim param As New List(Of SqlParameter)

        Dim query As String = "SELECT Kod_Penghutang,Kod_Kump_Wang,Kod_Operasi,Kod_Projek,Kod_PTJ,Kod_Vot,
                                '0.00' AS Debit_1 , 
                                CASE WHEN @bulankredit = 1 THEN SUM(Cr_1) ELSE 0 END AS Credit_1 ,
                                '0.00' AS  Debit_2 , 
                                CASE WHEN @bulankredit = 2 THEN SUM(Cr_2) ELSE 0 END AS Credit_2 ,
                                '0.00' AS  Debit_3 , 
                                CASE WHEN @bulankredit = 3 THEN SUM(Cr_3) ELSE 0 END AS Credit_3 ,
                                '0.00' AS  Debit_4 , 
                                CASE WHEN @bulankredit = 4 THEN SUM(Cr_4) ELSE 0 END AS Credit_4 ,
                                '0.00' AS  Debit_5 , 
                                CASE WHEN @bulankredit = 5 THEN SUM(Cr_5) ELSE 0 END AS Credit_5 ,
                                '0.00' AS  Debit_6 , 
                                CASE WHEN @bulankredit = 6 THEN SUM(Cr_6) ELSE 0 END AS Credit_6 ,
                                '0.00' AS  Debit_7 , 
                                CASE WHEN @bulankredit = 7 THEN SUM(Cr_7) ELSE 0 END AS Credit_7 ,
                                '0.00' AS Debit_8 , 
                                CASE WHEN @bulankredit = 8 THEN SUM(Cr_8) ELSE 0 END AS Credit_8 ,
                                '0.00' AS Debit_9 , 
                                CASE WHEN @bulankredit = 9 THEN SUM(Cr_9) ELSE 0 END AS Credit_9 ,
                                '0.00' AS Debit_10 , 
                                CASE WHEN @bulankredit = 10 THEN SUM(Cr_10) ELSE 0 END AS Credit_10 ,
                                '0.00' AS Debit_11 , 
                                CASE WHEN @bulankredit = 11 THEN SUM(Cr_11) ELSE 0 END AS Credit_11 ,
                                '0.00' AS Debit_12 , 
                                CASE WHEN @bulankredit = 12 THEN SUM(Cr_12) ELSE 0 END AS Credit_12 ,
                                '0.00' AS Debit_13 , 
                                CASE WHEN @bulankredit = 13 THEN SUM(Cr_13) ELSE 0 END AS Credit_13,
                                '1' as status
                                FROM SMKB_Lejar_Penghutang 
                                WHERE Kod_Penghutang=@kodpenghutang AND Kod_Kump_Wang=@kw AND Kod_Operasi=@kodko AND Kod_Projek=@kodkp AND Kod_PTJ=@kodptj AND Kod_Vot=@kodvot
                                GROUP BY Kod_Penghutang,Kod_Vot,Kod_PTJ,Kod_Projek,Kod_Operasi,Kod_Kump_Wang"

        param.Add(New SqlParameter("@kodpenghutang", kodpenghutang))
        param.Add(New SqlParameter("@kw", kodkw))
        param.Add(New SqlParameter("@kodko", kodko))
        param.Add(New SqlParameter("@kodkp", kodkp))
        param.Add(New SqlParameter("@kodptj", kodptj))
        param.Add(New SqlParameter("@kodvot", kodvot))
        param.Add(New SqlParameter("@bulankredit", bulan_kredit))

        dt = db.Read(query, param)
        If dt IsNot Nothing Then
            If dt.Rows.Count > 0 Then
                Dim JumlahKredit = 0.00
                If (bulan_kredit = 1) Then
                    JumlahKredit = dt.Rows(0).Item("Credit_1")
                ElseIf (bulan_kredit = 2) Then
                    JumlahKredit = dt.Rows(0).Item("Credit_2")
                ElseIf (bulan_kredit = 3) Then
                    JumlahKredit = dt.Rows(0).Item("Credit_3")
                ElseIf (bulan_kredit = 4) Then
                    JumlahKredit = dt.Rows(0).Item("Credit_4")
                ElseIf (bulan_kredit = 5) Then
                    JumlahKredit = dt.Rows(0).Item("Credit_5")
                ElseIf (bulan_kredit = 6) Then
                    JumlahKredit = dt.Rows(0).Item("Credit_6")
                ElseIf (bulan_kredit = 7) Then
                    JumlahKredit = dt.Rows(0).Item("Credit_7")
                ElseIf (bulan_kredit = 8) Then
                    JumlahKredit = dt.Rows(0).Item("Credit_8")
                ElseIf (bulan_kredit = 9) Then
                    JumlahKredit = dt.Rows(0).Item("Credit_9")
                ElseIf (bulan_kredit = 10) Then
                    JumlahKredit = dt.Rows(0).Item("Credit_10")
                ElseIf (bulan_kredit = 11) Then
                    JumlahKredit = dt.Rows(0).Item("Credit_11")
                ElseIf (bulan_kredit = 12) Then
                    JumlahKredit = dt.Rows(0).Item("Credit_12")
                End If
                'CInt(dt.Rows(0).Item("id")) + 1
                '    End If
                'If updatelejarpenghutang(orderid, JumlahKredit) = "OK" Then
                '    success += 1
                '    'If updatelejaram() <> "OK" Then

                '    'End If
                'Else
                '    'If updatelejaram(orderid) <> "OK" Then

                '    'End If
                'End If
            Else
                'If insertlejarpenghutang(orderid) = "OK" Then
                '    success += 1
                'End If
            End If
        Else
            'If insertlejarpenghutang(orderid) <> "OK" Then

            'End If
        End If

        'newOrderID = orderid + "SUBORD" + Right("0" + CStr(lastID), 2)
        'newOrderID = lastID
        Return success

    End Function

    Private Function updatelejarHUTANG(orderid As String, pid As String, jum As String)
        Dim db As New DBKewConn

        Dim success As Integer = 0
        Dim kodpenghutang = pid
        Dim nobil = orderid
        Dim kodkw = "07"
        Dim kodko = "01"
        Dim kodkp = "0000000"
        Dim kodptj = "500000"
        Dim kodvot = "71901"
        Dim bulan_kredit = Month(Now())
        Dim tahun_kredit = Year(Now())

        Dim param As New List(Of SqlParameter)

        Dim query As String = "SELECT Kod_Penghutang,Kod_Kump_Wang,Kod_Operasi,Kod_Projek,Kod_PTJ,Kod_Vot,
                                '0.00' AS Debit_1 , 
                                CASE WHEN @bulankredit = 1 THEN SUM(Cr_1) ELSE 0 END AS Credit_1 ,
                                '0.00' AS  Debit_2 , 
                                CASE WHEN @bulankredit = 2 THEN SUM(Cr_2) ELSE 0 END AS Credit_2 ,
                                '0.00' AS  Debit_3 , 
                                CASE WHEN @bulankredit = 3 THEN SUM(Cr_3) ELSE 0 END AS Credit_3 ,
                                '0.00' AS  Debit_4 , 
                                CASE WHEN @bulankredit = 4 THEN SUM(Cr_4) ELSE 0 END AS Credit_4 ,
                                '0.00' AS  Debit_5 , 
                                CASE WHEN @bulankredit = 5 THEN SUM(Cr_5) ELSE 0 END AS Credit_5 ,
                                '0.00' AS  Debit_6 , 
                                CASE WHEN @bulankredit = 6 THEN SUM(Cr_6) ELSE 0 END AS Credit_6 ,
                                '0.00' AS  Debit_7 , 
                                CASE WHEN @bulankredit = 7 THEN SUM(Cr_7) ELSE 0 END AS Credit_7 ,
                                '0.00' AS Debit_8 , 
                                CASE WHEN @bulankredit = 8 THEN SUM(Cr_8) ELSE 0 END AS Credit_8 ,
                                '0.00' AS Debit_9 , 
                                CASE WHEN @bulankredit = 9 THEN SUM(Cr_9) ELSE 0 END AS Credit_9 ,
                                '0.00' AS Debit_10 , 
                                CASE WHEN @bulankredit = 10 THEN SUM(Cr_10) ELSE 0 END AS Credit_10 ,
                                '0.00' AS Debit_11 , 
                                CASE WHEN @bulankredit = 11 THEN SUM(Cr_11) ELSE 0 END AS Credit_11 ,
                                '0.00' AS Debit_12 , 
                                CASE WHEN @bulankredit = 12 THEN SUM(Cr_12) ELSE 0 END AS Credit_12 ,
                                '0.00' AS Debit_13 , 
                                CASE WHEN @bulankredit = 13 THEN SUM(Cr_13) ELSE 0 END AS Credit_13,
                                '1' as status
                                FROM SMKB_Lejar_Penghutang 
                                WHERE Kod_Penghutang=@kodpenghutang AND Kod_Kump_Wang=@kw AND Kod_Operasi=@kodko AND Kod_Projek=@kodkp AND Kod_PTJ=@kodptj AND Kod_Vot=@kodvot
                                GROUP BY Kod_Penghutang,Kod_Vot,Kod_PTJ,Kod_Projek,Kod_Operasi,Kod_Kump_Wang"

        param.Add(New SqlParameter("@kodpenghutang", kodpenghutang))
        param.Add(New SqlParameter("@kw", kodkw))
        param.Add(New SqlParameter("@kodko", kodko))
        param.Add(New SqlParameter("@kodkp", kodkp))
        param.Add(New SqlParameter("@kodptj", kodptj))
        param.Add(New SqlParameter("@kodvot", kodvot))
        param.Add(New SqlParameter("@bulankredit", bulan_kredit))

        dt = db.Read(query, param)
        If dt IsNot Nothing Then
            If dt.Rows.Count > 0 Then
                Dim JumlahKredit = 0.00
                If (bulan_kredit = 1) Then
                    JumlahKredit = dt.Rows(0).Item("Credit_1")
                ElseIf (bulan_kredit = 2) Then
                    JumlahKredit = dt.Rows(0).Item("Credit_2")
                ElseIf (bulan_kredit = 3) Then
                    JumlahKredit = dt.Rows(0).Item("Credit_3")
                ElseIf (bulan_kredit = 4) Then
                    JumlahKredit = dt.Rows(0).Item("Credit_4")
                ElseIf (bulan_kredit = 5) Then
                    JumlahKredit = dt.Rows(0).Item("Credit_5")
                ElseIf (bulan_kredit = 6) Then
                    JumlahKredit = dt.Rows(0).Item("Credit_6")
                ElseIf (bulan_kredit = 7) Then
                    JumlahKredit = dt.Rows(0).Item("Credit_7")
                ElseIf (bulan_kredit = 8) Then
                    JumlahKredit = dt.Rows(0).Item("Credit_8")
                ElseIf (bulan_kredit = 9) Then
                    JumlahKredit = dt.Rows(0).Item("Credit_9")
                ElseIf (bulan_kredit = 10) Then
                    JumlahKredit = dt.Rows(0).Item("Credit_10")
                ElseIf (bulan_kredit = 11) Then
                    JumlahKredit = dt.Rows(0).Item("Credit_11")
                ElseIf (bulan_kredit = 12) Then
                    JumlahKredit = dt.Rows(0).Item("Credit_12")
                End If
                'CInt(dt.Rows(0).Item("id")) + 1
                '    End If
                If updatelejarpenghutang(nobil, kodpenghutang, jum, JumlahKredit) = "OK" Then
                    success += 1
                    'If updatelejaram() <> "OK" Then

                    'End If
                Else
                    'If updatelejaram(orderid) <> "OK" Then

                    'End If
                End If
            Else
                If insertlejarpenghutang(nobil, kodpenghutang, jum) = "OK" Then
                    success += 1
                End If
            End If
        Else
            If insertlejarpenghutang(nobil, kodpenghutang, jum) <> "OK" Then

            End If
        End If

        'newOrderID = orderid + "SUBORD" + Right("0" + CStr(lastID), 2)
        'newOrderID = lastID
        Return success

    End Function


    Private Function updatelejaram(orderid As OrderDetail_inv)
        Dim db As New DBKewConn

        Dim success As Integer = 0
        'Dim kodpenghutang = orderid.PenghutangID
        'Dim nobil = orderid.OrderID
        Dim kodkw = orderid.ddlKw
        Dim kodko = orderid.ddlKo
        Dim kodkp = orderid.ddlKp
        Dim kodptj = orderid.ddlPTJ
        Dim kodvot = orderid.ddlVot
        Dim bulan_kredit = Month(Now())
        Dim tahun_kredit = Year(Now())

        Dim param As New List(Of SqlParameter)

        Dim query As String = "SELECT Kod_Kump_Wang,Kod_Operasi,Kod_Projek,Kod_PTJ,Kod_Vot,
                                '0.00' AS Debit_1 , 
                                CASE WHEN @bulankredit = 1 THEN SUM(Cr_1) ELSE 0 END AS Credit_1 ,
                                '0.00' AS  Debit_2 , 
                                CASE WHEN @bulankredit = 2 THEN SUM(Cr_2) ELSE 0 END AS Credit_2 ,
                                '0.00' AS  Debit_3 , 
                                CASE WHEN @bulankredit = 3 THEN SUM(Cr_3) ELSE 0 END AS Credit_3 ,
                                '0.00' AS  Debit_4 , 
                                CASE WHEN @bulankredit = 4 THEN SUM(Cr_4) ELSE 0 END AS Credit_4 ,
                                '0.00' AS  Debit_5 , 
                                CASE WHEN @bulankredit = 5 THEN SUM(Cr_5) ELSE 0 END AS Credit_5 ,
                                '0.00' AS  Debit_6 , 
                                CASE WHEN @bulankredit = 6 THEN SUM(Cr_6) ELSE 0 END AS Credit_6 ,
                                '0.00' AS  Debit_7 , 
                                CASE WHEN @bulankredit = 7 THEN SUM(Cr_7) ELSE 0 END AS Credit_7 ,
                                '0.00' AS Debit_8 , 
                                CASE WHEN @bulankredit = 8 THEN SUM(Cr_8) ELSE 0 END AS Credit_8 ,
                                '0.00' AS Debit_9 , 
                                CASE WHEN @bulankredit = 9 THEN SUM(Cr_9) ELSE 0 END AS Credit_9 ,
                                '0.00' AS Debit_10 , 
                                CASE WHEN @bulankredit = 10 THEN SUM(Cr_10) ELSE 0 END AS Credit_10 ,
                                '0.00' AS Debit_11 , 
                                CASE WHEN @bulankredit = 11 THEN SUM(Cr_11) ELSE 0 END AS Credit_11 ,
                                '0.00' AS Debit_12 , 
                                CASE WHEN @bulankredit = 12 THEN SUM(Cr_12) ELSE 0 END AS Credit_12 ,
                                '0.00' AS Debit_13 , 
                                CASE WHEN @bulankredit = 13 THEN SUM(Cr_13) ELSE 0 END AS Credit_13,
                                '1' as status
                                FROM SMKB_Lejar_Am
                                WHERE Kod_Kump_Wang=@kw AND Kod_Operasi=@kodko AND Kod_Projek=@kodkp AND Kod_PTJ=@kodptj AND Kod_Vot=@kodvot
                                GROUP BY Kod_Vot,Kod_PTJ,Kod_Projek,Kod_Operasi,Kod_Kump_Wang"

        param.Add(New SqlParameter("@kw", kodkw))
        param.Add(New SqlParameter("@kodko", kodko))
        param.Add(New SqlParameter("@kodkp", kodkp))
        param.Add(New SqlParameter("@kodptj", kodptj))
        param.Add(New SqlParameter("@kodvot", kodvot))
        param.Add(New SqlParameter("@bulankredit", bulan_kredit))

        dt = db.Read(query, param)
        If dt IsNot Nothing Then
            If dt.Rows.Count > 0 Then
                Dim JumlahKredit = 0.00
                If (bulan_kredit = 1) Then
                    JumlahKredit = dt.Rows(0).Item("Credit_1")
                ElseIf (bulan_kredit = 2) Then
                    JumlahKredit = dt.Rows(0).Item("Credit_2")
                ElseIf (bulan_kredit = 3) Then
                    JumlahKredit = dt.Rows(0).Item("Credit_3")
                ElseIf (bulan_kredit = 4) Then
                    JumlahKredit = dt.Rows(0).Item("Credit_4")
                ElseIf (bulan_kredit = 5) Then
                    JumlahKredit = dt.Rows(0).Item("Credit_5")
                ElseIf (bulan_kredit = 6) Then
                    JumlahKredit = dt.Rows(0).Item("Credit_6")
                ElseIf (bulan_kredit = 7) Then
                    JumlahKredit = dt.Rows(0).Item("Credit_7")
                ElseIf (bulan_kredit = 8) Then
                    JumlahKredit = dt.Rows(0).Item("Credit_8")
                ElseIf (bulan_kredit = 9) Then
                    JumlahKredit = dt.Rows(0).Item("Credit_9")
                ElseIf (bulan_kredit = 10) Then
                    JumlahKredit = dt.Rows(0).Item("Credit_10")
                ElseIf (bulan_kredit = 11) Then
                    JumlahKredit = dt.Rows(0).Item("Credit_11")
                ElseIf (bulan_kredit = 12) Then
                    JumlahKredit = dt.Rows(0).Item("Credit_12")
                End If
                'CInt(dt.Rows(0).Item("id")) + 1
                '    End If
                If updatelejaram_details(orderid, JumlahKredit) = "OK" Then
                    success += 1
                    'If updatelejaram() <> "OK" Then

                    'End If
                Else
                    'If updatelejaram(orderid) <> "OK" Then

                    'End If
                End If
            Else
                If insertlejaram_details(orderid) = "OK" Then
                    success += 1
                End If
            End If
        Else
            If insertlejaram_details(orderid) <> "OK" Then

            End If
        End If

        'newOrderID = orderid + "SUBORD" + Right("0" + CStr(lastID), 2)
        'newOrderID = lastID
        Return success

    End Function

    Private Function updatelejarpenghutang(orderid As String, pid As String, jum As String, JumlahKredit As String)
        Dim db As New DBKewConn
        Dim bulan_kredit = Month(Now())
        Dim tahun_kredit = Year(Now())
        'Dim noitem = orderid.id
        Dim KREDIT = JumlahKredit
        KREDIT = KREDIT + jum
        Dim Data = Split(orderid, "|")
        Dim nobil = Data(0)
        'Dim noitem = Data(1)
        Dim query1 As String = "UPDATE LP
                                SET LP.Cr_1 =
                                    CASE WHEN @bulankredit=1 THEN  @KREDIT ELSE LP.Cr_1 END,
                                LP.Cr_2 = 
	                                CASE WHEN @bulankredit=2 THEN  @KREDIT ELSE LP.Cr_2 END,
                                LP.Cr_3 = 
	                                CASE WHEN @bulankredit=3 THEN  @KREDIT ELSE LP.Cr_3 END,
                                LP.Cr_4 = 
	                                CASE WHEN @bulankredit=4 THEN  @KREDIT ELSE LP.Cr_4 END,
                                LP.Cr_5 = 
	                                CASE WHEN @bulankredit=5 THEN  @KREDIT ELSE LP.Cr_5 END,
                                LP.Cr_6 = 
	                                CASE WHEN @bulankredit=6 THEN  @KREDIT ELSE LP.Cr_6 END,
                                LP.Cr_7 = 
	                                CASE WHEN @bulankredit=7 THEN  @KREDIT ELSE LP.Cr_7 END,
                                LP.Cr_8 = 
	                                CASE WHEN @bulankredit=8 THEN  @KREDIT ELSE LP.Cr_8 END,
                                LP.Cr_9 = 
	                                CASE WHEN @bulankredit=9 THEN  @KREDIT ELSE LP.Cr_9 END,
                                LP.Cr_10 = 
	                                CASE WHEN @bulankredit=10 THEN  @KREDIT ELSE LP.Cr_10 END,
                                LP.Cr_11 = 
	                                CASE WHEN @bulankredit=11 THEN  @KREDIT ELSE LP.Cr_11 END,
                                LP.Cr_12 = 
	                                CASE WHEN @bulankredit=12 THEN  @KREDIT ELSE LP.Cr_12 END
                                FROM SMKB_Lejar_Penghutang LP
								WHERE LP.Kod_Kump_Wang=@kodkw AND LP.Kod_Operasi=@kodko AND LP.Kod_Projek=@kodkp AND LP.Kod_PTJ=@kodptj AND LP.Kod_Vot=@kodvot
                                AND LP.Tahun=@tahun"
        Dim param1 As New List(Of SqlParameter)
        param1.Add(New SqlParameter("@bulankredit", bulan_kredit))
        param1.Add(New SqlParameter("@nobil", nobil))
        param1.Add(New SqlParameter("@kodpenghutang", pid))
        param1.Add(New SqlParameter("@kodkw", "07"))
        param1.Add(New SqlParameter("@kodko", "01"))
        param1.Add(New SqlParameter("@kodkp", "0000000"))
        param1.Add(New SqlParameter("@kodptj", "500000"))
        param1.Add(New SqlParameter("@kodvot", "71901"))
        param1.Add(New SqlParameter("@tahun", tahun_kredit))
        param1.Add(New SqlParameter("@KREDIT", KREDIT))
        Return db.Process(query1, param1)
    End Function
    Private Function updatelejaram_details(orderid As OrderDetail_inv, JumlahKredit As String)
        Dim db As New DBKewConn
        Dim bulan_kredit = Month(Now())
        Dim tahun_kredit = Year(Now())
        'Dim noitem = orderid.id
        Dim KREDIT = JumlahKredit
        KREDIT = KREDIT + orderid.amount
        Dim Data = Split(orderid.id, "|")
        Dim nobil = Data(0)
        Dim noitem = Data(1)
        Dim query1 As String = "UPDATE LA
                                SET LA.Cr_1 =
                                CASE WHEN @bulankredit=1 THEN  5.00 ELSE LA.Cr_1 END,
                                LA.Cr_2 = 
                                CASE WHEN @bulankredit=2 THEN  5.00 ELSE LA.Cr_2 END,
                                LA.Cr_3 = 
                                CASE WHEN @bulankredit=3 THEN  5.00 ELSE LA.Cr_3 END,
                                LA.Cr_4 = 
                                CASE WHEN @bulankredit=4 THEN  5.00 ELSE LA.Cr_4 END,
                                LA.Cr_5 = 
                                CASE WHEN @bulankredit=5 THEN  5.00 ELSE LA.Cr_5 END,
                                LA.Cr_6 = 
                                CASE WHEN @bulankredit=6 THEN  5.00 ELSE LA.Cr_6 END,
                                LA.Cr_7 = 
                                CASE WHEN @bulankredit=7 THEN  5.00 ELSE LA.Cr_7 END,
                                LA.Cr_8 = 
                                CASE WHEN @bulankredit=8 THEN  5.00 ELSE LA.Cr_8 END,
                                LA.Cr_9 = 
                                CASE WHEN @bulankredit=9 THEN  5.00 ELSE LA.Cr_9 END,
                                LA.Cr_10 = 
                                CASE WHEN @bulankredit=10 THEN  5.00 ELSE LA.Cr_10 END,
                                LA.Cr_11 = 
                                CASE WHEN @bulankredit=11 THEN  5.00 ELSE LA.Cr_11 END,
                                LA.Cr_12 = 
                                CASE WHEN @bulankredit=12 THEN  5.00 ELSE LA.Cr_12 END
                                FROM SMKB_Lejar_Penghutang LA
                                INNER JOIN
                                SMKB_Bil_Hdr BIL
                                ON LA.Kod_Penghutang = BIL.Kod_Penghutang
                                INNER JOIN 
                                SMKB_Bil_Dtl BIL_D
                                ON BIL.No_Bil = BIL_D.No_Bil AND No_Item=@noitem
                                WHERE BIL.No_Bil=@nobil
                                AND LA.Kod_Kump_Wang=@kodkw AND LA.Kod_Operasi=@kodko AND LA.Kod_Projek=@kodkp AND LA.Kod_PTJ=@kodptj AND LA.Kod_Vot=@kodvot
                                AND LA.Tahun=@tahun "
        Dim param1 As New List(Of SqlParameter)
        param1.Add(New SqlParameter("@bulankredit", bulan_kredit))
        param1.Add(New SqlParameter("@noitem", noitem))
        param1.Add(New SqlParameter("@nobil", nobil))
        param1.Add(New SqlParameter("@kodpenghutang", orderid.PenghutangID))
        param1.Add(New SqlParameter("@kodkw", orderid.ddlKw))
        param1.Add(New SqlParameter("@kodko", orderid.ddlKo))
        param1.Add(New SqlParameter("@kodkp", orderid.ddlKp))
        param1.Add(New SqlParameter("@kodptj", orderid.ddlPTJ))
        param1.Add(New SqlParameter("@kodvot", orderid.ddlVot))
        param1.Add(New SqlParameter("@tahun", tahun_kredit))
        param1.Add(New SqlParameter("@KREDIT", KREDIT))
        Return db.Process(query1, param1)
    End Function
    Private Function insertlejarpenghutang(orderid As String, pid As String, jum As String)
        Dim db As New DBKewConn
        Dim bulan_kredit = Month(Now())
        Dim tahun_kredit = Year(Now())
        'Dim noitem = orderid.id
        Dim KREDIT = jum
        Dim Data = Split(orderid, "|")
        Dim nobil = Data(0)
        'Dim noitem = Data(1)
        Dim query1 As String = "INSERT INTO SMKB_Lejar_Penghutang 
                                SELECT @kodpenghutang , @tahun , @kodkw, @kodko,@kodkp,@kodptj,@kodvot,
                                '0.00' AS Debit_1 , 
                                CASE WHEN @bulankredit = 1 THEN @KREDIT ELSE 0.00 END AS Credit_1 ,
                                '0.00' AS  Debit_2 , 
                                CASE WHEN @bulankredit = 2 THEN @KREDIT ELSE 0 END AS Credit_2 ,
                                '0.00' AS  Debit_3 , 
                                CASE WHEN @bulankredit = 3 THEN @KREDIT ELSE 0 END AS Credit_3 ,
                                '0.00' AS  Debit_4 , 
                                CASE WHEN @bulankredit = 4 THEN @KREDIT ELSE 0 END AS Credit_4 ,
                                '0.00' AS  Debit_5 , 
                                CASE WHEN @bulankredit = 5 THEN @KREDIT ELSE 0 END AS Credit_5 ,
                                '0.00' AS  Debit_6 , 
                                CASE WHEN @bulankredit = 6 THEN @KREDIT ELSE 0 END AS Credit_6 ,
                                '0.00' AS  Debit_7 , 
                                CASE WHEN @bulankredit = 7 THEN @KREDIT ELSE 0 END AS Credit_7 ,
                                '0.00' AS Debit_8 , 
                                CASE WHEN @bulankredit = 8 THEN @KREDIT ELSE 0 END AS Credit_8 ,
                                '0.00' AS Debit_9 , 
                                CASE WHEN @bulankredit = 9 THEN @KREDIT ELSE 0 END AS Credit_9 ,
                                '0.00' AS Debit_10 , 
                                CASE WHEN @bulankredit = 10 THEN @KREDIT ELSE 0 END AS Credit_10 ,
                                '0.00' AS Debit_11 , 
                                CASE WHEN @bulankredit = 11 THEN @KREDIT ELSE 0 END AS Credit_11 ,
                                '0.00' AS Debit_12 , 
                                CASE WHEN @bulankredit = 12 THEN @KREDIT ELSE 0 END AS Credit_12 ,
                                '0.00' AS Debit_13 , 
                                CASE WHEN @bulankredit = 13 THEN @KREDIT ELSE 0 END AS Credit_13,
                                '1' as status
                                FROM SMKB_Bil_Hdr
                                WHERE No_Bil=@nobil"
        Dim param1 As New List(Of SqlParameter)
        param1.Add(New SqlParameter("@bulankredit", bulan_kredit))
        'param1.Add(New SqlParameter("@noitem", noitem))
        param1.Add(New SqlParameter("@nobil", nobil))
        param1.Add(New SqlParameter("@kodpenghutang", pid))
        param1.Add(New SqlParameter("@kodkw", "07"))
        param1.Add(New SqlParameter("@kodko", "01"))
        param1.Add(New SqlParameter("@kodkp", "0000000"))
        param1.Add(New SqlParameter("@kodptj", "500000"))
        param1.Add(New SqlParameter("@kodvot", "71901"))
        param1.Add(New SqlParameter("@tahun", tahun_kredit))
        param1.Add(New SqlParameter("@KREDIT", KREDIT))
        Return db.Process(query1, param1)
    End Function

    Private Function insertlejaram_details(orderid As OrderDetail_inv)
        Dim db As New DBKewConn
        Dim bulan_kredit = Month(Now())
        Dim tahun_kredit = Year(Now())
        'Dim noitem = orderid.id
        Dim KREDIT = orderid.amount
        Dim Data = Split(orderid.id, "|")
        Dim nobil = Data(0)
        Dim noitem = Data(1)
        Dim kodkprt = "UTeM"
        Dim query1 As String = "INSERT INTO SMKB_Lejar_AM 
                                SELECT @tahun , @kodkw, @kodko,@kodptj,@kodvot,@kodkp,@kodkprt,
                                '0.00' AS Debit_1 , 
                                CASE WHEN @bulankredit = 1 THEN @KREDIT ELSE 0.00 END AS Credit_1 ,
                                '0.00' AS  Debit_2 , 
                                CASE WHEN @bulankredit = 2 THEN @KREDIT ELSE 0 END AS Credit_2 ,
                                '0.00' AS  Debit_3 , 
                                CASE WHEN @bulankredit = 3 THEN @KREDIT ELSE 0 END AS Credit_3 ,
                                '0.00' AS  Debit_4 , 
                                CASE WHEN @bulankredit = 4 THEN @KREDIT ELSE 0 END AS Credit_4 ,
                                '0.00' AS  Debit_5 , 
                                CASE WHEN @bulankredit = 5 THEN @KREDIT ELSE 0 END AS Credit_5 ,
                                '0.00' AS  Debit_6 , 
                                CASE WHEN @bulankredit = 6 THEN @KREDIT ELSE 0 END AS Credit_6 ,
                                '0.00' AS  Debit_7 , 
                                CASE WHEN @bulankredit = 7 THEN @KREDIT ELSE 0 END AS Credit_7 ,
                                '0.00' AS Debit_8 , 
                                CASE WHEN @bulankredit = 8 THEN @KREDIT ELSE 0 END AS Credit_8 ,
                                '0.00' AS Debit_9 , 
                                CASE WHEN @bulankredit = 9 THEN @KREDIT ELSE 0 END AS Credit_9 ,
                                '0.00' AS Debit_10 , 
                                CASE WHEN @bulankredit = 10 THEN @KREDIT ELSE 0 END AS Credit_10 ,
                                '0.00' AS Debit_11 , 
                                CASE WHEN @bulankredit = 11 THEN @KREDIT ELSE 0 END AS Credit_11 ,
                                '0.00' AS Debit_12 , 
                                CASE WHEN @bulankredit = 12 THEN @KREDIT ELSE 0 END AS Credit_12 ,
                                '0.00' AS Debit_13 , 
                                CASE WHEN @bulankredit = 13 THEN @KREDIT ELSE 0 END AS Credit_13,
                                '1' as status
                                FROM SMKB_Bil_Dtl
                                WHERE No_Bil=@nobil AND No_Item=@noitem"
        Dim param1 As New List(Of SqlParameter)

        param1.Add(New SqlParameter("@kodkprt", kodkprt))
        param1.Add(New SqlParameter("@bulankredit", bulan_kredit))
        param1.Add(New SqlParameter("@noitem", noitem))
        param1.Add(New SqlParameter("@nobil", nobil))
        param1.Add(New SqlParameter("@kodpenghutang", orderid.PenghutangID))
        param1.Add(New SqlParameter("@kodkw", orderid.ddlKw))
        param1.Add(New SqlParameter("@kodko", orderid.ddlKo))
        param1.Add(New SqlParameter("@kodkp", orderid.ddlKp))
        param1.Add(New SqlParameter("@kodptj", orderid.ddlPTJ))
        param1.Add(New SqlParameter("@kodvot", orderid.ddlVot))
        param1.Add(New SqlParameter("@tahun", tahun_kredit))
        param1.Add(New SqlParameter("@KREDIT", KREDIT))
        Return db.Process(query1, param1)
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

    Private Function Save_Lulus(id As String)
        Dim db As New DBKewConn
        Dim query As String = "UPDATE SMKB_Bil_Hdr
                                SET Flag_Lulus='1', Kod_Status_Dok='03'
                                WHERE No_Bil=@id"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@id", id))

        Return db.Process(query, param)
    End Function

    Private Function update_lejar(id As String)
        Dim db As New DBKewConn
        Dim query1 As String = "SELECT A.Kod_Penghutang,B.Kod_Vot,B.Kod_PTJ,B.Kod_Projek,B.Kod_Operasi,B.Kod_Kump_Wang, SUM(B.Jumlah), MONTH(A.Tkh_Mula) AS BULAN, YEAR(A.Tkh_Mula) AS TAHUN
                                FROM SMKB_Bil_Hdr A
                                INNER JOIN SMKB_Bil_Dtl B ON A.No_Bil=B.No_Bil
                                WHERE A.No_Bil=@id
                                GROUP BY A.Kod_Penghutang,B.Kod_Vot,B.Kod_PTJ,B.Kod_Projek,B.Kod_Operasi,B.Kod_Kump_Wang, B.Jumlah, A.Tkh_Mula "
        Dim param1 As New List(Of SqlParameter)
        param1.Add(New SqlParameter("@id", id))
        Return db.Process(query1, param1)

        Dim query As String = "UPDATE SMKB_Bil_Hdr
                                SET Flag_Lulus='1', Kod_Status_Dok='03'
                                WHERE No_Bil=@id"
        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@id", id))
        Return db.Process(query, param)

    End Function
End Class