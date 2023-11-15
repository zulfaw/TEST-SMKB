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

Public Class DaftarTahap
    Inherits System.Web.UI.Page

    Public dsTahap As New DataSet
    Public dvTahap As New DataView
    Dim dbconn As New DBKewConn
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            fBindGv()
        End If
    End Sub
    Private Function fBindGv()

        Try

            Dim dt As New DataTable
            dt = fCreateDtTahap()

            If dt.Rows.Count = 0 Then
                gvTahap.DataSource = New List(Of String)
            Else
                gvTahap.DataSource = dt
            End If
            gvTahap.DataBind()

            'Required for jQuery DataTables to work.
            gvTahap.UseAccessibleHeader = True
            gvTahap.HeaderRow.TableSection = TableRowSection.TableHeader

        Catch ex As Exception
            'ex.Message
        End Try
    End Function

    Private Function fCreateDtTahap() As DataTable

        Try

            Dim dt As New DataTable
            dt.Columns.Add("Kod_Tahap", GetType(String))
            dt.Columns.Add("Jen_Tahap", GetType(String))
            dt.Columns.Add("Butiran", GetType(String))
            dt.Columns.Add("status", GetType(String))

            Dim strKod_Tahap As String
            Dim strJen_Tahap As String
            Dim strButiran As String
            Dim strStatus As String

            Dim strSql As String = $"select Kod_Tahap, Jen_Tahap, Butiran, Status from SMKB_UTahap"


            dsTahap = dbconn.fSelectCommand(strSql)
            dvTahap = New DataView(dsTahap.Tables(0))

            For i As Integer = 0 To dsTahap.Tables(0).Rows.Count - 1

                strKod_Tahap = dsTahap.Tables(0).Rows(i)(0).ToString
                strJen_Tahap = dsTahap.Tables(0).Rows(i)(1).ToString
                strButiran = dsTahap.Tables(0).Rows(i)(2).ToString

                Dim status = dsTahap.Tables(0).Rows(i)(3).ToString
                If status = 1 Then
                    strStatus = "Aktif"
                Else
                    strStatus = "Tidak Aktif"
                End If

                dt.Rows.Add(strKod_Tahap, strJen_Tahap, strButiran, StrStatus)
            Next

            Return dt

        Catch ex As Exception

        End Try

    End Function

    Private Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.ServerClick
        Dim strSql As String
        Dim dbconn As New DBKewConn
        Try
            Dim strKod_Tahap As String = Trim(txtKodTahap.Value.TrimEnd)
            Dim strJen_Tahap As String = Trim(txtJenTahap.Value.ToUpper.TrimEnd)
            Dim strButiran As String = Trim(txtButiran.Value.ToUpper.TrimEnd)
            Dim strStatus As String = rblStatus.SelectedValue

            If hdnProc.Value = "insert" Then
                strSql = "select count(*) from SMKB_UTahap where Kod_Tahap = '" & strKod_Tahap & "'"
                If fCheckRec(strSql) > 0 Then
                    lblModalMessaage.Text = "Rekod telah wujud" 'message di modal
                    ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                    hdnProc.Value = ""
                    Exit Sub
                End If

                strSql = "insert into SMKB_UTahap (Kod_Tahap, Jen_Tahap, Butiran, Status) 
                    values (@Kod_Tahap, @Jen_Tahap, @Butiran, @Status)"
                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@Kod_Tahap", strKod_Tahap),
                    New SqlParameter("@Jen_Tahap", strJen_Tahap),
                    New SqlParameter("@Butiran", strButiran),
                    New SqlParameter("@Status", strStatus)
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

            ElseIf hdnProc.Value = "update" Then
                'UPDATE
                strSql = "update SMKB_UTahap set Jen_Tahap = @Jen_Tahap, Butiran = @Butiran, status = @Status
                where Kod_Tahap = '" & strKod_Tahap & "'"
                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@Jen_Tahap", strJen_Tahap),
                    New SqlParameter("@Butiran", strButiran),
                    New SqlParameter("@Status", strStatus)
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

            'If ViewState("SaveMode") = 1 Then
            'INSERT
            hdnProc.Value = ""

        Catch ex As Exception

        End Try
    End Sub


    Private Sub gvTahap_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvTahap.RowCommand
        Try
            If e.CommandName = "Select" Then


                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvTahap.Rows(index)

                Dim strKod As String = selectedRow.Cells(0).Text.ToString
                'txtDescription.Value = selectedRow.Cells(1).Text.ToString


                'Call sql
                Dim strSql As String = $"select Kod_Tahap, Jen_Tahap, Butiran, Status from SMKB_UTahap              
                            where Kod_Tahap = '{strKod}'"

                Dim dt As New DataTable
                dt = dbconn.fSelectCommandDt(strSql)
                If dt.Rows.Count > 0 Then

                    txtKodTahap.Value = dt.Rows(0)("Kod_Tahap")
                    txtJenTahap.Value = dt.Rows(0)("Jen_Tahap")
                    txtButiran.Value = dt.Rows(0)("Butiran")

                    Dim status = dt.Rows(0)("status")
                    rblStatus.SelectedValue = status

                End If

                ClientScript.RegisterStartupScript([GetType](), "alert", "ShowPopup('1');", True)


            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvTahap_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvTahap.PageIndexChanging
        gvTahap.PageIndex = e.NewPageIndex
        fBindGv()
    End Sub





End Class