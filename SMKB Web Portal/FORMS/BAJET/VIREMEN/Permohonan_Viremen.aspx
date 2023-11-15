<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Permohonan_Viremen.aspx.vb" Inherits="SMKB_Web_Portal.Permohonan_Viremen" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <%--<div class="container-fluid">--%>
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>

    <style>
        .valsummary ul
{
display: none;
visibility: hidden;
}
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate> 
                
            <div class="row">
            <div style="text-align:left;margin-bottom:10px;margin-top:20px;">             
                     <asp:LinkButton ID="lbtnList" runat="server" CssClass="btn " Width="140px">
						<i class="fas fa-list fa-lg"></i>&nbsp;&nbsp;&nbsp;Senarai Viremen
					</asp:LinkButton>
                </div>
                   </div>
                                   
                <div class="panel panel-default">
                <div class="panel-heading">Maklumat Viremen</div>
                    <div class="panel-body">
                        
                        <table style="width: 100%" class="table table table-borderless">
                            <tr>
                                <td style="width: 130px;">
                                    <label class="control-label" for="">No Viremen :</label></td>
                                <td>
                                    <asp:TextBox ID="txtNoVire" runat="server" BackColor="#FFFFCC" Width="150px" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    &nbsp &nbsp &nbsp &nbsp
                        <label class="control-label" for="">Status</label>
                                    :&nbsp; &nbsp;
                        <asp:TextBox ID="txtStatus" runat="server" BackColor="#FFFFCC" Width="40%" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    <asp:HiddenField ID="HidKodStatus" runat="server" />
                                </td>

                            </tr>
                            <tr>
                                <td>
                                    <label class="control-label" for="">Tarikh Mohon :</label></td>
                                <td>
                                    <asp:TextBox ID="txtTkhMohon" runat="server" BackColor="#FFFFCC" CssClass="form-control" Width="150px" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <label class="control-label" for="">Nama Pemohon :</label></td>
                                <td>

                                    <asp:TextBox ID="txtNoStaf" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 50px; text-align: left;"></asp:TextBox>
                                    &nbsp;-
                         <asp:TextBox ID="txtNamaPemohon" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Width="30%"></asp:TextBox>
                                    &nbsp;-
                         <asp:TextBox ID="txtJawPemohon" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="true" Width="50%"></asp:TextBox>

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label class="control-label" for="">Tahun Viremen :</label></td>
                                <td>
                                    <asp:TextBox ID="txtTahunVire" runat="server" BackColor="#FFFFCC" CssClass="form-control " Width="50px" ReadOnly="true"></asp:TextBox>

                                </td>
                            </tr>

                            <tr>
                                <td style="vertical-align: top;">
                                    <label class="control-label" for="">Tujuan :</label></td>
                                <td>
                                    <asp:TextBox ID="txtTujuan" runat="server" TextMode="multiline" CssClass="form-control" Width="50%" Rows="4" placeholder="Masukkan Tujuan"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Masukkan Tujuan" ControlToValidate="txtTujuan" CssClass="text-danger" ValidationGroup="grpSimpan"></asp:RequiredFieldValidator>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label class="control-label" for="">
                                        No. Rujukan Surat :</label></td>
                                <td>
                                    <asp:TextBox ID="txtRujSurat" runat="server" class="form-control" ReadOnly="False" Width="30%"></asp:TextBox>
                                    <label id="lblMsgRujSrt" class="control-label" for="" style="display: none; color: #820303;">
                                        (Masukkan Rujukan Surat)
                                    </label>
                                </td>
                            </tr>
                        </table>


                        <div class="panel panel-default" style="margin-left: 0px; width: 100%;">
                            <div class="panel-heading">Daripada</div>

                            <div class="panel-body">
                                <table class="nav-justified">

                                    <tr style="height: 25px">
                                        <td style="width: 180px">
                                            <label class="control-label" for="Butiran">
                                                Kumpulan Wang :
                                            </label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlKWk" runat="server" AutoPostBack="True" CssClass="form-control" Style="height: 21px;"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Pilih KW" ControlToValidate="ddlKWk" CssClass="text-danger" ValidationGroup="grpSimpan" InitialValue="0"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr style="height: 25px">
                                        <td>
                                            <label class="control-label" for="Butiran">
                                                Kod Operasi :
                                            </label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlKOk" runat="server" AutoPostBack="True" CssClass="form-control" Style="height: 21px;"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Pilih KO" ControlToValidate="ddlKOk" CssClass="text-danger" ValidationGroup="grpSimpan" InitialValue="0"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr style="height: 25px">
                                        <td>
                                            <label class="control-label" for="">
                                                PTJ / Fakulti :</label></td>
                                        <td>
                                            <asp:DropDownList ID="ddlPTJk" runat="server" AutoPostBack="True" CssClass="form-control" Style="height: 21px;"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Pilih PTj" ControlToValidate="ddlPTJk" CssClass="text-danger" ValidationGroup="grpSimpan" InitialValue="0"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr style="height: 25px">
                                        <td>
                                            <label class="control-label" for="Butiran">
                                            Kod Projek :               
                                        </td>
                                        <td style="height: 22px">
                                            <asp:DropDownList ID="ddlKPk" runat="server" AutoPostBack="True" CssClass="form-control" Style="height: 21px;"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Pilih KP" ControlToValidate="ddlKPk" CssClass="text-danger" ValidationGroup="grpSimpan" InitialValue="0"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>

                                    <tr style="height: 25px">
                                        <td>
                                            <label class="control-label" for="Butiran">
                                            Objek Sebagai :               
                                        </td>
                                        <td style="height: 22px">
                                            <asp:DropDownList ID="ddlObjSbgK" runat="server" AutoPostBack="True" CssClass="form-control" Style="height: 21px;"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Pilih Objek Sebagai" ControlToValidate="ddlObjSbgK" CssClass="text-danger" ValidationGroup="grpSimpan" InitialValue="0"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>

                                    <tr style="height: 25px">
                                        <td>
                                            <label class="control-label" for="">
                                                Baki Semasa (RM) :</label></td>
                                        <td style="height: 22px">
                                            <asp:TextBox ID="txtBakiSmsK" runat="server" BackColor="#FFFFCC" class="form-control rightAlign " ReadOnly="True" Width="30%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="height: 25px">
                                        <td>
                                            <label class="control-label" for="">
                                                Baki Selepas Viremen (RM) :</label></td>
                                        <td style="height: 22px">
                                            <asp:TextBox ID="txtBakiSlpsVireK" runat="server" BackColor="#FFFFCC" class="form-control rightAlign " ReadOnly="True" Width="30%"></asp:TextBox>
                                            <i aria-hidden="true" class="fas fa-info-circle fa-lg" data-placement="right" style="cursor: pointer;" data-toggle="tooltip" title="Baki Selepas Viremen = Baki Semasa - Jumlah Viremen"></i>
                                        </td>
                                    </tr>


                                </table>
                            </div>
                        </div>


                        <div class="panel panel-default" style="margin-left: 0px; width: 100%;">
                            <div class="panel-heading">
                                Kepada
                            </div>
                            <div class="panel-body">
                                
                                <table class="nav-justified">

                                    <tr style="height: 25px">
                                        <td style="width: 180px">
                                            <label class="control-label" for="Butiran">
                                                Kumpulan Wang :
                                            </label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlKWm" runat="server" AutoPostBack="True" CssClass="form-control" Style="height: 21px;"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Pilih KW" ControlToValidate="ddlKWm" CssClass="text-danger" ValidationGroup="grpSimpan" InitialValue="0"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr style="height: 25px">
                                        <td>
                                            <label class="control-label" for="Butiran">
                                                Kod Operasi :
                                            </label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlKOm" runat="server" AutoPostBack="True" CssClass="form-control" Style="height: 21px;"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Pilih KO" ControlToValidate="ddlKOm" CssClass="text-danger" ValidationGroup="grpSimpan" InitialValue="0"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>

                                    <tr style="height: 25px">
                                        <td>
                                            <label class="control-label" for="">
                                                PTJ / Fakulti :</label></td>
                                        <td>
                                            <asp:DropDownList ID="ddlPTJm" runat="server" AutoPostBack="True" CssClass="form-control" Style="height: 21px;"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Pilih PTj" ControlToValidate="ddlPTJm" CssClass="text-danger" ValidationGroup="grpSimpan" InitialValue="0"></asp:RequiredFieldValidator>
                                            &nbsp;&nbsp;
                          <label id="lblMsgPTJm" class="control-label" for="" style="display: none; color: #820303;">
                              (Pilih  PTJ / Fakulti )
                          </label>
                                        </td>
                                    </tr>
                                    <tr style="height: 25px">
                                        <td>
                                            <label class="control-label" for="Butiran">
                                            Kod Projek :               
                                        </td>
                                        <td style="height: 22px">
                                            <asp:DropDownList ID="ddlKPm" runat="server" AutoPostBack="True" CssClass="form-control" Style="height: 21px;"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Pilih KP" ControlToValidate="ddlKPm" CssClass="text-danger" ValidationGroup="grpSimpan" InitialValue="0"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>

                                    <tr style="height: 25px">
                                        <td>
                                            <label class="control-label" for="Butiran">
                                            Objek Sebagai :               
                                        </td>
                                        <td style="height: 22px">
                                            <asp:DropDownList ID="ddlObjSbgM" runat="server" AutoPostBack="True" CssClass="form-control" Style="height: 21px;"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Pilih Objek Sebagai" ControlToValidate="ddlObjSbgM" CssClass="text-danger" ValidationGroup="grpSimpan" InitialValue="0"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>

                              </tr>
                              <tr style="height: 25px">
                                  <td style="width: 116px; height: 22px;">
                                      <label class="control-label" for="">
                                          Baki Semasa (RM) :</label></td>
                                  <td style="height: 22px">
                                      <asp:TextBox ID="txtBakiSmsM" runat="server" BackColor="#FFFFCC" class="form-control rightAlign " ReadOnly="True" Width="30%"></asp:TextBox>
                                  </td>
                              </tr>
                                        <tr>
                                            <td>
                                                <label class="control-label" for="">
                                                    Baki Selepas Viremen (RM) :</label></td>
                                            <td style="height: 16px">
                                                <asp:TextBox ID="txtBakiSlpsVireM" runat="server" BackColor="#FFFFCC" class="form-control rightAlign " ReadOnly="True" Width="30%"></asp:TextBox>
                                                <i aria-hidden="true" class="fas fa-info-circle fa-lg" data-placement="right" data-toggle="tooltip" style="cursor: pointer;" title="Baki Selepas Viremen = Baki Semasa - Jumlah Viremen"></i>
                                            </td>
                                        </tr>
                                </table>
                            </div>
                        </div>

                        <div class="panel panel-default" style="margin-left: 0px; width: 100%;">
                            <div class="panel-heading">
                                Borang BEN/UBP/001
                            </div>
                            <div class="panel-body">
                                <table style="width: 100%; margin-right: 0px;">
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td style="width: 150px">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;"><b>Jumlah Viremen (RM) :</b></td>
                                        <td>
                                            <%--<asp:TextBox ID="txtJumVire" runat="server" class="form-control rightAlign " onkeypress="return isNumberKey(event,this)" onkeyup="fCalc()" Width="100px"></asp:TextBox>--%>
                                            <asp:TextBox ID="txtJumVire" runat="server" onkeypress="return isNumberKey(event,this)" onblur="if (this.dirty){this.onchange();}" AutoPostBack="true" Width="100px" Enabled="false" CssClass="form-control rightAlign"></asp:TextBox>

                                            <div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Masukkan jumlah viremen" ControlToValidate="txtJumVire" CssClass="text-danger" ValidationGroup="grpSimpan"></asp:RequiredFieldValidator>
                                                </div>
                                            &nbsp;&nbsp;
                                    <label id="lblMsgJumV" class="control-label" for="" style="display: none; color: #820303;">
                                        Masukkan Jumlah Viremen
                                    </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td><b>Butir-butir tahun dahulu : </b></td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>a) Jumlah Peruntukan (termasuk Tambahan dan Pindahan Peruntukan)</td>
                                        <td>
                                            <asp:TextBox ID="txtBoxA" runat="server" BackColor="#FFFFCC" CssClass="form-control center-align" Enabled="False" Style="width: 100px; margin-left: 0; text-align: right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>b) Banyaknya telah dibelanjakan</td>
                                        <td>
                                            <asp:TextBox ID="txtBoxB" runat="server" BackColor="#FFFFCC" CssClass="form-control center-align" Enabled="False" Style="width: 100px; text-align: right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;<b> Butir-butir anggaran semasa : </b></td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>a) Peruntukan asal </td>
                                        <td>
                                            <asp:TextBox ID="txtBoxA1" runat="server" BackColor="#FFFFCC" CssClass="form-control" Enabled="False" Style="width: 100px; margin-left: 0; text-align: right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>b) Tambahan-tambahan(Tambahan/Pindah Peruntukan) </td>
                                        <td>
                                            <asp:TextBox ID="txtBoxB1" runat="server" BackColor="#FFFFCC" CssClass="form-control" Enabled="False" Style="width: 100px; text-align: right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>c) Pengurangan (Pinda Peruntukan) </td>
                                        <td>
                                            <asp:TextBox ID="txtBoxC" runat="server" BackColor="#FFFFCC" CssClass="form-control" Enabled="False" Style="width: 100px; text-align: right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>d) Peruntukan dipinda (a+b-c) </td>
                                        <td>
                                            <asp:TextBox ID="txtBoxD" runat="server" BackColor="#FFFFCC" CssClass="form-control" Enabled="False" Style="width: 100px; text-align: right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>e) Banyaknya telah dibelanjakan pada tarikh permohonan ini </td>
                                        <td>
                                            <asp:TextBox ID="txtBoxE" runat="server" BackColor="#FFFFCC" CssClass="form-control" Enabled="False" Style="width: 100px; text-align: right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>f) Tanggungan tidak dapat dielakkan yang belum selesai </td>
                                        <td>
                                            <asp:TextBox ID="txtBoxF" runat="server" BackColor="#FFFFCC" CssClass="form-control" Enabled="False" Style="width: 100px; text-align: right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>g) Tanggungan yang diperlukan masa depan(tambahan kepada f) </td>
                                        <td>
                                            <asp:TextBox ID="txtBoxG" runat="server" BackColor="#FFFFCC" CssClass="form-control" Enabled="false" Style="width: 100px; text-align: right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>h) Jumlah dikehendaki sekarang untuk tahun ini(e+f+g) </td>
                                        <td>
                                            <asp:TextBox ID="txtBoxH" runat="server" BackColor="#FFFFCC" CssClass="form-control" Enabled="False" Style="width: 100px; text-align: right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>i) Sekatan melalui waran sekatan </td>
                                        <td>
                                            <asp:TextBox ID="txtBoxI" runat="server" BackColor="#FFFFCC" CssClass="form-control" Enabled="False" Style="width: 100px; text-align: right;"></asp:TextBox>
                                        </td>
                                    </tr>



                                </table>
                            </div>
                        </div>

                        <div style="text-align: center; margin-bottom: 10px;">
                            <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn  btnSave" ToolTip="Simpan maklumat viremen" ValidationGroup="grpSimpan">
									<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                            </asp:LinkButton>
                            
                           
                            &nbsp;&nbsp; &nbsp;&nbsp; 
                                            <asp:LinkButton ID="lbtnHapus" runat="server" CssClass="btn " OnClientClick="return fConfirmDel()" ToolTip="Batalkan maklumat viremen">
						<i class="fas fa-trash-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Hapus
                                            </asp:LinkButton>
                            &nbsp;&nbsp; 
                                            <asp:LinkButton ID="lbtnBaru" runat="server" CssClass="btn " ToolTip="Kosongkan Butiran Perbelanjaan">
									<i class="fas fa-file-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Rekod baru
                                            </asp:LinkButton>

                            
                        </div>
                    </div> 
           </div>

            <asp:Button ID="btnPopup1" runat="server" style="display:none;"/>                
                    <ajaxToolkit:ModalPopupExtender ID="mpeLst" runat="server" BackgroundCssClass="modalBackground" PopupControlID="PnlLstInv" TargetControlID="btnPopup1" BehaviorID="mpe2"  >
                                     </ajaxToolkit:ModalPopupExtender>                       
            <asp:Panel ID="PnlLstInv" runat="server" BackColor="White" Width="1200px" style="display:none ;">
               
                <table  style="border: Solid 3px #D5AA00; width: 100%; height: 100%;">
                    <tr style="background: linear-gradient(to bottom, #fceabb 0%,#fccd4d 50%,#f8b500 51%,#fbdf93 100%);">
                         <td style="height: 10%;text-align:center;" class="">
                            <b> Senarai Viremen</b></td>
                        <td style="width:50px;text-align:center;">   
                           <button runat="server" id="Button2" title="Tutup" class="btnNone ">
    <i class="far fa-window-close fa-2x"></i>
