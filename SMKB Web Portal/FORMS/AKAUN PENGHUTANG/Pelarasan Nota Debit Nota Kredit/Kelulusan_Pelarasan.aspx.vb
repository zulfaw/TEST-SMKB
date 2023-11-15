Imports System.Data.SqlClient
Public Class Kelulusan_Pelarasan
    Inherits System.Web.UI.Page

    Dim sLogBaru As String
    Dim sLogMedan As String
    Dim strNoStaf As String = UserInfo.strSesStaffID
    Dim sLogLevel As String = UserInfo.strUsrTahap
    Dim lsLogUsrIP As String = UserInfo.strUsrPcIP
    Dim lsLogUsrPC As String = UserInfo.strUsrPCName

    Private dbconn As New DBKewConn()
    Private dbconnSMSM As New DBSMConn()
    Dim ds As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Session("sesKodSubMenu") = Request.QueryString("KodSubMenu")

            fLoadKW()
            fLoadKO2()
            fLoadPTj2()
            fLoadKP2()
            fLoadVot2()
            fBindDdlFilStat()
            sLoadLst()

            Dim strNoStaf As String = Session("ssusrID")
            lblNoStafPel.Text = strNoStaf
            If Not String.IsNullOrEmpty(strNoStaf) Then
                Using dt = fGetUserInfo(strNoStaf)
                    lblJawPel.Text = dt.Rows(0)("JawGiliran").ToString
                    lblNoPTjPel.Text = dt.Rows(0)("KodPejabat").ToString & "0000"
                    lblNmPTjPel.Text = dt.Rows(0)("Pejabat").ToString
                    lblNmPel.Text = dt.Rows(0)("MS01_Nama").ToString
                End Using
            End If

            divList.Visible = True
            divWiz.Visible = False
        End If
    End Sub

    Private Sub fBindDdlFilStat()
        Dim dbconn As New DBKewConn
        Try
            Dim strSql As String = "select KodStatus, Butiran from AR_StatusDok where KodStatus in (
06,13,10,11)"

            Using ds = dbconn.fSelectCommand(strSql)
                ddlFilStat.DataSource = ds
                ddlFilStat.DataTextField = "Butiran"
                ddlFilStat.DataValueField = "KodStatus"
                ddlFilStat.DataBind()

                ddlFilStat.Items.Insert(0, New ListItem("- KESELURUHAN -", "0"))
                ddlFilStat.SelectedValue = "06"
            End Using



        Catch ex As Exception

        End Try
    End Sub

    Private Sub sLoadLst()
        Dim strFilter As String
        Dim intRec As Integer
        Dim strStatDok As String
        Try
            fClearGvLst()

            If ddlCarian.SelectedValue = 0 Then
                strFilter = ""
            ElseIf ddlCarian.SelectedValue = 1 Then
                strFilter = " and a.AR04_NoBil like '%" & Trim(txtCarian.Text.TrimEnd) & "%'"
            ElseIf ddlCarian.SelectedValue = 2 Then
                strFilter = " a.AR04_NoAdj like '%" & Trim(txtCarian.Text.TrimEnd) & "%''"
            End If

            If ddlFilStat.SelectedValue = 0 Then
                strStatDok = "06,10,11"
            Else
                strStatDok = ddlFilStat.SelectedValue
            End If

            Dim strSql As String = "Select A.AR04_NoBil, B.AR01_Tujuan, A.AR04_NoAdj, C.AR06_Tarikh as Tarikh_Bil, A.AR04_JumBesar, B.AR01_StatusDok, (Select D.Butiran from AR_StatusDok D where D.KodStatus = B.AR01_StatusDok) as ButStatusDok from AR04_BilAdj A
Inner Join AR01_Bil B on B.AR01_NoBil = A.AR04_NoBil
Inner Join AR06_statusdok C On C.AR06_StatusDok = B.AR01_StatusDok AND C.AR06_NoBil   = A.AR04_NoBil Where C.AR06_StatusDok in ('" & strStatDok & "') " & strFilter & " Order By AR04_NoAdj"

            Dim dbconn As New DBKewConn
            Dim ds As New DataSet
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    gvLst.DataSource = ds.Tables(0)
                    gvLst.DataBind()
                    intRec = ds.Tables(0).Rows.Count
                End If
            End If

            lblJumRekod.InnerText = intRec

        Catch ex As Exception

        End Try
    End Sub
    Private Sub fClearGvLst()
        gvLst.DataSource = New List(Of String)
        gvLst.DataBind()
    End Sub
    'Private Sub fLoadLampiran()
    '    Try
    '        '   Dim intRec As Integer

    '        Dim dt As New DataTable
    '        dt.Columns.Add("ar11_NamaDok", GetType(String))
    '        dt.Columns.Add("AR11_Bil", GetType(String))

    '        Dim strSql = "select ar11_id, ar11_NamaDok, AR11_Bil  from ar11_Lampiran"

    '        Dim ds = dbconn.fSelectCommand(strSql)

    '        Dim dvLampiran As New DataView
    '        dvLampiran = New DataView(ds.Tables(0))
    '        ViewState("dvLampiran") = dvLampiran.Table

    '        '  Dim strNamaDok, strBil As String

    '        If ds.Tables.Count > 0 Then
    '            gvLampiranDok.DataSource = ds
    '            gvLampiranDok.DataBind()
    '        End If

    '    Catch ex As Exception

    '    End Try
    'End Sub
    Protected Sub ViewFile(sender As Object, e As EventArgs)
        Dim id As Integer = Integer.Parse(TryCast(sender, LinkButton).CommandArgument)
        Try
            Dim url1 = Server.MapPath("~/")
            Dim url = Server.MapPath("~/FORMS/Akaun Penghutang/Permohon/FileAR.ashx")
            Dim fileExtension = ResolveUrl($"'{url}'?Id='{id}'")

            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "a", "window.open('" + fileExtension + "','_blank');", True)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub fLoadKW()
        Try

            Dim strSql As String = "Select KodKw,Butiran,(KodKw + ' - ' + Butiran ) as ButiranKW from MK_Kw order by KodKw"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)

            ViewState("dsKW") = ds

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fLoadKO2()
        Try
            Dim strSql As String = "SELECT DISTINCT TOP 100 PERCENT dbo.MK_kodOperasi.KodKO, (dbo.MK_kodOperasi.KodKO + ' - ' + dbo.MK_kodOperasi.Butiran) as Butiran " &
                "FROM dbo.MK_kodOperasi INNER JOIN dbo.MK01_VotTahun ON dbo.MK_kodOperasi.KodKO = dbo.MK01_VotTahun.KodKO " &
                "where dbo.MK01_VotTahun.MK01_Tahun = YEAR(GETDATE()) ORDER BY dbo.MK_kodOperasi.Kodko"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)
            ViewState("dsKO") = ds

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fLoadPTj2()
        Try
            Dim strSql As String = "SELECT DISTINCT   MK_PTJ.KodPTJ  , (MK_PTJ.KodPTJ   + ' - ' + MK_PTJ.Butiran  ) as Butiran  
                    From MK_PTJ INNER Join MK01_VotTahun ON MK_PTJ.KodPTJ   = MK01_VotTahun.KodPTJ   
                    where MK01_VotTahun.MK01_Tahun = YEAR(GETDATE()) and MK_PTJ.KodPTJ <> '-' and MK_PTJ.KodKategoriPTJ = 'P' and MK_PTJ.status = 1 
