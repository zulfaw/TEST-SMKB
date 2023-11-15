Public Class Maklumat_Agihan_Bajet
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim strTahun = Request.QueryString("Tahun")
            Dim strKodKW = Request.QueryString("KodkW")
            Dim strKodPTj = Request.QueryString("KodPTj")

            txtTahun.Text = strTahun
            txtKW.Text = strKodKW & " - " & fGetButKW(strKodKW)
            txtPTj.Text = strKodPTj & " - " & fGetButPTj(strKodPTj)

            hidKodKW.Value = strKodKW
            hidKodPTj.Value = strKodPTj

            sBindDdlKO()
            sClearGvObjAm()
            sClearGvObjSbg()
            sClearGvButiran()
        End If
    End Sub

    Private Sub gvObjAm_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvObjAm.SelectedIndexChanged
        Try

            Dim row As GridViewRow = gvObjAm.SelectedRow
            Dim strKodVot = CType(row.FindControl("lblObjAm"), Label).Text.ToString
            Dim strButVot = CType(row.FindControl("lblButVot"), Label).Text.ToString
            txtObjAm.Text = strKodVot & " - " & strButVot

            sBindDdlKP()
            sClearGvObjSbg()
            sClearGvButiran()
            hidObjAm.Value = strKodVot

        Catch ex As Exception

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
        Response.Redirect($"~/FORMS/BAJET/PERMOHONAN BAJET/Penyediaan_Bajet_PTJ.aspx?KodSub={KodSub}&KodSubMenu={KodSubMenu}", False)
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

    Private Function fBindGvObjAM(strTahun, strKodKW, strKodPTj)
        sClearGvObjAm()

        Try
            Dim strSql As String
            Dim strKodKo = ddlKO.SelectedValue

            strSql = "select KodVot, (select mk_vot.Butiran from MK_Vot where MK_Vot .KodVot = BG_SetupObjAm .KodVot) as ButVot from BG_SetupObjAm where KodKw = '" & strKodKW & "' and Status = 1"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then

                    Dim dt As New DataTable
                    dt.Columns.Add("KodVot", GetType(String))
                    dt.Columns.Add("ButVot", GetType(String))
                    dt.Columns.Add("JumBajet", GetType(Decimal))

                    Dim strVotAm, strButVot As String
                    Dim decJumBajet As Decimal

                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        strVotAm = ds.Tables(0).Rows(i)("KodVot")
                        strButVot = ds.Tables(0).Rows(i)("ButVot")
                        decJumBajet = FormatNumber(fGetAmtVotAm(strTahun, strKodKW, strKodKo, strKodPTj, strVotAm))

                        dt.Rows.Add(strVotAm, strButVot, decJumBajet)
                    Next

                    gvObjAm.DataSource = dt
                    gvObjAm.DataBind()
                End If
            End If

        Catch ex As Exception

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
        gvObjAm.DataSource = New List(Of String)
        gvObjAm.DataBind()
    End Sub

    Private Sub sClearGvButiran()
        gvButiran.DataSource = New List(Of String)
        gvButiran.DataBind()
    End Sub

    Private Sub ddlKP_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKP.SelectedIndexChanged

        fBindGvObjSebagai(txtTahun.Text.TrimEnd, hidKodKW.Value.TrimEnd, ddlKO.SelectedValue, hidKodPTj.Value.TrimEnd, ddlKP.SelectedValue, hidObjAm.Value.TrimEnd)

    End Sub

    Private Function fBindGvObjSebagai(strTahun, strKodKW, strKodKO, strKodPTj, strKodKP, strKodVot)
        Dim strSql As String
        Dim blnFound As Boolean = True

        sClearGvObjSbg()
        Try

            strSql = "Select MK01_VotTahun.KodVot, MK_Vot.Butiran as ButVot
