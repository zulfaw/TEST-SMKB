Public Class Semakan_Syarikat_Berdaftar
    Inherits System.Web.UI.Page

    Dim dbconn As New DBKewConn

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            fBindGVSya("")
        End If

    End Sub


    Private Sub fBindGVSya(strSql As String)

        If strSql Is String.Empty Then
            strSql = $"SELECT ROC01_NoSya, c.Butiran As StatAktif, ROC01_NamaSya, 
ROC01_TkhLulus,ROC01_TkhSahKemaskini, ROC01_TkhDaftar, ROC01_IDSem, ROC01_Bekalan, ROC01_Perkhidmatan, ROC01_Kerja
from ROC01_Syarikat a, ROC_StatAktif c
WHERE c.KodAktif = a.ROC01_KodAktif and (ROC01_KategoriSya IS NULL or  ROC01_KategoriSya = 1)"
        End If

        ViewState("Str") = strSql
        Dim ds = dbconn.fselectCommand(strSql)

        If ds IsNot Nothing Then
            Dim dt = ds.Tables(0)

            gvSyarikat.DataSource = fCreateDt(dt)
            gvSyarikat.DataBind()

            lblJumRekod.InnerText = dt.Rows.Count
        End If

    End Sub

    Private Function fCreateDt(dt As DataTable) As DataTable
        dt.Columns.Add("NamaNiaga", GetType(String))
        For Each row In dt.Rows
            Dim strNamaNiaga = ""

            If IIf(IsDBNull(row("ROC01_Bekalan")), False, row("ROC01_Bekalan")) Then
                strNamaNiaga = "B "
            End If
            If IIf(IsDBNull(row("ROC01_Perkhidmatan")), False, row("ROC01_Perkhidmatan")) Then
                strNamaNiaga = strNamaNiaga + "P "
            End If
            If IIf(IsDBNull(row("ROC01_Kerja")), False, row("ROC01_Kerja")) Then
                strNamaNiaga = strNamaNiaga + "K "
            End If
            row("NamaNiaga") = strNamaNiaga
        Next
        Return dt
    End Function

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click

        Dim strSql As String = ""
        If txtCari.Text IsNot "" Then
                If ddlCari.SelectedValue = 1 Then
                'No Syarikat
                strSql = $"SELECT ROC01_NoSya, c.Butiran As StatAktif, ROC01_NamaSya, 
ROC01_TkhLulus,ROC01_TkhSahKemaskini, ROC01_TkhDaftar, ROC01_IDSem, ROC01_Bekalan, ROC01_Perkhidmatan, ROC01_Kerja
from ROC01_Syarikat a, ROC_StatAktif c
WHERE c.KodAktif = a.ROC01_KodAktif AND ROC01_NoSya LIKE '%{txtCari.Text}%'"
            ElseIf ddlCari.SelectedValue = 2 Then
                'Nama Syarikat
                strSql = $"SELECT ROC01_NoSya, c.Butiran As StatAktif, ROC01_NamaSya, 
ROC01_TkhLulus,ROC01_TkhSahKemaskini, ROC01_TkhDaftar, ROC01_IDSem, ROC01_Bekalan, ROC01_Perkhidmatan, ROC01_Kerja
from ROC01_Syarikat a, ROC_StatAktif c
WHERE c.KodAktif = a.ROC01_KodAktif AND ROC01_NamaSya LIKE '%{txtCari.Text}%'"
            ElseIf ddlCari.SelectedValue = 3 Then
                'Tahun
                strSql = $"SELECT ROC01_NoSya, c.Butiran As StatAktif, ROC01_NamaSya, 
ROC01_TkhLulus,ROC01_TkhSahKemaskini, ROC01_TkhDaftar, ROC01_IDSem, ROC01_Bekalan, ROC01_Perkhidmatan, ROC01_Kerja
from ROC01_Syarikat a, ROC_StatAktif c
WHERE c.KodAktif = a.ROC01_KodAktif AND YEAR(ROC01_TkhLulus) = {txtCari.Text}"
            ElseIf ddlCari.SelectedValue = 4 Then
                'Status
                strSql = $"SELECT ROC01_NoSya, c.Butiran As StatAktif, ROC01_NamaSya, 
ROC01_TkhLulus,ROC01_TkhSahKemaskini, ROC01_TkhDaftar, ROC01_IDSem, ROC01_Bekalan, ROC01_Perkhidmatan, ROC01_Kerja
from ROC01_Syarikat a, ROC_StatAktif c
WHERE c.KodAktif = a.ROC01_KodAktif AND c.Butiran like '{txtCari.Text}%'"
            End If

                fBindGVSya(strSql)
            Else
            fBindGVSya("")
        End If
    End Sub

    Protected Sub ddlSaizRekod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSaizRekod.SelectedIndexChanged
        gvSyarikat.PageSize = CInt(ddlSaizRekod.SelectedValue)
        BindGridViewButiran()
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

    Protected Function fCreateDt() 
        Dim str = ViewState("Str")
        Using dt = dbconn.fSelectCommandDt(str)
            Return fCreateDt(dt)
        End Using
    End Function

    Protected Sub gvSyarikat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvSyarikat.SelectedIndexChanged
        Dim KodSubMenu = Request.QueryString("KodSubMenu")
        Dim KodSub = Request.QueryString("KodSub")
        Dim noSya As String = gvSyarikat.SelectedRow.Cells(1).Text
        Dim noSemSya As String = CType(gvSyarikat.SelectedRow.FindControl("lblIDSem"), Label).Text
        Response.Redirect($"~/FORMS/PENDAFTARAN SYARIKAT/SYARIKAT BERDAFTAR/Maklumat_Vendor.aspx?KodSub={KodSub}&KodSubMenu={KodSubMenu}&noSya={noSya}&noSem={noSemSya}")
    End Sub

    Protected Sub gvSyarikat_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvSyarikat.PageIndexChanging
        gvSyarikat.PageIndex = e.NewPageIndex
        Try
            If Session("SortedView") IsNot Nothing Then
                gvSyarikat.DataSource = Session("SortedView")
                gvSyarikat.DataBind()

                'Dim dt = DirectCast(Session("SortMohonBajetButiran"), DataTable)
                'dt.DefaultView.Sort = ViewState("SortExpression") & " " & ViewState("SortDirection")
                'gvCetakPT.DataSource = dt
                'gvCetakPT.DataBind()
            Else
                BindGridViewButiran()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub BindGridViewButiran()
        Dim str = ViewState("Str")
        Dim ds = dbconn.fselectCommand(str)
        Dim dt = ds.Tables(0)

        gvSyarikat.DataSource = fCreateDt(dt)
        gvSyarikat.DataBind()

        lblJumRekod.InnerText = dt.Rows.Count
    End Sub

    Protected Sub gvSyarikat_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvSyarikat.Sorting

        Dim sortedView As New DataView(fCreateDt())
        sortedView.Sort = e.SortExpression & " " & GetSortDirection(e.SortExpression)
        Session("SortedView") = sortedView
        gvSyarikat.DataSource = sortedView
        gvSyarikat.DataBind()
    End Sub
End Class