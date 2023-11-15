<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Permohonan_Bajet.aspx.vb" Inherits="SMKB_Web_Portal.Permohonan_Bajet" EnableEventValidation ="False"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>
    <link rel="stylesheet" type="text/css" href="../../../Content/Site.css">

<style type="text/css">
    .tooltip
    {
        position: absolute;
        top: 0;
        left: 0;
        z-index: 3;
        display: none;
        background-color: #FB66AA;
        color: White;
        padding: 5px;
        font-size: 10pt;
        font-family: Arial;
    }
    td
    {
        cursor: pointer;
    }
</style>
<style type="text/css"> .hiddencol { display: none; } </style>  

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">        
    <ContentTemplate>
         <div class="col-sm-9 col-md-6 col-lg-8">
         
        <div class="row" style="width :100%;">  
        <div class="panel panel-default" style="width :100%;">
        <div class="panel-heading">
            <h3 class="panel-title">Senarai Permohonan Bajet</h3>
        </div>
        <div class="panel-body" style="overflow-x:auto">
            <label class="control-label" for="">
                <div>
                   <table style="width:100%">
                       <tr>
                           <td style="width: 121px">
                               <label class="control-label" for="">
                               Carian Status :</label></td>
                           <td>
                               <label class="control-label" for="">
                               <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" CssClass="form-control" Width="35%">
                               </asp:DropDownList>
                               &nbsp;&nbsp;&nbsp;&nbsp; Tahun Bajet :
                               <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="True" CssClass="form-control" Width="15%">
                                   <asp:ListItem Selected="true" Text="2021" Value="0" />
                                   <asp:ListItem Text="2020" />
                               </asp:DropDownList>
                               </label>
                           </td>
                           <td colspan="2">
                               &nbsp;</td>
                           <td style="text-align:right">
                               &nbsp;</td>
                       </tr>
                       <tr>
                           <td style="width: 121px; height: 23px;">
                               <label class="control-label" for="">
                               Kumpulan Wang :</label></td>
                           <td colspan="4" style="height: 23px">
                               <asp:DropDownList ID="ddlKW" runat="server" AutoPostBack="True" CssClass="form-control" Width="40%">                               
                               </asp:DropDownList>
                           </td>
                       </tr>
                       <tr>
                           <td style="width: 121px">
                               <label class="control-label" for="">
                               Kod Operasi :</label></td>
                           <td colspan="2">
                               <asp:DropDownList ID="ddlKodOperasi" runat="server" AutoPostBack="True" CssClass="form-control" Width="30%">
                               </asp:DropDownList>
                           </td>
                           <td colspan="2" style="text-align:right">
                               <label class="control-label" for="">
                               <asp:LinkButton ID="lbtnMohonBaru" runat="server" CssClass="btn btn-info">
                        <i class="fa fa-plus fa-lg"></i>&nbsp;&nbsp;&nbsp;Mohon Baru
                    </asp:LinkButton>
                               </label>
                           </td>
                       </tr>
                   </table>
                </div>
                </label>
            
            <br />

            <asp:GridView ID="gvMohonBajet" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="false" EmptyDataText=" "
                cssclass="table table-striped table-bordered table-hover" Width="100%" BorderStyle="Solid" ShowFooter="true" OnDataBound="OnRowDataBoundxx">
                    <columns>
                <asp:TemplateField HeaderStyle-CssClass="centerAlign">
                        <HeaderTemplate>
                            <asp:CheckBox ID="checkAll" runat="server" text="Hantar" onclick = "checkAll(this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" onclick = "Check_Click(this)" />
                        </ItemTemplate>
                        <ItemStyle Width="2%" HorizontalAlign="Center"/>
                    </asp:TemplateField>

                    <asp:BoundField HeaderText="Kod KW" DataField="KodKW" SortExpression="KodKW" ReadOnly="true" HeaderStyle-CssClass="centerAlign">
                        <ItemStyle Width="5%" HorizontalAlign="Center"/>
                    </asp:BoundField>
                    
                    <asp:BoundField HeaderText="Kod KO" DataField="KodOperasi" SortExpression="KodOperasi" ReadOnly="true" HeaderStyle-CssClass="centerAlign">
                        <ItemStyle Width="5%" HorizontalAlign="Center"/>
                    </asp:BoundField>

                <asp:TemplateField  HeaderText="Program / Aktiviti">
                 <ItemTemplate>
                      <asp:Label ID="lblProgram" runat="server"  class="gridViewToolTip"
                                Text='<%# Limit(Eval("BG20_Program"), 30) %>' >
                      </asp:Label>
                 </ItemTemplate>
                    <ItemStyle Width="30%" />
                </asp:TemplateField>

                <asp:TemplateField  HeaderText="Justifikasi">
                 <ItemTemplate>
                      <asp:Label ID="lblDescription" runat="server"  class="gridViewToolTip"
                                Text='<%# Limit(Eval("BG20_Justifikasi"), 30) %>' >
                      </asp:Label>
                 </ItemTemplate>
                    <ItemStyle Width="30%" />
                </asp:TemplateField> 
                                 <asp:BoundField HeaderText="NoMohon" DataField="BG20_NoMohon" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" > </asp:BoundField>
                  <asp:BoundField HeaderText="Anggaran Permohonan (RM)" DataField="AngJumBesar" SortExpression="AngJumBesar" HeaderStyle-CssClass="centerAlign" DataFormatString="{0:N}">
                        <ItemStyle Width="10%" HorizontalAlign="Right"/>                       
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Status" DataField="Status" SortExpression="Status" HeaderStyle-CssClass="centerAlign">
                        <ItemStyle Width="20%" />
                    </asp:BoundField>
                    <%--<asp:buttonfield buttontype="Button" commandname="Select" text="Info" />--%>
                                 
                    <asp:TemplateField>                        
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnDetail" runat="server" CommandName="Select" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="btn-xs btn-primary" ToolTip="Maklumat Lanjut">
                                <i class="fa fa-ellipsis-h fa-lg"></i>
                            </asp:LinkButton>
                        </ItemTemplate>                                                                                                                                         
                        <ItemStyle Width="3%" HorizontalAlign="Center"/>
                    </asp:TemplateField>
                </columns>
            </asp:GridView>
            <div style="width:100%">Penyokong: 
                <asp:DropDownList ID="ddlPenyokong"  CssClass="form-control" runat="server" Width="50%">
                </asp:DropDownList>
            </div>
            <br />
                <div style="text-align:center">
                    <%--<asp:Button ID="btnMohonBaru" runat="server" Text="Mohon Baru" CssClass="btn" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnHantar" text="Hantar" runat="server" CssClass="btn" />--%>
                    &nbsp;
                    <asp:LinkButton ID="lbtnHantar" runat="server" CssClass="btn btn-info">
                        <i class="fa fa-paper-plane-o fa-lg"></i>&nbsp;&nbsp;&nbsp;Hantar
                    </asp:LinkButton>
                                      
                    
                </div>
            </div>
            </div> 
            
                
            <br /><br />
    
        </div>  
    
    </div>

     <asp:Button ID = "btnOpen" runat = "server" Text = ""  style="display:none" />
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground" 
            CancelControlID="lbNo" PopupControlID="pnlpopupHantar" TargetControlID="btnOpen" >
        </ajaxToolkit:ModalPopupExtender>        
            <asp:Panel ID="pnlpopupHantar" runat="server" BackColor="White" Width="70%" Style="display: none">
               <table style="border: Solid 3px #D5AA00; width: 100%; height: 100%; " >
                    <tr style="background: linear-gradient(to bottom, #fceabb 0%,#fccd4d 50%,#f8b500 51%,#fbdf93 100%);">
                        <td style="height: 10%;  font-weight: bold; text-align:center;" colspan="2" >
                            Maklumat Lanjut
                        </td>                        
                    </tr>
                    <tr style="vertical-align:top;">
                        <td colspan="2">
                           <div style="margin-bottom:10px;margin-top:10px;margin-left:10px;margin-right:10px;">
               <asp:GridView ID="gvHantar" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="False" AllowPaging="false" EmptyDataText=" "
                cssclass="table table-striped table-bordered table-hover" Width="100%" HeaderStyle-BackColor="#6699FF" Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" ShowFooter="false">
                    <columns>
                    
                    <asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" SortExpression="Bil" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="No Permohonan" DataField="NoMohon" SortExpression="NoPermohonan" ReadOnly="true" HeaderStyle-CssClass="centerAlign">
                        <ItemStyle Width="5%" HorizontalAlign="Center"/>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Program/ Aktiviti" DataField="Program" SortExpression="Program" ReadOnly="true" HeaderStyle-CssClass="centerAlign">
                        <ItemStyle Width="30%" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Tarikh Mohon" DataField="Tarikh" SortExpression="TarikhMohon" HeaderStyle-CssClass="centerAlign">
                        <ItemStyle Width="5%" HorizontalAlign="Center"/>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Anggaran Perbelanjaan (RM)" DataField="Jumlah" SortExpression="AngJumBesar" HeaderStyle-CssClass="centerAlign">
                        <ItemStyle Width="10%" HorizontalAlign="Right"/>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Status" DataField="Status" SortExpression="Status" HeaderStyle-CssClass="centerAlign">
                        <ItemStyle Width="20%" />
                    </asp:BoundField>                    
                </columns>
            </asp:GridView>
                          </div>
                        </td>
                    </tr>
                   <tr style="vertical-align:top;">
                        <td style="height: 10%;  font-weight: bold; text-align:center;" colspan="2" >
                            <div style="margin-bottom:10px;margin-top:10px;margin-left:10px;margin-right:10px;">
                            Adakah anda setuju menghantar permohonan bajet seperti di atas?
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
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lbtnHantar" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    
         
</asp:Content>
