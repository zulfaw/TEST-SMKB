Imports System.ComponentModel
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports Newtonsoft.Json
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports SMKB_Web_Portal.Dalam_Negara

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class MohonTuntutan_WS
    Inherits System.Web.Services.WebService

    Dim sqlcmd As SqlCommand
    Dim sqlcon As SqlConnection
    Dim sqlread As SqlDataReader
    Dim dt As DataTable

    <WebMethod()>
    Public Function HelloWorld() As String
        Return "Hello World"
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetUserInfo(nostaf As String)
        Dim db As New DBSMConn
        Dim query As String = $"SELECT  MS01_NoStaf as StafNo, MS01_Nama as Param1, MS08_Pejabat as Param2, JawGiliran as Param3, Kumpulan as Param4, 
                                Singkatan as Param5, MS02_GredGajiS as Param6, 
                                MS02_JumlahGajiS,  MS01_TelPejabat as Param7,  MS02_Kumpulan
                                FROM VK_AdvClm WHERE MS01_NoStaf = '{nostaf}'"
        Dim dt As DataTable = db.fselectCommandDt(query)

        Return JsonConvert.SerializeObject(dt)
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

    Private Function GetRecord_SenaraiPTJ(idPejabat As String, Optional draw As String = “”) As DataTable
        Dim db = New DBSMConn


        Dim query As String = "SELECT  MS01_NoStaf as StafNo, MS01_Nama , MS08_Pejabat, JawGiliran
                    FROM VK_AdvClm WHERE MS08_Pejabat = @idPejabat  "
        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@idPejabat", idPejabat))
        Return db.Read(query, param)
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

    Private Function GetDataKumpWang(kod As String) As DataTable
        Dim db As New DBKewConn
        Dim query As String = "SELECT DISTINCT A.Kod_Kump_Wang as value,B.Butiran as text
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
    Public Function GetKendAwam(ByVal q As String) As String
        'Dim newList As List(Of ItemList)
        'newList = listItem

        'If (q <> "") Then
        '    newList = listItem.Where(
        '        Function(x) x.value.Contains(q) Or x.text.Contains(q)
        '    ).ToList()
        'End If

        Dim tmpDT As DataTable = GetDataKenderaanAwam(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetDataKenderaanAwam(kod As String) As DataTable
        Dim db As New DBKewConn
        Dim query As String = "SELECT Kod_Detail as value, Butiran as text, Keutamaan, Status
                                FROM  SMKB_Lookup_Detail
                                WHERE        (Kod = 'AC11')"
        Dim param As New List(Of SqlParameter)
        If kod <> "" Then
            query &= " AND Status = 1 and (Kod_Detail LIKE '%' + @kod + '%' or Butiran LIKE '%' + @kod2 + '%') "
            'Else
            '    query &= " where A.Status = 1 and A.Kod_Vot =@kod3 order by A.Kod_Kump_Wang"
        End If

        param.Add(New SqlParameter("@kod", kod))
        param.Add(New SqlParameter("@kod2", kod))

        Return db.Read(query, param)
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

    Private Function GetDataOperasi(kod As String) As DataTable
        Dim db As New DBKewConn
        Dim query As String = "SELECT DISTINCT A.Kod_Operasi as value, B.Butiran As text
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
        Dim query As String = "Select distinct Kod_PTJ as value, 
        Kod_PTJ +' - '+(SELECT b.Pejabat FROM [DEVMIS\SQL_INS01].dbStaf.dbo.MS_PEJABAT as b
        WHERE b.STATUS = 1 and b.kodpejabat = left(Kod_PTJ,2)) as text
        from SMKB_COA_Master"

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

    Private Function GetDataProjek(kod As String) As DataTable
        Dim db As New DBKewConn
        Dim query As String = "SELECT DISTINCT A.Kod_Projek as value,B.Butiran as text FROM SMKB_COA_Master A
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

    <System.Web.Services.WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SaveRecordAdv(AdvList As MhnAdvance) As String
        Dim resp As New ResponseRepository
        resp.Success("Data telah disimpan")
        Dim success As Integer = 0
        Dim JumRekod As Integer = 0
        If AdvList Is Nothing Then
            resp.Failed("Tiada simpan")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        resp.Success("Rekod berjaya disimpan", "00", AdvList)
        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetJenisTugasTblSewaHotel(ByVal q As String) As String


        Dim tmpDT As DataTable = GetDataJenisTugasTblHotel(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetDataJenisTugasTblHotel(kod As String) As DataTable
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
    Public Function GetTugasElaunMkn(ByVal q As String) As String


        Dim tmpDT As DataTable = GetDataJenisTugasElaunMkn(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetDataJenisTugasElaunMkn(kod As String) As DataTable
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
    Public Function GetTugasElaunMknLojing(ByVal q As String) As String


        Dim tmpDT As DataTable = GetDataJenisTugasElaunMknLojing(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetDataJenisTugasElaunMknLojing(kod As String) As DataTable
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
    Public Function GetTempatTblHotel(ByVal q As String) As String


        Dim tmpDT As DataTable = GetDataTempatTblHotel(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetDataTempatTblHotel(kod As String) As DataTable
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

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetTempatTblElaunMkn(ByVal q As String) As String


        Dim tmpDT As DataTable = GetDataTempatTblElaunMkn(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetDataTempatTblElaunMkn(kod As String) As DataTable
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

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetTempatTblLojing(ByVal q As String) As String


        Dim tmpDT As DataTable = GetDataTempatTblLojing(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetDataTempatTblLojing(kod As String) As DataTable
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

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetJenisPelbagai(ByVal q As String) As String


        Dim tmpDT As DataTable = GetDataJenisBelanjaPelbagai(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetDataJenisBelanjaPelbagai(kod As String) As DataTable
        Dim db As New DBKewConn
        Dim query As String = "SELECT Kod_Detail as value, Kod_Detail +' - '+ Butiran as text  
        FROM   SMKB_Lookup_Detail WHERE (Kod = 'AC10') AND Kod_Korporat='UTeM' AND status=1"
        Dim param As New List(Of SqlParameter)
        If kod <> "" Then
            query &= " AND Kod_Detail ='01' OR Kod_Detail='02' AND 
                        Butiran like 'Tempat%' and (Kod_Detail LIKE '%' + @kod + '%') "
            param.Add(New SqlParameter("@kod", kod))

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
                WHERE  (b.Kod_Modul = '09') AND (b.Kod_Status_Dok = '02') AND (a.Jenis_Tuntutan ='DN')  " & tarikhQuery & " order by a.Tarikh_Mohon desc"
        ' WHERE a.Status_Dok='01' AND a.Pengesahan_Pemohon='0' " & tarikhQuery & " order by a.Tarikh_Mohon desc"

        param.Add(New SqlParameter("@staffP", staffP))

        Return db.Read(query, param)
    End Function

    <System.Web.Services.WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SaveRecordTuntutan(listClaim As MhnDlmNegara) As String
        Dim resp As New ResponseRepository
        resp.Success("Data telah disimpan")
        Dim success As Integer = 0
        Dim JumRekod As Integer = 0
        If listClaim Is Nothing Then
            resp.Failed("Tiada simpan")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        If listClaim.OrderID = "" Then 'untuk permohonan baru
            listClaim.OrderID = GenerateOrderID()

            If InsertNewOrder(listClaim) <> "OK" Then
                resp.Failed("Gagal Menyimpan order 1266")
                Return JsonConvert.SerializeObject(resp.GetResult())
                ' Exit Function
            End If
        Else 'untuk permohonan sedia ada

            If UpdateNewOrder(listClaim) <> "OK" Then
                'If InsertNewOrder(OtherList) <> "OK" Then
                resp.Failed("Gagal Menyimpan order 1266")
                Return JsonConvert.SerializeObject(resp.GetResult())
                ' Exit Function
                'End If
            End If
        End If


        If UpdateStatusDokOrder_Mohon(listClaim, "Y") <> "OK" Then

            'resp.Failed("Berjaya simpan") 'Gagal Menyimpan order YX
            Return JsonConvert.SerializeObject(resp.GetResult())
            ' Exit Function

        End If


        resp.Success("Rekod berjaya disimpan", "00", listClaim)
        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    Private Function InsertNewOrder(listClaim As MhnDlmNegara)
        Dim db As New DBKewConn
        'Dim year = Date.Now.ToString("yyyy")
        'Dim month = Date.Now.Month
        'Dim blnTuntut = month + "/" + year

        Dim query As String = "INSERT INTO  SMKB_Tuntutan_Hdr (No_Tuntutan, No_Staf, PTj, Tarikh_Mohon, Bulan_Tuntut,Tahun_Tuntut, Jenis_Tuntutan, 
                    No_Pendahuluan, Status_Dok, Status,  Jum_Pendahuluan, Kod_Kump_Wang, Kod_Operasi, Kod_Projek, Kod_PTJ,  ID_Mohon, Pengesahan_Pemohon, Akuan_Pemohon, Sebab_Lewat)
                    VALUES (@No_Tuntutan, @No_Staf, @PTj, @Tarikh_Mohon,  @Bulan_Tuntut, @Tahun_Tuntut, 
                    @Jenis_Tuntutan, @No_Pendahuluan, @Status_Dok, @Status, @Jum_Pendahuluan, @Kod_Kump_Wang, @Kod_Operasi, @Kod_Projek,@Kod_PTJ, @ID_Mohon, @Pengesahan_Pemohon, @Akuan_Pemohon,@Sebab_Lewat)"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@No_Tuntutan", listClaim.OrderID))
        param.Add(New SqlParameter("@No_Staf", Session("ssusrID")))
        param.Add(New SqlParameter("@PTj", listClaim.hidPtjPemohon))
        param.Add(New SqlParameter("@Tarikh_Mohon", listClaim.TkhMohon))
        param.Add(New SqlParameter("@Sebab_Lewat", listClaim.sebabLewat))
        param.Add(New SqlParameter("@Bulan_Tuntut", listClaim.Bulan))
        param.Add(New SqlParameter("@Tahun_Tuntut", listClaim.Tahun))
        param.Add(New SqlParameter("@Jenis_Tuntutan", "DN"))
        param.Add(New SqlParameter("@No_Pendahuluan", listClaim.noPendahuluan))
        param.Add(New SqlParameter("@Status_Dok", "02"))
        param.Add(New SqlParameter("@Status", "1"))
        param.Add(New SqlParameter("@Jum_Pendahuluan", listClaim.jumlahBaucer))
        param.Add(New SqlParameter("@Kod_Kump_Wang", listClaim.KumpWang))
        param.Add(New SqlParameter("@Kod_Operasi", listClaim.KodOperasi))
        param.Add(New SqlParameter("@Kod_Projek", listClaim.KodProjek))
        param.Add(New SqlParameter("@Kod_PTJ", listClaim.KodPtj))
        param.Add(New SqlParameter("@Id_Mohon", Session("ssusrID")))
        param.Add(New SqlParameter("@Pengesahan_Pemohon", listClaim.staPemohon))
        param.Add(New SqlParameter("@Akuan_Pemohon", "1"))



        Return db.Process(query, param)
    End Function

    Private Function UpdateNewOrder(listClaim As MhnDlmNegara)


        Dim db As New DBKewConn
        Dim query As String = "UPDATE SMKB_Pendahuluan_Hdr SET 
                                Tarikh_Mula = @Tarikh_Mula, Tarikh_Tamat = @Tarikh_Tamat, Justifikasi_Prgm = @TunjukSebab, Peruntukan_Prgm = @Peruntukan_Prgm,  
                                Kod_Kump_Wang = @Kod_Kump_Wang , Kod_Operasi = @Kod_Operasi, Kod_Projek = @Kod_Projek, Kod_PTJ = @Kod_PTJ, Kod_Vot= @Kod_Vot,  
                                 Tkh_Adv_Perlu = @TkhAdvPerlu,  CaraBayar = @CaraBayar 
                                WHERE No_Pendahuluan = @No_Pendahuluan"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@No_Pendahuluan", listClaim.OrderID))
        param.Add(New SqlParameter("@PTj", listClaim.KodPtj))
        'param.Add(New SqlParameter("@Tujuan", listClaim.TujuanMohon))
        'param.Add(New SqlParameter("@TunjukSebab", listDetail.TunjukSebab))
        param.Add(New SqlParameter("@Peruntukan_Prgm", listClaim.KodPtj))
        param.Add(New SqlParameter("@Kod_Kump_Wang", listClaim.KumpWang))
        param.Add(New SqlParameter("@Kod_Operasi", listClaim.KodOperasi))
        param.Add(New SqlParameter("@Kod_Projek", listClaim.KodProjek))
        param.Add(New SqlParameter("@Kod_PTJ", listClaim.KodPtj))


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

    Private Function UpdateStatusDokOrder_Mohon(listClaim As MhnDlmNegara, statusLulus As String)
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
        param.Add(New SqlParameter("@No_Rujukan", listClaim.OrderID))
        param.Add(New SqlParameter("@No_Staf", Session("ssusrID")))
        'param.Add(New SqlParameter("@Tkh_Tindakan", orderid))
        'param.Add(New SqlParameter("@Tkh_Transaksi", orderid))
        param.Add(New SqlParameter("@Status_Transaksi", 1))
        param.Add(New SqlParameter("@Status", 1))
        param.Add(New SqlParameter("@Ulasan", "-"))

        Return db.Process(query, param)

    End Function


End Class