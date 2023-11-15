<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Daftar_Penghutang.aspx.vb" Inherits="SMKB_Web_Portal.Daftar_Penghutang" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>

    <script type="text/javascript">
        function fCloseMpe() {
            $find("mpeLst").hide();
        };

    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="divList" runat="server" class="divList">
                <div style="margin: 20px; text-align: left;">
                    <asp:LinkButton ID="lBtnBaru" runat="server" CssClass="btn" Width="140px">
						<i class="fas fa-file-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Daftar Penghutang 
                    </asp:LinkButton>
                </div>

                <hr/>

                <div class="row" style="width: 700px;">                 
                    <div class="well">
                        <table style="width: 100%;">
                            <tr>
                                <td>Carian</td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList ID="ddlCarian" runat="server" AutoPostBack="true" CssClass="form-control" Style="width: 35%; height: 21px;">
                                        <asp:ListItem Text="- KESELURUHAN -" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Kod Penghutang" Value="1"></asp:ListItem>

                                         <asp:ListItem Text="Nama Penghutang" Value="2"></asp:ListItem>

                                         <asp:ListItem Text="Id Penghutang" Value="3"></asp:ListItem>
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

                <div class="row" style="width: 85%;">
                    <div class="panel panel-default" style="width: 100%;">
                        <div class="panel-heading">Senarai Penghutang</div>
                        <div class="panel-body">
                            <div class="GvTopPanel" style="height: 33px;">
                                <div style="float: left; margin-top: 5px; margin-left: 10px;">
                                    <label class="control-label" for="">Jumlah rekod :</label>&nbsp;
                              
                                    <asp:Label ID="lblJumRec" runat="server" style="color: mediumblue;" Text="0"/>

                                    &nbsp;&nbsp;<b style="color: #969696;">|</b> &nbsp;&nbsp;

                    <label class="control-label" for="Klasifikasi">Saiz Rekod :</label>
                                    <asp:DropDownList ID="ddlSaizRekod" runat="server" Width="50px" AutoPostBack="True" class="form-control">
                                        <asp:ListItem Text="10" Value="10" />
                                        <asp:ListItem Text="25" Value="25" Selected="True" />
                                        <asp:ListItem Text="50" Value="50" />
                                        <asp:ListItem Text="100" Value="100" />
                                    </asp:DropDownList>

                                    &nbsp;</div>
                            </div>
                            <asp:GridView ID="gvLst" runat="server" 
                                AllowPaging="True" 
                                AllowSorting="True" 
                                ShowHeaderWhenEmpty="True" 
                                AutoGenerateColumns="False" 
                                EmptyDataText=" Tiada Rekod."
                                PageSize="25" 
                                CssClass="table table-striped table-bordered table-hover" 
                                Width="100%" Height="100%" 
                                HeaderStyle-BackColor="#6699FF" 
                                Font-Size="8pt" BorderColor="#333333" 
                                BorderStyle="Solid" ShowFooter="True">
                                <Columns>

                                    <asp:TemplateField HeaderText="Bil">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBil" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblKod" runat="server" Text="Kod Penghutang" />&nbsp;
                            <asp:LinkButton ID="lnkKod" runat="server" CommandName="Sort" CommandArgument="KodPenghutang">
                                <span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span>
                            </asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblKodPenghutang" runat="server" Text='<%# Eval("KodPenghutang") %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center" Width="10%" />
                                    </asp:TemplateField>

                                   <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblID" runat="server" Text="ID" />&nbsp;
                            <asp:LinkButton ID="lnkID" runat="server" CommandName="Sort" CommandArgument="IdPenghutang"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblIdPenghutang" runat="server" Text='<%# Eval("IdPenghutang")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="15%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblNama" runat="server" Text="Nama" />&nbsp;
                            <asp:LinkButton ID="lnkNama" runat="server" CommandName="Sort" CommandArgument="NamaPenghutang"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblNama" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("NamaPenghutang"))) %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="43%" />
                                    </asp:TemplateField>

                                    

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblKat" runat="server" Text="Kategori" />&nbsp;
                            <asp:LinkButton ID="lnkKat" runat="server" CommandName="Sort" CommandArgument="Kategori"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblKat" runat="server" Text='<%# Eval("Kategori")%>' />
                                            &nbsp;-&nbsp;
                                            <asp:Label ID="lblButKat" runat="server" Text='<%# Eval("ButKat")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="15%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblStat" runat="server" Text="Status" />&nbsp;
                            <asp:LinkButton ID="lbkStat" runat="server" CommandName="Sort" CommandArgument="Status"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                   
                                            <asp:Label ID="Label1" runat="server" Text='<%# IIf(Boolean.Parse(Eval("Status").ToString()), "Aktif", "Tidak Aktif")%>' />
                                            
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="20%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnSelect" runat="server" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CommandName="Select" CssClass="btn-xs" ToolTip="Pilih">
                                                        <i class="fas fa-edit"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="7%" />
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle BackColor="#6699FF" />
                            </asp:GridView>

                        </div>
                    </div>
                </div>
            </div>

            <div id="divDt" visible="false" runat="server" style="width: 100%;">
                <div class="row">
                    <asp:LinkButton ID="lbtnKembali" runat="server" CssClass="btn " Width="100px" ToolTip="">
						<i class="fas fa-long-arrow-alt-left  fa-lg"></i> &nbsp;&nbsp; Kembali
                    </asp:LinkButton>
                </div>

                <div class="row">
                    <div class="panel panel-default" style="width:95%;">
                    <div class="panel-heading">
                        <h3 class="panel-title">Maklumat Penghutang</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <table style="width: 100%;">
                                <tr>                                   
                                    <td colspan="4">
                                        <div runat="server" id="alert" class="alert alert-info">
  Individu/Syarikat ini telah didaftarkan sebagai penghutang!
