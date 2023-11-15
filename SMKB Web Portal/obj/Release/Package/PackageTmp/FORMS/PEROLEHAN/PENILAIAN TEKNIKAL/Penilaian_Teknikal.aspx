<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Penilaian_Teknikal.aspx.vb" Inherits="SMKB_Web_Portal.Penilaian_Teknikal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<h1>Penilaian Teknikal</h1>
    <p></p>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">        
    <ContentTemplate>
                            
            <div class="panel panel-default" style="width:90%">
                <div class="panel-heading">
                    <h3 class="panel-title">
						Senarai Jualan Naskah Sebut Harga PTj/ Sebut Harga/ Tender Universiti
                    </h3>
                </div>
                 <div class="panel-body">
                      Tahun&nbsp;:
					<asp:TextBox ID="txtTahun" runat="server" CssClass="form-control" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
						<br /><br />

                <asp:GridView ID="gvPO" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="true" EmptyDataText=" Tiada rekod"
							cssclass="table table-striped table-bordered table-hover" Width="100%" BorderStyle="Solid" ShowFooter="false" Font-Size="8pt">
								<columns>
								 
								<asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" >
									<ItemTemplate>
										<%# Container.DataItemIndex + 1 %>
									</ItemTemplate>
								</asp:TemplateField> 
								<asp:BoundField HeaderText="No Perolehan" DataField="PO01_NoMohon" SortExpression="PO01_NoMohon" HeaderStyle-CssClass="centerAlign">
									<ItemStyle Width="3%" HorizontalAlign="Center"/>
								</asp:BoundField>
                                <asp:BoundField HeaderText="No Sebut Harga/ Tender" DataField="PO02_NoDaftar" SortExpression="PO02_NoDaftar" HeaderStyle-CssClass="centerAlign">
									<ItemStyle Width="6%" HorizontalAlign="Center"/>
								</asp:BoundField>                               						            
								<asp:BoundField HeaderText="Tujuan" DataField="PO01_Tujuan" SortExpression="PO01_Tujuan" HeaderStyle-CssClass="centerAlign">
									<ItemStyle Width="30%" />
								</asp:BoundField>
								<asp:BoundField HeaderText="Kategori" DataField="ButiranBrg" SortExpression="ButiranBrg" HeaderStyle-CssClass="centerAlign">
									<ItemStyle Width="4%" />
								</asp:BoundField>
								<asp:BoundField HeaderText="No Naskah Jualan" DataField="PO02_JualanID" SortExpression="PO02_JualanID" HeaderStyle-CssClass="centerAlign">
									<ItemStyle Width="5%" HorizontalAlign="Center"/>
								</asp:BoundField>
								<asp:BoundField HeaderText="Tarikh & Masa Mula Iklan" DataField="TarikhMasaMulaIklan" SortExpression="TarikhMasaMulaIklan">
									<ItemStyle Width="8%" />
								</asp:BoundField>
								 <asp:BoundField HeaderText="Tarikh & Masa Tamat Perolehan" DataField="TarikhMasaTamatPO" SortExpression="TarikhMasaTamatPO">
									<ItemStyle Width="8%" />
								</asp:BoundField>
								<asp:BoundField HeaderText="Status" DataField="ButiranStatus" SortExpression="ButiranStatus" HeaderStyle-CssClass="centerAlign">
									<ItemStyle Width="10%" />
								</asp:BoundField>                                                                                          
								<asp:TemplateField>                        
								<ItemTemplate>
										<%--<asp:LinkButton ID="lbDetail" runat="server" CommandName="Select" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="btn-xs btn-primary" ToolTip="Maklumat Lanjut">
											<i class="fa fa-ellipsis-h fa-lg"></i>
										</asp:LinkButton>--%>
										<asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CssClass="btn-xs btn-primary" ToolTip="Pilih Kemaskini">
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

