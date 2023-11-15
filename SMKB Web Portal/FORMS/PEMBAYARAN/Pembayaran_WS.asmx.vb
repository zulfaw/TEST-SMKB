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
Imports System.Globalization
'Imports System.Globalization
Imports System.Threading
' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class SharedPembayaran_WS
    Inherits System.Web.Services.WebService

    'test api
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=True)>
    Public Function ping() As String
        '  logInvoisDok("test", "testlog")
        'test
        Return JsonConvert.SerializeObject(Session("ssusrID"))
    End Function


End Class

Module SharedModulePembayaran

    '@Param KodLejar eg: GL General Ledger, AP
    Public Async Function sendDataIntoLejar(userId As String, Ledger As String, item As LedgerItem, isCredit As Boolean, tarikh As String, Optional vot As String = "") As Tasks.Task(Of Response)
        Dim res As New Response
        res.Code = 200

        Try

            Dim ticket As New TokenResponseModel()
            Dim servicex As New ValuesService()
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("ms-MY")
            Dim parsedDate As Date = CDate(tarikh).ToString("yyyy-MM-dd")
            Dim vBulan As String = parsedDate.Month
            Dim vTahun As String = parsedDate.Year
            Dim dbcr As String = "DR"
            Dim kodvot As String = ""
            If Not String.IsNullOrEmpty(vot) Then
                kodvot = vot
            Else
                kodvot = item.Kod_Vot
            End If

            If isCredit Then
                dbcr = "CR"
            End If

            Dim values As String = ""
            If Ledger.Equals("AP") Then
                values = Await servicex.SendDataLejar(ticket.GetTicket("smkb", userId),
                                            "AP", item.Kod_Pemiutang, item.Kod_Kump_Wang, item.Kod_PTJ,
                                            kodvot, item.Kod_Operasi, item.Kod_Projek, item.getSum(), dbcr, vBulan, vTahun)
            ElseIf Ledger.Equals("GL") Then
                values = Await servicex.SendDataLejar(ticket.GetTicket("smkb", userId),
                                            "GL", "UTeM", item.Kod_Kump_Wang, item.Kod_PTJ,
                                            kodvot, item.Kod_Operasi, item.Kod_Projek, item.getSum(), dbcr, vBulan, vTahun)

            End If


            If values.Contains("ok") Then
                res.Code = 200
            Else
                res.Code = 500
            End If
        Catch ex As Exception
            res.Code = 500
            res.Message = ex.Message
        End Try
        Return res
    End Function

    Public Function generateRunningNumberNoPTJ(Kod_Modul As String, Prefix As String, Butiran As String) As String

        Dim db As New DBKewConn
        Dim newid As String = ""
        Dim noAkhir As Integer = 1

        Dim year = Date.Now.ToString("yyyy")
        Dim month = Date.Now.Month


        Dim sql As String = "select TOP 1 No_Akhir as id from SMKB_No_Akhir where Kod_Modul =@Kod_Modul AND Prefix =@Prefix AND Tahun =@year"
        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@year", year))
        param.Add(New SqlParameter("@Kod_Modul", Kod_Modul))
        param.Add(New SqlParameter("@Prefix", Prefix))

        Dim dt As DataTable
        dt = db.Read(sql, param)

        If dt.Rows.Count = 0 Then
            'no number in the year yet
            SharedModulePembayaran.InsertNoAkhir(Kod_Modul, Prefix, year, noAkhir, Butiran)

        Else
            noAkhir = CInt(dt.Rows(0).Item("id")) + 1

            SharedModulePembayaran.UpdateNoAkhir(Kod_Modul, Prefix, year, noAkhir)
        End If
        newid = Prefix + Format(noAkhir, "000000").ToString + month.ToString("00") + Right(year.ToString(), 2)
        Return newid
    End Function

    Public Function generateRunningNumber(Kod_Modul As String, Prefix As String, Kod_PTJ As String, Butiran As String) As String

        Dim db As New DBKewConn
        Dim newid As String = ""
        Dim noAkhir As Integer = 1

        Dim year = Date.Now.ToString("yyyy")
        Dim month = Date.Now.Month


        Dim sql As String = "select TOP 1 No_Akhir as id from SMKB_No_Akhir where Kod_Modul =@Kod_Modul AND Prefix =@Prefix AND Tahun =@year AND Kod_PTJ=@Kod_PTJ"
        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@year", year))
        param.Add(New SqlParameter("@Kod_Modul", Kod_Modul))
        param.Add(New SqlParameter("@Prefix", Prefix))
        param.Add(New SqlParameter("@Kod_PTJ", Kod_PTJ))

        Dim dt As DataTable
        dt = db.Read(sql, param)

        If dt.Rows.Count = 0 Then
            'no number in the year yet
            SharedModulePembayaran.InsertNoAkhir(Kod_Modul, Prefix, year, noAkhir, Butiran, Kod_PTJ)

        Else
            noAkhir = CInt(dt.Rows(0).Item("id")) + 1

            SharedModulePembayaran.UpdateNoAkhir(Kod_Modul, Prefix, year, noAkhir, Kod_PTJ)
        End If
        newid = Prefix + Kod_PTJ + Format(noAkhir, "000000").ToString + month.ToString("00") + Right(year.ToString(), 2)
        Return newid
    End Function

    Public Sub logDOK(KodModule As String, No_Rujukan As String, Status_Dok As String, Ulasan As String)
        Dim userID As String = HttpContext.Current.Session("ssusrID")

        Try
            Using sqlConn As New SqlConnection(dbSMKB.strCon)
                sqlConn.Open()
                Dim sqlcmd As New SqlCommand
                sqlcmd.Connection = sqlConn
                sqlcmd.CommandText = "INSERT INTO SMKB_Status_Dok (Kod_Modul,Kod_Status_Dok,No_Rujukan,No_Staf,Tkh_Tindakan,Tkh_Transaksi,Status_Transaksi,Status,Ulasan)
                VALUES (@Kod_Modul,@Kod_Status_Dok,@No_Rujukan,@No_Staf,getdate(),getdate(),@Status_Transaksi,@Status,@Ulasan)  "
                sqlcmd.Parameters.Add(New SqlParameter("@Kod_Modul", KodModule))
                sqlcmd.Parameters.Add(New SqlParameter("@Kod_Status_Dok", Status_Dok))
                sqlcmd.Parameters.Add(New SqlParameter("@No_Rujukan", No_Rujukan))
                sqlcmd.Parameters.Add(New SqlParameter("@No_Staf", userID))
                sqlcmd.Parameters.Add(New SqlParameter("@Status_Transaksi", "1"))
                sqlcmd.Parameters.Add(New SqlParameter("@Status", "1"))
                sqlcmd.Parameters.Add(New SqlParameter("@Ulasan", Ulasan))
                sqlcmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            Dim msg As String = ex.Message

        End Try

    End Sub


    Public Function findInLookupDetails(Kod As String, Optional key As String = "") As DataTable
        Dim dt As New DataTable
        Using sqlconn As New SqlConnection(dbSMKB.strCon)
            sqlconn.Open()

            Dim cmd As New SqlCommand
            cmd.Connection = sqlconn

            Dim sql As String = "SELECT Kod_Detail, Butiran FROM SMKB_Lookup_Detail WHERE Kod=@Kod"
            cmd.Parameters.Add(New SqlParameter("@Kod", Kod))

            If Not key.IsNullOrWhiteSpace Then
                sql += " AND ( Kod_Detail LIKE @key OR Butiran LIKE @key)"
                cmd.Parameters.Add(New SqlParameter("@key", "%" & key & "%"))
            End If
            cmd.CommandText = sql
            dt.Load(cmd.ExecuteReader())
        End Using
        Return dt
    End Function


    Public Function UpdateNoAkhir(kodModul As String, prefix As String, year As String, ID As String, Optional Kod_PTJ As String = "")
        Dim db As New DBKewConn
        Dim query As String = "UPDATE SMKB_No_Akhir
        set No_Akhir = @No_Akhir
        where Kod_Modul=@Kod_Modul and Prefix=@Prefix and Tahun =@Tahun"
        Dim param As New List(Of SqlParameter)

        If Not String.IsNullOrEmpty(Kod_PTJ) Then
            query += " AND  Kod_PTJ=@Kod_PTJ"
            param.Add(New SqlParameter("@Kod_PTJ", Kod_PTJ))
        End If

        param.Add(New SqlParameter("@No_Akhir", ID))
        param.Add(New SqlParameter("@Kod_Modul", kodModul))
        param.Add(New SqlParameter("@Prefix", prefix))
        param.Add(New SqlParameter("@Tahun", year))

        Return db.Process(query, param)
    End Function

    Public Function InsertNoAkhir(kodModul As String, prefix As String, year As String, ID As String, butiran As String, Optional Kod_PTJ As String = "")
        Dim db As New DBKewConn
        Dim query As String = "INSERT INTO SMKB_No_Akhir
        VALUES(@Kod_Modul ,@Prefix, @No_Akhir, @Tahun, @Butiran"
        Dim param As New List(Of SqlParameter)


        If Not String.IsNullOrEmpty(Kod_PTJ) Then
            query += ",@Kod_PTJ"
            param.Add(New SqlParameter("@Kod_PTJ", Kod_PTJ))
        End If
        query += ")"
        param.Add(New SqlParameter("@Kod_Modul", kodModul))
        param.Add(New SqlParameter("@Prefix", prefix))
        param.Add(New SqlParameter("@No_Akhir", ID))
        param.Add(New SqlParameter("@Tahun", year))
        param.Add(New SqlParameter("@Butiran", butiran))


        Return db.Process(query, param)
    End Function
End Module

'abstraction of ledger item so both invois dtl and baucar dtl can simply use 1 function to be posted
<Serializable>
Public Class LedgerItem
    Public Property Kod_Pemiutang As String
    Public Property Kod_Kump_Wang As String
    Public Property Kod_Operasi As String
    Public Property Kod_PTJ As String
    Public Property Kod_Projek As String
    Public Property Kod_Vot As String

    Overridable Function getSum() As Double
        Return 0
    End Function
End Class