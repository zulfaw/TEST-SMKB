<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Pelarasan_Nota_Debit_Nota_Kredit_(Kewangan).aspx.vb" Inherits="SMKB_Web_Portal.Pelarasan_Nota_Debit_Nota_Kredit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>

    <style>      
        .btn {
            margin-left: 20px;
        }
    </style>
    <asp:UpdatePanel  runat="server">
        <ContentTemplate>
            <div id="divList" runat="server" class="divList">

                <div class="row" style="width: 700px;">
                    <div class="well">
                        <table style="width: 100%;">
                            <tr>
                                <td>Carian</td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList ID="ddlFindInv" runat="server" AutoPostBack="true" CssClass="form-control" Style="width: 35%; height: 21px;">
                                        <asp:ListItem Text="- KESELURUHAN -" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="No. Invois" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;
                          <asp:TextBox ID="txtCarian" runat="server" Width="250px" Enabled ="false" ></asp:TextBox>
                                    &nbsp; 
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>&nbsp;</td>
                                <td>
                                    <div style="margin-top: 20px;">
                                        <asp:LinkButton ID="lbtnCari" runat="server" CssClass="btn ">
						<i class="fas fa-search"></i>&nbsp;&nbsp;&nbsp;Cari
                                        </asp:LinkButton>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>

                <div class="panel panel-default" style="width: 80%; margin-left: 35px;">
                    <div class="panel-heading">Senarai Pelarasan Invois</div>
                    <div class="panel-body">
                        <div class="GvTopPanel" style="height: 33px;">
                            <div style="float: left; margin-top: 5px; margin-left: 10px;">
                                <label class="control-label" for="">Jumlah rekod :</label>&nbsp;<label class="control-label" id="lblJumRekod" runat="server" style="color: mediumblue;"></label>

                                &nbsp;&nbsp;<b style="color: #969696;">|</b> &nbsp;&nbsp;

                    <label class="control-label" for="Klasifikasi">Saiz Rekod :</label>
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
                        <asp:GridView ID="gvLst" runat="server" AllowPaging="true" AllowSorting="True" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText=" "
                            PageSize="25" CssClass="table table-striped table-bordered table-hover" Width="100%" HeaderStyle-BackColor="#6699FF" Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" ShowFooter="True">
                            <Columns>
                                <asp:TemplateField HeaderText="Bil">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBil" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tarikh Bil">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTkhBil" runat="server" Text='<%# (Eval("AR06_Tarikh")) %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No. Invois">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoInvCuk" runat="server" Text='<%# (Eval("AR01_NoBil")) %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tujuan">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTujuan" runat="server" Text='<%# (Eval("AR01_Tujuan")) %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="30%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nama Penerima">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNPenerima" runat="server" Text='<%# (Eval("AR01_NamaPenerima")) %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="20%" />
                                </asp:TemplateField>
                                <asp:TemplateField Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFlagAdj" runat="server" Text='<%# (Eval("AR01_FlagAdj")) %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="8%" />
                                </asp:TemplateField>
                                <asp:TemplateField Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatDok" runat="server" Text='<%# (Eval("ar01_statusdok")) %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="8%" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Jumlah (RM)">
                                    <ItemTemplate>                                   
                                        <asp:Label ID="lblJum" runat="server" ForeColor="#003399" Text='<%# Eval("Jumlah", "{0:###,###,###.00}")%>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# (Eval("statusbil")) %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="15%" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnSelect" runat="server" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CommandName="Select" CssClass="btn-xs" ToolTip="Pilih">
                                                        <i class="fas fa-edit"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle BackColor="#6699FF" />
                        </asp:GridView>

                    </div>
                </div>
            </div>

            <div id="divDt" runat="server" style="width: 100%;">

                <div class="row">
                    <asp:LinkButton ID="lbtnKembali" runat="server" CssClass="btn " Width="100px" ToolTip=""> <i class="fas fa-long-arrow-alt-left fa-lg"></i>&nbsp;&nbsp; Kembali</asp:LinkButton>
                </div>

                <div class="row">
                    <div class="panel panel-default" style="width: 100%;">
                        <div class="panel-heading">Maklumat Invois</div>
                        <div class="panel-body">                            
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>Status</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>No. Invois</td>
                                            <td>:</td>
                                            <td>
                                                <asp:TextBox ID="txtNoInv" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 150px;"></asp:TextBox>
                                                <td></td>
                                                <td>No. Pelarasan Sementara</td>
                                                <td>:</td>
                                                <td>
                                                    <asp:TextBox ID="txtNoPelSem" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 150px;"></asp:TextBox>
                                                </td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Tarikh Bil</td>
                                            <td>:</td>
                                            <td>
                                                <asp:TextBox ID="txtTkhBil" runat="server" CssClass="form-control" BackColor="#FFFFCC" ReadOnly="True" Style="width: 150px;"></asp:TextBox>

                                            </td>
                                            <td>&nbsp;</td>
                                            <td>Tarikh Pelarasan</td>
                                            <td>:</td>
                                            <td>
                                                <asp:TextBox ID="txtTkhPlrasan" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Width="150px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Bank</td>
                                            <td>:</td>
                                            <td>
                                                <asp:TextBox ID="txtBank" runat="server" CssClass="form-control" Width="88%" BackColor="#FFFFCC" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>No. Rujukan</td>
                                            <td>:</td>
                                            <td>
                                                <asp:TextBox ID="txtNoRujukan" runat="server" CssClass="form-control" Width="88%" BackColor="#FFFFCC" ReadOnly="True"></asp:TextBox>
                                                <label id="lblMsgNoRuj" class="control-label" for="" style="display: none; color: #820303;">
                                                    (Masukkan No. Rujukan)
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>Alamat </td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <asp:TextBox ID="txtAlamat1" runat="server" CssClass="form-control" Width="88%" BackColor="#FFFFCC" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Kategori</td>
                                            <td>:</td>
                                            <td>
                                                <asp:TextBox ID="txtKat" runat="server" CssClass="form-control" Width="88%" BackColor="#FFFFCC" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>:</td>
                                            <td>
                                                <asp:TextBox ID="txtAlamat2" runat="server" CssClass="form-control" Width="88%" BackColor="#FFFFCC" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Nama Penerima</td>
                                            <td>:</td>
                                            <td>
                                                <asp:TextBox ID="txtNPenerima" runat="server" CssClass="form-control" Width="88%" BackColor="#FFFFCC" ReadOnly="True"></asp:TextBox>


                                            </td>
                                            <td>&nbsp;</td>
                                            <td>Negeri</td>
                                            <td>:</td>
                                            <td>
                                                <asp:TextBox ID="txtNegeri" runat="server" CssClass="form-control" Width="88%" BackColor="#FFFFCC" ReadOnly="True"></asp:TextBox>

                                            </td>

                                        </tr>
                                        <tr>
                                            <td>ID / No.KP Penerima</td>
                                            <td>:</td>
                                            <td>
                                                <asp:TextBox ID="txtIDPenerima" runat="server" CssClass="form-control" Width="88%" BackColor="#FFFFCC" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>Negara</td>
                                            <td>:</td>
                                            <td>
                                                <asp:TextBox ID="txtNegara" runat="server" CssClass="form-control" Width="88%" BackColor="#FFFFCC" ReadOnly="True"></asp:TextBox>

                                                

                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align:top;">Tujuan</td>
                                            <td style="vertical-align:top;">:</td>
                                            <td>
                                                <asp:TextBox ID="txtTujuan" runat="server" CssClass="form-control" Rows="3" TextMode="multiline" Width="88%" BackColor="#FFFFCC" ReadOnly="True"></asp:TextBox>

                                            </td>
                                            <td>&nbsp;</td>
                                            <td>Bandar</td>
                                            <td>:</td>
                                            <td>
                                                <asp:TextBox ID="txtBandar" runat="server" CssClass="form-control" Width="160px" BackColor="#FFFFCC" ReadOnly="True"></asp:TextBox>
                                                &nbsp;Poskod &nbsp;:
                                <asp:TextBox ID="txtPoskod" runat="server" CssClass="form-control" Style="width: 130px;" BackColor="#FFFFCC" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>

                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>No.Tel</td>
                                            <td>:</td>
                                            <td>
                                                <asp:TextBox ID="txtNoTel" runat="server" CssClass="form-control" Width="160px" BackColor="#FFFFCC" ReadOnly="True"></asp:TextBox>
                                                &nbsp;No.Fax &nbsp;:&nbsp; 
                                <asp:TextBox ID="txtNoFax" runat="server" CssClass="form-control" Style="width: 130px;" BackColor="#FFFFCC" ReadOnly="True"></asp:TextBox>
                                            </td>


                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>Emel</td>
                                            <td>:</td>
                                            <td>
                                                <asp:TextBox ID="txtEmel" runat="server" CssClass="form-control" Width="88%" BackColor="#FFFFCC" ReadOnly="True"></asp:TextBox>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <asp:HiddenField ID="hfMaxBilPel" runat="server" />
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>Untuk Perhatian</td>
                                            <td>:</td>
                                            <td>
                                                <asp:TextBox ID="txtPerhatian" runat="server" CssClass="form-control" Width="88%" BackColor="#FFFFCC" ReadOnly="True"></asp:TextBox>
                                            </td>

                                        </tr>


                                    </table>  
                        </div>
                    </div>
                </div>

                <div class="row" style="width: 90%;">
                    <div class="panel" style="width: 100%;"> 
                            <ajaxToolkit:TabContainer ID="TabContainer" runat="server" Width="100%" CssClass="tabCtrl" ActiveTabIndex="0" AutoPostBack="true">
                                <ajaxToolkit:TabPanel ID="tabTrans" runat="Server" HeaderText="Transaksi">

                                    <ContentTemplate>                                           
                                            <asp:GridView ID="gvTransPel" runat="server" AllowSorting="True" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText=" "
                                                CssClass="table table-striped table-bordered table-hover" Width="100%" Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" ShowFooter="True">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Bil">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="2%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="KW">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hidKW" runat="server" Value='<%#Eval("KodKw")%>' />
                                                            <asp:DropDownList ID="ddlKW" runat="server" AutoPostBack="true" CssClass="form-control" Width="150px" OnSelectedIndexChanged="ddlKW_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="KO">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hidKO" runat="server" Value='<%#Eval("KodKO")%>' />
                                                            <asp:DropDownList ID="ddlKO" runat="server" AutoPostBack="true" CssClass="form-control" Width="100px" OnSelectedIndexChanged="ddlKO_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="PTJ">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hidPTj" runat="server" Value='<%#Eval("kodPTJ")%>' />
                                                            <asp:DropDownList ID="ddlPTj" runat="server" AutoPostBack="true" CssClass="form-control" Width="150px" OnSelectedIndexChanged="ddlPTj_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="KP">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hidKP" runat="server" Value='<%#Eval("kodKP")%>' />
                                                            <asp:DropDownList ID="ddlKP" runat="server" AutoPostBack="true" CssClass="form-control" Width="150px" OnSelectedIndexChanged="ddlKP_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Vot">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hidVot" runat="server" Value='<%#Eval("kodVot")%>' />
                                                            <asp:DropDownList ID="ddlVot" runat="server" AutoPostBack="true" CssClass="form-control" Width="150px">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Perkara">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtPerkara" runat="server" CssClass="form-control" Text='<%#Eval("Perkara")%>' Style="width: 100%;"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="2000px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Petunjuk">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hidPetunjuk" runat="server" Value='<%#Eval("Petunjuk")%>' />
                                                            <asp:DropDownList ID="ddlPetunjuk" runat="server" AutoPostBack="true" CssClass="form-control" Width="50px" OnSelectedIndexChanged="ddlPetunjuk_SelectedIndexChanged">
                                                            </asp:DropDownList>

                                                            <div>
                                                                <asp:RequiredFieldValidator ID="rfvKP" runat="server" CssClass="text-danger" ControlToValidate="ddlPetunjuk" InitialValue="" ErrorMessage="Sila pilih Petunjuk" ValidationGroup="grpSimpan" Display="Dynamic" />
                                                            </div>

                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="10px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Kuantiti">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtKuantiti" runat="server" CssClass="form-control rightAlign" Style="width: 100%;" onkeypress="return isNumberKey(event,this)" Text='<%#Eval("Kuantiti", "{0:n0}")%>' OnTextChanged="txtKuantiti_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Harga Seunit (RM)">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtHarga" runat="server" CssClass="form-control rightAlign" Style="width: 100%;" onkeypress="return isNumberKey(event,this)" Text='<%#Eval("KadarHarga", "{0:N2}")%>' OnTextChanged="txtHarga_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="text-align: right; font-weight: bold;">
                                                                <asp:Label runat="server" Text="Jumlah Besar (RM)" />
                                                            </div>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" Width="150px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Jumlah (RM)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblJum" runat="server" Text='<%#Eval("Jumlah", "{0:N2}")%>' />
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="text-align: right;">
                                                                <asp:Label ID="lblTotJum" runat="server" ClientIDMode="Static" CssClass="cssTotJum" ForeColor="#003399" Font-Bold="true" />
                                                            </div>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnDelete" runat="server" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CommandName="Delete" CssClass="btn-xs" ToolTip="Hapus rekod">
                            <i class="fas fa-trash-alt fa-lg"></i>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:LinkButton ID="lbtnTambah" runat="server" CausesValidation="true" CssClass="btn" OnClick="lbtnTambah_Click" ToolTip="Tambah rekod" Width="50px">
                        <i class="fas fa-plus fa-lg"></i>
                                                            </asp:LinkButton>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="50px" />
                                                    </asp:TemplateField>
                                                </Columns>

                                                <HeaderStyle BackColor="#6699FF"></HeaderStyle>
                                            </asp:GridView>
                                     

                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>

                                <ajaxToolkit:TabPanel ID="tabLampAsal" runat="Server" HeaderText="Transaksi Asal">
                                    <ContentTemplate>
                                        <div style="width: 95%;">                                    
                                            <asp:GridView ID="gvTransAsal" runat="server" AllowSorting="True" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText=" "
                                                CssClass="table table-striped table-bordered table-hover" Width="100%" Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" ShowFooter="True">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Bil">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="2%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="KW">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblKW" runat="server" Text='<%#Eval("kodKw")%>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="KO">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblKO" runat="server" Text='<%#Eval("kodKo")%>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="PTJ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPTj" runat="server" Text='<%#Eval("KodPTJ")%>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="KP">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblKp" runat="server" Text='<%#Eval("KodKP")%>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Vot">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVot" runat="server" Text='<%#Eval("KodVot")%>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Perkara">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPerkara" runat="server" Text='<%#Eval("Perkara")%>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="200px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Kuantiti">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblKuantiti" runat="server" Text='<%#Eval("Kuantiti", "{0:N0}")%>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Harga Seunit (RM)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHarga" runat="server" Text='<%#Eval("KadarHarga", "{0:N2}")%>' />
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="text-align: right; font-weight: bold;">
                                                                <asp:Label runat="server" Text="Jumlah Besar (RM)" />
                                                            </div>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Jumlah (RM)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblJum" runat="server" Text='<%#Eval("Jumlah", "{0:N2}")%>' />
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="text-align: right;">
                                                                <asp:Label ID="lblTotJum" runat="server" ClientIDMode="Static" CssClass="cssTotJum" ForeColor="#003399" Font-Bold="true" />
                                                            </div>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                    </asp:TemplateField>

                                                </Columns>

                                                <HeaderStyle BackColor="#6699FF"></HeaderStyle>
                                            </asp:GridView>
                                        </div>
                                        </div>                      	
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>

                            </ajaxToolkit:TabContainer>
                        </div>
                </div>

                <div class="row">
                    <div class="panel panel-default" style="width: 700px;">
                        <div class="panel-heading">
                            Maklumat Pemohon
                        </div>
                        <div class="panel-body">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 100px;">Nama Pemohon</td>
                                    <td>:</td>
                                    <td style="height: 23px">
                                        <asp:Label ID="lblNoStafPmhn" runat="server"></asp:Label>
                                        &nbsp;-&nbsp;<asp:Label ID="lblNmPmhn" runat="server"></asp:Label>
                                    </td>
                                    <td style="height: 23px"></td>
                                </tr>
                                <tr>
                                    <td>Jawatan</td>
                                    <td>:</td>
                                    <td>
                                        <asp:Label ID="lblJwtanMhn" runat="server"></asp:Label>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>

                                <tr>
                                    <td>PTj</td>
                                    <td>:</td>
                                    <td>
                                        <asp:Label ID="lblKodPTjPmhn" runat="server"></asp:Label>
                                        &nbsp;-&nbsp;<asp:Label ID="lblNmPTjPmhn" runat="server"></asp:Label>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>

                                <tr>
                                    <td style="vertical-align:top">Ulasan</td>
                                    <td style="vertical-align:top">:</td>
                                    <td style="vertical-align:top">
                                        <asp:TextBox ID="txtUlasBeza" runat="server" AutoPostBack="true" CssClass="form-control" Height="70px" TextMode="multiline" Width="90%"></asp:TextBox>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>

                            </table>
                        </div>
                    </div>
                </div>

                 <asp:ValidationSummary runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="grpSimpan"/>
                <div style="margin-bottom: 10px; margin-top: 20px; text-align: center;">
                    <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn " Width="80px" ToolTip="Simpan" ValidationGroup="grpSimpan"  OnClientClick="return confirm('Anda pasti untuk simpan permohonan pelarasan ini?');">
						<i class="far fa-save fa-lg"></i> &nbsp;&nbsp; Simpan
                    </asp:LinkButton>
                    &nbsp;&nbsp;&nbsp;

            <asp:LinkButton ID="lbtnBatal" runat="server" CssClass="btn" Width="80px" OnClientClick="return confirm('Anda pasti untuk batalkan rekod pelarasan ini?');">
						<i class="fas fa-trash-alt"></i> &nbsp;&nbsp; Batal
            </asp:LinkButton>
                </div>
            </div>

            <asp:HiddenField ID="hidBilPel" runat="server" />
            <asp:HiddenField ID="hidJumAsal" runat="server" />
            <asp:HiddenField ID="hidJumBaki" runat="server" />
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

