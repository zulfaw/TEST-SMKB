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

Public Class TahapPengguna
    Inherits System.Web.UI.Page

    Public dsKod As New DataSet
    Public dvKodKW As New DataView
    Dim dbconn As New DBKewConn
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            fCariNamaUser()
            fBindMasterLookup("")
            fBindGv()
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

    Private Function fCreateDtLookupDetail() As DataTable

        Try

            Dim dt As New DataTable
            dt.Columns.Add("Kod_Detail", GetType(String))
            dt.Columns.Add("Butiran", GetType(String))
            dt.Columns.Add("Keutamaan", GetType(String))
            dt.Columns.Add("Tarikh_Mula", GetType(String))
            dt.Columns.Add("Tarikh_Tamat", GetType(String))
            dt.Columns.Add("status", GetType(String))
            dt.Columns.Add("Dibuat_Oleh", GetType(String))
            dt.Columns.Add("Tarikh_Dibuat", GetType(String))

            Dim Kod_Detail As String
            Dim Butiran As String
            Dim Keutamaan As String
            Dim Tarikh_Mula As String
            Dim Tarikh_Tamat As String
            Dim status As String
            Dim Dibuat_Oleh As String
            Dim Tarikh_Dibuat As String

            Dim dataCarian = ddlCariNamaUser.SelectedValue

            Dim strSql As String = $"select Kod_Detail, Butiran, Keutamaan, 
                    CONVERT(VARCHAR,ISNULL(Tarikh_Mula,GETDATE()),103) as Tarikh_Mula,  CONVERT(VARCHAR,ISNULL(Tarikh_Tamat,GETDATE()),103) as Tarikh_Tamat, 
                    status, Dibuat_Oleh, Tarikh_Dibuat 
                    from SMKB_lookup_detail
                    where Kod_Korporat = 'UTeM'
                    and Kod = '{dataCarian}'"

            dsKod = dbconn.fSelectCommand(strSql)
            dvKodKW = New DataView(dsKod.Tables(0))

            For i As Integer = 0 To dsKod.Tables(0).Rows.Count - 1

                Kod_Detail = dsKod.Tables(0).Rows(i)(0).ToString
                Butiran = dsKod.Tables(0).Rows(i)(1).ToString
                Keutamaan = dsKod.Tables(0).Rows(i)(2).ToString
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

                dt.Rows.Add(Kod_Detail, Butiran, Keutamaan, Tarikh_Mula, Tarikh_Tamat, status, Dibuat_Oleh, Tarikh_Dibuat)
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


    'Private Sub gvKod_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvKod.RowCommand
    '    Try
    '        If e.CommandName = "Select" Then


    '            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
    '            Dim selectedRow As GridViewRow = gvKod.Rows(index)

    '            Dim strKod As String = selectedRow.Cells(0).Text.ToString
    '            txtDescription.Value = selectedRow.Cells(1).Text.ToString


    '            'Call sql
    '            Dim strSql As String = $"select Master_Reference_code, Detail_Reference_code, Description, priority_indicator, start_date, end_date, status from SMKB_lookup_detail
    '                            where hfc_cd = 'PKUTEM'
    '                            and Detail_Reference_code = '{strKod}' and Master_Reference_code ='" & ddlCariNamaUser.SelectedValue & "'"

    '            Dim dt As New DataTable
    '            dt = dbconn.fSelectCommandDt(strSql)
    '            If dt.Rows.Count > 0 Then

    '                Dim Master_Reference_code As String = dt.Rows(0)("Master_Reference_code")
    '                fBindMasterLookup(Master_Reference_code)
    '                fBindMasterLookup(Master_Reference_code)

    '                txtCode.Value = dt.Rows(0)("Detail_Reference_code")
    '                txtDescription.Value = dt.Rows(0)("Description")


    '                Dim startDate As DateTime = CDate(dt.Rows(0)("start_date"))

    '                'txtStartDate1.Value = 
    '                txtStartDate1.Value = Format(startDate, "yyyy-MM-dd")

    '                Dim endDate As DateTime = CDate(dt.Rows(0)("end_date"))


    '                txtEndDate1.Value = Format(endDate, "yyyy-MM-dd")

    '                Dim priority_indicator = dt.Rows(0)("priority_indicator")
    '                rblpriority_indicator.SelectedValue = priority_indicator

    '                Dim status = dt.Rows(0)("status")
    '                rblStatus.SelectedValue = status

    '            End If


    '            ClientScript.RegisterStartupScript([GetType](), "alert3", "ShowPopup('1');", True)


    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub gvKod_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvKod.PageIndexChanging
        gvKod.PageIndex = e.NewPageIndex
        fBindGv()
    End Sub

    Private Sub fBindMasterLookup(kodKlasifikasi As String)
        Try
            Dim strsql As String
            Dim dataCarian = ddlCariNamaUser.SelectedValue

            strsql = $"select Master_Reference_code, Description from smkb_lookup_master order by Master_Reference_code"

            Dim ds As New DataSet
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strsql)

            ddlMasterLookup.DataSource = ds
            ddlMasterLookup.DataTextField = "Description"
            ddlMasterLookup.DataValueField = "Master_Reference_code"
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

    Private Sub fCariNamaUser()
        Try
            Dim strsql As String

            strsql = $"SELECT MS01_NoStaf, MS01_Nama FROM [DEVMIS\SQL_INS01].dbStaf.dbo.MS01_Peribadi
                    WHERE MS01_Status = 1
                    AND MS01_NoStaf IN (select CLM_loginID from [DEVMIS\SQL_INS04].dbCLM.dbo.CLM_PenggunaSis
                    where CLM_SisKod = 'SMKB'
                    AND CLM_SisStatus = 'AKTIF')
                    ORDER BY MS01_Nama"

            Dim ds As New DataSet
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strsql)

            ddlCariNamaUser.DataSource = ds
            ddlCariNamaUser.DataTextField = "MS01_Nama"
            ddlCariNamaUser.DataValueField = "MS01_NoStaf"
            ddlCariNamaUser.DataBind()

            ddlCariNamaUser.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlCariNamaUser.SelectedIndex = 0


        Catch ex As Exception

        End Try
    End Sub

    Private Sub lbtnCari_Click(sender As Object, e As EventArgs) Handles lbtnCari.ServerClick
        fBindGv()
    End Sub

End Class