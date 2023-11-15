Imports System.Drawing
Imports System.Data.SqlClient
Public Class Orang_Awam
    Inherits System.Web.UI.Page

    Public Shared dvOrangAwam As New DataView
    Public Shared dsOrangAwam As New DataSet
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                If Session("LoggedIn") Then
                    fBindGVOA()
                    ViewState("savemode") = 1
                    btnHapus.Enabled = False
                    '  lbtnSimpan.Enabled = False
                    fBindDdlNegara()
                    ddlNegeri.Items.Add(New ListItem("- SILA PILIH NEGARA -", 0))
                    'fBindDdlNegara()
                    BindGVOA()
                Else
                    Response.Redirect("../../Logout.aspx")
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub fBindGVOA()
        Try
            Dim dt As New DataTable
            dt = fCreateDtOA()

            gvOrangAwam.DataSource = dt
            gvOrangAwam.DataBind()

        Catch ex As Exception

        End Try
    End Sub
    Private Function fCreateDtOA() As DataTable
        Try
            Dim ds As New DataSet
            Dim dt As New DataTable

            Dim IDNo_KP = New DataColumn("IDNo_KP", GetType(String))
            Dim Nama = New DataColumn("IDNama", GetType(String))
            Dim Alamat1 = New DataColumn("Alamat1", GetType(String))
            Dim Alamat2 = New DataColumn("Alamat2", GetType(String))
            Dim KodNegara = New DataColumn("KodNegara", GetType(String))
            Dim KodNegeri = New DataColumn("KodNegeri", GetType(String))
            Dim Bandar = New DataColumn("Bandar", GetType(String))
            Dim Poskod = New DataColumn("Poskod", GetType(String))
            Dim No_Tel = New DataColumn("No_Tel", GetType(String))
            Dim No_Fax = New DataColumn("No_Fax", GetType(String))
            Dim Emel = New DataColumn("Emel", GetType(String))
            Dim Status = New DataColumn("Status", GetType(String))

            dt.Columns.Add(IDNo_KP)
            dt.Columns.Add(Nama)
            dt.Columns.Add(Alamat1)
            dt.Columns.Add(Alamat2)
            dt.Columns.Add(KodNegara)
            dt.Columns.Add(KodNegeri)
            dt.Columns.Add(Bandar)
            dt.Columns.Add(Poskod)
            dt.Columns.Add(No_Tel)
            dt.Columns.Add(No_Fax)
            dt.Columns.Add(Emel)
            dt.Columns.Add(Status)

            Dim strIDNO_KP As String
            Dim strNama As String
            Dim strAlamat1 As String
            Dim strAlamat2 As String
            Dim strKodNegara As String
            Dim strKodNegeri As String
            Dim strBandar As String
            Dim strPoskod As String
            Dim strNoTel As String
            Dim strNoFax As String
            Dim strEmel As String
            Dim strStatus As String
            Dim blnStatus As Boolean

            ds = BindGVOA()
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                strIDNO_KP = ds.Tables(0).Rows(i)(0).ToString
                strNama = ds.Tables(0).Rows(i)(1).ToString
                strAlamat1 = ds.Tables(0).Rows(i)(2).ToString
                strAlamat2 = ds.Tables(0).Rows(i)(3).ToString
                strKodNegara = ds.Tables(0).Rows(i)(4).ToString
                strKodNegeri = ds.Tables(0).Rows(i)(5).ToString
                strBandar = ds.Tables(0).Rows(i)(6).ToString
                strPoskod = ds.Tables(0).Rows(i)(7).ToString
                strNoTel = ds.Tables(0).Rows(i)(8).ToString
                strNoFax = ds.Tables(0).Rows(i)(9).ToString
                strEmel = ds.Tables(0).Rows(i)(10).ToString
                blnStatus = ds.Tables(0).Rows(i)(11)

                'strIDNO_KP = ds.Tables(0).Rows(i)("IDNo_KP").ToString
                'strNama = ds.Tables(0).Rows(i)("IDNama").ToString
                'strAlamat1 = ds.Tables(0).Rows(i)("Alamat1").ToString
                'strAlamat2 = ds.Tables(0).Rows(i)("Alamat2").ToString
                'strBandar = ds.Tables(0).Rows(i)("Bandar").ToString
                'strPoskod = ds.Tables(0).Rows(i)("Poskod").ToString
                'strKodNegeri = ds.Tables(0).Rows(i)("KodNegeri").ToString
                'strKodNegara = ds.Tables(0).Rows(i)("KodNegara").ToString
                'strNoTel = ds.Tables(0).Rows(i)("No_Tel").ToString
                'strNoFax = ds.Tables(0).Rows(i)("No_Fax").ToString
                'strEmel = ds.Tables(0).Rows(i)("Emel").ToString
                'blnStatus = ds.Tables(0).Rows(i)("Status").ToString



                If blnStatus = True Then strStatus = "Aktif" Else strStatus = "Tidak Aktif"

                dt.Rows.Add(strIDNO_KP, strNama, strAlamat1, strAlamat2, strKodNegara, strKodNegeri, strBandar, strPoskod, strNoTel, strNoFax, strEmel, strStatus)

            Next

            gvOrangAwam.DataSource = dt
            gvOrangAwam.DataBind()

            Return dt
        Catch ex As Exception

        End Try

    End Function
    Private Function BindGVOA() As DataSet
        Try
            Dim intRec As Integer

            Dim strSql As String = "select * from mk_orang_awam order by IDNo_KP  asc"

            Dim dbconn As New DBKewConn
            Dim dt As New DataTable

            dsOrangAwam = dbconn.fselectCommand(strSql)
            dvOrangAwam = New DataView(dsOrangAwam.Tables(0))

            intRec = dsOrangAwam.Tables(0).Rows.Count
            lblJumRekod.InnerText = intRec

            Return dsOrangAwam

        Catch ex As Exception

        End Try

    End Function

    Private Sub gvOrangAwam_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvOrangAwam.PageIndexChanging
        Try
            gvOrangAwam.PageIndex = e.NewPageIndex
            If Session("SortedView") IsNot Nothing Then
                gvOrangAwam.DataSource = Session("SortedView")
                gvOrangAwam.DataBind()
            Else
                Dim dt As New DataTable
                dt = fCreateDtOA()
                gvOrangAwam.DataSource = dt
                gvOrangAwam.DataBind()

            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub fBindDdlNegeri()
        Try
            Dim strsql As String
            strsql = "select KodNegeri , (KodNegeri + ' - ' + Butiran ) as Butiran from MK_Negeri order by KodNegeri"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fselectCommand(strsql)
            ddlNegeri.DataSource = ds
            ddlNegeri.DataTextField = "Butiran"
            ddlNegeri.DataValueField = "KodNegeri"
            ddlNegeri.DataBind()

            'ddlNegeri.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlNegeri.Items.Add(New ListItem("-SILA PILIH-", "0"))
            ddlNegeri.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub
    Private Sub fBindDdlNegara()
        Try
            Dim strsql As String
            strsql = "select KodNegara , (KodNegara + ' - ' + Butiran ) as Butiran from MK_Negara order by KodNegara "

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fselectCommand(strsql)
            ddlNegara.DataSource = ds
            ddlNegara.DataTextField = "Butiran"
            ddlNegara.DataValueField = "KodNegara"
            ddlNegara.DataBind()

            ddlNegara.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            'ddlNegara.Items.Add(New ListItem("-SILA PILIH-", "0"))
            ddlNegara.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlNegara_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNegara.SelectedIndexChanged
        fBindDdlNegeri()
    End Sub

    Private Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.Click
        Dim dbconn As New DBKewConn
        Try
            Dim strSql As String

            If txtIDNo.Text = "" Or txtNama.Text = "" Or txtAlamat1.Text = "" Or txtAlamat2.Text = "" Or txtBandar.Text = "" Or txtPoskod.Text = "" Or txtNoTel.Text = "" Or txtNoFax.Text = "" Or txtEmel.Text = "" Or ddlNegeri.SelectedValue = 0 Or ddlNegara.SelectedValue = 0 Then
                fGlobalAlert("Sila pastikan tiada ruang yang dibiarkan kosong", Me.Page, Me.[GetType]())
                Exit Sub
            End If

            Dim strIDNO_KP As String = Trim(txtIDNo.Text.TrimEnd.ToUpper)
            Dim strNama As String = Trim(txtNama.Text.TrimEnd.ToUpper)
            Dim strAlamat1 As String = Trim(txtAlamat1.Text.TrimEnd.ToUpper)
            Dim strAlamat2 As String = Trim(txtAlamat2.Text.TrimEnd.ToUpper)
            Dim strBandar As String = Trim(txtBandar.Text.TrimEnd.ToUpper)
            Dim strPoskod As String = Trim(txtPoskod.Text.TrimEnd.ToUpper)
            Dim strKodNegeri As String = Trim(ddlNegeri.SelectedValue.TrimEnd.ToUpper)
            Dim strKodNegara As String = Trim(ddlNegara.SelectedValue.TrimEnd.ToUpper)
            Dim strNoTel As String = Trim(txtNoTel.Text.TrimEnd.ToUpper)
            Dim strNoFax As String = Trim(txtNoFax.Text.TrimEnd.ToUpper)
            Dim strEmel As String = Trim(txtEmel.Text.TrimEnd)
            Dim intStatus As Integer = rbStatus.SelectedValue

            If ViewState("savemode") = 1 Then

                strSql = "SELECT COUNT (IDNo_KP)  FROM MK_Orang_Awam WHERE IDNo_KP='" & strIDNO_KP & "'"

                If fCheckRec(strSql) > 0 Then
                    ViewState("savemode") = 2
                    'fGlobalAlert("Rekod telah wujud di dalam pangkalan data!", Me.Page, Me.[GetType]())
                    Exit Sub
                Else

                    'INSERT INTO MK_Orang_Awam
                    strSql = "insert into MK_Orang_Awam (IDNo_KP ,IDNama, Alamat1,Alamat2,Bandar,Poskod,KodNegeri,KodNegara,No_Tel,No_Fax,Emel,Status) VALUES (@IDNo_KP,@IDNama, @Alamat1, @Alamat2, @Bandar,@Poskod,@KodNegeri,@KodNegara,@No_Tel,@No_Fax,@Emel,@Status)"

                    Dim paramSql() As SqlParameter =
                {
                    New SqlParameter("@IDNo_KP", strIDNO_KP),
                    New SqlParameter("@IDNama", strNama),
                    New SqlParameter("@Alamat1", strAlamat1),
                    New SqlParameter("@Alamat2", strAlamat2),
                    New SqlParameter("@Bandar", strBandar),
                    New SqlParameter("@Poskod", strPoskod),
                    New SqlParameter("@KodNegeri", strKodNegeri),
                    New SqlParameter("@KodNegara", strKodNegara),
                    New SqlParameter("@No_Tel", strNoTel),
                    New SqlParameter("@No_Fax", strNoFax),
                    New SqlParameter("@Emel", strEmel),
                    New SqlParameter("@Status", intStatus)
                   }
                    dbconn.sConnBeginTrans()
                    If dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                        dbconn.sConnCommitTrans()
                        fGlobalAlert("Rekod telah disimpan!", Me.Page, Me.[GetType]())
                        Clearfield()
                        fBindGVOA()
                    Else
                        dbconn.sConnRollbackTrans()
                        fGlobalAlert("Rekod gagal disimpan!", Me.Page, Me.[GetType]())
                        Exit Sub
                    End If
                End If

            ElseIf ViewState("savemode") = 2 Then

                'UPDATE MK_Orang_Awam
                strSql = "update MK_Orang_Awam SET IDNo_KP = @IDNo_KP, IDNama = @IDNama, Alamat1 = @Alamat1,Alamat2 = @Alamat2,Bandar = @Bandar,Poskod = @Poskod,KodNegeri = @KodNegeri,KodNegara = @KodNegara,No_Tel = @No_Tel,No_Fax = @No_Fax,Emel = @Emel, Status = @Status  where IDNo_KP = @IDNo_KP"

                Dim paramSql() As SqlParameter = {
                        New SqlParameter("@IDNo_KP", strIDNO_KP),
                        New SqlParameter("@IDNama", strNama),
                        New SqlParameter("@Alamat1", strAlamat1),
                        New SqlParameter("@Alamat2", strAlamat2),
                        New SqlParameter("@Bandar", strBandar),
                        New SqlParameter("@Poskod", strPoskod),
                        New SqlParameter("@KodNegeri", strKodNegeri),
                        New SqlParameter("@KodNegara", strKodNegara),
                        New SqlParameter("@No_Tel", strNoTel),
                        New SqlParameter("@No_Fax", strNoFax),
                        New SqlParameter("@Emel", strEmel),
                        New SqlParameter("@Status", intStatus)
                }
                dbconn.sConnBeginTrans()
                If dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                    dbconn.sConnCommitTrans()
                    fGlobalAlert("Rekod telah dikemaskini!", Me.Page, Me.[GetType]())
                    Clearfield()
                    txtIDNo.Enabled = True
                    fBindGVOA()
                Else
                    dbconn.sConnRollbackTrans()
                    fGlobalAlert("Rekod gagal dikemaskini!", Me.Page, Me.[GetType]())
                    Exit Sub

                End If
            End If


        Catch ex As Exception
            dbconn.sConnRollbackTrans()
            fGlobalAlert("Rekod telah wujud di dalam pangkalan data & maklumat gagal disimpan!", Me.Page, Me.[GetType]())
            txtIDNo.Enabled = True
        End Try
    End Sub
    Private Function Clearfield()
        Try
            txtIDNo.Text = ""
            txtNama.Text = ""
            txtAlamat1.Text = ""
            txtAlamat2.Text = ""
            txtBandar.Text = ""
            txtPoskod.Text = ""
            txtEmel.Text = ""
            txtNoTel.Text = ""
            txtNoFax.Text = ""
            ddlNegeri.SelectedValue = 0
            ddlNegara.SelectedValue = 0

        Catch ex As Exception

        End Try
    End Function

    Private Sub lbtnBaru_Click(sender As Object, e As EventArgs) Handles lbtnBaru.Click
        Try
            txtIDNo.Text = ""
            txtNama.Text = ""
            txtAlamat1.Text = ""
            txtAlamat2.Text = ""
            txtBandar.Text = ""
            txtPoskod.Text = ""
            txtEmel.Text = ""
            txtNoTel.Text = ""
            txtNoFax.Text = ""
            ddlNegeri.SelectedValue = 0
            ddlNegara.SelectedValue = 0
            ViewState("savemode") = 1
            txtIDNo.Enabled = True
            btnHapus.Enabled = True

        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvOrangAwam_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvOrangAwam.RowCommand
        Try
            If e.CommandName = "Select" Then

                Dim intStatus As Integer

                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvOrangAwam.Rows(index)

                Dim strIDNO_KP As String = selectedRow.Cells(1).Text
                Dim strNama As String = selectedRow.Cells(2).Text
                Dim strAlamat1 As String = selectedRow.Cells(3).Text
                Dim strAlamat2 As String = selectedRow.Cells(4).Text
                Dim strKodNegara As String = selectedRow.Cells(5).Text
                Dim strKodNegeri As String = selectedRow.Cells(6).Text
                Dim strBandar As String = selectedRow.Cells(7).Text
                Dim strPoskod As String = selectedRow.Cells(8).Text
                Dim strNoTel As String = selectedRow.Cells(9).Text
                Dim strNoFax As String = selectedRow.Cells(10).Text
                Dim strEmel As String = selectedRow.Cells(11).Text
                Dim strStatus As String = selectedRow.Cells(12).Text

                'ddlNegeri.SelectedValue = strKodNegeri

                'fBindDdlNegara()
                'ddlNegara.SelectedValue = strKodNegara

                selectedRow.ForeColor = ColorTranslator.FromHtml("#0000FF")
                selectedRow.ToolTip = String.Empty

                txtIDNo.Text = strIDNO_KP
                txtNama.Text = strNama
                txtAlamat1.Text = strAlamat1
                txtAlamat2.Text = strAlamat2
                txtBandar.Text = strBandar
                txtPoskod.Text = strPoskod
                txtNoTel.Text = strNoTel
                txtNoFax.Text = strNoFax
                txtEmel.Text = strEmel
                rbStatus.SelectedValue = strStatus

                ddlNegara.SelectedValue = strKodNegara
                fBindDdlNegeri()
                ddlNegeri.SelectedValue = strKodNegeri


                If strStatus = "Aktif" Then intStatus = 1 Else intStatus = 0
                rbStatus.SelectedValue = intStatus

                ViewState("savemode") = 2

                btnHapus.Enabled = True
                ' lbtnBaru.Enabled = False
                txtIDNo.Enabled = False
            End If
        Catch ex As Exception
        End Try

    End Sub

    Private Sub btnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        Dim dbconn As New DBKewConn
        Try
            Dim strsql As String

            If txtIDNo.Text = "" Or txtNama.Text = "" Then
                fGlobalAlert("Sila pilih rekod yang ingin dihapuskan", Me.Page, Me.[GetType]())
                Exit Sub
            End If


            Dim strIDNO_KP As String = Trim(txtIDNo.Text.TrimEnd.ToUpper)
            Dim strNama As String = Trim(txtNama.Text.TrimEnd.ToUpper)
            Dim strAlamat1 As String = Trim(txtAlamat1.Text.TrimEnd.ToUpper)
            Dim strAlamat2 As String = Trim(txtAlamat2.Text.TrimEnd.ToUpper)
            Dim strBandar As String = Trim(txtBandar.Text.TrimEnd.ToUpper)
            Dim strPoskod As String = Trim(txtPoskod.Text.TrimEnd.ToUpper)
            Dim strKodNegeri As String = Trim(ddlNegeri.SelectedValue.TrimEnd.ToUpper)
            Dim strKodNegara As String = Trim(ddlNegara.SelectedValue.TrimEnd.ToUpper)
            Dim strNoTel As String = Trim(txtNoTel.Text.TrimEnd.ToUpper)
            Dim strNoFax As String = Trim(txtNoFax.Text.TrimEnd.ToUpper)
            Dim strEmel As String = Trim(txtEmel.Text.TrimEnd)
            Dim intStatus As Integer = rbStatus.SelectedValue

            strsql = "SELECT COUNT (IDNo_KP)  FROM MK_Orang_Awam WHERE IDNo_KP='" & strIDNO_KP & "'"

            If fCheckRec(strsql) > 0 Then

                strsql = "delete from MK_Orang_Awam WHERE IDNo_KP='" & strIDNO_KP & "'"

                Dim paramSql() As SqlParameter =
                {
                   New SqlParameter("@IDNo_KP", strIDNO_KP),
                        New SqlParameter("@IDNama", strNama)
                   }
                dbconn.sConnBeginTrans()
                If dbconn.fUpdateCommand(strsql, paramSql) > 0 Then
                    dbconn.sConnCommitTrans()
                    fGlobalAlert("Rekod telah dipadam!", Me.Page, Me.[GetType]())
                    Clearfield()
                    txtIDNo.Enabled = True
                    fBindGVOA()
                Else
                    dbconn.sConnRollbackTrans()
                    fGlobalAlert("Rekod gagal dipadam!", Me.Page, Me.[GetType]())
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            dbconn.sConnRollbackTrans()
        End Try

    End Sub

    Private Sub gvOrangAwam_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvOrangAwam.SelectedIndexChanged
        Try
            For Each row As GridViewRow In gvOrangAwam.Rows
                If row.RowIndex = gvOrangAwam.SelectedIndex Then

                    row.ForeColor = ColorTranslator.FromHtml("#0000FF")
                Else
                    row.ForeColor = ColorTranslator.FromHtml("#000000")
                End If
            Next
            ViewState("SaveMode") = 2 'New
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnFindOA_ServerClick(sender As Object, e As EventArgs) Handles btnFindOA.ServerClick
        Try
            Dim strsql As String
            Dim intStatus As Integer

            Dim IDNo As String = Trim(txtIDNo.Text.ToString)
            txtIDNo.Enabled = False

            strsql = "SELECT COUNT (IDNo_KP)  FROM MK_Orang_Awam WHERE IDNo_KP='" & IDNo & "'"

            If fCheckRec(strsql) > 0 Then
                fGlobalAlert("Rekod telah wujud di dalam pangkalan data!", Me.Page, Me.[GetType]())
            End If


            strsql = "select * from mk_orang_Awam where IDNo_KP ='" & IDNo & "'"

            Dim strStatus As String
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strsql)

            If ds.Tables(0).Rows.Count > 0 Then
                txtNama.Text = ds.Tables(0).Rows(0)("IDNama").ToString
                txtAlamat1.Text = ds.Tables(0).Rows(0)("alamat1").ToString
                txtAlamat2.Text = ds.Tables(0).Rows(0)("alamat2").ToString
                txtBandar.Text = ds.Tables(0).Rows(0)("Bandar").ToString
                txtPoskod.Text = ds.Tables(0).Rows(0)("Poskod").ToString
                ddlNegara.SelectedValue = ds.Tables(0).Rows(0)("KodNegara").ToString
                fBindDdlNegeri()
                ddlNegeri.SelectedValue = ds.Tables(0).Rows(0)("KodNegeri").ToString
                txtNoTel.Text = ds.Tables(0).Rows(0)("No_Tel").ToString
                txtNoFax.Text = ds.Tables(0).Rows(0)("No_Fax").ToString
                txtEmel.Text = ds.Tables(0).Rows(0)("Emel").ToString
                rbStatus.SelectedValue = strStatus

                If strStatus = "Aktif" Then intStatus = 1 Else intStatus = 0
                rbStatus.SelectedValue = intStatus
            End If
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub
End Class