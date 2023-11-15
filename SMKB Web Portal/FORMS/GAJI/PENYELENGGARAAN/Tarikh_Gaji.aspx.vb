Imports System
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Net.NetworkInformation
Public Class Tarikh_Gaji
    Inherits System.Web.UI.Page
    Dim dbconn As New DBKewConn
    Dim dbconnSM As New DBSMConn
    Public dsKod As New DataSet
    Public dvKodKW As New DataView
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            fBindGvJenis()

        End If
    End Sub
    Private Sub fBindGvJenis()


        Try
            Dim dt As New DataTable
            dt = fCreateDtJenis()

            If dt.Rows.Count = 0 Then
                gvJenis.DataSource = New List(Of String)
            Else
                gvJenis.DataSource = dt
            End If
            gvJenis.DataBind()
            'lblJumRekod.InnerText = dt.Rows.Count


            'Required for jQuery DataTables to work.
            gvJenis.UseAccessibleHeader = True
            gvJenis.HeaderRow.TableSection = TableRowSection.TableHeader

        Catch ex As Exception

        End Try
    End Sub
    Private Function fCreateDtJenis() As DataTable

        Try

            Dim dt As New DataTable
            dt.Columns.Add("Kod_Param", GetType(String))
            dt.Columns.Add("Tarikh_Byr_Gaji", GetType(String))
            dt.Columns.Add("status", GetType(String))


            Dim kodparam As String
            Dim tkhgaji As String
            Dim stagaji As String



            Dim strSql As String = "SELECT Kod_Param,Tarikh_Byr_Gaji,status from smkb_gaji_Tarikh_Gaji order by Kod_Param"
            Dim dbconn As New DBKewConn
            dsKod = dbconn.fSelectCommand(strSql)
            dvKodKW = New DataView(dsKod.Tables(0))

            For i As Integer = 0 To dsKod.Tables(0).Rows.Count - 1

                kodparam = dsKod.Tables(0).Rows(i)(0).ToString
                tkhgaji = dsKod.Tables(0).Rows(i)(1).ToString
                stagaji = dsKod.Tables(0).Rows(i)(2).ToString

                dt.Rows.Add(kodparam, tkhgaji, stagaji)
            Next

            Return dt

        Catch ex As Exception

        End Try

    End Function
End Class