<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="MaklumatPO.aspx.vb" Inherits="SMKB_Web_Portal.MaklumatPO" EnableEventValidation="False"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Maklumat Pesanan Belian (PO)</h1>
    
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

             <div class="row">
                 <asp:LinkButton ID="lbtnKembali" runat="server" CssClass="btn btn-info"> <i class="fa fa-arrow-left fa-lg"></i>&nbsp;&nbsp;&nbsp;Kembali </asp:LinkButton>
                </div>

            <asp:MultiView ID="mvPO" runat="server" ActiveViewIndex="0">				
				<asp:View ID="view1" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>

                <div class="panel panel-default" style="width:auto;overflow-x:auto;"  >
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                Maklumat Perolehan
                            </h3>
                        </div>
                        <div class="panel-body">
                            <table style="width:100%" class="table table-striped table-borderless">
                            <tr>
                                <td style="width: 20%;">No Permohonan :</td>
                                <td>
                                    <asp:TextBox ID="txtNoPermohonan" runat="server" BackColor="#FFFFCC" ReadOnly="true" CssClass="form-control" Width="200px"></asp:TextBox>
                                    &nbsp;&nbsp;&nbsp;Tarikh Mohon :&nbsp <asp:TextBox ID="txtTarikhMohon" runat="server" BackColor="#FFFFCC" ReadOnly="true" CssClass="form-control" Width="100px"></asp:TextBox>
                                </td>
							</tr>
                            <tr>
                                <td  style="vertical-align:top; width: 20%;">Tujuan Perolehan :</td>
							    <td>
								<asp:TextBox ID="txtTujuan" runat="server" style="width: 90%; height:auto; min-height:100px;" BackColor="#FFFFCC" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>								
							    </td>
							</tr>
							<tr>
                                <td style="width: 20%; height: 23px;"><Label class="control-label" for="">Kategori Perolehan :</Label></td>
                                <td>                                   
                                    <asp:label ID="lblKategoriPO" runat="server" ></asp:label>
                                    &nbsp;&nbsp;&nbsp;Status :
								&nbsp;<asp:label ID="lblStatus" runat="server" ></asp:label>
                                </td> 
                            </tr>
                            <tr>
							<td style="width: 20%;">Bekal Kepada (PTj) :</td>
							<td>									
								<asp:label ID="lblPTjMohon" runat="server" ></asp:label>
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
								<asp:label ID="lblNamaPemohon" runat="server"></asp:label>						
								&nbsp;&nbsp;&nbsp;Jawatan:
								&nbsp;<asp:label ID="lblJwtnPemohon" runat="server" ></asp:label>
						  	
							</td> 
						</tr>                             
                            <tr>
                                <td style="width: 20%;"><Label class="control-label" for="">No Pesanan Belian (PO) :</Label></td>
                                <td>
                                    <asp:TextBox ID="txtNoBelian2" runat="server" BackColor="#FFFFCC" CssClass="form-control" Width="200px"></asp:TextBox>
                                    <asp:HiddenField ID="hfNoPOSem" runat="server" /><asp:HiddenField ID="hdKodJnsBrg" runat="server" />
                                    &nbsp;&nbsp;&nbsp;Tarikh Daftar PO :&nbsp<asp:TextBox ID="txtTarikhPO" runat="server" ReadOnly="true" CssClass="form-control" Width="100px" BackColor="#FFFFCC"></asp:TextBox>
                                </td>
							</tr>                          
                            <tr>
                                <td style="width: 20%;">Vendor :</td>
                                <td><asp:DropDownList ID="ddlVendor" runat="server" AutoPostBack="True" CssClass="form-control" Width="50%"></asp:DropDownList>
                                   <asp:RequiredFieldValidator ID="rfvddlVendor" runat="server" ControlToValidate="ddlVendor" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="btnSimpan" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>
                                    <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn-sm" ToolTip="Cari">
					                    <i class="fa fa-hand-o-left fa-lg"></i>
				                    </asp:LinkButton>
                                </td>
							</tr>
                            <tr class="calendarContainerOverride">
                                <td style="width: 20%;">Tarikh Mula :</td>
                                <td>
                                    <asp:TextBox ID="txtTarikhMula" runat="server" AutoPostBack="True" CssClass="form-control" Text="<%# System.DateTime.Now%>" Width="100px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="calTarikhMula" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtTarikhMula" TodaysDateFormat="dd/MM/yyyy" PopupButtonID="lbtntxtTarikhMula"/>
                                    <asp:LinkButton ID="lbtntxtTarikhMula" runat="server" ToolTip="Klik untuk papar kalendar">
                                        <i class="far fa-calendar-alt fa-lg"></i>
                                    </asp:LinkButton>
                                    <asp:RequiredFieldValidator ID="rfvtxtTarikhMula" runat="server" ControlToValidate="txtTarikhMula" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="btnSimpan" Display="Dynamic"></asp:RequiredFieldValidator>
                                    &nbsp;&nbsp;&nbsp;Tarikh Tamat
                                    &nbsp;&nbsp;
                                    :&nbsp<asp:TextBox ID="txtTarikhTamat" runat="server" CssClass="form-control" Width="100px" ReadOnly="true" BackColor="#FFFFCC"></asp:TextBox>
                                    <%--<asp:HiddenField ID="hdKodJnsBrg" runat="server" /><%--<ajaxtoolkit:CalendarExtender ID="calTarikhTamat" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtTarikhTamat" TodaysDateFormat="dd/MM/yyyy" />--%>
                                </td>
							</tr>
                            <tr class="calendarContainerOverride">
                                <td  style="vertical-align:top; width: 15%;"><Label class="control-label" for=""><b>Tempoh Bekal/Siap :</b></Label></td>
								<td>
									<asp:TextBox ID="txtTempoh" runat="server" AutoPostBack="True" Width="100px" TextMode="number" CssClass="form-control"></asp:TextBox>
									<asp:RequiredFieldValidator ID="rfvtxtTempoh" runat="server" ControlToValidate="txtTempoh" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="btnSimpan" Display="Dynamic"></asp:RequiredFieldValidator>
									&nbsp;&nbsp;
									<asp:DropDownList ID="ddlJnsTmph" runat="server" AutoPostBack="True" CssClass="form-control" Width="100px"></asp:DropDownList>							
							<asp:RequiredFieldValidator ID="rfvddlJnsTmph" runat="server" ControlToValidate="ddlJnsTmph" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="btnSimpan" Display="Dynamic"></asp:RequiredFieldValidator>
							</tr>
							</table>
                            <br />
                            <asp:GridView ID="gvMohon" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="false"
						cssclass="table table-striped table-bordered table-hover" Width="100%"  BorderStyle="Solid" ShowFooter="True" DataKeyNames="PO01_DtID" Font-Size="8pt">
							<columns>
							<asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%"  ItemStyle-HorizontalAlign="Right"><ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate></asp:TemplateField>
                            <asp:BoundField DataField="KodKw" HeaderText="KW" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="centerAlign" ReadOnly="true" />
							<asp:BoundField DataField="KodKo" HeaderText="KO" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="centerAlign" ReadOnly="true" />
                            <asp:BoundField DataField="KodPtj" HeaderText="PTJ" SortExpression="KodPtj" ItemStyle-Width="3%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center" ReadOnly="true"/>
                            <asp:BoundField DataField="KodKp" HeaderText="KP" SortExpression="KodKp" ItemStyle-Width="2%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center" ReadOnly="true"/>
							<asp:BoundField DataField="KodVot" HeaderText="Vot" SortExpression="KodVot" ItemStyle-Width="3%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center" ReadOnly="true"/>
							<asp:BoundField DataField="PO01_Butiran" HeaderText="Barang / Perkara" SortExpression="PO01_Butiran" ItemStyle-Width="15%" HeaderStyle-CssClass="centerAlign" ReadOnly="true"/>
                            <asp:BoundField DataField="PO01_KadarHarga" HeaderText="Ang. Harga (RM)" SortExpression="PO01_KadarHarga" ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="centerAlign" ReadOnly="true"/>
							<asp:BoundField DataField="PO01_Kuantiti" HeaderText="Kuantiti" SortExpression="PO01_Kuantiti" ItemStyle-Width="2%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="right" ReadOnly="true" DataFormatString="{0:N0}"/>
                            <asp:BoundField DataField="Ukuran" HeaderText="Ukuran" ItemStyle-Width="3%"  HeaderStyle-CssClass="centerAlign" ReadOnly="true"/>
                            <asp:BoundField DataField="PO01_JumKadar" HeaderText="Ang. Jum. (RM)" ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="centerAlign" ReadOnly="true"/>
                            <asp:TemplateField HeaderText="Jenama" ItemStyle-Width="7%" HeaderStyle-CssClass="centerAlign">
                                <ItemTemplate>
                                    <asp:Label id="lblJenama" runat ="server" text='<%# IIf(IsDBNull(Eval("PO19_Jenama")), "", Eval("PO19_Jenama")) %>' ></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtJenama" runat="server" CssClass="form-control" Width="100px" Text='<%# Eval("PO19_Jenama") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Model" ItemStyle-Width="7%" HeaderStyle-CssClass="centerAlign">
                                <ItemTemplate>
                                    <asp:Label id="lblModel" runat ="server" text='<%# IIf(IsDBNull(Eval("PO19_Model")), "", Eval("PO19_Model")) %>' ></asp:Label>                                    
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtModel" runat="server" CssClass="form-control" Width="100px" Text='<%# Eval("PO19_Model") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>    
							<asp:TemplateField HeaderText="Negara Pembuat" ItemStyle-Width="10%" HeaderStyle-CssClass="centerAlign">
                                <ItemTemplate>
                                    <asp:Label id="lblNegara" runat ="server" text='<%# IIf(IsDBNull(Eval("Negara")), "", Eval("Negara")) %>' ></asp:Label>                                    
                                </ItemTemplate>                        
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlNegara" runat="server" CssClass="form-control" Width="300px"></asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Harga Seunit (Tanpa GST) (RM)" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="centerAlign">
                                <ItemTemplate>
                                    <asp:Label id="lblHargaSeunit" runat ="server" text='<%# Eval("PO19_KadarHarga", "{0:N}")%>' ></asp:Label>                                    
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtHargaSeunit" runat="server" CssClass="form-control rightAlign" Width="100%" Text='<%# Eval("PO19_KadarHarga", "{0:N}") %>' OnTextChanged="txtHrgSeunit_Change" AutoPostBack="true"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtHargaSeunit" runat="server" ControlToValidate="txtHargaSeunit" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="txtHargaSeunit" Display="Dynamic"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Inclusive GST? Kadar: 0%" ItemStyle-Width="3%" HeaderStyle-CssClass="centerAlign">
                                <ItemTemplate>
                                    <asp:Label id="lblIncludeGST" runat ="server" text='<%# IIf(IsDBNull(Eval("PO19_flagInclusiveGST")), Nothing, Eval("PO19_flagInclusiveGST")) %>' ></asp:Label>
                                </ItemTemplate>                        
                                <EditItemTemplate>
                                    <asp:RadioButtonList ID="rbGST" runat="server" Height="25px" Width="100px" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rbGST_Change" >
                                    <asp:ListItem Text=" Ya" Value=1/>
                                    <asp:ListItem Text=" Tidak" Value=0 selected="True"/>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="rfvrbGST" runat="server" ControlToValidate="rbGST" Display="Dynamic" ErrorMessage="" ValidationGroup="lbtnSave" ForeColor="Red" Text=" *Sila pilih"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Harga GST (RM)" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="centerAlign">
                                <ItemTemplate>
                                    <asp:Label id="lblHargaGST" runat ="server" text='<%# IIf(IsDBNull(Eval("PO19_JumGST")), "0.00", Eval("PO19_JumGST", "{0:N}"))%>' ></asp:Label>                                    
                                </ItemTemplate>
                                <%--<EditItemTemplate>
                                    <asp:TextBox ID="txtHargaGST" runat="server" CssClass="form-control rightAlign" Width="100%" Text='<%# Eval("PO20_JumGST", "{0:N}") %>'></asp:TextBox>
                                </EditItemTemplate>--%>
                            </asp:TemplateField>						
							<asp:TemplateField HeaderText="Jum. Harga (Tanpa GST) (RM)" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="centerAlign">
                                <ItemTemplate>
                                    <asp:Label id="lblJumTanpaGST" runat ="server" text='<%# Eval("PO19_JumTanpaGST", "{0:N}")%>' ></asp:Label>                                    
                                </ItemTemplate>
                                <%--<EditItemTemplate>
                                    <asp:TextBox ID="txtJumTanpaGST" runat="server" CssClass="form-control rightAlign" Width="100%" Text='<%# Eval("PO20_JumTanpaGST", "{0:N}") %>'></asp:TextBox>
                                </EditItemTemplate>--%>
                            </asp:TemplateField>
							<asp:TemplateField HeaderText="Jum. Harga (Termasuk GST) (RM)" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="centerAlign">
                                <ItemTemplate>
                                    <asp:Label id="lblJumHarga" runat ="server" text='<%# Eval("PO19_JumKadar", "{0:N}")%>'></asp:Label>                                    
                                </ItemTemplate>
                                <%--<EditItemTemplate>
                                    <asp:TextBox ID="txtJumHarga" runat="server" CssClass="form-control rightAlign" Width="100%" Text='<%# Eval("PO20_JumHarga", "{0:N}") %>'></asp:TextBox>
                                </EditItemTemplate>--%>
                            </asp:TemplateField>
							<asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblKodNegara" runat="server" Text='<%# Eval("PO19_NegaraBuat")%>'></asp:Label>
                                </ItemTemplate>
							</asp:TemplateField>                                
                         <asp:TemplateField Visible=false>
                             <ItemTemplate>
                             <asp:Label id="lblKodUkuran" runat ="server" text='<%# Eval("PO01_Ukuran")%>' >
                             </asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>                                
							<asp:TemplateField ItemStyle-Width="4%" ItemStyle-HorizontalAlign="Center" HeaderText="Tindakan">
                                <EditItemTemplate>											
								    <asp:LinkButton ID="lbtnSave" runat="server" CommandName="Update" CssClass="btn-xs" ToolTip="Simpan">
												<i class="far fa-save fa-lg"></i>
											</asp:LinkButton>
												&nbsp;&nbsp;
											 <asp:LinkButton ID="lbtnCancel" runat="server" CommandName="Cancel" CssClass="btn-xs" ToolTip="Undo">
												<i class="fas fa-undo fa-lg"></i>
											</asp:LinkButton>
										 </EditItemTemplate>
										 <ItemTemplate>
											 <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Edit" CssClass="btn-xs" ToolTip="Kemas Kini">
												<i class="far fa-edit fa-lg"></i>
											</asp:LinkButton>
                                             <%--&nbsp;&nbsp;
                                    <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete" CssClass="btn-xs" ToolTip="Padam"
											OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');">
										<i class="far fa-trash-alt fa-lg"></i>
										</asp:LinkButton>--%>
                                </ItemTemplate>
							</asp:TemplateField>
						</columns>
                        <selectedrowstyle ForeColor="Blue" />
					</asp:GridView>
                    
                   
                 </div>
                    <div class="row">
				   <div class="col-md-10" style="text-align:center">                                          
						<asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn" ToolTip="Simpan" ValidationGroup="btnSimpan">
						    <i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
					    </asp:LinkButton>
					</div>
				   <div class="col-md-2" style="text-align:right">                       
                        <asp:LinkButton ID="lbtnNext" runat="server" CssClass="btn btn-default btn-circle btn-lg" ToolTip="Seterusnya">
                            <i class="glyphicon glyphicon-chevron-right"></i>
                        </asp:LinkButton>                  
				</div>
			   </div>
                        </div>
             
             
            </ContentTemplate>
			<Triggers><asp:PostBackTrigger ControlID="lbtnNext"/></Triggers>
		</asp:UpdatePanel>
    </asp:View>
        <asp:View ID="View2" runat="server">
            <div class="panel panel-default" >
						<div class="panel-heading">
							<h3 class="panel-title">
								Lampiran Pesanan Belian</h3>
						</div>
						<div class="panel-body">
							<table style="width:100%" class="table table-borderless">					
							 <tr>
								<td style="width: 20%; vertical-align:top">
									<asp:Label ID="Label27" runat="server" Text="Lampiran" CssClass="control-label"></asp:Label>                                 
								</td>
								
								<td>
									Jenis-jenis dokumen: <br />
									<asp:RadioButtonList ID="rbDokumenType" runat="server" >
										<asp:ListItem Text="Kertas Kerja" Value="K" />
										<asp:ListItem Text="Lain-Lain" Value="L" />
									</asp:RadioButtonList>

									<div class="form-inline"> 
																		   
										<asp:FileUpload ID="fuLampiran" runat="server" Width="50%" CssClass="form-control" Height="25px" BackColor="#FFFFCC" EnableViewState="true"/> &nbsp;&nbsp;                              
										<asp:LinkButton ID="lbtnUploadPO" runat="server" CssClass="btn btn-info" ToolTip="Upload file">
											<i class="fa fa-upload fa-lg"></i>&nbsp;&nbsp;&nbsp;Upload
										</asp:LinkButton>
									</div><label>Saiz fail muat naik maksimum 5 mb</label>
									<br />
		 
									<asp:Label ID="LabelMessage1" runat="server" />
									<br />
									<asp:GridView ID="gvLampiran" runat="server" AutoGenerateColumns="false" BorderColor="#333333" BorderStyle="Solid" ShowHeaderWhenEmpty="true" Font-Size="8pt"
                                         cssclass="table table-striped table-bordered table-hover" DataKeyNames="PO13_ID" EmptyDataText=" Tiada rekod" HeaderStyle-BackColor="#6699FF" Width="80%">
                                         <Columns>
                                             <asp:TemplateField HeaderStyle-CssClass="centerAlign" HeaderText="Bil" ItemStyle-Width="2%" >
                                                 <ItemTemplate>
                                                     <%# Container.DataItemIndex + 1 %>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:BoundField DataField="PO13_NamaDok" HeaderText="File Name" ItemStyle-Width="40%" />
                                             <asp:BoundField DataField="JenisDok" HeaderText="Type" ItemStyle-Width="25%" />
                                              <asp:BoundField DataField="PO13_NoMohon" HeaderText="" ItemStyle-Width="1%" visible="false"/>
                                             <asp:HyperLinkField DataNavigateUrlFields="PO13_NoMohon,PO13_NamaDok, pathbaru" DataNavigateUrlFormatString="~/Upload/Document/{2}{1}" DataTextField="PO13_ID" DataTextFormatString="&lt;img src='../../../Images/search.png' alt='View' /&gt;" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%" Target="_blank" />
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
							<div class="col-md-8" style="text-align:left">
                                <asp:LinkButton ID="lbtnPrevView2" runat="server" CssClass="btn btn-default btn-circle btn-lg" ToolTip="Sebelumnya">
                                <i class="glyphicon glyphicon-chevron-left"></i>
                            </asp:LinkButton>
								</div>
							   <div class="col-md-4" style="text-align:right">
                                   <asp:LinkButton ID="lbtnNextView2" runat="server" CssClass="btn btn-default btn-circle btn-lg" ToolTip="Seterusnya">
                                        <i class="glyphicon glyphicon-chevron-right"></i>
                                    </asp:LinkButton>
								</div>
							</div>
					</div>
					</div>
            </asp:View>
                <asp:View ID="View3" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="panel panel-default">
                            <div class="panel-heading">
                                <h3 class="panel-title">Maklumat Tambahan</h3>
                            </div>
                            <div class="panel-body" style="overflow-x:auto">
                                <asp:GridView ID="gvBajet" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="true" EmptyDataText=" Tiada rekod" Font-Size="8pt"
						 cssclass="table table-striped table-bordered table-hover" HeaderStyle-BackColor="#6699FF" BorderColor="#333333" BorderStyle="Solid" Width="100%" ShowFooter="False"
							Visible="false">
							<columns>
							<asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%"  ItemStyle-HorizontalAlign="Right">
								<ItemTemplate>
									<%# Container.DataItemIndex + 1 %>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:BoundField DataField="KW" HeaderText="KW" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center"/>
							<asp:BoundField DataField="KO" HeaderText="KO" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center"/>
							<asp:BoundField DataField="PTJ" HeaderText="PTJ" ItemStyle-Width="4%" ItemStyle-HorizontalAlign="Center"/>
							<asp:BoundField DataField="KP" HeaderText="KP" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center"/>
							<asp:BoundField DataField="VotSebagai" HeaderText="Vot Sebagai" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center"/>
							<asp:BoundField DataField="BakiSbnr" HeaderText="Baki Sebenar (RM)" DataFormatString="{0:n2}" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right"/>							
							<asp:BoundField DataField="BljBlmLulus" HeaderText="Jum. Blja. Belum Lulus (RM)" DataFormatString="{0:n2}" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right"/>
							
						</columns>
						
					</asp:GridView>         
                    <br />
                               <table style="width:100%;" class="table table-borderless table-striped">
                                    <tr>
                                        <td>No Pesanan Belian (PO): </td>
                                        <td>
                                             <asp:textbox  id="txtNoBelian" runat="server" BackColor="#FFFFCC" CssClass="form-control" Width="250px" ReadOnly="true"> </asp:textbox>
                                        </td>
                                    </tr>
                                    <tr class="calendarContainerOverride">
                                        <td class="auto-style1">Tarikh Daftar PO :</td>
                                        <td class="auto-style2">
                                            <asp:TextBox ID="txtTarikhPO1" runat="server" CssClass="form-control rightAlign" Width="120px" ReadOnly="true" BackColor="#FFFFCC"></asp:TextBox>
                                            &nbsp;&nbsp;&nbsp;Tarikh Lulus PO : &nbsp;
                                            <asp:TextBox ID="txtTarikhLulus" runat="server" CssClass="form-control rightAlign" Width="120px" ReadOnly="true" BackColor="#FFFFCC"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 15%;">Pelulus PO :</td>
                                        <td>
                                            <asp:DropDownList ID="ddlPelulusPT" runat="server" AutoPostBack="True" CssClass="form-control" Width="40%">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlPelulusPT" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="lbtnHantar" Display="Dynamic" ></asp:RequiredFieldValidator>
                                            &nbsp;&nbsp;<asp:TextBox ID="txtJawatan" runat="server" CssClass="form-control" Width="30%" ReadOnly="true" BackColor="#FFFFCC"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trKelulusanPT" runat="server" visible="false">
								        <td style="vertical-align:top;width:15%;"> 
									        <b>Kelulusan PO :</b></td>
									        <td>
								            <asp:RadioButtonList ID="rbKelulusan" runat="server" Height="25px" Width="273px" RepeatDirection="Horizontal" AutoPostBack="true" ValidationGroup="lbtnHantar">
									        <asp:ListItem Text=" <b>Lulus</b>" Value="1" />
									        <asp:ListItem Text=" <b>Tidak Lulus</b>" Value="0" />
								            </asp:RadioButtonList>                                            
                                            <asp:RequiredFieldValidator ID="rfvrbKelulusan" runat="server" ControlToValidate="rbKelulusan" ErrorMessage="" ForeColor="Red" Text="*Sila Pilih" ValidationGroup="lbtnHantar" Display="Dynamic" Enabled="false"></asp:RequiredFieldValidator>
									        <div id="divUlasan" runat="server" visible="false">
										        <label class="control-label" for="Ulasan"><b>Ulasan Tidak Lulus :</b></label>
										        <br />
										        <asp:TextBox ID="txtUlasan" runat="server" TextMode="MultiLine" Width="90%" Rows="3" ></asp:TextBox>
										        <asp:RequiredFieldValidator ID="rfvUlasan" runat="server" ControlToValidate="txtUlasan" ErrorMessage="" ForeColor="Red" Text="*Sila isi" Display="Dynamic"  ValidationGroup="vgTidakLulus"></asp:RequiredFieldValidator>
									        </div>							
								        </td>
							        </tr>
                                    <tr id="trBatalPT" runat="server" visible="false">
								        <td style="vertical-align:top;width:15%;"> 
									        <b>Pembatalan PO :</b></td>
									        <td>                                                
                                            Kategori Pembatalan PT :
									        <asp:RadioButtonList ID="rbBatalPT" runat="server" Height="25px" Width="100%" RepeatDirection="Vertical" AutoPostBack="true" ValidationGroup="lbtnHantar">
									            <asp:ListItem Text=" Batal Pesanan Belian" Value=1 />
                                                <asp:ListItem Text=" Batal Pesanan Belian dan Permohonan Pembelian" Value=2 />
								            </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="rfvrbBatalPT" runat="server" ControlToValidate="rbBatalPT" ErrorMessage="" ForeColor="Red" Text="*Sila Pilih" ValidationGroup="lbtnHantar" Display="Dynamic" Enabled="false"></asp:RequiredFieldValidator>
                                            <div id="divBatalPT" runat="server">
										        <label class="control-label" for="Ulasan"><b>Ulasan Pembatalan :</b></label><br />
										        <asp:TextBox ID="txtUlasanBatalPT" runat="server" TextMode="MultiLine" Width="90%" Rows="3" ></asp:TextBox>
										        <asp:RequiredFieldValidator ID="rfvtxtBatalPT" runat="server" ControlToValidate="txtUlasanBatalPT" ErrorMessage="" ForeColor="Red" Text="*Sila isi" Display="Dynamic"  ValidationGroup="vgBatal"></asp:RequiredFieldValidator>
									         </div>
							
								        </td>
							        </tr>                                                                     
                                </table>
                    <div class="row">
					<div class="col-md-2" style="text-align:left">                    
                            <asp:LinkButton ID="lbtnPrevView3" runat="server" CssClass="btn btn-default btn-circle btn-lg" ToolTip="Sebelumnya">
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
                            
                    </ContentTemplate>
                        <Triggers><asp:PostBackTrigger ControlID="lbtnPrevView3"/></Triggers>
                        </asp:UpdatePanel>
                </asp:View>


                </asp:MultiView>
            
    <br />
    <div class="panel panel-default">
				  <div class="panel-heading">
					<h4 class="panel-title">
					  <a data-toggle="collapse" href="#collapse1"><span class="fas fa-users fa-lg"></span>&nbsp&nbsp Urusetia
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
    </a>
</asp:Content>
