Imports System.Web.Optimization

Public Class Global_asax
    Inherits HttpApplication

    Sub Application_Start(sender As Object, e As EventArgs)
        ' Fires when the application is started
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)
        ' Creare Application state variables
        Application("TotalApplications") = 0
        Application("TotalUserSessions") = 0
        'increment TotalApplication by 1
        Application("TotalApplications") = Integer.Parse(Application("TotalApplications")) + 1

    End Sub

    Sub Session_Start(sender As Object, e As EventArgs)
        Application("TotalUserSessions") = Integer.Parse(Application("TotalUserSessions")) + 1
    End Sub


    Sub Session_End(sender As Object, e As EventArgs)
        Application("TotalUserSessions") = Integer.Parse(Application("TotalUserSessions")) - 1
    End Sub

    Protected Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        Dim runTime As System.Web.Configuration.HttpRuntimeSection = CType(System.Web.Configuration.WebConfigurationManager.GetSection("system.web/httpRuntime"), System.Web.Configuration.HttpRuntimeSection)
        Dim maxRequestLength As Integer = (runTime.MaxRequestLength - 100) * 1024
        Dim context As HttpContext = (CType(sender, HttpApplication)).Context

        If context.Request.ContentLength > maxRequestLength Then
            Dim provider As IServiceProvider = CType(context, IServiceProvider)
            Dim workerRequest As HttpWorkerRequest = CType(provider.GetService(GetType(HttpWorkerRequest)), HttpWorkerRequest)

            If workerRequest.HasEntityBody() Then
                Dim requestLength As Integer = workerRequest.GetTotalEntityBodyLength()
                Dim initialBytes As Integer = 0
                If workerRequest.GetPreloadedEntityBody() IsNot Nothing Then initialBytes = workerRequest.GetPreloadedEntityBody().Length

                If Not workerRequest.IsEntireEntityBodyIsPreloaded() Then
                    Dim buffer As Byte() = New Byte(511999) {}
                    Dim receivedBytes As Integer = initialBytes

                    While requestLength - receivedBytes >= initialBytes
                        initialBytes = workerRequest.ReadEntityBody(buffer, buffer.Length)
                        receivedBytes += initialBytes
                    End While

                    initialBytes = workerRequest.ReadEntityBody(buffer, requestLength - receivedBytes)
                End If
            End If

            context.Server.ClearError()
            'context.Response.Redirect("~/Error.aspx")
        End If
    End Sub



End Class