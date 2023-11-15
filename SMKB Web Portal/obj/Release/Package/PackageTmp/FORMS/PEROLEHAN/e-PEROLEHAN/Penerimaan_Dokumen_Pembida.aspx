<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" Culture="ms-MY" CodeBehind="Penerimaan_Dokumen_Pembida.aspx.vb" Inherits="SMKB_Web_Portal.Penerimaan_Dokumen_Pembida" %>

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

    <h1>Penerimaan Dokumen Pembida</h1>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <p></p>


            <div class="panel panel-default" style="width: 90%">
                <div class="panel-heading">
                    <h3 class="panel-title">Senarai Jualan Naskah</h3>
                </div>
                <div class="panel-body">
                    Tahun: &nbsp;<asp:TextBox ID="txtTahun" runat="server" CssClass="form-control" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp; Status: &nbsp;PROSES TERIMAAN DOKUMEN
                            <br />
                    <br />

                    <asp:GridView ID="gvPO" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="true" EmptyDataText=" Tiada Rekod"
                        CssClass="table table-striped table-bordered table-hover" Width="100%" BorderStyle="Solid" ShowFooter="false" Font-Size="8pt">
                        <Columns>

                            <asp:TemplateField HeaderText="Bil">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="No Perolehan" DataField="PO01_NoMohon" SortExpression="PO01_NoMohon" />
                            <asp:BoundField HeaderText="Tujuan" DataField="PO01_Tujuan" SortExpression="PO01_Tujuan" />
                            <asp:BoundField HeaderText="Kategori" DataField="ButiranBrg" SortExpression="ButiranBrg" />
                            <asp:BoundField HeaderText="No Dokumen" DataField="PO02_NoDaftar" SortExpression="PO02_NoDaftar" ItemStyle-HorizontalAlign="Center" />

                            <asp:BoundField HeaderText="Tarikh & Masa Mula Iklan" DataField="PO02_TrkMasaMulaIklan" SortExpression="PO02_TrkMasaMulaIklan" DataFormatString="{0:dddd, dd MMMM yyyy, hh:mm tt}"/>
                            <asp:BoundField HeaderText="Tarikh & Masa Tamat Perolehan" DataField="PO02_TrkMasaTamatPO" SortExpression="PO02_TrkMasaTamatPO" DataFormatString="{0:dddd, dd MMMM yyyy, hh:mm tt}" />
                            <asp:BoundField HeaderText="Status" DataField="ButiranStatus" SortExpression="ButiranStatus" />
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblJualanID" runat="server" Text='<%# Eval("PO02_JualanID")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CssClass="btn-xs" ToolTip="Pilih Kemaskini"
                                        data-toggle="collapse" data-target="#collapse1">
											<i class="far fa-edit fa-lg"></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

                <br />
                <br />
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <a data-toggle="collapse" href="#collapse1" aria-hidden="true">Maklumat Pembida
                            </a>
                        </h3>
                    </div>

                    <div class="panel-body" id="pnlBody" runat="server" visible="false">
                        No Dokumen : <asp:Label ID="lblNoDok" runat="server" />  <asp:HiddenField ID="hfNoPo" runat="server" /> <asp:HiddenField ID="hfnoNJ" runat="server" />
                        <br />
                        <asp:GridView ID="gvPembida" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="false" Font-Size="8pt"
                            CssClass="table table-striped table-bordered table-hover" Width="100%" BorderStyle="Solid" ShowFooter="false" DataKeyNames="PO03_OrderID" EmptyDataText=" Tiada rekod">
                            <Columns>
                                <asp:TemplateField HeaderText="Bil" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Unik ID" DataField="PO03_UnikID" SortExpression="PO03_UnikID" ItemStyle-HorizontalAlign="Center" ReadOnly="true" />
                                <asp:BoundField HeaderText="Kod Pembida" DataField="PO03_BilTerimaanDok" SortExpression="PO03_BilTerimaanDok" ItemStyle-HorizontalAlign="Center" ReadOnly="true" />
                                <asp:BoundField HeaderText="Tarikh & Masa Terimaan" DataField="PO03_TarikhTerimaanDok" SortExpression="PO03_TarikhTerimaanDok" DataFormatString="{0:dd/MM/yyyy hh:mm:dd tt}" ReadOnly="true" ItemStyle-HorizontalAlign="Center" />
                                <asp:TemplateField HeaderText="Terimaan Dokumen?" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%#IIf(IsDBNull(Eval("PO03_StatusTerimaanDok")), False, Eval("PO03_StatusTerimaanDok")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Tindakan">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnSave" runat="server" CommandName="Update" CssClass="btn-xs" ToolTip="Simpan">
												<i class="far fa-save fa-lg"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <SelectedRowStyle ForeColor="Blue" />
                        </asp:GridView>
                        <div style="text-align: center;">
                        <table style="width: inherit; height: 400px; padding: 3px,3px,3px,3cm; width: 100%">
                            <tr>
                                <td>
                                    <div style="text-align: center;">
                                        <img id="Image1" src="../../../Images/logo.png" />
                                    </div>
                                    <div style="text-align: center;">
                                        <h4>UNIVERSITI TEKNIKAL MALAYSIA MELAKA</h4>
                                    </div>
                                    <div style="text-align: center;">
                                        <h4>UNIT PEROLEHAN</h4>
                                    </div>
                                    <div style="text-align: center;">
                                        <h4>BAHAGIAN PENGURUSAN BAJET DAN PEROLEHAN</h4>
                                    </div>
                                    <div style="text-align: center;">
                                        <h4>PEJABAT BENDAHARI</h4>
                                    </div>
                                    <div style="text-align: center;">
                                        <h4>PERAKUAN PEMBEKAL PENDAFTARAN SYARIKAT</h4>
                                    </div>
                                    <br />
                                    <p style="font-size: 14px; text-align: center;">
                                        Saya dengan ini mengesahkan bahawa terimaan dokumen telah tamat dan semua maklumat yang diterima adalah benar dan lengkap.
                                    </p>

                                    &nbsp;
										<asp:CheckBox ID="chxAgree" runat="server" Text=" Saya bersetuju." Font-Size="16px" />
                                    <br />
                                    <br />
                                    <br />
                                    <asp:LinkButton ID="lbtnHantar" runat="server" CssClass="btn btn-primary" ValidationGroup="lbtnHantar" OnClientClick="return confirm('Anda pasti untuk hantar rekod ini?');">
                                                <i class="fab fa-telegram-plane fa-lg"></i>&nbsp;&nbsp;&nbsp;Hantar
                                    </asp:LinkButton>

                                </td>
                            </tr>

                        </table>
                    </div>
                    </div>

                </div>
            </div>
            </div>
        
     
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

