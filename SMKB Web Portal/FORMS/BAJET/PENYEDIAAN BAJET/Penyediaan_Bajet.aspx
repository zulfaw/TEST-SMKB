<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Penyediaan_Bajet.aspx.vb" Inherits="SMKB_Web_Portal.Penyediaan_Bajet" EnableEventValidation="false"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>
    <script type="text/javascript">
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

        .row{
      width :70%;
  }


                /*@media (max-width: 1350px) {
  .panel-body{
      width :100%;
  }*/
}


    </style>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
           
             <div class="row">
          
                 <div class="col-sm-12" style="margin-bottom:10px;"><label class="control-label" for="">
                    Bajet tahun :</label> &nbsp; <asp:TextBox ID="txtYear" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox></div>
                       
       <div class="panel panel-default" >
            <div class="panel-heading"><h4 class="panel-title">Senarai Permohonan Bajet PTj</h4></div>
            <div class="panel-body">
             <div class="col-sm-12" style="margin-bottom:10px;padding-left:0px;">
                 <label class="control-label" for="">PTj :</label> &nbsp; 
                 <asp:DropDownList ID="ddlPTj" runat="server" AutoPostBack="True" CssClass="form-control" Width="50%"></asp:DropDownList>
             </div>  
                <div style="text-align:left;margin-top:10px;"> 
            <asp:Label runat="server" Text="Jumlah Rekod : " /><span style="font-weight :bold;"><%= TotalRec %> </span> 
        </div>     
            <asp:GridView ID="gvPermohonan" runat="server" AllowSorting="True" AutoGenerateColumns="False" 
                              BorderColor="#999999" BorderStyle="Double" BorderWidth="1px" 
                              CssClass="table table-striped table-bordered table-hover" 
                              EmptyDataText=" " Font-Size="8pt" ShowFooter="True" 
                              ShowHeaderWhenEmpty="True" Width="100%">
					<columns>
					<asp:TemplateField HeaderText = "Bil">
        <ItemTemplate>
            <asp:Label ID="lblBil" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
        </ItemTemplate>
                                      <ItemStyle Width="2%" />
    </asp:TemplateField>
					<asp:BoundField HeaderText="No Permohonan PTj" DataField="BG21_NoMohonPTj" SortExpression="NoPermohonan" ReadOnly="true">
						<ItemStyle Width="15%" />
					</asp:BoundField>
					    <asp:BoundField DataField="KodPTj" HeaderText="KodPTj" Visible="False" >
					    <ItemStyle Width="5%" />
                        </asp:BoundField>
					<asp:BoundField HeaderText="PTj" DataField="Ptj" SortExpression="Ptj" ReadOnly="true">
						<ItemStyle Width="20%" />
					</asp:BoundField>
					<asp:BoundField HeaderText="Tarikh Hantar" DataField="BG21_TarikhHantar" SortExpression="TarikhHantar">
						<ItemStyle Width="20%" />
					</asp:BoundField>
                    <asp:BoundField HeaderText="Anggaran Perbelanjaan (RM)" DataField="BG21_AmaunMohon" SortExpression="AngBelanja">
						<ItemStyle Width="20%" />
					</asp:BoundField>
                                                     
					
				        <asp:BoundField DataField="ButiranStat" HeaderText="Status Permohonan" >                                                    					
				        <ItemStyle Width="20%" />
                        </asp:BoundField>
                                                     
					<asp:TemplateField>
                        <%--<EditItemTemplate>
                            <asp:ImageButton ID="btnSave1" runat="server" CommandName="Update" Height="20px" ImageUrl="~/Images/Save_48x48.png" 
                                ToolTip="Simpan" Width="15px" OnItemCommand="gvButiranModul_ItemCommand" />
                                &nbsp;&nbsp;
                            <asp:ImageButton ID="btnCancel" runat="server" CommandName="Cancel" Height="20px" ImageUrl="~/Images/Cancel16x16.png" 
                                    ToolTip="Batal" Width="15px" OnItemCommand="gvButiranModul_ItemCommand" />
                                &nbsp;&nbsp;
                        </EditItemTemplate>--%>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CssClass="btn-xs" ToolTip="Pilih Kemaskini">
                                <i class="fa fa-hand-o-left fa-lg"></i>
                            </asp:LinkButton>
                            
                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CssClass="btn-xs" ToolTip="Padam"
                                OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');">
                                <i class="fa fa-trash-o fa-lg"></i>
                            </asp:LinkButton>
                            <%--<asp:ImageButton ID="btnEdit" runat="server" CommandName="Edit" Height="20px" ImageUrl="~/Images/Edit_48.png" 
                                ToolTip="Kemas Kini" Width="15px" OnItemCommand="gvButiranModul_ItemCommand" />
                            &nbsp;&nbsp;
				            <asp:ImageButton ID="btnDelete1" runat="server" CommandName="Delete"  ImageUrl="~/Images/Delete_32x32.png" 
					            ToolTip="Padam" Width="15px" Height="18px" OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');" />--%>
			            </ItemTemplate>
			            <ItemStyle Width="5%" HorizontalAlign="Center" />
			        </asp:TemplateField>
				</columns>
			</asp:GridView>

                 
            </div>
            </div> 
               
                </div>
               
            <div class="row">
        <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" CssClass="tabCtrl" Height="850px" Width="100%">                                    
                <ajaxToolkit:TabPanel ID="tabABM" runat="Server" HeaderText ="Anggaran Belanja Mengurus (ABM)">
                    <ContentTemplate>
               
                        <%--<asp:GridView ID="gvAbmMaster" runat="server"
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
                                                <asp:Label runat="server" CssClass="" Text='<%# Session("lblPeruntukanPrev") %>' />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("PeruntukanPrev") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label runat="server" Text='<%# Session("lblPerbelanjaanPrev") %>' />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("PerbelanjaanPrev") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="10%"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label runat="server" Text='<%# Session("lblAnggaranSyorNow") %>' />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("AnggaranSyorNow") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="10%"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label runat="server" Text='<%# Session("lblAnggaranMintaNext") %>' />
                                            </HeaderTemplate>
                                            <ItemTemplate >
                                                <asp:Label runat="server" Text='<%# Eval("AnggaranMintaNext") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" BackColor="#E1E1E1" ForeColor="#0033CC"  Width="10%"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label runat="server" Text='<%# Session("lblAnggaranSyorNext") %>' />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("AnggaranSyorNext") %>' />                                                                                               
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="10%"/>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbDetail0" runat="server" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="btn-xs btn-primary" OnClick="lnkView_Click" ToolTip="Program">
                                <i class="fa fa-ellipsis-h fa-lg"></i>
                            </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="3%" />
                                        </asp:TemplateField>

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

                        </asp:GridView>--%>
     
                        <br />
                        <br />
     
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>

                    <ajaxToolkit:TabPanel ID="tabKeseluruhan" runat="Server" HeaderText ="Senarai Permohonan">
                    <ContentTemplate>
                                                 
                         <div style="text-align:left;margin-top:10px;"> 
            <asp:Label runat="server" Text="Jumlah Rekod : " /><span style="font-weight :bold;"><%=Session("TotalRec")%> </span> 
        </div>
                          
                          <%--<asp:GridView ID="gvPermohonan" runat="server" 
                              AllowSorting="True" AutoGenerateColumns="False" 
                              BorderColor="#999999" BorderStyle="Double" BorderWidth="1px" 
                              CssClass="table table-striped table-bordered table-hover" 
                              EmptyDataText=" " Font-Size="8pt" ShowFooter="True" 
                              ShowHeaderWhenEmpty="True" Width="100%">
                              <Columns>
                                  <asp:TemplateField HeaderText = "Bil">
        <ItemTemplate>
            <asp:Label ID="lblBil" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
        </ItemTemplate>
                                      <ItemStyle Width="30px" />
    </asp:TemplateField>
                                  <asp:BoundField DataField="NoMohon" HeaderText="No. Permohonan" SortExpression="NoPermohonan" />
                                  <asp:BoundField DataField="Bahagian" HeaderText="Bahagian" />
                                  <asp:BoundField DataField="Unit" HeaderText="Unit" />
                                  <asp:BoundField DataField="Program" HeaderText="Program/Aktiviti" />
                                  <asp:BoundField DataField="Anggaran" HeaderText="Anggaran Perbelanjaan (RM)">
                                  <ItemStyle BackColor="#E1E1E1" Font-Bold="False" ForeColor="#0033CC" HorizontalAlign="Right" />
                                  </asp:BoundField>
                                  <asp:BoundField DataField="Justifikasi" HeaderText="Justifikasi" />
                                  <asp:BoundField DataField="TarikhMohon" HeaderText="Tarikh Mohon" />
                                  <asp:BoundField DataField="StatusDok" HeaderText="Status Permohonan" />
                                  <asp:TemplateField>                        
                        <ItemTemplate>
                            <asp:LinkButton ID="lbDetail" runat="server" CommandName="Select" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="btn-xs btn-primary" ToolTip="Maklumat Lanjut">
                                <i class="fa fa-ellipsis-h fa-lg"></i>
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="3%" HorizontalAlign="Center"/>
                    </asp:TemplateField>
                              </Columns>
                              <SelectedRowStyle BackColor="#FFFFAA" Font-Bold="True"/>
                          </asp:GridView>--%>
                       
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>

                </ajaxToolkit:TabContainer>
      </div>


            </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