</button></td>
                                                    
                        
                    </tr>
                    <tr>
                        <td colspan="2" >
                            <div class="row" style="margin-top:10px;width:100%;">
                                   
                                <table style="width: 100%;">
                  <tr>
                      <td style="width:50px;">Status</td>
                      <td style="width:10px;">:</td>
                      <td>                        
                          <asp:DropDownList ID="ddlStatusFil" runat="server" CssClass="form-control">
                          </asp:DropDownList>
                      </td>
                  </tr>
                                    <tr>
                                        <td >&nbsp;</td>
                                        <td >&nbsp;</td>
                                        <td style="height:45px;">
                                            <asp:LinkButton ID="lnkBtnCari" runat="server" CssClass="btn " ToolTip="Simpan" Width="80px">
						<i class="fas fa-search fa-lg"></i> &nbsp;&nbsp; Cari
					</asp:LinkButton>
                                        </td>
                                    </tr>
                 </table>

                                <div style="margin-top: 20px;">
                                    <div class="GvTopPanel" style="height: 33px;">
                                        <div style="float: left; margin-top: 5px; margin-left: 10px;">
                                            <label class="control-label" for="">Jumlah rekod :</label>&nbsp;<label class="control-label" id="lblJumRekod" runat="server" style="color: mediumblue;"></label>          
                                        </div>
                                    </div>
                                    <asp:GridView ID="gvViremen" runat="server" ShowHeaderWhenEmpty="True"
                                        AutoGenerateColumns="False" AllowSorting="True" EmptyDataText="Tiada rekod"
                                        CssClass="table table-striped table-bordered table-hover" Width="100%" HeaderStyle-BackColor="#ffffb3" Font-Size="8pt"
                                        BorderColor="#333333" BorderStyle="Solid"
                                        AllowPaging="true" PageSize="5">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Bil">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBil" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle Width="30px" />

                                            </asp:TemplateField>

                                            <asp:BoundField HeaderText="No. Viremen" DataField="NoViremen" SortExpression="NoViremen" ReadOnly="true">
                                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Tarikh Mohon" DataField="TkhMohon" SortExpression="TkhMohon" ReadOnly="true">
                                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="KW" DataField="KwD" SortExpression="DrKw" ReadOnly="true">
                                                <ItemStyle Width="5%" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="KO" DataField="KOD" SortExpression="DrKO" ReadOnly="true">
                                                <ItemStyle Width="5%" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="PTJ" DataField="PtjD" SortExpression="DrPtj" ReadOnly="true">
                                                <ItemStyle Width="5%" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="KP" DataField="KpD" SortExpression="DrKp" ReadOnly="true">
                                                <ItemStyle Width="5%" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Objek Sebagai" DataField="ObjSbgD" SortExpression="DrObjSbg" ReadOnly="true">
                                                <ItemStyle Width="8%" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Amaun (RM)" DataField="JumlahD" SortExpression="DrJumlah" ReadOnly="true">
                                                <ItemStyle Width="10%" HorizontalAlign="Right" ForeColor="#003399" Font-Bold="true" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="KW" DataField="KwK" SortExpression="KpdKw" ReadOnly="true">
                                                <ItemStyle Width="5%" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="KO" DataField="KOK" SortExpression="KpdKO" ReadOnly="true">
                                                <ItemStyle Width="5%" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="PTJ" DataField="PtjK" SortExpression="KpdPtj" ReadOnly="true">
                                                <ItemStyle Width="5%" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="KP" DataField="KpK" SortExpression="KpdKp" ReadOnly="true">
                                                <ItemStyle Width="5%" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Objek Sebagai" DataField="ObjSbgK" SortExpression="KpdOjkSbg" ReadOnly="true">
                                                <ItemStyle Width="8%" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Amaun (RM)" DataField="JumlahK" SortExpression="KpdJumlah" ReadOnly="true">
                                                <ItemStyle Width="10%" HorizontalAlign="Right" ForeColor="#003399" Font-Bold="true" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Status" HeaderText="Status">
                                                <ItemStyle HorizontalAlign="Left" Width="20%" />
                                            </asp:BoundField>

                                            <asp:TemplateField>

                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="btn-xs" ToolTip="Pilih">
                                            <i class="fas fa-edit"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="5%" />
                                            </asp:TemplateField>

                                        </Columns>

                                        <HeaderStyle BackColor="#FFFFB3" />

                                    </asp:GridView>
                                </div>                                                                                                                                                                          
                            </div>

                        </td>
                    </tr>                           
          </table> 
                
            </asp:Panel>     
                     </ContentTemplate>        
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
