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
Imports System.Reflection.Emit

Public Class KO
    Inherits System.Web.UI.Page

    Public dsKod As New DataSet
    Public dvKodKW As New DataView

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

        End Try
    End Function

    Private Function fCreateDtKW() As DataTable
        Dim blnStatus As Boolean
        Dim strStatus As String
        Try

            Dim dt As New DataTable
            dt.Columns.Add("Kod_Operasi", GetType(String))
            dt.Columns.Add("Butiran", GetType(String))
            dt.Columns.Add("Status", GetType(String))

            Dim strKod As String
            Dim strButiran As String

            Dim strSql As String = "select Kod_Operasi, Butiran, Status from SMKB_Operasi order by Kod_Operasi"

            Dim dbconn As New DBKewConn
            dsKod = dbconn.fSelectCommand(strSql)
            dvKodKW = New DataView(dsKod.Tables(0))

            For i As Integer = 0 To dsKod.Tables(0).Rows.Count - 1

                strKod = dsKod.Tables(0).Rows(i)(0).ToString
                strButiran = dsKod.Tables(0).Rows(i)(1).ToString
                blnStatus = dsKod.Tables(0).Rows(i)(2).ToString
                If blnStatus = True Then
                    strStatus = "Aktif"
                Else
                    strStatus = "Tidak Aktif"
                End If

                dt.Rows.Add(strKod, strButiran, strStatus)
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

            strSql = "select count(*) from SMKB_Operasi where Kod_Operasi = '" & strKod & "'"
            If fCheckRec(strSql) = 0 Then

                strSql = "insert into SMKB_Operasi (Kod_Operasi , Butiran, Status) values(@Kod,@Butiran,@Status)"
                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@Kod", strKod),
                    New SqlParameter("@Butiran", strButiran),
                    New SqlParameter("@Status", intStatus)
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
                strSql = "update SMKB_Operasi set Butiran = @Butiran, Status = @Status where Kod_Operasi = @Kod"
                Dim paramSql() As SqlParameter = {
                            New SqlParameter("@Kod", strKod),
                            New SqlParameter("@Butiran", strButiran),
                            New SqlParameter("@Status", intStatus)
                            }
                dbconn.sConnBeginTrans()
                If dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                    dbconn.sConnCommitTrans()
                    'fGlobalAlert("Rekod telah dikemaskini!", Me.Page, Me.[GetType]())
                    lblModalMessaage.Text = "Rekod telah dikemaskini!" 'message di modal
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

                txtKodKW.Value = selectedRow.Cells(0).Text.ToString

                txtButiran.Value = selectedRow.Cells(1).Text.ToString
                'Dim myHiddenField As LinkButton = CType(selectedRow.FindControl("lnkButton"), LinkButton)
                'Dim value As String = myHiddenField.Text
                'txtButiran.Value = value


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

    'Protected Sub lnkbtn_Click(ByVal sender As Object, ByVal e As EventArgs)

    '    Dim lnkbtn As LinkButton = TryCast(sender, LinkButton)
    '    Dim gvrow As GridViewRow = TryCast(lnkbtn.NamingContainer, GridViewRow)
    '    Dim hostelcode As Integer = Convert.ToInt32(gvKod.DataKeys(gvrow.RowIndex).Value.ToString())
    '    Dim butiran As String = gvrow.Cells(1).Text
    '    txtButiran.Value = butiran

    '    ClientScript.RegisterStartupScript([GetType](), "alert", "ShowPopup('1');", True)

    'End Sub


    Private Sub gvKod_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvKod.PageIndexChanging
        gvKod.PageIndex = e.NewPageIndex
        fBindGv()
    End Sub

End Class