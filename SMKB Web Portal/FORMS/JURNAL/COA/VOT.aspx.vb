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

Public Class VOT
    Inherits System.Web.UI.Page

    Public dsKod As New DataSet
    Public dvKodKW As New DataView
    Dim dbconn As New DBKewConn
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            fCariJenisVot()
            fBindJenisVot("")
            fBindKlasifikasiVot("")
            fBindGv()
            'firstBindViewStateButiran()
        End If
    End Sub

    'Private Sub firstBindViewStateButiran()
    '    Try
    '        Dim dt As New DataTable

    '        dt.Columns.Add("Kod_Vot", GetType(String))
    '        dt.Columns.Add("Butiran", GetType(String))
    '        dt.Columns.Add("Kod_Klasifikasi", GetType(String))
    '        dt.Columns.Add("Kod_Jenis", GetType(String))
    '        dt.Columns.Add("Kod_Vot_Saga", GetType(String))
    '        dt.Columns.Add("Status", GetType(String))

    '        'saving databale into viewstate   
    '        ' ViewState("CurrentButiran") = dt
    '        gvKod.DataSource = New List(Of String)
    '        'bind Gridview

    '        gvKod.DataBind()

    '        'Required for jQuery DataTables to work.
    '        gvKod.UseAccessibleHeader = True
    '        gvKod.HeaderRow.TableSection = TableRowSection.TableHeader
    '    Catch ex As Exception
    '    End Try

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
        Dim blnStatus As Boolean
        Dim strStatus As String
        Try


            Dim dt As New DataTable
            dt.Columns.Add("Kod_Vot", GetType(String))
            dt.Columns.Add("Butiran", GetType(String))
            dt.Columns.Add("Kod_Klasifikasi", GetType(String))
            dt.Columns.Add("Kod_Jenis", GetType(String))
            dt.Columns.Add("Kod_Vot_Saga", GetType(String))
            dt.Columns.Add("Status", GetType(String))


            Dim strKod As String
            Dim strButiran As String
            Dim kod_klasifikasi As String
            Dim Kod_Jenis As String
            Dim Kod_Vot_Saga As String

            Dim dataCarian = ddlCariJnsVot.SelectedValue

            Dim strSql As String = $"select Kod_Vot, Butiran,  Status , Kod_Klasifikasi, Kod_Jenis, Kod_Vot_Saga from SMKB_Vot WHERE Kod_Jenis = '{dataCarian}'"


            dsKod = dbconn.fSelectCommand(strSql)
            dvKodKW = New DataView(dsKod.Tables(0))

            For i As Integer = 0 To dsKod.Tables(0).Rows.Count - 1

                    strKod = dsKod.Tables(0).Rows(i)(0).ToString
                    strButiran = dsKod.Tables(0).Rows(i)(1).ToString
                    blnStatus = dsKod.Tables(0).Rows(i)(2).ToString
                    kod_klasifikasi = dsKod.Tables(0).Rows(i)(3).ToString
                    Kod_Jenis = dsKod.Tables(0).Rows(i)(4).ToString
                    Kod_Vot_Saga = dsKod.Tables(0).Rows(i)(5).ToString
                    If blnStatus = True Then
                        strStatus = "Aktif"
                    Else
                        strStatus = "Tidak Aktif"
                    End If

                    dt.Rows.Add(strKod, strButiran, kod_klasifikasi, Kod_Jenis, Kod_Vot_Saga, strStatus)
                Next

                Return dt
          
        Catch ex As Exception

        End Try

    End Function

    Private Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.ServerClick
        Dim strSql As String
        Dim dbconn As New DBKewConn
        Try
            Dim strKod As String = Trim(txtKod.Value.TrimEnd)
            Dim strButiran As String = Trim(txtButiran.Value.ToUpper.TrimEnd)
            Dim strKlasifikasi As String = ddlKlasifikasi.SelectedValue
            Dim strJenis As String = ddlJenVot.SelectedValue
            Dim strVotSaga As String = Trim(txtVotSaga.Value.ToUpper.TrimEnd)
            Dim intStatus As Integer = "1"



            'If ViewState("SaveMode") = 1 Then
            'INSERT

            strSql = "select count(*) from SMKB_Vot where Kod_Vot = '" & strKod & "'"
            If fCheckRec(strSql) = 0 Then

                strSql = "insert into SMKB_Vot (Kod_Vot , Butiran, Kod_Klasifikasi, Kod_Jenis ,Kod_Vot_Saga, Status, Kod_Jenis_Lanjut , Flag_Integrasi, Butiran_Integrasi)
                          values (@Kod,@Butiran, @Klasifikasi, @Jenis, @vot_Saga, @Status , @KodJenisLanjut , @Flag_Integrasi , @Butiran_Integrasi) "
                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@Kod", strKod),
                    New SqlParameter("@Butiran", strButiran),
                    New SqlParameter("@Klasifikasi", strKlasifikasi),
                    New SqlParameter("@Jenis", strJenis),
                    New SqlParameter("@vot_Saga", strVotSaga),
                    New SqlParameter("@Status", intStatus),
                    New SqlParameter("@KodJenisLanjut", "-"),
                    New SqlParameter("@Flag_Integrasi", "-"),
                    New SqlParameter("@Butiran_Integrasi", "-")
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
                strSql = "update SMKB_Vot set Butiran = @Butiran, Kod_Klasifikasi = @Klasifikasi,  Kod_Jenis = @Jenis, Kod_Vot_Saga = @vot_Saga, Status = @Status where Kod_Vot = @Kod"
                Dim paramSql() As SqlParameter = {
                            New SqlParameter("@Kod", strKod),
                            New SqlParameter("@Butiran", strButiran),
                            New SqlParameter("@Klasifikasi", strKlasifikasi),
                            New SqlParameter("@Jenis", strJenis),
                            New SqlParameter("@vot_Saga", strVotSaga),
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

                Dim strKod As String = selectedRow.Cells(1).Text.ToString
                txtButiran.Value = selectedRow.Cells(2).Text.ToString

                'Call sql
                Dim strSql As String = $"select Kod_Vot, Butiran,  Status , Kod_Klasifikasi, Kod_Jenis, Kod_Vot_Saga from SMKB_Vot where Kod_Vot = '{strKod}'"
                Dim dt As New DataTable
                dt = dbconn.fSelectCommandDt(strSql)
                If dt.Rows.Count > 0 Then
                    txtKod.Value = dt.Rows(0)("Kod_Vot")
                    txtButiran.Value = dt.Rows(0)("Butiran")

                    Dim kod_Jenis As String = dt.Rows(0)("Kod_Jenis")
                    Dim kod_Klasifikasi As String = dt.Rows(0)("Kod_Klasifikasi")
                    fBindJenisVot(kod_Jenis)
                    fBindKlasifikasiVot(kod_Klasifikasi)

                    Dim statusAktif = dt.Rows(0)("Status")
                    rblStatus.SelectedValue = statusAktif

                    txtVotSaga.Value = dt.Rows(0)("Kod_Vot_Saga")
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

    Private Sub fBindJenisVot(kodJenis As String)
        Try
            Dim strsql As String


            strsql = $"Select SMKB_Kod_Jenis_Vot as Kod, SMKB_Butiran as butiran from SMKB_Jenis_Vot  order by SMKB_Kod_Jenis_Vot"

            Dim ds As New DataSet
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strsql)
            'Dim strKod = Trim(ds.Tables(0).Rows(0)("Kod").ToString)

            ddlJenVot.DataSource = ds
            ddlJenVot.DataTextField = "butiran"
            ddlJenVot.DataValueField = "Kod"
            ddlJenVot.DataBind()


            ddlJenVot.Items.Insert(0, New ListItem("- SILA PILIH -", 0))

            If kodJenis = "" Then
                ddlJenVot.SelectedIndex = 0
            Else
                ddlJenVot.Items.FindByValue(kodJenis.Trim()).Selected = True
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindKlasifikasiVot(kodKlasifikasi As String)
        Try
            Dim strsql As String


            strsql = $"Select SMKB_Kod_Klasifikasi as Kod, SMKB_Butiran as butiran from SMKB_Klasifikasi_Vot  order by SMKB_Kod_Klasifikasi"

            Dim ds As New DataSet
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strsql)
            ' Dim strKod = Trim(ds.Tables(0).Rows(0)("Kod").ToString)

            ddlKlasifikasi.DataSource = ds
            ddlKlasifikasi.DataTextField = "butiran"
            ddlKlasifikasi.DataValueField = "Kod"
            ddlKlasifikasi.DataBind()


            ddlKlasifikasi.Items.Insert(0, New ListItem("- SILA PILIH -", 0))

            If kodKlasifikasi = "" Then
                ddlKlasifikasi.SelectedIndex = 0
            Else
                ddlKlasifikasi.Items.FindByValue(kodKlasifikasi.Trim()).Selected = True
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fCariJenisVot()
        Try
            Dim strsql As String


            strsql = $"Select SMKB_Kod_Jenis_Vot as Kod, concat(SMKB_Kod_Jenis_Vot,' - ',SMKB_Butiran) as butiran from SMKB_Jenis_Vot  order by SMKB_Kod_Jenis_Vot"

            Dim ds As New DataSet
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strsql)
            'Dim strKod = Trim(ds.Tables(0).Rows(0)("Kod").ToString)

            ddlCariJnsVot.DataSource = ds
            ddlCariJnsVot.DataTextField = "butiran"
            ddlCariJnsVot.DataValueField = "Kod"
            ddlCariJnsVot.DataBind()

            ddlCariJnsVot.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlCariJnsVot.SelectedIndex = 0


        Catch ex As Exception

        End Try
    End Sub

    Private Sub lbtnCari_Click(sender As Object, e As EventArgs) Handles lbtnCari.ServerClick
        fBindGv()
    End Sub



End Class