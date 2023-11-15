Imports System.Data.SqlClient
Imports System.Drawing

Public Class MaklumatPendaftaran
    Inherits System.Web.UI.Page

    Private dbconn As New DBKewConn


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Not Session("LoggedIn") Then
                Response.Redirect("../../Logout.aspx")
                Exit Sub
            End If

            Dim KodSubmenu = Request.QueryString("KodSubMenu")
            txtNoSya.Text = Request.QueryString("noSya")
            hdNoIDSem.Value = Request.QueryString("noSem")

            fBindDdlNegara()
            fBindDdlNegeri()
            fBindDdlKodBank()
            fLoadDetail()

        End If

    End Sub

    Private Sub fLoadDetail()
        Try
            If hdNoIDSem.Value.Equals(String.Empty) Then
                txtNoSya.Text = ""
                txtNamaSya.Text = ""
                txtNamaPendaftar.Text = ""
                txtAlamat1.Text = ""
                txtAlamat2.Text = ""
                txtPoskod.Text = ""
                txtBandar.Text = ""
                txtTelp1.Text = ""
                txtTelp2.Text = ""
                txtWeb.Text = ""
                txtEmailSya.Text = ""
                'rbKategoriSyarikat.ClearSelection()
                Exit Sub
            End If

            Dim strSql As String = $"SELECT [ROC01_IDSya],[ROC01_NoSya],[ROC01_NamaSya],ROC01_KategoriSya,
