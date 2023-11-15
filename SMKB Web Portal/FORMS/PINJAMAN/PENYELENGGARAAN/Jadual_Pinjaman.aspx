<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Jadual_Pinjaman.aspx.vb" Inherits="SMKB_Web_Portal.Jadual_Pinjaman" EnableEventValidation="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>
    <link rel="stylesheet" type="text/css" href="../../../Content/Site.css">
	 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
		<ContentTemplate>
<script type="text/javascript">
	function pageLoad() {
		$('[data-toggle="tooltip"]').tooltip({ placement: "Right", html: true })
	}
				
</script>

			<div class="row">

	  <div class="col-sm-12 col-md-8 col-lg-8">
		  <div class="panel panel-default">
	<div class="panel-body">
		<table style="width: 100%;" class="table table table-borderless">
			  <tr>
				  <td style="width: 20%"><label class="control-label" for="">Kategori Pinjaman:</label></td>
				  <td>
					 <asp:DropDownList ID="ddlKatPinj" runat="server" AutoPostBack="True" CssClass="form-control" width="100%">
				   </asp:DropDownList>
				   </td>
			  </tr>
			  <tr>
				  <td style="width: 20%;"><label class="control-label" for="">Jenis Tempoh:
				  </td>
				  <td><asp:DropDownList ID="ddlJenTempoh" runat="server" AutoPostBack="True" CssClass="form-control" width="100%">
				   </asp:DropDownList></td>
			  </tr>
			<tr style="height:20px; vertical-align :bottom ">
				<td style="width: 20%;"><label class="control-label" for="">Tempoh:</label></td>
				<td>
					<asp:TextBox ID="txtTempoh" runat="server" CssClass="form-control" Text="<%# System.DateTime.Now%>" Width="100px"></asp:TextBox>
					<asp:RequiredFieldValidator ID="rfvTempoh" runat="server" ControlToValidate="txtTempoh" ErrorMessage="" ForeColor="#820303" Text="*Sila isi" ValidationGroup="lbtnSimpan" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>    
				&nbsp;&nbsp;&nbsp;
				<label class="control-label" for="">Amaun (RM):</label>
				&nbsp;
					<asp:TextBox ID="txtAmaun" runat="server" CssClass="form-control" Text="<%# System.DateTime.Now.AddDays(1)%>" Width="100px"></asp:TextBox>
					<asp:RequiredFieldValidator ID="rfvAmaun" runat="server" ControlToValidate="txtAmaun" ErrorMessage="" ForeColor="#820303" Text="*Sila isi" ValidationGroup="lbtnSimpan" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>
				</td>
				</tr>
				<tr>
					<td style="width: 20%;"><Label class="control-label" for="">Ansuran : </Label></td>
					<td>
						<asp:TextBox ID="txtAnsuran" runat="server" CssClass="form-control" Width="100%" TextMode="SingleLine"></asp:TextBox>
						<asp:RequiredFieldValidator ID="rfvAnsuran" runat="server" ControlToValidate="txtAnsuran" ErrorMessage="" ForeColor="#820303" Text="*Sila isi" ValidationGroup="lbtnSimpan" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>
					</td>
				</tr>
			</table>

			<div class="row">
				<div style="text-align:center">
					<asp:LinkButton ID="lbtnReset" runat="server" CssClass="btn " ToolTip="Kosongkan Butiran Perbelanjaan">
						<i class="fa fa-refresh fa-lg"></i>&nbsp;&nbsp;&nbsp;Reset
					</asp:LinkButton>
					&nbsp;&nbsp;&nbsp;
					<asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn " ValidationGroup="btnSimpan">
						<i class="fa fa-floppy-o fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
					</asp:LinkButton>
                    <asp:LinkButton ID="lbtnKemaskini" runat="server" CssClass="btn " ToolTip="Kemaskini" ValidationGroup="btnSimpan">
                        <i class="fa fa-pencil fa-lg"></i>&nbsp;&nbsp;&nbsp;Kemaskini
                    </asp:LinkButton> 
                    &nbsp;&nbsp;
                    <asp:LinkButton ID="lbtnUndo" runat="server" CssClass="btn " ToolTip="Kemaskini">
                        <i class="fa fa-undo fa-lg"></i>&nbsp;&nbsp;&nbsp;Undo
                    </asp:LinkButton>                     
				</div>
			</div>
		 <div>
             <br />                   
			<table>
				<tr style="height:30px;">
				<td style="width:80px;">
					<label class="control-label" for="Klasifikasi">Saiz Rekod : </label>
				</td>
				<td style="width:50px;">
					<asp:DropDownList ID="ddlSaizRekod" runat="server" Width="50px" AutoPostBack="true" CssClass="form-control">
						<asp:ListItem Text="5" Value="5"  />
						<asp:ListItem Text="10" Value="10" />
						<asp:ListItem Text="25" Value="25" />
						<asp:ListItem Text="50" Value="50" Selected="true"/>
					</asp:DropDownList>
				</td>
				</tr>
				</table>
				</div>
			<asp:GridView ID="gvJadual" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="True" EmptyDataText=" "
				cssclass="table table-striped table-bordered table-hover" Width="100%" Font-Size="8pt" ShowFooter="False"
				AllowSorting="True" AllowPaging="True" PageSize="50">
					<columns>
					<asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" SortExpression="Bil" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center">
						<ItemTemplate>
							<asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
						</ItemTemplate>
					</asp:TemplateField>                                       
					<asp:TemplateField HeaderText="Kategori Pinjaman" ItemStyle-Width="3%" SortExpression="katPinj" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Right">
						<ItemTemplate>
							<asp:label id="lblKodSubmenu" runat="server" Text='<%# Eval("KategoriPinj") %>'></asp:label>
						</ItemTemplate>                        
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Jenis Tempoh" ItemStyle-Width="20%" SortExpression="jenTempoh" HeaderStyle-CssClass="centerAlign">
						<ItemTemplate>
							<asp:Label id="lblNamaSubMenu" runat ="server" text='<%# Eval("tempoh")%>' ></asp:Label>                            
						</ItemTemplate>
					</asp:TemplateField> 
					<asp:TemplateField HeaderText="Tempoh" ItemStyle-Width="30%" SortExpression="tempoh" HeaderStyle-CssClass="centerAlign">
						<ItemTemplate>
							<asp:Label id="lblPerkara" runat ="server" text='<%# Eval("tempoh")%>' ></asp:Label>                            
						</ItemTemplate>                        
					</asp:TemplateField>  
					<asp:TemplateField HeaderText="Ansuran (RM)" ItemStyle-Width="30%" SortExpression="ansuran" HeaderStyle-CssClass="centerAlign">
						<ItemTemplate>
							<asp:Label id="lblPerkara" runat ="server" text='<%# Eval("ansuran")%>' ></asp:Label>                            
						</ItemTemplate>                        
					</asp:TemplateField>                                                                                                                        
					<asp:TemplateField>
						<ItemTemplate>
							<asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CssClass="btn-xs" ToolTip="Pilih Kemaskini">
								<i class="fas fa-edit fa-lg"></i>
							</asp:LinkButton>
							
							<asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CssClass="btn-xs" ToolTip="Padam"
								OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');">
								<i class="fas fa-trash-alt fa-lg"></i>
							</asp:LinkButton>                            
						</ItemTemplate>
						<ItemStyle Width="6%" HorizontalAlign="Center" />
					</asp:TemplateField>
				</columns>
					<HeaderStyle BackColor="#996633" />
			</asp:GridView>
		</div>
			  </div>
	</div>


			</ContentTemplate>
		</asp:UpdatePanel>

</asp:Content>

