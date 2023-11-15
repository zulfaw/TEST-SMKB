<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Permohonan_Perolehan.aspx.vb" Inherits="SMKB_Web_Portal.Permohonan_Perolehan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Permohonan Perolehan</h1>

    <link rel="stylesheet" type="text/css" href="../../../Content/Site.css">

    <%--<script type="text/javascript">
    
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
</script>--%>

	<asp:UpdatePanel ID="UpdatePanel1" runat="server">
		<ContentTemplate>
  
			<p></p>

			
			<div class="panel panel-default">
				<div class="panel-heading">
					<h3 class="panel-title">
						Senarai Permohonan Perolehan 
					</h3>
				</div>
				<div class="panel-body" style="overflow-x:auto">
                    Carian Status&nbsp;:&nbsp;&nbsp;           
                    <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" CssClass="form-control" ></asp:DropDownList>					
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    Tahun&nbsp;:
                    <asp:TextBox ID="txtTahun" runat="server" CssClass="form-control" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
                    
                    <br /><br />

					<asp:GridView ID="gvMohonPO" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="false" EmptyDataText=" Tiada rekod"
							cssclass="table table-striped table-bordered table-hover" Width="100%" BorderStyle="Solid" ShowFooter="true" Font-Size="8pt" DataKeyNames="PO01_NoMohonSem">
								<columns>
                                 <%--<asp:TemplateField HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="2%">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat="server" text="Hantar" onclick = "checkAll(this);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" onclick = "Check_Click(this)" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
								<asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" >
									<ItemTemplate>
										<%# Container.DataItemIndex + 1 %>
									</ItemTemplate>
								</asp:TemplateField>
                                <%--<asp:TemplateField Visible=false ItemStyle-Width="1%">
								<ItemTemplate>
									<asp:Label id="lblNoPoSem" runat ="server" text='<%# Eval("PO01_NoMohonSem")%>' ></asp:Label>
								</ItemTemplate>
							    </asp:TemplateField>--%>
								<asp:BoundField HeaderText="No Perolehan" DataField="PO01_NoMohon" SortExpression="PO01_NoMohon" HeaderStyle-CssClass="centerAlign">
									<ItemStyle Width="5%" HorizontalAlign="Center"/>
								</asp:BoundField>						            
								<asp:BoundField HeaderText="Tujuan" DataField="PO01_Tujuan" SortExpression="PO01_Tujuan" HeaderStyle-CssClass="centerAlign">
									<ItemStyle Width="35%" />
								</asp:BoundField>
								<asp:BoundField HeaderText="Kategori" DataField="Kategori" SortExpression="Kategori" HeaderStyle-CssClass="centerAlign">
									<ItemStyle Width="5%" />
								</asp:BoundField>
                                <asp:BoundField HeaderText="Tarikh Mohon" DataField="PO01_TkhMohonSem" SortExpression="PO01_TkhMohonSem" HeaderStyle-CssClass="centerAlign" DataFormatString="{0:dd/MM/yyyy}">
                                    <ItemStyle Width="7%" HorizontalAlign="Center"/>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Anggaran Perbelanjaan (RM)" DataField="AnggaranBelanja" SortExpression="AnggaranBelanja" DataFormatString="{0:N}" HeaderStyle-CssClass="centerAlign">
                                    <ItemStyle Width="10%" HorizontalAlign="Right"/>                       
                                </asp:BoundField>
								<asp:BoundField HeaderText="Status" DataField="Butiran" SortExpression="Butiran" HeaderStyle-CssClass="centerAlign">
									<ItemStyle Width="20%" />
								</asp:BoundField>                                                                                             
								<asp:TemplateField>                        
								<ItemTemplate>
										<asp:LinkButton ID="lbDetail" runat="server" CommandName="Select" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CssClass="btn-xs btn-primary" ToolTip="Maklumat Lanjut">
											<i class="fa fa-ellipsis-h fa-lg"></i>
										</asp:LinkButton>                                    
									</ItemTemplate>
									<ItemStyle Width="3%" HorizontalAlign="Center"/>
								</asp:TemplateField>
							</columns>
						</asp:GridView>
					<div style="text-align:center">
                        <asp:Button ID="btnMohonBaru" text="Mohon Baru" runat="server" CssClass="btn" />
						<%--&nbsp;&nbsp;&nbsp;
						<asp:Button ID="btnHantar" text="Hantar" runat="server" CssClass="btn" />--%>
					</div>

				</div>
                </div>
			
             <%--<asp:Button ID = "btnOpen" runat = "server" Text = ""  style="display:none" />
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground" 
            CancelControlID="lbNo" PopupControlID="pnlpopupHantar" TargetControlID="btnOpen" 
            > </ajaxToolkit:ModalPopupExtender>
        
            <asp:Panel ID="pnlpopupHantar" runat="server" BackColor="White" Width="70%" Style="display: none">
               <table style="border: Solid 3px #D5AA00; width: 100%; height: 100%; " >
                    <tr style="background: linear-gradient(to bottom, #fceabb 0%,#fccd4d 50%,#f8b500 51%,#fbdf93 100%);">
                        <td style="height: 10%;  font-weight: bold; text-align:center;" colspan="2" >
                            Maklumat Lanjut
                        </td>
                        
                    </tr>
                    <tr style="vertical-align:top;">
                        <td colspan="2">
                           <div style="margin-bottom:10px;margin-top:10px;margin-left:10px;margin-right:10px;">
                              <asp:GridView ID="gvHantar" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="False" AllowPaging="false" EmptyDataText=" Tiada rekod"
                cssclass="table table-striped table-bordered table-hover" Width="100%" HeaderStyle-BackColor="#6699FF" BorderStyle="Solid" ShowFooter="false" Font-Size="8pt">
                    <columns>
                    
                    <asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="No Perolehan" DataField="NoMohon" SortExpression="NoMohon" HeaderStyle-CssClass="centerAlign">
						<ItemStyle Width="5%" HorizontalAlign="Center"/>
					</asp:BoundField>						            
					<asp:BoundField HeaderText="Tujuan" DataField="Program" SortExpression="Program" HeaderStyle-CssClass="centerAlign">
						<ItemStyle Width="35%" />
					</asp:BoundField>
					<asp:BoundField HeaderText="Kategori" DataField="Kategori" SortExpression="Kategori" HeaderStyle-CssClass="centerAlign">
						<ItemStyle Width="5%" />
					</asp:BoundField>
                    <asp:BoundField HeaderText="Tarikh Mohon" DataField="Tarikh" SortExpression="Tarikh" HeaderStyle-CssClass="centerAlign">
                        <ItemStyle Width="5%" HorizontalAlign="Center"/>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Anggaran Perbelanjaan (RM)" DataField="AngJumlah" SortExpression="AngJumlah" HeaderStyle-CssClass="centerAlign">
                        <ItemStyle Width="10%" HorizontalAlign="Right"/>                       
                    </asp:BoundField>
					<asp:BoundField HeaderText="Status" DataField="Status" SortExpression="Status" HeaderStyle-CssClass="centerAlign">
						<ItemStyle Width="20%" />
					</asp:BoundField>                  
                </columns>
            </asp:GridView>
                          </div>
                        </td>
                    </tr>
                   <tr style="vertical-align:top;">
                        <td style="height: 10%;  font-weight: bold; text-align:center;" colspan="2" >
                            <div style="margin-bottom:10px;margin-top:10px;margin-left:10px;margin-right:10px;">
                            Adakah anda setuju menghantar permohonan bajet seperti di atas?
                            </div>
                        </td>                        
                    </tr>
                   <tr>
                       <td style="height: 10%; text-align:center;" colspan="2" >
                           <div style="margin-bottom:10px;margin-top:10px;margin-left:10px;margin-right:10px;">
                            <asp:LinkButton ID="lbYes" runat="server" CssClass="btn btn-info">
                                <i class="fa fa-check fa-lg"></i>&nbsp;&nbsp;&nbsp;Ya
                            </asp:LinkButton>
                            &nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="lbNo" runat="server" CssClass="btn btn-info">
                                <i class="fa fa-times fa-lg"></i>&nbsp;&nbsp;&nbsp;Tidak
                            </asp:LinkButton>
                               </div>
                        </td>
                   </tr>
                </table>
            </asp:Panel>--%>
            

		</ContentTemplate>
        <%--<Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnHantar" EventName="Click" />
        </Triggers>--%>
	</asp:UpdatePanel>     
</asp:Content>
