<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Semakan_Syarikat.aspx.vb" Inherits="SMKB_Web_Portal.Semakan_Syarikat" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<h1>Pendaftaran Syarikat Tidak Berdaftar</h1>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
		<ContentTemplate>
         <div class="row">
			<p></p>			
			<div class="panel panel-default" style="width:auto;">
				<div class="panel-heading">
					<h3 class="panel-title">
						Senarai Syarikat Tidak Berdaftar
					</h3>
				</div>
				<div class="panel-body" style="overflow-x:auto">
                    <div>
        <table >
            
            <tr style="height:30px;">
                <td>
                    Jumlah rekod :&nbsp;<label class="control-label" id ="lblJumRekod" runat="server" style="color:mediumblue;" ></label>
                </td>
                <td >
                    &nbsp;|&nbsp;&nbsp;Saiz Rekod : 
                </td>
                <td style="width:50px;">
                    <asp:DropDownList ID="ddlSaizRekod" runat="server" Width="50px" AutoPostBack="true" class="form-control">
                        <asp:ListItem Text="10" Value=10 />
                        <asp:ListItem Text="25" Value=25 />
                        <asp:ListItem Text="50" Value=50 Selected="true"/>
                        <asp:ListItem Text="100" Value=100 />
                        <asp:ListItem Text="200" Value=200 />
                        <asp:ListItem Text="500" Value=500 />
                        <asp:ListItem Text="1000" Value=1000 />
                    </asp:DropDownList>
                </td>
                <td style="width:40px;"></td>
                <td>Tapisan :&nbsp;
                    <asp:DropDownList ID="ddlAktif" runat="server" Width="100px" AutoPostBack="true" class="form-control">
                        <asp:ListItem Text="Aktif" Value="01" Selected="True" />
                        <asp:ListItem Text="Tidak Aktif" Value="00" />
                    </asp:DropDownList>
                </td>
                <td style="width:40px;"></td>
                  <td style="width:40px;">
                      Cari : 
                </td>
                
                <td style="width:110px;">
                      <asp:TextBox ID="txtCari" runat="server" class="form-control" Width="100px" ToolTip="Masukkan Nilai"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvtxtCari" runat="server" ControlToValidate="txtCari" CssClass="fontValidatation" Display="Dynamic" ErrorMessage="" ForeColor="Red" Text="*Perlu diisi" ValidationGroup="btnCari"></asp:RequiredFieldValidator>
                  </td>
                    <td style="width:110px;">
                        <asp:DropDownList ID="ddlCari" runat="server" Width="200px" class="form-control">
                            <asp:ListItem Text="No Pendaftaran Syarikat" Value=1 />
                            <asp:ListItem Text="Nama Syarikat" Value=2 selected="True"/>
                            <asp:ListItem Text="Tahun Lulus" Value=3 />
                            <asp:ListItem Text="Status" Value=4 />
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;&nbsp;                               
                      <asp:LinkButton runat="server" ID="btnCari" CssClass="btn-circle btn-md" ToolTip="Cari" ValidationGroup="btnCari">
                        <i class="fa fa-search fa-lg" aria-hidden="true"></i>
                      </asp:LinkButton>         
                      
                  </td>
                   <td style="width:20px;"></td>
                <td style="text-align:left;">
                    <asp:LinkButton ID="lbtnMohonBaru" runat="server" CssClass="btn btn-md" ToolTip="Mohon Baru">
                        <i class="fa fa-plus-circle fa-lg"></i>&nbsp;&nbsp;&nbsp;Mohon Baru
                    </asp:LinkButton>
                </td>
                  
              </tr>
        </table>
        </div>                   
					<asp:GridView ID="gvSyarikat" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="true" EmptyDataText=" Tiada rekod"
							cssclass="table table-striped table-bordered table-hover" Width="100%" BorderStyle="Solid" PageSize="50" Font-Size="8pt">
								<columns>
								<asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" HeaderStyle-CssClass="centerAlign">
									<ItemTemplate>
										<%# Container.DataItemIndex + 1 %>
									</ItemTemplate>
								</asp:TemplateField>
                                <asp:BoundField HeaderText="ID Pembekal" DataField="ROC01_IDSya" SortExpression="ROC01_IDSya" HeaderStyle-CssClass="centerAlign" ItemStyle-Width="7%" /> 
								<asp:BoundField HeaderText="No Pendaftaran Syarikat" DataField="ROC01_NoSya" SortExpression="ROC01_NoSya" HeaderStyle-CssClass="centerAlign" ItemStyle-Width="10%" />															            
								<asp:BoundField HeaderText="Nama Syarikat" DataField="ROC01_NamaSya" SortExpression="ROC01_NamaSya" HeaderStyle-CssClass="centerAlign" ItemStyle-Width="30%"/>
								<asp:BoundField HeaderText="Wakil Syarikat" DataField="ROC01_WakilSya" SortExpression="ROC01_WakilSya" HeaderStyle-CssClass="centerAlign" ItemStyle-Width="20%" />									                         
                                <asp:BoundField HeaderText="Tarikh Daftar" DataField="ROC01_TkhDaftar" SortExpression="ROC01_TkhDaftar" HeaderStyle-CssClass="centerAlign" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="5%" />
                                <asp:BoundField HeaderText="Tarikh Lulus" DataField="ROC01_TkhLulus" SortExpression="ROC01_TkhLulus" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="5%" />									
								<asp:BoundField HeaderText="Status" DataField="StatAktif" SortExpression="StatAktif" HeaderStyle-CssClass="centerAlign" ItemStyle-Width="10%" />
								<asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblIDSem" runat="server" Text='<%# Eval("ROC01_IDSem")%>'></asp:Label>
                                </ItemTemplate>
							</asp:TemplateField>	                                                                                             
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
		</ContentTemplate>
       
	</asp:UpdatePanel>     
</asp:Content>

