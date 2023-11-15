<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Invois_Pelajar.aspx.vb" Inherits="SMKB_Web_Portal.Invois_Pelajar" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>

   <asp:UpdatePanel runat="server">
       <ContentTemplate>

           <div id="divList" runat="server">

               <div style="margin: 20px; text-align: left;">
                        <asp:LinkButton ID="lBtnBaru" runat="server" CssClass="btn" Width="140px">
						<i class="fas fa-file-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Permohonan Baru
                        </asp:LinkButton>
                    </div>

               <hr />

                <div class="row" style="width: 700px;">
                    <div class="well">
                        <table style="width: 100%;">
                            <tr>
                                <td>Carian</td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList ID="ddlCarian" runat="server" AutoPostBack="true" CssClass="form-control" Style="width: 35%; height: 21px;">
                                        <asp:ListItem Text="- KESELURUHAN -" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="No. Invois Sementara" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;
                          <asp:TextBox ID="txtCarian" runat="server" Width="250px" Enabled="false"></asp:TextBox>
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

                <div class="row">
                    <div class="panel panel-default" style="width: 100%;">
                        <div class="panel-heading">Senarai Invois Kewangan</div>
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

                                     &nbsp;&nbsp;<b style="color:#969696;">|</b> &nbsp;&nbsp;

                    <asp:DropDownList ID="ddlFilStat" runat="server" AutoPostBack="True" CssClass="form-control"/>

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
                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIdBil" runat="server" Text='<%# Eval("AR01_IdBil")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText ="No. Invois Sementara">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoInvSem" runat="server" Text='<%# Eval("AR01_NoBilSem")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="15%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText ="No. Invois">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoInv" runat="server" Text='<%# Eval("AR01_NoBil")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="15%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Tujuan">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTuj" runat="server" Text='<%# Eval("AR01_Tujuan")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="25%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Tarikh Mohon">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTkhMhn" runat="server" Text='<%# Eval("AR01_TkhMohon", "{0:dd/MM/yyyy}")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="12%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Jumlah Bayar (RM)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblJumlah" runat="server" ForeColor="#003399" Text='<%# Eval("AR01_Jumlah", "{0:###,###,###.00}")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Width="12%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatDok" runat="server" Visible="false" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("AR01_StatusDok"))) %>' />
                                            <asp:Label ID="lblStat" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("ButStatDok"))) %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="15%" />
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
            </div>

           <div id="divDetail" runat="server" style="width: 100%;" visible="false">

               <div class="row">
                   <asp:LinkButton ID="lnkBtnBack" runat="server" CssClass="btn " Width="100px" ToolTip="">
						<i class="fas fa-long-arrow-alt-left  fa-lg"></i> &nbsp;&nbsp; Kembali
                   </asp:LinkButton>
               </div>

               <div class="row">
                   <div class="panel panel-default" style="width: 95%;margin-left: 20px;">
                       <div class="panel-heading">Maklumat Invois</div>
                       <div class="panel-body">
                           <asp:HiddenField ID="hidIdBil" runat="server" />
                           <table style="width: 100%;">
                               <tr>
                                   <td style="width: 130px;">No. Invois Sementara </td>
                                   <td>:</td>
                                   <td style="width: 550px;">
                                       <asp:TextBox ID="txtNoInvSem" runat="server" CssClass="form-control" Style="width: 30%;" ReadOnly="True" BackColor="#FFFFCC"></asp:TextBox>

                                   </td>
                                   <td>&nbsp;</td>
                                   <td>Status</td>
                                   <td>:</td>
                                   <td colspan="4">
                                       <asp:TextBox ID="txtStatus" runat="server" CssClass="form-control" Width="350px" ReadOnly="True" BackColor="#FFFFCC"></asp:TextBox>
                                       <asp:HiddenField ID="HidKodStatus" runat="server" />
                                   </td>

                               </tr>
                               <tr>
                                   <td>Tarikh Mohon</td>
                                   <td>:</td>
                                   <td>
                                       <asp:TextBox ID="txtTkhMohon" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 30%;"></asp:TextBox>
                                   </td>
                                   <td></td>
                                   <td></td>
                                   <td></td>
                                   <td colspan="4"></td>
                               </tr>
                               <tr>
                                   <td>PTJ</td>
                                   <td>:</td>
                                   <td>
                                       <asp:TextBox ID="txtKodPTj" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 70px;"></asp:TextBox>
                                       &nbsp;-&nbsp;<asp:TextBox ID="txtPTj" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 450px;"></asp:TextBox>
                                   </td>
                                   <td>&nbsp;</td>
                                   <td>&nbsp;</td>
                                   <td>&nbsp;</td>
                                   <td colspan="4">&nbsp;</td>
                               </tr>
                               <tr>
                                   <td>&nbsp;</td>
                                   <td>&nbsp;</td>
                                   <td>&nbsp;</td>
                                   <td>&nbsp;</td>
                                   <td>&nbsp;</td>
                                   <td>&nbsp;</td>
                                   <td colspan="4">&nbsp;</td>
                               </tr>
                               <tr>
                                   <td>Bank</td>
                                   <td>:</td>
                                   <td>
                                       <asp:DropDownList ID="ddlBank" runat="server" CssClass="form-control"/>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" CssClass="text-danger" ControlToValidate="ddlBank" InitialValue="0" ErrorMessage="Sila pilih Bank" ValidationGroup="grpSimpan" Display="Dynamic"/>
                                   </td>
                                   <td>&nbsp;</td>
                                   <td>No. Rujukan</td>
                                   <td>:</td>
                                   <td colspan="4">
                                       <asp:TextBox ID="txtNoRujukan" runat="server" CssClass="form-control" Width="200px"></asp:TextBox>
                                       <label id="lblMsgNoRuj" class="control-label" for="" style="display: none; color: #820303;">
                                           (Masukkan No. Rujukan)
                                       </label>
                                   </td>
                               </tr>
                               <tr>
                                   <td>Kategori Pelajar</td>
                                   <td>:</td>
                                   <td>
                                       <asp:RadioButtonList ID="rbKatPel" runat="server" Height="35px" RepeatDirection="Horizontal" Width="350px" AutoPostBack="true">
                                           <asp:ListItem Text="  Pra Siswazah" Value="UG" />
                                           <asp:ListItem Text="  Pasca Siswazah" Value="PG" />
                                           <asp:ListItem Text="  Sepanjang Hayat" Value="SH" />
                                       </asp:RadioButtonList>
                                   
                                   </td>
                                   <td>&nbsp;</td>
                                   <td class="50">Alamat </td>
                                   <td>:</td>
                                   <td colspan="4">
                                       <asp:TextBox ID="txtAlamat1" runat="server" CssClass="form-control" Width="400px" TextMode="MultiLine" Rows="3"></asp:TextBox>

                                   </td>
                               </tr>
                               <tr>
                                   <td>Status Pelajar</td>
                                   <td>:</td>
                                   <td>
                                       <asp:DropDownList ID="ddlStatPel" runat="server" CssClass="form-control" AutoPostBack="true">
                                           <asp:ListItem Text="- SILA PILIH KATEGORI PELAJAR -" Value="0" Selected="True" />
                                       </asp:DropDownList>

                                       <asp:TextBox ID="txtStatPel" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 150px;" Visible="false"></asp:TextBox>

                                   </td>
                                   <td>&nbsp;</td>
                                   <td>&nbsp;</td>
                                   <td>&nbsp;</td>
                                   <td colspan="4">
                                       <label id="lblMsgAlamat1" class="control-label" for="" style="display: none; color: #820303;">
                                           (Masukkan Alamat 1)
                                       </label>
                                       <asp:TextBox ID="txtAlamat2" runat="server" CssClass="form-control" Width="400px" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                   </td>
                               </tr>
                               <tr>
                                   <td>Penaja</td>
                                   <td>:</td>
                                   <td>
                                       <%--<asp:TextBox ID="txtNamaPenerima" runat="server" CssClass="form-control"  style="width: 88%; "></asp:TextBox>--%>
                                       <asp:DropDownList ID="ddlPenaja" runat="server" AutoPostBack="True" CssClass="form-control" Width="500px">
                                       </asp:DropDownList>
                                       <div>
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="text-danger" ControlToValidate="ddlPenaja" InitialValue="0" ErrorMessage="Sila pilih Penghutang" ValidationGroup="grpSimpan" Display="Dynamic"/>

                                           <asp:TextBox ID="txtPenaja" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 450px;" Visible ="false"></asp:TextBox>
                                   
                                           </div>
                                   </td>
                                   <td>&nbsp;</td>
                                   <td>Negeri</td>
                                   <td>:</td>
                                   <td colspan="4">
                                        <asp:TextBox ID="txtKodNegeri" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 70px;"></asp:TextBox>
                                        &nbsp;-
                                        <asp:TextBox ID="txtNegeri" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 450px;"></asp:TextBox>
                                   </td>

                               </tr>
                               <tr>
                                   <td>Kod Penghutang</td>
                                   <td>:</td>
                                   <td>
                                       <asp:TextBox ID="txtKodPenghutang" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 350px;"></asp:TextBox>
                                   </td>
                                   <td>&nbsp;</td>
                                   <td>&nbsp;</td>
                                   <td>&nbsp;</td>
                                   <td colspan="4">
                                          &nbsp;</td>
                               </tr>
                               <tr>
                                   <td>Sesi Pengajian</td>
                                   <td>:</td>
                                   <td>
                                       <asp:DropDownList ID="ddlSesi" runat="server" AutoPostBack="true" CssClass="form-control">
                                       </asp:DropDownList>
                                       <asp:TextBox ID="txtSesi" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 150px;" Visible="false"></asp:TextBox>
                                   </td>
                                   <td>&nbsp;</td>
                                   <td>Negara</td>
                                   <td>:</td>
                                   <td colspan="4">
                                       <asp:TextBox ID="txtKodNegara" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 70px;"></asp:TextBox>
                                       &nbsp;-
                                       <asp:TextBox ID="txtNegara" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 450px;"></asp:TextBox>
                                   </td>
                               </tr>                      
                               <tr>
                                       <td>Tujuan</td>
                                       <td>:</td>
                                       <td><asp:TextBox ID="txtTujuan" runat="server" CssClass="form-control" Rows="3" TextMode="multiline" Width="88%"></asp:TextBox>
                                       <div>
                                           <asp:RequiredFieldValidator runat="server" ControlToValidate="txtTujuan" CssClass="text-danger" Display="Dynamic" ErrorMessage="Masukkan Tujuan" ValidationGroup="grpSimpan" />
                                       </div></td>
                                       <td>&nbsp;</td>
                                       <td>Bandar</td>
                                       <td>:</td>
                                       <td colspan="4">
                                           <asp:TextBox ID="txtBandar" runat="server" CssClass="form-control" Text="<%# System.DateTime.Now%>" Width="450px"></asp:TextBox>
                                       </td>
                               </tr>
                               <tr>
                                   <td style="vertical-align: top;">&nbsp;</td>
                                   <td style="vertical-align: top;">&nbsp;</td>
                                   <td>&nbsp;</td>
                                   <td>&nbsp;</td>
                                   <td>Poskod </td>
                                   <td>:</td>
                                   <td>
                                       <asp:TextBox ID="txtPoskod" runat="server" CssClass="form-control" Style="width: 160px;"></asp:TextBox>

                                   </td>
                                   <td>&nbsp;</td>
                                   <td>&nbsp;</td>
                                   <td>&nbsp;</td>
                       </tr>
                               <tr>
                                   <td>&nbsp;</td>
                                   <td>&nbsp;</td>
                                   <td></td>
                                   <td>&nbsp;</td>
                                   <td>No.Tel</td>
                                   <td>:</td>
                                   <td colspan="4">
                                       <asp:TextBox ID="txtNoTel" runat="server" CssClass="form-control" Width="160px"></asp:TextBox>
                                       &nbsp; &nbsp; No.Fax &nbsp;:&nbsp; &nbsp; 
                                    <asp:TextBox ID="txtNoFax" runat="server" CssClass="form-control" Style="width: 160px;"></asp:TextBox>
                                   </td>
                               </tr>
                               <tr>
                                   <td>&nbsp;</td>
                                   <td>&nbsp;</td>
                                   <td>&nbsp;</td>
                                   <td>&nbsp;</td>
                                   <td>Emel</td>
                                   <td>:</td>
                                   <td colspan="4">
                                       <asp:TextBox ID="txtEmel" runat="server" CssClass="form-control" Width="500px"></asp:TextBox>
                                       <label id="lblMsgEmel" class="control-label" for="" style="display: none; color: #820303;">
                                           (Masukkan Emel)
                                       </label>
                                   </td>
                               </tr>
                               <tr>
                                   <td></td>
                                   <td></td>
                                   <td></td>
                                   <td>&nbsp;</td>
                                   <td>Untuk Perhatian</td>
                                   <td>:</td>
                                   <td colspan="4">
                                       <asp:TextBox ID="txtPerhatian" runat="server" CssClass="form-control" Width="450px"></asp:TextBox>
                                       <label id="lblMsgPerhatian" class="control-label" for="" style="display: none; color: #820303;">
                                           (Masukkan Untuk Perhatian)
                                       </label>
                                   </td>
                               </tr>

                           </table>
                       </div>

                       </div>
               </div>

               <div class="row">
                   <div class="panel panel-default" style="width: 95%;">
                       <div class="panel-heading">Lampiran Pelajar</div>
                       <div class="panel-body">
                          <div runat="server" id="alert" class="alert alert-danger" visible ="false">Tiada pelajar dibawah tajaan &nbsp;<asp:Label ID="lblPenaja" runat="server" Text="" ForeColor="#000000" /> &nbsp; pada sesi <asp:Label ID="lblSesi" runat="server" Text="" ForeColor="#000000"/>!</div>

                           <div style="width: 75%; margin-top: 10px;">
                               <asp:GridView ID="gvLamp" runat="server" AllowSorting="True" 
                                   AutoGenerateColumns="False" BorderStyle="Solid" 
                                   CssClass="table table-striped table-bordered table-hover" Font-Size="8pt" 
                                   ShowFooter="True" ShowHeaderWhenEmpty="True" Width="100%"
                                   EmptyDataText=" Tiada rekod">

                                   <Columns>
                                        <asp:TemplateField>
