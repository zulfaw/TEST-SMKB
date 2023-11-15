<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Peruntukan_Universiti.aspx.vb" Inherits="SMKB_Web_Portal.Peruntukan_Universiti" %>

<%@ Import Namespace="System.Web.Mail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="panel panel-default">
                <div class="panel-heading">
                    Maklumat Peruntukan Universiti
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="GvTopPanel">
                            <div style="float: left; margin-top: 8px; margin-left: 10px;">
                                <label class="control-label" for="">Jumlah rekod :</label>&nbsp;<label class="control-label" id="lblJumRekod" runat="server" style="color: mediumblue;"></label>
                            </div>
                        </div>
                        <asp:GridView ID="gvPeruntukan" runat="server" AllowSorting="True"
                            AutoGenerateColumns="False" BorderColor="#999999" BorderStyle="Double"
                            BorderWidth="1px" CssClass="table table-striped table-bordered table-hover"
                            EmptyDataText=" " Font-Size="8pt" HeaderStyle-BackColor="#6699FF"
                            Height="100%" ShowFooter="True"
                            ShowHeaderWhenEmpty="True" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="Bil">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBil" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />
                                    </ItemTemplate>
                                    <ItemStyle Width="30px" />
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label runat="server" Text="Tahun" />
                                        <asp:LinkButton ID="lnkTahun" runat="server" CommandName="Sort" CommandArgument="BG17_Tahun"><span style="color:#4B4B4B"></span></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTahun" runat="server" Text='<%# Eval("BG17_Tahun")%>' />                                       
                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>


                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lblAsal" runat="server" Text="Agihan KPT (RM)" />&nbsp;
                            <asp:LinkButton ID="lnkAsal" runat="server" CommandName="Sort" CommandArgument="BG17_AgihanKPT"><span style="color:#4B4B4B"></span></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblAgih" runat="server" Text='<%#Eval("BG17_AgihanKPT", "{0:N2}")%>' />
                                        <asp:TextBox ID="txtAgihanKPT" runat="server" CssClass="form-control rightAlign" Text='<%#Eval("BG17_AgihanKPT", "{0:N2}")%>' Visible="false" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lblTB" runat="server" Text="Lulus KPT (RM)" />&nbsp;
                            <asp:LinkButton ID="lnkTB" runat="server" CommandName="Sort" CommandArgument="BG17_LulusKPT"><span style="color:#4B4B4B"></span></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblLulus" runat="server" Text='<%#Eval("BG17_LulusKPT", "{0:N2}")%>' />
                                        <asp:TextBox ID="txtLulusKPT" runat="server" CssClass="form-control rightAlign" Text='<%#Eval("BG17_LulusKPT", "{0:N2}")%>' Visible="false" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lblKG" runat="server" Text="Reserved (RM)" />&nbsp;
                            <asp:LinkButton ID="lnkKG" runat="server" CommandName="Sort" CommandArgument="BG17_Reserved"><span style="color:#4B4B4B"></span></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblReserved" runat="server" Text='<%#Eval("BG17_Reserved", "{0:N2}")%>' />
                                        <asp:TextBox ID="txtReserved" runat="server" CssClass="form-control rightAlign" Text='<%#Eval("BG17_Reserved", "{0:N2}")%>' Visible="false" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lblBF" runat="server" Text="Geran Kerajaan (RM)" />&nbsp;
                            <asp:LinkButton ID="lnkBF" runat="server" CommandName="Sort" CommandArgument="BG17_GeranKerajaan"><span style="color:#4B4B4B"></span></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblGeran" runat="server" Text='<%#Eval("BG17_GeranKerajaan", "{0:N2}")%>' />
                                        <asp:TextBox ID="txtGeran" runat="server" CssClass="form-control rightAlign" Text='<%#Eval("BG17_GeranKerajaan", "{0:N2}")%>' Visible="false" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="btn-xs" ToolTip="Pilih" Visible="false">
                                          <i class="fas fa-edit"></i>
                                        </asp:LinkButton>

                                    </ItemTemplate>

                                    <FooterStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle Font-Bold="True" ForeColor="#000000" />
                            <HeaderStyle BackColor="#6699FF" />
                        </asp:GridView>
                    </div>

                </div>

                <div class="row">

                    <div style="text-align: center; margin-bottom: 10px;">
                        <asp:LinkButton ID="linkSimpan" runat="server" CssClass="btn ">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                        </asp:LinkButton>
                    </div>
                    <div class="row">
                        <!--<asp:Button ID="btnSendMail" runat="server" Text="Send Mail" Visible="false" />-->

                    </div>

                </div>


                <asp:Button ID="btnPopup2" runat="server" Style="display: none;" />
                <ajaxToolkit:ModalPopupExtender ID="mpePnlSenarai" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlEdit" TargetControlID="btnPopup2" BehaviorID="mpe2">
                </ajaxToolkit:ModalPopupExtender>
                <asp:Panel ID="pnlEdit" runat="server" BackColor="White" Width="50%">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Jumlah Peruntukan
                    <button id="btnTutup" runat="server" class="btnNone " title="Tutup">
                        <i class="far fa-window-close fa-2x"></i>
                    </button>
                        </div>
                        <div class="panel-body">
                            <table>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>
                                        <label class="control-label" for="">Tahun :</label></td>
                                    <td><strong>
                                        <asp:Label ID="lblYear" runat="server"></asp:Label>
                                    </strong></td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>
                                        <label class="control-label" for="">Agihan KPT :</label></td>
                                    <td>
                                        <asp:TextBox ID="txtAgih" runat="server" CssClass="form-control rightAlign" Width="60%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>Lulus KPT:</td>
                                    <td>
                                        <asp:TextBox ID="txtLulus" runat="server" CssClass="form-control rightAlign" Width="60%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>
                                        <label class="control-label" for="">Geran Kerajaan :</label></td>
                                    <td>
                                        <asp:TextBox ID="txtGeran" runat="server" CssClass="form-control rightAlign" Width="60%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>
                                        <label class="control-label" for="">Reserved :</label></td>
                                    <td>
                                        <asp:TextBox ID="txtReserved" runat="server" CssClass="form-control rightAlign" Width="60%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td colspan="3">
                                        <div style="text-align: center; margin-bottom: 10px;">
                                            <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn ">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                                            </asp:LinkButton>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>




</asp:Content>
