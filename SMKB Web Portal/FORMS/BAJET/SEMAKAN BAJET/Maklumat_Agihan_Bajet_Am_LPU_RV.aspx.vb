
Imports System.Drawing
Imports System.Data.SqlClient
Imports System.Web
Imports System.Globalization
Public Class Maklumat_Agihan_Bajet_Am_LPU_RV
    Inherits System.Web.UI.Page


    Dim dbconn As New DBKewConn
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim strTahun = Request.QueryString("Tahun")
            Dim strKodKW = Request.QueryString("KodkW")
            Dim strKodPTj = Request.QueryString("KodPTj")
            Dim strVotAm = Request.QueryString("VotAm")
            'Dim strKO = Request.QueryString("KO")
            Dim strKodVot = Request.QueryString("kodVot")
            Dim strKodKO = Request.QueryString("strKodKO")

            Dim strkodName As String

            If strKodKW = "- SILA PILIH KW -" Then
                strKodKW = "01"
            End If


            fBindDdlUnitPTj(strKodPTj)

            txtKP.Text = "DEFAULT"
            lblHidObjAm.Text = strKodVot
            txtObjAm.Text = strKodVot & "-" & strkodName
            txtTahun.Text = strTahun
            txtKW.Text = strKodKW & " - " & fGetButKW(strKodKW)
            txtPTj.Text = strKodPTj & " - " & fGetButPTj(strKodPTj)
            Dim strKodKP = "0000000"

            If strKodKO = "01" Then
                txtKO.Text = "OPERASI"
                txtKo2.Text = "OPERASI"
            ElseIf strKodKO = "02" Then
                txtKO.Text = "KOMITED"
                txtKo2.Text = "KOMITED"
            End If

            hidKodKW.Value = strKodKW
            hidKodPTj.Value = strKodPTj
            hidKodKO.Value = strKodKO

            sBindDdlKO()
            fBindDt(strKodVot)
            sClearGvObjAm()
            sClearGvObjSbg()
            sClearGvButiran()

            fJumAll()
            fBindGvObjSebagai(strTahun, strKodKW, strKodKO, strKodPTj, strKodKP, strKodVot)

        End If
    End Sub

    Private Sub fBindDt(kodVot As String)
        Try
            Dim strSql As String = $"select KodVot, Butiran from MK_Vot where Klasifikasi = 'H1' and status = 1 and KodVot = '{kodVot}'"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)
            Dim ds_ = dbconn.fSelectCommand(strSql)
            If ds_.Tables.Count > 0 Then
                If ds_.Tables(0).Rows.Count = 1 Then
                    Dim vot = ds_.Tables(0).Rows(0)("KodVot").ToString
                    Dim butiran = ds_.Tables(0).Rows(0)("Butiran").ToString

                    txtObjAm.Text = vot + " - " + butiran

                End If
            End If

        Catch ex As Exception
            'fErrorLog("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
        End Try
    End Sub

    Private Sub sBindDdlKO()
        Try
            Dim strSql As String

            strSql = "select KodKO, (select butiran from MK_KodOperasi where MK_KodOperasi.KodKO = MK01_VotTahun.KodKO) as ButKO from MK01_VotTahun 
