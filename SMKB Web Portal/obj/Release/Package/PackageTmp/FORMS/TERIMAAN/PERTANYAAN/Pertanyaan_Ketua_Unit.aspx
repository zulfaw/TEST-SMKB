<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Pertanyaan_Ketua_Unit.aspx.vb" Inherits="SMKB_Web_Portal.Pertanyaan_Ketua_Unit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>

    <script type="text/javascript">
        
        </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>            

            <div class="row">
			<p></p>			
			<div class="panel panel-default" style="width:auto;">
				<div class="panel-heading">
					<h3 class="panel-title">
						Maklumat Terimaan Harian
					</h3>
				</div>
				<div class="panel-body" style="overflow-x:auto">
                    <table class="table table-borderless table-striped">
            
            <tr>
                <td>
                    Jumlah rekod :&nbsp;<label class="control-label" id ="lblJumRekod" runat="server" style="color:mediumblue;" ></label>
                &nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;Saiz Rekod : &nbsp;&nbsp;                           
                               <asp:DropDownList ID="ddlSaizRekodPraSiswa" runat="server" AutoPostBack="true" class="form-control" Width="60px">
                                   <asp:ListItem Text="10" Value="10" />
                                   <asp:ListItem Text="25" Value="25" Selected="true"/>
                                   <asp:ListItem  Text="50" Value="50" />
                                   <asp:ListItem Text="100" Value="100" />
                                   <asp:ListItem Text="200" Value="200" />
                                   <asp:ListItem Text="500" Value="500" />
                                   <asp:ListItem Text="1000" Value="1000" />
                               </asp:DropDownList>
                               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                      Cari No Resit : 
                &nbsp;&nbsp;&nbsp;
                      <asp:TextBox ID="txtCari" runat="server" class="form-control" Width="100px" ToolTip="Masukkan Nilai"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;                               
                      <asp:LinkButton runat="server" ID="btnCari" CssClass="btn-circle" ToolTip="Cari" ValidationGroup="btnCari">
                        <i class="fa fa-search fa-2x" aria-hidden="true"></i>
                      </asp:LinkButton>         
                      
                  </td>                  
              </tr>
            <tr>
                <td>
                    Jenis Terimaan &nbsp; : &nbsp;
                    <asp:DropDownList ID="ddlJenisTerimaan" runat="server" AutoPostBack="True" CssClass="form-control">
                        <asp:ListItem Text="Terimaan Harian Belum Tutup" Value=1 Selected="true"/>
                        <asp:ListItem Text="Terimaan Harian Sudah Tutup & Belum Lulus" Value=2 />
                        <asp:ListItem  Text="Terimaan Harian Sudah Lulus" Value=3 />
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvTerimaan" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="true" EmptyDataText=" Tiada rekod"
							cssclass="table table-striped table-bordered table-hover" Width="100%" BorderStyle="Solid" ShowFooter="false" DataKeyNames="RC01_ID" Font-Size="8pt" PageSize="25">
								<columns>
								<asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" HeaderStyle-CssClass="centerAlign">
									<ItemTemplate>
										<%# Container.DataItemIndex + 1 %>
									</ItemTemplate>
								</asp:TemplateField>
                                <asp:BoundField HeaderText="No Resit" DataField="RC01_NoResit" SortExpression="RC01_NoResit" itemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" />                                 
								<asp:BoundField HeaderText="No Resit Harian" DataField="RC01_NoResitHarian" SortExpression="RC01_NoResitHarian" itemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" />
								<asp:BoundField HeaderText="Tarikh Resit" DataField="RC01_TkhDaftar" SortExpression="RC01_TkhDaftar" itemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" DataFormatString="{0:dd/MM/yyyy}"/>
								<asp:BoundField HeaderText="Maklumat Pembayar" DataField="RC03_NamaPembayar" SortExpression="RC03_NamaPembayar"  ItemStyle-Width="18%" />
                                <asp:BoundField HeaderText="Jenis Urusniaga" DataField="Urusniaga" SortExpression="Urusniaga"  ItemStyle-Width="7%" />
                                <asp:BoundField HeaderText="Mod Terimaan" DataField="ModBayaran" SortExpression="ModBayaran"  ItemStyle-Width="5%" />
                                <asp:BoundField HeaderText="Tujuan" DataField="RC01_Tujuan" SortExpression="RC01_Tujuan" ItemStyle-Width="30%" />
<%--                                <asp:BoundField HeaderText="KW" DataField="NamaNiaga" SortExpression="NamaNiaga" itemStyle-HorizontalAlign="Center" ItemStyle-Width="2%" />									                         
                                <asp:BoundField HeaderText="KO" DataField="ROC01_TkhDaftar" SortExpression="ROC01_TkhDaftar" itemStyle-HorizontalAlign="Center" ItemStyle-Width="2%" />
                                <asp:BoundField HeaderText="PTJ" DataField="ROC01_TkhLulus" SortExpression="ROC01_TkhLulus" itemStyle-HorizontalAlign="Center" ItemStyle-Width="4%" />									
								<asp:BoundField HeaderText="KP" DataField="StatAktif" SortExpression="StatAktif" itemStyle-HorizontalAlign="Center" ItemStyle-Width="4%" />
                                <asp:BoundField HeaderText="Vot" DataField="StatAktif" SortExpression="StatAktif" itemStyle-HorizontalAlign="Center" ItemStyle-Width="4%" />--%>
                                <asp:BoundField HeaderText="Jumlah (RM)" DataField="RC01_Jumlah" SortExpression="RC01_Jumlah" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="right" DataFormatString="{0:N2}"/>	                                                                                             
                                <asp:BoundField HeaderText="Zon" DataField="NamaZon" SortExpression="NamaZon"  ItemStyle-Width="10%" />
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
                    <br />
                    
				</div>
                </div>            
             </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
