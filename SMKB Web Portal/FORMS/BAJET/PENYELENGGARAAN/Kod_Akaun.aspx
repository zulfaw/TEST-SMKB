<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Kod_Akaun.aspx.vb" Inherits="SMKB_Web_Portal.Kod_Akaun" EnableEventValidation ="false"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>

    <script >
        function fConfirm() {

            try {

                var valTxtKW = document.getElementById('<%=txtKW.ClientID%>').value
                var valTxtKO = document.getElementById('<%=txtKO.ClientID%>').value
                var valTxtPTj = document.getElementById('<%=txtPTj.ClientID%>').value
                var valTxtKP = document.getElementById('<%=txtKP.ClientID%>').value
                var valTxtKodvot = document.getElementById('<%=txtKodvot.ClientID%>').value

                if (valTxtKW == "" || valTxtKO == "" || valTxtPTj == "" || valTxtKP == "" || valTxtKodvot == "")
                {
                    alert('Sila pilih rekod yang hendak dikemas kini!')
                    return false;
                }



                if (confirm('Anda pasti untuk kemas kini rekod vot ini?')) {
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
                 <div class="panel panel-default" style="width:760px;">
                     <div class="panel-body">
                         <table class="table-responsive">
                             <tr>
                                 <td style="width:100px;">Tahun</td>
                                 <td>:</td>
                                 <td>
                                     <asp:TextBox ID="txtTahun" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 100px;"></asp:TextBox>
                                 </td>
                             </tr>
                             <tr>
                                 <td style="height: 25px;">
                                     <label class="control-label" for="Jenis">
                                         Kumpulan Wang</label></td>
                                 <td style="height: 25px;">:</td>
                                 <td>
                                     <asp:TextBox ID="txtKW" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 350px;"></asp:TextBox>
                                 </td>
                             </tr>
                             <tr>
                                 <td style="height: 25px;">
                                     <label class="control-label" for="Jenis">Kod Operasi</label></td>
                                 <td style="height: 25px;">:</td>
                                 <td>
                                     <asp:TextBox ID="txtKO" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 250px;"></asp:TextBox>
                                 </td>
                             </tr>
                             <tr>
                                 <td style="height: 25px;">
                                     <label class="control-label" for="Jenis">PTJ</label></td>
                                 <td style="height: 25px;">:</td>
                                 <td>
                                     <asp:TextBox ID="txtPTj" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 600px;"></asp:TextBox>
                                 </td>
                             </tr>
                             <tr>
                                 <td style="height: 25px;">
                                     <label class="control-label" for="Jenis">Kod Projek</label></td>
                                 <td style="height: 25px;">:</td>
                                 <td>
                                     <asp:TextBox ID="txtKP" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 600px;"></asp:TextBox>
                                 </td>
                             </tr>
                             <tr>
                                 <td style="height: 25px;">
                                     <label class="control-label" for="Jenis">Kod Vot</label></td>
                                 <td style="height: 25px;">:</td>
                                 <td>
                                     <asp:TextBox ID="txtKodvot" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 500px;"></asp:TextBox>
                                 </td>
                             </tr>
                             <tr>
                                 <td style="height: 25px;">
                                     <label class="control-label" for="Status">Status</label></td>
                                 <td style="height: 25px;">:</td>
                                 <td style="height: 22px; width: 378px;">
                                     <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" Height="25px" Width="273px" ID="rbStatus">
                                         <asp:ListItem Selected="True" Text=" Aktif" Value="1"></asp:ListItem>
                                         <asp:ListItem Text=" Tidak Aktif" Value="0"></asp:ListItem>
                                     </asp:RadioButtonList>

                                 </td>
                             </tr>

                             <tr style="height: 55px; vertical-align: bottom">
                                 <td>&nbsp; </td>
                                 <td>&nbsp;</td>
                                 <td style="width: 378px">&nbsp;&nbsp;&nbsp;
                          
                          
                          <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn " OnClientClick="return fConfirm()">
									<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                          </asp:LinkButton>
                                     &nbsp;&nbsp;
                          
                                 </td>
                             </tr>
                         </table>
                     </div>
                 </div>

             </div>



             <div class="row">
                 <div style="margin-left: 20px;width: 800px;">
                     <div class="GvTopPanel">
                         <div style="float: left; margin-top: 8px; margin-left: 10px;">
                             <label class="control-label" for="">Jumlah rekod :</label>&nbsp;<label class="control-label" id="lblJumRekod" runat="server" style="color: mediumblue;"></label>
                         </div>

                     </div>
                     <asp:GridView ID="gvKodAkaun" runat="server" EmptyDataText=" " ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True"
                         CssClass="table table-striped table-bordered table-hover" Width="100%" HeaderStyle-BackColor="#6699FF" Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" ShowFooter="True" PageSize="25">
                         <Columns>
                             <asp:TemplateField HeaderText="Bil">
                                 <ItemTemplate>
                                     <asp:Label ID="lblBil" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                 </ItemTemplate>
                                 <ItemStyle Width="5%" HorizontalAlign="Center" />
                             </asp:TemplateField>
                             <%--<asp:BoundField DataField="KodKW" HeaderText="KW">
                                     <ItemStyle Width="15%" HorizontalAlign="Center" />
                                    </asp:BoundField>--%>
                             <asp:TemplateField>
                                 <HeaderTemplate>
                                     <asp:Label ID="lblKodKW" runat="server" Text="KW" />&nbsp;
                            <asp:LinkButton ID="lnkKodKW" runat="server" CommandName="Sort" CommandArgument="KodKW"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                 </HeaderTemplate>
                                 <ItemTemplate>
                                     <asp:Label ID="lblKodKW" runat="server" Text='<%# Eval("KodKW")%>' />
                                 </ItemTemplate>
                             </asp:TemplateField>
                             <%-- <asp:BoundField DataField="kodKO" HeaderText="KO" >
                                     <ItemStyle HorizontalAlign="Center" Width="15%" />
                                     </asp:BoundField>--%>
                             <asp:TemplateField>
                                 <HeaderTemplate>
                                     <asp:Label ID="lblKodKO" runat="server" Text="KO" />&nbsp;
                            <asp:LinkButton ID="lnkKodKO" runat="server" CommandName="Sort" CommandArgument="kodKO"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                 </HeaderTemplate>
                                 <ItemTemplate>
                                     <asp:Label ID="lblKodKO" runat="server" Text='<%# Eval("kodKO")%>' />
                                 </ItemTemplate>
                             </asp:TemplateField>
                             <%--  <asp:BoundField HeaderText="PTJ" DataField="KodPTj">
                                         <ItemStyle Width="15%" HorizontalAlign="Center" />
                                     <ItemStyle Width="10%" />
                                    </asp:BoundField>--%>
                             <asp:TemplateField>
                                 <HeaderTemplate>
                                     <asp:Label ID="lblPTj" runat="server" Text="PTj" />&nbsp;
                            <asp:LinkButton ID="lnkPTj" runat="server" CommandName="Sort" CommandArgument="KodPTj"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                 </HeaderTemplate>
                                 <ItemTemplate>
                                     <asp:Label ID="lblPTj" runat="server" Text='<%# Eval("KodPTj")%>' />
                                 </ItemTemplate>
                             </asp:TemplateField>
                             <%--<asp:BoundField HeaderText="KP" DataField="kodKP">
                                     <ItemStyle HorizontalAlign="Center" Width="15%" />
                                    </asp:BoundField>--%>
                             <asp:TemplateField>
                                 <HeaderTemplate>
                                     <asp:Label ID="lblKodKP" runat="server" Text="KP" />&nbsp;
                            <asp:LinkButton ID="lnkKodKP" runat="server" CommandName="Sort" CommandArgument="kodKP"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                 </HeaderTemplate>
                                 <ItemTemplate>
                                     <asp:Label ID="lblKodKP" runat="server" Text='<%# Eval("kodKP")%>' />
                                 </ItemTemplate>
                             </asp:TemplateField>
                             <%--<asp:BoundField HeaderText="Kod Vot" DataField="kodVot">
                                     <ItemStyle HorizontalAlign="Center" Width="20%" />
                                    </asp:BoundField>--%>
                             <asp:TemplateField>
                                 <HeaderTemplate>
                                     <asp:Label ID="lblKodVot" runat="server" Text="KP" />&nbsp;
                            <asp:LinkButton ID="lnkKodVot" runat="server" CommandName="Sort" CommandArgument="kodVot"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                 </HeaderTemplate>
                                 <ItemTemplate>
                                     <asp:Label ID="lblKodVot" runat="server" Text='<%# Eval("kodVot")%>' />
                                 </ItemTemplate>
                             </asp:TemplateField>
                             <%--  <asp:BoundField HeaderText="Status" DataField="Status">
                                         <ItemStyle Width="15%" HorizontalAlign="Center" />
                                    </asp:BoundField>--%>
                             <asp:TemplateField>
                                 <HeaderTemplate>
                                     <asp:Label ID="lblStatus" runat="server" Text="KP" />&nbsp;
                            <asp:LinkButton ID="lnkStatus" runat="server" CommandName="Sort" CommandArgument="Status"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                 </HeaderTemplate>
                                 <ItemTemplate>
                                     <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status")%>' />
                                 </ItemTemplate>
                             </asp:TemplateField>
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
                                 <ItemStyle Width="3%" />
                             </asp:TemplateField>
                         </Columns>

                         <HeaderStyle BackColor="#6699FF"></HeaderStyle>
                     </asp:GridView>
                 </div>
             </div>



         </ContentTemplate></asp:UpdatePanel>
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
