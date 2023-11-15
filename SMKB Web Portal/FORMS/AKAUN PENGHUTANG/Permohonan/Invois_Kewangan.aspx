<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Invois_Kewangan.aspx.vb" Inherits="SMKB_Web_Portal.Invois_Cukai_Kewangan1" EnableEventValidation="false" %>
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
     var counter = 1;
     function AddFileUpload() {
         var div = document.createElement('DIV');
         div.innerHTML = '<input id="fu' + counter + '" name = "file' + counter +
                     '" type="file" style="width:650px;" />' +
                     '<button type="button" class="btn " onclick="RemoveFileUpload(this)" style="width:40px;height:20px;padding-bottom: 18px;" title="Padam"><span style=color:red;><i class="fas fa-times fa-lg"></i></span></button>';
                
         div.style.marginTop = "15px";
         //document.getElementById("FileUploadContainer").style.marginTop = "15px";
         document.getElementById("FileUploadContainer").appendChild(div);
         counter++;
     }
     function RemoveFileUpload(div) {
         document.getElementById("FileUploadContainer").removeChild(div.parentNode);
     }
    
    </script>

 
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>

<%--                <div class="stepwizard">
                    <div class="stepwizard-row setup-panel">
                        <div class="stepwizard-step col-xs-3">
                            <a href="#step-1" type="button" class="btnStep btn-success btn-circle">1</a>
                            <p>Maklumat Invois Cukai</p>
                        </div>
                        <div class="stepwizard-step col-xs-3">
                            <a href="#step-2" type="button" class="btnStep btn-default btn-circle disabled">2</a>
                            <p>Dokumen Sokongan</p>

                        </div>
                    </div>
                </div>--%>

                <form role="form">
                    <div class="row">
                        <div class="panel panel-default" id="step-1" style="width: 90%;">
                            <div class="panel-heading">
                                <%--  <h3 class="panel-title">Maklumat Invois Cukai</h3>--%>
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
                                                    <asp:TextBox ID="txtNoInvSem" runat="server" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 30%;"></asp:TextBox>
                                                    <asp:HiddenField ID="hidIdBil" runat="server" />
                                                </div>
                                            </td>
                                            <td style="width: 25px">&nbsp;</td>
                                            <td>&nbsp;</td>
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
                                                    <asp:DropDownList ID="ddlBank" runat="server" CssClass="form-control" Style="width: 350px; height: 21px;" required="required" />
                                                <div>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" CssClass="text-danger" ControlToValidate="ddlBank" InitialValue="0" ErrorMessage="Sila pilih Bank" ValidationGroup="grpSimpan" Display="Dynamic"/>
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
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="text-danger" ControlToValidate="ddlJenis" InitialValue="0" ErrorMessage="Sila pilih Jenis" ValidationGroup="grpSimpan" Display="Dynamic"/>
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
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtAlmt1" CssClass="text-danger" ErrorMessage="Masukkan Alamat" ValidationGroup="grpSimpan" Display="Dynamic"/>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td><span style="color: red">*</span></td>
                                            <td>Kategori</td>
                                            <td>:</td>
                                            <td>
                                                <asp:DropDownList ID="ddlKat" runat="server" AutoPostBack="True" CssClass="form-control" Style="width: 250px; height: 21px;">
                                                </asp:DropDownList>
                                                <div>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="text-danger" ControlToValidate="ddlKat" InitialValue="0" ErrorMessage="Sila pilih Kategori" ValidationGroup="grpSimpan" Display="Dynamic" />
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
                                                <div class="form-group">
                                                    <asp:DropDownList ID="ddlNmPenerima" runat="server" AutoPostBack="True" CssClass="form-control" Style="width: 400px; height: 21px;">
                                                    </asp:DropDownList>
                                                </div>
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
                                                 <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPoskod" CssClass="text-danger" ErrorMessage="Masukkan Poskod" ValidationGroup="grpSimpan" Display="Dynamic"/></div>
                                                </div>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>ID / No.KP Penerima</td>
                                            <td>:</td>
                                            <td>
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtIDPenerima" runat="server" BackColor="#FFFFCC" CssClass="form-control" Width="95%"></asp:TextBox>
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
                                            <td>&nbsp;</td>
                                            <td>Kod Penghutang</td>
                                            <td>:</td>
                                            <td>
                                                <asp:TextBox ID="txtKodPenghutang" runat="server" BackColor="#FFFFCC" CssClass="form-control" Width="250px"></asp:TextBox>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td colspan="4">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top"><span style="color: red">*</span></td>
                                            <td style="vertical-align: top">Tujuan</td>
                                            <td style="vertical-align: top">:</td>
                                            <td>
                                                    <textarea id="txtTujuan" runat="server" class="form-control" cols="20" name="S1" placeholder="Masukkan tujuan" required="required" rows="3" style="width: 95%;"></textarea>
                                                <div>
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtTujuan" CssClass="text-danger" ErrorMessage="Masukkan Tujuan" ValidationGroup="grpSimpan" Display="Dynamic"/>
                                                </div>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td><span style="color: red">*</span></td>
                                            <td>Negeri</td>
                                            <td>:</td>
                                            <td colspan="4">
                                                    <asp:DropDownList ID="ddlNegeri" runat="server" AutoPostBack="True" CssClass="form-control" Style="width: 88%; height: 21px;">
                                                    </asp:DropDownList>
                                                     <div>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="text-danger" ControlToValidate="ddlNegeri" InitialValue="0" ErrorMessage="Sila pilih Negeri" ValidationGroup="grpSimpan" Display="Dynamic"/></div>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top">&nbsp;</td>
                                            <td style="vertical-align: top">Berkontrak</td>
                                            <td style="vertical-align: top">:</td>
                                            <td>
                                                <asp:RadioButtonList ID="rbKontrak" runat="server" Height="25px" RepeatDirection="Horizontal" Width="273px" AutoPostBack="true" Enabled="false">
                                                    <asp:ListItem Text=" Ya" Value="True"></asp:ListItem>
                                                    <asp:ListItem Selected="True" Text=" Tidak" Value="False"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td><span style="color: red">*</span></td>
                                            <td>No.Tel</td>
                                            <td>:</td>
                                            <td>
                                                    <input id="txtNoTel" runat="server" class="form-control" placeholder="Masukkan No. Tel" required="required" style="width: 200px" type="text" />
                                                    <div>
                                                 <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNoTel" CssClass="text-danger" ErrorMessage="Masukkan No. Tel" ValidationGroup="grpSimpan" Display="Dynamic"/>
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
                                                <asp:DropDownList ID="ddlMemo" runat="server" AutoPostBack="True" CssClass="form-control" Style="width: 400px; height: 21px;" Enabled ="false">
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
                                                <asp:TextBox ID="txtTrkhMula" runat="server" AutoPostBack="true" BackColor="#FFFFCC" CssClass="form-control" Enabled="false" Style="width: 150px;"></asp:TextBox>
                                                <button id="btnCalMula" runat="server" class="btnCal" disabled="disabled">
                                                    <i class="far fa-calendar-alt icon-4x"></i>
                                                </button>
                                                <cc1:CalendarExtender ID="cal2" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" PopupButtonID="btnCalMula" TargetControlID="txtTrkhMula" TodaysDateFormat="dd/MM/yyyy" />
                                                <button id="btnPadamMula" runat="server" class="btnCal" style="margin-top: -4px;" title="Padam Tarikh Mula">
                                                    <%-- <i class="fas fa-times icon-4x""></i>--%><i class="fas fa-eraser icon-4x"></i>
                                                </button>
                                                <div>
                                                    <%--<asp:CustomValidator ID="cv1" runat="server" ErrorMessage="Pilih Tarikh Mula" ControlToValidate="TextBox1" ValidateEmptyText="True" ClientValidationFunction="validate" ValidationGroup="grpSimpan" CssClass="text-danger"/>--%>
                                                </div>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>Untuk Perhatian</td>
                                            <td>:</td>
                                            <td colspan="4">
                                            
                                                <input id="txtPerhatian" runat="server" class="form-control" placeholder="Masukkan Nama" style="width: 500px" type="text" />
                                            
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>Tarikh Tamat Kontrak</td>
                                            <td>:</td>
                                            <td>                                              
                                                <asp:TextBox ID="txtTrkhTmt" runat="server" CssClass="form-control" Style="width: 150px;" BackColor="#FFFFCC" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                        <button id="btnCalTamat" class="btnCal" runat="server" disabled="disabled">
                                            <i class="far fa-calendar-alt icon-4x"></i>
                                        </button>
                                        <cc1:CalendarExtender ID="cal1" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" PopupButtonID="btnCalTamat" TargetControlID="txtTrkhTmt" TodaysDateFormat="dd/MM/yyyy" />

                                       
                                            <button id="btnPadamTamat" runat="server" class="btnCal" style="margin-top: -4px;" title="Padam Tarikh Tamat">
                                                <%--<i class="fas fa-times icon-4x""></i>--%>
                                                <i class="fas fa-eraser icon-4x"></i>
                                            </button>
                                         <div>
                                   <%--     <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Pilih Tarikh Tamat" ControlToValidate="txtTrkhTmt" ValidateEmptyText="True" ClientValidationFunction="validate" ValidationGroup="grpSimpan" CssClass="text-danger" />--%>
                                             </div>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td></td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td colspan="4">

                                            </td>

                                        </tr>

                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>Tempoh kontrak</td>
                                            <td>:</td>
                                            <td>
                                                <asp:TextBox ID="txtTemKon" runat="server" BackColor="#FFFFCC" CssClass="form-control" Style="width: 50px;" Text="0"></asp:TextBox>

                                                &nbsp;&nbsp;
                                                <asp:DropDownList ID="ddlJenTemp" runat="server" CssClass="form-control" Enabled="false" />
                                            </td>
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
                                                        </div>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="300px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Kuantiti">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtKuantiti" runat="server" CssClass="form-control rightAlign" Style="width: 100%;" placeholder="0.00" required="required" onkeypress="return isNumberKey(event,this)" Text='<%#Eval("AR01_Kuantiti", "{0:N0}")%>' OnTextChanged="txtKuantiti_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        <div>
                                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtKuantiti" CssClass="text-danger" ErrorMessage="Masukkan Kuantiti" ValidationGroup="grpSimpan" Display="Dynamic" InitialValue="0" />
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

                                <%--   <div style="text-align: center; margin-bottom: 10px; margin-top: 20px;">
                                <button id="lbtnTerus" runat="server" class="btn btn-primary nextBtn pull-right" type="button" style="width: 120px;">Teruskan &nbsp;&nbsp; <i class="fas fa-angle-double-right fa-lg"></i></button>
                            </div>--%>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="panel panel-default" id="step-2">
                            <div class="panel-heading">
                                <%-- <h3 class="panel-title">Muat Naik Dokumen Sokongan</h3>--%>
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

                                <div class="row" runat="server">
                                    <asp:FileUpload ID="fu1" runat="server" Style="width: 650px;" />
                                    <div id="FileUploadContainer"></div>
                                    <div style="text-align: left; margin-top: 20px;">

                                        <button type="button" class="btn " onclick="AddFileUpload()" style="width: 100px" title="Tambah fail">
                                            <i class="fas fa-plus fa-lg"></i>&nbsp;Tambah fail
                                        </button>

                                    </div>
                                </div>


                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div style="text-align: center; margin-bottom: 10px; margin-top: 20px;">
                            <%-- <button id="btnSimpan" runat="server" class="btn" type="submit" style="width: 100px;"><i class="far fa-save fa-lg"></i>&nbsp;&nbsp; Simpan</button>--%>

                            <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn" Width="100px" ToolTip="Simpan" OnClientClick="return confirm('Anda pasti untuk simpan rekod ini?');" ValidationGroup="grpSimpan">
						<i class="far fa-save fa-lg"></i> &nbsp;&nbsp; Simpan
                            </asp:LinkButton>

                            &nbsp;&nbsp;

                            <asp:LinkButton ID="lbtnRekBaru" runat="server" CssClass="btn ">
						<i class="fas fa-file-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Rekod baru
					</asp:LinkButton>
                        </div>
                    </div>

                </form>

                <%-- <asp:HiddenField ID="hidNoStaf" runat="server" />
            <asp:HiddenField ID="hidKodPTj" runat="server" />--%>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lbtnSimpan" />
            <%--<asp:PostBackTrigger ControlID="btnUpload" />--%>
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
