<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Senarai_Invois_Kewangan.aspx.vb" Inherits="SMKB_Web_Portal.Senarai_Invois_Cukai" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>

     <style>
        /* start bootstrap step form*/

 body {
    margin-top:30px;
}
.stepwizard-step p {
    margin-top: 0px;
    color:#666;
}
.stepwizard-row {
    display: table-row;
}
.stepwizard {
    display: table;
    width: 100%;
    position: relative;
}
.stepwizard-step button[disabled] {
    /*opacity: 1 !important;
    filter: alpha(opacity=100) !important;*/
}
.stepwizard .btn.disabled, .stepwizard .btn[disabled], .stepwizard fieldset[disabled] .btn {
    opacity:1 !important;
    color:#bbb;
}
.stepwizard-row:before {
    top: 14px;
    bottom: 0;
    position: absolute;
    content:" ";
    width: 100%;
    height: 1px;
    background-color: #ccc;
    z-index: 0;
}
.stepwizard-step {
    display: table-cell;
    text-align: center;
    position: relative;
}
.btn-circle {
    width: 30px;
    height: 30px;
    text-align: center;
    padding: 6px 0;
    font-size: 12px;
    line-height: 1.428571429;
    border-radius: 15px;
}

   .btnStep {
    -webkit-border-radius: 5;
    -moz-border-radius: 5;
    border-radius: 3px;
    font-family: Arial;
    font-size: 12px;
    padding: 5px 5px 5px 5px;
    border: solid #808285 1px;
    display: inline-block;
    padding: 6px 9px;
    font-size: 13px;
    border: 1px solid gray;
    border-radius: 30px;
}       
   /* end bootstrap step form*/
    </style>

    <script>

        function validate(s, args) {
    
            // get the list (rendered as table)
            var ButtonList = document.getElementById("<%=rbKontrak.ClientID %>");

            if (ButtonList != null) {
                // get the buttons (inputs) in the table
                var Buttons = ButtonList.getElementsByTagName("input");

                for (var x = 0; x < Buttons.length; x++) {
                    if (Buttons[x].checked) {
                        var valRbKontrak = Buttons[x].value;

                        if (valRbKontrak == 'True') {
                            args.IsValid = args.Value != '';

                        }
                        else {
                            args.IsValid = true;
                        }
                    }
                }
            }
    }

        //$(document).ready(function () {
        //    localStorage.setItem('activeTab', '#step-1');
        //    fStepForm();
        //});

        ////rebind step form function
        //var prm = Sys.WebForms.PageRequestManager.getInstance();
        //prm.add_endRequest(function () {
        //    fStepForm();          
        //});


        //function fStepForm() {
        //    //bind step form function
        //    var navListItems = $('div.setup-panel div a'),
        //        allWells = $('.setup-content'),
        //        allNextBtn = $('.nextBtn');

        //    allWells.hide();

        //    navListItems.click(function (e) {
        //        e.preventDefault();
        //        var $target = $($(this).attr('href')),
        //            $item = $(this);

        //        if (!$item.hasClass('disabled')) {
        //            navListItems.removeClass('btn-success').addClass('btn-default');
        //            $item.addClass('btn-success');
        //            allWells.hide();
        //            $target.show();
        //            $target.find('input:eq(0)').focus();
        //        }
        //    });

        //    allNextBtn.click(function () {
        //        var curStep = $(this).closest(".setup-content"),
        //            curStepBtn = curStep.attr("id"),
        //            nextStepWizard = $('div.setup-panel div a[href="#' + curStepBtn + '"]').parent().next().children("a"),
        //            curInputs = curStep.find("input[type='text'],input[type='url']"),
        //            curTextareas = curStep.find('textarea'),
        //            curSelect = curStep.find('select'),
        //            isValid = true;

        //        $(".form-group").removeClass("has-error");
        //        for (var i = 0; i < curInputs.length; i++) {
        //            if (!curInputs[i].validity.valid || curInputs[i].value == '0') {
        //                isValid = false;
        //                $(curInputs[i]).closest(".form-group").addClass("has-error");
        //            }
        //        }

        //        for (var i = 0; i < curTextareas.length; i++) {
        //            if (!curTextareas[i].validity.valid) {
        //                isValid = false;
        //                $(curTextareas[i]).closest(".form-group").addClass("has-error");
        //            }
        //        }

        //        for (var i = 0; i < curSelect.length; i++) {
        //            if (curSelect[i].value == '0') {
        //                isValid = false;
        //                $(curSelect[i]).closest(".form-group").addClass("has-error");
        //            }
        //        }

        //        if (isValid) nextStepWizard.removeAttr('disabled').trigger('click');
        //    });

        //    //$('div.setup-panel div a.btn-success').trigger('click');
        //    var activeTab = localStorage.getItem('activeTab');
        //    $('#myTab a[href="' + activeTab + '"]').trigger('click');

        //    $('a[data-toggle="tab"]').on('show.bs.tab', function (e) {
        //        localStorage.setItem('activeTab', $(e.target).attr('href'));
        //    });
        //}

    </script>



    <asp:UpdatePanel ID="up1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>
            <div id="divList" runat="server">

                <div class="row" style="width: 700px;">
                    <div class="well">
                        <table style="width: 100%;">
                            <tr>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:DropDownList ID="ddlCarian" runat="server" AutoPostBack="true" CssClass="form-control" Style="width: 35%; height: 21px;">
                                        <asp:ListItem Text="- KESELURUHAN -" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="No. Invois Sementara" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;
                          <asp:TextBox ID="txtCarian" runat="server" Width="250px" Enabled="false"></asp:TextBox>
                                    &nbsp; 
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>&nbsp;</td>
                                <td>
                                    <div style="margin-top: 20px;">
                                        <asp:LinkButton ID="lbtnCari" runat="server" CssClass="btn ">
						<i class="fas fa-search"></i>&nbsp;&nbsp;&nbsp;Cari
                                        </asp:LinkButton>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>

                <div class="row">
                    <div class="panel panel-default" style="width: 80%;">
                        <div class="panel-heading">Senarai Invois Kewangan</div>
                        <div class="panel-body">
                            <div class="GvTopPanel" style="height: 33px;">
                                <div style="float: left; margin-top: 5px; margin-left: 10px;">
                                    <label class="control-label" for="">Jumlah rekod :</label>&nbsp;<label class="control-label" id="lblJumRekod" runat="server" style="color: mediumblue;"></label>
                                    &nbsp;&nbsp;   <b style="color: #969696;">|</b> &nbsp;&nbsp;

                    <label class="control-label" for="Klasifikasi">Saiz Rekod :</label>
                                    <asp:DropDownList ID="ddlSaizRekod" runat="server" Width="50px" AutoPostBack="True" class="form-control">
                                        <asp:ListItem Text="10" Value="10" />
                                        <asp:ListItem Text="25" Value="25" Selected="True" />
                                        <asp:ListItem Text="50" Value="50" />
                                        <asp:ListItem Text="100" Value="100" />
                                    </asp:DropDownList>

                                    &nbsp;&nbsp;<b style="color: #969696;">|</b> &nbsp;&nbsp;
                    Status : &nbsp;
                    <asp:DropDownList ID="ddlFilStat" runat="server" AutoPostBack="True" CssClass="form-control" />

                                </div>


                            </div>
                            <asp:GridView ID="gvLst" runat="server" 
                                AllowPaging="True" 
                                AllowSorting="True"
                                ShowHeaderWhenEmpty="True"
                                AutoGenerateColumns="False"
                                EmptyDataText="Tiada rekod"
                                PageSize="25" 
                                CssClass="table table-striped table-bordered table-hover"
                                Width="100%" Height="100%" 
                                HeaderStyle-BackColor="#6699FF"
                                Font-Size="8pt" BorderColor="#333333" 
                                BorderStyle="Solid" ShowFooter="True">
                                <Columns>

                                    <asp:TemplateField HeaderText="Bil">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBil" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIdBil" runat="server" Text='<%# Eval("AR01_IdBil")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblNoInvSem" runat="server" Text="No. Invois Sementara" />&nbsp;
                            <asp:LinkButton ID="lnkIdInvSem" runat="server" CommandName="Sort" CommandArgument="AR01_NoBilSem"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoInvSem" runat="server" Text='<%# Eval("AR01_NoBilSem")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="15%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblTuj" runat="server" Text="Tujuan" />&nbsp;
                            <asp:LinkButton ID="lnkTuj" runat="server" CommandName="Sort" CommandArgument="AR01_Tujuan"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTuj" runat="server" Text='<%# Eval("AR01_Tujuan")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="25%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblJenUNiaga" runat="server" Text="Jenis Urusniaga" />&nbsp;
                            <asp:LinkButton ID="lnkJenUNiaga" runat="server" CommandName="Sort" CommandArgument="ButJenUNiaga"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblKodJenUNiaga" runat="server" Visible ="false" Text='<%# Eval("AR01_JenisUrusniaga")%>' />
                                            <asp:Label ID="lblJenUNiaga" runat="server" Text='<%# Eval("ButJenUNiaga")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="12%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblKat" runat="server" Text="Kategori" />&nbsp;
                            <asp:LinkButton ID="lnkKat" runat="server" CommandName="Sort" CommandArgument="ButKat"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblKat" runat="server" Visible="false" Text='<%# Eval("AR01_Kategori")%>' />
                                            <asp:Label ID="lblButkat" runat="server" Text='<%# Eval("ButKat")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="12%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblTkhMhn" runat="server" Text="Tarikh Mohon" />&nbsp;
                            <asp:LinkButton ID="lnkTkhMhn" runat="server" CommandName="Sort" CommandArgument="AR01_TkhMohon"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTkhMhn" runat="server" Text='<%# Eval("AR01_TkhMohon", "{0:dd/MM/yyyy}")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="12%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblJumlah" runat="server" Text="Jumlah Bayar (RM)" />&nbsp;
                            <asp:LinkButton ID="lnkJumlah" runat="server" CommandName="Sort" CommandArgument="AR01_Jumlah"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblJumlah" runat="server" ForeColor="#003399" Text='<%# Eval("AR01_Jumlah", "{0:###,###,###.00}")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Width="12%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblStat" runat="server" Text="Status" />&nbsp;
                            <asp:LinkButton ID="lnkStat" runat="server" CommandName="Sort" CommandArgument="ButStatDok"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblKodStat" runat="server" Visible="false" Text='<%# Eval("AR01_StatusDok") %>' />
                                            <asp:Label ID="lblStat" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("ButStatDok"))) %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="15%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnSelect" runat="server" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CommandName="Select" CssClass="btn-xs" ToolTip="Pilih">
                                                        <i class="fas fa-edit"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Width="3%" HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                </Columns>
                                <HeaderStyle BackColor="#6699FF"></HeaderStyle>
                            </asp:GridView>

                        </div>
                    </div>
                </div>
            </div>

            <div id="divDetail" runat="server" style="width: 100%;">

                <div class="row">
                    <asp:LinkButton ID="lnkBtnBack" runat="server" CssClass="btn " Width="200px" ToolTip="" OnClientClick="return confirm('Anda pasti untuk kembali ke Paparan Senarai?');">
						<i class="fas fa-long-arrow-alt-left  fa-lg"></i> &nbsp;&nbsp; Kembali ke Senarai
                    </asp:LinkButton>
                </div>


                <%--<div class="stepwizard">
                    <div class="stepwizard-row setup-panel" id="myTab">
                        <div class="stepwizard-step col-xs-3"> 
                            <a href="#step-1" type="button" class="btnStep btn-success btn-circle" data-toggle="tab">1</a>
                            <p>Maklumat Invois Cukai</p>
                        </div>
                        <div class="stepwizard-step col-xs-3"> 
                            <a href="#step-2" type="button" class="btnStep btn-default btn-circle" data-toggle="tab">2</a>
                            <p>Dokumen Sokongan</p>
                        </div>                   
                    </div>
                </div>--%>


                <form role="form">

                    <div class="panel panel-default" style="width: 90%;">
                        <div class="panel-heading">
                            Maklumat Invois
                        </div>
                        <div class="panel-body">

                            <div class="alert alert-info" style="padding: 1px;">
                                <div class="row" style="margin: 0;">
                                    <div class="col-sm-2" style="background-color: white; width: 45px; padding: 10px;"><strong><span style="font-size: 20px;"><i class="fas fa-info-circle fa-lg"></i></span></strong></div>
                                    <div class="col-sm-8" style="padding: 10px;">
                                        <ul>
                                            <li><span style="color: red">* </span>(Ruang wajib)</li>
                                        </ul>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <table style="width: 100%;">
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td style="width: 130px">No. Invois Sementara </td>
                                        <td>:</td>
                                        <td style="width: 400px">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtNoInvSem" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 30%;"></asp:TextBox>
                                            </div>
                                        </td>
                                        <td style="width: 25px">&nbsp;</td>
                                        <td><span style="color: red">*</span></td>
                                        <td style="width: 100px">Status</td>
                                        <td>:</td>
                                        <td colspan="4">
                                            <asp:TextBox ID="txtStatus" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Width="88%"></asp:TextBox>
                                            <asp:HiddenField ID="HidKodStatus" runat="server" />
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>Tarikh Mohon</td>
                                        <td>:</td>
                                        <td>
                                            <div class="form-group">
                                                <asp:TextBox ID="txtTkhMohon" runat="server" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 30%;"></asp:TextBox>
                                            </div>

                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td colspan="4">&nbsp;</td>

                                    </tr>
                                    <tr>
                                        <td><span style="color: red">*</span></td>
                                        <td>Bank</td>
                                        <td>:</td>
                                        <td>
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlBank" runat="server" CssClass="form-control" Style="width: 350px; height: 21px;" required="required" />
                                                <div>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" CssClass="text-danger" ControlToValidate="ddlBank" InitialValue="0" ErrorMessage="Sila pilih Bank" ValidationGroup="grpSimpan" Display="Dynamic" />
                                                </div>
                                            </div>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td class="50">&nbsp;</td>
                                        <td class="50">No. Rujukan</td>
                                        <td>:</td>
                                        <td colspan="4">
                                            <div class="form-group">
                                                <input id="txtNoRujukan" runat="server" class="form-control" placeholder="Masukkan No. Rujukan" style="width: 300px" type="text" />
                                            </div>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td><span style="color: red">*</span></td>
                                        <td>Jenis Urusniaga</td>
                                        <td>:</td>
                                        <td>
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlJenis" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="text-danger" ControlToValidate="ddlJenis" InitialValue="0" ErrorMessage="Sila pilih Jenis" ValidationGroup="grpSimpan" Display="Dynamic" />
                                            </div>
                            </div>
                            </td>
                                    <td>&nbsp;</td>
                            <td><span style="color: red">*</span></td>
                            <td>Alamat </td>
                            <td>:</td>
                            <td colspan="4">
                                <div class="form-group">
                                    <input id="txtAlmt1" runat="server" type="text" required="required" class="form-control" placeholder="Masukkan Alamat 1" style="width: 85%" />
                                    <div>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtAlmt1" CssClass="text-danger" ErrorMessage="Masukkan Alamat" ValidationGroup="grpSimpan" Display="Dynamic" />
                                    </div>
                                </div>
                            </td>
                            </tr>
                                <tr>
                                    <td><span style="color: red">*</span></td>
                                    <td>Kategori</td>
                                    <td>:</td>
                                    <td>
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlKat" runat="server" AutoPostBack="True" CssClass="form-control" Style="width: 250px; height: 21px;">
                                            </asp:DropDownList>
                                            <div>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="text-danger" ControlToValidate="ddlKat" InitialValue="0" ErrorMessage="Sila pilih Kategori" ValidationGroup="grpSimpan" Display="Dynamic" />
                                            </div>
                                        </div>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>:</td>
                                    <td colspan="4">
                                        <div class="form-group">
                                            <input id="txtAlmt2" runat="server" type="text" required="required" class="form-control" placeholder="Masukkan Alamat 2" style="width: 85%" />
                                        </div>
                                    </td>

                                </tr>
                            <tr>
                                <td><span style="color: red">*</span></td>
                                <td>Nama Penerima</td>
                                <td>:</td>
                                <td>

                                    <asp:TextBox ID="txtNamaPenerima" runat="server" BackColor="#FFFFCC" CssClass="form-control" Width="95%" Visible="false"></asp:TextBox>
                                </td>
                                <td>&nbsp;</td>
                                <td><span style="color: red">*</span></td>
                                <td>Bandar</td>
                                <td>:</td>
                                <td>
                                    <div class="form-group">
                                        <input id="txtBandar" runat="server" class="form-control" placeholder="Masukkan Bandar" required="required" style="width: 200px" type="text" />
                                        <div>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBandar" CssClass="text-danger" ErrorMessage="Masukkan Bandar" ValidationGroup="grpSimpan" Display="Dynamic" />
                                        </div>
                                    </div>
                                </td>

                                <td>
                                    <span style="color: red">* </span>Poskod &nbsp;&nbsp;&nbsp;</td>

                                <td>:</td>

                                <td>
                                    <div class="form-group">
                                        <input id="txtPoskod" runat="server" class="form-control" placeholder="Masukkan Poskod" required="required" style="width: 200px" type="text" />
                                        <div>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPoskod" CssClass="text-danger" ErrorMessage="Masukkan Poskod" ValidationGroup="grpSimpan" Display="Dynamic" />
                                        </div>
                                    </div>
                                </td>

                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>ID / No.KP Penerima</td>
                                <td>:</td>
                                <td>
                                    <div class="form-group">
                                        <input id="txtIDPenerima" runat="server" type="text" required="required" class="form-control" placeholder="Pilih Nama Penerima" style="width: 85%; background-color: #FFFFCC" />
                                    </div>
                                </td>
                                <td>&nbsp;</td>
                                <td><span style="color: red">*</span></td>
                                <td>Negara</td>
                                <td>:</td>
                                <td colspan="4">
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlNegara" runat="server" AutoPostBack="True" CssClass="form-control" Style="width: 88%; height: 21px;">
                                        </asp:DropDownList>
                                        <div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="text-danger" ControlToValidate="ddlNegara" InitialValue="0" ErrorMessage="Sila pilih Negara" ValidationGroup="grpSimpan" Display="Dynamic" />
                                        </div>
                                    </div>
                                </td>

                            </tr>
                            <tr>
                                <td style="vertical-align: top"><span style="color: red">*</span></td>
                                <td style="vertical-align: top">Tujuan</td>
                                <td style="vertical-align: top">:</td>
                                <td>
                                    <div class="form-group">
                                        <textarea id="txtTujuan" runat="server" class="form-control" cols="20" name="S1" placeholder="Masukkan tujuan" required="required" rows="3" style="width: 95%;"></textarea>
                                        <div>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtTujuan" CssClass="text-danger" ErrorMessage="Masukkan Tujuan" ValidationGroup="grpSimpan" Display="Dynamic" />
                                        </div>
                                    </div>

                                </td>
                                <td>&nbsp;</td>
                                <td><span style="color: red">*</span></td>
                                <td>Negeri</td>
                                <td>:</td>
                                <td colspan="4">
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlNegeri" runat="server" AutoPostBack="True" CssClass="form-control" Style="width: 88%; height: 21px;">
                                        </asp:DropDownList>
                                        <div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="text-danger" ControlToValidate="ddlNegeri" InitialValue="0" ErrorMessage="Sila pilih Negeri" ValidationGroup="grpSimpan" Display="Dynamic" />
                                        </div>
                                    </div>
                                </td>

                            </tr>
                            <tr>
                                <td style="vertical-align: top"><span style="color: red">*</span></td>
                                <td style="vertical-align: top">Berkontrak</td>
                                <td style="vertical-align: top">:</td>
                                <td>
                                    <asp:RadioButtonList ID="rbKontrak" runat="server" Height="25px" RepeatDirection="Horizontal" Width="273px" AutoPostBack="true">
                                        <asp:ListItem Selected="True" Text=" Ya" Value="True"></asp:ListItem>
                                        <asp:ListItem Text=" Tidak" Value="False"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>&nbsp;</td>
                                <td><span style="color: red">*</span></td>
                                <td>No.Tel</td>
                                <td>:</td>
                                <td>
                                    <div class="form-group">
                                        <input id="txtNoTel" runat="server" class="form-control" placeholder="Masukkan No. Tel" required="required" style="width: 200px" type="text" />
                                    </div>
                                    <div>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNoTel" CssClass="text-danger" ErrorMessage="Masukkan No. Tel" ValidationGroup="grpSimpan" Display="Dynamic" />
                                    </div>
                                </td>

                                <td>No.Fax</td>

                                <td>:</td>

                                <td>
                                    <div class="form-group">
                                        <input id="txtNoFax" runat="server" class="form-control" placeholder="Masukkan No. Fax" style="width: 200px" type="text" />
                                    </div>
                                </td>



                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>Memorandum</td>
                                <td>:</td>
                                <td>

                                    <asp:DropDownList ID="ddlMemo" runat="server" AutoPostBack="True" CssClass="form-control" Enabled="false" Style="width: 400px; height: 21px;">
                                    </asp:DropDownList>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>Emel</td>
                                <td>:</td>
                                <td colspan="4">
                                    <input id="txtEmel" runat="server" class="form-control" placeholder="Masukkan Emel" style="width: 500px" type="text" />
                                </td>

                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>Tarikh Mula Kontrak</td>
                                <td>:</td>
                                <td>
                                    <asp:TextBox ID="txtTkhMula" runat="server" AutoPostBack="true" BackColor="#FFFFCC" CssClass="form-control" Enabled="false" Style="width: 150px;"></asp:TextBox>
                                    <button id="btnCalMula" runat="server" class="btnCal" disabled="disabled">
                                        <i class="far fa-calendar-alt icon-4x"></i>
                                    </button>
                                    <cc1:CalendarExtender ID="cal2" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" PopupButtonID="btnCalMula" TargetControlID="txtTkhMula" TodaysDateFormat="dd/MM/yyyy" />
                                    <button id="btnPadamMula" runat="server" class="btnCal" style="margin-top: -4px;" title="Padam Tarikh Mula">
                                        <%--<i class="fas fa-times icon-4x""></i>--%><i class="fas fa-eraser icon-4x"></i>
                                    </button>
                                    <div>
                                        <%--<asp:CustomValidator ID="cv1" runat="server" ErrorMessage="Pilih Tarikh Mula" ControlToValidate="txtTkhMula" ValidateEmptyText="True" ClientValidationFunction="validate" ValidationGroup="grpSimpan" CssClass="text-danger" />--%>
                                    </div>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>Untuk Perhatian</td>
                                <td>:</td>
                                <td colspan="4">
                                    <div class="form-group">
                                        &nbsp;<input id="txtPerhatian" runat="server" class="form-control" placeholder="Masukkan Nama" style="width: 500px" type="text" /></div>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>Tarikh Tamat Kontrak</td>
                                <td>:</td>
                                <td>

                                    <asp:TextBox ID="txtTkhTamat" runat="server" CssClass="form-control" Style="width: 150px;" BackColor="#FFFFCC" AutoPostBack="true" Enabled="false"></asp:TextBox>

                                    <button id="btnCalTamat" class="btnCal" runat="server" disabled="disabled">
                                        <i class="far fa-calendar-alt icon-4x"></i>
                                    </button>
                                    <cc1:CalendarExtender ID="cal1" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" PopupButtonID="btnCalTamat" TargetControlID="txtTkhTamat" TodaysDateFormat="dd/MM/yyyy" />


                                    <button id="btnPadamTamat" runat="server" class="btnCal" style="margin-top: -4px;" title="Padam Tarikh Tamat">
                                        <%--<i class="fas fa-times icon-4x""></i>--%>
                                        <i class="fas fa-eraser icon-4x"></i>
                                    </button>
                                    <div>
                                       <%-- <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Pilih Tarikh Tamat" ControlToValidate="txtTkhTamat" ValidateEmptyText="True" ClientValidationFunction="validate" ValidationGroup="grpSimpan" CssClass="text-danger" />--%>
                                    </div>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td colspan="4">

                                </td>

                            </tr>

                            <tr>
                                <td>&nbsp;</td>
                                <td>Tempoh Kontrak</td>
                                <td>:</td>
                                <td>
                                    <asp:TextBox ID="txtTemKon" runat="server" BackColor="#FFFFCC" CssClass="form-control" Style="width: 50px;" Text="0" Enabled="false"></asp:TextBox>

                                    &nbsp;<asp:DropDownList ID="ddlJenTemp" runat="server" CssClass="form-control" Enabled="false" />
                                        
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td colspan="4">&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td colspan="4">&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td colspan="4">&nbsp;</td>
                            </tr>

                            </table>                       
                        </div>

                        <div class="panel panel-default" style="width: 95%;">
                            <div class="panel-heading">
                                <h3 class="panel-title"><span style="color: red">*</span>Transaksi</h3>
                            </div>
                            <div class="panel-body">

                                <asp:GridView ID="gvInvDt" runat="server" AllowSorting="true" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText=" Tiada rekod"
                                    CssClass="table table-striped table-bordered table-hover" Font-Size="8pt" BorderStyle="Solid" ShowFooter="True" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Bil" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="2%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblId" runat="server" Text='<%# Eval("AR01_BilDtID")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="KW">
                                            <ItemTemplate>
                                                <div class="form-group">
                                                    <asp:HiddenField ID="hidKW" runat="server" Value='<%#Eval("KodKw")%>' />
                                                    <asp:DropDownList ID="ddlKW" runat="server" CssClass="form-control" Style="width: 150px; height: 21px;" AutoPostBack="true" OnSelectedIndexChanged="ddlKW_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <div>
                                                        <asp:RequiredFieldValidator ID="rfvKW" runat="server" CssClass="text-danger" ControlToValidate="ddlKW" InitialValue="0" ErrorMessage="Sila pilih KW" ValidationGroup="grpSimpan" Display="Dynamic" />
                                                    </div>
                                                </div>

                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="KO">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hidKO" runat="server" Value='<%#Eval("KodKO")%>' />
                                                <div class="form-group">
                                                    <asp:DropDownList ID="ddlKO" runat="server" CssClass="form-control" Style="width: 150px; height: 21px;" AutoPostBack="true" OnSelectedIndexChanged="ddlKO_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <div>
                                                        <asp:RequiredFieldValidator ID="rfvKO" runat="server" CssClass="text-danger" ControlToValidate="ddlKO" InitialValue="0" ErrorMessage="Sila pilih KO" ValidationGroup="grpSimpan" Display="Dynamic" />
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PTj">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hidPTj" runat="server" Value='<%#Eval("kodPTJ")%>' />
                                                <div class="form-group">
                                                    <asp:DropDownList ID="ddlPTj" runat="server" CssClass="form-control" Style="width: 150px; height: 21px;" AutoPostBack="true" OnSelectedIndexChanged="ddlPTj_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <div>
                                                        <asp:RequiredFieldValidator ID="rfvPTj" runat="server" CssClass="text-danger" ControlToValidate="ddlPTj" InitialValue="0" ErrorMessage="Sila pilih PTj" ValidationGroup="grpSimpan" Display="Dynamic" />
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="KP">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hidKP" runat="server" Value='<%#Eval("kodKP")%>' />
                                                <div class="form-group">
                                                    <asp:DropDownList ID="ddlKP" runat="server" CssClass="form-control" Style="width: 150px; height: 21px;" AutoPostBack="true" OnSelectedIndexChanged="ddlKP_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <div>
                                                        <asp:RequiredFieldValidator ID="rfvKP" runat="server" CssClass="text-danger" ControlToValidate="ddlKP" InitialValue="0" ErrorMessage="Sila pilih KP" ValidationGroup="grpSimpan" Display="Dynamic" />
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Vot">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hidVot" runat="server" Value='<%#Eval("kodVot")%>' />
                                                <div class="form-group">
                                                    <asp:DropDownList ID="ddlVot" runat="server" CssClass="form-control" Style="width: 150px; height: 21px;">
                                                    </asp:DropDownList>
                                                    <div>
                                                        <asp:RequiredFieldValidator ID="rfvVot" runat="server" CssClass="text-danger" ControlToValidate="ddlVot" InitialValue="0" ErrorMessage="Sila pilih Vot" ValidationGroup="grpSimpan" Display="Dynamic" />
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Perkara">
                                            <ItemTemplate>
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtPerkara" runat="server" CssClass="form-control" Text='<%#Eval("AR01_Perkara")%>' placeholder="Masukkan Perkara" required="required" Style="width: 100%;"></asp:TextBox>
                                                    <div>
                                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPerkara" CssClass="text-danger" ErrorMessage="Masukkan Perkara" ValidationGroup="grpSimpan" Display="Dynamic" />
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="300px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Kuantiti">
                                            <ItemTemplate>
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtKuantiti" runat="server" CssClass="form-control rightAlign" Style="width: 100%;" placeholder="0.00" required="required" onkeypress="return isNumberKey(event,this)" Text='<%#Eval("AR01_Kuantiti", "{0:N0}")%>' OnTextChanged="txtKuantiti_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    <div>
                                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtKuantiti" CssClass="text-danger" ErrorMessage="Masukkan Kuantiti" ValidationGroup="grpSimpan" Display="Dynamic" InitialValue="0" />
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Harga (RM)">
                                            <ItemTemplate>
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtHarga" runat="server" CssClass="form-control rightAlign" Style="width: 100%;" onkeypress="return isNumberKey(event,this)" placeholder="0.00" required="required" Text='<%#Eval("AR01_kadarHarga", "{0:###,###,###.00}")%>' OnTextChanged="txtHarga_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    <div>
                                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtHarga" CssClass="text-danger" ErrorMessage="Masukkan Harga" ValidationGroup="grpSimpan" Display="Dynamic" InitialValue="0" />
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div style="text-align: right; font-weight: bold;">
                                                    <asp:Label runat="server" Text="Jumlah Besar (RM)" />
                                                </div>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="150px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Jumlah (RM)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblJumlah" runat="server" ForeColor="#003399" Text='<%#Eval("AR01_Jumlah", "{0:###,###,###.00}")%>' />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div style="text-align: right;">
                                                    <asp:Label ID="lblTotJum" runat="server" ClientIDMode="Static" CssClass="cssTotJum" ForeColor="#003399" Font-Bold="true" />
                                                </div>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="150px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" HeaderText="Tindakan" FooterStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                &nbsp;
                                                        <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete" CssClass="btn-xs" ToolTip="Padam"
                                                            OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');">
										                    <i class="far fa-trash-alt fa-lg"></i>
                                                        </asp:LinkButton>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnTambah" runat="server" CausesValidation="true" CssClass="btn " ToolTip="Tambah transaksi" OnClick="lbtnTambah_Click" Width="50px">
                        <i class="fas fa-plus fa-lg"></i>
                                                </asp:LinkButton>
                                            </FooterTemplate>

                                            <FooterStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <EmptyDataTemplate>
                                    </EmptyDataTemplate>
                                    <SelectedRowStyle ForeColor="Blue" />
                                </asp:GridView>


                            </div>
                        </div>


                    </div>
                    <%--      </div>--%>

                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Muat Naik Dokumen Sokongan
                        </div>
                        <div class="panel-body">
                            <div class="alert alert-info" style="padding: 1px;">
                                <div class="row" style="margin: 0;">
                                    <div class="col-sm-2" style="background-color: white; width: 60px; padding: 18px;"><strong><span style="font-size: 20px;"><i class="fas fa-info-circle fa-lg"></i></span></strong></div>
                                    <div class="col-sm-8" style="padding: 10px;">
                                        <ul>
                                            <li>Muat naik dokumen sokongan (jika ada)</li>
                                            <li>Saiz maksimum setiap fail: 4 MB</li>
                                        </ul>
                                    </div>
                                </div>
                            </div>






                            <div class="well" style="margin-top: 60px;">


                                <asp:FileUpload ID="FileUpload1" runat="server" Width="450px" />
                                <div style="margin-top: 20px">
                                    <asp:LinkButton ID="lbtnUpload" runat="server" CssClass="btn " Width="100px">
						<i class="fas fa-file-upload fa-lg"></i> &nbsp; Muat Naik
                                    </asp:LinkButton>
                                    <%--<asp:LinkButton ID="lBtnAdd" runat="server" CssClass="btn " Width="80px">
						<i class="far fa-save fa-lg"></i> &nbsp;&nbsp; Tambah
					</asp:LinkButton>--%>
                                </div>
                            </div>
                            <div class="row">
                                <asp:GridView ID="gvLamp" runat="server" AllowSorting="True" AutoGenerateColumns="False" BorderColor="#333333" BorderStyle="Solid"
                                    CssClass="table table-striped table-bordered table-hover" EmptyDataText=" " Font-Size="8pt" HeaderStyle-BackColor="#6699FF"
                                    Height="100%" PageSize="10" ShowFooter="True" ShowHeaderWhenEmpty="True" Width="600px">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Bil">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBil" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="3%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblId" runat="server" Text='<%# Eval("AR11_ID")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdBil" runat="server" Text='<%# Eval("AR01_IdBil")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nama Dokumen">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNamaDok" runat="server" Text='<%# Eval("AR11_NamaDok")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDokPath" runat="server" Text='<%# Eval("AR11_Path")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("AR11_Status")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>

                                                <asp:LinkButton ID="lbtnSelect" runat="server" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CommandName="Select" CssClass="btn-xs" ToolTip="Lihat Lampiran">
                              <i class="fab fa-readme fa-lg"></i>                           
                                                </asp:LinkButton>

                                                <asp:LinkButton ID="lbtnDelete" runat="server" CommandArgument='<%# Eval("GUID") %>' CommandName="Delete" CssClass="btn-xs" OnClientClick="return confirm('Anda pasti untuk padam lampiran ini?');" ToolTip="Padam">
										                    <i class="far fa-trash-alt fa-lg"></i>
                                                </asp:LinkButton>

                                            </ItemTemplate>
                                            <ItemStyle Width="80px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="#6699FF" />
                                </asp:GridView>
                            </div>


                            <%-- <asp:FileUpload ID="fu1" runat="server" style="width:650px;"/>--%>

                            <%--  <asp:LinkButton ID="lBtnUpload" runat="server" CssClass="btn " Width="80px">
						<i class="far fa-save fa-lg"></i> &nbsp;&nbsp; Muat Naik
					</asp:LinkButton>

    <div id="FileUploadContainer"></div>
    <div style="text-align:left;margin-top:20px;">

        <button type="button" class="btn " onclick="AddFileUpload()" style="width:100px" title="Tambah fail">
     <i class="fas fa-plus fa-lg"></i>&nbsp;Tambah fail</button>
      
        </div>--%>



                            <%--<asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />--%>





                            <%-- <div style="margin-bottom:10px;margin-top:20px;text-align:center;">
                <asp:LinkButton ID="lnkBtnSaveDoc" runat="server" CssClass="btn " Width="80px" ToolTip="Simpan">
						<i class="far fa-save fa-lg"></i> &nbsp;&nbsp; Simpan
					</asp:LinkButton>
                    </div>--%>
                        </div>
                    </div>

                    <asp:ValidationSummary runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="grpSimpan" />
                    <div style="margin-bottom: 10px; margin-top: 20px; text-align: center;">
                        <asp:LinkButton ID="lnkBtnSaveInv" runat="server" CssClass="btn " Width="80px" ToolTip="Simpan" OnClientClick="return confirm('Anda pasti untuk simpan rekod ini?');" ValidationGroup="grpSimpan">
						<i class="far fa-save fa-lg"></i> &nbsp;&nbsp; Simpan
                        </asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="lBtnHapus" runat="server" CssClass="btn " Width="80px" ToolTip="Hapus" OnClientClick="return confirm('Anda pasti untuk hapuskan rekod ini?');">
						<i class="fas fa-trash-alt"></i> &nbsp;&nbsp; Hapus
            </asp:LinkButton>
                    </div>
                    <asp:HiddenField ID="hidIdBil" runat="server"></asp:HiddenField>
                </form>
    

                </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lbtnUpload" />
        </Triggers>
    </asp:UpdatePanel>

    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>

            <div style="background-color: #D2D2D2; filter: alpha(opacity=80); opacity: 0.80; width: 100%; top: 0px; left: 0px; position: fixed; height: 100%;">
            </div>
            <div style="margin: auto; font-family: Trebuchet MS; filter: alpha(opacity=100); opacity: 1; font-size: small; vertical-align: middle; color: #FFFFFF; position: fixed; top: 50%; left: 50%; margin-top: -50px; margin-left: -100px;">
                <table>
                    <tr>
                        <td>
                            <img src="../../../Images/loader.gif" alt="" />
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
            
</asp:Content>
