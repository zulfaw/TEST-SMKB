﻿Public Class Maklumat_Agihan_Bajet_Am_NC_171220202
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim strTahun = Request.QueryString("Tahun")
            Dim strKodKW = Request.QueryString("KodkW")
            Dim strKodPTj = Left(Request.QueryString("KodPTj"), 2)
            Dim strKodPTj_2 = Request.QueryString("KodPTj")
            Dim strVotAm = Request.QueryString("VotAm")
            'Dim strKO = Request.QueryString("KO")
            Dim strKodVot = Request.QueryString("kodVot")
            Dim strKodKO = Request.QueryString("strKodKO")

            Dim strkodName As String


            txtKP.Text = "DEFAULT"

            txtObjAm.Text = strKodVot & "-" & strkodName
            txtTahun.Text = strTahun
            txtKW.Text = strKodKW & " - " & fGetButKW(strKodKW)
            txtPTj.Text = strKodPTj & " - " & fGetButPTj(strKodPTj_2)
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
        Response.Redirect($"~/FORMS/BAJET/PERMOHONAN BAJET/Kelululusan_NC.aspx?KodSub={KodSub}&KodSubMenu={KodSubMenu}", False)
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
            Dim strSql As String = "select Butiran from MK_PTJ where KodPTJ = '" & strKodpTj & "'"

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


            strSql = $"select a.KodVotSebagai as KodVot, c.Butiran as ButVot , ISNULL(SUM(BG20_AngJumlah),0) as jumlahMohon from BG01_MohonDt as a
        inner join  BG01_Mohon as b on a.BG20_NoMohon = b.BG20_NoMohon
        inner join MK_Vot as c on c.kodvot = a.KodVotSebagai and c.status = 1
        WHERE b.BG20_TahunBajet = '{strTahun}' and b.KodKW = '{strKodKW}' and b.KodOperasi = '{strKodKO}' and b.KodPtj = '{strKodPTj}' and a.KodVotAm = '{strKodVot}' 
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
                        strAmtCad = 0
                        decJumBajet = 0 'fGetBajetpt(strKW, strKO, strPTj, strKP, KodSbg)

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

            strSql = $"select a.KodVotSebagai as KodVot, b.BG20_Program, a.BG20_Butiran ,BG20_AngJumlah  from BG01_MohonDt as a
                    inner join  BG01_Mohon as b on a.BG20_NoMohon = b.BG20_NoMohon
                    WHERE b.BG20_TahunBajet = '{strTahun}' and b.KodKW = '{kodkw}' and b.KodOperasi = '{kodKO}' and b.KodPtj = '{kodPtj}'
                    and a.KodVotSebagai = '{strKodVotSbg}'"

            ds = dbconn.fSelectCommand(strSql)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim dt As New DataTable

                    ' Create four typed columns in the DataTable.
                    dt.Columns.Add("Program", GetType(String))
                    dt.Columns.Add("Butiran", GetType(String))
                    dt.Columns.Add("AmtMohon", GetType(String))
                    dt.Columns.Add("AmtCad", GetType(String))
                    dt.Columns.Add("AmtAgihan", GetType(String))
                    dt.Columns.Add("Ulasan", GetType(String))



                    Dim KodSbg As String
                    Dim decJumBajet As Decimal
                    Dim Butiran As String
                    Dim AmtMohon As Decimal

                    Dim Program, AmtCad, AmtAgihan

                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Program = ds.Tables(0).Rows(i)("BG20_Program")
                        Butiran = Context.Server.HtmlDecode(ds.Tables(0).Rows(i)("BG20_Butiran"))
                        AmtMohon = CDec(ds.Tables(0).Rows(i)("BG20_AngJumlah"))
                        AmtCad = 0
                        AmtAgihan = 0 'fGetBajetpt(strKW, strKO, strPTj, strKP, KodSbg)

                        dt.Rows.Add(Program, Butiran, FormatNumber(AmtMohon), FormatNumber(AmtCad), FormatNumber(AmtAgihan))

                    Next

                    gvButiran.DataSource = dt
                    gvButiran.DataBind()

                End If
            End If


        Catch ex As Exception

        End Try



    End Sub

    Dim decTotAmtAgihBut As Decimal
    Private Sub gvButiran_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvButiran.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim txtAmtAgihan As TextBox = CType(e.Row.FindControl("txtAmtAgihan"), TextBox)
                decTotAmtAgihBut += CDec(txtAmtAgihan.Text)

            End If

            If e.Row.RowType = DataControlRowType.Footer Then

                Dim lblTotAmtAgihan As Label = CType(e.Row.FindControl("lblTotAmtAgihan"), Label)
                lblTotAmtAgihan.Text = FormatNumber(decTotAmtAgihBut)

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
            ' Response.Redirect($"~/FORMS/BAJET/PERMOHONAN BAJET/MaklumatPermohonanBajet_PTJ.aspx?KodSubMenu={KodSubMenu}&no={NoMohon}")
        End If
    End Sub

    Private Sub gvButiran_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvButiran.SelectedIndexChanged
        Dim KodSub = Request.QueryString("KodSub")
        Dim KodSubMenu = Request.QueryString("KodSubMenu")

        'Open other page.
        Response.Redirect($"~/FORMS/BAJET/PERMOHONAN BAJET/MaklumatSemakanBajet_NC.aspx?KodSub={KodSub}&KodSubMenu={KodSubMenu}", False)
    End Sub
End Class