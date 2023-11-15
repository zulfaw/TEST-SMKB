Imports System.ComponentModel
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports Newtonsoft.Json
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
'Imports System.Web.Http
Imports System.Data.SqlClient
Imports System.Net.Http.Headers
Imports Microsoft.Ajax.Utilities
Imports System.Threading
' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class Baucar_WS
    Inherits System.Web.Services.WebService

    'test api
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=True)>
    Public Function ping() As String

        Return "helo"
    End Function


    'senarai invois yg belum ada atau masih draf baucar
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function getInvoisDrafBaucar(ByVal DateStart As String, ByVal DateEnd As String) As String
        Dim req As Response = fetchInvoisForDrafBaucar(DateStart, DateEnd)
        Return JsonConvert.SerializeObject(req)
    End Function

    'invois listing baucar telah kemaskini untuk kelulusan 1
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function getInvoisBaucarKemaskini(ByVal DateStart As String, ByVal DateEnd As String) As String
        Dim dok As New List(Of String) From {"09"}
        Dim req As Response = fetchInvoisSumByBaucarDok(dok, DateStart, DateEnd)
        Return JsonConvert.SerializeObject(req)
    End Function
    'invois listing baucar telah LULUS 1 untuk kelulusan 2
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function getInvoisBaucarLulus1(ByVal DateStart As String, ByVal DateEnd As String) As String
        Dim dok As New List(Of String) From {"11"}
        Dim req As Response = fetchInvoisSumByBaucarDok(dok, DateStart, DateEnd)
        Return JsonConvert.SerializeObject(req)
    End Function


    'senarai details
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function getBaucarDtl(No_Baucar As String) As String
        Dim sqlcmd As New SqlCommand
        Dim dt As New DataTable
        Try
            Using sqlconn As New SqlConnection(dbSMKB.strCon)
                sqlconn.Open()
                sqlcmd.Connection = sqlconn
                sqlcmd.CommandText = "  
                 
                  Select iv.ID_Rujukan,iv.Kod_Vot,iv.Kod_PTJ,iv.Kod_Kump_Wang,iv.Kod_Operasi,iv.Kod_Projek,iv.Kod_Pemiutang, iv.Kadar_Harga,iv.Cukai,iv.Kuantiti_Sebenar,iv.Diskaun,
                  dtl.No_Baucar,dtl.No_Item,Jumlah_Bayar,dtl.Kod_Cukai,dtl.Cara_Bayar, pm.Kod_Pemiutang, pm.Nama_Pemiutang
                  FROM SMKB_Pembayaran_Baucar_Dtl dtl 
                   JOIN SMKB_Pembayaran_Baucar_Hdr hdr ON hdr.No_Baucar = dtl.No_Baucar 
                   JOIN SMKB_Pembayaran_Invois_Dtl iv ON iv.ID_Rujukan = hdr.ID_Rujukan AND iv.No_Item = dtl.No_Item
                   JOIN SMKB_Pemiutang_Master pm ON pm.Kod_Pemiutang = iv.Kod_Pemiutang

                WHERE hdr.No_Baucar =@No_Baucar"
                sqlcmd.Parameters.Add(New SqlParameter("@No_Baucar", No_Baucar))
                dt.Load(sqlcmd.ExecuteReader())
            End Using
        Catch ex As Exception

            Dim strex As String = ex.Message
        End Try
        Return JsonConvert.SerializeObject(dt)
    End Function

    'baucar details from invois ID Rujukan
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=True, ResponseFormat:=ResponseFormat.Json)>
    Public Function getBaucarByIDRujukan(ID_Rujukan As String) As String
        Dim response As New Response
        Try
            Using sqlconn As New SqlConnection(dbSMKB.strCon)
                Dim cmd As SqlCommand
                cmd = BaucarHdr.findBaucarByID_Rujukan(ID_Rujukan)
                cmd.Connection = sqlconn
                sqlconn.Open()

                Dim reader As SqlDataReader = cmd.ExecuteReader()


                If reader.HasRows Then
                    Dim dt As New DataTable
                    dt.Load(reader)
                    response.Code = 200
                    response.Payload = dt
                Else
                    ' no record
                    'insert baru
                    reader.Close()
                    Dim noBaucar As String = generateNoBaucar()
                    Dim newBaucarHdr As New BaucarHdr
                    newBaucarHdr.No_Baucar = noBaucar
                    newBaucarHdr.ID_Rujukan = ID_Rujukan
                    newBaucarHdr.Status_Dok = "08"
                    newBaucarHdr.Tarikh = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    Dim cmdInsert As SqlCommand = newBaucarHdr.insertCommand()
                    cmdInsert.Connection = sqlconn

                    If Not cmdInsert.ExecuteNonQuery() = 0 Then

                        Dim cmdDtl As New SqlCommand
                        cmdDtl.Connection = sqlconn
                        cmdDtl.CommandText = "INSERT INTO SMKB_Pembayaran_Baucar_Dtl (No_Baucar , No_Item)
                                                    SELECT @No_Baucar,No_Item
                                                    FROM SMKB_Pembayaran_Invois_Dtl WHERE ID_Rujukan = @ID_Rujukan"
                        cmdDtl.Parameters.Add(New SqlParameter("@No_Baucar", noBaucar))
                        cmdDtl.Parameters.Add(New SqlParameter("@ID_Rujukan", ID_Rujukan))

                        cmdDtl.ExecuteNonQuery()


                        response.Code = 200
                        response.Payload = newBaucarHdr




                    Else
                        response.Code = 500
                        response.Message = "New Baucar registration failed"
                    End If
                End If
            End Using
        Catch ex As Exception
            response.Message = ex.Message
            response.Code = 500
        End Try

        Return JsonConvert.SerializeObject(response)

    End Function




    'list of banks and vot
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function getSenaraiBank(ByVal q As String) As String
        Dim response As New Response
        Try
            Dim dt As DataTable = getBankList(q)
            response.Payload = dt
            response.Code = 200
        Catch ex As Exception
            response.Code = 500
            response.Message = ex.Message
        End Try
        Return JsonConvert.SerializeObject(response)
    End Function

    'list cara bayar
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function getSenaraiCaraBayar(ByVal q As String) As String
        Dim response As New Response
        Try
            Dim dt As DataTable = getCaraBayar(q)
            response.Payload = dt
            response.Code = 200
        Catch ex As Exception
            response.Code = 500
            response.Message = ex.Message
        End Try
        Return JsonConvert.SerializeObject(response)
    End Function

    'list kod cukai
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function getSenaraiKodCukai(ByVal q As String) As String
        Dim response As New Response
        Try
            Dim dt As DataTable = getKodCukai(q)
            response.Payload = dt
            response.Code = 200
        Catch ex As Exception
            response.Code = 500
            response.Message = ex.Message
        End Try
        Return JsonConvert.SerializeObject(response)
    End Function


    'save baucar 
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SaveBaucar(baucar As BaucarHdr) As String
        Dim response As New Response
        response.Code = 200
        response.Message = "Berjaya Di Simpan"
        'log sni id rujukan baucar atau invois? assumed baucar 
        baucar.Status_Dok = "08"
        Dim res As Response = saveFullBaucar(baucar)
        If res.Code.Equals("200") Then
            logBaucarDok(baucar.No_Baucar, baucar.Status_Dok)
        End If
        Return JsonConvert.SerializeObject(res)
    End Function


    'save and hantar baucar 
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SubmitBaucar(baucar As BaucarHdr) As String
        Dim response As New Response
        response.Code = 200
        response.Message = "Berjaya Di Simpan"
        'log sni id rujukan baucar atau invois? assumed baucar 
        baucar.Status_Dok = "09"
        Dim res As Response = saveFullBaucar(baucar)
        If res.Code.Equals("200") Then
            logBaucarDok(baucar.No_Baucar, baucar.Status_Dok)
        End If
        Return JsonConvert.SerializeObject(res)
    End Function
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function lulusBaucar1(baucar As BaucarHdr) As String
        baucar.Status_Dok = "11"
        Dim res As New Response
        res.Code = 200
        res = SaveBaucarHdr(baucar)
        If res.Code.Equals("200") Then
            logBaucarDok(baucar.No_Baucar, baucar.Status_Dok)
        End If
        Return JsonConvert.SerializeObject(res)
    End Function

    'lulus baycar
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Async Function lulusBaucar2(baucar As BaucarHdr, votInvois As String) As Tasks.Task(Of String)
        Dim res As New Response
        res.Code = 200
        res.Message = "Berjaya di luluskan"


        If String.IsNullOrEmpty(baucar.Kod_Bank) Then
            res.Code = 400
            res.Message = "Invalid Vot"
            Return JsonConvert.SerializeObject(res)
        End If

        If String.IsNullOrEmpty(votInvois) Then
            res.Code = 400
            res.Message = "Invalid Vot"
            Return JsonConvert.SerializeObject(res)
        End If
        baucar.Status_Dok = "12"
        'log sni id rujukan baucar atau invois? assumed baucar 
        res = SaveBaucarHdr(baucar)
        If res.Code.Equals("200") Then
            logBaucarDok(baucar.No_Baucar, baucar.Status_Dok)
        End If


        Dim userId As String = Session("ssusrID")

        For Each dtl As BaucarDtl In baucar.details
            Try
                If dtl.Kod_Pemiutang IsNot Nothing And Not dtl.Kod_Pemiutang.Equals("") Then
                    res = Await creditIntoGeneralLedger(userId, dtl, baucar.Tarikh, baucar.Kod_Bank)
                    If res.Code.Equals("200") Then
                        res = Await debitIntoAccPemiutang(userId, dtl, baucar.Tarikh, votInvois)
                    End If

                End If

            Catch ex As Exception
                res.Message = ex.Message
            End Try
        Next

        Return JsonConvert.SerializeObject(res)

    End Function



    Private Function saveFullBaucar(baucar As BaucarHdr) As Response
        Dim res As Response = SaveBaucarHdr(baucar)
        Dim resdtl As Response = SaveBaucarDtl(baucar.details)
        If res.Code <> "200" Then
            Return res
        End If
        Return resdtl
    End Function



    Private Function SaveBaucarHdr(baucar As BaucarHdr) As Response
        Dim response As New Response
        response.Code = 200
        response.Message = "Berjaya Di Simpan"
        Try
            Using sqlConn As New SqlConnection(dbSMKB.strCon)
                sqlConn.Open()
                Dim sqlCmd As SqlCommand
                sqlCmd = baucar.updateCommand()
                sqlCmd.Connection = sqlConn
                sqlCmd.ExecuteNonQuery()

            End Using
        Catch ex As Exception
            response.Code = 500
            response.Message = ex.Message
        End Try
        Return response

    End Function
    'save baucar dtl 
    Private Function SaveBaucarDtl(senaraiDtl As List(Of BaucarDtl)) As Response
        Dim response As New Response
        response.Code = 200
        response.Message = "Berjaya di simpan"
        If senaraiDtl IsNot Nothing AndAlso senaraiDtl.Count > 0 Then
            Try
                Using sqlConn As New SqlConnection(dbSMKB.strCon)
                    sqlConn.Open()
                    Dim sqlcmd As SqlCommand
                    For Each dtl As BaucarDtl In senaraiDtl
                        sqlcmd = dtl.updateCommand()
                        sqlcmd.Connection = sqlConn

                        sqlcmd.ExecuteNonQuery()
                    Next
                End Using
            Catch ex As Exception
                response.Code = 500
                response.Message = ex.Message
            End Try
        End If
        Return response
    End Function



    Private Function getBankList(Optional key As String = "") As DataTable
        Dim dt As New DataTable
        Using sqlconn As New SqlConnection(dbSMKB.strCon)
            sqlconn.Open()

            Dim cmd As New SqlCommand
            cmd.Connection = sqlconn

            Dim sql As String = "SELECT Kod_Bank, Nama_Bank FROM SMKB_Bank_Master "
            If Not key.IsNullOrWhiteSpace Then
                sql += " WHERE Kod_Bank LIKE @key OR Nama_Bank LIKE @key"
                cmd.Parameters.Add(New SqlParameter("@key", "%" & key & "%"))
            End If
            cmd.CommandText = sql
            dt.Load(cmd.ExecuteReader())
        End Using
        Return dt
    End Function

    Private Function getKodCukai(Optional key As String = "") As DataTable
        If key.IsNullOrWhiteSpace Then
            Return findInLookupDetails("KC01")
        Else
            Return findInLookupDetails("KC01", key)
        End If
    End Function
    Private Function getCaraBayar(Optional key As String = "") As DataTable
        If key.IsNullOrWhiteSpace Then
            Return findInLookupDetails("AP03")
        Else
            Return findInLookupDetails("AP03", key)
        End If
    End Function


    Private Sub logBaucarDok(No_Rujukan As String, Status_Dok As String)
        SharedModulePembayaran.logDOK("03", No_Rujukan, Status_Dok, "-")
    End Sub
    Private Sub logBaucarDok(No_Rujukan As String, Status_Dok As String, Ulasan As String)
        SharedModulePembayaran.logDOK("03", No_Rujukan, Status_Dok, Ulasan)

    End Sub

    Private Function generateNoBaucar() As String
        Return SharedModulePembayaran.generateRunningNumberNoPTJ("03", "BK", "Baucar")
    End Function

    Private Async Function creditIntoAccPemiutang(userId As String, dtl As BaucarDtl, tarikh As String, vot As String) As Tasks.Task(Of Response)
        Return Await SharedModulePembayaran.sendDataIntoLejar(userId, "AP", dtl, True, tarikh, vot)
    End Function
    Private Async Function debitIntoAccPemiutang(userId As String, dtl As BaucarDtl, tarikh As String, vot As String) As Tasks.Task(Of Response)
        Return Await SharedModulePembayaran.sendDataIntoLejar(userId, "AP", dtl, False, tarikh, vot) 'betul ke ap 
    End Function
    Private Async Function creditIntoGeneralLedger(userId As String, dtl As BaucarDtl, tarikh As String, vot As String) As Tasks.Task(Of Response)
        Return Await SharedModulePembayaran.sendDataIntoLejar(userId, "GL", dtl, True, tarikh, vot)
    End Function
    Private Async Function debitIntoGeneralLedger(userId As String, dtl As BaucarDtl, tarikh As String, vot As String) As Tasks.Task(Of Response)
        Return Await SharedModulePembayaran.sendDataIntoLejar(userId, "GL", dtl, False, tarikh, vot)
    End Function

    'query invois hdr with status dok of baucar
    Private Function fetchInvoisSumByBaucarDok(Status_Dok As List(Of String), DateStart As String, DateEnd As String) As Response
        Dim sqlcmd As New SqlCommand
        Dim dt As New DataTable
        Dim res As New Response
        res.Code = 200
        Try
            Using sqlconn As New SqlConnection(dbSMKB.strCon)
                sqlconn.Open()
                sqlcmd.Connection = sqlconn

                Dim statusList As String = String.Join(",", Status_Dok.Select(Function(s) $"'{s}'"))
                Dim sqlText As String = "Select hdr.ID_Rujukan, hdr.No_Invois, hdr.Tarikh_Daftar, hdr.Tarikh_Invois, hdr.Tarikh_Terima_Invois, hdr.Tujuan,
                jb.Kod + ' - ' + jb.Butiran as Jenis_Bayar,
                ji.Kod + ' - ' + ji.Butiran as Jenis_Invois,
                dtl.Jumlah
                From SMKB_Pembayaran_Invois_Hdr hdr
                JOIN 
                SMKB_Pembayaran_Jenis_Bayar jb ON hdr.Jenis_Bayar = jb.Kod
                JOIN
                SMKB_Pembayaran_Jenis_Invois ji ON hdr.Jenis_Invois = ji.Kod
                JOIN
                SMKB_Pembayaran_Baucar_Hdr bhdr ON hdr.ID_Rujukan = bhdr.ID_Rujukan
                
                LEFT JOIN
                (SELECT
                    ID_Rujukan,
                    SUM(
                        COALESCE(Kadar_Harga, 0) * 
                        COALESCE(Kuantiti_Sebenar, 0)
						-(COALESCE(Kadar_Harga, 0) *  COALESCE(Kuantiti_Sebenar, 0) * COALESCE(Diskaun, 0) / 100) 
                        +(COALESCE(Kadar_Harga, 0) *  COALESCE(Kuantiti_Sebenar, 0) * COALESCE(Cukai, 0) / 100)
						 
                    ) AS Jumlah
                FROM
                    SMKB_Pembayaran_Invois_Dtl
                GROUP BY
                    ID_Rujukan
                ) dtl ON hdr.ID_Rujukan = dtl.ID_Rujukan
                WHERE bhdr.Status_Dok IN (" & statusList & ") "



                If DateStart IsNot Nothing And DateStart <> "" Then
                    sqlText += " AND hdr.Tarikh_Daftar >= @DateStart "
                    sqlcmd.Parameters.Add(New SqlParameter("@DateStart", DateStart))
                End If

                If DateEnd IsNot Nothing And DateEnd <> "" Then
                    sqlText += " AND hdr.Tarikh_Daftar <= @DateEnd "
                    sqlcmd.Parameters.Add(New SqlParameter("@DateEnd", DateEnd))
                End If

                sqlcmd.CommandText = sqlText

                dt.Load(sqlcmd.ExecuteReader())
                res.Payload = dt
            End Using
        Catch ex As Exception
            Dim strex As String = ex.Message
            res.Code = 500
            res.Message = ex.Message
        End Try
        Return res

    End Function

    'query invois hdr with status dok of baucar
    Private Function fetchInvoisForDrafBaucar(DateStart As String, DateEnd As String) As Response
        Dim sqlcmd As New SqlCommand
        Dim dt As New DataTable
        Dim res As New Response
        res.Code = 200
        Try
            Using sqlconn As New SqlConnection(dbSMKB.strCon)
                sqlconn.Open()
                sqlcmd.Connection = sqlconn

                Dim sqlText As String = "Select hdr.ID_Rujukan, hdr.No_Invois, hdr.Tarikh_Daftar, hdr.Tarikh_Invois, hdr.Tarikh_Terima_Invois, hdr.Tujuan,
                jb.Kod + ' - ' + jb.Butiran as Jenis_Bayar,
                ji.Kod + ' - ' + ji.Butiran as Jenis_Invois,
                dtl.Jumlah
                From SMKB_Pembayaran_Invois_Hdr hdr
                JOIN 
                SMKB_Pembayaran_Jenis_Bayar jb ON hdr.Jenis_Bayar = jb.Kod
                JOIN
                SMKB_Pembayaran_Jenis_Invois ji ON hdr.Jenis_Invois = ji.Kod
                LEFT JOIN
                SMKB_Pembayaran_Baucar_Hdr bhdr ON hdr.ID_Rujukan = bhdr.ID_Rujukan
                
                LEFT JOIN
                (SELECT
                    ID_Rujukan,
                    SUM(
                        COALESCE(Kadar_Harga, 0) * 
                        COALESCE(Kuantiti_Sebenar, 0)
						-(COALESCE(Kadar_Harga, 0) *  COALESCE(Kuantiti_Sebenar, 0) * COALESCE(Diskaun, 0) / 100) 
                        +(COALESCE(Kadar_Harga, 0) *  COALESCE(Kuantiti_Sebenar, 0) * COALESCE(Cukai, 0) / 100)
						 
                    ) AS Jumlah
                FROM
                    SMKB_Pembayaran_Invois_Dtl
                GROUP BY
                    ID_Rujukan
                ) dtl ON hdr.ID_Rujukan = dtl.ID_Rujukan
                WHERE (bhdr.Status_Dok IS NULL OR bhdr.Status_Dok='08')  "



                If DateStart IsNot Nothing And DateStart <> "" Then
                    sqlText += " AND hdr.Tarikh_Daftar >= @DateStart "
                    sqlcmd.Parameters.Add(New SqlParameter("@DateStart", DateStart))
                End If

                If DateEnd IsNot Nothing And DateEnd <> "" Then
                    sqlText += " AND hdr.Tarikh_Daftar <= @DateEnd "
                    sqlcmd.Parameters.Add(New SqlParameter("@DateEnd", DateEnd))
                End If

                sqlcmd.CommandText = sqlText

                dt.Load(sqlcmd.ExecuteReader())
                res.Payload = dt
            End Using
        Catch ex As Exception
            Dim strex As String = ex.Message
            res.Code = 500
            res.Message = ex.Message
        End Try
        Return res

    End Function

