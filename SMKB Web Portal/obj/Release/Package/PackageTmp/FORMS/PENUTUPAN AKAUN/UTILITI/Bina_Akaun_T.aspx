<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Bina_Akaun_T.aspx.vb" Inherits="SMKB_Web_Portal.Bina_Akaun_T" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">

        <ContentTemplate>

            <div class="panel panel-default">

                <div class="panel-body">
                    <div class="alert alert-success" id="divSucc" visible="false" runat="server">
                        <strong><i class="fas fa-check-circle fa-lg"></i></strong>&nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Text="Label">Carta akaun telah dibina!</asp:Label>
                    </div>
                    <div class="alert alert-danger" id="divErr" runat="server" visible="false">
                        <strong><i class="fas fa-exclamation-circle fa-lg"></i></strong>&nbsp;&nbsp;<asp:Label ID="lblErr" runat="server" Text="Label" />
                    </div>

                    <table class="table table-borderless table-striped" >
                        <tr style="height: 25px">
                            <td>
                                <label class="control-label" for="">
                                    KW :
                                </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlKW1" InitialValue="0" ErrorMessage="" ForeColor="#820303" Text="*Sila pilih" ValidationGroup="btnProses" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="ddlKW1" runat="server" AutoPostBack="True" CssClass="form-control" />
                            </td>
                            <td>
                                <label class="control-label" for="">
                                    Hingga :
                                </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlKW2" InitialValue="0" ErrorMessage="" ForeColor="#820303" Text="*Sila pilih" ValidationGroup="btnProses" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="ddlKW2" runat="server" AutoPostBack="True" CssClass="form-control" />
                            </td>
                        </tr>
                        <tr style="height: 10px">
                            <td>&nbsp;</td>
                            <td></td>
                        </tr>
                        <tr style="height: 25px">
                            <td>
                                <label class="control-label" for="">
                                    Kod Operasi :
                                </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlKO1" InitialValue="0" ErrorMessage="" ForeColor="#820303" Text="*Sila pilih" ValidationGroup="btnProses" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="ddlKO1" runat="server" AutoPostBack="True" CssClass="form-control" />
                            </td>
                            <td>
                                <label class="control-label" for="">
                                    Hingga :
                                </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlKO2" InitialValue="0" ErrorMessage="" ForeColor="#820303" Text="*Sila pilih" ValidationGroup="btnProses" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="ddlKO2" runat="server" AutoPostBack="True" CssClass="form-control" />
                            </td>
                        </tr>
                        <tr style="height: 10px">
                            <td></td>
                            <td></td>
                        </tr>
                        <tr style="height: 25px">
                            <td>
                                <label class="control-label" for="">
                                    PTj :
                                </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlPTj1" InitialValue="0" ErrorMessage="" ForeColor="#820303" Text="*Sila pilih" ValidationGroup="btnProses" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="ddlPTj1" runat="server" AutoPostBack="True" CssClass="form-control" Width="70%" />
                            </td>
                            <td>
                                <label class="control-label" for="">
                                    Hingga :
                                </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlPTj2" InitialValue="0" ErrorMessage="" ForeColor="#820303" Text="*Sila pilih" ValidationGroup="btnProses" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="ddlPTj2" runat="server" AutoPostBack="True" CssClass="form-control" Width="70%"/>
                            </td>
                        </tr>
                        <tr style="height: 10px">
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>Senarai Semua Kod Projek :

                                    <div>
                                        Jumlah rekod :&nbsp;
                            <asp:Label ID="lblJumRekodS" runat="server" Style="color: mediumblue;"></asp:Label>

                                        &nbsp;&nbsp;   <b style="color: #969696;">|</b> &nbsp;&nbsp;

                            Saiz Rekod : 
                            <asp:DropDownList ID="ddlSaizRekod" runat="server" AutoPostBack="True" class="form-control">
                                <asp:ListItem Text="10" Value="10" Selected="True" />
                                <asp:ListItem Text="25" Value="25" />
                                <asp:ListItem Text="50" Value="50" />
                                <asp:ListItem Text="100" Value="100" />
                                <asp:ListItem Text="500" Value="500" />
                            </asp:DropDownList>
                                        <br />
                                        Carian : 
                           &nbsp;&nbsp;
                               <asp:TextBox ID="txtCari" runat="server" CssClass="form-control" Width="100px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtCari" runat="server" ControlToValidate="txtCari" Display="Dynamic" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="lbtnCari"></asp:RequiredFieldValidator>
                                        &nbsp;
                               <asp:DropDownList ID="ddlCari" runat="server" class="form-control">
                                   <asp:ListItem Text="Keseluruhan" Value="1" Selected="True" />
                                   <asp:ListItem Text="Kod Projek" Value="2" />
                                   <asp:ListItem Text="Nama Projek" Value="3" />
                               </asp:DropDownList>
                                        &nbsp;&nbsp;&nbsp;
                               <asp:LinkButton ID="lbtnCari" runat="server" CssClass="btn-circle" ToolTip="Cari" ValidationGroup="lbtnCari">
                        <i class="fa fa-search fa-lg" aria-hidden="true"></i>
                               </asp:LinkButton>

                                    </div>
                                <br />
                                <asp:GridView ID="gvKP1" runat="server" PageSize="10" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" EmptyDataText="Tiada rekod" Font-Size="8pt" ShowFooter="false" ShowHeaderWhenEmpty="True">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Bil">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Kod Projek" SortExpression="KodProjek" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center" DataField="KodProjek"></asp:BoundField>
                                        <asp:BoundField HeaderText="Butiran" SortExpression="Butiran" HeaderStyle-CssClass="centerAlign" DataField="Butiran"></asp:BoundField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CssClass="btn-xs" ToolTip="Pindah ke senarai pilihan">
											<i class="fas fa-forward"></i>
                                                </asp:LinkButton>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <SelectedRowStyle ForeColor="Blue" />
                                </asp:GridView>
                            </td>
                            <td style="vertical-align: top">Senarai Kod Projek yang Dipilih :
                                    <div style="height: 33px;">
                                        Jumlah rekod :&nbsp;
                            <asp:Label ID="lblJumRekodS2" runat="server" Style="color: mediumblue;"></asp:Label>
                                    </div>
                                <asp:GridView ID="gvKp2" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" EmptyDataText="Tiada rekod" Font-Size="8pt" ShowFooter="false" ShowHeaderWhenEmpty="True">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Bil">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Kod Projek" SortExpression="KodProjek" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center" DataField="KodProjek"></asp:BoundField>
                                        <asp:BoundField HeaderText="Butiran" SortExpression="Butiran" HeaderStyle-CssClass="centerAlign" DataField="Butiran"></asp:BoundField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CssClass="btn-xs" ToolTip="Padam">
										                <i class="far fa-trash-alt"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <SelectedRowStyle ForeColor="Blue" />
                                </asp:GridView>
                            </td>
                            <%--<label class="control-label" for="">
                                        Kod Projek :
                                    </label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlKP1" InitialValue="0" ErrorMessage="" ForeColor="#820303" Text="*Sila pilih" ValidationGroup="btnProses" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlKP1" runat="server" AutoPostBack="True" CssClass="form-control" Style="width: 95%;">
                                    </asp:DropDownList>--%>
                        </tr>
                        <tr style="height: 10px">
                            <td></td>
                            <td></td>
                        </tr>
                        <tr style="height: 25px">
                            <td>
                                <label class="control-label" for="">
                                    Vot :
                                </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlVot1" InitialValue="0" ErrorMessage="" ForeColor="#820303" Text="*Sila pilih" ValidationGroup="btnProses" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="ddlVot1" runat="server" AutoPostBack="True" CssClass="form-control" Width="70%"/>
                            </td>
                            <td>
                                <label class="control-label" for="">
                                    Hingga :
                                </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlVot2" InitialValue="0" ErrorMessage="" ForeColor="#820303" Text="*Sila pilih" ValidationGroup="btnProses" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="ddlVot2" runat="server" AutoPostBack="True" CssClass="form-control" Width="70%" />
                            </td>
                        </tr>
                        <tr style="height: 55px; vertical-align: bottom; text-align: center">
                            <td colspan="2">&nbsp; &nbsp;<asp:Button ID="btnProses" runat="server" CssClass="btn" Text="Proses" ValidationGroup="btnProses" />
                                &nbsp;&nbsp; &nbsp;&nbsp; 
                          
                            </td>
                        </tr>


                    </table>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
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
