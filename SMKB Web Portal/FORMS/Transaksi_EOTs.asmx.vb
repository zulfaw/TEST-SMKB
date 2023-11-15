Imports System.ComponentModel
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports Newtonsoft.Json
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
'Imports System.Web.Http
Imports System.Data.SqlClient
Imports System.IO
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Globalization
Imports System.Web.Configuration

Imports System.Net
Imports System.Net.Mail

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class Transaksi_EOTs
    Inherits System.Web.Services.WebService



    Dim sqlcmd As SqlCommand
    Dim sqlcon As SqlConnection
    Dim sqlread As SqlDataReader
    Dim dt As DataTable
    Private strConn As String = "Data Source=devmis12.utem.edu.my;Initial Catalog=dbKewanganV4;Persist Security Info=True;User ID=smkb;Password=Smkb@Dev2012"

    Public Function HelloWorld() As String
        Return "Hello World"
    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetPegPengesah(ByVal q As String, ByVal chk As Boolean, ByVal ptj As String) As String
        Dim tmpDT As DataTable = GetPengesah(q, chk, ptj)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetPengesah(q As String, chk As String, ptj As String) As DataTable
        Dim db As New DBKewConn("smsm")
        Dim where As String = ""
        Dim where2 As String = ""
        Dim query As String = "SELECT distinct a.MS01_NoStaf,a.MS01_Nama FROM MS01_Peribadi a 
                            inner join MS02_Perjawatan b ON a.MS01_NoStaf = b.MS01_NoStaf  "
        Dim param As New List(Of SqlParameter)
        If q <> "" Then
            where = " AND (a.MS01_NoStaf LIKE '%' + @q + '%' or a.MS01_Nama LIKE '%' + @q2 + '%')  and a.MS01_Status='1'"
            param.Add(New SqlParameter("@q", q))
            param.Add(New SqlParameter("@q2", q))
        End If

        If chk = False Then
            query &= " INNER JOIN MS08_Penempatan c ON a.MS01_NoStaf = c.MS01_NoStaf 
										and c.ms08_Pejabat= @kodPTJ "
            param.Add(New SqlParameter("@kodPTJ", ptj.Substring(0, 2)))
            where2 = " and a.MS01_Status='1' and c.ms08_staterkini='1'  "
        Else
            where2 = " and  a.MS01_Status='1'  "
        End If
        query = query + " WHERE 1 = 1 " + where + where2 + " ORDER BY a.MS01_Nama "
        Return db.ReadDB(query, param)
    End Function


    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetStaf() As DataTable
        Dim db = New DBKewConn

        Dim query As String = "select NoStaf, Nama, singkat As PTJ from [qa11].dbstaf.dbo.vperibadi12"

        Return db.Read(query)
    End Function

    Private Function fGetARNo() As String
        Dim db = New DBKewConn
        Dim lastID As Integer = 1
        Dim newOrderID As String = ""
        Dim param As New List(Of SqlParameter)

        Dim query As String = "SELECT   Kod_Modul, Prefix, No_Akhir, Tahun, Butiran, Kod_PTJ, ID
                               FROM  SMKB_No_Akhir WHERE Kod_Modul = 'EOT' AND Prefix = 'AR'
                               AND Tahun = '{Date.Now.Year}'"

        dt = db.Read(query)
        If dt.Rows.Count > 0 Then
            lastID = CInt(dt.Rows(0).Item("No_Akhir")) + 1
        End If

        newOrderID = lastID
        Return newOrderID
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadRecordStafArahan(idarahan As String) As String
        Dim resp As New ResponseRepository

        dt = GetRecordStafArahan(idarahan)
        'resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(dt)
    End Function
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetRecordStafArahan(idarahan As String) As DataTable
        Dim db As New DBKewConn


        Dim query As String = "select a.No_Arahan,a.No_Staf,b.MS01_Nama from SMKB_EOT_Arahan_Kerja_Dtl a inner join
            [qa11].dbstaf.dbo.ms01_peribadi  b on a.No_Staf = b.ms01_nostaf where a.No_Arahan = @No_Arahan ORDER BY a.No_Staf"
        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@No_Arahan", idarahan))


        Return db.ReadDB(query, param)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadRecordArahanEOT() As String
        Dim resp As New ResponseRepository

        dt = GetRecordArahanEOT()
        'resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(dt)
    End Function
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetRecordArahanEOT() As DataTable
        Dim db As New DBKewConn


        Dim query As String = "SELECT a.No_Arahan, a.No_Surat, a.No_Staf_Peg_AK, a.Kod_PTJ, a.Tkh_Mula, a.Tkh_Tamat, a.Lokasi, a.PeneranganK
            FROM            SMKB_EOT_Arahan_Kerja_Hdr a inner join SMKB_EOT_Arahan_Kerja_Dtl b on a.No_Arahan = b.No_Arahan where a.No_Staf_Peg_AK = '" & Session("ssusrID") & "'
            ORDER BY a.Date_Created"

        Return db.ReadDB(query)
    End Function



    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadRecordArahanInd(category_filter As String, isClicked As Boolean, tkhMula As DateTime, tkhTamat As DateTime) As String
        Dim resp As New ResponseRepository

        dt = GetRecordArahanInd(category_filter, tkhMula, tkhTamat)
        'resp.SuccessPayload(dt)
        'Return JsonConvert.SerializeObject(resp.GetResult())
        Return JsonConvert.SerializeObject(dt)
    End Function
    <System.Web.Services.WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetRecordArahanInd(category_filter As String, tkhMula As DateTime, tkhTamat As DateTime) As DataTable
        Dim db As New DBKewConn
        Dim param As New List(Of SqlParameter)
        Dim id As String = "02636"
        ' @No_staf = Session("ssusrID")
        'id = Session("ssusrID")

        Dim tarikhQuery As String = ""


        If category_filter = "1" Then 'Harini
            'tarikhQuery = " and a.Tkh_Transaksi = getdate() "
            tarikhQuery = " and CAST(a.Tkh_Mula AS DATE) = CAST(getdate() AS DATE) "
        ElseIf category_filter = "2" Then 'Semalam
            'tarikhQuery = " and a.Tkh_Transaksi >= DATEADD(day, -2, getdate()) and a.Tkh_Transaksi <= getdate() "
            tarikhQuery = " and CAST(a.Tkh_Mula AS DATE) = CAST(DATEADD(DAY, -1, GETDATE()) AS DATE) "
        ElseIf category_filter = "3" Then 'seminggu
            'tarikhQuery = " and a.Tkh_Transaksi >= DATEADD(day, -8, getdate()) and a.Tkh_Transaksi <= getdate() "
            tarikhQuery = " and CAST(a.Tkh_Mula AS DATE) >= CAST(DATEADD(DAY, -7, GETDATE()) AS DATE) "
        ElseIf category_filter = "4" Then '30 hari
            tarikhQuery = " and a.Tkh_Mula >= DATEADD(month, -1, getdate()) and a.Tkh_Transaksi <= getdate() "
        ElseIf category_filter = "5" Then '60 hari
            tarikhQuery = " and a.Tkh_Mula >= DATEADD(month, -2, getdate()) and a.Tkh_Transaksi <= getdate() "
        ElseIf category_filter = "6" Then 'custom
            tarikhQuery = " and  a.Tkh_Mula >= @tkhMula and a.Tkh_Tamat <= @TkhTamat "
            param = New List(Of SqlParameter)
            param.Add(New SqlParameter("@tkhMula", tkhMula))
            param.Add(New SqlParameter("@TkhTamat", tkhTamat))
        Else
            tarikhQuery = "ORDER BY a.No_Arahan"

        End If

        Dim query As String = "select a.No_Arahan,a.No_Surat,a.No_Staf_Peg_AK,b.ms01_nama, e.ms01_nama as Nama_Staf_Sah, 
                    d.No_Staf_SahB,d.No_Staf_SahB +' - '+ e.ms01_nama as Nama_Sah,g.ms01_nostaf as No_Staf_LulusB,
                    g.ms01_nama as Nama_Staf_LulusB,g.ms01_nostaf + ' - ' + g.ms01_nama as Staf_lulusB,
                    a.Kod_PTJ,c.pejabat,a.KW,a.Kod_Vot, FORMAT (a.Tkh_Mula, 'yyyy-MM-dd') as Tkh_Mula,FORMAT (a.Tkh_Tamat, 'yyyy-MM-dd')  AS Tkh_Tamat,
                    a.Lokasi,a.PeneranganK, isnull(f.File_Name,'-') as File_name, isnull(f.Folder,'-') as Folder,h.No_Staf 
                    from SMKB_EOT_Arahan_Kerja_Hdr a inner join SMKB_EOT_Arahan_Kerja_Dtl h
					on a.No_Arahan= h.No_Arahan 					
                    inner join [qa11].dbstaf.dbo.ms01_peribadi b on a.No_Staf_Peg_AK = b.ms01_Nostaf
                    inner join [qa11].dbstaf.dbo.ms_pejabat c on a.Kod_PTJ = c.kodPejabat
                    inner join SMKB_EOT_Pegawai d on a.No_Arahan = d.No_Mohon inner join
                    [qa11].dbstaf.dbo.ms01_peribadi e on d.No_Staf_SahB = e.ms01_nostaf 
                    inner join [qa11].dbstaf.dbo.ms01_peribadi g on  d.No_Staf_LulusB = g.ms01_nostaf
                    left outer join SMKB_EOT_Dok_ArahanK f on  f.No_Mohon = a.No_Arahan
					 where h.No_Staf = @No_staf " & tarikhQuery & " "

        param.Add(New SqlParameter("@No_staf", id))
        Return db.Read(query, param)

    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadRecordArahan(category_filter As String, isClicked As Boolean, tkhMula As DateTime, tkhTamat As DateTime) As String
        Dim resp As New ResponseRepository

        If isClicked = False Then
            Return JsonConvert.SerializeObject(New DataTable)
        End If

        dt = GetRecordArahan(category_filter, tkhMula, tkhTamat)
        'resp.SuccessPayload(dt)
        'Return JsonConvert.SerializeObject(resp.GetResult())


        'resp.SuccessPayload(dt)
        For Each x As DataRow In dt.Rows
            If Not IsDBNull(x.Item("File_Name")) Then
                Dim url As String = GetBaseUrl() + Trim(x.Item("Folder")) + "/" + x.Item("File_Name")
                x.Item("url") = url

            End If


        Next
        Return JsonConvert.SerializeObject(dt)


    End Function
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetRecordArahan(category_filter As String, tkhMula As DateTime, tkhTamat As DateTime) As DataTable
        Dim db As New DBKewConn
        Dim tarikhQuery As String = ""
        Dim param As List(Of SqlParameter)

        If category_filter = "1" Then 'Harini
            'tarikhQuery = " and a.Tkh_Transaksi = getdate() "
            tarikhQuery = " where CAST(a.Tkh_Mula AS DATE) = CAST(getdate() AS DATE) "
        ElseIf category_filter = "2" Then 'Semalam
            'tarikhQuery = " and a.Tkh_Transaksi >= DATEADD(day, -2, getdate()) and a.Tkh_Transaksi <= getdate() "
            tarikhQuery = " where CAST(a.Tkh_Mula AS DATE) = CAST(DATEADD(DAY, -1, GETDATE()) AS DATE) "
        ElseIf category_filter = "3" Then 'seminggu
            'tarikhQuery = " and a.Tkh_Transaksi >= DATEADD(day, -8, getdate()) and a.Tkh_Transaksi <= getdate() "
            tarikhQuery = " where CAST(a.Tkh_Mula AS DATE) >= CAST(DATEADD(DAY, -7, GETDATE()) AS DATE) "
        ElseIf category_filter = "4" Then '30 hari
            tarikhQuery = " where a.Tkh_Mula >= DATEADD(month, -1, getdate()) and a.Tkh_Transaksi <= getdate() "
        ElseIf category_filter = "5" Then '60 hari
            tarikhQuery = " where a.Tkh_Mula >= DATEADD(month, -2, getdate()) and a.Tkh_Transaksi <= getdate() "
        ElseIf category_filter = "6" Then 'custom
            tarikhQuery = " where  a.Tkh_Mula >= @tkhMula and a.Tkh_Tamat <= @TkhTamat "
            param = New List(Of SqlParameter)
            param.Add(New SqlParameter("@tkhMula", tkhMula))
            param.Add(New SqlParameter("@TkhTamat", tkhTamat))
        Else
            tarikhQuery = "ORDER BY a.No_Arahan"

        End If

        Dim query As String = "select a.No_Arahan,a.No_Surat,a.No_Staf_Peg_AK,b.ms01_nama, e.ms01_nama as Nama_Staf_Sah, 
                    d.No_Staf_SahB,g.ms01_nostaf as No_Staf_LulusB,g.ms01_nama as Nama_Staf_LulusB,
                    g.ms01_nostaf + ' - ' + g.ms01_nama as Staf_lulusB,
                    a.Kod_PTJ,c.pejabat,a.KW,a.Kod_Vot, FORMAT (a.Tkh_Mula, 'yyyy-MM-dd') as Tkh_Mula,FORMAT (a.Tkh_Tamat, 'yyyy-MM-dd')  AS Tkh_Tamat,
                    a.Lokasi,a.PeneranganK, isnull(f.File_Name,'-') as File_name, isnull(f.Folder,'-') as Folder,isnull(f.File_Name,'-') as url 
                    from SMKB_EOT_Arahan_Kerja_Hdr a 
                    inner join [qa11].dbstaf.dbo.ms01_peribadi b on a.No_Staf_Peg_AK = b.ms01_Nostaf
                    inner join [qa11].dbstaf.dbo.ms_pejabat c on a.Kod_PTJ = c.kodPejabat
                    inner join SMKB_EOT_Pegawai d on a.No_Arahan = d.No_Mohon inner join
                    [qa11].dbstaf.dbo.ms01_peribadi e on d.No_Staf_SahB = e.ms01_nostaf 
                    inner join [qa11].dbstaf.dbo.ms01_peribadi g on  d.No_Staf_LulusB = g.ms01_nostaf
                    left outer join SMKB_EOT_Dok_ArahanK f on  f.No_Mohon = a.No_Arahan   " & tarikhQuery & ""


        Return db.ReadDB(query)
    End Function
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadRecordByNoArahan(id As String) As String
        Dim resp As New ResponseRepository

        dt = GetRecordByNoArahan(id)
        'resp.SuccessPayload(dt)
        'Return JsonConvert.SerializeObject(resp.GetResult())
        Return JsonConvert.SerializeObject(dt)
    End Function


    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetRecordByNoArahan(id As String) As DataTable
        Dim db As New DBKewConn
        Dim param As New List(Of SqlParameter)

        Dim query As String = "select a.No_Arahan,a.No_Surat,a.No_Staf_Peg_AK,b.ms01_nama, e.ms01_nama as Nama_Staf_Sah, 
                    d.No_Staf_SahB,g.ms01_nostaf as No_Staf_LulusB,g.ms01_nama as Nama_Staf_LulusB,
                    d.No_Staf_SahB + ' - ' + e.ms01_nama as Nama_Sah,
                    g.ms01_nostaf + ' - ' + g.ms01_nama as Staf_lulusB,
                    a.Kod_PTJ,c.pejabat,a.KW,a.Kod_Vot, FORMAT (a.Tkh_Mula, 'yyyy-MM-dd') as Tkh_Mula,FORMAT (a.Tkh_Tamat, 'yyyy-MM-dd')  AS Tkh_Tamat,
                    a.Lokasi,a.PeneranganK, isnull(f.File_Name,'-') as File_name, isnull(f.Folder,'-') as Folder 
                    from SMKB_EOT_Arahan_Kerja_Hdr a 
                    inner join [qa11].dbstaf.dbo.ms01_peribadi b on a.No_Staf_Peg_AK = b.ms01_Nostaf
                    inner join [qa11].dbstaf.dbo.ms_pejabat c on a.Kod_PTJ = c.kodPejabat
                    inner join SMKB_EOT_Pegawai d on a.No_Arahan = d.No_Mohon inner join
                    [qa11].dbstaf.dbo.ms01_peribadi e on d.No_Staf_SahB = e.ms01_nostaf 
                    inner join [qa11].dbstaf.dbo.ms01_peribadi g on  d.No_Staf_LulusB = g.ms01_nostaf
                    left outer join SMKB_EOT_Dok_ArahanK f on  f.No_Mohon = a.No_Arahan
                    where a.No_Arahan = @NoArahan"


        param.Add(New SqlParameter("@NoArahan", id))

        Return db.Read(query, param)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadRecordEOTbyNoMohon(id As String) As String
        Dim resp As New ResponseRepository

        dt = GetRecordEOTbyNoMohon(id)
        'resp.SuccessPayload(dt)
        'Return JsonConvert.SerializeObject(resp.GetResult())
        Return JsonConvert.SerializeObject(dt)
    End Function


    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetRecordEOTbyNoMohon(id As String) As DataTable
        Dim db As New DBKewConn
        Dim param As New List(Of SqlParameter)

        Dim query As String = "SELECT No_Mohon, No_Turutan, FORMAT (Tkh_Tuntut, 'yyyy-MM-dd') as Tkh_Tuntut, Jam_Mula, Jam_Tamat, Jum_Jam_Tuntut, Kadar_Tuntut, Amaun_Tuntut
                               FROM  SMKB_EOT_Mohon_Dtl where No_Mohon = @NoMohon"


        param.Add(New SqlParameter("@NoMohon", id))

        Return db.Read(query, param)
    End Function


    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadRecordStaf() As String
        Dim resp As New ResponseRepository

        dt = GetRecordStaf()
        'resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(dt)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetRecordStaf() As DataTable
        Dim db As New DBKewConn("smsm")


        Dim query As String = "select  A.MS01_NOSTAF, A.MS01_NAMA,D.Singkatan FROM  MS01_Peribadi A INNER JOIN MS02_Perjawatan B ON A.MS01_NoStaf = B.MS01_NoStaf INNER JOIN
        MS08_Penempatan C ON B.MS01_NoStaf = C.MS01_NoStaf and c.MS08_StaTerkini = 1 INNER JOIN MS_Pejabat D 
        ON C.MS08_Pejabat = D.KodPejabat  AND A.MS01_Status =1 where b.MS02_KumpStaf = 0 and b.MS02_Kumpulan  in ('10','4','5')"

        Return db.ReadDB(query)
    End Function


    <System.Web.Services.WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function KiraAmtEOT(MohonEOT As MohonEOT) As String
        Dim resp As New ResponseRepository
        Dim db As New DBKewConn
        Dim amaun As Decimal = 0.00

        'Dim dtTkh As Date = DateTime.ParseExact(MohonEOT.Tkh_Tuntut, "dd/MM/yyyy", CultureInfo.CurrentCulture)
        'Dim strTkh As String = dtTkh.ToString("yyyy-MM-dd")
        Try
            Dim param1 As SqlParameter = New SqlParameter("@nostaf", SqlDbType.VarChar)
            param1.Value = Session("ssusrID")
            param1.Direction = ParameterDirection.Input
            param1.IsNullable = False

            Dim param2 As SqlParameter = New SqlParameter("@tarikh", SqlDbType.DateTime)
            param2.Value = MohonEOT.Tkh_Tuntut
            param2.Direction = ParameterDirection.Input
            param2.IsNullable = False

            Dim param3 As SqlParameter = New SqlParameter("@jammula", SqlDbType.VarChar)
            param3.Value = MohonEOT.Jam_Mula
            param3.Direction = ParameterDirection.Input
            param3.IsNullable = False

            Dim param4 As SqlParameter = New SqlParameter("@jamtamat", SqlDbType.VarChar)
            param4.Value = MohonEOT.Jam_Tamat
            param4.Direction = ParameterDirection.Input
            param4.IsNullable = False

            Dim param5 As SqlParameter = New SqlParameter("@amaun", SqlDbType.Decimal)
            param5.Value = amaun
            param5.Precision = 19
            param5.Scale = 2
            param5.Direction = ParameterDirection.Output
            param5.IsNullable = False
            Dim paramSql() As SqlParameter = {param1, param2, param3, param4, param5}

            Dim jumamaun = db.fExecuteSP("USP_KIRAAMTEOT", paramSql, param5, amaun)

        Catch ex As Exception
            fErrorLog("KiraAmtEOT - " & ex.Message.ToString)
        End Try

        'Return amaun
        MohonEOT.Amaun = amaun
        resp.Success("Amaun berjaya dikira", "00", MohonEOT.Amaun)
        Return JsonConvert.SerializeObject(resp.GetResult())


    End Function


    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadSenarai(ByVal NoAk As String) As String
        Dim resp As New ResponseRepository

        dt = GetSenarai(NoAk)
        resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetSenarai(NoAk As String) As DataTable
        Dim db As New DBKewConn
        Dim param As New List(Of SqlParameter)

        Dim query As String = "SELECT   A.No_Arahan, A.No_Staf,B.MS01_NAMA FROM SMKB_EOT_Arahan_Kerja_Dtl A inner join [qa11].dbstaf.dbo.MS01_Peribadi B 
        ON A.No_Staf = B.MS01_NOSTAF WHERE A.No_Arahan = @NoAk"
        param.Add(New SqlParameter("@NoAk", NoAk))

        Return db.Read(query, param)
    End Function
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadSemakanPermohonan() As String
        Dim resp As New ResponseRepository

        dt = GetSemakanPermohonan()
        'resp.SuccessPayload(dt)
        Return JsonConvert.SerializeObject(dt)
        'Return JsonConvert.SerializeObject(resp.GetResult())
    End Function
    Private Function GetSemakanPermohonan() As DataTable
        Dim db As New DBKewConn
        Dim param As New List(Of SqlParameter)
        Dim query As String = "select a.No_Mohon,a.No_Arahan,a.No_Staf,b.MS01_nama,FORMAT (a.Tkh_Mohon, 'yyyy-MM-dd') as Tkh_Mohon,c.Butiran from [dbo].[SMKB_EOT_Mohon_Hdr] a
			inner join [qa11].dbstaf.dbo.MS01_Peribadi b on a.No_Staf = b.ms01_nostaf
			inner join SMKB_EOT_Status_Dok c on a.Status_Mohon = c.KodStatus order by c.Butiran"

        Return db.Read(query, param)
    End Function
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadSenEOTPengesahanPenyelia(ByVal NoPegSah As String) As String
        Dim resp As New ResponseRepository

        dt = GetSenEOTPengesahanPenyelia(NoPegSah)
        'resp.SuccessPayload(dt)
        Return JsonConvert.SerializeObject(dt)
        'Return JsonConvert.SerializeObject(resp.GetResult())
    End Function
    Private Function GetSenEOTPengesahanPenyelia(NoPegSah As String) As DataTable
        Dim db As New DBKewConn
        Dim param As New List(Of SqlParameter)
        Dim query As String = "SELECT Distinct b.No_Mohon, a.No_Staf, c.ms01_nama as Nama,FORMAT (a.Tkh_Mohon, 'yyyy-MM-dd') as Tkh_Mohon, 
            sum(convert(int,substring(b.jum_jam_sah,1,2)) * 60 + convert(int,substring(b.jum_jam_sah,3,2))) as Jam, 
            sum(b.Amaun_Sah) as AmaunTuntut 
            FROM SMKB_EOT_Mohon_Hdr a INNER JOIN 
            SMKB_EOT_Mohon_Dtl b ON a.No_Mohon = b.No_Mohon
			inner join [qa11].dbstaf.dbo.ms01_peribadi c on c.ms01_nostaf = a.no_staf
            WHERE  (b.Status_Sah ='BS') AND (b.Status_Lulus ='BL')
			and (a.Status_Mohon='07' or a.Status_Mohon='02' or a.Status_Mohon='03') AND (a.Status_Cetak='1')
			and b.No_Staf_Sah = @NoPegSah
            GROUP BY b.No_Mohon, a.No_Staf,  c.ms01_nama,a.Tkh_Mohon"

        param.Add(New SqlParameter("@NoPegSah", NoPegSah))


        Return db.Read(query, param)
    End Function
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadSenBatalEOT(ByVal NoStaf As String) As String
        Dim resp As New ResponseRepository

        dt = GetSenSenBatalEOT(NoStaf)
        'resp.SuccessPayload(dt)
        Return JsonConvert.SerializeObject(dt)
        'Return JsonConvert.SerializeObject(resp.GetResult())
    End Function
    Private Function GetSenSenBatalEOT(NoStaf As String) As DataTable
        Dim db As New DBKewConn
        Dim param As New List(Of SqlParameter)
        Dim query As String = "SELECT Distinct b.No_Mohon, a.No_Staf, c.ms01_nama as Nama,FORMAT (a.Tkh_Mohon, 'yyyy-MM-dd') as Tkh_Mohon, 
            sum(convert(int,substring(b.jum_jam_sah,1,2)) * 60 + convert(int,substring(b.jum_jam_sah,3,2))) as Jam, 
            sum(b.Amaun_Sah) as AmaunTuntut 
            FROM SMKB_EOT_Mohon_Hdr a INNER JOIN 
            SMKB_EOT_Mohon_Dtl b ON a.No_Mohon = b.No_Mohon
			inner join [qa11].dbstaf.dbo.ms01_peribadi c on c.ms01_nostaf = a.no_staf
            WHERE  (b.Status_Sah ='BS') AND (b.Status_Lulus ='BL')
			and (a.Status_Mohon='01')
			and b.No_Staf_Sah = @NoStaf
            GROUP BY b.No_Mohon, a.No_Staf,  c.ms01_nama,a.Tkh_Mohon"

        param.Add(New SqlParameter("@NoStaf", NoStaf))


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
    Public Function getKJ(ptj As String) As String
        Dim db1 As New DBKewConn("smsm")

        dt = GetRecordKJ(ptj)
        'resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(dt)

    End Function
    <System.Web.Services.WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SimpanEOT(MohonEOT As MohonEOT) As String
        Dim resp As New ResponseRepository
        resp.Success("Data telah disimpan")
        Dim success As Integer = 0
        Dim X As Integer = 0
        Dim JumRekod As Integer = 0
        Dim strTkhToday2 As String = Now.ToString("yyyy-MM-dd")
        Dim dtTkhToday2 As DateTime = CDate(strTkhToday2)
        Dim sqlComm As New SqlCommand

        If MohonEOT Is Nothing Then
            resp.Failed("Rekod Tidak disimpan")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If
        If MohonEOT.Tkh_Tuntut > MohonEOT.Tkh_Tamat Then
            resp.Failed("Rekod Tidak disimpan.Sila semak Tarikh Tuntut")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If
        If MohonEOT.Tkh_Tuntut < MohonEOT.Tkh_Mula Then
            resp.Failed("Rekod Tidak disimpan.Sila semak Tarikh Tuntut")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If
        If MohonEOT.No_Mohon = "" Then
            MohonEOT.No_Mohon = UpdateNoAkhirEOT()
            If InsertEOTHdr(MohonEOT.No_Arahan, MohonEOT.No_Mohon, MohonEOT.No_Staf, MohonEOT.Tkh_Tuntut, MohonEOT.OT_Ptj) <> "OK" Then
                resp.Failed("Gagal Menyimpan Tuntutan Kerla Lebih Masa (Header")
                Return JsonConvert.SerializeObject(resp.GetResult())
                Exit Function
            Else

                sqlcon = New SqlConnection(strConn)
                Using (sqlcon)

                    sqlComm.Connection = sqlcon

                    sqlComm.CommandText = "USP_SIMPANAMTEOT"
                    sqlComm.CommandType = CommandType.StoredProcedure

                    sqlComm.Parameters.AddWithValue("nomohon", MohonEOT.No_Mohon)
                    sqlComm.Parameters.AddWithValue("nostaf", Session("ssusrID"))
                    sqlComm.Parameters.AddWithValue("tarikh", MohonEOT.Tkh_Tuntut)
                    sqlComm.Parameters.AddWithValue("jam_mula", MohonEOT.Jam_Mula)
                    sqlComm.Parameters.AddWithValue("jam_tamat", MohonEOT.Jam_Tamat)
                    sqlComm.Parameters.AddWithValue("l_success", 0)

                    sqlcon.Open()
                    X = sqlComm.ExecuteNonQuery()
                    If X > 0 Then

                        If UpdateEOTDtl(MohonEOT.Tujuan, MohonEOT.Catatan, MohonEOT.No_Staf_Sah, MohonEOT.OT_Ptj, MohonEOT.No_Staf_Lulus, MohonEOT.No_Mohon) <> "OK" Then
                            resp.Failed("Gagal Menyimpan Tuntutan Kerla Lebih Masa (Detail")
                            Return JsonConvert.SerializeObject(resp.GetResult())
                            Exit Function
                        Else
                            success = 1
                        End If
                    Else
                        resp.Failed("Gagal Menyimpan Tuntutan Kerla Lebih Masa (Detail SP")
                        Return JsonConvert.SerializeObject(resp.GetResult())
                        Exit Function
                    End If

                End Using

            End If
        End If
        If success = 1 Then

            resp.Success("Rekod berjaya disimpan", "00", MohonEOT)
            Return JsonConvert.SerializeObject(resp.GetResult())
        Else
            resp.Failed("Rekod tidak berjaya disimpan")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function


    <System.Web.Services.WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SimpanAK(ArahanK As ArahanK) As String
        'Public Function SimpanAK(No_Arahan As String, No_Surat As String, No_Staf_Peg_AK As String, Kod_PTJ As String, KW As String, Kod_Vot As String, Tkh_Mula As String Tkh_Tamat As String, Lokasi As String, PeneranganK As String,  Jen_Dok as string, File_Name as string) As String
        Dim resp As New ResponseRepository
        resp.Success("Data telah disimpan")
        Dim success As Integer = 0
        Dim JumRekod As Integer = 0
        Dim strTkhToday2 As String = Now.ToString("yyyy-MM-dd")
        Dim dtTkhToday2 As DateTime = CDate(strTkhToday2)


        If ArahanK Is Nothing Then
            resp.Failed("Tidak disimpan")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        If ArahanK.Tkh_Mula = "" Then
            resp.Failed("Tarikh mula perlu diisi")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        If ArahanK.No_Arahan = "" Then
            ArahanK.No_Arahan = GenerateEOTID()
            ArahanK.KW = "07"
            ArahanK.Kod_Vot = "14101"
            ArahanK.Kod_PTJ = Session("ssusrKodPTj").Substring(0, 2)
            ArahanK.No_Staf_Peg_AK = Session("ssusrID")
            ArahanK.No_Mohon = "-"
            ArahanK.Folder = "UPLOAD/DOCUMENT/EOT/AR/"
            ArahanK.Tkh_Upload = dtTkhToday2
            If InsertNewEOT(ArahanK.No_Arahan, ArahanK.No_Surat, ArahanK.No_Staf_Peg_AK, ArahanK.Kod_PTJ, ArahanK.KW, ArahanK.Kod_Vot, ArahanK.Tkh_Mula, ArahanK.Tkh_Tamat, ArahanK.Lokasi, ArahanK.PeneranganK) <> "OK" Then
                resp.Failed("Gagal Menyimpan Arahan kerja")
                Return JsonConvert.SerializeObject(resp.GetResult())
                Exit Function
            Else

                If InsertPegawai(ArahanK.No_Arahan, ArahanK.No_Staf_Sah, ArahanK.Ketua_PTJ) <> "OK" Then
                    resp.Failed("Gagal Menyimpan No Pengesah dan Pelulus")
                    Return JsonConvert.SerializeObject(resp.GetResult())
                    Exit Function
                Else
                    If InsertLampiran(ArahanK.No_Arahan, ArahanK.Jen_Dok, ArahanK.Folder, ArahanK.File_Name, ArahanK.Tkh_Upload) <> "OK" Then
                        resp.Failed("Gagal Menyimpan Lampiran")
                        Return JsonConvert.SerializeObject(resp.GetResult())
                        Exit Function
                    Else
                        success = 1

                    End If
                End If
                ' dt = GetOrder(ArahanK.No_Arahan)
                'If dt.Rows.Count = 0 Then
                'resp.Failed("Nombor Arahan tidak ditemui")
                'Return False
                'End If
            End If
        End If

        If success = 1 Then
            Session("NoArahan") = ArahanK.No_Arahan
            resp.Success("Rekod berjaya disimpan", "00", ArahanK)
            Return JsonConvert.SerializeObject(resp.GetResult())
        Else
            resp.Failed("Rekod tidak berjaya disimpan")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        Return JsonConvert.SerializeObject(resp.GetResult())