End Class

<Serializable>
Public Class BaucarHdr
    Public Property No_Baucar As String

    Public Property ID_Rujukan As String
    Public Property Tarikh As String
    Public Property Jenis_Invois As String
    Public Property Jenis_Baucar As String
    Public Property Kod_Bank As String
    Public Property Butiran As String
    Public Property Jumlah As Double?
    Public Property Status_Dok As String
    Public Property Cetak As String
    Public Property Status As String

    Public Property details As List(Of BaucarDtl)

    'insert new record
    Public Function insertCommand() As SqlCommand
        If No_Baucar Is Nothing Then
            Throw New Exception("No baucar tidak sah")
        End If

        Dim cmd As New SqlCommand
        Dim sql As String
        Dim values As String
        sql = "INSERT INTO SMKB_Pembayaran_Baucar_Hdr (No_Baucar"
        values = "(@No_Baucar"
        cmd.Parameters.Add(New SqlParameter("@No_Baucar", No_Baucar))

        If ID_Rujukan IsNot Nothing Then
            sql += ",ID_Rujukan"
            values += ",@ID_Rujukan"
            cmd.Parameters.Add(New SqlParameter("@ID_Rujukan", ID_Rujukan))
        End If

        If Tarikh IsNot Nothing Then
            sql += ",Tarikh"
            values += ",@Tarikh"
            cmd.Parameters.Add(New SqlParameter("@Tarikh", Tarikh))
        End If

        If Jenis_Invois IsNot Nothing Then
            sql += ",Jenis_Invois"
            values += ",@Jenis_Invois"
            cmd.Parameters.Add(New SqlParameter("@Jenis_Invois", Jenis_Invois))
        End If

        If Jenis_Baucar IsNot Nothing Then
            sql += ",Jenis_Baucar"
            values += ",@Jenis_Baucar"
            cmd.Parameters.Add(New SqlParameter("@Jenis_Baucar", Jenis_Baucar))
        End If

        If Kod_Bank IsNot Nothing Then
            sql += ",Kod_Bank"
            values += ",@Kod_Bank"
            cmd.Parameters.Add(New SqlParameter("@Kod_Bank", Kod_Bank))
        End If

        If Butiran IsNot Nothing Then
            sql += ",Butiran"
            values += ",@Butiran"
            cmd.Parameters.Add(New SqlParameter("@Butiran", Butiran))
        End If

        If Jumlah.HasValue Then
            sql += ",Jumlah"
            values += ",@Jumlah"
            cmd.Parameters.Add(New SqlParameter("@Jumlah", Jumlah))
        End If

        If Status_Dok IsNot Nothing Then
            sql += ",Status_Dok"
            values += ",@Status_Dok"
            cmd.Parameters.Add(New SqlParameter("@Status_Dok", Status_Dok))
        End If

        If Cetak IsNot Nothing Then
            sql += ",Cetak"
            values += ",@Cetak"
            cmd.Parameters.Add(New SqlParameter("@Cetak", Cetak))
        End If

        If Status IsNot Nothing Then
            sql += ",Status"
            values += ",@Status"
            cmd.Parameters.Add(New SqlParameter("@Status", Status))
        End If

        cmd.CommandText = sql + ") VALUES " + values + ")"

        Return cmd

    End Function

    'update changes
    Public Function updateCommand() As SqlCommand
        If No_Baucar Is Nothing Then
            Throw New Exception("No baucar tidak sah")
        End If

        Dim cmd As New SqlCommand
        Dim sql As String
        Dim values As String = ""
        sql = "UPDATE SMKB_Pembayaran_Baucar_Hdr SET "

        If ID_Rujukan IsNot Nothing Then
            values += "ID_Rujukan = @ID_Rujukan,"
            cmd.Parameters.Add(New SqlParameter("@ID_Rujukan", ID_Rujukan))
        End If

        If Tarikh IsNot Nothing Then
            values += "Tarikh = @Tarikh,"
            cmd.Parameters.Add(New SqlParameter("@Tarikh", Tarikh))
        End If

        If Jenis_Invois IsNot Nothing Then
            values += "Jenis_Invois = @Jenis_Invois,"
            cmd.Parameters.Add(New SqlParameter("@Jenis_Invois", Jenis_Invois))
        End If

        If Jenis_Baucar IsNot Nothing Then
            values += "Jenis_Baucar = @Jenis_Baucar,"
            cmd.Parameters.Add(New SqlParameter("@Jenis_Baucar", Jenis_Baucar))
        End If

        If Kod_Bank IsNot Nothing Then
            values += "Kod_Bank = @Kod_Bank,"
            cmd.Parameters.Add(New SqlParameter("@Kod_Bank", Kod_Bank))
        End If

        If Butiran IsNot Nothing Then
            values += "Butiran = @Butiran,"
            cmd.Parameters.Add(New SqlParameter("@Butiran", Butiran))
        End If

        If Jumlah.HasValue Then
            values += "Jumlah = @Jumlah,"
            cmd.Parameters.Add(New SqlParameter("@Jumlah", Jumlah))
        End If

        If Status_Dok IsNot Nothing Then
            values += "Status_Dok = @Status_Dok,"
            cmd.Parameters.Add(New SqlParameter("@Status_Dok", Status_Dok))
        End If

        If Cetak IsNot Nothing Then
            values += "Cetak = @Cetak,"
            cmd.Parameters.Add(New SqlParameter("@Cetak", Cetak))
        End If

        If Status IsNot Nothing Then
            values += "Status = @Status,"
            cmd.Parameters.Add(New SqlParameter("@Status", Status))
        End If

        If Not String.IsNullOrEmpty(values) Then
            values = values.Substring(0, values.Length - 1) 'remove extra ,

        End If

        cmd.CommandText = sql + values + " WHERE No_Baucar = @No_Baucar"
        cmd.Parameters.Add(New SqlParameter("@No_Baucar", No_Baucar))

        Return cmd

    End Function

    'find baucar using invois id
    Public Shared Function findBaucarByID_Rujukan(ID_Rujukan As String) As SqlCommand
        Dim cmd As New SqlCommand
        cmd.CommandText = "SELECT * FROM SMKB_Pembayaran_Baucar_Hdr WHERE ID_Rujukan=@ID_Rujukan"
        cmd.Parameters.Add(New SqlParameter("@ID_Rujukan", ID_Rujukan))
        Return cmd
    End Function
