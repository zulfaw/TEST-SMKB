<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Agihan_Objek_Sebagai.aspx.vb" Inherits="SMKB_Web_Portal.Agihan_Objek_Sebagai" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="panel-body well" style="width:70%;">
                  <div class="row">
             <table style="width: 100%;">
                     
                  <tr>
                      <td style="height: 25px;">
                          
                          Tahun</td>
                      <td style="height: 25px;">:</td>
                      <td>
                          <%--<asp:TextBox ID="txtTahun" runat="server" BackColor="#FFFFCC" class="form-control" ReadOnly="True" Width="10%"></asp:TextBox>--%>
                          <asp:DropDownList ID="ddlTahunAgih" runat="server" AutoPostBack="True" CssClass="form-control" style="width: 100px; height: 21px;"/>
                      </td>
                  </tr>
                  <tr>
                      <td style="height: 25px;">
                          
                          PTj</td>
                      <td style="height: 25px;">:</td>
                      <td>                          
                          <asp:DropDownList ID="ddlPTJ" runat="server" AutoPostBack="True" CssClass="form-control" style="width: 600px; height: 21px;">
                          </asp:DropDownList>
                          
                      </td>
                  </tr>
                  <tr>
                      <td style="height: 25px;">
                         
                          Kumpulan Wang </td>
                      <td style="height: 25px;">:</td>
                      <td>
                          <label id="lblMsgKO" class="control-label" for="" style="display:none;color:#820303;">
                          (Pilih Kod Operasi)
                          </label>
                          <asp:DropDownList ID="ddlKW" runat="server" AutoPostBack="True" CssClass="form-control" Width="60%">
                          </asp:DropDownList>
                      </td>
                  </tr>
          </table>
             </div>

                    
                    </div>




         
          <div class="panel panel-default">
              <div class="panel-heading">Peruntukan Objek Am</div>
    <div class="panel-body">
             
                              
                  

        <div class="row">
        <div style=" margin-top:10px;">
            <%--<div class="alert alert-danger" id="divMsg" visible ="false" runat="server">
                <strong><i class="fas fa-exclamation-circle fa-lg"></i></strong> &nbsp;&nbsp;<label class="control-label" id="lblmsg" runat="server"/>
            </div>--%>

            <div class="GvTopPanel" style="height:33px;">
                <div style="float:left;margin-top: 5px;margin-left: 10px;">
                <label class="control-label" for="">Jumlah rekod :</label>&nbsp;<label class="control-label" id ="lblJumRekod" runat="server" style="color:mediumblue;" ></label>
                 &nbsp;&nbsp;   <b style="color:#969696;">|</b> &nbsp;&nbsp;

                    <label class="control-label" for="Klasifikasi"> Saiz Rekod :</label>
        <asp:DropDownList ID="ddlSaizRekod" runat="server" Width="50px" AutoPostBack="True" class="form-control">
                <asp:ListItem Text="10" Value="10" />
                <asp:ListItem Text="25" Value="25" Selected="True" />
                <asp:ListItem Text="50" Value="50" />
                <asp:ListItem Text="100" Value="100" />
            </asp:DropDownList>
                    
                   &nbsp;&nbsp;<b style="color:#969696;">|</b> &nbsp;&nbsp;                 
                     <button runat="server" id="btnFilter" title="Tapisan" class="btnNone" style="margin-top:-4px;"  >
    <i class="fas fa-filter"></i>
</button>                                                                      
                </div>  

         <div class="panel-title pull-right" style="margin-top: 5px;margin-right: 5px;">
             <i class="fas fa-info-circle fa-lg" aria-hidden="true" data-html="true" data-toggle="tooltip" data-placement="top" style="cursor:pointer;color:#ba2818;" 
                 title="Petunjuk : <br/><span style='color:#008000;'><i class='far fa-check-circle fa-lg'></i> </span>Selesai diagih</span>   <br/><span style='color:#969696;'><i class='far fa-check-circle fa-lg'></i> </span>Belum selesai diagih</span>">
             </i></div>
            </div>
        <asp:GridView ID="gvObjAm" runat="server" AllowPaging="true" PageSize ="25" AllowSorting="true" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText=" " 
       cssclass="table table-striped table-bordered table-hover" Width="100%" Height="100%" HeaderStyle-BackColor="#6699FF" Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" ShowFooter="True"  >
                                 <columns>                                  
                                     <asp:TemplateField HeaderText="">