<%--                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="cbSelectAll" runat="server" AutoPostBack="true" onclick="checkAll(this);" />
                                                        </HeaderTemplate>--%>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="cbSelect" runat="server" AutoPostBack="true" CssClass="borderNone" Checked="true" OnCheckedChanged ="cbSelect_CheckedChanged" />
                                                  
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="1px" />
                                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Bil">
                                           <ItemTemplate>
                                               <%# Container.DataItemIndex + 1 %>
                                           </ItemTemplate>
                                           <ItemStyle HorizontalAlign="Right" Width="2%" />
                                       </asp:TemplateField>

                                       <asp:TemplateField HeaderText="Nama Penerima">
                                           <ItemTemplate>
                                               <asp:Label ID="lblNmPen" runat="server" Text='<%# Eval("Nama")%>' />
                                           </ItemTemplate>
                                           <ItemStyle HorizontalAlign="Left" Width="350px" />
                                       </asp:TemplateField>

                                       <asp:TemplateField HeaderText="No. KP/Passport">
                                           <ItemTemplate>
                                               <asp:Label ID="lblNoKP" runat="server" Text='<%# Eval("NoKP")%>' />
                                           </ItemTemplate>
                                           <ItemStyle HorizontalAlign="Left" Width="100px" />
                                       </asp:TemplateField>

                                       <asp:TemplateField HeaderText="No. Matrik">
                                           <ItemTemplate>
                                               <asp:Label ID="lblNoMatrik" runat="server" Text='<%# Eval("NoMatrik")%>' />
                                           </ItemTemplate>
                                           <ItemStyle HorizontalAlign="Left" Width="100px" />
                                       </asp:TemplateField>

                                       <asp:TemplateField HeaderText="Kursus">
                                           <ItemTemplate>
                                               <asp:Label ID="lblKursus" runat="server" Text='<%# Eval("Kursus")%>' />
                                           </ItemTemplate>
                                           <ItemStyle HorizontalAlign="Left" Width="100px" />
                                       </asp:TemplateField>

                                       <asp:TemplateField HeaderText="Butiran">
                                           <FooterTemplate>
                                               <div style="text-align: right; font-weight: bold;">
                                                   <asp:Label runat="server" Text="Jumlah Besar (RM)" />
                                               </div>
                                           </FooterTemplate>
                                           <ItemTemplate>
                                               <%--<asp:TextBox ID="txtButiran" runat="server" CssClass="form-control" Style="width: 100%;" Text='<%#Eval("Butiran")%>'></asp:TextBox>--%>
                                               <asp:Label ID="lblButiran" runat="server" Text='<%# Eval("Sesi")%>' />
                                           </ItemTemplate>
                                           <ItemStyle HorizontalAlign="Left" Width="100px" />
                                       </asp:TemplateField>

                                       <asp:TemplateField HeaderText="Jumlah (RM)">
                                           <FooterTemplate>
                                               <div style="text-align: right;">
                                                   <asp:Label ID="lblTotJum" runat="server" Font-Bold="true" ForeColor="#003399" />
                                               </div>
                                           </FooterTemplate>
                                           <ItemTemplate>
                                               <%--<asp:TextBox ID="txtAmaun" runat="server" AutoPostBack="true" CssClass="form-control rightAlign" onkeypress="return isNumberKey(event,this)" OnTextChanged="txtAmaun_TextChanged" Style="width: 100%;" Text='<%#Eval("JumDC", "{0:N2}")%>'></asp:TextBox>--%>
                                               <asp:Label ID="lblJumDC" runat="server" ForeColor="#003399" Text='<%# Eval("JumDC", "{0:N2}")%>' />
                                           </ItemTemplate>
                                           <ItemStyle HorizontalAlign="Right" Width="100px" />
                                       </asp:TemplateField>

                                       <asp:TemplateField Visible="false">
                                           <ItemTemplate>
                                               <asp:Label ID="lblKP" runat="server" Text='<%# Eval("KP")%>' />
                                           </ItemTemplate>
                                       </asp:TemplateField>

                                   <%--    <asp:TemplateField HeaderText="Tindakan">
                                           <ItemTemplate>
                                               <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete" CssClass="btn-xs" OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');" ToolTip="Padam">
										                    <i class="far fa-trash-alt fa-lg"></i>
                                               </asp:LinkButton>
                                           </ItemTemplate>
                                           <FooterStyle HorizontalAlign="Center" />
                                           <ItemStyle HorizontalAlign="Center" Width="5%" />
                                       </asp:TemplateField>--%>
                                   </Columns>
                                   <EmptyDataTemplate>
                                   </EmptyDataTemplate>
                                   <SelectedRowStyle ForeColor="Blue" />
                               </asp:GridView>
                           </div>

                       </div>
                   </div>
               </div>
               
               <div class="row">
                   <div class="panel panel-default" style="width: 95%;">
                       <div class="panel-heading">Transaksi</div>
                       <div class="panel-body">

                           <div style="width: 75%; margin-top: 10px;">
                               <asp:GridView ID="gvTransDt" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                   BorderStyle="Solid" CssClass="table table-striped table-bordered table-hover"
                                   EmptyDataText=" Tiada rekod" Font-Size="8pt" ShowFooter="True" ShowHeaderWhenEmpty="True"
                                   Width="100%">
                                   <Columns>

                                       <asp:TemplateField HeaderText="Bil">
                                           <ItemTemplate>
                                               <%# Container.DataItemIndex + 1 %>
                                           </ItemTemplate>
                                           <ItemStyle HorizontalAlign="Right" Width="2%" />
                                       </asp:TemplateField>

                                       <%--      <asp:TemplateField Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDtId" runat="server" Text='<%# Eval("AR01_BilDtID")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                                       <asp:TemplateField HeaderText="KW">
                                           <ItemTemplate>
                                               <%--<asp:HiddenField ID="hidKW" runat="server" Value='<%#Eval("KodKw")%>' />
                                            <asp:DropDownList ID="ddlKW" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlKW_SelectedIndexChanged" Width="150px">
                                            </asp:DropDownList>
                                            <div>
                                               <asp:RequiredFieldValidator ID="rfvKW" runat="server" CssClass="text-danger" ControlToValidate="ddlKW" InitialValue="0" ErrorMessage="Sila pilih KW" ValidationGroup="grpSimpan" Display="Dynamic" />
                                            </div>--%>

                                               <asp:Label ID="lblKodKW" runat="server" Text='<%#Eval("KodKw")%>' />
                                           </ItemTemplate>
                                           <ItemStyle HorizontalAlign="Center" Width="50px" />
                                       </asp:TemplateField>

                                       <asp:TemplateField HeaderText="KO">
                                           <ItemTemplate>
                                               <%-- <asp:HiddenField ID="hidKO" runat="server" Value='<%#Eval("KodKO")%>' />
                                            <asp:DropDownList ID="ddlKO" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlKO_SelectedIndexChanged" Width="100px">
                                            </asp:DropDownList>
                                            <div>
                                                <asp:RequiredFieldValidator ID="rfvKO" runat="server" CssClass="text-danger" ControlToValidate="ddlKO" InitialValue="0" ErrorMessage="Sila pilih KO" ValidationGroup="grpSimpan" Display="Dynamic" />
                                            </div>--%>

                                               <asp:Label ID="lblKodKO" runat="server" Text='<%#Eval("KodKO")%>' />
                                           </ItemTemplate>
                                           <ItemStyle HorizontalAlign="Center" Width="50px" />
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="PTj">
                                           <ItemTemplate>
                                               <%-- <asp:HiddenField ID="hidPTj" runat="server" Value='<%#Eval("kodPTJ")%>' />
                                            <asp:DropDownList ID="ddlPTj" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlPTj_SelectedIndexChanged" Width="150px">
                                            </asp:DropDownList>
                                            <div>
                                                <asp:RequiredFieldValidator ID="rfvPTj" runat="server" CssClass="text-danger" ControlToValidate="ddlPTj" InitialValue="0" ErrorMessage="Sila pilih PTj" ValidationGroup="grpSimpan" Display="Dynamic" />
                                            </div>--%>
                                               <asp:Label ID="lblKodPTj" runat="server" Text='<%#Eval("kodPTJ")%>' />
                                           </ItemTemplate>
                                           <ItemStyle HorizontalAlign="Center" Width="50px" />
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="KP">
                                           <ItemTemplate>
                                               <%-- <asp:HiddenField ID="hidKP" runat="server" Value='<%#Eval("kodKP")%>' />
                                            <asp:DropDownList ID="ddlKP" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlKP_SelectedIndexChanged" Width="150px">
                                            </asp:DropDownList>
                                            <div>
                                                <asp:RequiredFieldValidator ID="rfvKP" runat="server" CssClass="text-danger" ControlToValidate="ddlKP" InitialValue="0" ErrorMessage="Sila pilih KP" ValidationGroup="grpSimpan" Display="Dynamic" />
                                            </div>--%>
                                               <asp:Label ID="lblKodKP" runat="server" Text='<%#Eval("kodKP")%>' />
                                           </ItemTemplate>
                                           <ItemStyle HorizontalAlign="Center" Width="50px" />
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Vot">
                                           <ItemTemplate>
                                               <%--<asp:HiddenField ID="hidVot" runat="server" Value='<%#Eval("kodVot")%>' />
                                            <asp:DropDownList ID="ddlVot" runat="server" AutoPostBack="true" CssClass="form-control" Width="150px">
                                            </asp:DropDownList>
                                            <div>
                                                <asp:RequiredFieldValidator ID="rfvVot" runat="server" CssClass="text-danger" ControlToValidate="ddlVot" InitialValue="0" ErrorMessage="Sila pilih Vot" ValidationGroup="grpSimpan" Display="Dynamic" />
                                            </div>--%>
                                               <asp:Label ID="lblKodVot" runat="server" Text='<%#Eval("kodVot")%>' />
                                           </ItemTemplate>
                                           <ItemStyle HorizontalAlign="Center" Width="50px" />
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Perkara">
                                           <ItemTemplate>
                                               <%-- <asp:TextBox ID="txtPerkara" runat="server" CssClass="form-control" Style="width: 100%;" Text='<%#Eval("AR01_Perkara")%>'></asp:TextBox>
                                            <div>
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPerkara" CssClass="text-danger" ErrorMessage="Masukkan Perkara" ValidationGroup="grpSimpan" Display="Dynamic" />
                                            </div>--%>
                                               <asp:Label ID="lblPerkara" runat="server" Text='<%#Eval("AR01_Perkara")%>' />
                                           </ItemTemplate>
                                           <ItemStyle HorizontalAlign="Left" Width="300px" />
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Kuantiti">
                                           <ItemTemplate>
                                               <%-- <asp:TextBox ID="txtKuantiti" runat="server" AutoPostBack="true" CssClass="form-control rightAlign" onkeypress="return isNumberKey(event,this)" OnTextChanged="txtKuantiti_TextChanged" Style="width: 100%;" Text='<%#Eval("AR01_Kuantiti", "{0:N0}")%>'></asp:TextBox>
                                            <div>
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtKuantiti" CssClass="text-danger" ErrorMessage="Masukkan Kuantiti" ValidationGroup="grpSimpan" Display="Dynamic" InitialValue="0" />
                                            </div>--%>
                                               <asp:Label ID="lblKuantiti" runat="server" Text='<%#Eval("AR01_Kuantiti")%>' />
                                           </ItemTemplate>
                                           <ItemStyle HorizontalAlign="center" Width="50px" />
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Harga (RM)">
                                           <FooterTemplate>
                                               <div style="text-align: right; font-weight: bold;">
                                                   <asp:Label runat="server" Text="Jumlah Besar (RM)" />
                                               </div>
                                           </FooterTemplate>
                                           <ItemTemplate>
                                               <%--<asp:TextBox ID="txtHarga" runat="server" AutoPostBack="true" CssClass="form-control rightAlign" onkeypress="return isNumberKey(event,this)" OnTextChanged="txtHarga_TextChanged" Style="width: 100%;" Text='<%#Eval("AR01_kadarHarga", "{0:###,###,###.00}")%>'></asp:TextBox>
                                            <div>
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtHarga" CssClass="text-danger" ErrorMessage="Masukkan Harga" ValidationGroup="grpSimpan" Display="Dynamic" InitialValue="0" />
                                            </div>--%>

                                               <asp:Label ID="lblHarga" runat="server" Text='<%#Eval("AR01_kadarHarga", "{0:N2}")%>' />
                                           </ItemTemplate>
                                           <ItemStyle HorizontalAlign="Right" Width="150px" />
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Jumlah (RM)">
                                           <FooterTemplate>
                                               <div style="text-align: right;">
                                                   <asp:Label ID="lblTotJum" runat="server" ClientIDMode="Static" CssClass="cssTotJum" Font-Bold="true" ForeColor="#003399" />
                                               </div>
                                           </FooterTemplate>
                                           <ItemTemplate>
                                               <asp:Label ID="lblJumlah" runat="server" ForeColor="#003399" Text='<%#Eval("AR01_Jumlah", "{0:N2}")%>' />
                                           </ItemTemplate>
                                           <ItemStyle HorizontalAlign="Right" Width="150px" />
                                       </asp:TemplateField>
                                       <%--       <asp:TemplateField HeaderText="Tindakan">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnTambah" runat="server" CausesValidation="true" CssClass="btn " OnClick="lbtnTambah_Click" ToolTip="Tambah transaksi">
                        <i class="fas fa-plus fa-lg"></i>
                                               </asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            &nbsp;
                                            <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete" CssClass="btn-xs" OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');" ToolTip="Padam">
										                    <i class="far fa-trash-alt fa-lg"></i>
                                                        </asp:LinkButton>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    </asp:TemplateField>--%>
                                   </Columns>
                                   <SelectedRowStyle ForeColor="Blue" />
                               </asp:GridView>
                           </div>

                       </div>
                   </div>

               </div>

               <div class="row">
               <div class="panel panel-default" style="width: 50%;">
                   <div class="panel-heading">Maklumat Pemohon</div>
                   <div class="panel-body">
                       <table style="width: 100%;">
                           <tr>
                               <td style="width: 106px; height: 23px;">Nama Pemohon</td>
                               <td style="height: 23px">:</td>
                               <td style="height: 23px">
                                   <asp:Label ID="lblNoPmhn" runat="server"></asp:Label>
                                   &nbsp;-&nbsp;<asp:Label ID="lblNamaPemohon" runat="server"></asp:Label>
                                   &nbsp;</td>
                               <td style="height: 23px"></td>
                           </tr>
                           <tr>
                               <td style="width: 106px">Jawatan</td>
                               <td>:</td>
                               <td>
                                   <asp:Label ID="lblJawatan" runat="server"></asp:Label>
                               </td>
                               <td>&nbsp;</td>
                           </tr>
                       </table>
                   </div>
               </div>
                   </div>

               <div style="text-align: center; margin-top: 50px;margin-bottom:100px;">
                   <asp:LinkButton ID="lBtnSimpan" runat="server" CssClass="btn " Width="150px" ValidationGroup="grpSimpan"  OnClientClick="return confirm('Anda pasti untuk simpan rekod ini?');">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                   </asp:LinkButton>

                   &nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:LinkButton ID="lbtnHapus" runat="server" CssClass="btn" OnClientClick="return confirm('Anda pasti untuk hapuskan invois ini?');" Visible="false">
                     <i class="fas fa-trash-alt"></i> &nbsp;&nbsp; Hapus
                 </asp:LinkButton>
               </div>
           </div>

          <%-- <asp:Button ID="btnPopup1" runat="server" style="display:none;"   />                
                    <ajaxToolkit:ModalPopupExtender ID="mpeLstPel" runat="server" BackgroundCssClass="modalBackground" PopupControlID="PnlLstPel" TargetControlID="btnPopup1" CancelControlID="Button2">
                                     </ajaxToolkit:ModalPopupExtender>                       
            <asp:Panel ID="PnlLstPel" runat="server" BackColor="White" Width="800px" style="display:;">
               
                <table  style="border: Solid 3px #D5AA00; width: 100%; height: 100%;">
                    <tr style="background: linear-gradient(to bottom, #fceabb 0%,#fccd4d 50%,#f8b500 51%,#fbdf93 100%);">
                         <td style="height: 10%;text-align:center;" class="">
                            <b> Senarai Pelajar</b></td>
                        <td style="width:50px;text-align:center;">   
                           <button runat="server" id="Button2" title="Tutup" class="btnNone ">
    <i class="far fa-window-close fa-2x"></i>
