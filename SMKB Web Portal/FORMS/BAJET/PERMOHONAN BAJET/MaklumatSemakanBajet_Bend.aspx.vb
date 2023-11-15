Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization
Public Class MaklumatSemakanBajet_Bend
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
                Dim strKodSub = Request.QueryString("KodSub")
                Dim strKodSubMenu = Request.QueryString("KodSubMenu")

                lblKodSub.Text = strKodSub
                lblKodSubMenu.Text = strKodSubMenu

                firstBindViewStateButiran()
                Dim kod = fGetPeringkatBajetPTJ(Session("ssusrKodPTj"))
                fCheckPeringkat(kod)

                Dim passQS_NoMohon = Request.QueryString("no")

                If Request.QueryString("kodSkrin") <> "" Then
                    lblKodSkrin.Text = Request.QueryString("kodSkrin")
                    lblKodVotAmHid.Text = Request.QueryString("kodvot")
                Else
                    lblKodSkrin.Text = "1"
                End If

                fBindDdlJenisDasar()
                'If passQS_NoMohon = "0" Then
                'Mohonbaru()
                'Else
                MohonEdit(passQS_NoMohon)

                'End If

                fBindDdlVotAm()
                ddlVotA.Items.Insert(0, New ListItem("-SILA PILIH-", ""))
                ddlVotA.SelectedIndex = 0

                fBindDdlVot("0", "0")
                ddlVotSbg.Items.Insert(0, New ListItem("-SILA PILIH-", ""))
                ddlVotSbg.SelectedIndex = 0



                'test nostaf
                'Session("ssusrID") = "02534"



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

    Protected Sub gvButiran_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvButiran.RowCommand


        'Dim gvRow As GridViewRow = TryCast(ctrl.Parent.NamingContainer, GridViewRow)
        Dim gvRow As GridViewRow = CType(CType(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
        Dim RowIndex As Integer = gvRow.RowIndex

        'Dim rIndex As Integer = Convert.ToInt32(e.CommandArgument)
        'Dim gvRow As GridViewRow = gvButiran.Rows(rIndex)
        'Dim ToBeDeleteFileName As String = gvRow.Cells(0).Text

        Dim statusDlt_ As Label = CType(gvRow.FindControl("lblBend"), Label)
        Dim statusDlt = statusDlt_.Text

        Dim idDt_ As Label = CType(gvRow.FindControl("lblIDDt"), Label)  ' Find Your Control here
        Dim idDt = idDt_.Text
        Dim noMohon = txtNoMohon.Text

        Dim amaunAsal = ViewState("TotalAmount")
        Dim idAmaun_ As Label = CType(gvRow.FindControl("lblAngHrgUnit"), Label)
        Dim idAmaun1 = idAmaun_.Text

        Dim idAmaunAsal_ As Label = CType(gvRow.FindControl("lblAmaunAsalPTJ"), Label)
        Dim idAmaunAsal1 = idAmaunAsal_.Text


        If e.CommandName = "Padam" Then
            If noMohon <> "" Then

                Dim calc = amaunAsal - idAmaun1

                Dim strSQL_ = $"UPDATE 
                                BG01_MohonDt SET  BG20_JumBendahari = @calc , BG20_JumNC = @calc , BG20_JumLPU = @calc, BG20_FlagBendahari_Delete = @Status,  BG20_FlagNC_Delete = @Status,  BG20_FlagLPU_Delete = @Status Where BG20_NoMohon = '{noMohon}' and BG20_Id = '{idDt}'"


                Dim paramSql_() As SqlParameter =
                     {
                 New SqlParameter("@calc", 0.00),
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


            gvRow.BackColor = System.Drawing.Color.Tomato
            gvRow.FindControl("btnAktif").Visible = True
            gvRow.FindControl("btnDelete").Visible = False


            'Dim selectedRow As GridViewRow = gvButiran.SelectedRow
            ' vsDt.Rows.RemoveAt(RowIndex)
            'ViewState("CurrentButiran") = vsDt

            ' BindGridViewButiran()
            'update amaun
            Dim noPermohonan = txtNoMohon.Text
            'baca amaun latest
            Dim strSQL2_ = $"SELECT ISNULL(sum(ISNULL(BG20_JumBendahari,0)),0) as jumBend FROM BG01_MohonDt Where BG20_NoMohon = '{noMohon}' and BG20_FlagBendahari_Delete <> '0' and BG20_Status = '1'"

            Dim ds = dbconn.fSelectCommand(strSQL2_)
            If ds.Tables.Count > 0 Then
                Dim amaunN = ds.Tables(0).Rows(0)("jumBend").ToString

                Dim strSQL_Save = $"UPDATE 
                    BG01_Mohon SET   BG20_AmaunSyorBendahari = @amaun, BG20_AmaunLulusNC =@amaun , BG20_AmaunLulusLPU =@amaun, BG20_AmaunBajet = @amaun Where BG20_NoMohon = '{noPermohonan}'"

                Dim paramSql_simpan() As SqlParameter =
                 {
                 New SqlParameter("@amaun", amaunN)
                 }


                dbconn.sConnBeginTrans()
                If dbconn.fUpdateCommand(strSQL_Save, paramSql_simpan) > 0 Then
                    'dbconn.fUpdateCommand(strSQL_Save, paramSql_simpan)
                    dbconn.sConnCommitTrans()
                End If


            End If



        ElseIf e.CommandName = "Select" Then

            lblIDDt_.Text = TryCast(gvRow.FindControl("lblIDDt"), Label).Text.ToString
            ddlVotA.SelectedValue = TryCast(gvRow.FindControl("StrKodVotA"), Label).Text.ToString
            fBindDdlVot("0", "0")
            'txtAngJum.Text = TryCast(selectedRow.FindControl("lblAngHrgUnit"), Label).Text.ToString
            txtButiranDt.Text = TryCast(gvRow.FindControl("strButiran"), Label).Text.ToString
            txtButiranDt.Enabled = True

            ViewState("EditBil") = TryCast(gvRow.FindControl("lblNoButiran"), Label).Text.ToString
            'lblRowNumber

            If statusDlt = "True" Then
                lbtnKemaskini.Visible = False
            Else
                lbtnKemaskini.Visible = True
            End If



        ElseIf e.CommandName = "Aktif" Then

            Dim calc = amaunAsal + idAmaunAsal1
            If noMohon <> "" Then

                Dim strSQL_ = $"UPDATE 
                                BG01_MohonDt SET  BG20_JumBendahari = @calc , BG20_JumNC = @calc , BG20_JumLPU = @calc,  BG20_FlagBendahari_Delete = @Status,  BG20_FlagNC_Delete = @Status,  BG20_FlagLPU_Delete = @Status Where BG20_NoMohon = '{noMohon}' and BG20_Id = '{idDt}'"


                Dim paramSql_() As SqlParameter =
                     {
                 New SqlParameter("@calc", idAmaunAsal1),
                 New SqlParameter("@Status", True)
                 }

                dbconn.sConnBeginTrans()
                If dbconn.fUpdateCommand(strSQL_, paramSql_) > 0 Then
                    dbconn.fUpdateCommand(strSQL_, paramSql_)
                    dbconn.sConnCommitTrans()

                    gvRow.BackColor = System.Drawing.Color.White

                    gvRow.FindControl("btnAktif").Visible = False
                    gvRow.FindControl("btnDelete").Visible = True


                End If

                'update amaun
                Dim noPermohonan = txtNoMohon.Text
                'baca amaun latest
                Dim strSQL2_ = $"SELECT ISNULL(sum(ISNULL(BG20_JumBendahari,0)),0) as jumBend FROM BG01_MohonDt Where BG20_NoMohon = '{noMohon}' and BG20_FlagBendahari_Delete <> '0' and BG20_Status = '1'"

                Dim ds = dbconn.fSelectCommand(strSQL2_)
                If ds.Tables.Count > 0 Then
                    Dim amaunN = ds.Tables(0).Rows(0)("jumBend").ToString

                    Dim strSQL_Save = $"UPDATE 
                    BG01_Mohon SET   BG20_AmaunSyorBendahari = @amaun, BG20_AmaunLulusNC =@amaun , BG20_AmaunLulusLPU =@amaun, BG20_AmaunBajet = @amaun Where BG20_NoMohon = '{noPermohonan}'"

                    Dim paramSql_simpan() As SqlParameter =
                     {
                     New SqlParameter("@amaun", amaunN)
                     }


                    dbconn.sConnBeginTrans()
                    If dbconn.fUpdateCommand(strSQL_Save, paramSql_simpan) > 0 Then
                        'dbconn.fUpdateCommand(strSQL_Save, paramSql_simpan)
                        dbconn.sConnCommitTrans()
                    End If


                End If

            End If
        End If



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

            'e.Row.Cells(4).ColumnSpan = intNoOfMergeCol
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
            dt.Columns.AddRange(New DataColumn(7) {
                            New DataColumn("ID", GetType(String)),
                            New DataColumn("NoButiran", GetType(String)),
                            New DataColumn("KodVotA", GetType(String)),
                            New DataColumn("KodVotSbg", GetType(String)),
                            New DataColumn("Butiran", GetType(String)),
                            New DataColumn("AngHrgUnit", GetType(Decimal)),
                            New DataColumn("BendDelete", GetType(String)),
                            New DataColumn("AmaunPTj", GetType(Decimal))
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

    Protected Sub BindGridViewButiran_WithoutReloadGrid()
        Try
            Dim dt = DirectCast(ViewState("CurrentButiran"), DataTable)


            'Dim total As Decimal= Convert.ToDecimal(
            Dim total As Decimal = dt.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("AngHrgUnit"))

            ViewState("TotalAmount") = total
            'gvButiran.DataSource = dt
            'gvButiran.DataBind()


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
        '    lbtnTidakAktif.Visible = False
        '    lbtnReset.Visible = False
        '    lbtnSaveButiran.Visible = False
        'End If

        'lbtnTidakAktif.Visible = Not Yes
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
            Dim strSql As String = "select KodVot, Butiran, (KodVot + ' - ' + Butiran) as Butiran2 from mk_vot where klasifikasi = 'H2' and LEFT(kodvot,1) =  '" & votA & "' order by kodvot"
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
            Dim dsMohonDt As New DataSet
            Dim dbconn As New DBKewConn
            Dim dtUserInfo As New DataTable

            txtNoMohon.Text = NoMohon

            Dim strSql = $"Select KodPtj,KodBahagian,KodUnitPtj,KodMaksud,KodDasar,KodKW, BG20_TarikhMohon,BG20_TahunBajet,
                            KodOperasi,BG20_StaffIDPemohon,BG20_StaffIDPenyokong,BG20_StaffIDPengesah,StatusDok,CASE WHEN BG20_AmaunSyorBendahari IS NOT NULL  THEN BG20_AmaunSyorBendahari ELSE BG20_AmaunMohon END AS BG20_AmaunMohon,
                            BG20_Justifikasi,BG20_Program, BG20_AmaunMohon , BG20_FlagBendahari_Delete
                            from BG01_Mohon where BG20_NoMohon='{NoMohon}' and BG20_Status = '1'"

            dsMohon = dbconn.fSelectCommand(strSql)

            Dim KodPTj = dsMohon.Tables(0).Rows(0)("KodPtj").ToString
            Dim KodBahagian = dsMohon.Tables(0).Rows(0)("KodBahagian").ToString
            Dim KodUnit = dsMohon.Tables(0).Rows(0)("KodUnitPtj").ToString

            txtPtj.Text = fGetPTJ(KodPTj)
            txtBahagian.Text = fGetBahagianPTJ(KodBahagian)
            txtUnit.Text = fGetUnitPTJ(KodUnit)

            lblPtj.Text = KodPTj
            lblBahagian.Text = KodBahagian
            lblUnit.Text = KodUnit

            Dim KodMaksud = dsMohon.Tables(0).Rows(0)("KodMaksud").ToString
            Dim KodDsr = dsMohon.Tables(0).Rows(0)("KodDasar").ToString

            If Not IsDBNull(dsMohon.Tables(0).Rows(0)("BG20_TarikhMohon")) Then
                Dim tarikh As Date = dsMohon.Tables(0).Rows(0)("BG20_TarikhMohon")
                txtTarikh.Text = tarikh.ToShortDateString
            End If

            Dim TahunBajet = dsMohon.Tables(0).Rows(0)("BG20_TahunBajet").ToString
            Dim KodOperasi = dsMohon.Tables(0).Rows(0)("KodOperasi").ToString
            Dim KodKW = dsMohon.Tables(0).Rows(0)("KodKW").ToString
            txtNamaPemohon.Text = dsMohon.Tables(0).Rows(0)("BG20_StaffIDPemohon").ToString
            Dim PenyokongStafID = dsMohon.Tables(0).Rows(0)("BG20_StaffIDPenyokong").ToString
            Dim PengesahStafID = dsMohon.Tables(0).Rows(0)("BG20_StaffIDPengesah").ToString
            Dim jumlahMohon = dsMohon.Tables(0).Rows(0)("BG20_AmaunMohon").ToString
            txtJumlahMohon.Text = FormatNumber(jumlahMohon, 2)

            Dim statusDLT = dsMohon.Tables(0).Rows(0)("BG20_FlagBendahari_Delete").ToString
            If statusDLT = "True" Then
                lbtnAktif.Visible = True
                lbtnTidakAktif.Visible = False

                lbtnSaveButiran.Visible = False
                lbtnSimpan.Visible = False
                'lbtnKemaskini.Visible = False

            Else
                lbtnAktif.Visible = False
                lbtnTidakAktif.Visible = True

                lbtnSaveButiran.Visible = True
                lbtnSimpan.Visible = True
                'lbtnKemaskini.Visible = True
            End If


            'Bahagian

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


            fBindDdlJenisDasar()
            fBindDdlKodOperasi()
            fBindDdlKW()

            Dim StatusDok = fGetStatusDok(dsMohon.Tables(0).Rows(0)("StatusDok").ToString)
            KodStatus = StatusDok.FirstOrDefault.Key

            'If KodStatus.Equals("05") Then
            '    'lbtnHantar.Visible = False
            '    'lbtnKemaskini.Visible = False
            '    'lbtnTidakAktif.Visible = True
            '    'lbtnReset.Visible = True
            '    'lbtnSaveButiran.Visible = True

            'Else
            '    'lbtnTidakAktif.Visible = False
            '    lbtnReset.Visible = False
            '    lbtnSaveButiran.Visible = False
            'End If

            txtStatus.Text = StatusDok.FirstOrDefault.Value
            'txtPtj.Text = fGetPTJ(KodPTj)


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

            'txtNamaPemohon.Text = Session("ssusrName")
            'txtJawPemohon.Text = Session("ssusrPost")



            Dim strSqlDt = $"Select BG20_Id, BG20_NoButiran,BG20_Butiran,BG20_Kuantiti,  ISNULL((BG20_JumKetuaPTj),0)  as jumPTj,CASE WHEN BG20_JumBendahari IS NOT NULL  THEN ISNULL((BG20_JumBendahari),0) ELSE ISNULL((BG20_JumKetuaPTj),0) END AS BG20_AmaunMohon,BG20_AngJumlah,KodVotSebagai,KodVotAm, BG20_Butiran, ISNULL(BG20_FlagBendahari_Delete, '0') AS BendDelete from BG01_MohonDt where BG20_NoMohon='{NoMohon}' and BG20_Status = '1'"
            dsMohonDt = dbconn.fSelectCommand(strSqlDt)

            Dim drCurrentRow As DataRow = Nothing

            'get datatable from view state 
            Dim dtCurrentTable As DataTable = DirectCast(ViewState("CurrentButiran"), DataTable)

            For Each rowdt In dsMohonDt.Tables(0).Rows
                'add each row into data table
                drCurrentRow = dtCurrentTable.NewRow()
                ' drCurrentRow("ID") = rowdt("ID").ToString
                drCurrentRow("ID") = rowdt("BG20_Id").ToString
                drCurrentRow("NoButiran") = rowdt("BG20_NoButiran").ToString
                drCurrentRow("KodVotA") = rowdt("KodVotAm").ToString
                drCurrentRow("KodVotSbg") = rowdt("KodVotSebagai").ToString
                drCurrentRow("Butiran") = rowdt("BG20_Butiran").ToString
                drCurrentRow("AngHrgUnit") = CDec(rowdt("BG20_AmaunMohon")) '.ToString("#,##0.00")
                drCurrentRow("BendDelete") = rowdt("BendDelete").ToString
                drCurrentRow("AmaunPTj") = CDec(rowdt("jumPTj"))

                dtCurrentTable.Rows.Add(drCurrentRow)
            Next

            'saving datatable into viewstate
            ViewState("CurrentButiran") = dtCurrentTable

            'bind Gridview
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
        Dim strKodSub = Request.QueryString("KodSub")
        Dim strKodSubMenu = Request.QueryString("KodSubMenu")
        Dim strKodPTj = lblPtj.Text
        Dim strTahun = txtTahunBajet.Text
        Dim strKodKW = ddlKW.SelectedValue
        Dim strKodKO = ddlKodOperasi.SelectedValue
        Dim strKodVot = lblKodVotAmHid.Text
        Dim kod = lblKodSkrin.Text

        'If lblKodSkrin.Text = "1" Then
        Response.Redirect($"~/FORMS/BAJET/PERMOHONAN BAJET/Maklumat_Agihan_Bajet_Am_Bend.aspx?KodSub=" & strKodSub & "&KodSubMenu=" & strKodSubMenu & "&KodPTj=" & strKodPTj & "&Tahun=" & strTahun & "&KodKW=" & strKodKW & "&kodVot=" & strKodVot & "&strKodKO=" & strKodKO & "", False)
        'Else
        'Response.Redirect("~/FORMS/BAJET/PERMOHONAN BAJET/Penyediaan_Bajet_Bendahari.aspx")
        'End If


    End Sub

    Protected Sub lbtnReset_Click(sender As Object, e As EventArgs) Handles lbtnReset.Click
        clearTextButiran()
    End Sub
    Private Sub ddlVotA_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlVotA.SelectedIndexChanged
        Dim strA = Left(ddlVotA.SelectedValue.ToString(), 1)
        fBindDdlVot(strA, "0")
    End Sub

    Protected Sub lbtnAktif_Click(sender As Object, e As EventArgs) Handles lbtnAktif.Click

        Dim strKodSub = Request.QueryString("KodSub")
        Dim strKodSubMenu = Request.QueryString("KodSubMenu")
        Dim strKodPTj = lblPtj.Text
        Dim strTahun = txtTahunBajet.Text
        Dim strKodKW = ddlKW.SelectedValue
        Dim strKodKO = ddlKodOperasi.SelectedValue
        Dim strKodVot = lblKodVotAmHid.Text
        Dim kodSkrin As String = "2"

        Dim noMohon = txtNoMohon.Text

        Dim strSQL_DeleteMain = $"UPDATE 
                    BG01_Mohon SET BG20_FlagBendahari_Delete = @Delete  Where BG20_NoMohon = @NoMohon and BG20_Status = 1"

        Dim paramSql_simpan() As SqlParameter =
             {
             New SqlParameter("@NoMohon", noMohon),
             New SqlParameter("@Delete", 0)
             }

        Dim strSQL_DeleteDt = $"UPDATE 
                    BG01_MohonDt SET BG20_FlagBendahari_Delete = @Delete   Where BG20_NoMohon = @NoMohon and BG20_Status = 1"

        Dim paramSql_simpan_() As SqlParameter =
             {
             New SqlParameter("@NoMohon", noMohon),
             New SqlParameter("@Delete", 0)
             }

        dbconn.sConnBeginTrans()
        If dbconn.fUpdateCommand(strSQL_DeleteMain, paramSql_simpan) > 0 Then
            If dbconn.fUpdateCommand(strSQL_DeleteDt, paramSql_simpan_) > 0 Then

                dbconn.fUpdateCommand(strSQL_DeleteMain, paramSql_simpan)
                dbconn.fUpdateCommand(strSQL_DeleteDt, paramSql_simpan)
                dbconn.sConnCommitTrans()
                fGlobalAlert($"Rekod telah bertukar status AKTIF.", Me.Page, Me.[GetType](), "../../BAJET/PERMOHONAN BAJET/MaklumatSemakanBajet_Bend.aspx?KodSub=" & strKodSub & "&KodSubMenu=" & strKodSubMenu & "&KodPTj=" & strKodPTj & "&Tahun=" & strTahun & "&KodKW=" & strKodKW & "&kodVot=" & strKodVot & "&strKodKO=" & strKodKO & "&no=" & noMohon & "&kodSkrin=" & kodSkrin & "")


            Else

                dbconn.sConnRollbackTrans()
                fGlobalAlert("Maklumat Butiran tidak berjaya tukar ke status AKTIF!", Me.Page, Me.[GetType]())

            End If
        End If

    End Sub
    Protected Sub lbtnTidakAktif_Click(sender As Object, e As EventArgs) Handles lbtnTidakAktif.Click

        Dim strKodSub = Request.QueryString("KodSub")
        Dim strKodSubMenu = Request.QueryString("KodSubMenu")
        Dim strKodPTj = lblPtj.Text
        Dim strTahun = txtTahunBajet.Text
        Dim strKodKW = ddlKW.SelectedValue
        Dim strKodKO = ddlKodOperasi.SelectedValue
        Dim strKodVot = lblKodVotAmHid.Text
        Dim kodSkrin As String = "2"

        Dim noMohon = txtNoMohon.Text

        Dim strSQL_DeleteMain = $"UPDATE 
                    BG01_Mohon SET BG20_FlagBendahari_Delete = @Delete  Where BG20_NoMohon = @NoMohon and BG20_Status = 1"

        Dim paramSql_simpan() As SqlParameter =
             {
             New SqlParameter("@NoMohon", noMohon),
             New SqlParameter("@Delete", 1)
             }

        Dim strSQL_DeleteDt = $"UPDATE 
                    BG01_MohonDt SET BG20_FlagBendahari_Delete = @Delete   Where BG20_NoMohon = @NoMohon and BG20_Status = 1"

        Dim paramSql_simpan_() As SqlParameter =
             {
             New SqlParameter("@NoMohon", noMohon),
             New SqlParameter("@Delete", 1)
             }

        dbconn.sConnBeginTrans()
        If dbconn.fUpdateCommand(strSQL_DeleteMain, paramSql_simpan) > 0 Then
            If dbconn.fUpdateCommand(strSQL_DeleteDt, paramSql_simpan_) > 0 Then

                dbconn.fUpdateCommand(strSQL_DeleteMain, paramSql_simpan)
                dbconn.fUpdateCommand(strSQL_DeleteDt, paramSql_simpan)
                dbconn.sConnCommitTrans()
                fGlobalAlert($"Rekod telah bertukar status TIDAK AKTIF.", Me.Page, Me.[GetType](), "../../BAJET/PERMOHONAN BAJET/MaklumatSemakanBajet_Bend.aspx?KodSub=" & strKodSub & "&KodSubMenu=" & strKodSubMenu & "&KodPTj=" & strKodPTj & "&Tahun=" & strTahun & "&KodKW=" & strKodKW & "&kodVot=" & strKodVot & "&strKodKO=" & strKodKO & "&no=" & noMohon & "&kodSkrin=" & kodSkrin & "")

            Else

                dbconn.sConnRollbackTrans()
                fGlobalAlert("Maklumat Butiran tidak berjaya tukar ke status TIDAK AKTIF!", Me.Page, Me.[GetType]())

            End If
        End If

    End Sub
    Protected Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.Click
        Try
            Page.Validate("btnSimpan")

            If Page.IsValid Then
                Dim noPermohonan As String = ""
                'Dim tarikhMohon As DateTime
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

                'If ViewState("TotalAmount") <= txtJumlahMohon.Text Then

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
                    KodKW = @kodkw, BG20_Justifikasi = @justifikasi,  BG20_AmaunSyorBendahari = @amaun, BG20_AmaunBajet = @amaun Where BG20_NoMohon = '{noPermohonan}' and BG20_Status = '1'"

                    Dim paramSql_simpan() As SqlParameter =
                         {
                         New SqlParameter("@bahagian", lblBahagian.Text),'fGetKodBah()),                         
                         New SqlParameter("@unit", lblUnit.Text),  'fGetKodUnit()),
                         New SqlParameter("@dasar", ddlJenDasar.SelectedValue),
                         New SqlParameter("@operasi", ddlKodOperasi.SelectedValue),
                         New SqlParameter("@Program", txtProgram.Text),
                         New SqlParameter("@KodKW", ddlKW.SelectedValue),
                         New SqlParameter("@justifikasi", txtJust.Text),
                         New SqlParameter("@amaun", ViewState("TotalAmount"))
                         }


                    dbconn.sConnBeginTrans()
                    If dbconn.fUpdateCommand(strSQL_Save, paramSql_simpan) > 0 Then
                        dbconn.fUpdateCommand(strSQL_Save, paramSql_simpan)
                        dbconn.sConnCommitTrans()

                        ' Dim strSQL_Delete = $"DELETE FROM BG01_MohonDt Where BG20_NoMohon = @nomohon"
                        'Dim paramSql_delete() As SqlParameter =
                        '{
                        'New SqlParameter("@nomohon", noPermohonan)
                        '}

                        'dbconn.fUpdateCommand(strSQL_Delete, paramSql_delete)
                        'dbconn.sConnCommitTrans()

                        'Dim strSQL1_ = $"UPDATE 
                        '        BG01_MohonDt SET  BG20_Aktif = @Status Where BG20_NoMohon = '{noPermohonan}' and BG20_FlagBendahari_Delete = '0' "


                        'Dim paramSql1_() As SqlParameter =
                        '{
                        'New SqlParameter("@Status", False)
                        '}

                        'dbconn.sConnBeginTrans()
                        'If dbconn.fUpdateCommand(strSQL1_, paramSql1_) > 0 Then
                        '    dbconn.fUpdateCommand(strSQL1_, paramSql1_)
                        '    dbconn.sConnCommitTrans()
                        'End If


                        countButiran = 0
                        For Each drCurrentRow As DataRow In dtCurrentTable.Rows
                            'AngHrgUnit = Convert.ToDecimal(drCurrentRow("AngHrgUnit"))
                            'AngJumlah = Convert.ToDecimal(drCurrentRow("AngJumlah"))

                            If IsDBNull(drCurrentRow("ID")) Then
                                AngHrgUnit = drCurrentRow("AngHrgUnit")
                                'AngJumlah = drCurrentRow("AngJumlah")


                                strSqlButiran = "INSERT INTO BG01_MohonDt ( BG20_NoButiran, BG20_NoMohon, KodVotAm, KodVotSebagai,  BG20_JumBendahari, BG20_JumNC, BG20_AngJumlah,  BG20_Butiran, BG20_Aktif)
                                            VALUES (@NoButiran, @NoMohon ,@KodVotAm, @KodVotSbg, @AngJumlah,@AngJumlah, @AngJumlah,  @Butiran, @Aktif )"
                                paramSqlBtrn = {
                                New SqlParameter("@NoButiran", drCurrentRow("NoButiran")),
                                New SqlParameter("@NoMohon", noPermohonan),
                                New SqlParameter("@KodVotAm", drCurrentRow("KodVotA")),
                                New SqlParameter("@KodVotSbg", drCurrentRow("KodVotSbg")),
                                New SqlParameter("@AngJumlah", AngHrgUnit),
                                New SqlParameter("@Butiran", drCurrentRow("Butiran").Replace("'", "\")),
                                New SqlParameter("@Aktif", True)
                            }

                                If dbconn.fInsertCommand(strSqlButiran, paramSqlBtrn) > 0 Then
                                    'countButiran = countButiran + 1
                                    'KodStatus = "01"
                                    dbconn.sConnCommitTrans()
                                    'sAuditLog(Session("ssusrID"), Request.QueryString("KodSubMenu"), "BG20_Mohon", "1", "Simpan Button")
                                Else
                                    dbconn.sConnRollbackTrans()
                                    'fErrorLog("Jenis_Permohonan.aspx(btnSave_Click)- " & ex.Message.ToString)
                                End If

                            Else
                                Dim strSQL2_ = $"UPDATE 
                                BG01_MohonDt SET BG20_NoButiran = @NoButiran, BG20_NoMohon=@NoMohon, BG20_Butiran=@Butiran, 
                                BG20_AngJumlah= @AngJumlah,  BG20_JumBendahari= @AngJumlah, BG20_JumNC= @AngJumlah, KodVotAm = @KodVotAm, KodVotSebagai = @KodVotSbg  Where BG20_NoMohon = '{noPermohonan}' and BG20_Id = '{drCurrentRow("ID")}'"

                                info = drCurrentRow("Butiran")
                                AngHrgUnit = drCurrentRow("AngHrgUnit")

                                Dim paramSql2_() As SqlParameter =
                                     {
                                    New SqlParameter("@NoButiran", drCurrentRow("NoButiran")),
                                    New SqlParameter("@NoMohon", noPermohonan),
                                    New SqlParameter("@KodVotAm", drCurrentRow("KodVotA")),
                                    New SqlParameter("@KodVotSbg", drCurrentRow("KodVotSbg")),
                                    New SqlParameter("@AngJumlah", AngHrgUnit),
                                    New SqlParameter("@Butiran", info.Replace("'", "\"))
                                     }


                                'dbconn.sConnBeginTrans()
                                If dbconn.fUpdateCommand(strSQL2_, paramSql2_) > 0 Then
                                    countButiran = countButiran + 1
                                    dbconn.fUpdateCommand(strSQL2_, paramSql2_)
                                    dbconn.sConnCommitTrans()
                                End If



                            End If


                        Next

                        If countButiran > 0 Then
                            If lblKodSkrin.Text = "2" Then
                                Dim strNoMohon = txtNoMohon.Text
                                Dim kodVot = lblKodVotAmHid.Text
                                Dim KodSub = lblKodSub.Text
                                Dim KodSubMenu = lblKodSubMenu.Text

                                fGlobalAlert($"Rekod baru telah ditambah bersama '{countButiran.ToString}' butiran permohonan!", Me.Page, Me.[GetType](), $"../../BAJET/PERMOHONAN BAJET/MaklumatSemakanBajet_Bend.aspx?KodSub={KodSub}&KodSubMenu={KodSubMenu}&kodSkrin=2&no={strNoMohon}&kodvot={kodVot}")

                            Else

                                fGlobalAlert($"Rekod baru telah ditambah bersama '{countButiran.ToString}' butiran permohonan!", Me.Page, Me.[GetType](), "../../BAJET/PERMOHONAN BAJET/Penyediaan_Bajet_Bendahari.aspx")

                            End If


                        Else
                            fGlobalAlert("Maklumat Butiran tidak berjaya disimpan!", Me.Page, Me.[GetType]())
                        End If
                    End If



                    'End If

                    'fGlobalAlert($"Rekod telah dikemaskini!", Me.Page, Me.[GetType](), "../../BAJET/PERMOHONAN BAJET/Permohonan_Bajet_Tahunan.aspx")
                    'End Using


                Else

                    Dim ptjj = Session("ssusrKodPTj")
                    'Add new record
                    noPermohonan = fGetNoMohon()
                    'tarikhMohon = DateTime.ParseExact(txtTarikh.Text, "dd/MM/yyyy", New CultureInfo("en-US"))

                    Dim dt = Date.Today.ToString("dd/MM/yyyy").ToString
                    Dim dtTkhM As Date = DateTime.ParseExact(dt, "dd/MM/yyyy", CultureInfo.CurrentCulture)
                    Dim tarikhMohon As String = dtTkhM.ToString("yyyy-MM-dd")


                    strSql = "INSERT INTO BG01_MOHON (BG20_NoMohon,BG20_TarikhMohon,KodPtj,KodBahagian,KodUnitPtj,KodKorporat,
                            BG20_Program,BG20_Justifikasi,BG20_TahunBajet,KodDasar,KodOperasi,KodKW,BG20_Status,StatusDok,BG20_AmaunMohon, BG20_StaffIDPemohon)
                            VALUES (@NoMohon, @tarikhMohon, @KodPTJ, @KodBah, @KodUnit, @KodAgensi,
                            @Program, @Justification, @TahunBajet, @KodJenDsr, @KodOperasi, @KodKW, @Status,  @StatusDok, @AmaunMohon, @idMohon )"

                    Dim paramSql() As SqlParameter = {
                        New SqlParameter("@NoMohon", noPermohonan),
                        New SqlParameter("@tarikhMohon", tarikhMohon),
                        New SqlParameter("@KodPTJ", lblPtj.Text),'Left(Session("ssusrKodPTj"), 2)),
                        New SqlParameter("@KodBah", lblBahagian.Text),'fGetKodBah()),
                        New SqlParameter("@KodUnit", lblUnit.Text),'fGetKodUnit()),
                        New SqlParameter("@KodAgensi", fSelectNamaKorporat()),
                        New SqlParameter("@Program", txtProgram.Text),
                        New SqlParameter("@Justification", txtJust.Text),
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

                            strSqlButiran = "INSERT INTO BG01_MohonDt ( BG20_NoButiran, BG20_NoMohon, KodVotAm, KodVotSebagai, BG20_AngHrgUnit, BG20_AngJumlah, BG20_Status, BG20_Butiran)
                                            VALUES (" & (countButiran + 1) & ", @NoMohon ,@KodVotAm, @KodVotSbg, @AngHargaSeUnit, @AngJumlah, @Status, @Butiran )"
                            paramSqlBtrn = {
                                New SqlParameter("@NoMohon", noPermohonan),
                                New SqlParameter("@KodVotAm", drCurrentRow("KodVotA")),
                                New SqlParameter("@KodVotSbg", drCurrentRow("KodVotSbg")),
                                New SqlParameter("@AngHargaSeUnit", AngHrgUnit),
                                New SqlParameter("@AngJumlah", AngHrgUnit),
                                New SqlParameter("@Butiran", drCurrentRow("Butiran")),
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
                            fGlobalAlert($"Rekod telah dikemaskini bersama '{countButiran.ToString}' butiran permohonan!", Me.Page, Me.[GetType](), "../../BAJET/PERMOHONAN BAJET/Penyediaan_Bajet_Bendahari.aspx")
                        Else
                            fGlobalAlert("Maklumat Butiran tidak berjaya disimpan!", Me.Page, Me.[GetType]())
                        End If

                    Else
                        dbconn.sConnRollbackTrans()
                        fGlobalAlert("Maklumat tidak berjaya disimpan!", Me.Page, Me.[GetType]())
                    End If
                End If
                'Else
                fGlobalAlert("Maklumat tidak berjaya disimpan. Jumlah terkini telah melebihi jumlah asal permohonan.", Me.Page, Me.[GetType]())
                'End If
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
        Try
            'Dim strSqlNo = $"SELECT BG20_NoMohon FROM BG01_Mohon WHERE kodptj ='{Left(Session("ssusrKodPTj"), 2)}' AND BG20_TahunBajet = '{txtTahunBajet.Text}' ORDER BY ID DESC"
            Dim strSqlNo = $"SELECT BG20_NoMohon FROM BG01_Mohon WHERE kodptj ='{lblPtj.Text}' AND BG20_TahunBajet = '{txtTahunBajet.Text}' ORDER BY ID DESC"

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

    Protected Sub gvButiran_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvButiran.RowDeleting

        Dim vsDt As DataTable = TryCast(ViewState("CurrentButiran"), DataTable)
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

        'lbtnKemaskini.Visible = True
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


    Private Sub gvButiran_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvButiran.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim benddelete As Label = DirectCast(e.Row.FindControl("lblBend"), Label)
                Dim chck = benddelete.Text
                If chck = "True" Then
                    e.Row.BackColor = System.Drawing.Color.Tomato
                Else
                    e.Row.BackColor = System.Drawing.Color.White
                End If




            End If


        Catch ex As Exception

        End Try
    End Sub

End Class