<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="MaklumatPerlantikanVendor.aspx.vb" Inherits="SMKB_Web_Portal.MaklumatPerlantikanVendor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">       
       
        function pageLoad() {
            $('[data-toggle="tooltip"]').tooltip();
        }
    
       
        function Validate(sender, args) {
            //Get the reference of GridView
            var gridView = document.getElementById("<%=gvHarga.ClientID %>");
            debugger
            var checkBoxes = gridView.getElementsByTagName("input");

            for (var i = 0; i < checkBoxes.length; i++) {

                if (checkBoxes[i].type == "checkbox" && !checkBoxes[i].disabled) {
                        if (checkBoxes[i].checked) {
                            args.IsValid = true;
                            return;
                        }
                    }
                
            }
            args.IsValid = false;
        }

    function fCheckMainSyor() {       
        
        var gridView = document.getElementById("<%=gvHarga.ClientID %>");
            
            var checkBoxes = gridView.getElementsByTagName("input");
            
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].type == "checkbox" && !checkBoxes[i].disabled) {                    
                    if (checkBoxes[i].id == "CheckBox1") {
                        if (checkBoxes[i].checked) {
                            checkBoxes[i].checked = false;
                        }                        
                    }
                    }                                
            }            
    }

    function fCheckChildSyor() {

        var hd = document.getElementById("<%=hdBool.ClientID %>");
        var gridView = document.getElementById("<%=gvHarga.ClientID %>");
            
            var checkBoxes = gridView.getElementsByTagName("input");
            
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].type == "checkbox" && !checkBoxes[i].disabled) {                    
                    if (checkBoxes[i].id == "CheckBox1Dt") {
                        if (checkBoxes[i].checked) {
                            checkBoxes[i].checked = false;
                            hd.value = "true";
                        }                        
                    }
                    }
                                
            }
            
    }

    function fCheckMainSrtNiat() {
        var gridView = document.getElementById("<%=gvHarga.ClientID %>");            
            var checkBoxes = gridView.getElementsByTagName("input");            
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].type == "checkbox" && !checkBoxes[i].disabled) {                    
                    if (checkBoxes[i].id == "ChxSrtNiat") {
                        if (checkBoxes[i].checked) {
                            checkBoxes[i].checked = false;
                        }                        
                    }
                    }                                
            }            
    }

    function fCheckChildSrtNiat() {
        var hd = document.getElementById("<%=hdBool1.ClientID %>");
        var gridView = document.getElementById("<%=gvHarga.ClientID %>");            
            var checkBoxes = gridView.getElementsByTagName("input");            
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].type == "checkbox" && !checkBoxes[i].disabled) {                    
                    if (checkBoxes[i].id == "ChxSrtNiatDt") {
                        if (checkBoxes[i].checked) {
                            checkBoxes[i].checked = false;
                            hd.value = "true";
                        }                        
                    }
                }                                
            }            
    }
</script>
    
    <style type="text/css">
.calendarContainerOverride table
{
    width:0px;
    height:0px;
}