where MK01_VotTahun.MK01_Tahun = '" & Trim(txtTahun.Text.TrimEnd) & "' and mk01_votTahun.KodKw = '" & hidKodKW.Value.TrimEnd & "' and MK01_VotTahun .KodPTJ = '" & hidKodPTj.Value.TrimEnd & "' and left(kodvot,1) = '1'
group by KodKO"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn

            ds = dbconn.fSelectCommand(strSql)
            ddlKO.DataSource = ds
            ddlKO.DataTextField = "ButKO"
            ddlKO.DataValueField = "KodKO"
            ddlKO.DataBind()

            ddlKO.Items.Insert(0, New ListItem("- SILA PILIH -", "0"))
            ddlKO.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub



    Private Sub lnkBtnBack_Click(sender As Object, e As EventArgs) Handles lnkBtnBack.Click
        Dim KodSub = Request.QueryString("KodSub")
        Dim KodSubMenu = Request.QueryString("KodSubMenu")

        'Open other page.
        Response.Redirect($"~/FORMS/BAJET/SEMAKAN BAJET/Pengesahan_LPU.aspx?KodSub={KodSub}&KodSubMenu={KodSubMenu}", False)
    End Sub

    Public Function fGetButKW(strKodKW) As String
        Dim strButiran As String
        Try
            Dim strSql As String = "select Butiran from MK_Kw where KodKw = '" & strKodKW & "'"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strButiran = ds.Tables(0).Rows(0)("Butiran").ToString
                End If
            End If
            Return strButiran
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function

    Public Function fGetButPTj(strKodpTj) As String
        Dim strButiran As String
        Try
            Dim strSql As String = "SELECT dept_name FROM live_dept WHERE STATUS = 1 and dept_kod = '" & strKodpTj & "'"

            Dim ds As New DataSet
            Dim dbconn As New DBEQConn
            ds = dbconn.fselectCommand(strSql)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strButiran = ds.Tables(0).Rows(0)("dept_name").ToString.ToUpper
                End If
            End If
            Return strButiran
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function



    Private Function fGetAmtVotAm(strTahun, strKodKW, strKodKo, strKodPTj, strVotAm) As Decimal
        Try

            Dim strSql As String
            strSql = "select BG06_Amaun from BG06_AgihObjAm where BG06_Tahun = '" & strTahun & "' and KodKw = '" & strKodKW & "' and KodKO = '" & strKodKo & "' and KodPTJ = '" & strKodPTj & "' and KodVot = '" & strVotAm & "'"

            Dim decAmt As Decimal = 0.00
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    decAmt = CDec(IIf(IsDBNull(ds.Tables(0).Rows(0)("BG06_Amaun")), 0.00, ds.Tables(0).Rows(0))("BG06_Amaun"))
                End If
            End If

            Return decAmt
        Catch ex As Exception

        End Try
    End Function

    Private Sub sClearGvObjSbg()
        gvObjSebagai.DataSource = New List(Of String)
        gvObjSebagai.DataBind()
    End Sub

    Private Sub sClearGvObjAm()
        'gvObjAm.DataSource = New List(Of String)
        'gvObjAm.DataBind()
    End Sub

    Private Sub sClearGvButiran()
        gvButiran.DataSource = New List(Of String)
        gvButiran.DataBind()
    End Sub

    Private Sub ddlKP_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKP.SelectedIndexChanged

        'fBindGvObjSebagai(txtTahun.Text.TrimEnd, hidKodKW.Value.TrimEnd, ddlKO.SelectedValue, hidKodPTj.Value.TrimEnd, ddlKP.SelectedValue, hidObjAm.Value.TrimEnd)

    End Sub

    Private Function fBindGvObjSebagai(strTahun, strKodKW, strKodKO, strKodPTj, strKodKP, strKodVot)
        Dim strSql As String
        Dim blnFound As Boolean = True

        sClearGvObjSbg()
        Try


            strSql = $"select a.KodVotSebagai as KodVot, c.Butiran as ButVot , ISNULL(SUM(BG20_JumKetuaPTj),0) as jumlahMohon, ISNULL(SUM(BG20_JumBendahari),0) as jumlahBendahari,
        case when ISNULL(SUM(am.JumlahLPU ),0) = 0 then
        ISNULL(SUM(BG20_JumLPU),0) 
        else
        ISNULL(SUM(am.JumlahLPU ),0)
        end as jumlahNC from BG01_MohonDt_RV as a
        inner join  BG01_Mohon_RV as b on a.BG20_NoMohon = b.BG20_NoMohon
        inner join MK_Vot as c on c.kodvot = a.KodVotSebagai and c.status = 1
