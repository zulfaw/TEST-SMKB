Imports Microsoft.AspNet.Identity
Imports System.Globalization
Imports System.Threading
Imports System.Configuration
Imports System.Web.Configuration
Imports System.Collections.Specialized
Imports System.Reflection
Imports System
Imports System.Data.SqlClient
Imports iTextSharp.text

Public Class index
    Inherits System.Web.UI.Page
    Dim Sql As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Session("TukarPTj") = 0
        Dim trackno As Integer = 0

        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")

        ' Stop Caching in IE
        Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache)
        ' Stop Caching in Firefox
        Response.Cache.SetNoStore()

        Try


            'Dim config As Configuration = WebConfigurationManager.OpenWebConfiguration("~/Web.Config")
            'Dim section As SessionStateSection = DirectCast(config.GetSection("system.web/sessionState"), SessionStateSection)
            'Dim intTimeout As Integer = CInt(section.Timeout.TotalMinutes) * 60 * 1000

            'hidTimeOut.Value = intTimeout
            'hidWarn.Value = intTimeout + 10000

            trackno = 6
            If Not Page.IsPostBack Then
                Session("activeReport") = ""


                'For Local 
                'Dim usession As String = Request.QueryString("usession")
                ' Dim iLogin As String = "02634" 'Request.QueryString("usrLogin")
                'Session("ssusrID") = iLogin
                'Session("sessionID") = "0705025542019"


                'For Devmis
                Dim MenuSS As String = Request.QueryString("Menu")
                Dim usession As String = Request.QueryString("usession")
                Dim iLogin As String = Request.QueryString("usrLogin")

                If MenuSS = "" Then
                    If Session("ssusrID") = "" Then
                        Session("ssusrID") = iLogin
                    End If
                End If



                UserInfo.strSesStaffID = Session("ssusrID")
                UserInfo.strSessId = Session("sessionID")
                ' dbconn = New SqlConnection(strCon)

                Using dtUserInfo = fGetUserInfo(Session("ssusrID"))
                    If dtUserInfo.Rows.Count > 0 Then
                        strSesUserName = dtUserInfo.Rows.Item(0).Item("MS01_Nama")
                        strSesUserPost = dtUserInfo.Rows.Item(0).Item("JawGiliran")
                        struserPtj = dtUserInfo.Rows.Item(0).Item("Pejabat")
                        strSesUserKodPtj = dtUserInfo.Rows.Item(0).Item("KodPejabat") & "0000"
                        struserKodBahagian = dtUserInfo.Rows.Item(0).Item("MS08_Bahagian")
                        struserKodUnit = dtUserInfo.Rows.Item(0).Item("MS08_Unit")

                        Session("ssusrName") = strSesUserName
                        Session("ssusrPost") = strSesUserPost
                        Session("ssusrKodBahagian") = struserKodBahagian
                        Session("ssusrKodUnit") = struserKodUnit
                        Session("ssusrKodPTj") = strSesUserKodPtj
                        Session("ssusrPTj") = struserPtj
                        Using dtUserInfo1 = fGetUserCLM(Session("ssusrID"))
                            If dtUserInfo1.Rows.Count > 0 Then
                                Dim strDate As DateTime = dtUserInfo1.Rows.Item(0).Item("CLM_LastLogin")
                                strDateLastLogin = strDate.ToString("dd/MM/yyyy hh:mm:ss tt")
                                Session("ssLastLogin") = strDateLastLogin
                            End If
                        End Using
                    End If
                End Using

                'Dim iLogin As String = "00760"

                dbconnNew = New SqlConnection(strCon)
                dbconnNew.Open()


                Sql = "SELECT no_staf, kod_tahap from SMKB_UTahapDT where STATUS = 1 AND No_Staf  = '" & Session("ssusrID") & "' "
                dbcomm = New SqlCommand(Sql, dbconnNew)
                dbread = dbcomm.ExecuteReader()

                If dbread.HasRows Then
                    Do While dbread.Read()
                        Session("RefTahap") = dbread("kod_tahap")
                    Loop
                End If
                dbread.Close()
                dbcomm = Nothing

                Session("LoggedIn") = True

                '-------- check tahap---
                dbconnNew = New SqlConnection(strCon)
                dbconnNew.Open()

                Sql = "SELECT Kod_Modul, Nama_Modul, icon_location FROM SMKB_Modul WHERE Status = 1 AND Kod_Modul IN (SELECT Kod_Modul FROM SMKB_Sub_Modul
	                    WHERE Kod_Sub 
		                    IN (SELECT Kod_Sub FROM SMKB_Sub_Menu
		                    WHERE KOD_SUB_MENU 
			                    IN (SELECT KOD_SUB_MENU FROM SMKB_UProses_Kump
			                    WHERE Kod_Tahap = '" & Session("RefTahap") & "' AND Status = 1) 
		                    )) ORDER BY Urutan"

                dbcomm = New SqlCommand(Sql, dbconnNew)
                Dim paramSql() As SqlParameter = {
}
                Dim ds As DataSet = NewGetDataPB(Sql, paramSql, dbconnNew)
                If (ds.Tables.Count > 0) Then
                    rptMenu.DataSource = ds
                    rptMenu.DataBind()
                End If

                dbconnNew.Close()
                '---------------
            End If

            UserLogin.status = True

        Catch ex As Exception
            fErrorLog("Page_Load(), Err - " & ex.ToString)
        End Try
    End Sub



    'End Sub
    Protected Sub rptMenu_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        'Dim Modul As String = e.CommandArgument.ToString()
        ''Session("AspPage") = ASPPage
        ''Session("AspPage") = "FORMS/Main.aspx"
        'Session("KodModul") = Modul
        ''If Session("LOGINID") = "" Then
        ''    Session("LOGINID") = Session("ssusrID")
        ''End If


        ''Server.Transfer(Session("AspPage"))
        ''Response.Redirect("Site.Master.aspx")
        'Response.Redirect("~/" & Session("AspPage"))


        'Session("AspPage") = ASPPage
        Dim argument As String = e.CommandArgument.ToString()
        Dim values As String() = argument.Split("|")

        ' Use the individual values as needed
        Session("KodModul") = values(0)
        Session("ssusrID") = values(1)
        Session("Page") = "FORMS/Main.aspx"

        Response.Redirect("~/" & Session("Page"))





    End Sub

    'Protected Async Sub getTokenID()
    '    Dim username As String = Session("ssusrID")
    '    Dim password As String = "King@000"

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
    '        Session("tokenID") = GlobalSMKBAPI.UserLoginDetails._accessToken.AccessToken.ToString()

    '        'errorMessage.InnerText = GlobalSMKBAPI.UserLoginDetails.u_Userid
    '        'MessageBox.Show(Global.UserLoginDetails.u_Userid)
    '    Catch ex As Exception
    '        'errorMessage.InnerText = "Invalid username or password."
    '        'MessageBox.Show("Invalid login or password")
    '        'txtTokenID.Value = ""
    '    End Try


    'End Sub

End Class