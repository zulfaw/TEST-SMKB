﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Sokongan_Invois_Kewangan.aspx.vb" Inherits="SMKB_Web_Portal.Sokongan_Invois_Kewangan" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>

     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

    <div id="divList" runat="server" class="divList">
        <div class="row" style="width:700px;">
            <div class="well">
                <table style="width: 100%;">                
                  <tr>
                      <td >Carian</td>
                      <td >:</td>
                      <td >
                          <asp:DropDownList ID="ddlCarian" runat="server" AutoPostBack="True" CssClass="form-control">
                              <asp:ListItem Selected="True" Text="- KESELURUHAN -" Value="0"></asp:ListItem>
                              <asp:ListItem Text="No. Invois Sementara" Value="1"></asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;&nbsp;
                          <asp:TextBox ID="txtCarian" runat="server" Width="250px" Enabled="false"></asp:TextBox>
                      </td>
                  </tr>
                  <tr>
                      <td ></td>
                      <td>&nbsp;</td>
                      <td>
                          <div style="margin-top:20px;">
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
                    <div class="panel-heading">Senarai Invois</div>
                <div class="panel-body">
                     <div class="GvTopPanel" style="height:33px;">
                <div style="float:left;margin-top: 5px;margin-left: 10px;">
                <label class="control-label" for="">Jumlah rekod :</label>&nbsp;<label class="control-label" id ="lblJumRekod" runat="server" style="color:mediumblue;" ></label>
                 
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
                <asp:GridView ID="gvLst" runat="server"  AllowPaging= "true" AllowSorting="True" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText=" " 
                    PageSize="25" cssclass="table table-striped table-bordered table-hover" Width="100%" Height="100%" HeaderStyle-BackColor="#6699FF" Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" ShowFooter="True"  >
                                 <columns> 
                           
                                                <asp:TemplateField HeaderText="Bil">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBil" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5%" />
                                                </asp:TemplateField>
                                              
                                                <asp:TemplateField HeaderText="No. Invois Sementara">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNoInvSem" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("AR01_NOBILSEM"))) %>' />
                                                    </ItemTemplate>                                                   
                                                    <ItemStyle HorizontalAlign="Left" Width="150px" />
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
                            <td style="width: 160px">No. Invois Sementara</td>
                            <td>:</td>
                            <td style="width: 502px">
                                         <asp:label ID="lblNoInvSem" runat="server" ForeColor="#003399"></asp:label>
                                                                    
                            <td>&nbsp;</td>
                            <td style="width: 100px">Status</td>
                            <td>:</td>
                            <td colspan="6">
                                   <asp:Label ID="lblStat" runat="server" ForeColor="#003399"></asp:Label>
                                         </td>
                            
                        </tr>
                        <tr>
                            <td >Tarikh Mohon</td>
                            <td>:</td>
                            <td style="width: 502px">
                                <asp:Label ID="lblTkhMohon" runat="server" ForeColor="#003399"></asp:Label>
                                <td>&nbsp;</td>
                            <td >&nbsp;</td>
                            <td>&nbsp;</td>
                            <td colspan="6">
                                         &nbsp;</td>
                            
                        </tr>
                        <tr>
                            <td >PTJ</td>
                            <td>:</td>
                            <td colspan="10">
                                         <asp:Label ID="lblPtj" runat="server" ForeColor="#003399"></asp:Label>
                            </td>
                            
                        </tr>
                        <tr>
                            <td >Bank</td>
                            <td >:</td>
                            <td style="width: 502px">
                                         <asp:Label ID="lblBank" runat="server" ForeColor="#003399"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td colspan="6">
                                   &nbsp;</td>
                            
                        </tr>
                        <tr>
                            <td >Kategori</td>
                            <td>:</td>
                            <td style="width: 502px">
                                       <asp:Label ID="lblKat" runat="server" ForeColor="#003399"></asp:Label>                                                                   
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
                            <td >Nama Penerima</td>
                            <td>:</td>
                            <td style="width: 502px">
                                <asp:Label ID="lblNmPen" runat="server" ForeColor="#003399"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                            <td >&nbsp;</td>
                            <td>&nbsp;</td>
                            <td colspan="6">
                                   <asp:label ID="lblAlmt2" runat="server" ForeColor="#003399"></asp:label>
                            </td>
                            
                        </tr>
                        <tr>
                            <td >ID / No.KP Penerima</td>
                            <td>:</td>
                            <td style="width: 502px">
                                         <asp:Label ID="lblIdPen" runat="server" ForeColor="#003399"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                            <td >Negeri</td>
                            <td>:</td>
                            <td colspan="6">
                                <asp:label ID="lblNegeri" runat="server" ForeColor="#003399"></asp:label>
                            </td>
                           
                        </tr>
                        <tr>
                            <td >Tujuan</td>
                            <td>:</td>
                            <td style="width: 502px">
                                         <asp:Label ID="lblTujuan" runat="server" ForeColor="#003399"></asp:Label>
                                                                    
                            </td>
                            <td>&nbsp;</td>
                            <td >Negara</td>
                            <td>:</td>
                            <td colspan="6">
                                   <asp:label ID="lblNegara" runat="server" ForeColor="#003399"></asp:label>

                            </td>
                            
                        </tr>
                        <tr>
                            <td >Untuk Perhatian</td>
                            <td>:</td>
                            <td style="width: 502px">
                                         <asp:Label ID="lblUP" runat="server" ForeColor="#003399"></asp:Label>
                                                                    
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
                            <td >No. Rujukan</td>
                            <td>:</td>
                            <td style="width: 502px">
                                         <asp:Label ID="lblNoRuj" runat="server" ForeColor="#003399"></asp:Label>
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
                            <td >Berkontrak</td>
                            <td>:</td>

                          <td>
                                    <asp:Label ID="lblJenKontrak" runat="server" ForeColor="#003399"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                            <td >Emel</td>
                            <td>:</td>
                            <td colspan="6">
                                   <asp:label ID="lblEmel" runat="server" ForeColor="#003399"></asp:label>

                            </td>
                            


                        </tr>


                        <tr>
                            <td>Tarikh Mula Kontrak</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblTkhMulaKon" runat="server" ForeColor="#003399"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td colspan="6">&nbsp;</td>
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
                                </td>
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
            <div class="panel panel-default">
                    <div class="panel-heading">Transaksi</div>
                                <div class="table-responsive">
                    <div class="panel-body">
					
                        <asp:GridView ID="gvTrans" runat="server" AllowSorting="true" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText=" Tiada rekod"
                                            CssClass="table table-striped table-bordered table-hover" Font-Size="8pt" BorderStyle="Solid" ShowFooter="True" Width="100%">
                    <Columns>
                    <asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%"  ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>

                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="2%" />
                    </asp:TemplateField>
                          <asp:TemplateField HeaderText="KW">
                    <ItemTemplate>
                        <asp:Label ID="lblkodKW" runat="server" text='<%# Eval("KodKW")%>' />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="50px" />                   
                </asp:TemplateField>

                <asp:TemplateField HeaderText="KO">
                    <ItemTemplate>
                        <asp:Label ID="lblkodKO" runat="server" text='<%# Eval("KodKO")%>' />
                    </ItemTemplate>  
                    <ItemStyle HorizontalAlign="Center" Width="50px" />                  
                </asp:TemplateField>

               <asp:TemplateField HeaderText="PTj">
                    <ItemTemplate>
                        <asp:Label ID="lblkodPTj" runat="server" text='<%# Eval("kodPTJ")%>' />
                    </ItemTemplate>     
                    <ItemStyle HorizontalAlign="Center" Width="50px" />               
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="KP">
                    <ItemTemplate>
                        <asp:Label ID="lblkodKP" runat="server" text='<%# Eval("kodKP")%>' />
                    </ItemTemplate>  
                    <ItemStyle HorizontalAlign="Center" Width="50px" />                  
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Vot">
                    <ItemTemplate>
                        <asp:Label ID="lblkodVot" runat="server" text='<%# Eval("kodVot")%>' />
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
                    <ItemStyle HorizontalAlign="Right" Width="70px" />
                </asp:TemplateField>

                        <asp:TemplateField HeaderText="Harga (RM)">
                    <ItemTemplate>
                        <asp:Label ID="lblHarga" runat="server" text='<%# Eval("AR01_kadarHarga", "{0:###,###,###.00}")%>' />                    
                    </ItemTemplate>
                      <FooterTemplate>
                           <div style="text-align:right;font-weight:bold;">
                                 <asp:Label runat="server" Text="Jumlah Besar (RM)" />
                           </div>
                        </FooterTemplate>
                    <ItemStyle HorizontalAlign="Right" Width="150px" />
                </asp:TemplateField>

               <asp:TemplateField HeaderText="Jumlah (RM)">
                    <ItemTemplate>                       
                          <asp:Label ID="lblJumlah" runat="server" ForeColor="#003399" Text='<%#Eval("AR01_Jumlah", "{0:###,###,###.00}")%>' />
                    </ItemTemplate>   
                    <FooterTemplate>
                        <div style="text-align:right;">
                            <asp:Label ID="lblTotJum" runat="server" ClientIDMode="Static" CssClass="cssTotJum" ForeColor="#003399" Font-Bold="true"/>
                        </div>
                    </FooterTemplate> 
                    <ItemStyle HorizontalAlign="Right" Width="150px"/>                     
                </asp:TemplateField>                                             
                                </Columns>
                            <EmptyDataTemplate>

                            </EmptyDataTemplate>
                        <selectedrowstyle ForeColor="Blue" />
                                        </asp:GridView>

                        </div> 
                                    </div> 
                              </div>
        </div>

        <div class="row">
            <div class="panel panel-default" style="width: 700px;">
              <div class="panel-heading"><i class="fas fa-paperclip"></i> &nbsp;&nbsp;Lampiran</div>
    <div class="panel-body">
        <asp:GridView ID="gvLamp" runat="server" AutoGenerateColumns="false" EmptyDataText="Tiada fail yang dimuat naik" Width="80%" CssClass="table table-striped table-bordered table-hover"
                                            HeaderStyle-BackColor="#6699FF" Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" DataKeyNames="AR11_ID" ShowHeaderWhenEmpty="true">
                                            <Columns>
                                                <asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" >
												<ItemTemplate>
													<%# Container.DataItemIndex + 1 %>
												</ItemTemplate>
											        <ItemStyle Width="2%" />
											    </asp:TemplateField> 
 
                                                <asp:TemplateField Visible ="false">
                                                    <ItemTemplate>                        
                                                        <asp:Label ID="lblIdBil" runat="server" text='<%# Eval("AR01_IdBil")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField Visible ="false">
                                                    <ItemTemplate>                        
                                                        <asp:Label ID="lblId" runat="server" text='<%# Eval("AR11_ID")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Nama Fail">
                                                    <ItemTemplate>                        
                                                        <asp:Label ID="lblNamaDok" runat="server" text='<%# Eval("AR11_NamaDok")%>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                </asp:TemplateField>
                                               
                                                <asp:TemplateField>
                                                <ItemTemplate>
                                                <asp:LinkButton ID="lbtnSelect" runat="server" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CommandName="Select" CssClass="btn-xs" ToolTip="Lihat Lampiran">                                                    
                                                    <i class="fab fa-readme fa-lg"></i>
                                                    </asp:LinkButton>                                                                       
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="10px" />
                                                </asp:TemplateField>

                                                
                                            </Columns>
                                          
<HeaderStyle BackColor="#6699FF"></HeaderStyle>
                                          
                                        </asp:GridView>


        </div>
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
