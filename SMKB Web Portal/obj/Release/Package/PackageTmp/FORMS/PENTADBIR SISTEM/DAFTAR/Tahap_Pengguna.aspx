<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Tahap_Pengguna.aspx.vb" Inherits="SMKB_Web_Portal.Tahap_Pengguna" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
  <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>
    <link rel="stylesheet" type="text/css" href="../../../Content/Site.css">

     
         

                    <div class="row">
                        <div class="panel panel-default" style="width: 80%;">
                            <div class="panel-heading">Senarai Staf</div>
                            <div class="panel-body">

                                <div style="margin-bottom:20px;">
                                    <table class="nav-justified">
                                        <tr style="height: 25px">
                                            <td style="width: 116px">
                                                <label class="control-label" for="">Jabatan:</label></td>
                                            <td>
                                                <asp:DropDownList ID="ddlJabatan" runat="server" AutoPostBack="True" CssClass="form-control" Style="width: 280px;">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </div>

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
                    Status : &nbsp;
                    <asp:DropDownList ID="ddlTapisan" runat="server" Width="150px" class="form-control" AutoPostBack="true">
                                   <asp:ListItem Text="- KESELURUHAN -" Value="0" Selected="True" />
                                   <asp:ListItem Text="Nama" Value="1" />
                                   <asp:ListItem Text="No Staf" Value="2" />
                               </asp:DropDownList>
                                        &nbsp;
                                        <asp:TextBox ID="txtCarian" runat="server" CssClass="form-control" Width="150px" Enabled="false"></asp:TextBox>                          
                            &nbsp;&nbsp;&nbsp;


                             <asp:LinkButton ID="lbtnCari" runat="server" CssClass="btnNone" ToolTip="Cari" style="padding:4px;background-color: lightgrey;">
                        <i class="fa fa-search btn-xs" aria-hidden="true"></i>
                               </asp:LinkButton>
                                    </div>

                                </div>

                                <asp:GridView ID="gvStaf" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True" PageSize="20"
                                    CssClass="table table-striped table-bordered table-hover" Width="100%" Font-Size="8pt" BorderStyle="Solid" EmptyDataText=" ">
                                    <Columns>
                                        <asp:BoundField DataField="Bil" HeaderText="Bil" SortExpression="Bil" ReadOnly="True">
                                            <ItemStyle Width="2%" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="No. Staf" DataField="NoStaf" SortExpression="NoStaf"></asp:BoundField>

                                        <asp:BoundField HeaderText="Nama Staf" DataField="NamaStaf" SortExpression="NamaStaf"></asp:BoundField>
                                        <asp:BoundField HeaderText="Jawatan" DataField="Jawatan" SortExpression="Jawatan"></asp:BoundField>
                                        <asp:BoundField HeaderText="Tahap Pengguna" DataField="TahapPengguna" SortExpression="TahapPengguna"></asp:BoundField>
                                        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                                        <asp:TemplateField>

                                            <%--<InsertItemTemplate>                
                <asp:LinkButton runat="server" ID="lnkInsert" CommandName="Insert" Text="Simpan" ToolTip="Sna" />
                <asp:LinkButton runat="server" ID="lnkCancel" CommandName="Cancel" Text="Batal" ToolTip="Fu" />
            </InsertItemTemplate>--%>

                                            <EditItemTemplate>
                                                <%-- <asp:LinkButton runat="server" ID="lnkUpdate" CommandName="Update" Text="Simpan" ToolTip="Simpan" />
             <asp:LinkButton runat="server" ID="lnkCancel" CommandName="Cancel" Text="Batal" ToolTip="Batal" />--%>

                                                <asp:ImageButton ID="btnSave1" runat="server" CommandName="Update" Height="25px" ImageUrl="~/Images/Save_48x48.png"
                                                    ToolTip="Simpan" Width="20px" OnItemCommand="gvPeruntukan_ItemCommand" />
                                                &nbsp;&nbsp;
                 <asp:ImageButton ID="btnCancel" runat="server" CommandName="Cancel" Height="25px" ImageUrl="~/Images/Cancel16x16.png"
                     ToolTip="Batal" Width="20px" OnItemCommand="gvPeruntukan_ItemCommand" />
                                                &nbsp;&nbsp;


                                            </EditItemTemplate>

                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Select" CssClass="btn-xs" ToolTip="Pilih">
								<i class="far fa-edit fa-lg"></i>
                                                </asp:LinkButton>
                                                &nbsp;
                                                <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete" CssClass="btn-xs" ToolTip="Padam"
                                                    OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');">
										                    <i class="far fa-trash-alt fa-lg"></i>
                                                </asp:LinkButton>

                                            </ItemTemplate>
                                            <ItemStyle Width="8%" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="#6699FF" />
                                    <RowStyle Height="5px" />
                                    <SelectedRowStyle ForeColor="#0000FF" />
                                </asp:GridView>

                            </div>
                        </div>
                    </div>

            

                <div class="row">
                         <div class="panel panel-default" style="width:80%">
                            <div class="panel-body">
                                <table class="nav-justified">
              <tr style="height:25px">
                  <td style="width: 70px"><label class="control-label" for="">Nama Staf :</label></td>
                  <td><asp:TextBox runat="server" ReadOnly="True" BackColor="#FFFFCC" Width="60%" ID="txtNamaStaf" class="form-control"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="rfvSave1" runat="server" ControlToValidate="txtNamaStaf" ErrorMessage="" ForeColor="#820303" Text="*Pilih Staf" ValidationGroup="btnSave" Display="Dynamic"  ></asp:RequiredFieldValidator>
</td>             
              </tr>
            <tr style="height:25px">
                  <td style="width: 116px"><label class="control-label" for="">No. Staf:</label></td>
                  <td><asp:TextBox runat="server" ReadOnly="True" BackColor="#FFFFCC" Width="15%" ID="txtNoStaf" class="form-control"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="rfvSave2" runat="server" ControlToValidate="txtNoStaf" ErrorMessage="" ForeColor="#820303" Text="*Pilih Staf" ValidationGroup="btnSave" Display="Dynamic"  ></asp:RequiredFieldValidator>
                          </td>             
              </tr>
            <tr style="height:25px">
                  <td style="width: 116px"><label class="control-label" for="">Tahap Lama:</label></td>
                  <td><asp:TextBox runat="server" ReadOnly="True" BackColor="#FFFFCC" Width="60%" ID="txtTahapLama" class="form-control"></asp:TextBox>
</td>             
              </tr>
            <tr style="height:25px">
                  <td style="width: 116px"><label class="control-label" for="">Tahap Baru:</label></td>
                  <td>
                         <asp:DropDownList ID="ddlTahapBaru" runat="server" AutoPostBack="True" CssClass="form-control" style="width: 60%;">
                         </asp:DropDownList>
                      &nbsp;<asp:RequiredFieldValidator ID="rfvSave3" runat="server" ControlToValidate="ddlTahapBaru" ErrorMessage="" ForeColor="#820303" Text="*Pilih Tahap baru" ValidationGroup="btnSave" Display="Dynamic"  ></asp:RequiredFieldValidator>
                     </td>             
              </tr>
            <tr style="height:35px"><td></td> <td></td></tr>
            <tr style="height:25px">
                  <td ></td>
                  <td><asp:Button runat="server"  Text="Simpan"  CssClass="btn" ID="btnSave" OnClick = "OnConfirm" ValidationGroup="btnSave" ></asp:Button>    
                     </td>             
              </tr>
          </table>
                            </div>
                        </div>
                     </div>

                </ContentTemplate>
        </asp:UpdatePanel>
    


</asp:Content>
