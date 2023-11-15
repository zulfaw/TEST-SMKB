Imports System.ComponentModel
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports Newtonsoft.Json
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
'Imports System.Web.Http
Imports System.Data.SqlClient
Imports System.Collections.Generic

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class LejarPenghutangWS
    Inherits System.Web.Services.WebService
    Dim dt As DataTable

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadOrderRecord_LejarPenghutang() As String
        Dim resp As New ResponseRepository


        dt = GetOrder_SenaraiTransaksiInvois()
        'resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(dt)
    End Function
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetOrder_SenaraiTransaksiInvois() As DataTable
        Dim db = New DBKewConn


        Dim query As String = "SELECT Kod_Pelanggan,CONVERT(varchar,A.Tkh_Mula,103) AS Tarikh,C.Butiran AS Kump_Wang,D.Butiran AS Operasi,E.Butiran AS Projek, F.Pejabat AS Kod_PTJ,G.Butiran AS Kod_Vot,'0.00' AS Debit,B.jumlah AS Kredit
                                FROM SMKB_Bil_Hdr A
                                INNER JOIN SMKB_Bil_Dtl B ON A.No_Bil=B.No_Bil
                                INNER JOIN SMKB_Kump_Wang AS C ON B.Kod_Kump_Wang=C.Kod_Kump_Wang
                                INNER JOIN SMKB_Operasi AS D ON B.Kod_Operasi=D.Kod_Operasi
                                INNER JOIN SMKB_Projek AS E ON B.Kod_Projek=E.Kod_Projek
                                INNER JOIN [DEVMIS\SQL_INS01].dbStaf.dbo.MS_PEJABAT AS F ON F.status = '1' and F.kodpejabat = left(B.Kod_PTJ,2)
                                INNER JOIN SMKB_Vot AS G ON B.Kod_Vot=G.Kod_Vot
                                WHERE Kod_Pelanggan='H00001170323'
                                UNION SELECT Kod_Pelanggan,CONVERT(varchar,B.Tarikh,103) AS Tarikh,C.Butiran AS Kump_Wang,D.Butiran AS Operasi,E.Butiran AS Projek, F.Pejabat AS Kod_PTJ,G.Butiran AS Kod_Vot,B.Amaun_Terima AS Debit,'0.00' AS Kredit
                                FROM SMKB_Terima_Hdr A
                                INNER JOIN SMKB_Terima_Transaksi B ON A.No_Dok=B.No_Dokumen
                                INNER JOIN SMKB_Kump_Wang AS C ON B.Kod_Kump_Wang=C.Kod_Kump_Wang
                                INNER JOIN SMKB_Operasi AS D ON B.Kod_Operasi=D.Kod_Operasi
                                INNER JOIN SMKB_Projek AS E ON B.Kod_Projek=E.Kod_Projek
                                INNER JOIN [DEVMIS\SQL_INS01].dbStaf.dbo.MS_PEJABAT AS F ON F.status = '1' and F.kodpejabat = left(B.Kod_PTJ,2)
                                INNER JOIN SMKB_Vot AS G ON B.Kod_Vot=G.Kod_Vot
                                WHERE A.Kod_Pelanggan='H00001170323' 
                                ORDER BY Tarikh"
        Return db.Read(query)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadOrderRecord_Semasa() As String
        Dim resp As New ResponseRepository


        dt = GetOrder_SenaraiSemasa()
        'resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(dt)
    End Function
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetOrder_SenaraiSemasa() As DataTable
        Dim db = New DBKewConn


        Dim query As String = "SELECT '0.00' AS BAKI_MULA,SUM(Dr_5) AS DEBIT,SUM(CR_5)  AS CREDIT, REPLACE((SUM(Dr_5)-SUM(CR_5)),'-','') AS BAKI
                                FROM SMKB_Lejar_Penghutang
                                WHERE Kod_Penghutang='H00001170323'"
        Return db.Read(query)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadRecordPenghutang() As String
        Dim resp As New ResponseRepository


        dt = GetPenghutang_dtl()
        'resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(dt)
    End Function
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetPenghutang_dtl() As DataTable
        Dim db = New DBKewConn


        Dim query As String = "select Nama_Penghutang,Alamat_1+','+Alamat_2+','+A.Poskod+','+ (SELECT Butiran FROM SMKB_Negeri WHERE Kod_Negeri=A.Kod_Negeri)+','+
                                (SELECT Butiran FROM SMKB_Negara WHERE Kod_Negara=A.Kod_Negara) AS ALAMAT, Emel+'/'+Tel_Bimbit AS HUBUNGI,ID_Penghutang
                                FROM SMKB_Penghutang_Master A
                                WHERE Kod_Penghutang='H00001170323'"
        Return db.Read(query)
    End Function


    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadHdrPenghutang(ByVal id As String) As String
        Dim resp As New ResponseRepository

        dt = GetHdrPenghutang(id)
        resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    Private Function GetHdrPenghutang(id As String) As DataTable
        Dim db = New DBKewConn

        Dim query As String = "SELECT A.No_Bil,A.Kod_Pelanggan,B.Nama_Penghutang,A.Jenis_Urusniaga,C.Butiran, A.Kontrak,A.Tujuan,
                                CASE WHEN Tkh_Mula <> '' THEN FORMAT(Tkh_Mula, 'yyyy-MM-dd') END AS Tkh_Mula,
                                CASE WHEN Tkh_Tamat <> '' THEN FORMAT(Tkh_Tamat, 'yyyy-MM-dd') END AS Tkh_Tamat,Jumlah
                                FROM SMKB_Bil_Hdr A
                                INNER JOIN SMKB_Penghutang_Master B ON A.Kod_Pelanggan=B.Kod_Penghutang
                                INNER JOIN SMKB_Kod_Urusniaga C ON A.Jenis_Urusniaga=C.Kod_Urusniaga
                                WHERE No_Bil = @No_Invois AND A.Status='1'"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@No_Invois", id))

        Return db.Read(query, param)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadRecordLejarPenghutang(ByVal id As String) As String
        Dim resp As New ResponseRepository

        dt = GetTransaksiLejarPenghutang(id)
        resp.SuccessPayload(dt)

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    Private Function GetTransaksiLejarPenghutang(kod As String) As DataTable
        Dim db = New DBKewConn

        Dim query As String = "select Kod_Kump_Wang as colhidkw ,(select Butiran from SMKB_Kump_Wang as kw where a.Kod_Kump_Wang = kw.Kod_Kump_Wang) as colKW,
        Kod_Operasi  as colhidko, (select Butiran from SMKB_Operasi as ko where a.Kod_Operasi = ko.Kod_Operasi) as colKO,
        Kod_Projek as colhidkp,  (select Butiran from SMKB_Projek as kp where a.Kod_Projek = kp.Kod_Projek) as colKp,
        Kod_PTJ as colhidptj , (SELECT b.Pejabat FROM [DEVMIS\SQL_INS01].dbStaf.dbo.MS_PEJABAT as b
        WHERE b.STATUS = 1 and b.kodpejabat = left(Kod_PTJ,2)) as ButiranPTJ ,
        Kod_Vot , (select Butiran from SMKB_Vot as vot where a.Kod_Vot = vot.Kod_Vot) as ButiranVot,
        Perkara ,Kuantiti, Kadar_Harga, Jumlah, Diskaun, Cukai
        from SMKB_Bil_Dtl as a
        where No_Bil = @No_Invois
        and status = 1
        order by No_Item"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@No_Invois", kod))

        Return db.Read(query, param)
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
        Dim query As String = "SELECT CONCAT(a.Kod_Vot, ' - ', vot.Butiran, ', ', a.Kod_Operasi, ' - ', ko.Butiran, ', ', a.Kod_Projek, ', ', a.Kod_Kump_Wang, ' - ', REPLACE(kw.Butiran, 'KUMPULAN WANG', 'KW'), ', ', a.Kod_PTJ) AS text,
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

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SaveOrders(id As String) As String
        Dim resp As New ResponseRepository
        resp.Success("Data telah disimpan")
        Dim success As Integer = 0
        Dim JumRekod As Integer = 0
        If id Is Nothing Then
            resp.Failed("Tidak disimpan")
            Return JsonConvert.SerializeObject(resp.GetResult())
        End If

        If id = "" Then
            If Save_Lulus(id) <> "OK" Then
                resp.Failed("Gagal Menyimpan order")
                Exit Function
            Else
                If update_lejar(id) <> "OK" Then
                    resp.Success("Rekod berjaya disimpan", "00", id)
                    Exit Function
                Else

                End If

            End If
        End If

        If success = 0 Then
            resp.Failed("Rekod order detail gagal disimpan")
        End If

        'If Not success = JumRekod Then
        '    resp.Success("Rekod order detail berjaya disimpan dengan beberapa rekod tidak disimpan", "00", id)
        'Else
        '    resp.Success("Rekod berjaya disimpan", "00", id)
        'End If

        Return JsonConvert.SerializeObject(resp.GetResult())
    End Function

    Private Function Save_Lulus(id As String)
        Dim db As New DBKewConn
        Dim query As String = "UPDATE SMKB_Bil_Hdr
                                SET Flag_Lulus='1', Kod_Status_Dok='01'
                                WHERE No_Bil=@id"
        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@id", id))

        Return db.Process(query, param)
    End Function

    Private Function update_lejar(id As String)
        Dim db As New DBKewConn
        Dim query1 As String = "SELECT A.Kod_Pelanggan,B.Kod_Vot,B.Kod_PTJ,B.Kod_Projek,B.Kod_Operasi,B.Kod_Kump_Wang, SUM(B.Jumlah), MONTH(A.Tkh_Mula) AS BULAN, YEAR(A.Tkh_Mula) AS TAHUN
                                FROM SMKB_Bil_Hdr A
                                INNER JOIN SMKB_Bil_Dtl B ON A.No_Bil=B.No_Bil
                                WHERE A.No_Bil=@id
                                GROUP BY A.Kod_Pelanggan,B.Kod_Vot,B.Kod_PTJ,B.Kod_Projek,B.Kod_Operasi,B.Kod_Kump_Wang, B.Jumlah, A.Tkh_Mula "
        Dim param1 As New List(Of SqlParameter)
        param1.Add(New SqlParameter("@id", id))
        Return db.Process(query1, param1)

        Dim query As String = "UPDATE SMKB_Bil_Hdr
                                SET Flag_Lulus='1', Kod_Status_Dok='01'
                                WHERE No_Bil=@id"
        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@id", id))
        Return db.Process(query, param)

    End Function

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetPenghutangList(ByVal q As String) As String

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
End Class