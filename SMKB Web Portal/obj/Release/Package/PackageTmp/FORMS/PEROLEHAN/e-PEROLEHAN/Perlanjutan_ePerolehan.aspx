<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Perlanjutan_ePerolehan.aspx.vb" Inherits="SMKB_Web_Portal.Perlanjutan_ePerolehan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
            height: 23px;
        }
	   
	   </style>
    <script>
        function upper(ustr) {
            var str = ustr.value;
            ustr.value = str.toUpperCase();
        };
    </script>
    <h1>Perlanjutan Tarikh & Masa ePerolehan</h1>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">        
    <ContentTemplate>

        <p></p>

            
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
						Senarai Jualan Naskah</h3>
                </div>
                 <div class="panel-body">                                 
						Tahun: &nbsp;<asp:TextBox ID="txtTahun" runat="server" CssClass="form-control" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp; Status: &nbsp;PROSES JUALAN NASKAH
                            <br /><br />

                <asp:GridView ID="gvPO" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="true" EmptyDataText=" Tiada rekod"
							cssclass="table table-striped table-bordered table-hover" Width="100%" Font-Size="8pt" BorderStyle="Solid" ShowFooter="false">
								<columns>
								 
								<asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" >
									<ItemTemplate>
										<%# Container.DataItemIndex + 1 %>
									</ItemTemplate>
								</asp:TemplateField> 
								<asp:BoundField HeaderText="No Perolehan" DataField="PO01_NoMohon" SortExpression="PO01_NoMohon" HeaderStyle-CssClass="centerAlign">
									<ItemStyle Width="3%" HorizontalAlign="Center"/>
								</asp:BoundField>                                						            
								<asp:BoundField HeaderText="Tujuan" DataField="PO01_Tujuan" SortExpression="PO01_Tujuan" HeaderStyle-CssClass="centerAlign">
									<ItemStyle Width="20%" />
								</asp:BoundField>
								<asp:BoundField HeaderText="Kategori" DataField="ButiranBrg" SortExpression="ButiranBrg" HeaderStyle-CssClass="centerAlign">
									<ItemStyle Width="2%" />
								</asp:BoundField>
								<asp:BoundField HeaderText="No Naskah Jualan" DataField="PO02_JualanID" SortExpression="PO02_JualanID" HeaderStyle-CssClass="centerAlign">
									<ItemStyle Width="3%" HorizontalAlign="Center"/>
								</asp:BoundField>
                                <%--<asp:TemplateField HeaderText="Date Of Birth[Long Date]">
                                    <ItemTemplate>
                                    <asp:Label ID="lblDOB" runat="server" Text='<%# Convert.ToDateTime(Eval("PO02_TrkMasaMulaIklan")).ToString("dddd, dd MMMM yyyy, hh:mm tt", System.Globalization.CultureInfo.CreateSpecificCulture("ms-MY")) %>'></asp:Label>
                                    </ItemTemplate>
                                 </asp:TemplateField>--%>
								<asp:BoundField HeaderText="Tarikh & Masa Mula Iklan" DataField="TarikhMasaMulaIklan" SortExpression="TarikhMasaMulaIklan">
									<ItemStyle Width="10%" />
								</asp:BoundField>
								 <asp:BoundField HeaderText="Tarikh & Masa Tamat Perolehan" DataField="TarikhMasaTamatPO" SortExpression="TarikhMasaTamatPO">
									<ItemStyle Width="10%" />
								</asp:BoundField>
								<asp:BoundField HeaderText="Status" DataField="ButiranStatus" SortExpression="ButiranStatus" HeaderStyle-CssClass="centerAlign">
									<ItemStyle Width="10%" />
								</asp:BoundField>                                                                                          
								<asp:TemplateField>                        
								<ItemTemplate>
										<%--<asp:LinkButton ID="lbDetail" runat="server" CommandName="Select" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="btn-xs btn-primary" ToolTip="Maklumat Lanjut">
											<i class="fa fa-ellipsis-h fa-lg"></i>
										</asp:LinkButton>--%>
										<asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CssClass="btn-xs" ToolTip="Pilih Kemaskini"
                                            data-toggle="collapse" data-target="#collapse1">
											<i class="far fa-edit fa-lg"></i>
										</asp:LinkButton>
									</ItemTemplate>
									<ItemStyle Width="3%" HorizontalAlign="Center"/>
								</asp:TemplateField>
							</columns>
						</asp:GridView>                    
                </div>           
           
                <br />
                <br />
                <div class="panel panel-default" style="overflow:scroll;">
				<div class="panel-heading">
					<h3 class="panel-title">
						<a data-toggle="collapse" href="#collapse1" aria-hidden="true">
                            Maklumat Jualan Naskah
                        </a>
					</h3>
				</div>
                <div id="collapse1" class="panel-collapse collapse in">
				<div class="panel-body">
					<table style="width:100%;" class="table table-borderless table-striped">
					  <tr>
						  <td>ID Naskah Jualan :</td>
						  <td>
							  <asp:TextBox ID="txtIdNJ" runat="server" BackColor="#FFFFCC" CssClass="form-control"  Width="150px" ReadOnly="true"></asp:TextBox>
							  &nbsp;&nbsp;&nbsp;Tarikh:
								&nbsp;<asp:label ID="lblTarikhNJ" runat="server"></asp:label>
								&nbsp;&nbsp;&nbsp;Status:
								&nbsp;<asp:label ID="lblStatus" runat="server" ></asp:label>
						  </td>
					  </tr>
						<tr>
							<td class="auto-style1">No Perolehan :</td>
							<td class="auto-style1">
								<div class="form-inline">
									<asp:label ID="lblNoPO" runat="server"></asp:label>
									<%--&nbsp;&nbsp;
									<asp:LinkButton ID="lbtnCari" runat="server" CssClass="btn-circle" ToolTip="Cari Maklumat Permohonan Pembelian">
											<i class="fas fa-search fa-lg"></i>
										</asp:LinkButton>--%>
								</div>
																	
							</td>
						</tr>
						<tr>
							<td  style="vertical-align:top;">Tujuan Perolehan :</td>
							<td>
								<asp:label ID="lblTujuan" runat="server" ></asp:label>								
							</td>
						</tr>
						<tr>
							<td>Scope :</td>
							<td>                                
								<asp:label ID="lblScope" runat="server" > </asp:label>
							</td>
						</tr>	
						<tr>
							<td>Kategori Perolehan :</td>
							<td>									
								<asp:label ID="lblKategoriPO" runat="server"></asp:label>						
									
							</td> 
						</tr>
                        <tr>
							<td>Kaedah Perolehan :</td>
							<td>									
								<asp:label ID="lblKaedahPO" runat="server" ></asp:label>						
									
							</td> 
						</tr>
                        <tr>
							<td>PTJ Mohon :</td>
							<td>									
								<asp:label ID="lblPTjMohon" runat="server" ></asp:label>						
									
							</td> 
						</tr>
						
					   <tr>
								<td>No Sebut Harga / Tender :</td>
								<td>
									<asp:label ID="lblNoSHTD" runat="server" ></asp:label>
									
								</td>
							</tr>
					  <tr>
							<td>Tempat Hantar:</td>
							<td>
								<asp:label ID="lblTmptHantar" runat="server" ></asp:label>
								
							</td>
						</tr>
					  
					  <tr>
					      <td>Tarikh Mula Iklan :</td>
                          <td>
                              <asp:TextBox ID="txtTrkMulaIklan" runat="server" CssClass="form-control rightAlign" Width="100px" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
                              &nbsp;&nbsp;&nbsp; Masa Mula Iklan &nbsp; :&nbsp;<asp:TextBox ID="txtMasaMulaIklan" runat="server" CssClass="form-control rightAlign" TextMode="Time" Width="100px" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
                              
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
						<td>Tarikh Tamat Perolehan Asal :</td>
						<td>
							<asp:TextBox ID="txtTrkTamatPOLama" runat="server" CssClass="form-control rightAlign" Width="100px" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>							
							&nbsp;&nbsp;&nbsp;
						Masa Tamat Perolehan Asal :&nbsp;<asp:TextBox ID="txtMasaTamatPOLama" runat="server" CssClass="form-control rightAlign" Width="100px" TextMode="Time" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>							
						</td>
					</tr>
                       <tr  class="calendarContainerOverride">                    
						<td>Tarikh Tamat Perolehan Baru :</td>
						<td>
							<asp:TextBox ID="txtTrkTamatPO" contenteditable="false" runat="server" CssClass="form-control rightAlign" Width="100px" ></asp:TextBox>
                            <asp:LinkButton ID="lbtnTrkTamatPO" runat="server" ToolTip="Klik untuk papar kalendar">
                                <i class="far fa-calendar-alt fa-lg"></i>
                            </asp:LinkButton>
							<ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtTrkTamatPO" TodaysDateFormat="dd/MM/yyyy" PopupButtonID="lbtnTrkTamatPO"/>
							<asp:RequiredFieldValidator ID="rfvtxtTrkTamatPO" runat="server" ControlToValidate="txtTrkTamatPO" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="btnSimpan" Display="Dynamic"  ></asp:RequiredFieldValidator>
							&nbsp;&nbsp;&nbsp;
						Masa Tamat Perolehan Baru :&nbsp;<asp:TextBox ID="txtMasaTamatPO" runat="server" CssClass="form-control rightAlign" Width="100px" TextMode="Time"></asp:TextBox>
							<asp:RequiredFieldValidator ID="rfvtxtMasaTamatPO" runat="server" ControlToValidate="txtMasaTamatPO" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="btnSimpan" Display="Dynamic"  ></asp:RequiredFieldValidator>
						</td>
					</tr>                   
						<tr id="trLawatanTapak" runat="server" visible="false">
							<td style="vertical-align:top;">
								Lawatan Tapak :</td>
							<td>								
								<div id="divUlasan" runat="server" visible="false">
									Tempat Tapak :&nbsp;<asp:TextBox ID="txtTmptTapak" runat="server" CssClass="form-control" Width="60%" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
									<br />
									Tarikh Lawatan:&nbsp;<asp:TextBox ID="txtTrkLawTpk" runat="server" CssClass="form-control rightAlign" Width="100px" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
									&nbsp;&nbsp;&nbsp;
									Masa Lawatan:&nbsp;<asp:TextBox ID="txtMasaLawTpk" runat="server" CssClass="form-control" Width="100px" TextMode="time" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
								</div>
							</td>
						</tr>
                        <tr>
                            <td  style="vertical-align:top;">Ulasan perlanjutan tarikh / Masa: </td>
                            <td>
                                <asp:TextBox ID="txtUlasan" runat="server" TextMode="MultiLine" Rows="5" columns="100" onkeyup="upper(this)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtUlasan" runat="server" ControlToValidate="txtUlasan" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="btnSimpan" Display="Dynamic"  ></asp:RequiredFieldValidator>
                            </td>
							</tr>
				  </table>					
					<div class="row">
				   <div class="col-md-12" style="text-align:center; height: 17px;">                                     
						<asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn btn-info" ValidationGroup="btnSimpan">
						<i class="fa fa-floppy-o fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
					</asp:LinkButton>
					</div>				                    
				</div>
			   </div>
				</div>
                </div>
		    </div>
            </div>
        
     
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
