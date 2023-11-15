Imports System.Drawing

Public Class Agihan
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Session("sesKodSubMenu") = Request.QueryString("KodSubMenu")
                fBindDdlTahunAgih()
                fBindDdlKW()
                sClearGvAgihPTj()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindDdlTahunAgih()

        ddlTahunAgih.Items.Clear()
        Dim strTahun
        For i As Integer = 0 To 1
            strTahun = Now.Year + i
            ddlTahunAgih.Items.Add(New ListItem(strTahun, strTahun))
        Next

        ddlTahunAgih.SelectedValue = Now.Year + 1

    End Sub

    Private Sub ddlTahunAgih_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTahunAgih.SelectedIndexChanged
        fBindDdlKW()
    End Sub

    Private Sub fBindDdlKW()
        Try

            Dim strSql As String = "SELECT DISTINCT TOP 100 PERCENT dbo.MK_Kw.KodKw, (dbo.MK_Kw.KodKw + ' - ' + dbo.MK_Kw.Butiran) as Butiran " &
                  "FROM dbo.MK_Kw INNER JOIN dbo.MK01_VotTahun ON dbo.MK_Kw.KodKw = dbo.MK01_VotTahun.KodKw " &
                    "where dbo.MK01_VotTahun.MK01_Tahun = '" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' ORDER BY dbo.MK_Kw.KodKw"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)
            ddlKW.DataSource = ds

            ddlKW.DataTextField = "Butiran"
            ddlKW.DataValueField = "KodKW"
            ddlKW.DataBind()

            ddlKW.Items.Insert(0, "- SILA PILIH KW -")
            ddlKW.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub

    Private Sub sClearGvAgihPTj()
        gvAgihPTj.DataSource = New List(Of String)
        gvAgihPTj.DataBind()
    End Sub

    Dim decAmt10000Op, decAmt20000Op, decAmt30000Op, decAmt40000Op As Decimal
    Dim decAmt10000Ko, decAmt20000Ko, decAmt30000Ko, decAmt40000Ko As Decimal
    Dim decJumAgih As Decimal
    Private Sub gvAgihPTj_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvAgihPTj.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.ForeColor = ColorTranslator.FromHtml("#000000")

                Dim lblAmt10000Op As Label = CType(e.Row.FindControl("lblAmt10000Op"), Label)
                decAmt10000Op += CDec(lblAmt10000Op.Text)

                Dim lblAmt10000Ko As Label = CType(e.Row.FindControl("lblAmt10000Ko"), Label)
                decAmt10000Ko += CDec(lblAmt10000Ko.Text)

                Dim lblAmt20000Op As Label = CType(e.Row.FindControl("lblAmt20000Op"), Label)
                decAmt20000Op += CDec(lblAmt20000Op.Text)

                Dim lblAmt20000Ko As Label = CType(e.Row.FindControl("lblAmt20000Ko"), Label)
                decAmt20000Ko += CDec(lblAmt20000Ko.Text)

                Dim lblAmt30000Op As Label = CType(e.Row.FindControl("lblAmt30000Op"), Label)
                decAmt30000Op += CDec(lblAmt30000Op.Text)

                Dim lblAmt30000Ko As Label = CType(e.Row.FindControl("lblAmt30000Ko"), Label)
                decAmt30000Ko += CDec(lblAmt30000Ko.Text)

                Dim lblAmt40000Op As Label = CType(e.Row.FindControl("lblAmt40000Op"), Label)
                decAmt40000Op += CDec(lblAmt40000Op.Text)

                Dim lblAmt40000Ko As Label = CType(e.Row.FindControl("lblAmt40000Ko"), Label)
                decAmt40000Ko += CDec(lblAmt40000Ko.Text)

                Dim lblJumAgih As Label = CType(e.Row.FindControl("lblJumAgih"), Label)
                decJumAgih += CDec(lblJumAgih.Text)

            End If

            If e.Row.RowType = DataControlRowType.Footer Then

                Dim lblJumAm10000Op As Label = CType(e.Row.FindControl("lblJumAm10000Op"), Label)
                lblJumAm10000Op.Text = FormatNumber(decAmt10000Op)

                Dim lblJumAm10000Ko As Label = CType(e.Row.FindControl("lblJumAm10000Ko"), Label)
                lblJumAm10000Ko.Text = FormatNumber(decAmt10000Ko)

                Dim lblJumAm20000Op As Label = CType(e.Row.FindControl("lblJumAm20000Op"), Label)
                lblJumAm20000Op.Text = FormatNumber(decAmt20000Op)

                Dim lblJumAm20000Ko As Label = CType(e.Row.FindControl("lblJumAm20000Ko"), Label)
                lblJumAm20000Ko.Text = FormatNumber(decAmt20000Ko)

                Dim lblJumAm30000Op As Label = CType(e.Row.FindControl("lblJumAm30000Op"), Label)
                lblJumAm30000Op.Text = FormatNumber(decAmt30000Op)

                Dim lblJumAm30000Ko As Label = CType(e.Row.FindControl("lblJumAm30000Ko"), Label)
                lblJumAm30000Ko.Text = FormatNumber(decAmt30000Ko)

                Dim lblJumAm40000Op As Label = CType(e.Row.FindControl("lblJumAm40000Op"), Label)
                lblJumAm40000Op.Text = FormatNumber(decAmt40000Op)

                Dim lblJumAm40000Ko As Label = CType(e.Row.FindControl("lblJumAm40000Ko"), Label)
                lblJumAm40000Ko.Text = FormatNumber(decAmt40000Ko)



                Dim lblTotJum As Label = CType(e.Row.FindControl("lblTotJum"), Label)
                lblTotJum.Text = FormatNumber(decJumAgih)
            End If

            If e.Row.RowType = DataControlRowType.Header Then
                Dim lblTitleAgihan As Label = CType(e.Row.FindControl("lblTitleAgihan"), Label)
                lblTitleAgihan.Text = "Agihan " & CInt(ddlTahunAgih.SelectedValue) & " (RM)"

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlKW_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKW.SelectedIndexChanged

        Try
            'sClearField()
            If ddlKW.SelectedIndex <> 0 Then
                fBindGvPTj()
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Function fGetJumAgihKW() As Decimal
        Try
            Dim strSql As String

            strSql = "SELECT sum(bg04_amaun) as JumAgihKW FROM BG04_AgihKw WHERE BG04_Tahun='" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' and kodagih='AL' and kodkw = '" & ddlKW.SelectedValue & "'"
            Dim decJumAgihKW As Decimal = 0.00
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    decJumAgihKW = IIf(IsDBNull(ds.Tables(0).Rows(0)("JumAgihKW")), 0.00, ds.Tables(0).Rows(0)("JumAgihKW"))
                End If
            End If

            Return decJumAgihKW

        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Function fGetJumAgihPTj() As Decimal
        Try
            Dim strSql As String

            strSql = "SELECT sum(bg05_amaun) as JumAgihPTj FROM BG05_AgihPtj WHERE BG05_Tahun='" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' and kodkw = '" & ddlKW.SelectedValue & "' and kodagih='AL'"

            Dim decJumAgihPTj As Decimal = 0.00
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    decJumAgihPTj = IIf(IsDBNull(ds.Tables(0).Rows(0)("JumAgihPTj")), 0.00, ds.Tables(0).Rows(0)("JumAgihPTj"))
                End If
            End If

            Return decJumAgihPTj
        Catch ex As Exception
            Return 0
        End Try
    End Function

    'Private Sub sClearField()

    '    txtBajetKW.Text = ""
    '    txtAgihKW.Text = ""
    '    txtBakiKW.Text = ""
    '    hidIndKW.Value = ""
    'End Sub

    Private Sub sClearGvPTj()
        gvAgihPTj.DataSource = New List(Of String)
        gvAgihPTj.DataBind()
    End Sub

    Private Function fGetIdxKW() As String
        Try
            Dim strSql As String

            strSql = "Select bg04_indkw from BG04_AgihKw  where bg04_tahun='" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' and kodkw = '" & ddlKW.SelectedValue & "' and kodagih='AL'"
            Dim strIdxKW As String
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strIdxKW = ds.Tables(0).Rows(0)("bg04_indkw")
                End If
            End If

            Return strIdxKW
        Catch ex As Exception

        End Try
    End Function

    'Private Sub fFindBajetKW()

    '    Dim decBajetKW, decJumAsal, decJumTmbh, decJumBF, decJumKrg As Decimal
    '    Try
    '        decJumAsal = fGetJumAsalKW() 'BAJET ASAL KW
    '        decJumTmbh = fGetJumTBKW() 'BAJET TAMBAHAN KW
    '        decJumKrg = fGetJumKGKW() 'BAJET KURANGAN KW
    '        decJumBF = fGetJumBFKW() 'BAJET BF KW

    '        decBajetKW = decJumAsal + decJumTmbh + decJumBF - decJumKrg
    '        'strBajetKW = decBajetKW.ToString("#,##0.00")
    '        txtBajetKW.Text = FormatNumber(decJumAsal) 'FormatNumber(decBajetKW, 2)

    '        Dim decJumAsalP, decJumTbhP, decJumKrgP, decBajetBFP, decSumPTj, decBaki As Decimal
    '        Dim strSumPTj, strBaki As String

    '        'CARI BAJET PTJ
    '        decJumAsalP = fGetJumAsalPTj() 'BAJET ASAL KW
    '        decJumTbhP = fGetJumTBPTJ() 'BAJET TAMBAHAN KW
    '        decJumKrgP = fGetJumKGPTJ() 'BAJET KURANGAN KW
    '        decBajetBFP = fGetJumBFPTJ() 'BAJET BF KW

    '        decSumPTj = decJumAsalP + decJumTbhP + decBajetBFP - decJumKrgP
    '        strSumPTj = FormatNumber(decSumPTj, 2)
    '        decBaki = decBajetKW - decSumPTj
    '        strBaki = FormatNumber(decBaki, 2)
    '        txtAgihKW.Text = FormatNumber(decJumAsalP) 'strSumPTj
    '        hidTxtAgihKW.Text = strSumPTj

    '        txtBakiKW.Text = strBaki

    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub fBindGvPTj()
        sClearGvPTj()

        Try

            Dim strSql As String
            Dim strKodKW As String = Trim(ddlKW.SelectedValue.TrimEnd)
            Dim strTahun = ddlTahunAgih.SelectedValue.TrimEnd

            sClearGvPTj()
            strSql = "select distinct MK_PTJ.KodPTJ, MK_PTJ.Butiran from MK_PTJ inner join MK01_VotTahun on MK01_VotTahun.KodPTJ = MK_PTJ.KodPTJ
