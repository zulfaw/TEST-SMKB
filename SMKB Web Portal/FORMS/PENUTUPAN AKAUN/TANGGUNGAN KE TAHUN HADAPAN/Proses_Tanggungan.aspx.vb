Imports System.Data.SqlClient
Imports System.Threading

Public Class Proses_Tanggungan
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim script As String = "$(document).ready(function () { $('[id*=btnSubmit]').click(); });"
                ClientScript.RegisterStartupScript(Me.GetType(), "load", script, True)

                ViewState("NoStaf") = Session("ssusrID")
                ViewState("KodPTj") = Session("ssusrKodPTj")

                fBindDdlPTj()
                fBindDdlTahun()

                sClearGvLstPT()
                sClearGvPTProses()

                'If ViewState("dtProses") Is Nothing Then
                '    ViewState("dtProses") = fSetDtProses()
                'End If

                If ViewState("KodPTj") = "050000" Then
                    lblTitle1.Text = "Senarai Inden"
                    gvLstPT.HeaderRow.Cells(2).Text = "No. Inden"
                    gvProses.HeaderRow.Cells(2).Text = "No. Inden"
                    lblTitle2.Text = "Senarai Inden dibawa ke tahun "
                    fBindDdlCarian(ViewState("KodPTj"))
                Else
                    lblTitle1.Text = "Senarai Pesanan Tempatan"
                    gvLstPT.HeaderRow.Cells(2).Text = "No. PT"
                    gvProses.HeaderRow.Cells(2).Text = "No. PT"
                    lblTitle2.Text = "Senarai PT dibawa ke tahun "
                    fBindDdlCarian(ViewState("KodPTj"))
                End If


                sHapusTempCF()

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindDdlCarian(strKodPTj)
        Try
            Dim items As ListItem()
            If strKodPTj = "050000" Then
                items =
                {New ListItem("- KESELURUHAN -", "0"),
                New ListItem("No. Inden", "1"),
                 New ListItem("Nama Syarikat", "2"),
                  New ListItem("PTj", "3")}
            Else
                items =
                {New ListItem("- KESELURUHAN -", "0"),
                New ListItem("No. PT", "1"),
                 New ListItem("Nama Syarikat", "2"),
                  New ListItem("PTj", "3")}
            End If

            ddlCarian.Items.AddRange(items)

            ddlCarian.DataBind()
        Catch ex As Exception

        End Try

    End Sub
    Private Sub fBindDdlPTj()
        Try
            Dim ds As New DataSet
            ds = fLoadPTj()
            ddlPTj.DataSource = ds
            ddlPTj.DataTextField = "KodPTJ"
            ddlPTj.DataValueField = "KodPTJ"
            ddlPTj.DataBind()

            ddlPTj.Items.Insert(0, New ListItem("- SILA PILIH PTj-", 0))
            ddlPTj.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub
    Private Sub fBindDdlTahun()
        Try
            Dim ds As New DataSet
            ds = fPopTahun(4)
            ddlTahun.DataSource = ds
            ddlTahun.DataTextField = "Tahun"
            ddlTahun.DataValueField = "Val"
            ddlTahun.DataBind()

            ddlTahun.SelectedValue = Now.Year

            ddlTahun.Items.Insert(0, New ListItem("- SILA PILIH TAHUN -", 0))
            ddlTahun.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlCarian_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCarian.SelectedIndexChanged
        If ddlCarian.SelectedValue = 0 Then
            txtCarian.Enabled = False
            txtCarian.Visible = True
            ddlPTj.Visible = False
        ElseIf ddlCarian.SelectedValue = 1 Then
            txtCarian.Enabled = True
            txtCarian.Visible = True
            ddlPTj.Visible = False
        ElseIf ddlCarian.SelectedValue = 2 Then
            txtCarian.Enabled = True
            txtCarian.Visible = True
            ddlPTj.Visible = False
        ElseIf ddlCarian.SelectedValue = 3 Then
            txtCarian.Visible = False
            txtCarian.Visible = False
            ddlPTj.Visible = True
        End If
        txtCarian.Text = ""
    End Sub

    Private Sub sLoadPT()
        Try
            Dim intRec As Integer = 0
            Dim dt As New DataTable

            sClearGvLstPT()

            dt.Columns.AddRange(New DataColumn() {
                                New DataColumn("PO19_NoPt", GetType(String)),
                                New DataColumn("ROC01_IDSya", GetType(String)),
                                New DataColumn("ROC01_NamaSya", GetType(String)),
                                New DataColumn("PO02_JumSebenar", GetType(Decimal)),
                                New DataColumn("PO02_JumBlmByr", GetType(Decimal))
                                })

            Dim strFilter As String
            Dim strCond As String = ""
            If ddlCarian.SelectedValue = 1 Then
                strCond = Trim(txtCarian.Text.TrimEnd)
                strFilter = " AND a.PO19_NoPt like '%' + @Filter + '%'"
            ElseIf ddlCarian.SelectedValue = 2 Then
                strCond = Trim(txtCarian.Text.TrimEnd)
                strFilter = " AND b.ROC01_NamaSya like '%' + @Filter + '%'"
            ElseIf ddlCarian.SelectedValue = 3 Then
                strCond = Trim(ddlPTj.SelectedValue.TrimEnd)
                strFilter = " AND substring(a.PO01_NoMohon,3,6) like '%' + @Filter + '%'"
            End If


            Dim strTahun As String = Trim(ddlTahun.SelectedValue.TrimEnd)

            '            Dim strSql As String = "SELECT a.PO19_NoPt,a.ROC01_IDSya,b.ROC01_NamaSya, isnull(a.PO19_JumSebenar,0) as PO02_JumSebenar,isnull(a.PO19_JumBlmByr,0) as PO02_JumBlmByr,  a.PO19_FlagAdj as PO02_FlagAdj From PO19_Pt a,ROC01_Syarikat b WHERE a.ROC01_IDSya = b.ROC01_IDSya AND (a.PO19_JumBlmByr <> 0 ) and (a.po19_statustng <> 1) and a.PO19_NoPt NOT IN (SELECT MK06_NoDok From MK06_Transaksi WHERE     (KodDok = 'CF_LO') AND (LEFT(MK06_Rujukan, 21) NOT IN (SELECT     LEFT(MK06_Rujukan, 21) AS Expr1 
            'From MK06_Transaksi  
            'WHERE (KodDok = 'CF_LO') AND (MK06_Rujukan LIKE '%-R'))) AND (SUBSTRING(MK06_Rujukan, 5, 4) = '')) AND (a.PO02_StatusDok in ('12','13')) AND a.PO19_Status = 'A' Order by a.PO19_NoPt"

            Dim strSql As String = "SELECT a.PO19_NoPt,a.ROC01_IDSya,b.ROC01_NamaSya, isnull(a.PO19_JumSebenar,0) as PO02_JumSebenar,isnull(a.PO19_JumBlmByr,0) as PO02_JumBlmByr,  a.PO19_FlagAdj as PO02_FlagAdj From PO19_Pt a,ROC01_Syarikat b WHERE a.ROC01_IDSya = b.ROC01_IDSya AND (a.PO19_JumBlmByr <> 0 ) and (a.po19_statustng <> 1) and a.PO19_NoPt NOT IN (SELECT MK06_NoDok From MK06_Transaksi WHERE     (KodDok = 'CF_LO') AND (LEFT(MK06_Rujukan, 21) NOT IN (SELECT     LEFT(MK06_Rujukan, 21) AS Expr1 