End Class

<Serializable>
Public Class BaucarDtl
    Inherits LedgerItem
    Public Property No_Baucar As String
    Public Property No_Item As String
    Public Property Jumlah_Bayar As Double?
    Public Property Kod_Cukai As String
    Public Property Cara_Bayar As String


    Public Overrides Function getSum() As Double
        Return Jumlah_Bayar
    End Function
    Public Function updateCommand() As SqlCommand
        If No_Baucar Is Nothing Then
            Throw New Exception("No baucar tidak sah")
        End If
        If No_Item Is Nothing Then
            Throw New Exception("No Item tidak sah")
        End If

        Dim cmd As New SqlCommand
        Dim sql As String
        Dim values As String = ""
        sql = "UPDATE SMKB_Pembayaran_Baucar_Dtl SET No_Baucar = No_Baucar"

        If Jumlah_Bayar.HasValue Then
            values += "Jumlah_Bayar = @Jumlah_Bayar,"
            cmd.Parameters.Add(New SqlParameter("@Jumlah_Bayar", Jumlah_Bayar))
        End If

        If Kod_Cukai IsNot Nothing Then
            values += "Kod_Cukai = @Kod_Cukai,"
            cmd.Parameters.Add(New SqlParameter("@Kod_Cukai", Kod_Cukai))
        End If

        If Cara_Bayar IsNot Nothing Then
            values += "Cara_Bayar = @Cara_Bayar,"
            cmd.Parameters.Add(New SqlParameter("@Cara_Bayar", Cara_Bayar))
        End If

        If Not String.IsNullOrEmpty(values) Then
            values = values.Substring(0, values.Length - 1) 'remove extra ,
            sql += ","

        End If

        cmd.CommandText = sql + values + " WHERE No_Baucar = @No_Baucar AND No_Item= @No_Item"
        cmd.Parameters.Add(New SqlParameter("@No_Baucar", No_Baucar))
        cmd.Parameters.Add(New SqlParameter("@No_Item", No_Item))

        Return cmd
    End Function


End Class



