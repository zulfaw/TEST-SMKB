Imports System.Data.SqlClient

Public Class Daftar_Ptj
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fBindGvPTJ("")
        End If
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

    '''' <summary>
    '''' Add new column for unique numbering evering row
    '''' Pass the value of datatable
    '''' </summary>
    'Private Sub AddAutoIncrementColumn(mydt As DataTable)
    '    Using autoIncNo As New DataColumn("Bil", GetType(Int32))
    '        autoIncNo.AllowDBNull = False
    '        autoIncNo.AutoIncrement = True
    '        autoIncNo.AutoIncrementSeed = 1
    '        autoIncNo.AutoIncrementStep = 1
    '        autoIncNo.Unique = True
    '        mydt.Columns.Add(autoIncNo)
    '    End Using
    'End Sub

    Private Sub fBindGvPTJ(strSql As String)

        If strSql = "" Then
            strSql = "Select KodPTJ, Butiran, Singkatan, Status from MK_PTJ order by KodPTJ"
        End If

        Dim dtPTJ = fCreateDt(strSql)

        If dtPTJ.Rows.Count > 0 Then

            gvPTJ.DataSource = dtPTJ
            gvPTJ.DataBind()

        Else
            fGlobalAlert("Tiada Rekod Dijumpai", Me.Page, Me.GetType())
        End If

    End Sub

    Private Function fCreateDt(strSql As String) As DataTable
        Dim dbconn As New DBKewConn

        'pass as reference 
        Dim dsPTJ = dbconn.fselectCommand(strSql)

        'Convert dataset to datatable
        Dim dtPTJ = dsPTJ.Tables(0)

        If dtPTJ.Rows.Count > 0 Then

            dtPTJ.Columns.Add("StrStatus", GetType(String))

            For Each row As DataRow In dtPTJ.Rows
                If row("Status") = 0 Then
                    row("StrStatus") = "Tidak Aktif"
                Else
                    row("StrStatus") = "Aktif"
                End If
            Next
        End If

        Return dtPTJ
    End Function

    Protected Sub btnSavePTJ_Click(sender As Object, e As EventArgs) Handles btnSavePTJ.Click
        Try
            Page.Validate()
            If Page.IsValid Then

                Dim strSql = $"select count(*) from mk_ptj where kodptj = '{txtKodPTJ.Text}'"
                Dim dbconn As New DBKewConn

                If dbconn.fSelectCount(strSql) > 0 Then
                    fGlobalAlert("Rekod sudah ada!. Tidak boleh buat penambahan rekod yang sama", Me.Page, Me.[GetType]())
                Else
                    Dim stat As Boolean = False
                    If rbStatus.SelectedIndex = 0 Then
                        stat = True
                    End If

                    strSql = "INSERT INTO MK_PTJ VALUES (@KodPTjss, @Butiranss, @Singkatanss, @Statusss)"

                    Dim paramSql() As SqlParameter = {
                        New SqlParameter("@KodPTjss", txtKodPTJ.Text),
                        New SqlParameter("@Butiranss", txtButiran.Text),
                        New SqlParameter("@Singkatanss", txtSingkatan.Text),
                        New SqlParameter("@Statusss", stat)
                    }

                    'strSql = $"INSERT INTO MK_PTJ VALUES ('{txtKodPTJ.Text}','{txtButiran.Text}','{txtSingkatan.Text}','{stat}')"
                    If dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                        fGlobalAlert("Rekod baru telah disimpan!", Me.Page, Me.[GetType]())
                        btnReset_Click(sender, e)
                        fBindGvPTJ("")
                    Else
                        fGlobalAlert("Rekod tidak berjaya disimpan!", Me.Page, Me.[GetType]())
                    End If
                End If
            End If
        Catch ex As Exception
            '("Jenis_Permohonan.aspx(btnSave_Click)- " & ex.Message.ToString)
        End Try
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        txtButiran.Text = ""
        txtKodPTJ.Text = ""
        txtSingkatan.Text = ""
        rbStatus.SelectedIndex = 0
    End Sub

    Protected Sub gvPTJ_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvPTJ.Sorting
        Try
            Dim sortingDirection As String = String.Empty
            If direction = SortDirection.Ascending Then
                direction = SortDirection.Descending
                sortingDirection = "Desc"
            Else
                direction = SortDirection.Ascending
                sortingDirection = "Asc"
            End If

            Dim strSql = "Select KodPTJ, Butiran, Singkatan, Status from MK_PTJ order by KodPTJ"
            Dim dt = fCreateDt(strSql)
            If Not dt Is Nothing Then
                Using dvPTJ As New DataView(dt)
                    dvPTJ.Sort = e.SortExpression & " " & sortingDirection
                    Session("SortedView") = dvPTJ
                    gvPTJ.DataSource = dvPTJ
                    gvPTJ.DataBind()
                End Using
            End If
        Catch ex As Exception
            '(ex.Message.ToString)
        End Try
    End Sub

    Protected Sub gvPTJ_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvPTJ.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                'onclick every row
                'e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvPTJ, "Select$" & e.Row.RowIndex)

                If e.Row.RowState And DataControlRowState.Edit Then

                    ' get selected value of dropdownlist
                    Dim ddlStat As DropDownList = DirectCast(e.Row.FindControl("ddlStrStatus"), DropDownList)
                    Dim str As String = DataBinder.Eval(e.Row.DataItem, "StrStatus").ToString
                    ddlStat.Items.FindByText(str).Selected = True
                End If
            End If
        Catch ex As Exception
            '(ex.Message.ToString)
        End Try

    End Sub

    Protected Sub gvPTJ_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvPTJ.RowCancelingEdit
        gvPTJ.EditIndex = -1
        fBindGvPTJ("")
    End Sub

    Protected Sub gvPTJ_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvPTJ.PageIndexChanging
        gvPTJ.PageIndex = e.NewPageIndex

        ' Cancel the paging operation if the user attempts to navigate
        ' to another page while the GridView control is in edit mode. 
        If gvPTJ.EditIndex <> -1 Then

            ' Use the Cancel property to cancel the paging operation.
            e.Cancel = True

            ' Display an error message.
            Dim newPageNumber As Integer = e.NewPageIndex + 1
            fGlobalAlert($"Please update the record before moving To page '{newPageNumber.ToString}'.", Me.Page, Me.[GetType]())
        Else
            If Session("SortedView") IsNot Nothing Then
                gvPTJ.DataSource = Session("SortedView")
                gvPTJ.DataBind()
            Else
                fBindGvPTJ("")
            End If
        End If
    End Sub

    Protected Sub gvPTJ_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvPTJ.RowDeleting
        Try
            Dim strSql As String
            Dim dbconn As New DBKewConn
            Dim strKodPTj As String

            Dim row As GridViewRow = DirectCast(gvPTJ.Rows(e.RowIndex), GridViewRow)
            strKodPTj = Trim(row.Cells(1).Text.ToString.TrimEnd)

            strSql = "DELETE from MK_PTJ where KodPTJ = '" & strKodPTj & "'"
            If dbconn.fUpdateCommand(strSql) > 0 Then
                fGlobalAlert("Rekod telah dipadam!", Me.Page, Me.[GetType]())
                fBindGvPTJ("")
            End If

        Catch ex As Exception
            '("Jenis_Permohonan.aspx(gvJenPermohonan_RowDeleting)- " & ex.Message.ToString)
        End Try
    End Sub

    Protected Sub gvPTJ_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvPTJ.RowEditing
        gvPTJ.EditIndex = e.NewEditIndex
        fBindGvPTJ("")
    End Sub

    Protected Sub gvPTJ_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvPTJ.RowUpdating
        'Retrieve the table from the session object.
        Try

            'Update the values.
            Dim row = gvPTJ.Rows(e.RowIndex)
            Dim kodPTJ As String = row.Cells(1).Text.ToString
            Dim Butiran As String = CType((row.Cells(2).FindControl("txtButiran")), TextBox).Text
            Dim Singkatan As String = CType((row.Cells(3).FindControl("txtSingkatan")), TextBox).Text
            Dim Status As String = CType((row.Cells(4).FindControl("ddlStrStatus")), DropDownList).SelectedValue

            'Reset the edit index.
            gvPTJ.EditIndex = -1

            Dim dbconn As New DBKewConn

            Dim strSql As String = "UPDATE MK_PTJ SET Butiran=@Butiranss,Singkatan=@Singkatanss,Status=@Statusss WHERE kodPTJ=@kodPTJss"
            Dim paramSql() As SqlParameter = {
                        New SqlParameter("@Butiranss", Butiran),
                        New SqlParameter("@Singkatanss", Singkatan),
                        New SqlParameter("@Statusss", Status),
                        New SqlParameter("@kodPTJss", kodPTJ)
                        }

            If dbconn.fUpdateCommand(strSql, paramSql) > 0 Then

                'Bind data to the GridView control.
                fBindGvPTJ("")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        Try
            Dim strSql As String = ""

            If txtCari.Text IsNot "" Then
                If ddlCari.SelectedValue = "KodPTJ" Then
                    strSql = "SELECT * FROM mk_ptj WHERE kodptj LIKE '" + txtCari.Text + "%'"
                ElseIf ddlCari.SelectedValue = "Butiran" Then
                    strSql = "SELECT * FROM mk_ptj WHERE Butiran LIKE '" + txtCari.Text + "%'"
                ElseIf ddlCari.SelectedValue = "Singkatan" Then
                    strSql = "SELECT * FROM mk_ptj WHERE Singkatan LIKE '" + txtCari.Text + "%'"
                ElseIf ddlCari.SelectedValue = "Status" Then
                    strSql = "SELECT * FROM mk_ptj WHERE Status LIKE '" + txtCari.Text + "%'"
                End If
            Else
                strSql = "SELECT * FROM MK_PTJ ORDER BY kodptj"
            End If

            fBindGvPTJ(strSql)

        Catch ex As Exception
            '("Jenis_Permohonan.aspx(gvJenPermohonan_RowDeleting)- " & ex.Message.ToString)
        End Try
    End Sub

    Protected Sub ddlSaizRekod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSaizRekod.SelectedIndexChanged
        gvPTJ.PageSize = CInt(ddlSaizRekod.SelectedValue)
        fBindGvPTJ("")
    End Sub

    'Protected Sub gvPTJ_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvPTJ.SelectedIndexChanged

    '    ' Get the selected row.
    '    'Dim row As GridViewRow = gvPTJ.SelectedRow

    '    ' Check the row state. If the row is not in edit mode and is selected,
    '    ' exit edit mode. This ensures that the GridView control exits edit mode
    '    ' when a user selects a different row while the GridView control is in 
    '    ' edit mode. Notice that the DataControlRowState enumeration is a flag
    '    ' enumeration, which means that you can combine values using bitwise
    '    ' operations.
    '    'If row.RowState <> (DataControlRowState.Edit Or DataControlRowState.Selected) Then
    '    '    gvPTJ.EditIndex = -1
    '    '    fBindGvPTJ("")
    '    'End If
    'End Sub

End Class