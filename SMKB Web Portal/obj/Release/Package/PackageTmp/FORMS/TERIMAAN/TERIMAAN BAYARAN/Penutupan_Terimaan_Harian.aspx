<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Penutupan_Terimaan_Harian.aspx.vb" Inherits="SMKB_Web_Portal.Penutupan_Terimaan_Harian" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">

                    <div class="panel panel-default">
                        
                        <div class="panel-heading">Maklumat Penerima</div>
                        <div class="panel-body">
                            <table class="table table-borderless table-striped">
                                <tr>
                                    <td style="width: 106px">Tahun</td>
                                    <td>:</td>
                                    <td>
                                        <asp:label ID="lblTahun" runat="server" ></asp:label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 106px">Tarikh Terima</td>
                                    <td >:</td>
                                    <td>
                                        <asp:label ID="lblTkhMhn" runat="server" ></asp:label>
                                    </td>

                                </tr>
                                
                                <tr>
                                    <td style="width: 106px; height: 23px;">Nama Urusetia</td>
                                    <td style="height: 23px">:</td>
                                    <td style="height: 23px">
                                        <asp:label ID="lblNoPmhn" runat="server" ></asp:label>
                                        &nbsp;-&nbsp;<asp:label ID="lblNamaPemohon" runat="server" ></asp:label>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 106px">Jawatan</td>
                                    <td>:</td>
                                    <td>
                                        <asp:label ID="lblJawatan" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 106px">Zon</td>
                                    <td>:</td>
                                    <td>
                                        <asp:label ID="lblZon" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 106px">Jenis Terimaan</td>
                                    <td>:</td>
                                    <td>
                                        <asp:label ID="lblJnsTerimaan" runat="server"></asp:label>
                                    </td>
                                </tr>
                                
                            </table>
                        </div>
                    </div>
                </div>

            <div class="row">
			<p></p>			
			<div class="panel panel-default" style="width:auto;">
				<div class="panel-heading">
					<h3 class="panel-title">
						Maklumat Terimaan Harian
					</h3>
				</div>
				<div class="panel-body" style="overflow-x:auto">
                    <div>
        <table >
            
            <tr style="height:30px;">
                <td>
                    Jumlah rekod :&nbsp;<label class="control-label" id ="lblJumRekod" runat="server" style="color:mediumblue;" ></label>
                </td>               
                <td style="width:40px;"></td>
                  <td>
                      Cari No Resit : 
                </td>
                
                <td style="width:110px;">
                      <asp:TextBox ID="txtCari" runat="server" class="form-control" Width="100px" ToolTip="Masukkan Nilai"></asp:TextBox>
                  </td>
                    
                    <td>                                  
                      <asp:LinkButton runat="server" ID="btnCari" CssClass="btn-circle" ToolTip="Cari" ValidationGroup="btnCari">
                        <i class="fa fa-search fa-2x" aria-hidden="true"></i>
                      </asp:LinkButton>         
                      
                  </td>
                  
              </tr>
        </table>
        </div>                   
					<asp:GridView ID="gvTerimaan" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="false" EmptyDataText=" Tiada rekod"
							cssclass="table table-striped table-bordered table-hover" Width="100%" BorderStyle="Solid" ShowFooter="true" DataKeyNames="RC01_ID">
								<columns>
								<asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" HeaderStyle-CssClass="centerAlign">
									<ItemTemplate>
										<%# Container.DataItemIndex + 1 %>
									</ItemTemplate>
								</asp:TemplateField>                                 
								<asp:BoundField HeaderText="No Resit" DataField="RC01_NoResitHarian" SortExpression="RC01_NoResitHarian" itemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" />
								<asp:BoundField HeaderText="Tarikh Resit" DataField="RC01_TkhDaftar" SortExpression="RC01_TkhDaftar" itemStyle-HorizontalAlign="Center" ItemStyle-Width="8%" DataFormatString="{0:dd/MM/yyyy}"/>
								<asp:BoundField HeaderText="Maklumat Pembayar" DataField="RC03_NamaPembayar" SortExpression="RC03_NamaPembayar"  ItemStyle-Width="15%" />
                                <asp:BoundField HeaderText="Jenis Urusniaga" DataField="Urusniaga" SortExpression="Urusniaga"  ItemStyle-Width="10%" />
                                <asp:BoundField HeaderText="Mod Terimaan" DataField="ModBayaran" SortExpression="ModBayaran"  ItemStyle-Width="7%" />
                                <asp:BoundField HeaderText="Tujuan" DataField="RC01_Tujuan" SortExpression="RC01_Tujuan" ItemStyle-Width="30%" />
<%--                                <asp:BoundField HeaderText="KW" DataField="NamaNiaga" SortExpression="NamaNiaga" itemStyle-HorizontalAlign="Center" ItemStyle-Width="2%" />									                         
                                <asp:BoundField HeaderText="KO" DataField="ROC01_TkhDaftar" SortExpression="ROC01_TkhDaftar" itemStyle-HorizontalAlign="Center" ItemStyle-Width="2%" />
                                <asp:BoundField HeaderText="PTJ" DataField="ROC01_TkhLulus" SortExpression="ROC01_TkhLulus" itemStyle-HorizontalAlign="Center" ItemStyle-Width="4%" />									
								<asp:BoundField HeaderText="KP" DataField="StatAktif" SortExpression="StatAktif" itemStyle-HorizontalAlign="Center" ItemStyle-Width="4%" />
                                <asp:BoundField HeaderText="Vot" DataField="StatAktif" SortExpression="StatAktif" itemStyle-HorizontalAlign="Center" ItemStyle-Width="4%" />--%>
                                <asp:BoundField HeaderText="Jumlah (RM)" DataField="RC01_Jumlah" SortExpression="RC01_Jumlah" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="right" DataFormatString="{0:N2}"/>	                                                                                             
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
                    <div style="text-align:center">
                        <asp:LinkButton ID="lbtnTerimanBaru" runat="server" CssClass="btn btn-info">
                            <i class="fa fa-plus-circle fa-lg"></i>&nbsp;&nbsp;&nbsp;Terimaan Baru
                        </asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="lbtnHantar" runat="server" CssClass="btn btn-info" OnClientClick="return confirm('Anda pasti untuk hantar rekod ini?');" ValidationGroup="lbtnHantar" >
                            <i class="fas fa-paper-plane fa-lg"></i>&nbsp;&nbsp;&nbsp;Hantar
                        </asp:LinkButton>
                     </div>
                    <br />
				</div>
                </div>            
             </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
