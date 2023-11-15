Imports System.Drawing
Imports System.Data.SqlClient
Public Class Kelulusan_Bendahari
    Inherits System.Web.UI.Page

    Dim strNamaPTj As String
    Dim sLogBaru As String
    Dim sLogMedan As String
    Dim strNoStaf As String = UserInfo.strSesStaffID
    Dim sLogLevel As String = UserInfo.strUsrTahap
    Dim lsLogUsrIP As String = UserInfo.strUsrPcIP
    Dim lsLogUsrPC As String = UserInfo.strUsrPCName

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Session("sesKodSubMenu") = Request.QueryString("KodSubMenu")
                Dim dateNow As Date = Now.AddYears(+1)
                Dim strNextYear As String = dateNow.Year

                txtTahun.Text = Now.Year
                fBindGvSenarai()

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindGvSenarai()
        sClearGvSenarai()
        Try
            Dim intRec As Integer
            Dim strSql As String
            Dim dt As New DataTable
            dt.Columns.Add("kodKw", GetType(String))
            dt.Columns.Add("kodKo", GetType(String))
            dt.Columns.Add("PTj", GetType(String))
            dt.Columns.Add("KodKp", GetType(String))
            dt.Columns.Add("ObjSbg", GetType(String))
            dt.Columns.Add("Amaun", GetType(String))
            dt.Columns.Add("Jenis", GetType(String))
            dt.Columns.Add("TkhMohon", GetType(String))
            dt.Columns.Add("Butiran", GetType(String))
            dt.Columns.Add("IndKW", GetType(String))
            dt.Columns.Add("IndPTj", GetType(String))
            dt.Columns.Add("IndObjAm", GetType(String))
            dt.Columns.Add("IndObjSbg", GetType(String))

            strSql = "SELECT dbo.bg04_AgihKw.bg04_IndKw as IndKW, dbo.bg05_AgihPTJ.bg05_IndPTJ as IndPTj, dbo.bg06_AgihObjAm.bg06_IndObjAm as IndObjAm,  dbo.bg07_AgihObjSbg.bg07_IndObjSbg as IndObjSbg, dbo.bg04_AgihKw.bg04_Tahun, dbo.bg04_AgihKw.KodKw, " &
            "dbo.bg05_AgihPTJ.KodKo , (Select dbo.MK_PTJ.KodPTJ + ' - ' + dbo.MK_PTJ.Butiran from dbo.MK_PTJ where dbo.MK_PTJ.KodPTJ  = dbo.bg05_AgihPTJ.KodPTJ) as PTj, dbo.bg06_AgihObjAm.KodKP ,dbo.bg06_AgihObjAm.KodVot AS objam,  " &
            "(Select dbo.MK_Vot.KodVot + ' - ' + dbo.MK_Vot.Butiran from dbo.MK_Vot where dbo.MK_Vot.KodVot = dbo.bg07_AgihObjSbg.KodVot) AS objsbg, dbo.bg04_AgihKw.bg04_Butiran, dbo.bg04_AgihKw.bg04_Amaun,dbo.bg04_AgihKw.KodAgih , " &
            "dbo.bg04_AgihKw.bg04_TkhAgih, dbo.bg07_AgihObjSbg.bg07_StatSah, dbo.bg07_AgihObjSbg.bg07_StatLulus, dbo.bg07_AgihObjSbg.bg07_Status, dbo.bg04_AgihKw.KodAgih FROM dbo.bg04_AgihKw INNER JOIN dbo.bg05_AgihPTJ ON dbo.bg04_AgihKw.bg04_IndKw = dbo.bg05_AgihPTJ.bg04_IndKw " &
            "INNER JOIN dbo.bg06_AgihObjAm ON dbo.bg05_AgihPTJ.bg05_IndPTJ = dbo.bg06_AgihObjAm.bg05_IndPTJ  INNER JOIN dbo.bg07_AgihObjSbg ON dbo.bg06_AgihObjAm.bg06_IndObjAm = dbo.bg07_AgihObjSbg.bg06_IndObjAm " &
            "where dbo.bg07_AgihObjSbg.bg07_StatSah Is NULL And dbo.bg07_AgihObjSbg.bg07_StatLulus Is NULL And dbo.bg07_AgihObjSbg.bg07_Status = '1' And dbo.bg07_AgihObjSbg.KodAgih in ('TB','KG') and dbo.bg04_AgihKw.bg04_Tahun ='" & Trim(txtTahun.Text.TrimEnd) & "' " &
            "order by dbo.bg04_AgihKw.BG04_IndKw , dbo.bg04_AgihKw.KodKw, dbo.bg05_AgihPTJ.KodPTJ,dbo.bg07_Agihobjsbg.Kodvot,dbo.bg07_Agihobjsbg.bg07_tkhagih"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            Dim strKW, strKO, strPTj, strKP, strObjSbg, strAmaun, strJenis, strTarikh, strDasar, strButiran, strIndKW, strIndPTj, strIndObjAm, strIndDasar, strIndObjSbg As String
            Dim decAmaun As Decimal
            If Not ds Is Nothing Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    strKW = ds.Tables(0).Rows(i)("KodKw")
                    strKO = ds.Tables(0).Rows(i)("KodKo")
                    strPTj = ds.Tables(0).Rows(i)("PTj")
                    strKP = ds.Tables(0).Rows(i)("KodKP")
                    strObjSbg = ds.Tables(0).Rows(i)("objsbg")
                    decAmaun = CDec(ds.Tables(0).Rows(i)("bg04_Amaun"))
                    strAmaun = FormatNumber(decAmaun, 2)
                    strJenis = ds.Tables(0).Rows(i)("KodAgih")
                    strTarikh = ds.Tables(0).Rows(i)("bg04_TkhAgih")
                    strButiran = ds.Tables(0).Rows(i)("bg04_Butiran")
                    strIndKW = ds.Tables(0).Rows(i)("IndKW")
                    strIndPTj = ds.Tables(0).Rows(i)("IndPTj")
                    strIndObjAm = ds.Tables(0).Rows(i)("IndObjAm")
                    strIndObjSbg = ds.Tables(0).Rows(i)("IndObjSbg")

                    dt.Rows.Add(strKW, strKO, strPTj, strKP, strObjSbg, strAmaun, strJenis, strTarikh, strButiran, strIndKW, strIndPTj, strIndObjAm, strIndObjSbg)
                Next

                gvSenarai.DataSource = dt
                gvSenarai.DataBind()
                ViewState("dtSenarai") = dt
                intRec = ds.Tables(0).Rows.Count
            Else
                intRec = 0
            End If

            lblJumRekod.InnerText = intRec
        Catch ex As Exception

        End Try
    End Sub

    Private Sub sClearGvSenarai()
        Try
            gvSenarai.DataSource = New List(Of String)
            gvSenarai.DataBind()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub gvSenarai_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvSenarai.RowCommand
        Try
            If e.CommandName = "Select" Then
                Dim strButiranKW = "", strButiranKO = "", strButiranKP = "", strButiranAm = "", strButiranDsr = ""

                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvSenarai.Rows(index)

                Dim strKodKW As String = TryCast(selectedRow.FindControl("lblKw"), Label).Text.ToString
                Dim strKodKo As String = TryCast(selectedRow.FindControl("lblKo"), Label).Text.ToString
                Dim strPTj As String = TryCast(selectedRow.FindControl("lblPTj"), Label).Text.ToString
                Dim strKodPTj As String = strPTj.Substring(0, 6)
                Dim strPTjB As String = strPTj.Substring(9, strPTj.Length - 9)
                Dim strKodKp As String = TryCast(selectedRow.FindControl("lblKp"), Label).Text.ToString
                Dim strObjSbg As String = TryCast(selectedRow.FindControl("lblObjSbg"), Label).Text.ToString
                Dim strKodSbg As String = strObjSbg.Substring(0, 5)
                Dim strObjAm As String = strObjSbg.Substring(0, 1) & "0000"
                Dim strAmaun As String = TryCast(selectedRow.FindControl("lblAmaun"), Label).Text.ToString
                Dim strJenis As String = TryCast(selectedRow.FindControl("lblJenis"), Label).Text.ToString
                Dim strTkhMohon As String = TryCast(selectedRow.FindControl("lblTkhMohon"), Label).Text.ToString
                Dim strButiranP As String = TryCast(selectedRow.FindControl("Butiran"), Label).Text.ToString
                Dim strTkhToday As String = Now.ToString("yyyy-MM-dd")

                lblKodJenis.Text = strJenis
                If strJenis = "TB" Then
                    lblJenis.Text = "Tambahan"
                    lblJenis.ForeColor = ColorTranslator.FromHtml("#0000FF")
                ElseIf strJenis = "KG" Then
                    lblJenis.Text = "Kurangan"
                    lblJenis.ForeColor = ColorTranslator.FromHtml("#990000")
                End If

                txtTarikh.Text = strTkhMohon

                fFindKW(strKodKW, strButiranKW)
                txtKodKW.Text = strKodKW
                txtKw.Text = strButiranKW

                fFindKO(strKodKo, strButiranKO)
                txtKodKo.Text = strKodKo
                txtKo.Text = strButiranKO

                txtKodPTj.Text = strKodPTj
                txtPTj.Text = strPTjB

                fFindKP(strKodKp, strButiranKP)
                txtKodKP.Text = strKodKp
                txtKP.Text = strButiranKP
                strButiranAm = fFindVot(strObjAm)

                txtKodObjAm.Text = strObjAm
                txtObjAm.Text = fFindVot(strObjAm)

                txtKodObjSbg.Text = strObjSbg.Substring(0, 5)
                txtObjSbg.Text = strObjSbg.Substring(8, strObjSbg.Length - 8)

                Dim decBaki As Decimal = fGetBakiSebenar(Trim(txtTahun.Text.TrimEnd), strTkhToday, strKodKW, strKodKo, strKodPTj, strKodKp, strKodSbg)
                Dim strBaki As String = FormatNumber(decBaki, 2)
                txtBaki.Text = strBaki
                txtButiran.Text = Trim(strButiranP.TrimEnd)
                txtAmaun.Text = strAmaun

                hidIndKW.Value = TryCast(selectedRow.FindControl("lblIndKW"), Label).Text.ToString
                hidIndPTj.Value = TryCast(selectedRow.FindControl("lblIndPTj"), Label).Text.ToString
                hidObjAm.Value = TryCast(selectedRow.FindControl("lblIndObjAm"), Label).Text.ToString
                hidObjSbg.Value = TryCast(selectedRow.FindControl("lblIndObjSbg"), Label).Text.ToString

                divLst.Visible = False
                divDt.Visible = True

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvSenarai_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvSenarai.RowDataBound
        Try
            Dim strJenis As String
            If e.Row.RowType = DataControlRowType.DataRow Then
                strJenis = DataBinder.Eval(e.Row.DataItem, "Jenis").ToString()
                If strJenis = "TB" Then
                    e.Row.FindControl("btnTB").Visible = True
                    e.Row.FindControl("btnKG").Visible = False
                    TryCast(e.Row.FindControl("lblAmaun"), Label).ForeColor = System.Drawing.Color.Blue
                ElseIf strJenis = "KG" Then
                    e.Row.FindControl("btnTB").Visible = False
                    e.Row.FindControl("btnKG").Visible = True
                    TryCast(e.Row.FindControl("lblAmaun"), Label).ForeColor = ColorTranslator.FromHtml("#990000")
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function fLulusTB(ByVal strKodJen As String, ByVal strKodJenLanjut As String, ByVal strIndObjSbg As String, ByVal strKodVotAm As String, ByVal strKodVotSbg As String, ByVal strTarikh As String, ByVal strNoStaff As String, ByVal strUlasan As String) As Boolean

        Dim blnSuccess As Boolean = True
        Dim dbconn As New DBKewConn

        Try

            Dim strSql As String
            Dim strKodKW As String = Trim(txtKodKW.Text.TrimEnd)
            Dim strKodKO As String = Trim(txtKodKo.Text.TrimEnd)
            Dim strkodPTj As String = Trim(txtKodPTj.Text.TrimEnd)
            Dim strKodKP As String = Trim(txtKodKP.Text.TrimEnd)
            Dim intBulan As Integer = Now.Month
            Dim strTahun As String = Trim(txtTahun.Text.TrimEnd)
            Dim decAmaun As Decimal = CDec(Trim(txtAmaun.Text.TrimEnd))
            Dim strButiran As String = Trim(txtButiran.Text.TrimEnd)

            dbconn.sConnBeginTrans()

            'KEMAS KINI TABLE BG06_AGIHOBJSBG
            strSql = "UPDATE bg07_agihobjsbg SET BG07_StatLulus=@StatLulus,BG07_TkhLulus=@TkhLulus,BG07_NoStafLulus=@NoStaff,
                BG07_Ulasan=@Ulasan WHERE BG07_IndObjSbg=@IndObjSbg And BG07_StatLulus Is NULL And BG07_TkhLulus Is NULL 
                And BG07_NoStafLulus Is NULL"
            Dim paramSql() As SqlParameter = {
                    New SqlParameter("@StatLulus", 1),
                    New SqlParameter("@TkhLulus", strTarikh),
                    New SqlParameter("@NoStaff", strNoStaff),
                    New SqlParameter("@Ulasan", strUlasan),
                    New SqlParameter("@IndObjSbg", strIndObjSbg)
                    }

            If Not dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                blnSuccess = False
                Exit Try
            End If

            'KEMAS KINI BUKU VOT SEBAGAI
            Dim decNettJum, decBakiSms, decBakiPerutk, decBakiSlpsPem As Decimal
            Dim decPerutk, decVirMasuk, decVirKeluar, decBljytd, decTngytd, decPemLulus As Decimal

            strSql = "select mk01_perutk, mk01_virmasuk, mk01_virkeluar, mk01_bljytd, mk01_tngytd, mk01_pemlulus from mk01_vottahun with (nolock) where mk01_tahun='" & strTahun & "' and kodkw='" & strKodKW & "' and KodKO = '" & strKodKO & "' " &
                    "And kodptj='" & strkodPTj & "' and kodkp= '" & strKodKP & "'  and kodvot='" & strKodVotSbg & "'"

            Dim ds2 As New DataSet
            ds2 = fGetRec(strSql)

            If Not ds2 Is Nothing Then
                decPerutk = CDec(ds2.Tables(0).Rows(0)("mk01_perutk").ToString)
                decVirMasuk = CDec(ds2.Tables(0).Rows(0)("mk01_virmasuk").ToString)
                decVirKeluar = CDec(ds2.Tables(0).Rows(0)("mk01_virkeluar").ToString)
                decBljytd = CDec(ds2.Tables(0).Rows(0)("mk01_bljytd").ToString)
                decTngytd = CDec(ds2.Tables(0).Rows(0)("mk01_tngytd").ToString)
                decPemLulus = CDec(ds2.Tables(0).Rows(0)("mk01_pemlulus").ToString)

                decNettJum = decPerutk + decAmaun + decVirMasuk - decVirKeluar
                decBakiSms = decNettJum - decBljytd
                decBakiPerutk = decNettJum - decBljytd - decTngytd
                decBakiSlpsPem = decNettJum - decPemLulus

                strSql = "UPDATE mk01_vottahun SET MK01_Perutk=@Perutk,MK01_BakiSms=@BakiSms,MK01_BakiPerutk=@BakiPerutk,MK01_BakiSlpsPemLulus=@BakiSlpsPemLulus " &
                "WHERE KodKw=@KodKW AND KodKO = @KodKO  AND KodPTJ=@KodPTj AND KodKP = @KodKP  AND KodVot=@KodVot  AND MK01_Tahun=@Tahun"
                Dim paramSql2() As SqlParameter = {
                    New SqlParameter("@Perutk", decNettJum),
                    New SqlParameter("@BakiSms", decBakiSms),
                    New SqlParameter("@BakiPerutk", decBakiPerutk),
                    New SqlParameter("@BakiSlpsPemLulus", decBakiSlpsPem),
                    New SqlParameter("@KodKW", strKodKW),
                    New SqlParameter("@KodKO", strKodKO),
                    New SqlParameter("@KodPTj", strkodPTj),
                     New SqlParameter("@KodKP", strKodKP),
                      New SqlParameter("@KodVot", strKodVotSbg),
                      New SqlParameter("@Tahun", strTahun)
                    }

                If Not dbconn.fUpdateCommand(strSql, paramSql2) > 0 Then
                    blnSuccess = False
                    Exit Try
                End If

            End If

            'KEMAS KINI BUKU VOT AM
            Dim decNettJum2, decBakiSms2, decBakiPerutk2, decBakiSlpsPem2 As Decimal
            Dim decPerutk2, decAmaun2, decVirMasuk2, decVirKeluar2, decBljytd2, decTngytd2, decPemLulus2 As Decimal

            strSql = "select mk01_perutk, mk01_virmasuk, mk01_virkeluar, mk01_bljytd, mk01_tngytd, mk01_pemlulus from mk01_vottahun with (nolock) where mk01_tahun='" & strTahun & "' and kodkw='" & strKodKW & "' and KodKO = '" & strKodKO & "' " &
        "And kodptj='" & strkodPTj & "' and kodkp= '" & strKodKP & "'  and kodvot='" & strKodVotAm & "'"
            Dim ds3 As New DataSet
            'ds3 = dbconn.fSelectCommand(strSql)
            ds3 = fGetRec(strSql)

            If Not ds3 Is Nothing Then
                decPerutk2 = CDec(ds3.Tables(0).Rows(0)("mk01_perutk").ToString)
                decAmaun2 = decAmaun
                decVirMasuk2 = CDec(ds3.Tables(0).Rows(0)("mk01_virmasuk").ToString)
                decVirKeluar2 = CDec(ds3.Tables(0).Rows(0)("mk01_virkeluar").ToString)
                decBljytd2 = CDec(ds3.Tables(0).Rows(0)("mk01_bljytd").ToString)
                decTngytd2 = CDec(ds3.Tables(0).Rows(0)("mk01_tngytd").ToString)
                decPemLulus2 = CDec(ds3.Tables(0).Rows(0)("mk01_pemlulus").ToString)

                decNettJum2 = decPerutk2 + decAmaun2 + decVirMasuk2 - decVirKeluar2
                decBakiSms2 = decNettJum2 - decBljytd2
                decBakiPerutk2 = decNettJum2 - decBljytd2 - decTngytd2
                decBakiSlpsPem2 = decNettJum2 - decPemLulus2

                strSql = "UPDATE mk01_vottahun SET MK01_Perutk=@Perutk,MK01_BakiSms=@BakiSms,MK01_BakiPerutk=@BakiPerutk,MK01_BakiSlpsPemLulus=@BakiSlpsPemLulus " &
            "WHERE KodKw=@KodKW AND KodKO = @KodKO  AND KodPTJ=@KodPTj AND KodKP = @KodKP  AND KodVot=@KodVot  AND MK01_Tahun=@Tahun"
                Dim paramSql3() As SqlParameter = {
                    New SqlParameter("@Perutk", decNettJum2),
                    New SqlParameter("@BakiSms", decBakiSms2),
                    New SqlParameter("@BakiPerutk", decBakiPerutk2),
                    New SqlParameter("@BakiSlpsPemLulus", decBakiSlpsPem2),
                    New SqlParameter("@KodKW", strKodKW),
                    New SqlParameter("@KodKO", strKodKO),
                    New SqlParameter("@KodPTj", strkodPTj),
                     New SqlParameter("@KodKP", strKodKP),
                      New SqlParameter("@KodVot", strKodVotAm),
                      New SqlParameter("@Tahun", strTahun)
                    }

                If Not dbconn.fUpdateCommand(strSql, paramSql3) > 0 Then
                    blnSuccess = False
                    Exit Try
                End If

            End If

            'POSTING KE LEJAR
            strSql = "INSERT INTO mk06_transaksi(mk06_tkhtran,mk06_rujukan,mk06_nodok,mk06_butiran,koddok, " &
                "mk06_kodap,mk06_bil,kodkw,KodKO,kodptj,KodKP,kodvot,kodjen,kodjenlanjut,mk06_debit,mk06_kredit,mk06_bulan,mk06_tahun,mk06_status) " &
                "VALUES (@TkhTran,@Rujukan,@NoDok,@Butiran,@KodDok,@KodAP,@Bil,@KodKW,@KodKO,@KodPTj,@KodKP,@KodVotSbg,@KodJen,@KodJenLanjut,@Debit,@Kredit,@Bulan,@Tahun,@Status)"

                Dim paramSql4() As SqlParameter = {
                    New SqlParameter("@TkhTran", strTarikh),
                    New SqlParameter("@Rujukan", strIndObjSbg),
                    New SqlParameter("@NoDok", strIndObjSbg),
                    New SqlParameter("@Butiran", strButiran),
                    New SqlParameter("@KodDok", "BJTTAMBAH"),
                    New SqlParameter("@KodAP", "-"),
                    New SqlParameter("@Bil", 1),
                    New SqlParameter("@KodKW", strKodKW),
                    New SqlParameter("@KodKO", strKodKO),
                    New SqlParameter("@KodPTj", strkodPTj),
                    New SqlParameter("@KodKP", strKodKP),
                    New SqlParameter("@KodVotSbg", strKodVotSbg),
                    New SqlParameter("@KodJen", strKodJen),
                    New SqlParameter("@KodJenLanjut", strKodJenLanjut),
                    New SqlParameter("@Debit", 0),
                    New SqlParameter("@Kredit", decAmaun),
                    New SqlParameter("@Bulan", intBulan),
                    New SqlParameter("@Tahun", strTahun),
                    New SqlParameter("@Status", 0)
                }

                If Not dbconn.fInsertCommand(strSql, paramSql4) > 0 Then
                    blnSuccess = False
                    Exit Try
                End If

            'KEMAS KINI BAJET UNI
            Dim decTambahan, decJumBesar As Decimal

            strSql = "select BG03_Tambahan, BG03_JumBesar from BG03_BjtUniv with (nolock) where bg03_tahun = '" & strTahun & "'"
            Dim ds4 As New DataSet
            ds4 = fGetRec(strSql)

            If Not ds4 Is Nothing Then
                decTambahan = CDec(ds4.Tables(0).Rows(0)("BG03_Tambahan").ToString)
                decJumBesar = CDec(ds4.Tables(0).Rows(0)("BG03_JumBesar").ToString)
            End If

            decTambahan = decTambahan + decAmaun
            decJumBesar = decJumBesar + decAmaun

            strSql = "UPDATE BG03_BjtUniv SET BG03_Tambahan=@JumTB,BG03_JumBesar=@JumBesar WHERE BG03_Tahun=@Tahun"
            Dim paramSql5() As SqlParameter = {
                    New SqlParameter("@JumTB", decTambahan),
                    New SqlParameter("@JumBesar", decJumBesar),
                    New SqlParameter("@Tahun", strTahun)
                    }

            If Not dbconn.fUpdateCommand(strSql, paramSql5) > 0 Then
                blnSuccess = False
                Exit Try
            End If

        Catch ex As Exception
            blnSuccess = False
        End Try

        If blnSuccess = True Then
            dbconn.sConnCommitTrans()
            Return True

        ElseIf blnSuccess = False Then
            dbconn.sConnRollbackTrans()
            Return False
        End If

    End Function

    Private Function fLulusKG(ByVal strKodJen As String, ByVal strKodJenLanjut As String, ByVal strIndObjSbg As String, ByVal strKodVotAm As String, ByVal strKodVotSbg As String, ByVal strTarikh As String, ByVal strNoStaff As String, ByVal strUlasan As String) As Boolean

        Dim strSql As String
        Dim dbconn As New DBKewConn
        Dim blnSuccess As Boolean = True

        Try

            Dim strKodKW As String = Trim(txtKodKW.Text.TrimEnd)
            Dim strKodKO As String = Trim(txtKodKo.Text.TrimEnd)
            Dim strkodPTj As String = Trim(txtKodPTj.Text.TrimEnd)
            Dim strKodKP As String = Trim(txtKodKP.Text.TrimEnd)
            Dim intBulan As Integer = Now.Month
            Dim strTahun As String = Trim(txtTahun.Text.TrimEnd)
            Dim decAmaun As Decimal = CDec(Trim(txtAmaun.Text.TrimEnd))
            Dim strButiran As String = Trim(txtButiran.Text.TrimEnd)

            dbconn.sConnBeginTrans()

            'KEMAS KINI TABLE BG06_AGIHOBJSBG
            strSql = "UPDATE bg07_agihobjsbg SET BG07_StatLulus=@StatLulus,BG07_TkhLulus=@TkhLulus,BG07_NoStafLulus=@NoStaff, " &
                "BG07_Ulasan=@Ulasan WHERE BG07_IndObjSbg=@IndObjSbg AND BG07_StatLulus IS NULL AND BG07_TkhLulus IS NULL " &
                "And BG07_NoStafLulus Is NULL"
            Dim paramSql() As SqlParameter = {
                    New SqlParameter("@StatLulus", 1),
                    New SqlParameter("@TkhLulus", strTarikh),
                    New SqlParameter("@NoStaff", strNoStaff),
                    New SqlParameter("@Ulasan", strUlasan),
                    New SqlParameter("@IndObjSbg", strIndObjSbg)
                    }

            If Not dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                blnSuccess = False
                Exit Try
            End If

            'KEMAS KINI BUKU VOT SEBAGAI
            Dim decNettJum, decBakiSms, decBakiPerutk, decBakiSlpsPem As Decimal
            Dim decPerutk, decVirMasuk, decVirKeluar, decBljytd, decTngytd, decPemLulus As Decimal

            strSql = "select mk01_perutk, mk01_virmasuk, mk01_virkeluar, mk01_bljytd, mk01_tngytd, mk01_pemlulus from mk01_vottahun where mk01_tahun='" & strTahun & "' and kodkw='" & strKodKW & "' and KodKO = '" & strKodKO & "' " &
                "And kodptj='" & strkodPTj & "' and kodkp= '" & strKodKP & "'  and kodvot='" & strKodVotSbg & "'"
            Dim ds As New DataSet
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then

                    decPerutk = CDec(ds.Tables(0).Rows(0)("mk01_perutk").ToString)
                    decVirMasuk = CDec(ds.Tables(0).Rows(0)("mk01_virmasuk").ToString)
                    decVirKeluar = CDec(ds.Tables(0).Rows(0)("mk01_virkeluar").ToString)
                    decBljytd = CDec(ds.Tables(0).Rows(0)("mk01_bljytd").ToString)
                    decTngytd = CDec(ds.Tables(0).Rows(0)("mk01_tngytd").ToString)
                    decPemLulus = CDec(ds.Tables(0).Rows(0)("mk01_pemlulus").ToString)

                    decNettJum = decPerutk - decAmaun + decVirMasuk - decVirKeluar
                    decBakiSms = decNettJum - decBljytd
                    decBakiPerutk = decNettJum - decBljytd - decTngytd
                    decBakiSlpsPem = decNettJum - decPemLulus

                    strSql = "UPDATE mk01_vottahun SET MK01_Perutk=@Perutk,MK01_BakiSms=@BakiSms,MK01_BakiPerutk=@BakiPerutk,MK01_BakiSlpsPemLulus=@BakiSlpsPemLulus " &
                    "WHERE KodKw=@KodKW AND KodKO = @KodKO  AND KodPTJ=@KodPTj AND KodKP = @KodKP  AND KodVot=@KodVot  AND MK01_Tahun=@Tahun"
                    Dim paramSql2() As SqlParameter = {
                        New SqlParameter("@Perutk", decNettJum),
                        New SqlParameter("@BakiSms", decBakiSms),
                        New SqlParameter("@BakiPerutk", decBakiPerutk),
                        New SqlParameter("@BakiSlpsPemLulus", decBakiSlpsPem),
                        New SqlParameter("@KodKW", strKodKW),
                        New SqlParameter("@KodKO", strKodKO),
                        New SqlParameter("@KodPTj", strkodPTj),
                         New SqlParameter("@KodKP", strKodKP),
                          New SqlParameter("@KodVot", strKodVotSbg),
                          New SqlParameter("@Tahun", strTahun)
                        }


                    If Not dbconn.fUpdateCommand(strSql, paramSql2) > 0 Then
                        blnSuccess = False
                        Exit Try
                    End If

                End If
            End If

            'KEMAS KINI BUKU VOT AM
            Dim decNettJum2, decBakiSms2, decBakiPerutk2, decBakiSlpsPem2 As Decimal
            Dim decPerutk2, decAmaun2, decVirMasuk2, decVirKeluar2, decBljytd2, decTngytd2, decPemLulus2 As Decimal

            strSql = "select mk01_perutk, mk01_virmasuk, mk01_virkeluar, mk01_bljytd, mk01_tngytd, mk01_pemlulus from mk01_vottahun where mk01_tahun='" & strTahun & "' and kodkw='" & strKodKW & "' and KodKO = '" & strKodKO & "' " &
    "And kodptj='" & strkodPTj & "' and kodkp= '" & strKodKP & "'  and kodvot='" & strKodVotAm & "'"
            Dim ds3 As New DataSet
            ds3 = dbconn.fSelectCommand(strSql)

            If Not ds3 Is Nothing Then
                If ds3.Tables(0).Rows.Count > 0 Then

                    decPerutk2 = CDec(ds3.Tables(0).Rows(0)("mk01_perutk").ToString)
                    decAmaun2 = decAmaun
                    decVirMasuk2 = CDec(ds3.Tables(0).Rows(0)("mk01_virmasuk").ToString)
                    decVirKeluar2 = CDec(ds3.Tables(0).Rows(0)("mk01_virkeluar").ToString)
                    decBljytd2 = CDec(ds3.Tables(0).Rows(0)("mk01_bljytd").ToString)
                    decTngytd2 = CDec(ds3.Tables(0).Rows(0)("mk01_tngytd").ToString)
                    decPemLulus2 = CDec(ds3.Tables(0).Rows(0)("mk01_pemlulus").ToString)

                    decNettJum2 = decPerutk2 - decAmaun2 + decVirMasuk2 - decVirKeluar2
                    decBakiSms2 = decNettJum2 - decBljytd2
                    decBakiPerutk2 = decNettJum2 - decBljytd2 - decTngytd2
                    decBakiSlpsPem2 = decNettJum2 - decPemLulus2


                    strSql = "UPDATE mk01_vottahun SET MK01_Perutk=@Perutk,MK01_BakiSms=@BakiSms,MK01_BakiPerutk=@BakiPerutk,MK01_BakiSlpsPemLulus=@BakiSlpsPemLulus " &
        "WHERE KodKw=@KodKW AND KodKO = @KodKO  AND KodPTJ=@KodPTj AND KodKP = @KodKP  AND KodVot=@KodVot  AND MK01_Tahun=@Tahun"
                    Dim paramSql3() As SqlParameter = {
                        New SqlParameter("@Perutk", decNettJum2),
                        New SqlParameter("@BakiSms", decBakiSms2),
                        New SqlParameter("@BakiPerutk", decBakiPerutk2),
                        New SqlParameter("@BakiSlpsPemLulus", decBakiSlpsPem2),
                        New SqlParameter("@KodKW", strKodKW),
                        New SqlParameter("@KodKO", strKodKO),
                        New SqlParameter("@KodPTj", strkodPTj),
                         New SqlParameter("@KodKP", strKodKP),
                          New SqlParameter("@KodVot", strKodVotAm),
                          New SqlParameter("@Tahun", strTahun)
                        }

                    If Not dbconn.fUpdateCommand(strSql, paramSql3) > 0 Then
                        blnSuccess = False
                        Exit Try
                    End If

                End If
            End If

            'POSTING KE LEJAR
            strSql = "INSERT INTO mk06_transaksi(mk06_tkhtran,mk06_rujukan,mk06_nodok,mk06_butiran,koddok, " &
            "mk06_kodap,mk06_bil,kodkw,KodKO,kodptj,KodKP,kodvot,kodjen,kodjenlanjut,mk06_debit,mk06_kredit,mk06_bulan,mk06_tahun,mk06_status) " &
            "VALUES (@TkhTran,@Rujukan,@NoDok,@Butiran,@KodDok,@KodAP,@Bil,@KodKW,@KodKO,@KodPTj,@KodKP,@KodVotSbg,@KodJen,@KodJenLanjut,@Debit,@Kredit,@Bulan,@Tahun,@Status)"

            Dim paramSql4() As SqlParameter = {
                New SqlParameter("@TkhTran", strTarikh),
                New SqlParameter("@Rujukan", strIndObjSbg),
                New SqlParameter("@NoDok", strIndObjSbg),
                New SqlParameter("@Butiran", strButiran),
                New SqlParameter("@KodDok", "BJTKURANG"),
                New SqlParameter("@KodAP", "-"),
                New SqlParameter("@Bil", 1),
                New SqlParameter("@KodKW", strKodKW),
                New SqlParameter("@KodKO", strKodKO),
                New SqlParameter("@KodPTj", strkodPTj),
                New SqlParameter("@KodKP", strKodKP),
                New SqlParameter("@KodVotSbg", strKodVotSbg),
                New SqlParameter("@KodJen", strKodJen),
                New SqlParameter("@KodJenLanjut", strKodJenLanjut),
                New SqlParameter("@Debit", decAmaun),
                New SqlParameter("@Kredit", 0),
                New SqlParameter("@Bulan", intBulan),
                New SqlParameter("@Tahun", strTahun),
                New SqlParameter("@Status", 0)
            }


            If Not dbconn.fInsertCommand(strSql, paramSql4) > 0 Then
                blnSuccess = False
                Exit Try
            End If

            'KEMAS KINI BAJET UNI
            Dim decKurangan, decJumBesar As Decimal

            strSql = "select BG03_Kurangan , BG03_JumBesar  from BG03_BjtUniv where bg03_tahun = '" & strTahun & "'"
            Dim ds4 As New DataSet
            ds4 = dbconn.fSelectCommand(strSql)

            If Not ds4 Is Nothing Then
                If ds4.Tables(0).Rows.Count > 0 Then
                    decKurangan = CDec(ds4.Tables(0).Rows(0)("BG03_Kurangan").ToString)
                    decJumBesar = CDec(ds4.Tables(0).Rows(0)("BG03_JumBesar").ToString)
                End If
            End If

            decKurangan = decKurangan + decAmaun
            decJumBesar = decJumBesar - decAmaun

            strSql = "UPDATE BG03_BjtUniv  SET BG03_Kurangan=@JumKG,BG03_JumBesar=@JumBesar WHERE BG03_Tahun=@Tahun"
            Dim paramSql5() As SqlParameter = {
                New SqlParameter("@JumKG", decKurangan),
                New SqlParameter("@JumBesar", decJumBesar),
                New SqlParameter("@Tahun", strTahun)
                }

            If Not dbconn.fUpdateCommand(strSql, paramSql5) > 0 Then
                blnSuccess = False
                Exit Try
            End If

        Catch ex As Exception
            blnSuccess = False
        End Try

        If blnSuccess = True Then
            dbconn.sConnCommitTrans()
            Return True

        ElseIf blnSuccess = False Then
            dbconn.sConnRollbackTrans()
            Return False
        End If

    End Function

    Private Sub fReset()
        Try
            lblJenis.Text = ""
            lblKodJenis.Text = ""
            txtTarikh.Text = ""
            txtKodKW.Text = ""
            txtKw.Text = ""
            txtKodKo.Text = ""
            txtKo.Text = ""
            txtKodPTj.Text = ""
            txtPTj.Text = ""
            txtKodKP.Text = ""
            txtKP.Text = ""
            txtKodObjAm.Text = ""
            txtObjAm.Text = ""
            txtKodObjSbg.Text = ""
            txtObjSbg.Text = ""
            txtBaki.Text = ""
            txtButiran.Text = ""
            txtAmaun.Text = ""
            txtUlasan.Text = ""

        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvSenarai_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvSenarai.Sorting
        Try

            Dim sortingDirection As String = String.Empty
            If direction = SortDirection.Ascending Then
                direction = SortDirection.Descending
                sortingDirection = "Desc"
            Else
                direction = SortDirection.Ascending
                sortingDirection = "Asc"
            End If

            Dim dt As New DataTable
            dt = CType(ViewState("dtSenarai"), DataTable)

            Dim sortedView As New DataView(dt)
            sortedView.Sort = Convert.ToString(e.SortExpression + " ") & sortingDirection
            Session("SortedView") = sortedView
            gvSenarai.DataSource = sortedView
            gvSenarai.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub lbtnLulus_Click(sender As Object, e As EventArgs) Handles lbtnLulus.Click
        Dim blnSuccess As Boolean = True
        Dim dbconn As New DBKewConn

        Try
            Dim strKodJen, strKodJenLanjut As String
            Dim strIndObjSbg As String = hidObjSbg.Value

            Dim strKodVotAm As String = Trim(txtKodObjAm.Text.TrimEnd)
            Dim strKodVotSbg As String = Trim(txtKodObjSbg.Text.TrimEnd)

            Dim strTarikh As String = Now.ToString("yyyy-MM-dd")
            Dim strNoStaff As String = Session("ssusrID")
            Dim strUlasan As String = Trim(txtUlasan.Text.TrimEnd)

            Dim strSql As String
            strSql = "select kodjen,kodjenlanjut from mk_vot where kodvot='" & strKodVotSbg & "'"

            Dim ds As New DataSet
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strKodJen = ds.Tables(0).Rows(0)("kodjen").ToString
                    strKodJenLanjut = ds.Tables(0).Rows(0)("kodjenlanjut").ToString
                End If
            End If

            Dim strKodJenis As String
            strKodJenis = Trim(lblKodJenis.Text.TrimEnd)

            If strKodJenis = "TB" Then
                    'LULUS
                    If Not fLulusTB(strKodJen, strKodJenLanjut, strIndObjSbg, strKodVotAm, strKodVotSbg, strTarikh, strNoStaff, strUlasan) Then
                        blnSuccess = False
                    End If

                ElseIf strKodJenis = "KG" Then
                    If Not fLulusKG(strKodJen, strKodJenLanjut, strIndObjSbg, strKodVotAm, strKodVotSbg, strTarikh, strNoStaff, strUlasan) Then
                        blnSuccess = False
                    End If
                End If

        Catch ex As Exception
            blnSuccess = False
        End Try

        If blnSuccess Then
            fGlobalAlert("Maklumat telah disimpan!", Me.Page, Me.[GetType]())
            fReset()
            fBindGvSenarai()
            divLst.Visible = True
            divDt.Visible = False
        Else
            fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
        End If
    End Sub

    Private Function fXLulus(ByVal strTarikh As String, ByVal strNoStaff As String, ByVal strUlasan As String, ByVal strIndObjSbg As String) As Boolean

        Dim dbconn As New DBKewConn
        Dim strSql As String
        Try
            'KEMAS KINI TABLE BG06_AGIHOBJSBG
            strSql = "update BG07_AgihObjSbg set bg07_statlulus = @StatLulus, bg07_tkhlulus = @TkhLulus , bg07_ulasan = @Ulasan, bg07_nostaflulus = @NoStaff where bg07_indobjsbg = @IndObjSbg"
            Dim paramSql() As SqlParameter = {
                New SqlParameter("@StatLulus", 0),
                New SqlParameter("@TkhLulus", strTarikh),
                New SqlParameter("@NoStaff", strNoStaff),
                New SqlParameter("@Ulasan", strUlasan),
                New SqlParameter("@IndObjSbg", strIndObjSbg)
                }

            dbconn.sConnBeginTrans()
            If dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                dbconn.sConnCommitTrans()
                Return True
            Else
                dbconn.sConnRollbackTrans()
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub lbtnKembali_Click(sender As Object, e As EventArgs) Handles lbtnKembali.Click
        divLst.Visible = True
        divDt.Visible = False
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

    Private Function fGetRec(strSql) As DataSet
        Dim dbconn As New DBKewConn

        Try
            Dim ds As New DataSet
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Return ds
                Else
                    Return Nothing
                End If
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Private Sub lbtnXLulus_Click(sender As Object, e As EventArgs) Handles lbtnXLulus.Click

        Dim strUlasan As String = Trim(txtUlasan.Text.TrimEnd)
        Dim strTarikh As String = Now.ToString("yyyy-MM-dd")
        Dim strNoStaff As String = Session("ssusrID")

        Dim strIndObjSbg As String = hidObjSbg.Value

        If fXLulus(strTarikh, strNoStaff, strUlasan, strIndObjSbg) Then
            fGlobalAlert("Maklumat telah disimpan!", Me.Page, Me.[GetType]())
            fReset()
            fBindGvSenarai()
            divLst.Visible = True
            divDt.Visible = False
        Else
            fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
        End If


    End Sub
End Class