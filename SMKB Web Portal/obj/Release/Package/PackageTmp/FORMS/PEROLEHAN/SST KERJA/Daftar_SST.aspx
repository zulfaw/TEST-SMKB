<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Daftar_SST.aspx.vb" Inherits="SMKB_Web_Portal.Daftar_SST" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<h1>Pendaftaran Surat Setuju Terima (Kerja)</h1>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
		<ContentTemplate>
         <div class="row">
			<p></p>			
			<div class="row">
			<p></p>			
			<div class="panel panel-default" style="width:auto; overflow-y:auto;height:auto">
				<div class="panel-heading">
					<h3 class="panel-title">
						Senarai Permohonan Perolehan yang Masuk e-Perolehan
					</h3>
				</div>
				<div class="panel-body" style="overflow-x:auto">
                                       
					<asp:GridView ID="gvMohonPO" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="false" EmptyDataText=" Tiada rekod"
							cssclass="table table-striped table-bordered table-hover" Width="100%" BorderStyle="Solid" Font-Size="8pt">
								<columns>
								<asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" >
									<ItemTemplate>
										<%# Container.DataItemIndex + 1 %>
									</ItemTemplate>
								</asp:TemplateField> 
								<asp:BoundField HeaderText="No Perolehan" DataField="PO01_NoMohon" SortExpression="PO01_NoMohon" HeaderStyle-CssClass="centerAlign">
									<ItemStyle Width="5%" HorizontalAlign="Center"/>
								</asp:BoundField>
                                <asp:BoundField HeaderText="No Sebut Harga" DataField="PO02_NoDaftar" SortExpression="PO02_NoDaftar" HeaderStyle-CssClass="centerAlign">
									<ItemStyle Width="6%" HorizontalAlign="Center"/>
								</asp:BoundField> 						            
								<asp:BoundField HeaderText="Tujuan" DataField="PO01_Tujuan" SortExpression="PO01_Tujuan" HeaderStyle-CssClass="centerAlign">
									<ItemStyle Width="35%" />
								</asp:BoundField>
								<asp:BoundField HeaderText="Kategori" DataField="Kategori" SortExpression="Kategori" HeaderStyle-CssClass="centerAlign">
									<ItemStyle Width="5%" />
								</asp:BoundField>
                                <asp:BoundField HeaderText="No Naskah Jualan" DataField="PO02_JualanID" SortExpression="PO02_JualanID" HeaderStyle-CssClass="centerAlign">
									<ItemStyle Width="3%" HorizontalAlign="Center"/>
								</asp:BoundField>                                
                                <asp:BoundField HeaderText="Tarikh Mohon" DataField="PO01_TkhMohonSem" SortExpression="PO01_TkhMohonSem" HeaderStyle-CssClass="centerAlign" DataFormatString="{0:dd/MM/yyyy}">
                                    <ItemStyle Width="5%" HorizontalAlign="Center"/>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Tarikh Perlantikan" DataField="PO08_TarikhMasa" SortExpression="PO08_TarikhMasa" DataFormatString="{0:dd/MM/yyyy}">
									<ItemStyle Width="5%" />
								</asp:BoundField>
								<asp:BoundField HeaderText="Status" DataField="Status" SortExpression="Status" HeaderStyle-CssClass="centerAlign">
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
             
            <br />
            <div class="panel panel-default" style="width:auto; overflow-y:auto;height:auto">
				<div class="panel-heading">
					<h3 class="panel-title">
						Senarai Permohonan Perolehan yang Tidak Masuk e-Perolehan
					</h3>
				</div>
				<div class="panel-body" style="overflow-x:auto">
                                       
					<asp:GridView ID="gvMohonPO2" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="false" EmptyDataText=" Tiada rekod"
							cssclass="table table-striped table-bordered table-hover" Width="100%" BorderStyle="Solid" Font-Size="8pt">
								<columns>
								<asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" >
									<ItemTemplate>
										<%# Container.DataItemIndex + 1 %>
									</ItemTemplate>
								</asp:TemplateField> 
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
                                <asp:BoundField HeaderText="Tarikh Lulus" DataField="PO01_TkhLulus" SortExpression="PO01_TkhLulus" DataFormatString="{0:dd/MM/yyyy}">
									<ItemStyle Width="5%" />
								</asp:BoundField>
								<asp:BoundField HeaderText="Status" DataField="Butiran" SortExpression="Butiran" HeaderStyle-CssClass="centerAlign">
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
             </div>
             </div>
		</ContentTemplate>
       
	</asp:UpdatePanel>     
</asp:Content>
