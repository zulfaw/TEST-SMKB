<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Hantar_Anggaran_Harga.aspx.vb" Inherits="SMKB_Web_Portal.Hantar_Anggaran_Harga" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Hantar Anggaran Harga</h1>

	<link rel="stylesheet" type="text/css" href="../../../Content/Site.css">

	<asp:UpdatePanel ID="UpdatePanel1" runat="server">

	<ContentTemplate>


		
			<div class="panel panel-default" style="width:inherit">

				<div class="panel-heading">
					<h3 class="panel-title">
						Senarai Proses Jualan Naskah
					</h3>
				</div>

				<div class="panel-body" style="overflow-x:auto;">
                    Tahun: &nbsp;<asp:TextBox ID="txtTahun" runat="server" CssClass="form-control" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp; Status: &nbsp;PROSES JUALAN NASKAH
                    <br /><br />
					<asp:GridView ID="gvMohonPO" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="false" EmptyDataText=" Tiada Rekod"
							cssclass="table table-striped table-bordered table-hover" Font-Size="8pt" Width="100%" BorderStyle="Solid" ShowFooter="False" DataKeyNames="PO01_NoMohonSem">
								<columns>
								<asp:TemplateField HeaderText = "Bil" >
									<ItemTemplate>
										<%# Container.DataItemIndex + 1 %>
									</ItemTemplate>
								</asp:TemplateField> 
								<asp:BoundField HeaderText="No Perolehan" DataField="PO01_NoMohon" SortExpression="PO01_NoMohon" >
									<ItemStyle HorizontalAlign="Center"/>
								</asp:BoundField>						            
								<asp:BoundField HeaderText="Tujuan" DataField="PO01_Tujuan" SortExpression="PO01_Tujuan" />                               
								<asp:BoundField HeaderText="Kategori" DataField="Kategori" SortExpression="Kategori" />
                                <asp:BoundField HeaderText="Tarikh Mohon" DataField="TarikhMohon" SortExpression="TarikhMohon" >
                                    <ItemStyle HorizontalAlign="Center"/>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Anggaran Perbelanjaan (RM)" DataField="AnggaranBelanja" SortExpression="AnggaranBelanja"  DataFormatString="{0:N}">
                                    <ItemStyle HorizontalAlign="Right"/>                       
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Perubahan Anggaran Perbelanjaan (RM)" DataField="AnggaranBelanjaBaru" SortExpression="AnggaranBelanjaBaru" DataFormatString="{0:N}" NullDisplayText=" ">
                                    <ItemStyle HorizontalAlign="Right"/>                       
                                </asp:BoundField>
								<asp:BoundField HeaderText="Tarikh & Masa Mula Iklan" DataField="TarikhMasaMulaIklan" SortExpression="TarikhMasaMulaIklan"/>
								 <asp:BoundField HeaderText="Tarikh & Masa Tamat Perolehan" DataField="TarikhMasaTamatPO" SortExpression="TarikhMasaTamatPO"/>
                                <asp:BoundField HeaderText="Status Hantar" DataField="StatusHantar" SortExpression="StatusHantar" />									
                                <asp:BoundField HeaderText="Status Lulus" DataField="StatusLulus" SortExpression="StatusLulus" />									                                                                                        
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
	</ContentTemplate>
	</asp:UpdatePanel>
</asp:Content>