<ItemTemplate> 
  <asp:LinkButton ID="btnAgih1" runat="server">
      <a href="#" data-toggle="tooltip" title="Selesai diagih" style="cursor:default;">
          <span style="color:#008000;"><i class="fas fa-check-circle fa-lg"></i></span></a>
  </asp:LinkButton>
    <asp:LinkButton ID="btnAgih2" runat="server">
      <a href="#" data-toggle="tooltip" title="Belum selesai diagih" style="cursor:default;"> 
          <span style="color:#969696;"><i class="far fa-check-circle fa-lg"></i></span></a>
  </asp:LinkButton>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText = "Bil">
        <ItemTemplate>
            <asp:Label ID="lblBil" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
        </ItemTemplate>
                                      <ItemStyle Width="3%" />
    </asp:TemplateField>
                                    
                                    <asp:BoundField DataField="KW" HeaderText="KW" ReadOnly="True">
                                        <ItemStyle Width="3%" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="kodKO" HeaderText="KO" SortExpression="kodKO">
                                     <ItemStyle Width="3%" />
                                     </asp:BoundField>                                   
                                    <asp:BoundField HeaderText="PTJ" DataField="PTJ" ReadOnly="true">
                                         <ItemStyle Width="5%" />
                                    </asp:BoundField>
                                      <asp:BoundField DataField="KodKP" HeaderText="KP" SortExpression="KodKP">
                                     <ItemStyle Width="3%" />
                                     </asp:BoundField>
                                     <asp:BoundField HeaderText="Objek AM" DataField="ObjAm" ReadOnly="true">
                                         <ItemStyle Width="20%" />
                                    </asp:BoundField>
                                     <asp:BoundField HeaderText="Bajet (RM)" DataField="Bajet" ReadOnly="true" ItemStyle-HorizontalAlign ="Right" >
                                         <ItemStyle Width="12%" />
                                    </asp:BoundField>
                                     <asp:BoundField HeaderText="Tambahan (RM)" DataField="Tambahan" ReadOnly="true" ItemStyle-HorizontalAlign ="Right">
                                         <ItemStyle Width="11%" />
                                    </asp:BoundField>
                                     <asp:BoundField HeaderText="Kurangan (RM)" DataField="Kurangan" ReadOnly="true" ItemStyle-HorizontalAlign ="Right">
                                         <ItemStyle Width="11%" />
                                    </asp:BoundField>
                                     <asp:BoundField HeaderText="Baki BF (RM)" DataField="BakiBF" ReadOnly="true" ItemStyle-HorizontalAlign ="Right">
                                         <ItemStyle Width="11%" />
                                    </asp:BoundField>
                                     <asp:BoundField HeaderText="Jumlah (RM)" DataField="Jumlah" ReadOnly="true" ItemStyle-HorizontalAlign ="Right">
                                         <ItemStyle Width="11%" />                           
                                    </asp:BoundField>

                                    
                                  <asp:TemplateField>
                                  <EditItemTemplate>
                                      <asp:ImageButton ID="btnUpdate" runat="server" CommandName="Update" Height="25px" ImageUrl="~/Images/Save_48x48.png" OnItemCommand="gvSubMenu_ItemCommand" ToolTip="Simpan" Width="20px" />
                                      &nbsp;&nbsp;
                                      <asp:ImageButton ID="btnCancel" runat="server" CommandName="Cancel" Height="25px" ImageUrl="~/Images/Cancel16x16.png" OnItemCommand="gvSubMenu_ItemCommand" ToolTip="Batal" Width="20px" />
                                      &nbsp;&nbsp;
                                  </EditItemTemplate>
                                  <ItemTemplate>
                                      <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="btn-xs" ToolTip="Pilih">
                                            <i class="fas fa-edit"></i>
                                            </asp:LinkButton>
                                  </ItemTemplate>
                                  <ItemStyle Width="5%" />
                              </asp:TemplateField>
                                     <asp:TemplateField Visible=false>
                                        <ItemTemplate>
                                            <asp:Label ID="lblIndObjAm" runat ="server" text='<%# Eval("IndObjAm")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </columns>

