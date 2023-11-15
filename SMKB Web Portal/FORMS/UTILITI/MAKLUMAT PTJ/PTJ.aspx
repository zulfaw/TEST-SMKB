<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="PTJ.aspx.vb" Inherits="SMKB_Web_Portal.Daftar_Ptj" EnableEventValidation="False"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-sm-9 col-md-6">
            <p></p>
            <div class="panel panel-default" style="width:50%">
            <div class="panel-body">
            <table>
                <tr>
                    <td style="width:20%"><label class="control-label" for="">Kod PTj :</label> </td>
                    <td>
                        <asp:TextBox ID="txtKodPTJ" runat="server" class="form-control" Width="40px"></asp:TextBox>
                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtKodPTJ" ErrorMessage="" ForeColor="#820303" Text="*Perlu diisi" ValidationGroup="btnSavePTJ" Display="Dynamic" ></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td  style="width:20%"><label class="control-label" for="">Nama PTj : </td>
                    <td>
                          <asp:TextBox ID="txtButiran" runat="server" class="form-control" Width="80%"></asp:TextBox>
                          &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtButiran" ErrorMessage="" ForeColor="#820303" Text="*Perlu diisi" ValidationGroup="btnSavePTJ" Display="Dynamic" ></asp:RequiredFieldValidator>                  
                    </td>
                </tr>
                <tr>
                    <td  style="width:20%"><label class="control-label" for="">Nama Singkatan : </td>
                    <td>
                          <asp:TextBox ID="txtSingkatan" runat="server" class="form-control" Width="80%"></asp:TextBox>
                          &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSingkatan" ErrorMessage="" ForeColor="#820303" Text="*Perlu diisi" ValidationGroup="btnSavePTJ" Display="Dynamic" ></asp:RequiredFieldValidator>                  
                    </td>
                </tr>
                <tr>
                  <td  style="width:20%"><label class="control-label" for="Status">Status :</label></td>
                  <td style="width: 378px;" >
                    <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" Height="25px" Width="273px" ID="rbStatus">
                        <asp:ListItem Selected="True" Text=" Aktif" Value="0"></asp:ListItem>
                        <asp:ListItem Text=" Tidak Aktif" Value="1"></asp:ListItem>

                    </asp:RadioButtonList>

                </td>
              </tr> 
                <caption>
             
                  </tr>
                      <tr>
                          <td colspan="2" style="text-align:center">
                          <asp:Button ID="btnReset" runat="server" CssClass="btn" Text="Reset" />
                              &nbsp;&nbsp;
                              <asp:Button ID="btnSavePTJ" runat="server" CssClass="btn" Text="Simpan" ValidationGroup="btnSavePTJ" />
                          </td>
                    </tr>
              </caption>
          </table>
        </div>
        </div>

        <br />
        <div>
        <table >
            
            <tr style="height:30px;">
                <td style="width:80px;">
                    <label class="control-label" for="Klasifikasi">Saiz Rekod : </label>
                </td>
                <td style="width:50px;">
                    <asp:DropDownList ID="ddlSaizRekod" runat="server" Width="50px" AutoPostBack="true" class="form-control">
                        <asp:ListItem Text="10" Value="10" />
                        <asp:ListItem Text="25" Value="25" />
                        <asp:ListItem Text="50" Value="50" Selected="true"/>
                        <asp:ListItem Text="100" Value="100" />
                    </asp:DropDownList>
                </td>
                <td style="width:40px;"></td>
                  <td style="width:40px;">
                      <label class="control-label" for="Klasifikasi">Cari : </label>
                </td>
                
                <td style="width:110px;">
                      <asp:TextBox ID="txtCari" runat="server" class="form-control" Width="100px" ToolTip="Masukkan Nilai"></asp:TextBox>
                  </td>
                    <td style="width:110px;">
                        <asp:DropDownList ID="ddlCari" runat="server" Width="100px" class="form-control">
                            <asp:ListItem Text="Kod PTJ" Value="KodPTJ" />
                            <asp:ListItem Text="Nama PTJ" Value="Butiran" selected="True"/>
                            <asp:ListItem Text="Nama Singkatan" Value="Singkatan" />
                            <asp:ListItem Text="Status" Value="Status" />
                        </asp:DropDownList>
                    </td>
                    <td>                                  
                      <asp:LinkButton runat="server" ID="btnCari" CssClass="btn-circle-lg" ToolTip="Cari" ValidationGroup="btnCari">
                        <i class="fa fa-search" aria-hidden="true"></i>
                      </asp:LinkButton>         
                      
                  </td>
                  
              </tr>
        </table>
        </div>
        
          <asp:GridView ID="gvPTJ" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True" PageSize="10"
            cssclass="table table-striped table-bordered table-hover" HeaderStyle-BackColor="#6699FF" Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" ShowFooter="false" >
                <columns>
                <asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" SortExpression="Bil">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>                
                <asp:BoundField DataField="KodPTJ" HeaderText="Kod PTj" ReadOnly="true" SortExpression="KodPTJ">
                    <ItemStyle Width="5%" HorizontalAlign="Center"/>
                </asp:BoundField>
                <asp:TemplateField HeaderText="Nama PTj" ItemStyle-Width="30%" SortExpression="Butiran">
                    <ItemTemplate>
                        <%# Eval("Butiran") %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtButiran" runat="server" CssClass="form-control" Width="300px" Text='<%# Eval("Butiran") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Nama Singkatan" ItemStyle-Width="20%" SortExpression="Butiran">
                    <ItemTemplate>
                        <%# Eval("Singkatan") %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtSingkatan" runat="server" CssClass="form-control" Width="300px" Text='<%# Eval("Singkatan") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>                
                <asp:TemplateField HeaderText="Status" ItemStyle-Width="10%" SortExpression="Status">
                    <ItemTemplate>
                        <asp:label id="StrStatus" runat="server" Text='<%# Eval("StrStatus") %>'></asp:label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlStrStatus" runat="server" CssClass="form-control">
                            <asp:ListItem Value="1" Text="Aktif"> </asp:ListItem>
                            <asp:ListItem Value="0" Text="Tidak Aktif"> </asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:ImageButton ID="btnUpdate" runat="server" CommandName="Update" Height="18px" ImageUrl="~/Images/Save_48x48.png" 
                            ToolTip="Simpan" Width="15px" OnItemCommand="gvSubMenu_ItemCommand" />
                            &nbsp;&nbsp;
                        <asp:ImageButton ID="btnCancel" runat="server" CommandName="Cancel" Height="18px" ImageUrl="~/Images/Cancel16x16.png" 
                                ToolTip="Batal" Width="15px" OnItemCommand="gvSubMenu_ItemCommand" />
                            &nbsp;&nbsp;
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:ImageButton ID="btnEdit" runat="server" CommandName="Edit" Height="18px" ImageUrl="~/Images/Edit_48.png" 
                            ToolTip="Kemas Kini" Width="15px" OnItemCommand="gvSubMenu_ItemCommand" />
                        <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete"  ImageUrl="~/Images/Delete_32x32.png" 
                            ToolTip="Padam" Width="15px" Height="18px" OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');" />
                        </ItemTemplate>              
                    <ItemStyle Width="5%" />
                </asp:TemplateField>
            </columns>

        <HeaderStyle BackColor="#6699FF"></HeaderStyle>
    </asp:GridView>
            
      <br />

        

    </div>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
