<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Vot.aspx.vb" Inherits="SMKB_Web_Portal.Vot" EnableEventValidation ="false"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>


     <script type="text/javascript">

      function isNumberKey(evt)
      {
         var charCode = (evt.which) ? evt.which : evt.keyCode;
         if (charCode > 31 && (charCode < 48 || charCode > 57))
             return false;         
         return true;         
      }

          function fConfirm() {           
              try {

                debugger 
                var blnComplete = true;

                var txtKodVot = document.getElementById('<%=txtKodVot.ClientID%>').value
                var txtButiran = document.getElementById('<%=txtButiran.ClientID%>').value

                var ddlKlasifikasi = document.getElementById('<%=ddlKlasifikasi.ClientID%>');
                var intSelKlas = ddlKlasifikasi.selectedIndex

                var ddlJenis = document.getElementById('<%=ddlJenis.ClientID%>');
                var intSelJns = ddlJenis.selectedIndex

                  //Kod Vot
                if (txtKodVot == "") {
                    blnComplete = false
                    document.getElementById("lblMsgKodVot").style.display = 'inline-block';
                }
                else {
                    document.getElementById("lblMsgKodVot").style.display = 'none';
                }

                  //Butiran Vot
                if (txtButiran == "") {
                    blnComplete = false
                    document.getElementById("lblMsgButiran").style.display = 'inline-block';
                }
                else {
                    document.getElementById("lblMsgButiran").style.display = 'none';
                }

                //Klasifikasi
                if (intSelKlas == 0 || intSelKlas == -1) {
                    blnComplete = false
                    document.getElementById("lblMsgKlasifikasi").style.display = 'inline-block';
                }
                else {
                    document.getElementById("lblMsgKlasifikasi").style.display = 'none';
                }

                //Jenis
                if (intSelJns == 0 || intSelJns == -1) {
                    blnComplete = false
                    document.getElementById("lblMsgJenis").style.display = 'inline-block';
                }
                else {
                    document.getElementById("lblMsgJenis").style.display = 'none';
                }

                if (blnComplete == false) {
                    alert('Sila lengkapkan maklumat!')
                    return false;
                }

                <%--var ddlKlasifikasi = document.getElementById('<%=ddlKlasifikasi.ClientID%>');
                var intSelKlas = ddlKlasifikasi.selectedIndex;
                if (intSelKlas == 0) {
                    alert('Sila Pilih Klasifikasi')
                    return false;
                }
                
                var ddlJenis = document.getElementById('<%=ddlJenis.ClientID%>');
                var intSelJns = ddlJenis.selectedIndex;
                if (intSelJns == 0) {
                    alert('Sila Pilih Jenis')
                    return false;
                }

                if (valTxtKodVot == "")
                {
                    alert('Sila Masukkan Kod Vot!')
                    return false;
                }

                if (valTxtButiran == "")
                {
                    alert('Sila Masukkan Butiran !')
                    return false;
                }--%>
               
                
                if (confirm('Anda pasti untuk simpan rekod ini?')) {
                    return true;
                } else {
                    return false;
                }
            }
            catch (err) {
                return false
            }
        }

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

    <style>
     
        @media (min-width: 1450px){
            .panel
            {
                width :70%;
            }

            .row
            {
                width :70%;
            }
        }
        @media (max-width: 1450px) {
            .panel {
                width: 70%;
            }

            .row
            {
                width :100%;
            }
        }

        @media (max-width: 1250px) {
            .panel {
                min-width: 100%;
            }

            .row
            {
                width :100%;
            }
        }
        </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            
            <div class="row">
                <div class="panel panel-default" style="width:600px;">
                    <div class="panel-body">
                        <table class="table-responsive">
                            <tr style="height: 25px;">
                                <td>
                                    <label class="control-label" for="">Kod Vot </label>
                                </td>
                                <td >:</td>
                                <td>
                                    <asp:TextBox ID="txtKodVot" runat="server" class="form-control" Width="100px" BackColor="White" MaxLength="5" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    &nbsp;<%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtKodVot" ErrorMessage="" ForeColor="#820303" Text="*Perlu diisi" ValidationGroup="btnSaveVot" Display="Dynamic" ></asp:RequiredFieldValidator>--%><label id="lblMsgKodVot" class="control-label" for="" style="display: none; color: #820303;">(Masukkan Kod Vot)
                                    </label>
                                    <i aria-hidden="true" class="fas fa-info-circle fa-lg" data-html="true" data-placement="right" data-toggle="tooltip" title="&nbsp;Masukkan 5 nombor sahaja.Huruf tidak dibenarkan."></i>
                                </td>
                            </tr>
                            <tr style="height: 25px">
                                <td style="height: 22px; vertical-align: top;">
                                    <label class="control-label" for="Butiran">
                                        Butiran
                                    </label>
                                    <td style="height: 22px; vertical-align: top;">
                                        :<td>
                                            <asp:TextBox ID="txtButiran" runat="server" class="form-control" Height="50px" TextMode="MultiLine" Width="350px"></asp:TextBox>
                                            &nbsp;
                                            <label id="lblMsgButiran" class="control-label" for="" style="display: none; color: #820303;">
                                            (Masukkan Butiran Vot)
                                            </label>
                                        </td>
                                    </td>
                                    <td style="height: 22px">&nbsp;</td>
                                </td>
                            </tr>
                            <tr style="height: 25px">
                                <td style="height: 22px;">
                                    <label class="control-label" for="Klasifikasi">
                                        Klasifikasi
                                    </label>
                                </td>
                                <td style="height: 22px;">:</td>
                                <td style="height: 22px">
                                    <asp:DropDownList ID="ddlKlasifikasi" runat="server" AutoPostBack="True" CssClass="form-control" Style="width: 280px;">
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;
                          <label id="lblMsgKlasifikasi" class="control-label" for="" style="display: none; color: #820303;">
                              (Pilih Klasifikasi)
                          </label>
                                </td>
                            </tr>
                            <tr style="height: 25px">
                                <td style="height: 22px;">
                                    <label class="control-label" for="Jenis">
                                        Jenis</label></td>
                                <td style="height: 22px;">:</td>
                                <td style="height: 22px">
                                    <asp:DropDownList ID="ddlJenis" runat="server" AutoPostBack="True" CssClass="form-control" Style="width: 280px;">
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;
                          <label id="lblMsgJenis" class="control-label" for="" style="display: none; color: #820303;">
                              (Pilih Jenis)
                          </label>
                                </td>
                            </tr>
                            <tr style="height: 55px; vertical-align: bottom">
                                <td>&nbsp; </td>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn " OnClientClick="return fConfirm();">
									<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                                    </asp:LinkButton>
                                    &nbsp;&nbsp;&nbsp; &nbsp;
                          <asp:LinkButton ID="lbtnHapus" runat="server" CssClass="btn" Visible="false" OnClientClick="return confirm('Anda pasti untuk hapus rekod ini?');">
						<i class="fas fa-trash-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Hapus
                          </asp:LinkButton>
                                    &nbsp;&nbsp;
                          <asp:LinkButton ID="lbtnBaru" runat="server" CssClass="btn ">
									<i class="fas fa-file-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Rekod baru
                          </asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="row">


                <div style="width: 750px; margin-left: 20px;">
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
                     <label class="control-label" for="Klasifikasi">Carian :</label>
                            <asp:DropDownList ID="ddlCarian" runat="server" AutoPostBack="True" CssClass="form-control">
                                <asp:ListItem Value="0" Text="- KESELURUHAN -"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Kod Vot"></asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;&nbsp;
                    <asp:TextBox ID="txtCarian" runat="server" Width="150px" Visible="false" MaxLength="5"></asp:TextBox>
                            &nbsp;                       
                <button id="btnCarian" runat="server" class="btnNone" style="margin-top: -4px;">
                    <i class="fas fa-search"></i>
                </button>
                        </div>
                    </div>
                    <asp:GridView ID="gvVot" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BorderColor="#333333"
                        BorderStyle="Solid" CssClass="table table-striped table-bordered table-hover" Font-Size="8pt" HeaderStyle-BackColor="#6699FF"
                        PageSize="25" ShowFooter="True" ShowHeaderWhenEmpty="True" Width="100%" EmptyDataText="Tiada rekod">
                        <Columns>
                            <asp:TemplateField HeaderText="Bil">
                                <ItemTemplate>
                                    <asp:Label ID="lblBil" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                </ItemTemplate>
                                <ItemStyle Width="5%" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="KodVot" HeaderText="Kod Vot" ReadOnly="True">
                                <ItemStyle Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Butiran" HeaderText="Butiran" ReadOnly="true">                                
                            </asp:BoundField>
                            <asp:BoundField DataField="Klasifikasi" HeaderText="Klasifikasi" ReadOnly="true">
                                <ItemStyle Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Jenis" HeaderText="Jenis" ReadOnly="true">
                                <ItemStyle Width="10%" />
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
                    </asp:GridView>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
   
</asp:Content>
