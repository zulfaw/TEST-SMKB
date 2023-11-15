Imports System
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Diagnostics.Eventing.Reader
Imports System.Web.UI.HtmlControls

Public Class reportpnldetail
    Inherits System.Web.UI.Page
    Dim listBulan As String() = {"Januari", "Februari", "Mac", "April", "Mei", "Jun", "Julai", "Ogos", "September", "Oktober", "November", "Disember"}
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            tahun.Text = Session("tahun")
            Label1.Text = Session("tahun")
            Label2.Text = Session("tahun")
            bulan.Text = listBulan(Session("bulan") - 1)
        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub
End Class