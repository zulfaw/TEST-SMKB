<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Kenderaan.aspx.vb" Inherits="SMKB_Web_Portal.Kenderaan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Permohonan Pinjaman Kenderaan</h1>

    <link rel="stylesheet" type="text/css" href="../../../Content/Site.css">    
    <script type="text/javascript" src="../../../Scripts/misc.js"></script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <p></p>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Senarai Permohonan Pinjaman 
                    </h3>
                </div>
                <div class="panel-body" style="overflow-x: auto">
                    Carian Status&nbsp;:&nbsp;&nbsp;           
                    <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" CssClass="form-control"></asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    Tahun&nbsp;:
                    <asp:TextBox ID="txtTahun" runat="server" CssClass="form-control" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
                    <br />
                    <br />
           <asp:GridView ID="gvMohonPinj" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True" EmptyDataText=" Tiada rekod"
                        CssClass="table table-striped table-bordered table-hover" Width="100%" BorderStyle="Solid" ShowFooter="true" Font-Size="8pt" PageSize="20" DataKeyNames="">
                        <Columns>
                            <asp:TemplateField HeaderText="Bil" ItemStyle-Width="2%">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>.
                                </ItemTemplate>
                                <ItemStyle Width="2%" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:templatefield HeaderText="Tarikh Mohon" HeaderStyle-CssClass="centerAlign">
                                    <itemtemplate>
                                            <asp:label id="lblDate" runat="server" text='<%# Eval("PJM01_TkhMohon", "{0:dd/MM/yyyy}") %>' />
                                    </itemtemplate>
                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                            </asp:templatefield>
                            <asp:BoundField HeaderText="No Pinjaman" DataField="PJM01_NoPinj" SortExpression="PJM01_NoPinj">
                                <ItemStyle Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Jenis Pinjaman" DataField="JenPinj" SortExpression="JenPinj" HeaderStyle-CssClass="centerAlign">
                                <ItemStyle Width="25%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Status Permohonan" DataField="StatusDok" SortExpression="StatusDok" HeaderStyle-CssClass="centerAlign" DataFormatString="{0:dd/MM/yyyy}">
                                <ItemStyle Width="25%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Jumlah Pinjaman (RM)" DataField="PJM01_Amaun" SortExpression="PJM01_Amaun" DataFormatString="{0:N}" HeaderStyle-CssClass="centerAlign">
                                <ItemStyle Width="25%" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbDetail" runat="server" CommandName="Select" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="btn-xs btn-primary" ToolTip="Maklumat Lanjut">
											<i class="fa fa-ellipsis-h fa-lg"></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="3%" HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <div style="text-align: center">
                        <asp:Button ID="btnMohonBaru" Text="Mohon Baru" runat="server" CssClass="btn" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
