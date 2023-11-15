<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="MaklumatPendaftaran.aspx.vb" Inherits="SMKB_Web_Portal.MaklumatPendaftaran" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<h1>Maklumat Syarikat</h1>  
	<link rel="stylesheet" type="text/css" href="../../../Content/Site.css">
	<script type="text/javascript">
	    function upper(ustr) {
	        var str = ustr.value;
	        ustr.value = str.toUpperCase();	        
	    };

	    function trim(ustr) {
	        var str = ustr.value;
	        ustr.value = str.trim();
	    };
        </script>

    <div class="row">
				<asp:LinkButton ID="lbtnKembali" runat="server" CssClass="btn btn-info">
					<i class="fa fa-arrow-left fa-lg"></i>&nbsp;&nbsp;&nbsp;Kembali
				</asp:LinkButton>
			</div>

	<asp:UpdatePanel ID="UpdatePanel1" runat="server">        
	<ContentTemplate>
		<div class="panel panel-default">
						<div class="panel-heading">
							<h4 class="panel-title">
								Pendaftaran Vendor
							</h4>
						</div>
						<div class="panel-body">
						
							<asp:HiddenField ID="hdNoIDSem" runat="server" />
						<table style="width:100%" class="table table-borderless">
							<tr>
								<td style="width: 20%;">No Pendaftaran Syarikat:  </td>
								<td >
									<asp:TextBox ID="txtNoSya" runat="server" CssClass="form-control" Width="200px" MaxLength="50"></asp:TextBox>
									<asp:RequiredFieldValidator ID="rfvtxtNoSya" runat="server" ControlToValidate="txtNoSya" CssClass="fontValidatation" Display="Dynamic" ErrorMessage="" ForeColor="Red" Text="*Perlu diisi" ValidationGroup="Daftar"></asp:RequiredFieldValidator>
								</td>
							</tr>
							
							<tr>
								<td style="width: 20%;">Nama Syarikat: </td>
								<td >
									<asp:TextBox ID="txtNamaSya" runat="server" CssClass="form-control" Width="80%" MaxLength="120"></asp:TextBox>
									<asp:RequiredFieldValidator ID="rfvtxtNamaSya" runat="server" ControlToValidate="txtNamaSya" CssClass="fontValidatation" Display="Dynamic" ErrorMessage="" ForeColor="Red" Text="*Perlu diisi" ValidationGroup="btnSimpan"></asp:RequiredFieldValidator>
								</td>
							</tr>
							<tr>
						<td style="width: 20%">
							<asp:Label ID="lblNamaPendaftar" runat="server" AssociatedControlID="txtNamaPendaftar" CssClass="control-label" Text="Nama Pendaftar:"></asp:Label>
						</td>
						<td >
							<asp:TextBox ID="txtNamaPendaftar" runat="server" CssClass="form-control" Width="80%" Font-Size="Medium" onkeyup="upper(this)" MaxLength="80"></asp:TextBox>
							<asp:RequiredFieldValidator ID="rfvtxtNamaPendaftar" runat="server" ControlToValidate="txtNamaPendaftar" Display="Dynamic" CssClass="fontValidatation" ToolTip="Name is required." ForeColor="Red" ValidationGroup="btnSimpan">*Perlu diisi</asp:RequiredFieldValidator>
						</td>
                        <tr>
								<td style="width: 20%;">Status Aktif:</td>
								<td >
                                    <asp:DropDownList ID="ddlAktif" runat="server" AutoPostBack="True" CssClass="form-control" Width="300px">
                                        <asp:ListItem Text="-Sila Pilih-" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Aktif" Value="01"></asp:ListItem>
                                        <asp:ListItem Text="Tidak Aktif" Value="00"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlAktif" runat="server" ControlToValidate="ddlAktif" CssClass="fontValidatation" Display="Dynamic" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="btnSimpan"></asp:RequiredFieldValidator>                                   
								</td>
							</tr>
					</tr>  
							</table>

							<div class="panel panel-default">
						<div class="panel-heading">
							<h4 class="panel-title">
								Maklumat Bank
							</h4>
						</div>
						<div class="panel-body">
							<table style="width:100%" class="table table-borderless">
							<tr>
								<td style="width: 20%;">Kod Bank:</td>
								<td>
									<asp:DropDownList ID="ddlKodBank" runat="server" AutoPostBack="True" CssClass="form-control" Width="80%"></asp:DropDownList>
								</td>
							</tr>
							
							<tr>
								<td style="width: 20%;">No Akaun:</td>
								<td>
									<asp:TextBox ID="txtNoAkaun" runat="server" CssClass="form-control" Width="300px" onkeyup="trim(this)" MaxLength="20"></asp:TextBox>
								</td>
							</tr>                                                       
							
							</table>
							<br />

					</div></div>

							<div class="panel panel-default">
		   <div class="panel-heading">
					Alamat Perniagaan</div>
			<div class="panel-body">
				<table style="width: 100%;" class="table table-borderless">
                    <tr>
                        <td colspan="2">
                            <asp:CheckBox ID="chxCawangan" runat ="server" Text="Cawangan"/> &nbsp;&nbsp;&nbsp;
                            <asp:CheckBox ID="chxSyarikatPinjaman" runat ="server" Text="Syarikat Pinjaman Komputer / Kenderaan"/>
                        </td>
                    </tr>
						<tr>
							<td style="width: 20%;vertical-align:top;" rowspan="2">Alamat</td>
							<td>
								<asp:TextBox ID="txtAlamat1" runat="server" Width="90%" CssClass="form-control" onkeyup="upper(this)" MaxLength="120"></asp:TextBox>
								<asp:RequiredFieldValidator ID="rfvtxtAlamat1" runat="server" ControlToValidate="txtAlamat1" ErrorMessage="" ForeColor="Red" Text="*Perlu diisi" ValidationGroup="lbtnNextView1" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>
							</td>
						</tr>
						<tr>
							<td>
								<asp:TextBox ID="txtAlamat2" runat="server" CssClass="form-control" Width="90%" onkeyup="upper(this)" MaxLength="120"></asp:TextBox>                                
							</td>
						</tr>                        
						<tr>
							<td style="width: 20%;">Poskod</td>
							<td>
								<asp:TextBox ID="txtPoskod" runat="server" CssClass="form-control" Width="80px" onkeyup="trim(this)" MaxLength="10"></asp:TextBox>
								<asp:RequiredFieldValidator ID="rfvtxtPoskod" runat="server" ControlToValidate="txtPoskod" CssClass="fontValidatation" Display="Dynamic" ErrorMessage="" ForeColor="Red" Text="*Perlu diisi" ValidationGroup="btnSimpan"></asp:RequiredFieldValidator>
								&nbsp;&nbsp;&nbsp;
								Bandar &nbsp;
								<asp:TextBox ID="txtBandar" runat="server" CssClass="form-control" Width="200px" onkeyup="upper(this)" MaxLength="50"></asp:TextBox>
								<asp:RequiredFieldValidator ID="rfvtxtBandar" runat="server" ControlToValidate="txtBandar" CssClass="fontValidatation" Display="Dynamic" ErrorMessage="" ForeColor="Red" Text="*Perlu diisi" ValidationGroup="btnSimpan"></asp:RequiredFieldValidator>
							</td>
						</tr>
						<tr>
							<td style="width: 20%;">Negeri</td>
							<td>
								<asp:DropDownList ID="ddlNegeri" runat="server" AutoPostBack="True" CssClass="form-control" Width="90%"></asp:DropDownList>
								<asp:RequiredFieldValidator ID="rfvddlNegeri" runat="server" ControlToValidate="ddlNegeri" CssClass="fontValidatation" Display="Dynamic" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="btnSimpan"></asp:RequiredFieldValidator>
							</td>
						</tr>
						<tr>
							<td style="width: 20%;">Negara</td>
							<td>
								<asp:DropDownList ID="ddlNegara" runat="server" AutoPostBack="True" CssClass="form-control" Width="90%"></asp:DropDownList>
								<asp:RequiredFieldValidator ID="rfvddlNegara" runat="server" ControlToValidate="ddlNegara" CssClass="fontValidatation" Display="Dynamic" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="btnSimpan"></asp:RequiredFieldValidator>
							</td>
						</tr>
						<tr>
							<td style="width: 20%;">No. Telepon 1</td>
							<td>
								<asp:TextBox ID="txtTelp1" runat="server" CssClass="form-control" Width="100px" textmode="Phone" onkeyup="trim(this)" MaxLength="15"></asp:TextBox>
								<asp:RequiredFieldValidator ID="rfvtxttxtTelp1" runat="server" ControlToValidate="txtTelp1" CssClass="fontValidatation" Display="Dynamic" ErrorMessage="" ForeColor="Red" Text="*Perlu diisi" ValidationGroup="btnSimpan"></asp:RequiredFieldValidator>
							</td>
						</tr>
						<tr>
							<td style="width: 20%;">No. Telepon 2</td>
							<td>
								<asp:TextBox ID="txtTelp2" runat="server" CssClass="form-control" Width="100px" textmode="Phone" onkeyup="trim(this)" MaxLength="15"></asp:TextBox>
							</td>
						</tr>
						<tr>
							<td style="width: 20%;">No. Faksimili</td>
							<td>
								<asp:TextBox ID="txtFax" runat="server" CssClass="form-control" Width="100px" textmode="Phone" onkeyup="trim(this)" MaxLength="15"></asp:TextBox>
							</td>
						</tr>
						<tr>
							<td style="width: 20%;">Laman Web URL:</td>
							<td>
								<asp:TextBox ID="txtWeb" runat="server" CssClass="form-control" Width="90%" onkeyup="trim(this)" MaxLength="100"></asp:TextBox>
							</td>
						</tr>
						<tr>
							<td style="width: 20%;">Email Syarikat:</td>
							<td >
								<asp:textbox ID="txtEmailSya" runat="server" CssClass="form-control" Width="70%" onkeyup="trim(this)" MaxLength="50"></asp:textbox>									
							</td>
						</tr>
						<%--<tr>
							<td style="width: 20%;vertical-align:top;">Kategori:</td>
							<td>
								<asp:RadioButtonList ID="rbKategoriSyarikat" runat="server" Height="25px" RepeatDirection="Vertical" AutoPostBack="true" Width="300px" onkeyup="trim(this)">
										<asp:ListItem Text="  Penyewa sahaja" Value="2"></asp:ListItem>
										<asp:ListItem Text="  Tuntutan bayaran sahaja" Value="3"></asp:ListItem>
								</asp:RadioButtonList>
							</td>

						</tr>--%>
					</table>
		   </div></div>
                            <div class="row">
							<div class="col-md-12" style="text-align:center">
                                <%--<asp:LinkButton ID="lbtnHantar" runat="server" CssClass="btn btn-info" ValidationGroup="lbtnHantar" OnClientClick="return confirm('Anda pasti untuk hantar rekod ini?');">
                                    <i class="fab fa-telegram-plane fa-lg"></i>&nbsp;&nbsp;&nbsp;Hantar
                                </asp:LinkButton>--%>
                                <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn" ToolTip="Lulus" ValidationGroup="btnSimpan">
						            <i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Lulus
					            </asp:LinkButton>
								</div>
								</div>
							</div>
							
						</div>
			</div>

		</ContentTemplate>
		</asp:UpdatePanel>
</asp:Content>
