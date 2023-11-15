Imports System.Drawing
Imports System.Data.SqlClient

Public Class Pengagihan_Kumpulan_Wang
    Inherits System.Web.UI.Page

    Public Shared dsAgih As New DataSet
    Public Shared dvAgih As New DataView

    Dim sLogBaru As String
    Dim sLogMedan As String
    Dim strNoStaf As String = UserInfo.strSesStaffID
    Dim sLogLevel As String = UserInfo.strUsrTahap
    Dim lsLogUsrIP As String = UserInfo.strUsrPcIP
    Dim lsLogUsrPC As String = UserInfo.strUsrPCName


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                fBindDdlTahunAgih()
                Session("sesKodSubMenu") = Request.QueryString("KodSubMenu")

                gvAgihKW.DataSource = New List(Of String)
                gvAgihKW.DataBind()

                fBindGvAgihKW()
                sClearGvObjAm()
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

    Private Function fBindGvAgihKW()

        Try
            Dim strSql As String
            Dim dt As New DataTable
            Dim dcKW = New DataColumn("KW", GetType(String))
            Dim dcBajet = New DataColumn("Bajet", GetType(String))
            Dim dcTambahan = New DataColumn("Tambahan", GetType(String))
            Dim dcKurangan = New DataColumn("Kurangan", GetType(String))
            Dim dcBakiBF = New DataColumn("BakiBF", GetType(String))
            Dim dcJumlah = New DataColumn("Jumlah", GetType(String))

            dt.Columns.Add(dcKW)
            dt.Columns.Add(dcBajet)
            dt.Columns.Add(dcTambahan)
            dt.Columns.Add(dcKurangan)
            dt.Columns.Add(dcBakiBF)
            dt.Columns.Add(dcJumlah)

            strSql = "SELECT KodKw, Butiran  FROM MK_Kw where Status = 1 order by KodKw"

            Dim ds1 As New DataSet
            Dim dbconn As New DBKewConn
            ds1 = dbconn.fselectCommand(strSql)

            If Not ds1 Is Nothing Then
                Dim kodKW, ButiranKW As String
                Dim decJumBajet As Decimal
                Dim decJumTamb As Decimal
                Dim decJumKurang As Decimal
                Dim decJumBF As Decimal
                Dim decJumlah As Decimal

                For i As Integer = 0 To ds1.Tables(0).Rows.Count - 1
                    kodKW = ds1.Tables(0).Rows(i)("KodkW").ToString
                    ButiranKW = ds1.Tables(0).Rows(i)("Butiran").ToString

                    Dim JumBajet As String
                    Dim JumTamb As String
                    Dim JumKurang As String
                    Dim JumBF As String
                    Dim Jum As String

                    decJumBajet = fGetJumBajet(kodKW)
                    JumBajet = decJumBajet.ToString("#,##0.00")
                    decJumTamb = fGetJumTamb(kodKW)
                    JumTamb = decJumTamb.ToString("#,##0.00")
                    decJumKurang = fGetJumKurang(kodKW)
                    JumKurang = decJumKurang.ToString("#,##0.00")
                    decJumBF = fGetJumBF(kodKW)
                    JumBF = decJumBF.ToString("#,##0.00")
                    decJumlah = decJumBajet + decJumTamb + decJumBF - decJumKurang
                    Jum = decJumlah.ToString("#,##0.00")

                    dt.Rows.Add(kodKW & " - " & ButiranKW, JumBajet, JumTamb, JumKurang, JumBF, Jum)
                Next
            End If

            lblJumRekod.InnerText = dt.Rows.Count
            gvAgihKW.DataSource = dt
            gvAgihKW.DataBind()
            ViewState("dvKW") = dt

        Catch ex As Exception

        End Try

    End Function


    'BAJET ASAL
    Private Function fGetJumBajet(ByVal kodKW As String) As Decimal
        Try
            Dim strsql As String = "select sum(BG04_Amaun) as JumAsal from BG04_AgihKw where BG04_Tahun ='" & ddlTahunAgih.SelectedValue.TrimEnd & "' and KodAgih ='AL' and kodKW = '" & kodKW & "'"
            Dim strbajet As Decimal = 0.00
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strsql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strbajet = IIf(IsDBNull(ds.Tables(0).Rows(0)("JumAsal")), 0.00, ds.Tables(0).Rows(0)("JumAsal"))
                End If
            End If

            Return strbajet

        Catch ex As Exception

        End Try

    End Function

    'BAJET TAMBAHAN
    Private Function fGetJumTamb(ByVal kodKW As String) As Decimal
        Try
            Dim strSql = "select sum(BG04_Amaun) as JumTambah from BG04_AgihKw where BG04_Tahun ='" & ddlTahunAgih.SelectedValue.TrimEnd & "' and kodagih='TB' and kodKW = '" & kodKW & "'"
            Dim strtambahan As Decimal = 0.00
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strtambahan = IIf(IsDBNull(ds.Tables(0).Rows(0)("JumTambah")), 0.00, ds.Tables(0).Rows(0)("JumTambah"))

                End If
            End If
            Return strtambahan
        Catch ex As Exception

        End Try
    End Function
    'BAJET KURANG
    Private Function fGetJumKurang(ByVal kodKW As String) As Decimal
        Try
            Dim strsql As String = "select sum(BG04_Amaun) as JumKurang from BG04_AgihKw where BG04_Tahun='" & ddlTahunAgih.SelectedValue.TrimEnd & "' and kodagih='KG' and kodKW = '" & kodKW & "'"
            Dim strkurangan As Decimal = 0.00
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strsql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strkurangan = IIf(IsDBNull(ds.Tables(0).Rows(0)("JumKurang")), 0.00, ds.Tables(0).Rows(0)("JumKurang"))

                End If
            End If
            Return strkurangan

        Catch ex As Exception

        End Try
    End Function
    'BAJET BF

    Private Function fGetJumBF(ByVal kodKW As String) As Decimal
        Try
            Dim strSql As String = "SELECT sum(mk09_debit) as JumBF FROM mk09_bajetbf WHERE mk09_Tahun='" & ddlTahunAgih.SelectedValue.TrimEnd & "' and kodKW = '" & kodKW & "'"

            Dim strbakiBF As Decimal = 0.00
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strbakiBF = IIf(IsDBNull(ds.Tables(0).Rows(0)("JumBF")), 0.00, ds.Tables(0).Rows(0)("JumBF"))
                End If
            End If

            Return strbakiBF
        Catch ex As Exception

        End Try
    End Function

    'Private Sub gvAgihKW_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvAgihKW.RowCreated
    '    Try
    '        If e.Row.RowType = DataControlRowType.Footer Then
    '            Dim intNoOfMergeCol As Integer = 2
    '            For intCellCol As Integer = 1 To intNoOfMergeCol - 1
    '                e.Row.Cells.RemoveAt(1)

    '            Next

    '            e.Row.Cells(0).ColumnSpan = intNoOfMergeCol
    '            e.Row.Cells(0).Text = "Jumlah Besar (RM)"
    '            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Right
    '            e.Row.Cells(0).Font.Bold = True

    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Dim decTotBajet As Decimal
    Dim decTotTambahan As Decimal
    Dim decTotKurangan As Decimal
    Dim decTotBakiBF As Decimal
    Dim decTotJumlah As Decimal

    Dim decTotBajet2 As Decimal
    Dim decTotJumlah2 As Decimal
    Private Sub gvAgihKW_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvAgihKW.RowDataBound
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim decJumBajet As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Bajet").ToString())
                Dim decJumTambahan As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Tambahan").ToString())
                Dim decJumKurangan As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Kurangan").ToString())
                Dim decBakiBF As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BakiBF").ToString())
                Dim decBaki As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Jumlah").ToString())

                decTotBajet += decJumBajet
                decTotTambahan += decJumTambahan
                decTotKurangan += decJumKurangan
                decTotBakiBF += decBakiBF
                decTotJumlah += decBaki

                'Dim strTotBajet As String = decTotBajet.ToString("#,##0.00")
                'ViewState("strTotBajet") = strTotBajet
                'Dim strTotTambahan As String = decTotTambahan.ToString("#,##0.00")
                'ViewState("strTotTambahan") = strTotTambahan
                'Dim strTotKurangan As String = decTotKurangan.ToString("#,##0.00")
                'ViewState("strTotKurangan") = strTotKurangan
                'Dim strTotBakiBF As String = decTotBakiBF.ToString("#,##0.00")
                'ViewState("strTotBakiBF") = strTotBakiBF
                'Dim strTotJumlah As String = decTotJumlah.ToString("#,##0.00")
                'ViewState("strTotJumlah") = strTotJumlah

                Dim strKW As String = Trim(DataBinder.Eval(e.Row.DataItem, "KW").ToString().TrimEnd)
                Dim strKodKW As String = strKW.Substring(0, 2)

                If strKodKW = "01" OrElse strKodKW = "07" Then
                    e.Row.FindControl("lbtnSelect").Visible = True
                    e.Row.FindControl("lbtnBajetPast").Visible = True
                End If

            ElseIf e.Row.RowType = DataControlRowType.Footer Then

                Dim lblJumBajet As Label = CType(e.Row.FindControl("lblJumBajet"), Label)
                lblJumBajet.Text = FormatNumber(decTotBajet)

                Dim lblJumTamb As Label = CType(e.Row.FindControl("lblJumTamb"), Label)
                lblJumTamb.Text = FormatNumber(decTotTambahan)

                Dim lblJumKur As Label = CType(e.Row.FindControl("lblJumKur"), Label)
                lblJumKur.Text = FormatNumber(decTotKurangan)

                Dim lblJumBakiBF As Label = CType(e.Row.FindControl("lblJumBakiBF"), Label)
                lblJumBakiBF.Text = FormatNumber(decTotBakiBF)

                Dim lblTotJum As Label = CType(e.Row.FindControl("lblTotJum"), Label)
                lblTotJum.Text = FormatNumber(decTotJumlah)

            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub sClearGvObjAm()
        gvObjAm.DataSource = New List(Of String)
        gvObjAm.DataBind()
    End Sub

    Private Sub sClearGvObjAmPast()
        gvObjAmPast.DataSource = New List(Of String)
        gvObjAmPast.DataBind()
    End Sub

    Private Sub fBindGVObjAm(ByVal strKW As String)
        Try

            Dim dt As New DataTable
            dt = fCreateDtObjAm(strKW)

            If dt Is Nothing Then
                sClearGvObjAm()
                fGlobalAlert("Tiada Carta Akaun untuk Kumpulan Wang ini!", Me.Page, Me.[GetType]())
            Else
                gvObjAm.DataSource = dt
                gvObjAm.DataBind()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Function fCreateDtObjAm(ByVal strKW As String)
        Dim dt As New DataTable
        Try
            Dim dbconn As New DBKewConn
            Dim strSql As String

            Dim ds As New DataSet
            Dim strTahun = ddlTahunAgih.SelectedValue.TrimEnd

            dt.Columns.AddRange(New DataColumn() {
                New DataColumn("ObjAm", GetType(String)),
                New DataColumn("JumBajetPast", GetType(String)),
                New DataColumn("JumBajetUsed", GetType(String)),
                New DataColumn("JumBajet", GetType(String))
                                })

            strSql = "select distinct a.KodVot,(select butiran from MK_Vot b where b.KodVot = a.KodVot) as Butiran from MK01_VotTahun a where a.MK01_Tahun = '" & strTahun & "' and a.KodKw = '" & Trim(strKW.TrimEnd) & "' and RIGHT (a.kodvot,4) = '0000'
