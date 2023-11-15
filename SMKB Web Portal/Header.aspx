<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Header.aspx.vb" Inherits="SMKB_Web_Portal.Header" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Header" runat="server">
                <div class="modul-header">
                <span class="nav-btn" onclick="openNav()">&#9776;</span>
                <div class="back-btn" onclick="location.href='~/index.aspx';">
                    <i class="fa fa-angle-left" style="font-size:36px"></i>
                    <a>Manu Utama</a>
                </div>
                <div class="account-info">
                    <div class="action">
                        <div class="profile" onclick="menuToggle();">
                            <img src="../assets/images/user.png" />
                        </div>
                        <div class="menu">
                            <h3>Your Name<br /><span>System Admin</span></h3>
                            <ul>
                                <li>
                                    <a href="#">My profile</a>
                                </li>
                                </li>
                                <li>
                                    <a href="#">Logout</a>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
</asp:Content>


