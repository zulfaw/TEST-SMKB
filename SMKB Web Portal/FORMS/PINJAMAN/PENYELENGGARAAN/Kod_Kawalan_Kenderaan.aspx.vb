Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Public Class Kod_Kawalan_Kenderaan
    Inherits System.Web.UI.Page

    Dim dbconn As New DBKewConn

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                fBindgvJenamaKereta()
                fResetJenamaKereta()
                'fBindgvModel()
                'fResetModel()
                'fBindGvBhnBakar()
                'fBindgvSukatSilinder()
                'fBindgvKuasaEnjin()
                'fBindgvKelasKend()



                TabContainer1.ActiveTab = tabModul
            End If

        Catch ex As Exception
            'fErrorLog("Pendaftaran_Menu.aspx(Page_Load())- " & ex.Message.ToString)
        End Try
    End Sub
    Private Sub fBindgvJenamaKereta()
        Using dt = fCreateJenamaKereta()

            'Dim strurutan As String = dt.Rows(dt.Rows.Count - 1)("Kod")
            'Dim intUrutan As String = CInt(strurutan)
            'intUrutan = intUrutan + 1
            'txtUrutanModul.Text = intUrutan

            gvJenamaKereta.DataSource = dt
            gvJenamaKereta.DataBind()

            lblJumMod.InnerText = dt.Rows.Count
        End Using
    End Sub
    Private Sub fBindgvSukatSilinder()
        Using dt = fCreateSukatSilinder()

            'Dim strurutan As String = dt.Rows(dt.Rows.Count - 1)("Kod")
            'Dim intUrutan As String = CInt(strurutan)
            'intUrutan = intUrutan + 1
            'txtUrutanModul.Text = intUrutan

            gvSukatSilinder.DataSource = dt
            gvSukatSilinder.DataBind()

            lblJumMod.InnerText = dt.Rows.Count
        End Using
    End Sub
    Private Sub fBindgvKuasaEnjin()
        Using dt = fCreateKuasaEnjin()

            'Dim strurutan As String = dt.Rows(dt.Rows.Count - 1)("Kod")
            'Dim intUrutan As String = CInt(strurutan)
            'intUrutan = intUrutan + 1
            'txtUrutanModul.Text = intUrutan

            gvKuasaEnjin.DataSource = dt
            gvKuasaEnjin.DataBind()

            lblJumMod.InnerText = dt.Rows.Count
        End Using
    End Sub
    Private Sub fBindgvKelasKend()
        Using dt = fCreateKelasKend()

            'Dim strurutan As String = dt.Rows(dt.Rows.Count - 1)("Kod")
            'Dim intUrutan As String = CInt(strurutan)
            'intUrutan = intUrutan + 1
            'txtUrutanModul.Text = intUrutan

            gvKelasKend.DataSource = dt
            gvKelasKend.DataBind()

            lblJumMod.InnerText = dt.Rows.Count
        End Using
    End Sub
    Private Sub fBindgvKendSediada()
        Using dt = fCreateKendSediada()

            gvKendSediada.DataSource = dt
            gvKendSediada.DataBind()

            lblJumMod.InnerText = dt.Rows.Count
        End Using
    End Sub
    Private Sub fBindgvModel()
        Using dt = fCreateModel()

            gvModel.DataSource = dt
            gvModel.DataBind()

            lblJumSubMod.InnerText = dt.Rows.Count
        End Using

    End Sub
    Private Sub fBindgvBuatan()
        Using dt = fCreateBuatan()

            gvBuatan.DataSource = dt
            gvBuatan.DataBind()

            lblJumSubMod.InnerText = dt.Rows.Count
        End Using

    End Sub

    Private Sub fBindGvBhnBakar()
        Using dt = fCreateBhnBakar()

            gvBhnBakar.DataSource = dt
            gvBhnBakar.DataBind()

            lblJumBhnBakar.InnerText = dt.Rows.Count
        End Using

    End Sub
    Private Function fCreateJenamaKereta() As DataTable
        Dim strSql As String = "select KodJenama, Butiran from PJM_JenamaKereta order by KodJenama"

        Using dt = dbconn.fSelectCommandDt(strSql)
            Return dt
        End Using
    End Function
    Private Function fCreateKendSediada() As DataTable
        Dim strSql As String = "select Kod, Butiran from PJM_KendSediada order by Kod"

        Using dt = dbconn.fSelectCommandDt(strSql)
            Return dt
        End Using
    End Function
    Private Function fCreateKelasKend() As DataTable
        Dim strSql As String = "select Kod, Butiran from PJM_KelasKend order by Kod"

        Using dt = dbconn.fSelectCommandDt(strSql)
            Return dt
        End Using
    End Function
    Private Function fCreateSukatSilinder() As DataTable
        Dim strSql As String = "select Kod, Butiran from PJM_SukatSilinder order by Kod"

        Using dt = dbconn.fSelectCommandDt(strSql)
            Return dt
        End Using
    End Function
    Private Function fCreateKuasaEnjin() As DataTable
        Dim strSql As String = "select Kod, Butiran from PJM_KuasaEnjin order by Kod"

        Using dt = dbconn.fSelectCommandDt(strSql)
            Return dt
        End Using
    End Function
    Private Function fCreateBhnBakar() As DataTable
        Dim strSql As String = "select kod, Butiran from PJM_BhnBakar  ORDER BY Kod"

        Using dt = dbconn.fSelectCommandDt(strSql)
            Return dt
        End Using

    End Function
    Private Function fCreateModel() As DataTable

        Dim strSql As String = "select KodModel, Butiran from PJM_Model order by KodModel"

        Using dt = dbconn.fSelectCommandDt(strSql)
            Return dt
        End Using
    End Function
    Private Function fCreateBuatan() As DataTable

        Dim strSql As String = "select Kod, Butiran from PJM_Buatan order by Kod"

        Using dt = dbconn.fSelectCommandDt(strSql)
            Return dt
        End Using
    End Function
    Protected Sub OnRowDataBoundGvBhnBakar(sender As Object, e As GridViewRowEventArgs) Handles gvBhnBakar.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvBhnBakar, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Klik untuk pilih rekod ini."
            e.Row.ForeColor = ColorTranslator.FromHtml("#000000")
        End If

    End Sub


    Private Function fUpdateModel() As Boolean

        Dim blnSuccess As Boolean = True
        Dim strSql As String
        dbconn = New DBKewConn
        Try
            Dim strKodModel As String = Trim(txtKodModel.Text.TrimEnd)
            Dim strButirModel As String = Trim(txtButirModel.Text.TrimEnd)

            strSql = "update PJM_Model set butiran = @butirModel where kod = @kodModel"
            Dim paramSql() As SqlParameter = {
                        New SqlParameter("@butirModel", strButirModel),
                        New SqlParameter("@kodModel", strKodModel)
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

    Private Function fUpdateBuatan() As Boolean

        Dim blnSuccess As Boolean = True
        Dim strSql As String
        dbconn = New DBKewConn
        Try
            'Dim strKodBuatan As String = Trim(txtKodBuatan.Text.TrimEnd)
            'Dim strButirBuatan As String = Trim(txtButirBuatan.Text.TrimEnd)

            'strSql = "update PJM_Skim set butiran = @butirBuatan where kod = @kodBuatan"
            'Dim paramSql() As SqlParameter = {
            '            New SqlParameter("@butirBuatan", strButirBuatan),
            '            New SqlParameter("@kodBuatan", strKodBuatan)
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

    Private Function fInsertJenamaKereta() As Boolean
        Try
            Dim strSql As String
            Dim strKodJenamaKereta As String = Trim(txtKodJenamaKereta.Text.TrimEnd)
            Dim strButirJenamaKereta As String = Trim(txtButirJenamaKereta.Text.ToUpper.TrimEnd)
            '            Dim strNamaPaparan As String = Trim(txtNPModul.Text.ToUpper.TrimEnd)
            'Dim strUrutan As String = Trim(txtUrutanModul.Text.TrimEnd)
            'Dim intStatus As Integer = rbStatModul.SelectedValue

            strSql = "insert into PJM_JenamaKereta (Kod, Butiran) values (@KodJenamaKereta,@ButirJenamaKereta)"

            Dim paramSql() As SqlParameter = {
                    New SqlParameter("@KodJenamaKereta", strKodJenamaKereta),
                    New SqlParameter("@ButirJenamaKereta", strButirJenamaKereta)
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

    Private Function fInsertModel() As Boolean
        dbconn = New DBKewConn
        Dim strSql As String

        Try
            Dim strKodModel As String = Trim(txtKodModel.Text.TrimEnd)
            Dim strButirModel As String = Trim(txtButirModel.Text.ToUpper.TrimEnd)

            dbconn.sConnBeginTrans()

            strSql = "insert into PJM_Model (Kod , Butiran) " &
            "values (@KodModel,@ButirModel)"
            Dim paramSql() As SqlParameter = {
                           New SqlParameter("@KodModel", strKodModel),
                           New SqlParameter("@ButirModel", strButirModel)
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

    Private Function fInsertBuatan() As Boolean
        dbconn = New DBKewConn
        Dim strSql As String

        Try
            Dim strKodBuatan As String = Trim(txtKodBuatan.Text.TrimEnd)
            Dim strButirBuatan As String = Trim(txtButirBuatan.Text.ToUpper.TrimEnd)

            dbconn.sConnBeginTrans()

            strSql = "insert into PJM_Buatan (Kod , Butiran) " &
            "values (@KodBuatan,@ButirBuatan)"
            Dim paramSql() As SqlParameter = {
                           New SqlParameter("@KodBuatan", strKodBuatan),
                           New SqlParameter("@ButirBuatan", strButirBuatan)
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

    Private Sub gvJenamaKereta_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvJenamaKereta.SelectedIndexChanged
        For Each row As GridViewRow In gvJenamaKereta.Rows
            If row.RowIndex = gvJenamaKereta.SelectedIndex Then
                row.ForeColor = ColorTranslator.FromHtml("#0000FF")
                'row.ToolTip = String.Empty
            Else
                row.ForeColor = ColorTranslator.FromHtml("#000000")
                ' row.ToolTip = "Klik untuk pilih rekod ini."
            End If
        Next
    End Sub





    Private Sub gvJenamaKereta_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvJenamaKereta.PageIndexChanging

        gvJenamaKereta.PageIndex = e.NewPageIndex
        If Session("SortedView") IsNot Nothing Then
            gvJenamaKereta.DataSource = Session("SortedView")
            gvJenamaKereta.DataBind()
        Else

            Dim dt As New DataTable
            dt = fCreateJenamaKereta()
            gvJenamaKereta.DataSource = dt
            gvJenamaKereta.DataBind()
        End If

    End Sub
    Private Sub gvKelasKend_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvKelasKend.PageIndexChanging

        gvKelasKend.PageIndex = e.NewPageIndex
        If Session("SortedView") IsNot Nothing Then
            gvKelasKend.DataSource = Session("SortedView")
            gvKelasKend.DataBind()
        Else

            Dim dt As New DataTable
            dt = fCreateKelasKend()
            gvKelasKend.DataSource = dt
            gvKelasKend.DataBind()
        End If

    End Sub

    Private Sub gvKendSediada_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvKendSediada.PageIndexChanging

        gvKendSediada.PageIndex = e.NewPageIndex
        If Session("SortedView") IsNot Nothing Then
            gvKendSediada.DataSource = Session("SortedView")
            gvKendSediada.DataBind()
        Else

            Dim dt As New DataTable
            dt = fCreateKendSediada()
            gvKendSediada.DataSource = dt
            gvKendSediada.DataBind()
        End If

    End Sub
    Private Sub gvJenamaKereta_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvJenamaKereta.Sorting

        Dim sortingDirection As String = String.Empty
        If direction = SortDirection.Ascending Then
            direction = SortDirection.Descending
            sortingDirection = "Desc"
        Else
            direction = SortDirection.Ascending
            sortingDirection = "Asc"
        End If

        Dim sortedView As New DataView(fCreateJenamaKereta())
        sortedView.Sort = Convert.ToString(e.SortExpression + " ") & sortingDirection
        Session("SortedView") = sortedView
        gvJenamaKereta.DataSource = sortedView
        gvJenamaKereta.DataBind()

    End Sub
    Private Sub gvKelasKend_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvKelasKend.Sorting

        Dim sortingDirection As String = String.Empty
        If direction = SortDirection.Ascending Then
            direction = SortDirection.Descending
            sortingDirection = "Desc"
        Else
            direction = SortDirection.Ascending
            sortingDirection = "Asc"
        End If

        Dim sortedView As New DataView(fCreateKelasKend())
        sortedView.Sort = Convert.ToString(e.SortExpression + " ") & sortingDirection
        Session("SortedView") = sortedView
        gvKelasKend.DataSource = sortedView
        gvKelasKend.DataBind()

    End Sub

    Private Sub gvKendSediada_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvKendSediada.Sorting

        Dim sortingDirection As String = String.Empty
        If direction = SortDirection.Ascending Then
            direction = SortDirection.Descending
            sortingDirection = "Desc"
        Else
            direction = SortDirection.Ascending
            sortingDirection = "Asc"
        End If

        Dim sortedView As New DataView(fCreateKendSediada())
        sortedView.Sort = Convert.ToString(e.SortExpression + " ") & sortingDirection
        Session("SortedView") = sortedView
        gvKendSediada.DataSource = sortedView
        gvKendSediada.DataBind()

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

    Private Function fCheckKodJenamaKereta(ByVal strKodJenamaKereta As String) As Boolean
        Dim strSql = "select count(*) from PJM_JenamaKereta  where Kod = '" & strKodJenamaKereta & "'"
        Dim intCnt As Integer = dbconn.fSelectCount(strSql)
        If intCnt < 1 Then
            Return False
        Else
            Return True
        End If

    End Function
    Private Function fCheckKodKelasKend(ByVal strKodKelasKend As String) As Boolean
        Dim strSql = "select count(*) from PJM_KelasKend  where Kod = '" & strKodKelasKend & "'"
        Dim intCnt As Integer = dbconn.fSelectCount(strSql)
        If intCnt < 1 Then
            Return False
        Else
            Return True
        End If

    End Function
    Private Function fCheckKodKendSediada(ByVal strKodKendSediada As String) As Boolean
        Dim strSql = "select count(*) from PJM_KendSediada  where Kod = '" & strKodKendSediada & "'"
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



    Private Sub gvModel_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvModel.RowDeleting
        Try

            dbconn = New DBKewConn
            Dim strkodModel As String

            Dim row As GridViewRow = DirectCast(gvModel.Rows(e.RowIndex), GridViewRow)
            strkodModel = Trim(row.Cells(1).Text.ToString.TrimEnd)


        Catch ex As Exception
            'fErrorLog("Jenis_Permohonan.aspx(gvJenPermohonan_RowDeleting)- " & ex.Message.ToString)
        End Try
    End Sub
    Private Sub gvBuatan_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvBuatan.RowDeleting
        Try

            dbconn = New DBKewConn
            Dim strkodBuatan As String

            Dim row As GridViewRow = DirectCast(gvBuatan.Rows(e.RowIndex), GridViewRow)
            strkodBuatan = Trim(row.Cells(1).Text.ToString.TrimEnd)


        Catch ex As Exception
            'fErrorLog("Jenis_Permohonan.aspx(gvJenPermohonan_RowDeleting)- " & ex.Message.ToString)
        End Try
    End Sub

    Private Sub txtKodJenamaKereta_TextChanged(sender As Object, e As EventArgs) Handles txtKodJenamaKereta.TextChanged
        Dim digitsOnly As Regex = New Regex("[^\d]")
        txtKodJenamaKereta.Text = digitsOnly.Replace(txtKodJenamaKereta.Text, "")
    End Sub

    Private Sub gvJenamaKereta_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvJenamaKereta.RowCommand
        Try
            If e.CommandName = "Select" Then
                '               Dim intStatus As Integer
                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvJenamaKereta.Rows(index)

                Dim strKodJenamaKereta As String = TryCast(selectedRow.FindControl("lblKodJenamaKereta"), Label).Text.ToString
                Dim strButirJenamaKereta As String = selectedRow.Cells(2).Text
                Dim strNamaPaparan As String = selectedRow.Cells(3).Text
                '                Dim strUrutan As String = TryCast(selectedRow.FindControl("lblUrutan"), Label).Text.ToString
                '                ViewState("Urutan") = strUrutan
                '                Dim strStatus As String = TryCast(selectedRow.FindControl("lblStatus"), Label).Text.ToString

                txtKodJenamaKereta.Text = strKodJenamaKereta
                txtButirJenamaKereta.Text = strButirJenamaKereta
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

    Private Function fUpdateJenamaKereta() As Boolean
        Dim blnSuccess As Boolean = True
        Dim strSql As String
        dbconn = New DBKewConn
        Try

            Dim strKodJenamaKereta As String = Trim(txtKodJenamaKereta.Text.TrimEnd)
            Dim strButirJenamaKereta As String = Trim(txtButirJenamaKereta.Text.TrimEnd)
            'Dim strNamaPaparan As String = Trim(txtNPModul.Text.TrimEnd)
            '            Dim strUrutan As String = Trim(txtUrutanModul.Text.TrimEnd)
            '            Dim intStatus As Integer = rbStatModul.SelectedValue

            '            If Not strUrutan = ViewState("Urutan") Then
            '            dbconn.sConnBeginTrans()
            '            Dim strKodJenamaKereta2 As String = fGetKodJenamaKereta(strUrutan)
            '            If Not strKodJenamaKereta2 Is Nothing Then
            '            strSql = "update MK_UModul set Urutan = @Urutan where KodJenamaKereta = @strKodJenamaKereta2"
            '            Dim paramSql2() As SqlParameter = {
            '                                New SqlParameter("@Urutan", ViewState("Urutan")),
            '                                New SqlParameter("@strKodJenamaKereta2", strKodJenamaKereta2)
            '                                }

            '            If Not dbconn.fUpdateCommand(strSql, paramSql2) > 0 Then
            '            blnSuccess = False
            '            End If
            '            End If
            '            End If

            strSql = "update PJM_JenamaKereta set Butiran = @ButirJenamaKereta where Kod = @KodJenamaKereta"
            Dim paramSql() As SqlParameter = {
                        New SqlParameter("@ButirJenamaKereta", strButirJenamaKereta)}

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

    Private Function fGetKodJenamaKereta(ByVal strUrutan As String) As String

        '        Dim strKodModel As String = ""

        '        Dim strSql As String = "select Kod from PJM_JenamaKereta with (nolock) where Kod = " & strUrutan & ""
        '        dbconn.sSelectCommand(strSql, strKodModel)

        '        Return strKodModel

    End Function
    Private Sub txtKodKelasKend_TextChanged(sender As Object, e As EventArgs) Handles txtkodKelasKend.TextChanged
        Dim digitsOnly As Regex = New Regex("[^\d]")
        txtkodKelasKend.Text = digitsOnly.Replace(txtkodKelasKend.Text, "")
    End Sub

    Private Sub txtKodKendSediada_TextChanged(sender As Object, e As EventArgs) Handles txtKodKendSediada.TextChanged
        Dim digitsOnly As Regex = New Regex("[^\d]")
        txtKodKendSediada.Text = digitsOnly.Replace(txtKodKendSediada.Text, "")
    End Sub

    Protected Sub ddlSaizRekod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSaizRekod.SelectedIndexChanged
        gvKelasKend.PageSize = CInt(ddlSaizRekod.SelectedValue)
        fBindgvKelasKend()
    End Sub


    Private Sub gvKelasKend_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvKelasKend.RowCommand
        Try
            If e.CommandName = "Select" Then
                '               Dim intStatus As Integer
                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvKelasKend.Rows(index)

                Dim strKodKelasKend As String = TryCast(selectedRow.FindControl("lblKodKelasKend"), Label).Text.ToString
                Dim strButirKelasKend As String = selectedRow.Cells(2).Text
                Dim strNamaPaparan As String = selectedRow.Cells(3).Text
                '                Dim strUrutan As String = TryCast(selectedRow.FindControl("lblUrutan"), Label).Text.ToString
                '                ViewState("Urutan") = strUrutan
                '                Dim strStatus As String = TryCast(selectedRow.FindControl("lblStatus"), Label).Text.ToString

                txtKodKelasKend.Text = strKodKelasKend
                txtButirKelasKend.Text = strButirKelasKend
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
    Private Sub gvKendSediada_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvKendSediada.RowCommand
        Try
            If e.CommandName = "Select" Then
                '               Dim intStatus As Integer
                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvKendSediada.Rows(index)

                Dim strKodKendSediada As String = TryCast(selectedRow.FindControl("lblKodKendSediada"), Label).Text.ToString
                Dim strButirKendSediada As String = selectedRow.Cells(2).Text
                Dim strNamaPaparan As String = selectedRow.Cells(3).Text
                '                Dim strUrutan As String = TryCast(selectedRow.FindControl("lblUrutan"), Label).Text.ToString
                '                ViewState("Urutan") = strUrutan
                '                Dim strStatus As String = TryCast(selectedRow.FindControl("lblStatus"), Label).Text.ToString

                txtKodKendSediada.Text = strKodKendSediada
                txtButirKendSediada.Text = strButirKendSediada
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

    Private Function fUpdateKelasKend() As Boolean
        Dim blnSuccess As Boolean = True
        Dim strSql As String
        dbconn = New DBKewConn
        Try

            Dim strKodKelasKend As String = Trim(txtKodKelasKend.Text.TrimEnd)
            Dim strButirKelasKend As String = Trim(txtButirKelasKend.Text.TrimEnd)
            'Dim strNamaPaparan As String = Trim(txtNPModul.Text.TrimEnd)
            '            Dim strUrutan As String = Trim(txtUrutanModul.Text.TrimEnd)
            '            Dim intStatus As Integer = rbStatModul.SelectedValue

            '            If Not strUrutan = ViewState("Urutan") Then
            '            dbconn.sConnBeginTrans()
            '            Dim strKodKelasKend2 As String = fGetKodKelasKend(strUrutan)
            '            If Not strKodKelasKend2 Is Nothing Then
            '            strSql = "update MK_UModul set Urutan = @Urutan where KodKelasKend = @strKodKelasKend2"
            '            Dim paramSql2() As SqlParameter = {
            '                                New SqlParameter("@Urutan", ViewState("Urutan")),
            '                                New SqlParameter("@strKodKelasKend2", strKodKelasKend2)
            '                                }

            '            If Not dbconn.fUpdateCommand(strSql, paramSql2) > 0 Then
            '            blnSuccess = False
            '            End If
            '            End If
            '            End If

            strSql = "update PJM_KelasKend set Butiran = @ButirKelasKend where Kod = @KodKelasKend"
            Dim paramSql() As SqlParameter = {
                        New SqlParameter("@ButirKelasKend", strButirKelasKend)}

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
    Private Function fUpdateKendSediada() As Boolean
        Dim blnSuccess As Boolean = True
        Dim strSql As String
        dbconn = New DBKewConn
        Try

            Dim strKodKendSediada As String = Trim(txtKodKendSediada.Text.TrimEnd)
            Dim strButirKendSediada As String = Trim(txtButirKendSediada.Text.TrimEnd)
            'Dim strNamaPaparan As String = Trim(txtNPModul.Text.TrimEnd)
            '            Dim strUrutan As String = Trim(txtUrutanModul.Text.TrimEnd)
            '            Dim intStatus As Integer = rbStatModul.SelectedValue

            '            If Not strUrutan = ViewState("Urutan") Then
            '            dbconn.sConnBeginTrans()
            '            Dim strKodKendSediada2 As String = fGetKodKendSediada(strUrutan)
            '            If Not strKodKendSediada2 Is Nothing Then
            '            strSql = "update MK_UModul set Urutan = @Urutan where KodKendSediada = @strKodKendSediada2"
            '            Dim paramSql2() As SqlParameter = {
            '                                New SqlParameter("@Urutan", ViewState("Urutan")),
            '                                New SqlParameter("@strKodKendSediada2", strKodKendSediada2)
            '                                }

            '            If Not dbconn.fUpdateCommand(strSql, paramSql2) > 0 Then
            '            blnSuccess = False
            '            End If
            '            End If
            '            End If

            strSql = "update PJM_KendSediada set Butiran = @ButirKendSediada where Kod = @KodKendSediada"
            Dim paramSql() As SqlParameter = {
                        New SqlParameter("@ButirKendSediada", strButirKendSediada)}

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

    Private Function fGetKodKelasKend(ByVal strUrutan As String) As String

        '        Dim strKodModel As String = ""

        '        Dim strSql As String = "select Kod from PJM_KelasKend with (nolock) where Kod = " & strUrutan & ""
        '        dbconn.sSelectCommand(strSql, strKodModel)

        '        Return strKodModel

    End Function
    Private Function fGetKodModel(ByVal strKodstrKodBhnBakar As String, ByVal strUrutan As String) As String

        Dim strKodModel As String = ""

        Dim strSql As String = "select Kod from PJM_Model with (nolock) where Kod = '" & strKodstrKodBhnBakar & "'"
        dbconn.sSelectCommand(strSql, strKodModel)
        Return strKodModel

    End Function
    Private Function fGetKodBuatan(ByVal strKodJenamaKereta As String, ByVal strUrutan As String) As String

        Dim strKodBuatan As String = ""

        Dim strSql As String = "select Kod from PJM_Buatan with (nolock) where Kod  = '" & strKodJenamaKereta & "'"
        dbconn.sSelectCommand(strSql, strKodBuatan)
        Return strKodBuatan

    End Function

    Private Function fGetKodBhnBakar(ByVal strKodBhnBakar As String, ByVal strUrutan As String) As String
        Dim KodBhnBakar As String = ""
        Dim strSql As String = "select kod, Butiran from PJM_BhnBakar where kod = '" & strKodBhnBakar & "'"
        dbconn.sSelectCommand(strSql, KodBhnBakar)
        Return KodBhnBakar
    End Function

    Private Function fResetJenamaKereta()
        txtKodJenamaKereta.Text = ""
        txtButirJenamaKereta.Text = ""
        txtKodJenamaKereta.ReadOnly = True
        txtButirJenamaKereta.ReadOnly = True

    End Function
    Private Function fResetKelasKend()
        txtKodKelasKend.Text = ""
        txtButirKelasKend.Text = ""
        txtKodKelasKend.ReadOnly = True
        txtButirKelasKend.ReadOnly = True

    End Function
    Private Function fResetKendSediada()
        txtKodKendSediada.Text = ""
        txtButirKendSediada.Text = ""
        txtKodKendSediada.ReadOnly = True
        txtButirKendSediada.ReadOnly = True

    End Function

    Private Function fResetModel()
        txtKodModel.Text = ""
        txtButirModel.Text = ""
        txtButirModel.ReadOnly = True
    End Function

    Private Function fResetBuatan()
        txtKodBuatan.Text = ""
        txtButirBuatan.Text = ""
        txtButirBuatan.ReadOnly = True
    End Function
    Private Function fResetSukatSilinder()
        txtKodSukatSilinder.Text = ""
        txtButirSukatSilinder.Text = ""
        txtButirSukatSilinder.ReadOnly = True
    End Function
    Private Function fResetKuasaEnjin()
        txtKodKuasaEnjin.Text = ""
        txtButirKuasaEnjin.Text = ""
        txtButirKuasaEnjin.ReadOnly = True
    End Function
    Private Sub TabContainer1_ActiveTabChanged(sender As Object, e As EventArgs) Handles TabContainer1.ActiveTabChanged
        Select Case TabContainer1.ActiveTabIndex
            Case 0

                fBindgvJenamaKereta()
                fResetJenamaKereta()
            Case 1
                fBindgvModel()
                fResetModel()
            Case 2
                fBindGvBhnBakar()
            Case 3
                fBindgvBuatan()
                fResetBuatan()
            Case 4
                fBindgvSukatSilinder()
                fResetSukatSilinder()
            Case 5
                fBindgvKuasaEnjin()
                fResetKuasaEnjin()
            Case 6
                fBindgvKelasKend()
                fResetKelasKend()
            Case 7
                fBindgvKendSediada()
                fResetKendSediada()

        End Select

        ViewState("SaveMode") = 1
    End Sub

    Private Sub gvModel_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvModel.RowCommand
        Try
            If e.CommandName = "Select" Then

                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvModel.Rows(index)

                Dim strKodModel As String = TryCast(selectedRow.FindControl("lblKodModel"), Label).Text.ToString ' selectedRow.Cells(1).Text
                Dim strButirModel As String = selectedRow.Cells(2).Text

                txtKodModel.Text = strKodModel
                txtButirModel.Text = strButirModel

                ViewState("SaveMode") = "2"
                txtKodModel.ReadOnly = True

                txtButirModel.ReadOnly = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub gvBuatan_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvBuatan.RowCommand
        Try
            If e.CommandName = "Select" Then

                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvBuatan.Rows(index)

                Dim strKodBuatan As String = TryCast(selectedRow.FindControl("lblKodBuatan"), Label).Text.ToString ' selectedRow.Cells(1).Text
                Dim strButirBuatan As String = selectedRow.Cells(2).Text

                txtKodBuatan.Text = strKodBuatan
                txtButirBuatan.Text = strButirBuatan

                ViewState("SaveMode") = "2"
                txtKodBuatan.ReadOnly = True

                txtButirBuatan.ReadOnly = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub lbtnBaruModel_Click(sender As Object, e As EventArgs) Handles lbtnBaruModel.Click

        Dim strKodModel As String
        txtButirModel.Text = ""
        txtButirModel.ReadOnly = False
        txtKodModel.Text = ""

        ViewState("SaveMode") = "1"

    End Sub
    Protected Sub lbtnBaruBuatan_Click(sender As Object, e As EventArgs) Handles lbtnBaruBuatan.Click

        Dim strKodBuatan As String
        txtButirBuatan.Text = ""
        txtButirBuatan.ReadOnly = False
        txtKodBuatan.Text = ""

        ViewState("SaveMode") = "1"

    End Sub
    Protected Sub lbtnSimpanModel_Click(sender As Object, e As EventArgs) Handles lbtnSimpanModel.Click

        'Dim strKodModel As String
        'If ViewState("SaveMode") = "1" Then  'rekod baru
        '    Dim strKodModel As String = Trim(txtKodModel.Text.TrimEnd)
        '    'If fCheckKodJenamaKereta(strKodModel) = False Then
        '    '    If fInsertJenamaKereta() = True Then
        '    '        Alert("Rekod baru telah ditambah!")
        '    '        'fBindgvModel()
        '    '        fResetModel()
        '    '    Else
        '    '        Alert("Rekod baru gagal ditambah!")
        '    '    End If
        '    'Else
        '    '    Alert("Kod Modul yang dimasukkan telah wujud! Sila masukkan Kod Modul lain.")
        '    'End If


        'ElseIf ViewState("SaveMode") = "2" Then 'kemas kini
        '    If fUpdateModel() = True Then
        '        Alert("Rekod telah dikemas kini!")
        '        fBindgvModel(strKodModel)
        '        ViewState("SaveMode") = "1"
        '    Else
        '        Alert("Rekod gagal dikemas kini!")
        '    End If

        'End If

    End Sub




    Private Sub lbtnHapusModel_Click(sender As Object, e As EventArgs) Handles lbtnHapusModel.Click
        Try
            Dim strSql As String
            dbconn = New DBKewConn
            Dim strKodModel As String

            strKodModel = Trim(txtKodModel.Text.ToString.TrimEnd)

            strSql = "delete from PJM_JenisPinj where Kod = '" & strKodModel & "'"
            dbconn.sConnBeginTrans()
            If dbconn.fUpdateCommand(strSql) > 0 Then
                dbconn.sConnCommitTrans()
                Alert("Rekod telah dipadam!")
                fBindgvModel()
                fResetModel()
            Else
                dbconn.sConnRollbackTrans()
            End If



        Catch ex As Exception
            'fErrorLog("Jenis_Permohonan.aspx(gvJenPermohonan_RowDeleting)- " & ex.Message.ToString)
        End Try
    End Sub

    Protected Sub lbtnBaru_Click(sender As Object, e As EventArgs) Handles lbtnBaru.Click

        txtKodJenamaKereta.Text = ""
        '        txtUrutanModul.Text = fGetNewUrutanModul()
        txtButirJenamaKereta.Text = ""
        '        txtNPModul.Text = ""

        txtKodJenamaKereta.ReadOnly = False
        txtButirJenamaKereta.ReadOnly = False
        'txtNPModul.ReadOnly = False
        txtButirJenamaKereta.ReadOnly = False

        ViewState("SaveMode") = "1"

    End Sub


End Class