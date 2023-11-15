<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Mohon_Bajet_Penyelidikan.aspx.vb" Inherits="SMKB_Web_Portal.Mohon_Bajet_Penyelidikan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<script>
    debugger
    function fEmptyTextBox() {
        $("#txtPerkara").val("")
        $("#txtKuantiti").val("")
        $("#ddlUnit").val("")
        $("#txtAngHrgSeunit").val("")
        $("#txtJumAngHrg").val("")
    };

    function fAddSpekAm() {
        //Reference the GridView.
        var gridView = $("[id*=gvSpekAM]");

        var tableSpek = $("[id*=tableSpek]")
        //Reference the first row.
        var row = tableSpek.find("tr").eq(1);
        //Check if row is dummy, if yes then remove.
        if ($.trim(row.find("td").eq(0).html()) == "") {
            row.remove();
        }
        //Clone the reference first row.
        row = row.clone(true);

            

 
        //Add the Name value to first cell.
        var txtPerkara = $("[id*=txtPerkara]");
        SetValue(row, 0, "Perkara", txtPerkara);
 
        //Add the Country value to second cell.
        var txtKuantiti = $("[id*=txtKuantiti]");
        SetValue(row, 1, "Kuantiti", txtKuantiti);
 
        //Add the row to the GridView.
        gridView.append(row);
 
        return false;
    };
 
    function SetValue(row, index, name, textbox) {
        //Reference the Cell and set the value.
        row.find("td").eq(index).html(textbox.val());
 
        //Create and add a Hidden Field to send value to server.
        var input = $("<input type = 'hidden' />");
        input.prop("name", name);
        input.val(textbox.val());
        row.find("td").eq(index).append(input);
 
        //Clear the TextBox.
        textbox.val("");
    
    };

</script>

<style type="text/css">

.btn-circle {
  width: 30px;
  height: 30px;
  text-align: center;
  padding: 6px 0;
  font-size: 12px;
  line-height: 1.428571429;
  border-radius: 15px;
}
.btn-circle.btn-lg {
    width: 50px;
    height: 50px;
    padding: 1px 10px;
    border-radius: 35px;
    font-size: 25px;
    line-height: 50px;
}

.btn-circle.btn-xl {
  width: 70px;
  height: 70px;
  padding: 10px 16px;
  font-size: 35px;
  line-height: 1.33;
  border-radius: 35px;
}

.rightAlign { text-align:right; }

