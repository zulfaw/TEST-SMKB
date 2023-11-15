Public Class Senarai_Kelulusan1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                txtTahun.Text = Now.Year
                fBindDdlPTj()
                fBindViremen()

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

            ds = dbconn.fselectCommand(strSql)

            ddlPTJ.DataSource = ds
            ddlPTJ.DataTextField = "Butiran"
            ddlPTJ.DataValueField = "KodPTJ"
            ddlPTJ.DataBind()

            ddlPTJ.Items.Insert(0, New ListItem("- KESELURUHAN -", "0"))
            ddlPTJ.SelectedIndex = 0

        Catch ex As Exception

        End Try

    End Function

    Private Sub fBindViremen()
        Try
            Dim intRec As Integer
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

            Dim strKodPTj As String = ddlPTJ.SelectedValue

            If strKodPTj <> "0" Then
                strFilPTj = "and dbo.VBG_VirKeluar.KodPTJ='" & strKodPTj & "'"
            End If

            strSql = "SELECT DISTINCT dbo.BG10_Viremen.BG10_NoViremen,dbo.BG10_Viremen.BG10_TkhMohon,dbo.BG10_Viremen.BG10_NoStaf, dbo.BG10_Viremen.BG10_RujSurat, dbo.BG10_Viremen.KodStatusDok, dbo.VBG_VirKeluar.KodKw AS kodkwk, " &
            "dbo.VBG_VirKeluar.kodko  AS kodkok, dbo.VBG_VirKeluar.KodPTJ AS kodptjk, dbo.VBG_VirKeluar.kodkp AS kodkpk, dbo.VBG_VirKeluar.KodVot AS kodvotk,  dbo.VBG_VirKeluar.BG10_Amaun AS jumk, dbo.VBG_VirKeluar.KodVir AS kodvirk, " &
            "dbo.VBG_VirMasuk.KodKw AS kodkwm, dbo.VBG_VirMasuk.KodKO  AS kodkom, dbo.VBG_VirMasuk.KodPTJ AS kodptjm, dbo.VBG_VirMasuk.KodKP  AS kodKpm, dbo.VBG_VirMasuk.KodVot AS kodvotm,  dbo.VBG_VirMasuk.BG10_Amaun AS jumm, " &
            "dbo.VBG_VirMasuk.KodVir AS kodvirm, dbo.BG10_Viremen.BG10_StatusBen, dbo.BG10_Viremen.BG10_StatusKJ, dbo.VBG_VirKeluar.BG10_BakiSms AS bakismsk, MAX(dbo.VBG_VirKeluar.BG12_TkhProses) AS BG09_TkhProses, " &
            "dbo.VBG_VirMasuk.BG10_BakiSms AS bakismsm, dbo.BG10_Viremen.BG10_RujSuratLulus FROM dbo.BG10_Viremen INNER JOIN dbo.VBG_VirKeluar ON dbo.BG10_Viremen.BG10_NoViremen = dbo.VBG_VirKeluar.BG10_NoViremen " &
            "INNER JOIN dbo.VBG_VirMasuk ON dbo.BG10_Viremen.BG10_NoViremen = dbo.VBG_VirMasuk.BG10_NoViremen where BG10_Tahun = '" & Trim(txtTahun.Text.TrimEnd) & "' and dbo.BG10_Viremen.BG10_StatusKJ = 1 and dbo.BG10_Viremen.BG10_StatusBen=1  " & strFilPTj & " " &
            "GROUP BY dbo.BG10_Viremen.BG10_NoViremen,dbo.BG10_Viremen.BG10_TkhMohon,dbo.BG10_Viremen.BG10_NoStaf, dbo.BG10_Viremen.BG10_RujSurat, dbo.BG10_Viremen.KodStatusDok, dbo.VBG_VirKeluar.KodKw,dbo.VBG_VirKeluar.kodko, dbo.VBG_VirKeluar.KodPTJ, " &
            "dbo.VBG_VirKeluar.kodkp, dbo.VBG_VirKeluar.KodVot,  dbo.VBG_VirKeluar.BG10_Amaun, dbo.VBG_VirKeluar.KodVir, dbo.VBG_VirMasuk.KodKw,dbo.VBG_VirMasuk.KodKO, dbo.VBG_VirMasuk.KodPTJ,dbo.VBG_VirMasuk.KodKP, dbo.VBG_VirMasuk.KodVot, " &
            "dbo.VBG_VirMasuk.BG10_Amaun, dbo.VBG_VirMasuk.KodVir, dbo.VBG_VirKeluar.BG10_BakiSms, dbo.VBG_VirMasuk.BG10_BakiSms, dbo.BG10_Viremen.BG10_RujSuratLulus, dbo.BG10_Viremen.BG10_StatusBen , dbo.BG10_Viremen.BG10_StatusKJ " &
            "order by dbo.BG10_Viremen.BG10_NoViremen"

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
            Else
                intRec = 0
            End If

            lblJumRekod.InnerText = intRec

            gvViremen.DataSource = dt
            gvViremen.DataBind()

            'fReset()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ddlPTJ_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPTJ.SelectedIndexChanged
        fBindViremen()
    End Sub

    Private Sub gvViremen_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvViremen.RowCommand
        Try
            If e.CommandName = "Select" Then
                Dim strButiranKW, strButiranKO, strButiranPTj, strButiranKP, strButiranVot As String

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

                Dim strKodKWF As String = dvViremen.Item(0)("kodkwk").ToString
                Dim strkodKOF As String = dvViremen.Item(0)("kodkok").ToString
                Dim strKodPTjF As String = dvViremen.Item(0)("kodptjk").ToString
                Dim strKodKPF As String = dvViremen.Item(0)("kodkpk").ToString
                Dim strKodSbgF As String = dvViremen.Item(0)("kodvotk").ToString
                Dim decAmaunF As Decimal = CDec(dvViremen.Item(0)("jumk").ToString)
                Dim strAmaunF As String = FormatNumber(decAmaunF, 2)
                Dim decBakiSmsF As Decimal


                Dim strKodKWT As String = dvViremen.Item(0)("kodkwm").ToString
                Dim strkodKOT As String = dvViremen.Item(0)("kodkom").ToString
                Dim strKodPTjT As String = dvViremen.Item(0)("kodptjm").ToString
                Dim strKodKPT As String = dvViremen.Item(0)("kodkpm").ToString
                Dim strKodSbgT As String = dvViremen.Item(0)("kodvotm").ToString
                Dim decAmaunT As Decimal = CDec(dvViremen.Item(0)("jumm").ToString)
                Dim strAmaunT As String = FormatNumber(decAmaunT, 2)
                Dim decBakiSmsT As Decimal
                'Dim strBakiSmsT As String = dvViremen.Item(0)("bakismsm").ToString

                Dim strNamaPmhn, strJawPmhn, strKodPTjPmhn, strPTjPmhn As String

                txtTkhMohon.Text = strTkhMohon


                Dim strNoStafPmhn As String = dvViremen.Item(0)("BG10_NoStaf").ToString
                Dim dt As New DataTable
                dt = fGetUserInfo(strNoStafPmhn)
                If dt.Rows.Count > 0 Then
                    strNamaPmhn = dt.Rows.Item(0).Item("MS01_Nama")
                    strJawPmhn = dt.Rows.Item(0).Item("JawGiliran")
                    strKodPTjPmhn = dt.Rows.Item(0).Item("KodPejabat") & "0000"
                    strPTjPmhn = dt.Rows.Item(0).Item("Pejabat")
                End If

                txtNoStaf.Text = strNoStafPmhn
                txtNamaStaf.Text = strNamaPmhn
                txtJawatan.Text = strJawPmhn
                txtkodPTjPemohon.Text = strKodPTjPmhn
                txtPTjPemohon.Text = strPTjPmhn

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
                'txtBakiT.Text = strBakiSmsT
                decBakiSmsT = CDec(fGetBakiSebenar(intTahun, strTarikh, strKodKWT, strkodKOT, strKodPTjT, strKodKPT, strKodSbgT))
                txtBakiT.Text = FormatNumber(decBakiSmsT, 2)
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class