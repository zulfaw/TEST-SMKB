Imports System.Data.SqlClient
Imports System.IO
Imports System.Data
Imports System.Drawing

Public Class Pelepasan_Staf
    Inherits System.Web.UI.Page
    Dim dbconn As New DBKewConn
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'If Session("LoggedIn") <> "" Then
            ViewState("SaveMode") = 1
            fBindDdlNamaStaf()

            fBindGv()
            fBindGvList()
            'fBindGvJenisPjk()
            'Else
            '    Response.Redirect("../../Logout.aspx")
            'End If

        End If
    End Sub
    Private Sub fBindgvList()
        Using dt = fCreateGvList()

            gvList.DataSource = dt
            gvList.DataBind()

            'lblJumMod.InnerText = dt.Rows.Count
        End Using
    End Sub
    Private Sub fBindDdlNamaStaf()
        Try
            Dim strSql As String = "SELECT a.MS01_NoStaf, a.MS01_Nama" &
                                    " FROM MS01_Peribadi AS a" &
                                    " Where 1 = 1" &
                                    " AND a.MS01_NoStaf in (select ms01_nostaf from MS08_Penempatan where ms08_staterkini = 1)" &
                                    " ORDER BY a.MS01_Nama"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)

            ddlNamaStaf.DataSource = ds
            ddlNamaStaf.DataTextField = "MS01_Nama"
            ddlNamaStaf.DataValueField = "MS01_NoStaf"
            ddlNamaStaf.DataBind()

            ddlNamaStaf.Items.Insert(0, New ListItem("- SILA PILIH -", "0"))
            ddlNamaStaf.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindGv()

        Try
            Dim strSql As String = "SELECT a.MS01_NoStaf, a.MS01_Nama
FROM MS01_Peribadi AS a
Where 1 = 1
AND a.MS01_NoStaf in (select ms01_nostaf from MS08_Penempatan where ms08_staterkini = 1)
ORDER BY a.MS01_Nama"

            Dim ds As New DataSet
            Dim dbconn As New DBSMConn

            ds = dbconn.fSelectCommand(strSql)
            ddlNamaStaf.DataSource = ds
            ddlNamaStaf.DataTextField = "MS01_Nama"
            ddlNamaStaf.DataValueField = "MS01_NoStaf"
            ddlNamaStaf.DataBind()

        Catch ex As Exception

        End Try
        'Using dt = fCreateGvList()

        '    gvList.DataSource = dt
        '    gvList.DataBind()

        '    'lblJumMod.InnerText = dt.Rows.Count
        'End Using
    End Sub
    Private Sub gvList_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvList.PageIndexChanging

        gvList.PageIndex = e.NewPageIndex
        If Session("SortedView") IsNot Nothing Then
            gvList.DataSource = Session("SortedView")
            gvList.DataBind()
        Else

            Dim dt As New DataTable
            dt = fCreateGvList()
            gvList.DataSource = dt
            gvList.DataBind()
        End If

    End Sub
    Private Function fCreateGvList() As DataTable

        Dim strSql As String = "select a.id, a.NoStaf, a.PTjStaf, a.NoSurat, a.TkhDaftar, b.ms01_nama, d.Pejabat
from PJM_PelepasanStaf a, [QA11].dbStaf.dbo.ms01_peribadi b, [QA11].dbStaf.dbo.MS08_Penempatan c, [QA11].dbStaf.dbo.MS_Pejabat d
 Where b.ms01_nostaf = a.nostaf