#Disable Warning' Function doesn't return a value on all code paths
    End Function


    <System.Web.Services.WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function KemaskiniAK(SemakArahan As SemakArahan) As String

        Dim resp As New ResponseRepository
        resp.Success("Data telah disimpan")
        Dim success As Integer = 0
        Dim JumRekod As Integer = 0
        Dim strTkhToday2 As String = Now.ToString("yyyy-MM-dd")
        Dim dtTkhToday2 As DateTime = CDate(strTkhToday2)


        If SemakArahan Is Nothing Then
            resp.Failed("Rekod Tidak dikemaskini")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        If SemakArahan.Tkh_Mula = "" Then
            resp.Failed("Tarikh mula perlu diisi")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        If SemakArahan.No_Arahan <> "" Then
            ' ArahanK.No_Arahan = GenerateEOTID()
            'ArahanK.KW = "07"
            ' ArahanK.Kod_Vot = "14101"
            'ArahanK.Kod_PTJ = Session("ssusrKodPTj").Substring(0, 2)
            'ArahanK.No_Staf_Peg_AK = Session("ssusrID")
            ' ArahanK.No_Mohon = "-"
            SemakArahan.Folder = "UPLOAD/DOCUMENT/EOT/AR/"
            SemakArahan.Tkh_Upload = dtTkhToday2
            If UpdateNewEOT(SemakArahan.No_Arahan, SemakArahan.No_Surat, SemakArahan.Tkh_Mula, SemakArahan.Tkh_Tamat, SemakArahan.Lokasi, SemakArahan.PeneranganK) <> "OK" Then
                resp.Failed("Gagal Menyimpan Arahan kerja")
                Return JsonConvert.SerializeObject(resp.GetResult())
                Exit Function
            Else

                If UpdatePegawai(SemakArahan.No_Arahan, SemakArahan.No_Staf_Sah) <> "OK" Then
                    resp.Failed("Gagal Menyimpan No Pengesah dan Pelulus")
                    Return JsonConvert.SerializeObject(resp.GetResult())
                    Exit Function
                Else
                    If SemakLampiran(SemakArahan.No_Arahan) > 0 Then
                        If UpdateLampiran(SemakArahan.No_Arahan, SemakArahan.File_Name, SemakArahan.Tkh_Upload) <> "OK" Then
                            resp.Failed("Gagal Menyimpan Lampiran")
                            Return JsonConvert.SerializeObject(resp.GetResult())
                            Exit Function
                        Else
                            success = 1

                        End If
                    Else
                        If InsertLampiran(SemakArahan.No_Arahan, SemakArahan.Jen_Dok, SemakArahan.Folder, SemakArahan.File_Name, SemakArahan.Tkh_Upload) <> "OK" Then
                            resp.Failed("Gagal Menyimpan Lampiran")
                            Return JsonConvert.SerializeObject(resp.GetResult())
                            Exit Function
                        Else
                            success = 1

                        End If
                    End If

                End If
                    ' dt = GetOrder(ArahanK.No_Arahan)
                    'If dt.Rows.Count = 0 Then
                    'resp.Failed("Nombor Arahan tidak ditemui")
                    'Return False
                    'End If
                End If
        End If

        If success = 1 Then
            Session("NoArahan") = SemakArahan.No_Arahan
            resp.Success("Rekod berjaya dikemaskini", "00", SemakArahan)

            Return JsonConvert.SerializeObject(resp.GetResult())
        Else
            resp.Failed("Rekod tidak berjaya dikemaskini")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        Return JsonConvert.SerializeObject(resp.GetResult())
