<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Logout.aspx.vb" Inherits="SMKB_Web_Portal.Logout" %>

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
    <webopt:bundlereference runat="server" path="~/Content/css" />

    <link href="Images/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <style type="text/css">
        
        @media (max-width: 767px)
        {
            
            .brand
            {
                text-align: left !important;
                font-size: 22px;
                padding-left: 20px;
                line-height: 50px !important;
            }
        }
       
        .HeaderText {

    position: absolute;
    top: 40px;
    left: 200px;
    font-size: 30px;
    font-weight:600;
    color: white;
    width: 700px;
    font-family: arial;
    border-bottom: 1px solid #ffffff;
    width: 90%;
    
}    
        .HeaderText:before {
    content: "SISTEM MAKLUMAT KEWANGAN BERSEPADU"; 
    font-family: Arial;
    
    left:-5px;
    position:absolute;
    top:0;
    text-decoration: none;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    letter-spacing: 1px;
    display: block;
    text-shadow: 3px 3px #000000;

 }

        #parent {
    text-align:center;   
    height:400px;
  
}
.block {
    
    width:50%;
    text-align:left;
    background-color:#FAFAD0;
    border-color:#FFD400;
    border:thin;
    border-width:1px;
    font-weight:bold;
}
.center {
    margin:auto;  
}

.rcorners {
    border-radius: 5px;
    border: 1px solid #4B4B4B;
    padding: 20px; 
    box-shadow: 0px 0px 5px #878787;
  
}


    </style>
</head>
<body>
    <form id="form1" runat="server">
        <nav class="navbar navbar-default navbar-fixed-top">
  <div class="container-fluid">
    <div class="navbar-header">
   
       <%-- <span class="navbar-brand">
          <img id="Image1" src="/SMKBNet/Images/logo.png">
          <span class="header-text"></span>
  </span>
        --%>
    </div>
   
      <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
    
          </div>
  </div>
</nav>
        <br /><br />
        <div id="parent">
            <div class="block center rcorners">
            <table  class="nav-justified block center " style="width:100%">
            <tr>
                <td style="text-align:center;"><asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></td>
            </tr>
            </table></div></div>
        
        <br />
    <div style="text-align:center;">  
        

        
    </div>
    </form>
</body>
</html>
