Imports System.Data.SqlClient
Imports System.Drawing

Public Class Terimaan__Bendahari_
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                txtTahun.Text = Now.Year
                fBindDdlPTj()
                fBindViremen(ddlPTJ.SelectedValue)

                lblKodPTjPel2.Text = Session("ssusrKodPTj")
                lblNmPTjPel2.Text = Session("ssusrPTj")
                lblNoStafSah2.Text = Session("ssusrID")
                lblNmStafSah2.Text = Session("ssusrName")
                lblJawSah2.Text = Session("ssusrPost")
                lblTkhSah2.Text = Now.ToString("dd/MM/yyyy")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function fBindDdlPTj()

        Try

            Dim strSql As String = "SELECT DISTINCT TOP 100 PERCENT dbo.MK_PTJ.KodPTJ, (dbo.MK_PTJ.KodPTJ + ' - ' + dbo.MK_PTJ.Butiran) as Butiran , dbo.MK01_VotTahun.MK01_Tahun " &
            "FROM dbo.MK_PTJ INNER JOIN dbo.MK01_VotTahun ON dbo.MK_PTJ.KodPTJ = dbo.MK01_VotTahun.KodPTJ where MK01_Tahun = '" & Trim(txtTahun.Text.TrimEnd) & "' " &
            "ORDER BY dbo.MK01_VotTahun.MK01_Tahun, dbo.MK_PTJ.KodPTJ"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn

            ds = dbconn.fSelectCommand(strSql)

            ddlPTJ.DataSource = ds
            ddlPTJ.DataTextField = "Butiran"
            ddlPTJ.DataValueField = "KodPTJ"
            ddlPTJ.DataBind()

            ddlPTJ.Items.Insert(0, New ListItem("- KESELURUHAN -", "0"))
            ddlPTJ.SelectedIndex = 0

        Catch ex As Exception

        End Try

    End Function

    Private Sub fBindViremen(ByVal strKodPTj As String)
        Try
            Dim intRec As Integer = 0
            Dim strSql As String
            Dim strTahun As String = Trim(txtTahun.Text.Substring(2, txtTahun.Text.Length - 2))
            Dim strFilPTj As String

            Dim dt As New DataTable
            dt.Columns.Add("NoViremen", GetType(String))
            dt.Columns.Add("TkhMohon", GetType(String))
            dt.Columns.Add("NoStaf", GetType(String))
            dt.Columns.Add("KwF", GetType(String))
            dt.Columns.Add("KoF", GetType(String))
            dt.Columns.Add("PtjF", GetType(String))
            dt.Columns.Add("KpF", GetType(String))
            dt.Columns.Add("ObjSbgF", GetType(String))
            dt.Columns.Add("JumlahF", GetType(String))
            dt.Columns.Add("KwT", GetType(String))
            dt.Columns.Add("KoT", GetType(String))
            dt.Columns.Add("PtjT", GetType(String))
            dt.Columns.Add("KpT", GetType(String))
            dt.Columns.Add("ObjSbgT", GetType(String))
            dt.Columns.Add("JumlahT", GetType(String))

            If strKodPTj <> "0" Then
                strFilPTj = "and dbo.VBG_VirKeluar.KodPTJ='" & strKodPTj & "'"
            End If

            strSql = "SELECT DISTINCT dbo.BG10_Viremen.BG10_NoViremen,dbo.BG10_Viremen.BG10_TkhMohon,dbo.BG10_Viremen.BG10_NoStaf, dbo.BG10_Viremen.BG10_RujSurat, dbo.BG10_Viremen.KodStatusDok, 
