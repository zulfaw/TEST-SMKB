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

Public Class TetapanZonPTj
    Inherits System.Web.UI.Page

    Public dsZon As New DataSet
    Public dvKodZon As New DataView
    Dim dbconn As New DBKewConn
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fBindZon("")
            fBindGv()
            fBindZonEntry("")
            fBindPTjEntry("")

        End If
    End Sub


    Private Function fBindGv()

        Try

            Dim dt As New DataTable
            dt = fCreateDtZon()

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
            Dim errorMessage As String = "An error occurred: " & ex.Message
            Response.Write(errorMessage)
        End Try
    End Function

    Private Function fCreateDtZon() As DataTable

        Try

            Dim dt As New DataTable
            dt.Columns.Add("Kod_Zon", GetType(String))
            dt.Columns.Add("Kod_PTJ", GetType(String))
            dt.Columns.Add("namaPTj", GetType(String))
            dt.Columns.Add("Status", GetType(String))

            Dim Kod_Zon As String
            Dim Kod_PTJ As String
            Dim namaPTj As String
            Dim status As String

            Dim dataCarian = ddlZon.SelectedValue

            Dim strSql As String = $"select a.Kod_Zon, a.Kod_PTJ, b.singkatan as namaPTj, a.Status
                from SMKB_Zon_Ptj as a, [devmis\sql_ins01].dbstaf.dbo.MS_pejabat as b
                where b.kodPejabat = substring(a.Kod_PTJ,1,2) and a.Kod_Zon = '" & dataCarian & "'"


            dsZon = dbconn.fSelectCommand(strSql)
            dvKodZon = New DataView(dsZon.Tables(0))

            For i As Integer = 0 To dsZon.Tables(0).Rows.Count - 1

                Kod_Zon = dsZon.Tables(0).Rows(i)(0).ToString
                Kod_PTJ = dsZon.Tables(0).Rows(i)(1).ToString
                namaPTj = dsZon.Tables(0).Rows(i)(2).ToString

                Dim StrStatus = dsZon.Tables(0).Rows(i)(3).ToString
                If StrStatus = 1 Then
                    status = "Aktif"
                Else
                    status = "Tidak Aktif"
                End If

                dt.Rows.Add(Kod_Zon, Kod_PTJ, namaPTj, status)
            Next

            Return dt

        Catch ex As Exception
            Dim errorMessage As String = "An error occurred: " & ex.Message
            Response.Write(errorMessage)
        End Try

    End Function

    Private Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.ServerClick
        Dim strSql As String
        Dim dbconn As New DBKewConn
        Try
            Dim strKodZon As String = ddlZonEntry.SelectedValue
            Dim strKodPTj As String = ddlPTjEntry.SelectedValue
            Dim intStatus As String = Trim(rblStatus.SelectedValue)

            'INSERT
            strSql = "select count(*) from SMKB_Zon_Ptj where Kod_Zon = '" & strKodZon & "' and Kod_PTJ ='" & strKodPTj & "'"
            If fCheckRec(strSql) = 0 Then

                strSql = "insert into SMKB_Zon_Ptj (Kod_Zon, Kod_PTJ, Status) 
                    values (@Kod_Zon, @Kod_PTJ, @Status)"
                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@Kod_Zon", strKodZon),
                    New SqlParameter("@Kod_PTJ", strKodPTj),
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
                strSql = "update SMKB_Zon_Ptj set Kod_Zon = @Kod_Zon, Kod_PTJ = @Kod_PTJ, Status = @Status
                where Kod_Zon = '" & strKodZon & "' and Kod_PTJ ='" & strKodPTj & "'"
                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@Kod_Zon", strKodZon),
                    New SqlParameter("@Kod_PTJ", strKodPTj),
                    New SqlParameter("@Status", intStatus)
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

                Dim strZon As String = selectedRow.Cells(0).Text.ToString
                Dim strKodPtj As String = selectedRow.Cells(1).Text.ToString


                'Call sql
                Dim strSql As String = $"select Kod_Zon, Kod_PTJ, Status from SMKB_Zon_Ptj where Kod_Zon = '{strZon}' and Kod_PTJ = '{strKodPtj}'"

                Dim dt As New DataTable
                dt = dbconn.fSelectCommandDt(strSql)
                If dt.Rows.Count > 0 Then
                    fBindZonEntry(strZon)

                    fBindPTjEntry(dt.Rows(0)("Kod_PTJ"))
                    ddlZonEntry.Enabled = False
                    Dim status = dt.Rows(0)("status")
                    rblStatus.SelectedValue = status

                End If


                ClientScript.RegisterStartupScript([GetType](), "alert", "ShowPopup('1');", True)


            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvKod_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvKod.PageIndexChanging
        gvKod.PageIndex = e.NewPageIndex
        fBindGv()
    End Sub

    Private Sub fBindZonEntry(kodKlasifikasi As String)
        Try
            Dim strsql As String
            'Dim dataCarian = ddlCariJnsVot.SelectedValue

            strsql = $"select kod_zon, butiran from SMKB_Zon where status = '1' order by kod_zon"

            Dim ds As New DataSet
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strsql)
            ' Dim strKod = Trim(ds.Tables(0).Rows(0)("Kod").ToString)

            ddlZonEntry.DataSource = ds
            ddlZonEntry.DataTextField = "butiran"
            ddlZonEntry.DataValueField = "kod_zon"
            ddlZonEntry.DataBind()


            ddlZonEntry.Items.Insert(0, New ListItem("- SILA PILIH -", 0))

            If kodKlasifikasi = "" Then
                ddlZonEntry.SelectedIndex = 0
            Else
                ddlZonEntry.Items.FindByValue(kodKlasifikasi).Selected = True
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindPTjEntry(kodKlasifikasi As String)
        Try
            Dim strsql As String
            'Dim dataCarian = ddlCariJnsVot.SelectedValue

            strsql = $"select kodPejabat + '0000' as kodPejabat, Singkatan from [devmis\sql_ins01].dbstaf.dbo.MS_pejabat where kodpejabat <> '-'"

            Dim ds As New DataSet
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strsql)
            ' Dim strKod = Trim(ds.Tables(0).Rows(0)("Kod").ToString)

            ddlPTjEntry.DataSource = ds
            ddlPTjEntry.DataTextField = "Singkatan"
            ddlPTjEntry.DataValueField = "kodPejabat"
            ddlPTjEntry.DataBind()


            ddlPTjEntry.Items.Insert(0, New ListItem("- SILA PILIH -", 0))

            If kodKlasifikasi = "" Then
                ddlPTjEntry.SelectedIndex = 0
            Else
                ddlPTjEntry.Items.FindByValue(kodKlasifikasi).Selected = True
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindZon(kodKlasifikasi As String)
        Try
            Dim strsql As String
            'Dim dataCarian = ddlCariJnsVot.SelectedValue

            strsql = $"select kod_zon, butiran from SMKB_Zon where status = '1' order by kod_zon"

            Dim ds As New DataSet
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strsql)
            ' Dim strKod = Trim(ds.Tables(0).Rows(0)("Kod").ToString)

            ddlZon.DataSource = ds
            ddlZon.DataTextField = "butiran"
            ddlZon.DataValueField = "kod_zon"
            ddlZon.DataBind()


            ddlZon.Items.Insert(0, New ListItem("- SILA PILIH -", 0))

            If kodKlasifikasi = "" Then
                ddlZon.SelectedIndex = 0
            Else
                ddlZon.Items.FindByValue(kodKlasifikasi).Selected = True
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub lbtnCari_Click(sender As Object, e As EventArgs) Handles lbtnCari.ServerClick
        fBindGv()
        fBindZonEntry(ddlZon.SelectedValue)

    End Sub



End Class