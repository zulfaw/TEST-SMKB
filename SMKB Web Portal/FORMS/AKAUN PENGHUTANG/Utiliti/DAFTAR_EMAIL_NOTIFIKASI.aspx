<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="DAFTAR_EMAIL_NOTIFIKASI.aspx.vb" Inherits="SMKB_Web_Portal.DAFTAR_EMAIL_NOTIFIKASI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>

            <div class="row">
                    <div class="panel panel-default" style="width: 100%;">
                        <div class="panel-heading">Senarai Tugas</div>
                        <div class="panel-body">
                            <div class="GvTopPanel" style="height: 33px;">
                                <div style="float: left; margin-top: 5px; margin-left: 10px;">
                                    <label class="control-label" for="">Jumlah rekod :</label>&nbsp;<label class="control-label" id="lblJumRekod" runat="server" style="color: mediumblue;"></label>
                                    &nbsp;&nbsp;   <b style="color: #969696;">|</b> &nbsp;&nbsp;

                    <label class="control-label" for="Klasifikasi">Saiz Rekod :</label>
                                    <asp:DropDownList ID="ddlSaizRekod" runat="server" Width="50px" AutoPostBack="True" class="form-control">
                                        <asp:ListItem Text="10" Value="10" />
                                        <asp:ListItem Text="25" Value="25" Selected="True" />
                                        <asp:ListItem Text="50" Value="50" />
                                        <asp:ListItem Text="100" Value="100" />
                                    </asp:DropDownList>                                   
                                     </div>
                            </div>
                            <asp:GridView ID="gvLst" runat="server" AllowPaging="True" AllowSorting="True"
                                ShowHeaderWhenEmpty="True"
                                AutoGenerateColumns="False"
                                EmptyDataText="Tiada rekod"
                                PageSize="25" CssClass="table table-striped table-bordered table-hover"
                                Width="100%" Height="100%" HeaderStyle-BackColor="#6699FF"
                                Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" ShowFooter="True">
                                <Columns>
                                    <asp:TemplateField HeaderText="Bil">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBil" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Modul">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKodModul" Visible ="false" runat="server" Text='<%# Eval("KodModul")%>' />
                                            <asp:Label ID="lblModul" runat="server" Text='<%# Eval("ButModul")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Sub Modul">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKodSubModul" Visible ="false" runat="server" Text='<%# Eval("KodSubModul")%>' />
                                            <asp:Label ID="lblSubModul" runat="server" Text='<%# Eval("ButSubModul")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Sub Menu">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKodSubMenu" Visible ="false" runat="server" Text='<%# Eval("KodSubMenu")%>' />
                                            <asp:Label ID="lblSubMenu" runat="server" Text='<%# Eval("ButSubMenu")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                                    </asp:TemplateField>
                                  
                                    <asp:TemplateField HeaderText="No. Staf">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoStaf" runat="server" Text='<%# Eval("NoStaf")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Email">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("Email")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText ="Tugas">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKodTugas" Visible ="false" runat="server" Text='<%# Eval("KodTugas")%>' />
                                            <asp:Label ID="lblTugas" runat="server" Text='<%# Eval("butTugas")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# IIf(Boolean.Parse(Eval("Status").ToString()), "Aktif", "Tidak Aktif")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center"  Width="150px"/>
                                    </asp:TemplateField>
                               
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnSelect" runat="server" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CommandName="Select" CssClass="btn-xs" ToolTip="Pilih">
                                                        <i class="fas fa-edit"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Width="3%" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle BackColor="#6699FF"></HeaderStyle>
                            </asp:GridView>

                        </div>
                    </div>
                    
                </div>

            <div class="row">
            <div class="panel panel-default" style="width:60%;">
                    <div class="panel-heading">
                        <h3 class="panel-title">Daftar Tugas</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 130px">Modul</td>
                                    <td style="width: 10px">:</td>
                                    <td>
                                            <asp:DropDownList ID="ddlModulD" runat="server" AutoPostBack="true" CssClass="form-control" Style="width: 35%; height: 21px;" />
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="ddlModulD" CssClass="text-danger" ErrorMessage="Pilih 'Modul'" InitialValue="0" ValidationGroup="grpSimpan" />
                                    </td>

                                </tr>

                                <tr>
                                    <td style="width: 130px">Sub Modul</td>
                                    <td style="width: 10px">:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlSubModulD" runat="server" AutoPostBack="true" CssClass="form-control" Style="width: 35%; height: 21px;" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSubModulD" CssClass="text-danger" ErrorMessage="Pilih 'Sub Modul'" InitialValue="0" ValidationGroup="grpSimpan" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 130px">Sub Menu</td>
                                    <td style="width: 10px">:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlSubMenuD" runat="server" AutoPostBack="true" CssClass="form-control" Style="width: 35%; height: 21px;" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSubMenuD" CssClass="text-danger" ErrorMessage="Pilih 'Sub Menu'" InitialValue="0" ValidationGroup="grpSimpan" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 130px; height: 36px;">No. Staf</td>
                                    <td style="width: 10px; height: 36px;">:</td>
                                    <td style="height: 36px">
                                        <asp:DropDownList ID="ddlStaf" runat="server" AutoPostBack="true" CssClass="form-control" Style="width: 35%; height: 21px;" />

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlStaf" CssClass="text-danger" ErrorMessage="Pilih 'No. Staf'" InitialValue="0" ValidationGroup="grpSimpan" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 130px">Email</td>
                                    <td style="width: 10px">:</td>
                                    <td>
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Style="width: 450px;"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail" CssClass="text-danger" ErrorMessage="Masukkan email" ValidationGroup="grpSimpan" Display="Dynamic"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 130px">Tugas</td>
                                    <td style="width: 10px">:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlTugas" runat="server" AutoPostBack="true" CssClass="form-control" Style="width: 35%; height: 21px;" />

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlTugas" CssClass="text-danger" ErrorMessage="Pilih 'Tugas'" InitialValue="0" ValidationGroup="grpSimpan" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 130px">Status</td>
                                    <td style="width: 10px">:</td>
                                    <td>
                                        <asp:RadioButtonList ID="rbStatus" runat="server" Height="25px" RepeatDirection="Horizontal" Width="273px">
                                            <asp:ListItem Selected="True" Text=" Aktif" Value="True"></asp:ListItem>
                                            <asp:ListItem Text=" Tidak Aktif" Value="False"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>

                            </table>
                        </div>

                        <div style="margin-bottom: 20px; margin-top: 20px; text-align: center;">
                    <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn " Width="80px" ToolTip="Simpan" ValidationGroup="grpSimpan">
						<i class="far fa-save fa-lg"></i> &nbsp;&nbsp; Simpan
                    </asp:LinkButton>
                    &nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="lbtnNew" runat="server" CssClass="btn" Width="100px">
						    <i class="fas fa-file-alt fa-lg"></i> &nbsp;&nbsp; Rekod Baru
                        </asp:LinkButton>

                </div>
                        


                    </div>
                </div>
                </div>

            
           </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
