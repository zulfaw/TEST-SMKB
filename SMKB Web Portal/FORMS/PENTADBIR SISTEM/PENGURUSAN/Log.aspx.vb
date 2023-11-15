Imports System.Globalization
Public Class Log
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Session("LoggedIn") Then
                    fBindDdlKodModul()
                    txtUserID.Text = ""


                    If gvAuditLog.Rows.Count = 0 Then
                        gvAuditLog.DataSource = New List(Of String)
                        gvAuditLog.DataBind()
                    End If

                    ddlKodModul.Items.Insert(0, New ListItem(String.Empty, String.Empty))
                    ddlKodModul.SelectedIndex = 0
                End If
            Else
                Response.Redirect("../../Logout.aspx")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindDdlKodModul()

        Try

            Dim strSql As String = "Select KodModul, DisModul, (KodModul + ' - ' + DisModul) as Butiran From MK_UModul ORDER BY KODMODUL"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            ddlKodModul.DataSource = ds
            ddlKodModul.DataTextField = "Butiran"
            ddlKodModul.DataValueField = "KodModul"
            ddlKodModul.DataBind()

        Catch ex As Exception
            '("Pendaftaran_Menu.aspx(fBindDdlKodModul)- " & ex.Message.ToString)
        End Try
    End Sub

    Private Sub fBindDdlKodSubModul(ByVal strKodModul As String)
        Try
            Dim strSql As String = $"select KodSub, DisSub, (KodSub + ' - ' + DisSub) as Butiran From MK_USubModul WHERE kodModul = '{strKodModul}' ORDER BY KODSUB"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            ddlKodSubModul.DataSource = ds
            ddlKodSubModul.DataTextField = "Butiran"
            ddlKodSubModul.DataValueField = "KodSub"
            ddlKodSubModul.DataBind()

            'Trigger selectedindexchanged dropdownlist
            ddlKodSubModul_SelectedIndexChanged(Nothing, Nothing)

        Catch ex As Exception
            '("Pendaftaran_Menu.aspx(fBindDdlKodSubModul)- " & ex.Message.ToString)
        End Try
    End Sub
    Protected Sub OnBtnCari(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Page.Validate()
            If Page.IsValid Then
                Dim strSqlCari As String = Nothing
                Dim startDate As DateTime = DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", New CultureInfo("en-US"))
                Dim endDate As DateTime = DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", New CultureInfo("en-US"))

                If ddlKodSubMenu.Items.Count > 0 Then
                    'Jika submenu dropdownlist sudah populate
                    If txtUserID.Text Is String.Empty Then
                        'Jika pilih tarikh tanpa userid
                        strSqlCari = $"Select UserID, Tarikh, Masa, keterangan, UserUbah, SubMenu, InfoTable from MK_AuditLog WHERE (Tarikh >= CONVERT(DATETIME, '{startDate}', 102) AND Tarikh <= CONVERT(DATETIME, '{endDate}', 102)) AND SubMenu='{ddlKodSubMenu.SelectedValue}' ORDER BY Tarikh, Masa"
                    Else
                        'Jika pilih tarikh dan userid
                        strSqlCari = $"Select UserID, Tarikh, Masa, keterangan, UserUbah, SubMenu, InfoTable from MK_AuditLog WHERE (Tarikh >= CONVERT(DATETIME, '{startDate}', 102) AND Tarikh <= CONVERT(DATETIME, '{endDate}', 102)) AND SubMenu='{ddlKodSubMenu.SelectedValue}' AND UserID ='{txtUserID.Text}' ORDER BY Tarikh, Masa"
                    End If

                Else
                    'Jika tidak dropdownlist tidak populate
                    If txtUserID.Text Is String.Empty Then
                        'Jika pilih tarikh tanpa userid
                        strSqlCari = $"Select UserID, Tarikh, Masa, keterangan, UserUbah, SubMenu, InfoTable from MK_AuditLog WHERE (Tarikh >= CONVERT(DATETIME, '{startDate}', 102) AND Tarikh <= CONVERT(DATETIME, '{endDate}', 102)) ORDER BY Tarikh, Masa"
                    Else
                        'Jika pilih tarikh dan userid
                        strSqlCari = $"Select UserID, Tarikh, Masa, keterangan, UserUbah, SubMenu, InfoTable from MK_AuditLog WHERE (Tarikh >= CONVERT(DATETIME, '{startDate}', 102) AND Tarikh <= CONVERT(DATETIME, '{endDate}', 102)) AND UserID ='{txtUserID.Text}' ORDER BY Tarikh, Masa"
                    End If
                End If

                ViewState("SqlCari") = strSqlCari

                gvAuditLog.DataSource = fCreateDtAuditLog(strSqlCari)
                gvAuditLog.DataBind()

            End If
        Catch ex As Exception
            '("Jenis_Permohonan.aspx(btnSave_Click)- " & ex.Message.ToString)
        End Try
    End Sub
    Private Function fCreateDtAuditLog(strSql As String)
        Dim ds As New DataSet
        Dim dbconn As New DBKewConn
        Dim dt As New DataTable
        ds = dbconn.fSelectCommand(strSql)
        Dim strDate As DateTime = Nothing

        If ds.Tables(0).Rows.Count > 0 Then
            dt.Columns.Add("Bil", GetType(Int32))
            dt.Columns.Add("UserID", GetType(String))
            dt.Columns.Add("Tarikh", GetType(String))
            dt.Columns.Add("Masa", GetType(String))
            dt.Columns.Add("Keterangan", GetType(String))
            dt.Columns.Add("UserUbah", GetType(String))
            dt.Columns.Add("SubMenu", GetType(String))
            dt.Columns.Add("InfoTable", GetType(String))

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                Dim tableRow = dt.NewRow()
                tableRow("Bil") = i + 1
                tableRow("UserID") = ds.Tables(0).Rows(i)(0).ToString
                strDate = ds.Tables(0).Rows(i)(1).ToString
                tableRow("Tarikh") = strDate.ToString("dd/MM/yyyy")
                tableRow("Masa") = ds.Tables(0).Rows(i)(2).ToString
                tableRow("keterangan") = ds.Tables(0).Rows(i)(3).ToString
                tableRow("UserUbah") = ds.Tables(0).Rows(i)(4).ToString
                tableRow("SubMenu") = ds.Tables(0).Rows(i)(5).ToString
                tableRow("InfoTable") = ds.Tables(0).Rows(i)(6).ToString
                dt.Rows.Add(tableRow)
            Next
        Else
            fGlobalAlert("Tiada rekod dijumpai!", Me.Page, Me.[GetType]())
        End If
        Return dt
    End Function
    Protected Sub ddlKodModul_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKodModul.SelectedIndexChanged
        Try
            Dim strKodModul As String
            strKodModul = ddlKodModul.SelectedValue.ToString

            fBindDdlKodSubModul(strKodModul)

        Catch ex As Exception
            '("Pendaftaran_Menu.aspx(ddlKodModul_SelectedIndexChanged)- " & ex.Message.ToString)
        End Try
    End Sub

    Protected Sub ddlKodSubModul_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKodSubModul.SelectedIndexChanged
        Try
            Dim strKodSubModul As String
            strKodSubModul = ddlKodSubModul.SelectedValue.ToString

            fBindDdlKodSubMenu(strKodSubModul)

        Catch ex As Exception
            '("Pendaftaran_Menu.aspx(ddlKodModul_SelectedIndexChanged)- " & ex.Message.ToString)
        End Try
    End Sub

    Private Sub fBindDdlKodSubMenu(ByVal strKodSubModul As String)
        Try
            Dim strSql As String = $"select KodSubMenu, (KodSubMenu + ' - ' + NamaSubMenu) AS Butiran from MK_USubMenu WHERE KodSub='{strKodSubModul}' AND Status ='True' ORDER BY KodSubMenu"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)
            If ds.Tables.Count > 0 Then
                ddlKodSubMenu.DataSource = ds
                ddlKodSubMenu.DataTextField = "Butiran"
                ddlKodSubMenu.DataValueField = "KodSubMenu"
                ddlKodSubMenu.DataBind()
            End If

        Catch ex As Exception
            '("Pendaftaran_Menu.aspx(fBindDdlKodSubModul)- " & ex.Message.ToString)
        End Try
    End Sub

    Protected Sub gvAuditLog_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvAuditLog.Sorting
        Try
            Dim sortingDirection As String = String.Empty
            If direction = SortDirection.Ascending Then
                direction = SortDirection.Descending
                sortingDirection = "Desc"
            Else
                direction = SortDirection.Ascending

                sortingDirection = "Asc"
            End If
            Dim sortedView As New DataView(fCreateDtAuditLog(ViewState("SqlCari")))
            sortedView.Sort = Convert.ToString(e.SortExpression + " ") & sortingDirection
            Session("SortedView") = sortedView
            gvAuditLog.DataSource = sortedView
            gvAuditLog.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub gvAuditLog_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvAuditLog.PageIndexChanging
        Try
            gvAuditLog.PageIndex = e.NewPageIndex
            If Session("SortedView") IsNot Nothing Then
                gvAuditLog.DataSource = Session("SortedView")
                gvAuditLog.DataBind()
            Else
                gvAuditLog.DataSource = fCreateDtAuditLog(ViewState("SqlCari"))
                gvAuditLog.DataBind()
            End If

        Catch ex As Exception
            '("")
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