From MK06_Transaksi  
WHERE (KodDok = @KodDok) AND (MK06_Rujukan LIKE @Rujukan))) AND (SUBSTRING(MK06_Rujukan, 5, 4) = @Tahun)) 
AND (a.PO02_StatusDok in (@StatDok1,@StatDok2)) AND a.PO19_Status = @Status " & strFilter & " Order by a.PO19_NoPt"


            Dim paramSql() As SqlParameter =
                {
                New SqlParameter("@KodDok", "CF_LO"),
                New SqlParameter("@Rujukan", "%-R"),
                New SqlParameter("@Tahun", strTahun),
                New SqlParameter("@StatDok1", 12),
                New SqlParameter("@StatDok2", 13),
                New SqlParameter("@Status", "A"),
                New SqlParameter("@Filter", strCond)
                }

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql, paramSql)

            Dim strNoPT, strIdSya, strNmSya, strJum, strJumBaki As String
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        strNoPT = ds.Tables(0).Rows(i)("PO19_NoPt")
                        strIdSya = ds.Tables(0).Rows(i)("ROC01_IDSya")
                        strNmSya = ds.Tables(0).Rows(i)("ROC01_NamaSya")
                        strJum = ds.Tables(0).Rows(i)("PO02_JumSebenar")
                        strJumBaki = ds.Tables(0).Rows(i)("PO02_JumBlmByr")

                        dt.Rows.Add(strNoPT, strIdSya, strNmSya, strJum, strJumBaki)
                    Next

                    gvLstPT.DataSource = dt
                    gvLstPT.DataBind()
                    ViewState("dtLstPT") = dt
                    intRec = ds.Tables(0).Rows.Count
                End If
            End If

            lblJumRekod.InnerText = intRec

        Catch ex As Exception

        End Try
    End Sub

    Private Sub sClearGvLstPT()
        gvLstPT.DataSource = New List(Of String)
        gvLstPT.DataBind()
    End Sub

    Private Sub sClearGvPTProses()
        gvProses.DataSource = New List(Of String)
        gvProses.DataBind()
        lblJumPros.InnerText = 0
    End Sub

    Private Sub gvLstPT_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvLstPT.PageIndexChanging
        Try
            gvLstPT.PageIndex = e.NewPageIndex
            If ViewState("dtLstPT") IsNot Nothing Then
                gvLstPT.DataSource = ViewState("dtLstPT")
                gvLstPT.DataBind()

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub lbtnCari_Click(sender As Object, e As EventArgs) Handles lbtnCari.Click

        If ddlCarian.SelectedValue <> 0 Then
            If ddlCarian.SelectedValue = 1 Then
                If txtCarian.Text = "" Then
                    fGlobalAlert("Masukkan No. PT!", Me.Page, Me.[GetType]())
                    Exit Sub
                End If
            ElseIf ddlCarian.SelectedValue = 2 Then
                If txtCarian.Text = "" Then
                    fGlobalAlert("Masukkan Nama Syarikat!", Me.Page, Me.[GetType]())
                    Exit Sub
                End If
            ElseIf ddlCarian.SelectedValue = 3 Then
                If ddlPTj.SelectedValue = 0 Then
                    fGlobalAlert("Pilih PTj!", Me.Page, Me.[GetType]())
                    Exit Sub
                End If
            End If
        End If

        ddlTahun_SelectedIndexChanged(ddlTahun, EventArgs.Empty)
        'sLoadPT()
    End Sub

    Private Sub lBtnTmbh_Click(sender As Object, e As EventArgs) Handles lBtnTmbh.Click

        Try
            Dim strThnCF As String
            'Dim strDate As String = ddlTahun.SelectedValue
            'Dim dtThn As Date = CDate(strDate.ToString)
            If ddlTahun.SelectedIndex = 0 Then
                fGlobalAlert("Pilih Tahun!", Me.Page, Me.[GetType]())
                Exit Sub
            Else
                strThnCF = CInt(ddlTahun.SelectedValue) + 1
            End If



            lblTahun.Text = strThnCF

            For Each gvRow As GridViewRow In gvLstPT.Rows
                Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("cbSelect"), CheckBox)
                If chkSel.Checked = True Then

                    Dim dt As New DataTable
                    dt = TryCast(ViewState("dtProses"), DataTable)

                    Dim strNoPT As String = TryCast(gvRow.FindControl("lblNoPT"), Label).Text
                    Dim strIdSya As String = TryCast(gvRow.FindControl("lblIdSya"), Label).Text
                    Dim strNmSya As String = Server.HtmlDecode(TryCast(gvRow.FindControl("lblNmSya"), Label).Text)
                    Dim strJum As String = TryCast(gvRow.FindControl("lblJumlah"), Label).Text
                    'Dim strJumBaki As String = TryCast(gvRow.FindControl("lblJumBaki"), Label).Text
                    Dim strRujukan As String = "R"

                    Dim foundRows As DataRow()
                    foundRows = dt.Select("PO19_NoPt='" & strNoPT & "'")

                    If foundRows.Count = 0 Then
                        dt.Rows.Add(strNoPT, strIdSya, strNmSya, strJum, strRujukan)

                        chkSel.Enabled = False
                        gvRow.ForeColor = Drawing.Color.Blue
                    End If

                    gvProses.DataSource = dt
                    gvProses.DataBind()

                    lblJumPros.InnerText = dt.Rows.Count

                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Function fSetDtProses() As DataTable
        'Dim strSql = "Select  AR01_IdBil, AR11_ID, AR11_Path, AR11_NamaDok,AR11_ContentType, AR11_Status from AR11_Lampiran where AR01_NoBilSem = '" & strNoInvSem & "' and AR11_Status = 1 order by AR11_Bil "

        Try
            Dim dt As DataTable = New DataTable()
            Dim dr As DataRow = Nothing
            dt.Columns.AddRange(New DataColumn() {
                                New DataColumn("PO19_NoPt", GetType(String)),
                                New DataColumn("ROC01_IDSya", GetType(String)),
                                New DataColumn("ROC01_NamaSya", GetType(String)),
                                New DataColumn("PO02_JumSebenar", GetType(Decimal)),
                                New DataColumn("MK06_Rujukan", GetType(String))
                                })

            Return dt

        Catch ex As Exception

        End Try

    End Function

    Private Sub gvLstPT_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvLstPT.RowDataBound

        'Try

        '    For Each gvRow As GridViewRow In gvLstPT.Rows
        '        Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("cbSelect"), CheckBox)


        '        Dim dt As New DataTable
        '        dt = TryCast(ViewState("dtProses"), DataTable)
        '        If Not dt Is Nothing Then
        '            If dt.Rows.Count > 0 Then
        '                Dim strNoPT As String = TryCast(gvRow.FindControl("lblNoPT"), Label).Text

        '                Dim foundRows As DataRow()
        '                foundRows = dt.Select("PO19_NoPt='" & strNoPT & "'")

        '                If foundRows.Count > 0 Then
        '                    chkSel.Enabled = False
        '                    gvRow.ForeColor = Drawing.Color.Blue
        '                End If
        '            End If
        '        End If
        '    Next
        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub gvProses_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvProses.PageIndexChanging
        Try
            gvProses.PageIndex = e.NewPageIndex
            If ViewState("dtProses") IsNot Nothing Then
                gvProses.DataSource = ViewState("dtProses")
                gvProses.DataBind()

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub lBtnHapus_Click(sender As Object, e As EventArgs) Handles lBtnHapus.Click
        Dim blnSuccess As Boolean = True
        Dim dbconn As New DBKewConn
        Dim dt As New DataTable
        Try

            dbconn.sConnBeginTrans()

            For Each gvRow As GridViewRow In gvProses.Rows
                Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("cbSelect"), CheckBox)
                If chkSel.Checked = True Then
                    Dim strNoPT As String = TryCast(gvRow.FindControl("lblNoPT"), Label).Text
                    Dim strRujukan As String = TryCast(gvRow.FindControl("lblRujukan"), Label).Text

                    'insert rekod ke dalam table tempCF
                    Dim strSql As String = "INSERT INTO tempCF (NoDok,Rujukan) VALUES (@NoDok,@Rujukan)"
                    Dim paramSql() As SqlParameter =
                                            {
                                                New SqlParameter("@NoDok", strNoPT),
                                                New SqlParameter("@Rujukan", strRujukan)
                                            }

                    If Not dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                        blnSuccess = False
                        Exit Try
                    End If

                    'remove rekod dalam datatable 

                    dt = TryCast(ViewState("dtProses"), DataTable)
                    Dim foundRows As DataRow()
                    foundRows = dt.Select("PO19_NoPt='" & strNoPT & "'")
                    If foundRows.Count > 0 Then
                        foundRows(0).Delete()

                        For Each gvRow2 As GridViewRow In gvLstPT.Rows
                            Dim chkSel2 As CheckBox = DirectCast(gvRow2.FindControl("cbSelect"), CheckBox)
                            Dim strNoPT2 As String = TryCast(gvRow2.FindControl("lblNoPT"), Label).Text

                            If strNoPT = strNoPT2 Then
                                chkSel2.Enabled = True
                                chkSel2.Checked = False
                                gvRow2.ForeColor = Drawing.Color.Black
                            End If
                        Next
                    End If

                End If
            Next


        Catch ex As Exception
            blnSuccess = False
        End Try

        If blnSuccess Then
            dbconn.sConnCommitTrans()

            gvProses.DataSource = dt
            gvProses.DataBind()
            lblJumPros.InnerText = dt.Rows.Count
        Else
            dbconn.sConnRollbackTrans()
        End If

    End Sub

    Private Sub lnkBtnProses_Click(sender As Object, e As EventArgs) Handles lnkBtnProses.Click
        Try
            If fSimpan() Then
                sHapusTempCF()
                fGlobalAlert("proses tanggungan ke tahun " & lblTitle2.Text & " selesai!", Me.Page, Me.[GetType]())

                ddlTahun_SelectedIndexChanged(ddlTahun, EventArgs.Empty)

            Else
                fGlobalAlert("Proses gagal", Me.Page, Me.[GetType]())
            End If
        Catch ex As Exception

        End Try


    End Sub

    Private Function fSimpan() As Boolean
        Dim blnSuccess As Boolean = True
        Dim strSql As String
        Dim dbconn As New DBKewConn
        Dim strKodDok As String

        Try

            Dim strKodPTj As String = ViewState("KodPTj")
            Dim strThn As String = ddlTahun.SelectedValue
            Dim strThnCF As String = CInt(strThn) + 1
            Dim paramSql() As SqlParameter
            Dim strNoStaf As String = ViewState("NoStaf")

            If strKodPTj = "050000" Then
                strKodDok = "CF_INDEN"
            Else
                strKodDok = "CF_LO"
            End If


            For Each gvRow As GridViewRow In gvProses.Rows

                Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("cbSelect"), CheckBox)
                If chkSel.Checked = True Then

                    Dim strNoPT As String = TryCast(gvRow.FindControl("lblNoPT"), Label).Text

                    strSql = "select isnull(max(substring(mk06_rujukan,len(mk06_rujukan),1)),0) as BilMax from mk06_transaksi