where MK01_Tahun = '" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' and MK01_VotTahun.KodKw = '" & strKodKW & "' order by MK_PTJ .KodPTJ"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then

                    Dim strKodPTJ, strButiranPTj As String
                    Dim decAmt10000Op, decAmt20000Op, decAmt30000Op, decAmt40000Op As Decimal
                    Dim decAmt10000Ko, decAmt20000Ko, decAmt30000Ko, decAmt40000Ko As Decimal
                    Dim decJumAgihOp, decJumAgihKo As Decimal
                    Dim strAmtMohon

                    Dim dt As New DataTable
                    dt.Columns.Add("KodPTj", GetType(String))
                    dt.Columns.Add("ButPTj", GetType(String))
                    dt.Columns.Add("AmtMohon", GetType(String))
                    dt.Columns.Add("Amt10000Op", GetType(Decimal))
                    dt.Columns.Add("Amt10000Ko", GetType(Decimal))
                    dt.Columns.Add("Amt20000Op", GetType(Decimal))
                    dt.Columns.Add("Amt20000Ko", GetType(Decimal))
                    dt.Columns.Add("Amt30000Op", GetType(Decimal))
                    dt.Columns.Add("Amt30000Ko", GetType(Decimal))
                    dt.Columns.Add("Amt40000Op", GetType(Decimal))
                    dt.Columns.Add("Amt40000Ko", GetType(Decimal))
                    dt.Columns.Add("JumAgih", GetType(Decimal))

                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        strKodPTJ = Trim(ds.Tables(0).Rows(i)("KodPTJ")).TrimEnd
                        strButiranPTj = ds.Tables(0).Rows(i)("Butiran")
                        strAmtMohon = "-"
                        decAmt10000Op = fGetAmtVotAm(strTahun, strKodKW, "01", strKodPTJ, "10000")
                        decAmt10000Ko = fGetAmtVotAm(strTahun, strKodKW, "02", strKodPTJ, "10000")
                        decAmt20000Op = fGetAmtVotAm(strTahun, strKodKW, "01", strKodPTJ, "20000")
                        decAmt20000Ko = fGetAmtVotAm(strTahun, strKodKW, "02", strKodPTJ, "20000")
                        decAmt30000Op = fGetAmtVotAm(strTahun, strKodKW, "01", strKodPTJ, "30000")
                        decAmt30000Ko = fGetAmtVotAm(strTahun, strKodKW, "02", strKodPTJ, "30000")
                        decAmt40000Op = fGetAmtVotAm(strTahun, strKodKW, "01", strKodPTJ, "40000")
                        decAmt40000Ko = fGetAmtVotAm(strTahun, strKodKW, "02", strKodPTJ, "40000")


                        decJumAgihOp = decAmt10000Op + decAmt20000Op + decAmt30000Op + decAmt40000Op

                        decJumAgihKo = decAmt10000Ko + decAmt20000Ko + decAmt30000Ko + decAmt40000Ko

                        decJumAgih = decJumAgihOp + decJumAgihKo

                        dt.Rows.Add(strKodPTJ, strButiranPTj, strAmtMohon, decAmt10000Op, decAmt10000Ko, decAmt20000Op, decAmt20000Ko, decAmt30000Op, decAmt30000Ko, decAmt40000Op, decAmt40000Ko, decJumAgih)
                    Next

                    gvAgihPTj.DataSource = dt
                    gvAgihPTj.DataBind()
                End If
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Function fGetAmtVotAm(strTahun, strKodKW, strKO, strKodPTj, strVotAm) As Decimal
        Try

            Dim strSql As String
            'strSql = "select BG06_Amaun from BG06_AgihObjAm where BG06_Tahun = '" & strTahun & "' and KodPTJ = '" & strKodPTj & "' and KodKw = '" & strKodKW & "' and KodVot = '" & strVotAm & "'"

            strSql = "select ISNULL(sum(BG06_Amaun), 0) as BG06_Amaun from BG06_AgihObjAm where BG06_Tahun = '" & strTahun & "' and KodKw = '" & strKodKW & "' and KodKO = '" & strKO & "' and KodPTJ = '" & strKodPTj & "' and KodVot = '" & strVotAm & "'"

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

    Private Sub gvAgihPTj_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvAgihPTj.SelectedIndexChanged
        Try

            Dim row As GridViewRow = gvAgihPTj.SelectedRow
            Dim strKodPTj = CType(row.FindControl("lblKodPTJ"), Label).Text.ToString
            Dim strKodKW = Trim(ddlKW.SelectedValue.TrimEnd)
            Dim strTahun = Trim(ddlTahunAgih.SelectedValue.TrimEnd)
            Dim strKodSub = Request.QueryString("KodSub")
            Dim strKodSubMenu = Request.QueryString("KodSubMenu")

            'Open other page.
            Response.Redirect($"~/FORMS/BAJET/AGIHAN PERUNTUKAN UNIVERSITI/Maklumat_Agihan.aspx?KodSub=" & strKodSub & "&KodSubMenu=" & strKodSubMenu & "&KodPTj=" & strKodPTj & "&Tahun=" & strTahun & "&KodKW=" & strKodKW & "", False)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvAgihPTj_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvAgihPTj.RowCreated
        Try
            If e.Row.RowType = DataControlRowType.Header Then

                Dim row As GridViewRow = New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal)

                Dim cell As TableCell = New TableCell()
                cell.ColumnSpan = 3
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.BackColor = Color.White
                row.Cells.Add(cell)

                cell = New TableCell()
                cell.ColumnSpan = 2
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.Text = "VOT 10000"
                cell.Font.Bold = True
                row.Cells.Add(cell)

                cell = New TableCell()
                cell.ColumnSpan = 2
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.Text = "VOT 20000"
                cell.Font.Bold = True
                row.Cells.Add(cell)

                cell = New TableCell()
                cell.ColumnSpan = 2
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.Text = "VOT 30000"
                cell.Font.Bold = True
                row.Cells.Add(cell)

                cell = New TableCell()
                cell.ColumnSpan = 2
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.Text = "VOT 40000"
                cell.Font.Bold = True
                row.Cells.Add(cell)

                cell = New TableCell()
                cell.ColumnSpan = 4
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.BackColor = Color.White
                row.Cells.Add(cell)

                gvAgihPTj.Controls(0).Controls.AddAt(0, row)

                e.Row.BackColor = ColorTranslator.FromHtml("#FECB18")

            End If
        Catch ex As Exception

        End Try
    End Sub
End Class