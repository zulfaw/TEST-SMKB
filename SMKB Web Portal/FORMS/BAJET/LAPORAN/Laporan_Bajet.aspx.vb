Imports System.Web.Configuration
Imports Microsoft.Reporting.WebForms

Public Class Laporan_Bajet
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim NamaLaporan = Request.QueryString("NamaLaporan")
            Dim tarikhMula = ""
            Dim tarikhHingga = ""
            Dim KWFrom = ""
            Dim KWTo = ""
            Dim KpFrom = ""
            Dim KpTo = ""
            Dim PtjFrom = ""
            Dim PtjTo = ""
            Dim KoFrom = ""
            Dim KoTo = ""
            Dim votFrom = ""
            Dim votTo = ""
            Dim tahun As Integer
            Dim Param() = New ReportParameter() {}

            If NamaLaporan = "BG_BukuVot" Then
                'Buku Vot

                tarikhMula = Request.QueryString("StartDate")
                tarikhHingga = Request.QueryString("EndDate")
                KWFrom = Request.QueryString("KWFrom")
                KWTo = Request.QueryString("KWTo")
                KpFrom = Request.QueryString("KpFrom")
                KpTo = Request.QueryString("KpTo")
                PtjFrom = Request.QueryString("PtjFrom")
                PtjTo = Request.QueryString("PtjTo")
                KoFrom = Request.QueryString("KoFrom")
                KoTo = Request.QueryString("KoTo")
                votFrom = Request.QueryString("votFrom")
                votTo = Request.QueryString("votTo")

                Dim dtDate = CDate(tarikhMula)
                tahun = dtDate.Year

                tarikhMula = dtDate.ToString("yyyy-MM-dd")
                tarikhHingga = CDate(tarikhHingga).ToString("yyyy-MM-dd")
                Param = New ReportParameter() {
            New ReportParameter("tkhdr", tarikhMula),
            New ReportParameter("tkhke", tarikhHingga),
            New ReportParameter("kwdr", KWFrom),
            New ReportParameter("kwke", KWTo),
            New ReportParameter("ptjdr", PtjFrom),
            New ReportParameter("ptjke", PtjTo),
            New ReportParameter("kodr", KoFrom),
            New ReportParameter("koke", KoTo),
            New ReportParameter("votdr", votFrom),
            New ReportParameter("votke", votTo),
            New ReportParameter("kpdr", KpFrom),
            New ReportParameter("kpke", KpTo),
            New ReportParameter("ai_tahun", tahun),
            New ReportParameter("StafId", Session("ssusrID").ToString)
        }
            ElseIf NamaLaporan = "BG_BakiVot" Then
                tarikhMula = Request.QueryString("StartDate")
                KWFrom = Request.QueryString("KWFrom")
                KWTo = Request.QueryString("KWTo")
                KpFrom = Request.QueryString("KpFrom")
                KpTo = Request.QueryString("KpTo")
                PtjFrom = Request.QueryString("PtjFrom")
                PtjTo = Request.QueryString("PtjTo")
                KoFrom = Request.QueryString("KoFrom")
                KoTo = Request.QueryString("KoTo")
                votFrom = Request.QueryString("votFrom")
                votTo = Request.QueryString("votTo")

                Dim dt = CDate(tarikhMula)
                tahun = dt.Year

                Param = New ReportParameter() {
            New ReportParameter("ai_tahun", tahun),
            New ReportParameter("kwdr", KWFrom),
            New ReportParameter("kwke", KWTo),
            New ReportParameter("ptjdr", PtjFrom),
            New ReportParameter("ptjke", PtjTo),
            New ReportParameter("kodr", KoFrom),
            New ReportParameter("koke", KoTo),
            New ReportParameter("kpdr", KpFrom),
            New ReportParameter("kpke", KpTo),
            New ReportParameter("votdr", votFrom),
            New ReportParameter("votke", votTo),
            New ReportParameter("StafID", Session("ssusrID").ToString)
        }

            ElseIf NamaLaporan = "rpt_Peruntukan_KW" Then
                tarikhMula = Request.QueryString("StartDate")
                tarikhHingga = Request.QueryString("EndDate")
                KWFrom = Request.QueryString("KWFrom")
                KWTo = Request.QueryString("KWTo")

                Param = New ReportParameter() {
            New ReportParameter("StartDate", tarikhMula),
            New ReportParameter("EndDate", tarikhHingga),
            New ReportParameter("KWFrom", KWFrom),
            New ReportParameter("KWTo", KWTo)
        }
            End If


            Dim strRptServerUrl As String = WebConfigurationManager.AppSettings("RptServerUrl")
            Dim strRptServerPath As String = WebConfigurationManager.AppSettings("RptServerPath")


            Try
                ReportViewer1.ProcessingMode = ProcessingMode.Remote
                ReportViewer1.ServerReport.ReportServerUrl = New Uri(strRptServerUrl)
                ReportViewer1.ServerReport.ReportPath = $"{strRptServerPath}/{NamaLaporan}"
                ReportViewer1.ShowParameterPrompts = False
                ReportViewer1.ServerReport.SetParameters(Param)
                ReportViewer1.ServerReport.Refresh()
            Catch ex As Exception

            End Try
        End If


    End Sub



End Class