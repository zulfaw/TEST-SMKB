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
Imports System.Runtime.CompilerServices.RuntimeHelpers

Public Class LookupDetail
    Inherits System.Web.UI.Page

    Public dsDetail As New DataSet
    Public dvDetail As New DataView
    Dim dbconn As New DBKewConn
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            fBindMasterLookup("")
            fBindGv()
            fCariLookupMasterAdd("")
        End If
    End Sub
    Private Function fBindGv()

        Try

            Dim dt As New DataTable
            dt = fCreateDtDetail()

            If dt.Rows.Count = 0 Then
                gvDetail.DataSource = New List(Of String)
            Else
                gvDetail.DataSource = dt
            End If
            gvDetail.DataBind()

            'Required for jQuery DataTables to work.
            gvDetail.UseAccessibleHeader = True
            gvDetail.HeaderRow.TableSection = TableRowSection.TableHeader

        Catch ex As Exception
            'ex.Message
        End Try
    End Function
    Private Sub fCariLookupMasterAdd(kodKlasifikasi As String)
        Try
            Dim strsql As String


            strsql = $"select Kod, Butiran, Kod_Modul, status, Dibuat_Oleh, Tarikh_Dibuat from smkb_lookup_master where kod_korporat = 'UTeM' order by Kod"

            Dim ds As New DataSet
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strsql)

            ddlLookupMasterAdd.DataSource = ds
            ddlLookupMasterAdd.DataTextField = "Butiran"
            ddlLookupMasterAdd.DataValueField = "Kod"
            ddlLookupMasterAdd.DataBind()

            ddlLookupMasterAdd.Items.Insert(0, New ListItem("- SILA PILIH -", 0))

            If kodKlasifikasi = "" Then
                ddlLookupMasterAdd.SelectedIndex = 0
            Else
                ddlLookupMasterAdd.Items.FindByValue(kodKlasifikasi).Selected = True
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub fBindMasterLookup(kodKlasifikasi As String)
        Try
            Dim strsql As String

            strsql = $"select Kod, Butiran, Kod_Modul, status, Dibuat_Oleh, Tarikh_Dibuat from smkb_lookup_master where kod_korporat = 'UTeM' order by Kod"

            Dim ds As New DataSet
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strsql)

            ddlMasterLookup.DataSource = ds
            ddlMasterLookup.DataTextField = "Butiran"
            ddlMasterLookup.DataValueField = "Kod"
            ddlMasterLookup.DataBind()


            ddlMasterLookup.Items.Insert(0, New ListItem("- SILA PILIH -", 0))

            If kodKlasifikasi = "" Then
                ddlMasterLookup.SelectedIndex = 0
            Else
                ddlMasterLookup.Items.FindByValue(kodKlasifikasi).Selected = True
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Function fCreateDtDetail() As DataTable

        Try
            Dim dtDetail As New DataTable
            dtDetail.Columns.Add("Kod_Detail", GetType(String))
            dtDetail.Columns.Add("Butiran", GetType(String))
            dtDetail.Columns.Add("keutamaan", GetType(String))
            dtDetail.Columns.Add("Tarikh_Mula", GetType(String))
            dtDetail.Columns.Add("Tarikh_Tamat", GetType(String))
            dtDetail.Columns.Add("status", GetType(String))
            dtDetail.Columns.Add("Dibuat_Oleh", GetType(String))
            dtDetail.Columns.Add("Tarikh_Dibuat", GetType(String))

            Dim Kod_Detail As String
            Dim Butiran As String
            Dim keutamaan As String
            Dim Tarikh_Mula As String
            Dim Tarikh_Tamat As String
            Dim status As String
            Dim Dibuat_Oleh As String
            Dim Tarikh_Dibuat As String

            Dim dataCarian = ddlMasterLookup.SelectedValue


            Dim strSql As String = $"select Kod_Detail, Butiran, keutamaan, 
                    CONVERT(VARCHAR,ISNULL(Tarikh_Mula,GETDATE()),103) as Tarikh_Mula,  CONVERT(VARCHAR,ISNULL(Tarikh_Tamat,GETDATE()),103) as Tarikh_Tamat, 
                    status, Dibuat_Oleh, Tarikh_Dibuat 
                    from SMKB_lookup_detail
                    where Kod_Korporat = 'UTeM'
                    and Kod = '{dataCarian}'"

            dsDetail = dbconn.fSelectCommand(strSql)
            dvDetail = New DataView(dsDetail.Tables(0))

            For i As Integer = 0 To dsDetail.Tables(0).Rows.Count - 1

                Kod_Detail = dsDetail.Tables(0).Rows(i)(0).ToString
                Butiran = dsDetail.Tables(0).Rows(i)(1).ToString
                keutamaan = dsDetail.Tables(0).Rows(i)(2).ToString
                Tarikh_Mula = dsDetail.Tables(0).Rows(i)(3).ToString
                Tarikh_Tamat = dsDetail.Tables(0).Rows(i)(4).ToString

                Dim StrStatus = dsDetail.Tables(0).Rows(i)(5).ToString
                If StrStatus = 1 Then
                    status = "AKTIF"
                Else
                    status = "TIDAK AKTIF"
                End If

                Dibuat_Oleh = dsDetail.Tables(0).Rows(i)(6).ToString
                Tarikh_Dibuat = dsDetail.Tables(0).Rows(i)(7).ToString

                dtDetail.Rows.Add(Kod_Detail, Butiran, keutamaan, Tarikh_Mula, Tarikh_Tamat, status, Dibuat_Oleh, Tarikh_Dibuat)
            Next

            Return dtDetail

        Catch ex As Exception

        End Try

    End Function

    Private Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.ServerClick
        Dim strSql As String
        Dim dbconn As New DBKewConn
        Try
            Dim strCode As String = Trim(txtKod.Value.TrimEnd)
            Dim strButiran As String = Trim(txtButiran.Value.ToUpper.TrimEnd)
            Dim strMasterLookup As String = ddlMasterLookup.SelectedValue
            Dim strStartDate As String = Trim(txtStartDate1.Value)
            Dim strEndDate As String = Trim(txtEndDate1.Value)
            Dim strPriority As String = Trim(rblkeutamaan.SelectedValue)
            Dim intStatus As String = Trim(rblStatus.SelectedValue)
            Dim strKod_Korporat As String = "PKUTEM"
            Dim Dibuat_Oleh As String = Session("ssusrID")
            Dim currentDate As DateTime = DateTime.Now

            strSql = "select count(*) from SMKB_lookup_detail where Kod = '" & strMasterLookup & "' and Kod_Detail ='" & strCode & "'"
            If fCheckRec(strSql) = 0 Then

                strSql = "insert into SMKB_lookup_detail (Kod, Kod_Detail, Butiran, keutamaan, Tarikh_Mula, Tarikh_Tamat , status, Kod_Korporat, Dibuat_Oleh, Tarikh_Dibuat ) 
                    values (@Kod, @Kod_Detail,@Butiran, @keutamaan, @Tarikh_Mula, @Tarikh_Tamat, @Status, @Kod_Korporat, @Dibuat_Oleh, @Tarikh_Dibuat)"
                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@Kod_Detail", strCode),
                    New SqlParameter("@Kod", strMasterLookup),
                    New SqlParameter("@Butiran", strButiran),
                    New SqlParameter("@keutamaan", strPriority),
                    New SqlParameter("@Tarikh_Mula", strStartDate),
                    New SqlParameter("@Tarikh_Tamat", strEndDate),
                    New SqlParameter("@Status", intStatus),
                    New SqlParameter("@Kod_Korporat", strKod_Korporat),
                    New SqlParameter("@Dibuat_Oleh", Dibuat_Oleh),
                    New SqlParameter("@Tarikh_Dibuat", currentDate)
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
                strSql = "update SMKB_lookup_detail set Butiran = @Butiran, keutamaan = @keutamaan, Tarikh_Mula = @Tarikh_Mula, Tarikh_Tamat = @Tarikh_Tamat, status = @status
                where Kod = '" & strMasterLookup & "' AND Kod_Detail = '" & strCode & "'"
                Dim paramSql() As SqlParameter = {
                        New SqlParameter("@Code", strCode),
                        New SqlParameter("@Butiran", strButiran),
                        New SqlParameter("@keutamaan", strPriority),
                        New SqlParameter("@Tarikh_Mula", strStartDate),
                        New SqlParameter("@Tarikh_Tamat", strEndDate),
                        New SqlParameter("@Status", intStatus)
                            }
                dbconn.sConnBeginTrans()
                If dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                    dbconn.sConnCommitTrans()
                    lblModalMessaage.Text = "Rekod telah dikemaskini." 'message di modal
                    ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                    fBindGv()

                Else
                    dbconn.sConnRollbackTrans()

                    lblModalMessaage.Text = "Ralat!" 'message di modal
                    ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub


    Private Sub gvDetail_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvDetail.RowCommand
        Try
            If e.CommandName = "Select" Then


                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvDetail.Rows(index)

                Dim strKod As String = selectedRow.Cells(0).Text.ToString
                txtButiran.Value = selectedRow.Cells(1).Text.ToString


                'Call sql
                Dim strSql As String = $"select Kod, Kod_Detail, Butiran, keutamaan, Tarikh_Mula, Tarikh_Tamat, status from SMKB_lookup_detail
                                where Kod_Korporat = 'UTeM'
                                and Kod_Detail = '{strKod}' and Kod ='" & ddlMasterLookup.SelectedValue & "'"

                Dim dt As New DataTable
                dt = dbconn.fSelectCommandDt(strSql)
                If dt.Rows.Count > 0 Then

                    Dim Kod As String = dt.Rows(0)("Kod")
                    fCariLookupMasterAdd(Kod)

                    txtKod.Value = dt.Rows(0)("Kod_Detail")
                    txtButiran.Value = dt.Rows(0)("Butiran")

                    Dim startDate As DateTime = CDate(dt.Rows(0)("Tarikh_Mula"))
                    txtStartDate1.Value = Format(startDate, "yyyy-MM-dd")

                    Dim endDate As DateTime = CDate(dt.Rows(0)("Tarikh_Tamat"))
                    txtEndDate1.Value = Format(endDate, "yyyy-MM-dd")

                    Dim keutamaan = dt.Rows(0)("keutamaan")
                    rblkeutamaan.SelectedValue = keutamaan

                    Dim status = dt.Rows(0)("status")
                    rblStatus.SelectedValue = status

                End If
                ClientScript.RegisterStartupScript([GetType](), "alert", "ShowPopup('1');", True)


            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvDetail_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvDetail.PageIndexChanging
        gvDetail.PageIndex = e.NewPageIndex
        fBindGv()
    End Sub

    Private Sub lbtnCari_Click(sender As Object, e As EventArgs) Handles lbtnCari.ServerClick
        fBindGv()
        fCariLookupMasterAdd(ddlMasterLookup.SelectedValue)
    End Sub



End Class