Imports System.ComponentModel
Imports System.Web.Services
Imports System.Web.Services.Protocols

Imports Newtonsoft.Json
Imports System.Web.Script.Services
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System


' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class ROC_WS
    Inherits System.Web.Services.WebService

    Dim sqlcmd As SqlCommand
    Dim sqlcon As SqlConnection
    Dim sqlread As SqlDataReader
    Dim dt As DataTable
    Dim BulanGaji As String
    Dim TahunGaji As String
    Private strConnx As String = "Data Source=devmis12.utem.edu.my;Initial Catalog=dbKewanganV4;Persist Security Info=True;User ID=smkb;Password=Smkb@Dev2012"



    Public Function HelloWorld() As String
        Return "Hello World"
    End Function
    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadBlnGaji()
        Dim db As New DBKewConn
        Dim dbconn As New DBKewConn


        dt = DBConn.Read("SELECT bulan,tahun FROM SMKB_Gaji_Bulan ")
        If dt.Rows.Count > 0 Then
            BulanGaji = dt.Rows(0).Item("bulan").ToString()
            TahunGaji = dt.Rows(0).Item("tahun").ToString()
        End If
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadListROC() As String
        Dim resp As New ResponseRepository


        dt = GetListROC()
        'resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(dt)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadListUbahROC() As String
        Dim resp As New ResponseRepository


        dt = GetListUbahROC()
        'resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(dt)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadRumusROC()
        Dim db As New DBKewConn

        LoadBlnGaji()

        Dim query As String = $"Select sum(z.jumroc) totroc,sum(z.jumgaji) totgaji,sum(z.jumelaun) totelaun from (Select distinct count(*) As jumroc,0 jumgaji,0 As jumelaun From [qa11].dbstaf.dbo.MS15_ROC_1 a where 
                             Year(MS15_TkhDisahkan) = '{TahunGaji}' AND MONTH(MS15_TkhDisahkan) = '{BulanGaji}' and MS15_StaBendahari Is NULL And MS15_KodROC Not In (Select KOD_ROC FROM SMKB_Gaji_ROC) union all
        Select  0 jumroc,COUNT(DISTINCT b.MS15_NoRoc) As jumgaji,0 As jumelaun From [qa11].dbstaf.dbo.MS15_ROC a, [qa11].dbstaf.dbo.roc01_butir b where a.MS15_NoRoc=b.MS15_NoRoc
        And YEAR(MS15_TkhDisahkan) = '{TahunGaji}' AND MONTH(MS15_TkhDisahkan) = '{BulanGaji}' and MS15_StaBendahari Is NULL And MS15_KodROC Not In (Select KOD_ROC FROM SMKB_Gaji_ROC)
        And ROC01_KumpButiran=1 union all Select 0 jumroc,0 jumgaji,COUNT(DISTINCT b.MS15_NoRoc) As jumelaun From [qa11].dbstaf.dbo.MS15_ROC_1 a, [qa11].dbstaf.dbo.roc01_butir_1 b where a.MS15_NoRoc=b.MS15_NoRoc
        And YEAR(MS15_TkhDisahkan) = '{TahunGaji}' AND MONTH(MS15_TkhDisahkan) = '{BulanGaji}' and MS15_StaBendahari Is NULL And MS15_KodROC Not In (Select KOD_ROC FROM SMKB_Gaji_ROC)
        And ROC01_KumpButiran=2 ) z;"

        Dim dt As DataTable = db.fSelectCommandDt(query)

        Return JsonConvert.SerializeObject(dt)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadRumusSahROC(tahun As String, bulan As String)
        Dim db As New DBKewConn


        Dim query As String = $"Select sum(z.jumroc) totroc,sum(z.jumgaji) totgaji,sum(z.jumelaun) totelaun from (Select distinct count(*) As jumroc,0 jumgaji,0 As jumelaun From [qa11].dbstaf.dbo.MS15_ROC_1 a where 
                             Year(MS15_TkhDisahkan) = '{tahun}' AND MONTH(MS15_TkhDisahkan) = '{bulan}' and MS15_StaBendahari Is NOT NULL And MS15_KodROC Not In (Select KOD_ROC FROM SMKB_Gaji_ROC) union all
        Select  0 jumroc,COUNT(DISTINCT b.MS15_NoRoc) As jumgaji,0 As jumelaun From [qa11].dbstaf.dbo.MS15_ROC a, [qa11].dbstaf.dbo.roc01_butir b where a.MS15_NoRoc=b.MS15_NoRoc
        And YEAR(MS15_TkhDisahkan) = '{tahun}' AND MONTH(MS15_TkhDisahkan) = '{bulan}' and MS15_StaBendahari Is NOT NULL And MS15_KodROC Not In (Select KOD_ROC FROM SMKB_Gaji_ROC)
        And  ROC01_KumpButiran=1 union all Select 0 jumroc,0 jumgaji,COUNT(DISTINCT b.MS15_NoRoc) As jumelaun From [qa11].dbstaf.dbo.MS15_ROC_1 a, [qa11].dbstaf.dbo.roc01_butir_1 b where a.MS15_NoRoc=b.MS15_NoRoc
        And YEAR(MS15_TkhDisahkan) = '{tahun}' AND MONTH(MS15_TkhDisahkan) = '{bulan}' and MS15_StaBendahari Is NOT NULL And MS15_KodROC Not In (Select KOD_ROC FROM SMKB_Gaji_ROC)
        And  ROC01_KumpButiran=2 ) z;"

        Dim dt As DataTable = db.fSelectCommandDt(query)

        Return JsonConvert.SerializeObject(dt)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadBulanGaji()
        Dim db As New DBSMConn

        Dim query As String = $"select bulan,tahun from SMKB_Gaji_bulan;"
        Dim dt As DataTable = db.fselectCommandDt(query)

        Return JsonConvert.SerializeObject(dt)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadRekodROC(noroc As String)
        Dim db As New DBSMConn

        Dim query As String = $"Select ms15_NOROC R1NoROC,MS15_NORUJSURAT R1NoRujSurat,MS15_KETERANGAN R1Keterangan,Convert(VARCHAR, ISNULL(MS15_TkhRoc, GETDATE()), 103) R1TkhRoc,NamaROC From MS15_rOC_1,MS_KodROC WHERE ms15_kodroc = kodroc and ms15_NOROC = '{noroc}';"
        Dim dt As DataTable = db.fselectCommandDt(query)

        Return JsonConvert.SerializeObject(dt)
    End Function
    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadDtROC(noroc As String)
        Dim db As New DBSMConn


        Dim query As String = $"Select ROC01_Butiran,Convert(VARCHAR, ISNULL(ROC01_TkhMulaB, GETDATE()), 103) ROC01_TkhMulaB, Convert(VARCHAR, ISNULL(ROC01_TkhTamatB, GETDATE()), 103) ROC01_TkhTamatB,roc01_amaunakandibayar From ROC01_BUTIR_1 Where MS15_NoROC = '{noroc}' order by roc01_amaunakandibayar;"
        Dim dt As DataTable = db.fselectCommandDt(query)

        Return JsonConvert.SerializeObject(dt)
    End Function
    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadBlnThnGaji()
        Dim db As New DBKewConn


        Dim query As String = $"select bulan,tahun,cast(bulan as varchar(2)) + '/' + cast(tahun as varchar(5)) as butir from SMKB_Gaji_bulan;"
        Dim dt As DataTable = db.fSelectCommandDt(query)

        Return JsonConvert.SerializeObject(dt)
    End Function

    Public Class StaffROC
        Public Property NoStaf As String
        Public Property NoROC As String
    End Class

    Private Function GetListROC() As DataTable
        Dim db = New DBKewConn
        Dim thngj As String = ""
        Dim blngj As String = ""

        dt = db.Read("SELECT bulan,tahun FROM SMKB_Gaji_Bulan ")
        If dt.Rows.Count > 0 Then
            blngj = dt.Rows(0).Item("bulan").ToString()
            thngj = dt.Rows(0).Item("tahun").ToString()
        End If

        'Dim query As String = $"select ms01_nostaf, CONVERT(VARCHAR,ISNULL(MS15_TkhDisahkan,GETDATE()),103) MS15_TkhDisahkan, MS15_NoRujSurat, MS15_NoRoc, MS15_Keterangan  from [qa11].dbstaf.dbo.MS15_ROC 
        'Where MS15_StaBendahari Is NULL
        '                    And YEAR(MS15_TkhDisahkan) = '{thngj}' AND MONTH(MS15_TkhDisahkan) = '{blngj}'
        '                    And MS15_KodROC Not In (Select KOD_ROC FROM SMKB_Gaji_ROC) ORDER BY ms01_nostaf"
        'Dim query As String = $"select a.MS15_NoRoc MS15_NoRoc,a.ms01_nostaf ms01_nostaf,a.ms01_nostaf + '|' + b.ms01_nama as ms01_nama, CONVERT(VARCHAR,ISNULL(a.MS15_TkhDisahkan,GETDATE()),103) MS15_TkhDisahkan, a.MS15_NoRujSurat MS15_NoRujSurat, a.MS15_Keterangan  MS15_Keterangan from [qa11].dbstaf.dbo.MS15_ROC_1 a , [qa11].dbstaf.dbo.MS01_Peribadi_1 b
        '                    Where a.ms01_nostaf = b.ms01_nostaf and a.MS15_StaBendahari Is NULL
        '                    And YEAR(a.MS15_TkhDisahkan) = '{thngj}' AND MONTH(a.MS15_TkhDisahkan) = '{blngj}'
        '                    And a.MS15_KodROC Not In (Select KOD_ROC FROM SMKB_Gaji_ROC) ORDER BY a.ms01_nostaf"

        Dim query As String = $"select a.MS15_NoRoc MS15_NoRoc,a.ms01_nostaf + ' | ' + b.ms01_nama nama, CONVERT(VARCHAR,ISNULL(a.MS15_TkhDisahkan,GETDATE()),103) MS15_TkhDisahkan, a.MS15_NoRujSurat MS15_NoRujSurat, a.MS15_Keterangan  MS15_Keterangan from [qa11].dbstaf.dbo.MS15_ROC_1 a , [qa11].dbstaf.dbo.MS01_Peribadi_1 b
                            Where a.ms01_nostaf = b.ms01_nostaf and a.MS15_StaBendahari Is NULL
                            And YEAR(a.MS15_TkhDisahkan) = '{thngj}' AND MONTH(a.MS15_TkhDisahkan) = '{blngj}'
                            And a.MS15_KodROC Not In (Select KOD_ROC FROM SMKB_Gaji_ROC) ORDER BY a.ms01_nostaf"

        Return db.Read(query)
    End Function
    Private Function GetListUbahROC() As DataTable
        Dim db = New DBKewConn
        Dim thngj As String = ""
        Dim blngj As String = ""
        Dim blnsblm As Integer = 0
        Dim thnnsblm As Integer = 0

        dt = db.Read("SELECT bulan,tahun FROM SMKB_Gaji_Bulan ")
        If dt.Rows.Count > 0 Then
            blngj = dt.Rows(0).Item("bulan").ToString()
            thngj = dt.Rows(0).Item("tahun").ToString()
        End If

        'blnsblm = blngj - 1
        If blngj > 1 Then
            blnsblm = blngj - 1
            thnnsblm = thngj


        Else
            blnsblm = 12
            thnnsblm = thngj - 1
        End If
        ' thnnsblm = thngj - 1

        Dim query As String = $"Select a.no_staf no_staf,e.ms01_nama ms01_nama,kod_trans + ' | ' + (select c.Butiran from SMKB_Gaji_Kod_Trans c where c.Kod_Trans=a.Kod_Trans) as kod_trans,
            (select amaun from smkb_gaji_lejar b where a.kod_trans = b.Kod_Trans And b.bulan='{blnsblm}' And b.tahun='{thnnsblm}' And b.No_Staf=a.No_Staf) as jumlama ,amaun as jumbaru,d.MS15_Keterangan MS15_Keterangan,d.MS15_NoRoc MS15_NoRoc
            From smkb_gaji_master a inner Join [qa11].dbstaf.dbo.MS15_ROC_1 d on  a.no_roc = d.MS15_NoRoc 
            inner Join [qa11].dbstaf.dbo.ms01_peribadi_1 e on d.ms01_nostaf=e.ms01_nostaf
            Where MS15_StaBendahari Is Not NULL
            And YEAR(MS15_TkhDisahkan) = '{thngj}' 
            And MONTH(MS15_TkhDisahkan) = '{blngj}' And MS15_KodROC Not In (Select KOD_ROC FROM SMKB_Gaji_ROC)"

        Return db.Read(query)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SimpanROC(data As List(Of StaffROC))
        Dim resp As New ResponseRepository
        resp.Success("Data telah disimpan")
        Dim success As Integer = 0
        Dim counter As Integer = 0
        Dim sqlComm As New SqlCommand
        Dim cmd = New SqlCommand
        Dim dt As New DataTable
        Dim problem As String = ""
        Dim db = New DBKewConn
        Dim thngj As String = ""
        Dim blngj As String = ""

        dt = db.Read("SELECT bulan,tahun FROM SMKB_Gaji_Bulan ")
        If dt.Rows.Count > 0 Then
            blngj = dt.Rows(0).Item("bulan").ToString()
            thngj = dt.Rows(0).Item("tahun").ToString()
        End If

        While counter < data.Count
            Using sqlcon = New SqlConnection(strConnx)
                sqlComm.Connection = sqlcon

                sqlComm.CommandText = "USP_TERIMA_ROC"
                sqlComm.CommandType = CommandType.StoredProcedure
                sqlComm.Parameters.Clear()

                sqlComm.Parameters.AddWithValue("@iBlnTrans", blngj)
                sqlComm.Parameters.AddWithValue("@iThnTrans", thngj)
                sqlComm.Parameters.AddWithValue("@NoStaf", data(counter).NoStaf)
                sqlComm.Parameters.AddWithValue("@NoROC", data(counter).NoROC)
                sqlComm.Parameters.AddWithValue("@UserID", "00664")

                sqlcon.Open()

                'sqlComm.ExecuteNonQuery()
                'Dim rowsAffected As Integer = sqlComm.ExecuteNonQuery()
                Dim X = sqlComm.ExecuteNonQuery()
                If X > 0 Then
                    success = 1
                ElseIf (problem <> "" Or success = 1) And X <= 0 Then
                    problem &= data(counter).NoROC & " | "
                    success = 2
                Else
                    problem &= data(counter).NoROC & " | "
                    success = 0
                End If
            End Using

            counter += 1
        End While

        If success = 1 Then
            resp.Success("Rekod berjaya disimpan")
        ElseIf success = 2 Then
            resp.Success("Rekod berjaya disimpan tapi terdapat rekod tidak dapat diproses. No ROC " & problem)
        Else
            resp.Failed("Rekod tidak berjaya disimpan")
        End If

        Return JsonConvert.SerializeObject(resp.GetResult())
        'Return JsonConvert.SerializeObject(dt)
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadRekodStaf(nostaf As String)
        Dim db As New DBSMConn

        'Dim query As String = $"Select a.MS01_NoStaf,a.MS01_Nama,a.MS01_KpB,b.JawatanS,b.gredgajis,b.PejabatS,b.jumlahgajis,
        '                        case when a.ms01_status =1 then 'AKTIF' else 'TIDAK AKTIF' end status_staf,b.tarafkhidmat From MS01_Peribadi_1 a, VPerjawatan1 b, ms_skim
        '                        WHERE a.MS01_NoStaf = b.nostaf and ms01_nostaf = '{nostaf}' and ms08_staterkini=1;"

        Dim query As String = $"Select a.MS01_NoStaf,a.MS01_Nama,a.MS01_KpB,c.JawGiliran JawatanS,b.MS02_GredGajiS gredgajis,b.MS02_JumlahGajiS jumlahgajis,(Select skim from  MS_Skim where kodskim = b.MS02_Skim ) As skim,
        (select Pejabat from MS_Pejabat where KodPejabat=d.MS08_Pejabat) as PejabatS,
        Case when a.ms01_status =1 then 'AKTIF' else 'TIDAK AKTIF' end status_staf,(select TarafKhidmat from MS_TarafKhidmat where KodTarafKhidmat=b.MS02_Taraf) tarafkhidmat From MS01_Peribadi_1 a, MS02_Perjawatan b, MS_Jawatan c, MS08_Penempatan d
        WHERE a.MS01_NoStaf = b.MS01_NoStaf And a.MS01_NoStaf = d.MS01_NoStaf And a.ms01_nostaf = '{nostaf}' and d.MS08_StaTerkini = 1 and b.MS02_JawSandang = c.KodJawatan;"

        Dim dt As DataTable = db.fselectCommandDt(query)

        Return JsonConvert.SerializeObject(dt)
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SahROC(kodparam As String) As String
        Dim resp As New ResponseRepository
        resp.Success("Data telah disimpan")
        Dim success As Integer = 0
        Dim JumRekod As Integer = 0
        Dim strTkhToday2 As String = Now.ToString("yyyy-MM-dd")
        Dim dtTkhToday2 As DateTime = CDate(strTkhToday2)
        Dim dbconn As New DBKewConn


        fDeleteParam(kodparam)

        If fInsertParam(kodparam) <> "OK" Then
                resp.Failed("Gagal Menyimpan Rekod")
                Return JsonConvert.SerializeObject(resp.GetResult())
                Exit Function
            Else

                success = 1

            End If






        If success = 1 Then

            resp.Success("Rekod berjaya disimpan", "00", kodparam)
            Return JsonConvert.SerializeObject(resp.GetResult())
        Else
            resp.Failed("Rekod tidak berjaya disimpan")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        Return JsonConvert.SerializeObject(resp.GetResult())
