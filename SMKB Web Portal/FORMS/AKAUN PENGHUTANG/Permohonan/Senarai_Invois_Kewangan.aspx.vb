Imports System.Drawing
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class Senarai_Invois_Cukai
    Inherits System.Web.UI.Page
    Dim sLogBaru As String
    Dim sLogMedan As String
    Dim strNoStaf As String = UserInfo.strSesStaffID
    Dim sLogLevel As String = UserInfo.strUsrTahap
    Dim lsLogUsrIP As String = UserInfo.strUsrPcIP
    Dim lsLogUsrPC As String = UserInfo.strUsrPCName

    Private dbconnSMSM As New DBSMConn()
    Public Shared dsInvoisCukai As New DataSet
    Public Shared dvInvoisCukai As New DataView
    Private countButiran As Integer = 0
    Dim ds As New DataSet
    Dim ds1 As New DataSet
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Session("sesKodSubMenu") = Request.QueryString("KodSubMenu")

                fBindDdlBank()
                fBindJenisUrusniaga()
                fBindDdlKategori()
                fBindDdlNegara()
                fBindDdlNegeri()
                fBindDdlFilStat()
                fBindDdlJenTemp()

                fLoadKW()
                fLoadKO2()
                fLoadPTj2()
                fLoadKP2()
                fLoadVot2()

                fLoadInvKew()
                divList.Attributes.Add("style", "display:block")
                divDetail.Attributes.Add("style", "display:none")
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub lbtnCari_Click(sender As Object, e As EventArgs) Handles lbtnCari.Click
        fLoadInvKew()
    End Sub
    Private Sub fLoadInvKew()
        Dim strStat As String
        Try
            Dim intRec As Integer = 0
            ' Dim dt As New DataTable

            sClearGvLst()

            'dt.Columns.AddRange(New DataColumn() {
            '    New DataColumn("AR01_IdBil", GetType(Integer)),
            '                    New DataColumn("AR01_NoBilSem", GetType(String)),
            '                    New DataColumn("AR01_TkhMohon", GetType(Date)),
            '                    New DataColumn("AR01_Tujuan", GetType(String)),
            '                    New DataColumn("AR01_Kategori", GetType(String)),
            '                    New DataColumn("AR01_Kategori", GetType(String)),
            '                    New DataColumn("Jumlah", GetType(Decimal)),
            '                    New DataColumn("AR01_StatusDok", GetType(String)),
            '                    New DataColumn("Status", GetType(String))})

            'ViewState("dtSenarai") = dt
            Dim strFilter As String
            If ddlCarian.SelectedValue = 1 Then
                strFilter = " and AR01_NoBilSem = '" & Trim(txtCarian.Text.TrimEnd) & "'"
            End If

            If ddlFilStat.SelectedValue = 0 Then
                strStat = "01"
            Else
                strStat = ddlFilStat.SelectedValue
            End If

            Dim strSql As String = "select AR01_IdBil, AR01_NoBilSem, AR01_NoBil, AR01_TkhMohon, AR01_Tujuan, AR01_Kategori, (select Butiran  from MK_KategoriPenerima where Kod = AR01_Bil .AR01_Kategori ) as ButKat, AR01_JenisUrusniaga, (Select Butiran from RC_Urusniaga where RC_Urusniaga.KodUrusniaga = AR01_Bil.AR01_JenisUrusniaga) as ButJenUNiaga, AR01_Jumlah ,AR01_StatusDok, (select Butiran  from AR_StatusDok where KodStatus = AR01_Bil.AR01_StatusDok ) as ButStatDok  
from AR01_Bil where AR01_StatusDok in (" & strStat & ") AND AR01_NoStaf ='" & Session("ssusrID") & "' and AR01_Jenis ='01' and AR01_KodPTJMohon = '" & Session("ssusrKodPTj") & "' " & strFilter & " Order BY AR01_TkhMohon"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            'Dim intIdBil As Integer
            'Dim strNoInvCuk, strTujuan, strKat, strTkhMhn, strJum, strStatus, strKodStat
            'Dim decJum As Decimal
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    'For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    '    intIdBil = ds.Tables(0).Rows(i)("AR01_IdBil")
                    '    strNoInvCuk = ds.Tables(0).Rows(i)("AR01_NoBilSem")
                    '    strTkhMhn = ds.Tables(0).Rows(i)("AR01_TkhMohon")
                    '    strTujuan = ds.Tables(0).Rows(i)("AR01_Tujuan")
                    '    strKat = ds.Tables(0).Rows(i)("ButKat")
                    '    decJum = CDec(ds.Tables(0).Rows(i)("AR01_Jumlah"))
                    '    strJum = FormatNumber(decJum, 2)
                    '    strKodStat = ds.Tables(0).Rows(i)("AR01_StatusDok")
                    '    strStatus = ds.Tables(0).Rows(i)("ButStatDok")

                    '    dt.Rows.Add(intIdBil, strNoInvCuk, strTkhMhn, strTujuan, strKat, decJum, strKodStat, strStatus)

                    'Next

                    'ViewState("dtSenarai") = dt
                    'gvLst.DataSource = dt
                    'gvLst.DataBind()
                    'intRec = ds.Tables(0).Rows.Count

                    Dim dt As New DataTable
                    dt = ds.Tables(0)

                    ViewState("dtSenarai") = dt
                    gvLst.DataSource = dt
                    gvLst.DataBind()
                    intRec = ds.Tables(0).Rows.Count
                End If
            End If

            lblJumRekod.InnerText = intRec

        Catch ex As Exception

        End Try
    End Sub


    Private Sub fBindDdlBank()
        Try
            Dim strSql As String

            strSql = "select KodBank , (KodBank + ' - ' + NamaBank ) as NamaBank  from VMK_Bank01 ORDER BY KodBank"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)
            ddlBank.DataSource = ds
            ddlBank.DataTextField = "NamaBank"
            ddlBank.DataValueField = "KodBank"
            ddlBank.DataBind()

            ddlBank.Items.Insert(0, New ListItem(" - SILA PILIH -", 0))
            ddlBank.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub
    Private Sub fBindJenisUrusniaga()
        Try
            Dim strsql As String
            strsql = "Select KodUrusniaga ,(CONVERT (nvarchar, KodUrusniaga) + ' - ' +  Butiran) as Butiran 