left join BG01_JumlahBajetSbg as am on am.BG20_TahunBajet = b.BG20_TahunBajet 
        and am.KodPtj = b.KodPtj and am.KodKW = b.KodKW  and am.KodOperasi = b.KodOperasi
        and am.KodVotAm = a.KodVotAm and   am.KodVotSbg = a.KodVotSebagai
        WHERE b.BG20_TahunBajet = '{strTahun}' and b.KodKW = '{strKodKW}' and b.KodOperasi = '{strKodKO}' and b.KodPtj = '{strKodPTj}' and a.KodVotAm = '{strKodVot}' and BG20_Aktif = '1'
        GROUP BY a.KodVotSebagai , c.Butiran Order by a.KodVotSebagai "

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim dt As New DataTable
                    dt.Columns.Add("KodVot", GetType(String))
                    dt.Columns.Add("ButVot", GetType(String))
                    dt.Columns.Add("AmtAgihPast", GetType(String))
                    dt.Columns.Add("AmtBelanjaPast", GetType(String))
                    dt.Columns.Add("AmtMohon", GetType(String))
                    dt.Columns.Add("AmtCad", GetType(String))
                    dt.Columns.Add("AmtAgih", GetType(String))

                    Dim KodSbg As String
                    Dim decJumBajet As Decimal
                    Dim Butiran As String

                    Dim strAmtAgihPast, strAmtBelanjaPast, strAmtMohon, strAmtCad

                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        KodSbg = ds.Tables(0).Rows(i)("KodVot")
                        Butiran = Context.Server.HtmlDecode(ds.Tables(0).Rows(i)("ButVot"))
                        strAmtAgihPast = 0
                        strAmtBelanjaPast = 0
                        strAmtMohon = CDec(ds.Tables(0).Rows(i)("jumlahMohon"))
                        strAmtCad = CDec(ds.Tables(0).Rows(i)("jumlahBendahari"))
                        decJumBajet = CDec(ds.Tables(0).Rows(i)("jumlahNC"))

                        dt.Rows.Add(KodSbg, Butiran, FormatNumber(strAmtAgihPast), FormatNumber(strAmtBelanjaPast), FormatNumber(strAmtMohon), FormatNumber(strAmtCad), FormatNumber(decJumBajet))

                    Next

                    ViewState("dtObjSbg") = dt
                    gvObjSebagai.DataSource = dt
                    gvObjSebagai.DataBind()

                Else
                    blnFound = False
                End If
            Else
                blnFound = False
            End If

            If blnFound = False Then
                fGlobalAlert("Carta akaun belum dibina!", Me.Page, Me.[GetType]())
            End If

        Catch ex As Exception

        End Try
    End Function



    Private Sub ddlKO_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKO.SelectedIndexChanged
        'fBindGvObjAM(Trim(txtTahun.Text.TrimEnd), Trim(hidKodKW.Value.TrimEnd), Trim(hidKodPTj.Value.TrimEnd))

    End Sub

    Private Sub sBindDdlKP()
        Try

            Dim strSql As String = "select KodKP, (select butiran from MK_KodProjek where MK_KodProjek.KodProjek = MK01_VotTahun.KodKP) as ButKP from MK01_VotTahun 
