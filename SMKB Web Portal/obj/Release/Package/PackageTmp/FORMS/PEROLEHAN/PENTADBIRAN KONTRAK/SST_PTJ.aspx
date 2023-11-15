<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="SST_PTJ.aspx.vb" Inherits="SMKB_Web_Portal.SST_PTJ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style type="text/css">
        .calendarContainerOverride table {
            width: 0px;
            height: 0px;
        }

            .calendarContainerOverride table tr td {
                padding: 0;
                margin: 0;
            }
    </style>

    <h1>Surat Setuju Terima</h1>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <p></p>


            <div class="panel panel-default" style="width: 90%">
                <div class="panel-heading">
                    <h3 class="panel-title">Senarai Perolehan
                    </h3>
                </div>
                <div class="panel-body">
                    Tahun: &nbsp;<asp:TextBox ID="txtTahun" runat="server" CssClass="form-control" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp; Status: &nbsp;SAH PERLANTIKAN VENDOR
                            <br />
                    <br />

                    <asp:GridView ID="gvPO" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="true" EmptyDataText=" Tiada rekod"
                        CssClass="table table-striped table-bordered table-hover" Width="100%" BorderStyle="Solid" ShowFooter="false" Font-Size="8pt">
                        <Columns>

                            <asp:TemplateField HeaderText="Bil">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="No Perolehan" DataField="PO01_NoMohon" SortExpression="PO01_NoMohon" />
                            <asp:BoundField HeaderText="No Daftar Sebut Harga" DataField="PO02_NoDaftar" SortExpression="PO02_NoDaftar" />
                            <asp:BoundField HeaderText="No Naskah Jualan" DataField="PO02_JualanID" SortExpression="PO02_JualanID" />
                            <asp:BoundField HeaderText="Tujuan" DataField="PO01_Tujuan" SortExpression="PO01_Tujuan" />
                            <asp:BoundField HeaderText="Kategori" DataField="ButiranBrg" SortExpression="ButiranBrg" />
                            <asp:BoundField HeaderText="Status" DataField="ButiranStatus" SortExpression="ButiranStatus" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CssClass="btn-xs btn-primary c" ToolTip="Maklumat Lanjut"
                                        data-toggle="collapse" data-target="#collapse1">
											<i class="fa fa-ellipsis-h fa-lg"></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="3%" HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            </div>
        
     
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

