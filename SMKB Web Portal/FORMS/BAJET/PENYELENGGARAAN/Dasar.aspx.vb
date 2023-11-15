Imports System.Data.SqlClient
Imports System.Drawing
Public Class Dasar
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                fBindGvDasar()
                ViewState("savemode") = 1
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvDasar_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvDasar.RowCommand
        Try
            If e.CommandName = "Select" Then

                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvDasar.Rows(index)

                Dim strKodDsr As String = selectedRow.Cells(1).Text
                Dim strButiran As String = selectedRow.Cells(2).Text
                Dim strStatus As String = selectedRow.Cells(3).Text
                Dim intStatus As Integer

                If strStatus = "Aktif" Then intStatus = 1 Else intStatus = 0
                txtKodDsr.Text = strKodDsr
                txtButiranDsr.Text = strButiran
                rbStatus.SelectedValue = intStatus

                txtKodDsr.ReadOnly = True
                txtKodDsr.BackColor = ColorTranslator.FromHtml("#FFFFCC")

                ViewState("savemode") = 2
                lbtnHapus.Visible = True
            End If
        Catch ex As Exception

        End Try


    End Sub



    Private Sub lbtnBaru_Click(sender As Object, e As EventArgs) Handles lbtnBaru.Click
        fReset()
        ViewState("savemode") = 1
    End Sub

    Private Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.Click
        Dim strSql As String

        Dim strKodDasar As String = Trim(txtKodDsr.Text.TrimEnd.ToUpper)
        Dim strButiran As String = Trim(txtButiranDsr.Text.TrimEnd.ToUpper)
        Dim intStatus As Integer = rbStatus.SelectedValue
        If ViewState("savemode") = 1 Then
            'INSERT

            strSql = "select count(*)  from BG_Dasar where BG_KodDasar = '" & strKodDasar & "'"

            If fCheckRec(strSql) > 0 Then
                fGlobalAlert("Rekod telah wujud di dalam pangkalan data!", Me.Page, Me.[GetType]())
                Exit Sub
            Else
                Try
                    'INSERT INTO BG_Dasar
                    strSql = "insert into BG_Dasar(BG_KodDasar, Butiran, Status) values (@KodDasar, @Butiran, @Status)"

                    Dim paramSql() As SqlParameter = {
                         New SqlParameter("@KodDasar", strKodDasar),
                        New SqlParameter("@Butiran", strButiran),
                        New SqlParameter("@Status", intStatus)
                        }

                    Dim dbconn As New DBKewConn
                    dbconn.sConnBeginTrans()
                    If dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                        dbconn.sConnCommitTrans()
                        fGlobalAlert("Rekod telah disimpan!", Me.Page, Me.[GetType]())
                        fReset()
                        fBindGvDasar()
                    Else
                        dbconn.sConnRollbackTrans()
                        fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
                        Exit Sub
                    End If
                Catch ex As Exception
                    fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
                    Exit Sub
                End Try
            End If

        ElseIf ViewState("savemode") = 2 Then
            'UPDATE
            strSql = "update BG_Dasar SET Butiran = @Butiran, Status = @Status where BG_KodDasar = @KodDasar"

            Dim paramSql() As SqlParameter = {
                    New SqlParameter("@KodDasar", strKodDasar),
                        New SqlParameter("@Butiran", strButiran),
                        New SqlParameter("@Status", intStatus)
                        }

            Dim dbconn As New DBKewConn
            dbconn.sConnBeginTrans()
            If dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                dbconn.sConnCommitTrans()
                fGlobalAlert("Rekod telah disimpan!", Me.Page, Me.[GetType]())
                fReset()
                fBindGvDasar()
            Else
                dbconn.sConnRollbackTrans()
                fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
                Exit Sub
            End If
        End If

    End Sub

    Private Function fBindGvDasar()
        Try
            Dim strSql As String
            Dim dt As New DataTable
            Dim strKodDasar, strButiran, strStatus As String
            Dim blnStatus As Boolean

            dt.Columns.Add("BG_KodDasar", GetType(String))
            dt.Columns.Add("Butiran", GetType(String))
            dt.Columns.Add("Status", GetType(String))

            strSql = "select BG_KodDasar , Butiran ,Status from BG_Dasar with (nolock) Order By ID "

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        strKodDasar = ds.Tables(0).Rows(i)("BG_KodDasar")
                        strButiran = ds.Tables(0).Rows(i)("Butiran")
                        blnStatus = ds.Tables(0).Rows(i)("Status")

                        If blnStatus = True Then strStatus = "Aktif" Else strStatus = "Tidak Aktif"
                        dt.Rows.Add(strKodDasar, strButiran, strStatus)
                    Next
                End If
            End If

            lblJumRekod.InnerText = dt.Rows.Count

            If dt.Rows.Count = 0 Then
                gvDasar.DataSource = New List(Of String)
            Else
                gvDasar.DataSource = dt
            End If

            gvDasar.DataBind()

        Catch ex As Exception


        End Try

    End Function

    Private Function fReset()
        Try
            txtKodDsr.ReadOnly = False
            txtKodDsr.BackColor = ColorTranslator.FromHtml("#FFFFFF")
            txtKodDsr.Text = ""
            txtButiranDsr.Text = ""
            rbStatus.SelectedIndex = 0
            lbtnHapus.Visible = False
        Catch ex As Exception

        End Try
    End Function

    Protected Sub lbtnHapus_Click(sender As Object, e As EventArgs) Handles lbtnHapus.Click
        Try

            Dim dbconn As New DBKewConn
            Dim strKodDsr As String = Trim(txtKodDsr.Text.TrimEnd)

            Dim strSql As String = "Delete from BG_Dasar where BG_KodDasar = @KodDasar"
            Dim paramSql() As SqlParameter = {
                    New SqlParameter("@KodDasar", strKodDsr)
                            }
            dbconn.sConnBeginTrans()
            If dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                dbconn.sConnCommitTrans()
                fGlobalAlert("Rekod telah disimpan!", Me.Page, Me.[GetType]())
                fBindGvDasar()
                fReset()
            Else
                dbconn.sConnRollbackTrans()
                fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
            End If

        Catch ex As Exception

        End Try
    End Sub
End Class