and a.kodvot in (select kodvot from bg_setupobjam where KodKw = '" & Trim(strKW.TrimEnd) & "' and Status = 1)
order by a.KodVot "

            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                        Dim strObjAm, strButObjAm As String
                        Dim strJumBajet, strJumBajetPast, strJumBajetUsed As String

                        strObjAm = ds.Tables(0).Rows(i)("kodvot").ToString
                        strButObjAm = ds.Tables(0).Rows(i)("Butiran").ToString
                        strJumBajetPast = fGetJumBajet(strKW, strObjAm, CInt(strTahun) - 1)
                        strJumBajetUsed = fGetBelanja(CInt(strTahun) - 1, strKW, strObjAm)
                        strJumBajet = fGetJumBajet(strKW, strObjAm, strTahun)

                        dt.Rows.Add(strObjAm & " - " & strButObjAm, FormatNumber(strJumBajetPast), FormatNumber(strJumBajetUsed), FormatNumber(strJumBajet))
                    Next
                Else
                    dt = Nothing
                End If
            Else
                dt = Nothing
            End If

            Return dt

        Catch ex As Exception
            dt = Nothing
        End Try

    End Function

    Private Function fGetJumBajet(strKW, strObjAm, strTahun) As Decimal
        Try
            Dim strJumObjAm As String
            Dim strsql As String = "select sum(bg04_amaun) as JumObjAm from BG04_AgihKwDt " &
            "where BG04_Tahun ='" & strTahun & "' and KodKw ='" & strKW & "' and KodVot ='" & strObjAm & "' and KodAgih ='AL'"
            Dim strbajet As Decimal = 0.00
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strsql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strJumObjAm = IIf(IsDBNull(ds.Tables(0).Rows(0)("JumObjAm")), 0.00, ds.Tables(0).Rows(0)("JumObjAm"))
                End If
            End If

            Return strJumObjAm

        Catch ex As Exception

        End Try

    End Function

    Dim TotBajet, TotBajetPast, TotBajetUsed As Decimal
    Private Sub gvObjAm_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvObjAm.RowDataBound
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim lblJumBajetPast As Label = CType(e.Row.FindControl("lblJumBajetPast"), Label)
                TotBajetPast += CDec(lblJumBajetPast.Text)

                Dim lblJumBajetUsed As Label = CType(e.Row.FindControl("lblJumBajetUsed"), Label)
                TotBajetUsed += CDec(lblJumBajetUsed.Text)

                Dim txtBajet As TextBox = CType(e.Row.FindControl("txtBajet"), TextBox)
                TotBajet += CDec(txtBajet.Text)

            End If

            If e.Row.RowType = DataControlRowType.Footer Then

                Dim lblTotBajetPast As Label = CType(e.Row.FindControl("lblTotBajetPast"), Label)
                lblTotBajetPast.Text = FormatNumber(TotBajetPast)

                Dim lblTotBajetUsed As Label = CType(e.Row.FindControl("lblTotBajetUsed"), Label)
                lblTotBajetUsed.Text = FormatNumber(TotBajetUsed)

                Dim lblTotBajet As Label = CType(e.Row.FindControl("lblTotBajet"), Label)
                lblTotBajet.Text = FormatNumber(TotBajet)

            End If

            If e.Row.RowType = DataControlRowType.Header Then
                Dim lblBajetPast As Label = CType(e.Row.FindControl("lblBajetPast"), Label)
                lblBajetPast.Text = "Agihan " & CInt(ddlTahunAgih.SelectedValue) - 1 & " (RM)"

                Dim lblJumGuna As Label = CType(e.Row.FindControl("lblJumGuna"), Label)
                lblJumGuna.Text = "Belanja " & CInt(ddlTahunAgih.SelectedValue) - 1 & " (RM)"

                Dim lblBajetCur As Label = CType(e.Row.FindControl("lblBajetCur"), Label)
                lblBajetCur.Text = "Agihan " & CInt(ddlTahunAgih.SelectedValue) & " (RM)"

            End If


        Catch ex As Exception

        End Try

    End Sub

    Private Function fSimpan() As Boolean
        Dim dbconn As New DBKewConn
        Dim ds As New DataSet
        Dim strSql As String
        Dim blnSuccess As Boolean = True
        Dim strAmt As String
        Dim decAmt As Decimal
        Dim strVotAm As String
        Dim TotAmt As Decimal
        Dim strIndKwDt As String
        dbconn.sConnBeginTrans()
        Dim strTahun = ddlTahunAgih.SelectedValue.TrimEnd
        Try

            Dim strKodKW As String = Trim(txtKodKW.Text.TrimEnd)

            Dim IntIndex As Integer = CInt(strKodKW)
            strIndKwDt = strTahun + "K" + Format((IntIndex), "00000")
            Dim strKodAgih = "AL"

            For Each row As GridViewRow In gvObjAm.Rows

                strAmt = TryCast(row.FindControl("txtBajet"), TextBox).Text
                If strAmt = "" Then
                    strAmt = "0"
                End If
                decAmt = CDec(strAmt)

                strVotAm = TryCast(row.FindControl("ObjAm"), Label).Text.ToString.Substring(0, 5)

                strSql = "select count(*)  from BG04_AgihKwdt WITH (NOLOCK) where KodKw='" & strKodKW & "' and BG04_Tahun ='" & strTahun & "' and KodVot='" & strVotAm & "'"

                If fCheckRec(strSql) > 0 Then
                    strSql = "update BG04_AgihKwDt set BG04_Amaun = @Amaun where BG04_IndKwDt = @IndKwDt and BG04_Tahun = @Tahun and KodKw = @KodKW and kodvot = @KodVot and KodAgih = @kodAgih"

                    Dim paramSql() As SqlParameter = {
                New SqlParameter("@IndKwDt", strIndKwDt),
                New SqlParameter("@KodVot", strVotAm),
                New SqlParameter("@Amaun", decAmt),
                New SqlParameter("@Tahun", strTahun),
                New SqlParameter("@KodKW", strKodKW),
                New SqlParameter("@KodAgih", strKodAgih)
                }

                    If Not dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                        blnSuccess = False
                        Exit Try
                    Else
                        'AUDIT LOG
                        sLogMedan = "BG04_Amaun|BG04_IndKwDt|BG04_Tahun|KodKw|kodvot|KodAgih"
                        sLogBaru = decAmt & "|" & strIndKwDt & "|" & strTahun & "|" & strKodKW & "|" & strVotAm & "|" & strKodAgih

                        strSql = "insert into MK_AuditLog (UserID, UserGroup, UserUbah, SubMenu, Keterangan, InfoTable, RefKey, RefNo, InfoMedan, InfoBaru, " _
            & " InfoLama, UserIP, PCName) " _
            & " values (@UserID,@UserGroup,@UserUbah,@SubMenu,@Keterangan,@InfoTable,@RefKey, @RefNo,@InfoMedan,@InfoBaru,@InfoLama, " _
            & " @UserIP,@PCName)"

                        Dim paramSqlLog() As SqlParameter = {
                            New SqlParameter("@UserID", strNoStaf),
                            New SqlParameter("@UserGroup", sLogLevel),
                            New SqlParameter("@UserUbah", "-"),
                             New SqlParameter("@SubMenu", Session("sesKodSubMenu")),
                            New SqlParameter("@Keterangan", "UPDATE"),
                            New SqlParameter("@InfoTable", "BG04_AgihKwDt"),
                            New SqlParameter("@RefKey", "-"),
                            New SqlParameter("@RefNo", "-"),
                            New SqlParameter("@InfoMedan", sLogMedan),
                            New SqlParameter("@InfoBaru", sLogBaru),
                            New SqlParameter("@InfoLama", "-"),
                            New SqlParameter("@UserIP", lsLogUsrIP),
                            New SqlParameter("@PCName", lsLogUsrPC)
                        }

                        If Not dbconn.fInsertCommand(strSql, paramSqlLog) > 0 Then
                            blnSuccess = False
                            Exit Try
                        End If
                    End If
                Else
                    strSql = "insert into BG04_AgihKwDt (BG04_IndKwDt,KodVot,BG04_Amaun,BG04_Tahun,KodKw,KodAgih) values (@IndKwDt,@KodVot,@Amaun,@Tahun,@KodKW,@KodAgih)"

                    Dim paramSql() As SqlParameter = {
                New SqlParameter("@IndKwDt", strIndKwDt),
                New SqlParameter("@KodVot", strVotAm),
                New SqlParameter("@Amaun", decAmt),
                New SqlParameter("@Tahun", strTahun),
                New SqlParameter("@KodKW", strKodKW),
                New SqlParameter("@KodAgih", strKodAgih)
                }

                    If Not dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                        blnSuccess = False
                        Exit Try
                    Else
                        'AUDIT LOG
                        sLogMedan = "BG04_IndKwDt|KodVot|BG04_Amaun|BG04_Tahun|KodKw|KodAgih"
                        sLogBaru = strIndKwDt & "|" & strVotAm & "|" & decAmt & "|" & strTahun & "|" & strKodKW & "|" & strKodKW & "|" & strKodAgih

                        strSql = "insert into MK_AuditLog (UserID, UserGroup, UserUbah, SubMenu, Keterangan, InfoTable, RefKey, RefNo, InfoMedan, InfoBaru, " _
            & " InfoLama, UserIP, PCName) " _
            & " values (@UserID,@UserGroup,@UserUbah,@SubMenu,@Keterangan,@InfoTable,@RefKey, @RefNo,@InfoMedan,@InfoBaru,@InfoLama, " _
            & " @UserIP,@PCName)"

                        Dim paramSqlLog() As SqlParameter = {
                            New SqlParameter("@UserID", strNoStaf),
                            New SqlParameter("@UserGroup", sLogLevel),
                            New SqlParameter("@UserUbah", "-"),
                             New SqlParameter("@SubMenu", Session("sesKodSubMenu")),
                            New SqlParameter("@Keterangan", "INSERT"),
                            New SqlParameter("@InfoTable", "BG04_AgihKwDt"),
                            New SqlParameter("@RefKey", "-"),
                            New SqlParameter("@RefNo", "-"),
                            New SqlParameter("@InfoMedan", sLogMedan),
                            New SqlParameter("@InfoBaru", sLogBaru),
                            New SqlParameter("@InfoLama", "-"),
                            New SqlParameter("@UserIP", lsLogUsrIP),
                            New SqlParameter("@PCName", lsLogUsrPC)
                        }

                        If Not dbconn.fInsertCommand(strSql, paramSqlLog) > 0 Then
                            blnSuccess = False
                            Exit Try
                        End If
                    End If
                End If
                TotAmt += strAmt
            Next

            strSql = "select count(*) from BG04_AgihKw WITH (NOLOCK) where BG04_Tahun ='" & strTahun & "' and KodAgih ='AL' and KodKw ='" & strKodKW & "'"

            If fCheckRec(strSql) > 0 Then

                strSql = "update BG04_AgihKw set BG04_Amaun = @Amaun where BG04_IndKw = @IndKw"

                Dim paramSql() As SqlParameter = {
                New SqlParameter("@IndKw", strIndKwDt),
                New SqlParameter("@Amaun", TotAmt)
                }

                If Not dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                    blnSuccess = False
                    Exit Try
                Else
                    'AUDIT LOG
                    sLogMedan = "BG04_Amaun|BG04_IndKw"
                    sLogBaru = TotAmt & "|" & strIndKwDt

                    strSql = "insert into MK_AuditLog (UserID, UserGroup, UserUbah, SubMenu, Keterangan, InfoTable, RefKey, RefNo, InfoMedan, InfoBaru, " _
        & " InfoLama, UserIP, PCName) " _
        & " values (@UserID,@UserGroup,@UserUbah,@SubMenu,@Keterangan,@InfoTable,@RefKey, @RefNo,@InfoMedan,@InfoBaru,@InfoLama, " _
        & " @UserIP,@PCName)"

                    Dim paramSqlLog() As SqlParameter = {
                        New SqlParameter("@UserID", strNoStaf),
                        New SqlParameter("@UserGroup", sLogLevel),
                        New SqlParameter("@UserUbah", "-"),
                         New SqlParameter("@SubMenu", Session("sesKodSubMenu")),
                        New SqlParameter("@Keterangan", "UPDATE"),
                        New SqlParameter("@InfoTable", "BG04_AgihKw"),
                        New SqlParameter("@RefKey", "-"),
                        New SqlParameter("@RefNo", "-"),
                        New SqlParameter("@InfoMedan", sLogMedan),
                        New SqlParameter("@InfoBaru", sLogBaru),
                        New SqlParameter("@InfoLama", "-"),
                        New SqlParameter("@UserIP", lsLogUsrIP),
                        New SqlParameter("@PCName", lsLogUsrPC)
                    }

                    If Not dbconn.fInsertCommand(strSql, paramSqlLog) > 0 Then
                        blnSuccess = False
                        Exit Try
                    End If
                End If

            Else
                Dim TkhKodAgih As String = Now.ToString("yyyy-MM-dd")
                Dim strButiran = "-"
                'INSERT
                strSql = "insert into BG04_AgihKw (BG04_IndKw,BG04_Tahun,KodKw,BG04_Amaun,KodAgih,BG04_TkhAgih,BG04_Butiran,BG04_Status) 