Where KodDok = '" & strKodDok & "' and MK06_NoDok = '" & strNoPT & "'
And MK06_rujukan Not like '%-R'"

                    Dim ds As New DataSet
                    ds = dbconn.fSelectCommand(strSql)
                    Dim intBilMax As Integer
                    If Not ds Is Nothing Then
                        If ds.Tables(0).Rows.Count > 0 Then
                            intBilMax = ds.Tables(0).Rows(0)("BilMax")
                            If intBilMax <> 0 Then
                                intBilMax = intBilMax + 1
                            Else
                                intBilMax = 1
                            End If
                        End If
                    End If

                    'INSERT INTO TABLE MK06_TRANSAKSI
                    strSql = "Select KodKw, KodKO,KodPTJ, KodKP,KodVot,Sum(ISNULL(PO19_JumTanpaGST,PO19_JumKadar)) As PO19_JumBlmByr
From PO19_PtDt
where PO19_NoPt = '" & strNoPT & "'
Group by KodKw, KodKO,KodPTJ, KodKP,KodVot"

                    Dim ds1 As New DataSet
                    ds1 = dbconn.fSelectCommand(strSql)

                    Dim intBil
                    Dim decJumlahCF As Decimal
                    If Not ds1 Is Nothing Then
                        If ds1.Tables(0).Rows.Count > 0 Then
                            For i As Integer = 0 To ds1.Tables(0).Rows.Count - 1
                                Dim strTkhCF = strThnCF & "-01-01"
                                Dim strRujukan As String = strKodDok.Replace("_", "") & strThn & strNoPT & intBilMax
                                Dim strNoDok = strNoPT
                                Dim strButiran = "TANGGUNGAN BAWA KE HADAPAN " & strThnCF
                                Dim strKodAP = "-"
                                intBil = i + 1
                                Dim strKW = ds1.Tables(0).Rows(i)("KodKw")
                                Dim strKO = ds1.Tables(0).Rows(i)("KodKO")
                                Dim strPTJ = ds1.Tables(0).Rows(i)("KodPtj")
                                Dim strKP = ds1.Tables(0).Rows(i)("KodKP")
                                Dim strVot As String = ds1.Tables(0).Rows(i)("KodVot")
                                Dim strKodJen, strKodJenLanjut As String
                                fGetKodJen(strVot, strKodJen, strKodJenLanjut)
                                Dim PO19_JumBlmByr As Decimal = ds1.Tables(0).Rows(i)("PO19_JumBlmByr") ' Format(rs3("PO02_JumBlmByr"), "############.00")
                                Dim db As Decimal = PO19_JumBlmByr
                                'INSERT INTO mk06_transaksi (mk06_tkhtran,mk06_rujukan,mk06_nodok,mk06_butiran,koddok,mk06_kodap,mk06_bil,kodkw,kodptj,kodvot,kodakt,kodjen,kodjenlanjut,mk06_debit,mk06_kredit,mk06_bulan,mk06_tahun,mk06_status) 
                                'VALUES('01/01/2020','CFLO2019PO00134510181','PO0013451018','TANGGUNGAN BAWA KE HADAPAN 2020','CF_LO','-','1','01','010000','27101','00','E','-','112.00','0','1','2020','0')

                                strSql = "INSERT INTO mk06_transaksi (mk06_tkhtran,mk06_rujukan,mk06_nodok,mk06_butiran,koddok,mk06_kodap,mk06_bil,kodkw,KodOperasi ,kodptj,KodProjek ,kodvot, kodjen,kodjenlanjut,mk06_debit,mk06_kredit,mk06_bulan,mk06_tahun,mk06_status) 
