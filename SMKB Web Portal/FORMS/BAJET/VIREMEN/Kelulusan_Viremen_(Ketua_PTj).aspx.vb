Imports System.Drawing
Imports System.Data.SqlClient

Public Class Kelulusan_Ketua_PTj_Dt
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                lblKodPTjPel.Text = Session("ssusrKodPTj")
                lblNmPTjPel.Text = Session("ssusrPTj")
                lblNoStafPel.Text = Session("ssusrID")
                lblNmStafPel.Text = Session("ssusrName")
                lblJawPel.Text = Session("ssusrPost")
                lblTkhLulus.Text = Now.ToString("dd/MM/yyyy")

                fBindViremen()
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub fBindViremen()
        Try
            Dim intRec As Integer = 0
            Dim strSql As String

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

            strSql = "SELECT DISTINCT dbo.BG10_Viremen.BG10_NoViremen,convert(varchar,dbo.BG10_Viremen.BG10_TkhMohon,103) as BG10_TkhMohon,dbo.BG10_Viremen.BG10_NoStaf, dbo.BG10_Viremen.BG10_RujSurat, dbo.BG10_Viremen.KodStatusDok, dbo.VBG_VirKeluar.KodKw AS kodkwk, " &
            "dbo.VBG_VirKeluar.kodko  AS kodkok,dbo.VBG_VirKeluar.KodPTJ AS kodptjk,dbo.VBG_VirKeluar.kodkp  AS kodkpk, dbo.VBG_VirKeluar.KodVot AS kodvotk,  dbo.VBG_VirKeluar.BG10_Amaun AS jumk, dbo.VBG_VirKeluar.KodVir AS kodvirk, " &
            "dbo.VBG_VirMasuk.KodKw AS kodkwm,  dbo.VBG_VirMasuk.KodKO AS kodkom,dbo.VBG_VirMasuk.KodPTJ AS kodptjm,  dbo.VBG_VirMasuk.KodKP AS kodkpm, dbo.VBG_VirMasuk.KodVot AS kodvotm,  dbo.VBG_VirMasuk.BG10_Amaun AS jumm, " &
            "dbo.VBG_VirMasuk.KodVir AS kodvirm, dbo.BG10_Viremen.BG10_StatusBen, dbo.BG10_Viremen.BG10_StatusKJ, dbo.VBG_VirKeluar.BG10_BakiSms AS bakismsk, MAX(dbo.VBG_VirKeluar.BG12_TkhProses) AS BG09_TkhProses, dbo.VBG_VirMasuk.BG10_BakiSms AS bakismsm, " &
            "dbo.BG10_Viremen.BG10_RujSuratLulus FROM dbo.BG10_Viremen INNER JOIN dbo.VBG_VirKeluar ON dbo.BG10_Viremen.BG10_NoViremen = dbo.VBG_VirKeluar.BG10_NoViremen INNER JOIN dbo.VBG_VirMasuk ON dbo.BG10_Viremen.BG10_NoViremen = dbo.VBG_VirMasuk.BG10_NoViremen " &
            "where dbo.BG10_Viremen.BG10_Tahun = '" & Now.Year & "' and dbo.VBG_VirKeluar.KodPTJ='" & Trim(lblKodPTjPel.Text.TrimEnd) & "' And dbo.BG10_Viremen.KodStatusDok in ('02','12') " &
            "GROUP BY dbo.BG10_Viremen.BG10_NoViremen, dbo.BG10_Viremen.BG10_TkhMohon,dbo.BG10_Viremen.BG10_NoStaf, dbo.BG10_Viremen.BG10_RujSurat, dbo.BG10_Viremen.KodStatusDok, dbo.VBG_VirKeluar.KodKw, dbo.VBG_VirKeluar.kodko, dbo.VBG_VirKeluar.KodPTJ, " &
            "dbo.VBG_VirKeluar.kodkp, dbo.VBG_VirKeluar.KodVot,  dbo.VBG_VirKeluar.BG10_Amaun, dbo.VBG_VirKeluar.KodVir, dbo.VBG_VirMasuk.KodKw, dbo.VBG_VirMasuk.KodKO, dbo.VBG_VirMasuk.KodPTJ, dbo.VBG_VirMasuk.KodKP, dbo.VBG_VirMasuk.KodVot, " &
            "dbo.VBG_VirMasuk.BG10_Amaun, dbo.VBG_VirMasuk.KodVir, dbo.VBG_VirKeluar.BG10_BakiSms, dbo.VBG_VirMasuk.BG10_BakiSms, dbo.BG10_Viremen.BG10_RujSuratLulus, dbo.BG10_Viremen.BG10_StatusBen , dbo.BG10_Viremen.BG10_StatusKJ"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            Dim dvViremen As New DataView
            dvViremen = New DataView(ds.Tables(0))
            ViewState("dvViremen") = dvViremen.Table

            Dim strNoViremen, strTkhMohon, strNoStaf As String
            Dim strKWK, strKOK, strPTjK, strKPK, strObjSbgK, strAmaunK As String
            Dim strKWM, strKOM, strPTjM, strKPM, strObjSbgM, strAmaunM As String
            Dim decAmaunK, decAmaunM As Decimal
            'Dim dtTkkMohon As Date
            If Not ds Is Nothing Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    strNoViremen = ds.Tables(0).Rows(i)("BG10_NoViremen")
                    'dtTkkMohon = CDate(ds.Tables(0).Rows(i)("BG10_TkhMohon"))
                    strTkhMohon = ds.Tables(0).Rows(i)("BG10_TkhMohon")
                    strNoStaf = ds.Tables(0).Rows(i)("BG10_NoStaf")
                    strKWK = ds.Tables(0).Rows(i)("kodkwk")
                    strKOK = ds.Tables(0).Rows(i)("kodkok")
                    strPTjK = ds.Tables(0).Rows(i)("kodptjk")
                    strKPK = ds.Tables(0).Rows(i)("kodkpk")
                    strObjSbgK = ds.Tables(0).Rows(i)("kodvotk")
                    decAmaunK = CDec(ds.Tables(0).Rows(i)("jumk"))
                    strAmaunK = decAmaunK.ToString("#,##0.00")

                    strKWM = ds.Tables(0).Rows(i)("kodkwm")
                    strKOM = ds.Tables(0).Rows(i)("kodkom")
                    strPTjM = ds.Tables(0).Rows(i)("kodptjm")
                    strKPM = ds.Tables(0).Rows(i)("kodkpm")
                    strObjSbgM = ds.Tables(0).Rows(i)("kodvotm")
                    decAmaunM = CDec(ds.Tables(0).Rows(i)("jumm"))
                    strAmaunM = decAmaunM.ToString("#,##0.00")

                    dt.Rows.Add(strNoViremen, strTkhMohon, strNoStaf, strKWK, strKOK, strPTjK, strKPK, strObjSbgK, strAmaunK, strKWM, strKOM, strPTjM, strKPM, strObjSbgM, strAmaunM)
                Next

                gvViremen.DataSource = dt
                gvViremen.DataBind()
                ViewState("dtList") = dt
                intRec = ds.Tables(0).Rows.Count

            End If

            lblJumRekod.InnerText = intRec

            gvViremen.DataSource = dt
            gvViremen.DataBind()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub gvViremen_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvViremen.RowCommand
        Try
            If e.CommandName = "Select" Then
                System.Threading.Thread.Sleep(1000)
                Dim strButiranKW, strButiranKO, strButiranPTj, strButiranKP, strButiranVot As String

                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvViremen.Rows(index)

                Dim strNoViremen As String = selectedRow.Cells(1).Text

                Dim dvViremen As New DataView
                dvViremen = New DataView(ViewState("dvViremen"))
                dvViremen.RowFilter = "BG10_NoViremen = '" & strNoViremen & "'"



                Dim intTahun As Integer = Now.Year
                Dim strTarikh As String = Now.ToString("yyyy-MM-dd")
                Dim strTkhMohon As String = dvViremen.Item(0)("BG10_TkhMohon").ToString
                Dim strNoRujSurat As String = dvViremen.Item(0)("BG10_RujSurat").ToString

                Dim strKodKWF As String = dvViremen.Item(0)("kodkwk").ToString
                Dim strkodKOF As String = dvViremen.Item(0)("kodkok").ToString
                Dim strKodPTjF As String = dvViremen.Item(0)("kodptjk").ToString
                Dim strKodKPF As String = dvViremen.Item(0)("kodkpk").ToString
                Dim strKodSbgF As String = dvViremen.Item(0)("kodvotk").ToString

                Dim decAmaunF As Decimal = CDec(dvViremen.Item(0)("jumk").ToString)
                Dim strAmaunF As String = decAmaunF.ToString("#,##0.00")

                'Dim strBakiSmsF As String = dvViremen.Item(0)("bakismsk").ToString

                Dim strKodKWT As String = dvViremen.Item(0)("kodkwm").ToString
                Dim strkodKOT As String = dvViremen.Item(0)("kodkom").ToString
                Dim strKodPTjT As String = dvViremen.Item(0)("kodptjm").ToString
                Dim strKodKPT As String = dvViremen.Item(0)("kodkpm").ToString
                Dim strKodSbgT As String = dvViremen.Item(0)("kodvotm").ToString

                Dim decAmaunT As Decimal = CDec(dvViremen.Item(0)("jumm").ToString)
                Dim strAmaunT As String = decAmaunT.ToString("#,##0.00")
                ' Dim strBakiSmsT As String = dvViremen.Item(0)("bakismsm").ToString

                Dim strNamaPmhn, strJawPmhn, strKodPTjPmhn, strPTjPmhn As String

                lblTkhMohon.Text = strTkhMohon

                Dim strNoStafPmhn As String = dvViremen.Item(0)("BG10_NoStaf").ToString
                Dim dt As New DataTable
                dt = fGetUserInfo(strNoStafPmhn)
                If dt.Rows.Count > 0 Then
                    strNamaPmhn = dt.Rows.Item(0).Item("MS01_Nama")
                    strJawPmhn = dt.Rows.Item(0).Item("JawGiliran")
                    strKodPTjPmhn = dt.Rows.Item(0).Item("KodPejabat") & "0000"
                    strPTjPmhn = dt.Rows.Item(0).Item("Pejabat")
                End If

                txtNoViremen.Text = strNoViremen
                txtNoRujSurat.Text = strNoRujSurat

                lblNoStafPem.Text = strNoStafPmhn
                lblNmPem.Text = strNamaPmhn
                lblJawPem.Text = strJawPmhn
                lblKodPTjPem.Text = strKodPTjPmhn
                lblNmPTjPem.Text = strPTjPmhn



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
                Dim decBakiF As Decimal = CDec(fGetBakiSebenar(intTahun, strTarikh, strKodKWF, strkodKOF, strKodPTjF, strKodKPF, strKodSbgF))
                txtBakiF.Text = decBakiF.ToString("#,##0.00")

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
                'txtBakiT.Text = strBakiSmsT
                Dim decBakiT As Decimal = CDec(fGetBakiSebenar(intTahun, strTarikh, strKodKWT, strkodKOT, strKodPTjT, strKodKPT, strKodSbgT))
                txtBakiT.Text = decBakiT.ToString("#,##0.00")

                divlst.Visible = False
                divDt.Visible = True

            End If

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

    Private Function fGetKodJen(ByVal strKodVot As String, ByRef strKodJen As String, ByRef strKodjenlanjut As String) As String
        Try
            Dim strSql As String
            Dim dbconn As New DBKewConn

            strSql = "select kodjen,kodjenlanjut from mk_vot where kodvot='" & strKodVot & "'"
            Dim ds As New DataSet
            ds = dbconn.fselectCommand(strSql)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strKodJen = ds.Tables(0).Rows(0)("kodjen").ToString
                    strKodjenlanjut = ds.Tables(0).Rows(0)("kodjenlanjut").ToString
                End If
            End If
        Catch ex As Exception

        End Try
    End Function



    Private Function fReject() As Boolean
        Dim strSql As String
        Dim strNoViremen As String = Trim(txtNoViremen.Text.TrimEnd)
        Dim strStatusDok As String = "06"
        Dim strTkhProses As String = Now.ToString("yyyy-MM-dd")
        Dim strNoStaf As String = Session("ssusrID")
        Dim strUlasan As String = Trim(txtUlasan.Text.TrimEnd)

        Dim dbConn As New DBKewConn

        Try

            dbConn.sConnBeginTrans()
            strSql = "UPDATE BG10_Viremen SET bg10_statuskj=@StatusKj,bg10_statusben=@Statusben,kodstatusdok=@KodStatusDok " &
            "WHERE bg10_noviremen = @NoViremen"
            Dim paramSql() As SqlParameter = {
                New SqlParameter("@StatusKj", 0),
                New SqlParameter("@StatusBen", 0),
                New SqlParameter("@KodStatusDok", "06"),
                New SqlParameter("@NoViremen", strNoViremen)
                }

            If Not dbConn.fUpdateCommand(strSql, paramSql) > 0 Then
                dbConn.sConnRollbackTrans()
                Return False
            End If

            strSql = "select count(*) from BG12_StatusDok where BG10_NoViremen ='" & strNoViremen & "' and kodstatusdok = @KodStatusDok"
            If fCheckRec(strSql) > 0 Then
                strSql = "DELETE from BG12_StatusDok  WHERE bg10_noviremen=@NoViremen and kodstatusdok= @KodStatusDok"
                Dim paramSql2() As SqlParameter = {
                    New SqlParameter("@NoViremen", strNoViremen),
                    New SqlParameter("@KodStatusDok", strStatusDok)
                    }

                If Not dbConn.fUpdateCommand(strSql, paramSql2) > 0 Then
                    dbConn.sConnRollbackTrans()
                    Return False
                End If
            End If

            strSql = "INSERT INTO BG12_StatusDok(bg10_noviremen,kodstatusdok,bg12_tkhproses,bg12_nostaf,bg12_ulasan) " &
                            "values (@NoViremen, @KodStatusDok, @TkhProses, @NoStaf, @Ulasan)"

            Dim paramSql3() As SqlParameter = {
                    New SqlParameter("@NoViremen", strNoViremen),
                    New SqlParameter("@KodStatusDok", strStatusDok),
                    New SqlParameter("@TkhProses", strTkhProses),
                    New SqlParameter("@NoStaf", strNoStaf),
                    New SqlParameter("@Ulasan", strUlasan)
                }

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

    Private Function fLulusV() As Boolean
        Try
            Dim dbconn As New DBKewConn
            Dim strNoViremen As String = Trim(txtNoViremen.Text.TrimEnd)
            Dim strKWK As String = Trim(txtKodKWF.Text.TrimEnd)
            Dim strPTjK As String = Trim(txtKodPTjF.Text.TrimEnd)
            Dim strObjSbgK As String = Trim(txtKodSbgF.Text.TrimEnd)
            Dim blnSuccess As Boolean

            If strKWK = "03" Then  'KW03 = KUMPULAN WANG PENYELIDIKAN

                fLulus1(strNoViremen)

            Else 'LAIN2 KW
                If strPTjK.Substring(0, 2) = "51" OrElse strPTjK.Substring(0, 2) = "52" OrElse strPTjK.Substring(0, 2) = "54" OrElse strPTjK.Substring(0, 2) = "55" OrElse strPTjK.Substring(0, 2) = "57" Then
                    blnSuccess = fLulus2(strNoViremen, "03")
                ElseIf strPTjK = "500000" Then
                    blnSuccess = fLulus2(strNoViremen, "09")
                ElseIf strPTjK = Trim(txtKodPTjT.Text.TrimEnd) AndAlso strObjSbgK.Substring(0, 1) = Trim(txtKodSbgT.Text.TrimEnd).Substring(0, 1) Then
                    'PTJ SAMA & VOT AM SAMA
                    blnSuccess = fLulus1(strNoViremen)
                Else
                    blnSuccess = fLulus2(strNoViremen, "03")
                End If
            End If

            Return blnSuccess
        Catch ex As Exception

        End Try
    End Function

    Private Function fLulus1(ByVal strNoViremen As String) As Boolean
        Dim strSql As String
        Dim strStatusKJ As Int16 = 1
        Dim strStatusBen As Int16 = 1

        Dim strTkhToday As String = Now.ToString("yyyy-MM-dd")
        Dim strNoStaf As String = Session("ssusrID")
        Dim strUlasan As String = IIf(Trim(txtUlasan.Text.TrimEnd) = "", "-", (Trim(txtUlasan.Text.TrimEnd)))
        Dim decAmaunK As Decimal = CDec(Trim(txtAmaunF.Text.TrimEnd))
        Dim decAmaunM As Decimal = CDec(Trim(txtAmaunT.Text.TrimEnd))
        Dim intBulan As Integer = Now.Month
        Dim intTahun As Integer = Now.Year
        Dim strKodJen, strKodjenlanjut As String

        Dim strKWK As String = Trim(txtKodKWF.Text.TrimEnd)
        Dim strPTjK As String = Trim(txtKodPTjF.Text.TrimEnd)
        Dim strObjSbgK As String = Trim(txtKodSbgF.Text.TrimEnd)


        Dim strKodKWF As String = Trim(txtKodKWF.Text.TrimEnd)
        Dim strKodKOF As String = Trim(txtKodKWF.Text.TrimEnd)
        Dim strKodPTjF As String = Trim(txtKodPTjF.Text.TrimEnd)
        Dim strKodKPF As String = Trim(txtKodKPF.Text.TrimEnd)
        Dim strObjSbgF As String = Trim(txtKodSbgF.Text.TrimEnd)
        Dim strObjAmF As String = Trim(txtKodSbgF.Text.TrimEnd).Substring(0, 1) & "0000"

        Dim strKodKWT As String = Trim(txtKodKWT.Text.TrimEnd)
        Dim strKodKOT As String = Trim(txtKodKoT.Text.TrimEnd)
        Dim strKodPTjT As String = Trim(txtKodPTjT.Text.TrimEnd)
        Dim strKodKPT As String = Trim(txtKodKpT.Text.TrimEnd)
        Dim strObjSbgT As String = Trim(txtKodSbgT.Text.TrimEnd)
        Dim strObjAmT As String = Trim(txtKodSbgT.Text.TrimEnd).Substring(0, 1) & "0000"


        Dim dsKs As New DataSet
        Dim dsKa As New DataSet
        Dim dsMs As New DataSet
        Dim dsMa As New DataSet
        Dim dbConn As New DBKewConn
        Try
            dbConn.sConnBeginTrans()
            strSql = "UPDATE BG10_Viremen  SET BG10_StatusKJ=@StatusKJ,BG10_StatusBen=@StatusBen, kodstatusdok=@StatusDok " &
                                "WHERE BG10_NoViremen  = @NoViremen"
            Dim paramSql1() As SqlParameter = {
                New SqlParameter("@StatusKJ", 1),
                New SqlParameter("@StatusBen", 1),
                New SqlParameter("@StatusDok", "05"),
                New SqlParameter("@NoViremen", strNoViremen)
                }

            If Not dbConn.fUpdateCommand(strSql, paramSql1) > 0 Then
                dbConn.sConnRollbackTrans()
                Return False
            End If

            strSql = "Select count(*) from BG12_StatusDok WITH (NOLOCK) WHERE bg10_noviremen='" & strNoViremen & "' and kodstatusdok= '05'"
            If fCheckRec(strSql) > 0 Then
                strSql = "DELETE from BG12_StatusDok  WHERE bg10_noviremen=@NoViremen and kodstatusdok= @KodStatusDok"
                Dim paramSql2() As SqlParameter = {
                    New SqlParameter("@NoViremen", strNoViremen),
                    New SqlParameter("@KodStatusDok", "05")
                    }

                If Not dbConn.fUpdateCommand(strSql, paramSql2) > 0 Then
                    dbConn.sConnRollbackTrans()
                    Return False
                End If
            Else
                strSql = "INSERT INTO BG12_StatusDok(bg10_noviremen,kodstatusdok,bg12_tkhproses,bg12_nostaf,bg12_ulasan) " &
        "values (@NoViremen, @KodStatusDok, @TkhProses, @NoStaf, @Ulasan)"

                Dim paramSql3() As SqlParameter = {
                New SqlParameter("@NoViremen", strNoViremen),
                New SqlParameter("@KodStatusDok", "05"),
                New SqlParameter("@TkhProses", strTkhToday),
                New SqlParameter("@NoStaf", strNoStaf),
                New SqlParameter("@Ulasan", strUlasan)
            }

                If Not dbConn.fInsertCommand(strSql, paramSql3) > 0 Then
                    dbConn.sConnRollbackTrans()
                    Return False
                End If
            End If

            'VIREMEN KELUAR
            'SELECT VOT SEBAGAI KELUAR
            strSql = "select mk01_virkeluar,mk01_perutk, mk01_bljytd,mk01_bakitng, mk01_PemLulus  from mk01_vottahun WITH (NOLOCK) " &
                    "where kodkw='" & strKodKWF & "' and KodKO = '" & strKodKOF & "' and kodptj='" & strKodPTjF & "' and KodKP = '" & strKodKPF & "' and kodvot='" & strObjSbgF & "'  and right(mk01_tahun,2)='" & strNoViremen.Substring(strNoViremen.Length - 2) & "'"
            dsKs = fGetRec(strSql)


            'KEMAS KINI MK01_TAHUN VOT SEBAGAI
            If Not dsKs Is Nothing Then
                        If dsKs.Tables(0).Rows.Count > 0 Then

                            Dim decVirkeluar, decPerutk, decBljytd, decBakitng, decPemLulus As Decimal
                            Dim decJumVirK, decJumNet, decJumBlj, decJumTng, decJumPemLulus As Decimal
                            Dim decBakiSms, decBakiPerutk, decBakiSlpsPem As Decimal

                            decVirkeluar = CDec(Trim(dsKs.Tables(0).Rows(0)("mk01_virkeluar").ToString.TrimEnd))
                            decPerutk = CDec(Trim(dsKs.Tables(0).Rows(0)("mk01_perutk").ToString.TrimEnd))
                            decBljytd = CDec(Trim(dsKs.Tables(0).Rows(0)("mk01_bljytd").ToString.TrimEnd))
                            decBakitng = CDec(Trim(dsKs.Tables(0).Rows(0)("mk01_bakitng").ToString.TrimEnd))
                            decPemLulus = CDec(Trim(dsKs.Tables(0).Rows(0)("mk01_PemLulus").ToString.TrimEnd))

                            decJumVirK = decVirkeluar + decAmaunK
                            decJumNet = decPerutk - decAmaunK
                            decJumBlj = decBljytd
                            decJumTng = decBakitng
                            decJumPemLulus = decPemLulus

                            decBakiSms = decJumNet - decJumBlj
                            decBakiPerutk = decJumNet - decJumBlj - decJumTng
                            decBakiSlpsPem = decJumNet - decJumPemLulus

                    strSql = "update MK01_VotTahun set MK01_VirKeluar = @VirKeluar, MK01_Perutk = @Perutk, MK01_BakiSms = @BakiSms, MK01_BakiPerutk = @BakiPerutk, MK01_BakiSlpsPemLulus = @BakiSlpsPemLulus " &
                                        "where kodkw=@KodKW and KodKO = @KodKO and kodptj=@KodPTj and KodKP = @KodKP and kodvot=@KodVot  and right(mk01_tahun,2)=@Tahun"
                    Dim paramSql6() As SqlParameter = {
                                New SqlParameter("@VirKeluar", decJumVirK),
                                New SqlParameter("@Perutk", decJumNet),
                                New SqlParameter("@BakiSms", decBakiSms),
                                New SqlParameter("@BakiPerutk", decBakiPerutk),
                                New SqlParameter("@BakiSlpsPemLulus", decBakiSlpsPem),
                                New SqlParameter("@KodKW", strKodKWF),
                                New SqlParameter("@KodKO", strKodKOF),
                                New SqlParameter("@KodPTj", strKodPTjF),
                                New SqlParameter("@KodKP", strKodKPF),
                                New SqlParameter("@KodVot", strObjSbgF),
                                New SqlParameter("@Tahun", strNoViremen.Substring(strNoViremen.Length - 2))
                                }

                    If Not dbConn.fUpdateCommand(strSql, paramSql6) > 0 Then
                                dbConn.sConnRollbackTrans()
                                Return False
                            End If
                        End If
                    End If

            'SELECT VOT AM KELUAR
            strSql = "select mk01_virkeluar,mk01_perutk, mk01_bljytd,mk01_bakitng, mk01_PemLulus  from mk01_vottahun WITH (NOLOCK) " &
                    "where kodkw='" & strKodKWF & "' and KodKO = '" & strKodKOF & "' and kodptj='" & strKodPTjF & "' and KodKP = '" & strKodKPF & "' and kodvot='" & strObjAmF & "'  and right(mk01_tahun,2)='" & strNoViremen.Substring(strNoViremen.Length - 2) & "'"
            dsKa = fGetRec(strSql)
            'KEMAS KINI MK01_TAHUN VOT AM
            If Not dsKa Is Nothing Then
                If dsKa.Tables(0).Rows.Count > 0 Then

                    Dim decVirkeluar, decPerutk, decBljytd, decBakitng, decPemLulus As Decimal
                    Dim decJumVirK, decJumNet, decJumBlj, decJumTng, decJumPemLulus As Decimal
                    Dim decBakiSms, decBakiPerutk, decBakiSlpsPem As Decimal

                    decVirkeluar = CDec(Trim(dsKa.Tables(0).Rows(0)("mk01_virkeluar").ToString.TrimEnd))
                    decPerutk = CDec(Trim(dsKa.Tables(0).Rows(0)("mk01_perutk").ToString.TrimEnd))
                    decBljytd = CDec(Trim(dsKa.Tables(0).Rows(0)("mk01_bljytd").ToString.TrimEnd))
                    decBakitng = CDec(Trim(dsKa.Tables(0).Rows(0)("mk01_bakitng").ToString.TrimEnd))
                    decPemLulus = CDec(Trim(dsKa.Tables(0).Rows(0)("mk01_PemLulus").ToString.TrimEnd))

                    decJumVirK = decVirkeluar + decAmaunK
                    decJumNet = decPerutk - decAmaunK
                    decJumBlj = decBljytd
                    decJumTng = decBakitng
                    decJumPemLulus = decPemLulus

                    decBakiSms = decJumNet - decJumBlj
                    decBakiPerutk = decJumNet - decJumBlj - decJumTng
                    decBakiSlpsPem = decJumNet - decJumPemLulus


                    strSql = "update MK01_VotTahun set MK01_VirKeluar = @VirKeluar, MK01_Perutk = @Perutk, MK01_BakiSms = @BakiSms, MK01_BakiPerutk = @BakiPerutk, MK01_BakiSlpsPemLulus = @BakiSlpsPemLulus " &
                                        "where kodkw=@KodKW and KodKO = @KodKO and kodptj=@KodPTj and KodKP = @KodKP and kodvot=@KodVotA  and right(mk01_tahun,2)= @Tahun"
                    Dim paramSql6() As SqlParameter = {
                                New SqlParameter("@VirKeluar", decJumVirK),
                                New SqlParameter("@Perutk", decJumNet),
                                New SqlParameter("@BakiSms", decBakiSms),
                                New SqlParameter("@BakiPerutk", decBakiPerutk),
                                New SqlParameter("@BakiSlpsPemLulus", decBakiSlpsPem),
                                New SqlParameter("@KodKW", strKodKWF),
                                New SqlParameter("@KodKO", strKodKOF),
                                New SqlParameter("@KodPTj", strKodPTjF),
                                New SqlParameter("@KodKP", strKodKPF),
                                New SqlParameter("@KodVotA", strObjAmF),
                                New SqlParameter("@Tahun", strNoViremen.Substring(strNoViremen.Length - 2))
                                }

                    If Not dbConn.fUpdateCommand(strSql, paramSql6) > 0 Then
                        dbConn.sConnRollbackTrans()
                        Return False
                    End If
                End If
            End If
            'End If
            'End If

            'VIREMEN MASUK
            'SELECT VOT SEBAGAI MASUK
            strSql = "select mk01_virmasuk,mk01_perutk, mk01_bljytd,mk01_bakitng, mk01_PemLulus  from mk01_vottahun WITH (NOLOCK) " &
                    "where kodkw='" & strKodKWT & "' and KodKO = '" & strKodKOT & "' and kodptj='" & strKodPTjT & "' and KodKP = '" & strKodKPT & "' and kodvot='" & strObjSbgT & "'  and right(mk01_tahun,2)='" & strNoViremen.Substring(strNoViremen.Length - 2) & "'"
            dsMs = fGetRec(strSql)

            'KEMAS KINI MK01_TAHUN VOT SEBAGAI
            If Not dsMs Is Nothing Then
                        If dsMs.Tables(0).Rows.Count > 0 Then

                            Dim decVirMasuk, decPerutk, decBljytd, decBakitng, decPemLulus As Decimal
                            Dim decJumVirM, decJumNet, decJumBlj, decJumTng, decJumPemLulus As Decimal
                            Dim decBakiSms, decBakiPerutk, decBakiSlpsPem As Decimal

                            decVirMasuk = CDec(Trim(dsMs.Tables(0).Rows(0)("mk01_virmasuk").ToString.TrimEnd))
                            decPerutk = CDec(Trim(dsMs.Tables(0).Rows(0)("mk01_perutk").ToString.TrimEnd))
                            decBljytd = CDec(Trim(dsMs.Tables(0).Rows(0)("mk01_bljytd").ToString.TrimEnd))
                            decBakitng = CDec(Trim(dsMs.Tables(0).Rows(0)("mk01_bakitng").ToString.TrimEnd))
                            decPemLulus = CDec(Trim(dsMs.Tables(0).Rows(0)("mk01_PemLulus").ToString.TrimEnd))

                            decJumVirM = decVirMasuk + decAmaunK
                            decJumNet = decPerutk + decAmaunK
                            decJumBlj = decBljytd
                            decJumTng = decBakitng
                            decJumPemLulus = decPemLulus

                            decBakiSms = decJumNet - decJumBlj
                            decBakiPerutk = decJumNet - decJumBlj - decJumTng
                            decBakiSlpsPem = decJumNet - decJumPemLulus

                            strSql = "update MK01_VotTahun set mk01_virmasuk = @VirMasuk, MK01_Perutk = @Perutk, MK01_BakiSms = @BakiSms, MK01_BakiPerutk = @BakiPerutk, MK01_BakiSlpsPemLulus = @BakiSlpsPemLulus " &
                                        "where kodkw=@KodKW and KodKO = @KodKO and kodptj=@KodPTj and KodKP = @KodKP and kodvot=@KodVot  and right(mk01_tahun,2)=@Tahun"
                    Dim paramSql6() As SqlParameter = {
                                New SqlParameter("@VirMasuk", decJumVirM),
                                New SqlParameter("@Perutk", decJumNet),
                                New SqlParameter("@BakiSms", decBakiSms),
                                New SqlParameter("@BakiPerutk", decBakiPerutk),
                                New SqlParameter("@BakiSlpsPemLulus", decBakiSlpsPem),
                                New SqlParameter("@KodKW", strKodKWT),
                                New SqlParameter("@KodKO", strKodKOT),
                                New SqlParameter("@KodPTj", strKodPTjT),
                                New SqlParameter("@KodKP", strKodKPT),
                                New SqlParameter("@KodVot", strObjSbgT),
                                New SqlParameter("@Tahun", strNoViremen.Substring(strNoViremen.Length - 2))
                                }

                    If Not dbConn.fUpdateCommand(strSql, paramSql6) > 0 Then
                                dbConn.sConnRollbackTrans()
                                Return False
                            End If
                        End If
                    End If

            'SELECT Vot AM MASUK
            strSql = "select mk01_virmasuk,mk01_perutk, mk01_bljytd,mk01_bakitng, mk01_PemLulus  from mk01_vottahun WITH (NOLOCK) " &
                    "where kodkw='" & strKodKWT & "' and KodKO = '" & strKodKOT & "' and kodptj='" & strKodPTjT & "' and KodKP = '" & strKodKPT & "' and kodvot='" & strObjAmT & "'  and right(mk01_tahun,2)='" & strNoViremen.Substring(strNoViremen.Length - 2) & "'"
            dsMa = fGetRec(strSql)

            'KEMAS KINI MK01_TAHUN VOT AM
            If Not dsMa Is Nothing Then
                If dsMa.Tables(0).Rows.Count > 0 Then

                    Dim decVirmasuk, decPerutk, decBljytd, decBakitng, decPemLulus As Decimal
                    Dim decJumVirM, decJumNet, decJumBlj, decJumTng, decJumPemLulus As Decimal
                    Dim decBakiSms, decBakiPerutk, decBakiSlpsPem As Decimal

                    decVirmasuk = CDec(Trim(dsMa.Tables(0).Rows(0)("mk01_virmasuk").ToString.TrimEnd))
                    decPerutk = CDec(Trim(dsMa.Tables(0).Rows(0)("mk01_perutk").ToString.TrimEnd))
                    decBljytd = CDec(Trim(dsMa.Tables(0).Rows(0)("mk01_bljytd").ToString.TrimEnd))
                    decBakitng = CDec(Trim(dsMa.Tables(0).Rows(0)("mk01_bakitng").ToString.TrimEnd))
                    decPemLulus = CDec(Trim(dsMa.Tables(0).Rows(0)("mk01_PemLulus").ToString.TrimEnd))

                    decJumVirM = decVirmasuk + decAmaunK
                    decJumNet = decPerutk + decAmaunK
                    decJumBlj = decBljytd
                    decJumTng = decBakitng
                    decJumPemLulus = decPemLulus

                    decBakiSms = decJumNet - decJumBlj
                    decBakiPerutk = decJumNet - decJumBlj - decJumTng
                    decBakiSlpsPem = decJumNet - decJumPemLulus


                    strSql = "update MK01_VotTahun set mk01_virmasuk = @VirMasuk, MK01_Perutk = @Perutk, MK01_BakiSms = @BakiSms, MK01_BakiPerutk = @BakiPerutk, MK01_BakiSlpsPemLulus = @BakiSlpsPemLulus " &
                                        "where kodkw=@KodKW and KodKO = @KodKO and kodptj=@KodPTj and KodKP = @KodKP and kodvot=@KodVotA  and right(mk01_tahun,2)=@Tahun"
                    Dim paramSql6() As SqlParameter = {
                                New SqlParameter("@VirMasuk", decJumVirM),
                                New SqlParameter("@Perutk", decJumNet),
                                New SqlParameter("@BakiSms", decBakiSms),
                                New SqlParameter("@BakiPerutk", decBakiPerutk),
                                New SqlParameter("@BakiSlpsPemLulus", decBakiSlpsPem),
                                New SqlParameter("@KodKW", strKodKWT),
                                New SqlParameter("@KodKO", strKodKOT),
                                New SqlParameter("@KodPTj", strKodPTjT),
                                New SqlParameter("@KodKP", strKodKPT),
                                New SqlParameter("@KodVotA", strObjAmT),
                                New SqlParameter("@Tahun", strNoViremen.Substring(strNoViremen.Length - 2))
                                }

                    If Not dbConn.fUpdateCommand(strSql, paramSql6) > 0 Then
                        dbConn.sConnRollbackTrans()
                        Return False
                    End If
                End If
            End If

            'End If
            'End If


            'POSTING LEJAR VIREMEN KELUAR
            fGetKodJen(strObjSbgF, strKodJen, strKodjenlanjut)

            strSql = "INSERT INTO mk06_transaksi(mk06_tkhtran,mk06_rujukan,mk06_nodok,mk06_butiran,koddok,mk06_kodap,mk06_bil,kodkw, KodKO,kodptj,KodKP,kodvot,kodjen,kodjenlanjut,mk06_debit,mk06_kredit,mk06_bulan,mk06_tahun,mk06_status) " &
        "VALUES (@TkhTrans,@Rujukan,@NoDok,@Butiran,@KodDok,@KodAP,@Bil,@KodKW,@KodKO,@KodPTj,@KodKP,@KodVot,@KodJen,@kodJenLanjut,@Debit,@Kredit,@Bulan,@Tahun,@Status)"

            Dim paramSql4() As SqlParameter = {
                New SqlParameter("@TkhTrans", strTkhToday),
                New SqlParameter("@Rujukan", strNoViremen),
                New SqlParameter("@NoDok", strNoViremen),
                New SqlParameter("@Butiran", "VIREMEN KELUAR"),
                New SqlParameter("@KodDok", "VIRKELUAR"),
                New SqlParameter("@KodAP", "-"),
                New SqlParameter("@Bil", 1),
                New SqlParameter("@KodKW", strKodKWF),
                New SqlParameter("@KodKO", strKodKOF),
                New SqlParameter("@KodPTj", strKodPTjF),
                New SqlParameter("@KodKP", strKodKPF),
                New SqlParameter("@KodVot", strObjSbgF),
                New SqlParameter("@KodJen", strKodJen),
                New SqlParameter("@kodJenLanjut", strKodjenlanjut),
                New SqlParameter("@Debit", decAmaunK),
                New SqlParameter("@Kredit", 0),
                New SqlParameter("@Bulan", intBulan),
                New SqlParameter("@Tahun", intTahun),
                New SqlParameter("@Status", 0)
            }

            If Not dbConn.fInsertCommand(strSql, paramSql4) > 0 Then
                dbConn.sConnRollbackTrans()
                Return False
            End If

            'POSTING LEJAR VIREMEN MASUK
            fGetKodJen(strObjSbgT, strKodJen, strKodjenlanjut)

            strSql = "INSERT INTO mk06_transaksi(mk06_tkhtran,mk06_rujukan,mk06_nodok,mk06_butiran,koddok,mk06_kodap,mk06_bil,kodkw, KodKO,kodptj,KodKP,kodvot,kodjen,kodjenlanjut,mk06_debit,mk06_kredit,mk06_bulan,mk06_tahun,mk06_status) " &
            "VALUES (@TkhTrans,@Rujukan,@NoDok,@Butiran,@KodDok,@KodAP,@Bil,@KodKW,@KodKO,@KodPTj,@KodKP,@KodVot,@KodJen,@kodJenLanjut,@Debit,@Kredit,@Bulan,@Tahun,@Status)"

            Dim paramSql5() As SqlParameter = {
                New SqlParameter("@TkhTrans", strTkhToday),
                New SqlParameter("@Rujukan", strNoViremen),
                New SqlParameter("@NoDok", strNoViremen),
                New SqlParameter("@Butiran", "VIREMEN MASUK"),
                New SqlParameter("@KodDok", "VIRMASUK"),
                New SqlParameter("@KodAP", "-"),
                New SqlParameter("@Bil", 2),
                New SqlParameter("@KodKW", strKodKWT),
                New SqlParameter("@KodKO", strKodKOT),
                New SqlParameter("@KodPTj", strKodPTjT),
                New SqlParameter("@KodKP", strKodKPT),
                New SqlParameter("@KodVot", strObjSbgT),
                New SqlParameter("@KodJen", strKodJen),
                New SqlParameter("@kodJenLanjut", strKodjenlanjut),
                New SqlParameter("@Debit", 0),
                New SqlParameter("@Kredit", decAmaunM),
                New SqlParameter("@Bulan", intBulan),
                New SqlParameter("@Tahun", intTahun),
                New SqlParameter("@Status", 0)
            }

            If Not dbConn.fInsertCommand(strSql, paramSql5) > 0 Then
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

    Private Function fLulus2(ByVal strNoViremen As String, ByVal strKodStatusDok As String) As Boolean
        Dim strSql As String
        Dim strTkhProses As String = Now.ToString("yyyy-MM-dd")
        Dim strNoStaf As String = Session("ssusrID")
        Dim strUlasan As String = IIf(Trim(txtUlasan.Text.TrimEnd) = "", "-", (Trim(txtUlasan.Text.TrimEnd)))
        Dim dbconn As New DBKewConn
        Try


            dbconn.sConnBeginTrans()
            strSql = "UPDATE BG10_Viremen  SET BG10_StatusKJ=@StatusKJ,BG10_StatusBen=@StatusBen, kodstatusdok=@StatusDok " &
                           "WHERE BG10_NoViremen  = @NoViremen"
            Dim paramSql1() As SqlParameter = {
                New SqlParameter("@StatusKJ", 1),
                New SqlParameter("@StatusBen", 0),
                New SqlParameter("@StatusDok", strKodStatusDok),
                New SqlParameter("@NoViremen", strNoViremen)
                }

            If Not dbconn.fUpdateCommand(strSql, paramSql1) > 0 Then
                dbconn.sConnRollbackTrans()
                Return False
            End If


            strSql = "select count(*) from BG12_StatusDok where bg10_noviremen = '" & strNoViremen & "' and kodstatusdok = '" & strKodStatusDok & "'"
            If Not fCheckRec(strSql) > 0 Then
                strSql = "INSERT INTO BG12_StatusDok(bg10_noviremen,kodstatusdok,bg12_tkhproses,bg12_nostaf,bg12_ulasan) " &
        "values (@NoViremen, @KodStatusDok, @TkhProses, @NoStaf, @Ulasan)"

                Dim paramSql3() As SqlParameter = {
                    New SqlParameter("@NoViremen", strNoViremen),
                    New SqlParameter("@KodStatusDok", strKodStatusDok),
                    New SqlParameter("@TkhProses", strTkhProses),
                    New SqlParameter("@NoStaf", strNoStaf),
                    New SqlParameter("@Ulasan", strUlasan)
                }

                If Not dbconn.fInsertCommand(strSql, paramSql3) > 0 Then
                    dbconn.sConnRollbackTrans()
                    Return False
                End If
            End If

            dbconn.sConnCommitTrans()
            Return True
        Catch ex As Exception
            dbconn.sConnRollbackTrans()
            Return False
        End Try
    End Function

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

    Private Sub lbtnLulus_Click(sender As Object, e As EventArgs) Handles lbtnLulus.Click
        Try
            If fLulusV() = True Then
                fGlobalAlert("Permohonan viremen telah diluluskan!", Me.Page, Me.[GetType]())
                fReset()
                fBindViremen()
                divlst.Visible = True
                divDt.Visible = False
            Else
                fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lbtnXLulus_Click(sender As Object, e As EventArgs) Handles lbtnXLulus.Click
        If fReject() = True Then
            fGlobalAlert("Maklumat kelulusan viremen telah disimpan!", Me.Page, Me.[GetType]())
            fReset()
            fBindViremen()
            divlst.Visible = True
            divDt.Visible = False
        Else
            fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
        End If
    End Sub

    Protected Sub lbtnKembali_Click(sender As Object, e As EventArgs) Handles lbtnKembali.Click
        divlst.Visible = True
        divDt.Visible = False
    End Sub

    Private Sub gvViremen_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvViremen.PageIndexChanging
        Try
            gvViremen.PageIndex = e.NewPageIndex
            If ViewState("dtList") IsNot Nothing Then
                gvViremen.DataSource = ViewState("dtList")
                gvViremen.DataBind()
            End If

        Catch ex As Exception

        End Try
    End Sub
End Class