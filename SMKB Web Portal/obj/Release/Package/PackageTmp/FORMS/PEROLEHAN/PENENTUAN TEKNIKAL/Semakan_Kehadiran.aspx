<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Semakan_Kehadiran.aspx.vb" Inherits="SMKB_Web_Portal.Semakan_Kehadiran" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">        
    <ContentTemplate>
    <div class="container-fluid">
    <div class="col-sm-9 col-md-6 col-lg-8">
        <p></p>
    <div class="row" style="width :100%;">
        <div class="panel-body">
            <asp:GridView ID="gvSenaraiMesyuarat" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="False" AllowPaging="false" emptydatatext="no data"
			cssclass="table table-striped table-bordered table-hover" Width="100%" HeaderStyle-BackColor="#6699FF" Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" ShowFooter="false">
			    <columns>
                    <asp:BoundField DataField="IDMesyuarat" HeaderText="ID Mesyuarat" SortExpression="IDMesyuarat" ReadOnly="True">
					<ItemStyle Width="20%" />
					</asp:BoundField>
                    <asp:BoundField DataField="TarikhMesyuarat" HeaderText="Tarikh Mesyuarat" SortExpression="TarikhMesyuarat" ReadOnly="True">
					<ItemStyle Width="10%" />
					</asp:BoundField>
                    <asp:BoundField DataField="MasaMesyuarat" HeaderText="Masa Mesyuarat" SortExpression="MasaMesyuarat" ReadOnly="True">
					<ItemStyle Width="10%" />
					</asp:BoundField>
					<asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" ReadOnly="True">
					<ItemStyle Width="10%" />
					</asp:BoundField>
					            
		        </columns>
			    
            </asp:GridView>
        </div>
        <div class="panel panel-default">
            <div class="panel-body">
                <h4>Maklumat Kehadiran</h4>
                 <br />
                <asp:GridView ID="gvSenaraiAJK" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="False" AllowPaging="false" emptydatatext="no data"
			    cssclass="table table-striped table-bordered table-hover" Width="100%" HeaderStyle-BackColor="#6699FF" Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" ShowFooter="false">
			    <columns>
                    <asp:BoundField DataField="Nama" HeaderText="Nama" SortExpression="Nama" ReadOnly="True">
					<ItemStyle Width="20%" />
					</asp:BoundField>
                    <asp:BoundField DataField="Jawatan" HeaderText="Jawatan" SortExpression="Jawatan" ReadOnly="True">
					<ItemStyle Width="10%" />
					</asp:BoundField>
                    <asp:BoundField DataField="PTJ" HeaderText="PTJ" SortExpression="PTJ" ReadOnly="True">
					<ItemStyle Width="10%" />
					</asp:BoundField>
                                                       
					<asp:TemplateField HeaderText="Pilih" >
                        <ItemTemplate>
                            <asp:CheckBox ID="chkPilih" runat="server" Checked='<%# Convert.ToBoolean(Eval("Pilih")) %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="7%"/>
                    </asp:TemplateField>
					            
		        </columns>
			    
            </asp:GridView>
            </div>
        </div>
        <table class="nav-justified" style="width:100%;">
            <tr style="height:25px">
                <td >
                    <asp:Button ID="btnSimpan" runat="server" Text="Simpan" ValidationGroup="btnSimpan" CssClass="btn" />
                </td>
           </tr>
                                      
        </table>
    </div>
    </div>      
    </div>         
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
