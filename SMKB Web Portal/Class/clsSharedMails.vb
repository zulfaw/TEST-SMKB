Imports System.Net.Mail
Imports System.Web.Configuration
Imports System.Net.Mime
Imports System.IO

Public NotInheritable Class clsSharedMails


#Region "Declares"
    Private Shared SenderAddr As String = WebConfigurationManager.AppSettings("SenderAddr")
    Private Shared SMTPServer As String = WebConfigurationManager.AppSettings("SMTPServer")
    Private Shared SMTPPort As String = WebConfigurationManager.AppSettings("SMTPPort")
#End Region

    Private Sub New()
    End Sub

    Public Shared Function sendEmail(EmailAddTo As String, txtSubject As String, txtBody As String) As Boolean
        Using mm As New MailMessage(SenderAddr, EmailAddTo, txtSubject, txtBody)
            mm.IsBodyHtml = True
            Dim smtp As New SmtpClient(SMTPServer, SMTPPort)
            smtp.EnableSsl = False
            smtp.UseDefaultCredentials = True
            Try
                smtp.Send(mm)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Using
    End Function

    Public Shared Function sendEmail(EmailAddTo As String, CCAddTo As String, txtSubject As String, txtBody As String) As Boolean
        Using mm As New MailMessage(SenderAddr, EmailAddTo, txtSubject, txtBody)
            mm.CC.Add(CCAddTo)
            mm.IsBodyHtml = True
            Dim smtp As New SmtpClient(SMTPServer, SMTPPort)
            smtp.EnableSsl = False
            smtp.UseDefaultCredentials = True
            Try
                smtp.Send(mm)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Using
    End Function


    Public Shared Function sendEmail(EmailAddTo As String, CCAddTo As String, BCCAddTo As String, txtSubject As String, txtBody As String) As Boolean
        Using mm As New MailMessage(SenderAddr, EmailAddTo, txtSubject, txtBody)
            mm.CC.Add(CCAddTo)
            mm.Bcc.Add(BCCAddTo)
            mm.IsBodyHtml = True
            Dim smtp As New SmtpClient(SMTPServer, SMTPPort)
            smtp.EnableSsl = False
            smtp.UseDefaultCredentials = True
            Try
                smtp.Send(mm)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Using
    End Function

    Public Shared Function sendEmailCalender(EmailAddTo As String, txtSubject As String, txtBody As String, dateStart As DateTime, dateEnd As DateTime, location As String) As Boolean
        Using mm As New MailMessage(SenderAddr, EmailAddTo, txtSubject, txtBody)
            Try
                mm.Headers.Add("Content-class", "urn:content-classes:calendarmessage")
                mm.IsBodyHtml = True
                Dim smtp As New SmtpClient(SMTPServer, SMTPPort)
                smtp.EnableSsl = False
                smtp.UseDefaultCredentials = True
                Dim str = New StringBuilder()
                str.AppendLine("BEGIN:VCALENDAR")
                str.AppendLine("PRODID:-//Schedule a Meeting")
                str.AppendLine("VERSION:2.0")
                str.AppendLine("METHOD:REQUEST")
                str.AppendLine("BEGIN:VEVENT")
                str.AppendLine(String.Format("DTSTART:{0:yyyyMMddTHHmmss}", dateStart))
                str.AppendLine(String.Format("DTSTAMP:{0:yyyyMMddTHHmmss}", DateTime.Now))
                str.AppendLine(String.Format("DTEND:{0:yyyyMMddTHHmmss}", dateEnd))
                str.AppendLine($"LOCATION: {location}")
                str.AppendLine($"UID:{Guid.NewGuid()}")
                str.AppendLine($"DESCRIPTION:{ mm.Body}")
                str.AppendLine($"X-ALT-DESC;FMTTYPE=text/html:{ mm.Body}")
                str.AppendLine($"SUMMARY:{mm.Subject}")
                str.AppendLine($"ORGANIZER:MAILTO:{mm.From.Address}")

                Dim emailUsers = EmailAddTo.Split(";").ToList()
                For Each emailUser In emailUsers
                    str.AppendLine($"ATTENDEE;RSVP=TRUE:mailto:{emailUser}")
                Next

                str.AppendLine("BEGIN:VALARM")
                str.AppendLine("TRIGGER:-PT15M")
                str.AppendLine("ACTION:DISPLAY")
                str.AppendLine("DESCRIPTION:Reminder")
                str.AppendLine("END:VALARM")
                str.AppendLine("END:VEVENT")
                str.AppendLine("END:VCALENDAR")

                Dim byteArray = Encoding.ASCII.GetBytes(str.ToString())
                Dim stream = New MemoryStream(byteArray)
                Dim contype = New ContentType("text/calendar")
                contype.Parameters.Add("method", "REQUEST")
                contype.Parameters.Add("name", "Meeting.ics")
                mm.Attachments.Add(New Attachment(stream, contype))

                Dim HTML = AlternateView.CreateAlternateViewFromString(txtBody, New ContentType("text/html"))
                mm.AlternateViews.Add(HTML)
                Dim avCal = AlternateView.CreateAlternateViewFromString(str.ToString(), contype)
                mm.AlternateViews.Add(avCal)

                smtp.Send(mm)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Using
    End Function

    Public Shared Function sendEmailCalender(EmailAddTo As String, CCAddTo As String, txtSubject As String, txtBody As String, dateStart As DateTime, dateEnd As DateTime, location As String) As Boolean
        Using mm As New MailMessage(SenderAddr, EmailAddTo, txtSubject, txtBody)
            mm.CC.Add(CCAddTo)
            mm.Headers.Add("Content-class", "urn:content-classes:calendarmessage")
            mm.IsBodyHtml = True
            Dim smtp As New SmtpClient(SMTPServer, SMTPPort)
            smtp.EnableSsl = False
            smtp.UseDefaultCredentials = True
            Dim str = New StringBuilder()
            str.AppendLine("BEGIN:VCALENDAR")
            str.AppendLine("PRODID:-//Schedule a Meeting")
            str.AppendLine("VERSION:2.0")
            str.AppendLine("METHOD:REQUEST")
            str.AppendLine("BEGIN:VEVENT")
            str.AppendLine(String.Format("DTSTART:{0:yyyyMMddTHHmmss}", dateStart))
            str.AppendLine(String.Format("DTSTAMP:{0:yyyyMMddTHHmmss}", DateTime.Now))
            str.AppendLine(String.Format("DTEND:{0:yyyyMMddTHHmmss}", dateEnd))
            str.AppendLine($"LOCATION: {location}")
            str.AppendLine($"UID:{Guid.NewGuid()}")
            str.AppendLine($"DESCRIPTION:{ mm.Body}")
            str.AppendLine($"X-ALT-DESC;FMTTYPE=text/html:{ mm.Body}")
            str.AppendLine($"SUMMARY:{mm.Subject}")
            str.AppendLine($"ORGANIZER:MAILTO:{mm.From.Address}")

            Dim emailUsers = EmailAddTo.Split(";").ToList()
            For Each emailUser In emailUsers
                str.AppendLine($"ATTENDEE;RSVP=TRUE:mailto:{emailUser}")
            Next

            str.AppendLine("BEGIN:VALARM")
            str.AppendLine("TRIGGER:-PT15M")
            str.AppendLine("ACTION:DISPLAY")
            str.AppendLine("DESCRIPTION:Reminder")
            str.AppendLine("END:VALARM")
            str.AppendLine("END:VEVENT")
            str.AppendLine("END:VCALENDAR")

            Dim byteArray = Encoding.ASCII.GetBytes(str.ToString())
            Dim stream = New MemoryStream(byteArray)
            Dim contype = New ContentType("text/calendar")
            contype.Parameters.Add("method", "REQUEST")
            contype.Parameters.Add("name", "Meeting.ics")
            mm.Attachments.Add(New Attachment(stream, contype))

            Dim HTML = AlternateView.CreateAlternateViewFromString(txtBody, New ContentType("text/html"))
            mm.AlternateViews.Add(HTML)
            Dim avCal = AlternateView.CreateAlternateViewFromString(str.ToString(), contype)
            mm.AlternateViews.Add(avCal)
            Try
                smtp.Send(mm)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Using
    End Function
End Class
