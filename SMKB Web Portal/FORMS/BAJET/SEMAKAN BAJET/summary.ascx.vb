Public Class summary
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        fBindAmaun_LulusKPT()

        fBindAmaun_Mohon()
        fBindAmaun_Bend()
        fBindAmaun_NC()
        fBindAmaun_LPU()

        fBindAmaun_Geran()
        fBindAmaun_Reseserved()

        If lblAmaunLPU.Text = "" Then
            lblAmaunLPU.Text = "0.00"
        End If


        lblBezaBen.Text = lblLulusKPT.Text - lblAmaunBend.Text
        lblBezaNC.Text = lblLulusKPT.Text - lblAmaunNC.Text
        lblBezaLPU.Text = lblLulusKPT.Text - lblAmaunLPU.Text
    End Sub

    Private Function fBindAmaun_LulusKPT() As String
        Dim strSql As String
        Try
            strSql = "SELECT ISNULL(SUM(ISNULL(BG17_LulusKPT,0)),0) as amaun FROM BG17_Agihan WHERE BG17_Tahun = '2022' "
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim strAmaunMohon As String

            ds = dbconn.fSelectCommand(strSql)

            If FormatNumber(ds.Tables(0).Rows(0).Item("amaun").ToString, 2) <> 0 Then
                lblLulusKPT.Text = FormatNumber(ds.Tables(0).Rows(0).Item("amaun").ToString, 2)
                strAmaunMohon = FormatNumber(ds.Tables(0).Rows(0).Item("amaun").ToString, 2)
            Else
                lblLulusKPT.Text = "0.00"
                strAmaunMohon = 0
            End If



            Return strAmaunMohon

        Catch ex As Exception
            sErrLog(ex.HResult, ex.Message, fGetFormName(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session("ssusrID"), Session("PcIP"), Session("PcName"), Session("SysVer"))
        End Try


    End Function

    Private Function fBindAmaun_Reseserved() As String
        Dim strSql As String
        Try
            strSql = "SELECT ISNULL(SUM(ISNULL(BG17_Reserved,0)),0) as amaun FROM BG17_Agihan WHERE BG17_Tahun = '2022' "
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim strAmaunMohon As String

            ds = dbconn.fSelectCommand(strSql)

            If FormatNumber(ds.Tables(0).Rows(0).Item("amaun").ToString, 2) <> 0 Then
                lblAmaunReserved.Text = FormatNumber(ds.Tables(0).Rows(0).Item("amaun").ToString, 2)
                strAmaunMohon = FormatNumber(ds.Tables(0).Rows(0).Item("amaun").ToString, 2)
            Else
                lblAmaunReserved.Text = "0.00"
                strAmaunMohon = 0
            End If

            Return strAmaunMohon

        Catch ex As Exception
            sErrLog(ex.HResult, ex.Message, fGetFormName(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session("ssusrID"), Session("PcIP"), Session("PcName"), Session("SysVer"))
        End Try


    End Function
    Private Function fBindAmaun_Geran() As String
        Dim strSql As String
        Try
            strSql = "SELECT ISNULL(SUM(ISNULL(BG17_GeranKerajaan,0)),0) as amaun FROM BG17_Agihan WHERE BG17_Tahun = '2022' "
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim strAmaunMohon As String

            ds = dbconn.fSelectCommand(strSql)

            If FormatNumber(ds.Tables(0).Rows(0).Item("amaun").ToString, 2) <> 0 Then
                lblAmaunGeran.Text = FormatNumber(ds.Tables(0).Rows(0).Item("amaun").ToString, 2)
                strAmaunMohon = FormatNumber(ds.Tables(0).Rows(0).Item("amaun").ToString, 2)
            Else
                lblAmaunGeran.Text = "0.00"
                strAmaunMohon = 0
            End If

            Return strAmaunMohon

        Catch ex As Exception
            sErrLog(ex.HResult, ex.Message, fGetFormName(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session("ssusrID"), Session("PcIP"), Session("PcName"), Session("SysVer"))
        End Try

    End Function
    Private Function fBindAmaun_Mohon() As String
        Dim strSql As String
        Try
            strSql = "SELECT ISNULL(SUM(ISNULL(BG20_AmaunMohon,0)),0) as amaun FROM BG01_Mohon_RV "
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim strAmaunMohon As String

            ds = dbconn.fSelectCommand(strSql)

            If FormatNumber(ds.Tables(0).Rows(0).Item("amaun").ToString, 2) <> 0 Then
                lblAmaunMohon.Text = FormatNumber(ds.Tables(0).Rows(0).Item("amaun").ToString, 2)
                strAmaunMohon = FormatNumber(ds.Tables(0).Rows(0).Item("amaun").ToString, 2)
            Else
                lblAmaunMohon.Text = "0.00"
                strAmaunMohon = 0
            End If

            Return strAmaunMohon

        Catch ex As Exception
            sErrLog(ex.HResult, ex.Message, fGetFormName(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session("ssusrID"), Session("PcIP"), Session("PcName"), Session("SysVer"))
        End Try
    End Function

    Private Function fBindAmaun_Bend() As String
        Dim strSql As String
        Try
            strSql = "SELECT ISNULL(SUM(ISNULL(BG20_AmaunSyorBendahari,0)),0) as amaun FROM BG01_Mohon_RV "
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim strAmaunMohon As String

            ds = dbconn.fSelectCommand(strSql)

            If FormatNumber(ds.Tables(0).Rows(0).Item("amaun").ToString, 2) <> 0 Then
                lblAmaunBend.Text = FormatNumber(ds.Tables(0).Rows(0).Item("amaun").ToString, 2)
                strAmaunMohon = FormatNumber(ds.Tables(0).Rows(0).Item("amaun").ToString, 2)
            Else
                lblAmaunBend.Text = "0.00"
                strAmaunMohon = 0
            End If
            Return strAmaunMohon

        Catch ex As Exception
            sErrLog(ex.HResult, ex.Message, fGetFormName(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session("ssusrID"), Session("PcIP"), Session("PcName"), Session("SysVer"))
        End Try
    End Function
    Private Function fBindAmaun_NC() As String
        Dim strSql As String
        Try
            strSql = "SELECT isnull(SUM(ISNULL(BG20_AmaunLulusNC,0)),0) as amaun FROM BG01_Mohon_RV "
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim strAmaunMohon As String

            ds = dbconn.fSelectCommand(strSql)

            Dim a = FormatNumber(ds.Tables(0).Rows(0).Item("amaun").ToString, 2)

            If FormatNumber(ds.Tables(0).Rows(0).Item("amaun").ToString, 2) <> 0 Then
                lblAmaunNC.Text = FormatNumber(ds.Tables(0).Rows(0).Item("amaun").ToString, 2)
                strAmaunMohon = FormatNumber(ds.Tables(0).Rows(0).Item("amaun").ToString, 2)
            Else
                lblAmaunNC.Text = "0.00"
                strAmaunMohon = 0
            End If
            Return strAmaunMohon

        Catch ex As Exception
            sErrLog(ex.HResult, ex.Message, fGetFormName(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session("ssusrID"), Session("PcIP"), Session("PcName"), Session("SysVer"))
        End Try
    End Function

    Private Function fBindAmaun_LPU() As String
        Dim strSql As String
        Try
            strSql = "SELECT isnull(SUM(ISNULL(BG20_AmaunLulusLPU,0)),0) as amaun FROM BG01_Mohon_RV "
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim strAmaunMohon As String

            ds = dbconn.fSelectCommand(strSql)

            Dim a = FormatNumber(ds.Tables(0).Rows(0).Item("amaun").ToString, 2)

            If FormatNumber(ds.Tables(0).Rows(0).Item("amaun").ToString, 2) <> 0 Then
                lblAmaunLPU.Text = FormatNumber(ds.Tables(0).Rows(0).Item("amaun").ToString, 2)
                strAmaunMohon = FormatNumber(ds.Tables(0).Rows(0).Item("amaun").ToString, 2)
            Else
                lblAmaunLPU.Text = "0.00"
                strAmaunMohon = 0
            End If
            Return strAmaunMohon

        Catch ex As Exception
            sErrLog(ex.HResult, ex.Message, fGetFormName(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session("ssusrID"), Session("PcIP"), Session("PcName"), Session("SysVer"))
        End Try
    End Function


End Class