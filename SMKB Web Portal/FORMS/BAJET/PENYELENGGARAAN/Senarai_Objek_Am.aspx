<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Senarai_Objek_Am.aspx.vb" Inherits="SMKB_Web_Portal.Senarai_Objek_Am" EnableEventValidation ="false"  %>
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

                var ddlKodKW = document.getElementById('<%=ddlKodKW.ClientID%>');
                var intSelKW = ddlKodKW.selectedIndex

                var ddlObjekAm = document.getElementById('<%=ddlObjekAm.ClientID%>');
                var intSelObjAm = ddlObjekAm.selectedIndex

                //Kod KW
                if (intSelKW == 0 || intSelKW == -1) {
                    blnComplete = false
                    document.getElementById("lblMsgKodKW").style.display = 'inline-block';
                }
                else {
                    document.getElementById("lblMsgKodKW").style.display = 'none';
                }


                //Objek AM
                if (intSelObjAm == 0 || intSelObjAm == -1) {
                    blnComplete = false
                    document.getElementById("lblMsgObjAm").style.display = 'inline-block';
                }
                else {
                    document.getElementById("lblMsgObjAm").style.display = 'none';
                }

                if (blnComplete == false) {
                    alert('Sila lengkapkan maklumat!')
                    return false;
                }

               <%-- var ddlKodKW = document.getElementById('<%=ddlKodKW.ClientID%>');
                var intSelKW = ddlKodKW.selectedIndex;
                if (intSelKW == 0) {
                    alert('Sila Pilih Kod KW')
                    return false;
                }
                
                var ddlObjekAm = document.getElementById('<%=ddlObjekAm.ClientID%>');
                var intSelObjAm = ddlObjekAm.selectedIndex;
                if (intSelObjAm == 0) {
                    alert('Sila Pilih Objek Am')
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
                width: 80%;
            }        
        }

        @media (max-width: 1310px) {
              .panel {
                  width: 90%;
              }
          }

          @media (max-width: 1050px) {
              .panel {
                  width: 100%;
              }
          }
       
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <table class="nav-justified">
                            <tr>
                                <td style="width: 12%; height: 27px;">
                                    <label class="control-label" for="KodKW">Kod KW :</label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlKodKW" runat="server" AutoPostBack="True" CssClass="form-control" Style="width: 70%">
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;
                          <label id="lblMsgKodKW" class="control-label" for="" style="display: none; color: #820303;">
                              (Pilih Kod KW)
                          </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 27px;">
                                    <label class="control-label" for="ObjekAm">Objek Am :</label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlObjekAm" runat="server" AutoPostBack="True" CssClass="form-control" Style="width: 80%;">
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;
                          <label id="lblMsgObjAm" class="control-label" for="" style="display: none; color: #820303;">
                              (Pilih Objek AM)
                          </label>
                                </td>
                            </tr>

                            <caption>
                                &nbsp; &nbsp; &nbsp;
                 
                  <tr>
                      <td style="height: 27px;">Status :</td>
                      <td>
                          <asp:RadioButtonList ID="rbStatus" runat="server" Height="25px" RepeatDirection="Horizontal" Width="273px">
                              <asp:ListItem Selected="True" Text=" Aktif" Value="1"></asp:ListItem>
                              <asp:ListItem Text=" Tidak Aktif" Value="0"></asp:ListItem>
                          </asp:RadioButtonList>
                      </td>
                  </tr>

                                <tr style="height: 55px; vertical-align: bottom">
                                    <td>&nbsp; </td>
                                    <td>
                                        <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn " OnClientClick="return fConfirm();">
									<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                                        </asp:LinkButton>
                                        &nbsp;&nbsp;&nbsp; &nbsp;
                          <asp:LinkButton ID="lbtnHapus" runat="server" Visible="false" CssClass="btn " OnClientClick="return confirm('Anda pasti untuk hapus rekod ini?');">
						<i class="fas fa-trash-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Hapus
                          </asp:LinkButton>
                                        &nbsp;&nbsp;
                          <asp:LinkButton ID="lbtnBaru" runat="server" CssClass="btn " ToolTip="Kosongkan Butiran Perbelanjaan">
									<i class="fas fa-file-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Rekod baru
                          </asp:LinkButton>

                                    </td>
                                </tr>
                            </caption>
                        </table>
                    </div>
                </div>
                <div class="row">
                    <div class="GvTopPanel">
                        <div style="float: left; margin-top: 8px; margin-left: 10px;">
                            <label class="control-label" for="">Jumlah rekod :</label>&nbsp;<label class="control-label" id="lblJumRekod" runat="server" style="color: mediumblue;"></label>
                        </div>
                    </div>
                    <asp:GridView ID="gvObjekAm" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True" PageSize="20" EmptyDataText=" "
                        CssClass="table table-striped table-bordered table-hover" Width="100%" HeaderStyle-BackColor="#6699FF" Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" ShowFooter="True">
                        <Columns>
                            <asp:TemplateField HeaderText="Bil">
                                <ItemTemplate>
                                    <asp:Label ID="lblBil" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                </ItemTemplate>
                                <ItemStyle Width="30px" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Kod KW" DataField="KodKW" ReadOnly="true">
                                <ItemStyle Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Objek Am" DataField="ObjAm" ReadOnly="true"></asp:BoundField>
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
                        </Columns>

                        <HeaderStyle BackColor="#6699FF"></HeaderStyle>
                    </asp:GridView>

                </div>
            </div>
        </ContentTemplate> </asp:UpdatePanel>
</asp:Content>
