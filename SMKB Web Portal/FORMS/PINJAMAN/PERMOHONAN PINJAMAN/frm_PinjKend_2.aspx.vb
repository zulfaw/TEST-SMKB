Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Collections
Imports System.Web.HttpFileCollection

Public Class frm_PinjKend_2
    Inherits System.Web.UI.Page

    Private dbconn As New DBKewConn
    Public KodStatus As Int16 = 0
    Public KodLulus As Int16 = 0


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            tdNamaPendaftar.Visible = False
            tdKategori.Visible = False
            lbtnNext.Visible = True
            NiagaUtama.Visible = True

            If Not Session("LoggedIn") Then
                Response.Redirect("~/Account/Logout.aspx")
                Exit Sub
            End If

            If Not Session("SmkbMessage") = String.Empty Then
                lblWarning.Visible = True
                lblErr.Text = Session("SmkbMessage")
            End If

            hdNoIDSem.Value = Session("ssusrID")
            ViewState("vsDoneEdit") = False
            ViewState("kodsubmenu") = Request.QueryString("KodSubMenu")
            'fBindDdlNegara()
            'fBindDdlNegeri()
            'fBindDdlKodBank()
            'fLoadMakSya()
            'fLoadGVPenyata()
            'fLoadGVProfilSyarikat()
            'fLoadPenyata()

            'If Session("GrantAccess") = 0 Or Session("GrantAccess") = 6 Then
            '    ' Dim dbconnVen As New DBConn(DBVen)
            '    Dim strDaftar = $"SELECT [NoDaftar],[NamaSyarikat],[Email] FROM [ROC_Login] WHERE [UserID] = '{hdNoIDSem.Value}'"
            '    Using dt = dbconnVen.fselectCommandDt(strDaftar)
            '        txtNamaSya.Text = dt.Rows(0)("NamaSyarikat")
            '        txtNoSya.Text = dt.Rows(0)("NoDaftar")
            '        lblEmailSya.Text = dt.Rows(0)("Email")

            '        'New registration
            '        Session("GrantAccess") = 6
            '    End Using
            'Else


            If Session("GrantAccess") = 2 Then
                fGlobalAlert("Syarikat tuan telah disenarai hitam oleh pihak UTeM. Sila hubungi pihak UTeM.", Page, Me.GetType, "~/../../../../Default.aspx")
                Exit Sub
            End If

            If Session("GrantAccess") = 3 Then
                fGlobalAlert("Syarikat tuan telah digantung oleh pihak UTeM. Sila hubungi pihak UTeM.", Page, Me.GetType, "~/../../../../Default.aspx")
                Exit Sub
            End If

            If Session("GrantAccess") = 4 Then
                fGlobalAlert("Syarikat tuan sedang dalam proses kelulusan pendaftaran oleh pihak UTeM. Sila hubungi pihak UTeM.", Page, Me.GetType, "~/../../../../Default.aspx")
                Exit Sub
            End If

            If Session("GrantAccess") = 5 Then
                fGlobalAlert("Syarikat tuan sedang dalam proses kelulusan kemaskini pendaftaran oleh pihak UTeM. Sila hubungi pihak UTeM.", Page, Me.GetType, "~/../../../../Default.aspx")
                Exit Sub
            End If

            ViewState("kodsubmenu") = Request.QueryString("KodSubMenu")
            ' fLoadDetail()

            'If Session("GrantAccess") = 1 Then
            '    If rbKategoriSyarikat.SelectedItem Is Nothing Then
            '        lbtnNext.Visible = False
            '    End If
            'End If
        End If
        'lblMakSya.Text = Session("ssusrNoSya") + " /  " + Session("ssusrNamaSya")
        KodStatus = 1
        'End If
    End Sub

    Private Sub fLoadDetail()

        Dim strSql As String = $"SELECT [ROC01_IDSya],[ROC01_NoSya],[ROC01_NamaSya],ROC01_KategoriSya,
