<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Naskah_Jualan.aspx.vb" Inherits="SMKB_Web_Portal.Naskah_Jualan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	 <h1>Naskah Jualan</h1>

		<link rel="stylesheet" type="text/css" href="../../../Content/Site.css">

	
	<style type="text/css">
	    .calendarContainerOverride table {
	        width: 0px;
	        height: 0px;
	    }

	        .calendarContainerOverride table tr td {
	            padding: 0;
	            margin: 0;
	        }
	</style>			

    <div class="stepwizard">
        <div class="stepwizard-row">
            <div class="stepwizard-step">
                <button id="btnStep1" type="button" class="btn-default btn-circle">1</button>
                <p>Maklumat Iklan</p>
            </div>
            <div class="stepwizard-step">
                <button id="btnStep2" type="button" class="btn-default btn-circle">2</button>
                <p>Maklumat Lesen</p>
            </div>
            <div class="stepwizard-step">
                <button id="btnStep3" type="button" class="btn-default btn-circle">3</button>
                <p>Muat Naik Dok.</p>
            </div>
            <div class="stepwizard-step">
                <button id="btnStep4" type="button" class="btn-default btn-circle">4</button>
                <p>Pengesahan / Ringkasan</p>
            </div>            
        </div>
    </div>
    <div class="row">
				<asp:LinkButton ID="lbtnKembali" runat="server" CssClass="btn btn-info">
					<i class="fa fa-arrow-left fa-lg"></i>&nbsp;&nbsp;&nbsp;Kembali
				</asp:LinkButton>
			</div>
			<br />
		<div class="row">
			<asp:MultiView ID="mvNaskahJualan" runat="server" ActiveViewIndex="0" >
				<asp:View ID="View1" runat="server">

            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">        
	            <ContentTemplate>

			<div class="panel panel-default" >
				<div class="panel-heading">
					<h3 class="panel-title">
						Maklumat Jualan Naskah
					</h3>
				</div>
				<div class="panel-body">
					<table class="table table-borderless table-striped">
					  <tr>
						  <td style="width: 20%">ID Naskah Jualan :</td>
						  <td style="width: 80%">
                              <asp:TextBox ID="txtIdNJ" runat="server" BackColor="#FFFFCC" CssClass="form-control"  Width="150px" ReadOnly="true"></asp:TextBox>
							  &nbsp;&nbsp;&nbsp;Tarikh:
								&nbsp;<asp:label ID="lblTarikhNJ" runat="server"  Width="100px"></asp:label>
								&nbsp;&nbsp;&nbsp;Status:
								&nbsp;<asp:label ID="lblStatus1" runat="server"  ></asp:label>
						  </td>
					  </tr>
						<tr>
							<td style="width: 20%;">No Perolehan :</td>
							<td>
								<div class="form-inline">
									<asp:label ID="lblNoPO1" runat="server" ></asp:label>
									&nbsp;&nbsp;
									<asp:LinkButton ID="lbtnCari" runat="server" CssClass="btn-circle" ToolTip="Cari Maklumat Permohonan Pembelian">
											<i class="fas fa-search fa-lg"></i>
										</asp:LinkButton>
                                    <asp:HiddenField ID="hfNoMohonSem" runat="server" />
								</div>
																	
							</td>
						</tr>
						<tr>
							<td  style="vertical-align:top; width: 20%;">Tujuan Perolehan :</td>
							<td>
								<asp:label ID="lblTujuan1" runat="server" ></asp:label>								
							</td>
						</tr>
						<tr >
							<td style="width: 20%;">Scope :</td>
							<td>                                
								<asp:label ID="lblScope1" runat="server" > </asp:label>
							</td>
						</tr>	
						<tr>
							<td style="width: 20%;">Kategori Perolehan :</td>
							<td>									
								<asp:label ID="lblKategoriPO1" runat="server" ></asp:label>						
									
							</td> 
						</tr>
                        <tr>
							<td style="width: 20%;">Kaedah Perolehan :</td>
							<td>									
								<asp:label ID="lblKaedahPO" runat="server"></asp:label>						
									
							</td> 
						</tr>
                        <tr>
							<td style="width: 20%;">PTJ Mohon :</td>
							<td>									
								<asp:label ID="lblPTjMohon" runat="server"  ></asp:label>						
									
							</td> 
						</tr>
						<tr id="trVendor" runat="server" visible="false">
								<td style="vertical-align:top;width:15%;"> 
									<b>Rundingan Terus</b>
								</td>
								<td>
                                    <asp:CheckBox ID="chxRundinganTerus" runat="server" Text=" Rundingan Terus" AutoPostBack="true" ToolTip="Klik jika pemilihan vendor adalah melalui rundingan terus"/>                                 
                                    <i class="fas fa-question-circle fa-lg" aria-hidden="true" data-toggle="tooltip" data-placement="right" title="Vendor adalah satu-satunya syarikat yang memasuki e-Perolehan."></i>
                                    <br />
                                    Vendor &nbsp;<asp:DropDownList ID="ddlVendor" runat="server" AutoPostBack="True" CssClass="form-control" Width="80%" Enabled="false"></asp:DropDownList>
                                   <asp:RequiredFieldValidator ID="rfvddlVendor" runat="server" ControlToValidate="ddlVendor" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="btnSimpan" Enabled="false" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>
                                    <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn-sm" ToolTip="Cari">
					                    <i class="fa fa-hand-o-left fa-lg"></i>
				                    </asp:LinkButton>
                                </td>
                            </tr>
						<tr>
							<td style="width: 20%;">Harga Naskah :</td>
							<td>                                
								<asp:TextBox ID="txtHrgNskh" runat="server" CssClass="form-control rightAlign" Width="100px" TextMode="Number"> </asp:TextBox>
                                <asp:RegularExpressionValidator ID="revtxtHrgNskh" ControlToValidate="txtHrgNskh" runat="server" ErrorMessage=" Hanya format ringgit dibenarkan" ValidationExpression="(?=.)^\$?(([1-9][0-9]{0,2}(,[0-9]{3})*)|[0-9]+)?(\.[0-9]{1,2})?$" ValidationGroup="btnSimpan" CssClass="fontValidatation" ForeColor="Red"></asp:RegularExpressionValidator>
								<asp:RequiredFieldValidator ID="rfvtxtHrgNskh" runat="server" ControlToValidate="txtHrgNskh" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="btnSimpan" Display="Dynamic"  ></asp:RequiredFieldValidator>
							</td>
						</tr>	
					   <tr>
								<td style="width: 20%;">No Sebut Harga / Tender :</td>
								<td>
									<asp:TextBox ID="txtNoSHTD" runat="server" CssClass="form-control" Width="40%"></asp:TextBox>
									<asp:RequiredFieldValidator ID="rfvtxtNoSHTD" runat="server" ControlToValidate="txtNoSHTD" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="btnSimpan" Display="Dynamic"></asp:RequiredFieldValidator>
								</td>
							</tr>
					  <tr>
							<td style="width: 20%;">Tempat Hantar:</td>
							<td>
								<asp:TextBox ID="txtTmptHantar" runat="server" CssClass="form-control" Width="90%"></asp:TextBox>
								<asp:RequiredFieldValidator ID="rfvtxtTmptHantar" runat="server" ControlToValidate="txtTmptHantar" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="btnSimpan" Display="Dynamic"></asp:RequiredFieldValidator>
							</td>
						</tr>
					  
					  <tr class="calendarContainerOverride">
					      <td style="width: 20%">Tarikh Mula Iklan :</td>
                          <td style="width: 80%">
                              <asp:TextBox ID="txtTrkMulaIklan" runat="server" CssClass="form-control rightAlign" Width="100px"></asp:TextBox>                             
                            <asp:LinkButton ID="lbtnTrkMulaIklan" runat="server" ToolTip="Klik untuk papar kalendar">
                                <i class="far fa-calendar-alt fa-lg"></i>
                            </asp:LinkButton>
                              <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtTrkMulaIklan" TodaysDateFormat="dd/MM/yyyy" PopupButtonID="lbtnTrkMulaIklan" />                              
                              &nbsp;&nbsp;&nbsp; Masa Mula Iklan &nbsp; :&nbsp;<asp:TextBox ID="txtMasaMulaIklan" runat="server" CssClass="form-control rightAlign" TextMode="Time" Width="100px" ></asp:TextBox>
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMasaMulaIklan" Display="Dynamic" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="btnSimpan"></asp:RequiredFieldValidator>
                              <asp:RegularExpressionValidator ID="revtxtMasaMulaIklan" ControlToValidate="txtTrkMulaIklan" runat="server" ErrorMessage=" Sila pastikan format tarikh seperti ini: dd/MM/yyyy" ValidationExpression="^([0-2][0-9]|(3)[0-1])(\/)(((0)[0-9])|((1)[0-2]))(\/)\d{4}$" ValidationGroup="btnSimpan" ForeColor="Red"></asp:RegularExpressionValidator>
                          </td>
					<tr class="calendarContainerOverride">
						<td>
							Tarikh Mula Perolehan :
						</td>
						<td>
							<asp:TextBox ID="txtTrkMulaPO" runat="server" CssClass="form-control rightAlign" Width="100px" ></asp:TextBox>
							<ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtTrkMulaPO" TodaysDateFormat="dd/MM/yyyy" PopupButtonID="lbtnTrkMulaPO"/>
							<asp:LinkButton ID="lbtnTrkMulaPO" runat="server" ToolTip="Klik untuk papar kalendar">
                                <i class="far fa-calendar-alt fa-lg"></i>
                            </asp:LinkButton>
                            
							&nbsp;&nbsp;&nbsp;
							Masa Mula Perolehan
							&nbsp;
							:&nbsp;<asp:TextBox ID="txtMasaMulaPO" runat="server" CssClass="form-control rightAlign" Width="100px" TextMode="Time"></asp:TextBox>
							<asp:RequiredFieldValidator ID="rfvtxtMasaMulaPO" runat="server" ControlToValidate="txtMasaMulaPO" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="btnSimpan" Display="Dynamic"  ></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtTrkMulaPO" ControlToValidate="txtTrkMulaPO" runat="server" ErrorMessage=" Sila pastikan format tarikh seperti ini: dd/MM/yyyy" ValidationExpression="^([0-2][0-9]|(3)[0-1])(\/)(((0)[0-9])|((1)[0-2]))(\/)\d{4}$" ValidationGroup="btnSimpan" ForeColor="Red"></asp:RegularExpressionValidator>
						</td>
					</tr >
					<tr class="calendarContainerOverride">                    
						<td style="width: 20%">Tarikh Tamat Perolehan &nbsp;:</td>
						<td style="width: 80%">
							<asp:TextBox ID="txtTrkTamatPO" runat="server" CssClass="form-control rightAlign" Width="100px" ></asp:TextBox>
							<ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtTrkTamatPO" TodaysDateFormat="dd/MM/yyyy" PopupButtonID="lbtnTrkTamatPO"/>
							<asp:LinkButton ID="lbtnTrkTamatPO" runat="server" ToolTip="Klik untuk papar kalendar">
                                <i class="far fa-calendar-alt fa-lg"></i>
                            </asp:LinkButton>
                            
							&nbsp;&nbsp;&nbsp;
						Masa Tamat Perolehan&nbsp; :&nbsp;<asp:TextBox ID="txtMasaTamatPO" runat="server" CssClass="form-control rightAlign" Width="100px" TextMode="Time"></asp:TextBox>
							<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtMasaTamatPO" ErrorMessage="" ForeColor="Red" Text=" *Sila isi" ValidationGroup="btnSimpan" Display="Dynamic"  ></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtTrkTamatPO" ControlToValidate="txtTrkTamatPO" runat="server" ErrorMessage=" Sila pastikan format tarikh seperti ini: dd/MM/yyyy" ValidationExpression="^([0-2][0-9]|(3)[0-1])(\/)(((0)[0-9])|((1)[0-2]))(\/)\d{4}$" ValidationGroup="btnSimpan" ForeColor="Red"></asp:RegularExpressionValidator>
						</td>
					</tr>
                    <tr id="trGred" runat="server" visible="false">
						<td>
							Gred :
						</td>
                        <td>
                            <asp:DropDownList ID="ddlGred" runat="server" AutoPostBack="True" CssClass="form-control" Width="80%"></asp:DropDownList>
                        </td>
					</tr>
						<tr id="trLawatanTapak" runat="server" class="calendarContainerOverride">
							<td style="width: 20%; vertical-align:top;">
								Lawatan Tapak :</td>
							<td>
								<asp:RadioButtonList ID="rbLawTpk" runat="server" Height="25px" Width="273px" RepeatDirection="Horizontal" AutoPostBack="true">
									<asp:ListItem Text=" Tidak" Value="0" Selected="True" />
									<asp:ListItem Text=" Ya" Value="1" />
								</asp:RadioButtonList> 
								<div id="divUlasan" runat="server" visible="false">
									Tempat Tapak :&nbsp;<asp:TextBox ID="txtTmptTapak" runat="server" CssClass="form-control" Width="90%"></asp:TextBox>
									<asp:RequiredFieldValidator ID="rfvtxtTmptTapak" runat="server" ControlToValidate="txtTmptTapak" ErrorMessage="" ForeColor="Red" Text=" *Sila isi" ValidationGroup="btnSimpan" Display="Dynamic"  ></asp:RequiredFieldValidator>
									<br />
									Tarikh Lawatan:&nbsp;<asp:TextBox ID="txtTrkLawTpk" runat="server" CssClass="form-control rightAlign" Width="100px" ></asp:TextBox>
									<asp:LinkButton ID="lbtnTrkLawTpk" runat="server" ToolTip="Klik untuk papar kalendar">
                                        <i class="far fa-calendar-alt fa-lg"></i>
                                    </asp:LinkButton>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtTrkLawTpk" TodaysDateFormat="dd/MM/yyyy" PopupButtonID="lbtnTrkLawTpk"/>
                                    
									&nbsp;&nbsp;&nbsp;
									Masa Lawatan:&nbsp;<asp:TextBox ID="txtMasaLawTpk" runat="server" CssClass="form-control" Width="100px" TextMode="time"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtMasaLawTpk" runat="server" ControlToValidate="txtMasaLawTpk" ErrorMessage="" ForeColor="Red" Text=" *Sila isi" ValidationGroup="btnSimpan" Display="Dynamic"  ></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revtxtTrkLawTpk" ControlToValidate="txtTrkLawTpk" runat="server" ErrorMessage=" Sila pastikan format tarikh seperti ini: dd/MM/yyyy" ValidationExpression="^([0-2][0-9]|(3)[0-1])(\/)(((0)[0-9])|((1)[0-2]))(\/)\d{4}$" ValidationGroup="btnSimpan" ForeColor="Red" Enabled="false"></asp:RegularExpressionValidator>
								</div>
							</td>
						</tr>
                        <tr>
                                <td  style="vertical-align:top; width: 15%;">Arahan dan Maklumat kepada Pembekal/ Kontraktor/ Syarikat: </td>
                                <td style="width: 85%;">
                                    <%--columns="100"--%>
                                    <asp:TextBox ID="txtSyarat" runat="server" TextMode="MultiLine" Rows="10" columns="100"></asp:TextBox><br />
                                    <%--<ajaxToolkit:HtmlEditorExtender ID="HtmlEditorExtender1" TargetControlID="txtSyarat" runat="server" DisplaySourceTab="false">
                                        <Toolbar>
                                            <ajaxToolkit:Bold />                                                
                                            <ajaxToolkit:InsertOrderedList />
                                            <ajaxToolkit:InsertUnorderedList />                                                    
                                        </Toolbar>
                                    </ajaxToolkit:HtmlEditorExtender>--%>
                                </td>
							</tr>
				  </table>
					<br />
					
					<div class="row" style="text-align:center">
				                      
						<asp:Button ID="btnSimpan" text="Simpan" runat="server" CssClass="btn" ValidationGroup="btnSimpan" /> &nbsp;                    
						
                       &nbsp;&nbsp;&nbsp;                   
						<asp:LinkButton ID="lbtnNextView1" runat="server" CssClass="btn btn-default btn-circle btn-lg" ToolTip="Seterusnya">
                                <i class="glyphicon glyphicon-chevron-right"></i>
                            </asp:LinkButton>
				
			   </div>
				</div>
		    </div>

		<div class="panel panel-default">
				  <div class="panel-heading">
					<h4 class="panel-title">
					  <a data-toggle="collapse" href="#collapse1"><span class="fas fa-users fa-lg"></span>&nbsp&nbsp Pengguna</a>
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
								&nbsp&nbsp Pemohon <br />
								&nbsp&nbsp<asp:TextBox ID="txtNamaPemohon" runat="server" CssClass="form-control" ReadOnly="true" Width="45%" ToolTip="Nama Pemohon"></asp:TextBox>
								&nbsp<asp:TextBox ID="txtJawPemohon" runat="server" CssClass="form-control" ReadOnly="true" Width="50%" ToolTip="Jawatan Pemohon"></asp:TextBox>
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
                    <asp:UpdatePanel id=updatepanel2 runat="server">
                        <ContentTemplate>

                       
					<div class="panel panel-default" >
						<div class="panel-heading">
							<h3 class="panel-title">
								Maklumat Jualan Naskah</h3>
						</div>
						<div class="panel-body">
							<table style="width:100%" class="table table-borderless table-striped table-bordered">
						    

                                
							 <tr>
							<td style="width: 20%;vertical-align:top">Lesen Pendaftaran :</td>
							<td>
								<table style="width:100%" class="table table-borderless table-striped table-bordered">
									<tr>
										<td>
											<asp:DropDownList ID="ddlLesenDaftar" runat="server" AutoPostBack="True" CssClass="form-control" Width="70%"></asp:DropDownList>
									<asp:RequiredFieldValidator ID="rfvddlLesenDaftar" runat="server" ControlToValidate="ddlLesenDaftar"  Display="Dynamic" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="lbtnTambahLesen"></asp:RequiredFieldValidator>
										</td>
									</tr>
									<tr>
										<td>
											Maklumat lanjut: <br />
											<asp:TextBox ID="txtDetail" runat="server" CssClass="form-control" Width="70%" TextMode="MultiLine"></asp:TextBox>	  
										</td>
									</tr>
									<tr >
										<td >
                                            <div style="text-align:center;">
											<asp:LinkButton ID="lbtnTambahLesen" runat="server" CssClass="btn btn-primary" ValidationGroup="lbtnTambahLesen" ToolTip="Tambah ke senarai">
												<i class="fa fa-plus-circle fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
											</asp:LinkButton>
                                            </div>
										</td>
									</tr>
									<tr>
										<td>
											<asp:GridView ID="gvLesen" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="false" EmptyDataText=" Tiada rekod"
								cssclass="table table-bordered table-hover" Width="100%" BorderStyle="Solid" HeaderStyle-BackColor="#FECB18" DataKeyNames="PO02_kodLesen" Font-Size="8pt">
										<Columns>
											<asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%">
												<ItemTemplate>
													<%# Container.DataItemIndex + 1 %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:BoundField DataField="PO02_kodLesen" HeaderText="Kod Pendaftaran" ItemStyle-Width="10%" />
											<asp:BoundField DataField="Butiran" HeaderText="Lesen Pendaftaran" ItemStyle-Width="30%" />
											<asp:BoundField DataField="PO02_Detail" HeaderText="Maklumat lanjut" ItemStyle-Width="50%" />
											<asp:TemplateField ItemStyle-Width="3%">
												<ItemTemplate>
													<asp:LinkButton ID = "lbtnDelete"  CssClass="btn-xs" CommandName="Delete" ToolTip="Delete"  runat = "server" OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');">
														<i class="fas fa-trash-alt fa-lg"></i>
													</asp:LinkButton>
												</ItemTemplate>
											</asp:TemplateField>
										</Columns>
									</asp:GridView>
										</td>
									</tr>
								</table>
								
							</td>
						</tr>
							 
						<tr id="trMOF" runat="server" visible="false">
								<td style="width: 20%;vertical-align:top" >Bekalan &amp; Perkhidmatan (MOF) :</td>
								<td >
									<table style="width:100%" class="table table-borderless table-striped table-bordered">
									<tr>
										<td>
									Bidang Utama &nbsp;
									:&nbsp;<asp:DropDownList ID="ddlBidangUtama" runat="server" AutoPostBack="True" CssClass="form-control" Width="80%"></asp:DropDownList>
									</td>
										</tr>
									<tr>
										<td>
											Sub Bidang &nbsp;
									:&nbsp<asp:DropDownList ID="ddlSubBidang" runat="server" AutoPostBack="True" CssClass="form-control" Width="80%"></asp:DropDownList>
									
										</td>
									</tr>
                                    <tr>
										<td>
											Situasi Keperluan &nbsp;
									:&nbsp<asp:DropDownList ID="ddlSituasiBidang" runat="server" AutoPostBack="True" CssClass="form-control" Width="100px">
                                        <asp:ListItem Text=" dan" Value=1 ></asp:ListItem>
                                        <asp:ListItem Text=" atau" Value=2 Selected="True"></asp:ListItem>
                                        <asp:ListItem Text=" Terakhir" Value=0 ></asp:ListItem>
									      </asp:DropDownList>
									
										</td>
									</tr>
									<tr>
										<td>
											Bidang &nbsp;:
									<asp:GridView ID="gvBidang" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="false" EmptyDataText =" Tiada rekod"
						cssclass="table table-striped table-bordered table-hover" Width="100%"  BorderStyle="Solid" HeaderStyle-BackColor="#FECB18" ShowFooter="False" DataKeyNames="KodBidang" Font-Size="8pt">
							<columns>
							<asp:TemplateField HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="2%" HeaderText="Pilih">
								<%--<HeaderTemplate>
									<asp:CheckBox ID="checkAll" runat="server" text="All" onclick = "checkAll(this);" />
								</HeaderTemplate>--%>
								<ItemTemplate>
										<%--<asp:CheckBox ID="CheckBox1" runat="server" onclick = "Check_Click(this)" />--%>
                                        <asp:RadioButton id="rbPilih" runat="server" GroupName="rbPilih" onclick = "RadioCheck(this);"/>
									</ItemTemplate>
								</asp:TemplateField>
							<asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" >
								<ItemTemplate>
									<%# Container.DataItemIndex + 1 %>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:BoundField DataField="KodBidang" HeaderText="Kod Bidang" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"/>
							<asp:BoundField DataField="Butiran" HeaderText="Bidang" ItemStyle-Width="50%"/>
							   
						</columns>
					</asp:GridView>
										</td>
									</tr>
									<tr style="text-align:center;">
										<td>
											<asp:LinkButton ID="lbtnTambahMOF" runat="server" CssClass="btn btn-primary" ValidationGroup="btnSaveButiran" ToolTip="Tambah ke senarai">
												<i class="fa fa-plus-circle fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
											</asp:LinkButton>
										</td>
									</tr>
										<tr>
											<td>
												<asp:GridView ID="gvSyarikatBidang" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="false" EmptyDataText=" Tiada rekod"
						cssclass="table table-striped table-bordered table-hover" Width="100%"  BorderStyle="Solid" HeaderStyle-BackColor="#FECB18" ShowFooter="False" DataKeyNames="PO02_KodBidang" Font-Size="8pt">
							<columns>
							<asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" >
								<ItemTemplate>
									<%# Container.DataItemIndex + 1 %>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:BoundField DataField="PO02_KodBidang" HeaderText="Kod Bidang" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"/>
							<asp:BoundField DataField="Butiran" HeaderText="Bidang" ItemStyle-Width="50%" />
                            <asp:BoundField DataField="Condition" HeaderText="Situasi Keperluan" ItemStyle-Width="20%" />                              
							<asp:TemplateField ItemStyle-HorizontalAlign="Center">
									<ItemTemplate>                                      

										<asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete" CssClass="btn-xs" ToolTip="Padam"
											OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');">
										<i class="fas fa-trash-alt fa-lg"></i>
										</asp:LinkButton>
									</ItemTemplate>
									<ItemStyle Width="3%" />
								
							</asp:TemplateField>
						</columns>
					</asp:GridView>
											</td>
										</tr>
									</table>
									
									

								
											
	
								</td>
							</tr>
							
							
						<tr id="trCIDB" runat="server" visible="false">
								<td style="width: 20%;vertical-align:top;">Kerja (CIDB) :</td>
								<td >
									<table style="width:100%" class="table table-borderless table-striped table-bordered">
									
									<tr>
										<td>
											Kategori :&nbsp<asp:DropDownList ID="ddlKategori" runat="server" AutoPostBack="True" CssClass="form-control" Width="80%"></asp:DropDownList>
										</td>
									</tr>
                                        <tr>
										<td>
											Situasi Keperluan &nbsp;
									:&nbsp<asp:DropDownList ID="ddlSituasiCIDB" runat="server" AutoPostBack="True" CssClass="form-control" Width="100px">                                        
                                        <asp:ListItem Text=" dan" Value=1></asp:ListItem>
                                        <asp:ListItem Text=" atau" Value=2 Selected="True"></asp:ListItem>
                                        <asp:ListItem Text=" Terakhir" Value=0></asp:ListItem>
									      </asp:DropDownList>
									
										</td>
									</tr>
										<tr>
											<td>
												Pengkhususan &nbsp;                                
									<asp:GridView ID="gvKhususCIDB" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="false" Font-Size="8pt"
						cssclass="table table-striped table-bordered table-hover" Width="100%"  BorderStyle="Solid" ShowFooter="False" EmptyDataText=" Tiada rekod" DataKeyNames="KodKhusus">
							<columns>
							<asp:TemplateField HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="2%" HeaderText="Pilih">
								<%--<HeaderTemplate>
									<asp:CheckBox ID="checkAll" runat="server" text="All" onclick = "checkAll(this);" />
								</HeaderTemplate>--%>
									<ItemTemplate>
										<%--<asp:CheckBox ID="CheckBox1" runat="server" onclick = "Check_Click(this)" />--%>
                                        <asp:RadioButton id="rbPilih" runat="server" GroupName="rbPilih" onclick = "RadioCheck2(this);"/>
									</ItemTemplate>
								</asp:TemplateField>
							<asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%">
								<ItemTemplate>
									<%# Container.DataItemIndex + 1 %>
								</ItemTemplate>
							</asp:TemplateField>						    
							<asp:BoundField DataField="KodKhusus" HeaderText="Kod Khusus" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="centerAlign" HeaderStyle-HorizontalAlign="Center"/>
							<asp:BoundField DataField="Butiran" HeaderText="Pengkhususan" ItemStyle-Width="30%" HeaderStyle-CssClass="centerAlign"/>                                                               
						</columns>
					</asp:GridView>
											</td>
										</tr>
										<tr style="text-align:center;">
											<td>
												<asp:LinkButton ID="lbtnTambahCIDB" runat="server" CssClass="btn btn-primary" ValidationGroup="btnSaveButiran" ToolTip="Tambah ke senarai">
													<i class="fa fa-plus-circle fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
												</asp:LinkButton>
											</td>
										</tr>
										<tr>
											<td>
												<asp:GridView ID="GVSyarikatCIDB" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="false" Font-Size="8pt"
						cssclass="table table-striped table-bordered table-hover" Width="100%"  BorderStyle="Solid" ShowFooter="False" EmptyDataText=" Tiada rekod" DataKeyNames="PO02_KodKhusus">
							<columns>
							<asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" >
								<ItemTemplate>
									<%# Container.DataItemIndex + 1 %>
								</ItemTemplate>
							</asp:TemplateField>
							
							<asp:BoundField DataField="PO02_KodKategori" HeaderText="Kod Kategori" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="centerAlign"/>
							<asp:BoundField DataField="PO02_KodKhusus" HeaderText="Kod Khusus" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="centerAlign"/>
							<asp:BoundField DataField="Butiran" HeaderText="Pengkhususan" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="centerAlign"/>                                    
                            <asp:BoundField DataField="Condition" HeaderText="Situasi Keperluan" ItemStyle-Width="20%" />
							<asp:TemplateField>
									<ItemTemplate>                                        
										<asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete" CssClass="btn-xs" ToolTip="Padam"
											OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');">
										<i class="fas fa-trash-alt fa-lg"></i>
										</asp:LinkButton>
									</ItemTemplate>
									<ItemStyle Width="3%" />
								
							</asp:TemplateField>
						</columns>
					</asp:GridView>
											</td>
										</tr>
									</table>
									
									
								</td>
							</tr>
                                </table>
                               
						<%--<tr>
							<td style="width: 20%; vertical-align:top;"><Label class="control-label" for="">Syarat-Syarat Lain</Label></td>
							<td>
								<div class="form-inline">
								:&nbsp<asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" Width="70%" TextMode="MultiLine"></asp:TextBox>
		  
								
								<asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-primary" ValidationGroup="btnSaveButiran" ToolTip="Tambah ke senarai">
									<i class="fa fa-plus-circle fa-lg"></i>&nbsp;&nbsp;&nbsp;Tambah
								</asp:LinkButton>
								</div>
								<asp:GridView ID="gvSyaratLain" runat="server" AutoGenerateColumns="false" EmptyDataText = "No Data" Width="80%" cssclass="table table-striped table-bordered table-hover"
										HeaderStyle-BackColor="#6699FF" Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" ShowHeaderWhenEmpty="true">
										<Columns>
											<asp:BoundField DataField="Text" HeaderText="Syarat-Syarat Lain" ItemStyle-Width="70%" />
											
											<asp:TemplateField ItemStyle-Width="3%">
												<ItemTemplate>
													<asp:LinkButton ID = "lnkDelete"  CssClass="btn-xs" ToolTip="Delete"  runat = "server" >
														<i class="fa fa-trash-o fa-lg"></i>
													</asp:LinkButton>
												</ItemTemplate>
											</asp:TemplateField>
										</Columns>
									</asp:GridView>
							</td>
						</tr>--%>
                            <div class="row" style="text-align:center">
							                  
								<asp:LinkButton ID="lbtnPrevView2" runat="server" CssClass="btn btn-default btn-circle btn-lg" ToolTip="Sebelumnya">
                                <i class="glyphicon glyphicon-chevron-left"></i>
                            </asp:LinkButton> 
								              &nbsp;  &nbsp;    
									<asp:LinkButton ID="lbtnNextView2" runat="server" CssClass="btn btn-default btn-circle btn-lg" ToolTip="Seterusnya">
                                <i class="glyphicon glyphicon-chevron-right"></i>
                            </asp:LinkButton>
								
								
							</div>
                                </div>
                        </div>

                             </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="lbtnNextView2" />
                            <asp:PostBackTrigger ControlID="lbtnPrevView2" />
                        </Triggers>
                    </asp:UpdatePanel>
                                </asp:View>

				<asp:View ID="View3" runat="server">
					 <div class="panel panel-default" >
						<div class="panel-heading">
							<h3 class="panel-title">
								Muat Naik Dokumen
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
                                         <asp:ListItem Text="Syarat-syarat am" Value="A" />
                                         <asp:ListItem Text="Akaun Pembida" Value="B" />
                                         <asp:ListItem Text="Jaminan Pembekal" Value="C" />
                                         <asp:ListItem Text="MTO" Value="D" />
                                         <asp:ListItem Text="Jadual Penentuan Teknikal" Value="E" />
                                     </asp:RadioButtonList>
                                     <br />
                                     <div class="form-inline">
                                         <asp:FileUpload ID="FileUpload1" runat="server" BackColor="#FFFFCC" CssClass="form-control" Height="25px" Width="50%" />
                                         &nbsp;&nbsp;
                                         <asp:LinkButton ID="lbtnUploadLamp" runat="server" CssClass="btn btn-info" ToolTip="Upload file" ValidationGroup="btnUpload">
											<i class="fa fa-upload fa-lg"></i>&nbsp;&nbsp;&nbsp;Upload
										</asp:LinkButton>
                                     </div>
                                     <label>Saiz fail muat naik maksimum 5 mb</label>
                                     <br />
                                     <asp:Label ID="LabelMessage1" runat="server" />
                                     <br />
                                     <asp:GridView ID="gvLampiran" runat="server" AutoGenerateColumns="false" BorderColor="#333333" BorderStyle="Solid" ShowHeaderWhenEmpty="true" Font-Size="8pt"
                                         cssclass="table table-striped table-bordered table-hover" DataKeyNames="PO13_ID" EmptyDataText=" Tiada rekod" HeaderStyle-BackColor="#6699FF" Width="80%">
                                         <Columns>
                                             <asp:TemplateField HeaderStyle-CssClass="centerAlign" HeaderText="Bil" ItemStyle-Width="2%">
                                                 <ItemTemplate>
                                                     <%# Container.DataItemIndex + 1 %>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:BoundField DataField="PO13_NamaDok" HeaderText="Nama Fail" ItemStyle-Width="40%" />
                                             <asp:BoundField DataField="JenisDok" HeaderText="Type" ItemStyle-Width="25%" />
                                              <asp:BoundField DataField="PO13_NoMohon" HeaderText="" ItemStyle-Width="1%" visible="false"/>
                                             <asp:HyperLinkField DataNavigateUrlFields="PO13_NoMohon,PO13_NamaDok" DataNavigateUrlFormatString="~/Upload/Document/PO/JualanNaskah/{0}/{1}" DataTextField="PO13_ID" DataTextFormatString="&lt;img src='../../../Images/search.png' alt='View' /&gt;" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%" Target="_blank" />
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
							
							</table>
							
							<br />
						   
							<div class="row" style="text-align:center">
							              
								<asp:LinkButton ID="lbtnPrevView3" runat="server" CssClass="btn btn-default btn-circle btn-lg" ToolTip="Sebelumnya">
                                <i class="glyphicon glyphicon-chevron-left"></i>
                            </asp:LinkButton> 
								              &nbsp;&nbsp; 
									<asp:LinkButton ID="lbtnNextView3" runat="server" CssClass="btn btn-default btn-circle btn-lg" ToolTip="Seterusnya">
                                <i class="glyphicon glyphicon-chevron-right"></i>
                            </asp:LinkButton>                  
								
								
							</div>
					</div>
					</div>
					</asp:View>
				 <asp:View ID="View4" runat="server">
					 <div class="panel panel-default" >
					<div class="panel-heading">
							<h3 class="panel-title">
								Ringkasan Maklumat Jualan Naskah</h3>
						</div>
					<div class="panel-body">
			 
						   <br />
						<table style="width:100%;" class="table table-borderless table-striped">
					  <tr>
						  <td>ID Jualan Naskah :</td>
						  <td>
							  <asp:Label ID="lblIdJualan" runat="server" BackColor="#FFFFCC"></asp:Label>
									&nbsp;&nbsp;&nbsp;Tarikh:
									&nbsp;<asp:Label ID="lblTarikh" runat="server" BackColor="#FFFFCC" ></asp:Label>
									&nbsp;&nbsp;&nbsp;Status:
									&nbsp;<asp:Label ID="lblStatus" runat="server" BackColor="#FFFFCC"></asp:Label>
						  </td>
					  </tr>
						<tr>
							<td>No Perolehan :</td>
							<td>
								<asp:Label ID="lblNoPO" runat="server" BackColor="#FFFFCC"></asp:Label>
							</td>
						</tr>
						<tr>
							<td  style="vertical-align:top;">Tujuan / Tajuk :</td>
							<td>
								<asp:Label ID="lblTujuan" runat="server" BackColor="#FFFFCC"></asp:Label>
							</td>
						</tr>
						<tr id="trTeknikalScope" runat="server" visible="false">
							<td >Scope :</td>
							<td>                                
								<asp:Label ID="lblScope" runat="server" BackColor="#FFFFCC"></asp:Label>

							</td>
						</tr>
                        <tr>
                            <td>PTJ</td>
                            <td><asp:Label ID="lblPTJ1" runat="server" BackColor="#FFFFCC"></asp:Label></td>
                        </tr>
						<tr>
							<td>Kategori Perolehan :</td>
							<td>									
								<asp:Label ID="lblKategoriPO" runat="server" BackColor="#FFFFCC"></asp:Label>

							</td> 
						</tr>
                        <tr>
							<td style="vertical-align:top;">Kaedah Perolehan:</td>
							<td>
								<asp:Label ID="lblKaedah" runat="server" BackColor="#FFFFCC"></asp:Label>
							</td>
                            </tr>
												
						<tr>
							<td>Harga Naskah :</td>
							<td>                                
								<asp:Label ID="lblHargaNskh" runat="server" BackColor="#FFFFCC"></asp:Label>
							</td>
						</tr>	
					   <tr id="trNoSHTD2" runat="server">
								<td style="width: 20%;">No Sebut Harga / Tender :</td>
								<td>
								<asp:Label ID="lblNoSHTD" runat="server" BackColor="#FFFFCC"></asp:Label>                                   
								</td>
							</tr>
					  
					  <tr>
						  <td style="width: 20%">Tarikh Mula Iklan :</td>
						  <td style="width: 80%">
							  <asp:Label ID="lblTrkMulaIklan" runat="server" BackColor="#FFFFCC"></asp:Label>                                
								&nbsp;&nbsp;&nbsp;
								Masa Mula Iklan
								&nbsp;
							   :&nbsp;<asp:TextBox ID="txtMasaMulaIklan2" runat="server" CssClass="form-control" Width="100px" TextMode="Time" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
						  </td>
					  </tr>
					<tr>
						<td>
							Tarikh Mula Perolehan :
						</td>
						<td>
							<asp:Label ID="lblTrkMulaPO" runat="server" BackColor="#FFFFCC"></asp:Label>
							&nbsp;&nbsp;&nbsp;
							Masa Mula Perolehan
							&nbsp;
							:&nbsp;<asp:TextBox ID="txtMasaMulaPO2" runat="server" CssClass="form-control" Width="100px" TextMode="Time" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
						</td>
					</tr>
					<tr>                    
						<td>Tarikh Tamat Perolehan :</td>
						<td>
							<asp:Label ID="lblTrkTmtPO" runat="server" BackColor="#FFFFCC"></asp:Label>
							&nbsp;&nbsp;&nbsp;
						Masa Tamat Perolehan&nbsp; :&nbsp;<asp:TextBox ID="txtMasaTmtPO2" runat="server" CssClass="form-control" Width="100px" TextMode="Time" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
						</td>
					</tr>
                        <tr id="trGred1" runat="server" visible="false">
                            <td>Gred</td>
                            <td><asp:Label ID="lblGred1" runat="server" BackColor="#FFFFCC"></asp:Label></td>
                        </tr>
						<tr id="trLawatanTapak2" runat="server" visible="false">
							<td style="width: 15%; vertical-align:top; height: 40px;">
								Lawatan Tapak
							</td>
							<td style="height: 40px">                             
								
									Tempat Tapak :&nbsp;<asp:Label ID="lblTmptTpt" runat="server" BackColor="#FFFFCC"></asp:Label>
									&nbsp;&nbsp;&nbsp;
									<br />
									Tarikh Lawatan :&nbsp;<asp:Label ID="lblTrkLawatan" runat="server" BackColor="#FFFFCC"></asp:Label>
									
									&nbsp;&nbsp;&nbsp;
									Masa Lawatan :&nbsp;<asp:TextBox ID="txtMasaLawatan2" runat="server" CssClass="form-control" Width="100px" TextMode="Time" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
								
							</td>
						</tr>
                            <tr>
                                <td>Arahan kepada pembekal:</td>
                                <td><asp:textbox ID="txtRingArahan" runat="server" BackColor="#FFFFCC" ReadOnly="true" TextMode="MultiLine" Rows="5" Width="90%"></asp:textbox></td>
                            </tr>
						<tr>
							<td style=" vertical-align:top;"">
								Lesen
							</td>
							<td>
								<asp:GridView ID="gvLesen2" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="false" EmptyDataText=" Tiada rekod"
								cssclass="table table-bordered table-hover" Width="100%" BorderStyle="Solid" HeaderStyle-BackColor="#FECB18" DataKeyNames="PO02_kodLesen" Font-Size="8pt">
										<Columns>
											<asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" >
												<ItemTemplate>
													<%# Container.DataItemIndex + 1 %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:BoundField DataField="PO02_kodLesen" HeaderText="Kod Pendaftaran" ItemStyle-Width="10%" />
											<asp:BoundField DataField="Butiran" HeaderText="Lesen Pendaftaran" ItemStyle-Width="30%" />
											<asp:BoundField DataField="PO02_Detail" HeaderText="Maklumat lanjut" ItemStyle-Width="50%" />
										</Columns>
									</asp:GridView>
							</td>
						</tr>
							<tr id="trView3Bidang" runat="server" visible="false">
							<td style="width: 15%; vertical-align:top;">Kod Bidang</td>
							<td>
								<asp:GridView ID="gvSyarikatBidang2" runat="server" AutoGenerateColumns="False" EmptyDataText = " Tiada rekod" Width="100%" cssclass="table table-striped table-bordered table-hover"
										HeaderStyle-BackColor="#FECB18" BorderStyle="Solid" AllowPaging="false" AllowSorting="True" DataKeyNames="PO02_KodBidang" ShowFooter="False" ShowHeaderWhenEmpty="True" Font-Size="8pt">
										<Columns>
											
											<asp:TemplateField ItemStyle-Width="2%" HeaderStyle-CssClass="centerAlign" HeaderText="Bil" >
												<ItemTemplate>                                                    
													<asp:LinkButton ID="lbtnDelete0"  CssClass="btn-xs" ToolTip="Padam" runat="server" CommandName="Delete" OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');">
										<i class="fas fa-trash-alt fa-lg"></i>
										</asp:LinkButton>
												</ItemTemplate>
												<ItemTemplate>
													<%# Container.DataItemIndex + 1 %>
												</ItemTemplate>
											</asp:TemplateField>                                                
											<asp:BoundField DataField="PO02_KodBidang" HeaderText="Kod Bidang" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
											<asp:BoundField DataField="Butiran" HeaderText="Bidang" ItemStyle-Width="50%" />                                              
											<asp:BoundField DataField="Condition" HeaderText="Situasi Keperluan" ItemStyle-Width="20%" />
										</Columns>
									</asp:GridView>
							</td>
						</tr>
						<tr id="trView3CIDB" runat="server" visible="false">
							<td style="vertical-align:top; width:15%;">
								Pengkhususan
							</td>
							<td>
								<asp:GridView ID="gvSyarikatCIDB2" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="false" Font-Size="8pt"
						cssclass="table table-striped table-bordered table-hover" Width="100%"  BorderStyle="Solid" ShowFooter="False" EmptyDataText=" Tiada rekod" DataKeyNames="PO02_KodKhusus">
							<columns>
							<asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" >
								<ItemTemplate>
									<%# Container.DataItemIndex + 1 %>
								</ItemTemplate>
							</asp:TemplateField>							
							<asp:BoundField DataField="PO02_KodKategori" HeaderText="Kod Kategori" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="centerAlign"/>
							<asp:BoundField DataField="PO02_KodKhusus" HeaderText="Kod Khusus" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="centerAlign"/>
							<asp:BoundField DataField="Butiran" HeaderText="Pengkhususan" ItemStyle-Width="30%" HeaderStyle-CssClass="centerAlign"/>                                    
							<asp:BoundField DataField="Condition" HeaderText="Situasi Keperluan" ItemStyle-Width="20%" />
						</columns>
					</asp:GridView>
							</td>
						</tr>							
							<tr>
								<td  style="vertical-align:top; width: 15%;"> 
									 Lampiran SH/TD</td>
								<td>
									<asp:GridView ID="gvLampiran2" runat="server" AutoGenerateColumns="false" EmptyDataText = " Tiada rekod" Width="80%" cssclass="table table-striped table-bordered table-hover"
										HeaderStyle-BackColor="#6699FF"  BorderColor="#333333" BorderStyle="Solid" DataKeyNames="PO13_ID" ShowHeaderWhenEmpty="true" Font-Size="8pt">
										<Columns>
											<asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%">
												<ItemTemplate>
													<%# Container.DataItemIndex + 1 %>
												</ItemTemplate>
											</asp:TemplateField> 
											<asp:BoundField DataField="PO13_NamaDok" HeaderText="File Name" ItemStyle-Width="40%" />
				                            <asp:BoundField DataField="JenisDok" HeaderText="Type" ItemStyle-Width="25%" />
											<asp:BoundField DataField="PO13_NoMohon" HeaderText="" ItemStyle-Width="1%" visible="false"/>
                                            <asp:HyperLinkField DataNavigateUrlFields="PO13_NoMohon,PO13_NamaDok" DataNavigateUrlFormatString="~/Upload/Document/PO/JualanNaskah/{0}/{1}" DataTextField="PO13_ID" DataTextFormatString="&lt;img src='../../../Images/search.png' alt='View' /&gt;" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%" Target="_blank" />
                                             
										</Columns>
									</asp:GridView>
								</td>
							</tr>
							
							</table>
							<br>                   

							
						   
							<div class="row" style="text-align:center">
					                    
							<asp:LinkButton ID="lbtnPrev4" runat="server" CssClass="btn btn-default btn-circle btn-lg" ToolTip="Sebelumnya">
                                <i class="glyphicon glyphicon-chevron-left"></i>
                            </asp:LinkButton>&nbsp;&nbsp;&nbsp;
	              
							<asp:Button ID="btnHantar" text="Hantar" runat="server" CssClass="btn" />&nbsp;
                        <asp:Button ID="BtnBatal" text="Batal" runat="server" Visible="false" CssClass="btn" OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');" />						                     
						
						</div>
					</div>
						</div>
					 </asp:View>
				</asp:MultiView>
            </div>

    <%--<asp:Button ID = "btnOpen" runat = "server" Text = ""  style="display:none" />
        <ajaxToolkit:ModalPopupExtender ID="MPE" runat="server" BackgroundCssClass="modalBackground" BehaviorID="mpe1" 
            CancelControlID="btnClose" PopupControlID="pnl" TargetControlID="btnOpen"> </ajaxToolkit:ModalPopupExtender>
        
                     <asp:Panel ID="pnl" runat="server" BackColor="White" Width="300px" Height="80px">
                         <asp:UpdatePanel runat="server" ID="UpdatePanel3">
            <ContentTemplate>
               <table style="border: Solid 3px #D5AA00; width: 100%; height: 100%; " >                  
                   
                       <tr style="background: linear-gradient(to bottom, #fceabb 0%,#fccd4d 50%,#f8b500 51%,#fbdf93 100%);">
                           <td colspan="2" style="height: 10%;  font-weight: bold; text-align:center;">Maklumat Lanjut </td>
                           <td style="width: 10px; text-align: center;">
                               <button id="btnClose" runat="server" class="btnNone " title="Tutup" onclick="fClose();">
                                   <i class="far fa-window-close fa-2x"></i>
                               </button>
                           </td>
                       </tr>
                                       
                       <tr style="height:35px;">                           
                           <td colspan="3">
                            <asp:RadioButtonList ID="rbBatal" runat="server" Height="35px" RepeatDirection="Vertical" Width="350px" AutoPostBack="true">
                                <asp:ListItem Text=" Batal Naskah Jualan Sahaja" Value="1" />
                                <asp:ListItem Text=" Batal Naskah Jualan dan Permohonan Pembelian" Value="2" />
                            </asp:RadioButtonList>
                               <asp:RequiredFieldValidator ID="rfvrbBatal" runat="server" ControlToValidate="rbBatal" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="btnBatal1" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>
                           </td>
                       </tr>
                   <tr style="text-align:center;">                           
                           <td colspan="3">
                               <asp:Button ID="btnBatal1" text="Batal" runat="server" Visible="false" CssClass="btn" ValidationGroup="btnBatal1" OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');" />
                               </td>
                       </tr>
                </table>

                </ContentTemplate>
                             
        </asp:UpdatePanel>
            </asp:Panel>--%>

    <script type="text/javascript">

        function Check_Click(objRef) {
            //Get the Row based on checkbox
            var row = objRef.parentNode.parentNode;

            //Get the reference of GridView
            var GridView = row.parentNode;

            //Get all input elements in Gridview
            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {
                //The First element is the Header Checkbox
                var headerCheckBox = inputList[0];

                //Based on all or none checkboxes
                //are checked check/uncheck Header Checkbox
                var checked = true;
                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                    if (!inputList[i].checked) {
                        checked = false;
                        break;
                    }
                }
            }
            headerCheckBox.checked = checked;
        };

        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        //If the header checkbox is checked
                        //check all checkboxes
                        //and highlight all rows
                        //row.style.backgroundColor = "aqua";
                        inputList[i].checked = true;
                    }
                    else {
                        //If the header checkbox is checked
                        //uncheck all checkboxes
                        //and change rowcolor back to original
                        //if (row.rowIndex % 2 == 0) {
                        //    //Alternating Row Color
                        //    row.style.backgroundColor = "#C2D69B";
                        //}
                        //else {
                        //    row.style.backgroundColor = "white";
                        //}
                        inputList[i].checked = false;
                    }
                }
            }
        };

        function RadioCheck(rb) {
            //var gv = objRef.parentNode.parentNode.parentNode;
            var gv = document.getElementById("<%=gvBidang.ClientID%>");
	        var rbs = gv.getElementsByTagName("input");
	        var row = rb.parentNode.parentNode;

	        for (var i = 0; i < rbs.length; i++) {
	            if (rbs[i].type == "radio") {
	                if (rbs[i].checked && rbs[i] != rb) {
	                    rbs[i].checked = false;
	                    break;
	                }
	            }
	        }
	    };

	    function RadioCheck2(rb) {
	        //var gv = objRef.parentNode.parentNode.parentNode;
	        var gv = document.getElementById("<%=gvKhususCIDB.ClientID%>");
	        var rbs = gv.getElementsByTagName("input");
	        var row = rb.parentNode.parentNode;

	        for (var i = 0; i < rbs.length; i++) {
	            if (rbs[i].type == "radio") {
	                if (rbs[i].checked && rbs[i] != rb) {
	                    rbs[i].checked = false;
	                    break;
	                }
	            }
	        }
	    };

	    var kodvalue= <%=KodStatus%>; 

        $(function(){            

            if (kodvalue == 1){
                $('#btnStep1').addClass('btn-success').removeClass('btn-default');
            }
            if (kodvalue == 2){
                $('#btnStep1').addClass('btn-success').removeClass('btn-default');
                $('#btnStep2').addClass('btn-success').removeClass('btn-default');
            }        
            if (kodvalue == 3){
                $('#btnStep1').addClass('btn-success').removeClass('btn-default');
                $('#btnStep2').addClass('btn-success').removeClass('btn-default');
                $('#btnStep3').addClass('btn-success').removeClass('btn-default');
            }        
            if (kodvalue == 4){
                $('#btnStep1').addClass('btn-success').removeClass('btn-default');
                $('#btnStep2').addClass('btn-success').removeClass('btn-default');
                $('#btnStep3').addClass('btn-success').removeClass('btn-default');
                $('#btnStep4').addClass('btn-success').removeClass('btn-default');
            }
            if (kodvalue == 5){
                $('#btnStep1').addClass('btn-success').removeClass('btn-default');
                $('#btnStep2').addClass('btn-success').removeClass('btn-default');
                $('#btnStep3').addClass('btn-success').removeClass('btn-default');
                $('#btnStep4').addClass('btn-success').removeClass('btn-default');
                $('#btnStep5').addClass('btn-success').removeClass('btn-default');
            }
            if (kodvalue == 6){
                $('#btnStep1').addClass('btn-success').removeClass('btn-default');
                $('#btnStep2').addClass('btn-success').removeClass('btn-default');
                $('#btnStep3').addClass('btn-success').removeClass('btn-default');
                $('#btnStep4').addClass('btn-success').removeClass('btn-default');
                $('#btnStep5').addClass('btn-success').removeClass('btn-default');
                $('#btnStep6').addClass('btn-success').removeClass('btn-default');
            }
        });

</script>

</asp:Content>
