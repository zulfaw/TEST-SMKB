Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization

Public Class MaklumatPermohonanBajet
    Inherits System.Web.UI.Page

    Public KodStatus As String = String.Empty
    Private countButiran As Integer = 0
    Private StatusKemaskini As Boolean = False
    Private dbconn As New DBKewConn

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Dim passQS_NoMohon = Request.QueryString("no")
                firstBindViewStateButiran()
                Dim kod = fGetPeringkatBajetPTJ(Session("ssusrKodPTj"))
                fCheckPeringkat(kod)
                ViewState("KodPeringkat") = kod


                If passQS_NoMohon = "0" Then
                    Mohonbaru()
                Else
                    MohonEdit(passQS_NoMohon)
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

#Region "Bind DDL/GV"
    ''' <summary>
    ''' Bind ViewState for the first time.
    ''' </summary>
    Private Sub firstBindViewStateButiran()
        Try
            Dim dt As New DataTable()
            dt.Columns.AddRange(New DataColumn(5) {
                            New DataColumn("NoButiran", GetType(String)),
                            New DataColumn("KodVot", GetType(String)),
                            New DataColumn("Butiran", GetType(String)),
                            New DataColumn("Kuantiti", GetType(Integer)),
                            New DataColumn("AngHrgUnit", GetType(Decimal)),
                            New DataColumn("AngJumlah", GetType(Decimal))
                            })

            'saving databale into viewstate   
            ViewState("CurrentButiran") = dt

            'bind Gridview
            BindGridViewButiran()
        Catch ex As Exception
        End Try
    End Sub

    ''' <summary>
    ''' Bind GridViewButiran
    ''' </summary>
    Protected Sub BindGridViewButiran()
        Try
            Dim dt = DirectCast(ViewState("CurrentButiran"), DataTable)


            'Dim total As Decimal= Convert.ToDecimal(
            Dim total As Decimal = dt.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("AngJumlah"))
            ViewState("TotalAmount") = total
            gvButiran.DataSource = dt
            gvButiran.DataBind()


        Catch ex As Exception
        End Try
    End Sub

    Private Sub fBindDdlJenisDasar()
        Try
            Dim strSql As String = $"Select BG_KodDasar, Butiran from BG_Dasar where StatusDaftar='1'"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            ddlJenDasar.DataSource = ds
            ddlJenDasar.DataTextField = "Butiran"
            ddlJenDasar.DataValueField = "BG_KodDasar"
            ddlJenDasar.DataBind()

        Catch ex As Exception
            'fErrorLog("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
        End Try
    End Sub

    Private Sub fBindDdlKodOperasi()
        Try
            Dim strSql As String = $"Select KodOperasi, (KodOperasi + ' - ' + Butiran) AS Butiran2 from BG_KodOperasi"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            ddlKodOperasi.DataSource = ds
            ddlKodOperasi.DataTextField = "Butiran2"
            ddlKodOperasi.DataValueField = "KodOperasi"
            ddlKodOperasi.DataBind()
        Catch ex As Exception
            'fErrorLog("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
        End Try
    End Sub

    'Private Sub fBindDdlBahagian(kodptj As String)
    '    Try
    '        Dim strSql As String = $"Select KodBah, (KodBah + ' - ' + NamaBahagian) AS Butiran from MK_BahagianPTJ where kodptj='{kodptj}' ORDER BY KodBah ASC"

    '        Dim ds As New DataSet
    '        Dim dbconn As New DBKewConn
    '        ds = dbconn.fselectCommand(strSql)

    '        ddlBahagian.DataSource = ds
    '        ddlBahagian.DataTextField = "Butiran"
    '        ddlBahagian.DataValueField = "KodBah"
    '        ddlBahagian.DataBind()

    '    Catch ex As Exception
    '        'fErrorLog("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
    '    End Try
    'End Sub

    Protected Sub fBindDdlKW()
        Try
            Dim strSql As String = "Select KodKW, butiran, (KodKW +' - ' + butiran) as Butiran2 from mk_kw ORDER BY Kodkw "

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn

            ds = dbconn.fselectCommand(strSql)
            ddlKW.DataSource = ds
            ddlKW.DataTextField = "Butiran2"
            ddlKW.DataValueField = "KodKW"
            ddlKW.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    'Private Sub fBindDdlUnit(kodptj As String, kodbah As String)
    '    Try
    '        Dim strSql As String = $"Select KodUnit, (KodUnit + ' - ' + NamaUnit) AS Butiran from MK_UnitPTJ where kodptj='{kodptj}' and kodbah='{kodbah}' ORDER BY KodBah ASC"

    '        Dim ds As New DataSet
    '        Dim dbconn As New DBKewConn
    '        ds = dbconn.fselectCommand(strSql)

    '        ddlUnit.DataSource = ds
    '        ddlUnit.DataTextField = "Butiran"
    '        ddlUnit.DataValueField = "KodUnit"
    '        ddlUnit.DataBind()

    '    Catch ex As Exception
    '        'fErrorLog("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
    '    End Try
    'End Sub

    Private Sub fBindDdlVot()
        Try
            Dim strSql As String = "select KodVot, Butiran, (KodVot + ' - ' + Butiran) as Butiran2 from mk_vot where klasifikasi = 'H2' order by kodvot"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn

            ds = dbconn.fselectCommand(strSql)

            ddlVotSbg.DataSource = ds
            ddlVotSbg.DataTextField = "Butiran2"
            ddlVotSbg.DataValueField = "KodVot"
            ddlVotSbg.DataBind()

        Catch ex As Exception
            'fErrorLog("Peruntukan_KW.aspx(fBindDdlVot)- " & ex.Message.ToString)
        End Try


    End Sub

#End Region

    ''' <summary>
    ''' Get Jenis Dasar where the status is active
    ''' </summary>
    ''' <returns>Jenis Dasar</returns>
    Private Function fGetJenisDasar() As String
        Dim JnsDsr = ""
        Try
            Dim strSql As String = $"Select BG_KodDasar, Butiran from BG_Dasar where statusdaftar='1'"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)
            Dim dt = ds.Tables(0)
            Dim row As DataRow = dt.Select().FirstOrDefault()

            If Not row Is Nothing Then
                JnsDsr = row.Item("Butiran")
                ViewState("KodJenisDasar") = row.Item("BG_KodDasar")
            End If


        Catch ex As Exception
            'fErrorLog("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
        End Try

        Return JnsDsr
    End Function

    ''' <summary>
    ''' Select 
    ''' </summary>
    ''' <returns></returns>
    Private Function fSelectNamaKorporat() As String
        Dim Nama = ""
        Try
            Dim strSql As String = $"Select KodKorporat, Nama from MK_Korporat"

            Dim ds As New DataSet
            'Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)
            Dim dt = ds.Tables(0)
            Dim row As DataRow = dt.Select().FirstOrDefault()

            If Not row Is Nothing Then
                Nama = row.Item("Nama")
                ViewState("KodKorporat") = row.Item("KodKorporat")
            End If


        Catch ex As Exception
            'fErrorLog("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
        End Try

        Return Nama
    End Function

    Private Function fSelectMaksud() As String
        Dim Nama As String = ""
        Try
            Dim strSql As String = $"Select KodMaksud, NamaMaksud from BG_Maksud"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)
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

    ''' <summary>
    ''' Get 6 digit of running number base on same PTJ and year
    ''' </summary>
    ''' <returns>running number</returns>
    Private Function fGetRunnningNo()
        Dim runningNo As String = ""
        Try
            Dim strSqlNo = $"SELECT BG20_NoMohon FROM BG01_Mohon WHERE kodptj ='{Session("ssusrKodPTj")}' AND BG20_TahunBajet = '{txtTahunBajet.Text}' ORDER BY ID DESC"
            Dim dbconn As New DBKewConn
            Dim ds = dbconn.fselectCommand(strSqlNo)

            'get first row noMohon
            Dim noMohon = ds.Tables(0).Rows(0)("BG20_NoMohon").ToString
            runningNo = noMohon.Substring(8, 6)

            Dim intRunNo = CInt(runningNo) + 1

            'Two character of running number.
            runningNo = intRunNo.ToString("000000")

        Catch ex As Exception
            'fErrorLog("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
        End Try

        Return runningNo
    End Function

    ''' <summary>
    ''' Get no permohonan.
    ''' </summary>
    ''' <returns>no permohonan</returns>
    Private Function fGetNoMohon()
        'BG(2A)-PTJ(2N)-Bh(2N)-Unit(2N)-RunningNo(6N)-Bulan(2N)-Tahun(2N) = 18Digit
        Dim month = Date.Now.Month.ToString()
        Dim year = Date.Now.Year.ToString.Substring(2, 2)
        Dim noMohon As String = "BG" + Session("ssusrKodPTj") + fGetKodBah().Substring(2) + fGetKodUnit().Substring(4) + fGetRunnningNo() + month + year
        Return noMohon
    End Function

    ''' <summary>
    ''' Get Kod Bahagian
    ''' </summary>
    ''' <returns>Kod Bahagian</returns>
    Private Function fGetKodBah() As String
        Dim KodBah = "0000"

        If ViewState("KodPeringkat") = 2 Or ViewState("KodPeringkat") = 3 Then
            KodBah = Session("ssusrKodBahagian")
        End If

        Return KodBah
    End Function

    ''' <summary>
    ''' Get Kod Unit
    ''' </summary>
    ''' <returns>Kod Unit</returns>
    Private Function fGetKodUnit() As String
        Dim KodUnit = "000000"

        If ViewState("KodPeringkat") = 3 Then
            KodUnit = Session("ssusrKodUnit")
        End If

        Return KodUnit
    End Function

    ''' <summary>
    ''' Kemaskini Mod, all fields are disabled
    ''' otherwise, click button Kemaskini
    ''' </summary>
    ''' <param name="Yes"></param>
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

        lbtnHapus.Visible = Not Yes
        lbtnKembali.Visible = Not Yes
        lbtnEditButiran.Visible = False
        lbtnUndo.Visible = False
    End Sub


    ''' <summary>
    ''' Function to clear the textbox of Butiran Perbelanjaan
    ''' </summary>
    Private Sub clearTextButiran()
        txtButiran.Text = ""
        txtKuantiti.Text = ""
        txtAngJum.Text = ""
        txtAngHrgUnit.Text = ""

        'ddlVotSbg.Items.Insert(0, New ListItem("-SILA PILIH-", ""))
        ddlVotSbg.SelectedIndex = 0
    End Sub


    ''' <summary>
    ''' Text Box Anggaran Harga Unit changed event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub txtAngHrgUnit_TextChanged(sender As Object, e As EventArgs) Handles txtAngHrgUnit.TextChanged
        Dim angHrgSeunit = CDec(txtAngHrgUnit.Text)
        Dim JumAngHrg As Decimal = 0.00
        If txtKuantiti.Text = "0" Then
            JumAngHrg = angHrgSeunit
        Else
            JumAngHrg = CDec(CInt(txtKuantiti.Text) * angHrgSeunit)
        End If
        txtAngJum.Text = JumAngHrg.ToString("#,##0.00")
        txtAngHrgUnit.Text = angHrgSeunit.ToString("#,##0.00")
    End Sub

    Protected Sub gvButiran_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvButiran.RowDeleting
        Dim row As GridViewRow = DirectCast(gvButiran.Rows(e.RowIndex), GridViewRow)
        Dim strNoButiran = Convert.ToInt32(CType(row.FindControl("lblNoButiran"), Label).Text) 'Trim(row.Cells(1).Text.ToString.TrimEnd)
        Dim dbconn As New DBKewConn
        Dim noMohon = txtNoMohon.Text
        If noMohon IsNot String.Empty Then
            Dim strSql = $"delete from BG20_MohonDt where BG20_NoMohon='{noMohon}' and BG20_NoButiran='{strNoButiran}'"
            If dbconn.fUpdateCommand(strSql) > 0 Then
                fGlobalAlert("Rekod Butiran telah dipadam!", Me.Page, Me.[GetType](), "../../BAJET/PERMOHONAN BAJET/Permohonan_Bajet_Tahunan.aspx")
            End If
        End If

        Dim dtCurrentTable As DataTable = DirectCast(ViewState("CurrentButiran"), DataTable)

        For Each dr As DataRow In dtCurrentTable.Rows
            'Remove initial blank row
            If dr("NoButiran").ToString = strNoButiran Then
                dr("NoButiran").Delete()
                dr("NoButiran").AcceptChanges()
            End If
        Next
        ViewState("CurrentButiran") = dtCurrentTable

        BindGridViewButiran()

    End Sub


    Private Sub MohonEdit(NoMohon As String)
        'Disable all the input fields
        KemaskiniField(False)

        Try
            Dim dsMohon As New DataSet
            Dim dsMohonDt As New DataSet
            Dim dbconn As New DBKewConn
            Dim dtUserInfo As New DataTable

            txtNoMohon.Text = NoMohon

            Dim strSql = $"Select KodPtj,KodBahagian,KodUnitPtj,KodMaksud,KodDasar,KodKW, BG20_TarikhMohon,BG20_TahunBajet,
                            KodOperasi,BG20_StaffIDPemohon,BG20_StaffIDPenyokong,BG20_StaffIDPengesah,StatusDok,BG20_AmaunMohon,
                            BG20_Justifikasi,BG20_Program from BG20_Mohon where BG20_NoMohon='{NoMohon}'"

            dsMohon = dbconn.fSelectCommand(strSql, "MohonDs")

            Dim KodPTj = dsMohon.Tables("MohonDs").Rows(0)("KodPtj").ToString
            Dim KodBahagian = dsMohon.Tables("MohonDs").Rows(0)("KodBahagian").ToString
            Dim KodUnit = dsMohon.Tables("MohonDs").Rows(0)("KodUnitPtj").ToString
            Dim KodMaksud = dsMohon.Tables("MohonDs").Rows(0)("KodMaksud").ToString
            Dim KodDsr = dsMohon.Tables("MohonDs").Rows(0)("KodDasar").ToString
            Dim tarikh As Date = dsMohon.Tables("MohonDs").Rows(0)("BG20_TarikhMohon")
            Dim TahunBajet = dsMohon.Tables("MohonDs").Rows(0)("BG20_TahunBajet").ToString
            Dim KodOperasi = dsMohon.Tables("MohonDs").Rows(0)("KodOperasi").ToString
            Dim KodKW = dsMohon.Tables("MohonDs").Rows(0)("KodKW").ToString
            Dim PemohonStafID = dsMohon.Tables("MohonDs").Rows(0)("BG20_StaffIDPemohon").ToString
            Dim PenyokongStafID = dsMohon.Tables("MohonDs").Rows(0)("BG20_StaffIDPenyokong").ToString
            Dim PengesahStafID = dsMohon.Tables("MohonDs").Rows(0)("BG20_StaffIDPengesah").ToString

            'Bahagian
            If Not KodBahagian.Equals("0000") Then
                'Has kodbahagian
                txtBahagian.Text = fGetBahagianPTJ(KodBahagian).ToUpper

                'Check Unit
                If Not KodUnit.Equals("000000") Then
                    ' Has KodUnit
                    txtUnit.Text = fGetUnitPTJ(KodUnit).ToUpper
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

            Dim StatusDok = fGetStatusDok(dsMohon.Tables("MohonDs").Rows(0)("StatusDok").ToString)
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

            txtMaksud.Text = fGetMaksud(KodMaksud)
            txtAgensi.Text = fSelectNamaKorporat()
            'txtJenDasar.Text = fGetJenisDasar(KodDsr)
            txtJust.Text = dsMohon.Tables("MohonDs").Rows(0)("BG20_Justifikasi").ToString
            'ddlTahunBajet.SelectedIndex = ddlTahunBajet.Items.IndexOf(ddlTahunBajet.Items.FindByText(TahunBajet))
            txtTahunBajet.Text = TahunBajet

            txtProgram.Text = dsMohon.Tables("MohonDs").Rows(0)("BG20_Program").ToString

            txtNamaPemohon.Text = Session("ssusrName")
            txtJawPemohon.Text = Session("ssusrPost")

            If Not String.IsNullOrEmpty(PenyokongStafID) Then

                dtUserInfo = fGetUserInfo(strStaffID)

                If dtUserInfo.Rows.Count > 0 Then
                    txtNamaPenyokong = dtUserInfo.Rows.Item(0).Item("MS01_Nama")
                    txtJawPenyokong = dtUserInfo.Rows.Item(0).Item("JawGiliran")
                End If
            End If

            If Not String.IsNullOrEmpty(PengesahStafID) Then

                dtUserInfo = fGetUserInfo(strStaffID)

                If dtUserInfo.Rows.Count > 0 Then
                    txtNamaPengesah = dtUserInfo.Rows.Item(0).Item("MS01_Nama")
                    txtJawPengesah = dtUserInfo.Rows.Item(0).Item("JawGiliran")
                End If
            End If

            Dim strSqlDt = $"Select BG20_NoButiran,BG20_Butiran,BG20_Kuantiti,BG20_AngHrgUnit,BG20_AngJumlah,KodVotSebagai from BG20_MohonDt where BG20_NoMohon='{NoMohon}'"
            dsMohonDt = dbconn.fSelectCommand(strSqlDt, "MohonDtDs")

            Dim drCurrentRow As DataRow = Nothing

            'get datatable from view state 
            Dim dtCurrentTable As DataTable = DirectCast(ViewState("CurrentButiran"), DataTable)


            For Each rowdt In dsMohonDt.Tables("MohonDtDs").Rows
                'add each row into data table
                drCurrentRow = dtCurrentTable.NewRow()
                drCurrentRow("NoButiran") = rowdt("BG20_NoButiran").ToString
                drCurrentRow("KodVot") = rowdt("KodVotSebagai").ToString
                drCurrentRow("Butiran") = rowdt("BG20_Butiran").ToString
                drCurrentRow("Kuantiti") = CInt(rowdt("BG20_Kuantiti")) '.ToString
                drCurrentRow("AngHrgUnit") = CDec(rowdt("BG20_AngHrgUnit")) '.ToString("#,##0.00")
                drCurrentRow("AngJumlah") = CDec(rowdt("BG20_AngJumlah")) '.ToString("#,##0.00")
                dtCurrentTable.Rows.Add(drCurrentRow)
            Next

            'saving datatable into viewstate
            ViewState("CurrentButiran") = dtCurrentTable

            'bind Gridview
            BindGridViewButiran()

            'Populate ddl Vot
            fBindDdlVot()
            ddlVotSbg.Items.Insert(0, New ListItem("-SILA PILIH-", ""))
            ddlVotSbg.SelectedIndex = 0
        Catch ex As Exception
            'fErrorLog("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
        End Try
    End Sub


    ''' <summary>
    ''' Function for Mohon Baru
    ''' </summary>
    Private Sub Mohonbaru()

        txtNamaPemohon.Text = Session("ssusrName")
        txtJawPemohon.Text = Session("ssusrPost")

        txtStatus.Text = fGetStatusDok("01").FirstOrDefault.Value
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

        fBindDdlVot()
        ddlVotSbg.Items.Insert(0, New ListItem("-SILA PILIH-", ""))
        ddlVotSbg.SelectedIndex = 0

        txtMaksud.Text = fSelectMaksud()
        txtAgensi.Text = fSelectNamaKorporat()

        txtJust.Text = ""
        txtProgram.Text = ""
        txtTarikh.Text = Date.Today.ToString("dd/MM/yyyy")

        Dim NextYear = Date.Now.AddYears(1).Year.ToString
        txtTahunBajet.Text = NextYear

        KemaskiniField(True)
        lbtnKembali.Visible = True

    End Sub

#Region "Function Get Value"

    ''' <summary>
    ''' Get Nama Unit 
    ''' </summary>
    ''' <param name="KodUnit"></param>
    ''' <returns>Nama Unit</returns>
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

    ''' <summary>
    ''' Get Nama Maksud
    ''' </summary>
    ''' <param name="KodMak"></param>
    ''' <returns>Nama Maksud</returns>
    Private Function fGetMaksud(KodMak As String) As String
        Dim ds As New DataSet
        Dim NamaMaksud = ""
        Try
            Dim strSql As String = $"Select NamaMaksud from BG_Maksud where KodMaksud='{KodMak}'"

            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)
            NamaMaksud = ds.Tables(0).Rows(0)("NamaMaksud").ToString

        Catch ex As Exception
            'fErrorLog("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
        End Try

        Return NamaMaksud
    End Function

    Private Function fGetJenisDasar(KodDsr As String) As String
        Dim ds As New DataSet
        Dim NamaDasar = ""
        Try
            Dim strSql As String = $"Select Butiran from BG_Dasar where BG_KodDasar='{KodDsr}'"

            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)
            NamaDasar = ds.Tables(0).Rows(0)("Butiran").ToString

        Catch ex As Exception
            'fErrorLog("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
        End Try

        Return NamaDasar
    End Function

    ''' <summary>
    ''' Get Nama PTJ 
    ''' </summary>
    ''' <param name="KodPTJ"></param>
    ''' <returns>Nama PTJ</returns>
    Private Function fGetPTJ(KodPTJ As String) As String
        Dim strSql = $"SELECT Butiran FROM MK_PTJ WHERE KODPTJ='{KodPTJ}'"
        'Dim ds As New DataSet
        Dim dbconn As New DBKewConn
        Dim NamaPTJ = ""
        dbconn.sSelectCommand(strSql, NamaPTJ)
        'ds = dbconn.fselectCommand(strSql)
        Return NamaPTJ 'ds.Tables(0).Rows(0)("Butiran").ToString
    End Function

    ''' <summary>
    ''' Get Nama Bahagian 
    ''' </summary>
    ''' <param name="KodBah"></param>
    ''' <returns>Nama Bahagian</returns>
    Private Function fGetBahagianPTJ(KodBah As String) As String
        Dim strSql = $"Select NamaBahagian from MK_BahagianPtj WHERE KodBah='{KodBah}'"
        Dim dbconn As New DBKewConn
        Dim NamaBahagian = ""
        dbconn.sSelectCommand(strSql, NamaBahagian)
        'Dim s = ds.Tables(0).Rows(0)("NamaBahagian").ToString
        Return NamaBahagian
    End Function


#End Region
    Protected Sub lbtnKembali_Click(sender As Object, e As EventArgs) Handles lbtnKembali.Click

        'check if statuskemaskini is true and No mohon is already created
        If StatusKemaskini And Not String.IsNullOrEmpty(txtNoMohon.Text) Then
            fGlobalAlert("Sila klik button simpan!", Page, [GetType]())
        Else
            Response.Redirect("~/FORMS/BAJET/PERMOHONAN BAJET/Permohonan_Bajet_Tahunan.aspx")
        End If

    End Sub

    Protected Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.Click
        Try
            Page.Validate("btnSimpan")

            If Page.IsValid Then
                Dim noPermohonan As String = ""
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

                'Check if Butiran field is not filled up yet.
                If dtCurrentTable.Rows(0)(1).ToString Is String.Empty Then
                    fGlobalAlert("Maklumat Butiran tidak dimasukkan. Sila masukkan maklumat butiran!", Page, [GetType]())
                    Exit Sub
                End If

                'Check No Mohon is appeared or not in text field of txtNoMohon.
                'If appear, then it is for edit mode
                'Otherwise, it is for add new record
                If txtNoMohon.Text IsNot String.Empty Then
                    'Edit mode

                    noPermohonan = txtNoMohon.Text

                    Dim strSqlMohon = $"Select BG20_NoMohon, KodBahagian,KodUnitPtj,KodDasar,KodOperasi, KodKW, BG20_Program,BG20_Justifikasi,BG20_AmaunMohon from BG20_Mohon where BG20_NoMohon='{noPermohonan}'"

                    dsMohon = dbconn.fSelectCommand(strSqlMohon, "MohonDs")

                    If dsMohon IsNot Nothing Then
                        Dim dr As DataRow = dsMohon.Tables("MohonDs").Rows(0)

                        dr("KodBahagian") = fGetKodBah()
                        dr("KodUnitPtj") = fGetKodUnit()
                        dr("KodDasar") = ddlJenDasar.SelectedValue
                        dr("KodOperasi") = ddlKodOperasi.SelectedValue
                        dr("BG20_Program") = txtProgram.Text
                        dr("KodKW") = ddlKW.SelectedValue
                        dr("BG20_Justifikasi") = txtJust.Text
                        dr("BG20_AmaunMohon") = ViewState("TotalAmount")

                        dbconn.sUpdateCommand(dsMohon, strSqlMohon)
                        'sAuditLog(Session("ssusrID"), Request.QueryString("KodSubMenu"), "BG20_Mohon", "2", "Edit Permohonan Bajet") 'TransID: 1-Insert,2-Update,3-Delete
                    End If

                    fGlobalAlert($"Rekod telah dikemaskini!", Me.Page, Me.[GetType](), "../../BAJET/PERMOHONAN BAJET/Permohonan_Bajet_Tahunan.aspx")

                Else
                    'Add new record
                    noPermohonan = fGetNoMohon()
                    tarikhMohon = DateTime.ParseExact(txtTarikh.Text, "dd/MM/yyyy", New CultureInfo("en-US"))

                    strSql = "INSERT INTO BG20_MOHON (BG20_NoMohon,BG20_TarikhMohon,KodPtj,KodBahagian,KodUnitPtj,KodMaksud,KodKorporat,
                            BG20_Program,BG20_Justifikasi,BG20_TahunBajet,KodDasar,KodOperasi,KodKW,BG20_Status,BG20_StaffIDPemohon,StatusDok,BG20_AmaunMohon)
                            VALUES (@NoMohonn, @tarikhMohonn, @KodPTJ, @KodBah, @KodUnit, @KodMaksud, @KodAgensi,
                            @Programm, @Justificationn, @TahunBajett, @KodJenDsr, @KodOperasi, @KodKWw, @Statuss, @StaffIDd, @StatusDok,@AmaunMohonn )"

                    Dim paramSql() As SqlParameter = {
                        New SqlParameter("@NoMohonn", noPermohonan),
                        New SqlParameter("@tarikhMohonn", tarikhMohon),
                        New SqlParameter("@KodPTJ", Session("ssusrKodPTj")),
                        New SqlParameter("@KodBah", fGetKodBah()),
                        New SqlParameter("@KodUnit", fGetKodUnit()),
                        New SqlParameter("@KodMaksud", ViewState("KodMaksud")),
                        New SqlParameter("@KodAgensi", ViewState("KodKorporat")),
                        New SqlParameter("@Programm", txtProgram.Text),
                        New SqlParameter("@Justificationn", txtJust.Text),
                        New SqlParameter("@TahunBajett", txtTahunBajet.Text),
                        New SqlParameter("@KodJenDsr", ddlJenDasar.SelectedValue),
                        New SqlParameter("@KodOperasi", ddlKodOperasi.SelectedValue),
                        New SqlParameter("@KodKWw", ddlKW.SelectedValue),
                        New SqlParameter("@Statuss", True),
                        New SqlParameter("@StaffIDd", Session("ssusrID")),
                        New SqlParameter("@StatusDok", "01"),
                        New SqlParameter("@AmaunMohonn", ViewState("TotalAmount"))
                    }

                    dbconn.sConnBeginTrans()
                    If dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                        dbconn.sConnCommitTrans()
                        'sAuditLog(Session("ssusrID"), Request.QueryString("KodSubMenu"), "BG20_Mohon", "1", "Simpan Button") 'TransID: 1-Insert,2-Update,3-Delete

                        countButiran = 0
                        For Each drCurrentRow As DataRow In dtCurrentTable.Rows
                            'AngHrgUnit = Convert.ToDecimal(drCurrentRow("AngHrgUnit"))
                            'AngJumlah = Convert.ToDecimal(drCurrentRow("AngJumlah"))
                            AngHrgUnit = drCurrentRow("AngHrgUnit")
                            AngJumlah = drCurrentRow("AngJumlah")

                            strSqlButiran = "INSERT INTO BG20_MohonDt ( BG20_NoMohon, BG20_Butiran, BG20_Kuantiti, BG20_AngHrgUnit, BG20_AngJumlah, BG20_Status, KodVotSebagai)
                                            VALUES ( @NoMohonn, @Butirann, @Kuantitii, @AngHargaSeUnitt, @AngJumlahh, @Statuss, @KodVotSbg)"
                            paramSqlBtrn = {
                                New SqlParameter("@KodVotSbg", drCurrentRow("KodVot")),
                                New SqlParameter("@NoMohonn", noPermohonan),
                                New SqlParameter("@Butirann", drCurrentRow("Butiran")),
                                New SqlParameter("@Kuantitii", drCurrentRow("Kuantiti")),
                                New SqlParameter("@AngHargaSeUnitt", AngHrgUnit),
                                New SqlParameter("@AngJumlahh", AngJumlah),
                                New SqlParameter("@Statuss", True)
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

                        If countButiran > 0 Then
                            fGlobalAlert($"Rekod baru telah ditambah bersama '{countButiran.ToString}' butiran permohonan!", Me.Page, Me.[GetType](), "../../BAJET/PERMOHONAN BAJET/Permohonan_Bajet_Tahunan.aspx")
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

    'Protected Sub lbtnKemaskini_Click(sender As Object, e As EventArgs) Handles lbtnKemaskini.Click
    '    KemaskiniField(True)
    'End Sub

    Protected Sub lbtnHapus_Click(sender As Object, e As EventArgs) Handles lbtnHapus.Click
        Dim noMohon = txtNoMohon.Text
        If noMohon IsNot String.Empty Then
            Try
                Dim strSql As String


                strSql = $"delete from BG20_Mohon where BG20_NoMohon = '{noMohon}'"

                dbconn.sConnBeginTrans()
                If dbconn.fUpdateCommand(strSql) > 0 Then
                    dbconn.sConnCommitTrans()
                    'sAuditLog(Session("ssusrID"), Request.QueryString("KodSubMenu"), "BG20_Mohon", "3", "Hapus Button") 'TransID: 1-Insert,2-Update,3-Delete

                    strSql = $"delete from BG20_MohonDt where BG20_NoMohon = '{noMohon}'"
                    If dbconn.fUpdateCommand(strSql) > 0 Then
                        dbconn.sConnCommitTrans()
                        'sAuditLog(Session("ssusrID"), Request.QueryString("KodSubMenu"), "BG20_Mohon", "3", "Hapus Button") 'TransID: 1-Insert,2-Update,3-Delete
                        fGlobalAlert("Rekod telah dipadam!", Me.Page, Me.[GetType](), "../../BAJET/PERMOHONAN BAJET/Permohonan_Bajet_Tahunan.aspx")
                    Else
                        dbconn.sConnRollbackTrans()
                    End If
                Else
                    dbconn.sConnRollbackTrans()
                End If
            Catch ex As Exception
                'fErrorLog("Jenis_Permohonan.aspx(gvJenPermohonan_RowDeleting)- " & ex.Message.ToString)
            End Try


        End If
    End Sub

    Protected Sub lbtnReset_Click(sender As Object, e As EventArgs) Handles lbtnReset.Click
        clearTextButiran()
    End Sub

    Protected Sub lbtnSaveButiran_Click(sender As Object, e As EventArgs) Handles lbtnSaveButiran.Click
        Page.Validate("btnSaveButiran")
        If Page.IsValid() Then
            Dim Kuantiti As Integer = Convert.ToInt32(txtKuantiti.Text)
            Dim AngHrgUnit As Decimal = Convert.ToDecimal(txtAngHrgUnit.Text)
            Dim AngJumBelanja As Decimal = 0.00
            If txtKuantiti.Text = "0" Then
                AngJumBelanja = AngHrgUnit
            Else
                AngJumBelanja = CDec(Kuantiti * AngHrgUnit)
            End If

            Dim noPermohonan = txtNoMohon.Text

            If noPermohonan IsNot String.Empty Then
                'Edit mode
                Dim dbconn As New DBKewConn
                Dim dsMohonDt As New DataSet

                Dim strSqlButiran = "INSERT INTO BG20_MohonDt ( BG20_NoMohon, BG20_Butiran, BG20_Kuantiti, BG20_AngHrgUnit, BG20_AngJumlah, BG20_Status, KodVotSebagai)
                                            VALUES ( @NoMohonn, @Butirann, @Kuantitii, @AngHargaSeUnitt, @AngJumlahh, @Statuss, @KodVotSbg)"
                Dim paramSqlBtrn() As SqlParameter = {
                                New SqlParameter("@KodVotSbg", ddlVotSbg.SelectedValue),
                                New SqlParameter("@NoMohonn", noPermohonan),
                                New SqlParameter("@Butirann", txtButiran.Text),
                                New SqlParameter("@Kuantitii", Kuantiti),
                                New SqlParameter("@AngHargaSeUnitt", AngHrgUnit),
                                New SqlParameter("@AngJumlahh", AngJumBelanja),
                                New SqlParameter("@Statuss", True)
                            }

                dbconn.sConnBeginTrans()
                If dbconn.fInsertCommand(strSqlButiran, paramSqlBtrn) > 0 Then
                    dbconn.sConnCommitTrans()
                    'sAuditLog(Session("ssusrID"), Request.QueryString("KodSubMenu"), "BG20_Mohon", "1", "Kemaskini Button") 'TransID: 1-Insert,2-Update,3-Delete
                Else
                    dbconn.sConnRollbackTrans()
                End If

            Else
                countButiran = ViewState("CountButiran")

                countButiran = countButiran + 1

                ViewState("CountButiran") = countButiran
            End If

            'check view state is not null 
            If ViewState("CurrentButiran") IsNot Nothing Then

                Dim drCurrentRow As DataRow = Nothing
                'get datatable from view state 
                Dim dtCurrentTable As DataTable = DirectCast(ViewState("CurrentButiran"), DataTable)

                'add each row into data table
                drCurrentRow = dtCurrentTable.NewRow()
                drCurrentRow("NoButiran") = countButiran
                drCurrentRow("KodVot") = ddlVotSbg.SelectedValue
                drCurrentRow("Butiran") = txtButiran.Text
                drCurrentRow("Kuantiti") = Kuantiti '.ToString
                drCurrentRow("AngHrgUnit") = AngHrgUnit '.ToString("#,##0.00")
                drCurrentRow("AngJumlah") = AngJumBelanja '.ToString("#,##0.00")
                dtCurrentTable.Rows.Add(drCurrentRow)

                ViewState("CurrentButiran") = dtCurrentTable

                'bind Gridview
                BindGridViewButiran()


            End If

            'Clear textbox in Butiran Panel
            clearTextButiran()

        End If
    End Sub

    Protected Sub gvButiran_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvButiran.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                'If e.Row.RowState And DataControlRowState.Edit Then
                '    ' get selected value of dropdownlist
                '    Dim ddlVot As DropDownList = DirectCast(e.Row.FindControl("ddlKodVot"), DropDownList)
                '    fBindDllKodVotGV(ddlVot)
                '    Dim Str = DataBinder.Eval(e.Row.DataItem, "StrKodVot").ToString
                '    ddlVot.Items.FindByText(Str).Selected = True
                'End If

            End If

            If e.Row.RowType = DataControlRowType.Footer Then
                e.Row.Cells(2).Visible = False
            End If
        Catch ex As Exception
            'fErrorLog(ex.Message.ToString)
        End Try
    End Sub


    Private Sub fBindDllKodVotGV(ddl As DropDownList)
        Try
            Dim strSql As String = "select KodVot, Butiran, (KodVot + ' - ' + Butiran) as Butiran2 from mk_vot where klasifikasi = 'H2' order by kodvot"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn

            ds = dbconn.fselectCommand(strSql)

            ddl.DataSource = ds
            ddl.DataTextField = "Butiran2"
            ddl.DataValueField = "KodVot"
            ddl.DataBind()
        Catch ex As Exception
            'fErrorLog(ex.Message.ToString)
        End Try

    End Sub

    Protected Sub gvButiran_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvButiran.Sorting
        Try
            ' Cancel the sorting operation if the user attempts
            ' to sort by address.
            If e.SortExpression = "Bil" Then
                e.Cancel = True
                fGlobalAlert("Bil tidak boleh 'Sort'!", Me.Page, Me.[GetType]())
            End If


            Dim dt As DataTable = Nothing
            'If txtNoMohon.Text IsNot String.Empty Then
            '    Dim strSqlDt = $"Select KodVotSebagai, BG20_Butiran, BG20_Kuantiti, BG20_AngHrgUnit, BG20_AngJumlah from BG20_MohonDt where BG20_NoMohon='{noMohon}'"
            '    Dim dbconn As New DBKewConn

            '    'pass as reference 
            '    Dim ds = dbconn.fselectCommand(strSqlDt)

            '    'Convert dataset to datatable
            '    dt = ds.Tables(0)
            'Else
            dt = DirectCast(ViewState("CurrentButiran"), DataTable)
            Session("SortMohonBajetButiran") = dt
            'Dim newrow As DataRow
            'Dim newdt As New DataTable
            'newdt.Columns.AddRange(New DataColumn(5) {
            '                New DataColumn("NoButiran", GetType(String)),
            '                New DataColumn("KodVot", GetType(String)),
            '                New DataColumn("Butiran", GetType(String)),
            '                New DataColumn("Kuantiti", GetType(String)),
            '                New DataColumn("AngHrgUnit", GetType(String)),
            '                New DataColumn("AngJumlah", GetType(String))
            '                })
            'For Each rowdt In dt.Rows
            '    'add each row into data table
            '    newrow = newdt.NewRow()
            '    newrow("NoButiran") = rowdt("NoButiran").ToString
            '    newrow("KodVot") = rowdt("KodVot").ToString
            '    newrow("Butiran") = rowdt("Butiran").ToString
            '    newrow("Kuantiti") = CInt(rowdt("Kuantiti"))
            '    newrow("AngHrgUnit") = CDec(rowdt("AngHrgUnit"))
            '    newrow("AngJumlah") = CDec(rowdt("AngJumlah"))
            '    newdt.Rows.Add(newrow)
            'Next
            'End If


            If Not dt Is Nothing Then
                'gvButiran.EditIndex = -1
                dt.DefaultView.Sort = e.SortExpression & " " & GetSortDirection(e.SortExpression)

                gvButiran.DataSource = dt
                gvButiran.DataBind()
                'Using dv As New DataView(dt)
                '    dv.Sort = e.SortExpression & " " & GetSortDirection(e.SortExpression)
                '    ViewState("CurrentButiran") = dv
                '    gvButiran.DataSource = dv
                '    gvButiran.DataBind()
                'End Using

            End If
        Catch ex As Exception
            'fErrorLog(ex.Message.ToString)
        End Try
    End Sub

    Private Function GetSortDirection(ByVal column As String) As String

        ' By default, set the sort direction to ascending.
        Dim sortDirection = "ASC"

        ' Retrieve the last column that was sorted.
        Dim sortExpression = TryCast(ViewState("SortExpression"), String)

        If sortExpression IsNot Nothing Then
            ' Check if the same column is being sorted.
            ' Otherwise, the default value can be returned.
            If sortExpression = column Then
                Dim lastDirection = TryCast(ViewState("SortDirection"), String)
                If lastDirection IsNot Nothing AndAlso lastDirection = "ASC" Then
                    sortDirection = "DESC"
                End If
            End If
        End If

        ' Save new values in ViewState.
        ViewState("SortDirection") = sortDirection
        ViewState("SortExpression") = column

        Return sortDirection

    End Function

    Protected Sub gvButiran_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvButiran.RowCreated

        Try

            If e.Row.RowType = DataControlRowType.Footer Then

                'First cell Is used for specifying the Total text
                Dim intNoOfMergeCol = e.Row.Cells.Count - 3 ' /*except last column */
                For intCellCol As Integer = 1 To intNoOfMergeCol - 1
                    e.Row.Cells.RemoveAt(1)
                Next

                e.Row.Cells(0).ColumnSpan = intNoOfMergeCol
                e.Row.Cells(0).Text = "Jumlah Besar (RM)"
                e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(0).Font.Bold = True

                Dim total As Decimal = ViewState("TotalAmount")
                e.Row.Cells(1).Text = total.ToString("#,##0.00")
                e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(1).Font.Bold = True
            End If
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub lbtnEditButiran_Click(sender As Object, e As EventArgs) Handles lbtnEditButiran.Click
        Try
            Page.Validate()
            If Page.IsValid() Then

                Dim dbconn As New DBKewConn

                'Update the values.
                Dim noButiran As Integer = ViewState("NoButiran")
                Dim KodVot As String = ddlVotSbg.SelectedValue
                Dim Butiran As String = txtButiran.Text
                Dim Kuantiti As Integer = Convert.ToInt32(txtKuantiti.Text)
                Dim AngHrgUnit As Decimal = Convert.ToDecimal(txtAngHrgUnit.Text)
                Dim AngJumBelanja As Decimal = 0.00
                If txtKuantiti.Text = "0" Then
                    AngJumBelanja = AngHrgUnit
                Else
                    AngJumBelanja = CDec(Kuantiti * AngHrgUnit)
                End If

                If txtNoMohon.Text IsNot String.Empty Then
                    Dim strSqlButiran = $"UPDATE BG20_MohonDt SET BG20_Butiran=@Butirann,BG20_Kuantiti=@Kuantitii, BG20_AngHrgUnit=@AngHargaSeUnitt, 
                    BG20_AngJumlah=@AngJumlahh, KodVotSebagai=@KodVott WHERE BG20_NoButiran='{noButiran}' and BG20_NoMohon='{txtNoMohon.Text}'"
                    Dim paramSqlBtrn = {
                        New SqlParameter("@Butirann", Butiran),
                        New SqlParameter("@Kuantitii", Kuantiti),
                        New SqlParameter("@AngHargaSeUnitt", AngHrgUnit),
                        New SqlParameter("@AngJumlahh", AngJumBelanja),
                        New SqlParameter("@KodVott", KodVot)
                        }

                    dbconn.sConnBeginTrans()
                    If dbconn.fUpdateCommand(strSqlButiran, paramSqlBtrn) > 0 Then
                        dbconn.sConnCommitTrans()
                        'sAuditLog(Session("ssusrID"), Request.QueryString("KodSubMenu"), "BG20_Mohon", "3", "Hapus Button") 'TransID: 1-Insert,2-Update,3-Delete
                    Else
                        dbconn.sConnRollbackTrans()
                    End If

                End If

                Dim dtCurrentTable As DataTable = DirectCast(ViewState("CurrentButiran"), DataTable)
                If ViewState("SortExpression") IsNot Nothing Then
                    dtCurrentTable.DefaultView.Sort = ViewState("SortExpression") & " " & ViewState("SortDirection")
                End If

                For Each dr As DataRow In dtCurrentTable.Rows
                    'Remove initial blank row
                    If dr("NoButiran").ToString = noButiran Then
                        dr.BeginEdit()
                        dr("KodVot") = KodVot
                        dr("Butiran") = Butiran
                        dr("Kuantiti") = CInt(Kuantiti) '.ToString
                        dr("AngHrgUnit") = CDec(AngHrgUnit) '.ToString("#,##0.00")
                        dr("AngJumlah") = CDec(AngJumBelanja) '.ToString("#,##0.00")
                        dr.EndEdit()
                    End If
                Next

                ViewState("CurrentButiran") = dtCurrentTable

                BindGridViewButiran()

                lbtnUndo.Visible = False
                lbtnEditButiran.Visible = False
                lbtnSaveButiran.Visible = True

                ddlVotSbg.Items.Insert(0, New ListItem("-SILA PILIH-", ""))
                'Clear textbox in Butiran Panel
                clearTextButiran()

            End If
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub lbtnUndo_Click(sender As Object, e As EventArgs) Handles lbtnUndo.Click
        lbtnUndo.Visible = False
        lbtnEditButiran.Visible = False
        lbtnSaveButiran.Visible = True
        ddlVotSbg.Items.Insert(0, New ListItem("-SILA PILIH-", ""))
        clearTextButiran()
    End Sub

    Protected Sub gvButiran_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvButiran.SelectedIndexChanged
        'Dim selectedRow As GridViewRow = gvButiran.SelectedRow


        For Each row As GridViewRow In gvButiran.Rows
            If row.RowIndex = gvButiran.SelectedIndex Then

                row.ForeColor = ColorTranslator.FromHtml("#0000FF")
                lbtnUndo.Visible = True
                lbtnEditButiran.Visible = True
                lbtnSaveButiran.Visible = False

                Dim NoButiran As Integer = Convert.ToInt32(CType(row.FindControl("lblNoButiran"), Label).Text)
                Dim KodVot As String = CType((row.FindControl("strKodVot")), Label).Text
                Dim Butiran As String = CType((row.FindControl("lblButiran")), Label).Text
                Dim Kuantiti As Integer = Convert.ToInt32(CType((row.FindControl("lblKuantiti")), Label).Text)
                Dim AngHrgUnit As Decimal = Convert.ToDecimal(CType((row.FindControl("lblAngHrgUnit")), Label).Text)
                Dim AngJumBelanja As Decimal = Convert.ToDecimal(CType((row.FindControl("lblAngJumlah")), Label).Text)

                fBindDdlVot()
                ddlVotSbg.Items.FindByValue(KodVot.Trim()).Selected = True
                txtButiran.Text = Butiran
                txtKuantiti.Text = Kuantiti.ToString
                txtAngHrgUnit.Text = AngHrgUnit.ToString
                txtAngJum.Text = AngJumBelanja.ToString
                ViewState("NoButiran") = NoButiran
            Else
                row.ForeColor = ColorTranslator.FromHtml("#000000")
            End If
        Next

    End Sub

    Protected Sub ddlSaizRekod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSaizRekod.SelectedIndexChanged

        gvButiran.PageSize = CInt(ddlSaizRekod.SelectedValue)
        BindGridViewButiran()

    End Sub

    Protected Sub gvButiran_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvButiran.PageIndexChanging
        gvButiran.PageIndex = e.NewPageIndex
        Try
            If Session("SortMohonBajetButiran") IsNot Nothing Then
                Dim dt = DirectCast(Session("SortMohonBajetButiran"), DataTable)
                dt.DefaultView.Sort = ViewState("SortExpression") & " " & ViewState("SortDirection")
                gvButiran.DataSource = dt
                gvButiran.DataBind()
            Else
                BindGridViewButiran()
            End If
        Catch ex As Exception

        End Try



    End Sub

    Protected Sub txtKuantiti_TextChanged(sender As Object, e As EventArgs) Handles txtKuantiti.TextChanged

        If txtAngHrgUnit.Text IsNot String.Empty Then
            Dim angHrgSeunit = CDec(txtAngHrgUnit.Text)
            Dim JumAngHrg = CDec(CInt(txtKuantiti.Text) * angHrgSeunit)

            txtAngJum.Text = JumAngHrg.ToString("#,##0.00")
            txtAngHrgUnit.Text = angHrgSeunit.ToString("#,##0.00")
        End If

    End Sub
End Class