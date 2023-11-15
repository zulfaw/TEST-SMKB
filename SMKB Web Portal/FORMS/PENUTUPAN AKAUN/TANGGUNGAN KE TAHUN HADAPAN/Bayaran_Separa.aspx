<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Bayaran_Separa.aspx.vb" Inherits="SMKB_Web_Portal.Bayaran_Separa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>


        </ContentTemplate>
       </asp:UpdatePanel>


</asp:Content>
