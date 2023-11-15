<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Kod_Operasi.aspx.vb" Inherits="SMKB_Web_Portal.Kod_Operasi" %>
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
                var blnComplete = true;

                var txtKodKO = document.getElementById('<%=txtKodKO.ClientID%>').value
                  var txtNamaKodKO = document.getElementById('<%=txtNamaKodKO.ClientID%>').value

                  //Kod Operasi
                  if (txtKodKO == "") {
                      blnComplete = false
                      document.getElementById("lblMsgKodKO").style.display = 'inline-block';
                  }
                  else {
                      document.getElementById("lblMsgKodKO").style.display = 'none';
                  }


                  //Butiran KO

                  if (txtNamaKodKO == "") {
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
            <div class="row">
                <div class="panel panel-default" style="width:700px;">
                    <div class="panel-body">
                        <table class="table-responsive">

                            <tr style="height: 35px">
                                <td style="height: 22px;">
                                    <label class="control-label" for="">Kod Operasi</label>
                                </td>
                                <td style="height: 22px;">:</td>
                                <td>
                                    <asp:TextBox ID="txtKodKO" runat="server" class="form-control" Width="20%" MaxLength="2" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    &nbsp;<%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtKodKO" ForeColor="#820303" Text="*Perlu diisi" ValidationGroup="btnSaveModul" Display="Dynamic" ></asp:RequiredFieldValidator>--%><label id="lblMsgKodKO" class="control-label" for="" style="display: none; color: #820303;">(Masukkan Kod Operasi)
                                    </label>
                                    <i aria-hidden="true" class="fas fa-info-circle fa-lg" data-html="true" data-placement="right" data-toggle="tooltip" title="&nbsp;Masukkan 2 nombor sahaja.Huruf tidak dibenarkan."></i>
                                </td>
                            </tr>

                            <tr style="height: 25px">
                                <td style="height: 22px; vertical-align: top;">
                                    <label class="control-label" for="">Butiran</label>
                                </td>
                                <td style="height: 22px; vertical-align: top;">:</td>
                                <td style="height: 22px">
                                    <asp:TextBox ID="txtNamaKodKO" runat="server" class="form-control" Width="80%"></asp:TextBox>
                                    &nbsp;<%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNamaKodKO" ForeColor="#820303" Text="*Perlu diisi" ValidationGroup="btnSaveModul" Display="Dynamic" ></asp:RequiredFieldValidator>--%><label id="lblMsgButiran" class="control-label" for="" style="display: none; color: #820303;">(Masukkan Butiran KO)
                                    </label>
                                </td>
                            </tr>
                            <tr style="height: 25px;">
                                <td style="height: 22px; vertical-align: top;">Status</td>
                                <td style="height: 22px; vertical-align: top;">:</td>
                                <td style="height: 22px;">
                                    <asp:RadioButtonList ID="rbStatus" runat="server" Height="25px" RepeatDirection="Horizontal" Width="273px">
                                        <asp:ListItem Selected="True" Text=" Aktif" Value="1"></asp:ListItem>
                                        <asp:ListItem Text=" Tidak Aktif" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>

                                </td>
                            </tr>

                            <tr style="height: 55px; vertical-align: bottom">
                                <td>&nbsp; </td>
                                <td>&nbsp;</td>
                                <td style="text-align: left">
                                    <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn " OnClientClick="return fConfirm();">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                                    </asp:LinkButton>
                                    &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                           <asp:LinkButton ID="lbtnHapus" runat="server" CssClass="btn " OnClientClick="return confirm('Anda pasti untuk hapus rekod ini?');" Visible="false">
						<i class="fas fa-trash-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Hapus
                           </asp:LinkButton>
                                    &nbsp;&nbsp;			  
                             <asp:LinkButton ID="lbtnBaru" runat="server" CssClass="btn ">
						<i class="fas fa-file-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Rekod Baru
                             </asp:LinkButton>

                                </td>                              
                            </tr>

                        </table>
                    </div>
                </div>
            </div>

            <div class="row">
                <div style="margin-left: 20px; width: 700px;">
                    <div class="GvTopPanel">
                        <div style="float: left; margin-top: 8px; margin-left: 10px;">
                            <label class="control-label" for="">Jumlah rekod :</label>&nbsp;<label class="control-label" id="lblJumRekod" runat="server" style="color: mediumblue;"></label>
                        </div>
                    </div>
                    <asp:GridView ID="gvKodOperasi" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" EmptyDataText=" "
                        BorderColor="#333333" BorderStyle="Solid" CssClass="table table-striped table-bordered table-hover" Font-Size="8pt" PageSize="25" ShowHeaderWhenEmpty="True" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Bil">
                                <ItemTemplate>
                                    <asp:Label ID="lblBil" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                </ItemTemplate>
                                <ItemStyle Width="8%" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="KodOperasi" HeaderText="Kod Operasi" ReadOnly="True" SortExpression="KodOperasi">
                                <ItemStyle Width="17%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Butiran" HeaderText="Butiran" SortExpression="NamaModTerima">
                                <ItemStyle Width="50%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Status" HeaderText="Status">
                                <ItemStyle Width="20%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="btn-xs" ToolTip="Pilih">
                                            <i class="fas fa-edit"></i>
                                    </asp:LinkButton>
                                </ItemTemplate>

                                <ItemStyle Width="10%" />
                            </asp:TemplateField>

                        </Columns>
                        <HeaderStyle BackColor="#6699FF" />
                        <RowStyle Height="5px" />
                        <SelectedRowStyle ForeColor="Blue" />
                    </asp:GridView>
                </div>
             </div>
        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>