</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>No. Penghutang</td>
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox ID="txtNoPenghutang" runat="server" CssClass="form-control" Enabled="false" Style="width: 250px;"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><span style="color: red">*</span></td>
                                    <td>Kategori</td>
                                    <td>:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlKat" runat="server" AutoPostBack="True" CssClass="form-control" Width="300px" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="ddlKat" CssClass="text-danger" ErrorMessage="Pilih 'Kategori'" InitialValue="0" ValidationGroup="grpSimpan" />
                                    </td>
                                </tr>
                                <tr>                                   
                                    <td><span style="color: red">*</span></td>
                                    <td>Nama Penghutang</td>
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox ID="txtNamaPenghutang" runat="server" CssClass="form-control" Enabled="false" Style="width: 450px;"></asp:TextBox> &nbsp;
                                        

                                        <asp:LinkButton ID="lbtnOpenLst" runat="server" CssClass="btn" ToolTip="Cari" style="padding:2px;width: 31px; height: 25px;">
                        <i class="fa fa-search btn-xs" aria-hidden="true"></i>
                               </asp:LinkButton>

                                        <div>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtNamaPenghutang" CssClass="text-danger" Display="Dynamic" ErrorMessage="Masukkan 'Nama Penghutang'" ValidationGroup="grpSimpan" />
                                            </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 28px"><span style="color: red">*</span></td>
                                    <td style="height: 28px">ID / No.KP Penghutang</td>
                                    <td style="height: 28px">:</td>
                                    <td style="height: 28px">
                                        <asp:TextBox ID="txtIDPenghutang" runat="server" CssClass="form-control" Enabled="false" Style="width: 250px;"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtIDPenghutang" CssClass="text-danger" Display="Dynamic" ErrorMessage="Masukkan 'ID/No. KP Penerima'" ValidationGroup="grpSimpan" />
                                    </td>
                                    
                              

                                </tr>
                                <tr>
                                    <td style="height: 28px;vertical-align:top;"><span style="color: red">*</span></td>
                                    <td style="height: 28px;vertical-align:top;">Alamat </td>
                                    <td style="height: 28px;vertical-align:top;">:</td>
                                    <td style="height: 28px">
                        
                  <asp:TextBox ID="txtAlmt1" runat="server" CssClass="form-control" Style="width: 450px;" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                   
                                    </td>
                                 
                              
                                </tr>
                                <tr>
                                    <td style="height: 28px;vertical-align:top;">&nbsp;</td>
                                    <td style="height: 28px;vertical-align:top;">&nbsp;</td>
                                    <td style="height: 28px;vertical-align:top;">&nbsp;</td>
                                    <td style="height: 28px">
                              

                                        <asp:TextBox ID="txtAlmt2" runat="server" CssClass="form-control" Style="width: 450px;" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                    </td>                                                               
                                </tr>
                                <tr>

                                    <td style="vertical-align: top;"><span style="color: red">*</span></td>
                                    <td style="vertical-align: top;">Bandar</td>
                                    <td style="vertical-align: top;">:</td>
                                    <td style="height: 23px">
                                        <asp:TextBox ID="txtBandar" runat="server" CssClass="form-control" Width="450px"></asp:TextBox>
                                        <br />
                                    </td>
                                   
                                   

                                </tr>
                                <tr>
                                    <td style="vertical-align: top"><span style="color: red">*</span></td>
                                    <td style="vertical-align: top">Poskod</td>
                                    <td style="vertical-align: top">:</td>
                                    <td>
                                        <asp:TextBox ID="txtPoskod" runat="server" CssClass="form-control" Style="width: 150px;"></asp:TextBox>
                                        
                                    </td>
                                   
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;"><span style="color: red">*</span></td>
                                    <td style="vertical-align: top;">Negeri</td>
                                    <td style="vertical-align: top;">:</td>
                                    <td style="height: 23px">
                              <%--  <asp:TextBox ID="txtKodNegeri" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 50px;"></asp:TextBox>
                                        &nbsp;-&nbsp;
                                        <asp:TextBox ID="txtNegeri" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 450px;"></asp:TextBox>--%>
                                    <asp:DropDownList ID="ddlNegeri" runat="server" CssClass="form-control"/>
                                        &nbsp;&nbsp;
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" CssClass="text-danger" ControlToValidate="ddlNegeri" InitialValue="0" ErrorMessage="Sila pilih Negeri" ValidationGroup="grpSimpan" Display="Dynamic"/>
                                    </td>
                                  
                                </tr>

                                <tr>
                                    <td style="vertical-align: top;"><span style="color: red">*</span></td>
                                    <td style="vertical-align: top;">Negara</td>
                                    <td style="vertical-align: top;">:</td>
                                    <td style="height: 23px">
                                <%--<asp:TextBox ID="txtKodNegara" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 50px;"></asp:TextBox>
                                        &nbsp;-&nbsp;
                                        <asp:TextBox ID="txtNegara" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 450px;"></asp:TextBox>--%>

                                         <asp:DropDownList ID="ddlNegara" runat="server" CssClass="form-control"/>
                                        &nbsp;&nbsp;
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="text-danger" ControlToValidate="ddlNegara" InitialValue="0" ErrorMessage="Sila pilih Negara" ValidationGroup="grpSimpan" Display="Dynamic"/>

                                    </td>
                                   
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">&nbsp;</td>
                                    <td style="vertical-align: top;">No. Telefon</td>
                                    <td style="vertical-align: top;">:</td>
                                    <td style="height: 23px">
                                   
                                        <asp:TextBox ID="txtNoTel" runat="server" CssClass="form-control" Style="width: 150px;"></asp:TextBox>
                                    </td>
                                  
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">&nbsp;</td>
                                    <td style="vertical-align: top;">No. Fax</td>
                                    <td style="vertical-align: top;">:</td>
                                    <td style="height: 23px">
                                        <asp:TextBox ID="txtNoFax" runat="server" CssClass="form-control" Style="width: 150px;"></asp:TextBox>
                                    </td>
                                   
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">&nbsp;</td>
                                    <td style="vertical-align: top;">Emel</td>
                                    <td style="vertical-align: top;">:</td>
                                    <td style="height: 23px">

                               <asp:TextBox ID="txtEmel" runat="server" CssClass="form-control" Style="width: 450px;"></asp:TextBox>
                                    
                                    
                                    </td>
                                  
                                </tr>

                                <tr>
                                    <td style="vertical-align: top;">&nbsp;</td>
                                    <td style="vertical-align: top;">Perhatian</td>
                                    <td style="vertical-align: top;">:</td>
                                    <td style="height: 23px">
                                        <asp:TextBox ID="txtPerhatian" runat="server" CssClass="form-control" Style="width: 450px;"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td style="vertical-align: top;">&nbsp;</td>
                                    <td style="vertical-align: top;">Status</td>
                                    <td style="vertical-align: top;">:</td>
                                    <td style="height: 23px">
                                        <asp:RadioButtonList ID="rbStatus" runat="server" Height="25px" RepeatDirection="Horizontal" Width="273px">
                                                        <asp:ListItem Selected="True" Text=" Aktif" Value="True"></asp:ListItem>
                                                        <asp:ListItem Text=" Tidak Aktif" Value="False"></asp:ListItem>
                                                    </asp:RadioButtonList></td>
                                </tr>

                            </table>
                        </div>
                    </div>
                </div>
                </div>

                <div class="row">
                    <div style="margin-bottom: 10px; margin-top: 20px; text-align: center;">
                        <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn" Width="80px" ToolTip="Simpan" ValidationGroup="grpSimpan">
						    <i class="far fa-save fa-lg"></i> &nbsp;&nbsp; Simpan
                        </asp:LinkButton>

                        &nbsp;&nbsp;&nbsp;

                        <asp:LinkButton ID="lbtnNew" runat="server" CssClass="btn" Width="100px" Visible="false">
						    <i class="fas fa-file-alt fa-lg"></i> &nbsp;&nbsp; Rekod Baru
                        </asp:LinkButton>

                        &nbsp;&nbsp;&nbsp;

                        <asp:LinkButton ID="lbtnHapus" runat="server" CssClass="btn " OnClientClick="return confirm('Anda pasti untuk hapus rekod ini?');">
						<i class="fas fa-trash-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Hapus
                        </asp:LinkButton>

                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

       <asp:Button ID="btnOpenMPE" runat="server" Text="" Style="display: none" />
    <ajaxToolkit:ModalPopupExtender ID="mpeLstPenghutang" runat="server" BackgroundCssClass="modalBackground" BehaviorID="mpeLst"
        CancelControlID="btnCloseMpe" PopupControlID="pnlLstPenghutang" TargetControlID="btnOpenMPE">
    </ajaxToolkit:ModalPopupExtender>

    <asp:Panel ID="pnlLstPenghutang" runat="server" BackColor="White" Width="60%" Style="overflow: auto;">
        <asp:UpdatePanel runat="server" ID="UpdatePanel3">
            <ContentTemplate>
                <table style="border: Solid 3px #D5AA00; width: 100%; height: 100%;">

                    <tr style="background: linear-gradient(to bottom, #fceabb 0%,#fccd4d 50%,#f8b500 51%,#fbdf93 100%);">
                        <td colspan="2" style="height: 10%; font-weight: bold; text-align: center;">
                            <asp:Label ID="lblTitle" runat="server"></asp:Label> </td>
                        <td style="width: 10px; text-align: center;">
                            <button id="btnCloseMpe" runat="server" class="btnNone " title="Tutup" onclick="fCloseMpe();">
                                <i class="far fa-window-close fa-2x"></i>
                            </button>
                        </td>
                    </tr>

                    <tr style="height: 30px;">
                        <td colspan="3">
                            <div style="margin-top: 10px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Jumlah rekod :&nbsp;<label id="lblJumRekod" runat="server" class="control-label" style="color: mediumblue;"></label>
                            &nbsp;|&nbsp;&nbsp;                         
                           Cari : 
                           &nbsp;&nbsp;
                               
                               <asp:DropDownList ID="ddlFilter" runat="server" Width="150px" class="form-control" AutoPostBack="true"/>
                             &nbsp;
                            <asp:TextBox ID="txtFilter" runat="server" CssClass="form-control" Width="150px" Enabled="false"></asp:TextBox>                          
                            &nbsp;&nbsp;&nbsp;
                            <%--   <asp:LinkButton ID="lbtnCariPendahuluan" runat="server" CssClass="btn-circle" ToolTip="Cari" ValidationGroup="lbtnCariPendahuluan">
                        <i class="fa fa-search fa-2x" aria-hidden="true"></i>
                               </asp:LinkButton>--%>

                             <asp:LinkButton ID="lbtnFind" runat="server" CssClass="btnNone" ToolTip="Cari" style="padding:5px;width: 31px; height: 25px;">
                        <i class="fa fa-search btn-xs" aria-hidden="true"></i>
                               </asp:LinkButton>
                                </div>
                        </td>
                    </tr>


                    <tr style="vertical-align: top;">

                        <td colspan="3">
                            <div style="margin-bottom: 10px; margin-top: 10px; margin-left: 10px; margin-right: 10px;">
                                <asp:GridView ID="gvLstPenghutang" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True" EmptyDataText=" Tiada rekod"
                                    CssClass="table table-striped table-bordered table-hover" Width="100%" BorderStyle="Solid" Font-Size="8pt" DataKeyNames="ID" PageSize="15">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Bil" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="2%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblID" runat="server" Text="ID" />&nbsp;
                            <asp:LinkButton ID="lnkID" runat="server" CommandName="Sort" CommandArgument="ID"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblNama" runat="server" Text="Nama" />&nbsp;
                            <asp:LinkButton ID="lnkNama" runat="server" CommandName="Sort" CommandArgument="Nama"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblNama" runat="server" Text='<%# Eval("Nama")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField ItemStyle-Width="3%" ItemStyle-CssClass="Center" HeaderText="Tindakan">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Select" CssClass="btn-xs" ToolTip="Pilih">
								<i class="far fa-edit fa-lg"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Center" Width="5%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>

                </table>

            </ContentTemplate>

        </asp:UpdatePanel>
    </asp:Panel>

</asp:Content>
