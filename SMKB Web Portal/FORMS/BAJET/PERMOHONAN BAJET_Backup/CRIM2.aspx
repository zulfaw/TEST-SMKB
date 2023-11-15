<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CRIM2.aspx.vb" Inherits="SMKB_Web_Portal.CRIM" %>
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
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <p></p>
            <div class="row" style="width:100%;">
            <div class="col-sm-9 col-md-6 col-lg-8" style="width:100%;">
            <ul class="list-inline" style="text-align:center;">
                <li>                       
                    <asp:LinkButton ID="LinkButton21" runat="server" CssClass="btn btn-primary btn-circle btn-xl" ToolTip="Maklumat Permohonan">
                        <i class="fa fa-folder-open-o"></i>
                    </asp:LinkButton>
                </li>
                <li>
                    <i class="fa fa-arrow-right fa-lg" aria-hidden="true"></i>
                </li>
                <li>                            
                    <asp:LinkButton ID="LinkButton111" runat="server" ValidationGroup="btnStep1" CssClass="btn btn-primary btn-circle btn-xl " ToolTip="Maklumat Bajet Lanjut">
                        <i class="fa fa-paper-plane-o"></i>
                    </asp:LinkButton>
                </li>                
            </ul>
            
            
            <asp:MultiView ID="mvGrant1" runat="server" ActiveViewIndex="0">
                <asp:View ID="vGrant1" runat="server">
                    <div class="panel panel-default">
                       
                    <div class="panel-body">
                            
							<table style="width:100%">							
                            
                            
							</table>
							
							<br />
                            <div class="panel panel-default">
                                <div class="panel-heading">
                            <h3 class="panel-title">
                                C. BUDGET (Itemized Vot)
                            </h3>
                        </div>
                            <div class="panel-body">
                                 <table style="width:100%">
                <tr>
                    <td style="width: 20%;"><Label class="control-label" for="">Vot Sebagai</Label></td>
                    <td>
                        <asp:dropdownlist ID="ddlVotSbg" runat="server" CssClass="form-control" Width="70%"></asp:dropdownlist>
                        <asp:RequiredFieldValidator ID="rfvVotSbg" runat="server" ControlToValidate="ddlVotSbg" ErrorMessage="" ForeColor="#820303" Text="*Sila isi" ValidationGroup="btnSaveButiran" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>
                    </td>
                </tr>
                 <tr>
                    <td style="width: 20%;"><Label class="control-label" for="">Item</Label></td>
                    <td>
                        <asp:TextBox ID="txtButiran" runat="server" CssClass="form-control" Width="70%" TextMode="MultiLine"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvButiran" runat="server" ControlToValidate="txtButiran" ErrorMessage="" ForeColor="#820303" Text="*Sila isi" ValidationGroup="btnSaveButiran" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%;"><Label class="control-label" for="">Amount (RM)</Label></td>
                    <td>
                        <asp:TextBox ID="txtKuantiti" runat="server" CssClass="form-control" Width="100px" TextMode="Number" AutoPostBack="true"></asp:TextBox>
                        <a href="#" title="Kuantiti" data-toggle="popover" data-trigger="hover" data-content="Masukkan nilai '0' jika tiada jumlah kuantiti"><i class="fa fa-info-circle fa-lg"></i></a>
                        
                        <asp:RequiredFieldValidator ID="rfvKuantiti" runat="server" ControlToValidate="txtKuantiti" ErrorMessage="" ForeColor="#820303" Text="*Sila isi" ValidationGroup="btnSaveButiran" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>
                    </td>
                </tr>
               
                <tr>
                    
                    <td style="height:40px; text-align:center;" colspan="2" >
                    <asp:LinkButton ID="LinkButton3" runat="server" CssClass="btn btn-info" ToolTip="Kosongkan Butiran Perbelanjaan">
                        <i class="fa fa-refresh fa-lg"></i>&nbsp;&nbsp;&nbsp;Reset
                    </asp:LinkButton> &nbsp;&nbsp;
                    <asp:LinkButton ID="LinkButton4" runat="server" CssClass="btn btn-info" ValidationGroup="btnSaveButiran" ToolTip="Tambah ke senarai">
                        <i class="fa fa-plus-circle fa-lg"></i>&nbsp;&nbsp;&nbsp;Tambah
                    </asp:LinkButton>                    
                    </td>
                </tr>
            </table>
            <br />
            <asp:GridView ID="gvButiran" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="False" AllowPaging="false" Enabled="false"
                cssclass="table table-striped table-bordered table-hover" Width="100%" Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" ShowFooter="false">
                    <columns>
                    <asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" SortExpression="Bil" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="bil" SortExpression="bil" ReadOnly="true" Visible="False">
                        <ItemStyle Width="5%" HorizontalAlign="Center"/>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Kod Vot" ItemStyle-Width="3%" SortExpression="KodVot" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:label id="KodVot" runat="server" Text='<%# Eval("KodVot") %>'></asp:label>
                        </ItemTemplate>
                        
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlKodVot" runat="server" CssClass="form-control" Width="300px"></asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Item" ItemStyle-Width="50%" SortExpression="Butiran">
                        <ItemTemplate>
                            <%# Eval("Butiran") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtitem" runat="server" TextMode="MultiLine" CssClass="form-control" Width="100%" Text='<%# Eval("Butiran") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="amount" ItemStyle-Width="3%" SortExpression="Kuantiti" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%# Eval("Kuantiti") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtamount" runat="server" textmode="Number" CssClass="form-control rightAlign" Width="100%" Text='<%# Eval("Kuantiti") %>'></asp:TextBox>
                        </EditItemTemplate>
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
                    <FooterStyle Font-Bold="True" ForeColor="#0000ff"/>

            </asp:GridView>
                                

                            </div>
                            </div>
                            

                            
                                </div> 

                    <ul class="list-inline pull-right">
                        <li><%--<asp:button runat="server" class="btn btn-primary next-step">next</asp:button>--%>
                            
                            <asp:LinkButton ID="btnStep2" runat="server" ValidationGroup="btnStep2" CssClass="btn btn-primary btn-circle btn-lg previous-step" ToolTip="Previous">
                                <i class="glyphicon glyphicon-chevron-left"></i>
                            </asp:LinkButton>
                        </li>
                    </ul>
                </asp:View>
                <asp:View ID="vGrant21" runat="server">
                    
                        <ul class="list-inline pull-right">
                            <asp:LinkButton ID="LinkButton91" runat="server" CssClass="btn btn-default btn-circle btn-lg prev-step" ToolTip="Previous">
                                    <i class="glyphicon glyphicon-chevron-left"></i>
                                </asp:LinkButton>
                            <asp:LinkButton ID="LinkButton101" runat="server" ValidationGroup="btnStep2" CssClass="btn btn-primary btn-circle btn-lg next-step" ToolTip="Next">
                                    <i class="glyphicon glyphicon-send"></i>
                                </asp:LinkButton>
                        </ul>
                        
                            
                </asp:View>
             
            </asp:MultiView>
       
               </div> </div>
    </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
