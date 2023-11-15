<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="MaklumatAnggaranHarga.aspx.vb" Inherits="SMKB_Web_Portal.MaklumatAnggaranHarga" EnableEventValidation="False" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Perubahan Anggaran Harga Pada Maklumat Permohonan Perolehan</h1>
    <link rel="stylesheet" type="text/css" href="../../../Content/Site.css">

    <script type="text/javascript">
        //function CheckForDataSaving(msg) {
        //	var result = window.confirm(msg);
        //	if (!result) {
        //		alert("False")
        //		return (false);
        //	}
        //	else {
        //		alert("true");
        //	}
        //};

        //debugger;
        //function ConfirmNextView2(msg) {
        //	var result = window.confirm(msg);        
        //	if (result) {            


        //	}
        //};

        function upper(ustr) {
            var str = ustr.value;
            ustr.value = str.toUpperCase();
        };

        function lower(ustr) {
            var str = ustr.value;
            ustr.value = str.toLowerCase();
        };

        function Calculate() {
            var kuantiti = $("#<%=txtKuantiti.ClientID%>").val();
            var AngHargaUnit = $("#<%=txtAngHrgSeunitBaru.ClientID%>").val();
            var totalAmount = 0.00;

            if (kuantiti != "" && AngHargaUnit != "") {
                var amount = (AngHargaUnit * kuantiti);
                totalAmount = Comma(amount);

                $("#<%=txtJumAngHrgBaru.ClientID%>").val(totalAmount);
             }
         };

         function Comma(Num) { //function to add commas to textboxes
             Num += '';
             Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
             Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
             x = Num.split('.');
             x1 = x[0];
             x2 = x.length > 1 ? '.' + x[1] : '';
             var rgx = /(\d+)(\d{3})/;
             while (rgx.test(x1))
                 x1 = x1.replace(rgx, '$1' + ',' + '$2');
             return x1 + x2;
         };

    </script>


    <div class="row">
        <asp:LinkButton ID="lbtnKembali" runat="server" CssClass="btn btn-info">
			<i class="fa fa-arrow-left fa-lg"></i>&nbsp;&nbsp;&nbsp;Kembali
        </asp:LinkButton>
    </div>
    <br />

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>


            <div class="panel panel-default" style="width: 95%;">
                <div class="panel-heading">
                    <h3 class="panel-title">Maklumat Perolehan
                    </h3>
                </div>
                <div class="panel-body" style="overflow: auto;">
                    <div class="table-responsive">
                        <table style="width: 100%" class="table table-borderless">
                            <tbody>

                                <tr>
                                    <td style="width: 20%;">No Permohonan:</td>
                                    <td>
                                        <asp:TextBox ID="txtNoMohon" runat="server" BackColor="#FFFFCC" ReadOnly="true" CssClass="form-control" Width="200px"></asp:TextBox>
                                        &nbsp;&nbsp;&nbsp;Tarikh:
									&nbsp;<asp:Label ID="lblTarikhPO" runat="server"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;Status:
									&nbsp;<asp:Label ID="lblStatus" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%;">No Perolehan:</td>
                                    <td>
                                        <asp:TextBox ID="txtNoPO" runat="server" BackColor="#FFFFCC" ReadOnly="true" CssClass="form-control" Width="200px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%;">Tahun Perolehan:</td>
                                    <td>
                                        <asp:Label ID="lblTahun" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; width: 20%;"><b>Tajuk / Tujuan</b>:</td>
                                    <td>
                                        <asp:Label ID="lblTujuan" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; width: 20%;">
                                        <label class="control-label" for=""><b>Scope</b>:</label></td>
                                    <td>
                                        <asp:Label ID="lblScope" runat="server"></asp:Label>

                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%;">Kategori Perolehan:</td>
                                    <td class="col-sm-8">

                                        <asp:DropDownList ID="ddlkategoriPO" runat="server" AutoPostBack="True" CssClass="form-control" Enabled="false" BackColor="#FFFFCC"></asp:DropDownList>


                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%;">PTJ:</td>
                                    <td class="col-sm-8">

                                        <asp:DropDownList ID="ddlPTJPemohon" runat="server" AutoPostBack="True" CssClass="form-control" Enabled="false" BackColor="#FFFFCC"></asp:DropDownList>

                                    </td>
                                </tr>


                            </tbody>
                        </table>
                    </div>
                    <br />


                    <div class="panel panel-default" style="width: inherit">
                        <div class="panel-heading">
                            <h3 class="panel-title">Maklumat Bajet dan Spesifikasi
                            </h3>
                        </div>
                        <div class="panel-body">


                            <table class="table table-borderless" runat="server" id="tblSpek">

                                <tr>
                                    <td>
                                        <label class="control-label" for="">Kumpulan Wang</label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlKW" runat="server" CssClass="form-control" Enabled="false" BackColor="#FFFFCC"></asp:DropDownList>

                                    </td>
                                </tr>
                                <tr>
                                    <td>Operasi</td>
                                    <td>
                                        <asp:DropDownList ID="ddlKodOperasi" runat="server" CssClass="form-control" Enabled="false" BackColor="#FFFFCC"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label class="control-label" for="">PTj</label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlPTj" runat="server" CssClass="form-control" Enabled="false" BackColor="#FFFFCC"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Projek Komited</td>
                                    <td>
                                        <asp:DropDownList ID="ddlKodProjek" runat="server" CssClass="form-control" Enabled="false" BackColor="#FFFFCC"></asp:DropDownList>

                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <label class="control-label" for="">Vot Lanjut</label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlVotLanjut" runat="server" Width="95%" CssClass="form-control" Enabled="false" BackColor="#FFFFCC"></asp:DropDownList>


                                    </td>
                                </tr>


                                <tr>
                                    <td>
                                        <label class="control-label" for="">Baki Peruntukan (RM)</label></td>
                                    <td>
                                        <asp:TextBox ID="txtBakiPeruntukan" runat="server" CssClass="form-control rightAlign" BackColor="#FFFFCC" Width="150px" Enabled="false"></asp:TextBox>

                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <label class="control-label" for="">Barang / Perkara</label></td>
                                    <td>
                                        <asp:TextBox ID="txtPerkara" runat="server" Style="width: 90%; height: auto; min-height: 50px;" TextMode="MultiLine" CssClass="form-control" onkeyup="upper(this)" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>

                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label class="control-label" for="">Kuantiti</label></td>
                                    <td>
                                        <asp:TextBox ID="txtKuantiti" runat="server" TextMode="Number" CssClass="form-control rightAlign" Width="100px" AutoPostBack="true" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>

                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label class="control-label" for="">Ukuran</label></td>
                                    <td>
                                        <asp:TextBox ID="txtUkuran" runat="server" Style="width: 100px;" CssClass="form-control" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>

                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label class="control-label" runat="server">Anggaran Harga Seunit (RM)</asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="txtAngHrgSeunit" runat="server" CssClass="form-control rightAlign" Width="150px" AutoPostBack="true" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>

                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label class="control-label" runat="server">Jumlah Anggaran Harga (RM)</asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="txtJumAngHrg" runat="server" Enabled="false" CssClass="form-control rightAlign" Width="150px" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label class="control-label" runat="server">Perubahan Anggaran Harga Seunit (RM)</asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="txtAngHrgSeunitBaru" runat="server" CssClass="form-control rightAlign" Width="150px" onchange='Calculate();'></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="rfvtxtAngHrgSeunitBaru" ControlToValidate="txtAngHrgSeunitBaru" runat="server" ErrorMessage=" Hanya format ringgit dibenarkan" ValidationExpression="(?=.)^\$?(([1-9][0-9]{0,2}(,[0-9]{3})*)|[0-9]+)?(\.[0-9]{1,2})?$" ValidationGroup="JumHarga" ForeColor="Red"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label class="control-label" runat="server">Perubahan Jumlah Anggaran Harga (RM)</asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="txtJumAngHrgBaru" runat="server" Enabled="false" CssClass="form-control rightAlign" BackColor="#FFFFCC" Width="150px" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 40px; text-align: center;" colspan="2">

                                        <asp:Button ID="btnEdit" runat="server" CssClass="btn" Text="Kemaskini" ValidationGroup="btnTambahButiran" ToolTip="Kemaskini" />
                                        &nbsp;&nbsp;
								<asp:LinkButton ID="lbtnUndo" runat="server" CssClass="btn btn-info" ToolTip="Undo">
									<i class="fas fa-undo fa-lg"></i>&nbsp;&nbsp;&nbsp;Undo
                                </asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                            <asp:GridView ID="gvDtPembelian" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="true" EmptyDataText=" Tiada rekod"
                                CssClass="table table-striped table-bordered table-hover" Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" Width="100%" ShowFooter="True" PageSize="20">
                                <Columns>
                                    <asp:TemplateField HeaderText="Bil">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBil" runat="server" Text='<%# Eval("PO01_Bil")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("PO01_DtID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="KW" SortExpression="KodKw" ItemStyle-HorizontalAlign="Center" DataField="KodKw"></asp:BoundField>
                                    <asp:BoundField HeaderText="Kod Operasi" SortExpression="KodKo" ItemStyle-HorizontalAlign="Center" DataField="KodKo"></asp:BoundField>
                                    <asp:BoundField HeaderText="PTJ" SortExpression="KodPtj" ItemStyle-HorizontalAlign="Center" DataField="KodPtj"></asp:BoundField>
                                    <asp:BoundField HeaderText="Kod Projek" SortExpression="KodKp" ItemStyle-HorizontalAlign="Center" DataField="KodKp"></asp:BoundField>
                                    <asp:BoundField HeaderText="Vot Lanjut" SortExpression="KodVot" ItemStyle-HorizontalAlign="Center" DataField="KodVot"></asp:BoundField>
                                    <asp:BoundField HeaderText="Barang / Perkara" SortExpression="PO01_Butiran" DataField="PO01_Butiran"></asp:BoundField>
                                    <asp:BoundField HeaderText="Kuantiti" SortExpression="PO01_Kuantiti" ItemStyle-HorizontalAlign="Center" DataField="PO01_Kuantiti"></asp:BoundField>
                                    <asp:BoundField HeaderText="Ukuran" SortExpression="NamaUkuran" ItemStyle-HorizontalAlign="Center" DataField="NamaUkuran"></asp:BoundField>
                                    <asp:BoundField HeaderText="Anggaran Harga Seunit (RM)" SortExpression="PO01_KadarHarga" ItemStyle-HorizontalAlign="Right" DataField="PO01_KadarHarga" DataFormatString="{0:N}"></asp:BoundField>
                                    <asp:BoundField HeaderText="Jumlah Anggaran Harga (RM)" SortExpression="PO01_JumKadar" ItemStyle-HorizontalAlign="Right" DataField="PO01_JumKadar" DataFormatString="{0:N}"></asp:BoundField>
                                    <asp:BoundField HeaderText="Anggaran Harga Seunit Baru (RM)" SortExpression="PO01_KadarHargaBaru" ItemStyle-HorizontalAlign="Right" DataField="PO01_KadarHargaBaru" DataFormatString="{0:N}" NullDisplayText=" "></asp:BoundField>
                                    <asp:BoundField HeaderText="Jumlah Anggaran Harga Baru (RM)" SortExpression="PO01_JumKadarBaru" ItemStyle-HorizontalAlign="Right" DataField="PO01_JumKadarBaru" DataFormatString="{0:N}" NullDisplayText=" "></asp:BoundField>

                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>

                                            <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CssClass="btn-xs" ToolTip="Pilih Kemaskini">
											<i class="far fa-edit fa-lg"></i>
                                            </asp:LinkButton>

                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />

                                    </asp:TemplateField>
                                </Columns>
                                <SelectedRowStyle ForeColor="Blue" />
                            </asp:GridView>
                            <br />
                            <div style="text-align: center">
                                <asp:LinkButton ID="lbtnBatal" runat="server" CssClass="btn btn-info" ToolTip="Batal">
							<i class="fas fa-undo fa-lg"></i>&nbsp;&nbsp;&nbsp;Batal
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>


                </div>
                <br />

                <div class="row">
                    <table style="width: 100%" class="table table-borderless table-striped">
                        <tr id="trAngHrg" runat="server">
                            <td style="vertical-align: top; width: 15%;">
                                <b>Ulasan Perubahan Anggaran Harga</b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtUlasanAngHrg" runat="server" TextMode="MultiLine" Width="90%" Rows="3" MaxLength="500"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtUlasanAngHrg" runat="server" ControlToValidate="txtUlasanAngHrg" ErrorMessage="" ForeColor="Red" Text="*Sila isi" Display="Dynamic" ValidationGroup="lbtnHantar"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="trBajet" runat="server" visible="false">
                            <td style="vertical-align: top; width: 15%;">
                                <b>Bajet</b>
                            </td>
                            <td>
                                <asp:GridView ID="gvBajet" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="true" EmptyDataText=" Tiada rekod"
                                    CssClass="table table-striped table-bordered table-hover" HeaderStyle-BackColor="#6699FF" Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" Width="100%" ShowFooter="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Bil" ItemStyle-Width="2%">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="KW" HeaderText="KW" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="KO" HeaderText="KO" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="PTJ" HeaderText="PTJ" ItemStyle-Width="4%" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="KP" HeaderText="KP" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="VotSebagai" HeaderText="Vot Sebagai" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="BakiSbnr" HeaderText="Baki Sebenar (RM)" DataFormatString="{0:n2}" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="BljSdhLulus" HeaderText="Jum. Blja. Sudah Lulus (RM)" DataFormatString="{0:n2}" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="BakiByg" HeaderText="Baki Peruntukan Semasa (RM)" DataFormatString="{0:n2}" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="BljBlmLulus" HeaderText="Jum. Blja. Belum Lulus (RM)" DataFormatString="{0:n2}" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="BezaAngHrg" HeaderText="Jum. Beza Ang. Harga (RM)" DataFormatString="{0:n2}" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right" />

                                    </Columns>

                                </asp:GridView>
                                <div style="margin-bottom: 10px;" id="dvPetunjuk" runat="server" visible="false">
                                    <div>
                                        <i class="fas fa-info-circle fa-lg"></i>
                                        <asp:Label ID="Label3" runat="server" Text="Label" Font-Bold="true">Petunjuk :</asp:Label>
                                        <br />
                                    </div>
                                    <div>
                                        <span style="color: #FFFF00;"><i class="far fas fa-square-full fa-lg"></i></span>- Alert<br />
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr id="trKelulusanKetuaPtj" runat="server" visible="false">
                            <td style="vertical-align: top; width: 15%;">
                                <b>Kelulusan Ketua PTj</b>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rbKelulusan" runat="server" Height="25px" Width="273px" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rbKelulusan_SelectedIndexChanged">
                                    <asp:ListItem Text=" <b>Lulus</b>" Value="1" />
                                    <asp:ListItem Text=" <b>Tidak Lulus</b>" Value="0" />
                                </asp:RadioButtonList>
                                <div id="divUlasan" runat="server" visible="false">
                                    <label class="control-label" for="Ulasan"><b>Ulasan Tidak Lulus</b></label>
                                    <br />
                                    <asp:TextBox ID="txtUlasan" runat="server" TextMode="MultiLine" Width="90%" Rows="3" MaxLength="500"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvUlasan" runat="server" ControlToValidate="txtUlasan" ErrorMessage="" ForeColor="Red" Text="*Sila isi" Display="Dynamic" ValidationGroup="vgTidakLulus"></asp:RequiredFieldValidator>
                                </div>

                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12" style="text-align: center">
                        <asp:LinkButton ID="lbtnHantar" runat="server" CssClass="btn btn-info" ValidationGroup="lbtnHantar" OnClientClick="return confirm('Anda pasti untuk hantar rekod ini?');">
                                <i class="fab fa-telegram-plane fa-lg"></i>&nbsp;&nbsp;&nbsp;Hantar
                        </asp:LinkButton>
                        &nbsp;                    
						
                    </div>

                </div>

                <br />
            </div>
            <p></p>

            <div class="panel panel-default">
                <div class="panel-heading">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" href="#collapse1"><span class="fas fa-users fa-lg"></span>&nbsp&nbsp Pengguna</a>
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
                                            &nbsp&nbsp Pemohon
                                            <br />
                                            &nbsp&nbsp<asp:TextBox ID="txtNamaPemohon" runat="server" CssClass="form-control" ReadOnly="true" Width="45%" ToolTip="Nama Pemohon"></asp:TextBox>
                                            &nbsp<asp:TextBox ID="txtJawPemohon" runat="server" CssClass="form-control" ReadOnly="true" Width="50%" ToolTip="Jawatan Pemohon"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="row">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 5%;">
                                        <button type="button" id="btnPenyokong" class="btn btn-default btn-circle btn-lg" data-toggle="collapse" data-target="#Penyokong" title="Penyokong"><span class="fa fa-user"></span></button>
                                    </td>
                                    <td style="width: 80%;">
                                        <div id="Penyokong" class="collapse in">
                                            &nbsp&nbsp Penyokong
                                            <br />
                                            &nbsp&nbsp<asp:TextBox ID="txtNamaSokong" runat="server" CssClass="form-control" ReadOnly="true" Width="45%" ToolTip="Nama Penyemak"></asp:TextBox>
                                            &nbsp<asp:TextBox ID="txtJawSokong" runat="server" CssClass="form-control" ReadOnly="true" Width="50%" ToolTip="Jawatan Penyemak"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="row">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 5%;">
                                        <button type="button" id="btnPengesah" class="btn btn-default btn-circle btn-lg" data-toggle="collapse" data-target="#Pengesah" title="Pengesah"><span class="fa fa-user"></span></button>
                                    </td>
                                    <td style="width: 80%;">
                                        <div id="Pengesah" class="collapse in">
                                            &nbsp&nbsp Pelulus
                                            <br />
                                            &nbsp&nbsp<asp:TextBox ID="txtNamaPelulus" runat="server" CssClass="form-control" ReadOnly="true" Width="45%" ToolTip="Nama Pelulus"></asp:TextBox>
                                            &nbsp<asp:TextBox ID="txtJawPelulus" runat="server" CssClass="form-control" ReadOnly="true" Width="50%" ToolTip="Jawatan Pelulus"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="row">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 5%;">
                                        <button type="button" id="btnPenyemak" class="btn btn-default btn-circle btn-lg" data-toggle="collapse" data-target="#Penyokong" title="Penyokong"><span class="fa fa-user"></span></button>
                                    </td>
                                    <td style="width: 80%;">
                                        <div id="Penyemak" class="collapse in">
                                            &nbsp&nbsp Penyemak
                                            <br />
                                            &nbsp&nbsp<asp:TextBox ID="txtNamaPenyemak" runat="server" CssClass="form-control" ReadOnly="true" Width="45%" ToolTip="Nama Penyemak"></asp:TextBox>
                                            &nbsp<asp:TextBox ID="txtJawPenyemak" runat="server" CssClass="form-control" ReadOnly="true" Width="50%" ToolTip="Jawatan Penyemak"></asp:TextBox>
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
