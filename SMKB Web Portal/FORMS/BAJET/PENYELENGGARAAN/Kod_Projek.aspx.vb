Imports System.Data.SqlClient

Public Class Kod_Projek
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                ViewState("mode") = 1
                ViewState("modePfx") = 1

                TabContainer1.ActiveTabIndex = 0
                fBindDdlKatPrefix()
                fBindGvPfx()
            End If
        Catch ex As Exception
            fErrorLog(ex.Message.ToString)
        End Try

    End Sub

    Private Sub fBindDdlKatPrefix()
        Try
            Dim strSql As String = "select KodKategori, (KodKategori + ' - ' + Butiran) as Butiran from MK_KategoriPrefix"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)
            ViewState("dsKatPfx") = ds

            ddlKatPrefix.DataSource = ds
            ddlKatPrefix.DataTextField = "Butiran"
            ddlKatPrefix.DataValueField = "KodKategori"
            ddlKatPrefix.DataBind()

            ddlKatPrefix.Items.Insert(0, New ListItem("- SILA PILIH -", "0"))
            ddlKatPrefix.SelectedIndex = 0

            ddlKatPrefix2.DataSource = ds
            ddlKatPrefix2.DataTextField = "Butiran"
            ddlKatPrefix2.DataValueField = "KodKategori"
            ddlKatPrefix2.DataBind()

            ddlKatPrefix2.Items.Insert(0, New ListItem("- SILA PILIH -", "0"))
            ddlKatPrefix2.SelectedIndex = 0

        Catch ex As Exception
            fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
        End Try
    End Sub

    Private Sub fBindDdlPrefix(ByVal strKatPrefix As String)
        Try
            Dim strSql As String = "select KodPrefix from MK_KPPrefix where Status = 1 and KodKat = '" & strKatPrefix & "' order by KodPrefix"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)

            ddlPrefix.DataSource = ds
            ddlPrefix.DataTextField = "KodPrefix"
            ddlPrefix.DataValueField = "KodPrefix"
            ddlPrefix.DataBind()

            ddlPrefix.Items.Insert(0, New ListItem("- SILA PILIH -", "0"))
            ddlPrefix.SelectedIndex = 0
        Catch ex As Exception
            fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
        End Try
    End Sub
    Private Sub fBindGv()
        Try
            Dim intRec As Integer

            gvProjek.DataSource = New List(Of String)
            gvProjek.DataBind()

            Dim strSql As String = "select ID, KodProjek, Butiran, KodKat, Status from MK_KodProjek "

            Dim dbConn As New DBKewConn
            Dim ds As New DataSet
            ds = dbConn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim dt As New DataTable
                    dt.Columns.Add("ID", GetType(String))
                    dt.Columns.Add("KodKP", GetType(String))
                    dt.Columns.Add("Butiran", GetType(String))
                    dt.Columns.Add("KodKat", GetType(String))
                    dt.Columns.Add("Status", GetType(String))

                    Dim strKP, strButiran, strKodKat, strId, strstatus As String

                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        strId = ds.Tables(0).Rows(i)("ID")
                        strKP = ds.Tables(0).Rows(i)("KodProjek")
                        strButiran = IIf(IsDBNull(ds.Tables(0).Rows(i)("Butiran")), "", ds.Tables(0).Rows(i)("Butiran"))
                        strKodKat = IIf(IsDBNull(ds.Tables(0).Rows(i)("KodKat")), "", ds.Tables(0).Rows(i)("KodKat"))
                        strstatus = ds.Tables(0).Rows(i)("Status")
                        If strstatus = False Then
                            strstatus = "Tidak Aktif"
                        ElseIf strstatus = True Then
                            strstatus = "Aktif"
                        End If
                        dt.Rows.Add(strId, strKP, strButiran, strKodKat, strstatus)
                    Next
                    ViewState("dtKP") = dt
                    gvProjek.DataSource = dt
                    gvProjek.DataBind()
                Else
                    intRec = 0
                End If
            Else
                intRec = 0
            End If

            intRec = ds.Tables(0).Rows.Count
            lblJumRekod.InnerText = intRec

        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvProjek_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvProjek.RowCommand
        Try
            If e.CommandName = "Select" Then
                ViewState("mode") = 2
                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvProjek.Rows(index)

                Dim intId As Integer = gvProjek.DataKeys(index).Value
                Dim strKodKP As String = selectedRow.Cells(1).Text
                Dim strButiran As String = Server.HtmlDecode(selectedRow.Cells(2).Text)
                Dim strKodKat As String = selectedRow.Cells(3).Text
                Dim strStatus As String = selectedRow.Cells(4).Text
                Dim intStatus As Integer
                If strStatus = "Aktif" Then
                    intStatus = 1
                ElseIf strStatus = "Tidak Aktif" Then
                    intStatus = 0
                End If


                Dim strPrefix As String
                Dim strKod As String

                If strKodKP.Length = 7 Then
                    strPrefix = strKodKP.Substring(0, 2)

                ElseIf strKodKP.Length = 6 Then
                    strPrefix = strKodKP.Substring(0, 1)

                End If
                strKod = strKodKP.Substring(strKodKP.Length - 5)

                Dim dtPfx As New DataTable
                dtPfx = CType(ViewState("dtPfx"), DataTable)

                ddlKatPrefix2.SelectedValue = strKodKat
                ddlKatPrefix2.Enabled = False

                fBindDdlPrefix(strKodKat)

                ' ddlKatPrefix2.SelectedValue = fGetKatPfx(strPrefix)

                hidIdKP.Value = intId
                ddlPrefix.SelectedValue = strPrefix
                txtKodKP.Text = strKod
                txtButiran.Text = strButiran
                rbStatus.SelectedValue = intStatus
                ddlPrefix.Enabled = False
                lbtnHapus.Visible = True
                txtKodKP.Visible = True
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fReset()
        Try
            ddlKatPrefix2.SelectedValue = 0
            ddlKatPrefix2.Enabled = True

            ddlPrefix.Items.Clear()
            ddlPrefix.Items.Insert(0, New ListItem("- SILA PILIH KATEGORI -", "0"))
            ddlPrefix.SelectedIndex = 0

            txtKodKP.Text = ""
            txtButiran.Text = ""
            ddlPrefix.Enabled = True
            lbtnHapus.Visible = False
            ViewState("mode") = 1
            txtKodKP.Visible = False
        Catch ex As Exception

        End Try
    End Sub

    Private Function fGetKodKP(ByVal strPrefix As String) As String
        Try
            Dim strLastIdx, strIdx As String
            Dim intIdx As Integer
            Dim strSql As String
            strSql = "Select TOP 1 KodProjek from MK_KodProjek  where left(KodProjek ,2) = '" & strPrefix & "' order by KodProjek desc"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strLastIdx = ds.Tables(0).Rows(0)("KodProjek").ToString
                Else
                    strLastIdx = ""
                End If
            Else
                strLastIdx = ""
            End If

            If strLastIdx = "" Then
                strIdx = strPrefix & "00001"
            Else
                strIdx = strLastIdx.Substring(strLastIdx.Length - 5)
                intIdx = CInt(strIdx)
                intIdx = intIdx + 1
                strIdx = strPrefix & intIdx.ToString("D5")
            End If
            Return strIdx

        Catch ex As Exception

        End Try
    End Function

    Private Sub ddlPrefix_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPrefix.SelectedIndexChanged
        If ddlPrefix.SelectedValue = "00" Then
            txtKodKP.Text = "00000"
            txtKodKP.Visible = True
        Else
            txtKodKP.Visible = False
        End If
    End Sub

    Private Sub fResetPfx()
        Try
            txtPrefix.Text = ""
            txtButiranPfx.Text = ""
            txtPrefix.Enabled = True
            lbtnHapusPfx.Visible = False
            ddlKatPrefix.SelectedValue = 0
            ViewState("modePfx") = 1
            ddlKatPrefix.Enabled = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindGvPfx()
        Try
            Dim intRec As Integer

            gvPfx.DataSource = New List(Of String)
            gvPfx.DataBind()

            Dim strSql As String = "select KodPrefix, Butiran,KodKat, Status from MK_KPPrefix"
            Dim dbConn As New DBKewConn
            Dim ds As New DataSet
            ds = dbConn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim dt As New DataTable
                    dt.Columns.Add("KodPrefix", GetType(String))
                    dt.Columns.Add("Butiran", GetType(String))
                    dt.Columns.Add("KodKat", GetType(String))
                    dt.Columns.Add("Status", GetType(String))

                    Dim strPfx, strButiran, strKodKat, strStatus As String
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        strPfx = ds.Tables(0).Rows(i)("KodPrefix")
                        strButiran = IIf(IsDBNull(ds.Tables(0).Rows(i)("Butiran")), "", ds.Tables(0).Rows(i)("Butiran"))
                        strKodKat = IIf(IsDBNull(ds.Tables(0).Rows(i)("KodKat")), "", ds.Tables(0).Rows(i)("KodKat"))
                        strStatus = ds.Tables(0).Rows(i)("Status")
                        If strStatus = "True" Then
                            strStatus = "Aktif"
                        Else
                            strStatus = "Tidak Aktif"
                        End If

                        dt.Rows.Add(strPfx, strButiran, strKodKat, strStatus)
                    Next
                    ViewState("dtPfx") = dt
                    gvPfx.DataSource = dt
                    gvPfx.DataBind()
                Else
                    intRec = 0
                End If
            Else
                intRec = 0
            End If

            intRec = ds.Tables(0).Rows.Count
            lblJumRecPfx.InnerText = intRec

        Catch ex As Exception

        End Try
    End Sub

    Private Function fCheckExist(ByVal strkodPfx As String, ByVal strKodKat As String) As Boolean
        Try
            Try

                Dim strSql As String = "select KodPrefix from MK_KPPrefix where KodPrefix = '" & strkodPfx & "' and KodKat = '" & strKodKat & "'"
                Dim ds As New DataSet
                Dim dbconn As New DBKewConn

                ds = dbconn.fSelectCommand(strSql)

                If Not ds Is Nothing Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If

            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try

    End Function

    Private Sub gvPfx_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvPfx.RowCommand
        Try
            If e.CommandName = "Select" Then
                ViewState("modePfx") = 2
                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvPfx.Rows(index)

                Dim strKodPrefix As String = selectedRow.Cells(1).Text
                Dim strButiranPfx As String = selectedRow.Cells(2).Text
                Dim strKodKat As String = selectedRow.Cells(3).Text
                Dim strStatus As String = selectedRow.Cells(4).Text

                Dim intStatus As Integer
                If strStatus = "Aktif" Then
                    intStatus = 1
                ElseIf strStatus = "Tidak Aktif" Then
                    intStatus = 0
                End If

                txtPrefix.Text = strKodPrefix
                txtButiranPfx.Text = Server.HtmlDecode(strButiranPfx)
                rbStatusPfx.SelectedValue = intStatus
                ddlKatPrefix.SelectedValue = strKodKat
                ddlKatPrefix.Enabled = False
                txtPrefix.Enabled = False
                lbtnHapusPfx.Visible = True
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.Click

        Dim strSql As String
        Dim dbconn As New DBKewConn
        Dim strkodKP As String
        Dim strPrefix As String = Trim(ddlPrefix.SelectedValue.TrimEnd)
        Dim strButiran As String = Trim(txtButiran.Text.TrimEnd)
        Dim intStatus As Integer = rbStatus.SelectedValue
        Dim strKodKat As String = Trim(ddlKatPrefix2.SelectedValue.TrimEnd)
        Try

            If ViewState("mode") = 1 Then  'New   
                If ddlPrefix.SelectedValue <> "00" Then
                    strkodKP = fGetKodKP(strPrefix)
                Else
                    strkodKP = Trim(ddlPrefix.SelectedValue.TrimEnd) & Trim(txtKodKP.Text.TrimEnd)
                End If

                strSql = "select count (*) from MK_KodProjek where KodProjek = '" & strkodKP & "'"

                If dbconn.fSelectCount(strSql) > 0 Then
                    fGlobalAlert("Rekod telah wujud!", Me.Page, Me.[GetType]())
                    Exit Sub
                End If

                strSql = "insert into MK_KodProjek (KodProjek, Butiran, KodKat, Status) values (@KodProjek, @Butiran, @KodKat, @Status)"

                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@KodProjek", strkodKP),
                    New SqlParameter("@Butiran", strButiran),
                    New SqlParameter("@KodKat", strKodKat),
                    New SqlParameter("@Status", 1)
                }

                dbconn.sConnBeginTrans()
                If dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                    dbconn.sConnCommitTrans()
                    fGlobalAlert("Rekod telah disimpan!", Me.Page, Me.[GetType]())
                    fReset()
                    fBindGv()
                Else
                    dbconn.sConnRollbackTrans()
                    fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
                End If
            ElseIf ViewState("mode") = 2 Then  'Edit
                Dim intID As Integer = CInt(Trim(hidIdKP.Value.TrimEnd))
                strPrefix = Trim(ddlPrefix.SelectedValue.TrimEnd)
                strkodKP = strPrefix & Trim(txtKodKP.Text.TrimEnd)
                strSql = "update MK_KodProjek set Butiran = @Butiran, Status = @Status where ID = @ID"
                Dim paramSql2() As SqlParameter = {
                    New SqlParameter("@Butiran", strButiran),
                    New SqlParameter("@Status", intStatus),
                    New SqlParameter("@ID", intID)
                    }

                dbconn.sConnBeginTrans()
                If dbconn.fUpdateCommand(strSql, paramSql2) > 0 Then
                    dbconn.sConnCommitTrans()
                    fGlobalAlert("Rekod telah disimpan!", Me.Page, Me.[GetType]())
                    fReset()
                    fBindGv()
                Else
                    dbconn.sConnRollbackTrans()
                    fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
                End If

            End If

        Catch ex As Exception

        End Try




    End Sub

    Private Function fSimpan() As Boolean
        Dim blnSuccess As Boolean
        Try
            Dim strSql As String
            Dim strkodKP As String
            Dim dbconn As New DBKewConn

            Dim strPrefix As String = Trim(ddlPrefix.SelectedValue.TrimEnd)
            Dim strButiran As String = Trim(txtButiran.Text.TrimEnd)
            Dim intStatus As Integer = rbStatus.SelectedValue

            If ViewState("mode") = 1 Then  'New   
                If ddlPrefix.SelectedValue <> "00" Then
                    strkodKP = fGetKodKP(strPrefix)
                Else
                    strkodKP = Trim(ddlPrefix.SelectedValue.TrimEnd) & Trim(txtKodKP.Text.TrimEnd)
                End If

                strSql = "insert into MK_KodProjek (KodProjek, Butiran, Status ) values (@KodProjek, @Butiran, @Status)"

                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@KodProjek", strkodKP),
                    New SqlParameter("@Butiran", strButiran),
                    New SqlParameter("@Status", 1)
                }

                dbconn.sConnBeginTrans()
                If dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                    dbconn.sConnCommitTrans()
                    blnSuccess = True
                Else
                    dbconn.sConnRollbackTrans()
                    blnSuccess = False
                End If
            ElseIf ViewState("mode") = 2 Then  'Edit
                Dim intID As Integer = CInt(Trim(hidIdKP.Value.TrimEnd))
                strPrefix = Trim(ddlPrefix.SelectedValue.TrimEnd)
                strkodKP = strPrefix & Trim(txtKodKP.Text.TrimEnd)
                strSql = "update MK_KodProjek set Butiran = @Butiran, Status = @Status where ID = @ID"
                Dim paramSql2() As SqlParameter = {
                    New SqlParameter("@Butiran", strButiran),
                    New SqlParameter("@Status", intStatus),
                    New SqlParameter("@ID", intID)
                    }

                dbconn.sConnBeginTrans()
                If dbconn.fUpdateCommand(strSql, paramSql2) > 0 Then
                    dbconn.sConnCommitTrans()
                    blnSuccess = True
                Else
                    dbconn.sConnRollbackTrans()
                    blnSuccess = False
                End If

            End If

        Catch ex As Exception
            blnSuccess = False
        End Try

        Return blnSuccess

    End Function
    Private Sub lbtnBaru_Click(sender As Object, e As EventArgs) Handles lbtnBaru.Click
        fReset()
        ddlPrefix.Enabled = True
    End Sub

    Private Sub lbtnBaruPfx_Click(sender As Object, e As EventArgs) Handles lbtnBaruPfx.Click
        fResetPfx()
    End Sub

    Private Sub lbtnSimpanPfx_Click(sender As Object, e As EventArgs) Handles lbtnSimpanPfx.Click

        'If txtPrefix.Text.Length < 2 Then
        '    clsAlert.Alert(lblMsg, "Ruang 'Prefix' mesti dimasukkan 2 karakter!", clsAlert.AlertType.Warning, False)
        '    Exit Sub
        'End If
        If Page.IsValid() Then
            Dim strRes As String = fSimpanPfx()

            If strRes = "1" Then
                fResetPfx()
                fBindGvPfx()
                fGlobalAlert("Rekod telah disimpan!", Me.Page, Me.[GetType]())
            ElseIf strRes = "2" Then
                fGlobalAlert("Kod Prefix ini sudah wujud! Sila masukkan Kod Prefix yang lain.", Me.Page, Me.[GetType]())
            ElseIf strRes = "3" Then
                fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
            End If
        Else
            fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
        End If
    End Sub

    Private Function fSimpanPfx() As String
        Try
            Dim strSql As String
            Dim dbconn As New DBKewConn

            Dim strKodPfx As String = Trim(txtPrefix.Text.TrimEnd.ToUpper)
            Dim strButiran As String = Trim(txtButiranPfx.Text.TrimEnd.ToUpper)
            Dim intStatus As Integer = rbStatusPfx.SelectedValue
            Dim strKodKat As String = Trim(ddlKatPrefix.SelectedValue.TrimEnd)
            If ViewState("modePfx") = 1 Then  'New   

                strSql = "select COUNT(*) from MK_KPPrefix where KodPrefix = '" & strKodPfx & "' and KodKat = '" & strKodKat & "'"

                If dbconn.fSelectCount(strSql) > 0 Then
                    Return "2"
                End If

                strSql = "insert into MK_KPPrefix (KodPrefix , Butiran, KodKat, Status ) values (@KodPrefix, @Butiran, @KodKat, @Status)"

                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@KodPrefix", strKodPfx),
                    New SqlParameter("@Butiran", strButiran),
                    New SqlParameter("@KodKat", strKodKat),
                    New SqlParameter("@Status", intStatus)
                }

                dbconn.sConnBeginTrans()
                If dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                    dbconn.sConnCommitTrans()
                    Return "1"
                Else
                    dbconn.sConnRollbackTrans()
                    Return "3"
                End If
            ElseIf ViewState("modePfx") = 2 Then  'Edit

                strSql = "update MK_KPPrefix set Butiran = @Butiran, status = @status where KodPrefix = @kodPrefix and KodKat = @KodKat"
                Dim paramSql2() As SqlParameter = {
                    New SqlParameter("@Butiran", strButiran),
                    New SqlParameter("@status", intStatus),
                    New SqlParameter("@KodKat", strKodKat),
                    New SqlParameter("@kodPrefix", strKodPfx)
                    }

                dbconn.sConnBeginTrans()
                If dbconn.fUpdateCommand(strSql, paramSql2) > 0 Then
                    dbconn.sConnCommitTrans()
                    Return "1"
                Else
                    dbconn.sConnRollbackTrans()
                    Return "3"
                End If
            End If

        Catch ex As Exception
            Return "3"
        End Try

    End Function

    Private Sub gvPfx_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvPfx.PageIndexChanging
        Try
            gvPfx.PageIndex = e.NewPageIndex
            If ViewState("dtPfx") IsNot Nothing Then
                gvPfx.DataSource = ViewState("dtPfx")
                gvPfx.DataBind()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lbtnHapusPfx_Click(sender As Object, e As EventArgs) Handles lbtnHapusPfx.Click
        Try

            Dim dbconn As New DBKewConn

            Dim strPfx As String = Trim(txtPrefix.Text.TrimEnd)
            Dim strKodKat As String = Trim(ddlKatPrefix.SelectedValue.TrimEnd)

            Dim strSql As String = "delete from MK_KPPrefix where KodPrefix = @Pfx and KodKat = @KodKat"
            Dim paramSql() As SqlParameter = {
                    New SqlParameter("@Pfx", strPfx),
                    New SqlParameter("@KodKat", strKodKat)
                            }

            dbconn.sConnBeginTrans()
            If dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                dbconn.sConnCommitTrans()
                fGlobalAlert("Rekod telah dihapuskan!", Me.Page, Me.[GetType]())
                fBindGvPfx()
                fReset()
            Else
                dbconn.sConnRollbackTrans()
                fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvProjek_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvProjek.PageIndexChanging
        Try
            gvProjek.PageIndex = e.NewPageIndex
            If ViewState("dtKP") IsNot Nothing Then
                gvProjek.DataSource = ViewState("dtKP")
                gvProjek.DataBind()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lbtnHapus_Click(sender As Object, e As EventArgs) Handles lbtnHapus.Click
        Try
            Dim dbconn As New DBKewConn

            Dim strKP As String = Trim(ddlPrefix.SelectedValue.TrimEnd) & Trim(txtKodKP.Text.TrimEnd)

            Dim strSql As String = "delete from MK_KodProjek where KodProjek = @KP and Status = 1"
            Dim paramSql() As SqlParameter = {
                    New SqlParameter("@KP", strKP)
                            }

            dbconn.sConnBeginTrans()
            If dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                dbconn.sConnCommitTrans()
                fGlobalAlert("Rekod telah dihapuskan!", Me.Page, Me.[GetType]())
                fBindGv()
                fReset()
            Else
                dbconn.sConnRollbackTrans()
                fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub TabContainer1_ActiveTabChanged(sender As Object, e As EventArgs) Handles TabContainer1.ActiveTabChanged
        Select Case TabContainer1.ActiveTabIndex
            Case 1
                fReset()

                fBindGv()
        End Select
    End Sub

    Private Sub ddlKatPrefix2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKatPrefix2.SelectedIndexChanged
        fBindDdlPrefix(Trim(ddlKatPrefix2.SelectedValue.TrimEnd))
    End Sub

    Private Sub lbtnCariKP_Click(sender As Object, e As EventArgs) Handles lbtnCariKP.Click

        Dim dt, dt2 As New DataTable
        dt = CType(ViewState("dtKP"), DataTable)

        If ddlCarian.SelectedValue = 0 Then
            gvProjek.DataSource = dt
        Else
            dt2 = dt.Clone
            Dim strKodKP As String = Trim(txtCarianKP.Text.TrimEnd)
            Dim foundRows As DataRow()
            foundRows = dt.Select("KodKP='" & strKodKP & "'")

            For i As Integer = 0 To foundRows.Length - 1
                Dim strId As String = foundRows(i)(0)
                Dim strKP As String = foundRows(i)(1)
                Dim strButiran As String = foundRows(i)(2)
                Dim strKodKat As String = foundRows(i)(3)
                Dim strstatus As String = foundRows(i)(4)

                dt2.Rows.Add(strId, strKP, strButiran, strKodKat, strstatus)
            Next
            gvProjek.DataSource = dt2
        End If

        gvProjek.DataBind()

    End Sub

    Protected Sub ddlCarian_SelectedIndexChanged(sender As Object, e As EventArgs)
        If ddlCarian.SelectedValue = 0 Then
            txtCarianKP.Visible = False
        Else
            txtCarianKP.Visible = True
        End If
        txtCarianKP.Text = ""
    End Sub

    Private Sub ddlCariPfx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCariPfx.SelectedIndexChanged
        If ddlCariPfx.SelectedValue = 0 Then
            txtCariPfx.Visible = False
        Else
            txtCariPfx.Visible = True
        End If
        txtCariPfx.Text = ""
    End Sub

    Private Sub lBtnCariPfx_Click(sender As Object, e As EventArgs) Handles lBtnCariPfx.Click
        Try
            Dim dt, dt2 As New DataTable
            dt = CType(ViewState("dtPfx"), DataTable)

            If ddlCariPfx.SelectedValue = 0 Then
                gvPfx.DataSource = dt
            Else
                dt2 = dt.Clone
                Dim strKodPrefix As String = Trim(txtCariPfx.Text.TrimEnd)
                Dim foundRows As DataRow()
                foundRows = dt.Select("KodPrefix='" & strKodPrefix & "'")

                For i As Integer = 0 To foundRows.Length - 1
                    Dim strPfx As String = foundRows(i)(0)
                    Dim strButiran As String = foundRows(i)(1)
                    Dim strKodKat As String = foundRows(i)(2)
                    Dim strstatus As String = foundRows(i)(3)

                    dt2.Rows.Add(strPfx, strButiran, strKodKat, strstatus)
                Next
                gvPfx.DataSource = dt2
            End If

            gvPfx.DataBind()
        Catch ex As Exception

        End Try
    End Sub
End Class