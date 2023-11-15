Imports System.Drawing
Imports System.Data.SqlClient
Imports System.Web
Imports System.Globalization
Imports System.IO
Public Class Pengesahan_LPU
    Inherits System.Web.UI.Page

    'Private Shared dvButiran As New DataView
    Protected TotalRecProg, TotalRecDt As String
    Protected strStatMohon As String
    Protected strMsg, strNotice As String
    Protected TotalRec As String
    Protected lblPeruntukanPrev1, lblPeruntukanPrev, lblPerbelanjaanPrev, lblAnggaranSyorNow, lblAnggaranMintaNext, lblAnggaranSyorNext As String
    Private countButiran As Integer = 0

    Public Shared dvPermohonan As New DataView
    Public Shared dv As New DataView
    Dim ds As New DataSet
    Public Shared dvPTJ As New DataView

    Dim dateNext As Date = Now.AddYears(+1)
    Dim dbconn As New DBKewConn
    Dim dbconnEQ As New DBEQConn

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                fBindGVMohonBajet("27")
                fBindDdlKW()
                firstBindgvABM()
                fBindGvPTj()
                fLoadGVSalinanBajet()

                Dim datePrev As Date = Now.AddYears(-1)

                Dim strYearPrev As String = datePrev.Year
                txtYear.Text = dateNext.Year

                lblPeruntukanPrev1 = "Perbelanjaan Sebenar Tahun " & strYearPrev - 1 & " (RM)"
                lblPeruntukanPrev = "Perbelanjaan Sebenar Tahun " & strYearPrev & " (RM)"
                lblAnggaranSyorNow = "Peruntukan Asal Tahun " & Now.Year & " (RM)"
                lblAnggaranMintaNext = "Anggaran Diminta Tahun " & dateNext.Year & " (RM)"
                'lblAnggaranSyorNext = "Anggaran Disyorkan Tahun " & dateNext.Year & " (RM)"

                TabContainer1.ActiveTab = tabRingkasan

                'fBindgvPermohonan()
                fBindgvAbmMaster()

                Dim dt As New DataTable

                fFindRec()
                dt = fCreateDt("00", "00", "00")


            End If

            TotalRec = ViewState("TotalRec")
            TotalRecProg = ViewState("TotalRecProg")
            strNotice = ViewState("strNotice")
        Catch ex As Exception
            sErrLog(ex.HResult, ex.Message, fGetFormName(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session("ssusrID"), Session("PcIP"), Session("PcName"), Session("SysVer"))
        End Try
    End Sub

    Private Sub firstBindgvABM()
        Dim dt As New DataTable()

        dt.Columns.AddRange(New DataColumn() {
                            New DataColumn("ID", GetType(Integer)),
                            New DataColumn("KodKw", GetType(String)),
                            New DataColumn("KodKO", GetType(String)),
                            New DataColumn("KodPTJ", GetType(String)),
                            New DataColumn("KodKP", GetType(String)),
                            New DataColumn("KodVot", GetType(String)),
                            New DataColumn("Butiran", GetType(String)),
                            New DataColumn("Debit", GetType(Decimal)),
                            New DataColumn("Kredit", GetType(Decimal))
                            })
        ViewState("dsMohonDt") = dt
    End Sub

    Private Sub fPopMohonDt()
        Try
            Dim ds As New DataSet
            Dim dv As New DataView
            Dim strSql As String = "Select sum( a.BG20_AngJumlah) as Jumlah , a.KodVotSebagai ,(select  b.Butiran  from MK_Vot b where b.KodVot =  a.KodVotSebagai)  as Jenis   
from BG01_MohonDt_RV a where  LEFT(a.KodVotSebagai, 1) <> '0' and KodVotSebagai = '11000' group by a.KodVotSebagai"


            ds = dbconn.fSelectCommand(strSql)
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

            Dim strSql As String = "select KodVot, Butiran from mk_vot where Klasifikasi = 'H1' and KodVot <> '00000' and kodVot <= '50000' order by KodVot"

            Dim dbconn As New DBKewConn

            ds = dbconn.fSelectCommand(strSql)
            Dim dt As DataTable = ds.Tables(0)
            gvAbmMaster.DataSource = dt
            gvAbmMaster.DataBind()

        Catch ex As Exception
            sErrLog(ex.HResult, ex.Message, fGetFormName(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session("ssusrID"), Session("PcIP"), Session("PcName"), Session("SysVer"))
        End Try
    End Sub

    Private Sub sBindGVButiran(ByVal strNoMohon As String)
        Try
            Dim dt As New DataTable

            Dim ds As New DataSet
            Dim strSql As String = "select BG20_Butiran , BG20_Kuantiti , BG20_AngJumlah ,KodVotSebagai  from BG01_MohonDt_RV where BG20_NoMohon = '" & strNoMohon & "'"

            Dim dbconn As New DBKewConn

            ds = dbconn.fSelectCommand(strSql)

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
            Dim strSql As String = "select BG20_NoMohon , KodBahagian  , KodUnitPtj ,BG20_Program,BG20_AmaunMohon from BG01_Mohon_RV where BG20_NoMohon in (select BG20_NoMohon  from BG01_MohonDt_RV where KodVotSebagai = '11000'
AND KodBahagian = '4101')
            group by BG20_NoMohon , KodBahagian , KodUnitPtj ,BG20_Program, BG20_AmaunMohon"

            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

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
                dt.Columns.Add("PeruntukanPrev1", GetType(String))
                dt.Columns.Add("PeruntukanPrev", GetType(String))
                dt.Columns.Add("AnggaranSyorNow", GetType(String))
                dt.Columns.Add("AnggaranMintaNext", GetType(String))
                'dt.Columns.Add("AnggaranSyorNext", GetType(String))
                Dim strKodVotSbg As String
                Dim strJenis As String
                Dim decPeruntukanPrev1 As Decimal = 0.0
                Dim decPeruntukanPrev As Decimal = 0.0
                Dim strPeruntukanPrev As String
                Dim strPeruntukanPrev1 As String
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
                    strPeruntukanPrev1 = decPeruntukanPrev1.ToString("#,##0.00")
                    strPeruntukanPrev = decPeruntukanPrev.ToString("#,##0.00")
                    strPerbelanjaanPrev = decPerbelanjaanPrev.ToString("#,##0.00")
                    strAnggaranSyorNow = decAnggaranSyorNow.ToString("#,##0.00")
                    decAnggaranMintaNext = dv(i)("Jumlah")
                    strAnggaranMintaNext = decAnggaranMintaNext.ToString("#,##0.00")

                    dt.Rows.Add(strKodVotSbg, strJenis, strPeruntukanPrev1, strPeruntukanPrev, strAnggaranSyorNow, strAnggaranMintaNext)
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

                ViewState("decPeruntukanPrev1") += PeruntukanPrev
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

                Dim decPeruntukanPrev1 As Decimal = ViewState("decPeruntukanPrev1")
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

                ViewState("decPeruntukanPrev1") = 0
                ViewState("decPeruntukanPrev") = 0
                ViewState("decPerbelanjaanPrev") = 0
                ViewState("decAnggaranSyorNow") = 0
                ViewState("decAnggaranMintaNext") = 0

            End If

        Catch ex As Exception
            sErrLog(ex.HResult, ex.Message, fGetFormName(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session("ssusrID"), Session("PcIP"), Session("PcName"), Session("SysVer"))
        End Try
    End Sub

    'Protected Sub gvMohon_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvPermohonan.RowCommand
    '    If e.CommandName = "Select" Then
    '        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
    '        Dim selectedRow As GridViewRow = gvPermohonan.Rows(index)
    '        Dim strNoMohon As String = selectedRow.Cells(1).Text
    '        sBindGVButiran(strNoMohon)
    '        Me.ModalPopupExtender2.Show()
    '    End If
    'End Sub

    'Protected Sub gvPermohonan_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvPermohonan.RowCreated
    '    Try
    '        If e.Row.RowType = DataControlRowType.Footer Then

    '            Dim intNoOfMergeCol As Integer = 5

    '            For intCellCol As Integer = 1 To intNoOfMergeCol - 1
    '                e.Row.Cells.RemoveAt(1)
    '            Next
    '            e.Row.Cells(0).ColumnSpan = intNoOfMergeCol
    '            e.Row.Cells(0).Text = "Jumlah Besar (RM)"
    '            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Right
    '            e.Row.Cells(0).Font.Bold = True
    '        End If

    '    Catch ex As Exception
    '        sErrLog(ex.HResult, ex.Message, fGetFormName(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session("ssusrID"), Session("PcIP"), Session("PcName"), Session("SysVer"))
    '    End Try

    'End Sub

    'Protected Sub lbYes_Click(sender As Object, e As EventArgs) Handles lbYes.Click

    '    Dim strSql As String
    '    Dim dbconn As New DBKewConn
    '    Try
    '        If ViewState("Status") = "1" Then
    '            Dim strNoMohonPTj As String

    '            Dim intStatus As Integer

    '            strNoMohonPTj = fGetNoMohonPTj()
    '            If strNoMohonPTj <> "" Then
    '                intStatus = fInsertNoMohonPTj(strNoMohonPTj)
    '            End If

    '            If intStatus = 1 Then

    '                strSql = "update BG01_Mohon_RV set BG21_NoMohonPTj  = @BG21_NoMohonPTj, StatusDok = @StatusDok, BG20_StaffIDPengesah = @BG20_StaffIDPengesah  where  StatusDok = '05' and BG20_Status = 1 and KodPtj = '" & Session("ssusrKodPTj") & "'"
    '                Dim paramSql() As SqlParameter = {
    '                            New SqlParameter("@BG21_NoMohonPTj", strNoMohonPTj),
    '                            New SqlParameter("@StatusDok", "06"),
    '                            New SqlParameter("@BG20_StaffIDPengesah", Session("ssusrID"))
    '                            }

    '                dbconn.sConnBeginTrans()
    '                If dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
    '                    dbconn.sConnCommitTrans()
    '                    'sAuditLog(Session("ssusrID"), Request.QueryString("KodSubMenu"), "BG21_MohonPTj", "2", "hantar senarai SEMAKAN BAJET ke pejabat bendahari", Session("PcIP"), Session("PcName"), Session("SysVer")) 'TransID: 1-Insert,2-Update,3-Delete
    '                    fGlobalAlert("Senarai SEMAKAN BAJET telah dihantar ke pejabat Bendahari!", Me.Page, Me.[GetType]())
    '                Else
    '                    dbconn.sConnRollbackTrans()
    '                    fGlobalAlert("Error!", Me.Page, Me.[GetType]())
    '                End If

    '                ''------using stored proc example
    '                'Dim paramSql() As SqlParameter = {
    '                '            New SqlParameter("@BG21_NoMohonPTj", strNoMohonPTj),
    '                '            New SqlParameter("@StatusDok", "06"),
    '                '            New SqlParameter("@BG20_StaffIDPengesah", Session("ssusrID")),
    '                '            New SqlParameter("@KodPtj", Session("ssusrKodPTj"))
    '                '            }

    '                'If dbconn.fSP_Update("UpdateKodDok", paramSql) > 0 Then

    '                'End If
    '                ''---------------------------

    '            Else
    '                'failed insert 
    '                fGlobalAlert("Error!", Me.Page, Me.[GetType]())
    '            End If
    '        ElseIf ViewState("Status") = "2" Then

    '            strSql = "update BG01_Mohon_RV set  StatusDok = @StatusDok where BG20_NoMohon in (select BG20_NoMohon from BG01_Mohon_RV where StatusDok = '05' and BG20_Status = 1)"
    '            Dim paramSql() As SqlParameter = {
    '                    New SqlParameter("@StatusDok", "04")
    '                    }

    '            dbconn.sConnBeginTrans()
    '            If dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
    '                dbconn.sConnCommitTrans()
    '                'sAuditLog(Session("ssusrID"), Request.QueryString("KodSubMenu"), "BG21_MohonPTj", "2", "Hantar senarai SEMAKAN BAJET ke penyedia bajet") 'TransID: 1-Insert,2-Update,3-Delete
    '                fGlobalAlert("Senarai SEMAKAN BAJET telah dipulang ke penyedia bajet PTj!", Me.Page, Me.[GetType]())
    '            Else
    '                dbconn.sConnRollbackTrans()
    '                fGlobalAlert("Error!", Me.Page, Me.[GetType]())
    '            End If

    '        End If
    '        fBindgvPermohonan()
    '    Catch ex As Exception
    '        sErrLog(ex.HResult, ex.Message, fGetFormName(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session("ssusrID"), Session("PcIP"), Session("PcName"), Session("SysVer"))
    '    Finally
    '        dbconn.sCloseConnection()
    '    End Try
    'End Sub

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
            'dbconn.sCloseConnection()
        End Try
    End Function

    'Protected Sub btnPapar_Click(sender As Object, e As EventArgs) Handles btnPapar.Click

    'End Sub

    Private Function fGetAmaunMohon() As String
        Dim strSql As String
        Try
            strSql = "Select sum(BG20_AmaunMohon ) As AmaunMohon from BG01_Mohon_RV where KodPtj = '" & Session("ssusrKodPTj") & "'"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim strAmaunMohon As String

            ds = dbconn.fSelectCommand(strSql)

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
            ds = dbconn.fSelectCommand(strSql)
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

    'Private Sub gvPermohonan_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvPermohonan.RowDataBound


    '    Try

    '        If e.Row.RowType = DataControlRowType.DataRow Then

    '            Dim strAnggaranBelanja As String = DataBinder.Eval(e.Row.DataItem, "Anggaran").ToString()
    '            Dim AnggaranBelanja As Decimal
    '            If strAnggaranBelanja = "-" Then
    '                strAnggaranBelanja = "-"
    '            Else
    '                AnggaranBelanja = Convert.ToDecimal(strAnggaranBelanja)
    '            End If

    '            ViewState("decAnggaranBelanja") += AnggaranBelanja

    '        End If

    '        If e.Row.RowType = DataControlRowType.Footer Then

    '            Dim decAnggaranBelanja As Decimal = ViewState("decAnggaranBelanja")
    '            e.Row.Cells(1).Text = decAnggaranBelanja.ToString("#,##0.00")
    '            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
    '            e.Row.Cells(1).Font.Bold = True
    '        End If

    '    Catch ex As Exception
    '        sErrLog(ex.HResult, ex.Message, fGetFormName(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session("ssusrID"), Session("PcIP"), Session("PcName"), Session("SysVer"))
    '    End Try
    'End Sub



    Private Sub btnHantar_Click(sender As Object, e As EventArgs) Handles btnHantar.Click
        Try

            If TotalRec <> 0 Then
                'Session("msg") = "Anda pasti untuk hantar senarai SEMAKAN BAJET ke pejabat Bendahari?"
                strMsg = "Anda pasti untuk hantar senarai SEMAKAN BAJET ke pejabat Bendahari?"
                ViewState("Status") = "1"
                Me.ModalPopupExtender3.Show()
            End If
        Catch ex As Exception
            sErrLog(ex.HResult, ex.Message, fGetFormName(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session("ssusrID"), Session("PcIP"), Session("PcName"), Session("SysVer"))
        End Try
    End Sub

    Private Sub btnkemaskini_Click(sender As Object, e As EventArgs) Handles btnkemaskini.Click
        If TotalRec <> 0 Then
            'Session("msg") = "Anda pasti untuk pulangkan senarai SEMAKAN BAJET ke penyedia bajet?"
            strMsg = "Anda pasti untuk pulangkan senarai SEMAKAN BAJET ke penyedia bajet?"
            ViewState("Status") = "2"
            Me.ModalPopupExtender3.Show()
        End If
    End Sub

    Private Sub Pengesahan_Ketua_PTJ_Init(sender As Object, e As EventArgs) Handles Me.Init
        fPopMohonDt()
    End Sub

    'Protected Sub gvPermohonan_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvPermohonan.RowCommand

    '    ' CommandName property to determine which button was clicked.
    '    If e.CommandName = "Select" Then

    '        ' Convert the row index stored in the CommandArgument
    '        ' property to an Integer.
    '        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
    '        Dim KodSubMenu = Request.QueryString("KodSubMenu")

    '        ' Get the last name of the selected author from the appropriate
    '        ' cell in the GridView control.
    '        Dim selectedRow As GridViewRow = gvPermohonan.Rows(index)
    '        Dim NoMohon As String = selectedRow.Cells(2).Text

    '        ' Display the selected author.
    '        'Message.Text = "You selected " & contact & "."

    '        'Open other page.
    '        Response.Redirect($"~/FORMS/BAJET/SEMAKAN BAJET/MaklumatPenyediaanBajetBendahari.aspx?KodSubMenu={KodSubMenu}&no={NoMohon}")
    '    End If
    'End Sub

    Private Function fFindRec() As DataTable
        Try
            Dim strSql As String

            strSql = "SELECT a.KodPTJ,a.Butiran,a.Singkatan,a.Status AS StatusPTJ,(a.KodPTJ + ' - ' + a.Butiran) as PTJ,
                                           b.KodBah, b.NamaBahagian, b.Status AS StatusBhgn,(b.KodBah + ' - ' + b.NamaBahagian) as Bahagian,
                                           c.KodUnit, c.NamaUnit, c.Status AS StatusUnit,(c.KodUnit + ' - ' + c.NamaUnit) as Unit
                                    FROM MK_PTJ a Left OUTER JOIN MK_BahagianPTJ b ON b.KodPtj = a.KodPTJ
                                    LEFT OUTER JOIN MK_UnitPTJ AS c ON b.KodBah = c.KodBah
                                    where a.Status=1 and
                                    a.KodPTJ='410000'"

            ds = BindSQL(strSql)
            dvPTJ = New DataView(ds.Tables(0))
            ds = Nothing

            strSql = "SELECT a.BG20_NoMohon, a.BG20_Program, b.Butiran,a.BG20_AmaunMohon,c.Butiran,
                             a.BG20_TarikhMohon, a.KodPtj, a.KodBahagian, a.KodUnitPtj, 
                             a.BG20_TahunBajet, a.KodDasar, a.KodOperasi, a.KodKW, a.BG20_Status
                         
                            FROM  BG01_Mohon_RV  a INNER JOIN MK_KodOperasi b ON a.KodOperasi = b.KodKO
                            INNER JOIN BG_StatusDok c ON a.StatusDok = c.KodStatusDok
                            WHERE (StatusDok = '03') OR
                            (StatusDok = '08')"
            '    "SELECT a.BG20_NoMohon, a.BG20_Program,f.Butiran ,d.Total,a.BG20_Status,
            '                   a.KodOperasi,a.BG20_TarikhMohon, a.KodPtj, a.KodBahagian, a.KodUnitPtj,
            '                   a.KodMaksud, a.KodKorporat, a.BG20_Justifikasi, a.BG20_TahunBajet,
            '                   a.KodDasar,   a.StaffIDPemohon, a.BG20_StatusDok,
            '                   a.StaffIDPenyokong, a.StaffIDPengesah, a.StaffIDPelulus,
            '                   b.BG20_NoButiran, b.BG20_Butiran, 
            '                   b.BG20_Kuantiti, b.BG20_AngHrgUnit, b.BG20_AngJumlah, b.BG20_Status AS StatusButiran, 
            '                   b.KodVotSebagai, b.KodVotLanjut

            '                   From BG01_Mohon_RV a INNER Join BG01_MohonDt_RV b On a.BG20_NoMohon = b.BG20_NoMohon
            '                   LEFT OUTER JOIN
            '                       (SELECT c.BG20_NoMohon, SUM(c.BG20_AngJumlah) AS Total
            '                        FROM  BG01_MohonDt_RV c
            '                        GROUP BY c.BG20_NoMohon) d ON d.BG20_NoMohon = b.BG20_NoMohon
            'LEFT OUTER JOIN
            '                       (SELECT e.KodOperasi,e.Butiran
            '                        FROM  BG_KodOperasi e
            '                        ) f ON a.KodOperasi = f.KodOperasi
            '                  WHERE  a.BG20_Status=1 and a.KodPTJ=41"

            ds = BindSQL(strSql)
            dvPermohonan = New DataView(ds.Tables(0))
            ds = Nothing

        Catch ex As Exception
            'fErrorLog("Pendaftaran_Menu.aspx(fCreateDt())- " & ex.Message.ToString())
        End Try
    End Function
    Private Function fCreateDt(ByVal strUnit As String, ByVal strBahagian As String, ByVal strKod As String) As DataTable

        Try
            Dim dt As New DataTable

            'dt.Columns.Add("Pilih", GetType(Boolean))
            dt.Columns.Add("Bil", GetType(Integer))
            dt.Columns.Add("NoPermohonan", GetType(String))
            dt.Columns.Add("Program", GetType(String))
            dt.Columns.Add("KodKW", GetType(String))
            dt.Columns.Add("KodOperasi", GetType(String))
            dt.Columns.Add("Anggaran", GetType(Double))
            dt.Columns.Add("Status", GetType(String))

            'Dim blnPilih As Boolean
            Dim intBil As Integer
            Dim strNoPermohonan As String
            Dim strProgram As String
            Dim strKodKW As String
            Dim strKodOperasi As String
            Dim dblAnggaran As Double
            Dim strStatus As String

            If strBahagian = "00" Then
                dvPermohonan.RowFilter = [String].Empty
            Else
                dvPermohonan.RowFilter = "KodBahagian= '" & strBahagian & "'"
                If strUnit <> "00" Then
                    dvPermohonan.RowFilter = "KodUnit= '" & strUnit & "'"
                End If
            End If

            If strKod <> "00" Then
                dvPermohonan.RowFilter = "KodOperasi= '" & strKod & "'"
            End If

            'For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            For i As Integer = 0 To dvPermohonan.Count - 1
                'blnPilih = False
                intBil = i + 1
                strNoPermohonan = dvPermohonan(i)(0).ToString
                strProgram = dvPermohonan(i)(1).ToString
                strKodKW = dvPermohonan(i)(12).ToString
                strKodOperasi = dvPermohonan(i)(11).ToString
                dblAnggaran = dvPermohonan(i)(3).ToString
                strStatus = dvPermohonan(i)(4).ToString
                dt.Rows.Add(intBil, strNoPermohonan, strProgram, strKodKW, strKodOperasi, dblAnggaran, strStatus)
            Next

            Return dt

        Catch ex As Exception
            'fErrorLog("Pendaftaran_Menu.aspx(fCreateDt())- " & ex.Message.ToString())
        End Try

    End Function

    Private Function BindSQL(ByVal strSql As String) As DataSet
        Try
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)
            Return ds
        Catch ex As Exception
            'fErrorLog(ex.Message.ToString)
        End Try
    End Function



    Dim decAmt10000Op, decAmt20000Op, decAmt30000Op, decAmt40000Op, decAmt50000Op As Decimal
    Dim decAmt10000Ko, decAmt20000Ko, decAmt30000Ko, decAmt40000Ko, decAmt50000Ko As Decimal
    Dim decJumAgih As Decimal

    Private Sub gvAgihPTj_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvAgihPTj.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.ForeColor = ColorTranslator.FromHtml("#000000")

                Dim lblAmt10000Op As LinkButton = CType(e.Row.FindControl("lblAmt10000Op"), LinkButton)
                decAmt10000Op += CDec(lblAmt10000Op.Text)

                Dim lblAmt10000Ko As LinkButton = CType(e.Row.FindControl("lblAmt10000Ko"), LinkButton)
                decAmt10000Ko += CDec(lblAmt10000Ko.Text)

                Dim lblAmt20000Op As LinkButton = CType(e.Row.FindControl("lblAmt20000Op"), LinkButton)
                decAmt20000Op += CDec(lblAmt20000Op.Text)

                Dim lblAmt20000Ko As LinkButton = CType(e.Row.FindControl("lblAmt20000Ko"), LinkButton)
                decAmt20000Ko += CDec(lblAmt20000Ko.Text)

                Dim lblAmt30000Op As LinkButton = CType(e.Row.FindControl("lblAmt30000Op"), LinkButton)
                decAmt30000Op += CDec(lblAmt30000Op.Text)

                Dim lblAmt30000Ko As LinkButton = CType(e.Row.FindControl("lblAmt30000Ko"), LinkButton)
                decAmt30000Ko += CDec(lblAmt30000Ko.Text)

                Dim lblAmt40000Op As LinkButton = CType(e.Row.FindControl("lblAmt40000Op"), LinkButton)
                decAmt40000Op += CDec(lblAmt40000Op.Text)

                Dim lblAmt40000Ko As LinkButton = CType(e.Row.FindControl("lblAmt40000Ko"), LinkButton)
                decAmt40000Ko += CDec(lblAmt40000Ko.Text)

                Dim lblAmt50000Op As LinkButton = CType(e.Row.FindControl("lblAmt50000Op"), LinkButton)
                decAmt50000Op += CDec(lblAmt50000Op.Text)

                Dim lblAmt50000Ko As LinkButton = CType(e.Row.FindControl("lblAmt50000Ko"), LinkButton)
                decAmt50000Ko += CDec(lblAmt50000Ko.Text)

                Dim lblAmtJum As Label = CType(e.Row.FindControl("lblAmtJum"), Label)
                decJumAgih += CDec(lblAmtJum.Text)

            End If

            If e.Row.RowType = DataControlRowType.Footer Then

                Dim lblJumAm10000Op As Label = CType(e.Row.FindControl("lblJumAm10000Op"), Label)
                lblJumAm10000Op.Text = FormatNumber(decAmt10000Op)

                Dim lblJumAm10000Ko As Label = CType(e.Row.FindControl("lblJumAm10000Ko"), Label)
                lblJumAm10000Ko.Text = FormatNumber(decAmt10000Ko)

                Dim lblJumAm20000Op As Label = CType(e.Row.FindControl("lblJumAm20000Op"), Label)
                lblJumAm20000Op.Text = FormatNumber(decAmt20000Op)

                Dim lblJumAm20000Ko As Label = CType(e.Row.FindControl("lblJumAm20000Ko"), Label)
                lblJumAm20000Ko.Text = FormatNumber(decAmt20000Ko)

                Dim lblJumAm30000Op As Label = CType(e.Row.FindControl("lblJumAm30000Op"), Label)
                lblJumAm30000Op.Text = FormatNumber(decAmt30000Op)

                Dim lblJumAm30000Ko As Label = CType(e.Row.FindControl("lblJumAm30000Ko"), Label)
                lblJumAm30000Ko.Text = FormatNumber(decAmt30000Ko)

                Dim lblJumAm40000Op As Label = CType(e.Row.FindControl("lblJumAm40000Op"), Label)
                lblJumAm40000Op.Text = FormatNumber(decAmt40000Op)

                Dim lblJumAm40000Ko As Label = CType(e.Row.FindControl("lblJumAm40000Ko"), Label)
                lblJumAm40000Ko.Text = FormatNumber(decAmt40000Ko)

                Dim lblJumAm50000Op As Label = CType(e.Row.FindControl("lblJumAm50000Op"), Label)
                lblJumAm50000Op.Text = FormatNumber(decAmt50000Op)

                Dim lblJumAm50000Ko As Label = CType(e.Row.FindControl("lblJumAm50000Ko"), Label)
                lblJumAm50000Ko.Text = FormatNumber(decAmt50000Ko)

                Dim lblAmtJumAll As Label = CType(e.Row.FindControl("lblAmtJumAll"), Label)
                lblAmtJumAll.Text = FormatNumber(decJumAgih)
            End If

            If e.Row.RowType = DataControlRowType.Header Then
                Dim lblTitleAgihan As Label = CType(e.Row.FindControl("lblTitleAgihan"), Label)
                lblTitleAgihan.Text = "Agihan " & CInt(txtYear.Text) & " (RM)"

            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub gvAgihPTj_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvAgihPTj.RowCreated
        Try
            If e.Row.RowType = DataControlRowType.Header Then

                Dim row As GridViewRow = New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal)

                Dim cell As TableCell = New TableCell()
                cell = New TableCell()
                cell.ColumnSpan = 2
                row.Cells.Add(cell)

                cell = New TableCell()
                cell.ColumnSpan = 2
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.Text = "VOT 10000"
                cell.Font.Bold = True
                row.Cells.Add(cell)

                cell = New TableCell()
                cell.ColumnSpan = 2
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.Text = "VOT 20000"
                cell.Font.Bold = True
                row.Cells.Add(cell)

                cell = New TableCell()
                cell.ColumnSpan = 2
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.Text = "VOT 30000"
                cell.Font.Bold = True
                row.Cells.Add(cell)

                cell = New TableCell()
                cell.ColumnSpan = 2
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.Text = "VOT 40000"
                cell.Font.Bold = True
                row.Cells.Add(cell)

                cell = New TableCell()
                cell.ColumnSpan = 2
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.Text = "VOT 50000"
                cell.Font.Bold = True
                row.Cells.Add(cell)

                cell = New TableCell()
                cell.ColumnSpan = 4
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.BackColor = Color.White
                row.Cells.Add(cell)

                gvAgihPTj.Controls(0).Controls.AddAt(0, row)

                e.Row.BackColor = ColorTranslator.FromHtml("#FECB18")

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function fGetAmtVotAm(strTahun, strKodKW, strKO, strKodPTj, strVotAm) As Decimal
        Try

            Dim strSql As String
            'strSql = "select BG06_Amaun from BG06_AgihObjAm where BG06_Tahun = '" & strTahun & "' and KodPTJ = '" & strKodPTj & "' and KodKw = '" & strKodKW & "' and KodVot = '" & strVotAm & "'"

            strSql = $"select 
case when ISNULL(SUM(bjam.JumlahLPU ),0) = 0 then
ISNULL(SUM(BG20_JumLPU),0) 
else
ISNULL(SUM(bjam.JumlahLPU ),0)
end as BG06_Amaun
 from BG01_Mohon_RV as a inner join BG01_MohonDt_RV as b on a.BG20_NoMohon = b.BG20_NoMohon left join BG01_JumlahBajetAm as bjam 
on bjam.BG20_TahunBajet = a.BG20_TahunBajet 
and bjam.KodPtj = a.KodPtj and bjam.KodKW = a.KodKW  and bjam.KodOperasi = a.KodOperasi  
and bjam.KodVotAm = b.KodVotAm
where a.BG20_TahunBajet = '{strTahun}' and a.KodKW = '{strKodKW}' and a.KodOperasi = '{strKO}' and a.KodPtj = '{strKodPTj}' and b.KodVotAm = '{strVotAm}' and b.BG20_Aktif ='1' and BG20_FlagNC = '1'"

            Dim decAmt As Decimal = 0.00
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    decAmt = CDec(IIf(IsDBNull(ds.Tables(0).Rows(0)("BG06_Amaun")), 0.00, ds.Tables(0).Rows(0))("BG06_Amaun"))
                End If
            End If

            Return decAmt
        Catch ex As Exception

        End Try
    End Function

    Private Sub ddlKW_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKW.SelectedIndexChanged

        Try
            'sClearField()
            If ddlKW.SelectedIndex <> 0 Then
                fBindGvPTj()
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub fBindGvPTj()
        sClearGvPTj()

        Try

            Dim strSql As String
            Dim strKodKW As String = Trim(ddlKW.SelectedValue.TrimEnd)
            Dim strTahun = dateNext.Year 'txtYear.Text 'ddlTahunAgih.SelectedValue.TrimEnd

            If strKodKW = "- SILA PILIH KW -" Then
                strKodKW = "01"
            End If

            sClearGvPTj()
            strSql = "select distinct MK_PTJ.KodPTJ, MK_PTJ.Butiran from MK_PTJ inner join MK01_VotTahun on MK01_VotTahun.KodPTJ = MK_PTJ.KodPTJ
where MK01_Tahun = '" & Date.Now.Year.ToString & "' and MK01_VotTahun.KodKw = '" & strKodKW & "' order by MK_PTJ .KodPTJ"
            Dim ds As New DataSet
            'Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then

                    Dim strKodPTJ, strButiranPTj As String
                    Dim decAmt10000Op, decAmt20000Op, decAmt30000Op, decAmt40000Op, decAmt50000Op As Decimal
                    Dim decAmt10000Ko, decAmt20000Ko, decAmt30000Ko, decAmt40000Ko, decAmt50000Ko As Decimal
                    Dim decJumAgihOp, decJumAgihKo As Decimal
                    Dim decJumPtj As Decimal
                    Dim strAmtMohon

                    Dim dt As New DataTable
                    dt.Columns.Add("KodPTj", GetType(String))
                    dt.Columns.Add("ButPTj", GetType(String))
                    dt.Columns.Add("AmtMohon", GetType(String))
                    dt.Columns.Add("Amt10000Op", GetType(Decimal))
                    dt.Columns.Add("Amt10000Ko", GetType(Decimal))
                    dt.Columns.Add("Amt20000Op", GetType(Decimal))
                    dt.Columns.Add("Amt20000Ko", GetType(Decimal))
                    dt.Columns.Add("Amt30000Op", GetType(Decimal))
                    dt.Columns.Add("Amt30000Ko", GetType(Decimal))
                    dt.Columns.Add("Amt40000Op", GetType(Decimal))
                    dt.Columns.Add("Amt40000Ko", GetType(Decimal))
                    dt.Columns.Add("Amt50000Op", GetType(Decimal))
                    dt.Columns.Add("Amt50000Ko", GetType(Decimal))
                    dt.Columns.Add("JumAgih", GetType(Decimal))

                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        strKodPTJ = Left(Trim(ds.Tables(0).Rows(i)("KodPTJ")), 2)
                        strButiranPTj = Trim(ds.Tables(0).Rows(i)("Butiran"))
                        strAmtMohon = "-"
                        decAmt10000Op = fGetAmtVotAm(strTahun, strKodKW, "01", strKodPTJ, "10000")
                        decAmt10000Ko = fGetAmtVotAm(strTahun, strKodKW, "02", strKodPTJ, "10000")
                        decAmt20000Op = fGetAmtVotAm(strTahun, strKodKW, "01", strKodPTJ, "20000")
                        decAmt20000Ko = fGetAmtVotAm(strTahun, strKodKW, "02", strKodPTJ, "20000")
                        decAmt30000Op = fGetAmtVotAm(strTahun, strKodKW, "01", strKodPTJ, "30000")
                        decAmt30000Ko = fGetAmtVotAm(strTahun, strKodKW, "02", strKodPTJ, "30000")
                        decAmt40000Op = fGetAmtVotAm(strTahun, strKodKW, "01", strKodPTJ, "40000")
                        decAmt40000Ko = fGetAmtVotAm(strTahun, strKodKW, "02", strKodPTJ, "40000")
                        decAmt50000Op = fGetAmtVotAm(strTahun, strKodKW, "01", strKodPTJ, "50000")
                        decAmt50000Ko = fGetAmtVotAm(strTahun, strKodKW, "02", strKodPTJ, "50000")

                        decJumAgihOp = decAmt10000Op + decAmt20000Op + decAmt30000Op + decAmt40000Op + decAmt50000Op

                        decJumAgihKo = decAmt10000Ko + decAmt20000Ko + decAmt30000Ko + decAmt40000Ko + decAmt50000Ko

                        decJumAgih = decJumAgihOp + decJumAgihKo

                        dt.Rows.Add(strKodPTJ, strButiranPTj, strAmtMohon, decAmt10000Op, decAmt10000Ko, decAmt20000Op, decAmt20000Ko, decAmt30000Op, decAmt30000Ko, decAmt40000Op, decAmt40000Ko, decAmt50000Op, decAmt50000Ko, decJumAgih)
                    Next

                    gvAgihPTj.DataSource = dt
                    gvAgihPTj.DataBind()
                End If
            End If

        Catch ex As Exception

        End Try

    End Sub
    Private Sub sClearGvPTj()
        gvAgihPTj.DataSource = New List(Of String)
        gvAgihPTj.DataBind()
    End Sub
    Private Sub fBindDdlKW()
        Try

            Dim strSql As String = "SELECT DISTINCT TOP 100 PERCENT dbo.MK_Kw.KodKw, (dbo.MK_Kw.KodKw + ' - ' + dbo.MK_Kw.Butiran) as Butiran " &
                  "FROM dbo.MK_Kw INNER JOIN dbo.MK01_VotTahun ON dbo.MK_Kw.KodKw = dbo.MK01_VotTahun.KodKw " &
                    "where dbo.MK01_VotTahun.MK01_Tahun = '" & dateNext.Year & "' ORDER BY dbo.MK_Kw.KodKw"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)
            ddlKW.DataSource = ds

            ddlKW.DataTextField = "Butiran"
            ddlKW.DataValueField = "KodKW"
            ddlKW.DataBind()

            ddlKW.Items.Insert(0, "- SILA PILIH KW -")
            ddlKW.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvAgihPTj_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvAgihPTj.SelectedIndexChanged

    End Sub

    Protected Function Limit(ByVal desc As Object, ByVal maxLength As Integer) As String
        Dim description = CStr(desc)

        If String.IsNullOrEmpty(description) Then
            Return description
        End If

        Return If(description.Length <= maxLength, description, description.Substring(0, maxLength) & "...")
    End Function

    Protected Sub OnRowDataBoundxx(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(3).ToolTip = TryCast(e.Row.DataItem, DataRowView)("BG20_Program").ToString()
            e.Row.Cells(4).ToolTip = TryCast(e.Row.DataItem, DataRowView)("BG20_Justifikasi").ToString()
        End If

    End Sub

    Private Sub fBindGVMohonBajet(KodStatus As String)
        Try
            Dim ayatSql As String
            Dim strSql As String = ""
            Dim kodkw = ddlKW.SelectedValue
            Dim strSql_count As String = ""

            If (kodkw <> "") Then
                ayatSql = ayatSql + $" and KodKW = '{ddlKW.SelectedValue}' "
            End If

            If KodStatus = String.Empty Then
                strSql = $"select DISTINCT a.BG20_NoMohon, BG20_TarikhMohon, BG20_Program, ISNULL((BG20_AmaunBajet),0) as BG20_AmaunMohon, StatusDok, KodKW, KodOperasi, BG20_Justifikasi from BG01_Mohon_RV as a WHERE a.BG20_Status = 1 and BG20_FlagNC = '1' " + ayatSql + " ORDER BY BG20_NoMohon DESC,  BG20_TarikhMohon DESC"
                strSql_count = $"select count(DISTINCT a.BG20_NoMohon) as bil  from BG01_Mohon_RV as a WHERE a.BG20_Status = 1 and B20_FlagNC = '1'  " + ayatSql
            Else
                strSql = $"select DISTINCT a.BG20_NoMohon, BG20_TarikhMohon, BG20_Program, ISNULL((BG20_AmaunBajet),0) as BG20_AmaunMohon, StatusDok, KodKW, KodOperasi, BG20_Justifikasi from BG01_Mohon_RV as a where StatusDok='{KodStatus}'   and BG20_FlagNC = '1' " + ayatSql + " ORDER BY BG20_NoMohon DESC,  BG20_TarikhMohon DESC"
                strSql_count = $"select count(DISTINCT a.BG20_NoMohon) as bil  from BG01_Mohon_RV as a WHERE a.BG20_Status = 1 and B20_FlagNC = '1'  " + ayatSql
            End If

            Dim dt = fCreateDt(strSql)
            Dim total As Decimal = dt.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("BG20_AmaunMohon"))
            ViewState("TotalAmount") = total

            gvMohonBajet.DataSource = dt
            gvMohonBajet.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Private Function fCreateDt(strSql As String) As DataTable
        Dim dbconn As New DBKewConn

        'pass as reference 
        Dim ds = dbconn.fSelectCommand(strSql)

        'Convert dataset to datatable
        Dim dt = ds.Tables(0)

        If dt.Rows.Count > 0 Then
            Dim strDate As DateTime = Nothing
            'dt.Columns.Add("TarikhMohon", GetType(String))
            dt.Columns.Add("Status", GetType(String))
            dt.Columns.Add("AngJumBesar", GetType(Decimal))

            For Each row As DataRow In dt.Rows
                'strDate = row("BG20_TarikhMohon").ToString
                'row("TarikhMohon") = strDate.ToString("dd/MM/yyyy")
                Dim DicStatusDok As Dictionary(Of String, String) = fGetStatusDok(row("StatusDok").ToString)
                Dim status = DicStatusDok.FirstOrDefault.Value
                row("Status") = fGetStatusDok(row("StatusDok").ToString).FirstOrDefault.Value
                row("AngJumBesar") = CDec(row("BG20_AmaunMohon")) '.ToString("#,##0.00")
                'fGetJumlahBesarAnggaran(row("BG20_NoMohon").ToString)
            Next
        End If

        Return dt
    End Function

    Private Function fCreateDt2(strSql As String) As DataTable
        Dim dbconn As New DBKewConn

        'pass as reference 
        Dim ds = dbconn.fSelectCommand(strSql)

        'Convert dataset to datatable
        Dim dt = ds.Tables(0)

        If dt.Rows.Count > 0 Then
            Dim strDate As DateTime = Nothing
            'dt.Columns.Add("TarikhMohon", GetType(String))
            'dt.Columns.Add("Status", GetType(String))
            dt.Columns.Add("AngJumBesar", GetType(Decimal))

            For Each row As DataRow In dt.Rows
                'strDate = row("BG20_TarikhMohon").ToString
                'row("TarikhMohon") = strDate.ToString("dd/MM/yyyy")
                'Dim DicStatusDok As Dictionary(Of String, String) = fGetStatusDok(row("StatusDok").ToString)
                'Dim status = DicStatusDok.FirstOrDefault.Value
                'row("Status") = fGetStatusDok(row("StatusDok").ToString).FirstOrDefault.Value
                row("AngJumBesar") = CDec(row("AngHrgUnit")) '.ToString("#,##0.00")
                'fGetJumlahBesarAnggaran(row("BG20_NoMohon").ToString)
            Next
        End If

        Return dt
    End Function

    Protected Sub gvAgihPTj_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvAgihPTj.RowCommand


        ' Convert the row index stored in the CommandArgument
        ' property to an Integer.
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim KodSubMenu = Request.QueryString("KodSubMenu")

        ' Get the last name of the selected author from the appropriate
        ' cell in the GridView control.
        Dim selectedRow As GridViewRow = gvAgihPTj.Rows(index)
        Dim NoMohon As String = selectedRow.Cells(5).Text

        ' Display the selected author.
        'Message.Text = "You selected " & contact & "."


        ' CommandName property to determine which button was clicked.
        If e.CommandName = "Select" Then

            'Open other page.
            Response.Redirect($"~/FORMS/BAJET/SEMAKAN BAJET/MaklumatSemakanBajet_LPU_RV.aspx?KodSubMenu={KodSubMenu}&no={NoMohon}")

        ElseIf e.CommandName = "SelectVot" Then
            fBindVot(NoMohon)
            mpePnlSenaraiVot.Show()

        ElseIf e.CommandName = "10Op" Or e.CommandName = "10Ko" Or e.CommandName = "20Op" Or e.CommandName = "20Ko" Or e.CommandName = "30Op" Or e.CommandName = "30Ko" Or e.CommandName = "40Op" Or e.CommandName = "40Ko" Or e.CommandName = "50Op" Or e.CommandName = "50Ko" Then

            Try
                Dim strKodKW = Trim(ddlKW.SelectedValue.TrimEnd)
                Dim strTahun = Trim(dateNext.Year)
                Dim strKodSub = Request.QueryString("KodSub")
                Dim strKodSubMenu = Request.QueryString("KodSubMenu")
                Dim chckVot = Left(e.CommandName, 1)
                Dim chckKo = Right(e.CommandName, 2)
                Dim strKodKO As String

                'Dim row As GridViewRow = gvAgihPTj.SelectedRow

                Dim ctrl As Control = TryCast(e.CommandSource, Control)

                Dim gvRow As GridViewRow = TryCast(ctrl.Parent.NamingContainer, GridViewRow)

                Dim strKodPTj_ As Label = CType(gvRow.FindControl("lblKodPTJ"), Label)  ' Find Your Control here
                Dim strKodPTj = strKodPTj_.Text

                Dim lblCheck_ As LinkButton = CType(gvRow.FindControl("lblAmt" + chckVot + "0000Op"), LinkButton)  ' Find Your Control here
                Dim lblCheck = lblCheck_.Text


                'check if jumlah 0


                If lblCheck <> "0.00" Then
                    Dim strKodVot = chckVot + "0000"

                    If chckKo = "Op" Then
                        strKodKO = "01"
                    ElseIf chckKo = "Ko" Then
                        strKodKO = "02"
                    End If

                    'Open other page.
                    Response.Redirect($"~/FORMS/BAJET/SEMAKAN BAJET/Maklumat_Agihan_Bajet_Am_LPU_RV.aspx?KodSub=" & strKodSub & "&KodSubMenu=" & strKodSubMenu & "&KodPTj=" & strKodPTj & "&Tahun=" & strTahun & "&KodKW=" & strKodKW & "&kodVot=" & strKodVot & "&strKodKO=" & strKodKO & "", False)

                Else
                    fGlobalAlert("Tiada maklumat untuk dipaparkan!", Me.Page, Me.[GetType]())
                End If

            Catch ex As Exception

            End Try



        End If
    End Sub

    Protected Sub lbtnHantar_Click(sender As Object, e As EventArgs) Handles lbtnHantar.Click

        Dim strbuildr As New StringBuilder(String.Empty)
        Dim rows = gvMohonBajet.Rows
        Dim chk As CheckBox
        Dim dbconn As New DBKewConn
        Dim dsMohon As New DataSet
        Dim resultUpdate = False
        Dim resultCommit = False
        Dim strSqlButiran = ""
        Dim paramSqlBtrn() As SqlParameter = Nothing
        'Dim tarikhMohon As DateTime
        Dim ddlStatus As String
        Dim statusDok As String

        Dim dt = Date.Today.ToString("dd/MM/yyyy").ToString
        Dim dtTkhM As Date = DateTime.ParseExact(dt, "dd/MM/yyyy", CultureInfo.CurrentCulture)
        Dim tarikhMohon As String = dtTkhM.ToString("yyyy-MM-dd")

        ddlStatus = rblStatus.SelectedValue

        If ddlStatus = "0" And txtUlasan.Text = "" Then

            fGlobalAlert("Maklumat Butiran tidak berjaya disimpan. Sila pastikan ulasan dimasukkan untuk status Tidak Lengkap", Me.Page, Me.[GetType]())

        Else

            Dim flagKB
            'If ddlStatus = "1" Then
            '    statusDok = "29"
            'ElseIf ddlStatus = "0" Then
            '    statusDok = "30"
            'End If

            If ddlStatus = "1" Then
                statusDok = "29"
                flagKB = " ,  BG20_FlagLPU = '1'"
            ElseIf ddlStatus = "0" Then
                statusDok = "30"
                flagKB = " ,  BG20_FlagPemohon = '0', BG20_FlagKB = '0', B20_FlagKew = '0', B20_FlagPTj = '0', BG20_FlagLPU = '0' "
            End If

            'tarikhMohon = String.Format("{0:dd/MM/yyyy}", DateTime.Now)


            Dim paramSql() As SqlParameter = {}

            Dim noMohon As String

            dbconn.sConnBeginTrans()

            countButiran = 0

            For Each row As GridViewRow In rows
                chk = DirectCast(row.Cells(8).FindControl("CheckBox1"), CheckBox)
                If chk.Checked Then
                    noMohon = CType(row.FindControl("lblNoMohon"), Label).Text.ToString


                    strSqlButiran = "Update BG01_Mohon_RV set StatusDok = @statusDok , BG20_StaffIDBendahari = @idSemak ,BG20_TarikhLulusBend = @tarikh   " + flagKB + "  WHERE BG20_Status = @status and BG20_NoMohon = @NoMohon "
                    paramSqlBtrn = {
                        New SqlParameter("@NoMohon", noMohon),
                        New SqlParameter("@status", True),
                        New SqlParameter("@statusDok", statusDok),
                        New SqlParameter("@idSemak", Session("ssusrID")),
                        New SqlParameter("@tarikh", tarikhMohon)
                    }

                    Dim strSQLStatusDok = "INSERT INTO BG12_StatusDok (BG01_NoPermohonan, KodStatusDok, BG12_TkhProses, BG12_NoStaf, BG12_Ulasan) " &
                           " VALUES (@NoMohon, @StatusKod, @Tarikh, @NoStaf,@Ulasan)"

                    Dim paramSql2() As SqlParameter =
                      {
                         New SqlParameter("@NoMohon", noMohon),
                         New SqlParameter("@StatusKod", statusDok),
                         New SqlParameter("@Tarikh", tarikhMohon),
                         New SqlParameter("@NoStaf", Session("ssusrID")),
                         New SqlParameter("@Ulasan", txtUlasan.Text)
                         }

                    If dbconn.fUpdateCommand(strSqlButiran, paramSqlBtrn) > 0 Then
                        countButiran = countButiran + 1

                        dbconn.fInsertCommand(strSQLStatusDok, paramSql2)

                        dbconn.sConnCommitTrans()
                        'sAuditLog(Session("ssusrID"), Request.QueryString("KodSubMenu"), "BG20_Mohon", "1", "Simpan Button")
                    End If



                End If
            Next

            If countButiran > 0 Then
                fGlobalAlert($"Berjaya simpan", Me.Page, Me.[GetType](), "../../BAJET/SEMAKAN BAJET/Pengesahan_LPU.aspx")
            Else
                fGlobalAlert("Maklumat Butiran tidak berjaya disimpan!", Me.Page, Me.[GetType]())
            End If

        End If
    End Sub

    Private Sub fBindVot(NoMohon As String)
        Try
            Dim strSql As String = ""
            Dim dsMohon_View As New DataSet
            Dim strSqlDt As String = ""


            strSql = $"select DISTINCT a.BG20_NoMohon, BG20_TarikhMohon, BG20_Program, BG20_AmaunMohon, StatusDok, KodKW, KodOperasi, BG20_Justifikasi from BG01_Mohon_RV as a where a.BG20_NoMohon='{NoMohon}' "

            dsMohon_View = dbconn.fSelectCommand(strSql)
            Dim ds = dbconn.fSelectCommand(strSql)
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    lblDtAktiviti.Text = ds.Tables(0).Rows(0)("BG20_Program").ToString
                    lblHiddenID.Text = ds.Tables(0).Rows(0)("BG20_NoMohon").ToString
                    Dim jumlahMohon = ds.Tables(0).Rows(0)("BG20_AmaunMohon").ToString
                    txtJumlahPermohonan.Text = FormatNumber(jumlahMohon, 2)
                End If
            End If

            'baca bajet mohondt
            strSqlDt = $"select KodVotAm as KodVotA, KodVotSebagai as KodVotSbg, BG20_Butiran as Butiran, BG20_AngJumlah as AngHrgUnit
            from BG01_MohonDt_RV as a where a.BG20_NoMohon='{NoMohon}'
            order by KodVotAm, KodVotSebagai"

            Dim dt = fCreateDt2(strSqlDt)
            ' Dim total As Decimal = dt.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("BG20_AmaunMohon"))
            'ViewState("TotalAmount") = total
            gvButiranVot.DataSource = dt
            gvButiranVot.DataBind()


        Catch ex As Exception

        End Try
    End Sub
    Protected Sub txtBajet_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim footerRow = gvButiranVot.FooterRow
            Dim decJumBajetAm As Decimal
            decJumBajetAm = 0

            Dim txtBajet As TextBox = CType(sender, TextBox)
            Dim gvr As GridViewRow = CType(txtBajet.Parent.Parent, GridViewRow)
            Dim rowindex As Integer = gvr.RowIndex
            Dim selectedRow As GridViewRow = gvButiranVot.Rows(gvr.RowIndex)

            Dim lblJum As Label = CType(selectedRow.FindControl("lblJum"), Label)

            If txtBajet.Text = "" Then txtBajet.Text = "0.00"
            Dim decBajet As Decimal = CDec(txtBajet.Text)
            txtBajet.Text = FormatNumber(decBajet)

            'Semak jumlah besar
            Dim decBajetAm As Decimal
            For Each gvRow As GridViewRow In gvButiranVot.Rows
                decBajetAm = Trim(CType(gvRow.FindControl("txtBajet"), TextBox).Text.TrimEnd)
                decJumBajetAm += decBajetAm 'Total Jumlah Bajet Objek Am
            Next

            txtJumlahPermohonan.Text = FormatNumber(decJumBajetAm)
            CType(footerRow.FindControl("lblTotBajet"), Label).Text = FormatNumber(decJumBajetAm) 'Total Jumlah Besar Bajet
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lblKemaskiniVot_Click(sender As Object, e As EventArgs) Handles lblKemaskiniVot.Click
        countButiran = 0

        Dim strSQL_Save_ = $"UPDATE BG01_Mohon_RV SET BG20_AmaunMohon = @amaun Where BG20_NoMohon = @noMohon"

        Dim paramSql_simpan_() As SqlParameter =
             {
             New SqlParameter("@amaun", txtJumlahPermohonan.Text),
             New SqlParameter("@noMohon", lblHiddenID.Text)}

        Dim dbconn_ As New DBKewConn
        dbconn_.sConnBeginTrans()
        If dbconn_.fUpdateCommand(strSQL_Save_, paramSql_simpan_) > 0 Then
            countButiran = countButiran + 1
            dbconn_.fUpdateCommand(strSQL_Save_, paramSql_simpan_)
            dbconn_.sConnCommitTrans()



            If countButiran > 0 Then
                fGlobalAlert($"Maklumat berjaya dikemaskini.", Me.Page, Me.[GetType](), "../../BAJET/SEMAKAN BAJET/Pengesahan_LPU.aspx")
            Else
                fGlobalAlert("Maklumat Butiran tidak berjaya disimpan!", Me.Page, Me.[GetType]())
            End If

        End If



    End Sub

    Private Sub gvMohonBajet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvMohonBajet.SelectedIndexChanged
        Dim KodSub = Request.QueryString("KodSub")
        Dim KodSubMenu = Request.QueryString("KodSubMenu")

        Dim row As GridViewRow = gvMohonBajet.SelectedRow
        Dim strNoMohon = CType(row.FindControl("lblNoMohon"), Label).Text.ToString

        'Open other page.
        Response.Redirect($"~/FORMS/BAJET/SEMAKAN BAJET/MaklumatSemakanBajet_LPU_RV.aspx?KodSub={KodSub}&KodSubMenu={KodSubMenu}&kodSkrin=1&no={strNoMohon}", False)
    End Sub

    Private Sub fUpload(noPermohonan As String)
        Try
            simpanPath("1", "1", "1", "Minit", "2")
            fLoadGVSalinanBajet()

            If fuBajet.HasFile Then

                Dim objHttpPostedFile As HttpPostedFile = fuBajet.PostedFile
                Dim file1 = fuBajet.PostedFile.FileName
                Dim FileName = Path.GetFileName(fuBajet.FileName)
                If file1 IsNot String.Empty Then
                    Dim noMohon = noPermohonan
                    If fuBajet.PostedFile.ContentType = "application/pdf" Or fuBajet.PostedFile.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document" Then
                        If fuBajet.PostedFile.ContentLength < 51200000 Then  '500KB
                            'Dim FileName = Path.GetFileName(fuBajet.FileName)
                            If FileName.Length < 50 Then
                                Dim folderPath As String = Server.MapPath($"~/Upload/Document/Bajet/Minit/")
                                Dim ContentType = fuBajet.PostedFile.ContentType

                                'Check whether Directory (Folder) exists.
                                If Not Directory.Exists(folderPath) Then
                                    'If Directory (Folder) does not exists. Create it.
                                    Directory.CreateDirectory(folderPath)
                                End If

                                Try
                                    'Dim IdSem = hdNoIDSemSya.Value + "_"
                                    Dim SaveFileName = FileName 'String.Concat(IdSem, FileName)

                                    'Create the path And file name to check for duplicates.
                                    Dim pathToCheck = folderPath + SaveFileName

                                    'simpanPath(folderPath, SaveFileName, ContentType, "Baje2", noMohon)

                                    'Check to see if a file already exists with the same name as the file to upload.
                                    If File.Exists(pathToCheck) Then
                                        'LabelMessage1.ForeColor = Color.Red
                                        'LabelMessage1.Text = "The target file already exists, please rename it."
                                    Else
                                        fuBajet.SaveAs(folderPath & SaveFileName)
                                        simpanPath(folderPath, SaveFileName, ContentType, "Minit", noMohon)

                                        'Save the File to the Directory (Folder).

                                        'LabelMessage1.ForeColor = Color.Green
                                        'Display the success message.
                                        'LabelMessage1.Text = FileName + " has been uploaded."


                                        fLoadGVSalinanBajet()
                                    End If

                                Catch ex As Exception
                                    'Display the success message.
                                    'LabelMessage1.ForeColor = Color.Red
                                    'LabelMessage1.Text = FileName + " could NOT be uploaded."
                                End Try
                            Else
                                'LabelMessage1.ForeColor = Color.Red
                                'LabelMessage1.Text = "The filename length cannot exceed 50 characters, please rename it."
                            End If

                        Else
                            'LabelMessage1.ForeColor = Color.Red
                            'LabelMessage1.Text = "Please upload file With size Not more than 500KB"
                        End If
                    Else
                        'LabelMessage1.ForeColor = Color.Red
                        'LabelMessage1.Text = "Please upload file With type Of 'docx' or 'pdf'."
                    End If
                Else
                    'LabelMessage1.ForeColor = Color.Red
                    'LabelMessage1.Text = "Upload failed! : No file selected."
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub simpanPath(Path As String, FileName As String, ContentType As String, JenisLamp As String, noMohon As String)
        Dim rUpdate = False, rCommit = False
        Try
            Dim IdSem = noMohon

            FileName = "MinitLpu2.pdf"
            Path = "I:\SMKB\Working Directory\SMKB Web Portal\SMKB Web Portal\Upload\Document\Bajet\Permohonan\"
            ContentType = "application/pdf"

            Dim str = $"select BG16_Id, BG01_NoMohon, BG16_NamaDok, BG16_Path, BG16_ContentType, BG16_JenisDok, BG16_Status from BG16_Lampiran 
            where BG01_NoMohon = '2' and BG16_JenisDok = 'Minit'"
            Using ds = dbconn.fSelectCommand(str, True)
                'Dim dsRows = ds.Tables(0).AsEnumerable().Where(Function(r) r.Item("BG15_NoMohon") = IdSem)

                If ds.Tables.Count > 0 Then
                    'Dim Bil = record + 1

                    Dim dr As DataRow
                    dr = ds.Tables(0).NewRow
                    'dr("BG15_Id") = 1
                    dr("BG16_NamaDok") = FileName
                    'dr("ROC09_Bil") = Bil
                    dr("BG16_Path") = Path
                    dr("BG16_ContentType") = ContentType
                    dr("BG16_JenisDok") = JenisLamp
                    dr("BG16_Status") = 1
                    dr("BG01_NoMohon") = IdSem
                    ds.Tables(0).Rows.Add(dr)
                    dbconn.sUpdateCommand(ds, rUpdate, False)

                Else
                    'insert 
                    Dim paramSqlBtrn() As SqlParameter = Nothing

                    Dim strSqlButiran = "INSERT INTO BG16_Lampiran ( BG01_NoMohon, BG16_NamaDok, BG16_Path, BG16_ContentType, BG16_JenisDok, BG16_Status)
                                            VALUES ( @noMohon, @namaDok, @path, @contentType, @jenisDok, @status )"
                    paramSqlBtrn = {
                        New SqlParameter("@noMohon", "1"),
                        New SqlParameter("@namaDok", FileName),
                        New SqlParameter("@path", Path),
                        New SqlParameter("@contentType", ContentType),
                        New SqlParameter("@jenisDok", JenisLamp),
                        New SqlParameter("@status", True)
                    }

                    If dbconn.fInsertCommand(strSqlButiran, paramSqlBtrn) > 0 Then
                        countButiran = countButiran + 1
                        'KodStatus = "01"
                        dbconn.sConnCommitTrans()
                    End If


                End If

            End Using

            If rUpdate Then
                'Session("GrantAccess") = 8
            End If
        Catch ex As Exception
        End Try

    End Sub

    Private Sub fLoadGVSalinanBajet()
        Dim str = $"select BG16_Id, BG01_NoMohon, BG16_NamaDok, BG16_Path from BG16_Lampiran 
            where BG01_NoMohon = '2' and BG16_Status = 1  and BG16_JenisDok = 'Minit'"

        Using dt = dbconn.fSelectCommandDt(str)

            If dt.Rows.Count > 0 Then
                gvLampiran.DataSource = dt
                gvLampiran.DataBind()
            Else
                gvLampiran.DataSource = New List(Of ListItem)
                gvLampiran.DataBind()
            End If
        End Using
    End Sub
    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        fUpload("1")
    End Sub

    Protected Sub gvLampiran_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvLampiran.SelectedIndexChanged
        Dim selectedRow As GridViewRow = gvLampiran.SelectedRow


        Dim strSQL_Delete = $"DELETE FROM BG16_Lampiran Where BG01_NoMohon = @no and BG16_Status = 1 and BG16_JenisDok = 'Minit'"
        Dim paramSql_delete() As SqlParameter =
             {
             New SqlParameter("@no", "2")
             }

        dbconn.fUpdateCommand(strSQL_Delete, paramSql_delete)

        fLoadGVSalinanBajet()


    End Sub


End Class

