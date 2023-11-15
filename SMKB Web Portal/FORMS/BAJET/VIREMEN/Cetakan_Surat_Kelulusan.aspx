<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Cetakan_Surat_Kelulusan.aspx.vb" Inherits="SMKB_Web_Portal.Cetakan_Surat_Kelulusan" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="container-fluid">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>
    <div class="row">

        <div class="col-sm-9 col-md-6">
        <div class="panel panel-default" style="width:60%">
        <div class="panel-body">
            <table style="width: 100%;">
                  <tr style="height:25px">
                      <td style="width: 116px"><label class="control-label" for="">No Viremen:</label></td>
                      <td style="width: 40%">
                        <asp:TextBox ID="txtNoviremen" runat="server" CssClass="form-control"  Width="80%"></asp:TextBox>
                      </td>
                      <td>
                        <asp:Button ID="btnCari" runat="server" Text="Cari" ValidationGroup="btnCari" OnClick = "OnConfirm" CssClass="btn" />
                      </td>
                       <td style="width: 10%">
                           <asp:HiddenField ID="HidNoViremen" runat="server" />
                        <%--<asp:TextBox ID="txtNoViremenFull" runat="server" CssClass="form-control"  Width="80%"></asp:TextBox>--%>
                      </td>
                  </tr>
            </table>
         </div>
         </div>
         </div>

        <div class="col-sm-3 col-md-6">
            
        <asp:GridView ID="gvKelulusanViremen" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="true" PageSize="15"
        CssClass= "table table-striped table-bordered table-hover" Width="60%"  Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" ShowFooter="True" >
          <columns>

            <asp:BoundField HeaderText="Bil" DataField="Bil" SortExpression="Bil" >
            <ItemStyle Width="5%" />
            </asp:BoundField>

            <asp:BoundField HeaderText="Tarikh" DataField="Tarikh" SortExpression="Tarikh" dataformatstring="{0:MM/dd/yyyy}" htmlencode="false" >
            <ItemStyle Width="15%" />
            </asp:BoundField>

            <asp:BoundField HeaderText="No Viremen" DataField="NoViremen" SortExpression="NoViremen" >
            <ItemStyle Width="15%" />
            </asp:BoundField>

            <asp:BoundField HeaderText="Rujukan Surat" DataField="RujukanSurat" SortExpression="RujukanSurat" >
            <ItemStyle Width="30%" />
            </asp:BoundField>

          </columns>
           
          <HeaderStyle BackColor="#6699FF" />

          <RowStyle Height="5px" />

          <SelectedRowStyle  ForeColor="Blue" />

    </asp:GridView>
          <asp:Button ID="btnPapar" runat="server" Text="Papar" ValidationGroup="btnPapar" CssClass="btn" />

    </div>



    </div>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel> 
</asp:Content>

