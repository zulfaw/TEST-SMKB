Imports System.Drawing
Imports System.Data.SqlClient
Imports System.Web
Public Class MaklumatDefendLPU
    Inherits System.Web.UI.Page

    'Private Shared dvButiran As New DataView
    Protected TotalRecProg, TotalRecDt As String
    Protected strStatMohon As String
    Protected strMsg, strNotice As String
    Protected TotalRec As String
    Protected lblPeruntukanPrev1, lblPeruntukanPrev, lblPerbelanjaanPrev, lblAnggaranSyorNow, lblAnggaranMintaNext, lblAnggaranSyorNext As String

    Public Shared dvPermohonan As New DataView
    Public Shared dv As New DataView
    Dim ds As New DataSet
    Public Shared dvPTJ As New DataView

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                fBindgvAbmMaster()
                fBindgvAbmMaster2()
                fBindgvAbmMaster3()
                fBindgvAbmMaster4()

            End If
        Catch ex As Exception
            sErrLog(ex.HResult, ex.Message, fGetFormName(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session("ssusrID"), Session("PcIP"), Session("PcName"), Session("SysVer"))
        End Try

    End Sub


    Private Sub fBindgvAbmMaster()
        Try
            Dim ds As New DataSet
            Dim strSql As String = "SELECT kodvot, Butiran FROM MK_Vot WHERE  Klasifikasi = 'H2' AND Status = '1' AND LEFT(KodVot,1) = 1 ORDER BY KodVot"

            Dim dbconn As New DBKewConn

            ds = dbconn.fSelectCommand(strSql)
            Dim dt As DataTable = ds.Tables(0)
            gvAbmMaster.DataSource = dt
            gvAbmMaster.DataBind()

        Catch ex As Exception
            sErrLog(ex.HResult, ex.Message, fGetFormName(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session("ssusrID"), Session("PcIP"), Session("PcName"), Session("SysVer"))
        End Try
    End Sub
    Private Sub fBindgvAbmMaster2()
        Try
            Dim ds As New DataSet
            Dim strSql As String = "SELECT kodvot, Butiran FROM MK_Vot WHERE  Klasifikasi = 'H2' AND Status = '1' AND LEFT(KodVot,1) = 2 ORDER BY KodVot"

            Dim dbconn As New DBKewConn

            ds = dbconn.fSelectCommand(strSql)
            Dim dt As DataTable = ds.Tables(0)
            gvAbmMasterv2.DataSource = dt
            gvAbmMasterv2.DataBind()

        Catch ex As Exception
            sErrLog(ex.HResult, ex.Message, fGetFormName(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session("ssusrID"), Session("PcIP"), Session("PcName"), Session("SysVer"))
        End Try
    End Sub

    Private Sub fBindgvAbmMaster3()
        Try
            Dim ds As New DataSet
            Dim strSql As String = "SELECT kodvot, Butiran FROM MK_Vot WHERE  Klasifikasi = 'H2' AND Status = '1' AND LEFT(KodVot,1) = 3 ORDER BY KodVot"

            Dim dbconn As New DBKewConn

            ds = dbconn.fSelectCommand(strSql)
            Dim dt As DataTable = ds.Tables(0)
            'gvAbmMasterv3.DataSource = dt
            ' gvAbmMasterv3.DataBind()

        Catch ex As Exception
            sErrLog(ex.HResult, ex.Message, fGetFormName(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session("ssusrID"), Session("PcIP"), Session("PcName"), Session("SysVer"))
        End Try
    End Sub

    Private Sub fBindgvAbmMaster4()
        Try
            Dim ds As New DataSet
            Dim strSql As String = "SELECT kodvot, Butiran FROM MK_Vot WHERE  Klasifikasi = 'H2' AND Status = '1' AND LEFT(KodVot,1) = 4 ORDER BY KodVot"

            Dim dbconn As New DBKewConn

            ds = dbconn.fSelectCommand(strSql)
            Dim dt As DataTable = ds.Tables(0)
            'gvAbmMasterv4.DataSource = dt
            'gvAbmMasterv4.DataBind()

        Catch ex As Exception
            sErrLog(ex.HResult, ex.Message, fGetFormName(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session("ssusrID"), Session("PcIP"), Session("PcName"), Session("SysVer"))
        End Try
    End Sub


    Protected Sub lbtnKembali_Click(sender As Object, e As EventArgs) Handles lbtnKembali.Click

        'check if statuskemaskini is true and No mohon is already created

        Response.Redirect("~/FORMS/BAJET/PERMOHONAN BAJET/Cadangan_Agihan_Defend.aspx")

    End Sub

End Class

