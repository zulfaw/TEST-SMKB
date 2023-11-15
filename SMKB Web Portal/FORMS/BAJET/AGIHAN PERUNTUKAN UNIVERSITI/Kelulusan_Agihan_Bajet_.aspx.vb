Imports System.Data.SqlClient
Imports System.Drawing

Public Class Kelulusan_Agihan_Bajet_
    Inherits System.Web.UI.Page

    Dim decTotBajet2 As Decimal = 0.00
    Dim sLogBaru As String
    Dim sLogMedan As String
    Dim strNoStaf As String = UserInfo.strSesStaffID
    Dim sLogLevel As String = UserInfo.strUsrTahap
    Dim lsLogUsrIP As String = UserInfo.strUsrPcIP
    Dim lsLogUsrPC As String = UserInfo.strUsrPCName

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Session("sesKodSubMenu") = Request.QueryString("KodSubMenu")
            fBindDdlTahunAgih()
            fBindDdlPTJ()

            gvListAgihan.DataSource = New List(Of String)
            gvListAgihan.DataBind()
        End If

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

    Dim TotJum As Decimal
    Private Sub gvListAgihan_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvListAgihan.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim lblTotBajet As Label = CType(e.Row.FindControl("lblIndObjSbg"), Label)
                Dim strIndObjSbg As String = lblTotBajet.Text

                If strIndObjSbg = "" Then
                    e.Row.Font.Bold = True
                    e.Row.BackColor = System.Drawing.Color.FromArgb(251, 238, 213)
                    e.Row.Cells(7).ForeColor = ColorTranslator.FromHtml("#a71815")
                    Dim lblBajet As Label = CType(e.Row.FindControl("lblBajet"), Label)
                    TotJum += Decimal.Parse(lblBajet.Text)
                    ViewState("strJumBajet") = TotJum.ToString("#,##0.00")
                End If
            End If

            If e.Row.RowType = DataControlRowType.Footer Then
                Dim lblTotBajet As Label = CType(e.Row.FindControl("lblTotBajet"), Label)
                lblTotBajet.Text = ViewState("strJumBajet").ToString()
                lblTotBajet.Font.Bold = True
            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Function fBindGvlistAgihan()
        Dim intCount As Integer = 0
        Dim dbconn As New DBKewConn
        Try
            Dim strSql As String
            strSql = "select ISNULL(sum(bg05_amaun), 0) as JumAgihPTj from bg05_agihptj where bg05_tahun = '" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' and kodptj = '" & Trim(ddlPTJ.SelectedValue.TrimEnd) & "' and kodagih='AL'"

            Dim ds As New DataSet
            ds = dbconn.fSelectCommand(strSql)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim decJumAgihPTj As Decimal = CDec(Trim(ds.Tables(0).Rows(0)("JumAgihPTj").ToString.TrimEnd))

                    If decJumAgihPTj > 0 Then
                        strSql = "select ISNULL(sum(bg07_amaun), 0) as JumLulus from bg07_agihobjsbg where bg07_tahun = '" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' and kodptj = '" & Trim(ddlPTJ.SelectedValue.TrimEnd) & "' and kodagih='AL' and (bg07_statlulus='1' or bg07_statlulus is null)"
                        ds = dbconn.fSelectCommand(strSql)
                        If Not ds Is Nothing Then
                            If ds.Tables(0).Rows.Count > 0 Then
                                Dim decJumLulus As Decimal = CDec(Trim(ds.Tables(0).Rows(0)("JumLulus").ToString.TrimEnd))

                                If decJumAgihPTj = decJumLulus Then
                                    fGlobalAlert("Agihan bajet untuk PTj - " & ddlPTJ.SelectedItem.Text & " telah diluluskan.", Me.Page, Me.[GetType]())
                                    sClearGvLst()
                                    lbtnLulus.Visible = False
                                    Exit Function
                                End If
                            End If
                        End If
                    End If
                End If
            End If



            Dim dt As New DataTable
            dt.Columns.Add("IndObjSbg", GetType(String))
            dt.Columns.Add("kodKW", GetType(String))
            dt.Columns.Add("kodKO", GetType(String))
            dt.Columns.Add("kodPTj", GetType(String))
            dt.Columns.Add("kodKP", GetType(String))
            dt.Columns.Add("ObjAm", GetType(String))
            dt.Columns.Add("ObjSbg", GetType(String))
            dt.Columns.Add("Bajet", GetType(String))

            '1-Cari jumlah amaun PTj
            strSql = "select sum(bg05_amaun) as JumPTj,kodkw from bg05_agihptj where bg05_tahun = '" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' and kodptj = '" & Trim(ddlPTJ.SelectedValue.TrimEnd) & "' and kodagih='AL' group by kodkw"

            Dim ds1 As New DataSet

            ds1 = dbconn.fSelectCommand(strSql)
            Dim strKodKW As String
            Dim decJumPTj As Decimal
            Dim strLstKodKW = New List(Of String)
            Dim strLstKodKW2 = New List(Of String)
            Dim blnAgihPTj As Boolean = True
            Dim blnAgihSbg As Boolean = True


            If Not ds1 Is Nothing Then
                If ds1.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds1.Tables(0).Rows.Count - 1
                        decJumPTj = CDec(Trim(ds1.Tables(0).Rows(i)("JumPTj").ToString.TrimEnd))
                        strKodKW = Trim(ds1.Tables(0).Rows(i)("KodKW").ToString.TrimEnd)

                        'Semak KW yang tiada agihan kepada PTj
                        If decJumPTj = 0 Then
                            strLstKodKW.Add(strKodKW)
                            blnAgihPTj = False
                        End If


                        '2- Cari jumlah amaun objek sebagai
                        strSql = "select sum(bg07_amaun) as jumsbg from bg07_agihobjsbg where bg07_tahun = '" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' and kodptj = '" & Trim(ddlPTJ.SelectedValue.TrimEnd) & "' and kodagih='AL' and kodkw='" & strKodKW & "' and (bg07_statlulus='0' or bg07_statlulus is null) "

                        Dim ds2 As New DataSet
                        dbconn = New DBKewConn
                        ds2 = dbconn.fSelectCommand(strSql)
                        If Not ds2 Is Nothing Then
                            If ds2.Tables(0).Rows.Count > 0 Then
                                Dim decJumSbg As Decimal
                                decJumSbg = IIf(IsDBNull(ds2.Tables(0).Rows(0)("JumSbg")), 0, ds2.Tables(0).Rows(0)("JumSbg"))

                                '3 - compare amaun PTj dengan amaun objek sebagai
                                If decJumPTj = decJumSbg Then

                                    strSql = "select a.indsbg,a.indam,a.kodkw,a.kodKO,a.kodptj, a.kodkp, a.kodvot,(select b.KodVot + ' - ' + b.Butiran  from MK_Vot b where b.KodVot = a.KodVot) as ButiranVot,a.amaun from vbg_lulusagih a where a.tahun ='" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' and a.kodptj='" & Trim(ddlPTJ.SelectedValue.TrimEnd) & "' and a.kodkw='" & strKodKW & "' order by a.kodkw,a.kodKO,a.kodptj, a.kodkp,a.kodvot"

                                    Dim ds3 As New DataSet
                                    dbconn = New DBKewConn
                                    ds3 = dbconn.fSelectCommand(strSql)

                                    If Not ds3 Is Nothing Then
                                        If ds3.Tables(0).Rows.Count > 0 Then
                                            Dim strKodKW1, strKodKO, strKodPTj, strKodKP, strObjAm, strObjSbg, strBajet As String
                                            Dim strIndSbg As String
                                            Dim decBajet As Decimal
                                            For j As Integer = 0 To ds3.Tables(0).Rows.Count - 1
                                                strIndSbg = ds3.Tables(0).Rows(j).Item("indsbg").ToString
                                                If strIndSbg = "" Then
                                                    strKodKW1 = ds3.Tables(0).Rows(j).Item("kodkw").ToString
                                                    strKodKO = ds3.Tables(0).Rows(j).Item("KodKO").ToString
                                                    strKodPTj = ds3.Tables(0).Rows(j).Item("kodptj").ToString
                                                    strKodKP = ds3.Tables(0).Rows(j).Item("kodKP").ToString
                                                    strObjAm = ds3.Tables(0).Rows(j).Item("ButiranVot").ToString

                                                    strObjSbg = String.Empty
                                                    decBajet = ds3.Tables(0).Rows(j).Item("amaun").ToString
                                                    strBajet = decBajet.ToString("#,##0.00")

                                                Else
                                                    strKodKW1 = String.Empty
                                                    strKodKO = String.Empty
                                                    strKodPTj = String.Empty
                                                    strKodKP = String.Empty
                                                    strObjAm = String.Empty

                                                    strObjSbg = ds3.Tables(0).Rows(j).Item("ButiranVot").ToString
                                                    decBajet = ds3.Tables(0).Rows(j).Item("amaun").ToString
                                                    strBajet = decBajet.ToString("#,##0.00")
                                                End If
                                                'If CDec(strBajet) > 0 Then
                                                dt.Rows.Add(strIndSbg, strKodKW1, strKodKO, strKodPTj, strKodKP, strObjAm, strObjSbg, strBajet)
                                                'End If
                                            Next
                                        End If
                                    End If
                                Else
                                    'Semak KW yang tiada agihan kepada objek sebagai
                                    strLstKodKW2.Add(strKodKW)
                                    blnAgihSbg = False
                                End If
                            End If
                        Else

                        End If
                    Next

                Else
                    fGlobalAlert("Tiada agihan peruntukan kepada PTj " & ddlPTJ.SelectedItem.Text & ".", Me.Page, Me.[GetType]())
                    lbtnLulus.Visible = False
                    sClearGvLst()
                    Exit Try
                End If
            End If

            If blnAgihPTj = False Then
                Dim strLstKW As String
                For i = 0 To strLstKodKW.Count - 1
                    strLstKW = strLstKW + "<li>" + strLstKodKW(i) + " - " & fGetButKW(strLstKodKW(i)) + "</li>"
                Next

                strLstKW = Trim(strLstKW.TrimEnd).Remove(0, 1)
                fGlobalAlert("Peruntukan untuk KW berikut belum selesai diagih kepada PTj:" + "<br />" + "KW : " & strLstKW, Me.Page, Me.[GetType]())
                lbtnLulus.Visible = False
                sClearGvLst()
                Exit Try
            End If

            If blnAgihSbg = False Then
                Dim strLstKW As String
                For i = 0 To strLstKodKW2.Count - 1
                    strLstKW = strLstKW + "<li>" + strLstKodKW2(i) + " - " & fGetButKW(strLstKodKW2(i)) + "</li>"
                Next
                fGlobalAlert("Agihan peruntukan kepada 'Objek Sebagai' belum selesai." + "<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "Kumpulan Wang : " + "<ul>" + strLstKW + "</ul>", Me.Page, Me.[GetType]())
                lbtnLulus.Visible = False
                sClearGvLst()
                Exit Try
            End If

            If dt.Rows.Count > 0 Then
                intCount = dt.Rows.Count
            Else
                intCount = 0
                dt.Clear()
            End If
            hidRecCount.Value = intCount
            gvListAgihan.DataSource = dt
            gvListAgihan.DataBind()

            lbtnLulus.Visible = True

        Catch ex As Exception
        End Try

    End Function

    Private Sub sClearGvLst()
        gvListAgihan.DataSource = New List(Of String)
        gvListAgihan.DataBind()
    End Sub

    Protected Sub lbtnLulus_Click(sender As Object, e As EventArgs) Handles lbtnLulus.Click
        Dim str As String
        Dim trackno As Integer = 0
        If gvListAgihan.Rows.Count = 0 Then
            fGlobalAlert("Tiada senarai agihan untuk diluluskan!", Me.Page, Me.[GetType]())
            Exit Sub
        End If

        Dim strNoStaf = Session("ssusrID")

        System.Threading.Thread.Sleep(1000)
        Try
            Dim ds As New DataSet
            Dim blnSuccess As Boolean = True
            Dim strSql As String
            Dim strAmaun As String

            Dim strDtToday As String = Now.ToString("yyyy-MM-dd")
            Dim strStaffID As String = Session("ssusrID")

            Dim strIdxObjSbg As String
            Dim strKodKW, strKodKO, strKodPTj, strKodKP As String
            Dim dbconn As New DBKewConn
            Dim strTahun = Trim(ddlTahunAgih.SelectedValue.TrimEnd)
            Try
                dbconn.sConnBeginTrans()
                For Each row As GridViewRow In gvListAgihan.Rows
                    trackno = 10
                    strIdxObjSbg = TryCast(row.FindControl("lblIndObjSbg"), Label).Text

                    If (row.Cells(1).Text.Contains("&nbsp;") OrElse row.Cells(1).Text.Equals("")) Then
                    Else
                        strKodKW = row.Cells(1).Text
                        strKodKO = row.Cells(2).Text
                        strKodPTj = row.Cells(3).Text
                        strKodKP = row.Cells(4).Text
                    End If

                    If strIdxObjSbg <> "" Then
                        trackno = 11
                        str = TryCast(row.FindControl("lblObjSbg"), Label).Text
                        Dim strObjSbg = TryCast(row.FindControl("lblObjSbg"), Label).Text.Substring(0, 5)
                        strAmaun = CDec(TryCast(row.FindControl("lblBajet"), Label).Text)

                        '1--KEMAS KINI STATUS LULUS bg07_agihobjsbg
                        strSql = "UPDATE bg07_agihobjsbg Set bg07_Amaun=@Amaun, bg07_StatLulus=@StatLulus, bg07_TkhLulus=@TkhLulus,
                                    bg07_NoStafLulus=@StafID
                                    WHERE bg07_IndObjSbg=@IndObjSbg AND bg07_StatLulus=@StatLulus2"
                        Dim paramSql() As SqlParameter = {
                            New SqlParameter("@Amaun", strAmaun),
                            New SqlParameter("@StatLulus", 1),
                             New SqlParameter("@TkhLulus", strDtToday),
                            New SqlParameter("@StafID", strNoStaf),
                            New SqlParameter("@StaffID", strStaffID),
                            New SqlParameter("@IndObjSbg", strIdxObjSbg),
                            New SqlParameter("@StatLulus2", 0)
                            }

                        trackno = 1
                        If Not dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                            blnSuccess = False
                            Exit Try
                        Else
                            'AUDIT LOG
                            sLogMedan = "bg07_Amaun|bg07_StatLulus|bg07_TkhLulus|bg07_NoStafLulus|bg07_IndObjSbg|bg07_StatLulus"
                            sLogBaru = strAmaun & "|1|" & strDtToday & "|" & strNoStaf & "|" & strStaffID & "|" & strIdxObjSbg & "|0"

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
                                New SqlParameter("@InfoTable", "bg07_agihobjsbg"),
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

                        '--2  KIRA JUMLAH PERUNTUKAN, BAKI SEMASA, BAKI PERUNTUKAN, BAKI SELEPAS LULUS
                        Dim Nett_Jum, Baki_Sms, Baki_Perutk, Baki_Slps_Pem As Decimal

                        strSql = "select mk01_perutk,mk01_virmasuk,mk01_virkeluar,mk01_bljytd,mk01_tngytd,mk01_pemlulus 
                        from mk01_vottahun with (nolock) where mk01_tahun='" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' and kodkw='" & strKodKW & "' and kodko = '" & strKodKO & "' and kodptj='" & strKodPTj & "' and kodKP = '" & strKodKP & "' and kodvot='" & strObjSbg & "'"

                        ds = fGetRec(strSql)
                        If ds.Tables(0).Rows.Count > 0 Then

                            Dim decPerutk As Decimal = CDec(ds.Tables(0).Rows(0)("mk01_perutk").ToString)
                            Dim decVirMasuk As Decimal = CDec(ds.Tables(0).Rows(0)("mk01_virmasuk").ToString)
                            Dim decVirKeluar As Decimal = CDec(ds.Tables(0).Rows(0)("mk01_virkeluar").ToString)
                            Dim decBljytd As Decimal = CDec(ds.Tables(0).Rows(0)("mk01_bljytd").ToString)
                            Dim decTngytd As Decimal = CDec(ds.Tables(0).Rows(0)("mk01_tngytd").ToString)
                            Dim decPemlulus As Decimal = CDec(ds.Tables(0).Rows(0)("mk01_pemlulus").ToString)

                            Nett_Jum = decPerutk + CDec(strAmaun) + decVirMasuk + decVirKeluar
                            Baki_Sms = Nett_Jum - decBljytd
                            Baki_Perutk = Nett_Jum - decBljytd - decTngytd
                            Baki_Slps_Pem = Nett_Jum - decPemlulus

                            '--4 Update mk01_vottahun - ObjSbg

                            strSql = "UPDATE mk01_vottahun SET mk01_perutk = @perutk,mk01_bakisms = @bakisms, mk01_bakiperutk = @bakiperutk,
                        mk01_bakislpspemlulus = @bakislpspemlulus where mk01_tahun=@Tahun and kodkw=@KodKW and KodKO=@KodKO and kodptj=@KodPTJ and KodKP=@KodKP and kodvot= @kodObjSbg"
                            Dim paramSql2() As SqlParameter = {
                            New SqlParameter("@perutk", Nett_Jum),
                            New SqlParameter("@bakisms", Baki_Sms),
                            New SqlParameter("@bakiperutk", Baki_Perutk),
                             New SqlParameter("@bakislpspemlulus", Baki_Slps_Pem),
                            New SqlParameter("@Tahun", strTahun),
                            New SqlParameter("@KodKW", strKodKW),
                            New SqlParameter("@KodKO", strKodKO),
                            New SqlParameter("@KodPTJ", strKodPTj),
                            New SqlParameter("@KodKP", strKodKP),
                            New SqlParameter("@kodObjSbg", strObjSbg)
                            }

                            trackno = 2
                            If Not dbconn.fUpdateCommand(strSql, paramSql2) > 0 Then
                                blnSuccess = False
                                Exit Try
                            Else
                                'AUDIT LOG
                                sLogMedan = "mk01_perutk|mk01_bakisms|mk01_bakiperutk|mk01_bakislpspemlulus|mk01_tahun|kodkw|KodKO|kodptj|KodKP|kodvot"
                                sLogBaru = Nett_Jum & "|" & Baki_Sms & "|" & Baki_Perutk & "|" & Baki_Slps_Pem & "|" & strTahun & "|" & strKodKW & "|" & strKodKO & "|" & strKodPTj & "|" & strKodKP & "|" & strObjSbg

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
                                    New SqlParameter("@InfoTable", "mk01_vottahun"),
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

                            '--5 Select mk01_vottahun - ObjAm
                            Dim strObjAm As String = Trim(Trim(strObjSbg.Substring(0, 1).TrimEnd) & "0000").TrimEnd

                            strSql = "select mk01_perutk,mk01_virmasuk,mk01_virkeluar,mk01_bljytd,mk01_tngytd,mk01_pemlulus 
                        from mk01_vottahun with (nolock) where mk01_tahun='" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' and kodkw='" & strKodKW & "' and KodKO = '" & strKodKO & "' and kodptj='" & strKodPTj & "' and KodKP = '" & strKodKP & "' and kodvot='" & strObjAm & "'"

                            trackno = 3
                            ds = fGetRec(strSql)
                            If ds.Tables(0).Rows.Count > 0 Then
                                trackno = 4
                                '--6 Update mk01_vottahun  - ObjAm
                                Dim Nett_Jum2, Baki_Sms2, Baki_Perutk2, Baki_Slps_Pem2 As Decimal

                                Dim decPerutk2 As Decimal = CDec(ds.Tables(0).Rows(0)("mk01_perutk").ToString)
                                Dim decVirMasuk2 As Decimal = CDec(ds.Tables(0).Rows(0)("mk01_virmasuk").ToString)
                                Dim decVirKeluar2 As Decimal = CDec(ds.Tables(0).Rows(0)("mk01_virkeluar").ToString)
                                Dim decBljytd2 As Decimal = CDec(ds.Tables(0).Rows(0)("mk01_bljytd").ToString)
                                Dim decTngytd2 As Decimal = CDec(ds.Tables(0).Rows(0)("mk01_tngytd").ToString)
                                Dim decPemlulus2 As Decimal = CDec(ds.Tables(0).Rows(0)("mk01_pemlulus").ToString)

                                Nett_Jum2 = decPerutk2 + CDec(strAmaun) + decVirMasuk2 + decVirKeluar2
                                Baki_Sms2 = Nett_Jum2 - decBljytd2
                                Baki_Perutk2 = Nett_Jum2 - decBljytd2 - decTngytd2
                                Baki_Slps_Pem2 = Nett_Jum2 - decPemlulus2

                                trackno = 5
                                strSql = "UPDATE mk01_vottahun SET mk01_perutk = @perutk, mk01_bakisms = @bakisms, mk01_bakiperutk = @bakiperutk, mk01_bakislpspemlulus = @bakislpspemlulus where mk01_tahun=@Tahun and kodkw=@KodKW and KodKO=@KodKO and kodptj=@KodPTJ and KodKP=@KodKP  and kodvot= @kodObjAm"

                                Dim paramSql3() As SqlParameter = {
                                    New SqlParameter("@perutk", Nett_Jum2),
                                    New SqlParameter("@bakisms", Baki_Sms2),
                                    New SqlParameter("@bakiperutk", Baki_Perutk2),
                                     New SqlParameter("@bakislpspemlulus", Baki_Slps_Pem2),
                                    New SqlParameter("@Tahun", strTahun),
                                    New SqlParameter("@KodKW", strKodKW),
                                    New SqlParameter("@KodKO", strKodKO),
                                    New SqlParameter("@KodPTJ", strKodPTj),
                                    New SqlParameter("@KodKP", strKodKP),
                                    New SqlParameter("@kodObjAm", strObjAm)
                                    }

                                trackno = 6
                                If Not dbconn.fUpdateCommand(strSql, paramSql3) > 0 Then
                                    blnSuccess = False
                                    Exit Try
                                Else
                                    'AUDIT LOG
                                    sLogMedan = "mk01_perutk|mk01_bakisms|mk01_bakiperutk|mk01_bakislpspemlulus|mk01_tahun|kodkw|KodKO|kodptj|KodKP|kodvot"
                                    sLogBaru = Nett_Jum2 & "|" & Baki_Sms2 & "|" & Baki_Perutk2 & "|" & Baki_Slps_Pem2 & "|" & strTahun & "|" & strKodKW & "|" & strKodKO & "|" & strKodPTj & "|" & strKodKP & "|" & strObjAm

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
                                        New SqlParameter("@InfoTable", "mk01_vottahun"),
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

                        Else
                            blnSuccess = False
                            Exit Try
                        End If

                    End If

                Next
            Catch ex As Exception
                Dim err As Integer = trackno
                blnSuccess = False
                Dim str2 As String = str
            End Try

            If blnSuccess Then
                dbconn.sConnCommitTrans()
                fGlobalAlert("Agihan peruntukan telah diluluskan!", Me.Page, Me.[GetType]())
                sClearGvLst()
                lbtnLulus.Visible = False
            Else
                dbconn.sConnRollbackTrans()
                fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindDdlPTJ()
        Try
            Dim strSql As String

            strSql = "SELECT DISTINCT TOP 100 PERCENT dbo.MK_PTJ.KodPTJ,(MK_PTJ.KodPTJ + ' - ' + dbo.MK_PTJ.Butiran) as Butiran " &
"FROM dbo.MK_PTJ INNER JOIN dbo.MK01_VotTahun ON dbo.MK_PTJ.KodPTJ = dbo.MK01_VotTahun.KodPTJ " &
"WHERE dbo.MK01_VotTahun.mk01_tahun = '" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' order by KodPtj"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)
            ddlPTJ.DataSource = ds
            ddlPTJ.DataTextField = "Butiran"
            ddlPTJ.DataValueField = "KodPTJ"
            ddlPTJ.DataBind()

            ddlPTJ.Items.Insert(0, "- SILA PILIH -")
            ddlPTJ.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlPTJ_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPTJ.SelectedIndexChanged
        If ddlPTJ.SelectedIndex <> 0 Then
            fBindGvlistAgihan()
        Else
            gvListAgihan.DataSource = New List(Of String)
            gvListAgihan.DataBind()
        End If

    End Sub

    Private Sub ddlTahunAgih_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTahunAgih.SelectedIndexChanged
        fBindDdlPTJ()

        gvListAgihan.DataSource = New List(Of String)
        gvListAgihan.DataBind()
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
            '("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
        End Try
    End Function

End Class