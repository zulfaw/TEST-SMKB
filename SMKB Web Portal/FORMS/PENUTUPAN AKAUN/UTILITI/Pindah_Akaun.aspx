<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Pindah_Akaun.aspx.vb" Inherits="SMKB_Web_Portal.Pindah_Akaun" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>


    <script type="text/javascript">

        function fConfirm() {
            try {
           
                if (confirm('Adakah anda ingin meneruskan proses ini?')) {
                    return true;
                } else {
                    return false;
                }
            }
            catch (err) {
                return false
            }
        }

    </script>

    <style>
        @media (min-width: 1450px) {
            .panel {
                width: 60%;
            }
        }

        @media (max-width: 1450px) {
            .panel {
                width: 70%;
            }
        }

        @media (max-width: 1250px) {
            .panel {
                min-width: 100%;
            }
        }


        .modal {
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 0;
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
            -moz-opacity: 0.8;
        }

        .center {
            z-index: 1000;
            margin: 300px auto;
            padding: 10px;
            width: 130px;
            background-color: White;
            border-radius: 10px;
            filter: alpha(opacity=100);
            opacity: 1;
            -moz-opacity: 1;
        }

            .center img {
                height: 128px;
                width: 128px;
            }
        .auto-style1 {
            /*display: block;*/
        padding: 2px 2px;
            font-size: 12px;
            line-height: 1.428571429;
            color: #000000;
            vertical-align: middle;
            background-color: #ffffff;
            border: 1px solid #cccccc;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);
            box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);
            -webkit-transition: border-color ease-in-out 0.15s, box-shadow ease-in-out 0.15s;
            transition: border-color ease-in-out 0.15s, box-shadow ease-in-out 0.15s;
            width: 40%;
            margin-bottom: 0;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">

        <ContentTemplate>
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <table class="nav-justified">
                           
            <tr>
                <td style="width: 100px">&nbsp;</td>
                <td>
                    <asp:RadioButtonList ID="rbOption" runat="server" Height="25px" Width="300px" Autopostback = "true">
                        <asp:ListItem Selected="True" Text=" Pindah akaun yang tidak mempunyai kod projek " Value="0" />
                        <asp:ListItem Text=" Pindah akaun yang mempunyai kod projek " Value="1" />
                    </asp:RadioButtonList>


                </td>
            </tr>
                                <tr>
                                    <td style="width: 100px">&nbsp;</td>
                                    <td>&nbsp;</td>
                            </tr>
                                <tr>
                                    <td style="width: 100px">&nbsp;</td>
                                    <td>
                                        <asp:Panel ID="Panel1" runat="server" Visible="False">
                                        <div class="panel panel-default">
                                        <div class="panel-body">
                                        <div id="div1" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <label class="control-label" for="">
                                                        Kumpulan Wang:</label></td>
                                                    <td style="width: 300px"> <asp:DropDownList ID="ddlKW" runat="server" AutoPostBack="True" CssClass="form-control"  Width="110%">
                                                         </asp:DropDownList></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label class="control-label" for="">
                                                        Kod Projek:</label></td>
                                                    <td style="width: 300px"> <asp:DropDownList ID="ddlKP" runat="server" AutoPostBack="True" Width="110%" CssClass="auto-style1">
                                                    </asp:DropDownList></td>
                                                </tr>
                                            </table>
                                           
                                        </div> 
                                        </div> 
                                             </div> 
                                             </asp:Panel> 
                                        
                                    </td>
                                </tr>
                         
                             <tr>
                                    <td style="width: 100px">&nbsp;</td>
                                    <td>
                                        <div id="div2" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <label class="control-label" for="">
                                                        Daripada:</label></td>
                                                    <td style="width: 300px" > <asp:TextBox ID="txtDrpd" runat="server" class="form-control" ReadOnly="true" text-align="Right" Width="20%"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label class="control-label" for="">
                                                        Kepada:</label></td>
                                                    <td style="width: 300px"> <asp:TextBox ID="txtKpd" runat="server" class="form-control" ReadOnly="true" text-align="Right" Width="20%"></asp:TextBox></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>






                                <%--<tr style="height: 25px">
                                    <td>
                                        <label class="control-label" for="">
                                        Daripada:
                                        </label>
                                    </td>
                                    <td>
                                        <label class="control-label" for="">
                                        Tahun</label>
                                        <asp:TextBox ID="txtDrpd1" runat="server" class="form-control" ReadOnly="true" text-align="Right" Width="10%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="height: 25px">
                                    <td>
                                        <label class="control-label" for="">
                                        Kepada:
                                        </label>
                                    </td>
                                    <td>
                                        <label class="control-label" for="">
                                        Tahun</label>
                                        <asp:TextBox ID="txtKpd1" runat="server" class="form-control" ReadOnly="true" text-align="Right" Width="10%"></asp:TextBox>
                                    </td>
                                </tr>--%>
                                <tr style="height: 55px;">
                                    <td>&nbsp;</td>
                                    <td>
                                        <asp:Button ID="btnProses" runat="server" CssClass="btn" Text="Proses" ValidationGroup="btnProses" OnClientClick="return fConfirm();"/>
                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                            <ProgressTemplate>
                                                <div style="background-color: #D2D2D2; filter: alpha(opacity=80); opacity: 0.80; width: 100%; top: 0px; left: 0px; position: fixed; height: 100%;">
                                                </div>
                                                <div style="margin: auto; font-family: Trebuchet MS; filter: alpha(opacity=100); opacity: 1; font-size: small; vertical-align: middle; color: #FFFFFF; position: fixed; top: 50%; left: 50%; margin-top: -50px; margin-left: -100px;">
                                                    <table>
                                                        <tr>
                                                            <td><span style="font-family: Trebuchet MS; font-size: medium; font-weight: bold; color: Black">Sedang Diproses...</span></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <img src="../../../Images/loader.gif" alt="" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>
        </tr>
            
                            </tr>
            
                        </table>
                    </div>
                </div>
            </div>

          </table>
        </div>
              </div>        
    </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
