<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="MaklumatDefendBend.aspx.vb" Inherits="SMKB_Web_Portal.MaklumatDefendBend" EnableEventValidation="False" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">  

     <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>
    <link rel="stylesheet" type="text/css" href="../../../Content/Site.css">
  
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate> 

<script type="text/javascript">
         
        function pageLoad() {
            $('[data-toggle="tooltip"]').tooltip()
        }
      
        function showNestedGridView(obj) {
            var nestedGridView = document.getElementById(obj);
            var imageID = document.getElementById('image' + obj);
            if (nestedGridView.style.display == "none") {
                nestedGridView.style.display = "inline";
                imageID.src = "../../../Images/minus.png";
            } else {
                nestedGridView.style.display = "none";
                imageID.src = "../../../Images/plus.png";
            }
        }
</script>

<style> 
.btnDisabled{
    background: #ffffff;
    background-image: -webkit-linear-gradient(top, #ffffff, #eaeff2);
    background-image: -moz-linear-gradient(top, #ffffff, #eaeff2);
    background-image: -ms-linear-gradient(top, #ffffff, #eaeff2);
    background-image: -o-linear-gradient(top, #ffffff, #eaeff2);
    background-image: linear-gradient(to bottom, #ffffff, #eaeff2);
    -webkit-border-radius: 5;
    -moz-border-radius: 5;
    border-radius: 3px;
    font-family: Arial;
    color: #999999;
    font-size: 12px;
    padding: 5px 5px 5px 5px;
    border: solid #808285 1px;
    text-decoration: none;
    width: 120px;
}       
</style>

<div class="row">
                <div class="panel-group" id="pnlNotice" runat="server" visible="true">
                
                   <div>
                <asp:LinkButton ID="lbtnKembali" runat="server" CssClass="btn btn-info">
                    <i class="fa fa-arrow-left fa-lg"></i>&nbsp;&nbsp;&nbsp;Kembali
                </asp:LinkButton>
                   </div>
                
              </div>       
       
        <%--<div class="panel panel-default" style="width:70%">
            <div class="panel-heading"><h4 class="panel-title">Tapisan</h4></div>
            <div class="panel-body">
                 B<label class="control-label" for="">ahagian&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</label> 
            <asp:DropDownList ID="ddlUnit" runat="server" AutoPostBack="True" CssClass="form-control" Width="50%"></asp:DropDownList>
            </div>
            <div class="panel-body">
                 Status<label class="control-label" for="">&nbsp;&nbsp;:</label> 
            <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" CssClass="form-control" Width="50%"></asp:DropDownList>
            </div>
            </div>--%>
        <br />
      
        

        <div class="panel-group">

            <div id="PanelMohonBaru" class="panel-collapse">
                 <div class="panel-body">
                     <table class="table table table-borderless" style="width:100%">
                         <tr>
                             <td class="auto-style5">
                                 <Label class="control-label" for="">Tahun Bajet</Label>
                             </td>
                             <td class="auto-style2">
                                 <asp:TextBox ID="txtTahunBajet" runat="server" CssClass="form-control centerAlign" ReadOnly="true" Width="40px">2021</asp:TextBox>
                             </td>
                         </tr>
                         <tr>
                             <td class="auto-style5">
                                 <Label class="control-label" for="">PTj</Label>
                             </td>
                             <td class="auto-style2">
                                 <asp:TextBox ID="txtPtj" runat="server" CssClass="form-control" ReadOnly="true" Width="80%"></asp:TextBox>
                             </td>
                         </tr>
                     </table>
                     </div>
                </div>
           
    
        <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" CssClass="tabCtrl" Height="100%" Width="100%">                                    
                <ajaxToolkit:TabPanel ID="tabABM" runat="server" HeaderText ="Anggaran Belanja Mengurus (ABM)">
                    <HeaderTemplate>
                        Vot 10000
                    </HeaderTemplate>
                    <ContentTemplate>

                        <asp:GridView ID="gvAbmMaster" runat="server" AllowSorting="True" AutoGenerateColumns="False" BorderColor="#999999" BorderStyle="Double" BorderWidth="1px" CssClass="table table-striped table-bordered table-hover"  EmptyDataText=" " Font-Size="8pt" ShowFooter="True" ShowHeaderWhenEmpty="True" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="Bil">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBil0" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />
                                    </ItemTemplate>
                                    <ItemStyle Width="1%" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="kodvot" HeaderText="Kod Objek Sebagai">
                                <ItemStyle Width="10%" />
                                </asp:BoundField>

                                  <asp:TemplateField HeaderText="Kod Objek">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBil0" runat="server" Text='<%#Eval("Butiran")%>' />
                                    </ItemTemplate>
                         <FooterTemplate>                            
                            <div style="text-align:right;font-weight:bold;"><asp:Label Text="Jumlah Besar (RM)" runat="server" /></div>  
                        </FooterTemplate>
                                    <ItemStyle Width="30%" HorizontalAlign="Left" />
                                </asp:TemplateField>

                            <asp:TemplateField HeaderText="Permohonan 2021 (RM)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBil0" runat="server" Text="-" />
                                    </ItemTemplate>
                            <FooterTemplate>  
                            <div style="text-align:right;"> 
                            <asp:Label ID="lblTotTB" runat="server" Text="0.00"/>  
                                </div>
                        </FooterTemplate> 
                                    <ItemStyle Width="12%" HorizontalAlign="Right" />
                                </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cadangan Agihan 2021 (RM)">  
                        <ItemTemplate>
                            <%--<asp:TextBox ID="txtBajet" runat="server" Width="98%" Text='<%#Eval("Bajet")%>' 
                               onkeypress="return isNumberKey(event,this)" onkeyup="fCalcTotal(this);" CssClass="rightAlign"></asp:TextBox> --%>
                            <asp:TextBox ID="txtBajet"  runat="server" Width="98%" 
                                AutoPostBack="true" CssClass="form-control rightAlign"></asp:TextBox> 
                        </ItemTemplate> 
                            <FooterTemplate>  
                            <div style="text-align:right;"> 
                            <asp:Label ID="lblTotTB" runat="server" Text="0.00"/>  
                                </div>
                        </FooterTemplate> 
                        <ItemStyle HorizontalAlign="Right" Width="12%" />
                    </asp:TemplateField>


                            </Columns>
                            <SelectedRowStyle BackColor="#FFFFAA" Font-Bold="True" />
                        </asp:GridView>
     
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>

                    <ajaxToolkit:TabPanel ID="tabKeseluruhan" runat="server" HeaderText ="Senarai Permohonan">
                        <HeaderTemplate>
                            Vot&nbsp; 20000
                        </HeaderTemplate>
                    <ContentTemplate>                        



                         <div style="text-align:left;margin-top:10px;"> 
                           
                             <asp:GridView ID="gvAbmMasterv2" runat="server" AllowSorting="True" AutoGenerateColumns="False" BorderColor="#999999" BorderStyle="Double" BorderWidth="1px" CssClass="table table-striped table-bordered table-hover" EmptyDataText=" " Font-Size="8pt" ShowFooter="True" ShowHeaderWhenEmpty="True" Width="100%">
                                 <Columns>
                                     <asp:TemplateField HeaderText="Bil">
                                         <ItemTemplate>
                                             <asp:Label ID="lblBil1" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />
                                         </ItemTemplate>
                                         <ItemStyle Width="1%" />
                                     </asp:TemplateField>
                                     <asp:BoundField DataField="kodvot" HeaderText="Kod Objek Sebagai">
                                     <ItemStyle Width="10%" />
                                     </asp:BoundField>
                                     <asp:TemplateField HeaderText="Kod Objek">
                                         <FooterTemplate>
                                             <div style="text-align:right;font-weight:bold;">
                                                 <asp:Label runat="server" Text="Jumlah Besar (RM)" />
                                             </div>
                                         </FooterTemplate>
                                         <ItemTemplate>
                                             <asp:Label ID="lblBil2" runat="server" Text='<%#Eval("Butiran")%>' />
                                         </ItemTemplate>
                                         <ItemStyle HorizontalAlign="Left" Width="30%" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Permohonan 2021 (RM)">
                                         <FooterTemplate>
                                             <div style="text-align:right;">
                                                 <asp:Label ID="lblTotTB0" runat="server" Text="0.00" />
                                             </div>
                                         </FooterTemplate>
                                         <ItemTemplate>
                                             <asp:Label ID="lblBil3" runat="server" Text="-" />
                                         </ItemTemplate>
                                         <ItemStyle HorizontalAlign="Right" Width="12%" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Cadangan Agihan 2021 (RM)">
                                         <FooterTemplate>
                                             <div style="text-align:right;">
                                                 <asp:Label ID="lblTotTB1" runat="server" Text="0.00" />
                                             </div>
                                         </FooterTemplate>
                                         <ItemTemplate>
                                             <%--<asp:TextBox ID="txtBajet" runat="server" Width="98%" Text='<%#Eval("Bajet")%>' 
                               onkeypress="return isNumberKey(event,this)" onkeyup="fCalcTotal(this);" CssClass="rightAlign"></asp:TextBox> --%>
                                             <asp:TextBox ID="txtBajet0" runat="server" AutoPostBack="true" CssClass="form-control rightAlign" Width="98%"></asp:TextBox>
                                         </ItemTemplate>
                                         <ItemStyle HorizontalAlign="Right" Width="12%" />
                                     </asp:TemplateField>
                                 </Columns>
                                 <SelectedRowStyle BackColor="#FFFFAA" Font-Bold="True" />
                             </asp:GridView>
                           
        </div>



        </div>
                                                            <br />
                      
                          
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                                <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1"><HeaderTemplate>Vot 30000</HeaderTemplate></cc1:TabPanel>
                <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2"><HeaderTemplate>Vot 40000</HeaderTemplate></cc1:TabPanel>

                <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel3">
                    <HeaderTemplate>
                        Vot 50000
                    </HeaderTemplate>
                </cc1:TabPanel>

                </ajaxToolkit:TabContainer>
     <div></div>

        <div style="text-align:center;margin-top:10px;"> 
            
            <%--<asp:LinkButton ID="lbtnHantar" runat="server" CssClass="btn btn-info" data-toggle="tooltip" title="Keseluruhan permohonan bajet akan dihantar ke pejabat Bendahari." >
                        <i class="fa fa-paper-plane-o fa-lg"></i>&nbsp;&nbsp;&nbsp;Hantar
                    </asp:LinkButton> &nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="lbtnKemaskini" runat="server" CssClass="btn btn-info" data-toggle="tooltip" title="Keseluruhan permohonan bajet akan dipulangkan ke penyedia bajet untuk dikemas kini.">
                        <i class="fa fa-pencil-square-o fa-lg"></i>&nbsp;&nbsp;&nbsp;Kemas Kini
                    </asp:LinkButton>--%>

            <asp:Button ID="btnHantar" runat="server" Text="Simpan"  CssClass="btn btn-info"/>
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnkemaskini" runat="server" Text="Kemas Kini"  CssClass="btn btn-info" Visible="false"/>


        </div>
            
        
       </div>     

            

            <asp:Panel ID="pnlPopupButiran" runat="server" BackColor="White" Width="800px" Style="">
               
                <table width="100%" style="border: Solid 3px #D5AA00; width: 100%; height: 100%;" cellpadding="0" cellspacing="0" >
                    <tr style="background: linear-gradient(to bottom, #fceabb 0%,#fccd4d 50%,#f8b500 51%,#fbdf93 100%);">
                        <td style="height: 10%;  font-weight: bold; " align="center">
                            &nbsp;Maklumat Lanjut</td>
                        <td align="center" style="width:50px;">   
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Close_16x16.png" BorderColor="Black" BorderStyle="Inset" BorderWidth="1px" ToolTip="Tutup" />
                            <%--<asp:Button ID="btnCancel" runat="server" Text="X" />--%></td>
                                                    
                        
                    </tr>
                    <tr style="vertical-align:top;">
                        <td colspan="2" class="auto-style1"><div style="margin-bottom:10px;margin-top:10px;margin-left:10px;margin-right:10px;">
                              <asp:Label ID="Label1" runat="server" Text="Jumlah Rekod : " />
                              <span style="font-weight :bold;"><%= TotalRecDt %></span>
                              <br />
                              <asp:GridView ID="gvButiran" runat="server" AllowSorting="True" AutoGenerateColumns="False" BorderColor="#999999" BorderStyle="Double" BorderWidth="1px" CssClass="table table-striped table-bordered table-hover" EmptyDataText=" " Font-Size="8pt" ShowFooter="True" ShowHeaderWhenEmpty="True" Width="100%">
                                  <Columns>
                                      <asp:TemplateField HeaderText = "Bil">
        <ItemTemplate>
            <asp:Label ID="lblBil" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
        </ItemTemplate>
                                      <ItemStyle Width="5%" />
    </asp:TemplateField>
                                                              <asp:BoundField DataField="Butiran" HeaderStyle-Width="45%" HeaderText="Butiran" SortExpression="Butiran" >
                                                              <HeaderStyle Width="45%" />
                                      </asp:BoundField>
                                                              <asp:BoundField DataField="Kuantiti" HeaderStyle-Width="5%" HeaderText="Kuantiti" SortExpression="Kuantiti" >
                                                              <HeaderStyle Width="5%" />
                                      </asp:BoundField>
                                                              <asp:BoundField DataField="Anggaran" HeaderStyle-Width="15%" HeaderText="Anggaran Perbelanjaan (RM)" SortExpression="Anggaran" >
                                      <HeaderStyle Width="15%" />
                                      <ItemStyle HorizontalAlign="Right" />
                                      </asp:BoundField>
                                      <asp:BoundField DataField="KodVotSebagai" HeaderText="Vot Sebagai" />
                                  </Columns>
                                  <HeaderStyle BackColor="#FFEFB4" Font-Bold="True" ForeColor="black" />
                              </asp:GridView>
                                </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <asp:Panel ID="pnlPopProgram" runat="server" BackColor="White" Width="800px" >
               
                <table width="100%" style="border: Solid 3px #D5AA00; width: 100%; height: 100%;" cellpadding="0" cellspacing="0" >
                    <tr style="background: linear-gradient(to bottom, #fceabb 0%,#fccd4d 50%,#f8b500 51%,#fbdf93 100%);">
                        <td style="height: 10%;  font-weight: bold; " align="center">
                            Program </td>
                        <td align="center" style="width:50px;">   
                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Close_16x16.png" BorderColor="Black" BorderStyle="Inset" BorderWidth="1px" ToolTip="Tutup" />
                            <%--<asp:Button ID="btnCancel" runat="server" Text="X" />--%></td>
                                                    
                        
                    </tr>
                    <tr style="vertical-align:top;">
                        
                        <td colspan="2">&nbsp;<div style="margin-bottom:10px;margin-top:10px;margin-left:10px;margin-right:10px;">
                             
                            <asp:Label runat="server" Text="Jumlah Rekod : " /><span style="font-weight :bold;"> <%= TotalRecProg %> </span> 
                             <asp:GridView ID="gvProgram" runat="server" AllowSorting="True" 
                            AutoGenerateColumns="False"
                            CssClass="table table-striped table-bordered table-hover" 
                            EmptyDataText=" " Font-Size="8pt" ShowFooter="True" 
                            ShowHeaderWhenEmpty="True" Width="100%"  
                            BorderWidth="1px" BorderColor="#999999" BorderStyle="Double" >
                                            <Columns>
                                                <asp:TemplateField HeaderText = "Bil">
        <ItemTemplate>
            <asp:Label ID="lblBil" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
        </ItemTemplate>
                                      <ItemStyle Width="5%" />
    </asp:TemplateField>
                                                <asp:BoundField DataField="NoMohon" HeaderText="No. Permohonan">
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Bahagian" HeaderText="Bahagian">
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Unit" HeaderText="Unit" />
                                                <asp:BoundField DataField="Program" HeaderText="Program" />
                                                <asp:BoundField DataField="AmaunMohon" HeaderText="Anggaran Perbelanjaan (RM)" />
                                            </Columns>
                                            <HeaderStyle BackColor="#FFEFB4" Font-Bold="True" ForeColor="black" />
                                        </asp:GridView>
                                </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <asp:Button ID="btnShowPopup" runat="server" style="display:none" />
            <asp:Button ID="btnShowPopup2" runat="server" style="display:none" />
            
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground" CancelControlID="ImageButton2" PopupControlID="pnlPopProgram" TargetControlID="btnShowPopup" BehaviorID="_content_ModalPopupExtender1" DynamicServicePath="">
                                     </ajaxToolkit:ModalPopupExtender>
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground" CancelControlID="ImageButton1" PopupControlID="pnlPopupButiran" TargetControlID="btnShowPopup2" BehaviorID="_content_ModalPopupExtender1" DynamicServicePath="">
                                     </ajaxToolkit:ModalPopupExtender>
            
            
            
            <asp:Button ID = "btnOpen" runat = "server" Text = ""  style="display:none" />
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3"  runat="server" BackgroundCssClass="modalBackground" 
            CancelControlID="lbNo" PopupControlID="pnlpopupHantar" TargetControlID="btnOpen" 
            > </ajaxToolkit:ModalPopupExtender>
        
            <asp:Panel ID="pnlpopupHantar" runat="server" BackColor="White">
               <table style="border: Solid 3px #D5AA00; width: 100%; height: 100%; " >
                    <tr style="background: linear-gradient(to bottom, #fceabb 0%,#fccd4d 50%,#f8b500 51%,#fbdf93 100%);">
                        <td style="height: 10%;  font-weight: bold; text-align:center;" colspan="2" >
                            &nbsp;</td>
                        
                    </tr>
                   
                   <tr style="vertical-align:top;">
                        <td style="height: 10%;  font-weight: bold; text-align:center;" colspan="2" >
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

    </ContentTemplate> </asp:UpdatePanel>
</asp:Content>
