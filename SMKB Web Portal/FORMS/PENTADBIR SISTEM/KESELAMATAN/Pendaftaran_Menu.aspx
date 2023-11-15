<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Pendaftaran_Menu.aspx.vb" Inherits="SMKB_Web_Portal.Pendaftaran_Menu" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" type="text/css" href="../../../Content/Site.css" />
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>
    <script type="text/javascript">

        function copyText3() {
            src = document.getElementById('<%=txtNamaModul.ClientID%>');
            dest = document.getElementById('<%=txtNPModul.ClientID%>');
            src.value = src.value.toUpperCase();
            dest.value = src.value.toUpperCase();

        }

        function copyText1() {
            src = document.getElementById('<%=txtNamaSubMenu.ClientID%>');
            dest = document.getElementById('<%=txtNPSubMenu.ClientID%>');
            src.value = src.value.toUpperCase();
            dest.value = src.value.toUpperCase();

        }

        function copyText2() {
            src = document.getElementById('<%=txtNamaSubModul.ClientID%>');
            dest = document.getElementById('<%=txtNPSubModul.ClientID%>');
            src.value = src.value.toUpperCase();
            dest.value = src.value.toUpperCase();

        }

        function fConfirm() {
            try {
                var blnComplete = true;

                var txtKodModul = document.getElementById('<%=txtKodModul.ClientID%>')
                if (txtKodModul.readOnly == true) {
                    alert('Sila klik button \'Rekod baru\' untuk menambah rekod baru!')
                    return false;
                }


                //Kod Modul
                if (txtKodModul.value == "") {
                    blnComplete = false
                    document.getElementById("lblMsgKodModul").style.display = 'inline-block';
                }
                else {
                    document.getElementById("lblMsgKodModul").style.display = 'none';
                }

                //Nama Modul
                var txtNamaModul = document.getElementById('<%=txtNamaModul.ClientID%>')
                if (txtNamaModul.value == "") {
                    blnComplete = false
                    document.getElementById("lblMsgNamaModul").style.display = 'inline-block';
                }
                else {
                    document.getElementById("lblMsgNamaModul").style.display = 'none';
                }

                //Nama Paparan Modul
                var txtNPModul = document.getElementById('<%=txtNPModul.ClientID%>')
                if (txtNPModul.value == "") {
                    blnComplete = false
                    document.getElementById("lblMsgNPModul").style.display = 'inline-block';
                }
                else {
                    document.getElementById("lblMsgNPModul").style.display = 'none';
                }

                if (blnComplete == false) {
                    alert('Sila lengkapkan maklumat!')
                    return false;
                }

                if (confirm('Anda pasti untuk simpan rekod ini?')) {
                    return true;
                } else {
                    return false;
                }
            }
            catch (err) {
                alert(err)
                return false;
            }
        }

        function fConfirmDel() {
            try {

                var txtKodModul = document.getElementById('<%=txtKodModul.ClientID%>')
                if (txtKodModul.value == "") {
                    alert('Sila pilih rekod yang hendak dihapuskan!')
                    return false;
                }

                if (confirm('Anda pasti untuk hapuskan rekod ini?')) {
                    return true;
                } else {
                    return false;
                }

            }
            catch (err) {
                alert(err)
                return false;
            }
        }

        function fConfirmSubModul() {
            try {
                var blnComplete = true;

                var txtKodSubModul = document.getElementById('<%=txtKodSubModul.ClientID%>')
                if (txtKodSubModul.value == "") {
                    alert('Sila klik button \'Rekod baru\' untuk menambah rekod baru!')
                    return false;
                }

                //Nama SubModul
                var txtNamaSubModul = document.getElementById('<%=txtNamaSubModul.ClientID%>')
                if (txtNamaSubModul.value == "") {
                    blnComplete = false
                    document.getElementById("lblMsgNamaSubModul").style.display = 'inline-block';
                }
                else {
                    document.getElementById("lblMsgNamaSubModul").style.display = 'none';
                }

                //Nama Paparan SubModul
                var txtNPSubModul = document.getElementById('<%=txtNPSubModul.ClientID%>')
                if (txtNPSubModul.value == "") {
                    blnComplete = false
                    document.getElementById("lblMsgNPSubModul").style.display = 'inline-block';
                }
                else {
                    document.getElementById("lblMsgNPSubModul").style.display = 'none';
                }

                if (blnComplete == false) {
                    alert('Sila lengkapkan maklumat!')
                    return false;
                }

                if (confirm('Anda pasti untuk simpan rekod ini?')) {
                    return true;
                } else {
                    return false;
                }
            }
            catch (err) {
                alert(err)
                return false;
            }
        }

        function fConfirmDelSubModul() {
            try {

                var txtKodSubModul = document.getElementById('<%=txtKodSubModul.ClientID%>')
                if (txtKodSubModul.value == "") {
                    alert('Sila pilih rekod yang hendak dihapuskan!')
                    return false;
                }

                if (confirm('Anda pasti untuk hapuskan rekod ini?')) {
                    return true;
                } else {
                    return false;
                }

            }
            catch (err) {
                alert(err)
                return false;
            }
        }

        function fConfirmSubMenu() {
            try {
                var blnComplete = true;

                var txtKodSubMenu = document.getElementById('<%=txtKodSubMenu.ClientID%>')
                if (txtKodSubMenu.value == "") {
                    alert('Sila klik button \'Rekod baru\' untuk menambah rekod baru!')
                    return false;
                }

                //Nama SubModul
                var txtNamaSubMenu = document.getElementById('<%=txtNamaSubMenu.ClientID%>')
                if (txtNamaSubMenu.value == "") {
                    blnComplete = false
                    document.getElementById("lblMsgNamaSubMenu").style.display = 'inline-block';
                }
                else {
                    document.getElementById("lblMsgNamaSubMenu").style.display = 'none';
                }

                //Nama Paparan SubModul
                var txtNPSubMenu = document.getElementById('<%=txtNPSubMenu.ClientID%>')
                if (txtNPSubMenu.value == "") {
                    blnComplete = false
                    document.getElementById("lblMsgNPSubMenu").style.display = 'inline-block';
                }
                else {
                    document.getElementById("lblMsgNPSubMenu").style.display = 'none';
                }

                if (blnComplete == false) {
                    alert('Sila lengkapkan maklumat!')
                    return false;
                }

                if (confirm('Anda pasti untuk simpan rekod ini?')) {
                    return true;
                } else {
                    return false;
                }
            }
            catch (err) {
                alert(err)
                return false;
            }
        }

        function fConfirmDelSubMenu() {
            try {

                var txtKodSubMenu = document.getElementById('<%=txtKodSubMenu.ClientID%>')
                if (txtKodSubMenu.value == "") {
                    alert('Sila pilih rekod yang hendak dihapuskan!')
                    return false;
                }

                if (confirm('Anda pasti untuk hapuskan rekod ini?')) {
                    return true;
                } else {
                    return false;
                }

            }
            catch (err) {
                alert(err)
                return false;
            }
        }

        function fBaruSubModul() {
            try {

                var ddlKodModul2 = document.getElementById("<%=ddlKodModul2.ClientID %>");
                var intSelKodModul = ddlKodModul2.selectedIndex

                if (intSelKodModul == 0 || intSelKodModul == -1) {
                    alert('Sila pilih \'Kod Modul\'!')
                    return false;
                } else {
                    return true;
                }
            }
            catch (err) {
                alert(err)
                return false;
            }
        }

        function fBaruSubMenu() {
            try {
                var blnSuccess = true;
                var ddlKodModul = document.getElementById("<%=ddlKodModul.ClientID %>");
                var intSelKodModul = ddlKodModul.selectedIndex

                if (intSelKodModul == 0 || intSelKodModul == -1) {
                    blnSuccess = false;
                }

                var ddlKodSubModul = document.getElementById("<%=ddlKodSubModul.ClientID %>");
                var intSelKodSubModul = ddlKodSubModul.selectedIndex

                if (intSelKodSubModul == 0 || intSelKodSubModul == -1) {
                    blnSuccess = false;
                }

                if (blnSuccess == false) {
                    alert('Sila pilih \'Kod Modul\' dan \'Kod Sub Modul\'!')
                    return false
                }
                else {
                    return true
                }
            }
            catch (err) {
                alert(err)
                return false;
            }
        }
    </script>

    <style>
        .ajax__tab_xp .ajax__tab_body {
            font-size: 9pt;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row">
                <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" 
                    Width="100%" CssClass="tabCtrl" ActiveTabIndex="0" AutoPostBack="true">
                    <ajaxToolkit:TabPanel ID="tabModul" runat="Server" HeaderText="Daftar Modul">
                        <HeaderTemplate>
                            Daftar Modul
                        </HeaderTemplate>
                        <ContentTemplate>
                           <div class="row">
                                <div class="panel panel-default" style="width: 80%">
                                    <div class="panel-body">
                                        <table class="nav-justified">
                                            <tr style="height: 25px">
                                                <td>
                                                    <label class="control-label" for="">Kod Modul:</label></td>
                                                <td>
                                                    <asp:TextBox ID="txtKodModul" runat="server" class="form-control" Width="20%" MaxLength="2" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                                    <i aria-hidden="true" class="fas fa-info-circle fa-lg" data-placement="right" style="cursor: pointer; color: #327aef;" data-toggle="tooltip" title="Masukkan 2 digit numerik sahaja."></i>
                                                    &nbsp;<label id="lblMsgKodModul" class="control-label" for="" style="display: none; color: #820303;">
                                                        (Masukkan Kod Modul)
                                                    </label>

                                                </td>
                                            </tr>
                                            <tr style="height: 25px">
                                                <td style="width: 116px; height: 22px;">
                                                    <label class="control-label" for="Butiran">
                                                    Nama Modul:               
                                                </td>
                                                <td style="height: 22px">
                                                    <asp:TextBox ID="txtNamaModul" runat="server" class="form-control" Width="60%" onkeyup="copyText3()"></asp:TextBox>
                                                    &nbsp;
                                                            <label id="lblMsgNamaModul" class="control-label" for="" style="display: none; color: #820303;">
                                                                *Masukkan Nama Modul
                                                            </label>
                                                </td>
                                            </tr>
                                            <tr style="height: 25px">
                                                <td style="width: 116px; height: 19px;">
                                                    <label class="control-label" for="Butiran">
                                                        Nama Paparan :</label></td>
                                                <td style="height: 19px">
                                                    <asp:TextBox ID="txtNPModul" runat="server" class="form-control" Width="60%"></asp:TextBox>
                                                    &nbsp;
                              <label id="lblMsgNPModul" class="control-label" for="" style="display: none; color: #820303;">
                                  *Masukkan Nama Paparan
                              </label>
                                                </td>
                                            </tr>
                                            <tr style="height: 25px">
                                                <td style="width: 116px">
                                                    <label class="control-label" for="Butiran">
                                                        Urutan :</label></td>
                                                <td>
                                                    <asp:TextBox ID="txtUrutanModul" runat="server" class="form-control" ReadOnly="True" Width="15%"></asp:TextBox>
                                                </td>
                                            </tr>

                                            <tr style="height: 25px;">
                                                <td style="width: 116px;">Status :</td>
                                                <td>
                                                    <asp:RadioButtonList ID="rbStatModul" runat="server" Height="25px" RepeatDirection="Horizontal" Width="273px">
                                                        <asp:ListItem Selected="True" Text=" Aktif" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text=" Tidak Aktif" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 116px; height: 25px;">&nbsp;</td>
                                                <td style="height: 25px">
                                                    <asp:LinkButton ID="lbtnSimpanModul" runat="server" CssClass="btn btn-info" OnClientClick="return fConfirm()">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                                                    </asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp; &nbsp;
                          <asp:LinkButton ID="lbtnHapusModul" runat="server" CssClass="btn btn-info" OnClientClick="return fConfirmDel()">
						<i class="fas fa-trash-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Hapus
                          </asp:LinkButton>
                                                    &nbsp;&nbsp;<asp:LinkButton ID="lbtnBaru" runat="server" CssClass="btn btn-info">
						<i class="fas fa-file-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Rekod Baru
                                                    </asp:LinkButton>
                                                </td>
                                            </tr>



                                        </table>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="GvTopPanel" style="height: 33px;">
                                        <div style="float: left; margin-top: 5px; margin-left: 10px;">
                                            <label class="control-label" for="">Jumlah rekod :</label>&nbsp;<label class="control-label" id="lblJumMod" runat="server" style="color: mediumblue;"></label>

                                            &nbsp;&nbsp;   <b style="color: #969696;">|</b> &nbsp;&nbsp;

                    <label class="control-label" for="Klasifikasi">Saiz Rekod : </label>
                                            <asp:DropDownList ID="ddlSaizRekod" runat="server" Width="50px" AutoPostBack="True" class="form-control">
                                                <asp:ListItem Text="10" Value="10" />
                                                <asp:ListItem Text="25" Value="25" Selected="True" />
                                                <asp:ListItem Text="50" Value="50" />
                                                <asp:ListItem Text="100" Value="100" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    
                                    <asp:GridView ID="gvModul" runat="server" AllowPaging="True"
                                        AllowSorting="True" AutoGenerateColumns="False"
                                        EmptyDataText=" "
                                        BorderColor="#333333" BorderStyle="Solid"
                                        CssClass="table table-striped table-bordered table-hover"
                                        Font-Size="8pt" PageSize="25" ShowHeaderWhenEmpty="True"
                                        Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Bil">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBil" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle Width="5%" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblKodModul" runat="server" Text="Kod Modul" />&nbsp;
                        <asp:LinkButton ID="lnkKodModul" runat="server" CommandName="Sort" CommandArgument="KodModul"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblKodModul" runat="server" Text='<%# Eval("KodModul")%>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="NamaModul" HeaderText="Nama Modul" SortExpression="NamaModul">
                                                <ControlStyle Width="95%" />
                                                <ItemStyle Width="20%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DisModul" HeaderText="Nama Paparan" SortExpression="DisModul">
                                                <ItemStyle Width="20%" />
                                            </asp:BoundField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblUrutan" runat="server" Text="Urutan" />&nbsp;
                        <asp:LinkButton ID="lnkUrutan" runat="server" CommandName="Sort" CommandArgument="Urutan"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUrutan" runat="server" Text='<%# Eval("Urutan")%>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblStatus" runat="server" Text="Status" />&nbsp;
                        <asp:LinkButton ID="lnkStatus" runat="server" CommandName="Sort" CommandArgument="Status"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" runat="server" Text='<%#IIf(CBool(Eval("Status")), "Aktif", "Tidak Aktif") %>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="btn-xs" ToolTip="Kemaskini">
                          <i class="fas fa-edit"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="5%" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle BackColor="#6699FF" />
                                        <RowStyle Height="5px" />
                                        <SelectedRowStyle ForeColor="Blue" />
                                    </asp:GridView>
                                </div>
                               </div>
                           
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>

                    <ajaxToolkit:TabPanel ID="tabSubModul" runat="Server" HeaderText="Daftar Sub Modul">
                        <ContentTemplate>
                            <div class="row">

                                <div class="panel panel-default" style="width: 80%">
                                    <div class="panel-body">
                                        <table class="nav-justified">
                                            <tr style="height: 25px">
                                                <td style="width: 116px">
                                                    <label class="control-label" for="">Kod Modul:</label></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlKodModul2" runat="server" AutoPostBack="True" CssClass="form-control" Style="width: 280px;">
                                                    </asp:DropDownList>
                                                </td>

                                            </tr>
                                            <tr style="height: 25px">
                                                <td style="width: 116px; height: 22px;">
                                                    <label class="control-label" for="Butiran">
                                                    Kod Sub Modul: </td>
                                                <td style="height: 22px">
                                                    <asp:TextBox ID="txtKodSubModul" runat="server" class="form-control" Width="100px" ReadOnly="true"></asp:TextBox>
                                                &nbsp;
                                            </tr>

                                            <tr style="height: 25px">
                                                <td style="width: 150px">
                                                    <label class="control-label" for="Butiran">
                                                        Nama Sub Modul :</label></td>
                                                <td>
                                                    <asp:TextBox ID="txtNamaSubModul" runat="server" class="form-control" Width="60%" onkeyup="copyText2()"></asp:TextBox>
                                                    &nbsp;<label id="lblMsgNamaSubModul" class="control-label" for="" style="display: none; color: #820303;">
                                                        *Masukkan Nama Sub Modul
                                                    </label>

                                                </td>
                                            </tr>
                                            <tr style="height: 25px">
                                                <td style="width: 116px; height: 19px;">
                                                    <label class="control-label" for="Butiran">
                                                        Nama Paparan :</label></td>
                                                <td style="height: 19px">
                                                    <asp:TextBox ID="txtNPSubModul" runat="server" class="form-control" Width="60%"></asp:TextBox>
                                                    &nbsp;<label id="lblMsgNPSubModul" class="control-label" for="" style="display: none; color: #820303;">
                                                        *Masukkan Nama Paparan
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr style="height: 25px">
                                                <td style="width: 116px">
                                                    <label class="control-label" for="Butiran">
                                                        Status Sub Menu :</label></td>
                                                <td>
                                                    <asp:RadioButtonList ID="rbStatSubModul" runat="server" Height="25px" Width="273px" RepeatDirection="Horizontal">
                                                        <asp:ListItem Selected="True" Text=" Aktif" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text=" Tidak Aktif" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td style="width: 116px; height: 25px;">
                                                    <label class="control-label" for="Butiran">
                                                        Urutan :</label></td>
                                                <td style="height: 25px">
                                                    <asp:TextBox ID="txtUrutanSubModul" runat="server" class="form-control" ReadOnly="True" Width="10%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 116px; height: 25px;">&nbsp;</td>
                                                <td style="height: 25px">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="width: 116px; height: 25px;">&nbsp;</td>
                                                <td style="height: 25px">
                                                    <asp:LinkButton ID="lbtnSimpanSubModul" runat="server" CssClass="btn btn-info" OnClientClick="return fConfirmSubModul()">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                                                    </asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp; &nbsp;
                          <asp:LinkButton ID="lbtnHapusSubModul" runat="server" CssClass="btn btn-info" OnClientClick="return fConfirmDelSubModul()">
						<i class="fas fa-trash-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Hapus
                          </asp:LinkButton>
                                                    &nbsp;&nbsp;<asp:LinkButton ID="lbtnBaruSubModul" runat="server" CssClass="btn btn-info" OnClientClick="return fBaruSubModul()">
						<i class="fas fa-file-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Rekod Baru
                                                    </asp:LinkButton>
                                                </td>
                                            </tr>

                                        </table>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="GvTopPanel" style="height: 33px;">
                                        <div style="float: left; margin-top: 5px; margin-left: 10px;">
                                            <label class="control-label" for="">Jumlah rekod :</label>&nbsp;<label class="control-label" id="lblJumSubMod" runat="server" style="color: mediumblue;"></label>

                                            &nbsp;&nbsp;   <b style="color: #969696;">|</b> &nbsp;&nbsp;

                    <label class="control-label" for="Klasifikasi">
                        Saiz Rekod : 
                    <asp:DropDownList ID="ddlSaizRekodSubModul" runat="server" AutoPostBack="True" class="form-control" Width="50px">
                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                        <asp:ListItem Selected="True" Text="25" Value="25"></asp:ListItem>
                        <asp:ListItem Text="50" Value="50"></asp:ListItem>
                        <asp:ListItem Text="100" Value="100"></asp:ListItem>
                    </asp:DropDownList>
                    </label>
                                        </div>
                                    </div>
                                    <asp:GridView ID="gvSubModul" runat="server" AllowPaging="True" 
                                        AllowSorting="True" AutoGenerateColumns="False" EmptyDataText=" "
                                        BorderColor="#333333" BorderStyle="Solid" 
                                        CssClass="table table-striped table-bordered table-hover" 
                                        Font-Size="8pt" PageSize="25" ShowHeaderWhenEmpty="True"
                                        Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Bil">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBil" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle Width="5%" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <%--<asp:BoundField DataField="KodSubModul" HeaderText="Kod Sub Modul" ReadOnly="True" SortExpression="KodSubModul">
							<ControlStyle Width="50px" />
							<ItemStyle Width="10%" HorizontalAlign="Center" />
							</asp:BoundField>--%>

                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblKodSubModul" runat="server" Text="Kod Submodul" />&nbsp;
                        <asp:LinkButton ID="lnkKodSubModul" runat="server" CommandName="Sort" CommandArgument="KodSubModul"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblKodSubModul" runat="server" Text='<%# Eval("KodSub")%>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="NamaSub" HeaderText="Nama Sub Modul" SortExpression="NamaSub">
                                                <ControlStyle Width="95%" />
                                                <ItemStyle Width="25%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DisSub" HeaderText="Nama Paparan" SortExpression="DisSub">
                                                <ItemStyle Width="25%" />
                                            </asp:BoundField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblUrutan" runat="server" Text="Urutan" />&nbsp;
                        <asp:LinkButton ID="lnkUrutan" runat="server" CommandName="Sort" CommandArgument="Urutan"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUrutan" runat="server" Text='<%# Eval("Urutan")%>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblStatus" runat="server" Text="Status" />&nbsp;
                        <asp:LinkButton ID="lnkStatus" runat="server" CommandName="Sort" CommandArgument="Status"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" runat="server" Text='<%#IIf(CBool(Eval("Status")), "Aktif", "Tidak Aktif") %>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="15%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="btn-xs" ToolTip="Kemaskini">
                          <i class="fas fa-edit fa-lg"></i>
                                                    </asp:LinkButton>

                                                </ItemTemplate>
                                                <ItemStyle Width="5%" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle BackColor="#6699FF" />
                                        <RowStyle Height="5px" />
                                        <SelectedRowStyle ForeColor="Blue" />
                                    </asp:GridView>
                                </div>
                            </div>

                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>

                    <ajaxToolkit:TabPanel ID="tabSubMenu" runat="Server" HeaderText="Daftar Sub Menu">
                        <ContentTemplate>

                            <div class="row">
                                <div class="panel panel-default" style="width: 80%">
                                    <div class="panel-body">
                                        <table class="nav-justified">
                                            <tr style="height: 25px">
                                                <td style="width: 150px">
                                                    <label class="control-label" for="">Kod Modul:</label></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlKodModul" runat="server" AutoPostBack="True" CssClass="form-control" Style="width: 280px;">
                                                    </asp:DropDownList>

                                                </td>

                                            </tr>
                                            <tr style="height: 25px">
                                                <td style="width: 116px; height: 22px;">
                                                    <label class="control-label" for="Butiran">
                                                    Kod Sub Modul:
					  
                                                </td>
                                                <td style="height: 22px">
                                                    <asp:DropDownList ID="ddlKodSubModul" runat="server" AutoPostBack="True" CssClass="form-control" Style="width: 350px;">
                                                    </asp:DropDownList></td>


                                            </tr>

                                            <tr style="height: 25px">
                                                <td style="width: 116px; height: 19px;">
                                                    <label class="control-label" for="Butiran">
                                                        Kod Sub Menu :</label></td>
                                                <td style="height: 19px">
                                                    <asp:TextBox ID="txtKodSubMenu" runat="server" class="form-control" ReadOnly="True" Width="100px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr style="height: 25px">
                                                <td style="width: 116px">
                                                    <label class="control-label" for="Butiran">
                                                        Nama Sub Menu :</label></td>
                                                <td>
                                                    <asp:TextBox ID="txtNamaSubMenu" runat="server" class="form-control" onkeyup="copyText1()" Width="60%"></asp:TextBox>
                                                    &nbsp;<label id="lblMsgNamaSubMenu" class="control-label" for="" style="display: none; color: #820303;">
                                                        *Masukkan Nama Sub Menu
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr style="height: 25px">
                                                <td style="width: 116px; height: 19px;">
                                                    <label class="control-label" for="Butiran">
                                                        Nama Paparan :</label></td>
                                                <td style="height: 19px">
                                                    <asp:TextBox ID="txtNPSubMenu" runat="server" class="form-control" Width="60%"></asp:TextBox>
                                                    &nbsp;<label id="lblMsgNPSubMenu" class="control-label" for="" style="display: none; color: #820303;">
                                                        *Masukkan Nama Paparan
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr style="height: 25px">
                                                <td style="width: 116px">Urutan :</td>
                                                <td>
                                                    <asp:TextBox ID="txtUrutanSubMenu" runat="server" class="form-control" Width="10%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr style="height: 25px">
                                                <td style="width: 116px">
                                                    <label class="control-label" for="Butiran">
                                                        Status Sub Menu :</label></td>
                                                <td>
                                                    <asp:RadioButtonList ID="rbStatusSubMenu" runat="server" Height="25px" RepeatDirection="Horizontal" Width="273px">
                                                        <asp:ListItem Selected="True" Text=" Aktif" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text=" Tidak Aktif" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr style="height: 55px; vertical-align: bottom">
                                                <td style="width: 116px">&nbsp; </td>
                                                <td>
                                                    <asp:LinkButton ID="lbtnSimpanSubMenu" runat="server" CssClass="btn btn-info" OnClientClick="return fConfirmSubMenu()">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                                                    </asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp; &nbsp;
                              <asp:LinkButton ID="lbtnHapusSubMenu" runat="server" CssClass="btn btn-info" OnClientClick="return fConfirmDelSubMenu()">
						<i class="fas fa-trash-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Hapus
                              </asp:LinkButton>
                                                    &nbsp;&nbsp;<asp:LinkButton ID="lbtnBaruSubMenu" runat="server" CssClass="btn btn-info" OnClientClick="return fBaruSubMenu()">
						<i class="fas fa-file-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Rekod Baru
                                                    </asp:LinkButton>
                                                </td>
                                            </tr>

                                        </table>
                                    </div>
                                </div>


                                <div class="row">
                                    <div class="GvTopPanel" style="height: 33px;">
                                        <div style="float: left; margin-top: 5px; margin-left: 10px;">
                                            <label class="control-label" for="">Jumlah rekod :</label>&nbsp;<label class="control-label" id="lblJumSubMenu" runat="server" style="color: mediumblue;"></label>
                                            &nbsp;&nbsp;   <b style="color: #969696;">|</b> &nbsp;&nbsp;
                <label class="control-label" for="Klasifikasi">Saiz Rekod : </label>
                                            <asp:DropDownList ID="ddlSaizRekodSubMenu" runat="server" Width="50px" AutoPostBack="True" class="form-control">
                                                <asp:ListItem Text="10" Value="10" />
                                                <asp:ListItem Text="25" Value="25" Selected="True" />
                                                <asp:ListItem Text="50" Value="50" />
                                                <asp:ListItem Text="100" Value="100" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <asp:GridView ID="gvSubMenu" runat="server" AllowPaging="True" 
                                        AllowSorting="True" AutoGenerateColumns="False" EmptyDataText=" "
                                        BorderColor="#333333" BorderStyle="Solid" 
                                        CssClass="table table-striped table-bordered table-hover"
                                        Font-Size="8pt" OnRowDataBound="OnRowDataBoundGvSubMenu" 
                                        PageSize="25" ShowHeaderWhenEmpty="True" Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Bil">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBil" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle Width="5%" HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblKodSubMenu" runat="server" Text="Kod Submenu" />&nbsp;
                                    <asp:LinkButton ID="lnkKodSubMenu" runat="server" CommandName="Sort" CommandArgument="KodSubMenu"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblKodSubMenu" runat="server" Text='<%# Eval("KodSubMenu")%>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="NamaSubMenu" HeaderText="Nama Submenu" SortExpression="NamaSubMenu">
                                                <ControlStyle Width="95%" />
                                                <ItemStyle Width="25%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DisSubMenu" HeaderText="Nama Paparan">
                                                <ItemStyle Width="25%" />
                                            </asp:BoundField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblUrutan" runat="server" Text="Urutan" />&nbsp;
                                    <asp:LinkButton ID="lnkUrutan" runat="server" CommandName="Sort" CommandArgument="Urutan"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUrutan" runat="server" Text='<%# Eval("Urutan")%>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblStatus" runat="server" Text="Status" />&nbsp;
                                    <asp:LinkButton ID="lnkStatus" runat="server" CommandName="Sort" CommandArgument="Status"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" runat="server" Text='<%#IIf(CBool(Eval("Status")), "Aktif", "Tidak Aktif") %>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>


                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="btn-xs" ToolTip="Kemaskini">
                          <i class="fas fa-edit"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="5%" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle BackColor="#6699FF" />
                                        <RowStyle Height="5px" />
                                        <SelectedRowStyle ForeColor="Blue" />
                                    </asp:GridView>
                                </div>

                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
