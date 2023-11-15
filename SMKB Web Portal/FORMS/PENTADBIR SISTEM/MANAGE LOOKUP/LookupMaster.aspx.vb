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

Public Class LookupMaster
    Inherits System.Web.UI.Page

    Public dsKod As New DataSet
    Public dvKodKW As New DataView
    Dim dbconn As New DBKewConn
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            fBindGv()
            fBindModul("")
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
            dt.Columns.Add("Kod", GetType(String))
            dt.Columns.Add("Butiran", GetType(String))
            dt.Columns.Add("Dis_Modul", GetType(String))
            dt.Columns.Add("status", GetType(String))
            dt.Columns.Add("Dibuat_Oleh", GetType(String))
            dt.Columns.Add("Tarikh_Dibuat", GetType(String))

            Dim Kod As String
            Dim Butiran As String
            Dim Dis_Modul As String
            Dim status As String
            'Dim Dibuat_Oleh As String
            'Dim Tarikh_Dibuat As String

            Dim strSql As String = $"select a.Kod, a.Butiran, b.Dis_Modul, a.Status, a.Dibuat_Oleh, CONVERT(VARCHAR,ISNULL(a.Tarikh_Dibuat ,GETDATE()),103) Tarikh_Dibuat 
                            from smkb_lookup_master a, SMKB_Modul b
                            where 1 = 1
                            and a.Kod_Modul = b.Kod_Modul
                            and a.Kod_Korporat = 'UTeM'
                            order by Kod"


            dsKod = dbconn.fSelectCommand(strSql)
            dvKodKW = New DataView(dsKod.Tables(0))

            For i As Integer = 0 To dsKod.Tables(0).Rows.Count - 1

                Kod = dsKod.Tables(0).Rows(i)(0).ToString
                Butiran = dsKod.Tables(0).Rows(i)(1).ToString
                Dis_Modul = dsKod.Tables(0).Rows(i)(2).ToString
                'status = dsKod.Tables(0).Rows(i)(3).ToString

                Dim StrStatus = dsKod.Tables(0).Rows(i)(3).ToString
                If StrStatus = 1 Then
                    status = "Aktif"
                Else
                    status = "Tidak Aktif"
                End If

                'Dibuat_Oleh = dsKod.Tables(0).Rows(i)(4).ToString
                'Tarikh_Dibuat = dsKod.Tables(0).Rows(i)(5).ToString

                dt.Rows.Add(Kod, Butiran, Dis_Modul, status)
            Next

            Return dt

        Catch ex As Exception

        End Try

    End Function
    Private Sub fBindModul(kodKlasifikasi As String)
        Try
            Dim strsql As String

            strsql = $"select Kod_Modul, Nama_Modul from SMKB_Modul
                    where status  = 1
                    order by Urutan"

            Dim ds As New DataSet
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strsql)

            ddlModul.DataSource = ds
            ddlModul.DataTextField = "Nama_Modul"
            ddlModul.DataValueField = "Kod_Modul"
            ddlModul.DataBind()


            ddlModul.Items.Insert(0, New ListItem("- SILA PILIH -", 0))

            If kodKlasifikasi = "" Then
                ddlModul.SelectedIndex = 0
            Else
                ddlModul.Items.FindByValue(kodKlasifikasi).Selected = True
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.ServerClick
        Dim strSql As String
        Dim dbconn As New DBKewConn
        Try
            Dim strMasterLookup As String = Trim(txtKod.Value.TrimEnd)
            Dim strButiran As String = Trim(txtButiran.Value.ToUpper.TrimEnd)
            Dim strKod_Modul As String = ddlModul.SelectedValue
            Dim intStatus As Integer = "1"
            Dim strDibuat_Oleh As String = Session("ssusrID")
            Dim strhfc_cd As String = "UTeM"

            'If ViewState("SaveMode") = 1 Then
            'INSERT

            strSql = "select count(*) from smkb_lookup_master where Kod = '" & strMasterLookup & "'"
            If fCheckRec(strSql) = 0 Then

                strSql = "insert into smkb_lookup_master (Kod, Butiran, Kod_Modul, status, Dibuat_Oleh, Kod_Korporat, Tarikh_Dibuat) 
                    values (@Kod, @Butiran, @Kod_Modul, @Status, @Dibuat_Oleh, @Kod_Korporat, GETDATE())"
                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@Kod", strMasterLookup),
                    New SqlParameter("@Butiran", strButiran),
                    New SqlParameter("@Kod_Modul", strKod_Modul),
                    New SqlParameter("@Status", intStatus),
                    New SqlParameter("@Dibuat_Oleh", strDibuat_Oleh),
                    New SqlParameter("@Kod_Korporat", strhfc_cd)
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
                strSql = "update SMKB_lookup_master set Butiran = @Butiran, Kod_Modul = @Kod_Modul, status = @Status
                where Kod = '" & strMasterLookup & "'"
                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@Butiran", strButiran),
                    New SqlParameter("@Kod_Modul", strKod_Modul),
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
                'txtButiran.Value = selectedRow.Cells(1).Text.ToString


                'Call sql
                Dim strSql As String = $"select Kod, Butiran, Kod_Modul, status, Dibuat_Oleh, Tarikh_Dibuat 
                            from smkb_lookup_master              
                            where Kod = '{strKod}'"

                Dim dt As New DataTable
                dt = dbconn.fSelectCommandDt(strSql)
                If dt.Rows.Count > 0 Then

                    txtKod.Value = dt.Rows(0)("Kod")
                    txtKod.Attributes.Add("readonly", "readonly")
                    txtButiran.Value = dt.Rows(0)("Butiran")
                    ddlModul.SelectedValue = dt.Rows(0)("Kod_Modul")

                    Dim status = dt.Rows(0)("status")
                    rblStatus.SelectedValue = status

                End If

                ClientScript.RegisterStartupScript([GetType](), "alert", "ShowPopup('1');", True)
            ElseIf e.CommandName = "ShowDetail" Then

                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvKod.Rows(index)

                Dim dtDetail As New DataTable
                dtDetail.Columns.Add("Kod_Detail", GetType(String))
                dtDetail.Columns.Add("Butiran", GetType(String))
                dtDetail.Columns.Add("keutamaan", GetType(String))
                dtDetail.Columns.Add("Tarikh_Mula", GetType(String))
                dtDetail.Columns.Add("Tarikh_Tamat", GetType(String))
                dtDetail.Columns.Add("status", GetType(String))
                dtDetail.Columns.Add("Dibuat_Oleh", GetType(String))
                dtDetail.Columns.Add("Tarikh_Dibuat", GetType(String))

                Dim kod_detail As String
                Dim Butiran As String
                Dim keutamaan As String
                Dim Tarikh_Mula As String
                Dim Tarikh_Tamat As String
                Dim status As String
                Dim Dibuat_Oleh As String
                Dim Tarikh_Dibuat As String

                Dim dataCarian = selectedRow.Cells(0).Text.ToString

                Dim strSql As String = $"select kod_detail, Butiran, keutamaan, 
                    CONVERT(VARCHAR,ISNULL(Tarikh_Mula,GETDATE()),103) as Tarikh_Mula,  CONVERT(VARCHAR,ISNULL(Tarikh_Tamat,GETDATE()),103) as Tarikh_Tamat, 
                    status, Dibuat_Oleh, Tarikh_Dibuat 
                    from SMKB_lookup_detail
                    where Kod_Korporat = 'UTeM'
                    and Kod = '{dataCarian}'"

                dsKod = dbconn.fSelectCommand(strSql)
                dvKodKW = New DataView(dsKod.Tables(0))

                For i As Integer = 0 To dsKod.Tables(0).Rows.Count - 1

                    kod_detail = dsKod.Tables(0).Rows(i)(0).ToString
                    Butiran = dsKod.Tables(0).Rows(i)(1).ToString
                    keutamaan = dsKod.Tables(0).Rows(i)(2).ToString
                    Tarikh_Mula = dsKod.Tables(0).Rows(i)(3).ToString
                    Tarikh_Tamat = dsKod.Tables(0).Rows(i)(4).ToString

                    Dim StrStatus = dsKod.Tables(0).Rows(i)(5).ToString
                    If StrStatus = 1 Then
                        status = "AKTIF"
                    Else
                        status = "TIDAK AKTIF"
                    End If

                    Dibuat_Oleh = dsKod.Tables(0).Rows(i)(6).ToString
                    Tarikh_Dibuat = dsKod.Tables(0).Rows(i)(7).ToString

                    dtDetail.Rows.Add(kod_detail, Butiran, keutamaan, Tarikh_Mula, Tarikh_Tamat, status, Dibuat_Oleh, Tarikh_Dibuat)
                Next


                '
                'Return dtDetail
                gvDetail.DataSource = dtDetail
                gvDetail.DataBind()

                'Required for jQuery DataTables to work.
                gvDetail.UseAccessibleHeader = True
                gvDetail.HeaderRow.TableSection = TableRowSection.TableHeader
                'Required for jQuery DataTables to work.

                ClientScript.RegisterStartupScript([GetType](), "alert", "ShowPopup('3');", True)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvKod_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvKod.PageIndexChanging
        gvKod.PageIndex = e.NewPageIndex
        fBindGv()
    End Sub

End Class