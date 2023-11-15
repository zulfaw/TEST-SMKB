Imports System.Net
Imports System.Net.Mail

Public Class TestEmail
    Inherits System.Web.UI.Page

    Protected Sub btnSendEmail_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSendEmail.Click
        Dim fromAddress As New MailAddress("smkb@utem.edu.my", "SISTEM MAKLUMAT KEWANGAN BERSEPADU")
        Dim toAddress As New MailAddress("mohdazmi@utem.edu.my", "Mohd Azmi Suratman")
        Dim fromPassword As String = "your-email-password"
        Dim subject As String = "Testing email from SMKB v4"
        Dim body As String = "Assalamualaikum wrt. wbt. dan Salam Sejahtera
Prof. / Prof.Madya/ Dr./ Tuan / Puan

Untuk makluman, UNIVERSITI TEKNIKAL MALAYSIA MELAKA (UTeM) telah meluluskan bayaran seperti berikut :."

        Dim smtpClient As New SmtpClient()
        smtpClient.Host = "smtp.example.com"
        smtpClient.Port = 587
        smtpClient.EnableSsl = True
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network
        smtpClient.UseDefaultCredentials = False
        smtpClient.Credentials = New NetworkCredential(fromAddress.Address, fromPassword)

        Dim message As New MailMessage(fromAddress, toAddress)
        message.Subject = subject
        message.Body = body

        Try
            smtpClient.Send(message)
            Response.Write("Email sent successfully.")
        Catch ex As Exception
            Response.Write("Error sending email: " & ex.Message)
        End Try
    End Sub

End Class