from MK01_VotTahun 
inner join MK_Vot on MK_Vot .KodVot = MK01_VotTahun .KodVot 
WHERE (MK_Vot.Klasifikasi = 'H2') AND (MK01_VotTahun.MK01_Status = '1') and
MK01_VotTahun.mk01_tahun = '" & strTahun & "' and MK01_VotTahun.kodkw ='" & strKodKW & "' and MK01_VotTahun.kodko = '" & strKodKO & "' and MK01_VotTahun.kodptj='" & strKodPTj & "' and MK01_VotTahun.KodKP = '" & strKodKP & "'
and left(MK_Vot.kodvot,1) = '" & strKodVot.substring(0, 1) & "' ORDER BY MK_Vot.KodVot"

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
                        strAmtAgihPast = "-"
                        strAmtBelanjaPast = "-"
                        strAmtMohon = "-"
                        strAmtCad = "-"
                        decJumBajet = 0 'fGetBajetpt(strKW, strKO, strPTj, strKP, KodSbg)

                        dt.Rows.Add(KodSbg, Butiran, strAmtAgihPast, strAmtBelanjaPast, strAmtMohon, strAmtCad, FormatNumber(decJumBajet))

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
        fBindGvObjAM(Trim(txtTahun.Text.TrimEnd), Trim(hidKodKW.Value.TrimEnd), Trim(hidKodPTj.Value.TrimEnd))

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
    Private Sub gvObjAm_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvObjAm.RowDataBound

        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim lblJumBajet As Label = CType(e.Row.FindControl("lblJumBajet"), Label)
                decTotAmtBajet += CDec(lblJumBajet.Text)

            End If

            If e.Row.RowType = DataControlRowType.Footer Then

                Dim lblTotJum As Label = CType(e.Row.FindControl("lblTotJum"), Label)
                lblTotJum.Text = FormatNumber(decTotAmtBajet)

            End If

        Catch ex As Exception

        End Try

    End Sub

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

            sBindGvButiran(strKodVotSbg)
            txtVotSbg.Text = strKodVotSbg & " - " & strButVot
            txtKo2.Text = ddlKO.SelectedValue & " - " & ddlKO.SelectedItem.Text

        Catch ex As Exception

        End Try

    End Sub


    Private Sub sBindGvButiran(strKodVotSbg)

        'Dim strSql As String
        'Dim blnFound As Boolean = True

        'sClearGvButiran()
        'Try

        '            strSql = "Select MK01_VotTahun.KodVot, MK_Vot.Butiran as ButVot
        'from MK01_VotTahun 
        'inner join MK_Vot on MK_Vot .KodVot = MK01_VotTahun .KodVot 
        'WHERE (MK_Vot.Klasifikasi = 'H2') AND (MK01_VotTahun.MK01_Status = '1') and
        'MK01_VotTahun.mk01_tahun = '" & strTahun & "' and MK01_VotTahun.kodkw ='" & strKodKW & "' and MK01_VotTahun.kodko = '" & strKodKO & "' and MK01_VotTahun.kodptj='" & strKodPTj & "' and MK01_VotTahun.KodKP = '" & strKodKP & "'
        'and left(MK_Vot.kodvot,1) = '1' ORDER BY MK_Vot.KodVot"

        '    Dim ds As New DataSet
        '    Dim dbconn As New DBKewConn
        '    ds = dbconn.fSelectCommand(strSql)

        '    If Not ds Is Nothing Then
        '        If ds.Tables(0).Rows.Count > 0 Then
        '            Dim dt As New DataTable
        '            dt.Columns.Add("KodVot", GetType(String))
        '            dt.Columns.Add("ButVot", GetType(String))
        '            dt.Columns.Add("AmtAgihPast", GetType(String))
        '            dt.Columns.Add("AmtBelanjaPast", GetType(String))
        '            dt.Columns.Add("AmtMohon", GetType(String))
        '            dt.Columns.Add("AmtCad", GetType(String))
        '            dt.Columns.Add("AmtAgih", GetType(String))

        '            Dim KodSbg As String
        '            Dim decJumBajet As Decimal
        '            Dim Butiran As String

        '            Dim strAmtAgihPast, strAmtBelanjaPast, strAmtMohon, strAmtCad

        '            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
        '                KodSbg = ds.Tables(0).Rows(i)("KodVot")
        '                Butiran = Context.Server.HtmlDecode(ds.Tables(0).Rows(i)("ButVot"))
        '                strAmtAgihPast = "-"
        '                strAmtBelanjaPast = "-"
        '                strAmtMohon = "-"
        '                strAmtCad = "-"
        '                decJumBajet = 0 'fGetBajetpt(strKW, strKO, strPTj, strKP, KodSbg)

        '                dt.Rows.Add(KodSbg, Butiran, strAmtAgihPast, strAmtBelanjaPast, strAmtMohon, strAmtCad, FormatNumber(decJumBajet))

        '            Next

        '            ViewState("dtObjSbg") = dt
        '            gvObjSebagai.DataSource = dt
        '            gvObjSebagai.DataBind()

        '        Else
        '            blnFound = False
        '        End If
        '    Else
        '        blnFound = False
        '    End If

        '    If blnFound = False Then
        '        fGlobalAlert("Carta akaun belum dibina!", Me.Page, Me.[GetType]())
        '    End If

        'Catch ex As Exception

        'End Try

        Try
            sClearGvButiran()

            Dim dt As New DataTable

            ' Create four typed columns in the DataTable.
            dt.Columns.Add("Program", GetType(String))
            dt.Columns.Add("Butiran", GetType(String))
            dt.Columns.Add("AmtMohon", GetType(String))
            dt.Columns.Add("AmtCad", GetType(String))
            dt.Columns.Add("AmtAgihan", GetType(String))
            dt.Columns.Add("Ulasan", GetType(String))

            If strKodVotSbg = "14000" Then
                dt.Rows.Add("Elaun Lebih Masa", "Pejabat Naib Canselor, Pejabat Pengurusan dan Perhubungan Canselori, Pejabat Audit Dalam", FormatNumber(30000), FormatNumber(20000), FormatNumber(25000), "")

            ElseIf strKodVotSbg = "21000" Then
                dt.Rows.Add("Mesyuarat", "kursus dan tugas rasmi NC", FormatNumber(21500), FormatNumber(15000), FormatNumber(18000), "")
                dt.Rows.Add("Mesyuarat", "kursus dan tugas rasmi PPPC", FormatNumber(21500), FormatNumber(20000), FormatNumber(20000), "")
                dt.Rows.Add("Mesyuarat", "kursus dan tugas rasmi PUU", FormatNumber(29400), FormatNumber(23000), FormatNumber(24000), "")
                dt.Rows.Add("Mesyuarat", "kursus dan tugas rasmi PAD", FormatNumber(16110), FormatNumber(10000), FormatNumber(10000), "")
                dt.Rows.Add("Pameran", "Lawatan promosi (62 lokasi)", FormatNumber(99200), FormatNumber(9000), FormatNumber(70000), "")

            ElseIf strKodVotSbg = "23000" Then
                dt.Rows.Add("Bil", "Bil Telefon", FormatNumber(9600), FormatNumber(5000), FormatNumber(7000), "")
                dt.Rows.Add("Bil", "Bil Bulanan Pemandu", FormatNumber(960), FormatNumber(500), FormatNumber(500), "")
                dt.Rows.Add("Bil", "Bil Astro PPC", FormatNumber(1626), FormatNumber(1626), FormatNumber(1626), "")

            ElseIf strKodVotSbg = "27000" Then
                dt.Rows.Add("Langganan", "surat khabar dan bahan bercetak", FormatNumber(5000), FormatNumber(5000), FormatNumber(7000), "")
                dt.Rows.Add("alat tulis", "alat tulis", FormatNumber(7000), FormatNumber(500), FormatNumber(500), "")
                dt.Rows.Add("Telefon bimbit", "Telefon bimbit", FormatNumber(3000), FormatNumber(3000), FormatNumber(3000), "")
                dt.Rows.Add("Bendera", "Bendera hari kemerdekaan", FormatNumber(5000), FormatNumber(1000), FormatNumber(1000), "")
                dt.Rows.Add("Baju", "Baju Lounge Suit Pegawai Kanan", FormatNumber(30000), FormatNumber(2000), FormatNumber(2000), "")
                dt.Rows.Add("Peralatan", "Urin Test Kit", FormatNumber(3000), FormatNumber(2000), FormatNumber(2000), "")

            End If



            gvButiran.DataSource = dt
            gvButiran.DataBind()



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
            Response.Redirect($"~/FORMS/BAJET/PERMOHONAN BAJET/MaklumatPermohonanBajet.aspx?KodSubMenu={KodSubMenu}&no={NoMohon}")
        End If
    End Sub
End Class