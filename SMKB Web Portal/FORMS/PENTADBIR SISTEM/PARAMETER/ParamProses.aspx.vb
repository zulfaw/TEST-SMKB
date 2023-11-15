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

Public Class ParamProses
    Inherits System.Web.UI.Page

    Public dsKod As New DataSet
    Public dvKodKW As New DataView
    Dim dbconn As New DBKewConn
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            'fCariJenisVot()
            'fBindJenisVot("")
            'fBindMasterLookup("")
            fBindGv()
            'calStartDate.Visible = False
            'calEndDate.Visible = False
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
            'ex.Message
        End Try
    End Function

    Private Function fCreateDtKW() As DataTable

        Try

            Dim dt As New DataTable
            dt.Columns.Add("Bil", GetType(String))
            dt.Columns.Add("nama", GetType(String))
            dt.Columns.Add("type", GetType(String))
            dt.Columns.Add("jenis", GetType(String))
            dt.Columns.Add("Kategori", GetType(String))
            dt.Columns.Add("kadar", GetType(String))

            dt.Columns.Add("Tarikh_Mula", GetType(String))
            dt.Columns.Add("Tarikh_Akhir", GetType(String))
            dt.Columns.Add("Status", GetType(String))

            Dim Bil As Integer
            Dim strNama As String
            Dim strType As String
            Dim strJenis As String
            Dim strKategori As String
            Dim strKadar As String
            Dim strTarikhMula As String
            Dim strTarikhTamat As String
            Dim strStatus As String

            Dim strSql As String = $"select nama, type, jenis, Kategori, kadar, ISNULL(CONVERT(varchar, Tarikh_Mula, 120), '-') Tarikh_Mula, 
                        ISNULL(CONVERT(varchar, Tarikh_Akhir, 120), '-') Tarikh_Akhir, Status from SMKB_Param_Proses"


            dsKod = dbconn.fSelectCommand(strSql)
            dvKodKW = New DataView(dsKod.Tables(0))

            For i As Integer = 0 To dsKod.Tables(0).Rows.Count - 1

                Bil = i + 1
                strNama = dsKod.Tables(0).Rows(i)(0).ToString
                strType = dsKod.Tables(0).Rows(i)(1).ToString
                strKategori = dsKod.Tables(0).Rows(i)(2).ToString
                strJenis = dsKod.Tables(0).Rows(i)(3).ToString
                strKadar = dsKod.Tables(0).Rows(i)(4).ToString
                strTarikhMula = dsKod.Tables(0).Rows(i)(5).ToString
                strTarikhTamat = dsKod.Tables(0).Rows(i)(6).ToString

                Dim status = dsKod.Tables(0).Rows(i)(7).ToString
                If status = True Then
                    strStatus = "Aktif"
                Else
                    strStatus = "Tidak Aktif"
                End If

                dt.Rows.Add(Bil, strNama, strType, strJenis, strKategori, strKadar, strTarikhMula, strTarikhTamat, strStatus)
            Next

            Return dt

        Catch ex As Exception

        End Try

    End Function

    Private Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.ServerClick
        Dim strSql As String
        Dim dbconn As New DBKewConn
        Try
            Dim strMasterLookup As String = Trim(txtCode.Value.TrimEnd)
            Dim strDescription As String = Trim(txtDescription.Value.ToUpper.TrimEnd)
            Dim strsource_indicator As String = Trim(txtsource_indicator.Value.ToUpper.TrimEnd)
            Dim intStatus As Integer = "1"
            Dim strcreated_by As String = "01662"
            Dim strhfc_cd As String = "PKUTEM"




            'If ViewState("SaveMode") = 1 Then
            'INSERT

            strSql = "select count(*) from smkb_lookup_master where Master_Reference_code = '" & strMasterLookup & "'"
            If fCheckRec(strSql) = 0 Then

                strSql = "insert into smkb_lookup_master (Master_Reference_code, Description, source_indicator, status, created_by, created_date) 
                    values (@Master_Reference_code, @Description, @source_indicator, @Status, @created_by, GETDATE())"
                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@Master_Reference_code", strMasterLookup),
                    New SqlParameter("@Description", strDescription),
                    New SqlParameter("@source_indicator", strsource_indicator),
                    New SqlParameter("@Status", intStatus),
                    New SqlParameter("@created_by", strhfc_cd)
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
                strSql = "update SMKB_lookup_master set Description = @Description, source_indicator = @source_indicator, status = @Status
                where Master_Reference_code = '" & strMasterLookup & "'"
                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@Description", strDescription),
                    New SqlParameter("@source_indicator", strsource_indicator),
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
                'txtDescription.Value = selectedRow.Cells(1).Text.ToString


                'Call sql
                Dim strSql As String = $"select Master_Reference_code, Description, source_indicator, status, created_by, created_date 
                            from smkb_lookup_master              
                            where Master_Reference_code = '{strKod}'"

                Dim dt As New DataTable
                dt = dbconn.fSelectCommandDt(strSql)
                If dt.Rows.Count > 0 Then

                    txtCode.Value = dt.Rows(0)("Master_Reference_code")
                    txtDescription.Value = dt.Rows(0)("Description")
                    txtsource_indicator.Value = dt.Rows(0)("source_indicator")

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


    'Private Sub fBindMasterLookup(kodKlasifikasi As String)
    '    Try
    '        Dim strsql As String
    '        Dim dataCarian = ddlCariJnsVot.SelectedValue

    '        strsql = $"select Master_Reference_code, Description from smkb_lookup_master order by Master_Reference_code"

    '        Dim ds As New DataSet
    '        Dim dt As New DataTable
    '        ds = dbconn.fSelectCommand(strsql)
    '        ' Dim strKod = Trim(ds.Tables(0).Rows(0)("Kod").ToString)

    '        ddlMasterLookup.DataSource = ds
    '        ddlMasterLookup.DataTextField = "Description"
    '        ddlMasterLookup.DataValueField = "Master_Reference_code"
    '        ddlMasterLookup.DataBind()


    '        ddlMasterLookup.Items.Insert(0, New ListItem("- SILA PILIH -", 0))

    '        'If kodKlasifikasi = "" Then
    '        'ddlMasterLookup.SelectedIndex = 0
    '        'Else
    '        ddlMasterLookup.Items.FindByValue(ddlCariJnsVot.SelectedValue).Selected = True
    '        'End If

    '    Catch ex As Exception

    '    End Try
    'End Sub

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

    'Private Sub lbtnCari_Click(sender As Object, e As EventArgs) Handles lbtnCari.ServerClick
    '    fBindGv()
    'End Sub




End Class