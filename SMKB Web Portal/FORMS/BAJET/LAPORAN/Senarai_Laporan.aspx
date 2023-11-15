<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Senarai_Laporan.aspx.vb" Inherits="SMKB_Web_Portal.Senarai_Laporan" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>

    <script type="text/javascript">
        //$(function () {
        //    $("[id*=gvLaporan] td").hover(function () {
        //        $("td", $(this).closest("tr")).addClass("hover_row");
        //    }, function () {
        //        $("td", $(this).closest("tr")).removeClass("hover_row");
        //    });
        //});

        function fClosePilihanCarian1() {
            $find("mpe1").hide();
        }
    </script>

    <%--<style type="text/css">
        body {
            font-family: Arial;
            font-size: 10pt;
        }

        td {
            cursor: pointer;
        }

        .hover_row {
            background-color: #A1DCF2;
        }
    </style>--%>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="panel panel-default" style="width: 600px;">
                <div class="panel-heading">
                    Senarai Laporan
                </div>
                <div class="panel-body">
                    <asp:GridView ID="gvLaporan" runat="server" AllowSorting="True" AutoGenerateColumns="False" 
                        BorderStyle="Solid" CssClass="table table-striped table-bordered table-hover" 
                        DataKeyNames="KodLaporan" ShowHeaderWhenEmpty="True" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Bil">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBil" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="NamaLaporan" HeaderText="Nama Laporan"/>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Tindakan">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnPilih" runat="server" CommandName="Select" CssClass="btn-xs" ToolTip="Pilih">
								<i class="fas fa-edit"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                </Columns>
                                <RowStyle Height="5px" />
                                <SelectedRowStyle ForeColor="Blue" />
                            </asp:GridView>
                </div>
            </div>


            
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Button ID="btnPilihanCarian1" runat="server" Text="" Style="display: none" />
    <ajaxToolkit:ModalPopupExtender ID="MPEPilihanCarian1" runat="server" BackgroundCssClass="modalBackground" BehaviorID="mpe1"
        CancelControlID="btnClosePilihanCarian1" PopupControlID="pnlPilihanCarian1" TargetControlID="btnPilihanCarian1">
    </ajaxToolkit:ModalPopupExtender>

    <asp:Panel ID="pnlPilihanCarian1" runat="server" BackColor="White" Width="1000px" Style="overflow: auto;">
        <asp:UpdatePanel runat="server" ID="UpdatePanel3">
            <ContentTemplate>
                <table style="border: Solid 3px #D5AA00;">
                    <tr style="background: linear-gradient(to bottom, #fceabb 0%,#fccd4d 50%,#f8b500 51%,#fbdf93 100%);">
                        <td colspan="2" style="height: 10%; font-weight: bold; text-align: center;">Pilihan Carian </td>
                        <td style="width: 10px; text-align: center;">
                            <button id="btnClosePilihanCarian1" runat="server" class="btnNone" title="Tutup" onclick="fClosePilihanCarian1();">
                                <i class="far fa-window-close fa-2x"></i>
                            </button>
                        </td>
                    </tr>                    
                    <tr style="vertical-align: top;">
                        <td colspan="3">
                            <div style="margin-bottom: 10px; margin-top: 10px; margin-left: 10px; margin-right: 10px;">
                                <table>
                                    <tr style="height:40px;">
                                        <td >&nbsp;Tarikh Dari :
                                            <br />
                                            <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" Width="100px"></asp:TextBox>
                                            <asp:LinkButton ID="lbtnStartDate" runat="server" ToolTip="Klik untuk papar kalendar">
                                                <i class="far fa-calendar-alt fa-lg"></i>
                                            </asp:LinkButton>
                                            <ajaxToolkit:CalendarExtender ID="calStartDate" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtStartDate" TodaysDateFormat="dd/MM/yyyy" PopupButtonID="lbtnStartDate"/>
                                            <asp:RequiredFieldValidator ID="rfvtxtStartDate" runat="server" ControlToValidate="txtStartDate" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="btnPaparCarian1" Display="Dynamic" />
                                        </td>

                                        <td>&nbsp;<asp:label ID="lbltkhHgg" runat="server" Text="Hingga :"></asp:label>
                                            <br />
                                            <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" Width="100px"></asp:TextBox>
                                            <asp:LinkButton ID="lbtnEndDate" runat="server" ToolTip="Klik untuk papar kalendar">
                                                <i class="far fa-calendar-alt fa-lg"></i>
                                            </asp:LinkButton>
                                            <ajaxToolkit:CalendarExtender ID="calEndDate" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtEndDate" TodaysDateFormat="dd/MM/yyyy" PopupButtonID="lbtnEndDate"/>
                                            <asp:RequiredFieldValidator ID="rfvtxtEndDate" runat="server" ControlToValidate="txtEndDate" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="btnPaparCarian1" Display="Dynamic" />
                                        </td>

                                    </tr>
                                    <tr style="height:40px;">
                                        <td>&nbsp;<asp:label ID="lblKWFrom" runat="server" Text="KW dari :"></asp:label>
                                            <br />
                                            <asp:DropDownList ID="ddlKwFrom" runat="server" CssClass="form-control" Width="100%"/>
                                            <asp:RequiredFieldValidator ID="rfvddlKWFrom" runat="server" ControlToValidate="ddlKwFrom" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="btnPaparCarian1" Display="Dynamic" />
                                        </td>

                                        <td>&nbsp;<asp:label ID="lblKWTo" runat="server" Text="Hingga :"></asp:label> 
                                            <br />
                                            <asp:DropDownList ID="ddlKWTo" runat="server" CssClass="form-control" Width="100%"/>
                                            <asp:RequiredFieldValidator ID="rfvddlKWTo" runat="server" ControlToValidate="ddlKWTo" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="btnPaparCarian1" Display="Dynamic" />
                                        </td>

                                    </tr>
                                     <tr style="height:40px;">
                                        <td>&nbsp;<asp:label ID="lblKOFrom" runat="server" Text="KO dari : "></asp:label>
                                            <br />
                                            <asp:DropDownList ID="ddlKOFrom" runat="server" CssClass="form-control" Width="100%"/>
                                            <asp:RequiredFieldValidator ID="rfvddlKOFrom" runat="server" ControlToValidate="ddlKOFrom" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="btnPaparCarian1" Display="Dynamic" />
                                        </td>

                                        <td>&nbsp;<asp:label ID="lblKOTo" runat="server" Text="Hingga :"></asp:label> 
                                            <br />
                                            <asp:DropDownList ID="ddlKOTo" runat="server" CssClass="form-control" Width="100%"/>
                                            <asp:RequiredFieldValidator ID="rfvddlKOTo" runat="server" ControlToValidate="ddlKOTo" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="btnPaparCarian1" Display="Dynamic" />
                                        </td>

                                    </tr>
                                    <tr style="height:40px;">
                                        <td>&nbsp;<asp:label ID="lblPTJFrom" runat="server" Text="PTj Dari :"></asp:label> 
                                            <br />
                                            <asp:DropDownList ID="ddlPTjFrom" runat="server" CssClass="form-control" Width="100%"/>
                                            <asp:RequiredFieldValidator ID="rfvddlPTJFrom" runat="server" ControlToValidate="ddlPTjFrom" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="btnPaparCarian1" Display="Dynamic" />
                                        </td>

                                        <td >&nbsp;<asp:label ID="lblPTJTo" runat="server" Text="Hingga :"></asp:label>
                                            <br />
                                            <asp:DropDownList ID="ddlPTjTo" runat="server" CssClass="form-control" Width="100%"/>
                                            <asp:RequiredFieldValidator ID="rfvddlPTJTo" runat="server" ControlToValidate="ddlPTjTo" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="btnPaparCarian1" Display="Dynamic" />
                                        </td>

                                    </tr>
                                     <tr style="height:40px;">
                                        <td>&nbsp;<asp:label ID="lblKPFrom" runat="server" Text="KP dari :"></asp:label> 
                                            <br />
                                            <asp:DropDownList ID="ddlKPFrom" runat="server" CssClass="form-control" Width="100%"/>
                                            <asp:RequiredFieldValidator ID="rfvddlKPFrom" runat="server" ControlToValidate="ddlKPFrom" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="btnPaparCarian1" Display="Dynamic" />
                                        </td>

                                        <td>&nbsp;<asp:label ID="lblKPTo" runat="server" Text="Hingga :"></asp:label>
                                            <br />
                                            <asp:DropDownList ID="ddlKPTo" runat="server" CssClass="form-control" Width="100%"/>
                                            <asp:RequiredFieldValidator ID="rfvddlKPTo" runat="server" ControlToValidate="ddlKPTo" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="btnPaparCarian1" Display="Dynamic" />
                                        </td>

                                    </tr>
                                    <tr style="height:40px;">
                                        <td>&nbsp;<asp:label ID="lblVotFrom" runat="server" Text="Vot Dari :"></asp:label>
                                            <br />
                                            <asp:DropDownList ID="ddlVotFrom" runat="server" CssClass="form-control" Width="100%"/>
                                           <asp:RequiredFieldValidator ID="rfvddlVotFrom" runat="server" ControlToValidate="ddlVotFrom" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="btnPaparCarian1" Display="Dynamic" />
                                        </td>

                                        <td>&nbsp;<asp:label ID="lblVotTo" runat="server" Text="Hingga :"></asp:label>
                                            <br />
                                            <asp:DropDownList ID="ddlVotTo" runat="server" CssClass="form-control" Width="100%" />
                                            <asp:RequiredFieldValidator ID="rfvddlVotTo" runat="server" ControlToValidate="ddlVotTo" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="btnPaparCarian1" Display="Dynamic" />
                                        </td>

                                    </tr>
                                    <tr>                                        
                                        <td colspan="2">
                                            
                                        </td>
                                    </tr>

                                </table>

                                <div style="text-align: center; margin-bottom: 20px;margin-top: 30px;">
                                            <asp:linkButton ID="lbtnPaparCarian1" runat="server" CssClass="btn" ValidationGroup="btnPaparCarian1">
                                            <i class="far fa-file-alt"></i>&nbsp;&nbsp;&nbsp;Papar
                                            </asp:linkButton>
                                                </div>
                            </div>
                        </td>
                    </tr>

                </table>

            </ContentTemplate>

        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
