<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="MaklumatPenilaianTeknikal.aspx.vb" Inherits="SMKB_Web_Portal.MaklumatPenilaianTeknikal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">       
       
        function pageLoad() {
            $('[data-toggle="tooltip"]').tooltip();
        }
    
           function Validate(sender, args) {
            //Get the reference of GridView
            var gridView = document.getElementById("<%=gvTeknikal.ClientID %>");
            debugger
            var checkBoxes = gridView.getElementsByTagName("input");

            for (var i = 0; i < checkBoxes.length; i++) {
                var headerCheckBox = inputList[0];

                if (checkBoxes[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                        if (checkBoxes[i].checked) {
                            args.IsValid = true;
                            return;
                        }
                    }
                
            }
            args.IsValid = false;
           }

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
    }

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
    </style><h1>Penilaian Spesifikasi Teknikal</h1>
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
                                        
        <div class="panel panel-default">
                  <div class="panel-heading">
                    <h3 class="panel-title">
                        Maklumat Penilaian Spesifikasi Teknikal
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
                                <td style="width: 20%;">Tarikh Tamat Perolehan:</td>
                                <td>
                                    <asp:TextBox ID="trkTamatPO" runat="server" BackColor="#FFFFCC" ReadOnly="true" CssClass="form-control rightAlign" Width="120px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;">Tarikh Tamat Pembuka:</td>
                                <td>
                                    <asp:TextBox ID="txtTarikhTmtPembuka" runat="server" BackColor="#FFFFCC" ReadOnly="true" CssClass="form-control rightAlign" Width="120px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="calendarContainerOverride">
                                <td style="width: 20%;">Tarikh Mesyuarat:</td>
                                <td>
                                    <asp:TextBox ID="txtTarikh" runat="server" CssClass="form-control rightAlign" Width="120px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="cexttxtTarikh" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtTarikh" TodaysDateFormat="dd/MM/yyyy" />
                                    <asp:RequiredFieldValidator ID="rfvtxtTarikh" runat="server" ControlToValidate="txtTarikh" ErrorMessage="" ForeColor="Red" Text="*Sila Isi" ValidationGroup="btnSimpan" Display="Dynamic" ></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            
                            <tr>
                                <td style="width: 20%;">Masa</td>
                                <td>
                                    <asp:TextBox ID="txtMasa" runat="server" CssClass="form-control" Width="100px" TextMode="Time"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtMasa" runat="server" ControlToValidate="txtMasa" ErrorMessage="" ForeColor="Red" Text="*Sila Isi" ValidationGroup="btnSimpan" Display="Dynamic" ></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                    
                        <br />
                        <asp:GridView ID="gvTeknikal" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText=" Tiada rekod"
							cssclass="table table-striped table-bordered table-hover" Width="100%" BorderStyle="Solid" DataKeyNames="PO03_OrderID" Font-Size="8pt">
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
									<ItemStyle Width="2%" />
								</asp:TemplateField>
								<asp:BoundField HeaderText="Kod" DataField="Kod" SortExpression="Kod" ItemStyle-HorizontalAlign="Center" ReadOnly="true">				            
									<ItemStyle Width="3%" />
									</asp:BoundField>
								<asp:BoundField HeaderText="Unik ID" DataField="PO03_UnikID" SortExpression="PO03_UnikID" ReadOnly="true" ItemStyle-HorizontalAlign="Center">
									<ItemStyle Width="6%" />
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
                                <asp:TemplateField HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="2%">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat="server" text="Syor" onclick = "checkAll(this);" />                                        
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" onclick = "Check_Click(this)" Checked='<%#IIf(IsDBNull(Eval("PO03_SyorNilaiTek")), False, Eval("PO03_SyorNilaiTek")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Peratus Spesifikasi" ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblKeutamaan" runat="server" Text='<%#Eval("PO03_PeratusNilaiTek") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate >
                                        <asp:TextBox ID="txtKeutamaan" runat="server" Text='<%#Eval("PO03_PeratusNilaiTek") %>' TextMode="Number" Width="50px"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Center">
                                        <EditItemTemplate>
                                        <asp:LinkButton ID="btnUpdate" runat="server" CommandName="Update" CssClass="btn-xs" ToolTip="Simpan">
										    <i class="far fa-save fa-lg"></i>
									    </asp:LinkButton>												        
										    <asp:LinkButton ID="btnCancel" runat="server" CommandName="Cancel" CssClass="btn-xs" ToolTip="Undo">
										    <i class="fas fa-undo fa-lg"></i>
									    </asp:LinkButton>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                        <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Edit" CssClass="btn-xs" ToolTip="Kemaskini Keutamaan">
                                            <i class="fas fa-edit fa-lg"></i>
                                        </asp:LinkButton>
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
								cssclass="table table-bordered table-hover" Width="100%" BorderStyle="Solid" HeaderStyle-BackColor="#FECB18" Font-Size="8pt"
									 DataKeyNames="PO03_ePID">
									<columns>								 
									<asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" >
								<ItemTemplate>
									<%# Container.DataItemIndex + 1 %>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:BoundField DataField="PO01_Butiran" HeaderText="Barang / Perkara" ItemStyle-Width="40%" HeaderStyle-CssClass="centerAlign" />
							<asp:BoundField DataField="PO03_Jenama" HeaderText="Jenama" ItemStyle-Width="10%" HeaderStyle-CssClass="centerAlign" NullDisplayText=" " />
							<asp:BoundField DataField="PO03_Model" HeaderText="Model" ItemStyle-Width="10%" HeaderStyle-CssClass="centerAlign" NullDisplayText=" "/>
							<asp:BoundField DataField="Negara" HeaderText="Negara Pembuat" ItemStyle-Width="10%" HeaderStyle-CssClass="centerAlign" />							
							<asp:BoundField DataField="PO01_Kuantiti" HeaderText="Kuantiti" ItemStyle-Width="5%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Right"/>
							<asp:BoundField DataField="Ukuran" HeaderText="Ukuran" ItemStyle-Width="5%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center" />
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
                        <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Sila pilih salah satu syor."
    ClientValidationFunction="Validate" ForeColor="Red" ValidationGroup="btnSimpan"></asp:CustomValidator>
                        <br />
                       <br/>
                        <div class="panel panel-default" style="width:inherit">
                            <div class="panel-heading">
                                <h3 class="panel-title">Senarai Kehadiran Penilai Teknikal</h3>
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
                                    EmptyDataText=" Tiada rekod" ShowFooter="false" ShowHeaderWhenEmpty="True" Width="100%" DataKeyNames="PO06_StafID" Font-Size="8pt">
                                    <columns>
                                        <asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" >
									        <ItemTemplate>
										        <%# Container.DataItemIndex + 1 %>
									        </ItemTemplate>
								        </asp:TemplateField> 
                                        <asp:BoundField HeaderText="No Staf" DataField="PO06_StafID" SortExpression="PO05_StafID" ReadOnly="true">
						                    <ItemStyle Width="5%" />
					                    </asp:BoundField>
                                        <asp:BoundField HeaderText="Nama" DataField="MS01_Nama" SortExpression="Nama" ReadOnly="true">
						                    <ItemStyle Width="30%" />
					                    </asp:BoundField>
                                       <asp:BoundField HeaderText="PTJ" DataField="Pejabat" SortExpression="Pejabat" ReadOnly="true">
						                    <ItemStyle Width="20%" />
					                    </asp:BoundField>
                                         <asp:BoundField HeaderText="Jawatan" DataField="PO06_JawStaf" SortExpression="PO05_JawStaf" ReadOnly="true">
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
                     <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn" ToolTip="Simpan & Seterusnya" ValidationGroup="btnSimpan">
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
								Lampiran Penilaian Teknikal
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
                                         <asp:ListItem Text="Laporan penilaian spesifikasi teknikal" Value="PTek" />                                         
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
                                             <asp:TemplateField HeaderStyle-CssClass="centerAlign" HeaderText="Bil" ItemStyle-Width="2%" >
                                                 <ItemTemplate>
                                                     <%# Container.DataItemIndex + 1 %>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:BoundField DataField="PO13_NamaDok" HeaderText="File Name" ItemStyle-Width="40%" />
                                             <asp:BoundField DataField="JenisDok" HeaderText="Type" ItemStyle-Width="25%" />
                                              <asp:BoundField DataField="PO13_NoMohon" HeaderText="" ItemStyle-Width="1%" visible="false"/>
                                             <asp:HyperLinkField DataNavigateUrlFields="PO13_NoMohon,PO13_NamaDok" DataNavigateUrlFormatString="~/Upload/Document/PO/PenilaianTeknikal/{0}/{1}" DataTextField="PO13_ID" DataTextFormatString="&lt;img src='../../../Images/search.png' alt='View' /&gt;" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%" Target="_blank" />
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

