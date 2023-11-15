<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Jualan_Universiti.aspx.vb" Inherits="SMKB_Web_Portal.Sebut_Harga_Universiti" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Jualan Universiti</h1>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <p></p>


            <div class="panel panel-default" >
                <div class="panel-heading">
                    <h3 class="panel-title">Senarai Jualan Naskah Sebut Harga / Tender Universiti</h3>
                </div>
                <div class="panel-body">
                    Status&nbsp;:&nbsp;&nbsp;           
						<asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" CssClass="form-control"></asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Tahun&nbsp;:
					<asp:TextBox ID="txtTahun" runat="server" CssClass="form-control" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
                    <br />
                    <br />

                    <asp:GridView ID="gvPO" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="true" EmptyDataText=" No Data "
                        CssClass="table table-striped table-bordered table-hover" Width="100%" BorderStyle="Solid" ShowFooter="false" Font-Size="8pt">
                        <Columns>
                            <asp:TemplateField HeaderText="Bil" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="No Perolehan" DataField="PO01_NoMohon" SortExpression="PO01_NoMohon" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Tujuan" DataField="PO01_Tujuan" SortExpression="PO01_Tujuan" />
                            <asp:BoundField HeaderText="Kategori" DataField="ButiranBrg" SortExpression="ButiranBrg" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="No Naskah Jualan" DataField="PO02_JualanID" SortExpression="PO02_JualanID" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Tarikh & Masa Mula Iklan" DataField="TarikhMasaMulaIklan" SortExpression="TarikhMasaMulaIklan" />
                            <asp:BoundField HeaderText="Tarikh & Masa Tamat Perolehan" DataField="TarikhMasaTamatPO" SortExpression="TarikhMasaTamatPO" />
                            <asp:BoundField HeaderText="Status" DataField="ButiranStatus" SortExpression="ButiranStatus" HeaderStyle-CssClass="centerAlign" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CssClass="btn-xs btn-primary" ToolTip="Pilih Kemaskini">
											<i class="fa fa-ellipsis-h "></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
