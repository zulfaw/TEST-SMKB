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

Public Class testAirv4
    Inherits System.Web.UI.Page

    Public dsMaklumatKorporat As New DataSet
    Public dvMaklumatKorporat As New DataView
    Dim dbconn As New DBKewConn
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fBindGv()
            'btnUpload.Visible = False
        End If
    End Sub
    Private Function fBindGv()

        Try

            Dim dt As New DataTable
            dt = fCreateDtMaklumatKorporat()

            If dt.Rows.Count = 0 Then
                gvMaklumatKorporat.DataSource = New List(Of String)
            Else
                gvMaklumatKorporat.DataSource = dt
            End If
            gvMaklumatKorporat.DataBind()
            'lblJumRekod.InnerText = dt.Rows.Count


            'Required for jQuery DataTables to work.
            gvMaklumatKorporat.UseAccessibleHeader = True
            gvMaklumatKorporat.HeaderRow.TableSection = TableRowSection.TableHeader

        Catch ex As Exception
            'ex.Message
        End Try
    End Function

    Private Function fCreateDtMaklumatKorporat() As DataTable

        Try

            Dim dt As New DataTable
            dt.Columns.Add("Nama", GetType(String))
            dt.Columns.Add("Nama_Sing", GetType(String))
            dt.Columns.Add("no_tel1", GetType(String))
            dt.Columns.Add("no_faks1", GetType(String))
            dt.Columns.Add("Laman_Web", GetType(String))
            dt.Columns.Add("Emel", GetType(String))



            Dim Nama As String
            Dim Nama_Sing As String
            Dim no_tel1 As String
            Dim no_faks1 As String
            Dim Laman_Web As String
            Dim Emel As String

            Dim strSql As String = $"select Nama, Nama_Sing, no_tel1, no_faks1, Laman_Web, Emel
                            from SMKB_Korporat"


            dsMaklumatKorporat = dbconn.fSelectCommand(strSql)
            dvMaklumatKorporat = New DataView(dsMaklumatKorporat.Tables(0))

            For i As Integer = 0 To dsMaklumatKorporat.Tables(0).Rows.Count - 1

                Nama = dsMaklumatKorporat.Tables(0).Rows(i)(0).ToString
                Nama_Sing = dsMaklumatKorporat.Tables(0).Rows(i)(1).ToString
                no_tel1 = dsMaklumatKorporat.Tables(0).Rows(i)(2).ToString
                no_faks1 = dsMaklumatKorporat.Tables(0).Rows(i)(3).ToString
                Laman_Web = dsMaklumatKorporat.Tables(0).Rows(i)(4).ToString
                Emel = dsMaklumatKorporat.Tables(0).Rows(i)(5).ToString

                dt.Rows.Add(Nama, Nama_Sing, no_tel1, no_faks1, Laman_Web, Emel)
            Next

            Return dt

        Catch ex As Exception

        End Try

    End Function
    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Get the file name and extension
        Dim fileName As String = Path.GetFileName(fileUploadControl.FileName)
        Dim extension As String = Path.GetExtension(fileName)

        ' Validate the file extension
        If extension.ToLower() = ".jpg" OrElse extension.ToLower() = ".jpeg" OrElse extension.ToLower() = ".png" Then
            ' Convert the file to binary data
            Dim fileData As Byte() = ConvertFileToBinary(fileUploadControl.FileContent)

            ' Update the database with the binary data
            UpdateDatabase(fileData)
        Else
            ' Invalid file extension
            'lblMessage.Text = "Invalid file extension. Please upload a JPG or PNG image."
        End If
    End Sub

    Function ConvertFileToBinary(ByVal fileStream As Stream) As Byte()
        ' Convert the file to binary data
        Using binaryReader As New BinaryReader(fileStream)
            Return binaryReader.ReadBytes(CInt(fileStream.Length))
        End Using
    End Function


    Sub UpdateDatabase(ByVal logoData As Byte())
        ' Connect to the database
        Dim connectionString As String = strConStaf
        Dim connection As New SqlConnection(connectionString)
        connection.Open()

        Dim strNamaSing As String = "00270"


        ' Update the database with the binary data
        Dim sqlCommand As New SqlCommand()
        sqlCommand.Connection = connection
        sqlCommand.CommandText = "UPDATE MS01_Peribadi SET MS01_Gambar = @logo WHERE MS01_NoStaf ='" & strNamaSing & "'"

        'Dim namaSing As String = "your_nama_sing_value_here"

        sqlCommand.Parameters.Add("@logo", SqlDbType.Binary).Value = logoData


        sqlCommand.ExecuteNonQuery()

        ' Close the database connection
        connection.Close()

        ' Display a success message
        'lblMessage.Text = "The image was uploaded and updated in the database."
    End Sub

    Private Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.ServerClick
        Dim strSql As String
        Dim dbconn As New DBKewConn


        Try
            Dim strNamaSing As String = Trim(txtSingkatan.Value.TrimEnd)
            Dim strNama As String = Trim(txtNama.Value.ToUpper.TrimEnd)
            Dim strAlmt1 As String = Trim(txtAlamat1.Value.ToUpper.TrimEnd)
            Dim strAlmt2 As String = Trim(txtAlamat2.Value.ToUpper.TrimEnd)
            Dim strBandar As String = Trim(txtBandar.Value.ToUpper.TrimEnd)
            Dim strPoskod As String = Trim(txtPoskod.Value.ToUpper.TrimEnd)
            Dim strKodNegeri As String = Trim(txtNegeri.Value.ToUpper.TrimEnd)
            Dim strKodNegara As String = Trim(txtNegara.Value.ToUpper.TrimEnd)
            Dim strNoTel1 As String = Trim(txtTel1.Value.ToUpper.TrimEnd)
            Dim strNoTel2 As String = Trim(txtTel2.Value.ToUpper.TrimEnd)
            Dim strNoFaks As String = Trim(txtFaks1.Value.ToUpper.TrimEnd)
            Dim strLamanWeb As String = Trim(txtWeb.Value.ToUpper.TrimEnd)
            Dim strEmel As String = Trim(txtEmel.Value.ToUpper.TrimEnd)

            strSql = "select count(*) from SMKB_Korporat where Nama_Sing = '" & strNamaSing & "'"
            If fCheckRec(strSql) = 0 Then

                strSql = "insert into SMKB_Korporat (Nama_Sing, Nama, Almt1, Almt2, Bandar, Poskod, Kod_Negeri, Kod_Negara, No_Tel1, No_Tel2, No_Faks1, Laman_Web,Emel) 
                    values (@Nama_Sing, @Nama, @Almt1, @Almt2, @Bandar, @Poskod, @Kod_Negeri, @Kod_Negara, @No_Tel1, @No_Tel2, @No_Faks1, @Laman_Web, @Emel)"
                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@Nama", strNamaSing),
                    New SqlParameter("@Nama", strNama),
                    New SqlParameter("@Almt1", strAlmt1),
                    New SqlParameter("@Almt2", strAlmt2),
                    New SqlParameter("@Bandar", strBandar),
                    New SqlParameter("@Poskod", strPoskod),
                    New SqlParameter("@Kod_Negeri", strKodNegeri),
                    New SqlParameter("@Kod_Negara", strKodNegara),
                    New SqlParameter("@No_Tel1", strNoTel1),
                    New SqlParameter("@No_Tel2", strNoTel2),
                    New SqlParameter("@No_Faks1", strNoFaks),
                    New SqlParameter("@Laman_Web", strLamanWeb),
                    New SqlParameter("@Emel", strEmel)
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

            Else
                'UPDATE
                'update logo to data binary----------------------
                btnUpload_Click(btnUpload, EventArgs.Empty)
                '------------------------------------------------

                strSql = "update SMKB_Korporat set Nama = @Nama, Almt1 = @Almt1, Almt2 = @Almt2, Bandar = @Bandar, Poskod = @Poskod, 
                    Kod_Negeri = @Kod_Negeri, Kod_Negara = @Kod_Negara, No_Tel1  =@No_Tel1,  No_Tel2 = @No_Tel2, No_Faks1 = @No_Faks1, Laman_Web =Laman_Web ,Emel = @Emel
                    where Nama_Sing = '" & strNamaSing & "'"

                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@Nama", strNama),
                    New SqlParameter("@Almt1", strAlmt1),
                    New SqlParameter("@Almt2", strAlmt2),
                    New SqlParameter("@Bandar", strBandar),
                    New SqlParameter("@Poskod", strPoskod),
                    New SqlParameter("@Kod_Negeri", strKodNegeri),
                    New SqlParameter("@Kod_Negara", strKodNegara),
                    New SqlParameter("@No_Tel1", strNoTel1),
                    New SqlParameter("@No_Tel2", strNoTel2),
                    New SqlParameter("@No_Faks1", strNoFaks),
                    New SqlParameter("@Laman_Web", strLamanWeb),
                    New SqlParameter("@Emel", strEmel)
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


    Private Sub gvMaklumatKorporat_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvMaklumatKorporat.RowCommand
        Try
            If e.CommandName = "Select" Then


                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvMaklumatKorporat.Rows(index)

                Dim strKod As String = selectedRow.Cells(0).Text.ToString
                'txtDescription.Value = selectedRow.Cells(1).Text.ToString


                'Call sql
                Dim strSql As String = $"select Nama, Nama_Sing, Almt1, Almt2, Bandar, Poskod, Kod_Negeri, Kod_Negara, no_tel1,	no_tel2, no_faks1, Laman_Web, Logo, Emel, Kategori, No_GST
                            from SMKB_Korporat
                            where Nama_Sing = '{strKod}'"

                Dim dt As New DataTable
                dt = dbconn.fSelectCommandDt(strSql)
                If dt.Rows.Count > 0 Then

                    txtSingkatan.Value = dt.Rows(0)("Nama_Sing")
                    txtNama.Value = dt.Rows(0)("Nama")
                    txtAlamat1.Value = dt.Rows(0)("Almt1")
                    txtAlamat2.Value = dt.Rows(0)("Almt2")
                    txtBandar.Value = dt.Rows(0)("Bandar")
                    txtPoskod.Value = dt.Rows(0)("Poskod")
                    txtNegeri.Value = dt.Rows(0)("Kod_Negeri")
                    txtNegara.Value = dt.Rows(0)("Kod_Negara")
                    txtTel1.Value = dt.Rows(0)("no_tel1")
                    txtTel2.Value = dt.Rows(0)("no_tel2")
                    txtFaks1.Value = dt.Rows(0)("no_faks1")
                    txtWeb.Value = dt.Rows(0)("Laman_Web")
                    txtEmel.Value = dt.Rows(0)("Emel")

                    Dim imageData As Byte() = DirectCast(dt.Rows(0)("Logo"), Byte())
                    Dim base64String As String = Convert.ToBase64String(imageData)

                    ' Set the ImageUrl property of the Image control to display the image
                    imgMyImage.ImageUrl = String.Format("data:image/jpg;base64,{0}", base64String)


                End If

                ClientScript.RegisterStartupScript([GetType](), "alert", "ShowPopup('1');", True)


            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvMaklumatKorporat_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvMaklumatKorporat.PageIndexChanging
        gvMaklumatKorporat.PageIndex = e.NewPageIndex
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