<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Laporan_Terimaan.aspx.vb" Inherits="SMKB_Web_Portal.Laporan_Terimaan" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>
    <p></p>

   <div class="row"> 
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

                    <table  class="table table-borderless table-striped">
                        <tr>
                        <td> Jenis Terimaan :</td>
                         <td colspan="2">
                         <%--<asp:RadioButtonList ID="rbPilih" runat="server" Height="25px" Width="400px" RepeatDirection="Horizontal" AutoPostBack="true" ValidationGroup="lbtnHantar">
							<asp:ListItem Text=" <b>Kewangan</b>" Selected="True"/>
							<asp:ListItem Text=" <b>Pelajar</b>" />
						</asp:RadioButtonList>--%>
                             <asp:CheckBoxList id="chxTerimaan" CellPadding="5" CellSpacing="5" RepeatDirection="Horizontal"
                                RepeatLayout="Flow" TextAlign="Right" runat="server">
                                <asp:ListItem Value="TK" Selected="True">&nbsp;Kewangan </asp:ListItem>
                                <asp:ListItem Value="TP">&nbsp;Pelajar </asp:ListItem>
                                <asp:ListItem Value="TB">&nbsp;Biasiswa </asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                     </tr>
                        <tr>
                            <td>Jenis Laporan</td>
                            <td>
                                <asp:DropDownList ID="ddlJenisLaporan" runat="server" AutoPostBack="True" CssClass="form-control" Width="80%"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlJenisLaporan" runat="server" ControlToValidate="ddlJenisLaporan" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="lbtnCari" Display="Dynamic" ></asp:RequiredFieldValidator>
                            </td>
                            <td rowspan="4">
                             <asp:LinkButton ID="lbtnPapar" runat="server" CssClass="btn btn-info" ToolTip="Paparkan laporan" ValidationGroup="lbtnCari">
									<i class="fas fa-print fa-lg">&nbsp; Papar</i>
								</asp:LinkButton>
                         </td>
                        </tr>
                     
                        <tr>
                            <td>Tarikh :</td>
                            <td>
                                <asp:TextBox ID="txtTkhDari" runat="server" CssClass="form-control" Text="<%# System.DateTime.Now%>" Width="125px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtTkhDari" runat="server" ControlToValidate="txtTkhDari" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="lbtnCari" Display="Dynamic" ></asp:RequiredFieldValidator>
                                <ajaxtoolkit:CalendarExtender ID="calTkhInvoisDr" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtTkhDari" TodaysDateFormat="dd/MM/yyyy" PopupButtonID="lnkBtnIcon1" />
                                <asp:LinkButton ID="lnkBtnIcon1" runat="server" ToolTip="Klik untuk papar kalendar">
                                    <i class="far fa-calendar-alt fa-lg"></i>
                                </asp:LinkButton>
                                &nbsp; &nbsp; Hingga &nbsp;&nbsp;
                                <asp:TextBox ID="txtTkhHingga" runat="server" CssClass="form-control" Text="<%# System.DateTime.Now%>" Width="125px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtTkhHingga" runat="server" ControlToValidate="txtTkhHingga" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="lbtnCari" Display="Dynamic" ></asp:RequiredFieldValidator>
                                <ajaxtoolkit:CalendarExtender ID="calTkhInvoisHingga" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtTkhHingga" TodaysDateFormat="dd/MM/yyyy" PopupButtonID="lnkBtnIcon2" />
                                <asp:LinkButton ID="lnkBtnIcon2" runat="server" ToolTip="Klik untuk papar kalendar">
                                    <i class="far fa-calendar-alt fa-lg"></i>
                                </asp:LinkButton>
                            </td>
                        </tr>
                     <tr>
                        <td>Zon :</td>
                        <td>
                            <asp:DropDownList ID="ddlzon" runat="server" AutoPostBack="True" CssClass="form-control" Width="80%"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlzon" runat="server" ControlToValidate="ddlzon" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="lbtnCari" Display="Dynamic" ></asp:RequiredFieldValidator>
                        </td>
                         
                     </tr>    
                    
                    <tr>
                        <td>Vot :</td>
                        <td>                           
                            <asp:DropDownList ID="ddlvot" runat="server" AutoPostBack="True" CssClass="form-control" Width="80%"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlvot" runat="server" ControlToValidate="ddlvot" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="lbtnCari" Display="Dynamic" ></asp:RequiredFieldValidator>
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
                            Laporan Terimaan
                        </h3>
                    </div>
                    <div class="panel-body">
            <%--<iframe runat="server" scrolling="auto" id="frame1" name="frame1" frameborder="0" style="width:98%; height:800px; display:inline"></iframe>--%>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server"  Width="100%" Height="100%" Font-Names="Verdana" Font-Size="8pt" ProcessingMode="Remote" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" SizeToReportContent="True" BackColor="#D2D2D2" ShowBackButton="False" ShowRefreshButton="False" >
                
            </rsweb:ReportViewer>
                </div>
                </div>
        </div> 
        
</asp:Content>