from RC_Urusniaga 
where KodUrusniaga in (4,6,7,8,11,14,15,16,17) 
order by KodUrusniaga"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strsql)
            ddlJenis.DataSource = ds
            ddlJenis.DataTextField = "Butiran"
            ddlJenis.DataValueField = "KodUrusniaga"
            ddlJenis.DataBind()

            ddlJenis.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlJenis.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub
    Private Sub fBindDdlKategori()
        Try
            Dim strSql As String

            strSql = "SELECT Kod,(Kod + ' - ' + Butiran ) as Butiran from mk_kategoripenerima where (kod <> 'KJ' and kod <> 'PG' and kod <> 'UG' and kod <> 'PL')"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable

            ds = dbconn.fSelectCommand(strSql)
            ddlKat.DataSource = ds
            ddlKat.DataTextField = "Butiran"
            ddlKat.DataValueField = "Kod"
            ddlKat.DataBind()

            ddlKat.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlKat.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub
    'Private Sub fBindOA()
    '    Try
    '        Dim strsql As String
    '        strsql = "select IDNo_KP ,IDNama  from MK_Orang_Awam order by IDNama asc "

    '        Dim ds As New DataSet
    '        Dim dbconn As New DBKewConn
    '        ds = dbconn.fSelectCommand(strsql)

    '        ddlNmPenerima.DataSource = ds.Tables(0)
    '        ddlNmPenerima.DataTextField = "IDNama"
    '        ddlNmPenerima.DataValueField = "IDNo_KP"
    '        ddlNmPenerima.DataBind()

    '        ddlNmPenerima.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
    '        ddlNmPenerima.SelectedIndex = 0
    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Private Sub fBindStaff()
    '    Try

    '        Dim strSQLSMSM = "select MS01_NoStaf, (MS01_NoStaf + ' - ' + MS01_Nama) as NamaStaf from MS01_Peribadi Where MS01_Status = '1' Order by MS01_Nama"
    '        '"Select a.MS01_Nama, a.MS01_NoStaf as NoStaf, c.JawGiliran,(a.MS01_NoStaf + '-' + a.MS01_Nama) as Butiran
    '        '             FROM MS01_Peribadi a inner join  ms08_penempatan e On a.ms01_nostaf = e.ms01_nostaf And e.MS08_StaTerkini = 1, MS02_Perjawatan b, MS_Jawatan c
    '        '             Where 1 = 1
    '        '             And b.MS01_NoStaf = a.MS01_NoStaf
    '        '             And c.KodJawatan = b.MS02_JawSandang
    '        '             And b.ms02_kumpjawatan = '1'
    '        '             ORDER BY a.MS01_Nama"
    '        ds = dbconnSMSM.fselectCommand(strSQLSMSM)

    '        ddlNmPenerima.DataSource = ds
    '        ddlNmPenerima.DataTextField = "NamaStaf"
    '        ddlNmPenerima.DataValueField = "MS01_NoStaf"
    '        ddlNmPenerima.DataBind()

    '        ddlNmPenerima.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
    '        ddlNmPenerima.SelectedIndex = 0
    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Private Sub fBindDdlSyarikat()
    '    Try
    '        Dim strsql As String
    '        strsql = "select ROC01_IDSYA, (ROC01_IDSYA + ' - ' + ROC01_NamaSya ) as NamaSyarikat  from ROC01_Syarikat WHERE ROC01_KodLulus = '1' AND ROC01_KodAktif = '01' ORDER BY ROC01_NamaSya"

    '        Dim ds As New DataSet
    '        Dim dbconn As New DBKewConn

    '        ds = dbconn.fSelectCommand(strsql)
    '        ddlNmPenerima.DataSource = ds.Tables(0)
    '        ddlNmPenerima.DataTextField = "NamaSyarikat"
    '        ddlNmPenerima.DataValueField = "ROC01_IDSYA"
    '        ddlNmPenerima.DataBind()

    '        ddlNmPenerima.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
    '        ddlNmPenerima.SelectedIndex = 0
    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Private Sub fBindDdlPljr()
    '    Try
    '        Dim strsql As String
    '        strsql = "SELECT Kod,(Kod + ' - ' + Butiran ) as Butiran from mk_kategoripenerima where (kod <> 'KJ' )"
    '        ' strsql = "select Kod, (Kod + ' - ' + Butiran ) as Nama  from mk_kategoripenerima where kod in ('PG','UG') ORDER BY Kod"

    '        Dim ds As New DataSet
    '        Dim dbconn As New DBKewConn

    '        ds = dbconn.fSelectCommand(strsql)
    '        ddlNmPenerima.DataSource = ds.Tables(0)
    '        ddlNmPenerima.DataTextField = "Nama"
    '        ddlNmPenerima.DataValueField = "Kod"
    '        ddlNmPenerima.DataBind()

    '        ddlNmPenerima.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
    '        ddlNmPenerima.SelectedIndex = 0
    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Private Sub AlamatOA()
    '    Dim dbconn As New DBKewConn()
    '    Try

    '        Dim strSql As String
    '        Dim idOA As String = Trim(ddlNmPenerima.SelectedValue.TrimEnd)

    '        strSql = "select alamat1, alamat2, kodnegara, kodnegeri,bandar,poskod,no_tel,no_fax,emel from mk_orang_awam where idno_kp='" & idOA & "'"

    '        Dim ds As New DataSet
    '        ds = dbconn.fSelectCommand(strSql)

    '        If ds.Tables(0).Rows.Count > 0 Then
    '            txtAlmt1.Value = ds.Tables(0).Rows(0)("alamat1").ToString
    '            txtAlmt2.Value = ds.Tables(0).Rows(0)("alamat2").ToString
    '            txtBandar.Value = ds.Tables(0).Rows(0)("Bandar").ToString
    '            txtPoskod.Value = ds.Tables(0).Rows(0)("Poskod").ToString
    '            ddlNegara.SelectedValue = ds.Tables(0).Rows(0)("KodNegara").ToString
    '            fBindDdlNegeri()
    '            ddlNegeri.SelectedValue = ds.Tables(0).Rows(0)("KodNegeri").ToString
    '            txtNoTel.Value = ds.Tables(0).Rows(0)("No_Tel").ToString
    '            txtNoFax.Value = ds.Tables(0).Rows(0)("No_Fax").ToString
    '            txtEmel.Value = ds.Tables(0).Rows(0)("Emel").ToString
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Private Sub ddlNmPenerima_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNmPenerima.SelectedIndexChanged

    '    Try
    '        If ddlKat.SelectedValue = "OA" Then
    '            txtIDPenerima.Value = ddlNmPenerima.SelectedValue.TrimEnd
    '            AlamatOA()
    '        ElseIf ddlKat.SelectedValue = "ST" Then
    '            txtIDPenerima.Value = ddlNmPenerima.SelectedValue.TrimEnd
    '            AlamatStaff()

    '        ElseIf ddlKat.SelectedValue = "SY" Then
    '            txtIDPenerima.Value = ddlNmPenerima.SelectedValue.TrimEnd
    '            AlamatSyarikat()

    '        End If

    '    Catch ex As Exception

    '    End Try

    'End Sub
    Private Sub AlamatStaff()
        Dim dbconn As New DBKewConn()
        Try
            Dim strSql As String
            Dim idstaf As String = Trim(txtIDPenerima.Value.TrimEnd)

            Dim strSQLSMSM = "SELECT a.MS01_NoStaf, a.MS01_Nama , a.MS01_KpB,b.MS08_Unit,a.ms01_email 
                      FROM MS01_Peribadi a,MS08_Penempatan b WHERE b.MS08_StaTerkini = '1' AND a.MS01_NoStaf=b.MS01_NoStaf 
                      AND a.MS01_NoStaf =  '" & idstaf & "'"

            Dim ds As New DataSet
            ds = dbconnSMSM.fselectCommand(strSQLSMSM)

            If Not ds Is Nothing Then
                strSql = "Select Singkatan from MK_Ptj where KodPTJ = '" & ds.Tables(0).Rows(0)("MS08_Unit").ToString & "'"

                Dim ds2 As New DataSet
                ds2 = dbconn.fSelectCommand(strSql)

                If ds2.Tables(0).Rows.Count > 0 Then
                    txtAlmt1.Value = ds2.Tables(0).Rows(0)("Singkatan").ToString
                Else
                    txtAlmt1.Value = "-"
                End If
                txtEmel.Value = ds.Tables(0).Rows(0)("ms01_email").ToString
            End If

            strSql = "SELECT * FROM MK_Korporat"

            Dim ds3 As New DataSet
            ds3 = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds3.Tables(0).Rows.Count > 0 Then
                    txtAlmt2.Value = ds3.Tables(0).Rows(0)("Nama").ToString
                    txtBandar.Value = ds3.Tables(0).Rows(0)("Bandar").ToString
                    txtPoskod.Value = ds3.Tables(0).Rows(0)("Poskod").ToString
                    ddlNegara.SelectedValue = ds3.Tables(0).Rows(0)("KodNegara").ToString
                    fBindDdlNegeri()
                    ddlNegeri.SelectedValue = ds3.Tables(0).Rows(0)("KodNegeri").ToString
                    txtNoTel.Value = ds3.Tables(0).Rows(0)("NoTel1").ToString

                End If
            End If

        Catch ex As Exception

        End Try
    End Sub
    Function AlamatSyarikat()
        Try
            Dim strSql As String
            Dim IDSya As String = Trim(txtIDPenerima.Value.TrimEnd)

            strSql = "Select ROC01_AlmtP1,ROC01_AlmtP2,ROC01_BandarP,ROC01_PoskodP,ROC01_NegeriP,ROC01_NegaraP,ROC01_Tel1P,ROC01_FaksP,ROC01_EmelP,isnull(ROC01_WakilSya,'-') as ROC01_WakilSya from ROC01_Syarikat where ROC01_IDSya = '" & IDSya & "'"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then

                    txtAlmt1.Value = ds.Tables(0).Rows(0)("ROC01_AlmtP1").ToString
                    txtAlmt2.Value = ds.Tables(0).Rows(0)("ROC01_AlmtP2").ToString
                    txtBandar.Value = ds.Tables(0).Rows(0)("ROC01_BandarP").ToString
                    txtPoskod.Value = ds.Tables(0).Rows(0)("ROC01_PoskodP").ToString
                    ddlNegara.SelectedValue = ds.Tables(0).Rows(0)("ROC01_NegaraP").ToString
                    fBindDdlNegeri()
                    ddlNegeri.SelectedValue = ds.Tables(0).Rows(0)("ROC01_NegeriP").ToString
                    txtNoTel.Value = ds.Tables(0).Rows(0)("ROC01_Tel1P").ToString
                    txtNoFax.Value = ds.Tables(0).Rows(0)("ROC01_FaksP").ToString
                    txtEmel.Value = ds.Tables(0).Rows(0)("ROC01_EmelP").ToString
                    txtPerhatian.Value = ds.Tables(0).Rows(0)("ROC01_WakilSya").ToString

                End If

            End If
        Catch ex As Exception

        End Try
    End Function

    Private Sub fBindDdlNegeri()
        Try
            Dim strsql As String
            strsql = "select KodNegeri , (KodNegeri + ' - ' + Butiran ) as Butiran from MK_Negeri order by KodNegeri"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strsql)
            ddlNegeri.DataSource = ds
            ddlNegeri.DataTextField = "Butiran"
            ddlNegeri.DataValueField = "KodNegeri"
            ddlNegeri.DataBind()

            ddlNegeri.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlNegeri.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub
    Private Sub fBindDdlNegara()
        Try
            Dim strsql As String
            strsql = "select KodNegara , (KodNegara + ' - ' + Butiran ) as Butiran from MK_Negara order by KodNegara "

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strsql)
            ddlNegara.DataSource = ds
            ddlNegara.DataTextField = "Butiran"
            ddlNegara.DataValueField = "KodNegara"
            ddlNegara.DataBind()

            ddlNegara.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlNegara.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub
    'Private Sub ddlKat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKat.SelectedIndexChanged
    '    Try
    '        ddlNmPenerima.Items.Clear()
    '        If ddlKat.SelectedValue = "OA" Then
    '            fBindOA()

    '        ElseIf ddlKat.SelectedValue = "ST" Then
    '            fBindStaff()

    '        ElseIf ddlKat.SelectedValue = "SY" Then
    '            fBindDdlSyarikat()

    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub
    Private Sub ddlBank_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBank.SelectedIndexChanged
        fBindJenisUrusniaga()

    End Sub
    Private Sub ddlJenis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlJenis.SelectedIndexChanged
        fBindDdlKategori()
    End Sub
    Private Sub ddlNegara_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNegara.SelectedIndexChanged
        fBindDdlNegeri()
    End Sub



    Public Property direction() As SortDirection
        Get
            If ViewState("directionState") Is Nothing Then
                ViewState("directionState") = SortDirection.Ascending
            End If
            Return DirectCast(ViewState("directionState"), SortDirection)
        End Get
        Set
            ViewState("directionState") = Value
        End Set
    End Property

    Dim strTotJumlah As String
    Dim decJumlah As Decimal

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

    Private Function fLoadVot(ByVal strKodKW As String, ByVal strKodKO As String, ByVal strKodPTj As String, ByVal strKodKP As String, ByVal strKodJenUrusniaga As String) As DataSet
        Try

            '            Dim strSql As String = "SELECT DISTINCT A.KodVot, (A.KODVOT + ' - ' + B.BUTIRAN) as Butiran FROM MK03_AmTahun A INNER JOIN MK_VOT B ON A.KODVOT = B.KODVOT 
            'Where a.MK03_TAHUN = YEAR(GETDATE()) AND A.KodKw = '" & strKodKW & "' and A.KodKO = '" & strKodKO & "' AND A.KODPTJ = '" & strKodPTj & "' AND A.KodKP  = '" & strKodKP & "' AND  RIGHT(A.KODVOT,2) <> '00' ORDER BY A.KODVOT"

            Dim strSql = "SELECT DISTINCT A.KodVot, (A.KODVOT + ' - ' + B.BUTIRAN) as Butiran FROM MK03_AmTahun A INNER JOIN MK_VOT B ON A.KODVOT = B.KODVOT 