values (@IndKw,@Tahun,@KodKW,@Amaun,@KodAgih,@TkhKodAgih,@Butiran,@Status)"

                Dim paramSql() As SqlParameter = {
                New SqlParameter("@IndKw", strIndKwDt),
                New SqlParameter("@Tahun", strTahun),
                New SqlParameter("@KodKW", strKodKW),
                New SqlParameter("@Amaun", TotAmt),
                New SqlParameter("@KodAgih", strKodAgih),
                New SqlParameter("@TkhKodAgih", TkhKodAgih),
                New SqlParameter("@Butiran", strButiran),
                New SqlParameter("@Status", 1)
              }

                If Not dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                    blnSuccess = False
                    Exit Try
                Else
                    'AUDIT LOG
                    sLogMedan = "BG04_IndKw|BG04_Tahun|KodKw|BG04_Amaun|KodAgih|BG04_TkhAgih|BG04_Butiran|BG04_Status"
                    sLogBaru = strIndKwDt & "|" & strTahun & "|" & strKodKW & "|" & TotAmt & "|" & strKodAgih & "|" & TkhKodAgih & "|" & strButiran & "|1"

                    strSql = "insert into MK_AuditLog (UserID, UserGroup, UserUbah, SubMenu, Keterangan, InfoTable, RefKey, RefNo, InfoMedan, InfoBaru, " _
        & " InfoLama, UserIP, PCName) " _
        & " values (@UserID,@UserGroup,@UserUbah,@SubMenu,@Keterangan,@InfoTable,@RefKey, @RefNo,@InfoMedan,@InfoBaru,@InfoLama, " _
        & " @UserIP,@PCName)"

                    Dim paramSqlLog() As SqlParameter = {
                        New SqlParameter("@UserID", strNoStaf),
                        New SqlParameter("@UserGroup", sLogLevel),
                        New SqlParameter("@UserUbah", "-"),
                         New SqlParameter("@SubMenu", Session("sesKodSubMenu")),
                        New SqlParameter("@Keterangan", "INSERT"),
                        New SqlParameter("@InfoTable", "BG04_AgihKw"),
                        New SqlParameter("@RefKey", "-"),
                        New SqlParameter("@RefNo", "-"),
                        New SqlParameter("@InfoMedan", sLogMedan),
                        New SqlParameter("@InfoBaru", sLogBaru),
                        New SqlParameter("@InfoLama", "-"),
                        New SqlParameter("@UserIP", lsLogUsrIP),
                        New SqlParameter("@PCName", lsLogUsrPC)
                    }

                    If Not dbconn.fInsertCommand(strSql, paramSqlLog) > 0 Then
                        blnSuccess = False
                        Exit Try
                    End If
                End If
            End If

            Dim decBajet As Decimal = CDec(hidBajet.Value)
            Dim decJumBajet As Decimal = CDec(hidJumBajet.Value)

            Dim decJumAsal As Decimal = CDec(hidBajAsal.Value) - decBajet + TotAmt
            Dim decJumTamb As Decimal = CDec(hidBajTamb.Value)
            Dim decJumKur As Decimal = CDec(hidBajKur.Value)
            Dim decJumBesar As Decimal = CDec(hidBajJumBesar.Value) - decJumBajet + TotAmt

            hidBajet.Value = decBajet
            hidJumBajet.Value = decJumBajet

            strSql = "select count (*) from BG03_BjtUniv With (Nolock)  where bg03_tahun = '" & strTahun & "'"

            If fCheckRec(strSql) > 0 Then

                strSql = "update BG03_BjtUniv set BG03_Asal = @BG03_Asal, BG03_JumBesar = @BG03_JumBesar where BG03_tahun = @BG03_tahun"

                Dim paramSql() As SqlParameter = {
                New SqlParameter("@BG03_Asal", decJumAsal),
                New SqlParameter("@BG03_JumBesar", decJumBesar),
                New SqlParameter("@BG03_tahun", strTahun)
                }

                If Not dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                    blnSuccess = False
                    Exit Try
                End If

            Else
                strSql = "insert into BG03_BjtUniv (BG03_Tahun, BG03_Asal, BG03_Tambahan, BG03_Kurangan, BG03_JumBesar) values (@BG03_Tahun, @BG03_Asal, @BG03_Tambahan, @BG03_Kurangan, @BG03_JumBesar)"

                Dim paramSql() As SqlParameter = {
                New SqlParameter("@BG03_Tahun", strTahun),
                New SqlParameter("@BG03_Asal", decJumAsal),
                New SqlParameter("@BG03_Tambahan", decJumTamb),
                New SqlParameter("@BG03_Kurangan", decJumKur),
                New SqlParameter("@BG03_JumBesar", decJumBesar)
              }

                If Not dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                    blnSuccess = False
                    Exit Try
                End If
            End If

        Catch ex As Exception
            blnSuccess = False
            fErrorLog(ex.Message.ToString)

        End Try

        If blnSuccess Then
            dbconn.sConnCommitTrans()
            Return True
        Else
            dbconn.sConnRollbackTrans()
            Return False
        End If

    End Function

    Private Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.Click
        If fSimpan() Then
            fGlobalAlert("Maklumat agihan telah disimpan!", Me.Page, Me.[GetType]())
            fBindGvAgihKW()
            sClearGvObjAm()
            txtKodKW.Text = ""
            txtKW.Text = ""
            lbtnSimpan.Visible = False
        Else
            fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
        End If

    End Sub

    Private Sub gvAgihKW_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvAgihKW.RowCommand

        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim selectedRow As GridViewRow = gvAgihKW.Rows(index)
        Dim strKW As String = CType(selectedRow.FindControl("lblKW"), Label).Text.TrimEnd
        Dim strKodKW = strKW.Substring(0, 2)
        Dim strButKW = strKW.Substring(5, strKW.Length - 5)

        If e.CommandName = "Select" Then

            Try
                txtKodKW.Text = strKodKW
                txtKW.Text = strButKW

                fBindGVObjAm(strKodKW)

                Dim footerRow = gvAgihKW.FooterRow
                Dim decJumAsal As Decimal = CDec(CType(footerRow.FindControl("lblJumBajet"), Label).Text)
                Dim decJumTamb As Decimal = CDec(CType(footerRow.FindControl("lblJumTamb"), Label).Text)
                Dim decJumKur As Decimal = CDec(CType(footerRow.FindControl("lblJumKur"), Label).Text)
                Dim decJumBesar As Decimal = CDec(CType(footerRow.FindControl("lblTotJum"), Label).Text)
                Dim decJumBajet As Decimal = CDec(CType(footerRow.FindControl("lblJumBajet"), Label).Text)

                Dim decBajet As Decimal = CDec(CType(selectedRow.FindControl("lblBajet"), Label).Text.TrimEnd)

                hidBajAsal.Value = decJumAsal
                hidBajTamb.Value = decJumTamb
                hidBajKur.Value = decJumKur
                hidBajJumBesar.Value = decJumBesar
                hidBajet.Value = decBajet
                hidJumBajet.Value = decJumBajet

                lbtnSimpan.Visible = True

            Catch ex As Exception

            End Try

        ElseIf e.CommandName = "ShowPast" Then
            txtKodKWPast.Text = strKodKW
            txtKWPast.Text = strButKW

            sLoadTahun()
            fBindGVObjAmPast(strKodKW)
            modPopBajetPast.Show()

        End If

    End Sub

    Private Sub sLoadTahun()
        ddlTahun.Items.Clear()

        Dim ds As New DataSet
        ds = fPopTahun(7)
        ddlTahun.DataSource = ds
        ddlTahun.DataTextField = "Tahun"
        ddlTahun.DataValueField = "Val"
        ddlTahun.DataBind()

        ddlTahun.SelectedValue = Now.Year - 1
    End Sub

    Private Sub ddlTahun_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTahun.SelectedIndexChanged

        fBindGVObjAmPast(txtKodKWPast.Text)
        modPopBajetPast.Show()

    End Sub

    Protected Sub txtBajet_TextChanged(sender As Object, e As EventArgs)

        Try

            Dim txtBajet As TextBox = CType(sender, TextBox)
            Dim gvr As GridViewRow = CType(txtBajet.Parent.Parent, GridViewRow)
            Dim rowindex As Integer = gvr.RowIndex
            Dim selectedRow As GridViewRow = gvObjAm.Rows(gvr.RowIndex)

            If txtBajet.Text = "" Then txtBajet.Text = "0.00"
            Dim decBajet As Decimal = CDec(txtBajet.Text)
            txtBajet.Text = FormatNumber(decBajet)

            'Kira Jumlah Besar
            Dim decBajetPTj, decJumBajetPTj As Decimal

            For Each gvRow As GridViewRow In gvObjAm.Rows
                decBajetPTj = Trim(CType(gvRow.FindControl("txtBajet"), TextBox).Text.TrimEnd)
                decJumBajetPTj += decBajetPTj 'Total Jumlah Bajet
            Next

            Dim footerRow = gvObjAm.FooterRow
            CType(footerRow.FindControl("lblTotBajet"), Label).Text = FormatNumber(decJumBajetPTj) 'Total Jumlah Besar Bajet

        Catch ex As Exception

        End Try

    End Sub

    Private Sub fBindGVObjAmPast(ByVal strKW As String)

        sClearGvObjAmPast()

        Dim strTahun = ddlTahun.SelectedValue

        Try
            Dim dbconn As New DBKewConn
            Dim strSql As String

            Dim ds As New DataSet
            Dim dt As New DataTable

            dt.Columns.AddRange(New DataColumn() {
            New DataColumn("ObjAm", GetType(String)),
            New DataColumn("JumBajet", GetType(Decimal))
                            })

            strSql = "select distinct a.KodVot,(select butiran from MK_Vot b where b.KodVot = a.KodVot) as Butiran from MK01_VotTahun a where a.MK01_Tahun = '" & strTahun & "' and a.KodKw = '" & Trim(strKW.TrimEnd) & "' and RIGHT (a.kodvot,4) = '0000'
