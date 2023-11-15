<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="MaklumatSemakanBajet.aspx.vb" Inherits="SMKB_Web_Portal.MaklumatSemakanBajet" EnableEventValidation ="False"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Maklumat Permohonan Bajet</h1>
    <link rel="stylesheet" type="text/css" href="../../../Content/Site.css">


<style>


/*.table-borderless > tbody > tr > td,
.table-borderless > tbody > tr > th,
.table-borderless > tfoot > tr > td,
.table-borderless > tfoot > tr > th,
.table-borderless > thead > tr > td,
.table-borderless > thead > tr > th {
    border: none;
}*/

    .auto-style1 {
        width: 15%;
        height: 27px;
    }
    .auto-style2 {
        height: 27px;
    }

</style>

<script type="text/javascript">
    //Sys.Application.add_load(BindEvents);
    //function BindEvents() {
    //    $('[data-toggle="popover"]').popover();
    //}


    function pageLoad() {
        $('[data-toggle="popover"]').popover()
    }

    var kodvalue= <%=KodStatus%>;
    


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
    }

    $(function(){
        $('#btnPemohon').tooltip();
        $('#btnPenyokong').tooltip();
        $('#btnPengesah').tooltip();

        if (kodvalue == "02"){
            $('#btnStep1').addClass('btn-success').removeClass('btn-default');
        }
        if (kodvalue == "03"){
            $('#btnStep1').addClass('btn-success').removeClass('btn-default');
            $('#btnStep2').addClass('btn-success').removeClass('btn-default');
        }
        if (kodvalue == "04"){
            $('#btnStep1').addClass('btn-success').removeClass('btn-default');
            $('#btnStep2').addClass('btn-danger').removeClass('btn-default');
        }
        if (kodvalue == "05"){
            $('#btnStep1').addClass('btn-success').removeClass('btn-default');
            $('#btnStep2').addClass('btn-success').removeClass('btn-default');
            $('#btnStep3').addClass('btn-success').removeClass('btn-default');
        }
        if (kodvalue == "06"){
            $('#btnStep1').addClass('btn-success').removeClass('btn-default');
            $('#btnStep2').addClass('btn-success').removeClass('btn-default');
            $('#btnStep3').addClass('btn-danger').removeClass('btn-default');
        }
        if (kodvalue == "07"){
            $('#btnStep1').addClass('btn-success').removeClass('btn-default');
            $('#btnStep2').addClass('btn-success').removeClass('btn-default');
            $('#btnStep3').addClass('btn-success').removeClass('btn-default');
            $('#btnStep4').addClass('btn-success').removeClass('btn-default');
        }
        if (kodvalue == "08"){
            $('#btnStep1').addClass('btn-success').removeClass('btn-default');
            $('#btnStep2').addClass('btn-success').removeClass('btn-default');
            $('#btnStep3').addClass('btn-success').removeClass('btn-default');
            $('#btnStep4').addClass('btn-danger').removeClass('btn-default');
        }
    });
