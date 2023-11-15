Public Class Bidang_MOF
    Inherits System.Web.UI.Page

    Dim dbconn As New DBKewConn

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fLoadGVBidangUtama()
            fLoadGVBidangSub()
            fLoadDdlBidangUtama()
            fLoadDdlBidangSub()
            fLoadGVBidang()
        End If

    End Sub

    Protected Sub gvBidangUtama_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvBidangUtama.SelectedIndexChanged
        txtKod.Text = gvBidangUtama.SelectedRow.Cells(1).Text
        txtButiran.Text = gvBidangUtama.SelectedRow.Cells(2).Text
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
        Dim str As String = $"SELECT * FROM [ROC_BidangUtama] WHERE Kod='{txtKod.Text}'"
        Dim dbconn As New DBKewConn
        Dim rCommit = False
        Using ds = dbconn.fSelectCommand(str, "BidangUtama")
            Using dt = ds.Tables(0)
                Dim dr As DataRow
                If dt.Rows.Count = 0 Then
                    dr = dt.NewRow
                    dr("Kod") = txtKod.Text
                    dr("Butiran") = txtButiran.Text
                    dt.Rows.Add(dr)
                Else
                    dr = dt.Rows(0)
                    dr("Butiran") = txtButiran.Text
                End If

                dbconn.sUpdateCommand(ds, str, True, True, rCommit)
                If rCommit Then
                    fGlobalAlert("Rekod telah disimpan.", Me.Page, Me.GetType())
                    fLoadGVBidangUtama()
                    txtKod.Text = ""
                    txtButiran.Text = ""
                Else
                    fGlobalAlert("Rekod tidak boleh disimpan! Sila hubungi admin.", Me.Page, Me.GetType())
                End If
            End Using
        End Using
    End Sub

    Protected Sub ddlSaizRekod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSaizRekod.SelectedIndexChanged
        gvBidangUtama.PageSize = CInt(ddlSaizRekod.SelectedValue)
        fLoadgvBidangUtama()
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
        Dim str = "SELECT * FROM ROC_BidangUtama"
        Dim ds = dbconn.fselectCommand(str)
        Return ds.Tables(0)
    End Function

    Private Sub fLoadGVBidangUtama()
        Dim dt = fCreateDt()
        gvBidangUtama.DataSource = dt
        gvBidangUtama.DataBind()

        lblJumRekod.InnerText = dt.Rows.Count
        txtKod.Text = ""
        txtButiran.Text = ""
    End Sub

    Protected Sub gvBidangUtama_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvBidangUtama.RowDeleting
        Dim strKod As String = gvBidangUtama.DataKeys(e.RowIndex).Value
        Dim strSql As String = $"delete from ROC_BidangUtama where  Kod = '{ strKod }'"
        Dim dbconn As New DBKewConn
        dbconn.sConnBeginTrans()
        If dbconn.fUpdateCommand(strSql) > 0 Then
            dbconn.sConnCommitTrans()
            fGlobalAlert("Rekod telah dipadam", Me.Page, Me.GetType())
            fLoadGVBidangUtama()
        Else
            dbconn.sConnRollbackTrans()
        End If
    End Sub

    Protected Sub gvBidangUtama_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvBidangUtama.PageIndexChanging
        gvBidangUtama.PageIndex = e.NewPageIndex
        If Session("SortedView") IsNot Nothing Then
            gvBidangUtama.DataSource = Session("SortedView")
            gvBidangUtama.DataBind()
        Else
            fLoadGVBidangUtama()
        End If
    End Sub

    Protected Sub gvBidangUtama_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvBidangUtama.Sorting
        Dim sortedView As New DataView(fCreateDt())
        sortedView.Sort = e.SortExpression & " " & GetSortDirection(e.SortExpression)
        Session("SortedView") = sortedView
        gvBidangUtama.DataSource = sortedView
        gvBidangUtama.DataBind()
    End Sub

    Protected Sub lbtnRekodBaruSub_Click(sender As Object, e As EventArgs) Handles lbtnRekodBaruSub.Click
        txtKodSub.Text = fGetNewKodSub(ddlBidangUtama.SelectedValue)
        txtButiranSub.Text = ""
        txtKodSub.Enabled = True
    End Sub


    Protected Sub lbtnSimpanSub_Click(sender As Object, e As EventArgs) Handles lbtnSimpanSub.Click
        If txtKodSub.Text = "" Then
            fGlobalAlert("Tiada Kod", Page, Me.GetType())
            Exit Sub
        End If
        Dim str As String = $"SELECT * FROM ROC_SubBidang WHERE Kod='{txtKodSub.Text}'"
        Dim dbconn As New DBKewConn
        Dim rCommit = False
        Using ds = dbconn.fSelectCommand(str, "BidangSub")
            Using dt = ds.Tables(0)
                Dim dr As DataRow
                If dt.Rows.Count = 0 Then
                    dr = dt.NewRow
                    dr("Kod") = txtKodSub.Text
                    dr("Butiran") = txtButiranSub.Text
                    dr("KodBdgUtama") = ddlBidangUtama.SelectedValue
                    dt.Rows.Add(dr)
                Else
                    dr = dt.Rows(0)
                    dr("Butiran") = txtButiranSub.Text
                End If

                dbconn.sUpdateCommand(ds, str, True, True, rCommit)
                If rCommit Then
                    fGlobalAlert("Rekod telah disimpan.", Me.Page, Me.GetType())
                    fLoadGVBidangSub()
                    txtKodSub.Text = ""
                    txtButiranSub.Text = ""
                Else
                    fGlobalAlert("Rekod tidak boleh disimpan! Sila hubungi admin.", Me.Page, Me.GetType())
                End If
            End Using
        End Using
    End Sub

    Protected Sub ddlSaizSub_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSaizSub.SelectedIndexChanged
        gvSubBidang.PageSize = CInt(ddlSaizSub.SelectedValue)
        fLoadGVBidangSub()
    End Sub

    Protected Sub gvSubBidang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvSubBidang.SelectedIndexChanged
        txtKodSub.Text = gvSubBidang.SelectedRow.Cells(1).Text
        txtButiranSub.Text = gvSubBidang.SelectedRow.Cells(2).Text
        ddlBidangUtama.SelectedValue = gvSubBidang.SelectedRow.Cells(3).Text

    End Sub

    Protected Sub gvSubBidang_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvSubBidang.RowDeleting
        Dim strKod As String = gvSubBidang.DataKeys(e.RowIndex).Value
        Dim strSql As String = $"delete from ROC_SubBidang where  Kod = '{ strKod }'"
        Dim dbconn As New DBKewConn
        dbconn.sConnBeginTrans()
        If dbconn.fUpdateCommand(strSql) > 0 Then
            dbconn.sConnCommitTrans()
            fGlobalAlert("Rekod telah dipadam", Me.Page, Me.GetType())
            fLoadGVBidangSub()
        Else
            dbconn.sConnRollbackTrans()
        End If

        txtKodSub.Text = ""
        txtButiranSub.Text = ""
    End Sub

    Protected Sub gvSubBidang_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvSubBidang.Sorting
        Dim sortedView As New DataView(fCreateDtSub())
        sortedView.Sort = e.SortExpression & " " & GetSortDirectionSub(e.SortExpression)
        Session("SortedViewSub") = sortedView
        gvSubBidang.DataSource = sortedView
        gvSubBidang.DataBind()
    End Sub

    Protected Sub gvSubBidang_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvSubBidang.PageIndexChanging
        gvSubBidang.PageIndex = e.NewPageIndex
        If Session("SortedViewSub") IsNot Nothing Then
            gvSubBidang.DataSource = Session("SortedViewSub")
            gvSubBidang.DataBind()
        Else
            fLoadGVBidangSub()
        End If
    End Sub

    Private Function fCreateDtSub() As DataTable
        Dim str = ""
        If ddlBidangUtama.SelectedValue = "" Then
            str = $"SELECT * FROM ROC_SubBidang"
        Else
            str = $"SELECT * FROM ROC_SubBidang WHERE KodBdgUtama='{ddlBidangUtama.SelectedValue}'"
        End If

        Dim ds = dbconn.fselectCommand(str)
        Return ds.Tables(0)
    End Function

    Private Sub fLoadGVBidangSub()
        Dim dt = fCreateDtSub()
        gvSubBidang.DataSource = dt
        gvSubBidang.DataBind()
        lblJumRekodSub.InnerText = dt.Rows.Count
    End Sub

    Private Sub fLoadDdlBidangUtama()
        Dim str = "SELECT Kod, Butiran, (Kod + ' - ' + Butiran) as Butiran2 FROM ROC_BidangUtama"
        Dim ds = dbconn.fselectCommand(str)

        ddlBidangUtama.DataSource = ds.Tables(0)
        ddlBidangUtama.DataTextField = "Butiran2"
        ddlBidangUtama.DataValueField = "Kod"
        ddlBidangUtama.DataBind()

        ddlBidangUtama.Items.Insert(0, New ListItem("-Sila pilih-", String.Empty))
        ddlBidangUtama.SelectedIndex = 0
    End Sub

    Private Sub fLoadDdlBidangSub()
        Dim str = "SELECT Kod, Butiran, (Kod + ' - ' + Butiran) as Butiran2 FROM [ROC_SubBidang]"
        Dim ds = dbconn.fselectCommand(str)

        ddlSubBidang.DataSource = ds.Tables(0)
        ddlSubBidang.DataTextField = "Butiran2"
        ddlSubBidang.DataValueField = "Kod"
        ddlSubBidang.DataBind()

        ddlSubBidang.Items.Insert(0, New ListItem("-Sila pilih-", String.Empty))
        ddlSubBidang.SelectedIndex = 0
    End Sub

    Protected Sub ddlBidangUtama_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBidangUtama.SelectedIndexChanged
        fLoadGVBidangSub()
    End Sub

    Private Function fGetNewKodSub(strKod As String) As String

        Dim strSql As String = $"select max(kod) as KodAkhir from ROC_SubBidang where KodBdgUtama = '{strKod}'"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim NoAkhir As Integer
            Dim KodAkhir As String = ""
            Dim strNewKodSubModul As String

            dbconn.sSelectCommand(strSql, KodAkhir)
            If Not String.IsNullOrEmpty(KodAkhir) Then
                NoAkhir = CInt(KodAkhir.Substring(2, 2))
                NoAkhir = NoAkhir + 1
            Else
                NoAkhir = 1
            End If

            strNewKodSubModul = strKod & NoAkhir.ToString("D2")

        Return strNewKodSubModul
    End Function

    Protected Sub ddlSubBidang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSubBidang.SelectedIndexChanged
        fLoadGVBidang()
    End Sub

    Protected Sub lbtnRekodBaruBdg_Click(sender As Object, e As EventArgs) Handles lbtnRekodBaruBdg.Click
        txtKodBidang.Text = fGetNewKodBidang(ddlSubBidang.SelectedValue)
        txtButiranBdg.Text = ""
        txtKodBidang.Enabled = True
    End Sub

    Protected Sub lbtnSimpanBdg_Click(sender As Object, e As EventArgs) Handles lbtnSimpanBdg.Click
        If txtKodBidang.Text = "" Then
            fGlobalAlert("Tiada Kod", Page, Me.GetType())
            Exit Sub
        End If
        Dim str As String = $"SELECT * FROM ROC_Bidang WHERE KodBidang='{txtKodBidang.Text}'"
        Dim dbconn As New DBKewConn
        Dim rCommit = False
        Using ds = dbconn.fSelectCommand(str, "BidangSub")
            Using dt = ds.Tables(0)
                Dim dr As DataRow
                If dt.Rows.Count = 0 Then
                    dr = dt.NewRow
                    dr("KodBidang") = txtKodBidang.Text
                    dr("Butiran") = txtButiranBdg.Text
                    dr("KodSubBidang") = ddlSubBidang.SelectedValue
                    dt.Rows.Add(dr)
                Else
                    dr = dt.Rows(0)
                    dr("Butiran") = ddlSubBidang.Text
                End If

                dbconn.sUpdateCommand(ds, str, True, True, rCommit)
                If rCommit Then
                    fGlobalAlert("Rekod telah disimpan.", Me.Page, Me.GetType())
                    fLoadGVBidang()
                    txtKodBidang.Text = ""
                    txtButiran.Text = ""
                Else
                    fGlobalAlert("Rekod tidak boleh disimpan! Sila hubungi admin.", Me.Page, Me.GetType())
                End If
            End Using
        End Using
    End Sub

    Protected Sub gvBidang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvBidang.SelectedIndexChanged
        txtKodBidang.Text = gvBidang.SelectedRow.Cells(1).Text
        txtButiranBdg.Text = gvBidang.SelectedRow.Cells(2).Text
        ddlSubBidang.SelectedValue = gvBidang.SelectedRow.Cells(3).Text
    End Sub

    Protected Sub ddlSaizRekodBdg_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSaizRekodBdg.SelectedIndexChanged
        gvBidang.PageSize = CInt(ddlSaizRekodBdg.SelectedValue)
        fLoadGVBidang()
    End Sub

    Protected Sub gvBidang_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvBidang.RowDeleting
        Dim strKod As String = gvBidang.DataKeys(e.RowIndex).Value
        Dim strSql As String = $"delete from ROC_Bidang where  KodBidang = '{ strKod }'"
        Dim dbconn As New DBKewConn
        dbconn.sConnBeginTrans()
        If dbconn.fUpdateCommand(strSql) > 0 Then
            dbconn.sConnCommitTrans()
            fGlobalAlert("Rekod telah dipadam", Me.Page, Me.GetType())
            fLoadGVBidang()
        Else
            dbconn.sConnRollbackTrans()
        End If

        txtKodBidang.Text = ""
        txtButiranBdg.Text = ""
    End Sub

    Protected Sub gvBidang_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvBidang.PageIndexChanging
        gvBidang.PageIndex = e.NewPageIndex
        If Session("SortedViewBdg") IsNot Nothing Then
            gvBidang.DataSource = Session("SortedViewBdg")
            gvBidang.DataBind()
        Else
            fLoadGVBidang()
        End If
    End Sub

    Protected Sub gvBidang_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvBidang.Sorting
        'Select Case TOP 1000 [KodBidang] ,[Butiran]   ,[KodSubBidang] From [DbKewanganV1].[dbo].[ROC_Bidang]

        Dim sortedView As New DataView(fCreateDtBidang())
        sortedView.Sort = e.SortExpression & " " & GetSortDirectionBdg(e.SortExpression)
        Session("SortedViewBdg") = sortedView
        gvBidang.DataSource = sortedView
        gvBidang.DataBind()

    End Sub

    Private Function fCreateDtBidang() As DataTable
        Dim str = ""
        If ddlSubBidang.SelectedValue = "" Then
            str = $"SELECT * FROM ROC_Bidang"
        Else
            str = $"SELECT * FROM ROC_Bidang WHERE KodSubBidang='{ddlSubBidang.SelectedValue}'"
        End If

        Dim ds = dbconn.fselectCommand(str)
        Return ds.Tables(0)
    End Function

    Private Sub fLoadGVBidang()
        Dim dt = fCreateDtBidang()
        gvBidang.DataSource = dt
        gvBidang.DataBind()
        lblRekodBdg.InnerText = dt.Rows.Count
    End Sub

    Private Function fGetNewKodBidang(strKod As String) As String
        Dim strSql As String = $"select max(KodBidang) as KodAkhir from [ROC_Bidang] where [KodSubBidang] = '{strKod}'"
        Dim ds As New DataSet
        Dim dbconn As New DBKewConn
        Dim NoAkhir As Integer
        Dim KodAkhir As String = ""
        Dim strNewKod As String

        dbconn.sSelectCommand(strSql, KodAkhir)
        If Not String.IsNullOrEmpty(KodAkhir) Then
            NoAkhir = CInt(KodAkhir.Substring(4, 2))
            NoAkhir = NoAkhir + 1
        Else
            NoAkhir = 1
        End If

        strNewKod = strKod & NoAkhir.ToString("D2")

        Return strNewKod
    End Function

    'Protected Sub ChxEnable_CheckedChanged(sender As Object, e As EventArgs) Handles ChxEnable.CheckedChanged
    '    If ChxEnable.Checked Then
    '        txtKodSub.Enabled = True
    '    Else
    '        txtKodSub.Enabled = False
    '    End If
    'End Sub
End Class