#Disable Warning' Function doesn't return a value on all code paths
    End Function

    Private Function fDeleteParam(kodparam As String)
        Dim db As New DBKewConn
        Dim db2 As New DBKewConn
        Dim strTkhToday As String = Now.ToString("yyyy-MM-dd")
        Dim dtTkhToday As DateTime = CDate(strTkhToday)

        Dim strSql = "select count(*) from smkb_gaji_param  where kod_param =  '" & kodparam & "' and status= '01'"
        Dim intCnt As Integer = db.fSelectCount(strSql)
        If intCnt > 0 Then
            Dim query2 As String = "DELETE SMKB_Gaji_Param where kod_param= @kod and status=@staparam"
            Dim param2 As New List(Of SqlParameter)
            param2.Add(New SqlParameter("@kod", kodparam))
            param2.Add(New SqlParameter("@staparam", "01"))
            Return db2.Process(query2, param2)
        End If

    End Function
    Private Function fInsertParam(kodparam As String)
        Dim db As New DBKewConn
        Dim db2 As New DBKewConn
        Dim strTkhToday As String = Now.ToString("yyyy-MM-dd")
        Dim dtTkhToday As DateTime = CDate(strTkhToday)

        Dim query As String = "insert into SMKB_Gaji_Param (kod_param, status, Tarikh ) values (@kod,@staparam,@tarikh)"
        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@kod", kodparam))
        param.Add(New SqlParameter("@staparam", "01"))
        param.Add(New SqlParameter("@tarikh", dtTkhToday))


        Return db.Process(query, param)
    End Function
End Class