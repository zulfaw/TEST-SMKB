<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Semakan_Bendahari_PTJ.aspx.vb" Inherits="SMKB_Web_Portal.Semakan_Bendahari_PTJ" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

	<h1>Semakan Bendahari</h1>

	<link rel="stylesheet" type="text/css" href="../../../Content/Site.css">

	<asp:UpdatePanel ID="UpdatePanel1" runat="server">

	<ContentTemplate>


			<div class="panel-group">
			<div class="panel panel-default">

				<div class="panel-heading">
					<h3 class="panel-title">
						Senarai Permohonan Perolehan
					</h3>
				</div>

				<div class="panel-body" style="overflow-x:auto">
					Carian Status&nbsp;:&nbsp;&nbsp;           
                    <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" CssClass="form-control" ></asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Tahun: &nbsp;<asp:TextBox ID="txtTahun" runat="server" CssClass="form-control" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
					<br /><br />
					<asp:GridView ID="gvMohonPO" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="false" EmptyDataText=" Tiada rekod"
							cssclass="table table-striped table-bordered table-hover" Width="100%" BorderStyle="Solid" ShowFooter="False" Font-Size="8pt" DataKeyNames="PO01_NoMohonSem">
								<columns>
								<asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" >
									<ItemTemplate>
										<%# Container.DataItemIndex + 1 %>
									</ItemTemplate>
								</asp:TemplateField> 
								<%--<asp:TemplateField Visible=false ItemStyle-Width="1%">
								<ItemTemplate>
									<asp:Label id="lblNoPoSem" runat ="server" text='<%# Eval("PO01_NoMohonSem")%>' ></asp:Label>
								</ItemTemplate>
								</asp:TemplateField>--%>
								<asp:BoundField HeaderText="No Perolehan" DataField="PO01_NoMohon" SortExpression="PO01_NoMohon" HeaderStyle-CssClass="centerAlign">
									<ItemStyle Width="5%" HorizontalAlign="Center"/>
								</asp:BoundField>						            
								<asp:BoundField HeaderText="Tujuan" DataField="PO01_Tujuan" SortExpression="PO01_Tujuan" HeaderStyle-CssClass="centerAlign">
									<ItemStyle Width="35%" />
								</asp:BoundField>
								
								<asp:BoundField HeaderText="Kategori" DataField="Kategori" SortExpression="Kategori" HeaderStyle-CssClass="centerAlign">
									<ItemStyle Width="5%" />
								</asp:BoundField>
								<asp:BoundField HeaderText="Tarikh Mohon" DataField="PO01_TkhMohonSem" SortExpression="PO01_TkhMohonSem" HeaderStyle-CssClass="centerAlign" DataFormatString="{0:dd/MM/yyyy}">
									<ItemStyle Width="5%" HorizontalAlign="Center"/>
								</asp:BoundField>
								<asp:BoundField HeaderText="Anggaran Perbelanjaan (RM)" DataField="AnggaranBelanja" SortExpression="AnggaranBelanja" HeaderStyle-CssClass="centerAlign" DataFormatString="{0:N}">
									<ItemStyle Width="10%" HorizontalAlign="Right"/>                       
								</asp:BoundField>
								<asp:BoundField HeaderText="Status" DataField="Butiran" SortExpression="Butiran" HeaderStyle-CssClass="centerAlign">
									<ItemStyle Width="20%" />
								</asp:BoundField>                                                                                             
								<asp:TemplateField>                        
								<ItemTemplate>
										<asp:LinkButton ID="lbDetail" runat="server" CommandName="Select" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="btn-xs btn-primary" ToolTip="Maklumat Lanjut">
											<i class="fa fa-ellipsis-h fa-lg"></i>
										</asp:LinkButton>                                    
									</ItemTemplate>
									<ItemStyle Width="3%" HorizontalAlign="Center"/>
								</asp:TemplateField>
							</columns>
						</asp:GridView>

					

				</div>
			

			</div>
			</div>


	</ContentTemplate>

	</asp:UpdatePanel>  

</asp:Content>
