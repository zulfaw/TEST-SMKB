<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="MaklumatPembukaExtra.aspx.vb" Inherits="SMKB_Web_Portal.MaklumatPembukaExtra" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">       
                           
        function PrintPanel() {
            var panel = document.getElementById("<%=printPanel.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title>ePerolehan UTeM</title>');
            printWindow.document.write('</head><body ><center>');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('<br><hr><br>');
            printWindow.document.write('</center></body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }

        function pageLoad() {
            $('[data-toggle="tooltip"]').tooltip();
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

    <h1>Proses Pembuka</h1>
    <p></p>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">        
    <ContentTemplate>
        <div class="row">
				<asp:LinkButton ID="lbtnKembali" runat="server" CssClass="btn btn-info">
					<i class="fa fa-arrow-left fa-lg"></i>&nbsp;&nbsp;&nbsp;Kembali
				</asp:LinkButton>
			</div>
			<br />
        <asp:MultiView ID="mv" runat="server" >
            <asp:View ID="View1" runat="server">
                <div class="panel panel-default" id="printPanel" runat="server">
                  <div class="panel-heading">
                    <h3 class="panel-title">
                        Maklumat Pembuka
                    </h3>
                   </div>

                    <div class="panel-body" style="overflow-x:auto; width:initial;">
                        <table style="width:100%;" class="table table-borderless table-striped">
                            <tr>
						  <td style="width: 20%">ID Naskah Jualan :</td>
						  <td style="width: 80%">
							  <asp:TextBox ID="txtIdNJ" runat="server" BackColor="#FFFFCC" CssClass="form-control"  Width="150px" ReadOnly="true"></asp:TextBox>
							  
								&nbsp;&nbsp;&nbsp;Status:
								&nbsp;<asp:label ID="lblStatus" runat="server"></asp:label>
						  </td>
					  </tr>
						<tr>
							<td style="width: 20%;">No Perolehan :</td>
							<td>
								<div class="form-inline">
									<asp:label ID="lblNoPO" runat="server"></asp:label>
									
								</div>
																	
							</td>
						</tr>
						<tr>
							<td  style="vertical-align:top; width: 20%;">Tujuan Perolehan :</td>
							<td>
								<asp:label ID="lblTujuan" runat="server"></asp:label>								
							</td>
						</tr>
							
						<tr>
							<td style="width: 20%;">Kategori Perolehan :</td>
							<td>									
								<asp:label ID="lblKategoriPO" runat="server"></asp:label>
									
							</td> 
						</tr>
                        <tr>
							<td style="width: 20%;">Kaedah Perolehan :</td>
							<td>									
								<asp:label ID="lblKaedahPO" runat="server"></asp:label>						
									
							</td> 
						</tr>
                        <tr>
							<td style="width: 20%;">PTJ :</td>
							<td>									
								<asp:label ID="lblPTjMohon" runat="server"></asp:label>
							</td> 
						</tr>
						
					   <tr>
								<td style="width: 20%;">No Sebut Harga / Tender :</td>
								<td>
									<asp:label ID="lblNoSHTD" runat="server"></asp:label>
									
								</td>
							</tr>
                            <tr>
                                <td style="width: 20%;">Tarikh Tamat Perolehan :</td>
                                <td>
                                    <asp:TextBox ID="trkTamatPO" runat="server" BackColor="#FFFFCC" ReadOnly="true" CssClass="form-control rightAlign" Width="120px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;">Tarikh Mula Pembuka :</td>
                                <td>
                                    <asp:TextBox ID="txtTarikhMulaPembuka" runat="server" BackColor="#FFFFCC" ReadOnly="true" CssClass="form-control rightAlign" Width="120px"></asp:TextBox>
                                    &nbsp;&nbsp;Masa :&nbsp;&nbsp;
                                    <asp:TextBox ID="txtMasaMulaPembuka" runat="server" CssClass="form-control" Width="100px" TextMode="Time" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>                         
                           
                        </table>
                    <br />
                        <div class="panel panel-default">
				<div class="panel-heading">
					<h3 class="panel-title">
						
                            Maklumat Pembida
                        
					</h3>
				</div>
                
				<div class="panel-body">
					
					<br />
					<asp:GridView ID="gvPembida" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="false"
						cssclass="table table-striped table-bordered table-hover"  BorderStyle="Solid" ShowFooter="false" DataKeyNames="PO03_OrderID" Font-Size="8pt">
							<columns>
							<asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Right"><ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate></asp:TemplateField>
                            <asp:BoundField HeaderText="Unik ID" DataField="PO03_UnikID" SortExpression="PO03_UnikID" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" ReadOnly="true"/>                           
                            <asp:BoundField HeaderText="Kod Pembida Hantar" DataField="PO03_BilHantar" SortExpression="PO03_BilHantar" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="3%" ReadOnly="true" />	
                            <asp:BoundField HeaderText="Tarikh & Masa Terimaan" DataField="PO03_TarikhHantar" SortExpression="PO03_TarikhHantar" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="5%" ReadOnly="true"/>
                            <asp:TemplateField HeaderText="Terimaan Dokumen?" ItemStyle-Width="10%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center">                       
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" onclick = "Check_Click(this)" Checked='<%#IIf(IsDBNull(Eval("PO03_StatusHantar")), False, Eval("PO03_StatusHantar")) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>                               
							<asp:TemplateField ItemStyle-Width="4%" ItemStyle-HorizontalAlign="Center" HeaderText="Tindakan">
                                <%--<EditItemTemplate>											
								    <asp:LinkButton ID="lbtnSave" runat="server" CommandName="Update" CssClass="btn-xs" ToolTip="Simpan">
												<i class="far fa-save fa-lg"></i>
											</asp:LinkButton>
												&nbsp;&nbsp;
											 <asp:LinkButton ID="lbtnCancel" runat="server" CommandName="Cancel" CssClass="btn-xs" ToolTip="Undo">
												<i class="fas fa-undo fa-lg"></i>
											</asp:LinkButton>
										 </EditItemTemplate>--%>
										 <ItemTemplate>
											 <asp:LinkButton ID="lbtnSave" runat="server" CommandName="Update" CssClass="btn-xs" ToolTip="Simpan">
												<i class="far fa-save fa-lg"></i>
											</asp:LinkButton>
                                             &nbsp;&nbsp;
                                    <%--<asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete" CssClass="btn-xs" ToolTip="Padam"
											OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');">
										<i class="far fa-trash-alt fa-lg"></i>
										</asp:LinkButton>--%>
                                </ItemTemplate>
							</asp:TemplateField>
						</columns>
                        <selectedrowstyle ForeColor="Blue" />
					</asp:GridView>
					
			   </div>
				
                </div> 
                    </div>
            </div>
                <div class="row">
                    <div class="col-md-12" style="text-align:right">                    
						<asp:LinkButton ID="lbtnNextView1" runat="server" CssClass="btn btn-default btn-circle btn-lg" ToolTip="Sebelumnya">
                                <i class="glyphicon glyphicon-chevron-right"></i>
                            </asp:LinkButton>
				</div>
                    </div>
            </asp:View>   
            <asp:View ID="View2" runat="server"> 
             <div class="panel panel-default" style="width:inherit;">
                            <div class="panel-heading">
                                <h3 class="panel-title">Senarai petender</h3>
                            </div>
                            <div class="panel-body" style="overflow-x:auto">
                                <asp:GridView ID="gvPembuka" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText=" Tiada rekod"
							cssclass="table table-striped table-bordered table-hover" Width="95%" BorderStyle="Solid" DataKeyNames="PO03_OrderID" Font-Size="8pt">
								<columns>
								<asp:TemplateField HeaderText = "Bil" ItemStyle-HorizontalAlign="Right">
									<ItemTemplate>
										<%# Container.DataItemIndex + 1 %>
									</ItemTemplate>
									<HeaderStyle CssClass="centerAlign" />	
								</asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat="server" text="Padan" onclick = "checkAll(this);" />
                                        <i class="fas fa-info-circle fa-lg" data-toggle="tooltip" data-placement="right" style="color:#ba2818;" title="Klik jika padan dengan borang yang dihantar"></i>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" onclick = "Check_Click(this)" Checked='<%#IIf(IsDBNull(Eval("PO03_MatchHantar")), False, Eval("PO03_MatchHantar")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
								<asp:BoundField HeaderText="Kod" DataField="Kod" SortExpression="Kod" ItemStyle-HorizontalAlign="Right"/>				            
								<asp:BoundField HeaderText="Unik ID" DataField="PO03_UnikID" SortExpression="PO03_UnikID" ItemStyle-HorizontalAlign="Center" />
								<asp:BoundField HeaderText="Gred" DataField="ROC01_KodGred"  SortExpression="ROC01_KodGred" NullDisplayText="-" ItemStyle-HorizontalAlign="Center"/>
                                <asp:TemplateField HeaderText="Status Bumi" SortExpression="ROC01_KodBumi" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">                                    
                                    <ItemTemplate><%#IIf(CInt(Eval("ROC01_KodBumi")) = 1, "Bumi", "Bukan Bumi")%></ItemTemplate>
                                </asp:TemplateField>								
                                <asp:BoundField HeaderText="Harga Tawaran (RM)" DataField="JumHarga" SortExpression="JumHarga" dataformatstring="{0:N}" ItemStyle-HorizontalAlign="Right"/>						
								<%--<asp:BoundField HeaderText="Harga Tawaran Tanpa GST (RM)" DataField="JumTanpaGST" SortExpression="JumTanpaGST" dataformatstring="{0:N}" ItemStyle-HorizontalAlign="Right"/>--%>
								<asp:BoundField HeaderText="Tempoh Siap" DataField="Tempoh" SortExpression="Tempoh" ItemStyle-HorizontalAlign="Center"/>
                                <asp:TemplateField HeaderText="" SortExpression="PO03_StatusHantar" ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        Status ePerolehan
                                        <i class="fas fa-info-circle fa-lg" data-toggle="tooltip" data-placement="bottom" style="color:#ba2818;" title="Status Vendor Hantar Maklumat Harga melalui sistem"></i>
                                    </HeaderTemplate>
                                    <ItemTemplate><%#IIf(Eval("PO03_StatusHantar"), "Ya", "Tidak")%></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" SortExpression="PO03_StatusTerimaanDok" ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        Status Terimaan Dok 
                                        <i class="fas fa-info-circle fa-lg" data-toggle="tooltip" data-placement="bottom" style="color:#ba2818;" title="Status terimaan dokumen"></i>
                                    </HeaderTemplate>
                                    <ItemTemplate><%#IIf(Eval("PO03_StatusTerimaanDok"), "Ya", "Tidak")%></ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="No Daftar SSM" DataField="SSM" SortExpression="SSM" NullDisplayText="-" ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField HeaderText="No Daftar KKM" DataField="CIDB" SortExpression="CIDB" NullDisplayText="-" ItemStyle-HorizontalAlign="Center"/> 
                                <asp:BoundField HeaderText="No Daftar CIDB" DataField="CIDB" SortExpression="CIDB" NullDisplayText="-" ItemStyle-HorizontalAlign="Center"/>								
							</columns>
								<HeaderStyle BackColor="#6699FF" />
						</asp:GridView>
                       <br/>
                        <div style="text-align:center;">
                            <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn" ToolTip="Simpan & Seterusnya" ValidationGroup="btnSimpan">
						        <i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
					        </asp:LinkButton> 
                     
                        </div>
                        
                                
                            </div>
                        <div class="row">
							<div class="col-md-10" style="text-align:left">                    
								<asp:LinkButton ID="lbtnPrevView2" runat="server" CssClass="btn btn-default btn-circle btn-lg" ToolTip="Seterusnya">
                                <i class="glyphicon glyphicon-chevron-left"></i>
                            </asp:LinkButton> 
								</div>
							   <div class="col-md-2" style="text-align:right">                    
									<asp:LinkButton ID="lbtnNextView2" runat="server" CssClass="btn btn-default btn-circle btn-lg" ToolTip="Sebelumnya">
                                <i class="glyphicon glyphicon-chevron-right"></i>
                            </asp:LinkButton>
								
								</div>
							</div>
                 </div>
            </asp:View>
            <asp:View ID="View3" runat="server">
                <div class="panel panel-default" style="width:inherit;">
                            <div class="panel-heading">
                                <h3 class="panel-title">Senarai Kehadiran & Pengesahan Pembuka</h3>
                            </div>
                            <div class="panel-body" style="overflow-x:auto">
                                
                                <br/>
                                <asp:GridView ID="gvJawatanKuasa" runat="server" AllowPaging="false" AllowSorting="True" AutoGenerateColumns="False" BorderStyle="Solid" cssclass="table table-striped table-bordered table-hover" 
                                    EmptyDataText=" Tiada rekod" ShowFooter="false" ShowHeaderWhenEmpty="True" Width="100%" DataKeyNames="PO04_PembukaDtID" Font-Size="8pt">
                                    <columns>
                                        <asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%">
									        <ItemTemplate>
										        <%# Container.DataItemIndex + 1 %>
									        </ItemTemplate>
								        </asp:TemplateField> 
                                        <asp:BoundField HeaderText="No Staf" DataField="PO04_StafID" SortExpression="PO04_StafID" ReadOnly="true" ItemStyle-Width="5%"/>
                                        <asp:BoundField HeaderText="Nama" DataField="PO04_NamaStaf" SortExpression="PO04_NamaStaf" ReadOnly="true" ItemStyle-Width="30%"/>
						                <asp:BoundField HeaderText="PTJ" DataField="PO04_KodPTJStaf" SortExpression="PO04_KodPTJStaf" ReadOnly="true" ItemStyle-Width="8%"/>
                                        <asp:BoundField HeaderText="Jawatan" DataField="PO04_JawStaf" SortExpression="Jawatan" ReadOnly="true" ItemStyle-Width="20%"/>
                                        <asp:BoundField HeaderText="Email" DataField="PO04_EmailStaf" SortExpression="PO04_EmailStaf" ReadOnly="true" ItemStyle-Width="10%"/>
                                        <asp:TemplateField HeaderText="Kod Pengesahan" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">                                            
                                            <ItemTemplate>
                                                <asp:Textbox ID="txtUnikId" runat="server" Width="60px" TextMode="Number" MaxLength="4"></asp:Textbox>                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Sah?" DataField="Sah" SortExpression="Sah" ReadOnly="true" ItemStyle-Width="5%"/>                                                                                                                           
                                        <asp:TemplateField HeaderText="Tidak Hadir?" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="2%">                                            
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chxTidakHadir" runat="server" onclick = "Check_Click(this)" Checked='<%#IIf(IsDBNull(Eval("PO04_TidakHadir")), False, Eval("PO04_TidakHadir")) %>' Tooltip="Sila check jika tidak hadir"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="4%" ItemStyle-HorizontalAlign="Center" HeaderText="Tindakan">
										    <ItemTemplate>
											 <asp:LinkButton ID="lbtnSave" runat="server" CommandName="Update" CssClass="btn-xs" ToolTip="Simpan">
												<i class="far fa-save fa-lg"></i>
											</asp:LinkButton>
                                            </ItemTemplate>
							        </asp:TemplateField>
                                    </columns>
                                </asp:GridView>
                                <br/>
                                
                            
              

                                </div>
                        </div>
                <div class="row">
					<div class="col-md-2" style="text-align:left">                    
							<asp:LinkButton ID="lbtnPrevView3" runat="server" CssClass="btn btn-default btn-circle btn-lg" ToolTip="Seterusnya">
                                <i class="glyphicon glyphicon-chevron-right"></i>
                            </asp:LinkButton>
						</div>
					   <div class="col-md-10" style="text-align:center">                   
							<asp:LinkButton ID="lbtnHantar" runat="server" CssClass="btn btn-info" ValidationGroup="lbtnHantar" Visible="false" OnClientClick="return confirm('Anda pasti untuk hantar rekod ini?');">
                        <i class="fab fa-telegram-plane fa-lg"></i>&nbsp;&nbsp;&nbsp;Hantar
                    </asp:LinkButton>
                            &nbsp;&nbsp;
                    <asp:LinkButton ID="lbtnPrint" runat="server" CssClass="btn btn-info" ToolTip="Cetak" OnClientClick = "return PrintPanel();" Visible="false">
                        <i class="fas fa-print fa-lg"></i>&nbsp;&nbsp;&nbsp;Cetak
                    </asp:LinkButton>					                     
						</div>
						</div>

            </asp:View>                              
        
        </asp:MultiView>
        </ContentTemplate>
        </asp:UpdatePanel>

</asp:Content>

