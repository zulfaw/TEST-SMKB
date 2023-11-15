Public Class Sijil_Pendaftaran
    Inherits System.Web.UI.Page
    Dim dbconn As New DBKewConn

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fLoadGVLesen()
        End If
    End Sub

    Protected Sub gvLesen_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvLesen.SelectedIndexChanged
        txtKod.Text = gvLesen.SelectedRow.Cells(1).Text
        txtButiran.Text = gvLesen.SelectedRow.Cells(2).Text
    End Sub

    Protected Sub lbtnRekodBaru_Click(sender As Object, e As EventArgs) Handles lbtnRekodBaru.Click
        txtKod.Text = ""
        txtButiran.Text = ""
        txtKod.Enabled = True
    End Sub

    Protected Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.Click
        If txtKod.Text = "" Then
            fGlobalAlert("Tiada Kod", Page, Me.GetType())
            Exit Sub
        End If
        Dim str As String = $"SELECT * FROM [ROC_Daftar] WHERE KodDaftar='{txtKod.Text}'"
        Dim dbconn As New DBKewConn
        Dim rCommit = False
        Using ds = dbconn.fSelectCommand(str, "Lesen")
            Using dt = ds.Tables(0)
                Dim dr As DataRow
                If dt.Rows.Count = 0 Then
                    dr = dt.NewRow
                    dr("KodDaftar") = txtKod.Text
                    dr("Butiran") = txtButiran.Text
                    dt.Rows.Add(dr)
                Else
                    dr = dt.Rows(0)
                    dr("Butiran") = txtButiran.Text
                End If

                dbconn.sUpdateCommand(ds, str, True, True, rCommit)
                If rCommit Then
                    fGlobalAlert("Rekod telah disimpan.", Me.Page, Me.GetType())
                    fLoadGVLesen()
                    txtKod.Text = ""
                    txtButiran.Text = ""
                Else
                    fGlobalAlert("Rekod tidak boleh disimpan! Sila hubungi admin.", Me.Page, Me.GetType())
                End If
            End Using
        End Using
    End Sub

    Protected Sub ddlSaizRekod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSaizRekod.SelectedIndexChanged
        gvLesen.PageSize = CInt(ddlSaizRekod.SelectedValue)
        fLoadGVLesen()
    End Sub

    Protected Sub gvLesen_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvLesen.RowDeleting
        Dim strKod As String = gvLesen.DataKeys(e.RowIndex).Value
        Dim strSql As String = $"delete from ROC_Daftar where  KodDaftar = '{ strKod }'"
        Dim dbconn As New DBKewConn
        dbconn.sConnBeginTrans()
        If dbconn.fUpdateCommand(strSql) > 0 Then
            dbconn.sConnCommitTrans()
            fGlobalAlert("Rekod telah dipadam", Me.Page, Me.GetType())
            fLoadGVLesen()
        Else
            dbconn.sConnRollbackTrans()
        End If
    End Sub

    Protected Sub gvLesen_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvLesen.PageIndexChanging
        gvLesen.PageIndex = e.NewPageIndex
        If Session("SortedView") IsNot Nothing Then
            gvLesen.DataSource = Session("SortedView")
            gvLesen.DataBind()
        Else
            fLoadGVLesen()
        End If
    End Sub

    Protected Sub gvLesen_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvLesen.Sorting
        Dim sortedView As New DataView(fCreateDt())
        sortedView.Sort = e.SortExpression & " " & GetSortDirection(e.SortExpression)
        Session("SortedView") = sortedView
        gvLesen.DataSource = sortedView
        gvLesen.DataBind()
    End Sub

    Private Function GetSortDirection(ByVal column As String) As String

        ' By default, set the sort direction to ascending.
        Dim sortDirection = "ASC"

        ' Retrieve the last column that was sorted.
        Dim sortExpression = TryCast(ViewState("SortExpression"), String)

        If sortExpression IsNot Nothing Then
            ' Check if the same column is being sorted.
            ' Otherwise, the default value can be returned.
            If sortExpression = column Then
                Dim lastDirection = TryCast(ViewState("SortDirection"), String)
                If lastDirection IsNot Nothing AndAlso lastDirection = "ASC" Then
                    sortDirection = "DESC"
                End If
            End If
        End If

        ' Save new values in ViewState.
        ViewState("SortDirection") = sortDirection
        ViewState("SortExpression") = column

        Return sortDirection

    End Function

    Protected Function fCreateDt() As DataTable
        Dim str = "SELECT * FROM ROC_Daftar"
        Dim ds = dbconn.fselectCommand(str)
        Return ds.Tables(0)
    End Function

    Private Sub fLoadGVLesen()
        Dim dt = fCreateDt()
        gvLesen.DataSource = dt
        gvLesen.DataBind()

        lblJumRekod.InnerText = dt.Rows.Count
        txtKod.Text = ""
        txtButiran.Text = ""
    End Sub
End Class