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
Imports System.Runtime.CompilerServices.RuntimeHelpers

Public Class Bank
    Inherits System.Web.UI.Page

    Public dsBank As New DataSet
    Public dvBank As New DataView
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
                gvBank.DataSource = New List(Of String)
            Else
                gvBank.DataSource = dt
            End If
            gvBank.DataBind()
            'lblJumRekod.InnerText = dt.Rows.Count


            'Required for jQuery DataTables to work.
            gvBank.UseAccessibleHeader = True
            gvBank.HeaderRow.TableSection = TableRowSection.TableHeader

        Catch ex As Exception
            'ex.Message
        End Try
    End Function

    Private Function fCreateDtKW() As DataTable

        Try

            Dim dt As New DataTable
            dt.Columns.Add("Kod_Bank", GetType(String))
            dt.Columns.Add("nama_sing", GetType(String))
            dt.Columns.Add("nama_bank", GetType(String))
            dt.Columns.Add("Alamat_1", GetType(String))
            dt.Columns.Add("Alamat_2", GetType(String))
            dt.Columns.Add("Bandar", GetType(String))
            dt.Columns.Add("Poskod", GetType(String))
            dt.Columns.Add("Kod_Negeri", GetType(String))
            dt.Columns.Add("Kod_Negara", GetType(String))
            dt.Columns.Add("Cawangan", GetType(String))
            dt.Columns.Add("No_Akaun", GetType(String))
            dt.Columns.Add("No_Tel1", GetType(String))
            dt.Columns.Add("No_Tel2", GetType(String))
            dt.Columns.Add("No_Faks", GetType(String))
            dt.Columns.Add("Pegawai", GetType(String))
            dt.Columns.Add("Emel", GetType(String))
            dt.Columns.Add("Web", GetType(String))

            Dim Kod_Bank As String
            Dim nama_sing As String
            Dim nama_bank As String
            Dim Alamat_1 As String
            Dim Alamat_2 As String
            Dim Bandar As String
            Dim Poskod As String
            Dim Kod_Negeri As String
            Dim Kod_Negara As String
            Dim Cawangan As String
            Dim No_Akaun As String
            Dim No_Tel1 As String
            Dim No_Tel2 As String
            Dim No_Faks As String
            Dim Pegawai As String
            Dim Emel As String
            Dim Web As String

            Dim strSql As String = $"select Kod_Bank, nama_sing, nama_bank, Alamat_1, Alamat_2, Bandar, Poskod, Kod_Negeri, Kod_Negara, isnull(Cawangan,'-') Cawangan, 
                            No_Akaun, No_Tel1, No_Tel2, No_Faks, Pegawai, Emel, Web
                            from SMKB_Bank_Master
                            order by Kod_Bank"


            dsBank = dbconn.fSelectCommand(strSql)
            dvBank = New DataView(dsBank.Tables(0))

            If dsBank.Tables(0).Rows.Count > 1 Then
                For i As Integer = 0 To dsBank.Tables(0).Rows.Count - 1

                    Kod_Bank = dsBank.Tables(0).Rows(i)(0).ToString
                    nama_sing = dsBank.Tables(0).Rows(i)(1).ToString
                    nama_bank = dsBank.Tables(0).Rows(i)(2).ToString
                    Alamat_1 = dsBank.Tables(0).Rows(i)(3).ToString
                    Alamat_2 = dsBank.Tables(0).Rows(i)(4).ToString

                    Bandar = dsBank.Tables(0).Rows(i)(5).ToString
                    Poskod = dsBank.Tables(0).Rows(i)(6).ToString
                    Kod_Negeri = dsBank.Tables(0).Rows(i)(7).ToString
                    Kod_Negara = dsBank.Tables(0).Rows(i)(8).ToString
                    Cawangan = dsBank.Tables(0).Rows(i)(9).ToString

                    No_Akaun = dsBank.Tables(0).Rows(i)(10).ToString
                    No_Tel1 = dsBank.Tables(0).Rows(i)(11).ToString
                    No_Tel2 = dsBank.Tables(0).Rows(i)(12).ToString
                    No_Faks = dsBank.Tables(0).Rows(i)(13).ToString
                    Pegawai = dsBank.Tables(0).Rows(i)(14).ToString

                    Emel = dsBank.Tables(0).Rows(i)(15).ToString
                    Web = dsBank.Tables(0).Rows(i)(16).ToString

                    dt.Rows.Add(Kod_Bank, nama_sing, nama_bank, Alamat_1, Alamat_2, Bandar, Poskod, Kod_Negeri, Kod_Negara, Cawangan, No_Akaun, No_Tel1, No_Tel2, No_Faks, Pegawai, Emel, Web)
                Next
            Else
                Throw New Exception("Unable to create Data Table value.")
            End If

            Return dt

        Catch ex As Exception

        End Try

    End Function

    Private Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.ServerClick
        Dim strSql As String
        Dim dbconn As New DBKewConn
        Try
            Dim strKod_Bank As String = Trim(txtKodBank.Value.TrimEnd)
            Dim strnama_sing As String = Trim(txtSingkatan.Value.TrimEnd)
            Dim strnama_bank As String = Trim(txtNamaBank.Value.TrimEnd)
            Dim strAlamat_1 As String = Trim(txtAlamat1.Value.TrimEnd)
            Dim strAlamat_2 As String = Trim(txtAlamat2.Value.TrimEnd)
            Dim strBandar As String = Trim(txtBandar.Value.TrimEnd)
            Dim strPoskod As String = Trim(txtPoskod.Value.TrimEnd)
            Dim strKod_Negeri As String = Trim(txtNegeri.Value.TrimEnd)
            Dim strKod_Negara As String = Trim(txtNegara.Value.TrimEnd)
            Dim strCawangan As String = Trim(txtCawangan.Value.TrimEnd)
            Dim strNo_Akaun As String = Trim(txtNoAkaun.Value.TrimEnd)
            Dim strNo_Tel1 As String = Trim(txtTel1.Value.TrimEnd)
            Dim strNo_Tel2 As String = Trim(txtTel2.Value.TrimEnd)
            Dim strNo_Faks As String = Trim(txtFaks.Value.TrimEnd)
            Dim strPegawai As String = Trim(txtPegawai.Value.TrimEnd)
            Dim strEmel As String = Trim(txtEmel.Value.TrimEnd)
            Dim strWeb As String = Trim(txtWeb.Value.TrimEnd)

            'If ViewState("SaveMode") = 1 Then
            'INSERT

            strSql = "select count(*) from SMKB_Bank_Master where Kod_Bank = '" & strKod_Bank & "'"
            If fCheckRec(strSql) = 0 Then

                strSql = "insert into SMKB_Bank_Master (Kod_Bank, nama_sing, nama_bank, Alamat_1, Alamat_2, Bandar, Poskod, Kod_Negeri, Kod_Negara, Cawangan, No_Akaun, No_Tel1, No_Tel2, No_Faks, Pegawai, Emel, Web) 
                    values (@Kod_Bank, @nama_sing, @nama_bank, @Alamat_1, @Alamat_2, @Bandar, @Poskod, @Kod_Negeri, @Kod_Negara, @Cawangan, @No_Akaun, @No_Tel1, @No_Tel2, @No_Faks, @Pegawai, @Emel, @Web)"
                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@Kod_Bank", strKod_Bank),
                    New SqlParameter("@nama_sing", strnama_sing),
                    New SqlParameter("@nama_bank", strnama_bank),
                    New SqlParameter("@Alamat_1", strAlamat_1),
                    New SqlParameter("@Alamat_2", strAlamat_2),
                    New SqlParameter("@Bandar", strBandar),
                    New SqlParameter("@Poskod", strPoskod),
                    New SqlParameter("@Kod_Negeri", strKod_Negeri),
                    New SqlParameter("@Kod_Negara", strKod_Negara),
                    New SqlParameter("@Cawangan", strCawangan),
                    New SqlParameter("@No_Akaun", strNo_Akaun),
                    New SqlParameter("@No_Tel1", strNo_Tel1),
                    New SqlParameter("@No_Tel2", strNo_Tel2),
                    New SqlParameter("@No_Faks", strNo_Faks),
                    New SqlParameter("@Pegawai", strPegawai),
                    New SqlParameter("@Emel", strEmel),
                    New SqlParameter("@Web", strWeb)
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
                strSql = "update SMKB_Bank_Master set nama_sing = @nama_sing, nama_bank = @nama_bank, Alamat_1 = @Alamat_1, Alamat_2 = @Alamat_2, Bandar = @Bandar, Poskod = @Poskod, 
                        Kod_Negeri = @Kod_Negeri, Kod_Negara = @Kod_Negara, Cawangan = @Cawangan, No_Akaun =@No_Akaun, No_Tel1 = @No_Tel1, No_Tel2 = @No_Tel2, No_Faks = @No_Faks,
                        Pegawai = @Pegawai, Emel = @Emel, Web =@Web
                        where Kod_Bank = '" & strKod_Bank & "'"
                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@nama_sing", strnama_sing),
                    New SqlParameter("@nama_bank", strnama_bank),
                    New SqlParameter("@Alamat_1", strAlamat_1),
                    New SqlParameter("@Alamat_2", strAlamat_2),
                    New SqlParameter("@Bandar", strBandar),
                    New SqlParameter("@Poskod", strPoskod),
                    New SqlParameter("@Kod_Negeri", strKod_Negeri),
                    New SqlParameter("@Kod_Negara", strKod_Negara),
                    New SqlParameter("@Cawangan", strCawangan),
                    New SqlParameter("@No_Akaun", strNo_Akaun),
                    New SqlParameter("@No_Tel1", strNo_Tel1),
                    New SqlParameter("@No_Tel2", strNo_Tel2),
                    New SqlParameter("@No_Faks", strNo_Faks),
                    New SqlParameter("@Pegawai", strPegawai),
                    New SqlParameter("@Emel", strEmel),
                    New SqlParameter("@Web", strWeb)
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


    Private Sub gvBank_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvBank.RowCommand
        Try
            If e.CommandName = "Select" Then


                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvBank.Rows(index)

                Dim strKod As String = selectedRow.Cells(0).Text.ToString

                'Call sql
                Dim strSql As String = $"select Kod_Bank, nama_sing, nama_bank, Alamat_1, Alamat_2, Bandar, Poskod, Kod_Negeri, Kod_Negara, isnull(Cawangan,'-') Cawangan, 
                                    No_Akaun, No_Tel1, No_Tel2, No_Faks, Pegawai, Emel, Web
                                    from SMKB_Bank_Master
                                    where Kod_Bank = '{strKod}'"

                Dim dt As New DataTable
                dt = dbconn.fSelectCommandDt(strSql)
                If dt.Rows.Count > 0 Then

                    txtKodBank.Value = dt.Rows(0)("Kod_Bank")
                    txtSingkatan.Value = dt.Rows(0)("nama_sing")
                    txtNamaBank.Value = dt.Rows(0)("nama_bank")
                    txtAlamat1.Value = dt.Rows(0)("Alamat_1")
                    txtAlamat2.Value = dt.Rows(0)("Alamat_2")
                    txtBandar.Value = dt.Rows(0)("Bandar")

                    txtNegeri.Value = dt.Rows(0)("Kod_Negeri")
                    txtNegara.Value = dt.Rows(0)("Kod_Negara")
                    txtCawangan.Value = dt.Rows(0)("Cawangan")
                    txtNoAkaun.Value = dt.Rows(0)("No_Akaun")
                    txtTel1.Value = dt.Rows(0)("No_Tel1")
                    txtTel2.Value = dt.Rows(0)("No_Tel2")
                    txtFaks.Value = dt.Rows(0)("No_Faks")
                    txtPegawai.Value = dt.Rows(0)("Pegawai")
                    txtEmel.Value = dt.Rows(0)("Emel")
                    txtWeb.Value = dt.Rows(0)("Web")

                    'Dim status = dt.Rows(0)("status")
                    'rblStatus.SelectedValue = status

                End If

                ClientScript.RegisterStartupScript([GetType](), "alert", "ShowPopup('1');", True)


            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvBank_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvBank.PageIndexChanging
        gvBank.PageIndex = e.NewPageIndex
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