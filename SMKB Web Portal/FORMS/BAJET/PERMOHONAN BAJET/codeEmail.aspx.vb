Imports System.Data.SqlClient
Imports System.Net
Imports System.Net.Mail
Imports System.Web.Configuration


Public Class Register
    Inherits System.Web.UI.Page

    Dim clsMail As New clsMail.Mail
    Dim dbconn As New DBConn(DBVen)
    Dim dbconnKw As New DBConn(DBKew)
    Dim crypto As New clsCrypto

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            If Not String.IsNullOrEmpty(Session("ssusrID")) Then
                Response.Redirect("~/Default.aspx")
            Else
                ViewState("CountRegis") = 0
            End If

        End If
    End Sub



    'Private Function fSendEmail() As Boolean
    '    Dim strMsg As String = ""
    '    Dim SenderAddr = WebConfigurationManager.AppSettings("SenderAddr")
    '    Dim SMTPServer = WebConfigurationManager.AppSettings("SMTPServer")
    '    Dim SMTPPort = WebConfigurationManager.AppSettings("SMTPPort")
    '    Try

    '        Dim strTo As String
    '        Dim strFrom As String
    '        Dim strsubject As String
    '        Dim strbody As String = ""
    '        Dim strAtt As String

    '        strsubject = "TEST EMAIL"
    '        strbody += "<br><b> Ini email testing dari SMKB Net."
    '        strbody += "<br>Email ini tidak perlu dibalas."

    '        strFrom = SenderAddr  ' "smkbTest@utem.edu.my"
    '        strTo = txtemail.Text  'Put semicolon(;) for multiple receipients i.e: "aaa@utem.edu.my;bbb@gmail.com"

    '        'strSMTPServer  ' "smtp01.utem.edu.my"
    '        'strSMTPPort ' "25"
    '        strAtt = "test.pdf;test2.pdf;test.txt;test.bmp;test.docx" 'Put semicolon(;) for multiple attachment i.e: "aaa.pdf;bbb.txt"
    '        strMsg = clsMail.fSendMail(strFrom, strTo, strsubject, strbody, SMTPServer, SMTPPort, strAtt)
    '        If strMsg = "1" Then
    '            Return True
    '        Else
    '            fErrorLog("fSendNotification- " & strMsg)
    '            Return False
    '        End If
    '    Catch ex As Exception
    '        'clsSearch.sWriteErrorLog(Session("dwspath"), "(Check_In.aspx)fSendNotification() --> " & ex.Message)
    '    End Try
    '    Return False
    'End Function


    Protected Sub lbtnDaftar_Click(sender As Object, e As EventArgs) Handles lbtnDaftar.Click
        If cbxSetuju.Checked Then
            daftar()
        Else
            fGlobalAlert("Sila setuju dengan syarat pendaftaran.", Me.Page, Me.[GetType]())
        End If
    End Sub

    Private Sub daftar()
        Try
            Dim kategori = ""
            Dim useridSem As String = ""
            If rbKategori1.SelectedValue = 1 Then
                kategori = "S"
            Else
                kategori = "I"
            End If
            Dim NoDaftar = txtNoDaftar.Text.Trim
            Dim NamaSya = txtNamaSya.Text

            'Check either got duplicate Email
            Dim strsqlEmail = $"select ROC01_IDSem, ROC01_NoSya, ROC01_NamaSya, ROC01_EmelP from ROC01_Syarikat WHERE ROC01_EmelP = '{txtemail.Text.Trim}' and ROC01_NoSya <> '{NoDaftar}';"
            Using dtEmail = dbconnKw.fselectCommandDt(strsqlEmail)
                If dtEmail.Rows.Count > 0 Then
                    Dim nameSyar = dtEmail.Rows(0)("ROC01_NamaSya").ToString
                    fGlobalAlert($"Email syarikat tuan telah digunakan oleh Syarikat lain iaitu: {nameSyar}. Sila guna email yang lain!", Me.Page, Me.[GetType]())
                    Exit Sub
                End If
            End Using

            Dim strsql1 = $"select ROC01_IDSem, ROC01_NoSya, ROC01_NamaSya from ROC01_Syarikat WHERE ROC01_NoSya = '{NoDaftar}';
