Imports System.Web.Services
Imports Newtonsoft.Json

Imports System.Net
Imports System.Net.Mail
Imports System.Web.Configuration

Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports clsMail
Imports System.Reflection


Public Class Transaksi_Jurnal
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

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

    Protected Sub EmailJurnal3_Click(sender As Object, e As EventArgs) Handles EmailJurnal3.ServerClick

        Dim fullName As String = "Zulfa Wahida Binti Ahmad"
        Dim email As String = "zulfaw@utem.edu.my"

        'Send the New password to the user's email
        Dim subject As String = "UTeM - Sistem Maklumat Kewangan Bersepadu"
        Dim body As String = "Kelulusan Transaksi Jurnal " &
                     "<br><br>" &
                     vbCrLf & "Assalamualaikum Dan Salam Sejahtera " & fullName & "," &
                     "<br><br>" &
                     vbCrLf & "Memerlukan kelulusan anda untuk transaksi jurnal. " &
                     "<br><br>" &
                     vbCrLf & "Sila klik di link ini untuk menyemak transaksi jurnal untuk kelulusan melalui <a href=""https://devmis.utem.edu.my/iutem/"">https://devmis.utem.edu.my/iutem/</a>" &
                     "<br>" &
                     "<br><br>" &
                     vbCrLf & "Email dijanakan secara automatik daripada UTeM - Sistem Maklunat Kewangan Bersepadu. " &
                     "<br><br>" &
                     vbCrLf & "Email ini tidak perlu dibalas."

        myEmel(email, subject, body)
    End Sub




End Class

Public Class ItemList
    Public Property text As String
    Public Property value As String

    Public Sub New()

    End Sub

    Public Sub New(text_, val_)
        text = text_
        value = val_
    End Sub
End Class

Public Class Order
    Public Property OrderID As String

    Public Property NoRujukan As String
    Public Property Perihal As String
    Public Property Tarikh As String
    Public Property JenisTransaksi As String
    Public Property JumlahDebit As String
    Public Property Jumlahkredit As String
    Public Property JumlahBeza As String
    Public Property OrderDetails As List(Of OrderDetail)

    Public Sub New()

    End Sub

    Public Sub New(orderId_ As String, NoRujukan_ As String, Perihal_ As String, Tarikh_ As String, JenisTransaksi_ As String, JumlahDebit_ As String, Jumlahkredit_ As String, JumlahBeza_ As String, lOrderDetails_ As List(Of OrderDetail))
        OrderID = orderId_
        NoRujukan = NoRujukan_
        Perihal = Perihal_
        Tarikh = Tarikh_
        JenisTransaksi = JenisTransaksi_
        JumlahDebit = JumlahDebit_
        Jumlahkredit = Jumlahkredit_
        OrderDetails = lOrderDetails_
        JumlahBeza = JumlahBeza_

    End Sub
End Class

Public Class OrderDetail
    Public Property id As String
    Public Property OrderID As String
    Public Property ddlPTJ As String
    Public Property ddlVot As String
    Public Property ddlKw As String
    Public Property ddlKo As String
    Public Property ddlKp As String
    Public Property debit As Decimal
    Public Property kredit As Decimal

    Public Sub New()

    End Sub

    Public Sub New(Optional id_ As String = "", Optional orderId_ As String = "", Optional ddlVot_ As String = "",
                   Optional debit_ As Decimal = 0.00, Optional kredit_ As Decimal = 0.00, Optional ddlPTJ_ As String = "",
                   Optional ddlKw_ As String = "", Optional ddlKo_ As String = "", Optional ddlKp_ As String = "")
        id = id_
        ddlVot = ddlVot_
        ddlPTJ = ddlPTJ_
        ddlKw = ddlKw_
        ddlKo = ddlKo_
        ddlKp = ddlKp_
        debit = debit_
        kredit = kredit_
        OrderID = orderId_


    End Sub





End Class