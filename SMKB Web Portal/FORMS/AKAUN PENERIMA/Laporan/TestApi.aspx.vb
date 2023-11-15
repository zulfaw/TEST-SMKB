Imports Microsoft.AspNet.Identity
Imports System.Globalization
Imports System.Threading
Imports System.Configuration
Imports System.Web.Configuration
Imports System.Collections.Specialized
Imports System.Reflection
Imports System
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Net.NetworkInformation
Imports System.Diagnostics.Eventing
Imports System.Diagnostics.Eventing.Reader
Imports System.Linq

Public Class TestApi
    Inherits System.Web.UI.Page
    'Dim dbconn As New DBKewConn
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' txtTokenID.Value = Session("tokenID")

            txtTkhTrans.Value = DateTime.Today.ToString("yyyy-MM-dd")
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("ms-MY")
        End If
    End Sub


    'Protected Async Sub loginButton_Click(sender As Object, e As EventArgs) Handles loginButton.Click
    '    Dim username As String = Me.username.Value
    '    Dim password As String = Me.password.Value

    '    Dim mypass As String = ""
    '    Dim servicedd As New ValuesService()
    '    Dim hhz As String = CryptoService.EnecryptStringAES_notuser(password, username.ToLower().Trim())

    '    If hhz.Length = 24 Then
    '        mypass = CryptoService.GenerateRandomString(3) & hhz.Substring(0, hhz.Length - 2) & CryptoService.GenerateRandomString(5)
    '    ElseIf hhz.Length = 44 Then
    '        mypass = CryptoService.GenerateRandomString(3) & hhz.Substring(0, hhz.Length - 1) & CryptoService.GenerateRandomString(5)
    '    Else
    '        mypass = CryptoService.GenerateRandomString(3) & hhz.Substring(0, hhz.Length - 0) & CryptoService.GenerateRandomString(5)
    '    End If

    '    Try
    '        GlobalSMKBAPI.UserLoginDetails._accessToken = Await servicedd.Login(username.ToLower().Trim(), mypass)

    '        Dim servicex As New ValuesService()
    '        Dim values As IEnumerable(Of String) = Await servicex.GetValuesStart2(GlobalSMKBAPI.UserLoginDetails._accessToken.AccessToken.ToString(), "values/1")
    '        Dim myList = values.ToList()
    '        GlobalSMKBAPI.UserLoginDetails.u_Userid = GlobalSMKBAPI.UserLoginDetails._accessToken.Username.ToString()
    '        GlobalSMKBAPI.UserLoginDetails.u_Usertype = myList(0)
    '        txtTokenID.Value = GlobalSMKBAPI.UserLoginDetails._accessToken.AccessToken.ToString()

    '        errorMessage.InnerText = GlobalSMKBAPI.UserLoginDetails.u_Userid
    '        'MessageBox.Show(Global.UserLoginDetails.u_Userid)
    '    Catch ex As Exception
    '        errorMessage.InnerText = "Invalid username or password."
    '        'MessageBox.Show("Invalid login or password")
    '        txtTokenID.Value = ""
    '    End Try


    'End Sub

    Protected Async Sub GetDataApi_Click(sender As Object, e As EventArgs)
        Try
            Dim myGetTicket As New TokenResponseModel()

            Dim servicex As New ValuesService()
            Dim values As IEnumerable(Of String) = Await servicex.GetValuesStart2(myGetTicket.GetTicket("smkb", Session("ssusrID")), "values/1")
            Dim myList = values.ToList()
            errorMessage.InnerText = ("Data0 " & myList(0) & " " & "Data1 " & myList(1) & " " & "Data2 " & myList(2))
        Catch
            errorMessage.InnerText = "Invalid token"
        End Try
    End Sub

    Protected Async Sub InsertData_Click(sender As Object, e As EventArgs)
        Try
            Dim myGetTicket As New TokenResponseModel()

            Dim servicex As New ValuesService()
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("ms-MY")
            Dim parsedDate As Date = CDate(txtTkhTrans.Value).ToString("yyyy-MM-dd")
            Dim vBulan As String = parsedDate.Month
            Dim vTahun As String = parsedDate.Year
            Dim values As String = Await servicex.SendDataLejar(myGetTicket.GetTicket("smkb", Session("ssusrID")),
                                        txtData1.Value, txtData2.Value, txtData3.Value, txtData4.Value,
                                        txtData5.Value, txtData6.Value, txtData7.Value, txtData8.Value, txtData9.Value, vBulan, vTahun)

            If values.Contains("ok") Then

                lblModalMessaage.Text = "Rekod telah disimpan" 'message di modal
                ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)

            Else
                lblModalMessaage.Text = "Rekod Gagal disimpan" 'message di modal
                ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
            End If
        Catch
            lblModalMessaage.Text = "Invalid Token" 'message di modal
            ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)

        End Try
    End Sub






End Class