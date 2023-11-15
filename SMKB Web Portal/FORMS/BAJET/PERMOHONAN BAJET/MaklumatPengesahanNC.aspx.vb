Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization

Public Class MaklumatPengesahanNC
    Inherits System.Web.UI.Page

    Public KodStatus As String = String.Empty
    Private countButiran As Integer = 0
    Private StatusKemaskini As Boolean = False
    Private dbconn As New DBKewConn

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

        ' txtStatus.Text = fGetStatusDok("01").FirstOrDefault.Value
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

        fBindDdlVotAm()
        ddlVotSbgA.Items.Insert(0, New ListItem("-SILA PILIH-", ""))
        ddlVotSbgA.SelectedIndex = 0

        fBindDdlVot()
        ddlVotSbg.Items.Insert(0, New ListItem("-SILA PILIH-", ""))
        ddlVotSbg.SelectedIndex = 0

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
            'Dim AngHrgUnit As Decimal = Convert.ToDecimal(txtAngHrgUnit.Text)
            Dim AngJumBelanja As Decimal = 0.00
            ' txtKuantiti.Text = "0" Then
            'AngJumBelanja = AngHrgUnit
            'Else
            'AngJumBelanja = CDec(Kuantiti * AngHrgUnit)
            'End If

            Dim noPermohonan = txtNoMohon.Text


            'check view state is not null 
            If ViewState("CurrentButiran") IsNot Nothing Then

                Dim drCurrentRow As DataRow = Nothing
                'get datatable from view state 
                Dim dtCurrentTable As DataTable = DirectCast(ViewState("CurrentButiran"), DataTable)

                'add each row into data table
                drCurrentRow = dtCurrentTable.NewRow()
                drCurrentRow("NoButiran") = countButiran
                drCurrentRow("KodVot") = ddlVotSbg.SelectedValue
                'drCurrentRow("Butiran") = txtButiran.Text
                'drCurrentRow("Kuantiti") = Kuantiti '.ToString
                'drCurrentRow("AngHrgUnit") = AngHrgUnit '.ToString("#,##0.00")
                drCurrentRow("AngJumlah") = AngJumBelanja '.ToString("#,##0.00")
                dtCurrentTable.Rows.Add(drCurrentRow)

                ViewState("CurrentButiran") = dtCurrentTable
                clearTextButiran()
                BindGv()



            End If

            'Clear textbox in Butiran Panel
            clearTextButiran()

        End If
    End Sub

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

    Protected Sub BindGv()
        Try
            Dim dt = DirectCast(ViewState("CurrentButiran"), DataTable)

            gvButiran.DataSource = dt
            gvButiran.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub clearTextButiran()
        ' txtButiran.Text = ""
        'txtKuantiti.Text = ""
        txtAngJum.Text = ""
        'txtAngHrgUnit.Text = ""

        'ddlVotSbg.Items.Insert(0, New ListItem("-SILA PILIH-", ""))
        ddlVotSbg.SelectedIndex = 0
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

        lbtnHapus.Visible = Not Yes
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
        Dim Nama = ""
        Try
            Dim strSql As String = $"Select KodKorporat, Nama from MK_Korporat"

            Dim ds As New DataSet
            'Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)
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
            Dim strSql As String = "select KodVot, Butiran, (KodVot + ' - ' + Butiran) as Butiran2 from mk_vot where klasifikasi = 'H1' order by kodvot"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn

            ds = dbconn.fSelectCommand(strSql)

            ddlVotSbgA.DataSource = ds
            ddlVotSbgA.DataTextField = "Butiran2"
            ddlVotSbgA.DataValueField = "KodVot"
            ddlVotSbgA.DataBind()

        Catch ex As Exception
            'fErrorLog("Peruntukan_KW.aspx(fBindDdlVot)- " & ex.Message.ToString)
        End Try


    End Sub

    Private Sub fBindDdlVot()
        Try
            Dim strSql As String = "select KodVot, Butiran, (KodVot + ' - ' + Butiran) as Butiran2 from mk_vot where klasifikasi = 'H2' order by kodvot"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn

            ds = dbconn.fSelectCommand(strSql)

            ddlVotSbg.DataSource = ds
            ddlVotSbg.DataTextField = "Butiran2"
            ddlVotSbg.DataValueField = "KodVot"
            ddlVotSbg.DataBind()

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
            Dim dsMohonDt As New DataSet
            Dim dbconn As New DBKewConn
            Dim dtUserInfo As New DataTable

            txtNoMohon.Text = NoMohon

            Dim strSql = $"Select KodPtj,KodBahagian,KodUnitPtj,KodMaksud,KodDasar,KodKW, BG20_TarikhMohon,BG20_TahunBajet,
                            KodOperasi,BG20_StaffIDPemohon,BG20_StaffIDPenyokong,BG20_StaffIDPengesah,StatusDok,BG20_AmaunMohon,
                            BG20_Justifikasi,BG20_Program from BG01_Mohon where BG20_NoMohon='{NoMohon}'"

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

            'txtMaksud.Text = fGetMaksud(KodMaksud)
            'txtAgensi.Text = fSelectNamaKorporat()
            'txtJenDasar.Text = fGetJenisDasar(KodDsr)
            txtJust.Text = dsMohon.Tables("MohonDs").Rows(0)("BG20_Justifikasi").ToString
            'ddlTahunBajet.SelectedIndex = ddlTahunBajet.Items.IndexOf(ddlTahunBajet.Items.FindByText(TahunBajet))
            txtTahunBajet.Text = TahunBajet

            txtProgram.Text = dsMohon.Tables("MohonDs").Rows(0)("BG20_Program").ToString

            txtNamaPemohon.Text = Session("ssusrName")
            txtJawPemohon.Text = Session("ssusrPost")

            If Not String.IsNullOrEmpty(PenyokongStafID) Then

                dtUserInfo = fGetUserInfo("02634")

                If dtUserInfo.Rows.Count > 0 Then
                    txtNamaPenyokong = dtUserInfo.Rows.Item(0).Item("MS01_Nama")
                    txtJawPenyokong = dtUserInfo.Rows.Item(0).Item("JawGiliran")
                End If
            End If

            If Not String.IsNullOrEmpty(PengesahStafID) Then

                dtUserInfo = fGetUserInfo("02634")

                If dtUserInfo.Rows.Count > 0 Then
                    txtNamaPengesah = dtUserInfo.Rows.Item(0).Item("MS01_Nama")
                    txtJawPengesah = dtUserInfo.Rows.Item(0).Item("JawGiliran")
                End If
            End If

            Dim strSqlDt = $"Select BG20_NoButiran,BG20_Butiran,BG20_Kuantiti,BG20_AngHrgUnit,BG20_AngJumlah,KodVotSebagai from BG01_MohonDt where BG20_NoMohon='{NoMohon}'"
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

        Response.Redirect("~/FORMS/BAJET/PERMOHONAN BAJET/Kelululusan_NC.aspx")

    End Sub

End Class