<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Penilaian_Harga_PTJ.aspx.vb" Inherits="SMKB_Web_Portal.Penilaian_Harga_PTJ" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Penilaian Harga PTJ</h1>
    <p></p>  
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">        
    <ContentTemplate>

                  
            <div class="panel panel-default" style="width:90%">
                <div class="panel-heading">
                    <h3 class="panel-title">
						Senarai Jualan Naskah Sebut Harga PTJ</h3>
                </div>
                 <div class="panel-body">
                    Status&nbsp;:&nbsp;&nbsp;           
						<asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" CssClass="form-control" ></asp:DropDownList>
					 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;            
						Tahun&nbsp;:
					<asp:TextBox ID="txtTahun" runat="server" CssClass="form-control" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
						<br /><br />

                <asp:GridView ID="gvPO" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="true" EmptyDataText=" Tiada Rekod"
							cssclass="table table-striped table-bordered table-hover" Width="100%" BorderStyle="Solid" ShowFooter="false" Font-Size="8pt">
								<columns>
								 <asp:TemplateField HeaderText="Bil">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="No Perolehan" DataField="PO01_NoMohon" SortExpression="PO01_NoMohon" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="No Sebut Harga" DataField="PO02_NoDaftar" SortExpression="PO02_NoDaftar" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Tujuan" DataField="PO01_Tujuan" SortExpression="PO01_Tujuan" />
                            <asp:BoundField HeaderText="Kategori" DataField="ButiranBrg" SortExpression="ButiranBrg" />
                            <asp:BoundField HeaderText="No Naskah Jualan" DataField="PO02_JualanID" SortExpression="PO02_JualanID" />
                            <asp:BoundField HeaderText="Tarikh & Masa Mula Iklan" DataField="PO02_TrkMasaMulaIklan" SortExpression="PO02_TrkMasaMulaIklan" DataFormatString="{0:dd/MM/yyyy hh:mm:dd tt}" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Tarikh & Masa Tamat Perolehan" DataField="PO02_TrkMasaTamatPO" SortExpression="PO02_TrkMasaTamatPO" DataFormatString="{0:dd/MM/yyyy hh:mm:dd tt}" ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField HeaderText="Pendaftaran">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CssClass="btn-xs btn-primary" ToolTip="Pilih Kemaskini">
											<i class="fa fa-ellipsis-h fa-lg"></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
							</columns>
						</asp:GridView>                    
                </div>          
            </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
