<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="MaklumatPenilaianTeknikalExtra.aspx.vb" Inherits="SMKB_Web_Portal.MaklumatPenilaianTeknikalExtra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        function reloadPage() {
            window.location.reload();
        }


        function pageLoad() {
            $('[data-toggle="tooltip"]').tooltip();
        }

        function Validate(sender, args) {
            //Get the reference of GridView
            var gridView = document.getElementById("<%=gvTeknikal.ClientID %>");
               debugger
               var checkBoxes = gridView.getElementsByTagName("input");

               for (var i = 0; i < checkBoxes.length; i++) {
                   var headerCheckBox = inputList[0];

                   if (checkBoxes[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                       if (checkBoxes[i].checked) {
                           args.IsValid = true;
                           return;
                       }
                   }

               }
               args.IsValid = false;
           }

           function Validate2() {
               //Get the reference of GridView
               var gridView = document.getElementById("<%=gvTeknikal.ClientID %>");
        debugger
        var checkBoxes = gridView.getElementsByTagName("input");

        for (var i = 0; i < checkBoxes.length; i++) {
            var headerCheckBox = checkBoxes[0];

            if (checkBoxes[i].type == "checkbox" && checkBoxes[i] != headerCheckBox) {
                if (checkBoxes[i].checked) {
                    return true;
                }
            }
        }
        alert('Sila pilih sekurang-kurangnya satu syor.');
        return false;
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
    <h1>Penilaian Spesifikasi Teknikal</h1>
    <p></p>
    <div class="row">
        <asp:LinkButton ID="lbtnKembali" runat="server" CssClass="btn btn-info">
					<i class="fa fa-arrow-left fa-lg"></i>&nbsp;&nbsp;&nbsp;Kembali
        </asp:LinkButton>
    </div>
    <br />

    <asp:MultiView ID="mvNilaiHarga" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">Maklumat Penilaian Spesifikasi Teknikal
                            </h3>
                        </div>

                        <div class="panel-body" style="overflow-x: auto">
                            <table class="table table-borderless table-striped">
                                <tr>
                                    <td>ID Naskah Jualan :</td>
                                    <td>
                                        <asp:Label ID="lblIdNJ" runat="server" />
                                        <%--<asp:TextBox ID="txtIdNJ" runat="server" BackColor="#FFFFCC" CssClass="form-control" Width="150px" ReadOnly="true"></asp:TextBox>--%>

                                        &nbsp;&nbsp;&nbsp;Status:
								&nbsp;<asp:Label ID="lblStatus" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>No Perolehan :</td>
                                    <td>
                                        <div class="form-inline">
                                            <asp:Label ID="lblNoPO" runat="server"></asp:Label>

                                        </div>

                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">Tujuan Perolehan :</td>
                                    <td>
                                        <asp:Label ID="lblTujuan" runat="server"></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td>Kategori Perolehan :</td>
                                    <td>
                                        <asp:Label ID="lblKategoriPO" runat="server"></asp:Label>

                                    </td>
                                </tr>
                                <tr>
                                    <td>Kaedah Perolehan :</td>
                                    <td>
                                        <asp:Label ID="lblKaedahPO" runat="server"></asp:Label>

                                    </td>
                                </tr>
                                <tr>
                                    <td>PTJ :</td>
                                    <td>
                                        <asp:Label ID="lblPTjMohon" runat="server"></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td>No Sebut Harga / Tender :</td>
                                    <td>
                                        <asp:Label ID="lblNoSHTD" runat="server"></asp:Label>

                                    </td>
                                </tr>
                                <tr>
                                    <td>Tarikh Tamat Perolehan :</td>
                                    <td>
                                        <asp:Label ID="lbltrkTamatPO" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Tarikh Tamat Pembuka :</td>
                                    <td>
                                        <asp:Label ID="lblTarikhTmtPembuka" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="calendarContainerOverride">
                                    <td>Tarikh Mesyuarat:</td>
                                    <td>
                                        <asp:TextBox ID="txtTarikh" runat="server" CssClass="form-control rightAlign" Width="120px" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
                                        &nbsp;&nbsp;Masa : &nbsp;&nbsp;
                                    <asp:TextBox ID="txtMasa" runat="server" CssClass="form-control" Width="100px" TextMode="Time" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>

                            <br />
                            <asp:GridView ID="gvTeknikal" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText=" Tiada Rekod"
                                CssClass="table table-striped table-bordered table-hover" Width="100%" BorderStyle="Solid" DataKeyNames="PO03_OrderID" Font-Size="8pt">
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="2%">
                                        <ItemTemplate>
                                            <asp:Panel ID="pnlMaster" runat="server">
                                                <asp:Image ID="imgCollapsible" runat="server" ImageUrl="../../../Images/plus.png" Style="margin-right: 5px;" />
                                                <span style="font-weight: bold; display: none;"><%#Eval("PO03_OrderID")%></span>
                                            </asp:Panel>
                                            <ajaxToolkit:CollapsiblePanelExtender ID="ctlCollapsiblePanel" runat="Server" AutoCollapse="False" AutoExpand="False" CollapseControlID="pnlMaster" Collapsed="True" CollapsedImage="../../../Images/plus.png" CollapsedSize="0" ExpandControlID="pnlMaster" ExpandDirection="Vertical" ExpandedImage="../../../Images/minus.png" ImageControlID="imgCollapsible" ScrollContents="false" TargetControlID="pnlChild" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bil" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Kod" DataField="Kod" SortExpression="Kod" ItemStyle-HorizontalAlign="Center" ReadOnly="true" />
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnikID" runat="server" Text='<%# Eval("PO03_UnikID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField HeaderText="Unik ID" DataField="PO03_UnikID" SortExpression="PO03_UnikID" ReadOnly="true" ItemStyle-HorizontalAlign="Center">
									<ItemStyle Width="6%" />
									</asp:BoundField>--%>
                                    <asp:TemplateField HeaderText="Status Hantar" SortExpression="PO03_StatusHantar" ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            Status Terimaan Dokumen
                                        <i class="fas fa-question-circle fa-lg" aria-hidden="true" data-toggle="tooltip" data-placement="right" title="Status Penerimaan Dokumen daripada Vendor"></i>
                                        </HeaderTemplate>
                                        <ItemTemplate><%#IIf(Eval("PO03_StatusHantar"), "Ya", "Tidak")%></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="PO03_MatchHantar" ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            Status Suai Padan
                                        <i class="fas fa-question-circle fa-lg" aria-hidden="true" data-toggle="tooltip" data-placement="bottom" title="Status suai padan antara sistem eVendor dan manual ketika proses pembuka"></i>
                                        </HeaderTemplate>
                                        <ItemTemplate><%#IIf(Eval("PO03_MatchHantar"), "Ya", "Tidak")%></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="checkAll" runat="server" Text="Syor" onclick="checkAll(this);" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" onclick="Check_Click(this)" Checked='<%#IIf(IsDBNull(Eval("PO03_SyorNilaiTek")), False, Eval("PO03_SyorNilaiTek")) %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ulasan Syor" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtUlasan" runat="server" Text='<%#Eval("PO03_UlasanTeknikal") %>' MaxLength="250" TextMode="MultiLine" Style="width: inherit;"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Peratus Spesifikasi" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtKeutamaan" runat="server" Text='<%#Eval("PO03_PeratusNilaiTek") %>' TextMode="Number" Width="50px" CssClass="centerAlign"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtKeutamaan" runat="server" ControlToValidate="txtKeutamaan" ErrorMessage="" ForeColor="Red" Text="*Sila Isi" ValidationGroup="btnSimpan" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </ItemTemplate>
                                        <%--<EditItemTemplate >
                                        <asp:TextBox ID="txtKeutamaan" runat="server" Text='<%#Eval("PO03_PeratusNilaiTek") %>' TextMode="Number" Width="50px" CssClass="centerAlign"></asp:TextBox>
                                    </EditItemTemplate>--%>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbDetail" runat="server" CommandName="Select" CssClass="btn-xs " ToolTip="Maklumat Vendor">
											<i class="fas fa-user-tie fa-lg"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <tr>
                                                <td colspan="100">

                                                    <asp:Panel ID="pnlChild" runat="server" Style="margin-left: 20px; margin-right: 20px; height: 0px; overflow: auto;" Width="95%">

                                                        <asp:GridView ID="gvChild" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="false" EmptyDataText=" Tiada rekod"
                                                            CssClass="table table-bordered table-hover" Width="100%" BorderStyle="Solid" HeaderStyle-BackColor="#FECB18" Font-Size="8pt"
                                                            DataKeyNames="PO03_ePID">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Bil" ItemStyle-HorizontalAlign="Right">
                                                                    <ItemTemplate>
                                                                        <%# Container.DataItemIndex + 1 %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="PO01_Butiran" HeaderText="Barang / Perkara" HeaderStyle-CssClass="centerAlign" />
                                                                <asp:BoundField DataField="PO03_Jenama" HeaderText="Jenama" HeaderStyle-CssClass="centerAlign" NullDisplayText=" " />
                                                                <asp:BoundField DataField="PO03_Model" HeaderText="Model" HeaderStyle-CssClass="centerAlign" NullDisplayText=" " />
                                                                <asp:BoundField DataField="Negara" HeaderText="Negara Pembuat" HeaderStyle-CssClass="centerAlign" />
                                                                <asp:BoundField DataField="PO01_Kuantiti" HeaderText="Kuantiti" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Right" />
                                                                <asp:BoundField DataField="Ukuran" HeaderText="Ukuran" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center" />
                                                            </Columns>
                                                        </asp:GridView>

                                                    </asp:Panel>

                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <ItemStyle Width="1%" />
                                    </asp:TemplateField>
                                </Columns>
                                <%--<HeaderStyle BackColor="#6699FF" />--%>
                            </asp:GridView>
                            <%--<asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Sila pilih salah satu syor."
    ClientValidationFunction="Validate" ForeColor="Red" ValidationGroup="btnSimpan"></asp:CustomValidator>
                        <br />--%>
                            <br />
                            <div style="text-align: center;">
                                <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn" ToolTip="Simpan & Seterusnya" OnClientClick="return Validate2();" ValidationGroup="btnSimpan">
						        <i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                                </asp:LinkButton>

                            </div>
                            <br />
                            <div class="panel panel-default" style="width: inherit">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Senarai Kehadiran Penilai Teknikal</h3>
                                </div>
                                <div class="panel-body" style="overflow-x: auto">


                                    <asp:GridView ID="gvJawatanKuasa" runat="server" AllowPaging="false" AllowSorting="True" AutoGenerateColumns="False" BorderStyle="Solid" CssClass="table table-striped table-bordered table-hover"
                                        EmptyDataText=" Tiada Rekod" ShowFooter="false" ShowHeaderWhenEmpty="True" Width="100%" DataKeyNames="PO06_NilaiTeknikalDtID" Font-Size="8pt">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Bil" ItemStyle-Width="2%" HeaderStyle-CssClass="centerAlign">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="No Staf" DataField="PO06_StafID" SortExpression="PO06_StafID" ReadOnly="true" ItemStyle-Width="5%" />
                                            <asp:BoundField HeaderText="Nama" DataField="PO06_NamaStaf" SortExpression="PO06_NamaStaf" ReadOnly="true" ItemStyle-Width="30%" />
                                            <asp:BoundField HeaderText="PTJ" DataField="PO06_KodPTJStaf" SortExpression="PO06_KodPTJStaf" ReadOnly="true" ItemStyle-Width="8%" />
                                            <asp:BoundField HeaderText="Jawatan" DataField="PO06_JawStaf" SortExpression="Jawatan" ReadOnly="true" ItemStyle-Width="20%" />
                                            <asp:BoundField HeaderText="Email" DataField="PO06_EmailStaf" SortExpression="PO06_EmailStaf" ReadOnly="true" ItemStyle-Width="10%" />
                                            <asp:BoundField HeaderText="Sah?" DataField="Sah" SortExpression="Sah" ReadOnly="true" ItemStyle-Width="5%" />
                                            <asp:TemplateField HeaderText="Kod Pengesahan" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtUnikId" runat="server" Width="60px" TextMode="Number" MaxLength="4"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tidak Hadir?" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="2%">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chxTidakHadir" runat="server" onclick="Check_Click(this)" Checked='<%#IIf(IsDBNull(Eval("PO06_TidakHadir")), False, Eval("PO06_TidakHadir")) %>' ToolTip="Sila check jika tidak hadir" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="4%" ItemStyle-HorizontalAlign="Center" HeaderText="Tindakan">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnSave" runat="server" CommandName="Update" CssClass="btn-xs" ToolTip="Simpan">
												<i class="far fa-save fa-lg"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <br />
                                    <div style="text-align: center;">
                                        <asp:LinkButton ID="lbtnRefresh" runat="server" CssClass="btn-lg" ToolTip="Refresh" OnClientClick="reloadPage()">
						        <i class="fas fa-sync-alt fa-lg"></i>
                                        </asp:LinkButton>

                                    </div>
                                </div>
                            </div>

                        </div>


                        <div class="row">

                            <div class="col-md-12" style="text-align: right">
                                <asp:LinkButton ID="lbtnNextView1" runat="server" CssClass="btn btn-default btn-circle btn-lg" ToolTip="Seterusnya">
                                <i class="glyphicon glyphicon-chevron-right"></i>
                                </asp:LinkButton>

                            </div>
                        </div>
                    </div>

                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="lbtnNextView1" />
                </Triggers>
            </asp:UpdatePanel>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Lampiran Penilaian Teknikal
                    </h3>
                </div>
                <div class="panel-body">
                    <table style="width: 100%" class="table table-borderless">
                        <tr>
                            <td style="width: 20%; vertical-align: top">
                                <asp:Label ID="Label27" runat="server" CssClass="control-label" Text="Lampiran"></asp:Label>
                                &nbsp;:</td>
                            <td>Jenis-jenis dokumen:
                                     <br />
                                <asp:RadioButtonList ID="rbDokumenType" runat="server">
                                    <asp:ListItem Text="Laporan penilaian spesifikasi teknikal" Value="PTek" />
                                </asp:RadioButtonList>
                                <br />
                                <div class="form-inline">
                                    <asp:FileUpload ID="FileUpload1" runat="server" BackColor="#FFFFCC" CssClass="form-control" Height="25px" Width="50%" />
                                    &nbsp;&nbsp;
                                         <asp:LinkButton ID="lbtnUploadLamp" runat="server" CssClass="btn btn-info" ToolTip="Upload file" ValidationGroup="btnUpload">
											<i class="fa fa-upload fa-lg"></i>&nbsp;&nbsp;&nbsp;Upload
                                         </asp:LinkButton>
                                </div><label>Saiz fail muat naik maksimum 5 mb</label>
                                <br />
                                <asp:Label ID="LabelMessage1" runat="server" />
                                <br />
                                <asp:GridView ID="gvLampiran" runat="server" AutoGenerateColumns="false" BorderColor="#333333" BorderStyle="Solid" ShowHeaderWhenEmpty="true" Font-Size="8pt"
                                    CssClass="table table-striped table-bordered table-hover" DataKeyNames="PO13_ID" EmptyDataText=" Tiada rekod" HeaderStyle-BackColor="#6699FF" Width="80%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Bil" ItemStyle-Width="2%">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="PO13_NamaDok" HeaderText="File Name" ItemStyle-Width="40%" />
                                        <asp:BoundField DataField="JenisDok" HeaderText="Type" ItemStyle-Width="25%" />
                                        <asp:BoundField DataField="PO13_NoMohon" HeaderText="" ItemStyle-Width="1%" Visible="false" />
                                        <asp:HyperLinkField DataNavigateUrlFields="PO13_NoMohon,PO13_NamaDok" DataNavigateUrlFormatString="~/Upload/Document/PO/PenilaianTeknikal/{0}/{1}" DataTextField="PO13_ID" DataTextFormatString="&lt;img src='../../../Images/search.png' alt='View' /&gt;" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%" Target="_blank" />
                                        <asp:TemplateField ItemStyle-Width="3%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="delete" CssClass="btn-xs" ToolTip="Delete">
														<i class="fas fa-trash-alt fa-lg"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>

                    <br />

                    <div class="row">
                        <div class="col-md-2" style="text-align: left">
                            <asp:LinkButton ID="lbtnPrevView2" runat="server" CssClass="btn btn-default btn-circle btn-lg" ToolTip="Sebelumnya">
                                <i class="glyphicon glyphicon-chevron-left"></i>
                            </asp:LinkButton>
                        </div>
                        <div class="col-md-10" style="text-align: center">
                            <asp:LinkButton ID="lbtnHantar" runat="server" CssClass="btn btn-info" ValidationGroup="lbtnHantar" OnClientClick="return confirm('Anda pasti untuk hantar rekod ini?');">
                                        <i class="fab fa-telegram-plane fa-lg"></i>&nbsp;&nbsp;&nbsp;Hantar
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>

        </asp:View>
    </asp:MultiView>

</asp:Content>

