<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="frm_PinjKend_2.aspx.vb" Inherits="SMKB_Web_Portal.frm_PinjKend_2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Permohonan Skim Pinjaman Kenderaan Staf UTeM</h1>
    <link rel="stylesheet" type="text/css" href="../../../Content/Site.css">
    <script type="text/javascript">
        function upper(ustr) {
            var str = ustr.value;
            ustr.value = str.toUpperCase();
        };

        function trim(ustr) {
            var str = ustr.value;
            ustr.value = str.trim();
        };

        var kodvalue= 1;
        var kodLulus= 1;
        $(function () {

            if (kodvalue == 1) {
                $('#btnStep1').addClass('btn-success').removeClass('btn-default');
            }
            if (kodvalue == 2) {
                $('#btnStep1').addClass('btn-success').removeClass('btn-default');
                $('#btnStep2').addClass('btn-success').removeClass('btn-default');
            }
            if (kodvalue == 3) {
                $('#btnStep1').addClass('btn-success').removeClass('btn-default');
                $('#btnStep2').addClass('btn-success').removeClass('btn-default');
                $('#btnStep3').addClass('btn-success').removeClass('btn-default');
            }
            if (kodvalue == 4) {
                $('#btnStep1').addClass('btn-success').removeClass('btn-default');
                $('#btnStep2').addClass('btn-success').removeClass('btn-default');
                $('#btnStep3').addClass('btn-success').removeClass('btn-default');
                $('#btnStep4').addClass('btn-success').removeClass('btn-default');
            }
            if (kodvalue == 5) {
                $('#btnStep1').addClass('btn-success').removeClass('btn-default');
                $('#btnStep2').addClass('btn-success').removeClass('btn-default');
                $('#btnStep3').addClass('btn-success').removeClass('btn-default');
                $('#btnStep4').addClass('btn-success').removeClass('btn-default');
                $('#btnStep5').addClass('btn-success').removeClass('btn-default');
            }
        });

        function isInputNumber(evt)
        {
            var char = String.fromCharCode(evt.which);

            if(!(/[0-9]/.test(char))){
                evt.preventDefault();
            }
        }

    </script>
    <style type="text/css">
        .highlight{
            background: rgba(192,192,192,0.2);
            padding:10px;
        }
    </style>
    <div class="stepwizard">
        <%--<div class="stepwizard-row">--%>
            <div class="stepwizard-step" style="width:20%">
                <button id="btnStep1" type="button" class="btn-default btn-circle">1</button>
                <p>Maklumat Pemohon</p>
            </div>
            <div class="stepwizard-step" style="width:20%">
                <button id="btnStep2" type="button" class="btn-default btn-circle" runat="server">2</button>
                <p>Maklumat Kenderaan</p>
            </div>
            <div class="stepwizard-step" style="width:20%">
                <button id="btnStep3" type="button" class="btn-default btn-circle" runat="server"  disabled="disabled">3</button>
                <p>Maklumat Syarikat</p>
            </div>
            <div class="stepwizard-step" style="width:20%">
                <button id="btnStep4" type="button" class="btn-default btn-circle" runat="server"  disabled="disabled">4</button>
                <p>Penjamin</p>
            </div>
            <div class="stepwizard-step" style="width:20%">
                <button id="btnStep5" type="button" class="btn-default btn-circle" runat="server"  disabled="disabled">6</button>
                <p>Senarai Semak</p>
            </div>
        <%--</div>--%>
    </div>
    <div class="alert alert-warning" role="alert" runat="server" id="lblWarning" visible="false">
        <strong><i class="fas fa-exclamation-circle fa-lg"></i></strong>&nbsp;&nbsp;<asp:Label ID="lblErr" runat="server" Text="Label" />
    </div>
    <div id="trUlasan"  runat="server" class="alert alert-danger" visible="false">
        <strong><i class="fas fa-exclamation-circle fa-lg"></i></strong>&nbsp;&nbsp;Ulasan Kelulusan :&nbsp;&nbsp;<asp:Label runat="server" ID="ltrUlasan"/>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:100%">
                <tr>
                    <td>   
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">Maklumat Pemohon
                                </h4>
                            </div>
                            <div class="panel-body">
                                <asp:HiddenField ID="hdNoIDSem" runat="server" />
                                <table style="width: 70%" class="table table-borderless" align="center">
                                    <tr id="nopendafcaw" runat="Server" visible="false">
                                        <td style="width: 14%">No Pendaftaran Syarikat</td>
                                        <td style="width: 1%;">:</td>
                                        <td style="width: 20%;">
                                            <asp:TextBox ID="txtNoSya" runat="server" CssClass="form-control" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtNoSya" runat="server" ControlToValidate="txtNoSya" CssClass="fontValidatation" Display="Dynamic" ErrorMessage="" ForeColor="Red" Text="*Perlu diisi" ValidationGroup="Daftar"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="width: 14%;"></td>
                                        <td style="width: 1%;"></td>
                                        <td style="width: 20%;"></td>
                                    </tr>
                                    <tr id="namasya" runat="Server" visible="false">
                                        <td >Nama Syarikat</td>
                                        <td >:</td>
                                        <td >
                                            <asp:TextBox ID="txtNamaSya" runat="server" CssClass="form-control" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtNamaSya" runat="server" ControlToValidate="txtNamaSya" CssClass="fontValidatation" Display="Dynamic" ErrorMessage="" ForeColor="Red" Text="*Perlu diisi" ValidationGroup="btnSimpan"></asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr id="tdNamaPendaftar" runat="server">
                                        <td >
                                            <asp:Label ID="lblNamaPendaftar"  runat="server" AssociatedControlID="txtNamaPendaftar" CssClass="control-label" Text="Nama Pendaftar"></asp:Label>
                                        </td>
                                        <td >:</td>
                                        <td >
                                            <asp:TextBox ID="txtNamaPendaftar" runat="server" CssClass="form-control" Font-Size="Medium" onkeyup="upper(this)" MaxLength="80"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtNamaPendaftar" runat="server" ControlToValidate="txtNamaPendaftar" Display="Dynamic" CssClass="fontValidatation" ToolTip="Name is required." ForeColor="Red" ValidationGroup="btnSimpan">*Perlu diisi</asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr id="tdKategori" runat="server">
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td style="vertical-align: top;">Kategori</td>
                                        <td>:</td>
                                        <td>
                                            <asp:RadioButtonList ID="rbKategoriSyarikat" runat="server" Height="25px" RepeatDirection="Vertical" AutoPostBack="true" onkeyup="trim(this)">
                                                <asp:ListItem Text="  Pembekal" Value="1" Selected="True"></asp:ListItem><asp:ListItem Text="  Bukan Pembekal" Value="2"></asp:ListItem></asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr id="NiagaUtama" runat="Server" visible="false">
                                        <td style="text-align:right"><label style="color:red;">*</label>&nbsp;Perniagaan Utama</td>
                                        <td>:</td>
                                        <td>
                                            <asp:CheckBoxList ID="chxNiagaUtama" AutoPostBack="True" CellPadding="5" CellSpacing="5" RepeatDirection="Horizontal"
                                                RepeatLayout="Flow" TextAlign="Right" runat="server">
                                                <asp:ListItem Value="B">&nbsp;Bekalan </asp:ListItem>
                                                <asp:ListItem Value="P">&nbsp;Perkhidmatan </asp:ListItem>
                                                <asp:ListItem Value="K">&nbsp;Kerja </asp:ListItem>
                                            </asp:CheckBoxList>
                                        </td>
                                        <td style="text-align:right"><label style="color:red;">*</label>&nbsp;Alamat</td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="txtAlamat1" runat="server" CssClass="form-control" onkeyup="upper(this)" MaxLength="120" Width="90%"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtAlamat1" runat="server" ControlToValidate="txtAlamat1" ErrorMessage="" ForeColor="red" Text="*Perlu diisi" ValidationGroup="lbtnNextView1" Display="Dynamic" CssClass="fontValidatation"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr id="KategoriSyarikat" runat="Server">
                                        <td style="text-align:right"><label style="color:red">*</label>&nbsp;Kategori Syarikat</td>
                                        <td >:</td>
                                        <td >
                                            <asp:CheckBoxList ID="chxKatSyarikat" AutoPostBack="True" RepeatDirection="Horizontal"
                                                RepeatLayout="Flow" TextAlign="Right" runat="server">
                                                <asp:ListItem Value="E">&nbsp;Enterprise / Trading </asp:ListItem>
                                                <asp:ListItem Value="S">&nbsp;Sdn. Bhd. / Bhd. </asp:ListItem>
                                            </asp:CheckBoxList>
                                        </td>
                                        <td></td>
                                        <td></td>
                                        <td>
                                            <asp:TextBox ID="txtAlamat2" runat="server" CssClass="form-control" onkeyup="upper(this)" MaxLength="120" Width="90%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:right">Laman Web URL</td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="txtWeb" runat="server" CssClass="form-control" TextMode="url" onkeyup="trim(this)" MaxLength="50"></asp:TextBox></td>
                                        <td style="text-align:right"><label style="color:red">*</label>&nbsp;Negara</td>
                                        <td>:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlNegara" runat="server" AutoPostBack="True" CssClass="form-control" Width="70%"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlNegara" runat="server" ControlToValidate="ddlNegara" CssClass="fontValidatation" Display="Dynamic" ErrorMessage="" ForeColor="red" Text="*Perlu diisi" ValidationGroup="btnSimpan"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:right">Email Syarikat</td>
                                        <td>:</td>
                                        <td>
                                            <asp:label ID="lblEmailSya" runat="server" CssClass="control-label"></asp:label></td>
                                        <td style="text-align:right"><label style="color:red">*</label>&nbsp;Negeri</td>
                                        <td>:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlNegeri" runat="server" AutoPostBack="True" CssClass="form-control" Width="70%"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlNegeri" runat="server" ControlToValidate="ddlNegeri" CssClass="fontValidatation" Display="Dynamic" ErrorMessage="" ForeColor="red" Text="*Perlu diisi" ValidationGroup="btnSimpan"></asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:right"><label style="color:red">*</label>&nbsp;No. Telefon Bimbit</td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="txtTelp1" runat="server" CssClass="form-control" TextMode="phone" onkeyup="trim(this)" MaxLength="20" onkeypress="isInputNumber(event)"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxttxtTelp1" runat="server" ControlToValidate="txtTelp1" CssClass="fontValidatation" Display="Dynamic" ErrorMessage="" ForeColor="red" Text="*Perlu diisi" ValidationGroup="btnSimpanMakSya">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td style="text-align:right"><label style="color:red">*</label>&nbsp;Bandar</td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="txtBandar" runat="server" CssClass="form-control" Width="70%" onkeyup="upper(this)" MaxLength="30"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtBandar" runat="server" ControlToValidate="txtBandar" CssClass="fontValidatation" Display="Dynamic" ErrorMessage="" ForeColor="red" Text="*Perlu diisi" ValidationGroup="btnSimpan"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:right">No. Telefon Pejabat </td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="txtTelp2" runat="server" CssClass="form-control" TextMode="Phone" onkeyup="trim(this)" MaxLength="20" onkeypress="isInputNumber(event)"></asp:TextBox>
                                        </td> 
                                        <td style="text-align:right"><label style="color:red">*</label>&nbsp;Poskod</td>
                                        <td >:</td>
                                        <td>
                                            <asp:TextBox ID="txtPoskod" runat="server" CssClass="form-control"  onkeyup="trim(this)" MaxLength="10" Width="70%"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtPoskod" runat="server" ControlToValidate="txtPoskod" CssClass="fontValidatation" Display="Dynamic" ErrorMessage="" ForeColor="red" Text="*Perlu diisi" ValidationGroup="btnSimpan"></asp:RequiredFieldValidator>
							            </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:right">No. Faksimili </td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="txtFax" runat="server" CssClass="form-control" TextMode="Phone" onkeyup="trim(this)" MaxLength="15" onkeypress="isInputNumber(event)"></asp:TextBox>
                                        </td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                </table>
                                <table style="width: 100%">
                                    <tr>
                                        <td class="subtitle">Maklumat Bank</td>
                                    </tr>
                                </table>
                                <table style="width:100%">
                                    <tr>
                                    <td>
                                        <asp:GridView ID="gvPenyataAkaun" datakeyname="" runat="server" AllowSorting="true" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText=" Tiada rekod"
                                        CssClass="table table-striped table-bordered table-hover" Font-Size="8pt" BorderStyle="Solid" ShowFooter="True" Width="100%" DataKeyNames="ROC01_IDBank">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Bil" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Kod Bank" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblKodBank" runat="server" Text='<%# IIf(IsDBNull(Eval("ROC01_KodBank")), "", Eval("ROC01_KodBank")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlKodBank" runat="server" CssClass="form-control" Style="width: 100%;" AutoPostBack="true" ></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlKodBankEDIT" runat="server" ControlToValidate="ddlKodBank" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="lbtnSave" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddlKodBank" runat="server" CssClass="form-control" Style="width: 100%;" AutoPostBack="true" ></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlKodBank" runat="server" ControlToValidate="ddlKodBank" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="lbtnAdd" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nombor Akaun" ItemStyle-Width="40%" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNoAkaun" runat="server" Text='<%# IIf(IsDBNull(Eval("ROC01_NoAkaun")), "", Eval("ROC01_NoAkaun")) %>' ></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtNoAkaun" runat="server" CssClass="form-control rightAlign" Style="width: 100%;" Text='<%# IIf(IsDBNull(Eval("ROC01_NoAkaun")), "", Eval("ROC01_NoAkaun")) %>' TextMode="Number"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvtxtNoAkaunEDIT" runat="server" ControlToValidate="txtNoAkaun" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="lbtnSave" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtNoAkaun" runat="server" CssClass="form-control rightAlign" Style="width: 100%;"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvtxtNoAkaun" runat="server" ControlToValidate="txtNoAkaun" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="lbtnAdd" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Muka Hadapan Penyata Akaun" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                <asp:HyperLink ID="HLPenyataAkaun" runat="server" Target="_blank" NavigateUrl='' Text=''>
                                                </asp:HyperLink>
                                                </ItemTemplate>
                                                <ItemStyle ForeColor="blue"/>
                                                <EditItemTemplate>
                                                    <asp:FileUpload ID="fuPenyataAkaunEdit" runat="server" CssClass="form-control" Width="250px" />
                                                    <asp:HyperLink ID="HLPenyataAkaunEdit" runat="server" Target="_blank" NavigateUrl='' Text=''>
                                                    </asp:HyperLink>
                                                    <asp:RequiredFieldValidator ID="rfvfuPenyataAkaunEdit" runat="server" ControlToValidate="fuPenyataAkaunEdit" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="lbtnAdd" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:FileUpload ID="fuPenyataAkaun" runat="server" CssClass="form-control" Width="250px" />
                                                    <asp:RequiredFieldValidator ID="rfvfuPenyataAkaun" runat="server" ControlToValidate="fuPenyataAkaun" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="lbtnAdd" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Center" HeaderText="Tindakan" FooterStyle-HorizontalAlign="Center">
                                                <EditItemTemplate>
                                                    <asp:LinkButton ID="lbtnSave" runat="server" CommandName="Update" CssClass="btn-xs" ToolTip="Simpan" ValidationGroup="lbtnSave">
												                <i class="far fa-save fa-lg"></i>
                                                    </asp:LinkButton>&nbsp; <asp:LinkButton ID="lbtnCancel" runat="server" CommandName="Cancel" CssClass="btn-xs" ToolTip="Undo">
												                        <i class="fas fa-undo fa-lg"></i>
                                                                        </asp:LinkButton></EditItemTemplate><ItemTemplate>
                                                    <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Edit" CssClass="btn-xs" ToolTip="Kemas Kini">
												                    <i class="far fa-edit fa-lg"></i>
                                                    </asp:LinkButton>&nbsp; <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete" CssClass="btn-xs" ToolTip="Padam"
                                                                OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');">
										                        <i class="far fa-trash-alt fa-lg"></i>
                                                            </asp:LinkButton></ItemTemplate><FooterTemplate>
                                                    <asp:LinkButton ID="lbtnAdd" runat="server" CssClass="btn-xs" ToolTip="Tambah" OnClick="lbtnAdd_Click" ValidationGroup="lbtnAdd">
												                    <i class="fa fa-plus-circle fa-lg"></i>
                                                    </asp:LinkButton></FooterTemplate></asp:TemplateField></Columns><EmptyDataTemplate>
                                        </EmptyDataTemplate>
                                        <SelectedRowStyle ForeColor="Blue" />
                                    </asp:GridView>
                                    </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
            
            <div class="panel panel-default" >
                <div class="panel-heading">
                    <h4 class="panel-title">Maklumat Bank <%'If 'KodLulus = "1" Then %><asp:LinkButton ID="lbtnEditBank" runat="server" CssClass="btn-xs" ToolTip="Kemaskini">
							<i class="far fa-edit fa-lg"></i>
                        </asp:LinkButton><%'End If%></h4></div><div class="panel-body">
                    <table style="width: 100%" class="table table-borderless">
                        <tr>
                            <td style="width: 20%;">Kod Bank <label style="color:red;">*</label></td><td style="width: 3%;">:</td><td style="width: 77%;">
                                <asp:DropDownList ID="ddlKodBank" runat="server" AutoPostBack="True" CssClass="form-control" Width="80%"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlKodBank" runat="server" ControlToValidate="ddlKodBank" CssClass="fontValidatation" Display="Dynamic" ErrorMessage="" ForeColor="red" Text="*Perlu diisi" ValidationGroup="btnSimpan"></asp:RequiredFieldValidator></td></tr><tr>
                            <td style="width: 20%;">No Akaun <label style="color:red;">*</label></td><td>:</td><td>
                                <asp:TextBox ID="txtNoAkaun" runat="server" CssClass="form-control" Width="300px" onkeyup="trim(this)" MaxLength="20"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtNoAkaun" runat="server" ControlToValidate="txtNoAkaun" CssClass="fontValidatation" Display="Dynamic" ErrorMessage="" ForeColor="red" Text="*Perlu diisi" ValidationGroup="btnSimpan"></asp:RequiredFieldValidator></td></tr><tr id="trGSTUpload" runat="server" visible="true">
                            <td style="width: 20%; vertical-align: top">Muka Hadapan Penyata Akaun <label style="color:red;">*</label></td><td >:</td><td >
                                <div class="form-inline">
                                    <asp:FileUpload ID="fuPenyata" runat="server" Width="50%" CssClass="form-control" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="fuPenyata" CssClass="fontValidatation" Display="Dynamic" ErrorMessage="" ForeColor="Red" Text="*Perlu diisi" ValidationGroup="lbtnUpPenyata"></asp:RequiredFieldValidator><asp:LinkButton ID="lbtnUpPenyata" runat="server" ToolTip="Upload file" ValidationGroup="lbtnUpPenyata">
											<i class="fa fa-upload fa-lg"></i>
                                        </asp:LinkButton></div></td></tr><tr>
                            <td></td>
                            <td></td>
                            <td>
                                <asp:Label ID="LabelMessage1" runat="server" Visible="True"/>
                                <br />
                                <asp:GridView ID="gvPenyata" runat="server" AutoGenerateColumns="false" EmptyDataText="No files uploaded" Width="80%" CssClass="table table-striped table-bordered table-hover"
                                    HeaderStyle-BackColor="#6699FF" Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" DataKeyNames="ROC09_ID" ShowHeaderWhenEmpty="true">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Bil" ItemStyle-Width="2%" SortExpression="Bil" HeaderStyle-CssClass="centerAlign">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ROC09_NamaDok" HeaderText="Nama Fail" ItemStyle-Width="70%" />
                                        <asp:HyperLinkField DataNavigateUrlFields="ROC01_IDSem,ROC09_NamaDok" DataNavigateUrlFormatString="~/Upload/Vendor/MH Penyata Akaun/{0}/{1}" DataTextField="ROC09_ID" DataTextFormatString="&lt;img src='../../../Images/search.png' alt='View' /&gt;" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%" Target="_blank" />
                                        <asp:TemplateField ItemStyle-Width="3%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" CssClass="btn-xs" ToolTip="Delete" CommandName="delete" runat="server" OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');">
														<i class="fas fa-trash-alt fa-lg"></i>
                                                </asp:LinkButton></ItemTemplate></asp:TemplateField></Columns></asp:GridView></td></tr></table><table>
                        <tr>
                            <td style="width: 20%;">Profil Syarikat <label style="color:red;">*</label></td><td style="width: 3%;">:</td><td style="width: 77%;">
                                <div class="form-inline">
                                    <asp:FileUpload ID="fuProfilSya" runat="server" CssClass="form-control" Width="350px" /> 
                                    <asp:LinkButton ID="lbtnProfilSya" runat="server" ToolTip="Klik sini untuk upload fail" ValidationGroup="">
                                        <i class="fa fa-upload fa-lg" ></i>
                                    </asp:LinkButton></div></td></tr><tr>
                            <td></td>
                            <td></td>
                            <td>
                                <p><label style="color:red">* Maximum saiz fail <b>TIDAK MELEBIHI </b>500MB</label><br /> <b>Hanya</b> satu fail <b>sahaja</b> yang <b>boleh</b> dimuat <b>naik</b> untuk dokumen profil syarikat. </p><br /><asp:Label ID="lblmsgProfilSya" runat="server" Visible="True"/>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td>
                        <asp:GridView ID="gvProfilSya" runat="server" AutoGenerateColumns="false" EmptyDataText="No files uploaded" Width="80%" CssClass="table table-striped table-bordered table-hover"
                            HeaderStyle-BackColor="#6699FF" Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" DataKeyNames="ROC09_ID" ShowHeaderWhenEmpty="true">
                            <Columns>
                                <asp:TemplateField HeaderText="Bil" ItemStyle-Width="2%" SortExpression="Bil" HeaderStyle-CssClass="centerAlign">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ROC09_NamaDok" HeaderText="File Name" ItemStyle-Width="70%" />
                                <asp:HyperLinkField DataNavigateUrlFields="ROC01_IDSem,ROC09_NamaDok" DataNavigateUrlFormatString="~/Upload/Vendor/ProfilSyarikat/{0}/{1}" DataTextField="ROC09_ID" DataTextFormatString="&lt;img src='../../../Images/search.png' alt='View' /&gt;" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%" Target="_blank" />
                                <asp:TemplateField ItemStyle-Width="3%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDelete" CssClass="btn-xs" ToolTip="Delete" CommandName="delete" runat="server" OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');">
										        <i class="fas fa-trash-alt fa-lg"></i>
                                        </asp:LinkButton></ItemTemplate></asp:TemplateField></Columns></asp:GridView></td></tr></table><br /></div></div><table style="width:100%">
            <tr>
                <td>
                    <div class="panel panel-default" >
                        <div class="panel-heading" >Pegawai untuk Dihubungi</div><div class="panel-body">
                            <table style="width:100%" class="table table-borderless">
                                <tr>
                                    <td style="background-color:lightgrey;font-style:oblique;border:solid 1px;width:50%" align="center"><b>Pertama</b></td><td style="background-color:lightgrey;font-style:oblique;border:solid 1px;width:50%" align="center"><b>Kedua</b></td></tr></table><table style="width:100%" class="table table-borderless">
                                <tr>
                                    <td style="width: 20%;">Nama <label style="color:red">*</label></td><td style="width: 1%;">:</td><td style="width: 29%;">
                                        <asp:TextBox ID="txtPICNama" runat="server" CssClass="form-control" Width="90%" onkeyup="upper(this)" MaxLength="300"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPICNama" CssClass="fontValidatation" Display="Dynamic" ErrorMessage="" ForeColor="red" Text="*Perlu diisi" ValidationGroup="btnSimpanMakSya">
                                        </asp:RequiredFieldValidator></td><td style="width: 20%;">Nama</td><td style="width: 1%;">:</td><td style="width: 29%;">
                                        <asp:TextBox ID="txtPIC1_Nama" runat="server" CssClass="form-control" Width="90%" onkeyup="upper(this)" MaxLength="300"></asp:TextBox></td></tr><tr>
                                    <td>Jawatan <label style="color:red">*</label></td><td>:</td><td>   
                                        <asp:TextBox ID="txtPICJwtn" runat="server" CssClass="form-control" Width="60%" MaxLength="300"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPICJwtn" CssClass="fontValidatation" Display="Dynamic" ErrorMessage="" ForeColor="red" Text="*Perlu diisi" ValidationGroup="btnSimpanMakSya">
                                        </asp:RequiredFieldValidator></td><td>Jawatan</td><td>:</td><td>
                                        <asp:TextBox ID="txtPIC1_Jwtn" runat="server" CssClass="form-control" Width="60%" MaxLength="300"></asp:TextBox></td></tr><tr>
                                    <td>Emel </td><td>:</td><td>   
                                        <asp:TextBox ID="txtPICEmel" runat="server" CssClass="form-control" Width="50%" MaxLength="120"></asp:TextBox></td><td>Emel</td><td>:</td><td>
                                        <asp:TextBox ID="txtPIC1_Emel" runat="server" CssClass="form-control" Width="50%" MaxLength="120"></asp:TextBox></td></tr><tr>
                                    <td>No. Telefon Bimbit <label style="color:red">*</label> </td><td>:</td><td>
                                        <asp:TextBox ID="txtPICNum" runat="server" CssClass="form-control" Width="150px" MaxLength="13" onkeypress="isInputNumber(event)"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPICNum" CssClass="fontValidatation" Display="Dynamic" ErrorMessage="" ForeColor="red" Text="*Perlu diisi" ValidationGroup="btnSimpanMakSya">
                                        </asp:RequiredFieldValidator></td><td>No. Telefon Bimbit</td><td>:</td><td>
                                        <asp:TextBox ID="txtPIC1_Num" runat="server" CssClass="form-control" Width="150px" MaxLength="13" onkeypress="isInputNumber(event)"></asp:TextBox></td></tr><tr>
                                    <td style="width: 20%;">No. Telefon Pejabat <label style="color:red">*</label></td><td>:</td><td>
                                        <asp:TextBox ID="txtPICNumPej" runat="server" CssClass="form-control" Width="150px" MaxLength="13" onkeypress="isInputNumber(event)"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPICNumPej" CssClass="fontValidatation" Display="Dynamic" ErrorMessage="" ForeColor="red" Text="*Perlu diisi" ValidationGroup="btnSimpanMakSya">
                                        </asp:RequiredFieldValidator></td><td>No. Telefon Pejabat</td><td>:</td><td>
                                        <asp:TextBox ID="txtPIC1_NumPej" runat="server" CssClass="form-control" Width="150px" MaxLength="13" onkeypress="isInputNumber(event)"></asp:TextBox></td></tr></table></div></div></td></tr></table><table>
            </table>
            <div class="row" style="text-align:center;">
                <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn btn-primary" ValidationGroup="btnSimpan">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                </asp:LinkButton>&nbsp;&nbsp; <asp:LinkButton ID="lbtnNext" runat="server" CssClass="btn btn-default btn-circle btn-lg" ToolTip="Seterusnya" ValidationGroup="btnSimpan">
                    <i class="glyphicon glyphicon-chevron-right"></i>
                </asp:LinkButton></div></ContentTemplate><Triggers>  
           <asp:PostBackTrigger ControlID="lbtnUpPenyata" />  
            <asp:PostBackTrigger ControlID="lbtnProfilSya" /> 
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