<HeaderStyle BackColor="#6699FF"></HeaderStyle>
                            </asp:GridView>   
            
            
            
                    
            </div>
        </div>


        </div>
              </div>
            
           <div class="panel panel-default">
                <div class="panel-heading">Agihan Objek Sebagai</div>
                <div class="panel-body">
                    <table style="width: 100%; text-align: left;">
                        <tr style="height: 35px;">
                            <td style="width: 100px">Kumpulan Wang :</td>
                            <td colspan="5">
                                <asp:TextBox ID="txtKodKW" runat="server" BackColor="#FFFFCC" class="form-control" ReadOnly="True" Width="5%"></asp:TextBox>
                                -<asp:TextBox ID="txtKW" runat="server" BackColor="#FFFFCC" class="form-control" ReadOnly="true" Width="500px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height: 35px;">
                            <td>Kod Operasi :</td>
                            <td colspan="5">
                                <asp:TextBox ID="txtKodKO" runat="server" BackColor="#FFFFCC" class="form-control" ReadOnly="True" Width="5%"></asp:TextBox>
                                &nbsp;-&nbsp;<asp:TextBox ID="txtKOButiran" runat="server" BackColor="#FFFFCC" class="form-control" ReadOnly="true" Width="500px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height: 35px;">
                            <td>Kod Projek :</td>
                            <td colspan="5">
                                <asp:TextBox ID="txtKodKP" runat="server" BackColor="#FFFFCC" class="form-control" ReadOnly="True" Width="10%"></asp:TextBox>
                                &nbsp;-
                                        <asp:TextBox ID="txtKodKPButiran" runat="server" BackColor="#FFFFCC" class="form-control" ReadOnly="true" Width="500px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height: 35px;">
                            <td>Objek Am :</td>
                            <td colspan="5">
                                <asp:TextBox ID="txtKodObjAm" runat="server" BackColor="#FFFFCC" class="form-control" ReadOnly="True" Width="70px"></asp:TextBox>
                                &nbsp;-&nbsp;<asp:TextBox ID="txtObjAm" runat="server" BackColor="#FFFFCC" class="form-control" ReadOnly="true" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height: 35px;">
                            <td>Bajet (RM) :</td>
                            <td style="width: 200px;">
                                <asp:TextBox ID="txtBajetAm" runat="server" BackColor="#FFFFCC" class="form-control" ReadOnly="True" Style="text-align: right" Width="200px"></asp:TextBox>
                            </td>
                            <td style="width: 150px;text-align :right;">Agihan (RM) :</td>
                            <td style="text-align: left; width: 200px;">
                                <asp:TextBox ID="txtAgihan" runat="server" BackColor="#FFFFCC" class="form-control" ReadOnly="True" Style="text-align: right" Width="200px"></asp:TextBox>
                            </td>
                            <td style="width: 150px;text-align:right;">Baki (RM) :</td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txtBaki" runat="server" BackColor="#FFFFCC" class="form-control" Style="text-align: right" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                
                            </td>
                        </tr>
                    </table><asp:GridView ID="gvObjSebagai" runat="server" AllowSorting="True" AutoGenerateColumns="False" BorderColor="#333333" BorderStyle="Solid" CssClass="table table-striped table-bordered table-hover" EmptyDataText=" " Font-Size="8pt" HeaderStyle-BackColor="#6699FF" ShowFooter="True" ShowHeaderWhenEmpty="True" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Bil">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBil0" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />
                                            </ItemTemplate>
                                            <ItemStyle Width="3%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Objek Sebagai">
                                            <ItemTemplate>
                                                <asp:Label ID="ObjSebagai" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("ObjSebagai"))) %>' />

                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div style="text-align: right; font-weight: bold;">
                                                    <asp:Label runat="server" Text="Jumlah Besar (RM)" />
                                                </div>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bajet (RM)">
                                            <ItemTemplate>
                                                <%--<asp:TextBox ID="txtBajet" runat="server" onkeypress="return isNumberKey(event,this)" onkeyup="CalcTotal(this);" Text='<%#Eval("Bajet")%>' Width="100%"></asp:TextBox>--%>
                                                <asp:TextBox ID="txtBajet" runat="server" onkeypress="return isNumberKey(event,this)" Text='<%#Eval("Bajet")%>' Width="100%"  onblur="if (this.dirty){this.onchange();}" OnTextChanged="txtBajet_TextChanged" AutoPostBack="true" CssClass="form-control rightAlign"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div style="text-align: right;">
                                                    <asp:Label ID="lblTotBajet" runat="server" ClientIDMode="Static" CssClass="cssTotBajet" />
                                                </div>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" BackColor="#e8a196"/>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Tambahan (RM)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTamb" runat="server" Text='<%#Eval("Tambahan")%>'>  
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div style="text-align: right;">
                                                    <asp:Label ID="lblTotTamb" runat="server" />
                                                </div>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Kurangan (RM)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblKurg" runat="server" Text='<%#Eval("Kurangan")%>'>  
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div style="text-align: right;">
                                                    <asp:Label ID="lblTotKurg" runat="server" />
                                                </div>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Baki BF (RM)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBaki" runat="server" Text='<%#Eval("BakiBF")%>'>  
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div style="text-align: right;">
                                                    <asp:Label ID="lblTotBaki" runat="server" />
                                                </div>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Jumlah (RM)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblJum" runat="server" Text='<%#Eval("Jumlah")%>' ForeColor="#003399">  
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div style="text-align: right;">
                                                    <asp:Label ID="lblTotJum" runat="server" CssClass="cssTotBajet2" ForeColor="#003399" Font-Bold="true"/>
                                                </div>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="#6699FF" />
                                </asp:GridView>
                    <asp:HiddenField ID="hidIndObjAm" runat="server" />
                    <div style="text-align: center; margin-bottom: 10px;">
                        <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                        </asp:LinkButton>
                    </div>
                </div>
            </div>                
                  <asp:Button ID="btnShowPopup" runat="server"  style="display:none;"  />               
                    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"  PopupControlID="pnlpopup" TargetControlID="btnShowPopup" BehaviorID="mpe" >
                    </ajaxToolkit:ModalPopupExtender>  
     
        <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Width="750px" style="display:;">              
                <table  style="border: Solid 3px #D5AA00; width: 100%; height: 100%;"  >
                    <tr style="background: linear-gradient(to bottom, #fceabb 0%,#fccd4d 50%,#f8b500 51%,#fbdf93 100%);">
                        <td style="height: 10%;text-align:center;">
                            Tapisan</td>
                        <td   style="width:50px;text-align:center;">   
                           <button runat="server" id="btnCancel" title="Tutup" class="btnNone ">
    <i class="far fa-window-close fa-2x"></i>
