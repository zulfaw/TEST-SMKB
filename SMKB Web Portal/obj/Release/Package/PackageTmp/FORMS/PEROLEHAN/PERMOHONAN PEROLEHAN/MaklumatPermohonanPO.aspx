<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="MaklumatPermohonanPO.aspx.vb" Inherits="SMKB_Web_Portal.MaklumatPermohonanPO" EnableEventValidation="False" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        <asp:Label ID="lbltajuk" runat="server"></asp:Label></h1>
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


        //function multiply(element) {

        //    var a = Number(element.value);
        //    var angseunit = document.getElementById('txtAngHrgSeunit')
        //    if (!anfseunit.value == null) {
        //        var b = Number(document.getElementById('txtAngHrgSeunit').value);
        //        document.getElementById('txtJumAngHrg').value = a * b;
        //    }	    
        //}

        function Calculate() {
            var kuantiti = $("#<%=txtKuantiti.ClientID%>").val();
        var AngHargaUnit = $("#<%=txtAngHrgSeunit.ClientID%>").val();
        var totalAmount = 0.00;

        if (kuantiti != "" && AngHargaUnit != "") {
            var amount = (AngHargaUnit * kuantiti);
            totalAmount = Comma(amount);

            $("#<%=txtJumAngHrg.ClientID%>").val(totalAmount);
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

        var kodvalue= <%=KodStatus%>; 

        $(function(){            

            if (kodvalue == 1){
                $('#btnStep1').addClass('btn-success').removeClass('btn-default');
            }
            if (kodvalue == 2){
                $('#btnStep1').addClass('btn-success').removeClass('btn-default');
                $('#btnStep2').addClass('btn-success').removeClass('btn-default');
            }        
            if (kodvalue == 3){
                $('#btnStep1').addClass('btn-success').removeClass('btn-default');
                $('#btnStep2').addClass('btn-success').removeClass('btn-default');
                $('#btnStep3').addClass('btn-success').removeClass('btn-default');
            }        
            if (kodvalue == 4){
                $('#btnStep1').addClass('btn-success').removeClass('btn-default');
                $('#btnStep2').addClass('btn-success').removeClass('btn-default');
                $('#btnStep3').addClass('btn-success').removeClass('btn-default');
                $('#btnStep4').addClass('btn-success').removeClass('btn-default');
            }
            if (kodvalue == 5){
                $('#btnStep1').addClass('btn-success').removeClass('btn-default');
                $('#btnStep2').addClass('btn-success').removeClass('btn-default');
                $('#btnStep3').addClass('btn-success').removeClass('btn-default');
                $('#btnStep4').addClass('btn-success').removeClass('btn-default');
                $('#btnStep5').addClass('btn-success').removeClass('btn-default');
            }
            if (kodvalue == 6){
                $('#btnStep1').addClass('btn-success').removeClass('btn-default');
                $('#btnStep2').addClass('btn-success').removeClass('btn-default');
                $('#btnStep3').addClass('btn-success').removeClass('btn-default');
                $('#btnStep4').addClass('btn-success').removeClass('btn-default');
                $('#btnStep5').addClass('btn-success').removeClass('btn-default');
                $('#btnStep6').addClass('btn-success').removeClass('btn-default');
            }
        });
    </script>

     <div class="stepwizard">
        <div class="stepwizard-row">
            <div class="stepwizard-step">
                <button id="btnStep1" type="button" class="btn-default btn-circle">1</button>
                <p>Maklumat Permohonan</p>
            </div>
            <div class="stepwizard-step">
                <button id="btnStep2" type="button" class="btn-default btn-circle">2</button>
                <p>Muat Naik Dok.</p>
            </div>
            <div class="stepwizard-step">
                <button id="btnStep3" type="button" class="btn-default btn-circle">3</button>
                <p>Ringkasan</p>
            </div>                    
        </div>
    </div>
    <div class="row">
        <asp:LinkButton ID="lbtnKembali" runat="server" CssClass="btn btn-info">
					<i class="fa fa-arrow-left fa-lg"></i>&nbsp;&nbsp;&nbsp;Kembali
        </asp:LinkButton>
    </div>
    <br />



    <asp:MultiView ID="mvMohonBeli" runat="server" ActiveViewIndex="0">

        <asp:View ID="view1" runat="server">

            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>


                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">Maklumat Perolehan
                            </h3>
                        </div>
                        <div class="panel-body" style="overflow: auto;">
                            <div class="table-responsive">
                                <table style="width: 100%" class="table table-borderless">
                                    <tbody>

                                        <tr>
                                            <td>No Permohonan:</td>
                                            <td>
                                                <asp:TextBox ID="txtNoMohon" runat="server" BackColor="#FFFFCC" ReadOnly="true" CssClass="form-control" Width="200px"></asp:TextBox>
                                                &nbsp;&nbsp;&nbsp;Tarikh:
									&nbsp;<asp:Label ID="lblTarikhPO" runat="server"></asp:Label>
                                                &nbsp;&nbsp;&nbsp;Status:
									&nbsp;<asp:Label ID="lblStatus1" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>No Perolehan:</td>
                                            <td>
                                                <asp:TextBox ID="txtNoPO" runat="server" BackColor="#FFFFCC" ReadOnly="true" CssClass="form-control" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Tahun Perolehan:</td>
                                            <td>
                                                <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="True" CssClass="form-control" Width="90px"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlTahun" runat="server" ControlToValidate="ddlTahun" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="btnSimpan" Display="Dynamic"></asp:RequiredFieldValidator>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top;"><b>Tajuk / Tujuan</b>:</td>
                                            <td>
                                                <asp:TextBox ID="txtTujuan" runat="server" Style="width: 90%; height: auto; min-height: 100px;" TextMode="MultiLine" CssClass="form-control" MaxLength="1000" onkeyup="upper(this)"></asp:TextBox>
                                                <%--<asp:RequiredFieldValidator ID="rfvtxtTujuan" runat="server" ControlToValidate="txtTujuan" ErrorMessage="" ForeColor="Red" Text="*Sila Isi" ValidationGroup="btnSimpan" Display="Dynamic" ></asp:RequiredFieldValidator>--%>
                                            </td>
                                        </tr>
                                        <tr id="trTeknikalScope1" runat="server" visible="false">
                                            <td style="vertical-align: top;">
                                                <label class="control-label" for=""><b>Scope</b>:</label></td>
                                            <td>
                                                <asp:TextBox ID="txtScope" runat="server" Style="height: auto; min-height: 100px; width: 90%;" TextMode="MultiLine" ToolTip="Sila biarkan kosong jika Pembelian Terus" MaxLength="1000" onkeyup="upper(this)"></asp:TextBox>
                                                <%--<asp:RequiredFieldValidator ID="rfvtxtScope" runat="server" ControlToValidate="txtScope" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="btnSimpan" Display="Dynamic"  ></asp:RequiredFieldValidator>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Kategori Perolehan:</td>
                                            <td>

                                                <asp:DropDownList ID="ddlkategoriPO" runat="server" AutoPostBack="True" CssClass="form-control" Width="30%"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlkategoriPO" runat="server" ControlToValidate="ddlkategoriPO" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="btnSimpan" Display="Dynamic"></asp:RequiredFieldValidator>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td>PTJ:</td>
                                            <td>

                                                <asp:DropDownList ID="ddlPTJPemohon" runat="server" AutoPostBack="True" CssClass="form-control" Width="60%" BackColor="#FFFFCC" Enabled="false"></asp:DropDownList>
                                                <%--<asp:RequiredFieldValidator ID="rfvddlPTJPemohon" runat="server" ErrorMessage="" ForeColor="Red" ControlToValidate="ddlPTJPemohon"  ValidationGroup="btnSimpan" Display="Dynamic">*Sila pilih</asp:RequiredFieldValidator>--%>
									
                                            </td>
                                        </tr>


                                    </tbody>
                                </table>
                            </div>
                            <br />


                            <div class="panel panel-default" style="width: auto">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Maklumat Bajet dan Spesifikasi
                                    </h3>
                                </div>
                                <div class="panel-body">

                                    <table class="table table-borderless">

                                        <tr>
                                            <td>
                                                <label class="control-label" for="">Kumpulan Wang</label></td>
                                            <td>
                                                <asp:DropDownList ID="ddlKW" runat="server" AutoPostBack="True" CssClass="form-control"></asp:DropDownList>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Operasi</td>
                                            <td>
                                                <asp:DropDownList ID="ddlKodOperasi" runat="server" AutoPostBack="True" CssClass="form-control"></asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label class="control-label" for="">PTj</label></td>
                                            <td>
                                                <asp:DropDownList ID="ddlPTj" runat="server" AutoPostBack="True" CssClass="form-control" BackColor="#FFFFCC" Enabled="false"></asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Projek Komited</td>
                                            <td>
                                                <asp:DropDownList ID="ddlKodProjek" runat="server" AutoPostBack="True" CssClass="form-control"></asp:DropDownList>

                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <label class="control-label" for="">Vot Lanjut</label></td>
                                            <td>
                                                <asp:DropDownList ID="ddlVotLanjut" runat="server" AutoPostBack="True" CssClass="form-control" Width="95%"></asp:DropDownList>

                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <label class="control-label" for="">Baki Peruntukan (RM)</label></td>
                                            <td>
                                                <asp:TextBox ID="txtBakiPeruntukan" runat="server" CssClass="form-control rightAlign" BackColor="#FFFFCC" Width="150px" Enabled="false"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvTxtBakiPeruntukan" runat="server" ControlToValidate="txtBakiPeruntukan" ErrorMessage="" ForeColor="Red" Text="*Peruntukan mesti mempunyai baki" ValidationGroup="btnTambahButiran" Display="Dynamic"></asp:RequiredFieldValidator>

                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <label class="control-label" for="">Barang / Perkara</label></td>
                                            <td>
                                                <asp:TextBox ID="txtPerkara" runat="server" Style="width: 90%; height: auto; min-height: 50px;" TextMode="MultiLine" CssClass="form-control" onkeyup="upper(this)"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtPerkara" runat="server" ControlToValidate="txtPerkara" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="btnTambahButiran" Display="Dynamic"></asp:RequiredFieldValidator>
                                                <%--<asp:RegularExpressionValidator ID="revtxtPerkara" runat="server" ControlToValidate="txtPerkara" ErrorMessage=" guna abjad & angka sahaja" ForeColor="Red" ValidationExpression="^[a-zA-Z0-9-_ \n]*$" ValidationGroup="btnTambahButiran"></asp:RegularExpressionValidator>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label class="control-label" for="">Kuantiti</label></td>
                                            <td>
                                                <asp:TextBox ID="txtKuantiti" runat="server" TextMode="Number" CssClass="form-control rightAlign" Width="100px" onchange='Calculate();'></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtKuantiti" runat="server" ControlToValidate="txtKuantiti" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="btnTambahButiran" Display="Dynamic"></asp:RequiredFieldValidator>
                                                <%--<asp:RegularExpressionValidator ID="revtxtKuantiti" ControlToValidate="txtKuantiti" runat="server" ErrorMessage=" Hanya 2 titik perpuluhan dibenarkan" ValidationExpression="^\d+(.\d{1,2})?$" ValidationGroup="btnTambahButiran" ForeColor="Red"></asp:RegularExpressionValidator>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label class="control-label" for="">Ukuran</label></td>
                                            <td>
                                                <asp:DropDownList ID="ddlUkuran" runat="server" CssClass="form-control" Width="150px"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlUkuran" runat="server" ControlToValidate="ddlUkuran" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="btnTambahButiran" Display="Dynamic"></asp:RequiredFieldValidator>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label class="control-label" runat="server">Anggaran Harga Seunit (RM)<%--<small>(Tanpa GST) </small>--%></asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txtAngHrgSeunit" runat="server" CssClass="form-control rightAlign" Width="150px" onchange='Calculate();'></asp:TextBox>
                                                <%--<asp:RegularExpressionValidator ID="revtxtAngHrgSeunit" ControlToValidate="txtAngHrgSeunit" runat="server" ErrorMessage=" Hanya 2 titik perpuluhan dibenarkan" ValidationExpression="^\d+(.\d{1,2})?$" ValidationGroup="btnTambahButiran" ForeColor="Red"></asp:RegularExpressionValidator>--%>
                                                <asp:RegularExpressionValidator ID="revtxtAngHrgSeunit" ControlToValidate="txtAngHrgSeunit" runat="server" ErrorMessage=" Hanya format ringgit dibenarkan" ValidationExpression="(?=.)^\$?(([1-9][0-9]{0,2}(,[0-9]{3})*)|[0-9]+)?(\.[0-9]{1,2})?$" ValidationGroup="btnTambahButiran" ForeColor="Red"></asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label class="control-label" runat="server">Jumlah Anggaran Harga (RM)<%--<small>(Tanpa GST) </small>--%></asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txtJumAngHrg" runat="server" Enabled="false" CssClass="form-control rightAlign" BackColor="#FFFFCC" Width="150px" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 40px; text-align: center;" colspan="2">
                                                <asp:LinkButton ID="lbtnReset" runat="server" CssClass="btn btn-info" ToolTip="Kosongkan Butiran Perbelanjaan">
									<i class="fas fa-file-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Rekod Baru
                                                </asp:LinkButton>
                                                &nbsp;&nbsp;
								<asp:LinkButton ID="lbtnSaveButiran" runat="server" CssClass="btn btn-info" ValidationGroup="btnTambahButiran" ToolTip="Tambah ke senarai">
									<i class="fa fa-plus-circle fa-lg"></i>&nbsp;&nbsp;&nbsp;Tambah
                                </asp:LinkButton>
                                                <%--<asp:LinkButton ID="lbtnEditButiran" runat="server" CssClass="btn btn-info" ValidationGroup="btnSaveButiran" ToolTip="Kemaskini">
									<i class="fa fa-pencil-square-o fa-lg"></i>&nbsp;&nbsp;&nbsp;Kemaskini
								</asp:LinkButton>--%>&nbsp;&nbsp;
								<asp:Button ID="btnEdit" runat="server" CssClass="btn" Text="Kemaskini" ValidationGroup="btnTambahButiran" ToolTip="Kemaskini" />
                                                &nbsp;&nbsp;
								<asp:LinkButton ID="lbtnUndo" runat="server" CssClass="btn btn-info" ToolTip="Undo">
									<i class="fas fa-undo fa-lg"></i>&nbsp;&nbsp;&nbsp;Undo
                                </asp:LinkButton>
                                            </td>
                                        </tr>

                                    </table>

                                    <%--<div>
						<table>
						<tr style="height:30px;">
						<td style="width:80px;">
							<label class="control-label" for="Klasifikasi">Saiz Rekod : </label>
						</td>
						<td style="width:50px;">
							<asp:DropDownList ID="ddlSaizRekod" runat="server" Width="50px" AutoPostBack="true" CssClass="form-control">
								<asp:ListItem Text="5" Value="5" Selected="true" />
								<asp:ListItem Text="10" Value="10" />
								<asp:ListItem Text="25" Value="25" />
								<asp:ListItem Text="50" Value="50" />
							</asp:DropDownList>
						</td>
						</tr>
						</table>
					</div>--%>
                                    <asp:GridView ID="gvDtPembelian" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="true" EmptyDataText=" "
                                        CssClass="table table-striped table-bordered table-hover" HeaderStyle-BackColor="#6699FF" Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" Width="100%" ShowFooter="True" PageSize="20">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Bil" ItemStyle-Width="2%">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBil" runat="server" Text='<%# Eval("PO01_Bil")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("PO01_DtID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="KW" SortExpression="KodKw" ItemStyle-Width="2%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center" DataField="KodKw"></asp:BoundField>
                                            <asp:BoundField HeaderText="KO" SortExpression="KodKo" ItemStyle-Width="2%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center" DataField="KodKo"></asp:BoundField>
                                            <asp:BoundField HeaderText="PTJ" SortExpression="KodPtj" ItemStyle-Width="3%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center" DataField="KodPtj"></asp:BoundField>
                                            <asp:BoundField HeaderText="KP" SortExpression="KodKp" ItemStyle-Width="2%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center" DataField="KodKp"></asp:BoundField>
                                            <asp:BoundField HeaderText="Vot" SortExpression="KodVot" ItemStyle-Width="3%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center" DataField="KodVot"></asp:BoundField>
                                            <asp:BoundField HeaderText="Barang / Perkara" SortExpression="PO01_Butiran" ItemStyle-Width="30%" HeaderStyle-CssClass="centerAlign" DataField="PO01_Butiran"></asp:BoundField>
                                            <asp:BoundField HeaderText="Kuantiti" SortExpression="PO01_Kuantiti" ItemStyle-Width="3%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Right" DataField="PO01_Kuantiti" DataFormatString="{0:N0}"></asp:BoundField>
                                            <asp:BoundField HeaderText="Ukuran" SortExpression="NamaUkuran" ItemStyle-Width="5%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center" DataField="NamaUkuran"></asp:BoundField>
                                            <asp:BoundField HeaderText="Anggaran Harga Seunit (RM)" SortExpression="PO01_KadarHarga" ItemStyle-Width="8%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Right" DataField="PO01_KadarHarga" DataFormatString="{0:N}"></asp:BoundField>
                                            <asp:BoundField HeaderText="Jumlah Anggaran Harga (RM)" SortExpression="PO01_JumKadar" ItemStyle-Width="8%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Right" DataField="PO01_JumKadar" DataFormatString="{0:N}"></asp:BoundField>

                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CssClass="btn-xs" ToolTip="Pilih Kemaskini">
											<i class="far fa-edit fa-lg"></i>
                                                    </asp:LinkButton>

                                                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CssClass="btn-xs" ToolTip="Padam"
                                                        OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');">
										<i class="far fa-trash-alt fa-lg"></i>
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

                        <br />
                        <div class="row" style="text-align: center">

                            <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn" ToolTip="Simpan" ValidationGroup="btnSimpan">
						    <i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                            </asp:LinkButton>&nbsp;  
						<asp:Button ID="btnHapus" Text="Hapus" runat="server" CssClass="btn" OnClientClick="return confirm('Anda pasti untuk hapus rekod ini?');" />
                            &nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="lbtnNext" runat="server" CssClass="btn btn-default btn-circle btn-lg" ToolTip="Seterusnya">
                                <i class="glyphicon glyphicon-chevron-right"></i>
                            </asp:LinkButton>

                        </div>

                        <br />
                    </div>
                    <p></p>

                    <%--<asp:Button ID = "btnOpen" runat = "server" Text = ""  style="display:none" />
		<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground" 
			CancelControlID="btnClose2" PopupControlID="pnlpopupHantar" TargetControlID="btnOpen"
			> </ajaxToolkit:ModalPopupExtender>

			<asp:Panel ID="pnlpopupHantar" runat="server" BackColor="White" Width="60%" Height="500px" Style="display:none; overflow-y:scroll">
			   <table style="border: Solid 3px #D5AA00; width: 100%; height: 100%;" >
					<tr style="background: linear-gradient(to bottom, #fceabb 0%,#fccd4d 50%,#f8b500 51%,#fbdf93 100%);">
						<td style="height: 20px;  font-weight: bold; text-align:center;" colspan="2" >
							Maklumat Vot
						</td>
						<td style="width:50px;text-align:center;">   
                           <button runat="server" id="btnClose2" title="Tutup" class="btnNone ">
                        <i class="far fa-window-close fa-2x"></i>
                    </button></td>
					</tr>
					<tr>
						<td>
							Cari Butiran Vot: &nbsp;
							<asp:TextBox ID="txtCariVot" runat="server" Width="50%"></asp:TextBox>&nbsp;
							<asp:RequiredFieldValidator ID="rfvtxtCariVot" runat="server" ControlToValidate="txtCariVot" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="lbtnCariVOt1" Display="Dynamic"  ></asp:RequiredFieldValidator>
							&nbsp;&nbsp;
							<asp:LinkButton ID="lbtnCariVOt1" runat="server" CssClass="btn-lg" ToolTip="Cari Vot">
								<i class="fas fa-search fa-lg"></i>
							</asp:LinkButton>
						</td>
					</tr>
					<tr style="vertical-align:top;">
						<td colspan="2">
						   <div style="margin-bottom:10px;margin-top:10px;margin-left:10px;margin-right:10px;">
							  <asp:GridView ID="gvVot" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="False" AllowPaging="false" EmptyDataText=" No data available "
				cssclass="table table-striped table-bordered table-hover" Width="100%" HeaderStyle-BackColor="#6699FF" BorderStyle="Solid" ShowFooter="false">
					<columns>
					
					<asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" SortExpression="Bil" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center">
						<ItemTemplate>
							<%# Container.DataItemIndex + 1 %>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:BoundField HeaderText="Kod Vot" DataField="KodVot" SortExpression="KodVot" HeaderStyle-CssClass="centerAlign">
						<ItemStyle Width="10%" HorizontalAlign="Center"/>
					</asp:BoundField>						            
					<asp:BoundField HeaderText="Butiran Vot" DataField="Butiran" SortExpression="Butiran" HeaderStyle-CssClass="centerAlign">
						<ItemStyle Width="35%" />
					</asp:BoundField>					              
				</columns>
			</asp:GridView>
						  </div>
						</td>
					</tr>
				   
				   <%--<tr>
					   <td style="height: 10%; text-align:center;" colspan="2" >
						   <div style="margin-bottom:10px;margin-top:10px;margin-left:10px;margin-right:10px;">
							<%--<asp:LinkButton ID="lbYes" runat="server" CssClass="btn btn-info">
								<i class="fa fa-check fa-lg"></i>&nbsp;&nbsp;&nbsp;Ya
							</asp:LinkButton>
							&nbsp;&nbsp;&nbsp;
							<asp:LinkButton ID="lbNo" runat="server" CssClass="btn btn-info">
								<i class="fa fa-times fa-lg"></i>&nbsp;&nbsp;&nbsp;Tutp
							</asp:LinkButton>
							   </div>
						</td>
				   </tr>
				</table>
			</asp:Panel>--%>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="lbtnNext" />
                </Triggers>
            </asp:UpdatePanel>
        </asp:View>

        <asp:View ID="View2" runat="server">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Lampiran Dokumen Sokongan
                    </h3>
                </div>
                <div class="panel-body">
                    <table style="width: 100%" class="table table-borderless">
                        <tr>
                            <td style="width: 20%; vertical-align: top">
                                <asp:Label ID="Label27" runat="server" Text="Lampiran" CssClass="control-label"></asp:Label>
                            </td>

                            <td>Jenis-jenis dokumen:
                                <br />
                                <asp:RadioButtonList ID="rbDokumenType" runat="server">
                                    <asp:ListItem Text="Borang Penentuan Teknikal" Value="T" />
                                    <asp:ListItem Text="Kertas Kerja" Value="K" />
                                    <asp:ListItem Text="Lain-Lain" Value="L" />
                                </asp:RadioButtonList>

                                <div class="form-inline">
                                    <asp:FileUpload ID="fuLampiran" runat="server" Width="50%" CssClass="form-control" Height="25px" BackColor="#FFFFCC" EnableViewState="true" />
                                    &nbsp;&nbsp;                              
										<asp:LinkButton ID="lbUploadLampPO" runat="server" CssClass="btn btn-info" ToolTip="Upload file">
											<i class="fa fa-upload fa-lg"></i>&nbsp;&nbsp;&nbsp;Upload
                                        </asp:LinkButton>
                                </div>
                                <label>Saiz fail muat naik maksimum 5 mb</label><br /><asp:Label ID="LabelMessage1" runat="server" />
                                <br />
                                <asp:GridView ID="gvLampiran" runat="server" AutoGenerateColumns="false" EmptyDataText=" Tiada rekod" Width="80%" CssClass="table table-striped table-bordered table-hover"
                                    HeaderStyle-BackColor="#6699FF" Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" DataKeyNames="PO13_ID" ShowHeaderWhenEmpty="true">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Bil" ItemStyle-Width="2%">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="PO13_NamaDok" HeaderText="Nama Fail" ItemStyle-Width="70%" />
                                        <asp:BoundField DataField="PO13_NoMohon" HeaderText="" ItemStyle-Width="1%" Visible="false" />
                                        <asp:HyperLinkField DataTextField="PO13_ID" DataTextFormatString="<img src='../../../Images/search.png' alt='View' />" ItemStyle-Width="3%" DataNavigateUrlFields="PO13_NoMohon,PO13_NamaDok, pathbaru" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataNavigateUrlFormatString="~/Upload/Document/{2}{1}" Target="_blank" />
                                        <asp:TemplateField ItemStyle-Width="3%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" CssClass="btn-xs" ToolTip="Delete" CommandName="delete" runat="server">
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

                    <div class="row" style="text-align: center">

                        <asp:LinkButton ID="lbtnPrevView2" runat="server" CssClass="btn btn-default btn-circle btn-lg" ToolTip="Sebelumnya">
                                <i class="glyphicon glyphicon-chevron-left"></i>
                        </asp:LinkButton>
                        &nbsp;&nbsp;
                                   <asp:LinkButton ID="lbtnNextView2" runat="server" CssClass="btn btn-default btn-circle btn-lg" ToolTip="Seterusnya">
                                <i class="glyphicon glyphicon-chevron-right"></i>
                                   </asp:LinkButton>


                    </div>
                </div>
            </div>

        </asp:View>

        <%--<asp:View ID="View3" runat="server">
					<div class="panel panel-default" >
						<div class="panel-heading">
							<h3 class="panel-title">
								Maklumat Sebut Harga / Tender
							</h3>
						</div>
					<div class="panel-body">
							<table style="width:100%" class="table table-borderless">					
														
							<tr>
								<td  style="vertical-align:top; width: 15%;"> 
									 <b>Borang Teknikal</b>
								</td>
								<td>
									<div class="form-inline">
										<asp:FileUpload ID="fuBrgTeknikal" runat="server" CssClass="form-control" BackColor="#FFFFCC" Width="50%" EnableViewState="true"/>
										<asp:LinkButton ID="lbtnUploadBrgTeknikal" runat="server" CssClass="btn btn-info" ToolTip="Upload file">
											<i class="fa fa-upload fa-lg"></i>&nbsp;&nbsp;&nbsp;Upload
										</asp:LinkButton>
									</div>
									<br />
									<asp:Label ID="lblMessage2" ForeColor="Green" runat="server" />
									<asp:GridView ID="gvBorangTeknikal" runat="server" AutoGenerateColumns="false" EmptyDataText = "No files uploaded" Width="80%" cssclass="table table-striped table-bordered table-hover"
										HeaderStyle-BackColor="#6699FF" BorderColor="#333333" BorderStyle="Solid" DataKeyNames="PO13_ID" ShowHeaderWhenEmpty="true">
										<Columns>
											<asp:BoundField DataField="PO13_ID" HeaderText="ID" ItemStyle-Width="70%" Visible="false" />
											<asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" SortExpression="Bil" HeaderStyle-CssClass="centerAlign">
												<ItemTemplate>
													<%# Container.DataItemIndex + 1 %>
												</ItemTemplate>
											</asp:TemplateField> 
											<asp:BoundField DataField="PO13_NamaDok" HeaderText="File Name" ItemStyle-Width="70%" />
											<asp:HyperLinkField DataTextField="PO13_ID" DataTextFormatString="<img src='../../../Images/search.png' alt='View' />" ItemStyle-Width="3%" DataNavigateUrlFields="PO13_NamaDok" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataNavigateUrlFormatString="~/Upload/Document/PO/{0}" Target="_blank"/>
											<asp:TemplateField ItemStyle-Width="3%">
												<ItemTemplate>
													<asp:LinkButton ID = "lnkDelete"  CssClass="btn-xs" ToolTip="Delete" CommandName="delete" runat = "server">
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
						<div class="col-md-2" style="text-align:left">                    
							<asp:LinkButton ID="lbtnPrevView3" runat="server" CssClass="btn" ToolTip="Sebelumnya" ValidationGroup="lbtnView3">							
							<i class="fas fas fa-angle-left fa-lg"></i>&nbsp;&nbsp;&nbsp;Previous
							</asp:LinkButton>
							</div> 
					   <div class="col-md-10" style="text-align:right">                    
							<asp:LinkButton ID="lbtnNextView3" runat="server" CssClass="btn" ToolTip="Seterusnya" ValidationGroup="lbtnView3">
								Next &nbsp;&nbsp;&nbsp;
							<i class="fas fa-angle-right fa-lg"></i>
							</asp:LinkButton>                  
						</div>
							</div>
					   </div>
						</div>
					</asp:View>--%>

        <asp:View ID="View3" runat="server">

            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">Ringkasan Maklumat Permohonan Perolehan
                            </h3>
                        </div>
                        <div class="panel-body">
                            <asp:GridView ID="gvBajet" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="true" EmptyDataText=" Tiada rekod"
                                CssClass="table table-striped table-bordered table-hover" HeaderStyle-BackColor="#6699FF" Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" Width="100%" ShowFooter="False"
                                Visible="false">
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
                                </Columns>

                            </asp:GridView>
                            <div style="margin-bottom: 10px;" id="dvPetunjuk" runat="server" visible="false">
                                <div>
                                    <i class="fas fa-info-circle fa-lg"></i>
                                    <asp:Label ID="Label3" runat="server" Text="Label" Font-Bold="true">Petunjuk :</asp:Label>
                                    <br />
                                </div>
                                <div>
                                    <span style="color: #FFFF00;"><i class="far fas fa-square-full fa-lg"></i></span>- Dana tidak mencukupi<br />
                                </div>
                            </div>
                            <br />
                            <asp:HiddenField ID="hdKaedah" runat="server" />

                            <table style="width: 100%" class="table table-borderless table-striped">
                                <tr>
                                    <td style="vertical-align: top; width: 15%;">No Permohonan:</Label></td>
                                    <td>
                                        <asp:Label ID="lblNoMohon" runat="server" BackColor="#FFFFCC"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;Tarikh Mohon: &nbsp;<asp:Label ID="lblTarikh" runat="server" BackColor="#FFFFCC"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;Status:
									&nbsp;<asp:Label ID="lblStatus" runat="server" BackColor="#FFFFCC"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; width: 15%;">No Perolehan:</td>
                                    <td>
                                        <asp:Label ID="lblNoPO" runat="server" BackColor="#FFFFCC"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; width: 15%;">Tujuan:</td>
                                    <td>
                                        <asp:Label ID="lblTujuan" runat="server" BackColor="#FFFFCC"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="trTeknikalScope" runat="server">
                                    <td style="vertical-align: top; width: 15%;">Scope</td>
                                    <td>
                                        <asp:Label ID="lblScope" runat="server" BackColor="#FFFFCC" Font-Bold="true"></asp:Label>

                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; width: 15%;">Kategori Perolehan:</td>
                                    <td>
                                        <asp:Label ID="lblKategoriPO" runat="server" BackColor="#FFFFCC"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; width: 15%;">Kaedah Perolehan:</td>
                                    <td>
                                        <asp:Label ID="lblKaedah" runat="server" BackColor="#FFFFCC"></asp:Label>
                                    </td>
                                    <tr>
                                        <td style="vertical-align: top; width: 15%;">Detail Perolehan:</td>
                                        <td>
                                            <asp:GridView ID="gvDtPembelian2" runat="server" AllowPaging="true" AllowSorting="True" AutoGenerateColumns="False" BorderColor="#333333" BorderStyle="Solid"
                                                CssClass="table table-striped table-bordered table-hover" EmptyDataText=" Tiada rekod" HeaderStyle-BackColor="#6699FF" PageSize="20"
                                                ShowFooter="True" ShowHeaderWhenEmpty="True" Width="100%" Font-Size="8pt">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Bil" ItemStyle-Width="2%">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-Width="1%" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBil" runat="server" Text='<%# Eval("PO01_Bil")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-Width="1%" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("PO01_DtID")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="centerAlign" HeaderText="KW" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="2%" SortExpression="KodKw">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblKW" runat="server" Text='<%# Eval("KodKw")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="centerAlign" HeaderText="KO" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="2%" SortExpression="KodKo">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblKO" runat="server" Text='<%# Eval("KodKo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="centerAlign" HeaderText="PTJ" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%" SortExpression="KodPtj">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPTJ" runat="server" Text='<%# Eval("KodPtj")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="centerAlign" HeaderText="KP" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="2%" SortExpression="KodKp">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblKP" runat="server" Text='<%# Eval("KodKp")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="centerAlign" HeaderText="Vot" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%" SortExpression="KodVot">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVot" runat="server" Text='<%# Eval("KodVot")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="centerAlign" HeaderText="Barang / Perkara" ItemStyle-Width="30%" SortExpression="PO01_Butiran">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblButiran" runat="server" Text='<%# Eval("PO01_Butiran")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="centerAlign" HeaderText="Kuantiti" ItemStyle-HorizontalAlign="right" ItemStyle-Width="3%" SortExpression="PO01_Kuantiti">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblKuantiti" runat="server" Text='<%# Eval("PO01_Kuantiti", "{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="centerAlign" HeaderText="Ukuran" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" SortExpression="NamaUkuran">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNamaUkuran" runat="server" Text='<%# Eval("NamaUkuran")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="centerAlign" HeaderText="Anggaran Harga Seunit (Tanpa GST) (RM)" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%" SortExpression="PO01_KadarHarga">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblKadarHrg" runat="server" Text='<%# Eval("PO01_KadarHarga", "{0:N}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="centerAlign" HeaderText="Jumlah Anggaran Harga (Tanpa GST) (RM)" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%" SortExpression="PO01_JumKadar">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblJumKadar" runat="server" Text='<%# Eval("PO01_JumKadar", "{0:N}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                <tr>
                                    <td style="width: 15%; vertical-align: top">Borang Lampiran </td>
                                    <td>
                                        <asp:GridView ID="gvLampiran2" runat="server" AutoGenerateColumns="False"
                                            CssClass="table table-striped table-bordered table-hover" DataKeyNames="PO13_ID" EmptyDataText=" Tiada rekod"
                                            HeaderStyle-BackColor="#6699FF" ShowHeaderWhenEmpty="True" Width="80%" Font-Size="8pt">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-CssClass="centerAlign" HeaderText="Bil" ItemStyle-Width="2%">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="centerAlign" />
                                                    <ItemStyle Width="2%" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="PO13_NamaDok" HeaderText="File Name" ItemStyle-Width="70%" >
                                                <ItemStyle Width="70%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PO13_NoMohon" HeaderText="" ItemStyle-Width="1%" Visible="false" >
                                                <ItemStyle Width="1%" />
                                                </asp:BoundField>
                                                <asp:HyperLinkField DataNavigateUrlFields="PO13_NoMohon,PO13_NamaDok, pathbaru" DataNavigateUrlFormatString="~/Upload/Document/{2}{1}" DataTextField="PO13_ID" DataTextFormatString="&lt;img src='../../../Images/search.png' alt='View' /&gt;" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%" Target="_blank" >
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" Width="3%" />
                                                </asp:HyperLinkField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <%--<tr id="trTeknikalTitle" runat="server">
								<td  style="vertical-align:top; width: 15%;">A. Title</td>
								<td>
									<asp:Label ID="lblTitle" runat="server" BackColor="#FFFFCC" Font-Bold="true"></asp:Label>
									
								</td>
							</tr>
							
							<tr id="trTeknikalBorang" runat="server">
								<td  style="vertical-align:top; width: 15%;"> 
									 Borang Teknikal
								</td>
								<td>
									<asp:GridView ID="gvBorangTeknikal2" runat="server" AutoGenerateColumns="false" EmptyDataText = "No files uploaded" Width="80%" cssclass="table table-striped table-bordered table-hover"
										HeaderStyle-BackColor="#6699FF" BorderColor="#333333" BorderStyle="Solid" DataKeyNames="PO13_ID" ShowHeaderWhenEmpty="true">
										<Columns>
											<asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" SortExpression="Bil" HeaderStyle-CssClass="centerAlign">
												<ItemTemplate>
													<%# Container.DataItemIndex + 1 %>
												</ItemTemplate>
											</asp:TemplateField> 
											<asp:BoundField DataField="PO13_NamaDok" HeaderText="File Name" ItemStyle-Width="70%" />
											<asp:HyperLinkField DataTextField="PO13_ID" DataTextFormatString="<img src='../../../Images/search.png' alt='View' />" ItemStyle-Width="3%" DataNavigateUrlFields="PO13_NamaDok" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataNavigateUrlFormatString="~/Upload/Document/PO/{0}" Target="_blank"/>											
										</Columns>
									</asp:GridView>
								</td>
							</tr>--%>
                                <tr id="trSemakanBendahariPTJ" runat="server" visible="false">
                                    <td style="vertical-align: top; width: 15%;">
                                        <b>Semakan Bendahari PTJ</b>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chxVendorNC" runat="server" Text=" Perolehan yang dikecualikan daripada tatacara perolehan" ToolTip="Klik jika pemilihan vendor tidak melalui e-Perolehan" />
                                        <%--<i class="fas fa-question-circle fa-lg" aria-hidden="true" data-toggle="tooltip" data-placement="right" title="Vendor adalah syarikat yang tidak memasuki e-Perolehan untuk memperolehi PT."></i>--%>
                                        <asp:RadioButtonList ID="rbSmkBendPTJ" runat="server" Height="25px" Width="273px" RepeatDirection="Horizontal" AutoPostBack="true">
                                            <asp:ListItem Text=" <b>Sokong</b>" Value="1" />
                                            <asp:ListItem Text=" <b>Kemaskini</b>" Value="0" />
                                        </asp:RadioButtonList>
                                        <div id="divUlasanBendPTJ" runat="server" visible="false">
                                            <label class="control-label" for="Ulasan"><b>Ulasan Kemaskini</b></label><br />
                                            <asp:TextBox ID="txtUlasanSmkBndPTJ" runat="server" TextMode="MultiLine" Width="90%" Rows="3"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtUlasanSmkBndPTJ" runat="server" ControlToValidate="txtUlasanSmkBndPTJ" ErrorMessage="" ForeColor="Red" Text="*Sila isi" Display="Dynamic" ValidationGroup="vgTidakSokong"></asp:RequiredFieldValidator>
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
                                            <asp:TextBox ID="txtUlasan" runat="server" TextMode="MultiLine" Width="90%" Rows="3"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvUlasan" runat="server" ControlToValidate="txtUlasan" ErrorMessage="" ForeColor="Red" Text="*Sila isi" Display="Dynamic" ValidationGroup="vgTidakLulus"></asp:RequiredFieldValidator>
                                        </div>

                                    </td>
                                </tr>
                                <tr id="trSemakanBendahari" runat="server" visible="false">
                                    <td style="vertical-align: top; width: 15%;">
                                        <b>Semakan Bendahari</b>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="rbSmkBend" runat="server" Height="25px" Width="273px" RepeatDirection="Horizontal" AutoPostBack="true">
                                            <asp:ListItem Text=" <b>Lengkap</b>" Value="1" />
                                            <asp:ListItem Text=" <b>Kemaskini</b>" Value="0" />
                                        </asp:RadioButtonList>
                                        <div id="divUlasanBendahari" runat="server" visible="false">
                                            <label class="control-label" for="Ulasan"><b>Ulasan Kemaskini</b></label><br />
                                            <asp:TextBox ID="txtUlasanSmkBnd" runat="server" TextMode="MultiLine" Width="90%" Rows="3"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtUlasanSmkBnd" runat="server" ControlToValidate="txtUlasanSmkBnd" ErrorMessage="" ForeColor="Red" Text="*Sila isi" Display="Dynamic" ValidationGroup="vgTidakLengkap"></asp:RequiredFieldValidator>
                                        </div>

                                    </td>
                                </tr>
                            </table>
                            <br>

                            <div class="row" style="text-align: center">

                                <asp:LinkButton ID="lbtnPrev4" runat="server" CssClass="btn btn-default btn-circle btn-lg" ToolTip="Sebelumnya">
                                <i class="glyphicon glyphicon-chevron-left"></i>
                                </asp:LinkButton>
                                &nbsp;&nbsp;
						
					                 
							<asp:Button ID="btnHantar" Text="Hantar" runat="server" CssClass="btn" OnClientClick="return confirm('Anda pasti untuk hantar rekod ini?');" />

                            </div>
                        </div>
                    </div>

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
                <Triggers>
                    <asp:PostBackTrigger ControlID="lbtnPrev4" />
                </Triggers>
            </asp:UpdatePanel>
        </asp:View>

    </asp:MultiView>
</asp:Content>
