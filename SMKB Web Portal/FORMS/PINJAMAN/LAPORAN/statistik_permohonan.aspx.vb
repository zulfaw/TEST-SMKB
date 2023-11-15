Imports System.Globalization
Imports System.Threading
Imports System.Web.Services.Description

Public Class statistik_permohonan
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' txtTokenID.Value = Session("tokenID")

            'txtTkhTrans.Value = DateTime.Today.ToString("yyyy-MM-dd")
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("ms-MY")


            Dim currentYear As Integer = DateTime.Now.Year
            Dim startYear As Integer = currentYear - 10 ' Change the value as per your requirement
            Dim endYear As Integer = currentYear '+ 10 ' Change the value as per your requirement

            For year As Integer = startYear To endYear
                ddlTahun.Items.Add(New ListItem(year.ToString(), year.ToString()))
                ddlTahun.SelectedValue = year.ToString()
            Next
            ddlTahun.Items.FindByValue(DateTime.Now.Year).Selected = True

        End If
    End Sub
    Private Sub lbtnCari_Click(sender As Object, e As EventArgs) Handles lbtnCari.ServerClick
        Dim tahun As String
        tahun = ddlTahun.SelectedValue

        lblTajukStatistik.Text = "Statistik Permohonan Pinjaman pada Tahun " & tahun

        Session("tahunChart") = tahun
        myIframe.Attributes("src") = "barchart.aspx?tahun ='" & tahun & "'"

    End Sub



End Class