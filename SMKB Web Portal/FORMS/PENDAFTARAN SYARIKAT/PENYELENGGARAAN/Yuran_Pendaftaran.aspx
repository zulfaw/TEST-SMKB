<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Yuran_Pendaftaran.aspx.vb" Inherits="SMKB_Web_Portal.Yuran_Pendaftaran" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function upper(ustr) {
            var str = ustr.value;
            ustr.value = str.toUpperCase();
        };
    </script>
    <h1>Maklumat Lesen Pendaftaran</h1>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <p></p>
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="panel panel-default" style="width: auto">
                            <div class="panel-body">
                                <table style="width: 100%" class="table table table-borderless">

                                    <tr>
                                        <td style="width: 15%;">Amaun:</td>
                                        <td>
                                            <asp:TextBox ID="txtAmaun" runat="server" Width="200px" CssClass="form-control" onkeyup="upper(this)" TextMode="Number"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtAmaun" runat="server" ControlToValidate="txtAmaun" ForeColor="Red" Text="*Sila Isi" ValidationGroup="btnSimpanSub" Display="Dynamic"></asp:RequiredFieldValidator>
                                            <asp:HiddenField ID="hdKod" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 15%;">Status :</td>
                                        <td>
                                            <asp:RadioButtonList ID="rbStatus" runat="server" Height="25px" RepeatDirection="Horizontal" Width="273px">
                                                <asp:ListItem Selected="True" Text=" Aktif" Value="1"></asp:ListItem>
                                                <asp:ListItem Text=" Tidak Aktif" Value="0"></asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="rfvrbStatus" runat="server" ControlToValidate="rbStatus" ForeColor="Red" Text="*Sila Isi" ValidationGroup="btnSimpanSub" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>

                                </table>
                                <div style="text-align: center">
                                    &nbsp;&nbsp;<asp:LinkButton ID="lbtnRekodBaru" runat="server" CssClass="btn btn-info">
						<i class="fas fa-file-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Rekod Baru
                                    </asp:LinkButton>
                                    &nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn btn-info" OnClientClick="return confirm('Anda pasti untuk simpan rekod ini?')">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                        </asp:LinkButton>
                                </div>
                            </div>
                        </div>

                        <div class="panel panel-default" style="width: auto;">
                            <div class="panel-heading">
                                <h3 class="panel-title">Senarai Yuran Pendaftaran
                                </h3>
                            </div>
                            <div class="panel-body" style="overflow-x: auto">

                                <table>

                                    <tr style="height: 30px;">
                                        <td>Jumlah rekod :&nbsp;<label class="control-label" id="lblJumRekod" runat="server" style="color: mediumblue;"></label>
                                        </td>
                                        <td>&nbsp;|&nbsp;&nbsp;Saiz Rekod : 
                                        </td>
                                        <td style="width: 50px;">
                                            <asp:DropDownList ID="ddlSaizRekod" runat="server" Width="50px" AutoPostBack="True" class="form-control">
                                                <asp:ListItem Text="10" Value="10" />
                                                <asp:ListItem Text="25" Value="25" />
                                                <asp:ListItem Text="50" Value="50" Selected="True" />
                                                <asp:ListItem Text="100" Value="100" />
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <asp:GridView ID="gvLesen" runat="server" ShowHeaderWhenEmpty="True" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                    CssClass="table table-striped table-bordered table-hover" Width="100%" BorderStyle="Solid" PageSize="25" DataKeyNames="Kod">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Bil">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Amaun" HeaderText="Amaun" SortExpression="Amaun" ReadOnly="True" ItemStyle-HorizontalAlign="Right" />
                                        <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%#IIf(CBool(Eval("Status")), "Aktif", "Tidak Aktif") %>' ToolTip="Sila check jika tidak hadir" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CssClass="btn-xs" ToolTip="Kemaskini">
                                            <i class="fas fa-edit fa-lg"></i>
                                                </asp:LinkButton>

                                                <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete" CssClass="btn-xs" ToolTip="Padam"
                                                    OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');">
										<i class="far fa-trash-alt fa-lg"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="6%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <SelectedRowStyle ForeColor="Blue" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

