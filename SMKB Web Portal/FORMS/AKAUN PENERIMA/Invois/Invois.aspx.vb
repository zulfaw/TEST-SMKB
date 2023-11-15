Imports System.Web.Services
Imports Newtonsoft.Json
Imports System.Data
Imports System.Data.OleDb

Imports System.Net
Imports System.Net.Mail
Imports System.Web.Configuration

Imports System.Data.SqlClient
Imports clsMail
Imports System.Reflection

Public Class Invois
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim A = "ayam"
    End Sub
    Public strConEmail As String = "Provider=SQLOLEDB;Driver={SQL Server};server=V-SQL12.utem.edu.my\SQL_INS02;database=dbKewangan;uid=Smkb;pwd=smkb*pwd;"

    Public Function myEmel(alamat, subject, body)
        Dim cnExec As OleDb.OleDbConnection
        Dim cmdExec As OleDb.OleDbCommand

        Try
            cnExec = New OleDb.OleDbConnection(strConEmail)
            cnExec.Open()

            cmdExec = New OleDbCommand("EXEC msdb.dbo.sp_send_dbmail @profile_name= 'EmailSmkb', @recipients= '" & alamat & "', @subject = '" & subject & "', " &
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

    Protected Sub btnHantarEmail_Click(sender As Object, e As EventArgs) Handles btnHantarEmail.ServerClick

        ' (Haziq - 17/8/2023) 
        Dim noBil As String = Request.Form("emailBilTuntutan") ' Get the value of the "#emailBilTuntutan" input
        Dim fullName As String = Request.Form("emailName") ' Get the value of the "#emailName" input
        Dim email As String = Request.Form("emailEmail") ' Get the value of the "#emailEmail" input

        ' Send the new password to the user's email
        Dim subject As String = "UTeM - Sistem Maklumat Kewangan Bersepadu"
        Dim body As String = "Bil Tuntutan" _
        & "<br><br>" _
        & vbCrLf & "Assalamualaikum dan Salam Sejahtera " & fullName & "," _
        & "<br><br>" _
        & vbCrLf & "Pelanggan yang dihargai, " _
        & "<br><br>" _
        & vbCrLf & "E-mel ini adalah pemakluman mengenai Bil Tuntutan  (No Bil : " + noBil + ") yang didaftarkan atas nama Encik / Puan, " _
        & "<br><br>" _
        & vbCrLf & "Sila jelaskan bayaran sebelum tarikh tamat. Sila abaikan e-emel ini jika bayaran telah dibuat. " _
        & "<br><br>" _
        & vbCrLf & "Sekiranya terdapat sebarang pertanyaan, sila hubungi kami di talian +606-3316871 [Waktu Operasi 8:00 pagi hingga 5:00 petang, Isnin hingga Jumaat]" _
        & "<br>" _
        & "<br><br>" _
        & vbCrLf & "Email dijanakan secara automatik daripada UTeM - Sistem Maklumat Kewangan Bersepadu. " _
        & "<br><br>" _
        & vbCrLf & "Email ini tidak perlu dibalas"

        Dim strMsg = myEmel(email, subject, body)
        If strMsg = "1" Then
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "showDisplay('Berjaya');", True)
            'fGlobalAlert("Emel berjaya dihantar!", Me.Page, Me.[GetType]())
        Else
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "showDisplay('Tidak Berjaya');", True)
            'fGlobalAlert("Emel tidak berjaya dihantar!", Me.Page, Me.[GetType]())
        End If
    End Sub

End Class


Public Class ItemList_inv
    Public Property text As String
    Public Property value As String

    Public Sub New()

    End Sub

    Public Sub New(text_, val_)
        text = text_
        value = val_
    End Sub
End Class

Public Class Order_inv
    Public Property OrderID As String
    Public Property PenghutangID As String
    Public Property TkhMula As String
    Public Property TkhTamat As String
    Public Property Kontrak As String
    Public Property JenisUrusniaga As String
    Public Property Tujuan As String
    Public Property Jumlah As String
    Public Property tkhBil As String
    Public Property tempoh As String
    Public Property tempohbyrn As String
    Public Property norujukan As String




    Public Property OrderDetails As List(Of OrderDetail_inv)

    Public Sub New()

    End Sub

    Public Sub New(orderId_ As String, PenghutangID_ As String, TkhMula_ As String, TkhTamat_ As String, Kontrak_ As String, JenisUrusniaga_ As String, Tujuan_ As String, Jumlah_ As String, tkhBil_ As String, tempoh_ As String, tempohbyrn_ As String, norujukan_ As String, lOrderDetails_ As List(Of OrderDetail_inv))
        OrderID = orderId_
        PenghutangID = PenghutangID_
        TkhMula = TkhMula_
        TkhTamat = TkhTamat_
        Kontrak = Kontrak_
        JenisUrusniaga = JenisUrusniaga_
        Tujuan = Tujuan_
        OrderDetails = lOrderDetails_
        Jumlah = Jumlah_
        tkhBil = tkhBil_
        tempoh = tempoh_
        tempohbyrn = tempohbyrn_
        norujukan = norujukan_
    End Sub
End Class

Public Class OrderDetail_inv
    Public Property id As String
    Public Property OrderID As String
    Public Property ddlVot As String
    Public Property ddlPTJ As String
    Public Property ddlKw As String
    Public Property ddlKo As String
    Public Property ddlKp As String
    Public Property details As String
    Public Property quantity As Integer?
    Public Property price As Decimal?
    Public Property amount As Decimal?
    Public Property Diskaun As Decimal?
    Public Property Cukai As Decimal?
    Public Property Kadar_harga As Decimal?

    Public Property PenghutangID As String

    Public Sub New()

    End Sub

    Public Sub New(Optional id_ As String = "", Optional orderId_ As String = "", Optional ddlVot_ As String = "", Optional details_ As String = "",
                   Optional quantity_ As String = "", Optional price_ As String = "", Optional amount_ As String = "", Optional ddlPTJ_ As String = "",
                   Optional ddlKw_ As String = "", Optional ddlKo_ As String = "", Optional ddlKp_ As String = "", Optional Diskaun_ As String = "",
                   Optional Cukai_ As String = "", Optional Kadar_harga_ As String = "", Optional PenghutangID_ As String = "")
        id = id_
        ddlVot = ddlVot_
        details = details_
        quantity = quantity_
        price = price_
        amount = amount_
        OrderID = orderId_
        ddlPTJ = ddlPTJ_
        ddlKw = ddlKw_
        ddlKo = ddlKo_
        ddlKp = ddlKp_
        Diskaun = Diskaun_
        Cukai = Cukai_
        Kadar_harga = Kadar_harga_
        PenghutangID = PenghutangID_
    End Sub
End Class