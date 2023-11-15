<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="9701.aspx.vb" Inherits="SMKB_Web_Portal._9701" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      
<script src='../assets/bootstrap/js/bootstrap.min.js'></script>

        <div class="sub-modul-title">
            <h5><asp:Label ID="NamaSubMenu" runat="server" Text="Label"></asp:Label></h5>
        </div>

            <div class="tab">
                <asp:Repeater ID="rptSubMenu" runat="server">
                   <ItemTemplate>
                      <!-- Repeater item content -->
                       <button class="tablinks" onclick="openTab(event, '<%#Trim(Eval("Kod_Sub_Menu")) %>')"><%#Trim(Eval("Nama_Sub_Menu")) %></button>
                    </ItemTemplate>
                </asp:Repeater>
            </div> 

            <!-- FIRST TAB  -->
            <div id="970201" class="tabcontent">
                <h3>Paris</h3>
                <p>Paris is the capital of France.</p> 
            </div>



            <div id="970202" class="tabcontent">
                <h3>Paris</h3>
                <p>Paris is the capital of France.</p> 
            </div>

            <div id="970203" class="tabcontent">
                <h3>Tokyo</h3>
                <p>Tokyo is the capital of Japan.</p>
            </div>

    <script>
        function openTab(evt, subMenu) {
            // Declare all variables
            var i, tabcontent, tablinks;

            //alert(subMenu);

            // Get all elements with class="tabcontent" and hide them
            tabcontent = document.getElementsByClassName("tabcontent");
            for (i = 0; i < tabcontent.length; i++) {
                tabcontent[i].style.display = "none";
            }

            // Get all elements with class="tablinks" and remove the class "active"
            tablinks = document.getElementsByClassName("tablinks");
            for (i = 0; i < tablinks.length; i++) {
                tablinks[i].className = tablinks[i].className.replace(" active", "");

            }

            // Show the current tab, and add an "active" class to the button that opened the tab
            document.getElementById(subMenu).style.display = "block";
            evt.currentTarget.className += " active";
            //evt.currentTarget.className 

            //document.getElementById("defaultOpen").click();
        }



    </script>      

    

</asp:Content>