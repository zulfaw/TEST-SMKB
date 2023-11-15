<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Vwr_Peruntukan_KW.aspx.vb" Inherits="SMKB_Web_Portal.Viewer_Peruntukan_KW" %>

<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%--<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>--%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
   
    

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>          
        <rsweb:ReportViewer ID="ReportViewer1" runat="server"  Width="100%" Height="100%" Font-Names="Verdana" Font-Size="8pt" ProcessingMode="Remote" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" SizeToReportContent="True" BackColor="#D2D2D2" ShowBackButton="False" ShowRefreshButton="False" >
            <ServerReport ReportPath="/S2/S2D" ReportServerUrl="http://reporting.utem.edu.my/reportserver" />
        </rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>
