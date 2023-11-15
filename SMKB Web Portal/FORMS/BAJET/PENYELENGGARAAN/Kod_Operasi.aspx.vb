Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Public Class Kod_Operasi
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                fBindGvKO()
                ViewState("savemode") = 1
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub fBindGvKO()
        Try
            Dim dt As New DataTable
            dt = fCreateDtKO()

            If dt.Rows.Count > 0 Then
                gvKodOperasi.DataSource = dt
            Else
                gvKodOperasi.DataSource = New List(Of String)
            End If
            gvKodOperasi.DataBind()

        Catch ex As Exception

        End Try
    End Sub
    Private Function fCreateDtKO() As DataTable
        Try
            Dim ds As New DataSet
            Dim dt As New DataTable

            Dim KodKO = New DataColumn("KodOperasi", GetType(String))
            Dim ButirKO = New DataColumn("Butiran", GetType(String))
            Dim Status = New DataColumn("Status", GetType(String))

            dt.Columns.Add(KodKO)
            dt.Columns.Add(ButirKO)
            dt.Columns.Add(Status)

            Dim strKodKO As String
            Dim strButirKO As String
            Dim strStatus As String
            Dim blnStatus As Boolean

            ds = BindGvKO()
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                If ds.Tables(0).Rows.Count > 0 Then
                    strKodKO = ds.Tables(0).Rows(i)(1).ToString
                    strButirKO = ds.Tables(0).Rows(i)(2).ToString
                    blnStatus = ds.Tables(0).Rows(i)(3)
                    If blnStatus = True Then strStatus = "Aktif" Else strStatus = "Tidak Aktif"

                    dt.Rows.Add(strKodKO, strButirKO, strStatus)
                End If
            Next

            gvKodOperasi.DataSource = dt
            gvKodOperasi.DataBind()

            Return dt
        Catch ex As Exception

        End Try


    End Function

    Private Function BindGvKO() As DataSet
        Try
            Dim intRec As Integer
            Dim strSql As String = "select * from MK_KodOperasi where Status = 1 order by KodKO"

            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            Dim dsKodOperasi As New DataSet

            dsKodOperasi = dbconn.fselectCommand(strSql)

            intRec = dsKodOperasi.Tables(0).Rows.Count
            lblJumRekod.InnerText = intRec

            Return dsKodOperasi
        Catch ex As Exception

        End Try


    End Function

    Private Sub gvKodOperasi_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvKodOperasi.PageIndexChanging
        Try
            gvKodOperasi.PageIndex = e.NewPageIndex
            If Session("SortedView") IsNot Nothing Then
                gvKodOperasi.DataSource = Session("SortedView")
                gvKodOperasi.DataBind()
            Else
                Dim dt As New DataTable
                dt = fCreateDtKO()
                gvKodOperasi.DataSource = dt
                gvKodOperasi.DataBind()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub lbtnBaru_Click(sender As Object, e As EventArgs) Handles lbtnBaru.Click
        Try
            fReset()
            ViewState("savemode") = 1
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvKodOperasi_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvKodOperasi.RowCommand
        Try
            If e.CommandName = "Select" Then
                Dim intStatus As Integer

                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvKodOperasi.Rows(index)

                Dim strKodKO As String = selectedRow.Cells(1).Text
                Dim strButirKO As String = selectedRow.Cells(2).Text
                Dim strStatus As String = selectedRow.Cells(3).Text

                selectedRow.ForeColor = ColorTranslator.FromHtml("#0000FF")
                selectedRow.ToolTip = String.Empty

                txtKodKO.Text = strKodKO
                txtNamaKodKO.Text = strButirKO
                If strStatus = "Aktif" Then intStatus = 1 Else intStatus = 0
                rbStatus.SelectedValue = intStatus
                ViewState("savemode") = 2

                txtKodKO.ReadOnly = True
                txtKodKO.BackColor = ColorTranslator.FromHtml("#FFFFCC")

                lbtnHapus.Visible = True
            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Function fReset()
        Try
            txtKodKO.Text = ""
            txtNamaKodKO.Text = ""
            txtKodKO.ReadOnly = False
            txtKodKO.BackColor = ColorTranslator.FromHtml("#FFFFFF")
            rbStatus.SelectedIndex = 0
            For Each row As GridViewRow In gvKodOperasi.Rows
                row.ForeColor = ColorTranslator.FromHtml("#000000")
            Next

            lbtnHapus.Visible = False
        Catch ex As Exception

        End Try
    End Function

    Private Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.Click
        Dim dbconn As New DBKewConn
        Try
            Dim strSql As String

            If txtKodKO.Text = "" Or txtNamaKodKO.Text = "" Then
                fGlobalAlert("Sila pastikan tiada ruang yang dibiarkan kosong!", Me.Page, Me.[GetType]())
                Exit Sub
            End If

            Dim strKodKO As String = Trim(txtKodKO.Text.TrimEnd.ToUpper)
            Dim strButirKO As String = Trim(txtNamaKodKO.Text.TrimEnd.ToUpper)
            Dim intStatus As Integer = rbStatus.SelectedValue
            If ViewState("savemode") = 1 Then
                strSql = " SELECT COUNT (KodOperasi) FROM MK_KodOperasi where KodOperasi ='" & strKodKO & "'"

                If fCheckRec(strSql) > 0 Then
                    fGlobalAlert("Rekod telah wujud di dalam pangkalan data!", Me.Page, Me.[GetType]())
                    Exit Sub
                Else
                    'INSERT INTO MK_KodOperasi
                    strSql = "insert into MK_KodOperasi (KodKO ,Butiran, Status ) values(@KodOperasi,@Butiran, @Status)"

                    Dim paramSql() As SqlParameter = {
                         New SqlParameter("@KodOperasi", strKodKO),
                        New SqlParameter("@Butiran", strButirKO),
                        New SqlParameter("@Status", intStatus)
                        }
                    dbconn.sConnBeginTrans()
                    If dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                        dbconn.sConnCommitTrans()
                        fGlobalAlert("Rekod telah disimpan!", Me.Page, Me.[GetType]())
                        fReset()
                        fBindGvKO()
                    Else
                        dbconn.sConnRollbackTrans()
                        fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
                        Exit Sub
                    End If
                End If
            ElseIf ViewState("savemode") = 2 Then
                'UPDATE MK_KodOperasi
                strSql = "update MK_KodOperasi SET Butiran = @Butiran, Status = @Status where KodKO = @KodOperasi"

                Dim paramSql() As SqlParameter = {
                        New SqlParameter("@KodOperasi", strKodKO),
                       New SqlParameter("@Butiran", strButirKO),
                        New SqlParameter("@Status", intStatus)
                       }
                dbconn.sConnBeginTrans()
                If dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                    dbconn.sConnCommitTrans()
                    fGlobalAlert("Rekod telah disimpan!", Me.Page, Me.[GetType]())
                    fReset()
                    fBindGvKO()

                Else
                    dbconn.sConnRollbackTrans()
                    fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
                    Exit Sub
                End If
            End If

        Catch ex As Exception
            fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
        End Try
    End Sub

    Protected Sub lbtnHapus_Click(sender As Object, e As EventArgs) Handles lbtnHapus.Click
        Dim dbconn As New DBKewConn
        Try
            Dim strsql As String

            If txtKodKO.Text = "" Or txtNamaKodKO.Text = "" Then
                fGlobalAlert("Sila pilih rekod yang ingin dihapuskan!", Me.Page, Me.[GetType]())
                Exit Sub
            End If

            Dim strKodKO As String = Trim(txtKodKO.Text.TrimEnd.ToUpper)
            Dim strButirKO As String = Trim(txtNamaKodKO.Text.TrimEnd.ToUpper)


            strsql = "delete from MK_KodOperasi WHERE KodKO='" & strKodKO & "'"

            Dim paramSql() As SqlParameter =
                {
                    New SqlParameter("@KodOperasi", strKodKO),
                    New SqlParameter("@Butiran", strButirKO)
                   }
            dbconn.sConnBeginTrans()
            If dbconn.fUpdateCommand(strsql, paramSql) > 0 Then
                dbconn.sConnCommitTrans()
                fGlobalAlert("Rekod telah dihapuskan!", Me.Page, Me.[GetType]())
                fReset()
                fBindGvKO()
            Else
                dbconn.sConnRollbackTrans()
                fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
                Exit Sub
            End If

        Catch ex As Exception
            dbconn.sConnRollbackTrans()
        End Try
    End Sub

    Private Sub gvKodOperasi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvKodOperasi.SelectedIndexChanged
        Try
            For Each row As GridViewRow In gvKodOperasi.Rows
                If row.RowIndex = gvKodOperasi.SelectedIndex Then
                    row.ForeColor = ColorTranslator.FromHtml("#0000FF")
                Else
                    row.ForeColor = ColorTranslator.FromHtml("#000000")
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub
End Class