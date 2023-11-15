<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Unit_Ukuran.aspx.vb" Inherits="SMKB_Web_Portal.Unit_Ukuran" %>

<asp:content id="Content1" contentplaceholderid="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
            <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>   
                <div class="row">    
                    <div class="col-sm-6 col-md-9">
                        <asp:Label ID="lblKod" runat="server" Text="Kod : "></asp:Label>
                        <asp:TextBox ID="txtBoxKod" runat="server" MaxLength="2" Width="35px" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorKod" runat="server" ControlToValidate="txtBoxKod" ErrorMessage="Kod diperlukan" ForeColor="Red" Text="*" ValidationGroup="btnClick" Display="Dynamic"></asp:RequiredFieldValidator>
                        &nbsp &nbsp &nbsp 
                        <asp:Label ID="lblButiran" runat="server" Text="Butiran : "></asp:Label>
                        <asp:TextBox ID="txtBoxButiran" runat="server" Width="178px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorButiran" runat="server" ControlToValidate="txtBoxButiran" ErrorMessage="Butiran diperlukan" ForeColor="Red" Text="*" ValidationGroup="btnClick" Display="Dynamic"></asp:RequiredFieldValidator>
                        &nbsp &nbsp &nbsp 
                        <asp:Button ID="btnTambah" runat="server" Text="Tambah" ValidationGroup="btnClick" CausesValidation="False" />
                        &nbsp &nbsp &nbsp
                        <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
                        <br />
                        <br />
                        </div>
                    </div>
                    <div class="row">    
                    <div class="col-sm-6 col-md-9">
                        <asp:GridView ID="gvUnitUkuran" runat="server" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True"
                        CssClass= "table table-striped table-bordered table-hover" font-Size="8pt" Width="65%" HeaderStyle-BackColor="#6699FF" Height="829px" PageSize="50">
                            <columns>
                                <%--<asp:TemplateField ItemStyle-Width="10%" HeaderText="Bil">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCount" runat="server" Text='1'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:BoundField HeaderText="Bil" DataField="Bil" SortExpression="Bil" >
                                    <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Kod" DataField="KodUkuran" SortExpression="KodUkuran" >
                                    <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Butiran" DataField="Butiran" SortExpression="Butiran" >
                                    <ItemStyle Width="30%" />
                                </asp:BoundField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEdit" runat="server" CommandName="Edit" Height="13px" ImageUrl="~/Images/Edit_48.png" 
                                            ToolTip="Kemas Kini" Width="20px" OnItemCommand="gvUnitUkuran_RowEditing" />
                                        &nbsp;&nbsp;
                                        <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" Height="13px" ImageUrl="~/Images/Delete_32x32.png" 
                                            ToolTip="Padam" Width="20px" OnClientClick="return confirm('Are you sure you want to delete this item?');" />
                                    </ItemTemplate>
              
                                    <EditItemTemplate>              
                                        <asp:LinkButton runat="server" ID="lnkUpdate" CommandName="Update" Text="Update" ToolTip="Update" />
                                        <asp:LinkButton runat="server" ID="lnkCancel" CommandName="Cancel" Text="Cancel" ToolTip="Cancel" />
                                    </EditItemTemplate>
            
                                <%--<InsertItemTemplate>                
                                    <asp:LinkButton runat="server" ID="lnkInsert" CommandName="Insert" Text="Insert" ToolTip="Sna" />
                                    <asp:LinkButton runat="server" ID="lnkCancel" CommandName="Cancel" Text="Cancel" ToolTip="Fu" />
                                </InsertItemTemplate>--%>
                                    <ItemStyle Width="10%" />
                                </asp:TemplateField>
                            </columns>
                            <HeaderStyle BackColor="#6699FF" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:content>
