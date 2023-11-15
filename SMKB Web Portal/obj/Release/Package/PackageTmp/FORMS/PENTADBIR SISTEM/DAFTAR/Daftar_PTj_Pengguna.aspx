<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Daftar_PTj_Pengguna.aspx.vb" Inherits="SMKB_Web_Portal.Daftar_PTj_Pengguna" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

                <div class="well" style="width:600px; margin-left: 50px; margin-bottom: 60px;">
                        <table style="width: 100%;">
                            <tr>
                                <td style="width:70px;">No. Staf</td>
                                <td>
                                    <asp:DropDownList ID="ddlStaf" runat="server" AutoPostBack="True" CssClass="form-control" Style="width: 300px;">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:LinkButton ID="lbtnCari" runat="server" CssClass="btn">
                                <i class="fas fa-search fa-lg"></i>&nbsp;Cari
                                    </asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                </div>

            <div class="row">
                <div class="col-sm-5">
                    <div class="panel panel-default" style="width: 100%;">
                                <div class="panel-heading">
                                    SENARAI PTj
                                </div>
                                <div class="panel-body" style="overflow:scroll;height:600px">                                   
                                    <asp:GridView ID="gvPTj" runat="server" AllowSorting="True" AutoGenerateColumns="False" BorderColor="#333333" BorderStyle="Solid" CssClass="table table-striped table-bordered table-hover" EmptyDataText=" " Font-Size="8pt" HeaderStyle-BackColor="#6699FF" ShowFooter="True" ShowHeaderWhenEmpty="True" Width="100%">
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                            </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbSelect" runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="1px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Bil">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBil" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />
                                                </ItemTemplate>
                                                <ItemStyle Width="5px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="PTj">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblKodPTj" runat="server" Text='<%# Eval("KodPTJ") %>' />
                                                    &nbsp;-&nbsp;
                                                    <asp:Label ID="lblPTj" runat="server" Text='<%# Eval("ButPTj") %>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left"/>
                                            </asp:TemplateField>

                                        </Columns>
                                        <HeaderStyle BackColor="#6699FF" />
                                    </asp:GridView>
                                </div>

                            </div>
                </div>
                <div class="col-sm-2" style="text-align:center;">
                    <div style="margin-bottom: 20px;"> <asp:LinkButton ID="lbtnTambah" runat="server" CssClass="btn">
					Tambah&nbsp;&nbsp;&nbsp;<i class="fas fa-angle-double-right"></i>
                        </asp:LinkButton>
                    </div>               
                </div>
                <div class="col-sm-5">
                    <div class="panel panel-default" style="width: 100%;">
                                <div class="panel-heading">
                                    PTj YANG DIPILIH
                                </div>
                                <div class="panel-body" style="overflow:scroll;height:600px">                                   
                                    <asp:GridView ID="gvPTjSel" runat="server" AllowSorting="True" AutoGenerateColumns="False" BorderColor="#333333" BorderStyle="Solid" CssClass="table table-striped table-bordered table-hover" EmptyDataText=" " Font-Size="8pt" HeaderStyle-BackColor="#6699FF" ShowFooter="True" ShowHeaderWhenEmpty="True" Width="100%">
                                        <Columns>
                                            
                                            <asp:TemplateField HeaderText="Bil">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBil" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />
                                                </ItemTemplate>
                                                <ItemStyle Width="5px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="PTj">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblKodPTj" runat="server" Text='<%# Eval("KodPTJ") %>' />
                                                    &nbsp;-&nbsp;
                                                    <asp:Label ID="lblPTj" runat="server" Text='<%# Eval("ButPTj") %>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left"/>
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnDelete" runat="server" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CommandName="Delete" CssClass="btn-xs" ToolTip="Hapus dari senarai">
                                           <span style="color:red"> <i class="fas fa-times"></i> </span>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="left" Width="50px" />
                                                    </asp:TemplateField>

                                        </Columns>
                                        <HeaderStyle BackColor="#6699FF" />
                                    </asp:GridView>
                                </div>

                            </div>
                </div>
            </div>

            <div class="row">
                <div style="text-align: center; margin-top: 50px;">
                    <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn" OnClientClick="return confirm('Anda pasti untuk simpan pendaftaran PTj untuk staf ini?');">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                    </asp:LinkButton>
                </div>
            </div>

         </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