ORDER BY MK_PTJ.KodPTJ "

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)
            ViewState("dsPTj") = ds

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fLoadKP2()
        Try

            Dim strSql As String = "SELECT DISTINCT TOP 100 PERCENT MK_KodProjek.KodProjek as KodKP, (MK_KodProjek .KodProjek  + ' - ' + MK_KodProjek.Butiran ) as Butiran  
                    From MK_KodProjek INNER Join MK01_VotTahun ON MK_KodProjek.KodProjek  = MK01_VotTahun.KodKP
                    where MK01_VotTahun.MK01_Tahun = YEAR(GETDATE())  ORDER BY MK_KodProjek.KodProjek"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)
            ViewState("dsKP") = ds

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fLoadVot2()
        Try

            Dim strSql As String = "SELECT DISTINCT A.KodVot, (A.KODVOT + ' - ' + B.BUTIRAN) as Butiran FROM MK03_AmTahun A INNER JOIN MK_VOT B ON A.KODVOT = B.KODVOT 
            Where a.MK03_TAHUN = YEAR(GETDATE()) AND  RIGHT(A.KODVOT,2) <> '00' ORDER BY A.KODVOT"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)
            ViewState("dsVot") = ds


        Catch ex As Exception

        End Try
    End Sub
    Private Function fLoadKO(ByVal strKodKW As String) As DataSet
        Try

            'Dim strSql As String = "SELECT DISTINCT TOP 100 PERCENT dbo.MK_kodOperasi.KodKO, (dbo.MK_kodOperasi.KodKO + ' - ' + dbo.MK_kodOperasi.Butiran) as ButiranKO " &
            '    "FROM dbo.MK_kodOperasi INNER JOIN dbo.MK01_VotTahun ON dbo.MK_kodOperasi.KodKO = dbo.MK01_VotTahun.KodKO " &
            '    "where dbo.MK01_VotTahun.MK01_Tahun = '" & Now.Year & "' and dbo.mk01_votTahun.KodKw = '" & ddlKW.SelectedValue & "' and dbo.mk01_votTahun.KodPTj = '" & ddlPTJ.SelectedValue & "' ORDER BY dbo.MK_kodOperasi.Kodko"

            Dim strSql As String = "SELECT DISTINCT TOP 100 PERCENT dbo.MK_kodOperasi.KodKO, (dbo.MK_kodOperasi.KodKO + ' - ' + dbo.MK_kodOperasi.Butiran) as ButiranKO " &
                "FROM dbo.MK_kodOperasi INNER JOIN dbo.MK01_VotTahun ON dbo.MK_kodOperasi.KodKO = dbo.MK01_VotTahun.KodKO " &
                "where dbo.MK01_VotTahun.MK01_Tahun = YEAR(GETDATE()) and dbo.mk01_votTahun.KodKw = '" & strKodKW & "' ORDER BY dbo.MK_kodOperasi.Kodko"

            'Dim strSql As String = "SELECT DISTINCT TOP 100 PERCENT dbo.MK_kodOperasi.KodKO, (dbo.MK_kodOperasi.KodKO + ' - ' + dbo.MK_kodOperasi.Butiran) as ButiranKO " &
            '    "FROM dbo.MK_kodOperasi INNER JOIN dbo.MK01_VotTahun ON dbo.MK_kodOperasi.KodKO = dbo.MK01_VotTahun.KodKO " &
            '    "where dbo.MK01_VotTahun.MK01_Tahun = '" & Now.Year & "' ORDER BY dbo.MK_kodOperasi.Kodko"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)
            'ViewState("dsKO") = ds
            Return ds

        Catch ex As Exception

        End Try
    End Function
    Private Function fLoadPTj(ByVal strKodKW As String, ByVal strKodKO As String) As DataSet
        Try


            Dim strSql As String = "SELECT DISTINCT   MK_PTJ.KodPTJ  , (MK_PTJ.KodPTJ   + ' - ' + MK_PTJ.Butiran  ) as ButiranPTj  
                    From MK_PTJ INNER Join MK01_VotTahun ON MK_PTJ.KodPTJ   = MK01_VotTahun.KodPTJ   
                    where MK01_VotTahun.MK01_Tahun = YEAR(GETDATE()) and MK01_VotTahun.KodKw = '" & strKodKW & "' and MK01_VotTahun.KodKO = '" & strKodKO & "' and MK_PTJ.KodPTJ <> '-' and MK_PTJ.KodKategoriPTJ = 'P' and MK_PTJ.status = 1 
ORDER BY MK_PTJ.KodPTJ "

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)
            'ViewState("dsPTj") = ds
            Return ds

        Catch ex As Exception

        End Try
    End Function

    Private Function fLoadKP(ByVal strKodKW As String, ByVal strKodKO As String, ByVal strKodPTj As String) As DataSet
        Try

            Dim strSql As String = "SELECT DISTINCT TOP 100 PERCENT MK_KodProjek.KodProjek , (MK_KodProjek .KodProjek  + ' - ' + MK_KodProjek.Butiran ) as Butiran  
                    From MK_KodProjek INNER Join MK01_VotTahun ON MK_KodProjek.KodProjek  = MK01_VotTahun.KodKP
                    where MK01_VotTahun.MK01_Tahun = YEAR(GETDATE()) and MK01_VotTahun.KodKw = '" & strKodKW & "' and MK01_VotTahun.KodKO = '" & strKodKO & "' and MK01_VotTahun .KodPTJ = '" & strKodPTj & "' ORDER BY MK_KodProjek.KodProjek"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)
            'ViewState("dsKP") = ds
            Return ds

        Catch ex As Exception

        End Try
    End Function

    Private Function fLoadVot(ByVal strKodKW As String, ByVal strKodKO As String, ByVal strKodPTj As String, ByVal strKodKP As String) As DataSet
        Try

            Dim strSql As String = "SELECT DISTINCT A.KodVot, (A.KODVOT + ' - ' + B.BUTIRAN) as Butiran FROM MK03_AmTahun A INNER JOIN MK_VOT B ON A.KODVOT = B.KODVOT 
                                    Where a.MK03_TAHUN = YEAR(GETDATE()) AND A.KodKw = '" & strKodKW & "' and A.KodKO = '" & strKodKO & "' AND A.KODPTJ = '" & strKodPTj & "' AND A.KodKP  = '" & strKodKP & "' 
                                    AND  RIGHT(A.KODVOT,2) <> '00' ORDER BY A.KODVOT"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)
            'ViewState("dsKP") = ds
            Return ds

        Catch ex As Exception

        End Try
    End Function
    Protected Sub ddlKW_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try

            Dim gvr As GridViewRow = CType(((CType(sender, Control)).NamingContainer), GridViewRow)
            Dim ddlKW As DropDownList = CType(gvr.FindControl("ddlKW"), DropDownList)
            Dim strKodKW As String = ddlKW.SelectedItem.Value

            Dim ddlKO = DirectCast(gvr.FindControl("ddlKO"), DropDownList)
            ddlKO.DataSource = fLoadKO(strKodKW)   'ViewState("dsKO")
            ddlKO.DataTextField = "ButiranKO"
            ddlKO.DataValueField = "KodKO"
            ddlKO.DataBind()

            ddlKO.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlKO.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub ddlKO_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try

            Dim gvr As GridViewRow = CType(((CType(sender, Control)).NamingContainer), GridViewRow)
            Dim ddlKW As DropDownList = CType(gvr.FindControl("ddlKW"), DropDownList)
            Dim strKodKW As String = ddlKW.SelectedItem.Value

            Dim ddlKO As DropDownList = CType(gvr.FindControl("ddlKO"), DropDownList)
            Dim strKodKO As String = ddlKO.SelectedItem.Value

            Dim ddlPTj = DirectCast(gvr.FindControl("ddlPTj"), DropDownList)
            ddlPTj.DataSource = fLoadPTj(strKodKW, strKodKO)   'ViewState("dsKO")
            ddlPTj.DataTextField = "ButiranPTj"
            ddlPTj.DataValueField = "KodPTj"
            ddlPTj.DataBind()

            ddlPTj.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlPTj.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub ddlPTj_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try

            Dim gvr As GridViewRow = CType(((CType(sender, Control)).NamingContainer), GridViewRow)
            Dim ddlKW As DropDownList = CType(gvr.FindControl("ddlKW"), DropDownList)
            Dim strKodKW As String = ddlKW.SelectedItem.Value

            Dim ddlKO As DropDownList = CType(gvr.FindControl("ddlKO"), DropDownList)
            Dim strKodKO As String = ddlKO.SelectedItem.Value

            Dim ddlPTj As DropDownList = CType(gvr.FindControl("ddlPTj"), DropDownList)
            Dim strKodPTj As String = ddlPTj.SelectedItem.Value

            Dim ddlKP = DirectCast(gvr.FindControl("ddlKP"), DropDownList)
            ddlKP.DataSource = fLoadKP(strKodKW, strKodKO, strKodPTj)   'ViewState("dsKO")
            ddlKP.DataTextField = "Butiran"
            ddlKP.DataValueField = "KodProjek"
            ddlKP.DataBind()

            ddlKP.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlKP.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlKP_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try

            Dim gvr As GridViewRow = CType(((CType(sender, Control)).NamingContainer), GridViewRow)
            Dim ddlKW As DropDownList = CType(gvr.FindControl("ddlKW"), DropDownList)
            Dim strKodKW As String = ddlKW.SelectedItem.Value

            Dim ddlKO As DropDownList = CType(gvr.FindControl("ddlKO"), DropDownList)
            Dim strKodKO As String = ddlKO.SelectedItem.Value

            Dim ddlPTj As DropDownList = CType(gvr.FindControl("ddlPTj"), DropDownList)
            Dim strKodPTj As String = ddlPTj.SelectedItem.Value

            Dim ddlKP As DropDownList = CType(gvr.FindControl("ddlKP"), DropDownList)
            Dim strKodKP As String = ddlKP.SelectedItem.Value

            Dim ddlVot = DirectCast(gvr.FindControl("ddlVot"), DropDownList)
            ddlVot.DataSource = fLoadVot(strKodKW, strKodKO, strKodPTj, strKodKP)   'ViewState("dsKO")
            ddlVot.DataTextField = "Butiran"
            ddlVot.DataValueField = "KodVot"
            ddlVot.DataBind()

            ddlVot.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlVot.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txtKuantiti_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim kuantiti = CType(CType(sender, Control), TextBox).Text
            Dim gvRow As GridViewRow = CType(CType(sender, Control), TextBox).Parent.Parent
            Dim txtHarga As TextBox = CType(gvRow.FindControl("txtHarga"), TextBox)
            Dim lblJumlah As Label = CType(gvRow.FindControl("lblJumlah"), Label)
            ' If txtHarga.Text IsNot String.Empty Then
            If txtHarga.Text = "" Then txtHarga.Text = 0
            Dim angHrgSeunit = CDec(txtHarga.Text)
            Dim JumAngHrg = CDec(CInt(kuantiti) * angHrgSeunit)
            lblJumlah.Text = FormatNumber(JumAngHrg, 2) 'JumAngHrg.ToString("N2")
            '  End If

            fSetFooter()

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub txtHarga_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim HrgUnit = CType(CType(sender, Control), TextBox).Text
            Dim gvRow As GridViewRow = CType(CType(sender, Control), TextBox).Parent.Parent
            Dim txtKuantiti As TextBox = CType(gvRow.FindControl("txtKuantiti"), TextBox)
            Dim lblJum As Label = CType(gvRow.FindControl("lblJum"), Label)

            '  If txtKuantiti.Text IsNot String.Empty Then
            Dim angHrgSeunit = CDec(HrgUnit)
            Dim JumAngHrg As Decimal = 0.00

            JumAngHrg = CDec(CInt(txtKuantiti.Text) * angHrgSeunit)
            lblJum.Text = FormatNumber(JumAngHrg, 2) 'JumAngHrg.ToString("N2")
            ' End If

            fSetFooter()

        Catch ex As Exception

        End Try

    End Sub
    Private Sub fSetFooter()
        Try
            Dim decJumTrans As Decimal
            For Each gvRow1 As GridViewRow In gvTrans.Rows
                Dim decJumlah As String = CDec(Trim(CType(gvRow1.FindControl("lblJum"), Label).Text.TrimEnd))
                decJumTrans += decJumlah
            Next

            Dim footerRow = gvTrans.FooterRow
            CType(footerRow.FindControl("lblTotJum"), Label).Text = FormatNumber(decJumTrans, 2)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub txtJumlah_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim decJumTrans As Decimal
            For Each gvRow As GridViewRow In gvTransAsal.Rows
                Dim decJumlah As String = CDec(Trim(CType(gvRow.FindControl("txtJumlah"), TextBox).Text.TrimEnd))
                decJumTrans += decJumlah
            Next

            Dim footerRow = gvTransAsal.FooterRow
            CType(footerRow.FindControl("lblTotJum"), Label).Text = FormatNumber(decJumTrans, 2)


        Catch ex As Exception

        End Try
    End Sub

    Private Sub lbtnCari_Click(sender As Object, e As EventArgs) Handles lbtnCari.Click
        sLoadLst()
    End Sub


    Private Sub fBindInvAdj(strNoInvCuk As String)



        Try

            Dim strSql As String = "select A.AR04_NoBil, A.AR04_NoAdj, A.AR04_NoAdjSem, B.AR01_KodPTJMohon, (Select MK_PTJ.Butiran from MK_PTJ where MK_PTJ.KodPTJ = B.AR01_KodPTJMohon ) as PTjMohon, B.AR01_NoStaf, B.AR01_Jenis, (select D.Butiran from AR_Jenis D where D.Kod = B.AR01_Jenis) as ButJenis, B.AR01_KodBank,
