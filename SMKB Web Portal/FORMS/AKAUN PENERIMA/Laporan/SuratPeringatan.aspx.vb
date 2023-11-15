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
Imports System.Runtime.InteropServices.ComTypes

Public Class SuratPeringatan
    Inherits System.Web.UI.Page
    Dim dbconn As New DBKewConn

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fCariPenghutang()
        End If
    End Sub
    Protected Sub idModul_SelectedIndexChanged(sender As Object, e As EventArgs)
        If ddlPenghutang.SelectedValue <> "0" Then
            fNoBil()

            BilSection.Visible = True
            btnSearch.Visible = True
        End If
    End Sub

    Private Sub fNoBil()
        Try
            Dim strsql As String

            strsql = $"SELECT No_Bil as bil FROM SMKB_Bil_Hdr A
                        WHERE Kod_Penghutang='" & ddlPenghutang.SelectedValue & "'"

            Dim ds As New DataSet
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strsql)

            ddlBil.DataSource = ds
            ddlBil.DataTextField = "bil"
            ddlBil.DataValueField = "bil"
            ddlBil.DataBind()

            ddlBil.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlBil.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fCariPenghutang()
        Try
            Dim strsql As String


            strsql = $"SELECT DISTINCT B.Kod_Penghutang + ' - ' + A.Nama_Penghutang AS kod_nama, B.Kod_Penghutang as kod,A.Nama_Penghutang as nama FROM SMKB_Penghutang_Master A
                        INNER JOIN SMKB_Bil_Hdr B ON A.Kod_Penghutang=B.Kod_Penghutang"

            Dim ds As New DataSet
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strsql)

            ddlPenghutang.DataSource = ds
            ddlPenghutang.DataTextField = "kod_nama"
            ddlPenghutang.DataValueField = "kod"
            ddlPenghutang.DataBind()

            ddlPenghutang.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlPenghutang.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        ' Get the selected value of ddlBil
        Dim nobil As String = ddlBil.SelectedValue
        Dim kod As String = ddlPenghutang.SelectedValue

        If Not String.IsNullOrEmpty(nobil) AndAlso nobil <> "0" Then
            ' Redirect to CetakSuratPeringatan page with nobil as a query parameter
            Dim params As String = "scrollbars=no,resizable=no,status=no,location=no,toolbar=no,menubar=no,width=0,height=0,left=-1000,top=-1000"
            Dim url As String = $"{ResolveClientUrl("~/FORMS/AKAUN PENERIMA/Laporan/CetakSuratPeringatan.aspx")}?nobil={nobil}&kod={kod}"
            ClientScript.RegisterStartupScript(Me.GetType(), "OpenWindow", $"<script>window.open('{url}', '_blank', '{params}');</script>")
        Else
            ' Handle the case where nobil is empty or zero
            ClientScript.RegisterStartupScript(Me.GetType(), "ShowAlert", "<script>alert('Please select a valid No. Bil');</script>")
        End If
    End Sub


End Class