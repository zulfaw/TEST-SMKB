<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="MaklumatPelarasanPT.aspx.vb" Inherits="SMKB_Web_Portal.MaklumatPelarasanPT" EnableEventValidation ="False" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Maklumat Pelarasan PT</h1>
	<link rel="stylesheet" type="text/css" href="../../../Content/Site.css">

<script type="text/javascript">
	function CheckForDataSaving(msg) {
		var result = window.confirm(msg);
		if (!result) {
			alert("False")
			return (false);
		}
		else {
			alert("true");
		}

	};

	debugger;
	function ConfirmNextView2(msg) {
		var result = window.confirm(msg);        
		if (result) {            
			
			
		}
	};

	function upper(ustr) {
		var str = ustr.value;
		ustr.value = str.toUpperCase();
	};

	function lower(ustr) {
		var str = ustr.value;
		ustr.value = str.toLowerCase();
	};
	

</script>
			
			<div class="row">
				<asp:LinkButton ID="lbtnKembali" runat="server" CssClass="btn btn-info">
					<i class="fa fa-arrow-left fa-lg"></i>&nbsp;&nbsp;&nbsp;Kembali
				</asp:LinkButton>
			</div>
			<br />

	<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
		<ContentTemplate>

			
		   <div class="panel panel-default" >
			   <div class="panel-heading">
					<h3 class="panel-title">
						Maklumat Perolehan
					</h3>
				</div>
			   <div class="panel-body" style="overflow:auto;">
							<div class="table-responsive">
					<table style="width:100%;" class="table table-borderless table-striped">
                        <tr>
                            <td style="width: 20%">No Pelarasan PT :</td>
                            <td>
                            <asp:TextBox ID="txtNoPTAdj" runat="server" BackColor="#FFFFCC" ReadOnly="true" CssClass="form-control" Width="200px"></asp:TextBox>
								&nbsp;&nbsp;&nbsp;Tarikh Pelarasan PT:
								&nbsp;<asp:TextBox ID="txtTarikhPTAdj" runat="server" BackColor="#FFFFCC" ReadOnly="true" CssClass="form-control" Width="100px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%">No PT :</td>
                            <td>
                            <asp:label ID="lblNoPT" runat="server"></asp:label>
								&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;Tarikh PT:
								&nbsp;<asp:label ID="lblTrkPT" runat="server" ></asp:label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%;">No Sebut Harga / Tender :</td>
								<td>
									<asp:label ID="lblNoSHTD" runat="server" ></asp:label>
                                    
								</td>
                        </tr>
                        <tr>
						  <td style="width: 20%">No Perolehan :</td>
						  <td style="width: 80%">
							  <asp:label ID="lblNoPO" runat="server"></asp:label>
								&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;Tarikh Mohon :
								&nbsp;<asp:Label ID="lblTrkMohon" runat="server"></asp:Label>
                                &nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;Status:
								&nbsp;<asp:label ID="lblStatus" runat="server" ></asp:label>
						  </td>
					  </tr>						
						<tr>
							<td  style="vertical-align:top; width: 20%;">Tujuan Perolehan :</td>
							<td>
								<asp:label ID="lblTujuan" runat="server" ></asp:label>								
							</td>
						</tr>
							
						<tr>
							<td style="width: 20%;">Kategori Perolehan :</td>
							<td>									
								<asp:label ID="lblKategoriPO" runat="server" ></asp:label>						
									
							</td> 
						</tr>
                        <tr>
							<td style="width: 20%;">Kaedah Perolehan :</td>
							<td>									
								<asp:label ID="lblKaedahPO" runat="server" ></asp:label>						
						        <asp:HiddenField ID="hdKodJnsDok" runat="server" />
							</td> 
						</tr>
                        <tr>
							<td style="width: 20%;">Pemohon :</td>
							<td>									
								<asp:label ID="lblNamaPemohon" runat="server" ></asp:label>						
								&nbsp;&nbsp;&nbsp;Jawatan:
								&nbsp;<asp:label ID="lblJwtnPemohon" runat="server" ></asp:label>
						  	
							</td> 
						</tr>
                        <tr>
							<td style="width: 20%;">Bekal Kepada (PTj) :</td>
							<td>									
								<asp:label ID="lblPTjMohon" runat="server" ></asp:label>					
									
							</td> 
						</tr>
                        <tr>
                            <td style="width: 20%;">Pembekal :</td>
                            <td>									
								<asp:label ID="lblPembekal" runat="server" ></asp:label>
                                &nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;Bekal Sebelum :
								&nbsp;<asp:Label ID="lblTrkBekal" runat="server"></asp:Label>
							</td>
                        </tr>
                        <tr>
                            <td style="width: 20%;">Bayar atas nama :</td>
                            <td>									
								<asp:label ID="lblByrAtasNama" runat="server" ></asp:label>
                                
							</td>
                        </tr>						
                        </table>
							</div>
							<br />

					
					<div class="panel panel-default" style="width:90%">
						<div class="panel-heading">
							<h3 class="panel-title">
								Maklumat Bajet dan Spesifikasi
							</h3>
						</div>
					<div class="panel-body">
						<%--<asp:RadioButtonList ID="rbKodProjek" runat="server" Height="25px" Width="273px" RepeatDirection="Horizontal" AutoPostBack="true">
							<asp:ListItem Text=" Bajet Komited" Value="02" Selected="True" />
							<asp:ListItem Text=" Bajet Mengurus" Value="01" />
						</asp:RadioButtonList>   --%>      
				
					   <%--<br />--%>

					<%--<div id="divBajet" runat="server">--%>
					<div class="panel panel-default" style="width:90%">
						<div class="panel-heading">
							<h4 class="panel-title">
								<asp:Label id="BajetTitle" class="control-label" runat="server" Text="Maklumat Bajet Komited"></asp:Label>
							</h4>
						</div>
						<div class="panel-body">
							<div class="table-responsive">
								<table class="table table-borderless" style="width:100%">
							
							<tr>
								<td class="auto-style1"><Label class="control-label" for="">Kumpulan Wang</Label></td>
								<td class="auto-style2">
									<asp:DropDownList ID="ddlKW" runat="server" AutoPostBack="True" CssClass="form-control" Width="90%"  ></asp:DropDownList>
									<%--<asp:TextBox ID="txtKW" runat="server" CssClass="form-control" Width="50%" ReadOnly="true"></asp:TextBox>--%>
									<%--<asp:RequiredFieldValidator ID="rfvdllKw" runat="server" ControlToValidate="ddlKw" ErrorMessage="" ForeColor="Red" Text="*Perlu dipilih" ValidationGroup="btnStep1" Display="Dynamic"  ></asp:RequiredFieldValidator>--%>
								</td>
							</tr>
							<tr>
								<td style="width: 20%;">Operasi</td>
								<td >
									<asp:DropDownList ID="ddlKodOperasi" runat="server" AutoPostBack="True" CssClass="form-control" Width="90%" ></asp:DropDownList>
								</td>
							</tr>
							<tr>
								<td style="width: 20%;"><Label class="control-label" for="">PTj</Label></td>
								<td >
									<asp:DropDownList ID="ddlPTj" runat="server" AutoPostBack="True" CssClass="form-control" Width="90%"></asp:DropDownList>
								</td>
							</tr>
                            <tr>
								<td style="width: 20%;">Projek Komited</td>
								<td>
									<asp:DropDownList ID="ddlKodProjek" runat="server" AutoPostBack="True" CssClass="form-control" Width="90%"></asp:DropDownList>
									<%--<asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="ddlProjKomited" ErrorMessage="" ForeColor="Red" Text="*Perlu dipilih" ValidationGroup="btnStep1" Display="Dynamic"  ></asp:RequiredFieldValidator>--%>
								</td> 
							</tr>
							<%--<tr>
								<td style="width: 20%;"><Label class="control-label" for="">Bahagian</Label></td>
								<td>
									<asp:DropDownList ID="ddlBahagian" runat="server" AutoPostBack="True" CssClass="form-control" Width="90%"></asp:DropDownList>
									<asp:RequiredFieldValidator ID="rfvBahagian" runat="server" ControlToValidate="ddlKw" ErrorMessage="" ForeColor="Red" Text="*Perlu dipilih" ValidationGroup="btnStep1" Display="Dynamic"  ></asp:RequiredFieldValidator>
								</td>
							</tr>
							<tr>
								<td style="width: 20%;"><Label class="control-label" for="">Unit</Label></td>
								<td>
									<asp:DropDownList ID="ddlUnitPtj" runat="server" AutoPostBack="True" CssClass="form-control" Width="90%"></asp:DropDownList>
									<asp:RequiredFieldValidator ID="rfvUnit" runat="server" ControlToValidate="ddlKw" ErrorMessage="" ForeColor="Red" Text="*Perlu dipilih" ValidationGroup="btnStep1" Display="Dynamic"  ></asp:RequiredFieldValidator>
								</td>
							</tr>--%>
							
							<tr>
								<td style="width: 20%;"><Label class="control-label" for="">Vot Lanjut</Label></td>
								<td>
									<div class="form-inline">
									<asp:DropDownList ID="ddlVotLanjut" runat="server" AutoPostBack="True" CssClass="form-control" Width="90%"></asp:DropDownList>
									
										</div>
									<%--<asp:TextBox ID="txtVotSbg" runat="server" CssClass="form-control" Width="50%" ReadOnly="true"></asp:TextBox>--%>
									<%--<asp:RequiredFieldValidator ID="rfvdllKw" runat="server" ControlToValidate="ddlKw" ErrorMessage="" ForeColor="Red" Text="*Perlu dipilih" ValidationGroup="btnStep1" Display="Dynamic"  ></asp:RequiredFieldValidator>--%>
								</td>
							</tr>
							<%--<tr>
								<td style="width: 20%;"><label class="control-label" for="">Operasi Bajet</label></td>
								<td>
									<asp:RadioButtonList ID="rbOperasiBajet" runat="server" Height="25px" RepeatDirection="Horizontal" Width="30%" AutoPostBack="true">
										<asp:ListItem Selected="True" Text="  Mengurus" Value="1"></asp:ListItem>
										<asp:ListItem Text="  Komited" Value="2"></asp:ListItem>
									</asp:RadioButtonList>
								</td> 
							</tr>--%>
							
							<tr>
								<td style="width: 20%;"><Label class="control-label" for="">Baki Peruntukan (RM)</Label></td>
								<td >
									<asp:TextBox ID="txtBakiPeruntukan" runat="server" CssClass="form-control rightAlign" BackColor="#FFFFCC" Width="150px" Enabled="false"></asp:TextBox>
									<asp:RequiredFieldValidator ID="rfvTxtBakiPeruntukan" runat="server" ControlToValidate="txtBakiPeruntukan" ErrorMessage="" ForeColor="Red" Text="*Peruntukan mesti mempunyai baki" ValidationGroup="btnTambahButiran" Display="Dynamic" ></asp:RequiredFieldValidator>
								
								</td>
							</tr>
							</table>
							</div>
								</div></div>

						 <table id="tableSpek" class="table table-borderless">
						<tr>
							<td style="width: 25%;"><Label class="control-label" for="">Barang / Perkara</Label></td>
							<td>
								<asp:TextBox ID="txtPerkara" runat="server" style="width: 90%; height:auto; min-height:50px;" TextMode="MultiLine" CssClass="form-control" onkeyup="upper(this)"></asp:TextBox>
								<asp:RequiredFieldValidator ID="rfvtxtPerkara" runat="server" ControlToValidate="txtPerkara" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="btnTambahButiran" Display="Dynamic"  ></asp:RequiredFieldValidator>
                                <asp:HiddenField ID="hdIdPTDt" runat="server" />
							</td>
						</tr>
                        <tr>
							<td style="width: 25%;">Jenama :</td>
							<td>
								<asp:TextBox ID="txtJenama" runat="server" CssClass="form-control" Width="90%" onkeyup="upper(this)"></asp:TextBox>
								<asp:RequiredFieldValidator ID="rfvtxtJenama" runat="server" ControlToValidate="txtJenama" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="btnEdit" Display="Dynamic"></asp:RequiredFieldValidator>								
							</td>
						</tr>
						<tr>
							<td style="width: 25%;">Model :</td>
							<td>
								<asp:TextBox ID="txtModel" runat="server" CssClass="form-control" Width="90%" onkeyup="upper(this)"></asp:TextBox>
								<asp:RequiredFieldValidator ID="rfvtxtModel" runat="server" ControlToValidate="txtModel" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="btnEdit" Display="Dynamic"></asp:RequiredFieldValidator>
							</td>
						</tr>
						<tr>
							<td style="width: 25%;">Negara Pembuat :</td>
							<td>
								<asp:DropDownList ID="ddlNegaraBuat" runat="server" AutoPostBack="True" CssClass="form-control" Width="90%"></asp:DropDownList>							
							</td>
						</tr>
						<tr>
							<td style="width: 25%;"><Label class="control-label" for="">Kuantiti</Label></td>
							<td>
								<asp:TextBox ID="txtKuantiti" runat="server" TextMode="Number" CssClass="form-control rightAlign" Width="100px" AutoPostBack="true"></asp:TextBox>
								<asp:RequiredFieldValidator ID="rfvtxtKuantiti" runat="server" ControlToValidate="txtKuantiti" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="btnTambahButiran" Display="Dynamic"  ></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revtxtKuantiti" ControlToValidate="txtKuantiti" runat="server" ErrorMessage=" Hanya 2 titik perpuluhan dibenarkan" ValidationExpression="^\d+(.\d{1,2})?$" ValidationGroup="JumKuantiti"  ForeColor="Red"></asp:RegularExpressionValidator>
							</td>
						</tr>
						<tr>
							<td style="width: 25%;"><Label class="control-label" for="">Ukuran</Label></td>
							<td>
								<asp:DropDownList ID="ddlUkuran" runat="server" CssClass="form-control" Width="150px"></asp:DropDownList>
								<asp:RequiredFieldValidator ID="rfvddlUkuran" runat="server" ControlToValidate="ddlUkuran" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="btnTambahButiran" Display="Dynamic"  ></asp:RequiredFieldValidator>
								
							</td>
						</tr>
						<tr>
							<td style="width: 25%;">Harga Seunit :</td>
							<td>
								<asp:TextBox ID="txtHrgSeunit" runat="server" CssClass="form-control rightAlign" Width="150px" AutoPostBack="true"></asp:TextBox>
								<asp:RegularExpressionValidator ID="revtxtHrgSeunit" ControlToValidate="txtHrgSeunit" runat="server" ErrorMessage=" Hanya format ringgit dibenarkan" ValidationExpression="(?=.)^\$?(([1-9][0-9]{0,2}(,[0-9]{3})*)|[0-9]+)?(\.[0-9]{1,2})?$" ValidationGroup="JumHarga" CssClass="fontValidatation" ForeColor="Red"></asp:RegularExpressionValidator>
								<asp:RequiredFieldValidator ID="rfvtxtHrgSeunit" runat="server" ControlToValidate="txtHrgSeunit" ErrorMessage="" ForeColor="Red" Text=" *Sila isi" ValidationGroup="btnEdit" Display="Dynamic"></asp:RequiredFieldValidator>
							</td>
						</tr>
						<tr>
							<td style="width: 25%;">Inclusive of GST :</td>
							<td>
								<asp:RadioButtonList ID="rbGST" runat="server" Height="25px" Width="180px" RepeatDirection="Horizontal" AutoPostBack="true">
                                    <asp:ListItem Text=" Ya" Value="1"/>
                                    <asp:ListItem Text=" Tidak" Value="0" />
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="rfvrbGST" runat="server" ControlToValidate="rbGST" Display="Dynamic" ErrorMessage="" ValidationGroup="btnEdit" ForeColor="Red" Text=" *Sila pilih"></asp:RequiredFieldValidator>
							</td>
						</tr>
						<tr>
							<td style="width: 25%; height: 27px;"><asp:Label class="control-label" runat="server">Jumlah Harga Tanpa GST (RM) :</asp:Label></td>
							<td style="height: 27px">
								<asp:TextBox ID="txtJumHrg" runat="server"  CssClass="form-control rightAlign" BackColor="#FFFFCC" Width="150px" ReadOnly="true"></asp:TextBox>
							</td>
						</tr>
						<tr>
							<td style="width: 25%;"><asp:Label class="control-label" runat="server">Jumlah GST (RM) :</asp:Label></td>
							<td>
								<asp:TextBox ID="txtJumGST" runat="server" CssClass="form-control rightAlign" BackColor="#FFFFCC" Width="150px" ReadOnly="true"></asp:TextBox>
                                &nbsp &nbsp Kadar (%) &nbsp: 
                                <asp:TextBox ID="txtkadarGST" runat="server" CssClass="form-control rightAlign" TextMode="Number" Width="30px" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
							</td>
						</tr>
						<tr>
							<td style="width: 25%;"><asp:Label class="control-label" runat="server">Jumlah Harga Keseluruhan (RM) :</asp:Label></td>
							<td>
								<asp:TextBox ID="txtJumBesar" runat="server" CssClass="form-control rightAlign" BackColor="#FFFFCC" Width="150px" ReadOnly="true"></asp:TextBox>
							</td>
						</tr>                        
						<tr>
							<td style="height:40px; text-align:center;" colspan="2" >
                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-xs" ValidationGroup="btnTambahButiran">
							        <i class="far fa-edit fa-lg"></i>&nbsp;&nbsp;&nbsp;Kemaskini
						        </asp:LinkButton>								
								&nbsp;&nbsp;
								<asp:LinkButton ID="lbtnUndo" runat="server" CssClass="btn btn-info">
									<i class="fas fa-undo fa-lg"></i>&nbsp;&nbsp;&nbsp;Undo
								</asp:LinkButton>                   
							</td>
						</tr>
								
					</table>
						
					<%--<div>
						<table>
						<tr style="height:30px;">
						<td style="width:80px;">
							<label class="control-label" for="Klasifikasi">Saiz Rekod : </label>
						</td>
						<td style="width:50px;">
							<asp:DropDownList ID="ddlSaizRekod" runat="server" Width="50px" AutoPostBack="true" CssClass="form-control">
								<asp:ListItem Text="5" Value="5" Selected="true" />
								<asp:ListItem Text="10" Value="10" />
								<asp:ListItem Text="25" Value="25" />
								<asp:ListItem Text="50" Value="50" />
							</asp:DropDownList>
						</td>
						</tr>
						</table>
					</div>--%>
					<asp:GridView ID="gvDtPembelian" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="true" EmptyDataText=" Tiada Rekod"
						 cssclass="table table-striped table-bordered table-hover" HeaderStyle-BackColor="#6699FF" Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" Width="100%" 
                        ShowFooter="True" PageSize="20" DataKeyNames="po19_ptdtid">
							<columns>
							<asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" SortExpression="Bil" HeaderStyle-CssClass="centerAlign">
								<ItemTemplate>
									<%# Container.DataItemIndex + 1 %>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField Visible=false ItemStyle-Width="1%">
								<ItemTemplate>
									<asp:Label id="lblBil" runat ="server" text='<%# Eval("po19_bil")%>' ></asp:Label>
								</ItemTemplate>
							</asp:TemplateField>
                            <asp:BoundField HeaderText="KW" SortExpression="KodKw" ItemStyle-Width="2%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center" DataField="KodKw"></asp:BoundField> 
							<asp:BoundField HeaderText="Kod Operasi" SortExpression="KodKo" ItemStyle-Width="2%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center" DataField="KodKo"></asp:BoundField>
							<asp:BoundField HeaderText="PTJ" SortExpression="KodPtj" ItemStyle-Width="3%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center" DataField="KodPtj"></asp:BoundField>
                            <asp:BoundField HeaderText="Kod Projek" SortExpression="KodKp" ItemStyle-Width="2%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center" DataField="KodKp"></asp:BoundField>
                            <asp:BoundField HeaderText="Vot Lanjut" SortExpression="KodVot" ItemStyle-Width="3%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center" DataField="KodVot"></asp:BoundField>
                            <asp:BoundField DataField="PO19_Butiran" HeaderText="Barang / Perkara" ItemStyle-Width="25%" HeaderStyle-CssClass="centerAlign" />
							<asp:BoundField DataField="PO19_Jenama" HeaderText="Jenama" ItemStyle-Width="7%" HeaderStyle-CssClass="centerAlign" NullDisplayText=" " />
							<asp:BoundField DataField="PO19_Model" HeaderText="Model" ItemStyle-Width="7%" HeaderStyle-CssClass="centerAlign" NullDisplayText=" "/>
							<asp:BoundField DataField="Negara" HeaderText="Negara Pembuat" ItemStyle-Width="10%" HeaderStyle-CssClass="centerAlign" />							
							<asp:BoundField DataField="PO19_Kuantiti" HeaderText="Kuantiti" ItemStyle-Width="2%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}"/>
							<asp:BoundField DataField="NamaUkuran" HeaderText="Ukuran" ItemStyle-Width="3%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center" />                            
							<asp:BoundField DataField="PO19_flagInclusiveGST" HeaderText="Inclusive GST?" ItemStyle-Width="3%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Right" NullDisplayText=" "/>
							<asp:BoundField DataField="PO19_KadarHarga" HeaderText="Harga Seunit (Tanpa GST) (RM)" ItemStyle-Width="7%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Right" NullDisplayText=" " DataFormatString="{0:N2}"/>
							<asp:BoundField DataField="JumGST" HeaderText="Cukai GST (RM)" ItemStyle-Width="7%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Right" NullDisplayText=" " DataFormatString="{0:N2}"/>
							<asp:BoundField DataField="JumTanpaGST" HeaderText="Jumlah Harga (Tanpa GST) (RM)" ItemStyle-Width="7%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Right" NullDisplayText=" " DataFormatString="{0:N2}"/>
							<asp:BoundField DataField="PO19_JumKadar" HeaderText="Jumlah Harga (Termasuk GST) (RM)" ItemStyle-Width="7%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Right" NullDisplayText=" " DataFormatString="{0:N2}"/>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblKodNegara" runat="server" Text='<%# Eval("PO19_NegaraBuat")%>'></asp:Label>
                                </ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField ItemStyle-HorizontalAlign="Center">
									<ItemTemplate>										

										<asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CssClass="btn-xs" ToolTip="Pilih Kemaskini">
											<i class="far fa-edit fa-lg"></i>
										</asp:LinkButton>
							
									</ItemTemplate>
									<ItemStyle Width="6%" />
								
							</asp:TemplateField>
						</columns>
					<SelectedRowStyle ForeColor="Blue" />
					</asp:GridView>
						</div>
					</div>

					
				 </div>
               <br />
               <div id="divUlasan">
			        <label class="control-label" for="Ulasan"><b>Ulasan Perbezaan :</b></label>
			        <br />
			        <asp:TextBox ID="txtUlasan" runat="server" TextMode="MultiLine" Width="90%" Rows="3" ></asp:TextBox>
			        <asp:RequiredFieldValidator ID="rfvUlasan" runat="server" ControlToValidate="txtUlasan" ErrorMessage="" ForeColor="Red" Text="*Sila isi" Display="Dynamic"  ValidationGroup="lbtnSimpan"></asp:RequiredFieldValidator>
                   </div>
				<br />
			   <div class="row">
				   <div class="col-md-12" style="text-align:center">                                         
						                  
					   <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn" ToolTip="Simpan" ValidationGroup="btnSimpan">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
					</asp:LinkButton>
						
					</div>
						</div>
 
				

	   </ContentTemplate>

		</asp:UpdatePanel>


		
	
			
</asp:Content>