Where a.MK03_TAHUN = YEAR(GETDATE()) AND A.KodKw = '" & strKodKW & "' and A.KodKO = '" & strKodKO & "' AND A.KODPTJ = '" & strKodPTj & "' AND A.KodKP  = '" & strKodKP & "' AND  RIGHT(A.KODVOT,2) <> '00'
AND  A.KODVOT IN (SELECT KodVot FROM AR_VotUrusniaga WHERE KodKW = '" & strKodKW & "' AND KodJenUrusniaga = '" & strKodJenUrusniaga & "')
ORDER BY A.KODVOT"

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

            Dim strKodJenUrusniaga = Trim(ddlJenis.SelectedValue.TrimEnd)

            Dim ddlVot = DirectCast(gvr.FindControl("ddlVot"), DropDownList)
            ddlVot.DataSource = fLoadVot(strKodKW, strKodKO, strKodPTj, strKodKP, strKodJenUrusniaga)   'ViewState("dsKO")
            ddlVot.DataTextField = "Butiran"
            ddlVot.DataValueField = "KodVot"
            ddlVot.DataBind()

            ddlVot.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlVot.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub

    Private Sub sLoadInv(ByVal strNoInvCuk As String)

        Dim dbconn As New DBKewConn
        Try
            Dim strSqlInv = $"Select KodPenghutang, AR01_TkhMohon,AR01_KODPTJMOHON, AR01_NOSTAF, AR01_Jenis,AR01_KODBANK,AR01_NORUJUKAN, AR01_JenisUrusniaga,AR01_ALMT1,AR01_ALMT2,
                            AR01_KATEGORI,AR01_IDPENERIMA, AR01_NAMAPENERIMA,kodnegara, kodnegeri, AR01_TUJUAN, AR01_BANDAR,AR01_POSKOD, AR01_Kontrak, AR01_NoMemo, AR01_TkhKonDari,AR01_TkhKonHingga, AR01_JenTemp, AR01_NOTEL, 
                            AR01_NOFAKS, AR01_TempohKontrak, AR01_Emel,AR01_UtkPerhatian, AR01_StatusDok, (select Butiran  from AR_StatusDok where KodStatus = AR01_Bil.AR01_StatusDok  ) as ButStatDok from AR01_Bil where AR01_NoBilSem='{strNoInvCuk}';"

            'Load Permohonan Invois Cukai
            Dim ds = dbconn.fSelectCommand(strSqlInv)

            Using dtMhn = ds.Tables(0)
                If dtMhn.Rows.Count > 0 Then
                    Dim TkhMhn As Date = dtMhn.Rows(0)("AR01_TkhMohon")
                    Dim KodPTJ As String = dtMhn.Rows(0)("AR01_KODPTJMOHON")
                    Dim NoPmhn As String = dtMhn.Rows(0)("AR01_NOSTAF")
                    Dim katInv As String = dtMhn.Rows(0)("AR01_Jenis")
                    Dim Bank As String = dtMhn.Rows(0)("AR01_KODBANK")
                    Dim NoRuj As String = dtMhn.Rows(0)("AR01_NORUJUKAN")
                    Dim JnsUrus As String = dtMhn.Rows(0)("AR01_JenisUrusniaga")
                    Dim Almt1 As String = dtMhn.Rows(0)("AR01_ALMT1")
                    Dim Almt2 As String = dtMhn.Rows(0)("AR01_ALMT2")
                    Dim Kat As String = dtMhn.Rows(0)("AR01_KATEGORI")
                    Dim strKodPenghutang As String = dtMhn.Rows(0)("KodPenghutang")
                    Dim NPenerima As String = dtMhn.Rows(0)("AR01_NAMAPENERIMA")
                    Dim Negara As String = dtMhn.Rows(0)("kodnegara")
                    Dim Negeri As String = dtMhn.Rows(0)("kodnegeri")
                    Dim IDPenerima As String = dtMhn.Rows(0)("AR01_IDPENERIMA")
                    Dim Tujuan As String = dtMhn.Rows(0)("AR01_TUJUAN")
                    Dim Bandar As String = dtMhn.Rows(0)("AR01_BANDAR")
                    Dim Poskod As String = dtMhn.Rows(0)("AR01_POSKOD")
                    Dim blnKontrak As Boolean = CBool(dtMhn.Rows(0)("AR01_Kontrak"))
                    Dim NoTel As String = dtMhn.Rows(0)("AR01_NOTEL")
                    Dim NoFax As String = dtMhn.Rows(0)("AR01_NOFAKS")

                    Dim Emel As String = dtMhn.Rows(0)("AR01_Emel")
                    Dim Perhatian As String = dtMhn.Rows(0)("AR01_UtkPerhatian")
                    Dim strStatDok As String = dtMhn.Rows(0)("AR01_StatusDok")

                    Dim TkhKonDr As String = IIf(IsDBNull(dtMhn.Rows(0)("AR01_TkhKonDari")), "", dtMhn.Rows(0)("AR01_TkhKonDari"))
                    Dim TkhKonHingga As String = IIf(IsDBNull(dtMhn.Rows(0)("AR01_TkhKonHingga")), "", dtMhn.Rows(0)("AR01_TkhKonHingga"))
                    'dtMhn.Rows(0)("AR01_TkhKonHinggaAR01_TkhKonHingga
                    Dim TmphKon As String = IIf(IsDBNull(dtMhn.Rows(0)("AR01_TempohKontrak")), 0, dtMhn.Rows(0)("AR01_TempohKontrak"))
                    'dtMhn.Rows(0)("AR01_TempohKontrak")

                    Dim strNoMemo As String = IIf(IsDBNull(dtMhn.Rows(0)("AR01_NoMemo")), "", dtMhn.Rows(0)("AR01_NoMemo"))

                    'Dim strJenTemp As String = IIf(IsDBNull(dtMhn.Rows(0)("AR01_JenTemp")), "", CInt(dtMhn.Rows(0)("AR01_JenTemp")))
                    Dim strJenTemp As String '= dtMhn.Rows(0)("AR01_JenTemp")
                    If IsDBNull(dtMhn.Rows(0)("AR01_JenTemp")) Then
                        strJenTemp = ""
                    Else
                        strJenTemp = Trim(dtMhn.Rows(0)("AR01_JenTemp").ToString.TrimEnd)
                    End If

                    txtStatus.Text = dtMhn.Rows(0)("ButStatDok")
                    txtTkhMohon.Text = TkhMhn

                    txtNoInvSem.Text = strNoInvCuk
                    txtIDPenerima.Value = IDPenerima
                    txtNamaPenerima.Text = strKodPenghutang & " - " & NPenerima
                    txtTujuan.Value = Tujuan
                    txtNoRujukan.Value = NoRuj
                    txtAlmt2.Value = Almt1
                    txtAlmt1.Value = Almt2
                    txtBandar.Value = Bandar
                    txtPoskod.Value = Poskod
                    txtNoTel.Value = NoTel
                    txtNoFax.Value = NoFax
                    txtEmel.Value = Emel
                    txtPerhatian.Value = Perhatian

                    rbKontrak.SelectedValue = blnKontrak

                    ddlBank.SelectedValue = Bank
                    ddlJenis.SelectedValue = JnsUrus
                    ddlKat.SelectedValue = Kat
                    ddlNegara.SelectedValue = Negara
                    ddlNegeri.SelectedValue = Negeri

                    txtNamaPenerima.Visible = True

                    ddlJenis.Enabled = False
                    ddlKat.Enabled = False

                    If blnKontrak = True Then
                        sBindMemo(strKodPenghutang)
                        btnCalMula.Disabled = False
                        btnCalTamat.Disabled = False
                        ddlMemo.SelectedValue = strNoMemo
                        ddlJenTemp.SelectedIndex = IIf(strJenTemp = "", 0, strJenTemp)
                    Else
                        btnCalMula.Disabled = True
                        btnCalTamat.Disabled = True
                        ddlMemo.SelectedValue = 0
                        ddlJenTemp.SelectedValue = 0
                    End If

                    txtTkhMula.Text = TkhKonDr
                    txtTkhTamat.Text = TkhKonHingga
                    txtTemKon.Text = TmphKon

                End If
            End Using


        Catch ex As Exception

        End Try

    End Sub

    Private Sub fBindDdlJenTemp()
        Try
            Dim strSql As String

            strSql = "select Kod, Butiran from PO_Tempoh order by Kod"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)
            ddlJenTemp.DataSource = ds
            ddlJenTemp.DataTextField = "Butiran"
            ddlJenTemp.DataValueField = "Kod"
            ddlJenTemp.DataBind()

            ddlJenTemp.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlJenTemp.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub sBindMemo(strKodPenghutang)

        Try


            Dim strSql = "select PUU05_NoMemo, PUU06_TkhMula, PUU06_TkhTamat, PUU06_Tempoh, PUU06_JnsTempoh from PUU06_DaftarMemo where ROC01_IDSya = (select IdPenghutang  from AR_Penghutang where KodPenghutang = '" & strKodPenghutang & "')"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)

            ddlMemo.DataSource = ds
            ddlMemo.DataTextField = "PUU05_NoMemo"
            ddlMemo.DataValueField = "PUU05_NoMemo"
            ddlMemo.DataBind()

            ddlMemo.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlMemo.SelectedIndex = 0

            ViewState("dsMemo") = ds

        Catch ex As Exception

        End Try
    End Sub

    Private Sub sLoadInvDt(strNoInvSem)
        Try
            Dim strSql = "select AR01_BilDtID, KodKw,KodKO,KodPTJ,KodKP,KodVot,AR01_Perkara,AR01_Kuantiti,AR01_kadarHarga,AR01_Jumlah from AR01_BilDt where AR01_NoBilSem='" & strNoInvSem & "';"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            Using dt = ds.Tables(0)
                ViewState("dtInvDt") = dt
                gvInvDt.DataSource = dt
                gvInvDt.DataBind()
            End Using
        Catch ex As Exception

        End Try
    End Sub

    Private Sub sLoadInvLamp(strNoInvSem)

        sClearGvLamp()

        Try
            Dim strSql = "Select  AR01_IdBil, AR11_ID, AR11_Path, AR11_NamaDok,AR11_ContentType, AR11_Status from AR11_Lampiran where AR01_NoBilSem = '" & strNoInvSem & "' and AR11_Status = 1 order by AR11_Bil "
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)


            Dim dt As New DataTable
            dt = fSetDtLamp()

            Dim GuidId As Guid
            Dim IdBil, intID As Integer
            Dim strPath As String
            Dim strNamaDok As String
            Dim strContType As String
            Dim strStat As String
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                GuidId = Guid.NewGuid()
                IdBil = ds.Tables(0).Rows(i)("AR01_IdBil").ToString
                intID = ds.Tables(0).Rows(i)("AR11_ID").ToString
                strPath = ds.Tables(0).Rows(i)("AR11_Path").ToString
                strNamaDok = ds.Tables(0).Rows(i)("AR11_NamaDok").ToString
                strContType = ds.Tables(0).Rows(i)("AR11_ContentType").ToString
                strStat = ds.Tables(0).Rows(i)("AR11_Status").ToString

                dt.Rows.Add(GuidId, IdBil, intID, strPath, strNamaDok, strContType, strStat)
            Next

            ViewState("dtLampiran") = dt
            gvLamp.DataSource = dt
            gvLamp.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub sClearGvLamp()
        gvLamp.DataSource = New List(Of String)
        gvLamp.DataBind()
    End Sub
    Private Sub ddlSaizRekod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSaizRekod.SelectedIndexChanged
        Try
            Dim dt As New DataTable
            gvLst.PageSize = CInt(ddlSaizRekod.Text)
            gvLst.DataSource = ViewState("dtSenarai")
            gvLst.DataBind()
        Catch ex As Exception

        End Try

    End Sub


    Dim strTotJumlah2 As String
    Dim decJumlah2 As Decimal
    Private Sub ddlCarian_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCarian.SelectedIndexChanged
        If ddlCarian.SelectedIndex = 0 Then
            txtCarian.Enabled = False
        Else
            txtCarian.Enabled = True
        End If
        txtCarian.Text = ""
    End Sub



    Private Sub gvLst_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvLst.PageIndexChanging
        Try
            gvLst.PageIndex = e.NewPageIndex
            If ViewState("dtSenarai") IsNot Nothing Then
                gvLst.DataSource = ViewState("dtSenarai")
                gvLst.DataBind()
            Else
                Dim dt As New DataTable
                'BindGvViewButiran()
                ViewState("dtSenarai") = dt

            End If
        Catch ex As Exception

        End Try
    End Sub

    'Private Sub gvLst_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvLst.RowCommand
    '    Try
    '        If e.CommandName = "Select" Then
    '            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
    '            Dim selectedRow As GridViewRow = gvLst.Rows(index)

    '            Dim strNoInvSem As String = CType(selectedRow.FindControl("lblNoInvSem"), Label).Text.TrimEnd
    '            Dim intIdBil As String = CInt(CType(selectedRow.FindControl("lblIdBil"), Label).Text.TrimEnd)

    '            hidIdBil.Value = intIdBil
    '            sLoadInv(strNoInvSem)
    '            sLoadInvDt(strNoInvSem)
    '            sLoadInvLamp(strNoInvSem)

    '            divList.Visible = False
    '            divDetail.Visible = True
    '        End If

    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub gvLst_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvLst.Sorting
        Try
            Dim sortingDirection As String = String.Empty
            If direction = SortDirection.Ascending Then
                direction = SortDirection.Descending
                sortingDirection = "Desc"
            Else
                direction = SortDirection.Ascending
                sortingDirection = "Asc"
            End If

            Dim dt As New DataTable
            dt = CType(ViewState("dtSenarai"), DataTable)

            Dim sortedView As New DataView(dt)
            sortedView.Sort = Convert.ToString(e.SortExpression + " ") & sortingDirection
            Session("SortedView") = sortedView
            gvLst.DataSource = sortedView
            gvLst.DataBind()
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
            Dim txtHarga As TextBox = CType(CType(sender, Control), TextBox)
            Dim HrgUnit = txtHarga.Text
            Dim gvRow As GridViewRow = CType(CType(sender, Control), TextBox).Parent.Parent
            Dim txtKuantiti As TextBox = CType(gvRow.FindControl("txtKuantiti"), TextBox)
            Dim lblJumlah As Label = CType(gvRow.FindControl("lblJumlah"), Label)

            '  If txtKuantiti.Text IsNot String.Empty Then
            Dim angHrgSeunit = CDec(HrgUnit)
            Dim JumAngHrg As Decimal = 0.00

            JumAngHrg = CDec(CInt(txtKuantiti.Text) * angHrgSeunit)
            lblJumlah.Text = FormatNumber(JumAngHrg, 2) 'JumAngHrg.ToString("N2")
            ' End If

            txtHarga.Text = FormatNumber(txtHarga.Text, 2)
            fSetFooter()

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub txtJumlah_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim decJumTrans As Decimal
            For Each gvRow As GridViewRow In gvInvDt.Rows
                Dim decJumlah As String = CDec(Trim(CType(gvRow.FindControl("txtJumlah"), TextBox).Text.TrimEnd))
                decJumTrans += decJumlah
            Next

            Dim footerRow = gvInvDt.FooterRow
            CType(footerRow.FindControl("lblTotJum"), Label).Text = FormatNumber(decJumTrans, 2)


        Catch ex As Exception

        End Try
    End Sub
    Private Sub fSetFooter()
        Try
            Dim decJumTrans As Decimal
            For Each gvRow1 As GridViewRow In gvInvDt.Rows
                Dim decJumlah As String = CDec(Trim(CType(gvRow1.FindControl("lblJumlah"), Label).Text.TrimEnd))
                decJumTrans += decJumlah
            Next

            Dim footerRow = gvInvDt.FooterRow
            CType(footerRow.FindControl("lblTotJum"), Label).Text = FormatNumber(decJumTrans, 2)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lbtnTambah_Click(sender As Object, e As EventArgs)
        AddNewRowToGrid()
    End Sub

    Private Sub AddNewRowToGrid()
        Try

            'If fCheckTrans() = False Then
            '    Exit Sub
            'End If

            Dim rowIndex As Integer = 0
            If ViewState("dtInvDt") IsNot Nothing Then
                Dim dtvsDtInv As DataTable = CType(ViewState("dtInvDt"), DataTable)
                Dim drCurrentRow As DataRow = Nothing

                If dtvsDtInv.Rows.Count > 0 Then

                    For i As Integer = 1 To dtvsDtInv.Rows.Count

                        Dim strKW = CType(gvInvDt.Rows(rowIndex).Cells(1).FindControl("ddlKW"), DropDownList).Text
                        Dim strKO = CType(gvInvDt.Rows(rowIndex).Cells(2).FindControl("ddlKO"), DropDownList).Text
                        Dim strPTj = CType(gvInvDt.Rows(rowIndex).Cells(3).FindControl("ddlPTj"), DropDownList).Text
                        Dim strKP = CType(gvInvDt.Rows(rowIndex).Cells(4).FindControl("ddlKP"), DropDownList).Text
                        Dim strVot = CType(gvInvDt.Rows(rowIndex).Cells(5).FindControl("ddlVot"), DropDownList).Text
                        Dim strPerkara = CType(gvInvDt.Rows(rowIndex).Cells(6).FindControl("txtPerkara"), TextBox).Text
                        Dim strKuantiti = CType(gvInvDt.Rows(rowIndex).Cells(7).FindControl("txtKuantiti"), TextBox).Text
                        Dim strHarga = CType(gvInvDt.Rows(rowIndex).Cells(8).FindControl("txtHarga"), TextBox).Text
                        Dim strJumlah = CType(gvInvDt.Rows(rowIndex).Cells(9).FindControl("lblJumlah"), Label).Text

                        dtvsDtInv.Rows(i - 1)("KodKw") = strKW
                        dtvsDtInv.Rows(i - 1)("KodKO") = strKO
                        dtvsDtInv.Rows(i - 1)("KodPTJ") = strPTj
                        dtvsDtInv.Rows(i - 1)("KodKP") = strKP
                        dtvsDtInv.Rows(i - 1)("KodVot") = strVot
                        dtvsDtInv.Rows(i - 1)("AR01_Perkara") = strPerkara
                        dtvsDtInv.Rows(i - 1)("AR01_Kuantiti") = IIf(strKuantiti = "", 0, strKuantiti)
                        dtvsDtInv.Rows(i - 1)("AR01_kadarHarga") = IIf(strHarga = "", 0, strHarga)
                        dtvsDtInv.Rows(i - 1)("AR01_Jumlah") = IIf(strJumlah = "", 0, strJumlah)

                        rowIndex += 1
                    Next

                    drCurrentRow = dtvsDtInv.NewRow()
                    dtvsDtInv.Rows.Add(drCurrentRow)
                    ViewState("dtInvDt") = dtvsDtInv
                    gvInvDt.DataSource = dtvsDtInv
                    gvInvDt.DataBind()
                End If
            Else
                Response.Write("ViewState is null")
            End If

            SetPreviousData()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SetPreviousData()
        Try
            Dim rowIndex As Integer = 0

            If ViewState("dtInvDt") IsNot Nothing Then
                Dim dt As DataTable = CType(ViewState("dtInvDt"), DataTable)

                If dt.Rows.Count > 0 Then

                    For i As Integer = 0 To dt.Rows.Count - 1

                        Dim gvRow As GridViewRow = gvInvDt.Rows(i)
                        Dim ddl1 As DropDownList = CType(gvRow.FindControl("ddlKW"), DropDownList)
                        Dim ddl2 As DropDownList = CType(gvRow.FindControl("ddlKO"), DropDownList)
                        Dim ddl3 As DropDownList = CType(gvRow.FindControl("ddlPTj"), DropDownList)
                        Dim ddl4 As DropDownList = CType(gvRow.FindControl("ddlKP"), DropDownList)
                        Dim ddl5 As DropDownList = CType(gvRow.FindControl("ddlVot"), DropDownList)
                        Dim box1 As TextBox = CType(gvRow.FindControl("txtPerkara"), TextBox)
                        Dim box2 As TextBox = CType(gvRow.FindControl("txtKuantiti"), TextBox)
                        Dim box3 As TextBox = CType(gvRow.FindControl("txtHarga"), TextBox)
                        Dim box4 As Label = CType(gvRow.FindControl("lblJumlah"), Label)

                        Dim strKtt As String = dt.Rows(i)("AR01_Kuantiti").ToString()
                        Dim strHarga As String = dt.Rows(i)("AR01_kadarHarga").ToString()
                        Dim strJumlah As String = dt.Rows(i)("AR01_Jumlah").ToString()

                        ddl1.SelectedValue = dt.Rows(i)("KodKw").ToString()
                        ddl2.SelectedValue = dt.Rows(i)("KodKO").ToString()
                        ddl3.SelectedValue = dt.Rows(i)("KodPTJ").ToString()
                        ddl4.SelectedValue = dt.Rows(i)("KodKP").ToString()
                        ddl5.SelectedValue = dt.Rows(i)("KodVot").ToString()
                        box1.Text = dt.Rows(i)("AR01_Perkara").ToString()
                        box2.Text = IIf(strKtt = "", 0, strKtt)
                        box3.Text = IIf(strHarga = "", 0, strHarga)
                        box4.Text = IIf(strJumlah = "", 0, strJumlah)

                        rowIndex += 1
                    Next
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvInvDt_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvInvDt.RowDataBound
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

                Dim strJumlah As String = CType(e.Row.FindControl("lblJumlah"), Label).Text
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

    Private Sub lnkBtnSaveInv_Click(sender As Object, e As EventArgs) Handles lnkBtnSaveInv.Click
        Dim strNoBilSem As String = Trim(txtNoInvSem.Text.TrimEnd)
        If fKemasKini(strNoBilSem) Then
            fGlobalAlert("Maklumat telah disimpan!", Me.Page, Me.[GetType]())
        Else
            fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
        End If
    End Sub

    Private Function fKemasKini(ByVal strNoInvSem) As Boolean

        Dim strSql As String
        Dim dbconn As New DBKewConn
        Dim blnSuccess As Boolean = True
        Dim dtInvDt As New DataTable

        Try

            Dim dtTkhMhnInv As DateTime = CDate(Trim(txtTkhMohon.Text.TrimEnd))
            Dim strTkhMhnInv As String = dtTkhMhnInv.ToString("yyyy-MM-dd")
            Dim strIDPmhn As String = Trim(Session("ssusrID"))
            Dim strPerhatian As String = Trim(txtPerhatian.Value.TrimEnd)
            Dim strNoRujuk As String = Trim(txtNoRujukan.Value.TrimEnd)
            'Dim IDPenerima As String = Trim(txtIDPenerima.Text.TrimEnd)
            'Dim NamaPenerima As String = Trim(ddlNamaPenerima1.SelectedItem.ToString)
            Dim strAlamat1 As String = Trim(txtAlmt1.Value.TrimEnd)
            Dim strAlamat2 As String = Trim(txtAlmt2.Value.TrimEnd)
            Dim strBdr As String = Trim(txtBandar.Value.TrimEnd)
            Dim strPoskod As String = Trim(txtPoskod.Value.TrimEnd)
            Dim KodNegara As String = Trim(ddlNegara.SelectedValue.TrimEnd)
            Dim KodNegeri As String = Trim(ddlNegeri.SelectedValue.TrimEnd)
            Dim strEmel As String = Trim(txtEmel.Value.TrimEnd)
            Dim strNoTel As String = Trim(txtNoTel.Value.TrimEnd)
            Dim strNoFax As String = Trim(txtNoFax.Value.TrimEnd)
            Dim strBank As String = Trim(ddlBank.SelectedValue.TrimEnd)
            Dim strTujuan As String = Trim(txtTujuan.Value.TrimEnd)
            Dim strTkhKonMula As String
            Dim strTkhKonTamat As String
            Dim intTempKon As Integer

            Dim blnKontrak As Boolean = CBool(rbKontrak.SelectedValue)


            If txtTkhMula.Text <> "" Then
                Dim dtTkhMula As Date = DateTime.ParseExact(Trim(txtTkhMula.Text.TrimEnd), "dd/MM/yyyy", CultureInfo.CurrentCulture)
                strTkhKonMula = dtTkhMula.ToString("yyyy-MM-dd")
            Else
                strTkhKonMula = ""
            End If

            If txtTkhTamat.Text <> "" Then
                Dim dtTkhTamat As Date = DateTime.ParseExact(Trim(txtTkhTamat.Text.TrimEnd), "dd/MM/yyyy", CultureInfo.CurrentCulture)
                strTkhKonTamat = dtTkhTamat.ToString("yyyy-MM-dd")
            Else
                strTkhKonTamat = ""
            End If

            If txtTemKon.Text = "" Then intTempKon = 0 Else intTempKon = CInt(txtTemKon.Text)

            Dim footerTrans = gvInvDt.FooterRow
            Dim decTotJum As Decimal = CDec(CType(footerTrans.FindControl("lblTotJum"), Label).Text)

            'Update AR01_Bil
            strSql = "UPDATE AR01_Bil Set AR01_UtkPerhatian = @UtkPerhatian, AR01_NoRujukan = @NoRujuk, AR01_Jumlah = @Jumlah,