[ROC01_AlmtP1], [ROC01_AlmtP2], [ROC01_BandarP], [ROC01_PoskodP], [ROC01_NegeriP], [ROC01_NegaraP], [ROC01_Tel1P],
[ROC01_Tel2P], [ROC01_FaksP], [ROC01_WebP], ROC01_EmelP, ROC01_WakilSya,[ROC01_KodBank],[ROC01_NoAkaun],ROC01_Bekalan,ROC01_Perkhidmatan,ROC01_Kerja,ROC01_KatSya
FROM [ROC01_Syarikat] 
WHERE [ROC01_IDSem] = '{hdNoIDSem.Value}'"
        Using dt = dbconn.fSelectCommandDt(strSql)

            If dt.Rows.Count > 0 Then
                txtNoSya.Text = dt.Rows(0)("ROC01_NoSya").ToString
                txtNamaSya.Text = dt.Rows(0)("ROC01_NamaSya").ToString
                txtNamaPendaftar.Text = dt.Rows(0)("ROC01_WakilSya").ToString
                txtAlamat1.Text = dt.Rows(0)("ROC01_AlmtP1").ToString
                txtAlamat2.Text = dt.Rows(0)("ROC01_AlmtP2").ToString
                txtPoskod.Text = dt.Rows(0)("ROC01_PoskodP").ToString
                txtBandar.Text = dt.Rows(0)("ROC01_BandarP").ToString
                txtTelp1.Text = dt.Rows(0)("ROC01_Tel1P").ToString
                txtTelp2.Text = dt.Rows(0)("ROC01_Tel2P").ToString
                txtWeb.Text = dt.Rows(0)("ROC01_WebP").ToString
                lblEmailSya.Text = dt.Rows(0)("ROC01_EmelP").ToString
                txtFax.Text = dt.Rows(0)("ROC01_FaksP").ToString

                ddlNegara.SelectedValue = dt.Rows(0)("ROC01_NegaraP").ToString
                ddlNegeri.SelectedValue = dt.Rows(0)("ROC01_NegeriP").ToString
                ddlKodBank.SelectedValue = dt.Rows(0)("ROC01_KodBank").ToString
                txtNoAkaun.Text = dt.Rows(0)("ROC01_NoAkaun").ToString
                Dim kat = dt.Rows(0)("ROC01_KategoriSya").ToString
                rbKategoriSyarikat.SelectedValue = kat
                Dim Bekalan As Boolean = IIf(IsDBNull(dt.Rows(0)("ROC01_Bekalan")), False, dt.Rows(0)("ROC01_Bekalan"))
                Dim Perkhidmatan As Boolean = IIf(IsDBNull(dt.Rows(0)("ROC01_Perkhidmatan")), False, dt.Rows(0)("ROC01_Perkhidmatan"))
                Dim Kerja As Boolean = IIf(IsDBNull(dt.Rows(0)("ROC01_Kerja")), False, dt.Rows(0)("ROC01_Kerja"))
                If dt.Rows(0)("ROC01_KatSya") = "E" Then
                    chxKatSyarikat.Items(0).Selected = True
                ElseIf dt.Rows(0)("ROC01_KatSya") = "S" Then
                    chxKatSyarikat.Items(1).Selected = True
                End If
                'If kat = "1" Then
                '    lbtnNext.Visible = True
                'Else
                '    lbtnNext.Visible = False
                'End If
                If Bekalan Then
                    chxNiagaUtama.Items(0).Selected = True
                    'divMOF.Visible = True
                End If

                If Perkhidmatan Then
                    chxNiagaUtama.Items(1).Selected = True
                    'divMOF.Visible = True
                End If
                If Kerja Then
                    chxNiagaUtama.Items(2).Selected = True
                    'trGredKerja.Visible = True
                    'rfvddlGredKerja.Enabled = True
                    'divCIDB.Visible = True
                End If
            Else
                txtNoSya.Text = Session("ssusrNoSya")
                txtNamaSya.Text = Session("ssusrNamaSya")
                txtNamaPendaftar.Text = ""
                txtAlamat1.Text = ""
                txtAlamat2.Text = ""
                txtPoskod.Text = ""
                txtBandar.Text = ""
                txtTelp1.Text = ""
                txtTelp2.Text = ""
                txtWeb.Text = ""
                lblEmailSya.Text = Session("ssusrEmail")
                rbKategoriSyarikat.ClearSelection()
            End If
        End Using
    End Sub
    Private Sub fLoadMakSya()

        Dim strSql As String = $"SELECT [ROC01_IDSya],[ROC01_NoSya],[ROC01_NamaSya],ROC01_KategoriSya,
                                [ROC01_AlmtP1], [ROC01_AlmtP2], [ROC01_BandarP], [ROC01_PoskodP], [ROC01_NegeriP], [ROC01_NegaraP], [ROC01_Tel1P],
                                [ROC01_Tel2P], [ROC01_FaksP], [ROC01_WebP], ROC01_EmelP, ROC01_WakilSya,[ROC01_KodBank],[ROC01_NoAkaun],[ROC01_PIC],
                                [ROC01_PIC1],[ROC01_PIC_Num],[ROC01_PIC_NumPej],[ROC01_PIC_Emel],[ROC01_PIC_Jwtn],[ROC01_PIC1_Num],[ROC01_PIC1_NumPej]
                                ,[ROC01_PIC1_Emel],[ROC01_PIC1_Jwtn],ROC01_KodLulus
                                FROM [ROC01_Syarikat] 
                                WHERE [ROC01_IDSem] = '{hdNoIDSem.Value}'"
        Using dt = dbconn.fSelectCommandDt(strSql)

            If dt.Rows.Count > 0 Then
                txtNoSya.Text = dt.Rows(0)("ROC01_NoSya").ToString
                txtNamaSya.Text = dt.Rows(0)("ROC01_NamaSya").ToString
                'txtNamaPendaftar.Text = dt.Rows(0)("ROC01_WakilSya").ToString
                txtAlamat1.Text = dt.Rows(0)("ROC01_AlmtP1").ToString
                txtAlamat2.Text = dt.Rows(0)("ROC01_AlmtP2").ToString
                txtPoskod.Text = dt.Rows(0)("ROC01_PoskodP").ToString
                txtBandar.Text = dt.Rows(0)("ROC01_BandarP").ToString
                txtTelp1.Text = dt.Rows(0)("ROC01_Tel1P").ToString
                txtTelp2.Text = dt.Rows(0)("ROC01_Tel2P").ToString
                txtWeb.Text = dt.Rows(0)("ROC01_WebP").ToString
                lblEmailSya.Text = dt.Rows(0)("ROC01_EmelP").ToString
                txtFax.Text = dt.Rows(0)("ROC01_FaksP").ToString

                ddlNegara.SelectedValue = dt.Rows(0)("ROC01_NegaraP").ToString
                ddlNegeri.SelectedValue = dt.Rows(0)("ROC01_NegeriP").ToString
                ddlKodBank.SelectedValue = dt.Rows(0)("ROC01_KodBank").ToString
                txtNoAkaun.Text = dt.Rows(0)("ROC01_NoAkaun").ToString
                txtPICNama.Text = dt.Rows(0)("ROC01_PIC").ToString
                txtPICNum.Text = dt.Rows(0)("ROC01_PIC_Num").ToString
                txtPICNumPej.Text = dt.Rows(0)("ROC01_PIC_NumPej").ToString
                txtPICEmel.Text = dt.Rows(0)("ROC01_PIC_Emel").ToString
                txtPICJwtn.Text = dt.Rows(0)("ROC01_PIC_Jwtn").ToString
                txtPIC1_Nama.Text = dt.Rows(0)("ROC01_PIC1").ToString
                txtPIC1_Num.Text = dt.Rows(0)("ROC01_PIC1_Num").ToString
                txtPIC1_NumPej.Text = dt.Rows(0)("ROC01_PIC1_NumPej").ToString
                txtPIC1_Emel.Text = dt.Rows(0)("ROC01_PIC1_Emel").ToString
                txtPIC1_Jwtn.Text = dt.Rows(0)("ROC01_PIC1_Jwtn").ToString
                Dim kat = dt.Rows(0)("ROC01_KategoriSya").ToString
                'rbKategoriSyarikat.SelectedValue = kat
                'If kat = "1" Then
                '    lbtnNext.Visible = True
                'Else
                '    lbtnNext.Visible = False
                'End If
                If dt.Rows(0)("ROC01_KodLulus").ToString = "2" Or dt.Rows(0)("ROC01_KodLulus").ToString = "5" Then
                    trUlasan.Visible = "True"
                    fLoadUlasan()
                Else
                    trUlasan.Visible = "False"
                End If

            Else
                txtNoSya.Text = Session("ssusrNoSya")
                txtNamaSya.Text = Session("ssusrNamaSya")
                'txtNamaPendaftar.Text = ""
                txtAlamat1.Text = ""
                txtAlamat2.Text = ""
                txtPoskod.Text = ""
                txtBandar.Text = ""
                txtTelp1.Text = ""
                txtTelp2.Text = ""
                txtWeb.Text = ""
                'lblEmailSya.Text = Session("ssusrEmail")
                rbKategoriSyarikat.ClearSelection()
            End If
        End Using
    End Sub

    Private Sub fLoadUlasan()

        Dim strSql As String = $"SELECT TOP (1) [ROC10_NoID1],[ROC10_NoID2],[KodDok],[ROC10_NoPerson],[ROC10_Tkh],[ROC10_Ulasan]
                                  FROM [DbKewanganV1].[dbo].[ROC10_StatusDok]
                                  WHERE [ROC10_NoID1] = '{hdNoIDSem.Value}'
                                  ORDER BY ROC10_Tkh DESC"
        Using dt = dbconn.fSelectCommandDt(strSql)

            If dt.Rows.Count > 0 Then
                ltrUlasan.Text = dt.Rows(0)("ROC10_Ulasan").ToString
            End If
        End Using
    End Sub

    Private Sub fLoadPenyata()
        Dim str2 = $"SELECT ROC01_IDBank,ROC01_IDSem,ROC01_IDSya,ROC01_KodBank,ROC01_NoAkaun,ROC01_StatusDelete 
