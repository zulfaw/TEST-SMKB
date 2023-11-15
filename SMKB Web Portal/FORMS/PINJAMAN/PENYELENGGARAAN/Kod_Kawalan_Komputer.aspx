<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Kod_Kawalan_Komputer.aspx.vb" Inherits="SMKB_Web_Portal.Kod_Kawalan_Komputer" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" type="text/css" href="../../../Content/Site.css" />
    <h1></h1>
    <script type="text/javascript">


        function copyText1() {
            src = document.getElementById('<%=txtKodJenama.ClientID%>');
            src.value = src.value.toUpperCase();

        }

        function copyText2() {
              src = document.getElementById('<%=txtButirJenama.ClientID%>');
            src.value = src.value.toUpperCase();

        }

        function fConfirm() {
            try {
                var blnComplete = true;

                var txtKodJenama = document.getElementById('<%=txtKodJenama.ClientID%>')
                if (txtKodJenama.readOnly == true) {
                    alert('Sila klik button \'Rekod baru\' untuk menambah rekod baru!')
                    return false;
                }


                //Kod Modul
                if (txtKodJenama.value == "") {
                    blnComplete = false
                    document.getElementById("lblMsgKodJenama").style.display = 'inline-block';
                }
                else {
                    document.getElementById("lblMsgKodJenama").style.display = 'none';
                }

                //Nama Modul
                var txtButirJenama = document.getElementById('<%=txtButirJenama.ClientID%>')
                if (txtButirJenama.value == "") {
                    blnComplete = false
                    document.getElementById("lblMsgButirJenama").style.display = 'inline-block';
                }
                else {
                    document.getElementById("lblMsgButirJenama").style.display = 'none';
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

                var txtKodJenama = document.getElementById('<%=txtKodJenama.ClientID%>')
                if (txtKodJenama.value == "") {
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

        function fConfirmIngatan() {
            try {
                var blnComplete = true;

                var txtKodIngatan = document.getElementById('<%=txtKodIngatan.ClientID%>')
                if (txtKodIngatan.value == "") {
                    alert('Sila klik button \'Rekod baru\' untuk menambah rekod baru!')
                    return false;
                }

                //Nama Ingatan
                var txtButirIngatan = document.getElementById('<%=txtButirIngatan.ClientID%>')
                if (txtButirIngatan.value == "") {
                    blnComplete = false
                    document.getElementById("lblMsgNamaIngatan").style.display = 'inline-block';
                }
                else {
                    document.getElementById("lblMsgNamaIngatan").style.display = 'none';
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

        function fConfirmDelIngatan() {
            try {

                var txtKodIngatan = document.getElementById('<%=txtKodIngatan.ClientID%>')
                if (txtKodIngatan.value == "") {
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

        function fConfirmCakeraKeras() {
            try {
                var blnComplete = true;

                var txtKodCakeraKeras = document.getElementById('<%=txtKodCakeraKeras.ClientID%>')
                if (txtKodCakeraKeras.value == "") {
                    alert('Sila klik button \'Rekod baru\' untuk menambah rekod baru!')
                    return false;
                }
                
            }
            catch (err) {
                alert(err)
                return false;
            }
        }

        function fConfirmDelCakeraKeras() {
            try {

                var txtKodCakeraKeras = document.getElementById('<%=txtKodCakeraKeras.ClientID%>')
                if (txtKodCakeraKeras.value == "") {
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

        function fBaruIngatan() {
            try {

                var ddlKodJenama2 = document.getElementById("<%=txtKodJenama.ClientID %>");
                var intSelKodJenama = ddlKodJenama2.selectedIndex

                if (intSelKodJenama == 0 || intSelKodJenama == -1) {
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

        function fBaruCakeraKeras() {
            try {
                var blnSuccess = true;
                var intSelKodIngatan = ddlKodIngatan.selectedIndex

                if (intSelKodIngatan == 0 || intSelKodIngatan == -1) {
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
                    <ajaxToolkit:TabPanel ID="tabModul" runat="Server" HeaderText="Jenama">
                        <HeaderTemplate>
                            Jenama
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
                                                    <asp:TextBox ID="txtKodJenama" runat="server" class="form-control" Width="20%" MaxLength="2" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                                    <i aria-hidden="true" class="fas fa-info-circle fa-lg" data-placement="right" style="cursor: pointer; color: #327aef;" data-toggle="tooltip" title="Masukkan 2 digit numerik sahaja."></i>
                                                    &nbsp;<label id="lblMsgKodJenama" class="control-label" for="" style="display: none; color: #820303;">
                                                        (Masukkan Kod Modul)
                                                    </label>

                                                </td>
                                            </tr>
                                            <tr style="height: 25px">
                                                <td style="width: 116px; height: 22px;">
                                                    Butiran<label class="control-label" for="Butiran">:               
                                                </td>
                                                <td style="height: 22px">
                                                    <asp:TextBox ID="txtButirJenama" runat="server" class="form-control" Width="60%" onkeyup="copyText3()"></asp:TextBox>
                                                    &nbsp;
                                                            <label id="lblMsgButirJenama" class="control-label" for="" style="display: none; color: #820303;">
                                                                *Masukkan Nama Modul
                                                            </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 116px; height: 25px;">&nbsp;</td>
                                                <td style="height: 25px">
                                                    <asp:LinkButton ID="lbtnSimpanJenama" runat="server" CssClass="btn btn-info" OnClientClick="return fConfirm()">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                                                    </asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp; &nbsp;
                          <asp:LinkButton ID="lbtnHapusJenama" runat="server" CssClass="btn btn-info" OnClientClick="return fConfirmDel()">
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
                                    <asp:GridView ID="gvJenama" runat="server" AllowPaging="True"
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
                                                    <asp:Label ID="lblKodJenama" runat="server" Text="Kod Kategori" />&nbsp;
                        <asp:LinkButton ID="lnkKodJenama" runat="server" CommandName="Sort" CommandArgument="KodJenama"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblKodJenama" runat="server" Text='<%# Eval("Kod")%>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="Butiran" HeaderText="Butiran Kategori" SortExpression="ButirJenama">
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

                    <ajaxToolkit:TabPanel ID="tabIngatan" runat="Server" HeaderText="Ingatan">
                        <HeaderTemplate>
                            Ingatan
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
                                                    <asp:TextBox ID="txtKodIngatan" runat="server" class="form-control" Width="100px" ReadOnly="True"></asp:TextBox>
                                                &nbsp;
                                            </tr>

                                            <tr style="height: 25px">
                                                <td style="width: 150px">
                                                    <label class="control-label" for="Butiran">
                                                        Butiran :</label></td>
                                                <td>
                                                    <asp:TextBox ID="txtButirIngatan" runat="server" class="form-control" Width="60%" onkeyup="copyText2()"></asp:TextBox>
                                                    &nbsp;<label id="lblMsgNamaIngatan" class="control-label" for="" style="display: none; color: #820303;">
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
                                                    <asp:LinkButton ID="lbtnSimpanIngatan" runat="server" CssClass="btn btn-info" OnClientClick="return fConfirmIngatan()">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                                                    </asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp; &nbsp;
                          <asp:LinkButton ID="lbtnHapusIngatan" runat="server" CssClass="btn btn-info" OnClientClick="return fConfirmDelIngatan()">
						<i class="fas fa-trash-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Hapus
                          </asp:LinkButton>
                                                    &nbsp;&nbsp;<asp:LinkButton ID="lbtnBaruIngatan" runat="server" CssClass="btn btn-info" OnClientClick="return fBaruIngatan()">
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
                    <asp:DropDownList ID="ddlSaizRekodIngatan" runat="server" AutoPostBack="True" class="form-control" Width="50px">
                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                        <asp:ListItem Selected="True" Text="25" Value="25"></asp:ListItem>
                        <asp:ListItem Text="50" Value="50"></asp:ListItem>
                        <asp:ListItem Text="100" Value="100"></asp:ListItem>
                    </asp:DropDownList>
                    </label>
                                        </div>
                                    </div>
                                    <asp:GridView ID="gvIngatan" runat="server" AllowPaging="True" 
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
                                                    <asp:Label ID="lblIngatan" runat="server" Text="Kod" />&nbsp;
                        <asp:LinkButton ID="lnkKodIngatan" runat="server" CommandName="Sort" CommandArgument="KodIngatan"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblKodIngatan" runat="server" Text='<%# Eval("Kod")%>' />
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

                    <ajaxToolkit:TabPanel ID="tabCakeraKeras" runat="Server" HeaderText="Cakera Keras">
                        <HeaderTemplate>
                            Cakera Keras
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
                                                    <asp:TextBox ID="txtKodCakeraKeras" runat="server" class="form-control" ReadOnly="True" Width="100px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr style="height: 25px">
                                                <td style="width: 116px">
                                                    <label class="control-label" for="Butiran">
                                                        Butiran :</label></td>
                                                <td>
                                                    <asp:TextBox ID="txtButirCakeraKeras" runat="server" class="form-control" onkeyup="copyText1()" Width="60%"></asp:TextBox>
                                                    &nbsp;<label id="lblMsgNamaCakeraKeras" class="control-label" for="" style="display: none; color: #820303;">
                                                        *Masukkan Cakera Keras
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr style="height: 55px; vertical-align: bottom">
                                                <td style="width: 116px">&nbsp; </td>
                                                <td>
                                                    <asp:LinkButton ID="lbtnSimpanCakeraKeras" runat="server" CssClass="btn btn-info" OnClientClick="return fConfirmCakeraKeras()">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                                                    </asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp; &nbsp;
                              <asp:LinkButton ID="lbtnHapusCakeraKeras" runat="server" CssClass="btn btn-info" OnClientClick="return fConfirmDelCakeraKeras()">
						<i class="fas fa-trash-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Hapus
                              </asp:LinkButton>
                                                    &nbsp;&nbsp;<asp:LinkButton ID="lbtnBaruCakeraKeras" runat="server" CssClass="btn btn-info" OnClientClick="return fBaruCakeraKeras()">
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
                                            <label class="control-label" for="">Jumlah rekod :</label>&nbsp;<label class="control-label" id="lblJumCakeraKeras" runat="server" style="color: mediumblue;"></label>
                                            &nbsp;&nbsp;   <b style="color: #969696;">|</b> &nbsp;&nbsp;
                <label class="control-label" for="Klasifikasi">Saiz Rekod : </label>
                                            <asp:DropDownList ID="ddlSaizRekodCakeraKeras" runat="server" Width="50px" AutoPostBack="True" class="form-control">
                                                <asp:ListItem Text="10" Value="10" />
                                                <asp:ListItem Text="25" Value="25" Selected="True" />
                                                <asp:ListItem Text="50" Value="50" />
                                                <asp:ListItem Text="100" Value="100" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <asp:GridView ID="gvCakeraKeras" runat="server" AllowPaging="True" 
                                        AllowSorting="True" AutoGenerateColumns="False" EmptyDataText=" "
                                        BorderColor="#333333" BorderStyle="Solid" 
                                        CssClass="table table-striped table-bordered table-hover"
                                        Font-Size="8pt" OnRowDataBound="OnRowDataBoundgvCakeraKeras" 
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
                                                    <asp:Label ID="lblKodCakeraKeras" runat="server" Text="Kod" />&nbsp;
                                    <asp:LinkButton ID="lnkKodCakeraKeras" runat="server" CommandName="Sort" CommandArgument="KodCakeraKeras"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblKodCakeraKeras" runat="server" Text='<%# Eval("Kod")%>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Butiran" HeaderText="Butiran" SortExpression="ButirCakeraKeras">
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
                    <ajaxToolkit:TabPanel ID="tabKekunci" runat="Server" HeaderText="Papan Kekunci">
                        <HeaderTemplate>
                            Papan Kekunci
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
                                                    <asp:TextBox ID="txtKodKekunci" runat="server" class="form-control" Width="20%" MaxLength="2" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                                    <i aria-hidden="true" class="fas fa-info-circle fa-lg" data-placement="right" style="cursor: pointer; color: #327aef;" data-toggle="tooltip" title="Masukkan 2 digit numerik sahaja."></i>
                                                    &nbsp;<label id="lblMsgKodKekunci" class="control-label" for="" style="display: none; color: #820303;">
                                                        (Masukkan Kod )
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr style="height: 25px">
                                                <td style="width: 116px; height: 22px;">
                                                    Butiran<label class="control-label" for="Butiran">:               
                                                </td>
                                                <td style="height: 22px">
                                                    <asp:TextBox ID="txtButirKekunci" runat="server" class="form-control" Width="60%" onkeyup="copyText3()"></asp:TextBox>
                                                    &nbsp;
                                                            <label id="lblMsgButiranKekunci" class="control-label" for="" style="display: none; color: #820303;">
                                                                *Masukkan Butiran
                                                            </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 116px; height: 25px;">&nbsp;</td>
                                                <td style="height: 25px">
                                                    <asp:LinkButton ID="lbtnSimpanKekunci" runat="server" CssClass="btn btn-info" OnClientClick="return fConfirm()">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                                                    </asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp; &nbsp;
                          <asp:LinkButton ID="lbtnHapusKekunci" runat="server" CssClass="btn btn-info" OnClientClick="return fConfirmDel()">
						<i class="fas fa-trash-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Hapus
                          </asp:LinkButton>
                                                    &nbsp;&nbsp;<asp:LinkButton ID="lbtnBaruKekunci" runat="server" CssClass="btn btn-info">
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
                                    <asp:GridView ID="gvKekunci" runat="server" AllowPaging="True"
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
                                                    <asp:Label ID="lblKodKekunci" runat="server" Text="Kod Kekunci" />&nbsp;
                        <asp:LinkButton ID="lnkKodKekunci" runat="server" CommandName="Sort" CommandArgument="KodKekunci"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblKodKekunci" runat="server" Text='<%# Eval("Kod")%>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="Butiran" HeaderText="Butiran Kekunci" SortExpression="ButiranKekunci">
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


  <ajaxToolkit:TabPanel ID="TabPencetak" runat="Server" HeaderText="Pencetak">
                        <HeaderTemplate>
                            Pencetak
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
                                                    <asp:TextBox ID="txtKodPencetak" runat="server" class="form-control" Width="20%" MaxLength="2" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                                    <i aria-hidden="true" class="fas fa-info-circle fa-lg" data-placement="right" style="cursor: pointer; color: #327aef;" data-toggle="tooltip" title="Masukkan 2 digit numerik sahaja."></i>
                                                    &nbsp;<label id="lblMsgKodPencetak" class="control-label" for="" style="display: none; color: #820303;">
                                                        (Masukkan Kod )
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr style="height: 25px">
                                                <td style="width: 116px; height: 22px;">
                                                    Butiran<label class="control-label" for="Butiran">:               
                                                </td>
                                                <td style="height: 22px">
                                                    <asp:TextBox ID="txtButirPencetak" runat="server" class="form-control" Width="60%" onkeyup="copyText3()"></asp:TextBox>
                                                    &nbsp;
                                                            <label id="lblMsgButiranPencetak" class="control-label" for="" style="display: none; color: #820303;">
                                                                *Masukkan Butiran
                                                            </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 116px; height: 25px;">&nbsp;</td>
                                                <td style="height: 25px">
                                                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-info" OnClientClick="return fConfirm()">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                                                    </asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp; &nbsp;
                          <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-info" OnClientClick="return fConfirmDel()">
						<i class="fas fa-trash-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Hapus
                          </asp:LinkButton>
                                                    &nbsp;&nbsp;<asp:LinkButton ID="LinkButton3" runat="server" CssClass="btn btn-info">
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
                                            <label class="control-label" for="">Jumlah rekod :</label>&nbsp;<label class="control-label" id="Label2" runat="server" style="color: mediumblue;"></label>

                                            &nbsp;&nbsp;   <b style="color: #969696;">|</b> &nbsp;&nbsp;

                    <label class="control-label" for="Klasifikasi">Saiz Rekod : </label>
                                            <asp:DropDownList ID="DropDownList2" runat="server" Width="50px" AutoPostBack="True" class="form-control">
                                                <asp:ListItem Text="10" Value="10" />
                                                <asp:ListItem Text="25" Value="25" Selected="True" />
                                                <asp:ListItem Text="50" Value="50" />
                                                <asp:ListItem Text="100" Value="100" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <asp:GridView ID="gvPencetak" runat="server" AllowPaging="True"
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
                                                    <asp:Label ID="lblKodPencetak" runat="server" Text="Kod Pencetak" />&nbsp;
                        <asp:LinkButton ID="lnkKodPencetak" runat="server" CommandName="Sort" CommandArgument="KodPencetak"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblKodPencetak" runat="server" Text='<%# Eval("Kod")%>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="Butiran" HeaderText="Butiran Pencetak" SortExpression="ButiranPencetak">
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
