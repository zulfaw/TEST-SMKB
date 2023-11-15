﻿<%@ Page Title="" Language="vb" AutoEventWireup="false"  CodeBehind="Laporan_Peruntukan_KW.aspx.vb" Inherits="SMKB_Web_Portal.Laporan_Peruntukan_KW" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %> 

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
    <title></title>
    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
         <%: Scripts.Render("~/bundles/jQuery1120") %>
     <%: Scripts.Render("~/bundles/bootstrap336") %>
    <%: Scripts.Render("~/bundles/bootstrap337") %>
     <%: Styles.Render("~/bundles/fontAwesome") %>
         <%: Styles.Render("~/bundles/bootstrap") %>
    </asp:PlaceHolder>
    <link rel="stylesheet" type="text/css" href="../../../Content/Site.css">
</head>
  <body>
      <form id="form1" runat="server">
       <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <div class="row">
    <div class="col-sm-3 col-md-6">
       
        <div class="panel panel-default" style="width:600px">
    <div class="panel-body">
         <table style="text-align: right;" border="0">
          <tr style="text-align:left">
               <td style="width: 300px;">&nbsp;<label for="txtStartDate" style="font-weight:700;" >Tarikh Dari : </label><br />
                   <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" Text="<%# System.DateTime.Now%>" Width="100px"></asp:TextBox>
                   <cc1:CalendarExtender ID="calStartDate" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtStartDate" TodaysDateFormat="dd/MM/yyyy" />
                 </td>
              
               <td>&nbsp;<label style="font-weight:700;">Hingga : </label><br />
                   <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" Text="<%# System.DateTime.Now%>" Width="100px"></asp:TextBox>
                   <cc1:CalendarExtender ID="calEndDate" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtEndDate" TodaysDateFormat="dd/MM/yyyy" />
                  </td>
              
          </tr>
          <tr style="height:60px;text-align:left;">
               <td style="width: 300px;">&nbsp;<label style="font-weight:700;">KW dari : </label><br />
                   <asp:DropDownList ID="ddlKwFrom" runat="server" CssClass="form-control" style="width: 280px;">
                   </asp:DropDownList>
                  </td>
               
               <td style="width:300px;">&nbsp;<label style="font-weight:700;">Hingga : </label><br />
                   <asp:DropDownList ID="ddlKWTo" runat="server" CssClass="form-control" style="width: 280px;">
                   </asp:DropDownList>
                   </td>
             
          </tr>
          <tr style="height:60px;text-align:left;">
               <td style="width: 300px">&nbsp;<label style="font-weight:700;">PTj Dari : </label><br />
                   <asp:DropDownList ID="ddlPTjFrom" runat="server" CssClass="form-control" style="width: 280px;">
                   </asp:DropDownList>
                  </td>
               
               <td style="width: 300px">&nbsp;<label style="font-weight:700;">Hingga : </label><br />
                   <asp:DropDownList ID="ddlPTjTo" runat="server" CssClass="form-control" style="width: 280px;">
                   </asp:DropDownList>
                  </td>
               
          </tr>
          <tr style="height:60px;text-align:left">
               <td style="width: 300px" >&nbsp;<label style="font-weight:700;">Vot Dari : </label><br />
                   <asp:DropDownList ID="ddlVotFrom" runat="server" CssClass="form-control" style="width: 280px;">
                   </asp:DropDownList>
                  </td>
               
               <td style="width: 300px">&nbsp;<label style="font-weight:700;">Hingga : </label><br />
                   <asp:DropDownList ID="ddlVotTo" runat="server" CssClass="form-control" style="width: 280px;">
                   </asp:DropDownList>
                  </td>
              
          </tr>
             <tr style="height:40px;">
               <td style="width: 300px">&nbsp;</td>
               <td style="width: 361px" >
                   <asp:Button ID="btnPapar0" runat="server" CssClass="btn btn-default" Width="100px"  Text="Papar" ToolTip="SSRS custom filter (rdl)" />
                   &nbsp;&nbsp;</td>
              
              
          </tr>
             
      </table>
        
        </div>


 

    

  </div>

     </div>
  
  </div>
     </form>
    <div>
          
                
    </div>   </body>
    </html>
 
    

