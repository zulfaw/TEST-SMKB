Imports System.ComponentModel
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports Newtonsoft.Json
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Web



' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class Pendahuluan_WS
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
    Public Function GetJenisKumpWang(ByVal q As String) As String
        'Dim newList As List(Of ItemList)
        'newList = listItem

        'If (q <> "") Then
        '    newList = listItem.Where(
        '        Function(x) x.value.Contains(q) Or x.text.Contains(q)
        '    ).ToList()
        'End If

        Dim tmpDT As DataTable = GetDataKumpWang(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetJenisOperasi(ByVal q As String) As String
        'Dim newList As List(Of ItemList)
        'newList = listItem

        'If (q <> "") Then
        '    newList = listItem.Where(
        '        Function(x) x.value.Contains(q) Or x.text.Contains(q)
        '    ).ToList()
        'End If

        Dim tmpDT As DataTable = GetDataOperasi(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetJenisProjek(ByVal q As String) As String
        'Dim newList As List(Of ItemList)
        'newList = listItem

        'If (q <> "") Then
        '    newList = listItem.Where(
        '        Function(x) x.value.Contains(q) Or x.text.Contains(q)
        '    ).ToList()
        'End If

        Dim tmpDT As DataTable = GetDataProjek(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function


    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetPenginapan(ByVal q As String) As String


        Dim tmpDT As DataTable = GetDataPenginapan(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetTempat(ByVal q As String) As String


        Dim tmpDT As DataTable = GetDataTempat(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function
    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetKaedahBayar(ByVal q As String) As String


        Dim tmpDT As DataTable = GetDataKaedahBayar(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetJenisTugas(ByVal q As String) As String


        Dim tmpDT As DataTable = GetDataJenisTugas(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetDataKaedahBayar(kod As String) As DataTable
        Dim db As New DBKewConn
        Dim query As String = "SELECT Kod_Detail as value, Kod_Detail + ' - ' + Butiran as text  
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

    Private Function GetDataPenginapan(kod As String) As DataTable
        Dim db As New DBKewConn
        Dim query As String = "SELECT Kod_Detail as value,  Kod_Detail +' - '+ Butiran as text  
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

    Private Function GetDataTempat(kod As String) As DataTable
        Dim db As New DBKewConn
        Dim query As String = "SELECT Kod_Detail as value, Kod_Detail +' - '+ Butiran as text  
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

    Private Function GetDataJenisTugas(kod As String) As DataTable
        Dim db As New DBKewConn
        Dim query As String = "SELECT Kod_Detail as value, Kod_Detail +' - '+ Butiran as text  FROM   
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
    Public Function GetJenisJalan(ByVal q As String) As String


        Dim tmpDT As DataTable = GetDataPerjalanan(q)
        Return JsonConvert.SerializeObject(tmpDT)
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

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetJenisVot(ByVal q As String) As String
        Dim tmpDT As DataTable = GetDataVot(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function



    Private Function GetDataPerjalanan(kod As String) As DataTable
        Dim db As New DBKewConn
        Dim query As String = "SELECT Kod_Detail as value, Kod_Detail +' - '+ Butiran as text  FROM   
                    SMKB_Lookup_Detail WHERE Kod='AC02' AND Kod_Korporat='UTeM' AND status=1"
        Dim param As New List(Of SqlParameter)
        If kod <> "" Then
            query &= " WHERE Kod_Detail ='DN' OR Kod ='LN' and Butiran like 'DALAM%'  and (Kod_Detail LIKE '%' + @kod + '%' or Butiran LIKE '%' + @kod2 + '%') "
            param.Add(New SqlParameter("@kod", kod))
            param.Add(New SqlParameter("@kod2", kod))
        End If

        Return db.Read(query, param)

    End Function
    Private Function GetDataVot(kod As String) As DataTable
        Dim db As New DBKewConn
        Dim query As String = "SELECT DISTINCT A.Kod_Vot as value, A.Kod_Detail +' - '+ B.Butiran as text
                                FROM SMKB_COA_Master A
                                INNER JOIN SMKB_Vot B ON A.Kod_Vot=B.Kod_Vot"

        Dim param As New List(Of SqlParameter)
        If kod <> "" Then
            query &= " where A.Status = 1 and (A.Kod_Vot LIKE '%' + @kod + '%' or B.Butiran LIKE '%' + @kod2 + '%') and A.Kod_PTJ =@kod3 order by A.Kod_Vot"
            'Else
            '    query &= " where A.Status = 1 and A.Kod_PTJ =@kod3 order by A.Kod_Vot"
        End If

        param.Add(New SqlParameter("@kod", kod))
        param.Add(New SqlParameter("@kod2", kod))
        param.Add(New SqlParameter("@kod3", kod))

        Return db.Read(query, param)
    End Function

    Private Function GetDataProjek(kod As String) As DataTable
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
    Private Function GetDataOperasi(kod As String) As DataTable
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

    Private Function GetDataKumpWang(kod As String) As DataTable
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
    Public Function GetListPTJStaf(ByVal q As String) As String
        'Dim newList As List(Of ItemList)
        'newList = listItem

        'If (q <> "") Then
        '    newList = listItem.Where(
        '        Function(x) x.value.Contains(q) Or x.text.Contains(q)
        '    ).ToList()
        'End If

        Dim tmpDT As DataTable = GetDataListPTJStaf(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function


    Private Function GetDataListPTJStaf(kod As String) As DataTable
        Dim db As New DBKewConn
        Dim query As String = "SELECT  MS01_NoStaf, MS01_Nama, MS08_Pejabat, MS02_GredGajiS, JawGiliran
                        FROM            VK_AdvClm  "

        Dim param As New List(Of SqlParameter)
        If kod <> "" Then
            query &= "WHERE MS08_Pejabat = '43' AND RIGHT(MS02_GredGajiS,2) >=@kod "

            param.Add(New SqlParameter("@kod", kod))

        End If

        Return db.Read(query, param)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function fnCariStaf(ByVal q As String) As String


        Dim tmpDT As DataTable = GetListStaf(q)
        Return JsonConvert.SerializeObject(tmpDT)
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

    'Private Function InsertOrderDetail(orderDetail As OrderDetail)
    '    Dim db As New DBKewConn
    '    Dim query As String = "INSERT INTO SMKB_Jurnal_Dtl
    '    VALUES(@No_Jurnal , @No_Item, @Kod_Kump_Wang, @Kod_Operasi , @Kod_Projek, @Kod_PTJ  , @Kod_Vot ,
    '    @Butiran, @Debit, @Kredit, @Kod_Penyesuaian, @ID_Penyesuaian, @Status_Lejar, NULL , @status)"
    '    Dim param As New List(Of SqlParameter)

    '    param.Add(New SqlParameter("@No_Jurnal", orderDetail.OrderID))
    '    param.Add(New SqlParameter("@No_Item", orderDetail.id))
    '    param.Add(New SqlParameter("@Kod_Kump_Wang", orderDetail.ddlKw))
    '    param.Add(New SqlParameter("@Kod_Operasi", orderDetail.ddlKo))
    '    param.Add(New SqlParameter("@Kod_Projek", orderDetail.ddlKp))
    '    param.Add(New SqlParameter("@Kod_PTJ", orderDetail.ddlPTJ))
    '    param.Add(New SqlParameter("@Kod_Vot", orderDetail.ddlVot))
    '    param.Add(New SqlParameter("@Butiran", "test"))
    '    param.Add(New SqlParameter("@Debit", orderDetail.debit))
    '    param.Add(New SqlParameter("@Kredit", orderDetail.kredit))
    '    param.Add(New SqlParameter("@Kod_Penyesuaian", "U"))
    '    param.Add(New SqlParameter("@ID_Penyesuaian", "-"))
    '    param.Add(New SqlParameter("@Status_Lejar", "-"))
    '    param.Add(New SqlParameter("@Status", 1))

    '    Return db.Process(query, param)
    'End Function

    'Private Function UpdateOrderDetail(orderDetail As OrderDetail)
    '    Dim db = New db
    '    Dim query As String = "UPDATE ORDERDETAILS
    '    set ddlVot = @ddlVot, details = @details, quantity = @quantity, 
    '    price = @price, amount = @amount
    '    where id = @id"
    '    Dim param As New List(Of SqlParameter)

    '    param.Add(New SqlParameter("@ddlVot", orderDetail.ddlVot))
    '    param.Add(New SqlParameter("@price", orderDetail.debit))
    '    param.Add(New SqlParameter("@amount", orderDetail.kredit))
    '    param.Add(New SqlParameter("@id", orderDetail.id))

    '    Return db.Process(query, param)
    'End Function

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
        Dim query As String = "SELECT DISTINCT A.Kod_Vot as value, A.Kod_Vot + ' - ' + B.Butiran as text
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
        Dim query As String = "SELECT DISTINCT A.Kod_Kump_Wang as value, A.Kod_Kump_Wang +' - '+ B.Butiran as text
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

    Private Function GetKodPtjList2(kod As String) As DataTable
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


        Dim query As String = "SELECT a.No_Pendahuluan, a.Tarikh_Mohon,  FORMAT(a.Tarikh_Mohon,'dd-MM-yyyy') as Tarikh_MohonDisplay, a.No_Staf + ' - ' + c.ms01_nama   as NamaPemohon, 
a.Tujuan, a.Jum_Mohon as Jum_Mohon,  a.Tempat_Perjalanan,  FORMAT(a.Tarikh_Mula, 'yyyy-MM-dd') as Tarikh_Mula, FORMAT(a.Tarikh_Tamat, 'yyyy-MM-dd') as Tarikh_Tamat, a.Tempoh_Pjln, a.Status, a.JenisTempat,
                        FORMAT(a.Tkh_Adv_Perlu, 'yyyy-MM-dd') as Tkh_Adv_Perlu ,FORMAT(a.Tarikh_Mohon,'yyyy-MM-dd') as TkhMohonPapar,                      
                        (SELECT  Butiran FROM   SMKB_Lookup_Detail AS jt WHERE (Kod = 'AC03') AND (a.JenisTempat = Kod_Detail)) AS ButiranJenisTempat , 
                        a.JenisTugas, (SELECT Butiran FROM  SMKB_Lookup_Detail as jtg WHERE jtg.Kod='AC04' AND (a.JenisTugas = jtg.Kod_Detail)) AS ButiranJenisTugas,
                        a.JenisPjln, (SELECT  Butiran  FROM  SMKB_Lookup_Detail as jp WHERE jp.Kod='AC02'  AND (a.JenisPjln = jp.Kod_Detail)) AS ButiranJenisPjln,
                        a.Jenis_Penginapan, (SELECT  Butiran  FROM  SMKB_Lookup_Detail as jpn WHERE jpn.Kod='AC01'  AND (a.Jenis_Penginapan = jpn.Kod_Detail)) AS ButiranJenisPginap,
                        a.CaraBayar, (SELECT  Butiran  FROM  SMKB_Lookup_Detail as jb WHERE jb.Kod='AC14'  AND (a.CaraBayar = jb.Kod_Detail)) AS ButiranJenisBayar,
                        a.Kod_Kump_Wang,(select Butiran from SMKB_Kump_Wang as kw where a.Kod_Kump_Wang = kw.Kod_Kump_Wang) as colKW,
                        '00' as colhidko, (select Butiran from SMKB_Operasi as ko where  ko.Kod_Operasi = '00') as colKO,
                        '0000000' as colhidkp, (select Butiran from SMKB_Projek as kp where kp.Kod_Projek = '0000000') as colKp,
                        a.Ptj, (SELECT b.Pejabat FROM [DEVMIS\SQL_INS01].dbStaf.dbo.MS_PEJABAT as b WHERE b.STATUS = 1 and b.kodpejabat = left(Ptj,2)) as ButiranPTJ ,
                        a.Kod_Vot, (select Kod_Vot+' - '+ Butiran from SMKB_Vot as vot where a.Kod_Vot = vot.Kod_Vot) as ButiranVot, a.Status_Dok,
                        b.Butiran, a.No_Staf as Nopemohon, a.If_Mkn, a.Folder,a.File_Name, a.File_Name as url 
                        FROM SMKB_Pendahuluan_Hdr as a INNER JOIN 
                        SMKB_Kod_Status_Dok AS b ON a.Status_Dok = b.Kod_Status_Dok INNER JOIN
                        [qa11].dbStaf.dbo.MS01_Peribadi as c ON a.No_Staf = c.ms01_noStaf
                        WHERE   (b.Kod_Modul = '09') AND (b.Kod_Status_Dok = '01') AND a.Jenis_Pendahuluan='PD'  " & tarikhQuery & " order by a.Tarikh_Mohon desc"


        param.Add(New SqlParameter("@staffP", staffP))

        Return db.Read(query, param)
    End Function


    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadOrderRecord_PermohonanSendiri(category_filter As String, isClicked As Boolean, tkhMula As DateTime, tkhTamat As DateTime, staffP As String) As String
        'LoadOrderRecord_PermohonanSendiri(ByVal id As String) As String
        Dim resp As New ResponseRepository

        If isClicked = False Then
            Return JsonConvert.SerializeObject(New DataTable)
        End If

        dt = GetRecord_SenaraiSendiri(category_filter, tkhMula, tkhTamat, staffP)

        'resp.SuccessPayload(dt)
        For Each x As DataRow In dt.Rows
            If Not IsDBNull(x.Item("File_Name")) Then
                Dim url As String = GetBaseUrl() + Trim(x.Item("Folder")) + "/" + x.Item("File_Name")
                x.Item("url") = url
            End If
        Next
        Return JsonConvert.SerializeObject(dt)

        'dt = GetRecord_SenaraiSendiri(id)
        'resp.SuccessPayload(dt)
        ''resp.GetResult()
        'Return JsonConvert.SerializeObject(dt)
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
    Public Function LoadRecordPermohonan(ByVal id As String) As String
        Dim resp As New ResponseRepository

        dt = GetDataPermohonan(id)
        resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(resp.GetResult())
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
    Public Function LoadRecordPTJ(ByVal id As String) As String
        Dim resp As New ResponseRepository
        Dim jumRekod As Integer = 0
        Dim recordsFiltered As Integer = 0
        Dim draw As String = ""
        If id = "0" Then
            'resp.Failed("Tiada ptj dipilih")
            Return JsonConvert.SerializeObject(New With
               {.draw = draw, .recordsTotal = jumRekod, .recordsFiltered = recordsFiltered, .data = New DataTable()})

            'Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        dt = GetRecord_SenaraiPTJ(id, draw)
        resp.SuccessPayload(dt)

        jumRekod = dt.Rows.Count
        recordsFiltered = jumRekod
        Return JsonConvert.SerializeObject(New With
        {.draw = draw, .recordsTotal = jumRekod, .recordsFiltered = recordsFiltered, .data = dt})

        'Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    Private Function GetRecord_SenaraiPTJ(idPejabat As String, Optional draw As String = “”) As DataTable
        Dim db = New DBSMConn


        Dim query As String = "SELECT  MS01_NoStaf as StafNo, MS01_Nama , MS08_Pejabat, JawGiliran
                    FROM VK_AdvClm WHERE MS08_Pejabat = @idPejabat  "
        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@idPejabat", idPejabat))
        Return db.Read(query, param)
    End Function

    Private Function GenerateOrderID() As String
        Dim db As New DBKewConn

        Dim year = Date.Now.ToString("yyyy")
        Dim month = Date.Now.Month

        Dim lastID As Integer = 1
        Dim newOrderID As String = ""

        Dim query As String = $"select TOP 1 No_Akhir as id from SMKB_No_Akhir where Kod_Modul ='09' AND Prefix ='AV' AND Tahun =@year"
        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@year", year))

        dt = db.Read(query, param)

        If dt.Rows.Count > 0 Then
            lastID = CInt(dt.Rows(0).Item("id")) + 1

            UpdateNoAkhir("09", "AV", year, lastID)
        Else

            InsertNoAkhir("09", "AV", year, lastID)
        End If
        newOrderID = "AV" + Format(lastID, "000000").ToString + month.ToString("00") + Right(year.ToString(), 2)

        Return newOrderID
    End Function


    <System.Web.Services.WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SaveRecordAdv(AdvList As MhnAdvance) As String
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



        If AdvList.OrderID = "" Then 'untuk permohonan baru
            AdvList.OrderID = GenerateOrderID()
            AdvList.Tkh_Upload = strTkhToday2
            AdvList.Folder = "UPLOAD/DOCUMENT/PENDAHULUAN DAN TUNTUTAN/PD/"

            'InsertNewOrder(AdvList.OrderID, AdvList.stafID, AdvList.kodPTj, AdvList.noTel, AdvList.Tujuan, AdvList.Lokasi, AdvList.ArahanK, AdvList.JnsTempat,
            'AdvList.JnsTugas, AdvList.JnsJalan, AdvList.TkhMula, AdvList.TkhTamat, AdvList.Tempoh, AdvList.hadMin, AdvList.JumlahAll, AdvList.stafID,
            ' AdvList.KodVot, AdvList.kodKW, AdvList.kodKO, AdvList.kodKP, AdvList.TkhAdvance, AdvList.JnsPginapan, AdvList.JnsBayar)
            If InsertNewOrder(AdvList) <> "OK" Then
                resp.Failed("Gagal Menyimpan order 1266")
                Return JsonConvert.SerializeObject(resp.GetResult())
                ' Exit Function
            End If
        Else 'untuk permohonan sedia ada
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
            End If



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


    'orderid As String, Nostaf As String, Ptj As String, notel As String, Tujuan As String, lokasi As String, RujArahan As String, JnsTmpt As String, JnsTugas As String,
    'JenisPjln As String, Tarikh_Mula As String, Tarikh_Tamat As String, Tempoh_Pjln As String, Jum_Layak As Decimal, Jum_Mohon As Decimal,
    'Id_Mohon As String, KodVot As String, Kump_Wang As String, KodKo As String, KodProjek As String, Tkh_Adv_Perlu As String,
    'JenisPginap As String, CaraBayar As String
    Private Function InsertNewOrder(AdvList As MhnAdvance)
        Dim db As New DBKewConn
        Dim query As String = "INSERT INTO SMKB_Pendahuluan_Hdr(No_Pendahuluan, No_Staf, Samb_Telefon, PTj, Kategori, Jenis_Pendahuluan, Tujuan, 
                                Tempat_Perjalanan, Negara, Kategori_Ngr, JenisTempat, JenisTugas, JenisPjln, 
                                Tarikh_Mula, Tarikh_Tamat, Tempoh_Pjln, Justifikasi_Prgm, Peruntukan_Prgm, Jum_Layak, 
                                Jum_Mohon, Jum_Lulus, Id_Mohon, Pengesahan_Pemohon, Kod_Kump_Wang, Kod_Operasi, Kod_Projek, Kod_PTJ, Kod_Vot,
                                Jenis_Penginapan, If_Mkn, Tkh_Adv_Perlu, Status_Dok, Status, CaraBayar, Tarikh_Mohon,
                                Folder, File_Name, Tkh_Upload)
                                VALUES(@No_Pendahuluan ,@No_Staf, @Samb_Telefon, @PTjMohon, @Kategori, @Jenis_Pendahuluan, @Tujuan,
                                @Tempat_Perjalanan, @Negara ,@Kategori_Ngr, @JenisTempat, @JenisTugas, @JenisPjln, 
                                @Tarikh_Mula, @Tarikh_Tamat, @Tempoh_Pjln, @Justifikasi_Prgm, @Peruntukan_Prgm, @Jum_Layak, 
                                @Jum_Mohon, @Jum_Lulus, @Id_Mohon,@PengesahanPemohon,  @Kod_Kump_Wang, @Kod_Operasi, @Kod_Projek, @Kod_PTJ, @Kod_Vot, 
                                @JenisPginap, @staMakan, @Tkh_Adv_Perlu, @Status_Dok, @Status, @CaraBayar, @TkhMohon, 
                                @Folder, @namafile, @tkhUpload)"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@No_Pendahuluan", AdvList.OrderID))
        param.Add(New SqlParameter("@No_Staf", AdvList.stafID))
        param.Add(New SqlParameter("@Samb_Telefon", AdvList.noTel))
        param.Add(New SqlParameter("@PTjMohon", AdvList.PtjMohon))
        param.Add(New SqlParameter("@Kategori", "-"))
        param.Add(New SqlParameter("@Jenis_Pendahuluan", "PD"))
        param.Add(New SqlParameter("@Tujuan", AdvList.Tujuan))
        param.Add(New SqlParameter("@Tempat_Perjalanan", AdvList.Lokasi))
        param.Add(New SqlParameter("@Negara", "-"))
        param.Add(New SqlParameter("@Kategori_Ngr", "-"))
        param.Add(New SqlParameter("@JenisTempat", AdvList.JnsTempat))
        param.Add(New SqlParameter("@JenisTugas", AdvList.JnsTugas))
        param.Add(New SqlParameter("@JenisPjln", AdvList.JnsJalan))
        param.Add(New SqlParameter("@Tarikh_Mula", AdvList.TkhMula))
        param.Add(New SqlParameter("@Tarikh_Tamat", AdvList.TkhTamat))
        param.Add(New SqlParameter("@Tempoh_Pjln", AdvList.Tempoh))
        param.Add(New SqlParameter("@Justifikasi_Prgm", "-"))
        param.Add(New SqlParameter("@Peruntukan_Prgm", "-"))
        param.Add(New SqlParameter("@Jum_Lulus", "0.00"))
        param.Add(New SqlParameter("@Jum_Layak", AdvList.hadMin))
        param.Add(New SqlParameter("@Jum_Mohon", AdvList.JumlahAll))
        param.Add(New SqlParameter("@Id_Mohon", Session("ssusrID")))
        param.Add(New SqlParameter("@PengesahanPemohon", AdvList.statusPemohon))
        param.Add(New SqlParameter("@Kod_Kump_Wang", AdvList.kodKW))
        param.Add(New SqlParameter("@Kod_Operasi", AdvList.kodKO))
        param.Add(New SqlParameter("@Kod_Projek", AdvList.kodKP))
        param.Add(New SqlParameter("@Kod_PTJ", AdvList.kodPTj))
        param.Add(New SqlParameter("@Kod_Vot", AdvList.KodVot))
        param.Add(New SqlParameter("@Tkh_Adv_Perlu", AdvList.TkhAdvance))
        param.Add(New SqlParameter("@Status_Dok", "01"))
        param.Add(New SqlParameter("@Status", "1"))
        param.Add(New SqlParameter("@JenisPginap", AdvList.JnsPginapan))
        param.Add(New SqlParameter("@staMakan", AdvList.staMakan))
        param.Add(New SqlParameter("@CaraBayar", AdvList.JnsBayar))
        param.Add(New SqlParameter("@TkhMohon", AdvList.tkhMohon))
        param.Add(New SqlParameter("@Folder", AdvList.Folder))
        param.Add(New SqlParameter("@namafile", AdvList.File_Name))
        param.Add(New SqlParameter("@tkhUpload", AdvList.Tkh_Upload))


        Return db.Process(query, param)
    End Function

    Private Function UpdateNewOrder(AdvList As MhnAdvance)


        Dim db As New DBKewConn
        Dim query As String = "UPDATE SMKB_Pendahuluan_Hdr SET Tujuan = @Tujuan, Tempat_Perjalanan = @Tempat_Perjalanan, JenisTempat= @JenisTempat,PTj = @PTjMohon,
                               JenisTugas = @JenisTugas, JenisPjln = @JenisPjln, Tarikh_Mula = @Tarikh_Mula, Tarikh_Tamat = @Tarikh_Tamat, Tempoh_Pjln = @Tempoh_Pjln, 
                                Kod_Kump_Wang = @Kod_Kump_Wang , Kod_Operasi = @Kod_Operasi, Kod_Projek = @Kod_Projek, Kod_PTJ = @Kod_PTJ, Kod_Vot= @Kod_Vot, Jenis_Penginapan = @JenisPginap, 
                                If_Mkn = @staMakan, Tkh_Adv_Perlu = @Tkh_Adv_Perlu,  CaraBayar = @CaraBayar, Jum_Mohon = @Jum_Mohon, Tarikh_Mohon = @TkhMohon, Id_Mohon = @Id_Mohon,
                                Folder = @folder, [File_Name]= @namafile, Tkh_Upload= @tkhUpload
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
        param.Add(New SqlParameter("@TkhMohon", AdvList.tkhMohon))
        param.Add(New SqlParameter("@Id_Mohon", Session("ssusrID")))
        param.Add(New SqlParameter("@Folder", AdvList.Folder))
        param.Add(New SqlParameter("@namafile", AdvList.File_Name))
        param.Add(New SqlParameter("@tkhUpload", AdvList.Tkh_Upload))
        param.Add(New SqlParameter("@No_Pendahuluan", AdvList.OrderID))

        'param.Add(New SqlParameter("@PTj", AdvList.kodPTj))

        Return db.Process(query, param)
    End Function

    Private Function DeleteOrderDtl(orderid As String)
        Dim db As New DBKewConn
        Dim query As String = "DELETE FROM SMKB_Jurnal_Dtl WHERE No_Jurnal=@No_Jurnal AND Status = 1"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@No_Jurnal", orderid))

        Return db.Process(query, param)
    End Function

    Private Function DeleteOrderHdr(orderid As String, norujukan As String, perihal As String, tarikh As String, jenistransaksi As String, jumlahBesar As Decimal)
        Dim db As New DBKewConn
        Dim query As String = "DELETE FROM SMKB_Jurnal_Hdr WHERE No_Jurnal=@No_Jurnal "
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@No_Jurnal", orderid))

        Return db.Process(query, param)
    End Function


    Private Function UpdateStatusDokOrder_Mohon(AdvList As MhnAdvance, statusLulus As String)
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


    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function UploadFile() As String
        Dim postedFile As HttpPostedFile = HttpContext.Current.Request.Files(0)
        Dim fileUpload = HttpContext.Current.Request.Form("fileSurat")
        Dim fileName As String = HttpContext.Current.Request.Form("fileName")

        Try
            ' Convert the base64 string to byte array
            'Dim fileBytes As Byte() = Convert.FromBase64String(fileData)

            ' Specify the file path where you want to save the uploaded file
            Dim savePath As String = Server.MapPath("~/UPLOAD/DOCUMENT/PENDAHULUAN DAN TUNTUTAN/PD/" & fileName)


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