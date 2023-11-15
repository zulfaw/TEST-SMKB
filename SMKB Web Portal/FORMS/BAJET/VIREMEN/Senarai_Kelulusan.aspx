<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Senarai_Kelulusan.aspx.vb" Inherits="SMKB_Web_Portal.Senarai_Kelulusan1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="panel panel-default" style="margin-top:15px;width:1200px;">
                            <div class="panel-heading">
                                Senarai viremen yang telah diluluskan
                            </div>
                            <div class="panel-body">

                                <div class="row">
                 <table style="width: 100%;">
                     
                  <tr>
                      <td style="height: 25px;width:150px;">
                          <label class="control-label" for="">Tahun :
                          </label>
                      </td>
                      <td>
                          <asp:TextBox ID="txtTahun" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" style="width: 150px;"></asp:TextBox>
                      </td>
                  </tr>                    
                     <tr>
                         <td style="height: 25px;width:150px;">
                             <label class="control-label" for="">PTj :</label></td>
                         <td>
                             <asp:DropDownList ID="ddlPTJ" runat="server" AutoPostBack="True" CssClass="form-control" style="width: 600px;">
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
                     <div class="GvTopPanel">
                <div style="float:left;margin-top: 8px;margin-left: 10px;">
                <label class="control-label" for="">Jumlah rekod :</label>&nbsp;<label class="control-label" id ="lblJumRekod" runat="server" style="color:mediumblue;" ></label>
                    </div>
                      
            </div>
             <asp:GridView ID="gvViremen" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" EmptyDataText =" "
                        cssclass="table table-striped table-bordered table-hover" Width="100%" HeaderStyle-BackColor="#ffffb3" Font-Size="8pt"
                        BorderColor="#333333" BorderStyle="Solid" >
                            <columns>
                          
                                 <asp:TemplateField HeaderText ="Bil">
                                    <ItemTemplate >
                                        <asp:Label ID="lblBil" Text ='<%# Container.DataItemIndex + 1 %>' runat ="server" />
                                  
                                    </ItemTemplate>
                                    <ItemStyle Width ="10px" />

                                </asp:TemplateField>
                            <asp:BoundField HeaderText="No. Viremen" DataField="NoViremen" SortExpression="NoViremen" ReadOnly="true">
                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                            </asp:BoundField>
                                <asp:BoundField HeaderText="Tarikh Mohon" DataField="TkhMohon" SortExpression="TkhMohon" ReadOnly="true">
                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                            </asp:BoundField>
                                <asp:BoundField HeaderText="No. Staf" DataField="NoStaf" SortExpression="NoStaf" ReadOnly="true">
                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="KW" DataField="KwF" SortExpression="DrKw" ReadOnly="true">
                                <ItemStyle Width="5%" HorizontalAlign="Center"/>
                            </asp:BoundField>
                                 <asp:BoundField DataField="KoF" HeaderText="KO" >
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                </asp:BoundField>
                            <asp:BoundField HeaderText="PTj" DataField="PtjF" SortExpression="DrPtj" ReadOnly="true">
                                <ItemStyle Width="5%" HorizontalAlign="Center"/>
                            </asp:BoundField>
                                 <asp:BoundField DataField="KpF" HeaderText="KP" >
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                </asp:BoundField>
                            <asp:BoundField HeaderText="Objek Sebagai" DataField="ObjSbgF" SortExpression="DrObjSbg" ReadOnly="true">
                                <ItemStyle Width="5%" HorizontalAlign="Center"/>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Amaun (RM)" DataField="JumlahF" SortExpression="DrJumlah" ReadOnly="true">
                                <ItemStyle Width="10%" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="KW" DataField="KwT" ReadOnly="true">
                                <ItemStyle Width="5%" HorizontalAlign="Center"/>
                            </asp:BoundField>
                                 <asp:BoundField DataField="KoT" HeaderText="KO" >
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                </asp:BoundField>
                            <asp:BoundField HeaderText="PTj" DataField="PtjT" ReadOnly="true">
                                <ItemStyle Width="5%" HorizontalAlign="Center"/>
                            </asp:BoundField>
                                 <asp:BoundField DataField="KpT" HeaderText="KP" >
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                </asp:BoundField>
                            <asp:BoundField HeaderText="Objek Sebagai" DataField="ObjSbgT" ReadOnly="true">
                                <ItemStyle Width="5%" HorizontalAlign="Center"/>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Amaun (RM)" DataField="JumlahT" ReadOnly="true">
                                <ItemStyle Width="10%" HorizontalAlign="Right" />
                            </asp:BoundField>  
                                <asp:TemplateField>
                                  
                                  <ItemTemplate>
                                      <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="btn-xs" ToolTip="Pilih">
                                            <i class="fas fa-edit"></i>
                                            </asp:LinkButton>
                                  </ItemTemplate>
                                  <ItemStyle Width="5%" />
                              </asp:TemplateField>                                                                        
                           
                        </columns>                        
                            <HeaderStyle BackColor="#FFFFB3" />                       
                    </asp:GridView>
                 </div>



                                </div></div>

            <div class="panel panel-default" style="margin-top:15px;width:1100px;">
                            <div class="panel-heading">
                                Maklumat Terperinci
                            </div>
                            <div class="panel-body">

             <div class="row">
                 <table style="width: 100%;">
                     
                  <tr>
                      <td style="height: 25px;width:150px;">
                          <label class="control-label" for="">No Viremen :
                          </label>
                      </td>
                      <td>
                          <asp:TextBox ID="txtNoViremen" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" style="width: 200px;"></asp:TextBox>
                      </td>
                  </tr>                    
                     <tr>
                         <td style="height: 25px;width:150px;">
                             <label class="control-label" for="">
                             Tarikh Mohon :
                             </label>
                         </td>
                         <td>
                             
                             <asp:TextBox ID="txtTkhMohon" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" style="width: 150px;"></asp:TextBox>
                             
                         </td>
                     </tr>
          </table>
             </div>

             <div class="auto-style1">
                 <div class="panel panel-default" style="margin-top:15px;margin-left:0px;width:100%;">
                            <div class="panel-heading">
                                Viremen Keluar
                            </div>
                            <div class="panel-body">
                                <table style="width: 100%;">
                     
                  <tr>
                      <td style="height: 25px;">
                          <label class="control-label" for="">
                          Kumpulan Wang :
                          </label>
                      </td>
                      <td>
                          <asp:TextBox ID="txtKodKWF" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" style="width: 50px;text-align:left;"></asp:TextBox>
                          &nbsp;-
                          <asp:TextBox ID="txtKwF" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" style="width: 400px;"></asp:TextBox>
   
                      </td>
                  </tr>
                  <tr>
                      <td style="height: 25px;">
                          <label class="control-label" for="">
                          Kod Operasi :
                          </label>
                      </td>
                      <td>
                          <asp:TextBox ID="txtKodKoF" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" style="width: 50px;text-align:left;"></asp:TextBox>
                          &nbsp;-
                          <asp:TextBox ID="txtKoF" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" style="width: 200px;"></asp:TextBox>
                      </td>
                  </tr>
                  <tr style="height:25px">
                      <td style="height: 25px;">
                          <label class="control-label" for="">
                          PTJ :
                          </label>
                      </td>
                      <td>
                          <asp:TextBox ID="txtKodPTjF" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" style="width: 100px;text-align:left; "></asp:TextBox>
                          &nbsp;-
                          <asp:TextBox ID="txtPTjF" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" style="width: 600px;"></asp:TextBox>
                      </td>
                  </tr>
                  <tr style="height:25px">
                      <td style="height: 25px;">
                          <label class="control-label" for="">
                          Kod Projek :
                          </label>
                      </td>
                      <td>
                          <asp:TextBox ID="txtKodKPF" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" style="width: 100px;text-align:left; "></asp:TextBox>
                          &nbsp;-
                          <asp:TextBox ID="txtKPF" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" style="width: 200px;"></asp:TextBox>
                      </td>
                  </tr>
                  <tr style="height:25px">
                      <td style="height: 25px;">
                          <label class="control-label" for="">
                          Objek Sebagai :
                          </label>
                      </td>
                      <td>
                          <asp:TextBox ID="txtKodSbgF" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" style="width: 100px;text-align:left;"></asp:TextBox>
                          &nbsp;-
                          <asp:TextBox ID="txtObjSbgF" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" style="width: 500px;"></asp:TextBox>
                      </td>
                  </tr>
                                    <tr style="height:25px">
                                        <td style="height: 25px;">
                                            <label class="control-label" for="">
                                            Amaun (RM) :
                                            </label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAmaunF" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" style="width: 100px;text-align:right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="height:25px">
                                        <td style="height: 25px;">
                                            <label class="control-label" for="">
                                            Baki Peruntukan (RM) :
                                            </label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtBakiF" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" style="width: 100px;text-align:right;"></asp:TextBox>
                                        </td>
                                    </tr>
          </table>
                                
                               
                            </div>
                        </div>
             </div>

             <div class="row" style="width:100%;">
                 <div class="panel panel-default" style="margin-top:15px;margin-left:0px;width:100%;">
                            <div class="panel-heading">
                                Viremen Masuk
                            </div>
                            <div class="panel-body">
                                <table style="width: 100%;">
                     
                  <tr>
                      <td style="height: 25px;">
                          <label class="control-label" for="">
                          Kumpulan Wang :
                          </label>
                      </td>
                      <td>
                          <asp:TextBox ID="txtKodKWT" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" style="width: 50px;text-align:left;"></asp:TextBox>
                          &nbsp;-
                          <asp:TextBox ID="txtKWT" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" style="width: 400px;"></asp:TextBox>                         
                      </td>
                  </tr>
                  <tr>
                      <td style="height: 25px;">
                          <label class="control-label" for="">
                          Kod Operasi :
                          </label>
                      </td>
                      <td>
                          <asp:TextBox ID="txtKodKoT" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" style="width: 50px;text-align:left;"></asp:TextBox>
                          &nbsp;-
                          <asp:TextBox ID="txtKoT" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" style="width: 200px;"></asp:TextBox>
                      </td>
                  </tr>
                  <tr style="height:25px">
                      <td style="height: 25px;">
                          <label class="control-label" for="">
                          PTJ :
                          </label>
                      </td>
                      <td>
                          <asp:TextBox ID="txtKodPTjT" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" style="width: 100px;text-align:left; "></asp:TextBox>
                          &nbsp;-
                          <asp:TextBox ID="txtPTjT" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" style="width: 600px;"></asp:TextBox>
                      </td>
                  </tr>
                  <tr style="height:25px">
                      <td style="height: 25px;">
                          <label class="control-label" for="">
                          Kod Projek :
                          </label>
                      </td>
                      <td>
                          <asp:TextBox ID="txtKodKpT" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" style="width: 100px;text-align:left; "></asp:TextBox>
                          &nbsp;-
                          <asp:TextBox ID="txtKpT" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" style="width: 200px;"></asp:TextBox>
                      </td>
                  </tr>
                  <tr style="height:25px">
                      <td style="height: 25px;">
                          <label class="control-label" for="">
                          Objek Sebagai :
                          </label>
                      </td>
                      <td>
                          <asp:TextBox ID="txtKodSbgT" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" style="width: 100px;text-align:left;"></asp:TextBox>
                          &nbsp;-
                          <asp:TextBox ID="txtObjSbgT" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" style="width: 300px;"></asp:TextBox>
                      </td>
                  </tr>
                                    <tr style="height:25px">
                                        <td style="height: 25px;">
                                            <label class="control-label" for="">
                                            Amaun (RM) :
                                            </label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAmaunT" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" style="width: 100px;text-align:right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="height:25px">
                                        <td style="height: 25px;">
                                            <label class="control-label" for="">
                                            Baki Peruntukan (RM) :
                                            </label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtBakiT" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" style="width: 100px;text-align:right;"></asp:TextBox>
                                        </td>
                                    </tr>
          </table>
                                
                               
                            </div>
                        </div>
             </div>

             
