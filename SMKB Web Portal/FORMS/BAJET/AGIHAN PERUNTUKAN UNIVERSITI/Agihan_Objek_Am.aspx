<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Agihan_Objek_Am.aspx.vb" Inherits="SMKB_Web_Portal.Agihan_Objek_Am" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="panel-body well" style="width:70%;">
                  <div class="row">  
                    <table style="width: 100%;">
                      <tr>
                            <td style="width: 70px">
                                <label class="control-label" for="">Tahun :</label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlTahunAgih" runat="server" AutoPostBack="True" CssClass="form-control" style="width: 100px; height: 21px;"/>
                              
                            <%-- <asp:TextBox ID="txtTahun" runat="server" ReadOnly="True" CssClass="form-control"  style="width: 100px;" BackColor="#FFFFCC"></asp:TextBox>--%>                            
                            </td>
                        </tr>
                 
                      <tr>
                      <td style="width: 100px; height: 25px;">
                          Kumpulan Wang :</td>
                      <td>
                          <asp:DropDownList ID="ddlKW" runat="server" AutoPostBack="True" CssClass="form-control" style="width: 400px;">
                          </asp:DropDownList>
                           &nbsp;&nbsp;
                          <label id="lblMsgKW" class="control-label" for="" style="display:none;color:#820303;">
                          (Pilih KW)
                          </label>
                      </td>
                        
                  </tr>
                        <tr>
                            <td style="width: 100px; height: 25px;">
                                <label class="control-label" for="">
                                PTj :</label> </td>
                            <td>
                                <asp:DropDownList ID="ddlPTj" runat="server" AutoPostBack="True" CssClass="form-control" style="width: 600px;">
                                </asp:DropDownList>
                                 &nbsp;&nbsp;
                                <label id="lblMsgPTJ" class="control-label" for="" style="display:none;color:#820303;">
                                (Pilih PTJ)
                                </label>
                            </td>
                        </tr>
                  </table>
                      </div>

                    <div class="row">
                    <table>
                        <tr>
                            <td style="width: 100px; height: 22px;">
                                <label class="control-label" for="">Bajet (RM) : </label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtJumBajetPTj" runat="server" ReadOnly="True"  CssClass="form-control rightAlign" style="width: 200px;" BackColor="#FFFFCC"></asp:TextBox>
                            </td>
                            <td style="width: 100px; height: 22px;text-align:right;">
                                <label class="control-label" for="">Agihan (RM) : </label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtJumAgihPTj" runat="server" ReadOnly="True" CssClass="form-control rightAlign" style="width: 200px;" BackColor="#FFFFCC"></asp:TextBox>                            
                                <asp:HiddenField ID="hidAgihPTj" runat="server" />
                            </td>
                            <td style="width: 100px; height: 22px;text-align:right;">
                                <label class="control-label" for="">Baki (RM) : </label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtJumBakiPTj" runat="server" ReadOnly="True" CssClass="form-control rightAlign" style="width: 200px;" BackColor="#FFFFCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px; height: 22px;">                          
                                &nbsp;</td>
                            <td>&nbsp;</td>
                            <td style="width: 100px; height: 22px;text-align:right;">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td style="width: 100px; height: 22px;text-align:right;">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                        </div>
                    </div>

            <div class="panel panel-default" style="width:95%">
              <div class="panel-heading">Agihan Peruntukan PTj</div>
    <div class="panel-body">
        <div class="row">
        <table style="width: 100%;">
                         <tr>
                             <td style="width: 100px;height: 25px;">
                                 <label class="control-label" for="">
                                 Kod Operasi :</label></td>
                             <td>
                                 <asp:DropDownList ID="ddlKO" runat="server" AutoPostBack="True" CssClass="form-control" style="width: 400px;">
                                 </asp:DropDownList>
                                  &nbsp;&nbsp;
                                 <label id="lblMsgKO" class="control-label" for="" style="display:none;color:#820303;">
                                 (Pilih KO)
                                 </label>
                             </td>
                      </tr>
                         <tr>
                             <td style="width: 100px;height: 25px;">
                                 <label class="control-label" for="">
                                 Kod Projek :</label></td>
                             <td>
                                 <asp:DropDownList ID="ddlKp" runat="server" AutoPostBack="True" CssClass="form-control" style="width: 400px;">
                                 </asp:DropDownList>
                                 <label id="lblMsgKP" class="control-label" for="" style="display:none;color:#820303;">
                                 (Pilih KP)
                                 </label>
                             </td>
                      </tr>
                         <tr>
                             <td >
                                  </td>
                             <td>
                                 &nbsp;</td>
                      </tr>
                         <table style="width:100%;">
                        <tr>
                            <td style="width: 100px;height: 22px;">
                                <label class="control-label" for="">Bajet (RM) :</label>
                            </td>
                            <td>
                               <%-- <asp:TextBox ID="txtJumBajetKp" runat="server" CssClass="form-control rightAlign" style="width: 150px;" onkeypress="return isNumberKey(event,this)" onkeyup="fCalc(this);"></asp:TextBox--%>                              
                                <asp:TextBox ID="txtJumBajetKp" runat="server" CssClass="form-control rightAlign" style="width: 200px;" onkeypress="return isNumberKey(event,this)" onblur="if (this.dirty){this.onchange();}" AutoPostBack="true"></asp:TextBox>
                                <asp:HiddenField ID="hidBajetKp" runat="server" />

                            </td>
                            <td style="width: 150px; ">
                                <label class="control-label" for="">Tambahan (RM) :</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTBKp" runat="server" BackColor="#FFFFCC" CssClass="form-control rightAlign" ReadOnly="True" style="width: 200px;"></asp:TextBox>
                            </td>
                            <td>Kurangan (RM) :
                                </td>
                            <td>
                                <asp:TextBox ID="txtKGKp" runat="server" BackColor="#FFFFCC" CssClass="form-control rightAlign" ReadOnly="True" style="width: 200px;"></asp:TextBox>
                            </td>
                            <td style="width: 100px;">
                                <label class="control-label" for="">Baki BF :</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtBFKp" runat="server" ReadOnly="True" CssClass="form-control rightAlign" style="width: 200px;" BackColor="#FFFFCC"></asp:TextBox>
                            </td>
                        </tr>                          
                                     <tr>
                                         <td style="height: 10px;">
                                             &nbsp;</td>
                                         <td>
                                             &nbsp;</td>
                                         <td>
                                             </td>
                                         <td>
                                             &nbsp;</td>
                                         <td>
                                             &nbsp;</td>
                                         <td>
                                             &nbsp;</td>
                                     </tr>
                                                   
                             <tr>
                                 <td style="height: 25px;">
                                     <label class="control-label" for="">
                                     Jumlah (RM) :</label> </td>
                                 <td>
                                     <asp:TextBox ID="txtJumKp" runat="server" BackColor="#FFFFCC" CssClass="form-control rightAlign" ReadOnly="True" style="width: 200px;"></asp:TextBox>
                                 </td>
                                 <td>
                                     <label class="control-label" for="">
                                     Agihan (RM) :</label> </td>
                                 <td>
                                     <asp:TextBox ID="txtAgihanKp" runat="server" BackColor="#FFFFCC" CssClass="form-control rightAlign" ReadOnly="True" style="width: 200px;"></asp:TextBox>
                                 </td>
                                 <td>
                                     <label class="control-label" for="">
                                     Baki (RM) :</label> </td>
                                 <td>
                                     <asp:TextBox ID="txtBakiKp" runat="server" BackColor="#FFFFCC" CssClass="form-control rightAlign" ReadOnly="True" style="width: 200px;"></asp:TextBox>
                                 </td>
                             </tr>
                                                   
                    </table>
               
                  </table>
            </div>
        </div>


                <div class="panel panel-default" style="width:95%;">
              <div class="panel-heading">Senarai Agihan Peruntukan Objek Am</div>
    <div class="panel-body">
        <div class="GvTopPanel">
                <div style="float:left;margin-top: 8px;margin-left: 10px;">
                <label class="control-label" for="">Jumlah rekod :</label>&nbsp;<label class="control-label" id ="lblJumRekod" runat="server" style="color:mediumblue;" ></label>
                    </div>
            </div>
        <asp:GridView ID="gvObjAm" runat="server" AutoGenerateColumns="False" BorderColor="#333333" BorderStyle="Solid" 
            cssclass="table table-striped table-bordered table-hover" EmptyDataText=" " Font-Size="8pt" 
            HeaderStyle-BackColor="#6699FF" Height="100%" ShowFooter="True" ShowHeaderWhenEmpty="True" 
            Width="100%">
            <columns>
                <asp:TemplateField HeaderText="Bil">
                    <ItemTemplate>
                        <asp:Label ID="lblBil" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />
                    </ItemTemplate>
                    <ItemStyle Width="3%" HorizontalAlign="Right" />
                </asp:TemplateField>
        

                <asp:TemplateField HeaderText="Objek Am"  >  
                        <ItemTemplate><asp:Label ID ="ObjAm" runat="server"  Text='<%#Eval("ObjAm")%>'  /></ItemTemplate>  
  
                        <FooterTemplate>                            
                            <div style="text-align:right;font-weight:bold;"><asp:Label Text="Jumlah Besar (RM)" runat="server" /></div>  
                        </FooterTemplate>    
                    </asp:TemplateField> 

                <asp:TemplateField HeaderText="Bajet (RM)">  
                        <ItemTemplate>                        
                            <asp:TextBox ID="txtBajet"  runat="server" Width="98%" Text='<%#Eval("Bajet")%>' 
                               onkeypress="return isNumberKey(event,this)" OnTextChanged="txtBajet_TextChanged" onblur="if (this.dirty){this.onchange();}" AutoPostBack="true" CssClass="form-control rightAlign"></asp:TextBox> 
                        </ItemTemplate>  
  
                        <FooterTemplate> 
                            <div style="text-align:right;">  
                            <asp:Label ID="lblTotBajet" runat="server" CssClass="cssTotBajet"/>  
                                </div>
                        </FooterTemplate>  
                        <ItemStyle HorizontalAlign="Right" Width="12%" BackColor="#e8a196"/>
                    </asp:TemplateField>

             
                <asp:TemplateField HeaderText="Tambahan (RM)">  
                        <ItemTemplate><asp:Label ID="lblTB" runat="server" Text='<%#Eval("TB")%>'>  
                            </asp:Label></ItemTemplate>  
  
                        <FooterTemplate>  
                            <div style="text-align:right;"> 
                            <asp:Label ID="lblTotTB" runat="server" />  
                                </div>
                        </FooterTemplate>  
                        <ItemStyle HorizontalAlign="Right" Width="12%" />
                    </asp:TemplateField>

              
                <asp:TemplateField HeaderText="Kurangan (RM)">  
                        <ItemTemplate><asp:Label ID="lblKG" runat="server" Text='<%#Eval("KG")%>'>  
                            </asp:Label></ItemTemplate>  
  
                        <FooterTemplate> 
                            <div style="text-align:right;">  
                            <asp:Label ID="lblTotKG" runat="server" />  
                                </div>
                        </FooterTemplate>  
                        <ItemStyle HorizontalAlign="Right" Width="12%" />
                    </asp:TemplateField>

              
                <asp:TemplateField HeaderText="Baki BF (RM)">  
                        <ItemTemplate><asp:Label ID="lblBakiBF" runat="server" Text='<%#Eval("BakiBF")%>'>  
                            </asp:Label></ItemTemplate>  
  
                        <FooterTemplate>  
                            <div style="text-align:right;"> 
                            <asp:Label ID="lblTotBakiBF" runat="server" /> 
                                </div> 
                        </FooterTemplate>  
                        <ItemStyle HorizontalAlign="Right" Width="12%" />
                    </asp:TemplateField>

               
                <asp:TemplateField HeaderText="Jumlah (RM)">  
                        <ItemTemplate>
                            <asp:Label ID="lblJum" runat="server" Text='<%#Eval("Jumlah")%>' ForeColor="#003399">  
                            </asp:Label></ItemTemplate>  
  
                        <FooterTemplate> 
                            <div style="text-align:right;"> 
                            <asp:Label ID="lblTotJum" runat="server" CssClass="cssTotBajet2" ForeColor="#003399" Font-Bold="true"/>  
                                </div>
                        </FooterTemplate>  
                        <ItemStyle HorizontalAlign="Right" Width="12%" />
                    </asp:TemplateField>
               
            </columns>
            <HeaderStyle BackColor="#6699FF" />
        </asp:GridView>
        
            
                    <div style="text-align:center;margin-bottom:10px;">                           
                        <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn ">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
					</asp:LinkButton>
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
