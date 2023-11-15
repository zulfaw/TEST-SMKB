<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Ralat.aspx.vb" Inherits="SMKB_Web_Portal.Ralat" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
  <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>
    <link rel="stylesheet" type="text/css" href="../../../Content/Site.css">
                <div class="col-sm-9 col-md-6">
      <p></p>         
                     <div class="row">

                          <div class="panel panel-default" style="width:50%">
    <div class="panel-body">
        <table class="nav-justified">
              <tr style="height:25px">
                  <td style="width: 116px"><asp:RadioButton GroupName="grpRalat" runat="server" ID="rbTarikh" Text=" Tarikh :" OnCheckedChanged="grpRalat_CheckedChanged" AutoPostBack="true" Checked ="true"  />
                     
                  <td>
                      <asp:TextBox ID="txtDateFrom" runat="server" CssClass="form-control" Text="<%# System.DateTime.Now%>" Width="100px"></asp:TextBox>
                         <cc1:CalendarExtender ID="calDateFrom" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtDateFrom" TodaysDateFormat="dd/MM/yyyy" />  
                      &nbsp; hingga &nbsp;
                      <asp:TextBox ID="txtDateTo" runat="server" CssClass="form-control" Text="<%# System.DateTime.Now%>" Width="100px"></asp:TextBox>
                      <cc1:CalendarExtender ID="calDateTo" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtDateTo" TodaysDateFormat="dd/MM/yyyy" />
                     </td>             
              </tr>

            <tr style="height:25px">
                  <td style="width: 116px"><asp:RadioButton GroupName="grpRalat" runat="server" ID="rbBulan" Text=" Bulan :" OnCheckedChanged="grpRalat_CheckedChanged" AutoPostBack="true" /></td>
                  <td>
                         <asp:DropDownList ID="ddlBulan" runat="server" AutoPostBack="True" CssClass="form-control" style="width: 150px;">
                         </asp:DropDownList>
                      &nbsp;<label class="control-label" for="">/</label>
                      &nbsp; <asp:Label ID="lblYear" runat="server"></asp:Label>
                     </td>             
              </tr>

            
            <tr style="height:55px">
                  <td style="width: 116px"></td>
                  <td>
                        <asp:Button runat="server"  Text="Cari"  CssClass="btn" ID="btnSearch" OnClick = "OnSearch" ValidationGroup="btnSearch" ></asp:Button>
                     </td>             
              </tr>
          </table>
        </div>
              </div>

      <asp:GridView ID="gvRalat" runat="server" DataKeyNames = "IdRalat" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True" PageSize="20"
        CssClass= "table table-striped table-bordered table-hover" Width="80%"  Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" ShowFooter="True">
        <columns>
            <asp:BoundField DataField="Bil" HeaderText="Bil" SortExpression="Bil" ReadOnly="True">
            <ItemStyle Width="2%" />
            </asp:BoundField>
            <asp:BoundField DataField="IdRalat" HeaderText="ID Ralat" SortExpression="IdRalat" Visible="False" />
<asp:BoundField HeaderText="No. Ralat" DataField="NoRalat" SortExpression="NoRalat" >
            </asp:BoundField>