</script>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">        
	<ContentTemplate>
 <div class="container-fluid">
             <div class="row">
            <div class="col-sm-12 col-md-8 col-lg-8">
        <div>
            <asp:LinkButton ID="lbtnKembali" runat="server" CssClass="btn btn-info">
                <i class="fa fa-arrow-left fa-lg"></i>&nbsp;&nbsp;&nbsp;Kembali
            </asp:LinkButton>
        </div>
        </div></div>

        <div class="row">
        <div class="panel panel-default" style="overflow-x: auto;">
        <div class="panel-group" style="width :100%;">
         <div class="panel panel-default" style="width :99%;">
          <%--<div class="panel-heading clearfix">
            <h4 class="panel-title pull-left">Permohonan</h4>
            <div class="pull-right">
                <asp:LinkButton ID="lbtnKembali" runat="server" CssClass="btn-circle" ToolTip="Kembali">
                    <i class="fa fa-times-circle-o fa-lg"></i>
                </asp:LinkButton>
              </div>
          </div>--%>
        <div class="panel-heading">
            <h4 class="panel-title">Permohonan</h4>
           
          </div>
            <div id="PanelMohonBaru" class="panel-collapse">
            <div class="panel-body">
            <table style="width:100%" class="table table table-borderless">
                <tr>
                    <td style="width: 15%;"><Label class="control-label" for="">No Mohon</Label></td>
                    <td>
                        <asp:TextBox ID="txtNoMohon" runat="server" Width="150px" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        &nbsp &nbsp &nbsp &nbsp
                        <Label class="control-label" for="">Status</Label>
                        &nbsp &nbsp
                        <asp:TextBox ID="txtStatus" runat="server" Width="40%" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 15%;"><Label class="control-label" for="">Tarikh</Label></td>
                    <td>
                        <asp:TextBox ID="txtTarikh" runat="server" CssClass="form-control leftAlign" Width="80px" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1"><Label class="control-label" for="">PTj</Label></td>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtPtj" runat="server" CssClass="form-control" Width="80%" ReadOnly="true"></asp:TextBox>
                        <asp:Label ID="lblPtj" runat="server" visible="false" ></asp:Label>
                    </td>
                </tr>
                <tr id="trBahagian" runat="server">
                    <td style="width: 15%;"><Label class="control-label" for="">Bahagian</Label></td>
                    <td>
                        <asp:TextBox ID="txtBahagian" runat="server" CssClass="form-control" Width="80%" ReadOnly="true"></asp:TextBox>
                        <asp:Label ID="lblBahagian" runat="server" visible="false" ></asp:Label>
                        <%--<asp:DropDownList ID="ddlBahagian" runat="server" CssClass="form-control" Width="80%" AutoPostBack="true"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvddlBahagian" runat="server" ControlToValidate="ddlBahagian" ErrorMessage="" ForeColor="#820303" Text="*Sila Pilih" ValidationGroup="btnSimpan" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr id="trUnit" runat="server">
                    <td style="width: 15%;"><Label class="control-label" for="">Unit</Label></td>
                    <td>
                        <asp:TextBox ID="txtUnit" runat="server" CssClass="form-control" Width="80%" ReadOnly="true"></asp:TextBox>
                        <asp:Label ID="lblUnit" runat="server" visible="false" ></asp:Label>
                        <%--<asp:DropDownList ID="ddlUnit" runat="server" CssClass="form-control" Width="80%" ></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvddlUnit" runat="server" ControlToValidate="ddlUnit" ErrorMessage="" ForeColor="#820303" Text="*Sila Pilih" ValidationGroup="btnSimpan" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td style="width: 15%;"><Label class="control-label" for="">Tahun Bajet</Label></td>
                    <td>
                        <asp:TextBox ID="txtTahunBajet" runat="server" CssClass="form-control centerAlign" Width="50px" ReadOnly="true"></asp:TextBox>
                       
                    </td>
                </tr>
                <tr>
                    <td style="width: 15%;">
                        <Label class="control-label" for="">Jenis Dasar</Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlJenDasar" runat="server" CssClass="form-control" Width="20%">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlJenDasar" CssClass="fontValidatation" Display="Dynamic" ErrorMessage="" ForeColor="#820303" Text="*Sila Pilih" ValidationGroup="btnSimpan"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 15%;">
                        <Label class="control-label" for="">Kumpulan Wang</Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlKW" runat="server" AutoPostBack="true" CssClass="form-control" Width="80%">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvddlKW" runat="server" ControlToValidate="ddlKW" CssClass="fontValidatation" Display="Dynamic" ErrorMessage="" ForeColor="#820303" Text="*Sila Pilih" ValidationGroup="btnSimpan"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 15%;">
                        K<Label class="control-label" for="">od Operasi</Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlKodOperasi" runat="server" CssClass="form-control" Width="50%">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvKodOperasi" runat="server" ControlToValidate="ddlKodOperasi" CssClass="fontValidatation" Display="Dynamic" ErrorMessage="" ForeColor="#820303" Text="*Sila Pilih" ValidationGroup="btnSimpan"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 15%;"><Label class="control-label" for="">Program / Aktiviti</Label></td>
                    <td>
                        <asp:TextBox ID="txtProgram" runat="server" textmode="multiline" CssClass="form-control" Width="100%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvProg" runat="server" ControlToValidate="txtProgram" ErrorMessage="" ForeColor="#820303" Text="*Sila Isi" ValidationGroup="btnSimpan" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 15%;"><Label class="control-label" for="">Justifikasi</Label></td>
                    <td>
                        <asp:TextBox ID="txtJust" runat="server" textmode="multiline" CssClass="form-control" Width="100%" Rows="5"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvJust" runat="server" ControlToValidate="txtJust" ErrorMessage="" ForeColor="#820303" Text="*Sila Isi" ValidationGroup="btnSimpan" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 15%;">Jumlah Permohonan (RM)</td>
                    <td>
                        <asp:TextBox ID="txtJumlahMohon" runat="server" CssClass="form-control rightAlign" ReadOnly="true" Width="100px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 15%;">Dokumen</td>
                    <td>
                        <asp:GridView ID="gvLampiran" runat="server" AutoGenerateColumns="false" BorderColor="#333333" BorderStyle="Solid" CssClass="table table-striped table-bordered table-hover" DataKeyNames="BG16_Id" EmptyDataText="No files uploaded" Font-Size="8pt" HeaderStyle-BackColor="#6699FF" ShowHeaderWhenEmpty="true" Width="80%">
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="centerAlign" HeaderText="Bil" ItemStyle-Width="2%" SortExpression="Bil">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="BG01_NoMohon" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoMohonLampiran" runat="server" text='<%#Eval("BG01_NoMohon")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="BG16_Id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIDLampiran" runat="server" text='<%#Eval("BG16_Id")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="BG16_NamaDok" HeaderText="File Name" ItemStyle-Width="70%" />
                                <asp:HyperLinkField DataNavigateUrlFields="BG16_NamaDok" DataNavigateUrlFormatString="~/Upload/Document/Bajet/Permohonan/{0}" DataTextField="BG16_Id" DataTextFormatString="&lt;img src='../../../Images/search.png' alt='View' /&gt;" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%" Target="_blank" />

                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
                
            <div class="panel panel-default" style="overflow-x:scroll ; width :95%;">
            <div class="panel-heading">Butiran Permohonan</div>
            <div class="panel-body">
            <table style="width:100%" class="table table table-borderless">
                <tr>
                    <td style="width: 20%;">Objek Am</td>
                    <td>
                        <asp:DropDownList ID="ddlVotA" runat="server" AutoPostBack="True" CssClass="form-control" Width="40%" >
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvVotSbg0" runat="server" ControlToValidate="ddlVotA" CssClass="fontValidatation" Display="Dynamic" ErrorMessage="" ForeColor="#820303" Text="*Sila isi" ValidationGroup="btnSaveButiran"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                 <tr>
                     <td style="width: 20%;">
                         O<Label class="control-label" for="">bjek Sebagai</Label>
                     </td>
                     <td>
                         <asp:DropDownList ID="ddlVotSbg" runat="server" CssClass="form-control" Width="60%">
                         </asp:DropDownList>
                         <asp:RequiredFieldValidator ID="rfvVotSbg" runat="server" ControlToValidate="ddlVotSbg" CssClass="fontValidatation" Display="Dynamic" ErrorMessage="" ForeColor="#820303" Text="*Sila isi" ValidationGroup="btnSaveButiran"></asp:RequiredFieldValidator>
                     </td>
                </tr>
                <tr>
                    <td style="width: 20%;">Buitran</td>
                    <td>
                        <asp:TextBox ID="txtButiranDt" runat="server" CssClass="form-control" Rows="5" textmode="multiline" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%;"><Label class="control-label" for="">Jumlah Permohonan (RM)</Label></td>
                    <td>
                        <asp:TextBox ID="txtAngJum" name="txtAngJum" runat="server" CssClass="form-control rightAlign" onkeypress="return isNumberKey(event,this)" Width="100px" ></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="tfvAngJum" runat="server" ControlToValidate="txtAngJum" ErrorMessage="" ForeColor="#820303" Text="*Sila isi" ValidationGroup="btnSaveButiran" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>--%>
                        <asp:RequiredFieldValidator ID="rfvKuantiti0" runat="server" ControlToValidate="txtAngJum" CssClass="fontValidatation" Display="Dynamic" ErrorMessage="" ForeColor="#820303" Text="*Sila isi" ValidationGroup="btnSaveButiran"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    
                    <td style="height:40px; text-align:center;" colspan="2" >
                         <asp:Label ID="lblIDDt_" runat="server" visible="false"></asp:Label>
                    <asp:LinkButton ID="lbtnReset" runat="server" CssClass="btn btn-info" ToolTip="Kosongkan Butiran Perbelanjaan">
                        <i class="fa fa-refresh fa-lg"></i>&nbsp;&nbsp;&nbsp;Reset
                    </asp:LinkButton> 
                    &nbsp;&nbsp;
                    <asp:LinkButton ID="lbtnKemaskini" runat="server" CssClass="btn btn-info" ValidationGroup="btnSaveButiran" ToolTip="Kemaskini" visible="false">
                        <i class="fa fa-plus-circle fa-lg"></i>&nbsp;&nbsp;&nbsp;Kemaskini
                    </asp:LinkButton>
                    &nbsp;&nbsp;
                    <asp:LinkButton ID="lbtnSaveButiran" runat="server" CssClass="btn btn-info" ValidationGroup="btnSaveButiran" ToolTip="Tambah ke senarai">
                        <i class="fa fa-plus-circle fa-lg"></i>&nbsp;&nbsp;&nbsp;Tambah
                    </asp:LinkButton>
                 
                    </td>
                </tr>
            </table>
            <div>
            </div>
            <asp:GridView ID="gvButiran" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="True" EmptyDataText=""
                cssclass="table table-striped table-bordered table-hover" Width="100%" Font-Size="8pt" ShowFooter="True"  
                 AllowSorting="True" AllowPaging="True" PageSize="5" DataKeyNames="NoButiran" OnRowCommand="gvButiran_RowCommand">
                 <columns>
                    <asp:TemplateField  HeaderText = "Bil" ItemStyle-Width="2%" SortExpression="Bil" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField Visible="False">
                         <ItemTemplate>
                             <asp:Label ID="lblIDDt" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField Visible="false">
                         <ItemTemplate>
                             <asp:Label ID="lblNoButiran" runat="server" Text='<%# Eval("NoButiran")%>'></asp:Label>
                         </ItemTemplate>
                      </asp:TemplateField>                 
                    <asp:TemplateField HeaderText="Objek Am" ItemStyle-Width="15%" SortExpression="KodVotA" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:label id="StrKodVotA" runat="server" Text='<%# Eval("KodVotA") %>'></asp:label>
                        </ItemTemplate>                        
                        <%--<EditItemTemplate>
                            <asp:DropDownList ID="ddlKodVot" runat="server" CssClass="form-control" Width="300px"></asp:DropDownList>
                        </EditItemTemplate>--%>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Objek Sebagai" ItemStyle-Width="15%" SortExpression="KodVotSbg" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:label id="StrKodVotSbg" runat="server" Text='<%# Eval("KodVotSbg") %>'></asp:label>
                        </ItemTemplate>                        
                        <%--<EditItemTemplate>
                            <asp:DropDownList ID="ddlKodVot" runat="server" CssClass="form-control" Width="300px"></asp:DropDownList>
                        </EditItemTemplate>--%>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Butiran" ItemStyle-Width="35%" SortExpression="Butiran" HeaderStyle-CssClass="centerLeft" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:label id="strButiran" runat="server" Text='<%# Eval("Butiran") %>'></asp:label>
                        </ItemTemplate>                        
                        <%--<EditItemTemplate>
                            <asp:DropDownList ID="ddlKodVot" runat="server" CssClass="form-control" Width="300px"></asp:DropDownList>
                        </EditItemTemplate>--%>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jumlah Permohonan (RM)" ItemStyle-Width="20%" SortExpression="AngHrgUnit" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="centerAlign">
                        <ItemTemplate>
                            <asp:Label id="lblAngHrgUnit" runat ="server" text='<%# Eval("AngHrgUnit", "{0:N}")%>' ></asp:Label>
                            <%--<%# Eval("AngHrgUnit") %>--%>
                        </ItemTemplate>
                        <%--<EditItemTemplate>
                            <asp:TextBox ID="txtAngHrgUnit" runat="server" CssClass="form-control rightAlign" Width="100%" Text='<%# Eval("AngHrgUnit") %>'></asp:TextBox>
                        </EditItemTemplate>--%>
                    </asp:TemplateField> 
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CssClass="btn-xs" ToolTip="Pilih Kemaskini">
					<i class="far fa-edit fa-lg"></i>
                            </asp:LinkButton>

                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="Padam" CssClass="btn-xs" ToolTip="Padam"
                                OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');">
				<i class="far fa-trash-alt fa-lg"></i>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                </columns>
                    <HeaderStyle BackColor="#996633" />
            </asp:GridView>
            </div>
            </div>

           
                <br />
                <div class="row">
                    <div style="text-align:center">                    
                    <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn btn-info" ValidationGroup="btnSimpan">
                        <i class="fa fa-floppy-o fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                    </asp:LinkButton>

                    &nbsp;&nbsp;
                    <asp:LinkButton ID="lbtnHapus" runat="server" CssClass="btn btn-info" OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');" Visible="false">
                        <i class="fa fa-trash-o fa-lg"></i>&nbsp;&nbsp;&nbsp;Hapus
                    </asp:LinkButton>
                    
                    <%--<asp:LinkButton ID="lbtnHantar" runat="server" CssClass="btn btn-info pull-right">
                        <i class="fa fa-paper-plane-o fa-lg"></i>&nbsp;&nbsp;&nbsp;Hantar
                    </asp:LinkButton>--%> 
                    </div>
                
                </div>
                
            </div>
            </div>
                
            </div>
            </div>
            <p></p>
            <div class="panel-group">
                <div class="panel panel-default">
                  <div class="panel-heading">
                    <h4 class="panel-title">
                      <a data-toggle="collapse" href="#collapse1"><span class="fa fa-users fa-lg"></span>&nbsp&nbsp Pengguna</a>
                    </h4>
                  </div>
                  <div id="collapse1" class="panel-collapse collapse">
                    <div class="panel-body">
                        <div class="row">                    
                    <table style="width:100%;">
                        <tr>
                            <td style="width: 5%;" >
                                <button type="button" id="btnPemohon" class="btn btn-default btn-circle btn-lg" data-toggle="collapse" data-target="#Pemohon" title="Pemohon"><span class="fa fa-user"></span> </button>
                            </td>
                            <td style="width: 80%;">
                                <div id="Pemohon" class="collapse in">
                                &nbsp&nbsp Pemohon <br />
                                &nbsp&nbsp<asp:TextBox ID="txtNamaPemohon" runat="server" CssClass="form-control" ReadOnly="true" Width="45%" ToolTip="Nama Pemohon"></asp:TextBox>
                                &nbsp<asp:TextBox ID="txtJawPemohon" runat="server" CssClass="form-control" ReadOnly="true" Width="50%" ToolTip="Jawatan Pemohon"></asp:TextBox>
                                </div>
                            </td>                        
                        </tr>
                    </table>                        
                </div>
                <div class="row">                    
                    <table style="width:100%;">
                        <tr>
                            <td style="width: 5%;" >
                                <button type="button" id="btnPenyokong" class="btn btn-default btn-circle btn-lg" data-toggle="collapse" data-target="#Penyokong" title="Penyokong"><span class="fa fa-user"></span> </button>
                            </td>
                            <td style="width: 80%;">
                                <div id="Penyokong" class="collapse in">
                                &nbsp&nbsp Penyokong <br />
                                &nbsp&nbsp<asp:TextBox ID="txtNamaPenyokong" runat="server" CssClass="form-control" ReadOnly="true" Width="45%" ToolTip="Nama Penyokong"></asp:TextBox>
                                &nbsp<asp:TextBox ID="txtJawPenyokong" runat="server" CssClass="form-control" ReadOnly="true" Width="50%" ToolTip="Jawatan Penyokong"></asp:TextBox>
                                </div>
                            </td>                        
                        </tr>
                    </table>                        
                </div>
                <div class="row">                    
                    <table style="width:100%;">
                        <tr>
                            <td style="width: 5%;" >
                                <button type="button" id="btnPengesah" class="btn btn-default btn-circle btn-lg" data-toggle="collapse" data-target="#Pengesah" title="Pengesah"><span class="fa fa-user"></span> </button>
                            </td>
                            <td style="width: 80%;">
                                <div id="Pengesah" class="collapse in">
                                &nbsp&nbsp Pengesah <br />
                                &nbsp&nbsp<asp:TextBox ID="txtNamaPengesah" runat="server" CssClass="form-control" ReadOnly="true" Width="45%" ToolTip="Nama Pengesah"></asp:TextBox>
                                &nbsp<asp:TextBox ID="txtJawPengesah" runat="server" CssClass="form-control" ReadOnly="true" Width="50%" ToolTip="Jawatan Pengesah"></asp:TextBox>
                                </div>
                            </td>                        
                        </tr>
                    </table>                        
                </div>

                    </div>
                    
                  </div>
                </div>
              </div>

            
            </div>
            
            
                    
                </div>
        </div>
        

        <%--<div class="row">
            <div class="col-md-4 col-lg-4">
                <div class="panel panel-default">
                <div class="panel-heading">
                    <h5 class="panel-title">Pemohon</h5>
                </div>
                <div class="panel-body">
                    <table style="width:100%;" class="table table-borderless">
                    <tr>
                        <td style="width: 20%;"><Label class="control-label" for="">Nama </Label></td>
                        <td>
                            &nbsp&nbsp<asp:TextBox ID="txtNamaPemohon1" runat="server" Width="95%" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </td>    
                    </tr>
                    <tr>
                        <td style="width: 15%;"><Label class="control-label" for="">Jawatan </Label></td>
                        <td>
                            &nbsp&nbsp<asp:TextBox ID="txtJawPemohon1" runat="server" Width="95%" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </td>  
                    </tr>
                    </table>               
                </div></div></div>
            <div class="col-md-4 col-lg-4">
                <div class="panel panel-default">
                <div class="panel-heading">
                    <h5 class="panel-title">Penyokong</h5>
                </div>
                <div class="panel-body">
                    <table style="width:100%" class="table table table-borderless">
                    <tr>
                        <td style="width: 20%;"><Label class="control-label" for="">Nama </Label></td>
                        <td>
                            &nbsp&nbsp<asp:TextBox ID="txtNamaPenyokong" runat="server" Width="95%" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </td>    
                    </tr>
                    <tr>
                        <td style="width: 15%;"><Label class="control-label" for="">Jawatan </Label></td>
                        <td>
                            &nbsp&nbsp<asp:TextBox ID="txtJawPenyokong" runat="server" Width="95%" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </td>  
                    </tr>
                    </table>                  
                </div></div></div>
            <div class="col-md-4 col-lg-4">
                <div class="panel panel-default">
                <div class="panel-heading">
                    <h5 class="panel-title">Pengesah</h5>
                </div>
                <div class="panel-body">
                    <table style="width:100%" class="table table table-borderless">
                    <tr>
                        <td style="width: 20%;"><Label class="control-label" for="">Nama </Label></td>
                        <td>
                            &nbsp&nbsp<asp:TextBox ID="txtNamaPengesah" runat="server" Width="95%" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </td>    
                    </tr>
                    <tr>
                        <td style="width: 15%;"><Label class="control-label" for="">Jawatan </Label></td>
                        <td>
                            &nbsp&nbsp<asp:TextBox ID="txtJawPengesah" runat="server" Width="95%" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </td>  
                    </tr>
                    </table>                    
                </div>
            </div></div>            
            </div>--%>
    </div>
        <asp:Button ID = "btnOpen" runat = "server" Text = ""  style="display:none" />
        <ajaxToolkit:ModalPopupExtender ID="mpeButiranMohon" runat="server" BackgroundCssClass="modalBackground" 
            CancelControlID="lbNo" PopupControlID="pnlpopupHantar" TargetControlID="btnOpen" 
            > </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlpopupHantar" runat="server" BackColor="White" Width="70%" Style="display: none">
               <table style="border: Solid 3px #D5AA00; width: 100%; height: 100%; " >
                    <tr style="background: linear-gradient(to bottom, #fceabb 0%,#fccd4d 50%,#f8b500 51%,#fbdf93 100%);">
                        <td style="height: 10%;  font-weight: bold; text-align:center;" colspan="2" >
                            Maklumat Lanjut
                        </td>
                        
                    </tr>
                    <tr style="vertical-align:top;">
                        <td colspan="2">
                           <div style="margin-bottom:10px;margin-top:10px;margin-left:10px;margin-right:10px;">
                              <asp:GridView ID="gvHantar" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="False" AllowPaging="false" EmptyDataText=" "
                cssclass="table table-striped table-bordered table-hover" Width="100%" HeaderStyle-BackColor="#6699FF" Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" ShowFooter="false">
                    <columns>
                    
                    <asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" SortExpression="Bil" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="No Permohonan" DataField="NoMohon" SortExpression="NoPermohonan" ReadOnly="true" HeaderStyle-CssClass="centerAlign">
                        <ItemStyle Width="5%" HorizontalAlign="Center"/>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Program/ Aktiviti" DataField="Program" SortExpression="Program" ReadOnly="true" HeaderStyle-CssClass="centerAlign">
                        <ItemStyle Width="30%" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Tarikh Mohon" DataField="Tarikh" SortExpression="TarikhMohon" HeaderStyle-CssClass="centerAlign">
                        <ItemStyle Width="5%" HorizontalAlign="Center"/>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Anggaran Perbelanjaan (RM)" DataField="Jumlah" SortExpression="AngJumBesar" HeaderStyle-CssClass="centerAlign">
                        <ItemStyle Width="10%" HorizontalAlign="Right"/>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Status" DataField="Status" SortExpression="Status" HeaderStyle-CssClass="centerAlign">
                        <ItemStyle Width="20%" />
                    </asp:BoundField>                    
                </columns>
            </asp:GridView>
                          </div>
                        </td>
                    </tr>
                   <tr style="vertical-align:top;">
                        <td style="height: 10%;  font-weight: bold; text-align:center;" colspan="2" >
                            <div style="margin-bottom:10px;margin-top:10px;margin-left:10px;margin-right:10px;">
                            Adakah anda setuju menghantar permohonan bajet seperti di atas?
                            </div>
                        </td>                        
                    </tr>
                   <tr>
                       <td style="height: 10%; text-align:center;" colspan="2" >
                           <div style="margin-bottom:10px;margin-top:10px;margin-left:10px;margin-right:10px;">
                            <asp:LinkButton ID="lbYes" runat="server" CssClass="btn btn-info">
                                <i class="fa fa-check fa-lg"></i>&nbsp;&nbsp;&nbsp;Ya
                            </asp:LinkButton>
                            &nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="lbNo" runat="server" CssClass="btn btn-info">
                                <i class="fa fa-times fa-lg"></i>&nbsp;&nbsp;&nbsp;Tidak
                            </asp:LinkButton>
                               </div>
                        </td>
                   </tr>
                </table>                
            </asp:Panel>
        </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    
         
</asp:Content>