FROM ROC01_BankSya
WHERE ROC01_IDSem='{hdNoIDSem.Value}' AND ROC01_StatusDelete <> 0 ;"

        Using ds = dbconn.fselectCommandDs(str2)
            Using dt2 = ds.Tables(0)
                ViewState("vsDt") = dt2
                BindGridViewButiran()
            End Using
        End Using
    End Sub

    Protected Sub lbtnUpPenyata_Click(sender As Object, e As EventArgs) Handles lbtnUpPenyata.Click
        If fuPenyata.HasFile Then
            If fuPenyata.PostedFile.ContentType = "application/pdf" Or fuPenyata.PostedFile.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document" Then
                If fuPenyata.PostedFile.ContentLength < 51200000 Then  '500KB
                    Dim FileName = Path.GetFileName(fuPenyata.FileName)
                    If FileName.Length < 50 Then
                        Dim folderPath As String = Server.MapPath($"~/Upload/Vendor/MH Penyata Akaun/{hdNoIDSem.Value}/")
                        Dim ContentType = fuPenyata.PostedFile.ContentType

                        'Check whether Directory (Folder) exists.
                        If Not Directory.Exists(folderPath) Then
                            'If Directory (Folder) does not exists. Create it.
                            Directory.CreateDirectory(folderPath)
                        End If

                        Try
                            'Dim IdSem = hdNoIDSemSya.Value + "_"
                            Dim SaveFileName = FileName 'String.Concat(IdSem, FileName)

                            'Create the path And file name to check for duplicates.
                            Dim pathToCheck = folderPath + SaveFileName

                            'Check to see if a file already exists with the same name as the file to upload.
                            If File.Exists(pathToCheck) Then
                                LabelMessage1.ForeColor = Color.Red
                                LabelMessage1.Text = "The target file already exists, please rename it."
                                fGlobalAlert("Maaf! Muat naik dokumen tidak berjaya. Nama fail telah wujud. Sila pastikan nama fail yang hendak dimasukkan adalah berbeza.", Me.Page, Me.GetType())
                                Exit Sub
                            Else
                                'Save the File to the Directory (Folder).
                                fuPenyata.SaveAs(folderPath & SaveFileName)

                                LabelMessage1.ForeColor = Color.Green
                                'Display the success message.
                                LabelMessage1.Text = FileName + " has been uploaded."

                                'simpanPath(folderPath, SaveFileName, ContentType, "PAS")
                                'fLoadGVPenyata()
                            End If

                        Catch ex As Exception
                            'Display the success message.
                            LabelMessage1.ForeColor = Color.Red
                            LabelMessage1.Text = FileName + " could NOT be uploaded."
                        End Try
                    Else
                        LabelMessage1.ForeColor = Color.Red
                        LabelMessage1.Text = "The filename length cannot exceed 50 characters, please rename it."
                    End If

                Else
                    LabelMessage1.ForeColor = Color.Red
                    LabelMessage1.Text = "Please upload file With size Not more than 500KB"
                End If
            Else
                LabelMessage1.ForeColor = Color.Red
                LabelMessage1.Text = "Please upload file With type Of 'docx' or 'pdf'."
            End If
        Else
            LabelMessage1.ForeColor = Color.Red
            LabelMessage1.Text = "Upload failed! : No file selected."
        End If
        KodStatus = 1
    End Sub

    Protected Sub lbtnProfilSya_Click(sender As Object, e As EventArgs) Handles lbtnProfilSya.Click
        If fuProfilSya.HasFile Then
            If checkFile(hdNoIDSem.Value) > 0 Then
                fGlobalAlert("Maaf! Muat naik dokumen tidak berjaya. Sila padam fail Profil Syarikat yang sedia ada sebelum memuat naik fail yang baru.", Me.Page, Me.GetType())
                Exit Sub
            Else
                If fuProfilSya.PostedFile.ContentType = "application/pdf" Or fuProfilSya.PostedFile.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document" Then
                    If fuProfilSya.PostedFile.ContentLength < 51200000 Then  '500KB
                        Dim FileName = Path.GetFileName(fuProfilSya.FileName)
                        If FileName.Length < 50 Then
                            Dim folderPath As String = Server.MapPath($"~/Upload/Vendor/ProfilSyarikat/{hdNoIDSem.Value}/")
                            Dim ContentType = fuProfilSya.PostedFile.ContentType

                            'Check whether Directory (Folder) exists.
                            If Not Directory.Exists(folderPath) Then
                                'If Directory (Folder) does not exists. Create it.
                                Directory.CreateDirectory(folderPath)
                            End If

                            Try
                                'Dim rn = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 9)
                                'Dim IdSem = hdNoIDSemSya.Value + "_"
                                Dim SaveFileName = FileName 'String.Concat(IdSem, FileName)
                                'Dim SaveFileName = rn + FileName 'String.Concat(IdSem, FileName)

                                'Create the path And file name to check for duplicates.
                                Dim pathToCheck = folderPath + SaveFileName

                                'Check to see if a file already exists with the same name as the file to upload.
                                If File.Exists(pathToCheck) Then
                                    lblmsgProfilSya.ForeColor = Color.Red
                                    lblmsgProfilSya.Text = "The target file already exists, please rename it."
                                Else
                                    'Save the File to the Directory (Folder).
                                    fuProfilSya.SaveAs(folderPath & SaveFileName)

                                    lblmsgProfilSya.ForeColor = Color.Green
                                    'Display the success message.
                                    lblmsgProfilSya.Text = FileName + " has been uploaded."

                                    simpanPath(folderPath, SaveFileName, ContentType, "Prof")
                                    fLoadGVProfilSyarikat()
                                End If

                            Catch ex As Exception
                                'Display the success message.
                                lblmsgProfilSya.ForeColor = Color.Red
                                lblmsgProfilSya.Text = FileName + " could NOT be uploaded."
                            End Try
                        Else
                            lblmsgProfilSya.ForeColor = Color.Red
                            lblmsgProfilSya.Text = "The filename length cannot exceed 50 characters, please rename it."
                        End If

                    Else
                        lblmsgProfilSya.ForeColor = Color.Red
                        lblmsgProfilSya.Text = "Please upload file With size Not more than 500KB"
                    End If
                Else
                    lblmsgProfilSya.ForeColor = Color.Red
                    lblmsgProfilSya.Text = "Please upload file With type Of 'docx' or 'pdf'."
                End If
            End If
        Else
            lblmsgProfilSya.ForeColor = Color.Red
            lblmsgProfilSya.Text = "Upload failed! : No file selected."
        End If
        KodStatus = 1
    End Sub


    Protected Sub gvPenyata_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvPenyata.RowDeleting
        Try
            Dim index As Integer = e.RowIndex
            Dim id = gvPenyata.DataKeys(index).Value
            Dim filename = gvPenyata.Rows(index).Cells(1).Text

            If String.IsNullOrEmpty(filename) Then
                fGlobalAlert("Tiada Dokumen untuk dihapuskan", Me.Page, Me.GetType())
                Exit Sub
            End If

            Dim folderPath As String = Server.MapPath($"~/Upload/Vendor/MH Penyata Akaun/{hdNoIDSem.Value}/")
            Dim tarikhDelete As String = DateTime.Now.ToString("ddMMyyyyHHmmss")

            Dim filepathfile = folderPath + filename
            Dim renameFile = filename + "_" + tarikhDelete + "_forDelete"
            'Dim filepath = Request.Url.Authority + Request.ApplicationPath + filepathfile
            Dim destfilepathfile = folderPath + renameFile
            Dim fileInfo As New FileInfo(filepathfile)
            If fileInfo.Exists Then
                fileInfo.MoveTo(destfilepathfile)

                Dim strSql = $"UPDATE [ROC09_Lampiran] SET [ROC09_StatusDelete]=@StatusDlt, [ROC09_NamaDok]=@NamaDok WHERE ROC09_ID=@Id;"
                Dim strSql1 = $"UPDATE ROC01_Syarikat SET ROC01_FlagKlikSimpan = @FlagKlikSimpann WHERE ROC01_IDSem=@IdSEM;"
                Dim paramSql = {
                    New SqlParameter("@FlagKlikSimpann", True),
                New SqlParameter("@IdSEM", hdNoIDSem.Value),
                            New SqlParameter("@Id", id),
                            New SqlParameter("@StatusDlt", 1),
                            New SqlParameter("@NamaDok", renameFile)
                            }

                If dbconn.fUpdateCommand(strSql + strSql1, paramSql) > 0 Then
                    LabelMessage1.ForeColor = Color.Green
                    LabelMessage1.Text = "Dokumen telah dipadam!."
                    fLoadGVPenyata()
                Else
                    LabelMessage1.ForeColor = Color.Red
                    LabelMessage1.Text = "Dokumen tidak berjaya dipadam!."
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub gvProfilSya_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvProfilSya.RowDeleting
        Try
            Dim index As Integer = e.RowIndex
            Dim id = gvProfilSya.DataKeys(index).Value
            Dim filename = gvProfilSya.Rows(index).Cells(1).Text
            If String.IsNullOrEmpty(filename) Then
                fGlobalAlert("Tiada Dokumen untuk dihapuskan", Me.Page, Me.GetType())
                Exit Sub
            End If

            Dim folderPath As String = Server.MapPath($"~/Upload/Vendor/ProfilSyarikat/{hdNoIDSem.Value}/")
            Dim tarikhDelete As String = DateTime.Now.ToString("ddMMyyyyHHmmss")

            Dim filepathfile = folderPath + filename.Replace("&amp;", "&")
            Dim renameFile = filename + "_" + tarikhDelete + "_forDelete"
            'Dim filepath = Request.Url.Authority + Request.ApplicationPath + filepathfile
            Dim destfilepathfile = folderPath + renameFile
            Dim fileInfo As New FileInfo(filepathfile)
            If fileInfo.Exists Then
                fileInfo.MoveTo(destfilepathfile)

                Dim strSql = $"UPDATE [ROC09_Lampiran] SET [ROC09_StatusDelete]=@StatusDlt, [ROC09_NamaDok]=@NamaDok WHERE ROC09_ID=@Id;"
                Dim strSql1 = $"UPDATE ROC01_Syarikat SET ROC01_FlagKlikSimpan = @FlagKlikSimpann WHERE ROC01_IDSem=@IdSEM;"
                Dim paramSql = {
                    New SqlParameter("@FlagKlikSimpann", True),
                New SqlParameter("@IdSEM", hdNoIDSem.Value),
                            New SqlParameter("@Id", id),
                            New SqlParameter("@StatusDlt", 1),
                            New SqlParameter("@NamaDok", renameFile)
                            }

                If dbconn.fUpdateCommand(strSql + strSql1, paramSql) > 0 Then
                    lblmsgProfilSya.ForeColor = Color.Green
                    lblmsgProfilSya.Text = "Dokumen telah dipadam!."
                    fLoadGVProfilSyarikat()
                Else
                    lblmsgProfilSya.ForeColor = Color.Red
                    lblmsgProfilSya.Text = "Dokumen tidak berjaya dipadam!."
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub simpanPath(Path As String, FileName As String, ContentType As String, JenisLamp As String)

        Try
            Dim IdSem = hdNoIDSem.Value
            Dim rUpdate, rCommit As Boolean

            Dim str = "select [ROC09_ID], [ROC01_IDSem], [ROC09_NamaDok], [ROC09_Bil], [ROC09_Path], 