AR01_Almt1 = @ALMT1, AR01_Almt2 = @ALMT2, 
AR01_Bandar = @BANDAR, AR01_Poskod = @POSKOD, KodNegara =@KODNEGARA, KodNegeri =@KODNEGERI,
AR01_Emel = @EMEL, AR01_NoTel = @NOTEL, AR01_NoFaks = @NOFAKS, AR01_KodBank = @KODBANK, AR01_Tujuan = @TUJUAN, AR01_TkhKonDari = @AR01_TkhKonDari, AR01_TkhKonHingga =@AR01_TkhKonHingga, AR01_TempohKontrak = @AR01_TempohKontrak, AR01_Kontrak =@AR01_Kontrak
WHERE AR01_NoBilSem = @NoInv"

            Dim paramSql() As SqlParameter =
                {
                New SqlParameter("@NoInv", strNoInvSem),
                New SqlParameter("@UTKPERHATIAN", strPerhatian),
                New SqlParameter("@NoRujuk", strNoRujuk),
                New SqlParameter("@Jumlah", decTotJum),
                New SqlParameter("@ALMT1", strAlamat1),
                New SqlParameter("@ALMT2", strAlamat2),
                New SqlParameter("@BANDAR", strBdr),
                New SqlParameter("@POSKOD", strPoskod),
                New SqlParameter("@KODNEGARA", KodNegara),
                New SqlParameter("@KODNEGERI", KodNegeri),
                New SqlParameter("@EMEL", strEmel),
                New SqlParameter("@NOTEL", strNoTel),
                New SqlParameter("@NOFAKS", strNoFax),
                New SqlParameter("@KODBANK", strBank),
                New SqlParameter("@TUJUAN", strTujuan),
                New SqlParameter("@AR01_TkhKonDari", IIf(strTkhKonMula = "", DBNull.Value, strTkhKonMula)),
                New SqlParameter("@AR01_TkhKonHingga", IIf(strTkhKonTamat = "", DBNull.Value, strTkhKonTamat)),
                New SqlParameter("@AR01_TempohKontrak", intTempKon),
                New SqlParameter("@AR01_Kontrak", blnKontrak)
                }

            dbconn.sConnBeginTrans()
            If Not dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                blnSuccess = False
                Exit Try
            Else

                'AUDIT LOG
                sLogMedan = "AR01_UtkPerhatian|AR01_NoRujukan|AR01_Jumlah|AR01_Almt1|AR01_Almt2|AR01_Bandar|AR01_Poskod|KodNegara|KodNegeri|AR01_Emel|AR01_NoTel|AR01_NoFaks|AR01_KodBank|AR01_Tujuan|AR01_TkhKonDari|AR01_TkhKonHingga|AR01_TempohKontrak|AR01_Kontrak|AR01_NoBilSem"
                sLogBaru = strPerhatian & "|" & strNoRujuk & "|" & decTotJum & "|" & strAlamat1 & "|" & strAlamat2 & "|" & strBdr & "|" & strPoskod & "|" & KodNegara & "|" & KodNegeri & "|" & strEmel & "|" & strNoTel & "|" & strNoFax & "|" & strAlamat1 & "|" & strAlamat2 & "|" & strBdr & "|" & strPoskod & "|" & KodNegara & "|" & KodNegeri & "|" & strEmel & "|" & strNoTel & "|" & strNoFax & "|" & strBank & "|" & strTujuan & "|" & strTkhKonMula & "|" & strTkhKonTamat & "|" & intTempKon & "|" & blnKontrak & ""

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
                    New SqlParameter("@InfoTable", "AR01_Bil"),
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

            Dim intBil As Integer
            Dim strKodKW, strKodKO, strPTJ, strKodKP, strKodVot, strPerkara As String
            Dim decKuantiti, decKdrHrg, decJum As Decimal

            'DELETE AR01_BilDT
            strSql = "DELETE FROM AR01_BilDt WHERE AR01_NoBilSem = '" & strNoInvSem & "'"
            If Not dbconn.fUpdateCommand(strSql) > 0 Then
                blnSuccess = False
                Exit Try
            End If

            'INSERT AR01_BilDT
            For Each gvTransInvrow As GridViewRow In gvInvDt.Rows
                intBil = intBil + 1
                strKodKW = TryCast(gvTransInvrow.FindControl("ddlKW"), DropDownList).SelectedValue
                strKodKO = TryCast(gvTransInvrow.FindControl("ddlKO"), DropDownList).SelectedValue
                strPTJ = TryCast(gvTransInvrow.FindControl("ddlPTj"), DropDownList).SelectedValue
                strKodKP = TryCast(gvTransInvrow.FindControl("ddlKP"), DropDownList).SelectedValue
                strKodVot = TryCast(gvTransInvrow.FindControl("ddlVot"), DropDownList).SelectedValue
                strPerkara = TryCast(gvTransInvrow.FindControl("txtPerkara"), TextBox).Text
                decKuantiti = CDec(TryCast(gvTransInvrow.FindControl("txtKuantiti"), TextBox).Text)
                decKdrHrg = CDec(TryCast(gvTransInvrow.FindControl("txtHarga"), TextBox).Text)
                decJum = CDec(TryCast(gvTransInvrow.FindControl("lblJumlah"), Label).Text)



                strSql = " INSERT INTO AR01_BilDT(AR01_NoBilSem,AR01_NoBil,AR01_Bil,KodKw,KodKO,KodPTJ,KodKP,KodVot,AR01_Perkara,AR01_Kuantiti,AR01_kadarHarga,AR01_Jumlah)" &
                        "values (@NOBILSEM, @NOBIL,@BIL,@kodKw, @KodKO, @KodPTJ, @KodKP, @KodVot, @Perkara, @Kuantiti,@kadarHarga,@Jumlah)"

                Dim paramSql2() As SqlParameter =
                    {
                     New SqlParameter("@NOBILSEM", strNoInvSem),
                     New SqlParameter("@NOBIL", strNoInvSem),
                     New SqlParameter("@BIL", intBil),
                     New SqlParameter("@kodKw", strKodKW),
                     New SqlParameter("@KodKO", strKodKO),
                     New SqlParameter("@KodPTJ", strPTJ),
                     New SqlParameter("@KodKP", strKodKP),
                     New SqlParameter("@KodVot", strKodVot),
                     New SqlParameter("@Perkara", strPerkara),
                     New SqlParameter("@Kuantiti", decKuantiti),
                     New SqlParameter("@kadarHarga", decKdrHrg),
                     New SqlParameter("@Jumlah", decJum)
                    }

                If Not dbconn.fInsertCommand(strSql, paramSql2) > 0 Then
                    blnSuccess = False
                    Exit Try
                Else
                    'AUDIT LOG
                    sLogMedan = "AR01_NoBilSem|AR01_NoBil|AR01_Bil|KodKw|KodKO|KodPTJ|KodKP|KodVot|AR01_Perkara|AR01_Kuantiti|AR01_kadarHarga|AR01_Jumlah"
                    sLogBaru = strNoInvSem & "|" & strNoInvSem & "|" & intBil & "|" & strKodKW & "|" & strKodKO & "|" & strPTJ & "|" & strKodKP & "|" & strKodVot & "|" & strPerkara & "|" & decKuantiti & "|" & decKdrHrg & "|" & decJum & ""

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
                        New SqlParameter("@InfoTable", "AR01_BilDT"),
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

    'Private Sub lnkBtnSaveDoc_Click(sender As Object, e As EventArgs) Handles lnkBtnSaveDoc.Click
    '    Dim strNoBilSem As String = Trim(txtNoInvSem.Text.TrimEnd)
    '    If fKemasKiniDoc(strNoBilSem) Then
    '        fGlobalAlert("Lampiran telah dikemaskini!", Me.Page, Me.[GetType]())
    '    Else
    '        fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
    '    End If
    'End Sub

    Private Function fKemasKiniDoc(ByVal strNoInvSem) As Boolean
        Dim strSql As String
        Dim paramSql As SqlParameter()
        Dim dbconn As New DBKewConn
        Dim blnSuccess As Boolean = True
        Dim intIdBil = Trim(hidIdBil.Value.TrimEnd)
        Dim strKodSub As String = Request.QueryString("KodSub")
        Dim strKodModul As String = strKodSub.Substring(0, 2)
        Try
            'Check changes in existing record
            If ViewState("dtLampiran") IsNot Nothing Then
                Dim dt As New DataTable
                dt = TryCast(ViewState("dtLampiran"), DataTable)

                '"Select  AR11_ID, AR11_Path, AR11_NamaDok, AR11_Status from AR11_Lampiran where AR01_NoBilSem = '" & strNoInvSem & "' and AR11_Status = 1 order by AR11_Bil "

                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim blnStatus As Boolean = CBool(dt.Rows(i)("AR11_Status"))
                    If blnStatus = False Then
                        Dim intId As Integer = CInt(dt.Rows(i)("AR11_ID").ToString)
                        'Dim intIdBil As Integer = CInt(dt.Rows(i)("AR01_IdBil").ToString)



                        'Dim strNamaDok = Trim(dt.Rows(i)("AR11_NamaDok").ToString.TrimEnd)
                        Dim strFoldPath = Trim(dt.Rows(i)("AR11_Path").ToString.TrimEnd)
                        Dim strTkhToday = Now.ToString("yyyyMMdd")
                        Dim strArcPath = strFoldPath & "Archive\"

                        'Dim strExt = Path.GetExtension(strNamaDok)
                        'Dim strFileName = Path.GetFileNameWithoutExtension(strNamaDok)

                        'Dim folderPath As String = Server.MapPath($"~/Upload/Document/AR/InvoisCukai/{txtNoInvSem.Text}/")
                        'Dim tarikhDelete As String = Now.ToString("ddMMyyyyHHmmss")


                        Dim strNamaDok As String = intIdBil & "-" & intId
                        Dim filepathfile = strFoldPath + strNamaDok
                        'Dim renameFile = strFileName & "_" & tarikhDelete & "_forDelete" & strExt
                        'Dim filepath = Request.Url.Authority + Request.ApplicationPath + filepathfile
                        Dim destfilepathfile = strArcPath & "\" & strNamaDok

                        If Not Directory.Exists(strArcPath) Then
                            Directory.CreateDirectory(strArcPath)
                        End If


                        Dim fileInfo As New FileInfo(filepathfile)
                        If fileInfo.Exists Then
                            'Dim dbconn As New DBKewConn
                            strSql = "update AR11_Lampiran set AR11_Status = @Status, AR11_Path = @Path where AR11_ID  = @Id "
                            paramSql = {
                                        New SqlParameter("@Id", intId),
                                        New SqlParameter("@Status", 0),
                                        New SqlParameter("@Path", strArcPath)
                                        }

                            If Not dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                                blnSuccess = False
                            End If

                            Try
                                fileInfo.MoveTo(destfilepathfile)

                            Catch ex As Exception
                                blnSuccess = False
                            End Try
                        End If
                    End If
                Next
            End If

            'Muat Naik lampiran baru 
            Try
                Dim intIdLamp As Integer
                Dim intBil As Integer
                For i As Integer = 0 To Request.Files.Count - 1
                    intBil += 1
                    Dim PostedFile As HttpPostedFile = Request.Files(i)

                    If PostedFile.ContentLength > 0 Then
                        Dim FileName As String = System.IO.Path.GetFileName(PostedFile.FileName)
                        Dim strExt As String = Path.GetExtension(FileName)
                        Dim folderPath As String = Server.MapPath("~/Upload/Document/" & strKodModul & "/" & intIdBil & "/")
                        Dim strConType = PostedFile.ContentType

                        strSql = "INSERT INTO AR11_Lampiran (AR01_IdBil, AR01_NoBilSem, AR11_NamaDok, AR11_Bil, AR11_Path , AR11_ContentType , AR11_Status)
