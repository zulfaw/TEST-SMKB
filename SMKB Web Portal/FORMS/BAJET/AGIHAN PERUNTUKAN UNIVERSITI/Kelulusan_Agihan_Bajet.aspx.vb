Imports System.Data.SqlClient
Imports System.Drawing

Public Class Kelulusan_Agihan_Bajet_Ketua_PTj
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
            fBindGvPTjAgih()

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


    Protected Sub lbtnLulus_Click(sender As Object, e As EventArgs) Handles lbtnLulus.Click

        Dim blnChecked As Boolean = False
        For Each gvRow As GridViewRow In GvPTjAgih.Rows
            Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("cbSelect"), CheckBox)
            If chkSel.Checked = True Then
                blnChecked = True
                Exit For
            End If
        Next

        If blnChecked Then
            If fLulus() Then
                fGlobalAlert("Agihan peruntukan telah diluluskan!", Me.Page, Me.[GetType]())
                lbtnLulus.Visible = False
            Else
                fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
            End If
        Else
            fGlobalAlert("Sila pilih agihan PTj yang ingin diluluskan!", Me.Page, Me.[GetType]())
            Exit Sub
        End If



    End Sub

    Private Function fLulus()

        Dim dbconn As New DBKewConn
        Dim strNoStaf = Session("ssusrID")
        Dim strSql
        Dim strTkhToday As String = Now.ToString("yyyy-MM-dd")
        Dim strTahun = Trim(ddlTahunAgih.SelectedValue.TrimEnd)
        Dim blnSuccess As Boolean = True

        Try
            System.Threading.Thread.Sleep(1000)
            dbconn.sConnBeginTrans()

            For Each gvRow As GridViewRow In GvPTjAgih.Rows
                Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("cbSelect"), CheckBox)
                If chkSel.Checked = True Then
                    Dim strKodPTj As String = TryCast(gvRow.FindControl("lblKodPTJ"), Label).Text

                    strSql = "select BG07_IndObjSbg, KodKw, KodKO, KodPTJ, KodKP, KodVot, BG07_Amaun from BG07_AgihObjSbg with (nolock) where BG07_Tahun = '" & strTahun & "' and KodPTJ = '" & strKodPTj & "' and (BG07_StatLulus is null or BG07_StatLulus = 0)"
                    Dim dsBG07 As New DataSet
                    dsBG07 = fGetRec(strSql)
                    If Not dsBG07 Is Nothing Then
                        If dsBG07.Tables(0).Rows.Count > 0 Then

                            For i As Integer = 0 To dsBG07.Tables(0).Rows.Count - 1
                                Dim strIndObjSbg = dsBG07.Tables(0).Rows(i)("BG07_IndObjSbg")
                                Dim strKodKW = dsBG07.Tables(0).Rows(i)("KodKw")
                                Dim strKodKO = dsBG07.Tables(0).Rows(i)("KodKO")
                                'Dim strKodPTj = ds.Tables(0).Rows(0)("KodPTJ")
                                Dim strKodKP = dsBG07.Tables(0).Rows(i)("KodKP")
                                Dim strKodVot = dsBG07.Tables(0).Rows(i)("KodVot")
                                Dim decAmaun As Decimal = CDec(dsBG07.Tables(0).Rows(i)("BG07_Amaun"))

                                '1--KEMAS KINI STATUS LULUS bg07_agihobjsbg
                                strSql = "UPDATE bg07_agihobjsbg Set bg07_StatLulus=@StatLulus, bg07_TkhLulus=@TkhLulus, bg07_NoStafLulus=@StafID