[ROC01_AlmtP1], [ROC01_AlmtP2], [ROC01_BandarP], [ROC01_PoskodP], [ROC01_NegeriP], [ROC01_NegaraP], [ROC01_Tel1P],
[ROC01_Tel2P], [ROC01_FaksP], [ROC01_WebP], ROC01_EmelP, ROC01_WakilSya,[ROC01_KodBank],[ROC01_NoAkaun], ROC01_KodAktif,
ROC01_Cawangan, ROC01_SyktPnjm
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
                    txtEmailSya.Text = dt.Rows(0)("ROC01_EmelP").ToString
                    txtFax.Text = dt.Rows(0)("ROC01_FaksP").ToString

                    ddlNegara.SelectedValue = dt.Rows(0)("ROC01_NegaraP").ToString
                    ddlNegeri.SelectedValue = dt.Rows(0)("ROC01_NegeriP").ToString
                    Dim kodbank = dt.Rows(0)("ROC01_KodBank").ToString
                    ddlKodBank.SelectedValue = kodbank
                    txtNoAkaun.Text = dt.Rows(0)("ROC01_NoAkaun").ToString
                    'rbKategoriSyarikat.SelectedValue = dt.Rows(0)("ROC01_KategoriSya").ToString
                    ddlAktif.SelectedValue = dt.Rows(0)("ROC01_KodAktif").ToString
                    If dt.Rows(0)("ROC01_Cawangan") = "Y" Then
                        chxCawangan.Checked = True
                    End If

                    If dt.Rows(0)("ROC01_SyktPnjm") = "Y" Then
                        chxSyarikatPinjaman.Checked = True
                    End If

                    'Disable jika vendor register sendiri guna evendor
                    If Not IsDBNull(dt.Rows(0)("ROC01_KategoriSya")) And dt.Rows(0)("ROC01_KategoriSya") = 2 Then
                        txtNoSya.ReadOnly = True
                        txtNamaSya.ReadOnly = True
                        txtNamaPendaftar.ReadOnly = True
                        txtAlamat1.ReadOnly = True
                        txtAlamat2.ReadOnly = True
                        txtPoskod.ReadOnly = True
                        txtBandar.ReadOnly = True
                        txtTelp1.ReadOnly = True
                        txtTelp2.ReadOnly = True
                        txtWeb.ReadOnly = True
                        txtEmailSya.ReadOnly = True
                        txtFax.ReadOnly = True

                        ddlNegara.Enabled = False
                        ddlNegeri.Enabled = False
                        ddlKodBank.Enabled = False
                        txtNoAkaun.Text = dt.Rows(0)("ROC01_NoAkaun").ToString
                    End If
                End If
            End Using
        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindDdlNegeri()
        Try
            Dim strSql As String = $"SELECT KodNegeri, Butiran FROM MK_Negeri ORDER BY Butiran"

            Dim ds As New DataSet
            ds = dbconn.fselectCommand(strSql)


            ddlNegeri.DataSource = ds
            ddlNegeri.DataTextField = "Butiran"
            ddlNegeri.DataValueField = "KodNegeri"
            ddlNegeri.DataBind()

            ddlNegeri.Items.Insert(0, New ListItem("-Sila Pilih-", ""))
            ddlNegeri.SelectedIndex = 0

        Catch ex As Exception
            'fErrorLog("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
        End Try
    End Sub

    Private Sub fBindDdlNegara()
        Try
            Dim strSql As String = $"SELECT KodNegara, Butiran FROM MK_Negara ORDER BY Butiran"

            Dim ds As New DataSet
            ds = dbconn.fselectCommand(strSql)

            ddlNegara.DataSource = ds
            ddlNegara.DataTextField = "Butiran"
            ddlNegara.DataValueField = "KodNegara"
            ddlNegara.DataBind()
            '121 - Malaysia
            ddlNegara.SelectedValue = "121"

        Catch ex As Exception
            'fErrorLog("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
        End Try
    End Sub

    Private Sub fBindDdlKodBank()

        'Dim strSql As String = $"SELECT KodBank, (KodBank + ' - ' + NamaBank) As Butiran FROM MK_Bank ORDER BY NamaBank"
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

    Private Function fJanaNoROCNC()
        Dim strNoROC As String = ""
        Dim indexPO As Integer = 0
        Dim noakhir As Double
        Dim strSQL As String = $"SELECT Top(1) NoAkhir From MK_NoAkhir WHERE KodModul = 'ROC' AND Prefix = 'NC' AND Tahun=2015"
        dbconn.sSelectCommand(strSQL, noakhir)

        If noakhir <> Nothing Then
            indexPO = noakhir + 1
        Else
            indexPO = 1
        End If

        strNoROC = "NC" + Format(indexPO, "000000").ToString
        Return strNoROC
    End Function

    Private Sub UpdateNoAkhirROCNC()
        Dim strSql = "SELECT Top(1) * From MK_NoAkhir WHERE KodModul = 'ROC' AND Prefix = 'NC' AND Tahun=2015"
        Dim ds = dbconn.fSelectCommand(strSql, "MKNoAkhir")

        If ds.Tables(0).Rows.Count > 0 Then
            ds.Tables("MKNoAkhir").Rows(0)("NoAkhir") = ds.Tables("MKNoAkhir").Rows(0)("NoAkhir") + 1
        Else
            Dim dr As DataRow
            dr = ds.Tables("MKNoAkhir").NewRow
            dr("KodModul") = "ROC"
            dr("Prefix") = "NC"
            dr("noakhir") = 1
            dr("Butiran") = "ROC NC"
            dr("kodPTJ") = ""
            ds.Tables("MKNoAkhir").Rows.Add(dr)
        End If

        dbconn.sUpdateCommand(ds, strSql)
    End Sub

    Private Function fJanaNoROCRC()
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

    Private Sub UpdateNoAkhirROCRC()
        Dim strSql = "SELECT TOP(1) * From MK_NoAkhir WHERE KodModul = 'ROC' AND Prefix = 'RC'"
        Dim ds = dbconn.fSelectCommand(strSql, "MKNoAkhir")

        If ds.Tables(0).Rows.Count > 0 Then
            ds.Tables("MKNoAkhir").Rows(0)("NoAkhir") = ds.Tables("MKNoAkhir").Rows(0)("NoAkhir") + 1
        Else
            Dim dr As DataRow
            dr = ds.Tables("MKNoAkhir").NewRow
            dr("KodModul") = "ROC"
            dr("Prefix") = "RC"
            dr("noakhir") = 1
            dr("Butiran") = "ROC RC"
            dr("kodPTJ") = ""
            ds.Tables("MKNoAkhir").Rows.Add(dr)
        End If

        dbconn.sUpdateCommand(ds, strSql)
    End Sub
    Protected Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.Click
        'If rbKategoriSyarikat.SelectedItem Is Nothing Then
        '    fGlobalAlert("Sila Pilih kategori vendor", Page, Me.GetType)
        '    Exit Sub
        'End If
        Dim bCommit As Boolean = False
        Dim noIDNCsya = fJanaNoROCNC() 'NC
        Dim noIDRCsya = fJanaNoROCRC() 'RC
        Dim foundIDRC = False, foundIDNC = False
        dbconn = New DBKewConn
        Dim strSql As String = $"SELECT ROC01_IDSem,[ROC01_IDSya],[ROC01_NoSya],[ROC01_NamaSya],ROC01_KategoriSya, 
