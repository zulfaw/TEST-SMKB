Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Public Class Kod_Kawalan_Komputer
    Inherits System.Web.UI.Page

    Dim dbconn As New DBKewConn

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                fBindgvJenama()
                fResetJenama()
                fBindgvIngatan()
                fResetIngatan()
                fBindGvCakeraKeras()
                fBindgvPencetak()



                TabContainer1.ActiveTab = tabModul
            End If

        Catch ex As Exception
            'fErrorLog("Pendaftaran_Menu.aspx(Page_Load())- " & ex.Message.ToString)
        End Try
    End Sub
    Private Sub fBindgvJenama()
        Using dt = fCreateJenama()

            'Dim strurutan As String = dt.Rows(dt.Rows.Count - 1)("Kod")
            'Dim intUrutan As String = CInt(strurutan)
            'intUrutan = intUrutan + 1
            'txtUrutanModul.Text = intUrutan

            gvJenama.DataSource = dt
            gvJenama.DataBind()

            lblJumMod.InnerText = dt.Rows.Count
        End Using
    End Sub
    Private Sub fBindgvPencetak()
        Using dt = fCreatePencetak()

            'Dim strurutan As String = dt.Rows(dt.Rows.Count - 1)("Kod")
            'Dim intUrutan As String = CInt(strurutan)
            'intUrutan = intUrutan + 1
            'txtUrutanModul.Text = intUrutan

            gvPencetak.DataSource = dt
            gvPencetak.DataBind()

            lblJumMod.InnerText = dt.Rows.Count
        End Using
    End Sub
    Private Sub fBindgvIngatan()
        Using dt = fCreateIngatan()

            gvIngatan.DataSource = dt
            gvIngatan.DataBind()

            lblJumSubMod.InnerText = dt.Rows.Count
        End Using

    End Sub
    Private Sub fBindgvKekunci()
        Using dt = fCreateKekunci()

            gvKekunci.DataSource = dt
            gvKekunci.DataBind()

            lblJumSubMod.InnerText = dt.Rows.Count
        End Using

    End Sub

    Private Sub fBindGvCakeraKeras()
        Using dt = fCreateCakeraKeras()

            gvCakeraKeras.DataSource = dt
            gvCakeraKeras.DataBind()

            lblJumCakeraKeras.InnerText = dt.Rows.Count
        End Using

    End Sub
    Private Function fCreateJenama() As DataTable
        Dim strSql As String = "select Kod, Butiran from PJM_Jenama order by Kod"

        Using dt = dbconn.fSelectCommandDt(strSql)
            Return dt
        End Using
    End Function
    Private Function fCreatePencetak() As DataTable
        Dim strSql As String = "select Kod, Butiran from PJM_NamaPencetak order by Kod"

        Using dt = dbconn.fSelectCommandDt(strSql)
            Return dt
        End Using
    End Function
    Private Function fCreateCakeraKeras() As DataTable
        Dim strSql As String = "select kod, Butiran from PJM_CakeraKeras  ORDER BY Kod"

        Using dt = dbconn.fSelectCommandDt(strSql)
            Return dt
        End Using

    End Function
    Private Function fCreateIngatan() As DataTable

        Dim strSql As String = "select Kod, Butiran from PJM_Ingatan order by Kod"

        Using dt = dbconn.fSelectCommandDt(strSql)
            Return dt
        End Using
    End Function
    Private Function fCreateKekunci() As DataTable

        Dim strSql As String = "select Kod, Butiran from PJM_Kekunci order by Kod"

        Using dt = dbconn.fSelectCommandDt(strSql)
            Return dt
        End Using
    End Function
    'Protected Sub OnRowDataBoundgvJenama(sender As Object, e As GridViewRowEventArgs) Handles gvJenama.RowDataBound

    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvJenama, "Select$" & e.Row.RowIndex)
    '        e.Row.ToolTip = "Klik untuk pilih rekod ini."
    '        e.Row.ForeColor = ColorTranslator.FromHtml("#000000")
    '    End If

    'End Sub

    'Protected Sub OnRowDataBoundgvIngatan(sender As Object, e As GridViewRowEventArgs) Handles gvIngatan.RowDataBound

    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvIngatan, "Select$" & e.Row.RowIndex)
    '        e.Row.ToolTip = "Klik untuk pilih rekod ini."
    '        e.Row.ForeColor = ColorTranslator.FromHtml("#000000")
    '    End If

    'End Sub

    Protected Sub OnRowDataBoundGvCakeraKeras(sender As Object, e As GridViewRowEventArgs) Handles gvCakeraKeras.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvCakeraKeras, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Klik untuk pilih rekod ini."
            e.Row.ForeColor = ColorTranslator.FromHtml("#000000")
        End If

    End Sub


    Private Function fUpdateIngatan() As Boolean

        Dim blnSuccess As Boolean = True
        Dim strSql As String
        dbconn = New DBKewConn
        Try
            Dim strKodIngatan As String = Trim(txtKodIngatan.Text.TrimEnd)
            Dim strButirIngatan As String = Trim(txtButirIngatan.Text.TrimEnd)

            strSql = "update PJM_Ingatan set butiran = @butirIngatan where kod = @kodIngatan"
            Dim paramSql() As SqlParameter = {
                        New SqlParameter("@butirIngatan", strButirIngatan),
                        New SqlParameter("@kodIngatan", strKodIngatan)
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

    Private Function fUpdateKekunci() As Boolean

        Dim blnSuccess As Boolean = True
        Dim strSql As String
        dbconn = New DBKewConn
        Try
            'Dim strKodKekunci As String = Trim(txtKodKekunci.Text.TrimEnd)
            'Dim strButirKekunci As String = Trim(txtButirKekunci.Text.TrimEnd)

            'strSql = "update PJM_Skim set butiran = @butirKekunci where kod = @kodKekunci"
            'Dim paramSql() As SqlParameter = {
            '            New SqlParameter("@butirKekunci", strButirKekunci),
            '            New SqlParameter("@kodKekunci", strKodKekunci)
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

    Private Function fInsertJenama() As Boolean
        Try
            Dim strSql As String
            Dim strKodJenama As String = Trim(txtKodJenama.Text.TrimEnd)
            Dim strButirJenama As String = Trim(txtButirJenama.Text.ToUpper.TrimEnd)
            '            Dim strNamaPaparan As String = Trim(txtNPModul.Text.ToUpper.TrimEnd)
            'Dim strUrutan As String = Trim(txtUrutanModul.Text.TrimEnd)
            'Dim intStatus As Integer = rbStatModul.SelectedValue

            strSql = "insert into PJM_Jenama (Kod, Butiran) values (@KodJenama,@ButirJenama)"

            Dim paramSql() As SqlParameter = {
                    New SqlParameter("@KodJenama", strKodJenama),
                    New SqlParameter("@ButirJenama", strButirJenama)
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

    Private Function fInsertIngatan() As Boolean
        dbconn = New DBKewConn
        Dim strSql As String

        Try
            Dim strKodIngatan As String = Trim(txtKodIngatan.Text.TrimEnd)
            Dim strButirIngatan As String = Trim(txtButirIngatan.Text.ToUpper.TrimEnd)

            dbconn.sConnBeginTrans()

            strSql = "insert into PJM_Ingatan (Kod , Butiran) " &
            "values (@KodIngatan,@ButirIngatan)"
            Dim paramSql() As SqlParameter = {
                           New SqlParameter("@KodIngatan", strKodIngatan),
                           New SqlParameter("@ButirIngatan", strButirIngatan)
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

    Private Function fInsertKekunci() As Boolean
        dbconn = New DBKewConn
        Dim strSql As String

        Try
            Dim strKodKekunci As String = Trim(txtKodKekunci.Text.TrimEnd)
            Dim strButirKekunci As String = Trim(txtButirKekunci.Text.ToUpper.TrimEnd)

            dbconn.sConnBeginTrans()

            strSql = "insert into PJM_Kekunci (Kod , Butiran) " &
            "values (@KodKekunci,@ButirKekunci)"
            Dim paramSql() As SqlParameter = {
                           New SqlParameter("@KodKekunci", strKodKekunci),
                           New SqlParameter("@ButirKekunci", strButirKekunci)
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

    Private Sub gvJenama_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvJenama.SelectedIndexChanged
        For Each row As GridViewRow In gvJenama.Rows
            If row.RowIndex = gvJenama.SelectedIndex Then
                row.ForeColor = ColorTranslator.FromHtml("#0000FF")
                'row.ToolTip = String.Empty
            Else
                row.ForeColor = ColorTranslator.FromHtml("#000000")
                ' row.ToolTip = "Klik untuk pilih rekod ini."
            End If
        Next
    End Sub





    Private Sub gvJenama_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvJenama.PageIndexChanging

        gvJenama.PageIndex = e.NewPageIndex
        If Session("SortedView") IsNot Nothing Then
            gvJenama.DataSource = Session("SortedView")
            gvJenama.DataBind()
        Else

            Dim dt As New DataTable
            dt = fCreateJenama()
            gvJenama.DataSource = dt
            gvJenama.DataBind()
        End If

    End Sub

    Private Sub gvJenama_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvJenama.Sorting

        Dim sortingDirection As String = String.Empty
        If direction = SortDirection.Ascending Then
            direction = SortDirection.Descending
            sortingDirection = "Desc"
        Else
            direction = SortDirection.Ascending
            sortingDirection = "Asc"
        End If

        Dim sortedView As New DataView(fCreateJenama())
        sortedView.Sort = Convert.ToString(e.SortExpression + " ") & sortingDirection
        Session("SortedView") = sortedView
        gvJenama.DataSource = sortedView
        gvJenama.DataBind()

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

    Private Function fCheckKodJenama(ByVal strKodJenama As String) As Boolean
        Dim strSql = "select count(*) from MK_Jenama  where Kod = '" & strKodJenama & "'"
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



    Private Sub gvIngatan_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvIngatan.RowDeleting
        Try

            dbconn = New DBKewConn
            Dim strkodIngatan As String

            Dim row As GridViewRow = DirectCast(gvIngatan.Rows(e.RowIndex), GridViewRow)
            strkodIngatan = Trim(row.Cells(1).Text.ToString.TrimEnd)


        Catch ex As Exception
            'fErrorLog("Jenis_Permohonan.aspx(gvJenPermohonan_RowDeleting)- " & ex.Message.ToString)
        End Try
    End Sub
    Private Sub gvKekunci_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvKekunci.RowDeleting
        Try

            dbconn = New DBKewConn
            Dim strkodKekunci As String

            Dim row As GridViewRow = DirectCast(gvKekunci.Rows(e.RowIndex), GridViewRow)
            strkodKekunci = Trim(row.Cells(1).Text.ToString.TrimEnd)


        Catch ex As Exception
            'fErrorLog("Jenis_Permohonan.aspx(gvJenPermohonan_RowDeleting)- " & ex.Message.ToString)
        End Try
    End Sub

    Private Sub txtKodJenama_TextChanged(sender As Object, e As EventArgs) Handles txtKodJenama.TextChanged
        Dim digitsOnly As Regex = New Regex("[^\d]")
        txtKodJenama.Text = digitsOnly.Replace(txtKodJenama.Text, "")
    End Sub

    Protected Sub ddlSaizRekod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSaizRekod.SelectedIndexChanged
        gvJenama.PageSize = CInt(ddlSaizRekod.SelectedValue)
        fBindgvJenama()
    End Sub


    Private Sub gvJenama_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvJenama.RowCommand
        Try
            If e.CommandName = "Select" Then
                '               Dim intStatus As Integer
                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvJenama.Rows(index)

                Dim strKodJenama As String = TryCast(selectedRow.FindControl("lblKodJenama"), Label).Text.ToString
                Dim strButirJenama As String = selectedRow.Cells(2).Text
                Dim strNamaPaparan As String = selectedRow.Cells(3).Text
                '                Dim strUrutan As String = TryCast(selectedRow.FindControl("lblUrutan"), Label).Text.ToString
                '                ViewState("Urutan") = strUrutan
                '                Dim strStatus As String = TryCast(selectedRow.FindControl("lblStatus"), Label).Text.ToString

                txtKodJenama.Text = strKodJenama
                txtButirJenama.Text = strButirJenama
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

    Private Function fUpdateJenama() As Boolean
        Dim blnSuccess As Boolean = True
        Dim strSql As String
        dbconn = New DBKewConn
        Try

            Dim strKodJenama As String = Trim(txtKodJenama.Text.TrimEnd)
            Dim strButirJenama As String = Trim(txtButirJenama.Text.TrimEnd)
            'Dim strNamaPaparan As String = Trim(txtNPModul.Text.TrimEnd)
            '            Dim strUrutan As String = Trim(txtUrutanModul.Text.TrimEnd)
            '            Dim intStatus As Integer = rbStatModul.SelectedValue

            '            If Not strUrutan = ViewState("Urutan") Then
            '            dbconn.sConnBeginTrans()
            '            Dim strKodJenama2 As String = fGetKodJenama(strUrutan)
            '            If Not strKodJenama2 Is Nothing Then
            '            strSql = "update MK_UModul set Urutan = @Urutan where KodJenama = @strKodJenama2"
            '            Dim paramSql2() As SqlParameter = {
            '                                New SqlParameter("@Urutan", ViewState("Urutan")),
            '                                New SqlParameter("@strKodJenama2", strKodJenama2)
            '                                }

            '            If Not dbconn.fUpdateCommand(strSql, paramSql2) > 0 Then
            '            blnSuccess = False
            '            End If
            '            End If
            '            End If

            strSql = "update PJM_Jenama set Butiran = @ButirJenama where Kod = @KodJenama"
            Dim paramSql() As SqlParameter = {
                        New SqlParameter("@ButirJenama", strButirJenama)}

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

    Private Function fGetKodJenama(ByVal strUrutan As String) As String

        '        Dim strKodIngatan As String = ""

        '        Dim strSql As String = "select Kod from PJM_Jenama with (nolock) where Kod = " & strUrutan & ""
        '        dbconn.sSelectCommand(strSql, strKodIngatan)

        '        Return strKodIngatan

    End Function

    Private Function fGetKodIngatan(ByVal strKodstrKodCakeraKeras As String, ByVal strUrutan As String) As String

        Dim strKodIngatan As String = ""

        Dim strSql As String = "select Kod from PJM_Ingatan with (nolock) where Kod = '" & strKodstrKodCakeraKeras & "'"
        dbconn.sSelectCommand(strSql, strKodIngatan)
        Return strKodIngatan

    End Function
    Private Function fGetKodKekunci(ByVal strKodJenama As String, ByVal strUrutan As String) As String

        Dim strKodKekunci As String = ""

        Dim strSql As String = "select Kod from PJM_Kekunci with (nolock) where Kod  = '" & strKodJenama & "'"
        dbconn.sSelectCommand(strSql, strKodKekunci)
        Return strKodKekunci

    End Function

    Private Function fGetKodCakeraKeras(ByVal strKodCakeraKeras As String, ByVal strUrutan As String) As String
        Dim KodCakeraKeras As String = ""
        Dim strSql As String = "select kod, Butiran from PJM_CakeraKeras where kod = '" & strKodCakeraKeras & "'"
        dbconn.sSelectCommand(strSql, KodCakeraKeras)
        Return KodCakeraKeras
    End Function

    Private Function fResetJenama()
        txtKodJenama.Text = ""
        txtButirJenama.Text = ""
        txtKodJenama.ReadOnly = True
        txtButirJenama.ReadOnly = True

    End Function

    Private Function fResetIngatan()
        txtKodIngatan.Text = ""
        txtButirIngatan.Text = ""
        txtButirIngatan.ReadOnly = True
    End Function

    Private Function fResetKekunci()
        txtKodKekunci.Text = ""
        txtButirKekunci.Text = ""
        txtButirKekunci.ReadOnly = True
    End Function
    Private Function fResetPencetak()
        txtKodPencetak.Text = ""
        txtButirPencetak.Text = ""
        txtButirPencetak.ReadOnly = True
    End Function
    Private Sub TabContainer1_ActiveTabChanged(sender As Object, e As EventArgs) Handles TabContainer1.ActiveTabChanged
        Select Case TabContainer1.ActiveTabIndex
            Case 0

                fBindgvJenama()
                fResetJenama()
            Case 1
                fBindgvIngatan()
                fResetIngatan()
            Case 2
                fBindGvCakeraKeras()
            Case 3
                fBindgvKekunci()
                fResetKekunci()
            Case 4
                fBindgvPencetak()
                fResetPencetak()

        End Select

        ViewState("SaveMode") = 1
    End Sub

    Private Sub gvIngatan_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvIngatan.RowCommand
        Try
            If e.CommandName = "Select" Then

                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvIngatan.Rows(index)

                Dim strKodIngatan As String = TryCast(selectedRow.FindControl("lblKodIngatan"), Label).Text.ToString ' selectedRow.Cells(1).Text
                Dim strButirIngatan As String = selectedRow.Cells(2).Text

                txtKodIngatan.Text = strKodIngatan
                txtButirIngatan.Text = strButirIngatan

                ViewState("SaveMode") = "2"
                txtKodIngatan.ReadOnly = True

                txtButirIngatan.ReadOnly = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub gvKekunci_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvKekunci.RowCommand
        Try
            If e.CommandName = "Select" Then

                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvKekunci.Rows(index)

                Dim strKodKekunci As String = TryCast(selectedRow.FindControl("lblKodKekunci"), Label).Text.ToString ' selectedRow.Cells(1).Text
                Dim strButirKekunci As String = selectedRow.Cells(2).Text

                txtKodKekunci.Text = strKodKekunci
                txtButirKekunci.Text = strButirKekunci

                ViewState("SaveMode") = "2"
                txtKodKekunci.ReadOnly = True

                txtButirKekunci.ReadOnly = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub lbtnBaruIngatan_Click(sender As Object, e As EventArgs) Handles lbtnBaruIngatan.Click

        Dim strKodIngatan As String
        txtButirIngatan.Text = ""
        txtButirIngatan.ReadOnly = False
        txtKodIngatan.Text = ""

        ViewState("SaveMode") = "1"

    End Sub
    Protected Sub lbtnBaruKekunci_Click(sender As Object, e As EventArgs) Handles lbtnBaruKekunci.Click

        Dim strKodKekunci As String
        txtButirKekunci.Text = ""
        txtButirKekunci.ReadOnly = False
        txtKodKekunci.Text = ""

        ViewState("SaveMode") = "1"

    End Sub
    Protected Sub lbtnSimpanIngatan_Click(sender As Object, e As EventArgs) Handles lbtnSimpanIngatan.Click

        'Dim strKodIngatan As String
        'If ViewState("SaveMode") = "1" Then  'rekod baru
        '    Dim strKodIngatan As String = Trim(txtKodIngatan.Text.TrimEnd)
        '    'If fCheckKodJenama(strKodIngatan) = False Then
        '    '    If fInsertJenama() = True Then
        '    '        Alert("Rekod baru telah ditambah!")
        '    '        'fBindgvIngatan()
        '    '        fResetIngatan()
        '    '    Else
        '    '        Alert("Rekod baru gagal ditambah!")
        '    '    End If
        '    'Else
        '    '    Alert("Kod Modul yang dimasukkan telah wujud! Sila masukkan Kod Modul lain.")
        '    'End If


        'ElseIf ViewState("SaveMode") = "2" Then 'kemas kini
        '    If fUpdateIngatan() = True Then
        '        Alert("Rekod telah dikemas kini!")
        '        fBindgvIngatan(strKodIngatan)
        '        ViewState("SaveMode") = "1"
        '    Else
        '        Alert("Rekod gagal dikemas kini!")
        '    End If

        'End If

    End Sub




    Private Sub lbtnHapusIngatan_Click(sender As Object, e As EventArgs) Handles lbtnHapusIngatan.Click
        Try
            Dim strSql As String
            dbconn = New DBKewConn
            Dim strKodIngatan As String

            strKodIngatan = Trim(txtKodIngatan.Text.ToString.TrimEnd)

            strSql = "delete from PJM_JenisPinj where Kod = '" & strKodIngatan & "'"
            dbconn.sConnBeginTrans()
            If dbconn.fUpdateCommand(strSql) > 0 Then
                dbconn.sConnCommitTrans()
                Alert("Rekod telah dipadam!")
                fBindgvIngatan()
                fResetIngatan()
            Else
                dbconn.sConnRollbackTrans()
            End If



        Catch ex As Exception
            'fErrorLog("Jenis_Permohonan.aspx(gvJenPermohonan_RowDeleting)- " & ex.Message.ToString)
        End Try
    End Sub

    Protected Sub lbtnBaru_Click(sender As Object, e As EventArgs) Handles lbtnBaru.Click

        txtKodJenama.Text = ""
        '        txtUrutanModul.Text = fGetNewUrutanModul()
        txtButirJenama.Text = ""
        '        txtNPModul.Text = ""

        txtKodJenama.ReadOnly = False
        txtButirJenama.ReadOnly = False
        'txtNPModul.ReadOnly = False
        txtButirJenama.ReadOnly = False

        ViewState("SaveMode") = "1"

    End Sub


End Class