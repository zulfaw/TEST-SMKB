Imports System.Drawing
Imports System.Data.SqlClient
Imports System.Web
Public Class Pengesahan_Ketua_PTJ
    Inherits System.Web.UI.Page


    'Private Shared dvButiran As New DataView
    Protected TotalRecProg, TotalRecDt As String
    Protected strStatMohon As String
    Protected strMsg, strNotice As String
    Protected TotalRec As String
    Protected lblPeruntukanPrev, lblPerbelanjaanPrev, lblAnggaranSyorNow, lblAnggaranMintaNext, lblAnggaranSyorNext As String



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

                TabContainer1.ActiveTab = tabABM

                fBindgvPermohonan()
                fBindgvAbmMaster()

            End If

            TotalRec = ViewState("TotalRec")
            TotalRecProg = ViewState("TotalRecProg")
            strNotice = ViewState("strNotice")
        Catch ex As Exception
            sErrLog(ex.HResult, ex.Message, fGetFormName(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session("ssusrID"), Session("PcIP"), Session("PcName"), Session("SysVer"))
        End Try
    End Sub

    Private Sub fPopMohonDt()
        Try
            Dim ds As New DataSet
            Dim dv As New DataView
            Dim strSql As String = "Select sum( a.BG20_AngJumlah) as Jumlah , a.KodVotSebagai ,(select  b.Butiran  from MK_Vot b where b.KodVot =  a.KodVotSebagai)  as Jenis   
from BG20_MohonDt a where  LEFT(a.KodVotSebagai, 1) <> '0' and BG20_NoMohon in  (select BG20_NoMohon  from BG20_Mohon where StatusDok in ('05','06') and BG20_Status = 1) group by a.KodVotSebagai"
            Dim dbconn As New DBKewConn

            ds = dbconn.fselectCommand(strSql)
            dv = New DataView(ds.Tables(0))
            ViewState("dsMohonDt") = ds
        Catch ex As Exception
            sErrLog(ex.HResult, ex.Message, fGetFormName(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session("ssusrID"), Session("PcIP"), Session("PcName"), Session("SysVer"))
        Finally

        End Try
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
            sErrLog(ex.HResult, ex.Message, fGetFormName(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session("ssusrID"), Session("PcIP"), Session("PcName"), Session("SysVer"))
        End Try
    End Sub

    Private Sub fBindgvPermohonan()
        Try

            Dim ds As New DataSet
            Dim strSql As String = "Select  a.BG20_NoMohon , (Select b.KodBah + '-' + b.NamaBahagian from MK_BahagianPTJ b where b.KodPtj = a.KodPtj and b.KodBah = a.KodBahagian ) as Bahagian,
(select c.KodUnit + '-' + c.NamaUnit from MK_UnitPTJ c where c.kodbah = a.KodBahagian and c.KodUnit  = a.KodUnitPTj) as Unit,
BG20_Program , BG20_AmaunMohon,
BG20_Justifikasi, BG20_TarikhMohon, StatusDok, (select  d.BG_Butiran  from BG_StatusDok d where d.BG_KodStatus = a.StatusDok   ) as ButiranStat 
From BG20_Mohon a Where a.KodPtj = '" & Session("ssusrKodPTj") & "' and BG20_TahunBajet = '" & Trim(txtYear.Text.TrimEnd) & "' and StatusDok in ('05','06') and BG20_Status = 1 "

            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            Dim dt As New DataTable
            dt.Columns.Add("NoMohon", GetType(String))
            dt.Columns.Add("Bahagian", GetType(String))
            dt.Columns.Add("Unit", GetType(String))
            dt.Columns.Add("Program", GetType(String))
            dt.Columns.Add("Anggaran", GetType(String))
            dt.Columns.Add("Justifikasi", GetType(String))
            dt.Columns.Add("TarikhMohon", GetType(String))
            dt.Columns.Add("StatusDok", GetType(String))

            Dim strNoMohon As String
            Dim strBahagian As String
            Dim strUnit As String
            Dim strProgram As String
            Dim decAnggaran As Decimal
            Dim strAnggaran As String
            Dim strJustifikasi As String
            Dim strTarikhMohon As String
            Dim strStatusDok As String
            Dim strNoStatusDok As String

            'Session("TotalRec") = ds.Tables(0).Rows.Count
            ViewState("TotalRec") = ds.Tables(0).Rows.Count
            TotalRec = ViewState("TotalRec")

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                strNoMohon = ds.Tables(0).Rows(i).Item("BG20_NoMohon").ToString
                strBahagian = ds.Tables(0).Rows(i).Item("Bahagian").ToString
                strUnit = ds.Tables(0).Rows(i).Item("Unit").ToString
                strProgram = ds.Tables(0).Rows(i).Item("BG20_Program").ToString

                decAnggaran = ds.Tables(0).Rows(i).Item("BG20_AmaunMohon")
                strAnggaran = decAnggaran.ToString("#,##0.00")
                strJustifikasi = ds.Tables(0).Rows(i).Item("BG20_Justifikasi").ToString
                strTarikhMohon = ds.Tables(0).Rows(i).Item("BG20_TarikhMohon").ToString
                strStatusDok = ds.Tables(0).Rows(i).Item("ButiranStat").ToString
                strNoStatusDok = ds.Tables(0).Rows(i).Item("StatusDok").ToString
                dt.Rows.Add(strNoMohon, strBahagian, strUnit, strProgram, strAnggaran, strJustifikasi, strTarikhMohon, strStatusDok)
            Next

            gvPermohonan.DataSource = dt
            gvPermohonan.DataBind()
            If TotalRec = 0 Then
                ViewState("strNotice") = "Tiada rekod permohonan bajet"
                strNotice = ViewState("strNotice")
                pnlNotice.Visible = True
                btnHantar.Enabled = False
                btnkemaskini.Enabled = False
            Else

                If strNoStatusDok = "06" Then
                    ViewState("strNotice") = "Senarai Permohonan Bajet telah dihantar ke Pejabat Bendahari"
                    strNotice = ViewState("strNotice")

                    pnlNotice.Visible = True
                    btnHantar.Enabled = False
                    btnkemaskini.Enabled = False
                Else
                    pnlNotice.Visible = False
                    btnHantar.Enabled = True
                    btnkemaskini.Enabled = True
                End If
            End If

        Catch ex As Exception
            sErrLog(ex.HResult, ex.Message, fGetFormName(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session("ssusrID"), Session("PcIP"), Session("PcName"), Session("SysVer"))
        End Try
    End Sub

    Private Sub sBindGVButiran(ByVal strNoMohon As String)
        Try
            Dim dt As New DataTable

            Dim ds As New DataSet
            Dim strSql As String = "select BG20_Butiran , BG20_Kuantiti , BG20_AngJumlah ,KodVotSebagai  from BG20_MohonDt where BG20_NoMohon = '" & strNoMohon & "'"

            Dim dbconn As New DBKewConn

            ds = dbconn.fselectCommand(strSql)

            dt.Columns.Add("Butiran", GetType(String))
            dt.Columns.Add("Kuantiti", GetType(String))
            dt.Columns.Add("Anggaran", GetType(String))
            dt.Columns.Add("KodVotSebagai", GetType(String))

            Dim strButiran As String
            Dim strKuantiti As String
            Dim decAnggaran As Decimal
            Dim strAnggaran As String
            Dim strKodVotSebagai As String

            TotalRecDt = ds.Tables(0).Rows.Count
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                strButiran = ds.Tables(0).Rows(i).Item("BG20_Butiran").ToString
                strKuantiti = ds.Tables(0).Rows(i).Item("BG20_Kuantiti").ToString

                decAnggaran = ds.Tables(0).Rows(i).Item("BG20_AngJumlah")
                strAnggaran = decAnggaran.ToString("#,##0.00")
                strKodVotSebagai = ds.Tables(0).Rows(i).Item("KodVotSebagai").ToString

                dt.Rows.Add(strButiran, strKuantiti, strAnggaran, strKodVotSebagai)
            Next

            gvButiran.DataSource = dt
            gvButiran.DataBind()

        Catch ex As Exception
            sErrLog(ex.HResult, ex.Message, fGetFormName(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session("ssusrID"), Session("PcIP"), Session("PcName"), Session("SysVer"))
        End Try
    End Sub



    Protected Sub lnkView_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim grdrow As GridViewRow = CType((CType(sender, LinkButton)).NamingContainer, GridViewRow)
        Dim strKodVot As String = grdrow.Cells(1).Text
        sBindGvProgram(strKodVot)

        Me.ModalPopupExtender1.Show()
    End Sub

    Private Sub sBindGvProgram(ByVal strKodVot As String)
        Try
            Dim dt As New DataTable

            Dim ds As New DataSet
            Dim strSql As String = "select BG20_NoMohon , KodBahagian  , KodUnitPtj ,BG20_Program,BG20_AmaunMohon   from BG20_Mohon where BG20_NoMohon in (select BG20_NoMohon  from BG20_MohonDt where KodVotSebagai = '" & strKodVot & "')
            group by BG20_NoMohon , KodBahagian , KodUnitPtj ,BG20_Program, BG20_AmaunMohon"

            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            dt.Columns.Add("NoMohon", GetType(String))
            dt.Columns.Add("Bahagian", GetType(String))
            dt.Columns.Add("Unit", GetType(String))
            dt.Columns.Add("Program", GetType(String))
            dt.Columns.Add("AmaunMohon", GetType(String))

            Dim strNoMohon As String
            Dim strBahagian As String
            Dim strUnit As Decimal
            Dim strProgram As String
            Dim decAmaunMohon As Decimal
            Dim strAmaunMohon As String

            ViewState("TotalRecProg") = ds.Tables(0).Rows.Count
            TotalRecProg = ViewState("TotalRecProg")
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                strNoMohon = ds.Tables(0).Rows(i).Item("BG20_NoMohon").ToString
                strBahagian = ds.Tables(0).Rows(i).Item("KodBahagian").ToString
                strUnit = ds.Tables(0).Rows(i).Item("KodUnitPtj").ToString
                strProgram = ds.Tables(0).Rows(i).Item("BG20_Program").ToString
                decAmaunMohon = ds.Tables(0).Rows(i).Item("BG20_AmaunMohon")
                strAmaunMohon = decAmaunMohon.ToString("#,##0.00")

                dt.Rows.Add(strNoMohon, strBahagian, strUnit, strProgram, strAmaunMohon)
            Next

            gvProgram.DataSource = dt
            gvProgram.DataBind()

        Catch ex As Exception
            sErrLog(ex.HResult, ex.Message, fGetFormName(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session("ssusrID"), Session("PcIP"), Session("PcName"), Session("SysVer"))
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
            sErrLog(ex.HResult, ex.Message, fGetFormName(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session("ssusrID"), Session("PcIP"), Session("PcName"), Session("SysVer"))
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
            sErrLog(ex.HResult, ex.Message, fGetFormName(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session("ssusrID"), Session("PcIP"), Session("PcName"), Session("SysVer"))
        End Try

    End Sub

    Protected Sub gvAbmDt_RowDataBound(ByVal sender As Object, e As GridViewRowEventArgs)

        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim strPeruntukanPrev As String = DataBinder.Eval(e.Row.DataItem, "PeruntukanPrev").ToString()
                Dim PeruntukanPrev As Decimal
                If strPeruntukanPrev = "-" Then
                    strPeruntukanPrev = "-"
                Else
                    PeruntukanPrev = Convert.ToDecimal(strPeruntukanPrev)
                End If


                Dim strPerbelanjaanPrev As String = DataBinder.Eval(e.Row.DataItem, "PerbelanjaanPrev").ToString()
                Dim PerbelanjaanPrev As Decimal
                If strPerbelanjaanPrev = "-" Then
                    strPerbelanjaanPrev = "-"
                Else
                    PerbelanjaanPrev = Convert.ToDecimal(strPerbelanjaanPrev)
                End If

                Dim strAnggaranSyorNow As String = DataBinder.Eval(e.Row.DataItem, "AnggaranSyorNow").ToString()
                Dim AnggaranSyorNow As Decimal
                If strAnggaranSyorNow = "-" Then
                    strAnggaranSyorNow = "-"
                Else
                    AnggaranSyorNow = Convert.ToDecimal(strAnggaranSyorNow)
                End If
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
            sErrLog(ex.HResult, ex.Message, fGetFormName(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session("ssusrID"), Session("PcIP"), Session("PcName"), Session("SysVer"))
        End Try
    End Sub

    Protected Sub gvPermohonan_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvPermohonan.RowCommand
        If e.CommandName = "Select" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim selectedRow As GridViewRow = gvPermohonan.Rows(index)
            Dim strNoMohon As String = selectedRow.Cells(1).Text
            sBindGVButiran(strNoMohon)
            Me.ModalPopupExtender2.Show()
        End If
    End Sub

    Protected Sub gvPermohonan_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvPermohonan.RowCreated
        Try
            If e.Row.RowType = DataControlRowType.Footer Then

                Dim intNoOfMergeCol As Integer = 5

                For intCellCol As Integer = 1 To intNoOfMergeCol - 1
                    e.Row.Cells.RemoveAt(1)
                Next
                e.Row.Cells(0).ColumnSpan = intNoOfMergeCol
                e.Row.Cells(0).Text = "Jumlah Besar (RM)"
                e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(0).Font.Bold = True
            End If

        Catch ex As Exception
            sErrLog(ex.HResult, ex.Message, fGetFormName(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session("ssusrID"), Session("PcIP"), Session("PcName"), Session("SysVer"))
        End Try

    End Sub

    Protected Sub lbYes_Click(sender As Object, e As EventArgs) Handles lbYes.Click

        Dim strSql As String
        Dim dbconn As New DBKewConn
        Try
            If ViewState("Status") = "1" Then
                Dim strNoMohonPTj As String

                Dim intStatus As Integer

                strNoMohonPTj = fGetNoMohonPTj()
                If strNoMohonPTj <> "" Then
                    intStatus = fInsertNoMohonPTj(strNoMohonPTj)
                End If

                If intStatus = 1 Then

                    strSql = "update BG20_Mohon set BG21_NoMohonPTj  = @BG21_NoMohonPTj, StatusDok = @StatusDok, BG20_StaffIDPengesah = @BG20_StaffIDPengesah  where  StatusDok = '05' and BG20_Status = 1 and KodPtj = '" & Session("ssusrKodPTj") & "'"
                    Dim paramSql() As SqlParameter = {
                                New SqlParameter("@BG21_NoMohonPTj", strNoMohonPTj),
                                New SqlParameter("@StatusDok", "06"),
                                New SqlParameter("@BG20_StaffIDPengesah", Session("ssusrID"))
                                }

                    dbconn.sConnBeginTrans()
                    If dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                        dbconn.sConnCommitTrans()
                        'sAuditLog(Session("ssusrID"), Request.QueryString("KodSubMenu"), "BG21_MohonPTj", "2", "hantar senarai permohonan bajet ke pejabat bendahari", Session("PcIP"), Session("PcName"), Session("SysVer")) 'TransID: 1-Insert,2-Update,3-Delete
                        fGlobalAlert("Senarai permohonan bajet telah dihantar ke pejabat Bendahari!", Me.Page, Me.[GetType]())
                    Else
                        dbconn.sConnRollbackTrans()
                        fGlobalAlert("Error!", Me.Page, Me.[GetType]())
                    End If

                    ''------using stored proc example
                    'Dim paramSql() As SqlParameter = {
                    '            New SqlParameter("@BG21_NoMohonPTj", strNoMohonPTj),
                    '            New SqlParameter("@StatusDok", "06"),
                    '            New SqlParameter("@BG20_StaffIDPengesah", Session("ssusrID")),
                    '            New SqlParameter("@KodPtj", Session("ssusrKodPTj"))
                    '            }

                    'If dbconn.fSP_Update("UpdateKodDok", paramSql) > 0 Then

                    'End If
                    ''---------------------------

                Else
                    'failed insert 
                    fGlobalAlert("Error!", Me.Page, Me.[GetType]())
                End If
            ElseIf ViewState("Status") = "2" Then

                strSql = "update BG20_Mohon set  StatusDok = @StatusDok where BG20_NoMohon in (select BG20_NoMohon from BG20_Mohon where StatusDok = '05' and BG20_Status = 1)"
                Dim paramSql() As SqlParameter = {
                        New SqlParameter("@StatusDok", "04")
                        }

                dbconn.sConnBeginTrans()
                If dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                    dbconn.sConnCommitTrans()
                    'sAuditLog(Session("ssusrID"), Request.QueryString("KodSubMenu"), "BG21_MohonPTj", "2", "Hantar senarai permohonan bajet ke penyedia bajet") 'TransID: 1-Insert,2-Update,3-Delete
                    fGlobalAlert("Senarai permohonan bajet telah dipulang ke penyedia bajet PTj!", Me.Page, Me.[GetType]())
                Else
                    dbconn.sConnRollbackTrans()
                    fGlobalAlert("Error!", Me.Page, Me.[GetType]())
                End If

            End If
            fBindgvPermohonan()
        Catch ex As Exception
            sErrLog(ex.HResult, ex.Message, fGetFormName(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session("ssusrID"), Session("PcIP"), Session("PcName"), Session("SysVer"))
        Finally
            dbconn.sCloseConnection()
        End Try
    End Sub

    Private Function fInsertNoMohonPTj(strNoMohonPTj As String) As Integer
        Dim strSql As String
        Dim intStatus As Integer
        Dim strAmaunMohon As String
        Dim dbconn As New DBKewConn
        Try
            strAmaunMohon = fGetAmaunMohon()

            '------using parameterized
            strSql = "insert into BG21_MohonPTj (BG21_NoMohonPTj , KodPTj , BG21_TahunBajet,bg21_AmaunMohon,  StatusDok) values (@BG21_NoMohonPTj,@KodPTj,@TahunBajet,@AmaunMohon, @StatusDok)"

            Dim paramSql() As SqlParameter = {
                New SqlParameter("@BG21_NoMohonPTj", strNoMohonPTj),
                New SqlParameter("@KodPTj", Session("ssusrKodPTj")),
                New SqlParameter("@TahunBajet", Trim(txtYear.Text.TrimEnd)),
                 New SqlParameter("@AmaunMohon", strAmaunMohon),
                New SqlParameter("@StatusDok", "06")
            }

            dbconn.sConnBeginTrans() '<---
            If dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                dbconn.sConnCommitTrans() '<---
                intStatus = 1

                ' sAuditLog(Session("ssusrID"), Request.QueryString("KodSubMenu"), "BG21_MohonPTj", "1", "insert kod mohon bajet ptj baru") 'TransID: 1-Insert,2-Update,3-Delete

            Else
                dbconn.sConnRollbackTrans() '<---
                intStatus = 0
            End If

            ''------using stored proc example
            'Dim paramSql() As SqlParameter = {
            '                    New SqlParameter("@BG21_NoMohonPTj", strNoMohonPTj),
            '    New SqlParameter("@KodPTj", Session("ssusrKodPTj")),
            '    New SqlParameter("@TahunBajet", Trim(txtYear.Text.TrimEnd)),
            '     New SqlParameter("@AmaunMohon", strAmaunMohon),
            '    New SqlParameter("@StatusDok", "06")
            '                    }

            'If dbconn.fSP_Insert("InsertKodMohonPTj", paramSql) > 0 Then
            '    intStatus = 1
            'Else
            '    intStatus = 0
            'End If
            ''---------------------------

            Return intStatus
        Catch ex As Exception
            sErrLog(ex.HResult, ex.Message, fGetFormName(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session("ssusrID"), Session("PcIP"), Session("PcName"), Session("SysVer"))
        Finally
            dbconn.sCloseConnection()
        End Try
    End Function


    Private Function fGetAmaunMohon() As String
        Dim strSql As String
        Try
            strSql = "Select sum(BG20_AmaunMohon ) As AmaunMohon from BG20_Mohon where KodPtj = '" & Session("ssusrKodPTj") & "'"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim strAmaunMohon As String

            ds = dbconn.fselectCommand(strSql)

            strAmaunMohon = ds.Tables(0).Rows(0).Item("AmaunMohon").ToString
            Return strAmaunMohon

        Catch ex As Exception
            sErrLog(ex.HResult, ex.Message, fGetFormName(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session("ssusrID"), Session("PcIP"), Session("PcName"), Session("SysVer"))
        End Try
    End Function

    Private Function fGetNoMohonPTj() As String
        Try
            Dim strSql As String
            Dim strNoMohonPTj As String
            Dim intNo As Integer
            Dim strKodpTj As String = Session("ssusrKodPTj")
            Dim strNo As String
            Dim strBlnThn As String = Now.ToString("MMyy")

            strSql = "SELECT * FROM    bg21_mohonptj WHERE   ID = (SELECT MAX(ID)  FROM bg21_mohonptj where KodPTj = '" & Session("ssusrKodPTj") & "')"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)
            If ds.Tables(0).Rows.Count > 0 Then
                strNoMohonPTj = ds.Tables(0).Rows(0).Item("bg21_NoMohonPTj").ToString
                strNo = strNoMohonPTj.Substring(8, 2)
                intNo = CInt(strNoMohonPTj.Substring(8, 2))

                intNo = intNo + 1
                If strNo.Length = 1 Then
                    strNo = "00000" & intNo
                Else
                    strNo = "0000" & intNo
                End If


            Else
                'nomohon ptj = BG/kodptj/running no./bulan/tahun   BG/xx/xxxxxx/xx/xx i.e : BG410000011217

                strNo = "000001"
            End If
            strNoMohonPTj = "BG" & strKodpTj & strNo & strBlnThn
            Return strNoMohonPTj
        Catch ex As Exception
            sErrLog(ex.HResult, ex.Message, fGetFormName(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session("ssusrID"), Session("PcIP"), Session("PcName"), Session("SysVer"))
        End Try
    End Function

    Private Sub gvPermohonan_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvPermohonan.RowDataBound


        Try

            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim strAnggaranBelanja As String = DataBinder.Eval(e.Row.DataItem, "Anggaran").ToString()
                Dim AnggaranBelanja As Decimal
                If strAnggaranBelanja = "-" Then
                    strAnggaranBelanja = "-"
                Else
                    AnggaranBelanja = Convert.ToDecimal(strAnggaranBelanja)
                End If

                ViewState("decAnggaranBelanja") += AnggaranBelanja

            End If

            If e.Row.RowType = DataControlRowType.Footer Then

                Dim decAnggaranBelanja As Decimal = ViewState("decAnggaranBelanja")
                e.Row.Cells(1).Text = decAnggaranBelanja.ToString("#,##0.00")
                e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(1).Font.Bold = True
            End If

        Catch ex As Exception
            sErrLog(ex.HResult, ex.Message, fGetFormName(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session("ssusrID"), Session("PcIP"), Session("PcName"), Session("SysVer"))
        End Try
    End Sub



    Private Sub btnHantar_Click(sender As Object, e As EventArgs) Handles btnHantar.Click
        Try

            If TotalRec <> 0 Then
                'Session("msg") = "Anda pasti untuk hantar senarai permohonan bajet ke pejabat Bendahari?"
                strMsg = "Anda pasti untuk hantar senarai permohonan bajet ke pejabat Bendahari?"
                ViewState("Status") = "1"
                Me.ModalPopupExtender3.Show()
            End If
        Catch ex As Exception
            sErrLog(ex.HResult, ex.Message, fGetFormName(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session("ssusrID"), Session("PcIP"), Session("PcName"), Session("SysVer"))
        End Try
    End Sub

    Private Sub btnkemaskini_Click(sender As Object, e As EventArgs) Handles btnkemaskini.Click
        If TotalRec <> 0 Then
            'Session("msg") = "Anda pasti untuk pulangkan senarai permohonan bajet ke penyedia bajet?"
            strMsg = "Anda pasti untuk pulangkan senarai permohonan bajet ke penyedia bajet?"
            ViewState("Status") = "2"
            Me.ModalPopupExtender3.Show()
        End If
    End Sub

    Private Sub Pengesahan_Ketua_PTJ_Init(sender As Object, e As EventArgs) Handles Me.Init
        fPopMohonDt()
    End Sub
End Class

