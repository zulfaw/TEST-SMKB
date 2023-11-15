Imports System.ComponentModel
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports Newtonsoft.Json
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports SMKB_Web_Portal.ItemDetail

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class PengesahanDiri_WS
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


        Dim query As String = "SELECT  a.No_Pendahuluan,a.No_Staf,  a.Tujuan, isnull(Jum_Mohon,'0.00') as Jum_Mohon, a.Tarikh_Mohon, 
                        FORMAT(a.Tarikh_Mohon, 'dd-MM-yyyy') AS Tarikh_MohonDisplay, a.Pengesahan_Pemohon, a.Status_Dok,
                        a.Jenis_Pendahuluan,(SELECT Butiran FROM  SMKB_Lookup_Detail AS jt
                        WHERE        (Kod = 'AC06') AND (a.Jenis_Pendahuluan = Kod_Detail)) AS JnsMohon
                        FROM            SMKB_Pendahuluan_Hdr AS a                   
                        WHERE a.Status_Dok='01'  " & tarikhQuery & " order by a.Tarikh_Mohon desc"
        ' WHERE a.Status_Dok='01' AND a.Pengesahan_Pemohon='0' " & tarikhQuery & " order by a.Tarikh_Mohon desc"

        param.Add(New SqlParameter("@staffP", staffP))

        Return db.Read(query, param)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetUserInfo(nostaf As String)
        Dim db As New DBSMConn
        Dim query As String = $"SELECT  MS01_NoStaf as StafNo, MS01_Nama as Param1, MS08_Pejabat as Param2, JawGiliran as Param3, Kumpulan as Param4, 
                                Singkatan as Param5, MS02_GredGajiS as Param6,right(MS02_GredGajiS,2) as GredGaji, 
                                MS02_JumlahGajiS,  MS01_TelPejabat as Param7,  MS02_Kumpulan
                                FROM VK_AdvClm WHERE MS01_NoStaf = '{nostaf}'"
        Dim dt As DataTable = db.fselectCommandDt(query)

        Return JsonConvert.SerializeObject(dt)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetMohonDetailPD(mohonID As String)

        Dim db As New DBKewConn
        Dim query As String = $"SELECT a.No_Pendahuluan, a.Tarikh_Mohon,  FORMAT(a.Tarikh_Mohon,'dd-MM-yyyy') as Tarikh_MohonDisplay, a.No_Staf + ' - ' + c.ms01_nama   as NamaPemohon, 
            a.Tujuan, a.Jum_Mohon As Jum_Mohon, a.Tempat_Perjalanan, Format(a.Tarikh_Mula, 'yyyy-MM-dd') as Tarikh_Mula, FORMAT(a.Tarikh_Tamat, 'yyyy-MM-dd') as Tarikh_Tamat, a.Tempoh_Pjln, a.Status, a.JenisTempat,
            Format(a.Tkh_Adv_Perlu, 'yyyy-MM-dd') as Tkh_Adv_Perlu,                        
            (SELECT  Butiran FROM   SMKB_Lookup_Detail AS jt WHERE (Kod = 'AC03') AND (a.JenisTempat = Kod_Detail)) AS ButiranJenisTempat , 
            a.JenisTugas, (SELECT Butiran FROM  SMKB_Lookup_Detail as jtg WHERE jtg.Kod='AC04' AND (a.JenisTugas = jtg.Kod_Detail)) AS ButiranJenisTugas,
            a.JenisPjln, (SELECT  Butiran  FROM  SMKB_Lookup_Detail as jp WHERE jp.Kod='AC02'  AND (a.JenisPjln = jp.Kod_Detail)) AS ButiranJenisPjln,
            a.Jenis_Penginapan, (SELECT  Butiran  FROM  SMKB_Lookup_Detail as jpn WHERE jpn.Kod='AC01'  AND (a.Jenis_Penginapan = jpn.Kod_Detail)) AS ButiranJenisPginap,
            a.CaraBayar, (SELECT  Butiran  FROM  SMKB_Lookup_Detail as jb WHERE jb.Kod='AC14'  AND (a.CaraBayar = jb.Kod_Detail)) AS ButiranJenisBayar,
            a.Kod_Kump_Wang, (select Butiran from SMKB_Kump_Wang as kw where a.Kod_Kump_Wang = kw.Kod_Kump_Wang) as colKW,
            '00' as colhidko, (select Butiran from SMKB_Operasi as ko where  ko.Kod_Operasi = '00') as colKO,
            '0000000' as colhidkp, (select Butiran from SMKB_Projek as kp where kp.Kod_Projek = '0000000') as colKp,
            a.Ptj, (SELECT b.Pejabat FROM [DEVMIS\SQL_INS01].dbStaf.dbo.MS_PEJABAT as b WHERE b.STATUS = 1 And b.kodpejabat = left(Ptj, 2)) as ButiranPTJ ,
            a.Kod_Vot, (select Kod_Vot+' - '+ Butiran from SMKB_Vot as vot where a.Kod_Vot = vot.Kod_Vot) as ButiranVot, a.Status_Dok,
            b.Butiran, a.No_Staf As Nopemohon, a.If_Mkn, a.Folder, a.File_Name, a.File_Name as url 
            From SMKB_Pendahuluan_Hdr As a INNER Join
            SMKB_Kod_Status_Dok As b On a.Status_Dok = b.Kod_Status_Dok INNER Join
            [qa11].dbStaf.dbo.MS01_Peribadi as c ON a.No_Staf = c.ms01_noStaf
            Where (b.Kod_Modul = '09') AND (b.Kod_Status_Dok = '01') AND a.Jenis_Pendahuluan='PD' 
            AND a.No_Pendahuluan='{mohonID}'"


        Dim dt As DataTable = db.fselectCommandDt(query)
        Return JsonConvert.SerializeObject(dt)

    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetMohonDetailPP(mohonID As String)

        Dim db As New DBKewConn
        Dim query As String = $"SELECT a.No_Pendahuluan, a.Tujuan, a.Tempat_Perjalanan, a.PTj, FORMAT(a.Tarikh_Mula, 'yyyy-MM-dd') as Tarikh_Mula, FORMAT(a.Tarikh_Tamat, 'yyyy-MM-dd') as Tarikh_Tamat, a.Status, 
                        FORMAT(a.Tkh_Adv_Perlu, 'yyyy-MM-dd') as Tkh_Adv_Perlu,FORMAT(a.Tarikh_Mohon, 'yyyy-MM-dd') AS Tarikh_Mohon,  a.Jum_Mohon as Jum_Mohon,                        
                        a.CaraBayar, (SELECT  Butiran  FROM  SMKB_Lookup_Detail as jb WHERE jb.Kod='AC14'  AND (a.CaraBayar = jb.Kod_Detail)) AS ButiranJenisBayar,
                        a.Kod_Kump_Wang,(select Butiran from SMKB_Kump_Wang as kw where a.Kod_Kump_Wang = kw.Kod_Kump_Wang) as colKW,
                        '00' as colhidko, (select Butiran from SMKB_Operasi as ko where  ko.Kod_Operasi = '00') as colKO,
                        '0000000' as colhidkp, (select Butiran from SMKB_Projek as kp where kp.Kod_Projek = '0000000') as colKp,
                        a.Kod_PTJ, (SELECT b.Pejabat FROM [DEVMIS\SQL_INS01].dbStaf.dbo.MS_PEJABAT as b WHERE b.STATUS = 1 and b.kodpejabat = left(a.Kod_PTJ,2)) as ButiranPTJ  ,
                        a.Kod_Vot, (select Kod_Vot+' - '+ Butiran from SMKB_Vot as vot where a.Kod_Vot = vot.Kod_Vot) as ButiranVot, a.Status_Dok,
                        b.Butiran, a.No_Staf + ' - ' + c.ms01_nama   as NamaPemohon, a.No_Staf as Nopemohon,a.Peruntukan_Prgm,a.Justifikasi_Prgm, a.Tempat_Prgm
                        FROM SMKB_Pendahuluan_Hdr as a INNER JOIN 
                        SMKB_Kod_Status_Dok AS b ON a.Status_Dok = b.Kod_Status_Dok INNER JOIN
                        [qa11].dbStaf.dbo.MS01_Peribadi as c ON a.No_Staf = c.ms01_noStaf
                        WHERE  (b.Kod_Modul = '09') AND (b.Kod_Status_Dok = '01') AND (a.Jenis_Pendahuluan ='PP')  AND  a.No_Pendahuluan ='{mohonID}'"


        Dim dt As DataTable = db.fSelectCommandDt(query)
        Return JsonConvert.SerializeObject(dt)

    End Function


    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadListingKeperluan(ByVal id As String) As String
        Dim resp As New ResponseRepository

        dt = GetDataKeperluan(id)
        resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    Private Function GetDataKeperluan(id As String) As DataTable
        Dim db = New DBKewConn

        Dim query As String = $"SELECT  a.No_Pendahuluan, a.No_Item, a.Butiran, a.Kuantiti, a.Kadar_Harga, a.Jumlah_anggaran, b.Jum_Mohon
                            FROM            SMKB_Pendahuluan_Dtl a INNER JOIN SMKB_Pendahuluan_Hdr as b ON a.No_Pendahuluan = b.No_Pendahuluan
                            WHERE a.No_Pendahuluan= @No_Permohonan
                            ORDER BY a.No_Item ASC"


        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@No_Permohonan", id))

        Return db.Read(query, param)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetVotCOA(ByVal q As String) As String


        Dim tmpDT As DataTable = GetKodCOAList(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetKodCOAList(kodCariVot As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT CONCAT(a.Kod_Kump_Wang, ' - ', REPLACE(kw.Butiran, 'KUMPULAN WANG', 'KW'), ' , ' , mj.Pejabat, ', ', a.Kod_Vot, ' -', vot.Butiran,  ' , ', ko.Butiran, ', ' ,kp.Butiran) AS text,
                    a.Kod_Vot  AS value, a.Kod_Kump_Wang as colhidkw, kw.Butiran as colKW ,
                    mj.Pejabat as colPTJ , ko.Butiran as colKO ,  kp.Butiran as colKp , 
                    a.Kod_PTJ as colhidptj ,a.Kod_Vot AS colhidVot , a.Kod_Operasi as colhidko , a.Kod_Projek as colhidkp
                    FROM SMKB_COA_Master AS a 
                    JOIN SMKB_Vot AS vot ON a.Kod_Vot = vot.Kod_Vot
                    JOIN SMKB_Operasi AS ko ON a.Kod_Operasi = ko.Kod_Operasi
                    JOIN SMKB_Kump_Wang AS kw ON a.Kod_Kump_Wang = kw.Kod_Kump_Wang
					JOIN SMKB_Projek as kp on kp.Kod_Projek = a.Kod_Projek
					JOIN [DEVMIS\SQL_INS01].dbStaf.dbo.MS_PEJABAT AS mj ON mj.status = '1' and mj.kodpejabat = left(a.Kod_PTJ,2) 
                    WHERE a.status = 1 AND a.Kod_Vot='74102' "

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
    Public Function GetKaedahBayar(ByVal q As String) As String


        Dim tmpDT As DataTable = GetDataKaedahBayar(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetDataKaedahBayar(kod As String) As DataTable
        Dim db As New DBKewConn
        Dim query As String = "SELECT Kod_Detail as value, Butiran as text  
        FROM   SMKB_Lookup_Detail WHERE (Kod = 'AC14') AND Kod_Korporat='UTeM'  "

        Dim param As New List(Of SqlParameter)
        If kod <> "" Then
            query &= " AND Kod_Detail ='01' OR Kod_Detail='02' AND 
                        Butiran like 'Auto%' and (Kod_Detail LIKE '%' + @kod + '%' or Butiran LIKE '%' + @kod2 + '%') "
            param.Add(New SqlParameter("@kod", kod))
            param.Add(New SqlParameter("@kod2", kod))
        End If

        Return db.Read(query, param)

    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetVotCOAPD(ByVal q As String) As String


        Dim tmpDT As DataTable = GetKodCOAListPD(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetKodCOAListPD(kodCariVot As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT CONCAT(a.Kod_Kump_Wang, ' - ', REPLACE(kw.Butiran, 'KUMPULAN WANG', 'KW'), ' , ' , mj.Pejabat, ', ', a.Kod_Vot, ' -', vot.Butiran,  ' , ', ko.Butiran, ', ' ,kp.Butiran) AS text,
                    a.Kod_Vot  AS value, a.Kod_Kump_Wang as colhidkw, kw.Butiran as colKW ,
                    mj.Pejabat as colPTJ , ko.Butiran as colKO ,  kp.Butiran as colKp , 
                    a.Kod_PTJ as colhidptj ,a.Kod_Vot AS colhidVot , a.Kod_Operasi as colhidko , a.Kod_Projek as colhidkp
                    FROM SMKB_COA_Master AS a 
                    JOIN SMKB_Vot AS vot ON a.Kod_Vot = vot.Kod_Vot
                    JOIN SMKB_Operasi AS ko ON a.Kod_Operasi = ko.Kod_Operasi
                    JOIN SMKB_Kump_Wang AS kw ON a.Kod_Kump_Wang = kw.Kod_Kump_Wang
					JOIN SMKB_Projek as kp on kp.Kod_Projek = a.Kod_Projek
					JOIN [DEVMIS\SQL_INS01].dbStaf.dbo.MS_PEJABAT AS mj ON mj.status = '1' and mj.kodpejabat = left(a.Kod_PTJ,2) 
                    WHERE a.status = 1 AND a.Kod_Vot='74101' "

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

    <System.Web.Services.WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SaveRecordAdv(AdvList As MhnAdvanceP) As String
        Dim resp As New ResponseRepository
        resp.Success("Data telah disimpan")
        Dim success As Integer = 0
        Dim JumRekod As Integer = 0
        Dim strTkhToday2 As String = Now.ToString("yyyy-MM-dd")
        Dim dtTkhToday2 As DateTime = CDate(strTkhToday2)

        If AdvList Is Nothing Then
            resp.Failed("Tiada simpan")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If



        'If AdvList.OrderID = "" Then 'untuk permohonan baru
        '    AdvList.OrderID = GenerateOrderID()
        '    AdvList.Tkh_Upload = strTkhToday2
        '    AdvList.Folder = "UPLOAD/DOCUMENT/PENDAHULUAN DAN TUNTUTAN/PD/"

        '    'InsertNewOrder(AdvList.OrderID, AdvList.stafID, AdvList.kodPTj, AdvList.noTel, AdvList.Tujuan, AdvList.Lokasi, AdvList.ArahanK, AdvList.JnsTempat,
        '    'AdvList.JnsTugas, AdvList.JnsJalan, AdvList.TkhMula, AdvList.TkhTamat, AdvList.Tempoh, AdvList.hadMin, AdvList.JumlahAll, AdvList.stafID,
        '    ' AdvList.KodVot, AdvList.kodKW, AdvList.kodKO, AdvList.kodKP, AdvList.TkhAdvance, AdvList.JnsPginapan, AdvList.JnsBayar)
        '    If InsertNewOrder(AdvList) <> "OK" Then
        '        resp.Failed("Gagal Menyimpan order 1266")
        '        Return JsonConvert.SerializeObject(resp.GetResult())
        '        ' Exit Function
        '    End If
        'Else 'untuk permohonan sedia ada
        If AdvList.File_Name <> "" Then
            AdvList.Tkh_Upload = strTkhToday2
            AdvList.Folder = "UPLOAD/DOCUMENT/PENDAHULUAN DAN TUNTUTAN/PD/"
        End If
        If UpdateNewOrder(AdvList) <> "OK" Then
            'If InsertNewOrder(AdvList) <> "OK" Then
            resp.Failed("Gagal Menyimpan order 1266")
            Return JsonConvert.SerializeObject(resp.GetResult())
            '    ' Exit Function
            'End If
        End If
        ' End If



        If UpdateStatusDokOrder_Mohon(AdvList, "Y") <> "OK" Then

            'resp.Failed("Berjaya simpan") 'Gagal Menyimpan order YX
            Return JsonConvert.SerializeObject(resp.GetResult())
            ' Exit Function

        End If

        'If success = 0 Then
        '    resp.Failed("Rekod order detail gagal disimpan")
        '    Return JsonConvert.SerializeObject(resp.GetResult())
        'End If

        'If Not success = JumRekod Then
        '    resp.Success("Rekod order detail berjaya disimpan dengan beberapa rekod tidak disimpan", "00", AdvList)
        '    Return JsonConvert.SerializeObject(resp.GetResult())
        'Else

        'End If

        resp.Success("Rekod berjaya disimpan", "00", AdvList)
        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    Private Function UpdateNewOrder(AdvList As MhnAdvanceP)


        Dim db As New DBKewConn
        Dim query As String = "UPDATE SMKB_Pendahuluan_Hdr SET Tujuan = @Tujuan, Tempat_Perjalanan = @Tempat_Perjalanan, JenisTempat= @JenisTempat,PTj = @PTjMohon,
                               JenisTugas = @JenisTugas, JenisPjln = @JenisPjln, Tarikh_Mula = @Tarikh_Mula, Tarikh_Tamat = @Tarikh_Tamat, Tempoh_Pjln = @Tempoh_Pjln, 
                                Kod_Kump_Wang = @Kod_Kump_Wang, Kod_Operasi = @Kod_Operasi, Kod_Projek = @Kod_Projek, Kod_PTJ = @Kod_PTJ, Kod_Vot= @Kod_Vot, Jenis_Penginapan = @JenisPginap, 
                                If_Mkn = @staMakan, Tkh_Adv_Perlu = @Tkh_Adv_Perlu,  CaraBayar = @CaraBayar, Jum_Mohon = @Jum_Mohon,
                                Folder = @folder, [File_Name]= @namafile, Tkh_Upload= @tkhUpload, Pengesahan_Pemohon = @PengesahanPemohon
                                WHERE No_Pendahuluan = @No_Pendahuluan AND Status = 1"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@Tujuan", AdvList.Tujuan))
        param.Add(New SqlParameter("@PTjMohon", AdvList.PtjMohon))
        param.Add(New SqlParameter("@Tempat_Perjalanan", AdvList.Lokasi))
        param.Add(New SqlParameter("@JenisTempat", AdvList.JnsTempat))
        param.Add(New SqlParameter("@JenisTugas", AdvList.JnsTugas))
        param.Add(New SqlParameter("@JenisPjln", AdvList.JnsJalan))
        param.Add(New SqlParameter("@Tarikh_Mula", AdvList.TkhMula))
        param.Add(New SqlParameter("@Tarikh_Tamat", AdvList.TkhTamat))
        param.Add(New SqlParameter("@Tempoh_Pjln", AdvList.Tempoh))
        param.Add(New SqlParameter("@Kod_Kump_Wang", AdvList.kodKW))
        param.Add(New SqlParameter("@Kod_Operasi", AdvList.kodKO))
        param.Add(New SqlParameter("@Kod_Projek", AdvList.kodKP))
        param.Add(New SqlParameter("@Kod_PTJ", AdvList.kodPTj))
        param.Add(New SqlParameter("@Kod_Vot", AdvList.KodVot))
        param.Add(New SqlParameter("@JenisPginap", AdvList.JnsPginapan))
        param.Add(New SqlParameter("@staMakan", AdvList.staMakan))
        param.Add(New SqlParameter("@Tkh_Adv_Perlu", AdvList.TkhAdvance))
        param.Add(New SqlParameter("@CaraBayar", AdvList.JnsBayar))
        param.Add(New SqlParameter("@Jum_Mohon", AdvList.JumlahAll))
        'param.Add(New SqlParameter("@TkhMohon", AdvList.tkhMohon))
        param.Add(New SqlParameter("@Folder", AdvList.Folder))
        param.Add(New SqlParameter("@namafile", AdvList.File_Name))
        param.Add(New SqlParameter("@tkhUpload", AdvList.Tkh_Upload))
        param.Add(New SqlParameter("@No_Pendahuluan", AdvList.OrderID))
        param.Add(New SqlParameter("@PengesahanPemohon", "1"))

        'param.Add(New SqlParameter("@PTj", AdvList.kodPTj))

        Return db.Process(query, param)
    End Function

    Private Function UpdateStatusDokOrder_Mohon(AdvList As MhnAdvanceP, statusLulus As String)
        Dim db As New DBKewConn

        Dim statusMohon As String = ""

        If statusLulus = "Y" Then

            statusMohon = "01"

        End If


        Dim query As String = "INSERT INTO SMKB_Status_Dok (Kod_Modul  , Kod_Status_Dok  ,  No_Rujukan , No_Staf , Tkh_Tindakan , Tkh_Transaksi , Status_Transaksi , Status , Ulasan )
    			VALUES
    			(@Kod_Modul , @Kod_Status_Dok , @No_Rujukan , @No_Staf , getdate() , getdate(), @Status_Transaksi , @Status , @Ulasan)"

        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@Kod_Modul", "09"))
        param.Add(New SqlParameter("@Kod_Status_Dok", statusMohon))
        param.Add(New SqlParameter("@No_Rujukan", AdvList.OrderID))
        param.Add(New SqlParameter("@No_Staf", Session("ssusrID")))
        param.Add(New SqlParameter("@Status_Transaksi", 1))
        param.Add(New SqlParameter("@Status", 1))
        param.Add(New SqlParameter("@Ulasan", "-"))

        Return db.Process(query, param)

    End Function


    Function GetBaseUrl() As String
        Dim curUrl As Uri = HttpContext.Current.Request.Url
        Dim scheme As String = curUrl.Scheme
        Dim host As String = curUrl.Host
        Dim port As Integer = curUrl.Port
        Dim segments As String() = curUrl.Segments

        If port <> 80 Then
            host = host + ":" + port.ToString()
        End If

        Return scheme + "://" + host + "/" + segments(1) + "/"
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadDataSurat(ByVal id As String) As String
        Dim resp As New ResponseRepository

        dt = GetDataCheckListSurat(id)

        For Each x As DataRow In dt.Rows
            If Not IsDBNull(x.Item("Nama_Fail")) Then
                Dim url As String = GetBaseUrl() + Trim(x.Item("Path")) + "/" + x.Item("Nama_Fail")
                x.Item("url") = url
            End If
        Next

        resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    Private Function GetDataCheckListSurat(id As String) As DataTable
        Dim db = New DBKewConn

        Dim query As String = "SELECT No_Pendahuluan, ID_CheckList, Hantar, Nama_Fail, Path, Nama_Fail as url
            FROM  SMKB_Pendahuluan_CheckList WHERE  No_Pendahuluan= @mohonID AND ID_CheckList='4'"


        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@mohonID", id))

        Return db.Read(query, param)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadDataBajet(ByVal id As String) As String
        Dim resp As New ResponseRepository

        dt = GetDataCheckListBajet(id)

        For Each x As DataRow In dt.Rows
            If Not IsDBNull(x.Item("Nama_Fail")) Then
                Dim url As String = GetBaseUrl() + Trim(x.Item("Path")) + "/" + x.Item("Nama_Fail")
                x.Item("url") = url
            End If
        Next
        resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    Private Function GetDataCheckListBajet(id As String) As DataTable
        Dim db = New DBKewConn

        Dim query As String = "SELECT No_Pendahuluan, ID_CheckList, Hantar, Nama_Fail, Path, Nama_Fail as url
            FROM  SMKB_Pendahuluan_CheckList WHERE  No_Pendahuluan= @mohonID AND ID_CheckList='5'"


        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@mohonID", id))

        Return db.Read(query, param)
    End Function

    <System.Web.Services.WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SaveRecordChecklist(checkList As Lampiran) As String
        Dim resp As New ResponseRepository
        resp.Success("Data telah disimpan")
        Dim success As Integer = 0
        Dim JumRekod As Integer = 0
        Dim strTkhToday2 As String = Now.ToString("yyyy-MM-dd")
        Dim dtTkhToday2 As DateTime = CDate(strTkhToday2)

        If checkList Is Nothing Then
            resp.Failed("Tiada simpan")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        checkList.Tkh_Upload = strTkhToday2
        checkList.folderSurat = "UPLOAD/DOCUMENT/PENDAHULUAN DAN TUNTUTAN/PP/"
        checkList.folderBajet = "UPLOAD/DOCUMENT/PENDAHULUAN DAN TUNTUTAN/PP/"

        If checkList.mohonID = "" Then 'untuk permohonan baru
            Exit Function


        Else 'untuk permohonan sedia ada

            If semakdatachecklist(checkList.mohonID, checkList.idSurat) = "wujud" Then

                If checkList.namaSurat <> "" Then
                    checkList.folderSurat = "UPLOAD/DOCUMENT/PENDAHULUAN DAN TUNTUTAN/PP/"
                    If UpdateNewLampiranSurat(checkList) <> "OK" Then
                        'If InsertNewOrder(AdvList) <> "OK" Then
                        resp.Failed("Gagal Menyimpan order 1266")
                        Return JsonConvert.SerializeObject(resp.GetResult())
                        '    ' Exit Function
                        'End If
                    End If
                End If

            Else

                If checkList.namaSurat <> "" Then
                    checkList.Tkh_Upload = strTkhToday2
                    checkList.folderSurat = "UPLOAD/DOCUMENT/PENDAHULUAN DAN TUNTUTAN/PP/"
                    If InsertNewLampiranSurat(checkList) <> "OK" Then
                        'If InsertNewOrder(AdvList) <> "OK" Then
                        resp.Failed("Gagal Menyimpan order 1266")
                        Return JsonConvert.SerializeObject(resp.GetResult())
                        '    ' Exit Function
                        'End If
                    End If
                End If

            End If

            If semakdatachecklist(checkList.mohonID, checkList.idBajet) = "wujud" Then
                If checkList.namaBajet <> "" Then
                    checkList.Tkh_Upload = strTkhToday2
                    checkList.folderBajet = "UPLOAD/DOCUMENT/PENDAHULUAN DAN TUNTUTAN/PP/"
                    If UpdateNewLampiranBajet(checkList) <> "OK" Then
                        'If InsertNewOrder(AdvList) <> "OK" Then
                        resp.Failed("Gagal Menyimpan order 1266")
                        Return JsonConvert.SerializeObject(resp.GetResult())
                        '    ' Exit Function
                        'End If
                    End If
                End If
            Else
                If checkList.namaBajet <> "" Then
                    checkList.Tkh_Upload = strTkhToday2
                    checkList.folderBajet = "UPLOAD/DOCUMENT/PENDAHULUAN DAN TUNTUTAN/PP/"
                    If InsertNewLampiranBajet(checkList) <> "OK" Then
                        'If InsertNewOrder(AdvList) <> "OK" Then
                        resp.Failed("Gagal Menyimpan order 1266")
                        Return JsonConvert.SerializeObject(resp.GetResult())
                        '    ' Exit Function
                        'End If
                    End If
                End If
            End If

        End If

        If UpdateStatusDokPermohonan(checkList, "Y") <> "OK" Then

            'resp.Failed("Berjaya simpan") 'Gagal Menyimpan order YX
            Return JsonConvert.SerializeObject(resp.GetResult())
            ' Exit Function
        End If

        resp.Success("Rekod berjaya disimpan", "00", checkList)
        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    Private Function UpdateNewLampiranSurat(checkList As Lampiran)

        Dim db As New DBKewConn
        Dim query As String = "UPDATE SMKB_Pendahuluan_CheckList SET Nama_Fail = @Nama_Fail, Path = @Path
                                WHERE  No_Pendahuluan = @No_Pendahuluan AND ID_CheckList = @ID_CheckList"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@Nama_Fail", checkList.namaSurat))
        param.Add(New SqlParameter("@Path", checkList.folderSurat))
        param.Add(New SqlParameter("@No_Pendahuluan", checkList.mohonID))
        param.Add(New SqlParameter("@ID_CheckList", checkList.idSurat))
        Return db.Process(query, param)
    End Function

    Private Function UpdateNewLampiranBajet(checkList As Lampiran)

        Dim db As New DBKewConn
        Dim query As String = "UPDATE SMKB_Pendahuluan_CheckList SET Nama_Fail = @Nama_Fail, Path = @Path
                                WHERE  No_Pendahuluan = @No_Pendahuluan AND ID_CheckList = @ID_CheckList"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@Nama_Fail", checkList.namaBajet))
        param.Add(New SqlParameter("@Path", checkList.folderBajet))
        param.Add(New SqlParameter("@No_Pendahuluan", checkList.mohonID))
        param.Add(New SqlParameter("@ID_CheckList", checkList.idBajet))
        Return db.Process(query, param)
    End Function


    Private Function InsertNewLampiranSurat(checkList As Lampiran)
        Dim db As New DBKewConn

        Dim query As String = "INSERT INTO SMKB_Pendahuluan_CheckList
        VALUES(@No_Pendahuluan ,@ID_CheckList, @Hantar, @Nama_Fail, @Path)"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@No_Pendahuluan", checkList.mohonID))
        param.Add(New SqlParameter("@ID_CheckList", checkList.idSurat))
        param.Add(New SqlParameter("@Hantar", checkList.staHantarSurat))
        param.Add(New SqlParameter("@Nama_Fail", checkList.namaSurat))
        param.Add(New SqlParameter("@Path", checkList.folderSurat))

        Return db.Process(query, param)
    End Function

    Private Function InsertNewLampiranBajet(checkList As Lampiran)
        Dim db As New DBKewConn

        Dim query As String = "INSERT INTO SMKB_Pendahuluan_CheckList
        VALUES(@No_Pendahuluan,@ID_CheckList, @Hantar, @Nama_Fail, @Path)"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@No_Pendahuluan", checkList.mohonID))
        param.Add(New SqlParameter("@ID_CheckList", checkList.idBajet))
        param.Add(New SqlParameter("@Hantar", checkList.staHantarBajet))
        param.Add(New SqlParameter("@Nama_Fail", checkList.namaBajet))
        param.Add(New SqlParameter("@Path", checkList.folderBajet))

        Return db.Process(query, param)
    End Function

    Private Function semakdatachecklist(mohonID, id) As String
        Dim db As New DBKewConn

        Dim statusLampiran As String = ""

        Dim query As String = $"SELECT DISTINCT No_Pendahuluan FROM SMKB_Pendahuluan_CheckList WHERE No_Pendahuluan= @mohonID AND ID_CheckList = @id"
        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@mohonID", mohonID))
        param.Add(New SqlParameter("@id", id))

        dt = db.Read(query, param)

        If dt.Rows.Count > 0 Then
            statusLampiran = "wujud"
        Else

            statusLampiran = "tidakWujud"
        End If

        Return statusLampiran
    End Function

    Private Function UpdateStatusDokPermohonan(checkList As Lampiran, statusLulus As String)
        Dim db As New DBKewConn

        Dim statusMohon As String = ""

        If statusLulus = "Y" Then

            statusMohon = "01"

        End If


        Dim query As String = "INSERT INTO SMKB_Status_Dok (Kod_Modul  , Kod_Status_Dok  ,  No_Rujukan , No_Staf , Tkh_Tindakan , Tkh_Transaksi , Status_Transaksi , Status , Ulasan )
    			VALUES
    			(@Kod_Modul , @Kod_Status_Dok , @No_Rujukan , @No_Staf , getdate() , getdate(), @Status_Transaksi , @Status , @Ulasan)"

        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@Kod_Modul", "09"))
        param.Add(New SqlParameter("@Kod_Status_Dok", statusMohon))
        param.Add(New SqlParameter("@No_Rujukan", checkList.mohonID))
        param.Add(New SqlParameter("@No_Staf", Session("ssusrID")))
        param.Add(New SqlParameter("@Status_Transaksi", 1))
        param.Add(New SqlParameter("@Status", 1))
        param.Add(New SqlParameter("@Ulasan", "-"))

        Return db.Process(query, param)

    End Function

    <System.Web.Services.WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SaveRecordPelbagai(OtherList As MhnPelbagaiPP) As String
        Dim resp As New ResponseRepository
        resp.Success("Data telah disimpan")
        Dim success As Integer = 0
        Dim JumRekod As Integer = 0
        If OtherList Is Nothing Then
            resp.Failed("Tiada simpan")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If



        'If OtherList.mohonID = "" Then 'untuk permohonan baru
        '    OtherList.mohonID = GenerateOrderID()

        '    'InsertNewOrder(AdvList.OrderID, AdvList.stafID, AdvList.kodPTj, AdvList.noTel, AdvList.Tujuan, AdvList.Lokasi, AdvList.ArahanK, AdvList.JnsTempat,
        '    'AdvList.JnsTugas, AdvList.JnsJalan, AdvList.TkhMula, AdvList.TkhTamat, AdvList.Tempoh, AdvList.hadMin, AdvList.JumlahAll, AdvList.stafID,
        '    ' AdvList.KodVot, AdvList.kodKW, AdvList.kodKO, AdvList.kodKP, AdvList.TkhAdvance, AdvList.JnsPginapan, AdvList.JnsBayar)
        '    If InsertNewOrder(OtherList) <> "OK" Then
        '        resp.Failed("Gagal Menyimpan order 1266")
        '        Return JsonConvert.SerializeObject(resp.GetResult())
        '        ' Exit Function
        '    End If
        'Else 'untuk permohonan sedia ada

        'start delete dlu detail sedia ada...
        'DeleteOrderHdr(AdvList.OrderID, AdvList.NoRujukan, AdvList.Perihal, AdvList.Tarikh, AdvList.JenisTransaksi, AdvList.JumlahDebit)
        'DeleteOrderDtl(AdvList.OrderID)
        'end delete

        'comment tuk data--
        'AdvList.OrderID, AdvList.stafID, AdvList.kodPTj, AdvList.noTel, AdvList.Tujuan, AdvList.Lokasi, AdvList.ArahanK, AdvList.JnsTempat,
        'AdvList.JnsTugas, AdvList.JnsJalan, AdvList.TkhMula, AdvList.TkhTamat, AdvList.Tempoh, AdvList.hadMin, AdvList.JumlahAll, AdvList.stafID,
        'AdvList.KodVot, AdvList.kodKW, AdvList.kodKO, AdvList.kodKP, AdvList.TkhAdvance, AdvList.JnsPginapan, AdvList.JnsBayar
        If UpdateNewOrderPP(OtherList) <> "OK" Then
            'If InsertNewOrder(OtherList) <> "OK" Then
            resp.Failed("Gagal Menyimpan order 1266")
            Return JsonConvert.SerializeObject(resp.GetResult())
            ' Exit Function
            'End If
        End If
        'End If



        If UpdateStatusDokOrder_MohonPP(OtherList, "Y") <> "OK" Then

            'resp.Failed("Berjaya simpan") 'Gagal Menyimpan order YX
            Return JsonConvert.SerializeObject(resp.GetResult())
            ' Exit Function

        End If

        'If success = 0 Then
        '    resp.Failed("Rekod order detail gagal disimpan")
        '    Return JsonConvert.SerializeObject(resp.GetResult())
        'End If

        'If Not success = JumRekod Then
        '    resp.Success("Rekod order detail berjaya disimpan dengan beberapa rekod tidak disimpan", "00", AdvList)
        '    Return JsonConvert.SerializeObject(resp.GetResult())
        'Else

        'End If

        resp.Success("Rekod berjaya disimpan", "00", OtherList)
        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function


    Private Function UpdateStatusDokOrder_MohonPP(OtherList As MhnPelbagaiPP, statusLulus As String)
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
        param.Add(New SqlParameter("@No_Rujukan", OtherList.mohonID))
        param.Add(New SqlParameter("@No_Staf", Session("ssusrID")))
        'param.Add(New SqlParameter("@Tkh_Tindakan", orderid))
        'param.Add(New SqlParameter("@Tkh_Transaksi", orderid))
        param.Add(New SqlParameter("@Status_Transaksi", 1))
        param.Add(New SqlParameter("@Status", 1))
        param.Add(New SqlParameter("@Ulasan", "-"))

        Return db.Process(query, param)

    End Function
    Private Function UpdateNewOrderPP(OtherList As MhnPelbagaiPP)


        Dim db As New DBKewConn
        Dim query As String = "UPDATE SMKB_Pendahuluan_Hdr SET Tujuan = @Tujuan,Tempat_Prgm = @TempatProgram,
                                Tarikh_Mula = @Tarikh_Mula, Tarikh_Tamat = @Tarikh_Tamat, Justifikasi_Prgm = @TunjukSebab, Peruntukan_Prgm = @Peruntukan_Prgm,  
                                Kod_Kump_Wang = @Kod_Kump_Wang , Kod_Operasi = @Kod_Operasi, Kod_Projek = @Kod_Projek, Kod_PTJ = @Kod_PTJ, Kod_Vot= @Kod_Vot,  
                                 Tkh_Adv_Perlu = @TkhAdvPerlu,  CaraBayar = @CaraBayar , Pengesahan_Pemohon = @PengesahanPemohon
                                WHERE No_Pendahuluan = @No_Pendahuluan"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@No_Pendahuluan", OtherList.mohonID))
        param.Add(New SqlParameter("@PTj", OtherList.kodPTj))
        param.Add(New SqlParameter("@Tujuan", OtherList.TujuanProgram))
        param.Add(New SqlParameter("@Tarikh_Mula", OtherList.TkhMula))
        param.Add(New SqlParameter("@Tarikh_Tamat", OtherList.TkhTamat))
        param.Add(New SqlParameter("@TunjukSebab", OtherList.TunjukSebab))
        param.Add(New SqlParameter("@Peruntukan_Prgm", OtherList.kodPTj))
        param.Add(New SqlParameter("@Kod_Kump_Wang", OtherList.kodKW))
        param.Add(New SqlParameter("@Kod_Operasi", OtherList.kodKO))
        param.Add(New SqlParameter("@Kod_Projek", OtherList.kodKP))
        param.Add(New SqlParameter("@Kod_PTJ", OtherList.kodPTj))
        param.Add(New SqlParameter("@Kod_Vot", OtherList.KodVot))
        param.Add(New SqlParameter("@CaraBayar", OtherList.JnsBayar))
        param.Add(New SqlParameter("@TkhAdvPerlu", OtherList.TkhAdvPerlu))
        param.Add(New SqlParameter("@PengesahanPemohon", "1"))
        param.Add(New SqlParameter("@TempatProgram", OtherList.TempatProgram))

        Return db.Process(query, param)
    End Function


    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function DeleteOrder(ByVal id As String, mohonID As String) As String
        Dim resp As New ResponseRepository

        'DeleteOrderDetails(id, mohonID)


        If DeleteOrderRecord(id, mohonID) <> "OK" Then
            resp.Failed("Gagal")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        resp.Success("Order telah dihapuskan")
        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function


    Private Function DeleteOrderRecord(orderid As String, mohonID As String)
        Dim db = New DBKewConn
        Dim query As String = "DELETE FROM SMKB_Pendahuluan_Dtl WHERE No_Pendahuluan = @idmohon AND  No_Item = @orderid "
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@orderid", orderid))
        param.Add(New SqlParameter("@idmohon", mohonID))

        Return db.Process(query, param)
    End Function


    <System.Web.Services.WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SaveRecordItem(keperluan As MhnPelbagai) As String
        Dim resp As New ResponseRepository
        resp.Success("Data telah disimpan")
        Dim success As Integer = 0
        Dim JumRekod As Integer = 0
        Dim sumTotal As Decimal = 0.00
        keperluan.Jumlah = 0.00


        For Each orderItem As ItemDetail In keperluan.GroupItem

            If orderItem.txtKeperluan = "" Then
                Continue For
            End If

            JumRekod += 1

            'orderDetail.kredit = 0 'orderDetail.quantity * orderDetail.debit 'This can be automated insie orderdetail model

            If orderItem.mohonID <> "" Then
                If semakdataKeperluan(orderItem.mohonID, orderItem.id) = "wujud" Then
                    'updateDataKeperluan--

                    If UpdateOrderDetail(orderItem) = "OK" Then
                        success += 1
                        keperluan.Jumlah += orderItem.txtAnggaran
                    End If
                Else
                    'insert Data Keperluan
                    orderItem.id = GenerateOrderDetailID(keperluan.mohonID)
                    orderItem.mohonID = keperluan.mohonID
                    If InsertDataItem(orderItem) = "OK" Then
                        success += 1
                        keperluan.Jumlah += orderItem.txtAnggaran
                    End If
                End If
            Else

            End If
        Next

        If UpdateTotalItem(keperluan) <> "OK" Then
            'If InsertNewOrder(OtherList) <> "OK" Then
            resp.Failed("Gagal Menyimpan order 1266")
            Return JsonConvert.SerializeObject(resp.GetResult())
            ' Exit Function
            'End If
        End If

        resp.Success("Rekod berjaya disimpan", "00", keperluan)
        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    Private Function semakdataKeperluan(mohonID, id) As String
        Dim db As New DBKewConn

        Dim statusLampiran As String = ""

        Dim query As String = $"SELECT No_Pendahuluan, No_Item FROM SMKB_Pendahuluan_Dtl Where No_Pendahuluan= @mohonID AND No_Item = @id"
        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@mohonID", mohonID))
        param.Add(New SqlParameter("@id", id))

        dt = db.Read(query, param)

        If dt.Rows.Count > 0 Then
            statusLampiran = "wujud"
        Else

            statusLampiran = "tidakWujud"
        End If

        Return statusLampiran
    End Function

    Private Function UpdateOrderDetail(orderItem As ItemDetail)
        Dim db = New DBKewConn
        Dim query As String = "UPDATE SMKB_Pendahuluan_Dtl
        set Butiran = @Butiran, Kuantiti = @Kuantiti, Kadar_Harga = @Kadar_Harga, 
        Jumlah_anggaran = @Jumlah_anggaran
        where No_Item = @No_Item AND No_Pendahuluan=@No_Pendahuluan"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@No_Pendahuluan", orderItem.mohonID))
        param.Add(New SqlParameter("@No_Item", orderItem.id))
        param.Add(New SqlParameter("@Butiran", orderItem.txtKeperluan))
        param.Add(New SqlParameter("@Kuantiti", orderItem.Kuantiti))
        param.Add(New SqlParameter("@Kadar_Harga", orderItem.txtKadarHarga))
        param.Add(New SqlParameter("@Jumlah_anggaran", orderItem.txtAnggaran))

        Return db.Process(query, param)
    End Function

    Private Function GenerateOrderDetailID(itemId As String) As String
        Dim db = New DBKewConn
        Dim lastID As Integer = 1
        Dim newOrderID As String = ""
        Dim param As New List(Of SqlParameter)

        Dim query As String = "select TOP 1 No_Item as id
        from SMKB_Pendahuluan_Dtl 
        where No_Pendahuluan = @itemId
        ORDER BY No_Item DESC"

        param.Add(New SqlParameter("@itemId", itemId))

        dt = db.Read(query, param)

        If dt.Rows.Count > 0 Then
            lastID = CInt(dt.Rows(0).Item("id")) + 1
        End If

        newOrderID = lastID

        Return newOrderID
    End Function

    Private Function InsertDataItem(orderItem As ItemDetail)
        Dim db As New DBKewConn

        Dim query As String = "INSERT INTO SMKB_Pendahuluan_Dtl
        VALUES(@No_Pendahuluan , @No_Item, @Butiran, @Kuantiti , @Kadar_Harga, @Jumlah_anggaran)"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@No_Pendahuluan", orderItem.mohonID))
        param.Add(New SqlParameter("@No_Item", orderItem.id))
        param.Add(New SqlParameter("@Butiran", orderItem.txtKeperluan))
        param.Add(New SqlParameter("@Kuantiti", orderItem.Kuantiti))
        param.Add(New SqlParameter("@Kadar_Harga", orderItem.txtKadarHarga))
        param.Add(New SqlParameter("@Jumlah_anggaran", orderItem.txtAnggaran))


        Return db.Process(query, param)
    End Function

    Private Function UpdateTotalItem(keperluan As MhnPelbagai) As String


        Dim db As New DBKewConn
        Dim query As String = "UPDATE SMKB_Pendahuluan_Hdr SET Jum_Mohon = @Jum_Mohon                                 
                                WHERE No_Pendahuluan = @No_Pendahuluan"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@No_Pendahuluan", keperluan.mohonID))
        param.Add(New SqlParameter("@Jum_Mohon", keperluan.Jumlah))


        Return db.Process(query, param)
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function UploadFileBajet() As String
        Dim postedFile As HttpPostedFile = HttpContext.Current.Request.Files(0)
        Dim fileUpload = HttpContext.Current.Request.Form("fileSurat")
        Dim fileName As String = HttpContext.Current.Request.Form("fileName")

        Try
            ' Convert the base64 string to byte array
            'Dim fileBytes As Byte() = Convert.FromBase64String(fileData)

            ' Specify the file path where you want to save the uploaded file
            Dim savePath As String = Server.MapPath("~/UPLOAD/DOCUMENT/PENDAHULUAN DAN TUNTUTAN/PP/" & fileName)


            ' Save the file to the specified path
            postedFile.SaveAs(savePath)

            ' Store the uploaded file name in session
            Session("UploadedFileName") = fileName

            Return " File uploaded successfully."
        Catch ex As Exception
            Return "Error uploading file: " & ex.Message
        End Try
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function UploadFileSurat() As String
        Dim postedFile As HttpPostedFile = HttpContext.Current.Request.Files(0)
        Dim fileUpload = HttpContext.Current.Request.Form("fileSurat")
        Dim fileName As String = HttpContext.Current.Request.Form("fileName")

        Try
            ' Convert the base64 string to byte array
            'Dim fileBytes As Byte() = Convert.FromBase64String(fileData)

            ' Specify the file path where you want to save the uploaded file
            Dim savePath As String = Server.MapPath("~/UPLOAD/DOCUMENT/PENDAHULUAN DAN TUNTUTAN/PP/" & fileName)


            ' Save the file to the specified path
            postedFile.SaveAs(savePath)

            ' Store the uploaded file name in session
            Session("UploadedFileName") = fileName

            Return " File uploaded successfully."
        Catch ex As Exception
            Return "Error uploading file: " & ex.Message
        End Try
    End Function
End Class