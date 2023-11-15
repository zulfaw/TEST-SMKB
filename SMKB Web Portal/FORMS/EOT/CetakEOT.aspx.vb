﻿Imports System
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Collections
Imports System.Web.HttpFileCollection
Imports Microsoft.Office.Interop.Excel


Public Class CetakEOT
    Inherits System.Web.UI.Page
    Dim dbconn As New DBKewConn
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim nostaf As String = Request.QueryString("txtNoStaf")
        Dim nomohon As String = Request.QueryString("txtNoMohon")

        If nomohon IsNot Nothing Then
            fBindTransaksi(nostaf)
            fBindTransaksi_Details(nomohon)
        End If
    End Sub
     Private Sub fBindTransaksi(nostaf As String)
        If nostaf <> "" Then
            Dim strSql As String

                If Not String.IsNullOrEmpty(nostaf) Then
                    Using dtUserInfo = fGetUserInfo(nostaf)
                        If dtUserInfo.Rows.Count > 0 Then
                            Dim strPejabat = dtUserInfo.Rows.Item(0).Item("PEJABAT")
                            Dim strKodPejabat = dtUserInfo.Rows.Item(0).Item("KodPejabat")
                            Dim strKetuaPej As String = ""
                            txtPTJ.Text = strPejabat
                            txtKetuaPej.Text = getKJ(strKodPejabat)
                            strKetuaPej = txtKetuaPej.Text
                            hidketuaPTJ.Value = strKetuaPej.Substring(0, 5)
                            fnPengesah(strKodPejabat)
                            ViewState("KodPejabat") = strKodPejabat
                        End If
                    End Using
                End If

        End If
    End Sub

    Private Sub fBindTransaksi_Details(nomohon As String)
        If bil <> "" Then
            Dim strSql As String
            strSql = $"select a.No_Bil,Perkara ,Kuantiti, Kadar_Harga, Jumlah, Diskaun, Cukai
                                from SMKB_Bil_Dtl as a
                                where No_Bil = '{bil}'
                                and status = 1
                                order by No_Item"
            Dim ds = dbconn.fSelectCommand(strSql)
            If ds IsNot Nothing Then
                Dim dt = ds.Tables(0)

                gvTransaksi.DataSource = dt
                gvTransaksi.DataBind()

            End If
        End If
    End Sub
End Class