VALUES (@TkhCF,@Rujukan,@NoDok,@Tujuan,@KodDok,@KodAP,@Bil,@kodKw,@KodKO,@KodPTj,@KodKP,@KodVot,@kodJen,@KodJenLanjut,@Dt,@Kt,@BulanTran,@Tahun,@Status)"

                                paramSql =
                        {
                         New SqlParameter("@TkhCF", strTkhCF),
                         New SqlParameter("@Rujukan", strRujukan),
                         New SqlParameter("@NoDok", strNoDok),
                         New SqlParameter("@Tujuan", "TANGGUNGAN BAWA KE HADAPAN 2020"),
                         New SqlParameter("@KodDok", strKodDok),
                         New SqlParameter("@KodAP", strKodAP),
                         New SqlParameter("@Bil", intBil),
                         New SqlParameter("@kodKw", strKW),
                         New SqlParameter("@KodKO", strKO),
                         New SqlParameter("@KodPTJ", strPTJ),
                         New SqlParameter("@KodKP", strKP),
                         New SqlParameter("@KodVot", strVot),
                         New SqlParameter("@kodJen", strKodJen),
                         New SqlParameter("@KodJenLanjut", strKodJenLanjut),
                         New SqlParameter("@Dt", db),
                         New SqlParameter("@Kt", 0),
                         New SqlParameter("@BulanTran", 1),
                         New SqlParameter("@Tahun", strThnCF),
                         New SqlParameter("@Status", 0)
                        }
                                dbconn.sConnBeginTrans()
                                If Not dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                                    blnSuccess = False
                                    Exit Try
                                End If

                                decJumlahCF = decJumlahCF + db

                                'UPDATE MK01_VOTTAHUN H2
                                Dim VotH2 As String = strVot.Substring(0, 2) + "000"

                                strSql = "SELECT MK01_TngYtd,MK01_PemLulus,MK01_BakiPerutk,MK01_BakiTng,MK01_BakiSlpsPemLulus 
From MK01_VotTahun WHERE KodKw = '" & strKW & "' AND KodKO = '" & strKO & "' And KodPTJ = '" & strPTJ & "' AND KodKP = '" & strKP & "' AND KodVot = '" & VotH2 & "' AND MK01_Tahun = '" & Now.Year & "'"
                                Dim ds2 As New DataSet
                                ds2 = dbconn.fSelectCommand(strSql)

                                If Not ds2 Is Nothing Then
                                    If ds2.Tables(0).Rows.Count > 0 Then

                                        'rs1("MK01_TngYtd") = rs1("MK01_TngYtd") + Format(rs3("PO02_JumBlmByr"), "############.00")
                                        'rs1("MK01_PemLulus") = rs1("MK01_PemLulus") + Format(rs3("PO02_JumBlmByr"), "############.00")
                                        'rs1("MK01_BakiPerutk") = rs1("MK01_BakiPerutk") - Format(rs3("PO02_JumBlmByr"), "############.00")
                                        'rs1("MK01_BakiTng") = rs1("MK01_BakiTng") + Format(rs3("PO02_JumBlmByr"), "############.00")
                                        'rs1("MK01_BakiSlpsPemLulus") = rs1("MK01_BakiSlpsPemLulus") - Format(rs3("PO02_JumBlmByr"), "############.00")

                                        Dim MK01_TngYtd As Decimal = CDec(ds2.Tables(0).Rows(0)("MK01_TngYtd").ToString) + PO19_JumBlmByr
                                        Dim MK01_PemLulus As Decimal = CDec(ds2.Tables(0).Rows(0)("MK01_PemLulus").ToString) + PO19_JumBlmByr
                                        Dim MK01_BakiPerutk As Decimal = CDec(ds2.Tables(0).Rows(0)("MK01_BakiPerutk").ToString) - PO19_JumBlmByr
                                        Dim MK01_BakiTng As Decimal = CDec(ds2.Tables(0).Rows(0)("MK01_BakiTng").ToString) + PO19_JumBlmByr
                                        Dim MK01_BakiSlpsPemLulus As Decimal = CDec(ds2.Tables(0).Rows(0)("MK01_BakiSlpsPemLulus").ToString) - PO19_JumBlmByr


                                        strSql = "update MK01_VotTahun set MK01_TngYtd = @MK01_TngYtd, MK01_PemLulus = @MK01_PemLulus, MK01_BakiPerutk = @MK01_BakiPerutk, MK01_BakiTng = @MK01_BakiTng, MK01_BakiSlpsPemLulus = @MK01_BakiSlpsPemLulus 
WHERE KodKw = @KodKw AND KodKO = @KodKO And KodPTJ = @KodPTJ AND KodKP = @KodKP AND KodVot = @KodVot AND MK01_Tahun = @MK01_Tahun"
                                        paramSql =
                                                {
                                                    New SqlParameter("@MK01_TngYtd", MK01_TngYtd),
                                                    New SqlParameter("@MK01_PemLulus", MK01_PemLulus),
                                                    New SqlParameter("@MK01_BakiPerutk", MK01_BakiPerutk),
                                                    New SqlParameter("@MK01_BakiTng", MK01_BakiTng),
                                                    New SqlParameter("@MK01_BakiSlpsPemLulus", MK01_BakiSlpsPemLulus),
                                                    New SqlParameter("@KodKw", strKW),
                                                    New SqlParameter("@KodKO", strKO),
                                                    New SqlParameter("@KodPTJ", strPTJ),
                                                    New SqlParameter("@KodKP", strKP),
                                                    New SqlParameter("@KodVot", VotH2),
                                                    New SqlParameter("@MK01_Tahun", Now.Year)
                                                }

                                        If Not dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                                            blnSuccess = False
                                            Exit Try
                                        End If
                                    End If
                                End If



                                'UPDATE MK01_VOTTAHUN H1
                                Dim VotH1 As String = strVot.Substring(0, 1) + "0000"

                                strSql = "SELECT MK01_TngYtd,MK01_PemLulus,MK01_BakiPerutk,MK01_BakiTng,MK01_BakiSlpsPemLulus 
From MK01_VotTahun WHERE KodKw = '" & strKW & "' AND KodKO = '" & strKO & "' And KodPTJ = '" & strPTJ & "' AND KodKP = '" & strKP & "' AND KodVot = '" & VotH1 & "' AND MK01_Tahun = '" & Now.Year & "'"
                                Dim ds3 As New DataSet
                                ds3 = dbconn.fSelectCommand(strSql)

                                If Not ds3 Is Nothing Then
                                    If ds3.Tables(0).Rows.Count > 0 Then
                                        'rs1("MK01_TngYtd") = rs1("MK01_TngYtd") + Format(rs3("PO02_JumBlmByr"), "############.00")
                                        'rs1("MK01_PemLulus") = rs1("MK01_PemLulus") + Format(rs3("PO02_JumBlmByr"), "############.00")
                                        'rs1("MK01_BakiPerutk") = rs1("MK01_BakiPerutk") - Format(rs3("PO02_JumBlmByr"), "############.00")
                                        'rs1("MK01_BakiTng") = rs1("MK01_BakiTng") + Format(rs3("PO02_JumBlmByr"), "############.00")
                                        'rs1("MK01_BakiSlpsPemLulus") = rs1("MK01_BakiSlpsPemLulus") - Format(rs3("PO02_JumBlmByr"), "############.00")

                                        Dim MK01_TngYtd As Decimal = CDec(ds3.Tables(0).Rows(0)("MK01_TngYtd").ToString) + PO19_JumBlmByr
                                        Dim MK01_PemLulus As Decimal = CDec(ds3.Tables(0).Rows(0)("MK01_PemLulus").ToString) + PO19_JumBlmByr
                                        Dim MK01_BakiPerutk As Decimal = CDec(ds3.Tables(0).Rows(0)("MK01_BakiPerutk").ToString) - PO19_JumBlmByr
                                        Dim MK01_BakiTng As Decimal = CDec(ds3.Tables(0).Rows(0)("MK01_BakiTng").ToString) + PO19_JumBlmByr
                                        Dim MK01_BakiSlpsPemLulus As Decimal = CDec(ds3.Tables(0).Rows(0)("MK01_BakiSlpsPemLulus").ToString) - PO19_JumBlmByr

                                        strSql = "update MK01_VotTahun set MK01_TngYtd = @MK01_TngYtd, MK01_PemLulus = @MK01_PemLulus, MK01_BakiPerutk = @MK01_BakiPerutk, MK01_BakiTng = @MK01_BakiTng, MK01_BakiSlpsPemLulus = @MK01_BakiSlpsPemLulus 
