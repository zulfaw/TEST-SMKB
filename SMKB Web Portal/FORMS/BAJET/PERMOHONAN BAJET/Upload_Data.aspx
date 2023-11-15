<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Upload_Data.aspx.vb" Inherits="SMKB_Web_Portal.Upload_Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>

    <script>       
         function fConfirm() {

              try {

                  debugger

                var blnComplete = true;

                var txtKodDsr = document.getElementById('<%=txtKodDsr.ClientID%>').value
                var txtButiranDsr = document.getElementById('<%=txtButiranDsr.ClientID%>').value

                  //Kod Dasar
                  if (txtKodDsr == "") {
                      blnComplete = false
                      document.getElementById("lblMsgKodDsr").style.display = 'inline-block';
                  }
                  else {
                      document.getElementById("lblMsgKodDsr").style.display = 'none';
                  }


                  //Butiran Dasar
                  if (txtButiranDsr == "") {
                      blnComplete = false
                      document.getElementById("lblMsgButiran").style.display = 'inline-block';
                  }
                  else {
                      document.getElementById("lblMsgButiran").style.display = 'none';
                  }
                
                  if (blnComplete == false) {
                      alert('Sila lengkapkan maklumat!')
                      return false;
                  }
               

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
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>            
            <div class="row">
                <div class="panel panel-default well" style="width:800px">
                    <div class="panel-body">
                        <table class="nav-justified">

                            <tr style="height: 35px">
                                <td style="height: 22px;width:70px;">
                                    <label class="control-label" for="">Kod Dasar</label></td>
                                <td style="height: 22px;width:10px;">:</td>
                                <td>
                                    <asp:TextBox ID="txtKodDsr" runat="server" class="form-control" Width="20%" onkeypress="return fCheckChar(event,this);" MaxLength="3"></asp:TextBox>
                                    &nbsp;<%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtKodDsr" ForeColor="#820303" Text="*Perlu diisi" ValidationGroup="btnSave" Display="Dynamic" ></asp:RequiredFieldValidator>--%><label id="lblMsgKodDsr" class="control-label" for="" style="display: none; color: #820303;">(Masukkan Kod Dasar)
                                    </label>
                                    <i aria-hidden="true" class="fas fa-info-circle fa-lg" data-html="true" data-placement="right" data-toggle="tooltip" title="&nbsp;Masukkan 3 aksara."></i>
                                </td>
                            </tr>

                            <tr style="height: 25px">
                                <td style="height: 22px; vertical-align: top;">
                                    <label class="control-label" for="">Butiran</label>
                                </td>
                                <td style="height: 22px; vertical-align: top;">:</td>
                                <td>
                                    <asp:TextBox ID="txtButiranDsr" runat="server" class="form-control" Width="80%"></asp:TextBox>
                                    &nbsp;<%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtButiranDsr" ForeColor="#820303" Text="*Perlu diisi" ValidationGroup="btnSave" Display="Dynamic" ></asp:RequiredFieldValidator>--%><label id="lblMsgButiran" class="control-label" for="" style="display: none; color: #820303;">(Masukkan Butiran Dasar)
                                    </label>
                                </td>
                            </tr>
                            <tr style="height: 25px">
                                <td style="height: 22px; vertical-align: top;">Status</td>
                                <td style="height: 22px; vertical-align: top;">:</td>
                                <td>
                                    <asp:RadioButtonList ID="rbStatus" runat="server" Height="25px" RepeatDirection="Horizontal" Width="273px">
                                        <asp:ListItem Selected="True" Text=" Aktif" Value="1"></asp:ListItem>
                                        <asp:ListItem Text=" Tidak Aktif" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>

                            <tr style="height: 25px">
                                <td style="height: 22px; vertical-align: top;">&nbsp;</td>
                                <td style="height: 22px; vertical-align: top;">&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr style="height: 25px">
                                <td style="height: 22px; vertical-align: top;">&nbsp;</td>
                                <td style="height: 22px; vertical-align: top;">&nbsp;</td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>Select File </td>
                                            <td>
                                                <asp:FileUpload ID="FileUpload2" runat="server" />
                                            </td>
                                            <td></td>
                                            <td>
                                                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Upload" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                            <tr style="height: 55px; vertical-align: bottom">
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td style="text-align: left">
                                    <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn " OnClientClick="return fConfirm();">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                                    </asp:LinkButton>
                                    &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                          <asp:LinkButton ID="lbtnHapus" runat="server" Visible="false" CssClass="btn " OnClientClick="return confirm('Anda pasti untuk hapus rekod ini?');">
						<i class="fas fa-trash-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Hapus
                          </asp:LinkButton>
                                    &nbsp; &nbsp;
                          <asp:LinkButton ID="lbtnBaru" runat="server" CssClass="btn ">
						<i class="fas fa-file-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Rekod Baru
                          </asp:LinkButton>
                                </td>
                            </tr>

                        </table>
                    </div>
                </div>

            </div>



        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