where MK01_VotTahun.MK01_Tahun = '" & Trim(txtTahun.Text.TrimEnd) & "' and mk01_votTahun.KodKw = '" & hidKodKW.Value.TrimEnd & "' and MK01_VotTahun.KodPTJ = '" & hidKodPTj.Value.TrimEnd & "' and MK01_VotTahun.KodKO = '" & ddlKO.SelectedValue & "' and left(kodvot,1) = '1'
group by KodKP"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn

            ds = dbconn.fSelectCommand(strSql)
            ddlKP.DataSource = ds
            ddlKP.DataTextField = "ButKP"
            ddlKP.DataValueField = "KodKP"
            ddlKP.DataBind()

            ddlKP.Items.Insert(0, New ListItem("- SILA PILIH -", "0"))
            ddlKP.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Dim decTotAmtBajet As Decimal

    Dim decTotAmtCad, decTotAmtAgih, decTotAmtMohon, decTotAmtBelanjaPast, decTotAmtAgihPast As Decimal
    Private Sub gvObjSebagai_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvObjSebagai.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim lblAmtAgihPast As Label = CType(e.Row.FindControl("lblAmtAgihPast"), Label)
                decTotAmtAgihPast += CDec(lblAmtAgihPast.Text)

                Dim lblAmtBelanjaPast As Label = CType(e.Row.FindControl("lblAmtBelanjaPast"), Label)
                decTotAmtBelanjaPast += CDec(lblAmtBelanjaPast.Text)

                Dim lblAmtMohon As Label = CType(e.Row.FindControl("lblAmtMohon"), Label)
                decTotAmtMohon += CDec(lblAmtMohon.Text)

                Dim lblAmtCad As Label = CType(e.Row.FindControl("lblAmtCad"), Label)
                decTotAmtCad += CDec(lblAmtCad.Text)

                Dim txtAgih As TextBox = CType(e.Row.FindControl("txtAgih"), TextBox)
                decTotAmtAgih += CDec(txtAgih.Text)

            End If

            If e.Row.RowType = DataControlRowType.Footer Then

                Dim lblTotAmtAgihPast As Label = CType(e.Row.FindControl("lblTotAmtAgihPast"), Label)
                lblTotAmtAgihPast.Text = FormatNumber(decTotAmtAgihPast)

                Dim lblTotAmtBelanjaPast As Label = CType(e.Row.FindControl("lblTotAmtBelanjaPast"), Label)
                lblTotAmtBelanjaPast.Text = FormatNumber(decTotAmtBelanjaPast)

                Dim lblTotAmtMohon As Label = CType(e.Row.FindControl("lblTotAmtMohon"), Label)
                lblTotAmtMohon.Text = FormatNumber(decTotAmtMohon)

                Dim lblTotAmtCad As Label = CType(e.Row.FindControl("lblTotAmtCad"), Label)
                lblTotAmtCad.Text = FormatNumber(decTotAmtCad)

                Dim lblTotAgih As Label = CType(e.Row.FindControl("lblTotAgih"), Label)
                lblTotAgih.Text = FormatNumber(decTotAmtAgih)

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvObjSebagai_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvObjSebagai.SelectedIndexChanged

        Try

            Dim row As GridViewRow = gvObjSebagai.SelectedRow
            Dim strKodVotSbg = CType(row.FindControl("lblKodVot"), Label).Text.ToString
            Dim strButVot = CType(row.FindControl("lblButVot"), Label).Text.ToString

            Dim strTahun = txtTahun.Text
            Dim kodkw = hidKodKW.Value
            Dim kodPtj = hidKodPTj.Value
            Dim kodKO = hidKodKO.Value


            sBindGvButiran(strKodVotSbg, strTahun, kodkw, kodKO, kodPtj)
            txtVotSbg.Text = strKodVotSbg & " - " & strButVot
            'txtKo2.Text = ddlKO.SelectedValue & " - " & ddlKO.SelectedItem.Text
            hidVotSbg.Value = strKodVotSbg
            lblHidObjSbg.Text = strKodVotSbg

            fJumAll_Sbg(strKodVotSbg)
            ddlUnit.Enabled = True

        Catch ex As Exception

        End Try

    End Sub


    Private Sub sBindGvButiran(strKodVotSbg, strTahun, kodkw, kodKO, kodPtj)

        Dim strSql As String
        Dim blnFound As Boolean = True

        Try
            sClearGvButiran()
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn

            strSql = $"select a.BG20_NoMohon, a.KodVotSebagai as KodVot, b.BG20_Program, a.BG20_Butiran ,ISNULL((BG20_JumKetuaPTj),0) as BG20_JumKetuaPTj ,ISNULL((BG20_JumBendahari),0)  as BG20_JumBendahari,ISNULL((BG20_JumNC),0) as BG20_JumNC  from BG01_MohonDt_RV as a
                    inner join  BG01_Mohon_RV as b on a.BG20_NoMohon = b.BG20_NoMohon
                    WHERE b.BG20_TahunBajet = '{strTahun}' and b.KodKW = '{kodkw}' and b.KodOperasi = '{kodKO}' and b.KodPTj = '{kodPtj}'
                    and a.KodVotSebagai = '{strKodVotSbg}' and BG20_Aktif = '1'"

            If ddlUnit.SelectedValue <> "" Then
                strSql = strSql + $" and b.KodUnitPtj = '{ddlUnit.SelectedValue}'"
            End If

            ds = dbconn.fSelectCommand(strSql)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim dt As New DataTable

                    ' Create four typed columns in the DataTable.
                    dt.Columns.Add("Program", GetType(String))
                    dt.Columns.Add("NoMohon", GetType(String))
                    dt.Columns.Add("Butiran", GetType(String))
                    dt.Columns.Add("AmtMohon", GetType(String))
                    dt.Columns.Add("AmtCad", GetType(String))
                    dt.Columns.Add("AmtAgihan", GetType(String))
                    dt.Columns.Add("Ulasan", GetType(String))



                    Dim KodSbg, NoMohon As String
                    Dim decJumBajet As Decimal
                    Dim Butiran As String
                    Dim AmtMohon As Decimal

                    Dim Program, AmtCad, AmtAgihan

                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Program = ds.Tables(0).Rows(i)("BG20_Program")
                        NoMohon = ds.Tables(0).Rows(i)("BG20_NoMohon")
                        Butiran = Context.Server.HtmlDecode(ds.Tables(0).Rows(i)("BG20_Butiran"))
                        AmtMohon = CDec(ds.Tables(0).Rows(i)("BG20_JumKetuaPTj"))
                        AmtCad = CDec(ds.Tables(0).Rows(i)("BG20_JumBendahari"))
                        AmtAgihan = CDec(ds.Tables(0).Rows(i)("BG20_JumNC")) 'fGetBajetpt(strKW, strKO, strPTj, strKP, KodSbg)

                        dt.Rows.Add(Program, NoMohon, Butiran, FormatNumber(AmtMohon), FormatNumber(AmtCad), FormatNumber(AmtAgihan))

                    Next

                    gvButiran.DataSource = dt
                    gvButiran.DataBind()

                End If
            End If


        Catch ex As Exception

        End Try



    End Sub

    Dim decTotAmtAgihBut, decTotAmtAgihMohon, decTotAmtAgihBend As Decimal
    Private Sub gvButiran_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvButiran.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim txtAmtAgihan As Label = CType(e.Row.FindControl("lblAmtAgihan"), Label)
                decTotAmtAgihBut += CDec(txtAmtAgihan.Text)

                Dim txtAmtMohon As Label = CType(e.Row.FindControl("lblAmtMohon"), Label)
                decTotAmtAgihMohon += CDec(txtAmtMohon.Text)

                Dim txtAmtBend As Label = CType(e.Row.FindControl("lblAmtCad"), Label)
                decTotAmtAgihBend += CDec(txtAmtBend.Text)

            End If

            If e.Row.RowType = DataControlRowType.Footer Then

                Dim lblTotAmtAgihan As Label = CType(e.Row.FindControl("lblTotAmtAgihan"), Label)
                lblTotAmtAgihan.Text = FormatNumber(decTotAmtAgihBut)

                Dim lblTotAmtMohon As Label = CType(e.Row.FindControl("lblTotAmtMohon"), Label)
                lblTotAmtMohon.Text = FormatNumber(decTotAmtAgihMohon)

                Dim lblTotAmtCad As Label = CType(e.Row.FindControl("lblTotAmtCad"), Label)
                lblTotAmtCad.Text = FormatNumber(decTotAmtAgihBend)


            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub gvButirant_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvButiran.RowCommand

        ' CommandName property to determine which button was clicked.
        If e.CommandName = "Select" Then

            ' Convert the row index stored in the CommandArgument
            ' property to an Integer.
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim KodSubMenu = Request.QueryString("KodSubMenu")

            ' Get the last name of the selected author from the appropriate
            ' cell in the GridView control.
            Dim selectedRow As GridViewRow = gvButiran.Rows(index)
            Dim NoMohon As String = selectedRow.Cells(5).Text

            ' Display the selected author.
            'Message.Text = "You selected " & contact & "."

            'Open other page.
            ' Response.Redirect($"~/FORMS/BAJET/SEMAKAN BAJET/MaklumatPermohonanBajet_PTJ.aspx?KodSubMenu={KodSubMenu}&no={NoMohon}")
        End If
    End Sub

    Private Sub gvButiran_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvButiran.SelectedIndexChanged
        Dim KodSub = Request.QueryString("KodSub")
        Dim KodSubMenu = Request.QueryString("KodSubMenu")
        Dim kodVot = lblHidObjAm.Text

        Dim row As GridViewRow = gvButiran.SelectedRow
        Dim strNoMohon = CType(row.FindControl("lblNoMohon"), Label).Text.ToString

        'Open other page.
        Response.Redirect($"~/FORMS/BAJET/SEMAKAN BAJET/MaklumatSemakanBajet_LPU_RV.aspx?KodSub={KodSub}&KodSubMenu={KodSubMenu}&kodSkrin=2&no={strNoMohon}&kodvot={kodVot}", False)
    End Sub

    Private Sub fBindDdlUnitPTj(kodPTj As String)
        Try
            Dim strSql As String = $"Select unit_kod,unit_name  from live_unit where Status=1 AND dept_kod='{kodPTj}' ORDER BY unit_kod ASC"

            Dim dbconn As New DBEQConn
            Dim ds As New DataSet
            ds = dbconn.fselectCommand(strSql)

            ddlUnit.DataSource = ds
            ddlUnit.DataTextField = "unit_name"
            ddlUnit.DataValueField = "unit_kod"
            ddlUnit.DataBind()

            ddlUnit.Items.Insert(0, New ListItem("-KESELURUHAN-", ""))
            ddlUnit.SelectedIndex = 0
        Catch ex As Exception
            'fErrorLog("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
        End Try
    End Sub

    Protected Sub ddlUnit_Click(sender As Object, e As EventArgs) Handles ddlUnit.SelectedIndexChanged
        fBindView()
    End Sub

    Private Sub fBindView()

        Dim strKodVotSbg = hidVotSbg.Value
        Dim strTahun = txtTahun.Text
        Dim kodkw = hidKodKW.Value
        Dim kodPtj = hidKodPTj.Value
        Dim kodKO = hidKodKO.Value

        sBindGvButiran(strKodVotSbg, strTahun, kodkw, kodKO, kodPtj)


    End Sub

    Protected Sub lnlBtnSave_Click(sender As Object, e As EventArgs) Handles lnlBtnSave.Click

        Dim dt = Date.Today.ToString("dd/MM/yyyy").ToString
        Dim dtTkhM As Date = DateTime.ParseExact(dt, "dd/MM/yyyy", CultureInfo.CurrentCulture)
        Dim tarikhMohon As String = dtTkhM.ToString("yyyy-MM-dd")
        Dim dsDt As New DataSet

        Dim jumlahAll = txtJum.Text
        Dim tahun = txtTahun.Text
        Dim kw = hidKodKW.Value
        Dim Ptj = hidKodPTj.Value
        Dim ko = hidKodKO.Value
        Dim votAm = lblHidObjAm.Text
        Dim chkExist = lblExist.Text

        If chkExist = "N" Then


            Dim strSQLStatusDok = "INSERT INTO BG01_JumlahBajetAm (BG20_TahunBajet , KodPtj,  KodKW, KodOperasi, KodVotAm,  JumlahLPU) " &
                           " VALUES (@tahun, @ptj, @kw, @ko, @vot, @jumlah)"

            Dim paramSql2() As SqlParameter =
                          {
                             New SqlParameter("@tahun", tahun),
                             New SqlParameter("@ptj", Ptj),
                             New SqlParameter("@kw", kw),
                             New SqlParameter("@ko", ko),
                             New SqlParameter("@vot", votAm),
                             New SqlParameter("@jumlah", jumlahAll)
                             }

            dbconn.fInsertCommand(strSQLStatusDok, paramSql2)
            dbconn.sConnCommitTrans()

            fGlobalAlert($"Berjaya simpan", Me.Page, Me.[GetType]())

        Else
            Dim strSQLStatusDok = "Update BG01_JumlahBajetAm set JumlahLPU = @jumlah
                where BG20_TahunBajet = @tahun and KodPtj =@ptj and  KodKW =@kw and KodOperasi =@ko and KodVotAm =@vot "

            Dim paramSql2() As SqlParameter =
                          {
                             New SqlParameter("@tahun", tahun),
                             New SqlParameter("@ptj", Ptj),
                             New SqlParameter("@kw", kw),
                             New SqlParameter("@ko", ko),
                             New SqlParameter("@vot", votAm),
                             New SqlParameter("@jumlah", jumlahAll)
                             }
            If dbconn.fUpdateCommand(strSQLStatusDok, paramSql2) > 0 Then
                ''dbconn.fUpdateCommand(strSQLStatusDok, paramSql2)
                dbconn.sConnCommitTrans()

                fGlobalAlert($"Berjaya simpan", Me.Page, Me.[GetType]())
            End If

        End If

        'Dim strDt = $"SELECT ID, BG20_TahunBajet , KodPtj,  KodKW, KodOperasi, KodVotAm, JumlahNC, JumlahLPU FROM BG01_JumlahBajet 
        'where KodPtj = '{Ptj}' and KodKW = '{kw}' and KodOperasi = '{ko}' and BG20_TahunBajet = '{tahun}' and KodVotAm = '{votAm}'"
        'dsDt = dbconn.fSelectCommand(strDt, "dsStrDt")

        'Dim drDt As DataRow
        'drDt = dsDt.Tables(0).NewRow
        'drDt("BG20_TahunBajet") = tahun
        'drDt("KodPtj") = Ptj
        'drDt("KodKW") = kw
        'drDt("KodOperasi") = ko
        'drDt("KodVotAm") = votAm
        'drDt("JumlahNC") = jumlahAll
        'drDt("JumlahLPU") = jumlahAll
        'dsDt.Tables(0).Rows.Add(drDt)
        'dbconn.sUpdateCommand(dsDt, strDt)


    End Sub

    Private Sub fJumAll()

        Dim tahun = txtTahun.Text
        Dim kw = hidKodKW.Value
        Dim Ptj = hidKodPTj.Value
        Dim ko = hidKodKO.Value
        Dim votAm = lblHidObjAm.Text

        Try
            Dim strSql As String = $"SELECT ID, BG20_TahunBajet , KodPtj,  KodKW, KodOperasi, KodVotAm, JumlahNC, JumlahLPU FROM BG01_JumlahBajetAm
            where KodPtj = '{Ptj}' and KodKW = '{kw}' and KodOperasi = '{ko}' and BG20_TahunBajet = '{tahun}' and KodVotAm = '{votAm}'"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)
            Dim ds_ = dbconn.fSelectCommand(strSql)
            If ds_.Tables.Count > 0 Then
                If ds_.Tables(0).Rows.Count = 1 Then
                    Dim jumlah = ds_.Tables(0).Rows(0)("JumlahLPU").ToString
                    txtJum.Text = jumlah

                    'exist in db
                    lblExist.Text = "Y"

                Else
                    'baca sum dari dt
                    Dim strSql2 As String = $"select ISNULL(SUM(BG20_JumLPU),0) as jumlahLPU from BG01_MohonDt_RV as a
                inner join  BG01_Mohon_RV as b on a.BG20_NoMohon = b.BG20_NoMohon
                inner join MK_Vot as c on c.kodvot = a.KodVotSebagai and c.status = 1
                WHERE b.BG20_TahunBajet = '{tahun}' and b.KodKW = '{kw}' and b.KodOperasi = '{ko}' and b.KodPtj = '{Ptj}' and a.KodVotAm = '{votAm}' and BG20_Aktif = '1' "

                    Dim ds2 As New DataSet
                    'Dim dbconn As New DBKewConn
                    ds2 = dbconn.fSelectCommand(strSql2)
                    Dim ds2_ = dbconn.fSelectCommand(strSql2)
                    If ds2_.Tables.Count > 0 Then
                        If ds2_.Tables(0).Rows.Count = 1 Then
                            Dim jumlah = ds2_.Tables(0).Rows(0)("jumlahLPU").ToString
                            txtJum.Text = jumlah

                            'exist in db
                            lblExist.Text = "N"

                        End If
                    End If


                End If


            End If


        Catch ex As Exception
            'fErrorLog("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
        End Try
    End Sub

    Private Sub fJumAll_Sbg(strKodVotSbg)

        Dim tahun = txtTahun.Text
        Dim kw = hidKodKW.Value
        Dim Ptj = hidKodPTj.Value
        Dim ko = hidKodKO.Value
        Dim votSbg = strKodVotSbg

        Try
            Dim strSql As String = $"SELECT ID, BG20_TahunBajet , KodPtj,  KodKW, KodOperasi, KodVotAm, KodVotSbg , JumlahNC, JumlahLPU FROM BG01_JumlahBajetSbg 
            where KodPtj = '{Ptj}' and KodKW = '{kw}' and KodOperasi = '{ko}' and BG20_TahunBajet = '{tahun}' and KodVotSbg = '{votSbg}'"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)
            Dim ds_ = dbconn.fSelectCommand(strSql)
            If ds_.Tables.Count > 0 Then
                If ds_.Tables(0).Rows.Count = 1 Then
                    Dim jumlah = ds_.Tables(0).Rows(0)("JumlahLPU").ToString
                    txtJumSbg.Text = jumlah

                    'exist in db
                    lblExistSbg.Text = "Y"

                Else
                    'baca sum dari dt
                    Dim strSql2 As String = $"select ISNULL((BG20_JumLPU),0) as JumlahLPU  from BG01_MohonDt_RV as a
                    inner join  BG01_Mohon_RV as b on a.BG20_NoMohon = b.BG20_NoMohon
                    WHERE b.BG20_TahunBajet = '{tahun}' and b.KodKW = '{kw}' and b.KodOperasi = '{ko}' and b.KodPTj = '{Ptj}'
                    and a.KodVotSebagai = '{strKodVotSbg}' and BG20_Aktif = '1'"

                    Dim ds2 As New DataSet
                    'Dim dbconn As New DBKewConn
                    ds2 = dbconn.fSelectCommand(strSql2)
                    Dim ds2_ = dbconn.fSelectCommand(strSql2)
                    If ds2_.Tables.Count > 0 Then
                        If ds2_.Tables(0).Rows.Count = 1 Then
                            Dim jumlah = ds2_.Tables(0).Rows(0)("JumlahLPU").ToString
                            txtJumSbg.Text = jumlah

                            'exist in db
                            lblExistSbg.Text = "N"

                        End If
                    End If


                End If


            End If


        Catch ex As Exception
            'fErrorLog("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
        End Try
    End Sub

    Protected Sub lnlBtnSaveSbg_Click(sender As Object, e As EventArgs) Handles lnlBtnSaveSbg.Click

        Dim dt = Date.Today.ToString("dd/MM/yyyy").ToString
        Dim dtTkhM As Date = DateTime.ParseExact(dt, "dd/MM/yyyy", CultureInfo.CurrentCulture)
        Dim tarikhMohon As String = dtTkhM.ToString("yyyy-MM-dd")
        Dim dsDt As New DataSet

        Dim jumlahAll = txtJumSbg.Text
        Dim tahun = txtTahun.Text
        Dim kw = hidKodKW.Value
        Dim Ptj = hidKodPTj.Value
        Dim ko = hidKodKO.Value
        Dim votAm = lblHidObjAm.Text
        Dim votSbg = lblHidObjSbg.Text
        Dim chkExist = lblExistSbg.Text

        If chkExist = "N" Then


            Dim strSQLStatusDok = "INSERT INTO BG01_JumlahBajetSbg (BG20_TahunBajet , KodPtj,  KodKW, KodOperasi, KodVotAm, KodVotSbg, JumlahLPU) " &
                           " VALUES (@tahun, @ptj, @kw, @ko, @vot, @votSbg, @jumlah)"

            Dim paramSql2() As SqlParameter =
                          {
                             New SqlParameter("@tahun", tahun),
                             New SqlParameter("@ptj", Ptj),
                             New SqlParameter("@kw", kw),
                             New SqlParameter("@ko", ko),
                             New SqlParameter("@vot", votAm),
                             New SqlParameter("@votSbg", votSbg),
                             New SqlParameter("@jumlah", jumlahAll)
                             }

            dbconn.fInsertCommand(strSQLStatusDok, paramSql2)
            dbconn.sConnCommitTrans()

            'fGlobalAlert($"Berjaya simpan", Me.Page, Me.[GetType]())

            Response.Redirect($"~/FORMS/BAJET/SEMAKAN BAJET/Maklumat_Agihan_Bajet_Am_LPU_RV.aspx?KodSub=0113&KodSubMenu=010111&KodPTj=" & Ptj & "&Tahun=" & tahun & "&KodKW=" & kw & "&kodVot=" & votAm & "&strKodKO=" & ko & "", False)


        Else
            Dim strSQLStatusDok = "Update BG01_JumlahBajetSbg set  JumlahLPU = @jumlah
                where BG20_TahunBajet = @tahun and KodPtj =@ptj and  KodKW =@kw and KodOperasi =@ko and KodVotAm =@vot and KodVotSbg =@votSbg "

            Dim paramSql2() As SqlParameter =
                          {
                             New SqlParameter("@tahun", tahun),
                             New SqlParameter("@ptj", Ptj),
                             New SqlParameter("@kw", kw),
                             New SqlParameter("@ko", ko),
                             New SqlParameter("@vot", votAm),
                             New SqlParameter("@votSbg", votSbg),
                             New SqlParameter("@jumlah", jumlahAll)
                             }
            If dbconn.fUpdateCommand(strSQLStatusDok, paramSql2) > 0 Then
                ''dbconn.fUpdateCommand(strSQLStatusDok, paramSql2)
                dbconn.sConnCommitTrans()

                'fGlobalAlert($"Berjaya simpan", Me.Page, Me.[GetType]())

                Response.Redirect($"~/FORMS/BAJET/SEMAKAN BAJET/Maklumat_Agihan_Bajet_Am_LPU_RV.aspx?KodSub=0113&KodSubMenu=010111&KodPTj=" & Ptj & "&Tahun=" & tahun & "&KodKW=" & kw & "&kodVot=" & votAm & "&strKodKO=" & ko & "", False)

            End If

        End If

        'Dim strDt = $"SELECT ID, BG20_TahunBajet , KodPtj,  KodKW, KodOperasi, KodVotAm, JumlahNC, JumlahLPU FROM BG01_JumlahBajet 
        'where KodPtj = '{Ptj}' and KodKW = '{kw}' and KodOperasi = '{ko}' and BG20_TahunBajet = '{tahun}' and KodVotAm = '{votAm}'"
        'dsDt = dbconn.fSelectCommand(strDt, "dsStrDt")

        'Dim drDt As DataRow
        'drDt = dsDt.Tables(0).NewRow
        'drDt("BG20_TahunBajet") = tahun
        'drDt("KodPtj") = Ptj
        'drDt("KodKW") = kw
        'drDt("KodOperasi") = ko
        'drDt("KodVotAm") = votAm
        'drDt("JumlahNC") = jumlahAll
        'drDt("JumlahLPU") = jumlahAll
        'dsDt.Tables(0).Rows.Add(drDt)
        'dbconn.sUpdateCommand(dsDt, strDt)


    End Sub
End Class