WHERE KodKw = @KodKw AND KodKO = @KodKO And KodPTJ = @KodPTJ AND KodKP = @KodKP AND KodVot = @KodVot AND MK01_Tahun = @MK01_Tahun"
                                        paramSql =
                                                {
                                                    New SqlParameter("@MK01_TngYtd", MK01_TngYtd),
                                                    New SqlParameter("@MK01_PemLulus", MK01_PemLulus),
                                                    New SqlParameter("@MK01_BakiPerutk", MK01_BakiPerutk),
                                                    New SqlParameter("@MK01_BakiTng", MK01_BakiTng),
                                                    New SqlParameter("@MK01_BakiSlpsPemLulus", MK01_BakiSlpsPemLulus),
                                                    New SqlParameter("@KodKw", strKW),
                                                    New SqlParameter("@KodKO", strKO),
                                                    New SqlParameter("@KodPTJ", strPTJ),
                                                    New SqlParameter("@KodKP", strKP),
                                                    New SqlParameter("@KodVot", VotH1),
                                                    New SqlParameter("@MK01_Tahun", Now.Year)
                                                }

                                        If Not dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                                            blnSuccess = False
                                            Exit Try
                                        End If

                                    End If
                                End If


                                'Untuk GST
                                '                            '--- tambahan GST ---------
                                '                            'INSERT INTO MK06_TRANSAKSI
                                '                            strSql = "Select KwGST,KOGST,PTJGST,KPGST,VOTGST,Sum(PO19_JumGST) As JumBlmByr From PO19_PtDt  where PO19_NoPt = '" & strNoPT & "' Group by KwGST,KOGST,PTJGST,KPGST,VOTGST"
                                '                            Dim ds4 As New DataSet
                                '                            ds4 = dbconn.fSelectCommand(strSql)
                                '                            If Not ds4 Is Nothing Then
                                '                                If ds4.Tables(0).Rows.Count > 0 Then
                                '                                    For j As Integer = 0 To ds4.Tables(0).Rows.Count - 1
                                '                                        If Not IsDBNull(ds4.Tables(0).Rows(j)("JumBlmByr")) OrElse Not ds4.Tables(0).Rows(j)("JumBlmByr") = 0 Then
                                '                                            Dim decJumBlmByrGst As Decimal = CDec(ds4.Tables(0).Rows(j)("JumBlmByr").ToString)
                                '                                            Dim strTkhCFgst = strThnCF & "-01-01"
                                '                                            Dim strRujukanGST As String = strKodDok.Replace("_", "") & ddlTahun.SelectedValue & strNoPT & intBilMax

                                '                                            Dim strNoDokGST = strNoPT
                                '                                            Dim strButiranGST = "TANGGUNGAN BAWA KE HADAPAN " & strThnCF

                                '                                            Dim strKodAPGST = "-"
                                '                                            Dim intBilGST As Integer = j + 1
                                '                                            Dim strKWgst = ds4.Tables(0).Rows(i)("KwGST")
                                '                                            Dim strKOgst = ds4.Tables(0).Rows(i)("KOGST")
                                '                                            Dim strPTJgst = ds4.Tables(0).Rows(i)("PTJGST")
                                '                                            Dim strKPgst = ds4.Tables(0).Rows(i)("KPGST")
                                '                                            Dim strVotGST = ds4.Tables(0).Rows(i)("VOTGST")

                                '                                            fGetKodJen(strVotGST, strKodJen, strKodJenLanjut)

                                '                                            If IsDBNull(ds4.Tables(0).Rows(i)("JumBlmByr")) Then
                                '                                                db = 0
                                '                                            Else
                                '                                                db = CDec(ds4.Tables(0).Rows(i)("JumBlmByr"))
                                '                                            End If


                                '                                            strSql = "INSERT INTO mk06_transaksi
                                '(mk06_tkhtran,mk06_rujukan,mk06_nodok,mk06_butiran,koddok,mk06_kodap,mk06_bil,kodkw,KodOperasi ,kodptj,KodProjek ,kodvot,kodjen,kodjenlanjut,mk06_debit,mk06_kredit,mk06_bulan,mk06_tahun,mk06_status)
                                'VALUES (@TkhCF,@Rujukan,@NoDok,@Tujuan,@KodDok,@KodAP,@Bil, @kodKw, @KodKO,@KodPTJ, @KodKP,@KodVot,@kodJen, @KodJenLanjut, @Dt,@Kt, @Bulan, @Tahun,@Status)"

                                '                                            paramSql =
                                '                                                {
                                '                                                    New SqlParameter("@TkhCF", strTkhCFgst),
                                '                                                    New SqlParameter("@Rujukan", strRujukanGST),
                                '                                                    New SqlParameter("@NoDok", strNoDokGST),
                                '                                                    New SqlParameter("@Tujuan", strButiranGST),
                                '                                                    New SqlParameter("@KodDok", strKodDok),
                                '                                                    New SqlParameter("@KodAP", strKodAPGST),
                                '                                                    New SqlParameter("@Bil", intBilGST),
                                '                                                    New SqlParameter("@kodKw", strKWgst),
                                '                                                    New SqlParameter("@KodKO", strKOgst),
                                '                                                    New SqlParameter("@KodPTJ", strPTJgst),
                                '                                                    New SqlParameter("@KodKP", strKPgst),
                                '                                                    New SqlParameter("@KodVot", strVotGST),
                                '                                                    New SqlParameter("@kodJen", strKodJen),
                                '                                                    New SqlParameter("@KodJenLanjut", strKodJenLanjut),
                                '                                                    New SqlParameter("@Dt", db),
                                '                                                    New SqlParameter("@Kt", 0),
                                '                                                    New SqlParameter("@Bulan", 1),
                                '                                                    New SqlParameter("@Tahun", strThnCF),
                                '                                                    New SqlParameter("@Status", 0)
                                '                                                }

                                '                                            If Not dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                                '                                                blnSuccess = False
                                '                                                Exit Try
                                '                                            End If

                                '                                            decJumlahCF = decJumlahCF + db

                                '                                            'UPDATE MK01_VOTTAHUN H2
                                '                                            Dim VotGSTH2 As String = strVot.Substring(0, 2) + "000"
                                '                                            strSql = "SELECT MK01_TngYtd,MK01_PemLulus,MK01_BakiPerutk,MK01_BakiTng,MK01_BakiSlpsPemLulus
                                'From MK01_VotTahun WHERE KodKw = '" & strKWgst & "' and KodKO = '" & strKOgst & "'  And KodPTJ = '" & strPTJgst & "' and KodKP = '" & strKPgst & "' AND KodVot = '" & VotGSTH2 & "' AND MK01_Tahun = '" & Now.Year & "'"

                                '                                            Dim ds5 As New DataSet
                                '                                            ds5 = dbconn.fSelectCommand(strSql)

                                '                                            If Not ds5 Is Nothing Then
                                '                                                If ds5.Tables(0).Rows.Count > 0 Then

                                '                                                    'rs1("MK01_TngYtd") = rs1("MK01_TngYtd") + Format(rs3("JumBlmByr"), "############.00")
                                '                                                    'rs1("MK01_PemLulus") = rs1("MK01_PemLulus") + Format(rs3("JumBlmByr"), "############.00")
                                '                                                    'rs1("MK01_BakiPerutk") = rs1("MK01_BakiPerutk") - Format(rs3("JumBlmByr"), "############.00")
                                '                                                    'rs1("MK01_BakiTng") = rs1("MK01_BakiTng") + Format(rs3("JumBlmByr"), "############.00")
                                '                                                    'rs1("MK01_BakiSlpsPemLulus") = rs1("MK01_BakiSlpsPemLulus") - Format(rs3("JumBlmByr"), "############.00")

                                '                                                    Dim MK01_TngYtd As Decimal = CDec(ds5.Tables(0).Rows(0)("MK01_TngYtd")) + decJumBlmByrGst
                                '                                                    Dim MK01_PemLulus As Decimal = CDec(ds5.Tables(0).Rows(0)("MK01_PemLulus")) + decJumBlmByrGst
                                '                                                    Dim MK01_BakiPerutk As Decimal = CDec(ds5.Tables(0).Rows(0)("MK01_BakiPerutk")) - decJumBlmByrGst
                                '                                                    Dim MK01_BakiTng As Decimal = CDec(ds5.Tables(0).Rows(0)("MK01_BakiTng")) + decJumBlmByrGst
                                '                                                    Dim MK01_BakiSlpsPemLulus As Decimal = CDec(ds5.Tables(0).Rows(0)("MK01_BakiSlpsPemLulus")) - decJumBlmByrGst

                                '                                                    strSql = "update MK01_VotTahun set MK01_TngYtd = @MK01_TngYtd, MK01_PemLulus = @MK01_PemLulus, MK01_BakiPerutk = @MK01_BakiPerutk, MK01_BakiTng = @MK01_BakiTng, MK01_BakiSlpsPemLulus = @MK01_BakiSlpsPemLulus 
                                'WHERE KodKw = @KodKw AND KodKO = @KodKO And KodPTJ = @KodPTJ AND KodKP = @KodKP AND KodVot = @KodVot AND MK01_Tahun = @MK01_Tahun"
                                '                                                    paramSql =
                                '                                                            {
                                '                                                                New SqlParameter("@MK01_TngYtd", MK01_TngYtd),
                                '                                                                New SqlParameter("@MK01_PemLulus", MK01_PemLulus),
                                '                                                                New SqlParameter("@MK01_BakiPerutk", MK01_BakiPerutk),
                                '                                                                New SqlParameter("@MK01_BakiTng", MK01_BakiTng),
                                '                                                                New SqlParameter("@MK01_BakiSlpsPemLulus", MK01_BakiSlpsPemLulus),
                                '                                                                New SqlParameter("@KodKw", strKWgst),
                                '                                                                New SqlParameter("@KodKO", strKOgst),
                                '                                                                New SqlParameter("@KodPTJ", strPTJgst),
                                '                                                                New SqlParameter("@KodKP", strKPgst),
                                '                                                                New SqlParameter("@KodVot", VotGSTH2),
                                '                                                                New SqlParameter("@MK01_Tahun", Now.Year)
                                '                                                            }

                                '                                                    If Not dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                                '                                                        blnSuccess = False
                                '                                                        Exit Try
                                '                                                    End If
                                '                                                End If
                                '                                            End If

                                '                                            'UPDATE MK01_VOTTAHUN H1
                                '                                            Dim VotGSTH1 As String = strVot.Substring(0, 2) + "000"
                                '                                            strSql = "SELECT MK01_TngYtd,MK01_PemLulus,MK01_BakiPerutk,MK01_BakiTng,MK01_BakiSlpsPemLulus
                                'From MK01_VotTahun WHERE KodKw = '" & strKWgst & "' and KodKO = '" & strKOgst & "'  And KodPTJ = '" & strPTJgst & "' and KodKP = '" & strKPgst & "' AND KodVot = '" & VotGSTH1 & "' AND MK01_Tahun = '" & Now.Year & "'"

                                '                                            Dim ds6 As New DataSet
                                '                                            ds6 = dbconn.fSelectCommand(strSql)

                                '                                            If Not ds6 Is Nothing Then
                                '                                                If ds6.Tables(0).Rows.Count > 0 Then

                                '                                                    'rs1("MK01_TngYtd") = rs1("MK01_TngYtd") + Format(rs3("JumBlmByr"), "############.00")
                                '                                                    'rs1("MK01_PemLulus") = rs1("MK01_PemLulus") + Format(rs3("JumBlmByr"), "############.00")
                                '                                                    'rs1("MK01_BakiPerutk") = rs1("MK01_BakiPerutk") - Format(rs3("JumBlmByr"), "############.00")
                                '                                                    'rs1("MK01_BakiTng") = rs1("MK01_BakiTng") + Format(rs3("JumBlmByr"), "############.00")
                                '                                                    'rs1("MK01_BakiSlpsPemLulus") = rs1("MK01_BakiSlpsPemLulus") - Format(rs3("JumBlmByr"), "############.00")

                                '                                                    Dim MK01_TngYtd As Decimal = CDec(ds6.Tables(0).Rows(0)("MK01_TngYtd")) + decJumBlmByrGst
                                '                                                    Dim MK01_PemLulus As Decimal = CDec(ds6.Tables(0).Rows(0)("MK01_PemLulus")) + decJumBlmByrGst
                                '                                                    Dim MK01_BakiPerutk As Decimal = CDec(ds6.Tables(0).Rows(0)("MK01_BakiPerutk")) - decJumBlmByrGst
                                '                                                    Dim MK01_BakiTng As Decimal = CDec(ds6.Tables(0).Rows(0)("MK01_BakiTng")) + decJumBlmByrGst
                                '                                                    Dim MK01_BakiSlpsPemLulus As Decimal = CDec(ds6.Tables(0).Rows(0)("MK01_BakiSlpsPemLulus")) - decJumBlmByrGst

                                '                                                    strSql = "update MK01_VotTahun set MK01_TngYtd = @MK01_TngYtd, MK01_PemLulus = @MK01_PemLulus, MK01_BakiPerutk = @MK01_BakiPerutk, MK01_BakiTng = @MK01_BakiTng, MK01_BakiSlpsPemLulus = @MK01_BakiSlpsPemLulus 
                                'WHERE KodKw = @KodKw AND KodKO = @KodKO And KodPTJ = @KodPTJ AND KodKP = @KodKP AND KodVot = @KodVot AND MK01_Tahun = @MK01_Tahun"
                                '                                                    paramSql =
                                '                                                            {
                                '                                                                New SqlParameter("@MK01_TngYtd", MK01_TngYtd),
                                '                                                                New SqlParameter("@MK01_PemLulus", MK01_PemLulus),
                                '                                                                New SqlParameter("@MK01_BakiPerutk", MK01_BakiPerutk),
                                '                                                                New SqlParameter("@MK01_BakiTng", MK01_BakiTng),
                                '                                                                New SqlParameter("@MK01_BakiSlpsPemLulus", MK01_BakiSlpsPemLulus),
                                '                                                                New SqlParameter("@KodKw", strKWgst),
                                '                                                                New SqlParameter("@KodKO", strKOgst),
                                '                                                                New SqlParameter("@KodPTJ", strPTJgst),
                                '                                                                New SqlParameter("@KodKP", strKPgst),
                                '                                                                New SqlParameter("@KodVot", VotGSTH1),
                                '                                                                New SqlParameter("@MK01_Tahun", Now.Year)
                                '                                                            }

                                '                                                    If Not dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                                '                                                        blnSuccess = False
                                '                                                        Exit Try
                                '                                                    End If
                                '                                                End If
                                '                                            End If
                                '                                        End If
                                '                                    Next

                                '                                End If
                                '                            End If

                                Dim strNoSiri As String = fGetNoSiri()
                                If strKodPTj = "050000" Then
                                    'INDEN
                                    'Update dlm Table IND01_Inden
                                    strSql = "UPDATE IND01_Inden SET IND01_JumCF = @IND01_JumCF,IND01_StatusCF = @IND01_StatusCF
