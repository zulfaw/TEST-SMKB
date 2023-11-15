Imports System.ComponentModel
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Web.Script.Services
Imports Newtonsoft.Json
Imports System.Collections.Generic
Imports System.Web.Script.Serialization
Imports System.Data.SqlClient
Imports System
Imports SMKB_Web_Portal.PenerimaanPD

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. <System.Web.Script.Services.ScriptService()> _
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class Penerimaan_WS
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
    Public Function LoadOrderRecordPD(category_filter As String, isClicked As Boolean, tkhMula As DateTime, tkhTamat As DateTime, staffP As String) As String
        Dim resp As New ResponseRepository

        If isClicked = False Then
            Return JsonConvert.SerializeObject(New DataTable)
        End If

        dt = GetRecord_SenaraiSendiriPD(category_filter, tkhMula, tkhTamat, staffP)



        'dt = GetRecord_SenaraiSendiri(id)
        resp.SuccessPayload(dt)
        'resp.GetResult()
        Return JsonConvert.SerializeObject(dt)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetRecord_SenaraiSendiriPD(category_filter As String, tkhMula As DateTime, tkhTamat As DateTime, staffP As String) As DataTable
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


        Dim query As String = "SELECT a.No_Pendahuluan, a.Tarikh_Mohon,  FORMAT(a.Tarikh_Mohon,'dd-MM-yyyy') as Tarikh_MohonDisplay, a.No_Staf + ' - ' + c.ms01_nama   as NamaPemohon, 