</button></td>
                                                                            
                    </tr>
                    <tr>
                        <td colspan="2" >
                            <div class="row" style="margin-top:10px;width:100%;">
                                   
                                <table style="width: 100%;">
                  <tr>
                      <td style="width:160px;">
                          <asp:DropDownList ID="ddlFilter" runat="server" CssClass="form-control" AutoPostBack ="true" > 
                              <asp:ListItem Text="No. Matrik" Value="1" Selected="True"></asp:ListItem>                 
                              <asp:ListItem Text="Nama Pelajar" Value="2"></asp:ListItem>
                              
                          </asp:DropDownList>&nbsp;&nbsp;
                          &nbsp;&nbsp;</td>
                      <td>
                          <asp:TextBox ID="txtFilter" runat="server" CssClass="form-control" Style="width: 300px;"></asp:TextBox>
                      </td>
                  </tr>
                                    <tr>
                                        <td >
                                            
                                        </td>
                                        <td>
                                            <div style="margin-bottom:20px;margin-top:10px;">
                                                <asp:LinkButton ID="lbtnCariPel" runat="server" CssClass="btn " ToolTip="Simpan" Width="80px">
						<i class="fas fa-search fa-lg"></i> &nbsp;&nbsp; Cari
					</asp:LinkButton>
                                            </div>
                                        </td>
                                    </tr>
                 </table>                              
                                <div style="margin-top: 10px;">
                                    <asp:GridView ID="gvLstPel" runat="server" AllowSorting="True"
                                        ShowHeaderWhenEmpty="True" AutoGenerateColumns="False"
                                        EmptyDataText=" " CssClass="table table-striped table-bordered table-hover"
                                        Width="100%" Height="100%" HeaderStyle-BackColor="#6699FF"
                                        Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid"
                                        ShowFooter="True" AllowPaging ="true" PageSize="15">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Bil">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBil" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No. Matrik">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNoMatrik" runat="server" Text='<%# Eval("NoMatrik")%>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Nama Pelajar">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNmPel" runat="server" Text='<%# Eval("NamaPel")%>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left"/>
                                            </asp:TemplateField>

                                             <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblKodKursus" runat="server" Text='<%# Eval("KodKursus")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                             <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIdPel" runat="server" Text='<%# Eval("IDPel")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                                                       
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnSelect" runat="server" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CommandName="Select" CssClass="btn-xs" ToolTip="Pilih">
                              <i class="fas fa-edit"></i>                           
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="20px" />
                                            </asp:TemplateField>

                                        </Columns>
                                        <HeaderStyle BackColor="#6699FF"></HeaderStyle>
                                    </asp:GridView>
                                </div>                                                                                                                                                                          
                            </div>

                        </td>
                    </tr>                           
          </table> 
                
            </asp:Panel>--%>
           
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
