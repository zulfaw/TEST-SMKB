<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Sokongan_Invois_Pelajar.aspx.vb" Inherits="SMKB_Web_Portal.Sokongan_Invois_Pelajar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>
    <asp:UpdatePanel runat="server">
       <ContentTemplate>

           <div id="divList" runat="server">

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
                    <asp:LinkButton ID="lbtnKembali" runat="server" CssClass="btn " Width="100px" ToolTip="">
						<i class="fas fa-long-arrow-alt-left  fa-lg"></i> &nbsp;&nbsp; Kembali
                    </asp:LinkButton>
                </div>

               <div class="panel panel-default" style="width: 85%;">
                   <div class="panel-heading">Maklumat Invois</div>
                   <div class="panel-body">
                       <table style="width: 100%;">
                           <tr>
                               <td style="width: 130px;">No. Invois Sementara </td>
                               <td>:</td>
                               <td style="width: 450px;">
                                   <asp:Label ID="lblNoInvSem" runat="server" ForeColor="#003399"></asp:Label>

                               </td>
                               <td>&nbsp;</td>
                               <td style="width:50px;">Status</td>
                               <td style="width:10px;">:</td>
                               <td colspan="4">
                                   <asp:Label ID="lblStat" runat="server" ForeColor="#003399"></asp:Label>
                               </td>

                           </tr>
                           <tr>
                               <td>Tarikh Mohon</td>
                               <td>:</td>
                               <td>
                                   <asp:Label ID="lblTkhMohon" runat="server" ForeColor="#003399"></asp:Label>
                               </td>
                               <td></td>
                               <td></td>
                               <td></td>
                               <td colspan="4"></td>
                           </tr>
                           <tr>
                               <td>PTJ</td>
                               <td>:</td>
                               <td rowspan="2" style="vertical-align:top">
                                   <asp:Label ID="lblKodPTj" runat="server" ForeColor="#003399"></asp:Label>
                                   &nbsp;-&nbsp;<asp:Label ID="lblNmPTj" runat="server" ForeColor="#003399"></asp:Label>
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
                               <td colspan="4">&nbsp;</td>
                           </tr>
                           <tr>
                               <td>Bank</td>
                               <td>:</td>
                               <td>
                                   
                                   <asp:Label ID="lblBank" runat="server" ForeColor="#003399"></asp:Label>
                               </td>
                               <td>&nbsp;</td>
                               <td>&nbsp;</td>
                               <td>&nbsp;</td>
                               <td colspan="4">
                               
                                   &nbsp;</td>
                           </tr>
                           <tr>
                               <td>Kategori Pelajar</td>
                               <td>:</td>
                               <td>
                                   <asp:Label ID="lblKatPel" runat="server" ForeColor="#003399"></asp:Label>
                               </td>
                               <td>&nbsp;</td>
                               <td class="50">Alamat </td>
                               <td>:</td>
                               <td colspan="4">
                                   <asp:Label ID="lblAlmt1" runat="server" ForeColor="#003399"></asp:Label>
                               </td>
                           </tr>
                           <tr>
                               <td>&nbsp;</td>
                               <td>&nbsp;</td>
                               <td>
                                  
                               </td>
                               <td>&nbsp;</td>
                               <td>&nbsp;</td>
                               <td>&nbsp;</td>
                               <td colspan="4">
                                   
                                   <asp:Label ID="lblAlmt2" runat="server" ForeColor="#003399"></asp:Label>
                               </td>
                           </tr>
                           <tr>
                               <td>Nama Penerima</td>
                               <td>:</td>
                               <td>
                                   <asp:Label ID="lblNmPen" runat="server" ForeColor="#003399"></asp:Label>
                               </td>
                               <td>&nbsp;</td>
                               <td>Negeri</td>
                               <td>:</td>
                               <td colspan="4">                                   
                                   <asp:Label ID="lblNegeri" runat="server" ForeColor="#003399"></asp:Label>
                               </td>

                           </tr>
                           <tr>
                               <td>ID Penerima</td>
                               <td>:</td>
                               <td>
                                   <asp:Label ID="lblIdPen" runat="server" ForeColor="#003399"></asp:Label>
                               </td>
                               <td>&nbsp;</td>
                               <td>Negara</td>
                               <td>:</td>
                               <td colspan="4">
                                  
                                   <asp:Label ID="lblNegara" runat="server" ForeColor="#003399"></asp:Label>
                               </td>
                           </tr>
                           <tr>
                               <td style="vertical-align: top;">Tujuan</td>
                               <td style="vertical-align: top;">:</td>
                               <td rowspan="3" style="vertical-align:top">
                                  
                                   <asp:Label ID="lblTujuan" runat="server" ForeColor="#003399"></asp:Label>
                               </td>
                               <td>&nbsp;</td>
                               <td>Bandar</td>
                               <td>:</td>
                               <td style="width:250px;">
                                   
                                   <asp:Label ID="lblBandar" runat="server" ForeColor="#003399"></asp:Label>
                               </td>
                               <td style="width:60px;">Poskod &nbsp;&nbsp;</td>
                               <td>:</td>
                               <td>
                                   <asp:Label ID="lblPoskod" runat="server" ForeColor="#003399"></asp:Label>
                               </td>
                           </tr>
                           <tr>
                               <td style="vertical-align: top;">&nbsp;</td>
                               <td style="vertical-align: top;">&nbsp;</td>
                               <td>&nbsp;</td>
                               <td>&nbsp;</td>
                               <td>&nbsp;</td>
                               <td>&nbsp;</td>
                               <td>&nbsp;</td>
                               <td>&nbsp;</td>
                               <td>&nbsp;</td>
                           </tr>
                           <tr>
                               <td style="vertical-align: top;">&nbsp;</td>
                               <td style="vertical-align: top;">&nbsp;</td>
                               <td>&nbsp;</td>
                               <td>&nbsp;</td>
                               <td>&nbsp;</td>
                               <td>&nbsp;</td>
                               <td>&nbsp;</td>
                               <td>&nbsp;</td>
                               <td>&nbsp;</td>
                           </tr>
                           <tr>
                               <td>No. Rujukan</td>
                               <td>:</td>
                               <td>
                                   
                                   <asp:Label ID="lblNoRuj" runat="server" ForeColor="#003399"></asp:Label>
                                   
                               </td>
                               <td>&nbsp;</td>
                               <td>No.Tel</td>
                               <td>:</td>
                               <td>                                  
                                   <asp:Label ID="lblTel" runat="server" ForeColor="#003399"></asp:Label>
                                   </td>
                               <td>No.Fax</td>
                               <td>:</td>
                               <td>
                                   <asp:Label ID="lblFax" runat="server" ForeColor="#003399"></asp:Label>
                               </td>
                           </tr>
                           <tr>
                               <td>Untuk Perhatian</td>
                               <td>:</td>
                               <td>
                                   <asp:Label ID="lblUP" runat="server" ForeColor="#003399"></asp:Label>
                               </td>
                               <td>&nbsp;</td>
                               <td>Emel</td>
                               <td>:</td>
                               <td colspan="4">                                  
                                   <asp:Label ID="lblEmel" runat="server" ForeColor="#003399"></asp:Label>
                               </td>
                           </tr>

                       </table>

  
                   </div>
               </div>

               
               <div class="row" style="margin-left:30px;">
                    <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" Width="100%" CssClass="tabCtrl" ActiveTabIndex="0" AutoPostBack="true">
                                <ajaxToolkit:TabPanel ID="tabTrans" runat="Server" HeaderText="Transaksi">

                                    <ContentTemplate>
                                        <div style="width: 95%;">
                                            <asp:GridView ID="gvTrans" runat="server" AllowSorting="True" AutoGenerateColumns="False" BorderStyle="Solid" CssClass="table table-striped table-bordered table-hover" EmptyDataText=" Tiada rekod" Font-Size="8pt" ShowFooter="True" ShowHeaderWhenEmpty="True" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Bil">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" Width="2%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="KW">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblKodKW" runat="server" text='<%# Eval("KodKw")%>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="KO">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblKodKO" runat="server" text='<%# Eval("KodKO")%>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PTj">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPTj" runat="server" text='<%# Eval("kodPTJ")%>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="KP">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblKP" runat="server" text='<%# Eval("kodKP")%>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Vot">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVot" runat="server" text='<%# Eval("kodVot")%>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Perkara">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPerkara" runat="server" text='<%# Eval("AR01_Perkara")%>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="300px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kuantiti">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblKuantiti" runat="server" text='<%# Eval("AR01_Kuantiti")%>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Harga (RM)">
                                                        <ItemTemplate>
                                                             <asp:Label ID="lblHarga" runat="server" text='<%# Eval("AR01_kadarHarga", "{0:###,###,###.00}")%>' />
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="text-align: right; font-weight: bold;">
                                                                <asp:Label runat="server" Text="Jumlah Besar (RM)" />
                                                            </div>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Jumlah (RM)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblJumlah" runat="server" text='<%# Eval("AR01_Jumlah", "{0:###,###,###.00}")%>' ForeColor="#003399"/>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="text-align: right;">
                                                                <asp:Label ID="lblTotJum" runat="server" Font-Bold="true"  ForeColor="#003399" />
                                                            </div>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <SelectedRowStyle ForeColor="Blue" />
                                            </asp:GridView>
                                            </div>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>

                                <ajaxToolkit:TabPanel ID="tabLamp" runat="Server" HeaderText="Lampiran">
                                    <ContentTemplate>
                                        <div style="width: 95%;">                                    
                                            <asp:GridView ID="gvLamp" runat="server" AllowSorting="True" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False"
                                                CssClass="table table-striped table-bordered table-hover" Width="100%" Font-Size="8pt" BorderStyle="Solid" ShowFooter="True">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Bil">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="2%" HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField Visible ="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIdLamp" runat="server" text='<%# Eval("AR02_IdLamp")%>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="250px" />
                                                    </asp:TemplateField>
                                    
                                                    <asp:TemplateField HeaderText="Nama Penerima">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNmPen" runat="server" text='<%# Eval("AR02_NamaPenerima")%>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="250px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="No. KP/Passport">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNoKP" runat="server" text='<%# Eval("AR02_NoKP")%>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="No. Matrik">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNoMatrik" runat="server" text='<%# Eval("AR02_NoMatrik")%>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Kursus">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblKursus" runat="server" text='<%# Eval("AR02_Kursus")%>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Butiran">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblButiran" runat="server" text='<%# Eval("AR02_Butiran")%>' />
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                        <div style="text-align: right; font-weight: bold;">
                                                            <asp:Label runat="server" Text="Jumlah Besar (RM)" />
                                                        </div>
                                                    </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="350px"/>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Jumlah (RM)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblJumlah" runat="server" text='<%# Eval("AR02_Amaun", "{0:###,###,###.00}")%>' ForeColor="#003399"/>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                        <div style="text-align: right;">
                                                            <asp:Label ID="lblTotJum" runat="server" Font-Bold="true" ForeColor="#003399" />
                                                        </div>
                                                    </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" Width="80px" />
                                                    </asp:TemplateField>

                                                </Columns>

                                                <EmptyDataTemplate>
                                                </EmptyDataTemplate>
                                                <SelectedRowStyle ForeColor="Blue" />
                                            </asp:GridView>
                                        </div>
                                                       	
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>

                            </ajaxToolkit:TabContainer>
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
                                   &nbsp;-&nbsp;<asp:Label ID="lblNmPemohon" runat="server"></asp:Label>
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
                           <tr>
                               <td style="width: 106px; height: 19px;">PTj</td>
                               <td style="height: 19px">:</td>
                               <td style="height: 19px">
                                   <asp:Label ID="lblKodPTjPmhn" runat="server"></asp:Label>
                                   &nbsp;-
                                   <asp:Label ID="lblNmPTjPmhn" runat="server"></asp:Label>
                               </td>
                               <td style="height: 19px"></td>
                           </tr>
                       </table>
                   </div>
               </div>
                   </div>

               <div class="row">
          <div class="panel panel-default" style="width: 700px;">
              <div class="panel-heading">Penyokong</div>
    <div class="panel-body">
        <table style="width: 100%;">
                                <tr>
                                    <td style="width: 106px; height: 23px;">Nama Penyokong</td>
                                    <td style="height: 23px">:</td>
                                    <td style="height: 23px">
                                        <asp:label ID="lblNoStafSokong" runat="server" ></asp:label>
                                        &nbsp;-&nbsp;<asp:label ID="lblNmStafSokong" runat="server" ></asp:label>
                                        &nbsp;</td>
                                    <td style="height: 23px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 106px">Jawatan</td>
                                    <td>:</td>
                                    <td>
                                        <asp:label ID="lblJawSokong" runat="server"></asp:label>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>

                                <tr>
                                    <td style="width: 106px">PTj</td>
                                    <td>:</td>
                                    <td>
                                        <asp:Label ID="lblKodPTjSokong" runat="server"></asp:Label>
                                        &nbsp;-&nbsp;<asp:Label ID="lblNmPTjSokong" runat="server"></asp:Label>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>

                                <tr>
                                    <td style="width: 106px">Tarikh Sokong</td>
                                    <td>:</td>
                                    <td>
                                        <asp:label ID="lblTkhSokong" runat="server"></asp:label>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="vertical-align:top;">Ulasan</td>
                                    <td style="vertical-align:top;">:</td>
                                    <td>
                                
                                <asp:TextBox ID="txtUlasan" runat="server" cssClass="form-control" Height="70px" textmode="multiline" Width="90%" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                    </table> 
                      </div>                 
              </div>
    </div>

               <div class="row">
              <div style="text-align:center;margin-top:50px;">
                     <asp:LinkButton ID="lbtnSokong" runat="server" CssClass="btn " OnClientClick="return confirm('Anda pasti untuk sokong permohonan invois ini?');">
						<i class="fas fa-check-circle"></i>&nbsp;&nbsp;&nbsp;Sokong
					</asp:LinkButton>
                     
                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                         <asp:LinkButton ID="lbtnXSokong" runat="server" CssClass="btn " ValidationGroup="grpXLulus" OnClientClick="return confirm('Permohonan invois ini akan disimpan sebagai Tidak Sokong. Teruskan?');">
						<i class="fas fa-times-circle"></i>&nbsp;&nbsp;&nbsp;Tidak Sokong
					</asp:LinkButton> 
                  <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="grpXLulus" DisplayMode="SingleParagraph" HeaderText="Sila masukkan ulasan jika tidak sokong!" />                                         
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
