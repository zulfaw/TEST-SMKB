<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="MaklumatPenyediaanBajetPTJ.aspx.vb" Inherits="SMKB_Web_Portal.MaklumatPenyediaanBajetPTJ" EnableEventValidation ="False"  %>

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

        <div class="panel-group"  style="width :100%;">
        <div class="panel panel-default"  style="width :99%;">
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
            <div class="panel-body" style="width :100%;">
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
                        <asp:TextBox ID="txtTarikh" runat="server" CssClass="form-control centerAlign" Width="80px" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1"><Label class="control-label" for="">PTj</Label></td>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtPtj" runat="server" CssClass="form-control" Width="80%" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr id="trBahagian" runat="server">
                    <td style="width: 15%;"><Label class="control-label" for="">Bahagian</Label></td>
                    <td>
                        <asp:TextBox ID="txtBahagian" runat="server" CssClass="form-control" Width="80%" ReadOnly="true"></asp:TextBox>
                        <%--<asp:DropDownList ID="ddlBahagian" runat="server" CssClass="form-control" Width="80%" AutoPostBack="true"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvddlBahagian" runat="server" ControlToValidate="ddlBahagian" ErrorMessage="" ForeColor="#820303" Text="*Sila Pilih" ValidationGroup="btnSimpan" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr id="trUnit" runat="server">
                    <td style="width: 15%;"><Label class="control-label" for="">Unit</Label></td>
                    <td>
                        <asp:TextBox ID="txtUnit" runat="server" CssClass="form-control" Width="80%" ReadOnly="true"></asp:TextBox>
                        <%--<asp:DropDownList ID="ddlUnit" runat="server" CssClass="form-control" Width="80%" ></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvddlUnit" runat="server" ControlToValidate="ddlUnit" ErrorMessage="" ForeColor="#820303" Text="*Sila Pilih" ValidationGroup="btnSimpan" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td style="width: 15%;"><Label class="control-label" for="">Jenis Dasar</Label></td>
                    <td>
                        <asp:DropDownList ID="ddlJenDasar" runat="server" CssClass="form-control" Width="50%"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlJenDasar" ErrorMessage="" ForeColor="#820303" Text="*Sila Pilih" ValidationGroup="btnSimpan" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 15%;"><Label class="control-label" for="">Tahun Bajet</Label></td>
                    <td>
                        <asp:TextBox ID="txtTahunBajet" runat="server" CssClass="form-control centerAlign" Width="40px" ReadOnly="true"></asp:TextBox>
                       
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
                        <Label class="control-label" for="">Operasi</Label>
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
                    <td style="width: 15%;">Lampiran</td>
                    <td>                                 
                        <asp:FileUpload ID="fuLampiran" runat="server" Width="50%" CssClass="form-control" Height="25px" BackColor="#FFFFCC" EnableViewState="true" />
                    </td>
                </tr>
            </table>
            <br />
                
            <div class="panel panel-default" style="overflow-x:scroll">
            <div class="panel-heading">Butiran Perbelanjaan</div>
            <div class="panel-body">
            <table style="width:100%" class="table table table-borderless">
                <tr>
                    <td style="width: 20%;">Objek Am</td>
                    <td>
                        <asp:DropDownList ID="ddlVotSbgA" runat="server" CssClass="form-control" Width="70%">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvVotSbg0" runat="server" ControlToValidate="ddlVotSbgA" CssClass="fontValidatation" Display="Dynamic" ErrorMessage="" ForeColor="#820303" Text="*Sila isi" ValidationGroup="btnSaveButiran"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                 <tr>
                     <td style="width: 20%;">
                         O<Label class="control-label" for="">bjek Sebagai</Label>
                     </td>
                     <td>
                         <asp:DropDownList ID="ddlVotSbg" runat="server" CssClass="form-control" Width="70%">
                         </asp:DropDownList>
                         <asp:RequiredFieldValidator ID="rfvVotSbg" runat="server" ControlToValidate="ddlVotSbg" CssClass="fontValidatation" Display="Dynamic" ErrorMessage="" ForeColor="#820303" Text="*Sila isi" ValidationGroup="btnSaveButiran"></asp:RequiredFieldValidator>
                     </td>
                </tr>
                <tr>
                    <td style="width: 20%;"><Label class="control-label" for="">Jumlah Perbelanjaan (RM)</Label></td>
                    <td>
                        <asp:TextBox ID="txtAngJum" runat="server" CssClass="form-control rightAlign" Width="100px" ></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="tfvAngJum" runat="server" ControlToValidate="txtAngJum" ErrorMessage="" ForeColor="#820303" Text="*Sila isi" ValidationGroup="btnSaveButiran" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>--%>
                        <asp:RequiredFieldValidator ID="rfvKuantiti0" runat="server" ControlToValidate="txtAngJum" CssClass="fontValidatation" Display="Dynamic" ErrorMessage="" ForeColor="#820303" Text="*Sila isi" ValidationGroup="btnSaveButiran"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    
                    <td style="height:40px; text-align:center;" colspan="2" >
                    <asp:LinkButton ID="lbtnReset" runat="server" CssClass="btn btn-info" ToolTip="Kosongkan Butiran Perbelanjaan">
                        <i class="fa fa-refresh fa-lg"></i>&nbsp;&nbsp;&nbsp;Reset
                    </asp:LinkButton> 
                    &nbsp;&nbsp;
                    <asp:LinkButton ID="lbtnSaveButiran" runat="server" CssClass="btn btn-info" ValidationGroup="btnSaveButiran" ToolTip="Tambah ke senarai">
                        <i class="fa fa-plus-circle fa-lg"></i>&nbsp;&nbsp;&nbsp;Tambah
                    </asp:LinkButton>
                 
                    </td>
                </tr>
            </table>
            <div>
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
            </div>
            <asp:GridView ID="gvButiran" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="True" EmptyDataText=" "
                cssclass="table table-striped table-bordered table-hover" Width="100%" Font-Size="8pt" ShowFooter="True"
                AllowSorting="True" AllowPaging="True" PageSize="5">
                    <columns>
                    <asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" SortExpression="Bil" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>                    
                    <asp:TemplateField Visible=false>
                         <ItemTemplate>
                             <asp:Label id="lblNoButiran" runat ="server" text='<%# Eval("NoButiran")%>' ></asp:Label>
                         </ItemTemplate>
                      </asp:TemplateField>  
                       <asp:TemplateField HeaderText="Objek Am" ItemStyle-Width="3%" SortExpression="KodVot" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:label id="StrKodVot" runat="server" Text='<%# Eval("KodVot") %>'></asp:label>
                        </ItemTemplate>                        
                        <%--<EditItemTemplate>
                            <asp:DropDownList ID="ddlKodVot" runat="server" CssClass="form-control" Width="300px"></asp:DropDownList>
                        </EditItemTemplate>--%>
                    </asp:TemplateField>                  
                    <asp:TemplateField HeaderText="Objek Sebagai" ItemStyle-Width="3%" SortExpression="KodVot" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:label id="StrKodVot" runat="server" Text='<%# Eval("KodVot") %>'></asp:label>
                        </ItemTemplate>                        
                        <%--<EditItemTemplate>
                            <asp:DropDownList ID="ddlKodVot" runat="server" CssClass="form-control" Width="300px"></asp:DropDownList>
                        </EditItemTemplate>--%>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Butiran" ItemStyle-Width="50%" SortExpression="Butiran">
                        <ItemTemplate>
                            <asp:Label id="lblButiran" runat ="server" text='<%# Eval("Butiran")%>' ></asp:Label>                            
                        </ItemTemplate>
                        <%--<EditItemTemplate>
                            <asp:TextBox ID="txtButiran" runat="server" TextMode="MultiLine" CssClass="form-control" Width="100%" Text='<%# Eval("Butiran") %>'></asp:TextBox>
                        </EditItemTemplate>--%>
                    </asp:TemplateField> 

                    <asp:TemplateField HeaderText="Jumlah Perbelanjaan (RM)" ItemStyle-Width="10%" SortExpression="AngHrgUnit" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="centerAlign">
                        <ItemTemplate>
                            <asp:Label id="lblAngHrgUnit" runat ="server" text='<%# Eval("AngHrgUnit", "{0:N}")%>' ></asp:Label>
                            <%--<%# Eval("AngHrgUnit") %>--%>
                        </ItemTemplate>
                        <%--<EditItemTemplate>
                            <asp:TextBox ID="txtAngHrgUnit" runat="server" CssClass="form-control rightAlign" Width="100%" Text='<%# Eval("AngHrgUnit") %>'></asp:TextBox>
                        </EditItemTemplate>--%>
                    </asp:TemplateField>
                                                                           
                    <asp:TemplateField>
                        <%--<EditItemTemplate>
                            <asp:ImageButton ID="btnSave1" runat="server" CommandName="Update" Height="20px" ImageUrl="~/Images/Save_48x48.png" 
                                ToolTip="Simpan" Width="15px" OnItemCommand="gvButiranModul_ItemCommand" />
                                &nbsp;&nbsp;
                            <asp:ImageButton ID="btnCancel" runat="server" CommandName="Cancel" Height="20px" ImageUrl="~/Images/Cancel16x16.png" 
                                    ToolTip="Batal" Width="15px" OnItemCommand="gvButiranModul_ItemCommand" />
                                &nbsp;&nbsp;
                        </EditItemTemplate>--%>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CssClass="btn-xs" ToolTip="Pilih Kemaskini">
                                <i class="fa fa-hand-o-left fa-lg"></i>
                            </asp:LinkButton>
                            
                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CssClass="btn-xs" ToolTip="Padam"
                                OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');">
                                <i class="fa fa-trash-o fa-lg"></i>
                            </asp:LinkButton>
                            <%--<asp:ImageButton ID="btnEdit" runat="server" CommandName="Edit" Height="20px" ImageUrl="~/Images/Edit_48.png" 
                                ToolTip="Kemas Kini" Width="15px" OnItemCommand="gvButiranModul_ItemCommand" />
                            &nbsp;&nbsp;
				            <asp:ImageButton ID="btnDelete1" runat="server" CommandName="Delete"  ImageUrl="~/Images/Delete_32x32.png" 
					            ToolTip="Padam" Width="15px" Height="18px" OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');" />--%>
			            </ItemTemplate>
			            <ItemStyle Width="5%" HorizontalAlign="Center" />
			        </asp:TemplateField>
                </columns>
                    <HeaderStyle BackColor="#996633" />
            </asp:GridView>
            </div>
            </div>
                                <br />
                       <div class="panel panel-default" style="width:90%" >
                       <div ><h4 class="panel-title">Status:
                               <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                                   <asp:ListItem Value="1">Lengkap</asp:ListItem>
                                   <asp:ListItem Value="1">Tidak Lengkap</asp:ListItem>
                               </asp:RadioButtonList>
                               </h4>
                       </div>
                        <div class="panel-heading"><h4 class="panel-title">Ulasan</h4></div>
                        <div class="panel-body">
                            
                             <asp:TextBox ID="txtUlasan" runat="server" CssClass="form-control" Rows="3" Width="90%" Height="53px"></asp:TextBox>
                        </div>
            </div>
           
                <br />
                <div class="row">
                    <div style="text-align:center">                    
                    
                    &nbsp;&nbsp;
                    <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn btn-info" ValidationGroup="btnSimpan">
                        <i class="fa fa-floppy-o fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                    </asp:LinkButton>
                    &nbsp;&nbsp;
                                        
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
