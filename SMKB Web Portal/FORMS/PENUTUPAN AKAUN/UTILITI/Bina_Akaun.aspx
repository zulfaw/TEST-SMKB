<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Bina_Akaun.aspx.vb" Inherits="SMKB_Web_Portal.Bina_Akaun" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>

    <style>
        .modal
{
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
.center
{
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
.center img
{
    height: 128px;
    width: 128px;
}

.borderNone {
     border: none;
}

.btn2{
    display: inline-block;
    width: 100px;
    /* height: 30px; */
    /* display: block; */
    margin: 150px auto;
}

    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">        
        <ContentTemplate>
            <div class="row">
                <div>
                <table style="width: 100%;">
                    <tr style="height: 40px">
                        <td style="width: 75px;">KW</td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlKW1" runat="server" CssClass="form-control" AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlKW1" Display="Dynamic" ErrorMessage="" ForeColor="#820303" InitialValue="0" Text="*Sila pilih" ValidationGroup="btnProses"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr style="height: 40px">
                        <td>KO</td>
                        <td>:</td>
                        <td>

                            <asp:DropDownList ID="ddlKO1" runat="server" CssClass="form-control" AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlKO1" Display="Dynamic" ErrorMessage="" ForeColor="#820303" InitialValue="0" Text="*Sila pilih" ValidationGroup="btnProses"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr style="height: 40px;">
                        <td>PTj</td>
                        <td>:</td>
                        <td>


                            <asp:DropDownList ID="ddlPTj1" runat="server" CssClass="form-control" AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlPTj1" Display="Dynamic" ErrorMessage="" ForeColor="#820303" InitialValue="0" Text="*Sila pilih" ValidationGroup="btnProses"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr style="height: 40px;">
                        <td>KP</td>
                        <td>:</td>
                        <td>


                            <asp:DropDownList ID="ddlKP1" runat="server" CssClass="form-control" AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlKP1" Display="Dynamic" ErrorMessage="" ForeColor="#820303" InitialValue="0" Text="*Sila pilih" ValidationGroup="btnProses"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr style="height: 40px;">
                        <td>Vot Am</td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlVotAm" runat="server" AutoPostBack="True" CssClass="form-control">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlVotAm" Display="Dynamic" ErrorMessage="" ForeColor="#820303" InitialValue="0" Text="*Sila pilih" ValidationGroup="btnProses"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr style="height: 40px;">
                        <td>Vot Sebagai</td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlVotSbg" runat="server" AutoPostBack="True" CssClass="form-control">
                                <asp:ListItem Text="- SILA PILIH VOT AM -" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlVotSbg" Display="Dynamic" ErrorMessage="" ForeColor="#820303" InitialValue="0" Text="*Sila pilih" ValidationGroup="btnProses"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr style="height: 40px">
                        <td style="vertical-align: top" colspan="3">
                            <div class="row" style="margin-left: 0; width: 1300px;">
                                <div class="col-sm-5">
                                    <div class="panel panel-default" style="margin-left: 0; margin-top: 15px; width: 550px;">
                                        <div class="panel-heading">
                                            Senarai Vot Lanjut
                                        </div>
                                        <div class="panel-body" style="overflow-y: scroll;height:600px;">
                                            <asp:GridView ID="gvObjSbg1" runat="server" AllowSorting="True" AutoGenerateColumns="False" BorderColor="#333333" BorderStyle="Solid" CssClass="table table-striped table-bordered table-hover" EmptyDataText="Tiada rekod" Font-Size="8pt" HeaderStyle-BackColor="#6699FF" Height="100%" ShowFooter="True" ShowHeaderWhenEmpty="True" Width="500px">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="cbSelectAll" runat="server" AutoPostBack="true" onclick="checkAll(this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="cbSelect" runat="server" AutoPostBack="true" CssClass="borderNone" onclick="Check_Click(this)" />
                                                            <asp:LinkButton ID="btnChecked" runat="server" CssClass="borderNone" Visible="false">
                                    <a href="#" data-toggle="tooltip" title="Telah wujud dalam carta akaun" style="cursor:default;border:none;">
                                    <span style="color:#4CAF50;"><i class="fas fa-check-circle fa-lg"></i></span></a>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="1px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Objek Sebagai">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblKodVot" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("KodVot"))) %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="center" Width="70px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Butiran">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblButiran" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("Butiran"))) %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle BackColor="#6699FF" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-2" style="text-align: center;">
                                    <asp:LinkButton ID="lBtnAdd" runat="server" CssClass="btn btn2" ToolTip="Tambah ke senarai" ValidationGroup="btnProses">
				            <i class="fas fa-angle-double-right fa-lg"></i>
                                    </asp:LinkButton>
                                    <br />
                                    <br />
                                    <br />
                                    <%--<asp:LinkButton ID="lBtnRemove" runat="server" CssClass="btn " ToolTip="Hapus dari senarai" Width="70px" ValidationGroup="btnProses">
				            <i class="fas fa-angle-double-left"></i>
			            </asp:LinkButton>--%>
                                </div>
                                <div class="col-sm-5">
                                    <div class="panel panel-default" style="margin-left: 0; margin-top: 15px; width: 550px;">
                                        <div class="panel-heading">
                                            Senarai Vot Lanjut yang dipilih
                                        </div>
                                        <div class="panel-body" style="overflow-y: scroll;height:600px;">
                                            <asp:GridView ID="gvObjSbg2" runat="server" AllowSorting="True" AutoGenerateColumns="False" BorderColor="#333333" BorderStyle="Solid" CssClass="table table-striped table-bordered table-hover" EmptyDataText="Tiada rekod" Font-Size="8pt" HeaderStyle-BackColor="#6699FF" Height="100%" ShowFooter="True" ShowHeaderWhenEmpty="True" Width="500px">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Objek Sebagai">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblKodVot" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("KodVot"))) %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="center" Width="70px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Butiran">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblButiran" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("Butiran"))) %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="left" />
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
                        </td>
                        <td style="height: 22px"></td>
                    </tr>
                    <tr style="height: 10px">
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td></td>

                    </tr>
                    <tr style="height: 55px; vertical-align: bottom; text-align: center">
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td colspan="2">&nbsp; &nbsp;
                            &nbsp;&nbsp; &nbsp;&nbsp;
                          
                        </td>
                    </tr>


                </table>
                    </div>
                
            </div>

            <div class="row">
                <div style="text-align: center; margin-bottom: 10px;">
                    <asp:Button ID="btnProses" runat="server" CssClass="btn" Text="Proses" ValidationGroup="btnProses" />
                </div>
            </div>

        </ContentTemplate></asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div style="background-color: #D2D2D2; filter: alpha(opacity=80); opacity: 0.80; width: 100%; top: 0px; left: 0px; position: fixed; height: 100%;">
            </div>
            <div style="margin: auto; font-family: Trebuchet MS; filter: alpha(opacity=100); opacity: 1; font-size: small; vertical-align: middle; color: #FFFFFF; position: fixed; top: 50%; left: 50%; margin-top: -50px; margin-left: -100px;">
                <table>
                    <tr>
                        <td>
                            <img src="../../../Images/loader.gif" alt="" />
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

</asp:Content>
