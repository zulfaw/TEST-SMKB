Imports System.Web.Configuration
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization
Imports System.IO


Public Class Peruntukan_Universiti
    Inherits System.Web.UI.Page
    Dim clsMail As New clsMail.Mail

    Dim strSMTPServer As String = WebConfigurationManager.AppSettings("SMTPServer")
    Dim strSMTPPort As String = WebConfigurationManager.AppSettings("SMTPPort")
    Dim strSenderAdr As String = WebConfigurationManager.AppSettings("SenderAddr")

    Dim dbconn As New DBKewConn

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (Not Page.IsPostBack) Then
            'fBindGV()
            fBindGVBajet()
            Dim strNoStaf As String = Session("ssusrID")
        End If

    End Sub

    Private Sub gvPeruntukan_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvPeruntukan.PageIndexChanging

        Try

            gvPeruntukan.PageIndex = e.NewPageIndex
            If Session("SortedView") IsNot Nothing Then
                gvPeruntukan.DataSource = Session("SortedView")
                gvPeruntukan.DataBind()
            Else
                Dim dt As New DataTable
                dt = fCreateDt()
                gvPeruntukan.DataSource = dt
                gvPeruntukan.DataBind()

            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Function fCreateDts(strSql As String) As DataTable
        Dim ds = dbconn.fSelectCommand(strSql)

        'Convert dataset to datatable
        Dim dt = ds.Tables(0)


        If dt.Rows.Count > 0 Then
            Dim strDate As DateTime = Nothing


            For Each row As DataRow In dt.Rows

            Next
        End If

        Return dt
    End Function

    Private Function fCreateDt()
        Try

            'dt.Columns.Add("BG17_Tahun", GetType(String))
            'dt.Columns.Add("BG17_AgihanKPT", GetType(String))
            'dt.Columns.Add("BG17_LulusKPT", GetType(String))
            'dt.Columns.Add("BG17_Reserved", GetType(String))
            'dt.Columns.Add("BG17_GeranKerajaan", GetType(String))

            'Dim strTahun As String

            'Dim decJumAgih As Decimal
            'Dim strJumAgih As String

            'Dim decJumLulus As Decimal
            'Dim strJumLulus As String

            'Dim decJumReserved As Decimal
            'Dim strJumReserved As String

            'Dim decJumGeran As Decimal
            'Dim strJumGeran As String


        Catch ex As Exception

        End Try
    End Function

    Private Sub gvPeruntukan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvPeruntukan.SelectedIndexChanged
        Try

            Dim row As GridViewRow = gvPeruntukan.SelectedRow
            'Dim tahun = CType(row.FindControl("lblTahun"), Label).Text.ToString()
            'Dim agih = CType(row.FindControl("lblAgih"), Label).Text.ToString()
            'Dim lulus = CType(row.FindControl("lblLulus"), Label).Text.ToString()
            'Dim reserved = CType(row.FindControl("lblReserved"), Label).Text.ToString()
            'Dim geran = CType(row.FindControl("lblGeran"), Label).Text.ToString()

            row.FindControl("txtAgihanKPT").Visible = True
            row.FindControl("lblAgih").Visible = False

            row.FindControl("txtLulusKPT").Visible = True
            row.FindControl("lblLulus").Visible = False

            row.FindControl("txtReserved").Visible = True
            row.FindControl("lblReserved").Visible = False

            row.FindControl("txtGeran").Visible = True
            row.FindControl("lblGeran").Visible = False

            'lblYear.Text = tahun
            'txtAgih.Text = agih
            'txtLulus.Text = lulus
            'txtReserved.Text = reserved
            'txtGeran.Text = geran

            'mpePnlSenarai.Show()

        Catch

        End Try

    End Sub


    Private Sub gvPeruntukan_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvPeruntukan.Sorting

        Try

            Dim sortingDirection As String = String.Empty
            If direction = SortDirection.Ascending Then
                direction = SortDirection.Descending
                sortingDirection = "Desc"
            Else
                direction = SortDirection.Ascending
                sortingDirection = "Asc"
            End If

            Dim sortedView As New DataView(fCreateDt())
            sortedView.Sort = Convert.ToString(e.SortExpression + " ") & sortingDirection
            Session("SortedView") = sortedView
            gvPeruntukan.DataSource = sortedView
            gvPeruntukan.DataBind()

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

    Private Function fBindGV()

        Try

            Dim dt As New DataTable
            dt = fCreateDt()

            lblJumRekod.InnerText = dt.Rows.Count

            gvPeruntukan.DataSource = dt
            gvPeruntukan.DataBind()

        Catch ex As Exception

        End Try

    End Function



    Private Function ConvertSortDirectionToSql(sortDirection__1 As SortDirection) As String
        Dim newSortDirection As String = [String].Empty

        Select Case sortDirection__1
            Case SortDirection.Ascending
                newSortDirection = "ASC"
                Exit Select

            Case SortDirection.Descending
                newSortDirection = "DESC"
                Exit Select
        End Select

        Return newSortDirection
    End Function

    Private Sub fBindGVBajet()
        Dim strSql As String
        'Dim ds 'As New DataSet
        Dim dt As New DataTable

        strSql = $"SELECT BG17_Tahun , isnull(BG17_AgihanKPT,0.00) as BG17_AgihanKPT , isnull(BG17_LulusKPT,0.00) as BG17_LulusKPT , isnull(BG17_AgihanLPU,0.00) as BG17_AgihanLPU  , isnull(BG17_Reserved,0.00) as BG17_Reserved  , isnull(BG17_GeranKerajaan,0.00)  as BG17_GeranKerajaan
                    FROM BG17_Agihan
                    ORDER BY BG17_Tahun DESC"
        dt = fCreateDts(strSql)

        lblJumRekod.InnerText = dt.Rows.Count

        gvPeruntukan.DataSource = dt
        gvPeruntukan.DataBind()

    End Sub

    Private Sub btnTutup_ServerClick(sender As Object, e As EventArgs) Handles btnTutup.ServerClick
        mpePnlSenarai.Hide()
    End Sub


    Private Sub linkSimpan_Click(sender As Object, e As EventArgs) Handles linkSimpan.Click
        'sql simpan
        Dim row As GridViewRow = gvPeruntukan.SelectedRow

        Dim agih = CType(row.FindControl("txtAgihanKPT"), TextBox).Text.ToString()
        Dim lulus = CType(row.FindControl("txtLulusKPT"), TextBox).Text.ToString()
        Dim reserved = CType(row.FindControl("txtReserved"), TextBox).Text.ToString()
        Dim geran = CType(row.FindControl("txtGeran"), TextBox).Text.ToString()

        Dim strSQL_Save = $"UPDATE 
                    BG17_Agihan SET BG17_AgihanKPT = @agih, BG17_LulusKPT=@lulus, BG17_Reserved=@reserved, BG17_GeranKerajaan =@geran Where BG17_Tahun = '2022'"


        Dim paramSql_simpan() As SqlParameter =
                         {
                         New SqlParameter("@agih", agih),
                         New SqlParameter("@lulus", lulus),
                         New SqlParameter("@reserved", reserved),
                         New SqlParameter("@geran", geran)
                         }

        dbconn.sConnBeginTrans()
        If dbconn.fUpdateCommand(strSQL_Save, paramSql_simpan) > 0 Then
            dbconn.sConnCommitTrans()

            fGlobalAlert($"Berjaya disimpan!", Me.Page, Me.[GetType](), "../../BAJET/PERUNTUKAN UNIVERSITI/Peruntukan_Universiti.aspx")
        Else
            fGlobalAlert($"Tidak berjaya disimpan!", Me.Page, Me.[GetType](), "../../BAJET/PERUNTUKAN UNIVERSITI/Peruntukan_Universiti.aspx")

        End If


    End Sub

    Private Sub gvPeruntukan_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvPeruntukan.RowDataBound
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim tahun As String = DataBinder.Eval(e.Row.DataItem, "BG17_Tahun").ToString()
                Dim tahun_depan = Date.Now.Year.ToString() + 1
                If tahun = tahun_depan Then
                    e.Row.FindControl("lbtnSelect").Visible = True
                End If

            End If

        Catch ex As Exception
        End Try
    End Sub

End Class