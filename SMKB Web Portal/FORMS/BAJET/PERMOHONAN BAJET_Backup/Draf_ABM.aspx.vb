Imports System.Drawing
Imports System.Data.SqlClient
Public Class Draf_ABM
    Inherits System.Web.UI.Page

    Protected TotalRecProg, TotalRecDt As String
    Protected strStatMohon As String
    Protected strMsg, strNotice As String
    Protected TotalRec As String
    Protected lblPeruntukanPrev, lblPerbelanjaanPrev, lblAnggaranSyorNow, lblAnggaranMintaNext, lblAnggaranSyorNext As String

    Public Sub New()
        fPopMohonDt()
    End Sub

    Private Sub fPopMohonDt()
        Try
            Dim ds As New DataSet
            Dim dv As New DataView
            Dim strSql As String = "Select sum( a.BG20_AngJumlah) as Jumlah , a.KodVotSebagai ,(select  b.Butiran  from MK_Vot b where b.KodVot =  a.KodVotSebagai)  as Jenis   
from BG20_MohonDt a where  LEFT(a.KodVotSebagai, 1) <> '0' and BG20_NoMohon in  (select BG20_NoMohon  from BG20_Mohon where StatusDok in ('03','08') and BG20_Status = 1) group by a.KodVotSebagai"
            Dim dbconn As New DBKewConn

            ds = dbconn.fselectCommand(strSql)
            dv = New DataView(ds.Tables(0))

            ViewState("dsMohonDt") = ds
        Catch ex As Exception

        End Try


    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim datePrev As Date = Now.AddYears(-1)
                Dim dateNext As Date = Now.AddYears(+1)
                Dim strYearPrev As String = datePrev.Year
                txtYear.Text = dateNext.Year

                lblPeruntukanPrev = "Peruntukan Asal Tahun " & strYearPrev & " (RM)"
                lblPerbelanjaanPrev = "Perbelanjaan Sebenar Tahun " & strYearPrev & " (RM)"
                lblAnggaranSyorNow = "Anggaran Disyorkan Tahun " & Now.Year & " (RM)"
                lblAnggaranMintaNext = "Anggaran Diminta Tahun " & dateNext.Year & " (RM)"
                lblAnggaranSyorNext = "Anggaran Disyorkan Tahun " & dateNext.Year & " (RM)"

                fBindgvAbmMaster()

            End If

            TotalRec = ViewState("TotalRec")
            TotalRecProg = ViewState("TotalRecProg")

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub lbtnKembali_Click(sender As Object, e As EventArgs) Handles lbtnKembali.Click
        Response.Redirect("~/FORMS/BAJET/PERMOHONAN BAJET/Penyediaan_Bajet_PTJ.aspx")
    End Sub

    Private Sub fBindgvAbmMaster()
        Try
            Dim ds As New DataSet

            Dim strSql As String = "select KodVot, Butiran from mk_vot where Klasifikasi = 'H1' and KodVot <> '00000'"

            Dim dbconn As New DBKewConn

            ds = dbconn.fselectCommand(strSql)
            Dim dt As DataTable = ds.Tables(0)
            gvAbmMaster.DataSource = dt
            gvAbmMaster.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvAbmMaster_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvAbmMaster.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then


                Dim gvAbmDt As GridView = DirectCast(e.Row.FindControl("gvAbmDt"), GridView)
                Dim strKodVot As String = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "KodVot"))

                Dim strKodVotAm As String = strKodVot.Substring(0, 1)
                Dim dt As New DataTable
                Dim ds As New DataSet
                ds = ViewState("dsMohonDt")

                Dim contacts As New DataTable
                contacts = ds.Tables(0)
                Dim query As EnumerableRowCollection(Of DataRow) = From contact In contacts.AsEnumerable() Where contact.Field(Of String)("KodVotSebagai").StartsWith(strKodVotAm) Select contact

                Dim dv As New DataView
                dv = query.AsDataView

                dt.Columns.Add("KodVot", GetType(String))
                dt.Columns.Add("Jenis", GetType(String))
                dt.Columns.Add("PeruntukanPrev", GetType(String))
                dt.Columns.Add("PerbelanjaanPrev", GetType(String))
                dt.Columns.Add("AnggaranSyorNow", GetType(String))
                dt.Columns.Add("AnggaranMintaNext", GetType(String))
                dt.Columns.Add("AnggaranSyorNext", GetType(String))
                Dim strKodVotSbg As String
                Dim strJenis As String
                Dim decPeruntukanPrev As Decimal = 0.0
                Dim strPeruntukanPrev As String
                Dim decPerbelanjaanPrev As Decimal = 0.0
                Dim strPerbelanjaanPrev As String
                Dim decAnggaranSyorNow As Decimal = 0.0
                Dim strAnggaranSyorNow As String
                Dim decAnggaranMintaNext As Decimal = 0.0
                Dim strAnggaranMintaNext As String
                Dim AnggaranSyorNext As String = "-"

                For i As Integer = 0 To dv.Count - 1
                    strKodVotSbg = dv(i)("KodVotSebagai").ToString
                    strJenis = dv(i)("Jenis").ToString


                    strPeruntukanPrev = decPeruntukanPrev.ToString("#,##0.00")
                    strPerbelanjaanPrev = decPerbelanjaanPrev.ToString("#,##0.00")
                    strAnggaranSyorNow = decAnggaranSyorNow.ToString("#,##0.00")
                    decAnggaranMintaNext = dv(i)("Jumlah")
                    strAnggaranMintaNext = decAnggaranMintaNext.ToString("#,##0.00")

                    dt.Rows.Add(strKodVotSbg, strJenis, strPeruntukanPrev, strPerbelanjaanPrev, strAnggaranSyorNow, strAnggaranMintaNext, AnggaranSyorNext)
                Next

                gvAbmDt.DataSource = dt
                gvAbmDt.DataBind()



            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub gvAbmDt_RowCreated(ByVal sender As Object, e As GridViewRowEventArgs)

        Try

            If e.Row.RowType = DataControlRowType.Footer Then

                Dim intNoOfMergeCol As Integer = 3
                For intCellCol As Integer = 1 To intNoOfMergeCol - 1
                    e.Row.Cells.RemoveAt(1)
                Next
                e.Row.Cells(0).ColumnSpan = intNoOfMergeCol
                e.Row.Cells(0).Text = "Jumlah Besar (RM)"
                e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(0).Font.Bold = True

            End If



        Catch ex As Exception

        End Try

    End Sub

    Protected Sub gvAbmDt_RowDataBound(ByVal sender As Object, e As GridViewRowEventArgs)

        Dim intLog As Integer

        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                intLog = 11

                Dim strPeruntukanPrev As String = DataBinder.Eval(e.Row.DataItem, "PeruntukanPrev").ToString()
                Dim PeruntukanPrev As Decimal
                If strPeruntukanPrev = "-" Then
                    strPeruntukanPrev = "-"
                Else
                    PeruntukanPrev = Convert.ToDecimal(strPeruntukanPrev)
                End If

                intLog = 12

                Dim strPerbelanjaanPrev As String = DataBinder.Eval(e.Row.DataItem, "PerbelanjaanPrev").ToString()
                Dim PerbelanjaanPrev As Decimal
                If strPerbelanjaanPrev = "-" Then
                    strPerbelanjaanPrev = "-"
                Else
                    PerbelanjaanPrev = Convert.ToDecimal(strPerbelanjaanPrev)
                End If

                intLog = 13

                Dim strAnggaranSyorNow As String = DataBinder.Eval(e.Row.DataItem, "AnggaranSyorNow").ToString()
                Dim AnggaranSyorNow As Decimal
                If strAnggaranSyorNow = "-" Then
                    strAnggaranSyorNow = "-"
                Else
                    AnggaranSyorNow = Convert.ToDecimal(strAnggaranSyorNow)
                End If

                intLog = 14

                Dim strAnggaranMintaNext As String = DataBinder.Eval(e.Row.DataItem, "AnggaranMintaNext").ToString()
                Dim AnggaranMintaNext As Decimal
                If strAnggaranMintaNext = "-" Then
                    strAnggaranMintaNext = "-"
                Else
                    AnggaranMintaNext = Convert.ToDecimal(strAnggaranMintaNext)
                End If

                'decPeruntukanPrev += PeruntukanPrev
                ViewState("decPeruntukanPrev") += PeruntukanPrev
                'decPerbelanjaanPrev += PerbelanjaanPrev
                ViewState("decPerbelanjaanPrev") += PerbelanjaanPrev
                'decAnggaranSyorNow += AnggaranSyorNow
                ViewState("decAnggaranSyorNow") += AnggaranSyorNow
                'decAnggaranMintaNext += AnggaranMintaNext
                ViewState("decAnggaranMintaNext") += AnggaranMintaNext

            End If

            If e.Row.RowType = DataControlRowType.Footer Then

                Dim decPeruntukanPrev As Decimal = ViewState("decPeruntukanPrev")
                Dim decPerbelanjaanPrev As Decimal = ViewState("decPerbelanjaanPrev")
                Dim decAnggaranSyorNow As Decimal = ViewState("decAnggaranSyorNow")
                Dim decAnggaranMintaNext As Decimal = ViewState("decAnggaranMintaNext")

                e.Row.Cells(1).Text = decPeruntukanPrev.ToString("#,##0.00")
                e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(1).Font.Bold = True

                e.Row.Cells(2).Text = decPerbelanjaanPrev.ToString("#,##0.00")
                e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(2).Font.Bold = True

                e.Row.Cells(3).Text = decAnggaranSyorNow.ToString("#,##0.00")
                e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(3).Font.Bold = True

                e.Row.Cells(4).Text = decAnggaranMintaNext.ToString("#,##0.00")
                e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(4).Font.Bold = True

                e.Row.Cells(5).Text = "-"
                e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(5).Font.Bold = True

                ViewState("decPeruntukanPrev") = 0
                ViewState("decPerbelanjaanPrev") = 0
                ViewState("decAnggaranSyorNow") = 0
                ViewState("decAnggaranMintaNext") = 0

            End If

        Catch ex As Exception
            Dim strErr As String = ex.ToString & ", intLog - " & intLog
        End Try
    End Sub
    Protected Sub lnkView_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim grdrow As GridViewRow = CType((CType(sender, LinkButton)).NamingContainer, GridViewRow)
        Dim strKodVot As String = grdrow.Cells(1).Text
        'sBindGvProgram(strKodVot)

        ' Me.ModalPopupExtender1.Show()
    End Sub

    'Private Sub sBindGvProgram(ByVal strKodVot As String)
    '    Try
    '        Dim dt As New DataTable

    '        Dim ds As New DataSet
    '        Dim strSql As String = "select BG20_NoMohon , KodBahagian  , KodUnitPtj ,BG20_Program,BG20_AmaunMohon   from BG20_Mohon where BG20_NoMohon in (select BG20_NoMohon  from BG20_MohonDt where KodVotSebagai = '" & strKodVot & "')
    '        group by BG20_NoMohon , KodBahagian , KodUnitPtj ,BG20_Program, BG20_AmaunMohon"

    '        Dim dbconn As New DBKewConn
    '        ds = dbconn.fselectCommand(strSql)

    '        dt.Columns.Add("NoMohon", GetType(String))
    '        dt.Columns.Add("Bahagian", GetType(String))
    '        dt.Columns.Add("Unit", GetType(String))
    '        dt.Columns.Add("Program", GetType(String))
    '        dt.Columns.Add("AmaunMohon", GetType(String))

    '        Dim strNoMohon As String
    '        Dim strBahagian As String
    '        Dim strUnit As Decimal
    '        Dim strProgram As String
    '        Dim decAmaunMohon As Decimal
    '        Dim strAmaunMohon As String

    '        ViewState("TotalRecProg") = ds.Tables(0).Rows.Count
    '        TotalRecProg = ViewState("TotalRecProg")
    '        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
    '            strNoMohon = ds.Tables(0).Rows(i).Item("BG20_NoMohon").ToString
    '            strBahagian = ds.Tables(0).Rows(i).Item("KodBahagian").ToString
    '            strUnit = ds.Tables(0).Rows(i).Item("KodUnitPtj").ToString
    '            strProgram = ds.Tables(0).Rows(i).Item("BG20_Program").ToString
    '            decAmaunMohon = ds.Tables(0).Rows(i).Item("BG20_AmaunMohon")
    '            strAmaunMohon = decAmaunMohon.ToString("#,##0.00")

    '            dt.Rows.Add(strNoMohon, strBahagian, strUnit, strProgram, strAmaunMohon)
    '        Next

    '        gvProgram.DataSource = dt
    '        gvProgram.DataBind()

    '    Catch ex As Exception

    '    End Try
    'End Sub

    Protected Sub btnHantar_Click(sender As Object, e As EventArgs) Handles btnHantar.Click
        Try
            'Session("msg") = "Anda pasti untuk hantar senarai permohonan bajet ke pejabat Bendahari?"
            strMsg = "Anda pasti untuk hantar senarai permohonan bajet ke pejabat Bendahari?"
            Me.ModalPopupExtender3.Show()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lbYes_Click(sender As Object, e As EventArgs) Handles lbYes.Click

        Dim strSql As String
        Dim dbconn As New DBKewConn
        Try

            'strNoMohonPTj = fGetNoMohonPTj()
            'If strNoMohonPTj <> "" Then
            '    intStatus = fInsertNoMohonPTj(strNoMohonPTj)
            'End If

            strSql = "update BG20_Mohon set StatusDok = @StatusDok where  (StatusDok = '03' or StatusDok='08') and BG20_Status = 1 and KodPtj = '41'"
            Dim paramSql() As SqlParameter = {
                                New SqlParameter("@StatusDok", "05")
                                }

            dbconn.sConnBeginTrans()
            If dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                dbconn.sConnCommitTrans()
                'sAuditLog(Session("ssusrID"), Request.QueryString("KodSubMenu"), "BG21_MohonPTj", "2", "hantar senarai permohonan bajet ke pejabat bendahari") 'TransID: 1-Insert,2-Update,3-Delete
                'fGlobalAlert("Senarai permohonan bajet telah dihantar ke pejabat Bendahari!", Me.Page, Me.[GetType]())
            Else
                dbconn.sConnRollbackTrans()
                fGlobalAlert("Error!", Me.Page, Me.[GetType]())
            End If

        Catch ex As Exception

        Finally
            dbconn.sCloseConnection()
        End Try
    End Sub
End Class