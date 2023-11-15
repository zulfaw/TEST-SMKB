<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="9702.aspx.vb" Inherits="SMKB_Web_Portal._9702" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">
    ...
    <asp:TextBox runat="server" ID="hidFresh" style="display:none;"></asp:TextBox>
    <script type="text/javascript">
        var isFresh = '<%=isFresh%>';
        var obj = document.getElementsByClassName("tablinks")[0];
        
        $(document).ready(async function () {
            <%--if ($('#<%=hidFresh.ClientID%>').val() == "Y") {
                obj.click();
            }--%>

            //Autoload first menu - 8 ogos 2023 
            if ($('#<%=hidFresh.ClientID%>').val() == "Y") {
                var status = await checkURLValidity($(obj).data("url"));
                if (status === true) {
                    obj.click();
                }
            }

            //Autoload first menu - 8 ogos 2023 
            async function checkURLValidity(url) {
                try {
                    const response = await fetch(url, { method: "HEAD" });
                   
                    if (response.status === 200) {
                        return true; // URL is valid
                    } else if (response.status === 404) {
                        return false; // URL returned a 404 error
                    } else {
                        throw new Error("URL check failed with status: " + response.status);
                    }
                } catch (error) {
                    console.error('Error:', error);
                    return false;
                }
            }
        });
    </script>
</asp:Content>
