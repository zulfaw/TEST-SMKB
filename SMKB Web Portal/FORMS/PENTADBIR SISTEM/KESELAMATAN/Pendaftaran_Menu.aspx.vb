Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Public Class Pendaftaran_Menu
    Inherits System.Web.UI.Page

    Dim dbconn As New DBKewConn

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                fBindDdlKodModul()
                fBindGvModul()
                fResetModul()

                TabContainer1.ActiveTab = tabModul
            End If

        Catch ex As Exception
            'fErrorLog("Pendaftaran_Menu.aspx(Page_Load())- " & ex.Message.ToString)
        End Try
    End Sub
    Private Sub fBindDdlKodModul()
        Dim strSql As String = "Select KodModul, DisModul, (KodModul + ' - ' + DisModul) as Butiran From MK_UModul ORDER BY KODMODUL"
        Using dt = dbconn.fSelectCommandDt(strSql)
            ddlKodModul.DataSource = dt
            ddlKodModul.DataTextField = "Butiran"
            ddlKodModul.DataValueField = "KodModul"
            ddlKodModul.DataBind()

            ddlKodModul.Items.Insert(0, New ListItem("- SILA PILIH - ", ""))
            ddlKodModul.SelectedIndex = 0

            ddlKodModul2.DataSource = dt
            ddlKodModul2.DataTextField = "Butiran"
            ddlKodModul2.DataValueField = "KodModul"
            ddlKodModul2.DataBind()

            ddlKodModul2.Items.Insert(0, New ListItem("- SILA PILIH - ", ""))
            ddlKodModul2.SelectedIndex = 0
        End Using
    End Sub

    Private Sub fBindDdlKodSubModul(ByVal strKodModul As String)
        Dim strSql As String = "select KodSub, DisSub, (KodSub + ' - ' + DisSub) as Butiran From MK_USubModul WHERE kodModul = '" & strKodModul & "' and status = 'true' ORDER BY Urutan"
        Using dt = dbconn.fSelectCommandDt(strSql)

            ddlKodSubModul.DataSource = dt
            ddlKodSubModul.DataTextField = "Butiran"
            ddlKodSubModul.DataValueField = "KodSub"
            ddlKodSubModul.DataBind()

            ddlKodSubModul.Items.Insert(0, New ListItem("- SILA PILIH - ", String.Empty))
            ddlKodSubModul.SelectedIndex = 0
        End Using
    End Sub

    Protected Sub ddlKodModul_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKodModul.SelectedIndexChanged
        Try
            Dim strKodModul As String
            strKodModul = ddlKodModul.SelectedValue.ToString

            fBindDdlKodSubModul(strKodModul)

        Catch ex As Exception
            'fErrorLog("Pendaftaran_Menu.aspx(ddlKodModul_SelectedIndexChanged)- " & ex.Message.ToString)
        End Try
    End Sub

    Protected Sub ddlKodSubModul_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKodSubModul.SelectedIndexChanged
        Try
            Dim strKodSubModul As String
            strKodSubModul = ddlKodSubModul.SelectedValue.ToString
            fBindGvSubMenu(strKodSubModul)
            fResetSubMenu()

        Catch ex As Exception
            'fErrorLog("Pendaftaran_Menu.aspx(ddlKodModul_SelectedIndexChanged)- " & ex.Message.ToString)
        End Try
    End Sub

    Private Sub fBindGvModul()
        Using dt = fCreateDtModul()

            Dim strurutan As String = dt.Rows(dt.Rows.Count - 1)("Urutan")
            Dim intUrutan As Integer = CInt(strurutan)
            intUrutan = intUrutan + 1
            txtUrutanModul.Text = intUrutan

            gvModul.DataSource = dt
            gvModul.DataBind()

            lblJumMod.InnerText = dt.Rows.Count
        End Using
    End Sub

    Private Sub fBindGVSubModul(ByVal strKodModul As String)
        Using dt = fCreateDtSubModul(strKodModul)

            gvSubModul.DataSource = dt
            gvSubModul.DataBind()

            lblJumSubMod.InnerText = dt.Rows.Count
        End Using

    End Sub


    Private Sub fBindGvSubMenu(ByVal strKodSubModul As String)


        Using dt = fCreateDtSubMenu(strKodSubModul)

            gvSubMenu.DataSource = dt
            gvSubMenu.DataBind()

            lblJumSubMenu.InnerText = dt.Rows.Count
        End Using

    End Sub

    Private Function fCreateDtModul() As DataTable

        Dim strSql As String = "select KodModul, NamaModul,DisModul,Urutan,Status from MK_UModul order by Urutan"

        Using dt = dbconn.fSelectCommandDt(strSql)
            Return dt
        End Using
    End Function

    Private Function fCreateDtSubModul(ByVal strKodModul As String) As DataTable
        Dim strSql As String = "select KodModul,KodSub,NamaSub ,DisSub ,Status ,Urutan from mk_usubmodul where kodmodul = '" & strKodModul & "' order by urutan"

        Using dt = dbconn.fSelectCommandDt(strSql)
            Return dt
        End Using
    End Function

    Private Function fCreateDtSubMenu(ByVal strKodSubModul As String) As DataTable
        Dim strSql As String = "select KodSub, KodSubMenu, NamaSubMenu, DisSubMenu, Urutan, Status from MK_USubMenu WHERE KodSub='" & strKodSubModul & "' ORDER BY URUTAN"

        Using dt = dbconn.fSelectCommandDt(strSql)
            Return dt
        End Using

    End Function


    'Protected Sub OnRowDataBoundGvModul(sender As Object, e As GridViewRowEventArgs) Handles gvModul.RowDataBound

    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvModul, "Select$" & e.Row.RowIndex)
    '        e.Row.ToolTip = "Klik untuk pilih rekod ini."
    '        e.Row.ForeColor = ColorTranslator.FromHtml("#000000")
    '    End If

    'End Sub

    'Protected Sub OnRowDataBoundGvSubModul(sender As Object, e As GridViewRowEventArgs) Handles gvSubModul.RowDataBound

    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvSubModul, "Select$" & e.Row.RowIndex)
    '        e.Row.ToolTip = "Klik untuk pilih rekod ini."
    '        e.Row.ForeColor = ColorTranslator.FromHtml("#000000")
    '    End If

    'End Sub

    Protected Sub OnRowDataBoundGvSubMenu(sender As Object, e As GridViewRowEventArgs) Handles gvSubMenu.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvSubMenu, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Klik untuk pilih rekod ini."
            e.Row.ForeColor = ColorTranslator.FromHtml("#000000")
        End If

    End Sub
    Private Function fUpdateSubMenu() As Boolean
        dbconn = New DBKewConn
        Dim blnSuccess As Boolean = True
        Try
            Dim strSql As String
            Dim strKodSubModul As String = Trim(ddlKodSubModul.SelectedValue.TrimEnd)
            Dim strKodSubMenu As String = Trim(txtKodSubMenu.Text.TrimEnd)
            Dim strNamaSubMenu As String = Trim(txtNamaSubMenu.Text.TrimEnd)
            Dim strNamaPaparan As String = Trim(txtNPSubMenu.Text.TrimEnd)
            Dim intUrutan As Integer = CInt(Trim(txtUrutanSubMenu.Text.TrimEnd))
            Dim intStatus As Integer = rbStatusSubMenu.SelectedValue

            If Not intUrutan = ViewState("Urutan") Then
                dbConn.sConnBeginTrans()

                Dim strKodSubMenu2 As String = fGetKodSubMenu(strKodSubModul, intUrutan)
                If strKodSubMenu2 <> "" Then
                    strSql = "update MK_USubMenu set urutan = @Urutan where KodSubMenu = @KodSubMenu"
                    Dim paramSql2() As SqlParameter = {
                                New SqlParameter("@Urutan", ViewState("Urutan")),
                                New SqlParameter("@KodSubMenu", strKodSubMenu2)
                                }

                    If Not dbconn.fUpdateCommand(strSql, paramSql2) > 0 Then
                        blnSuccess = False
                        Exit Try
                    End If
                End If
            End If

            strSql = "update MK_USubMenu set NamaSubMenu  = @NamaSubMenu, DisSubMenu = @DisSub, Urutan = @Urutan, Status = @Status where KodSubMenu = @KodSubMenu "
            Dim paramSql() As SqlParameter = {
                        New SqlParameter("@NamaSubMenu", strNamaSubMenu),
                        New SqlParameter("@DisSub", strNamaPaparan),
                        New SqlParameter("@Urutan", intUrutan),
                        New SqlParameter("@Status", intStatus),
                        New SqlParameter("@KodSubMenu", strKodSubMenu)
                        }

            If Not dbConn.fUpdateCommand(strSql, paramSql) > 0 Then
                blnSuccess = False
                Exit Try
            End If

        Catch ex As Exception
            blnSuccess = False
        End Try

        If blnSuccess = True Then
            dbConn.sConnCommitTrans()
            Return True
        Else
            dbConn.sConnRollbackTrans()
            Return False
        End If

    End Function

    Private Function fUpdateSubModul() As Boolean

        Dim blnSuccess As Boolean = True
        Dim strSql As String
        dbconn = New DBKewConn
        Try
            Dim strKodModul As String = Trim(ddlKodModul2.SelectedValue.TrimEnd).Substring(0, 2)
            Dim strKodSubModul As String = Trim(txtKodSubModul.Text.TrimEnd)
            Dim strNamaSubModul As String = Trim(txtNamaSubModul.Text.TrimEnd)
            Dim strNamaPaparan As String = Trim(txtNPSubModul.Text.TrimEnd)
            Dim intUrutan As Integer = CInt(Trim(txtUrutanSubModul.Text.TrimEnd))
            Dim intStatus As Integer = rbStatSubModul.SelectedValue

            If Not intUrutan = ViewState("Urutan") Then
                dbconn.sConnBeginTrans()

                Dim strKodSub As String = fGetKodSubModul(strKodModul, intUrutan)
                If Not strKodSub Is Nothing Then
                    strSql = "update MK_USubModul set Urutan = @Urutan where KodSub = @strKodSub"
                    Dim paramSql2() As SqlParameter = {
                            New SqlParameter("@Urutan", ViewState("Urutan")),
                            New SqlParameter("@strKodSub", strKodSub)
                            }

                    If Not dbconn.fUpdateCommand(strSql, paramSql2) > 0 Then
                        blnSuccess = False
                        Exit Try
                    End If
                End If
            End If

            strSql = "update MK_USubModul set NamaSub = @NamaSub, DisSub = @DisSub, Urutan = @Urutan, Status = @Status where kodsub = @kodsub"
            Dim paramSql() As SqlParameter = {
                        New SqlParameter("@NamaSub", strNamaSubModul),
                        New SqlParameter("@DisSub", strNamaPaparan),
                        New SqlParameter("@Urutan", intUrutan),
                        New SqlParameter("@Status", intStatus),
                        New SqlParameter("@kodsub", strKodSubModul)
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

    Private Function fInsertModul() As Boolean
        Try
            Dim strSql As String
            Dim strKodModul As String = Trim(txtKodModul.Text.TrimEnd)
            Dim strNamaModul As String = Trim(txtNamaModul.Text.ToUpper.TrimEnd)
            Dim strNamaPaparan As String = Trim(txtNPModul.Text.ToUpper.TrimEnd)
            Dim strUrutan As String = Trim(txtUrutanModul.Text.TrimEnd)
            Dim intStatus As Integer = rbStatModul.SelectedValue

            strSql = "insert into MK_UModul (KodModul, namaModul, dismodul, Urutan, Status) values (@KodModul,@KodNamaModul,@NamaPaparan,@Urutan, @Status)"

            Dim paramSql() As SqlParameter = {
                    New SqlParameter("@KodModul", strKodModul),
                    New SqlParameter("@KodNamaModul", strNamaModul),
                    New SqlParameter("@NamaPaparan", strNamaPaparan),
                    New SqlParameter("@Urutan", strUrutan),
                    New SqlParameter("@Status", intStatus)
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

    Private Function fInsertSubModul() As Boolean
        dbconn = New DBKewConn
        Dim strSql As String

        Try
            Dim strKodModul As String = Trim(ddlKodModul2.SelectedValue.ToString)
            Dim strKodSubModul As String = Trim(txtKodSubModul.Text.TrimEnd)
            Dim strNamaSubModul As String = Trim(txtNamaSubModul.Text.ToUpper.TrimEnd)
            Dim strNamaPaparan As String = Trim(txtNPSubModul.Text.ToUpper.TrimEnd)
            Dim intUrutan As Integer = CInt(Trim(txtUrutanSubModul.Text.TrimEnd))
            Dim intStatus As Integer = rbStatSubModul.SelectedValue

            dbconn.sConnBeginTrans()

            strSql = "insert into MK_USubModul (KodModul , kodsub, NamaSub , DisSub , urutan, Status ) " &
            "values (@KodModul,@KodSub,@NamaSub,@DisSub,@Urutan,@Status)"
            Dim paramSql() As SqlParameter = {
                           New SqlParameter("@KodModul", strKodModul),
                           New SqlParameter("@KodSub", strKodSubModul),
                           New SqlParameter("@NamaSub", strNamaSubModul),
                           New SqlParameter("@DisSub", strNamaPaparan),
                           New SqlParameter("@Urutan", intUrutan),
                           New SqlParameter("@Status", intStatus)
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
    Private Function fInsertSubMenu() As Boolean

        dbconn = New DBKewConn
        Dim strSql As String

        Try
            Dim strKodSubModul As String = Trim(ddlKodSubModul.SelectedValue.TrimEnd)
            Dim strKodSubMenu As String = Trim(txtKodSubMenu.Text.TrimEnd)
            Dim strNamaSubMenu As String = Trim(txtNamaSubMenu.Text.ToUpper.TrimEnd)
            Dim strNamaPaparan As String = Trim(txtNPSubMenu.Text.ToUpper.TrimEnd)
            Dim intUrutan As Integer = CInt(Trim(txtUrutanSubMenu.Text.TrimEnd))
            Dim intStatus As Integer = CInt(rbStatusSubMenu.SelectedValue)

            dbconn.sConnBeginTrans()

            strSql = "insert into MK_USubMenu (KodSub , KodSubMenu , NamaSubMenu , DisSubMenu , Urutan, Status ) values (@KodSub, @KodSubMenu, @NamaSubMenu, @DisSubMenu, @Urutan, @Status)"
            Dim paramSql() As SqlParameter = {
                           New SqlParameter("@KodSub", strKodSubModul),
                           New SqlParameter("@KodSubMenu", strKodSubMenu),
                           New SqlParameter("@NamaSubMenu", strNamaSubMenu),
                           New SqlParameter("@DisSubMenu", strNamaPaparan),
                           New SqlParameter("@Urutan", intUrutan),
                           New SqlParameter("@Status", intStatus)
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

    Private Sub gvModul_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvModul.SelectedIndexChanged
        For Each row As GridViewRow In gvModul.Rows
            If row.RowIndex = gvModul.SelectedIndex Then
                row.ForeColor = ColorTranslator.FromHtml("#0000FF")
                'row.ToolTip = String.Empty
            Else
                row.ForeColor = ColorTranslator.FromHtml("#000000")
                ' row.ToolTip = "Klik untuk pilih rekod ini."
            End If
        Next
    End Sub


    Private Sub gvSubMenu_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvSubMenu.SelectedIndexChanged

        Try
            For Each row As GridViewRow In gvSubMenu.Rows
                If row.RowIndex = gvSubMenu.SelectedIndex Then
                    row.ForeColor = ColorTranslator.FromHtml("#0000FF")
                    'row.ToolTip = String.Empty
                Else
                    row.ForeColor = ColorTranslator.FromHtml("#000000")
                    'row.ToolTip = "Klik untuk pilih rekod ini."
                End If
            Next
        Catch ex As Exception

        End Try

    End Sub

    Private Sub gvSubMenu_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvSubMenu.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvSubMenu, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Klik untuk pilih rekod ini."
            e.Row.ForeColor = ColorTranslator.FromHtml("#000000")
        End If
    End Sub

    Private Sub ddlKodModul2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKodModul2.SelectedIndexChanged

        Dim strKodModul As String
            strKodModul = ddlKodModul2.SelectedValue.ToString
            fBindGVSubModul(strKodModul)
        fResetSubModul()
    End Sub

    Private Sub gvModul_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvModul.PageIndexChanging

        gvModul.PageIndex = e.NewPageIndex
            If Session("SortedView") IsNot Nothing Then
                gvModul.DataSource = Session("SortedView")
                gvModul.DataBind()
            Else

                Dim dt As New DataTable
                dt = fCreateDtModul()
                gvModul.DataSource = dt
                gvModul.DataBind()
            End If

    End Sub

    Private Sub gvModul_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvModul.Sorting

        Dim sortingDirection As String = String.Empty
            If direction = SortDirection.Ascending Then
                direction = SortDirection.Descending
                sortingDirection = "Desc"
            Else
                direction = SortDirection.Ascending
                sortingDirection = "Asc"
            End If

            Dim sortedView As New DataView(fCreateDtModul())
            sortedView.Sort = Convert.ToString(e.SortExpression + " ") & sortingDirection
            Session("SortedView") = sortedView
            gvModul.DataSource = sortedView
            gvModul.DataBind()

    End Sub

    Private Sub gvSubModul_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvSubModul.PageIndexChanging

        gvSubModul.PageIndex = e.NewPageIndex
            If Session("SortedView") IsNot Nothing Then
                gvSubModul.DataSource = Session("SortedView")
                gvSubModul.DataBind()
            Else

                Dim dt As New DataTable
                Dim strKodModul As String = Trim(ddlKodModul2.SelectedValue.ToString.TrimEnd)
                dt = fCreateDtSubModul(strKodModul) ' fCreateDtModul()
                gvSubModul.DataSource = dt
                gvSubModul.DataBind()
            End If

    End Sub

    Private Sub gvSubMenu_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvSubMenu.PageIndexChanging

        gvSubMenu.PageIndex = e.NewPageIndex
            If Session("SortedView") IsNot Nothing Then
                gvSubMenu.DataSource = Session("SortedView")
                gvSubMenu.DataBind()
            Else

                Dim dt As New DataTable
                Dim strKodSubModul As String = Trim(ddlKodSubModul.SelectedValue.ToString.TrimEnd)
                dt = fCreateDtSubMenu(strKodSubModul) ' fCreateDtModul()
                gvSubMenu.DataSource = dt
                gvSubMenu.DataBind()
            End If

    End Sub

    Private Sub gvSubMenu_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvSubMenu.Sorting

        Dim sortingDirection As String = String.Empty
            If direction = SortDirection.Ascending Then
                direction = SortDirection.Descending
                sortingDirection = "Desc"
            Else
                direction = SortDirection.Ascending
                sortingDirection = "Asc"
            End If
            Dim strKodSubModul As String
            strKodSubModul = Trim(ddlKodSubModul.SelectedValue.ToString.TrimEnd)
            Dim sortedView As New DataView(fCreateDtSubMenu(strKodSubModul))
            sortedView.Sort = Convert.ToString(e.SortExpression + " ") & sortingDirection
            Session("SortedView") = sortedView
            gvSubMenu.DataSource = sortedView
            gvSubMenu.DataBind()

    End Sub

    Private Sub gvSubModul_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvSubModul.Sorting

        Dim sortingDirection As String = String.Empty
            If direction = SortDirection.Ascending Then
                direction = SortDirection.Descending
                sortingDirection = "Desc"
            Else
                direction = SortDirection.Ascending
                sortingDirection = "Asc"
            End If
            Dim strKodModul As String = Trim(ddlKodModul2.SelectedValue.ToString.TrimEnd)
            Dim sortedView As New DataView(fCreateDtSubModul(strKodModul))
            sortedView.Sort = Convert.ToString(e.SortExpression + " ") & sortingDirection
            Session("SortedView") = sortedView
            gvSubModul.DataSource = sortedView
            gvSubModul.DataBind()


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

    Private Function fCheckKodModul(ByVal strKodModul As String) As Boolean
        Dim strSql = "select count(*) from MK_UModul  where KodModul = '" & strKodModul & "'"
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



    Private Sub gvSubModul_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvSubModul.RowDeleting
        Try
            Dim strSql As String
            dbconn = New DBKewConn
            Dim strKodModul As String
            Dim strkodSubmodul As String

            Dim row As GridViewRow = DirectCast(gvSubModul.Rows(e.RowIndex), GridViewRow)
            strkodSubmodul = Trim(row.Cells(1).Text.ToString.TrimEnd)

            'delete from MK_UModul where KodModul = ''
            strSql = "delete from MK_USubModul where KodSub  = '" & strkodSubmodul & "'"
            If dbconn.fUpdateCommand(strSql) > 0 Then
                Alert("Rekod telah dipadam!")
                strKodModul = ddlKodModul2.SelectedValue.ToString
                fBindGVSubModul(strKodModul)
            End If

        Catch ex As Exception
            'fErrorLog("Jenis_Permohonan.aspx(gvJenPermohonan_RowDeleting)- " & ex.Message.ToString)
        End Try
    End Sub


    Private Sub txtKodModul_TextChanged(sender As Object, e As EventArgs) Handles txtKodModul.TextChanged
        Dim digitsOnly As Regex = New Regex("[^\d]")
        txtKodModul.Text = digitsOnly.Replace(txtKodModul.Text, "")
    End Sub

    Protected Sub ddlSaizRekod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSaizRekod.SelectedIndexChanged
        gvModul.PageSize = CInt(ddlSaizRekod.SelectedValue)
        fBindGvModul()
    End Sub

    Protected Sub ddlSaizRekodSubMenu_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSaizRekodSubMenu.SelectedIndexChanged
        gvSubModul.PageSize = CInt(ddlSaizRekod.SelectedValue)
        fBindGvSubMenu(ddlKodSubModul.SelectedValue)
    End Sub

    Protected Sub ddlSaizRekodSubModul_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSaizRekodSubModul.SelectedIndexChanged
        gvSubMenu.PageSize = CInt(ddlSaizRekod.SelectedValue)

        fBindGVSubModul(ddlKodModul2.SelectedValue)
    End Sub

    Private Sub gvModul_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvModul.RowCommand
        Try
            If e.CommandName = "Select" Then
                Dim intStatus As Integer
                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvModul.Rows(index)

                Dim strKodModul As String = TryCast(selectedRow.FindControl("lblKodModul"), Label).Text.ToString
                Dim strNamaModul As String = selectedRow.Cells(2).Text
                Dim strNamaPaparan As String = selectedRow.Cells(3).Text
                Dim strUrutan As String = TryCast(selectedRow.FindControl("lblUrutan"), Label).Text.ToString
                ViewState("Urutan") = strUrutan
                Dim strStatus As String = TryCast(selectedRow.FindControl("lblStatus"), Label).Text.ToString

                txtKodModul.Text = strKodModul
                txtNamaModul.Text = strNamaModul
                txtNPModul.Text = strNamaPaparan
                txtUrutanModul.Text = strUrutan
                If strStatus = "Aktif" Then intStatus = 1 Else intStatus = 0
                rbStatModul.SelectedValue = intStatus
                txtUrutanModul.ReadOnly = False
                ViewState("SaveMode") = "2"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function fUpdateModul() As Boolean
        Dim blnSuccess As Boolean = True
        Dim strSql As String
        dbconn = New DBKewConn
        Try

            Dim strKodModul As String = Trim(txtKodModul.Text.TrimEnd)
            Dim strNamaModul As String = Trim(txtNamaModul.Text.TrimEnd)
            Dim strNamaPaparan As String = Trim(txtNPModul.Text.TrimEnd)
            Dim strUrutan As String = Trim(txtUrutanModul.Text.TrimEnd)
            Dim intStatus As Integer = rbStatModul.SelectedValue

            If Not strUrutan = ViewState("Urutan") Then
                dbconn.sConnBeginTrans()
                Dim strKodModul2 As String = fGetKodModul(strUrutan)
                If Not strKodModul2 Is Nothing Then
                    strSql = "update MK_UModul set Urutan = @Urutan where KodModul = @strKodModul2"
                    Dim paramSql2() As SqlParameter = {
                                New SqlParameter("@Urutan", ViewState("Urutan")),
                                New SqlParameter("@strKodModul2", strKodModul2)
                                }

                    If Not dbconn.fUpdateCommand(strSql, paramSql2) > 0 Then
                        blnSuccess = False
                    End If
                End If
            End If

            strSql = "update MK_UModul set NamaModul = @NamaModul,DisModul = @DisModul, Urutan = @Urutan, Status = @Status where KodModul = @KodModul"
            Dim paramSql() As SqlParameter = {
                        New SqlParameter("@NamaModul", strNamaModul),
                        New SqlParameter("@DisModul", strNamaPaparan),
                        New SqlParameter("@KodModul", strKodModul),
                        New SqlParameter("@Urutan", strUrutan),
                         New SqlParameter("@Status", intStatus)
                        }

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

    Private Function fGetKodModul(ByVal strUrutan As String) As String

        Dim strKodSubModul As String = ""

        Dim strSql As String = "select KodModul from MK_UModul with (nolock) where Urutan = " & strUrutan & ""
            dbconn.sSelectCommand(strSql, strKodSubModul)

        Return strKodSubModul

    End Function

    Private Function fGetKodSubModul(ByVal strKodModul As String, ByVal strUrutan As String) As String

        Dim strKodSubModul As String = ""

        Dim strSql As String = "select KodSub from MK_USubModul with (nolock) where kodmodul = '" & strKodModul & "' and Urutan = " & strUrutan & ""
        dbconn.sSelectCommand(strSql, strKodSubModul)
        Return strKodSubModul

    End Function

    Private Function fGetKodSubMenu(ByVal strKodSubModul As String, ByVal strUrutan As String) As String
        Dim KodSubMenu As String = ""
        Dim strSql As String = "select KodSubMenu from MK_USubMenu with (nolock) where KodSub = '" & strKodSubModul & "' and Urutan = " & strUrutan & ""
        dbconn.sSelectCommand(strSql, KodSubMenu)
        Return KodSubMenu
    End Function

    Private Function fResetModul()
        txtKodModul.Text = ""
        txtNamaModul.Text = ""
        txtNPModul.Text = ""
        txtUrutanModul.Text = ""
        rbStatModul.SelectedIndex = 0
        txtKodModul.ReadOnly = True
        txtNamaModul.ReadOnly = True
        txtNPModul.ReadOnly = True
        txtUrutanModul.ReadOnly = True
        rbStatModul.SelectedIndex = 0

    End Function

    Private Function fResetSubModul()
        txtKodSubModul.Text = ""
        txtNamaSubModul.Text = ""
        txtNPSubModul.Text = ""
        txtUrutanSubModul.Text = ""
        rbStatSubModul.SelectedIndex = 0
        txtNamaSubModul.ReadOnly = True
        txtNPSubModul.ReadOnly = True
        txtUrutanSubModul.ReadOnly = True
    End Function

    Private Function fResetSubMenu()
        txtKodSubMenu.Text = ""
        txtNamaSubMenu.Text = ""
        txtNPSubMenu.Text = ""
        txtUrutanSubMenu.Text = ""

        txtKodSubMenu.ReadOnly = True
        txtNamaSubMenu.ReadOnly = True
        txtNPSubMenu.ReadOnly = True
        txtUrutanSubMenu.ReadOnly = True
        rbStatusSubMenu.SelectedIndex = 0

    End Function

    Private Sub TabContainer1_ActiveTabChanged(sender As Object, e As EventArgs) Handles TabContainer1.ActiveTabChanged
        Select Case TabContainer1.ActiveTabIndex
            Case 0

                fBindDdlKodModul()
                fBindGvModul()
                fResetModul()
            Case 1
                gvSubModul.DataSource = New List(Of String)
                gvSubModul.DataBind()
                fBindDdlKodModul()
                fResetSubModul()
            Case 2
                gvSubMenu.DataSource = New List(Of String)
                gvSubMenu.DataBind()
                fBindDdlKodModul()
                fResetSubMenu()
                ddlKodSubModul.Items.Clear()
        End Select

        ViewState("SaveMode") = 1
    End Sub

    Private Sub gvSubModul_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvSubModul.RowCommand
        Try
            If e.CommandName = "Select" Then

                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvSubModul.Rows(index)

                Dim strKodSubModul As String = TryCast(selectedRow.FindControl("lblKodSubModul"), Label).Text.ToString ' selectedRow.Cells(1).Text
                Dim strNamaSubModul As String = selectedRow.Cells(2).Text
                Dim strNamaPaparan As String = selectedRow.Cells(3).Text
                Dim strUrutan As String = TryCast(selectedRow.FindControl("lblUrutan"), Label).Text.ToString  ' selectedRow.Cells(4).Text
                Dim strStatus As String = TryCast(selectedRow.FindControl("lblStatus"), Label).Text.ToString  ' selectedRow.Cells(5).Text

                txtKodSubModul.Text = strKodSubModul
                txtNamaSubModul.Text = strNamaSubModul
                txtNPSubModul.Text = strNamaPaparan
                If strStatus = "Aktif" Then rbStatSubModul.SelectedValue = 1 Else rbStatSubModul.SelectedValue = 0
                txtUrutanSubModul.Text = strUrutan
                ViewState("Urutan") = strUrutan

                ViewState("SaveMode") = "2"
                txtKodSubModul.ReadOnly = True

                txtNamaSubModul.ReadOnly = False
                txtNPSubModul.ReadOnly = False
                txtUrutanSubModul.ReadOnly = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lbtnBaruSubModul_Click(sender As Object, e As EventArgs) Handles lbtnBaruSubModul.Click


        txtNamaSubModul.Text = ""
            txtNPSubModul.Text = ""
            rbStatSubModul.SelectedIndex = 0
            txtNamaSubModul.ReadOnly = False
            txtNPSubModul.ReadOnly = False


            Dim strKodModul As String = Trim(ddlKodModul2.SelectedValue.TrimEnd)
            txtKodSubModul.Text = fGetNewKodSubModul(strKodModul)
            txtUrutanSubModul.Text = fGetNewUrutanSubModul(strKodModul)

            ViewState("SaveMode") = "1"

    End Sub

    Protected Sub lbtnSimpanSubModul_Click(sender As Object, e As EventArgs) Handles lbtnSimpanSubModul.Click

        Dim strKodModul As String
            strKodModul = ddlKodModul2.SelectedValue.ToString
            If ViewState("SaveMode") = "1" Then  'rekod baru
                If fInsertSubModul() = True Then
                    Alert("Rekod baru telah ditambah!")
                    fBindGVSubModul(strKodModul)
                    fResetSubModul()
                Else
                    Alert("Rekod baru gagal ditambah!")
                End If

            ElseIf ViewState("SaveMode") = "2" Then 'kemas kini
                If fUpdateSubModul() = True Then
                    Alert("Rekod telah dikemas kini!")
                    fBindGVSubModul(strKodModul)
                    ViewState("SaveMode") = "1"
                Else
                    Alert("Rekod gagal dikemas kini!")
                End If

            End If

    End Sub

    Private Sub gvSubMenu_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvSubMenu.RowCommand

        If e.CommandName = "Select" Then
                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvSubMenu.Rows(index)

                Dim strKodSubMenu As String = TryCast(selectedRow.FindControl("lblKodSubMenu"), Label).Text.ToString ' selectedRow.Cells(1).Text
                txtKodSubMenu.Text = strKodSubMenu
                txtNamaSubMenu.Text = selectedRow.Cells(2).Text
                txtNPSubMenu.Text = selectedRow.Cells(3).Text
                Dim strUrutan As String = TryCast(selectedRow.FindControl("lblUrutan"), Label).Text.ToString
                txtUrutanSubMenu.Text = strUrutan
                ViewState("Urutan") = strUrutan
                Dim strStatus As String = TryCast(selectedRow.FindControl("lblStatus"), Label).Text.ToString
                If strStatus = "Aktif" Then rbStatusSubMenu.SelectedIndex = 0 Else rbStatusSubMenu.SelectedIndex = 1

                ViewState("SaveMode") = "2"
                txtNamaSubMenu.ReadOnly = False
                txtNPSubMenu.ReadOnly = False
                txtUrutanSubMenu.ReadOnly = False
            End If

    End Sub

    Protected Sub lbtnSimpanSubMenu_Click(sender As Object, e As EventArgs) Handles lbtnSimpanSubMenu.Click

        If ViewState("SaveMode") = "1" Then


                If fInsertSubMenu() = True Then
                    fResetSubMenu()
                    Dim strKodSubModul As String = ddlKodSubModul.SelectedValue.ToString
                    fBindGvSubMenu(strKodSubModul)
                    Alert("Rekod baru telah ditambah!")
                Else
                    Alert("Rekod baru gagal ditambah!")
                End If

            ElseIf ViewState("SaveMode") = "2" Then
                If fUpdateSubMenu() = True Then
                    fResetSubMenu()
                    Dim strKodSubModul As String
                    strKodSubModul = ddlKodSubModul.SelectedValue.ToString
                    fBindGvSubMenu(strKodSubModul)
                    Alert("Rekod telah dikemas kini!")
                Else
                    Alert("Rekod gagal dikemas kini!")
                End If
            End If


    End Sub

    Protected Sub lbtnBaruSubMenu_Click(sender As Object, e As EventArgs) Handles lbtnBaruSubMenu.Click


        fResetSubMenu()
            txtNamaSubMenu.ReadOnly = False
            txtNPSubMenu.ReadOnly = False

            Dim strKodSubModul As String = ddlKodSubModul.SelectedValue
            txtKodSubMenu.Text = fGetNewKodSubMenu(strKodSubModul)
            txtUrutanSubMenu.Text = fGetNewUrutanSubMenu(strKodSubModul)

            ViewState("SaveMode") = "1"

    End Sub

    Private Function fGetNewKodSubMenu(ByVal strKodSubModul As String) As String

        Dim strSql As String = "select max(KodSubMenu ) as KodAkhir from MK_USubMenu where kodsub = '" & strKodSubModul & "'"

        Dim NoAkhir As Integer
            Dim KodAkhir As String = ""
            Dim strNewKodSubMenu As String
        Using ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    KodAkhir = ds.Tables(0).Rows(0)("KodAkhir").ToString
                End If
            End If
        End Using

        If KodAkhir = "" Then
                NoAkhir = 1
            Else
                NoAkhir = CInt(KodAkhir.Substring(4, 2))
                NoAkhir = NoAkhir + 1
            End If

            strNewKodSubMenu = strKodSubModul & NoAkhir.ToString("D2")

            Return strNewKodSubMenu

    End Function

    Private Function fGetNewUrutanSubMenu(ByVal strKodSubModul As String) As String

        Dim strSql As String = "select max(Urutan) as Urutan from MK_USubMenu where kodsub = '" & strKodSubModul & "'"

        Dim intUrutan, intNewUrutan As Integer
        Using ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    intUrutan = CInt(IIf(ds.Tables(0).Rows(0)("Urutan").ToString = "", 0, ds.Tables(0).Rows(0)("Urutan").ToString))
                Else
                    intUrutan = 0
                End If
            Else
                intUrutan = 0
            End If

            intNewUrutan = intUrutan + 1

            Return intNewUrutan
        End Using
    End Function

    Private Function fGetNewKodSubModul(ByVal strKodModul As String) As String

        'Dim strSql As String = "select count(*) as JumRow from MK_USubModul where KodModul = '" & strKodModul & "'"
        Dim strSql As String = "select max(kodsub) as KodAkhir from MK_USubModul where KodModul = '" & strKodModul & "'"

        Dim NoAkhir As Integer
            Dim KodAkhir As String = ""
            Dim strNewKodSubModul As String
        Using ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    KodAkhir = ds.Tables(0).Rows(0)("KodAkhir").ToString
                End If
            End If
        End Using
        If KodAkhir = "" Then
                NoAkhir = 1
            Else
                NoAkhir = CInt(KodAkhir.Substring(2, 2))
                NoAkhir = NoAkhir + 1
            End If

            strNewKodSubModul = strKodModul & NoAkhir.ToString("D2")

            Return strNewKodSubModul

    End Function

    Private Function fGetNewUrutanSubModul(ByVal strKodModul As String) As String

        Dim strSql As String = "select max(Urutan) as Urutan from MK_USubModul where KodModul = '" & strKodModul & "'"

        Dim intUrutan, intNewUrutan As Integer
        Using ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    intUrutan = CInt(IIf(ds.Tables(0).Rows(0)("Urutan").ToString = "", 0, ds.Tables(0).Rows(0)("Urutan").ToString))
                Else
                    intUrutan = 0
                End If
            Else
                intUrutan = 0
            End If

            intNewUrutan = intUrutan + 1

            Return intNewUrutan
        End Using
    End Function

    Private Function fGetNewUrutanModul() As String

        Dim strSql As String = "select max(Urutan) as Urutan from MK_UModul"

        Dim intUrutan, intNewUrutan As Integer
        Using ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    intUrutan = CInt(IIf(ds.Tables(0).Rows(0)("Urutan").ToString = "", 0, ds.Tables(0).Rows(0)("Urutan").ToString))
                Else
                    intUrutan = 0
                End If
            Else
                intUrutan = 0
            End If

            intNewUrutan = intUrutan + 1

            Return intNewUrutan

        End Using

    End Function

    Protected Sub lbtnHapusSubMenu_Click(sender As Object, e As EventArgs) Handles lbtnHapusSubMenu.Click
        Try
            Dim strSql As String
            dbconn = New DBKewConn
            Dim strKodSubMenu As String

            strKodSubMenu = Trim(txtKodSubMenu.Text.ToString.TrimEnd)
            strSql = "delete from MK_USubMenu where KodSubMenu = '" & strKodSubMenu & "'"
            dbconn.sConnBeginTrans()
            If dbconn.fUpdateCommand(strSql) > 0 Then
                dbconn.sConnCommitTrans()
                Alert("Rekod telah dipadam!")
                fBindGvSubMenu(strKodSubModul:=Trim(ddlKodSubModul.SelectedValue))
                fResetSubMenu()
            Else
                dbconn.sConnRollbackTrans()
            End If

        Catch ex As Exception
            'fErrorLog("Jenis_Permohonan.aspx(gvJenPermohonan_RowDeleting)- " & ex.Message.ToString)
        End Try
    End Sub

    Private Sub lbtnHapusSubModul_Click(sender As Object, e As EventArgs) Handles lbtnHapusSubModul.Click
        Try
            Dim strSql As String
            dbconn = New DBKewConn
            Dim strKodSubModul As String

            strKodSubModul = Trim(txtKodSubModul.Text.ToString.TrimEnd)

            strSql = "select * from MK_USubMenu where KodSub = '" & strKodSubModul & "'"
            If fCheckRec(strSql) = False Then
                strSql = "delete from MK_USubModul where KodSub = '" & strKodSubModul & "'"
                dbconn.sConnBeginTrans()
                If dbconn.fUpdateCommand(strSql) > 0 Then
                    dbconn.sConnCommitTrans()
                    Alert("Rekod telah dipadam!")
                    fBindGVSubModul(Trim(ddlKodModul2.SelectedValue.TrimEnd))
                    fResetSubModul()
                Else
                    dbconn.sConnRollbackTrans()
                End If
            Else
                Alert("Rekod tidak dapat dihapuskan! Terdapat rekod submenu dalam submodul ini.")
            End If


        Catch ex As Exception
            'fErrorLog("Jenis_Permohonan.aspx(gvJenPermohonan_RowDeleting)- " & ex.Message.ToString)
        End Try
    End Sub

    Protected Sub lbtnBaru_Click(sender As Object, e As EventArgs) Handles lbtnBaru.Click

        txtKodModul.Text = ""
            txtUrutanModul.Text = fGetNewUrutanModul()
            txtNamaModul.Text = ""
            txtNPModul.Text = ""

            txtKodModul.ReadOnly = False
            txtNamaModul.ReadOnly = False
            txtNPModul.ReadOnly = False
            txtNamaModul.ReadOnly = False

            ViewState("SaveMode") = "1"

    End Sub

    Protected Sub lbtnSimpanModul_Click(sender As Object, e As EventArgs) Handles lbtnSimpanModul.Click

        If ViewState("SaveMode") = "1" Then  'rekod baru
                Dim strKodModul As String = Trim(txtKodModul.Text.TrimEnd)
                If fCheckKodModul(strKodModul) = False Then
                    If fInsertModul() = True Then
                        Alert("Rekod baru telah ditambah!")
                        fBindGvModul()
                        fResetModul()
                    Else
                        Alert("Rekod baru gagal ditambah!")
                    End If
                Else
                    Alert("Kod Modul yang dimasukkan telah wujud! Sila masukkan Kod Modul lain.")
                End If

            ElseIf ViewState("SaveMode") = "2" Then 'kemas kini

                If fUpdateModul() = True Then
                    Alert("Rekod telah dikemas kini!")
                    fBindGvModul()
                    fResetModul()
                    ViewState("SaveMode") = "1"
                Else
                    Alert("Rekod gagal dikemas kini!")
                End If
            End If

    End Sub

    Protected Sub lbtnHapusModul_Click(sender As Object, e As EventArgs) Handles lbtnHapusModul.Click

        Dim strSql As String
        dbconn = New DBKewConn
        Dim strKodModul As String = Trim(txtKodModul.Text.ToString.TrimEnd)

            strSql = "select * from MK_USubModul where KodModul = '" & strKodModul & "'"
            If fCheckRec(strSql) = False Then
                strSql = "delete from MK_UModul where KodModul = '" & strKodModul & "'"
                dbconn.sConnBeginTrans()
                If dbconn.fUpdateCommand(strSql) > 0 Then
                    dbconn.sConnCommitTrans()
                    Alert("Rekod telah dipadam!")
                    fBindGvModul()
                    fResetModul()
                Else
                    dbconn.sConnRollbackTrans()
                    Alert("Rekod gagal dipadam!")
                End If

            Else
                Alert("Rekod tidak dapat dihapuskan! Terdapat submodul dalam modul ini.")
            End If

    End Sub
End Class