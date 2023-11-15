<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Cadangan_Agihan_Defend.aspx.vb" Inherits="SMKB_Web_Portal.Cadangan_Agihan_Defend" EnableEventValidation="False" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">  

     <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>
    <link rel="stylesheet" type="text/css" href="../../../Content/Site.css">
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

            <div>
                <table class="auto-style1" >
                    <tr style="height:25px">
                        <td class="auto-style3">
                            <label class="control-label" for="">
                            Bajet tahun</label></td>
                        <td style="width: 80%">
                            <asp:TextBox ID="txtYear" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">
                            <label class="control-label" for="">
                            Kumpulan Wang</label></td>
                        <td class="auto-style2">
                            <label class="control-label" for="">
                            <asp:DropDownList ID="ddlKW" runat="server" CssClass="form-control" Width="50%">
                                <asp:ListItem Text="- KESELURUHAN -" Value="1"></asp:ListItem>
                                <asp:ListItem Text="01 - KUMPULAN WANG MENGURUS" Value="1"></asp:ListItem>
                                <asp:ListItem Text="07 - KUMPULAN WANG PENDAPATAN" Value="1"></asp:ListItem>
                            </asp:DropDownList>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">
                            <label class="control-label" for="">
                            Kod Operasi</label></td>
                        <td class="auto-style2">
                            <label class="control-label" for="">
                            <asp:DropDownList ID="ddlKodOperasi" runat="server" CssClass="form-control" Width="50%">
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
  
        <br />
    
     <div>

         <asp:GridView ID="gvAbmMaster" runat="server" AllowSorting="True" AutoGenerateColumns="False" BorderColor="#999999" BorderStyle="Double" BorderWidth="1px" CssClass="table table-striped table-bordered table-hover" DataKeyNames="KodPTJ" EmptyDataText=" " Font-Size="8pt" ShowFooter="True" ShowHeaderWhenEmpty="True" Width="100%">
             <Columns>

                 <asp:TemplateField HeaderText="Bil">
                     <ItemTemplate>
                         <asp:Label ID="lblBil" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />
                     </ItemTemplate>
                     <ItemStyle Width="1%" />
                 </asp:TemplateField>
                 <asp:BoundField DataField="Butiran" HeaderText="PTj">
                 <ItemStyle Width="30%" />
                 </asp:BoundField>
                 <asp:BoundField DataField="KodPTJ" HeaderText="Kod PTj">
                 <ItemStyle Width="5%"  HorizontalAlign="Center"/>
                 </asp:BoundField>
                 <asp:BoundField DataField="N1" HeaderText="10000">
                 <ItemStyle Width="7%"  HorizontalAlign="Right"/>
                 </asp:BoundField>
                 <asp:BoundField DataField="N2" HeaderText="20000">
                 <ItemStyle Width="7%"  HorizontalAlign="Right"/>
                 </asp:BoundField>
                 <asp:BoundField DataField="N3" HeaderText="30000">
                 <ItemStyle Width="7%"  HorizontalAlign="Right"/>
                 </asp:BoundField>
                  <asp:BoundField DataField="N4" HeaderText="40000">
                 <ItemStyle Width="7%"  HorizontalAlign="Right"/>
                 </asp:BoundField>
                 <asp:BoundField DataField="N4" HeaderText="50000">
                 <ItemStyle Width="7%"  HorizontalAlign="Right"/>
                 </asp:BoundField>
                 <asp:BoundField DataField="Total" HeaderText="Cadangan Agihan 2021">
                 <ItemStyle Width="7%"  HorizontalAlign="Right"/>
                 </asp:BoundField>

                <asp:TemplateField>                        
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnDetail" runat="server" CommandName="Select" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="btn-xs btn-primary" ToolTip="Maklumat Lanjut">
                            <i class="fa fa-ellipsis-h fa-lg"></i>
                        </asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="3%" HorizontalAlign="Center"/>
                </asp:TemplateField>

                 <asp:TemplateField HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="2%">
                        <HeaderTemplate>
                            <asp:CheckBox ID="checkAll" runat="server" text="Hantar" onclick = "checkAll(this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" onclick = "Check_Click(this)" />
                        </ItemTemplate>
                 </asp:TemplateField>
             </Columns>
             <SelectedRowStyle BackColor="#FFFFAA" Font-Bold="True" />
         </asp:GridView>

     </div>

        <div style="text-align:center;margin-top:10px;"> 
            
            <%--<asp:LinkButton ID="lbtnHantar" runat="server" CssClass="btn btn-info" data-toggle="tooltip" title="Keseluruhan permohonan bajet akan dihantar ke pejabat Bendahari." >
                        <i class="fa fa-paper-plane-o fa-lg"></i>&nbsp;&nbsp;&nbsp;Hantar
                    </asp:LinkButton> &nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="lbtnKemaskini" runat="server" CssClass="btn btn-info" data-toggle="tooltip" title="Keseluruhan permohonan bajet akan dipulangkan ke penyedia bajet untuk dikemas kini.">
                        <i class="fa fa-pencil-square-o fa-lg"></i>&nbsp;&nbsp;&nbsp;Kemas Kini
                    </asp:LinkButton>--%>

            <asp:Button ID="btnHantar" runat="server" Text="Hantar"  CssClass="btn btn-info"/>
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnkemaskini" runat="server" Text="Kemas Kini"  CssClass="btn btn-info" Visible="false"/>


        </div>
            
        
       </div>     

            

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
        
    </ContentTemplate> </asp:UpdatePanel>
</asp:Content>
