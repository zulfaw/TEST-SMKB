<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Cetakan_PT.aspx.vb" Inherits="SMKB_Web_Portal.Cetakan_PT" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
    .calendarContainerOverride table
{
    width:0px;
    height:0px;
}

.calendarContainerOverride table tr td
{
    padding:0;
    margin:0;
}        

        </style>

    <h1>Cetakkan PT</h1>
    <p></p>

   <div class="row"> 
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="panel panel-default" style="width:auto;overflow-x:scroll;">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            Maklumat Perolehan
                        </h3>
                    </div>
                    <div class="panel-body">
                    <table style="width:600px"  class="table table-borderless table-striped">
                     <tr>
                         <td colspan="3">
                         <asp:RadioButtonList ID="rbPilih" runat="server" Height="25px" Width="400px" RepeatDirection="Horizontal" AutoPostBack="true" ValidationGroup="lbtnHantar">
							<asp:ListItem Text=" <b>PT</b>" Selected="True"/>
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
                         <td rowspan="2">
                             <asp:LinkButton ID="lbtnCari" runat="server" CssClass="btn btn-info" ToolTip="Cari Maklumat Permohonan Pembelian">
									<i class="fas fa-search fa-lg">&nbsp; Cari</i>
								</asp:LinkButton>
                         </td>
                     </tr>    
                    
                    <tr class="calendarContainerOverride">
                        <td>Pembekal :</td>
                        <td>                           
                            <asp:CheckBox runat="server" ID="chxSemuaPembekal" Text=" Keseluruhan" />
                            &nbsp;&nbsp;&nbsp;
                            Bulan/Tahun :
                            &nbsp;<asp:TextBox ID="txtBulanTahun" runat="server" CssClass="form-control rightAlign" Width="70px"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="caltxtBulanTahun" runat="server" DefaultView="Months" FirstDayOfWeek="Monday" Format="MM/yyyy" TargetControlID="txtBulanTahun" TodaysDateFormat="MM/yyyy"/>
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
                        <asp:GridView ID="gvCetakPT" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="true" EmptyDataText=" Tiada rekod"
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
                                <asp:BoundField HeaderText="No PT" DataField="PO19_NoPt" SortExpression="PO19_NoPt" HeaderStyle-CssClass="centerAlign">
									<HeaderStyle CssClass="centerAlign" />
									<ItemStyle Width="5%" HorizontalAlign="Center"/>
								</asp:BoundField>
                                <asp:BoundField HeaderText="Tarikh PT" DataField="PO19_TkhPt" SortExpression="PO19_TkhPt" HeaderStyle-CssClass="centerAlign" dataformatstring="{0:MM/dd/yyyy}">
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
										<asp:LinkButton ID="lbDetail" runat="server" CommandName="Select" CssClass="btn-xs" ToolTip="Cetak">
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
       </div>
        <%--<div class="row">
            <div class="panel panel-default" style="width:auto;overflow-x:scroll;" >
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            Laporan
                        </h3>
                    </div>
                    <div class="panel-body">
            <%--<iframe runat="server" scrolling="auto" id="frame1" name="frame1" frameborder="0" style="width:98%; height:800px; display:inline"></iframe>--%>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server"  Width="100%" Height="100%" Font-Names="Verdana" Font-Size="8pt" ProcessingMode="Remote" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" SizeToReportContent="True" BackColor="#D2D2D2" ShowBackButton="False" ShowRefreshButton="False" >
                
            </rsweb:ReportViewer>
                </div>
                </div>
        </div>--%> 
        
</asp:Content>
