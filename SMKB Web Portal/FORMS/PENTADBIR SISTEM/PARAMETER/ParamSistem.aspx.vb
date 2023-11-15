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

Public Class ParamSistem
    Inherits System.Web.UI.Page

    Public dsKod As New DataSet
    Public dvKodKW As New DataView
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
            dt = fCreateDtKW()

            If dt.Rows.Count = 0 Then
                gvKod.DataSource = New List(Of String)
            Else
                gvKod.DataSource = dt
            End If
            gvKod.DataBind()
            'lblJumRekod.InnerText = dt.Rows.Count


            'Required for jQuery DataTables to work.
            gvKod.UseAccessibleHeader = True
            gvKod.HeaderRow.TableSection = TableRowSection.TableHeader

        Catch ex As Exception
            'ex.Message
        End Try
    End Function

    Private Function fCreateDtKW() As DataTable

        Try

            Dim dt As New DataTable
            dt.Columns.Add("Bil", GetType(String))
            dt.Columns.Add("DATABASE_NAME", GetType(String))
            dt.Columns.Add("SERVER_NAME", GetType(String))
            dt.Columns.Add("TYPE", GetType(String))
            dt.Columns.Add("STATUS", GetType(String))

            Dim Bil As Integer
            Dim strDatabaseName As String
            Dim strServerName As String
            Dim strType As String
            Dim strStatus As String

            Dim strSql As String = $"SELECT SMKB_Param_SistemID, DATABASE_NAME, SERVER_NAME, TYPE, STATUS FROM SMKB_PARAM_SISTEM"


            dsKod = dbconn.fSelectCommand(strSql)
            dvKodKW = New DataView(dsKod.Tables(0))

            For i As Integer = 0 To dsKod.Tables(0).Rows.Count - 1

                Bil = dsKod.Tables(0).Rows(i)(0).ToString
                strDatabaseName = dsKod.Tables(0).Rows(i)(1).ToString
                strServerName = dsKod.Tables(0).Rows(i)(2).ToString
                strType = dsKod.Tables(0).Rows(i)(3).ToString

                Dim status = dsKod.Tables(0).Rows(i)(4).ToString
                If status = True Then
                    strStatus = "Aktif"
                Else
                    strStatus = "Tidak Aktif"
                End If

                dt.Rows.Add(Bil, strDatabaseName, strServerName, strType, strStatus)
            Next

            Return dt

        Catch ex As Exception

        End Try

    End Function

    Private Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.ServerClick
        Dim strSql As String
        Dim dbconn As New DBKewConn
        Try
            Dim strKod As String = Trim(txtSistemID.Value.TrimEnd)
            Dim strDatabaseName As String = Trim(txtDatabaseName.Value.TrimEnd)
            Dim strServerName As String = Trim(txtServerName.Value.ToUpper.TrimEnd)
            Dim strType As String = Trim(txtType.Value.ToUpper.TrimEnd)
            Dim strStatus As String = rblStatus.SelectedValue

            strSql = "SELECT COUNT(*) FROM SMKB_PARAM_SISTEM WHERE SMKB_Param_SistemID =  '" & strKod & "'"
            If fCheckRec(strSql) = 0 Then

                strSql = "insert into SMKB_PARAM_SISTEM (DATABASE_NAME, SERVER_NAME, TYPE, STATUS) 
                    values (@DATABASE_NAME, @SERVER_NAME, @TYPE, @STATUS)"
                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@DATABASE_NAME", strDatabaseName),
                    New SqlParameter("@SERVER_NAME", strServerName),
                    New SqlParameter("@TYPE", strType),
                    New SqlParameter("@STATUS", strStatus)
                }

                dbconn.sConnBeginTrans()
                If dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                    dbconn.sConnCommitTrans()
                    'fGlobalAlert("Rekod baru telah disimpan!", Me.Page, Me.[GetType]())
                    lblModalMessaage.Text = "Rekod baru telah disimpan" 'message di modal
                    ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                    fBindGv()
                    'fReset()
                Else
                    dbconn.sConnRollbackTrans()
                    'fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
                    lblModalMessaage.Text = "Ralat!" 'message di modal
                    ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                End If

                'Else


                'End If

                'ElseIf ViewState("SaveMode") = 2 Then
            Else
                'UPDATE
                strSql = "update SMKB_PARAM_SISTEM set DATABASE_NAME = @DATABASE_NAME, SERVER_NAME = @SERVER_NAME, TYPE = @TYPE, STATUS = @STATUS
                where SMKB_Param_SistemID = '" & strKod & "'"
                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@DATABASE_NAME", strDatabaseName),
                    New SqlParameter("@SERVER_NAME", strServerName),
                    New SqlParameter("@TYPE", strType),
                    New SqlParameter("@STATUS", strStatus)
                            }
                dbconn.sConnBeginTrans()
                If dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                    dbconn.sConnCommitTrans()
                    'fGlobalAlert("Rekod telah dikemaskini!", Me.Page, Me.[GetType]())
                    lblModalMessaage.Text = "Rekod telah dikemaskini." 'message di modal
                    ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                    fBindGv()
                    'fReset()
                Else
                    dbconn.sConnRollbackTrans()
                    'fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
                    lblModalMessaage.Text = "Ralat!" 'message di modal
                    ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub


    Private Sub gvKod_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvKod.RowCommand
        Try
            If e.CommandName = "Select" Then


                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvKod.Rows(index)

                Dim strKod As String = selectedRow.Cells(0).Text.ToString
                txtSistemID.Value = strKod

                'Call sql
                Dim strSql As String = $"SELECT DATABASE_NAME, SERVER_NAME, TYPE, STATUS FROM SMKB_PARAM_SISTEM WHERE SMKB_Param_SistemID = '{strKod}'"

                Dim dt As New DataTable
                dt = dbconn.fSelectCommandDt(strSql)
                If dt.Rows.Count > 0 Then

                    txtDatabaseName.Value = dt.Rows(0)("DATABASE_NAME")
                    txtServerName.Value = dt.Rows(0)("SERVER_NAME")
                    txtType.Value = dt.Rows(0)("TYPE")

                    Dim status = dt.Rows(0)("status")
                    rblStatus.SelectedValue = status

                End If

                ClientScript.RegisterStartupScript([GetType](), "alert", "ShowPopup('1');", True)


            End If
            If e.CommandName = "Hapus" Then
                ClientScript.RegisterStartupScript([GetType](), "alert", "ShowPopup('3');", True)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvKod_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvKod.PageIndexChanging
        gvKod.PageIndex = e.NewPageIndex
        fBindGv()
    End Sub

    'Protected Sub gvKod_RowDeleting(sender As Object, e As GridViewDeleteEventArgs)

    'End Sub


    'Private Sub fBindMasterLookup(kodKlasifikasi As String)
    '    Try
    '        Dim strsql As String
    '        Dim dataCarian = ddlCariJnsVot.SelectedValue

    '        strsql = $"select Master_Reference_code, Description from smkb_lookup_master order by Master_Reference_code"

    '        Dim ds As New DataSet
    '        Dim dt As New DataTable
    '        ds = dbconn.fSelectCommand(strsql)
    '        ' Dim strKod = Trim(ds.Tables(0).Rows(0)("Kod").ToString)

    '        ddlMasterLookup.DataSource = ds
    '        ddlMasterLookup.DataTextField = "Description"
    '        ddlMasterLookup.DataValueField = "Master_Reference_code"
    '        ddlMasterLookup.DataBind()


    '        ddlMasterLookup.Items.Insert(0, New ListItem("- SILA PILIH -", 0))

    '        'If kodKlasifikasi = "" Then
    '        'ddlMasterLookup.SelectedIndex = 0
    '        'Else
    '        ddlMasterLookup.Items.FindByValue(ddlCariJnsVot.SelectedValue).Selected = True
    '        'End If

    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Private Sub fCariJenisVot()
    '    Try
    '        Dim strsql As String


    '        strsql = $"select Master_Reference_code, Description, source_indicator, status, created_by, created_date from smkb_lookup_master order by Master_Reference_code"

    '        Dim ds As New DataSet
    '        Dim dt As New DataTable
    '        ds = dbconn.fSelectCommand(strsql)
    '        'Dim strKod = Trim(ds.Tables(0).Rows(0)("Kod").ToString)

    '        ddlCariJnsVot.DataSource = ds
    '        ddlCariJnsVot.DataTextField = "Description"
    '        ddlCariJnsVot.DataValueField = "Master_Reference_code"
    '        ddlCariJnsVot.DataBind()

    '        ddlCariJnsVot.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
    '        ddlCariJnsVot.SelectedIndex = 0


    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Private Sub lbtnCari_Click(sender As Object, e As EventArgs) Handles lbtnCari.ServerClick
    '    fBindGv()
    'End Sub




End Class