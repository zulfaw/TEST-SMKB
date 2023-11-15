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
Imports System.Drawing.Printing
Imports System.IO
'Imports iTextSharp.text
'Imports iTextSharp.text.html.simpleparser
'Imports iTextSharp.text.pdf

Public Class JadualBayarBalik
    Inherits System.Web.UI.Page

    Public dsTahapSkrin As New DataSet
    Public dvTahapSkrin As New DataView
    Dim dbconn As New DBKewConn
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            fCariTahap()
            fBindGv()

        End If
    End Sub

    Private Function fBindGv()

        Try

            Dim dt As New DataTable
            dt = fCreateDtTahapSkrin()

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

    Private Function fCreateDtTahapSkrin() As DataTable

        Try

            Dim dt As New DataTable
            dt.Columns.Add("Bil", GetType(String))
            dt.Columns.Add("Ansuran", GetType(String))
            dt.Columns.Add("Faedah", GetType(String))
            dt.Columns.Add("Pokok", GetType(String))
            dt.Columns.Add("JumFaedah", GetType(String))
            dt.Columns.Add("JumPokok", GetType(String))
            dt.Columns.Add("BakiPokok", GetType(String))

            Dim strBil As String
            Dim strAnsuran As String
            Dim strFaedah As String
            Dim strPokok As String
            Dim strJumFaedah As String
            Dim strJumPokok As String
            Dim strBakiPokok As String

            Dim dataCarian = ddlCariTahap.SelectedValue

            Dim strSql As String = $"select  b.Bil_Byr, b.Ansuran, b.Faedah, b.Pokok, b.Ansuran, b.Baki_Pokok
                from SMKB_Pinjaman_Master AS a, SMKB_Pinjaman_Jadual_Bayar_Balik as b, [devmis\sql_ins01].dbstaf.dbo.MS01_Peribadi AS c,
                [devmis\sql_ins01].dbstaf.dbo.MS08_Penempatan AS d, [devmis\sql_ins01].dbstaf.dbo.MS_pejabat AS e
                where 1 = 1
                and b.No_Pinj = a.No_Pinj
                and a.No_Staf = c.MS01_NoStaf
                and a.No_Staf = d.MS01_NoStaf
                and d.MS08_StaTerkini = 1
                and SUBSTRING(d.MS08_Unit,1,2) = e.kodpejabat
                and a.No_Pinj = '" & dataCarian & "'"

            dsTahapSkrin = dbconn.fSelectCommand(strSql)
            dvTahapSkrin = New DataView(dsTahapSkrin.Tables(0))

            For i As Integer = 0 To dsTahapSkrin.Tables(0).Rows.Count - 1
                strBil = dsTahapSkrin.Tables(0).Rows(i)(0).ToString
                strAnsuran = dsTahapSkrin.Tables(0).Rows(i)(1).ToString
                strFaedah = dsTahapSkrin.Tables(0).Rows(i)(2).ToString
                strPokok = dsTahapSkrin.Tables(0).Rows(i)(3).ToString
                strJumFaedah = "" 'dsTahapSkrin.Tables(0).Rows(i)(1).ToString + dsTahapSkrin.Tables(0).Rows(i)(2).ToString
                strJumPokok = "" 'dsTahapSkrin.Tables(0).Rows(i)(4).ToString
                strBakiPokok = dsTahapSkrin.Tables(0).Rows(i)(4).ToString


                dt.Rows.Add(strBil, strAnsuran, strFaedah, strPokok, strJumFaedah, strJumPokok, strBakiPokok)
            Next

            Return dt

        Catch ex As Exception

        End Try

    End Function
    Protected Sub ExportToPDF(sender As Object, e As EventArgs)

        Dim noPinj As String = ddlCariTahap.SelectedValue
        'Dim btn As ImageButton = CType(sender, ImageButton)
        Dim file As String = "PV00200422.pdf" 'nomatrik + "_FULL.pdf"
        'Dim Tahun As String = Me.txtTahun.Text

        'Dim url As String = "//10.1.2.79/eFYP/Thesis/" & Tahun & "/" & nomatrik & "/" & file
        Dim url As String = "../../../PDF/" & file
        Session("FileDoc") = url + "?" + DateTime.Now()
        Session("noPinj") = noPinj

        ClientScript.RegisterStartupScript(Me.GetType(), "script", "OpenWindowPB('viewDoc.aspx',1524,800, '" & Session("FileDoc") & "','" & Session("noPinj") & "');", True)

    End Sub

    Protected Sub PrintPreview(sender As Object, e As EventArgs)

        Dim noPinj As String = ddlCariTahap.SelectedValue

        Session("noPinj") = noPinj

        ClientScript.RegisterStartupScript(Me.GetType(), "script", "OpenWindowPB('CetakJadualBayarBalik.aspx',1524,800, '" & Session("noPinj") & "');", True)

    End Sub
    'Private Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.ServerClick
    '    Dim strSql As String
    '    Dim dbconn As New DBKewConn
    '    Try
    '        Dim strCode As String = Trim(txtCode.Value.TrimEnd)
    '        Dim strDescription As String = Trim(txtDescription.Value.ToUpper.TrimEnd)
    '        Dim strMasterLookup As String = ddlMasterLookup.SelectedValue
    '        Dim strStartDate As String = Trim(txtStartDate1.Value)
    '        Dim strEndDate As String = Trim(txtEndDate1.Value)
    '        Dim strPriority As String = Trim(rblpriority_indicator.DataValueField)
    '        Dim intStatus As Integer = Trim(rblStatus.DataValueField)
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


    'Private Sub gvKod_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvKod.RowCommand
    '    Try
    '        If e.CommandName = "Select" Then


    '            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
    '            Dim selectedRow As GridViewRow = gvKod.Rows(index)

    '            Dim strKodSubMenu As String = selectedRow.Cells(0).Text.ToString

    '            'Call sql
    '            Dim strSql As String = $"SELECT a.Kod_Sub_Menu, b.Nama_Sub_Menu, isnull(a.Tkh_Mula,GETDATE()) Tkh_Mula, isnull(a.Tkh_Tamat,getdate()) Tkh_Tamat, a.Jen_Capai, a.Status
    '                    FROM SMKB_UProses_Kump a, SMKB_Sub_Menu b
    '                    WHERE b.Kod_Sub_Menu = a.Kod_Sub_Menu
    '                    AND a.Kod_Sub_Menu = '{strKodSubMenu}' AND a.Kod_Tahap = '" & ddlCariTahap.SelectedValue & "'"


    '            Dim dt As New DataTable
    '            dt = dbconn.fSelectCommandDt(strSql)
    '            If dt.Rows.Count > 0 Then

    '                Dim strKodTahap As String = ddlCariTahap.SelectedValue
    '                fBindTahap(strKodTahap)

    '                txtKodSubMenu.Value = dt.Rows(0)("Kod_Sub_Menu")
    '                fBindSubMenu(txtKodSubMenu.Value)

    '                Dim startDate As DateTime = CDate(dt.Rows(0)("Tkh_Mula"))
    '                txtTkhMula.Value = Format(startDate, "yyyy-MM-dd")

    '                Dim endDate As DateTime = CDate(dt.Rows(0)("Tkh_Tamat"))
    '                txtTkhTamat.Value = Format(endDate, "yyyy-MM-dd")

    '                txtJenCapai.Value = dt.Rows(0)("Jen_Capai")

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

    'Private Sub fBindSubMenu(kodKlasifikasi As String)
    '    Try
    '        Dim strsql As String
    '        Dim dataCarian = ddlCariTahap.SelectedValue

    '        strsql = $"select a.No_Pinj, a.No_Staf, b.MS01_Nama
    '                    from SMKB_Pinjaman_Master AS a, [devmis\sql_ins01].dbstaf.dbo.MS01_Peribadi AS b
    '                    WHERE a.No_Staf = b.MS01_NoStaf
    '                    AND a.Kod_Stat_Mohon = 'A'
    '                    AND a.No_Pinj like 'PV%'
    '                    AND a.Kod_Skim = 'S00001'
    '                    ORDER BY b.MS01_Nama"

    '        Dim ds As New DataSet
    '        Dim dt As New DataTable
    '        ds = dbconn.fSelectCommand(strsql)
    '        ' Dim strKod = Trim(ds.Tables(0).Rows(0)("Kod").ToString)

    '        ddlNamaSubMenu.DataSource = ds
    '        ddlNamaSubMenu.DataTextField = "MS01_Nama"
    '        ddlNamaSubMenu.DataValueField = "No_Pinj"
    '        ddlNamaSubMenu.DataBind()


    '        ddlNamaSubMenu.Items.Insert(0, New ListItem("- SILA PILIH -", 0))

    '        'If kodKlasifikasi = "" Then
    '        'ddlMasterLookup.SelectedIndex = 0
    '        'Else
    '        ddlNamaSubMenu.Items.FindByValue(kodKlasifikasi).Selected = True
    '        'End If

    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Private Sub fBindTahap(kodKlasifikasi As String)
    '    Try
    '        Dim strsql As String
    '        Dim dataCarian = ddlCariTahap.SelectedValue

    '        strsql = $"select a.No_Pinj, a.No_Staf, b.MS01_Nama
    '                    from SMKB_Pinjaman_Master AS a, [devmis\sql_ins01].dbstaf.dbo.MS01_Peribadi AS b
    '                    WHERE a.No_Staf = b.MS01_NoStaf
    '                    AND a.Kod_Stat_Mohon = 'A'
    '                    AND a.No_Pinj like 'PV%'
    '                    AND a.Kod_Skim = 'S00001'
    '                    ORDER BY b.MS01_Nama"

    '        Dim ds As New DataSet
    '        Dim dt As New DataTable
    '        ds = dbconn.fSelectCommand(strsql)
    '        ' Dim strKod = Trim(ds.Tables(0).Rows(0)("Kod").ToString)

    '        ddlTahap.DataSource = ds
    '        ddlTahap.DataTextField = "MS01_Nama"
    '        ddlTahap.DataValueField = "No_Pinj"
    '        ddlTahap.DataBind()


    '        ddlTahap.Items.Insert(0, New ListItem("- SILA PILIH -", 0))

    '        'If kodKlasifikasi = "" Then
    '        'ddlMasterLookup.SelectedIndex = 0
    '        'Else
    '        ddlTahap.Items.FindByValue(ddlCariTahap.SelectedValue).Selected = True
    '        'End If

    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub fCariTahap()
        Try
            Dim strsql As String


            strsql = $"select a.No_Pinj, a.No_Staf, b.MS01_Nama
                        from SMKB_Pinjaman_Master AS a, [devmis\sql_ins01].dbstaf.dbo.MS01_Peribadi AS b
                        WHERE a.No_Staf = b.MS01_NoStaf
                        AND a.Kod_Stat_Mohon = 'A'
                        AND a.No_Pinj like 'PV%'
                        AND a.Kod_Skim = 'S00001'
                        ORDER BY b.MS01_Nama"

            Dim ds As New DataSet
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strsql)

            ddlCariTahap.DataSource = ds
            ddlCariTahap.DataTextField = "MS01_Nama"
            ddlCariTahap.DataValueField = "No_Pinj"
            ddlCariTahap.DataBind()

            ddlCariTahap.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlCariTahap.SelectedIndex = 0


        Catch ex As Exception

        End Try
    End Sub

    Private Sub lbtnCari_Click(sender As Object, e As EventArgs) Handles lbtnCari.ServerClick
        fBindGv()
    End Sub

End Class