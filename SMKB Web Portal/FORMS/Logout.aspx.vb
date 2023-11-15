Public Class Logout
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Session("SmkbMessage") = "" Then
                lblMsg.Text = "Sesi Tamat. Sila login semula di i@UteM"
            Else
                lblMsg.Text = Session("SmkbMessage")
            End If

            Session("LoggedIn") = False
            Session.Clear()
            Session.Abandon()
            Session.RemoveAll()
            Response.Buffer = True
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1D)
            Response.Expires = 0
            Response.CacheControl = "no-cache"
            'Request.QueryString.Remove("usrLogin")
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            'strSesStaffID = ""

        Catch ex As Exception

        End Try

    End Sub

End Class