Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization


Public Class MaklumatPenyediaanBajetPTJ
    Inherits System.Web.UI.Page

    Dim ds As New DataSet
    Dim dtDel As New DataTable
    Public Shared dsMohon As DataSet

    Public Shared dvButiran As DataView
    Public Shared dtButiran As DataTable

    Public KodStatus As String = String.Empty
    'Private listTambahBtrn As New List(Of ButiranMohonUnitModel)
    Private countButiran As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then

                'fBindDdlJenisDasar()
                fBindDdlKodOperasi()
                fBindDdlKW()
                fBindDdlVot()
                Dim passQS_NoMohon = Request.QueryString("no")
                MohonEdit(passQS_NoMohon)

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MohonEdit(NoMohon As String)
        KemaskiniField(False)
        Try

            Dim dsMohon As New DataSet
            Dim dsMohonDt As New DataSet
            'dtButiran = New DataTable
            Dim dt As New DataTable
            'Dim strKodOperasi As String
            'Dim strKW As String

            Dim strSql = "SELECT        a.BG20_NoMohon, 
                                        a.BG20_Program, 
                                        a.BG20_Justifikasi, 
                                        a.BG20_TahunBajet, 
                                        a.BG20_StaffIDPelulus, 
                                        a.BG20_AmaunMohon, 
                                        a.BG20_AmaunSyorNC, 
                                        a.BG20_TarikhSokong, 
                                        c.Butiran, 
                                        b.KodOperasi,
                                        e.NamaUnit, 
                                        d.NamaBahagian, 
                                        a.BG20_TarikhMohon, 
                                        f.BG_Butiran as StatusDoc,
                                        a.KodKW,
                                        g.Butiran as KW
                         FROM            BG20_Mohon a INNER JOIN
                         BG_KodOperasi      b ON a.KodOperasi = b.KodOperasi INNER JOIN
                         MK_PTJ             c ON a.KodPtj = c.KodPTJ INNER JOIN
                         MK_BahagianPTJ     d ON a.KodBahagian = d.KodBah AND a.KodPtj = d.KodPtj INNER JOIN
                         MK_UnitPTJ         e ON d.KodBah = e.KodBah INNER JOIN
                         BG_StatusDok       f ON a.StatusDok = f.BG_KodStatus  INNER JOIN
                         MK_Kw              g ON a.KodKW = g.KodKw
                         where BG20_NoMohon='" & NoMohon & "'"
            '$"Select * from a where BG20_NoMohon='{NoMohon}'"

            'Dim strSqlDt = $"Select * from aDt where BG20_NoMohon='{NoMohon}'"

            dsMohon = BindSQL(strSql)

            Dim Status = dsMohon.Tables(0).Rows(0)("StatusDoc").ToString
            Dim Tarikh As Date = dsMohon.Tables(0).Rows(0)("BG20_TarikhMohon")
            Dim Bahagian = dsMohon.Tables(0).Rows(0)("NamaBahagian").ToString
            Dim Unit = dsMohon.Tables(0).Rows(0)("NamaUnit").ToString
            Dim Program = dsMohon.Tables(0).Rows(0)("BG20_Program").ToString
            Dim Just = dsMohon.Tables(0).Rows(0)("BG20_Justifikasi").ToString
            Dim KodOperasi = dsMohon.Tables(0).Rows(0)("KodOperasi").ToString
            Dim KW = dsMohon.Tables(0).Rows(0)("KodKW").ToString

            txtNoMohon.Text = NoMohon
            txtStatus.Text = Status
            txtTarikh.Text = Tarikh
            txtBahagian.Text = Bahagian
            txtUnit.Text = Unit
            txtProgram.Text = Program
            txtJust.Text = Just
            ddlKodOperasi.Items.FindByValue(KodOperasi.Trim()).Selected = True
            ddlKW.Items.FindByValue(KW.Trim()).Selected = True

            strSql = "SELECT BG20_NoButiran,
                             KodVotSebagai,
                             BG20_Butiran,
                             BG20_Kuantiti,
                             BG20_AngHrgUnit,
                             BG20_AngJumlah
                        FROM            BG20_MohonDt
                        where BG20_NoMohon='" & NoMohon & "'"

            dsMohonDt = BindSQL(strSql)

            Dim dtButiran As New DataTable
            Dim intBil As Integer
            Dim strButiran As String
            Dim strVotSebagai As String
            Dim intKuantiti As Integer
            Dim strAnggaranSeunit As String
            Dim strAnggaranJumlah As String
            Dim intBilButiran As Integer
            Dim intEdit As Integer


            dtButiran.Columns.Add("Bil", GetType(Integer))
            dtButiran.Columns.Add("KodVot", GetType(String))
            dtButiran.Columns.Add("Butiran", GetType(String))
            dtButiran.Columns.Add("Kuantiti", GetType(String))
            dtButiran.Columns.Add("AnggaranSeunit", GetType(String))
            dtButiran.Columns.Add("AnggaranJumlah", GetType(String))
            dtButiran.Columns.Add("NoButiran", GetType(Integer))
            dtButiran.Columns.Add("Edit", GetType(Integer))

            For i As Integer = 0 To dsMohonDt.Tables(0).Rows.Count - 1
                'blnPilih = False
                intBil = i + 1
                strVotSebagai = dsMohonDt.Tables(0).Rows(i)("KodVotSebagai").ToString
                strButiran = dsMohonDt.Tables(0).Rows(i)("BG20_Butiran").ToString
                intKuantiti = dsMohonDt.Tables(0).Rows(i)("BG20_Kuantiti").ToString
                strAnggaranSeunit = CDec(dsMohonDt.Tables(0).Rows(i)("BG20_AngHrgUnit")).ToString("#,##0.00")
                strAnggaranJumlah = CDec(dsMohonDt.Tables(0).Rows(i)("BG20_AngJumlah")).ToString("#,##0.00")
                intBilButiran = dsMohonDt.Tables(0).Rows(i)("BG20_NoButiran").ToString
                intEdit = 0
                dtButiran.Rows.Add(intBil, strVotSebagai, strButiran, intKuantiti, strAnggaranSeunit, strAnggaranJumlah, intBilButiran, intEdit)
            Next

            ViewState("countButiran") = dsMohonDt.Tables(0).Rows.Count
            ViewState("CurrentButiran") = dtButiran

            'Dim dvButiran = New DataView(dtButiran)
            gvChartOfAccount.DataSource = dtButiran
            gvChartOfAccount.DataBind()

            Dim total As Decimal = dt.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("AnggaranJumlah"))
            gvChartOfAccount.FooterRow.Cells(1).Text = "Jumlah Besar"
            gvChartOfAccount.FooterRow.Cells(1).HorizontalAlign = HorizontalAlign.Right
            gvChartOfAccount.FooterRow.Cells(2).Text = total.ToString("N2")

            dtDel.Columns.Add("NoButiran", GetType(Integer))
            ViewState("DelDT") = dtDel

        Catch ex As Exception

        End Try
    End Sub

    Private Sub KemaskiniField(Yes As Boolean)

        'ddlBahagian.Enabled = Yes
        'ddlUnit.Enabled = Yes
        'ddlJenDasar.Enabled = Yes
        'ddlKodOperasi.Enabled = Yes
        'ddlVotSbg.Enabled = Yes

        'txtJust.Enabled = Yes
        'txtButiran.Enabled = Yes
        'txtKuantiti.Enabled = Yes
        'txtAngHrgUnit.Enabled = Yes
        'txtProgram.Enabled = Yes

        'gvButiran.Enabled = Yes

        'lbtnReset.Enabled = Yes
        'lbtnSaveButiran.Enabled = Yes

        lbtnHapus.Visible = Not Yes
        'lbtnKemaskini.Visible = Not Yes

        'lbtnSimpan.Visible = Yes
        lbtnKembali.Visible = Not Yes

    End Sub
    Private Sub fBindDdlKodOperasi()
        Try

            Dim strSql As String = $"Select KodOperasi, (KodOperasi + ' - ' + Butiran) AS Butiran2 from BG_KodOperasi"
            ds = BindSQL(strSql)

            ddlKodOperasi.DataSource = ds
            ddlKodOperasi.DataTextField = "Butiran2"
            ddlKodOperasi.DataValueField = "KodOperasi"
            ddlKodOperasi.DataBind()

        Catch ex As Exception
            'fErrorLog("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
        End Try
    End Sub

    Protected Sub fBindDdlKW()
        Try
            Dim strSql As String = "Select KodKW, butiran, (KodKW +' - ' + butiran) as Butiran2 from mk_kw ORDER BY Kodkw "
            ds = BindSQL(strSql)

            ddlKW.DataSource = ds
            ddlKW.DataTextField = "Butiran2"
            ddlKW.DataValueField = "KodKW"
            ddlKW.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindDdlVot()
        Try
            Dim strSql As String = "select KodVot, Butiran, (KodVot + ' - ' + Butiran) as Butiran2 from mk_vot where klasifikasi = 'H2' order by kodvot"
            ds = BindSQL(strSql)

            ddlVotSbg.DataSource = ds
            ddlVotSbg.DataTextField = "Butiran2"
            ddlVotSbg.DataValueField = "KodVot"
            ddlVotSbg.DataBind()
            ddlVotSbg.Items.Insert(0, New ListItem("-SILA PILIH-", ""))

        Catch ex As Exception
            'fErrorLog("Peruntukan_KW.aspx(fBindDdlVot)- " & ex.Message.ToString)
        End Try


    End Sub
    Private Function BindSQL(ByVal strSql As String) As DataSet
        Try
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fselectCommand(strSql)
            Return ds
        Catch ex As Exception
            'fErrorLog(ex.Message.ToString)
        End Try
    End Function

    Protected Sub gvChartOfAccount_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvChartOfAccount.SelectedIndexChanged

        Try


            For Each row As GridViewRow In gvChartOfAccount.Rows
                If row.RowIndex = gvChartOfAccount.SelectedIndex Then

                    lbtnReset.Visible = False
                    lbtnTambah.Visible = False

                    lbtnUndo.Visible = True
                    lbtnKemaskini.Visible = True

                    Dim NoButiran As String = gvChartOfAccount.Rows(row.RowIndex).Cells(0).Text
                    Dim KodVot As String = gvChartOfAccount.Rows(row.RowIndex).Cells(1).Text
                    Dim Butiran As String = gvChartOfAccount.Rows(row.RowIndex).Cells(2).Text
                    Dim Kuantiti As Integer = gvChartOfAccount.Rows(row.RowIndex).Cells(3).Text
                    Dim AngHrgUnit As Decimal = gvChartOfAccount.Rows(row.RowIndex).Cells(4).Text
                    Dim AngJumBelanja As Decimal = gvChartOfAccount.Rows(row.RowIndex).Cells(5).Text

                    'ddlVotSbg.Items.FindByValue(KodVot.Trim()).Selected = True
                    txtNoButiran.Text = 0
                    txtBil.Text = NoButiran.ToString
                    ddlVotSbg.SelectedValue = KodVot.Trim
                    txtButiran.Text = Butiran
                    txtKuantiti.Text = Kuantiti.ToString
                    txtAngHrgUnit.Text = AngHrgUnit.ToString
                    txtAngJum.Text = AngJumBelanja.ToString
                    'ViewState("NoButiran") = NoButiran
                Else
                    row.ForeColor = ColorTranslator.FromHtml("#000000")
                End If
            Next
        Catch ex As Exception

        End Try

    End Sub


    Protected Sub lbtnKembali_Click(sender As Object, e As EventArgs) Handles lbtnKembali.Click
        Response.Redirect("~/FORMS/BAJET/PERMOHONAN BAJET/Penyediaan_Bajet_PTJ.aspx")
    End Sub

    Protected Sub lbtnReset_Click(sender As Object, e As EventArgs) Handles lbtnReset.Click
        clearTextButiran()
    End Sub

    Private Sub clearTextButiran()
        txtButiran.Text = ""
        txtKuantiti.Text = "0"
        txtAngJum.Text = "0"
        txtAngHrgUnit.Text = "0"

        'ddlVotSbg.Items.Insert(0, New ListItem("-SILA PILIH-", ""))
        ddlVotSbg.SelectedIndex = 0
    End Sub
    Protected Sub lbtnUndo_Click(sender As Object, e As EventArgs) Handles lbtnUndo.Click
        lbtnUndo.Visible = False
        lbtnKemaskini.Visible = False
        lbtnTambah.Visible = True
        lbtnReset.Visible = True
        ddlVotSbg.Items.Insert(0, New ListItem("-SILA PILIH-", ""))
        clearTextButiran()
    End Sub

    Protected Sub lbtnTambah_Click(sender As Object, e As EventArgs) Handles lbtnTambah.Click
        'Page.Validate()
        'If Page.IsValid() Then
        'If txtAngJum.Text = "" Then
        Try


            Dim IntEdit As Integer = 1
            Dim AngHrgUnit = CDec(txtAngHrgUnit.Text)
            Dim JumAngHrg As Decimal = 0.00

            If txtKuantiti.Text = "0" Then
                JumAngHrg = AngHrgUnit
            Else
                JumAngHrg = CDec(CInt(txtKuantiti.Text) * AngHrgUnit)
            End If
            'txtAngJum.Text = JumAngHrg.ToString("#,##0.00")

            Dim noPermohonan = txtNoMohon.Text

            If ViewState("CurrentButiran") IsNot Nothing Then

                countButiran = ViewState("countButiran")

                countButiran = countButiran + 1

                ViewState("countButiran") = countButiran


                Dim drCurrentRow As DataRow = Nothing
                'get datatable from view state 
                Dim dtButiran As DataTable = DirectCast(ViewState("CurrentButiran"), DataTable)

                'add each row into data table
                drCurrentRow = dtButiran.NewRow()
                drCurrentRow("Bil") = countButiran
                drCurrentRow("KodVot") = ddlVotSbg.SelectedValue
                drCurrentRow("Butiran") = txtButiran.Text
                drCurrentRow("Kuantiti") = CInt(txtKuantiti.Text) '.ToString
                drCurrentRow("AnggaranSeunit") = CDec(txtAngHrgUnit.Text) '.ToString("#,##0.00")
                drCurrentRow("AnggaranJumlah") = CDec(JumAngHrg).ToString("#,##0.00")
                drCurrentRow("NoButiran") = 0
                drCurrentRow("Edit") = 1 'save into database
                dtButiran.Rows.Add(drCurrentRow)

                ViewState("CurrentButiran") = dtButiran
                'bind Gridview
                gvChartOfAccount.DataSource = dtButiran
                gvChartOfAccount.DataBind()


            End If


            'If dvButiran IsNot Nothing Then

            'dtButiran.Columns.Add("Bil", GetType(Integer))
            'dtButiran.Columns.Add("KodVot", GetType(String))
            'dtButiran.Columns.Add("Butiran", GetType(String))
            'dtButiran.Columns.Add("Kuantiti", GetType(String))
            'dtButiran.Columns.Add("AnggaranSeunit", GetType(String))
            'dtButiran.Columns.Add("AnggaranJumlah", GetType(String))
            'dtButiran.Columns.Add("NoButiran", GetType(Integer))
            'dtButiran.Columns.Add("Edit", GetType(Integer))


            'Dim intBil As String = dvButiran.Count + 1
            'Dim strVotSebagai As String = ddlVotSbg.SelectedValue
            '    Dim strButiran As String = txtButiran.Text
            '    Dim intKuantiti As Integer = txtKuantiti.Text
            '    Dim intNoButiran As Integer = 0
            '    Dim intEditing As Integer
            ' Dim AngHrgUnit As Decimal = angHrgSeunit
            ' Dim AngJumBelanja As Decimal = AngJumBelanja.ToString

            'Dim newRow As DataRowView = dvButiran.AddNew()
            '    newRow("Bil") = intBil
            '    newRow("KodVot") = strVotSebagai
            '    newRow("Butiran") = strButiran
            '    newRow("Kuantiti") = intKuantiti
            '    newRow("AnggaranSeunit") = strButiran
            '    newRow("NoButiran") = strButiran
            '    newRow("Edit") = intEditing
            '    newRow.EndEdit()
            '    dvButiran.Sort = "product_id"

            'dvButiran.Add(intBil, strVotSebagai, strButiran, intKuantiti, AngHrgUnit, JumAngHrg, "", 0)


            'Clear textbox in Butiran Panel
            clearTextButiran()
        Catch ex As Exception

        End Try
        'End If
    End Sub

    'Protected Sub txtAngHrgUnit_TextChanged(sender As Object, e As EventArgs) Handles txtAngHrgUnit.TextChanged
    '    Dim angHrgSeunit = CDec(txtAngHrgUnit.Text)
    '    Dim JumAngHrg As Decimal = 0.00
    '    If txtKuantiti.Text = "0" Then
    '        JumAngHrg = angHrgSeunit
    '    Else
    '        JumAngHrg = CDec(CInt(txtKuantiti.Text) * angHrgSeunit)
    '    End If
    '    txtAngJum.Text = JumAngHrg.ToString("#,##0.00")
    '    txtAngHrgUnit.Text = angHrgSeunit.ToString("#,##0.00")
    'End Sub

    Private Sub gvChartOfAccount_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvChartOfAccount.PageIndexChanging
        Try

            gvChartOfAccount.PageIndex = e.NewPageIndex

            If Session("SortedView") IsNot Nothing Then
                gvChartOfAccount.DataSource = Session("SortedView")
                gvChartOfAccount.DataBind()
            Else

                'Dim dt As New DataTable
                'dt = fCreateDt2()
                gvChartOfAccount.DataSource = dvButiran
                gvChartOfAccount.DataBind()



            End If

        Catch ex As Exception
            'fErrorLog(ex.Message.ToString)
        End Try
    End Sub

    Protected Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.Click
        Try
            Dim dbconn As New DBKewConn
            Dim strnoPermohonan As String = txtNoMohon.Text
            Dim intNoButiran As Integer
            Dim paramSqlBtrn() As SqlParameter = Nothing
            Dim strSqlMohon = $"Select BG20_NoMohon, KodBahagian,KodUnitPtj,KodDasar,KodOperasi, KodKW, BG20_Program,BG20_Justifikasi,BG20_AmaunMohon from BG20_Mohon where BG20_NoMohon='{strnoPermohonan}'"


            Dim dsMohon As New DataSet
            dsMohon = dbconn.fSelectCommand(strSqlMohon, "MohonDs")

            If dsMohon IsNot Nothing Then
                Dim dr As DataRow = dsMohon.Tables("MohonDs").Rows(0)
                dr("KodBahagian") = txtBahagian.Text
                dr("KodUnitPtj") = txtUnit.Text
                'dr("KodDasar") = ddlJenDasar.SelectedValue
                dr("KodOperasi") = ddlKodOperasi.SelectedValue
                dr("BG20_Program") = txtProgram.Text
                dr("KodKW") = ddlKW.SelectedValue
                dr("BG20_Justifikasi") = txtJust.Text
                dr("BG20_AmaunMohon") = dsMohon.Tables(0).Rows(0)("BG20_AmaunMohon")
                dbconn.sUpdateCommand(dsMohon, strSqlMohon)
            End If

            'check butiran that suppose to be deleted

            Dim dtDel As DataTable = DirectCast(ViewState("DelDT"), DataTable)

            If dtDel IsNot Nothing Then
                For Each RowDel As DataRow In dtDel.Rows
                    intNoButiran = CInt(RowDel("NoButiran"))
                    Dim strSql = $"delete from BG20_MohonDt where BG20_NoMohon='{strnoPermohonan}' and BG20_NoButiran='{intNoButiran}'"
                    If dbconn.fUpdateCommand(strSql) > 0 Then
                        dbconn.sConnCommitTrans()
                    Else
                        dbconn.sConnRollbackTrans()
                        'fGlobalAlert("Rekod Butiran telah dipadam!", Me.Page, Me.[GetType](), "../../BAJET/PERMOHONAN BAJET/Permohonan_Bajet_Tahunan.aspx")
                    End If
                Next
            End If

            Dim dtButiran As DataTable = DirectCast(ViewState("CurrentButiran"), DataTable)

            For Each Row As DataRow In dtButiran.Rows
                Dim intEdit As Integer = CInt(Row("Edit"))

                If intEdit = 1 Then 'save new row

                    Dim strSqlButiran As String = "INSERT INTO BG20_MohonDt ( BG20_NoMohon, BG20_Butiran, BG20_Kuantiti, BG20_AngHrgUnit, BG20_AngJumlah, BG20_Status, KodVotSebagai)
                                            VALUES ( @NoMohonn, @Butirann, @Kuantitii, @AnggaranSeunit, @AnggaranJumlah, @Status, @KodVotSbg)"
                    paramSqlBtrn = {
                               New SqlParameter("@KodVotSbg", Row("KodVot")),
                               New SqlParameter("@NoMohonn", strnoPermohonan),
                               New SqlParameter("@Butirann", Row("Butiran")),
                               New SqlParameter("@Kuantitii", CInt(Row("Kuantiti"))),
                               New SqlParameter("@AnggaranSeunit", CDec(Row("AnggaranSeunit"))),
                               New SqlParameter("@AnggaranJumlah", CDec(Row("AnggaranJumlah"))),
                               New SqlParameter("@Status", True)
                           }

                    If dbconn.fInsertCommand(strSqlButiran, paramSqlBtrn) > 0 Then
                        countButiran = countButiran + 1
                        KodStatus = "01"
                        dbconn.sConnCommitTrans()

                        fGlobalAlert($"Rekod telah dikemaskini!", Me.Page, Me.[GetType]())

                    Else
                        dbconn.sConnRollbackTrans()
                        fGlobalAlert("Maklumat Butiran tidak berjaya disimpan!", Me.Page, Me.[GetType]())

                        'fErrorLog("Jenis_Permohonan.aspx(btnSave_Click)- " & ex.Message.ToString)
                    End If

                ElseIf intEdit = 2 Then 'update row
                    intNoButiran = CInt(Row("NoButiran"))
                    Dim strSqlButiran = $"SELECT BG20_NoButiran, BG20_NoMohon, BG20_Butiran, BG20_Kuantiti, BG20_AngHrgUnit, BG20_AngJumlah, BG20_Status, KodVotSebagai, KodVotLanjut
                    FROM  BG20_MohonDt where BG20_NoButiran='{intNoButiran}'"

                    Dim dsMohondt As New DataSet
                    dsMohondt = dbconn.fSelectCommand(strSqlMohon, "MohonDsDt")

                    If dsMohondt IsNot Nothing Then
                        Dim dr As DataRow = dsMohondt.Tables("MohonDsDt").Rows(0)
                        dr("BG20_Butiran") = dtButiran.Rows(0)("Butiran").ToString
                        dr("BG20_Kuantiti") = dtButiran.Rows(0)("Kuantiti").ToString
                        dr("BG20_AngHrgUnit") = dtButiran.Rows(0)("AnggaranSeunit").ToString
                        dr("BG20_AngJumlah") = dtButiran.Rows(0)("AnggaranJumlah").ToString
                        dr("KodVotSebagai") = dtButiran.Rows(0)("KodVot").ToString
                        'dr("BG20_Justifikasi") = dtButiran.Rows(0)("Butiran").ToString
                        'dr("BG20_AmaunMohon") = dsMohon.Tables(0).Rows(0)("BG20_AmaunMohon")
                        dbconn.sUpdateCommand(dsMohondt, strSqlButiran)
                    End If

                End If


            Next



        Catch ex As Exception

        End Try
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

    Protected Sub lbtnKemaskini_Click(sender As Object, e As EventArgs) Handles lbtnKemaskini.Click

        Dim AngHrgUnit = CDec(txtAngHrgUnit.Text)
        Dim JumAngHrg As Decimal = 0.00

        If txtKuantiti.Text = "0" Then
            JumAngHrg = AngHrgUnit
        Else
            JumAngHrg = CDec(CInt(txtKuantiti.Text) * AngHrgUnit)
        End If

        Dim noPermohonan = txtNoMohon.Text
        Dim intBilButiran As Integer = CInt(txtBil.Text)

        If ViewState("CurrentButiran") IsNot Nothing Then

            Dim drCurrentRow As DataRow = Nothing
            'get datatable from view state 
            Dim dtButiran As DataTable = DirectCast(ViewState("CurrentButiran"), DataTable)


            dtButiran.Rows.RemoveAt(intBilButiran - 1)

            'add each row into data table
            drCurrentRow = dtButiran.NewRow()
            drCurrentRow("Bil") = intBilButiran
            drCurrentRow("KodVot") = ddlVotSbg.SelectedValue
            drCurrentRow("Butiran") = txtButiran.Text
            drCurrentRow("Kuantiti") = CInt(txtKuantiti.Text) '.ToString
            drCurrentRow("AnggaranSeunit") = CDec(txtAngHrgUnit.Text) '.ToString("#,##0.00")
            drCurrentRow("AnggaranJumlah") = CDec(JumAngHrg).ToString("#,##0.00")
            drCurrentRow("NoButiran") = 0
            drCurrentRow("Edit") = 2 'update into database
            dtButiran.Rows.Add(drCurrentRow)

            dvButiran = New DataView(dtButiran)
            dvButiran.Sort = "Bil ASC"
            dtButiran = dvButiran.ToTable
            ViewState("CurrentButiran") = dtButiran

            'bind Gridview
            gvChartOfAccount.DataSource = dtButiran
            gvChartOfAccount.DataBind()

            lbtnKemaskini.Visible = False
            lbtnUndo.Visible = False
            lbtnTambah.Visible = True
            lbtnReset.Visible = True


        End If

        'If dvButiran IsNot Nothing Then

        '    Dim intBil As String = dvButiran.Count + 1
        '    Dim strVotSebagai As String = ddlVotSbg.SelectedValue
        '    Dim strButiran As String = txtButiran.Text
        '    Dim intKuantiti As Integer = txtKuantiti.Text
        '    Dim intNoButiran As Integer = 0
        '    Dim intEditing As Integer
        '    ' Dim AngHrgUnit As Decimal = angHrgSeunit
        '    ' Dim AngJumBelanja As Decimal = AngJumBelanja.ToString

        '    'dvButiran.Table.Rows(3).Delete()

        '    Dim newRow As DataRowView = dvButiran.AddNew()
        '    newRow("Bil") = intBil
        '    newRow("KodVot") = strVotSebagai
        '    newRow("Butiran") = strButiran
        '    newRow("Kuantiti") = intKuantiti
        '    newRow("AnggaranSeunit") = strButiran
        '    newRow("NoButiran") = strButiran
        '    newRow("Edit") = intEditing
        '    newRow.EndEdit()
        '    dvButiran.Sort = "Bil"


        'End If

        'Clear textbox in Butiran Panel
        clearTextButiran()

    End Sub

    Private Sub gvChartOfAccount_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvChartOfAccount.RowDeleting
        Try
            Dim row As GridViewRow = DirectCast(gvChartOfAccount.Rows(e.RowIndex), GridViewRow)
            Dim dtButiran As DataTable = DirectCast(ViewState("CurrentButiran"), DataTable)
            Dim intBil As Integer = Trim(row.Cells(0).Text.ToString.TrimEnd)
            Dim intNoButiran As Integer = Convert.ToInt32(CType(row.FindControl("lblNoButiran"), Label).Text)

            Dim dtDel As DataTable = DirectCast(ViewState("DelDT"), DataTable)

            dtDel.Rows.Add(intNoButiran)
            ViewState("DelDT") = dtDel

            dtButiran.Rows.RemoveAt(intBil - 1)
            Dim i As Integer = 0
            'For i As Integer = 0 To dtButiran.Rows.Count - 1
            For Each dtrow As DataRow In dtButiran.Rows
                i = i + 1
                dtrow("Bil") = i
            Next

            ViewState("CurrentButiran") = dtButiran

            'bind Gridview
            gvChartOfAccount.DataSource = dtButiran
            gvChartOfAccount.DataBind()

        Catch ex As Exception
            'fErrorLog("Jenis_Permohonan.aspx(gvJenPermohonan_RowDeleting)- " & ex.Message.ToString)
        End Try
    End Sub
End Class