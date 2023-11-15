Public Class _Default
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Session("ssusrID") = Trim(Request.QueryString("usrLogin"))
            Session("sessionID") = Trim(Request.QueryString("usession"))

            'Session("sessionID") = "2402025542020"

            'fErrorLog("1 - Request.QueryString('usrLogin') - " & Request.QueryString("usrLogin") & ", Request.QueryString('usession') - " & Request.QueryString("usession"))

            'fErrorLog("2 - Session('ssusrID') - " & Session("ssusrID") & ", Session('sessionID') - " & Session("sessionID"))

            Response.Redirect("/SMKBNet/FORMS/Main.aspx", False)


        End If

    End Sub
End Class