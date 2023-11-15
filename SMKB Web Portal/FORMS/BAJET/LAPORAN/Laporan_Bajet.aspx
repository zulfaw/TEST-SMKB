<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Laporan_Bajet.aspx.vb" Inherits="SMKB_Web_Portal.Laporan_Bajet" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div>
         <div class="row">
            <div class="panel panel-default" style="width:auto;overflow-x:scroll;" >
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            Laporan Bajet
                        </h3>
                    </div>
                    <div class="panel-body">
            
            <rsweb:ReportViewer ID="ReportViewer1" runat="server"  Width="100%" Height="100%" Font-Names="Verdana" 
                Font-Size="8pt" ProcessingMode="Remote" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" 
                SizeToReportContent="True" BackColor="#D2D2D2" ShowBackButton="False" ShowRefreshButton="False" >
                
            </rsweb:ReportViewer>
                </div>
                </div>
        </div>
    </div>
    </form>
</body>
</html>
