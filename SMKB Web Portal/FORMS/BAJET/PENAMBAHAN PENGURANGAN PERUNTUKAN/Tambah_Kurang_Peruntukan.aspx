<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Tambah_Kurang_Peruntukan.aspx.vb" Inherits="SMKB_Web_Portal.Penambahan" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>

    <style>
        .panel
        {
            width:80%;
        }

        @media (max-width: 1600px) {
            .panel {
                width: 95%;
            }
        }

        .tooltip{
            width:50px;
        }
    </style>

    <script> 
     var counter = 1;
     function AddFileUpload() {
         var div = document.createElement('DIV');
         div.innerHTML = '<input id="fu' + counter + '" name = "file' + counter +
                     '" type="file" style="width:650px;" />' +
                     '<button type="button" class="btn " onclick="RemoveFileUpload(this)" style="width:40px;height:20px;padding-bottom: 18px;" title="Padam"><span style=color:red;><i class="fas fa-times fa-lg"></i></span></button>';
                
         div.style.marginTop = "15px";
         document.getElementById("FileUploadContainer").appendChild(div);
         counter++;
     }
     function RemoveFileUpload(div) {
         document.getElementById("FileUploadContainer").removeChild(div.parentNode);
     }
    
    </script>

    <script type="text/javascript">
        function fConfirmDel() {

            try {         
                if (confirm('Anda pasti untuk padam maklumat ini?')) {
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
     
   </script>



<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <form role="form">
        <div id="divList" runat="server">

            <div style="margin: 20px; text-align: left;">
                <asp:LinkButton ID="lbtnMohonbaru" runat="server" CssClass="btn" Width="140px">
						<i class="fas fa-file-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Permohonan Baru
                </asp:LinkButton>
            </div>

            <hr />

            <div class="panel panel-default">
                <div class="panel-heading">
                    Senarai Permohonan Tambah/Kurang
                </div>
                <div class="panel-body">
                    <div class="row">

                        <div class="GvTopPanel">
                            <div style="float: left; margin-top: 8px; margin-left: 10px;">
                                <label class="control-label" for="">Jumlah rekod :</label>&nbsp;<label class="control-label" id="lblJumRekod" runat="server" style="color: mediumblue;"></label>
                            </div>
                            <div class="panel-title pull-right" style="margin-top: 5px; margin-right: 5px;">
                                <i class="fas fa-info-circle fa-lg" aria-hidden="true" data-html="true" data-toggle="popover" style="cursor: pointer; color: #ba2818;" data-placement="top"
                                    title="Petunjuk" data-content="<span style='color:#0055FF;'><i class='fas fa-arrow-circle-up fa-lg'></i> </span> - Penambahan Peruntukan   <br/><span style='color:#D50000;'><i class='fas fa-arrow-circle-down fa-lg'></i> </span> - Pengurangan Peruntukan"></i>
                            </div>
                        </div>
                        <asp:GridView ID="gvSenarai" runat="server" AllowSorting="true" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText=" "
                            CssClass="table table-striped table-bordered table-hover" Width="100%" Height="100%" HeaderStyle-BackColor="#6699FF" Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" ShowFooter="True">
                            <Columns>

                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnTB" runat="server">
      <a href="#" data-toggle="tooltip" title="Penambahan" style="cursor:default;">
          <span style="color:#0055FF;"><i class="fas fa-arrow-circle-up fa-lg"></i> </span></a>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btnKG" runat="server">
      <a href="#" data-toggle="tooltip" title="Pengurangan" style="cursor:default;"> 
          <span style="color:#D50000;"><i class="fas fa-arrow-circle-down fa-lg"></i> </span></a>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Bil">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBil" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Width="3%" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lblKw" runat="server" Text="KW" />&nbsp;
                            <asp:LinkButton ID="lnkKw" runat="server" CommandName="Sort" CommandArgument="kodKw"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblKw" runat="server" Text='<%# Eval("KodKw")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lblKo" runat="server" Text="KO" />&nbsp;
                            <asp:LinkButton ID="lnkKo" runat="server" CommandName="Sort" CommandArgument="kodKo"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblKo" runat="server" Text='<%# Eval("KodKo")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lblPTj" runat="server" Text="PTJ" />&nbsp;
                            <asp:LinkButton ID="lnkPTj" runat="server" CommandName="Sort" CommandArgument="PTJ"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPTj" runat="server" Text='<%# Eval("PTJ")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lblKp" runat="server" Text="KP" />&nbsp;
                            <asp:LinkButton ID="lnkKp" runat="server" CommandName="Sort" CommandArgument="KodKP"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblKp" runat="server" Text='<%# Eval("KodKP")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lblObjSbg" runat="server" Text="Objek Sebagai" />&nbsp;
                            <asp:LinkButton ID="lnkObjSbg" runat="server" CommandName="Sort" CommandArgument="ObjSbg"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblObjSbg" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("VotSebagai"))) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lblAmaun" runat="server" Text="Amaun (RM)" />&nbsp;
                            <asp:LinkButton ID="lnkAmaun" runat="server" CommandName="Sort" CommandArgument="Amaun"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmaun" runat="server" Text='<%# Eval("bg04_Amaun")%>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblJenis" runat="server" Text='<%# Eval("KodAgih")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lblTkhMohon" runat="server" Text="Tarikh Mohon" />&nbsp;
                            <asp:LinkButton ID="lnkTkhMohon" runat="server" CommandName="Sort" CommandArgument="TkhMohon"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTkhMohon" runat="server" Text='<%# Eval("bg04_TkhAgih")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIndKW" runat="server" Text='<%# Eval("bg04_IndKw")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIndPTj" runat="server" Text='<%# Eval("bg05_IndPTJ")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIndObjAm" runat="server" Text='<%# Eval("bg06_IndObjAm")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIndObjSbg" runat="server" Text='<%# Eval("bg07_IndObjSbg")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblButiran" runat="server" Text='<%# Eval("bg04_Butiran")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="btn-xs" ToolTip="Pilih">
                                            <i class="fas fa-edit"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" />
                                </asp:TemplateField>

                                
                              
                            </Columns>

                            <HeaderStyle BackColor="#6699FF"></HeaderStyle>
                        </asp:GridView>

                    </div>
                </div>
            </div>

        </div>

        <div id="divDetail" runat="server" style="width: 100%;" visible="false">
            
            <div class="row">
                   <asp:LinkButton ID="lnkBtnBack" runat="server" CssClass="btn " Width="100px" ToolTip="">
						<i class="fas fa-long-arrow-alt-left  fa-lg"></i> &nbsp;&nbsp; Kembali
                   </asp:LinkButton>
               </div>

            <div class="row">
                  <div class="panel panel-default" style="width: 80%;">
                        <div class="panel-heading">Maklumat Tambah/Kurang</div>
                        <div class="panel-body">
                <table style="width: 100%;">

                    <tr style="height: 25px">
                        <td style="width: 150px; height: 22px;">&nbsp;</td>
                        <td>
                            <div class="panel panel-default" style="width: 350px; margin-left: 0;">
                                <div class="panel-body">
                                    <asp:RadioButtonList ID="rbMenu" runat="server" Height="25px" RepeatDirection="Horizontal" Width="273px" AutoPostBack="true">
                                        <asp:ListItem Selected="True" Text=" Penambahan" Value="0" />
                                        <asp:ListItem Text=" Pengurangan" Value="1" />
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 25px;">
                            <label class="control-label" for="">
                                Tahun :
                            </label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTahun" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 100px;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 25px;">
                            <label class="control-label" for="">
                                Kumpulan Wang :
                            </label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlKW" runat="server" AutoPostBack="True" CssClass="form-control" Style="width: 400px;">
                            </asp:DropDownList>
                            &nbsp;&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Pilih KW" ControlToValidate="ddlKW" CssClass="text-danger" ValidationGroup="grpSimpan" InitialValue="0"></asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td style="height: 25px;">
                            <label class="control-label" for="">
                                Kod Operasi :
                            </label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlKO" runat="server" AutoPostBack="True" CssClass="form-control" Style="width: 400px;">
                            </asp:DropDownList>
                            &nbsp;&nbsp;
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Pilih KO" ControlToValidate="ddlKO" CssClass="text-danger" ValidationGroup="grpSimpan" InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr style="height: 25px">
                        <td style="height: 25px;">
                            <label class="control-label" for="">
                                PTJ :
                            </label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPTJ" runat="server" AutoPostBack="True" CssClass="form-control" Style="width: 600px;">
                            </asp:DropDownList>
                            &nbsp;&nbsp;
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Pilih PTj" ControlToValidate="ddlPTJ" CssClass="text-danger" ValidationGroup="grpSimpan" InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr style="height: 25px">
                        <td style="height: 25px;">
                            <label class="control-label" for="">
                                Kod Projek :
                            </label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlKP" runat="server" AutoPostBack="True" CssClass="form-control" Style="width: 600px;">
                            </asp:DropDownList>
                            &nbsp;&nbsp;
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Pilih KP" ControlToValidate="ddlKP" CssClass="text-danger" ValidationGroup="grpSimpan" InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr style="height: 25px">
                        <td style="height: 25px;">
                            <label class="control-label" for="">
                                Objek Am :
                            </label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlObjAm" runat="server" AutoPostBack="True" CssClass="form-control" Style="width: 500px;">
                            </asp:DropDownList>
                            &nbsp;&nbsp;
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Pilih Objek Am" ControlToValidate="ddlObjAm" CssClass="text-danger" ValidationGroup="grpSimpan" InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr style="height: 25px">
                        <td style="height: 25px;">
                            <label class="control-label" for="">
                                Objek Sebagai :
                            </label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlObjSbg" runat="server" AutoPostBack="True" CssClass="form-control" Style="width: 500px;">
                            </asp:DropDownList>
                            &nbsp;&nbsp;
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Pilih Objek Sebagai" ControlToValidate="ddlObjSbg" CssClass="text-danger" ValidationGroup="grpSimpan" InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr style="height: 25px">
                        <td style="height: 25px;">
                            <label class="control-label" for="">
                                Baki Peruntukan (RM) :
                            </label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtBaki" runat="server" ReadOnly="True" CssClass="form-control rightAlign" Style="width: 100px;" BackColor="#FFFFCC"></asp:TextBox>


                        </td>
                    </tr>


                    <tr style="height: 25px">
                        <td style="height: 22px; vertical-align: top;">
                            <label class="control-label" for="">
                                Butiran :
                            </label>
                        </td>
                        <td style="vertical-align: top;">
                            <asp:TextBox ID="txtButiran" runat="server" CssClass="form-control" TextMode="multiline" Width="60%" Height="50px"></asp:TextBox>
                            &nbsp;&nbsp;
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Masukkan Butiran" ControlToValidate="txtButiran" CssClass="text-danger" ValidationGroup="grpSimpan"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr style="height: 25px">
                        <td style="height: 25px; vertical-align: top;">
                            <label class="control-label" for="">
                                Amaun (RM) :
                            </label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAmaun" runat="server" CssClass="form-control rightAlign" Width="100px" onkeypress="return isNumberKey(event,this)" onblur="if (this.dirty){this.onchange();}" AutoPostBack="true"></asp:TextBox>
                            &nbsp;&nbsp;
                        </td>
                    </tr>

                </table>
                            </div></div>
            </div>

            <div class="row" runat="server" id="divUpload1">
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

                        <div class="row" runat="server">
                            <asp:FileUpload ID="fu1" runat="server" Style="width: 650px;" />
                            <div id="FileUploadContainer"></div>
                            <div style="text-align: left; margin-top: 20px;">

                                <button type="button" class="btn " onclick="AddFileUpload()" style="width: 100px" title="Tambah fail">
                                    <i class="fas fa-plus fa-lg"></i>&nbsp;Tambah fail
                                </button>

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
                                    <asp:Label ID="lblId" runat="server" Text='<%# Eval("BG14_Id")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblIndKw" runat="server" Text='<%# Eval("BG04_IndKw")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nama Dokumen">
                                <ItemTemplate>
                                    <asp:Label ID="lblNamaDok" runat="server" Text='<%# Eval("bg14_NamaDok")%>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblDokPath" runat="server" Text='<%# Eval("BG14_Path")%>' />
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

                        </div>


                    </div>
                </div>
            </div>

            

            <div class="row">
                <div style="text-align: center; margin-bottom: 10px; margin-top: 20px;">

                    <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn " ValidationGroup="grpSimpan">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                    </asp:LinkButton>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               
                        <asp:LinkButton ID="lbtnHapus" runat="server" CssClass="btn " OnClientClick="return confirm('Anda pasti untuk hapus rekod ini?');">
						<i class="fas fa-trash-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Hapus
                        </asp:LinkButton>
                    &nbsp;&nbsp;
                                                                 
                        <asp:LinkButton ID="lbtnBaru" runat="server" CssClass="btn ">
						<i class="fas fa-file-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Rekod Baru
                        </asp:LinkButton>

                </div>
            </div>
            

            <asp:HiddenField ID="hidIndKW" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="hidIndPTj" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="hidIndObjAm" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="hidIndObjSbg" runat="server"></asp:HiddenField>

        </div>
        </form>
          
    </ContentTemplate>

    <Triggers>
            <asp:PostBackTrigger ControlID="lbtnSimpan" />

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

