<%@ Page Language="vb" AutoEventWireup="false" Async="true" CodeBehind="index.aspx.vb" Inherits="SMKB_Web_Portal.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <!--LOCAL -->
    <script src="<%=ResolveClientUrl("~/Content/js/jquery.min.js")%> "></script>
    <link rel="stylesheet" href="<%=ResolveClientUrl("~/Content/css/bootstrap.min.css")%>" />
    <script src='<%=ResolveClientUrl("~/Content/js/bootstrap.min.js")%>'></script>
    <link rel="stylesheet" href="<%=ResolveClientUrl("~/Content/css/styles.css")%>" />
    <script src='<%=ResolveClientUrl("~/Content/script/script.js")%>'></script>
    <script src='<%=ResolveClientUrl("~/Content/js/selectize.min.js")%>'></script>
    <!-- FONT LINK -->
    <link href="Content/font-awesome/css/style.css" rel="stylesheet" />
</head>
<body>
    <%If Session("ssusrID") <> "" Then %> 
    <form id="form1" runat="server">
        <div class="header">
            <div class="header-logo">
                <img src="<%=ResolveClientUrl("~/assets/images/smkb-logo.svg")%>" />
            </div>
            <div class="account-info dark">
                <a><%=Session("ssusrName")%></a>
                <div class="action">
                    <div class="profile" onclick="menuToggle();">

                        <img src="https://portal.utem.edu.my/oas/directory/bak/image.asp?myStaff=<%=Session("ssusrID")%>" />

                    </div>

                    <div class="menu">
                        <h3><%=Session("ssusrName")%><br />
                            <span><%=Session("ssusrPost")%></span></h3>
                        <ul>
                            <li>
                                <i class="las la-sign-out-alt"></i>
                                <a href="#" id="logoutLink">Logout</a>
                            </li>
                        </ul>
                    </div>


                </div>
            </div>
        </div>

        <div class="dashboard-content">
            <div id="nav-placeholder">
            </div>
            <div class="row">

                <asp:Repeater ID="rptMenu" runat="server" OnItemCommand="rptMenu_ItemCommand">
                    <ItemTemplate>
                        <div class="col-md-3">
                            <asp:LinkButton ID="ASPPage" runat="server" CommandArgument='<%#Trim(Eval("Kod_Modul")) + "|" + Session("ssusrID") %>'>
                                <div id="menumain" runat="server" class="menu-card">
                                    <img src="<%#Trim(Eval("icon_location")) %>" />
                                    <p><%#Trim(Eval("Nama_Modul")) %></p>
                                </div>

                            </asp:LinkButton>
                        </div>


                    </ItemTemplate>
                </asp:Repeater>

            </div>
        </div>
        <div id="alert-box" class="btn">

        </div>
        <script>
            $(function () {
                $("#nav-placeholder").load("/nav.html");
            });


            function divClicked() {
                __doPostBack('menumain', '');
            }
    </script>

    <script>
        // Get a reference to the logout link
        var logoutLink = document.getElementById("logoutLink");

        // Add a click event listener to the logout link
        logoutLink.addEventListener("click", function (event) {
            event.preventDefault(); // Prevent the default link behavior

            // Perform logout-related tasks here, such as clearing cookies, session data, etc.

            // Close the window/tab
            window.close();
        });
    </script>

    </form>
    <%Else  %> 
    <script>
        alert("Sila Login Melalui Portal UTeM")
    </script>
    <%End If %> 
</body>
</html>
