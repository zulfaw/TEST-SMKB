<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="MaklumatPembuka.aspx.vb" Inherits="SMKB_Web_Portal.MaklumatPembuka" %>
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
                                    &nbsp;&nbsp; Masa : &nbsp;&nbsp;
                                    <asp:TextBox ID="txtMasaMulaPembuka" runat="server" CssClass="form-control" Width="100px" TextMode="Time"></asp:TextBox>                                    
                                    <asp:RequiredFieldValidator ID="rfvtxtTujuan" runat="server" ControlToValidate="txtMasaMulaPembuka" ErrorMessage="" ForeColor="Red" Text="*Sila Isi" ValidationGroup="lbtnHantar" Display="Dynamic" ></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            
                            
                        </table>
                    
                        <b>Senarai petender:</b>
                        <br />
                        <asp:GridView ID="gvPembuka" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText=" Tiada rekod"
							cssclass="table table-striped table-bordered table-hover" BorderStyle="Solid" font-size="8pt" DataKeyNames="PO03_OrderID">
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
                      
                    </div>
                    
                     <div style="text-align:center">
                     <%--<asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn" ToolTip="Simpan & Seterusnya" ValidationGroup="btnSimpan">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
					</asp:LinkButton> --%>
                         <i class="fas fa-info-circle fa-lg" aria-hidden="true" data-html="true" data-toggle="tooltip" data-placement="bottom" style="cursor:pointer;color:#ba2818;"  title="Butang Hantar adalah proses terakhir. Tiada perubahan boleh dilakukan selepas klik butan Hantar. Oleh itu, pastikan klik pada petak checkbox Padan pada gridview tersebut "></i>
                     &nbsp;&nbsp;                        
                    <asp:LinkButton ID="lbtnHantar" runat="server" CssClass="btn btn-info" ValidationGroup="lbtnHantar" OnClientClick="return confirm('Anda pasti untuk hantar rekod ini?');">
                        <i class="fab fa-telegram-plane fa-lg"></i>&nbsp;&nbsp;&nbsp;Hantar
                    </asp:LinkButton>
                    &nbsp;&nbsp;
                    <asp:LinkButton ID="lbtnPrint" runat="server" CssClass="btn btn-info" ToolTip="Cetak" OnClientClick = "return PrintPanel();" Visible="false">
                        <i class="fas fa-print fa-lg"></i>&nbsp;&nbsp;&nbsp;Cetak
                    </asp:LinkButton>
                    <p></p>
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
        </asp:UpdatePanel>

</asp:Content>
