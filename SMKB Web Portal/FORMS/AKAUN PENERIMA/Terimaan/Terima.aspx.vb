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

Public Class Terima
    Inherits System.Web.UI.Page

    Public dsKod As New DataSet
    Public dvKodKW As New DataView
    Dim dbconn As New DBKewConn
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            'fCariNamaUser()
            'fBindMasterLookup("")
            fBindGv()
            fLoadBankUTeM()
            fLoadModTerimaan()

        End If
    End Sub

    Private Function fBindGv()

        Try

            Dim dt As New DataTable
            dt = fCreateDtLookupDetail()

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

        End Try
    End Function



    Private Sub fLoadModTerimaan()
        Try
            Dim strsql As String

            strsql = $"select Kod_Detail, Butiran from SMKB_Lookup_Detail
                    where Kod = 'AR01'
                    AND STATUS	= 1
                    AND Kod_Korporat = 'UTeM'"

            Dim ds As New DataSet
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strsql)

            ddlModTerimaan.DataSource = ds
            ddlModTerimaan.DataTextField = "Butiran"
            ddlModTerimaan.DataValueField = "Kod_Detail"
            ddlModTerimaan.DataBind()

            ddlModTerimaan.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlModTerimaan.SelectedIndex = 0


        Catch ex As Exception

        End Try
    End Sub



    Private Sub fLoadBankUTeM()
        Try
            Dim strsql As String

            strsql = $"select Kod_Vot, Kod_Vot + ' - ' + Butiran as Butiran from SMKB_Vot
                where Kod_Vot in ('76101','76102','76103','76104','76105','76106','76107','76108') order by Kod_Vot"

            Dim ds As New DataSet
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strsql)

            ddlBankUTeM.DataSource = ds
            ddlBankUTeM.DataTextField = "Butiran"
            ddlBankUTeM.DataValueField = "Kod_Vot"
            ddlBankUTeM.DataBind()

            ddlBankUTeM.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlBankUTeM.SelectedIndex = 0


        Catch ex As Exception

        End Try
    End Sub

    Private Function fCreateDtLookupDetail() As DataTable

        Try

            Dim dt As New DataTable
            dt.Columns.Add("No_Bil", GetType(String))
            dt.Columns.Add("Kod_Penghutang", GetType(String))
            dt.Columns.Add("Nama_Penghutang", GetType(String))
            dt.Columns.Add("Tkh_Mohon", GetType(String))
            dt.Columns.Add("Tujuan", GetType(String))
            dt.Columns.Add("Jumlah", GetType(String))

            Dim No_Bil As String
            Dim Kod_Penghutang As String
            Dim Nama_Penghutang As String
            Dim Tkh_Mohon As String
            Dim Tujuan As String
            Dim Jumlah As String
            Dim strSql As String

            'Dim dataCarian = ddlCariNamaUser.SelectedValue
            Dim dataCarian = txtInput.Text


            If dataCarian = "" Then
                strSql = $"select a.No_Bil, a.Kod_Penghutang, b.Nama_Penghutang, FORMAT(a.Tkh_Mohon, 'dd/MM/yyyy') as Tkh_Mohon, a.Tujuan, a.Jumlah 
                            from SMKB_Bil_Hdr a, SMKB_Penghutang_Master b
                            where 1 = 1
                            and a.Kod_Penghutang = b.Kod_Penghutang"
            Else
                strSql = $"select a.No_Bil, a.Kod_Penghutang, b.Nama_Penghutang, FORMAT(a.Tkh_Mohon, 'dd/MM/yyyy') as Tkh_Mohon, a.Tujuan, a.Jumlah 
                            from SMKB_Bil_Hdr a, SMKB_Penghutang_Master b
                            where 1 = 1
                            and a.Kod_Penghutang = b.Kod_Penghutang
                            and a.Kod_Penghutang = '{dataCarian}'"

            End If

            dsKod = dbconn.fSelectCommand(strSql)
            dvKodKW = New DataView(dsKod.Tables(0))

            For i As Integer = 0 To dsKod.Tables(0).Rows.Count - 1

                No_Bil = dsKod.Tables(0).Rows(i)(0).ToString
                Kod_Penghutang = dsKod.Tables(0).Rows(i)(1).ToString
                Nama_Penghutang = dsKod.Tables(0).Rows(i)(2).ToString
                Tkh_Mohon = dsKod.Tables(0).Rows(i)(3).ToString
                Tujuan = dsKod.Tables(0).Rows(i)(4).ToString
                Jumlah = dsKod.Tables(0).Rows(i)(5).ToString

                dt.Rows.Add(No_Bil, Kod_Penghutang, Nama_Penghutang, Tkh_Mohon, Tujuan, Jumlah)
            Next

            Return dt

        Catch ex As Exception

        End Try

    End Function

    'Private Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.ServerClick
    '    Dim strSql As String
    '    Dim dbconn As New DBKewConn
    '    Try
    '        Dim strCode As String = Trim(txtCode.Value.TrimEnd)
    '        Dim strDescription As String = Trim(txtDescription.Value.ToUpper.TrimEnd)
    '        Dim strMasterLookup As String = ddlMasterLookup.SelectedValue
    '        Dim strStartDate As String = Trim(txtStartDate1.Value)
    '        Dim strEndDate As String = Trim(txtEndDate1.Value)
    '        Dim strPriority As String = Trim(rblpriority_indicator.SelectedValue)
    '        Dim intStatus As String = Trim(rblStatus.SelectedValue)
    '        Dim strhfc_cd As String = "PKUTEM"

    '        strSql = "select count(*) from SMKB_lookup_detail where Master_Reference_code = '" & strMasterLookup & "' and Detail_Reference_code ='" & strCode & "'"
    '        If fCheckRec(strSql) = 0 Then

    '            strSql = "insert into SMKB_lookup_detail (Master_Reference_code, Detail_Reference_code, Description, priority_indicator, start_date, end_date , status, hfc_cd ) 
    '                values (@Master_Reference_code, @Detail_Reference_code,@Description, @priority_indicator, @start_date, @end_date, @Status, @hfc_cd)"
    '            Dim paramSql() As SqlParameter = {
    '                New SqlParameter("@Detail_Reference_code", strCode),
    '                New SqlParameter("@Master_Reference_code", strMasterLookup),
    '                New SqlParameter("@Description", strDescription),
    '                New SqlParameter("@priority_indicator", strPriority),
    '                New SqlParameter("@start_date", strStartDate),
    '                New SqlParameter("@end_date", strEndDate),
    '                New SqlParameter("@Status", intStatus),
    '                New SqlParameter("@hfc_cd", strhfc_cd)
    '            }

    '            dbconn.sConnBeginTrans()
    '            If dbconn.fInsertCommand(strSql, paramSql) > 0 Then
    '                dbconn.sConnCommitTrans()
    '                lblModalMessaage.Text = "Rekod baru telah disimpan" 'message di modal
    '                ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
    '                fBindGv()
    '            Else
    '                dbconn.sConnRollbackTrans()
    '                lblModalMessaage.Text = "Ralat!" 'message di modal
    '                ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
    '            End If

    '        Else
    '            'UPDATE
    '            strSql = "update SMKB_lookup_detail set Description = @Description, priority_indicator = @priority_indicator, start_date = @start_date, end_date = @end_date, status = @status
    '            where Master_Reference_code = '" & strMasterLookup & "' AND Detail_Reference_code = '" & strCode & "'"
    '            Dim paramSql() As SqlParameter = {
    '                    New SqlParameter("@Code", strCode),
    '                    New SqlParameter("@Description", strDescription),
    '                    New SqlParameter("@priority_indicator", strPriority),
    '                    New SqlParameter("@start_date", strStartDate),
    '                    New SqlParameter("@end_date", strEndDate),
    '                    New SqlParameter("@Status", intStatus)
    '                        }
    '            dbconn.sConnBeginTrans()
    '            If dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
    '                dbconn.sConnCommitTrans()
    '                lblModalMessaage.Text = "Rekod telah dikemaskini." 'message di modal
    '                ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
    '                fBindGv()

    '            Else
    '                dbconn.sConnRollbackTrans()

    '                lblModalMessaage.Text = "Ralat!" 'message di modal
    '                ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
    '            End If
    '        End If

    '    Catch ex As Exception

    '    End Try
    'End Sub


    Private Sub gvKod_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvKod.RowCommand
        Try
            If e.CommandName = "Select" Then


                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvKod.Rows(index)


                Dim strKod As String = selectedRow.Cells(0).Text.ToString

                Dim strSql As String = $"select a.No_Bil, a.Kod_Penghutang, b.Nama_Penghutang, FORMAT(a.Tkh_Mohon, 'dd/MM/yyyy') as Tkh_Mohon, a.Tujuan,  FORMAT(a.Jumlah, 'N2') AS Jumlah 
                            from SMKB_Bil_Hdr a, SMKB_Penghutang_Master b
                            where 1 = 1
                            and a.Kod_Penghutang = b.Kod_Penghutang
                            and a.No_Bil = '{strKod}'"

                Dim dt As New DataTable
                dt = dbconn.fSelectCommandDt(strSql)
                If dt.Rows.Count > 0 Then

                    txtNoBil.Value = dt.Rows(0)("No_Bil")
                    txtKodPenghutang.Value = dt.Rows(0)("Kod_Penghutang")
                    txtNamaPenghutang.Value = dt.Rows(0)("Nama_Penghutang")
                    txtTkhMohon.Value = dt.Rows(0)("Tkh_Mohon")
                    txtTujuan.InnerText = dt.Rows(0)("Tujuan")
                    txtJumlah.Value = dt.Rows(0)("Jumlah")
                    totalbil.Value = dt.Rows(0)("Jumlah")
                    txtJumlahSbnr.Value = dt.Rows(0)("Jumlah")


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

    Private Sub lbtnCari_Click(sender As Object, e As EventArgs) Handles lbtnCari.ServerClick
        fBindGv()
    End Sub

End Class