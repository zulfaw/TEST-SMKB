Imports System.Data.SqlClient

Public Class DAFTAR_EMAIL_NOTIFIKASI
    Inherits System.Web.UI.Page

    Dim sLogBaru As String
    Dim sLogMedan As String
    Dim strNoStaf As String = UserInfo.strSesStaffID
    Dim sLogLevel As String = UserInfo.strUsrTahap
    Dim lsLogUsrIP As String = UserInfo.strUsrPcIP
    Dim lsLogUsrPC As String = UserInfo.strUsrPCName

    Dim dbconn As New DBKewConn
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Session("sesKodSubMenu") = Request.QueryString("KodSubMenu")

                ddlSubModulD.Items.Insert(0, New ListItem("- SILA PILIH MODUL- ", 0))
                ddlSubModulD.SelectedIndex = 0

                ddlSubMenuD.Items.Insert(0, New ListItem("- SILA PILIH SUB MODUL- ", 0))
                ddlSubMenuD.SelectedIndex = 0
                fBindDdlModul()
                fBindStaff()
                fBindTugas()
                sLoadLst()
            End If

        Catch ex As Exception
            'fErrorLog("Pendaftaran_Menu.aspx(Page_Load())- " & ex.Message.ToString)
        End Try
    End Sub

    Private Sub sLoadLst()
        Dim intRec As Integer = 0
        Dim strSql As String
        Try

            sClearGvLst()



            strSql = "select SUBSTRING(KodSubMenu , 1, 2) AS KodModul, (select MK_UModul .NamaModul from MK_UModul where MK_UModul.KodModul = SUBSTRING(KodSubMenu , 1, 2)) as ButModul, SUBSTRING(KodSubMenu , 1, 4) as KodSubModul, (select MK_USubModul.NamaSub from MK_USubModul where MK_USubModul.KodSub = SUBSTRING(KodSubMenu , 1, 4)) as ButSubModul, KodSubMenu, (Select MK_USubMenu.NamaSubMenu from MK_USubMenu where MK_USubMenu.KodSubMenu = MK_TugasDt.KodSubMenu) as ButSubMenu, NoStaf, KodTugas, (select MK_Tugas.Butiran from MK_Tugas where mk_tugas.KodTugas = MK_TugasDt.KodTugas) as butTugas, Email, Status from MK_TugasDt"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    gvLst.DataSource = ds
                    gvLst.DataBind()
                    intRec = ds.Tables(0).Rows.Count
                End If
            End If

            lblJumRekod.InnerText = intRec

        Catch ex As Exception

        End Try
    End Sub

    Private Sub sClearGvLst()
        gvLst.DataSource = New List(Of String)
        gvLst.DataBind()
    End Sub

    Private Sub ddlModulD_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlModulD.SelectedIndexChanged
        Try
            Dim strKodModul As String
            strKodModul = ddlModulD.SelectedValue.ToString

            fBindDdlSubModul(strKodModul)



        Catch ex As Exception
            'fErrorLog("Pendaftaran_Menu.aspx(ddlKodModul_SelectedIndexChanged)- " & ex.Message.ToString)
        End Try
    End Sub

    Private Function fBindDdlSubModul(strKodModul)
        Try
            ddlSubModulD.Items.Clear()
            Dim strSql As String = "select KodSub, (KodSub + ' - ' + DisSub) as Butiran From MK_USubModul WHERE kodModul = '" & strKodModul & "' and status = 1 ORDER BY Urutan"
            Using dt = dbconn.fSelectCommandDt(strSql)

                ddlSubModulD.DataSource = dt
                ddlSubModulD.DataTextField = "Butiran"
                ddlSubModulD.DataValueField = "KodSub"
                ddlSubModulD.DataBind()

                ddlSubModulD.Items.Insert(0, New ListItem("- SILA PILIH - ", 0))
                ddlSubModulD.SelectedIndex = 0
            End Using
        Catch ex As Exception

        End Try
    End Function


    Private Sub ddlSubModulD_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSubModulD.SelectedIndexChanged
        Try
            Dim strKodSubModul As String
            strKodSubModul = ddlSubModulD.SelectedValue.ToString

            fBindDdlSubMenu(strKodSubModul)

        Catch ex As Exception
            'fErrorLog("Pendaftaran_Menu.aspx(ddlKodModul_SelectedIndexChanged)- " & ex.Message.ToString)
        End Try
    End Sub

    Private Function fBindDdlSubMenu(strKodSubModul)
        Try
            ddlSubMenuD.Items.Clear()
            Dim strSql As String = "select KodSubMenu, (KodSubMenu + ' - ' + NamaSubMenu) as Butiran from MK_USubMenu WHERE KodSub='" & strKodSubModul & "' and Status = 1 ORDER BY URUTAN"

            Using dt = dbconn.fSelectCommandDt(strSql)
                ddlSubMenuD.DataSource = dt
                ddlSubMenuD.DataTextField = "Butiran"
                ddlSubMenuD.DataValueField = "KodSubMenu"
                ddlSubMenuD.DataBind()

                ddlSubMenuD.Items.Insert(0, New ListItem("- SILA PILIH - ", 0))
                ddlSubMenuD.SelectedIndex = 0
            End Using
        Catch ex As Exception

        End Try
    End Function
    Private Sub fBindStaff()
        Dim ds As New DataSet
        Dim dbconnSMSM As New DBSMConn()
        Try

            Dim strSQLSMSM = "select MS01_NoStaf, (MS01_NoStaf + ' - ' + MS01_Nama) as NamaStaf, MS01_Email from MS01_Peribadi Where MS01_Status = '1' Order by MS01_Nama"
            ds = dbconnSMSM.fselectCommand(strSQLSMSM)

            ddlStaf.DataSource = ds
            ddlStaf.DataTextField = "NamaStaf"
            ddlStaf.DataValueField = "MS01_NoStaf"
            ddlStaf.DataBind()

            ddlStaf.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlStaf.SelectedIndex = 0

            Dim dt As DataTable = ds.Tables(0)
            ViewState("dtStaf") = dt

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindTugas()
        Dim ds As New DataSet
        Dim dbconnSMSM As New DBSMConn()

        Try
            Dim strSql As String = "select KodTugas, Butiran from mk_tugas"

            Using dt = dbconn.fSelectCommandDt(strSql)
                ddlTugas.DataSource = dt
                ddlTugas.DataTextField = "Butiran"
                ddlTugas.DataValueField = "KodTugas"
                ddlTugas.DataBind()

                ddlTugas.Items.Insert(0, New ListItem("- SILA PILIH - ", 0))
                ddlTugas.SelectedIndex = 0
            End Using


        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlStaf_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStaf.SelectedIndexChanged
        Try
            Dim strNoStaf As String = Trim(ddlStaf.SelectedValue.TrimEnd)

            Dim dtStaf As DataTable = ViewState("dtStaf")
            Dim dvStaf As New DataView(dtStaf)

            dvStaf.RowFilter = "MS01_NoStaf = '" & strNoStaf & "'"

            txtEmail.Text = dvStaf.Item(0)("MS01_Email").ToString

        Catch ex As Exception

        End Try
    End Sub

    Private Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.Click
        If Page.IsValid Then

            Dim strKodSubMenu As String = ddlSubMenuD.SelectedValue

            Try
                Dim strSql = "select count(*) from MK_TugasDt where KodSubMenu = '" & strKodSubMenu & "'"

                If fCheckRec(strSql) > 0 Then
                    If fKemasKini(strKodSubMenu) = True Then
                        fGlobalAlert("Maklumat telah disimpan!", Me.Page, Me.[GetType]())
                        sLoadLst()
                    Else
                        fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
                    End If
                Else
                    If fSimpan() = True Then
                        fGlobalAlert("Maklumat telah disimpan!", Me.Page, Me.[GetType]())
                        sLoadLst()
                    Else
                        fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
                    End If
                End If
            Catch ex As Exception
                fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
                Exit Sub
            End Try

        End If
    End Sub

    Private Function fSimpan() As Boolean
        Dim blnSuccess As Boolean = True
        Dim dbconn As New DBKewConn

        Try

            Dim strSql As String
            Dim strKodSubMenu As String = ddlSubMenuD.SelectedValue
            Dim strNoStaf As String = ddlStaf.SelectedValue
            Dim strKodTugas As String = ddlTugas.SelectedValue
            Dim strEmail As String = Trim(txtEmail.Text.TrimEnd)
            Dim blnStatus As Boolean = rbStatus.SelectedValue

            dbconn.sConnBeginTrans()

            'Insert Into AR01_Bil
            strSql = "insert into MK_TugasDt (KodSubMenu , NoStaf , KodTugas , Email , STATUS) values (@KodSubMenu, @NoStaf, @KodTugas, @Email, @Status)"

            Dim paramSql() As SqlParameter =
                {
                New SqlParameter("@KodSubMenu", strKodSubMenu),
                New SqlParameter("@NoStaf", strNoStaf),
                New SqlParameter("@KodTugas", strKodTugas),
                New SqlParameter("@Email", strEmail),
                New SqlParameter("@Status", blnStatus)
                }

            If Not dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                blnSuccess = False
                Exit Try
            Else
                'AUDIT LOG
                sLogMedan = "KodSubMenu|NoStaf|KodTugas|Email|STATUS"
                sLogBaru = strKodSubMenu & "|" & strNoStaf & "|" & strKodTugas & "|" & strEmail & "|" & blnStatus & "   "
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
                    New SqlParameter("@InfoTable", "MK_TugasDt"),
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

        Catch ex As Exception
            blnSuccess = False
        End Try

        If blnSuccess = True Then
            dbconn.sConnCommitTrans()
            Return True
        ElseIf blnSuccess = False Then
            dbconn.sConnRollbackTrans()
            Return False
        End If

    End Function

    Private Function fKemasKini(ByVal strKodSubMenu As String) As Boolean

        Dim strSql As String
        Dim dbconn As New DBKewConn
        Dim blnSuccess As Boolean = True

        Try

            Dim strNoStaf As String = ddlStaf.SelectedValue
            Dim strKodTugas As String = ddlTugas.SelectedValue
            Dim strEmail As String = Trim(txtEmail.Text.TrimEnd)
            Dim blnStatus As Boolean = rbStatus.SelectedValue

            'Update AR01_Bil
            strSql = "Update MK_TugasDt set NoStaf = @NoStaf, KodTugas = @KodTugas, Email = @Email, Status = @Status where KodSubMenu = @KodSubMenu"

            Dim paramSql() As SqlParameter =
                {
                New SqlParameter("@NoStaf", strNoStaf),
                New SqlParameter("@KodTugas", strKodTugas),
                New SqlParameter("@Email", strEmail),
                New SqlParameter("@Status", blnStatus),
                New SqlParameter("@KodSubMenu", strKodSubMenu)
                }

            dbconn.sConnBeginTrans()
            If Not dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                blnSuccess = False
                Exit Try
            Else
                'AUDIT LOG
                sLogMedan = "NoStaf|KodTugas|Email|Status|KodSubMenu"
                sLogBaru = strNoStaf & "|" & strKodTugas & "|" & strEmail & "|" & blnStatus & "|" & strKodSubMenu

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
                    New SqlParameter("@InfoTable", "MK_TugasDt"),
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

        Catch ex As Exception
            blnSuccess = False
        End Try

        If blnSuccess = True Then
            dbconn.sConnCommitTrans()
            Return True
        ElseIf blnSuccess = False Then
            dbconn.sConnRollbackTrans()
            Return False
        End If
    End Function

    Private Sub fBindDdlModul()
        Dim ds As New DataSet
        Dim dbconnSMSM As New DBSMConn()

        Try
            ddlModulD.Items.Clear()
            Dim strSql As String = "select KodModul, (KodModul + ' - ' + NamaModul) as Butiran from MK_UModul where Status = 1 ORDER BY URUTAN"

            Using dt = dbconn.fSelectCommandDt(strSql)
                ddlModulD.DataSource = dt
                ddlModulD.DataTextField = "Butiran"
                ddlModulD.DataValueField = "KodModul"
                ddlModulD.DataBind()

                ddlModulD.Items.Insert(0, New ListItem("- SILA PILIH - ", 0))
                ddlModulD.SelectedIndex = 0
            End Using


        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvLst_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvLst.SelectedIndexChanged
        Try
            Dim row As GridViewRow = gvLst.SelectedRow

            Dim strKodModul As String = CType(gvLst.SelectedRow.FindControl("lblKodModul"), Label).Text.TrimEnd
            Dim strKodSubModul As String = CType(gvLst.SelectedRow.FindControl("lblKodSubModul"), Label).Text.TrimEnd
            Dim strKodSubMenu As String = CType(gvLst.SelectedRow.FindControl("lblKodSubMenu"), Label).Text.TrimEnd
            Dim strNoStaf As String = CType(gvLst.SelectedRow.FindControl("lblNoStaf"), Label).Text.TrimEnd
            Dim strEmail As String = CType(gvLst.SelectedRow.FindControl("lblEmail"), Label).Text.TrimEnd
            Dim strKodTugas As String = CType(gvLst.SelectedRow.FindControl("lblKodTugas"), Label).Text.TrimEnd
            Dim strStatus As String = CType(gvLst.SelectedRow.FindControl("lblStatus"), Label).Text.TrimEnd

            ddlModulD.SelectedValue = strKodModul

            fBindDdlSubModul(strKodModul)
            ddlSubModulD.SelectedValue = strKodSubModul

            fBindDdlSubMenu(strKodSubModul)
            ddlSubMenuD.SelectedValue = strKodSubMenu

            ddlStaf.SelectedValue = strNoStaf
            txtEmail.Text = strEmail
            ddlTugas.SelectedValue = strKodTugas

            If strStatus = "Aktif" Then strStatus = True Else strStatus = False
            rbStatus.SelectedValue = strStatus



        Catch ex As Exception

        End Try
    End Sub

    Private Sub lbtnNew_Click(sender As Object, e As EventArgs) Handles lbtnNew.Click
        fBindDdlModul()
        ddlSubModulD.Items.Clear()
        ddlSubModulD.Items.Insert(0, New ListItem("- SILA PILIH MODUL- ", 0))
        ddlSubModulD.SelectedIndex = 0

        ddlSubMenuD.Items.Clear()
        ddlSubMenuD.Items.Insert(0, New ListItem("- SILA PILIH SUB MODUL- ", 0))
        ddlSubMenuD.SelectedIndex = 0

        ddlStaf.SelectedValue = 0
        txtEmail.Text = ""
        ddlTugas.SelectedValue = 0
        rbStatus.SelectedValue = True

    End Sub
End Class