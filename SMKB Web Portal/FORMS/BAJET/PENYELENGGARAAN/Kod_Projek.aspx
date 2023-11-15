<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Kod_Projek.aspx.vb" Inherits="SMKB_Web_Portal.Kod_Projek" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>         
            <div class="row">
                <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" Width="100%" Height="800px" CssClass="tabCtrl" ActiveTabIndex="1" AutoPostBack="true">
                    <ajaxToolkit:TabPanel ID="tabPrefix" runat="Server" HeaderText="Daftar Prefix">
                        <ContentTemplate>
                            <div class="row1">
                                <div class="panel panel-default" style="width: 1000px; margin-top: 30px; margin-left: 10px;">
                                    <div class="panel-body">
                                        <table class="nav-justified">

                                            <tr style="height: 35px">
                                                <td style="height: 22px;width :100px;">
                                                    Kategori</td>
                                                <td style="height: 22px;">: </td>
                                                <td>

                                                    <asp:DropDownList ID="ddlKatPrefix" runat="server" AutoPostBack="True" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>

                                            <tr style="height: 35px">
                                                <td style="height: 22px;width :100px;">Prefix</td>
                                                <td style="height: 22px;">:</td>
                                                <td>
                                                    <asp:TextBox ID="txtPrefix" runat="server" class="form-control" MaxLength="2" onkeypress="return fCheckChar(event,this);" Width="20%"></asp:TextBox>
                                                    <i aria-hidden="true" class="fas fa-info-circle fa-lg" data-html="true" data-placement="right" data-toggle="tooltip" title="&nbsp;Masukkan 2 huruf sahaja."></i>&nbsp;
                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPrefix" CssClass="text-danger" Display="Dynamic" ErrorMessage="Sila masukkan Prefix!" ValidationGroup="grpSimpanPfx"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtPrefix" CssClass="text-danger" Display="Dynamic" ErrorMessage="Masukkan 2 karakter tanpa numerik sahaja." ValidationExpression="^[a-zA-Z00''-'\s]{2,}$" ValidationGroup="grpSimpanPfx"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>

                                            <tr style="height: 25px">
                                                <td style="height: 22px; vertical-align: top;">
                                                    Butiran</td>
                                                <td style="height: 22px; vertical-align: top;">:</td>
                                                <td style="height: 22px">
                                                    <asp:TextBox ID="txtButiranPfx" runat="server" class="form-control" Width="500px"></asp:TextBox>
                                                    &nbsp;
                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtButiranPfx" CssClass="text-danger" ErrorMessage="Sila masukkan Butiran!" ValidationGroup ="grpSimpanPfx"/>
                                                </td>
                                            </tr>
                                            <tr style="height: 25px">
                                                <td style="height: 22px; vertical-align: top;">Status</td>
                                                <td style="height: 22px; vertical-align: top;">:</td>
                                                <td style="height: 22px">
                                                    <asp:RadioButtonList ID="rbStatusPfx" runat="server" Height="25px" RepeatDirection="Horizontal" Width="273px">
                                                        <asp:ListItem Selected="True" Text=" Aktif" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text=" Tidak Aktif" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr style="height: 55px; vertical-align: bottom">
                                                <td>&nbsp; </td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;&nbsp;&nbsp;
                          
                          <asp:LinkButton ID="lbtnSimpanPfx" runat="server" CssClass="btn btn-info" ValidationGroup ="grpSimpanPfx">
									<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                          </asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp; &nbsp;
                          <asp:LinkButton ID="lbtnHapusPfx" runat="server" CssClass="btn" Visible="False" OnClientClick="return confirm('Anda pasti untuk hapus rekod ini?');">
						<i class="fas fa-trash-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Hapus
                          </asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp; &nbsp;
                          <asp:LinkButton ID="lbtnBaruPfx" runat="server" CssClass="btn btn-info">
									<i class="fas fa-file-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Rekod baru
                          </asp:LinkButton>
                                                </td>
                                            </tr>

                                        </table>
                                    </div>
                                </div>
                            </div>

                            <div class="row" style="width: 1200px;">
                                <div class="GvTopPanel" style="height:37px;">
                                    <div style="float: left; margin-top: 5px; margin-left: 10px;">
                                        <label class="control-label" for="">Jumlah rekod :</label>&nbsp;<label class="control-label" id="lblJumRecPfx" runat="server" style="color: mediumblue;"></label>
                                        &nbsp;&nbsp;   
                                        <b style="color: #969696;">|</b> &nbsp;&nbsp;
                    
                                        <label class="control-label" for="Klasifikasi">Carian :</label>
                                        &nbsp;&nbsp;
                                        <asp:DropDownList ID="ddlCariPfx" runat="server" AutoPostBack="True" CssClass="form-control">
                                            <asp:ListItem Text="- KESELURUHAN -" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Prefix" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtCariPfx" runat="server" Width="250px" Visible="false"></asp:TextBox>
                                        &nbsp; 
                                        <asp:LinkButton ID="lBtnCariPfx" runat="server" CssClass="btn-xs">
                                            <i class="fas fa-search fa-lg"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <asp:GridView ID="gvPfx" runat="server" 
                                    AllowPaging="True" AllowSorting="True" 
                                    AutoGenerateColumns="False" BorderColor="#333333"
                                    BorderStyle="Solid" CssClass="table table-striped table-bordered table-hover" 
                                    Font-Size="8pt" PageSize="15" ShowFooter="True" ShowHeaderWhenEmpty="True" 
                                    Width="100%" EmptyDataText="Tiada rekod">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Bil">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBil" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="KodPrefix" HeaderText="Prefix" ReadOnly="True">
                                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Butiran" HeaderText="Butiran" ReadOnly="True">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left"/>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="KodKat" HeaderText="Kod Kategori">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="50px"/>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="status" HeaderText="Status">
                                            <ItemStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="btn-xs" ToolTip="Kemaskini">
                                            <i class="fas fa-edit"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="50px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="#6699FF" />
                                </asp:GridView>
                            </div>


                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>

                    <ajaxToolkit:TabPanel ID="tabKodProjek" runat="Server" HeaderText="Daftar Kod Projek">
                        <HeaderTemplate>
                            Daftar Kod Projek
                        </HeaderTemplate>
                        <ContentTemplate>
                            
                            <div class="row1">
                                <div class="panel panel-default" style="width: 1000px; margin-top: 30px; margin-left: 10px;">
                                    <div class="panel-body">
                                        <table class="nav-justified">

                                            <tr style="height: 35px">
                                                <td style="height: 22px; width: 100px;">Kategori</td>
                                                <td>:</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlKatPrefix2" runat="server" AutoPostBack="True" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>

                                            <tr style="height: 35px">
                                                <td style="height: 22px; width: 100px;">
                                                    <label class="control-label" for="Klasifikasi">
                                                        Kod Projek</label></td>
                                                <td>:</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlPrefix" runat="server" AutoPostBack="True" CssClass="form-control">
                                                    </asp:DropDownList>
                                                    &nbsp;
                                                    <asp:TextBox ID="txtKodKP" runat="server" CssClass="form-control" BackColor="#FFFFCC" ReadOnly="True" Style="width: 250px;" Visible="False"></asp:TextBox>
                                                    &nbsp;<asp:RequiredFieldValidator Display="Dynamic" runat="server" ControlToValidate="ddlPrefix" CssClass="text-danger" ErrorMessage="Sila pilih Kod Prefix!" ValidationGroup="grpSimpan" InitialValue="0" />
                                                    <br />
                                                    <asp:HiddenField ID="hidIdKP" runat="server" />
                                                </td>
                                            </tr>
                                            <tr style="height: 25px">
                                                <td style="height: 22px; vertical-align: top;">
                                                    <label class="control-label" for="Jenis">
                                                        Butiran</label></td>
                                                <td style="height: 22px; vertical-align: top;">:</td>
                                                <td style="height: 22px">
                                                    <asp:TextBox ID="txtButiran" runat="server" CssClass="form-control" Height="50px" TextMode="MultiLine" Width="90%"></asp:TextBox>
                                                    &nbsp;<asp:RequiredFieldValidator runat="server" ControlToValidate="txtButiran" CssClass="text-danger" Display="Dynamic" ErrorMessage="Sila masukkan Butiran!" ValidationGroup="grpSimpan"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr style="height: 25px">
                                                <td style="height: 22px; vertical-align: top;">Status</td>
                                                <td style="height: 22px; vertical-align: top;">:</td>
                                                <td style="height: 22px">
                                                    <asp:RadioButtonList ID="rbStatus" runat="server" Height="25px" RepeatDirection="Horizontal" Width="273px">
                                                        <asp:ListItem Selected="True" Text=" Aktif" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text=" Tidak Aktif" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr style="height: 25px">
                                                <td style="height: 22px; vertical-align: top;">&nbsp;</td>
                                                <td style="height: 22px; vertical-align: top;">&nbsp;</td>
                                                <td style="height: 22px">&nbsp;</td>
                                            </tr>
                                            <tr style="height: 25px;">
                                                <td style="height: 22px; vertical-align: top;">&nbsp;</td>
                                                <td style="height: 22px; vertical-align: top;">&nbsp;</td>
                                                <td style="height: 22px;">
                                                    <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn btn-info" ValidationGroup="grpSimpan">
									                <i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                                                    </asp:LinkButton>
                                                    &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lbtnHapus" runat="server" CssClass="btn" OnClientClick="return confirm('Anda pasti untuk hapus rekod ini?');" Visible="False">
						<i class="fas fa-trash-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Hapus
                                                    </asp:LinkButton>
                                                    &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lbtnBaru" runat="server" CssClass="btn btn-info">
									<i class="fas fa-file-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Rekod baru
                                                    </asp:LinkButton>
                                                </td>

                                            </tr>

                                        </table>
                                    </div>
                                </div>
                            </div>

                            <div class="row" style="width: 1200px;">
                                <div class="GvTopPanel" style="height:37px;">
                                    <div style="float: left; margin-top: 5px; margin-left: 10px;">
                                        <label class="control-label" for="">Jumlah rekod :</label>&nbsp;<label class="control-label" id="lblJumRekod" runat="server" style="color: mediumblue;"></label>
                                        &nbsp;&nbsp;   
                                        <b style="color: #969696;">|</b> &nbsp;&nbsp;
                    
                                        <label class="control-label" for="Klasifikasi">Carian :</label>
                                        &nbsp;&nbsp;
                                        <asp:DropDownList ID="ddlCarian" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlCarian_SelectedIndexChanged">
                                            <asp:ListItem Text="- KESELURUHAN -" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Kod Projek" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtCarianKP" runat="server" Width="250px" Visible="False"></asp:TextBox>
                                        &nbsp; 
                                        <asp:LinkButton ID="lbtnCariKP" runat="server" CssClass="btn-xs">
                                            <i class="fas fa-search fa-lg"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <asp:GridView ID="gvProjek" runat="server" 
                                    AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BorderColor="#333333"
                                    BorderStyle="Solid" CssClass="table table-striped table-bordered table-hover" Font-Size="8pt"
                                    PageSize="15" ShowFooter="True" ShowHeaderWhenEmpty="True" Width="100%" EmptyDataText="Tiada rekod."
                                    DataKeyNames="ID">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Bil">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBil" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="KodKP" HeaderText="Kod Projek" ReadOnly="True">
                                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Butiran" HeaderText="Butiran" ReadOnly="True">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Kodkat" HeaderText="Kat">
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:BoundField>                                           
                                        <asp:BoundField DataField="Status" HeaderText="Status">
                                            <ItemStyle HorizontalAlign="Center" Width="150px" />
                                        </asp:BoundField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="btn-xs" ToolTip="Kemaskini">
                                            <i class="fas fa-edit"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="50px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
                                    </Columns>
                                    <HeaderStyle BackColor="#6699FF" />
                                </asp:GridView>
                            </div>
                            
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>



                </ajaxToolkit:TabContainer>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