<asp:BoundField HeaderText="Keterangan Ralat" DataField="DescRalat" SortExpression="DescRalat" >
            </asp:BoundField>
            <asp:BoundField HeaderText="Tarikh" DataField="Tarikh"  SortExpression="Tarikh" >
            </asp:BoundField>
            <asp:BoundField HeaderText="Masa" DataField="Masa" SortExpression="Masa" >
            </asp:BoundField>
            <asp:BoundField DataField="NamaForm" HeaderText="Nama Form" SortExpression="NamaForm" />
            <asp:BoundField DataField="Pengguna" HeaderText="Pengguna" SortExpression="Pengguna" />
            <asp:TemplateField HeaderText="Maklumat Terperinci">
            
            <%--<InsertItemTemplate>                
                <asp:LinkButton runat="server" ID="lnkInsert" CommandName="Insert" Text="Simpan" ToolTip="Sna" />
                <asp:LinkButton runat="server" ID="lnkCancel" CommandName="Cancel" Text="Batal" ToolTip="Fu" />
            </InsertItemTemplate>--%>

             <EditItemTemplate>              
            <%-- <asp:LinkButton runat="server" ID="lnkUpdate" CommandName="Update" Text="Simpan" ToolTip="Simpan" />
             <asp:LinkButton runat="server" ID="lnkCancel" CommandName="Cancel" Text="Batal" ToolTip="Batal" />--%>

                 <asp:ImageButton ID="btnSave1" runat="server" CommandName="Update" Height="25px" ImageUrl="~/Images/Save_48x48.png" 
                        ToolTip="Simpan" Width="20px" OnItemCommand="gvPeruntukan_ItemCommand" />
                    &nbsp;&nbsp;
                 <asp:ImageButton ID="btnCancel" runat="server" CommandName="Cancel" Height="25px" ImageUrl="~/Images/Cancel16x16.png" 
                        ToolTip="Batal" Width="20px" OnItemCommand="gvPeruntukan_ItemCommand" />
                    &nbsp;&nbsp;


             </EditItemTemplate>
            
                <ItemTemplate>
                    <asp:ImageButton ID="btnDetail" runat="server" CommandName="Select"  ImageUrl="~/Images/Details32x32.png" 
                        ToolTip="Maklumat Terperinci" Width="15px" Height="18px" OnItemCommand="" />
                    &nbsp;&nbsp;
                   <%-- <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete"  ImageUrl="~/Images/Delete_32x32.png" 
                        ToolTip="Padam" Width="15px" Height="18px" OnClientClick="return confirm('Anda pasti untuk padam Tahap Pengguna untuk rekod ini?');" />--%>
                </ItemTemplate>              
                
                <ItemStyle Width="120px" />
            </asp:TemplateField>
</columns>
          <HeaderStyle BackColor="#6699FF" />
          <RowStyle Height="5px" />
          <SelectedRowStyle  ForeColor="#0000FF" />
    </asp:GridView>                       
                         </div>
    </div>

                <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup" PopupControlID="pnlpopup"
                CancelControlID="btnCancel" BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="269px" Width="650px" Style="display: none">
                <table width="100%" style="border: Solid 3px #D5AA00; width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                    <tr style="background-color: #D5AA00">
                        <td colspan="3" style="height: 10%; color: White; font-weight: bold; font-size: larger" align="center">
                            <asp:Image ID="Image1" runat="server" Height="20px" Width ="18px" ImageUrl="~/Images/Details32x32.png" /> &nbsp;Maklumat Terperinci</td>
                    </tr>
                    <tr>
                        <td style="width: 2%">&nbsp;</td>
                        <td align="left" style="width: 15%;"> &nbsp;</td>
                        <td>&nbsp; </td>
                    </tr>
                    <tr>
                        <td ></td>
                        <td align="left" style="font-weight :bold">No. Ralat:  
                        </td>
                        <td>
                            &nbsp;<asp:Label ID="lblNoRalat" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td align="left" style="font-weight:bold">Keterangan:  
                        </td>
                        <td>
                            &nbsp;<asp:Label ID="lblKeterangan" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td align="left" style="font-weight:bold">Form:  
                        </td>
                        <td>
                           &nbsp; <asp:Label ID="lblForm" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td align="left" style="font-weight:bold">Event:  
                        </td>
                        <td>
                           &nbsp;<asp:Label ID="lblEvent" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td align="left" style="font-weight:bold">Id Pengguna:  </td>
                        <td>
                           &nbsp;<asp:Label ID="lblIdPengguna" runat="server"></asp:Label>
                            
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td align="left" style="font-weight:bold">Alamat IP:  </td>
                        <td>
                           &nbsp;<asp:Label ID="lblAlamatIP" runat="server"></asp:Label>
                            
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td align="left" style="font-weight:bold">Nama PC:</td>
                        <td>
                           &nbsp;<asp:Label ID="lblNamaPC" runat="server"></asp:Label>
                            
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td>
                            <asp:Button ID="btnCancel" runat="server" Text="Tutup" CssClass="btn" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>

                </ContentTemplate>
        </asp:UpdatePanel>
    

</asp:Content>
