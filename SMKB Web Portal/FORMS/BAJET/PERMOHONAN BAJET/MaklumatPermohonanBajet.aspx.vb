Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization
Imports System.IO


Public Class MaklumatPermohonanBajet
    Inherits System.Web.UI.Page

    Public KodStatus As String = String.Empty
    Private countButiran As Integer = 0
    Private StatusKemaskini As Boolean = False
    Private dbconn As New DBKewConn
    Private dbconnEQ As New DBEQConn
    Private dsBajet As New DataSet("MohonDs")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                firstBindViewStateButiran()
                Dim kod = fGetPeringkatBajetPTJ(Session("ssusrKodPTj"))
                fCheckPeringkat(kod)

                Dim passQS_NoMohon = Request.QueryString("no")
                fBindDdlJenisDasar()
                If passQS_NoMohon = "0" Then
                    Mohonbaru()
                Else
                    MohonEdit(passQS_NoMohon)
                    fLoadGVSalinanBajet(passQS_NoMohon)
                    lbtnHapus.Visible = True
                End If

                fBindDdlVotAm()
                ddlVotA.Items.Insert(0, New ListItem("-SILA PILIH-", ""))
                ddlVotA.SelectedIndex = 0

                fBindDdlVot("0", "0")
                ddlVotSbg.Items.Insert(0, New ListItem("-SILA PILIH-", ""))
                ddlVotSbg.SelectedIndex = 0

                Dim dsMohon_View As New DataSet
                Dim dtData As New DataTable
                Dim dt = TryCast(ViewState("vsSenaraiBaucarCek"), DataTable)

                'test nostaf
                'Session("ssusrID") = "00245"

                Dim strSql_eqsa = $"Select staf.att15 As nostaf, Replace(ptj.kodpejabat,' ','') as kodpejabat, ptj.name as ptj, dept.dept_kod, dept.dept_name, Unit.unit_kod, Unit.unit_name From
                                    [eqcas].[dbo].live_user_staf as staf
                                    Left Join [eqcas].[dbo].live_group as ptj on staf.att1 = ptj.kodpejabat
                                    left join [eqcas].[dbo].live_dept as dept on staf.att10 = dept.dept_kod
                                    left join [eqcas].[dbo].live_unit as unit on staf.att11 = unit.unit_kod
                                    where att15 = '{Session("ssusrID")}'"                                                                                                                                                                                           '


                'dsMohon_View = dbconnEQ.fselectCommand(strSql_eqsa)
                Dim ds = dbconnEQ.fselectCommand(strSql_eqsa)
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count = 1 Then
                        txtPtj.Text = ds.Tables(0).Rows(0)("ptj").ToString.ToUpper()
                        txtBahagian.Text = ds.Tables(0).Rows(0)("dept_name").ToString.ToUpper()


                        lblPtj.Text = ds.Tables(0).Rows(0)("kodpejabat").ToString.ToUpper()
                        lblBahagian.Text = ds.Tables(0).Rows(0)("dept_kod").ToString.ToUpper()

                        If ds.Tables(0).Rows(0)("unit_kod").ToString <> "0" Then
                            txtUnit.Text = ds.Tables(0).Rows(0)("unit_name").ToString.ToUpper()
                            lblUnit.Text = ds.Tables(0).Rows(0)("unit_kod").ToString.ToUpper()
                        Else
                            lblUnit.Text = lblBahagian.Text
                            txtUnit.Text = ds.Tables(0).Rows(0)("dept_name").ToString.ToUpper()

                        End If

                    End If

                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fCheckPeringkat(kod As Integer)
        If kod.Equals(1) Then
            trBahagian.Visible = False
            trUnit.Visible = False
        ElseIf kod.Equals(2) Then
            trUnit.Visible = False
        End If
    End Sub

    Private Sub fBindDdlJenisDasar()
        Try
            Dim strSql As String = $"Select BG_KodDasar, Butiran from BG_Dasar where Status = '1'"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            ddlJenDasar.DataSource = ds
            ddlJenDasar.DataTextField = "Butiran"
            ddlJenDasar.DataValueField = "BG_KodDasar"
            ddlJenDasar.DataBind()

        Catch ex As Exception
            'fErrorLog("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
        End Try
    End Sub

    Private Sub Mohonbaru()

        txtNamaPemohon.Text = Session("ssusrName")
        txtJawPemohon.Text = Session("ssusrPost")

        '  txtStatus.Text = fGetStatusDok("01").FirstOrDefault.Value
        txtPtj.Text = Session("ssusrPTj")

        fBindDdlKW()
        ddlKW.Items.Insert(0, New ListItem("-SILA PILIH-", ""))
        ddlKW.SelectedIndex = 0

        If trBahagian.Visible Then
            txtBahagian.Text = fGetBahagianPTJ(Session("ssusrKodBahagian")).ToUpper
        End If

        If trUnit.Visible Then

            txtUnit.Text = fGetUnitPTJ(Session("ssusrKodUnit")).ToUpper

        End If

            fBindDdlKodOperasi()
        ddlKodOperasi.Items.Insert(0, New ListItem("-SILA PILIH-", ""))
        ddlKodOperasi.SelectedIndex = 0

        fBindDdlJenisDasar()
        ddlJenDasar.Items.Insert(0, New ListItem("-SILA PILIH-", ""))
        ddlJenDasar.SelectedIndex = 0



        'txtMaksud.Text = fSelectMaksud()
        'txtAgensi.Text = fSelectNamaKorporat()

        txtJust.Text = ""
        txtProgram.Text = ""
        txtTarikh.Text = Date.Today.ToString("dd/MM/yyyy")

        Dim NextYear = Date.Now.AddYears(1).Year.ToString
        txtTahunBajet.Text = NextYear

        KemaskiniField(True)
        'lbtnKembali.Visible = True

    End Sub
    Protected Sub lbtnSaveButiran_Click(sender As Object, e As EventArgs) Handles lbtnSaveButiran.Click
        Page.Validate("btnSaveButiran")
        If Page.IsValid() Then
            'Dim Kuantiti As Integer = Convert.ToInt32(txtKuantiti.Text)
            'Dim txtAngJum = txtAngJum.Text
            Dim AngJumBelanja As Decimal = 0.00
            ' txtKuantiti.Text = "0" Then
            'AngJumBelanja = txtAngJum
            'Else
            'AngJumBelanja = CDec(Kuantiti * AngHrgUnit)
            'End If

            Dim noPermohonan = txtNoMohon.Text


            'check view state is not null 
            If ViewState("CurrentButiran") IsNot Nothing Then

                Dim drCurrentRow As DataRow = Nothing
                Dim value As Integer = 0
                'get datatable from view state 
                Dim dtCurrentTable As DataTable = DirectCast(ViewState("CurrentButiran"), DataTable)

                If dtCurrentTable.Rows.Count > 0 Then
                    value = dtCurrentTable.Rows.Count
                End If

                Dim aaa = ddlVotA.SelectedValue
                'add each row into data table
                drCurrentRow = dtCurrentTable.NewRow()
                'drCurrentRow("ID") = value + 1
                drCurrentRow("NoButiran") = value + 1
                drCurrentRow("KodVotA") = ddlVotA.SelectedValue
                drCurrentRow("KodVotSbg") = ddlVotSbg.SelectedValue
                drCurrentRow("Butiran") = txtButiranDt.Text
                drCurrentRow("AngHrgUnit") = txtAngJum.Text '.ToString("#,##0.00")

                dtCurrentTable.Rows.Add(drCurrentRow)

                ViewState("CurrentButiran") = dtCurrentTable
                clearTextButiran()
                BindGridViewButiran()



            End If

            'Clear textbox in Butiran Panel
            clearTextButiran()

        End If
    End Sub


    Private Sub gvButiran_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvButiran.RowCreated
        If e.Row.RowType = DataControlRowType.Footer Then
            'First cell Is used for specifying the Total text
            Dim intNoOfMergeCol = e.Row.Cells.Count - 5 ' /*except last column */
            For intCellCol As Integer = 1 To intNoOfMergeCol - 1
                e.Row.Cells.RemoveAt(1)
            Next

            ' e.Row.Cells(3).ColumnSpan = intNoOfMergeCol
            e.Row.Cells(3).Text = "Jumlah (RM)"
            e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(3).Font.Bold = True

            Dim TotalDebit As Decimal = ViewState("TotalAmount")
            e.Row.Cells(4).Text = TotalDebit.ToString("N2")
            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(4).Font.Bold = True



        End If
    End Sub

    Private Sub firstBindViewStateButiran()
        Try
            Dim dt As New DataTable()
            dt.Columns.AddRange(New DataColumn(5) {
                            New DataColumn("ID", GetType(String)),
                            New DataColumn("NoButiran", GetType(String)),
                            New DataColumn("KodVotA", GetType(String)),
                            New DataColumn("KodVotSbg", GetType(String)),
                            New DataColumn("Butiran", GetType(String)),
                            New DataColumn("AngHrgUnit", GetType(Decimal))
                            })

            'saving databale into viewstate   
            ViewState("CurrentButiran") = dt

            'bind Gridview
            BindGridViewButiran()
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub BindGridViewButiran()
        Try
            Dim dt = DirectCast(ViewState("CurrentButiran"), DataTable)

            'Dim total As Decimal= Convert.ToDecimal(
            Dim total As Decimal = dt.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("AngHrgUnit"))

            ViewState("TotalAmount") = total
            gvButiran.DataSource = dt
            gvButiran.DataBind()


        Catch ex As Exception
        End Try
    End Sub



    Private Sub clearTextButiran()

        txtAngJum.Text = ""
        txtButiranDt.Text = ""
        ddlVotA.SelectedIndex = 0
        'ddlVotSbg.SelectedIndex = -1
        ddlVotSbg.Items.Clear()
        lbtnKemaskini.Visible = False

    End Sub

    Private Sub KemaskiniField(Yes As Boolean)

        'StatusKemaskini = Yes
        'ddlBahagian.Enabled = Yes
        'ddlUnit.Enabled = Yes
        'ddlJenDasar.Enabled = Yes
        'ddlKodOperasi.Enabled = Yes
        'ddlVotSbg.Enabled = Yes
        'ddlKW.Enabled = Yes
        'ddlSaizRekod.Enabled = Yes
        'txtJust.Enabled = Yes
        'txtButiran.Enabled = Yes
        'txtKuantiti.Enabled = Yes
        'txtAngHrgUnit.Enabled = Yes
        'txtProgram.Enabled = Yes
        'gvButiran.Enabled = Yes
        'lbtnReset.Visible = Yes
        'lbtnSaveButiran.Visible = Yes
        'lbtnHantar.Visible = Not Yes
        'lbtnKemaskini.Visible = Not Yes
        'lbtnSimpan.Visible = Yes

        'Check if status=Permohonan Baru, then can edit, else just can view only.
        'If Not txtStatus.Text = "PERMOHONAN" Then
        '    lbtnHantar.Visible = False
        '    lbtnKemaskini.Visible = False
        '    lbtnHapus.Visible = False
        '    lbtnReset.Visible = False
        '    lbtnSaveButiran.Visible = False
        'End If

        'lbtnHapus.Visible = Not Yes
        'lbtnKembali.Visible = Not Yes
        'lbtnEditButiran.Visible = False
        'lbtnUndo.Visible = False
    End Sub

    Private Function fSelectMaksud() As String
        Dim Nama As String = ""
        Try
            Dim strSql As String = $"Select KodMaksud, NamaMaksud from BG_Maksud"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)
            Dim dt = ds.Tables(0)
            Dim row As DataRow = dt.Select().FirstOrDefault()

            If Not row Is Nothing Then
                Nama = row.Item("NamaMaksud").ToString.ToUpper()
                ViewState("KodMaksud") = row.Item("KodMaksud")
            End If
        Catch ex As Exception
            'fErrorLog("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
        End Try

        Return Nama

    End Function
    Private Function fSelectNamaKorporat() As String
        Dim KodKorporat = ""
        Try
            Dim strSql As String = $"Select KodKorporat from MK_Korporat"

            Dim ds As New DataSet
            'Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)
            Dim dt = ds.Tables(0)
            Dim row As DataRow = dt.Select().FirstOrDefault()

            If Not row Is Nothing Then
                KodKorporat = row.Item("KodKorporat")
                'ViewState("KodKorporat") = row.Item("KodKorporat")
            End If
            Return KodKorporat

        Catch ex As Exception
            'fErrorLog("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
        End Try



    End Function

    Protected Sub fBindDdlKW()
        Try
            Dim strSql As String = "Select KodKW, butiran, (KodKW +' - ' + butiran) as Butiran2 from mk_kw ORDER BY Kodkw "

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn

            ds = dbconn.fSelectCommand(strSql)
            ddlKW.DataSource = ds
            ddlKW.DataTextField = "Butiran2"
            ddlKW.DataValueField = "KodKW"
            ddlKW.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindDdlKodOperasi()
        Try
            Dim strSql As String = $"Select KodKO, (KodKO + ' - ' + Butiran) AS Butiran2 from MK_KodOperasi where Status = '1'"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            ddlKodOperasi.DataSource = ds
            ddlKodOperasi.DataTextField = "Butiran2"
            ddlKodOperasi.DataValueField = "KodKO"
            ddlKodOperasi.DataBind()
        Catch ex As Exception
            'fErrorLog("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
        End Try
    End Sub

    Private Sub fBindDdlVotAm()
        Try
            Dim strSql As String = "select KodVot, Butiran, (KodVot + ' - ' + Butiran) as Butiran2 from mk_vot where klasifikasi = 'H1' and (LEFT(KodVot,1) >= 1 AND LEFT(KodVot,1) <= 5) order by kodvot"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn

            ds = dbconn.fSelectCommand(strSql)

            ddlVotA.DataSource = ds
            ddlVotA.DataTextField = "Butiran2"
            ddlVotA.DataValueField = "KodVot"
            ddlVotA.DataBind()

        Catch ex As Exception
            'fErrorLog("Peruntukan_KW.aspx(fBindDdlVot)- " & ex.Message.ToString)
        End Try


    End Sub

    Private Sub fBindDdlVot(votA As String, votB As String)
        Try

            Dim strSql As String

            strSql = "select KodVot, Butiran, (KodVot + ' - ' + Butiran) as Butiran2 from mk_vot where klasifikasi = 'H2' and LEFT(kodvot,1) =  '" & votA & "'  and LEFT(kodvot,1) <> '0' order by kodvot"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn

            ds = dbconn.fSelectCommand(strSql)

            ddlVotSbg.DataSource = ds
            ddlVotSbg.DataTextField = "Butiran2"
            ddlVotSbg.DataValueField = "KodVot"
            ddlVotSbg.DataBind()

            If votB <> "0" Then
                ddlVotSbg.SelectedValue = votB
            End If



        Catch ex As Exception
            'fErrorLog("Peruntukan_KW.aspx(fBindDdlVot)- " & ex.Message.ToString)
        End Try


    End Sub



    Private Function fGetBahagianPTJ(KodBah As String) As String
        Dim strSql = $"Select NamaBahagian from MK_BahagianPtj WHERE KodBah='{KodBah}'"
        Dim dbconn As New DBKewConn
        Dim NamaBahagian = ""
        dbconn.sSelectCommand(strSql, NamaBahagian)
        'Dim s = ds.Tables(0).Rows(0)("NamaBahagian").ToString
        Return NamaBahagian
    End Function

    Private Function fGetUnitPTJ(KodUnit As String) As String
        'Dim ds As New DataSet
        Dim NamaUnit = ""
        Try
            Dim strSql = $"Select NamaUnit from MK_UnitPtj WHERE kodUnit='{KodUnit}'"

            Dim dbconn As New DBKewConn
            dbconn.sSelectCommand(strSql, NamaUnit)

            Return NamaUnit
        Catch ex As Exception
            'fErrorLog("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
        End Try
        Return NamaUnit
    End Function

    Private Sub MohonEdit(NoMohon As String)
        'Disable all the input fields
        KemaskiniField(False)

        Try
            Dim dsMohon As New DataSet
            Dim dsMohonStatus As New DataSet
            Dim dsMohonDt As New DataSet
            Dim dbconn As New DBKewConn
            Dim dtUserInfo As New DataTable

            txtNoMohon.Text = NoMohon

            Dim strSql = $"Select KodPtj,KodBahagian,KodUnitPtj,KodMaksud,KodDasar,KodKW, BG20_TarikhMohon,BG20_TahunBajet,
                            KodOperasi,BG20_StaffIDPemohon,BG20_StaffIDPenyokong,BG20_StaffIDPengesah,StatusDok,BG20_AmaunMohon,
                            BG20_Justifikasi,BG20_Program, StatusDok from BG01_Mohon where BG20_NoMohon='{NoMohon}' "

            dsMohon = dbconn.fSelectCommand(strSql)

            Dim KodPTj = dsMohon.Tables(0).Rows(0)("KodPtj").ToString
            Dim KodBahagian = dsMohon.Tables(0).Rows(0)("KodBahagian").ToString
            Dim KodUnit = dsMohon.Tables(0).Rows(0)("KodUnitPtj").ToString
            Dim KodMaksud = dsMohon.Tables(0).Rows(0)("KodMaksud").ToString
            Dim KodDsr = dsMohon.Tables(0).Rows(0)("KodDasar").ToString
            Dim tarikh As Date = dsMohon.Tables(0).Rows(0)("BG20_TarikhMohon")
            Dim TahunBajet = dsMohon.Tables(0).Rows(0)("BG20_TahunBajet").ToString
            Dim KodOperasi = dsMohon.Tables(0).Rows(0)("KodOperasi").ToString
            Dim KodKW = dsMohon.Tables(0).Rows(0)("KodKW").ToString
            Dim PemohonStafID = dsMohon.Tables(0).Rows(0)("BG20_StaffIDPemohon").ToString
            Dim PenyokongStafID = dsMohon.Tables(0).Rows(0)("BG20_StaffIDPenyokong").ToString
            Dim PengesahStafID = dsMohon.Tables(0).Rows(0)("BG20_StaffIDPengesah").ToString
            Dim statsuDokMohon = dsMohon.Tables(0).Rows(0)("StatusDok").ToString

            Dim open
            If (statsuDokMohon <> "01") And (statsuDokMohon <> "18") And (statsuDokMohon <> "20") And (statsuDokMohon <> "06") Then
                lblHiddenOpen.Text = "0"
                lblHiddenOpen.Visible = False
            Else
                lblHiddenOpen.Text = "1"
                lblHiddenOpen.Visible = False
            End If



            Dim strSemakStatus = $"select BG12_Ulasan from BG12_StatusDok where BG01_NoPermohonan = '{NoMohon}'
                                and KodStatusDok = '{statsuDokMohon}'
                                order by BG12_Bil desc"
            dsMohonStatus = dbconn.fSelectCommand(strSemakStatus)
            Dim statusD = dsMohonStatus.Tables(0).Rows(0)("BG12_Ulasan").ToString
            lblTextUlasan.Text = "Ulasan"
            txtUlasan.Text = statusD
            txtUlasan.Visible = True

            'Bahagian
            If Not KodBahagian.Equals("0000") Then
                'Has kodbahagian
                txtBahagian.Text = fGetBahagianPTJ(KodBahagian).ToUpper

                'Check Unit
                If Not KodUnit.Equals("000000") Then
                    ' Has KodUnit
                    If KodUnit = KodBahagian Then
                        txtUnit.Text = fGetBahagianPTJ(KodBahagian).ToUpper
                    Else
                        txtUnit.Text = fGetUnitPTJ(KodUnit).ToUpper
                    End If
                Else
                    'Does not have KodUnit
                    fCheckPeringkat(2)
                End If
            Else
                'Does not have KodBahagain
                fCheckPeringkat(1)
            End If

            fBindDdlJenisDasar()
            fBindDdlKodOperasi()
            fBindDdlKW()

            Dim StatusDok = fGetStatusDok(dsMohon.Tables(0).Rows(0)("StatusDok").ToString)
            KodStatus = StatusDok.FirstOrDefault.Key

            If Not KodStatus.Equals("01") Then
                'lbtnHantar.Visible = False
                'lbtnKemaskini.Visible = False
                lbtnHapus.Visible = False
                lbtnReset.Visible = False
                lbtnSaveButiran.Visible = False
            End If

            txtStatus.Text = StatusDok.FirstOrDefault.Value
            txtPtj.Text = fGetPTJ(KodPTj)
            txtTarikh.Text = tarikh.ToShortDateString



            ddlKodOperasi.Items.FindByValue(KodOperasi.Trim()).Selected = True
            ddlJenDasar.Items.FindByValue(KodDsr.Trim()).Selected = True
            ddlKW.Items.FindByValue(KodKW.Trim()).Selected = True

            'txtMaksud.Text = fGetMaksud(KodMaksud)
            'txtAgensi.Text = fSelectNamaKorporat()
            'txtJenDasar.Text = fGetJenisDasar(KodDsr)
            txtJust.Text = dsMohon.Tables(0).Rows(0)("BG20_Justifikasi").ToString.Replace("\", "'")
            'ddlTahunBajet.SelectedIndex = ddlTahunBajet.Items.IndexOf(ddlTahunBajet.Items.FindByText(TahunBajet))
            txtTahunBajet.Text = TahunBajet

            txtProgram.Text = dsMohon.Tables(0).Rows(0)("BG20_Program").ToString.Replace("\", "'")

            txtNamaPemohon.Text = Session("ssusrName")
            txtJawPemohon.Text = Session("ssusrPost")

            If Not String.IsNullOrEmpty(PenyokongStafID) Then

                dtUserInfo = fGetUserInfo(PenyokongStafID)

                If dtUserInfo.Rows.Count > 0 Then
                    txtNamaPenyokong.Text = dtUserInfo.Rows.Item(0).Item("MS01_Nama")
                    txtJawPenyokong.Text = dtUserInfo.Rows.Item(0).Item("JawGiliran")
                End If
            End If

            If Not String.IsNullOrEmpty(PengesahStafID) Then

                dtUserInfo = fGetUserInfo(PengesahStafID)

                If dtUserInfo.Rows.Count > 0 Then
                    txtNamaPengesah.Text = dtUserInfo.Rows.Item(0).Item("MS01_Nama")
                    txtJawPengesah.Text = dtUserInfo.Rows.Item(0).Item("JawGiliran")
                End If
            End If

            Dim strSqlDt = $"Select BG20_Id, BG20_NoButiran,BG20_Butiran,BG20_Kuantiti,BG20_AngHrgUnit,BG20_AngJumlah,KodVotSebagai,KodVotAm, BG20_Butiran from BG01_MohonDt where BG20_NoMohon='{NoMohon}' ORDER BY KodVotAm, KodVotSebagai "
            dsMohonDt = dbconn.fSelectCommand(strSqlDt)

            Dim drCurrentRow As DataRow = Nothing

            'get datatable from view state 
            Dim dtCurrentTable As DataTable = DirectCast(ViewState("CurrentButiran"), DataTable)

            For Each rowdt In dsMohonDt.Tables(0).Rows
                'add each row into data table
                drCurrentRow = dtCurrentTable.NewRow()
                drCurrentRow("ID") = rowdt("BG20_Id").ToString
                drCurrentRow("NoButiran") = rowdt("BG20_NoButiran").ToString
                drCurrentRow("KodVotA") = rowdt("KodVotAm").ToString
                drCurrentRow("KodVotSbg") = rowdt("KodVotSebagai").ToString
                drCurrentRow("Butiran") = rowdt("BG20_Butiran").ToString.Replace("\", "'")
                drCurrentRow("AngHrgUnit") = CDec(rowdt("BG20_AngHrgUnit")) '.ToString("#,##0.00")
                dtCurrentTable.Rows.Add(drCurrentRow)
            Next

            'saving datatable into viewstate
            ViewState("CurrentButiran") = dtCurrentTable

            'bind GridView
            BindGridViewButiran()

            'Populate ddl Vot
            fBindDdlVot("0", "0")
            ddlVotSbg.Items.Insert(0, New ListItem("-SILA PILIH-", ""))
            ddlVotSbg.SelectedIndex = 0
        Catch ex As Exception
            'fErrorLog("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
        End Try
    End Sub

    Private Function fGetPTJ(KodPTJ As String) As String
        Dim strSql = $"SELECT Butiran FROM MK_PTJ WHERE KODPTJ='{KodPTJ}'"
        'Dim ds As New DataSet
        Dim dbconn As New DBKewConn
        Dim NamaPTJ = ""
        dbconn.sSelectCommand(strSql, NamaPTJ)
        'ds = dbconn.fselectCommand(strSql)
        Return NamaPTJ 'ds.Tables(0).Rows(0)("Butiran").ToString
    End Function

    Private Function fGetMaksud(KodMak As String) As String
        Dim ds As New DataSet
        Dim NamaMaksud = ""
        Try
            Dim strSql As String = $"Select NamaMaksud from BG_Maksud where KodMaksud='{KodMak}'"

            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)
            NamaMaksud = ds.Tables(0).Rows(0)("NamaMaksud").ToString

        Catch ex As Exception
            'fErrorLog("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
        End Try

        Return NamaMaksud
    End Function

    Protected Sub lbtnKembali_Click(sender As Object, e As EventArgs) Handles lbtnKembali.Click

        'check if statuskemaskini is true and No mohon is already created

        Response.Redirect("~/FORMS/BAJET/PERMOHONAN BAJET/Permohonan_Bajet.aspx")

    End Sub

    Protected Sub lbtnReset_Click(sender As Object, e As EventArgs) Handles lbtnReset.Click
        clearTextButiran()
    End Sub
    Private Sub ddlVotA_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlVotA.SelectedIndexChanged
        Dim strA = Left(ddlVotA.SelectedValue.ToString(), 1)
        fBindDdlVot(strA, "0")
    End Sub


    Protected Sub lbtnHapus_Click(sender As Object, e As EventArgs) Handles lbtnHapus.Click

        dbconn.sConnBeginTrans()
        Dim strSQL_DeleteMain = $"DELETE FROM BG01_Mohon Where BG20_NoMohon = @nomohon"
        Dim strSQL_Delete = $"DELETE FROM BG01_MohonDt Where BG20_NoMohon = @nomohon"
        Dim paramSql_delete() As SqlParameter =
         {
         New SqlParameter("@nomohon", txtNoMohon.Text)
         }

        dbconn.fUpdateCommand(strSQL_DeleteMain, paramSql_delete)
        dbconn.sConnCommitTrans()

        dbconn.fUpdateCommand(strSQL_Delete, paramSql_delete)
        dbconn.sConnCommitTrans()

        countButiran = 1

        If countButiran > 0 Then
            fGlobalAlert($"Rekod telah dipadam!", Me.Page, Me.[GetType](), "../../BAJET/PERMOHONAN BAJET/Permohonan_Bajet.aspx")
        Else
            fGlobalAlert("Maklumat Butiran tidak berjaya dipadam!", Me.Page, Me.[GetType]())
        End If
    End Sub
    Protected Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.Click
        Try
            Page.Validate("btnSimpan")

            If Page.IsValid Then
                Dim noPermohonan As String = ""
                Dim idDt As String = ""
                Dim sqlDt As String = ""
                Dim tarikhMohon As DateTime
                Dim strSql As String = ""
                Dim strSqlButiran = ""
                Dim paramSqlBtrn() As SqlParameter = Nothing
                Dim dbconn As New DBKewConn
                Dim AngHrgUnit As Decimal = 0.00
                Dim AngJumlah As Decimal = 0.00
                Dim dtCurrentTable As DataTable = DirectCast(ViewState("CurrentButiran"), DataTable)
                Dim dsMohon As New DataSet
                Dim dsMohonDt As New DataSet
                Dim resultUpdate = True
                Dim dr As DataRow = Nothing
                Dim info As String


                Dim unitStaf_ As String
                If lblUnit.Text = "0" Or lblUnit.Text = "" Or lblUnit.Text = "TIADA DILAPORKAN" Then
                    unitStaf_ = lblBahagian.Text
                Else
                    unitStaf_ = lblUnit.Text
                End If


                If dtCurrentTable.Rows.Count = 0 Then
                    fGlobalAlert("Maklumat Butiran Permohonan tidak dimasukkan. Sila masukkan maklumat butiran permohonan!", Page, [GetType]())
                    Exit Sub
                End If


                'Check if Butiran field is not filled up yet.
                'If dtCurrentTable.Rows(0)(1).ToString Is String.Empty Then
                'fGlobalAlert("Maklumat Butiran tidak dimasukkan. Sila masukkan maklumat butiran!", Page, [GetType]())
                'Exit Sub
                'Else
                'fGlobalAlert("Maklumat Butiran tidak dimasukkan. Sila masukkan maklumat butiran!", Page, [GetType]())
                'Exit Sub
                'End If

                'Check No Mohon is appeared or not in text field of txtNoMohon.
                'If appear, then it is for edit mode
                'Otherwise, it is for add new record
                If txtNoMohon.Text IsNot String.Empty Then
                        'Edit mode

                        noPermohonan = txtNoMohon.Text
                        Dim program = txtProgram.Text
                        Dim justifikasi = txtJust.Text
                        'Dim strSqlMohon__ = $"Select BG20_NoMohon, KodBahagian,KodUnitPtj,KodDasar,KodOperasi, KodKW, BG20_Program,BG20_Justifikasi,BG20_AmaunMohon from BG01_Mohon where BG20_NoMohon='{noPermohonan}'"

                        '
                        'dsBajet = dbconn.fSelectCommand(strSqlMohon__)

                        ' dbconn.sConnBeginTrans()
                        'If dsBajet.Tables(0).Rows.Count > 0 Then
                        'dr = dsBajet.Tables(0).Rows(0)
                        'dr("KodBahagian") = fGetKodBah()
                        'dr("KodUnitPtj") = fGetKodUnit()
                        'dr("KodDasar") = ddlJenDasar.SelectedValue
                        'dr("KodOperasi") = ddlKodOperasi.SelectedValue
                        'dr("BG20_Program") = txtProgram.Text
                        'dr("KodKW") = ddlKW.SelectedValue
                        'dr("BG20_Justifikasi") = txtJust.Text
                        'dr("BG20_AmaunMohon") = ViewState("TotalAmount")
                        ' dsMohon.Tables("MohonDs").Rows.Add(dr)
                        'dsBajet.Tables(0).Rows.Add(dr)
                        'sAuditLog(Session("ssusrID"), Request.QueryString("KodSubMenu"), "BG20_Mohon", "2", "Edit Permohonan Bajet") 'TransID: 1-Insert,2-Update,3-Delete



                        Dim strSQL_Save = $"UPDATE 
                    BG01_Mohon SET KodBahagian = @bahagian, KodUnitPtj=@unit, KodDasar=@dasar, KodOperasi =@operasi, BG20_Program = @Program,
                    KodKW = @kodkw, BG20_Justifikasi = @justifikasi,  BG20_AmaunBajet = @amaun , BG20_AmaunMohon = @amaun, BG20_AmaunMohon_KB = @amaun, BG20_AmaunMohon_BajetPTJ = @amaun, BG20_AmaunMohon_KetuaPTJ = @amaun, BG20_AmaunSyorBendahari = @amaun, BG20_AmaunLulusNC = @amaun, BG20_AmaunLulusLPU = @amaun, BG20_AmaunBendahari = @amaun , StatusDok = @StatusDok Where BG20_NoMohon = '{noPermohonan}'"

                        Dim paramSql_simpan() As SqlParameter =
                         {
                         New SqlParameter("@bahagian", lblBahagian.Text),'fGetKodBah()),                         
                         New SqlParameter("@unit", unitStaf_),  'fGetKodUnit()),
                         New SqlParameter("@dasar", ddlJenDasar.SelectedValue),
                         New SqlParameter("@operasi", ddlKodOperasi.SelectedValue),
                         New SqlParameter("@Program", program.Replace("'", "\")),
                         New SqlParameter("@KodKW", ddlKW.SelectedValue),
                         New SqlParameter("@justifikasi", justifikasi.Replace("'", "\")),
                         New SqlParameter("@amaun", ViewState("TotalAmount")),
                         New SqlParameter("@StatusDok", "01")
                         }

                        Dim strSQLStatusDok = "INSERT INTO BG12_StatusDok (BG01_NoPermohonan, KodStatusDok, BG12_TkhProses, BG12_NoStaf, BG12_Ulasan) " &
                       " VALUES (@NoMohon, @StatusKod, @Tarikh, @NoStaf,@Ulasan)"

                        Dim paramSql2() As SqlParameter =
                  {
                     New SqlParameter("@NoMohon", noPermohonan),
                     New SqlParameter("@StatusKod", "01"),
                     New SqlParameter("@Tarikh", tarikhMohon),
                     New SqlParameter("@NoStaf", Session("ssusrID")),
                     New SqlParameter("@Ulasan", "-")
                     }


                        dbconn.sConnBeginTrans()
                        If dbconn.fUpdateCommand(strSQL_Save, paramSql_simpan) > 0 Then
                            dbconn.fUpdateCommand(strSQL_Save, paramSql_simpan)
                            dbconn.sConnCommitTrans()

                            Dim strSQL_Delete = $"DELETE FROM BG01_MohonDt Where BG20_NoMohon = @nomohon and BG20_Status = '0'"
                            '"
                            Dim paramSql_delete() As SqlParameter =
                        {
                         New SqlParameter("@nomohon", noPermohonan)
                         }

                            dbconn.fUpdateCommand(strSQL_Delete, paramSql_delete)
                            dbconn.sConnCommitTrans()


                            countButiran = 0
                            For Each drCurrentRow As DataRow In dtCurrentTable.Rows
                                'AngHrgUnit = Convert.ToDecimal(drCurrentRow("AngHrgUnit"))
                                'AngJumlah = Convert.ToDecimal(drCurrentRow("AngJumlah"))



                                ''check if id exist


                                If IsDBNull(drCurrentRow("ID")) Then

                                    AngHrgUnit = drCurrentRow("AngHrgUnit")
                                    'AngJumlah = drCurrentRow("AngJumlah")
                                    info = drCurrentRow("Butiran")

                                strSqlButiran = "INSERT INTO BG01_MohonDt ( BG20_NoButiran, BG20_NoMohon, KodVotAm, KodVotSebagai, BG20_AngHrgUnit, BG20_AngJumlah, BG20_JumKB, BG20_JumKewPTj, BG20_JumKetuaPTj, BG20_JumBendahari, BG20_JumNC, BG20_JumLPU,  BG20_Status, BG20_Butiran)
                                            VALUES (@NoButiran, @NoMohon ,@KodVotAm, @KodVotSbg, @AngHargaSeUnit, @AngJumlah, @AngJumlah, @AngJumlah , @AngJumlah, @AngJumlah, @AngJumlah , @AngJumlah , @AngJumlah ,  @Status, @Butiran )"
                                paramSqlBtrn = {
                                New SqlParameter("@NoButiran", drCurrentRow("NoButiran")),
                                New SqlParameter("@NoMohon", noPermohonan),
                                New SqlParameter("@KodVotAm", drCurrentRow("KodVotA")),
                                New SqlParameter("@KodVotSbg", drCurrentRow("KodVotSbg")),
                                New SqlParameter("@AngHargaSeUnit", AngHrgUnit),
                                New SqlParameter("@AngJumlah", AngHrgUnit),
                                New SqlParameter("@Butiran", info.Replace("'", "\")),
                                New SqlParameter("@Status", True)
                            }

                                    If dbconn.fInsertCommand(strSqlButiran, paramSqlBtrn) > 0 Then
                                        countButiran = countButiran + 1
                                        KodStatus = "01"
                                        dbconn.sConnCommitTrans()
                                        'sAuditLog(Session("ssusrID"), Request.QueryString("KodSubMenu"), "BG20_Mohon", "1", "Simpan Button")
                                    Else
                                        dbconn.sConnRollbackTrans()
                                        'fErrorLog("Jenis_Permohonan.aspx(btnSave_Click)- " & ex.Message.ToString)
                                    End If


                                Else
                                    Dim strSQL_ = $"UPDATE 
                                BG01_MohonDt SET BG20_NoButiran = @NoButiran, BG20_NoMohon=@NoMohon, BG20_Butiran=@Butiran, BG20_AngHrgUnit = @AngHargaSeUnit,
                                BG20_AngJumlah = @AngJumlah, BG20_JumKB = @AngJumlah, BG20_JumKewPTj = @AngJumlah, BG20_JumKetuaPTj = @AngJumlah, BG20_JumBendahari = @AngJumlah, BG20_JumNC = @AngJumlah, BG20_JumLPU = @AngJumlah,  BG20_Status = @Status,  KodVotAm = @KodVotAm, KodVotSebagai = @KodVotSbg  Where BG20_NoMohon = '{noPermohonan}' and BG20_Id = '{drCurrentRow("ID")}'"

                                    info = drCurrentRow("Butiran")
                                    AngHrgUnit = drCurrentRow("AngHrgUnit")

                                    Dim paramSql_() As SqlParameter =
                                     {
                                    New SqlParameter("@NoButiran", drCurrentRow("NoButiran")),
                                    New SqlParameter("@NoMohon", noPermohonan),
                                    New SqlParameter("@KodVotAm", drCurrentRow("KodVotA")),
                                    New SqlParameter("@KodVotSbg", drCurrentRow("KodVotSbg")),
                                    New SqlParameter("@AngHargaSeUnit", AngHrgUnit),
                                    New SqlParameter("@AngJumlah", AngHrgUnit),
                                    New SqlParameter("@Butiran", info.Replace("'", "\")),
                                    New SqlParameter("@Status", True)
                                     }


                                    dbconn.sConnBeginTrans()
                                    If dbconn.fUpdateCommand(strSQL_, paramSql_) > 0 Then
                                        countButiran = countButiran + 1
                                        dbconn.fUpdateCommand(strSQL_, paramSql_)
                                        dbconn.sConnCommitTrans()
                                    End If



                                End If


                            Next

                            fUpload(noPermohonan)


                            If countButiran > 0 Then

                                If dbconn.fInsertCommand(strSQLStatusDok, paramSql2) > 0 Then
                                    dbconn.sConnCommitTrans()
                                End If

                                fGlobalAlert($"Rekod baru telah ditambah bersama '{countButiran.ToString}' butiran permohonan!", Me.Page, Me.[GetType](), "../../BAJET/PERMOHONAN BAJET/Permohonan_Bajet.aspx")
                            Else
                                fGlobalAlert("Maklumat Butiran tidak berjaya disimpan!", Me.Page, Me.[GetType]())
                            End If
                        End If



                        'End If

                        'fGlobalAlert($"Rekod telah dikemaskini!", Me.Page, Me.[GetType](), "../../BAJET/PERMOHONAN BAJET/Permohonan_Bajet_Tahunan.aspx")
                        'End Using


                    Else
                        'Session("ssusrID") = "00245"

                        Dim ptjj = Session("ssusrKodPTj")
                        'Add new record
                        noPermohonan = fGetNoMohon()
                        tarikhMohon = DateTime.ParseExact(txtTarikh.Text, "dd/MM/yyyy", New CultureInfo("en-US"))

                        strSql = "INSERT INTO BG01_MOHON (BG20_NoMohon,BG20_TarikhMohon,KodPtj,KodBahagian,KodUnitPtj,KodKorporat,
                            BG20_Program,BG20_Justifikasi,BG20_TahunBajet,KodDasar,KodOperasi,KodKW,BG20_Status,StatusDok,BG20_AmaunBajet, BG20_AmaunMohon,BG20_AmaunMohon_KB, BG20_AmaunMohon_BajetPTJ, BG20_AmaunMohon_KetuaPTJ, BG20_AmaunSyorBendahari, BG20_AmaunLulusNC, BG20_AmaunBendahari, BG20_StaffIDPemohon, BG20_AmaunLulusLPU)
                            VALUES (@NoMohon, @tarikhMohon, @KodPTJ, @KodBah, @KodUnit, @KodAgensi,
                            @Program, @Justification, @TahunBajet, @KodJenDsr, @KodOperasi, @KodKW, @Status,  @StatusDok,  @AmaunMohon, @AmaunMohon, @AmaunMohon,@AmaunMohon,@AmaunMohon,@AmaunMohon,@AmaunMohon,@AmaunMohon, @idMohon,@AmaunMohon )"

                        Dim paramSql() As SqlParameter = {
                        New SqlParameter("@NoMohon", noPermohonan),
                        New SqlParameter("@tarikhMohon", tarikhMohon),
                        New SqlParameter("@KodPTJ", lblPtj.Text),'Left(Session("ssusrKodPTj"), 2)),
                        New SqlParameter("@KodBah", lblBahagian.Text),'fGetKodBah()),
                        New SqlParameter("@KodUnit", unitStaf_),'fGetKodUnit()),
                        New SqlParameter("@KodAgensi", fSelectNamaKorporat()),
                        New SqlParameter("@Program", txtProgram.Text.Replace("'", "\")),
                        New SqlParameter("@Justification", txtJust.Text.Replace("'", "\")),
                        New SqlParameter("@TahunBajet", txtTahunBajet.Text),
                        New SqlParameter("@KodJenDsr", ddlJenDasar.SelectedValue),
                        New SqlParameter("@KodOperasi", ddlKodOperasi.SelectedValue),
                        New SqlParameter("@KodKW", ddlKW.SelectedValue),
                        New SqlParameter("@Status", True),
                        New SqlParameter("@StatusDok", "01"),
                        New SqlParameter("@AmaunMohon", ViewState("TotalAmount")),
                        New SqlParameter("@idMohon", Session("ssusrID"))
                    }

                        Dim strSQLStatusDok = "INSERT INTO BG12_StatusDok (BG01_NoPermohonan, KodStatusDok, BG12_TkhProses, BG12_NoStaf, BG12_Ulasan) " &
                       " VALUES (@NoMohon, @StatusKod, @Tarikh, @NoStaf,@Ulasan)"

                        Dim paramSql2() As SqlParameter =
                  {
                     New SqlParameter("@NoMohon", noPermohonan),
                     New SqlParameter("@StatusKod", "01"),
                     New SqlParameter("@Tarikh", tarikhMohon),
                     New SqlParameter("@NoStaf", Session("ssusrID")),
                     New SqlParameter("@Ulasan", "-")
                     }

                        dbconn.sConnBeginTrans()
                        If dbconn.fInsertCommand(strSql, paramSql) > 0 Then

                            If dbconn.fInsertCommand(strSQLStatusDok, paramSql2) > 0 Then
                                dbconn.sConnCommitTrans()
                            End If

                            'dbconn.sUpdateCommand2(strSQLStatusDok, paramSql2, True, True, resultUpdate, resultCommit)

                            'sAuditLog(Session("ssusrID"), Request.QueryString("KodSubMenu"), "BG20_Mohon", "1", "Simpan Button") 'TransID: 1-Insert,2-Update,3-Delete

                            countButiran = 0
                            For Each drCurrentRow As DataRow In dtCurrentTable.Rows
                                'AngHrgUnit = Convert.ToDecimal(drCurrentRow("AngHrgUnit"))
                                'AngJumlah = Convert.ToDecimal(drCurrentRow("AngJumlah"))
                                AngHrgUnit = drCurrentRow("AngHrgUnit")
                            'AngJumlah = drCurrentRow("AngJumlah")

                            strSqlButiran = "INSERT INTO BG01_MohonDt ( BG20_NoButiran, BG20_NoMohon, KodVotAm, KodVotSebagai, BG20_AngHrgUnit, BG20_AngJumlah,  BG20_JumKB, BG20_JumKewPTj, BG20_JumKetuaPTj, BG20_JumBendahari, BG20_JumNC,BG20_JumLPU, BG20_Status, BG20_Butiran, BG20_Aktif)
                                            VALUES (" & (countButiran + 1) & ", @NoMohon ,@KodVotAm, @KodVotSbg, @AngHargaSeUnit, @AngJumlah, @AngJumlah, @AngJumlah , @AngJumlah, @AngJumlah, @AngJumlah, @AngJumlah, @Status, @Butiran, @Aktif )"
                            paramSqlBtrn = {
                                New SqlParameter("@NoMohon", noPermohonan),
                                New SqlParameter("@KodVotAm", drCurrentRow("KodVotA")),
                                New SqlParameter("@KodVotSbg", drCurrentRow("KodVotSbg")),
                                New SqlParameter("@AngHargaSeUnit", AngHrgUnit),
                                New SqlParameter("@AngJumlah", AngHrgUnit),
                                New SqlParameter("@Butiran", drCurrentRow("Butiran").Replace("'", "\")),
                                New SqlParameter("@Status", True),
                                New SqlParameter("@Aktif", True)
                            }

                                If dbconn.fInsertCommand(strSqlButiran, paramSqlBtrn) > 0 Then
                                    countButiran = countButiran + 1
                                    KodStatus = "01"
                                    dbconn.sConnCommitTrans()
                                    'sAuditLog(Session("ssusrID"), Request.QueryString("KodSubMenu"), "BG20_Mohon", "1", "Simpan Button")
                                Else
                                    dbconn.sConnRollbackTrans()
                                    'fErrorLog("Jenis_Permohonan.aspx(btnSave_Click)- " & ex.Message.ToString)
                                End If

                            Next

                            fUpload(noPermohonan)
                            UpdateNoAkhir()

                            If countButiran > 0 Then
                                fGlobalAlert($"Rekod telah dikemaskini bersama '{countButiran.ToString}' butiran permohonan!", Me.Page, Me.[GetType](), "../../BAJET/PERMOHONAN BAJET/Permohonan_Bajet.aspx")
                            Else
                                fGlobalAlert("Maklumat Butiran tidak berjaya disimpan!", Me.Page, Me.[GetType]())
                            End If

                        Else
                            dbconn.sConnRollbackTrans()
                            fGlobalAlert("Maklumat tidak berjaya disimpan!", Me.Page, Me.[GetType]())
                        End If
                    End If

                End If
        Catch ex As Exception
            'fErrorLog("Jenis_Permohonan.aspx(btnSave_Click)- " & ex.Message.ToString)
        End Try
    End Sub

    Private Function fGetKodBah() As String
        Dim KodBah = "0000"

        If ViewState("KodPeringkat") = 2 Or ViewState("KodPeringkat") = 3 Then
            KodBah = Session("ssusrKodBahagian")
        End If

        Return KodBah

    End Function
    Private Function fGetKodUnit() As String
        Dim KodUnit = "000000"

        If ViewState("KodPeringkat") = 3 Then
            KodUnit = Session("ssusrKodUnit")
        End If

        Return KodUnit
    End Function
    Private Function fGetNoMohon()
        'BG(2A)-PTJ(2N)-Bh(2N)-Unit(2N)-RunningNo(6N)-Bulan(2N)-Tahun(2N) = 18Digit
        Dim month = Date.Now.Month.ToString()
        Dim year = Date.Now.Year.ToString.Substring(2, 2)
        'Dim noMohon As String = "BG" + Left(Session("ssusrKodPTj"), 2) + fGetKodBah().Substring(2) + fGetKodUnit().Substring(4) + fGetRunnningNo() + month + year
        Dim noMohon As String = "BG" + lblPtj.Text + lblBahagian.Text + lblUnit.Text + fGetRunnningNo() + month + year

        Return noMohon
    End Function

    ''' <summary>
    ''' Get 6 digit of running number base on same PTJ and year
    ''' </summary>
    ''' <returns>running number</returns>
    Private Function fGetRunnningNo()
        Dim runningNo As String = ""
        Dim runningNo_ As Integer = 0
        Try
            'Dim strSqlNo = $"Select BG20_NoMohon FROM BG01_Mohon WHERE kodptj ='{Left(Session("ssusrKodPTj"), 2)}' AND BG20_TahunBajet = '{txtTahunBajet.Text}' ORDER BY ID DESC"
            Dim strSqlNo = $"SELECT KodModul, Prefix, NoAkhir FROM MK_NoAkhir WHERE KodModuL = 'BG' AND Prefix = 'BG' AND KodPTJ ='{lblPtj.Text}' AND Tahun = '{Date.Now.Year}'"

            Dim dbconn As New DBKewConn
            Dim ds = dbconn.fSelectCommand(strSqlNo)

            'get first row noMohon
            runningNo_ = ds.Tables(0).Rows(0)("NoAkhir") + 1
            'runningNo = noMohon.Substring(8, 6)

            'Dim intRunNo = CInt(runningNo) + 1

            'Two character of running number.
            runningNo = runningNo_.ToString("000000")

        Catch ex As Exception
            'fErrorLog("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
        End Try

        Return runningNo
    End Function

    Private Sub UpdateNoAkhir()
        Dim year = Date.Now.Year
        'Dim KodPtj = Session("ssusrKodPTj").ToString.PadRight(6, "0")

        Dim strSql = "SELECT KodModul, Prefix, NoAkhir, Tahun, Butiran, kodPTJ, ID From MK_NoAkhir WHERE KodModul = @KodModull AND Prefix = @Prefixx AND Tahun = @years and kodptj = @KodPtjj"
        Dim paramSql() As SqlParameter = {
            New SqlParameter("@KodModull", "BG"),
            New SqlParameter("@Prefixx", "BG"),
            New SqlParameter("@years", Date.Now.Year),
            New SqlParameter("@KodPtjj", lblPtj.Text)
            }

        Dim dbconn As New DBKewConn
        Dim ds = dbconn.fSelectCommand(strSql, "MKNoAkhir", paramSql)

        If ds.Tables(0).Rows.Count > 0 Then
            ds.Tables("MKNoAkhir").Rows(0)("NoAkhir") = ds.Tables("MKNoAkhir").Rows(0)("NoAkhir") + 1
        Else
            Dim dr As DataRow
            dr = ds.Tables("MKNoAkhir").NewRow
            dr("KodModul") = "BG"
            dr("Prefix") = "BG"
            dr("noakhir") = 1
            dr("Tahun") = year
            dr("Butiran") = "Mohon Bajet"
            dr("kodPTJ") = lblPtj.Text
            ds.Tables("MKNoAkhir").Rows.Add(dr)
        End If

        dbconn.sUpdateCommand(ds, strSql)

    End Sub

    Protected Sub gvButiran_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvButiran.RowDeleting

        Dim vsDt As DataTable = TryCast(ViewState("CurrentButiran"), DataTable)
        Dim selectedRow As GridViewRow = gvButiran.SelectedRow

        Dim idDt_ As Label = CType(selectedRow.FindControl("lblIDDt"), Label)
        Dim idDt = idDt_.Text

        If txtNoMohon.Text <> "" Then
            'coding update status jdi 0
            Dim strSQL_ = $"UPDATE 
                                BG01_MohonDt SET  BG20_Status = @Status Where BG20_NoMohon = '{txtNoMohon.Text}' and BG20_Id = '{idDt}'"



            Dim paramSql_() As SqlParameter =
                                         {
                                        New SqlParameter("@Status", False)
                                         }


            dbconn.sConnBeginTrans()
            If dbconn.fUpdateCommand(strSQL_, paramSql_) > 0 Then
                dbconn.fUpdateCommand(strSQL_, paramSql_)
                dbconn.sConnCommitTrans()
            End If
        End If


        'stop

        vsDt.Rows.RemoveAt(e.RowIndex)


        ViewState("CurrentButiran") = vsDt
        BindGridViewButiran()


    End Sub


    Protected Sub gvButiran_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvButiran.SelectedIndexChanged
        Dim selectedRow As GridViewRow = gvButiran.SelectedRow

        lblIDDt_.Text = TryCast(selectedRow.FindControl("lblIDDt"), Label).Text.ToString
        ddlVotA.SelectedValue = TryCast(selectedRow.FindControl("StrKodVotA"), Label).Text.ToString

        Dim strBg = TryCast(selectedRow.FindControl("StrKodVotSbg"), Label).Text.ToString
        fBindDdlVot(Left(ddlVotA.SelectedValue, 1), strBg)
        'txtAngJum.Text = TryCast(selectedRow.FindControl("lblAngHrgUnit"), Label).Text.ToString
        txtButiranDt.Text = TryCast(selectedRow.FindControl("strButiran"), Label).Text.ToString
        txtAngJum.Text = TryCast(selectedRow.FindControl("lblAngHrgUnit"), Label).Text.ToString

        ViewState("EditBil") = TryCast(selectedRow.FindControl("lblNoButiran"), Label).Text.ToString
        'lblRowNumber

        lbtnKemaskini.Visible = True
    End Sub

    'Protected Sub gvButiran_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvButiran.RowCommand
    'Protected Sub gvButiran_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
    Protected Sub gvButiran_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvButiran.RowCommand


        'Dim gvRow As GridViewRow = TryCast(ctrl.Parent.NamingContainer, GridViewRow)
        Dim gvRow As GridViewRow = CType(CType(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
        Dim RowIndex As Integer = gvRow.RowIndex

        'Dim rIndex As Integer = Convert.ToInt32(e.CommandArgument)
        'Dim gvRow As GridViewRow = gvButiran.Rows(rIndex)
        'Dim ToBeDeleteFileName As String = gvRow.Cells(0).Text

        Dim idDt_ As Label = CType(gvRow.FindControl("lblIDDt"), Label)  ' Find Your Control here
        Dim idDt = idDt_.Text
        Dim noMohon = txtNoMohon.Text

        If e.CommandName = "Padam" Then

            If lblHiddenOpen.Text = "0" Then
                fGlobalAlert("Maklumat Butiran tidak berjaya dipadam!", Me.Page, Me.[GetType]())

            Else

                If noMohon <> "" Then

                Dim strSQL_ = $"UPDATE 
                                BG01_MohonDt SET  BG20_Status = @Status Where BG20_NoMohon = '{noMohon}' and BG20_Id = '{idDt}'"


                Dim paramSql_() As SqlParameter =
                     {
                 New SqlParameter("@Status", False)
                 }

                dbconn.sConnBeginTrans()
                If dbconn.fUpdateCommand(strSQL_, paramSql_) > 0 Then
                    dbconn.fUpdateCommand(strSQL_, paramSql_)
                    dbconn.sConnCommitTrans()
                End If

            End If
            'Dim grdrow As GridViewRow = CType((CType(sender, LinkButton)).NamingContainer, GridViewRow)

            'Dim gvRow1 As GridViewRow = TryCast(ctrl.Parent.NamingContainer, GridViewRow)
            'Dim strKodVot As String = gvRow.Cells(1).Text

            Dim vsDt As DataTable = TryCast(ViewState("CurrentButiran"), DataTable)

            ' Dim rowCollection As DataRowCollection = vsDt.Rows
            ' Dim Row As GridViewRow = gvButiran.SelectedRow

            'Dim b As String = gvRow.Cells(1).Text
            'Dim keyColumns As DataColumn() = New DataColumn(0) {}
            'keyColumns(0) = vsDt.Columns("id")
            'vsDt.PrimaryKey = keyColumns

            ' rowCollection.Contains(gvRow.FindControl("lblRowNumber"))
            'Dim idrow_ As DataRow = rowCollection.Find(gvRow.FindControl("id")) ' Find Your Control here




            'Dim selectedRow As GridViewRow = gvButiran.SelectedRow
            vsDt.Rows.RemoveAt(RowIndex)
            ViewState("CurrentButiran") = vsDt
            BindGridViewButiran()

            End If


        ElseIf e.CommandName = "Select" Then

            lblIDDt_.Text = TryCast(gvRow.FindControl("lblIDDt"), Label).Text.ToString
            ddlVotA.SelectedValue = TryCast(gvRow.FindControl("StrKodVotA"), Label).Text.ToString

            fBindDdlVot(Left(ddlVotA.SelectedValue, 1), "0")

            'txtAngJum.Text = TryCast(selectedRow.FindControl("lblAngHrgUnit"), Label).Text.ToString
            txtButiranDt.Text = TryCast(gvRow.FindControl("strButiran"), Label).Text.ToString
            txtButiranDt.Enabled = True

            ViewState("EditBil") = TryCast(gvRow.FindControl("lblNoButiran"), Label).Text.ToString
            'lblRowNumber

            lbtnKemaskini.Visible = True

        End If



    End Sub
    Protected Sub lbtnKemaskini_Click(sender As Object, e As EventArgs) Handles lbtnKemaskini.Click
        Try
            Dim intBil As Integer = CInt(ViewState("EditBil"))
            'Page.Validate("btnSaveButiran")
            If Page.IsValid() Then

                If ViewState("CurrentButiran") IsNot Nothing Then

                    Dim EditDr As DataRow
                    'get datatable from view state 
                    Dim dtCurrentTable As DataTable = TryCast(ViewState("CurrentButiran"), DataTable)  'DirectCast(ViewState("CurrentButiran"), DataTable)
                    EditDr = dtCurrentTable.Select($"NoButiran = '{intBil}'").FirstOrDefault()

                    EditDr("ID") = lblIDDt_.Text
                    EditDr("NoButiran") = countButiran
                    EditDr("KodVotA") = ddlVotA.SelectedValue
                    EditDr("KodVotSbg") = ddlVotSbg.SelectedValue
                    EditDr("Butiran") = txtButiranDt.Text
                    EditDr("AngHrgUnit") = txtAngJum.Text '.ToString("#,##0.00")

                    ViewState("CurrentButiran") = dtCurrentTable
                    BindGridViewButiran()


                End If

                'Clear textbox in Butiran Panel
                clearTextButiran()

            End If
        Catch ex As Exception

        End Try
    End Sub


    'Protected Sub lbUploadLampPO_Click(sender As Object, e As EventArgs) Handles lbUploadLampPO.Click
    Private Sub fUpload(noPermohonan As String)
        Try


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
                                Dim folderPath As String = Server.MapPath($"~/Upload/Document/Bajet/Permohonan/")
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
                                        simpanPath(folderPath, SaveFileName, ContentType, "Bajet", noMohon)

                                        'Save the File to the Directory (Folder).

                                        'LabelMessage1.ForeColor = Color.Green
                                        'Display the success message.
                                        'LabelMessage1.Text = FileName + " has been uploaded."


                                        fLoadGVSalinanBajet(noMohon)
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

            Dim str = $"select BG16_Id, BG01_NoMohon, BG16_NamaDok, BG16_Path, BG16_ContentType, BG16_JenisDok, BG16_Status from BG16_Lampiran 
            where BG01_NoMohon = '{IdSem}'"
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
                        New SqlParameter("@noMohon", IdSem),
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

    Private Sub fLoadGVSalinanBajet(noMohon)
        Dim str = $"select BG16_Id, BG01_NoMohon, BG16_NamaDok, BG16_Path from BG16_Lampiran 
            where BG01_NoMohon = '{noMohon}' and BG16_Status = 1 "

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

    Protected Sub gvLampiran_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvLampiran.SelectedIndexChanged
        Dim selectedRow As GridViewRow = gvLampiran.SelectedRow

        Dim deleteLampiran = TryCast(selectedRow.FindControl("lblIDLampiran"), Label).Text.ToString
        Dim noMohon = TryCast(selectedRow.FindControl("lblNoMohonLampiran"), Label).Text.ToString
        If lblHiddenOpen.Text = "0" Then
            fGlobalAlert("Maklumat Butiran tidak berjaya dipadam!", Me.Page, Me.[GetType]())

        Else
            Dim strSQL_Delete = $"DELETE FROM BG16_Lampiran Where BG16_Id = @id and BG16_Status = 1 "
            Dim paramSql_delete() As SqlParameter =
             {
             New SqlParameter("@id", deleteLampiran)
             }

            dbconn.fUpdateCommand(strSQL_Delete, paramSql_delete)

            fLoadGVSalinanBajet(noMohon)

        End If
    End Sub

    Private Sub gvButiran_PageIndexChanged(sender As Object, e As EventArgs) Handles gvButiran.PageIndexChanged

    End Sub
End Class