Imports Microsoft.Reporting.WebForms
Imports System.Globalization
Imports System.Threading
Imports System.Web.Configuration

Public Class Viewer_Peruntukan_KW
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            Dim strRptServerUrl As String = WebConfigurationManager.AppSettings("RptServerUrl")
            Dim strRptServerPath As String = WebConfigurationManager.AppSettings("RptServerPath")

            If Not IsPostBack Then
                Dim strStartDate As String
                Dim strEndDate As String
                Dim strKWFrom As String
                Dim strKWTo As String

                'Dim strstartdate2, strEndDate2 As Date

                strStartDate = Session("StartDate")
                strEndDate = Session("strEndDate")
                strKWFrom = Session("strKWFrom")
                strKWTo = Session("strKWTo")

                ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote
                ReportViewer1.ServerReport.ReportServerUrl = New Uri(strRptServerUrl)  'http://reporting.utem.edu.my/reportserver
                ReportViewer1.ServerReport.ReportPath = strRptServerPath & "/rpt_Peruntukan_KW"  '/S2/S2D/rpt_Peruntukan_KW

                Dim Param As Microsoft.Reporting.WebForms.ReportParameter() = New Microsoft.Reporting.WebForms.ReportParameter(3) {}
                Param(0) = New Microsoft.Reporting.WebForms.ReportParameter("StartDate", strStartDate)
                Param(1) = New Microsoft.Reporting.WebForms.ReportParameter("EndDate", strEndDate)
                Param(2) = New Microsoft.Reporting.WebForms.ReportParameter("KWFrom", strKWFrom)
                Param(3) = New Microsoft.Reporting.WebForms.ReportParameter("KWTo", strKWTo)

                ReportViewer1.ShowParameterPrompts = False
                ReportViewer1.ServerReport.SetParameters(Param)
                ReportViewer1.ServerReport.Refresh()
            End If

        Catch ex As Exception

        End Try

    End Sub

End Class