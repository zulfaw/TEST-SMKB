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

Public Class Daftar_Kod_Terima
    Inherits System.Web.UI.Page

    Public dsKodKW As New DataSet
    Public dvKodKW As New DataView

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fBindGvKW()
        End If
    End Sub



    Private Function fBindGvKW()


        Try
            Dim dt As New DataTable
            dt = fCreateDtKW()

            If dt.Rows.Count = 0 Then
                gvKodKW.DataSource = New List(Of String)
            Else
                gvKodKW.DataSource = dt
            End If
            gvKodKW.DataBind()
            'lblJumRekod.InnerText = dt.Rows.Count


            'Required for jQuery DataTables to work.
            gvKodKW.UseAccessibleHeader = True
            gvKodKW.HeaderRow.TableSection = TableRowSection.TableHeader

        Catch ex As Exception

        End Try
    End Function

    Private Function fCreateDtKW() As DataTable
        Dim blnStatus As Boolean
        Dim strStatus As String
        Try

            Dim dt As New DataTable
            dt.Columns.Add("Kod_Terima", GetType(String))
            dt.Columns.Add("Butiran", GetType(String))
            dt.Columns.Add("Status", GetType(String))

            Dim strKod As String
            Dim strButiranKW As String

            Dim strSql As String = "SELECT Kod_Terima, Butiran, Status FROM SMKB_Kod_Terima ORDER BY Kod_Terima"

            Dim dbconn As New DBKewConn
            dsKodKW = dbconn.fSelectCommand(strSql)
            dvKodKW = New DataView(dsKodKW.Tables(0))

            For i As Integer = 0 To dsKodKW.Tables(0).Rows.Count - 1

                strKod = dsKodKW.Tables(0).Rows(i)(0).ToString
                strButiranKW = dsKodKW.Tables(0).Rows(i)(1).ToString
                blnStatus = dsKodKW.Tables(0).Rows(i)(2).ToString
                If blnStatus = True Then
                    strStatus = "Aktif"
                Else
                    strStatus = "Tidak Aktif"
                End If
                dt.Rows.Add(strKod, strButiranKW, strStatus)
            Next

            Return dt

        Catch ex As Exception

        End Try

    End Function

    Private Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.ServerClick
        Dim strSql As String
        Dim dbconn As New DBKewConn
        Try
            Dim strKod As String = Trim(txtKodKW.Value.TrimEnd)
            Dim strButiran As String = Trim(txtButiran.Value.ToUpper.TrimEnd)
            Dim intStatus As Integer = "1"



            'If ViewState("SaveMode") = 1 Then
            'INSERT

            strSql = "select count(*) from SMKB_Kod_Terima where Kod_Terima = '" & strKod & "'"
            If fCheckRec(strSql) = 0 Then

                strSql = "insert into SMKB_Kod_Terima (Kod_Terima , Butiran, Status) values(@KodKW,@Butiran,@Status)"
                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@KodKW", strKod),
                    New SqlParameter("@Butiran", strButiran),
                    New SqlParameter("@Status", intStatus)
                }

                dbconn.sConnBeginTrans()
                If dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                    dbconn.sConnCommitTrans()
                    'fGlobalAlert("Rekod baru telah disimpan!", Me.Page, Me.[GetType]())
                    fBindGvKW()
                    'fReset()
                    lblModalMessaage.Text = "Rekod baru telah disimpan." 'message di modal
                    ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
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
                strSql = "update SMKB_Kod_Terima set Butiran = @Butiran, Status = @Status where Kod_Terima = @KodKw"
                Dim paramSql() As SqlParameter = {
                            New SqlParameter("@KodKW", strKod),
                            New SqlParameter("@Butiran", strButiran),
                            New SqlParameter("@Status", intStatus)
                            }
                dbconn.sConnBeginTrans()
                If dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                    dbconn.sConnCommitTrans()
                    'fGlobalAlert("Rekod telah dikemaskini!", Me.Page, Me.[GetType]())
                    fBindGvKW()
                    'fReset()
                    lblModalMessaage.Text = "Rekod telah dikemaskini." 'message di modal
                    ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                Else
                    dbconn.sConnRollbackTrans()
                    'fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
                    lblModalMessaage.Text = "Ralat!" 'message di modal
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub


    Private Sub gvKodKW_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvKodKW.RowCommand
        Try
            If e.CommandName = "Select" Then


                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvKodKW.Rows(index)

                txtKodKW.Value = selectedRow.Cells(0).Text.ToString
                txtButiran.Value = selectedRow.Cells(1).Text.ToString

                Dim statusAktif = selectedRow.Cells(3).Text.ToString
                If statusAktif = "Aktif" Then
                    statusAktif = 1
                Else
                    statusAktif = 0
                End If
                rblStatus.SelectedValue = statusAktif

                ClientScript.RegisterStartupScript([GetType](), "alert", "ShowPopup('1');", True)


            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvKodKW_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvKodKW.PageIndexChanging
        gvKodKW.PageIndex = e.NewPageIndex
        fBindGvKW()
    End Sub

End Class