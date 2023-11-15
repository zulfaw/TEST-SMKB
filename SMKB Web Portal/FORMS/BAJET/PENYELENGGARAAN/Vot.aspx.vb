Imports System.Drawing
Imports System.Data.SqlClient

Public Class Vot
    Inherits System.Web.UI.Page

    Private Shared dsVot As New DataSet
    Private Shared dvVot As New DataView
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ViewState("SaveMode") = 1
            fBindDdlKlasifikasi()
            fBindDdlJenis()

            fBindGvVot("")

        End If
    End Sub

    Private Sub sClearGvVot()
        Try
            gvVot.DataSource = New List(Of String)
            gvVot.DataBind()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub fBindDdlKlasifikasi()

        Try
            ddlKlasifikasi.Items.Add(New ListItem("-SILA PILIH-", "0"))
            ddlKlasifikasi.Items.Add(New ListItem("H1", "H1"))
            ddlKlasifikasi.Items.Add(New ListItem("H2", "H2"))
            ddlKlasifikasi.Items.Add(New ListItem("H3", "H3"))
            ddlKlasifikasi.Items.Add(New ListItem("D", "D"))
            ddlKlasifikasi.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindDdlJenis()

        Try
            Dim strSql As String = "select KodJen, (KodJen + ' - ' + Butiran) as Butiran from mk_jenvot order by KodJen"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fselectCommand(strSql)

            ddlJenis.DataSource = ds
            ddlJenis.DataTextField = "Butiran"
            ddlJenis.DataValueField = "KodJen"
            ddlJenis.DataBind()

            ddlJenis.Items.Insert(0, New ListItem("-SILA PILIH-", "0"))
            ddlJenis.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub
    Private Function fBindGvVot(ByVal strKodVot As String)

        Try
            Dim dt As New DataTable
            dt = fCreateDtVot(strKodVot)

            If dt.Rows.Count = 0 Then
                gvVot.DataSource = New List(Of String)
            Else
                gvVot.DataSource = dt
            End If

            lblJumRekod.InnerText = dt.Rows.Count
            gvVot.DataBind()


        Catch ex As Exception

        End Try

    End Function

    Private Function fCreateDtVot(ByVal strFilKodVot As String) As DataTable
        Try
            Dim strSql As String
            Dim strFilter As String
            Dim strKodVot, strButiran, strStatus, strKlasifikasi, strJenis As String

            Dim dt As New DataTable
            dt.Columns.Add("KodVot", GetType(String))
            dt.Columns.Add("Butiran", GetType(String))
            dt.Columns.Add("Klasifikasi", GetType(String))
            dt.Columns.Add("Jenis", GetType(String))

            If strFilKodVot <> "" Then
                strFilter = "Where kodvot like '" & strFilKodVot & "%'"
            Else
                strFilter = ""
            End If
            strSql = "select kodvot, Butiran, Klasifikasi , KodJen from mk_vot " & strFilter & " order by kodvot"

            Dim dbconn As New DBKewConn
            dsVot = dbconn.fselectCommand(strSql)
            dvVot = New DataView(dsVot.Tables(0))

            For i As Integer = 0 To dsVot.Tables(0).Rows.Count - 1

                strKodVot = dsVot.Tables(0).Rows(i)(0).ToString
                strButiran = dsVot.Tables(0).Rows(i)(1).ToString
                strKlasifikasi = dsVot.Tables(0).Rows(i)(2).ToString
                strJenis = dsVot.Tables(0).Rows(i)(3).ToString

                dt.Rows.Add(strKodVot, strButiran, strKlasifikasi, strJenis)
            Next
            ViewState("dtVot") = dt
            Return dt

        Catch ex As Exception

        End Try
    End Function



    Private Sub gvVot_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvVot.SelectedIndexChanged
        Try
            For Each row As GridViewRow In gvVot.Rows
                If row.RowIndex = gvVot.SelectedIndex Then
                    row.ForeColor = ColorTranslator.FromHtml("#0000FF")
                Else
                    row.ForeColor = ColorTranslator.FromHtml("#000000")
                End If
            Next

        Catch ex As Exception

        End Try
    End Sub



    Protected Sub gvVot_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvVot.PageIndexChanging
        Try
            gvVot.PageIndex = e.NewPageIndex
            If ViewState("dtVot") IsNot Nothing Then
                gvVot.DataSource = ViewState("dtVot")
                gvVot.DataBind()
            Else
                Dim dt As New DataTable
                dt = fCreateDtVot("")
                gvVot.DataSource = dt
                gvVot.DataBind()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvVot_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvVot.RowCommand

        Try
            If e.CommandName = "Select" Then
                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvVot.Rows(index)

                Dim strKodVot As String = selectedRow.Cells(1).Text
                Dim strButiran As String = selectedRow.Cells(2).Text
                Dim strKlasifikasi As String = selectedRow.Cells(3).Text
                Dim strJenis As String = selectedRow.Cells(4).Text
                Dim intStatus As Integer

                txtKodVot.Text = strKodVot
                txtButiran.Text = strButiran
                ddlKlasifikasi.SelectedValue = strKlasifikasi
                ddlJenis.SelectedValue = strJenis

                ViewState("SaveMode") = 2 'Edit

                txtKodVot.ReadOnly = True
                txtKodVot.BackColor = ColorTranslator.FromHtml("#FFFFCC")
                lbtnHapus.Visible = True
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fReset()
        Try
            txtKodVot.Text = ""
            txtKodVot.ReadOnly = False
            txtKodVot.BackColor = ColorTranslator.FromHtml("#FFFFFF")
            txtButiran.Text = ""
            ddlJenis.SelectedIndex = 0
            ddlKlasifikasi.SelectedIndex = 0
            lbtnHapus.Visible = False
            For Each row As GridViewRow In gvVot.Rows
                row.ForeColor = ColorTranslator.FromHtml("#000000")
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlSaizRekod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSaizRekod.SelectedIndexChanged
        Try
            gvVot.PageSize = CInt(ddlSaizRekod.Text)
            fBindGvVot("")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub lbtnHapus_Click(sender As Object, e As EventArgs) Handles lbtnHapus.Click
        Try
            Dim strSql As String
            Dim dbconn As New DBKewConn
            Dim strKodVot As String

            strKodVot = Trim(txtKodVot.Text.TrimEnd)

            strSql = "delete from mk_vot where KodVot = '" & strKodVot & "'"
            If dbconn.fUpdateCommand(strSql) > 0 Then
                fGlobalAlert("Rekod telah dihapuskan!", Me.Page, Me.[GetType]())
                fReset()
                fBindGvVot("")
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.Click
        Dim dbconn As New DBKewConn
        Dim strSql As String
        Try


            Dim strKodVot As String = Trim(txtKodVot.Text.ToUpper.TrimEnd)
            Dim strButiran As String = Trim(txtButiran.Text.ToUpper.TrimEnd)
            Dim strKlasifikasi As String = Trim(ddlKlasifikasi.SelectedItem.ToString.TrimEnd)
            Dim strJenis As String = Trim(ddlJenis.SelectedValue.ToString.TrimEnd)

            If ViewState("SaveMode") = 1 Then
                'INSERT

                strSql = "select count(*) from mk_vot where kodvot = '" & strKodVot & "'"
                If fCheckRec(strSql) = 0 Then
                    strSql = "insert into MK_Vot (KodVot, Butiran, Klasifikasi, KodJen) values (@KodVot,@Butiran,@Klasifikasi,@KodJen)"

                    Dim paramSql() As SqlParameter = {
                        New SqlParameter("@KodVot", strKodVot),
                        New SqlParameter("@Butiran", strButiran),
                        New SqlParameter("@Klasifikasi", strKlasifikasi),
                        New SqlParameter("@KodJen", strJenis)
                    }

                    dbconn.sConnBeginTrans()
                    If dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                        dbconn.sConnCommitTrans()
                        fGlobalAlert("Rekod telah disimpan!", Me.Page, Me.[GetType]())
                        fBindGvVot("")
                        fReset()
                    Else
                        dbconn.sConnRollbackTrans()
                        fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
                    End If
                Else
                    fGlobalAlert("Kod Vot yang dimasukkan telah wujud! Sila masukkan Kod Vot lain.", Me.Page, Me.[GetType]())
                    Exit Sub
                End If

            ElseIf ViewState("SaveMode") = 2 Then
                'UPDATE
                strSql = "update MK_Vot set Butiran=@Butiran,Klasifikasi= @Klasifikasi,KodJen = @KodJen where kodvot = @KodVot"
                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@KodVot", strKodVot),
                            New SqlParameter("@Butiran", strButiran),
                            New SqlParameter("@Klasifikasi", strKlasifikasi),
                            New SqlParameter("@KodJen", strJenis)
                            }

                dbconn.sConnBeginTrans()
                If dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                    dbconn.sConnCommitTrans()
                    fGlobalAlert("Rekod telah dikemas kini!", Me.Page, Me.[GetType]())
                    fReset()
                    fBindGvVot("")
                Else
                    dbconn.sConnRollbackTrans()
                    fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
                End If
            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Sub lbtnBaru_Click(sender As Object, e As EventArgs) Handles lbtnBaru.Click
        fReset()
        ViewState("SaveMode") = 1
    End Sub

    Private Sub ddlCarian_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCarian.SelectedIndexChanged
        If ddlCarian.SelectedValue = 0 Then
            txtCarian.Visible = False
        Else
            txtCarian.Visible = True
        End If
    End Sub

    Private Sub btnCarian_ServerClick(sender As Object, e As EventArgs) Handles btnCarian.ServerClick
        If ddlCarian.SelectedValue = 0 Then
            fBindGvVot("")
        Else
            fBindGvVot(Trim(txtCarian.Text.TrimEnd))
        End If
    End Sub
End Class