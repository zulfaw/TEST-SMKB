Imports System.Web.Services
Imports Newtonsoft.Json

Imports System.Net
Imports System.Net.Mail
Imports System.Web.Configuration

Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports clsMail
Imports System.Reflection

Public Class Kelulusan1_JK
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

    Protected Sub EmailJurnalLulus_Click(sender As Object, e As EventArgs) Handles EmailJurnalLulus.ServerClick

        Dim fullName As String = "Zulfa Wahida Binti Ahmad"
        Dim email As String = "zulfaw@utem.edu.my"

        'Send the New password to the user's email
        Dim subject As String = "UTeM - Sistem Maklumat Kewangan Bersepadu"
        Dim body As String = "Kelulusan Transaksi Jurnal " &
                     "<br><br>" &
                     vbCrLf & "Assalamualaikum Dan Salam Sejahtera " & fullName & "," &
                     "<br><br>" &
                     vbCrLf & "Transaksi jurnal telah diluluskan. " &
                     "<br><br>" &
                     vbCrLf & "Sila klik di link ini untuk menyemak status transaksi jurnal melalui <a href=""https://devmis.utem.edu.my/iutem/"">https://devmis.utem.edu.my/iutem/</a>" &
                     "<br>" &
                     "<br><br>" &
                     vbCrLf & "Email dijanakan secara automatik daripada UTeM - Sistem Maklunat Kewangan Bersepadu. " &
                     "<br><br>" &
                     vbCrLf & "Email ini tidak perlu dibalas."

        myEmel(email, subject, body)
    End Sub

End Class

