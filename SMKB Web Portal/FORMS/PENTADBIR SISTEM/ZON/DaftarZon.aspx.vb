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

Public Class DaftarZon
    Inherits System.Web.UI.Page

    Public dsZon As New DataSet
    Public dvZon As New DataView
    Dim dbconn As New DBKewConn
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fBindGv()

        End If
    End Sub
    Private Function fBindGv()

        Try

            Dim dt As New DataTable
            dt = fCreateDtKW()

            If dt.Rows.Count = 0 Then
                gvZon.DataSource = New List(Of String)
            Else
                gvZon.DataSource = dt
            End If
            gvZon.DataBind()

            'Required for jQuery DataTables to work.
            gvZon.UseAccessibleHeader = True
            gvZon.HeaderRow.TableSection = TableRowSection.TableHeader

        Catch ex As Exception
            'ex.Message
        End Try
    End Function

    Private Function fCreateDtKW() As DataTable

        Try

            Dim dt As New DataTable
            dt.Columns.Add("kod_zon", GetType(String))
            dt.Columns.Add("butiran", GetType(String))
            dt.Columns.Add("status", GetType(String))

            Dim kod_zon As String
            Dim butiran As String
            Dim status As String

            Dim strSql As String = $"select kod_zon, butiran, status from SMKB_Zon order by kod_zon"

            dsZon = dbconn.fSelectCommand(strSql)
            dvZon = New DataView(dsZon.Tables(0))

            For i As Integer = 0 To dsZon.Tables(0).Rows.Count - 1

                kod_zon = dsZon.Tables(0).Rows(i)(0).ToString
                butiran = dsZon.Tables(0).Rows(i)(1).ToString

                Dim StrStatus = dsZon.Tables(0).Rows(i)(2).ToString
                If StrStatus = 1 Then
                    status = "Aktif"
                Else
                    status = "Tidak Aktif"
                End If

                dt.Rows.Add(kod_zon, butiran, status)
            Next

            Return dt

        Catch ex As Exception

        End Try

    End Function

    Private Sub gvZon_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvZon.RowCommand
        Try
            If e.CommandName = "Select" Then


                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvZon.Rows(index)

                Dim strKod As String = selectedRow.Cells(0).Text.ToString
                'txtDescription.Value = selectedRow.Cells(1).Text.ToString


                'Call sql
                Dim strSql As String = $"select kod_zon, butiran, status from SMKB_Zon where kod_zon = '{strKod}'"

                Dim dt As New DataTable
                dt = dbconn.fSelectCommandDt(strSql)
                If dt.Rows.Count > 0 Then

                    txtKodZon.Value = dt.Rows(0)("kod_zon")
                    txtButiran.Value = dt.Rows(0)("butiran")

                    Dim status = dt.Rows(0)("status")
                    rblStatus.SelectedValue = status

                End If

                ClientScript.RegisterStartupScript([GetType](), "alert", "ShowPopup('1');", True)


            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvZon_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvZon.PageIndexChanging
        gvZon.PageIndex = e.NewPageIndex
        fBindGv()
    End Sub


    Private Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.ServerClick
        Dim strSql As String
        Dim dbconn As New DBKewConn
        Try
            Dim strKod_Zon As String = Trim(txtKodZon.Value.TrimEnd)
            Dim strButiran As String = Trim(txtButiran.Value.TrimEnd)
            Dim strStatus As String = Trim(rblStatus.SelectedValue)


            'If ViewState("SaveMode") = 1 Then
            'INSERT

            strSql = "select count(*) from SMKB_Zon where kod_zon = '" & strKod_Zon & "'"
            If fCheckRec(strSql) = 0 Then

                strSql = "insert into SMKB_Zon( kod_zon, butiran, status) 
                    values (@Kod_Zon, @Butiran, @Status)"
                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@Kod_Modul", strKod_Zon),
                    New SqlParameter("@Butiran", strButiran),
                    New SqlParameter("@Status", strStatus)
                }

                dbconn.sConnBeginTrans()
                If dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                    dbconn.sConnCommitTrans()

                    lblModalMessaage.Text = "Rekod baru telah disimpan" 'message di modal
                    ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                    fBindGv()
                Else
                    dbconn.sConnRollbackTrans()
                    lblModalMessaage.Text = "Ralat!" 'message di modal
                    ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                End If

            Else
                'UPDATE
                strSql = "update SMKB_Zon set butiran = @Butiran, status = @Status
                where Kod_Zon = '" & strKod_Zon & "'"
                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@Butiran", strButiran),
                    New SqlParameter("@Status", strStatus)
                            }
                dbconn.sConnBeginTrans()
                If dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                    dbconn.sConnCommitTrans()
                    lblModalMessaage.Text = "Rekod telah dikemaskini." 'message di modal
                    ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                    fBindGv()
                    'fReset()
                Else
                    dbconn.sConnRollbackTrans()
                    lblModalMessaage.Text = "Ralat!" 'message di modal
                    ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub



End Class