and a.kodvot in (select kodvot from bg_setupobjam where KodKw = '" & Trim(strKW.TrimEnd) & "' and Status = 1)
order by a.KodVot "

            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                        Dim strObjAm As String
                        Dim decJumBajet As Decimal

                        strObjAm = ds.Tables(0).Rows(i)("kodvot").ToString & " - " & ds.Tables(0).Rows(i)("Butiran").ToString
                        decJumBajet = fGetJumBajet(strKW, strObjAm, strTahun)

                        dt.Rows.Add(strObjAm, decJumBajet)
                    Next

                    gvObjAmPast.DataSource = dt
                    gvObjAmPast.DataBind()

                End If
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub ddlTahunAgih_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTahunAgih.SelectedIndexChanged
        fBindGvAgihKW()
        sClearGvObjAm()
    End Sub

    Private Function fGetBelanja(strTahun, strKW, KodVotAm) As Decimal

        Dim dbconn As New DBKewConn
        Dim decBelanjaSbnr, decJrnlDt, decJrnlKt, decJrnlLejar, decJumBil As Decimal
        Dim ds As New DataSet
        Dim strSql
        Try

            strSql = " Select  isnull(sum(mk06_debit) ,0) as JumDebit, isnull(sum(mk06_kredit),0) as JumKredit
 From MK06_TRANSAKSI Where
 KODkw = '" & strKW & "' And year(mk06_tkhtran) = '" & strTahun & "'
 And substring(kodvot,1,1) + '0000' = '" & KodVotAm & "'
 And substring(mk06_rujukan,1,2) in ('RT','BK','RS','JP','BP');"

            ds = dbconn.fSelectCommand(strSql)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    decJrnlDt = ds.Tables(0).Rows(0)("JumDebit")
                    decJrnlKt = ds.Tables(0).Rows(0)("JumKredit")
                End If
            End If

            strSql = "Select isnull(sum(mk06_debit),0) - isnull(sum(mk06_kredit),0) as JumJrnlLejar
