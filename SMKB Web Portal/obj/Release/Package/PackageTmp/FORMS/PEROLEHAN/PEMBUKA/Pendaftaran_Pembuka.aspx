<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Pendaftaran_Pembuka.aspx.vb" Inherits="SMKB_Web_Portal.Pendaftaran_Pembuka" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        function pageLoad() {
            $('[data-toggle="tooltip"]').tooltip();
        }

        function Check_Click(objRef) {
            //Get the Row based on checkbox
            var row = objRef.parentNode.parentNode;


            //Get the reference of GridView
            var GridView = row.parentNode;

            //Get all input elements in Gridview
            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {
                //The First element is the Header Checkbox
                var headerCheckBox = inputList[0];

                //Based on all or none checkboxes
                //are checked check/uncheck Header Checkbox
                var checked = true;
                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                    if (!inputList[i].checked) {
                        checked = false;
                        break;
                    }
                }
            }
            headerCheckBox.checked = checked;
        }

        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        //If the header checkbox is checked
                        //check all checkboxes
                        //and highlight all rows
                        //row.style.backgroundColor = "aqua";
                        inputList[i].checked = true;
                    }
                    else {
                        //If the header checkbox is checked
                        //uncheck all checkboxes
                        //and change rowcolor back to original
                        //if (row.rowIndex % 2 == 0) {
                        //    //Alternating Row Color
                        //    row.style.backgroundColor = "#C2D69B";
                        //}
                        //else {
                        //    row.style.backgroundColor = "white";
                        //}
                        inputList[i].checked = false;
                    }
                }
            }
        }
    </script>
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
    <h1>Proses Pembuka</h1>
    <p></p>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <asp:LinkButton ID="lbtnKembali" runat="server" CssClass="btn btn-info">
					<i class="fa fa-arrow-left fa-lg"></i>&nbsp;&nbsp;&nbsp;Kembali
                </asp:LinkButton>
            </div>
            <br />
            <div class="panel panel-default" id="printPanel" runat="server">
                <div class="panel-heading">
                    <h3 class="panel-title">Maklumat Pembuka
                    </h3>
                </div>

                <div class="panel-body" style="overflow-x: auto; width: initial;">
                    <table style="width: 100%;" class="table table-borderless table-striped">
                        <tr>
                            <td style="width: 20%">ID Naskah Jualan :</td>
                            <td style="width: 80%">
                                <asp:TextBox ID="txtIdNJ" runat="server" BackColor="#FFFFCC" CssClass="form-control" Width="150px" ReadOnly="true"></asp:TextBox>

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
                        <tr>
                            <td style="width: 20%;">Tarikh Tamat Perolehan :</td>
                            <td>
                                <asp:Label ID="lblTamatPO" runat="server" />
                                <%--<asp:TextBox ID="trkTamatPO" runat="server" BackColor="#FFFFCC" ReadOnly="true" CssClass="form-control rightAlign" Width="120px"></asp:TextBox>--%>
                            </td>
                        </tr>
                        <tr class="calendarContainerOverride">
                            <td style="width: 20%;">Tarikh Mula Pembuka :</td>
                            <td>
                                <asp:TextBox ID="txtTarikhMulaPembuka" runat="server" CssClass="form-control rightAlign" Width="120px"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtTarikhMulaPembuka" TodaysDateFormat="dd/MM/yyyy" PopupButtonID="lbtnTarikhMulaPembuka" />
                                <asp:LinkButton ID="lbtnTarikhMulaPembuka" runat="server" ToolTip="Klik untuk papar kalendar">
                                        <i class="far fa-calendar-alt fa-lg"></i>
                                </asp:LinkButton>
                                <asp:RequiredFieldValidator ID="rfvtxtTarikhMulaPembuka" runat="server" ControlToValidate="txtTarikhMulaPembuka" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="btnSimpan" Display="Dynamic"></asp:RequiredFieldValidator>
                                &nbsp;&nbsp;&nbsp; Masa :
                                    <asp:TextBox ID="txtMasaMulaPembuka" runat="server" CssClass="form-control" Width="100px" TextMode="Time"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtTujuan" runat="server" ControlToValidate="txtMasaMulaPembuka" ErrorMessage="" ForeColor="Red" Text="*Sila Isi" ValidationGroup="btnSimpan" Display="Dynamic"></asp:RequiredFieldValidator>
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
                    <div class="panel panel-default" style="width: inherit;">
                        <div class="panel-heading">
                            <h3 class="panel-title">Senarai Pembuka</h3>
                        </div>
                        <div class="panel-body" style="overflow-x: auto">
                            <table style="width: 100%" class="table table-borderless table-striped">
                                <tr>
                                    <td style="width: 15%;">PTJ :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlPTJ" runat="server" AutoPostBack="True" CssClass="form-control" Width="90%"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlPTJ" runat="server" ControlToValidate="ddlPTJ" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="btnTambah" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%;">Nama Staf :
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
                                EmptyDataText=" " ShowFooter="false" ShowHeaderWhenEmpty="True" Width="100%" DataKeyNames="PO04_StafID" Font-Size="8pt">
                                <Columns>
                                    <asp:TemplateField HeaderText="Bil" ItemStyle-Width="2%" HeaderStyle-CssClass="centerAlign">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="No Staf" DataField="PO04_StafID" SortExpression="PO04_StafID" ReadOnly="true" ItemStyle-Width="5%" />
                                    <asp:BoundField HeaderText="Nama" DataField="PO04_NamaStaf" SortExpression="PO04_NamaStaf" ReadOnly="true" ItemStyle-Width="30%" />
                                    <asp:BoundField HeaderText="PTJ" DataField="PO04_KodPTJStaf" SortExpression="PO04_KodPTJStaf" ReadOnly="true" ItemStyle-Width="8%" />
                                    <asp:BoundField HeaderText="Jawatan" DataField="PO04_JawStaf" SortExpression="PO04_JawStaf" ReadOnly="true" ItemStyle-Width="20%" />
                                    <asp:BoundField HeaderText="Email" DataField="PO04_EmailStaf" SortExpression="PO04_EmailStaf" ReadOnly="true" ItemStyle-Width="10%" />
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

                <div style="text-align: center">
                    <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn" ToolTip="Simpan & Seterusnya" ValidationGroup="btnSimpan">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                    </asp:LinkButton>
                    <p></p>
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
