Public Class Cetakan_Pelarasan
    Inherits System.Web.UI.Page
    Private dbconn As New DBKewConn()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                fBindDdlCarian()
                sLoadLst()

            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub fBindDdlCarian()
        Try
            ddlCarian.Items.Clear()
            ddlCarian.Items.Add(New ListItem("KESELURUHAN", 1))
            ddlCarian.Items.Add(New ListItem("No. Invois", 2))
            ddlCarian.Items.Add(New ListItem("Nama Penerima", 3))

            txtCarian.Enabled = False
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlCarian_SelectedIndexChanged(sender As Object, e As EventArgs)
        If ddlCarian.SelectedValue = 0 Then
            txtCarian.Enabled = False
            txtCarian.Text = ""
        Else
            txtCarian.Enabled = True
        End If
    End Sub


    Private Sub lbtnFindInv_Click(sender As Object, e As EventArgs) Handles lbtnFindInv.Click
        sLoadLst()
    End Sub

    Private Sub sClearGv()
        gvCetakInv.DataSource = New List(Of String)
        gvCetakInv.DataBind()
    End Sub

    Private Sub sLoadLst()
        sClearGv()

        Try
            Dim intRec As Integer

            Dim strSql As String = ""
            Dim strJns As String
            If rbJns.SelectedValue = 0 Then
                strJns = "01"
            ElseIf rbJns.SelectedValue = 1 Then
                strJns = "02"
            End If

            Dim strFilter = ""
            If ddlCarian.SelectedValue = 1 Then
                strFilter = ""
            ElseIf ddlCarian.SelectedValue = 2 Then
                strFilter = " AND b.AR01_NOBIL LIKE '%" & (txtCarian.Text.TrimEnd) & "%'"
            ElseIf ddlCarian.SelectedValue = 3 Then
                strFilter = "AND b.AR01_NamaPenerima LIKE '%" & (txtCarian.Text.TrimEnd) & "%'"
            End If

            'strSql = $"Select  a.AR01_NoBil  ,convert(varchar,b.AR06_Tarikh ,103) as TkhBil,AR01_Kategori , AR01_NamaPenerima, (Select isnull (SUM(AR01_Jumlah),0) 
            '                         From AR01_Bildt c 
            '                         Where a.AR01_NoBil = c.AR01_NoBil) As Jumlah
            '                         From AR01_Bil a, AR06_statusdok b
            '                         Where a.AR01_statusdok = b.AR06_StatusDok And b.AR06_NoBil = a.AR01_NoBil
            '                         And a.AR01_StatusDok='03' AND AR01_Jenis='" & strJns & "' " & strFilter & " order by AR01_NoBil desc"

            strSql = "select b.AR01_NoBil, convert(varchar,c.AR06_Tarikh ,103) as TkhBil, a.AR04_NoAdj
From AR04_BilAdj a 
inner join AR01_Bil b on b.AR01_NoBil = a.AR04_NoBil
inner join AR06_StatusDok c on c.AR06_NoBil = a.AR04_NoBil 
where b.ar01_statusdok ='10'and b.ar01_jenis='01' and c.AR06_StatusDok = '06' and b.AR01_Jenis = '" & strJns & "' " & strFilter & " order by a.AR04_NoAdj asc"

            Dim ds As New DataSet
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    ViewState("vsDtInv") = ds
                    gvCetakInv.DataSource = ds
                    gvCetakInv.DataBind()
                    'BindGvViewButiran()

                    intRec = ds.Tables(0).Rows.Count
                Else
                    intRec = 0
                End If

            Else
                intRec = 0
            End If

            lblJumRekod.InnerText = intRec
            If intRec = 0 Then
                'fGlobalAlert("Tiada rekod!", Me.Page, Me.[GetType]())
            End If

        Catch ex As Exception

        End Try
    End Sub

    'Private Sub gvCetakInv_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvCetakInv.RowCreated
    '    Try
    '        If e.Row.RowType = DataControlRowType.Footer Then
    '            'First cell Is used for specifying the Total text
    '            Dim intNoOfMergeCol = e.Row.Cells.Count - 2 ' /*except last column */
    '            'Dim intNoOfMergeCol As Integer = 16
    '            For intCellCol As Integer = 1 To intNoOfMergeCol - 1
    '                e.Row.Cells.RemoveAt(1)
    '            Next

    '            e.Row.Cells(0).ColumnSpan = intNoOfMergeCol
    '            e.Row.Cells(0).Text = "Jumlah Besar (RM)"
    '            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Right
    '            e.Row.Cells(0).Font.Bold = True

    '            Dim total As Decimal = ViewState("TotalAmount")
    '            e.Row.Cells(1).Text = total.ToString("#,##0.00")
    '            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
    '            e.Row.Cells(1).Font.Bold = True

    '            'e.Row.Cells(2).Visible = False
    '            'e.Row.Cells(3).Visible = False

    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub gvCetakInv_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvCetakInv.SelectedIndexChanged
        Dim strNoInv As String = CType(gvCetakInv.SelectedRow.FindControl("lblNoInvCuk"), Label).Text.TrimEnd

        Dim RptName As String = ""


        Dim url As String = $"{RptName}&rs:Command=Render&NoBD={strNoInv}"
        Dim strSVR = $"https://reporting.utem.edu.my/ReportServer/Pages/ReportViewer.aspx?%2fS2%2fS2D%2f{url}"
        Dim fullURL As String = $"window.open('{strSVR}', '_blank', 'width=800,height=850,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=yes,titlebar=no' );"
        ScriptManager.RegisterStartupScript(Me, GetType(String), "OPEN_WINDOW", fullURL, True)


    End Sub

    Private Sub ddlSaizRekod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSaizRekod.SelectedIndexChanged
        gvCetakInv.PageSize = CInt(ddlSaizRekod.Text)
        'BindGvViewButiran()
    End Sub

    Private Sub gvCetakInv_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvCetakInv.PageIndexChanging
        Try
            gvCetakInv.PageIndex = e.NewPageIndex
            If ViewState("vsDtInv") IsNot Nothing Then
                gvCetakInv.DataSource = ViewState("vsDtInv")
                gvCetakInv.DataBind()
            Else
                Dim dt As New DataTable
                'BindGvViewButiran()

                'gvCetakInv.DataSource = dt
                'gvCetakInv.DataBind()

            End If
        Catch ex As Exception

        End Try
    End Sub
End Class