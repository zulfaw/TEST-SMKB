<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Error.aspx.vb" Inherits="SMKB_Web_Portal._Error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>&nbsp;</h1>

    <script>
        function redirect() {
            location.href = 'javascript:history.go(-1)';
        }
    </script>

    <div style="margin-left: 85px; margin-top: 3px;">
        <p><b><i class="fas fa-exclamation-triangle fa-lg"></i>&nbsp;Saiz fail melebihi 500kb!</b></p>
        <%--<a href='javascript:history.go(-1)'>Kembali</a>--%>
        <asp:LinkButton runat="server" CssClass="btn" onclientclick='redirect()'>
                        <i class="fas fa-arrow-left fa-lg"></i>&nbsp;&nbsp;&nbsp;Kembali
                    </asp:LinkButton>
    </div>
</asp:Content>