<div class="row" style="width:100%;">
                 <div class="panel panel-default" style="margin-top:15px;margin-left:0px;width:100%;">
                            <div class="panel-heading">
                                Maklumat Pemohon
                            </div>
                            <div class="panel-body">
                                <table style="width: 100%;">
                     
                  <tr>
                      <td style="height: 25px;">
                          <label class="control-label" for="">
                          Staf :
                          </label>
                      </td>
                      <td>
                          <asp:TextBox ID="txtNoStaf" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" style="width: 50px;text-align:left;"></asp:TextBox>
                             &nbsp;-
                             <asp:TextBox ID="txtNamaStaf" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" style="width: 600px;"></asp:TextBox>                     
                      </td>
                  </tr>
                  <tr>
                      <td style="height: 25px;">
                          <label class="control-label" for="">
                          Jawatan :
                          </label>
                      </td>
                      <td>
                          <asp:TextBox ID="txtJawatan" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" style="width: 350px;text-align:left;"></asp:TextBox>
                          
                      </td>
                  </tr>
                  <tr style="height:25px">
                      <td style="height: 25px;">
                          <label class="control-label" for="">
                          PTJ :
                          </label>
                      </td>
                      <td>
                          <asp:TextBox ID="txtkodPTjPemohon" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" style="width: 100px;text-align:left; "></asp:TextBox>
                          &nbsp;-
                          <asp:TextBox ID="txtPTjPemohon" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" style="width: 600px;"></asp:TextBox>
                      </td>
                  </tr>
          </table>
                                
                               
                            </div>
                        </div>
             </div>
           </div></div>




            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
