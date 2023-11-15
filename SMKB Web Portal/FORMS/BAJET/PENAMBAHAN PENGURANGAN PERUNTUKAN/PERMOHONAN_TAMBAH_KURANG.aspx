<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="PERMOHONAN_TAMBAH_KURANG.aspx.vb" Inherits="SMKB_Web_Portal.PERMOHONAN_TAMBAH_KURANG" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
       <h1>PERMOHONAN TAMBAH/KURANG</h1>
    
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

            <form role="form">
            <div class="row">
                   <asp:LinkButton ID="lnkBtnBack" runat="server" CssClass="btn " Width="100px" ToolTip="">
						<i class="fas fa-long-arrow-alt-left  fa-lg"></i> &nbsp;&nbsp; Kembali
                   </asp:LinkButton>
               </div>

             <div class="row" style="margin-top: 50px;">
                 <div style="width: 80%;margin-left: 20px;">
                     <table style="width: 100%;">
                         <tr>
                             <td style="width: 100px;">
                                 Tahun</td>
                             <td style="height: 25px;">:</td>
                             <td>
                                 <asp:TextBox ID="txtTahun" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 100px;"></asp:TextBox>
                             </td>
                             <td>&nbsp;</td>
                             <td>&nbsp;</td>
                             <td>&nbsp;</td>
                         </tr>
                         <tr>
                             <td>No. Mohon</td>
                             <td>:</td>
                             <td>
                                 <asp:TextBox ID="txtNoMohon" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 200px;"></asp:TextBox>
                             </td>
                             <td>Status</td>
                             <td>:</td>
                             <td>
                                 <asp:TextBox ID="txtStatus" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 300px;"></asp:TextBox>
                             </td>
                         </tr>
                         <tr>
                             <td>Tarikh Mohon</td>
                             <td>:</td>
                             <td>
                                 <asp:TextBox ID="txtTkhMohon" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 200px;"></asp:TextBox>
                             </td>
                             <td>&nbsp;</td>
                             <td>&nbsp;</td>
                             <td>&nbsp;</td>
                         </tr>
                         <tr>
                             <td style="vertical-align:top;">Butiran</td>
                             <td style="vertical-align:top;">:</td>
                             <td>
                                 <asp:TextBox ID="txtButiran" runat="server" CssClass="form-control" Height="50px" TextMode="multiline" Width="60%"></asp:TextBox>
                                 &nbsp;&nbsp;&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtButiran" CssClass="text-danger" ErrorMessage="Masukkan Butiran" ValidationGroup="grpSimpan"></asp:RequiredFieldValidator>
                             </td>
                             <td>&nbsp;</td>
                             <td>&nbsp;</td>
                             <td>&nbsp;</td>
                         </tr>
                         <tr>
                             <td style="height: 25px;">Amaun</td>
                             <td style="height: 25px;">:</td>
                             <td>
                                 <asp:TextBox ID="txtAmaun" runat="server" AutoPostBack="true" CssClass="form-control rightAlign" onblur="if (this.dirty){this.onchange();}" onkeypress="return isNumberKey(event,this)" Width="150px"></asp:TextBox>

                                 &nbsp;&nbsp;&nbsp;&nbsp;
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtAmaun" CssClass="text-danger" ErrorMessage="Masukkan Amaun Tambah/Kurang" ValidationGroup="grpSimpan"></asp:RequiredFieldValidator>
                             </td>
                             <td>&nbsp;</td>
                             <td>&nbsp;</td>
                             <td>&nbsp;</td>
                         </tr>
                     </table>
                 </div>               
            </div>

            <div class="row">
                  <div class="panel panel-default" style="width: 70%;">
                        <div class="panel-heading">Daripada</div>
                        <div class="panel-body">
                <table style="width: 100%;">

                    <tr>
                        <td style="width : 150px;">
                            <label class="control-label" for="">
                                Kumpulan Wang
                            </label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlKWDpd" runat="server" AutoPostBack="True" CssClass="form-control">
                            </asp:DropDownList>
                            &nbsp;&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Pilih KW" ControlToValidate="ddlKWDpd" CssClass="text-danger" ValidationGroup="grpSimpan" InitialValue="0"></asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td style="height: 25px;">
                            <label class="control-label" for="">
                                Kod Operasi
                            </label>
                        </td>
                        <td style="height: 25px;">:</td>
                        <td>
                            <asp:DropDownList ID="ddlKODpd" runat="server" AutoPostBack="True" CssClass="form-control">
                                <asp:ListItem Value="0" Text="- SILA PILIH -" />
                            </asp:DropDownList>
                            &nbsp;&nbsp;
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Pilih KO" ControlToValidate="ddlKODpd" CssClass="text-danger" ValidationGroup="grpSimpan" InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr style="height: 25px">
                        <td style="height: 25px;">
                            <label class="control-label" for="">
                                PTJ
                            </label>
                        </td>
                        <td style="height: 25px;">:</td>
                        <td>
                            <asp:DropDownList ID="ddlPTJDpd" runat="server" AutoPostBack="True" CssClass="form-control">
                                <asp:ListItem Value="0" Text="- SILA PILIH -" />
                            </asp:DropDownList>
                            &nbsp;&nbsp;
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Pilih PTj" ControlToValidate="ddlPTJDpd" CssClass="text-danger" ValidationGroup="grpSimpan" InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr style="height: 25px">
                        <td style="height: 25px;">
                            <label class="control-label" for="">
                                Kod Projek
                            </label>
                        </td>
                        <td style="height: 25px;">:</td>
                        <td>
                            <asp:DropDownList ID="ddlKPDpd" runat="server" AutoPostBack="True" CssClass="form-control">
                                <asp:ListItem Value="0" Text="- SILA PILIH -" />
                            </asp:DropDownList>
                            &nbsp;&nbsp;
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Pilih KP" ControlToValidate="ddlKPDpd" CssClass="text-danger" ValidationGroup="grpSimpan" InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr style="height: 25px">
                        <td style="height: 25px;">
                            <label class="control-label" for="">
                                Objek Am
                            </label>
                        </td>
                        <td style="height: 25px;">:</td>
                        <td>
                            <asp:DropDownList ID="ddlObjAmDpd" runat="server" AutoPostBack="True" CssClass="form-control">
                                <asp:ListItem Value="0" Text="- SILA PILIH -" />
                            </asp:DropDownList>
                            &nbsp;&nbsp;
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Pilih Objek Am" ControlToValidate="ddlObjAmDpd" CssClass="text-danger" ValidationGroup="grpSimpan" InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr style="height: 25px">
                        <td style="height: 25px;">
                            <label class="control-label" for="">
                                Objek Sebagai
                            </label>
                        </td>
                        <td style="height: 25px;">:</td>
                        <td>
                            <asp:DropDownList ID="ddlObjSbgDpd" runat="server" AutoPostBack="True" CssClass="form-control">
                                <asp:ListItem Value="0" Text="- SILA PILIH -" />
                            </asp:DropDownList>
                            &nbsp;&nbsp;
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Pilih Objek Sebagai" ControlToValidate="ddlObjSbgDpd" CssClass="text-danger" ValidationGroup="grpSimpan" InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr style="height: 25px">
                        <td style="height: 25px;">
                            <label class="control-label" for="">
                                Baki Peruntukan (RM)
                            </label>
                        </td>
                        <td style="height: 25px;">:</td>
                        <td>
                            <asp:TextBox ID="txtBakiDpd" runat="server" ReadOnly="True" CssClass="form-control rightAlign" Style="width: 150px;" BackColor="#FFFFCC" Text=""></asp:TextBox>


                        </td>
                    </tr>


                    <tr style="height: 25px">
                        <td style="height: 25px;">Baki Selepas Tambah/Kurang (RM)</td>
                        <td style="height: 25px;">:</td>
                        <td>
                            <asp:TextBox ID="txtBakiSlpsDpd" runat="server" BackColor="#FFFFCC" CssClass="form-control rightAlign" ReadOnly="True" Style="width: 150px;" Text="0.00"></asp:TextBox>
                        </td>
                    </tr>


                </table>
                            </div></div>
            </div>

            <div class="row">
                  <div class="panel panel-default" style="width: 70%;">
                        <div class="panel-heading">Kepada</div>
                        <div class="panel-body">
                <table style="width: 100%;">

                    <tr>
                        <td style="width : 150px;">
                            <label class="control-label" for="">
                                Kumpulan Wang
                            </label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlKWKpd" runat="server" AutoPostBack="True" CssClass="form-control">
                            </asp:DropDownList>
                            &nbsp;&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Pilih KW" ControlToValidate="ddlKWKpd" CssClass="text-danger" ValidationGroup="grpSimpan" InitialValue="0"></asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td style="height: 25px;">
                            <label class="control-label" for="">
                                Kod Operasi
                            </label>
                        </td>
                        <td style="height: 25px;">:</td>
                        <td>
                            <asp:DropDownList ID="ddlKOKpd" runat="server" AutoPostBack="True" CssClass="form-control">
                                <asp:ListItem Value="0" Text="- SILA PILIH -" />
                            </asp:DropDownList>
                            &nbsp;&nbsp;
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Pilih KO" ControlToValidate="ddlKOKpd" CssClass="text-danger" ValidationGroup="grpSimpan" InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr style="height: 25px">
                        <td style="height: 25px;">
                            <label class="control-label" for="">
                                PTJ
                            </label>
                        </td>
                        <td style="height: 25px;">:</td>
                        <td>
                            <asp:DropDownList ID="ddlPTjKpd" runat="server" AutoPostBack="True" CssClass="form-control">
                                <asp:ListItem Value="0" Text="- SILA PILIH -" />
                            </asp:DropDownList>
                            &nbsp;&nbsp;
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Pilih PTj" ControlToValidate="ddlPTjKpd" CssClass="text-danger" ValidationGroup="grpSimpan" InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr style="height: 25px">
                        <td style="height: 25px;">
                            <label class="control-label" for="">
                                Kod Projek
                            </label>
                        </td>
                        <td style="height: 25px;">:</td>
                        <td>
                            <asp:DropDownList ID="ddlKPKpd" runat="server" AutoPostBack="True" CssClass="form-control">
                                <asp:ListItem Value="0" Text="- SILA PILIH -" />
                            </asp:DropDownList>
                            &nbsp;&nbsp;
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Pilih KP" ControlToValidate="ddlKPKpd" CssClass="text-danger" ValidationGroup="grpSimpan" InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr style="height: 25px">
                        <td style="height: 25px;">
                            <label class="control-label" for="">
                                Objek Am
                            </label>
                        </td>
                        <td style="height: 25px;">:</td>
                        <td>
                            <asp:DropDownList ID="ddlObjAmKpd" runat="server" AutoPostBack="True" CssClass="form-control">
                                <asp:ListItem Value="0" Text="- SILA PILIH -" />
                            </asp:DropDownList>
                            &nbsp;&nbsp;
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Pilih Objek Am" ControlToValidate="ddlObjAmKpd" CssClass="text-danger" ValidationGroup="grpSimpan" InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr style="height: 25px">
                        <td style="height: 25px;">
                            <label class="control-label" for="">
                                Objek Sebagai
                            </label>
                        </td>
                        <td style="height: 25px;">:</td>
                        <td>
                            <asp:DropDownList ID="ddlObjSbgKpd" runat="server" AutoPostBack="True" CssClass="form-control">
                                <asp:ListItem Value="0" Text="- SILA PILIH -" />
                            </asp:DropDownList>
                            &nbsp;&nbsp;
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Pilih Objek Sebagai" ControlToValidate="ddlObjSbgKpd" CssClass="text-danger" ValidationGroup="grpSimpan" InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr style="height: 25px">
                        <td style="height: 25px;">
                            <label class="control-label" for="">
                                Baki Peruntukan (RM)
                            </label>
                        </td>
                        <td style="height: 25px;">:</td>
                        <td>
                            <asp:TextBox ID="txtBakiKpd" runat="server" ReadOnly="True" CssClass="form-control rightAlign" Style="width: 150px;" BackColor="#FFFFCC" Text=""></asp:TextBox>
                        </td>
                    </tr>


                    <tr style="height: 25px">
                        <td style="height: 25px;">Baki Selepas Tambah/Kurang (RM)</td>
                        <td style="height: 25px;">:</td>
                        <td>
                            <asp:TextBox ID="txtBakiSlpsKpd" runat="server" BackColor="#FFFFCC" CssClass="form-control rightAlign" ReadOnly="True" Style="width: 150px;" Text="0.00"></asp:TextBox>
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
                        

                        <div class="row" runat="server">
                            <div runat="server" id="divLampList" class="row" style="margin-left:0px;">
                                
                               <span style="margin-bottom:50px;"> <asp:Label ID="lblBil" runat="server" Text="SENARAI DOKUMEN YANG TELAH DIMUAT NAIK" Font-Bold="true" /> </span>
                                
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
                                    <asp:Label ID="lblBG15_Id" runat="server" Text='<%# Eval("BG15_Id")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblNoMohon" runat="server" Text='<%# Eval("BG15_NoMohon")%>' />
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

                                <hr />
                            </div>
                            <div>
                                
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
            </div>
           
            <div class="row">
                <div style="text-align: center; margin-bottom: 10px; margin-top: 20px;">

                    <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn " ValidationGroup="grpSimpan">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                    </asp:LinkButton>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

               <asp:LinkButton ID="lbtnHantar" runat="server" CssClass="btn" OnClientClick="return confirm('Anda pasti untuk hantar permohonan Tambah/Kurang ini?');">
						<i class="fas fa-share-square fa-lg"></i></i>&nbsp;&nbsp;&nbsp;Hantar
               </asp:LinkButton>
                    &nbsp;&nbsp;

                        <asp:LinkButton ID="lbtnHapus" runat="server" CssClass="btn " OnClientClick="return confirm('Anda pasti untuk hapus rekod ini?');">
						<i class="fas fa-trash-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Hapus
                        </asp:LinkButton>
                    &nbsp;&nbsp;
                                                                 
                        <asp:LinkButton ID="lbtnBaru" runat="server" CssClass="btn ">
						<i class="fas fa-file-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Rekod Baru
                        </asp:LinkButton>

                </div>
            </div>
            
            <asp:HiddenField ID="hidIdMohon" runat="server"></asp:HiddenField>
                <asp:HiddenField ID="hidIndKWDpd" runat="server"></asp:HiddenField>
                <asp:HiddenField ID="hidIndPTjDpd" runat="server"></asp:HiddenField>
                <asp:HiddenField ID="hidIndObjAmDpd" runat="server"></asp:HiddenField>
                <asp:HiddenField ID="hidIndObjSbgDpd" runat="server"></asp:HiddenField>

                <asp:HiddenField ID="hidIndKWKpd" runat="server"></asp:HiddenField>
                <asp:HiddenField ID="hidIndPTjKpd" runat="server"></asp:HiddenField>
                <asp:HiddenField ID="hidIndObjAmKpd" runat="server"></asp:HiddenField>
                <asp:HiddenField ID="hidIndObjSbgKpd" runat="server"></asp:HiddenField>
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
