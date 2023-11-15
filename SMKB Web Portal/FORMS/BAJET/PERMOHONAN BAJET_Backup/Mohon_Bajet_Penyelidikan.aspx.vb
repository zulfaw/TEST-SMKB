Public Class Mohon_Bajet_Penyelidikan
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (IsPostBack) Then

            MultiView1.ActiveViewIndex = 0

        End If
    End Sub

    Private Sub btnGoToStep2_Click(sender As Object, e As EventArgs) Handles btnGoToStep2.Click
        MultiView1.ActiveViewIndex = 1
    End Sub

    Protected Sub btnGoToStep3_Click(sender As Object, e As EventArgs) Handles btnGoToStep3.Click
        MultiView1.ActiveViewIndex = 2

        lblFirstName.Text = txtFirstName.Text
        lblLastName.Text = txtLastName.Text
        lblGender.Text = ddlGender.SelectedValue

        lblEmail.Text = txtEmail.Text
        lblMobile.Text = txtMobile.Text
    End Sub

    Protected Sub btnBackToStep1_Click(sender As Object, e As EventArgs) Handles btnBackToStep1.Click
        MultiView1.ActiveViewIndex = 0

    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Response.Redirect("`/Confirmation.aspx")
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        MultiView1.ActiveViewIndex = 1

    End Sub
End Class