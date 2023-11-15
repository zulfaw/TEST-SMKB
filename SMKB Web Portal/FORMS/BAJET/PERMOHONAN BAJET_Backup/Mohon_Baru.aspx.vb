Imports System.Drawing

Public Class Mohon_Baru
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub OnBtnMohonBaru(sender As Object, e As EventArgs) Handles btnMohonBaru.Click

    End Sub

    Protected Sub btnHantar_Click(sender As Object, e As EventArgs) Handles btnHantar.Click

    End Sub
End Class