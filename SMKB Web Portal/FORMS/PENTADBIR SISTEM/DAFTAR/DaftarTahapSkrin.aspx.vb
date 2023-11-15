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

Public Class DaftarTahapSkrin
    Inherits System.Web.UI.Page

    Public dsTahapSkrin As New DataSet
    Public dvTahapSkrin As New DataView
    Dim dbconn As New DBKewConn
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            fCariTahap()
            fBindGv()
            fBindSubMenu("")
            fBindTahap("")

            'Dim tkhMula As DateTime = Date.Now
            'Dim tkhTamat As DateTime

            ''tkhMula = Date.Now
            'tkhTamat = tkhMula.AddYears(10)

            'txtTkhMula.Value = Format(tkhMula, "yyyy-MM-dd")

            'txtTkhTamat.Value = Format(tkhTamat, "yyyy-MM-dd")

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
            'lblJumRekod.InnerText = dt.Rows.Count


            'Required for jQuery DataTables to work.
            gvKod.UseAccessibleHeader = True
            gvKod.HeaderRow.TableSection = TableRowSection.TableHeader

        Catch ex As Exception

        End Try
    End Function

    Private Function fCreateDtTahapSkrin() As DataTable

        Try

            Dim dt As New DataTable
            dt.Columns.Add("Kod_Sub_Menu", GetType(String))
            dt.Columns.Add("Nama_Sub_Menu", GetType(String))
            dt.Columns.Add("Tkh_Mula", GetType(String))
            dt.Columns.Add("Tkh_Tamat", GetType(String))
            dt.Columns.Add("Jen_Capai", GetType(String))
            dt.Columns.Add("status", GetType(String))

            Dim strKod_Sub_Menu As String
            Dim strNama_Sub_Menu As String
            Dim strTkh_Mula As String
            Dim strTkh_Tamat As String
            Dim strJen_Capai As String
            Dim strStatus As String

            Dim dataCarian = ddlCariTahap.SelectedValue

            Dim strSql As String = $"SELECT a.Kod_Sub_Menu, b.Nama_Sub_Menu, CONVERT(VARCHAR,ISNULL(a.Tkh_Mula,GETDATE()),103) Tkh_Mula, CONVERT(VARCHAR,ISNULL(a.Tkh_Tamat,GETDATE()),103) Tkh_Tamat, a.Jen_Capai, ISNULL(a.Status,'0') status
                        FROM SMKB_UProses_Kump a, SMKB_Sub_Menu b
                        WHERE b.Kod_Sub_Menu = a.Kod_Sub_Menu
                        AND a.Kod_Tahap = '" & dataCarian & "'"

            dsTahapSkrin = dbconn.fSelectCommand(strSql)
            dvTahapSkrin = New DataView(dsTahapSkrin.Tables(0))

            For i As Integer = 0 To dsTahapSkrin.Tables(0).Rows.Count - 1

                strKod_Sub_Menu = dsTahapSkrin.Tables(0).Rows(i)(0).ToString
                strNama_Sub_Menu = dsTahapSkrin.Tables(0).Rows(i)(1).ToString
                strTkh_Mula = dsTahapSkrin.Tables(0).Rows(i)(2).ToString
                strTkh_Tamat = dsTahapSkrin.Tables(0).Rows(i)(3).ToString
                strJen_Capai = dsTahapSkrin.Tables(0).Rows(i)(4).ToString

                Dim status = dsTahapSkrin.Tables(0).Rows(i)(5).ToString
                If status = True Then
                    strStatus = "AKTIF"
                Else
                    strStatus = "TIDAK AKTIF"
                End If

                dt.Rows.Add(strKod_Sub_Menu, strNama_Sub_Menu, strTkh_Mula, strTkh_Tamat, strJen_Capai, StrStatus)
            Next

            Return dt

        Catch ex As Exception

        End Try

    End Function

    Private Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.ServerClick
        Dim strSql As String
        Dim dbconn As New DBKewConn
        Try

            Dim strKodTahap As String = ddlTahap.SelectedValue
            Dim strKodSubMenu As String = ddlNamaSubMenu.SelectedValue

            Dim strStartDate As String = Trim(txtTkhMula.Value)
            Dim strEndDate As String = Trim(txtTkhTamat.Value)

            Dim strJenisCapai As String = Trim(txtJenCapai.Value)

            Dim intStatus As String = Trim(rblStatus.SelectedValue)

            'INSERT
            strSql = "SELECT count(*)
                    FROM SMKB_UProses_Kump
                    WHERE Kod_Sub_Menu = '" & strKodSubMenu & "' AND Kod_Tahap = '" & strKodTahap & "'"

            If fCheckRec(strSql) = 0 Then

                strSql = "insert into SMKB_UProses_Kump (Kod_Sub_Menu, Tkh_Mula, Tkh_Tamat, Kod_Tahap, Jen_Capai, Status) 
                    values (@Kod_Sub_Menu, @Tkh_Mula, @Tkh_Tamat, @Kod_Tahap, @Jen_Capai, @Status)"
                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@Kod_Sub_Menu", strKodSubMenu),
                    New SqlParameter("@Tkh_Mula", strStartDate),
                    New SqlParameter("@Tkh_Tamat", strEndDate),
                    New SqlParameter("@Kod_Tahap", strKodTahap),
                    New SqlParameter("@Jen_Capai", strJenisCapai),
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
                strSql = "update SMKB_UProses_Kump set Tkh_Mula = @Tkh_Mula, Tkh_Tamat = @Tkh_Tamat, Jen_Capai = @Jen_Capai, Status = @Status
                WHERE Kod_Sub_Menu = '" & strKodSubMenu & "' AND Kod_Tahap = '" & strKodTahap & "'"
                Dim paramSql() As SqlParameter = {
                        New SqlParameter("@Tkh_Mula", strStartDate),
                        New SqlParameter("@Tkh_Tamat", strEndDate),
                        New SqlParameter("@Jen_Capai", strJenisCapai),
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
            Dim errorMessage As String = "An error occurred: " & ex.Message
            Response.Write(errorMessage)
        End Try
    End Sub


    Private Sub gvKod_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvKod.RowCommand
        Try
            If e.CommandName = "Select" Then


                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvKod.Rows(index)

                Dim strKodSubMenu As String = selectedRow.Cells(0).Text.ToString

                'Call sql
                Dim strSql As String = $"SELECT a.Kod_Sub_Menu, b.Nama_Sub_Menu, isnull(a.Tkh_Mula,GETDATE()) Tkh_Mula, isnull(a.Tkh_Tamat,getdate()) Tkh_Tamat, a.Jen_Capai, a.Status
                        FROM SMKB_UProses_Kump a, SMKB_Sub_Menu b
                        WHERE b.Kod_Sub_Menu = a.Kod_Sub_Menu
                        AND a.Kod_Sub_Menu = '{strKodSubMenu}' AND a.Kod_Tahap = '" & ddlCariTahap.SelectedValue & "'"


                Dim dt As New DataTable
                dt = dbconn.fSelectCommandDt(strSql)
                If dt.Rows.Count > 0 Then

                    Dim strKodTahap As String = ddlCariTahap.SelectedValue
                    fBindTahap(strKodTahap)

                    txtKodSubMenu.Value = dt.Rows(0)("Kod_Sub_Menu")
                    fBindSubMenu(txtKodSubMenu.Value)

                    Dim startDate As DateTime = CDate(dt.Rows(0)("Tkh_Mula"))
                    txtTkhMula.Value = Format(startDate, "yyyy-MM-dd")

                    Dim endDate As DateTime = CDate(dt.Rows(0)("Tkh_Tamat"))
                    txtTkhTamat.Value = Format(endDate, "yyyy-MM-dd")

                    txtJenCapai.Value = dt.Rows(0)("Jen_Capai")

                    Dim status = dt.Rows(0)("status")
                    rblStatus.SelectedValue = status

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

    Private Sub fBindSubMenu(kodKlasifikasi As String)
        Try
            Dim strsql As String
            Dim dataCarian = ddlCariTahap.SelectedValue

            strsql = $"SELECT Kod_Sub_Menu, Nama_Sub_Menu FROM SMKB_Sub_Menu WHERE Status = 1 ORDER BY Kod_Sub_Menu , Nama_Sub_Menu"

            Dim ds As New DataSet
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strsql)
            ' Dim strKod = Trim(ds.Tables(0).Rows(0)("Kod").ToString)

            ddlNamaSubMenu.DataSource = ds
            ddlNamaSubMenu.DataTextField = "Nama_Sub_Menu"
            ddlNamaSubMenu.DataValueField = "Kod_Sub_Menu"
            ddlNamaSubMenu.DataBind()


            ddlNamaSubMenu.Items.Insert(0, New ListItem("- SILA PILIH -", 0))

            If kodKlasifikasi = "" Then
                ddlNamaSubMenu.SelectedIndex = 0
            Else
                ddlNamaSubMenu.Items.FindByValue(kodKlasifikasi).Selected = True
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindTahap(kodKlasifikasi As String)
        Try
            Dim strsql As String
            Dim dataCarian = ddlCariTahap.SelectedValue

            strsql = $"SELECT Kod_Tahap, Jen_Tahap FROM SMKB_UTahap WHERE Status = 1 ORDER BY Jen_Tahap"

            Dim ds As New DataSet
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strsql)
            ' Dim strKod = Trim(ds.Tables(0).Rows(0)("Kod").ToString)

            ddlTahap.DataSource = ds
            ddlTahap.DataTextField = "Jen_Tahap"
            ddlTahap.DataValueField = "Kod_Tahap"
            ddlTahap.DataBind()


            ddlTahap.Items.Insert(0, New ListItem("- SILA PILIH -", 0))

            'If kodKlasifikasi = "" Then
            'ddlMasterLookup.SelectedIndex = 0
            'Else
            ddlTahap.Items.FindByValue(ddlCariTahap.SelectedValue).Selected = True
            'End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fCariTahap()
        Try
            Dim strsql As String


            strsql = $"SELECT Kod_Tahap, Jen_Tahap FROM SMKB_UTahap WHERE Status = 1 ORDER BY Jen_Tahap"

            Dim ds As New DataSet
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strsql)

            ddlCariTahap.DataSource = ds
            ddlCariTahap.DataTextField = "Jen_Tahap"
            ddlCariTahap.DataValueField = "Kod_Tahap"
            ddlCariTahap.DataBind()

            ddlCariTahap.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlCariTahap.SelectedIndex = 0


        Catch ex As Exception

        End Try
    End Sub

    Private Sub lbtnCari_Click(sender As Object, e As EventArgs) Handles lbtnCari.ServerClick
        fBindGv()
        fBindTahap(ddlCariTahap.SelectedValue)
    End Sub

End Class