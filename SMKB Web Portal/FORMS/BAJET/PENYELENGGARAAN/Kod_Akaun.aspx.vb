Imports System.Drawing
Imports System.Data.SqlClient
Public Class Kod_Akaun
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fLoadKW()
            fLoadKO()
            fLoadPTj()
            fLoadKP()
            fLoadVot()

            txtTahun.Text = Now.Year
            fBindGv()

        End If
    End Sub

    Private Sub fLoadKW()
        Try
            Dim dbconn As New DBKewConn

            Dim strSql As String = "Select KodKw,Butiran,(KodKw + ' - ' + Butiran ) as ButiranKW from MK_Kw Where Status = 1 order by KodKw"

            Dim ds As New DataSet
            ds = dbConn.fselectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim dvKW As New DataView
                    dvKW = New DataView(ds.Tables(0))
                    ViewState("dvKW") = dvKW.Table
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fLoadKO()
        Try

            Dim strSql As String = "select KodKO , (KodKO + ' - ' + Butiran ) as butiranKO from MK_KodOperasi order by KodKO "
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn

            ds = dbconn.fselectCommand(strSql)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim dvKO As New DataView
                    dvKO = New DataView(ds.Tables(0))
                    ViewState("dvKO") = dvKO.Table
                End If
            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Sub fLoadPTj()
        Try
            Dim strSql As String = "select KodPTJ ,(KodPTJ + ' - ' + Butiran ) as ButiranPTj from MK_PTJ where KodPTJ <> '-' and KodKategoriPTJ = 'P' and status = 1 order by KodPTJ "
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn

            ds = dbconn.fselectCommand(strSql)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim dvKO As New DataView
                    dvKO = New DataView(ds.Tables(0))
                    ViewState("dvPTj") = dvKO.Table
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub fLoadKP()
        Try

            Dim strSql As String = "select KodProjek, (KodProjek + ' - ' + Butiran) as ButiranKP  from MK_KodProjek"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn

            ds = dbconn.fselectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim dvKO As New DataView
                    dvKO = New DataView(ds.Tables(0))
                    ViewState("dvKP") = dvKO.Table
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fLoadVot()
        Try

            Dim strSql As String = "select kodvot, (kodvot + ' - ' + Butiran) as ButiranVot from MK_Vot "
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn

            ds = dbconn.fselectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim dvKO As New DataView
                    dvKO = New DataView(ds.Tables(0))
                    ViewState("dvVot") = dvKO.Table
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvKodAkaun_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvKodAkaun.SelectedIndexChanged
        Try
            For Each row As GridViewRow In gvKodAkaun.Rows
                If row.RowIndex = gvKodAkaun.SelectedIndex Then
                    row.ForeColor = ColorTranslator.FromHtml("#0000FF")
                Else
                    row.ForeColor = ColorTranslator.FromHtml("#000000")
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvKodAkaun_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvKodAkaun.RowCommand
        Try
            If e.CommandName = "Select" Then
                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvKodAkaun.Rows(index)

                'Dim strKW As String = selectedRow.Cells(1).Text
                'Dim strKO As String = selectedRow.Cells(2).Text
                'Dim strPTj As String = selectedRow.Cells(3).Text
                'Dim strKP As String = selectedRow.Cells(4).Text
                'Dim strKodvot As String = selectedRow.Cells(5).Text
                'Dim strStatus As String = selectedRow.Cells(6).Text

                Dim strKW As String = TryCast(selectedRow.FindControl("lblKodKW"), Label).Text.ToString
                Dim strKO As String = TryCast(selectedRow.FindControl("lblKodKO"), Label).Text.ToString
                Dim strPTj As String = TryCast(selectedRow.FindControl("lblPTj"), Label).Text.ToString
                Dim strKP As String = TryCast(selectedRow.FindControl("lblKodKP"), Label).Text.ToString
                Dim strKodvot As String = TryCast(selectedRow.FindControl("lblKodVot"), Label).Text.ToString
                Dim strStatus As String = TryCast(selectedRow.FindControl("lblStatus"), Label).Text.ToString

                Dim dvKW As New DataView
                dvKW = New DataView(ViewState("dvKW"))
                dvKW.RowFilter = "KodKW = '" & strKW & "'"
                txtKW.Text = dvKW.Item(0)("ButiranKW").ToString

                Dim dvKO As New DataView
                dvKO = New DataView(ViewState("dvKO"))
                dvKO.RowFilter = "KodKO = '" & strKO & "'"
                txtKO.Text = dvKO.Item(0)("ButiranKO").ToString

                Dim dvPTj As New DataView
                dvPTj = New DataView(ViewState("dvPTj"))
                dvPTj.RowFilter = "KodPTj = '" & strPTj & "'"
                txtPTj.Text = dvPTj.Item(0)("ButiranPTj").ToString

                Dim dvKP As New DataView
                dvKP = New DataView(ViewState("dvKP"))
                dvKP.RowFilter = "KodProjek = '" & strKP & "'"
                txtKP.Text = dvKP.Item(0)("ButiranKP").ToString

                Dim dvVot As New DataView
                dvVot = New DataView(ViewState("dvVot"))
                dvVot.RowFilter = "Kodvot = '" & strKodvot & "'"
                txtKodvot.Text = dvVot.Item(0)("ButiranVot").ToString


                If strStatus = "Aktif" Then
                    rbStatus.SelectedValue = 1
                ElseIf strStatus = "Tidak Aktif" Then
                    rbStatus.SelectedValue = 0
                End If

            End If

        Catch ex As Exception

        End Try
    End Sub


    Private Sub fClearTxt()
        Try
            txtKW.Text = ""
            txtKO.Text = ""
            txtPTj.Text = ""
            txtKP.Text = ""
            txtKodvot.Text = ""
            rbStatus.SelectedIndex = 0
            For Each row As GridViewRow In gvKodAkaun.Rows
                row.ForeColor = ColorTranslator.FromHtml("#000000")
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindGv()
        Try
            Dim intRec As Integer
            Dim strSql As String = "SELECT DISTINCT dbo.MK01_VotTahun.KodKw, dbo.MK01_VotTahun.KodKO , dbo.MK01_VotTahun.KodPTJ, dbo.MK01_VotTahun.KodKP , dbo.MK01_VotTahun.KodVot, dbo.MK01_VotTahun.MK01_Tahun , dbo.MK01_VotTahun.MK01_Status " &
                            "FROM dbo.MK01_VotTahun INNER JOIN dbo.MK03_AmTahun ON dbo.MK01_VotTahun.KodKw = dbo.MK03_AmTahun.KodKw where dbo.MK01_VotTahun.mk01_tahun = '" & Trim(txtTahun.Text.TrimEnd) & "' " &
                            "order by dbo.MK01_VotTahun.kodkw,dbo.MK01_VotTahun.KodKO ,dbo.MK01_VotTahun.kodptj,dbo.MK01_VotTahun.KodKP ,dbo.MK01_VotTahun.kodvot"
            Dim dbConn As New DBKewConn
            Dim ds As New DataSet
            ds = dbConn.fselectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim dt As New DataTable
                    dt.Columns.Add("kodKW", GetType(String))
                    dt.Columns.Add("kodKO", GetType(String))
                    dt.Columns.Add("kodPTj", GetType(String))
                    dt.Columns.Add("kodKP", GetType(String))
                    dt.Columns.Add("kodVot", GetType(String))
                    dt.Columns.Add("Status", GetType(String))

                    Dim strKW, strKO, strPTj, strKP, strKodvot, strStatus As String
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        strKW = ds.Tables(0).Rows(i)("KodKw")
                        strKO = ds.Tables(0).Rows(i)("KodKO")
                        strPTj = ds.Tables(0).Rows(i)("KodPTJ")
                        strKP = ds.Tables(0).Rows(i)("KodKP")
                        strKodvot = ds.Tables(0).Rows(i)("KodVot")
                        strStatus = IIf(ds.Tables(0).Rows(i)("MK01_Status") = 1, "Aktif", "Tidak Aktif")

                        dt.Rows.Add(strKW, strKO, strPTj, strKP, strKodvot, strStatus)
                    Next
                    gvKodAkaun.DataSource = dt
                    gvKodAkaun.DataBind()
                    ViewState("dtKodAkaun") = dt
                Else
                    intRec = 0
                End If
            Else
                intRec = 0
            End If

            intRec = ds.Tables(0).Rows.Count
            lblJumRekod.InnerText = intRec

        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvKodAkaun_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvKodAkaun.PageIndexChanging
        Try

            gvKodAkaun.PageIndex = e.NewPageIndex
            If Session("dtKodAkaun") IsNot Nothing Then
                gvKodAkaun.DataSource = Session("dtKodAkaun")
                gvKodAkaun.DataBind()
            Else
                fBindGv()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.Click
        Dim blnSuccess As Boolean

        Try
            Dim strSql As String

            Dim intStatus As Integer = rbStatus.SelectedValue
            Dim strTahun As String = Trim(txtTahun.Text.TrimEnd)
            Dim strKodKW As String = Trim(txtKW.Text.TrimEnd).Substring(0, 2)
            Dim strKodKO As String = Trim(txtKO.Text.TrimEnd).Substring(0, 2)
            Dim strKodPTj As String = Trim(txtPTj.Text.TrimEnd).Substring(0, 6)
            Dim strKodKP As String = Trim(txtKP.Text.TrimEnd).Substring(0, 7)
            Dim strKodVot As String = Trim(txtKodvot.Text.TrimEnd).Substring(0, 5)

            Dim dbConn As New DBKewConn
            strSql = "UPDATE mk01_vottahun SET MK01_Status = @Status WHERE MK01_Tahun = @Tahun AND KodKw = @KodKW AND KodKO = @KodKO AND KodPTJ = @KodPTj AND KodKP = @KodKP AND KodVot = @Kodvot "
            Dim paramSql() As SqlParameter = {
                        New SqlParameter("@Status", intStatus),
                        New SqlParameter("@Tahun", strTahun),
                        New SqlParameter("@KodKW", strKodKW),
                        New SqlParameter("@KodKO", strKodKO),
                        New SqlParameter("@KodPTj", strKodPTj),
                        New SqlParameter("@KodKP", strKodKP),
                        New SqlParameter("@Kodvot", strKodVot)
                        }

            dbConn.sConnBeginTrans()
            If dbConn.fUpdateCommand(strSql, paramSql) > 0 Then
                dbConn.sConnCommitTrans()
                blnSuccess = True
            Else
                dbConn.sConnRollbackTrans()
                blnSuccess = False
            End If

        Catch ex As Exception
            blnSuccess = False
        End Try

        If blnSuccess = True Then
            fGlobalAlert("Rekod telah dikemas kini!", Me.Page, Me.[GetType]())
            fClearTxt()
            fBindGv()
        ElseIf blnSuccess = False Then
            fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
        End If
    End Sub

    Private Sub gvKodAkaun_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvKodAkaun.Sorting
        Try

            Dim sortingDirection As String = String.Empty
            If direction = SortDirection.Ascending Then
                direction = SortDirection.Descending
                sortingDirection = "Desc"
            Else
                direction = SortDirection.Ascending
                sortingDirection = "Asc"
            End If

            Dim dt As New DataTable
            dt = CType(ViewState("dtKodAkaun"), DataTable)

            Dim sortedView As New DataView(dt)
            sortedView.Sort = Convert.ToString(e.SortExpression + " ") & sortingDirection
            Session("SortedView") = sortedView
            gvKodAkaun.DataSource = sortedView
            gvKodAkaun.DataBind()

        Catch ex As Exception

        End Try
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
End Class