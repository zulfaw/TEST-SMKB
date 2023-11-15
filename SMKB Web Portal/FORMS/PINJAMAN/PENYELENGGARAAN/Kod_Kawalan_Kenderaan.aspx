<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Kod_Kawalan_Kenderaan.aspx.vb" Inherits="SMKB_Web_Portal.Kod_Kawalan_Kenderaan" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" type="text/css" href="../../../Content/Site.css" />
    <h1></h1>
    <script type="text/javascript">


        function copyText1() {
            src = document.getElementById('<%=txtKodJenamaKereta.ClientID%>');
            src.value = src.value.toUpperCase();

        }

        function copyText2() {
              src = document.getElementById('<%=txtButirJenamaKereta.ClientID%>');
            src.value = src.value.toUpperCase();

        }

        function fConfirm() {
            try {
                var blnComplete = true;

                var txtKodJenamaKereta = document.getElementById('<%=txtKodJenamaKereta.ClientID%>')
                if (txtKodJenamaKereta.readOnly == true) {
                    alert('Sila klik button \'Rekod baru\' untuk menambah rekod baru!')
                    return false;
                }


                //Kod Modul
                if (txtKodJenamaKereta.value == "") {
                    blnComplete = false
                    document.getElementById("lblMsgKodJenamaKereta").style.display = 'inline-block';
                }
                else {
                    document.getElementById("lblMsgKodJenamaKereta").style.display = 'none';
                }

                //Nama Modul
                var txtButirJenamaKereta = document.getElementById('<%=txtButirJenamaKereta.ClientID%>')
                if (txtButirJenamaKereta.value == "") {
                    blnComplete = false
                    document.getElementById("lblMsgButirJenamaKereta").style.display = 'inline-block';
                }
                else {
                    document.getElementById("lblMsgButirJenamaKereta").style.display = 'none';
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

                var txtKodJenamaKereta = document.getElementById('<%=txtKodJenamaKereta.ClientID%>')
                if (txtKodJenamaKereta.value == "") {
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

        function fConfirmModel() {
            try {
                var blnComplete = true;

                var txtKodModel = document.getElementById('<%=txtKodModel.ClientID%>')
                if (txtKodModel.value == "") {
                    alert('Sila klik button \'Rekod baru\' untuk menambah rekod baru!')
                    return false;
                }

                //Nama Model
                var txtButirModel = document.getElementById('<%=txtButirModel.ClientID%>')
                if (txtButirModel.value == "") {
                    blnComplete = false
                    document.getElementById("lblMsgNamaModel").style.display = 'inline-block';
                }
                else {
                    document.getElementById("lblMsgNamaModel").style.display = 'none';
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

        function fConfirmDelModel() {
            try {

                var txtKodModel = document.getElementById('<%=txtKodModel.ClientID%>')
                if (txtKodModel.value == "") {
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

        function fConfirmBhnBakar() {
            try {
                var blnComplete = true;

                var txtKodBhnBakar = document.getElementById('<%=txtKodBhnBakar.ClientID%>')
                if (txtKodBhnBakar.value == "") {
                    alert('Sila klik button \'Rekod baru\' untuk menambah rekod baru!')
                    return false;
                }
                
            }
            catch (err) {
                alert(err)
                return false;
            }
        }

        function fConfirmDelBhnBakar() {
            try {

                var txtKodBhnBakar = document.getElementById('<%=txtKodBhnBakar.ClientID%>')
                if (txtKodBhnBakar.value == "") {
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

        function fBaruModel() {
            try {

                var ddlKodJenamaKereta2 = document.getElementById("<%=txtKodJenamaKereta.ClientID %>");
                var intSelKodJenamaKereta = ddlKodJenamaKereta2.selectedIndex

                if (intSelKodJenamaKereta == 0 || intSelKodJenamaKereta == -1) {
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

        function fBaruBhnBakar() {
            try {
                var blnSuccess = true;
                var intSelKodModel = ddlKodModel.selectedIndex

                if (intSelKodModel == 0 || intSelKodModel == -1) {
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
                    <ajaxToolkit:TabPanel ID="tabModul" runat="Server" HeaderText="Jenama Kereta">
                        <HeaderTemplate>
                            Jenama Kereta
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
                                                    <asp:TextBox ID="txtKodJenamaKereta" runat="server" class="form-control" Width="20%" MaxLength="2" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                                    <i aria-hidden="true" class="fas fa-info-circle fa-lg" data-placement="right" style="cursor: pointer; color: #327aef;" data-toggle="tooltip" title="Masukkan 2 digit numerik sahaja."></i>
                                                    &nbsp;<label id="lblMsgKodJenamaKereta" class="control-label" for="" style="display: none; color: #820303;">
                                                        (Masukkan Kod Modul)
                                                    </label>

                                                </td>
                                            </tr>
                                            <tr style="height: 25px">
                                                <td style="width: 116px; height: 22px;">
                                                    Butiran<label class="control-label" for="Butiran">:               
                                                </td>
                                                <td style="height: 22px">
                                                    <asp:TextBox ID="txtButirJenamaKereta" runat="server" class="form-control" Width="60%" onkeyup="copyText3()"></asp:TextBox>
                                                    &nbsp;
                                                            <label id="lblMsgButirJenamaKereta" class="control-label" for="" style="display: none; color: #820303;">
                                                                *Masukkan Nama Modul
                                                            </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 116px; height: 25px;">&nbsp;</td>
                                                <td style="height: 25px">
                                                    <asp:LinkButton ID="lbtnSimpanJenamaKereta" runat="server" CssClass="btn btn-info" OnClientClick="return fConfirm()">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                                                    </asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp; &nbsp;
                          <asp:LinkButton ID="lbtnHapusJenamaKereta" runat="server" CssClass="btn btn-info" OnClientClick="return fConfirmDel()">
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
                                    <asp:GridView ID="gvJenamaKereta" runat="server" AllowPaging="True"
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
                                                    <asp:Label ID="lblKodJenamaKereta" runat="server" Text="Kod Kategori" />&nbsp;
                        <asp:LinkButton ID="lnkKodJenamaKereta" runat="server" CommandName="Sort" CommandArgument="KodJenamaKereta"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblKodJenamaKereta" runat="server" Text='<%# Eval("KodJenama")%>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="Butiran" HeaderText="Butiran Kategori" SortExpression="ButirJenamaKereta">
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

                    <ajaxToolkit:TabPanel ID="tabModel" runat="Server" HeaderText="Model">
                        <HeaderTemplate>
                            Model
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
                                                    <asp:TextBox ID="txtKodModel" runat="server" class="form-control" Width="100px" ReadOnly="True"></asp:TextBox>
                                                &nbsp;
                                            </tr>

                                            <tr style="height: 25px">
                                                <td style="width: 150px">
                                                    <label class="control-label" for="Butiran">
                                                        Butiran :</label></td>
                                                <td>
                                                    <asp:TextBox ID="txtButirModel" runat="server" class="form-control" Width="60%" onkeyup="copyText2()"></asp:TextBox>
                                                    &nbsp;<label id="lblMsgNamaModel" class="control-label" for="" style="display: none; color: #820303;">
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
                                                    <asp:LinkButton ID="lbtnSimpanModel" runat="server" CssClass="btn btn-info" OnClientClick="return fConfirmModel()">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                                                    </asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp; &nbsp;
                          <asp:LinkButton ID="lbtnHapusModel" runat="server" CssClass="btn btn-info" OnClientClick="return fConfirmDelModel()">
						<i class="fas fa-trash-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Hapus
                          </asp:LinkButton>
                                                    &nbsp;&nbsp;<asp:LinkButton ID="lbtnBaruModel" runat="server" CssClass="btn btn-info" OnClientClick="return fBaruModel()">
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
                    <asp:DropDownList ID="ddlSaizRekodModel" runat="server" AutoPostBack="True" class="form-control" Width="50px">
                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                        <asp:ListItem Selected="True" Text="25" Value="25"></asp:ListItem>
                        <asp:ListItem Text="50" Value="50"></asp:ListItem>
                        <asp:ListItem Text="100" Value="100"></asp:ListItem>
                    </asp:DropDownList>
                    </label>
                                        </div>
                                    </div>
                                    <asp:GridView ID="gvModel" runat="server" AllowPaging="True" 
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
                                                    <asp:Label ID="lblModel" runat="server" Text="Kod" />&nbsp;
                        <asp:LinkButton ID="lnkKodModel" runat="server" CommandName="Sort" CommandArgument="KodModel"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblKodModel" runat="server" Text='<%# Eval("KodModel")%>' />
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

                    <ajaxToolkit:TabPanel ID="tabBhnBakar" runat="Server" HeaderText="Bahan Bakar">
                        <HeaderTemplate>
                            Bahan Bakar
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
                                                    <asp:TextBox ID="txtKodBhnBakar" runat="server" class="form-control" ReadOnly="True" Width="100px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr style="height: 25px">
                                                <td style="width: 116px">
                                                    <label class="control-label" for="Butiran">
                                                        Butiran :</label></td>
                                                <td>
                                                    <asp:TextBox ID="txtButirBhnBakar" runat="server" class="form-control" onkeyup="copyText1()" Width="60%"></asp:TextBox>
                                                    &nbsp;<label id="lblMsgNamaBhnBakar" class="control-label" for="" style="display: none; color: #820303;">
                                                        *Masukkan Cakera Keras
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr style="height: 55px; vertical-align: bottom">
                                                <td style="width: 116px">&nbsp; </td>
                                                <td>
                                                    <asp:LinkButton ID="lbtnSimpanBhnBakar" runat="server" CssClass="btn btn-info" OnClientClick="return fConfirmBhnBakar()">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                                                    </asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp; &nbsp;
                              <asp:LinkButton ID="lbtnHapusBhnBakar" runat="server" CssClass="btn btn-info" OnClientClick="return fConfirmDelBhnBakar()">
						<i class="fas fa-trash-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Hapus
                              </asp:LinkButton>
                                                    &nbsp;&nbsp;<asp:LinkButton ID="lbtnBaruBhnBakar" runat="server" CssClass="btn btn-info" OnClientClick="return fBaruBhnBakar()">
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
                                            <label class="control-label" for="">Jumlah rekod :</label>&nbsp;<label class="control-label" id="lblJumBhnBakar" runat="server" style="color: mediumblue;"></label>
                                            &nbsp;&nbsp;   <b style="color: #969696;">|</b> &nbsp;&nbsp;
                <label class="control-label" for="Klasifikasi">Saiz Rekod : </label>
                                            <asp:DropDownList ID="ddlSaizRekodBhnBakar" runat="server" Width="50px" AutoPostBack="True" class="form-control">
                                                <asp:ListItem Text="10" Value="10" />
                                                <asp:ListItem Text="25" Value="25" Selected="True" />
                                                <asp:ListItem Text="50" Value="50" />
                                                <asp:ListItem Text="100" Value="100" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <asp:GridView ID="gvBhnBakar" runat="server" AllowPaging="True" 
                                        AllowSorting="True" AutoGenerateColumns="False" EmptyDataText=" "
                                        BorderColor="#333333" BorderStyle="Solid" 
                                        CssClass="table table-striped table-bordered table-hover"
                                        Font-Size="8pt" OnRowDataBound="OnRowDataBoundgvBhnBakar" 
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
                                                    <asp:Label ID="lblKodBhnBakar" runat="server" Text="Kod Bahan Bakar" />&nbsp;
                                    <asp:LinkButton ID="lnkKodBhnBakar" runat="server" CommandName="Sort" CommandArgument="KodBhnBakar"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblKodBhnBakar" runat="server" Text='<%# Eval("Kod")%>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Butiran" HeaderText="Butiran Bahan Bakar" SortExpression="ButirBhnBakar">
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
                    <ajaxToolkit:TabPanel ID="tabBuatan" runat="Server" HeaderText="Buatan">
                        <HeaderTemplate>
                            Buatan
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
                                                    <asp:TextBox ID="txtKodBuatan" runat="server" class="form-control" Width="20%" MaxLength="2" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                                    <i aria-hidden="true" class="fas fa-info-circle fa-lg" data-placement="right" style="cursor: pointer; color: #327aef;" data-toggle="tooltip" title="Masukkan 2 digit numerik sahaja."></i>
                                                    &nbsp;<label id="lblMsgKodBuatan" class="control-label" for="" style="display: none; color: #820303;">
                                                        (Masukkan Kod )
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr style="height: 25px">
                                                <td style="width: 116px; height: 22px;">
                                                    Butiran<label class="control-label" for="Butiran">:               
                                                </td>
                                                <td style="height: 22px">
                                                    <asp:TextBox ID="txtButirBuatan" runat="server" class="form-control" Width="60%" onkeyup="copyText3()"></asp:TextBox>
                                                    &nbsp;
                                                            <label id="lblMsgButiranBuatan" class="control-label" for="" style="display: none; color: #820303;">
                                                                *Masukkan Butiran
                                                            </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 116px; height: 25px;">&nbsp;</td>
                                                <td style="height: 25px">
                                                    <asp:LinkButton ID="lbtnSimpanBuatan" runat="server" CssClass="btn btn-info" OnClientClick="return fConfirm()">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                                                    </asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp; &nbsp;
                          <asp:LinkButton ID="lbtnHapusBuatan" runat="server" CssClass="btn btn-info" OnClientClick="return fConfirmDel()">
						<i class="fas fa-trash-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Hapus
                          </asp:LinkButton>
                                                    &nbsp;&nbsp;<asp:LinkButton ID="lbtnBaruBuatan" runat="server" CssClass="btn btn-info">
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
                                    <asp:GridView ID="gvBuatan" runat="server" AllowPaging="True"
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
                                                    <asp:Label ID="lblKodBuatan" runat="server" Text="Kod Buatan" />&nbsp;
                        <asp:LinkButton ID="lnkKodBuatan" runat="server" CommandName="Sort" CommandArgument="KodBuatan"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblKodBuatan" runat="server" Text='<%# Eval("Kod")%>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="Butiran" HeaderText="Butiran Buatan" SortExpression="ButiranBuatan">
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


  <ajaxToolkit:TabPanel ID="TabSukatSilinder" runat="Server" HeaderText="SukatSilinder">
                        <HeaderTemplate>
                            Sukat Silinder
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
                                                    <asp:TextBox ID="txtKodSukatSilinder" runat="server" class="form-control" Width="20%" MaxLength="2" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                                    <i aria-hidden="true" class="fas fa-info-circle fa-lg" data-placement="right" style="cursor: pointer; color: #327aef;" data-toggle="tooltip" title="Masukkan 2 digit numerik sahaja."></i>
                                                    &nbsp;<label id="lblMsgKodSukatSilinder" class="control-label" for="" style="display: none; color: #820303;">
                                                        (Masukkan Kod )
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr style="height: 25px">
                                                <td style="width: 116px; height: 22px;">
                                                    Butiran<label class="control-label" for="Butiran">:               
                                                </td>
                                                <td style="height: 22px">
                                                    <asp:TextBox ID="txtButirSukatSilinder" runat="server" class="form-control" Width="60%" onkeyup="copyText3()"></asp:TextBox>
                                                    &nbsp;
                                                            <label id="lblMsgButiranSukatSilinder" class="control-label" for="" style="display: none; color: #820303;">
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
                                    <asp:GridView ID="gvSukatSilinder" runat="server" AllowPaging="True"
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
                                                    <asp:Label ID="lblKodSukatSilinder" runat="server" Text="Kod Sukat Silinder" />&nbsp;
                        <asp:LinkButton ID="lnkKodSukatSilinder" runat="server" CommandName="Sort" CommandArgument="KodSukatSilinder"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblKodSukatSilinder" runat="server" Text='<%# Eval("Kod")%>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="Butiran" HeaderText="Butiran Sukat Silinder" SortExpression="ButiranSukatSilinder">
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

<ajaxToolkit:TabPanel ID="TabKuasaEnjin" runat="Server" HeaderText="KuasaEnjin">
                        <HeaderTemplate>
                            Kuasa Enjin
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
                                                    <asp:TextBox ID="txtKodKuasaEnjin" runat="server" class="form-control" Width="20%" MaxLength="2" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                                    <i aria-hidden="true" class="fas fa-info-circle fa-lg" data-placement="right" style="cursor: pointer; color: #327aef;" data-toggle="tooltip" title="Masukkan 2 digit numerik sahaja."></i>
                                                    &nbsp;<label id="lblMsgKodKuasaEnjin" class="control-label" for="" style="display: none; color: #820303;">
                                                        (Masukkan Kod )
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr style="height: 25px">
                                                <td style="width: 116px; height: 22px;">
                                                    Butiran<label class="control-label" for="Butiran">:               
                                                </td>
                                                <td style="height: 22px">
                                                    <asp:TextBox ID="txtButirKuasaEnjin" runat="server" class="form-control" Width="60%" onkeyup="copyText3()"></asp:TextBox>
                                                    &nbsp;
                                                            <label id="lblMsgButiranKuasaEnjin" class="control-label" for="" style="display: none; color: #820303;">
                                                                *Masukkan Butiran
                                                            </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 116px; height: 25px;">&nbsp;</td>
                                                <td style="height: 25px">
                                                    <asp:LinkButton ID="LinkButton4" runat="server" CssClass="btn btn-info" OnClientClick="return fConfirm()">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                                                    </asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp; &nbsp;
                          <asp:LinkButton ID="LinkButton5" runat="server" CssClass="btn btn-info" OnClientClick="return fConfirmDel()">
						<i class="fas fa-trash-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Hapus
                          </asp:LinkButton>
                                                    &nbsp;&nbsp;<asp:LinkButton ID="LinkButton6" runat="server" CssClass="btn btn-info">
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
                                            <label class="control-label" for="">Jumlah rekod :</label>&nbsp;<label class="control-label" id="Label3" runat="server" style="color: mediumblue;"></label>
                                            &nbsp;&nbsp;   <b style="color: #969696;">|</b> &nbsp;&nbsp;
                                            <label class="control-label" for="Klasifikasi">Saiz Rekod : </label>
                                            <asp:DropDownList ID="DropDownList3" runat="server" Width="50px" AutoPostBack="True" class="form-control">
                                                <asp:ListItem Text="10" Value="10" />
                                                <asp:ListItem Text="25" Value="25" Selected="True" />
                                                <asp:ListItem Text="50" Value="50" />
                                                <asp:ListItem Text="100" Value="100" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <asp:GridView ID="gvKuasaEnjin" runat="server" AllowPaging="True"
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
                                                    <asp:Label ID="lblKodKuasaEnjin" runat="server" Text="Kod Kuasa Enjin" />&nbsp;
                                                    <asp:LinkButton ID="lnkKodKuasaEnjin" runat="server" CommandName="Sort" CommandArgument="KodKuasaEnjin"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblKodKuasaEnjin" runat="server" Text='<%# Eval("Kod")%>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="Butiran" HeaderText="Butiran Kuasa Enjin" SortExpression="ButiranKuasaEnjin">
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

<ajaxToolkit:TabPanel ID="TabKelasKend" runat="Server" HeaderText="KelasKend">
                        <HeaderTemplate>
                            Kelas Kenderaan
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
                                                    <asp:TextBox ID="txtkodKelasKend" runat="server" class="form-control" Width="20%" MaxLength="2" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                                    <i aria-hidden="true" class="fas fa-info-circle fa-lg" data-placement="right" style="cursor: pointer; color: #327aef;" data-toggle="tooltip" title="Masukkan 2 digit numerik sahaja."></i>
                                                    &nbsp;<label id="lblMsgKodKelasKend" class="control-label" for="" style="display: none; color: #820303;">
                                                        (Masukkan Kod )
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr style="height: 25px">
                                                <td style="width: 116px; height: 22px;">
                                                    Butiran<label class="control-label" for="Butiran">:               
                                                </td>
                                                <td style="height: 22px">
                                                    <asp:TextBox ID="txtButirKelasKend" runat="server" class="form-control" Width="60%" onkeyup="copyText3()"></asp:TextBox>
                                                    &nbsp;
                                                            <label id="lblMsgButiranKelasKend" class="control-label" for="" style="display: none; color: #820303;">
                                                                *Masukkan Butiran
                                                            </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 116px; height: 25px;">&nbsp;</td>
                                                <td style="height: 25px">
                                                    <asp:LinkButton ID="LinkButton7" runat="server" CssClass="btn btn-info" OnClientClick="return fConfirm()">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                                                    </asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp; &nbsp;
                          <asp:LinkButton ID="LinkButton8" runat="server" CssClass="btn btn-info" OnClientClick="return fConfirmDel()">
						<i class="fas fa-trash-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Hapus
                          </asp:LinkButton>
                                                    &nbsp;&nbsp;<asp:LinkButton ID="LinkButton9" runat="server" CssClass="btn btn-info">
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
                                            <label class="control-label" for="">Jumlah rekod :</label>&nbsp;<label class="control-label" id="Label4" runat="server" style="color: mediumblue;"></label>
                                            &nbsp;&nbsp;   <b style="color: #969696;">|</b> &nbsp;&nbsp;
                                            <label class="control-label" for="Klasifikasi">Saiz Rekod : </label>
                                            <asp:DropDownList ID="DropDownList4" runat="server" Width="50px" AutoPostBack="True" class="form-control">
                                                <asp:ListItem Text="10" Value="10" />
                                                <asp:ListItem Text="25" Value="25" Selected="True" />
                                                <asp:ListItem Text="50" Value="50" />
                                                <asp:ListItem Text="100" Value="100" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <asp:GridView ID="gvKelasKend" runat="server" AllowPaging="True"
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
                                                    <asp:Label ID="lblKodKelasKend" runat="server" Text="Kod Kelas Kenderaan" />&nbsp;
                                                    <asp:LinkButton ID="lnkKodKelasKend" runat="server" CommandName="Sort" CommandArgument="KodKelasKend"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblKodKelasKend" runat="server" Text='<%# Eval("Kod")%>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="Butiran" HeaderText="Butiran Kelas Kenderaan" SortExpression="ButiranKelasKend">
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

<ajaxToolkit:TabPanel ID="tabKendSediada" runat="Server" HeaderText="KendSediada">
                        <HeaderTemplate>
                            Kenderaan Sedia Ada
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
                                                    <asp:TextBox ID="txtKodKendSediada" runat="server" class="form-control" Width="20%" MaxLength="2" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                                    <i aria-hidden="true" class="fas fa-info-circle fa-lg" data-placement="right" style="cursor: pointer; color: #327aef;" data-toggle="tooltip" title="Masukkan 2 digit numerik sahaja."></i>
                                                    &nbsp;<label id="lblMsgKodKendSediada" class="control-label" for="" style="display: none; color: #820303;">
                                                        (Masukkan Kod )
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr style="height: 25px">
                                                <td style="width: 116px; height: 22px;">
                                                    Butiran<label class="control-label" for="Butiran">:               
                                                </td>
                                                <td style="height: 22px">
                                                    <asp:TextBox ID="txtButirKendSediada" runat="server" class="form-control" Width="60%" onkeyup="copyText3()"></asp:TextBox>
                                                    &nbsp;
                                                            <label id="lblMsgButiranKendSediada" class="control-label" for="" style="display: none; color: #820303;">
                                                                *Masukkan Butiran
                                                            </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 116px; height: 25px;">&nbsp;</td>
                                                <td style="height: 25px">
                                                    <asp:LinkButton ID="LinkButton10" runat="server" CssClass="btn btn-info" OnClientClick="return fConfirm()">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                                                    </asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp; &nbsp;
                          <asp:LinkButton ID="LinkButton11" runat="server" CssClass="btn btn-info" OnClientClick="return fConfirmDel()">
						<i class="fas fa-trash-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Hapus
                          </asp:LinkButton>
                                                    &nbsp;&nbsp;<asp:LinkButton ID="LinkButton12" runat="server" CssClass="btn btn-info">
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
                                            <label class="control-label" for="">Jumlah rekod :</label>&nbsp;<label class="control-label" id="Label5" runat="server" style="color: mediumblue;"></label>
                                            &nbsp;&nbsp;   <b style="color: #969696;">|</b> &nbsp;&nbsp;
                                            <label class="control-label" for="Klasifikasi">Saiz Rekod : </label>
                                            <asp:DropDownList ID="DropDownList5" runat="server" Width="50px" AutoPostBack="True" class="form-control">
                                                <asp:ListItem Text="10" Value="10" />
                                                <asp:ListItem Text="25" Value="25" Selected="True" />
                                                <asp:ListItem Text="50" Value="50" />
                                                <asp:ListItem Text="100" Value="100" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <asp:GridView ID="gvKendSediada" runat="server" AllowPaging="True"
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
                                                    <asp:Label ID="lblKodKendSediada" runat="server" Text="Kod Kenderaan Sediada" />&nbsp;
                                                    <asp:LinkButton ID="lnkKodKendSediada" runat="server" CommandName="Sort" CommandArgument="KodKendSediada"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblKodKendSediada" runat="server" Text='<%# Eval("Kod")%>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="Butiran" HeaderText="Butiran Kenderaan Sediada" SortExpression="ButiranKendSediada">
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