B.AR01_NORUJUKAN, B.AR01_ALMT1, B.AR01_ALMT2, B.AR01_KATEGORI, (select E.Butiran from MK_KategoriPenerima E where E.Kod = B.AR01_Kategori) as ButKat, B.AR01_NAMAPENERIMA, B.kodnegara, (Select G.Butiran from MK_Negara G where G.KodNegara  = B.KodNegara) as ButNegara, B.kodnegeri, (Select F.Butiran from MK_Negeri F where F.KodNegeri = B.KodNegeri) as ButNegeri, B.AR01_IDPENERIMA, B.AR01_TUJUAN, B.AR01_BANDAR, B.AR01_POSKOD, B.AR01_TkhKonDari, B.AR01_TkhKonHingga, B.AR01_NOTEL, B.AR01_NOFAKS, B.AR01_TempohKontrak, B.AR01_TkhPeringatan1, B.AR01_TkhPeringatan2, B.AR01_TkhPeringatan3, B.AR01_TkhLulus, B.AR01_Emel, B.AR01_UtkPerhatian, B.AR01_DokSok, B.AR01_FlagAdj, C.AR06_Tarikh as Tarikh_Bil, B.AR01_JUMLAH       
from AR04_BilAdj A
inner join AR01_Bil B on B.AR01_NoBil = A.AR04_NoBil
inner join AR06_StatusDok C on C.AR06_NoBil = A.AR04_NoBil AND C.AR06_StatusDok = B.AR01_StatusDok 
where  A.AR04_NoBil = '" & strNoInvCuk & "' "

            Dim ds As New DataSet
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Using dtAdj = ds.Tables(0)

                        lblNoPelSem.Text = dtAdj.Rows(0)("AR04_NoAdjSem")
                        lblKodPTJ.Text = dtAdj.Rows(0)("AR01_KODPTJMOHON")
                        lblNamaPTJ.Text = dtAdj.Rows(0)("PTjMohon")
                        Dim NoPmhn As String = dtAdj.Rows(0)("AR01_NOSTAF")
                        Dim strJenis As String = dtAdj.Rows(0)("AR01_Jenis")
                        Dim strButJenis As String = dtAdj.Rows(0)("ButJenis")
                        lblKodBank.Text = dtAdj.Rows(0)("AR01_KODBANK")
                        lblNoRuj.Text = IIf(IsDBNull(dtAdj.Rows(0)("AR01_NORUJUKAN")), Nothing, dtAdj.Rows(0)("AR01_NORUJUKAN"))
                        lblAlmt1.Text = dtAdj.Rows(0)("AR01_ALMT1")
                        lblAlmt2.Text = dtAdj.Rows(0)("AR01_ALMT2")
                        Dim strKat As String = dtAdj.Rows(0)("AR01_KATEGORI")
                        Dim strButKat As String = dtAdj.Rows(0)("ButKat")

                        Dim NPenerima As String = dtAdj.Rows(0)("AR01_NAMAPENERIMA")
                        lblKodNegara.Text = dtAdj.Rows(0)("kodnegara")
                        lblButNegara.Text = dtAdj.Rows(0)("ButNegara")
                        lblKodNegeri.Text = dtAdj.Rows(0)("kodnegeri")
                        Dim strButNegeri As String = dtAdj.Rows(0)("ButNegeri")
                        lblButNegeri.Text = strButNegeri

                        lblIDPenerima.Text = dtAdj.Rows(0)("AR01_IDPENERIMA")
                        txtTujuan.Text = dtAdj.Rows(0)("AR01_TUJUAN")
                        lblBandar.Text = dtAdj.Rows(0)("AR01_BANDAR")
                        lblPoskod.Text = dtAdj.Rows(0)("AR01_POSKOD")
                        lblNoTel.Text = dtAdj.Rows(0)("AR01_NOTEL")
                        lblNoFaks.Text = dtAdj.Rows(0)("AR01_NOFAKS")
                        lblEmel.Text = dtAdj.Rows(0)("AR01_Emel")
                        lblPerhatian.Text = dtAdj.Rows(0)("AR01_UtkPerhatian")
                        Dim TkhLulus As String = IIf(IsDBNull(dtAdj.Rows(0)("AR01_TkhLulus")), Nothing, dtAdj.Rows(0)("AR01_TkhLulus"))
                        Dim DokSok As String = IIf(IsDBNull(dtAdj.Rows(0)("AR01_DokSok")), Nothing, dtAdj.Rows(0)("AR01_DokSok"))

                        lblNoInv.Text = strNoInvCuk
                        lblJenis.Text = strJenis
                        lblButJenis.Text = strButJenis
                        lblKat.Text = strKat
                        lblButKat.Text = strButKat

                        lblNmPenerima.Text = NPenerima

                        lblNoStafPmhn.Text = NoPmhn
                        If Not String.IsNullOrEmpty(NoPmhn) Then
                            Using dt = fGetUserInfo(NoPmhn)
                                lblJwtanMhn.Text = dt.Rows(0)("JawGiliran").ToString
                                lblKodPTjPmhn.Text = dt.Rows(0)("KodPejabat").ToString & "0000"
                                lblNmPTjPmhn.Text = dt.Rows(0)("Pejabat").ToString
                                lblNmPmhn.Text = dt.Rows(0)("MS01_Nama").ToString
                            End Using
                        End If

                        hidJumAsal.Value = dtAdj.Rows(0)("AR01_JUMLAH")

                        strSql = "Select AR04_BilPel, ar04_jumbesar from ar04_biladj Where ar04_noadjsem='" & Me.lblNoPelSem.Text & "'"

                        Using ds2 = dbconn.fSelectCommand(strSql)
                            If Not ds2 Is Nothing Then
                                If ds2.Tables(0).Rows.Count > 0 Then
                                    hidJumBaki.Value = ds2.Tables(0).Rows(0)("ar04_jumbesar")
                                    hidBilPel.Value = ds2.Tables(0).Rows(0)("AR04_BilPel")
                                End If
                            End If

                        End Using

                    End Using
                End If
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub fBindTrans(strNoInv As String)
        Try
            Dim strSql = $"Select AR04_NoAdjSem , ar07_noadj, ar07_nobil, ar07_bil,kodkw ,kodko, kodptj, kodkp,kodvot, AR07_Perkara AS AR01_Perkara , AR07_Kuantiti AS AR01_Kuantiti, AR07_KadarHarga AS AR01_KadarHarga , ar07_jumlah AS AR01_Jumlah, AR07_BilPel , ar07_petunjuk from AR07_Bezaadj