and c.ms01_nostaf = a.NoStaf
and d.kodpejabat = c.MS08_PejabatAsal
and c.ms08_staterkini = 1"

        Using dt = dbconn.fSelectCommandDt(strSql)
            Return dt
        End Using



    End Function

    Private Sub gvList_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvList.RowCommand
        Try
            If e.CommandName = "Select" Then
                ViewState("SaveMode") = 2
                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvList.Rows(index)

                Dim intId As Integer = gvList.DataKeys(index).Value
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
                ddlNamaStaf.SelectedValue = strPrefix
                txtButiran.Text = HttpUtility.HtmlDecode(strButiran)
                ddlNamaStaf.Enabled = False

            End If

        Catch ex As Exception

        End Try
    End Sub
    
    Private Sub fReset()
        Try
            ddlNamaStaf.SelectedIndex = 0
            txtButiran.Text = ""
            ddlNamaStaf.Enabled = True
            ViewState("SaveMode") = 1
        Catch ex As Exception

        End Try
    End Sub

    Private Sub fResetPfx()
        Try
            'txtPrefix.Text = ""
            'ddlKW.SelectedIndex = 0
            'txtPrefix.ReadOnly = False

            ViewState("SaveMode") = 1

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindGvPfx()
        Try
            'Dim intRec As Integer

            'gvPfx.DataSource = New List(Of String)
            'gvPfx.DataBind()

            'Dim strSql As String = "select a.KodPrefix, (select b.KodKw + ' - ' + b.Butiran  from MK_Kw b where b.KodKw = a.kodkw) as KW , Status from MK_KPPrefix a"
            'Dim dbConn As New DBKewConn
            'Dim ds As New DataSet
            'ds = dbConn.fSelectCommand(strSql)

            'If Not ds Is Nothing Then
            '    If ds.Tables(0).Rows.Count > 0 Then
            '        Dim dt As New DataTable
            '        dt.Columns.Add("KodPrefix", GetType(String))
            '        dt.Columns.Add("KW", GetType(String))
            '        dt.Columns.Add("Status", GetType(String))

            '        Dim strPfx, strKW, strStatus As String
            '        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            '            strPfx = ds.Tables(0).Rows(i)("KodPrefix")
            '            strKW = ds.Tables(0).Rows(i)("KW")
            '            strStatus = ds.Tables(0).Rows(i)("Status")
            '            If strStatus = "True" Then
            '                strStatus = "Aktif"
            '            Else
            '                strStatus = "Tidak Aktif"
            '            End If

            '            dt.Rows.Add(strPfx, strKW, strStatus)
            '        Next
            '        gvPfx.DataSource = dt
            '        gvPfx.DataBind()
            '    Else
            '        intRec = 0
            '    End If
            'Else
            '    intRec = 0
            'End If

            'intRec = ds.Tables(0).Rows.Count
            'lblJumRecPfx.InnerText = intRec

        Catch ex As Exception

        End Try
    End Sub



    'Private Function fJnsPjkExist(ByVal strkodPfx As String) As Boolean

    '    Try

    '        Dim strSql As String = "select KodPrefix from BG_Prefix where KodPrefix = '" & strkodPfx & "'"
    '        Dim ds As New DataSet
    '        Dim dbconn As New DBKewConn

    '        ds = dbconn.fSelectCommand(strSql)

    '        If Not ds Is Nothing Then
    '            If ds.Tables(0).Rows.Count > 0 Then
    '                Return True
    '            Else
    '                Return False
    '            End If
    '        Else
    '            Return False
    '        End If

    '    Catch ex As Exception

    '    End Try


    'End Function

    'Private Function fCheckExist(ByVal strkodPfx As String) As Boolean

    '    Try

    '        Dim strSql As String = "select KodPrefix from BG_Prefix where KodPrefix = '" & strkodPfx & "'"
    '        Dim ds As New DataSet
    '        Dim dbconn As New DBKewConn

    '        ds = dbconn.fSelectCommand(strSql)

    '        If Not ds Is Nothing Then
    '            If ds.Tables(0).Rows.Count > 0 Then
    '                Return True
    '            Else
    '                Return False
    '            End If
    '        Else
    '            Return False
    '        End If

    '    Catch ex As Exception

    '    End Try


    'End Function

    'Private Sub gvPfx_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvPfx.RowCommand
    '    Try
    '        If e.CommandName = "Select" Then
    '            ViewState("SaveMode") = 2
    '            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
    '            Dim selectedRow As GridViewRow = gvPfx.Rows(index)

    '            Dim strKodPrefix As String = selectedRow.Cells(1).Text
    '            Dim strKW As String = selectedRow.Cells(2).Text
    '            Dim strKodKW As String = strKW.Substring(0, 2)
    '            Dim strStatus As String = selectedRow.Cells(3).Text
    '            Dim intStatus As Integer
    '            If strStatus = "Aktif" Then
    '                intStatus = 1
    '            ElseIf strStatus = "Tidak Aktif" Then
    '                intStatus = 0
    '            End If

    '            txtPrefix.Text = strKodPrefix
    '            ddlKW.SelectedValue = strKodKW
    '            rbStatusPfx.SelectedValue = intStatus

    '            txtPrefix.ReadOnly = True

    '        End If

    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.Click
        Dim blnSuccess As Boolean
        Try
            'Dim strSql As String
            'Dim strkodKP As String
            Dim dbconn As New DBKewConn

            Dim strPrefix As String = Trim(ddlNamaStaf.SelectedValue.TrimEnd)
            Dim strButiran As String = Trim(txtButiran.Text.TrimEnd)
            ' Dim intStatus As Integer = rbStatus.SelectedValue

            If ViewState("SaveMode") = 1 Then  'New   
                'strSql = "insert into MK_KodProjek (KodProjek, Butiran, Status ) values (@KodProjek, @Butiran, @Status)"

                'Dim paramSql() As SqlParameter = {
                '    New SqlParameter("@KodProjek", strkodKP),
                '    New SqlParameter("@Butiran", strButiran),
                '    New SqlParameter("@Status", 1)
                '}

                'dbconn.sConnBeginTrans()
                'If dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                '    dbconn.sConnCommitTrans()
                '    blnSuccess = True
                'Else
                '    dbconn.sConnRollbackTrans()
                '    blnSuccess = False
                'End If
            ElseIf ViewState("SaveMode") = 2 Then  'Edit
                '                Dim intID As Integer = CInt(Trim(HidtxtId.Text.TrimEnd))
                'strPrefix = Trim(ddlNamaStaf.SelectedValue.TrimEnd)
                ''               strkodKP = strPrefix & Trim(txtKodKP.Text.TrimEnd)
                'strSql = "update MK_KodProjek set Butiran = @Butiran, Status = @Status where ID = @ID"
                'Dim paramSql2() As SqlParameter = {
                '    New SqlParameter("@Butiran", strButiran),
                '    New SqlParameter("@ID", intID)
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
            fGlobalAlert("Rekod telah dikemas kini!", Me.Page, Me.[GetType]())
            fReset()
            fBindGv()
        ElseIf blnSuccess = False Then
            fGlobalAlert("Rekod gagal dikemaskini!", Me.Page, Me.[GetType]())
        End If
    End Sub

    Private Sub lbtnBaru_Click(sender As Object, e As EventArgs) Handles lbtnBaru.Click
        fReset()
        ddlNamaStaf.Enabled = True
    End Sub



    'Private Sub lbtnSimpanPfx_Click(sender As Object, e As EventArgs) Handles lbtnSimpanPfx.Click
    '    Try
    '        Dim blnSuccess As Boolean
    '        Try
    '            Dim strSql As String
    '            Dim dbconn As New DBKewConn

    '            Dim strKodPfx As String = Trim(txtPrefix.Text.TrimEnd.ToUpper)
    '            Dim strKodKW As String = Trim(ddlKW.SelectedValue.TrimEnd)
    '            Dim intStatus As Integer = rbStatusPfx.SelectedValue

    '            If ViewState("SaveMode") = 1 Then  'New  

    '                If fCheckExist(strKodPfx) = True Then
    '                    fGlobalAlert("Kod Prefix ini sudah wujud! Sila masukkan Kod Prefix yang lain.", Me.Page, Me.[GetType]())
    '                Else
    '                    If fInsertPfx(strKodPfx, strKodKW, intStatus) Then
    '                        fResetPfx()
    '                        fBindGvPfx()
    '                        fGlobalAlert("Rekod telah ditambah!", Me.Page, Me.[GetType]())
    '                    Else
    '                        fGlobalAlert("Rekod gagal ditambah!", Me.Page, Me.[GetType]())
    '                    End If
    '                End If

    '            ElseIf ViewState("SaveMode") = 2 Then  'Edit

    '                'strSql = "update BG_Prefix set KodKW = @Butiran, status = @status where KodPrefix = @kodPrefix"
    '                'Dim paramSql2() As SqlParameter = {
    '                '    New SqlParameter("@Butiran", strButiran),
    '                '    New SqlParameter("@status", intStatus),
    '                '    New SqlParameter("@kodPrefix", strKodPfx)
    '                '    }

    '                'dbconn.sConnBeginTrans()
    '                'If dbconn.fUpdateCommand(strSql, paramSql2) > 0 Then
    '                '    dbconn.sConnCommitTrans()
    '                '    blnSuccess = True
    '                'Else
    '                '    dbconn.sConnRollbackTrans()
    '                '    blnSuccess = False
    '                'End If

    '            End If

    '        Catch ex As Exception
    '            blnSuccess = False
    '        End Try

    '        If blnSuccess = True Then

    '            fResetPfx()
    '            fBindGvPfx()
    '            fGlobalAlert("Rekod telah dikemas kini!", Me.Page, Me.[GetType]())
    '        ElseIf blnSuccess = False Then
    '            fGlobalAlert("Rekod gagal dikemaskini!", Me.Page, Me.[GetType]())
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

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

End Class