[ROC09_ContentType], [ROC09_JenisDok], [ROC09_StatusDelete] from [ROC09_Lampiran]"
            Using dt = dbconn.fSelectCommandDt(str, True)
                Dim dsRows = dt.AsEnumerable().Where(Function(r) r.Item("ROC01_IDSem") = IdSem)
                Dim record = dsRows.Count

                Dim Bil = record + 1

                Dim dr As DataRow
                dr = dt.NewRow
                dr("ROC01_IDSem") = IdSem
                dr("ROC09_NamaDok") = FileName
                dr("ROC09_Bil") = Bil
                dr("ROC09_Path") = Path
                dr("ROC09_ContentType") = ContentType
                dr("ROC09_JenisDok") = JenisLamp
                dr("ROC09_StatusDelete") = 0
                dt.Rows.Add(dr)
                dbconn.sUpdateCommandDt(dt, rUpdate, True, rCommit)
            End Using

            If rCommit Then
                Session("GrantAccess") = 8
            End If
        Catch ex As Exception
        End Try

    End Sub


    Private Function checkFile(ByVal Kod As String) As String
        Dim Bilfile As String = ""
        Dim strSql = $"select [ROC09_ID], [ROC01_IDSem], [ROC09_NamaDok], [ROC09_Bil], [ROC09_Path], 
[ROC09_ContentType], [ROC09_JenisDok], [ROC09_StatusDelete] from [ROC09_Lampiran] WHERE ROC01_IDSem='{hdNoIDSem.Value}' AND ROC09_JenisDok='Prof' AND ROC09_StatusDelete = 0"
        'Dim dbconn As New DBKewConn
        Dim ds = dbconn.fselectCommandDs(strSql)

        If ds.Tables(0).Rows.Count > 0 Then
            Bilfile = ds.Tables(0).Rows.Count
        Else
            Bilfile = 0
        End If

        Return Bilfile
    End Function

    Private Sub fLoadGVPenyata()
        Dim str = $"SELECT [ROC01_IDSem], [ROC09_ID], [ROC09_NamaDok]
