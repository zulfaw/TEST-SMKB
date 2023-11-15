<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Pelarasan_PT.aspx.vb" Inherits="SMKB_Web_Portal.Pelarasan_PT" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">

    function pageLoad() {
        HideControl();
    }

    window.onload = pageLoad;

    function HideControl()
    {
        debugger;
        var ddlCari = document.getElementById('<%=ddlCari.ClientID%>');
        var trBoxPt = document.getElementById('trnopt')
        var trBoxAdjPt = document.getElementById('trnoadjpt')
        var trBoxTarikh = document.getElementById('trtarikh')
        var trBoxPembekal = document.getElementById('trpembekal')
            
            if (ddlCari.value == 0)
            {
                trBoxPt.style.display = 'block';
                trBoxAdjPt.style.display = 'none';
                trBoxTarikh.style.display = 'none';
                trBoxPembekal.style.display = 'none';
            }
            else if(ddlCari.value == 1)
            {
                trBoxPt.style.display = 'none';
                trBoxAdjPt.style.display = 'block';
                trBoxTarikh.style.display = 'none';
                trBoxPembekal.style.display = 'none';
            }
            else if (ddlCari.value == 2) {
                trBoxPt.style.display = 'none';
                trBoxAdjPt.style.display = 'none';
                trBoxTarikh.style.display = 'block';
                trBoxPembekal.style.display = 'none';
            }
            else if (ddlCari.value == 3) {
                trBoxPt.style.display = 'none';
                trBoxAdjPt.style.display = 'none';
                trBoxTarikh.style.display = 'none';
                trBoxPembekal.style.display = 'block';
            }
        } 
 </script>

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
<h1>Pelarasan Pesanan Tempatan PTJ</h1>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
            <p></p>
                <table style="width:auto;overflow:auto;"  class="table table-borderless table-striped" >
                     <tr>
                         <td colspan="3">
                         <asp:RadioButtonList ID="rbPilihJnsPT" runat="server" Height="25px" Width="400px" RepeatDirection="Horizontal" AutoPostBack="true" ValidationGroup="lbtnHantar">
							<asp:ListItem Text=" <b>PT</b>" Selected="True" Value="0"/>
							<asp:ListItem Text=" <b>PT Adj</b>" value="1"/>
						</asp:RadioButtonList>
                        </td>
                     </tr>
                    <tr>
                        <td>
                            Kaedah Carian :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCari" runat="server" Width="150px" class="form-control" onchange="HideControl();">
                                <asp:ListItem Text="No PT" Value=0 Selected="True" />
                                <asp:ListItem Text="No PT Adj" Value=1 />
                                <asp:ListItem Text="Tarikh" Value=2 />
                                <asp:ListItem Text="Pembekal" Value=3 />
                            </asp:DropDownList>
                        </td>                        
                        <td >
                             <asp:LinkButton ID="lbtnCari" runat="server" CssClass="btn btn-info" ToolTip="Cari Maklumat Permohonan Pembelian" >
									<i class="fas fa-search fa-lg">&nbsp; Cari</i>
								</asp:LinkButton>
                         </td>
                    </tr>
                    </table>
                <table style="width:auto;overflow:auto;"  class="table table-borderless table-striped" >                    
                     <tr id="trnopt">
                        <td>No PT :</td>
                        <td>
                            <asp:TextBox ID="txtNoPT" runat="server" CssClass="form-control rightAlign" Width="120px"></asp:TextBox>
                        </td>
                         
                     </tr>
                    <tr id="trnoadjpt">
                        <td>No Pelarasan PT :</td>
                        <td><asp:TextBox ID="txtNoAdjPT" runat="server" CssClass="form-control rightAlign" Width="120px"></asp:TextBox></td>
                    </tr>   
                    <tr id="trtarikh" class="calendarContainerOverride">
                        <td>
                            Tarikh :
                        </td>
                        <td>
                            &nbsp;Dari : &nbsp;
                            <asp:TextBox ID="txtTarikhMula" runat="server" CssClass="form-control rightAlign" Width="100px" ></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="caltxtTarikhMula" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtTarikhMula" TodaysDateFormat="dd/MM/yyyy" PopupButtonID="lbtntxtTarikhMula"/>
                            <asp:LinkButton ID="lbtntxtTarikhMula" runat="server" ToolTip="Klik untuk papar kalendar">
                                <i class="far fa-calendar-alt fa-lg"></i>
                            </asp:LinkButton>
                            &nbsp;&nbsp;Hingga : &nbsp;
                            <asp:TextBox ID="txtTarikhTamat" runat="server" CssClass="form-control rightAlign" Width="100px" ></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CaltxtTarikhTamat" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtTarikhTamat" TodaysDateFormat="dd/MM/yyyy" PopupButtonID="lbtntxtTarikhTamat"/>
                            <asp:LinkButton ID="lbtntxtTarikhTamat" runat="server" ToolTip="Klik untuk papar kalendar">
                                <i class="far fa-calendar-alt fa-lg"></i>
                            </asp:LinkButton>
                        </td>
                    </tr>
                    <tr id="trpembekal">
                        <td>Pembekal :</td>
                        <td>                           
                           <asp:DropDownList ID="ddlVendor" runat="server" AutoPostBack="True" CssClass="form-control" Width="100%"></asp:DropDownList>
                        </td>                        
                    </tr>                        
                     </table>
               <div class="panel panel-default">
				<div class="panel-heading">
					<h3 class="panel-title">
						Senarai Pesanan Tempatan 
					</h3>
				</div>
				<div class="panel-body" style="overflow-x:auto">
					<asp:GridView ID="gvPT" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="false" EmptyDataText=" Tiada Rekod"
							cssclass="table table-striped table-bordered table-hover" Width="100%" BorderStyle="Solid" Font-Size="8pt">
								<columns>
                                
								<asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" >
									<ItemTemplate>
										<%# Container.DataItemIndex + 1 %>
									</ItemTemplate>
								</asp:TemplateField> 
								<asp:BoundField HeaderText="No PT" DataField="PO19_NoPt" SortExpression="PO19_NoPt" HeaderStyle-CssClass="centerAlign" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center"/>									
                                <asp:BoundField HeaderText="Tarikh PT" DataField="PO19_TkhPT" SortExpression="PO19_TkhPT" HeaderStyle-CssClass="centerAlign" dataformatstring="{0:dd/MM/yyyy}" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="No PT Adj" DataField="PO19_NoPtAdj" SortExpression="PO19_NoPtAdj" HeaderStyle-CssClass="centerAlign" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField HeaderText="Tarikh PT Adj" DataField="PO19_TkhPtAdj" SortExpression="PO19_TkhPtAdj" HeaderStyle-CssClass="centerAlign" dataformatstring="{0:dd/MM/yyyy}" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" />    
								<%--<asp:BoundField HeaderText="Kategori" DataField="JenisBarang" SortExpression="Kategori" HeaderStyle-CssClass="centerAlign" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center"/>--%>
                                <asp:BoundField HeaderText="Kaedah" DataField="Butiran" SortExpression="Butiran" HeaderStyle-CssClass="centerAlign" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField HeaderText="Vendor" DataField="ROC01_NamaSya" SortExpression="ROC01_NamaSya" HeaderStyle-CssClass="centerAlign" ItemStyle-Width="30%"/>                                     
                                <%--<asp:BoundField HeaderText="Jumlah(RM)" DataField="PO02_JumSebenar" SortExpression="AnggaranBelanja" HeaderStyle-CssClass="centerAlign" DataFormatString="{0:N}" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" />--%>
								<%--<asp:BoundField HeaderText="Status" DataField="StatusDok" SortExpression="Status" HeaderStyle-CssClass="centerAlign" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />--%>                                                                                         
								<asp:TemplateField>                        
								<ItemTemplate>
										<asp:LinkButton ID="lbDetail" runat="server" CommandName="Select"  CssClass="btn-xs btn-primary" ToolTip="Maklumat Lanjut">
											<i class="fa fa-ellipsis-h fa-lg"></i>
										</asp:LinkButton>
									</ItemTemplate>
									<ItemStyle Width="3%" HorizontalAlign="Center"/>
								</asp:TemplateField>
							</columns>
						</asp:GridView>
				</div>
                </div>   

            </div>
            
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
