<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Kod_Kawalan_Pinjaman.aspx.vb" Inherits="SMKB_Web_Portal.Kod_Kawalan_Pinjaman" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" type="text/css" href="../../../Content/Site.css" />
    <h1></h1>
    <script type="text/javascript">


        function copyText1() {
            src = document.getElementById('<%=txtKodKat.ClientID%>');
            src.value = src.value.toUpperCase();

        }

        function copyText2() {
              src = document.getElementById('<%=txtButiranKat.ClientID%>');
            src.value = src.value.toUpperCase();

        }

        function fConfirm() {
            try {
                var blnComplete = true;

                var txtKodKat = document.getElementById('<%=txtKodKat.ClientID%>')
                if (txtKodKat.readOnly == true) {
                    alert('Sila klik button \'Rekod baru\' untuk menambah rekod baru!')
                    return false;
                }


                //Kod Modul
                if (txtKodKat.value == "") {
                    blnComplete = false
                    document.getElementById("lblMsgKodKat").style.display = 'inline-block';
                }
                else {
                    document.getElementById("lblMsgKodKat").style.display = 'none';
                }

                //Nama Modul
                var txtButiranKat = document.getElementById('<%=txtButiranKat.ClientID%>')
                if (txtButiranKat.value == "") {
                    blnComplete = false
                    document.getElementById("lblMsgButiranKat").style.display = 'inline-block';
                }
                else {
                    document.getElementById("lblMsgButiranKat").style.display = 'none';
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

                var txtKodKat = document.getElementById('<%=txtKodKat.ClientID%>')
                if (txtKodKat.value == "") {
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

        function fConfirmJenPinj() {
            try {
                var blnComplete = true;

                var txtKodJenPinj = document.getElementById('<%=txtKodJenPinj.ClientID%>')
                if (txtKodJenPinj.value == "") {
                    alert('Sila klik button \'Rekod baru\' untuk menambah rekod baru!')
                    return false;
                }

                //Nama JenPinj
                var txtButirJenPinj = document.getElementById('<%=txtButirJenPinj.ClientID%>')
                if (txtButirJenPinj.value == "") {
                    blnComplete = false
                    document.getElementById("lblMsgNamaJenPinj").style.display = 'inline-block';
                }
                else {
                    document.getElementById("lblMsgNamaJenPinj").style.display = 'none';
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

        function fConfirmDelJenPinj() {
            try {

                var txtKodJenPinj = document.getElementById('<%=txtKodJenPinj.ClientID%>')
                if (txtKodJenPinj.value == "") {
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

        function fConfirmKdrTempoh() {
            try {
                var blnComplete = true;

                var txtKodKdrTempoh = document.getElementById('<%=txtKodKdrTempoh.ClientID%>')
                if (txtKodKdrTempoh.value == "") {
                    alert('Sila klik button \'Rekod baru\' untuk menambah rekod baru!')
                    return false;
                }


            }
            catch (err) {
                alert(err)
                return false;
            }
        }

        function fConfirmDelKdrTempoh() {
            try {

                var txtKodKdrTempoh = document.getElementById('<%=txtKodKdrTempoh.ClientID%>')
                if (txtKodKdrTempoh.value == "") {
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

        function fBaruJenPinj() {
            try {

                var ddlKodKat2 = document.getElementById("<%=txtKodKat.ClientID %>");
                var intSelKodKat = ddlKodKat2.selectedIndex

                if (intSelKodKat == 0 || intSelKodKat == -1) {
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

        function fBaruKdrTempoh() {
            try {
                var blnSuccess = true;
                var intSelKodJenPinj = ddlKodJenPinj.selectedIndex

                if (intSelKodJenPinj == 0 || intSelKodJenPinj == -1) {
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
                    Width="100%" CssClass="tabCtrl" ActiveTabIndex="3" AutoPostBack="true">
                    <ajaxToolkit:TabPanel ID="tabModul" runat="Server" HeaderText="Kategori Pinjaman">
                        <HeaderTemplate>
                            Kategori Pinjaman
                        </HeaderTemplate>
                        <ContentTemplate>
                           <div class="row">
                                <div class="panel panel-default" style="width: 80%">
                                    <div class="panel-body">
                                        <table class="nav-justified">
                                            <tr style="height: 25px">
                                                <td>
                                                    <label class="control-label" for="">Kod :</label></td>
                                                <td>
                                                    <asp:TextBox ID="txtKodKat" runat="server" class="form-control" Width="20%" MaxLength="2" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                                    <i aria-hidden="true" class="fas fa-info-circle fa-lg" data-placement="right" style="cursor: pointer; color: #327aef;" data-toggle="tooltip" title="Masukkan 2 digit numerik sahaja."></i>
                                                    &nbsp;<label id="lblMsgKodKat" class="control-label" for="" style="display: none; color: #820303;">
                                                        (Masukkan Kod Modul)
                                                    </label>

                                                </td>
                                            </tr>
                                            <tr style="height: 25px">
                                                <td style="width: 116px; height: 22px;">
                                                    Butiran<label class="control-label" for="Butiran">:               
                                                </td>
                                                <td style="height: 22px">
                                                    <asp:TextBox ID="txtButiranKat" runat="server" class="form-control" Width="60%" onkeyup="copyText3()"></asp:TextBox>
                                                    &nbsp;
                                                            <label id="lblMsgButiranKat" class="control-label" for="" style="display: none; color: #820303;">
                                                                *Masukkan Nama Modul
                                                            </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 116px; height: 25px;">&nbsp;</td>
                                                <td style="height: 25px">
                                                    <asp:LinkButton ID="lbtnSimpanKatPinj" runat="server" CssClass="btn btn-info" OnClientClick="return fConfirm()">
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
                                    <asp:GridView ID="gvKatPinj" runat="server" AllowPaging="True"
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
                                                    <asp:Label ID="lblKodKat" runat="server" Text="Kod Kategori" />&nbsp;
                        <asp:LinkButton ID="lnkKodKat" runat="server" CommandName="Sort" CommandArgument="KodKat"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblKodKat" runat="server" Text='<%# Eval("Kod")%>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="Butiran" HeaderText="Butiran Kategori" SortExpression="ButiranKat">
                                                <ControlStyle Width="95%" />
                                                <ItemStyle Width="20%" />
                                            </asp:BoundField>       
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

                    <ajaxToolkit:TabPanel ID="tabJenPinj" runat="Server" HeaderText="Jenis Pinjaman">
                        <HeaderTemplate>
                            Jenis Pinjaman
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div class="row">

                                <div class="panel panel-default" style="width: 80%">
                                    <div class="panel-body">
                                        <table class="nav-justified">
                                            <tr style="height: 25px">
                                                <td style="width: 116px; height: 22px;">
                                                    <label class="control-label" for="Butiran">
                                                    Kod: </td>
                                                <td style="height: 22px">
                                                    <asp:TextBox ID="txtKodJenPinj" runat="server" class="form-control" Width="100px" ReadOnly="True"></asp:TextBox>
                                                &nbsp;
                                            </tr>

                                            <tr style="height: 25px">
                                                <td style="width: 150px">
                                                    <label class="control-label" for="Butiran">
                                                        Butiran :</label></td>
                                                <td>
                                                    <asp:TextBox ID="txtButirJenPinj" runat="server" class="form-control" Width="60%" onkeyup="copyText2()"></asp:TextBox>
                                                    &nbsp;<label id="lblMsgNamaJenPinj" class="control-label" for="" style="display: none; color: #820303;">
                                                        *Masukkan Nama Sub Modul
                                                    </label>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 116px; height: 25px;">&nbsp;</td>
                                                <td style="height: 25px">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="width: 116px; height: 25px;">&nbsp;</td>
                                                <td style="height: 25px">
                                                    <asp:LinkButton ID="lbtnSimpanJenPinj" runat="server" CssClass="btn btn-info" OnClientClick="return fConfirmJenPinj()">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                                                    </asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp; &nbsp;
                          <asp:LinkButton ID="lbtnHapusJenPinj" runat="server" CssClass="btn btn-info" OnClientClick="return fConfirmDelJenPinj()">
						<i class="fas fa-trash-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Hapus
                          </asp:LinkButton>
                                                    &nbsp;&nbsp;<asp:LinkButton ID="lbtnBaruJenPinj" runat="server" CssClass="btn btn-info" OnClientClick="return fBaruJenPinj()">
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
                    <asp:DropDownList ID="ddlSaizRekodJenPinj" runat="server" AutoPostBack="True" class="form-control" Width="50px">
                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                        <asp:ListItem Selected="True" Text="25" Value="25"></asp:ListItem>
                        <asp:ListItem Text="50" Value="50"></asp:ListItem>
                        <asp:ListItem Text="100" Value="100"></asp:ListItem>
                    </asp:DropDownList>
                    </label>
                                        </div>
                                    </div>
                                    <asp:GridView ID="gvJenPinj" runat="server" AllowPaging="True" 
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

                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblJenPinj" runat="server" Text="Kod" />&nbsp;
                        <asp:LinkButton ID="lnkKodJenPinj" runat="server" CommandName="Sort" CommandArgument="KodJenPinj"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblKodJenPinj" runat="server" Text='<%# Eval("Kod")%>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Butiran" HeaderText="Butiran" SortExpression="Butiran">
                                                <ControlStyle Width="95%" />
                                                <ItemStyle Width="25%" />
                                            </asp:BoundField>

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

                    <ajaxToolkit:TabPanel ID="tabKdrTempoh" runat="Server" HeaderText="Kadar Tempoh">
                        <HeaderTemplate>
                            Kadar Tempoh
                        </HeaderTemplate>
                        <ContentTemplate>

                            <div class="row">
                                <div class="panel panel-default" style="width: 80%">
                                    <div class="panel-body">
                                        <table class="nav-justified">

                                            <tr style="height: 25px">
                                                <td style="width: 116px; height: 19px;">
                                                    <label class="control-label" for="Butiran">
                                                        Kod&nbsp; :</label></td>
                                                <td style="height: 19px">
                                                    <asp:TextBox ID="txtKodKdrTempoh" runat="server" class="form-control" ReadOnly="True" Width="100px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr style="height: 25px">
                                                <td style="width: 116px">
                                                    <label class="control-label" for="Butiran">
                                                        Butiran :</label></td>
                                                <td>
                                                    <asp:TextBox ID="txtButirKdrTempoh" runat="server" class="form-control" onkeyup="copyText1()" Width="60%"></asp:TextBox>
                                                    &nbsp;<label id="lblMsgNamaKdrTempoh" class="control-label" for="" style="display: none; color: #820303;">
                                                        *Masukkan Nama Sub Menu
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr style="height: 55px; vertical-align: bottom">
                                                <td style="width: 116px">&nbsp; </td>
                                                <td>
                                                    <asp:LinkButton ID="lbtnSimpanKdrTempoh" runat="server" CssClass="btn btn-info" OnClientClick="return fConfirmKdrTempoh()">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                                                    </asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp; &nbsp;
                              <asp:LinkButton ID="lbtnHapusKdrTempoh" runat="server" CssClass="btn btn-info" OnClientClick="return fConfirmDelKdrTempoh()">
						<i class="fas fa-trash-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Hapus
                              </asp:LinkButton>
                                                    &nbsp;&nbsp;<asp:LinkButton ID="lbtnBaruKdrTempoh" runat="server" CssClass="btn btn-info" OnClientClick="return fBaruKdrTempoh()">
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
                                            <label class="control-label" for="">Jumlah rekod :</label>&nbsp;<label class="control-label" id="lblJumKdrTempoh" runat="server" style="color: mediumblue;"></label>
                                            &nbsp;&nbsp;   <b style="color: #969696;">|</b> &nbsp;&nbsp;
                <label class="control-label" for="Klasifikasi">Saiz Rekod : </label>
                                            <asp:DropDownList ID="ddlSaizRekodKdrTempoh" runat="server" Width="50px" AutoPostBack="True" class="form-control">
                                                <asp:ListItem Text="10" Value="10" />
                                                <asp:ListItem Text="25" Value="25" Selected="True" />
                                                <asp:ListItem Text="50" Value="50" />
                                                <asp:ListItem Text="100" Value="100" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <asp:GridView ID="gvKdrTempoh" runat="server" AllowPaging="True" 
                                        AllowSorting="True" AutoGenerateColumns="False" EmptyDataText=" "
                                        BorderColor="#333333" BorderStyle="Solid" 
                                        CssClass="table table-striped table-bordered table-hover"
                                        Font-Size="8pt" OnRowDataBound="OnRowDataBoundgvKdrTempoh" 
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
                                                    <asp:Label ID="lblKodKdrTempoh" runat="server" Text="Kod" />&nbsp;
                                    <asp:LinkButton ID="lnkKodKdrTempoh" runat="server" CommandName="Sort" CommandArgument="KodKdrTempoh"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblKodKdrTempoh" runat="server" Text='<%# Eval("Kod")%>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Butiran" HeaderText="Butiran" SortExpression="ButirKdrTempoh">
                                                <ControlStyle Width="95%" />
                                                <ItemStyle Width="25%" />
                                            </asp:BoundField>
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
                    <ajaxToolkit:TabPanel ID="tabSkimPinj" runat="Server" HeaderText="Skim Pinjaman">
                        <HeaderTemplate>
                            Skim Pinjaman
                        </HeaderTemplate>
                        <ContentTemplate>
                           <div class="row">
                                <div class="panel panel-default" style="width: 80%">
                                    <div class="panel-body">
                                        <table class="nav-justified">
                                            <tr style="height: 25px">
                                                <td>
                                                    <label class="control-label" for="">Kod :</label></td>
                                                <td>
                                                    <asp:TextBox ID="txtKodSkimPinj" runat="server" class="form-control" Width="20%" MaxLength="2" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                                    <i aria-hidden="true" class="fas fa-info-circle fa-lg" data-placement="right" style="cursor: pointer; color: #327aef;" data-toggle="tooltip" title="Masukkan 2 digit numerik sahaja."></i>
                                                    &nbsp;<label id="lblMsgKodSkim" class="control-label" for="" style="display: none; color: #820303;">
                                                        (Masukkan Kod )
                                                    </label>

                                                </td>
                                            </tr>
                                            <tr style="height: 25px">
                                                <td style="width: 116px; height: 22px;">
                                                    Butiran<label class="control-label" for="Butiran">:               
                                                </td>
                                                <td style="height: 22px">
                                                    <asp:TextBox ID="txtButirSkimPinj" runat="server" class="form-control" Width="60%" onkeyup="copyText3()"></asp:TextBox>
                                                    &nbsp;
                                                            <label id="lblMsgButiranSkim" class="control-label" for="" style="display: none; color: #820303;">
                                                                *Masukkan Butiran
                                                            </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 116px; height: 25px;">&nbsp;</td>
                                                <td style="height: 25px">
                                                    <asp:LinkButton ID="lbtnSimpanSkimPinj" runat="server" CssClass="btn btn-info" OnClientClick="return fConfirm()">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                                                    </asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp; &nbsp;
                          <asp:LinkButton ID="lbtnHapusSkimPinj" runat="server" CssClass="btn btn-info" OnClientClick="return fConfirmDel()">
						<i class="fas fa-trash-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Hapus
                          </asp:LinkButton>
                                                    &nbsp;&nbsp;<asp:LinkButton ID="lbtnBaruSkimPinj" runat="server" CssClass="btn btn-info">
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
                                            <label class="control-label" for="">Jumlah rekod :</label>&nbsp;<label class="control-label" id="Label1" runat="server" style="color: mediumblue;"></label>

                                            &nbsp;&nbsp;   <b style="color: #969696;">|</b> &nbsp;&nbsp;

                    <label class="control-label" for="Klasifikasi">Saiz Rekod : </label>
                                            <asp:DropDownList ID="DropDownList1" runat="server" Width="50px" AutoPostBack="True" class="form-control">
                                                <asp:ListItem Text="10" Value="10" />
                                                <asp:ListItem Text="25" Value="25" Selected="True" />
                                                <asp:ListItem Text="50" Value="50" />
                                                <asp:ListItem Text="100" Value="100" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <asp:GridView ID="gvSkimPinj" runat="server" AllowPaging="True"
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
                                                    <asp:Label ID="lblKodSkim" runat="server" Text="Kod Skim" />&nbsp;
                        <asp:LinkButton ID="lnkKodSkim" runat="server" CommandName="Sort" CommandArgument="KodSkim"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblKodSkim" runat="server" Text='<%# Eval("Kod")%>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="Butiran" HeaderText="Butiran Skim" SortExpression="ButiranSkim">
                                                <ControlStyle Width="95%" />
                                                <ItemStyle Width="20%" />
                                            </asp:BoundField>    
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
