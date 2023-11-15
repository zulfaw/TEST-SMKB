Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Optimization

Public Class BundleConfig
    ' For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkID=303951
    Public Shared Sub RegisterBundles(ByVal bundles As BundleCollection)
        bundles.Add(New ScriptBundle("~/bundles/WebFormsJs").Include(
                        "~/Scripts/WebForms/WebForms.js",
                        "~/Scripts/WebForms/WebUIValidation.js",
                        "~/Scripts/WebForms/MenuStandards.js",
                        "~/Scripts/WebForms/Focus.js",
                        "~/Scripts/WebForms/GridView.js",
                        "~/Scripts/WebForms/DetailsView.js",
                        "~/Scripts/WebForms/TreeView.js",
                        "~/Scripts/WebForms/WebParts.js"))

        ' Order is very important for these files to work, they have explicit dependencies
        bundles.Add(New ScriptBundle("~/bundles/MsAjaxJs").Include(
                "~/Scripts/WebForms/MsAjax/MicrosoftAjax.js",
                "~/Scripts/WebForms/MsAjax/MicrosoftAjaxApplicationServices.js",
                "~/Scripts/WebForms/MsAjax/MicrosoftAjaxTimer.js",
                "~/Scripts/WebForms/MsAjax/MicrosoftAjaxWebForms.js"))

        ' Use the Development version of Modernizr to develop with and learn from. Then, when you’re
        ' ready for production, use the build tool at http://modernizr.com to pick only the tests you need
        bundles.Add(New ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"))

        ScriptManager.ScriptResourceMapping.AddDefinition("respond", New ScriptResourceDefinition() With {
                .Path = "~/Scripts/respond.min.js",
                .DebugPath = "~/Scripts/respond.js"})

        bundles.Add(New ScriptBundle("~/bundles/bootstrap336").Include(
                        "~/Scripts/bootstrap 3.3.6/bootstrap.min.js"))
        bundles.Add(New ScriptBundle("~/bundles/bootstrap337").Include(
                        "~/Scripts/bootstrap 3.3.7/bootstrap.min.js"))
        bundles.Add(New ScriptBundle("~/bundles/jQuery1120").Include(
                        "~/Scripts/jquery 1.12.0/jquery.min.js"))

        'bundles.Add(New StyleBundle("~/bundles/fontAwesome").Include(
        '                "~/Content/Font-Awesome-4_7_0/css/font-awesome.min.css"))
        'bundles.Add(New StyleBundle("~/bundles/fontAwesome").Include(
        '                "~/Content/Font-Awesome-5-0-6/css/fontawesome-all.min.css"))
        bundles.Add(New StyleBundle("~/bundles/fontAwesome-5-7-1").Include(
                        "~/Content/Font-Awesome-5-7-1/css/all.min.css"))
        bundles.Add(New StyleBundle("~/bundles/bootstrap").Include(
                        "~/Content/bootstrap.css"))
        bundles.Add(New ScriptBundle("~/bundles/nicescroll").Include(
                        "~/Scripts/jquery.nicescroll.min.js"))
        bundles.Add(New StyleBundle("~/bundles/Style").Include(
                        "~/Content/Style.css"))
        BundleTable.EnableOptimizations = False

    End Sub
End Class
