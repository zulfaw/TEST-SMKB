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

Public Class DaftarSubModul
    Inherits System.Web.UI.Page

    Public dsSubModul As New DataSet
    Public dvSubModul As New DataView
    Dim dbconn As New DBKewConn
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            fCariModul()
            fBindModul("")
            fBindGv()
            fddlNamaIcon("")
        End If
    End Sub

    Private Function fBindGv()

        Try

            Dim dt As New DataTable
            dt = fCreateDtModul()

            If dt.Rows.Count = 0 Then
                gvSubModul.DataSource = New List(Of String)
            Else
                gvSubModul.DataSource = dt
            End If
            gvSubModul.DataBind()

            'Required for jQuery DataTables to work.
            gvSubModul.UseAccessibleHeader = True
            gvSubModul.HeaderRow.TableSection = TableRowSection.TableHeader

        Catch ex As Exception

        End Try
    End Function

    Private Function fCreateDtModul() As DataTable

        Try

            Dim dt As New DataTable
            dt.Columns.Add("Kod_Sub", GetType(String))
            dt.Columns.Add("Nama_Sub", GetType(String))
            dt.Columns.Add("Dis_Sub", GetType(String))
            dt.Columns.Add("Nama_Icon", GetType(String))
            dt.Columns.Add("Urutan", GetType(String))
            dt.Columns.Add("status", GetType(String))

            Dim strKod_Sub As String
            Dim strNama_Sub As String
            Dim strDis_Sub As String
            Dim strNama_Icon As String
            Dim strUrutan As String
            Dim strstatus As String

            Dim dataCarian = ddlCariModul.SelectedValue

            Dim strSql As String = $"SELECT Kod_Sub, Nama_Sub, Dis_Sub, Nama_Icon, Urutan, isnull(Status,'0') Status FROM SMKB_Sub_Modul WHERE Kod_Modul = '{dataCarian}' ORDER BY Kod_Sub"

            dsSubModul = dbconn.fSelectCommand(strSql)
            dvSubModul = New DataView(dsSubModul.Tables(0))

            For i As Integer = 0 To dsSubModul.Tables(0).Rows.Count - 1

                strKod_Sub = dsSubModul.Tables(0).Rows(i)(0).ToString
                strNama_Sub = dsSubModul.Tables(0).Rows(i)(1).ToString
                strDis_Sub = dsSubModul.Tables(0).Rows(i)(2).ToString

                strNama_Icon = dsSubModul.Tables(0).Rows(i)(3).ToString
                strUrutan = dsSubModul.Tables(0).Rows(i)(4).ToString

                Dim status = dsSubModul.Tables(0).Rows(i)(5).ToString

                If status = "" Then
                    status = "0"
                End If
                If status = 1 Then
                    strstatus = "Aktif"
                Else
                    strstatus = "Tidak Aktif"
                End If

                dt.Rows.Add(strKod_Sub, strNama_Sub, strDis_Sub, strNama_Icon, strUrutan, StrStatus)
            Next

            Return dt

        Catch ex As Exception

        End Try

    End Function

    Private Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.ServerClick
        Dim strSql As String
        Dim dbconn As New DBKewConn
        Try
            Dim strKodSub As String = Trim(txtKodSub.Value.TrimEnd)
            Dim strNama_Sub As String = Trim(txtNamaSub.Value.ToUpper.TrimEnd)
            Dim strModul As String = ddlModul.SelectedValue
            Dim strDis_Sub As String = Trim(txtKeterangan.Value.ToUpper.TrimEnd)
            Dim strNama_Icon As String = ddlNamaIcon.SelectedValue
            Dim strUrutan As String = Trim(txtUrutan.Value.ToUpper.TrimEnd)
            Dim strStatus As String = rblStatus.SelectedValue


            'If ViewState("SaveMode") = 1 Then
            'INSERT

            strSql = "select count(*) from SMKB_Sub_Modul where Kod_Modul = '" & strModul & "' and Kod_Sub ='" & strKodSub & "'"
            If fCheckRec(strSql) = 0 Then

                strSql = "insert into SMKB_Sub_Modul (Kod_Modul, Kod_Sub, Nama_Sub, Dis_Sub, Nama_Icon, Urutan, Status) 
                    values (@Kod_Modul, @Kod_Sub, @Nama_Sub, @Dis_Sub, @Nama_Icon, @Urutan, @Status)"
                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@Kod_Modul", strModul),
                    New SqlParameter("@Kod_Sub", strKodSub),
                    New SqlParameter("@Nama_Sub", strNama_Sub),
                    New SqlParameter("@Dis_Sub", strDis_Sub),
                    New SqlParameter("@Nama_Icon", strNama_Icon),
                    New SqlParameter("@Urutan", strUrutan),
                    New SqlParameter("@Status", strStatus)
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
                strSql = "update SMKB_Sub_Modul set Kod_Sub = @Kod_Sub, Nama_Sub = @Nama_Sub, Dis_Sub = @Dis_Sub, Nama_Icon = @Nama_Icon, Urutan = @Urutan, status = @status
                where Kod_Modul = '" & strModul & "' and Kod_Sub ='" & strKodSub & "'"
                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@Kod_Sub", strKodSub),
                    New SqlParameter("@Nama_Sub", strNama_Sub),
                    New SqlParameter("@Dis_Sub", strDis_Sub),
                    New SqlParameter("@Nama_Icon", strNama_Icon),
                    New SqlParameter("@Urutan", strUrutan),
                    New SqlParameter("@Status", strStatus)
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


    Private Sub gvSubModul_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvSubModul.RowCommand
        Try
            If e.CommandName = "Select" Then


                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvSubModul.Rows(index)

                Dim strKod As String = selectedRow.Cells(0).Text.ToString

                'Call sql
                Dim strSql As String = $"select Kod_Sub, Nama_Sub, Dis_Sub, Nama_Icon, Urutan, Status FROM SMKB_Sub_Modul 
                                WHERE Kod_Sub = '{strKod}' AND Kod_Modul ='" & ddlCariModul.SelectedValue & "'"


                Dim dt As New DataTable
                dt = dbconn.fSelectCommandDt(strSql)
                If dt.Rows.Count > 0 Then

                    'Dim Master_Reference_code As String = dt.Rows(0)("Master_Reference_code")
                    fBindModul(ddlCariModul.SelectedValue)


                    txtKodSub.Value = dt.Rows(0)("Kod_Sub")
                    txtNamaSub.Value = dt.Rows(0)("Nama_Sub")
                    txtKeterangan.Value = dt.Rows(0)("Dis_Sub")

                    fddlNamaIcon(dt.Rows(0)("Nama_Icon"))
                    txtUrutan.Value = dt.Rows(0)("Urutan")

                    Dim status = dt.Rows(0)("status")
                    rblStatus.SelectedValue = status

                End If


                ClientScript.RegisterStartupScript([GetType](), "alert3", "ShowPopup('1');", True)


            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvSubModul_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvSubModul.PageIndexChanging
        gvSubModul.PageIndex = e.NewPageIndex
        fBindGv()
    End Sub


    Private Sub fBindModul(kodKlasifikasi As String)
        Try
            Dim strsql As String
            Dim dataCarian = ddlCariModul.SelectedValue

            strsql = $"SELECT Kod_Modul, Nama_Modul, Dis_Modul, Urutan, Icon_Location, Status FROM SMKB_Modul WHERE Status = 1 ORDER BY Urutan"

            Dim ds As New DataSet
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strsql)

            ddlModul.DataSource = ds
            ddlModul.DataTextField = "Nama_Modul"
            ddlModul.DataValueField = "Kod_Modul"
            ddlModul.DataBind()


            ddlModul.Items.Insert(0, New ListItem("- SILA PILIH -", 0))

            'If kodKlasifikasi = "" Then
            'ddlModul.SelectedIndex = 0
            'Else
            ddlModul.Items.FindByValue(ddlCariModul.SelectedValue).Selected = True
            'End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fCariModul()
        Try
            Dim strsql As String


            strsql = $"SELECT Kod_Modul, Nama_Modul, Dis_Modul, Urutan, Icon_Location, Status FROM SMKB_Modul WHERE Status = 1 ORDER BY Urutan"

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

    Private Sub fddlNamaIcon(kodKlasifikasi As String)
        Try
            Dim strsql As String

            strsql = $"select butiran from SMKB_Lookup_Detail
                    where kod = '0143'
                    order by butiran"

            Dim ds As New DataSet
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strsql)
            'Dim strKod = Trim(ds.Tables(0).Rows(0)("Kod").ToString)

            ddlNamaIcon.DataSource = ds
            ddlNamaIcon.DataTextField = "butiran"
            ddlNamaIcon.DataValueField = "butiran"
            ddlNamaIcon.DataBind()

            ddlNamaIcon.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            If kodKlasifikasi = "" Then
                ddlNamaIcon.SelectedIndex = 0
            Else
                ddlNamaIcon.Items.FindByValue(kodKlasifikasi).Selected = True
            End If



        Catch ex As Exception

        End Try
    End Sub

    Private Sub lbtnCari_Click(sender As Object, e As EventArgs) Handles lbtnCari.ServerClick
        fBindGv()
    End Sub



End Class