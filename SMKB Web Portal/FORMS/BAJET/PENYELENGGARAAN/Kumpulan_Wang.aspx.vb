Imports System.Drawing
Imports System.Data.SqlClient

Public Class KW
    Inherits System.Web.UI.Page

    Public dsKodKW As New DataSet
    Public dvKodKW As New DataView
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            ViewState("SaveMode") = 1 'New

            fBindGvKW()

        End If
    End Sub

    Private Function fBindGvKW()

        Try
            Dim dt As New DataTable
            dt = fCreateDtKW()

            If dt.Rows.Count = 0 Then
                gvKW.DataSource = New List(Of String)
            Else
                gvKW.DataSource = dt
            End If
            gvKW.DataBind()
            lblJumRekod.InnerText = dt.Rows.Count
        Catch ex As Exception

        End Try

    End Function

    Private Function fCreateDtKW() As DataTable
        Dim blnStatus As Boolean
        Dim strStatus As String
        Try

            Dim dt As New DataTable
            dt.Columns.Add("KodKW", GetType(String))
            dt.Columns.Add("ButiranKW", GetType(String))
            dt.Columns.Add("Status", GetType(String))

            Dim strKodKW As String
            Dim strButiranKW As String

            Dim strSql As String = " select KodKw, Butiran, Status from MK_Kw order by KodKw"

            Dim dbconn As New DBKewConn
            dsKodKW = dbconn.fselectCommand(strSql)
            dvKodKW = New DataView(dsKodKW.Tables(0))

            For i As Integer = 0 To dsKodKW.Tables(0).Rows.Count - 1

                strKodKW = dsKodKW.Tables(0).Rows(i)(0).ToString
                strButiranKW = dsKodKW.Tables(0).Rows(i)(1).ToString
                blnStatus = dsKodKW.Tables(0).Rows(i)(2).ToString
                If blnStatus = True Then strStatus = "Aktif" Else strStatus = "Tidak Aktif"
                dt.Rows.Add(strKodKW, strButiranKW, strStatus)
            Next

            Return dt

        Catch ex As Exception

        End Try

    End Function

    Private Sub fReset()
        Try
            txtKodKW.Text = ""
            txtKodKW.ReadOnly = False
            txtKodKW.BackColor = ColorTranslator.FromHtml("#FFFFFF")
            txtButiran.Text = ""
            rbStatus.SelectedIndex = 0

            For Each row As GridViewRow In gvKW.Rows
                row.ForeColor = ColorTranslator.FromHtml("#000000")
            Next
            lbtnHapus.Visible = False
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub gvKW_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvKW.RowCommand
        Try
            If e.CommandName = "Select" Then
                Dim intStatus As Integer

                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvKW.Rows(index)

                Dim strKod As String = selectedRow.Cells(1).Text
                Dim strButiran As String = selectedRow.Cells(2).Text
                Dim strStatus As String = selectedRow.Cells(3).Text
                If strStatus = "Aktif" Then intStatus = 1 Else intStatus = 0
                txtKodKW.Text = strKod
                txtButiran.Text = strButiran
                rbStatus.SelectedValue = intStatus
                ViewState("SaveMode") = 2 'Edit


                txtKodKW.ReadOnly = True
                txtKodKW.BackColor = ColorTranslator.FromHtml("#FFFFCC")
                lbtnHapus.Visible = True
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvKW_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvKW.SelectedIndexChanged
        Try
            For Each row As GridViewRow In gvKW.Rows
                If row.RowIndex = gvKW.SelectedIndex Then

                    row.ForeColor = ColorTranslator.FromHtml("#0000FF")
                Else
                    row.ForeColor = ColorTranslator.FromHtml("#000000")
                End If
            Next

        Catch ex As Exception

        End Try
    End Sub

    Private Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.Click
        Dim strSql As String
        Dim dbconn As New DBKewConn
        Try
            Dim strKodKW As String = Trim(txtKodKW.Text.TrimEnd)
            Dim strButiran As String = Trim(txtButiran.Text.ToUpper.TrimEnd)
            Dim intStatus As Integer = rbStatus.SelectedValue

            If ViewState("SaveMode") = 1 Then
                'INSERT

                strSql = "select count(*) from MK_Kw where KodKw = '" & strKodKW & "'"
                If fCheckRec(strSql) = 0 Then

                    strSql = "insert into MK_Kw (KodKw , Butiran, Status) values(@KodKW,@Butiran,@Status)"
                    Dim paramSql() As SqlParameter = {
                        New SqlParameter("@KodKW", strKodKW),
                        New SqlParameter("@Butiran", strButiran),
                        New SqlParameter("@Status", intStatus)
                    }

                    dbconn.sConnBeginTrans()
                    If dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                        dbconn.sConnCommitTrans()
                        fGlobalAlert("Rekod baru telah disimpan!", Me.Page, Me.[GetType]())
                        fBindGvKW()
                        fReset()
                    Else
                        dbconn.sConnRollbackTrans()
                        fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
                    End If

                Else


                End If
            ElseIf ViewState("SaveMode") = 2 Then
                'UPDATE
                strSql = "update MK_Kw set Butiran = @Butiran, Status = @Status where KodKw = @KodKw"
                Dim paramSql() As SqlParameter = {
                            New SqlParameter("@KodKW", strKodKW),
                            New SqlParameter("@Butiran", strButiran),
                            New SqlParameter("@Status", intStatus)
                            }
                dbconn.sConnBeginTrans()
                If dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                    dbconn.sConnCommitTrans()
                    fGlobalAlert("Rekod telah dikemas kini!", Me.Page, Me.[GetType]())
                    fBindGvKW()
                    fReset()
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
        ViewState("SaveMode") = 1 'New
    End Sub

    Private Sub lbtnHapus_Click(sender As Object, e As EventArgs) Handles lbtnHapus.Click
        Try
            Dim strSql As String
            Dim dbconn As New DBKewConn
            Dim strKodKW As String

            strKodKW = Trim(txtKodKW.Text.TrimEnd)
            strSql = "Delete from MK_Kw where KodKw = '" & strKodKW & "'"
            If dbconn.fUpdateCommand(strSql) > 0 Then
                fGlobalAlert("Rekod telah dihapuskan!", Me.Page, Me.[GetType]())
                fBindGvKW()
                fReset()
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class