</button></td>
                                                    
                        
                    </tr>
                    <tr style="vertical-align:top;">
                        <td colspan="2">&nbsp;
                            <div class="panel panel-default" style="width:95%;">
    <div class="panel-body">
        <table class="nav-justified" style="margin:8px;">
              <tr>
                  <td style="height: ;width:100px;"><label class="control-label">
                      Kod Operasi :</label>               
                  </td>
                  <td >                     
                      <asp:DropDownList ID="ddlKO" runat="server" AutoPostBack="True" CssClass="form-control" Width="50%">
                      </asp:DropDownList>
                  </td>                                
              </tr>
            <tr>
                  <td style="height: ;width:100px;"><label class="control-label">
                      Kod Projek :</label>               
                  </td>
                  <td >                     
                      <asp:DropDownList ID="ddlKP" runat="server" AutoPostBack="True" CssClass="form-control" Width="50%">
                      </asp:DropDownList>
                  </td>                                
              </tr>


        </table>

        </div></div>


                            


                            
                              
                      </tr>
         

                    <tr>
                        <td colspan="2">
                            <div style="text-align:center;margin-bottom:10px;">
                            <asp:LinkButton ID="lbtnTapis" runat="server" CssClass="btn ">
                        <i class="fas fa-filter fa-lg"></i>&nbsp;&nbsp;&nbsp;Tapis
                    </asp:LinkButton></div>
                        </td>
                    </tr>
          </table> 
            </asp:Panel>
  
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
