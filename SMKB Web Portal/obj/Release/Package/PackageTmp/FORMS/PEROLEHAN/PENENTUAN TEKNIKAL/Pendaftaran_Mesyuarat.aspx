<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Pendaftaran_Mesyuarat.aspx.vb" Inherits="SMKB_Web_Portal.Pendaftaran_Mesyuarat" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">        
    <ContentTemplate>
        <div class="container-fluid">
        <div class="col-sm-9 col-md-6 col-lg-8">
        <p></p>
        <div class="row" style="width :100%;">  
            <div class="panel panel-default">
                 <div class="panel-body">
                 <h4>Maklumat Pendaftaran</h4>
                 <br />
                    <asp:GridView ID="gvMaklumatPendaftaran" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="False" AllowPaging="false" emptydatatext=" "
			         cssclass="table table-striped table-bordered table-hover" Width="100%" HeaderStyle-BackColor="#6699FF" Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" ShowFooter="false">
			            <columns>
					            <asp:BoundField DataField="SH/TD" HeaderText="No SH/TD" SortExpression="SH/TD" ReadOnly="True">
						            <ItemStyle Width="10%" />
					            </asp:BoundField>
					            <asp:BoundField HeaderText="Nama Projek" DataField="NamaProjek" SortExpression="NamaProjek" ReadOnly="true">
						            <ItemStyle Width="10%" />
					            </asp:BoundField>
                                <asp:BoundField HeaderText="Kategori Perolehan" DataField="KategoriPerolehan" SortExpression="KategoriPerolehan" ReadOnly="true">
						            <ItemStyle Width="10%" />
					            </asp:BoundField>
                                <asp:BoundField HeaderText="PTJ" DataField="PTJ" SortExpression="PTJ" ReadOnly="true">
						            <ItemStyle Width="40%" />
					            </asp:BoundField>
                                <asp:BoundField HeaderText="Status" DataField="Status" SortExpression="Status" ReadOnly="true">
						            <ItemStyle Width="10%" />
					            </asp:BoundField>
					            <asp:BoundField HeaderText="Tarikh Mesyuarat" DataField="TarikhMesyuarat" SortExpression="TarikhMesyuarat" ReadOnly="true">
						            <ItemStyle Width="40%" />
					            </asp:BoundField>
                                 <asp:BoundField HeaderText="Masa Mesyuarat" DataField="MasaMesyuarat" SortExpression="MasaMesyuarat" ReadOnly="true">
						            <ItemStyle Width="40%" />
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
            <div class="panel panel-default">
                <div class="panel-body">
                    <table class="nav-justified" style="width:100%;">
                      <tr style="height:25px">
                          <td style="width: 180px"><label class="control-label" for="">ID Mesyuarat :</label></td>
                          <td style="width: 80%">
                              <asp:TextBox ID="txtIDMesyuarat" runat="server" CssClass="form-control"  Width="101px"></asp:TextBox>
                          </td>
                      </tr>
                      <tr style="height:25px">
                          <td style="width: 180px"><label class="control-label" for="">Tarikh Mesyuarat :</label></td>
                          <td style="width: 80%">
                              <asp:TextBox ID="txtTarikhMesyuarat" runat="server" CssClass="form-control" Text="<%# System.DateTime.Now%>" Width="101px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="calStartDate" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtTarikhMesyuarat" TodaysDateFormat="dd/MM/yyyy"/>
                          </td>
                      </tr>
                    <tr style="height:25px">
                          <td style="width: 180px"><label class="control-label" for="">Masa Mesyuarat :</label></td>
                          <td style="width: 80%">
                              <asp:TextBox ID="txtMasaMesyuarat" runat="server" CssClass="form-control"  Width="101px"></asp:TextBox>
                          </td>
                      </tr>
                   
                  </table>
                </div>
            
                <div class="panel-body">
                <h4>Senarai AJK</h4>
                <br />
                    <asp:GridView ID="gvSenaraiAJK" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="False" AllowPaging="false" emptydatatext=" "
			     cssclass="table table-striped table-bordered table-hover" Width="100%" HeaderStyle-BackColor="#6699FF" Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" ShowFooter="false">
			        <columns>
					        <asp:BoundField DataField="Nama" HeaderText="Nama AJK" SortExpression="Nama" ReadOnly="True">
						        <ItemStyle Width="10%" />
					        </asp:BoundField>
                            <asp:BoundField DataField="Jawatan" HeaderText="Jawatan" SortExpression="Jawatan" ReadOnly="True">
						        <ItemStyle Width="10%" />
					        </asp:BoundField>
                            <asp:BoundField HeaderText="PTJ" DataField="PTJ" SortExpression="PTJ" ReadOnly="true">
						        <ItemStyle Width="40%" />
					        </asp:BoundField>
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
