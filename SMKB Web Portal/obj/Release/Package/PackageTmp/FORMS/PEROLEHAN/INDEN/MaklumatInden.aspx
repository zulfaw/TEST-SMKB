<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="MaklumatInden.aspx.vb" Inherits="SMKB_Web_Portal.MaklumatInden" %>
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
            width: 15%;
            height: 27px;
        }
        .auto-style2 {
            height: 27px;
        }

    </style>

    <script type="text/javascript">
    function fCheckBatal(obj) {
        debugger;
        var gridView = document.getElementById("<%=gvInden.ClientID %>");
        var TxtNoInden = document.getElementById("<%=txtNoIndenBatal1.ClientID %>");
        var btnHantar = document.getElementById("<%=lbtnHantar.ClientID %>");

        var row = obj.parentNode.parentNode;
        var intRowidx = row.rowIndex;
        var noInden = gridView.rows[intRowidx].cells[11].outerText;
        var objChx = gridView.rows[intRowidx].cells[14].childNodes[1];
        if (objChx.checked) {
            if (TxtNoInden.value == " " || TxtNoInden.value == "") {
                TxtNoInden.value = noInden;
            }
            else {
                TxtNoInden.value = TxtNoInden.value + " & " + noInden;
            }
            
            btnHantar.style.visibility = "visible";
        }
        else {
            TxtNoInden.value = "";
            btnHantar.style.visibility = "hidden";
        }

        
    }
    </script>

    <h1>Maklumat Inden</h1>
    <p></p>

        <div class="row">
				<asp:LinkButton ID="lbtnKembali" runat="server" CssClass="btn btn-info">
					<i class="fa fa-arrow-left fa-lg"></i>&nbsp;&nbsp;&nbsp;Kembali
				</asp:LinkButton>
			</div>
			<br />
       
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">        
    <ContentTemplate>
                                        
        <div class="panel panel-default" style="width:auto;">
                  <div class="panel-heading">
                    <h3 class="panel-title">
                        Maklumat Inden
                    </h3>
                   </div>

                    <div class="panel-body" style="overflow-x:auto; width:initial;">
                        <table style="width:100%;" class="table table-borderless table-striped">
                            <tr>
						  <td style="width: 20%">No Perolehan :</td>
						  <td style="width: 80%">
							  <asp:TextBox ID="txtNoPO" runat="server" BackColor="#FFFFCC" ReadOnly="true" CssClass="form-control" Width="200px"></asp:TextBox>
								&nbsp;&nbsp;&nbsp;Status:
								&nbsp;<asp:label ID="lblStatus" runat="server" ></asp:label>
						  </td>
					  </tr>
						<tr>
							<td style="width: 20%;">ID Naskah Jualan :</td>
							<td>
								<div class="form-inline">
                                    <asp:label ID="lblIdNJ" runat="server" ></asp:label>
								</div>
																	
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
                                <asp:HiddenField ID="hdKodJnsBrg" runat="server" />
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
								<td style="width: 20%;">No Sebut Harga / Tender :</td>
								<td>
									<asp:label ID="lblNoSHTD" runat="server" ></asp:label>
								</td>
							</tr>                            
                        </table>
                    
                        <b>Senarai petender:</b>
                        <br />
                        <asp:GridView ID="gvInden" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText=" Tiada rekod" Font-Size="8pt"
							cssclass="table table-striped table-bordered table-hover" Width="98%" BorderStyle="Solid" DataKeyNames="PO03_OrderID">
								<columns>
                                <asp:TemplateField ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
								<ItemTemplate>								   
									<asp:Panel ID="pnlMaster" runat="server">
										<asp:Image ID="imgCollapsible" runat="server" ImageUrl="../../../Images/plus.png" Style="margin-right: 5px;" />
										<span style="font-weight:bold;display:none;"><%#Eval("PO03_OrderID")%></span>
									</asp:Panel>										
									<ajaxToolkit:CollapsiblePanelExtender ID="ctlCollapsiblePanel" runat="Server" AutoCollapse="False" AutoExpand="False" CollapseControlID="pnlMaster" Collapsed="True" CollapsedImage="../../../Images/plus.png" CollapsedSize="0" ExpandControlID="pnlMaster" ExpandDirection="Vertical" ExpandedImage="../../../Images/minus.png" ImageControlID="imgCollapsible" ScrollContents="false" TargetControlID="pnlChild"/>
								</ItemTemplate>
                                    </asp:TemplateField>
								<asp:TemplateField HeaderText = "Bil"  ItemStyle-HorizontalAlign="Right">
									<ItemTemplate>
										<%# Container.DataItemIndex + 1 %>
									</ItemTemplate>
									<HeaderStyle CssClass="centerAlign" />
									<ItemStyle Width="2%" />
								</asp:TemplateField>
                                <asp:BoundField HeaderText="Nama Syarikat" DataField="ROC01_NamaSya"  SortExpression="ROC01_NamaSya" NullDisplayText="-" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%"/>                                								
								<asp:BoundField HeaderText="ID Syarikat" DataField="ROC01_IDSya"  SortExpression="ROC01_IDSya" NullDisplayText="-" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%"/>                                								
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
                                <asp:BoundField HeaderText="Tarikh Jangka Siap/ Bekal" DataField="PO09_TkhSiap" SortExpression="PO09_TkhSiap" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" >
									<ItemStyle Width="5%" />
								</asp:BoundField>
                                <asp:BoundField HeaderText="No Inden" DataField="PO19_NoPt" SortExpression="PO19_NoPt" NullDisplayText=" " ItemStyle-HorizontalAlign="Center">
									<ItemStyle Width="10%" />
								</asp:BoundField>                                
                                <asp:BoundField HeaderText="Status" DataField="NamaStatus" SortExpression="NamaStatus" NullDisplayText=" " HeaderStyle-CssClass="centerAlign">
									<ItemStyle Width="13%" />
								</asp:BoundField>
                                <asp:TemplateField HeaderStyle-CssClass="centerAlign" HeaderText="Lulus" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="2%">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChxLulus" runat="server" Checked='<%#IIf(IsDBNull(Eval("PO19_LulusPT")), False, Eval("PO19_LulusPT")) %>' Enabled="false" />                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="centerAlign" HeaderText="Batal" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="2%">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChxBatal" runat="server" onclick="fCheckBatal(this)" Checked='<%#IIf(IsDBNull(Eval("PO19_BatalPT")), False, Eval("PO19_BatalPT")) %>' Enabled="false" />
                                        <%--<asp:LinkButton ID="lbtnBatal" runat="server" CommandName="Select" CssClass="btn-xs btn-primary" ToolTip="Batal PT">
											<i class="fa fa-ellipsis-h fa-lg"></i>
										</asp:LinkButton>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>  
                                <asp:TemplateField> 
                                <ItemTemplate>										
										<asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CssClass="btn-xs" ToolTip="Kemaskini Maklumat Tambahan">
											<i class="far fa-edit fa-lg"></i>
										</asp:LinkButton>
									</ItemTemplate>
									<ItemStyle Width="3%" HorizontalAlign="Center"/>
								</asp:TemplateField>                                
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatusPP" runat="server" Text='<%# Eval("PO19_StatusPP")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>                           
                                                       
								
                                <asp:TemplateField>
								<ItemTemplate>
									<tr>
										<td colspan="100">                                            
								<asp:Panel ID="pnlChild" runat="server" Style="margin-left:20px;margin-right:20px; height:0px;overflow: auto;" Width="98%">
											
                                    <asp:GridView ID="gvChild" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="false" EmptyDataText=" Tiada rekod"
								cssclass="table table-bordered table-hover" Width="100%" BorderStyle="Solid" HeaderStyle-BackColor="#FECB18" ShowFooter="true" Font-Size="8pt"
									 OnRowCreated ="gvChild_RowCreated" DataKeyNames="PO03_ePID">
									<columns>								 
									<asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Right">
								<ItemTemplate>
									<%# Container.DataItemIndex + 1 %>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:BoundField DataField="PO01_Butiran" HeaderText="Barang / Perkara" ItemStyle-Width="25%" HeaderStyle-CssClass="centerAlign" />
							<asp:BoundField DataField="PO03_Jenama" HeaderText="Jenama" ItemStyle-Width="7%" HeaderStyle-CssClass="centerAlign" NullDisplayText=" " />
							<asp:BoundField DataField="PO03_Model" HeaderText="Model" ItemStyle-Width="7%" HeaderStyle-CssClass="centerAlign" NullDisplayText=" "/>
							<asp:BoundField DataField="Negara" HeaderText="Negara Pembuat" ItemStyle-Width="10%" HeaderStyle-CssClass="centerAlign" />							
							<asp:BoundField DataField="PO01_Kuantiti" HeaderText="Kuantiti" ItemStyle-Width="2%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Right"/>
                            <asp:BoundField DataField="KodKw" HeaderText="KW" ItemStyle-Width="2%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center"/>
							<asp:BoundField DataField="KodKo" HeaderText="KO" ItemStyle-Width="2%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center"/>
                            <asp:BoundField DataField="KodPtj" HeaderText="PTJ" ItemStyle-Width="3%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center"/>
                            <asp:BoundField DataField="KodKp" HeaderText="KP" ItemStyle-Width="3%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center"/>
                            <asp:BoundField DataField="KodVot" HeaderText="VOT" ItemStyle-Width="3%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center"/>
                            <asp:BoundField DataField="Ukuran" HeaderText="Ukuran" ItemStyle-Width="3%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center" />
                            <%--<asp:TemplateField HeaderText="Jenis Harga" SortExpression="PO03_JnsHarga" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center">                                    
                                    <ItemTemplate><%#IIf(CInt(Eval("PO03_JnsHarga")) = 1, "Bercukai (Tanpa GST)", "Tanpa cukai (FOB)")%></ItemTemplate>
                                </asp:TemplateField>--%>                            
							<asp:BoundField DataField="PO03_flagInclusiveGST" HeaderText="Inclusive GST?" ItemStyle-Width="3%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Right" NullDisplayText=" "/>
							<asp:BoundField DataField="PO03_HargaSeunit" HeaderText="Harga Seunit (Tanpa GST) (RM)" ItemStyle-Width="7%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Right" NullDisplayText=" " DataFormatString="{0:N2}"/>
							<asp:BoundField DataField="PO03_JumGST" HeaderText="Cukai GST (RM)" ItemStyle-Width="7%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Right" NullDisplayText=" " DataFormatString="{0:N2}"/>
							<asp:BoundField DataField="PO03_JumTanpaGST" HeaderText="Jumlah Harga (Tanpa GST) (RM)" ItemStyle-Width="7%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Right" NullDisplayText=" " DataFormatString="{0:N2}"/>
							<asp:BoundField DataField="PO03_JumHarga" HeaderText="Jumlah Harga (Termasuk GST) (RM)" ItemStyle-Width="7%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Right" NullDisplayText=" " DataFormatString="{0:N2}"/>       
							
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
                        <br />
                        <div id="divAddNewInden" runat="server" style="text-align:center;" visible="false">
                            <asp:label ID="lblGantiInden" runat="server" ToolTip="Penggantian Inden yang telah dibatalkan : " >Ganti Inden</asp:label>
                            <asp:DropDownList ID="ddlSenaraiBatal" runat="server" AutoPostBack="True" CssClass="form-control" Width="120px"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlSenaraiBatal" runat="server" ControlToValidate="ddlSenaraiBatal" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="Tambah" Display="Dynamic" ></asp:RequiredFieldValidator>
                            &nbsp;&nbsp;
                            <asp:LinkButton ID="lbtnTambahPt" runat="server" CssClass="btn btn-info" ToolTip="Tambah ke senarai" ValidationGroup="Tambah">
                                    <i class="fa fa-plus-circle fa-lg"></i>&nbsp;&nbsp;&nbsp;Tambah
                            </asp:LinkButton>
                        </div>
                        <br />
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h3 class="panel-title">Maklumat Tambahan</h3>
                            </div>
                            <div class="panel-body" style="overflow-x:auto">
                                <asp:GridView ID="gvBajet" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="true" EmptyDataText=" Tiada rekod"
						 cssclass="table table-striped table-bordered table-hover" HeaderStyle-BackColor="#6699FF" Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" Width="100%" ShowFooter="False"
							Visible="false">
							<columns>
							<asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Right">
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
                                    <tr class="calendarContainerOverride">
                                        <td class="auto-style1">Tarikh Daftar Inden :</td>
                                        <td class="auto-style2">
                                            <asp:TextBox ID="txtTarikh" runat="server" CssClass="form-control rightAlign" Width="120px" ReadOnly="true" BackColor="#FFFFCC"></asp:TextBox>
                                            <%--<ajaxToolkit:CalendarExtender ID="caltxtTarikh" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtTarikh" TodaysDateFormat="dd/MM/yyyy" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTarikh" ErrorMessage="" ForeColor="Red" Text="*Sila Isi" ValidationGroup="lbtnHantar" Display="Dynamic" ></asp:RequiredFieldValidator>
                                            --%>
                                            &nbsp;&nbsp;&nbsp;Tarikh Lulus Inden : &nbsp;
                                            <asp:TextBox ID="txtTarikhLulus" runat="server" CssClass="form-control rightAlign" Width="120px" ReadOnly="true" BackColor="#FFFFCC"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 15%;">Wakil Pemohon :</td>
                                        <td>
                                        <asp:DropDownList ID="ddlWakilPemohon" runat="server" AutoPostBack="True" CssClass="form-control" Width="40%"></asp:DropDownList>
                                        &nbsp;&nbsp;<asp:TextBox ID="txtJawatanWakil" runat="server" CssClass="form-control" Width="30%" ReadOnly="true" BackColor="#FFFFCC"></asp:TextBox>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 15%;">Pegawai Inden (Pelulus):</td>
                                        <td>
                                            <asp:DropDownList ID="ddlPelulusInden" runat="server" AutoPostBack="True" CssClass="form-control" Width="40%">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlPelulusInden" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="lbtnHantar" Display="Dynamic" ></asp:RequiredFieldValidator>
                                            &nbsp;&nbsp;<asp:TextBox ID="txtJawatan" runat="server" CssClass="form-control" Width="30%" ReadOnly="true" BackColor="#FFFFCC"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 15%;">Penyelaras Projek :</td>
                                        <td>
                                            <asp:DropDownList ID="ddlPenyelarasProj" runat="server" AutoPostBack="True" CssClass="form-control" Width="40%">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlPenyelarasProj" runat="server" ControlToValidate="ddlPenyelarasProj" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="lbtnHantar" Display="Dynamic" ></asp:RequiredFieldValidator>
                                            &nbsp;&nbsp;<asp:TextBox ID="txtPenyelarasProj" runat="server" CssClass="form-control" Width="30%" ReadOnly="true" BackColor="#FFFFCC"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 15%;">Penyelia Kerja :</td>
                                        <td>
                                            <asp:DropDownList ID="ddlPenyeliaKerja" runat="server" AutoPostBack="True" CssClass="form-control" Width="40%">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlPenyeliaKerja" runat="server" ControlToValidate="ddlPenyeliaKerja" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="lbtnHantar" Display="Dynamic" ></asp:RequiredFieldValidator>
                                            &nbsp;&nbsp;<asp:TextBox ID="txtPenyeliaKerja" runat="server" CssClass="form-control" Width="30%" ReadOnly="true" BackColor="#FFFFCC"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trKelulusanInden" runat="server" visible="false">
								        <td style="vertical-align:top;width:15%;"> 
									        <b>Kelulusan Inden :</b></td>
									        <td>
                                                No Inden : <asp:textbox  id="txtNoIndenLulus" runat="server" BackColor="#FFFFCC" CssClass="form-control" Width="250px" ReadOnly="true" rows="1" cols="50" textmode="MultiLine"> </asp:textbox>                                           
                                            <br />
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
                                    <tr id="trBatalInden" runat="server" visible="false">
								        <td style="vertical-align:top;width:15%;"> 
									        <b>Pembatalan Inden</b>
									        </td>
									        <td>
                                                No Inden : <textarea  type="text" name="txtNoIndenBatal1" id="txtNoIndenBatal1" runat="server" style="text-align:left;background-color:#FFFFCC;" Class="form-control" Width="250px" ReadOnly="readonly" rows="1" cols="50"> </textarea>                                           
                                            <br />
                                                Kategori Pembatalan Inden :
									        <asp:RadioButtonList ID="rbBatalInden" runat="server" Height="25px" Width="273px" RepeatDirection="Vertical" AutoPostBack="true" ValidationGroup="lbtnHantar">
									            <asp:ListItem Text=" Batal Inden" Value="1" />
									            <asp:ListItem Text=" Batal Inden dan Perlantikan" Value="2" />
                                                <asp:ListItem Text=" Batal Inden dan Permohonan Pembelian" Value="3" />
								            </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="rfvrbBatalInden" runat="server" ControlToValidate="rbBatalInden" ErrorMessage="" ForeColor="Red" Text="*Sila Pilih" ValidationGroup="lbtnHantar" Display="Dynamic" Enabled="false"></asp:RequiredFieldValidator>
                                            <div id="divBatalInden" runat="server">
										        <label class="control-label" for="Ulasan"><b>Ulasan Pembatalan :</b></label><br />
										        <asp:TextBox ID="txtUlasanBatalInden" runat="server" TextMode="MultiLine" Width="90%" Rows="3" ></asp:TextBox>
										        <asp:RequiredFieldValidator ID="rfvtxtBatalInden" runat="server" ControlToValidate="txtUlasanBatalInden" ErrorMessage="" ForeColor="Red" Text="*Sila isi" Display="Dynamic"  ValidationGroup="vgBatal"></asp:RequiredFieldValidator>
									         </div>
							
								        </td>
							        </tr>
                                    <tr>
                                        <td colspan="2" style="text-align:center;">
                                            <br />
                                            <%--<asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn" ToolTip="Simpan & Seterusnya" ValidationGroup="btnSimpan">
						                        <i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
					                        </asp:LinkButton>
                                            &nbsp;&nbsp;--%>
                                            <asp:LinkButton ID="lbtnHantar" runat="server" CssClass="btn btn-info" ValidationGroup="lbtnHantar" OnClientClick="return confirm('Anda pasti untuk hantar rekod ini?');">
                                                <i class="fab fa-telegram-plane fa-lg"></i>&nbsp;&nbsp;&nbsp;Hantar
                                            </asp:LinkButton>
                                        </td>
                                    </tr>                                    
                                </table>                                
                                </div>
                            </div>
                       <br/>
                        
                    </div>
                    
            <br />
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
            </div>
        </ContentTemplate>
        </asp:UpdatePanel>
           
</asp:Content>