a.Tujuan, a.Jum_Mohon as Jum_Mohon,  a.Tempat_Perjalanan,  FORMAT(a.Tarikh_Mula, 'yyyy-MM-dd') as Tarikh_Mula, FORMAT(a.Tarikh_Tamat, 'yyyy-MM-dd') as Tarikh_Tamat, a.Tempoh_Pjln, a.Status, a.JenisTempat,
                        FORMAT(a.Tkh_Adv_Perlu, 'yyyy-MM-dd') as Tkh_Adv_Perlu,                        
                        (SELECT  Butiran FROM   SMKB_Lookup_Detail AS jt WHERE (Kod = 'AC03') AND (a.JenisTempat = Kod_Detail)) AS ButiranJenisTempat , 
                        a.JenisTugas, (SELECT Butiran FROM  SMKB_Lookup_Detail as jtg WHERE jtg.Kod='AC04' AND (a.JenisTugas = jtg.Kod_Detail)) AS ButiranJenisTugas,
                        a.JenisPjln, (SELECT  Butiran  FROM  SMKB_Lookup_Detail as jp WHERE jp.Kod='AC02'  AND (a.JenisPjln = jp.Kod_Detail)) AS ButiranJenisPjln,
                        a.Jenis_Penginapan, (SELECT  Butiran  FROM  SMKB_Lookup_Detail as jpn WHERE jpn.Kod='AC01'  AND (a.Jenis_Penginapan = jpn.Kod_Detail)) AS ButiranJenisPginap,
                        a.CaraBayar, (SELECT  Butiran  FROM  SMKB_Lookup_Detail as jb WHERE jb.Kod='0018'  AND (a.CaraBayar = jb.Kod_Detail)) AS ButiranJenisBayar,
                        a.Kod_Kump_Wang,(select Butiran from SMKB_Kump_Wang as kw where a.Kod_Kump_Wang = kw.Kod_Kump_Wang) as colKW,
                        '00' as colhidko, (select Butiran from SMKB_Operasi as ko where  ko.Kod_Operasi = '00') as colKO,
                        '0000000' as colhidkp, (select Butiran from SMKB_Projek as kp where kp.Kod_Projek = '0000000') as colKp,
                        a.Ptj, (SELECT b.Pejabat FROM [DEVMIS\SQL_INS01].dbStaf.dbo.MS_PEJABAT as b WHERE b.STATUS = 1 and b.kodpejabat = left(Ptj,2)) as ButiranPTJ ,
                        a.Kod_Vot, (select Kod_Vot+' - '+ Butiran from SMKB_Vot as vot where a.Kod_Vot = vot.Kod_Vot) as ButiranVot, a.Status_Dok,
                        b.Butiran, a.No_Staf as Nopemohon, a.If_Mkn, a.Folder,a.File_Name, a.File_Name as url 
                        FROM SMKB_Pendahuluan_Hdr as a INNER JOIN 
                        SMKB_Kod_Status_Dok AS b ON a.Status_Dok = b.Kod_Status_Dok INNER JOIN
                        [qa11].dbStaf.dbo.MS01_Peribadi as c ON a.No_Staf = c.ms01_noStaf
                        WHERE   (b.Kod_Modul = '09') AND (b.Kod_Status_Dok = '05') AND a.Jenis_Pendahuluan='PD'  " & tarikhQuery & " order by a.Tarikh_Mohon desc"


        param.Add(New SqlParameter("@staffP", staffP))

        Return db.Read(query, param)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadOrderRecordPP(category_filter As String, isClicked As Boolean, tkhMula As DateTime, tkhTamat As DateTime, staffP As String) As String
        Dim resp As New ResponseRepository

        If isClicked = False Then
            Return JsonConvert.SerializeObject(New DataTable)
        End If

        dt = GetRecord_SenaraiPP(category_filter, tkhMula, tkhTamat, staffP)
        resp.SuccessPayload(dt)
        'resp.GetResult()
        Return JsonConvert.SerializeObject(dt)
    End Function

    Private Function GetRecord_SenaraiPP(category_filter As String, tkhMula As DateTime, tkhTamat As DateTime, staffP As String) As DataTable
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

        Dim query As String = "SELECT a.No_Pendahuluan, a.Tujuan, a.Tempat_Perjalanan,  FORMAT(a.Tarikh_Mula, 'yyyy-MM-dd') as Tarikh_Mula, FORMAT(a.Tarikh_Tamat, 'yyyy-MM-dd') as Tarikh_Tamat, a.Status, 
                            FORMAT(a.Tkh_Adv_Perlu, 'yyyy-MM-dd') as Tkh_Adv_Perlu,FORMAT(a.Tarikh_Mohon, 'yyyy-MM-dd') AS Tarikh_Mohon, 
                            a.Jum_Mohon as Jum_Mohon,  a.CaraBayar, (SELECT  Butiran  FROM  SMKB_Lookup_Detail as jb WHERE jb.Kod='0018'  AND (a.CaraBayar = jb.Kod_Detail)) AS ButiranJenisBayar,                       
                            a.Kod_Kump_Wang,(select Butiran from SMKB_Kump_Wang as kw where a.Kod_Kump_Wang = kw.Kod_Kump_Wang) as colKW,
                            '00' as colhidko, (select Butiran from SMKB_Operasi as ko where  ko.Kod_Operasi = '00') as colKO,
                            '0000000' as colhidkp, (select Butiran from SMKB_Projek as kp where kp.Kod_Projek = '0000000') as colKp,
                            a.Ptj, (SELECT b.Pejabat FROM [DEVMIS\SQL_INS01].dbStaf.dbo.MS_PEJABAT as b WHERE b.STATUS = 1 and b.kodpejabat = left(Ptj,2)) as ButiranPTJ ,
                            a.Kod_Vot, (select Kod_Vot+' - '+ Butiran from SMKB_Vot as vot where a.Kod_Vot = vot.Kod_Vot) as ButiranVot, a.Status_Dok,
                            b.Butiran, a.No_Staf + ' - ' + c.ms01_nama   as NamaPemohon, a.No_Staf as Nopemohon, a.Justifikasi_Prgm, a.Peruntukan_Prgm
                            FROM SMKB_Pendahuluan_Hdr as a INNER JOIN 
                            SMKB_Kod_Status_Dok AS b ON a.Status_Dok = b.Kod_Status_Dok INNER JOIN
                            [qa11].dbStaf.dbo.MS01_Peribadi as c ON a.No_Staf = c.ms01_noStaf
                            WHERE  a.Jenis_Pendahuluan='PP' and (b.Kod_Modul = '09') AND (b.Kod_Status_Dok = '05') " & tarikhQuery & " order by a.Tarikh_Mohon desc"

        param.Add(New SqlParameter("@staffP", staffP))

        Return db.Read(query, param)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetDataPelulus(mohonID As String)
        Dim db As New DBKewConn
        Dim query As String = $"SELECT   DISTINCT     a.No_Pendahuluan,  a.PTj, a.Jenis_Pendahuluan, b.No_Staf +' - '+ d.ms01_nama as NamaPelulus, f.JawGiliran as Jawatan
                FROM            SMKB_Pendahuluan_Hdr AS a INNER JOIN
                SMKB_Status_Dok AS b ON a.Status_Dok = b.Kod_Status_Dok AND a.No_Pendahuluan = b.No_Rujukan INNER JOIN
                [qa11].dbStaf.[dbo].[MS_KetuaPejPBU] as c ON a.ptj = c.KodPejPBU INNER JOIN
                [qa11].dbStaf.dbo.MS01_Peribadi as d ON b.No_Staf = d.ms01_nostaf INNER JOIN
                [qa11].dbStaf.dbo.MS02_Perjawatan as e ON b.No_Staf = e.MS01_NoStaf  INNER JOIN
                [qa11].dbStaf.dbo.MS_Jawatan as f ON e.MS02_JawSandang = f.KodJawatan
                where b.Kod_Modul='09' AND a.Status_Dok='05' AND a.No_Pendahuluan='{mohonID}' "
        Dim dt As DataTable = db.fSelectCommandDt(query)

        Return JsonConvert.SerializeObject(dt)
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
    Public Function GetHadMin(hadMin As String)
        Dim db As New DBKewConn
        Dim query As String = $"SELECT ID_HadMin, Dari, Hingga, Kump, HadMin as param6, Status
                                FROM            SMKB_Pendahuluan_Had_Min
                                WHERE Dari <='{hadMin}' AND Hingga >='{hadMin}'"
        Dim dt As DataTable = db.fSelectCommandDt(query)

        Return JsonConvert.SerializeObject(dt)
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
                    a.Kod_PTJ as colhidpt ,a.Kod_Vot AS colhidVot , a.Kod_Operasi as colhidko , a.Kod_Projek as colhidkp
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
    Public Function GetKiraAdv(jtugas As String, jtempat As String, jjalan As String)
        Dim db As New DBKewConn
        Dim query As String = $"SELECT JenisTugas, Tempat, GredDari, GredKe, KadarMkn, KadarHotel, KadarLojing, KumpGred
                                FROM            SMKB_CLM_KdrMknHtlLjg
                                WHERE (GredDari<='41' AND GredKe >='41') AND JenisTugas='K' AND Tempat='SM'"
        Dim dt As DataTable = db.fSelectCommandDt(query)

        Return JsonConvert.SerializeObject(dt)
    End Function


    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetJenisTugas(ByVal q As String) As String

        Dim tmpDT As DataTable = GetDataJenisTugas(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetDataJenisTugas(kod As String) As DataTable
        Dim db As New DBKewConn
        Dim query As String = "SELECT Kod_Detail as value,  Kod_Detail + ' - '+ Butiran as text  FROM   
                    SMKB_Lookup_Detail WHERE Kod='AC04' AND Kod_Korporat='UTeM' AND status=1"
        Dim param As New List(Of SqlParameter)
        If kod <> "" Then
            query &= " AND Kod_Detail='K' OR Kod_Detail='R' AND 
                        Butiran like 'Rasmi%' and (Kod_Detail LIKE '%' + @kod + '%' or Butiran LIKE '%' + @kod2 + '%') "
            param.Add(New SqlParameter("@kod", kod))
            param.Add(New SqlParameter("@kod2", kod))
        End If

        Return db.Read(query, param)

    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetPenginapan(ByVal q As String) As String


        Dim tmpDT As DataTable = GetDataPenginapan(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetDataPenginapan(kod As String) As DataTable
        Dim db As New DBKewConn
        Dim query As String = "SELECT Kod_Detail as value, Kod_Detail + ' - '+ Butiran as text  
        FROM   SMKB_Lookup_Detail WHERE (Kod = '0146') AND Kod_Korporat='UTeM' AND status=1"
        Dim param As New List(Of SqlParameter)
        If kod <> "" Then
            query &= " AND Kod_Detail ='H' OR Kod_Detail='L' AND 
                        Butiran like 'Hotel%' and (Kod_Detail LIKE '%' + @kod + '%' or Butiran LIKE '%' + @kod2 + '%') "
            param.Add(New SqlParameter("@kod", kod))
            param.Add(New SqlParameter("@kod2", kod))
        End If

        Return db.Read(query, param)

    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetTempat(ByVal q As String) As String


        Dim tmpDT As DataTable = GetDataTempat(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetDataTempat(kod As String) As DataTable
        Dim db As New DBKewConn
        Dim query As String = "SELECT Kod_Detail as value, Kod_Detail + ' - '+ Butiran as text  
        FROM   SMKB_Lookup_Detail WHERE (Kod = 'AC03') AND Kod_Korporat='UTeM' AND status=1"
        Dim param As New List(Of SqlParameter)
        If kod <> "" Then
            query &= " AND Kod_Detail ='H' OR Kod_Detail='L' AND 
                        Butiran like 'Hotel%' and (Kod_Detail LIKE '%' + @kod + '%' or Butiran LIKE '%' + @kod2 + '%') "
            param.Add(New SqlParameter("@kod", kod))
            param.Add(New SqlParameter("@kod2", kod))
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
        Dim query As String = "SELECT Kod_Detail as value, Kod_Detail + ' - '+  Butiran as text  
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
    Public Function GetJenisJalan(ByVal q As String) As String


        Dim tmpDT As DataTable = GetDataPerjalanan(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetDataPerjalanan(kod As String) As DataTable
        Dim db As New DBKewConn
        Dim query As String = "SELECT Kod_Detail as value, Kod_Detail + ' - '+ Butiran as text  FROM   
                    SMKB_Lookup_Detail WHERE Kod='AC02' AND Kod_Korporat='UTeM' AND status=1"
        Dim param As New List(Of SqlParameter)
        If kod <> "" Then
            query &= " WHERE Kod_Detail ='DN' OR Kod ='LN' and Butiran like 'DALAM%'  and (Kod_Detail LIKE '%' + @kod + '%' or Butiran LIKE '%' + @kod2 + '%') "
            param.Add(New SqlParameter("@kod", kod))
            param.Add(New SqlParameter("@kod2", kod))
        End If

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
        Dim query As String = "SELECT  MS01_NoStaf as StafNo, MS01_Nama , MS08_Pejabat, JawGiliran, right(MS02_GredGajiS,2) as GredGaji
                    FROM VK_AdvClm "

        Dim param As New List(Of SqlParameter)

        If kodPjbt <> "" Then

            query &= "WHERE MS08_Pejabat = @kodPjbt  AND RIGHT(MS02_GredGajiS,2) >='41' "
            param.Add(New SqlParameter("@kodPjbt", kodPjbt))

        End If

        Return db.Read(query, param)

    End Function

    <System.Web.Services.WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SaveRecordPenerimaan(Terimaan As Terimaan) As String
        Dim resp As New ResponseRepository
        resp.Success("Data telah disimpan")
        Dim success As Integer = 0
        Dim JumRekod As Integer = 0
        Dim strTkhToday2 As String = Now.ToString("yyyy-MM-dd")
        Dim dtTkhToday2 As DateTime = CDate(strTkhToday2)

        If Terimaan Is Nothing Then
            resp.Failed("Tiada simpan")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If



        If Terimaan.mohonID = "" Then 'untuk permohonan baru
            resp.Failed("Tiada simpan")
            Return JsonConvert.SerializeObject(resp.GetResult())
        Else
            If UpdatePenerimaan(Terimaan) <> "OK" Then
                resp.Failed("Gagal Menyimpan order 1266")
                Return JsonConvert.SerializeObject(resp.GetResult())
            End If

        End If

        If UpdateStatusDokPenerimaan(Terimaan, Terimaan.statusDok) <> "OK" Then

            'resp.Failed("Berjaya simpan") 'Gagal Menyimpan order YX
            Return JsonConvert.SerializeObject(resp.GetResult())
            ' Exit Function
        End If


        resp.Success("Rekod berjaya disimpan", "00", Terimaan)
            Return JsonConvert.SerializeObject(resp.GetResult())
    End Function


    Private Function UpdatePenerimaan(Terimaan As Terimaan)
        Dim query As String = ""
        Dim db As New DBKewConn
        Dim param = New List(Of SqlParameter)

        If Terimaan.Tempoh = "" Then

            query = "UPDATE SMKB_Pendahuluan_Hdr SET  Tarikh_Mula = @Tarikh_Mula, Tarikh_Tamat = @Tarikh_Tamat, 
                                Jum_Mohon = @Jum_Mohon, Jum_Lulus = @jum_Lulus, Status_Dok = @status                                
                                WHERE No_Pendahuluan = @No_Pendahuluan AND Status = 1"

        Else

            query = "UPDATE SMKB_Pendahuluan_Hdr SET  Tarikh_Mula = @Tarikh_Mula, Tarikh_Tamat = @Tarikh_Tamat, Tempoh_Pjln = @Tempoh_Pjln, 
                                Jum_Mohon = @Jum_Mohon, Jum_Lulus = @jum_Lulus, Status_Dok = @status                                
                                WHERE No_Pendahuluan = @No_Pendahuluan AND Status = 1"

            param.Add(New SqlParameter("@Tempoh_Pjln", Terimaan.Tempoh))

        End If


        param.Add(New SqlParameter("@Tarikh_Mula", Terimaan.TkhMula))
        param.Add(New SqlParameter("@Tarikh_Tamat", Terimaan.TkhTamat))
        param.Add(New SqlParameter("@Jum_Mohon", Terimaan.jumlahMohon))
        param.Add(New SqlParameter("@jum_Lulus", Terimaan.jumlahLulus))
        param.Add(New SqlParameter("@status", Terimaan.statusDok))
        param.Add(New SqlParameter("@No_Pendahuluan", Terimaan.mohonID))
        Return db.Process(query, param)
    End Function

    Private Function UpdateStatusDokPenerimaan(Terimaan As Terimaan, statusLulus As String)
        Dim db As New DBKewConn

        'Dim kodstatusLulus As String

        'If statusLulus = "Y" Then

        '    kodstatusLulus = "06"

        'End If


        Dim query As String = "INSERT INTO SMKB_Status_Dok (Kod_Modul  , Kod_Status_Dok  ,  No_Rujukan , No_Staf , Tkh_Tindakan , Tkh_Transaksi , Status_Transaksi , Status , Ulasan )
    			VALUES
    			(@Kod_Modul , @Kod_Status_Dok , @No_Rujukan , @No_Staf , getdate() , getdate(), @Status_Transaksi , @Status , @Ulasan)"

        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@Kod_Modul", "09"))
        param.Add(New SqlParameter("@Kod_Status_Dok", Terimaan.statusDok))
        param.Add(New SqlParameter("@No_Rujukan", Terimaan.mohonID))
        param.Add(New SqlParameter("@No_Staf", Session("ssusrID")))
        param.Add(New SqlParameter("@Status_Transaksi", 1))
        param.Add(New SqlParameter("@Status", 1))
        param.Add(New SqlParameter("@Ulasan", Terimaan.catatan))

        Return db.Process(query, param)

    End Function
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadRecordPermohonan(ByVal id As String) As String
        Dim resp As New ResponseRepository

        dt = GetDataPermohonan(id)
        resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function


    Private Function GetDataPermohonan(kod As String) As DataTable
        Dim db = New DBKewConn

        Dim query As String = "SELECT a.No_Pendahuluan, a.Tujuan, a.Tempat_Perjalanan, a.Tarikh_Mula, a.Tarikh_Tamat, a.Tempoh_Pjln, a.Status, a.JenisTempat,a.Tkh_Adv_Perlu,
                    (SELECT  Butiran FROM   SMKB_Lookup_Detail AS jt WHERE (Kod = 'AC03') AND (a.JenisTempat = Kod_Detail)) AS ButiranJenisTempat, 
                    a.JenisTugas, (SELECT Butiran FROM  SMKB_Lookup_Detail as jtg WHERE jtg.Kod='AC04' AND (a.JenisTugas = jtg.Kod_Detail)) AS ButiranJenisTugas,
                    a.JenisPjln, (SELECT  Butiran  FROM  SMKB_Lookup_Detail as jp WHERE jp.Kod='AC02'  AND (a.JenisPjln = jp.Kod_Detail)) AS ButiranJenisPjln,
                    a.JenisPginap, (SELECT  Butiran  FROM  SMKB_Lookup_Detail as jpn WHERE jpn.Kod='AC01'  AND (a.JenisPginap = jpn.Kod_Detail)) AS ButiranJenisPginap,
                    a.CaraBayar, (SELECT  Butiran  FROM  SMKB_Lookup_Detail as jb WHERE jb.Kod='0018'  AND (a.CaraBayar = jb.Kod_Detail)) AS ButiranJenisBayar,
                    Kump_Wang as colhidkw ,(select Butiran from SMKB_Kump_Wang as kw where a.Kump_Wang = kw.Kod_Kump_Wang) as colKW,
                    '00' as colhidko, (select Butiran from SMKB_Operasi as ko where  ko.Kod_Operasi = '00') as colKO,
                    '0000000' as colhidkp,  (select Butiran from SMKB_Projek as kp where kp.Kod_Projek = '0000000') as colKp,
                    Ptj as colhidptj , (SELECT b.Pejabat FROM [DEVMIS\SQL_INS01].dbStaf.dbo.MS_PEJABAT as b WHERE b.STATUS = 1 and b.kodpejabat = left(Ptj,2)) as ButiranPTJ ,
                    KodVot , (select Kod_Vot+' - '+ Butiran from SMKB_Vot as vot where a.KodVot = vot.Kod_Vot) as ButiranVot,
                    a.Folder,a.File_Name
                    FROM SMKB_Pendahuluan_Hdr as a
                    WHERE (a.No_Pendahuluan = @No_Permohonan)"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@No_Permohonan", kod))

        Return db.Read(query, param)
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
    Public Function LoadListingKeperluan(ByVal id As String) As String
        Dim resp As New ResponseRepository

        dt = GetDataKeperluan(id)
        resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    Private Function GetDataKeperluan(id As String) As DataTable
        Dim db = New DBKewConn

        Dim query As String = "SELECT  a.No_Pendahuluan, a.No_Item, a.Butiran, a.Kuantiti, a.Kadar_Harga, a.Jumlah_anggaran, b.Jum_Mohon
                            FROM            SMKB_Pendahuluan_Dtl a INNER JOIN SMKB_Pendahuluan_Hdr as b ON a.No_Pendahuluan = b.No_Pendahuluan
                            WHERE a.No_Pendahuluan= @No_Permohonan
                            ORDER BY a.No_Item ASC"


        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@No_Permohonan", id))

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
End Class