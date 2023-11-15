<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Maklumat_Agihan.aspx.vb" Inherits="SMKB_Web_Portal.Maklumat_Agihan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>MAKLUMAT AGIHAN</h1>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row">
                <asp:LinkButton ID="lnkBtnBack" runat="server" CssClass="btn " Width="100px" ToolTip="">
						<i class="fas fa-long-arrow-alt-left  fa-lg"></i> &nbsp;&nbsp; Kembali
                </asp:LinkButton>
            </div>

            <div class="panel-body well" style="width:800px;padding:5px;">
                  <div class="row">
             <table style="width: 100%;">
                     
                  <tr>
                      <td style="width: 125px;">
                          
                          Bajet tahun</td>
                      <td style="height: 25px;">:</td>
                      <td>
                          <asp:textbox id="txtTahun" runat="server" backcolor="#ffffcc" class="form-control" readonly="true" width="100px"></asp:textbox>

                      </td>
                  </tr>
                  <tr>
                      <td>
                          
                          Kumpulan Wang</td>
                      <td style="height: 25px;">:</td>
                      <td>                          
                          <asp:TextBox ID="txtKW" runat="server" BackColor="#FFFFCC" class="form-control" ReadOnly="true" Width="500px"></asp:TextBox>
                                                    
                          <asp:HiddenField ID="hidKodKW" runat="server" />
                                                    
                      </td>
                  </tr>
                  <tr>
                      <td>PTj</td>
                      <td style="height: 25px;">:</td>
                      <td>
                          <asp:TextBox ID="txtPTj" runat="server" BackColor="#FFFFCC" class="form-control" ReadOnly="true" Width="500px"></asp:TextBox>
                          <asp:HiddenField ID="hidKodPTj" runat="server" />
                      </td>
                  </tr>
                  <tr>
                      <td>Kod Operasi</td>
                      <td style="height: 25px;">&nbsp;</td>
                      <td>
                          <asp:DropDownList ID="ddlKO" runat="server" AutoPostBack="True" CssClass="form-control" Width="50%">
                          </asp:DropDownList>
                      </td>
                  </tr>
          </table>
             </div>

                    
                    </div>
       
            <div class="panel panel-default" style="width:750px">
                <div class="panel-heading">Vot Am</div>
                <div class="panel-body">
                            <asp:GridView ID="gvObjAm" runat="server" AllowPaging="true" PageSize="25" AllowSorting="true" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText=" "
                                CssClass="table table-striped table-bordered table-hover" Width="100%" Height="100%" HeaderStyle-BackColor="#6699FF" Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" ShowFooter="True">
                                <Columns>
                                    <asp:TemplateField HeaderText="Vot Am">
                                        <ItemTemplate>
                                            <asp:Label ID="lblObjAm" runat="server" Text='<%#Eval("KodVot")%>' />
                                            &nbsp; - &nbsp;
                                    <asp:Label ID="lblButVot" runat="server" Text='<%#Eval("ButVot")%>' />
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Jumlah (RM)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblJumBajet" runat="server" ForeColor="#003399" Text='<%#Eval("JumBajet", "{0:N2}")%>'>  
                                            </asp:Label>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <div style="text-align: right;">
                                                <asp:Label ID="lblTotJum" runat="server" ForeColor="#003399" Font-Bold="true" />
                                            </div>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" Width="25%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="btn-xs" ToolTip="Kemas kini">
                                          <i class="fas fa-edit"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="40px" />
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                </div>
            </div>
            
           <div class="panel panel-default" style="width:80%;">
                <div class="panel-heading">Vot Sebagai</div>
                <div class="panel-body">
                    <table style="width: 100%; text-align: left;">
                        <tr style="height: 35px;">
                            <td>Objek Am</td>
                            <td>:</td>
                            <td colspan="2">
                                <asp:TextBox ID="txtObjAm" runat="server" BackColor="#FFFFCC" class="form-control" ReadOnly="True" Style="text-align: left " Width="500px"></asp:TextBox>
                  
                                <asp:HiddenField ID="hidObjAm" runat="server" />
                  
                            </td>
                        </tr>
                        <tr style="height: 35px;">
                            <td>Kod Projek</td>
                            <td>:</td>
                            <td colspan="2">
                               <asp:DropDownList ID="ddlKP" runat="server" AutoPostBack="True" CssClass="form-control" Width="50%">
                      </asp:DropDownList>
                  
                            </td>
                        </tr>
                        <tr style="height: 35px;">
                            <td>Agihan 2021 (RM)</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtAgihan0" runat="server" class="form-control"  Style="text-align: right" Width="200px"></asp:TextBox>
                                </td>
                            <td style="text-align: left;">
                                &nbsp;</td>
                        </tr>
                    </table>
                     <div style="margin-top :20px;">
                    <asp:GridView ID="gvObjSebagai" runat="server" AllowSorting="True" AutoGenerateColumns="False" BorderColor="#333333" BorderStyle="Solid" CssClass="table table-striped table-bordered table-hover" EmptyDataText=" " Font-Size="8pt" HeaderStyle-BackColor="#6699FF" ShowFooter="True" ShowHeaderWhenEmpty="True" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Bil">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBil" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />
                                            </ItemTemplate>
                                            <ItemStyle Width="3%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vot Sebagai">
                                            <ItemTemplate>
                                                <asp:Label ID="lblKodVot" runat="server" Text='<%# Eval("KodVot") %>' />
                                                &nbsp;-&nbsp;
                                                <asp:Label ID="lblButVot" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("ButVot"))) %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Agihan 2020 (RM)">
                                            <ItemTemplate>                                     
                                                <asp:Label ID="lblAmtAgihPast" runat="server" Text='<%# Eval("AmtAgihPast", "{0:N2}") %>' />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div style="text-align: right;">
                                                    <asp:Label ID="lblTotAmtAgihPast" runat="server"  />
                                                </div>
                                            </FooterTemplate>
                                             <ItemStyle HorizontalAlign="Right"/>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Belanja 2020 (RM)">
                                            <ItemTemplate>
                                      
                                                <asp:Label ID="lblAmtBelanjaPast" runat="server" Text='<%# Eval("AmtBelanjaPast", "{0:N2}") %>' />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div style="text-align: right;">
                                                    <asp:Label ID="lblTotAmtBelanjaPast" runat="server"  />
                                                </div>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right"/>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Mohon 2021 (RM)">
                                            <ItemTemplate>
                                      
                                                <asp:Label ID="lblAmtMohon" runat="server" Text='<%# Eval("AmtMohon", "{0:N2}") %>' />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div style="text-align: right;">
                                                    <asp:Label ID="lblTotAmtMohon" runat="server"  />
                                                </div>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right"/>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cadangan 2021 (RM)">
                                            <ItemTemplate>                                      
                                                <asp:Label ID="lblAmtCad" runat="server" Text='<%# Eval("AmtCad", "{0:N2}") %>' />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div style="text-align: right;">
                                                    <asp:Label ID="lblTotAmtCad" runat="server"  />
                                                </div>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right"/>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Agihan 2021 (RM)">
                                            <ItemTemplate>
                                      
                                                <asp:TextBox ID="txtAgih" runat="server" onkeypress="return isNumberKey(event,this)" Text='<%#Eval("AmtAgih", "{0:N2}")%>' Width="100%"  onblur="if (this.dirty){this.onchange();}"  AutoPostBack="true" CssClass="form-control rightAlign"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div style="text-align: right;">
                                                    <asp:Label ID="lblTotAgih" runat="server"  />
                                                </div>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" BackColor="#e8a196"/>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="btn-xs" ToolTip="Kemas kini">
                                          <i class="fas fa-edit"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="40px" />
                                    </asp:TemplateField>

                                    </Columns>
                                    <HeaderStyle BackColor="#6699FF" />
                                </asp:GridView>
                         </div>

                    <div style="text-align: center; margin-bottom: 10px;">
                        <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                        </asp:LinkButton>
                    </div>
                </div>
            </div>                
                   
    

            <div class="panel panel-default" style="width:95%;">
                <div class="panel-heading">Butiran Permohonan</div>
                <div class="panel-body">
                    <table style="width: 100%; text-align: left;">
                        <tr style="height: 35px;">
                            <td>Kod Operasi</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtKo2" runat="server" BackColor="#FFFFCC" class="form-control" ReadOnly="True" Style="text-align: left " Width="500px"></asp:TextBox>
                  
                            </td>
                        </tr>
                        <tr style="height: 35px;">
                            <td>Vot Sebagai</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtVotSbg" runat="server" BackColor="#FFFFCC" class="form-control" ReadOnly="True" Style="text-align: left " Width="500px"></asp:TextBox>
                                <asp:HiddenField ID="hidVotSbg" runat="server" />
                            </td>
                        </tr>
                    </table>

                    <asp:GridView ID="gvButiran" runat="server" AllowSorting="True" AutoGenerateColumns="False" BorderColor="#333333" BorderStyle="Solid" CssClass="table table-striped table-bordered table-hover" EmptyDataText=" " Font-Size="8pt" HeaderStyle-BackColor="#6699FF" ShowFooter="True" ShowHeaderWhenEmpty="True" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Bil">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBil" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />
                                            </ItemTemplate>
                                            <ItemStyle Width="3%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Program">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProgram" runat="server" Text='<%# Eval("Program") %>' />                                                
                                            </ItemTemplate>
     
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Butiran">
                                            <ItemTemplate>
                                                <asp:Label ID="lblButiran" runat="server" Text='<%# Eval("Butiran") %>' />                                                
                                            </ItemTemplate>
     
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Mohon 2021 (RM)">
                                            <ItemTemplate>                                      
                                                <asp:Label ID="lblAmtMohon" runat="server" Text='<%# Eval("AmtMohon") %>' /> 
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div style="text-align: right;">
                                                    <asp:Label ID="lblTotAmtMohon" runat="server"  />
                                                </div>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="10%"/>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cadangan 2021 (RM)">
                                            <ItemTemplate>                                      
                                                <asp:Label ID="lblAmtCad" runat="server" Text='<%# Eval("AmtCad") %>' /> 
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div style="text-align: right;">
                                                    <asp:Label ID="lblTotAmtCad" runat="server"  />
                                                </div>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="10%"/>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Agihan 2021 (RM)">
                                            <ItemTemplate>                                      
                                                <asp:TextBox ID="txtAmtAgihan" runat="server" onkeypress="return isNumberKey(event,this)" Text='<%#Eval("AmtAgihan", "{0:N2}")%>' Width="100%"  onblur="if (this.dirty){this.onchange();}"  AutoPostBack="true" CssClass="form-control rightAlign"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div style="text-align: right;">
                                                    <asp:Label ID="lblTotAmtAgihan" runat="server"  />
                                                </div>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="10%"/>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Ulasan">
                                            <ItemTemplate>                                    
                                                <asp:TextBox ID="txtUlasan" runat="server" Text='<%#Eval("Ulasan")%>' Width="100%" CssClass="form-control" TextMode="MultiLine" Height="50px"></asp:TextBox>
                                            </ItemTemplate> 
                                            <ItemStyle Width="30%" />                                      
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="#6699FF" />
                                </asp:GridView>               
                </div>
            </div>
            </ContentTemplate> 
         </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div style="background-color: #D2D2D2; filter: alpha(opacity=80); opacity: 0.80; width: 100%; top: 0px; left: 0px; position: fixed; height: 100%;">
            </div>
            <div style="margin: auto; font-family: Trebuchet MS; filter: alpha(opacity=100); opacity: 1; font-size: small; vertical-align: middle; top: auto; position: absolute; left: auto; color: #FFFFFF; position: fixed; top: 50%; left: 50%; margin-top: -50px; margin-left: -100px;">
                <table>
                    <tr>
                        <td style="text-align: center;">
                            <img src="../../../Images/loader.gif" alt="" />
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress> 

</asp:Content>
