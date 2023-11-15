<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Draf_ABM.aspx.vb" Inherits="SMKB_Web_Portal.Draf_ABM"  EnableEventValidation="False" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="container-fluid">
    
    <div class="row">
       
            <div class="col-sm-12 col-md-8 col-lg-8">
                <div>
                    <asp:LinkButton ID="lbtnKembali" runat="server" CssClass="btn btn-info">
                    <i class="fa fa-arrow-left fa-lg"></i>&nbsp;&nbsp;&nbsp;Kembali
                    </asp:LinkButton>
                </div>
             </div>
        <br/>
        <br/>
        <div class="col-sm-9 col-md-6 col-lg-8">
            <div class="panel-group">
            <div class="panel panel-default">
             <div class="panel-heading">
                <h4 class="panel-title">DRAF ABM</h4>
             </div>
                 <br/>
                <div class="col-sm-12" style="margin-bottom:10px;padding-left: 0;">
                <label class="control-label" for="">
                    Bajet tahun :</label> &nbsp; <asp:TextBox ID="txtYear" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox></div>
                <asp:GridView ID="gvAbmMaster" runat="server"
                            AllowSorting="True" AutoGenerateColumns="False" 
                              BorderColor="#999999" BorderStyle="Double" BorderWidth="1px" 
                              CssClass="table table-striped table-bordered table-hover" 
                              EmptyDataText=" " Font-Size="8pt" ShowFooter="True" 
                              ShowHeaderWhenEmpty="True" Width="95%"
                            DataKeyNames="KodVot" >
                            <Columns>
                                <asp:TemplateField>
                <ItemTemplate>
                    <asp:Panel ID="pnlAbmMaster" runat="server">
                        <asp:Image ID="imgCollapsible" runat="server" ImageUrl="../../../Images/plus.png" Style="margin-right: 5px;" />
                        <span style="font-weight:bold;display:none;"><%#Eval("KodVot")%></span>
                    </asp:Panel>
                 
                    <cc1:CollapsiblePanelExtender ID="ctlCollapsiblePanel" runat="Server" AutoCollapse="False" AutoExpand="False" CollapseControlID="pnlAbmMaster" Collapsed="True" CollapsedImage="../../../Images/plus.png" CollapsedSize="0" ExpandControlID="pnlAbmMaster" ExpandDirection="Vertical" ExpandedImage="../../../Images/minus.png" ImageControlID="imgCollapsible" ScrollContents="false" TargetControlID="pnlAbmChild" />
                </ItemTemplate>
                                    <ItemStyle Width="1px" />
            </asp:TemplateField>
                                <asp:TemplateField HeaderText = "Bil">
        <ItemTemplate>
            <asp:Label ID="lblBil" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
        </ItemTemplate>
                                      <ItemStyle Width="30px" />
    </asp:TemplateField>
                                <asp:BoundField DataField="KodVot" HeaderText="Kod">
                                <ItemStyle Width="18%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Butiran" HeaderText="Jenis Perbelanjaan">
                               
                                <ItemStyle Width="80%" />
                               
                                </asp:BoundField>
                                <asp:TemplateField>
                <ItemTemplate>
                    <tr>
                        <td colspan="100%">
                            <asp:Panel ID="pnlAbmChild" runat="server" Style="margin-left:20px;margin-right:20px;
                                   height:0px;overflow: hidden;" Width="95%">
                                <i class="fa fa-info-circle fa-lg" aria-hidden="true" data-toggle="tooltip" data-placement="top" title="Jumlah 'Anggaran Disyorkan' akan ditentukan setelah permohonan dihantar ke pejabat Bendahari.">&nbsp;&nbsp;Info</i>
                                <asp:GridView ID="gvAbmDt" runat="server" 
                                    AllowSorting="True" AutoGenerateColumns="False" 
                                    BorderColor="#999999" BorderStyle="Double" 
                                    BorderWidth="1px" CssClass="table table-striped table-bordered table-hover" 
                                    EmptyDataText=" " Font-Size="8pt" 
                                    ShowFooter="True" ShowHeaderWhenEmpty="True" 
                                    Width="100%" 
                                    OnRowCreated="gvAbmDt_RowCreated"
                                    OnRowDataBound ="gvAbmDt_RowDataBound"  >
                                    <Columns>
                                        <asp:TemplateField HeaderText = "Bil">
        <ItemTemplate>
            <asp:Label ID="lblBil" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
        </ItemTemplate>
                                      <ItemStyle Width="5%" />
    </asp:TemplateField>
                                        <asp:BoundField DataField="KodVot" HeaderText="Kod" ReadOnly="True">
                                        <ItemStyle Font-Bold="True" Width="5%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Jenis" HeaderText="Jenis Perbelanjaan" ReadOnly="True">
                                        <ItemStyle Font-Bold="True" Width="15%" />
                                        </asp:BoundField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label runat="server" CssClass="" Text='<%# lblPeruntukanPrev %>' />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("PeruntukanPrev") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label runat="server" Text='<%# lblPerbelanjaanPrev %>' />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("PerbelanjaanPrev") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="10%"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label runat="server" Text='<%# lblAnggaranSyorNow %>' />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("AnggaranSyorNow") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="10%"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label runat="server" Text='<%# lblAnggaranMintaNext %>' />
                                            </HeaderTemplate>
                                            <ItemTemplate >
                                                <asp:Label runat="server" Text='<%# Eval("AnggaranMintaNext") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" BackColor="#E1E1E1" ForeColor="#0033CC"  Width="10%"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label runat="server" Text='<%# lblAnggaranSyorNext %>' />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("AnggaranSyorNext") %>' />                                                                                               
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="10%"/>
                                        </asp:TemplateField>
                                        
                                        <%--<asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbDetail0" runat="server" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="btn-xs btn-primary" OnClick="lnkView_Click" ToolTip="Program">
                                <i class="fa fa-ellipsis-h fa-lg"></i>
                            </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="3%" />
                                        </asp:TemplateField>--%>
                                    </Columns>
                                    <HeaderStyle BackColor="#FFEFB4" Font-Bold="True" ForeColor="black" />
                                </asp:GridView>
                                
                            </asp:Panel>
                        </td>
                    </tr>
                </ItemTemplate>
                                    <ItemStyle Width="1px" />
            </asp:TemplateField>
                                
                                    
                            </Columns>
                            <SelectedRowStyle BackColor="#FFFFAA" Font-Bold="True" />

                        </asp:GridView>
                 <br/>
                <div style="text-align:center">
                    <asp:Button ID="btnHantar" runat="server" Text="Hantar"  CssClass="btn btn-info"/>
                </div>
           


             </div>
             </div>
             </div>
            </div>
         
    </div>
    
       <%--  <asp:Button ID="btnShowPopup" runat="server" style="display:none" />--%>
            <%--<asp:Button ID="btnShowPopup2" runat="server" style="display:none" />--%>
            
            <%--<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground" CancelControlID="ImageButton2" PopupControlID="pnlPopProgram" TargetControlID="btnShowPopup" BehaviorID="_content_ModalPopupExtender1" DynamicServicePath="">
                                     </ajaxToolkit:ModalPopupExtender>
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground" CancelControlID="ImageButton1" PopupControlID="pnlPopupButiran" TargetControlID="btnShowPopup2" BehaviorID="_content_ModalPopupExtender1" DynamicServicePath="">
                                     </ajaxToolkit:ModalPopupExtender>--%>
            
            
           <asp:Button ID = "btnOpen" runat = "server" Text = ""  style="display:none" />
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3"  runat="server" BackgroundCssClass="modalBackground" 
            CancelControlID="lbNo" PopupControlID="pnlpopupHantar" TargetControlID="btnOpen"> </ajaxToolkit:ModalPopupExtender>
        
            <asp:Panel ID="pnlpopupHantar" runat="server" BackColor="White">
               <table style="border: Solid 3px #D5AA00; width: 100%; height: 100%; " >
                    <tr style="background: linear-gradient(to bottom, #fceabb 0%,#fccd4d 50%,#f8b500 51%,#fbdf93 100%);">
                        <td style="height: 10%;  font-weight: bold; text-align:center;" colspan="2" >
                            &nbsp;</td>
                        
                    </tr>
                   
                   <tr style="vertical-align:top;">
                        <td style="font-weight: bold; text-align:center;" colspan="2" class="auto-style1" >
                            <div style="margin-bottom:10px;margin-top:10px;margin-left:10px;margin-right:10px;">                         
                                <%= strMsg  %>
                                </div>
                        </td>                        
                    </tr>
                   <tr>
                       <td style="height: 10%; text-align:center;" colspan="2" >
                           <div style="margin-bottom:10px;margin-top:10px;margin-left:10px;margin-right:10px;">
                            <asp:LinkButton ID="lbYes" runat="server" CssClass="btn btn-info">
                                <i class="fa fa-check fa-lg"></i>&nbsp;&nbsp;&nbsp;Ya
                            </asp:LinkButton>
                            &nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="lbNo" runat="server" CssClass="btn btn-info">
                                <i class="fa fa-times fa-lg"></i>&nbsp;&nbsp;&nbsp;Tidak
                            </asp:LinkButton>
                              
                               </div>
                        </td>
                   </tr>
                </table>
            </asp:Panel>
          

        
    </ContentTemplate>
</asp:UpdatePanel>  
</asp:Content>
