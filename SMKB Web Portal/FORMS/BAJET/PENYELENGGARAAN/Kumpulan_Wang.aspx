<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Kumpulan_Wang.aspx.vb" Inherits="SMKB_Web_Portal.KW" EnableEventValidation ="false" %>
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

                  var txtKodKW = document.getElementById('<%=txtKodKW.ClientID%>').value
                  var txtButiran = document.getElementById('<%=txtButiran.ClientID%>').value

                  //KodKW
                  if (txtKodKW == "") {
                      blnComplete = false
                      document.getElementById("lblMsgKodKW").style.display = 'inline-block';
                  }
                  else {
                      document.getElementById("lblMsgKodKW").style.display = 'none';
                  }


                  //ButiranKW

                  if (txtButiran == "") {
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
                //if (valTxtKodKW == "")
                //{
                //    alert('Sila Masukkan Kod KW!')
                //    return false;
                //}

                //if (valTxtButiran == "")
                //{
                //    alert('Sila Masukkan Butiran !')
                //    return false;
                //}

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
            .row
            {
                width :75%;
            }

            .gv{
                width :55%;
            }

        }

        @media (max-width: 1450px) {
            .row  {
                width: 95%;
            }
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>           
            <div class="row">
    
          <div class="panel panel-default" style="margin-left :0px;" >
    <div class="panel-body" >
        <table class="nav-justified">
              <tr style="height:25px">
                  <td style="width: 10%"><label class="control-label" for="">Kod KW</label>
                  </td>
                      <td>:</td>
                      <td>
                          <asp:TextBox ID="txtKodKW" runat="server" class="form-control" Width="10%" MaxLength ="2" onkeypress="return isNumberKey(event)"></asp:TextBox>
                          &nbsp;<%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtKodKW" ErrorMessage="" ForeColor="#820303" Text="*Perlu diisi" ValidationGroup="btnSimpan" Display="Dynamic" ></asp:RequiredFieldValidator>--%><label id="lblMsgKodKW" class="control-label" for="" style="display:none;color:#820303;">(Masukkan Kod KW)
                          </label>
                          <i aria-hidden="true" class="fas fa-info-circle fa-lg" data-html="true" data-placement="right" data-toggle="tooltip" title="&nbsp;Masukkan 2 nombor sahaja.Huruf tidak dibenarkan."></i>
                      </td>
                      <caption>
                          &nbsp;
                  </caption>
              </tr>
              <tr style="height:25px">
                  <td style="height: 22px;"><label class="control-label" for="Butiran">Butiran</label> 
                      </td> 
                       <td style="height: 22px;">:</td>
                       <td>
                          <asp:TextBox ID="txtButiran" runat="server" class="form-control" Width="80%"></asp:TextBox>
                          &nbsp;<%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtButiran" ErrorMessage="" ForeColor="#820303" Text="*Perlu diisi" ValidationGroup="btnSimpan" Display="Dynamic" ></asp:RequiredFieldValidator></td>--%><label id="lblMsgButiran" class="control-label" for="" style="display:none;color:#820303;">(Masukkan Butiran KW)
                          </label>
                           </td> 
            
              </tr>
              <caption>

                  &nbsp; &nbsp; &nbsp;
                 
                  <tr style="height: 25px">
                      <td style="height: 22px;">Status<td style="height: 22px;">
                          :<td>
                              <asp:RadioButtonList ID="rbStatus" runat="server" Height="25px" RepeatDirection="Horizontal" Width="273px">
                                  <asp:ListItem Selected="True" Text=" Aktif" Value="1"></asp:ListItem>
                                  <asp:ListItem Text=" Tidak Aktif" Value="0"></asp:ListItem>
                              </asp:RadioButtonList>
                          </td>
                          <td style="height: 22px">&nbsp;</td>
                          </td>
                      </td>
                  </tr>
                 
                  <tr style="height:55px;vertical-align :bottom ">
                      <td>&nbsp; </td>
                      <td>&nbsp;</td>
                      <td>&nbsp;
                          &nbsp;<asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn " OnClientClick="return fConfirm();">
									<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
								</asp:LinkButton>
                          &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;
                          <asp:LinkButton ID="lbtnHapus" runat="server" Visible="false" CssClass="btn " OnClientClick="return confirm('Anda pasti untuk hapus rekod ini?');">
						<i class="fas fa-trash-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Hapus
					</asp:LinkButton>
                          &nbsp; &nbsp;
                          <asp:LinkButton ID="lbtnBaru" runat="server" CssClass="btn ">
									<i class="fas fa-file-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Rekod baru
								</asp:LinkButton>

                      </td>
                  </tr>
              </caption>
          </table>
        </div>
              </div>
                </div>


                <div class="row">
                <div class ="gv">
                 <div class="GvTopPanel">
                <div style="float:left;margin-top: 8px;margin-left: 10px;">
                <label class="control-label" for="">Jumlah rekod :</label>&nbsp;<label class="control-label" id ="lblJumRekod" runat="server" style="color:mediumblue;" ></label>
                    
                    </div>  
                        </div>
          <asp:GridView ID="gvKW" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" 
                                cssclass="table table-striped table-bordered table-hover" HeaderStyle-BackColor="#6699FF" Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" ShowFooter="True" Width="100%"  >
                                 <columns>
                                     <asp:TemplateField HeaderText = "Bil">
        <ItemTemplate>
            <asp:Label ID="lblBil" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
        </ItemTemplate>
                                      <ItemStyle Width="30px" />
    </asp:TemplateField>
                                    <asp:BoundField DataField="KodKW" HeaderText="Kod KW" ReadOnly="True">
                                        <ItemStyle Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Butiran" DataField="ButiranKW" ReadOnly="true">
                                    </asp:BoundField>
                                     <asp:BoundField DataField="Status" HeaderText="Status" />
                                     <asp:TemplateField>
                                         <EditItemTemplate>
                                            <asp:ImageButton ID="btnUpdate" runat="server" CommandName="Update" Height="25px" ImageUrl="~/Images/Save_48x48.png" 
                                                ToolTip="Simpan" Width="20px" OnItemCommand="gvSubMenu_ItemCommand" />
                                                &nbsp;&nbsp;
                                            <asp:ImageButton ID="btnCancel" runat="server" CommandName="Cancel" Height="25px" ImageUrl="~/Images/Cancel16x16.png" 
                                                    ToolTip="Batal" Width="20px" OnItemCommand="gvSubMenu_ItemCommand" />
                                                &nbsp;&nbsp;
                                         </EditItemTemplate>
                                         <ItemTemplate>
                                            <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="btn-xs" ToolTip="Kemaskini">
                                            <i class="fas fa-edit"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>              
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                </columns>

<HeaderStyle BackColor="#6699FF"></HeaderStyle>
                            </asp:GridView>
   </div></div>
    
            </ContentTemplate>
        </asp:UpdatePanel>



 
</asp:Content>