FROM  ROC09_Lampiran
WHERE ROC01_IDSem='{hdNoIDSem.Value}' AND ROC09_JenisDok='PAS' AND ROC09_StatusDelete = 0"

        Using dt = dbconn.fSelectCommandDt(str)

            If dt.Rows.Count > 0 Then
                gvPenyata.DataSource = dt
                gvPenyata.DataBind()
            Else
                gvPenyata.DataSource = New List(Of ListItem)
                gvPenyata.DataBind()
            End If
        End Using
    End Sub

    Private Sub fLoadGVProfilSyarikat()
        Dim str = $"SELECT [ROC01_IDSem], [ROC09_ID], [ROC09_NamaDok]
FROM  ROC09_Lampiran
WHERE ROC01_IDSem='{hdNoIDSem.Value}' AND ROC09_JenisDok='Prof' AND ROC09_StatusDelete = 0"

        Using dt = dbconn.fSelectCommandDt(str)

            If dt.Rows.Count > 0 Then
                gvProfilSya.DataSource = dt
                gvProfilSya.DataBind()
            Else
                gvProfilSya.DataSource = New List(Of ListItem)
                gvProfilSya.DataBind()
            End If
        End Using
    End Sub

    Private Sub fBindDdlNegeri()

        Dim strSql As String = $"SELECT KodNegeri, Butiran FROM MK_Negeri ORDER BY Butiran"

        Using dt = dbconn.fSelectCommandDt(strSql)
            ddlNegeri.DataSource = dt
            ddlNegeri.DataTextField = "Butiran"
            ddlNegeri.DataValueField = "KodNegeri"
            ddlNegeri.DataBind()

            ddlNegeri.Items.Insert(0, New ListItem("-Sila Pilih-", ""))
            ddlNegeri.SelectedIndex = 0
        End Using

    End Sub

    Private Sub fBindDdlNegara()

        Dim strSql As String = $"SELECT KodNegara, Butiran FROM MK_Negara ORDER BY Butiran"

        Using dt = dbconn.fSelectCommandDt(strSql)

            ddlNegara.DataSource = dt
            ddlNegara.DataTextField = "Butiran"
            ddlNegara.DataValueField = "KodNegara"
            ddlNegara.DataBind()
            '121 - Malaysia
            ddlNegara.SelectedValue = "121"
        End Using
    End Sub

    Private Sub fBindDdlKodBank()

        Dim strSql As String = $"SELECT Kod, (Kod + ' - ' + Nama) AS Butiran FROM MK_BankCreditOnline ORDER BY Nama"

        Using ds = dbconn.fSelectCommandDt(strSql)

            ddlKodBank.DataSource = ds
            ddlKodBank.DataTextField = "Butiran"
            ddlKodBank.DataValueField = "Kod"
            ddlKodBank.DataBind()

            ddlKodBank.Items.Insert(0, New ListItem("-Sila Pilih-", ""))
            ddlKodBank.SelectedIndex = 0
        End Using
    End Sub

    Protected Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.Click
        'If rbKategoriSyarikat.SelectedItem Is Nothing Then
        '    fGlobalAlert("Sila Pilih kategori vendor", Page, Me.GetType)
        '    Exit Sub
        'End If
        If gvProfilSya.Rows.Count = 0 Then
            fGlobalAlert("Sila Muat Naik Profil Syarikat.", Page, Me.GetType)
            Exit Sub
        End If
        If gvPenyata.Rows.Count = 0 Then
            fGlobalAlert("Sila Muat Naik Muka Hadapan Penyata Akaun Syarikat.", Page, Me.GetType)
            Exit Sub
        End If
        Dim bCommit = False, bUpdate = False
        Dim strSql As String = $"SELECT ROC01_IDSem,[ROC01_IDSya],[ROC01_NoSya],[ROC01_NamaSya],ROC01_KategoriSya, 
                                [ROC01_AlmtP1], [ROC01_AlmtP2], [ROC01_BandarP], [ROC01_PoskodP], [ROC01_NegeriP], [ROC01_NegaraP], [ROC01_Tel1P],
                                [ROC01_Tel2P], [ROC01_FaksP], [ROC01_WebP], ROC01_EmelP, ROC01_WakilSya,[ROC01_KodBank],[ROC01_NoAkaun],
                                ROC01_TkhDaftar, ROC01_TkhKemaskini, ROC01_FlagKlikSimpan, ROC01_KodAktif,[ROC01_PIC],[ROC01_PIC1],[ROC01_PIC_Num],[ROC01_PIC_NumPej],[ROC01_PIC_Emel],[ROC01_PIC_Jwtn]
                                ,[ROC01_PIC1_Num],[ROC01_PIC1_NumPej],[ROC01_PIC1_Emel],[ROC01_PIC1_Jwtn],ROC01_Bekalan,ROC01_Perkhidmatan,ROC01_Kerja,ROC01_Kategori,ROC01_KatSya
                                FROM [ROC01_Syarikat] 
                                WHERE [ROC01_IDSem] = '{hdNoIDSem.Value}'"

        Dim paramSql() As SqlParameter = {
                New SqlParameter("@idSem", hdNoIDSem.Value)
            }

        Using dt = dbconn.fSelectCommandDt(strSql, True, paramSql)
            If dt.Rows.Count > 0 Then
                'Kemaskini
                dt.Rows(0)("ROC01_AlmtP1") = txtAlamat1.Text
                dt.Rows(0)("ROC01_AlmtP2") = txtAlamat2.Text
                dt.Rows(0)("ROC01_BandarP") = txtBandar.Text
                dt.Rows(0)("ROC01_PoskodP") = txtPoskod.Text
                dt.Rows(0)("ROC01_NegeriP") = ddlNegeri.SelectedValue
                dt.Rows(0)("ROC01_NegaraP") = ddlNegara.SelectedValue
                dt.Rows(0)("ROC01_Tel1P") = txtTelp1.Text
                dt.Rows(0)("ROC01_Tel2P") = txtTelp2.Text
                dt.Rows(0)("ROC01_FaksP") = txtFax.Text
                dt.Rows(0)("ROC01_WebP") = txtWeb.Text
                dt.Rows(0)("ROC01_WakilSya") = txtNamaPendaftar.Text
                dt.Rows(0)("ROC01_KodBank") = ddlKodBank.SelectedValue
                dt.Rows(0)("ROC01_NoAkaun") = txtNoAkaun.Text
                If chxKatSyarikat.Items(0).Selected Then
                    dt.Rows(0)("ROC01_KatSya") = "E"
                Else
                    dt.Rows(0)("ROC01_KatSya") = "S"
                End If
                'dt.Rows(0)("ROC01_KategoriSya") = rbKategoriSyarikat.SelectedValue
                dt.Rows(0)("ROC01_TkhKemaskini") = Now
                If IsDBNull(dt.Rows(0)("ROC01_TkhDaftar")) Then
                    dt.Rows(0)("ROC01_TkhDaftar") = Now
                End If
                If rbKategoriSyarikat.SelectedValue.Equals("1") Then
                    'Pembekal
                    dt.Rows(0)("ROC01_FlagKlikSimpan") = True
                Else
                    'Bukan pembekal
                    dt.Rows(0)("ROC01_FlagKlikSimpan") = False
                    dt.Rows(0)("ROC01_KodAktif") = "01" 'Aktif
                End If
                dt.Rows(0)("ROC01_PIC") = txtPICNama.Text
                dt.Rows(0)("ROC01_PIC_Num") = txtPICNum.Text
                dt.Rows(0)("ROC01_PIC_NumPej") = txtPICNumPej.Text
                dt.Rows(0)("ROC01_PIC_Emel") = txtPICEmel.Text
                dt.Rows(0)("ROC01_PIC_Jwtn") = txtPICJwtn.Text
                dt.Rows(0)("ROC01_PIC1") = txtPIC1_Nama.Text
                dt.Rows(0)("ROC01_PIC1_Num") = txtPIC1_Num.Text
                dt.Rows(0)("ROC01_PIC1_NumPej") = txtPIC1_NumPej.Text
                dt.Rows(0)("ROC01_PIC1_Emel") = txtPIC1_Emel.Text
                dt.Rows(0)("ROC01_PIC1_Jwtn") = txtPIC1_Jwtn.Text
                If chxNiagaUtama.Items(0).Selected Then
                    dt.Rows(0)("ROC01_Bekalan") = True
                Else
                    dt.Rows(0)("ROC01_Bekalan") = False
                End If
                If chxNiagaUtama.Items(1).Selected Then
                    dt.Rows(0)("ROC01_Perkhidmatan") = True
                Else
                    dt.Rows(0)("ROC01_Perkhidmatan") = False
                End If
                If chxNiagaUtama.Items(2).Selected Then
                    dt.Rows(0)("ROC01_Kerja") = True
                Else
                    dt.Rows(0)("ROC01_Kerja") = False
                End If
                dt.Rows(0)("ROC01_FlagKlikSimpan") = True
            Else
                'Add new syarikat
                Dim dr As DataRow
                dr = dt.NewRow
                dr("ROC01_IDSem") = hdNoIDSem.Value
                dr("ROC01_NoSya") = txtNoSya.Text
                dr("ROC01_NamaSya") = txtNamaSya.Text
                dr("ROC01_AlmtP1") = txtAlamat1.Text
                dr("ROC01_AlmtP2") = txtAlamat2.Text
                dr("ROC01_BandarP") = txtBandar.Text
                dr("ROC01_PoskodP") = txtPoskod.Text
                dr("ROC01_NegeriP") = ddlNegeri.SelectedValue
                dr("ROC01_NegaraP") = ddlNegara.SelectedValue
                dr("ROC01_Tel1P") = txtTelp1.Text
                dr("ROC01_Tel2P") = txtTelp2.Text
                dr("ROC01_FaksP") = txtFax.Text
                dr("ROC01_WebP") = txtWeb.Text
                'dr("ROC01_WakilSya") = txtNamaPendaftar.Text
                dr("ROC01_EmelP") = lblEmailSya.Text
                dr("ROC01_KodBank") = ddlKodBank.SelectedValue
                dr("ROC01_NoAkaun") = txtNoAkaun.Text
                'dr("ROC01_KategoriSya") = rbKategoriSyarikat.SelectedValue
                dr("ROC01_TkhDaftar") = Now
                dr("ROC01_TkhKemaskini") = Now
                If rbKategoriSyarikat.SelectedValue.Equals("1") Then
                    'Pembekal
                    dr("ROC01_FlagKlikSimpan") = True
                Else
                    'Bukan pembekal
                    dr("ROC01_FlagKlikSimpan") = False
                    dr("ROC01_KodAktif") = "01" 'Aktif

                End If
                dr("ROC01_PIC") = txtPICNama.Text
                dr("ROC01_PIC_Num") = txtPICNum.Text
                dr("ROC01_PIC_NumPej") = txtPICNumPej.Text
                dr("ROC01_PIC_Emel") = txtPICEmel.Text
                dr("ROC01_PIC_Jwtn") = txtPICJwtn.Text
                dr("ROC01_PIC1") = txtPIC1_Nama.Text
                dr("ROC01_PIC1_Num") = txtPIC1_Num.Text
                dr("ROC01_PIC1_NumPej") = txtPIC1_NumPej.Text
                dr("ROC01_PIC1_Emel") = txtPIC1_Emel.Text
                dr("ROC01_PIC1_Jwtn") = txtPIC1_Jwtn.Text
                dr("ROC01_Kategori") = "S"
                If chxNiagaUtama.Items(0).Selected Then
                    dr("ROC01_Bekalan") = True

                End If
                If chxNiagaUtama.Items(1).Selected Then
                    dr("ROC01_Perkhidmatan") = True

                End If
                If chxNiagaUtama.Items(2).Selected Then
                    dr("ROC01_Kerja") = True

                End If
                dr("ROC01_FlagKlikSimpan") = True
                If chxKatSyarikat.Items(0).Selected Then
                    dt.Rows(0)("ROC01_KatSya") = "E"
                Else
                    dt.Rows(0)("ROC01_KatSya") = "S"
                End If
                dt.Rows.Add(dr)
            End If

            dbconn.sUpdateCommandDt(dt, bUpdate, False, bCommit)

            If bUpdate Then
                Dim strSql2 = $"INSERT INTO ROC10_StatusDok VALUES (@IdSEM,@NoSya,@KodLulus,@noId,@tarikh,@ulasan);"
                paramSql = {
                            New SqlParameter("@NoSya", txtNoSya.Text),
                            New SqlParameter("@IdSEM", hdNoIDSem.Value),
                            New SqlParameter("@KodLulus", 11),
                            New SqlParameter("@noId", hdNoIDSem.Value),
                            New SqlParameter("@tarikh", Now),
                            New SqlParameter("@ulasan", "Perubahan Pada Maklumat Utama")
                            }
                dbconn.sUpdateCommandNonQuery(strSql2, paramSql, False, True, bUpdate, bCommit)
            End If


            If bCommit Then
                fGlobalAlert("Rekod berjaya disimpan.", Page, Me.GetType())

                'If rbKategoriSyarikat.SelectedValue.Equals("1") Then
                Session("GrantAccess") = 8
                'Dim crypto As New clsCrypto
                'Dim kategori = crypto.AESEncrypt(rbKategoriSyarikat.SelectedValue)
                'Response.Redirect($"~/Forms/Pendaftaran/Pendaftaran/Maklumat_Vendor.aspx?Ktg={kategori}&KodSubmenu={ViewState("kodsubmenu")}", False)
                'Response.Redirect($"~/Forms/Pendaftaran/Pendaftaran/Maklumat_Vendor.aspx?KodSubmenu={ViewState("kodsubmenu")}", False)
                'Else
                'Session("GrantAccess") = 7
                'Session("SmkbMessage") = ""
                'Response.Redirect("~/Default.aspx")
                'End If
                fGlobalAlert("Rekod telah berjaya disimpan. Sila klik butang seterusnya untuk memasukkan maklumat cawangan.", Page, Me.GetType())
            Else
                fGlobalAlert("Rekod tidak berjaya disimpan. Sila hubungi Admin!", Page, Me.GetType())
            End If
        End Using
    End Sub


    Protected Sub lbtnNext_Click(sender As Object, e As EventArgs) Handles lbtnNext.Click
        'Dim crypto As New clsCrypto
        'Dim kategori = crypto.AESEncrypt(rbKategoriSyarikat.SelectedValue)
        'Response.Redirect($"~/Forms/Pendaftaran/Pendaftaran/Maklumat_Vendor.aspx?Ktg={kategori}&KodSubmenu={ViewState("kodsubmenu")}")
        Response.Redirect($"~/Forms/Pendaftaran/Pendaftaran/Maklumat_Vendor.aspx?KodSubmenu={ViewState("kodsubmenu")}")
    End Sub

    Protected Sub btnStep2_Click(sender As Object, e As EventArgs) Handles btnStep2.ServerClick
        Response.Redirect($"~/Forms/Pendaftaran/Pendaftaran/Maklumat_Vendor.aspx?KodSubmenu={ViewState("kodsubmenu")}")
    End Sub

    Protected Sub rbKategoriSyarikat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rbKategoriSyarikat.SelectedIndexChanged
        If rbKategoriSyarikat.SelectedValue.Equals("1") Then
            'Pembekal
            lbtnNext.Visible = True
        Else
            'Bukan Pembekal
            lbtnNext.Visible = False
        End If
    End Sub

    Private Function fJanaNoROC()
        Dim strNoROC As String = ""
        Dim indexPO As Integer = 0
        Dim noakhir As Double
        Dim strSQL As String = $"SELECT TOP(1) NoAkhir From MK_NoAkhir WHERE KodModul = 'ROC' AND Prefix = 'RC'"
        dbconn.sSelectCommand(strSQL, noakhir)

        If noakhir <> Nothing Then
            indexPO = noakhir + 1
        Else
            indexPO = 1
        End If

        strNoROC = "RC" + Format(indexPO, "000000").ToString

        Return strNoROC
    End Function

    Private Sub UpdateNoAkhirROC()
        Dim strSql = "SELECT TOP(1) * From MK_NoAkhir WHERE KodModul = 'ROC' AND Prefix = 'RC'"
        Dim dt = dbconn.fSelectCommandDt(strSql, True)

        If dt.Rows.Count > 0 Then
            dt.Rows(0)("NoAkhir") = dt.Rows(0)("NoAkhir") + 1
        Else
            Dim dr As DataRow
            dr = dt.NewRow
            dr("KodModul") = "ROC"
            dr("Prefix") = "RC"
            dr("noakhir") = 1
            dr("Butiran") = "ROC RC"
            dr("kodPTJ") = ""
            dt.Rows.Add(dr)
        End If

        dbconn.sUpdateCommandDt(dt)
    End Sub

    Protected Sub lbtnAdd_Click(sender As Object, e As EventArgs)
        Dim control As Control = gvPenyataAkaun.FooterRow
        Dim ddlKodBank As DropDownList = CType(control.FindControl("ddlKodBank"), DropDownList)
        '    Dim ddlKo As DropDownList = CType(control.FindControl("ddlKo"), DropDownList)
        '    Dim ddlPTJ As DropDownList = CType(control.FindControl("ddlPTJ"), DropDownList)
        '    Dim ddlKp As DropDownList = CType(control.FindControl("ddlKp"), DropDownList)
        '    Dim ddlVot As DropDownList = CType(control.FindControl("ddlVot"), DropDownList)
        Dim txtNoAkaun As TextBox = CType(control.FindControl("txtNoAkaun"), TextBox)
        '    Dim txtDebit As TextBox = CType(control.FindControl("txtDebit"), TextBox)
        '    Dim txtKredit As TextBox = CType(control.FindControl("txtKredit"), TextBox)
        '    Dim amaunByr As Decimal = CDec(txtKredit.Text)
        Dim fuPenyataAkaun As FileUpload = CType(control.FindControl("fuPenyataAkaun"), FileUpload)

        Try
            Dim IdSem = hdNoIDSem.Value
            Dim rUpdate, rCommit As Boolean

            Dim str = "SELECT ROC01_IDBank,ROC01_IDSem,ROC01_IDSya,ROC01_KodBank,ROC01_NoAkaun,ROC01_StatusDelete 
                        FROM ROC01_BankSya
                        WHERE ROC01_StatusDelete <> 0"
            Using dt = dbconn.fSelectCommandDt(str, True)
                Dim dsRows = dt.AsEnumerable().Where(Function(r) r.Item("ROC01_IDSem") = IdSem)
                Dim record = dsRows.Count

                Dim Bil = record + 1
                Dim dr As DataRow
                dr = dsRows(0)
                'dr("ROC01_IDSya") = Session("ssusrNoSya")
                dr("ROC01_IDSem") = hdNoIDSem.Value
                dr("ROC01_KodBank") = ddlKodBank.SelectedValue
                dr("ROC01_NoAkaun") = txtNoAkaun.Text
                dr("ROC01_StatusDelete") = "0"
                'dt.Rows.Add(dr)
                dbconn.sUpdateCommandDt(dt, rUpdate, True, rCommit)
            End Using

            If rCommit Then
                Session("GrantAccess") = 8
            End If
            'upload
            If fuPenyataAkaun.HasFile Then
                If fuPenyataAkaun.PostedFile.ContentType = "application/pdf" Or fuPenyataAkaun.PostedFile.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document" Then
                    If fuPenyataAkaun.PostedFile.ContentLength < 51200000 Then  '500KB
                        Dim FileName = Path.GetFileName(fuPenyataAkaun.FileName)
                        If FileName.Length < 50 Then
                            Dim folderPath As String = Server.MapPath($"~/Upload/Vendor/MH Penyata Akaun/{hdNoIDSem.Value}/")
                            Dim ContentType = fuPenyataAkaun.PostedFile.ContentType

                            'Check whether Directory (Folder) exists.
                            If Not Directory.Exists(folderPath) Then
                                'If Directory (Folder) does not exists. Create it.
                                Directory.CreateDirectory(folderPath)
                            End If

                            Try
                                'Dim IdSem = hdNoIDSemSya.Value + "_"
                                Dim SaveFileName = FileName 'String.Concat(IdSem, FileName)

                                'Create the path And file name to check for duplicates.
                                Dim pathToCheck = folderPath + SaveFileName

                                'Check to see if a file already exists with the same name as the file to upload.
                                If File.Exists(pathToCheck) Then
                                    LabelMessage1.ForeColor = Color.Red
                                    LabelMessage1.Text = "The target file already exists, please rename it."
                                    fGlobalAlert("Maaf! Muat naik dokumen tidak berjaya. Nama fail telah wujud. Sila pastikan nama fail yang hendak dimasukkan adalah berbeza.", Me.Page, Me.GetType())
                                    Exit Sub
                                Else
                                    'Save the File to the Directory (Folder).
                                    fuPenyataAkaun.SaveAs(folderPath & SaveFileName)

                                    LabelMessage1.ForeColor = Color.Green
                                    'Display the success message.
                                    LabelMessage1.Text = FileName + " has been uploaded."

                                    'simpanPath(folderPath, SaveFileName, ContentType, "PAS")
                                    'fLoadGVPenyata()
                                End If

                            Catch ex As Exception
                                'Display the success message.
                                LabelMessage1.ForeColor = Color.Red
                                LabelMessage1.Text = FileName + " could NOT be uploaded."
                            End Try
                        Else
                            LabelMessage1.ForeColor = Color.Red
                            LabelMessage1.Text = "The filename length cannot exceed 50 characters, please rename it."
                        End If

                    Else
                        LabelMessage1.ForeColor = Color.Red
                        LabelMessage1.Text = "Please upload file With size Not more than 500KB"
                    End If
                Else
                    LabelMessage1.ForeColor = Color.Red
                    LabelMessage1.Text = "Please upload file With type Of 'docx' or 'pdf'."
                End If
            Else
                LabelMessage1.ForeColor = Color.Red
                LabelMessage1.Text = "Upload failed! : No file selected."
            End If

        Catch ex As Exception
        End Try

        'ViewState("vsDt") = dt
        fLoadPenyata()
        BindGridViewButiran()
    End Sub

    Protected Sub gvPenyataAkaun_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvPenyataAkaun.RowCancelingEdit
        gvPenyataAkaun.EditIndex = -1
        BindGridViewButiran()
    End Sub

    Protected Sub gvPenyataAkaun_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvPenyataAkaun.RowEditing
        gvPenyataAkaun.EditIndex = e.NewEditIndex
        BindGridViewButiran()
    End Sub

    Protected Sub gvPenyataAkaun_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvPenyataAkaun.RowUpdating
        Dim dt As DataTable = TryCast(ViewState("vsDt"), DataTable)
        If dt IsNot Nothing Then
            Dim row = gvPenyataAkaun.Rows(e.RowIndex)

            dt.Rows(row.DataItemIndex)("ROC01_KodBank") = (CType((row.Cells(1).Controls(1)), DropDownList)).SelectedValue
            dt.Rows(row.DataItemIndex)("ROC01_NoAkaun") = (CType((row.Cells(2).Controls(1)), TextBox)).Text

            'Dim kreditTxt = (CType((row.Cells(8).Controls(1)), TextBox)).Text
            'If String.IsNullOrEmpty(kreditTxt) Then
            '    dt.Rows(row.DataItemIndex)("RC01_Kredit") = 0.00
            'Else
            '    dt.Rows(row.DataItemIndex)("RC01_Kredit") = CDec(kreditTxt)
            'End If


        End If

        'Reset the edit index.
        gvPenyataAkaun.EditIndex = -1

        ViewState("vsDt") = dt
        BindGridViewButiran()
    End Sub

    Protected Sub gvPenyataAkaun_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvPenyataAkaun.RowDeleting
        Dim vsDt As DataTable = TryCast(ViewState("vsDt"), DataTable)
        vsDt.Rows.RemoveAt(e.RowIndex)

        ViewState("vsDt") = vsDt
        BindGridViewButiran()
    End Sub

    Protected Sub gvPenyataAkaun_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvPenyataAkaun.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'If e.Row.DataItemIndex = 0 Then
            '    Dim lbtnEdit As LinkButton = DirectCast(e.Row.FindControl("lbtnEdit"), LinkButton)
            '    Dim lbtnDelete As LinkButton = DirectCast(e.Row.FindControl("lbtnDelete"), LinkButton)
            '    lbtnEdit.Visible = False
            '    lbtnDelete.Visible = False
            'End If
            If e.Row.RowState And DataControlRowState.Edit Then

                Dim row As DataRowView = e.Row.DataItem
                Dim ddlKodBank As DropDownList = CType(e.Row.FindControl("ddlKodBank"), DropDownList)
                'Dim ddlKo As DropDownList = CType(e.Row.FindControl("ddlKo"), DropDownList)
                'Dim ddlPTJ As DropDownList = CType(e.Row.FindControl("ddlPTJ"), DropDownList)
                'Dim ddlKp As DropDownList = CType(e.Row.FindControl("ddlKp"), DropDownList)
                'Dim ddlVot As DropDownList = CType(e.Row.FindControl("ddlVot"), DropDownList)

                ' get selected value of dropdownlist
                'fBindDdlGv(ddlKodBank, ddlKo, ddlPTJ, ddlKp, ddlVot, row(4).ToString, row(5).ToString, row(6).ToString, row(7).ToString, row(8).ToString)
            End If

        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            Dim row As DataRowView = e.Row.DataItem
            Dim ddlKodBank As DropDownList = CType(e.Row.FindControl("ddlKodBank"), DropDownList)
            'Dim ddlKo As DropDownList = CType(e.Row.FindControl("ddlKo"), DropDownList)
            'Dim ddlPTJ As DropDownList = CType(e.Row.FindControl("ddlPTJ"), DropDownList)
            'Dim ddlKp As DropDownList = CType(e.Row.FindControl("ddlKp"), DropDownList)
            'Dim ddlVot As DropDownList = CType(e.Row.FindControl("ddlVot"), DropDownList)

            ' get selected value of dropdownlist
            fBindDdlKodBank(ddlKodBank)

        End If
    End Sub

    Private Sub BindGridViewButiran()

        Dim vsDt As DataTable = TryCast(ViewState("vsDt"), DataTable)

        If vsDt.Rows.Count = 0 Then
            Dim dr As DataRow = vsDt.NewRow()
            vsDt.Rows.Add(dr)
            'txtJumBayaran.Text = ""
            'txtJumlahSebenar.Text = ""
        Else
            'Dim total? As Decimal = vsDt.AsEnumerable().Sum(Function(row) row.Field(Of Decimal?)("RC01_Kredit"))
            'txtJumBayaran.Text = total.ToString()
            'txtJumlahSebenar.Text = total.ToString()
        End If

        gvPenyataAkaun.DataSource = vsDt
        gvPenyataAkaun.DataBind()
    End Sub

    Private Sub fBindDdlKodBank(ddl As DropDownList)

        Dim strsql = "Select KodBank, (KodBank + ' - ' + NamaBank ) as Butiran from MK_Bank ORDER BY NamaBank"

        Dim ds = dbconn.fselectCommandDs(strsql)
        ddl.DataSource = ds
        ddl.DataTextField = "Butiran"
        ddl.DataValueField = "KodBank"
        ddl.DataBind()

        ddl.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
        ddl.SelectedIndex = 0
    End Sub

    Private Function getButiranVot(kod) As String
        Dim butiranVot As String = ""
        Dim str = $"Select butiran from mk_vot where kodVot ='{ kod }';"
        dbconn.sSelectCommand(str, butiranVot)
        Return butiranVot
    End Function

End Class