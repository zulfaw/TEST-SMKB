<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="PEMBATALAN_INVOIS_KEWANGAN.aspx.vb" Inherits="SMKB_Web_Portal.PEMBATALAN_INVOIS_KEWANGAN" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>

      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="divList" runat="server">

                <div class="row" style="width: 700px;">
                    <div class="well">
                        <table style="width: 100%;">
                            <tr>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:DropDownList ID="ddlCarian" runat="server" AutoPostBack="true" CssClass="form-control" Style="width: 35%; height: 21px;">
                                        <asp:ListItem Text="- KESELURUHAN -" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="No. Invois" Value="1"></asp:ListItem>
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
                    <div class="panel panel-default" style="width: 80%;">
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

                                    &nbsp;&nbsp;<b style="color: #969696;">|</b> &nbsp;&nbsp;
                    Status : &nbsp;
                    <asp:DropDownList ID="ddlFilStat" runat="server" AutoPostBack="True" CssClass="form-control" />

                                </div>


                            </div>

                            <asp:GridView ID="gvLst" runat="server"  AllowPaging= "true" AllowSorting="True" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText=" " 
                    PageSize="25" cssclass="table table-striped table-bordered table-hover" Width="100%" Height="100%" HeaderStyle-BackColor="#6699FF" Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" ShowFooter="True"  >
                                 <columns> 
                           
                                                <asp:TemplateField HeaderText="Bil">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBil" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5%" />
                                                </asp:TemplateField>
                                              
                                     <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIdBil" runat="server" Text='<%# Eval("AR01_IdBil") %>' />
                                                    </ItemTemplate>                        
                                                </asp:TemplateField>

                                     <asp:TemplateField HeaderText ="No. Invois">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoInv" runat="server" Text='<%# Eval("AR01_NoBil")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                                    </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Tarikh Mohon">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTkhMhn" runat="server" Text='<%# Eval("AR01_TkhMohon", "{0:dd/MM/yyyy}") %>' />
                                                    </ItemTemplate>                                                   
                                                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Tujuan">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTujuan" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("AR01_Tujuan"))) %>' />
                                                    </ItemTemplate>                                                   
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Nama Penerima">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNPenerima" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("AR01_NamaPenerima"))) %>' />
                                                    </ItemTemplate>                                                   
                                                    <ItemStyle HorizontalAlign="Left" Width="250px" />
                                                </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Kategori">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKat" runat="server" Text='<%# Eval("AR01_Kategori")%>' />
                                            &nbsp;-&nbsp;
                                            <asp:Label ID="lblButkat" runat="server" Text='<%# Eval("ButKat")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="150px" />
                                    </asp:TemplateField>

                                               <asp:TemplateField HeaderText="Jumlah (RM)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJum" runat="server" text='<%# Eval("AR01_JUMLAH", "{0:###,###,###.00}")%>' ForeColor="#003399"/>

                                                    </ItemTemplate>                                                   
                                                    <ItemStyle HorizontalAlign="right" Width="150px" />
                                                </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblKodStatus" runat="server" Text='<%# Eval("ar01_statusdok") %>' Visible="false"/>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("ButStatus") %>' />
                                                    </ItemTemplate>                                                   
                                                    <ItemStyle HorizontalAlign="Left" Width="200px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                <ItemTemplate>
                                                <asp:LinkButton ID="lbtnSelect" runat="server" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CommandName="Select" CssClass="btn-xs" ToolTip="Pilih">
                                                        <i class="fas fa-edit"></i>
                                                    </asp:LinkButton>                             
                                                </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                        </asp:TemplateField>
                                            </columns>
                            <HeaderStyle BackColor="#6699FF" />
                    </asp:GridView>

                        </div>
                    </div>
                </div>
            </div>

            <div id="divDetail" runat="server" style ="width:100%;">

        <div class="row">
				<asp:LinkButton ID="lbtnKembali" runat="server" CssClass="btn " Width="100px" ToolTip=""  >
						<i class="fas fa-long-arrow-alt-left  fa-lg"></i> &nbsp;&nbsp; Kembali
				</asp:LinkButton>
			</div>
          
        <div class="row" style="width:95%;">
        <div class="panel panel-default" style="width:95%;">
              <div class="panel-heading">Maklumat Invois</div>
    <div class="panel-body">
        <table style="width: 100%;">
                        <tr>
                            <td style="width: 160px">No. Invois</td>
                            <td>:</td>
                            <td style="width: 502px">
                                         <asp:label ID="lblNoInv" runat="server" ForeColor="#003399"></asp:label>
                                                                    
                            <td>&nbsp;</td>
                            <td style="width: 100px">Tarikh Mohon</td>
                            <td>:</td>
                            <td colspan="6">
                                   <asp:label ID="lblTkhMhn2" runat="server" ForeColor="#003399"></asp:label>
                                 </td>
                            
                        </tr>
                        <tr>
                            <td >No. Jurnal Sementara</td>
                            <td>:</td>
                            <td style="width: 502px">
                                <asp:TextBox ID="txtNoJurnSem" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 150px;"></asp:TextBox>
                               
                                <td>&nbsp;</td>
                            <td >Tarikh Invois</td>
                            <td>:</td>
                            <td colspan="6">
                                         <asp:label ID="lblTkhInv" runat="server" ForeColor="#003399"></asp:label>
                                                                    
                                 </td>
                            
                        </tr>
                        <tr>
                            <td >&nbsp;</td>
                            <td>&nbsp;</td>
                            <td style="width: 502px">
                                         &nbsp;</td>
                            <td>&nbsp;</td>
                            <td >Status</td>
                            <td>:</td>
                            <td colspan="6">
                                   <asp:label ID="lblStatus" runat="server" ForeColor="#003399"></asp:label>
                            </td>
                            
                        </tr>
                        <tr>
                            <td >PTJ</td>
                            <td>:</td>
                            <td style="width: 502px">
                                <asp:Label ID="lblKodPtj" runat="server" ForeColor="#003399"></asp:Label>
                                &nbsp;-&nbsp;<asp:Label ID="lblNPTJ" runat="server" ForeColor="#003399"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                            <td >&nbsp;</td>
                            <td>&nbsp;</td>
                            <td colspan="6">
                                   &nbsp;</td>
                            
                        </tr>
                        <tr>
                            <td >Bank</td>
                            <td>:</td>
                            <td style="width: 502px">
                                         <asp:label ID="lblKodBank" runat="server" ForeColor="#003399"></asp:label>
                                         &nbsp;- &nbsp;
                                <asp:label ID="lblNmBank" runat="server" ForeColor="#003399"></asp:label>
                                                                    
                            </td>
                            <td>&nbsp;</td>
                            <td class="50" >Alamat </td>
                            <td>:</td>
                            <td colspan="6">
                                <label id="lblMsgNoRuj" class="control-label" for="" style="display:none;color:#820303;">
                               (Masukkan No. Rujukan)
                                </label>
                                   <asp:label ID="lblAlmt1" runat="server" ForeColor="#003399"></asp:label>
                            </td>
                            
                        </tr>
                        <tr>
                            <td >Kategori</td>
                            <td>:</td>
                            <td style="width: 502px">
                                <asp:Label ID="lblKodKat" runat="server" ForeColor="#003399"></asp:Label>
                                &nbsp; - &nbsp;&nbsp;<asp:Label ID="lblKat" runat="server" ForeColor="#003399"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                            <td >&nbsp;</td>
                            <td>&nbsp;</td>
                            <td colspan="6">
                                   <asp:label ID="lblAlmt2" runat="server" ForeColor="#003399"></asp:label>
                            </td>
                            
                        </tr>
                        <tr>
                            <td >Nama Penerima</td>
                            <td>:</td>
                            <td style="width: 502px">
                                         <asp:Label ID="lblNmPen" runat="server" ForeColor="#003399"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                            <td >Negeri</td>
                            <td>:</td>
                            <td colspan="6">
                                <asp:label ID="lblKodNegeri" runat="server" ForeColor="#003399"></asp:label>
                                &nbsp; - &nbsp;
                                   <asp:Label ID="lblNegeri" runat="server" ForeColor="#003399"></asp:Label>
                            </td>
                           
                        </tr>
                        <tr>
                            <td >ID / No.KP Penerima</td>
                            <td>:</td>
                            <td style="width: 502px">
                                         <asp:Label ID="lblIdPen" runat="server" ForeColor="#003399"></asp:Label>
                                                                    
                            </td>
                            <td>&nbsp;</td>
                            <td >Negara</td>
                            <td>:</td>
                            <td colspan="6">
                                   <asp:label ID="lblKodNegara" runat="server" ForeColor="#003399"></asp:label>
                                &nbsp; - &nbsp;
                                   <asp:label ID="lblNegara" runat="server" ForeColor="#003399"></asp:label>

                            </td>
                            
                        </tr>
                        <tr>
                            <td >Tujuan</td>
                            <td>:</td>
                            <td style="width: 502px">
                                         <asp:Label ID="lblTujuan" runat="server" ForeColor="#003399"></asp:Label>
                                                                    
                            </td>
                            <td>&nbsp;</td>
                            <td >Bandar</td>
                            <td>:</td>
                            <td colspan="2">
                                   <asp:label ID="lblBandar" runat="server" ForeColor="#003399"></asp:label>

                            </td>
                           
                            <td style="width:50px;">
                                Poskod</td>                           
                            <td>
                                :</td>
                           
                            <td colspan="2">
                                   <asp:label ID="lblPoskod" runat="server" ForeColor="#003399"></asp:label>

                            </td>
                           
                        </tr>
                        <tr>
                            <td>Untuk Perhatian</td>
                            <td >:</td>
                            <td style="width: 502px">
                                         <asp:Label ID="lblPerhatian" runat="server" ForeColor="#003399"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                            <td>No.Tel</td>
                            <td>:</td>
                            <td>
                                   <asp:label ID="lblNoTel" runat="server" ForeColor="#003399"></asp:label>

                                </td>
                            <td colspan="2">
                                No. Fax</td>
                            <td colspan="2">
                                :</td>
                            <td>
                                   <asp:label ID="lblNoFax" runat="server" ForeColor="#003399"></asp:label>

                            </td>
                            
                        </tr>
                        <tr>
                            <td>No. Rujukan</td>
                            <td>&nbsp;</td>
                            <td style="width: 502px">
                                <asp:Label ID="lblNoRuj" runat="server" ForeColor="#003399"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                            <td>Emel</td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:Label ID="lblEmel" runat="server" ForeColor="#003399"></asp:Label>
                            </td>
                            <td colspan="2">&nbsp;</td>
                            <td colspan="2">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>Berkontrak</td>
                            <td>:</td>
                            <td style="width: 502px">
                                <asp:Label ID="lblJenKontrak" runat="server" ForeColor="#003399"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td colspan="2">&nbsp;</td>
                            <td colspan="2">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td >Tarikh Mula Kontrak</td>
                            <td>:</td>

                          <td>
                                    <asp:Label ID="lblTkhMulaKon" runat="server" ForeColor="#003399"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                            <td >&nbsp;</td>
                            <td>&nbsp;</td>
                            <td colspan="6">
                                   &nbsp;</td>
                            


                        </tr>


                        <tr>
                            <td>Taikh Tamat Kontrak</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblTkhTamatKon" runat="server" ForeColor="#003399"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td colspan="6">&nbsp;</td>
                        </tr>
                        <tr>
                            <td>Tempoh Kontrak</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblTemKon" runat="server" ForeColor="#003399"></asp:Label>
                                &nbsp;
                                <asp:Label ID="lblJenTem" runat="server" ForeColor="#003399"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td colspan="6">&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td colspan="6">&nbsp;</td>
                        </tr>


                        </table>

        <br />
                                                         
        </div>           
              </div>
                  </div> 
      
        <div class="row">
            <div class="panel panel-default" style="width:95%;">
                    <div class="panel-heading">Transaksi</div>                 
                    <div class="panel-body">
					
                        <ajaxToolkit:TabContainer ID="TabContainer" runat="server" Width="100%" CssClass="tabCtrl" ActiveTabIndex="1" AutoPostBack="true">
                                <ajaxToolkit:TabPanel ID="tabTransAsl" runat="Server" HeaderText="Transaksi Asal">

                                    <ContentTemplate>                                           
                                            <asp:GridView ID="gvTransAsl" runat="server" AllowSorting="True" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText=" "
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
                                                             <asp:Label ID="lblKW" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("KodKw"))) %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="KO">
                                                        <ItemTemplate>
                                                             <asp:Label ID="lblKO" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("KodKO"))) %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="PTJ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPTj" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("KodPTJ"))) %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="KP">
                                                        <ItemTemplate>
                                                             <asp:Label ID="lblKP" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("KodKP"))) %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Vot">
                                                        <ItemTemplate>
                                                             <asp:Label ID="lblVot" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("KodVot"))) %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Butiran">
                                                        <ItemTemplate>
                                                             <asp:Label ID="lblButiran" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("MK06_Butiran"))) %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left"/>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Debit (RM)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDebit" runat="server" Text='<%# Eval("MK06_Debit", "{0:n2}") %>' ForeColor="#003399" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" Width="10%"/>
                                                        <FooterTemplate>
                                                            <div style="text-align: right;">
                                                                <asp:Label ID="lblTotDbt" runat="server" ForeColor="#003399" Font-Bold="true" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Kredit (RM)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblKredit" runat="server" Text='<%# Eval("MK06_Kredit", "{0:n2}") %>' ForeColor="#003399" />

                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" Width="10%"/>
                                                        <FooterTemplate>
                                                            <div style="text-align: right;">
                                                                <asp:Label ID="lblTotKdt" runat="server" ForeColor="#003399" Font-Bold="true" />

                                                            </div>

                                                        </FooterTemplate>

                                                    </asp:TemplateField>
                                                </Columns>

                                                <HeaderStyle BackColor="#6699FF"></HeaderStyle>
                                            </asp:GridView>
                                     

                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>

                                <ajaxToolkit:TabPanel ID="tabTransLej" runat="Server" HeaderText="Transaksi Lejar">
                                    <ContentTemplate>
                                        <div style="width: 95%;">                                    
                                            <asp:GridView ID="gvTransLej" runat="server" AllowSorting="True" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText=" "
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
                                                             <asp:Label ID="lblKW" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("KodKw"))) %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="KO">
                                                        <ItemTemplate>
                                                             <asp:Label ID="lblKO" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("KodKO"))) %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="PTJ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPTj" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("KodPTJ"))) %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="KP">
                                                        <ItemTemplate>
                                                             <asp:Label ID="lblKP" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("KodKP"))) %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Vot">
                                                        <ItemTemplate>
                                                             <asp:Label ID="lblVot" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("KodVot"))) %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Butiran">
                                                        <ItemTemplate>
                                                             <asp:Label ID="lblButiran" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("MK06_Butiran"))) %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left"/>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Debit (RM)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDebit" runat="server" Text='<%# Eval("MK06_Debit", "{0:n2}") %>' ForeColor="#003399" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" Width="10%"/>
                                                        <FooterTemplate>
                                                            <div style="text-align: right;">
                                                                <asp:Label ID="lblTotDbt" runat="server" ForeColor="#003399" Font-Bold="true" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Kredit (RM)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblKredit" runat="server" Text='<%# Eval("MK06_Kredit", "{0:n2}") %>' ForeColor="#003399" />

                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" Width="10%"/>
                                                        <FooterTemplate>
                                                            <div style="text-align: right;">
                                                                <asp:Label ID="lblTotKdt" runat="server" ForeColor="#003399" Font-Bold="true" />

                                                            </div>

                                                        </FooterTemplate>

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
        </div>

               
                <div class="row">
            <div class="panel panel-default" style="width: 700px;">
                <div class="panel-heading">
                    Maklumat Penyedia
                </div>
                <div class="panel-body">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 100px;">Nama Pemohon</td>
                            <td>:</td>
                            <td style="height: 23px">
                                <asp:Label ID="lblNoStaf" runat="server"></asp:Label>
                                &nbsp;-&nbsp;<asp:Label ID="lblNama" runat="server"></asp:Label>
                            </td>
                            <td style="height: 23px"></td>
                        </tr>
                        <tr>
                            <td>Jawatan</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblJwtn" runat="server"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                        </tr>

                        <tr>
                            <td>PTj</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblKodPTjPenyedia" runat="server"></asp:Label>
                                &nbsp;-&nbsp;<asp:Label ID="lblNamaPTjPenyedia" runat="server"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                        </tr>

                        <tr>
                            <td>Tarikh</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblTarikh" runat="server"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                        </tr>

                    </table>
                </div>
            </div>
        </div>
              
                <div class="row">
                    <div style="text-align: center; margin-top: 50px;">
                        <asp:LinkButton ID="lbtnBatal" runat="server" CssClass="btn" Width="100px" ToolTip="Batal Invois" OnClientClick="return confirm('Anda pasti untuk batal invois ini?');">
						<i class="fa fa-times fa-lg" aria-hidden="true"></i>&nbsp;&nbsp; Batal Invois
                        </asp:LinkButton>

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
