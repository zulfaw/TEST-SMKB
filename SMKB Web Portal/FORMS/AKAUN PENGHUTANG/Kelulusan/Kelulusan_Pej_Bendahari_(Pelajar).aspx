<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Kelulusan_Pej_Bendahari_(Pelajar).aspx.vb" Inherits="SMKB_Web_Portal.Kelulusan_Pej_Bendahari__Pelajar_" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>
     
     <script type="text/javascript">
      
        function pageLoad() {
            $('[data-toggle="tooltip"]').tooltip()

        }
        
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div id="divList" runat="server" class="divList">
                <div class="row" style="width:700px;">
            <div class="well">
                <table style="width: 100%;">                
                  <tr>
                      <td >Carian</td>
                      <td >:</td>
                      <td >
                          <asp:DropDownList ID="ddlCarian" runat="server" AutoPostBack="True" CssClass="form-control">
                              <asp:ListItem Selected="True" Text="- KESELURUHAN -" Value="0"></asp:ListItem>
                              <asp:ListItem Text="No. Invois Sementara" Value="1"></asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;&nbsp;
                          <asp:TextBox ID="txtCarian" runat="server" Width="250px" Enabled="false"></asp:TextBox>
                      </td>
                  </tr>
                  <tr>
                      <td ></td>
                      <td>&nbsp;</td>
                      <td>
                          <div style="margin-top:20px;">
                          <asp:LinkButton ID="lbtnCari" runat="server" CssClass="btn ">
						<i class="fas fa-search"></i>&nbsp;&nbsp;&nbsp;Cari
					</asp:LinkButton>
                              </div>
                      </td>
                  </tr>
              </table>            
            </div>
        </div>

                <div class="panel panel-default" style="width:70%;">
                    <div class="panel-heading">Senarai Invois</div>
                <div class="panel-body">
                     <div class="GvTopPanel" style="height:33px;">
                <div style="float:left;margin-top: 5px;margin-left: 10px;">
                <label class="control-label" for="">Jumlah rekod :</label>&nbsp;<label class="control-label" id ="lblJumRekod" runat="server" style="color:mediumblue;" ></label>
                 
                   &nbsp;&nbsp;<b style="color:#969696;">|</b> &nbsp;&nbsp;

                    <label class="control-label" for="Klasifikasi"> Saiz Rekod :</label>
        <asp:DropDownList ID="ddlSaizRekod" runat="server" Width="50px" AutoPostBack="True" class="form-control">
                <asp:ListItem Text="10" Value="10" />
                <asp:ListItem Text="25" Value="25" Selected="True" />
                <asp:ListItem Text="50" Value="50" />
                <asp:ListItem Text="100" Value="100" />
            </asp:DropDownList>
                    &nbsp;&nbsp;<b style="color:#969696;">|</b> &nbsp;&nbsp;
                      Status : &nbsp;
                    <asp:DropDownList ID="ddlFilStat" runat="server" AutoPostBack="True" CssClass="form-control"/>
                                   
                </div>
                            
                      
            </div>
                <asp:GridView ID="gvLst" runat="server" AllowPaging="True" AllowSorting="True"
                                ShowHeaderWhenEmpty="True"
                                AutoGenerateColumns="False"
                                EmptyDataText="Tiada rekod"
                                PageSize="25" CssClass="table table-striped table-bordered table-hover"
                                Width="100%" Height="100%" HeaderStyle-BackColor="#6699FF"
                                Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" ShowFooter="True">
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

                                    <asp:TemplateField HeaderText ="No. Invois Sementara">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoInvSem" runat="server" Text='<%# Eval("AR01_NoBilSem")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="15%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText ="No. Invois">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoInv" runat="server" Text='<%# Eval("AR01_NoBil")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="15%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Tujuan">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTuj" runat="server" Text='<%# Eval("AR01_Tujuan")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="25%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Tarikh Mohon">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTkhMhn" runat="server" Text='<%# Eval("AR01_TkhMohon", "{0:dd/MM/yyyy}")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="12%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Jumlah Bayar (RM)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblJumlah" runat="server" ForeColor="#003399" Text='<%# Eval("AR01_Jumlah", "{0:###,###,###.00}")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Width="12%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatDok" runat="server" Visible="false" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("AR01_StatusDok"))) %>' />
                                            <asp:Label ID="lblStat" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("ButStatus"))) %>' />
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

            <div id="divWiz" runat="server" style ="width:100%;">

            <div class="row">
				<asp:LinkButton ID="lbtnKembali" runat="server" CssClass="btn " Width="100px" ToolTip=""  >
						<i class="fas fa-long-arrow-alt-left  fa-lg"></i> &nbsp;&nbsp; Kembali
				</asp:LinkButton>
			</div>
        
        
           
                <div class="panel panel-default" style="width: 85%;">
                    <div class="panel-heading">
                        Maklumat Invois              
                    </div>
                    <div class="panel-body">
                        <asp:HiddenField ID="hidIdBil" runat="server" />
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 180px">Status</td>
                                <td style="width: 10px">:</td>
                                <td style="width: 450px">
                                    <asp:Label ID="lblStatus" runat="server" ForeColor="#000099"></asp:Label>


                                <td style="width: 120px">&nbsp;</td>
                                <td class="auto-style2">&nbsp;</td>
                                <td colspan="4">&nbsp;</td>

                            </tr>
                            <tr>
                                <td style="width: 180px">No. Invois Sementara</td>
                                <td style="width: 10px">:</td>
                                <td style="width: 450px">
                                    <asp:Label ID="lblNoInvSem" runat="server" ForeColor="#000099"></asp:Label>
                                    <td style="width: 100px">Tarikh Mohon</td>
                                    <td class="auto-style2">:</td>
                                    <td colspan="4">
                                        <asp:Label ID="lblTkhMohon" runat="server" ForeColor="#000099"></asp:Label>
                                    </td>
                                </td>
                            </tr>
                            <tr>
                                <td>No. Invois</td>
                                <td>:</td>
                                <td>
                                    <asp:TextBox ID="txtNoInv" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 150px;"></asp:TextBox>

                                    <td style="width: 70px">&nbsp;</td>
                                    <td class="auto-style2">&nbsp;</td>
                                    <td colspan="4">&nbsp;</td>

                                </td>
                            </tr>
                            <tr>
                                <td>Tarikh Bil</td>
                                <td>:</td>
                                <td><%--  <button runat="server" id="btnFindInv" title="Carian invois" class="btnNone" style="margin-top:-4px;">                         
                            <i class="fas fa-search"></i>
                            </button>--%>
                                    <asp:Label ID="lblTkhBil" runat="server" ForeColor="#000099"></asp:Label>
                                </td>

                                <td>No. Rujukan</td>
                                <td class="auto-style2">:</td>
                                <td colspan="4">
                                    <asp:Label ID="lblNoRuj" runat="server" ForeColor="#000099"></asp:Label>
                                </td>

                            </tr>
                            <tr>
                                <td class="auto-style1">Bank</td>
                                <td class="auto-style1">:</td>
                                <td class="auto-style1" colspan="7">
                                    <asp:DropDownList ID="ddlBank" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </td>

                            </tr>
                            <tr>
                                <td class="auto-style1">PTJ</td>
                                <td class="auto-style1">:</td>
                                <td class="auto-style1" colspan="7">
                                    <asp:Label ID="lblKodPTJP" runat="server" ForeColor="#000099"></asp:Label>
                                    &nbsp;-&nbsp;<asp:Label ID="lblNPTJP" runat="server" ForeColor="#000099"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Kategori Pelajar</td>
                                <td>:</td>
                                <td colspan="7">
                                    <asp:Label ID="lblKodKatPel" runat="server" ForeColor="#000099"></asp:Label>
                                    &nbsp;-
                                <asp:Label ID="lblKatPel" runat="server" ForeColor="#000099"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Status Pelajar</td>
                                <td>:</td>
                                <td colspan="7">
                                    <asp:Label ID="lblStatPel" runat="server" ForeColor="#000099"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Sesi</td>
                                <td>:</td>
                                <td colspan="7">
                                    <asp:Label ID="lblSesi" runat="server" ForeColor="#000099"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="9">&nbsp;</td>

                            </tr>
                            <tr>
                                <td>Nama Penaja</td>
                                <td>:</td>
                                <td>
                                    <asp:Label ID="lblNamaP" runat="server" ForeColor="#000099"></asp:Label>
                                </td>
                                <td class="50" style="width: 55px">Alamat </td>
                                <td class="auto-style2">:</td>
                                <td colspan="4">
                                    <asp:Label ID="lblAlmt1" runat="server" ForeColor="#000099"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>ID Penaja</td>
                                <td>:</td>
                                <td>
                                    <asp:Label ID="lblIdPenerima" runat="server" ForeColor="#000099"></asp:Label>
                                </td>

                                <td style="width: 55px">&nbsp;</td>
                                <td class="auto-style2">&nbsp;</td>
                                <td colspan="4">
                                    <asp:Label ID="lblAlmt2" runat="server" ForeColor="#000099"></asp:Label>
                                </td>

                            </tr>
                            <tr>
                                <td>Tujuan</td>
                                <td>:</td>
                                <td>
                                    <%-- <asp:DropDownList ID="ddlNamaPenerima" runat="server" AutoPostBack="True" CssClass="form-control" Style="width: 88%; height: 21px;">
                                </asp:DropDownList>--%>

                                    <asp:Label ID="lblTujuan" runat="server" ForeColor="#000099"></asp:Label>

                                </td>

                                <td style="width: 55px">Bandar</td>
                                <td class="auto-style2">:</td>
                                <td style="width: 250px">
                                    <asp:Label ID="lblBandar" runat="server" ForeColor="#000099"></asp:Label>
                                </td>

                                <td style="width: 50px;">Poskod</td>
                                <td style="width: 10px;">:</td>
                                <td>
                                    <asp:Label ID="lblPoskod" runat="server" ForeColor="#000099"></asp:Label>
                                </td>

                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td><%-- <asp:DropDownList ID="ddlNamaPenerima" runat="server" AutoPostBack="True" CssClass="form-control" Style="width: 88%; height: 21px;">
                                </asp:DropDownList>--%>
                                </td>
                                <td style="width: 55px">Negeri</td>
                                <td class="auto-style2">:</td>
                                <td colspan="4">
                                    <asp:Label ID="lblNegeri" runat="server" ForeColor="#000099"></asp:Label>
                                    &nbsp;&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>

                                <td style="width: 55px">Negara</td>
                                <td class="auto-style2">:</td>
                                <td colspan="4">
                                    <asp:Label ID="lblNegara" runat="server" ForeColor="#000099"></asp:Label>
                                </td>

                            </tr>
                            <tr>
                                <td style="vertical-align: top;">&nbsp;</td>
                                <td style="vertical-align: top;">&nbsp;</td>
                                <td>&nbsp;</td>

                                <td style="width: 55px">No.Tel</td>
                                <td class="auto-style2">:</td>
                                <td>
                                    <asp:Label ID="lblNoTel" runat="server" ForeColor="#000099"></asp:Label>
                                    &nbsp;</td>

                                <td>No.Fax</td>
                                <td>:</td>
                                <td>
                                    <asp:Label ID="lblNoFax" runat="server" ForeColor="#000099"></asp:Label>
                                </td>

                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>

                                <td>&nbsp;</td>

                                <td style="width: 55px">Emel</td>
                                <td class="auto-style2">:</td>
                                <td colspan="4">
                                    <asp:Label ID="lblEmail" runat="server" ForeColor="#000099"></asp:Label>
                                </td>



                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td style="width: 55px">Untuk Perhatian</td>
                                <td class="auto-style2">:</td>
                                <td colspan="4">
                                    <asp:Label ID="lblUP" runat="server" ForeColor="#000099"></asp:Label>
                                </td>

                            </tr>


                        </table>

                        <br />




                    </div>
                </div>

                <div class="row">
                    <div class="panel panel-default" style="width: 85%;">
                        <div class="panel-heading">
                            Lampiran            
                        </div>
                        <div class="panel-body">
                            <%--<div style="width: 95%">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 130px;">Pelajar</td>
                                        <td>:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlPel" runat="server" AutoPostBack="True" CssClass="form-control" Width="500px">
                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlPel" CssClass="text-danger" ErrorMessage="Pilih 'Pelajar'" InitialValue="0" ValidationGroup="grpTambah" />

                                        </td>

                                    </tr>
                                    <tr>
                                        <td>Nama</td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="txtNamaPelajar" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 550px;"></asp:TextBox>
                                            &nbsp;&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>No. Matrik</td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="txtNoMatrik" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 350px;"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>No. KP/Passport</td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="txtNoKP" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 350px;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Kod Kursus</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtKodKursus" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 150px;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Butiran</td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="txtButiran" runat="server" CssClass="form-control" Style="width: 650px;"></asp:TextBox>
                                            &nbsp;&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtButiran" CssClass="text-danger" ErrorMessage="Masukkan 'Butiran'" ValidationGroup="grpTambah" />
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>Jumlah (RM)</td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="txtAmaun" runat="server" CssClass="form-control rightAlign" Style="width: 150px;" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                            &nbsp;&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAmaun" CssClass="text-danger" ErrorMessage="Masukkan 'Amaun'" ValidationGroup="grpTambah" />
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:LinkButton ID="lbtnTambahPen" runat="server" CssClass="btn " Width="150px" ValidationGroup="grpTambah">
						<i class="fas fa-plus fa-lg"></i>&nbsp;&nbsp;&nbsp;Tambah Penerima
                                            </asp:LinkButton>
                                        </td>
                                    </tr>

                                </table>
                            </div>--%>

                            <%--   <div style="text-align: left; margin-bottom: 25px;margin-top:10px;">
          <asp:LinkButton ID="lbtnTambahPen" runat="server" CssClass="btn " Width="150px" >
						<i class="fas fa-plus fa-lg"></i>&nbsp;&nbsp;&nbsp;Tambah Pelajar
                                           </asp:LinkButton>
               </div>--%>

                            <div style="width: 95%; margin-top: 30px;">
                                <asp:GridView ID="gvLamp" runat="server" AllowSorting="True" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False"
                                    CssClass="table table-striped table-bordered table-hover" Width="100%" Font-Size="8pt" BorderStyle="Solid" ShowFooter="True">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Bil">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <ItemStyle Width="2%" HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdLamp" runat="server" Text='<%# Eval("AR02_IdLamp")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="250px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Nama Penerima">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNmPen" runat="server" Text='<%# Eval("AR02_NamaPenerima")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="250px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="No. KP/Passport">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNoKP" runat="server" Text='<%# Eval("AR02_NoKP")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="No. Matrik">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNoMatrik" runat="server" Text='<%# Eval("AR02_NoMatrik")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Kursus">
                                            <ItemTemplate>
                                                <asp:Label ID="lblKursus" runat="server" Text='<%# Eval("AR02_Kursus")%>' />
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Butiran">
                                            <ItemTemplate>
                                                <asp:Label ID="lblButiran" runat="server" Text='<%# Eval("AR02_Butiran")%>' />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div style="text-align: right; font-weight: bold;">
                                                    <asp:Label runat="server" Text="Jumlah Besar (RM)" />
                                                </div>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Jumlah (RM)">
                                            <ItemTemplate>
                                                <%--   <asp:TextBox ID="txtJumLamp" runat="server" AutoPostBack="true" CssClass="form-control rightAlign" onkeypress="return isNumberKey(event,this)" Style="width: 100%;" Text='<%#Eval("AR02_Amaun", "{0:###,###,###.00}")%>' OnTextChanged="txtJumLamp_TextChanged"></asp:TextBox>--%>

                                                <asp:Label ID="lblAmaun" runat="server" ForeColor="#003399" Text='<%# Eval("AR02_Amaun", "{0:N2}")%>' />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div style="text-align: right;">
                                                    <asp:Label ID="lblTotJum" runat="server" Font-Bold="true" ForeColor="#003399" />
                                                </div>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                        </asp:TemplateField>

                                        <%--<asp:TemplateField HeaderText="Tindakan">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete" CssClass="btn-xs" OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');" ToolTip="Padam">
										                    <i class="far fa-trash-alt fa-lg"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                        </asp:TemplateField>--%>
                                    </Columns>

                                    <EmptyDataTemplate>
                                    </EmptyDataTemplate>
                                    <SelectedRowStyle ForeColor="Blue" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                

                <div class="row">
                    <div class="panel panel-default" style="width: 85%;">
                        <div class="panel-heading">
                            Transaksi            
                        </div>
                        <div class="panel-body">
                            <asp:GridView ID="gvTrans" runat="server" AllowSorting="true" AutoGenerateColumns="False" BorderStyle="Solid" CssClass="table table-striped table-bordered table-hover" EmptyDataText=" Tiada rekod" Font-Size="8pt" ShowFooter="True" ShowHeaderWhenEmpty="True" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Bil" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="2%">
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
                                              <%--  <asp:HiddenField ID="hidKW" runat="server" Value='<%#Eval("KodKw")%>' />
                                                <asp:DropDownList ID="ddlKW" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlKW_SelectedIndexChanged" Width="150px">
                                                </asp:DropDownList>--%>
                                                <asp:Label ID="lblKW" runat="server" Text='<%#Eval("KodKw")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="KO">
                                            <ItemTemplate>
                                                <%--<asp:HiddenField ID="hidKO" runat="server" Value='<%#Eval("KodKO")%>' />
                                                <asp:DropDownList ID="ddlKO" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlKO_SelectedIndexChanged" Width="100px">
                                                </asp:DropDownList>--%>

                                                <asp:Label ID="lblKO" runat="server" Text='<%#Eval("KodKO")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PTj">
                                            <ItemTemplate>
                                                <%--<asp:HiddenField ID="hidPTj" runat="server" Value='<%#Eval("KodPTJ")%>' />
                                                <asp:DropDownList ID="ddlPTj" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlPTj_SelectedIndexChanged" Width="150px">
                                                </asp:DropDownList>--%>

                                                <asp:Label ID="lblPTj" runat="server" Text='<%#Eval("KodPTJ")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="KP">
                                            <ItemTemplate>
                                                <%--<asp:HiddenField ID="hidKP" runat="server" Value='<%#Eval("KodKP")%>' />
                                                <asp:DropDownList ID="ddlKP" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlKP_SelectedIndexChanged" Width="150px">
                                                </asp:DropDownList>--%>
                                                <asp:Label ID="lblKP" runat="server" Text='<%#Eval("KodKP")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vot">
                                            <ItemTemplate>
                                                <%--<asp:HiddenField ID="hidVot" runat="server" Value='<%#Eval("KodVot")%>' />
                                                <asp:DropDownList ID="ddlVot" runat="server" AutoPostBack="true" CssClass="form-control" Width="150px">
                                                </asp:DropDownList>--%>
                                                <asp:Label ID="lblVot" runat="server" Text='<%#Eval("KodVot")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Perkara">
                                            <ItemTemplate>
                                                <%--<asp:TextBox ID="txtPerkara" runat="server" CssClass="form-control" Style="width: 100%;" Text='<%#Eval("AR01_Perkara")%>'></asp:TextBox>--%>
                                                <asp:Label ID="lblPerkara" runat="server" Text='<%#Eval("AR01_Perkara")%>' />

                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="300px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Kuantiti">
                                            <ItemTemplate>
                                               <%-- <asp:TextBox ID="txtKuantiti" runat="server" AutoPostBack="true" CssClass="form-control rightAlign" onkeypress="return isNumberKey(event,this)" OnTextChanged="txtKuantiti_TextChanged" Style="width: 100%;" Text='<%#Eval("AR01_Kuantiti", "{0:N0}")%>'></asp:TextBox>--%>
                                                <asp:Label ID="lblKuantiti" runat="server" Text='<%#Eval("AR01_Kuantiti", "{0:N0}")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Harga (RM)">
                                            <ItemTemplate>
                                     <%--           <asp:TextBox ID="txtHarga" runat="server" AutoPostBack="true" CssClass="form-control rightAlign" onkeypress="return isNumberKey(event,this)" OnTextChanged="txtHarga_TextChanged" Style="width: 100%;" Text='<%#Eval("AR01_kadarHarga", "{0:###,###,###.00}")%>'></asp:TextBox>--%>

                                                <asp:Label ID="lblHarga" runat="server" Text='<%#Eval("AR01_kadarHarga", "{0:N2}")%>' />
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
                                                    <asp:Label ID="lblTotJum" runat="server" ClientIDMode="Static" CssClass="cssTotJum" Font-Bold="true" ForeColor="#003399" />
                                                </div>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="150px" />
                                        </asp:TemplateField>
                                       <%-- <asp:TemplateField HeaderText="Tindakan" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                &nbsp;
                                                        <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete" CssClass="btn-xs" OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');" ToolTip="Padam">
										                    <i class="far fa-trash-alt fa-lg"></i>
                                                        </asp:LinkButton>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnTambah" runat="server" CausesValidation="true" CssClass="btn " OnClick="lbtnTambah_Click" ToolTip="Tambah transaksi" Width="50px">
                        <i class="fas fa-plus fa-lg"></i>
                                                </asp:LinkButton>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                        </asp:TemplateField>--%>
                                    </Columns>
                                    <EmptyDataTemplate>
                                    </EmptyDataTemplate>
                                    <SelectedRowStyle ForeColor="Blue" />
                                </asp:GridView>
                        </div>
                    </div>
                </div>

                
       
            <div class="row">
               <div class="panel panel-default" style="width: 50%;">
                   <div class="panel-heading">Maklumat Pemohon</div>
                   <div class="panel-body">
                       <table style="width: 100%;">
                           <tr>
                               <td style="width: 106px; height: 23px;">Nama Pemohon</td>
                               <td style="height: 23px">:</td>
                               <td style="height: 23px">
                                   <asp:Label ID="lblNoPmhn" runat="server"></asp:Label>
                                   &nbsp;-&nbsp;<asp:Label ID="lblNmPemohon" runat="server"></asp:Label>
                                   &nbsp;</td>
                               <td style="height: 23px"></td>
                           </tr>
                           <tr>
                               <td style="width: 106px">Jawatan</td>
                               <td>:</td>
                               <td>
                                   <asp:Label ID="lblJawatan" runat="server"></asp:Label>
                               </td>
                               <td>&nbsp;</td>
                           </tr>
                           <tr>
                               <td style="width: 106px; height: 19px;">PTj</td>
                               <td style="height: 19px">:</td>
                               <td style="height: 19px">
                                   <asp:Label ID="lblKodPTjPmhn" runat="server"></asp:Label>
                                   &nbsp;-
                                   <asp:Label ID="lblNmPTjPmhn" runat="server"></asp:Label>
                               </td>
                               <td style="height: 19px"></td>
                           </tr>
                       </table>
                   </div>
               </div>
                   </div>

             
                <%--<div class="row">
                    <div class="panel panel-default" style="width: 700px;">
                        <div class="panel-heading">Pelulus</div>
                        <div class="panel-body">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 106px; height: 23px;">Nama Pelulus</td>
                                    <td style="height: 23px">:</td>
                                    <td style="height: 23px">
                                        <asp:Label ID="lblNoStafPel" runat="server"></asp:Label>
                                        &nbsp;-&nbsp;<asp:Label ID="lblNmStafPel" runat="server"></asp:Label>
                                        &nbsp;</td>
                                    <td style="height: 23px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 106px">Jawatan</td>
                                    <td>:</td>
                                    <td>
                                        <asp:Label ID="lblJawPel" runat="server"></asp:Label>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>

                                <tr>
                                    <td style="width: 106px">PTj</td>
                                    <td>:</td>
                                    <td>
                                        <asp:Label ID="lblKodPTjPel" runat="server"></asp:Label>
                                        &nbsp;-&nbsp;<asp:Label ID="lblNmPTjPel" runat="server"></asp:Label>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>

                                <tr>
                                    <td style="width: 106px">Tarikh Lulus</td>
                                    <td>:</td>
                                    <td>
                                        <asp:Label ID="lblTkhLulus" runat="server"></asp:Label>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">Ulasan</td>
                                    <td style="vertical-align: top;">:</td>
                                    <td>

                                        <asp:TextBox ID="txtUlasan" runat="server" CssClass="form-control" Height="70px" TextMode="multiline" Width="90%" AutoPostBack="true" ValidationGroup="grpXLulus"></asp:TextBox>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>--%>
      
        <div class="row">
              <div style="text-align:center;margin-top:50px;">
                     <asp:LinkButton ID="lbtnLulus" runat="server" CssClass="btn " OnClientClick="return confirm('Anda pasti untuk luluskan permohonan invois ini?');">
						<i class="fas fa-check-circle"></i>&nbsp;&nbsp;&nbsp;Lulus
					</asp:LinkButton>
                     
                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                         <asp:LinkButton ID="lbtnXLulus" runat="server" CssClass="btn " ValidationGroup="grpXLulus" OnClientClick="return confirm('Permohonan invois ini akan disimpan sebagai Tidak Lulus. Teruskan?');">
						<i class="fas fa-times-circle"></i>&nbsp;&nbsp;&nbsp;Tidak Lulus
					</asp:LinkButton> 
                  <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="grpXLulus" DisplayMode="SingleParagraph" HeaderText="Sila masukkan ulasan jika tidak lulus!" />                                         
                                </div>
                    </div>
                </div>

            <asp:Button ID="btnPopup1" runat="server" Style="display: none;" />                
                    <ajaxToolkit:ModalPopupExtender ID="mpeLstPel" runat="server" BackgroundCssClass="modalBackground" PopupControlID="PnlLstPel" TargetControlID="btnPopup1" CancelControlID="Button2">
                                     </ajaxToolkit:ModalPopupExtender>                       
            <asp:Panel ID="PnlLstPel" runat="server" BackColor="White" Width="800px" Style="display: ;">

                <table style="border: Solid 3px #D5AA00; width: 100%; height: 100%;">
                    <tr style="background: linear-gradient(to bottom, #fceabb 0%,#fccd4d 50%,#f8b500 51%,#fbdf93 100%);">
                        <td style="height: 10%; text-align: center;" class="">
                            <b>Senarai Pelajar</b></td>
                        <td style="width: 50px; text-align: center;">
                            <button runat="server" id="Button2" title="Tutup" class="btnNone ">
                                <i class="far fa-window-close fa-2x"></i>
                            </button>
                        </td>

                    </tr>
                    <tr>
                        <td colspan="2">
                            <div class="row" style="margin-top: 10px; width: 100%;">

                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 160px;">
                                            <asp:DropDownList ID="ddlFilter" runat="server" CssClass="form-control" AutoPostBack="true">
                                                <asp:ListItem Text="No. Matrik" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Nama Pelajar" Value="2"></asp:ListItem>

                                            </asp:DropDownList>&nbsp;&nbsp;
                          &nbsp;&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtFilter" runat="server" CssClass="form-control"  Style="width: 300px;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <div style="margin-bottom: 20px; margin-top: 10px;">
                                                <asp:LinkButton ID="lbtnCariPel" runat="server" CssClass="btn " ToolTip="Simpan" Width="80px">
						<i class="fas fa-search fa-lg"></i> &nbsp;&nbsp; Cari
                                                </asp:LinkButton>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <div style="margin-top: 10px;">
                                    <asp:GridView ID="gvLstPel" runat="server" AllowSorting="True"
                                        ShowHeaderWhenEmpty="True" AutoGenerateColumns="False"
                                        EmptyDataText=" " CssClass="table table-striped table-bordered table-hover"
                                        Width="100%" Height="100%" HeaderStyle-BackColor="#6699FF"
                                        Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid"
                                        ShowFooter="True" AllowPaging="true" PageSize="15">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Bil">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBil" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle Width="5%" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No. Matrik">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNoMatrik" runat="server" Text='<%# Eval("NoMatrik")%>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Nama Pelajar">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNmPel" runat="server" Text='<%# Eval("NamaPel")%>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblKodKursus" runat="server" Text='<%# Eval("KodKursus")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIdPel" runat="server" Text='<%# Eval("IDPel")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnSelect" runat="server" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CommandName="Select" CssClass="btn-xs" ToolTip="Pilih">
                              <i class="fas fa-edit"></i>                           
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="20px" />
                                            </asp:TemplateField>

                                        </Columns>
                                        <HeaderStyle BackColor="#6699FF"></HeaderStyle>
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