WHERE IND01_NoInden = @IND01_NoInden"
                                    paramSql =
                                   {
                                       New SqlParameter("@IND01_JumCF", decJumlahCF),
                                       New SqlParameter("@IND01_StatusCF", 1),
                                       New SqlParameter("@IND01_NoInden", strNoPT)
                                                               }

                                    If Not dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                                        blnSuccess = False
                                        Exit Try
                                    End If


                                    'Update dahulu dlm Table IND16_LogCF
                                    strSql = "select count (*) from IND16_LogCF where IND01_NoInden = '" & strNoPT & "'"
                                    If dbconn.fSelectCount(strSql) > 0 Then
                                        strSql = "UPDATE IND16_LogCF SET IND16_Status = @IND16_Status
WHERE IND01_NoInden = @IND01_NoInden"
                                        paramSql =
                                   {
                                       New SqlParameter("@IND16_Status", decJumlahCF),
                                       New SqlParameter("@IND01_NoInden", strNoPT)
                                                               }

                                        If Not dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                                            blnSuccess = False
                                            Exit Try
                                        End If
                                    End If


                                    'insert dlm table IND16_LogCF - rekod terkini
                                    strSql = "INSERT INTO IND16_LogCF
(IND16_Siri,IND01_NoInden,IND16_NoRujCF,IND16_Tarikh,NoStaf,IND16_Status,IND16_TahunCF)
VALUES (@IND16_Siri,IND01_NoInden,IND16_NoRujCF,@IND16_Tarikh, @NoStaf,@IND16_Status,@strThnCF)"
                                    paramSql =
                               {
                                   New SqlParameter("@IND16_Siri", strNoSiri),
                                   New SqlParameter("@IND01_NoInden", strNoPT),
                                   New SqlParameter("@IND16_NoRujCF", strRujukan),
                                   New SqlParameter("@IND16_Tarikh", Now.ToString("yyyy-MM-dd")),
                                   New SqlParameter("@NoStaf", strNoStaf),
                                   New SqlParameter("@IND16_Status", 1),
                                   New SqlParameter("@IND16_TahunCF", strNoPT)
                                                           }

                                    If Not dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                                        blnSuccess = False
                                        Exit Try
                                    End If

                                Else
                                    'PESANAN TEMPATAN
                                    'Update Table PO19_Pt
                                    strSql = "UPDATE PO19_Pt SET PO19_JumCF = @PO19_JumCF,PO19_StatusCF = @PO19_StatusCF WHERE PO19_NoPt = @PO19_NoPt"
                                    paramSql =
                                    {
                                        New SqlParameter("@PO19_JumCF", decJumlahCF),
                                        New SqlParameter("@PO19_StatusCF", 1),
                                        New SqlParameter("@PO19_NoPt", strNoPT)
                                                                }

                                    If Not dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                                        blnSuccess = False
                                        Exit Try
                                    End If

                                    'Update Table PO16_LogCF
                                    strSql = "select count (*) from PO16_LogCF where PO02_NoPt = '" & strNoPT & "'"
                                    If dbconn.fSelectCount(strSql) > 0 Then
                                        strSql = "UPDATE PO16_LogCF SET PO16_Status = @PO16_Status WHERE PO02_NoPt = @PO02_NoPt"
                                        paramSql =
                                        {
                                            New SqlParameter("@PO16_Status", 0),
                                            New SqlParameter("@PO02_NoPt", strNoPT)
                                                                    }
                                        If Not dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                                            blnSuccess = False
                                            Exit Try
                                        End If
                                    End If



                                    'insert dlm table PO16_LogCF - rekod terkini
                                    strSql = "INSERT INTO PO16_LogCF (PO16_Siri,PO02_NoPt,PO16_NoRujCF,PO16_Tarikh,NoStaf,PO16_Status,PO16_TahunCF) 
