Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Public Class Kod_Kawalan_Pinjaman
    Inherits System.Web.UI.Page

    Dim dbconn As New DBKewConn

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                fBindgvKatPinj()
                fResetKatPinj()
                fBindgvJenPinj()
                fResetJenPinj()
                fBindGvKdrTempoh()



                TabContainer1.ActiveTab = tabModul
            End If

        Catch ex As Exception
            'fErrorLog("Pendaftaran_Menu.aspx(Page_Load())- " & ex.Message.ToString)
        End Try
    End Sub
    Private Sub fBindgvKatPinj()
        Using dt = fCreateKatPinj()

            'Dim strurutan As String = dt.Rows(dt.Rows.Count - 1)("Kod")
            'Dim intUrutan As String = CInt(strurutan)
            'intUrutan = intUrutan + 1
            'txtUrutanModul.Text = intUrutan

            gvKatPinj.DataSource = dt
            gvKatPinj.DataBind()

            lblJumMod.InnerText = dt.Rows.Count
        End Using
    End Sub

    Private Sub fBindgvJenPinj()
        Using dt = fCreateJenPinj()

            gvJenPinj.DataSource = dt
            gvJenPinj.DataBind()

            lblJumSubMod.InnerText = dt.Rows.Count
        End Using

    End Sub
    Private Sub fBindgvSkimPinj()
        Using dt = fCreateSkimPinj()

            gvSkimPinj.DataSource = dt
            gvSkimPinj.DataBind()

            lblJumSubMod.InnerText = dt.Rows.Count
        End Using

    End Sub

    Private Sub fBindGvKdrTempoh()


        Using dt = fCreateDtKdrTempoh()

            gvKdrTempoh.DataSource = dt
            gvKdrTempoh.DataBind()

            lblJumKdrTempoh.InnerText = dt.Rows.Count
        End Using

    End Sub


    Private Function fCreateKatPinj() As DataTable

        Dim strSql As String = "select Kod, Butiran from PJM_KatPinj order by Kod"

        Using dt = dbconn.fSelectCommandDt(strSql)
            Return dt
        End Using
    End Function


    Private Function fCreateDtKdrTempoh() As DataTable
        Dim strSql As String = "select kod, Butiran from PJM_KadarTempoh  ORDER BY Kod"

        Using dt = dbconn.fSelectCommandDt(strSql)
            Return dt
        End Using

    End Function
    Private Function fCreateJenPinj() As DataTable

        Dim strSql As String = "select Kod, Butiran from PJM_JenisPinj order by Kod"

        Using dt = dbconn.fSelectCommandDt(strSql)
            Return dt
        End Using
    End Function
    Private Function fCreateSkimPinj() As DataTable

        Dim strSql As String = "select Kod, Butiran from PJM_Skim order by Kod"

        Using dt = dbconn.fSelectCommandDt(strSql)
            Return dt
        End Using
    End Function
    'Protected Sub OnRowDataBoundgvKatPinj(sender As Object, e As GridViewRowEventArgs) Handles gvKatPinj.RowDataBound

    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvKatPinj, "Select$" & e.Row.RowIndex)
    '        e.Row.ToolTip = "Klik untuk pilih rekod ini."
    '        e.Row.ForeColor = ColorTranslator.FromHtml("#000000")
    '    End If

    'End Sub

    'Protected Sub OnRowDataBoundgvJenPinj(sender As Object, e As GridViewRowEventArgs) Handles gvJenPinj.RowDataBound

    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvJenPinj, "Select$" & e.Row.RowIndex)
    '        e.Row.ToolTip = "Klik untuk pilih rekod ini."
    '        e.Row.ForeColor = ColorTranslator.FromHtml("#000000")
    '    End If

    'End Sub

    Protected Sub OnRowDataBoundGvKdrTempoh(sender As Object, e As GridViewRowEventArgs) Handles gvKdrTempoh.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvKdrTempoh, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Klik untuk pilih rekod ini."
            e.Row.ForeColor = ColorTranslator.FromHtml("#000000")
        End If

    End Sub


    Private Function fUpdateJenPinj() As Boolean

        Dim blnSuccess As Boolean = True
        Dim strSql As String
        dbconn = New DBKewConn
        Try
            Dim strKodJenPinj As String = Trim(txtKodJenPinj.Text.TrimEnd)
            Dim strButirJenPinj As String = Trim(txtButirJenPinj.Text.TrimEnd)

            strSql = "update PJM_JenisPinj set butiran = @butirJenPinj where kod = @kodJenPinj"
            Dim paramSql() As SqlParameter = {
                        New SqlParameter("@butirJenPinj", strButirJenPinj),
                        New SqlParameter("@kodJenPinj", strKodJenPinj)
                        }

            If Not dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                blnSuccess = False
                Exit Try
            End If

        Catch ex As Exception
            blnSuccess = False
        End Try

        If blnSuccess = True Then
            dbconn.sConnCommitTrans()
            Return True
        Else
            dbconn.sConnRollbackTrans()
            Return False
        End If

    End Function

    Private Function fUpdateSkimPinj() As Boolean

        Dim blnSuccess As Boolean = True
        Dim strSql As String
        dbconn = New DBKewConn
        Try
            'Dim strKodSkimPinj As String = Trim(txtKodSkimPinj.Text.TrimEnd)
            'Dim strButirSkimPinj As String = Trim(txtButirSkimPinj.Text.TrimEnd)

            'strSql = "update PJM_Skim set butiran = @butirSkimPinj where kod = @kodSkimPinj"
            'Dim paramSql() As SqlParameter = {
            '            New SqlParameter("@butirSkimPinj", strButirSkimPinj),
            '            New SqlParameter("@kodSkimPinj", strKodSkimPinj)
            '            }

            'If Not dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
            '    blnSuccess = False
            '    Exit Try
            'End If

        Catch ex As Exception
            blnSuccess = False
        End Try

        If blnSuccess = True Then
            dbconn.sConnCommitTrans()
            Return True
        Else
            dbconn.sConnRollbackTrans()
            Return False
        End If

    End Function

    Private Function fInsertKatPinj() As Boolean
        Try
            Dim strSql As String
            Dim strKodKat As String = Trim(txtKodKat.Text.TrimEnd)
            Dim strButiranKat As String = Trim(txtButiranKat.Text.ToUpper.TrimEnd)
            '            Dim strNamaPaparan As String = Trim(txtNPModul.Text.ToUpper.TrimEnd)
            'Dim strUrutan As String = Trim(txtUrutanModul.Text.TrimEnd)
            'Dim intStatus As Integer = rbStatModul.SelectedValue

            strSql = "insert into PJM_KatPinj (Kod, Butiran) values (@KodKat,@ButiranKat)"

            Dim paramSql() As SqlParameter = {
                    New SqlParameter("@KodKat", strKodKat),
                    New SqlParameter("@ButiranKat", strButiranKat)
                }

            dbconn = New DBKewConn
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

    Private Function fInsertJenPinj() As Boolean
        dbconn = New DBKewConn
        Dim strSql As String

        Try
            Dim strKodJenPinj As String = Trim(txtKodJenPinj.Text.TrimEnd)
            Dim strButirJenPinj As String = Trim(txtButirJenPinj.Text.ToUpper.TrimEnd)

            dbconn.sConnBeginTrans()

            strSql = "insert into PJM_JenisPinj (Kod , Butiran) " &
            "values (@KodJenPinj,@ButirJenPinj)"
            Dim paramSql() As SqlParameter = {
                           New SqlParameter("@KodJenPinj", strKodJenPinj),
                           New SqlParameter("@ButirJenPinj", strButirJenPinj)
                            }

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

    Private Function fInsertSkimPinj() As Boolean
        dbconn = New DBKewConn
        Dim strSql As String

        Try
            Dim strKodSkimPinj As String = Trim(txtKodSkimPinj.Text.TrimEnd)
            Dim strButirSkimPinj As String = Trim(txtButirSkimPinj.Text.ToUpper.TrimEnd)

            dbconn.sConnBeginTrans()

            strSql = "insert into PJM_Skim (Kod , Butiran) " &
            "values (@KodSkimPinj,@ButirSkimPinj)"
            Dim paramSql() As SqlParameter = {
                           New SqlParameter("@KodSkimPinj", strKodSkimPinj),
                           New SqlParameter("@ButirSkimPinj", strButirSkimPinj)
                            }

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

    Private Sub gvKatPinj_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvKatPinj.SelectedIndexChanged
        For Each row As GridViewRow In gvKatPinj.Rows
            If row.RowIndex = gvKatPinj.SelectedIndex Then
                row.ForeColor = ColorTranslator.FromHtml("#0000FF")
                'row.ToolTip = String.Empty
            Else
                row.ForeColor = ColorTranslator.FromHtml("#000000")
                ' row.ToolTip = "Klik untuk pilih rekod ini."
            End If
        Next
    End Sub





    Private Sub gvKatPinj_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvKatPinj.PageIndexChanging

        gvKatPinj.PageIndex = e.NewPageIndex
        If Session("SortedView") IsNot Nothing Then
            gvKatPinj.DataSource = Session("SortedView")
            gvKatPinj.DataBind()
        Else

            Dim dt As New DataTable
            dt = fCreateKatPinj()
            gvKatPinj.DataSource = dt
            gvKatPinj.DataBind()
        End If

    End Sub

    Private Sub gvKatPinj_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvKatPinj.Sorting

        Dim sortingDirection As String = String.Empty
        If direction = SortDirection.Ascending Then
            direction = SortDirection.Descending
            sortingDirection = "Desc"
        Else
            direction = SortDirection.Ascending
            sortingDirection = "Asc"
        End If

        Dim sortedView As New DataView(fCreateKatPinj())
        sortedView.Sort = Convert.ToString(e.SortExpression + " ") & sortingDirection
        Session("SortedView") = sortedView
        gvKatPinj.DataSource = sortedView
        gvKatPinj.DataBind()

    End Sub


    Public Property direction() As SortDirection
        Get
            If ViewState("directionState") Is Nothing Then
                ViewState("directionState") = SortDirection.Ascending
            End If
            Return DirectCast(ViewState("directionState"), SortDirection)
        End Get
        Set
            ViewState("directionState") = Value
        End Set
    End Property

    Private Function fCheckKodKat(ByVal strKodKat As String) As Boolean
        Dim strSql = "select count(*) from MK_UModul  where KodKat = '" & strKodKat & "'"
        Dim intCnt As Integer = dbconn.fSelectCount(strSql)
        If intCnt < 1 Then
            Return False
        Else
            Return True
        End If

    End Function

    Private Sub Alert(msg As String)
        Dim sb As New StringBuilder()
        sb.Append("alert('")
        sb.Append(msg.Replace(vbLf, "\n").Replace(vbCr, "").Replace("'", "\'"))
        sb.Append("');")
        ScriptManager.RegisterStartupScript(Me.Page, Me.[GetType](), "showalert", sb.ToString(), True)
    End Sub



    Private Sub gvJenPinj_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvJenPinj.RowDeleting
        Try

            dbconn = New DBKewConn
            Dim strkodJenPinj As String

            Dim row As GridViewRow = DirectCast(gvJenPinj.Rows(e.RowIndex), GridViewRow)
            strkodJenPinj = Trim(row.Cells(1).Text.ToString.TrimEnd)


        Catch ex As Exception
            'fErrorLog("Jenis_Permohonan.aspx(gvJenPermohonan_RowDeleting)- " & ex.Message.ToString)
        End Try
    End Sub
    Private Sub gvSkimPinj_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvSkimPinj.RowDeleting
        Try

            dbconn = New DBKewConn
            Dim strkodSkimPinj As String

            Dim row As GridViewRow = DirectCast(gvSkimPinj.Rows(e.RowIndex), GridViewRow)
            strkodSkimPinj = Trim(row.Cells(1).Text.ToString.TrimEnd)


        Catch ex As Exception
            'fErrorLog("Jenis_Permohonan.aspx(gvJenPermohonan_RowDeleting)- " & ex.Message.ToString)
        End Try
    End Sub

    Private Sub txtKodKat_TextChanged(sender As Object, e As EventArgs) Handles txtKodKat.TextChanged
        Dim digitsOnly As Regex = New Regex("[^\d]")
        txtKodKat.Text = digitsOnly.Replace(txtKodKat.Text, "")
    End Sub

    Protected Sub ddlSaizRekod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSaizRekod.SelectedIndexChanged
        gvKatPinj.PageSize = CInt(ddlSaizRekod.SelectedValue)
        fBindgvKatPinj()
    End Sub


    Private Sub gvKatPinj_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvKatPinj.RowCommand
        Try
            If e.CommandName = "Select" Then
                '               Dim intStatus As Integer
                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvKatPinj.Rows(index)

                Dim strKodKat As String = TryCast(selectedRow.FindControl("lblKodKat"), Label).Text.ToString
                Dim strButiranKat As String = selectedRow.Cells(2).Text
                Dim strNamaPaparan As String = selectedRow.Cells(3).Text
                '                Dim strUrutan As String = TryCast(selectedRow.FindControl("lblUrutan"), Label).Text.ToString
                '                ViewState("Urutan") = strUrutan
                '                Dim strStatus As String = TryCast(selectedRow.FindControl("lblStatus"), Label).Text.ToString

                txtKodKat.Text = strKodKat
                txtButiranKat.Text = strButiranKat
                '                txtNPModul.Text = strNamaPaparan
                '                txtUrutanModul.Text = strUrutan
                '                If strStatus = "Aktif" Then intStatus = 1 Else intStatus = 0
                '                rbStatModul.SelectedValue = intStatus
                '                txtUrutanModul.ReadOnly = False
                ViewState("SaveMode") = "2"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function fUpdateKatPinj() As Boolean
        Dim blnSuccess As Boolean = True
        Dim strSql As String
        dbconn = New DBKewConn
        Try

            Dim strKodKat As String = Trim(txtKodKat.Text.TrimEnd)
            Dim strButiranKat As String = Trim(txtButiranKat.Text.TrimEnd)
            'Dim strNamaPaparan As String = Trim(txtNPModul.Text.TrimEnd)
            '            Dim strUrutan As String = Trim(txtUrutanModul.Text.TrimEnd)
            '            Dim intStatus As Integer = rbStatModul.SelectedValue

            '            If Not strUrutan = ViewState("Urutan") Then
            '            dbconn.sConnBeginTrans()
            '            Dim strKodKat2 As String = fGetKodKat(strUrutan)
            '            If Not strKodKat2 Is Nothing Then
            '            strSql = "update MK_UModul set Urutan = @Urutan where KodKat = @strKodKat2"
            '            Dim paramSql2() As SqlParameter = {
            '                                New SqlParameter("@Urutan", ViewState("Urutan")),
            '                                New SqlParameter("@strKodKat2", strKodKat2)
            '                                }

            '            If Not dbconn.fUpdateCommand(strSql, paramSql2) > 0 Then
            '            blnSuccess = False
            '            End If
            '            End If
            '            End If

            strSql = "update PJM_KatPinj set Butiran = @ButiranKat where KodKat = @KodKat"
            Dim paramSql() As SqlParameter = {
                        New SqlParameter("@ButiranKat", strButiranKat)}

            If Not dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                blnSuccess = False
            End If

        Catch ex As Exception
            blnSuccess = False
        End Try

        If blnSuccess = True Then
            dbconn.sConnCommitTrans()
            Return True
        Else
            dbconn.sConnRollbackTrans()
            Return False
        End If


    End Function

    Private Function fGetKodKat(ByVal strUrutan As String) As String

        '        Dim strKodJenPinj As String = ""

        '        Dim strSql As String = "select Kod from PJM_KatPinj with (nolock) where Kod = " & strUrutan & ""
        '        dbconn.sSelectCommand(strSql, strKodJenPinj)

        '        Return strKodJenPinj

    End Function

    Private Function fGetKodJenPinj(ByVal strKodstrKodKdrTempoh As String, ByVal strUrutan As String) As String

        Dim strKodJenPinj As String = ""

        Dim strSql As String = "select Kod from PJM_KadarTempoh with (nolock) where Kod = '" & strKodstrKodKdrTempoh & "'"
        dbconn.sSelectCommand(strSql, strKodJenPinj)
        Return strKodJenPinj

    End Function
    Private Function fGetKodSkimPinj(ByVal strKodKat As String, ByVal strUrutan As String) As String

        Dim strKodSkimPinj As String = ""

        Dim strSql As String = "select Kod from PJM_Skim with (nolock) where Kod  = '" & strKodKat & "'"
        dbconn.sSelectCommand(strSql, strKodSkimPinj)
        Return strKodSkimPinj

    End Function

    Private Function fGetKodKdrTempoh(ByVal strKodKdrTempoh As String, ByVal strUrutan As String) As String
        Dim KodKdrTempoh As String = ""
        Dim strSql As String = "select kod, Butiran from PJM_KadarTempoh where kod = '" & strKodKdrTempoh & "'"
        dbconn.sSelectCommand(strSql, KodKdrTempoh)
        Return KodKdrTempoh
    End Function

    Private Function fResetKatPinj()
        txtKodKat.Text = ""
        txtButiranKat.Text = ""
        'txtNPModul.Text = ""
        'txtUrutanModul.Text = ""
        'rbStatModul.SelectedIndex = 0
        txtKodKat.ReadOnly = True
        txtButiranKat.ReadOnly = True
        'txtNPModul.ReadOnly = True
        'txtUrutanModul.ReadOnly = True
        'rbStatModul.SelectedIndex = 0

    End Function

    Private Function fResetJenPinj()
        txtKodJenPinj.Text = ""
        txtButirJenPinj.Text = ""
        txtButirJenPinj.ReadOnly = True
    End Function

    Private Function fResetSkimPinj()
        txtKodSkimPinj.Text = ""
        txtButirSkimPinj.Text = ""
        txtButirSkimPinj.ReadOnly = True
    End Function

    Private Sub TabContainer1_ActiveTabChanged(sender As Object, e As EventArgs) Handles TabContainer1.ActiveTabChanged
        Select Case TabContainer1.ActiveTabIndex
            Case 0

                fBindgvKatPinj()
                fResetKatPinj()
            Case 1
                '               gvJenPinj.DataSource = New List(Of String)
                '                gvJenPinj.DataBind()
                fBindgvJenPinj()
                fResetJenPinj()
            Case 2
                'gvKdrTempoh.DataSource = New List(Of String)
                'gvKdrTempoh.DataBind()
                fBindGvKdrTempoh()
                'fResetKdrTempoh()
            Case 3
                fBindgvSkimPinj()
                fResetSkimPinj()



        End Select

        ViewState("SaveMode") = 1
    End Sub

    Private Sub gvJenPinj_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvJenPinj.RowCommand
        Try
            If e.CommandName = "Select" Then

                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvJenPinj.Rows(index)

                Dim strKodJenPinj As String = TryCast(selectedRow.FindControl("lblKodJenPinj"), Label).Text.ToString ' selectedRow.Cells(1).Text
                Dim strButirJenPinj As String = selectedRow.Cells(2).Text

                txtKodJenPinj.Text = strKodJenPinj
                txtButirJenPinj.Text = strButirJenPinj

                ViewState("SaveMode") = "2"
                txtKodJenPinj.ReadOnly = True

                txtButirJenPinj.ReadOnly = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub gvSkimPinj_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvSkimPinj.RowCommand
        Try
            If e.CommandName = "Select" Then

                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvSkimPinj.Rows(index)

                Dim strKodSkimPinj As String = TryCast(selectedRow.FindControl("lblKodSkimPinj"), Label).Text.ToString ' selectedRow.Cells(1).Text
                Dim strButirSkimPinj As String = selectedRow.Cells(2).Text

                txtKodSkimPinj.Text = strKodSkimPinj
                txtButirSkimPinj.Text = strButirSkimPinj

                ViewState("SaveMode") = "2"
                txtKodSkimPinj.ReadOnly = True

                txtButirSkimPinj.ReadOnly = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub lbtnBaruJenPinj_Click(sender As Object, e As EventArgs) Handles lbtnBaruJenPinj.Click

        Dim strKodJenPinj As String
        txtButirJenPinj.Text = ""
        txtButirJenPinj.ReadOnly = False
        txtKodJenPinj.Text = ""

        ViewState("SaveMode") = "1"

    End Sub
    Protected Sub lbtnBaruSkimPinj_Click(sender As Object, e As EventArgs) Handles lbtnBaruSkimPinj.Click

        Dim strKodSkimPinj As String
        txtButirSkimPinj.Text = ""
        txtButirSkimPinj.ReadOnly = False
        txtKodSkimPinj.Text = ""

        ViewState("SaveMode") = "1"

    End Sub
    Protected Sub lbtnSimpanJenPinj_Click(sender As Object, e As EventArgs) Handles lbtnSimpanJenPinj.Click

        'Dim strKodJenPinj As String
        'If ViewState("SaveMode") = "1" Then  'rekod baru
        '    Dim strKodJenPinj As String = Trim(txtKodJenPinj.Text.TrimEnd)
        '    'If fCheckKodKat(strKodJenPinj) = False Then
        '    '    If fInsertKatPinj() = True Then
        '    '        Alert("Rekod baru telah ditambah!")
        '    '        'fBindgvJenPinj()
        '    '        fResetJenPinj()
        '    '    Else
        '    '        Alert("Rekod baru gagal ditambah!")
        '    '    End If
        '    'Else
        '    '    Alert("Kod Modul yang dimasukkan telah wujud! Sila masukkan Kod Modul lain.")
        '    'End If


        'ElseIf ViewState("SaveMode") = "2" Then 'kemas kini
        '    If fUpdateJenPinj() = True Then
        '        Alert("Rekod telah dikemas kini!")
        '        fBindgvJenPinj(strKodJenPinj)
        '        ViewState("SaveMode") = "1"
        '    Else
        '        Alert("Rekod gagal dikemas kini!")
        '    End If

        'End If

    End Sub




    Private Sub lbtnHapusJenPinj_Click(sender As Object, e As EventArgs) Handles lbtnHapusJenPinj.Click
        Try
            Dim strSql As String
            dbconn = New DBKewConn
            Dim strKodJenPinj As String

            strKodJenPinj = Trim(txtKodJenPinj.Text.ToString.TrimEnd)

            strSql = "delete from PJM_JenisPinj where Kod = '" & strKodJenPinj & "'"
            dbconn.sConnBeginTrans()
            If dbconn.fUpdateCommand(strSql) > 0 Then
                dbconn.sConnCommitTrans()
                Alert("Rekod telah dipadam!")
                fBindgvJenPinj()
                fResetJenPinj()
            Else
                    dbconn.sConnRollbackTrans()
                End If



        Catch ex As Exception
            'fErrorLog("Jenis_Permohonan.aspx(gvJenPermohonan_RowDeleting)- " & ex.Message.ToString)
        End Try
    End Sub

    Protected Sub lbtnBaru_Click(sender As Object, e As EventArgs) Handles lbtnBaru.Click

        txtKodKat.Text = ""
        '        txtUrutanModul.Text = fGetNewUrutanModul()
        txtButiranKat.Text = ""
        '        txtNPModul.Text = ""

        txtKodKat.ReadOnly = False
        txtButiranKat.ReadOnly = False
        'txtNPModul.ReadOnly = False
        txtButiranKat.ReadOnly = False

        ViewState("SaveMode") = "1"

    End Sub


End Class