From mk06_transaksi d, gl01_jrnltran
Where d.mk06_rujukan = gl01_jrnltran.gl01_nojrnl
And d.kodkw = '" & strKW & "' And year(d.mk06_tkhtran) = '" & strTahun & "'
And substring(kodvot,1,1) + '0000' = '" & KodVotAm & "'    
And substring(d.mk06_rujukan,1,2) in ('JK')            
And (gl01_statuslejar Is null Or gl01_statuslejar=0)"

            ds = dbconn.fSelectCommand(strSql)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    decJrnlLejar = ds.Tables(0).Rows(0)("JumJrnlLejar")
                End If
            End If

            strSql = " Select isnull(sum(mk06_debit),0) - isnull(sum(mk06_kredit),0) as JumBil
 From mk06_transaksi Where Year(mk06_tkhtran) = '" & strTahun & "' 
 And kodkw = '" & strKW & "' And substring(kodvot,1,1) + '0000' = '" & KodVotAm & "' and koddok in ('BIL','ADJ_BIL')"

            ds = dbconn.fSelectCommand(strSql)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    decJumBil = ds.Tables(0).Rows(0)("JumBil")
                End If
            End If

            decBelanjaSbnr = (decJrnlDt - decJrnlKt) + decJrnlLejar + decJumBil
            Return decBelanjaSbnr

        Catch ex As Exception
            Return 0
        End Try


    End Function
End Class
