<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CRIM1.aspx.vb" Inherits="SMKB_Web_Portal.CRIM" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %> 

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
    <h1>Maklumat Sebut Harga/Tender Bekalan dan Perkhidmatan</h1>
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <p></p>
            <div class="row" style="width:100%;">
            <div class="col-sm-9 col-md-6 col-lg-8" style="width:100%;">
            <ul class="list-inline" style="text-align:center;">
                <li>                       
                    <asp:LinkButton ID="LinkButton" runat="server" CssClass="btn btn-primary btn-circle btn-xl" ToolTip="Maklumat Permohonan">
                        <i class="fa fa-folder-open-o"></i>
                    </asp:LinkButton>
                </li>
                <li>
                    <i class="fa fa-arrow-right fa-lg" aria-hidden="true"></i>
                </li>
                <li>                            
                    <asp:LinkButton ID="LinkButton11" runat="server" ValidationGroup="btnStep1" CssClass="btn btn-primary btn-circle btn-xl " ToolTip="Maklumat Bajet Lanjut">
                        <i class="fa fa-paper-plane-o"></i>
                    </asp:LinkButton>
                </li>                
            </ul>
            
            
            <asp:MultiView ID="mvGrant" runat="server" ActiveViewIndex="0">
                <asp:View ID="vGrant" runat="server">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                RESEARCH GRANT ACCEPTANCE
                            </h3>
                        </div>
                    <div class="panel-body">
                            
							<table style="width:100%">							
                            
                            
							</table>
							
							<br />
                            <div class="panel panel-default">
                                <div class="panel-heading">
                            <h3 class="panel-title">
                                A. PROJECT DETAILS
                            </h3>
                        </div>
                            <div class="panel-body">
                                <table style="width:100%">							
                                   <tr>
                    <td style="width: 25%;"><Label class="control-label" for="">Principal Researcher</Label></td>
                    <td>
                        <asp:TextBox ID="txtPrincipal" runat="server" Width="550px" CssClass="form-control" ReadOnly="true" ></asp:TextBox>
                        &nbsp &nbsp &nbsp &nbsp
                        <Label class="control-label" for="">Tarikh Mohon</Label>
                        &nbsp &nbsp
                        <asp:TextBox ID="txtTarikhMohon" runat="server" Width="10%" CssClass="form-control" ReadOnly="true" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%;"><Label class="control-label" for="">Faculty/Centre</Label></td>
                    <td>
                        <asp:DropDownList ID="ddlFac" runat="server" CssClass="form-control" Width="60%">
                        </asp:DropDownList>
                    </td>
				</tr>
                <tr>
                    <td style="width: 25%;"><Label class="control-label" for="">Contact No.(Off./HP)</Label></td>
                    <td>
                        <asp:TextBox ID="txtNoHP" runat="server" CssClass="form-control" Width="30%"></asp:TextBox>
                    </td>
				</tr>
                <tr>
                    <td style="width: 25%;"><Label class="control-label" for="">Email</Label></td>
                    <td>
                        <asp:TextBox ID="txtAgensi" runat="server" CssClass="form-control" Width="30%"  value=""></asp:TextBox>
                    </td>
				</tr>
                <tr>
                    <td style="width: 25%;"><Label class="control-label" for="">Project Title</Label></td>
                    <td>
                        <asp:TextBox ID="txtProjTitle" runat="server" CssClass="form-control" Width="60%" textmode="multiline"></asp:TextBox>
                    </td>
				</tr>
                <tr>
                    <td style="width: 25%;"><Label class="control-label" for="">Project No.</Label></td>
                    <td>
                        <asp:TextBox ID="txtProjNo" runat="server" CssClass="form-control" Width="30%" ReadOnly="true"></asp:TextBox>
                    </td>
				</tr>
                <tr>
                    <td style="width: 25%;"><Label class="control-label" for="">Date of Approval (dd/mm/yy)</Label></td>
                    <td>
                        <asp:TextBox ID="txtDateApproval" runat="server" CssClass="form-control" Width="30%" ReadOnly="true"></asp:TextBox>
                    </td>
				</tr>
                <tr>
                    <td style="width: 25%;"><Label class="control-label" for="">Project Duration (date)</Label></td>
                    <td>  &nbsp; From &nbsp;
                      <asp:TextBox ID="txtDateFrom" runat="server" CssClass="form-control" Text="<%# System.DateTime.Now%>" Width="100px"></asp:TextBox>
                         <cc1:CalendarExtender ID="calDateFrom" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtDateFrom" TodaysDateFormat="dd/MM/yyyy" />  
                      &nbsp; To &nbsp;
                      <asp:TextBox ID="txtDateTo" runat="server" CssClass="form-control" Text="<%# System.DateTime.Now%>" Width="100px"></asp:TextBox>
                      <cc1:CalendarExtender ID="calDateTo" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtDateTo" TodaysDateFormat="dd/MM/yyyy" />
                     </td>         

				</tr>
                <tr>
                    <td style="width: 25%;"><Label class="control-label" for="">Total Approved Duration</Label></td>
                    <td>
                        <asp:TextBox ID="txtTotalDura" runat="server" CssClass="form-control" ReadOnly ="true" Width="30%"></asp:TextBox>
                    </td>
				</tr>
                <tr>
                    <td style="width: 15%; height: 22px;"><Label class="control-label" for="">Total Approved Budget (RM)</Label></td>
                    <td style="height: 22px">
                        <asp:TextBox ID="txtAppBg" runat="server" CssClass="form-control" Width="30%"></asp:TextBox>
                    </td>
				</tr>
                            
							    </table>
                                

                            </div>
                            </div>
                            

                            <div class="panel panel-default">
                                <div class="panel-heading">
                            <h3 class="panel-title">
                                B. RESEARCH ADMISSION
                            </h3>
                        </div>
                            <div class="panel-body">
                                				
                                <h5 ><b>
                                I, as the Principal Investigator/Co-Investigator of the above mentioned research project hereby agree to abide by the terms and conditions of the grant and will follow the University Research and Innovation Policy and Guidelines. I agree to work towards research excellence for the University and the country.
                                </h5> </b>
                                         
							     <table style="width:100%">
                <tr>
                    <td style="width: 20%;"><Label class="control-label" for="">Name of Researchers (Staff No.)</Label></td>
                    <td>
                        <asp:TextBox ID="txtNameRea" runat="server" CssClass="form-control" Width="30%"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td style="width: 20%;"><Label class="control-label" for="">Contact No.</Label></td>
                    <td>
                <asp:TextBox ID="txtContact" runat="server" CssClass="form-control" Width="20%"></asp:TextBox>                    </td>
                </tr>
                <tr>
                    <td style="width: 20%;"><Label class="control-label" for="">Designation (Faculty)</Label></td>
                    <td>
                        <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control" Width="20%" TextMode="Number" AutoPostBack="true"></asp:TextBox>
                    </td>
                </tr>
                  
                <tr>
                    
                    <td style="height:40px; text-align:center;" colspan="2" >
                    <asp:LinkButton ID="lbtnReset" runat="server" CssClass="btn btn-info" ToolTip="Kosongkan Butiran Perbelanjaan">
                        <i class="fa fa-refresh fa-lg"></i>&nbsp;&nbsp;&nbsp;Reset
                    </asp:LinkButton> &nbsp;&nbsp;
                    <asp:LinkButton ID="lbtnSaveButiran" runat="server" CssClass="btn btn-info" ValidationGroup="btnSaveButiran" ToolTip="Tambah ke senarai">
                        <i class="fa fa-plus-circle fa-lg"></i>&nbsp;&nbsp;&nbsp;Tambah
                    </asp:LinkButton>                    
                    </td>
                </tr>
            </table>
             <br />
            <asp:GridView ID="gvRA" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="False" AllowPaging="false" Enabled="false" EmptyDataText =" "
                cssclass="table table-striped table-bordered table-hover" Width="100%" Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" ShowFooter="false">
                    <columns>
                    <asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" SortExpression="Bil" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name of Researchers (Staff No.)" ItemStyle-Width="3%" SortExpression="nameRA" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Right">
                   </asp:TemplateField> 
                    <asp:TemplateField HeaderText="Contact No." ItemStyle-Width="50%" SortExpression="Butiran">
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="Designation (Faculty)" ItemStyle-Width="3%" SortExpression="designation" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Right">
                    </asp:TemplateField>                                                                 
                    <asp:TemplateField>
                        <EditItemTemplate>
                            <asp:ImageButton ID="btnSave1" runat="server" CommandName="Update" Height="20px" ImageUrl="~/Images/Save_48x48.png" 
                                ToolTip="Simpan" Width="15px" OnItemCommand="gvButiranModul_ItemCommand" />
                                &nbsp;&nbsp;
                            <asp:ImageButton ID="btnCancel" runat="server" CommandName="Cancel" Height="20px" ImageUrl="~/Images/Cancel16x16.png" 
                                    ToolTip="Batal" Width="15px" OnItemCommand="gvButiranModul_ItemCommand" />
                                &nbsp;&nbsp;
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="btnEdit" runat="server" CommandName="Edit" Height="20px" ImageUrl="~/Images/Edit_48.png" 
                                ToolTip="Kemas Kini" Width="15px" OnItemCommand="gvButiranModul_ItemCommand" />
                            &nbsp;&nbsp;
				            <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete"  ImageUrl="~/Images/Delete_32x32.png" 
					            ToolTip="Padam" Width="15px" Height="18px" OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');" />
			            </ItemTemplate>
			            <ItemStyle Width="5%" />
			        </asp:TemplateField>
                </columns>
                    <HeaderStyle BackColor="#996633" />
            </asp:GridView>
                                </div> 

                                <h5 ><b>
                                    Notification on the updates of co-investigators as per above list compared with the original research proposal (if any)
                                </h5> </b>
                              
                                <h6 >
                                    i. Name of the co-investigator(s) that has been added / removed:
                                </h6> 
                               
                                <table style="width:100%">
                <tr>
                    <td style="width: 20%;"><Label class="control-label" for="">Name of Co-Investigator (Staff No.)</Label></td>
                    <td>
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" Width="30%"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td style="width: 20%;"><Label class="control-label" for="">Contact No.</Label></td>
                    <td>
                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" Width="20%"></asp:TextBox>                    </td>
                </tr>
                <tr>
                    <td style="width: 20%;"><Label class="control-label" for="">Designation (Faculty)</Label></td>
                    <td>
                        <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" Width="20%" TextMode="Number" AutoPostBack="true"></asp:TextBox>
                    </td>
                </tr>
                  
                <tr>
                    
                    <td style="height:40px; text-align:center;" colspan="2" >
                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-info" ToolTip="Kosongkan Butiran Perbelanjaan">
                        <i class="fa fa-refresh fa-lg"></i>&nbsp;&nbsp;&nbsp;Reset
                    </asp:LinkButton> &nbsp;&nbsp;
                    <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-info" ValidationGroup="btnSaveButiran" ToolTip="Tambah ke senarai">
                        <i class="fa fa-plus-circle fa-lg"></i>&nbsp;&nbsp;&nbsp;Tambah
                    </asp:LinkButton>                    
                    </td>
                </tr>
            </table>
             <br />
            <asp:GridView ID="GridView1" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="False" AllowPaging="false" Enabled="false" EmptyDataText =" "
                cssclass="table table-striped table-bordered table-hover" Width="100%" Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" ShowFooter="false">
                    <columns>
                    <asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" SortExpression="Bil" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name of Co-Investigator (Staff No.)" ItemStyle-Width="3%" SortExpression="nameRA" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Right">
                   </asp:TemplateField> 
                    <asp:TemplateField HeaderText="Contact No." ItemStyle-Width="50%" SortExpression="Butiran">
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="Designation (Faculty)" ItemStyle-Width="3%" SortExpression="designation" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Right">
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="Status" ItemStyle-Width="3%" SortExpression="status" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Right">
                    </asp:TemplateField>                                                                 
                    <asp:TemplateField>
                        <EditItemTemplate>
                            <asp:ImageButton ID="btnSave1" runat="server" CommandName="Update" Height="20px" ImageUrl="~/Images/Save_48x48.png" 
                                ToolTip="Simpan" Width="15px" OnItemCommand="gvButiranModul_ItemCommand" />
                                &nbsp;&nbsp;
                            <asp:ImageButton ID="btnCancel" runat="server" CommandName="Cancel" Height="20px" ImageUrl="~/Images/Cancel16x16.png" 
                                    ToolTip="Batal" Width="15px" OnItemCommand="gvButiranModul_ItemCommand" />
                                &nbsp;&nbsp;
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="btnEdit" runat="server" CommandName="Edit" Height="20px" ImageUrl="~/Images/Edit_48.png" 
                                ToolTip="Kemas Kini" Width="15px" OnItemCommand="gvButiranModul_ItemCommand" />
                            &nbsp;&nbsp;
				            <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete"  ImageUrl="~/Images/Delete_32x32.png" 
					            ToolTip="Padam" Width="15px" Height="18px" OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');" />
			            </ItemTemplate>
			            <ItemStyle Width="5%" />
			        </asp:TemplateField>
                </columns>
                    <HeaderStyle BackColor="#996633" />
            </asp:GridView>
                    <ul class="list-inline pull-right">
                        <li><%--<asp:button runat="server" class="btn btn-primary next-step">next</asp:button>--%>
                            
                            <asp:LinkButton ID="btnStep" runat="server" ValidationGroup="btnStep" CssClass="btn btn-primary btn-circle btn-lg next-step" ToolTip="Next">
                                <i class="glyphicon glyphicon-chevron-right"></i>
                            </asp:LinkButton>
                        </li>
                    </ul>
                </asp:View>
                <asp:View ID="vGrant2" runat="server">
                    
                        <ul class="list-inline pull-right">
                            <asp:LinkButton ID="LinkButton9" runat="server" CssClass="btn btn-default btn-circle btn-lg prev-step" ToolTip="Previous">
                                    <i class="glyphicon glyphicon-chevron-left"></i>
                                </asp:LinkButton>
                            <asp:LinkButton ID="LinkButton10" runat="server" ValidationGroup="btnStep2" CssClass="btn btn-primary btn-circle btn-lg next-step" ToolTip="Next">
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