SELECT ROC01_IDSem, ROC01_NoSya, ROC01_NamaSya From [ROC01_Syarikat] Where [ROC01_NoSya] = '{NoDaftar}' AND ROC01_IDSya IS NOT NULL AND ROC01_KodLulus = '1';
SELECT ROC01_IDSem, ROC01_NoSya, ROC01_NamaSya From [ROC01_Syarikat] Where [ROC01_NoSya] = '{NoDaftar}' AND ROC01_IDSya IS NOT NULL;"
            Using dsSya = dbconnKw.fselectCommandDs(strsql1)
                If dsSya IsNot Nothing Then
                    If dsSya.Tables(0).Rows.Count = 0 Then
                        useridSem = fJanaNoIdSemROC()
                    ElseIf dsSya.Tables(0).Rows.Count = 1 Then
                        useridSem = dsSya.Tables(0).Rows(0)("ROC01_IDSem")
                    ElseIf dsSya.Tables(0).Rows.Count > 1 Then
                        If dsSya.Tables(1).Rows.Count = 0 Then
                            If dsSya.Tables(2).Rows.Count = 1 Then
                                useridSem = dsSya.Tables(2).Rows(0)("ROC01_IDSem")
                            Else
                                fGlobalAlert("No Pendaftaran syarikat mempunyai lebih satu rekod. Sila hubungi admin.", Me.Page, Me.[GetType]())
                                Exit Sub
                            End If
                        Else
                            useridSem = dsSya.Tables(1).Rows(0)("ROC01_IDSem")
                        End If
                    End If

                    Dim passwd = crypto.fEncryptHashingMD5(txtPassword.Text.Trim())

                    Dim paramSql() As SqlParameter = {
                                New SqlParameter("@UserID", useridSem),
                                New SqlParameter("@NoSya", txtNoDaftar.Text),
                                New SqlParameter("@NamaSya", txtNamaSya.Text),
                                New SqlParameter("@Password", passwd),
                                New SqlParameter("@Email", txtemail.Text.Trim()),
                                New SqlParameter("@Kategori", kategori)
                                }

                    Dim intStatus As Integer

                    If rbKategori1.SelectedValue = 1 Then
                        dbconn.sSP_GetItem("ROC_Insert_Vendor", paramSql, intStatus)
                        'Else
                        'dbconn.sSP_GetItem("ROC_Insert_User", paramSql, intStatus)
                    End If

                    Dim message As String = String.Empty
                    Dim strNamaSya As String
                    Select Case intStatus
                        Case -1
                            message = "No pendaftaran telah digunakan."
                            fGlobalAlert(message, Me.Page, Me.[GetType]())
                            Exit Select
                        Case Else
                            Dim activationCode As String = Guid.NewGuid().ToString()
                            Dim strsql = "INSERT INTO ROC_LoginActivation VALUES(@UserId, @ActivationCode)"
                            paramSql = {
                            New SqlParameter("@UserID", useridSem),
                            New SqlParameter("@ActivationCode", activationCode)
                            }

                            Dim countinsert = dbconn.fUpdateCommand(strsql, paramSql)
                            If countinsert > 0 Then
                                strNamaSya = txtNamaSya.Text
                                Dim Subject = "Pengaktifan Akaun"
                                Dim body As String = "Salam sejahtera," + strNamaSya
                                body += "<br /><br />Sila klik pautan berikut untuk tujuan pengaktifan akaun"
                                body += "<br /><a href = '" + Request.Url.AbsoluteUri.Replace("Register", Convert.ToString("EmailActivation.aspx?ActivationCode=") & activationCode) + "'>Klik di sini untuk pengaktifan akaun tuan.</a>"
                                body += "<br /><br />Terima kasih"
                                Dim sent = clsSharedMails.sendEmail(txtemail.Text, Subject, body)
                                If sent Then
                                    message = $"Pendaftaran berjaya. Kod pengaktifan telah dihantar melalui email: {txtemail.Text}."
                                    fGlobalAlert(message, Me.Page, Me.[GetType](), "../Account/Login.aspx")
                                End If
                            Else
                                message = $"Pendaftaran Tidak berjaya."
                                fGlobalAlert(message, Me.Page, Me.[GetType]())
                            End If
                            Exit Select
                    End Select
                End If
            End Using
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub lbtnBatal_Click(sender As Object, e As EventArgs) Handles lbtnBatal.Click
        txtNoDaftar.Text = ""
        txtNamaSya.Text = ""

        txtemail.Text = ""
        txtPassword.Text = ""
        txtConfirmPassword.Text = ""
    End Sub

    Private Function fJanaNoIdSemROC() As String
        Dim year = Date.Now.ToString("yy")
        Dim month = Date.Now.Month

        Dim indx = NoIndexSemROC()
        Dim strNo = Format(indx, "000000").ToString
        Dim NoId = "SS" + strNo + month.ToString("00") + year
        UpdateNoAkhir(indx)
        Return NoId
    End Function

    Private Function NoIndexSemROC()
        Dim index As Double = 0

        Dim year = Date.Now.Year

        Dim strSql = $"SELECT NoAkhir From MK_NoAkhir WHERE KodModul = 'ROC' AND Prefix = 'SS' AND Tahun = {year}"

        Using dt = dbconnKw.fselectCommandDt(strSql)

            If dt.Rows.Count = 1 Then
                index = dt.Rows(0)("NoAkhir") + 1
            Else
                index = 1
            End If
        End Using

        Return index
    End Function

    Private Sub UpdateNoAkhir(no As Integer)
        Dim year = Date.Now.Year

        Dim strSql = "SELECT KodModul, Prefix, NoAkhir, Tahun, Butiran, kodPTJ, ID From MK_NoAkhir WHERE KodModul = @KodModull AND Prefix = @Prefixx AND Tahun = @years and kodptj = @KodPtjj"
        Dim paramSql() As SqlParameter = {
            New SqlParameter("@KodModull", "ROC"),
            New SqlParameter("@Prefixx", "SS"),
            New SqlParameter("@years", year),
            New SqlParameter("@KodPtjj", "-")
            }
        dbconnKw = New DBConn(DBKew)
        Using dt = dbconnKw.fSelectCommandDt(strSql, True, paramSql)

            If dt.Rows.Count > 0 Then
                dt.Rows(0)("NoAkhir") = no
            Else
                Dim dr As DataRow
                dr = dt.NewRow
                dr("KodModul") = "ROC"
                dr("Prefix") = "SS"
                dr("noakhir") = 1
                dr("Tahun") = year
                dr("Butiran") = "ROC"
                dr("kodPTJ") = "-"
                dt.Rows.Add(dr)
            End If

            dbconnKw.sUpdateCommandDt(dt)
        End Using

    End Sub

    Private Function checkNoSyarikat() As Int16
        Dim strSql As String = $"SELECT [ROC01_NoSya], [ROC01_NamaSya], ROC01_EmelP From [ROC01_Syarikat] Where [ROC01_NoSya] = '{txtNoDaftar.Text}'"
        Using dt = dbconnKw.fselectCommandDt(strSql)

            Dim value = 0

            If dt.Rows.Count > 0 Then

                value = 1

                If ViewState("CountRegis") = 0 Then
                    fGlobalAlert("No pendaftaran telah terdapat di dalam rekod pengkalan data, dan anda hendaklah memasuki kata laluan yang baru.", Page, Me.GetType)
                    txtemail.Text = IIf(IsDBNull(dt.Rows(0)("ROC01_EmelP")), "", dt.Rows(0)("ROC01_EmelP"))

                    txtNamaSya.Text = dt.Rows(0)("ROC01_NamaSya")
                    txtemail.ReadOnly = True
                    txtNamaSya.ReadOnly = True
                    txtNoDaftar.ReadOnly = True
                End If

                ViewState("CountRegis") = ViewState("CountRegis") + 1

                ' End If
            End If

            Return value
        End Using
    End Function

    Protected Sub txtNoDaftar_TextChanged(sender As Object, e As EventArgs) Handles txtNoDaftar.TextChanged
        checkSyarikat(txtNoDaftar.Text.Trim)
    End Sub

    Private Sub checkSyarikat(nosya As String)
        'Check Login in DBCLM
        Dim strSql As String = $"SELECT count(*) From ROC_Login Where NoDaftar = @NoDaftarr"
        Dim paramSql() As SqlParameter = {
                            New SqlParameter("@NoDaftarr", nosya)
                            }
        If dbconn.fCountRecord(strSql, paramSql) > 0 Then
            fGlobalAlert("No pendaftaran tidak perlu didaftarkan. Sila log masuk!", Page, Me.GetType(), "../Account/Login.aspx")
        Else
            'Check ROC in DBKewangan
            strSql = $"SELECT [ROC01_NoSya],[ROC01_NamaSya], ROC01_EmelP From [ROC01_Syarikat] Where [ROC01_NoSya] = '{nosya}';
