﻿Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization

Public Class MaklumatSemakanBajet_PTJ
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

                fBindDdlVotAm()
                ddlVotA.Items.Insert(0, New ListItem("-SILA PILIH-", ""))
                ddlVotA.SelectedIndex = 0

                fBindDdlVot()
                ddlVotSbg.Items.Insert(0, New ListItem("-SILA PILIH-", ""))
                ddlVotSbg.SelectedIndex = 0

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
                'get datatable from view state 
                Dim dtCurrentTable As DataTable = DirectCast(ViewState("CurrentButiran"), DataTable)

                Dim aaa = ddlVotA.SelectedValue
                'add each row into data table
                drCurrentRow = dtCurrentTable.NewRow()
                drCurrentRow("NoButiran") = countButiran
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

            e.Row.Cells(3).ColumnSpan = intNoOfMergeCol
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
            dt.Columns.AddRange(New DataColumn(4) {
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
        ddlVotA.SelectedIndex = 0
        ddlVotSbg.ClearSelection()

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

    Private Sub fBindDdlVot()
        Try
            Dim strSql As String = "select KodVot, Butiran, (KodVot + ' - ' + Butiran) as Butiran2 from mk_vot where klasifikasi = 'H2' and LEFT(kodvot,1) =  '" & Left(Trim(ddlVotA.SelectedValue.TrimEnd), 1) & "' order by kodvot"
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
            txtJust.Text = dsMohon.Tables(0).Rows(0)("BG20_Justifikasi").ToString
            'ddlTahunBajet.SelectedIndex = ddlTahunBajet.Items.IndexOf(ddlTahunBajet.Items.FindByText(TahunBajet))
            txtTahunBajet.Text = TahunBajet

            txtProgram.Text = dsMohon.Tables(0).Rows(0)("BG20_Program").ToString

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

            Dim strSqlDt = $"Select BG20_NoButiran,BG20_Butiran,BG20_Kuantiti,BG20_AngHrgUnit,BG20_AngJumlah,KodVotSebagai,KodVotAm, BG20_Butiran from BG01_MohonDt where BG20_NoMohon='{NoMohon}'"
            dsMohonDt = dbconn.fSelectCommand(strSqlDt)

            Dim drCurrentRow As DataRow = Nothing

            'get datatable from view state 
            Dim dtCurrentTable As DataTable = DirectCast(ViewState("CurrentButiran"), DataTable)


            For Each rowdt In dsMohonDt.Tables(0).Rows
                'add each row into data table
                drCurrentRow = dtCurrentTable.NewRow()
                drCurrentRow("NoButiran") = rowdt("BG20_NoButiran").ToString
                drCurrentRow("KodVotA") = rowdt("KodVotAm").ToString
                drCurrentRow("KodVotSbg") = rowdt("KodVotSebagai").ToString
                drCurrentRow("Butiran") = rowdt("Butiran").ToString
                drCurrentRow("AngHrgUnit") = CDec(rowdt("BG20_AngHrgUnit")) '.ToString("#,##0.00")
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

        Response.Redirect("~/FORMS/BAJET/PERMOHONAN BAJET/Pengesahan_Ketua_PTJ.aspx")

    End Sub
    Private Sub ddlVotA_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlVotA.SelectedIndexChanged
        fBindDdlVot()
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
                Dim resultUpdate = False
                Dim dr As DataRow = Nothing

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

                    Dim strSqlMohon = $"Select BG01_NoMohon, KodBahagian,KodUnitPtj,KodDasar,KodOperasi, KodKW, BG20_Program,BG20_Justifikasi,BG20_AmaunMohon from BG01_Mohon where BG20_NoMohon='{noPermohonan}'"

                    dbconn.sConnBeginTrans()
                    Using dsMohonBj = dbconn.fSelectCommand(strSqlMohon, "MohonDs", True)
                        If dsMohonBj.Tables(0).Rows.Count > 0 Then
                            dr = dsMohonBj.Tables(0).Rows(0)
                            dr("KodBahagian") = fGetKodBah()
                            dr("KodUnitPtj") = fGetKodUnit()
                            dr("KodDasar") = ddlJenDasar.SelectedValue
                            dr("KodOperasi") = ddlKodOperasi.SelectedValue
                            dr("BG20_Program") = txtProgram.Text
                            dr("KodKW") = ddlKW.SelectedValue
                            dr("BG20_Justifikasi") = txtJust.Text
                            dr("BG20_AmaunMohon") = ViewState("TotalAmount")
                            'sAuditLog(Session("ssusrID"), Request.QueryString("KodSubMenu"), "BG20_Mohon", "2", "Edit Permohonan Bajet") 'TransID: 1-Insert,2-Update,3-Delete

                            dbconn.sUpdateCommand(dsMohon, strSqlMohon, resultUpdate, False)

                        End If

                        'fGlobalAlert($"Rekod telah dikemaskini!", Me.Page, Me.[GetType](), "../../BAJET/PERMOHONAN BAJET/Permohonan_Bajet_Tahunan.aspx")
                    End Using

                    Dim strSQlDt = $"SELECT * From BG01_MohonDt where BG20_NoMohon='{noPermohonan}'"
                    Using dsMohonBjDt = dbconn.fSelectCommand(strSQlDt, "MohonDtDs", True)
                        Dim OriginalDt = dsMohonBjDt.Tables(0)
                        Dim Bil = 0


                    End Using

                Else

                    Dim ptjj = Session("ssusrKodPTj")
                    'Add new record
                    noPermohonan = fGetNoMohon()
                    tarikhMohon = DateTime.ParseExact(txtTarikh.Text, "dd/MM/yyyy", New CultureInfo("en-US"))

                    strSql = "INSERT INTO BG01_MOHON (BG20_NoMohon,BG20_TarikhMohon,KodPtj,KodBahagian,KodUnitPtj,KodKorporat,
                            BG20_Program,BG20_Justifikasi,BG20_TahunBajet,KodDasar,KodOperasi,KodKW,BG20_Status,StatusDok,BG20_AmaunMohon)
                            VALUES (@NoMohon, @tarikhMohon, @KodPTJ, @KodBah, @KodUnit, @KodAgensi,
                            @Program, @Justification, @TahunBajet, @KodJenDsr, @KodOperasi, @KodKW, @Status,  @StatusDok, @AmaunMohon )"

                    Dim paramSql() As SqlParameter = {
                        New SqlParameter("@NoMohon", noPermohonan),
                        New SqlParameter("@tarikhMohon", tarikhMohon),
                        New SqlParameter("@KodPTJ", Left(Session("ssusrKodPTj"), 2)),
                        New SqlParameter("@KodBah", fGetKodBah()),
                        New SqlParameter("@KodUnit", fGetKodUnit()),
                        New SqlParameter("@KodAgensi", fSelectNamaKorporat()),
                        New SqlParameter("@Program", txtProgram.Text),
                        New SqlParameter("@Justification", txtJust.Text),
                        New SqlParameter("@TahunBajet", txtTahunBajet.Text),
                        New SqlParameter("@KodJenDsr", ddlJenDasar.SelectedValue),
                        New SqlParameter("@KodOperasi", ddlKodOperasi.SelectedValue),
                        New SqlParameter("@KodKW", ddlKW.SelectedValue),
                        New SqlParameter("@Status", True),
                        New SqlParameter("@StatusDok", "01"),
                        New SqlParameter("@AmaunMohon", ViewState("TotalAmount"))
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

                        dbconn.fInsertCommand(strSQLStatusDok, paramSql2)
                        'dbconn.sUpdateCommand2(strSQLStatusDok, paramSql2, True, True, resultUpdate, resultCommit)
                        dbconn.sConnCommitTrans()
                        'sAuditLog(Session("ssusrID"), Request.QueryString("KodSubMenu"), "BG20_Mohon", "1", "Simpan Button") 'TransID: 1-Insert,2-Update,3-Delete

                        countButiran = 0
                        For Each drCurrentRow As DataRow In dtCurrentTable.Rows
                            'AngHrgUnit = Convert.ToDecimal(drCurrentRow("AngHrgUnit"))
                            'AngJumlah = Convert.ToDecimal(drCurrentRow("AngJumlah"))
                            AngHrgUnit = drCurrentRow("AngHrgUnit")
                            'AngJumlah = drCurrentRow("AngJumlah")

                            strSqlButiran = "INSERT INTO BG01_MohonDt ( BG20_NoButiran, BG20_NoMohon, BG20_Butiran, KodVotAm, KodVotSebagai, BG20_AngHrgUnit, BG20_AngJumlah, BG20_Status, BG20_Butiran)
                                            VALUES (" & (countButiran + 1) & ", @NoMohon,'-' ,@KodVotAm, @KodVotSbg, @AngHargaSeUnit, @AngJumlah, @Status, @Butiran )"
                            paramSqlBtrn = {
                                New SqlParameter("@NoMohon", noPermohonan),
                                New SqlParameter("@KodVotAm", drCurrentRow("KodVotA")),
                                New SqlParameter("@KodVotSbg", drCurrentRow("KodVotSbg")),
                                New SqlParameter("@AngHargaSeUnit", AngHrgUnit),
                                New SqlParameter("@AngJumlah", AngHrgUnit),
                                New SqlParameter("@Butiran", txtButiranDt.Text),
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

                        Next

                        If countButiran > 0 Then
                            fGlobalAlert($"Rekod baru telah ditambah bersama '{countButiran.ToString}' butiran permohonan!", Me.Page, Me.[GetType](), "../../BAJET/PERMOHONAN BAJET/Penyediaan_Bajet_PTJ.aspx")
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
        Dim noMohon As String = "BG" + Left(Session("ssusrKodPTj"), 2) + fGetKodBah().Substring(2) + fGetKodUnit().Substring(4) + fGetRunnningNo() + month + year
        Return noMohon
    End Function

    ''' <summary>
    ''' Get 6 digit of running number base on same PTJ and year
    ''' </summary>
    ''' <returns>running number</returns>
    Private Function fGetRunnningNo()
        Dim runningNo As String = ""
        Try
            Dim strSqlNo = $"SELECT BG20_NoMohon FROM BG01_Mohon WHERE kodptj ='{Left(Session("ssusrKodPTj"), 2)}' AND BG20_TahunBajet = '{txtTahunBajet.Text}' ORDER BY ID DESC"
            Dim dbconn As New DBKewConn
            Dim ds = dbconn.fSelectCommand(strSqlNo)

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

End Class