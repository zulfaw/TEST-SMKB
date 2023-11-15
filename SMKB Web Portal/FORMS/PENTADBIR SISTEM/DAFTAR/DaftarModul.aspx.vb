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
Imports System.IO

Public Class DaftarModul
    Inherits System.Web.UI.Page

    Public dsModul As New DataSet
    Public dvModul As New DataView
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
                gvModul.DataSource = New List(Of String)
            Else
                gvModul.DataSource = dt
            End If
            gvModul.DataBind()
            'lblJumRekod.InnerText = dt.Rows.Count


            'Required for jQuery DataTables to work.
            gvModul.UseAccessibleHeader = True
            gvModul.HeaderRow.TableSection = TableRowSection.TableHeader

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Private Function fCreateDtKW() As DataTable

        Try

            Dim dt As New DataTable
            dt.Columns.Add("Kod_Modul", GetType(String))
            dt.Columns.Add("Nama_Modul", GetType(String))
            dt.Columns.Add("Dis_Modul", GetType(String))
            dt.Columns.Add("Urutan", GetType(String))
            dt.Columns.Add("Icon_Location", GetType(String))
            dt.Columns.Add("Status", GetType(String))

            Dim strKod_Modul As String
            Dim strNama_Modul As String
            Dim strDis_Modul As String
            Dim strUrutan As String
            Dim strIcon_Location As String
            Dim Status As String

            Dim strSql As String = $"SELECT Kod_Modul, Nama_Modul, Dis_Modul, Urutan, Icon_Location, Status FROM SMKB_Modul
                        ORDER BY Kod_Modul"


            dsModul = dbconn.fSelectCommand(strSql)
            dvModul = New DataView(dsModul.Tables(0))

            For i As Integer = 0 To dsModul.Tables(0).Rows.Count - 1

                strKod_Modul = dsModul.Tables(0).Rows(i)(0).ToString
                strNama_Modul = dsModul.Tables(0).Rows(i)(1).ToString
                strDis_Modul = dsModul.Tables(0).Rows(i)(2).ToString
                strUrutan = dsModul.Tables(0).Rows(i)(3).ToString
                strIcon_Location = dsModul.Tables(0).Rows(i)(4).ToString

                Dim StrStatus = dsModul.Tables(0).Rows(i)(5).ToString
                If StrStatus = 1 Then
                    Status = "Aktif"
                Else
                    Status = "Tidak Aktif"
                End If

                dt.Rows.Add(strKod_Modul, strNama_Modul, strDis_Modul, strUrutan, strIcon_Location, Status)
            Next

            Return dt

        Catch ex As Exception

        End Try

    End Function

    Private Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.ServerClick
        Dim strSql As String
        Dim dbconn As New DBKewConn
        Try
            Dim strKod_Modul As String = Trim(txtKodModul.Value.TrimEnd)
            Dim strNama_Modul As String = Trim(txtNamaModul.Value.TrimEnd)
            Dim strDis_Modul As String = Trim(txtDis_Modul.Value.TrimEnd)
            Dim strUrutan As String = Trim(txtUrutan.Value.TrimEnd)
            'Dim strIcon_Location As String = Trim(txtIcon_Location.Value.TrimEnd)
            Dim strStatus As String = Trim(rblStatus.SelectedValue)


            'If ViewState("SaveMode") = 1 Then
            'INSERT
            Dim strIcon_Location As String = ""
            If fileUploadControl.HasFile Then
                Dim fileName As String = Path.GetFileName(fileUploadControl.FileName)
                Dim savePath As String = "./assets/icon/" 'Server.MapPath("./assets/icon/") ' Set the destination folder
                strIcon_Location = Path.Combine(savePath, fileName)

                'fileUploadControl.SaveAs(filePath)
                ' You can add additional logic here, such as saving the file path to a database or displaying a success message.
            End If


            strSql = "select count(*) from SMKB_Modul where Kod_Modul = '" & strKod_Modul & "'"
            If fCheckRec(strSql) = 0 Then


                Dim strNewKodModul As String = getKodModul()

                strSql = "insert into SMKB_Modul (Kod_Modul, Nama_Modul, Dis_Modul, Urutan, Icon_Location, Status) 
                    values (@Kod_Modul, @Nama_Modul, @Dis_Modul, @Urutan, @Icon_Location, @Status)"
                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@Kod_Modul", strNewKodModul),
                    New SqlParameter("@Nama_Modul", strNama_Modul),
                    New SqlParameter("@Dis_Modul", strDis_Modul),
                    New SqlParameter("@Urutan", strUrutan),
                    New SqlParameter("@Icon_Location", strIcon_Location),
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


                If fileUploadControl.HasFile Then
                    Dim fileName As String = Path.GetFileName(fileUploadControl.FileName)
                    Dim savePath As String = "./assets/icon/" 'Server.MapPath("./assets/icon/") ' Set the destination folder
                    strIcon_Location = Path.Combine(savePath, fileName)

                    'fileUploadControl.SaveAs(filePath)
                    ' You can add additional logic here, such as saving the file path to a database or displaying a success message.
                End If


                strSql = "update SMKB_Modul set Nama_Modul = @Nama_Modul, Dis_Modul = @Dis_Modul, Urutan = @Urutan, Icon_Location = @Icon_Location, status = @Status
                where Kod_Modul = '" & strKod_Modul & "'"
                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@Nama_Modul", strNama_Modul),
                    New SqlParameter("@Dis_Modul", strDis_Modul),
                    New SqlParameter("@Urutan", strUrutan),
                    New SqlParameter("@Icon_Location", strIcon_Location),
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


    Private Sub gvModul_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvModul.RowCommand
        Try
            If e.CommandName = "Select" Then


                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvModul.Rows(index)

                Dim strKodModul As String = selectedRow.Cells(0).Text.ToString

                'Call sql
                Dim strSql As String = $"SELECT Kod_Modul, Nama_Modul, Dis_Modul, Urutan, Icon_Location, Status FROM SMKB_Modul            
                            where Kod_Modul = '{strKodModul}'"

                Dim dt As New DataTable
                dt = dbconn.fSelectCommandDt(strSql)
                If dt.Rows.Count > 0 Then

                    txtKodModul.Value = dt.Rows(0)("Kod_Modul")
                    txtNamaModul.Value = dt.Rows(0)("Nama_Modul")
                    txtDis_Modul.Value = dt.Rows(0)("Dis_Modul")
                    txtUrutan.Value = dt.Rows(0)("Urutan")
                    txtIcon_Location.Value = dt.Rows(0)("Icon_Location")

                    Dim status = dt.Rows(0)("status")
                    rblStatus.SelectedValue = status

                End If




                ClientScript.RegisterStartupScript([GetType](), "alert", "ShowPopup('1');", True)


            End If
        Catch ex As Exception

        End Try
    End Sub

    Function getKodModul() As String
        Dim sql As String
        Using dbconn As New SqlConnection(strCon)
            dbconn.Open()
            Dim kodModul As String = ""
            '----Dapatkan Penyelia
            sql = "SELECT TOP 1 CONVERT(INT, Kod_Modul) + 1 AS Next_Kod_Modul
FROM SMKB_Modul
ORDER BY Kod_Modul DESC"

            dbcomm = New SqlCommand(sql, dbconn)
            dbread = dbcomm.ExecuteReader()
            dbread.Read()

            If dbread.HasRows Then
                kodModul = dbread("Next_Kod_Modul")

            End If
            dbread.Close()

            Return kodModul
        End Using

    End Function


    Private Sub gvModul_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvModul.PageIndexChanging
        gvModul.PageIndex = e.NewPageIndex
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

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        If fileUploadControl.HasFile Then
            Dim fileName As String = Path.GetFileName(fileUploadControl.FileName)
            Dim savePath As String = Server.MapPath("./assets/icon/") ' Set the destination folder
            Dim filePath As String = Path.Combine(savePath, fileName)

            fileUploadControl.SaveAs(filePath)
            ' You can add additional logic here, such as saving the file path to a database or displaying a success message.
        End If
    End Sub


End Class