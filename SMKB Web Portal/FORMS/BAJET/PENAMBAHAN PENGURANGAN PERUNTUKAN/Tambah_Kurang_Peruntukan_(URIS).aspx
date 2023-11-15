<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Tambah_Kurang_Peruntukan_(URIS).aspx.vb" Inherits="SMKB_Web_Portal.Tambah_Kurang_Peruntukan__URIS_" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>TAMBAH / KURANG PERUNTUKAN (URIS)</h1>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        
            <div style="margin: 20px; text-align: left;">
                <asp:LinkButton ID="lbtnMohonbaru" runat="server" CssClass="btn" Width="140px">
						<i class="fas fa-file-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Permohonan Baru
                </asp:LinkButton>
            </div>

            <hr />

            <div class="panel panel-default">
                <div class="panel-heading">
                    Senarai Permohonan Tambah/Kurang
                </div>
                <div class="panel-body">
                    <div class="row">

                        <div style="margin-top: 20px;">
                            <div class="GvTopPanel" style="height: 33px;">
                                <div style="float: left; margin-top: 5px; margin-left: 10px;">
                                    <label class="control-label" for="">Jumlah rekod :</label>&nbsp;<label class="control-label" id="lblJumRekod" runat="server" style="color: mediumblue;"></label>
                                     &nbsp;&nbsp;<b style="color:#969696;">|</b> &nbsp;&nbsp;

                    <label class="control-label" for="Klasifikasi"> Saiz Rekod :</label>
        <asp:DropDownList ID="ddlSaizRekod" runat="server" Width="50px" AutoPostBack="True" class="form-control">
                <asp:ListItem Text="10" Value="10" />
                <asp:ListItem Text="25" Value="25" Selected="True" />
                <asp:ListItem Text="50" Value="50" />
                <asp:ListItem Text="100" Value="100" />
            </asp:DropDownList>

                    &nbsp;&nbsp;<b style="color:#969696;">|</b> &nbsp;&nbsp;
                    Status : &nbsp;
                    <asp:DropDownList ID="ddlFilStat" runat="server" AutoPostBack="True" CssClass="form-control"/>
                                </div>
                            </div>
                            <asp:GridView ID="gvTKList" runat="server" ShowHeaderWhenEmpty="True"
                                AutoGenerateColumns="False" AllowSorting="True" EmptyDataText="Tiada rekod"
                                CssClass="table table-striped table-bordered table-hover" Width="100%" HeaderStyle-BackColor="#ffffb3" Font-Size="8pt"
                                BorderColor="#333333" BorderStyle="Solid"
                                AllowPaging="True" PageSize="20">
                                <Columns>
                                    <asp:TemplateField HeaderText="Bil">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBil" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="30px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIdMohon" runat="server" Text='<%# Eval("BG15_Id")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="No. Mohon">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoMohon" runat="server" Text='<%# Eval("BG15_NoMohon")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Amaun (RM)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmaun" runat="server" Text='<%# Eval("BG15_Amaun", "{0:n2}")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Width="50px" ForeColor="#003399" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Tarikh Mohon">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTkhMohon" runat="server" Text='<%# Eval("BG15_TkhMohon", "{0:dd/MM/yyyy}")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblButiran" runat="server" Text='<%# Eval("BG15_Butiran")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="KW">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKWDpd" runat="server" Text='<%# Eval("KodKwDpd")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="KO">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKODpd" runat="server" Text='<%# Eval("kodkoDpd")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="PTJ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPTjDpd" runat="server" Text='<%# Eval("KodPTJDpd")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="KP">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKPDpd" runat="server" Text='<%# Eval("kodkpDpd")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Objek Sebagai">
                                        <ItemTemplate>
                                            <asp:Label ID="lblObjSbgDpd" runat="server" Text='<%# Eval("KodVotDpd")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="KW">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKWKpd" runat="server" Text='<%# Eval("KodKwKpd")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="KO">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKOKpd" runat="server" Text='<%# Eval("kodkoKpd")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="PTJ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPTjKpd" runat="server" Text='<%# Eval("KodPTJKpd")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="KP">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKPKpd" runat="server" Text='<%# Eval("kodkpKpd")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Objek Sebagai">
                                        <ItemTemplate>
                                            <asp:Label ID="lblObjSbgKpd" runat="server" Text='<%# Eval("KodVotKpd")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatDok" runat="server" Text='<%# Eval("BG12_StatusDok")%>' Visible="false" />
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="200px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="btn-xs" ToolTip="Pilih">
                                            <i class="fas fa-edit"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>

                                </Columns>

                                <HeaderStyle BackColor="#FFFFB3" />

                            </asp:GridView>
                        </div>


                    </div>
                </div>
            </div>


                
    </ContentTemplate>
    </asp:UpdatePanel>  
    

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

