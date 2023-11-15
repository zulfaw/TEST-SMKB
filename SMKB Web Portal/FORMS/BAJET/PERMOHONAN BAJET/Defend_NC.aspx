<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Defend_NC.aspx.vb" Inherits="SMKB_Web_Portal.Defend_NC" EnableEventValidation="False" %>
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
                <div class="panel panel-default" style="border-color :#b94a48;">
                  <div class="panel-heading">
                    <h4 class="panel-title">
                      <a data-toggle="collapse" href="#collapse1"><span class="fa fa-bell-o fa-lg"></span>&nbsp&nbsp Notifikasi
                          <i class="fa fa-chevron-down" aria-hidden="true"></i>
                      </a>
                        
                    </h4>
                  </div>
                  <div id="collapse1" class="panel-collapse collapse">
                    <div class="panel-body" style="width :100%;" >                                                           
                                <%= strNotice %>                                                                   
                    </div>
                    
                  </div>
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
        <br />
            <div>
                <table class="table table table-borderless" style="width:80%;" >
                    <tr style="height:25px">
                        <td class="auto-style3">
                            <label class="control-label" for="">
                            Bajet tahun</label></td>
                        <td style="width: 80%">
                            <asp:TextBox ID="txtYear" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="height:25px">
                        <td class="auto-style3">
                            <label class="control-label" for="">
                            Kumpulan Wang</label></td>
                        <td style="width: 80%">
                            <label class="control-label" for="">
                            <asp:DropDownList ID="ddlKW" runat="server" CssClass="form-control" Width="30%">
                                <asp:ListItem Text="- KESELURUHAN -" Value="1"></asp:ListItem>
                                <asp:ListItem Text="01 - KUMPULAN WANG MENGURUS" Value="1"></asp:ListItem>
                                <asp:ListItem Text="07 - KUMPULAN WANG PENDAPATAN" Value="1"></asp:ListItem>
                            </asp:DropDownList>
                            </label>
                        </td>
                    </tr>
                    <tr style="height:25px">
                        <td class="auto-style3">
                            <label class="control-label" for="">
                            Kod Operasi</label></td>
                        <td style="width: 80%">
                            <label class="control-label" for="">
                            <asp:DropDownList ID="ddlKodOperasi" runat="server" CssClass="form-control" Width="40%">
                                <asp:ListItem Text="- KESELURUHAN -" Value="1"></asp:ListItem>
                                <asp:ListItem Text="00 - DEFAULT" Value="1"></asp:ListItem>
                                <asp:ListItem Text="01 - OPERASI" Value="1"></asp:ListItem>
                                <asp:ListItem Text="02 - KOMITED" Value="1"></asp:ListItem>
                            </asp:DropDownList>
                            </label>
                        </td>
                    </tr>
                </table>
            </div>
  

    
        <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" CssClass="tabCtrl" Height="850px" Width="100%">                                    
                <ajaxToolkit:TabPanel ID="tabABM" runat="server" HeaderText ="Anggaran Belanja Mengurus (ABM)">
                    <HeaderTemplate>
                        Anggaran Belanja Mengurus (ABM)
                    </HeaderTemplate>
                    <ContentTemplate>

                        <asp:GridView ID="gvAbmMaster" runat="server" AllowSorting="True" AutoGenerateColumns="False" BorderColor="#999999" BorderStyle="Double" BorderWidth="1px" CssClass="table table-striped table-bordered table-hover" DataKeyNames="KodPTJ" EmptyDataText=" " Font-Size="8pt" ShowFooter="True" ShowHeaderWhenEmpty="True" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="Bil">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBil0" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />
                                    </ItemTemplate>
                                    <ItemStyle Width="1%" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Butiran" HeaderText="PTj">
                                <ItemStyle Width="30%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="KodPTJ" HeaderText="Kod PTj">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="N1" HeaderText="10000">
                                <ItemStyle HorizontalAlign="Right" Width="7%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="N2" HeaderText="20000">
                                <ItemStyle HorizontalAlign="Right" Width="7%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="N3" HeaderText="30000">
                                <ItemStyle HorizontalAlign="Right" Width="7%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="N4" HeaderText="40000">
                                <ItemStyle HorizontalAlign="Right" Width="7%" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="N4" HeaderText="50000">
                                <ItemStyle HorizontalAlign="Right" Width="7%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Total" HeaderText="Permohonan 2021">
                                <ItemStyle HorizontalAlign="Right" Width="7%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Total" HeaderText="Cadangan Agihan 2021">
                                <ItemStyle HorizontalAlign="Right" Width="7%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Total" HeaderText="Agihan NC 2021">
                                <ItemStyle HorizontalAlign="Right" Width="7%" />
                                </asp:BoundField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnDetail" runat="server" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CommandName="Select" CssClass="btn-xs btn-primary" ToolTip="Maklumat Lanjut">
                            <i class="fa fa-ellipsis-h fa-lg"></i>
                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="3%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="2%">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll0" runat="server" onclick="checkAll(this);" text="Hantar" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox2" runat="server" onclick="Check_Click(this)" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <SelectedRowStyle BackColor="#FFFFAA" Font-Bold="True" />
                        </asp:GridView>
     
                        <br />
                        <div style="text-align:center">
                            <asp:LinkButton ID="lbtnHantar0" runat="server" CssClass="btn btn-info">
                        <i class="fa fa-paper-plane-o fa-lg"></i>&nbsp;&nbsp;&nbsp;Hantar
                    </asp:LinkButton>
                        </div>
     
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>

                    <ajaxToolkit:TabPanel ID="tabKeseluruhan" runat="server" HeaderText ="Senarai Permohonan">
                        <HeaderTemplate>
                            Senarai Permohonan
                        </HeaderTemplate>
                    <ContentTemplate>                         

                                    <div>
                <table class="table table table-borderless" style="width:80%;" >
                    <tr style="height:25px">
                        <td class="auto-style3">
                            PTj</td>
                        <td style="width: 80%">
                            <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" CssClass="form-control" Height="21px" Width="80%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="height:25px">
                        <td class="auto-style3">
                            <label class="control-label" for="">
                            Bahagian:</label></td>
                        <td style="width: 80%">
                            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" CssClass="form-control" Height="21px" Width="80%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="height:25px">
                        <td class="auto-style3">
                            <label class="control-label" for="">
                            Unit:</label></td>
                        <td style="width: 80%">
                            <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" CssClass="form-control" Height="21px" Width="80%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>

                         <div style="text-align:left;margin-top:10px;"> 
            <asp:Label runat="server" Text="Jumlah Rekod : " /><span style="font-weight :bold;"><%= TotalRec %> </span> 
                             <asp:GridView ID="gvStatusPermohonan" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BorderColor="#333333" BorderStyle="Solid" CssClass="table table-striped table-bordered table-hover" emptydatatext="no data" Font-Size="8pt" PageSize="5" ShowFooter="True" ShowHeaderWhenEmpty="True" Width="100%">
                                 <columns>
 
                                     <asp:BoundField DataField="Bil" HeaderText="Bil" ReadOnly="True" SortExpression="Bil">
                                     <HeaderStyle CssClass="centerAlign" />
                                     <ItemStyle HorizontalAlign="Center" Width="3%" />
                                     </asp:BoundField>
                                     <asp:BoundField DataField="NoPermohonan" HeaderText="No Permohonan" ReadOnly="True" SortExpression="NoPermohonan">
                                     <HeaderStyle CssClass="centerAlign" />
                                     <ItemStyle HorizontalAlign="Center" Width="5%" />
                                     </asp:BoundField>
                                     <asp:BoundField DataField="Program" HeaderText="Program" ReadOnly="True" SortExpression="Program">
                                     <HeaderStyle CssClass="centerAlign" />
                                     <ItemStyle Width="30%" />
                                     </asp:BoundField>
                                     <asp:BoundField DataField="KodOperasi" HeaderText="Kod Operasi" ReadOnly="True" SortExpression="KodOperasi">
                                     <HeaderStyle CssClass="centerAlign" />
                                     <ItemStyle Width="5%" />
                                     </asp:BoundField>
                                     <asp:BoundField DataField="Anggaran" HeaderText="Anggaran Perbelanjaan (RM)" ReadOnly="True" SortExpression="Anggaran">
                                     <HeaderStyle CssClass="centerAlign" />
                                     <ItemStyle HorizontalAlign="Right" Width="3%" />
                                     </asp:BoundField>
                                     <asp:BoundField DataField="Status" HeaderText="Status" ReadOnly="True" SortExpression="Status">
                                     <HeaderStyle CssClass="centerAlign" />
                                     <ItemStyle Width="10%" />
                                     </asp:BoundField>
                                     <asp:TemplateField>
                                         <ItemTemplate>
                                             <asp:LinkButton ID="lbDetail" runat="server" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CommandName="Select" CssClass="btn-xs btn-primary" ToolTip="Maklumat Lanjut">
                                <i class="fa fa-ellipsis-h fa-lg"></i>
                            </asp:LinkButton>
                                         </ItemTemplate>
                                         <ItemStyle HorizontalAlign="Center" Width="3%" />
                                     </asp:TemplateField>
                                 </columns>
                                 <HeaderStyle BackColor="#6699FF" />
                                 <RowStyle Height="5px" />
                                 <SelectedRowStyle ForeColor="Blue" />
                             </asp:GridView>
        </div>
                                  <div class="panel panel-default" style="width:90%" >
            </div>
                                    <br />
                <div style="text-align:center">
                                      
                    
                </div>    
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>

                </ajaxToolkit:TabContainer>
     <div></div>

        <div style="text-align:center;margin-top:10px;"> 
            
            <%--<asp:LinkButton ID="lbtnHantar" runat="server" CssClass="btn btn-info" data-toggle="tooltip" title="Keseluruhan permohonan bajet akan dihantar ke pejabat Bendahari." >
                        <i class="fa fa-paper-plane-o fa-lg"></i>&nbsp;&nbsp;&nbsp;Hantar
                    </asp:LinkButton> &nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="lbtnKemaskini" runat="server" CssClass="btn btn-info" data-toggle="tooltip" title="Keseluruhan permohonan bajet akan dipulangkan ke penyedia bajet untuk dikemas kini.">
                        <i class="fa fa-pencil-square-o fa-lg"></i>&nbsp;&nbsp;&nbsp;Kemas Kini
                    </asp:LinkButton>--%>

            <asp:Button ID="btnHantar" runat="server" Text="Simpan"  CssClass="btn btn-info" Visible="false"/>
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