#Disable Warning' Function doesn't return a value on all code paths
    End Function


    Function GetRecordKJ(ptj As String)

        Dim db1 As New DBKewConn("smsm")


        Dim param As New List(Of SqlParameter)
        Dim query1 As String = "SELECT TOP 1  a.MS01_NoStaf,a.MS01_Nama ,(a.MS01_Nostaf + ' - ' + a.ms01_nama) as NamaStaf, MS08_Pejabat as KodPejabat FROM MS01_Peribadi a inner join ms08_penempatan b on a.ms01_nostaf=b.ms01_nostaf 
											 inner join MS02_Perjawatan c on a.ms01_nostaf=c.ms01_nostaf where a.MS01_Status='1' and b.ms08_staterkini='1' 
											 And b.MS08_Pejabat =@Ptj and ms02_kumpulan in ('1','2','3','6','7','8','9') "
        param.Add(New SqlParameter("@Ptj", ptj.Substring(0, 2)))

        Return db1.ReadDB(query1, param)
    End Function


    Private Function UpdateNoAkhirEOT()
        Dim year = Date.Now.Year
        Dim RunningNoAR As String = ""
        Dim day = Date.Now.Day.ToString()
        Dim month = Date.Now.Month.ToString()
        Dim year1 = Date.Now.Year.ToString.Substring(2, 2)
        'Dim KodPtj = Session("ssusrKodPTj").ToString.PadRight(6, "0")

        Dim strSql = "SELECT Kod_Modul, Prefix, No_Akhir, Tahun, Butiran, kod_PTJ From SMKB_No_Akhir WHERE Kod_Modul = '23' AND Prefix = 'OT' AND Tahun = @years"
        Dim paramSql() As SqlParameter = {
            New SqlParameter("@KodModull", "23"),
            New SqlParameter("@Prefixx", "OT"),
            New SqlParameter("@years", Date.Now.Year)
            }

        Dim dbconn As New DBKewConn
        Dim ds = dbconn.fSelectCommand(strSql, "MKNoAkhir", paramSql)

        If ds.Tables(0).Rows.Count > 0 Then
            ds.Tables("MKNoAkhir").Rows(0)("No_Akhir") = ds.Tables("MKNoAkhir").Rows(0)("No_Akhir") + 1
            RunningNoAR = ds.Tables("MKNoAkhir").Rows(0)("No_Akhir")
        Else
            Dim dr As DataRow
            dr = ds.Tables("MKNoAkhir").NewRow
            dr("Kod_Modul") = "23"
            dr("Prefix") = "OT"
            dr("no_akhir") = 1
            dr("Tahun") = year
            dr("Butiran") = "No OT"

            ds.Tables("MKNoAkhir").Rows.Add(dr)
            RunningNoAR = 1
        End If

        dbconn.sUpdateCommand(ds, strSql)
        RunningNoAR = RunningNoAR.ToString.PadLeft(6, "0")

        Dim NoPermohonanAR As String = "OT" + RunningNoAR + month + year1
        Return NoPermohonanAR

    End Function
    Private Function GenerateEOTID() As String
        Dim db As New DBKewConn

        Dim year = Date.Now.ToString("yyyy")
        Dim month = Date.Now.Month

        Dim lastID As Integer = 1
        Dim newEOTID As String = ""

        Dim query As String = $"select TOP 1 No_Akhir as id from SMKB_No_Akhir where Kod_Modul ='23' AND Prefix ='AR' AND Tahun =@year"
        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@year", year))

        dt = db.Read(query, param)

        If dt.Rows.Count > 0 Then
            lastID = CInt(dt.Rows(0).Item("id")) + 1

            UpdateNoAkhir("23", "AR", year, lastID)
        Else

            InsertNoAkhir("23", "AR", year, lastID)
        End If
        newEOTID = "AR" + Format(lastID, "000000").ToString + month.ToString("00") + Right(year.ToString(), 2)

        Return newEOTID
    End Function

    'Public Function GetWebMethodValue() As String
    '    Dim newEOTID As String = GenerateEOTID()
    '    Return newEOTID
    'End Function
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
        param.Add(New SqlParameter("@Butiran", "Arahan_Kerja"))
        param.Add(New SqlParameter("@Kod_PTJ", "-"))


        Return db.Process(query, param)
    End Function

    Private Function InsertPegawai(No_Arahan As String, No_Staf_Sah As String, Ketua_PTJ As String)
        Dim db As New DBKewConn


        Dim query As String = $"INSERT INTO SMKB_EOT_Pegawai (No_Mohon,No_Staf_SahL,No_Staf_SahB,No_Staf_LulusL,No_Staf_LulusB)
                            VALUES(@No_Mohon,@No_Staf_SahL,@No_Staf_SahB,@No_Staf_LulusL,@No_Staf_LulusB)"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@No_Mohon", No_Arahan))
        param.Add(New SqlParameter("@No_Staf_SahL", No_Staf_Sah))
        param.Add(New SqlParameter("@No_Staf_SahB", No_Staf_Sah))
        param.Add(New SqlParameter("@No_Staf_LulusL", Ketua_PTJ))
        param.Add(New SqlParameter("@No_Staf_LulusB", Ketua_PTJ))


        Return db.Process(query, param)

    End Function

    Private Function UpdatePegawai(No_Arahan As String, No_Staf_Sah As String)
        Dim db As New DBKewConn


        Dim query As String = $"UPDATE SMKB_EOT_Pegawai set No_Staf_SahL = @No_Staf_SahL , No_Staf_SahB = @No_Staf_SahB
                               where No_Mohon = @No_Mohon"

        Dim param As New List(Of SqlParameter)


        param.Add(New SqlParameter("@No_Staf_SahL", No_Staf_Sah))
        param.Add(New SqlParameter("@No_Staf_SahB", No_Staf_Sah))

        param.Add(New SqlParameter("@No_Mohon", No_Arahan))

        Return db.Process(query, param)

    End Function
    Private Function SemakLampiran(No_Arahan As String) As String
        Dim db As New DBKewConn
        Dim JumRekod As Integer
        Dim query As String = $"SELECT No_Mohon FROM SMKB_EOT_Dok_ArahanK WHERE No_Mohon = @No_Arahan"
        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@No_Arahan", No_Arahan))

        dt = db.Read(query, param)

        If dt.Rows.Count > 0 Then
            JumRekod = 1
        Else
            JumRekod = 0
        End If
        Return JumRekod

    End Function
    Private Function InsertLampiran(No_Arahan As String, Jen_Dok As String, Folder As String, File_Name As String, Tkh_Upload As Date)
        Dim db As New DBKewConn

        Dim query As String = $"INSERT INTO SMKB_EOT_Dok_ArahanK (No_Mohon, Jen_Dok, Folder, File_Name, Tkh_Upload)
                            VALUES(@No_Mohon,@Jen_Dok,@Folder,@File_Name,@Tkh_Upload)"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@No_Mohon", No_Arahan))
        param.Add(New SqlParameter("@Jen_Dok", Jen_Dok))
        param.Add(New SqlParameter("@Folder", Folder))
        param.Add(New SqlParameter("@File_Name", File_Name))
        param.Add(New SqlParameter("@Tkh_Upload", Tkh_Upload))


        Return db.Process(query, param)


    End Function

    Private Function UpdateLampiran(No_Arahan As String, File_Name As String, Tkh_Upload As Date)
        Dim db As New DBKewConn

        Dim query As String = $"UPDATE SMKB_EOT_Dok_ArahanK SET File_Name = @File_Name,Tkh_Upload=@Tkh_Upload
                                WHERE No_Mohon=@No_Mohon "

        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@File_Name", File_Name))
        param.Add(New SqlParameter("@Tkh_Upload", Tkh_Upload))
        param.Add(New SqlParameter("@No_Mohon", No_Arahan))


        Return db.Process(query, param)


    End Function

    <System.Web.Services.WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function InsertNewEOT(No_Arahan As String, No_Surat As String, No_Staf_Peg_AK As String, Kod_PTJ As String, KW As String, Kod_Vot As String, Tkh_Mula As Date, Tkh_Tamat As Date, Lokasi As String, PeneranganK As String)
        Dim db As New DBKewConn
        Dim strTkhToday As String = Now.ToString("yyyy-MM-dd")
        Dim dtTkhToday As DateTime = CDate(strTkhToday)

        Dim query As String = "INSERT INTO SMKB_EOT_Arahan_Kerja_Hdr
        VALUES(@No_Arahan, @No_Mohon,@No_Surat, @No_Staf_Peg_AK, @Kod_PTJ, @KW, @Kod_Vot, @Tkh_Mula, @Tkh_Tamat, @Lokasi, @PeneranganK,@Login_Created,@date_Created,@Login_modified,@date_modified)"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@No_Arahan", No_Arahan))
        param.Add(New SqlParameter("@No_Mohon", "-"))
        param.Add(New SqlParameter("@No_Surat", No_Surat))
        param.Add(New SqlParameter("@No_Staf_Peg_AK", Session("ssusrID")))
        param.Add(New SqlParameter("@Kod_PTJ", Kod_PTJ))
        param.Add(New SqlParameter("@KW", KW))
        param.Add(New SqlParameter("@Kod_Vot", Kod_Vot))
        param.Add(New SqlParameter("@Tkh_Mula", Tkh_Mula))
        param.Add(New SqlParameter("@Tkh_Tamat", Tkh_Tamat))
        param.Add(New SqlParameter("@Lokasi", Lokasi))
        param.Add(New SqlParameter("@PeneranganK", PeneranganK))
        param.Add(New SqlParameter("@Login_Created", No_Staf_Peg_AK))
        param.Add(New SqlParameter("@Date_Created", dtTkhToday))
        param.Add(New SqlParameter("@Login_modified", No_Staf_Peg_AK))
        param.Add(New SqlParameter("@Date_modified", dtTkhToday))



        Return db.Process(query, param)
    End Function
    <System.Web.Services.WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function InsertEOTHdr(No_Arahan As String, No_Mohon As String, No_Staf As String, Tkh_Mohon As Date, OT_Ptj As String)
        Dim db As New DBKewConn
        Dim strGajiS As String
        Dim strNoTel As String
        Dim param As New List(Of SqlParameter)

        Dim strTkhToday As String = Now.ToString("yyyy-MM-dd")
        Dim dtTkhToday As DateTime = CDate(strTkhToday)
        Using dtUserInfo = fGetUserInfo(Session("ssusrID"))
            If dtUserInfo.Rows.Count > 0 Then
                strGajiS = dtUserInfo.Rows.Item(0).Item("GajiS")
                strNoTel = dtUserInfo.Rows.Item(0).Item("NoTel")
            End If
        End Using


        Dim query As String = "INSERT INTO SMKB_EOT_Mohon_Hdr
        VALUES(@No_Mohon,@No_Arahan,@No_Staf, @Gaji, @Tkh_Mohon, @Status_Mohon, @Samb, @OT_Ptj, @Status_Cetak)"

        param.Add(New SqlParameter("@No_Mohon", No_Mohon))
        param.Add(New SqlParameter("@No_Arahan", No_Arahan))
        param.Add(New SqlParameter("@No_Staf", Session("ssusrID")))
        param.Add(New SqlParameter("@Gaji", strGajiS))
        param.Add(New SqlParameter("@Tkh_Mohon", Tkh_Mohon))
        param.Add(New SqlParameter("@Status_Mohon", "01"))
        param.Add(New SqlParameter("@Samb", strNoTel))
        param.Add(New SqlParameter("@OT_Ptj", OT_Ptj))
        param.Add(New SqlParameter("@Status_Cetak", "0"))




        Return db.Process(query, param)
    End Function
    <System.Web.Services.WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function UpdateEOTDtl(Tujuan As String, Catatan As String, No_Staf_Sah As String, OT_Ptj As String, No_Staf_Lulus As String, No_Mohon As String)
        Dim db As New DBKewConn
        Dim strTkhToday As String = Now.ToString("yyyy-MM-dd")
        Dim dtTkhToday As DateTime = CDate(strTkhToday)

        Dim query As String = "UPDATE SMKB_EOT_Mohon_Dtl Set Tujuan = @Tujuan, Catatan=@Catatan,No_Staf_Sah = @No_Staf_Sah, 
        OT_Ptj = @OT_Ptj,No_Staf_Lulus = @No_Staf_Lulus WHERE No_Mohon = @No_Mohon"
        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@Tujuan", Tujuan))
        param.Add(New SqlParameter("@Catatan", Catatan))
        param.Add(New SqlParameter("@No_Staf_Sah", No_Staf_Sah))
        param.Add(New SqlParameter("@OT_Ptj", OT_Ptj))
        param.Add(New SqlParameter("@No_Staf_Lulus", No_Staf_Lulus))
        param.Add(New SqlParameter("@No_Mohon", No_Mohon))

        Return db.Process(query, param)
    End Function
    <System.Web.Services.WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function UpdateNewEOT(No_Arahan As String, No_Surat As String, Tkh_Mula As Date, Tkh_Tamat As Date, Lokasi As String, PeneranganK As String)
        Dim db As New DBKewConn
        Dim strTkhToday As String = Now.ToString("yyyy-MM-dd")
        Dim dtTkhToday As DateTime = CDate(strTkhToday)

        Dim query As String = "UPDATE SMKB_EOT_Arahan_Kerja_Hdr
        SET No_Surat = @No_Surat, Tkh_Mula= @Tkh_Mula, Tkh_Tamat = @Tkh_Tamat, Lokasi = @Lokasi, 
        PeneranganK = @PeneranganK,Login_modified = @Login_modified , Date_modified = @date_modified WHERE No_Arahan = @No_Arahan"
        Dim param As New List(Of SqlParameter)



        param.Add(New SqlParameter("@No_Surat", No_Surat))
        param.Add(New SqlParameter("@Tkh_Mula", Tkh_Mula))
        param.Add(New SqlParameter("@Tkh_Tamat", Tkh_Tamat))
        param.Add(New SqlParameter("@Lokasi", Lokasi))
        param.Add(New SqlParameter("@PeneranganK", PeneranganK))
        param.Add(New SqlParameter("@Login_modified", Session("ssusrID")))
        param.Add(New SqlParameter("@Date_modified", dtTkhToday))
        param.Add(New SqlParameter("@No_Arahan", No_Arahan))



        Return db.Process(query, param)
    End Function

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function SimpanStafAK(selectedData As String(), noarahan As String) As String
        Dim resp As New ResponseRepository

        SimpanStafAK_(selectedData, noarahan)
        resp.Success("Rekod Berjaya Disimpan")
        Return JsonConvert.SerializeObject(resp.GetResult())
        ' Return a response message

    End Function

    Private Function SimpanStafAK_(selectedData As String(), noarahan As String) As String
        If noarahan = "" Then
            Exit Function
        End If
        Dim db As New DBKewConn

        For Each data As String In selectedData
            If SemakStafAK(noarahan, data) = 0 Then
                Dim query As String = "INSERT INTO SMKB_EOT_Arahan_Kerja_Dtl VALUES(@No_Arahan,@No_Staf)"
                Dim param As New List(Of SqlParameter)

                param.Add(New SqlParameter("@No_Arahan", noarahan))
                param.Add(New SqlParameter("@No_Staf", data))

                db.Process(query, param)
                SendEmail(noarahan)
            End If
        Next

    End Function

    Private Function SemakStafAK(No_Arahan As String, No_Staf As String) As String
        Dim db As New DBKewConn
        Dim JumRekod As Integer
        Dim query As String = $"SELECT No_Arahan,No_Staf FROM SMKB_EOT_Arahan_Kerja_Dtl WHERE No_Mohon = @No_Arahan and No_Staf =@No_Staf"
        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@No_Arahan", No_Arahan))
        param.Add(New SqlParameter("@No_Staf", No_Staf))

        dt = db.Read(query, param)

        If dt.Rows.Count > 0 Then
            JumRekod = 1
        Else
            JumRekod = 0
        End If
        Return JumRekod

    End Function


    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadDeleteStafAK(id As String, id1 As String) As String
        Dim resp As New ResponseRepository
        ' DeleteStafAK(id, id1)

        ' Return a response message
        'Return "Data saved successfully"

        If DeleteStafAK(id, id1) <> "OK" Then
            resp.Failed("Rekod Gagal dihapuskan")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        resp.Success("Rekod telah dihapuskan")
        Return JsonConvert.SerializeObject(resp.GetResult())



    End Function

    Private Function DeleteStafAK(idnostaf As String, idnoarahan As String) As String
        If idnoarahan = "" Then
            Exit Function
        End If

        Dim db As New DBKewConn

        Dim query As String = "DELETE FROM SMKB_EOT_Arahan_Kerja_Dtl WHERE No_Arahan = @No_Arahan AND  No_Staf = @No_Staf "
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@No_Arahan", idnoarahan))
        param.Add(New SqlParameter("@No_Staf", idnostaf))

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
            Dim savePath As String = Server.MapPath("~/UPLOAD/DOCUMENT/EOT/AR/" & fileName)


            ' Save the file to the specified path
            postedFile.SaveAs(savePath)

            ' Store the uploaded file name in session
            Session("UploadedFileName") = fileName

            Return " File uploaded successfully."
        Catch ex As Exception
            Return "Error uploading file: " & ex.Message
        End Try
    End Function


    <System.Web.Services.WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetVotCOA(ByVal q As String, ByVal ptj As String) As String

        ' Dim kdptj As String = Session("ssusrKodPTj")
        Dim tmpDT As DataTable = GetKodCOAList(q, ptj)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    Private Function GetKodCOAList(kodCariVot As String, kptj As String) As DataTable
        Dim db = New DBKewConn
        ' and a.Kod_PTJ = @kptj 
        Dim query As String = "SELECT Top(50) CONCAT(a.Kod_Vot, ' - ', vot.Butiran, ', ', a.Kod_Operasi, ' - ', ko.Butiran, ', ', kp.Butiran, ', ', a.Kod_Kump_Wang, ' - ', REPLACE(kw.Butiran, 'KUMPULAN WANG', 'KW'), ', ', mj.Pejabat) AS text,
                    a.Kod_Vot AS value ,
                    mj.Pejabat as colPTJ , kw.Butiran as colKW , ko.Butiran as colKO ,  kp.Butiran as colKp , 
                    a.Kod_PTJ as colhidptj , a.Kod_Kump_Wang as colhidkw , a.Kod_Operasi as colhidko , a.Kod_Projek as colhidkp
                    FROM SMKB_COA_Master AS a 
                    JOIN SMKB_Vot AS vot ON a.Kod_Vot = vot.Kod_Vot
                    JOIN SMKB_Operasi AS ko ON a.Kod_Operasi = ko.Kod_Operasi
                    JOIN SMKB_Kump_Wang AS kw ON a.Kod_Kump_Wang = kw.Kod_Kump_Wang
					JOIN SMKB_Projek as kp on kp.Kod_Projek = a.Kod_Projek
					JOIN [DEVMIS\SQL_INS01].dbStaf.dbo.MS_PEJABAT AS mj ON mj.status = '1' and mj.kodpejabat = left(a.Kod_PTJ,2) 
                    WHERE a.status = 1 and a.Kod_Vot = '14000' AND a.Kod_PTJ = @ptj "

        Dim param As New List(Of SqlParameter)


        param.Add(New SqlParameter("@ptj", kptj))
        If kodCariVot <> "" Then
            query &= "AND (a.Kod_Vot LIKE '%' + @kod + '%' OR a.Kod_Operasi LIKE '%' + @kod2 + '%' OR a.Kod_Projek LIKE '%' + @kod3 + '%' OR a.Kod_Kump_Wang LIKE '%' + @kod4 + '%' OR a.Kod_PTJ LIKE '%' + @kod5 + '%' OR vot.Butiran LIKE '%' + @kodButir + '%' OR ko.Butiran LIKE '%' + @kodButir1 + '%'  OR kw.Butiran LIKE '%' + @kodButir2 + '%' OR mj.pejabat LIKE '%' + @kodButir3 + '%') "

            param.Add(New SqlParameter("@kod", kodCariVot))
            param.Add(New SqlParameter("@kod2", kodCariVot))
            param.Add(New SqlParameter("@kod3", kodCariVot))
            param.Add(New SqlParameter("@kod4", kodCariVot))
            param.Add(New SqlParameter("@kod5", kodCariVot))
            param.Add(New SqlParameter("@kodButir", kodCariVot))
            param.Add(New SqlParameter("@kodButir1", kodCariVot))
            param.Add(New SqlParameter("@kodButir2", kodCariVot))
            param.Add(New SqlParameter("@kodButir3", kodCariVot))
            ' param.Add(New SqlParameter("@kptj", Session("ssusrKodPTj")))

        End If

        Return db.Read(query, param)
    End Function

    Public strConEmail As String = "Provider=SQLOLEDB;Driver={SQL Server};server=V-SQL12.utem.edu.my\SQL_INS02;database=dbKewangan;uid=Smkb;pwd=smkb*pwd;"

    Public Function myEmel(alamat, subject, body)
        Dim cnExec As OleDb.OleDbConnection
        Dim cmdExec As OleDb.OleDbCommand

        Try
            cnExec = New OleDb.OleDbConnection(strConEmail)
            cnExec.Open()

            cmdExec = New OleDb.OleDbCommand("EXEC msdb.dbo.sp_send_dbmail @profile_name= 'EmailSmkb', @recipients= '" & alamat & "', @subject = '" & subject & "', " &
                  "@body= '" & Replace(body, "'", "''") & "', @body_format='HTML';", cnExec)
            cmdExec.ExecuteNonQuery()
            cmdExec.Dispose()
            cmdExec = Nothing
            cnExec.Dispose()
            cnExec.Close()
            cnExec = Nothing

            Return 1    'Proses Berjaya
        Catch ex As Exception
            ' Show the exception's message.
            MsgBox(ex.Message)
            Return 0    'Proses Gagal
        End Try

    End Function
    Private Function SendEmail(noarahan As String) As String
        Dim fullName As String = "Rozana binti Abu Bakar"
        Dim email As String = "rozana@utem.edu.my"

        ' Send the new password to the user's email
        Dim subject As String = "UTeM - Sistem Maklumat Kewangan Bersepadu"
        Dim body As String = "Arahan Kerja " _
        & "<br><br>" _
        & vbCrLf & "Assalamualaikum Dan Salam Sejahtera " & fullName & "," _
        & "<br><br>" _
        & vbCrLf & "Sila buat permohonan EOT mengikut no arahan " & noarahan & "." _
        & "<br><br>" _
        & vbCrLf & "Sila log masuk ke dalam UTeM - Sistem Maklunat Kewangan Bersepadu." _
        & "<br>" _
        & "<br><br>" _
        & vbCrLf & "Email ini dijanakan secara automatik daripada UTeM - Sistem Maklunat Kewangan Bersepadu. " _
        & "<br><br>"

        myEmel(email, subject, body)

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
End Class