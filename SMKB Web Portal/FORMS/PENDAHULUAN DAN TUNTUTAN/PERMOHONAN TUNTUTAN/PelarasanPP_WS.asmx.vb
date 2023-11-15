Imports System.ComponentModel
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports Newtonsoft.Json
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports SMKB_Web_Portal.ItemDetail
Imports System

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class Pelarasan_WS
    Inherits System.Web.Services.WebService

    Dim sqlcmd As SqlCommand
    Dim sqlcon As SqlConnection
    Dim sqlread As SqlDataReader
    Dim dt As DataTable

    <WebMethod()>
    Public Function HelloWorld() As String
        Return "Hello World"
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadOrderRecord_PermohonanSendiri(category_filter As String, isClicked As Boolean, tkhMula As DateTime, tkhTamat As DateTime, staffP As String) As String
        Dim resp As New ResponseRepository

        If isClicked = False Then
            Return JsonConvert.SerializeObject(New DataTable)
        End If

        dt = GetRecord_SenaraiSendiri(category_filter, tkhMula, tkhTamat, staffP)
        resp.SuccessPayload(dt)
        'resp.GetResult()
        Return JsonConvert.SerializeObject(dt)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetRecord_SenaraiSendiri(category_filter As String, tkhMula As DateTime, tkhTamat As DateTime, staffP As String) As DataTable
        Dim db = New DBKewConn
        Dim tarikhQuery As String = ""
        Dim param = New List(Of SqlParameter)

        If category_filter = "1" Then 'Harini
            'tarikhQuery = " and a.Tkh_Transaksi = getdate() "
            tarikhQuery = "and a.No_Staf = @staffP and CAST(a.Tarikh_Mohon AS DATE) = CAST(getdate() AS DATE) "
        ElseIf category_filter = "2" Then 'Semalam
            'tarikhQuery = " and a.Tkh_Transaksi >= DATEADD(day, -2, getdate()) and a.Tkh_Transaksi <= getdate() "
            tarikhQuery = "and a.No_Staf = @staffP and CAST(a.Tarikh_Mohon AS DATE) = CAST(DATEADD(DAY, -1, GETDATE()) AS DATE) "
        ElseIf category_filter = "3" Then 'seminggu
            'tarikhQuery = " and a.Tkh_Transaksi >= DATEADD(day, -8, getdate()) and a.Tkh_Transaksi <= getdate() "
            tarikhQuery = " and a.No_Staf = @staffP  and CAST(a.Tarikh_Mohon AS DATE) >= CAST(DATEADD(DAY, -7, GETDATE()) AS DATE) "
        ElseIf category_filter = "4" Then '30 hari
            tarikhQuery = " and a.No_Staf = @staffP and a.Tarikh_Mohon >= DATEADD(month, -1, getdate()) and a.Tarikh_Mohon <= getdate() "
        ElseIf category_filter = "5" Then '60 hari
            tarikhQuery = " and a.No_Staf = @staffP and a.Tarikh_Mohon >= DATEADD(month, -2, getdate()) and a.Tarikh_Mohon <= getdate() "
        ElseIf category_filter = "6" Then 'custom
            tarikhQuery = " and a.No_Staf = @staffP  and a.Tarikh_Mohon >= @tkhMula and a.Tarikh_Mohon <= @TkhTamat "
            param.Add(New SqlParameter("@tkhMula", tkhMula))
            param.Add(New SqlParameter("@TkhTamat", tkhTamat))
        End If


        Dim query As String = "SELECT a.No_Tuntutan, a.Tujuan_Tuntutan,  a.PTj, a.Bulan_Tuntut, a.Tahun_Tuntut, a.Status, a.No_Pendahuluan,
                FORMAT(a.Tarikh_Mohon, 'yyyy-MM-dd') AS Tarikh_Mohon,  isnull(a.Jum_Pendahuluan,'0.00') as Jum_Pendahuluan,                      
                a.Kod_Kump_Wang, (select Butiran from SMKB_Kump_Wang as kw where a.Kod_Kump_Wang = kw.Kod_Kump_Wang) as colKW,
                a.Kod_Operasi, (select Butiran from SMKB_Operasi as ko where  ko.Kod_Operasi = a.Kod_Operasi) as colKO,
                a.Kod_Projek,  (select Butiran from SMKB_Projek as kp where kp.Kod_Projek = a.Kod_Projek) as colKp,
                a.Kod_PTJ,  (SELECT b.Pejabat FROM [DEVMIS\SQL_INS01].dbStaf.dbo.MS_PEJABAT as b WHERE b.STATUS = 1 and b.kodpejabat = left(a.Kod_PTJ,2)) as ButiranPTJ  ,
                 a.Status_Dok,
                b.Butiran, a.No_Staf + ' - ' + c.ms01_nama   as NamaPemohon, a.No_Staf as Nopemohon
                FROM SMKB_Tuntutan_Hdr as a INNER JOIN 
                SMKB_Kod_Status_Dok AS b ON a.Status_Dok = b.Kod_Status_Dok INNER JOIN
                [qa11].dbStaf.dbo.MS01_Peribadi as c ON a.No_Staf = c.ms01_noStaf
                WHERE  (b.Kod_Modul = '09') AND (b.Kod_Status_Dok = '02') AND (a.Jenis_Tuntutan ='P')  " & tarikhQuery & " order by a.Tarikh_Mohon desc"
        ' WHERE a.Status_Dok='01' AND a.Pengesahan_Pemohon='0' " & tarikhQuery & " order by a.Tarikh_Mohon desc"

        param.Add(New SqlParameter("@staffP", staffP))

        Return db.Read(query, param)
    End Function
    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function fnCariStaf(ByVal q As String) As String

        Dim tmpDT As DataTable = GetListStaf(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetListStaf(kodPjbt As String) As DataTable
        Dim db = New DBSMConn
        kodPjbt = "41"
        Dim query As String = "SELECT  MS01_NoStaf as StafNo, MS01_Nama , MS08_Pejabat, JawGiliran
                    FROM VK_AdvClm "

        Dim param As New List(Of SqlParameter)

        If kodPjbt <> "" Then

            query &= "WHERE MS08_Pejabat = @kodPjbt  AND RIGHT(MS02_GredGajiS,2) >='41' "

            param.Add(New SqlParameter("@kodPjbt", kodPjbt))

        End If

        Return db.Read(query, param)

    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetUserInfo(nostaf As String)
        Dim db As New DBSMConn
        Dim query As String = $"Select a.MS01_NoStaf As StafNo, a.MS01_Nama As Param1, a.MS08_Pejabat As Param2, a.JawGiliran As Param3, a.Kumpulan As Param4, 
                                a.Singkatan as Param5, a.MS02_GredGajiS As Param6, Right(a.MS02_GredGajiS, 2) As GredGaji, 
                                a.MS02_JumlahGajiS, a.MS01_TelPejabat As Param7, a.MS02_Kumpulan, b.KodPBU as KodPejPemohon
                                From VK_AdvClm As a INNER Join MS_PejabatPBU As b On a.MS08_Pejabat = b.KodPejabat  
                                Where a.MS01_NoStaf = '{nostaf}' AND b.StatusPTJ=1"



        Dim dt As DataTable = db.fselectCommandDt(query)
        Return JsonConvert.SerializeObject(dt)

    End Function

    Public Function GetListKumpWang(kod As String) As DataTable

        Dim db As New DBKewConn
        Dim query As String = "SELECT DISTINCT A.Kod_Kump_Wang as value,A.Kod_Kump_Wang +' - '+ B.Butiran as text
                                FROM SMKB_COA_Master A
                                INNER JOIN SMKB_Kump_Wang B ON A.Kod_Kump_Wang=B.Kod_Kump_Wang"
        Dim param As New List(Of SqlParameter)
        If kod <> "" Then
            query &= " where A.Status = 1 and (A.Kod_Kump_Wang LIKE '%' + @kod + '%' or B.Butiran LIKE '%' + @kod2 + '%') and A.Kod_Vot =@kod3 order by A.Kod_Kump_Wang"
            'Else
            '    query &= " where A.Status = 1 and A.Kod_Vot =@kod3 order by A.Kod_Kump_Wang"
        End If

        param.Add(New SqlParameter("@kod", kod))
        param.Add(New SqlParameter("@kod2", kod))
        param.Add(New SqlParameter("@kod3", kod))

        Return db.Read(query, param)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetDataKumpWang(ByVal q As String) As String


        Dim tmpDT As DataTable = GetListKumpWang(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetDataOperasi(ByVal q As String) As String


        Dim tmpDT As DataTable = GetListDataOperasi(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetListDataOperasi(kod As String) As DataTable
        Dim db As New DBKewConn
        Dim query As String = "SELECT DISTINCT A.Kod_Operasi as value, A.Kod_Operasi +' - '+ B.Butiran As text
                                From SMKB_COA_Master A
                                INNER Join SMKB_Operasi B ON A.Kod_Operasi=B.Kod_Operasi"
        Dim param As New List(Of SqlParameter)
        If kod <> "" Then
            query &= " where A.Status = 1 And (A.Kod_Operasi Like '%' + @kod + '%' or B.Butiran LIKE '%' + @kod2 + '%') and A.Kod_Kump_Wang =@kod3 order by A.Kod_Operasi"
            'Else
            '    query &= " where A.Status = 1 and A.Kod_Kump_Wang =@kod3 order by A.Kod_Operasi"
        End If

        param.Add(New SqlParameter("@kod", kod))
        param.Add(New SqlParameter("@kod2", kod))
        param.Add(New SqlParameter("@kod3", kod))

        Return db.Read(query, param)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetDataProjek(ByVal q As String) As String


        Dim tmpDT As DataTable = GetListDataProjek(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetListDataProjek(kod As String) As DataTable
        Dim db As New DBKewConn
        Dim query As String = "SELECT DISTINCT A.Kod_Projek as value, A.Kod_Projek +' - '+ B.Butiran as text FROM SMKB_COA_Master A
                                INNER JOIN SMKB_Projek B ON A.Kod_Projek = B.Kod_Projek"
        Dim param As New List(Of SqlParameter)
        If kod <> "" Then
            query &= " where A.Status = 1 and (A.Kod_Projek LIKE '%' + @kod + '%' or B.Butiran LIKE '%' + @kod2 + '%') and A.Kod_Operasi =@kod3 order by A.Kod_Projek"
            'Else
            '    query &= " where A.Status = 1 and A.Kod_Operasi =@kod3 order by A.Kod_Projek"
        End If

        param.Add(New SqlParameter("@kod", kod))
        param.Add(New SqlParameter("@kod2", kod))
        param.Add(New SqlParameter("@kod3", kod))

        Return db.Read(query, param)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetKodPtj(ByVal q As String) As String
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

    Private Function GetKodPtjList(kod As String) As DataTable
        Dim db As New DBKewConn

        Dim query As String = "Select distinct TOP (20)  Kod_PTJ as value, 
        Kod_PTJ  + ' - ' + (SELECT b.Pejabat FROM [DEVMIS\SQL_INS01].dbStaf.dbo.MS_PEJABAT as b
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

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadRecord_PermohonanPP(staffP As String) As String
        Dim resp As New ResponseRepository

        'If isClicked = False Then
        '    Return JsonConvert.SerializeObject(New DataTable)
        'End If

        dt = GetRecord_SenaraiSendiriPP(staffP)



        'dt = GetRecord_SenaraiSendiri(id)
        resp.SuccessPayload(dt)
        'resp.GetResult()
        Return JsonConvert.SerializeObject(dt)
    End Function

    Private Function GetRecord_SenaraiSendiriPP(staffP As String) As DataTable
        Dim db = New DBKewConn

        Dim query As String = "SELECT  No_Pendahuluan, No_Staf, Jenis_Pendahuluan,Tujuan, Jum_Lulus, isnull(No_Baucar,'-') as No_Baucar
                            FROM  SMKB_Pendahuluan_Hdr
                            WHERE (Jenis_Pendahuluan = 'PP') AND  No_Staf = @staffP "


        Dim param = New List(Of SqlParameter)
        param.Add(New SqlParameter("@staffP", staffP))
        Return db.Read(query, param)
    End Function

    <System.Web.Services.WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SaveRecordTuntutan(listDetail As MhnTuntutan) As String
        Dim resp As New ResponseRepository
        resp.Success("Data telah disimpan")
        Dim success As Integer = 0
        Dim JumRekod As Integer = 0
        If listDetail Is Nothing Then
            resp.Failed("Tiada simpan")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        If listDetail.mohonID = "" Then 'untuk permohonan baru
            listDetail.mohonID = GenerateOrderID()

            If InsertNewOrder(listDetail) <> "OK" Then
                resp.Failed("Gagal Menyimpan order 1266")
                Return JsonConvert.SerializeObject(resp.GetResult())
                ' Exit Function
            End If
        Else 'untuk permohonan sedia ada

            If UpdateNewOrder(listDetail) <> "OK" Then
                'If InsertNewOrder(OtherList) <> "OK" Then
                resp.Failed("Gagal Menyimpan order 1266")
                Return JsonConvert.SerializeObject(resp.GetResult())
                ' Exit Function
                'End If
            End If
        End If


        If UpdateStatusDokOrder_Mohon(listDetail, "Y") <> "OK" Then

            'resp.Failed("Berjaya simpan") 'Gagal Menyimpan order YX
            Return JsonConvert.SerializeObject(resp.GetResult())
            ' Exit Function

        End If


        resp.Success("Rekod berjaya disimpan", "00", listDetail)
        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    Private Function InsertNewOrder(listDetail As MhnTuntutan)
        Dim db As New DBKewConn
        'Dim year = Date.Now.ToString("yyyy")
        'Dim month = Date.Now.Month
        'Dim blnTuntut = month + "/" + year

        Dim query As String = "INSERT INTO  SMKB_Tuntutan_Hdr (No_Tuntutan, No_Staf, PTj, Tarikh_Mohon,Tujuan_Tuntutan, Bulan_Tuntut,Tahun_Tuntut, Jenis_Tuntutan, 
                    No_Pendahuluan, Status_Dok, Status,  Jum_Pendahuluan, Kod_Kump_Wang, Kod_Operasi, Kod_Projek, Kod_PTJ,  ID_Mohon, Pengesahan_Pemohon, Akuan_Pemohon)
                    VALUES (@No_Tuntutan, @No_Staf, @PTj, @Tarikh_Mohon,  @Tujuan, @Bulan_Tuntut, @Tahun_Tuntut, 
                    @Jenis_Tuntutan, @No_Pendahuluan, @Status_Dok, @Status, @Jum_Pendahuluan, @Kod_Kump_Wang, @Kod_Operasi, @Kod_Projek,@Kod_PTJ, @ID_Mohon, @Pengesahan_Pemohon, @Akuan_Pemohon)"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@No_Tuntutan", listDetail.mohonID))
        param.Add(New SqlParameter("@No_Staf", Session("ssusrID")))
        param.Add(New SqlParameter("@PTj", listDetail.PtjMohon))
        param.Add(New SqlParameter("@Tarikh_Mohon", listDetail.TkhMohon))
        param.Add(New SqlParameter("@Tujuan", listDetail.TujuanMohon))
        param.Add(New SqlParameter("@Bulan_Tuntut", listDetail.blnTuntut))
        param.Add(New SqlParameter("@Tahun_Tuntut", listDetail.thnTuntut))
        param.Add(New SqlParameter("@Jenis_Tuntutan", "P"))
        param.Add(New SqlParameter("@No_Pendahuluan", listDetail.noPendahuluan))
        param.Add(New SqlParameter("@Status_Dok", "02"))
        param.Add(New SqlParameter("@Status", "1"))
        param.Add(New SqlParameter("@Jum_Pendahuluan", listDetail.jumlahPendahuluan))
        param.Add(New SqlParameter("@Kod_Kump_Wang", listDetail.kodKW))
        param.Add(New SqlParameter("@Kod_Operasi", listDetail.kodKO))
        param.Add(New SqlParameter("@Kod_Projek", listDetail.kodKP))
        param.Add(New SqlParameter("@Kod_PTJ", listDetail.kodPTj))
        param.Add(New SqlParameter("@Id_Mohon", Session("ssusrID")))
        param.Add(New SqlParameter("@Pengesahan_Pemohon", listDetail.StatusPemohon))
        param.Add(New SqlParameter("@Akuan_Pemohon", "1"))



        Return db.Process(query, param)
    End Function

    Private Function UpdateNewOrder(listDetail As MhnTuntutan)


        Dim db As New DBKewConn
        Dim query As String = "UPDATE SMKB_Pendahuluan_Hdr SET Tujuan = @Tujuan,
                                Tarikh_Mula = @Tarikh_Mula, Tarikh_Tamat = @Tarikh_Tamat, Justifikasi_Prgm = @TunjukSebab, Peruntukan_Prgm = @Peruntukan_Prgm,  
                                Kod_Kump_Wang = @Kod_Kump_Wang , Kod_Operasi = @Kod_Operasi, Kod_Projek = @Kod_Projek, Kod_PTJ = @Kod_PTJ, Kod_Vot= @Kod_Vot,  
                                 Tkh_Adv_Perlu = @TkhAdvPerlu,  CaraBayar = @CaraBayar 
                                WHERE No_Pendahuluan = @No_Pendahuluan"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@No_Pendahuluan", listDetail.mohonID))
        param.Add(New SqlParameter("@PTj", listDetail.kodPTj))
        param.Add(New SqlParameter("@Tujuan", listDetail.TujuanMohon))
        'param.Add(New SqlParameter("@TunjukSebab", listDetail.TunjukSebab))
        param.Add(New SqlParameter("@Peruntukan_Prgm", listDetail.kodPTj))
        param.Add(New SqlParameter("@Kod_Kump_Wang", listDetail.kodKW))
        param.Add(New SqlParameter("@Kod_Operasi", listDetail.kodKO))
        param.Add(New SqlParameter("@Kod_Projek", listDetail.kodKP))
        param.Add(New SqlParameter("@Kod_PTJ", listDetail.kodPTj))


        Return db.Process(query, param)
    End Function

    Private Function GenerateOrderID() As String
        Dim db As New DBKewConn

        Dim year = Date.Now.ToString("yyyy")
        Dim month = Date.Now.Month

        Dim lastID As Integer = 1
        Dim newOrderID As String = ""

        Dim query As String = $"select TOP 1 No_Akhir as id from SMKB_No_Akhir where Kod_Modul ='09' AND Prefix ='CL' AND Tahun =@year"
        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@year", year))

        dt = db.Read(query, param)

        If dt.Rows.Count > 0 Then
            lastID = CInt(dt.Rows(0).Item("id")) + 1

            UpdateNoAkhir("09", "CL", year, lastID)
        Else

            InsertNoAkhir("09", "CL", year, lastID)
        End If
        newOrderID = "CL" + Format(lastID, "000000").ToString + month.ToString("00") + Right(year.ToString(), 2)

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

    Private Function UpdateStatusDokOrder_Mohon(listDetail As MhnTuntutan, statusLulus As String)
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
        param.Add(New SqlParameter("@Kod_Status_Dok", "01"))
        param.Add(New SqlParameter("@No_Rujukan", listDetail.mohonID))
        param.Add(New SqlParameter("@No_Staf", Session("ssusrID")))
        'param.Add(New SqlParameter("@Tkh_Tindakan", orderid))
        'param.Add(New SqlParameter("@Tkh_Transaksi", orderid))
        param.Add(New SqlParameter("@Status_Transaksi", 1))
        param.Add(New SqlParameter("@Status", 1))
        param.Add(New SqlParameter("@Ulasan", "-"))

        Return db.Process(query, param)

    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadHdrPermohonan(ByVal id As String) As String
        Dim resp As New ResponseRepository

        dt = GetHdrPermohonan(id)
        resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    Private Function GetHdrPermohonan(id As String) As DataTable
        Dim db = New DBKewConn

        Dim query As String = "SELECT DISTINCT a.No_Pendahuluan, FORMAT(a.Tarikh_Mohon, 'yyyy-MM-dd') AS Tarikh_Mohon, a.Status, a.Jenis_Pendahuluan, a.Tujuan, a.Jum_Mohon, b.Butiran, b.Kod_Modul, b.Kod_Status_Dok, 
                a.No_Staf + ' - ' + c.MS01_Nama AS NamaPemohon, a.JenisTugas,(SELECT Butiran FROM  SMKB_Lookup_Detail AS jtg WHERE  (Kod = 'AC04') AND (a.JenisTugas = Kod_Detail)) AS ButiranJenisTugas, 
                a.JenisTempat,(SELECT  Butiran FROM  SMKB_Lookup_Detail AS jt WHERE  (Kod = 'AC03') AND (a.JenisTempat = Kod_Detail)) AS ButiranJenisTempat, 
                a.Rujukan_Arahan, a.JenisPjln,(SELECT  Butiran FROM   SMKB_Lookup_Detail AS jp WHERE  (Kod = 'AC02') AND (a.JenisPjln = Kod_Detail)) AS ButiranJenisPjln, 
                FORMAT(a.Tarikh_Mula, 'yyyy-MM-dd') as Tarikh_Mula , FORMAT(a.Tarikh_Tamat, 'yyyy-MM-dd') as Tarikh_Tamat, a.Tempoh_Pjln, a.Jum_Layak, a.Kod_Kump_Wang, (SELECT  Butiran FROM  SMKB_Kump_Wang AS kw WHERE  (a.Kod_Kump_Wang = Kod_Kump_Wang)) AS colKW, 
                a.Kod_Operasi, (SELECT  Butiran FROM   SMKB_Operasi AS ko WHERE (Kod_Operasi = a.Kod_Operasi)) AS colKO, 
                a.Kod_Projek, (SELECT  Butiran FROM    SMKB_Projek AS kp WHERE  (Kod_Projek = a.Kod_Projek)) AS colKp, 
                a.Kod_PTJ,(SELECT  a.Kod_PTJ FROM   [DEVMIS\SQL_INS01].dbStaf.dbo.MS_PEJABAT AS b WHERE (Status = 1) AND (a.Kod_PTJ = LEFT(a.PTj, 2))) AS ButiranPTJ, 
                a.Kod_Vot, (SELECT Kod_Vot + ' - ' + Butiran AS Expr1 FROM   SMKB_Vot AS vot WHERE  (a.Kod_Vot = Kod_Vot)) AS ButiranVot, 
                a.Jenis_Penginapan, (SELECT Butiran FROM  SMKB_Lookup_Detail AS jpn WHERE  (Kod = 'AC01') AND (a.Jenis_Penginapan = Kod_Detail)) AS ButiranJenisPginap, 
                a.If_Mkn, FORMAT(a.Tkh_Adv_Perlu, 'yyyy-MM-dd') as Tkh_Adv_Perlu, a.CaraBayar,(SELECT Butiran FROM   SMKB_Lookup_Detail AS jb WHERE  (Kod = '0018') AND (a.CaraBayar = Kod_Detail)) AS ButiranJenisBayar, 
                a.No_Staf, a.PTj, a.Tempat_Perjalanan, a.Samb_Telefon, a.Folder,a.File_Name
                FROM            SMKB_Pendahuluan_Hdr AS a INNER JOIN
                SMKB_Kod_Status_Dok AS b ON a.Status_Dok = b.Kod_Status_Dok INNER JOIN
                QA11.dbStaf.dbo.MS01_Peribadi AS c ON a.No_Staf = c.MS01_NoStaf
                WHERE a.No_Pendahuluan = @No_Permohonan AND  (b.Kod_Modul = '09') AND (b.Kod_Status_Dok = '01') "



        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@No_Permohonan", id))

        Return db.Read(query, param)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadListingKeperluan(ByVal id As String, ByVal mohonID As String) As String
        Dim resp As New ResponseRepository

        dt = GetDataKeperluan(id, mohonID)
        resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function


    Private Function GetDataKeperluan(id As String, mohonID As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String

        query = $"SELECT No_Tuntutan, No_Item, Butiran, Kuantiti, Jumlah_anggaran, Nama_Fail, Path
            FROM SMKB_Tuntutan_Dtl WHERE No_Tuntutan= @mohonID "

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@mohonID", mohonID))

        dt = db.Read(query, param)
        If dt.Rows.Count > 0 Then
            Return dt

        Else

            query = "SELECT  a.No_Pendahuluan, a.No_Item, a.Butiran, a.Kuantiti, a.Kadar_Harga, a.Jumlah_anggaran, ' - ' as Nama_Fail, '' as Path,   b.Jum_Mohon
                            FROM  SMKB_Pendahuluan_Dtl a INNER JOIN SMKB_Pendahuluan_Hdr as b ON a.No_Pendahuluan = b.No_Pendahuluan
                            WHERE a.No_Pendahuluan= @No_Permohonan
                            ORDER BY a.No_Item ASC"


            'Dim param As New List(Of SqlParameter)
            param.Add(New SqlParameter("@No_Permohonan", id))
            Return db.Read(query, param)

        End If


    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function UploadFileSurat() As String
        Dim resp As New ResponseRepository
        Dim postedFile As HttpPostedFile = HttpContext.Current.Request.Files(0)
        Dim fileUpload = HttpContext.Current.Request.Form("fileSurat")
        Dim fileName As String = HttpContext.Current.Request.Form("fileName")

        Dim checkList As New UploadResit

        checkList.namaItem = HttpContext.Current.Request.Form("namaItem")
        checkList.idItem = HttpContext.Current.Request.Form("idItem")
        checkList.mohonID = HttpContext.Current.Request.Form("mohonID")
        checkList.jumlah = HttpContext.Current.Request.Form("jumlah")
        checkList.kuantiti = HttpContext.Current.Request.Form("kuantiti")

        Dim strTkhToday2 As String = Now.ToString("yyyy-MM-dd")
        Dim dtTkhToday2 As DateTime = CDate(strTkhToday2)


        Try
            ' Convert the base64 string to byte array
            'Dim fileBytes As Byte() = Convert.FromBase64String(fileData)

            ' Specify the file path where you want to save the uploaded file
            Dim savePath As String = Server.MapPath("~/UPLOAD/DOCUMENT/PENDAHULUAN DAN TUNTUTAN/CL/P/" & fileName)
            Dim folder As String = "UPLOAD/DOCUMENT/PENDAHULUAN DAN TUNTUTAN/CL/P/"

            ' Save the file to the specified path
            postedFile.SaveAs(savePath)

            ' Store the uploaded file name in session
            Session("UploadedFileName") = fileName

            '---Save File kat table----
            Dim db As New DBKewConn

            Dim queryC As String = $"SELECT DISTINCT No_Tuntutan, No_Item  FROM SMKB_Tuntutan_Dtl 
                        WHERE No_Tuntutan= @No_Tuntutan AND No_Item = @No_Item"

            Dim paramC As New List(Of SqlParameter)
            paramC.Add(New SqlParameter("@No_Tuntutan", checkList.mohonID))
            paramC.Add(New SqlParameter("@No_Item", checkList.idItem))

            dt = db.Read(queryC, paramC)

            If dt.Rows.Count > 0 Then
                If UpdateResitTuntut(checkList) <> "OK" Then
                    resp.Failed("Gagal Menyimpan order 1266")
                    Return JsonConvert.SerializeObject(resp.GetResult())
                End If
            Else

                If InsertResitTuntutan(checkList) <> "OK" Then
                    resp.Failed("Gagal Menyimpan order 1266")
                    Return JsonConvert.SerializeObject(resp.GetResult())
                End If
            End If

            Return " File uploaded successfully."
        Catch ex As Exception
            Return "Error uploading file: " & ex.Message
        End Try
    End Function


    Private Function InsertResitTuntutan(checkList As UploadResit)

        Dim db As New DBKewConn
        Dim fileName As String = HttpContext.Current.Request.Form("fileName")

        ' Store the uploaded file name in session
        Session("UploadedFileName") = fileName

        Dim folder As String = "UPLOAD/DOCUMENT/PENDAHULUAN DAN TUNTUTAN/CL/P/"

        Dim query As String = "INSERT INTO SMKB_Tuntutan_Dtl (No_Tuntutan, No_Item, Butiran, Kuantiti,  Jumlah_anggaran, Nama_Fail, Path)
                 VALUES(@No_Tuntutan,@No_Item, @Butiran, @Kuantiti, @Jumlah, @Nama_Fail, @Path )"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@No_Tuntutan", checkList.mohonID))
        param.Add(New SqlParameter("@No_Item", checkList.idItem))
        param.Add(New SqlParameter("@Butiran", checkList.namaItem))
        param.Add(New SqlParameter("@Kuantiti", checkList.kuantiti))
        param.Add(New SqlParameter("@Jumlah", checkList.jumlah))
        param.Add(New SqlParameter("@Nama_Fail", fileName))
        param.Add(New SqlParameter("@Path", folder))
        Return db.Process(query, param)

    End Function

    Private Function UpdateResitTuntut(checkList As UploadResit)

        Dim db As New DBKewConn
        Dim fileName As String = HttpContext.Current.Request.Form("fileName")

        ' Store the uploaded file name in session
        Session("UploadedFileName") = fileName
        Dim folder As String = "UPLOAD/DOCUMENT/PENDAHULUAN DAN TUNTUTAN/CL/P/"

        Dim queryU As String = "UPDATE SMKB_Tuntutan_Dtl SET Nama_Fail = @Nama_Fail, Path = @Path, Butiran = @Butiran, Kuantiti =@Kuantiti,  Jumlah_anggaran=@Jumlah
                                WHERE  No_Tuntutan = @No_Tuntutan AND No_Item = @No_Item"

        Dim paramU As New List(Of SqlParameter)
        paramU.Add(New SqlParameter("@No_Tuntutan", checkList.mohonID))
        paramU.Add(New SqlParameter("@No_Item", checkList.idItem))
        paramU.Add(New SqlParameter("@Butiran", checkList.namaItem))
        paramU.Add(New SqlParameter("@Kuantiti", checkList.kuantiti))
        paramU.Add(New SqlParameter("@Jumlah", checkList.jumlah))
        paramU.Add(New SqlParameter("@Nama_Fail", fileName))
        paramU.Add(New SqlParameter("@Path", folder))
        Return db.Process(queryU, paramU)

    End Function
    <System.Web.Services.WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SaveRecordBaki(kiraBaki As KiraTuntut) As String
        Dim resp As New ResponseRepository
        Dim db As New DBKewConn
        resp.Success("Data telah disimpan")
        Dim success As Integer = 0


        If kiraBaki Is Nothing Then
            resp.Failed("Tiada simpan")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        If kiraBaki.mohonID = "" Then 'untuk permohonan baru
            Exit Function

        Else 'untuk permohonan sedia ada

            Dim query As String = "UPDATE SMKB_Tuntutan_Hdr
                    SET Jum_Tuntut = @Jum_Tuntut, Jum_Baki_Tuntut = @Jum_Baki_Tuntut, Jum_Pendahuluan = @Jum_Pendahuluan
                    WHERE No_Tuntutan=@No_Tuntutan "
            Dim param As New List(Of SqlParameter)

            param.Add(New SqlParameter("@No_Tuntutan", kiraBaki.mohonID))
            param.Add(New SqlParameter("@Jum_Tuntut", kiraBaki.jumlahTuntut))
            param.Add(New SqlParameter("@Jum_Baki_Tuntut", kiraBaki.bakiTuntut))
            param.Add(New SqlParameter("@Jum_Pendahuluan", kiraBaki.jumlahPendahuluan))

            'Return db.Process(query, param)
        End If

        resp.Success("Rekod berjaya disimpan", "00", kiraBaki)
        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadDataBakiTuntut(ByVal id As String) As String
        Dim resp As New ResponseRepository

        dt = GetDataBakiTuntut(id)
        resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    Private Function GetDataBakiTuntut(id As String) As DataTable
        Dim db = New DBKewConn

        Dim query As String = "SELECT  No_Tuntutan, No_Pendahuluan,Jum_Tuntut, Jum_Baki_Tuntut, 
                                Jum_Pendahuluan FROM SMKB_Tuntutan_Hdr WHERE No_Tuntutan = @No_Tuntutan"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@No_Tuntutan", id))

        Return db.Read(query, param)
    End Function





End Class