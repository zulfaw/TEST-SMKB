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

    Public dsErr_Log As New DataSet
    Public dvErr_Log As New DataView
    Dim dbconn As New DBKewConn
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            'fCariJenisVot()
            'fBindJenisVot("")
            'fBindMasterLookup("")
            fBindGv()
            'calStartDate.Visible = False
            'calEndDate.Visible = False
        End If
    End Sub
    Private Function fBindGv()

        Try

            Dim dt As New DataTable
            dt = fCreateDtGV()

            If dt.Rows.Count = 0 Then
                gvErr_Log.DataSource = New List(Of String)
            Else
                gvErr_Log.DataSource = dt
            End If
            gvErr_Log.DataBind()
            'lblJumRekod.InnerText = dt.Rows.Count


            'Required for jQuery DataTables to work.
            gvErr_Log.UseAccessibleHeader = True
            gvErr_Log.HeaderRow.TableSection = TableRowSection.TableHeader

        Catch ex As Exception
            'ex.Message
        End Try
    End Function

    Private Function fCreateDtGV() As DataTable

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


            dsErr_Log = dbconn.fSelectCommand(strSql)
            dvErr_Log = New DataView(dsErr_Log.Tables(0))

            For i As Integer = 0 To dsErr_Log.Tables(0).Rows.Count - 1

                Err_Date = dsErr_Log.Tables(0).Rows(i)(0).ToString
                Err_Time = dsErr_Log.Tables(0).Rows(i)(1).ToString
                Err_Number = dsErr_Log.Tables(0).Rows(i)(2).ToString
                Err_Dec = dsErr_Log.Tables(0).Rows(i)(3).ToString
                Err_Object = dsErr_Log.Tables(0).Rows(i)(4).ToString
                Err_Form = dsErr_Log.Tables(0).Rows(i)(5).ToString
                Err_Event = dsErr_Log.Tables(0).Rows(i)(6).ToString
                Err_UID = dsErr_Log.Tables(0).Rows(i)(7).ToString
                Err_PcIP = dsErr_Log.Tables(0).Rows(i)(8).ToString
                Err_PcUser = dsErr_Log.Tables(0).Rows(i)(9).ToString
                Err_PcName = dsErr_Log.Tables(0).Rows(i)(10).ToString
                Err_Status = dsErr_Log.Tables(0).Rows(i)(11).ToString

                dt.Rows.Add(Err_Date, Err_Time, Err_Number, Err_Dec, Err_Object, Err_Form, Err_Event, Err_UID, Err_PcIP, Err_PcUser, Err_PcName, Err_Status)
            Next

            Return dt

        Catch ex As Exception

        End Try

    End Function

    'Private Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.ServerClick
    '    Dim strSql As String
    '    Dim dbconn As New DBKewConn
    '    Try
    '        Dim strMasterLookup As String = Trim(txtCode.Value.TrimEnd)
    '        Dim strDescription As String = Trim(txtDescription.Value.ToUpper.TrimEnd)
    '        Dim strsource_indicator As String = Trim(txtsource_indicator.Value.ToUpper.TrimEnd)
    '        Dim intStatus As Integer = "1"
    '        Dim strcreated_by As String = "01662"
    '        Dim strhfc_cd As String = "PKUTEM"




    '        'If ViewState("SaveMode") = 1 Then
    '        'INSERT

    '        strSql = "select count(*) from smkb_lookup_master where Master_Reference_code = '" & strMasterLookup & "'"
    '        If fCheckRec(strSql) = 0 Then

    '            strSql = "insert into smkb_lookup_master (Master_Reference_code, Description, source_indicator, status, created_by, created_date) 
    '                values (@Master_Reference_code, @Description, @source_indicator, @Status, @created_by, GETDATE())"
    '            Dim paramSql() As SqlParameter = {
    '                New SqlParameter("@Master_Reference_code", strMasterLookup),
    '                New SqlParameter("@Description", strDescription),
    '                New SqlParameter("@source_indicator", strsource_indicator),
    '                New SqlParameter("@Status", intStatus),
    '                New SqlParameter("@created_by", strhfc_cd)
    '            }

    '            dbconn.sConnBeginTrans()
    '            If dbconn.fInsertCommand(strSql, paramSql) > 0 Then
    '                dbconn.sConnCommitTrans()
    '                'fGlobalAlert("Rekod baru telah disimpan!", Me.Page, Me.[GetType]())
    '                lblModalMessaage.Text = "Rekod baru telah disimpan" 'message di modal
    '                ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
    '                fBindGv()
    '                'fReset()
    '            Else
    '                dbconn.sConnRollbackTrans()
    '                'fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
    '                lblModalMessaage.Text = "Ralat!" 'message di modal
    '                ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
    '            End If

    '            'Else


    '            'End If

    '            'ElseIf ViewState("SaveMode") = 2 Then
    '        Else
    '            'UPDATE
    '            strSql = "update SMKB_lookup_master set Description = @Description, source_indicator = @source_indicator, status = @Status
    '            where Master_Reference_code = '" & strMasterLookup & "'"
    '            Dim paramSql() As SqlParameter = {
    '                New SqlParameter("@Description", strDescription),
    '                New SqlParameter("@source_indicator", strsource_indicator),
    '                New SqlParameter("@Status", intStatus)
    '                        }
    '            dbconn.sConnBeginTrans()
    '            If dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
    '                dbconn.sConnCommitTrans()
    '                'fGlobalAlert("Rekod telah dikemaskini!", Me.Page, Me.[GetType]())
    '                lblModalMessaage.Text = "Rekod telah dikemaskini." 'message di modal
    '                ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
    '                fBindGv()
    '                'fReset()
    '            Else
    '                dbconn.sConnRollbackTrans()
    '                'fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
    '                lblModalMessaage.Text = "Ralat!" 'message di modal
    '                ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
    '            End If
    '        End If

    '    Catch ex As Exception

    '    End Try
    'End Sub


    'Private Sub gvErr_Log_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvErr_Log.RowCommand
    '    Try
    '        If e.CommandName = "Select" Then


    '            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
    '            Dim selectedRow As GridViewRow = gvErr_Log.Rows(index)

    '            Dim strKod As String = selectedRow.Cells(0).Text.ToString
    '            'txtDescription.Value = selectedRow.Cells(1).Text.ToString


    '            'Call sql
    '            Dim strSql As String = $"select Master_Reference_code, Description, source_indicator, status, created_by, created_date 
    '                        from smkb_lookup_master              
    '                        where Master_Reference_code = '{strKod}'"

    '            Dim dt As New DataTable
    '            dt = dbconn.fSelectCommandDt(strSql)
    '            If dt.Rows.Count > 0 Then

    '                txtCode.Value = dt.Rows(0)("Master_Reference_code")
    '                txtDescription.Value = dt.Rows(0)("Description")
    '                txtsource_indicator.Value = dt.Rows(0)("source_indicator")

    '                Dim status = dt.Rows(0)("status")
    '                rblStatus.SelectedValue = status

    '            End If

    '            ClientScript.RegisterStartupScript([GetType](), "alert", "ShowPopup('1');", True)


    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub gvErr_Log_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvErr_Log.PageIndexChanging
        gvErr_Log.PageIndex = e.NewPageIndex
        fBindGv()
    End Sub





End Class