.calendarContainerOverride table tr td
{
    padding:0;
    margin:0;
}
             .auto-style1 {
                 width: 20%;
                 height: 27px;
             }
             .auto-style2 {
                 height: 27px;
             }
    </style>
    
    <h1>Perlantikan Vendor</h1>
    <p></p>
    <div class="row">
				<asp:LinkButton ID="lbtnKembali" runat="server" CssClass="btn btn-info">
					<i class="fa fa-arrow-left fa-lg"></i>&nbsp;&nbsp;&nbsp;Kembali
				</asp:LinkButton>
			</div>
			<br />

    <asp:MultiView ID="mvNilaiHarga" runat="server" ActiveViewIndex="0">
		<asp:View ID="View1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">        
    <ContentTemplate>
                                        
        
        <div class="panel panel-default" style="width:auto;">
                  <div class="panel-heading">
                    <h3 class="panel-title">
                        Maklumat Perlantikan Vendor
                    </h3>
                   </div>

                    <div class="panel-body" style="overflow-x:auto">
                        <table style="width:100%;" class="table table-borderless table-striped">
                            <tr>
						  <td style="width: 20%">ID Naskah Jualan :</td>
						  <td style="width: 80%">
							  <asp:TextBox ID="txtIdNJ" runat="server" BackColor="#FFFFCC" CssClass="form-control"  Width="150px" ReadOnly="true"></asp:TextBox>
							  
								&nbsp;&nbsp;&nbsp;Status:
								&nbsp;<asp:TextBox ID="txtStatus" runat="server" BackColor="#FFFFCC" ReadOnly="true" CssClass="form-control" Width="40%"></asp:TextBox>
						  </td>
					  </tr>
						<tr>
							<td style="width: 20%;">No Perolehan :</td>
							<td>
								<div class="form-inline">
									<asp:TextBox ID="txtNoPO" runat="server" BackColor="#FFFFCC" ReadOnly="true" CssClass="form-control" Width="200px"></asp:TextBox>									
								</div>
																	
							</td>
						</tr>
						<tr>
							<td  style="vertical-align:top; width: 20%;">Tujuan Perolehan :</td>
							<td>
								<asp:TextBox ID="txtTujuan" runat="server" style="width: 90%; height:auto; min-height:100px;" BackColor="#FFFFCC" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>								
							</td>
						</tr>
							
						<tr>
							<td style="width: 20%;">Kategori Perolehan :</td>
							<td>									
								<asp:TextBox ID="txtKategoriPO" runat="server" BackColor="#FFFFCC" ReadOnly="true" CssClass="form-control" Width="200px"></asp:TextBox>						
									
							</td> 
						</tr>
                        <tr>
							<td style="width: 20%;">Kaedah Perolehan :</td>
							<td>									
								<asp:TextBox ID="txtKaedahPO" runat="server" BackColor="#FFFFCC" ReadOnly="true" CssClass="form-control" Width="60%"></asp:TextBox>						
									
							</td> 
						</tr>
                        <tr>
							<td style="width: 20%;">PTJ :</td>
							<td>									
								<asp:TextBox ID="txtPTjMohon" runat="server" BackColor="#FFFFCC" ReadOnly="true" CssClass="form-control" Width="50%"></asp:TextBox>						
									
							</td> 
						</tr>
						
					   <tr>
								<td style="width: 20%;">No Sebut Harga / Tender :</td>
								<td>
									<asp:TextBox ID="txtNoSHTD" runat="server" CssClass="form-control" Width="40%" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
									
								</td>
							</tr>
                            <tr>
								<td style="width: 20%;">Anggaran Harga (RM):</td>
								<td>
									<asp:TextBox ID="txtAngHarga" runat="server" CssClass="form-control rightAlign" Width="120px" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
									
								</td>
							</tr>
                            <tr>
					      <td style="width: 20%">Tarikh Mula Iklan :</td>
                          <td style="width: 80%">
                              <asp:TextBox ID="txtTrkMulaIklan" runat="server" CssClass="form-control rightAlign" Width="100px" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
                              
                              &nbsp;&nbsp;&nbsp; Masa Mula Iklan &nbsp; :&nbsp;<asp:TextBox ID="txtMasaMulaIklan" runat="server" CssClass="form-control rightAlign" BackColor="#FFFFCC" ReadOnly="true" TextMode="Time" Width="100px" ></asp:TextBox>
                              
                          </td>
					<tr>
						<td>
							Tarikh Mula Perolehan :
						</td>
						<td>
							<asp:TextBox ID="txtTrkMulaPO" runat="server" CssClass="form-control rightAlign" Width="100px" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
							
							&nbsp;&nbsp;&nbsp;
							Masa Mula Perolehan
							&nbsp;
							:&nbsp;<asp:TextBox ID="txtMasaMulaPO" runat="server" CssClass="form-control rightAlign" Width="100px" TextMode="Time" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
							
						</td>
					</tr>
					<tr>                    
						<td style="width: 20%">Tarikh Tamat Perolehan &nbsp;:</td>
						<td style="width: 80%">
							<asp:TextBox ID="txtTrkTamatPO" runat="server" CssClass="form-control rightAlign" Width="100px" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
							
							&nbsp;&nbsp;&nbsp;
						Masa Tamat Perolehan&nbsp; :&nbsp;<asp:TextBox ID="txtMasaTamatPO" runat="server" CssClass="form-control rightAlign" Width="100px" TextMode="Time" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
							
						</td>
					</tr>
                    <tr>                    
						<td style="width: 20%">Tarikh Lawatan Tapak &nbsp;:</td>
						<td style="width: 80%">
							<asp:TextBox ID="txtTrkLawTpk" runat="server" CssClass="form-control rightAlign" Width="100px" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
							
							&nbsp;&nbsp;&nbsp;
						Masa Tamat Perolehan&nbsp; :&nbsp;<asp:TextBox ID="txtMasaLawTpk" runat="server" CssClass="form-control rightAlign" Width="100px" TextMode="Time" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
							
						</td>
					</tr>
                            <tr>
                                <td class="auto-style1">Tarikh Mula Pembuka:</td>
                                <td class="auto-style2">
                                    <asp:TextBox ID="txtTarikhPembuka" runat="server" BackColor="#FFFFCC" ReadOnly="true" CssClass="form-control rightAlign" Width="120px"></asp:TextBox>
                                     &nbsp;&nbsp;&nbsp; Masa Mula Pembuka &nbsp; :&nbsp;<asp:TextBox ID="txtMasaPembuka" runat="server" CssClass="form-control rightAlign" TextMode="Time" Width="100px" BackColor="#FFFFCC" ReadOnly="true" ></asp:TextBox>
                              
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1">Tarikh Penilaian Harga:</td>
                                <td class="auto-style2">
                                    <asp:TextBox ID="txtTarikhNilaiHrg" runat="server" BackColor="#FFFFCC" ReadOnly="true" CssClass="form-control rightAlign" Width="120px"></asp:TextBox>                                    
                              
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1">Tarikh Penilaian Teknikal:</td>
                                <td class="auto-style2">
                                    <asp:TextBox ID="txtTarikhNilaiTek" runat="server" BackColor="#FFFFCC" ReadOnly="true" CssClass="form-control rightAlign" Width="120px"></asp:TextBox>                                    
                              
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1">Tarikh Pengesyoran:</td>
                                <td class="auto-style2">
                                    <asp:TextBox ID="txtTarikhSyor" runat="server" BackColor="#FFFFCC" ReadOnly="true" CssClass="form-control rightAlign" Width="120px"></asp:TextBox>                                    
                              
                                </td>
                            </tr>
                            <tr class="calendarContainerOverride">
                                <td style="width: 20%;">Tarikh Mesyuarat:</td>
                                <td>
                                    <asp:TextBox ID="txtTarikh" runat="server" CssClass="form-control rightAlign" Width="120px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="caltxtTarikh" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtTarikh" TodaysDateFormat="dd/MM/yyyy" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTarikh" ErrorMessage="" ForeColor="Red" Text="*Sila Isi" ValidationGroup="btnSimpan" Display="Dynamic" ></asp:RequiredFieldValidator>
                                    &nbsp;&nbsp;&nbsp; Masa Mesyuarat &nbsp; :&nbsp;<asp:TextBox ID="txtMasa" runat="server" CssClass="form-control rightAlign" TextMode="Time" Width="100px" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMasa" ErrorMessage="" ForeColor="Red" Text="*Sila Isi" ValidationGroup="btnSimpan" Display="Dynamic" ></asp:RequiredFieldValidator>
                                </td>
                            </tr>                           
                            <tr>
								<td style="width: 20%;">Nama Mesyuarat :</td>
								<td>
									<asp:TextBox ID="txtNamaMesyuarat" runat="server" CssClass="form-control" Width="40%" ></asp:TextBox>
									<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNamaMesyuarat" ErrorMessage="" ForeColor="Red" Text="*Sila Isi" ValidationGroup="btnSimpan" Display="Dynamic" ></asp:RequiredFieldValidator>
								</td>
							</tr>
                            <tr>
                                <td colspan="2" style="text-align:center">
                                    <asp:LinkButton ID="lbtnMohonPO" runat="server" CssClass="btn" ToolTip="Lihat Maklumat Permohonan Pembelian" >
						<i class="fas fa-external-link-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Permohonan
					</asp:LinkButton> &nbsp; &nbsp;
                                    <asp:LinkButton ID="lbtnPembuka" runat="server" CssClass="btn" ToolTip="Lihat Maklumat Pembuka" >
						<i class="fas fa-external-link-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Pembuka
					</asp:LinkButton> &nbsp; &nbsp;
                                    <asp:LinkButton ID="lbtnNilaiHarga" runat="server" CssClass="btn" ToolTip="Lihat Maklumat Penilaian Harga" >
						<i class="fas fa-external-link-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;P. Harga
					</asp:LinkButton> &nbsp; &nbsp;
                                    <asp:LinkButton ID="lbtnNilaiTeknikal" runat="server" CssClass="btn" ToolTip="Lihat Maklumat Penilaian Teknikal" >
						<i class="fas fa-external-link-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;P. Teknikal
					</asp:LinkButton>
                                    &nbsp; &nbsp;
                                    <asp:LinkButton ID="lbtnSyor" runat="server" CssClass="btn" ToolTip="Lihat Maklumat Pengesyoran" >
						<i class="fas fa-external-link-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Pengesyoran
					</asp:LinkButton></td>
                            </tr>
                        </table>
                    
                        <br />
                       
                        <asp:GridView ID="gvHarga" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText=" Tiada rekod"
							cssclass="table table-striped table-bordered table-hover" Width="100%" BorderStyle="Solid" DataKeyNames="PO03_OrderID">
								<columns>
                                <asp:TemplateField ItemStyle-Width="2%">
								<ItemTemplate>								   
										<asp:Panel ID="pnlMaster" runat="server">
											<asp:Image ID="imgCollapsible" runat="server" ImageUrl="../../../Images/plus.png" Style="margin-right: 5px;" />
											<span style="font-weight:bold;display:none;"><%#Eval("PO03_OrderID")%></span>
										</asp:Panel>										
										<ajaxToolkit:CollapsiblePanelExtender ID="ctlCollapsiblePanel" runat="Server" AutoCollapse="False" AutoExpand="False" CollapseControlID="pnlMaster" Collapsed="True" CollapsedImage="../../../Images/plus.png" CollapsedSize="0" ExpandControlID="pnlMaster" ExpandDirection="Vertical" ExpandedImage="../../../Images/minus.png" ImageControlID="imgCollapsible" ScrollContents="false" TargetControlID="pnlChild"/>
								</ItemTemplate>
                                    </asp:TemplateField>
								<asp:TemplateField HeaderText = "Bil" >
									<ItemTemplate>
										<%# Container.DataItemIndex + 1 %>
									</ItemTemplate>
									<HeaderStyle CssClass="centerAlign" />
									<ItemStyle Width="3%" />
								</asp:TemplateField>
								<asp:BoundField HeaderText="Kod" DataField="Kod" SortExpression="Kod" ItemStyle-HorizontalAlign="Center" ReadOnly="true">				            
									<ItemStyle Width="4%" />
									</asp:BoundField>
                                <asp:BoundField HeaderText="Unik ID" DataField="PO03_UnikID" SortExpression="PO03_UnikID" ReadOnly="true" ItemStyle-HorizontalAlign="Center">
									<ItemStyle Width="6%" />
									</asp:BoundField>								
								<asp:TemplateField HeaderText="Status Bumi" SortExpression="ROC01_KodBumi" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">                                    
                                    <ItemTemplate><%#IIf(CInt(Eval("ROC01_KodBumi")) = 1, "Bumi", "Bukan Bumi")%></ItemTemplate>
                                </asp:TemplateField>                                
								<asp:BoundField HeaderText="Tempoh Bekal/Siap" DataField="Tempoh" SortExpression="Tempoh" ItemStyle-HorizontalAlign="Center" ReadOnly="true">
									<ItemStyle Width="15%" />
									</asp:BoundField>
                                <asp:TemplateField HeaderText="Status Hantar" SortExpression="PO03_StatusHantar" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        Status Hantar
                                        <i class="fas fa-question-circle fa-lg" aria-hidden="true" data-toggle="tooltip" data-placement="right" title="Status vendor hantar melalui sistem eVendor"></i>
                                    </HeaderTemplate>
                                    <ItemTemplate><%#IIf(Eval("PO03_StatusHantar"), "Ya", "Tidak")%></ItemTemplate>
                                </asp:TemplateField>						
                                <asp:TemplateField SortExpression="PO03_MatchHantar" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        Status Suai Padan
                                        <i class="fas fa-question-circle fa-lg" aria-hidden="true" data-toggle="tooltip" data-placement="bottom" title="Status suai padan antara sistem eVendor dan manual ketika proses pembuka"></i>
                                    </HeaderTemplate>
                                    <ItemTemplate><%#IIf(Eval("PO03_MatchHantar"), "Ya", "Tidak")%></ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Harga Tawaran Termasuk GST (RM)" DataField="JumHarga" SortExpression="JumHarga" ItemStyle-HorizontalAlign="Right" ReadOnly="true"  DataFormatString="{0:N}">
									<ItemStyle Width="8%" />
									</asp:BoundField>							
								<asp:BoundField HeaderText="Harga Tawaran Tanpa GST (RM)" DataField="JumTanpaGST" SortExpression="JumTanpaGST" ItemStyle-HorizontalAlign="Right" ReadOnly="true" DataFormatString="{0:N}">
									<ItemStyle Width="8%" />
									</asp:BoundField>
                                <asp:BoundField HeaderText="Perbezaan Harga Tawaran (RM)" DataField="BezaHarga" SortExpression="BezaHarga" DataFormatString="{0:#,##0.00;(#,##0.00);0}" ItemStyle-HorizontalAlign="Right" ReadOnly="true">
									<ItemStyle Width="8%" />
									</asp:BoundField>
                                <asp:BoundField HeaderText="Peratusan Perbezaan" DataField="Peratus" SortExpression="Peratus" DataFormatString="{0:P}" ItemStyle-HorizontalAlign="Right" ReadOnly="true">
									<ItemStyle Width="3%" />
									</asp:BoundField> 
                                <asp:TemplateField HeaderText="Syor P. Harga" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="2%">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chxHarga" runat="server" Enabled="false" Checked='<%#IIf(IsDBNull(Eval("PO03_SyorNilaiHarga")), False, Eval("PO03_SyorNilaiHarga")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Keutamaan P. Harga" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblKeutamaan" runat="server" Text='<%#Eval("PO03_RankNilaiHarga") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Syor P. Teknikal" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="2%">
                                     <ItemTemplate>
                                        <asp:CheckBox ID="chxTek" runat="server" enabled="false" Checked='<%#IIf(IsDBNull(Eval("PO03_SyorNilaiTek")), False, Eval("PO03_SyorNilaiTek")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Peratus Spesifikasi" ItemStyle-Width="4%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPeratusSpek" runat="server" Text='<%#Eval("PO03_PeratusNilaiTek") %>'></asp:Label>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Syor" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="2%">
                                     <ItemTemplate>
                                        <asp:CheckBox ID="chxSyor" runat="server" enabled="false" Checked='<%#IIf(IsDBNull(Eval("PO03_Syor")), False, Eval("PO03_Syor")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="centerAlign" HeaderText="Surat Niat" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="2%">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChxSrtNiat" runat="server" Name="ChxSrtNiat" onclick="fCheckChildSrtNiat()" ClientIDMode="Static" Checked='<%#IIf(IsDBNull(Eval("PO03_SuratNiat")), False, Eval("PO03_SuratNiat")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="centerAlign" HeaderText="Lantik" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="2%">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" Name="CheckBox1" onclick="fCheckChildSyor()" ClientIDMode="Static" Checked='<%#IIf(IsDBNull(Eval("PO03_Lantik")), False, Eval("PO03_Lantik")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">                                        
                                        <ItemTemplate>                                       
                                            <asp:LinkButton ID="lbDetail" runat="server" CommandName="Select" CssClass="btn-xs " ToolTip="Maklumat Vendor">
											<i class="fas fa-user-tie fa-lg"></i>
                                            </asp:LinkButton> 
                                    </ItemTemplate>              
                                </asp:TemplateField>
                                <asp:TemplateField>
								<ItemTemplate>
									<tr>
										<td colspan="100">
                                            
								<asp:Panel ID="pnlChild" runat="server" Style="margin-left:20px;margin-right:20px; height:0px;overflow: auto;" Width="95%">
											
                                    <asp:GridView ID="gvChild" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="false" EmptyDataText=" Tiada rekod"
								cssclass="table table-bordered table-hover" Width="100%" BorderStyle="Solid" HeaderStyle-BackColor="#FECB18" ShowFooter="true"
									 OnRowCreated ="gvChild_RowCreated" DataKeyNames="PO03_ePID">
									<columns>								 
									<asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" >
								<ItemTemplate>
									<%# Container.DataItemIndex + 1 %>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:BoundField DataField="PO01_Butiran" HeaderText="Barang / Perkara" ItemStyle-Width="25%" HeaderStyle-CssClass="centerAlign" />
							<asp:BoundField DataField="PO03_Jenama" HeaderText="Jenama" ItemStyle-Width="7%" HeaderStyle-CssClass="centerAlign" NullDisplayText=" " />
							<asp:BoundField DataField="PO03_Model" HeaderText="Model" ItemStyle-Width="7%" HeaderStyle-CssClass="centerAlign" NullDisplayText=" "/>
							<asp:BoundField DataField="Negara" HeaderText="Negara Pembuat" ItemStyle-Width="10%" HeaderStyle-CssClass="centerAlign" />							
							<asp:BoundField DataField="PO01_Kuantiti" HeaderText="Kuantiti" ItemStyle-Width="2%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Right"/>
							<asp:BoundField DataField="Ukuran" HeaderText="Ukuran" ItemStyle-Width="3%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField HeaderText="Jenis Harga" SortExpression="PO03_JnsHarga" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center">                                    
                                    <ItemTemplate><%#IIf(CInt(Eval("PO03_JnsHarga")) = 1, "Bercukai (Tanpa GST)", "Tanpa cukai (FOB)")%></ItemTemplate>
                                </asp:TemplateField>                            
							<asp:BoundField DataField="PO03_flagInclusiveGST" HeaderText="Inclusive GST?" ItemStyle-Width="3%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Right" NullDisplayText=" "/>
							<asp:BoundField DataField="PO03_HargaSeunit" HeaderText="Harga Seunit (Tanpa GST) (RM)" ItemStyle-Width="7%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Right" NullDisplayText=" " DataFormatString="{0:N2}"/>
							<asp:BoundField DataField="PO03_JumGST" HeaderText="Cukai GST (RM)" ItemStyle-Width="7%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Right" NullDisplayText=" " DataFormatString="{0:N2}"/>
							<asp:BoundField DataField="PO03_JumTanpaGST" HeaderText="Jumlah Harga (Tanpa GST) (RM)" ItemStyle-Width="7%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Right" NullDisplayText=" " DataFormatString="{0:N2}"/>
							<asp:BoundField DataField="PO03_JumHarga" HeaderText="Jumlah Harga (Termasuk GST) (RM)" ItemStyle-Width="7%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Right" NullDisplayText=" " DataFormatString="{0:N2}"/>       
							<asp:TemplateField HeaderStyle-CssClass="centerAlign" HeaderText="Syor" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="2%">
                                <ItemTemplate>
                                    <asp:CheckBox ID="SyorDt" runat="server" enabled="false" Checked='<%#IIf(IsDBNull(Eval("PO03_SyorDt")), False, Eval("PO03_SyorDt")) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="centerAlign" HeaderText="Surat Niat" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="2%">
                                <ItemTemplate>
                                    <asp:CheckBox ID="ChxSrtNiatDt" runat="server" name="ChxSrtNiatDt" ClientIDMode="Static" onclick="fCheckMainSrtNiat()" Checked='<%#IIf(IsDBNull(Eval("PO03_SuratNiatDt")), False, Eval("PO03_SuratNiatDt")) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="centerAlign" HeaderText="Lantik" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="2%">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1Dt" runat="server" name="CheckBox1Dt" ClientIDMode="Static" onclick="fCheckMainSyor()" Checked='<%#IIf(IsDBNull(Eval("PO03_LantikDt")), False, Eval("PO03_LantikDt")) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                                    </columns>
							</asp:GridView>
                                    
									</asp:Panel>
							   
						</td>
					</tr>
								</ItemTemplate>
								<ItemStyle Width="1%" />
							</asp:TemplateField>		
							</columns>
								<HeaderStyle BackColor="#6699FF" />
						</asp:GridView>
                        <asp:HiddenField ID="hdBool" runat="server" Value="false" />
                      <asp:HiddenField ID="hdBool1" runat="server" Value="false" />
                         <br/><br/>
                        <div class="panel panel-default" style="width:inherit">
                            <div class="panel-heading">
                                <h3 class="panel-title">Senarai Kehadiran Penilai Harga</h3>
                            </div>
                            <div class="panel-body" style="overflow-x:auto">
                                <table style="width:100%" class="table table-borderless table-striped">
                                    <tr>
                                        <td style="width: 10%;">
                                            PTJ :
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlPTJ" runat="server" AutoPostBack="True" CssClass="form-control" Width="90%"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlPTJ" runat="server" ControlToValidate="ddlPTJ" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="btnTambah" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>                                        
                                    </tr>
                                    <tr>
                                        <td style="width: 10%;">
                                            Nama Staf :
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlStaf" runat="server" AutoPostBack="True" CssClass="form-control" Width="90%"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlStaf" runat="server" ControlToValidate="ddlStaf" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="btnTambah" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>                                        
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="text-align:center;">
                                             <asp:LinkButton ID="lbtnTambahStaf" runat="server" CssClass="btn btn-info" ToolTip="Tambah ke senarai" ValidationGroup="btnTambah">
                                            <i class="fa fa-plus-circle fa-lg"></i>&nbsp;&nbsp;&nbsp;Tambah
                                            </asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                                <br/>
                                <asp:GridView ID="gvJawatanKuasa" runat="server" AllowPaging="false" AllowSorting="True" AutoGenerateColumns="False" BorderStyle="Solid" cssclass="table table-striped table-bordered table-hover" 
                                    EmptyDataText=" Tiada rekod" ShowFooter="false" ShowHeaderWhenEmpty="True" Width="100%" DataKeyNames="PO08_StafID" Font-Size="8pt">
                                    <columns>
                                        <asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" >
									        <ItemTemplate>
										        <%# Container.DataItemIndex + 1 %>
									        </ItemTemplate>
								        </asp:TemplateField> 
                                        <asp:BoundField HeaderText="No Staf" DataField="PO08_StafID" SortExpression="PO08_StafID" ReadOnly="true">
						                    <ItemStyle Width="5%" />
					                    </asp:BoundField>
                                        <asp:BoundField HeaderText="Nama" DataField="MS01_Nama" SortExpression="Nama" ReadOnly="true">
						                    <ItemStyle Width="30%" />
					                    </asp:BoundField>
                                       <asp:BoundField HeaderText="PTJ" DataField="Pejabat" SortExpression="Pejabat" ReadOnly="true">
						                    <ItemStyle Width="20%" />
					                    </asp:BoundField>
                                         <asp:BoundField HeaderText="Jawatan" DataField="PO08_JawStaf" SortExpression="PO08_JawStaf" ReadOnly="true">
						                    <ItemStyle Width="20%" />
					                    </asp:BoundField>
                            
                                        <asp:TemplateField ItemStyle-CssClass="centerAlign">
			                                <ItemTemplate>
                                                                        
                                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CssClass="btn-xs" ToolTip="Padam"
                                                OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');">
                                            <i class="fas fa-trash-alt fa-lg"></i>
                                            </asp:LinkButton>
			                            </ItemTemplate>
			                            <ItemStyle Width="3%"/>
			                    
			                            </asp:TemplateField>
                                    </columns>
                                </asp:GridView>
                                <br/>
                                
                            </div>
                        </div>
                        <br />  <br />
                        <div class="panel panel-default" style="width:inherit">
                            <div class="panel-heading">
                                <h3 class="panel-title">Senarai Dokumen</h3>
                            </div>
                            <div class="panel-body" style="overflow-x:auto">
                               
                                <asp:GridView ID="gvLampiran2" runat="server" AutoGenerateColumns="false" BorderColor="#333333" BorderStyle="Solid" ShowHeaderWhenEmpty="true" Font-Size="8pt"
                                         cssclass="table table-striped table-bordered table-hover" DataKeyNames="PO13_ID" EmptyDataText=" Tiada rekod" HeaderStyle-BackColor="#6699FF" Width="100%">
                                         <Columns>
                                             <asp:TemplateField HeaderText="Bil" ItemStyle-Width="2%" >
                                                 <ItemTemplate>
                                                     <%# Container.DataItemIndex + 1 %>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:BoundField DataField="PO13_NamaDok" HeaderText="File Name" ItemStyle-Width="40%" />
                                             <asp:BoundField DataField="JenisDok" HeaderText="Type" ItemStyle-Width="25%" />
                                              <asp:BoundField DataField="PO13_NoMohon" HeaderText="" ItemStyle-Width="1%" visible="false"/>
                                             <asp:HyperLinkField DataNavigateUrlFields="PO13_NoMohon,PO13_NamaDok,Path" DataNavigateUrlFormatString="~/Upload/Document/PO/{2}/{0}/{1}" DataTextField="PO13_ID" DataTextFormatString="&lt;img src='../../../Images/search.png' alt='View' /&gt;" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%" Target="_blank" />
                                             
                                         </Columns>
                                     </asp:GridView>
                                <br/>
                                
                            </div>
                        </div>
                       
                     
                    </div>
                    
                  <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Sila salah pilih satu syor."
    ClientValidationFunction="Validate" ForeColor="Red" ValidationGroup="btnSimpan"></asp:CustomValidator>   
            <div class="row">                
                <div class="col-md-10" style="text-align:center">
                     <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn" ToolTip="Simpan" ValidationGroup="btnSimpan">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
					</asp:LinkButton>
                            </div>
					<div class="col-md-2" style="text-align:right"> 
					<asp:LinkButton ID="lbtnNextView1" runat="server" CssClass="btn btn-default btn-circle btn-lg" ToolTip="Seterusnya">
                                <i class="glyphicon glyphicon-chevron-right"></i>
                            </asp:LinkButton>
                        
					</div>
				</div>
            </div>
         <div class="panel panel-default">
				  <div class="panel-heading">
					<h4 class="panel-title">
					  <a data-toggle="collapse" href="#collapse1"><span class="fas fa-users fa-lg"></span>&nbsp&nbsp Urusetia</a>
					</h4>
				  </div>
				  <div id="collapse1" class="panel-collapse collapse">
					<div class="panel-body">
						<div class="row">                    
					<table style="width:100%;">
						<tr>
							<td style="width: 5%;" >
								<button type="button" id="btnPemohon" class="btn btn-default btn-circle btn-lg" data-toggle="collapse" data-target="#Pemohon" title="Pemohon"><span class="fa fa-user"></span> </button>
							</td>
							<td style="width: 80%;">
								<div id="Pemohon" class="collapse in">
								&nbsp&nbsp Urusetia <br />
								&nbsp&nbsp<asp:TextBox ID="txtNamaUrusetia" runat="server" CssClass="form-control" ReadOnly="true" Width="45%" ToolTip="Nama Pemohon"></asp:TextBox>
								&nbsp<asp:TextBox ID="txtJawUrusetia" runat="server" CssClass="form-control" ReadOnly="true" Width="50%" ToolTip="Jawatan Pemohon"></asp:TextBox>
								</div>
							</td>                        
						</tr>
					</table>                        
				</div>		
                     </div>
					
				  </div>
				</div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lbtnNextView1" />            
        </Triggers>
        </asp:UpdatePanel>
            </asp:View>
        <asp:View ID="View2" runat="server">
            <div class="panel panel-default" >
						<div class="panel-heading">
							<h3 class="panel-title">
								Lampiran Dokumen Pengesyoran
							</h3>
						</div>
						<div class="panel-body">
							<table style="width:100%" class="table table-borderless">					
							 <tr>					
							     <td style="width: 20%; vertical-align:top">
                                     <asp:Label ID="Label27" runat="server" CssClass="control-label" Text="Lampiran"></asp:Label>
                                     &nbsp;:</td>
                                 <td>Jenis-jenis dokumen:
                                     <br />
                                     <asp:RadioButtonList ID="rbDokumenType" runat="server">
                                         <asp:ListItem Text="Laporan Perlantikan" Value="LNTK" />                                         
                                     </asp:RadioButtonList>
                                     <br />
                                     <div class="form-inline">
                                         <asp:FileUpload ID="FileUpload1" runat="server" BackColor="#FFFFCC" CssClass="form-control" Height="25px" Width="50%" />
                                         &nbsp;&nbsp;
                                         <asp:LinkButton ID="lbtnUploadLamp" runat="server" CssClass="btn btn-info" ToolTip="Upload file" ValidationGroup="btnUpload">
											<i class="fa fa-upload fa-lg"></i>&nbsp;&nbsp;&nbsp;Upload
										</asp:LinkButton>
                                     </div><label>Saiz fail muat naik maksimum 5 mb</label>
                                     <br />
                                     <asp:Label ID="LabelMessage1" runat="server" />
                                     <br />
                                     <asp:GridView ID="gvLampiran" runat="server" AutoGenerateColumns="false" BorderColor="#333333" BorderStyle="Solid" ShowHeaderWhenEmpty="true" Font-Size="8pt"
                                         cssclass="table table-striped table-bordered table-hover" DataKeyNames="PO13_ID" EmptyDataText=" Tiada rekod" HeaderStyle-BackColor="#6699FF" Width="80%">
                                         <Columns>
                                             <asp:TemplateField HeaderText="Bil" ItemStyle-Width="2%" >
                                                 <ItemTemplate>
                                                     <%# Container.DataItemIndex + 1 %>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:BoundField DataField="PO13_NamaDok" HeaderText="File Name" ItemStyle-Width="40%" />
                                             <asp:BoundField DataField="JenisDok" HeaderText="Type" ItemStyle-Width="25%" />
                                              <asp:BoundField DataField="PO13_NoMohon" HeaderText="" ItemStyle-Width="1%" visible="false"/>
                                             <asp:HyperLinkField DataNavigateUrlFields="PO13_NoMohon,PO13_NamaDok" DataNavigateUrlFormatString="~/Upload/Document/PO/Perlantikan/{0}/{1}" DataTextField="PO13_ID" DataTextFormatString="&lt;img src='../../../Images/search.png' alt='View' /&gt;" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%" Target="_blank" />
                                             <asp:TemplateField ItemStyle-Width="3%">
                                                 <ItemTemplate>
                                                     <asp:LinkButton ID="lnkDelete" runat="server" CommandName="delete" CssClass="btn-xs" ToolTip="Delete">
														<i class="fas fa-trash-alt fa-lg"></i>
													</asp:LinkButton>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                         </Columns>
                                     </asp:GridView>
                                 </td>
							    </tr>
							</table>
							
							<br />
						   
							<div class="row">
							<div class="col-md-2" style="text-align:left">                    
                                <asp:LinkButton ID="lbtnPrevView2" runat="server" CssClass="btn btn-default btn-circle btn-lg" ToolTip="Sebelumnya">
                                <i class="glyphicon glyphicon-chevron-left"></i>
                                </asp:LinkButton>
								</div>
							   <div class="col-md-10" style="text-align:center">
                                    <asp:LinkButton ID="lbtnHantar" runat="server" CssClass="btn btn-info" ValidationGroup="lbtnHantar" OnClientClick="return confirm('Anda pasti untuk hantar rekod ini?');">
                                        <i class="fab fa-telegram-plane fa-lg"></i>&nbsp;&nbsp;&nbsp;Hantar
                                    </asp:LinkButton>
								</div>
							</div>
					</div>
					</div>

            </asp:View>
        </asp:MultiView>
    
</asp:Content>