SELECT [ROC01_NoSya],[ROC01_NamaSya], ROC01_EmelP From [ROC01_Syarikat] Where [ROC01_NoSya] = '{nosya}' AND ROC01_IDSya IS NOT NULL AND ROC01_KodLulus = '1';
SELECT [ROC01_NoSya],[ROC01_NamaSya], ROC01_EmelP From [ROC01_Syarikat] Where [ROC01_NoSya] = '{nosya}' AND ROC01_IDSya IS NOT NULL;"
            Using ds = dbconnKw.fselectCommandDs(strSql)
                Dim dt = ds.Tables(0)

                If dt.Rows.Count > 0 Then
                    fGlobalAlert("No pendaftaran telah terdapat didalam rekod pengkalan data. Sila masukkan email and dan kata laluan", Page, Me.GetType)
                    If dt.Rows.Count > 1 Then

                        Dim dt1 = ds.Tables(1)
                        If dt1.Rows.Count = 1 Then

                            txtemail.Text = IIf(IsDBNull(dt1.Rows(0)("ROC01_EmelP")), "", dt1.Rows(0)("ROC01_EmelP"))
                            txtNamaSya.Text = dt1.Rows(0)("ROC01_NamaSya")
                            If Not String.IsNullOrEmpty(txtemail.Text) Then
                                txtemail.ReadOnly = True
                            End If

                            txtNamaSya.ReadOnly = True
                            txtNoDaftar.ReadOnly = True
                        ElseIf dt1.Rows.Count = 0 Then
                            Dim dt2 = ds.Tables(2)
                            If dt1.Rows.Count > 0 Then
                                txtemail.Text = IIf(IsDBNull(dt1.Rows(0)("ROC01_EmelP")), "", dt1.Rows(0)("ROC01_EmelP"))
                                txtNamaSya.Text = dt1.Rows(0)("ROC01_NamaSya")
                                If Not String.IsNullOrEmpty(txtemail.Text) Then
                                    txtemail.ReadOnly = True
                                End If

                                txtNamaSya.ReadOnly = True
                                txtNoDaftar.ReadOnly = True
                            Else
                                fGlobalAlert("No Pendaftaran syarikat tuan tiada dalam rekod. Sila hubungi admin.", Me.Page, Me.[GetType]())
                            End If
                        End If
                    Else

                        txtemail.Text = IIf(IsDBNull(dt.Rows(0)("ROC01_EmelP")), "", dt.Rows(0)("ROC01_EmelP"))
                        txtNamaSya.Text = dt.Rows(0)("ROC01_NamaSya")
                        If Not String.IsNullOrEmpty(txtemail.Text) Then
                            txtemail.ReadOnly = True
                        End If

                        txtNamaSya.ReadOnly = True
                        txtNoDaftar.ReadOnly = True
                    End If
                End If
            End Using
        End If
    End Sub

    Protected Sub rbKategori1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rbKategori1.SelectedIndexChanged
        If rbKategori1.SelectedValue = 1 Then
            lblNoDaftar.Text = "No Pendaftaran SSM :"
            lblNamaSya.Text = "Nama Syarikat :"
            EmailLabel.Text = "Email Syarikat :"
        ElseIf rbKategori1.SelectedValue = 2 Then
            lblNoDaftar.Text = "No Kad Pengenalan :"
            lblNamaSya.Text = "Nama :"
            EmailLabel.Text = "Email :"
        Else
            lblNoDaftar.Text = "No ROS :"
            lblNamaSya.Text = "Nama Pertubuhan :"
            EmailLabel.Text = "Email Pertubuhan :"
        End If

    End Sub
End Class