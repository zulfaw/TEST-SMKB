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
Imports System.Runtime.InteropServices.ComTypes

Public Class DaftarTahapPengguna
    Inherits System.Web.UI.Page

    Public dsKod As New DataSet
    Public dvKodKW As New DataView
    Dim dbconn As New DBKewConn
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            fCariJenisVot()
            'fBindJenisVot("")
            fBindMasterLookup("")
            fBindGv()
            'calStartDate.Visible = False
            'calEndDate.Visible = False
        End If
    End Sub
    'Protected Sub txtStartDate_Click(ByVal sender As Object, ByVal e As EventArgs)
    '    calStartDate.Visible = True
    'End Sub

    'Protected Sub calStartDate_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs)
    '    txtStartDate.Text = calStartDate.SelectedDate.ToShortDateString()
    '    calStartDate.Visible = False
    'End Sub
    'Protected Sub txtEndDate_Click(ByVal sender As Object, ByVal e As EventArgs)
    '    calEndDate.Visible = True
    'End Sub

    'Protected Sub calEndDate_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs)
    '    txtEndDate.Text = calEndDate.SelectedDate.ToShortDateString()
    '    calEndDate.Visible = False
    'End Sub
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

        Try

            Dim dt As New DataTable
            dt.Columns.Add("Detail_Reference_code", GetType(String))
            dt.Columns.Add("Description", GetType(String))
            dt.Columns.Add("priority_indicator", GetType(String))
            dt.Columns.Add("start_date", GetType(String))
            dt.Columns.Add("end_date", GetType(String))
            dt.Columns.Add("status", GetType(String))
            dt.Columns.Add("created_by", GetType(String))
            dt.Columns.Add("created_date", GetType(String))

            Dim Detail_Reference_code As String
            Dim Description As String
            Dim priority_indicator As String
            Dim start_date As String
            Dim end_date As String
            Dim status As String
            Dim created_by As String
            Dim created_date As String

            Dim dataCarian = ddlCariJnsVot.SelectedValue

            Dim strSql As String = $"select Detail_Reference_code, Description, priority_indicator, start_date, end_date, status, created_by, created_date 
                    from SMKB_lookup_detail
                    where hfc_cd = 'PKUTEM'
                    and Master_Reference_code = '{dataCarian}'"

            dsKod = dbconn.fSelectCommand(strSql)
            dvKodKW = New DataView(dsKod.Tables(0))

            For i As Integer = 0 To dsKod.Tables(0).Rows.Count - 1

                Detail_Reference_code = dsKod.Tables(0).Rows(i)(0).ToString
                Description = dsKod.Tables(0).Rows(i)(1).ToString
                priority_indicator = dsKod.Tables(0).Rows(i)(2).ToString
                start_date = dsKod.Tables(0).Rows(i)(3).ToString
                end_date = dsKod.Tables(0).Rows(i)(4).ToString

                Dim StrStatus = dsKod.Tables(0).Rows(i)(5).ToString
                If StrStatus = 1 Then
                    status = "Aktif"
                Else
                    status = "Tidak Aktif"
                End If

                created_by = dsKod.Tables(0).Rows(i)(6).ToString
                created_date = dsKod.Tables(0).Rows(i)(7).ToString
                'If status = True Then
                '    strStatus = "Aktif"
                'Else
                '    strStatus = "Tidak Aktif"
                'End If

                dt.Rows.Add(Detail_Reference_code, Description, priority_indicator, start_date, end_date, status, created_by, created_date)
            Next

            Return dt

        Catch ex As Exception

        End Try

    End Function

    Private Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.ServerClick
        Dim strSql As String
        Dim dbconn As New DBKewConn
        Try
            Dim strCode As String = Trim(txtCode.Value.TrimEnd)
            Dim strDescription As String = Trim(txtDescription.Value.ToUpper.TrimEnd)
            Dim strMasterLookup As String = ddlMasterLookup.SelectedValue
            Dim strStartDate As String = Trim(txtStartDate1.Value)
            Dim strEndDate As String = Trim(txtEndDate1.Value)
            Dim strPriority As String = Trim(rblpriority_indicator.DataValueField)
            Dim intStatus As Integer = Trim(rblStatus.DataValueField)
            Dim strhfc_cd As String = "PKUTEM"




            'If ViewState("SaveMode") = 1 Then
            'INSERT

            strSql = "select count(*) from SMKB_lookup_detail where Master_Reference_code = '" & strMasterLookup & "' and Detail_Reference_code ='" & strCode & "'"
            If fCheckRec(strSql) = 0 Then

                strSql = "insert into SMKB_lookup_detail (Master_Reference_code, Detail_Reference_code, Description, priority_indicator, start_date, end_date , status, hfc_cd ) 
                    values (@Master_Reference_code, @Detail_Reference_code,@Description, @priority_indicator, @start_date, @end_date, @Status, @hfc_cd)"
                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@Detail_Reference_code", strCode),
                    New SqlParameter("@Master_Reference_code", strMasterLookup),
                    New SqlParameter("@Description", strDescription),
                    New SqlParameter("@priority_indicator", strPriority),
                    New SqlParameter("@start_date", strStartDate),
                    New SqlParameter("@end_date", strEndDate),
                    New SqlParameter("@Status", intStatus),
                    New SqlParameter("@hfc_cd", strhfc_cd)
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
                strSql = "update SMKB_lookup_detail set Description = @Description, priority_indicator = @priority_indicator, start_date = @start_date, end_date = @end_date, status = @status
                where Master_Reference_code = '" & strMasterLookup & "' AND Detail_Reference_code = '" & strCode & "'"
                Dim paramSql() As SqlParameter = {
                        New SqlParameter("@Code", strCode),
                        New SqlParameter("@Description", strDescription),
                        New SqlParameter("@priority_indicator", strPriority),
                        New SqlParameter("@start_date", strStartDate),
                        New SqlParameter("@end_date", strEndDate),
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

                Dim strKod As String = selectedRow.Cells(0).Text.ToString
                txtDescription.Value = selectedRow.Cells(1).Text.ToString


                'Call sql
                Dim strSql As String = $"select Master_Reference_code, Detail_Reference_code, Description, priority_indicator, start_date, end_date, status from SMKB_lookup_detail
                                where hfc_cd = 'PKUTEM'
                                and Detail_Reference_code = '{strKod}' and Master_Reference_code ='" & ddlCariJnsVot.SelectedValue & "'"

                Dim dt As New DataTable
                dt = dbconn.fSelectCommandDt(strSql)
                If dt.Rows.Count > 0 Then

                    Dim Master_Reference_code As String = dt.Rows(0)("Master_Reference_code")
                    fBindMasterLookup(Master_Reference_code)

                    txtCode.Value = dt.Rows(0)("Detail_Reference_code")
                    txtDescription.Value = dt.Rows(0)("Description")


                    Dim startDate As DateTime = CDate(dt.Rows(0)("start_date"))

                    'txtStartDate1.Value = 
                    txtStartDate1.Value = Format(startDate, "yyyy-MM-dd")

                    Dim endDate As DateTime = CDate(dt.Rows(0)("end_date"))

                    'txtEndDate1.Value = 
                    'txtEndDate1.Value = endDate.ToShortDateString()

                    txtEndDate1.Value = Format(endDate, "yyyy-MM-dd")

                    Dim priority_indicator = dt.Rows(0)("priority_indicator")
                    rblpriority_indicator.SelectedValue = priority_indicator

                    Dim status = dt.Rows(0)("status")
                    rblStatus.SelectedValue = status

                    'ClientScript.RegisterStartupScript([GetType](), "alert1", "updStartDate('" & Format(startDate, "yyyy-MM-dd") & "');", True)
                    ' ClientScript.RegisterStartupScript([GetType](), "alert2", "updEndDate('" & Format(endDate, "yyyy-MM-dd") & "');", True)
                End If


                ClientScript.RegisterStartupScript([GetType](), "alert3", "ShowPopup('1');", True)


            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvKod_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvKod.PageIndexChanging
        gvKod.PageIndex = e.NewPageIndex
        fBindGv()
    End Sub


    Private Sub fBindMasterLookup(kodKlasifikasi As String)
        Try
            Dim strsql As String
            Dim dataCarian = ddlCariJnsVot.SelectedValue

            strsql = $"select Master_Reference_code, Description from smkb_lookup_master order by Master_Reference_code"

            Dim ds As New DataSet
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strsql)
            ' Dim strKod = Trim(ds.Tables(0).Rows(0)("Kod").ToString)

            ddlMasterLookup.DataSource = ds
            ddlMasterLookup.DataTextField = "Description"
            ddlMasterLookup.DataValueField = "Master_Reference_code"
            ddlMasterLookup.DataBind()


            ddlMasterLookup.Items.Insert(0, New ListItem("- SILA PILIH -", 0))

            'If kodKlasifikasi = "" Then
            'ddlMasterLookup.SelectedIndex = 0
            'Else
            ddlMasterLookup.Items.FindByValue(ddlCariJnsVot.SelectedValue).Selected = True
            'End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fCariJenisVot()
        Try
            Dim strsql As String


            strsql = $"select Master_Reference_code, Description, source_indicator, status, created_by, created_date from smkb_lookup_master order by Master_Reference_code"

            Dim ds As New DataSet
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strsql)
            'Dim strKod = Trim(ds.Tables(0).Rows(0)("Kod").ToString)

            ddlCariJnsVot.DataSource = ds
            ddlCariJnsVot.DataTextField = "Description"
            ddlCariJnsVot.DataValueField = "Master_Reference_code"
            ddlCariJnsVot.DataBind()

            ddlCariJnsVot.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlCariJnsVot.SelectedIndex = 0


        Catch ex As Exception

        End Try
    End Sub

    Private Sub lbtnCari_Click(sender As Object, e As EventArgs) Handles lbtnCari.ServerClick
        fBindGv()
    End Sub

    Public Shared Function GetAutoCompleteData(ByVal username As String) As List(Of String)
        Dim result As New List(Of String)()
        Dim dsKodAuto As New DataSet

        Dim dbconn As New DBKewConn

        Dim strSql As String = $"select Kod_Vot, Butiran,  Status , Kod_Klasifikasi, Kod_Jenis, Kod_Vot_Saga from SMKB_Vot WHERE Kod_Jenis = '{username}'"


        dsKodAuto = dbconn.fSelectCommand(strSql)


        For i As Integer = 0 To dsKodAuto.Tables(0).Rows.Count - 1

            Dim strKodKW = dsKodAuto.Tables(0).Rows(i)(0).ToString

            result.Add(String.Format("{0}/{1}", strKodKW, strKodKW))

        Next

        Return result

    End Function


End Class