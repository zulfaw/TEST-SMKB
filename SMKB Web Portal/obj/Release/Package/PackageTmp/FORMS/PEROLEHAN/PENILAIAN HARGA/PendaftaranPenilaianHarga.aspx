<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="PendaftaranPenilaianHarga.aspx.vb" Inherits="SMKB_Web_Portal.PendaftaranPenilaianHarga" %>

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
    <h1>Pendaftaran Penilaian Harga</h1>
    <p></p>
    <div class="row">
        <asp:LinkButton ID="lbtnKembali" runat="server" CssClass="btn btn-info">
			<i class="fa fa-arrow-left fa-lg"></i>&nbsp;&nbsp;&nbsp;Kembali
        </asp:LinkButton>
    </div>
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="panel panel-default" style="width: auto;">

                <div class="panel-heading">
                    <h3 class="panel-title">Maklumat Penilaian Harga
                    </h3>
                </div>

                <div class="panel-body" style="overflow-x: auto">
                    <table style="width: 100%;" class="table table-borderless table-striped">
                        <tr>
                            <td style="width: 20%">ID Naskah Jualan :</td>
                            <td style="width: 80%">
                                <asp:Label ID="lblIdNJ" runat="server" />
                                &nbsp;&nbsp;&nbsp;Status:
								&nbsp;<asp:Label ID="lblStatus" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%;">No Perolehan :</td>
                            <td>
                                <div class="form-inline">
                                    <asp:Label ID="lblNoPO" runat="server"></asp:Label>
                                </div>

                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; width: 20%;">Tujuan Perolehan :</td>
                            <td>
                                <asp:Label ID="lblTujuan" runat="server"></asp:Label>
                            </td>
                        </tr>

                        <tr>
                            <td style="width: 20%;">Kategori Perolehan :</td>
                            <td>
                                <asp:Label ID="lblKategoriPO" runat="server"></asp:Label>

                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%;">Kaedah Perolehan :</td>
                            <td>
                                <asp:Label ID="lblKaedahPO" runat="server"></asp:Label>

                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%;">PTJ :</td>
                            <td>
                                <asp:Label ID="lblPTjMohon" runat="server"></asp:Label>

                            </td>
                        </tr>

                        <tr>
                            <td style="width: 20%;">No Sebut Harga / Tender :</td>
                            <td>
                                <asp:Label ID="lblNoSHTD" runat="server"></asp:Label>

                            </td>
                        </tr>
                        <%--<tr>
								<td style="width: 20%;">Anggaran Harga (RM):</td>
								<td>
									<asp:label ID="lblAngHarga" runat="server" ></asp:label>
									
								</td>
							</tr>--%>
                        <tr>
                            <td style="width: 20%;">Tarikh Tamat Perolehan:</td>
                            <td>
                                <asp:Label ID="lblTamatPO" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%;">Tarikh Tamat Pembuka:</td>
                            <td>
                                <asp:Label ID="lblTarikhTmtPembuka" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr class="calendarContainerOverride">
                            <td style="width: 20%;">Tarikh Mesyuarat :</td>
                            <td>
                                <asp:TextBox ID="txtTarikh" runat="server" CssClass="form-control rightAlign" Width="120px"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="cexttxtTarikh" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtTarikh" TodaysDateFormat="dd/MM/yyyy" PopupButtonID="lbtntxtTarikh" />
                                <asp:LinkButton ID="lbtntxtTarikh" runat="server" ToolTip="Klik untuk papar kalendar">
                                        <i class="far fa-calendar-alt fa-lg"></i>
                                </asp:LinkButton>
                                <asp:RequiredFieldValidator ID="rfvtxtTarikh" runat="server" ControlToValidate="txtTarikh" ErrorMessage="" ForeColor="Red" Text="*Sila Isi" ValidationGroup="btnSimpan" Display="Dynamic"></asp:RequiredFieldValidator>
                                &nbsp;&nbsp; Masa : &nbsp;&nbsp;
                                    <asp:TextBox ID="txtMasa" runat="server" CssClass="form-control" Width="100px" TextMode="Time"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtMasa" runat="server" ControlToValidate="txtMasa" ErrorMessage="" ForeColor="Red" Text="*Sila Isi" ValidationGroup="btnSimpan" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Tempat :</td>
                            <td>
                                <asp:TextBox ID="txtTempat" runat="server" CssClass="form-control" Width="90%"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtTempat" runat="server" ControlToValidate="txtTempat" ErrorMessage="" ForeColor="Red" Text="*Sila Isi" ValidationGroup="btnSimpan" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>

                    <br />

                    <br />
                    <div class="panel panel-default" style="width: inherit">
                        <div class="panel-heading">
                            <h3 class="panel-title">Senarai Kehadiran Penilai Harga</h3>
                        </div>
                        <div class="panel-body" style="overflow-x: auto">
                            <table style="width: 100%" class="table table-borderless table-striped">
                                <tr>
                                    <td style="width: 10%;">PTJ :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlPTJ" runat="server" AutoPostBack="True" CssClass="form-control" Width="90%"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlPTJ" runat="server" ControlToValidate="ddlPTJ" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="btnTambah" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%;">Nama Staf :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlStaf" runat="server" AutoPostBack="True" CssClass="form-control" Width="90%"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlStaf" runat="server" ControlToValidate="ddlStaf" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="btnTambah" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center;">
                                        <asp:LinkButton ID="lbtnTambahStaf" runat="server" CssClass="btn btn-info" ToolTip="Tambah ke senarai" ValidationGroup="btnTambah">
                                            <i class="fa fa-plus-circle fa-lg"></i>&nbsp;&nbsp;&nbsp;Tambah
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <asp:GridView ID="gvJawatanKuasa" runat="server" AllowPaging="false" AllowSorting="True" AutoGenerateColumns="False" BorderStyle="Solid" CssClass="table table-striped table-bordered table-hover"
                                EmptyDataText=" Tiada Rekod" ShowFooter="false" ShowHeaderWhenEmpty="True" Width="100%" DataKeyNames="PO05_StafID" Font-Size="8pt">
                                <Columns>
                                    <asp:TemplateField HeaderText="Bil" ItemStyle-Width="2%">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="No Staf" DataField="PO05_StafID" SortExpression="PO05_StafID" ReadOnly="true" ItemStyle-Width="5%" />
                                    <asp:BoundField HeaderText="Nama" DataField="PO05_NamaStaf" SortExpression="PO05_NamaStaf" ReadOnly="true" ItemStyle-Width="30%" />
                                    <asp:BoundField HeaderText="PTJ" DataField="PO05_KodPTJStaf" SortExpression="PO05_KodPTJStaf" ReadOnly="true" ItemStyle-Width="8%" />
                                    <asp:BoundField HeaderText="Jawatan" DataField="PO05_JawStaf" SortExpression="PO05_JawStaf" ReadOnly="true" ItemStyle-Width="20%" />
                                    <asp:BoundField HeaderText="Email" DataField="PO05_EmailStaf" SortExpression="PO05_EmailStaf" ReadOnly="true" ItemStyle-Width="10%" />
                                    <asp:TemplateField ItemStyle-CssClass="centerAlign">
                                        <ItemTemplate>

                                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CssClass="btn-xs" ToolTip="Padam"
                                                OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');">
                                            <i class="fas fa-trash-alt fa-lg"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Width="3%" />

                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <br />

                        </div>
                    </div>

                </div>


                <div class="row">
                    <div class="col-md-12" style="text-align: center">
                        <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn" ToolTip="Simpan & Seterusnya" ValidationGroup="btnSimpan">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                        </asp:LinkButton>
                    </div>

                </div>
            </div>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" href="#collapse1"><span class="fas fa-users fa-lg"></span>&nbsp&nbsp Urusetia</a>
                    </h4>
                </div>
                <div id="collapse1" class="panel-collapse collapse">
                    <div class="panel-body">
                        <div class="row">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 5%;">
                                        <button type="button" id="btnPemohon" class="btn btn-default btn-circle btn-lg" data-toggle="collapse" data-target="#Pemohon" title="Pemohon"><span class="fa fa-user"></span></button>
                                    </td>
                                    <td style="width: 80%;">
                                        <div id="Pemohon" class="collapse in">
                                            &nbsp&nbsp Urusetia
                                            <br />
                                            &nbsp&nbsp<asp:TextBox ID="txtNamaUrusetia" runat="server" CssClass="form-control" ReadOnly="true" Width="45%" ToolTip="Nama Pemohon"></asp:TextBox>
                                            &nbsp<asp:TextBox ID="txtJawUrusetia" runat="server" CssClass="form-control" ReadOnly="true" Width="50%" ToolTip="Jawatan Pemohon"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
