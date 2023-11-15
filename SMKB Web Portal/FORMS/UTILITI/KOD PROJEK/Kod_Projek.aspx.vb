Imports System.Data.SqlClient
Imports System.IO

Public Class Kod_Projek
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'If Session("LoggedIn") <> "" Then
            ViewState("SaveMode") = 1

            TabContainer1.ActiveTabIndex = 0
            fBindDdlPrefix()


            fBindGv()
            'fBindGvPfx()
            'fBindGvJenisPjk()
            'Else
            '    Response.Redirect("../../Logout.aspx")
            'End If

        End If
    End Sub

    Private Sub fBindDdlPrefix()
        Try
            Dim strSql As String = "select KodPrefix from MK_KPPrefix where status = 1 order by KodPrefix"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fselectCommand(strSql)

            ddlPrefix.DataSource = ds
            ddlPrefix.DataTextField = "KodPrefix"
            ddlPrefix.DataValueField = "KodPrefix"
            ddlPrefix.DataBind()

            ddlPrefix.Items.Insert(0, New ListItem("- SILA PILIH -", "0"))
            ddlPrefix.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindDdlKW()
        Try
            Dim strSql As String = "select kodkw,(KodKw + '-' + Butiran ) as Butiran from MK_Kw where status = 1"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fselectCommand(strSql)

            ddlKW.DataSource = ds
            ddlKW.DataTextField = "Butiran"
            ddlKW.DataValueField = "kodkw"
            ddlKW.DataBind()

            ddlKW.Items.Insert(0, New ListItem("- SILA PILIH -", "0"))
            ddlKW.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub
    Private Sub fBindGv()
        Try
            Dim intRec As Integer


            gvProjek.DataSource = New List(Of String)
            gvProjek.DataBind()

            Dim strSql As String = "select ID, KodProjek, Butiran, Status  from MK_KodProjek"
            Dim dbConn As New DBKewConn
            Dim ds As New DataSet
            ds = dbConn.fselectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim dt As New DataTable
                    dt.Columns.Add("ID", GetType(String))
                    dt.Columns.Add("KodKP", GetType(String))
                    dt.Columns.Add("Butiran", GetType(String))
                    dt.Columns.Add("Status", GetType(String))

                    Dim strKP, strButiran, strId, strstatus As String
                    Dim intStatus As String

                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        strId = ds.Tables(0).Rows(i)("ID")
                        strKP = ds.Tables(0).Rows(i)("KodProjek")
                        strButiran = ds.Tables(0).Rows(i)("Butiran")
                        strstatus = ds.Tables(0).Rows(i)("Status")
                        If strstatus = False Then
                            strstatus = "Tidak Aktif"
                        ElseIf strstatus = True Then
                            strstatus = "Aktif"
                        End If
                        dt.Rows.Add(strId, strKP, strButiran, strstatus)
                    Next
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
                ViewState("SaveMode") = 2
                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvProjek.Rows(index)

                Dim intId As Integer = gvProjek.DataKeys(index).Value
                Dim strKodKP As String = selectedRow.Cells(1).Text
                Dim strButiran As String = selectedRow.Cells(2).Text
                Dim strStatus As String = selectedRow.Cells(3).Text
                Dim intStatus As Integer
                If strStatus = "Aktif" Then
                    intStatus = 1
                ElseIf strStatus = "Tidak Aktif" Then
                    intStatus = 0
                End If

                Dim strPrefix As String = strKodKP.Substring(0, 2)
                Dim strKod As String = strKodKP.Substring(strKodKP.Length - 5)
                HidtxtId.Text = intId
                ddlPrefix.SelectedValue = strPrefix
                txtKodKP.Text = strKod
                txtButiran.Text = HttpUtility.HtmlDecode(strButiran)
                rbStatus.SelectedValue = intStatus
                ddlPrefix.Enabled = False

            End If

        Catch ex As Exception

        End Try
    End Sub





    Private Sub fReset()
        Try
            ddlPrefix.SelectedIndex = 0
            txtKodKP.Text = ""
            txtButiran.Text = ""
            ddlPrefix.Enabled = True
            ViewState("SaveMode") = 1
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
            ds = dbconn.fselectCommand(strSql)

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
        End If
    End Sub



    Private Sub fResetPfx()
        Try
            txtPrefix.Text = ""
            ddlKW.SelectedIndex = 0
            txtPrefix.ReadOnly = False

            ViewState("SaveMode") = 1

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindGvPfx()
        Try
            Dim intRec As Integer

            gvPfx.DataSource = New List(Of String)
            gvPfx.DataBind()

            Dim strSql As String = "select a.KodPrefix, (select b.KodKw + ' - ' + b.Butiran  from MK_Kw b where b.KodKw = a.kodkw) as KW , Status from MK_KPPrefix a"
            Dim dbConn As New DBKewConn
            Dim ds As New DataSet
            ds = dbConn.fselectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim dt As New DataTable
                    dt.Columns.Add("KodPrefix", GetType(String))
                    dt.Columns.Add("KW", GetType(String))
                    dt.Columns.Add("Status", GetType(String))

                    Dim strPfx, strKW, strStatus As String
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        strPfx = ds.Tables(0).Rows(i)("KodPrefix")
                        strKW = ds.Tables(0).Rows(i)("KW")
                        strStatus = ds.Tables(0).Rows(i)("Status")
                        If strStatus = "True" Then
                            strStatus = "Aktif"
                        Else
                            strStatus = "Tidak Aktif"
                        End If

                        dt.Rows.Add(strPfx, strKW, strStatus)
                    Next
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



    Private Function fJnsPjkExist(ByVal strkodPfx As String) As Boolean

        Try

            Dim strSql As String = "select KodPrefix from BG_Prefix where KodPrefix = '" & strkodPfx & "'"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn

            ds = dbconn.fselectCommand(strSql)

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


    End Function

    Private Function fCheckExist(ByVal strkodPfx As String) As Boolean

        Try

            Dim strSql As String = "select KodPrefix from BG_Prefix where KodPrefix = '" & strkodPfx & "'"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn

            ds = dbconn.fselectCommand(strSql)

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


    End Function

    Private Sub gvPfx_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvPfx.RowCommand
        Try
            If e.CommandName = "Select" Then
                ViewState("SaveMode") = 2
                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvPfx.Rows(index)

                Dim strKodPrefix As String = selectedRow.Cells(1).Text
                Dim strKW As String = selectedRow.Cells(2).Text
                Dim strKodKW As String = strKW.Substring(0, 2)
                Dim strStatus As String = selectedRow.Cells(3).Text
                Dim intStatus As Integer
                If strStatus = "Aktif" Then
                    intStatus = 1
                ElseIf strStatus = "Tidak Aktif" Then
                    intStatus = 0
                End If

                txtPrefix.Text = strKodPrefix
                ddlKW.SelectedValue = strKodKW
                rbStatusPfx.SelectedValue = intStatus

                txtPrefix.ReadOnly = True

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.Click
        Dim blnSuccess As Boolean
        Try
            Dim strSql As String
            Dim strkodKP As String
            Dim dbconn As New DBKewConn

            Dim strPrefix As String = Trim(ddlPrefix.SelectedValue.TrimEnd)
            Dim strButiran As String = Trim(txtButiran.Text.TrimEnd)
            Dim intStatus As Integer = rbStatus.SelectedValue

            If ViewState("SaveMode") = 1 Then  'New   
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
            ElseIf ViewState("SaveMode") = 2 Then  'Edit
                Dim intID As Integer = CInt(Trim(HidtxtId.Text.TrimEnd))
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

        If blnSuccess = True Then
            fGlobalAlert("Rekod telah dikemas kini!", Me.Page, Me.[GetType]())
            fReset()
            fBindGv()
        ElseIf blnSuccess = False Then
            fGlobalAlert("Rekod gagal dikemaskini!", Me.Page, Me.[GetType]())
        End If
    End Sub

    Private Sub lbtnBaru_Click(sender As Object, e As EventArgs) Handles lbtnBaru.Click
        fReset()
        ddlPrefix.Enabled = True
    End Sub

    Private Sub lbtnBaruPfx_Click(sender As Object, e As EventArgs) Handles lbtnBaruPfx.Click
        fResetPfx()
    End Sub

    Private Sub lbtnSimpanPfx_Click(sender As Object, e As EventArgs) Handles lbtnSimpanPfx.Click
        Try
            Dim blnSuccess As Boolean
            Try
                'Dim strSql As String
                Dim dbconn As New DBKewConn

                Dim strKodPfx As String = Trim(txtPrefix.Text.TrimEnd.ToUpper)
                Dim strKodKW As String = Trim(ddlKW.SelectedValue.TrimEnd)
                Dim intStatus As Integer = rbStatusPfx.SelectedValue

                If ViewState("SaveMode") = 1 Then  'New  

                    If fCheckExist(strKodPfx) = True Then
                        fGlobalAlert("Kod Prefix ini sudah wujud! Sila masukkan Kod Prefix yang lain.", Me.Page, Me.[GetType]())
                    Else
                        If fInsertPfx(strKodPfx, strKodKW, intStatus) Then
                            fResetPfx()
                            fBindGvPfx()
                            fGlobalAlert("Rekod telah ditambah!", Me.Page, Me.[GetType]())
                        Else
                            fGlobalAlert("Rekod gagal ditambah!", Me.Page, Me.[GetType]())
                        End If
                    End If

                ElseIf ViewState("SaveMode") = 2 Then  'Edit

                    'strSql = "update BG_Prefix set KodKW = @Butiran, status = @status where KodPrefix = @kodPrefix"
                    'Dim paramSql2() As SqlParameter = {
                    '    New SqlParameter("@Butiran", strButiran),
                    '    New SqlParameter("@status", intStatus),
                    '    New SqlParameter("@kodPrefix", strKodPfx)
                    '    }

                    'dbconn.sConnBeginTrans()
                    'If dbconn.fUpdateCommand(strSql, paramSql2) > 0 Then
                    '    dbconn.sConnCommitTrans()
                    '    blnSuccess = True
                    'Else
                    '    dbconn.sConnRollbackTrans()
                    '    blnSuccess = False
                    'End If

                End If

            Catch ex As Exception
                blnSuccess = False
            End Try

            If blnSuccess = True Then

                fResetPfx()
                fBindGvPfx()
                fGlobalAlert("Rekod telah dikemas kini!", Me.Page, Me.[GetType]())
            ElseIf blnSuccess = False Then
                fGlobalAlert("Rekod gagal dikemaskini!", Me.Page, Me.[GetType]())
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function fInsertPfx(strKodPfx, strKodKW, intStatus) As Boolean
        Dim strSql As String
        Dim dbconn As New DBKewConn
        Try


            strSql = "insert into MK_KPPrefix (KodPrefix , KodKW, Status ) values (@KodPrefix, @KodKW, @Status)"

            Dim paramSql() As SqlParameter = {
                New SqlParameter("@KodPrefix", strKodPfx),
                New SqlParameter("@KodKW", strKodKW),
                New SqlParameter("@Status", intStatus)
            }

            dbconn.sConnBeginTrans()
            If dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                dbconn.sConnCommitTrans()
                Return True
            Else
                dbconn.sConnRollbackTrans()
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Sub TabContainer1_ActiveTabChanged(sender As Object, e As EventArgs) Handles TabContainer1.ActiveTabChanged

        Select Case TabContainer1.ActiveTabIndex
            Case 0
                fReset()
                fBindGv()
                fBindDdlPrefix()
            Case 1
                fResetPfx()
                fBindGvPfx()
                fBindDdlKW()
        End Select

        ViewState("SaveMode") = 1

    End Sub


End Class