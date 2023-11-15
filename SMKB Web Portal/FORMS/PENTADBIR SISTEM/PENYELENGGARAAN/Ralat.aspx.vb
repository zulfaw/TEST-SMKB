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
Imports System.Diagnostics.Eventing.Reader

Public Class Ralat
    Inherits System.Web.UI.Page

    Public dsErr_log As New DataSet
    Public dvErr_log As New DataView
    Dim dbconn As New DBKewConn
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fBindGv()

        End If
    End Sub
    Private Function fBindGv()

        Try

            Dim dt As New DataTable
            dt = fCreateDtMaklumatKorporat()

            If dt.Rows.Count = 0 Then
                gvErr_log.DataSource = New List(Of String)
            Else
                gvErr_log.DataSource = dt
            End If
            gvErr_log.DataBind()

            'Required for jQuery DataTables to work.
            gvErr_log.UseAccessibleHeader = True
            gvErr_log.HeaderRow.TableSection = TableRowSection.TableHeader

        Catch ex As Exception
            'ex.Message
        End Try
    End Function

    Private Function fCreateDtMaklumatKorporat() As DataTable

        Try

            Dim dt As New DataTable
            dt.Columns.Add("Err_Date", GetType(String))
            dt.Columns.Add("Err_Time", GetType(String))
            dt.Columns.Add("Err_Number", GetType(String))
            dt.Columns.Add("Err_Dec", GetType(String))
            dt.Columns.Add("Err_Object", GetType(String))
            dt.Columns.Add("Err_Form", GetType(String))
            dt.Columns.Add("Err_Event", GetType(String))
            dt.Columns.Add("Err_UID", GetType(String))
            dt.Columns.Add("Err_PcIP", GetType(String))
            dt.Columns.Add("Err_PcUser", GetType(String))
            dt.Columns.Add("Err_PcName", GetType(String))
            dt.Columns.Add("Err_Status", GetType(String))

            Dim Err_Date As String
            Dim Err_Time As String
            Dim Err_Number As String
            Dim Err_Dec As String
            Dim Err_Object As String
            Dim Err_Form As String
            Dim Err_Event As String
            Dim Err_UID As String
            Dim Err_PcIP As String
            Dim Err_PcUser As String
            Dim Err_PcName As String
            Dim Err_Status As String

            Dim strSql As String = $"select top 500 Err_Date, Err_Time, Err_Number, Err_Dec, isnull(Err_Object,'-') Err_Object, Err_Form, Err_Event, Err_UID, Err_PcIP, Err_PcUser, Err_PcName, Err_Status
                            from SMKB_Err_Log
                            order by Err_Date desc"


            dsErr_log = dbconn.fSelectCommand(strSql)
            dvErr_log = New DataView(dsErr_log.Tables(0))

            For i As Integer = 0 To dsErr_log.Tables(0).Rows.Count - 1

                Err_Date = dsErr_log.Tables(0).Rows(i)(0).ToString
                Err_Time = dsErr_log.Tables(0).Rows(i)(1).ToString
                Err_Number = dsErr_log.Tables(0).Rows(i)(2).ToString
                Err_Dec = dsErr_log.Tables(0).Rows(i)(3).ToString
                Err_Object = dsErr_log.Tables(0).Rows(i)(4).ToString
                Err_Form = dsErr_log.Tables(0).Rows(i)(5).ToString
                Err_Event = dsErr_log.Tables(0).Rows(i)(6).ToString
                Err_UID = dsErr_log.Tables(0).Rows(i)(7).ToString
                Err_PcIP = dsErr_log.Tables(0).Rows(i)(8).ToString
                Err_PcUser = dsErr_log.Tables(0).Rows(i)(9).ToString
                Err_PcName = dsErr_log.Tables(0).Rows(i)(10).ToString
                Err_Status = dsErr_log.Tables(0).Rows(i)(11).ToString

                dt.Rows.Add(Err_Date, Err_Time, Err_Number, Err_Dec, Err_Object, Err_Form, Err_Event, Err_UID, Err_PcIP, Err_PcUser, Err_PcName, Err_Status)
            Next

            Return dt

        Catch ex As Exception

        End Try

    End Function


    Private Sub gvErr_log_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvErr_log.RowCommand
        Try
            If e.CommandName = "Select" Then

                lblModalMessaage.Text = "Ralat!" 'message di modal
                ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvErr_log_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvErr_log.PageIndexChanging
        gvErr_log.PageIndex = e.NewPageIndex
        fBindGv()
    End Sub



End Class