<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ViewDoc.aspx.vb" Inherits="SMKB_Web_Portal.ViewDoc" %>

<!DOCTYPE html>
<script src="<%=ResolveClientUrl("~/vendor/jquery/jquery.min.js")%>"></script>
<link href="<%=ResolveClientUrl("~/vendor/bootstrap/css/bootstrap.min.css")%>" rel="stylesheet" />
<link href="<%=ResolveClientUrl("~/Css/Style.css?ver=1.1")%>" rel="stylesheet" />
<link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css">
 <link href= "<%=ResolveClientUrl("~/vendor/StyleSheet1.css?ver=1.8")%>" rel="stylesheet" />
<html>
<head runat="server">
    <title></title>
</head>
    <style>

        body
        {
            font-family: "Century Gothic", Georgia, sans-serif important;
            font-size: 18px;
        }

        .GridPager a, .GridPager span
        {
            display: block;
            height: 25px;
            width: 25px;
            font-weight: bold;
            text-align: center;
            text-decoration: none;
        }
        .GridPager a
        {
            background-color: #f5f5f5;
            color: #969696;
            border: 1px solid #969696;
        }
        .GridPager span
        {
            background-color: #A1DCF2;
            color: #000;
            border: 1px solid #3AC0F2;
        }
    </style>
<body>
    <form id="form1" runat="server">
        <table cellpadding="2" cellspacing="2" border="0" style="width:100%">
            <tr align="left">
                <td class="SubTitle" style="width:8%"> No Pinjaman. : </td>                        
                <td style="width:10%"><asp:TextBox ID="txtNoPinj" runat="server" Width="60%" BackColor="#eeeeee" ReadOnly="true" CssClass="form-control"></asp:TextBox></td>            
                <td class="SubTitle" style="width:8%">Nama Staf : </td>     
                <td style="width:25%"><asp:TextBox ID="txtNama" runat="server" Width="80%" BackColor="#eeeeee" ReadOnly="true" CssClass="form-control"></asp:TextBox></td>
                <td class="SubTitle" style="width:8%">Pusat Tanggungjawab : </td>     
                <td style="width:15%"><asp:TextBox ID="txtNamaPtj" runat="server" Width="100%" BackColor="#eeeeee" ReadOnly="true" CssClass="form-control"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:TextBox Visible="false" textmode="MultiLine" ID="rtbResults" runat="server" Rows="5" ReadOnly="true"  CssClass="form-control"/>
                </td>
            </tr>
        </table>
        <div>
             <iframe id="ShowF" runat="server" style="height:100%;width:98%;display:block;margin-left: auto;margin-right: auto;position:fixed; overflow:hidden;"></iframe> 
        </div>
    </form>
</body>
</html>
