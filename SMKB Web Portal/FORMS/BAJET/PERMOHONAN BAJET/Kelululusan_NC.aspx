<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Kelululusan_NC.aspx.vb" Inherits="SMKB_Web_Portal.Kelululusan_NC" EnableEventValidation="False" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1><%= TotalRecDt %></h1>
    <link rel="stylesheet" type="text/css" href="../../../Content/Site.css">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <script type="text/javascript">

                function pageLoad() {
                    $('[data-toggle="tooltip"]').tooltip()

                }

                function showNestedGridView(obj) {

                    var nestedGridView = document.getElementById(obj);
                    var imageID = document.getElementById('image' + obj);
                    if (nestedGridView.style.display == "none") {
                        nestedGridView.style.display = "inline";
                        imageID.src = "../../../Images/minus.png";
                    } else {
                        nestedGridView.style.display = "none";
                        imageID.src = "../../../Images/plus.png";
                    }
                }
            </script>
            <style type="text/css">
                .hiddencol {
                    display: none;
                }
            </style>
            <style>
                .btnDisabled {
                    background: #ffffff;
                    background-image: -webkit-linear-gradient(top, #ffffff, #eaeff2);
                    background-image: -moz-linear-gradient(top, #ffffff, #eaeff2);
                    background-image: -ms-linear-gradient(top, #ffffff, #eaeff2);
                    background-image: -o-linear-gradient(top, #ffffff, #eaeff2);
                    background-image: linear-gradient(to bottom, #ffffff, #eaeff2);
                    -webkit-border-radius: 5;
                    -moz-border-radius: 5;
                    border-radius: 3px;
                    font-family: Arial;
                    color: #999999;
                    font-size: 12px;
                    padding: 5px 5px 5px 5px;
                    border: solid #808285 1px;
                    text-decoration: none;
                    width: 120px;
                }
            </style>

            <script type="text/javascript">
                function Calculation() {
                    var grid = document.getElementById("<%= gvButiranVot.ClientID%>");
                    var total = 0;
                    for (var i = 0; i < grid.rows.length - 1; i++) {
                        var txtAmountReceive = $("input[id*=txtBajet]")
                        //if (txtAmountReceive[i].value != '') {
                        alert(txtAmountReceive[i].value);

                        total = total + parseFloat(txtAmountReceive[i].value);
                        document.getElementById("myText").value = total;
                        //}
                    }



                }
            </script>

            <div class="row">
                <div class="panel-group" id="pnlNotice" runat="server" visible="false">
                    <div class="panel panel-default" style="border-color: #b94a48;">
                        <div class="panel-heading">
                            <h4 class="panel-title">
                                <a data-toggle="collapse" href="#collapse1"><span class="fa fa-bell-o fa-lg"></span>&nbsp&nbsp Notifikasi
                          <i class="fa fa-chevron-down" aria-hidden="true"></i>
                                </a>

                            </h4>
                        </div>
                        <div id="collapse1" class="panel-collapse collapse">
                            <div class="panel-body" style="width: 100%;">
                                <%= strNotice %>
                            </div>

                        </div>
                    </div>
                </div>

                <%--<div class="panel panel-default" style="width:70%">
            <div class="panel-heading"><h4 class="panel-title">Tapisan</h4></div>
            <div class="panel-body">
                 B<label class="control-label" for="">ahagian&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</label> 
            <asp:DropDownList ID="ddlUnit" runat="server" AutoPostBack="True" CssClass="form-control" Width="50%"></asp:DropDownList>
            </div>
            <div class="panel-body">
                 Status<label class="control-label" for="">&nbsp;&nbsp;:</label> 
            <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" CssClass="form-control" Width="50%"></asp:DropDownList>
            </div>
            </div>--%>
                <br />
                <br />
                <div>
                    <table class="table table table-borderless" style="width: 100%;">
                        <tr style="height: 25px">
                            <td class="auto-style2">
                                <label class="control-label" for="">
                                    Bajet tahun</label></td>
                            <td style="width: 50%">
                                <asp:TextBox ID="txtYear" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </td>
                            <td class="auto-style3" rowspan="4">
                                <div>
                                    <table class="table table table-borderless" style="width: 100%;" border="1">
                                        <tr style="background-color: #FECB18">
                                            <td class="text-center" style="border: 1px solid black;"><strong>Jumlah (RM)</strong></td>
                                            <td class="text-center" style="border: 1px solid black;"><strong>Status</strong></td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style1" style="border: 1px solid black;">Permohonan</td>
                                            <td class="auto-style2" style="border: 1px solid black;">0.00</td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style7" style="border: 1px solid black;">Agihan Bendahari</td>
                                            <td style="border: 1px solid black;" class="text-right">0.00</td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style7" style="border: 1px solid black;">Lulus KPT</td>
                                            <td style="border: 1px solid black;" class="text-right">0.00</td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid black;" class="auto-style7">Agihan NC</td>
                                            <td class="text-right" style="border: 1px solid black;">0.00</td>
                                        </tr>

                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr style="height: 25px">
                            <td class="auto-style2">
                                <label class="control-label" for="">
                                    Kumpulan Wang :</label></td>
                            <td>
                                <label class="control-label" for="">
                                    <asp:DropDownList ID="ddlKW" runat="server" AutoPostBack="True" CssClass="form-control" Style="width: 40%;">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr style="height: 25px">
                            <td class="auto-style2">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr style="height: 25px">
                            <td class="auto-style2">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </div>

                <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" CssClass="tabCtrl" Height="100%" Width="100%">
                    <ajaxToolkit:TabPanel ID="tabRingkasan" runat="server" ActiveTabIndex="1" HeaderText="Ringkasan ABM">
                        <ContentTemplate>
                            <div class="panel-body">
                                <div class="row" style="width: 99%;">
                                    <asp:GridView ID="gvAgihPTj" runat="server" AutoGenerateColumns="False" BorderColor="#333333" BorderStyle="Solid"
                                        CssClass="table table-striped table-bordered table-hover" EmptyDataText=" " Font-Size="8pt" Height="100%" ShowFooter="True" ShowHeaderWhenEmpty="True"
                                        Width="100%" OnRowCommand="gvAgihPTj_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Bil">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBil" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />
                                                </ItemTemplate>
                                                <ItemStyle Width="25px" HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="PTJ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblKodPTJ" runat="server" Text='<%#Eval("KodPTj")%>' Visible="false" />
                                                    <asp:Label ID="lblPTJ" runat="server" Text='<%#Eval("ButPTj")%>' />
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <div style="text-align: right; font-weight: bold;">
                                                        <asp:Label Text="Jumlah Besar (RM)" runat="server" />
                                                    </div>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="1000px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Operasi">
                                                <ItemTemplate>
                                                    <%-- <asp:HyperLink ID="hlAmt10000" runat="server" Text='<%#Eval("Amt10000", "{0:N2}")%>'/>--%>
                                                    <asp:LinkButton ID="lblAmt10000Op" runat="server" CommandName="10Op" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" Text='<%#Eval("Amt10000Op", "{0:N2}")%>'></asp:LinkButton>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <div style="text-align: right;">
                                                        <asp:Label ID="lblJumAm10000Op" runat="server" ForeColor="#003399" Font-Bold="true" />
                                                    </div>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" Width="200px" BackColor="#ACD6FF" Font-Size="9pt" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Komited">
                                                <ItemTemplate>
                                                    <%-- <asp:HyperLink ID="hlAmt10000" runat="server" Text='<%#Eval("Amt10000", "{0:N2}")%>'/>--%>
                                                    <asp:LinkButton ID="lblAmt10000Ko" runat="server" CommandName="10Ko" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" Text='<%#Eval("Amt10000Ko", "{0:N2}")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <div style="text-align: right;">
                                                        <asp:Label ID="lblJumAm10000Ko" runat="server" ForeColor="#003399" Font-Bold="true" Font-Size="9pt" />
                                                    </div>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" Width="200px" BackColor="#D0E8FF" Font-Size="9pt" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Operasi">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblAmt20000Op" runat="server" CommandName="20Op" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" Text='<%#Eval("Amt20000Op", "{0:N2}")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <div style="text-align: right;">
                                                        <asp:Label ID="lblJumAm20000Op" runat="server" ForeColor="#003399" Font-Bold="true" Font-Size="9pt" />
                                                    </div>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" Width="200px" BackColor="#ACD6FF" Font-Size="9pt" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Komited">
                                                 <ItemTemplate>
                                                    <asp:LinkButton ID="lblAmt20000Ko" runat="server" CommandName="20Ko" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" Text='<%#Eval("Amt20000Ko", "{0:N2}")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <div style="text-align: right;">
                                                        <asp:Label ID="lblJumAm20000Ko" runat="server" ForeColor="#003399" Font-Bold="true" Font-Size="9pt" />
                                                    </div>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" Width="200px" BackColor="#D0E8FF" Font-Size="9pt" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Operasi">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblAmt30000Op" runat="server" CommandName="30Op" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" Text='<%#Eval("Amt30000Op", "{0:N2}")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <div style="text-align: right;">
                                                        <asp:Label ID="lblJumAm30000Op" runat="server" ForeColor="#003399" Font-Bold="true" Font-Size="9pt" />
                                                    </div>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" Width="200px" BackColor="#ACD6FF" Font-Size="9pt" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Komited">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblAmt30000Ko" runat="server" CommandName="30Ko" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" Text='<%#Eval("Amt30000Ko", "{0:N2}")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <div style="text-align: right;">
                                                        <asp:Label ID="lblJumAm30000Ko" runat="server" ForeColor="#003399" Font-Bold="true" Font-Size="9pt" />
                                                    </div>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" Width="200px" BackColor="#D0E8FF" Font-Size="9pt" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Operasi">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblAmt40000Op" runat="server" CommandName="40Op" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" Text='<%#Eval("Amt40000Op", "{0:N2}")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <div style="text-align: right;">
                                                        <asp:Label ID="lblJumAm40000Op" runat="server" ForeColor="#003399" Font-Bold="true" Font-Size="9pt" />
                                                    </div>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" Width="200px" BackColor="#ACD6FF" Font-Size="9pt" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Komited">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblAmt40000Ko" runat="server" CommandName="40Ko" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" Text='<%#Eval("Amt40000Ko", "{0:N2}")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <div style="text-align: right;">
                                                        <asp:Label ID="lblJumAm40000Ko" runat="server" ForeColor="#003399" Font-Bold="true" Font-Size="9pt" />
                                                    </div>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" Width="200px" BackColor="#D0E8FF" Font-Size="9pt" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Operasi">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblAmt50000Op" runat="server" CommandName="50Op" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" Text='<%#Eval("Amt50000Op", "{0:N2}")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <div style="text-align: right;">
                                                        <asp:Label ID="lblJumAm50000Op" runat="server" ForeColor="#003399" Font-Bold="true" Font-Size="9pt" />
                                                    </div>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" Width="200px" BackColor="#ACD6FF" Font-Size="9pt" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Komited">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblAmt50000Ko" runat="server" CommandName="50Ko" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" Text='<%#Eval("Amt50000Ko", "{0:N2}")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <div style="text-align: right;">
                                                        <asp:Label ID="lblJumAm50000Ko" runat="server" ForeColor="#003399" Font-Bold="true" Font-Size="9pt" />
                                                    </div>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" Width="200px" BackColor="#D0E8FF" Font-Size="9pt" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Jumlah Permohonan (RM)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmtMohon" runat="server" Text='<%#Eval("AmtMohon", "{0:N2}")%>' />
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <div style="text-align: right;">
                                                        <asp:Label ID="lblJumAm10000" runat="server" ForeColor="#003399" Font-Bold="true" Font-Size="9pt" />
                                                    </div>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" Width="200px" Font-Size="9pt" />
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="btn-xs" ToolTip="Kemas kini">
                                          <i class="fas fa-edit"></i>
                                                    </asp:LinkButton>


                                                </ItemTemplate>

                                                <FooterStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" Width="5px" />
                                            </asp:TemplateField>

                                        </Columns>
                                        <HeaderStyle BackColor="#6699FF" />
                                    </asp:GridView>

                                </div>

                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="tabABM" runat="server" HeaderText="Senarai Permohonan">
                        <ContentTemplate>

                            <asp:GridView ID="gvAbmMaster" runat="server"
                                AllowSorting="True" AutoGenerateColumns="False"
                                BorderColor="#999999" BorderStyle="Double" BorderWidth="1px"
                                CssClass="table table-striped table-bordered table-hover"
                                EmptyDataText=" " Font-Size="8pt" ShowFooter="True"
                                ShowHeaderWhenEmpty="True" Width="95%"
                                DataKeyNames="KodVot" Visible="false">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Panel ID="pnlAbmMaster" runat="server">
                                                <asp:Image ID="imgCollapsible" runat="server" ImageUrl="../../../Images/plus.png" Style="margin-right: 5px;" />
                                                <span style="font-weight: bold; display: none;"><%#Eval("KodVot")%></span>
                                            </asp:Panel>

                                            <cc1:CollapsiblePanelExtender ID="ctlCollapsiblePanel" runat="Server" AutoCollapse="False" AutoExpand="False" CollapseControlID="pnlAbmMaster" Collapsed="True" CollapsedImage="../../../Images/plus.png" CollapsedSize="0" ExpandControlID="pnlAbmMaster" ExpandDirection="Vertical" ExpandedImage="../../../Images/minus.png" ImageControlID="imgCollapsible" ScrollContents="false" TargetControlID="pnlAbmChild" />
                                        </ItemTemplate>
                                        <ItemStyle Width="1px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bil">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBil" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="30px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="KodVot" HeaderText="Objek Am">
                                        <ItemStyle Width="18%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Butiran" HeaderText="Deskripsi">

                                        <ItemStyle Width="80%" />

                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <tr>
                                                <td colspan="100%">
                                                    <asp:Panel ID="pnlAbmChild" runat="server" Style="margin-left: 20px; margin-right: 20px; height: 0px; overflow: hidden;"
                                                        Width="95%">

                                                        <asp:GridView ID="gvAbmDt" runat="server"
                                                            AllowSorting="True" AutoGenerateColumns="False"
                                                            BorderColor="#999999" BorderStyle="Double"
                                                            BorderWidth="1px" CssClass="table table-striped table-bordered table-hover"
                                                            EmptyDataText=" " Font-Size="8pt"
                                                            ShowFooter="True" ShowHeaderWhenEmpty="True"
                                                            Width="100%"
                                                            OnRowCreated="gvAbmDt_RowCreated"
                                                            OnRowDataBound="gvAbmDt_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Bil">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBil" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="2%" HorizontalAlign="Center" />
                                                                </asp:TemplateField>


                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <asp:Label runat="server" Text='Objek Sebagai' />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" Text='11000' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <asp:Label runat="server" Text='Deskripsi' />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" Text='Gaji dan Upahan' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="15%" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <asp:Label runat="server" Text='Perbelanjaan Sebenar Tahun 2018 (RM)' />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" Text='1,235,653.22' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <asp:Label runat="server" Text='Perbelanjaan Sebenar Tahun 2019 (RM)' />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" Text='1,115,232.22' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <asp:Label runat="server" Text='Peruntukan Asal Tahun 2020 (RM)' />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" Text='1,000,256.22' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <asp:Label runat="server" Text='Anggaran Diminta Tahun 2021 (RM)' />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" Text='1,119,653.00' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" BackColor="#E1E1E1" ForeColor="#0033CC" Width="10%" />
                                                                </asp:TemplateField>


                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lbDetail0" runat="server" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="btn-xs btn-primary" OnClick="lnkView_Click" ToolTip="Program">
                                <i class="fa fa-ellipsis-h fa-lg"></i>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" Width="3%" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle BackColor="#FFEFB4" Font-Bold="True" ForeColor="black" />
                                                        </asp:GridView>

                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <ItemStyle Width="1px" />
                                    </asp:TemplateField>


                                </Columns>
                                <SelectedRowStyle BackColor="#FFFFAA" Font-Bold="True" />

                            </asp:GridView>

                            <br />
                            <br />

                        </ContentTemplate>
                        <ContentTemplate>
                            <div style="text-align: left; margin-top: 10px;">

                                <asp:GridView ID="gvMohonBajet" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="false" EmptyDataText=" "
                                    CssClass="table table-striped table-bordered table-hover" Width="100%" BorderStyle="Solid" ShowFooter="true" OnDataBound="OnRowDataBoundxx">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="centerAlign">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="checkAll" runat="server" Text="Hantar" onclick="checkAll(this);" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" onclick="Check_Click(this)" />
                                            </ItemTemplate>
                                            <ItemStyle Width="2%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Kod KW" DataField="KodKW" SortExpression="KodKW" ReadOnly="true" HeaderStyle-CssClass="centerAlign">
                                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                                        </asp:BoundField>

                                        <asp:BoundField HeaderText="Kod KO" DataField="KodOperasi" SortExpression="KodOperasi" ReadOnly="true" HeaderStyle-CssClass="centerAlign">
                                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                                        </asp:BoundField>

                                        <asp:TemplateField HeaderText="Program / Aktiviti">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProgram" runat="server" class="gridViewToolTip"
                                                    Text='<%# Limit(Eval("BG20_Program"), 30) %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="30%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Justifikasi">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescription" runat="server" class="gridViewToolTip"
                                                    Text='<%# Limit(Eval("BG20_Justifikasi"), 30) %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="30%" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="NoMohon" DataField="BG20_NoMohon" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"></asp:BoundField>
                                        <asp:TemplateField HeaderText="Anggaran Permohonan (RM)">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnAnggaran" runat="server" CommandName="SelectVot" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" Text='<%#Eval("AngJumBesar", "{0:N2}")%>' Style="color: blue"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="10%" Font-Size="9pt" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Status" DataField="Status" SortExpression="Status" HeaderStyle-CssClass="centerAlign">
                                            <ItemStyle Width="20%" />
                                        </asp:BoundField>
                                        <%--<asp:buttonfield buttontype="Button" commandname="Select" text="Info" />--%>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnDetail" runat="server" CommandName="Select" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="btn-xs btn-primary" ToolTip="Maklumat Lanjut">
                                <i class="fa fa-ellipsis-h fa-lg"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="3%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="panel panel-default" style="width: 90%">
                                <div>
                                    <h4 class="panel-title" style="font-weight: normal"><strong>Status</strong><asp:RadioButtonList ID="rblStatus" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1">Lengkap</asp:ListItem>
                                        <asp:ListItem Value="0">Tidak Lengkap</asp:ListItem>
                                    </asp:RadioButtonList>
                                    </h4>
                                </div>
                                <div class="panel-heading">
                                    <h4 class="panel-title">Ulasan</h4>
                                </div>
                                <div class="panel-body">

                                    <asp:TextBox ID="txtUlasan" runat="server" CssClass="form-control" Rows="3" Width="90%" Height="53px"></asp:TextBox>
                                </div>
                            </div>

                            <br />
                            <div style="text-align: center">
                                <%--<asp:Button ID="btnMohonBaru" runat="server" Text="Mohon Baru" CssClass="btn" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnHantar" text="Hantar" runat="server" CssClass="btn" />--%>
                                <%--<asp:LinkButton ID="lbtnMohonBaru" runat="server" CssClass="btn btn-info">
                        <i class="fa fa-plus fa-lg"></i>&nbsp;&nbsp;&nbsp;Mohon Baru
                    </asp:LinkButton>--%>
                    &nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="lbtnHantar" runat="server" CssClass="btn btn-info">
                        <i class="fa fa-paper-plane-o fa-lg"></i>&nbsp;&nbsp;&nbsp;Hantar
                    </asp:LinkButton>


                            </div>

                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>

                 

                </ajaxToolkit:TabContainer>
                <div></div>

                <div style="text-align: center; margin-top: 10px;">

                    <%--<asp:LinkButton ID="lbtnHantar" runat="server" CssClass="btn btn-info" data-toggle="tooltip" title="Keseluruhan permohonan bajet akan dihantar ke pejabat Bendahari." >
                        <i class="fa fa-paper-plane-o fa-lg"></i>&nbsp;&nbsp;&nbsp;Hantar
                    </asp:LinkButton> &nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="lbtnKemaskini" runat="server" CssClass="btn btn-info" data-toggle="tooltip" title="Keseluruhan permohonan bajet akan dipulangkan ke penyedia bajet untuk dikemas kini.">
                        <i class="fa fa-pencil-square-o fa-lg"></i>&nbsp;&nbsp;&nbsp;Kemas Kini
                    </asp:LinkButton>--%>

                    <asp:Button ID="btnHantar" runat="server" Text="Simpan" CssClass="btn btn-info" Visible="false" />
                    &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnkemaskini" runat="server" Text="Kemas Kini" CssClass="btn btn-info" Visible="false" />


                </div>


            </div>



            <asp:Panel ID="pnlPopupButiran" runat="server" BackColor="White" Width="800px" Style="">

                <table width="100%" style="border: Solid 3px #D5AA00; width: 100%; height: 100%;" cellpadding="0" cellspacing="0">
                    <tr style="background: linear-gradient(to bottom, #fceabb 0%,#fccd4d 50%,#f8b500 51%,#fbdf93 100%);">
                        <td style="height: 10%; font-weight: bold;" align="center">&nbsp;Maklumat Lanjut</td>
                        <td align="center" style="width: 50px;">
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Close_16x16.png" BorderColor="Black" BorderStyle="Inset" BorderWidth="1px" ToolTip="Tutup" />
                            <%--<asp:Button ID="btnCancel" runat="server" Text="X" />--%></td>


                    </tr>
                    <tr style="vertical-align: top;">
                        <td colspan="2" class="auto-style1">
                            <div style="margin-bottom: 10px; margin-top: 10px; margin-left: 10px; margin-right: 10px;">
                                <asp:Label ID="Label1" runat="server" Text="Jumlah Rekod : " />
                                <span style="font-weight: bold;"><%= TotalRecDt %></span>
                                <br />
                                <asp:GridView ID="gvButiran" runat="server" AllowSorting="True" AutoGenerateColumns="False" BorderColor="#999999" BorderStyle="Double" BorderWidth="1px" CssClass="table table-striped table-bordered table-hover" EmptyDataText=" " Font-Size="8pt" ShowFooter="True" ShowHeaderWhenEmpty="True" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Bil">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBil" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle Width="5%" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Butiran" HeaderStyle-Width="45%" HeaderText="Butiran" SortExpression="Butiran">
                                            <HeaderStyle Width="45%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Kuantiti" HeaderStyle-Width="5%" HeaderText="Kuantiti" SortExpression="Kuantiti">
                                            <HeaderStyle Width="5%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Anggaran" HeaderStyle-Width="15%" HeaderText="Anggaran Perbelanjaan (RM)" SortExpression="Anggaran">
                                            <HeaderStyle Width="15%" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="KodVotSebagai" HeaderText="Vot Sebagai" />
                                    </Columns>
                                    <HeaderStyle BackColor="#FFEFB4" Font-Bold="True" ForeColor="black" />
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <asp:Panel ID="pnlPopProgram" runat="server" BackColor="White" Width="800px">

                <table width="100%" style="border: Solid 3px #D5AA00; width: 100%; height: 100%;" cellpadding="0" cellspacing="0">
                    <tr style="background: linear-gradient(to bottom, #fceabb 0%,#fccd4d 50%,#f8b500 51%,#fbdf93 100%);">
                        <td style="height: 10%; font-weight: bold;" align="center">Program </td>
                        <td align="center" style="width: 50px;">
                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Close_16x16.png" BorderColor="Black" BorderStyle="Inset" BorderWidth="1px" ToolTip="Tutup" />
                            <%--<asp:Button ID="btnCancel" runat="server" Text="X" />--%></td>


                    </tr>
                    <tr style="vertical-align: top;">

                        <td colspan="2">&nbsp;<div style="margin-bottom: 10px; margin-top: 10px; margin-left: 10px; margin-right: 10px;">

                            <asp:Label runat="server" Text="Jumlah Rekod : " /><span style="font-weight: bold;"> <%= TotalRecProg %> </span>
                            <asp:GridView ID="gvProgram" runat="server" AllowSorting="True"
                                AutoGenerateColumns="False"
                                CssClass="table table-striped table-bordered table-hover"
                                EmptyDataText=" " Font-Size="8pt" ShowFooter="True"
                                ShowHeaderWhenEmpty="True" Width="100%"
                                BorderWidth="1px" BorderColor="#999999" BorderStyle="Double">
                                <Columns>
                                    <asp:TemplateField HeaderText="Bil">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBil" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="NoMohon" HeaderText="No. Permohonan"></asp:BoundField>
                                    <asp:BoundField DataField="Bahagian" HeaderText="Bahagian"></asp:BoundField>
                                    <asp:BoundField DataField="Unit" HeaderText="Unit" />
                                    <asp:BoundField DataField="Program" HeaderText="Program" />
                                    <asp:BoundField DataField="AmaunMohon" HeaderText="Anggaran Perbelanjaan (RM)">
                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                    </asp:BoundField>
                                </Columns>
                                <HeaderStyle BackColor="#FFEFB4" Font-Bold="True" ForeColor="black" />
                            </asp:GridView>
                        </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
            <asp:Button ID="btnShowPopup2" runat="server" Style="display: none" />

            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground" CancelControlID="ImageButton2" PopupControlID="pnlPopProgram" TargetControlID="btnShowPopup" BehaviorID="_content_ModalPopupExtender1" DynamicServicePath="">
            </ajaxToolkit:ModalPopupExtender>
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground" CancelControlID="ImageButton1" PopupControlID="pnlPopupButiran" TargetControlID="btnShowPopup2" BehaviorID="_content_ModalPopupExtender1" DynamicServicePath="">
            </ajaxToolkit:ModalPopupExtender>



            <asp:Button ID="btnOpen" runat="server" Text="" Style="display: none" />
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground"
                CancelControlID="lbNo" PopupControlID="pnlpopupHantar" TargetControlID="btnOpen">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="pnlpopupHantar" runat="server" BackColor="White">
                <table style="border: Solid 3px #D5AA00; width: 100%; height: 100%;">
                    <tr style="background: linear-gradient(to bottom, #fceabb 0%,#fccd4d 50%,#f8b500 51%,#fbdf93 100%);">
                        <td style="height: 10%; font-weight: bold; text-align: center;" colspan="2">&nbsp;</td>

                    </tr>

                    <tr style="vertical-align: top;">
                        <td style="height: 10%; font-weight: bold; text-align: center;" colspan="2">
                            <div style="margin-bottom: 10px; margin-top: 10px; margin-left: 10px; margin-right: 10px;">
                                <%= strMsg  %>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10%; text-align: center;" colspan="2">
                            <div style="margin-bottom: 10px; margin-top: 10px; margin-left: 10px; margin-right: 10px;">
                                <asp:LinkButton ID="lbYes" runat="server" CssClass="btn btn-info">
                                <i class="fa fa-check fa-lg"></i>&nbsp;&nbsp;&nbsp;Ya
                                </asp:LinkButton>
                                &nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="lbNo" runat="server" CssClass="btn btn-info">
                                <i class="fa fa-times fa-lg"></i>&nbsp;&nbsp;&nbsp;Tidak
                            </asp:LinkButton>

                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <asp:Button ID="btnPopup2" runat="server" Style="display: none;" />
            <ajaxToolkit:ModalPopupExtender ID="mpePnlSenaraiVot" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlVot" TargetControlID="btnPopup2" BehaviorID="mpe2">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlVot" runat="server" BackColor="White" Width="800px" Style="display: ;">
                <table style="border: Solid 3px #D5AA00; width: 100%; height: 100%;">
                    <tr style="background: linear-gradient(to bottom, #fceabb 0%,#fccd4d 50%,#f8b500 51%,#fbdf93 100%);">
                        <td style="height: 10%; font-weight: bold; text-align: center;" colspan="2">&nbsp;</td>

                    </tr>

                    <tr style="vertical-align: top;">
                        <td style="text-align: center;" colspan="2">
                            <div style="margin-bottom: 10px; margin-top: 10px; margin-left: 10px; margin-right: 10px;">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="height: 30%">Program / Aktiviti</td>
                                        <td style="height: 10%">:</td>
                                        <td style="height: 60%; text-align: left">
                                            <asp:Label ID="lblDtAktiviti" runat="server"></asp:Label>
                                            <asp:Label ID="lblHiddenID" runat="server" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 30%">Anggaran Permohonan (RM)</td>
                                        <td style="height: 10%">:</td>
                                        <td style="height: 60%; text-align: left">
                                            <asp:TextBox ID="txtJumlahPermohonan" runat="server" CssClass="form-control rightAlign" onkeypress="return isNumberKey(event,this)" OnTextChanged="txtBajet_TextChanged"></asp:TextBox></td>

                                    </tr>
                                    <tr>
                                        <td style="height: 30%">&nbsp;</td>
                                        <td style="height: 10%">&nbsp;</td>
                                        <td style="height: 60%; text-align: left">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style1" colspan="3">
                                            <asp:GridView ID="gvButiranVot" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover" EmptyDataText="" Font-Size="8pt" PageSize="5" ShowFooter="True" ShowHeaderWhenEmpty="True" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-CssClass="centerAlign" HeaderText="Bil" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="2%" SortExpression="Bil">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNumber" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="centerAlign" HeaderText="Objek Am" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" SortExpression="KodVotA">
                                                        <ItemTemplate>
                                                            <asp:Label ID="StrKodVotA" runat="server" Text='<%# Eval("KodVotA") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <%--<EditItemTemplate>
                            <asp:DropDownList ID="ddlKodVot" runat="server" CssClass="form-control" Width="300px"></asp:DropDownList>
                        </EditItemTemplate>--%></asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="centerAlign" HeaderText="Objek Sebagai" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" SortExpression="KodVotSbg">
                                                        <ItemTemplate>
                                                            <asp:Label ID="StrKodVotSbg" runat="server" Text='<%# Eval("KodVotSbg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <%--<EditItemTemplate>
                            <asp:DropDownList ID="ddlKodVot" runat="server" CssClass="form-control" Width="300px"></asp:DropDownList>
                        </EditItemTemplate>--%></asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="center" HeaderText="Butiran" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50%" SortExpression="Butiran">
                                                        <ItemTemplate>
                                                            <asp:Label ID="strButiran" runat="server" Text='<%# Eval("Butiran") %>'></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Jumlah Permohonan (RM)">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtBajet" runat="server" Width="98%" Text='<%# Eval("AngHrgUnit", "{0:N}")%>'
                                                                onkeypress="return isNumberKey(event,this)" CssClass="form-control rightAlign" OnTextChanged="txtBajet_TextChanged"></asp:TextBox>
                                                        </ItemTemplate>

                                                        <FooterTemplate>
                                                            <div style="text-align: right;">
                                                                <asp:Label ID="lblTotBajet" runat="server" CssClass="cssTotBajet" />
                                                            </div>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" Width="12%" BackColor="#e8a196" />
                                                    </asp:TemplateField>

                                                </Columns>
                                                <HeaderStyle BackColor="#996633" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 30%">&nbsp;</td>
                                        <td style="height: 10%">&nbsp;</td>
                                        <td style="height: 60%; text-align: left">&nbsp;</td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10%; text-align: center;" colspan="2">
                            <div style="margin-bottom: 10px; margin-top: 10px; margin-left: 10px; margin-right: 10px;">
                                <asp:LinkButton ID="lblKemaskiniVot" runat="server" CssClass="btn btn-info">
                                Kemaskini
                                </asp:LinkButton>
                                &nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-info">
                                Kembali
                            </asp:LinkButton>

                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
