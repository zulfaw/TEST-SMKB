Public Class CIDB
    Inherits System.Web.UI.Page
    Dim dbconn As New DBKewConn

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fLoadGVGred()
            fLoadGVKategori()
            fLoadDdlBidangSub()
            fLoadGVKhusus()
        End If

    End Sub

    Protected Sub gvGred_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvGred.SelectedIndexChanged
        txtKod.Text = gvGred.SelectedRow.Cells(1).Text
        txtButiran.Text = gvGred.SelectedRow.Cells(2).Text
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
        Dim str As String = $"SELECT * FROM [ROC_GredCIDB] WHERE KodGred='{txtKod.Text}'"
        Dim dbconn As New DBKewConn
        Dim rCommit = False
        Using ds = dbconn.fSelectCommand(str, "Gred")
            Using dt = ds.Tables(0)
                Dim dr As DataRow
                If dt.Rows.Count = 0 Then
                    dr = dt.NewRow
                    dr("KodGred") = txtKod.Text
                    dr("Butiran") = txtButiran.Text
                    dr("HadUpaya") = txtHadUpaya.Text
                    dt.Rows.Add(dr)
                Else
                    dr = dt.Rows(0)
                    dr("Butiran") = txtButiran.Text
                    dr("HadUpaya") = txtHadUpaya.Text
                End If

                dbconn.sUpdateCommand(ds, str, True, True, rCommit)
                If rCommit Then
                    fGlobalAlert("Rekod telah disimpan.", Me.Page, Me.GetType())
                    fLoadGVGred()
                Else
                    fGlobalAlert("Rekod tidak boleh disimpan! Sila hubungi admin.", Me.Page, Me.GetType())
                End If
            End Using
        End Using
    End Sub

    Protected Sub ddlSaizRekod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSaizRekod.SelectedIndexChanged
        gvGred.PageSize = CInt(ddlSaizRekod.SelectedValue)
        fLoadGVGred()
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

    Private Function GetSortDirectionSub(ByVal column As String) As String

        ' By default, set the sort direction to ascending.
        Dim sortDirection = "ASC"

        ' Retrieve the last column that was sorted.
        Dim sortExpression = TryCast(ViewState("SortExpressionSub"), String)

        If sortExpression IsNot Nothing Then
            ' Check if the same column is being sorted.
            ' Otherwise, the default value can be returned.
            If sortExpression = column Then
                Dim lastDirection = TryCast(ViewState("SortDirectionSub"), String)
                If lastDirection IsNot Nothing AndAlso lastDirection = "ASC" Then
                    sortDirection = "DESC"
                End If
            End If
        End If

        ' Save new values in ViewState.
        ViewState("SortDirectionSub") = sortDirection
        ViewState("SortExpressionSub") = column

        Return sortDirection

    End Function


    Private Function GetSortDirectionBdg(ByVal column As String) As String

        ' By default, set the sort direction to ascending.
        Dim sortDirection = "ASC"

        ' Retrieve the last column that was sorted.
        Dim sortExpression = TryCast(ViewState("SortExpressionBdg"), String)

        If sortExpression IsNot Nothing Then
            ' Check if the same column is being sorted.
            ' Otherwise, the default value can be returned.
            If sortExpression = column Then
                Dim lastDirection = TryCast(ViewState("SortDirectionBdg"), String)
                If lastDirection IsNot Nothing AndAlso lastDirection = "ASC" Then
                    sortDirection = "DESC"
                End If
            End If
        End If

        ' Save new values in ViewState.
        ViewState("SortDirectionBdg") = sortDirection
        ViewState("SortExpressionBdg") = column

        Return sortDirection

    End Function

    Protected Function fCreateDt() As DataTable
        Dim str = "SELECT * FROM ROC_GredCIDB"
        Dim ds = dbconn.fselectCommand(str)
        Return ds.Tables(0)
    End Function

    Private Sub fLoadGVGred()
        Try


            Dim dt = fCreateDt()
        gvGred.DataSource = dt
        gvGred.DataBind()

        lblJumRekodGred.InnerText = dt.Rows.Count
        txtKod.Text = ""
            txtButiran.Text = ""
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub gvGred_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvGred.RowDeleting
        Dim strKod As String = gvGred.DataKeys(e.RowIndex).Value
        Dim strSql As String = $"delete from ROC_GredCIDB where  KodGred = '{ strKod }'"
        Dim dbconn As New DBKewConn
        dbconn.sConnBeginTrans()
        If dbconn.fUpdateCommand(strSql) > 0 Then
            dbconn.sConnCommitTrans()
            fGlobalAlert("Rekod telah dipadam", Me.Page, Me.GetType())
            fLoadGVGred()
        Else
            dbconn.sConnRollbackTrans()
        End If
    End Sub

    Protected Sub gvGred_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvGred.PageIndexChanging
        gvGred.PageIndex = e.NewPageIndex
        If Session("SortedView") IsNot Nothing Then
            gvGred.DataSource = Session("SortedView")
            gvGred.DataBind()
        Else
            fLoadGVGred()
        End If
    End Sub

    Protected Sub gvGred_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvGred.Sorting
        Dim sortedView As New DataView(fCreateDt())
        sortedView.Sort = e.SortExpression & " " & GetSortDirection(e.SortExpression)
        Session("SortedView") = sortedView
        gvGred.DataSource = sortedView
        gvGred.DataBind()
    End Sub

    Protected Sub lbtnRekodBaruSub_Click(sender As Object, e As EventArgs) Handles lbtnRekodBaruSub.Click
        txtKodSub.Text = ""
        txtButiranSub.Text = ""
        txtKodSub.Enabled = True
    End Sub


    Protected Sub lbtnSimpanSub_Click(sender As Object, e As EventArgs) Handles lbtnSimpanSub.Click
        If txtKodSub.Text = "" Then
            fGlobalAlert("Tiada Kod", Page, Me.GetType())
            Exit Sub
        End If
        Dim str As String = $"SELECT * FROM ROC_KategoriCIDB WHERE KodKategori ='{txtKodSub.Text}'"
        Dim dbconn As New DBKewConn
        Dim rCommit = False
        Using ds = dbconn.fSelectCommand(str, "Kategori")
            Using dt = ds.Tables(0)
                Dim dr As DataRow
                If dt.Rows.Count = 0 Then
                    dr = dt.NewRow
                    dr("KodKategori") = txtKodSub.Text
                    dr("Butiran") = txtButiranSub.Text

                    dt.Rows.Add(dr)
                Else
                    dr = dt.Rows(0)
                    dr("Butiran") = txtButiranSub.Text
                End If

                dbconn.sUpdateCommand(ds, str, True, True, rCommit)
                If rCommit Then
                    fGlobalAlert("Rekod telah disimpan.", Me.Page, Me.GetType())
                    fLoadGVKategori()
                Else
                    fGlobalAlert("Rekod tidak boleh disimpan! Sila hubungi admin.", Me.Page, Me.GetType())
                End If
            End Using
        End Using
    End Sub

    Protected Sub ddlSaizSub_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSaizSub.SelectedIndexChanged
        gvKategori.PageSize = CInt(ddlSaizSub.SelectedValue)
        fLoadGVKategori()
    End Sub

    Protected Sub gvSubBidang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvKategori.SelectedIndexChanged
        txtKodSub.Text = gvKategori.SelectedRow.Cells(1).Text
        txtButiranSub.Text = gvKategori.SelectedRow.Cells(2).Text
    End Sub

    Protected Sub gvSubBidang_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvKategori.RowDeleting
        Dim strKod As String = gvKategori.DataKeys(e.RowIndex).Value
        Dim strSql As String = $"delete from ROC_KategoriCIDB where  KodKategori = '{ strKod }'"
        Dim dbconn As New DBKewConn
        dbconn.sConnBeginTrans()
        If dbconn.fUpdateCommand(strSql) > 0 Then
            dbconn.sConnCommitTrans()
            fGlobalAlert("Rekod telah dipadam", Me.Page, Me.GetType())
            fLoadGVKategori()
        Else
            dbconn.sConnRollbackTrans()
        End If

        txtKodSub.Text = ""
        txtButiranSub.Text = ""
    End Sub

    Protected Sub gvSubBidang_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvKategori.Sorting
        Dim sortedView As New DataView(fCreateDtSub())
        sortedView.Sort = e.SortExpression & " " & GetSortDirectionSub(e.SortExpression)
        Session("SortedViewSub") = sortedView
        gvKategori.DataSource = sortedView
        gvKategori.DataBind()
    End Sub

    Protected Sub gvSubBidang_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvKategori.PageIndexChanging
        gvKategori.PageIndex = e.NewPageIndex
        If Session("SortedViewSub") IsNot Nothing Then
            gvKategori.DataSource = Session("SortedViewSub")
            gvKategori.DataBind()
        Else
            fLoadGVKategori()
        End If
    End Sub

    Private Function fCreateDtSub() As DataTable
        Dim str = $"SELECT * FROM ROC_KategoriCIDB"
        Dim ds = dbconn.fselectCommand(str)
        Return ds.Tables(0)
    End Function

    Private Sub fLoadGVKategori()
        Dim dt = fCreateDtSub()
        gvKategori.DataSource = dt
        gvKategori.DataBind()
        lblJumRekodSub.InnerText = dt.Rows.Count
    End Sub


    Private Sub fLoadDdlBidangSub()
        Dim str = "SELECT KodKategori, Butiran, (KodKategori + ' - ' + Butiran) as Butiran2 FROM [ROC_KategoriCIDB]"
        Dim ds = dbconn.fselectCommand(str)

        ddlKategori.DataSource = ds.Tables(0)
        ddlKategori.DataTextField = "Butiran2"
        ddlKategori.DataValueField = "KodKategori"
        ddlKategori.DataBind()

        ddlKategori.Items.Insert(0, New ListItem("-Sila pilih-", String.Empty))
        ddlKategori.SelectedIndex = 0
    End Sub

    Protected Sub ddlSubBidang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKategori.SelectedIndexChanged
        fLoadGVKhusus()
    End Sub

    Protected Sub lbtnRekodBaruBdg_Click(sender As Object, e As EventArgs) Handles lbtnRekodBaruBdg.Click
        txtKhusus.Text = ""
        txtButiranKhusus.Text = ""
        txtKhusus.Enabled = True
    End Sub

    Protected Sub lbtnSimpanBdg_Click(sender As Object, e As EventArgs) Handles lbtnSimpanBdg.Click
        If txtKhusus.Text = "" Then
            fGlobalAlert("Tiada Kod", Page, Me.GetType())
            Exit Sub
        End If
        Dim str As String = $"SELECT * FROM ROC_PengkhususanCIDB WHERE KodKhusus ='{txtKhusus.Text}'"
        Dim dbconn As New DBKewConn
        Dim rCommit = False
        Using ds = dbconn.fSelectCommand(str, "Khusus")
            Using dt = ds.Tables(0)
                Dim dr As DataRow
                If dt.Rows.Count = 0 Then
                    dr = dt.NewRow
                    dr("KodKhusus") = txtKhusus.Text
                    dr("Butiran") = txtButiranKhusus.Text
                    dr("KodKategori") = ddlKategori.SelectedValue
                    dt.Rows.Add(dr)
                Else
                    dr = dt.Rows(0)
                    dr("Butiran") = ddlKategori.Text
                End If

                dbconn.sUpdateCommand(ds, str, True, True, rCommit)
                If rCommit Then
                    fGlobalAlert("Rekod telah disimpan.", Me.Page, Me.GetType())
                    fLoadGVKhusus()
                Else
                    fGlobalAlert("Rekod tidak boleh disimpan! Sila hubungi admin.", Me.Page, Me.GetType())
                End If
            End Using
        End Using
    End Sub

    Protected Sub gvBidang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvKhusus.SelectedIndexChanged
        txtKhusus.Text = gvKhusus.SelectedRow.Cells(1).Text
        txtButiranKhusus.Text = gvKhusus.SelectedRow.Cells(2).Text
        ddlKategori.SelectedValue = gvKhusus.SelectedRow.Cells(3).Text
    End Sub

    Protected Sub ddlSaizRekodBdg_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSaizRekodKhusus.SelectedIndexChanged
        gvKhusus.PageSize = CInt(ddlSaizRekodKhusus.SelectedValue)
        fLoadGVKhusus()
    End Sub

    Protected Sub gvBidang_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvKhusus.RowDeleting
        Dim strKod As String = gvKhusus.DataKeys(e.RowIndex).Value
        Dim strSql As String = $"delete from ROC_PengkhususanCIDB where  KodKhusus = '{ strKod }'"
        Dim dbconn As New DBKewConn
        dbconn.sConnBeginTrans()
        If dbconn.fUpdateCommand(strSql) > 0 Then
            dbconn.sConnCommitTrans()
            fGlobalAlert("Rekod telah dipadam", Me.Page, Me.GetType())
            fLoadGVKhusus()
        Else
            dbconn.sConnRollbackTrans()
        End If

        txtKhusus.Text = ""
        txtButiranKhusus.Text = ""
    End Sub

    Protected Sub gvBidang_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvKhusus.PageIndexChanging
        gvKhusus.PageIndex = e.NewPageIndex
        If Session("SortedViewBdg") IsNot Nothing Then
            gvKhusus.DataSource = Session("SortedViewBdg")
            gvKhusus.DataBind()
        Else
            fLoadGVKhusus()
        End If
    End Sub

    Protected Sub gvBidang_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvKhusus.Sorting
        'Select Case TOP 1000 [KodBidang] ,[Butiran]   ,[KodSubBidang] From [DbKewanganV1].[dbo].[ROC_Bidang]

        Dim sortedView As New DataView(fCreateDtBidang())
        sortedView.Sort = e.SortExpression & " " & GetSortDirectionBdg(e.SortExpression)
        Session("SortedViewBdg") = sortedView
        gvKhusus.DataSource = sortedView
        gvKhusus.DataBind()

    End Sub

    Private Function fCreateDtBidang() As DataTable
        Dim str = ""
        If ddlKategori.SelectedValue = "" Then
            str = $"SELECT * FROM ROC_PengkhususanCIDB"
        Else
            str = $"SELECT * FROM ROC_PengkhususanCIDB WHERE KodKategori='{ddlKategori.SelectedValue}'"
        End If

        Dim ds = dbconn.fselectCommand(str)
        Return ds.Tables(0)
    End Function

    Private Sub fLoadGVKhusus()
        Dim dt = fCreateDtBidang()
        gvKhusus.DataSource = dt
        gvKhusus.DataBind()
        lblRekodKhusus.InnerText = dt.Rows.Count
    End Sub

    'Private Function fGetNewKodBidang(strKod As String) As String
    '    Dim strSql As String = $"select max(KodBidang) as KodAkhir from [ROC_Bidang] where [KodSubBidang] = '{strKod}'"
    '    Dim ds As New DataSet
    '    Dim dbconn As New DBKewConn
    '    Dim NoAkhir As Integer
    '    Dim KodAkhir As String = ""
    '    Dim strNewKod As String

    '    dbconn.sSelectCommand(strSql, KodAkhir)
    '    If Not String.IsNullOrEmpty(KodAkhir) Then
    '        NoAkhir = CInt(KodAkhir.Substring(4, 2))
    '        NoAkhir = NoAkhir + 1
    '    Else
    '        NoAkhir = 1
    '    End If

    '    strNewKod = strKod & NoAkhir.ToString("D2")

    '    Return strNewKod
    'End Function
End Class