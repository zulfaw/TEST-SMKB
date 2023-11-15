<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Laporan_Perolehan.aspx.vb" Inherits="SMKB_Web_Portal.Laporan_Perolehan" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>
    <p></p>

   <div class="row"> 
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
                    <table  class="table table-borderless table-striped">
                    <tr>
                        <td> Jenis Dokumen Perolehan :</td>
                         <td colspan="2">
                            <asp:DropDownList ID="ddlJenisDok" runat="server" AutoPostBack="True" CssClass="form-control" Width="80%"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlJenisDok" runat="server" ControlToValidate="ddlJenisDok" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="lbtnCari" Display="Dynamic" ></asp:RequiredFieldValidator>
                        </td>
                     </tr>
                        <tr>
                            <td>Jenis Laporan :</td>
                            <td>
                                <asp:DropDownList ID="ddlJenisLaporan" runat="server" AutoPostBack="True" CssClass="form-control" Width="80%"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlJenisLaporan" runat="server" ControlToValidate="ddlJenisLaporan" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="lbtnCari" Display="Dynamic" ></asp:RequiredFieldValidator>
                            </td>
                            <td rowspan="7">
                             <asp:LinkButton ID="lbtnPapar" runat="server" CssClass="btn btn-info" ToolTip="Paparkan laporan" ValidationGroup="lbtnCari">
								<i class="fas fa-print fa-lg">&nbsp; Papar</i>
							</asp:LinkButton>
                         </td>
                        </tr>
                    <tr>
                        <td>Kategori :</td>
                        <td><asp:CheckBoxList id="ChxKategoriPO" CellPadding="5" CellSpacing="5" RepeatDirection="Horizontal"
                                RepeatLayout="Flow" TextAlign="Right" runat="server">
                                <asp:ListItem Value="B" Selected="True">&nbsp;Bekalan </asp:ListItem>
                                <asp:ListItem Value="P" Selected="True">&nbsp;Perkhidmatan </asp:ListItem>
                                <asp:ListItem Value="K" Selected="True">&nbsp;Kerja </asp:ListItem>
                            </asp:CheckBoxList></td>
                    </tr>
                    <tr>
                        <td style="vertical-align:top">Kaedah :</td>
                        <td><asp:CheckBoxList id="ChxKaedahPO" CellPadding="5" CellSpacing="5" RepeatDirection="Vertical"
                                RepeatLayout="Flow" TextAlign="Right" runat="server">
                                <asp:ListItem Value="P05" Selected="True">&nbsp;Pesanan Belian </asp:ListItem>
                                <asp:ListItem Value="P01">&nbsp;Pembelian Terus </asp:ListItem>
                                <asp:ListItem Value="P04">&nbsp;Sebut Harga PTJ </asp:ListItem>
                                <asp:ListItem Value="P03">&nbsp;Sebut Harga Universiti </asp:ListItem>
                                <asp:ListItem Value="P02">&nbsp;Tender </asp:ListItem>
                            </asp:CheckBoxList></td>
                    </tr>
                    <tr>
                            <td>Tarikh : <%--<asp:CheckBox id="chxTarikh" runat="server" Text=" Tarikh :" />--%>
                                </td>
                            <td>
                                <asp:TextBox ID="txtTkhDari" runat="server" CssClass="form-control" Text="<%# System.DateTime.Now%>" Width="125px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtTkhDari" runat="server" ControlToValidate="txtTkhDari" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="lbtnCari" Display="Dynamic"></asp:RequiredFieldValidator>
                                <ajaxtoolkit:CalendarExtender ID="calTkhInvoisDr" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtTkhDari" TodaysDateFormat="dd/MM/yyyy" PopupButtonID="lnkBtnIcon1" />
                                <asp:LinkButton ID="lnkBtnIcon1" runat="server" ToolTip="Klik untuk papar kalendar">
                                    <i class="far fa-calendar-alt fa-lg"></i>
                                </asp:LinkButton>
                                &nbsp; &nbsp; Hingga :&nbsp;&nbsp;
                                <asp:TextBox ID="txtTkhHingga" runat="server" CssClass="form-control" Text="<%# System.DateTime.Now%>" Width="125px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtTkhHingga" runat="server" ControlToValidate="txtTkhHingga" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="lbtnCari" Display="Dynamic"></asp:RequiredFieldValidator>
                                <ajaxtoolkit:CalendarExtender ID="calTkhInvoisHingga" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtTkhHingga" TodaysDateFormat="dd/MM/yyyy" PopupButtonID="lnkBtnIcon2" />
                                <asp:LinkButton ID="lnkBtnIcon2" runat="server" ToolTip="Klik untuk papar kalendar">
                                    <i class="far fa-calendar-alt fa-lg"></i>
                                </asp:LinkButton>
                            </td>
                        </tr>
                    <tr>
                        <td style="vertical-align:top"><asp:CheckBox id="chxJenisPembayaran" runat="server" Text=" Jenis Pembayaran :" AutoPostBack="true" />
                            </td>
                        <td>
                            <asp:RadioButtonList ID="rbPilihBayaran" runat="server" Height="100px" Width="200px" RepeatDirection="Vertical" AutoPostBack="true" ValidationGroup="lbtnHantar" Enabled="false">
							<asp:ListItem Text=" Belum bayar" Value=1/>
							<asp:ListItem Text=" Sudah bayar" Value=2/>
                            <asp:ListItem Text=" Bayaran Ikut Julat" Value=3/>
                            <asp:ListItem Text=" Keseluruhan" Value=4/>
						</asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="rfvrbPilihBayaran" runat="server" ControlToValidate="rbPilihBayaran" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="lbtnCari" Display="Dynamic" Enabled="false" ></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                     <%--<tr>
                         <td><asp:CheckBox id="chxTahun" runat="server" Text=" Tahun :" />
                             </td>
                         <td><asp:TextBox ID="txtTahun" runat="server" CssClass="form-control" TextMode="Number" Width="80px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtTahun" runat="server" ControlToValidate="txtTahun" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="lbtnCari" Display="Dynamic" Enabled="false" ></asp:RequiredFieldValidator></td>
                     </tr>--%>
                        
                    <%--<tr>
                        <td><asp:CheckBox id="chxKw" runat="server" Text=" KW :" />
                            </td>
                        <td>
                            <asp:DropDownList ID="ddlKW" runat="server" AutoPostBack="True" CssClass="form-control" Width="80%"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlKW" runat="server" ControlToValidate="ddlKW" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="lbtnCari" Display="Dynamic" Enabled="false"></asp:RequiredFieldValidator>
                        </td>
                         
                     </tr>--%>
                     <tr>
                        <td><asp:CheckBox id="chxPTJ" runat="server" Text=" PTJ :" AutoPostBack="true" />
                            </td>
                        <td>
                            <asp:DropDownList ID="ddlPTJ" runat="server" AutoPostBack="True" CssClass="form-control" Width="80%" Enabled="false"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlPTJ" runat="server" ControlToValidate="ddlPTJ" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="lbtnCari" Display="Dynamic" Enabled="false"></asp:RequiredFieldValidator>
                        </td>
                     </tr>
                     <%--<tr>
                        <td><asp:CheckBox id="chxVot" runat="server" Text=" Vot :" /></td>
                        <td>                           
                            <asp:DropDownList ID="ddlvot" runat="server" AutoPostBack="True" CssClass="form-control" Width="80%"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlvot" runat="server" ControlToValidate="ddlvot" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="lbtnCari" Display="Dynamic" Enabled="false"></asp:RequiredFieldValidator>
                        </td>                        
                    </tr>--%>
                    <tr>
                        <td><asp:CheckBox id="chxNamaSyarikat" runat="server" Text=" Nama Syarikat :" AutoPostBack="true" /></td>
                        <td>                           
                            <asp:DropDownList ID="ddlSyarikat" runat="server" AutoPostBack="True" CssClass="form-control" Width="80%" Enabled="false"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlSyarikat" runat="server" ControlToValidate="ddlSyarikat" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="lbtnCari" Display="Dynamic" Enabled="false"></asp:RequiredFieldValidator>
                        </td>                        
                    </tr>                        
                     </table>
           
            </ContentTemplate>
    </asp:UpdatePanel>
       </div>
        <div class="row">
            <div class="panel panel-default" style="width:auto;overflow-x:scroll;" >
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            Laporan Perolehan
                        </h3>
                    </div>
                    <div class="panel-body">
            <%--<iframe runat="server" scrolling="auto" id="frame1" name="frame1" frameborder="0" style="width:98%; height:800px; display:inline"></iframe>--%>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server"  Width="100%" Height="100%" 
                Font-Names="Verdana" Font-Size="8pt" ProcessingMode="Remote" WaitMessageFont-Names="Verdana" 
                WaitMessageFont-Size="14pt" SizeToReportContent="True" BackColor="#D2D2D2" ShowBackButton="False" 
                ShowRefreshButton="False" >
                
            </rsweb:ReportViewer>
                </div>
                </div>
        </div> 
</asp:Content>
