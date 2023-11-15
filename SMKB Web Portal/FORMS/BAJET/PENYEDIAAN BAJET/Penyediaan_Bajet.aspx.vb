Imports System.Drawing


Public Class Penyediaan_Bajet
    Inherits System.Web.UI.Page
    Protected TotalRec As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim datePrev As Date = Now.AddYears(-1)
            Dim dateNext As Date = Now.AddYears(+1)
            Dim strYearPrev As String = datePrev.Year
            txtYear.Text = dateNext.Year

            'If Session("LoggedIn") <> "" Then
            fBindDdlPTj()
            fBindgvPermohonan()
        End If

        TotalRec = ViewState("TotalRec")

    End Sub

    Private Sub fBindDdlPTj()
        Try
            Dim strSql As String = "select KodPTJ ,(KodPTJ +' - '+ Butiran ) as Butiran  from MK_PTJ  where status = 1 order by KodPTJ "
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fselectCommand(strSql)

            ddlPTj.DataSource = ds
            ddlPTj.DataTextField = "Butiran"
            ddlPTj.DataValueField = "KodPTJ"
            ddlPTj.DataBind()

            ddlPTj.Items.Insert(0, New ListItem("-KESELURUHAN-", "0"))
            ddlPTj.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindDdlFilPTj()

        Try

            ddlPTj.Items.Insert(0, New ListItem("-KESELURUHAN-", "00"))
            ddlPTj.Items.Insert(1, New ListItem("02 - PEJABAT PENDAFTAR", "02"))
            ddlPTj.Items.Insert(2, New ListItem("03 - PEJABAT BENDAHARI", "03"))
            ddlPTj.Items.Insert(3, New ListItem("41 - PUSAT PERKHIDMATAN PENGETAHUAN DAN KOMUNIKASI", "41"))
            ddlPTj.SelectedIndex = 0

        Catch ex As Exception

        End Try

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Private Sub fBindgvPermohonan()
        Try

            Dim ds As New DataSet
            Dim strsql As String = "select a.BG21_NoMohonPTj ,a.KodPTj , (select b.KodPTJ + ' - ' + b.Butiran  from MK_PTJ b where b.KodPTJ = a.KodPTj ) as PTj, 
a.BG21_TarikhHantar, a.BG21_AmaunMohon, (select  d.BG_Butiran  from BG_StatusDok d where d.BG_KodStatus = a.StatusDok   ) as ButiranStat  from BG21_MohonPTj a where StatusDok = '06' and BG21_TahunBajet = '" & Trim(txtYear.Text.TrimEnd) & "'"

            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            Dim dt As New DataTable
            dt.Columns.Add("BG21_NoMohonPTj", GetType(String))
            dt.Columns.Add("KodPTj", GetType(String))
            dt.Columns.Add("PTj", GetType(String))
            dt.Columns.Add("BG21_TarikhHantar", GetType(String))
            dt.Columns.Add("BG21_AmaunMohon", GetType(String))
            dt.Columns.Add("ButiranStat", GetType(String))


            Dim strNoMohon As String
            Dim strKodPTj As String
            Dim strPTj As String
            Dim strTarikhHantar As String
            Dim decAmaunMohon As Decimal
            Dim strAmaunMohon As String

            Dim strTarikhMohon As String
            Dim strButiranStat As String
            'Dim strNoStatusDok As String

            ViewState("TotalRec") = ds.Tables(0).Rows.Count
            TotalRec = ViewState("TotalRec")

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                strNoMohon = ds.Tables(0).Rows(i).Item("BG21_NoMohonPTj").ToString
                strKodPTj = ds.Tables(0).Rows(i).Item("KodPTj").ToString
                strPTj = ds.Tables(0).Rows(i).Item("PTj").ToString
                strTarikhHantar = ds.Tables(0).Rows(i).Item("BG21_TarikhHantar").ToString
                decAmaunMohon = ds.Tables(0).Rows(i).Item("BG21_AmaunMohon")
                strAmaunMohon = decAmaunMohon.ToString("#,##0.00")
                strButiranStat = ds.Tables(0).Rows(i).Item("ButiranStat").ToString

                dt.Rows.Add(strNoMohon, strKodPTj, strPTj, strTarikhHantar, strAmaunMohon, strButiranStat)
            Next

            gvPermohonan.DataSource = dt
            gvPermohonan.DataBind()
            'If Session("TotalRec") = 0 Then
            '    Session("strNotice") = "Tiada rekod permohonan bajet"

            'Else

            '    If strNoStatusDok = "06" Then
            '        Session("strNotice") = "senarai Permohonan Bajet telah dihantar ke Pejabat Bendahari"

            '    Else

            '    End If


            'End If

        Catch ex As Exception

        End Try
    End Sub

End Class