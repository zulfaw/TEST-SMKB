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
Imports System.Web.Services

Public Class TetapanZonStaf
    Inherits System.Web.UI.Page

    Public dsKod As New DataSet
    Public dvKodKW As New DataView
    Dim dbconn As New DBKewConn
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            'fCariJenisVot()
            'fBindJenisVot("")
            fBindZon("")
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

        Try

            Dim dt As New DataTable
            dt.Columns.Add("Bil", GetType(String))
            dt.Columns.Add("KodZon", GetType(String))
            dt.Columns.Add("NoStaf", GetType(String))
            dt.Columns.Add("NamaStaf", GetType(String))
            dt.Columns.Add("Status", GetType(String))

            Dim strBil As Integer
            Dim strKodZon As String
            Dim strNoStaf As String
            Dim strNama As String
            Dim strStatus As String

            Dim dataCarian = ddlZon.SelectedValue

            Dim strSql As String = $"SELECT a.Kod_Zon,  a.No_Staf, b.ms01_nama,  a.Status 
                        FROM SMKB_Zon_Staf as a, [DEVMIS\SQL_INS01].dbStaf.dbo.MS01_Peribadi as b
                        WHERE ms01_nostaf = a.No_Staf
                        AND a.kod_zon =  '{dataCarian}'
                        ORDER BY b.ms01_nama"


            dsKod = dbconn.fSelectCommand(strSql)
            dvKodKW = New DataView(dsKod.Tables(0))

            For i As Integer = 0 To dsKod.Tables(0).Rows.Count - 1

                strBil = i + 1
                strKodZon = dsKod.Tables(0).Rows(i)(0).ToString
                strNoStaf = dsKod.Tables(0).Rows(i)(1).ToString
                strNama = dsKod.Tables(0).Rows(i)(2).ToString



                Dim status = dsKod.Tables(0).Rows(i)(3).ToString
                If status = 1 Then
                    strStatus = "AKTIF"
                Else
                    strStatus = "TIDAK AKTIF"
                End If

                dt.Rows.Add(strBil, strKodZon, strNoStaf, strNama, strStatus)
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
    '        Dim strMasterLookup As String = ddlZon.SelectedValue
    '        Dim strStartDate As String = Trim(txtStartDate.Text.ToUpper.TrimEnd)
    '        Dim strEndDate As String = Trim(txtEndDate.Text.ToUpper.TrimEnd)
    '        Dim strPriority As String = Trim(rblpriority_indicator.DataValueField)
    '        Dim intStatus As Integer = "1"
    '        Dim strhfc_cd As String = "PKUTEM"




    '        'If ViewState("SaveMode") = 1 Then
    '        'INSERT

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
    '                'fGlobalAlert("Rekod baru telah disimpan!", Me.Page, Me.[GetType]())
    '                lblModalMessaage.Text = "Rekod baru telah disimpan" 'message di modal
    '                ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
    '                fBindGv()
    '                'fReset()
    '            Else
    '                dbconn.sConnRollbackTrans()
    '                'fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
    '                lblModalMessaage.Text = "Ralat!" 'message di modal
    '                ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
    '            End If

    '            'Else


    '            'End If

    '            'ElseIf ViewState("SaveMode") = 2 Then
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
    '                'fGlobalAlert("Rekod telah dikemaskini!", Me.Page, Me.[GetType]())
    '                lblModalMessaage.Text = "Rekod telah dikemaskini." 'message di modal
    '                ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
    '                fBindGv()
    '                'fReset()
    '            Else
    '                dbconn.sConnRollbackTrans()
    '                'fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
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

                Dim strKodZon As String = selectedRow.Cells(0).Text.ToString
                txtNoStaf.Value = selectedRow.Cells(1).Text.ToString


                'Call sql
                Dim strSql As String = $"SELECT a.Kod_Zon,  a.No_Staf, b.ms01_nama,  a.Status 
                        FROM SMKB_Zon_Staf as a, [DEVMIS\SQL_INS01].dbStaf.dbo.MS01_Peribadi as b
                        WHERE b.ms01_nostaf = a.No_Staf
                        AND a.kod_zon =  '" & ddlZon.SelectedValue & "' and a.No_Staf ='" & txtNoStaf.Value & "'"

                Dim dt As New DataTable
                dt = dbconn.fSelectCommandDt(strSql)
                If dt.Rows.Count > 0 Then
                    fBindZonEntry(ddlZon.SelectedValue)
                    txtNoStaf.Value = dt.Rows(0)("No_Staf")
                    fBindNamaStafKew(txtNoStaf.Value)

                    Dim status = dt.Rows(0)("status")
                    rblStatus.SelectedValue = status

                End If


                ClientScript.RegisterStartupScript([GetType](), "alert", "ShowPopup('1');", True)


            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub fBindZonEntry(kodKlasifikasi As String)
        Try
            Dim strsql As String

            strsql = $"select kod_zon, butiran from SMKB_Zon where status = '1' order by kod_zon"

            Dim ds As New DataSet
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strsql)

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


    Private Sub fBindNamaStafKew(kodKlasifikasi As String)
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
            ' Dim strKod = Trim(ds.Tables(0).Rows(0)("Kod").ToString)

            ddlNamaStaf.DataSource = ds
            ddlNamaStaf.DataTextField = "MS01_Nama"
            ddlNamaStaf.DataValueField = "MS01_NoStaf"
            ddlNamaStaf.DataBind()


            ddlNamaStaf.Items.Insert(0, New ListItem("- SILA PILIH -", 0))

            If kodKlasifikasi = "" Then
                ddlNamaStaf.SelectedIndex = 0
            Else
                ddlNamaStaf.Items.FindByValue(kodKlasifikasi).Selected = True
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvKod_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvKod.PageIndexChanging
        gvKod.PageIndex = e.NewPageIndex
        fBindGv()
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

    'Private Sub fCariJenisVot()
    '    Try
    '        Dim strsql As String


    '        strsql = $"select Master_Reference_code, Description, source_indicator, status, created_by, created_date from smkb_lookup_master order by Master_Reference_code"

    '        Dim ds As New DataSet
    '        Dim dt As New DataTable
    '        ds = dbconn.fSelectCommand(strsql)
    '        'Dim strKod = Trim(ds.Tables(0).Rows(0)("Kod").ToString)

    '        ddlCariJnsVot.DataSource = ds
    '        ddlCariJnsVot.DataTextField = "Description"
    '        ddlCariJnsVot.DataValueField = "Master_Reference_code"
    '        ddlCariJnsVot.DataBind()

    '        ddlCariJnsVot.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
    '        ddlCariJnsVot.SelectedIndex = 0


    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub lbtnCari_Click(sender As Object, e As EventArgs) Handles lbtnCari.ServerClick
        fBindGv()
    End Sub

    Protected Sub ddlNamaStaf_SelectedIndexChanged(sender As Object, e As EventArgs)
        txtNoStaf.Value = ddlNamaStaf.SelectedValue
    End Sub
End Class