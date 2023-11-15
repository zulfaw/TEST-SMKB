
<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Kelulusan_Ketua_PTJ.aspx.vb" Inherits="SMKB_Web_Portal.Kelulusan_Ketua_PTJ" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

	<h1>Kelulusan Ketua PTj</h1>

	<link rel="stylesheet" type="text/css" href="../../../Content/Site.css">

<script type="text/javascript">
	
	function Check_Click(objRef) {
		//Get the Row based on checkbox
		var row = objRef.parentNode.parentNode;
		

		//Get the reference of GridView
		var GridView = row.parentNode;

		//Get all input elements in Gridview
		var inputList = GridView.getElementsByTagName("input");

		for (var i = 0; i < inputList.length; i++) {
			//The First element is the Header Checkbox
			var headerCheckBox = inputList[0];

			//Based on all or none checkboxes
			//are checked check/uncheck Header Checkbox
			var checked = true;
			if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
				if (!inputList[i].checked) {
					checked = false;
					break;
				}
			}
		}
		headerCheckBox.checked = checked;
	};

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

	<asp:UpdatePanel ID="UpdatePanel1" runat="server">
		<ContentTemplate>
			
			<p></p>
			<div class ="row">
			Tahun: &nbsp;
			<asp:TextBox ID="txtTahun" runat="server" CssClass="form-control" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
			
                <br /><br />
            <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" CssClass="tabCtrl" Width="100%">                                    
				
				<ajaxToolkit:TabPanel ID="Tab1" runat="server" HeaderText ="Senarai Status Permohonan Perolehan">
					<ContentTemplate>
						<div class="panel panel-default" style="width:95%;">
				<div class="panel-heading">
					<h3 class="panel-title">
						Senarai Permohonan Perolehan
					</h3>
				</div>
				<div class="panel-body" style="overflow-x:auto">
						Status&nbsp;:&nbsp;&nbsp;           
						<asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" CssClass="form-control" ></asp:DropDownList>
						<br /><br />
						<asp:GridView ID="gvMohonPO" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" EmptyDataText=" Tiada rekod"
							cssclass="table table-striped table-bordered table-hover" Width="100%" BorderStyle="Solid" ShowFooter="True" DataKeyNames="PO01_NoMohonSem" Font-Size="8pt">
								<columns>                                 
								<asp:TemplateField HeaderText = "Bil" >
									<ItemTemplate>
										<%# Container.DataItemIndex + 1 %>
									</ItemTemplate>
									<HeaderStyle CssClass="centerAlign" />
									<ItemStyle Width="2%" />
								</asp:TemplateField> 
								<asp:BoundField HeaderText="No Perolehan" DataField="PO01_NoMohon" SortExpression="PO01_NoMohon">
									<HeaderStyle CssClass="centerAlign" />
									<ItemStyle Width="5%" HorizontalAlign="Center"/>
								</asp:BoundField>						            
								<asp:BoundField HeaderText="Tujuan" DataField="PO01_Tujuan" SortExpression="PO01_Tujuan">
									<HeaderStyle CssClass="centerAlign" />
									<ItemStyle Width="35%" />
								</asp:BoundField>
								<asp:BoundField HeaderText="Kategori" DataField="Kategori" SortExpression="Kategori">
									<HeaderStyle CssClass="centerAlign" />
									<ItemStyle Width="5%" />
								</asp:BoundField>
								<asp:BoundField HeaderText="Tarikh Mohon" DataField="PO01_TkhMohonSem" SortExpression="PO01_TkhMohonSem" DataFormatString="{0:dd/MM/yyyy}">
									<HeaderStyle CssClass="centerAlign" />
									<ItemStyle Width="5%" HorizontalAlign="Center"/>
								</asp:BoundField>
								<asp:BoundField HeaderText="Anggaran Perbelanjaan (RM)" DataField="AnggaranBelanja" DataFormatString="{0:N}" SortExpression="AnggaranBelanja">
									<HeaderStyle CssClass="centerAlign" />
									<ItemStyle Width="10%" HorizontalAlign="Right"/>                       
								</asp:BoundField>
								<asp:BoundField HeaderText="Status" DataField="Butiran" SortExpression="Butiran">
									<HeaderStyle CssClass="centerAlign" />
									<ItemStyle Width="20%" />
								</asp:BoundField>                                                                                             
								<asp:TemplateField>                        
								<ItemTemplate>
										<asp:LinkButton ID="lbDetail" runat="server" CommandName="Select" CssClass="btn-xs btn-primary" ToolTip="Maklumat Lanjut">
											<i class="fa fa-ellipsis-h fa-lg"></i>
										</asp:LinkButton>                                    
									</ItemTemplate>
									<ItemStyle Width="3%" HorizontalAlign="Center"/>
								</asp:TemplateField>
							</columns>
						</asp:GridView>
					</div>
							</div>
						
						</ContentTemplate>
					</ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="Tab2" runat="server" HeaderText ="Senarai Vot Permohonan Perolehan">
					<ContentTemplate>
                <div class="panel panel-default" style="width:95%;">
				<div class="panel-heading">
					<h3 class="panel-title">
						Senarai Permohonan Perolehan					
                    <%--<div class="panel-title pull-right" style="margin-top: 2px;margin-right: 4px;">
                     <i class="fas fa-info-circle fa-lg" aria-hidden="true" data-html="true" data-toggle="tooltip" data-placement="top" style="cursor:pointer;color:#ba2818;" 
                         title="Petunjuk : <br/><span style='color:#008000;'><i class='far fa-check-circle fa-lg'></i> </span>Selesai kelulusan</span>   <br/><span style='color:#969696;'><i class='far fa-check-circle fa-lg'></i> </span>Belum selesai kelulusan</span>">
                     </i>
                    </div>--%>
                        </h3>
				</div>
				<div class="panel-body" style="overflow-x:auto">
			
				   <asp:GridView ID="gvParentMohonPO" runat="server" AutoGenerateColumns="False" EmptyDataText=" Tiada rekod" CssClass="table table-striped table-bordered table-hover"
					   BorderStyle="Solid" ShowHeaderWhenEmpty="True" Width="95%" Font-Size="8pt"
						DataKeyNames="VotSebagai">
						<Columns>
							<asp:TemplateField>
								<ItemTemplate>								   
										<asp:Panel ID="pnlMaster" runat="server">
											<asp:Image ID="imgCollapsible" runat="server" ImageUrl="../../../Images/plus.png" Style="margin-right: 5px;" />
											<span style="font-weight:bold;display:none;"><%#Eval("VotSebagai")%></span>
										</asp:Panel>										
										<ajaxToolkit:CollapsiblePanelExtender ID="ctlCollapsiblePanel" runat="Server" AutoCollapse="False" AutoExpand="False" CollapseControlID="pnlMaster" Collapsed="True" CollapsedImage="../../../Images/plus.png" CollapsedSize="0" ExpandControlID="pnlMaster" ExpandDirection="Vertical" ExpandedImage="../../../Images/minus.png" ImageControlID="imgCollapsible" ScrollContents="false" TargetControlID="pnlChild"/>
								</ItemTemplate>
							    <ItemStyle HorizontalAlign="Center" Width="2%" />
							</asp:TemplateField>
							<asp:TemplateField HeaderText = "Bil">
									<ItemTemplate>
										<%# Container.DataItemIndex + 1 %>
									</ItemTemplate>
								    <HeaderStyle CssClass="centerAlign" />
                                    <ItemStyle HorizontalAlign="Right" Width="4%" />
								</asp:TemplateField>
							<asp:BoundField DataField="KodKw" HeaderText="KW" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="4%" />
							<asp:BoundField DataField="KodKo" HeaderText="KO" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" />
							<asp:BoundField DataField="KodPtj" HeaderText="PTJ" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" />
							<asp:BoundField DataField="KodKp" HeaderText="KP" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" />
							<asp:BoundField DataField="VotSebagai" HeaderText="Vot Sebagai" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%" />
							<asp:BoundField DataField="BakiSebenar" HeaderText="Baki Sebenar (RM)" DataFormatString="{0:n2}" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="12%" />
                            <asp:BoundField DataField="JumBelanjaSdhLulus" HeaderText="Jum. Ang. Blja. Sudah Lulus (RM)" DataFormatString="{0:n2}" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="12%" />
							<asp:BoundField DataField="BakiBayang" HeaderText="Baki Peruntukan Semasa (RM)" DataFormatString="{0:n2}" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="12%" />
							<asp:BoundField DataField="JumBelanjaBlmLulus" HeaderText="Jum. Ang. Blja. Belum Lulus (RM)" DataFormatString="{0:n2}" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="12%" />
							
							<%--<asp:BoundField DataField="JumTanggungan" HeaderText="Jum. Ang. Blja. Menjadi Tanggungan (RM)" DataFormatString="{0:n2}" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="12%" />--%>							
							
							<asp:TemplateField >
								<ItemTemplate>
									<tr>
										<td colspan="100">
                                            
								<asp:Panel ID="pnlChild" runat="server" Style="margin-left:20px;margin-right:20px; height:0px;overflow: auto;" Width="98%">
											
                                    <asp:GridView ID="gvChildMohonPO" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="false" EmptyDataText=" Tiada rekod"
								cssclass="table table-striped table-bordered table-hover" Width="100%" Font-Size="8pt" BorderStyle="Solid" ShowFooter="true" HeaderStyle-BackColor="#FECB18"
												OnRowCreated ="gvChildMohonPO_RowCreated" OnRowDataBound="gvChildMohonPO_RowDataBound" DataKeyNames="PO01_NoMohonSem">
									<columns>								    
									<asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Right">
										<ItemTemplate>
											<%# Container.DataItemIndex + 1 %>
										</ItemTemplate>
									</asp:TemplateField>
                                    <asp:TemplateField Visible=false ItemStyle-Width="1%">
								    <ItemTemplate>
									    <asp:Label id="lblNoPoSem" runat ="server" text='<%# Eval("PO01_NoMohonSem")%>' ></asp:Label>
								    </ItemTemplate>
								    </asp:TemplateField>
									<asp:BoundField HeaderText="No Perolehan" DataField="PO01_NoMohon" HeaderStyle-CssClass="centerAlign">
										<ItemStyle Width="5%" HorizontalAlign="Center"/>
									</asp:BoundField>						            
									<asp:BoundField HeaderText="Tujuan" DataField="PO01_Tujuan" HeaderStyle-CssClass="centerAlign">
										<ItemStyle Width="35%" />
									</asp:BoundField>
									<asp:BoundField HeaderText="Kategori" DataField="PO01_JenisBrg" HeaderStyle-CssClass="centerAlign">
										<ItemStyle Width="2%" HorizontalAlign="Center"/>
									</asp:BoundField>
									<asp:BoundField HeaderText="Tarikh Mohon" DataField="TkhMohonSem" HeaderStyle-CssClass="centerAlign">
										<ItemStyle Width="5%" HorizontalAlign="Center"/>
									</asp:BoundField>
										<asp:BoundField HeaderText="Status" DataField="Butiran" HeaderStyle-CssClass="centerAlign">
										<ItemStyle Width="10%" HorizontalAlign="Center"/>
									</asp:BoundField>
                                    <%--<asp:TemplateField HeaderText="">
                                        <ItemTemplate> 
                                          <asp:LinkButton ID="btnSelesai" runat="server">
                                              <a href="#" data-toggle="tooltip" title="Selesai kelulusan" style="cursor:default;">
                                                  <span style="color:#008000;"><i class="far fa-check-circle fa-lg"></i> </span></a>
                                          </asp:LinkButton>
                                            <asp:LinkButton ID="btnBelumSelesai" runat="server">
                                              <a href="#" data-toggle="tooltip" title="Belum selesai kelulusan" style="cursor:default;"> 
                                                  <span style="color:#969696;"><i class="far fa-check-circle fa-lg"></i> </span></a>
                                          </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

										<asp:BoundField HeaderText="KW" DataField="kodKw" HeaderStyle-CssClass="centerAlign">
										<ItemStyle Width="2%" HorizontalAlign="Center"/>
									</asp:BoundField>
										<asp:BoundField HeaderText="KO" DataField="KodKo" HeaderStyle-CssClass="centerAlign">
										<ItemStyle Width="2%" HorizontalAlign="Center" />
									</asp:BoundField>
									<asp:BoundField HeaderText="Anggaran Perbelanjaan (RM)" DataField="AngJumlah"  DataFormatString="{0:N}" HeaderStyle-CssClass="centerAlign">
										<ItemStyle Width="8%" HorizontalAlign="Right"/>                       
									</asp:BoundField>
																																
									<asp:TemplateField>                        
									<ItemTemplate  >
										<asp:LinkButton ID="lbDetail" runat="server" CssClass="btn-xs btn-primary" ToolTip="Maklumat Lanjut" OnClick="lbtnDetail_Click">
											<i class="fa fa-ellipsis-h fa-lg"></i>
										</asp:LinkButton>
										</ItemTemplate>
										<ItemStyle Width="2%" HorizontalAlign="Center"/>
									</asp:TemplateField>
								</columns>
							</asp:GridView>
                                    
									</asp:Panel>
							   
						</td>
					</tr>
								</ItemTemplate>
								<ItemStyle Width="1%" />
							</asp:TemplateField>

						</Columns>
					   <SelectedRowStyle ForeColor="Blue" Font-Bold="True" />
					</asp:GridView>
                    <div style="color:#787878;">
            <div style="margin-bottom:10px;">
                <div>
                <i class="fas fa-info-circle fa-lg"></i> <asp:Label ID="Label3" runat="server" Text="Petunjuk :" Font-Bold="True"></asp:Label> <br />
                </div>
                <div>
                <span style="color:cornflowerblue;"><i class="far fas fa-square-full fa-lg"></i> </span> - Dana mencukupi<br />
                <span style="color:yellow;"><i class="far fas fa-square-full fa-lg"></i> </span> - Dana tidak mencukupi<br />
                <%--<span style="color:black;"><i class="far fas fa-square-full fa-lg"></i> </span> - Memerlukan Kelulusan Ketua PTJ<br />--%>
            </div></div>
										
					</div>
				</div>
                </div>

                   

			
			
						
					</ContentTemplate>
					</ajaxToolkit:TabPanel>
					</ajaxToolkit:TabContainer>
					
				
			   
            </div>


		   
			
			
		</ContentTemplate>
	</asp:UpdatePanel>     
</asp:Content>
