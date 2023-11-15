<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Senarai_Agihan.aspx.vb" Inherits="SMKB_Web_Portal.Senarai_Agihan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>

    <style>
        .panel
        {
            width:70%;
        }

        @media (max-width: 1600px) {
            .panel {
                width: 95%;
            }
        }
    </style>

    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate> 
              <div class="row">       
            <div class="panel-body well" style="width:800px;">
        <div class="row">
        <table style="width: 100%;">
                      <tr>
                            <td style="width: 70px">
                                <label class="control-label" for="">
                                Tahun :</label></td>
                            <td>
                                <%--<asp:TextBox ID="txtTahun" runat="server" BackColor="#FFFFCC" class="form-control" ReadOnly="True" Width="10%"></asp:TextBox>--%>
                                 <asp:DropDownList ID="ddlTahunAgih" runat="server" AutoPostBack="True" CssClass="form-control" style="width: 100px; height: 21px;"/>
                            </td>
                        </tr>
                         <tr>
                             <td>
                                 <label class="control-label" for="">
                                 PTj :</label></td>
                             <td>
                                 <asp:DropDownList ID="ddlPTJ" runat="server" AutoPostBack="True" CssClass="form-control" style="width: 600px; height: 21px;">
                                 </asp:DropDownList>
                             </td>
                      </tr>
               
                  </table>
            </table>
            </div>
        </div>
            </div> 

         <div class="panel panel-default">
        <div class="panel-heading">
            <h4 class="panel-title">Senarai Kelulusan Agihan Peruntukan</h4>
            <li><span style="font-weight:normal">Peruntukan yang telah diagih dan diluluskan</span></li>                       
          </div>
             <div class="panel-body" style="max-height:1000px;overflow:scroll;">
                <div style=" margin:10px;width:95%;">
            <div class="GvTopPanel">
                
                <div class="panel-title pull-right" style="margin-top: 5px;margin-right: 5px;">
             <i class="fas fa-info-circle fa-lg" aria-hidden="true" data-html="true" data-toggle="tooltip" data-placement="top" style="cursor:pointer;color:#ba2818;" 
                 title="Petunjuk : <br/><span style='font-weight:bold'>Jumlah Besar </span> = Jumlah bajet semua Objek Am  <br/><span style='font-weight:bold'>Bajet Objek Am </span>= Jumlah bajet semua Objek Sebagai dalam Objek Am tersebut">
             </i></div></div>
            
        <asp:GridView ID="gvListAgihan" runat="server" 
            ShowHeaderWhenEmpty="True" 
            AutoGenerateColumns="False" 
            EmptyDataText="" 
       cssclass="table table-bordered table-hover" Width="100%" Height="100%" HeaderStyle-BackColor="#D4D4FF" Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" ShowFooter="True"  
            DataKeyNames="ObjAm">
                                 <AlternatingRowStyle BackColor="#F9F9F9" />
                                 <columns>  
                                     <asp:TemplateField HeaderText="IndexObjSbg" Visible="false"  >  
                        <ItemTemplate><asp:Label ID ="lblIndObjSbg" runat="server"  Text='<%#Eval("IndObjSbg")%>'  />
                        </ItemTemplate>
                    </asp:TemplateField>  
                                    <asp:BoundField DataField="kodKW" HeaderText="Kod KW" ReadOnly="True" ItemStyle-HorizontalAlign ="Center">
                                        <ItemStyle Width="8%" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="kodKO" HeaderText="Kod KO" ReadOnly="True" ItemStyle-HorizontalAlign ="Center">
                                        <ItemStyle Width="8%" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Kod PTJ" DataField="kodPTJ" ReadOnly="true" ItemStyle-HorizontalAlign ="Center">
                                         <ItemStyle Width="8%" />
                                    </asp:BoundField>
                                     <asp:BoundField HeaderText="Kod KP" DataField="kodKP" ReadOnly="true" ItemStyle-HorizontalAlign ="Center">
                                         <ItemStyle Width="8%" />
                                    </asp:BoundField>
                                     <asp:BoundField HeaderText="Objek AM" DataField="ObjAm" ReadOnly="true" >
                                         <ItemStyle Width="25%" />
                                    </asp:BoundField>
                                     <%--<asp:BoundField HeaderText="Objek Sebagai" DataField="ObjSbg" ReadOnly="true" >
                                         <ItemStyle Width="30%" />
                                    </asp:BoundField>--%>

                                     <asp:TemplateField HeaderText="Objek Sebagai">  
                        <ItemTemplate>
                            <asp:Label ID="lblObjSbg" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("ObjSbg"))) %>'>  
                            </asp:Label>
                        </ItemTemplate>  
  
                        <FooterTemplate>  
                            <div style="text-align:right;font-weight:bold;"><asp:Label ID="lblJumlah" runat="server" Text="Jumlah Besar (RM)" /></div>  
                        </FooterTemplate>  
                                         <ItemStyle Width="25%" />
                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Bajet (RM)">  
                        <ItemTemplate>
                            <div style="text-align:right;"><asp:Label ID="lblBajet" runat="server" Text='<%#Eval("Bajet")%>'>  
                            </asp:Label></div>

                        </ItemTemplate>  
  
                        <FooterTemplate>  
                           <div style="text-align:right;font-weight:bold;">
                               <asp:Label ID="lblTotBajet" runat="server" ForeColor="#003399" Font-Bold="true"/> 
                           </div>
                        </FooterTemplate>  
                                         
                                          <ItemStyle Width="12%" />
                                         
                    </asp:TemplateField>
                                                                          
                                </columns>

<HeaderStyle BackColor="#FECB18"></HeaderStyle>
                            </asp:GridView>  
        </div>
            </div>
        </div>


    </div>
  
            </ContentTemplate>

     </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                              <ProgressTemplate>
                                 
                                  <div style="background-color:#D2D2D2;    filter:alpha(opacity=80);    opacity:0.80; width: 100%; top: 0px; left: 0px; position: fixed; height: 100%;">

        </div>
          <div style="margin:auto;font-family:Trebuchet MS;filter:alpha(opacity=100);    opacity:1;     font-size:small;    
 vertical-align: middle; top: auto; position: absolute; left: auto; color: #FFFFFF;
 position: fixed;
  top: 50%;
  left: 50%;
  margin-top: -50px;
  margin-left: -100px;">
                <table>
                <tr>
                <td><span style="font-family: Trebuchet MS; font-size: medium; font-weight: bold; color: Black">Memuat data...</span></td>
                </tr>

                    <tr>
                        <td>
                            <img src="../../../Images/loader.gif" alt=""  />
                        </td>
                    </tr>

                </table>

        </div>
                                 
                              </ProgressTemplate>
                          </asp:UpdateProgress>
</asp:Content>
