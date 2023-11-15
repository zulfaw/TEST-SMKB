<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Kawalan_Tarikh.aspx.vb" EnableEventValidation="False" Inherits="SMKB_Web_Portal.Capaian_Kelompok_" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>
    <link rel="stylesheet" type="text/css" href="../../../Content/Site.css">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="panel panel-default">
                            <div class="panel-body">
                    <table style="width: 100%" class="table table table-borderless">
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Tahap Pengguna" Style="width: 20%;"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlTahapPengguna" runat="server" AutoPostBack="True" CssClass="form-control" Style="width: 100%;"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvDdlThpPgna" runat="server" ControlToValidate="ddlTahapPengguna" ErrorMessage="" ForeColor="#820303" Text="*Sila Pilih" ValidationGroup="btnSave" Display="Dynamic" CssClass="fontValidatation"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                <asp:Label ID="Label1" runat="server" Text="Modul"></asp:Label>
                            </td>
                            <td >
                                <asp:DropDownList ID="ddlKodModul" runat="server" AutoPostBack="True" CssClass="form-control" Width="100%"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Sub Modul"></asp:Label>
                            </td>
                            <td >
                                <asp:DropDownList ID="ddlKodSubModul" runat="server" AutoPostBack="True" CssClass="form-control" Width="100%"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label class="control-label" for="">
                                Sub Menu:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlKodSubMenu" runat="server" AutoPostBack="True" CssClass="form-control" Width="100%">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvKodSubMenu" runat="server" ControlToValidate="ddlKodSubMenu" ErrorMessage="" ForeColor="#820303" Text="*Sila Pilih" ValidationGroup="btnSimpan" Display="Dynamic" CssClass="fontValidatation"></asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="Jenis Capaian" Style="width: 20%;"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlJenisCapaian" runat="server" AutoPostBack="True" CssClass="form-control" Style="width: 130px;"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvDdlJnsCapaian" runat="server" ControlToValidate="ddlJenisCapaian" ErrorMessage="" ForeColor="#820303" Text="*Sila pilih" ValidationGroup="btnSave" Display="Dynamic" CssClass="fontValidatation"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="trTarikh" runat="server">
                            <td>
                                <asp:Label ID="Label7" runat="server" Text="Tarikh Mula" Style="width: 20%;"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control centerAlign" Text="<%# System.DateTime.Now.ToShortDateString() %>" Width="100px"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="calStartDate" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtStartDate" TodaysDateFormat="dd/MM/yyyy" />
                                <asp:RequiredFieldValidator ID="rfvTarikhMula" runat="server" ControlToValidate="txtStartDate" ErrorMessage="" ForeColor="#820303" Text="*Perlu diisi" ValidationGroup="btnSave" Display="Dynamic" CssClass="fontValidatation"></asp:RequiredFieldValidator>

                                &nbsp;&nbsp;&nbsp;<label class="control-label" for="">Tarikh Tamat:</label>
                                &nbsp;&nbsp;&nbsp;
									<asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control centerAlign" Text="<%# System.DateTime.Now.AddDays(1).ToShortDateString() %>" Width="100px"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="calEndDate" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtEndDate" TodaysDateFormat="dd/MM/yyyy" />
                                <asp:RequiredFieldValidator ID="rfvTarikhAkhir" runat="server" ControlToValidate="txtEndDate" ErrorMessage="" ForeColor="#820303" Text="*Perlu diisi" ValidationGroup="btnSave" Display="Dynamic" CssClass="fontValidatation"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label9" runat="server" Text="Fungsi Dibenarkan" Style="width: 20%;"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFungsi" runat="server" AutoPostBack="True" CssClass="form-control centerAlign" Style="width: 150px;"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvKod" runat="server" ControlToValidate="ddlFungsi" ErrorMessage="" ForeColor="#820303" Text="*Sila pilih" ValidationGroup="btnSave" Display="Dynamic" CssClass="fontValidatation"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <div class="row">
                        <div style="text-align: center">
                            <asp:LinkButton ID="lbtnReset" runat="server" CssClass="btn btn-info" ToolTip="Kosongkan Butiran Perbelanjaan">
						            <i class="fas fa-sync-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Reset
                            </asp:LinkButton>
                            &nbsp;&nbsp;&nbsp;
					            <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn btn-info" ValidationGroup="btnSimpan">
						            <i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                                </asp:LinkButton>
                            <asp:LinkButton ID="lbtnKemaskini" runat="server" CssClass="btn btn-info" ToolTip="Kemaskini" ValidationGroup="btnSimpan">
                                    <i class="fas fa-edit fa-lg"></i>&nbsp;&nbsp;&nbsp;Kemaskini
                            </asp:LinkButton>
                            &nbsp;&nbsp;
                                <asp:LinkButton ID="lbtnUndo" runat="server" CssClass="btn btn-info" ToolTip="Kemaskini">
                                    <i class="fas fa-undo fa-lg"></i>&nbsp;&nbsp;&nbsp;Undo
                                </asp:LinkButton>
                        </div>
                    </div>

                    <div class="GvTopPanel" style="height: 33px;">
                        <div style="float: left; margin-top: 5px; margin-left: 10px;">
                            Jumlah rekod :&nbsp;
                            <asp:Label ID="lblJumRekodS" runat="server" style="color: mediumblue;" ></asp:Label>

                            &nbsp;&nbsp;   <b style="color: #969696;">|</b> &nbsp;&nbsp;

                            Saiz Rekod : 
                            <asp:DropDownList ID="ddlSaizRekod" runat="server" Width="50px" AutoPostBack="True" class="form-control">
                                <asp:ListItem Text="25" Value="25" Selected="True" />
                                <asp:ListItem Text="50" Value="50" />
                                <asp:ListItem Text="100" Value="100" />
                                <asp:ListItem Text="500" Value="500" />
                            </asp:DropDownList>
                            <%-- &nbsp;&nbsp;
                           Cari : 
                           &nbsp;&nbsp;
                               <asp:TextBox ID="txtCariPendahuluan" runat="server" CssClass="form-control" Width="150px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtCariPendahuluan" runat="server" ControlToValidate="txtCariPendahuluan" Display="Dynamic" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="lbtnCariPendahuluan"></asp:RequiredFieldValidator>
                            &nbsp;
                               <asp:DropDownList ID="ddlCariPendahuluan" runat="server" Width="150px" class="form-control">
                                   <asp:ListItem Text="Kod Submenu" Value=1 Selected="True" />
                                   <asp:ListItem Text="Nama Submenu" Value=2 />
                                   <asp:ListItem Text="Kod Tahap" Value=3 />
                               </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;
                               <asp:LinkButton ID="lbtnCariPendahuluan" runat="server" CssClass="btn-circle" ToolTip="Cari" ValidationGroup="lbtnCariPendahuluan">
                        <i class="fa fa-search fa-lg" aria-hidden="true"></i>
                               </asp:LinkButton>--%>
                        </div>
                    </div>

                    <asp:GridView ID="gvTahapPengguna" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="true" AllowPaging="true" PageSize="100"
                        CssClass="table table-striped table-bordered table-hover" Width="100%" Font-Size="8pt" EmptyDataText=" " BorderStyle="Solid">
                        <Columns>
                            <asp:TemplateField HeaderText="Bil" ItemStyle-Width="2%" SortExpression="Bil" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Kod Sub Menu" DataField="KodSubMenu" SortExpression="KodSubMenu" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Nama Sub Menu" DataField="NamaSubMenu" SortExpression="NamaSubMenu" />
                            <asp:BoundField HeaderText="Kod Tahap" DataField="Butiran" SortExpression="Butiran" />
                            <asp:TemplateField SortExpression="JenCapai" HeaderText="Jenis Capaian">
                                <ItemTemplate>
                                    <asp:Label ID="lblJnsCapaian" runat="server" Text='<%#IIf(Eval("JenCapai") = "U", "U - TIADA HAD", "L - TERHAD") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField HeaderText="Jenis Capaian" DataField="JenisCapaian" SortExpression="JenisCapaian" />--%>
                            <asp:BoundField HeaderText="Tarikh Mula" DataField="TkhMula" SortExpression="TkhMula" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Tarikh Tamat" DataField="TkhTamat" SortExpression="TkhTamat" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField SortExpression="Fungsi" HeaderText="Fungsi Dibenarkan">
                                <ItemTemplate>
                                    <asp:Label ID="lblFungsi" runat="server" Text='<%#IIf(Eval("Fungsi") = "R", "R - BACA SAHAJA", "W - BACA & TULIS") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="Fungsi" HeaderText="Fungsi Dibenarkan" SortExpression="Fungsi" />--%>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CssClass="btn-xs" ToolTip="Pilih Kemaskini">
						<i class="fas fa-edit fa-lg"></i>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CssClass="btn-xs" ToolTip="Padam"
                                        OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');">
						<i class="fas fa-trash-alt fa-lg"></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="5%" HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <SelectedRowStyle ForeColor="#0000FF" />
                    </asp:GridView>


                </div>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <hr>
</asp:Content>