</style>
    <h1>RESEARCH GRANT ACCEPTANCE</h1>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <p></p>
            <div class="row" style="width:100%;">
            <div class="col-sm-9 col-md-6 col-lg-8" style="width:100%;">
            <ul class="list-inline" style="text-align:center;">
                <li>                       
                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-primary btn-circle btn-xl" ToolTip="Maklumat Bajet">
                        <i class="fa fa-folder-open-o"></i>
                    </asp:LinkButton>
                </li>
                <li>
                    <i class="fa fa-arrow-right fa-lg" aria-hidden="true"></i>
                </li>
                <li>                            
                    <asp:LinkButton ID="LinkButton2" runat="server" ValidationGroup="btnStep1" CssClass="btn btn-primary btn-circle btn-xl " ToolTip="Maklumat Perolehan">
                        <i class="fa fa-paper-plane-o"></i>
                    </asp:LinkButton>
                </li>                
            </ul>
            
            
            <asp:MultiView ID="mvMohonBeli" runat="server" ActiveViewIndex="0">
                <asp:View ID="vBeli1" runat="server">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                Maklumat Sebut Harga / Tender
                            </h3>
                        </div>
                    <div class="panel-body">
                            
							<table style="width:100%">							
                            <tr>
                                <td  style="vertical-align:top; width: 15%;"><Label class="control-label" for=""><b>A. Title</b></Label></td>
                                <td style="width: 85%;">
                                    <asp:TextBox ID="TextBox2" runat="server" style="height:auto; min-height:100px; width: 60%;" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                </td>
							</tr>
                            <tr>
                                <td  style="vertical-align:top; width: 15%;"><Label class="control-label" for=""><b>B. Scope</b></Label></td>
                                <td style="width: 85%;">
                                    <asp:TextBox ID="TextBox3" runat="server" style="height:auto; min-height:100px; width: 60%;" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                </td>
							</tr>
                            
							</table>
							
							<br />
                            <div class="panel panel-default">
                                <div class="panel-heading">
                            <h3 class="panel-title">
                                1.0 General Specification
                            </h3>
                        </div>
                            <div class="panel-body">
                                <table style="width:100%">							
                                    <tr>
                                        <td  style="vertical-align:top; width: 15%;"><Label class="control-label" for=""><b>Division</b></Label></td>
                                        <td style="width: 85%;">
                                            <asp:DropDownList ID="ddlGenSpecItem" runat="server" CssClass="form-control" AutoPostBack="true" style="width: 50%;"></asp:DropDownList>
                                            &nbsp
                                            <asp:LinkButton ID="LinkButton7" runat="server" CssClass="btn btn-primary btn-circle" ToolTip="Kemaskini item division">
                                                <i class="glyphicon glyphicon-plus"></i>
                                            </asp:LinkButton>
                                        </td>
							        </tr>
                                    <tr>
                                        <td  style="vertical-align:top; width: 15%;"><Label class="control-label" for=""><b>Matter</b></Label></td>
                                        <td style="width: 85%;">
                                            <%--columns="100"--%>
                                            <asp:TextBox ID="txtGenSpecItem" runat="server" TextMode="MultiLine" Rows="10" columns="100"></asp:TextBox><br />
                                            <ajaxToolkit:HtmlEditorExtender ID="HtmlEditorExtender1" TargetControlID="txtGenSpecItem" runat="server" DisplaySourceTab="false">
                                                <Toolbar>
                                                    <ajaxToolkit:Bold />                                                
                                                    <ajaxToolkit:InsertOrderedList />
                                                    <ajaxToolkit:InsertUnorderedList />                                                    
                                                </Toolbar>
                                            </ajaxToolkit:HtmlEditorExtender>
                                        </td>
							        </tr>
                            
							    </table>
                                <br />
                                <asp:GridView ID="GridView1" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="False" AllowPaging="false" EmptyDataText=" "
								cssclass="table table-striped table-bordered table-hover" Width="100%" HeaderStyle-BackColor="#6699FF" Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" ShowFooter="false">
								 <columns>
									<asp:BoundField DataField="Bil" HeaderText="Bil" SortExpression="Bil" ReadOnly="True">
										<ItemStyle Width="2%" />
									</asp:BoundField>
									<asp:BoundField HeaderText="Division" DataField="Division" SortExpression="Division">
										 <ItemStyle Width="25%" />
									</asp:BoundField>
									<%--<asp:BoundField HeaderText="Description" DataField="Description" SortExpression="Description" ReadOnly="true">
										 <ItemStyle Width="35%" />
									</asp:BoundField>--%>
									<asp:BoundField HeaderText="Matter" DataField="Matter" SortExpression="Matter">
										 <ItemStyle Width="35%" />
									</asp:BoundField>                                                                     
									<asp:TemplateField>
			                            <ItemTemplate>
				                            &nbsp;&nbsp;
				                            <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete"  ImageUrl="~/Images/Delete_32x32.png" 
					                            ToolTip="Padam" Width="15px" Height="18px" OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');" />
			                            </ItemTemplate>
			                            <ItemStyle Width="5%" />
			                        </asp:TemplateField>
								</columns>
							</asp:GridView>

                            </div>
                            </div>
                            

                            <div class="panel panel-default">
                                <div class="panel-heading">
                            <h3 class="panel-title">
                                2.0 Technical Specification
                            </h3>
                        </div>
                            <div class="panel-body">
                                
                                <table style="width:100%">							
                                    <tr>
                                        <td  style="vertical-align:top; width: 15%;"><Label class="control-label" for=""><b>Division</b></Label></td>
                                        <td style="width: 85%;">
                                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" AutoPostBack="true" style="width:50%"></asp:DropDownList>
                                            &nbsp
                                            <asp:LinkButton ID="LinkButton6" runat="server" CssClass="btn btn-primary btn-circle" ToolTip="Kemaskini item division">
                                                <i class="glyphicon glyphicon-plus"></i>
                                            </asp:LinkButton>
                                        </td>
							        </tr>
                                    <tr>
                                        <td  style="vertical-align:top; width: 15%;"><Label class="control-label" for=""><b>Matter</b></Label></td>
                                        <td style="width: 85%;">
                                            <asp:TextBox runat="server" ID="txtBox2" TextMode="MultiLine" Columns="100" Rows="10"></asp:TextBox><br />
                                            <ajaxToolkit:HtmlEditorExtender ID="htmlEditorExtender2" TargetControlID="txtBox2"
                                                runat="server" DisplaySourceTab="true" DisplayPreviewTab="true">
                                                <Toolbar>
                                                    <ajaxToolkit:Bold />                                                
                                                    <ajaxToolkit:InsertOrderedList />
                                                    <ajaxToolkit:InsertUnorderedList /> 
                                                </Toolbar>
                                            </ajaxToolkit:HtmlEditorExtender>
                                        </td>
							        </tr>
                            
							    </table>

                                <asp:GridView ID="GridView2" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="False" AllowPaging="false" EmptyDataText=" "
								cssclass="table table-striped table-bordered table-hover" Width="100%" HeaderStyle-BackColor="#6699FF" Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" ShowFooter="false">
								 <columns>
									<asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" SortExpression="Bil">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
									<asp:BoundField HeaderText="Division" DataField="Division" SortExpression="Division">
										 <ItemStyle Width="25%" />
									</asp:BoundField>
									<%--<asp:BoundField HeaderText="Description" DataField="Description" SortExpression="Description" ReadOnly="true">
										 <ItemStyle Width="35%" />
									</asp:BoundField>--%>
									<asp:BoundField HeaderText="Matter" DataField="Matter" SortExpression="Matter">
										 <ItemStyle Width="35%" />
									</asp:BoundField>                                                                     
									<asp:TemplateField>
			                            <ItemTemplate>
				                            &nbsp;&nbsp;
				                            <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete"  ImageUrl="~/Images/Delete_32x32.png" 
					                            ToolTip="Padam" Width="15px" Height="18px" OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');" />
			                            </ItemTemplate>
			                            <ItemStyle Width="5%" />
			                        </asp:TemplateField>
								</columns>
							</asp:GridView>
                            </div>
                            </div>

                            </div>
					</div>


                    <ul class="list-inline pull-right">
                        <li><%--<asp:button runat="server" class="btn btn-primary next-step">next</asp:button>--%>
                            
                            <asp:LinkButton ID="btnStep1" runat="server" ValidationGroup="btnStep1" CssClass="btn btn-primary btn-circle btn-lg next-step" ToolTip="Next">
                                <i class="glyphicon glyphicon-chevron-right"></i>
                            </asp:LinkButton>
                        </li>
                    </ul>
                </asp:View>
                <asp:View ID="vBeli2" runat="server">
                    
                        <ul class="list-inline pull-right">
                            <asp:LinkButton ID="LinkButton4" runat="server" CssClass="btn btn-default btn-circle btn-lg prev-step" ToolTip="Previous">
                                    <i class="glyphicon glyphicon-chevron-left"></i>
                                </asp:LinkButton>
                            <asp:LinkButton ID="LinkButton5" runat="server" ValidationGroup="btnStep2" CssClass="btn btn-primary btn-circle btn-lg next-step" ToolTip="Next">
                                    <i class="glyphicon glyphicon-send"></i>
                                </asp:LinkButton>
                        </ul>
                        
                            
                </asp:View>
                <%--<asp:View ID="vBeli3" runat="server">
                    
                        <ul class="list-inline pull-right">
                            <asp:LinkButton ID="btnPrevStep2" runat="server" CssClass="btn btn-default btn-circle btn-lg prev-step" ToolTip="Previous">
                                    <i class="glyphicon glyphicon-chevron-left"></i>
                                </asp:LinkButton>
                            <asp:LinkButton ID="btnStep2" runat="server" ValidationGroup="btnStep2" CssClass="btn btn-primary btn-circle btn-lg next-step" ToolTip="Next">
                                    <i class="glyphicon glyphicon-chevron-right"></i>
                                </asp:LinkButton>
                        </ul>
                </asp:View>--%>
            </asp:MultiView>
        <%--</div> <%-- wizard --%>
               </div> </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
