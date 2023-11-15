<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="MaklumatSST.aspx.vb" Inherits="SMKB_Web_Portal.MaklumatSST" %>
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

</style>


    <h1>Maklumat Surat Setuju Terima & Jaminan Bank</h1>
    <p></p>

        <div class="row">
				<asp:LinkButton ID="lbtnKembali" runat="server" CssClass="btn btn-info">
					<i class="fa fa-arrow-left fa-lg"></i>&nbsp;&nbsp;&nbsp;Kembali
				</asp:LinkButton>
			</div>
			<br />
        <asp:MultiView ID="mvSST" runat="server" ActiveViewIndex="0">
		<asp:View ID="View1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">        
    <ContentTemplate>
                                        
        <div class="panel panel-default" id="printPanel" runat="server">
                  <div class="panel-heading">
                    <h3 class="panel-title">
                        Maklumat Surat Setuju Terima & Jaminan Bank
                    </h3>
                   </div>

                    <div class="panel-body" style="overflow-x:auto; width:initial;">
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
								<asp:TextBox ID="txtPTjMohon" runat="server" BackColor="#FFFFCC" ReadOnly="true" CssClass="form-control" Width="60%"></asp:TextBox>						
									
							</td> 
						</tr>
						
					   <tr>
								<td style="width: 20%;">No Sebut Harga / Tender :</td>
								<td>
									<asp:TextBox ID="txtNoSHTD" runat="server" CssClass="form-control" Width="40%" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
									
								</td>
							</tr>
                            
                        </table>
                    
                        <b>Senarai petender:</b>
                        <br />
                        <asp:GridView ID="gvSST" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText=" Tiada rekod" Font-Size="8pt"
							cssclass="table table-striped table-bordered table-hover" Width="95%" BorderStyle="Solid" DataKeyNames="PO03_OrderID">
								<columns>
								<asp:TemplateField HeaderText = "Bil" ItemStyle-HorizontalAlign="Right">
									<ItemTemplate>
										<%# Container.DataItemIndex + 1 %>
									</ItemTemplate>
									<HeaderStyle CssClass="centerAlign" />
									<ItemStyle Width="2%" />
								</asp:TemplateField>
                                <asp:BoundField HeaderText="Nama Syarikat" DataField="ROC01_NamaSya"  SortExpression="ROC01_NamaSya" NullDisplayText="-" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%"/>                                								
								<asp:BoundField HeaderText="Gred" DataField="ROC01_KodGred"  SortExpression="ROC01_KodGred" NullDisplayText="-" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%"/>
                                <asp:TemplateField HeaderText="Status Bumi" SortExpression="ROC01_KodBumi" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">                                    
                                    <ItemTemplate><%#IIf(CInt(Eval("ROC01_KodBumi")) = 1, "Bumi", "Bukan Bumi")%></ItemTemplate>
                                </asp:TemplateField>								
                                <asp:BoundField HeaderText="Jumlah Besar (RM)" DataField="JumHarga" SortExpression="JumHarga" dataformatstring="{0:N}" ItemStyle-HorizontalAlign="Right">
									<ItemStyle Width="8%" />
									</asp:BoundField>							
								<asp:BoundField HeaderText="Jumlah Jaminan (RM)" DataField="PO09_JumJamin" SortExpression="PO09_JumJamin" NullDisplayText="-" dataformatstring="{0:N}" ItemStyle-HorizontalAlign="Right">
									<ItemStyle Width="8%" />
									</asp:BoundField>
								<asp:BoundField HeaderText="Tempoh Siap" DataField="Tempoh" SortExpression="Tempoh" ItemStyle-HorizontalAlign="Center">
									<ItemStyle Width="10%" />
									</asp:BoundField>
                                <asp:BoundField HeaderText="Tarikh SST/Mula" DataField="PO09_TkhSST" SortExpression="PO09_TkhSST" NullDisplayText="-" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}">
									<ItemStyle Width="5%" />
								</asp:BoundField>
                                <asp:BoundField HeaderText="Tarikh Jangka Siap" DataField="PO09_TkhSiap" SortExpression="PO09_TkhSiap" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" >
									<ItemStyle Width="5%" />
								</asp:BoundField>
                                <asp:BoundField HeaderText="Peratus Jaminan" DataField="PO09_Peratus" SortExpression="PO09_Peratus" DataFormatString="{0:P}" NullDisplayText="-" ItemStyle-HorizontalAlign="Center">
									<ItemStyle Width="3%" />
								</asp:BoundField>                                
                                <asp:BoundField HeaderText="No Ruj. Surat" DataField="PO09_RujSurat" SortExpression="PO09_RujSurat"  NullDisplayText="-" ItemStyle-HorizontalAlign="Center">
									<ItemStyle Width="3%" />
								</asp:BoundField>
                                <asp:BoundField HeaderText="Tempoh Sah Jaminan (Bulan)" DataField="PO09_TempohSahJB" SortExpression="PO09_TempohSahJB" NullDisplayText="-" ItemStyle-HorizontalAlign="Center">
									<ItemStyle Width="3%" />
								</asp:BoundField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrderID" runat="server" Text='<%# Eval("PO03_OrderID")%>'></asp:Label>
                                        </ItemTemplate>
                                </asp:TemplateField>                                
                                <asp:TemplateField>                        
								<ItemTemplate>										
										<asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CssClass="btn-xs" ToolTip="Kemaskini Maklumat Tambahan & Salinan">
											<i class="far fa-edit fa-lg"></i>
										</asp:LinkButton>
									</ItemTemplate>
									<ItemStyle Width="3%" HorizontalAlign="Center"/>
								</asp:TemplateField>
                                								
							</columns>
								<HeaderStyle BackColor="#6699FF" />
						</asp:GridView>
                        <br />
                        <div class="panel panel-default" style="width:inherit;">
                            <div class="panel-heading">
                                <h3 class="panel-title">Maklumat Tambahan</h3>
                            </div>
                            <div class="panel-body" style="overflow-x:auto">
                                <table style="width:100%;" class="table table-borderless table-striped">
                                    <tr class="calendarContainerOverride">
                                        <td style="width: 15%;">Tarikh Mula :</td>
                                        <td>
                                            <asp:TextBox ID="txtTarikh" runat="server" CssClass="form-control rightAlign" Width="120px"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="caltxtTarikh" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtTarikh" TodaysDateFormat="dd/MM/yyyy" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTarikh" ErrorMessage="" ForeColor="Red" Text="*Sila Isi" ValidationGroup="btnSaveButiran" Display="Dynamic" ></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 15%;">No. Rujukan Surat :</td>
                                        <td>
                                            <asp:TextBox ID="txtRujukan" runat="server" CssClass="form-control rightAlign" Width="250px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRujukan" ErrorMessage="" ForeColor="Red" Text="*Sila Isi" ValidationGroup="btnSaveButiran" Display="Dynamic" ></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 15%;">Tempoh Sah Jaminan :</td>
                                        <td>
                                            <asp:RadioButtonList ID="rbtempohJaminan" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="12 Bulan" Value="12" />
                                                <asp:ListItem Text="24 Bulan" Value="24" />
                                                <asp:ListItem Text="36 Bulan" Value="36" />
                                                <asp:ListItem Text="48 Bulan" Value="48" />
                                                <asp:ListItem Text="Tiada" Value="00" />                                         
                                            </asp:RadioButtonList>                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="text-align:center;">
                                            <asp:LinkButton ID="lbtnEdit" runat="server" CssClass="btn btn-info" ToolTip="Kemaskini" ValidationGroup="btnSaveButiran" Visible="false">
                                                <i class="fa fa-pencil fa-lg"></i>&nbsp;&nbsp;&nbsp;Kemaskini
                                            </asp:LinkButton> 
                                        </td>
                                    </tr>                                    
                                </table>
                                <asp:HiddenField runat="server" ID="hdOrderId" />
                                </div>
                            </div>
                       <br/>
                        <div class="panel panel-default" style="width:inherit;">
                            <div class="panel-heading">
                                <h3 class="panel-title">Salinan kepada</h3>
                            </div>
                            <div class="panel-body" style="overflow-x:auto">
                                <table style="width:100%" class="table table-borderless table-striped">
                                    <tr>
							            <td style="width: 10%;">Nama Syarikat :</td>
							            <td>									
								            <asp:TextBox ID="txtNamaSyarikat" runat="server" BackColor="#FFFFCC" ReadOnly="true" CssClass="form-control" Width="60%"></asp:TextBox>						
									
							            </td> 
						            </tr>
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
                                             <asp:LinkButton ID="lbtnTambahStaf" runat="server" CssClass="btn btn-info" ToolTip="Tambah ke senarai" ValidationGroup="btnTambah" Visible="false">
                                            <i class="fa fa-plus-circle fa-lg"></i>&nbsp;&nbsp;&nbsp;Tambah
                                            </asp:LinkButton>
                                        </td>
                                    </tr>
                                    
                                </table>
                                <br/>
                                <asp:GridView ID="gvJawatanKuasa" runat="server" AllowPaging="false" AllowSorting="True" AutoGenerateColumns="False" BorderStyle="Solid" cssclass="table table-striped table-bordered table-hover" 
                                    EmptyDataText=" Tiada rekod" ShowFooter="false" ShowHeaderWhenEmpty="True" Width="100%" DataKeyNames="PO09_StafID" Font-Size="8pt">
                                    <columns>
                                        <asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" >
									        <ItemTemplate>
										        <%# Container.DataItemIndex + 1 %>
									        </ItemTemplate>
								        </asp:TemplateField> 
                                        <asp:BoundField HeaderText="No Staf" DataField="PO09_StafID" SortExpression="PO09_StafID" ReadOnly="true" >
						                    <ItemStyle Width="5%" />
					                    </asp:BoundField>
                                        <asp:BoundField HeaderText="Nama" DataField="PO09_NamaStaf" SortExpression="PO09_NamaStaf" ReadOnly="true" HeaderStyle-CssClass="Center">
						                    <ItemStyle Width="30%" />
					                    </asp:BoundField>
                                       <asp:BoundField HeaderText="PTJ" DataField="P009_PTJStaf" SortExpression="P009_PTJStaf" ReadOnly="true" HeaderStyle-CssClass="Center">
						                    <ItemStyle Width="20%" />
					                    </asp:BoundField>
                                         <asp:BoundField HeaderText="Jawatan" DataField="PO09_JawStaf" SortExpression="PO09_JawStaf" ReadOnly="true" HeaderStyle-CssClass="Center">
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
                     
                    </div>
                    
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
								Dokumen Pentadbiran Kontrak
							</h3>
						</div>
						<div class="panel-body">
							<table style="width:100%" class="table table-borderless">                            	
							<tr>
								<td  style="vertical-align:top; width: 15%;"> 
									 <b>Borang untuk muat turun</b>
								</td>
								<td>
									<table  style="width:100%" class="table table-bordered table-striped">
                                        
                                        <tr style="background-color:#FECB18">
                                            <th style="width:10%;">
                                                Bil
                                            </th>
                                            <th style="width:70%;">
                                                Nama Dokumen
                                            </th>
                                            <th style="width:10%;">
                                                Tindakan                                         
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>1</td>
                                            <td>Surat Setuju Terima</td>
                                            <td>
                                                <asp:LinkButton ID = "lbtnSST"  CssClass="btn-sm" ToolTip="Download" runat = "server">
												    <i class="fas fa-download fa-lg"></i>
											    </asp:LinkButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>2</td>
                                            <td>Borang Jaminan Bank</td>
                                            <td>
                                                <asp:LinkButton ID = "lbtnJaminanBank"  CssClass="btn-sm" ToolTip="Download" runat = "server">
												    <i class="fas fa-download fa-lg"></i>
											    </asp:LinkButton>
                                            </td>
                                        </tr>
									</table>
								</td>
							</tr>
											
							 
							
							</table>
							
							<br />
						   
							<div class="row">
							<div class="col-md-12" style="text-align:center">                    
                                <asp:LinkButton ID="lbtnPrevView2" runat="server" CssClass="btn btn-default btn-circle btn-lg" ToolTip="Sebelumnya">
                                <i class="glyphicon glyphicon-chevron-left"></i>
                                </asp:LinkButton>
								</div>
							   <%--<div class="col-md-10" style="text-align:center">
                                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-info" ValidationGroup="lbtnHantar">
                                        <i class="fab fa-telegram-plane fa-lg"></i>&nbsp;&nbsp;&nbsp;Hantar
                                    </asp:LinkButton>
								</div>--%>
							</div>
					</div>
					</div>

            </asp:View>
        </asp:MultiView>

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
        

</asp:Content>