[ROC01_AlmtP1], [ROC01_AlmtP2], [ROC01_BandarP], [ROC01_PoskodP], [ROC01_NegeriP], [ROC01_NegaraP], [ROC01_Tel1P],
[ROC01_Tel2P], [ROC01_FaksP], [ROC01_WebP], ROC01_EmelP, ROC01_WakilSya,[ROC01_KodBank],[ROC01_NoAkaun], ROC01_KodAktif, 
ROC01_Cawangan, ROC01_SyktPnjm, ROC01_KodLulus, ROC01_KategoriSya
FROM [ROC01_Syarikat] 
WHERE [ROC01_IDSem] = @idSem;"

        Dim paramSql() As SqlParameter = {
                New SqlParameter("@idSem", hdNoIDSem.Value)
            }

        Using ds = dbconn.fSelectCommand(strSql, "DsSya", paramSql)
            Dim dt = ds.Tables(0)
            If dt.Rows.Count > 0 Then
                If dt.Rows(0)("ROC01_IDSya").ToString.Equals("") Then
                    If IsDBNull(dt.Rows(0)("ROC01_KategoriSya")) Then
                        dt.Rows(0)("ROC01_IDSya") = noIDNCsya
                        foundIDNC = True
                    ElseIf dt.Rows(0)("ROC01_KategoriSya") = 2 Then
                        dt.Rows(0)("ROC01_IDSya") = noIDRCsya
                        foundIDRC = True
                    End If
                End If
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
                    'dt.Rows(0)("ROC01_KategoriSya") = rbKategoriSyarikat.SelectedValue
                    dt.Rows(0)("ROC01_KodAktif") = ddlAktif.SelectedValue
                    dt.Rows(0)("ROC01_KodLulus") = "1"

                    If chxCawangan.Checked Then
                        dt.Rows(0)("ROC01_Cawangan") = "Y"
                    Else
                        dt.Rows(0)("ROC01_Cawangan") = "T"
                    End If

                    If chxSyarikatPinjaman.Checked Then
                        dt.Rows(0)("ROC01_SyktPnjm") = "Y"
                    Else
                        dt.Rows(0)("ROC01_SyktPnjm") = "T"
                    End If
                Else
                    Dim dr As DataRow
                dr = dt.NewRow
                foundIDNC = True
                dr("ROC01_IDSya") = noIDNCsya
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
                dr("ROC01_WakilSya") = txtNamaPendaftar.Text
                dr("ROC01_EmelP") = txtEmailSya.Text
                dr("ROC01_KodBank") = ddlKodBank.SelectedValue
                dr("ROC01_NoAkaun") = txtNoAkaun.Text
                'dr("ROC01_KategoriSya") = rbKategoriSyarikat.SelectedValue
                dr("ROC01_KodAktif") = ddlAktif.SelectedValue
                dr("ROC01_KodLulus") = "1"

                If chxCawangan.Checked Then
                    dt.Rows(0)("ROC01_Cawangan") = "Y"
                Else
                    dt.Rows(0)("ROC01_Cawangan") = "T"
                End If

                If chxSyarikatPinjaman.Checked Then
                    dt.Rows(0)("ROC01_SyktPnjm") = "Y"
                Else
                    dt.Rows(0)("ROC01_SyktPnjm") = "T"
                End If

                dt.Rows.Add(dr)
            End If

            dbconn.sUpdateCommand(ds, strSql, False, True, bCommit)
        End Using
        If bCommit Then
            If foundIDNC Then
                UpdateNoAkhirROCNC()
            ElseIf foundIDRC Then
                UpdateNoAkhirROCRC()
            End If
            fGlobalAlert("Rekod telah disimpan", Me.Page, Me.GetType(), $"../../PENDAFTARAN SYARIKAT/SYARIKAT TIDAK BERDAFTAR/Semakan_Syarikat.aspx?KodSub=0702&KodSubMenu=070201")
            Else
                fGlobalAlert("Rekod tidak berjaya disimpan", Me.Page, Me.GetType())
        End If
    End Sub

    Protected Sub lbtnKembali_Click(sender As Object, e As EventArgs) Handles lbtnKembali.Click
        Response.Redirect($"~/Forms/PENDAFTARAN SYARIKAT/SYARIKAT TIDAK BERDAFTAR/Semakan_Syarikat.aspx?KodSub=0702&KodSubMenu=070201")
    End Sub
End Class