where AR07_NoBil = '" & strNoInv & "';"

            Dim dbconn As New DBKewConn
            Dim ds As New DataSet
            ds = dbconn.fSelectCommand(strSql)
            Using dt = ds.Tables(0)
                gvTrans.DataSource = dt
                gvTrans.DataBind()

                gvTransPel.DataSource = dt
                gvTransPel.DataBind()

                ViewState("vsDtInv") = dt
            End Using

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindTransAsal(strNoInvCuk As String)

        Try
            Dim strSql As String = "select KodKw,KodKO,KodPTJ,KodKP,KodVot,AR01_Perkara,AR01_Kuantiti,AR01_kadarHarga,AR01_Jumlah from AR01_BilDt where AR01_NoBil='" & strNoInvCuk & "';"

            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Using dt = ds.Tables(0)
                        gvTransAsal.DataSource = dt
                        gvTransAsal.DataBind()
                    End Using

                End If
            End If


        Catch ex As Exception

        End Try


    End Sub

    Dim strTotJumlah As String
    Dim decJumlah As Decimal

    Private Sub lbtnLulus_Click(sender As Object, e As EventArgs) Handles lbtnLulus.Click
        Dim dbConn As New DBKewConn
        Try

            Dim strNoAdj As String = Trim(txtNoAdj.Text.TrimEnd)
            If String.IsNullOrEmpty(strNoAdj) Then
                If fSimpan() Then
                    fGlobalAlert("permohonan pelarasan ini telah diluluskan!", Me.Page, Me.[GetType]())
                    lbtnLulus.Visible = False
                    lbtnXLulus.Visible = False
                    Exit Sub
                Else
                    fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
                    Exit Sub
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Function fSimpan() As Boolean
        Dim blnSuccess As Boolean = True
        Dim dbconn As New DBKewConn
        Dim ds As New DataSet
        Dim strNoPel As String
        Dim strTkhToday As String = Now.ToString

        'Dim dtTkhInv As Date = DateTime.ParseExact(Trim(txtTkhInv.Text.TrimEnd), "dd/MM/yyyy", CultureInfo.CurrentCulture)
        'Dim strTkhInv As String = dtTkhInv.ToString("yyyy-MM-dd")

        Try
            Dim strSql As String
            Dim strNoPelSem As String = Trim(lblNoPelSem.Text.TrimEnd)
            Dim tkhBil As String = Trim(lblTkhBil.Text.Trim)
            Dim dtTkhToday As DateTime = CDate(strTkhToday)

            Dim strPerhatian As String = Trim(lblPerhatian.Text.TrimEnd)
            Dim strNoRujuk As String = Trim(lblNoRuj.Text.TrimEnd)
            Dim IDPenerima As String = Trim(lblIDPenerima.Text.TrimEnd)
            Dim strAlamat1 As String = Trim(lblAlmt1.Text.TrimEnd)
            Dim strAlamat2 As String = Trim(lblAlmt2.Text.TrimEnd)
            Dim strBdr As String = Trim(lblBandar.Text.TrimEnd)
            Dim strPoskod As String = Trim(lblPoskod.Text.TrimEnd)
            Dim strEmel As String = Trim(lblEmel.Text.TrimEnd)
            Dim strNoTel As String = Trim(lblNoTel.Text.TrimEnd)

            Dim strKodPTJ As String = Trim(lblKodPTJ.Text.TrimEnd)
            Dim strStatDok As String = "10"
            Dim strKodDok As String = "ADJ_BIL"
            Dim strKodAP As String = "-"
            Dim strNoStaf As String = Trim(lblNoStafPel.Text.TrimEnd)


            dbconn.sConnBeginTrans()

            Dim paramSql() As SqlParameter

            strSql = "delete from AR07_BezaAdj Where AR04_NoAdjSem = @NoAdjSem"
            paramSql =
                       {
                       New SqlParameter("@NoAdjSem", strNoPelSem)
                       }

            If Not dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                blnSuccess = False
                Exit Try
            Else
                'AUDIT LOG
                sLogMedan = "AR04_NoAdjSem"
                sLogBaru = strNoPelSem

                strSql = "insert into MK_AuditLog (UserID, UserGroup, UserUbah, SubMenu, Keterangan, InfoTable, RefKey, RefNo, InfoMedan, InfoBaru, " _
    & " InfoLama, UserIP, PCName) " _
    & " values (@UserID,@UserGroup,@UserUbah,@SubMenu,@Keterangan,@InfoTable,@RefKey, @RefNo,@InfoMedan,@InfoBaru,@InfoLama, " _
    & " @UserIP,@PCName)"

                Dim paramSqlLog() As SqlParameter = {
                    New SqlParameter("@UserID", strNoStaf),
                    New SqlParameter("@UserGroup", sLogLevel),
                    New SqlParameter("@UserUbah", "-"),
                     New SqlParameter("@SubMenu", Session("sesKodSubMenu")),
                    New SqlParameter("@Keterangan", "DELETE"),
                    New SqlParameter("@InfoTable", "AR07_BezaAdj"),
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

            strSql = "delete from AR04_BilAdj Where AR04_NoAdjSem = @NoAdjSem"
            paramSql =
                       {
                       New SqlParameter("@NoAdjSem", strNoPelSem)
                       }

            If Not dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                blnSuccess = False
                Exit Try
            Else
                'AUDIT LOG
                sLogMedan = "AR04_NoAdjSem"
                sLogBaru = strNoPelSem

                strSql = "insert into MK_AuditLog (UserID, UserGroup, UserUbah, SubMenu, Keterangan, InfoTable, RefKey, RefNo, InfoMedan, InfoBaru, " _
    & " InfoLama, UserIP, PCName) " _
    & " values (@UserID,@UserGroup,@UserUbah,@SubMenu,@Keterangan,@InfoTable,@RefKey, @RefNo,@InfoMedan,@InfoBaru,@InfoLama, " _
    & " @UserIP,@PCName)"

                Dim paramSqlLog() As SqlParameter = {
                    New SqlParameter("@UserID", strNoStaf),
                    New SqlParameter("@UserGroup", sLogLevel),
                    New SqlParameter("@UserUbah", "-"),
                     New SqlParameter("@SubMenu", Session("sesKodSubMenu")),
                    New SqlParameter("@Keterangan", "DELETE"),
                    New SqlParameter("@InfoTable", "AR04_BilAdj"),
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

            Dim strPrefix, strButiranNoAkhir
            Dim baki = hidJumBaki.Value
            Dim decJumBaki As Decimal = CDec(hidJumBaki.Value)
            Dim JumAsal As Decimal = CDec(hidJumAsal.Value)

            If decJumBaki > JumAsal Then
                strButiranNoAkhir = "Max Nota Debit AR"
                strPrefix = "ND"
            Else
                strButiranNoAkhir = "Max Nota Kredit AR"
                strPrefix = "KD"
            End If

            Dim strTahun As String = Now.Year
            Dim strKodModul = "AR"

            strNoPel = fGetNo(strTahun, strKodModul, strPrefix, strButiranNoAkhir)

            If strNoPel Is Nothing Then
                Exit Function
            End If

            Dim strNoInv As String = Trim(lblNoInv.Text.TrimEnd)

            strSql = $"UPDATE ar01_bil set ar01_statusdok= @StatDok,ar01_flagadj= @flagAdj,ar01_jumblmbyr= @jumBlmByr where ar01_nobil= @NoInv"
            paramSql =
                       {
                       New SqlParameter("@StatDok", strStatDok),
                       New SqlParameter("@flagAdj", 1),
                       New SqlParameter("@jumBlmByr", decJumBaki),
                       New SqlParameter("@NoInv", strNoInv)
                       }

            If Not dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                blnSuccess = False
                Exit Try
            Else
                'AUDIT LOG
                sLogMedan = "ar01_statusdok|ar01_flagadj|ar01_jumblmbyr|ar01_nobil"
                sLogBaru = strStatDok & "|1|" & decJumBaki & "|" & strNoInv & ""

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
                    New SqlParameter("@InfoTable", "ar01_bil"),
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


            Dim strUlasBeza As String = Trim(txtUlasan.Text.TrimEnd)
            Dim intBilPel As Integer = CInt(hidBilPel.Value)
            Dim strTujuan As String = Trim(txtTujuan.Text.TrimEnd)

            strSql = "INSERT INTO AR04_BilAdj(AR04_NoBil,AR04_NoAdj,AR04_NoAdjsem,AR04_Ulasan,AR04_TkhAdj,AR04_JumBesar,AR04_BilPel,AR04_Tujuan,AR04_JUMBLMBYR)" _
         & " Values (@NoInv, @NoAdj, @NoAdjSem, @Ulasan, @TkhAdj, @JumBesar, @BilPel, @Tujuan, @JumBaki)"


            Dim paramSql1() As SqlParameter =
                {
                New SqlParameter("@NoInv", strNoInv),
                New SqlParameter("@NoAdj", strNoPel),
                New SqlParameter("@NoAdjSem", strNoPelSem),
                New SqlParameter("@Ulasan", strUlasBeza),
                New SqlParameter("@TkhAdj", dtTkhToday),
                New SqlParameter("@JumBesar", decJumBaki),
                New SqlParameter("@BilPel", intBilPel),
                New SqlParameter("@Tujuan", strTujuan),
                 New SqlParameter("@JumBaki", decJumBaki)
                }

            If Not dbconn.fInsertCommand(strSql, paramSql1) > 0 Then
                blnSuccess = False
                Exit Try
            Else
                'AUDIT LOG
                sLogMedan = "AR04_NoBil|AR04_NoAdj|AR04_NoAdjsem|AR04_Ulasan|AR04_TkhAdj|AR04_JumBesar|AR04_BilPel|AR04_Tujuan|AR04_JUMBLMBYR"
                sLogBaru = strNoInv & "|" & strNoPel & "|" & strNoPelSem & "|" & strUlasBeza & "|" & dtTkhToday & "|" & decJumBaki & "|" & intBilPel & "|" & strTujuan & "|" & decJumBaki

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
                    New SqlParameter("@InfoTable", "AR04_BilAdj"),
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

            Dim decJumDt, decTotJum As Decimal
            For Each gvrow As GridViewRow In gvTransAsal.Rows

                decJumDt = CDec(TryCast(gvrow.FindControl("lblJum"), Label).Text)
                decTotJum += decJumDt
            Next

            Dim intBil As Integer
            Dim intBlnTrans As Integer = Month(dtTkhToday)
            Dim intThnTrans As Integer = Year(dtTkhToday)

            For Each gvRow As GridViewRow In gvTrans.Rows
                intBil = intBil + 1
                Dim strKodKW As String = TryCast(gvRow.FindControl("ddlKW"), DropDownList).SelectedValue
                Dim strKodKO As String = TryCast(gvRow.FindControl("ddlKO"), DropDownList).SelectedValue
                Dim strPTJ As String = TryCast(gvRow.FindControl("ddlPTj"), DropDownList).SelectedValue
                Dim strKodKP As String = TryCast(gvRow.FindControl("ddlKP"), DropDownList).SelectedValue
                Dim strKodVot As String = TryCast(gvRow.FindControl("ddlVot"), DropDownList).SelectedValue
                Dim strPerkara As String = TryCast(gvRow.FindControl("txtPerkara"), TextBox).Text
                Dim strPetunjuk As String = TryCast(gvRow.FindControl("ddlPetunjuk"), DropDownList).SelectedValue
                Dim decKuantiti As Decimal = CDec(TryCast(gvRow.FindControl("txtKuantiti"), TextBox).Text)
                Dim decKdrHrg As Decimal = CDec(TryCast(gvRow.FindControl("txtHarga"), TextBox).Text)
                Dim decJum As Decimal = CDec(TryCast(gvRow.FindControl("lblJum"), Label).Text)
                ' decDebit = 0

                strSql = "INSERT INTO AR07_BEZAADJ(AR07_NoAdj,AR04_NoAdjSem,AR07_Bil,KodKw,KodKo,KodPTJ, KodKP, KodVot,AR07_Perkara, AR07_Petunjuk,AR07_Kuantiti,AR07_kadarHarga,AR07_Jumlah,AR07_NoBil,AR07_BilPel,AR04_Bil,AR07_Kriteria,VotGST, JenTax, KodTax, PtjGST, KwGST, AktGST, AR07_JumGST, AR07_JumTanpaGST)" &
                          "values (@NoAdj, @NoAdjSem ,@Bil,@KodKw,@KodKo, @KodPTJ, @KodKP, @KodVot, @Perkara, @Petunjuk,@Kuantiti, @kadarHarga, @Jum, @NoBil,@BilPel,@AR04Bil, @Kriteria,
                          @VotGST, @JenTax, @KodTax, @PtjGST, @KwGST, @AktGST, @JumGST, @JumTanpaGST)"

                paramSql =
                    {
                New SqlParameter("@NoAdj", strNoPel),
                New SqlParameter("@NoAdjSem", strNoPelSem),
                New SqlParameter("@Bil", intBil),
                New SqlParameter("@KodKw", strKodKW),
                New SqlParameter("@KodKo", strKodKO),
                New SqlParameter("@KodPTJ", strPTJ),
                New SqlParameter("@KodKP", strKodKP),
                New SqlParameter("@KodVot", strKodVot),
                New SqlParameter("@Perkara", strPerkara),
                New SqlParameter("@Petunjuk", strPetunjuk),
                New SqlParameter("@Kuantiti", decKuantiti),
                New SqlParameter("@kadarHarga", decKdrHrg),
                New SqlParameter("@Jum", decJum),
                New SqlParameter("@NoBil", strNoInv),
                New SqlParameter("@BilPel", intBilPel),
                New SqlParameter("@AR04Bil", intBil),
                New SqlParameter("@Kriteria", ""),
                New SqlParameter("@VotGST", ""),
                New SqlParameter("@JenTax", "SUP"),
                New SqlParameter("@KodTax", ""),
                New SqlParameter("@PtjGST", ""),
                New SqlParameter("@KwGST", ""),
                New SqlParameter("@AktGST", ""),
                New SqlParameter("@JumGST", 0.00),
                New SqlParameter("@JumTanpaGST", 0.00)
                    }

                If Not dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                    blnSuccess = False
                    Exit Try
                Else
                    'AUDIT LOG
                    sLogMedan = "AR07_NoAdj|AR04_NoAdjSem|AR07_Bil|KodKw|KodKo|KodPTJ| KodKP| KodVot|AR07_Perkara| AR07_Petunjuk|AR07_Kuantiti|AR07_kadarHarga|AR07_Jumlah|AR07_NoBil|AR07_BilPel|AR04_Bil|AR07_Kriteria|VotGST| JenTax| KodTax| PtjGST| KwGST| AktGST| AR07_JumGST| AR07_JumTanpaGST"
                    sLogBaru = strNoPel & "|" & strNoPelSem & "|" & intBil & "|" & strKodKW & "|" & strKodKO & "|" & strPTJ & "|" & strKodKP & "|" & strKodVot & "|" & strPerkara & "|" & strPetunjuk & "|" & decKuantiti & "|" & decKdrHrg & "|" & decJum & "|" & strNoInv & "|" & intBilPel & "|||SUP|||||0.00|0.00"

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
                        New SqlParameter("@InfoTable", "AR07_BEZAADJ"),
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

                strSql = "select kodjen,kodjenlanjut from mk_vot where kodvot='" & strKodVot & "'"

                Dim strKodjen, strKodjenLanjut As String
                Using ds1 = fQuery(strSql)
                    strKodjen = ds1.Tables(0).Rows(0)("kodjen")
                    strKodjenLanjut = ds1.Tables(0).Rows(0)("kodjenlanjut")
                End Using

                Dim decDebit, decKredit As Decimal
                If strPetunjuk = "+" Then
                    decDebit = 0
                    decKredit = decJum
                ElseIf strPetunjuk = "-" Then
                    decDebit = decJum
                    decKredit = 0
                End If

                strSql = "INSERT INTO mk06_transaksi (mk06_tkhtran,mk06_rujukan,mk06_nodok,mk06_butiran,koddok,mk06_kodap,mk06_bil,kodkw, KodKO,kodptj,KodKP,kodvot,kodjen,kodjenlanjut,mk06_debit,mk06_kredit,mk06_bulan,mk06_tahun,mk06_status)
VALUES (@TkhTrans, @Rujukan,@NoDok, @Butiran, @KodDok, @KodAP, @Bil, @KodKW, @KodKO, @KodPTj, @KodKP, @KodVot, @KodJen,@KodJenLanjut, @Debit, @Kredit, @BulanTrans, @TahunTrans, @Status)"

                paramSql =
                {
            New SqlParameter("@TkhTrans", dtTkhToday),
            New SqlParameter("@Rujukan", strNoInv),
            New SqlParameter("@NoDok", strNoPel),
            New SqlParameter("@Butiran", strPerkara),
            New SqlParameter("@KodDok", strKodDok),
            New SqlParameter("@KodAP", strKodAP),
            New SqlParameter("@Bil", intBil),
            New SqlParameter("@KodKW", strKodKW),
            New SqlParameter("@KodKO", strKodKO),
            New SqlParameter("@KodPTj", strKodPTJ),
            New SqlParameter("@KodKP", strKodKP),
            New SqlParameter("@KodVot", strKodVot),
            New SqlParameter("@KodJen", strKodjen),
            New SqlParameter("@KodJenLanjut", strKodjenLanjut),
            New SqlParameter("@Debit", decDebit),
            New SqlParameter("@Kredit", decKredit),
            New SqlParameter("@BulanTrans", intBlnTrans),
            New SqlParameter("@TahunTrans", intThnTrans),
            New SqlParameter("@Status", 0)
                }

                If Not dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                    blnSuccess = False
                    Exit Try
                Else
                    'AUDIT LOG
                    sLogMedan = "mk06_tkhtran|mk06_rujukan|mk06_nodok|mk06_butiran|koddok|mk06_kodap|mk06_bil|kodkw|KodKO|kodptj|KodKP|kodvot|kodjen|kodjenlanjut|mk06_debit|mk06_kredit|mk06_bulan|mk06_tahun|mk06_status"
                    sLogBaru = dtTkhToday & "|" & strNoInv & "|" & strNoPel & "|" & strPerkara & "|" & strKodDok & "|" & strKodAP & "|" & intBil & "|" & strKodKW & "|" & strKodKO & "|" & strKodPTJ & "|" & strKodKP & "|" & strKodVot & "|" & strKodjen & "|" & strKodjenLanjut & "|" & decDebit & "|" & decKredit & "|" & intBlnTrans & "|" & intThnTrans & "|0"

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
                        New SqlParameter("@InfoTable", "mk06_transaksi"),
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

            Next

            'Insert Into AR06_STATUSDOK
            strSql = "INSERT INTO AR06_STATUSDOK(AR06_NOBILSEM,AR06_NoBil, AR06_StatusDok,AR06_Tarikh,AR06_Ulasan, AR06_NoStaf)" &
                             "VALUES (@NOBILSEM,@NOBIL, @STATUSDOK,@TARIKH,@ULASAN, @NoStaf)"

            Dim paramSql3() As SqlParameter =
                {
                New SqlParameter("@NOBILSEM", strNoPel),
                 New SqlParameter("@NOBIL", strNoInv),
                 New SqlParameter("@STATUSDOK", strStatDok),
                 New SqlParameter("@TARIKH", dtTkhToday),
                 New SqlParameter("@ULASAN", " - "),
                 New SqlParameter("@NoStaf", strNoStaf)
                }

            If Not dbconn.fInsertCommand(strSql, paramSql3) > 0 Then
                blnSuccess = False
                Exit Try
            End If


        Catch ex As Exception
            blnSuccess = False
        End Try

        If blnSuccess = True Then
            dbconn.sConnCommitTrans()
            ViewState("SaveInv") = True
            txtNoAdj.Text = strNoPel
            txtTkhAdj.Text = strTkhToday
            Return True
        ElseIf blnSuccess = False Then
            dbconn.sConnRollbackTrans()
            Return False
        End If
    End Function

    'Private Function fUpdateAdj(ByVal strNoInvCuk) As Boolean
    '    Dim strSql As String
    '    Dim dbconn As New DBKewConn
    '    Dim blnSuccess As Boolean = True

    '    Try
    '        strNoInvCuk = Trim(txtNoAdj.Text.TrimEnd)
    '        Dim strPerhatian As String = Trim(lblPerhatian.Text.TrimEnd)
    '        Dim strNoRujuk As String = Trim(lblNoRuj.Text.TrimEnd)
    '        Dim IDPenerima As String = Trim(lblIDPenerima.Text.TrimEnd)
    '        Dim strAlamat1 As String = Trim(lblAlmt1.Text.TrimEnd)
    '        Dim strAlamat2 As String = Trim(lblAlmt2.Text.TrimEnd)
    '        Dim strBdr As String = Trim(lblBandar.Text.TrimEnd)
    '        Dim strPoskod As String = Trim(lblPoskod.Text.TrimEnd)
    '        Dim strEmel As String = Trim(lblEmel.Text.TrimEnd)
    '        Dim strNoTel As String = Trim(lblNoTel.Text.TrimEnd)
    '        Dim strNoFax As String = Trim(lblNoFaks.Text.TrimEnd)
    '        Dim strTujuan As String = Trim(txtTujuan.Text.TrimEnd)

    '        Dim decJumDt, decTotJum As Decimal
    '        For Each gvrow As GridViewRow In gvTransAsal.Rows

    '            decJumDt = CDec(TryCast(gvrow.FindControl("txtJumlah"), TextBox).Text)
    '            decTotJum += decJumDt
    '        Next

    '        'Update AR01_Bil
    '        strSql = "update AR01_Bil SET AR01_UtkPerhatian = @UtkPerhatian, AR01_NoRujukan = @NoRujuk, AR01_Jumlah = @Jumlah, AR01_JenisUrusniaga = @JnsUrus, AR01_Kategori = @Kategori,
    'AR01_IDPenerima = @IDPENERIMA, AR01_NamaPenerima = @NAMAPENERIMA, AR01_Almt1 = @ALMT1, AR01_Almt2 = @ALMT2, AR01_Bandar = @BANDAR, AR01_Poskod = @POSKOD, KodNegara =@KODNEGARA, KodNegeri =@KODNEGERI,
    'AR01_Emel = @EMEL, AR01_NoTel = @NOTEL, AR01_NoFaks = @NOFAKS, AR01_KodBank = @KODBANK, AR01_Tujuan = @TUJUAN, AR01_TkhKonDari = @TkhKonDari , AR01_TkhKonHingga = @TkhKonHingga,
    'AR01_TempohKontrak = @TempohKontrak where AR01_NoBilSem = @NoInv"

    '        'Dim paramSql() As SqlParameter =
    '        '    {
    '        '    New SqlParameter("@NoInv", strNoInvCuk),
    '        '    New SqlParameter("@TkhMhn", dtTkhMhnInv),
    '        '    New SqlParameter("@UTKPERHATIAN", strPerhatian),
    '        '    New SqlParameter("@NoRujuk", strNoRujuk),
    '        '    New SqlParameter("@Jumlah", decTotJum),
    '        '    New SqlParameter("@JnsUrus", strJenisUrusniaga),
    '        '    New SqlParameter("@Kategori", strKat),
    '        '    New SqlParameter("@IDPENERIMA", IDPenerima),
    '        '    New SqlParameter("@NAMAPENERIMA", NamaPenerima),
    '        '    New SqlParameter("@ALMT1", strAlamat1),
    '        '    New SqlParameter("@ALMT2", strAlamat2),
    '        '    New SqlParameter("@BANDAR", strBdr),
    '        '    New SqlParameter("@POSKOD", strPoskod),
    '        '    New SqlParameter("@KODNEGARA", KodNegara),
    '        '    New SqlParameter("@KODNEGERI", KodNegeri),
    '        '    New SqlParameter("@EMEL", strEmel),
    '        '    New SqlParameter("@NOTEL", strNoTel),
    '        '    New SqlParameter("@NOFAKS", strNoFax),
    '        '    New SqlParameter("@KODBANK", strBank),
    '        '    New SqlParameter("@TUJUAN", strTujuan),
    '        '    New SqlParameter("@TkhKonDari", strTkhDari),
    '        '    New SqlParameter("@TkhKonHingga", strTkhTo),
    '        '    New SqlParameter("@TempohKontrak", strTmph)
    '        '    }

    '        'dbconn.sConnBeginTrans()
    '        'If Not dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
    '        '    blnSuccess = False
    '        '    Exit Try
    '        'End If

    '        Dim intBil As Integer
    '        Dim strKodKW, strKodKO, strPTJ, strKodKP, strKodVot, strPerkara As String
    '        Dim decKuantiti, decKdrHrg, decJum As Decimal

    '        For Each gvTransInvrow As GridViewRow In gvTransAsal.Rows
    '            intBil = intBil + 1
    '            strKodKW = TryCast(gvTransInvrow.FindControl("ddlKW"), DropDownList).SelectedValue
    '            strKodKO = TryCast(gvTransInvrow.FindControl("ddlKO"), DropDownList).SelectedValue
    '            strPTJ = TryCast(gvTransInvrow.FindControl("ddlPTj"), DropDownList).SelectedValue
    '            strKodKP = TryCast(gvTransInvrow.FindControl("ddlKP"), DropDownList).SelectedValue
    '            strKodVot = TryCast(gvTransInvrow.FindControl("ddlVot"), DropDownList).SelectedValue
    '            strPerkara = TryCast(gvTransInvrow.FindControl("txtPerkara"), TextBox).Text
    '            decKuantiti = CDec(TryCast(gvTransInvrow.FindControl("txtKuantiti"), TextBox).Text)
    '            decKdrHrg = CDec(TryCast(gvTransInvrow.FindControl("txtHarga"), TextBox).Text)
    '            decJum = CDec(TryCast(gvTransInvrow.FindControl("txtJumlah"), TextBox).Text)

    '            strSql = "update AR01_BilDT set KodKw = @KodKw, KodKO = @KodKo, KodPTJ = @KodPTJ, KodKP = @KodKP, KodVot = @KodVot, AR01_Perkara = @Perkara, AR01_Kuantiti = @Kuantiti, AR01_kadarHarga = @kadarHarga, AR01_Jumlah = @Jumlah where AR01_Bil = @InvDtBil"

    '            Dim paramSql2() As SqlParameter =
    '                {
    '                 New SqlParameter("@InvDtBil", intBil),
    '                 New SqlParameter("@kodKw", strKodKW),
    '                 New SqlParameter("@KodKO", strKodKO),
    '                 New SqlParameter("@KodPTJ", strPTJ),
    '                 New SqlParameter("@KodKP", strKodKP),
    '                 New SqlParameter("@KodVot", strKodVot),
    '                 New SqlParameter("@Perkara", strPerkara),
    '                 New SqlParameter("@Kuantiti", decKuantiti),
    '                 New SqlParameter("@kadarHarga", decKdrHrg),
    '                 New SqlParameter("@Jumlah", decJum)
    '                }

    '            If Not dbconn.fUpdateCommand(strSql, paramSql2) > 0 Then
    '                blnSuccess = False
    '                Exit Try
    '            End If

    '        Next

    '    Catch ex As Exception
    '        blnSuccess = False
    '    End Try

    '    If blnSuccess = True Then
    '        dbconn.sConnCommitTrans()
    '        Return True
    '    ElseIf blnSuccess = False Then
    '        dbconn.sConnRollbackTrans()
    '        Return False
    '    End If
    'End Function
    Function fGetID_Bil(ByVal strPrefix As String, ByVal strButiran As String)
        Try
            'Dim JumBaki As Decimal = Trim(txtJumBaki.Text.Trim)
            'Dim JumAsal As Decimal = Trim(txtJumAsal.Text.Trim)
            Dim strSql As String
            Dim strIdx As String
            Dim intLastIdx As Integer
            Dim strCol As String = "NoAkhir"
            Dim strTahun As String = Now.Year
            Dim strKodModul As String = "AR"

            strSql = "select * from mk_noakhir where tahun = '" & strTahun & "' and kodmodul='" & strKodModul & "' and prefix='" & strPrefix & "'"
            strCol = "NoAkhir"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)


            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    intLastIdx = CInt(ds.Tables(0).Rows(0)(strCol).ToString)
                Else
                    intLastIdx = 0
                End If

                If intLastIdx = 0 Then
                    strIdx = strPrefix & "000001" & Now.ToString("MM") & Now.ToString("yy")

                    strSql = " insert into mk_noakhir (KodModul , Prefix, NoAkhir, Tahun, Butiran, KodPTJ)" &
                        "values(@kodmodul,@prefix,@noakhir,@tahun,@butiran,@kodPTJ)"

                    Dim paramSql() As SqlParameter = {
                            New SqlParameter("@kodmodul", strKodModul),
                            New SqlParameter("@prefix", strPrefix),
                            New SqlParameter("@noakhir", 1),
                            New SqlParameter("@tahun", strTahun),
                            New SqlParameter("@butiran", strButiran),
                            New SqlParameter("@kodPTJ", "-")
                        }

                    dbconn = New DBKewConn
                    dbconn.sConnBeginTrans()
                    If dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                        dbconn.sConnCommitTrans()
                    Else
                        dbconn.sConnRollbackTrans()
                    End If

                Else

                    intLastIdx = intLastIdx + 1
                    strIdx = strPrefix & intLastIdx.ToString("D6") & Now.ToString("MM") & Now.ToString("yy")

                    strSql = "update MK_NoAkhir set NoAkhir =@noakhir where tahun =@tahun and KodModul =@kodmodul and prefix = @prefix"

                    Dim paramSql2() As SqlParameter = {
                                    New SqlParameter("@noakhir", intLastIdx),
                                    New SqlParameter("@tahun", strTahun),
                                    New SqlParameter("@kodmodul", strKodModul),
                                    New SqlParameter("@prefix", strPrefix)
                                    }

                    dbconn.sConnBeginTrans()
                    If dbconn.fUpdateCommand(strSql, paramSql2) > 0 Then
                        dbconn.sConnCommitTrans()
                    Else
                        dbconn.sConnRollbackTrans()
                    End If
                End If

                Return strIdx
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub ddlCarian_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCarian.SelectedIndexChanged

        If ddlCarian.SelectedIndex = 0 Then
            txtCarian.Enabled = False
            txtCarian.Text = ""
        Else
            txtCarian.Enabled = True
        End If


    End Sub

    Private Sub lnkBtnBack_Click(sender As Object, e As EventArgs) Handles lnkBtnBack.Click
        sLoadLst()
        divList.Visible = True
        divWiz.Visible = False
    End Sub

    Dim decJumAsal As Decimal
    Private Sub gvTransAsal_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvTransAsal.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim strJumlah As String = CType(e.Row.FindControl("lblJum"), Label).Text
                If strJumlah = "" Then strJumlah = "0.00"
                decJumAsal += CDec(strJumlah)
            End If

            If e.Row.RowType = DataControlRowType.Footer Then
                Dim lblTotJum As Label = CType(e.Row.FindControl("lblTotJum"), Label)
                lblTotJum.Text = FormatNumber(decJumAsal, 2)

            End If
            '  End If
        Catch ex As Exception

        End Try
    End Sub



    Protected Sub lbtnTambah_Click(sender As Object, e As EventArgs)
        AddNewRowToGrid()
    End Sub

    Private Sub AddNewRowToGrid()
        Try
            Dim rowIndex As Integer = 0
            Dim decJumBayar As Decimal
            If ViewState("vsDtInv") IsNot Nothing Then
                Dim dtvsDtInv As DataTable = CType(ViewState("vsDtInv"), DataTable)
                Dim drCurrentRow As DataRow = Nothing

                If dtvsDtInv.Rows.Count > 0 Then

                    For i As Integer = 1 To dtvsDtInv.Rows.Count
                        Dim ddl1 As DropDownList = CType(gvTrans.Rows(rowIndex).Cells(1).FindControl("ddlKW"), DropDownList)
                        Dim ddl2 As DropDownList = CType(gvTrans.Rows(rowIndex).Cells(2).FindControl("ddlKO"), DropDownList)
                        Dim ddl3 As DropDownList = CType(gvTrans.Rows(rowIndex).Cells(3).FindControl("ddlPTj"), DropDownList)
                        Dim ddl4 As DropDownList = CType(gvTrans.Rows(rowIndex).Cells(4).FindControl("ddlKP"), DropDownList)
                        Dim ddl5 As DropDownList = CType(gvTrans.Rows(rowIndex).Cells(5).FindControl("ddlVot"), DropDownList)

                        Dim box1 As TextBox = CType(gvTrans.Rows(rowIndex).Cells(6).FindControl("txtPerkara"), TextBox)
                        Dim ddl6 As DropDownList = CType(gvTrans.Rows(rowIndex).Cells(5).FindControl("ddlPetunjuk"), DropDownList)
                        Dim box2 As TextBox = CType(gvTrans.Rows(rowIndex).Cells(7).FindControl("txtKuantiti"), TextBox)
                        Dim box3 As TextBox = CType(gvTrans.Rows(rowIndex).Cells(8).FindControl("txtHarga"), TextBox)
                        Dim box4 As Label = CType(gvTrans.Rows(rowIndex).Cells(9).FindControl("lblJum"), Label)

                        drCurrentRow = dtvsDtInv.NewRow()
                        dtvsDtInv.Rows(i - 1)("KodKw") = ddl1.Text
                        dtvsDtInv.Rows(i - 1)("KodKO") = ddl2.Text
                        dtvsDtInv.Rows(i - 1)("KodPTJ") = ddl3.Text
                        dtvsDtInv.Rows(i - 1)("KodKP") = ddl4.Text
                        dtvsDtInv.Rows(i - 1)("KodVot") = ddl5.Text
                        dtvsDtInv.Rows(i - 1)("AR07_Petunjuk") = ddl6.Text
                        dtvsDtInv.Rows(i - 1)("AR01_Perkara") = box1.Text
                        dtvsDtInv.Rows(i - 1)("AR01_Kuantiti") = box2.Text
                        dtvsDtInv.Rows(i - 1)("AR01_kadarHarga") = box3.Text
                        dtvsDtInv.Rows(i - 1)("AR01_Jumlah") = box4.Text

                        Dim angHrgSeunit = CDec(box3.Text)
                        Dim JumAngHrg As Decimal = 0.00
                        If box3.Text IsNot String.Empty Then
                            angHrgSeunit = CDec(box3.Text)
                            JumAngHrg = CDec(box2.Text * angHrgSeunit)
                            box4.Text = JumAngHrg.ToString("#,##0.00")
                        End If

                        JumAngHrg = CDec(box2.Text * angHrgSeunit)
                        box4.Text = JumAngHrg.ToString("#,##0.00")
                        box3.Text = angHrgSeunit.ToString("#,##0.00")

                        Dim decJum As Decimal
                        If box4.Text = "" Then decJum = 0 Else decJum = CDec(box4.Text)
                        decJumBayar += decJum
                        ViewState("JumByr") = FormatNumber(decJumBayar, 2)

                        rowIndex += 1
                    Next

                    dtvsDtInv.Rows.Add(drCurrentRow)
                    ViewState("vsDtInv") = dtvsDtInv
                    gvTrans.DataSource = dtvsDtInv
                    gvTrans.DataBind()

                    SetPreviousData()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SetPreviousData()
        Try
            Dim rowIndex As Integer = 0

            If ViewState("vsDtInv") IsNot Nothing Then
                Dim dt As DataTable = CType(ViewState("vsDtInv"), DataTable)

                If dt.Rows.Count > 0 Then

                    For i As Integer = 0 To dt.Rows.Count - 1
                        Dim gvRow As GridViewRow = gvTrans.Rows(i)
                        Dim ddl1 As DropDownList = CType(gvRow.FindControl("ddlKW"), DropDownList)
                        Dim ddl2 As DropDownList = CType(gvRow.FindControl("ddlKO"), DropDownList)
                        Dim ddl3 As DropDownList = CType(gvRow.FindControl("ddlPTj"), DropDownList)
                        Dim ddl4 As DropDownList = CType(gvRow.FindControl("ddlKP"), DropDownList)
                        Dim ddl5 As DropDownList = CType(gvRow.FindControl("ddlVot"), DropDownList)
                        Dim box1 As TextBox = CType(gvRow.FindControl("txtPerkara"), TextBox)
                        Dim ddl6 As DropDownList = CType(gvRow.FindControl("ddlPetunjuk"), DropDownList)
                        Dim box2 As TextBox = CType(gvRow.FindControl("txtKuantiti"), TextBox)
                        Dim box3 As TextBox = CType(gvRow.FindControl("txtHarga"), TextBox)
                        Dim box4 As Label = CType(gvRow.FindControl("lblJum"), Label)

                        ddl1.SelectedValue = dt.Rows(i)("KodKw").ToString()
                        ddl2.SelectedValue = dt.Rows(i)("KodKO").ToString()
                        ddl3.SelectedValue = dt.Rows(i)("KodPTJ").ToString()
                        ddl4.SelectedValue = dt.Rows(i)("KodKP").ToString()
                        ddl5.SelectedValue = dt.Rows(i)("KodVot").ToString()
                        box1.Text = dt.Rows(i)("AR01_Perkara").ToString()
                        ddl6.SelectedValue = dt.Rows(i)("AR07_Petunjuk").ToString()
                        box2.Text = dt.Rows(i)("AR01_Kuantiti").ToString()
                        box3.Text = FormatNumber(dt.Rows(i)("AR01_kadarHarga").ToString())
                        box4.Text = FormatNumber(dt.Rows(i)("AR01_Jumlah").ToString())

                        rowIndex += 1

                    Next
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub lbtnXLulus_Click(sender As Object, e As EventArgs) Handles lbtnXLulus.Click

        If String.IsNullOrEmpty(txtUlasan.Text) Then
            fGlobalAlert("Sila masukkan Ulasan Tidak Lulus di ruang Ulasan!", Me.Page, Me.[GetType]())
            Exit Sub
        End If

        Dim strUlasan As String = Trim(txtUlasan.Text.TrimEnd)

        Dim strStatDok As String = "11"
        Dim blnSuccess As Boolean = True
        Try
            Dim strNoInv As String = Trim(lblNoInv.Text.TrimEnd)
            Dim strNoPelSem As String = Trim(lblNoPelSem.Text.TrimEnd)
            Dim dtTkhToday As DateTime = CDate(Now.ToString("yyyy-MM-dd"))
            Dim strNoStaf As String = Trim(lblNoStafPel.Text.TrimEnd)


            dbconn.sConnBeginTrans()
            Dim strSql As String = "Update AR01_Bil set AR01_StatusDok = @StatDok where AR01_NoBil = @NoInv"

            Dim paramSql() As SqlParameter = {
                                                New SqlParameter("@StatDok", strStatDok),
                                                New SqlParameter("@NoInv", strNoInv)
                                                }
            If Not dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                blnSuccess = False
                Exit Try
            End If

            'Insert Into AR06_STATUSDOK
            strSql = "INSERT INTO AR06_STATUSDOK(AR06_NOBILSEM,AR06_NoBil, AR06_StatusDok,AR06_Tarikh,AR06_Ulasan, AR06_NoStaf)" &
                             "VALUES (@NOBILSEM,@NOBIL, @STATUSDOK,@TARIKH,@ULASAN, @NoStaf)"

            Dim paramSql3() As SqlParameter =
                {
                New SqlParameter("@NOBILSEM", strNoPelSem),
                 New SqlParameter("@NOBIL", strNoInv),
                 New SqlParameter("@STATUSDOK", strStatDok),
                 New SqlParameter("@TARIKH", dtTkhToday),
                 New SqlParameter("@ULASAN", strUlasan),
                 New SqlParameter("@NoStaf", strNoStaf)
                }

            If Not dbconn.fInsertCommand(strSql, paramSql3) > 0 Then
                blnSuccess = False
                Exit Try
            End If

        Catch ex As Exception
            blnSuccess = False
        End Try

        If blnSuccess = True Then
            dbconn.sConnCommitTrans()
            fGlobalAlert("Maklumat telah disimpan sebagai 'Tidak Lulus'!", Me.Page, Me.[GetType]())
            lbtnLulus.Visible = False
            lbtnXLulus.Visible = False
            sLoadLst()
        ElseIf blnSuccess = False Then
            dbconn.sConnRollbackTrans()
            fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
        End If

    End Sub

    Private Sub gvLst_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvLst.PageIndexChanging
        Try
            gvLst.PageIndex = e.NewPageIndex
            If ViewState("vsDtInv") IsNot Nothing Then
                gvLst.DataSource = ViewState("vsDtInv")
                gvLst.DataBind()
            Else
                Dim dt As New DataTable
                gvLst.DataSource = dt
                gvLst.DataBind()

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvLst_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvLst.SelectedIndexChanged
        Try
            Dim row As GridViewRow = gvLst.SelectedRow

            Dim strTkhBil As String = CType(row.FindControl("lblTkhBil"), Label).Text.Trim
            Dim strNoInvCuk As String = CType(row.FindControl("lblNoInvCuk"), Label).Text.Trim
            Dim strNoAdj As String = CType(row.FindControl("lblNoAdj"), Label).Text.Trim
            Dim strTuj As String = CType(row.FindControl("lblTujuan"), Label).Text.Trim
            Dim strStatDok As String = CType(row.FindControl("lblStatDok"), Label).Text.Trim

            lblTkhBil.Text = strTkhBil
            lblNoInv.Text = strNoInvCuk 'no invois
            ' txtNoInv.Text = strNoAdj
            txtTujuan.Text = strTuj

            fBindInvAdj(strNoInvCuk)
            fBindTrans(strNoInvCuk)
            fBindTransAsal(strNoInvCuk)

            If strStatDok = "06" Then
                lbtnLulus.Visible = True
                lbtnXLulus.Visible = True
            Else
                lbtnLulus.Visible = False
                lbtnXLulus.Visible = False
            End If

            TabContainer.ActiveTabIndex = 0
            divList.Visible = False
            divWiz.Visible = True

        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvTrans_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvTrans.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim ddlKW = DirectCast(e.Row.FindControl("ddlKW"), DropDownList)
                ddlKW.DataSource = ViewState("dsKW")
                ddlKW.DataTextField = "ButiranKW"
                ddlKW.DataValueField = "KodKw"
                ddlKW.DataBind()
                ddlKW.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
                ddlKW.SelectedValue = TryCast(e.Row.FindControl("hidKW"), HiddenField).Value


                Dim ddlKO = DirectCast(e.Row.FindControl("ddlKO"), DropDownList)
                Dim strSelKO As String = TryCast(e.Row.FindControl("hidKO"), HiddenField).Value

                If strSelKO = "0" OrElse strSelKO = "" Then
                    ddlKO.Items.Insert(0, New ListItem("- SILA PILIH KW -", 0))
                    ddlKO.SelectedValue = strSelKO
                Else
                    ddlKO.DataSource = ViewState("dsKO")
                    ddlKO.DataTextField = "Butiran"
                    ddlKO.DataValueField = "KodKO"
                    ddlKO.DataBind()

                    ddlKO.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
                    ddlKO.SelectedValue = strSelKO
                End If

                Dim ddlPTj = DirectCast(e.Row.FindControl("ddlPTj"), DropDownList)
                Dim strSelPTj As String = TryCast(e.Row.FindControl("hidPTj"), HiddenField).Value

                If strSelPTj = "0" OrElse strSelPTj = "" Then
                    ddlPTj.Items.Insert(0, New ListItem("- SILA PILIH KO -", 0))
                    ddlPTj.SelectedValue = strSelPTj
                Else
                    ddlPTj.DataSource = ViewState("dsPTj")
                    ddlPTj.DataTextField = "Butiran"
                    ddlPTj.DataValueField = "KodPTj"
                    ddlPTj.DataBind()

                    ddlPTj.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
                    ddlPTj.SelectedValue = strSelPTj
                End If

                Dim ddlKP = DirectCast(e.Row.FindControl("ddlKP"), DropDownList)
                Dim strSelKP As String = TryCast(e.Row.FindControl("hidKP"), HiddenField).Value
                If strSelKP = "0" OrElse strSelKP = "" Then
                    ddlKP.Items.Insert(0, New ListItem("- SILA PILIH PTj -", 0))
                    ddlKP.SelectedValue = strSelKP
                Else
                    ddlKP.DataSource = ViewState("dsKP")
                    ddlKP.DataTextField = "Butiran"
                    ddlKP.DataValueField = "KodKP"
                    ddlKP.DataBind()

                    ddlKP.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
                    ddlKP.SelectedValue = strSelKP
                End If

                Dim ddlVot = DirectCast(e.Row.FindControl("ddlVot"), DropDownList)
                Dim strSelVot As String = TryCast(e.Row.FindControl("hidVot"), HiddenField).Value

                If strSelVot = "0" OrElse strSelVot = "" Then
                    ddlVot.Items.Insert(0, New ListItem("- SILA PILIH KP -", 0))
                    ddlVot.SelectedValue = strSelVot
                Else
                    ddlVot.DataSource = ViewState("dsVot")
                    ddlVot.DataTextField = "Butiran"
                    ddlVot.DataValueField = "KodVot"
                    ddlVot.DataBind()

                    ddlVot.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
                    ddlVot.SelectedValue = strSelVot
                End If

                Dim ddlPetunjuk = DirectCast(e.Row.FindControl("ddlPetunjuk"), DropDownList)
                Dim strSelPetunjuk As String = TryCast(e.Row.FindControl("hidPetunjuk"), HiddenField).Value

                If strSelPetunjuk = "0" OrElse strSelPetunjuk = "" Then
                    ddlVot.Items.Insert(0, New ListItem("", ""))
                    ddlVot.SelectedValue = strSelPetunjuk
                Else
                    ddlPetunjuk.DataSource = ViewState("dsPetunjuk")
                    ddlPetunjuk.Items.Add(New ListItem("+", "+"))
                    ddlPetunjuk.Items.Add(New ListItem("-", "-"))
                    ddlPetunjuk.Items.Insert(0, New ListItem("", ""))
                    ddlPetunjuk.SelectedValue = strSelPetunjuk
                End If


                Dim strJumlah As String = CType(e.Row.FindControl("lblJum"), Label).Text
                If strJumlah = "" Then strJumlah = "0.00"
                decJumlah += CDec(strJumlah)

            End If

            If e.Row.RowType = DataControlRowType.Footer Then
                Dim lblTotJum As Label = CType(e.Row.FindControl("lblTotJum"), Label)
                lblTotJum.Text = FormatNumber(decJumlah, 2)

            End If

        Catch ex As Exception

        End Try
    End Sub

    Dim decJumlahPel As Decimal
    Private Sub gvTransPel_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvTransPel.RowDataBound
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim strJumlah As String = CType(e.Row.FindControl("lblJum"), Label).Text
                If strJumlah = "" Then strJumlah = "0.00"
                decJumlahPel += CDec(strJumlah)
            End If

            If e.Row.RowType = DataControlRowType.Footer Then
                Dim lblTotJum As Label = CType(e.Row.FindControl("lblTotJum"), Label)
                lblTotJum.Text = FormatNumber(decJumlah, 2)

            End If



        Catch ex As Exception

        End Try
    End Sub

    Private Function fQuery(strSql) As DataSet
        Try
            Dim dbconn As New DBKewConn
            Using ds = dbconn.fSelectCommand(strSql)
                Return ds
            End Using
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub gvTrans_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvTrans.RowDeleting
        Try
            Dim rowIndex As Integer = 0
            Dim index As Integer = Convert.ToInt32(e.RowIndex)

            If ViewState("vsDtInv") IsNot Nothing Then
                Dim dtCurrentTable As DataTable = CType(ViewState("vsDtInv"), DataTable)
                Dim drCurrentRow As DataRow = Nothing

                If dtCurrentTable.Rows.Count > 0 Then
                    dtCurrentTable.Rows(index).Delete()
                    dtCurrentTable.AcceptChanges()
                    ViewState("vsDtInv") = dtCurrentTable
                    gvTrans.DataSource = dtCurrentTable
                    gvTrans.DataBind()
                End If
                'Else
                '    sCleargvInvDt()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlFilStat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFilStat.SelectedIndexChanged
        sLoadLst()
    End Sub
End Class