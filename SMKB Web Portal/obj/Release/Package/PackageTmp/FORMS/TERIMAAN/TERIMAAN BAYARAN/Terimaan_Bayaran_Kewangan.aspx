<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Terimaan_Bayaran_Kewangan.aspx.vb" Inherits="SMKB_Web_Portal.Terimaan_Bayaran_Kewangan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>
    <%--<script>--%>


    <script type="text/javascript">

        function RadioCheck(rb) {
            //var gv = objRef.parentNode.parentNode.parentNode;
            var gv = document.getElementById("<%=gvBil.ClientID%>");
            var rbs = gv.getElementsByTagName("input");
            var row = rb.parentNode.parentNode;

            for (var i = 0; i < rbs.length; i++) {
                if (rbs[i].type == "radio") {
                    if (rbs[i].checked && rbs[i] != rb) {
                        rbs[i].checked = false;
                        break;
                    }
                }
            }
        };

        function fClosebil() {
            $find("mpe2").hide();
        };

        function fClosePendahuluan() {
            $find("mpe3").hide();
        };

        function fClosePinjaman() {
            $find("mpe4").hide();
        };

        function fCloseWPR() {
            $find("mpe5").hide();
        };

        function fCloseSiswa() {
            $find("mpe6").hide();
        };

        function fCloseSiswaPG() {
            $find("mpe7").hide();
        };

        function upper(ustr) {
            var str = ustr.value;
            ustr.value = str.toUpperCase();
        };

        function lower(ustr) {
            var str = ustr.value;
            ustr.value = str.toLowerCase();
        };


    </script>
    <%--<div class="row">
				<asp:LinkButton ID="lbtnKembali" runat="server" CssClass="btn btn-info">
					<i class="fa fa-arrow-left fa-lg"></i>&nbsp;&nbsp;&nbsp;Kembali
				</asp:LinkButton>
			</div>
			<br />--%>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <div class="row">

                <div class="panel panel-default">

                    <div class="panel-heading">Maklumat Penerima</div>
                    <div class="panel-body">
                        <table>
                            <tr>
                                <td style="width: 106px">Tahun</td>
                                <td>:</td>
                                <td>
                                    <asp:Label ID="lblTahun" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 106px">Tarikh Terima</td>
                                <td>:</td>
                                <td>
                                    <asp:Label ID="lblTkhMhn" runat="server"></asp:Label>
                                </td>

                            </tr>
                            <tr id="trMasa" runat="server" visible="false">
                                <td style="width: 106px">Masa</td>
                                <td>:</td>
                                <td>
                                    <asp:Label ID="lblMasa" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <%--<tr>
                                    <td style="width: 106px">PTJ</td>
                                    <td>:</td>
                                    <td>
                                        <asp:label ID="lblKodPTJ" runat="server" ></asp:label>
                                        &nbsp;-&nbsp;<asp:label ID="lblNamaPTJ" runat="server"></asp:label>
                                        &nbsp;</td>
                                </tr>--%>
                            <tr>
                                <td style="width: 106px; height: 23px;">Nama Penerima</td>
                                <td style="height: 23px">:</td>
                                <td style="height: 23px">
                                    <asp:Label ID="lblNoPmhn" runat="server"></asp:Label>
                                    &nbsp;-&nbsp;<asp:Label ID="lblNamaPemohon" runat="server"></asp:Label>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 106px">Jawatan</td>
                                <td>:</td>
                                <td>
                                    <asp:Label ID="lblJawatan" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 106px">Jenis Terimaan</td>
                                <td>:</td>
                                <td>
                                    <asp:Label ID="lblJnsTerimaan" runat="server"></asp:Label>
                                </td>
                            </tr>

                        </table>
                    </div>
                </div>
            </div>

            <div class="row">

                <div class="panel panel-default">
                    <div class="panel-heading">
                        Maklumat Terimaan
                    </div>
                    <div class="panel-body">
                        <table>
                            <tr>
                                <td style="width: 12%;">No Resit :</td>
                                <td style="width: 40%;">
                                    <asp:TextBox ID="txtNoResit" runat="server" BackColor="#FFFFCC" ReadOnly="true" CssClass="form-control" Width="100%"></asp:TextBox>
                                    <asp:HiddenField ID="hdNoDok" runat="server" />
                                    <asp:HiddenField ID="hdNoDokSem" runat="server" />
                                </td>
                                <td style="width: 2%;">&nbsp;</td>
                                <td style="width: 8%; vertical-align: top;" rowspan="2">Alamat :</td>
                                <td style="width: 32%;">
                                    <asp:TextBox ID="txtAlamat1" runat="server" CssClass="form-control" Width="100%" ReadOnly="true" TextMode="MultiLine" Rows="2"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Jenis Urusniaga :</td>
                                <td>
                                    <asp:DropDownList ID="ddlJnsUrusniaga" runat="server" AutoPostBack="True" CssClass="form-control" Width="100%"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlJnsUrusniaga" runat="server" ControlToValidate="ddlJnsUrusniaga" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="lbtnHantar" Display="Dynamic"></asp:RequiredFieldValidator></td>
                                <td style="width: 2%">&nbsp;</td>

                                <td>
                                    <asp:TextBox ID="txtAlamat2" runat="server" CssClass="form-control" Width="100%" ReadOnly="true"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Kategori Pembayar :</td>
                                <td>
                                    <asp:DropDownList ID="ddlKtgPembayar" runat="server" AutoPostBack="True" CssClass="form-control" Width="100%"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlKtgPembayar" runat="server" ControlToValidate="ddlKtgPembayar" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="lbtnHantar" Display="Dynamic"></asp:RequiredFieldValidator></td>
                                <td style="width: 2%">&nbsp;</td>
                                <td>Bandar :</td>
                                <td>
                                    <asp:TextBox ID="txtBandar" runat="server" CssClass="form-control" Width="50%" ReadOnly="true"></asp:TextBox>
                                    &nbsp;&nbsp;Poskod :&nbsp;&nbsp;
                                        <asp:TextBox ID="txtPoskod" runat="server" CssClass="form-control" Width="60px" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Pembayar :</td>
                                <td>
                                    <asp:DropDownList ID="ddlPembayar" runat="server" AutoPostBack="True" CssClass="form-control" Width="85%"></asp:DropDownList>
                                                        
                                    <asp:LinkButton ID="lbtnTambahOA" runat="server" CssClass="btn-circle" ToolTip="Tambah Orang Awam" Visible="false">
                                            <i class="fas fa-plus-square fa-lg" aria-hidden="true"></i>                                                
                                    </asp:LinkButton>

                           <asp:TextBox ID="txtPembayar" runat="server" CssClass="form-control" Width="350px" Enabled="false" Visible="false"></asp:TextBox>                            
                            &nbsp;&nbsp;
                                     <asp:LinkButton ID="lbtnOpenMpe" runat="server" CssClass="btnNone" ToolTip="Cari pembayar" style="padding:4px;background-color: lightgrey;">
                        <i class="fa fa-search btn-xs" aria-hidden="true"></i>
                               </asp:LinkButton>

                                </td>
                                <td style="width: 2%">&nbsp;</td>
                                <td>Negeri :</td>
                                <td>
                                    <asp:TextBox ID="txtNegeri" runat="server" ReadOnly="true" CssClass="form-control" Width="100%"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Mod Terimaan :</td>
                                <td>
                                    <asp:DropDownList ID="ddlModTerimaan" runat="server" AutoPostBack="True" CssClass="form-control" Width="100%"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlModTerimaan" runat="server" ControlToValidate="ddlModTerimaan" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="lbtnHantar" Display="Dynamic"></asp:RequiredFieldValidator></td>
                                <td style="width: 2%">&nbsp;</td>
                                <td>Negara :</td>
                                <td>
                                    <asp:TextBox ID="txtNegara" runat="server" CssClass="form-control" Width="100%" ReadOnly="true"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <%--<td>Bank UTeM :</td>
                                    <td><asp:DropDownList ID="ddlBank" runat="server" AutoPostBack="True" CssClass="form-control" Width="100%"></asp:DropDownList>
									<asp:RequiredFieldValidator ID="rfvddlBankUtem" runat="server" ControlToValidate="ddlBank" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="lbtnHantar" Display="Dynamic" ></asp:RequiredFieldValidator></td>--%>
                                <td rowspan="2">Tujuan :</td>
                                <td rowspan="2">
                                    <asp:TextBox ID="txtTujuan" runat="server" TextMode="MultiLine" CssClass="form-control" Width="100%" Rows="4" onkeyup="upper(this)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtTujuan" runat="server" ControlToValidate="txtTujuan" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="lbtnHantar" Display="Dynamic"></asp:RequiredFieldValidator></td>
                                <td style="width: 2%">&nbsp;</td>
                                <td>No Telp :</td>
                                <td>
                                    <asp:TextBox ID="txtNoTel" runat="server" CssClass="form-control" Width="100px" ReadOnly="true"></asp:TextBox>
                                    <label id="lblMsgNoTel" class="control-label" for="" style="display: none; color: #820303;">
                                        (Masukkan No.Tel)
                                    </label>
                                    &nbsp; &nbsp; No.Fax/ No Telp (R) &nbsp;:&nbsp; &nbsp; 
                                    <asp:TextBox ID="txtNoFax" runat="server" CssClass="form-control" Style="width: 100px;" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>

                                <td style="width: 2%">&nbsp;</td>
                                <td>Email :</td>
                                <td>
                                    <asp:TextBox ID="txtEmel" runat="server" CssClass="form-control" Width="100%" TextMode="Email" ReadOnly="true"></asp:TextBox>
                                    <asp:HiddenField ID="hdICStaf" runat="server" />
                                </td>
                            </tr>
                        </table>
                        <br />

                        <div class="panel panel-default" style="width: auto; overflow-x: auto;">
                            <div class="panel-heading">
                                Butiran
                            </div>
                            <div class="panel-body">


                                <asp:GridView ID="gvTerimaan" datakeyname="" runat="server" AllowSorting="true" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText=" Tiada rekod"
                                    CssClass="table table-striped table-bordered table-hover" Font-Size="8pt" BorderStyle="Solid" ShowFooter="True" Width="100%" DataKeyNames="ID">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Bil" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="KW" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblKw" runat="server" Text='<%# IIf(IsDBNull(Eval("kodKw")), "", Eval("kodKw")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlKW" runat="server" CssClass="form-control" Style="width: 45px;" AutoPostBack="true" OnSelectedIndexChanged="ddlKW_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlKWE" runat="server" ControlToValidate="ddlKW" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="lbtnSave" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlKW" runat="server" CssClass="form-control" Style="width: 45px;" AutoPostBack="true" OnSelectedIndexChanged="ddlKW_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlKW" runat="server" ControlToValidate="ddlKW" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="lbtnAdd" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="KO" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblKo" runat="server" Text='<%# IIf(IsDBNull(Eval("kodKo")), "", Eval("kodKo")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlKo" runat="server" CssClass="form-control" Style="width: 45px;" AutoPostBack="true" OnSelectedIndexChanged="ddlKo_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlKoE" runat="server" ControlToValidate="ddlKo" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="lbtnSave" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlKo" runat="server" CssClass="form-control" Style="width: 45px;" AutoPostBack="true" OnSelectedIndexChanged="ddlKo_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlKo" runat="server" ControlToValidate="ddlKo" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="lbtnAdd" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PTj" ItemStyle-Width="4%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPTj" runat="server" Text='<%# IIf(IsDBNull(Eval("KodPTJ")), "", Eval("KodPTJ")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlPTj" runat="server" CssClass="form-control" Style="width: 70px;" AutoPostBack="true" OnSelectedIndexChanged="ddlPTJ_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlPTjE" runat="server" ControlToValidate="ddlPTj" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="lbtnSave" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlPTj" runat="server" CssClass="form-control" Style="width: 70px;" AutoPostBack="true" OnSelectedIndexChanged="ddlPTJ_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlPTj" runat="server" ControlToValidate="ddlPTj" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="lbtnAdd" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="KP" ItemStyle-Width="4%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblKP" runat="server" Text='<%# IIf(IsDBNull(Eval("KodKP")), "", Eval("KodKP")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlKP" runat="server" CssClass="form-control" Style="width: 75px;" AutoPostBack="true" OnSelectedIndexChanged="ddlKp_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlKPE" runat="server" ControlToValidate="ddlKP" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="lbtnSave" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlKP" runat="server" CssClass="form-control" Style="width: 75px;" AutoPostBack="true" OnSelectedIndexChanged="ddlKp_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlKP" runat="server" ControlToValidate="ddlKP" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="lbtnAdd" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vot" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVot" runat="server" Text='<%# IIf(IsDBNull(Eval("KodVot")), "", Eval("KodVot")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlVot" runat="server" CssClass="form-control" Style="width: 65px;" AutoPostBack="true" OnSelectedIndexChanged="ddlVot_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlVotE" runat="server" ControlToValidate="ddlVot" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="lbtnSave" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlVot" runat="server" CssClass="form-control" Style="width: 65px;" AutoPostBack="true" OnSelectedIndexChanged="ddlVot_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlVot" runat="server" ControlToValidate="ddlVot" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="lbtnAdd" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Butiran" ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblButiran" runat="server" Text='<%# IIf(IsDBNull(Eval("RC01_Butiran")), "", Eval("RC01_Butiran")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtButiran" runat="server" CssClass="form-control" TextMode="MultiLine" Style="width: 100%;" Text='<%# Eval("RC01_Butiran") %>' onkeyup="upper(this)"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtButiranE" runat="server" ControlToValidate="txtButiran" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="lbtnSave" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtButiran" runat="server" CssClass="form-control" TextMode="MultiLine" Style="width: 100%;" Rows="1" onkeyup="upper(this)"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtButiran" runat="server" ControlToValidate="txtButiran" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="lbtnAdd" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Debit" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDebit" runat="server" Text='<%# Eval("RC01_Debit", "{0:N2}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblDebitE" runat="server" Text='0.00'></asp:Label>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblDebitF" runat="server" Text='0.00'></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Kredit" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblKredit" runat="server" Text='<%# Eval("RC01_Kredit", "{0:N2}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtKredit" runat="server" CssClass="form-control rightAlign" Style="width: 90px;" Text='<%# Eval("RC01_Kredit", "{0:N2}") %>' TextMode="Number"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtKreditE" runat="server" ControlToValidate="txtKredit" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="lbtnSave" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtKredit" runat="server" CssClass="form-control rightAlign" Style="width: 90px;" TextMode="Number"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtKredit" runat="server" ControlToValidate="txtKredit" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="lbtnAdd" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="4%" ItemStyle-HorizontalAlign="Center" HeaderText="Tindakan" FooterStyle-HorizontalAlign="Center">
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="lbtnSave" runat="server" CommandName="Update" CssClass="btn-xs" ToolTip="Simpan" ValidationGroup="lbtnSave">
												            <i class="far fa-save fa-lg"></i>
                                                </asp:LinkButton>
                                                &nbsp;
											                     <asp:LinkButton ID="lbtnCancel" runat="server" CommandName="Cancel" CssClass="btn-xs" ToolTip="Undo">
												                    <i class="fas fa-undo fa-lg"></i>
                                                                 </asp:LinkButton>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Edit" CssClass="btn-xs" ToolTip="Kemas Kini">
												                <i class="far fa-edit fa-lg"></i>
                                                </asp:LinkButton>
                                                &nbsp;
                                                        <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete" CssClass="btn-xs" ToolTip="Padam"
                                                            OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');">
										                    <i class="far fa-trash-alt fa-lg"></i>
                                                        </asp:LinkButton>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnAdd" runat="server" CssClass="btn-xs" ToolTip="Tambah" OnClick="lbtnAdd_Click" ValidationGroup="lbtnAdd">
												                <i class="fa fa-plus-circle fa-lg"></i>
                                                </asp:LinkButton>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <EmptyDataTemplate>
                                    </EmptyDataTemplate>
                                    <SelectedRowStyle ForeColor="Blue" />
                                </asp:GridView>
                                <div class="row rightAlign" style="text-align: right">
                                    <table class="table table-borderless table-striped">
                                        <tr>
                                            <td>Jumlah Sebenar (RM) :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtJumlahSebenar" runat="server" CssClass="form-control rightAlign" Width="100px" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Jumlah Bayaran (RM) :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtJumBayaran" runat="server" CssClass="form-control rightAlign" Width="100px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="trEFT" runat="server" visible="false">
                                            <td>No EFT :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNoEft" runat="server" CssClass="form-control" Width="100px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtNoEft" runat="server" ControlToValidate="txtNoEft" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="EFT" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <table id="tblCek" runat="server" visible="false" class="table table-borderless table-striped">
                                    <tr>
                                        <td>Bank Pembayar :
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlBankPembayar" runat="server" AutoPostBack="True" CssClass="form-control" Width="100%"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlBankPembayar" runat="server" ControlToValidate="ddlBankPembayar" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="Cek" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>&nbsp</td>
                                        <td>No Cek/ Dokumen :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNoCek" runat="server" CssClass="form-control" Width="100px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtNoCek" runat="server" ControlToValidate="txtNoCek" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="Cek" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Cawangan :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCawBank" runat="server" CssClass="form-control" Width="100px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtCawBank" runat="server" ControlToValidate="txtCawBank" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="Cek" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>&nbsp</td>
                                        <td>Jenis Cek :
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlJnsCek" runat="server" AutoPostBack="True" CssClass="form-control" Width="100%"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlJnsCek" runat="server" ControlToValidate="ddlJnsCek" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="Cek" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Tarikh Cek :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTrkCek" runat="server" CssClass="form-control" Width="100px"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="caltxtTrkCek" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtTrkCek" TodaysDateFormat="dd/MM/yyyy" PopupButtonID="lbtntxtTrkCek" />
                                            <asp:LinkButton ID="lbtntxtTrkCek" runat="server" ToolTip="Klik untuk papar kalendar">
                                                <i class="far fa-calendar-alt fa-lg"></i>
                                            </asp:LinkButton>
                                            <asp:RequiredFieldValidator ID="rfvtxtTrkCek" runat="server" ControlToValidate="txtTrkCek" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="Cek" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>&nbsp</td>
                                        <td>Tarikh Terima Cek :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTrkTerimaCek" runat="server" CssClass="form-control" Text="<%# System.DateTime.Now%>" Width="100px"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CaltxtTrkTerimaCek" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtTrkTerimaCek" TodaysDateFormat="dd/MM/yyyy" PopupButtonID="lbtntxtTrkTerimaCek" />
                                            <asp:LinkButton ID="lbtntxtTrkTerimaCek" runat="server" ToolTip="Klik untuk papar kalendar">
                                                <i class="far fa-calendar-alt fa-lg"></i>
                                            </asp:LinkButton>
                                            <asp:RequiredFieldValidator ID="rfvtxtTrkTerimaCek" runat="server" ControlToValidate="txtTrkTerimaCek" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="Cek" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>



                            </div>
                        </div>
                        <div style="text-align: center">
                            <asp:LinkButton ID="lbtnHantar" runat="server" CssClass="btn btn-info" OnClientClick="return confirm('Anda pasti untuk hantar rekod ini?');" ValidationGroup="lbtnHantar">
                            <i class="fas fa-paper-plane"></i>&nbsp;&nbsp;&nbsp;Hantar
                            </asp:LinkButton>
                            &nbsp;&nbsp;&nbsp;
                                     <asp:LinkButton ID="lbtnCetak" runat="server" CssClass="btn btn-info" ToolTip="Paparkan Resit A" Visible="false">
									<i class="fas fa-print fa-lg">&nbsp; Cetak</i>
                                     </asp:LinkButton>
                        </div>
                        <div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Button ID="btnOpenBilTuntutan" runat="server" Text="" Style="display: none" />
    <ajaxToolkit:ModalPopupExtender ID="MPEBilTuntutan" runat="server" BackgroundCssClass="modalBackground" BehaviorID="mpe2"
        CancelControlID="btnClose1" PopupControlID="pnlBilTuntutan" TargetControlID="btnOpenBilTuntutan" >
    </ajaxToolkit:ModalPopupExtender>

    <asp:Panel ID="pnlBilTuntutan" runat="server" BackColor="White" Width="60%" Style="overflow: auto;">
        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
            <ContentTemplate>
                <table style="border: Solid 3px #D5AA00; width: 100%; height: 100%;">

                    <tr style="background: linear-gradient(to bottom, #fceabb 0%,#fccd4d 50%,#f8b500 51%,#fbdf93 100%);">
                        <td colspan="2" style="height: 10%; font-weight: bold; text-align: center;">Maklumat Lanjut Tuntutan</td>
                        <td style="width: 10px; text-align: center;">
                            <button id="btnClose1" runat="server" class="btnNone " title="Tutup" onclick="fClosebil();">
                                <i class="far fa-window-close fa-2x"></i>
                            </button>
                        </td>
                    </tr>

                    <tr style="height: 30px;">
                        <td colspan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Jumlah rekod :&nbsp;<label id="lblJumRekod" runat="server" class="control-label" style="color: mediumblue;"></label>
                            &nbsp;|&nbsp;&nbsp;Saiz Rekod : &nbsp;&nbsp;                           
                               <asp:DropDownList ID="ddlSaizRekod" runat="server" AutoPostBack="true" class="form-control" Width="60px">
                                   <asp:ListItem Text="10" Value="10" />
                                   <asp:ListItem Text="25" Value="25" Selected="true" />
                                   <asp:ListItem Text="50" Value="50" />
                                   <asp:ListItem Text="100" Value="100" />
                                   <asp:ListItem Text="200" Value="200" />
                                   <asp:ListItem Text="500" Value="500" />
                                   <asp:ListItem Text="1000" Value="1000" />
                               </asp:DropDownList>

                            &nbsp;&nbsp;
                           Cari : 
                           &nbsp;&nbsp;
                               <asp:TextBox ID="txtCari" runat="server" CssClass="form-control" Width="150px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtCari" runat="server" ControlToValidate="txtCari" Display="Dynamic" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="lbtnCari"></asp:RequiredFieldValidator>
                            &nbsp;
                               <asp:DropDownList ID="ddlCariBil" runat="server" Width="150px" class="form-control">
                                   <asp:ListItem Text="Nama" Value="1" Selected="True" />
                                   <asp:ListItem Text="No Bil" Value="2" />
                                   <asp:ListItem Text="Id Penerima" Value="3" />
                               </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;
                               <asp:LinkButton ID="lbtnCari" runat="server" CssClass="btn-circle" ToolTip="Cari" ValidationGroup="lbtnCari">
                        <i class="fa fa-search fa-2x" aria-hidden="true"></i>
                               </asp:LinkButton>
                        </td>
                    </tr>


                    <tr style="vertical-align: top;">

                        <td colspan="3">
                            <div style="margin-bottom: 10px; margin-top: 10px; margin-left: 10px; margin-right: 10px;">
                                <asp:GridView ID="gvBil" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="true" AllowPaging="true" EmptyDataText=" Tiada rekod"
                                    CssClass="table table-striped table-bordered table-hover" Width="100%" BorderStyle="Solid" ShowFooter="false" Font-Size="8pt" DataKeyNames="AR01_NoBilsem" PageSize="25">
                                    <Columns>
                                        <%--<asp:TemplateField HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="2%">                                    
                        <ItemTemplate>
                            <asp:RadioButton id="rbPilih" runat="server" GroupName="rbPilih" onclick = "RadioCheck(this);"/>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Bil" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="No Bil" DataField="AR01_NoBil" SortExpression="AR01_NoBil" ItemStyle-Width="10%" />
                                        <asp:BoundField HeaderText="Id Penerima" DataField="AR01_IdPenerima" SortExpression="AR01_IdPenerima" ItemStyle-Width="10%" />
                                        <asp:BoundField HeaderText="Nama" DataField="AR01_NamaPenerima" SortExpression="AR01_NamaPenerima" ItemStyle-Width="50%" />
                                        <asp:BoundField HeaderText="Jumlah (RM)" DataField="AR01_JumBlmByr" SortExpression="AR01_JumBlmByr" DataFormatString="{0:N}" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right" />
                                        <asp:TemplateField ItemStyle-Width="3%" ItemStyle-CssClass="Center" HeaderText="Tindakan">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Select" CssClass="btn-xs" ToolTip="Pilih">
								<i class="far fa-edit fa-lg"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>

            </ContentTemplate>

        </asp:UpdatePanel>
    </asp:Panel>

    <asp:Button ID="btnOpenPendahuluan" runat="server" Text="" Style="display: none" />
    <ajaxToolkit:ModalPopupExtender ID="MPEPendahuluan" runat="server" BackgroundCssClass="modalBackground" BehaviorID="mpe3"
        CancelControlID="btnClosePendahuluan" PopupControlID="pnlPendahuluan" TargetControlID="btnOpenPendahuluan">
    </ajaxToolkit:ModalPopupExtender>

    <asp:Panel ID="pnlPendahuluan" runat="server" BackColor="White" Width="60%" Style="overflow: auto;">
        <asp:UpdatePanel runat="server" ID="UpdatePanel3">
            <ContentTemplate>
                <table style="border: Solid 3px #D5AA00; width: 100%; height: 100%;">

                    <tr style="background: linear-gradient(to bottom, #fceabb 0%,#fccd4d 50%,#f8b500 51%,#fbdf93 100%);">
                        <td colspan="2" style="height: 10%; font-weight: bold; text-align: center;">Senarai Pendahuluan</td>
                        <td style="width: 10px; text-align: center;">
                            <button id="btnClosePendahuluan" runat="server" class="btnNone " title="Tutup" onclick="fClosePendahuluan();">
                                <i class="far fa-window-close fa-2x"></i>
                            </button>
                        </td>
                    </tr>

                    <tr style="height: 30px;">
                        <td colspan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Jumlah rekod :&nbsp;<label id="lblJumRekodPend" runat="server" class="control-label" style="color: mediumblue;"></label>
                            &nbsp;|&nbsp;&nbsp;                         
                           Cari : 
                           &nbsp;&nbsp;
                               
                               <asp:DropDownList ID="ddlCariPendahuluan" runat="server" Width="150px" class="form-control" AutoPostBack="true">
                                   <asp:ListItem Text="- KESELURUHAN -" Value="0" Selected="True" />
                                   <asp:ListItem Text="Nama" Value="1" />
                                   <asp:ListItem Text="No Staf" Value="2" />
                                   <asp:ListItem Text="No Pendahuluan" Value="3" />
                               </asp:DropDownList>
                             &nbsp;
                            <asp:TextBox ID="txtCariPendahuluan" runat="server" CssClass="form-control" Width="150px" Enabled="false"></asp:TextBox>                          
                            &nbsp;&nbsp;&nbsp;
                            <%--   <asp:LinkButton ID="lbtnCariPendahuluan" runat="server" CssClass="btn-circle" ToolTip="Cari" ValidationGroup="lbtnCariPendahuluan">
                        <i class="fa fa-search fa-2x" aria-hidden="true"></i>
                               </asp:LinkButton>--%>

                             <asp:LinkButton ID="lbtnCariPendahuluan" runat="server" CssClass="btnNone" ToolTip="Cari" style="padding:4px;background-color: lightgrey;">
                        <i class="fa fa-search btn-xs" aria-hidden="true"></i>
                               </asp:LinkButton>
                        </td>
                    </tr>


                    <tr style="vertical-align: top;">

                        <td colspan="3">
                            <div style="margin-bottom: 10px; margin-top: 10px; margin-left: 10px; margin-right: 10px;">
                                <asp:GridView ID="gvPendahuluan" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="true" AllowPaging="true" EmptyDataText=" Tiada rekod"
                                    CssClass="table table-striped table-bordered table-hover" Width="100%" BorderStyle="Solid" ShowFooter="false" Font-Size="8pt" DataKeyNames="ADV01_NoAdvSem" PageSize="15">
                                    <Columns>
                                        <%--<asp:TemplateField HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="2%">                                    
                        <ItemTemplate>
                            <asp:RadioButton id="rbPilih" runat="server" GroupName="rbPilih" onclick = "RadioCheck(this);"/>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Bil" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="No Pend." DataField="ADV01_NoAdv" SortExpression="ADV01_NoAdv" ItemStyle-Width="10%" />
                                        <asp:BoundField HeaderText="No Staf" DataField="ADV01_NoStaf" SortExpression="ADV01_NoStaf" ItemStyle-Width="5%" />
                                        <asp:BoundField HeaderText="Nama" DataField="MS01_Nama" SortExpression="MS01_Nama" ItemStyle-Width="50%" />
                                        <asp:BoundField HeaderText="Jumlah (RM)" DataField="ADV01_JumHutang" SortExpression="ADV01_JumHutang" DataFormatString="{0:N}" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField HeaderText="Vot" DataField="kodvot" SortExpression="kodvot" ItemStyle-Width="5%" />
                                        <asp:TemplateField ItemStyle-Width="3%" ItemStyle-CssClass="Center" HeaderText="Tindakan">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Select" CssClass="btn-xs" ToolTip="Pilih">
								<i class="far fa-edit fa-lg"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>

                </table>

            </ContentTemplate>

        </asp:UpdatePanel>
    </asp:Panel>

    <asp:Button ID="btnOpenPinjaman" runat="server" Text="" Style="display: none;" />
    <ajaxToolkit:ModalPopupExtender ID="MPEPinjaman" runat="server" BackgroundCssClass="modalBackground" BehaviorID="mpe4"
        CancelControlID="btnClosePinjaman" PopupControlID="pnlPinjaman" TargetControlID="btnOpenPinjaman">
    </ajaxToolkit:ModalPopupExtender>

    <asp:Panel ID="pnlPinjaman" runat="server" BackColor="White" Width="60%" Style="overflow: auto;">
        <asp:UpdatePanel runat="server" ID="UpdatePanel4">
            <ContentTemplate>
                <table style="border: Solid 3px #D5AA00; width: 100%; height: 100%;">

                    <tr style="background: linear-gradient(to bottom, #fceabb 0%,#fccd4d 50%,#f8b500 51%,#fbdf93 100%);">
                        <td colspan="2" style="height: 10%; font-weight: bold; text-align: center;">Senarai Pinjaman</td>
                        <td style="width: 10px; text-align: center;">
                            <button id="btnClosePinjaman" runat="server" class="btnNone " title="Tutup" onclick="fClosePinjaman();">
                                <i class="far fa-window-close fa-2x"></i>
                            </button>
                        </td>
                    </tr>

                    <tr style="height: 30px;">
                        <td colspan="3" style="padding: 10px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Jumlah rekod :&nbsp;<label id="lblJumRekodPinj" runat="server" class="control-label" style="color: mediumblue;"></label>
                            &nbsp;|&nbsp;&nbsp;Carian : &nbsp;&nbsp;
                            <asp:DropDownList ID="ddlCariPinj" runat="server" Width="150px" class="form-control" AutoPostBack="true">
                                   <asp:ListItem Text="- KESELURUHAN -" Value="0" Selected="True"/>
                                   <asp:ListItem Text="Nama" Value="1"  />
                                   <asp:ListItem Text="No Staf" Value="2" />
                                   <asp:ListItem Text="No Pinjaman" Value="3" />
                               </asp:DropDownList>
                            &nbsp;
                               <asp:TextBox ID="txtCariPinj" runat="server" CssClass="form-control" Width="150px" Enabled="false"></asp:TextBox>                            
                            &nbsp;
                               
                            &nbsp;&nbsp;&nbsp;
                               <asp:LinkButton ID="lbtnCariPinj" runat="server" CssClass="btnNone" ToolTip="Cari" style="padding:4px;background-color: lightgrey;">
                        <i class="fa fa-search btn-xs" aria-hidden="true"></i>
                               </asp:LinkButton>


                            <%-- <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Select" CssClass="btn-xs" ToolTip="Pilih">
								<i class="far fa-edit fa-lg"></i>
                                                </asp:LinkButton>--%>
                        </td>
                    </tr>


                    <tr style="vertical-align: top;">

                        <td colspan="3">
                            <div style="margin-bottom: 10px; margin-top: 10px; margin-left: 10px; margin-right: 10px;">
                                <asp:GridView ID="gvPinjaman" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="true" AllowPaging="true" EmptyDataText=" Tiada rekod"
                                    CssClass="table table-striped table-bordered table-hover" Width="100%" BorderStyle="Solid" ShowFooter="false" Font-Size="8pt" PageSize="15">
                                    <Columns>
                                        <%--<asp:TemplateField HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="2%">                                    
                        <ItemTemplate>
                            <asp:RadioButton id="rbPilih" runat="server" GroupName="rbPilih" onclick = "RadioCheck(this);"/>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Bil" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="No Pinj." DataField="GJ03_NoTrans" SortExpression="ADV01_NoAdv" ItemStyle-Width="10%" />
                                        <asp:BoundField HeaderText="No Staf" DataField="GJ03_NoStaf" SortExpression="ADV01_NoStaf" ItemStyle-Width="5%" />
                                        <asp:BoundField HeaderText="Nama" DataField="MS01_Nama" SortExpression="MS01_Nama" ItemStyle-Width="40%" />
                                        <asp:BoundField HeaderText="Jumlah (RM)" DataField="PJM06_BakiPokok" SortExpression="ADV01_JumHutang" DataFormatString="{0:N}" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField HeaderText="Kategori" DataField="PJM01_KatPinj" SortExpression="PJM01_KatPinj" ItemStyle-Width="5%" />
                                        <asp:BoundField HeaderText="No Sem." DataField="PJM01_NoPinjSem" SortExpression="PJM01_NoPinjSem" ItemStyle-Width="5%" />
                                        <asp:TemplateField ItemStyle-Width="3%" ItemStyle-CssClass="Center" HeaderText="Tindakan">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Select" CssClass="btn-xs" ToolTip="Pilih">
								<i class="far fa-edit fa-lg"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>

                </table>

            </ContentTemplate>

        </asp:UpdatePanel>
    </asp:Panel>


    <asp:Button ID="btnOpenWPR" runat="server" Text="" Style="display: none;" />
    <ajaxToolkit:ModalPopupExtender ID="mpeWPR" runat="server" BackgroundCssClass="modalBackground" BehaviorID="mpe5"
        CancelControlID="btnCloseWPR" PopupControlID="pnlWPR" TargetControlID="btnOpenWPR">
    </ajaxToolkit:ModalPopupExtender>

    <asp:Panel ID="pnlWPR" runat="server" BackColor="White" Width="60%" Height="70%" Style="overflow: auto;">
        <asp:UpdatePanel runat="server" ID="UpdatePanel5">
            <ContentTemplate>
                <table style="border: Solid 3px #D5AA00; width: 100%; height: 100%;">

                    <tr style="background: linear-gradient(to bottom, #fceabb 0%,#fccd4d 50%,#f8b500 51%,#fbdf93 100%);">
                        <td colspan="2" style="height: 10%; font-weight: bold; text-align: center;">Maklumat Lanjut WPR</td>
                        <td style="width: 10px; text-align: center;">
                            <button id="btnCloseWPR" runat="server" class="btnNone " title="Tutup" onclick="fCloseWPR();">
                                <i class="far fa-window-close fa-2x"></i>
                            </button>
                        </td>
                    </tr>

                    <tr style="height: 30px;">
                        <td colspan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Jumlah rekod :&nbsp;<label id="lblJumRekodWPR" runat="server" class="control-label" style="color: mediumblue;"></label>
                            &nbsp;|&nbsp;&nbsp;Saiz Rekod : &nbsp;&nbsp;                           
                               <asp:DropDownList ID="ddlSaizRekodWPR" runat="server" AutoPostBack="true" class="form-control" Width="60px">
                                   <asp:ListItem Text="10" Value="10" />
                                   <asp:ListItem Text="25" Value="25" Selected="true" />
                                   <asp:ListItem Text="50" Value="50" />
                                   <asp:ListItem Text="100" Value="100" />
                                   <asp:ListItem Text="200" Value="200" />
                                   <asp:ListItem Text="500" Value="500" />
                                   <asp:ListItem Text="1000" Value="1000" />
                               </asp:DropDownList>
                            &nbsp;&nbsp;
                           Cari : 
                           &nbsp;&nbsp;
                               <asp:TextBox ID="txtCariWPR" runat="server" CssClass="form-control" Width="150px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtCariWPR" runat="server" ControlToValidate="txtCariWPR" Display="Dynamic" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="lbtnCariWPR"></asp:RequiredFieldValidator>
                            &nbsp;
                               <asp:DropDownList ID="ddlCariWPR" runat="server" Width="150px" class="form-control">
                                   <asp:ListItem Text="Kod PTJ" Value="1" Selected="True" />
                                   <asp:ListItem Text="No WPR" Value="2" />
                               </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;
                               <asp:LinkButton ID="lbtnCariWPR" runat="server" CssClass="btn-circle" ToolTip="Cari" ValidationGroup="lbtnCariWPR">
                        <i class="fa fa-search fa-2x" aria-hidden="true"></i>
                               </asp:LinkButton>
                        </td>
                    </tr>


                    <tr style="vertical-align: top;">

                        <td colspan="3">
                            <div style="margin-bottom: 10px; margin-top: 10px; margin-left: 10px; margin-right: 10px;">
                                <asp:GridView ID="gvWPR" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="true" AllowPaging="true" EmptyDataText=" Tiada rekod"
                                    CssClass="table table-striped table-bordered table-hover" Width="100%" BorderStyle="Solid" ShowFooter="false" Font-Size="8pt" PageSize="25">
                                    <Columns>
                                        <%--<asp:TemplateField HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="2%">                                    
                        <ItemTemplate>
                            <asp:RadioButton id="rbPilih" runat="server" GroupName="rbPilih" onclick = "RadioCheck(this);"/>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Bil" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="No WPR" DataField="WPR02_NoWPR" SortExpression="WPR02_NoWPR" ItemStyle-Width="10%" />
                                        <asp:BoundField HeaderText="Tarikh Mohon" DataField="WPR02_TkhMohon" SortExpression="WPR02_TkhMohon" ItemStyle-Width="5%" DataFormatString="dd/MM/yyyy" />
                                        <asp:BoundField HeaderText="Jabatan/ Fakulti" DataField="Butiran" SortExpression="Butiran" ItemStyle-Width="40%" />
                                        <asp:BoundField HeaderText="Jumlah (RM)" DataField="Wpr01_Baki" SortExpression="Wpr01_Baki" DataFormatString="{0:N}" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField HeaderText="KW" DataField="kodkump" SortExpression="kodkump" ItemStyle-Width="5%" />
                                        <asp:BoundField HeaderText="Kod PTJ" DataField="KodPTJ" SortExpression="KodPTJ" ItemStyle-Width="5%" />
                                        <asp:TemplateField ItemStyle-Width="3%" ItemStyle-CssClass="Center" HeaderText="Tindakan">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Select" CssClass="btn-xs" ToolTip="Pilih">
								<i class="far fa-edit fa-lg"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>

                </table>

            </ContentTemplate>

        </asp:UpdatePanel>
    </asp:Panel>

    <asp:Button ID="btnOpenSiswa" runat="server" Text="" Style="display: none;" />
    <ajaxToolkit:ModalPopupExtender ID="MPESiswa" runat="server" BackgroundCssClass="modalBackground" BehaviorID="mpe6"
        CancelControlID="btnCloseSiswa" PopupControlID="pnlSiswa" TargetControlID="btnOpenSiswa" >
    </ajaxToolkit:ModalPopupExtender>

    <asp:Panel ID="pnlSiswa" runat="server" BackColor="White" Width="60%" Height="70%" Style="overflow: auto; margin-left:auto;">
        <asp:UpdatePanel runat="server" ID="UpdatePanel6">
            <ContentTemplate>
                <table style="border: Solid 3px #D5AA00; width: 100%; height: 100%;">

                    <tr style="background: linear-gradient(to bottom, #fceabb 0%,#fccd4d 50%,#f8b500 51%,#fbdf93 100%);">
                        <td colspan="2" style="height: 10%; font-weight: bold; text-align: center;">Maklumat Lanjut Siswazah</td>
                        <td style="width: 10px; text-align: center;">
                            <button id="btnCloseSiswa" runat="server" class="btnNone " title="Tutup" onclick="fCloseSiswa();">
                                <i class="far fa-window-close fa-2x"></i>
                            </button>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="3" style="height: 30px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Jumlah rekod :&nbsp;<label id="lblJumRekodSiswa" runat="server" class="control-label" style="color: mediumblue;"></label>
                            &nbsp;|&nbsp;&nbsp;Saiz Rekod : &nbsp;&nbsp;                           
                               <asp:DropDownList ID="ddlSaizRekodSiswa" runat="server" AutoPostBack="true" class="form-control" Width="60px">
                                   <asp:ListItem Text="10" Value="10" />
                                   <asp:ListItem Text="25" Value="25" Selected="true" />
                                   <asp:ListItem Text="50" Value="50" />
                                   <asp:ListItem Text="100" Value="100" />
                                   <asp:ListItem Text="200" Value="200" />
                                   <asp:ListItem Text="500" Value="500" />
                                   <asp:ListItem Text="1000" Value="1000" />
                               </asp:DropDownList>
                            &nbsp;&nbsp;
                           Cari : 
                           &nbsp;&nbsp;
                               <asp:TextBox ID="txtCariSiswa" runat="server" CssClass="form-control" Width="150px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtCariSiswa" runat="server" ControlToValidate="txtCariSiswa" Display="Dynamic" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="lbtnCariSiswa"></asp:RequiredFieldValidator>
                            &nbsp;
                               <asp:DropDownList ID="ddlCariSiswa" runat="server" Width="150px" class="form-control">
                                   <asp:ListItem Text="No Matrik" Value="1" Selected="True" />
                                   <asp:ListItem Text="Nama" Value="2" />
                                   <asp:ListItem Text="KP/Passport" Value="2" />
                                   <asp:ListItem Text="Fakulti" Value="2" />
                               </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;
                               <asp:LinkButton ID="lbtnCariSiswa" runat="server" CssClass="btn-circle" ToolTip="Cari" ValidationGroup="lbtnCariSiswa">
                        <i class="fa fa-search fa-2x" aria-hidden="true"></i>
                               </asp:LinkButton>
                        </td>
                    </tr>


                    <tr style="vertical-align: top;">

                        <td colspan="3">
                            <div style="margin-bottom: 10px; margin-top: 10px; margin-left: 10px; margin-right: 10px;">
                                <asp:GridView ID="gvSiswa" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="true" AllowPaging="true" EmptyDataText=" Tiada rekod"
                                    CssClass="table table-striped table-bordered table-hover" Width="100%" BorderStyle="Solid" ShowFooter="false" Font-Size="8pt" PageSize="25">
                                    <Columns>
                                        <%--<asp:TemplateField HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="2%">                                    
                        <ItemTemplate>
                            <asp:RadioButton id="rbPilih" runat="server" GroupName="rbPilih" onclick = "RadioCheck(this);"/>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Bil" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="No Matrik" DataField="SMP01_Nomatrik" SortExpression="SMP01_Nomatrik" ItemStyle-Width="10%" />
                                        <asp:BoundField HeaderText="Nama" DataField="SMP01_Nama" SortExpression="SMP01_Nama" ItemStyle-Width="30%" />
                                        <asp:BoundField HeaderText="No KP/ Passport" DataField="SMP01_KP" SortExpression="SMP01_KP" ItemStyle-Width="10%" />
                                        <asp:BoundField HeaderText="Fakulti" DataField="SMP01_Fakulti" SortExpression="SMP01_Fakulti" ItemStyle-Width="10%" />
                                        <asp:TemplateField ItemStyle-Width="3%" ItemStyle-CssClass="Center" HeaderText="Tindakan">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Select" CssClass="btn-xs" ToolTip="Pilih">
								<i class="far fa-edit fa-lg"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>

                </table>

            </ContentTemplate>

        </asp:UpdatePanel>
    </asp:Panel>

    <asp:Button ID="btnOpenSiswaPG" runat="server" Text="" Style="display: none;" />
    <ajaxToolkit:ModalPopupExtender ID="MPESiswaPG" runat="server" BackgroundCssClass="modalBackground" BehaviorID="mpe7"
        CancelControlID="btnCloseSiswaPG" PopupControlID="pnlSiswaPG" TargetControlID="btnOpenSiswaPG">
    </ajaxToolkit:ModalPopupExtender>

    <asp:Panel ID="pnlSiswaPG" runat="server" BackColor="White" Width="60%" Height="70%" Style="overflow: auto;">
        <asp:UpdatePanel runat="server" ID="UpdatePanel7">
            <ContentTemplate>
                <table style="border: Solid 3px #D5AA00; width: 100%; height: 100%;">

                    <tr style="background: linear-gradient(to bottom, #fceabb 0%,#fccd4d 50%,#f8b500 51%,#fbdf93 100%);">
                        <td colspan="2" style="height: 10%; font-weight: bold; text-align: center;">Maklumat Lanjut Pasca Siswazah</td>
                        <td style="width: 10px; text-align: center;">
                            <button id="btnCloseSiswaPG" runat="server" class="btnNone " title="Tutup" onclick="fCloseSiswaPG();">
                                <i class="far fa-window-close fa-2x"></i>
                            </button>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="3" style="height: 30px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Jumlah rekod :&nbsp;<label id="lblJumRekodSiswaPG" runat="server" class="control-label" style="color: mediumblue;"></label>
                            &nbsp;|&nbsp;&nbsp;Saiz Rekod : &nbsp;&nbsp;                           
                               <asp:DropDownList ID="ddlSaizRekodSiswaPG" runat="server" AutoPostBack="true" class="form-control" Width="60px">
                                   <asp:ListItem Text="10" Value="10" />
                                   <asp:ListItem Text="25" Value="25" Selected="true" />
                                   <asp:ListItem Text="50" Value="50" />
                                   <asp:ListItem Text="100" Value="100" />
                                   <asp:ListItem Text="200" Value="200" />
                                   <asp:ListItem Text="500" Value="500" />
                                   <asp:ListItem Text="1000" Value="1000" />
                               </asp:DropDownList>
                            &nbsp;&nbsp;
                           Cari : 
                           &nbsp;&nbsp;
                               <asp:TextBox ID="txtCariSiswaPG" runat="server" CssClass="form-control" Width="150px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtCariSiswaPG" runat="server" ControlToValidate="txtCariSiswaPG" Display="Dynamic" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="lbtnCariSiswaPG"></asp:RequiredFieldValidator>
                            &nbsp;
                               <asp:DropDownList ID="ddlCariSiswaPG" runat="server" Width="150px" class="form-control">
                                   <asp:ListItem Text="No Matrik" Value="1" Selected="True" />
                                   <asp:ListItem Text="Nama" Value="2" />
                                   <asp:ListItem Text="KP/Passport" Value="2" />
                                   <asp:ListItem Text="Fakulti" Value="2" />
                               </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;
                               <asp:LinkButton ID="lbtnCariSiswaPG" runat="server" CssClass="btn-circle" ToolTip="Cari" ValidationGroup="lbtnCariSiswa">
                        <i class="fa fa-search fa-2x" aria-hidden="true"></i>
                               </asp:LinkButton>
                        </td>
                    </tr>


                    <tr style="vertical-align: top;">

                        <td colspan="3">
                            <div style="margin-bottom: 10px; margin-top: 10px; margin-left: 10px; margin-right: 10px;">
                                <asp:GridView ID="gvSiswaPG" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="true" AllowPaging="true" EmptyDataText=" Tiada rekod"
                                    CssClass="table table-striped table-bordered table-hover" Width="100%" BorderStyle="Solid" ShowFooter="false" Font-Size="8pt" PageSize="25">
                                    <Columns>
                                        <%--<asp:TemplateField HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="2%">                                    
                        <ItemTemplate>
                            <asp:RadioButton id="rbPilih" runat="server" GroupName="rbPilih" onclick = "RadioCheck(this);"/>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Bil" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="No Matrik" DataField="SMG01_NOMATRIK" SortExpression="SMG01_NOMATRIK" ItemStyle-Width="10%" />
                                        <asp:BoundField HeaderText="Nama" DataField="SMG02_NAMA" SortExpression="SMG02_NAMA" ItemStyle-Width="30%" />
                                        <asp:BoundField HeaderText="No KP/Passport" DataField="SMG02_ID" SortExpression="SMG02_ID" ItemStyle-Width="10%" />
                                        <asp:BoundField HeaderText="Fakulti" DataField="SMG01_KODFAKULTI" SortExpression="SMG01_KODFAKULTI" ItemStyle-Width="10%" />
                                        <asp:TemplateField ItemStyle-Width="3%" ItemStyle-CssClass="Center" HeaderText="Tindakan">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Select" CssClass="btn-xs" ToolTip="Pilih">
								<i class="far fa-edit fa-lg"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>

                </table>

            </ContentTemplate>

        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