VALUES (@PO16_Siri,@PO02_NoPt,@PO16_NoRujCF,@PO16_Tarikh,@NoStaf,@PO16_Status,@PO16_TahunCF)"
                                    paramSql =
                                    {
                                        New SqlParameter("@PO16_Siri", strNoSiri),
                                        New SqlParameter("@PO02_NoPt", strNoPT),
                                        New SqlParameter("@PO16_NoRujCF", strRujukan),
                                        New SqlParameter("@PO16_Tarikh", Now.ToString("yyyy-MM-dd")),
                                        New SqlParameter("@NoStaf", strNoStaf),
                                        New SqlParameter("@PO16_Status", 1),
                                        New SqlParameter("@PO16_TahunCF", strThnCF)
                                                                }
                                    If Not dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                                        blnSuccess = False
                                        Exit Try
                                    End If
                                End If

                            Next
                        End If
                    End If
                End If
            Next

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

    Private Sub ddlTahun_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTahun.SelectedIndexChanged
        Thread.Sleep(5000)
        If ddlTahun.SelectedIndex <> 0 Then
            lblTahun.Text = CInt(ddlTahun.SelectedValue) + 1
            sLoadPT()
            If ddlPTj.SelectedIndex = 0 Then
                sListCF()
            Else
                sListCFPTj()
            End If
        End If

    End Sub

    Private Sub sListCF()
        Dim strSql As String
        Dim intRec As Integer = 0
        Dim dtPros As New DataTable
        Try
            sClearGvPTProses()
            'dtPros.Columns.AddRange(New DataColumn() {
            '                    New DataColumn("PO19_NoPt", GetType(String)),
            '                    New DataColumn("ROC01_IDSya", GetType(String)),
            '                    New DataColumn("ROC01_NamaSya", GetType(String)),
            '                    New DataColumn("PO02_JumSebenar", GetType(Decimal)),
            '                    New DataColumn("PO02_JumBlmByr", GetType(Decimal)),
            '                    New DataColumn("MK06_Rujukan", GetType(Decimal))
            '                        })

            dtPros = fSetDtProses()



            If ViewState("KodPTj") = "050000" Then
                strSql = "Select a.KodDok,a.MK06_Rujukan,a.MK06_NoDok,Sum(isnull(a.MK06_Debit,0)) As MK06_Debit,b.ROC01_IdSya,b.ROC01_NamaSya
        From MK06_Transaksi a,ROC01_Syarikat b,IND01_Inden c
         Where a.MK06_NoDok = C.IND01_NoInden
         and c.ROC01_IDSya = b.ROC01_IDSya
         And a.KodDok = 'CF_INDEN'
         AND left(a.mk06_rujukan,28) not in (SELECT left(mk06_rujukan,28) From mk06_transaksi WHERE koddok='CF_INDEN' and mk06_rujukan like '%-R')
         And Substring(a.MK06_Rujukan,8,4) = '" & Trim(ddlTahun.SelectedValue.TrimEnd) & "'
         Group By a.KodDok,a.MK06_Rujukan,a.MK06_NoDok,b.ROC01_IdSya,b.ROC01_NamaSya
         Order by a.MK06_NoDok"
            Else
                strSql = "Select a.KodDok,a.MK06_Rujukan,a.MK06_NoDok,Sum(isnull(a.MK06_Debit,0)) As MK06_Debit,b.ROC01_IdSya,b.ROC01_NamaSya
         From MK06_Transaksi a,ROC01_Syarikat b,PO19_Pt c
         Where a.MK06_NoDok = C.PO19_NoPt
         and c.ROC01_IDSya = b.ROC01_IDSya
         And a.KodDok = 'CF_LO'
        AND left(a.mk06_rujukan,21) not in (SELECT left(mk06_rujukan,21) From mk06_transaksi WHERE koddok='CF_LO' and mk06_rujukan like '%-R')
         And Substring(a.MK06_Rujukan,5,4) = '" & Trim(ddlTahun.SelectedValue.TrimEnd) & "'
         Group By a.KodDok,a.MK06_Rujukan,a.MK06_NoDok,b.ROC01_IdSya,b.ROC01_NamaSya
         Order by a.MK06_NoDok"
            End If

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            Dim strNoPT, strIdSya, strNmSya, strJum, strRujukan As String
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        strNoPT = ds.Tables(0).Rows(i)("MK06_NoDok")
                        strIdSya = ds.Tables(0).Rows(i)("ROC01_IdSya")
                        strNmSya = ds.Tables(0).Rows(i)("ROC01_NamaSya")
                        strJum = ds.Tables(0).Rows(i)("MK06_Debit")
                        strRujukan = ds.Tables(0).Rows(i)("MK06_Rujukan")

                        dtPros.Rows.Add(strNoPT, strIdSya, strNmSya, strJum, strRujukan)
                    Next

                    gvProses.DataSource = dtPros
                    gvProses.DataBind()

                    intRec = ds.Tables(0).Rows.Count
                End If
            End If

            lblJumPros.InnerText = intRec
            ViewState("dtProses") = dtPros
        Catch ex As Exception

        End Try
    End Sub

    Private Sub sListCFPTj()
        Dim strSql As String
        Dim intRec As Integer = 0
        Dim dtPros As New DataTable
        Try
            sClearGvPTProses()

            dtPros = fSetDtProses()

            If ViewState("KodPTj") = "050000" Then
                strSql = "Select a.KodDok,a.MK06_Rujukan,a.MK06_NoDok,Sum(isnull(a.MK06_Debit,0)) As MK06_Debit,b.ROC01_IdSya,b.ROC01_NamaSya
         From MK06_Transaksi a,ROC01_Syarikat b,IND01_Inden c
         Where a.MK06_NoDok = C.IND01_NoInden
         and c.ROC01_IDSya = b.ROC01_IDSya
         And a.KodDok = 'CF_INDEN'
         AND left(a.mk06_rujukan,28) not in (SELECT left(mk06_rujukan,28) From mk06_transaksi WHERE koddok='CF_INDEN' and mk06_rujukan like '%-R')
         And Substring(a.MK06_Rujukan,8,4) = '" & Trim(ddlTahun.SelectedValue.TrimEnd) & "'
         Group By a.KodDok,a.MK06_Rujukan,a.MK06_NoDok,b.ROC01_IdSya,b.ROC01_NamaSya
         Order by a.MK06_NoDok"
            Else
                strSql = "Select a.KodDok,a.MK06_Rujukan,a.MK06_NoDok,Sum(isnull(a.MK06_Debit,0)) As MK06_Debit,b.ROC01_IdSya,b.ROC01_NamaSya
         From MK06_Transaksi a,ROC01_Syarikat b,PO19_Pt c
         Where a.MK06_NoDok = C.PO19_NoPt
         and c.ROC01_IDSya = b.ROC01_IDSya
         And a.KodDok = 'CF_LO'
         AND left(a.mk06_rujukan,21) not in (SELECT left(mk06_rujukan,21) From mk06_transaksi WHERE koddok='CF_LO' and mk06_rujukan like '%-R')
         And Substring(a.MK06_Rujukan,5,4) = '" & Trim(ddlTahun.SelectedValue.TrimEnd) & "' and substring(c.PO01_NoMohon,3,6) = '" & Trim(ddlPTj.SelectedValue.TrimEnd) & "'
         Group By a.KodDok,a.MK06_Rujukan,a.MK06_NoDok,b.ROC01_IdSya,b.ROC01_NamaSya
         Order by a.MK06_NoDok"
            End If

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            Dim strNoPT, strIdSya, strNmSya, strJum, strRujukan As String
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        strNoPT = ds.Tables(0).Rows(i)("MK06_NoDok")
                        strIdSya = ds.Tables(0).Rows(i)("ROC01_IdSya")
                        strNmSya = ds.Tables(0).Rows(i)("ROC01_NamaSya")
                        strJum = ds.Tables(0).Rows(i)("MK06_Debit")
                        strRujukan = ds.Tables(0).Rows(i)("MK06_Rujukan")

                        dtPros.Rows.Add(strNoPT, strIdSya, strNmSya, strJum, strRujukan)
                    Next

                    gvProses.DataSource = dtPros
                    gvProses.DataBind()

                    intRec = ds.Tables(0).Rows.Count
                End If
            End If

            lblJumPros.InnerText = intRec
            ViewState("dtProses") = dtPros
        Catch ex As Exception

        End Try
    End Sub

    Public Function fGetNoSiri() As String
        Try
            Dim blnFound As Boolean = False
            Dim ds As New DataSet
            Dim strRunNo As String
            Dim dbconn As New DBKewConn
            Dim strKodModul As String = "CLS"
            Dim strTahun As String = Now.Year
            Dim intNoAkhir As Integer
            Dim strPrefix As String = "CF"

            Dim strSql As String = "SELECT NoAkhir From MK_NoAkhir WHERE KodModul = '" & strKodModul & "' AND  Prefix = '" & strPrefix & "' AND Tahun = '" & Now.Year & "'"
            ds = fGetRec(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    intNoAkhir = CInt(ds.Tables(0).Rows(0)("NoAKhir").ToString)
                    blnFound = True
                Else
                    intNoAkhir = 0
                End If
            Else
                intNoAkhir = 0
            End If

            intNoAkhir = intNoAkhir + 1

            If blnFound = True Then
                strSql = "update MK_NoAkhir set NoAkhir =@noakhir  WHERE KodModul = @kodmodul AND  Prefix = @prefix AND Tahun = @tahun"

                Dim paramSql2() As SqlParameter = {
                            New SqlParameter("@noakhir", intNoAkhir),
                            New SqlParameter("@tahun", Now.Year),
                            New SqlParameter("@kodmodul", strKodModul),
                            New SqlParameter("@prefix", strPrefix)
                            }

                dbconn.sConnBeginTrans()
                If dbconn.fUpdateCommand(strSql, paramSql2) > 0 Then
                    dbconn.sConnCommitTrans()
                Else
                    dbconn.sConnRollbackTrans()
                End If
            Else
                strSql = "insert into MK_NoAkhir (KodModul, Prefix, NoAkhir, Tahun, Butiran) " &
                "values (@KodModul, @Prefix, @NoAkhir, @Tahun, @Butiran)"
                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@KodModul", strKodModul),
                    New SqlParameter("@Prefix", strPrefix),
                    New SqlParameter("@NoAkhir", 1),
                    New SqlParameter("@Tahun", strTahun),
                    New SqlParameter("@Butiran", "-")
                }

                dbconn = New DBKewConn
                dbconn.sConnBeginTrans()
                If dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                    dbconn.sConnCommitTrans()
                Else
                    dbconn.sConnRollbackTrans()
                End If
            End If

            '00000119
            strRunNo = intNoAkhir.ToString("D6") + Now.ToString("yy")
            Return strRunNo

        Catch ex As Exception

        End Try
    End Function

    Private Sub sHapusTempCF()
        Try

            Dim strSql As String = "Delete From tempCF"
            Dim dbconn As New DBKewConn
            dbconn.fUpdateCommand(strSql)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvProses_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvProses.RowDataBound
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim strRujukan As String = DataBinder.Eval(e.Row.DataItem, "MK06_Rujukan").ToString()
                If strRujukan = "R" Then
                    e.Row.FindControl("cbSelect").Visible = True
                    e.Row.FindControl("btnChecked").Visible = False
                    'e.Row.Cells(7).ForeColor = System.Drawing.Color.Blue
                Else
                    e.Row.FindControl("cbSelect").Visible = False
                    e.Row.FindControl("btnChecked").Visible = True
                    'e.Row.Cells(7).ForeColor = System.Drawing.Color.Blue
                End If
            End If


        Catch ex As Exception

        End Try
    End Sub
End Class