dbo.VBG_VirKeluar.KodKw AS kodkwk,dbo.VBG_VirKeluar.kodko  AS kodkok, dbo.VBG_VirKeluar.KodPTJ AS kodptjk, dbo.VBG_VirKeluar.kodkp AS kodkpk, dbo.VBG_VirKeluar.KodVot AS kodvotk,  
dbo.VBG_VirKeluar.BG10_Amaun AS jumk, dbo.VBG_VirKeluar.KodVir AS kodvirk, dbo.VBG_VirMasuk.KodKw AS kodkwm, dbo.VBG_VirMasuk.KodKO  AS kodkom, dbo.VBG_VirMasuk.KodPTJ AS kodptjm, 
dbo.VBG_VirMasuk.KodKP  AS kodKpm, dbo.VBG_VirMasuk.KodVot AS kodvotm,  dbo.VBG_VirMasuk.BG10_Amaun AS jumm, dbo.VBG_VirMasuk.KodVir AS kodvirm, dbo.BG10_Viremen.BG10_StatusBen, 
dbo.BG10_Viremen.BG10_StatusKJ, dbo.VBG_VirKeluar.BG10_BakiSms AS bakismsk, MAX(dbo.VBG_VirKeluar.BG12_TkhProses) AS BG09_TkhProses, dbo.VBG_VirMasuk.BG10_BakiSms AS bakismsm, 
dbo.BG10_Viremen.BG10_RujSuratLulus, dbo.BG12_StatusDok.BG12_NoStaf as NoPelulus1, CONVERT(varchar,dbo.BG12_StatusDok.BG12_TkhProses,103) as TkhLulus 
FROM dbo.BG10_Viremen
INNER JOIN dbo.VBG_VirKeluar ON dbo.BG10_Viremen.BG10_NoViremen = dbo.VBG_VirKeluar.BG10_NoViremen
INNER JOIN dbo.VBG_VirMasuk ON dbo.BG10_Viremen.BG10_NoViremen = dbo.VBG_VirMasuk.BG10_NoViremen
INNER JOIN  dbo.BG12_StatusDok ON dbo.BG12_StatusDok.BG10_NoViremen = dbo.BG10_Viremen.BG10_NoViremen
WHERE dbo.BG10_Viremen.BG10_Tahun = '" & Trim(txtTahun.Text.TrimEnd) & "' and dbo.BG10_Viremen.BG10_StatusKJ = '1' and dbo.BG10_Viremen.BG10_StatusBen='0' and dbo.BG10_Viremen.KodStatusDok in ('03') " & strFilPTj &
"GROUP BY dbo.BG10_Viremen.BG10_NoViremen,dbo.BG10_Viremen.BG10_TkhMohon,dbo.BG10_Viremen.BG10_NoStaf, dbo.BG10_Viremen.BG10_RujSurat, dbo.BG10_Viremen.KodStatusDok, dbo.VBG_VirKeluar.KodKw,dbo.VBG_VirKeluar.kodko, 
dbo.VBG_VirKeluar.KodPTJ, dbo.VBG_VirKeluar.kodkp, dbo.VBG_VirKeluar.KodVot,  dbo.VBG_VirKeluar.BG10_Amaun, dbo.VBG_VirKeluar.KodVir, dbo.VBG_VirMasuk.KodKw,dbo.VBG_VirMasuk.KodKO, dbo.VBG_VirMasuk.KodPTJ,dbo.VBG_VirMasuk.KodKP, 
dbo.VBG_VirMasuk.KodVot, dbo.VBG_VirMasuk.BG10_Amaun, dbo.VBG_VirMasuk.KodVir, dbo.VBG_VirKeluar.BG10_BakiSms, dbo.VBG_VirMasuk.BG10_BakiSms, dbo.BG10_Viremen.BG10_RujSuratLulus, dbo.BG10_Viremen.BG10_StatusBen, 
dbo.BG10_Viremen.BG10_StatusKJ,dbo.BG12_StatusDok.BG12_NoStaf, dbo.BG12_StatusDok.BG12_TkhProses 
ORDER BY dbo.BG10_Viremen.BG10_NoViremen"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            Dim dvViremen As New DataView
            dvViremen = New DataView(ds.Tables(0))
            ViewState("dvViremen") = dvViremen.Table

            Dim strNoViremen, strTkhMohon, strNoStaf As String
            Dim strKWK, strKOK, strPTjK, strKPK, strObjSbgK, strAmaunK As String
            Dim strKWM, strKOM, strPTjM, strKPM, strObjSbgM, strAmaunM As String
            Dim decAmaunK, decAmaunM As Decimal
            Dim dtTkhMohon As Date

            If Not ds Is Nothing Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    strNoViremen = ds.Tables(0).Rows(i)("BG10_NoViremen")
                    dtTkhMohon = CDate(ds.Tables(0).Rows(i)("BG10_TkhMohon"))
                    strTkhMohon = dtTkhMohon.ToString("dd/MM/yyyy")

                    strNoStaf = ds.Tables(0).Rows(i)("BG10_NoStaf")
                    strKWK = ds.Tables(0).Rows(i)("kodkwk")
                    strKOK = ds.Tables(0).Rows(i)("kodkok")
                    strPTjK = ds.Tables(0).Rows(i)("kodptjk")
                    strKPK = ds.Tables(0).Rows(i)("kodkpk")
                    strObjSbgK = ds.Tables(0).Rows(i)("kodvotk")
                    decAmaunK = CDec(ds.Tables(0).Rows(i)("jumk"))
                    strAmaunK = FormatNumber(decAmaunK, 2)

                    strKWM = ds.Tables(0).Rows(i)("kodkwm")
                    strKOM = ds.Tables(0).Rows(i)("kodkom")
                    strPTjM = ds.Tables(0).Rows(i)("kodptjm")
                    strKPM = ds.Tables(0).Rows(i)("kodkpm")
                    strObjSbgM = ds.Tables(0).Rows(i)("kodvotm")
                    decAmaunM = CDec(ds.Tables(0).Rows(i)("jumm"))
                    strAmaunM = FormatNumber(decAmaunM, 2)

                    dt.Rows.Add(strNoViremen, strTkhMohon, strNoStaf, strKWK, strKOK, strPTjK, strKPK, strObjSbgK, strAmaunK, strKWM, strKOM, strPTjM, strKPM, strObjSbgM, strAmaunM)
                Next

                gvViremen.DataSource = dt
                gvViremen.DataBind()

                intRec = ds.Tables(0).Rows.Count

            End If

            lblJumRekod.InnerText = intRec

            gvViremen.DataSource = dt
            gvViremen.DataBind()

        Catch ex As Exception

        End Try

    End Sub

    Private Sub gvViremen_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvViremen.RowCreated

        Try
            If e.Row.RowType = DataControlRowType.Header Then

                Dim row As GridViewRow = New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal)

                Dim cell As TableCell = New TableCell()
                cell.ColumnSpan = 4
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.BackColor = Color.White
                row.Cells.Add(cell)

                cell = New TableCell()
                cell.ColumnSpan = 6
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.Text = "Viremen Keluar"
                row.Cells.Add(cell)

                cell = New TableCell()
                cell.ColumnSpan = 6
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.Text = "Viremen Masuk"
                row.Cells.Add(cell)

                cell = New TableCell()
                cell.ColumnSpan = 1
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.BackColor = Color.White
                row.Cells.Add(cell)

                gvViremen.Controls(0).Controls.AddAt(0, row)

                e.Row.BackColor = ColorTranslator.FromHtml("#FECB18")

            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Sub gvViremen_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvViremen.RowCommand
        Try
            If e.CommandName = "Select" Then
                fReset()
                Dim strButiranKW, strButiranKO, strButiranPTj, strButiranKP As String

                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvViremen.Rows(index)

                Dim strNoViremen As String = selectedRow.Cells(1).Text

                Dim dvViremen As New DataView
                dvViremen = New DataView(ViewState("dvViremen"))
                dvViremen.RowFilter = "BG10_NoViremen = '" & strNoViremen & "'"

                txtNoViremen.Text = strNoViremen

                Dim intTahun As Integer = CInt(Trim(txtTahun.Text.TrimEnd))
                Dim strTarikh As String = Now.ToString("yyyy-MM-dd")
                Dim dtTkhMohon As Date = CDate(dvViremen.Item(0)("BG10_TkhMohon").ToString)
                Dim strTkhMohon As String = dtTkhMohon.ToString("dd/MM/yyyy")
                Dim strNoRujSurat As String = dvViremen.Item(0)("BG10_RujSurat").ToString

                Dim strKodKWF As String = dvViremen.Item(0)("kodkwk").ToString
                Dim strkodKOF As String = dvViremen.Item(0)("kodkok").ToString
                Dim strKodPTjF As String = dvViremen.Item(0)("kodptjk").ToString
                Dim strKodKPF As String = dvViremen.Item(0)("kodkpk").ToString
                Dim strKodSbgF As String = dvViremen.Item(0)("kodvotk").ToString
                Dim decAmaunF As Decimal = CDec(dvViremen.Item(0)("jumk").ToString)
                Dim strAmaunF As String = FormatNumber(decAmaunF, 2)
                Dim decBakiSmsF As Decimal
                'Dim strBakiSmsF As String = dvViremen.Item(0)("bakismsk").ToString

                Dim strKodKWT As String = dvViremen.Item(0)("kodkwm").ToString
                Dim strkodKOT As String = dvViremen.Item(0)("kodkom").ToString
                Dim strKodPTjT As String = dvViremen.Item(0)("kodptjm").ToString
                Dim strKodKPT As String = dvViremen.Item(0)("kodkpm").ToString
                Dim strKodSbgT As String = dvViremen.Item(0)("kodvotm").ToString
                Dim decAmaunT As Decimal = CDec(dvViremen.Item(0)("jumm").ToString)
                Dim strAmaunT As String = FormatNumber(decAmaunT, 2)
                Dim decBakiSmsT As Decimal
                'Dim strBakiSmsT As String = dvViremen.Item(0)("bakismsm").ToString

                txtNoRujSurat.Text = strNoRujSurat
                lblTkhMohon.Text = strTkhMohon

                Dim strNoStafPmhn As String = dvViremen.Item(0)("BG10_NoStaf").ToString
                Dim dt As New DataTable
                dt = fGetUserInfo(strNoStafPmhn)
                If dt.Rows.Count > 0 Then
                    lblNoStafPem.Text = strNoStafPmhn
                    lblNmPem.Text = dt.Rows.Item(0).Item("MS01_Nama")
                    lblJawPem.Text = dt.Rows.Item(0).Item("JawGiliran")
                    lblKodPTjPem.Text = dt.Rows.Item(0).Item("KodPejabat").ToString.PadRight(6, "0"c)
                    lblNmPTjPem.Text = dt.Rows.Item(0).Item("Pejabat")
                End If

                Dim strNoPelulus1 As String = dvViremen.Item(0)("NoPelulus1").ToString
                Dim dt2 As New DataTable
                dt2 = fGetUserInfo(strNoPelulus1)
                If dt2.Rows.Count > 0 Then
                    lblNoStafPel1.Text = strNoPelulus1
                    lblNmStafPel1.Text = dt2.Rows.Item(0).Item("MS01_Nama")
                    lblJawPel1.Text = dt2.Rows.Item(0).Item("JawGiliran")
                    lblKodPTjPel1.Text = dt2.Rows.Item(0).Item("KodPejabat").ToString.PadRight(6, "0"c)
                    lblNmPTjPel1.Text = dt2.Rows.Item(0).Item("Pejabat")
                    lblTkhLulus1.Text = dvViremen.Item(0)("TkhLulus").ToString
                End If

                txtKodKWF.Text = strKodKWF
                fFindKW(strKodKWF, strButiranKW)
                txtKwF.Text = strButiranKW
                txtKodKoF.Text = strkodKOF
                fFindKO(strkodKOF, strButiranKO)
                txtKoF.Text = strButiranKO
                txtKodPTjF.Text = strKodPTjF
                fFindPTj(strKodPTjF, strButiranPTj)
                txtPTjF.Text = strButiranPTj
                txtKodKPF.Text = strKodKPF
                fFindKP(strKodKPF, strButiranKP)
                txtKPF.Text = strButiranKP
                txtKodSbgF.Text = strKodSbgF

                txtObjSbgF.Text = fFindVot(strKodSbgF)
                txtAmaunF.Text = strAmaunF
                'txtBakiF.Text = strBakiSmsF

                decBakiSmsF = CDec(fGetBakiSebenar(intTahun, strTarikh, strKodKWF, strkodKOF, strKodPTjF, strKodKPF, strKodSbgF))
                txtBakiF.Text = FormatNumber(decBakiSmsF, 2)


                txtKodKWT.Text = strKodKWT
                fFindKW(strKodKWT, strButiranKW)
                txtKWT.Text = strButiranKW
                txtKodKoT.Text = strkodKOT
                fFindKO(strkodKOT, strButiranKO)
                txtKoT.Text = strButiranKO
                txtKodPTjT.Text = strKodPTjT
                fFindPTj(strKodPTjT, strButiranPTj)
                txtPTjT.Text = strButiranPTj
                txtKodKpT.Text = strKodKPT
                fFindKP(strKodKPT, strButiranKP)
                txtKpT.Text = strButiranKP
                txtKodSbgT.Text = strKodSbgT

                txtObjSbgT.Text = fFindVot(strKodSbgT)
                txtAmaunT.Text = strAmaunT
                decBakiSmsT = CDec(fGetBakiSebenar(intTahun, strTarikh, strKodKWT, strkodKOT, strKodPTjT, strKodKPT, strKodSbgT))
                txtBakiT.Text = FormatNumber(decBakiSmsT, 2)

                divLst.Visible = False
                divDt.Visible = True


            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Function fReset()
        Try
            txtNoViremen.Text = ""
            txtKodKWF.Text = ""
            txtKwF.Text = ""
            txtKodKoF.Text = ""
            txtKoF.Text = ""
            txtKodPTjF.Text = ""
            txtPTjF.Text = ""
            txtKodKPF.Text = ""
            txtKPF.Text = ""
            txtKodSbgF.Text = ""
            txtObjSbgF.Text = ""
            txtAmaunF.Text = ""
            txtBakiF.Text = ""
            txtKodKWT.Text = ""
            txtKWT.Text = ""
            txtKodKoT.Text = ""
            txtKoT.Text = ""
            txtKodPTjT.Text = ""
            txtPTjT.Text = ""
            txtKodKpT.Text = ""
            txtKpT.Text = ""
            txtKodSbgT.Text = ""
            txtObjSbgT.Text = ""
            txtAmaunT.Text = ""
            txtBakiT.Text = ""
            txtUlasan.Text = ""

            lblNoStafPem.Text = ""
            lblNmPem.Text = ""
            lblJawPem.Text = ""
            lblKodPTjPem.Text = ""
            lblNmPTjPem.Text = ""
            lblTkhMohon.Text = ""

        Catch ex As Exception

        End Try
    End Function

    Private Function fLulusV() As Boolean
        Dim dbconn As New DBKewConn
        Dim ds As New DataSet
        Dim trackno As Integer
        Dim blnSuccess As Boolean = True
        Dim strStatDok As String = "14"
        Try
            trackno = 1
            Dim strSql As String
            Dim strNoViremen As String = Trim(txtNoViremen.Text.TrimEnd)

            Dim strNoStaf As String = Trim(lblNoStafSah2.Text.TrimEnd)
            Dim strTkhToday As String = Now.ToString("yyyy-MM-dd")
            Dim strUlasan As String = IIf(Trim(txtUlasan.Text.TrimEnd) = "", "-", Trim(txtUlasan.Text.TrimEnd))

            Dim strNoRujLulus As String = Trim(txtNoRujLulus.Text.TrimEnd)
            If strNoRujLulus = "" Then
                strNoRujLulus = "-"
            End If

            trackno = 2
            dbconn.sConnBeginTrans()
            strSql = "UPDATE BG10_Viremen  SET BG10_RujSuratLulus = @NoRujLulus, KodStatusDok = @KodStatusDok " &
                            "WHERE BG10_NoViremen  = @NoViremen"
            Dim paramSql() As SqlParameter = {
                New SqlParameter("@KodStatusDok", strStatDok),
                New SqlParameter("@NoViremen", strNoViremen),
                New SqlParameter("@NoRujLulus", strNoRujLulus)
                }

            If Not dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                blnSuccess = False
                Exit Try
            End If

            strSql = "INSERT INTO BG12_StatusDok(bg10_noviremen,kodstatusdok,bg12_tkhproses,bg12_nostaf,bg12_ulasan) " &
        "values (@NoViremen, @KodStatusDok, @TkhProses, @NoStaf, @Ulasan)"

            Dim paramSql3() As SqlParameter = {
                New SqlParameter("@NoViremen", strNoViremen),
                New SqlParameter("@KodStatusDok", strStatDok),
                New SqlParameter("@TkhProses", strTkhToday),
                New SqlParameter("@NoStaf", strNoStaf),
                New SqlParameter("@Ulasan", strUlasan)
            }

            If Not dbconn.fInsertCommand(strSql, paramSql3) > 0 Then
                blnSuccess = False
                Exit Try
            End If

        Catch ex As Exception
            blnSuccess = False
        End Try

        If blnSuccess Then
            dbconn.sConnCommitTrans()
            Return True
        Else
            dbconn.sConnRollbackTrans()
            Return False
        End If


    End Function



    Private Sub ddlPTJ_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPTJ.SelectedIndexChanged
        fBindViremen(ddlPTJ.SelectedValue)
    End Sub

    Private Function fPulang() As Boolean
        Dim dbConn As New DBKewConn
        Try
            Dim strSql As String
            Dim strNoViremen As String = Trim(txtNoViremen.Text.TrimEnd)
            Dim strStatusDok As String = "16"
            Dim strTkhToday As String = Now.ToString("yyyy-MM-dd")
            Dim strNoStaf As String = Session("ssusrID")
            Dim strUlasan As String = Trim(txtUlasan.Text.TrimEnd)
            Dim dtTkhLulusNC As Date = CDate(Trim(lblTkhSah2.Text.TrimEnd))

            dbConn.sConnBeginTrans()
            'KEMAS KINI TABLE BG10_VIREMEN
            strSql = "UPDATE BG10_Viremen SET kodstatusdok=@KodStatusDok " &
            "WHERE bg10_noviremen = @NoViremen"
            Dim paramSql() As SqlParameter = {
                New SqlParameter("@KodStatusDok", strStatusDok),
                New SqlParameter("@NoViremen", strNoViremen)
                }

            If Not dbConn.fUpdateCommand(strSql, paramSql) > 0 Then
                dbConn.sConnRollbackTrans()
                Return False
            End If

            strSql = "INSERT INTO BG12_StatusDok(bg10_noviremen,kodstatusdok,bg12_tkhproses,bg12_nostaf,bg12_ulasan)" &
        "values (@NoViremen, @KodStatusDok, @TkhProses, @NoStaf, @Ulasan)"

            Dim paramSql3() As SqlParameter = {
                    New SqlParameter("@NoViremen", strNoViremen),
                    New SqlParameter("@KodStatusDok", strStatusDok),
                    New SqlParameter("@TkhProses", strTkhToday),
                    New SqlParameter("@NoStaf", strNoStaf),
                    New SqlParameter("@Ulasan", strUlasan)
                }

            dbConn.sConnBeginTrans()
                If Not dbConn.fInsertCommand(strSql, paramSql3) > 0 Then
                    dbConn.sConnRollbackTrans()
                    Return False
                End If


            dbConn.sConnCommitTrans()
            Return True

        Catch ex As Exception
            dbConn.sConnRollbackTrans()
            Return False
        End Try
    End Function

    Private Sub lbtnLulus_Click(sender As Object, e As EventArgs) Handles lbtnLulus.Click

        If fLulusV() = True Then
            fGlobalAlert("Permohonan viremen telah disahkan!", Me.Page, Me.[GetType]())
            fBindViremen(ddlPTJ.SelectedValue)
            fReset()
            divLst.Visible = True
            divDt.Visible = False
        Else
            fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
        End If

    End Sub

    Private Sub lbtnKembali_Click(sender As Object, e As EventArgs) Handles lbtnKembali.Click
        divDt.Visible = False
        divLst.Visible = True
    End Sub

    Private Sub lBtnPulang_Click(sender As Object, e As EventArgs) Handles lBtnPulang.Click
        If fPulang() = True Then
            fGlobalAlert("Maklumat permohonan viremen telah dipulangkan kepada pemohon!", Me.Page, Me.[GetType]())
            fBindViremen(ddlPTJ.SelectedValue)
            fReset()
            divLst.Visible = True
            divDt.Visible = False
        Else
            fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
        End If
    End Sub
End Class