WHERE bg07_IndObjSbg=@IndObjSbg AND (bg07_StatLulus is null or bg07_StatLulus=0)"
                                Dim paramSql() As SqlParameter = {
                                    New SqlParameter("@Amaun", decAmaun),
                                    New SqlParameter("@StatLulus", 1),
                                     New SqlParameter("@TkhLulus", strTkhToday),
                                    New SqlParameter("@StafID", strNoStaf),
                                    New SqlParameter("@IndObjSbg", strIndObjSbg)
                                    }

                                If Not dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                                    blnSuccess = False
                                    Exit Try
                                Else
                                    'AUDIT LOG
                                    sLogMedan = "bg07_Amaun|bg07_StatLulus|bg07_TkhLulus|bg07_NoStafLulus|bg07_IndObjSbg"
                                    sLogBaru = "|1|" & strTkhToday & "|" & strNoStaf & "|" & strIndObjSbg

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
                        from mk01_vottahun with (nolock) where mk01_tahun='" & strTahun & "' and kodkw='" & strKodKW & "' and kodko = '" & strKodKO & "' and kodptj='" & strKodPTj & "' and kodKP = '" & strKodKP & "' and kodvot='" & strKodVot & "'"

                                Dim dsMk01_1 As New DataSet
                                dsMk01_1 = fGetRec(strSql)
                                If dsMk01_1.Tables(0).Rows.Count > 0 Then

                                    Dim decPerutk As Decimal = CDec(dsMk01_1.Tables(0).Rows(0)("mk01_perutk").ToString)
                                    Dim decVirMasuk As Decimal = CDec(dsMk01_1.Tables(0).Rows(0)("mk01_virmasuk").ToString)
                                    Dim decVirKeluar As Decimal = CDec(dsMk01_1.Tables(0).Rows(0)("mk01_virkeluar").ToString)
                                    Dim decBljytd As Decimal = CDec(dsMk01_1.Tables(0).Rows(0)("mk01_bljytd").ToString)
                                    Dim decTngytd As Decimal = CDec(dsMk01_1.Tables(0).Rows(0)("mk01_tngytd").ToString)
                                    Dim decPemlulus As Decimal = CDec(dsMk01_1.Tables(0).Rows(0)("mk01_pemlulus").ToString)

                                    Nett_Jum = decPerutk + decAmaun + decVirMasuk + decVirKeluar
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
                                    New SqlParameter("@kodObjSbg", strKodVot)
                                    }

                                    If Not dbconn.fUpdateCommand(strSql, paramSql2) > 0 Then
                                        blnSuccess = False
                                        Exit Try
                                    Else
                                        'AUDIT LOG
                                        sLogMedan = "mk01_perutk|mk01_bakisms|mk01_bakiperutk|mk01_bakislpspemlulus|mk01_tahun|kodkw|KodKO|kodptj|KodKP|kodvot"
                                        sLogBaru = Nett_Jum & "|" & Baki_Sms & "|" & Baki_Perutk & "|" & Baki_Slps_Pem & "|" & strTahun & "|" & strKodKW & "|" & strKodKO & "|" & strKodPTj & "|" & strKodKP & "|" & strKodVot

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
                                    Dim strObjAm As String = Trim(Trim(strKodVot.Substring(0, 1).TrimEnd) & "0000").TrimEnd

                                    strSql = "select mk01_perutk,mk01_virmasuk,mk01_virkeluar,mk01_bljytd,mk01_tngytd,mk01_pemlulus 
                        from mk01_vottahun with (nolock) where mk01_tahun='" & strTahun & "' and kodkw='" & strKodKW & "' and KodKO = '" & strKodKO & "' and kodptj='" & strKodPTj & "' and KodKP = '" & strKodKP & "' and kodvot='" & strObjAm & "'"

                                    Dim dsMk01_2 As New DataSet
                                    dsMk01_2 = fGetRec(strSql)
                                    If dsMk01_2.Tables(0).Rows.Count > 0 Then

                                        '--6 Update mk01_vottahun  - ObjAm
                                        Dim Nett_Jum2, Baki_Sms2, Baki_Perutk2, Baki_Slps_Pem2 As Decimal

                                        Dim decPerutk2 As Decimal = CDec(dsMk01_2.Tables(0).Rows(0)("mk01_perutk").ToString)
                                        Dim decVirMasuk2 As Decimal = CDec(dsMk01_2.Tables(0).Rows(0)("mk01_virmasuk").ToString)
                                        Dim decVirKeluar2 As Decimal = CDec(dsMk01_2.Tables(0).Rows(0)("mk01_virkeluar").ToString)
                                        Dim decBljytd2 As Decimal = CDec(dsMk01_2.Tables(0).Rows(0)("mk01_bljytd").ToString)
                                        Dim decTngytd2 As Decimal = CDec(dsMk01_2.Tables(0).Rows(0)("mk01_tngytd").ToString)
                                        Dim decPemlulus2 As Decimal = CDec(dsMk01_2.Tables(0).Rows(0)("mk01_pemlulus").ToString)

                                        Nett_Jum2 = decPerutk2 + decAmaun + decVirMasuk2 + decVirKeluar2
                                        Baki_Sms2 = Nett_Jum2 - decBljytd2
                                        Baki_Perutk2 = Nett_Jum2 - decBljytd2 - decTngytd2
                                        Baki_Slps_Pem2 = Nett_Jum2 - decPemlulus2


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
                            Next
                        End If
                    End If

                End If
            Next

        Catch ex As Exception
            blnSuccess = False
        End Try

        If blnSuccess Then
            dbconn.sConnCommitTrans()
            sCheckAgih()
            Return True
        Else
            dbconn.sConnRollbackTrans()
            Return False
        End If


    End Function

    Private Sub fBindGvPTjAgih()
        sClearGvPTjAgih()

        Dim dbconn As New DBKewConn
        Dim intCount As Integer

        Try
            Dim strSql = "select BG05_IndPTJ, KodPTJ, (select Butiran from MK_PTJ where MK_PTJ .KodPTJ = bg05_agihptj.KodPTJ) as butPTj, BG05_Amaun from bg05_agihptj where bg05_tahun = '" & Trim(ddlTahunAgih.SelectedValue.TrimEnd) & "' and kodagih='AL' and BG05_Amaun > 0"

            Dim ds As New DataSet
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    GvPTjAgih.DataSource = ds.Tables(0)
                    GvPTjAgih.DataBind()
                    intCount = ds.Tables(0).Rows.Count
                End If
            End If

            If intCount > 0 Then
                lbtnLulus.Visible = True
                sCheckAgih()
            Else
                lbtnLulus.Visible = False
            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Sub sClearGvPTjAgih()
        GvPTjAgih.DataSource = New List(Of String)
        GvPTjAgih.DataBind()
    End Sub

    Private Sub sCheckAgih()

        Try
            For Each gvRow As GridViewRow In GvPTjAgih.Rows
                Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("cbSelect"), CheckBox)
                Dim btnChecked As LinkButton = DirectCast(gvRow.FindControl("btnChecked"), LinkButton)
                Dim btnLulus As LinkButton = DirectCast(gvRow.FindControl("btnLulus"), LinkButton)
                Dim strKodpTj As String = TryCast(gvRow.FindControl("lblKodPTJ"), Label).Text
                Dim decBajetPTj As String = TryCast(gvRow.FindControl("lblBajetPTj"), Label).Text
                Dim strTahun As String = ddlTahunAgih.SelectedValue.TrimEnd

                If fSelesaiLulus(strTahun, strKodpTj, decBajetPTj) Then
                    chkSel.Visible = False
                    btnChecked.Visible = False
                    btnLulus.Visible = True
                    gvRow.ForeColor = ColorTranslator.FromHtml("#5a5a5a")
                Else

                    If fSelesaiAgih(strTahun, strKodpTj, decBajetPTj) Then
                        chkSel.Visible = True
                        btnChecked.Visible = False
                        btnLulus.Visible = False
                        gvRow.ForeColor = Drawing.Color.Black
                    Else
                        chkSel.Visible = False
                        btnChecked.Visible = True
                        btnLulus.Visible = False
                        gvRow.ForeColor = ColorTranslator.FromHtml("#5a5a5a")
                    End If

                End If



                'Dim blnAgih As Boolean = fAgih(strTahun, strKodpTj, decBajetPTj)

                'If blnAgih Then
                '    chkSel.Visible = True
                '    btnChecked.Visible = False
                '    gvRow.ForeColor = Drawing.Color.Black
                'Else
                '    chkSel.Visible = False
                '    btnChecked.Visible = True

                '    gvRow.ForeColor = ColorTranslator.FromHtml("#5a5a5a")
                'End If
            Next
        Catch ex As Exception

        End Try

    End Sub

    Private Function fSelesaiAgih(ByVal strtahun As String, ByVal strKodPTj As String, ByVal decBajetPTj As Decimal) As Boolean

        Dim dbconn As New DBKewConn
        Try

            '2- Cari jumlah amaun objek sebagai
            Dim strSql = "select sum(bg07_amaun) as jumsbg from bg07_agihobjsbg where bg07_tahun = '" & strtahun & "' and kodptj = '" & strKodPTj & "' and kodagih='AL' and kodkw='01' and (bg07_statlulus='0' or bg07_statlulus is null) "

            Dim ds2 As New DataSet
            dbconn = New DBKewConn
            ds2 = dbconn.fSelectCommand(strSql)
            If Not ds2 Is Nothing Then
                If ds2.Tables(0).Rows.Count > 0 Then
                    Dim decJumSbg As Decimal
                    decJumSbg = IIf(IsDBNull(ds2.Tables(0).Rows(0)("JumSbg")), 0, ds2.Tables(0).Rows(0)("JumSbg"))

                    '3 - compare amaun PTj dengan amaun objek sebagai
                    If decBajetPTj = decJumSbg Then
                        Return True
                    Else
                        Return False
                    End If
                End If
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function fSelesaiLulus(ByVal strtahun As String, ByVal strKodPTj As String, ByVal decBajetPTj As Decimal) As Boolean

        Dim dbconn As New DBKewConn
        Try

            'cari amaun lulus
            Dim strSql = "select sum(bg07_amaun) as jumsbg from bg07_agihobjsbg where bg07_tahun = '" & strtahun & "' and kodptj = '" & strKodPTj & "' and kodagih='AL' and kodkw='01' and (bg07_statlulus='1') "

            Dim ds2 As New DataSet
            dbconn = New DBKewConn
            ds2 = dbconn.fSelectCommand(strSql)
            If Not ds2 Is Nothing Then
                If ds2.Tables(0).Rows.Count > 0 Then
                    Dim decJumSbg As Decimal
                    decJumSbg = IIf(IsDBNull(ds2.Tables(0).Rows(0)("JumSbg")), 0, ds2.Tables(0).Rows(0)("JumSbg"))

                    'compare agihan ptj dengan agihan objek sebagai lulus
                    If decBajetPTj = decJumSbg Then
                        Return True
                    Else
                        Return False
                    End If
                End If
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ddlTahunAgih_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTahunAgih.SelectedIndexChanged
        fBindGvPTjAgih()

    End Sub

    Dim decJumBajet As Decimal
    Private Sub GvPTjAgih_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GvPTjAgih.RowDataBound

        Try
            If e.Row.RowType = DataControlRowType.DataRow Then


                Dim lblBajetPTj As Label = CType(e.Row.FindControl("lblBajetPTj"), Label)
                decJumBajet += CDec(lblBajetPTj.Text)


            End If

            If e.Row.RowType = DataControlRowType.Footer Then

                Dim lblJumBajet As Label = CType(e.Row.FindControl("lblJumBajet"), Label)
                lblJumBajet.Text = FormatNumber(decJumBajet)

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub GvPTjAgih_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GvPTjAgih.SelectedIndexChanged
        Try

            Dim strTkhToday = Now.ToString("yyyy-MM-dd")
            Dim row As GridViewRow = GvPTjAgih.SelectedRow
            Dim strKodPTj = CType(row.FindControl("lblKodPTJ"), Label).Text.ToString
            Dim strTahun = Trim(ddlTahunAgih.SelectedValue.TrimEnd)
            Dim strKodSub = Request.QueryString("KodSub")
            Dim strKodSubMenu = Request.QueryString("KodSubMenu")

            'Open other page.
            Response.Redirect($"~/FORMS/BAJET/AGIHAN PERUNTUKAN UNIVERSITI/Maklumat_Kelulusan_Agihan.aspx?KodSub=" & strKodSub & "&KodSubMenu=" & strKodSubMenu & "&KodPTj=" & strKodPTj & "&Tahun=" & strTahun & "", False)

        Catch ex As Exception

        End Try
    End Sub
End Class