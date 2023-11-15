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

Public Class DaftarSubMenu
    Inherits System.Web.UI.Page

    Public dsSubMenu As New DataSet
    Public dvSubMenu As New DataView
    Dim dbconn As New DBKewConn
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            fCariModul()
            fCariSubModul()
            'fBindModul("")
            fBindGv()

        End If
    End Sub

    Private Function fBindGv()

        Try

            Dim dt As New DataTable
            dt = fCreateDtSubMenu()

            If dt.Rows.Count = 0 Then
                gvSubMenu.DataSource = New List(Of String)
            Else
                gvSubMenu.DataSource = dt
            End If
            gvSubMenu.DataBind()

            'Required for jQuery DataTables to work.
            gvSubMenu.UseAccessibleHeader = True
            gvSubMenu.HeaderRow.TableSection = TableRowSection.TableHeader

        Catch ex As Exception

        End Try
    End Function

    Private Function fCreateDtSubMenu() As DataTable

        Try

            Dim dt As New DataTable
            dt.Columns.Add("Kod_Sub_Menu", GetType(String))
            dt.Columns.Add("Nama_Sub_Menu", GetType(String))
            dt.Columns.Add("Dis_Sub_Menu", GetType(String))
            dt.Columns.Add("Laporan", GetType(String))
            dt.Columns.Add("Urutan", GetType(String))
            dt.Columns.Add("Nama_Fail", GetType(String))
            dt.Columns.Add("status", GetType(String))

            Dim strKod_Sub_Menu As String
            Dim strNama_Sub_Menu As String
            Dim strDis_Sub_Menu As String
            Dim strUrutan As String
            Dim strNama_Fail As String
            Dim strstatus As String
            Dim strLaporan As String

            Dim dataCarian = ddlCariSubModul.SelectedValue

            Dim strSql As String = $"SELECT Kod_Sub_Menu, Nama_Sub_Menu, Dis_Sub_Menu, Urutan, Status, isnull(Nama_Fail,'-') Nama_Fail, isnull(Laporan,'-') as Laporan FROM SMKB_Sub_Menu where Kod_Sub = '{dataCarian}' ORDER BY Urutan"

            dsSubMenu = dbconn.fSelectCommand(strSql)
            dvSubMenu = New DataView(dsSubMenu.Tables(0))

            For i As Integer = 0 To dsSubMenu.Tables(0).Rows.Count - 1

                strKod_Sub_Menu = dsSubMenu.Tables(0).Rows(i)(0).ToString
                strNama_Sub_Menu = dsSubMenu.Tables(0).Rows(i)(1).ToString
                strDis_Sub_Menu = dsSubMenu.Tables(0).Rows(i)(2).ToString
                strUrutan = dsSubMenu.Tables(0).Rows(i)(3).ToString

                Dim laporan = dsSubMenu.Tables(0).Rows(i)(6).ToString
                If laporan = 1 Then
                    strLaporan = "Aktif"
                Else
                    strLaporan = "Tidak Aktif"
                End If

                strNama_Fail = dsSubMenu.Tables(0).Rows(i)(5).ToString

                Dim status = dsSubMenu.Tables(0).Rows(i)(4).ToString
                If status = 1 Then
                    strstatus = "Aktif"
                Else
                    strstatus = "Tidak Aktif"
                End If

                dt.Rows.Add(strKod_Sub_Menu, strNama_Sub_Menu, strDis_Sub_Menu, strLaporan, strUrutan, strNama_Fail, strstatus)
            Next

            Return dt

        Catch ex As Exception

        End Try

    End Function

    Protected Sub idModul_SelectedIndexChanged(sender As Object, e As EventArgs)
        Me.ClearGrid()
        Me.SurroundingSub()

        fCariSubModul()
        fBindSubModul("")



    End Sub

    Private Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.ServerClick
        Dim strSql As String
        Dim dbconn As New DBKewConn
        Try
            Dim strKodSub As String = ddlSubModul.SelectedValue
            Dim strKod_Sub_Menu As String = Trim(txtKodSubMenu.Value.ToUpper.TrimEnd)
            Dim strNama_Sub_Menu As String = Trim(txtNamaSubMenu.Value.ToString.TrimEnd)
            Dim strDis_Sub_Menu As String = Trim(txtKeterangan.Value.ToUpper.TrimEnd)
            Dim strNama_Fail As String = Trim(txtNamaFail.Value.ToUpper.TrimEnd)
            Dim strUrutan As String = Trim(txtUrutan.Value.ToUpper.TrimEnd)
            Dim strLaporan As String = rblLaporan.SelectedValue
            Dim strStatus As String = rblStatus.SelectedValue


            'If ViewState("SaveMode") = 1 Then
            'INSERT

            strSql = "select count(*) from SMKB_Sub_Menu where Kod_Sub_Menu = '" & strKod_Sub_Menu & "'"
            If fCheckRec(strSql) = 0 Then

                strSql = "insert into SMKB_Sub_Menu (Kod_Sub, Kod_Sub_Menu, Nama_Sub_Menu, Dis_Sub_Menu, Urutan, Laporan, Status, Nama_Fail) 
                    values (@Kod_Sub, @Kod_Sub_Menu, @Nama_Sub_Menu, @Dis_Sub_Menu, @Urutan, @Laporan, @Status, @Nama_Fail)"
                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@Kod_Sub", strKodSub),
                    New SqlParameter("@Kod_Sub_Menu", strKod_Sub_Menu),
                    New SqlParameter("@Nama_Sub_Menu", strNama_Sub_Menu),
                    New SqlParameter("@Dis_Sub_Menu", strDis_Sub_Menu),
                    New SqlParameter("@Urutan", strUrutan),
                    New SqlParameter("@Laporan", strLaporan),
                    New SqlParameter("@Status", strStatus),
                    New SqlParameter("@Nama_Fail", strNama_Fail)
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
                strSql = "update SMKB_Sub_Menu set Kod_Sub_Menu = @Kod_Sub_Menu, Nama_Sub_Menu = @Nama_Sub_Menu, Dis_Sub_Menu = @Dis_Sub_Menu, Urutan = @Urutan, Laporan = @Laporan, Status = @Status, Nama_Fail = @Nama_Fail
                where Kod_Sub_Menu = '" & strKod_Sub_Menu & "'"
                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@Kod_Sub_Menu", strKod_Sub_Menu),
                    New SqlParameter("@Nama_Sub_Menu", strNama_Sub_Menu),
                    New SqlParameter("@Dis_Sub_Menu", strDis_Sub_Menu),
                    New SqlParameter("@Urutan", strUrutan),
                    New SqlParameter("@Laporan", strLaporan),
                    New SqlParameter("@Status", strStatus),
                    New SqlParameter("@Nama_Fail", strNama_Fail)
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


    Private Sub gvSubMenu_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvSubMenu.RowCommand
        Try
            If e.CommandName = "Select" Then


                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvSubMenu.Rows(index)

                Dim strKodSubMenu As String = selectedRow.Cells(0).Text.ToString

                'Call sql
                Dim strSql As String = $"select Kod_Sub, Kod_Sub_Menu, Nama_Sub_Menu, Dis_Sub_Menu, Urutan, Laporan, Status, isnull(Nama_Fail,'-') Nama_Fail FROM SMKB_Sub_Menu 
                                WHERE Kod_Sub_Menu = '" & strKodSubMenu & "'"


                Dim dt As New DataTable
                dt = dbconn.fSelectCommandDt(strSql)
                If dt.Rows.Count > 0 Then

                    'Dim Master_Reference_code As String = dt.Rows(0)("Master_Reference_code")
                    fBindSubModul(ddlCariSubModul.SelectedValue)

                    ddlSubModul.SelectedValue = dt.Rows(0)("Kod_Sub")
                    txtKodSubMenu.Value = dt.Rows(0)("Kod_Sub_Menu")
                    txtNamaSubMenu.Value = dt.Rows(0)("Nama_Sub_Menu")
                    txtKeterangan.Value = dt.Rows(0)("Dis_Sub_Menu")
                    txtNamaFail.Value = dt.Rows(0)("Nama_Fail")
                    txtUrutan.Value = dt.Rows(0)("Urutan")

                    Dim laporan = dt.Rows(0)("laporan")
                    rblLaporan.SelectedValue = laporan


                    Dim status = dt.Rows(0)("status")
                    rblStatus.SelectedValue = status

                End If


                ClientScript.RegisterStartupScript([GetType](), "alert3", "ShowPopup('1');", True)


            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvSubMenu_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvSubMenu.PageIndexChanging
        gvSubMenu.PageIndex = e.NewPageIndex
        fBindGv()
    End Sub

    Private Sub fBindSubModul(kodKlasifikasi As String)
        Try
            Dim strsql As String
            Dim dataCarian = ddlCariModul.SelectedValue

            'strsql = $"SELECT Kod_Sub, Nama_Sub FROM SMKB_Sub_Modul WHERE Status = 1 ORDER BY Urutan"

            strsql = $"SELECT Kod_Sub, Nama_Sub, Dis_Sub, Nama_Icon, Urutan, Status
                    FROM SMKB_Sub_Modul
                    WHERE Status = 1 AND Kod_Modul = '" & ddlCariModul.SelectedValue & "' ORDER BY Urutan"

            Dim ds As New DataSet
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strsql)

            ddlSubModul.DataSource = ds
            ddlSubModul.DataTextField = "Nama_Sub"
            ddlSubModul.DataValueField = "Kod_Sub"
            ddlSubModul.DataBind()


            ddlSubModul.Items.Insert(0, New ListItem("- SILA PILIH -", 0))

            If kodKlasifikasi = "" Then
                ddlSubModul.SelectedIndex = 0
            Else
                ddlSubModul.Items.FindByValue(kodKlasifikasi).Selected = True
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fCariModul()
        Try
            Dim strsql As String


            strsql = $"SELECT Kod_Modul, Nama_Modul, Dis_Modul, Urutan, Status FROM SMKB_Modul WHERE Status = 1 ORDER BY Urutan"

            Dim ds As New DataSet
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strsql)
            'Dim strKod = Trim(ds.Tables(0).Rows(0)("Kod").ToString)

            ddlCariModul.DataSource = ds
            ddlCariModul.DataTextField = "Nama_Modul"
            ddlCariModul.DataValueField = "Kod_Modul"
            ddlCariModul.DataBind()

            ddlCariModul.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlCariModul.SelectedIndex = 0


        Catch ex As Exception

        End Try
    End Sub

    Private Sub fCariSubModul()
        Try
            Dim strsql As String

            strsql = $"SELECT Kod_Sub, Nama_Sub, Dis_Sub, Nama_Icon, Urutan, Status
                    FROM SMKB_Sub_Modul
                    WHERE Status = 1 AND Kod_Modul = '" & ddlCariModul.SelectedValue & "' ORDER BY Urutan"

            Dim ds As New DataSet
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strsql)
            'Dim strKod = Trim(ds.Tables(0).Rows(0)("Kod").ToString)

            ddlCariSubModul.DataSource = ds
            ddlCariSubModul.DataTextField = "Nama_Sub"
            ddlCariSubModul.DataValueField = "Kod_Sub"
            ddlCariSubModul.DataBind()

            ddlCariSubModul.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlCariSubModul.SelectedIndex = 0


        Catch ex As Exception

        End Try
    End Sub

    Private Sub lbtnCari_Click(sender As Object, e As EventArgs) Handles lbtnCari.ServerClick
        fBindGv()
    End Sub

    Private Sub ClearGrid()
        gvSubMenu.DataSource = Nothing
        gvSubMenu.DataBind()
    End Sub

    Private Sub SurroundingSub()

        'Using dbconn As New SqlConnection(strCon)

        Dim dt As New DataTable()
            dt.Columns.Add("Kod_Sub_Menu", GetType(String))
            dt.Columns.Add("Nama_Sub_Menu", GetType(String))
            dt.Columns.Add("Dis_Sub_Menu", GetType(String))
            dt.Columns.Add("Urutan", GetType(String))
            dt.Columns.Add("Nama_Fail", GetType(String))
            dt.Columns.Add("status", GetType(String))

            gvSubMenu.DataSource = dt
            gvSubMenu.DataBind()
        ' End Using

    End Sub
End Class