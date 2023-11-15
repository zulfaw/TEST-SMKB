<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Cetak_SST.aspx.vb" Inherits="SMKB_Web_Portal.Cetak_SST" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<h1>Cetakkan SST Kerja</h1>
    <p></p>

   
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="panel panel-default" style="width:auto">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            Maklumat Perolehan
                        </h3>
                    </div>
                    <div class="panel-body">
                    <table style="width:400px"  class="table table-borderless table-striped">
                     <tr>
                         <td colspan="2">
                         <asp:RadioButtonList ID="rbPilih" runat="server" Height="25px" Width="400px" RepeatDirection="Horizontal" AutoPostBack="true" ValidationGroup="lbtnHantar">
							<asp:ListItem Text=" <b>Inden</b>" Selected="True"/>
							<asp:ListItem Text=" <b>Pembekal</b>" />
						</asp:RadioButtonList>
                        </td>
                     </tr>
                     <tr>
                        <td>PT :</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chxSemuaPT" Text=" Keseluruhan" />
                            &nbsp;&nbsp;&nbsp;No PT :&nbsp;
                            <asp:TextBox ID="txtNoPT" runat="server" CssClass="form-control rightAlign" Width="120px"></asp:TextBox>
                        </td>
                     </tr>    
                    
                    <tr>
                        <td>Pembekal :</td>
                        <td>
                            <%--<asp:DropDownList ID="ddlPembekal" runat="server" AutoPostBack="True" CssClass="form-control" Width="120px">
                                <asp:ListItem Selected="True" Value="0">Keseluruhan</asp:ListItem>                                
                            </asp:DropDownList>--%>                            
                            <asp:CheckBox runat="server" ID="chxSemuaPembekal" Text=" Keseluruhan" />
                            &nbsp;&nbsp;&nbsp;
                            Bulan/Tahun :
                            &nbsp;<asp:TextBox ID="txtBulanTahun" runat="server" CssClass="form-control rightAlign" Width="70px"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="caltxtBulanTahun" runat="server" DefaultView="Months" FirstDayOfWeek="Monday" Format="MM/yyyy" TargetControlID="txtBulanTahun" TodaysDateFormat="MM/yyyy"/>
                        </td>
                    </tr>
                        <tr>
                            <td colspan="2" style="text-align:center">
                                <asp:LinkButton ID="lbtnCari" runat="server" CssClass="btn btn-info" ToolTip="Cari Maklumat Permohonan Pembelian">
									<i class="fas fa-search fa-lg">&nbsp; Cari</i>
								</asp:LinkButton>
                            </td>
                        </tr>
                     </table>
                        <div>
                <tr style="height:30px;">
                <td style="width:80px;">
                    <label class="control-label" for="Klasifikasi">Saiz Rekod : </label>
                </td>
                <td style="width:50px;">
                    <asp:DropDownList ID="ddlSaizRekod" runat="server" Width="50px" AutoPostBack="true" CssClass="form-control">                        
                        <asp:ListItem Text="10" Value="10" />
                        <asp:ListItem Text="25" Value="25" Selected="true" />
                        <asp:ListItem Text="50" Value="50" />
                        <asp:ListItem Text="100" Value="100" />
                    </asp:DropDownList>
                </td>
                </tr>
            </div>
                        <asp:GridView ID="gvCetakInden" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="true" EmptyDataText=" Tiada rekod"
							cssclass="table table-striped table-bordered table-hover" Width="100%" BorderStyle="Solid" PageSize="25" Font-Size="8pt">
								<columns>                                 
								<asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" >
									<ItemTemplate>
										<%# Container.DataItemIndex + 1 %>
									</ItemTemplate>
								    <HeaderStyle CssClass="centerAlign" />
                                    <ItemStyle Width="2%" />
								</asp:TemplateField> 
								<asp:BoundField HeaderText="No Perolehan" DataField="PO01_NoMohon" SortExpression="PO01_NoMohon" HeaderStyle-CssClass="centerAlign">
									<HeaderStyle CssClass="centerAlign" />
									<ItemStyle Width="5%" HorizontalAlign="Center"/>
								</asp:BoundField>
                                <asp:BoundField HeaderText="No SST" DataField="PO19_NoPt" SortExpression="PO19_NoPt" HeaderStyle-CssClass="centerAlign">
									<HeaderStyle CssClass="centerAlign" />
									<ItemStyle Width="5%" HorizontalAlign="Center"/>
								</asp:BoundField>
                                <asp:BoundField HeaderText="Tarikh SST" DataField="PO19_TkhPt" SortExpression="PO19_TkhPt" HeaderStyle-CssClass="centerAlign" dataformatstring="{0:MM/dd/yyyy}">
                                    <HeaderStyle CssClass="centerAlign" />
                                    <ItemStyle Width="5%" HorizontalAlign="Center"/>
                                </asp:BoundField>							            
								<asp:BoundField HeaderText="Tujuan" DataField="PO01_Tujuan" SortExpression="PO01_Tujuan" HeaderStyle-CssClass="centerAlign">
									<HeaderStyle CssClass="centerAlign" />
									<ItemStyle Width="25%" />
								</asp:BoundField>
								<asp:BoundField HeaderText="Nama Pembekal" DataField="ROC01_NamaSya" SortExpression="ROC01_NamaSya" HeaderStyle-CssClass="centerAlign">
									<HeaderStyle CssClass="centerAlign" />
									<ItemStyle Width="25%" />
								</asp:BoundField>                                
                                <asp:BoundField HeaderText="Jumlah Perbelanjaan (RM)" DataField="PO19_JumSebenar" SortExpression="PO19_JumSebenar" HeaderStyle-CssClass="centerAlign" DataFormatString="{0:N}">
                                    <HeaderStyle CssClass="centerAlign" />
                                    <ItemStyle Width="8%" HorizontalAlign="Right"/>                       
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Telah dicetak" SortExpression="PO19_Cetak" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">                                    
                                    <ItemTemplate><%#IIf(IsDBNull(Eval("PO19_Cetak")) OrElse CInt(Eval("PO19_Cetak") = 0), "Tidak", "Ya")%></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="3%" />
                                </asp:TemplateField>								                                                                                            
								<asp:TemplateField>                        
								<ItemTemplate>
										<asp:LinkButton ID="lbDetail" runat="server" CommandName="Select" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="btn-xs btn-primary" ToolTip="Maklumat Lanjut">
											<i class="fa fa-print fa-lg"></i>
										</asp:LinkButton>
									</ItemTemplate>
									<ItemStyle Width="3%" HorizontalAlign="Center"/>
								</asp:TemplateField>
							</columns>
						        <PagerSettings FirstPageText="First" LastPageText="Last" />
                            <%--<PagerStyle CssClass="pagination-ys" />--%>
						</asp:GridView>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


