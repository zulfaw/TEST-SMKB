<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Maklumat_Vendor.aspx.vb" UICulture="ms-MY" Culture="ms-MY" Inherits="SMKB_Web_Portal.Maklumat_Vendor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Maklumat Vendor</h1>
    <link rel="stylesheet" type="text/css" href="../../../../Content/Site.css">
    <script type="text/javascript">
        function upper(ustr) {
            var str = ustr.value;
            ustr.value = str.toUpperCase();
        };

        function lower(ustr) {
            var str = ustr.value;
            ustr.value = str.toLowerCase();
        };

        function fCloseSenarai() {
            $find("mpe7").hide();
        };
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

    <div class="row">
        <asp:LinkButton ID="lbtnKembali" runat="server" CssClass="btn btn-info">
					<i class="fa fa-arrow-left fa-lg"></i>&nbsp;&nbsp;&nbsp;Kembali
        </asp:LinkButton>
    </div>

    <asp:UpdatePanel ID="updatepanel1" runat="server">
        <ContentTemplate>


            <div class="panel panel-default">
                <div class="panel-heading">
                    <h4 class="panel-title">Maklumat Syarikat
                    </h4>
                </div>
                <div class="panel-body">
                    <asp:HiddenField ID="hdNoSya" runat="server" />
                    <asp:HiddenField ID="hdNoIDSemSya" runat="server" />
                    <table class="table table-borderless table-stripped">
                        <tr>
                            <td>No Pendaftaran Syarikat (SSM):</td>
                            <td>
                                <asp:TextBox ID="txtNoSya" runat="server" CssClass="form-control" Width="150px" ReadOnly="true" BackColor="#FFFFCC"></asp:TextBox>
                                &nbsp;&nbsp;
                                <asp:LinkButton ID="lbtnPerubahan" runat="server" CssClass="btn btn-info" ToolTip="Senarai perubahan yang dibuat oleh vendor">
                                        <i class="fas fa-clipboard-list fa-lg"></i>&nbsp;&nbsp;&nbsp;Senarai
                                </asp:LinkButton>
                                
                            </td>
                        </tr>
                        <tr>
                            <td>Nama Syarikat:</td>
                            <td>
                                <asp:TextBox ID="txtNamaSya" runat="server" CssClass="form-control" Width="90%" ReadOnly="true" BackColor="#FFFFCC" onkeyup="upper(this)"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Perniagaan Utama:</td>
                            <td>
                                <asp:CheckBoxList ID="chxNiagaUtama" AutoPostBack="True" CellPadding="5" CellSpacing="5" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" TextAlign="Right" runat="server" Enabled="false">
                                    <asp:ListItem Value="B">&nbsp;Bekalan </asp:ListItem>
                                    <asp:ListItem Value="P">&nbsp;Perkhidmatan </asp:ListItem>
                                    <asp:ListItem Value="K">&nbsp;Kerja </asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <td>Gred Kerja:</td>
                            <td>
                                <asp:Label ID="lblGredKerja" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Status Bumiputera: </td>
                            <td>
                                <asp:RadioButtonList ID="rbBumi" runat="server" Height="25px" RepeatDirection="Horizontal" AutoPostBack="true" RepeatColumns="2" Width="300px" Enabled="false">
                                    <asp:ListItem Text="  Bumiputera" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="  Bukan Bumiputera" Value="0"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>Status Lulus:</td>
                            <td>
                                <asp:TextBox ID="txtStatLulus" runat="server" CssClass="form-control" Width="300px" ReadOnly="true" BackColor="#FFFFCC" onkeyup="upper(this)"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Status Aktif:</td>
                            <td>
                                <asp:TextBox ID="txtStatAktif" runat="server" CssClass="form-control" Width="300px" ReadOnly="true" BackColor="#FFFFCC" onkeyup="upper(this)"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trLulusDaftar" runat="server" visible="false">
                            <td>Kelulusan Pendaftaran:</td>
                            <td>
                                <asp:RadioButtonList ID="rbKelulusanDaftar" runat="server" Height="25px" Width="273px" RepeatDirection="Horizontal" AutoPostBack="true">
                                    <asp:ListItem Text=" <b>Lulus</b>" Value="1" />
                                    <asp:ListItem Text=" <b>Tidak Lulus</b>" Value="2" />
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="rfvrbKelulusanDaftar" runat="server" ControlToValidate="rbKelulusanDaftar" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="lbtnHantar" Display="Dynamic" Enabled="false"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="trLulusKemaskini" runat="server" visible="false">
                            <td>Kelulusan Kemaskini:</td>
                            <td>
                                <asp:RadioButtonList ID="rbKelulusanKemaskini" runat="server" Height="25px" Width="273px" RepeatDirection="Horizontal" AutoPostBack="true">
                                    <asp:ListItem Text=" <b>Lulus</b>" Value="1" />
                                    <asp:ListItem Text=" <b>Tidak Lulus</b>" Value="2" />
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="rfvrbKelulusanKemaskini" runat="server" ControlToValidate="rbKelulusanKemaskini" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="lbtnHantar" Display="Dynamic" Enabled="false"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="trTamat" runat="server" visible="false">
                            <td>Penamatan:</td>
                            <td>
                                <asp:DropDownList ID="ddlPenamatan" runat="server" AutoPostBack="True" CssClass="form-control" Width="300px">
                                    <asp:ListItem Text="-Sila Pilih-" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Senarai Hitam" Value="02"></asp:ListItem>
                                    <asp:ListItem Text="Digantung" Value="03"></asp:ListItem>
                                    <asp:ListItem Text="Tidak Aktif" Value="00"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlPenamatan" runat="server" ControlToValidate="ddlPenamatan" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="lbtnHantar" Display="Dynamic" Enabled="false"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="trUlasan" runat="server" visible="false">
                            <td>Ulasan:</td>
                            <td>
                                <asp:TextBox ID="txtUlasan" runat="server" TextMode="MultiLine" Columns="20" Rows="3"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvUlasan" runat="server" ControlToValidate="txtUlasan" ErrorMessage="" ForeColor="Red" Text="*Sila isi" Display="Dynamic" ValidationGroup="lbtnHantar" Enabled="false"></asp:RequiredFieldValidator>

                            </td>
                        </tr>
                        <tr id="trbtnHantar" runat="server" visible="false">
                            <td colspan="2" style="text-align: center; height: 40px">
                                
                                <asp:LinkButton ID="lbtnHantar" runat="server" CssClass="btn btn-info" ValidationGroup="lbtnHantar" OnClientClick="return confirm('Anda pasti untuk hantar rekod ini?');">
                                        <i class="fab fa-telegram-plane fa-lg"></i>&nbsp;&nbsp;&nbsp;Hantar
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </table>                    
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:MultiView ID="mvSyarikat" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h4 class="panel-title">Maklumat Syarikat
                            </h4>
                        </div>
                        <div class="panel-body">

                            <div class="row">
                                <table class="table table-borderless table-striped">
                                    <tr>
                                        <th style="width: 20%; text-align: center" colspan="2">Status Pembayaran Terkini</th>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%">Status Pembayaran</td>
                                        <td>
                                            <asp:Label ID="lblStatusBayar" runat="server" CssClass="form-control" Width="150px" BackColor="#FFFFCC"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%">Tahun Aktif</td>
                                        <td>
                                            <asp:Label ID="lblThnAktif" runat="server" CssClass="form-control" Width="80px" BackColor="#FFFFCC"></asp:Label></td>
                                    </tr>
                                </table>
                            </div>

                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title">Maklumat Bank
                                    </h4>
                                </div>
                                <div class="panel-body">
                                    <table style="width: 100%">
                                        <tr>
                                            <td>Kod Bank :</td>
                                            <td>
                                                <asp:TextBox ID="txtKodBank" runat="server" CssClass="form-control" Width="300px" ReadOnly="true" BackColor="#FFFFCC"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>No Akaun :</td>
                                            <td>
                                                <asp:TextBox ID="txtNoAkaun" runat="server" CssClass="form-control" Width="300px" ReadOnly="true" BackColor="#FFFFCC"></asp:TextBox>
                                            </td>
                                        </tr>

                                    </table>
                                    <br />

                                </div>
                            </div>

                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title">Maklumat GST
                                    </h4>
                                </div>
                                <div class="panel-body">
                                    <table style="width: 100%" class="table table-borderless">
                                        <tr>
                                            <td style="width: 20%;">No Pendaftaran GST:</td>
                                            <td>
                                                <asp:TextBox ID="txtNoGST" runat="server" CssClass="form-control" Width="300px" ReadOnly="true" BackColor="#FFFFCC"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr class="calendarContainerOverride">
                                            <td style="width: 20%;">Tarikh Berkuatkuasa:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtTrkMulaGST" runat="server" CssClass="form-control" Width="20%" ReadOnly="true" BackColor="#FFFFCC"></asp:TextBox>

                                                &nbsp;&nbsp;&nbsp;
									<label class="control-label" for="">Hingga</label>
                                                &nbsp
									:&nbsp<asp:TextBox ID="txtTrkHinggaGST" runat="server" CssClass="form-control" Width="20%" ReadOnly="true" BackColor="#FFFFCC"></asp:TextBox>

                                            </td>
                                        </tr>
                                    </table>
                                    <br />

                                </div>
                            </div>

                            <div class="panel panel-default" style="width: 90%">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Butiran Alamat
                                    </h3>
                                </div>
                                <div class="panel-body">

                                    <div class="row">
                                        <div class="col">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                    Alamat Perniagaan
                                                </div>
                                                <div class="panel-body">
                                                    <table style="width: 100%;" class="table table-borderless">
                                                        <tr>
                                                            <td style="width: 20%; vertical-align: top;" rowspan="2">Alamat:</td>
                                                            <td>
                                                                <asp:TextBox ID="txtAlamat1" runat="server" Width="90%" CssClass="form-control" BackColor="#FFFFCC" ReadOnly="true" onkeyup="upper(this)"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtAlamat2" runat="server" CssClass="form-control" Width="90%" BackColor="#FFFFCC" ReadOnly="true" onkeyup="upper(this)"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%;">Poskod:</td>
                                                            <td>
                                                                <asp:TextBox ID="txtPoskod" runat="server" CssClass="form-control" Width="80px" TextMode="Number" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>

                                                                &nbsp;&nbsp;&nbsp;
								Bandar &nbsp;
								<asp:TextBox ID="txtBandar" runat="server" CssClass="form-control" Width="200px" BackColor="#FFFFCC" ReadOnly="true" onkeyup="upper(this)"></asp:TextBox>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%;">Negeri:</td>
                                                            <td>
                                                                <asp:TextBox ID="txtNegeri" runat="server" CssClass="form-control" Width="300px" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%;">Negara:</td>
                                                            <td>
                                                                <asp:TextBox ID="txtNegara" runat="server" CssClass="form-control" Width="300px" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%;">No. Telepon 1:</td>
                                                            <td>
                                                                <asp:TextBox ID="txtTelp1" runat="server" BackColor="#FFFFCC" ReadOnly="true" CssClass="form-control" Width="100px" TextMode="Number"></asp:TextBox>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%;">No. Telepon 2:</td>
                                                            <td>
                                                                <asp:TextBox ID="txtTelp2" runat="server" BackColor="#FFFFCC" ReadOnly="true" CssClass="form-control" Width="100px" TextMode="Number"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%;">No. Faksimili:</td>
                                                            <td>
                                                                <asp:TextBox ID="txtFax" runat="server" BackColor="#FFFFCC" ReadOnly="true" CssClass="form-control" Width="100px" TextMode="Number"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%;">Laman Web URL:</td>
                                                            <td>
                                                                <asp:TextBox ID="txtWeb" runat="server" BackColor="#FFFFCC" ReadOnly="true" CssClass="form-control" Width="90%"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%;">Email Syarikat:</td>
                                                            <td>
                                                                <asp:TextBox ID="txtEmailSya" runat="server" CssClass="form-control" Width="90%" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                        </tr>

                                                    </table>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="col">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                    Alamat Cawangan (Jika ada)
                                                </div>
                                                <div class="panel-body">
                                                    <table id="tableSpek" style="width: 100%;" class="table table-borderless">
                                                        <%--	<tr>
							<td colspan="2">
								<asp:CheckBox ID="chbalamatCaw" runat="server" Text=" Ada Cawangan?"/>
							</td>
						</tr>--%>
                                                        <tr>
                                                            <td style="width: 20%; vertical-align: top;" rowspan="2">Alamat:</td>
                                                            <td>
                                                                <asp:TextBox ID="txtAlamat1Caw" runat="server" Width="90%" CssClass="form-control" onkeyup="upper(this)" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtAlamat2Caw" runat="server" CssClass="form-control" Width="90%" AutoPostBack="true" onkeyup="upper(this)" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%;">Poskod:</td>
                                                            <td>
                                                                <asp:TextBox ID="txtPoskodCaw" runat="server" CssClass="form-control" Width="80px" TextMode="Number" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
                                                                &nbsp;&nbsp;&nbsp;
								Bandar &nbsp;
								<asp:TextBox ID="txtBandarCaw" runat="server" CssClass="form-control" Width="200px" onkeyup="upper(this)" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%;">Negeri:</td>
                                                            <td>
                                                                <asp:TextBox ID="txtNegeriCaw" runat="server" CssClass="form-control" Width="300px" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%;">Negara:</td>
                                                            <td>
                                                                <asp:TextBox ID="txtNegaraCaw" runat="server" CssClass="form-control" Width="300px" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%;">No. Telepon 1:</td>
                                                            <td>
                                                                <asp:TextBox ID="txtTelp1Caw" runat="server" CssClass="form-control" Width="100px" TextMode="Number" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%;">No. Telepon 2:</td>
                                                            <td>
                                                                <asp:TextBox ID="txtTelp2Caw" runat="server" CssClass="form-control" Width="100px" TextMode="Number" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%;">No. Faksimili:</td>
                                                            <td>
                                                                <asp:TextBox ID="txtFaxCaw" runat="server" CssClass="form-control" Width="100px" TextMode="Number" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%;">Laman Web URL:</td>
                                                            <td>
                                                                <asp:TextBox ID="txtWebCaw" runat="server" CssClass="form-control" Width="90%" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%;">Email:</td>
                                                            <td>
                                                                <asp:TextBox ID="txtEmailCaw" runat="server" CssClass="form-control" Width="90%" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                        </tr>

                                                    </table>
                                                </div>
                                            </div>

                                        </div>

                                    </div>


                                    <br />


                                </div>
                            </div>
                        </div>
                        <div class="row" style="text-align: center">                            
                                <asp:LinkButton ID="lbtnNextView1" runat="server" CssClass="btn btn-default btn-circle btn-lg" ToolTip="Seterusnya">
                                <i class="glyphicon glyphicon-chevron-right"></i>
                                </asp:LinkButton>
                        </div>

                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="lbtnNextView1" />
                </Triggers>
            </asp:UpdatePanel>


        </asp:View>
        <asp:View ID="View2" runat="server">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h4 class="panel-title">Maklumat Kakitangan Syarikat
                            </h4>
                        </div>
                        <div class="panel-body">
                            <asp:GridView ID="gvStaf" runat="server" ShowHeaderWhenEmpty="True" EmptyDataText=" no data" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="false"
                                CssClass="table table-striped table-bordered table-hover" HeaderStyle-BackColor="#6699FF" Width="100%" BorderStyle="Solid" ShowFooter="False" DataKeyNames="ROC04_ID">
                                <Columns>
                                    <asp:TemplateField HeaderText="Bil" ItemStyle-Width="2%" HeaderStyle-CssClass="centerAlign">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ROC04_Gelaran" HeaderText="Gelaran" ItemStyle-Width="5%" HeaderStyle-CssClass="centerAlign" />
                                    <asp:BoundField DataField="ROC04_Nama" HeaderText="Nama" ItemStyle-Width="20%" HeaderStyle-CssClass="centerAlign" />
                                    <asp:BoundField DataField="ROC04_IC" HeaderText="No KP/ Passport" ItemStyle-Width="5%" HeaderStyle-CssClass="centerAlign" />
                                    <asp:BoundField DataField="ROC04_Jwtn" HeaderText="Jawatan" ItemStyle-Width="10%" HeaderStyle-CssClass="centerAlign" />
                                    <asp:BoundField DataField="ROC04_Jbtn" HeaderText="Jabatan" ItemStyle-Width="10%" HeaderStyle-CssClass="centerAlign" />
                                    <asp:BoundField DataField="ROC04_NoTel" HeaderText="No Telp" ItemStyle-Width="5%" HeaderStyle-CssClass="centerAlign" />
                                    <asp:BoundField DataField="ROC04_JnsSijil" HeaderText="Sijil Tertinggi" ItemStyle-Width="5%" HeaderStyle-CssClass="centerAlign" />

                                </Columns>
                                <SelectedRowStyle BackColor="LightCyan" ForeColor="DarkBlue" Font-Bold="true" />
                            </asp:GridView>

                        </div>

                        <div class="panel panel-default" style="width: auto;">
                            <div class="panel-heading">
                                <h4 class="panel-title">Maklumat Lembaga Pengarah Syarikat
                                </h4>
                            </div>
                            <div class="panel-body">

                                <asp:GridView ID="gvStafLembPeng" runat="server" ShowHeaderWhenEmpty="True" EmptyDataText=" no data" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="false"
                                    CssClass="table table-striped table-bordered table-hover" HeaderStyle-BackColor="#6699FF" Width="100%" BorderStyle="Solid" ShowFooter="False" DataKeyNames="ROC04_ID">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Bil" ItemStyle-Width="2%" HeaderStyle-CssClass="centerAlign">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ROC04_Gelaran" HeaderText="Gelaran" ItemStyle-Width="5%" HeaderStyle-CssClass="centerAlign" />
                                        <asp:BoundField DataField="ROC04_Nama" HeaderText="Nama" ItemStyle-Width="20%" HeaderStyle-CssClass="centerAlign" />
                                        <asp:BoundField DataField="ROC04_IC" HeaderText="No Kad Pengenalan / Passport" ItemStyle-Width="10%" HeaderStyle-CssClass="centerAlign" />
                                        <%--<asp:BoundField DataField="ROC04_Passport" HeaderText="Passport" ItemStyle-Width="5%" HeaderStyle-CssClass="centerAlign"/>--%>
                                        <asp:BoundField DataField="ROC04_NoTel" HeaderText="No Telp" ItemStyle-Width="5%" HeaderStyle-CssClass="centerAlign" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                        <div class="panel panel-default" style="width: auto;">
                            <div class="panel-heading">
                                <h4 class="panel-title">Maklumat Pemegang Saham Syarikat
                                </h4>
                            </div>
                            <div class="panel-body">

                                <asp:GridView ID="gvStafPemegangSaham" runat="server" ShowHeaderWhenEmpty="True" EmptyDataText=" no data" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="false"
                                    CssClass="table table-striped table-bordered table-hover" HeaderStyle-BackColor="#6699FF" Width="100%" BorderStyle="Solid" ShowFooter="False" DataKeyNames="ROC04_ID">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Bil" ItemStyle-Width="2%" HeaderStyle-CssClass="centerAlign">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ROC04_Nama" HeaderText="Nama" ItemStyle-Width="20%" HeaderStyle-CssClass="centerAlign" />
                                        <asp:BoundField DataField="ROC04_IC" HeaderText="No KP / Passport / No Syarikat" ItemStyle-Width="10%" HeaderStyle-CssClass="centerAlign" />
                                        <asp:BoundField DataField="ROC04_NoTel" HeaderText="No Telp" ItemStyle-Width="5%" HeaderStyle-CssClass="centerAlign" />
                                        <asp:BoundField DataField="ROC04_Peratus" HeaderText="Peratusan (%)" ItemStyle-Width="5%" HeaderStyle-CssClass="centerAlign" DataFormatString="{0:P}" ItemStyle-HorizontalAlign="Right" />
                                    </Columns>

                                </asp:GridView>
                            </div>
                        </div>


                        <div class="row" style="text-align: center">
                            
                                <asp:LinkButton ID="lbtnPrevView2" runat="server" CssClass="btn btn-default btn-circle btn-lg" ToolTip="Sebelumnya">
                                <i class="glyphicon glyphicon-chevron-left"></i>
                                </asp:LinkButton>
                            
                            &nbsp;&nbsp;
                                <asp:LinkButton ID="lbtnNextView2" runat="server" CssClass="btn btn-default btn-circle btn-lg" ToolTip="Seterusnya" ValidationGroup="btnSimpan">
                                <i class="glyphicon glyphicon-chevron-right"></i>
                                </asp:LinkButton>
                            
                        </div>

                    </div>



                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="lbtnPrevView2" />
                    <asp:PostBackTrigger ControlID="lbtnNextView2" />
                </Triggers>
            </asp:UpdatePanel>


        </asp:View>
        <asp:View ID="View3" runat="server">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <div class="panel panel-group" style="width: auto;">

                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">Maklumat Pendaftaran Perniagaan
                                </h4>
                            </div>
                            <div class="panel-body">

                                <asp:GridView ID="gvLesen" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText=" tiada rekod" AllowSorting="True" AllowPaging="false" Font-Size="8pt"
                                    CssClass="table table-striped table-bordered table-hover" HeaderStyle-BackColor="#6699FF" Width="95%" BorderStyle="Solid" ShowFooter="False" DataKeyNames="ROC02_ID">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Bil" ItemStyle-Width="2%" HeaderStyle-CssClass="centerAlign">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="KodDaftar" HeaderText="Kod Daftar" ItemStyle-Width="5%" HeaderStyle-CssClass="centerAlign" />
                                        <asp:BoundField DataField="ROC02_NoDaftar" HeaderText="No Pendaftaran" ItemStyle-Width="20%" HeaderStyle-CssClass="centerAlign" />
                                        <asp:BoundField DataField="TkhMula" HeaderText="Tarikh Berkuatkuasa" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="10%" HeaderStyle-CssClass="centerAlign" />
                                        <asp:BoundField DataField="TkhTmt" HeaderText="Tarikh Tamat" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="10%" HeaderStyle-CssClass="centerAlign" />

                                    </Columns>
                                    <SelectedRowStyle BackColor="LightCyan" ForeColor="DarkBlue" Font-Bold="true" />

                                </asp:GridView>

                            </div>
                        </div>

                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" href="#collapse1">Maklumat Bidang Perolehan Bekalan &amp; Perkhidmatan (MOF)</a>
                                </h4>
                            </div>
                            <div id="collapse1" class="panel-collapse collapse in">
                                <div class="panel-body">

                                    <br />
                                    <asp:GridView ID="gvSyarikatBidang" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="false" EmptyDataText=" Tiada rekod"
                                        CssClass="table table-striped table-bordered table-hover" Width="100%" BorderStyle="Solid" HeaderStyle-BackColor="#FECB18" ShowFooter="False" DataKeyNames="ROC03_KodBidang">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Bil" ItemStyle-Width="2%" HeaderStyle-CssClass="centerAlign">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ROC03_KodBidang" HeaderText="Kod Bidang" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Butiran" HeaderText="Bidang" ItemStyle-Width="50%" />

                                        </Columns>
                                    </asp:GridView>

                                </div>
                            </div>
                        </div>

                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">

                                    <a data-toggle="collapse" href="#collapse2">Maklumat Bidang Perolehan Kerja (CIDB)</a>
                                </h4>
                            </div>
                            <div id="collapse2" class="panel-collapse collapse in">
                                <div class="panel-body">

                                    <asp:GridView ID="gvSyarikatCIDB" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="false" Font-Size="8pt"
                                        CssClass="table table-striped table-bordered table-hover" Width="100%" BorderStyle="Solid" ShowFooter="False" EmptyDataText=" Tiada rekod" DataKeyNames="ROC07_KodKhusus">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Bil" ItemStyle-Width="2%" HeaderStyle-CssClass="centerAlign">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="ROC07_KodKategori" HeaderText="Kod Kategori" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="centerAlign" />
                                            <asp:BoundField DataField="ROC07_KodKhusus" HeaderText="Kod Khusus" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="centerAlign" />
                                            <asp:BoundField DataField="Butiran" HeaderText="Pengkhususan" ItemStyle-Width="30%" HeaderStyle-CssClass="centerAlign" />

                                        </Columns>
                                    </asp:GridView>

                                </div>

                            </div>
                        </div>

                        <div class="row"  style="text-align: center">
                            
                                <asp:LinkButton ID="lbtnPrevView3" runat="server" CssClass="btn btn-default btn-circle btn-lg" ToolTip="Sebelumnya">
                                <i class="glyphicon glyphicon-chevron-left"></i>
                                </asp:LinkButton>
                            &nbsp;&nbsp;
                                <asp:LinkButton ID="lbtnNextView3" runat="server" CssClass="btn btn-default btn-circle btn-lg" ToolTip="Seterusnya">
                                <i class="glyphicon glyphicon-chevron-right"></i>
                                </asp:LinkButton>
                                
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="lbtnNextView3" />
                    <asp:PostBackTrigger ControlID="lbtnPrevView3" />
                </Triggers>
            </asp:UpdatePanel>
        </asp:View>


        <asp:View ID="View4" runat="server">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h4 class="panel-title">Dokumen
                    </h4>
                </div>
                <div class="panel-body">
                    <table class="table table-borderless table-striped">

                        <tr>
                            <td style="width: 20%; vertical-align: top">Salinan Kelulusan Pendaftaran GST dari Kastam:
                            </td>

                            <td>
                                <asp:GridView ID="gvGST" runat="server" AutoGenerateColumns="false" EmptyDataText=" Tiada rekod" Width="80%" CssClass="table table-striped table-bordered table-hover"
                                    HeaderStyle-BackColor="#6699FF" Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" DataKeyNames="ROC09_ID" ShowHeaderWhenEmpty="true">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Bil" ItemStyle-Width="2%">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ROC09_NamaDok" HeaderText="File Name" ItemStyle-Width="70%" />
                                        <asp:HyperLinkField DataNavigateUrlFields="ROC01_IDSem,ROC09_NamaDok" DataNavigateUrlFormatString="~/Upload/Vendor/Lesen/{0}/{1}" DataTextField="ROC09_ID" DataTextFormatString="&lt;img src='../../../Images/search.png' alt='View' /&gt;" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%" Target="_blank" />

                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top;">Dokumen Perakuan Pendaftaran:
                            </td>
                            <td>
                                <%--<div class="form-inline">
                                        <asp:FileUpload ID="fuSijil" runat="server" CssClass="form-control" Width="350px" />
                                        <asp:LinkButton ID="lbtnSijil" runat="server" CssClass="btn btn-info" ToolTip="Upload file" ValidationGroup="lbtnTambahStaf">
											<i class="fa fa-upload fa-lg"></i>&nbsp;&nbsp;&nbsp;Upload
										</asp:LinkButton>
                                    </div>
                                    <br />--%>
                                <asp:Label ID="lblMsgSijil" runat="server" />
                                <br />
                                <asp:GridView ID="gvSijilLesen" runat="server" AutoGenerateColumns="false" BorderColor="#333333" BorderStyle="Solid" CssClass="table table-striped table-bordered table-hover"
                                    DataKeyNames="ROC02_ID" EmptyDataText=" Tiada rekod" HeaderStyle-BackColor="#6699FF" ShowHeaderWhenEmpty="true" Width="95%" Font-Size="8pt">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="centerAlign" HeaderText="Bil" ItemStyle-Width="2%">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="KodDaftar" HeaderText="Kod Daftar" ItemStyle-Width="5%" ReadOnly="true" HeaderStyle-CssClass="centerAlign" />
                                        <asp:BoundField DataField="ROC02_NoDaftar" HeaderText="No Pendaftaran" ItemStyle-Width="10%" ReadOnly="true" HeaderStyle-CssClass="centerAlign" />
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIDNamaDok" runat="server" Text='<%# Eval("ROC09_NamaDok")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIDLamp" runat="server" Text='<%# Eval("ROC09_ID")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ROC01_IDSem" ItemStyle-Width="1%" Visible="false" />
                                        <asp:TemplateField HeaderText="Nama Dokumen" ItemStyle-Width="50%">
                                            <ItemTemplate>
                                                <%# Eval("ROC09_NamaDok") %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:FileUpload ID="fuSijil" runat="server" CssClass="form-control" Width="350px" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:HyperLinkField DataNavigateUrlFields="ROC01_IDSem,ROC09_NamaDok" DataNavigateUrlFormatString="~/Upload/Vendor/Lesen/{0}/{1}" DataTextField="ROC09_ID" DataTextFormatString="&lt;img src='../../../Images/search.png' alt='View' /&gt;" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%" Target="_blank" />


                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top;">Dokumen Sijil Tertinggi Staf :
                            </td>
                            <td>
                                <%--<div class="form-inline">
                                        <asp:FileUpload ID="fuSijil" runat="server" CssClass="form-control" Width="350px" />
                                        <asp:LinkButton ID="lbtnSijil" runat="server" CssClass="btn btn-info" ToolTip="Upload file" ValidationGroup="lbtnTambahStaf">
											<i class="fa fa-upload fa-lg"></i>&nbsp;&nbsp;&nbsp;Upload
										</asp:LinkButton>
                                    </div>
                                    <br />--%>
                                <asp:Label ID="lblMsgSijilStaf" runat="server" />
                                <br />
                                <asp:GridView ID="gvLampSijilStaf" runat="server" AutoGenerateColumns="false" BorderColor="#333333" BorderStyle="Solid" CssClass="table table-striped table-bordered table-hover"
                                    DataKeyNames="ROC04_ID" EmptyDataText=" Tiada rekod" HeaderStyle-BackColor="#6699FF" ShowHeaderWhenEmpty="true" Width="95%" Font-Size="8pt">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="centerAlign" HeaderText="Bil" ItemStyle-Width="2%">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ROC04_Nama" HeaderText="Nama" ItemStyle-Width="30%" HeaderStyle-CssClass="centerAlign" />
                                        <asp:BoundField DataField="ROC04_JnsSijil" HeaderText="Sijil Tertinggi" ItemStyle-Width="5%" HeaderStyle-CssClass="centerAlign" />
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIDNamaDok" runat="server" Text='<%# Eval("ROC09_NamaDok")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIDLamp" runat="server" Text='<%# Eval("ROC09_ID")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ROC01_IDSem" ItemStyle-Width="1%" Visible="false" />
                                        <asp:TemplateField HeaderText="Nama Dokumen" ItemStyle-Width="30%">
                                            <ItemTemplate>
                                                <%# Eval("ROC09_NamaDok") %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:FileUpload ID="fuSijil" runat="server" CssClass="form-control" Width="350px" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:HyperLinkField DataNavigateUrlFields="ROC01_IDSem,ROC09_NamaDok" DataNavigateUrlFormatString="~/Upload/Vendor/Lesen/{0}/{1}" DataTextField="ROC09_ID" DataTextFormatString="&lt;img src='../../../Images/search.png' alt='View' /&gt;" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%" Target="_blank" />

                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>

                    </table>

                    <br />
                    <br />
                    <div class="row" style="text-align: center">                        
                            <asp:LinkButton ID="lbtnPrevView4" runat="server" CssClass="btn btn-default btn-circle btn-lg" ToolTip="Sebelumnya">
                                <i class="glyphicon glyphicon-chevron-left"></i>
                            </asp:LinkButton>                       
                    </div>
                </div>
            </div>
           

        </asp:View>


    </asp:MultiView>



    <asp:Button ID="btnOpenSenarai" runat="server" Text="" Style="display: none;" />
    <ajaxToolkit:ModalPopupExtender ID="MPESenarai" runat="server" BackgroundCssClass="modalBackground" BehaviorID="mpe7"
        CancelControlID="btnCloseSenarai" PopupControlID="pnlSenarai" TargetControlID="btnOpenSenarai">
    </ajaxToolkit:ModalPopupExtender>

    <asp:Panel ID="pnlSenarai" runat="server" BackColor="White" Width="60%" Height="70%" Style="overflow: auto;">
        <asp:UpdatePanel runat="server" ID="UpdatePanel7">
            <ContentTemplate>
                <table style="border: Solid 3px #D5AA00; width: 100%; height: 100%;">

                    <tr style="background: linear-gradient(to bottom, #fceabb 0%,#fccd4d 50%,#f8b500 51%,#fbdf93 100%);">
                        <td colspan="2" style="height: 10%; font-weight: bold; text-align: center;">Maklumat Lanjut </td>
                        <td style="width: 10px; text-align: center;">
                            <button id="btnCloseSenarai" runat="server" class="btnNone " title="Tutup" onclick="fCloseSenarai();">
                                <i class="far fa-window-close fa-2x"></i>
                            </button>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="3" style="height: 30px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Jumlah rekod :&nbsp;<label id="lblJumRekodSenarai" runat="server" class="control-label" style="color: mediumblue;"></label>
                            &nbsp;|&nbsp;&nbsp;Saiz Rekod : &nbsp;&nbsp;                           
                               <asp:DropDownList ID="ddlSaizRekodSenarai" runat="server" AutoPostBack="true" class="form-control" Width="60px">
                                   <asp:ListItem Text="10" Value="10" />
                                   <asp:ListItem Text="25" Value="25" Selected="true" />
                                   <asp:ListItem Text="50" Value="50" />
                                   <asp:ListItem Text="100" Value="100" />
                                   <asp:ListItem Text="200" Value="200" />
                                   <asp:ListItem Text="500" Value="500" />
                                   <asp:ListItem Text="1000" Value="1000" />
                               </asp:DropDownList>                            
                        </td>
                    </tr>


                    <tr style="vertical-align: top;">

                        <td colspan="3">
                            <div style="margin-bottom: 10px; margin-top: 10px; margin-left: 10px; margin-right: 10px;">
                                <asp:GridView ID="gvSenarai" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="true" AllowPaging="true" EmptyDataText=" Tiada rekod"
                                    CssClass="table table-striped table-bordered table-hover" Width="100%" BorderStyle="Solid" ShowFooter="false" Font-Size="8pt" PageSize="25">
                                    <Columns>                                        
                                        <asp:TemplateField HeaderText="Bil" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Tarikh" DataField="ROC10_Tkh" SortExpression="ROC10_Tkh" DataFormatString="{0:dddd, dd MMMM yyyy, hh:mm tt}"/>
                                        <asp:BoundField HeaderText="Ulasan" DataField="ROC10_Ulasan" SortExpression="ROC10_Ulasan"  />                                     
                                        
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

