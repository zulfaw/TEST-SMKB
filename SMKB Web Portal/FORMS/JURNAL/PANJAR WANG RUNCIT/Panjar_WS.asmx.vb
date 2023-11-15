Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Web.Script.Services
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports Newtonsoft.Json

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class Panjar_WS
    Inherits System.Web.Services.WebService

    Dim dt As DataTable

    <WebMethod()>
    Public Function HelloWorld() As String
        Return "Hello World"
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadJumlah_Mohon() As String
        dt = GetJumlah_Mohon()
        Return JsonConvert.SerializeObject(dt)
    End Function

    Private Function GetJumlah_Mohon() As DataTable
        Dim db As New DBKewConn

        Dim query As String = "SELECT COUNT(*) AS Jumlah_Permohonan FROM SMKB_WPR_Maklumat_Hdr WHERE Kod_PTJ = @ptj"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@ptj", Session("ssusrKodPTj")))

        Return db.Read(query, param)
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadJumlah_Lulus() As String
        dt = GetJumlah_Lulus()
        Return JsonConvert.SerializeObject(dt)
    End Function

    Private Function GetJumlah_Lulus() As DataTable
        Dim db As New DBKewConn

        Dim query As String = "SELECT COUNT(a.No_Wpr) AS Jumlah_PermohonanLulus FROM SMKB_WPR_Dtl AS a
        JOIN SMKB_WPR_Maklumat_Hdr AS b ON a.No_Wpr = b.No_Wpr
        WHERE a.Status = 'L' AND b.Kod_PTJ = @ptj"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@ptj", Session("ssusrKodPTj")))

        Return db.Read(query, param)
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadJumlah_Tolak() As String
        dt = GetJumlah_Tolak()
        Return JsonConvert.SerializeObject(dt)
    End Function

    Private Function GetJumlah_Tolak() As DataTable
        Dim db As New DBKewConn

        Dim query As String = "SELECT COUNT(a.No_Wpr) AS Jumlah_PermohonanTolak FROM SMKB_WPR_Dtl AS a
        JOIN SMKB_WPR_Maklumat_Hdr AS b ON a.No_Wpr = b.No_Wpr
        WHERE a.Status_Gantian = '1' AND b.Kod_PTJ = @ptj"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@ptj", Session("ssusrKodPTj")))

        Return db.Read(query, param)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetCOA(ByVal q As String) As String
        Dim tmptDT As DataTable = GetKodCOAList(q)
        Return JsonConvert.SerializeObject(tmptDT)
    End Function

    Private Function GetKodCOAList(kodCariVot As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT TOP 10 CONCAT(a.Kod_Vot, '-', vot.Butiran, ', ', a.Kod_Operasi, '-', ko.Butiran, 
        ', ', a.Kod_Projek, '-', kp.Butiran, ', ', a.Kod_Kump_Wang, '-', REPLACE(kw.Butiran, 'KUMPULAN WANG', 'KW'), ', ',
        LEFT(a.Kod_PTJ,2),'-', mj.Pejabat) AS text,
                    a.Kod_Vot AS value, mj.Pejabat as colPTJ, kw.Butiran as colKW, ko.Butiran as colKo,
                    kp.Butiran as colKp, a.Kod_PTJ as colhidPtj, a.Kod_Kump_Wang as colhidKw, a.Kod_Operasi as colhidKo, a.Kod_Projek as colhidKp
                    FROM SMKB_COA_Master AS a 
                    JOIN SMKB_Vot AS vot ON a.Kod_Vot = vot.Kod_Vot
                    JOIN SMKB_Operasi AS ko ON a.Kod_Operasi = ko.Kod_Operasi
                    JOIN SMKB_Kump_Wang AS kw ON a.Kod_Kump_Wang = kw.Kod_Kump_Wang
                        JOIN SMKB_Projek as kp on kp.Kod_Projek = a.Kod_Projek
					    JOIN [DEVMIS\SQL_INS01].dbStaf.dbo.MS_PEJABAT AS mj ON mj.status = '1' and mj.kodpejabat = left(a.Kod_PTJ,2) 
                    WHERE a.status = 1"

        Dim param As New List(Of SqlParameter)
        If kodCariVot <> "" Then
            query &= "AND (a.Kod_Vot LIKE '%' + @kod + '%' OR a.Kod_Operasi LIKE '%' + @kod2 + '%' OR a.Kod_Projek LIKE '%' + @kod3 + '%' OR a.Kod_Kump_Wang LIKE '%' + 
            @kod4 + '%' OR a.Kod_PTJ LIKE '%' + @kod5 + '%' OR vot.Butiran LIKE '%' + @kodButir + '%' OR ko.Butiran LIKE '%' + @kodButir1 + '%'  OR kw.Butiran LIKE '%' + 
            @kodButir2 + '%' OR mj.pejabat LIKE '%' + @kodButir3 + '%')"

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

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetKump(ByVal q As String, ByVal kodptj As String) As String
        Dim tmpDT As DataTable = GetKumpulan(q, kodptj)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetKumpulan(kod As String, kodptj As String) As DataTable

        Dim db As New DBKewConn
        Dim query As String = "select Kod_Kump as value, Butiran as text From SMKB_WPR_Kumpulan"
        Dim param As New List(Of SqlParameter)

        If kod <> "" Then
            query &= " where Kod_Kump LIKE '%' + @kod + '%' or Butiran LIKE '%' + @kod2 + '%'"
            param.Add(New SqlParameter("@kod", kod))
            param.Add(New SqlParameter("@kod2", kod))
        Else
            query &= " where Kod_PTJ = @kod3"
        End If
        param.Add(New SqlParameter("@kod", kod))
        param.Add(New SqlParameter("@kod2", kod))
        param.Add(New SqlParameter("@kod3", kodptj))
        Return db.Read(query, param)

    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetPTJAgih(ByVal q As String) As String
        Dim tmpDT As DataTable = GetPTJAgihan(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetPTJAgihan(kod As String) As DataTable

        Dim db As New DBKewConn

        Dim query As String = "SELECT DISTINCT TOP 5 a.Kod_PTJ AS value, b.Pejabat AS text
        FROM SMKB_WPR_Agih AS a
        JOIN [DEVMIS\SQL_INS01].dbStaf.dbo.MS_PEJABAT AS b ON a.Kod_PTJ = b.KodPejPBU "

        Dim param As New List(Of SqlParameter)

        If kod <> "" Then
            query &= " WHERE b.Pejabat LIKE '%' + @kod + '%' OR b.Singkatan LIKE '%' + @kod + '%'"
            param.Add(New SqlParameter("@kod", kod))
        End If

        Return db.Read(query, param)

    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadOrderRecord_SenaraiPermohonan(category_filter As String, isClicked As Boolean, tkhMula As DateTime, tkhTamat As DateTime) As String
        Dim resp As New ResponseRepository
        If isClicked = False Then
            Return JsonConvert.SerializeObject(New DataTable)
        End If
        dt = GetOrder_SenaraiPermohonan(category_filter, tkhMula, tkhTamat)
        Return JsonConvert.SerializeObject(dt)
    End Function

    Private Function GetOrder_SenaraiPermohonan(category_filter As String, tkhMula As DateTime, tkhTamat As DateTime) As DataTable

        Dim db As New DBKewConn

        Dim tarikhQuery As String = ""
        Dim param As New List(Of SqlParameter)

        If category_filter = "1" Then 'Harini
            'tarikhQuery = " and a.Tkh_Transaksi = getdate() "
            tarikhQuery = " AND CAST(Tarikh_Mohon AS DATE) = CAST(getdate() AS DATE) "
        ElseIf category_filter = "2" Then 'Semalam
            'tarikhQuery = " and a.Tkh_Transaksi >= DATEADD(day, -1, getdate()) and a.Tkh_Transaksi <= getdate() "
            tarikhQuery = " AND CAST(Tarikh_Mohon AS DATE) = CAST(DATEADD(DAY, -1, GETDATE()) AS DATE) "
        ElseIf category_filter = "3" Then 'seminggu
            'tarikhQuery = " and a.Tkh_Transaksi >= DATEADD(day, -8, getdate()) and a.Tkh_Transaksi <= getdate() "
            tarikhQuery = " AND CAST(Tarikh_Mohon AS DATE) >= CAST(DATEADD(DAY, -7, GETDATE()) AS DATE) "
        ElseIf category_filter = "4" Then '30 hari
            tarikhQuery = " AND Tarikh_Mohon >= DATEADD(month, -1, getdate()) and Tarikh_Mohon <= getdate() "
        ElseIf category_filter = "5" Then '60 hari
            tarikhQuery = " AND Tarikh_Mohon >= DATEADD(month, -2, getdate()) and Tarikh_Mohon <= getdate() "
        ElseIf category_filter = "6" Then 'custom
            tarikhQuery = " AND Tarikh_Mohon >= @tkhMula and Tarikh_Mohon <= @TkhTamat "
            param = New List(Of SqlParameter)
            param.Add(New SqlParameter("@tkhMula", tkhMula))
            param.Add(New SqlParameter("@TkhTamat", tkhTamat))
        End If

        Dim query As String = "SELECT No_Wpr, Kod_PTJ, FORMAT (Tarikh_Mohon, 'dd-MM-yyyy') as Tarikh_Mohon, Jumlah_Belanja, 
        (SELECT Butiran FROM SMKB_Kod_Status_Dok WHERE Kod_Modul = '15' AND Status = 1 AND Kod_Status_Dok = SMKB_WPR_Maklumat_Hdr.Status_Dok) AS Kod_Status_Dok
        FROM SMKB_WPR_Maklumat_Hdr WHERE Kod_PTJ = @ptj AND Status_Dok BETWEEN '01' AND '02'" & tarikhQuery

        param.Add(New SqlParameter("@ptj", Session("ssusrKodPTj")))

        Return db.Read(query, param)

    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadRecord_PTj(ByVal ptj As String) As String
        dt = GetPTj(ptj)
        Return JsonConvert.SerializeObject(dt)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetPTj(ptj As String) As DataTable

        Dim db As New DBKewConn
        Dim query As String = "SELECT Pejabat FROM [DEVMIS\SQL_INS01].dbStaf.dbo.MS_PEJABAT WHERE KodPejPBU = @PTj"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@PTj", ptj))

        Return db.Read(query, param)

    End Function

    Private Function DeleteRecord(id As String)
        Dim db As New DBKewConn
        Dim query As String = "DELETE a FROM SMKB_WPR_Maklumat_Hdr As a
        FULL Join SMKB_WPR_Dtl AS b ON a.No_Wpr = b.No_Wpr 
        WHERE b.No_Wpr =  @orderMid"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@orderMid", id))

        Return db.Process(query, param)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadOrder_KumpulanPTJ(ByVal ptj As String) As String
        Dim resp As New ResponseRepository
        dt = GetOrder_KumpulanPTJ(ptj)
        'resp.SuccessPayload(dt)
        Return JsonConvert.SerializeObject(dt)
    End Function

    Private Function GetOrder_KumpulanPTJ(ptj As String) As DataTable
        Dim db As New DBKewConn

        Dim query As String = "SELECT Kod_Kump_Wang FROM SMKB_WPR_Agih WHERE Kod_PTJ = @ptj"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@ptj", ptj))

        Return db.Read(query, param)
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadOrderRecord_SenaraiCetakan() As String
        dt = GetOrder_SenaraiCetakan()
        Return JsonConvert.SerializeObject(dt)
    End Function

    Private Function GetOrder_SenaraiCetakan() As DataTable

        Dim db As New DBKewConn
        Dim query As String = "SELECT No_Wpr, (SELECT b.Pejabat FROM [DEVMIS\SQL_INS01].dbStaf.dbo.MS_PEJABAT AS b
        WHERE KodPejPBU = SMKB_WPR_Maklumat_Hdr.Kod_PTJ) AS Kod_PTJ, FORMAT (Tarikh_Mohon, 'dd-MM-yyyy') AS Tarikh_Mohon, FORMAT (Jumlah_Belanja,'0.00') AS Jumlah_Belanja
        FROM SMKB_WPR_Maklumat_Hdr WHERE Kod_PTJ = @ptj AND Status_Dok BETWEEN '01' AND '02'"

        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@ptj", Session("ssusrKodPTj")))


        Return db.Read(query, param)

    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadOrderRecord_SenaraiPemeriksaanPermohonan(category_filter As String, isClicked As Boolean, tkhMula As DateTime, tkhTamat As DateTime) As String
        Dim resp As New ResponseRepository
        If isClicked = False Then
            Return JsonConvert.SerializeObject(New DataTable)
        End If
        dt = GetOrder_SenaraiPemeriksaanPermohonan(category_filter, tkhMula, tkhTamat)
        Return JsonConvert.SerializeObject(dt)
    End Function

    Private Function GetOrder_SenaraiPemeriksaanPermohonan(category_filter As String, tkhMula As DateTime, tkhTamat As DateTime) As DataTable

        Dim db As New DBKewConn

        Dim tarikhQuery As String = ""
        Dim param As New List(Of SqlParameter)

        If category_filter = "1" Then 'Harini
            'tarikhQuery = " and a.Tkh_Transaksi = getdate() "
            tarikhQuery = " AND CAST(Tarikh_Mohon AS DATE) = CAST(getdate() AS DATE) "
        ElseIf category_filter = "2" Then 'Semalam
            'tarikhQuery = " and a.Tkh_Transaksi >= DATEADD(day, -1, getdate()) and a.Tkh_Transaksi <= getdate() "
            tarikhQuery = " AND CAST(Tarikh_Mohon AS DATE) = CAST(DATEADD(DAY, -1, GETDATE()) AS DATE) "
        ElseIf category_filter = "3" Then 'seminggu
            'tarikhQuery = " and a.Tkh_Transaksi >= DATEADD(day, -8, getdate()) and a.Tkh_Transaksi <= getdate() "
            tarikhQuery = " AND CAST(Tarikh_Mohon AS DATE) >= CAST(DATEADD(DAY, -7, GETDATE()) AS DATE) "
        ElseIf category_filter = "4" Then '30 hari
            tarikhQuery = " AND Tarikh_Mohon >= DATEADD(month, -1, getdate()) and Tarikh_Mohon <= getdate() "
        ElseIf category_filter = "5" Then '60 hari
            tarikhQuery = " AND Tarikh_Mohon >= DATEADD(month, -2, getdate()) and Tarikh_Mohon <= getdate() "
        ElseIf category_filter = "6" Then 'custom
            tarikhQuery = " AND Tarikh_Mohon >= @tkhMula and Tarikh_Mohon <= @TkhTamat "
            param = New List(Of SqlParameter)
            param.Add(New SqlParameter("@tkhMula", tkhMula))
            param.Add(New SqlParameter("@TkhTamat", tkhTamat))
        End If

        Dim query As String = "SELECT No_Wpr, (SELECT b.Pejabat FROM [DEVMIS\SQL_INS01].dbStaf.dbo.MS_PEJABAT AS b
        WHERE KodPejPBU = SMKB_WPR_Maklumat_Hdr.Kod_PTJ) AS Kod_PTJ, FORMAT (Tarikh_Mohon, 'dd-MM-yyyy') AS Tarikh_Mohon, FORMAT (Jumlah_Belanja,'0.00') AS Jumlah_Belanja, 
        (SELECT Butiran FROM SMKB_Kod_Status_Dok WHERE Kod_Modul = '15' AND Status = 1 AND Kod_Status_Dok = SMKB_WPR_Maklumat_Hdr.Status_Dok) AS Kod_Status_Dok
        FROM SMKB_WPR_Maklumat_Hdr WHERE Status_Dok = '03' " & tarikhQuery

        Return db.Read(query, param)

    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadOrderRecord_SenaraiPenerimaan(category_filter As String, isClicked As Boolean, tkhMula As DateTime, tkhTamat As DateTime) As String
        Dim resp As New ResponseRepository
        If isClicked = False Then
            Return JsonConvert.SerializeObject(New DataTable)
        End If
        dt = GetOrder_SenaraiPenerimaan(category_filter, tkhMula, tkhTamat)
        Return JsonConvert.SerializeObject(dt)
    End Function

    Private Function GetOrder_SenaraiPenerimaan(category_filter As String, tkhMula As DateTime, tkhTamat As DateTime) As DataTable

        Dim db As New DBKewConn

        Dim tarikhQuery As String = ""
        Dim param As New List(Of SqlParameter)

        If category_filter = "1" Then 'Harini
            'tarikhQuery = " and a.Tkh_Transaksi = getdate() "
            tarikhQuery = " AND CAST(Tarikh_Mohon AS DATE) = CAST(getdate() AS DATE) "
        ElseIf category_filter = "2" Then 'Semalam
            'tarikhQuery = " and a.Tkh_Transaksi >= DATEADD(day, -1, getdate()) and a.Tkh_Transaksi <= getdate() "
            tarikhQuery = " AND CAST(Tarikh_Mohon AS DATE) = CAST(DATEADD(DAY, -1, GETDATE()) AS DATE) "
        ElseIf category_filter = "3" Then 'seminggu
            'tarikhQuery = " and a.Tkh_Transaksi >= DATEADD(day, -8, getdate()) and a.Tkh_Transaksi <= getdate() "
            tarikhQuery = " AND CAST(Tarikh_Mohon AS DATE) >= CAST(DATEADD(DAY, -7, GETDATE()) AS DATE) "
        ElseIf category_filter = "4" Then '30 hari
            tarikhQuery = " AND Tarikh_Mohon >= DATEADD(month, -1, getdate()) and Tarikh_Mohon <= getdate() "
        ElseIf category_filter = "5" Then '60 hari
            tarikhQuery = " AND Tarikh_Mohon >= DATEADD(month, -2, getdate()) and Tarikh_Mohon <= getdate() "
        ElseIf category_filter = "6" Then 'custom
            tarikhQuery = " AND Tarikh_Mohon >= @tkhMula and Tarikh_Mohon <= @TkhTamat "
            param = New List(Of SqlParameter)
            param.Add(New SqlParameter("@tkhMula", tkhMula))
            param.Add(New SqlParameter("@TkhTamat", tkhTamat))
        End If

        Dim query As String = "SELECT No_Wpr, FORMAT (Tarikh_Mohon, 'dd-MM-yyyy') as Tarikh_Mohon, FORMAT (Jumlah_Belanja, '0.00') AS Jumlah_Belanja,
        FORMAT (Jumlah_Tolak, '0.00') AS Jumlah_Tolak, FORMAT (Jumlah_Bersih, '0.00') AS Jumlah_Bersih, (SELECT b.Pejabat FROM [DEVMIS\SQL_INS01].dbStaf.dbo.MS_PEJABAT AS b
        WHERE KodPejPBU = SMKB_WPR_Maklumat_Hdr.Kod_PTJ) AS Kod_PTJ, (SELECT Butiran FROM SMKB_WPR_Kumpulan WHERE Kod_PTJ = SMKB_WPR_Maklumat_Hdr.Kod_PTJ AND 
        Kod_Kump = SMKB_WPR_Maklumat_Hdr.Kod_Kump_Wang ) AS Kod_Kump_Wang
        FROM SMKB_WPR_Maklumat_Hdr WHERE Kod_PTJ = @ptj AND Status_Dok = '05' " & tarikhQuery



        param.Add(New SqlParameter("@ptj", Session("ssusrKodPTj")))

        Return db.Read(query, param)

    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadRecord_Pengagihan(ByVal ptj As String) As String
        dt = GetRecord_Pengagihan(ptj)
        Return JsonConvert.SerializeObject(dt)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetRecord_Pengagihan(ptj As String) As DataTable

        Dim db As New DBKewConn
        Dim param As New List(Of SqlParameter)

        Dim query As String = "SELECT Kod_PTJ, (SELECT b.Pejabat FROM [DEVMIS\SQL_INS01].dbStaf.dbo.MS_PEJABAT AS b
        WHERE KodPejPBU = SMKB_WPR_Agih.Kod_PTJ) AS Pejabat, (SELECT Butiran FROM SMKB_WPR_Kumpulan 
        WHERE Kod_PTJ = SMKB_WPR_Agih.Kod_PTJ AND Kod_Kump = SMKB_WPR_Agih.Kod_Kump_Wang) AS Butiran, Kod_Kump_Wang, Jumlah_Agih, Baki
        FROM SMKB_WPR_Agih WHERE Kod_PTJ = @ptj"

        param.Add(New SqlParameter("@ptj", ptj))

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

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetHdrPermohonan(id As String) As DataTable

        Dim db As New DBKewConn
        Dim query As String = "select No_Wpr, Kod_PTJ, FORMAT (Tarikh_Mohon, 'yyyy-MM-dd') as Tarikh_Mohon, Kod_Kump_Wang,
        (select Butiran FROM SMKB_WPR_Kumpulan where Kod_PTJ = SMKB_WPR_Maklumat_Hdr.Kod_PTJ and Kod_Kump = SMKB_WPR_Maklumat_Hdr.Kod_Kump_Wang) as ButiranKumpulan
        from SMKB_WPR_Maklumat_Hdr where No_Wpr = @No_Wpr"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@No_Wpr", id))

        Return db.Read(query, param)

    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadCetakan(ByVal id As String) As String
        Dim resp As New ResponseRepository
        dt = GetCetakan(id)
        resp.SuccessPayload(dt)
        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    Private Function GetCetakan(id As String) As DataTable

        Dim db As New DBKewConn
        Dim query As String = "SELECT a.No_Wpr, (SELECT b.Singkatan FROM [DEVMIS\SQL_INS01].dbStaf.dbo.MS_PEJABAT AS b
        WHERE KodPejPBU = a.Kod_PTJ) AS Pejabat, a.Kod_Kump_Wang, FORMAT (a.Tarikh_Mohon, 'dd/MM/yyyy') as Tarikh_Mohon,
        a.Jumlah_Belanja, d.Butiran_Belanja, c.MS01_Email as Email, c.MS01_Nama as Nama, c.MS01_NoAkaun as Akaun
        FROM SMKB_WPR_Maklumat_Hdr AS a
        JOIN SMKB_WPR_Dtl as d ON a.No_Wpr = d.No_Wpr
        JOIN [DEVMIS\SQL_INS01].dbStaf.dbo.MS01_PERIBADI as c ON @nostaf = c.MS01_NoStaf
        WHERE a.No_Wpr = @No_Wpr"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@No_Wpr", id))
        param.Add(New SqlParameter("@nostaf", Session("ssusrID")))

        Return db.Read(query, param)

    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadHdrPenerimaan(ByVal id As String) As String
        Dim resp As New ResponseRepository
        dt = GetHdrPenerimaan(id)
        resp.SuccessPayload(dt)
        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    Private Function GetHdrPenerimaan(id As String) As DataTable
        Dim db As New DBKewConn

        Dim query As String = "SELECT No_Wpr, FORMAT(Tarikh_Mohon,'yyyy-MM-dd') AS Tarikh_Mohon, Kod_PTJ, Kod_Kump_Wang,
        (SELECT Butiran FROM SMKB_WPR_Kumpulan WHERE SMKB_WPR_Kumpulan.Kod_Kump = SMKB_WPR_Maklumat_Hdr.Kod_Kump_Wang AND
        SMKB_WPR_Kumpulan.Kod_PTJ = SMKB_WPR_Maklumat_Hdr.Kod_PTJ) AS ButiranKumpulan
        FROM SMKB_WPR_Maklumat_Hdr WHERE No_Wpr = @NoWpr"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@NoWpr", id))

        Return db.Read(query, param)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadPenerimaan(ByVal id As String) As String
        Dim resp As New ResponseRepository
        dt = GetPenerimaan(id)
        resp.SuccessPayload(dt)
        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    Private Function GetPenerimaan(id As String) As DataTable
        Dim db As New DBKewConn

        Dim query As String = "SELECT a.Kod_Kump_Wang, a.Butiran_Belanja, a.No_Resit, a.Kod_PTJ, FORMAT (a.Jumlah_Butiran, '0.00') AS Jumlah_Butiran, 
        FORMAT (a.Baki, '0.00') AS Baki, a.Kod_Vot, (SELECT b.Pejabat FROM [DEVMIS\SQL_INS01].dbStaf.dbo.MS_PEJABAT as b
        WHERE b.STATUS = 1 and b.kodpejabat = left(Kod_PTJ,2)) as ButiranPTJ, kw.Butiran as ButiranKw,
        (SELECT Butiran FROM SMKB_Vot WHERE Kod_Vot = a.Kod_Vot) as Butiran, FORMAT(a.Jumlah_Lulus, '0.00') AS Jumlah_Lulus
        FROM SMKB_WPR_Dtl AS a
        JOIN SMKB_Kump_Wang AS kw ON a.Kod_Kump_Wang = kw.Kod_Kump_Wang
        WHERE a.No_Wpr = @No_Wpr"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@No_Wpr", id))

        Return db.Read(query, param)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadBaki_Pengagihan(ByVal ptj As String, ByVal kumpulan As String) As String
        dt = GetBaki_Pengagihan(ptj, kumpulan)
        Return JsonConvert.SerializeObject(dt)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetBaki_Pengagihan(ptj As String, kumpulan As String) As DataTable

        Dim db As New DBKewConn
        Dim query As String = "SELECT a.Jumlah_Agih, a.Baki, b.Butiran, mj.Pejabat
        FROM SMKB_WPR_Agih AS a
        INNER JOIN SMKB_WPR_Kumpulan AS b ON a.Kod_PTJ = b.Kod_PTJ AND a.Kod_Kump_Wang = b.Kod_Kump
        INNER JOIN [DEVMIS\SQL_INS01].dbStaf.dbo.MS_PEJABAT AS mj ON mj.kodpejabat = left(a.Kod_PTJ,2)  
        WHERE a.Kod_PTJ = @KodPTJ"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@KodPTJ", ptj))
        'param.Add(New SqlParameter("@KodKumpWang", kumpulan))

        Return db.Read(query, param)

    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadGantian(ByVal ptj As String, ByVal kump As String) As String
        Dim resp As New ResponseRepository
        dt = GetGantian(ptj, kump)
        resp.SuccessPayload(dt)
        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetGantian(ptj As String, kump As String) As DataTable

        Dim db As New DBKewConn
        Dim query As String = "SELECT Butiran_Belanja, No_Resit, (SELECT Butiran FROM SMKB_Vot WHERE Kod_Vot = SMKB_WPR_Dtl.Kod_Vot) as Butiran,
        Kod_PTJ, Kod_Vot, FORMAT (Jumlah_Butiran, '0.00') as Amaun, Status_Gantian, Baki_Peruntukan
        FROM SMKB_WPR_Dtl WHERE Kod_PTJ =@Kod_PTJ AND Kod_Kump = @Kump AND Status_Gantian = '1'"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@Kod_PTJ", ptj))
        param.Add(New SqlParameter("@Kump", kump))

        Return db.Read(query, param)

    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadOrder_Gantian(category_filter As String, isClicked As Boolean, tkhMula As DateTime, tkhTamat As DateTime, ptj As String, kump As String) As String
        Dim resp As New ResponseRepository
        If isClicked = False Then
            Return JsonConvert.SerializeObject(New DataTable)
        End If
        dt = GetOrder_Gantian(category_filter, tkhMula, tkhTamat, ptj, kump)
        Return JsonConvert.SerializeObject(dt)
    End Function

    Private Function GetOrder_Gantian(category_filter As String, tkhMula As DateTime, tkhTamat As DateTime, ptj As String, kump As String) As DataTable
        Dim db As New DBKewConn

        Dim tarikhQuery = ""
        Dim param As New List(Of SqlParameter)

        If category_filter = "1" Then 'Harini
            'tarikhQuery = " and a.Tkh_Transaksi = getdate() "
            tarikhQuery = " AND CAST(Tarikh_Mohon AS DATE) = CAST(getdate() AS DATE) "
        ElseIf category_filter = "2" Then 'Semalam
            'tarikhQuery = " and a.Tkh_Transaksi >= DATEADD(day, -1, getdate()) and a.Tkh_Transaksi <= getdate() "
            tarikhQuery = " AND CAST(Tarikh_Mohon AS DATE) = CAST(DATEADD(DAY, -1, GETDATE()) AS DATE) "
        ElseIf category_filter = "3" Then 'seminggu
            'tarikhQuery = " and a.Tkh_Transaksi >= DATEADD(day, -8, getdate()) and a.Tkh_Transaksi <= getdate() "
            tarikhQuery = " AND CAST(Tarikh_Mohon AS DATE) >= CAST(DATEADD(DAY, -7, GETDATE()) AS DATE) "
        ElseIf category_filter = "4" Then '30 hari
            tarikhQuery = " AND Tarikh_Mohon >= DATEADD(month, -1, getdate()) and Tarikh_Mohon <= getdate() "
        ElseIf category_filter = "5" Then '60 hari
            tarikhQuery = " AND Tarikh_Mohon >= DATEADD(month, -2, getdate()) and Tarikh_Mohon <= getdate() "
        ElseIf category_filter = "6" Then 'custom
            tarikhQuery = " AND Tarikh_Mohon >= @tkhMula and Tarikh_Mohon <= @TkhTamat "
            param = New List(Of SqlParameter)
            param.Add(New SqlParameter("@tkhMula", tkhMula))
            param.Add(New SqlParameter("@TkhTamat", tkhTamat))
        End If

        Dim query As String = "SELECT a.No_Wpr, FORMAT(a.Tarikh_Mohon,'dd/MM/yyyy') as Tarikh_Mohon, a.Jumlah_Belanja,
        a.Status_Dok FROM SMKB_WPR_Maklumat_Hdr AS a
        INNER JOIN SMKB_WPR_Dtl AS b ON a.No_Wpr = b.No_Wpr
        WHERE a.Kod_PTJ = @ptj AND a.Kod_Kump_Wang = @kump AND b.Status_Gantian = '1'" & tarikhQuery

        param.Add(New SqlParameter("@ptj", ptj))
        param.Add(New SqlParameter("@kump", kump))

        Return db.Read(query, param)
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadOrder_SenaraiLaporan(isClicked As Boolean, tahun As String, bulan As String) As String
        Dim resp As New ResponseRepository
        If isClicked = False Then
            Return JsonConvert.SerializeObject(New DataTable)
        End If
        dt = GetOrder_SenaraiLaporan(tahun, bulan)
        'resp.SuccessPayload(dt)
        Return JsonConvert.SerializeObject(dt)
    End Function

    Private Function GetOrder_SenaraiLaporan(tahun, bulan) As DataTable
        Dim db As New DBKewConn

        Dim tahunBulan = tahun + "-" + bulan
        Dim tarikhQuery = ""
        Dim param As New List(Of SqlParameter)

        tarikhQuery = " AND CONCAT(YEAR(Tarikh_Mohon),'-',MONTH(Tarikh_Mohon)) = @tahunBulan"
        param = New List(Of SqlParameter)
        param.Add(New SqlParameter("@tahunBulan", tahunBulan))

        Dim query As String = "SELECT DISTINCT SUBSTRING(No_Wpr, 1, 5) AS No_Wpr, Status_Dok FROM SMKB_WPR_Maklumat_Hdr
        WHERE Kod_PTJ = @ptj AND Status_Dok = '06'" & tarikhQuery

        param.Add(New SqlParameter("@ptj", Session("ssusrKodPTj")))

        Return db.Read(query, param)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadRecordPermohonan(ByVal id As String) As String
        Dim resp As New ResponseRepository
        dt = GetRecordPermohonan(id)
        resp.SuccessPayload(dt)
        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    Private Function GetRecordPermohonan(id As String) As DataTable

        Dim db As New DBKewConn
        Dim query As String = "SELECT a.Kod_Kump_Wang, a.Butiran_Belanja, a.No_Resit, a.Kod_PTJ, FORMAT (a.Jumlah_Butiran, '0.00') AS Jumlah_Butiran, 
        FORMAT (a.Baki, '0.00') AS Baki, a.Kod_Vot, (SELECT b.Pejabat FROM [DEVMIS\SQL_INS01].dbStaf.dbo.MS_PEJABAT as b
        WHERE b.STATUS = 1 and b.kodpejabat = left(Kod_PTJ,2)) as ButiranPTJ, kw.Butiran as ButiranKw, a.Kod_Operasi, ko.Butiran as ButiranKo,
        a.Kod_Projek, kp.Butiran as ButiranKp, (SELECT Butiran FROM SMKB_Vot WHERE Kod_Vot = a.Kod_Vot) as Butiran
        FROM SMKB_WPR_Dtl AS a
        INNER JOIN SMKB_Kump_Wang AS kw ON a.Kod_Kump_Wang = kw.Kod_Kump_Wang
        INNER JOIN SMKB_Operasi AS ko ON a.Kod_Operasi = ko.Kod_Operasi
        INNER JOIN SMKB_Projek AS kp ON a.Kod_Projek = kp.Kod_Projek
        WHERE a.No_Wpr = @No_Wpr"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@No_Wpr", id))

        Return db.Read(query, param)

    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadOrderRecord_SenaraiLulusPermohonan(category_filter As String, isClicked As Boolean, tkhMula As DateTime, tkhTamat As DateTime) As String
        Dim resp As New ResponseRepository
        If isClicked = False Then
            Return JsonConvert.SerializeObject(New DataTable)
        End If
        dt = GetOrderRecord_SenaraiLulusPermohonan(category_filter, tkhMula, tkhTamat)
        Return JsonConvert.SerializeObject(dt)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetOrderRecord_SenaraiLulusPermohonan(category_filter As String, tkhMula As DateTime, tkhTamat As DateTime) As DataTable

        Dim db As New DBKewConn

        Dim tarikhQuery As String = ""
        Dim param As New List(Of SqlParameter)

        If category_filter = "1" Then 'Harini
            'tarikhQuery = " and a.Tkh_Transaksi = getdate() "
            tarikhQuery = " AND CAST(Tarikh_Mohon AS DATE) = CAST(getdate() AS DATE) "
        ElseIf category_filter = "2" Then 'Semalam
            'tarikhQuery = " and a.Tkh_Transaksi >= DATEADD(day, -1, getdate()) and a.Tkh_Transaksi <= getdate() "
            tarikhQuery = " AND CAST(Tarikh_Mohon AS DATE) = CAST(DATEADD(DAY, -1, GETDATE()) AS DATE) "
        ElseIf category_filter = "3" Then 'seminggu
            'tarikhQuery = " and a.Tkh_Transaksi >= DATEADD(day, -8, getdate()) and a.Tkh_Transaksi <= getdate() "
            tarikhQuery = " AND CAST(Tarikh_Mohon AS DATE) >= CAST(DATEADD(DAY, -7, GETDATE()) AS DATE) "
        ElseIf category_filter = "4" Then '30 hari
            tarikhQuery = " AND Tarikh_Mohon >= DATEADD(month, -1, getdate()) and Tarikh_Mohon <= getdate() "
        ElseIf category_filter = "5" Then '60 hari
            tarikhQuery = " AND Tarikh_Mohon >= DATEADD(month, -2, getdate()) and Tarikh_Mohon <= getdate() "
        ElseIf category_filter = "6" Then 'custom
            tarikhQuery = " AND Tarikh_Mohon >= @tkhMula and Tarikh_Mohon <= @TkhTamat "
            param = New List(Of SqlParameter)
            param.Add(New SqlParameter("@tkhMula", tkhMula))
            param.Add(New SqlParameter("@TkhTamat", tkhTamat))
        End If

        Dim query As String = "SELECT No_Wpr, FORMAT (Tarikh_Mohon, 'dd-MM-yyyy') as Tarikh_Mohon, (SELECT b.Pejabat FROM [DEVMIS\SQL_INS01].dbStaf.dbo.MS_PEJABAT as b
        WHERE KodPejPBU = SMKB_WPR_Maklumat_Hdr.Kod_PTJ) as Kod_PTJ, Jumlah_Belanja, 
        (SELECT Butiran FROM SMKB_Kod_Status_Dok WHERE Kod_Modul = '15' AND Status = 1 AND Kod_Status_Dok = '04') as Kod_Status_Dok
        FROM SMKB_WPR_Maklumat_Hdr WHERE Status_Dok = '04' " & tarikhQuery


        Return db.Read(query, param)

    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadTahunan(ByVal ptj As String, ByVal kumpulan As String) As String
        dt = GetTahunan(ptj, kumpulan)
        Return JsonConvert.SerializeObject(dt)
    End Function

    Private Function GetTahunan(ptj As String, kumpulan As String) As DataTable

        Dim db As New DBKewConn

        Dim query As String = "SELECT MONTH(a.Tarikh_Mohon) AS Bulan, b.Jumlah_Lulus
        FROM SMKB_WPR_Maklumat_Hdr AS a
        INNER JOIN SMKB_WPR_Dtl AS b ON a.No_Wpr = b.No_Wpr
        WHERE a.Kod_PTJ = @ptj AND a.Kod_Kump_Wang = @kump AND YEAR(a.Tarikh_Mohon) = YEAR(GETDATE())  AND Status_Dok BETWEEN '05' AND '06'"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@ptj", ptj))
        param.Add(New SqlParameter("@kump", kumpulan))

        Return db.Read(query, param)

    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadRecord_Laporan(ByVal id As String, ByVal year As String, ByVal month As String) As String
        Dim resp As New ResponseRepository
        dt = GetRecord_Laporan(id, year, month)
        resp.SuccessPayload(dt)
        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    Private Function GetRecord_Laporan(id As String, year As String, month As String) As DataTable

        Dim db As New DBKewConn

        Dim tahunBulan = year + "-" + month
        Dim tarikhQuery = ""
        Dim param As New List(Of SqlParameter)

        tarikhQuery = " AND CONCAT(YEAR(Tarikh_Mohon),'-',MONTH(Tarikh_Mohon)) = @tahunBulan"
        param = New List(Of SqlParameter)
        param.Add(New SqlParameter("@tahunBulan", tahunBulan))

        Dim query As String = "SELECT FORMAT(a.Tarikh_Mohon, 'dd-MM-yyyy') AS Tarikh_Mohon, b.Butiran_Belanja, b.No_Resit, 
        FORMAT (b.Jumlah_Butiran, '0.00') AS Jumlah_Butiran, FORMAT (b.Baki, '0.00') AS Baki, b.Kod_Vot        
        FROM SMKB_WPR_Maklumat_Hdr AS a
        INNER JOIN SMKB_WPR_Dtl AS b ON a.No_Wpr = b.No_Wpr
        WHERE a.No_Wpr LIKE @No_Wpr + '%' AND a.Status_Dok = '06'" & tarikhQuery

        param.Add(New SqlParameter("@No_Wpr", id))

        Return db.Read(query, param)

    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function DeleteOrder(ByVal id As String) As String
        Dim resp As New ResponseRepository

        If DeleteRecord(id) <> "OK" Then
            resp.Failed("Rekod tidak berjaya dihapuskan")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        resp.Success("Rekod telah dihapuskan")
        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function



    <WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SaveOrder(orderM As OrderM) As String
        Dim resp As New ResponseRepository
        resp.Success("Data telah disimpan")
        Dim success As Integer = 0
        Dim JumRekod As Integer = 0

        If orderM Is Nothing Then
            resp.Failed("Tiada simpan")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        If (orderM.Kumpulan = "" Or orderM.Jumlah = "") Then
            resp.Failed("Maklumat header tidak lengkap.")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        If orderM.OrderMDetails Is Nothing Then
            resp.Failed("Maklumat detail tidak lengkap")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        If orderM.OrderMId = "" Then
            orderM.OrderMId = GenerateOrderID(orderM.NoPtj, orderM.Kumpulan)
            If InsertNewOrder(orderM.OrderMId, orderM.Tarikh, orderM.NoPtj, orderM.Kumpulan, orderM.Jumlah) <> "OK" Then
                resp.Failed("Gagal menyimpan order")
                Return JsonConvert.SerializeObject(resp.GetResult())
            End If
        Else
            DeleteOrderHdr(orderM.OrderMId, orderM.Tarikh, orderM.NoPtj, orderM.Kumpulan, orderM.Jumlah)

            If UpdateNewOrder(orderM.OrderMId, orderM.Tarikh, orderM.NoPtj, orderM.Kumpulan, orderM.Jumlah) <> "OK" Then
                If InsertNewOrder(orderM.OrderMId, orderM.Tarikh, orderM.NoPtj, orderM.Kumpulan, orderM.Jumlah) <> "OK" Then
                    resp.Failed("Gagal Menyimpan order 1266")
                    Return JsonConvert.SerializeObject(resp.GetResult())
                    ' Exit Function
                End If
            End If
        End If

        For Each orderMDetail As OrderMDetail In orderM.OrderMDetails

            If orderMDetail.ddlcoa = "" Then
                Continue For
            End If

            JumRekod += 1
            DeleteOrderDtl(orderM.OrderMId)

            If orderMDetail.id = "" Then
                orderMDetail.id = GenerateOrderDetailID(orderM.OrderMId)
                orderMDetail.OrderMId = orderM.OrderMId
                If InsertOrderDetail(orderMDetail, orderM.Kumpulan) = "OK" Then
                    success += 1
                End If
            Else
                If InsertOrderDetail(orderMDetail, orderM.Kumpulan) = "OK" Then
                    success += 1
                End If
            End If
        Next

        If UpdateStatusDokOrder_Mohon(orderM.OrderMId, "Y") <> "OK" Then
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        If success = 0 Then
            resp.Failed("Rekod order detail gagal disimpan")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        If Not success = JumRekod Then
            resp.Success("Rekod order detail berjaya disimpan dengan beberapa rekod tidak disimpan", "00", orderM)
            Return JsonConvert.SerializeObject(resp.GetResult())
        Else
            resp.Success("Rekod berjaya disimpan", "00", orderM)
        End If

        Return JsonConvert.SerializeObject(resp.GetResult())


    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SubmitOrders(orderM As OrderM) As String
        Dim resp As New ResponseRepository
        resp.Success("Data telah dihantar.")
        Dim success As Integer = 0
        Dim JumRekod As Integer = 0

        If orderM Is Nothing Then
            resp.Failed("Tiada simpan")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        If orderM.OrderMId = "" Then
            orderM.OrderMId = GenerateOrderID(orderM.NoPtj, orderM.Kumpulan)
            If InsertNewOrder(orderM.OrderMId, orderM.Tarikh, orderM.NoPtj, orderM.Kumpulan, orderM.Jumlah) Then
                resp.Failed("Gagal menghantar maklumat")
                Return JsonConvert.SerializeObject(resp.GetResult())
            End If
        Else
            DeleteOrderHdr(orderM.OrderMId, orderM.Tarikh, orderM.NoPtj, orderM.Kumpulan, orderM.Jumlah)
            DeleteOrderDtl(orderM.OrderMId)

            If UpdateNewOrder(orderM.OrderMId, orderM.Tarikh, orderM.NoPtj, orderM.Kumpulan, orderM.Jumlah) <> "OK" Then
                If InsertNewOrder(orderM.OrderMId, orderM.Tarikh, orderM.NoPtj, orderM.Kumpulan, orderM.Jumlah) <> "OK" Then
                    resp.Failed("Gagal menyimpan order 1226")
                    Return JsonConvert.SerializeObject(resp.GetResult())
                End If
            End If

        End If

        For Each orderMDetail As OrderMDetail In orderM.OrderMDetails

            If orderMDetail.ddlcoa = "" Then
                Continue For
            End If

            JumRekod += 1
            DeleteOrderDtl(orderM.OrderMId)

            If orderMDetail.id = "" Then
                orderMDetail.id = GenerateOrderDetailID(orderM.OrderMId)
                orderMDetail.OrderMId = orderM.OrderMId
                If InsertOrderDetail(orderMDetail, orderM.Kumpulan) = "OK" Then
                    success += 1
                End If
            Else
                If InsertOrderDetail(orderMDetail, orderM.Kumpulan) = "OK" Then
                    success += 1
                End If
            End If
        Next

        If success = 0 Then
            resp.Failed("Rekod order detail gagal dihantar")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        If Not success = JumRekod Then
            resp.Success("Rekod order detail berjaya disimpan dengan beberapa rekod tidak disimpan", "00", orderM)
            Return JsonConvert.SerializeObject(resp.GetResult())

        Else
            UpdateStatusSubmit(orderM.OrderMId)
            resp.Success("Rekod berjaya dihantar", "00", orderM)

            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    Private Function InsertOrderDetail(orderMDetail As OrderMDetail, kump As String)

        If orderMDetail.resit = "" Then
            orderMDetail.resit = "-"
        End If

        Dim db As New DBKewConn
        Dim query As String = "INSERT INTO SMKB_WPR_Dtl
        VALUES (@No_Wpr, @Bil, @Kod_Kump_Wang, @Kod_PTJ , @Kod_Vot, @Kod_Operasi, @Kod_Projek, 
        @Kod_Akt, @Kod_Kumpulan, @Butiran_Belanja, @No_Resit, @Jumlah_Butiran, @Jumlah_Lulus,  @Status, @Ulasan, 
        @Status_Rayu, @Baki, @Baki_Peruntukan, @Status_Gantian, NULL)"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@No_Wpr", orderMDetail.OrderMId))
        param.Add(New SqlParameter("@Bil", orderMDetail.id))
        param.Add(New SqlParameter("@Kod_Kump_Wang", orderMDetail.kw))
        param.Add(New SqlParameter("@Kod_PTJ", orderMDetail.ptj))
        param.Add(New SqlParameter("@Kod_Vot", orderMDetail.ddlcoa))
        param.Add(New SqlParameter("@Kod_Operasi", orderMDetail.ko))
        param.Add(New SqlParameter("@Kod_Projek", orderMDetail.kp))
        param.Add(New SqlParameter("@Kod_Akt", "00"))
        param.Add(New SqlParameter("@Kod_Kumpulan", kump))
        param.Add(New SqlParameter("@Butiran_Belanja", orderMDetail.butiran))
        param.Add(New SqlParameter("@No_Resit", orderMDetail.resit))
        param.Add(New SqlParameter("@Jumlah_Butiran", orderMDetail.jumlah))
        param.Add(New SqlParameter("@Jumlah_Lulus", "0.00"))
        param.Add(New SqlParameter("@Status", "A"))
        param.Add(New SqlParameter("@Ulasan", "-"))
        param.Add(New SqlParameter("@Status_Rayu", "0"))
        param.Add(New SqlParameter("@Baki", orderMDetail.baki))
        param.Add(New SqlParameter("@Baki_Peruntukan", "0.00"))
        param.Add(New SqlParameter("@Status_Gantian", "0"))


        Return db.Process(query, param)
    End Function

    Private Function UpdateOrderDetail(orderMDetail As OrderMDetail)
        Dim db = New db
        Dim query As String = "UPDATE SMKB_WPR_Dtl SET Butiran_Belanja = @Butiran, Jumlah_Butiran = @Jumlah, Baki = @Baki
        WHERE No_Wprd = @No_Wpr"

        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@Butiran", orderMDetail.butiran))
        param.Add(New SqlParameter("@Jumlah", orderMDetail.jumlah))
        param.Add(New SqlParameter("@Baki", orderMDetail.baki))
        param.Add(New SqlParameter("@No_Wpr", orderMDetail.OrderMId))
    End Function

    Private Function GenerateOrderDetailID(orderid As String) As String
        Dim db = New DBKewConn
        Dim lastID As Integer = 1
        Dim newOrderID As String
        Dim param As New List(Of SqlParameter)

        Dim query As String = "SELECT TOP 1 Bil as id from SMKB_WPR_Dtl WHERE No_Wpr = @orderid"

        param.Add(New SqlParameter("@orderid", orderid))

        dt = db.Read(query, param)

        If dt.Rows.Count > 0 Then
            lastID = CInt(dt.Rows(0).Item("id"))
        End If

        newOrderID = lastID

        Return newOrderID
    End Function

    Private Function UpdateStatusSubmit(kod As String)
        Dim db As New DBKewConn

        Dim statusdok As String = "03" 'hantar
        Dim query As String = "Update SMKB_WPR_Maklumat_Hdr set Status_Dok =  @statusdok where No_Wpr = @No_Wpr"

        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@No_Wpr", kod))
        param.Add(New SqlParameter("@statusdok", statusdok))


        Return db.Process(query, param)
    End Function

    Private Function UpdateStatusLulus(id)
        Dim db As New DBKewConn

        Dim query As String = "UPDATE SMKB_WPR_Maklumat_Hdr SET Status_Dok = '05'
        WHERE No_Wpr = @NoWpr"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@NoWpr", id))

        Return db.Process(query, param)
    End Function

    Private Function UpdateBakiAgihan(baki As Decimal, ptj As String, kumpulan As String)
        Dim db As New DBKewConn
        Dim query As String = "UPDATE SMKB_WPR_Agih Set Baki = @baki WHERE Kod_PTJ = @ptj AND Kod_Kump_Wang = @kump"

        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@baki", baki))
        param.Add(New SqlParameter("@ptj", ptj))
        param.Add(New SqlParameter("@kump", kumpulan))

        Return db.Process(query, param)
    End Function
    Private Function UpdateNoAkhir(kodmodul As String, prefix As String, year As String, ID As String)
        Dim db As New DBKewConn
        Dim query As String = "UPDATE SMKB_No_Akhir set No_Akhir = @No_AKhir where Kod_Modul = @Kod_Modul and Prefix = @Pefix and Tahun = @Tahun"

        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@No_Akhir", ID))
        param.Add(New SqlParameter("@Kod_Modul", kodmodul))
        param.Add(New SqlParameter("@Pefix", prefix))
        param.Add(New SqlParameter("@Tahun", year))

        Return db.Process(query, param)
    End Function

    Private Function InsertNoAkhir(kodModul As String, prefix As String, year As String, ID As String)
        Dim db As New DBKewConn
        Dim query As String = "INSERT INTO SMKB_No_Akhir VALUES(@Kod_Modul ,@Prefix, @No_Akhir, @Tahun, @Butiran, @Kod_PTJ)"

        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@Kod_Modul", kodModul))
        param.Add(New SqlParameter("@Prefix", prefix))
        param.Add(New SqlParameter("@No_Akhir", ID))
        param.Add(New SqlParameter("@Tahun", year))
        param.Add(New SqlParameter("@Butiran", "Panjar Wang Runcit"))
        param.Add(New SqlParameter("@Kod_PTJ", "-"))

        Return db.Process(query, param)
    End Function



    Private Function DeleteOrderHdr(ordermid As String, tarikh As String, noptj As String, kumpulan As String, jumlah As Decimal)
        Dim db As New DBKewConn
        Dim query As String = "DELETE FROM SMKB_WPR_Maklumat_Hdr WHERE No_Wpr = @No_Wpr"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@No_Wpr", ordermid))

        Return db.Process(query, param)
    End Function

    Private Function DeleteOrderDtl(orderid As String)
        Dim db As New DBKewConn
        Dim query As String = "DELETE FROM SMKB_WPR_Dtl WHERE No_Wpr = @No_Wpr"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@No_Wpr", orderid))

        Return db.Process(query, param)
    End Function

    Private Function UpdateNewOrder(ordermid As String, tarikh As String, noptj As String, kumpulan As String, jumlah As Decimal)
        Dim db As New DBKewConn
        Dim query As String = "UPDATE SMKB_WPR_Maklumat_Hdr SET No_PTJ = @No_PTJ, Kod_Kump_Wang = @Kod_Kump_Wang, 
        Jumlah_Belanja = @Jumlah_Belanja WHERE No_Wpr = @No_Wpr"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@No_Wpr", ordermid))
        param.Add(New SqlParameter("@No_PTJ", noptj))
        param.Add(New SqlParameter("@Kod_Kump_Wang", kumpulan))
        param.Add(New SqlParameter("@Jumlah_Belanja", jumlah))

        Return db.Process(query, param)
    End Function

    Private Function UpdateSemak(orderMId As String)
        Dim db As New DBKewConn

        Dim kodstatusSemak As String = "04"

        Dim query As String = "UPDATE SMKB_WPR_Maklumat_Hdr SET Status_Dok = @kodStatus
        WHERE No_Wpr = @No_Wpr"

        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@kodStatus", kodstatusSemak))
        param.Add(New SqlParameter("@No_Wpr", orderMId))

        Return db.Process(query, param)
    End Function

    Private Function UpdateSemakX(orderMId As String)
        Dim db As New DBKewConn

        Dim kodstatusSemak As String = "02"

        Dim query As String = "UPDATE SMKB_WPR_Maklumat_Hdr SET Status_Dok = @kodStatus
        WHERE No_Wpr = @No_Wpr"

        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@kodStatus", kodstatusSemak))
        param.Add(New SqlParameter("@No_Wpr", orderMId))

        Return db.Process(query, param)
    End Function

    Private Function UpdateLulus(orderMId As String)
        Dim db As New DBKewConn

        Dim kodstatusLulus As String = "05"

        Dim query As String = "UPDATE SMKB_WPR_Dtl SET Status = 'L', Jumlah_Lulus = Jumlah_Butiran
        WHERE No_Wpr = @No_Wpr"

        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@kodStatus", kodstatusLulus))
        param.Add(New SqlParameter("@No_Wpr", orderMId))

        Return db.Process(query, param)
    End Function

    Private Function UpdateXLulus(orderMId As String)
        Dim db As New DBKewConn

        Dim query As String = "UPDATE SMKB_WPR_Dtl SET Status_Gantian = '1'
        WHERE No_Wpr = @No_Wpr"

        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@No_Wpr", orderMId))

        Return db.Process(query, param)
    End Function

    Private Function UpdateTerimaan(id As String)
        Dim db As New DBKewConn

        Dim query As String = "UPDATE SMKB_WPR_Maklumat_Hdr SET Status_Dok = '06'
        WHERE No_Wpr = @NoWpr"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@NoWpr", id))

        Return db.Process(query, param)
    End Function

    Private Function UpdatePenerimaan(id As String, tolak As Decimal, jumlah As Decimal)
        Dim db As New DBKewConn

        Dim bersih = jumlah - tolak

        Dim query As String = "UPDATE SMKB_WPR_Maklumat_Hdr SET Jumlah_Tolak = @tolak, Jumlah_Bersih = @bersih
        WHERE No_Wpr = @No_Wpr"

        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@tolak", tolak))
        param.Add(New SqlParameter("@bersih", bersih))
        param.Add(New SqlParameter("@No_Wpr", id))

        Return db.Process(query, param)
    End Function

    Private Function UpdatePengagihan(ptj As String, kumpulan As String, agih As Decimal)
        Dim db As New DBKewConn

        Dim query As String = "UPDATE SMKB_WPR_Agih SET Jumlah_Agih = (Jumlah_Agih + @agih), Tarikh_Agih = GETDATE(), 
        Baki = (Baki + @agih), Tahun = YEAR(GETDATE())
        WHERE Kod_PTJ = @Kod_PTJ AND Kod_Kump_Wang = @Kod_Kump_Wang"

        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@agih", agih))
        param.Add(New SqlParameter("@Kod_PTJ", ptj))
        param.Add(New SqlParameter("@Kod_Kump_Wang", kumpulan))

        Return db.Process(query, param)
    End Function

    Private Function InsertNewOrder(ordermid As String, tarikh As String, noptj As String, kumpulan As String, jumlah As Decimal)
        Dim db As New DBKewConn
        Dim query As String = "INSERT INTO SMKB_WPR_Maklumat_Hdr
        VALUES (@No_Wpr, @Kod_PTJ, @Kod_Kump_Wang, @Tarikh_Mohon, @Jumlah_Belanja, @Jumlah_Tolak, @Jumlah_Bersih, @Status_Dok, 
        @FlagB, @No_Baucar, @FlagJ, @No_Jurnal, @Flag_Akhir_Tahun, @Flag_Resit, @No_Resit, NULL)"

        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@No_Wpr", ordermid))
        param.Add(New SqlParameter("@Kod_PTJ", noptj))
        param.Add(New SqlParameter("@Kod_Kump_Wang", kumpulan))
        param.Add(New SqlParameter("@Tarikh_Mohon", tarikh))
        param.Add(New SqlParameter("@Jumlah_Belanja", jumlah))
        param.Add(New SqlParameter("@Jumlah_Tolak", "0.00"))
        param.Add(New SqlParameter("@Jumlah_Bersih", jumlah))
        param.Add(New SqlParameter("@Status_Dok", "01"))
        param.Add(New SqlParameter("@FlagB", "0"))
        param.Add(New SqlParameter("@No_Baucar", "-"))
        param.Add(New SqlParameter("@FlagJ", "0"))
        param.Add(New SqlParameter("@No_Jurnal", "-"))
        param.Add(New SqlParameter("@Flag_Akhir_Tahun", "0"))
        param.Add(New SqlParameter("@Flag_Resit", "0"))
        param.Add(New SqlParameter("@No_Resit", "-"))

        Return db.Process(query, param)
    End Function

    Private Function UpdateStatusDokOrder_Mohon(ordermid As String, statuslulus As String)
        Dim db As New DBKewConn

        Dim kodstatuslulus As String = "00"

        If statuslulus = "Y" Then
            kodstatuslulus = "02"
        End If

        Dim query As String = "INSERT INTO SMKB_Status_Dok VALUES (@Kod_Modul, @Kod_Status_Dok, @No_Rujukan, @No_Staf, getdate(), getdate(),
        @Status_Transaksi, @Status, @Ulasan, NULL)"

        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@Kod_Modul", "15"))
        param.Add(New SqlParameter("@Kod_Status_Dok", kodstatuslulus))
        param.Add(New SqlParameter("@No_Rujukan", ordermid))
        param.Add(New SqlParameter("@No_Staf", Session("ssusrID")))
        param.Add(New SqlParameter("@Status_Transaksi", 1))
        param.Add(New SqlParameter("@Status", 1))
        param.Add(New SqlParameter("@Ulasan", "-"))

        Return db.Process(query, param)
    End Function

    Private Function GenerateOrderID(noptj As String, nokumpulan As String) As String
        Dim db As New DBKewConn

        Dim year = Date.Now.ToString("yyyy")
        Dim month = Date.Now.Month
        Dim lastID As Integer = 1
        Dim newOrderID As String = ""
        Dim ptj = noptj

        Dim query As String = $"select No_Akhir as id from SMKB_No_Akhir where Kod_Modul = '15' and Prefix = 'WR' and Tahun =@year"
        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@year", year))

        dt = db.Read(query, param)

        If dt.Rows.Count > 0 Then
            lastID = CInt(dt.Rows(0).Item("id")) + 1
            UpdateNoAkhir("15", "WR", year, lastID)
        Else
            InsertNoAkhir("15", "WR", year, lastID)
        End If

        newOrderID = "WR" + ptj.ToString + nokumpulan.ToString + Format(lastID, "000000").ToString + month.ToString("00") + Right(year.ToString(), 2)
        Return newOrderID
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SemakOrderLulus(id As String) As String
        Dim resp As New ResponseRepository
        resp.Success("Semakan berjaya diterima.")

        If id Is Nothing Then
            resp.Failed("Tiada simpan.")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        If UpdateSemak(id) <> "OK" Then
            resp.Failed("Semakan gagal diterima.")
            Return JsonConvert.SerializeObject(resp.GetResult())

        End If

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SemakOrderXLulus(id As String) As String
        Dim resp As New ResponseRepository
        resp.Success("Semakan berjaya diterima.")

        If id Is Nothing Then
            resp.Failed("Tiada simpan.")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        If UpdateSemakX(id) <> "OK" Then
            resp.Failed("Semakan gagal diterima.")
            Return JsonConvert.SerializeObject(resp.GetResult())

        End If

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LulusOrder(ByVal id As String, ByVal ptj As String, ByVal kump As String, ByVal baki As Decimal) As String
        Dim resp As New ResponseRepository
        resp.Success("Permohonan berjaya diluluskan.")

        If id Is Nothing Then
            resp.Failed("Tiada simpan.")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        If UpdateLulus(id) <> "OK" Then
            resp.Failed("Kelulusan gagal diterima.")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        If UpdateStatusLulus(id)<>"OK" Then
            resp.Failed("Gagal mengemaskini status header.")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        If UpdateBakiAgihan(baki, ptj, kump) <> "OK" Then
            resp.Failed("Baki Agihan gagal dikemaskini.")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function XLulusOrder(ByVal id As String) As String
        Dim resp As New ResponseRepository
        resp.Success("Kelulusan diterima. Permohonan TIDAK diluluskan.")

        If id Is Nothing Then
            resp.Failed("Tiada simpan.")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        If UpdateXLulus(id) <> "OK" Then
            resp.Failed("Kelulusan gagal diterima.")
            Return JsonConvert.SerializeObject(resp.GetResult())

        End If

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function savePenerimaan(id As String, tolak As Decimal, jumlah As Decimal) As String
        Dim resp As New ResponseRepository
        resp.Success("Rekod berjaya disimpan.")

        If id Is Nothing Then
            resp.Failed("Tiada simpan.")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        If UpdatePenerimaan(id, tolak, jumlah) <> "OK" Then
            resp.Failed("Rekod gagal disimpan.")
            Return JsonConvert.SerializeObject(resp.GetResult())

        End If

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SaveTerimaan(ByVal id As String) As String
        Dim resp As New ResponseRepository
        resp.Success("Terimaan berjaya.")

        If id Is Nothing Then
            resp.Failed("Terima tidak berjaya. Tiada rekod dipilih.")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        If UpdateTerimaan(id) <> "OK" Then
            resp.Failed("Terimaan gagal.")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SaveOrder_Pengagihan(ptj As String, kumpulan As String, agih As Decimal) As String
        Dim resp As New ResponseRepository
        resp.Success("Rekod berjaya disimpan.")

        If ptj Is Nothing Or kumpulan Is Nothing Then
            resp.Failed("Tiada simpan.")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        If UpdatePengagihan(ptj, kumpulan, agih) <> "OK" Then
            resp.Failed("Rekod gagal disimpan.")
            Return JsonConvert.SerializeObject(resp.GetResult())

        End If

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

End Class