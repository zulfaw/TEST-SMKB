Public Class summary2_ABM7
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub lblCetak_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblCetak.Click
        Dim url = "BG_ABM7 - all"
        Dim strSVR = $"https://reporting.utem.edu.my/ReportServer/Pages/ReportViewer.aspx?%2fS2%2fS2D%2f{url}"
        Dim fullURL As String = $"window.open('{strSVR}', '_blank', 'width=' + (parseInt(window.innerWidth) * 0.3) + ',height=' + (parseInt(window.innerHeight) * .3) + ',status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=yes,titlebar=no' );"
        ScriptManager.RegisterStartupScript(Me, GetType(String), "OPEN_WINDOW", fullURL, True)

    End Sub

    Protected Sub lblCetakdS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblCetakdS.Click

        Dim url = "BG_ABM7_DSA"
        Dim strSVR = $"https://reporting.utem.edu.my/ReportServer/Pages/ReportViewer.aspx?%2fS2%2fS2D%2f{url}"
        Dim fullURL As String = $"window.open('{strSVR}', '_blank', 'width=' + (parseInt(window.innerWidth) * 0.3) + ',height=' + (parseInt(window.innerHeight) * .3) + ',status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=yes,titlebar=no' );"
        ScriptManager.RegisterStartupScript(Me, GetType(String), "OPEN_WINDOW", fullURL, True)

    End Sub

    Protected Sub lblCetakdB_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblCetakdB.Click

        Dim url = "BG_ABM7_DBR"
        Dim strSVR = $"https://reporting.utem.edu.my/ReportServer/Pages/ReportViewer.aspx?%2fS2%2fS2D%2f{url}"
        Dim fullURL As String = $"window.open('{strSVR}', '_blank', 'width=' + (parseInt(window.innerWidth) * 0.3) + ',height=' + (parseInt(window.innerHeight) * .3) + ',status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=yes,titlebar=no' );"
        ScriptManager.RegisterStartupScript(Me, GetType(String), "OPEN_WINDOW", fullURL, True)

    End Sub

    Protected Sub lblCetakoff_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblCetakoff.Click

        Dim url = "BG_ABM7_OFF"
        Dim strSVR = $"https://reporting.utem.edu.my/ReportServer/Pages/ReportViewer.aspx?%2fS2%2fS2D%2f{url}"
        Dim fullURL As String = $"window.open('{strSVR}', '_blank', 'width=' + (parseInt(window.innerWidth) * 0.3) + ',height=' + (parseInt(window.innerHeight) * .3) + ',status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=yes,titlebar=no' );"
        ScriptManager.RegisterStartupScript(Me, GetType(String), "OPEN_WINDOW", fullURL, True)

    End Sub

End Class