VALUES (@IdBil, @NoInvSem, @NamaDok, @Bil, @Path, @ConType, @Status)"

                        Dim paramSql4() As SqlParameter =
                {
                New SqlParameter("@IdBil", intIdBil),
                New SqlParameter("@NoInvSem", strNoInvSem),
                 New SqlParameter("@NamaDok", FileName),
                 New SqlParameter("@Bil", intBil),
                 New SqlParameter("@Path", folderPath),
                 New SqlParameter("@ConType", strConType),
                 New SqlParameter("@Status", 1)
                }

                        If Not dbconn.fInsertCommand(strSql, paramSql4, intIdLamp) > 0 Then
                            blnSuccess = False
                            Exit Try
                        End If

                        If Not Directory.Exists(folderPath) Then
                            Directory.CreateDirectory(folderPath)
                        End If

                        FileName = intIdBil & "-" & intIdLamp & strExt
                        PostedFile.SaveAs(folderPath & FileName)
                        Dim bytFileContent As Byte() = fReadFile(folderPath & "/" & FileName)
                        Dim SrcPath = folderPath & "/" & intIdBil & "-" & intIdLamp

                        If fEncFile(SrcPath, bytFileContent) Then
                            File.Delete(folderPath & "/" & FileName)
                        Else
                            blnSuccess = False
                            Exit Try
                        End If

                    End If
                Next

            Catch ex As Exception
                blnSuccess = False
                Exit Try
            End Try

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

    Private Sub gvLamp_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvLamp.RowDeleting
        Dim dbconn As New DBKewConn
        Dim blnSuccess As Boolean = True
        Try
            Dim index As Integer = e.RowIndex
            Dim selectedRow As GridViewRow = gvLamp.Rows(index)

            Dim intId As Integer = CInt(CType(selectedRow.FindControl("lblId"), Label).Text.TrimEnd)
            Dim intIdBil As Integer = CInt(CType(selectedRow.FindControl("lblIdBil"), Label).Text.TrimEnd)
            Dim strFoldPath = CType(selectedRow.FindControl("lblDokPath"), Label).Text.TrimEnd
            Dim strTkhToday = Now.ToString("yyyyMMdd")
            Dim strArcPath = strFoldPath & "Archive\"

            Dim strNamaDok As String = intIdBil & "-" & intId
            Dim filepathfile = strFoldPath + strNamaDok
            Dim destfilepathfile = strArcPath & "\" & strNamaDok

            If Not Directory.Exists(strArcPath) Then
                Directory.CreateDirectory(strArcPath)
            End If


            Dim fileInfo As New FileInfo(filepathfile)
            If fileInfo.Exists Then
                'Dim dbconn As New DBKewConn
                Dim strSql = "update AR11_Lampiran set AR11_Status = @Status, AR11_Path = @Path where AR11_ID  = @Id "
                Dim paramSql() As SqlParameter = {
                            New SqlParameter("@Id", intId),
                            New SqlParameter("@Status", 0),
                            New SqlParameter("@Path", strArcPath)
                            }

                dbconn.sConnBeginTrans()
                If Not dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                    blnSuccess = False
                End If

                Try
                    fileInfo.MoveTo(destfilepathfile)

                Catch ex As Exception
                    blnSuccess = False
                    Exit Try
                End Try

            End If

        Catch ex As Exception
            blnSuccess = False
        End Try

        If blnSuccess Then
            dbconn.sConnCommitTrans()
            sLoadInvLamp(Trim(txtNoInvSem.Text.TrimEnd))
        Else
            dbconn.sConnRollbackTrans()
            fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
        End If
    End Sub

    Private Sub gvInvDt_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvInvDt.RowDeleting
        Try
            Dim rowIndex As Integer = 0
            Dim index As Integer = Convert.ToInt32(e.RowIndex)

            If ViewState("dtInvDt") IsNot Nothing Then
                Dim dtCurrentTable As DataTable = CType(ViewState("dtInvDt"), DataTable)
                Dim drCurrentRow As DataRow = Nothing

                If dtCurrentTable.Rows.Count > 0 Then
                    dtCurrentTable.Rows(index).Delete()
                    dtCurrentTable.AcceptChanges()
                    ViewState("dtInvDt") = dtCurrentTable
                    gvInvDt.DataSource = dtCurrentTable
                    gvInvDt.DataBind()
                End If
            Else
                sCleargvInvDt()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub sCleargvInvDt()
        gvInvDt.DataSource = New List(Of String)
        gvInvDt.DataBind()
    End Sub

    Private Sub sClearGvLst()
        gvLst.DataSource = New List(Of String)
        gvLst.DataBind()
    End Sub

    Private Sub gvLamp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvLamp.SelectedIndexChanged
        Try
            Dim row As GridViewRow = gvLamp.SelectedRow
            Dim strKodModul = Request.QueryString("KodSub").Substring(0, 2)

            Dim intIdBil As String = CType(gvLamp.SelectedRow.FindControl("lblIdBil"), Label).Text.TrimEnd
            Dim intId As String = CType(gvLamp.SelectedRow.FindControl("lblId"), Label).Text.TrimEnd

            Dim url As String = "../../View_File.aspx?KodModul=" & strKodModul & "&intIdBil=" & intIdBil & "&Id=" & intId
            Dim fullURL As String = (Convert.ToString("window.open('") & url) + "', '_blank', 'height=800,width=850,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=yes,titlebar=no' );"
            ScriptManager.RegisterStartupScript(Me, GetType(String), "OPEN_WINDOW", fullURL, True)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub UploadButton_Click(sender As Object, e As EventArgs)
        If FileUpload1.HasFile Then

            Try
                Dim filename As String = FileUpload1.FileName

                'FileUpload1.SaveAs(Server.MapPath("~/uploads/") + FileUpload1.FileName)
                'FileUploadedLabel.Text = "File name: " & FileUpload1.PostedFile.FileName & "<br>" + FileUpload1.PostedFile.ContentLength & " kb<br>" & "Content type: " + FileUpload1.PostedFile.ContentType
            Catch ex As Exception
                'FileUploadedLabel.Text = "ERROR: " & ex.Message.ToString()
            End Try
        Else
            'FileUploadedLabel.Text = "You have not specified a file."
        End If
    End Sub

    Private Sub gvLst_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvLst.SelectedIndexChanged
        Try
            Dim row As GridViewRow = gvLst.SelectedRow

            Dim strNoInvSem As String = CType(gvLst.SelectedRow.FindControl("lblNoInvSem"), Label).Text.TrimEnd
            Dim intIdBil As String = CType(gvLst.SelectedRow.FindControl("lblIdBil"), Label).Text.TrimEnd
            Dim strKodStat As String = CType(gvLst.SelectedRow.FindControl("lblKodStat"), Label).Text.TrimEnd

            hidIdBil.Value = intIdBil
            sClearFields()
            sLoadInv(strNoInvSem)
            sLoadInvDt(strNoInvSem)
            sLoadInvLamp(strNoInvSem)

            'divList.Visible = False
            'divDetail.Visible = True

            If strKodStat = "01" Then
                lnkBtnSaveInv.Visible = True
                lBtnHapus.Visible = True
            Else
                lnkBtnSaveInv.Visible = False
                lBtnHapus.Visible = False
            End If

            divList.Attributes.Add("style", "display:none")
            divDetail.Attributes.Add("style", "display:block")

        Catch ex As Exception

        End Try


        'Try
        '    If e.CommandName = "Select" Then
        '        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        '        Dim selectedRow As GridViewRow = gvLst.Rows(index)

        '        Dim strNoInvSem As String = CType(selectedRow.FindControl("lblNoInvSem"), Label).Text.TrimEnd
        '        Dim intIdBil As String = CInt(CType(selectedRow.FindControl("lblIdBil"), Label).Text.TrimEnd)

        '        hidIdBil.Value = intIdBil
        '        sLoadInv(strNoInvSem)
        '        sLoadInvDt(strNoInvSem)
        '        sLoadInvLamp(strNoInvSem)

        '        divList.Visible = False
        '        divDetail.Visible = True
        '    End If

        'Catch ex As Exception

        'End Try


    End Sub

    Protected Sub lnkBtnBack_Click(sender As Object, e As EventArgs) Handles lnkBtnBack.Click

        fLoadInvKew()

        divList.Attributes.Add("style", "display:block")
        divDetail.Attributes.Add("style", "display:none")
    End Sub

    'Private Sub lBtnAdd_Click(sender As Object, e As EventArgs) Handles lBtnAdd.Click

    '    Try
    '        Dim file = FileUpload1.PostedFile
    '        Dim myUploadedFiles As List(Of MyFile) = Nothing
    '        If file IsNot Nothing AndAlso FileUpload1.PostedFile.FileName <> "" Then
    '            Dim content = New Byte(file.ContentLength - 1) {}
    '            file.InputStream.Read(content, 0, content.Length)

    '            If ViewState("UploadedFiles") Is Nothing Then
    '                myUploadedFiles = New List(Of MyFile)()
    '            Else
    '                myUploadedFiles = CType(ViewState("UploadedFiles"), List(Of MyFile))
    '            End If

    '            myUploadedFiles.Add(New MyFile() With {
    '                .Content = content,
    '                .ContentType = file.ContentType,
    '                .Name = file.FileName
    '            })
    '            ViewState("UploadedFiles") = myUploadedFiles


    '            Dim dt As New DataTable
    '            dt = TryCast(ViewState("dtLampiran"), DataTable)

    '            'Dim strSql = "Select  AR01_IdBil, AR11_ID, AR11_Path, AR11_NamaDok,AR11_ContentType, AR11_Status from AR11_Lampiran where AR01_NoBilSem = '" & strNoInvSem & "' and AR11_Status = 1 order by AR11_Bil "

    '            Dim strPath As String = Path.GetDirectoryName(file.FileName) & "\"
    '            Dim strFileName As String = Path.GetFileName(file.FileName)
    '            Dim strContType As String = file.ContentType


    '            Dim GuidId As Guid
    '            GuidId = Guid.NewGuid

    '            dt.Rows.Add(GuidId, 0, 0, strPath, strFileName, strContType, 0)

    '            gvLamp.DataSource = dt
    '            gvLamp.DataBind()

    '        Else
    '            ViewState("UploadedFiles") = Nothing
    '        End If

    '        'uploadedFileList.DataSource = myUploadedFiles
    '        'uploadedFileList.DataBind()

    '    Catch ex As Exception

    '    End Try

    'End Sub

    Protected Sub lkbCommandAction_Command(ByVal sender As Object, ByVal e As CommandEventArgs)
        Try
            If e.CommandArgument Is Nothing Then
            Else
                'Dim myUploadedFiles As List(Of MyFile) = Nothing

                'If ViewState("UploadedFiles") Is Nothing Then
                '    myUploadedFiles = New List(Of MyFile)()
                'Else
                '    myUploadedFiles = CType(ViewState("UploadedFiles"), List(Of MyFile))
                'End If

                ''Dim removeId As Guid = CType(e.CommandArgument, Guid)
                'Dim removeId As Guid = New Guid(DirectCast(e.CommandArgument, String))
                'Dim fileToRemove As MyFile = myUploadedFiles.FirstOrDefault(Function(ff) ff.Id = removeId)

                'If fileToRemove IsNot Nothing Then
                '    myUploadedFiles.Remove(fileToRemove)
                'End If



                'uploadedFileList.DataSource = myUploadedFiles
                'uploadedFileList.DataBind()

                Dim dt As New DataTable
                dt = CType(ViewState("dtLampiran"), DataTable)

                Dim removeId As Guid = New Guid(DirectCast(e.CommandArgument, String))
                ' Dim fileToRemove As MyFile = myUploadedFiles.FirstOrDefault(Function(ff) ff.Id = removeId)
                'Dim DrResult As Array = dt.Select("GUID='" & removeId.ToString & "'")

                Dim foundRow As DataRow() = dt.Select("GUID='" & removeId.ToString & "'")
                If foundRow.Count > 0 Then foundRow(0).Delete()

                gvLamp.DataSource = dt
                gvLamp.DataBind()

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function fSetDtLamp() As DataTable

        Try
            Dim dt As DataTable = New DataTable()
            Dim dr As DataRow = Nothing
            dt.Columns.Add(New DataColumn("GUID", GetType(Guid)))
            dt.Columns.Add(New DataColumn("AR01_IdBil", GetType(Integer)))
            dt.Columns.Add(New DataColumn("AR11_ID", GetType(String)))
            dt.Columns.Add(New DataColumn("AR11_Path", GetType(String)))
            dt.Columns.Add(New DataColumn("AR11_NamaDok", GetType(String)))
            dt.Columns.Add(New DataColumn("AR11_ContentType", GetType(String)))
            dt.Columns.Add(New DataColumn("AR11_Status", GetType(String)))

            Return dt

        Catch ex As Exception

        End Try

    End Function

    Private Sub gvLamp_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvLamp.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim boolStatus As Boolean = CBool(CType(e.Row.FindControl("lblStatus"), Label).Text)
                Dim lbtnSelect As LinkButton = CType(e.Row.FindControl("lbtnSelect"), LinkButton)

                If boolStatus = False Then
                    lbtnSelect.Visible = False
                Else
                    lbtnSelect.Visible = True
                End If

            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lbtnDelete_Command(sender As Object, e As CommandEventArgs)
        Try
            Dim dt As New DataTable
            dt = CType(ViewState("dtLampiran"), DataTable)

            Dim removeId As Guid = New Guid(DirectCast(e.CommandArgument, String))
            Dim foundRow As DataRow() = dt.Select("GUID='" & removeId.ToString & "'")
            If foundRow.Count > 0 Then foundRow(0).Delete()

            gvLamp.DataSource = dt
            gvLamp.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lbtnUpload_Click(sender As Object, e As EventArgs) Handles lbtnUpload.Click
        Dim strKodSub As String = Request.QueryString("KodSub")
        Dim strKodModul As String = strKodSub.Substring(0, 2)
        Dim intIdBil As Integer = CInt(hidIdBil.Value)
        Dim strNoInvSem As String = Trim(txtNoInvSem.Text.TrimEnd)
        Dim blnSuccess As Boolean = True
        Dim intIdLamp As Integer
        Dim dbconn As New DBKewConn()

        If FileUpload1.HasFile Then
            Try
                Dim FileName As String = FileUpload1.FileName
                Dim strExt As String = Path.GetExtension(FileName)
                Dim folderPath As String = Server.MapPath("~/Upload/Document/" & strKodModul & "/" & intIdBil & "/")
                Dim strConType = FileUpload1.PostedFile.ContentType

                Dim strSql = "INSERT INTO AR11_Lampiran (AR01_IdBil, AR01_NoBilSem, AR11_NamaDok, AR11_Bil, AR11_Path , AR11_ContentType , AR11_Status)
VALUES (@IdBil, @NoInvSem, @NamaDok, @Bil, @Path, @ConType, @Status)"

                Dim paramSql4() As SqlParameter =
        {
        New SqlParameter("@IdBil", intIdBil),
        New SqlParameter("@NoInvSem", strNoInvSem),
         New SqlParameter("@NamaDok", FileName),
         New SqlParameter("@Bil", vbNull),
         New SqlParameter("@Path", folderPath),
         New SqlParameter("@ConType", strConType),
         New SqlParameter("@Status", 1)
        }

                dbconn.sConnBeginTrans()

                If Not dbconn.fInsertCommand(strSql, paramSql4, intIdLamp) > 0 Then
                    blnSuccess = False
                    Exit Try
                End If

                If Not Directory.Exists(folderPath) Then
                    Directory.CreateDirectory(folderPath)
                End If

                FileName = intIdBil & "-" & intIdLamp & strExt
                FileUpload1.SaveAs(folderPath & FileName)
                Dim bytFileContent As Byte() = fReadFile(folderPath & "/" & FileName)
                Dim SrcPath = folderPath & "/" & intIdBil & "-" & intIdLamp

                If fEncFile(SrcPath, bytFileContent) Then
                    File.Delete(folderPath & "/" & FileName)
                Else
                    blnSuccess = False
                    Exit Try
                End If

            Catch ex As Exception
                blnSuccess = False
            End Try

            If blnSuccess Then
                dbconn.sConnCommitTrans()
                sLoadInvLamp(strNoInvSem)
            Else
                dbconn.sConnRollbackTrans()
                fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
            End If

        End If

    End Sub

    Private Sub lBtnHapus_Click(sender As Object, e As EventArgs) Handles lBtnHapus.Click
        Dim dbconn As New DBKewConn
        Dim blnSuccess As Boolean = True
        Dim StatDok As String = "07"
        Dim strNoInvSem As String = Trim(txtNoInvSem.Text.TrimEnd)
        Dim paramSql() As SqlParameter
        Dim strNoStaf As String = Session("ssusrID")
        Try

            Dim strSql = "update AR01_Bil set AR01_StatusDok = @StatDok where AR01_NoBilSem = @NoBilSem"

            paramSql =
                    {
                        New SqlParameter("@StatDok", StatDok),
                        New SqlParameter("@NoBilSem", strNoInvSem)
                    }

            dbconn.sConnBeginTrans()

            If Not dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                blnSuccess = False
                Exit Try

            Else
                'AUDIT LOG
                sLogMedan = "AR01_NoBilSem|AR01_StatusDok"
                sLogBaru = strNoInvSem & "|" & StatDok & ""

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
                    New SqlParameter("@InfoTable", "AR01_Bil"),
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

            strSql = "INSERT INTO AR06_STATUSDOK(AR06_NOBILSEM,AR06_NoBil, AR06_StatusDok,AR06_Tarikh,AR06_Ulasan, AR06_NoStaf)" &
                             "VALUES (@NOBILSEM,@NOBIL, @STATUSDOK,@TARIKH,@ULASAN, @NOSTAF)"

            paramSql =
                {
                New SqlParameter("@NOBILSEM", strNoInvSem),
                 New SqlParameter("@NOBIL", strNoInvSem),
                 New SqlParameter("@STATUSDOK", "07"),
                 New SqlParameter("@TARIKH", Now.ToString("yyyy-MM-dd")),
                 New SqlParameter("@ULASAN", " - "),
                 New SqlParameter("@NOSTAF", strNoStaf)
                }

            If Not dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                blnSuccess = False
                Exit Try
            End If


        Catch ex As Exception
            blnSuccess = False
        End Try


        If blnSuccess Then
            dbconn.sConnCommitTrans()
            fGlobalAlert("Rekod telah dihapuskan!", Me.Page, Me.[GetType]())
            fLoadInvKew()
            divList.Attributes.Add("style", "display:block")
            divDetail.Attributes.Add("style", "display:none")
        Else
            dbconn.sConnRollbackTrans()
            fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())

        End If


    End Sub

    Private Sub txtTkhMula_TextChanged(sender As Object, e As EventArgs) Handles txtTkhMula.TextChanged
        If txtTkhMula.Text <> "" AndAlso txtTkhTamat.Text <> "" Then
            Dim dtTkhMula As Date = DateTime.ParseExact(Trim(txtTkhMula.Text.TrimEnd), "dd/MM/yyyy", CultureInfo.CurrentCulture)
            Dim dtTkhTamat As Date = DateTime.ParseExact(Trim(txtTkhTamat.Text.TrimEnd), "dd/MM/yyyy", CultureInfo.CurrentCulture)
            txtTemKon.Text = (dtTkhTamat - dtTkhMula).TotalDays + 1
        End If
    End Sub

    Private Sub txtTkhTamat_TextChanged(sender As Object, e As EventArgs) Handles txtTkhTamat.TextChanged
        If txtTkhMula.Text <> "" AndAlso txtTkhTamat.Text <> "" Then
            Dim dtTkhMula As Date = DateTime.ParseExact(Trim(txtTkhMula.Text.TrimEnd), "dd/MM/yyyy", CultureInfo.CurrentCulture)
            Dim dtTkhTamat As Date = DateTime.ParseExact(Trim(txtTkhTamat.Text.TrimEnd), "dd/MM/yyyy", CultureInfo.CurrentCulture)
            txtTemKon.Text = (dtTkhTamat - dtTkhMula).TotalDays + 1
        End If
    End Sub

    Private Sub rbKontrak_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rbKontrak.SelectedIndexChanged
        If rbKontrak.SelectedValue = "True" Then
            btnCalMula.Disabled = False
            btnCalTamat.Disabled = False

            txtTkhMula.Text = ""
            txtTkhTamat.Text = ""


        Else
            btnCalMula.Disabled = True
            btnCalTamat.Disabled = True

            txtTkhMula.Text = ""
            txtTkhTamat.Text = ""

        End If

        ddlJenTemp.SelectedIndex = 0
    End Sub


    'Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
    '    Try


    '        For i As Integer = 0 To Request.Files.Count - 1

    '            Dim PostedFile As HttpPostedFile = Request.Files(i)

    '            If PostedFile.ContentLength > 0 Then
    '                Dim FileName As String = System.IO.Path.GetFileName(PostedFile.FileName)




    '            End If
    '        Next

    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub sClearFields()
        txtNoInvSem.Text = ""
        txtStatus.Text = "PERMOHONAN "
        ddlBank.SelectedValue = 0
        ddlJenis.SelectedValue = 0
        ddlKat.SelectedValue = 0

        ddlNegara.SelectedValue = 0
        ddlNegeri.SelectedValue = 0

        txtTujuan.Value = ""

        txtNoRujukan.Value = ""
        txtAlmt1.Value = ""
        txtAlmt2.Value = ""
        txtBandar.Value = ""
        txtPoskod.Value = ""
        txtNoTel.Value = ""
        txtNoFax.Value = ""
        txtEmel.Value = ""
        txtPerhatian.Value = ""
        gvInvDt.DataSource = New List(Of String)
        gvInvDt.DataBind()

        txtNamaPenerima.Text = ""
        txtNamaPenerima.Visible = False
        'ddlNmPenerima.Visible = True
        'ddlNmPenerima.SelectedValue = 0

        txtIDPenerima.Value = ""

        ddlJenis.Enabled = True
        ddlKat.Enabled = True
        rbKontrak.SelectedValue = "False"
        txtTkhMula.Text = ""
        txtTkhTamat.Text = ""
    End Sub

    Private Sub btnPadamMula_ServerClick(sender As Object, e As EventArgs) Handles btnPadamMula.ServerClick
        txtTkhMula.Text = ""
        txtTemKon.Text = ""
    End Sub

    Private Sub btnPadamTamat_ServerClick(sender As Object, e As EventArgs) Handles btnPadamTamat.ServerClick
        txtTkhTamat.Text = ""
        txtTemKon.Text = ""
    End Sub

    Private Sub fBindDdlFilStat()
        Dim dbconn As New DBKewConn
        Try
            Dim strSql As String = "select KodStatus, Butiran from AR_StatusDok where KodStatus in (01,03,05)"

            Using ds = dbconn.fSelectCommand(strSql)
                ddlFilStat.DataSource = ds
                ddlFilStat.DataTextField = "Butiran"
                ddlFilStat.DataValueField = "KodStatus"
                ddlFilStat.DataBind()

                ddlFilStat.Items.Insert(0, New ListItem("- KESELURUHAN -", "0"))
                ddlFilStat.SelectedValue = "01"
            End Using

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlFilStat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFilStat.SelectedIndexChanged
        fLoadInvKew()
    End Sub
End Class